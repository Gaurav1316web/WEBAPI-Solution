<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmItemExciseMapping
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
        Me.components = New System.ComponentModel.Container
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.lblHcessTaxCode = New common.Controls.MyLabel
        Me.fndHcessTaxCode = New common.UserControls.txtFinder
        Me.lblEcessTaxCode = New common.Controls.MyLabel
        Me.fndEcessTaxCode = New common.UserControls.txtFinder
        Me.lblExciseTaxCode = New common.Controls.MyLabel
        Me.fndExciseTaxCode = New common.UserControls.txtFinder
        Me.txthcess = New common.MyNumBox
        Me.lblHcess = New common.Controls.MyLabel
        Me.txtuom = New common.MyNumBox
        Me.lblUOM = New common.Controls.MyLabel
        Me.txtcess = New common.MyNumBox
        Me.lblEcess = New common.Controls.MyLabel
        Me.txtexcise = New common.MyNumBox
        Me.lblExcise = New common.Controls.MyLabel
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.lblcust = New common.Controls.MyLabel
        Me.fnditem = New common.UserControls.txtFinder
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.Export = New Telerik.WinControls.UI.RadMenuItem
        Me.Import = New Telerik.WinControls.UI.RadMenuItem
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblHcessTaxCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEcessTaxCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExciseTaxCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txthcess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblHcess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtuom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEcess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtexcise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExcise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcust, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblHcessTaxCode)
        Me.RadGroupBox1.Controls.Add(Me.fndHcessTaxCode)
        Me.RadGroupBox1.Controls.Add(Me.lblEcessTaxCode)
        Me.RadGroupBox1.Controls.Add(Me.fndEcessTaxCode)
        Me.RadGroupBox1.Controls.Add(Me.lblExciseTaxCode)
        Me.RadGroupBox1.Controls.Add(Me.fndExciseTaxCode)
        Me.RadGroupBox1.Controls.Add(Me.txthcess)
        Me.RadGroupBox1.Controls.Add(Me.txtuom)
        Me.RadGroupBox1.Controls.Add(Me.txtcess)
        Me.RadGroupBox1.Controls.Add(Me.txtexcise)
        Me.RadGroupBox1.Controls.Add(Me.btnnew)
        Me.RadGroupBox1.Controls.Add(Me.lblEcess)
        Me.RadGroupBox1.Controls.Add(Me.lblExcise)
        Me.RadGroupBox1.Controls.Add(Me.lblHcess)
        Me.RadGroupBox1.Controls.Add(Me.lblUOM)
        Me.RadGroupBox1.Controls.Add(Me.lblcust)
        Me.RadGroupBox1.Controls.Add(Me.fnditem)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 5)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(525, 142)
        Me.RadGroupBox1.TabIndex = 0
        '
        'lblHcessTaxCode
        '
        Me.lblHcessTaxCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHcessTaxCode.Location = New System.Drawing.Point(213, 109)
        Me.lblHcessTaxCode.Name = "lblHcessTaxCode"
        Me.lblHcessTaxCode.Size = New System.Drawing.Size(55, 16)
        Me.lblHcessTaxCode.TabIndex = 45
        Me.lblHcessTaxCode.Text = "Tax Code"
        '
        'fndHcessTaxCode
        '
        Me.fndHcessTaxCode.Location = New System.Drawing.Point(274, 109)
        Me.fndHcessTaxCode.MendatroryField = True
        Me.fndHcessTaxCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndHcessTaxCode.MyLinkLable1 = Me.lblHcessTaxCode
        Me.fndHcessTaxCode.MyLinkLable2 = Nothing
        Me.fndHcessTaxCode.MyReadOnly = False
        Me.fndHcessTaxCode.Name = "fndHcessTaxCode"
        Me.fndHcessTaxCode.Size = New System.Drawing.Size(158, 20)
        Me.fndHcessTaxCode.TabIndex = 8
        Me.fndHcessTaxCode.Value = ""
        '
        'lblEcessTaxCode
        '
        Me.lblEcessTaxCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEcessTaxCode.Location = New System.Drawing.Point(213, 85)
        Me.lblEcessTaxCode.Name = "lblEcessTaxCode"
        Me.lblEcessTaxCode.Size = New System.Drawing.Size(55, 16)
        Me.lblEcessTaxCode.TabIndex = 43
        Me.lblEcessTaxCode.Text = "Tax Code"
        '
        'fndEcessTaxCode
        '
        Me.fndEcessTaxCode.Location = New System.Drawing.Point(274, 84)
        Me.fndEcessTaxCode.MendatroryField = True
        Me.fndEcessTaxCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndEcessTaxCode.MyLinkLable1 = Me.lblEcessTaxCode
        Me.fndEcessTaxCode.MyLinkLable2 = Nothing
        Me.fndEcessTaxCode.MyReadOnly = False
        Me.fndEcessTaxCode.Name = "fndEcessTaxCode"
        Me.fndEcessTaxCode.Size = New System.Drawing.Size(158, 20)
        Me.fndEcessTaxCode.TabIndex = 6
        Me.fndEcessTaxCode.Value = ""
        '
        'lblExciseTaxCode
        '
        Me.lblExciseTaxCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExciseTaxCode.Location = New System.Drawing.Point(213, 60)
        Me.lblExciseTaxCode.Name = "lblExciseTaxCode"
        Me.lblExciseTaxCode.Size = New System.Drawing.Size(55, 16)
        Me.lblExciseTaxCode.TabIndex = 41
        Me.lblExciseTaxCode.Text = "Tax Code"
        '
        'fndExciseTaxCode
        '
        Me.fndExciseTaxCode.Location = New System.Drawing.Point(274, 60)
        Me.fndExciseTaxCode.MendatroryField = True
        Me.fndExciseTaxCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndExciseTaxCode.MyLinkLable1 = Me.lblExciseTaxCode
        Me.fndExciseTaxCode.MyLinkLable2 = Nothing
        Me.fndExciseTaxCode.MyReadOnly = False
        Me.fndExciseTaxCode.Name = "fndExciseTaxCode"
        Me.fndExciseTaxCode.Size = New System.Drawing.Size(158, 20)
        Me.fndExciseTaxCode.TabIndex = 4
        Me.fndExciseTaxCode.Value = ""
        '
        'txthcess
        '
        Me.txthcess.BackColor = System.Drawing.Color.White
        Me.txthcess.DecimalPlaces = 0
        Me.txthcess.Location = New System.Drawing.Point(75, 109)
        Me.txthcess.MendatroryField = False
        Me.txthcess.MyLinkLable1 = Me.lblHcess
        Me.txthcess.MyLinkLable2 = Nothing
        Me.txthcess.Name = "txthcess"
        Me.txthcess.Size = New System.Drawing.Size(132, 20)
        Me.txthcess.TabIndex = 7
        Me.txthcess.Text = "0"
        Me.txthcess.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txthcess.Value = 0
        '
        'lblHcess
        '
        Me.lblHcess.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHcess.Location = New System.Drawing.Point(29, 109)
        Me.lblHcess.Name = "lblHcess"
        Me.lblHcess.Size = New System.Drawing.Size(38, 16)
        Me.lblHcess.TabIndex = 39
        Me.lblHcess.Text = "Hcess"
        '
        'txtuom
        '
        Me.txtuom.BackColor = System.Drawing.Color.White
        Me.txtuom.DecimalPlaces = 0
        Me.txtuom.Location = New System.Drawing.Point(75, 36)
        Me.txtuom.MendatroryField = False
        Me.txtuom.MyLinkLable1 = Me.lblUOM
        Me.txtuom.MyLinkLable2 = Nothing
        Me.txtuom.Name = "txtuom"
        Me.txtuom.Size = New System.Drawing.Size(132, 20)
        Me.txtuom.TabIndex = 2
        Me.txtuom.Text = "0"
        Me.txtuom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtuom.Value = 0
        '
        'lblUOM
        '
        Me.lblUOM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUOM.Location = New System.Drawing.Point(36, 37)
        Me.lblUOM.Name = "lblUOM"
        Me.lblUOM.Size = New System.Drawing.Size(33, 16)
        Me.lblUOM.TabIndex = 39
        Me.lblUOM.Text = "UOM"
        '
        'txtcess
        '
        Me.txtcess.BackColor = System.Drawing.Color.White
        Me.txtcess.DecimalPlaces = 0
        Me.txtcess.Location = New System.Drawing.Point(75, 84)
        Me.txtcess.MendatroryField = False
        Me.txtcess.MyLinkLable1 = Me.lblEcess
        Me.txtcess.MyLinkLable2 = Nothing
        Me.txtcess.Name = "txtcess"
        Me.txtcess.Size = New System.Drawing.Size(132, 20)
        Me.txtcess.TabIndex = 5
        Me.txtcess.Text = "0"
        Me.txtcess.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtcess.Value = 0
        '
        'lblEcess
        '
        Me.lblEcess.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEcess.Location = New System.Drawing.Point(30, 86)
        Me.lblEcess.Name = "lblEcess"
        Me.lblEcess.Size = New System.Drawing.Size(37, 16)
        Me.lblEcess.TabIndex = 39
        Me.lblEcess.Text = "Ecess"
        '
        'txtexcise
        '
        Me.txtexcise.BackColor = System.Drawing.Color.White
        Me.txtexcise.DecimalPlaces = 0
        Me.txtexcise.Location = New System.Drawing.Point(75, 60)
        Me.txtexcise.MendatroryField = False
        Me.txtexcise.MyLinkLable1 = Me.lblExcise
        Me.txtexcise.MyLinkLable2 = Nothing
        Me.txtexcise.Name = "txtexcise"
        Me.txtexcise.Size = New System.Drawing.Size(132, 20)
        Me.txtexcise.TabIndex = 3
        Me.txtexcise.Text = "0"
        Me.txtexcise.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtexcise.Value = 0
        '
        'lblExcise
        '
        Me.lblExcise.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExcise.Location = New System.Drawing.Point(29, 60)
        Me.lblExcise.Name = "lblExcise"
        Me.lblExcise.Size = New System.Drawing.Size(40, 16)
        Me.lblExcise.TabIndex = 39
        Me.lblExcise.Text = "Excise"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(226, 13)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 21)
        Me.btnnew.TabIndex = 1
        '
        'lblcust
        '
        Me.lblcust.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcust.Location = New System.Drawing.Point(5, 12)
        Me.lblcust.Name = "lblcust"
        Me.lblcust.Size = New System.Drawing.Size(62, 16)
        Me.lblcust.TabIndex = 39
        Me.lblcust.Text = "Item  Code"
        '
        'fnditem
        '
        Me.fnditem.Location = New System.Drawing.Point(75, 14)
        Me.fnditem.MendatroryField = False
        Me.fnditem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fnditem.MyLinkLable1 = Me.lblcust
        Me.fnditem.MyLinkLable2 = Nothing
        Me.fnditem.MyReadOnly = False
        Me.fnditem.Name = "fnditem"
        Me.fnditem.Size = New System.Drawing.Size(149, 19)
        Me.fnditem.TabIndex = 0
        Me.fnditem.Value = ""
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(468, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(70, 3)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(3, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(543, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Export, Me.Import})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'Export
        '
        Me.Export.AccessibleDescription = "RadMenuItem2"
        Me.Export.AccessibleName = "RadMenuItem2"
        Me.Export.Name = "Export"
        Me.Export.Text = "Export"
        Me.Export.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'Import
        '
        Me.Import.AccessibleDescription = "RadMenuItem3"
        Me.Import.AccessibleName = "RadMenuItem3"
        Me.Import.Name = "Import"
        Me.Import.Text = "Import"
        Me.Import.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(543, 183)
        Me.SplitContainer1.SplitterDistance = 154
        Me.SplitContainer1.TabIndex = 2
        '
        'FrmItemExciseMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(543, 203)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmItemExciseMapping"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Item Excise Mapping"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblHcessTaxCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEcessTaxCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExciseTaxCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txthcess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblHcess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtuom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEcess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtexcise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExcise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcust, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents fnditem As common.UserControls.txtFinder
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblUOM As common.Controls.MyLabel
    Friend WithEvents lblHcess As common.Controls.MyLabel
    Friend WithEvents lblExcise As common.Controls.MyLabel
    Friend WithEvents lblEcess As common.Controls.MyLabel
    Friend WithEvents lblcust As common.Controls.MyLabel
    Friend WithEvents txtexcise As common.MyNumBox
    Friend WithEvents txthcess As common.MyNumBox
    Friend WithEvents txtuom As common.MyNumBox
    Friend WithEvents txtcess As common.MyNumBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndExciseTaxCode As common.UserControls.txtFinder
    Friend WithEvents lblExciseTaxCode As common.Controls.MyLabel
    Friend WithEvents lblEcessTaxCode As common.Controls.MyLabel
    Friend WithEvents fndEcessTaxCode As common.UserControls.txtFinder
    Friend WithEvents lblHcessTaxCode As common.Controls.MyLabel
    Friend WithEvents fndHcessTaxCode As common.UserControls.txtFinder
End Class

