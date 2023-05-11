Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdjustmentVoucher
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAdjustmentVoucher))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnUnSelect = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.FndLocationCode = New common.UserControls.txtFinder()
        Me.lblLocationCode = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.lblEmpCode = New common.Controls.MyLabel()
        Me.txtEmpCode = New common.UserControls.txtFinder()
        Me.lblAdjustmentByName = New common.Controls.MyLabel()
        Me.lblEmpName = New common.Controls.MyLabel()
        Me.lblPayPeriodName = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.findPayperiod = New common.UserControls.txtFinder()
        Me.lblPayPeriod = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblRemarks = New common.Controls.MyLabel()
        Me.lblAdjustBy = New common.Controls.MyLabel()
        Me.txtAdjustBy = New common.UserControls.txtFinder()
        Me.lblAdjustmentDate = New common.Controls.MyLabel()
        Me.dtpAdjustDate = New common.Controls.MyDateTimePicker()
        Me.txtcode = New common.UserControls.txtNavigator()
        Me.lblAdjustmentCode = New common.Controls.MyLabel()
        Me.gvAdjustmentVoucher = New common.UserControls.MyRadGridView()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnUnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdjustmentByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdjustBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdjustmentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpAdjustDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdjustmentCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAdjustmentVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAdjustmentVoucher.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnUnSelect)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.FndLocationCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmpCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEmpCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAdjustmentByName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmpName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriodName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findPayperiod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAdjustBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAdjustBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAdjustmentDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpAdjustDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAdjustmentCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gvAdjustmentVoucher)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(757, 510)
        Me.SplitContainer1.SplitterDistance = 463
        Me.SplitContainer1.TabIndex = 0
        '
        'btnUnSelect
        '
        Me.btnUnSelect.Location = New System.Drawing.Point(361, 142)
        Me.btnUnSelect.Name = "btnUnSelect"
        Me.btnUnSelect.Size = New System.Drawing.Size(80, 23)
        Me.btnUnSelect.TabIndex = 201
        Me.btnUnSelect.Text = "Select All"
        '
        'btnGo
        '
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(361, 169)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(55, 18)
        Me.btnGo.TabIndex = 200
        Me.btnGo.Text = ">>"
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.Location = New System.Drawing.Point(352, 47)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(222, 19)
        Me.lblLocationName.TabIndex = 199
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FndLocationCode
        '
        Me.FndLocationCode.Location = New System.Drawing.Point(129, 47)
        Me.FndLocationCode.MendatroryField = True
        Me.FndLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndLocationCode.MyLinkLable1 = Nothing
        Me.FndLocationCode.MyLinkLable2 = Nothing
        Me.FndLocationCode.MyReadOnly = False
        Me.FndLocationCode.MyShowMasterFormButton = False
        Me.FndLocationCode.Name = "FndLocationCode"
        Me.FndLocationCode.Size = New System.Drawing.Size(221, 19)
        Me.FndLocationCode.TabIndex = 198
        Me.FndLocationCode.Value = ""
        '
        'lblLocationCode
        '
        Me.lblLocationCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationCode.Location = New System.Drawing.Point(10, 50)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(79, 16)
        Me.lblLocationCode.TabIndex = 155
        Me.lblLocationCode.Text = "Location Code"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(476, 24)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 197
        '
        'lblEmpCode
        '
        Me.lblEmpCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpCode.Location = New System.Drawing.Point(365, 151)
        Me.lblEmpCode.Name = "lblEmpCode"
        Me.lblEmpCode.Size = New System.Drawing.Size(87, 16)
        Me.lblEmpCode.TabIndex = 154
        Me.lblEmpCode.Text = "Employee Code"
        Me.lblEmpCode.Visible = False
        '
        'txtEmpCode
        '
        Me.txtEmpCode.Location = New System.Drawing.Point(485, 157)
        Me.txtEmpCode.MendatroryField = True
        Me.txtEmpCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.MyLinkLable1 = Me.lblEmpCode
        Me.txtEmpCode.MyLinkLable2 = Nothing
        Me.txtEmpCode.MyReadOnly = False
        Me.txtEmpCode.MyShowMasterFormButton = False
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.Size = New System.Drawing.Size(207, 19)
        Me.txtEmpCode.TabIndex = 4
        Me.txtEmpCode.Value = ""
        Me.txtEmpCode.Visible = False
        '
        'lblAdjustmentByName
        '
        Me.lblAdjustmentByName.AutoSize = False
        Me.lblAdjustmentByName.BorderVisible = True
        Me.lblAdjustmentByName.Location = New System.Drawing.Point(352, 117)
        Me.lblAdjustmentByName.Name = "lblAdjustmentByName"
        Me.lblAdjustmentByName.Size = New System.Drawing.Size(222, 19)
        Me.lblAdjustmentByName.TabIndex = 8
        Me.lblAdjustmentByName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.Location = New System.Drawing.Point(708, 157)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(26, 19)
        Me.lblEmpName.TabIndex = 5
        Me.lblEmpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblEmpName.Visible = False
        '
        'lblPayPeriodName
        '
        Me.lblPayPeriodName.AutoSize = False
        Me.lblPayPeriodName.BorderVisible = True
        Me.lblPayPeriodName.Location = New System.Drawing.Point(352, 67)
        Me.lblPayPeriodName.Name = "lblPayPeriodName"
        Me.lblPayPeriodName.Size = New System.Drawing.Size(222, 19)
        Me.lblPayPeriodName.TabIndex = 3
        Me.lblPayPeriodName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(352, 21)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'findPayperiod
        '
        Me.findPayperiod.Location = New System.Drawing.Point(129, 69)
        Me.findPayperiod.MendatroryField = True
        Me.findPayperiod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findPayperiod.MyLinkLable1 = Nothing
        Me.findPayperiod.MyLinkLable2 = Nothing
        Me.findPayperiod.MyReadOnly = False
        Me.findPayperiod.MyShowMasterFormButton = False
        Me.findPayperiod.Name = "findPayperiod"
        Me.findPayperiod.Size = New System.Drawing.Size(221, 19)
        Me.findPayperiod.TabIndex = 2
        Me.findPayperiod.Value = ""
        '
        'lblPayPeriod
        '
        Me.lblPayPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayPeriod.Location = New System.Drawing.Point(10, 72)
        Me.lblPayPeriod.Name = "lblPayPeriod"
        Me.lblPayPeriod.Size = New System.Drawing.Size(92, 16)
        Me.lblPayPeriod.TabIndex = 189
        Me.lblPayPeriod.Text = "Pay Period Code"
        '
        'txtDescription
        '
        Me.txtDescription.AutoSize = False
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(129, 142)
        Me.txtDescription.MaxLength = 49
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.lblRemarks
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(221, 47)
        Me.txtDescription.TabIndex = 9
        '
        'lblRemarks
        '
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(12, 159)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(51, 16)
        Me.lblRemarks.TabIndex = 177
        Me.lblRemarks.Text = "Remarks"
        '
        'lblAdjustBy
        '
        Me.lblAdjustBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdjustBy.Location = New System.Drawing.Point(10, 117)
        Me.lblAdjustBy.Name = "lblAdjustBy"
        Me.lblAdjustBy.Size = New System.Drawing.Size(79, 16)
        Me.lblAdjustBy.TabIndex = 165
        Me.lblAdjustBy.Text = "Adjustment By"
        '
        'txtAdjustBy
        '
        Me.txtAdjustBy.Location = New System.Drawing.Point(129, 117)
        Me.txtAdjustBy.MendatroryField = False
        Me.txtAdjustBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdjustBy.MyLinkLable1 = Me.lblAdjustBy
        Me.txtAdjustBy.MyLinkLable2 = Nothing
        Me.txtAdjustBy.MyReadOnly = False
        Me.txtAdjustBy.MyShowMasterFormButton = False
        Me.txtAdjustBy.Name = "txtAdjustBy"
        Me.txtAdjustBy.Size = New System.Drawing.Size(221, 19)
        Me.txtAdjustBy.TabIndex = 7
        Me.txtAdjustBy.Value = ""
        '
        'lblAdjustmentDate
        '
        Me.lblAdjustmentDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdjustmentDate.Location = New System.Drawing.Point(10, 93)
        Me.lblAdjustmentDate.Name = "lblAdjustmentDate"
        Me.lblAdjustmentDate.Size = New System.Drawing.Size(90, 16)
        Me.lblAdjustmentDate.TabIndex = 164
        Me.lblAdjustmentDate.Text = "Adjustment Date"
        '
        'dtpAdjustDate
        '
        Me.dtpAdjustDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpAdjustDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpAdjustDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAdjustDate.Location = New System.Drawing.Point(129, 94)
        Me.dtpAdjustDate.MendatroryField = True
        Me.dtpAdjustDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAdjustDate.MyLinkLable1 = Me.lblAdjustmentDate
        Me.dtpAdjustDate.MyLinkLable2 = Nothing
        Me.dtpAdjustDate.Name = "dtpAdjustDate"
        Me.dtpAdjustDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAdjustDate.Size = New System.Drawing.Size(130, 18)
        Me.dtpAdjustDate.TabIndex = 6
        Me.dtpAdjustDate.TabStop = False
        Me.dtpAdjustDate.Text = "04/07/2013"
        Me.dtpAdjustDate.Value = New Date(2013, 7, 4, 0, 0, 0, 0)
        '
        'txtcode
        '
        Me.txtcode.Location = New System.Drawing.Point(129, 21)
        Me.txtcode.MendatroryField = True
        Me.txtcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcode.MyLinkLable1 = Me.lblAdjustmentCode
        Me.txtcode.MyLinkLable2 = Nothing
        Me.txtcode.MyMaxLength = 12
        Me.txtcode.MyReadOnly = False
        Me.txtcode.Name = "txtcode"
        Me.txtcode.Size = New System.Drawing.Size(221, 21)
        Me.txtcode.TabIndex = 0
        Me.txtcode.Value = ""
        '
        'lblAdjustmentCode
        '
        Me.lblAdjustmentCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdjustmentCode.Location = New System.Drawing.Point(12, 28)
        Me.lblAdjustmentCode.Name = "lblAdjustmentCode"
        Me.lblAdjustmentCode.Size = New System.Drawing.Size(93, 16)
        Me.lblAdjustmentCode.TabIndex = 161
        Me.lblAdjustmentCode.Text = "Adjustment Code"
        '
        'gvAdjustmentVoucher
        '
        Me.gvAdjustmentVoucher.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvAdjustmentVoucher.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvAdjustmentVoucher.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvAdjustmentVoucher.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvAdjustmentVoucher.ForeColor = System.Drawing.Color.Black
        Me.gvAdjustmentVoucher.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvAdjustmentVoucher.Location = New System.Drawing.Point(9, 195)
        '
        'gvAdjustmentVoucher
        '
        Me.gvAdjustmentVoucher.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvAdjustmentVoucher.MasterTemplate.AllowAddNewRow = False
        Me.gvAdjustmentVoucher.MasterTemplate.AutoGenerateColumns = False
        Me.gvAdjustmentVoucher.MasterTemplate.EnableGrouping = False
        Me.gvAdjustmentVoucher.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvAdjustmentVoucher.Name = "gvAdjustmentVoucher"
        Me.gvAdjustmentVoucher.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvAdjustmentVoucher.ShowHeaderCellButtons = True
        Me.gvAdjustmentVoucher.Size = New System.Drawing.Size(739, 264)
        Me.gvAdjustmentVoucher.TabIndex = 10
        Me.gvAdjustmentVoucher.TabStop = False
        Me.gvAdjustmentVoucher.Text = "RadGridView4"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(81, 16)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 16)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(682, 16)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(153, 16)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(757, 20)
        Me.RadMenu2.TabIndex = 16
        Me.RadMenu2.Text = "RadMenu2"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "File"
        Me.RadMenuItem3.AccessibleName = "File"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Export Blank Sheet"
        Me.RadMenuItem1.AccessibleName = "Export Blank Sheet"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Export Blank Sheet"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Import"
        Me.RadMenuItem2.AccessibleName = "Import"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Import"
        '
        'frmAdjustmentVoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(757, 510)
        Me.Controls.Add(Me.RadMenu2)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmAdjustmentVoucher"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Adjustment Voucher"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnUnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdjustmentByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdjustBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdjustmentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpAdjustDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdjustmentCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAdjustmentVoucher.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAdjustmentVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents gvAdjustmentVoucher As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtcode As common.UserControls.txtNavigator
    Friend WithEvents lblAdjustmentCode As common.Controls.MyLabel
    Friend WithEvents lblEmpCode As common.Controls.MyLabel
    Friend WithEvents txtEmpCode As common.UserControls.txtFinder
    Friend WithEvents lblAdjustmentDate As common.Controls.MyLabel
    Friend WithEvents dtpAdjustDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblAdjustBy As common.Controls.MyLabel
    Friend WithEvents txtAdjustBy As common.UserControls.txtFinder
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents findPayperiod As common.UserControls.txtFinder
    Friend WithEvents lblPayPeriod As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblPayPeriodName As common.Controls.MyLabel
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents lblAdjustmentByName As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblLocationCode As common.Controls.MyLabel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents FndLocationCode As common.UserControls.txtFinder
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnUnSelect As Telerik.WinControls.UI.RadButton
End Class
