Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form26Q27Q
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.rdoform27 = New common.Controls.MyRadioButton
        Me.rdoform26 = New common.Controls.MyRadioButton
        Me.fdnbranch = New common.UserControls.txtFinder
        Me.fndfiscal = New common.UserControls.txtFinder
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabe3 = New common.Controls.MyLabel
        Me.rdoquarter = New common.Controls.MyRadioButton
        Me.rdoannual = New common.Controls.MyRadioButton
        Me.rdofront = New common.Controls.MyRadioButton
        Me.rdodetail = New common.Controls.MyRadioButton
        Me.rdoannexure = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cmbtype = New common.Controls.MyComboBox
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rdoform27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdoform26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabe3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdoquarter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdoannual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdofront, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdodetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdoannexure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.cmbtype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rdoform27)
        Me.RadGroupBox1.Controls.Add(Me.rdoform26)
        Me.RadGroupBox1.HeaderText = "Select Form Number"
        Me.RadGroupBox1.Location = New System.Drawing.Point(11, 11)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(243, 51)
        Me.RadGroupBox1.TabIndex = 1
        Me.RadGroupBox1.Text = "Select Form Number"
        '
        'rdoform27
        '
        Me.rdoform27.Location = New System.Drawing.Point(142, 23)
        Me.rdoform27.MyLinkLable1 = Nothing
        Me.rdoform27.MyLinkLable2 = Nothing
        Me.rdoform27.Name = "rdoform27"
        Me.rdoform27.Size = New System.Drawing.Size(61, 18)
        Me.rdoform27.TabIndex = 2
        Me.rdoform27.Text = "Form 27"
        '
        'rdoform26
        '
        Me.rdoform26.Location = New System.Drawing.Point(26, 23)
        Me.rdoform26.MyLinkLable1 = Nothing
        Me.rdoform26.MyLinkLable2 = Nothing
        Me.rdoform26.Name = "rdoform26"
        Me.rdoform26.Size = New System.Drawing.Size(61, 18)
        Me.rdoform26.TabIndex = 1
        Me.rdoform26.Text = "Form 26"
        '
        'fdnbranch
        '
        Me.fdnbranch.Location = New System.Drawing.Point(89, 73)
        Me.fdnbranch.MendatroryField = False
        Me.fdnbranch.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fdnbranch.MyLinkLable1 = Nothing
        Me.fdnbranch.MyLinkLable2 = Nothing
        Me.fdnbranch.MyReadOnly = False
        Me.fdnbranch.Name = "fdnbranch"
        Me.fdnbranch.Size = New System.Drawing.Size(134, 21)
        Me.fdnbranch.TabIndex = 2
        Me.fdnbranch.Value = ""
        '
        'fndfiscal
        '
        Me.fndfiscal.Location = New System.Drawing.Point(326, 73)
        Me.fndfiscal.MendatroryField = False
        Me.fndfiscal.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndfiscal.MyLinkLable1 = Nothing
        Me.fndfiscal.MyLinkLable2 = Nothing
        Me.fndfiscal.MyReadOnly = False
        Me.fndfiscal.Name = "fndfiscal"
        Me.fndfiscal.Size = New System.Drawing.Size(122, 21)
        Me.fndfiscal.TabIndex = 3
        Me.fndfiscal.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(13, 73)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(70, 18)
        Me.RadLabel1.TabIndex = 3
        Me.RadLabel1.Text = "Branch Code"
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(229, 73)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(91, 18)
        Me.RadLabel2.TabIndex = 4
        Me.RadLabel2.Text = "Fiscal Year Name"
        '
        'RadLabe3
        '
        Me.RadLabe3.Location = New System.Drawing.Point(13, 104)
        Me.RadLabe3.Name = "RadLabe3"
        Me.RadLabe3.Size = New System.Drawing.Size(108, 18)
        Me.RadLabe3.TabIndex = 5
        Me.RadLabe3.Text = "Return of Deduction"
        '
        'rdoquarter
        '
        Me.rdoquarter.Location = New System.Drawing.Point(142, 104)
        Me.rdoquarter.MyLinkLable1 = Nothing
        Me.rdoquarter.MyLinkLable2 = Nothing
        Me.rdoquarter.Name = "rdoquarter"
        Me.rdoquarter.Size = New System.Drawing.Size(59, 18)
        Me.rdoquarter.TabIndex = 4
        Me.rdoquarter.Text = "Quarter"
        '
        'rdoannual
        '
        Me.rdoannual.Location = New System.Drawing.Point(142, 128)
        Me.rdoannual.MyLinkLable1 = Nothing
        Me.rdoannual.MyLinkLable2 = Nothing
        Me.rdoannual.Name = "rdoannual"
        Me.rdoannual.Size = New System.Drawing.Size(55, 18)
        Me.rdoannual.TabIndex = 6
        Me.rdoannual.Text = "Annual"
        '
        'rdofront
        '
        Me.rdofront.Location = New System.Drawing.Point(10, 23)
        Me.rdofront.MyLinkLable1 = Nothing
        Me.rdofront.MyLinkLable2 = Nothing
        Me.rdofront.Name = "rdofront"
        Me.rdofront.Size = New System.Drawing.Size(74, 18)
        Me.rdofront.TabIndex = 1
        Me.rdofront.Text = "Front Page"
        '
        'rdodetail
        '
        Me.rdodetail.Location = New System.Drawing.Point(173, 25)
        Me.rdodetail.MyLinkLable1 = Nothing
        Me.rdodetail.MyLinkLable2 = Nothing
        Me.rdodetail.Name = "rdodetail"
        Me.rdodetail.Size = New System.Drawing.Size(77, 18)
        Me.rdodetail.TabIndex = 2
        Me.rdodetail.Text = "Detail Page"
        '
        'rdoannexure
        '
        Me.rdoannexure.Location = New System.Drawing.Point(319, 23)
        Me.rdoannexure.MyLinkLable1 = Nothing
        Me.rdoannexure.MyLinkLable2 = Nothing
        Me.rdoannexure.Name = "rdoannexure"
        Me.rdoannexure.Size = New System.Drawing.Size(96, 18)
        Me.rdoannexure.TabIndex = 3
        Me.rdoannexure.Text = "Annexure Page"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rdoannexure)
        Me.RadGroupBox2.Controls.Add(Me.rdofront)
        Me.RadGroupBox2.Controls.Add(Me.rdodetail)
        Me.RadGroupBox2.HeaderText = "Select the Print Page"
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 161)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(435, 51)
        Me.RadGroupBox2.TabIndex = 7
        Me.RadGroupBox2.Text = "Select the Print Page"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cmbtype)
        Me.RadGroupBox3.Controls.Add(Me.RadGroupBox1)
        Me.RadGroupBox3.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox3.Controls.Add(Me.fdnbranch)
        Me.RadGroupBox3.Controls.Add(Me.rdoannual)
        Me.RadGroupBox3.Controls.Add(Me.fndfiscal)
        Me.RadGroupBox3.Controls.Add(Me.rdoquarter)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.RadLabe3)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(12, 3)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(462, 243)
        Me.RadGroupBox3.TabIndex = 15
        '
        'cmbtype
        '
        Me.cmbtype.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "First "
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Second "
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "Third "
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "Fourth "
        RadListDataItem4.TextWrap = True
        Me.cmbtype.Items.Add(RadListDataItem1)
        Me.cmbtype.Items.Add(RadListDataItem2)
        Me.cmbtype.Items.Add(RadListDataItem3)
        Me.cmbtype.Items.Add(RadListDataItem4)
        Me.cmbtype.Location = New System.Drawing.Point(229, 104)
        Me.cmbtype.MendatroryField = False
        Me.cmbtype.MyLinkLable1 = Nothing
        Me.cmbtype.MyLinkLable2 = Nothing
        Me.cmbtype.Name = "cmbtype"
        Me.cmbtype.Size = New System.Drawing.Size(91, 20)
        Me.cmbtype.TabIndex = 5
        Me.cmbtype.Text = "First"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(12, 5)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 9
        Me.btnreset.Text = "&Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(410, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "&Close"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(86, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 8
        Me.btnPrint.Text = "&Print"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Size = New System.Drawing.Size(487, 299)
        Me.SplitContainer1.SplitterDistance = 264
        Me.SplitContainer1.TabIndex = 16
        '
        'form26Q27Q
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(487, 299)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "form26Q27Q"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "TDS Form 26Q/27Q Report"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rdoform27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdoform26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabe3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdoquarter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdoannual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdofront, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdodetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdoannexure, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.cmbtype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdoform26 As common.Controls.MyRadioButton
    Friend WithEvents rdoform27 As common.Controls.MyRadioButton
    Friend WithEvents fdnbranch As common.UserControls.txtFinder
    Friend WithEvents fndfiscal As common.UserControls.txtFinder
    Friend WithEvents rdoannual As common.Controls.MyRadioButton
    Friend WithEvents rdofront As common.Controls.MyRadioButton
    Friend WithEvents rdoquarter As common.Controls.MyRadioButton
    Friend WithEvents rdodetail As common.Controls.MyRadioButton
    Friend WithEvents rdoannexure As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents cmbtype As common.Controls.MyComboBox
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabe3 As common.Controls.MyLabel
End Class

