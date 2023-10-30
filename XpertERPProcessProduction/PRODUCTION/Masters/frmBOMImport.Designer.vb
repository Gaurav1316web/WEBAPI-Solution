<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBOMImport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBOMImport))
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnExport = New Telerik.WinControls.UI.RadButton
        Me.btnImport = New Telerik.WinControls.UI.RadButton
        Me.RadMenuItemExport = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.txtDocPath = New common.Controls.MyTextBox
        Me.lblDocument = New common.Controls.MyLabel
        Me.MyTextBox1 = New common.Controls.MyTextBox
        Me.cboBOMStatus = New common.Controls.MyComboBox
        Me.btnBrowse = New Telerik.WinControls.UI.RadButton
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.RadLabel4 = New common.Controls.MyLabel
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.txtDescription = New common.Controls.MyTextBox
        Me.cboStatus = New common.Controls.MyComboBox
        Me.txtDocPath1 = New common.Controls.MyTextBox
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnImport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtDocPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyTextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboBOMStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocPath1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(409, 22)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExport.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnExport.Location = New System.Drawing.Point(74, 22)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(68, 18)
        Me.btnExport.TabIndex = 1
        Me.btnExport.Text = "Export"
        '
        'btnImport
        '
        Me.btnImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnImport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImport.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnImport.Location = New System.Drawing.Point(3, 22)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(68, 18)
        Me.btnImport.TabIndex = 0
        Me.btnImport.Text = "Import"
        '
        'RadMenuItemExport
        '
        Me.RadMenuItemExport.AccessibleDescription = "File"
        Me.RadMenuItemExport.AccessibleName = "File"
        Me.RadMenuItemExport.Name = "RadMenuItemExport"
        Me.RadMenuItemExport.Text = "File"
        Me.RadMenuItemExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnImport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(478, 231)
        Me.SplitContainer1.SplitterDistance = 177
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtDocPath)
        Me.RadGroupBox1.Controls.Add(Me.MyTextBox1)
        Me.RadGroupBox1.Controls.Add(Me.cboBOMStatus)
        Me.RadGroupBox1.Controls.Add(Me.btnBrowse)
        Me.RadGroupBox1.Controls.Add(Me.lblDocument)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.Controls.Add(Me.RadButton1)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = " "
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(463, 162)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = " "
        '
        'txtDocPath
        '
        Me.txtDocPath.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocPath.Location = New System.Drawing.Point(86, 70)
        Me.txtDocPath.MaxLength = 49
        Me.txtDocPath.MendatroryField = False
        Me.txtDocPath.MyLinkLable1 = Me.lblDocument
        Me.txtDocPath.MyLinkLable2 = Nothing
        Me.txtDocPath.Name = "txtDocPath"
        Me.txtDocPath.Size = New System.Drawing.Size(219, 18)
        Me.txtDocPath.TabIndex = 3
        Me.txtDocPath.Visible = False
        '
        'lblDocument
        '
        Me.lblDocument.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDocument.Location = New System.Drawing.Point(13, 72)
        Me.lblDocument.Name = "lblDocument"
        Me.lblDocument.Size = New System.Drawing.Size(58, 16)
        Me.lblDocument.TabIndex = 5
        Me.lblDocument.Text = "Document"
        Me.lblDocument.Visible = False
        '
        'MyTextBox1
        '
        Me.MyTextBox1.Location = New System.Drawing.Point(86, 45)
        Me.MyTextBox1.MaxLength = 50
        Me.MyTextBox1.MendatroryField = True
        Me.MyTextBox1.MyLinkLable1 = Nothing
        Me.MyTextBox1.MyLinkLable2 = Nothing
        Me.MyTextBox1.Name = "MyTextBox1"
        Me.MyTextBox1.Size = New System.Drawing.Size(219, 20)
        Me.MyTextBox1.TabIndex = 2
        '
        'cboBOMStatus
        '
        Me.cboBOMStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboBOMStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "BOM Head"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "BOM Components"
        RadListDataItem2.TextWrap = True
        Me.cboBOMStatus.Items.Add(RadListDataItem1)
        Me.cboBOMStatus.Items.Add(RadListDataItem2)
        Me.cboBOMStatus.Location = New System.Drawing.Point(86, 24)
        Me.cboBOMStatus.MendatroryField = True
        Me.cboBOMStatus.MyLinkLable1 = Nothing
        Me.cboBOMStatus.MyLinkLable2 = Nothing
        Me.cboBOMStatus.Name = "cboBOMStatus"
        Me.cboBOMStatus.Size = New System.Drawing.Size(219, 18)
        Me.cboBOMStatus.TabIndex = 0
        '
        'btnBrowse
        '
        Me.btnBrowse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(311, 70)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(66, 18)
        Me.btnBrowse.TabIndex = 4
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.Visible = False
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(12, 45)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel3.TabIndex = 6
        Me.RadLabel3.Text = "Description"
        '
        'RadLabel4
        '
        Me.RadLabel4.Location = New System.Drawing.Point(12, 89)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(2, 2)
        Me.RadLabel4.TabIndex = 19
        '
        'RadButton1
        '
        Me.RadButton1.Image = CType(resources.GetObject("RadButton1.Image"), System.Drawing.Image)
        Me.RadButton1.Location = New System.Drawing.Point(352, -422)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(14, 20)
        Me.RadButton1.TabIndex = 17
        Me.RadButton1.Text = " "
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(311, 23)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(12, 23)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(68, 18)
        Me.RadLabel1.TabIndex = 7
        Me.RadLabel1.Text = "BOM Import"
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(88, 44)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.RadLabel3
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(360, 20)
        Me.txtDescription.TabIndex = 1
        '
        'cboStatus
        '
        Me.cboStatus.BackColor = System.Drawing.Color.Transparent
        Me.cboStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem3.Text = "BOM Head"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "BOM Components"
        RadListDataItem4.TextWrap = True
        Me.cboStatus.Items.Add(RadListDataItem3)
        Me.cboStatus.Items.Add(RadListDataItem4)
        Me.cboStatus.Location = New System.Drawing.Point(88, 22)
        Me.cboStatus.MendatroryField = False
        Me.cboStatus.MyLinkLable1 = Nothing
        Me.cboStatus.MyLinkLable2 = Nothing
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(203, 18)
        Me.cboStatus.TabIndex = 2
        '
        'txtDocPath1
        '
        Me.txtDocPath1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocPath1.Location = New System.Drawing.Point(88, 70)
        Me.txtDocPath1.MaxLength = 49
        Me.txtDocPath1.MendatroryField = False
        Me.txtDocPath1.MyLinkLable1 = Me.lblDocument
        Me.txtDocPath1.MyLinkLable2 = Nothing
        Me.txtDocPath1.Name = "txtDocPath1"
        Me.txtDocPath1.Size = New System.Drawing.Size(188, 18)
        Me.txtDocPath1.TabIndex = 218
        '
        'frmBOMImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(478, 231)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmBOMImport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "BOM Import"
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnImport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtDocPath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyTextBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboBOMStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocPath1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnImport As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItemExport As Telerik.WinControls.UI.RadMenuItem
    'Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    'Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    'Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    'Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadDropDownMenu
    'Friend WithEvents MenuItemImport As Telerik.WinControls.UI.RadMenuItem
    'Friend WithEvents MenuItemExport As Telerik.WinControls.UI.RadMenuItem
    'Friend WithEvents MenuItemClose As Telerik.WinControls.UI.RadMenuItem
    'Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnBrowse As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDocument As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents cboStatus As common.Controls.MyComboBox
    Friend WithEvents txtDocPath1 As common.Controls.MyTextBox
    Friend WithEvents cboBOMStatus As common.Controls.MyComboBox
    Friend WithEvents MyTextBox1 As common.Controls.MyTextBox
    Friend WithEvents txtDocPath As common.Controls.MyTextBox
End Class

