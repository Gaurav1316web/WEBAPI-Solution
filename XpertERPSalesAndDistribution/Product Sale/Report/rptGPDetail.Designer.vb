<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptGPDetail
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtmultCity = New common.UserControls.txtMultiSelectFinder()
        Me.lblCity = New common.Controls.MyLabel()
        Me.txtmultLocation = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtmultCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.lblCustomer = New common.Controls.MyLabel()
        Me.txtVehicle = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtTransporter = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtfromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rm1SaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rm1DeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(553, 380)
        Me.SplitContainer1.SplitterDistance = 339
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(553, 339)
        Me.RadPageView1.TabIndex = 4
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtmultCity)
        Me.RadPageViewPage1.Controls.Add(Me.lblCity)
        Me.RadPageViewPage1.Controls.Add(Me.txtmultLocation)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtmultCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.lblCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.txtVehicle)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtTransporter)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(532, 291)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'txtmultCity
        '
        Me.txtmultCity.arrDispalyMember = Nothing
        Me.txtmultCity.arrValueMember = Nothing
        Me.txtmultCity.Location = New System.Drawing.Point(79, 122)
        Me.txtmultCity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmultCity.MyLinkLable1 = Me.lblCity
        Me.txtmultCity.MyLinkLable2 = Nothing
        Me.txtmultCity.MyNullText = "All"
        Me.txtmultCity.Name = "txtmultCity"
        Me.txtmultCity.Size = New System.Drawing.Size(281, 19)
        Me.txtmultCity.TabIndex = 369
        '
        'lblCity
        '
        Me.lblCity.FieldName = Nothing
        Me.lblCity.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCity.Location = New System.Drawing.Point(6, 122)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(25, 18)
        Me.lblCity.TabIndex = 370
        Me.lblCity.Text = "City"
        '
        'txtmultLocation
        '
        Me.txtmultLocation.arrDispalyMember = Nothing
        Me.txtmultLocation.arrValueMember = Nothing
        Me.txtmultLocation.Location = New System.Drawing.Point(79, 98)
        Me.txtmultLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmultLocation.MyLinkLable1 = Me.lblLocation
        Me.txtmultLocation.MyLinkLable2 = Nothing
        Me.txtmultLocation.MyNullText = "All"
        Me.txtmultLocation.Name = "txtmultLocation"
        Me.txtmultLocation.Size = New System.Drawing.Size(281, 19)
        Me.txtmultLocation.TabIndex = 367
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(6, 98)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 368
        Me.lblLocation.Text = "Location"
        '
        'txtmultCustomer
        '
        Me.txtmultCustomer.arrDispalyMember = Nothing
        Me.txtmultCustomer.arrValueMember = Nothing
        Me.txtmultCustomer.Location = New System.Drawing.Point(94, 206)
        Me.txtmultCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmultCustomer.MyLinkLable1 = Me.lblCustomer
        Me.txtmultCustomer.MyLinkLable2 = Nothing
        Me.txtmultCustomer.MyNullText = "All"
        Me.txtmultCustomer.Name = "txtmultCustomer"
        Me.txtmultCustomer.Size = New System.Drawing.Size(281, 19)
        Me.txtmultCustomer.TabIndex = 365
        Me.txtmultCustomer.Visible = False
        '
        'lblCustomer
        '
        Me.lblCustomer.FieldName = Nothing
        Me.lblCustomer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(21, 206)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(55, 18)
        Me.lblCustomer.TabIndex = 366
        Me.lblCustomer.Text = "Customer"
        Me.lblCustomer.Visible = False
        '
        'txtVehicle
        '
        Me.txtVehicle.arrDispalyMember = Nothing
        Me.txtVehicle.arrValueMember = Nothing
        Me.txtVehicle.Location = New System.Drawing.Point(79, 73)
        Me.txtVehicle.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicle.MyLinkLable1 = Me.MyLabel1
        Me.txtVehicle.MyLinkLable2 = Nothing
        Me.txtVehicle.MyNullText = "All"
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.Size = New System.Drawing.Size(281, 19)
        Me.txtVehicle.TabIndex = 363
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 73)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(42, 18)
        Me.MyLabel1.TabIndex = 364
        Me.MyLabel1.Text = "Vehicle"
        '
        'txtTransporter
        '
        Me.txtTransporter.arrDispalyMember = Nothing
        Me.txtTransporter.arrValueMember = Nothing
        Me.txtTransporter.Location = New System.Drawing.Point(79, 49)
        Me.txtTransporter.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransporter.MyLinkLable1 = Me.MyLabel2
        Me.txtTransporter.MyLinkLable2 = Nothing
        Me.txtTransporter.MyNullText = "All"
        Me.txtTransporter.Name = "txtTransporter"
        Me.txtTransporter.Size = New System.Drawing.Size(281, 19)
        Me.txtTransporter.TabIndex = 361
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(6, 49)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(64, 18)
        Me.MyLabel2.TabIndex = 362
        Me.MyLabel2.Text = "Transporter"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.txtToDate)
        Me.RadGroupBox3.Controls.Add(Me.txtfromDate)
        Me.RadGroupBox3.HeaderText = "Date Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(16, 1)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(285, 42)
        Me.RadGroupBox3.TabIndex = 53
        Me.RadGroupBox3.Text = "Date Range"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(149, 16)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(5, 16)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(176, 15)
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(96, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "24/10/2011"
        Me.txtToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'txtfromDate
        '
        Me.txtfromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtfromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtfromDate.Location = New System.Drawing.Point(44, 15)
        Me.txtfromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtfromDate.Name = "txtfromDate"
        Me.txtfromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtfromDate.Size = New System.Drawing.Size(99, 20)
        Me.txtfromDate.TabIndex = 0
        Me.txtfromDate.TabStop = False
        Me.txtfromDate.Text = "24/10/2011"
        Me.txtfromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1135, 519)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.Name = "Gv1"
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(1135, 519)
        Me.Gv1.TabIndex = 0
        Me.Gv1.Text = "RadGridView1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(470, 10)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 143
        Me.btnClose.Text = "Close"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExcel, Me.rmPDF})
        Me.btnExport.Location = New System.Drawing.Point(158, 10)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 18)
        Me.btnExport.TabIndex = 144
        Me.btnExport.Text = "Export"
        '
        'rmExcel
        '
        Me.rmExcel.AccessibleDescription = "RadMenuItem1"
        Me.rmExcel.AccessibleName = "RadMenuItem1"
        Me.rmExcel.Name = "rmExcel"
        Me.rmExcel.Text = "Ecxel"
        '
        'rmPDF
        '
        Me.rmPDF.AccessibleDescription = "RadMenuItem2"
        Me.rmPDF.AccessibleName = "RadMenuItem2"
        Me.rmPDF.Name = "rmPDF"
        Me.rmPDF.Text = "PDF"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Location = New System.Drawing.Point(12, 10)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(68, 18)
        Me.btnGo.TabIndex = 143
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(85, 10)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 142
        Me.btnReset.Text = "Reset"
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(553, 20)
        Me.rdmenufile.TabIndex = 70
        Me.rdmenufile.Text = "File"
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rm1SaveLayout, Me.rm1DeleteLayout})
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "Settings"
        '
        'rm1SaveLayout
        '
        Me.rm1SaveLayout.AccessibleDescription = "Save Layout"
        Me.rm1SaveLayout.AccessibleName = "Save Layout"
        Me.rm1SaveLayout.Name = "rm1SaveLayout"
        Me.rm1SaveLayout.Text = "Save Layout"
        '
        'rm1DeleteLayout
        '
        Me.rm1DeleteLayout.AccessibleDescription = "Delete Layout"
        Me.rm1DeleteLayout.AccessibleName = "Delete Layout"
        Me.rm1DeleteLayout.Name = "rm1DeleteLayout"
        Me.rm1DeleteLayout.Text = "Delete Layout"
        '
        'RptGPDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(553, 400)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Name = "RptGPDetail"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptGPDetail"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents txtfromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtVehicle As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtTransporter As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents rmExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtmultCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents rm1SaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rm1DeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtmultCity As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCity As common.Controls.MyLabel
    Friend WithEvents txtmultLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
End Class

