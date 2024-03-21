<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMaterialQuotationOrder
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.chkTaxable = New common.Controls.MyCheckBox()
        Me.txtlocation = New common.Controls.MyTextBox()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.chkOnHold = New Telerik.WinControls.UI.RadCheckBox()
        Me.UsLock1 = New common.usLock()
        Me.txtMaterialQuotation = New common.UserControls.txtFinder()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel27 = New common.Controls.MyLabel()
        Me.lblTotRAmt = New common.Controls.MyLabel()
        Me.lblQuotation = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.txtcustdesc = New common.Controls.MyTextBox()
        Me.lblQuotationOrder = New common.Controls.MyLabel()
        Me.txtCustomer = New common.UserControls.txtFinder()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtRmks = New common.Controls.MyTextBox()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.txtRefNo = New common.Controls.MyTextBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.btnAmendment = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.Setting = New Telerik.WinControls.UI.RadMenuItem()
        Me.saveLayoutbtn = New Telerik.WinControls.UI.RadMenuItem()
        Me.DeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnimporthead = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnimportdetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnexporthead = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnexportdetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.chkTaxable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtlocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQuotation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcustdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQuotationOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRmks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAmendment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkTaxable)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtlocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel15)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkOnHold)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMaterialQuotation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel27)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTotRAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblQuotation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtcustdesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblQuotationOrder)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustomer)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRmks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRefNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtComment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel8)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAmendment)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(884, 452)
        Me.SplitContainer1.SplitterDistance = 420
        Me.SplitContainer1.TabIndex = 1
        '
        'chkTaxable
        '
        Me.chkTaxable.Enabled = False
        Me.chkTaxable.Location = New System.Drawing.Point(507, 31)
        Me.chkTaxable.MyLinkLable1 = Nothing
        Me.chkTaxable.MyLinkLable2 = Nothing
        Me.chkTaxable.Name = "chkTaxable"
        Me.chkTaxable.Size = New System.Drawing.Size(58, 18)
        Me.chkTaxable.TabIndex = 1435
        Me.chkTaxable.Tag1 = Nothing
        Me.chkTaxable.Text = "Taxable"
        '
        'txtlocation
        '
        Me.txtlocation.CalculationExpression = Nothing
        Me.txtlocation.FieldCode = Nothing
        Me.txtlocation.FieldDesc = Nothing
        Me.txtlocation.FieldMaxLength = 0
        Me.txtlocation.FieldName = Nothing
        Me.txtlocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlocation.isCalculatedField = False
        Me.txtlocation.IsSourceFromTable = False
        Me.txtlocation.IsSourceFromValueList = False
        Me.txtlocation.IsUnique = False
        Me.txtlocation.Location = New System.Drawing.Point(323, 75)
        Me.txtlocation.MaxLength = 200
        Me.txtlocation.MendatroryField = False
        Me.txtlocation.MyLinkLable1 = Me.RadLabel15
        Me.txtlocation.MyLinkLable2 = Nothing
        Me.txtlocation.Name = "txtlocation"
        Me.txtlocation.ReadOnly = True
        Me.txtlocation.ReferenceFieldDesc = Nothing
        Me.txtlocation.ReferenceFieldName = Nothing
        Me.txtlocation.ReferenceTableName = Nothing
        Me.txtlocation.Size = New System.Drawing.Size(228, 18)
        Me.txtlocation.TabIndex = 30
        Me.txtlocation.TabStop = False
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(7, 74)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 29
        Me.RadLabel15.Text = "Location"
        '
        'fndLocation
        '
        Me.fndLocation.CalculationExpression = Nothing
        Me.fndLocation.Enabled = False
        Me.fndLocation.FieldCode = Nothing
        Me.fndLocation.FieldDesc = Nothing
        Me.fndLocation.FieldMaxLength = 0
        Me.fndLocation.FieldName = Nothing
        Me.fndLocation.isCalculatedField = False
        Me.fndLocation.IsSourceFromTable = False
        Me.fndLocation.IsSourceFromValueList = False
        Me.fndLocation.IsUnique = False
        Me.fndLocation.Location = New System.Drawing.Point(118, 74)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.RadLabel15
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(204, 19)
        Me.fndLocation.TabIndex = 28
        Me.fndLocation.Value = ""
        '
        'chkOnHold
        '
        Me.chkOnHold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnHold.Location = New System.Drawing.Point(741, 31)
        Me.chkOnHold.Name = "chkOnHold"
        Me.chkOnHold.Size = New System.Drawing.Size(62, 16)
        Me.chkOnHold.TabIndex = 4
        Me.chkOnHold.Text = "On Hold"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(805, 29)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(75, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 16
        '
        'txtMaterialQuotation
        '
        Me.txtMaterialQuotation.CalculationExpression = Nothing
        Me.txtMaterialQuotation.FieldCode = Nothing
        Me.txtMaterialQuotation.FieldDesc = Nothing
        Me.txtMaterialQuotation.FieldMaxLength = 0
        Me.txtMaterialQuotation.FieldName = Nothing
        Me.txtMaterialQuotation.isCalculatedField = False
        Me.txtMaterialQuotation.IsSourceFromTable = False
        Me.txtMaterialQuotation.IsSourceFromValueList = False
        Me.txtMaterialQuotation.IsUnique = False
        Me.txtMaterialQuotation.Location = New System.Drawing.Point(118, 49)
        Me.txtMaterialQuotation.MendatroryField = True
        Me.txtMaterialQuotation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMaterialQuotation.MyLinkLable1 = Me.RadLabel2
        Me.txtMaterialQuotation.MyLinkLable2 = Nothing
        Me.txtMaterialQuotation.MyReadOnly = False
        Me.txtMaterialQuotation.MyShowMasterFormButton = False
        Me.txtMaterialQuotation.Name = "txtMaterialQuotation"
        Me.txtMaterialQuotation.ReferenceFieldDesc = Nothing
        Me.txtMaterialQuotation.ReferenceFieldName = Nothing
        Me.txtMaterialQuotation.ReferenceTableName = Nothing
        Me.txtMaterialQuotation.Size = New System.Drawing.Size(204, 22)
        Me.txtMaterialQuotation.TabIndex = 27
        Me.txtMaterialQuotation.Value = ""
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(465, 139)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel2.TabIndex = 22
        Me.RadLabel2.Text = "Comment"
        '
        'RadLabel27
        '
        Me.RadLabel27.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel27.FieldName = Nothing
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(690, 400)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(74, 16)
        Me.RadLabel27.TabIndex = 14
        Me.RadLabel27.Text = "Total Amount"
        '
        'lblTotRAmt
        '
        Me.lblTotRAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotRAmt.AutoSize = False
        Me.lblTotRAmt.BorderVisible = True
        Me.lblTotRAmt.FieldName = Nothing
        Me.lblTotRAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt.Location = New System.Drawing.Point(769, 398)
        Me.lblTotRAmt.Name = "lblTotRAmt"
        Me.lblTotRAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotRAmt.TabIndex = 13
        Me.lblTotRAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblQuotation
        '
        Me.lblQuotation.FieldName = Nothing
        Me.lblQuotation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQuotation.Location = New System.Drawing.Point(7, 53)
        Me.lblQuotation.Name = "lblQuotation"
        Me.lblQuotation.Size = New System.Drawing.Size(99, 16)
        Me.lblQuotation.TabIndex = 26
        Me.lblQuotation.Text = "Material Quotation"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(7, 160)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(874, 238)
        Me.RadGroupBox2.TabIndex = 12
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(854, 208)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'txtcustdesc
        '
        Me.txtcustdesc.CalculationExpression = Nothing
        Me.txtcustdesc.FieldCode = Nothing
        Me.txtcustdesc.FieldDesc = Nothing
        Me.txtcustdesc.FieldMaxLength = 0
        Me.txtcustdesc.FieldName = Nothing
        Me.txtcustdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustdesc.isCalculatedField = False
        Me.txtcustdesc.IsSourceFromTable = False
        Me.txtcustdesc.IsSourceFromValueList = False
        Me.txtcustdesc.IsUnique = False
        Me.txtcustdesc.Location = New System.Drawing.Point(323, 96)
        Me.txtcustdesc.MaxLength = 200
        Me.txtcustdesc.MendatroryField = False
        Me.txtcustdesc.MyLinkLable1 = Me.RadLabel2
        Me.txtcustdesc.MyLinkLable2 = Nothing
        Me.txtcustdesc.Name = "txtcustdesc"
        Me.txtcustdesc.ReferenceFieldDesc = Nothing
        Me.txtcustdesc.ReferenceFieldName = Nothing
        Me.txtcustdesc.ReferenceTableName = Nothing
        Me.txtcustdesc.Size = New System.Drawing.Size(228, 18)
        Me.txtcustdesc.TabIndex = 25
        '
        'lblQuotationOrder
        '
        Me.lblQuotationOrder.FieldName = Nothing
        Me.lblQuotationOrder.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQuotationOrder.Location = New System.Drawing.Point(7, 31)
        Me.lblQuotationOrder.Name = "lblQuotationOrder"
        Me.lblQuotationOrder.Size = New System.Drawing.Size(75, 16)
        Me.lblQuotationOrder.TabIndex = 20
        Me.lblQuotationOrder.Text = "Document No"
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
        Me.txtCustomer.Location = New System.Drawing.Point(118, 96)
        Me.txtCustomer.MendatroryField = True
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Me.RadLabel2
        Me.txtCustomer.MyLinkLable2 = Nothing
        Me.txtCustomer.MyReadOnly = False
        Me.txtCustomer.MyShowMasterFormButton = False
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.ReferenceFieldDesc = Nothing
        Me.txtCustomer.ReferenceFieldName = Nothing
        Me.txtCustomer.ReferenceTableName = Nothing
        Me.txtCustomer.Size = New System.Drawing.Size(204, 20)
        Me.txtCustomer.TabIndex = 24
        Me.txtCustomer.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(360, 29)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(118, 119)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel5
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(346, 18)
        Me.txtDesc.TabIndex = 8
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(7, 120)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel5.TabIndex = 18
        Me.RadLabel5.Text = "Description"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(7, 96)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel3.TabIndex = 19
        Me.MyLabel3.Text = "Customer"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(422, 30)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(79, 18)
        Me.txtDate.TabIndex = 2
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(388, 31)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 21
        Me.RadLabel4.Text = "Date"
        '
        'txtRmks
        '
        Me.txtRmks.CalculationExpression = Nothing
        Me.txtRmks.FieldCode = Nothing
        Me.txtRmks.FieldDesc = Nothing
        Me.txtRmks.FieldMaxLength = 0
        Me.txtRmks.FieldName = Nothing
        Me.txtRmks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRmks.isCalculatedField = False
        Me.txtRmks.IsSourceFromTable = False
        Me.txtRmks.IsSourceFromValueList = False
        Me.txtRmks.IsUnique = False
        Me.txtRmks.Location = New System.Drawing.Point(118, 138)
        Me.txtRmks.MaxLength = 200
        Me.txtRmks.MendatroryField = False
        Me.txtRmks.MyLinkLable1 = Me.RadLabel8
        Me.txtRmks.MyLinkLable2 = Nothing
        Me.txtRmks.Name = "txtRmks"
        Me.txtRmks.ReferenceFieldDesc = Nothing
        Me.txtRmks.ReferenceFieldName = Nothing
        Me.txtRmks.ReferenceTableName = Nothing
        Me.txtRmks.Size = New System.Drawing.Size(346, 18)
        Me.txtRmks.TabIndex = 10
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(7, 139)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel8.TabIndex = 17
        Me.RadLabel8.Text = "Remarks"
        '
        'txtRefNo
        '
        Me.txtRefNo.CalculationExpression = Nothing
        Me.txtRefNo.FieldCode = Nothing
        Me.txtRefNo.FieldDesc = Nothing
        Me.txtRefNo.FieldMaxLength = 0
        Me.txtRefNo.FieldName = Nothing
        Me.txtRefNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.isCalculatedField = False
        Me.txtRefNo.IsSourceFromTable = False
        Me.txtRefNo.IsSourceFromValueList = False
        Me.txtRefNo.IsUnique = False
        Me.txtRefNo.Location = New System.Drawing.Point(549, 119)
        Me.txtRefNo.MaxLength = 50
        Me.txtRefNo.MendatroryField = False
        Me.txtRefNo.MyLinkLable1 = Me.RadLabel3
        Me.txtRefNo.MyLinkLable2 = Nothing
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.ReferenceFieldDesc = Nothing
        Me.txtRefNo.ReferenceFieldName = Nothing
        Me.txtRefNo.ReferenceTableName = Nothing
        Me.txtRefNo.Size = New System.Drawing.Size(315, 18)
        Me.txtRefNo.TabIndex = 9
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(465, 120)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(76, 16)
        Me.RadLabel3.TabIndex = 23
        Me.RadLabel3.Text = "Reference No"
        '
        'txtComment
        '
        Me.txtComment.CalculationExpression = Nothing
        Me.txtComment.FieldCode = Nothing
        Me.txtComment.FieldDesc = Nothing
        Me.txtComment.FieldMaxLength = 0
        Me.txtComment.FieldName = Nothing
        Me.txtComment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComment.isCalculatedField = False
        Me.txtComment.IsSourceFromTable = False
        Me.txtComment.IsSourceFromValueList = False
        Me.txtComment.IsUnique = False
        Me.txtComment.Location = New System.Drawing.Point(549, 138)
        Me.txtComment.MaxLength = 200
        Me.txtComment.MendatroryField = False
        Me.txtComment.MyLinkLable1 = Me.RadLabel2
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        Me.txtComment.Size = New System.Drawing.Size(315, 18)
        Me.txtComment.TabIndex = 11
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(118, 29)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblQuotationOrder
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(241, 20)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'btnAmendment
        '
        Me.btnAmendment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAmendment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAmendment.Location = New System.Drawing.Point(408, 5)
        Me.btnAmendment.Name = "btnAmendment"
        Me.btnAmendment.Size = New System.Drawing.Size(69, 22)
        Me.btnAmendment.TabIndex = 8
        Me.btnAmendment.Text = "Amendment"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(79, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(154, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(810, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.RadMenu1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(884, 25)
        Me.Panel2.TabIndex = 6
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Setting})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(884, 20)
        Me.RadMenu1.TabIndex = 0
        '
        'Setting
        '
        Me.Setting.AccessibleDescription = "Layout"
        Me.Setting.AccessibleName = "Layout"
        Me.Setting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.saveLayoutbtn, Me.DeleteLayout, Me.btnimport, Me.btnexport})
        Me.Setting.Name = "Setting"
        Me.Setting.Text = "Setting"
        '
        'saveLayoutbtn
        '
        Me.saveLayoutbtn.Name = "saveLayoutbtn"
        Me.saveLayoutbtn.Text = "Save Layout"
        '
        'DeleteLayout
        '
        Me.DeleteLayout.Name = "DeleteLayout"
        Me.DeleteLayout.Text = "Delete Layout"
        '
        'btnimport
        '
        Me.btnimport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnimporthead, Me.btnimportdetail})
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Text = "Import"
        Me.btnimport.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        '
        'btnimporthead
        '
        Me.btnimporthead.Name = "btnimporthead"
        Me.btnimporthead.Text = "Import Head"
        '
        'btnimportdetail
        '
        Me.btnimportdetail.Name = "btnimportdetail"
        Me.btnimportdetail.Text = "Import Detail"
        '
        'btnexport
        '
        Me.btnexport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexporthead, Me.btnexportdetail})
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Text = "Export"
        Me.btnexport.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        '
        'btnexporthead
        '
        Me.btnexporthead.Name = "btnexporthead"
        Me.btnexporthead.Text = "Export Head"
        '
        'btnexportdetail
        '
        Me.btnexportdetail.Name = "btnexportdetail"
        Me.btnexportdetail.Text = "Export Detail"
        '
        'frmMaterialQuotationOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 452)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmMaterialQuotationOrder"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Material Quotation Order"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.chkTaxable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtlocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQuotation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcustdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQuotationOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRmks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAmendment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtRmks As common.Controls.MyTextBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtRefNo As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents lblQuotationOrder As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel27 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents chkOnHold As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents Setting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents saveLayoutbtn As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents DeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnimporthead As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnimportdetail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnexporthead As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnexportdetail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnAmendment As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCustomer As common.UserControls.txtFinder
    Friend WithEvents txtcustdesc As common.Controls.MyTextBox
    Friend WithEvents txtMaterialQuotation As common.UserControls.txtFinder
    Friend WithEvents lblQuotation As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents txtlocation As common.Controls.MyTextBox
    Friend WithEvents chkTaxable As common.Controls.MyCheckBox

End Class

