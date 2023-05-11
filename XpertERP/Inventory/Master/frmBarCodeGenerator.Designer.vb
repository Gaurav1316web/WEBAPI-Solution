<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBarCodeGenerator
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
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.chkIntegrate = New Telerik.WinControls.UI.RadCheckBox
        Me.btnRefersh = New Telerik.WinControls.UI.RadButton
        Me.ddlItem = New common.Controls.MyComboBox
        Me.lblItemType = New common.Controls.MyLabel
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.gvDetail = New common.UserControls.MyRadGridView
        Me.RadLabel12 = New common.Controls.MyLabel
        Me.gvPrint = New common.UserControls.MyRadGridView
        Me.btnPrintBarCode = New Telerik.WinControls.UI.RadButton
        Me.gbOptions = New Telerik.WinControls.UI.RadGroupBox
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.chkName = New Telerik.WinControls.UI.RadCheckBox
        Me.chkBarCode = New Telerik.WinControls.UI.RadCheckBox
        Me.chkMRP = New Telerik.WinControls.UI.RadCheckBox
        Me.btnShowBarCode = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.rmi = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.chkIntegrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefersh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPrint.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintBarCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbOptions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbOptions.SuspendLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBarCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMRP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShowBarCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnShowBarCode)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(879, 478)
        Me.SplitContainer1.SplitterDistance = 449
        Me.SplitContainer1.TabIndex = 2
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkIntegrate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnRefersh)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ddlItem)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblItemType)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Size = New System.Drawing.Size(879, 449)
        Me.SplitContainer2.SplitterDistance = 44
        Me.SplitContainer2.TabIndex = 0
        '
        'chkIntegrate
        '
        Me.chkIntegrate.Location = New System.Drawing.Point(326, 14)
        Me.chkIntegrate.Name = "chkIntegrate"
        Me.chkIntegrate.Size = New System.Drawing.Size(122, 18)
        Me.chkIntegrate.TabIndex = 2
        Me.chkIntegrate.Text = "Integrate With SAGE"
        Me.chkIntegrate.Visible = False
        '
        'btnRefersh
        '
        Me.btnRefersh.Location = New System.Drawing.Point(234, 13)
        Me.btnRefersh.Name = "btnRefersh"
        Me.btnRefersh.Size = New System.Drawing.Size(77, 19)
        Me.btnRefersh.TabIndex = 1
        Me.btnRefersh.Text = "Refresh"
        Me.btnRefersh.Visible = False
        '
        'ddlItem
        '
        Me.ddlItem.AllowShowFocusCues = False
        Me.ddlItem.AutoCompleteDisplayMember = Nothing
        Me.ddlItem.AutoCompleteValueMember = Nothing
        Me.ddlItem.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "Payment"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Advance"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "Misc Payment"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "Apply Document"
        RadListDataItem4.TextWrap = True
        RadListDataItem5.Text = "On Account"
        RadListDataItem5.TextWrap = True
        RadListDataItem6.Text = "Receipt"
        RadListDataItem6.TextWrap = True
        Me.ddlItem.Items.Add(RadListDataItem1)
        Me.ddlItem.Items.Add(RadListDataItem2)
        Me.ddlItem.Items.Add(RadListDataItem3)
        Me.ddlItem.Items.Add(RadListDataItem4)
        Me.ddlItem.Items.Add(RadListDataItem5)
        Me.ddlItem.Items.Add(RadListDataItem6)
        Me.ddlItem.Location = New System.Drawing.Point(69, 13)
        Me.ddlItem.MendatroryField = False
        Me.ddlItem.MyLinkLable1 = Me.lblItemType
        Me.ddlItem.MyLinkLable2 = Nothing
        Me.ddlItem.Name = "ddlItem"
        Me.ddlItem.Size = New System.Drawing.Size(159, 18)
        Me.ddlItem.TabIndex = 0
        '
        'lblItemType
        '
        Me.lblItemType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemType.Location = New System.Drawing.Point(6, 14)
        Me.lblItemType.Name = "lblItemType"
        Me.lblItemType.Size = New System.Drawing.Size(57, 16)
        Me.lblItemType.TabIndex = 3
        Me.lblItemType.Text = "Item Type"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.SplitContainer4)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.gvPrint)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnPrintBarCode)
        Me.SplitContainer3.Panel2.Controls.Add(Me.gbOptions)
        Me.SplitContainer3.Size = New System.Drawing.Size(879, 401)
        Me.SplitContainer3.SplitterDistance = 668
        Me.SplitContainer3.TabIndex = 2
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.gv1)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.gvDetail)
        Me.SplitContainer4.Panel2.Controls.Add(Me.RadLabel12)
        Me.SplitContainer4.Size = New System.Drawing.Size(668, 401)
        Me.SplitContainer4.SplitterDistance = 268
        Me.SplitContainer4.TabIndex = 0
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.Size = New System.Drawing.Size(668, 268)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'gvDetail
        '
        Me.gvDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDetail.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvDetail.ForeColor = System.Drawing.Color.Black
        Me.gvDetail.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvDetail.Location = New System.Drawing.Point(0, 0)
        '
        'gvDetail
        '
        Me.gvDetail.MasterTemplate.AllowDeleteRow = False
        Me.gvDetail.MasterTemplate.EnableFiltering = True
        Me.gvDetail.Name = "gvDetail"
        Me.gvDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvDetail.ShowGroupPanel = False
        Me.gvDetail.Size = New System.Drawing.Size(668, 113)
        Me.gvDetail.TabIndex = 0
        Me.gvDetail.TabStop = False
        Me.gvDetail.Text = "RadGridView1"
        '
        'RadLabel12
        '
        Me.RadLabel12.AutoSize = False
        Me.RadLabel12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel12.Location = New System.Drawing.Point(0, 113)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(668, 16)
        Me.RadLabel12.TabIndex = 1
        Me.RadLabel12.Text = "Double click to add in Printing"
        Me.RadLabel12.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        '
        'gvPrint
        '
        Me.gvPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvPrint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvPrint.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvPrint.ForeColor = System.Drawing.Color.Black
        Me.gvPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvPrint.Location = New System.Drawing.Point(0, 56)
        '
        'gvPrint
        '
        Me.gvPrint.MasterTemplate.AllowDeleteRow = False
        Me.gvPrint.MasterTemplate.EnableFiltering = True
        Me.gvPrint.Name = "gvPrint"
        Me.gvPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvPrint.ShowGroupPanel = False
        Me.gvPrint.Size = New System.Drawing.Size(207, 327)
        Me.gvPrint.TabIndex = 2
        Me.gvPrint.TabStop = False
        Me.gvPrint.Text = "RadGridView1"
        '
        'btnPrintBarCode
        '
        Me.btnPrintBarCode.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnPrintBarCode.Location = New System.Drawing.Point(0, 383)
        Me.btnPrintBarCode.Name = "btnPrintBarCode"
        Me.btnPrintBarCode.Size = New System.Drawing.Size(207, 18)
        Me.btnPrintBarCode.TabIndex = 1
        Me.btnPrintBarCode.Text = "Print Bar Code"
        '
        'gbOptions
        '
        Me.gbOptions.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbOptions.Controls.Add(Me.RadButton1)
        Me.gbOptions.Controls.Add(Me.chkName)
        Me.gbOptions.Controls.Add(Me.chkBarCode)
        Me.gbOptions.Controls.Add(Me.chkMRP)
        Me.gbOptions.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbOptions.HeaderText = "Print"
        Me.gbOptions.Location = New System.Drawing.Point(0, 0)
        Me.gbOptions.Name = "gbOptions"
        Me.gbOptions.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbOptions.Size = New System.Drawing.Size(207, 56)
        Me.gbOptions.TabIndex = 0
        Me.gbOptions.Text = "Print"
        '
        'RadButton1
        '
        Me.RadButton1.Location = New System.Drawing.Point(4, 35)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(199, 18)
        Me.RadButton1.TabIndex = 3
        Me.RadButton1.Text = "Clear all Bar Code"
        '
        'chkName
        '
        Me.chkName.Location = New System.Drawing.Point(76, 14)
        Me.chkName.Name = "chkName"
        Me.chkName.Size = New System.Drawing.Size(76, 18)
        Me.chkName.TabIndex = 1
        Me.chkName.Text = "Item Name"
        '
        'chkBarCode
        '
        Me.chkBarCode.Location = New System.Drawing.Point(4, 14)
        Me.chkBarCode.Name = "chkBarCode"
        Me.chkBarCode.Size = New System.Drawing.Size(66, 18)
        Me.chkBarCode.TabIndex = 0
        Me.chkBarCode.Text = "Bar Code"
        '
        'chkMRP
        '
        Me.chkMRP.Location = New System.Drawing.Point(153, 14)
        Me.chkMRP.Name = "chkMRP"
        Me.chkMRP.Size = New System.Drawing.Size(43, 18)
        Me.chkMRP.TabIndex = 2
        Me.chkMRP.Text = "MRP"
        '
        'btnShowBarCode
        '
        Me.btnShowBarCode.Location = New System.Drawing.Point(135, 3)
        Me.btnShowBarCode.Name = "btnShowBarCode"
        Me.btnShowBarCode.Size = New System.Drawing.Size(123, 18)
        Me.btnShowBarCode.TabIndex = 1
        Me.btnShowBarCode.Text = "Show Bar Code"
        Me.btnShowBarCode.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(808, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(6, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(123, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save  Bar  Code"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmi})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(879, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'rmi
        '
        Me.rmi.AccessibleDescription = "File"
        Me.rmi.AccessibleName = "File"
        Me.rmi.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.rmi.Name = "rmi"
        Me.rmi.Text = "File"
        Me.rmi.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Export"
        Me.RadMenuItem1.AccessibleName = "Export"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Export"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Import"
        Me.RadMenuItem2.AccessibleName = "Import"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Import"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'FrmBarCodeGenerator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(879, 498)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmBarCodeGenerator"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bar Code Generator"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.chkIntegrate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefersh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPrint.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintBarCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbOptions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbOptions.ResumeLayout(False)
        Me.gbOptions.PerformLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBarCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMRP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShowBarCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents ddlItem As common.Controls.MyComboBox
    Friend WithEvents lblItemType As common.Controls.MyLabel
    Friend WithEvents gbOptions As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkName As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkMRP As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents chkBarCode As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnPrintBarCode As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnShowBarCode As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefersh As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkIntegrate As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents gvPrint As common.UserControls.MyRadGridView
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents gvDetail As common.UserControls.MyRadGridView
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rmi As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
End Class

