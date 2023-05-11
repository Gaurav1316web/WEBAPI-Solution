<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucItemBalance
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucItemBalance))
        Me.lblMrp = New System.Windows.Forms.Label
        Me.HeaderMRP = New System.Windows.Forms.Label
        Me.lblLocationName = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblBalance = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblOrderQty = New System.Windows.Forms.Label
        Me.OrderHeader = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.btnDrillDown = New Telerik.WinControls.UI.RadButton
        Me.lblUOM = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.btnOrder_Qty = New Telerik.WinControls.UI.RadButton
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.btnDrillDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOrder_Qty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblMrp
        '
        Me.lblMrp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMrp.Location = New System.Drawing.Point(611, 23)
        Me.lblMrp.Name = "lblMrp"
        Me.lblMrp.Size = New System.Drawing.Size(95, 14)
        Me.lblMrp.TabIndex = 3
        Me.lblMrp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMrp.Visible = False
        '
        'HeaderMRP
        '
        Me.HeaderMRP.AutoSize = True
        Me.HeaderMRP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HeaderMRP.Location = New System.Drawing.Point(533, 23)
        Me.HeaderMRP.Name = "HeaderMRP"
        Me.HeaderMRP.Size = New System.Drawing.Size(31, 14)
        Me.HeaderMRP.TabIndex = 2
        Me.HeaderMRP.Text = "MRP"
        Me.HeaderMRP.Visible = False
        '
        'lblLocationName
        '
        Me.lblLocationName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationName.Location = New System.Drawing.Point(75, 23)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(278, 14)
        Me.lblLocationName.TabIndex = 7
        Me.lblLocationName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocationName.UseCompatibleTextRendering = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(54, 14)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Location"
        '
        'lblBalance
        '
        Me.lblBalance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalance.Location = New System.Drawing.Point(75, 44)
        Me.lblBalance.Name = "lblBalance"
        Me.lblBalance.Size = New System.Drawing.Size(95, 14)
        Me.lblBalance.TabIndex = 5
        Me.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBalance.UseCompatibleTextRendering = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(533, 44)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 14)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Available Qty"
        '
        'lblOrderQty
        '
        Me.lblOrderQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrderQty.Location = New System.Drawing.Point(423, 44)
        Me.lblOrderQty.Name = "lblOrderQty"
        Me.lblOrderQty.Size = New System.Drawing.Size(95, 14)
        Me.lblOrderQty.TabIndex = 9
        Me.lblOrderQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblOrderQty.Visible = False
        '
        'OrderHeader
        '
        Me.OrderHeader.AutoSize = True
        Me.OrderHeader.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OrderHeader.Location = New System.Drawing.Point(361, 44)
        Me.OrderHeader.Name = "OrderHeader"
        Me.OrderHeader.Size = New System.Drawing.Size(60, 14)
        Me.OrderHeader.TabIndex = 8
        Me.OrderHeader.Text = "Order Qty"
        Me.OrderHeader.Visible = False
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(266, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 14)
        Me.Label4.TabIndex = 11
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label4.UseCompatibleTextRendering = True
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(616, 44)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 14)
        Me.Label7.TabIndex = 13
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label7.UseCompatibleTextRendering = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(4, 44)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 14)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Qty on Hand"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.btnOrder_Qty)
        Me.RadGroupBox1.Controls.Add(Me.btnDrillDown)
        Me.RadGroupBox1.Controls.Add(Me.lblOrderQty)
        Me.RadGroupBox1.Controls.Add(Me.OrderHeader)
        Me.RadGroupBox1.Controls.Add(Me.Label4)
        Me.RadGroupBox1.Controls.Add(Me.lblBalance)
        Me.RadGroupBox1.Controls.Add(Me.Label5)
        Me.RadGroupBox1.Controls.Add(Me.Label7)
        Me.RadGroupBox1.Controls.Add(Me.Label9)
        Me.RadGroupBox1.Controls.Add(Me.lblUOM)
        Me.RadGroupBox1.Controls.Add(Me.Label11)
        Me.RadGroupBox1.Controls.Add(Me.lblLocationName)
        Me.RadGroupBox1.Controls.Add(Me.Label8)
        Me.RadGroupBox1.Controls.Add(Me.lblMrp)
        Me.RadGroupBox1.Controls.Add(Me.HeaderMRP)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = " "
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(710, 65)
        Me.RadGroupBox1.TabIndex = 1
        Me.RadGroupBox1.Text = " "
        '
        'btnDrillDown
        '
        Me.btnDrillDown.Image = CType(resources.GetObject("btnDrillDown.Image"), System.Drawing.Image)
        Me.btnDrillDown.ImageAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDrillDown.Location = New System.Drawing.Point(169, 42)
        Me.btnDrillDown.Name = "btnDrillDown"
        Me.btnDrillDown.Size = New System.Drawing.Size(91, 18)
        Me.btnDrillDown.TabIndex = 14
        Me.btnDrillDown.Text = "Commited Qty"
        Me.btnDrillDown.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUOM
        '
        Me.lblUOM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUOM.Location = New System.Drawing.Point(423, 23)
        Me.lblUOM.Name = "lblUOM"
        Me.lblUOM.Size = New System.Drawing.Size(95, 14)
        Me.lblUOM.TabIndex = 9
        Me.lblUOM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(361, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(32, 14)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "UOM"
        '
        'btnOrder_Qty
        '
        Me.btnOrder_Qty.Image = CType(resources.GetObject("btnOrder_Qty.Image"), System.Drawing.Image)
        Me.btnOrder_Qty.ImageAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.btnOrder_Qty.Location = New System.Drawing.Point(326, 42)
        Me.btnOrder_Qty.Name = "btnOrder_Qty"
        Me.btnOrder_Qty.Size = New System.Drawing.Size(91, 18)
        Me.btnOrder_Qty.TabIndex = 15
        Me.btnOrder_Qty.Text = "Order Qty"
        Me.btnOrder_Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ucItemBalance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RadGroupBox1)
        Me.MaximumSize = New System.Drawing.Size(710, 65)
        Me.MinimumSize = New System.Drawing.Size(710, 65)
        Me.Name = "ucItemBalance"
        Me.Size = New System.Drawing.Size(710, 65)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.btnDrillDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOrder_Qty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents lblLocationName As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents lblBalance As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents lblMrp As System.Windows.Forms.Label
    Private WithEvents HeaderMRP As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents lblOrderQty As System.Windows.Forms.Label
    Private WithEvents OrderHeader As System.Windows.Forms.Label
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Private WithEvents lblUOM As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnDrillDown As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnOrder_Qty As Telerik.WinControls.UI.RadButton

End Class
