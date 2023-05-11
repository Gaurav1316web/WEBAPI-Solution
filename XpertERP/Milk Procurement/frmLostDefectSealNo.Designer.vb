<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLostDefectSealNo
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
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.lblPending = New common.usLock
        Me.btnGo = New Telerik.WinControls.UI.RadButton
        Me.lblLocation = New common.Controls.MyLabel
        Me.lblLocationDec = New common.Controls.MyLabel
        Me.fndLocation = New common.UserControls.txtFinder
        Me.lblDocDate = New common.Controls.MyLabel
        Me.dtpDocDate = New common.Controls.MyDateTimePicker
        Me.lblDocNo = New common.Controls.MyLabel
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.ddlDocType = New Telerik.WinControls.UI.RadDropDownList
        Me.fndDocNo = New common.UserControls.txtNavigator
        Me.lblDocType = New common.Controls.MyLabel
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.gv = New common.UserControls.MyRadGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtRemarks = New common.Controls.MyTextBox
        Me.btnPost = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.cmbSealType = New Telerik.WinControls.UI.RadDropDownList
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSealType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(640, 473)
        Me.SplitContainer1.SplitterDistance = 443
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmbSealType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblPending)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocationDec)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDocDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpDocDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDocNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ddlDocType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndDocNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDocType)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Size = New System.Drawing.Size(640, 443)
        Me.SplitContainer2.SplitterDistance = 78
        Me.SplitContainer2.TabIndex = 0
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(486, 51)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(118, 25)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 276
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(422, 55)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(58, 18)
        Me.btnGo.TabIndex = 275
        Me.btnGo.Text = ">>"
        '
        'lblLocation
        '
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(6, 31)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 273
        Me.lblLocation.Text = "Location"
        '
        'lblLocationDec
        '
        Me.lblLocationDec.AutoSize = False
        Me.lblLocationDec.BorderVisible = True
        Me.lblLocationDec.Location = New System.Drawing.Point(339, 31)
        Me.lblLocationDec.Name = "lblLocationDec"
        Me.lblLocationDec.Size = New System.Drawing.Size(265, 19)
        Me.lblLocationDec.TabIndex = 274
        Me.lblLocationDec.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fndLocation
        '
        Me.fndLocation.Location = New System.Drawing.Point(70, 31)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.lblLocation
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.Size = New System.Drawing.Size(266, 19)
        Me.fndLocation.TabIndex = 272
        Me.fndLocation.Value = ""
        '
        'lblDocDate
        '
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocDate.Location = New System.Drawing.Point(403, 9)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(53, 16)
        Me.lblDocDate.TabIndex = 253
        Me.lblDocDate.Text = "Doc Date"
        '
        'dtpDocDate
        '
        Me.dtpDocDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDocDate.Location = New System.Drawing.Point(458, 7)
        Me.dtpDocDate.MendatroryField = False
        Me.dtpDocDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocDate.MyLinkLable1 = Me.lblDocDate
        Me.dtpDocDate.MyLinkLable2 = Nothing
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocDate.Size = New System.Drawing.Size(145, 20)
        Me.dtpDocDate.TabIndex = 252
        Me.dtpDocDate.TabStop = False
        Me.dtpDocDate.Text = "10/06/2011"
        Me.dtpDocDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblDocNo
        '
        Me.lblDocNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDocNo.Location = New System.Drawing.Point(7, 10)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(46, 16)
        Me.lblDocNo.TabIndex = 14
        Me.lblDocNo.Text = "Doc No"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnReset.Location = New System.Drawing.Point(322, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(14, 21)
        Me.btnReset.TabIndex = 12
        '
        'ddlDocType
        '
        Me.ddlDocType.AllowShowFocusCues = False
        Me.ddlDocType.AutoCompleteDisplayMember = Nothing
        Me.ddlDocType.AutoCompleteValueMember = Nothing
        Me.ddlDocType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem4.Text = "Co. Owned"
        RadListDataItem4.TextWrap = True
        RadListDataItem5.Text = "Co. Leased"
        RadListDataItem5.TextWrap = True
        RadListDataItem6.Text = "Chilling Basis"
        RadListDataItem6.TextWrap = True
        Me.ddlDocType.Items.Add(RadListDataItem4)
        Me.ddlDocType.Items.Add(RadListDataItem5)
        Me.ddlDocType.Items.Add(RadListDataItem6)
        Me.ddlDocType.Location = New System.Drawing.Point(70, 54)
        Me.ddlDocType.Name = "ddlDocType"
        Me.ddlDocType.SelectNextOnDoubleClick = True
        Me.ddlDocType.Size = New System.Drawing.Size(109, 20)
        Me.ddlDocType.TabIndex = 11
        '
        'fndDocNo
        '
        Me.fndDocNo.Location = New System.Drawing.Point(70, 6)
        Me.fndDocNo.MendatroryField = True
        Me.fndDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocNo.MyLinkLable1 = Me.lblDocNo
        Me.fndDocNo.MyLinkLable2 = Nothing
        Me.fndDocNo.MyMaxLength = 32767
        Me.fndDocNo.MyReadOnly = False
        Me.fndDocNo.Name = "fndDocNo"
        Me.fndDocNo.Size = New System.Drawing.Size(253, 21)
        Me.fndDocNo.TabIndex = 10
        Me.fndDocNo.Value = ""
        '
        'lblDocType
        '
        Me.lblDocType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocType.Location = New System.Drawing.Point(5, 54)
        Me.lblDocType.Name = "lblDocType"
        Me.lblDocType.Size = New System.Drawing.Size(55, 16)
        Me.lblDocType.TabIndex = 13
        Me.lblDocType.Text = "Doc Type"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.gv)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer3.Size = New System.Drawing.Size(640, 361)
        Me.SplitContainer3.SplitterDistance = 287
        Me.SplitContainer3.TabIndex = 0
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(0, 0)
        Me.gv.Name = "gv"
        Me.gv.Size = New System.Drawing.Size(640, 287)
        Me.gv.TabIndex = 265
        Me.gv.Text = "RadGridView1"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(640, 70)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.AutoSize = False
        Me.txtRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtRemarks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtRemarks.Location = New System.Drawing.Point(3, 18)
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.MyLinkLable1 = Nothing
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(634, 49)
        Me.txtRemarks.TabIndex = 8
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(145, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 18)
        Me.btnPost.TabIndex = 8
        Me.btnPost.Text = "Post"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(76, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(569, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(7, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save"
        '
        'cmbSealType
        '
        Me.cmbSealType.AllowShowFocusCues = False
        Me.cmbSealType.AutoCompleteDisplayMember = Nothing
        Me.cmbSealType.AutoCompleteValueMember = Nothing
        Me.cmbSealType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "Co. Owned"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Co. Leased"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "Chilling Basis"
        RadListDataItem3.TextWrap = True
        Me.cmbSealType.Items.Add(RadListDataItem1)
        Me.cmbSealType.Items.Add(RadListDataItem2)
        Me.cmbSealType.Items.Add(RadListDataItem3)
        Me.cmbSealType.Location = New System.Drawing.Point(243, 54)
        Me.cmbSealType.Name = "cmbSealType"
        Me.cmbSealType.SelectNextOnDoubleClick = True
        Me.cmbSealType.Size = New System.Drawing.Size(141, 20)
        Me.cmbSealType.TabIndex = 277
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(181, 56)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel1.TabIndex = 278
        Me.MyLabel1.Text = "Seal Type"
        '
        'FrmLostDefectSealNo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(640, 473)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmLostDefectSealNo"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmLostDefectSerialNo"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlDocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSealType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblDocNo As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents ddlDocType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents fndDocNo As common.UserControls.txtNavigator
    Friend WithEvents lblDocType As common.Controls.MyLabel
    Friend WithEvents lblDocDate As common.Controls.MyLabel
    Friend WithEvents dtpDocDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblLocationDec As common.Controls.MyLabel
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents cmbSealType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
End Class

