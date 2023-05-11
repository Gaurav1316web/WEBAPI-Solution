<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCarousal
    Inherits Telerik.WinControls.UI.RadForm

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
        Dim CarouselBezierPath1 As Telerik.WinControls.UI.CarouselBezierPath = New Telerik.WinControls.UI.CarouselBezierPath
        Me.RadCarousel1 = New Telerik.WinControls.UI.RadCarousel
        Me.AdministrativeServices = New Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
        Me.CommonServices = New Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
        Me.Receivables = New Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
        Me.Payables = New Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
        Me.GeneralLedger = New Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
        Me.SalesAndDistribution = New Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
        Me.MaterialManagment = New Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
        Me.PurchaseOrderMain = New Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
        Me.TaxDeducted = New Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
        Me.FixedAssets = New Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
        Me.HRandPayroll = New Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
        Me.Production = New Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
        Me.BusinessIntelligence = New Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
        Me.Utility = New Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
        CType(Me.RadCarousel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadCarousel1
        '
        Me.RadCarousel1.AutoLoopPauseCondition = Telerik.WinControls.UI.AutoLoopPauseConditions.OnMouseOverItem
        Me.RadCarousel1.AutoScroll = True
        Me.RadCarousel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadCarousel1.BackgroundImage = Global.ERP.My.Resources.Resources.administrator
        Me.RadCarousel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        CarouselBezierPath1.CtrlPoint1 = New Telerik.WinControls.UI.Point3D(14, 76, 70)
        CarouselBezierPath1.CtrlPoint2 = New Telerik.WinControls.UI.Point3D(86, 76, 70)
        CarouselBezierPath1.FirstPoint = New Telerik.WinControls.UI.Point3D(1.4444444444444444, 10.065645514223196, 0)
        CarouselBezierPath1.LastPoint = New Telerik.WinControls.UI.Point3D(96.111111111111114, 10.065645514223196, 0)
        CarouselBezierPath1.ZScale = 500
        Me.RadCarousel1.CarouselPath = CarouselBezierPath1
        Me.RadCarousel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadCarousel1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.AdministrativeServices, Me.CommonServices, Me.Receivables, Me.Payables, Me.GeneralLedger, Me.SalesAndDistribution, Me.MaterialManagment, Me.PurchaseOrderMain, Me.TaxDeducted, Me.FixedAssets, Me.HRandPayroll, Me.Production, Me.BusinessIntelligence, Me.Utility})
        Me.RadCarousel1.Location = New System.Drawing.Point(0, 0)
        Me.RadCarousel1.Name = "RadCarousel1"
        Me.RadCarousel1.SelectedIndex = 0
        Me.RadCarousel1.Size = New System.Drawing.Size(900, 457)
        Me.RadCarousel1.TabIndex = 12
        Me.RadCarousel1.Text = "RadCarousel1"
        Me.RadCarousel1.VisibleItemCount = 5
        '
        'AdministrativeServices
        '
        Me.AdministrativeServices.AccessibleDescription = "System Administrator"
        Me.AdministrativeServices.AccessibleName = "System Administrator"
        Me.AdministrativeServices.Image = Global.ERP.My.Resources.Resources.administrator
        Me.AdministrativeServices.Name = "AdministrativeServices"
        Me.AdministrativeServices.ShowBorder = False
        Me.AdministrativeServices.Text = "System Administrator"
        Me.AdministrativeServices.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.AdministrativeServices.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.AdministrativeServices.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'CommonServices
        '
        Me.CommonServices.AccessibleDescription = "Common Services"
        Me.CommonServices.AccessibleName = "Common Services"
        Me.CommonServices.Image = Global.ERP.My.Resources.Resources.comService
        Me.CommonServices.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CommonServices.Name = "CommonServices"
        Me.CommonServices.ShowBorder = False
        Me.CommonServices.StretchHorizontally = True
        Me.CommonServices.Text = "Common Services"
        Me.CommonServices.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CommonServices.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'Receivables
        '
        Me.Receivables.AccessibleDescription = "Receipt"
        Me.Receivables.AccessibleName = "Receipt"
        Me.Receivables.Image = Global.ERP.My.Resources.Resources.RECEIVABLES
        Me.Receivables.Name = "Receivables"
        Me.Receivables.ShowBorder = False
        Me.Receivables.Text = "Receivables"
        Me.Receivables.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Receivables.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'Payables
        '
        Me.Payables.AccessibleDescription = "Payment"
        Me.Payables.AccessibleName = "Payment"
        Me.Payables.Image = Global.ERP.My.Resources.Resources.PAYMENT
        Me.Payables.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.Payables.Name = "Payables"
        Me.Payables.ShowBorder = False
        Me.Payables.Text = "Payables"
        Me.Payables.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Payables.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'GeneralLedger
        '
        Me.GeneralLedger.AccessibleDescription = "General Legder"
        Me.GeneralLedger.AccessibleName = "General Legder"
        Me.GeneralLedger.Image = Global.ERP.My.Resources.Resources.GENERAL_LEDGERS
        Me.GeneralLedger.Name = "GeneralLedger"
        Me.GeneralLedger.ShowBorder = False
        Me.GeneralLedger.Text = "General Legder"
        Me.GeneralLedger.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.GeneralLedger.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'SalesAndDistribution
        '
        Me.SalesAndDistribution.AccessibleDescription = "Sale And Distribution"
        Me.SalesAndDistribution.AccessibleName = "Sale And Distribution"
        Me.SalesAndDistribution.Image = Global.ERP.My.Resources.Resources.SALE
        Me.SalesAndDistribution.Name = "SalesAndDistribution"
        Me.SalesAndDistribution.ShowBorder = False
        Me.SalesAndDistribution.Text = "Sale And Distribution"
        Me.SalesAndDistribution.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.SalesAndDistribution.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'MaterialManagment
        '
        Me.MaterialManagment.AccessibleDescription = "GridGroupHeaderItemButtonElement1"
        Me.MaterialManagment.AccessibleName = "GridGroupHeaderItemButtonElement1"
        Me.MaterialManagment.Image = Global.ERP.My.Resources.Resources.INVENTORY
        Me.MaterialManagment.Name = "MaterialManagment"
        Me.MaterialManagment.ShowBorder = False
        Me.MaterialManagment.Text = "Material Management"
        Me.MaterialManagment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.MaterialManagment.UseSystemSkin = Telerik.WinControls.UseSystemSkinMode.NoInheritable
        Me.MaterialManagment.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'PurchaseOrderMain
        '
        Me.PurchaseOrderMain.AccessibleDescription = "GridGroupHeaderItemButtonElement1"
        Me.PurchaseOrderMain.AccessibleName = "GridGroupHeaderItemButtonElement1"
        Me.PurchaseOrderMain.Image = Global.ERP.My.Resources.Resources.PURCHASE
        Me.PurchaseOrderMain.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.PurchaseOrderMain.Name = "PurchaseOrderMain"
        Me.PurchaseOrderMain.ShowBorder = False
        Me.PurchaseOrderMain.Text = "Purchase"
        Me.PurchaseOrderMain.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.PurchaseOrderMain.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'TaxDeducted
        '
        Me.TaxDeducted.AccessibleDescription = "Tax Deduction at Source"
        Me.TaxDeducted.AccessibleName = "Tax Deduction at Source"
        Me.TaxDeducted.Image = Global.ERP.My.Resources.Resources.tax_png
        Me.TaxDeducted.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.TaxDeducted.Name = "TaxDeducted"
        Me.TaxDeducted.ShowBorder = False
        Me.TaxDeducted.Text = "Tax Deduction at Source"
        Me.TaxDeducted.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.TaxDeducted.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'FixedAssets
        '
        Me.FixedAssets.AccessibleDescription = "Fixed Asset"
        Me.FixedAssets.AccessibleName = "Fixed Asset"
        Me.FixedAssets.Image = Global.ERP.My.Resources.Resources.FIXED_ASSET
        Me.FixedAssets.Name = "FixedAssets"
        Me.FixedAssets.ShowBorder = False
        Me.FixedAssets.Text = "Fixed Asset"
        Me.FixedAssets.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.FixedAssets.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'HRandPayroll
        '
        Me.HRandPayroll.AccessibleDescription = "HR And Payroll"
        Me.HRandPayroll.AccessibleName = "HR And Payroll"
        Me.HRandPayroll.Image = Global.ERP.My.Resources.Resources.HR_AND_PAYROLL
        Me.HRandPayroll.Name = "HRandPayroll"
        Me.HRandPayroll.ShowBorder = False
        Me.HRandPayroll.Text = "HR And Payroll"
        Me.HRandPayroll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.HRandPayroll.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'Production
        '
        Me.Production.AccessibleDescription = "Production"
        Me.Production.AccessibleName = "Production"
        Me.Production.Image = Global.ERP.My.Resources.Resources.PRODUCTION
        Me.Production.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.Production.Name = "Production"
        Me.Production.ShowBorder = False
        Me.Production.Text = "Production"
        Me.Production.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Production.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'BusinessIntelligence
        '
        Me.BusinessIntelligence.AccessibleDescription = "Business Inteligence"
        Me.BusinessIntelligence.AccessibleName = "Business Inteligence"
        Me.BusinessIntelligence.Image = Global.ERP.My.Resources.Resources.BUSINESS_INTELLIGENCE
        Me.BusinessIntelligence.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.BusinessIntelligence.Name = "BusinessIntelligence"
        Me.BusinessIntelligence.ShowBorder = False
        Me.BusinessIntelligence.Text = "Business Intelligence"
        Me.BusinessIntelligence.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BusinessIntelligence.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'Utility
        '
        Me.Utility.AccessibleDescription = "Utility"
        Me.Utility.AccessibleName = "Utility"
        Me.Utility.Image = Global.ERP.My.Resources.Resources.Utility
        Me.Utility.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.Utility.Name = "Utility"
        Me.Utility.ShowBorder = False
        Me.Utility.Text = "Utility"
        Me.Utility.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Utility.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'FrmCarousal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(900, 457)
        Me.Controls.Add(Me.RadCarousel1)
        Me.Name = "FrmCarousal"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Menu"
        CType(Me.RadCarousel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadCarousel1 As Telerik.WinControls.UI.RadCarousel
    Friend WithEvents MaterialManagment As Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
    Friend WithEvents Payables As Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
    Friend WithEvents Production As Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
    Friend WithEvents SalesAndDistribution As Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
    Friend WithEvents PurchaseOrderMain As Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
    Friend WithEvents Receivables As Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
    Friend WithEvents GeneralLedger As Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
    Friend WithEvents HRandPayroll As Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
    Friend WithEvents BusinessIntelligence As Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
    Friend WithEvents FixedAssets As Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
    Friend WithEvents AdministrativeServices As Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
    Friend WithEvents CommonServices As Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
    Friend WithEvents TaxDeducted As Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
    Friend WithEvents Utility As Telerik.WinControls.UI.GridGroupHeaderItemButtonElement
End Class

