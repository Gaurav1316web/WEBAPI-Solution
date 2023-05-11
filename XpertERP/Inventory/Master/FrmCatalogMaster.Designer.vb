<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCatalogMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCatalogMaster))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.lblmainItemDesc = New common.Controls.MyLabel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Txtfeature = New common.Controls.MyTextBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.TxtSpecification = New common.Controls.MyTextBox
        Me.MyLabel10 = New common.Controls.MyLabel
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtcatlogdate = New common.Controls.MyDateTimePicker
        Me.txtDescription = New common.Controls.MyTextBox
        Me.mylabel18 = New common.Controls.MyLabel
        Me.lblCode = New common.Controls.MyLabel
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.txtCode = New common.UserControls.txtNavigator
        Me.label12 = New common.Controls.MyLabel
        Me.label15 = New common.Controls.MyLabel
        Me.txtBom = New common.UserControls.txtFinder
        Me.lblBomDesc = New common.Controls.MyLabel
        Me.lblmainItem = New common.Controls.MyLabel
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.pageComponent = New Telerik.WinControls.UI.RadPageViewPage
        Me.gvBOM = New common.UserControls.MyRadGridView
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.RDSaveLayout = New Telerik.WinControls.UI.RadMenuItem
        Me.RDDeleteLayout = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblmainItemDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txtfeature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSpecification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcatlogdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mylabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.label12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.label15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBomDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblmainItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.pageComponent.SuspendLayout()
        CType(Me.gvBOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvBOM.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(977, 472)
        Me.SplitContainer1.SplitterDistance = 440
        Me.SplitContainer1.TabIndex = 0
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblmainItemDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.PictureBox1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Txtfeature)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtSpecification)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel10)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtcatlogdate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.mylabel18)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.label12)
        Me.SplitContainer2.Panel1.Controls.Add(Me.label15)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtBom)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBomDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblmainItem)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Size = New System.Drawing.Size(977, 440)
        Me.SplitContainer2.SplitterDistance = 176
        Me.SplitContainer2.TabIndex = 0
        '
        'lblmainItemDesc
        '
        Me.lblmainItemDesc.AutoSize = False
        Me.lblmainItemDesc.BorderVisible = True
        Me.lblmainItemDesc.Location = New System.Drawing.Point(336, 78)
        Me.lblmainItemDesc.Name = "lblmainItemDesc"
        Me.lblmainItemDesc.Size = New System.Drawing.Size(397, 19)
        Me.lblmainItemDesc.TabIndex = 334
        Me.lblmainItemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Location = New System.Drawing.Point(752, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(203, 166)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 333
        Me.PictureBox1.TabStop = False
        '
        'Txtfeature
        '
        Me.Txtfeature.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtfeature.Location = New System.Drawing.Point(107, 120)
        Me.Txtfeature.MaxLength = 200
        Me.Txtfeature.MendatroryField = False
        Me.Txtfeature.MyLinkLable1 = Nothing
        Me.Txtfeature.MyLinkLable2 = Nothing
        Me.Txtfeature.Name = "Txtfeature"
        Me.Txtfeature.Size = New System.Drawing.Size(414, 18)
        Me.Txtfeature.TabIndex = 6
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(10, 121)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel2.TabIndex = 332
        Me.MyLabel2.Text = "Feature"
        '
        'TxtSpecification
        '
        Me.TxtSpecification.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSpecification.Location = New System.Drawing.Point(107, 100)
        Me.TxtSpecification.MaxLength = 200
        Me.TxtSpecification.MendatroryField = False
        Me.TxtSpecification.MyLinkLable1 = Nothing
        Me.TxtSpecification.MyLinkLable2 = Nothing
        Me.TxtSpecification.Name = "TxtSpecification"
        Me.TxtSpecification.Size = New System.Drawing.Size(415, 18)
        Me.TxtSpecification.TabIndex = 5
        '
        'MyLabel10
        '
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel10.Location = New System.Drawing.Point(10, 101)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel10.TabIndex = 330
        Me.MyLabel10.Text = "Specification"
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(451, 12)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel1.TabIndex = 22
        Me.MyLabel1.Text = "Date"
        '
        'txtcatlogdate
        '
        Me.txtcatlogdate.CustomFormat = "dd/MM/yyyy"
        Me.txtcatlogdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcatlogdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtcatlogdate.Location = New System.Drawing.Point(485, 11)
        Me.txtcatlogdate.MendatroryField = True
        Me.txtcatlogdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtcatlogdate.MyLinkLable1 = Me.MyLabel1
        Me.txtcatlogdate.MyLinkLable2 = Nothing
        Me.txtcatlogdate.Name = "txtcatlogdate"
        Me.txtcatlogdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtcatlogdate.Size = New System.Drawing.Size(125, 18)
        Me.txtcatlogdate.TabIndex = 2
        Me.txtcatlogdate.TabStop = False
        Me.txtcatlogdate.Text = "13/06/2011"
        Me.txtcatlogdate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(107, 32)
        Me.txtDescription.MaxLength = 500
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.mylabel18
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(625, 20)
        Me.txtDescription.TabIndex = 3
        '
        'mylabel18
        '
        Me.mylabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mylabel18.Location = New System.Drawing.Point(10, 34)
        Me.mylabel18.Name = "mylabel18"
        Me.mylabel18.Size = New System.Drawing.Size(63, 16)
        Me.mylabel18.TabIndex = 17
        Me.mylabel18.Text = "Description"
        '
        'lblCode
        '
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCode.Location = New System.Drawing.Point(10, 12)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(79, 16)
        Me.lblCode.TabIndex = 18
        Me.lblCode.Text = "Catalog Code"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(431, 9)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(107, 8)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(322, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'label12
        '
        Me.label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label12.Location = New System.Drawing.Point(10, 79)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(59, 16)
        Me.label12.TabIndex = 15
        Me.label12.Text = "Main Item "
        '
        'label15
        '
        Me.label15.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.label15.Location = New System.Drawing.Point(10, 55)
        Me.label15.Name = "label15"
        Me.label15.Size = New System.Drawing.Size(31, 18)
        Me.label15.TabIndex = 16
        Me.label15.Text = "BOM"
        '
        'txtBom
        '
        Me.txtBom.Location = New System.Drawing.Point(107, 55)
        Me.txtBom.MendatroryField = True
        Me.txtBom.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBom.MyLinkLable1 = Me.label15
        Me.txtBom.MyLinkLable2 = Nothing
        Me.txtBom.MyReadOnly = False
        Me.txtBom.Name = "txtBom"
        Me.txtBom.Size = New System.Drawing.Size(219, 19)
        Me.txtBom.TabIndex = 4
        Me.txtBom.Value = ""
        '
        'lblBomDesc
        '
        Me.lblBomDesc.AutoSize = False
        Me.lblBomDesc.BorderVisible = True
        Me.lblBomDesc.Location = New System.Drawing.Point(336, 55)
        Me.lblBomDesc.Name = "lblBomDesc"
        Me.lblBomDesc.Size = New System.Drawing.Size(397, 19)
        Me.lblBomDesc.TabIndex = 20
        Me.lblBomDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblmainItem
        '
        Me.lblmainItem.AutoSize = False
        Me.lblmainItem.BorderVisible = True
        Me.lblmainItem.Location = New System.Drawing.Point(107, 78)
        Me.lblmainItem.Name = "lblmainItem"
        Me.lblmainItem.Size = New System.Drawing.Size(218, 19)
        Me.lblmainItem.TabIndex = 19
        Me.lblmainItem.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.pageComponent)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.pageComponent
        Me.RadPageView1.Size = New System.Drawing.Size(977, 260)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'pageComponent
        '
        Me.pageComponent.Controls.Add(Me.gvBOM)
        Me.pageComponent.ItemSize = New System.Drawing.SizeF(80.0!, 28.0!)
        Me.pageComponent.Location = New System.Drawing.Point(10, 37)
        Me.pageComponent.Name = "pageComponent"
        Me.pageComponent.Size = New System.Drawing.Size(956, 212)
        Me.pageComponent.Text = "Components"
        '
        'gvBOM
        '
        Me.gvBOM.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvBOM.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvBOM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvBOM.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvBOM.ForeColor = System.Drawing.Color.Black
        Me.gvBOM.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvBOM.Location = New System.Drawing.Point(0, 0)
        '
        'gvBOM
        '
        Me.gvBOM.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvBOM.MasterTemplate.AllowAddNewRow = False
        Me.gvBOM.MasterTemplate.AutoGenerateColumns = False
        Me.gvBOM.MasterTemplate.EnableGrouping = False
        Me.gvBOM.Name = "gvBOM"
        Me.gvBOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvBOM.Size = New System.Drawing.Size(956, 212)
        Me.gvBOM.TabIndex = 0
        Me.gvBOM.Text = "RadGridView1"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(175, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(73, 20)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = "Print"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(896, 4)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(96, 4)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(16, 4)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(977, 20)
        Me.RadMenu1.TabIndex = 11
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RDSaveLayout, Me.RDDeleteLayout})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RDSaveLayout
        '
        Me.RDSaveLayout.AccessibleDescription = "Save Layout"
        Me.RDSaveLayout.AccessibleName = "Save Layout"
        Me.RDSaveLayout.Name = "RDSaveLayout"
        Me.RDSaveLayout.Text = "Save Layout"
        Me.RDSaveLayout.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RDDeleteLayout
        '
        Me.RDDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.RDDeleteLayout.AccessibleName = "Delete Layout"
        Me.RDDeleteLayout.Name = "RDDeleteLayout"
        Me.RDDeleteLayout.Text = "Delete Layout"
        Me.RDDeleteLayout.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'FrmCatalogMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(977, 492)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmCatalogMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Catalog Master"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblmainItemDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txtfeature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSpecification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcatlogdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mylabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.label12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.label15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBomDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblmainItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.pageComponent.ResumeLayout(False)
        CType(Me.gvBOM.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvBOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents pageComponent As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvBOM As common.UserControls.MyRadGridView
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents mylabel18 As common.Controls.MyLabel
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents label12 As common.Controls.MyLabel
    Friend WithEvents label15 As common.Controls.MyLabel
    Friend WithEvents txtBom As common.UserControls.txtFinder
    Friend WithEvents lblBomDesc As common.Controls.MyLabel
    Friend WithEvents lblmainItem As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtcatlogdate As common.Controls.MyDateTimePicker
    Friend WithEvents Txtfeature As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents TxtSpecification As common.Controls.MyTextBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblmainItemDesc As common.Controls.MyLabel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RDSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RDDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
End Class

