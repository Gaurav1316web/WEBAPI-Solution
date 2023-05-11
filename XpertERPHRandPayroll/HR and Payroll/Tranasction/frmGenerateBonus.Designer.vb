Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGenerateBonus
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGenerateBonus))
        Me.UsLock1 = New common.usLock()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.lblPayablePayPeriodName = New common.Controls.MyLabel()
        Me.lblFromPayPeriodName = New common.Controls.MyLabel()
        Me.lblToPayPeriodName = New common.Controls.MyLabel()
        Me.txtToPayPeriodCode = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtPayablePayPeriodCode = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtFromPayPeriodCode = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.btnGenerate = New Telerik.WinControls.UI.RadButton()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblcardno = New common.Controls.MyLabel()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblDivision = New common.Controls.MyLabel()
        Me.fndDivision = New common.UserControls.txtFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.PageFinalBonus = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.pageBonusSummary = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvBonusSummary = New common.UserControls.MyRadGridView()
        Me.pageBonusDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvBonusDetail = New common.UserControls.MyRadGridView()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayablePayPeriodName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromPayPeriodName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToPayPeriodName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGenerate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcardno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.PageFinalBonus.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageBonusSummary.SuspendLayout()
        CType(Me.gvBonusSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvBonusSummary.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageBonusDetail.SuspendLayout()
        CType(Me.gvBonusDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvBonusDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(449, 16)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 189
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(10, 18)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel3.TabIndex = 179
        Me.MyLabel3.Text = "Code"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(358, 16)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'lblPayablePayPeriodName
        '
        Me.lblPayablePayPeriodName.AutoSize = False
        Me.lblPayablePayPeriodName.BorderVisible = True
        Me.lblPayablePayPeriodName.FieldName = Nothing
        Me.lblPayablePayPeriodName.Location = New System.Drawing.Point(358, 131)
        Me.lblPayablePayPeriodName.Name = "lblPayablePayPeriodName"
        Me.lblPayablePayPeriodName.Size = New System.Drawing.Size(189, 18)
        Me.lblPayablePayPeriodName.TabIndex = 7
        Me.lblPayablePayPeriodName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFromPayPeriodName
        '
        Me.lblFromPayPeriodName.AutoSize = False
        Me.lblFromPayPeriodName.BorderVisible = True
        Me.lblFromPayPeriodName.FieldName = Nothing
        Me.lblFromPayPeriodName.Location = New System.Drawing.Point(358, 85)
        Me.lblFromPayPeriodName.Name = "lblFromPayPeriodName"
        Me.lblFromPayPeriodName.Size = New System.Drawing.Size(189, 18)
        Me.lblFromPayPeriodName.TabIndex = 3
        Me.lblFromPayPeriodName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblToPayPeriodName
        '
        Me.lblToPayPeriodName.AutoSize = False
        Me.lblToPayPeriodName.BorderVisible = True
        Me.lblToPayPeriodName.FieldName = Nothing
        Me.lblToPayPeriodName.Location = New System.Drawing.Point(358, 108)
        Me.lblToPayPeriodName.Name = "lblToPayPeriodName"
        Me.lblToPayPeriodName.Size = New System.Drawing.Size(189, 18)
        Me.lblToPayPeriodName.TabIndex = 5
        Me.lblToPayPeriodName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtToPayPeriodCode
        '
        Me.txtToPayPeriodCode.CalculationExpression = Nothing
        Me.txtToPayPeriodCode.FieldCode = Nothing
        Me.txtToPayPeriodCode.FieldDesc = Nothing
        Me.txtToPayPeriodCode.FieldMaxLength = 0
        Me.txtToPayPeriodCode.FieldName = Nothing
        Me.txtToPayPeriodCode.isCalculatedField = False
        Me.txtToPayPeriodCode.IsSourceFromTable = False
        Me.txtToPayPeriodCode.IsSourceFromValueList = False
        Me.txtToPayPeriodCode.IsUnique = False
        Me.txtToPayPeriodCode.Location = New System.Drawing.Point(132, 108)
        Me.txtToPayPeriodCode.MendatroryField = True
        Me.txtToPayPeriodCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToPayPeriodCode.MyLinkLable1 = Me.MyLabel1
        Me.txtToPayPeriodCode.MyLinkLable2 = Me.lblToPayPeriodName
        Me.txtToPayPeriodCode.MyReadOnly = False
        Me.txtToPayPeriodCode.MyShowMasterFormButton = False
        Me.txtToPayPeriodCode.Name = "txtToPayPeriodCode"
        Me.txtToPayPeriodCode.ReferenceFieldDesc = Nothing
        Me.txtToPayPeriodCode.ReferenceFieldName = Nothing
        Me.txtToPayPeriodCode.ReferenceTableName = Nothing
        Me.txtToPayPeriodCode.Size = New System.Drawing.Size(221, 19)
        Me.txtToPayPeriodCode.TabIndex = 4
        Me.txtToPayPeriodCode.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(10, 109)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(78, 16)
        Me.MyLabel1.TabIndex = 157
        Me.MyLabel1.Text = "To Pay Period"
        '
        'txtPayablePayPeriodCode
        '
        Me.txtPayablePayPeriodCode.CalculationExpression = Nothing
        Me.txtPayablePayPeriodCode.FieldCode = Nothing
        Me.txtPayablePayPeriodCode.FieldDesc = Nothing
        Me.txtPayablePayPeriodCode.FieldMaxLength = 0
        Me.txtPayablePayPeriodCode.FieldName = Nothing
        Me.txtPayablePayPeriodCode.isCalculatedField = False
        Me.txtPayablePayPeriodCode.IsSourceFromTable = False
        Me.txtPayablePayPeriodCode.IsSourceFromValueList = False
        Me.txtPayablePayPeriodCode.IsUnique = False
        Me.txtPayablePayPeriodCode.Location = New System.Drawing.Point(132, 131)
        Me.txtPayablePayPeriodCode.MendatroryField = True
        Me.txtPayablePayPeriodCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayablePayPeriodCode.MyLinkLable1 = Me.MyLabel2
        Me.txtPayablePayPeriodCode.MyLinkLable2 = Me.lblPayablePayPeriodName
        Me.txtPayablePayPeriodCode.MyReadOnly = False
        Me.txtPayablePayPeriodCode.MyShowMasterFormButton = False
        Me.txtPayablePayPeriodCode.Name = "txtPayablePayPeriodCode"
        Me.txtPayablePayPeriodCode.ReferenceFieldDesc = Nothing
        Me.txtPayablePayPeriodCode.ReferenceFieldName = Nothing
        Me.txtPayablePayPeriodCode.ReferenceTableName = Nothing
        Me.txtPayablePayPeriodCode.Size = New System.Drawing.Size(221, 19)
        Me.txtPayablePayPeriodCode.TabIndex = 6
        Me.txtPayablePayPeriodCode.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(10, 132)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(106, 16)
        Me.MyLabel2.TabIndex = 159
        Me.MyLabel2.Text = "Payable Pay Period"
        '
        'txtFromPayPeriodCode
        '
        Me.txtFromPayPeriodCode.CalculationExpression = Nothing
        Me.txtFromPayPeriodCode.FieldCode = Nothing
        Me.txtFromPayPeriodCode.FieldDesc = Nothing
        Me.txtFromPayPeriodCode.FieldMaxLength = 0
        Me.txtFromPayPeriodCode.FieldName = Nothing
        Me.txtFromPayPeriodCode.isCalculatedField = False
        Me.txtFromPayPeriodCode.IsSourceFromTable = False
        Me.txtFromPayPeriodCode.IsSourceFromValueList = False
        Me.txtFromPayPeriodCode.IsUnique = False
        Me.txtFromPayPeriodCode.Location = New System.Drawing.Point(132, 85)
        Me.txtFromPayPeriodCode.MendatroryField = True
        Me.txtFromPayPeriodCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromPayPeriodCode.MyLinkLable1 = Me.MyLabel4
        Me.txtFromPayPeriodCode.MyLinkLable2 = Me.lblFromPayPeriodName
        Me.txtFromPayPeriodCode.MyReadOnly = False
        Me.txtFromPayPeriodCode.MyShowMasterFormButton = False
        Me.txtFromPayPeriodCode.Name = "txtFromPayPeriodCode"
        Me.txtFromPayPeriodCode.ReferenceFieldDesc = Nothing
        Me.txtFromPayPeriodCode.ReferenceFieldName = Nothing
        Me.txtFromPayPeriodCode.ReferenceTableName = Nothing
        Me.txtFromPayPeriodCode.Size = New System.Drawing.Size(221, 19)
        Me.txtFromPayPeriodCode.TabIndex = 2
        Me.txtFromPayPeriodCode.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(10, 86)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel4.TabIndex = 153
        Me.MyLabel4.Text = "From Pay Period"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(132, 16)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.MyLabel3
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(221, 19)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'btnGenerate
        '
        Me.btnGenerate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerate.Location = New System.Drawing.Point(5, 12)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(96, 18)
        Me.btnGenerate.TabIndex = 9
        Me.btnGenerate.Text = "Generate"
        '
        'txtDescription
        '
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(132, 154)
        Me.txtDescription.MaxLength = 49
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.lblcardno
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(415, 18)
        Me.txtDescription.TabIndex = 8
        '
        'lblcardno
        '
        Me.lblcardno.FieldName = Nothing
        Me.lblcardno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcardno.Location = New System.Drawing.Point(10, 155)
        Me.lblcardno.Name = "lblcardno"
        Me.lblcardno.Size = New System.Drawing.Size(63, 16)
        Me.lblcardno.TabIndex = 155
        Me.lblcardno.Text = "Description"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(176, 12)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(107, 12)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(702, 15)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(245, 12)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(955, 20)
        Me.RadMenu1.TabIndex = 13
        Me.RadMenu1.Text = "RadMenu1"
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
        Me.RadMenuItem1.AccessibleDescription = "Export Summary"
        Me.RadMenuItem1.AccessibleName = "Export Summary"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Export Summary"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Export Detail"
        Me.RadMenuItem2.AccessibleName = "Export Detail"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export Detail"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnGenerate)
        Me.SplitContainer2.Size = New System.Drawing.Size(955, 510)
        Me.SplitContainer2.SplitterDistance = 470
        Me.SplitContainer2.TabIndex = 14
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblDivision)
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndDivision)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndLocation)
        Me.SplitContainer3.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblcardno)
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblPayablePayPeriodName)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblFromPayPeriodName)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblToPayPeriodName)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtToPayPeriodCode)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtPayablePayPeriodCode)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtFromPayPeriodCode)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer3.Size = New System.Drawing.Size(955, 470)
        Me.SplitContainer3.SplitterDistance = 178
        Me.SplitContainer3.TabIndex = 0
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(10, 63)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel7.TabIndex = 195
        Me.MyLabel7.Text = "Division"
        '
        'lblDivision
        '
        Me.lblDivision.AutoSize = False
        Me.lblDivision.BorderVisible = True
        Me.lblDivision.FieldName = Nothing
        Me.lblDivision.Location = New System.Drawing.Point(358, 62)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.Size = New System.Drawing.Size(189, 18)
        Me.lblDivision.TabIndex = 194
        Me.lblDivision.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fndDivision
        '
        Me.fndDivision.CalculationExpression = Nothing
        Me.fndDivision.FieldCode = Nothing
        Me.fndDivision.FieldDesc = Nothing
        Me.fndDivision.FieldMaxLength = 0
        Me.fndDivision.FieldName = Nothing
        Me.fndDivision.isCalculatedField = False
        Me.fndDivision.IsSourceFromTable = False
        Me.fndDivision.IsSourceFromValueList = False
        Me.fndDivision.IsUnique = False
        Me.fndDivision.Location = New System.Drawing.Point(132, 62)
        Me.fndDivision.MendatroryField = False
        Me.fndDivision.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDivision.MyLinkLable1 = Me.MyLabel7
        Me.fndDivision.MyLinkLable2 = Me.lblDivision
        Me.fndDivision.MyReadOnly = False
        Me.fndDivision.MyShowMasterFormButton = False
        Me.fndDivision.Name = "fndDivision"
        Me.fndDivision.ReferenceFieldDesc = Nothing
        Me.fndDivision.ReferenceFieldName = Nothing
        Me.fndDivision.ReferenceTableName = Nothing
        Me.fndDivision.Size = New System.Drawing.Size(221, 19)
        Me.fndDivision.TabIndex = 193
        Me.fndDivision.Value = ""
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(10, 41)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel5.TabIndex = 192
        Me.MyLabel5.Text = "Location"
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(358, 40)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(189, 18)
        Me.lblLocation.TabIndex = 191
        Me.lblLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fndLocation
        '
        Me.fndLocation.CalculationExpression = Nothing
        Me.fndLocation.FieldCode = Nothing
        Me.fndLocation.FieldDesc = Nothing
        Me.fndLocation.FieldMaxLength = 0
        Me.fndLocation.FieldName = Nothing
        Me.fndLocation.isCalculatedField = False
        Me.fndLocation.IsSourceFromTable = False
        Me.fndLocation.IsSourceFromValueList = False
        Me.fndLocation.IsUnique = False
        Me.fndLocation.Location = New System.Drawing.Point(132, 40)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.MyLabel5
        Me.fndLocation.MyLinkLable2 = Me.lblLocation
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(221, 19)
        Me.fndLocation.TabIndex = 190
        Me.fndLocation.Value = ""
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.PageFinalBonus)
        Me.RadPageView1.Controls.Add(Me.pageBonusSummary)
        Me.RadPageView1.Controls.Add(Me.pageBonusDetail)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.PageFinalBonus
        Me.RadPageView1.Size = New System.Drawing.Size(955, 288)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Final Bonus"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Final Bonus"
        '
        'PageFinalBonus
        '
        Me.PageFinalBonus.Controls.Add(Me.gv1)
        Me.PageFinalBonus.ItemSize = New System.Drawing.SizeF(76.0!, 26.0!)
        Me.PageFinalBonus.Location = New System.Drawing.Point(10, 35)
        Me.PageFinalBonus.Name = "PageFinalBonus"
        Me.PageFinalBonus.Size = New System.Drawing.Size(934, 242)
        Me.PageFinalBonus.Text = "Final Bonus"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(934, 242)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'pageBonusSummary
        '
        Me.pageBonusSummary.Controls.Add(Me.gvBonusSummary)
        Me.pageBonusSummary.ItemSize = New System.Drawing.SizeF(100.0!, 26.0!)
        Me.pageBonusSummary.Location = New System.Drawing.Point(10, 35)
        Me.pageBonusSummary.Name = "pageBonusSummary"
        Me.pageBonusSummary.Size = New System.Drawing.Size(934, 242)
        Me.pageBonusSummary.Text = "Bonus Summary"
        '
        'gvBonusSummary
        '
        Me.gvBonusSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvBonusSummary.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvBonusSummary.MasterTemplate.AllowAddNewRow = False
        Me.gvBonusSummary.MasterTemplate.EnableFiltering = True
        Me.gvBonusSummary.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvBonusSummary.Name = "gvBonusSummary"
        Me.gvBonusSummary.ShowHeaderCellButtons = True
        Me.gvBonusSummary.Size = New System.Drawing.Size(934, 242)
        Me.gvBonusSummary.TabIndex = 1
        Me.gvBonusSummary.TabStop = False
        Me.gvBonusSummary.Text = "RadGridView1"
        '
        'pageBonusDetail
        '
        Me.pageBonusDetail.Controls.Add(Me.gvBonusDetail)
        Me.pageBonusDetail.ItemSize = New System.Drawing.SizeF(80.0!, 26.0!)
        Me.pageBonusDetail.Location = New System.Drawing.Point(10, 35)
        Me.pageBonusDetail.Name = "pageBonusDetail"
        Me.pageBonusDetail.Size = New System.Drawing.Size(934, 242)
        Me.pageBonusDetail.Text = "Bonus Detail"
        '
        'gvBonusDetail
        '
        Me.gvBonusDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvBonusDetail.Location = New System.Drawing.Point(0, 0)
        '
        'gvBonusDetail
        '
        Me.gvBonusDetail.MasterTemplate.AllowAddNewRow = False
        Me.gvBonusDetail.MasterTemplate.EnableFiltering = True
        Me.gvBonusDetail.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvBonusDetail.Name = "gvBonusDetail"
        Me.gvBonusDetail.ShowHeaderCellButtons = True
        Me.gvBonusDetail.Size = New System.Drawing.Size(934, 242)
        Me.gvBonusDetail.TabIndex = 1
        Me.gvBonusDetail.TabStop = False
        Me.gvBonusDetail.Text = "RadGridView1"
        '
        'frmGenerateBonus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(955, 530)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmGenerateBonus"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Generate Bonus"
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayablePayPeriodName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromPayPeriodName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToPayPeriodName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGenerate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcardno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.PageFinalBonus.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageBonusSummary.ResumeLayout(False)
        CType(Me.gvBonusSummary.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvBonusSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageBonusDetail.ResumeLayout(False)
        CType(Me.gvBonusDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvBonusDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblcardno As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents btnGenerate As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblPayablePayPeriodName As common.Controls.MyLabel
    Friend WithEvents lblFromPayPeriodName As common.Controls.MyLabel
    Friend WithEvents lblToPayPeriodName As common.Controls.MyLabel
    Friend WithEvents txtToPayPeriodCode As common.UserControls.txtFinder
    Friend WithEvents txtPayablePayPeriodCode As common.UserControls.txtFinder
    Friend WithEvents txtFromPayPeriodCode As common.UserControls.txtFinder
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents PageFinalBonus As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents pageBonusSummary As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvBonusSummary As common.UserControls.MyRadGridView
    Friend WithEvents pageBonusDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvBonusDetail As common.UserControls.MyRadGridView
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents lblDivision As common.Controls.MyLabel
    Friend WithEvents fndDivision As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents fndLocation As common.UserControls.txtFinder
End Class
