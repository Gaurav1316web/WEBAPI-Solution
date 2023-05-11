<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMakePayment
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtEntryNo = New common.UserControls.txtNavigator()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnUnSelect = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.LblRouteName = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.fndZone = New common.UserControls.txtFinder()
        Me.rdlblrouteno = New common.Controls.MyLabel()
        Me.txtLocation = New common.Controls.MyTextBox()
        Me.lblZoneName = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.fndPayType = New common.UserControls.txtFinder()
        Me.lblCustomerName = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.txtCustomer = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndRouteNo = New common.UserControls.txtFinder()
        Me.ddlType = New common.Controls.MyComboBox()
        Me.rdlbltransferdate = New common.Controls.MyLabel()
        Me.dtDocDate = New common.Controls.MyDateTimePicker()
        Me.MasterTemplate = New common.UserControls.MyRadGridView()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.lblTransporterName = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.lblTransporter = New common.Controls.MyLabel()
        Me.txtTransporterReceiptAmt = New common.MyNumBox()
        Me.lblTransporterReceiptAmt = New common.Controls.MyLabel()
        Me.lblTotalReceiptAmt = New common.Controls.MyLabel()
        Me.txtTotalReceiptAmt = New common.MyNumBox()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblRouteName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblrouteno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblZoneName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbltransferdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterTemplate.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTransporterReceiptAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporterReceiptAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalReceiptAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalReceiptAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(6, 12)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(82, 16)
        Me.RadLabel1.TabIndex = 9
        Me.RadLabel1.Text = "Quick Entry No"
        '
        'txtEntryNo
        '
        Me.txtEntryNo.FieldName = Nothing
        Me.txtEntryNo.Location = New System.Drawing.Point(97, 10)
        Me.txtEntryNo.MendatroryField = False
        Me.txtEntryNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtEntryNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtEntryNo.MyLinkLable1 = Me.RadLabel1
        Me.txtEntryNo.MyLinkLable2 = Nothing
        Me.txtEntryNo.MyMaxLength = 32767
        Me.txtEntryNo.MyReadOnly = False
        Me.txtEntryNo.Name = "txtEntryNo"
        Me.txtEntryNo.Size = New System.Drawing.Size(254, 20)
        Me.txtEntryNo.TabIndex = 0
        Me.txtEntryNo.TabStop = False
        Me.txtEntryNo.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(351, 10)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 20)
        Me.btnAddNew.TabIndex = 1
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.btnReverse)
        Me.RadGroupBox1.Controls.Add(Me.btnUnSelect)
        Me.RadGroupBox1.Controls.Add(Me.btnPrint)
        Me.RadGroupBox1.Controls.Add(Me.btnClose)
        Me.RadGroupBox1.Controls.Add(Me.btnDelete)
        Me.RadGroupBox1.Controls.Add(Me.btnPost)
        Me.RadGroupBox1.Controls.Add(Me.btnSave)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 439)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(832, 34)
        Me.RadGroupBox1.TabIndex = 1
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(381, 6)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(113, 22)
        Me.btnReverse.TabIndex = 260
        Me.btnReverse.Text = "Revese and Unpost"
        Me.btnReverse.Visible = False
        '
        'btnUnSelect
        '
        Me.btnUnSelect.Location = New System.Drawing.Point(296, 6)
        Me.btnUnSelect.Name = "btnUnSelect"
        Me.btnUnSelect.Size = New System.Drawing.Size(80, 22)
        Me.btnUnSelect.TabIndex = 259
        Me.btnUnSelect.Text = "Select All"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(221, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(748, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(148, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(77, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(6, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTotalReceiptAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTotalReceiptAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTransporterReceiptAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTransporterReceiptAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTransporter)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTransporterName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblRouteName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndZone)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblZoneName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndPayType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCustomerName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustomer)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndRouteNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ddlType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdlblrouteno)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEntryNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdlbltransferdate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtDocDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.MasterTemplate)
        Me.SplitContainer1.Size = New System.Drawing.Size(832, 419)
        Me.SplitContainer1.SplitterDistance = 146
        Me.SplitContainer1.TabIndex = 0
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationName.Location = New System.Drawing.Point(469, 32)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(182, 18)
        Me.lblLocationName.TabIndex = 112
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocationName.TextWrap = False
        '
        'LblRouteName
        '
        Me.LblRouteName.AutoSize = False
        Me.LblRouteName.BorderVisible = True
        Me.LblRouteName.FieldName = Nothing
        Me.LblRouteName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRouteName.Location = New System.Drawing.Point(278, 74)
        Me.LblRouteName.Name = "LblRouteName"
        Me.LblRouteName.Size = New System.Drawing.Size(261, 18)
        Me.LblRouteName.TabIndex = 111
        Me.LblRouteName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblRouteName.TextWrap = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 54)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel1.TabIndex = 11
        Me.MyLabel1.Text = "Zone"
        '
        'fndZone
        '
        Me.fndZone.CalculationExpression = Nothing
        Me.fndZone.FieldCode = Nothing
        Me.fndZone.FieldDesc = Nothing
        Me.fndZone.FieldMaxLength = 0
        Me.fndZone.FieldName = Nothing
        Me.fndZone.isCalculatedField = False
        Me.fndZone.IsSourceFromTable = False
        Me.fndZone.IsSourceFromValueList = False
        Me.fndZone.IsUnique = False
        Me.fndZone.Location = New System.Drawing.Point(97, 53)
        Me.fndZone.MendatroryField = True
        Me.fndZone.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndZone.MyLinkLable1 = Me.rdlblrouteno
        Me.fndZone.MyLinkLable2 = Nothing
        Me.fndZone.MyReadOnly = False
        Me.fndZone.MyShowMasterFormButton = False
        Me.fndZone.Name = "fndZone"
        Me.fndZone.ReferenceFieldDesc = Nothing
        Me.fndZone.ReferenceFieldName = Nothing
        Me.fndZone.ReferenceTableName = Nothing
        Me.fndZone.Size = New System.Drawing.Size(178, 19)
        Me.fndZone.TabIndex = 103
        Me.fndZone.Value = ""
        '
        'rdlblrouteno
        '
        Me.rdlblrouteno.FieldName = Nothing
        Me.rdlblrouteno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblrouteno.Location = New System.Drawing.Point(6, 75)
        Me.rdlblrouteno.Name = "rdlblrouteno"
        Me.rdlblrouteno.Size = New System.Drawing.Size(67, 16)
        Me.rdlblrouteno.TabIndex = 10
        Me.rdlblrouteno.Text = "Route Code"
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(372, 32)
        Me.txtLocation.MendatroryField = False
        Me.txtLocation.MyLinkLable1 = Nothing
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReadOnly = True
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(91, 18)
        Me.txtLocation.TabIndex = 102
        Me.txtLocation.TabStop = False
        '
        'lblZoneName
        '
        Me.lblZoneName.AutoSize = False
        Me.lblZoneName.BorderVisible = True
        Me.lblZoneName.FieldName = Nothing
        Me.lblZoneName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZoneName.Location = New System.Drawing.Point(278, 53)
        Me.lblZoneName.Name = "lblZoneName"
        Me.lblZoneName.Size = New System.Drawing.Size(261, 18)
        Me.lblZoneName.TabIndex = 110
        Me.lblZoneName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblZoneName.TextWrap = False
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(6, 33)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(82, 16)
        Me.RadLabel3.TabIndex = 106
        Me.RadLabel3.Text = "Payment Mode"
        '
        'fndPayType
        '
        Me.fndPayType.CalculationExpression = Nothing
        Me.fndPayType.FieldCode = Nothing
        Me.fndPayType.FieldDesc = Nothing
        Me.fndPayType.FieldMaxLength = 0
        Me.fndPayType.FieldName = Nothing
        Me.fndPayType.isCalculatedField = False
        Me.fndPayType.IsSourceFromTable = False
        Me.fndPayType.IsSourceFromValueList = False
        Me.fndPayType.IsUnique = False
        Me.fndPayType.Location = New System.Drawing.Point(97, 32)
        Me.fndPayType.MendatroryField = True
        Me.fndPayType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPayType.MyLinkLable1 = Me.RadLabel3
        Me.fndPayType.MyLinkLable2 = Nothing
        Me.fndPayType.MyReadOnly = False
        Me.fndPayType.MyShowMasterFormButton = False
        Me.fndPayType.Name = "fndPayType"
        Me.fndPayType.ReferenceFieldDesc = Nothing
        Me.fndPayType.ReferenceFieldName = Nothing
        Me.fndPayType.ReferenceTableName = Nothing
        Me.fndPayType.Size = New System.Drawing.Size(214, 19)
        Me.fndPayType.TabIndex = 105
        Me.fndPayType.Value = ""
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = False
        Me.lblCustomerName.BorderVisible = True
        Me.lblCustomerName.FieldName = Nothing
        Me.lblCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerName.Location = New System.Drawing.Point(278, 115)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(261, 18)
        Me.lblCustomerName.TabIndex = 108
        Me.lblCustomerName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustomerName.TextWrap = False
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(6, 116)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel2.TabIndex = 109
        Me.RadLabel2.Text = "Customer"
        '
        'txtCustomer
        '
        Me.txtCustomer.CalculationExpression = Nothing
        Me.txtCustomer.FieldCode = Nothing
        Me.txtCustomer.FieldDesc = Nothing
        Me.txtCustomer.FieldMaxLength = 0
        Me.txtCustomer.FieldName = Nothing
        Me.txtCustomer.isCalculatedField = False
        Me.txtCustomer.IsSourceFromTable = False
        Me.txtCustomer.IsSourceFromValueList = False
        Me.txtCustomer.IsUnique = False
        Me.txtCustomer.Location = New System.Drawing.Point(97, 115)
        Me.txtCustomer.MendatroryField = True
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Me.RadLabel2
        Me.txtCustomer.MyLinkLable2 = Me.lblCustomerName
        Me.txtCustomer.MyReadOnly = False
        Me.txtCustomer.MyShowMasterFormButton = False
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.ReferenceFieldDesc = Nothing
        Me.txtCustomer.ReferenceFieldName = Nothing
        Me.txtCustomer.ReferenceTableName = Nothing
        Me.txtCustomer.Size = New System.Drawing.Size(178, 18)
        Me.txtCustomer.TabIndex = 107
        Me.txtCustomer.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(317, 33)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel2.TabIndex = 12
        Me.MyLabel2.Text = "Location"
        '
        'fndRouteNo
        '
        Me.fndRouteNo.CalculationExpression = Nothing
        Me.fndRouteNo.FieldCode = Nothing
        Me.fndRouteNo.FieldDesc = Nothing
        Me.fndRouteNo.FieldMaxLength = 0
        Me.fndRouteNo.FieldName = Nothing
        Me.fndRouteNo.isCalculatedField = False
        Me.fndRouteNo.IsSourceFromTable = False
        Me.fndRouteNo.IsSourceFromValueList = False
        Me.fndRouteNo.IsUnique = False
        Me.fndRouteNo.Location = New System.Drawing.Point(97, 74)
        Me.fndRouteNo.MendatroryField = True
        Me.fndRouteNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRouteNo.MyLinkLable1 = Me.rdlblrouteno
        Me.fndRouteNo.MyLinkLable2 = Nothing
        Me.fndRouteNo.MyReadOnly = False
        Me.fndRouteNo.MyShowMasterFormButton = False
        Me.fndRouteNo.Name = "fndRouteNo"
        Me.fndRouteNo.ReferenceFieldDesc = Nothing
        Me.fndRouteNo.ReferenceFieldName = Nothing
        Me.fndRouteNo.ReferenceTableName = Nothing
        Me.fndRouteNo.Size = New System.Drawing.Size(178, 19)
        Me.fndRouteNo.TabIndex = 3
        Me.fndRouteNo.Value = ""
        '
        'ddlType
        '
        Me.ddlType.CalculationExpression = Nothing
        Me.ddlType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlType.FieldCode = Nothing
        Me.ddlType.FieldDesc = Nothing
        Me.ddlType.FieldMaxLength = 0
        Me.ddlType.FieldName = Nothing
        Me.ddlType.isCalculatedField = False
        Me.ddlType.IsSourceFromTable = False
        Me.ddlType.IsSourceFromValueList = False
        Me.ddlType.IsUnique = False
        RadListDataItem1.Selected = True
        RadListDataItem1.Text = "Receipt"
        RadListDataItem2.Text = "Misc Receipt"
        RadListDataItem3.Text = "Payment"
        Me.ddlType.Items.Add(RadListDataItem1)
        Me.ddlType.Items.Add(RadListDataItem2)
        Me.ddlType.Items.Add(RadListDataItem3)
        Me.ddlType.Location = New System.Drawing.Point(543, 10)
        Me.ddlType.MendatroryField = True
        Me.ddlType.MyLinkLable1 = Nothing
        Me.ddlType.MyLinkLable2 = Nothing
        Me.ddlType.Name = "ddlType"
        Me.ddlType.ReferenceFieldDesc = Nothing
        Me.ddlType.ReferenceFieldName = Nothing
        Me.ddlType.ReferenceTableName = Nothing
        Me.ddlType.Size = New System.Drawing.Size(69, 20)
        Me.ddlType.TabIndex = 4
        Me.ddlType.Text = "Receipt"
        Me.ddlType.Visible = False
        '
        'rdlbltransferdate
        '
        Me.rdlbltransferdate.FieldName = Nothing
        Me.rdlbltransferdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlbltransferdate.Location = New System.Drawing.Point(375, 12)
        Me.rdlbltransferdate.Name = "rdlbltransferdate"
        Me.rdlbltransferdate.Size = New System.Drawing.Size(30, 16)
        Me.rdlbltransferdate.TabIndex = 12
        Me.rdlbltransferdate.Text = "Date"
        '
        'dtDocDate
        '
        Me.dtDocDate.CalculationExpression = Nothing
        Me.dtDocDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtDocDate.FieldCode = Nothing
        Me.dtDocDate.FieldDesc = Nothing
        Me.dtDocDate.FieldMaxLength = 0
        Me.dtDocDate.FieldName = Nothing
        Me.dtDocDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtDocDate.isCalculatedField = False
        Me.dtDocDate.IsSourceFromTable = False
        Me.dtDocDate.IsSourceFromValueList = False
        Me.dtDocDate.IsUnique = False
        Me.dtDocDate.Location = New System.Drawing.Point(411, 10)
        Me.dtDocDate.MendatroryField = False
        Me.dtDocDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtDocDate.MyLinkLable1 = Me.rdlbltransferdate
        Me.dtDocDate.MyLinkLable2 = Nothing
        Me.dtDocDate.Name = "dtDocDate"
        Me.dtDocDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtDocDate.ReferenceFieldDesc = Nothing
        Me.dtDocDate.ReferenceFieldName = Nothing
        Me.dtDocDate.ReferenceTableName = Nothing
        Me.dtDocDate.Size = New System.Drawing.Size(128, 20)
        Me.dtDocDate.TabIndex = 2
        Me.dtDocDate.TabStop = False
        Me.dtDocDate.Text = "27/06/2011 12:00 AM"
        Me.dtDocDate.Value = New Date(2011, 6, 27, 0, 0, 0, 0)
        '
        'MasterTemplate
        '
        Me.MasterTemplate.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MasterTemplate.Cursor = System.Windows.Forms.Cursors.Default
        Me.MasterTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MasterTemplate.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MasterTemplate.ForeColor = System.Drawing.Color.Black
        Me.MasterTemplate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.MasterTemplate.Location = New System.Drawing.Point(0, 0)
        '
        'MasterTemplate
        '
        Me.MasterTemplate.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.MasterTemplate.MasterTemplate.AllowColumnReorder = False
        Me.MasterTemplate.MasterTemplate.AllowDeleteRow = False
        Me.MasterTemplate.MasterTemplate.EnableGrouping = False
        Me.MasterTemplate.MasterTemplate.EnableSorting = False
        Me.MasterTemplate.MasterTemplate.ShowHeaderCellButtons = True
        Me.MasterTemplate.Name = "MasterTemplate"
        Me.MasterTemplate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MasterTemplate.ShowHeaderCellButtons = True
        Me.MasterTemplate.Size = New System.Drawing.Size(832, 269)
        Me.MasterTemplate.TabIndex = 0
        Me.MasterTemplate.TabStop = False
        Me.MasterTemplate.Text = "RadGridView1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Save Layout"
        Me.RadMenuItem1.AccessibleName = "Save Layout"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem2.AccessibleName = "Delete Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Delete Layout"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(832, 20)
        Me.RadMenu1.TabIndex = 2
        Me.RadMenu1.Text = "RadMenu1"
        '
        'lblTransporterName
        '
        Me.lblTransporterName.AutoSize = False
        Me.lblTransporterName.BorderVisible = True
        Me.lblTransporterName.FieldName = Nothing
        Me.lblTransporterName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransporterName.Location = New System.Drawing.Point(278, 95)
        Me.lblTransporterName.Name = "lblTransporterName"
        Me.lblTransporterName.Size = New System.Drawing.Size(261, 18)
        Me.lblTransporterName.TabIndex = 114
        Me.lblTransporterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTransporterName.TextWrap = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(6, 96)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel4.TabIndex = 113
        Me.MyLabel4.Text = "Transporter"
        '
        'lblTransporter
        '
        Me.lblTransporter.AutoSize = False
        Me.lblTransporter.BorderVisible = True
        Me.lblTransporter.FieldName = Nothing
        Me.lblTransporter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransporter.Location = New System.Drawing.Point(97, 95)
        Me.lblTransporter.Name = "lblTransporter"
        Me.lblTransporter.Size = New System.Drawing.Size(178, 18)
        Me.lblTransporter.TabIndex = 115
        Me.lblTransporter.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTransporter.TextWrap = False
        '
        'txtTransporterReceiptAmt
        '
        Me.txtTransporterReceiptAmt.CalculationExpression = Nothing
        Me.txtTransporterReceiptAmt.DecimalPlaces = 2
        Me.txtTransporterReceiptAmt.FieldCode = Nothing
        Me.txtTransporterReceiptAmt.FieldDesc = Nothing
        Me.txtTransporterReceiptAmt.FieldMaxLength = 0
        Me.txtTransporterReceiptAmt.FieldName = Nothing
        Me.txtTransporterReceiptAmt.isCalculatedField = False
        Me.txtTransporterReceiptAmt.IsSourceFromTable = False
        Me.txtTransporterReceiptAmt.IsSourceFromValueList = False
        Me.txtTransporterReceiptAmt.IsUnique = False
        Me.txtTransporterReceiptAmt.Location = New System.Drawing.Point(659, 94)
        Me.txtTransporterReceiptAmt.MendatroryField = False
        Me.txtTransporterReceiptAmt.MyLinkLable1 = Me.lblTransporterReceiptAmt
        Me.txtTransporterReceiptAmt.MyLinkLable2 = Nothing
        Me.txtTransporterReceiptAmt.Name = "txtTransporterReceiptAmt"
        Me.txtTransporterReceiptAmt.ReferenceFieldDesc = Nothing
        Me.txtTransporterReceiptAmt.ReferenceFieldName = Nothing
        Me.txtTransporterReceiptAmt.ReferenceTableName = Nothing
        Me.txtTransporterReceiptAmt.Size = New System.Drawing.Size(123, 20)
        Me.txtTransporterReceiptAmt.TabIndex = 116
        Me.txtTransporterReceiptAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTransporterReceiptAmt.Value = 0R
        '
        'lblTransporterReceiptAmt
        '
        Me.lblTransporterReceiptAmt.FieldName = Nothing
        Me.lblTransporterReceiptAmt.Location = New System.Drawing.Point(546, 95)
        Me.lblTransporterReceiptAmt.Name = "lblTransporterReceiptAmt"
        Me.lblTransporterReceiptAmt.Size = New System.Drawing.Size(107, 18)
        Me.lblTransporterReceiptAmt.TabIndex = 117
        Me.lblTransporterReceiptAmt.Text = "Transporter Amount"
        '
        'lblTotalReceiptAmt
        '
        Me.lblTotalReceiptAmt.FieldName = Nothing
        Me.lblTotalReceiptAmt.Location = New System.Drawing.Point(546, 115)
        Me.lblTotalReceiptAmt.Name = "lblTotalReceiptAmt"
        Me.lblTotalReceiptAmt.Size = New System.Drawing.Size(87, 18)
        Me.lblTotalReceiptAmt.TabIndex = 119
        Me.lblTotalReceiptAmt.Text = "Receipt Amount"
        '
        'txtTotalReceiptAmt
        '
        Me.txtTotalReceiptAmt.CalculationExpression = Nothing
        Me.txtTotalReceiptAmt.DecimalPlaces = 2
        Me.txtTotalReceiptAmt.FieldCode = Nothing
        Me.txtTotalReceiptAmt.FieldDesc = Nothing
        Me.txtTotalReceiptAmt.FieldMaxLength = 0
        Me.txtTotalReceiptAmt.FieldName = Nothing
        Me.txtTotalReceiptAmt.isCalculatedField = False
        Me.txtTotalReceiptAmt.IsSourceFromTable = False
        Me.txtTotalReceiptAmt.IsSourceFromValueList = False
        Me.txtTotalReceiptAmt.IsUnique = False
        Me.txtTotalReceiptAmt.Location = New System.Drawing.Point(659, 114)
        Me.txtTotalReceiptAmt.MendatroryField = False
        Me.txtTotalReceiptAmt.MyLinkLable1 = Me.lblTotalReceiptAmt
        Me.txtTotalReceiptAmt.MyLinkLable2 = Nothing
        Me.txtTotalReceiptAmt.Name = "txtTotalReceiptAmt"
        Me.txtTotalReceiptAmt.ReferenceFieldDesc = Nothing
        Me.txtTotalReceiptAmt.ReferenceFieldName = Nothing
        Me.txtTotalReceiptAmt.ReferenceTableName = Nothing
        Me.txtTotalReceiptAmt.Size = New System.Drawing.Size(123, 20)
        Me.txtTotalReceiptAmt.TabIndex = 118
        Me.txtTotalReceiptAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalReceiptAmt.Value = 0R
        '
        'FrmMakePayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(832, 473)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.MinimumSize = New System.Drawing.Size(599, 443)
        Me.Name = "FrmMakePayment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Make Payment"
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblRouteName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblrouteno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblZoneName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbltransferdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterTemplate.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTransporterReceiptAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporterReceiptAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalReceiptAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalReceiptAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtEntryNo As common.UserControls.txtNavigator
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dtDocDate As common.Controls.MyDateTimePicker
    Friend WithEvents rdlbltransferdate As common.Controls.MyLabel
    Friend WithEvents rdlblrouteno As common.Controls.MyLabel
    Friend WithEvents ddlType As common.Controls.MyComboBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndRouteNo As common.UserControls.txtFinder
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents btnUnSelect As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndZone As common.UserControls.txtFinder
    Friend WithEvents fndPayType As common.UserControls.txtFinder
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents LblRouteName As common.Controls.MyLabel
    Friend WithEvents lblZoneName As common.Controls.MyLabel
    Friend WithEvents lblCustomerName As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents txtCustomer As common.UserControls.txtFinder
    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents btnReverse As RadButton
    Friend WithEvents lblTransporter As common.Controls.MyLabel
    Friend WithEvents lblTransporterName As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblTotalReceiptAmt As common.Controls.MyLabel
    Friend WithEvents txtTotalReceiptAmt As common.MyNumBox
    Friend WithEvents lblTransporterReceiptAmt As common.Controls.MyLabel
    Friend WithEvents txtTransporterReceiptAmt As common.MyNumBox
End Class

