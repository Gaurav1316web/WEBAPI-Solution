<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptActiveUser
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmsaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.txtPurchasedLicence = New common.MyNumBox()
        Me.lblConvRate = New common.Controls.MyLabel()
        Me.txtReserveForMCC = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtActiveUser = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtRemainingUser = New common.MyNumBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPurchasedLicence, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReserveForMCC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtActiveUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemainingUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(711, 20)
        Me.RadMenu1.TabIndex = 2
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Setting"
        Me.RadMenuItem1.AccessibleName = "Setting"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmsaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'rmsaveLayout
        '
        Me.rmsaveLayout.AccessibleDescription = "Save Layout"
        Me.rmsaveLayout.AccessibleName = "Save Layout"
        Me.rmsaveLayout.Name = "rmsaveLayout"
        Me.rmsaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.rmDeleteLayout.AccessibleName = "Delete Layout"
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Gv1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(711, 351)
        Me.SplitContainer1.SplitterDistance = 317
        Me.SplitContainer1.TabIndex = 3
        '
        'Gv1
        '
        Me.Gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.Gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Gv1.ForeColor = System.Drawing.Color.Black
        Me.Gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Gv1.Location = New System.Drawing.Point(0, 34)
        '
        'Gv1
        '
        Me.Gv1.MasterTemplate.AllowAddNewRow = False
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.Name = "Gv1"
        Me.Gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(711, 283)
        Me.Gv1.TabIndex = 0
        Me.Gv1.Text = "RadGridView1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtRemainingUser)
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Controls.Add(Me.txtActiveUser)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.txtReserveForMCC)
        Me.Panel1.Controls.Add(Me.MyLabel1)
        Me.Panel1.Controls.Add(Me.txtPurchasedLicence)
        Me.Panel1.Controls.Add(Me.lblConvRate)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(711, 34)
        Me.Panel1.TabIndex = 1
        '
        'btnExp
        '
        Me.btnExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExp.Location = New System.Drawing.Point(155, 4)
        Me.btnExp.Name = "btnExp"
        Me.btnExp.Size = New System.Drawing.Size(83, 22)
        Me.btnExp.TabIndex = 157
        Me.btnExp.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.AccessibleDescription = "Excel"
        Me.rmiExcel.AccessibleName = "Excel"
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        '
        'rmiPDF
        '
        Me.rmiPDF.AccessibleDescription = "PDF"
        Me.rmiPDF.AccessibleName = "PDF"
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(624, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 153
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(3, 4)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 151
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(79, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 152
        Me.btnReset.Text = "Reset"
        '
        'txtPurchasedLicence
        '
        Me.txtPurchasedLicence.BackColor = System.Drawing.Color.White
        Me.txtPurchasedLicence.CalculationExpression = Nothing
        Me.txtPurchasedLicence.DecimalPlaces = 2
        Me.txtPurchasedLicence.FieldCode = Nothing
        Me.txtPurchasedLicence.FieldDesc = Nothing
        Me.txtPurchasedLicence.FieldMaxLength = 0
        Me.txtPurchasedLicence.FieldName = Nothing
        Me.txtPurchasedLicence.isCalculatedField = False
        Me.txtPurchasedLicence.IsSourceFromTable = False
        Me.txtPurchasedLicence.IsSourceFromValueList = False
        Me.txtPurchasedLicence.IsUnique = False
        Me.txtPurchasedLicence.Location = New System.Drawing.Point(110, 6)
        Me.txtPurchasedLicence.MendatroryField = False
        Me.txtPurchasedLicence.MyLinkLable1 = Nothing
        Me.txtPurchasedLicence.MyLinkLable2 = Nothing
        Me.txtPurchasedLicence.Name = "txtPurchasedLicence"
        Me.txtPurchasedLicence.ReadOnly = True
        Me.txtPurchasedLicence.ReferenceFieldDesc = Nothing
        Me.txtPurchasedLicence.ReferenceFieldName = Nothing
        Me.txtPurchasedLicence.ReferenceTableName = Nothing
        Me.txtPurchasedLicence.Size = New System.Drawing.Size(75, 20)
        Me.txtPurchasedLicence.TabIndex = 140
        Me.txtPurchasedLicence.Text = "1"
        Me.txtPurchasedLicence.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPurchasedLicence.Value = 1.0R
        '
        'lblConvRate
        '
        Me.lblConvRate.FieldName = Nothing
        Me.lblConvRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConvRate.Location = New System.Drawing.Point(3, 8)
        Me.lblConvRate.Name = "lblConvRate"
        Me.lblConvRate.Size = New System.Drawing.Size(103, 16)
        Me.lblConvRate.TabIndex = 141
        Me.lblConvRate.Text = "Purchased Licence"
        '
        'txtReserveForMCC
        '
        Me.txtReserveForMCC.BackColor = System.Drawing.Color.White
        Me.txtReserveForMCC.CalculationExpression = Nothing
        Me.txtReserveForMCC.DecimalPlaces = 2
        Me.txtReserveForMCC.FieldCode = Nothing
        Me.txtReserveForMCC.FieldDesc = Nothing
        Me.txtReserveForMCC.FieldMaxLength = 0
        Me.txtReserveForMCC.FieldName = Nothing
        Me.txtReserveForMCC.isCalculatedField = False
        Me.txtReserveForMCC.IsSourceFromTable = False
        Me.txtReserveForMCC.IsSourceFromValueList = False
        Me.txtReserveForMCC.IsUnique = False
        Me.txtReserveForMCC.Location = New System.Drawing.Point(297, 6)
        Me.txtReserveForMCC.MendatroryField = False
        Me.txtReserveForMCC.MyLinkLable1 = Nothing
        Me.txtReserveForMCC.MyLinkLable2 = Nothing
        Me.txtReserveForMCC.Name = "txtReserveForMCC"
        Me.txtReserveForMCC.ReadOnly = True
        Me.txtReserveForMCC.ReferenceFieldDesc = Nothing
        Me.txtReserveForMCC.ReferenceFieldName = Nothing
        Me.txtReserveForMCC.ReferenceTableName = Nothing
        Me.txtReserveForMCC.Size = New System.Drawing.Size(75, 20)
        Me.txtReserveForMCC.TabIndex = 142
        Me.txtReserveForMCC.Text = "1"
        Me.txtReserveForMCC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtReserveForMCC.Value = 1.0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(189, 8)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(104, 16)
        Me.MyLabel1.TabIndex = 143
        Me.MyLabel1.Text = "Reserved For MCC"
        '
        'txtActiveUser
        '
        Me.txtActiveUser.BackColor = System.Drawing.Color.White
        Me.txtActiveUser.CalculationExpression = Nothing
        Me.txtActiveUser.DecimalPlaces = 2
        Me.txtActiveUser.FieldCode = Nothing
        Me.txtActiveUser.FieldDesc = Nothing
        Me.txtActiveUser.FieldMaxLength = 0
        Me.txtActiveUser.FieldName = Nothing
        Me.txtActiveUser.isCalculatedField = False
        Me.txtActiveUser.IsSourceFromTable = False
        Me.txtActiveUser.IsSourceFromValueList = False
        Me.txtActiveUser.IsUnique = False
        Me.txtActiveUser.Location = New System.Drawing.Point(450, 6)
        Me.txtActiveUser.MendatroryField = False
        Me.txtActiveUser.MyLinkLable1 = Nothing
        Me.txtActiveUser.MyLinkLable2 = Nothing
        Me.txtActiveUser.Name = "txtActiveUser"
        Me.txtActiveUser.ReadOnly = True
        Me.txtActiveUser.ReferenceFieldDesc = Nothing
        Me.txtActiveUser.ReferenceFieldName = Nothing
        Me.txtActiveUser.ReferenceTableName = Nothing
        Me.txtActiveUser.Size = New System.Drawing.Size(75, 20)
        Me.txtActiveUser.TabIndex = 144
        Me.txtActiveUser.Text = "1"
        Me.txtActiveUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtActiveUser.Value = 1.0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(376, 8)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel2.TabIndex = 145
        Me.MyLabel2.Text = "Active Users"
        '
        'txtRemainingUser
        '
        Me.txtRemainingUser.BackColor = System.Drawing.Color.White
        Me.txtRemainingUser.CalculationExpression = Nothing
        Me.txtRemainingUser.DecimalPlaces = 2
        Me.txtRemainingUser.FieldCode = Nothing
        Me.txtRemainingUser.FieldDesc = Nothing
        Me.txtRemainingUser.FieldMaxLength = 0
        Me.txtRemainingUser.FieldName = Nothing
        Me.txtRemainingUser.isCalculatedField = False
        Me.txtRemainingUser.IsSourceFromTable = False
        Me.txtRemainingUser.IsSourceFromValueList = False
        Me.txtRemainingUser.IsUnique = False
        Me.txtRemainingUser.Location = New System.Drawing.Point(626, 6)
        Me.txtRemainingUser.MendatroryField = False
        Me.txtRemainingUser.MyLinkLable1 = Nothing
        Me.txtRemainingUser.MyLinkLable2 = Nothing
        Me.txtRemainingUser.Name = "txtRemainingUser"
        Me.txtRemainingUser.ReadOnly = True
        Me.txtRemainingUser.ReferenceFieldDesc = Nothing
        Me.txtRemainingUser.ReferenceFieldName = Nothing
        Me.txtRemainingUser.ReferenceTableName = Nothing
        Me.txtRemainingUser.Size = New System.Drawing.Size(75, 20)
        Me.txtRemainingUser.TabIndex = 146
        Me.txtRemainingUser.Text = "1"
        Me.txtRemainingUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRemainingUser.Value = 1.0R
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(529, 8)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(93, 16)
        Me.MyLabel3.TabIndex = 147
        Me.MyLabel3.Text = "Remaining Users"
        '
        'rptActiveUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(711, 371)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "rptActiveUser"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Active Users"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPurchasedLicence, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReserveForMCC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtActiveUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemainingUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmsaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExp As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtRemainingUser As common.MyNumBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtActiveUser As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtReserveForMCC As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtPurchasedLicence As common.MyNumBox
    Friend WithEvents lblConvRate As common.Controls.MyLabel
End Class

