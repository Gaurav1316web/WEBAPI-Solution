Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmOutputEntry
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtQtyLTR = New common.MyNumBox()
        Me.txtFatKG = New common.MyNumBox()
        Me.txtSnfKG = New common.MyNumBox()
        Me.txtFatPer = New common.MyNumBox()
        Me.txtSNFPer = New common.MyNumBox()
        Me.txtQtyKG = New common.MyNumBox()
        Me.cboOutPutType = New common.Controls.MyComboBox()
        Me.lblOutputType = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.txtMccName = New common.Controls.MyTextBox()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.lblTankerNo = New common.Controls.MyLabel()
        Me.txtMCC = New common.UserControls.txtFinder()
        Me.txtLocName = New common.Controls.MyTextBox()
        Me.fndLoc = New common.UserControls.txtFinder()
        Me.lblPlant = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.UsLock1 = New common.usLock()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQtyLTR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFatKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSnfKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFatPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSNFPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQtyKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboOutPutType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOutputType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMccName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPlant, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(991, 509)
        Me.SplitContainer1.SplitterDistance = 479
        Me.SplitContainer1.TabIndex = 1
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(991, 20)
        Me.RadMenu1.TabIndex = 2
        Me.RadMenu1.Text = "RadMenu1"
        Me.RadMenu1.Visible = False
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3, Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Export"
        Me.RadMenuItem2.AccessibleName = "Export"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Import"
        Me.RadMenuItem3.AccessibleName = "Import"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Import"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.AccessibleDescription = "Save Layout"
        Me.rmSaveLayout.AccessibleName = "Save Layout"
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.rmDeleteLayout.AccessibleName = "Delete Layout"
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'RadPageView1
        '
        Me.RadPageView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(5, 26)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(991, 454)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtQtyLTR)
        Me.RadPageViewPage1.Controls.Add(Me.txtFatKG)
        Me.RadPageViewPage1.Controls.Add(Me.txtSnfKG)
        Me.RadPageViewPage1.Controls.Add(Me.txtFatPer)
        Me.RadPageViewPage1.Controls.Add(Me.txtSNFPer)
        Me.RadPageViewPage1.Controls.Add(Me.txtQtyKG)
        Me.RadPageViewPage1.Controls.Add(Me.cboOutPutType)
        Me.RadPageViewPage1.Controls.Add(Me.lblOutputType)
        Me.RadPageViewPage1.Controls.Add(Me.txtToDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblToDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtMccName)
        Me.RadPageViewPage1.Controls.Add(Me.lblfromDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblTankerNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtMCC)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocName)
        Me.RadPageViewPage1.Controls.Add(Me.fndLoc)
        Me.RadPageViewPage1.Controls.Add(Me.lblPlant)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(80.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(970, 408)
        Me.RadPageViewPage1.Text = "Output Entry"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(4, 251)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel6.TabIndex = 424
        Me.MyLabel6.Text = "Snf (%)"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(4, 226)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel5.TabIndex = 423
        Me.MyLabel5.Text = "Fat (%)"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(4, 201)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel4.TabIndex = 422
        Me.MyLabel4.Text = "Snf (Kg)"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(3, 174)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(47, 16)
        Me.MyLabel3.TabIndex = 421
        Me.MyLabel3.Text = "Fat (Kg)"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(3, 148)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel2.TabIndex = 420
        Me.MyLabel2.Text = "Quantity (Ltr)"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(4, 122)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(73, 16)
        Me.MyLabel1.TabIndex = 419
        Me.MyLabel1.Text = "Quantity (Kg)"
        '
        'txtQtyLTR
        '
        Me.txtQtyLTR.BackColor = System.Drawing.Color.White
        Me.txtQtyLTR.CalculationExpression = Nothing
        Me.txtQtyLTR.DecimalPlaces = 2
        Me.txtQtyLTR.FieldCode = Nothing
        Me.txtQtyLTR.FieldDesc = Nothing
        Me.txtQtyLTR.FieldMaxLength = 100
        Me.txtQtyLTR.FieldName = Nothing
        Me.txtQtyLTR.isCalculatedField = False
        Me.txtQtyLTR.IsSourceFromTable = False
        Me.txtQtyLTR.IsSourceFromValueList = False
        Me.txtQtyLTR.IsUnique = False
        Me.txtQtyLTR.Location = New System.Drawing.Point(102, 145)
        Me.txtQtyLTR.MendatroryField = False
        Me.txtQtyLTR.MyLinkLable1 = Nothing
        Me.txtQtyLTR.MyLinkLable2 = Nothing
        Me.txtQtyLTR.Name = "txtQtyLTR"
        Me.txtQtyLTR.ReferenceFieldDesc = Nothing
        Me.txtQtyLTR.ReferenceFieldName = Nothing
        Me.txtQtyLTR.ReferenceTableName = Nothing
        Me.txtQtyLTR.Size = New System.Drawing.Size(107, 20)
        Me.txtQtyLTR.TabIndex = 418
        Me.txtQtyLTR.Text = "0"
        Me.txtQtyLTR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtQtyLTR.Value = 0R
        '
        'txtFatKG
        '
        Me.txtFatKG.BackColor = System.Drawing.Color.White
        Me.txtFatKG.CalculationExpression = Nothing
        Me.txtFatKG.DecimalPlaces = 3
        Me.txtFatKG.FieldCode = Nothing
        Me.txtFatKG.FieldDesc = Nothing
        Me.txtFatKG.FieldMaxLength = 100
        Me.txtFatKG.FieldName = Nothing
        Me.txtFatKG.isCalculatedField = False
        Me.txtFatKG.IsSourceFromTable = False
        Me.txtFatKG.IsSourceFromValueList = False
        Me.txtFatKG.IsUnique = False
        Me.txtFatKG.Location = New System.Drawing.Point(102, 171)
        Me.txtFatKG.MendatroryField = False
        Me.txtFatKG.MyLinkLable1 = Nothing
        Me.txtFatKG.MyLinkLable2 = Nothing
        Me.txtFatKG.Name = "txtFatKG"
        Me.txtFatKG.ReferenceFieldDesc = Nothing
        Me.txtFatKG.ReferenceFieldName = Nothing
        Me.txtFatKG.ReferenceTableName = Nothing
        Me.txtFatKG.Size = New System.Drawing.Size(107, 20)
        Me.txtFatKG.TabIndex = 417
        Me.txtFatKG.Text = "0"
        Me.txtFatKG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFatKG.Value = 0R
        '
        'txtSnfKG
        '
        Me.txtSnfKG.BackColor = System.Drawing.Color.White
        Me.txtSnfKG.CalculationExpression = Nothing
        Me.txtSnfKG.DecimalPlaces = 3
        Me.txtSnfKG.FieldCode = Nothing
        Me.txtSnfKG.FieldDesc = Nothing
        Me.txtSnfKG.FieldMaxLength = 100
        Me.txtSnfKG.FieldName = Nothing
        Me.txtSnfKG.isCalculatedField = False
        Me.txtSnfKG.IsSourceFromTable = False
        Me.txtSnfKG.IsSourceFromValueList = False
        Me.txtSnfKG.IsUnique = False
        Me.txtSnfKG.Location = New System.Drawing.Point(102, 198)
        Me.txtSnfKG.MendatroryField = False
        Me.txtSnfKG.MyLinkLable1 = Nothing
        Me.txtSnfKG.MyLinkLable2 = Nothing
        Me.txtSnfKG.Name = "txtSnfKG"
        Me.txtSnfKG.ReferenceFieldDesc = Nothing
        Me.txtSnfKG.ReferenceFieldName = Nothing
        Me.txtSnfKG.ReferenceTableName = Nothing
        Me.txtSnfKG.Size = New System.Drawing.Size(107, 20)
        Me.txtSnfKG.TabIndex = 416
        Me.txtSnfKG.Text = "0"
        Me.txtSnfKG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSnfKG.Value = 0R
        '
        'txtFatPer
        '
        Me.txtFatPer.BackColor = System.Drawing.Color.White
        Me.txtFatPer.CalculationExpression = Nothing
        Me.txtFatPer.DecimalPlaces = 2
        Me.txtFatPer.Enabled = False
        Me.txtFatPer.FieldCode = Nothing
        Me.txtFatPer.FieldDesc = Nothing
        Me.txtFatPer.FieldMaxLength = 100
        Me.txtFatPer.FieldName = Nothing
        Me.txtFatPer.isCalculatedField = False
        Me.txtFatPer.IsSourceFromTable = False
        Me.txtFatPer.IsSourceFromValueList = False
        Me.txtFatPer.IsUnique = False
        Me.txtFatPer.Location = New System.Drawing.Point(102, 223)
        Me.txtFatPer.MendatroryField = False
        Me.txtFatPer.MyLinkLable1 = Nothing
        Me.txtFatPer.MyLinkLable2 = Nothing
        Me.txtFatPer.Name = "txtFatPer"
        Me.txtFatPer.ReferenceFieldDesc = Nothing
        Me.txtFatPer.ReferenceFieldName = Nothing
        Me.txtFatPer.ReferenceTableName = Nothing
        Me.txtFatPer.Size = New System.Drawing.Size(107, 20)
        Me.txtFatPer.TabIndex = 415
        Me.txtFatPer.Text = "0"
        Me.txtFatPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFatPer.Value = 0R
        '
        'txtSNFPer
        '
        Me.txtSNFPer.BackColor = System.Drawing.Color.White
        Me.txtSNFPer.CalculationExpression = Nothing
        Me.txtSNFPer.DecimalPlaces = 2
        Me.txtSNFPer.Enabled = False
        Me.txtSNFPer.FieldCode = Nothing
        Me.txtSNFPer.FieldDesc = Nothing
        Me.txtSNFPer.FieldMaxLength = 100
        Me.txtSNFPer.FieldName = Nothing
        Me.txtSNFPer.isCalculatedField = False
        Me.txtSNFPer.IsSourceFromTable = False
        Me.txtSNFPer.IsSourceFromValueList = False
        Me.txtSNFPer.IsUnique = False
        Me.txtSNFPer.Location = New System.Drawing.Point(102, 248)
        Me.txtSNFPer.MendatroryField = False
        Me.txtSNFPer.MyLinkLable1 = Nothing
        Me.txtSNFPer.MyLinkLable2 = Nothing
        Me.txtSNFPer.Name = "txtSNFPer"
        Me.txtSNFPer.ReferenceFieldDesc = Nothing
        Me.txtSNFPer.ReferenceFieldName = Nothing
        Me.txtSNFPer.ReferenceTableName = Nothing
        Me.txtSNFPer.Size = New System.Drawing.Size(107, 20)
        Me.txtSNFPer.TabIndex = 414
        Me.txtSNFPer.Text = "0"
        Me.txtSNFPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSNFPer.Value = 0R
        '
        'txtQtyKG
        '
        Me.txtQtyKG.BackColor = System.Drawing.Color.White
        Me.txtQtyKG.CalculationExpression = Nothing
        Me.txtQtyKG.DecimalPlaces = 2
        Me.txtQtyKG.FieldCode = Nothing
        Me.txtQtyKG.FieldDesc = Nothing
        Me.txtQtyKG.FieldMaxLength = 100
        Me.txtQtyKG.FieldName = Nothing
        Me.txtQtyKG.isCalculatedField = False
        Me.txtQtyKG.IsSourceFromTable = False
        Me.txtQtyKG.IsSourceFromValueList = False
        Me.txtQtyKG.IsUnique = False
        Me.txtQtyKG.Location = New System.Drawing.Point(102, 119)
        Me.txtQtyKG.MendatroryField = False
        Me.txtQtyKG.MyLinkLable1 = Nothing
        Me.txtQtyKG.MyLinkLable2 = Nothing
        Me.txtQtyKG.Name = "txtQtyKG"
        Me.txtQtyKG.ReferenceFieldDesc = Nothing
        Me.txtQtyKG.ReferenceFieldName = Nothing
        Me.txtQtyKG.ReferenceTableName = Nothing
        Me.txtQtyKG.Size = New System.Drawing.Size(107, 20)
        Me.txtQtyKG.TabIndex = 413
        Me.txtQtyKG.Text = "0"
        Me.txtQtyKG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtQtyKG.Value = 0R
        '
        'cboOutPutType
        '
        Me.cboOutPutType.AutoCompleteDisplayMember = Nothing
        Me.cboOutPutType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOutPutType.AutoCompleteValueMember = Nothing
        Me.cboOutPutType.BackColor = System.Drawing.Color.Transparent
        Me.cboOutPutType.CalculationExpression = Nothing
        Me.cboOutPutType.FieldCode = Nothing
        Me.cboOutPutType.FieldDesc = Nothing
        Me.cboOutPutType.FieldMaxLength = 0
        Me.cboOutPutType.FieldName = Nothing
        Me.cboOutPutType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOutPutType.isCalculatedField = False
        Me.cboOutPutType.IsSourceFromTable = False
        Me.cboOutPutType.IsSourceFromValueList = False
        Me.cboOutPutType.IsUnique = False
        Me.cboOutPutType.Location = New System.Drawing.Point(102, 98)
        Me.cboOutPutType.MendatroryField = True
        Me.cboOutPutType.MyLinkLable1 = Nothing
        Me.cboOutPutType.MyLinkLable2 = Nothing
        Me.cboOutPutType.Name = "cboOutPutType"
        Me.cboOutPutType.ReferenceFieldDesc = Nothing
        Me.cboOutPutType.ReferenceFieldName = Nothing
        Me.cboOutPutType.ReferenceTableName = Nothing
        '
        '
        '
        Me.cboOutPutType.RootElement.StretchVertically = True
        Me.cboOutPutType.Size = New System.Drawing.Size(198, 18)
        Me.cboOutPutType.TabIndex = 412
        '
        'lblOutputType
        '
        Me.lblOutputType.FieldName = Nothing
        Me.lblOutputType.Location = New System.Drawing.Point(4, 98)
        Me.lblOutputType.Name = "lblOutputType"
        Me.lblOutputType.Size = New System.Drawing.Size(69, 18)
        Me.lblOutputType.TabIndex = 411
        Me.lblOutputType.Text = "Output Type"
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(241, 73)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate.TabIndex = 410
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "17-12-2011"
        Me.txtToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(190, 74)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 409
        Me.lblToDate.Text = "To Date"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(102, 73)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 408
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "17-12-2011"
        Me.txtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'txtMccName
        '
        Me.txtMccName.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtMccName.CalculationExpression = Nothing
        Me.txtMccName.Enabled = False
        Me.txtMccName.FieldCode = Nothing
        Me.txtMccName.FieldDesc = Nothing
        Me.txtMccName.FieldMaxLength = 0
        Me.txtMccName.FieldName = Nothing
        Me.txtMccName.isCalculatedField = False
        Me.txtMccName.IsSourceFromTable = False
        Me.txtMccName.IsSourceFromValueList = False
        Me.txtMccName.IsUnique = False
        Me.txtMccName.Location = New System.Drawing.Point(310, 49)
        Me.txtMccName.MendatroryField = False
        Me.txtMccName.MyLinkLable1 = Nothing
        Me.txtMccName.MyLinkLable2 = Nothing
        Me.txtMccName.Name = "txtMccName"
        Me.txtMccName.ReferenceFieldDesc = Nothing
        Me.txtMccName.ReferenceFieldName = Nothing
        Me.txtMccName.ReferenceTableName = Nothing
        Me.txtMccName.Size = New System.Drawing.Size(188, 20)
        Me.txtMccName.TabIndex = 407
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.Location = New System.Drawing.Point(4, 74)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblfromDate.TabIndex = 406
        Me.lblfromDate.Text = "From Date"
        '
        'lblTankerNo
        '
        Me.lblTankerNo.FieldName = Nothing
        Me.lblTankerNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerNo.Location = New System.Drawing.Point(4, 52)
        Me.lblTankerNo.Name = "lblTankerNo"
        Me.lblTankerNo.Size = New System.Drawing.Size(61, 16)
        Me.lblTankerNo.TabIndex = 405
        Me.lblTankerNo.Text = "MCC/Plant"
        '
        'txtMCC
        '
        Me.txtMCC.CalculationExpression = Nothing
        Me.txtMCC.FieldCode = Nothing
        Me.txtMCC.FieldDesc = Nothing
        Me.txtMCC.FieldMaxLength = 0
        Me.txtMCC.FieldName = Nothing
        Me.txtMCC.isCalculatedField = False
        Me.txtMCC.IsSourceFromTable = False
        Me.txtMCC.IsSourceFromValueList = False
        Me.txtMCC.IsUnique = False
        Me.txtMCC.Location = New System.Drawing.Point(102, 48)
        Me.txtMCC.MendatroryField = True
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Nothing
        Me.txtMCC.MyLinkLable2 = Nothing
        Me.txtMCC.MyReadOnly = False
        Me.txtMCC.MyShowMasterFormButton = False
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.ReferenceFieldDesc = Nothing
        Me.txtMCC.ReferenceFieldName = Nothing
        Me.txtMCC.ReferenceTableName = Nothing
        Me.txtMCC.Size = New System.Drawing.Size(198, 20)
        Me.txtMCC.TabIndex = 404
        Me.txtMCC.Value = ""
        '
        'txtLocName
        '
        Me.txtLocName.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtLocName.CalculationExpression = Nothing
        Me.txtLocName.Enabled = False
        Me.txtLocName.FieldCode = Nothing
        Me.txtLocName.FieldDesc = Nothing
        Me.txtLocName.FieldMaxLength = 0
        Me.txtLocName.FieldName = Nothing
        Me.txtLocName.isCalculatedField = False
        Me.txtLocName.IsSourceFromTable = False
        Me.txtLocName.IsSourceFromValueList = False
        Me.txtLocName.IsUnique = False
        Me.txtLocName.Location = New System.Drawing.Point(310, 25)
        Me.txtLocName.MendatroryField = False
        Me.txtLocName.MyLinkLable1 = Nothing
        Me.txtLocName.MyLinkLable2 = Nothing
        Me.txtLocName.Name = "txtLocName"
        Me.txtLocName.ReferenceFieldDesc = Nothing
        Me.txtLocName.ReferenceFieldName = Nothing
        Me.txtLocName.ReferenceTableName = Nothing
        Me.txtLocName.Size = New System.Drawing.Size(188, 20)
        Me.txtLocName.TabIndex = 403
        '
        'fndLoc
        '
        Me.fndLoc.CalculationExpression = Nothing
        Me.fndLoc.FieldCode = Nothing
        Me.fndLoc.FieldDesc = Nothing
        Me.fndLoc.FieldMaxLength = 0
        Me.fndLoc.FieldName = Nothing
        Me.fndLoc.isCalculatedField = False
        Me.fndLoc.IsSourceFromTable = False
        Me.fndLoc.IsSourceFromValueList = False
        Me.fndLoc.IsUnique = False
        Me.fndLoc.Location = New System.Drawing.Point(102, 24)
        Me.fndLoc.MendatroryField = True
        Me.fndLoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLoc.MyLinkLable1 = Nothing
        Me.fndLoc.MyLinkLable2 = Nothing
        Me.fndLoc.MyReadOnly = False
        Me.fndLoc.MyShowMasterFormButton = False
        Me.fndLoc.Name = "fndLoc"
        Me.fndLoc.ReferenceFieldDesc = Nothing
        Me.fndLoc.ReferenceFieldName = Nothing
        Me.fndLoc.ReferenceTableName = Nothing
        Me.fndLoc.Size = New System.Drawing.Size(198, 20)
        Me.fndLoc.TabIndex = 402
        Me.fndLoc.Value = ""
        '
        'lblPlant
        '
        Me.lblPlant.FieldName = Nothing
        Me.lblPlant.Location = New System.Drawing.Point(4, 26)
        Me.lblPlant.Name = "lblPlant"
        Me.lblPlant.Size = New System.Drawing.Size(31, 18)
        Me.lblPlant.TabIndex = 42
        Me.lblPlant.Text = "Plant"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(388, 2)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 18
        Me.RadLabel4.Text = "Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(4, 2)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel1.TabIndex = 22
        Me.RadLabel1.Text = "Document No"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(360, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 18)
        Me.btnAddNew.TabIndex = 1
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(877, 0)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(88, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 11
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(102, 0)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(258, 20)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.Value = ""
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
        Me.txtDate.Location = New System.Drawing.Point(421, 1)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(77, 18)
        Me.txtDate.TabIndex = 2
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(655, 3)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(121, 22)
        Me.btnReverse.TabIndex = 25
        Me.btnReverse.Text = "Reverse and Unpost"
        Me.btnReverse.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(155, 2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(80, 2)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(914, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'frmOutputEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(991, 509)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmOutputEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Output Entry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQtyLTR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFatKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSnfKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFatPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSNFPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQtyKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboOutPutType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOutputType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMccName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPlant, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblPlant As common.Controls.MyLabel
    Friend WithEvents fndLoc As common.UserControls.txtFinder
    Friend WithEvents txtLocName As common.Controls.MyTextBox
    Friend WithEvents txtMCC As common.UserControls.txtFinder
    Friend WithEvents lblTankerNo As common.Controls.MyLabel
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents txtMccName As common.Controls.MyTextBox
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblOutputType As common.Controls.MyLabel
    Friend WithEvents cboOutPutType As common.Controls.MyComboBox
    Friend WithEvents txtQtyLTR As common.MyNumBox
    Friend WithEvents txtFatKG As common.MyNumBox
    Friend WithEvents txtSnfKG As common.MyNumBox
    Friend WithEvents txtFatPer As common.MyNumBox
    Friend WithEvents txtSNFPer As common.MyNumBox
    Friend WithEvents txtQtyKG As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
End Class

