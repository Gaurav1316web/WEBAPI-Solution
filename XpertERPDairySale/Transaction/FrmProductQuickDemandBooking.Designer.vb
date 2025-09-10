<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProductQuickDemandBooking
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
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.txtAddress = New common.Controls.MyTextBox()
        Me.lblAddress = New common.Controls.MyLabel()
        Me.txtDistributor = New common.Controls.MyTextBox()
        Me.lblDistributor = New common.Controls.MyLabel()
        Me.txtBoothName = New common.Controls.MyTextBox()
        Me.lblBoothName = New common.Controls.MyLabel()
        Me.rgbTaxType = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnIceCream = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnProduct = New Telerik.WinControls.UI.RadRadioButton()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.txtAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBoothName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBoothName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgbTaxType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbTaxType.SuspendLayout()
        CType(Me.rbtnIceCream, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(947, 450)
        Me.SplitContainer1.SplitterDistance = 414
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtAddress)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblAddress)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDistributor)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDistributor)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtBoothName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBoothName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rgbTaxType)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(947, 414)
        Me.SplitContainer2.SplitterDistance = 93
        Me.SplitContainer2.TabIndex = 0
        '
        'txtAddress
        '
        Me.txtAddress.CalculationExpression = Nothing
        Me.txtAddress.FieldCode = Nothing
        Me.txtAddress.FieldDesc = Nothing
        Me.txtAddress.FieldMaxLength = 0
        Me.txtAddress.FieldName = Nothing
        Me.txtAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.isCalculatedField = False
        Me.txtAddress.IsSourceFromTable = False
        Me.txtAddress.IsSourceFromValueList = False
        Me.txtAddress.IsUnique = False
        Me.txtAddress.Location = New System.Drawing.Point(501, 11)
        Me.txtAddress.MaxLength = 50
        Me.txtAddress.MendatroryField = False
        Me.txtAddress.Modified = True
        Me.txtAddress.MyLinkLable1 = Nothing
        Me.txtAddress.MyLinkLable2 = Nothing
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ReadOnly = True
        Me.txtAddress.ReferenceFieldDesc = Nothing
        Me.txtAddress.ReferenceFieldName = Nothing
        Me.txtAddress.ReferenceTableName = Nothing
        Me.txtAddress.Size = New System.Drawing.Size(214, 18)
        Me.txtAddress.TabIndex = 1542
        '
        'lblAddress
        '
        Me.lblAddress.FieldName = Nothing
        Me.lblAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress.Location = New System.Drawing.Point(448, 12)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(48, 16)
        Me.lblAddress.TabIndex = 1541
        Me.lblAddress.Text = "Address"
        '
        'txtDistributor
        '
        Me.txtDistributor.CalculationExpression = Nothing
        Me.txtDistributor.FieldCode = Nothing
        Me.txtDistributor.FieldDesc = Nothing
        Me.txtDistributor.FieldMaxLength = 0
        Me.txtDistributor.FieldName = Nothing
        Me.txtDistributor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDistributor.isCalculatedField = False
        Me.txtDistributor.IsSourceFromTable = False
        Me.txtDistributor.IsSourceFromValueList = False
        Me.txtDistributor.IsUnique = False
        Me.txtDistributor.Location = New System.Drawing.Point(285, 10)
        Me.txtDistributor.MaxLength = 50
        Me.txtDistributor.MendatroryField = False
        Me.txtDistributor.Modified = True
        Me.txtDistributor.MyLinkLable1 = Nothing
        Me.txtDistributor.MyLinkLable2 = Nothing
        Me.txtDistributor.Name = "txtDistributor"
        Me.txtDistributor.ReadOnly = True
        Me.txtDistributor.ReferenceFieldDesc = Nothing
        Me.txtDistributor.ReferenceFieldName = Nothing
        Me.txtDistributor.ReferenceTableName = Nothing
        Me.txtDistributor.Size = New System.Drawing.Size(156, 18)
        Me.txtDistributor.TabIndex = 1540
        '
        'lblDistributor
        '
        Me.lblDistributor.FieldName = Nothing
        Me.lblDistributor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDistributor.Location = New System.Drawing.Point(222, 13)
        Me.lblDistributor.Name = "lblDistributor"
        Me.lblDistributor.Size = New System.Drawing.Size(58, 16)
        Me.lblDistributor.TabIndex = 1539
        Me.lblDistributor.Text = "Distributor"
        '
        'txtBoothName
        '
        Me.txtBoothName.CalculationExpression = Nothing
        Me.txtBoothName.FieldCode = Nothing
        Me.txtBoothName.FieldDesc = Nothing
        Me.txtBoothName.FieldMaxLength = 0
        Me.txtBoothName.FieldName = Nothing
        Me.txtBoothName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoothName.isCalculatedField = False
        Me.txtBoothName.IsSourceFromTable = False
        Me.txtBoothName.IsSourceFromValueList = False
        Me.txtBoothName.IsUnique = False
        Me.txtBoothName.Location = New System.Drawing.Point(80, 11)
        Me.txtBoothName.MaxLength = 50
        Me.txtBoothName.MendatroryField = False
        Me.txtBoothName.Modified = True
        Me.txtBoothName.MyLinkLable1 = Nothing
        Me.txtBoothName.MyLinkLable2 = Nothing
        Me.txtBoothName.Name = "txtBoothName"
        Me.txtBoothName.ReadOnly = True
        Me.txtBoothName.ReferenceFieldDesc = Nothing
        Me.txtBoothName.ReferenceFieldName = Nothing
        Me.txtBoothName.ReferenceTableName = Nothing
        Me.txtBoothName.Size = New System.Drawing.Size(134, 18)
        Me.txtBoothName.TabIndex = 1538
        '
        'lblBoothName
        '
        Me.lblBoothName.FieldName = Nothing
        Me.lblBoothName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBoothName.Location = New System.Drawing.Point(3, 12)
        Me.lblBoothName.Name = "lblBoothName"
        Me.lblBoothName.Size = New System.Drawing.Size(69, 16)
        Me.lblBoothName.TabIndex = 1537
        Me.lblBoothName.Text = "Booth Name"
        '
        'rgbTaxType
        '
        Me.rgbTaxType.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbTaxType.Controls.Add(Me.rbtnIceCream)
        Me.rgbTaxType.Controls.Add(Me.rbtnProduct)
        Me.rgbTaxType.HeaderText = "Item Type"
        Me.rgbTaxType.Location = New System.Drawing.Point(8, 34)
        Me.rgbTaxType.Name = "rgbTaxType"
        Me.rgbTaxType.Size = New System.Drawing.Size(155, 59)
        Me.rgbTaxType.TabIndex = 1536
        Me.rgbTaxType.Text = "Item Type"
        '
        'rbtnIceCream
        '
        Me.rbtnIceCream.Location = New System.Drawing.Point(67, 22)
        Me.rbtnIceCream.Name = "rbtnIceCream"
        Me.rbtnIceCream.Size = New System.Drawing.Size(71, 18)
        Me.rbtnIceCream.TabIndex = 1
        Me.rbtnIceCream.Text = "Ice-Cream"
        '
        'rbtnProduct
        '
        Me.rbtnProduct.Location = New System.Drawing.Point(6, 22)
        Me.rbtnProduct.Name = "rbtnProduct"
        Me.rbtnProduct.Size = New System.Drawing.Size(59, 18)
        Me.rbtnProduct.TabIndex = 0
        Me.rbtnProduct.Text = "Product"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(947, 317)
        Me.gv1.TabIndex = 3
        Me.gv1.TabStop = False
        Me.gv1.VarID = ""
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(854, 10)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(70, 19)
        Me.btnclose.TabIndex = 89
        Me.btnclose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(8, 10)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(70, 19)
        Me.btnSave.TabIndex = 90
        Me.btnSave.Text = "Save"
        '
        'FrmProductQuickDemandBooking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(947, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmProductQuickDemandBooking"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Product Quick Demand"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.txtAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBoothName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBoothName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgbTaxType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbTaxType.ResumeLayout(False)
        Me.rgbTaxType.PerformLayout()
        CType(Me.rbtnIceCream, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents btnclose As RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents rgbTaxType As RadGroupBox
    Friend WithEvents rbtnIceCream As RadRadioButton
    Friend WithEvents rbtnProduct As RadRadioButton
    Friend WithEvents txtAddress As common.Controls.MyTextBox
    Friend WithEvents lblAddress As common.Controls.MyLabel
    Friend WithEvents txtDistributor As common.Controls.MyTextBox
    Friend WithEvents lblDistributor As common.Controls.MyLabel
    Friend WithEvents txtBoothName As common.Controls.MyTextBox
    Friend WithEvents lblBoothName As common.Controls.MyLabel
    Friend WithEvents btnSave As RadButton
End Class
