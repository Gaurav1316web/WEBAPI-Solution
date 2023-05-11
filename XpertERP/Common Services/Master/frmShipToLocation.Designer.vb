<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShipToLocation
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
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn4 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn5 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn6 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn7 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn8 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn9 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn10 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn11 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn12 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.menuFile = New Telerik.WinControls.UI.RadMenuItem
        Me.menuImport = New Telerik.WinControls.UI.RadMenuItem
        Me.menuExport = New Telerik.WinControls.UI.RadMenuItem
        Me.menuClose = New Telerik.WinControls.UI.RadMenuItem
        Me.menuPrint = New Telerik.WinControls.UI.RadMenuItem
        Me.txtCustomerName = New common.Controls.MyTextBox
        Me.lblCustomer = New common.Controls.MyLabel
        Me.MasterTemplate = New common.UserControls.MyRadGridView
        Me.ddlShipToType = New common.Controls.MyComboBox
        Me.lblShipToType = New common.Controls.MyLabel
        Me.btnOpen = New Telerik.WinControls.UI.RadButton
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.grpShipToLocation = New Telerik.WinControls.UI.RadGroupBox
        Me.fndCustomer = New common.UserControls.txtNavigator
        Me.lblCompany = New Telerik.WinControls.UI.RadLabelElement
        Me.lblUserCode = New Telerik.WinControls.UI.RadLabelElement
        Me.RadLabelElement1 = New Telerik.WinControls.UI.RadLabelElement
        Me.RadLabelElement2 = New Telerik.WinControls.UI.RadLabelElement
        Me.RadLabelElement3 = New Telerik.WinControls.UI.RadLabelElement
        Me.RadLabelElement4 = New Telerik.WinControls.UI.RadLabelElement
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.Details = New Telerik.WinControls.UI.RadPageViewPage
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage
        Me.UcCustomFields1 = New ERP.ucCustomFields
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterTemplate.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlShipToType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShipToType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOpen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpShipToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpShipToLocation.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.Details.SuspendLayout()
        Me.pvpCustomFields.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'menuFile
        '
        Me.menuFile.AccessibleDescription = "File"
        Me.menuFile.AccessibleName = "File"
        Me.menuFile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.menuImport, Me.menuExport, Me.menuClose, Me.menuPrint})
        Me.menuFile.Name = "menuFile"
        Me.menuFile.Text = "File"
        Me.menuFile.TextAlignment = System.Drawing.ContentAlignment.BottomLeft
        Me.menuFile.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'menuImport
        '
        Me.menuImport.AccessibleDescription = "Export.."
        Me.menuImport.AccessibleName = "Export.."
        Me.menuImport.Name = "menuImport"
        Me.menuImport.Text = "Import.."
        Me.menuImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'menuExport
        '
        Me.menuExport.AccessibleDescription = "Close"
        Me.menuExport.AccessibleName = "Close"
        Me.menuExport.Name = "menuExport"
        Me.menuExport.Text = "Export"
        Me.menuExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'menuClose
        '
        Me.menuClose.AccessibleDescription = "RadMenuItem2"
        Me.menuClose.AccessibleName = "RadMenuItem2"
        Me.menuClose.Name = "menuClose"
        Me.menuClose.Text = "Close"
        Me.menuClose.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'menuPrint
        '
        Me.menuPrint.AccessibleDescription = "Print.."
        Me.menuPrint.AccessibleName = "Print.."
        Me.menuPrint.Name = "menuPrint"
        Me.menuPrint.Text = "Print.."
        Me.menuPrint.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'txtCustomerName
        '
        Me.txtCustomerName.AutoSize = False
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.Location = New System.Drawing.Point(311, 63)
        Me.txtCustomerName.MaxLength = 50
        Me.txtCustomerName.MendatroryField = False
        Me.txtCustomerName.Multiline = True
        Me.txtCustomerName.MyLinkLable1 = Me.lblCustomer
        Me.txtCustomerName.MyLinkLable2 = Nothing
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.ReadOnly = True
        Me.txtCustomerName.Size = New System.Drawing.Size(320, 21)
        Me.txtCustomerName.TabIndex = 2
        Me.txtCustomerName.Text = " "
        '
        'lblCustomer
        '
        Me.lblCustomer.Location = New System.Drawing.Point(14, 63)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(55, 18)
        Me.lblCustomer.TabIndex = 9
        Me.lblCustomer.Text = "Customer"
        '
        'MasterTemplate
        '
        Me.MasterTemplate.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MasterTemplate.Cursor = System.Windows.Forms.Cursors.Default
        Me.MasterTemplate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MasterTemplate.ForeColor = System.Drawing.Color.Black
        Me.MasterTemplate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.MasterTemplate.Location = New System.Drawing.Point(14, 99)
        '
        'MasterTemplate
        '
        Me.MasterTemplate.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GridViewTextBoxColumn1.HeaderText = "Ship-To  Location"
        GridViewTextBoxColumn1.Name = "column1"
        GridViewTextBoxColumn1.ReadOnly = True
        GridViewTextBoxColumn1.Width = 80
        GridViewTextBoxColumn2.HeaderText = "Description"
        GridViewTextBoxColumn2.Name = "column2"
        GridViewTextBoxColumn2.ReadOnly = True
        GridViewTextBoxColumn2.Width = 80
        GridViewTextBoxColumn3.HeaderText = "Address1"
        GridViewTextBoxColumn3.Name = "column4"
        GridViewTextBoxColumn3.ReadOnly = True
        GridViewTextBoxColumn3.Width = 80
        GridViewTextBoxColumn4.HeaderText = "Address2"
        GridViewTextBoxColumn4.Name = "column5"
        GridViewTextBoxColumn4.ReadOnly = True
        GridViewTextBoxColumn4.Width = 80
        GridViewTextBoxColumn5.HeaderText = "Address3"
        GridViewTextBoxColumn5.Name = "column6"
        GridViewTextBoxColumn5.ReadOnly = True
        GridViewTextBoxColumn5.Width = 80
        GridViewTextBoxColumn6.HeaderText = "Address4"
        GridViewTextBoxColumn6.Name = "column7"
        GridViewTextBoxColumn6.ReadOnly = True
        GridViewTextBoxColumn6.Width = 80
        GridViewTextBoxColumn7.HeaderText = "City"
        GridViewTextBoxColumn7.Name = "column8"
        GridViewTextBoxColumn7.ReadOnly = True
        GridViewTextBoxColumn7.Width = 80
        GridViewTextBoxColumn8.HeaderText = "State/Province"
        GridViewTextBoxColumn8.Name = "column9"
        GridViewTextBoxColumn8.ReadOnly = True
        GridViewTextBoxColumn8.Width = 80
        GridViewTextBoxColumn9.HeaderText = "ZipPostalCode"
        GridViewTextBoxColumn9.Name = "column10"
        GridViewTextBoxColumn9.ReadOnly = True
        GridViewTextBoxColumn9.Width = 80
        GridViewTextBoxColumn10.HeaderText = "Country"
        GridViewTextBoxColumn10.Name = "column11"
        GridViewTextBoxColumn10.ReadOnly = True
        GridViewTextBoxColumn10.Width = 80
        GridViewTextBoxColumn11.HeaderText = "Telephone"
        GridViewTextBoxColumn11.Name = "column12"
        GridViewTextBoxColumn11.ReadOnly = True
        GridViewTextBoxColumn11.Width = 80
        GridViewTextBoxColumn12.HeaderText = "Email"
        GridViewTextBoxColumn12.Name = "column3"
        GridViewTextBoxColumn12.ReadOnly = True
        GridViewTextBoxColumn12.Width = 80
        Me.MasterTemplate.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewTextBoxColumn3, GridViewTextBoxColumn4, GridViewTextBoxColumn5, GridViewTextBoxColumn6, GridViewTextBoxColumn7, GridViewTextBoxColumn8, GridViewTextBoxColumn9, GridViewTextBoxColumn10, GridViewTextBoxColumn11, GridViewTextBoxColumn12})
        Me.MasterTemplate.MasterTemplate.EnableGrouping = False
        Me.MasterTemplate.Name = "MasterTemplate"
        Me.MasterTemplate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MasterTemplate.Size = New System.Drawing.Size(889, 194)
        Me.MasterTemplate.TabIndex = 3
        Me.MasterTemplate.TabStop = False
        Me.MasterTemplate.Text = "RadGridView1"
        '
        'ddlShipToType
        '
        Me.ddlShipToType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlShipToType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Selected = True
        RadListDataItem1.Text = "Sales"
        RadListDataItem1.TextWrap = True
        Me.ddlShipToType.Items.Add(RadListDataItem1)
        Me.ddlShipToType.Location = New System.Drawing.Point(99, 31)
        Me.ddlShipToType.MendatroryField = False
        Me.ddlShipToType.MyLinkLable1 = Me.lblShipToType
        Me.ddlShipToType.MyLinkLable2 = Nothing
        Me.ddlShipToType.Name = "ddlShipToType"
        Me.ddlShipToType.Size = New System.Drawing.Size(132, 18)
        Me.ddlShipToType.TabIndex = 0
        Me.ddlShipToType.Text = "Sales"
        '
        'lblShipToType
        '
        Me.lblShipToType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblShipToType.Location = New System.Drawing.Point(14, 31)
        Me.lblShipToType.Name = "lblShipToType"
        Me.lblShipToType.Size = New System.Drawing.Size(60, 16)
        Me.lblShipToType.TabIndex = 10
        Me.lblShipToType.Text = "Ship Type"
        '
        'btnOpen
        '
        Me.btnOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOpen.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpen.Location = New System.Drawing.Point(18, 5)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(68, 18)
        Me.btnOpen.TabIndex = 4
        Me.btnOpen.Text = "Open"
        '
        'btnNew
        '
        Me.btnNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Location = New System.Drawing.Point(89, 5)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(68, 18)
        Me.btnNew.TabIndex = 5
        Me.btnNew.Text = "New"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(160, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(903, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        '
        'grpShipToLocation
        '
        Me.grpShipToLocation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpShipToLocation.Controls.Add(Me.lblShipToType)
        Me.grpShipToLocation.Controls.Add(Me.lblCustomer)
        Me.grpShipToLocation.Controls.Add(Me.fndCustomer)
        Me.grpShipToLocation.Controls.Add(Me.txtCustomerName)
        Me.grpShipToLocation.Controls.Add(Me.MasterTemplate)
        Me.grpShipToLocation.Controls.Add(Me.ddlShipToType)
        Me.grpShipToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpShipToLocation.HeaderText = "Ship To Location"
        Me.grpShipToLocation.Location = New System.Drawing.Point(3, 3)
        Me.grpShipToLocation.Name = "grpShipToLocation"
        Me.grpShipToLocation.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpShipToLocation.Size = New System.Drawing.Size(941, 338)
        Me.grpShipToLocation.TabIndex = 0
        Me.grpShipToLocation.Text = "Ship To Location"
        '
        'fndCustomer
        '
        Me.fndCustomer.Location = New System.Drawing.Point(99, 62)
        Me.fndCustomer.MendatroryField = True
        Me.fndCustomer.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCustomer.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCustomer.MyLinkLable1 = Me.lblCustomer
        Me.fndCustomer.MyLinkLable2 = Nothing
        Me.fndCustomer.MyMaxLength = 32767
        Me.fndCustomer.MyReadOnly = True
        Me.fndCustomer.Name = "fndCustomer"
        Me.fndCustomer.Size = New System.Drawing.Size(202, 21)
        Me.fndCustomer.TabIndex = 1
        Me.fndCustomer.Value = ""
        '
        'lblCompany
        '
        Me.lblCompany.AccessibleDescription = "Company"
        Me.lblCompany.AccessibleName = "Company"
        Me.lblCompany.Margin = New System.Windows.Forms.Padding(1)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Text = "Company"
        Me.lblCompany.TextWrap = True
        Me.lblCompany.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'lblUserCode
        '
        Me.lblUserCode.AccessibleDescription = "UserCode"
        Me.lblUserCode.AccessibleName = "UserCode"
        Me.lblUserCode.Margin = New System.Windows.Forms.Padding(1)
        Me.lblUserCode.Name = "lblUserCode"
        Me.lblUserCode.Text = "UserCode"
        Me.lblUserCode.TextWrap = True
        Me.lblUserCode.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'RadLabelElement1
        '
        Me.RadLabelElement1.AccessibleDescription = "Company"
        Me.RadLabelElement1.AccessibleName = "Company"
        Me.RadLabelElement1.Margin = New System.Windows.Forms.Padding(1)
        Me.RadLabelElement1.Name = "RadLabelElement1"
        Me.RadLabelElement1.Text = "Company"
        Me.RadLabelElement1.TextWrap = True
        Me.RadLabelElement1.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'RadLabelElement2
        '
        Me.RadLabelElement2.AccessibleDescription = "UserCode"
        Me.RadLabelElement2.AccessibleName = "UserCode"
        Me.RadLabelElement2.Margin = New System.Windows.Forms.Padding(1)
        Me.RadLabelElement2.Name = "RadLabelElement2"
        Me.RadLabelElement2.Text = "UserCode"
        Me.RadLabelElement2.TextWrap = True
        Me.RadLabelElement2.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'RadLabelElement3
        '
        Me.RadLabelElement3.AccessibleDescription = "Company"
        Me.RadLabelElement3.AccessibleName = "Company"
        Me.RadLabelElement3.Margin = New System.Windows.Forms.Padding(1)
        Me.RadLabelElement3.Name = "RadLabelElement3"
        Me.RadLabelElement3.Text = "Company"
        Me.RadLabelElement3.TextWrap = True
        Me.RadLabelElement3.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'RadLabelElement4
        '
        Me.RadLabelElement4.AccessibleDescription = "UserCode"
        Me.RadLabelElement4.AccessibleName = "UserCode"
        Me.RadLabelElement4.Margin = New System.Windows.Forms.Padding(1)
        Me.RadLabelElement4.Name = "RadLabelElement4"
        Me.RadLabelElement4.Text = "UserCode"
        Me.RadLabelElement4.TextWrap = True
        Me.RadLabelElement4.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'Timer1
        '
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.menuFile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(989, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnOpen)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(989, 450)
        Me.SplitContainer1.SplitterDistance = 415
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.Details)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Location = New System.Drawing.Point(3, 5)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.Details
        Me.RadPageView1.Size = New System.Drawing.Size(968, 407)
        Me.RadPageView1.TabIndex = 2
        Me.RadPageView1.Text = "Ship To Location"
        '
        'Details
        '
        Me.Details.Controls.Add(Me.grpShipToLocation)
        Me.Details.Location = New System.Drawing.Point(10, 37)
        Me.Details.Name = "Details"
        Me.Details.Size = New System.Drawing.Size(947, 359)
        Me.Details.Text = "Details"
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(730, 359)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(730, 359)
        Me.UcCustomFields1.TabIndex = 2
        '
        'frmShipToLocation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(989, 470)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmShipToLocation"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Ship-To Location"
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterTemplate.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlShipToType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShipToType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOpen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpShipToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpShipToLocation.ResumeLayout(False)
        Me.grpShipToLocation.PerformLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.Details.ResumeLayout(False)
        Me.pvpCustomFields.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtCustomerName As common.Controls.MyTextBox
    Friend WithEvents dgShipToLocation As common.UserControls.MyRadGridView
    Friend WithEvents ddlShipToType As common.Controls.MyComboBox
    Friend WithEvents btnOpen As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents menuFile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents grpShipToLocation As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblCompany As Telerik.WinControls.UI.RadLabelElement
    Friend WithEvents lblUserCode As Telerik.WinControls.UI.RadLabelElement
    Friend WithEvents RadLabelElement1 As Telerik.WinControls.UI.RadLabelElement
    Friend WithEvents RadLabelElement2 As Telerik.WinControls.UI.RadLabelElement
    Friend WithEvents RadLabelElement3 As Telerik.WinControls.UI.RadLabelElement
    Friend WithEvents RadLabelElement4 As Telerik.WinControls.UI.RadLabelElement
    Friend WithEvents menuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents menuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuPrint As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents fndCustomer As common.UserControls.txtNavigator
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents lblShipToType As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents Details As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
End Class

