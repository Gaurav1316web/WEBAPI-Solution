<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDispatchTransfer
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
        Me.txtFromLocationNameOld = New common.Controls.MyTextBox
        Me.lblLocation = New common.Controls.MyLabel
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.fndFromLocationOld = New common.UserControls.txtFinder
        Me.lblDocDate = New common.Controls.MyLabel
        Me.txtNewToLocationName = New common.Controls.MyTextBox
        Me.lblVendor = New common.Controls.MyLabel
        Me.fndNewToLocCode = New common.UserControls.txtFinder
        Me.lblPending = New common.usLock
        Me.rbtnReset = New Telerik.WinControls.UI.RadButton
        Me.fndDocNo = New common.UserControls.txtNavigator
        Me.lblDocNo = New common.Controls.MyLabel
        Me.dtpDate = New common.Controls.MyDateTimePicker
        Me.btnPost = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.fndTankerNo = New common.UserControls.txtFinder
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.txtChallanNo = New common.Controls.MyTextBox
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.ddlTransferTo = New common.Controls.MyComboBox
        Me.txtOLDToLocName = New common.Controls.MyTextBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtOLDToLocCode = New common.Controls.MyTextBox
        CType(Me.txtFromLocationNameOld, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNewToLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChallanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlTransferTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOLDToLocName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOLDToLocCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtFromLocationNameOld
        '
        Me.txtFromLocationNameOld.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtFromLocationNameOld.Enabled = False
        Me.txtFromLocationNameOld.Location = New System.Drawing.Point(397, 32)
        Me.txtFromLocationNameOld.MendatroryField = False
        Me.txtFromLocationNameOld.MyLinkLable1 = Nothing
        Me.txtFromLocationNameOld.MyLinkLable2 = Nothing
        Me.txtFromLocationNameOld.Name = "txtFromLocationNameOld"
        Me.txtFromLocationNameOld.Size = New System.Drawing.Size(438, 20)
        Me.txtFromLocationNameOld.TabIndex = 273
        '
        'lblLocation
        '
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(8, 32)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(62, 16)
        Me.lblLocation.TabIndex = 274
        Me.lblLocation.Text = "From MCC"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtOLDToLocCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtOLDToLocName)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.ddlTransferTo)
        Me.RadPageViewPage1.Controls.Add(Me.txtChallanNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.fndTankerNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromLocationNameOld)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.fndFromLocationOld)
        Me.RadPageViewPage1.Controls.Add(Me.txtNewToLocationName)
        Me.RadPageViewPage1.Controls.Add(Me.lblVendor)
        Me.RadPageViewPage1.Controls.Add(Me.fndNewToLocCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblPending)
        Me.RadPageViewPage1.Controls.Add(Me.rbtnReset)
        Me.RadPageViewPage1.Controls.Add(Me.lblDocDate)
        Me.RadPageViewPage1.Controls.Add(Me.fndDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.dtpDate)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(130.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(843, 424)
        Me.RadPageViewPage1.Text = "MCC Dispatch Transfer"
        '
        'fndFromLocationOld
        '
        Me.fndFromLocationOld.Location = New System.Drawing.Point(127, 32)
        Me.fndFromLocationOld.MendatroryField = True
        Me.fndFromLocationOld.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndFromLocationOld.MyLinkLable1 = Me.lblLocation
        Me.fndFromLocationOld.MyLinkLable2 = Nothing
        Me.fndFromLocationOld.MyReadOnly = False
        Me.fndFromLocationOld.MyShowMasterFormButton = False
        Me.fndFromLocationOld.Name = "fndFromLocationOld"
        Me.fndFromLocationOld.Size = New System.Drawing.Size(261, 19)
        Me.fndFromLocationOld.TabIndex = 272
        Me.fndFromLocationOld.Value = ""
        '
        'lblDocDate
        '
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocDate.Location = New System.Drawing.Point(394, 10)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(85, 16)
        Me.lblDocDate.TabIndex = 257
        Me.lblDocDate.Text = "Document Date"
        '
        'txtNewToLocationName
        '
        Me.txtNewToLocationName.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtNewToLocationName.Enabled = False
        Me.txtNewToLocationName.Location = New System.Drawing.Point(392, 128)
        Me.txtNewToLocationName.MendatroryField = False
        Me.txtNewToLocationName.MyLinkLable1 = Nothing
        Me.txtNewToLocationName.MyLinkLable2 = Nothing
        Me.txtNewToLocationName.Name = "txtNewToLocationName"
        Me.txtNewToLocationName.Size = New System.Drawing.Size(441, 20)
        Me.txtNewToLocationName.TabIndex = 261
        '
        'lblVendor
        '
        Me.lblVendor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendor.Location = New System.Drawing.Point(10, 128)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Size = New System.Drawing.Size(109, 16)
        Me.lblVendor.TabIndex = 262
        Me.lblVendor.Text = "MCC/PLANT (NEW)"
        '
        'fndNewToLocCode
        '
        Me.fndNewToLocCode.Location = New System.Drawing.Point(126, 128)
        Me.fndNewToLocCode.MendatroryField = True
        Me.fndNewToLocCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndNewToLocCode.MyLinkLable1 = Me.lblVendor
        Me.fndNewToLocCode.MyLinkLable2 = Nothing
        Me.fndNewToLocCode.MyReadOnly = False
        Me.fndNewToLocCode.MyShowMasterFormButton = False
        Me.fndNewToLocCode.Name = "fndNewToLocCode"
        Me.fndNewToLocCode.Size = New System.Drawing.Size(260, 19)
        Me.fndNewToLocCode.TabIndex = 260
        Me.fndNewToLocCode.Value = ""
        '
        'lblPending
        '
        Me.lblPending.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(736, 8)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(97, 20)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 256
        '
        'rbtnReset
        '
        Me.rbtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.rbtnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtnReset.Location = New System.Drawing.Point(369, 6)
        Me.rbtnReset.Name = "rbtnReset"
        Me.rbtnReset.Size = New System.Drawing.Size(19, 21)
        Me.rbtnReset.TabIndex = 254
        '
        'fndDocNo
        '
        Me.fndDocNo.Location = New System.Drawing.Point(128, 6)
        Me.fndDocNo.MendatroryField = False
        Me.fndDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocNo.MyLinkLable1 = Me.lblDocNo
        Me.fndDocNo.MyLinkLable2 = Nothing
        Me.fndDocNo.MyMaxLength = 32767
        Me.fndDocNo.MyReadOnly = False
        Me.fndDocNo.Name = "fndDocNo"
        Me.fndDocNo.Size = New System.Drawing.Size(239, 21)
        Me.fndDocNo.TabIndex = 252
        Me.fndDocNo.Value = ""
        '
        'lblDocNo
        '
        Me.lblDocNo.Location = New System.Drawing.Point(8, 8)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(79, 18)
        Me.lblDocNo.TabIndex = 255
        Me.lblDocNo.Text = "Document No."
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(485, 8)
        Me.dtpDate.MendatroryField = False
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.MyLinkLable1 = Me.lblDocDate
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.Size = New System.Drawing.Size(153, 20)
        Me.dtpDate.TabIndex = 253
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "10/06/2011 11:51:56 AM"
        Me.dtpDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(144, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 18)
        Me.btnPost.TabIndex = 7
        Me.btnPost.Text = "Post"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(73, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(784, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(864, 502)
        Me.SplitContainer1.SplitterDistance = 472
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(864, 472)
        Me.RadPageView1.TabIndex = 2
        Me.RadPageView1.Text = "RadPageView1"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save"
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(8, 57)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(59, 16)
        Me.MyLabel3.TabIndex = 276
        Me.MyLabel3.Text = "Tanker No"
        '
        'fndTankerNo
        '
        Me.fndTankerNo.Location = New System.Drawing.Point(127, 55)
        Me.fndTankerNo.MendatroryField = True
        Me.fndTankerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTankerNo.MyLinkLable1 = Me.MyLabel3
        Me.fndTankerNo.MyLinkLable2 = Nothing
        Me.fndTankerNo.MyReadOnly = False
        Me.fndTankerNo.MyShowMasterFormButton = False
        Me.fndTankerNo.Name = "fndTankerNo"
        Me.fndTankerNo.Size = New System.Drawing.Size(261, 19)
        Me.fndTankerNo.TabIndex = 275
        Me.fndTankerNo.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(395, 58)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel4.TabIndex = 278
        Me.MyLabel4.Text = "Challan No."
        '
        'txtChallanNo
        '
        Me.txtChallanNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtChallanNo.Enabled = False
        Me.txtChallanNo.Location = New System.Drawing.Point(462, 55)
        Me.txtChallanNo.MendatroryField = False
        Me.txtChallanNo.MyLinkLable1 = Nothing
        Me.txtChallanNo.MyLinkLable2 = Nothing
        Me.txtChallanNo.Name = "txtChallanNo"
        Me.txtChallanNo.Size = New System.Drawing.Size(373, 20)
        Me.txtChallanNo.TabIndex = 279
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(8, 105)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(103, 16)
        Me.MyLabel5.TabIndex = 281
        Me.MyLabel5.Text = "Tanker Transfer To"
        '
        'ddlTransferTo
        '
        Me.ddlTransferTo.AllowShowFocusCues = False
        Me.ddlTransferTo.AutoCompleteDisplayMember = Nothing
        Me.ddlTransferTo.AutoCompleteValueMember = Nothing
        Me.ddlTransferTo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "MCC"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "PLANT"
        RadListDataItem2.TextWrap = True
        Me.ddlTransferTo.Items.Add(RadListDataItem1)
        Me.ddlTransferTo.Items.Add(RadListDataItem2)
        Me.ddlTransferTo.Location = New System.Drawing.Point(126, 103)
        Me.ddlTransferTo.MendatroryField = True
        Me.ddlTransferTo.MyLinkLable1 = Me.MyLabel5
        Me.ddlTransferTo.MyLinkLable2 = Nothing
        Me.ddlTransferTo.Name = "ddlTransferTo"
        Me.ddlTransferTo.Size = New System.Drawing.Size(262, 20)
        Me.ddlTransferTo.TabIndex = 280
        '
        'txtOLDToLocName
        '
        Me.txtOLDToLocName.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtOLDToLocName.Enabled = False
        Me.txtOLDToLocName.Location = New System.Drawing.Point(393, 79)
        Me.txtOLDToLocName.MendatroryField = False
        Me.txtOLDToLocName.MyLinkLable1 = Nothing
        Me.txtOLDToLocName.MyLinkLable2 = Nothing
        Me.txtOLDToLocName.Name = "txtOLDToLocName"
        Me.txtOLDToLocName.Size = New System.Drawing.Size(441, 20)
        Me.txtOLDToLocName.TabIndex = 283
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(10, 79)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(106, 16)
        Me.MyLabel1.TabIndex = 284
        Me.MyLabel1.Text = "MCC/PLANT (OLD)"
        '
        'txtOLDToLocCode
        '
        Me.txtOLDToLocCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtOLDToLocCode.Enabled = False
        Me.txtOLDToLocCode.Location = New System.Drawing.Point(126, 79)
        Me.txtOLDToLocCode.MendatroryField = False
        Me.txtOLDToLocCode.MyLinkLable1 = Nothing
        Me.txtOLDToLocCode.MyLinkLable2 = Nothing
        Me.txtOLDToLocCode.Name = "txtOLDToLocCode"
        Me.txtOLDToLocCode.Size = New System.Drawing.Size(262, 20)
        Me.txtOLDToLocCode.TabIndex = 285
        '
        'FrmDispatchTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(864, 502)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmDispatchTransfer"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmDispatchTransfer"
        CType(Me.txtFromLocationNameOld, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNewToLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChallanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlTransferTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOLDToLocName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOLDToLocCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtFromLocationNameOld As common.Controls.MyTextBox
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents fndFromLocationOld As common.UserControls.txtFinder
    Friend WithEvents lblDocDate As common.Controls.MyLabel
    Friend WithEvents txtNewToLocationName As common.Controls.MyTextBox
    Friend WithEvents lblVendor As common.Controls.MyLabel
    Friend WithEvents fndNewToLocCode As common.UserControls.txtFinder
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents rbtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndDocNo As common.UserControls.txtNavigator
    Friend WithEvents lblDocNo As common.Controls.MyLabel
    Friend WithEvents dtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents fndTankerNo As common.UserControls.txtFinder
    Friend WithEvents txtChallanNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents ddlTransferTo As common.Controls.MyComboBox
    Friend WithEvents txtOLDToLocName As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtOLDToLocCode As common.Controls.MyTextBox
End Class

