<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmItemPrice
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
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.dtpitem = New Telerik.WinControls.UI.RadDateTimePicker
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.cg = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgPrice = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkpriceselect = New common.Controls.MyRadioButton
        Me.chkPriceall = New common.Controls.MyRadioButton
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgtype = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chktypeSelect = New common.Controls.MyRadioButton
        Me.chktypeAll = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cgvitems = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkselect = New common.Controls.MyRadioButton
        Me.chkitemall = New common.Controls.MyRadioButton
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.dtptodate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpitem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.cg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cg.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkpriceselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPriceall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chktypeSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chktypeAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkitemall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadLabel2
        '
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(13, 9)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(59, 16)
        Me.RadLabel2.TabIndex = 15
        Me.RadLabel2.Text = "Price Date"
        '
        'dtpitem
        '
        Me.dtpitem.CustomFormat = "dd/MM/yyyy"
        Me.dtpitem.Location = New System.Drawing.Point(85, 8)
        Me.dtpitem.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpitem.Name = "dtpitem"
        Me.dtpitem.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpitem.Size = New System.Drawing.Size(152, 20)
        Me.dtpitem.TabIndex = 16
        Me.dtpitem.TabStop = False
        Me.dtpitem.Text = "Monday, August 01, 2011"
        Me.dtpitem.Value = New Date(2011, 8, 1, 14, 49, 31, 531)
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(430, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 24
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(86, 9)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 23
        Me.btnPrint.Text = "Print"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(12, 9)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 25
        Me.btnreset.Text = "Reset"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cg)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox6)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.dtptodate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.dtpitem)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 13)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(486, 543)
        Me.RadGroupBox1.TabIndex = 26
        '
        'cg
        '
        Me.cg.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.cg.Controls.Add(Me.cbgPrice)
        Me.cg.Controls.Add(Me.Panel2)
        Me.cg.HeaderText = "Price Code"
        Me.cg.Location = New System.Drawing.Point(18, 59)
        Me.cg.Name = "cg"
        Me.cg.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.cg.Size = New System.Drawing.Size(453, 155)
        Me.cg.TabIndex = 47
        Me.cg.Text = "Price Code"
        '
        'cbgPrice
        '
        Me.cbgPrice.CheckedValue = Nothing
        Me.cbgPrice.DataSource = Nothing
        Me.cbgPrice.DisplayMember = "Name"
        Me.cbgPrice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgPrice.Location = New System.Drawing.Point(10, 40)
        Me.cbgPrice.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgPrice.MyShowHeadrText = False
        Me.cbgPrice.Name = "cbgPrice"
        Me.cbgPrice.Size = New System.Drawing.Size(433, 105)
        Me.cbgPrice.TabIndex = 2
        Me.cbgPrice.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkpriceselect)
        Me.Panel2.Controls.Add(Me.chkPriceall)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(433, 20)
        Me.Panel2.TabIndex = 1
        '
        'chkpriceselect
        '
        Me.chkpriceselect.Location = New System.Drawing.Point(211, 1)
        Me.chkpriceselect.MyLinkLable1 = Nothing
        Me.chkpriceselect.MyLinkLable2 = Nothing
        Me.chkpriceselect.Name = "chkpriceselect"
        Me.chkpriceselect.Size = New System.Drawing.Size(50, 18)
        Me.chkpriceselect.TabIndex = 2
        Me.chkpriceselect.Text = "Select"
        '
        'chkPriceall
        '
        Me.chkPriceall.Location = New System.Drawing.Point(155, 1)
        Me.chkPriceall.MyLinkLable1 = Nothing
        Me.chkPriceall.MyLinkLable2 = Nothing
        Me.chkPriceall.Name = "chkPriceall"
        Me.chkPriceall.Size = New System.Drawing.Size(33, 18)
        Me.chkPriceall.TabIndex = 1
        Me.chkPriceall.Text = "All"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.cbgtype)
        Me.RadGroupBox6.Controls.Add(Me.Panel4)
        Me.RadGroupBox6.HeaderText = "UOM"
        Me.RadGroupBox6.Location = New System.Drawing.Point(18, 381)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(453, 150)
        Me.RadGroupBox6.TabIndex = 46
        Me.RadGroupBox6.Text = "UOM"
        '
        'cbgtype
        '
        Me.cbgtype.CheckedValue = Nothing
        Me.cbgtype.DataSource = Nothing
        Me.cbgtype.DisplayMember = "Name"
        Me.cbgtype.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgtype.Location = New System.Drawing.Point(10, 40)
        Me.cbgtype.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgtype.MyShowHeadrText = False
        Me.cbgtype.Name = "cbgtype"
        Me.cbgtype.Size = New System.Drawing.Size(433, 100)
        Me.cbgtype.TabIndex = 1
        Me.cbgtype.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chktypeSelect)
        Me.Panel4.Controls.Add(Me.chktypeAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(433, 20)
        Me.Panel4.TabIndex = 0
        '
        'chktypeSelect
        '
        Me.chktypeSelect.Location = New System.Drawing.Point(193, 1)
        Me.chktypeSelect.MyLinkLable1 = Nothing
        Me.chktypeSelect.MyLinkLable2 = Nothing
        Me.chktypeSelect.Name = "chktypeSelect"
        Me.chktypeSelect.Size = New System.Drawing.Size(50, 18)
        Me.chktypeSelect.TabIndex = 1
        Me.chktypeSelect.Text = "Select"
        '
        'chktypeAll
        '
        Me.chktypeAll.Location = New System.Drawing.Point(142, 1)
        Me.chktypeAll.MyLinkLable1 = Nothing
        Me.chktypeAll.MyLinkLable2 = Nothing
        Me.chktypeAll.Name = "chktypeAll"
        Me.chktypeAll.Size = New System.Drawing.Size(33, 18)
        Me.chktypeAll.TabIndex = 0
        Me.chktypeAll.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cgvitems)
        Me.RadGroupBox2.Controls.Add(Me.Panel1)
        Me.RadGroupBox2.HeaderText = "Item"
        Me.RadGroupBox2.Location = New System.Drawing.Point(18, 219)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(453, 156)
        Me.RadGroupBox2.TabIndex = 45
        Me.RadGroupBox2.Text = "Item"
        '
        'cgvitems
        '
        Me.cgvitems.CheckedValue = Nothing
        Me.cgvitems.DataSource = Nothing
        Me.cgvitems.DisplayMember = "Name"
        Me.cgvitems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cgvitems.Location = New System.Drawing.Point(10, 40)
        Me.cgvitems.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cgvitems.MyShowHeadrText = False
        Me.cgvitems.Name = "cgvitems"
        Me.cgvitems.Size = New System.Drawing.Size(433, 106)
        Me.cgvitems.TabIndex = 2
        Me.cgvitems.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkselect)
        Me.Panel1.Controls.Add(Me.chkitemall)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(433, 20)
        Me.Panel1.TabIndex = 1
        '
        'chkselect
        '
        Me.chkselect.Location = New System.Drawing.Point(199, 1)
        Me.chkselect.MyLinkLable1 = Nothing
        Me.chkselect.MyLinkLable2 = Nothing
        Me.chkselect.Name = "chkselect"
        Me.chkselect.Size = New System.Drawing.Size(50, 18)
        Me.chkselect.TabIndex = 2
        Me.chkselect.Text = "Select"
        '
        'chkitemall
        '
        Me.chkitemall.Location = New System.Drawing.Point(147, 1)
        Me.chkitemall.MyLinkLable1 = Nothing
        Me.chkitemall.MyLinkLable2 = Nothing
        Me.chkitemall.Name = "chkitemall"
        Me.chkitemall.Size = New System.Drawing.Size(33, 18)
        Me.chkitemall.TabIndex = 1
        Me.chkitemall.Text = "All"
        '
        'RadLabel3
        '
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(273, 9)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(46, 16)
        Me.RadLabel3.TabIndex = 26
        Me.RadLabel3.Text = "To Date"
        '
        'dtptodate
        '
        Me.dtptodate.CustomFormat = "dd/MM/yyyy"
        Me.dtptodate.Location = New System.Drawing.Point(346, 8)
        Me.dtptodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.Name = "dtptodate"
        Me.dtptodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.Size = New System.Drawing.Size(120, 20)
        Me.dtptodate.TabIndex = 17
        Me.dtptodate.TabStop = False
        Me.dtptodate.Text = "Monday, August 01, 2011"
        Me.dtptodate.Value = New Date(2011, 8, 1, 14, 49, 31, 531)
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Size = New System.Drawing.Size(513, 599)
        Me.SplitContainer1.SplitterDistance = 565
        Me.SplitContainer1.TabIndex = 27
        '
        'frmItemPrice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(513, 599)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmItemPrice"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Item Price"
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpitem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.cg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cg.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkpriceselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPriceall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chktypeSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chktypeAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkitemall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtpitem As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dtptodate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cgvitems As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkselect As common.Controls.MyRadioButton
    Friend WithEvents chkitemall As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgtype As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chktypeSelect As common.Controls.MyRadioButton
    Friend WithEvents chktypeAll As common.Controls.MyRadioButton
    Friend WithEvents cg As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgPrice As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkpriceselect As common.Controls.MyRadioButton
    Friend WithEvents chkPriceall As common.Controls.MyRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

