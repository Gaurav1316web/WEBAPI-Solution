<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTankerDispatchPrice_Master
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Me.penalItemCode = New System.Windows.Forms.Panel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblItemCode = New common.Controls.MyLabel()
        Me.txtItemCode = New common.UserControls.txtFinder()
        Me.chkJobWork = New System.Windows.Forms.CheckBox()
        Me.UsLock1 = New common.usLock()
        Me.txtMCC = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtrate = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtefctdate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtname = New common.Controls.MyTextBox()
        Me.lblvendorname = New common.Controls.MyLabel()
        Me.lblvandorno = New common.Controls.MyLabel()
        Me.fndcode = New common.UserControls.txtNavigator()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.MenuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.penalItemCode.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtefctdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvendorname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(668, 517)
        Me.SplitContainer1.SplitterDistance = 488
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Location = New System.Drawing.Point(0, 26)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(665, 459)
        Me.RadPageView1.TabIndex = 13
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.penalItemCode)
        Me.RadPageViewPage1.Controls.Add(Me.chkJobWork)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtMCC)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel16)
        Me.RadPageViewPage1.Controls.Add(Me.txtrate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.txtefctdate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtname)
        Me.RadPageViewPage1.Controls.Add(Me.lblvendorname)
        Me.RadPageViewPage1.Controls.Add(Me.lblvandorno)
        Me.RadPageViewPage1.Controls.Add(Me.fndcode)
        Me.RadPageViewPage1.Controls.Add(Me.btnnew)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(77.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(644, 411)
        Me.RadPageViewPage1.Text = "Price Details"
        '
        'penalItemCode
        '
        Me.penalItemCode.Controls.Add(Me.MyLabel2)
        Me.penalItemCode.Controls.Add(Me.lblItemCode)
        Me.penalItemCode.Controls.Add(Me.txtItemCode)
        Me.penalItemCode.Location = New System.Drawing.Point(3, 113)
        Me.penalItemCode.Name = "penalItemCode"
        Me.penalItemCode.Size = New System.Drawing.Size(444, 25)
        Me.penalItemCode.TabIndex = 1462
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(6, 3)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel2.TabIndex = 1456
        Me.MyLabel2.Text = "Item Code"
        '
        'lblItemCode
        '
        Me.lblItemCode.AutoSize = False
        Me.lblItemCode.BorderVisible = True
        Me.lblItemCode.FieldName = Nothing
        Me.lblItemCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemCode.Location = New System.Drawing.Point(223, 1)
        Me.lblItemCode.Name = "lblItemCode"
        Me.lblItemCode.Size = New System.Drawing.Size(192, 21)
        Me.lblItemCode.TabIndex = 1461
        '
        'txtItemCode
        '
        Me.txtItemCode.CalculationExpression = Nothing
        Me.txtItemCode.FieldCode = Nothing
        Me.txtItemCode.FieldDesc = Nothing
        Me.txtItemCode.FieldMaxLength = 0
        Me.txtItemCode.FieldName = Nothing
        Me.txtItemCode.isCalculatedField = False
        Me.txtItemCode.IsSourceFromTable = False
        Me.txtItemCode.IsSourceFromValueList = False
        Me.txtItemCode.IsUnique = False
        Me.txtItemCode.Location = New System.Drawing.Point(93, 1)
        Me.txtItemCode.MendatroryField = True
        Me.txtItemCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.MyLinkLable1 = Nothing
        Me.txtItemCode.MyLinkLable2 = Nothing
        Me.txtItemCode.MyReadOnly = False
        Me.txtItemCode.MyShowMasterFormButton = False
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.ReferenceFieldDesc = Nothing
        Me.txtItemCode.ReferenceFieldName = Nothing
        Me.txtItemCode.ReferenceTableName = Nothing
        Me.txtItemCode.Size = New System.Drawing.Size(126, 21)
        Me.txtItemCode.TabIndex = 1460
        Me.txtItemCode.Value = ""
        '
        'chkJobWork
        '
        Me.chkJobWork.AutoSize = True
        Me.chkJobWork.Location = New System.Drawing.Point(425, 9)
        Me.chkJobWork.Name = "chkJobWork"
        Me.chkJobWork.Size = New System.Drawing.Size(73, 17)
        Me.chkJobWork.TabIndex = 1455
        Me.chkJobWork.Text = "Job work"
        Me.chkJobWork.UseVisualStyleBackColor = True
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(544, 4)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(97, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1454
        '
        'txtMCC
        '
        Me.txtMCC.arrDispalyMember = Nothing
        Me.txtMCC.arrValueMember = Nothing
        Me.txtMCC.Location = New System.Drawing.Point(96, 49)
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Me.MyLabel16
        Me.txtMCC.MyLinkLable2 = Nothing
        Me.txtMCC.MyNullText = "Select"
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.Size = New System.Drawing.Size(321, 19)
        Me.txtMCC.TabIndex = 391
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(8, 49)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel16.TabIndex = 392
        Me.MyLabel16.Text = "MCC"
        '
        'txtrate
        '
        Me.txtrate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtrate.CalculationExpression = Nothing
        Me.txtrate.DecimalPlaces = 2
        Me.txtrate.FieldCode = Nothing
        Me.txtrate.FieldDesc = Nothing
        Me.txtrate.FieldMaxLength = 0
        Me.txtrate.FieldName = Nothing
        Me.txtrate.isCalculatedField = False
        Me.txtrate.IsSourceFromTable = False
        Me.txtrate.IsSourceFromValueList = False
        Me.txtrate.IsUnique = False
        Me.txtrate.Location = New System.Drawing.Point(96, 90)
        Me.txtrate.MendatroryField = True
        Me.txtrate.MyLinkLable1 = Nothing
        Me.txtrate.MyLinkLable2 = Nothing
        Me.txtrate.Name = "txtrate"
        Me.txtrate.ReferenceFieldDesc = Nothing
        Me.txtrate.ReferenceFieldName = Nothing
        Me.txtrate.ReferenceTableName = Nothing
        Me.txtrate.Size = New System.Drawing.Size(125, 20)
        Me.txtrate.TabIndex = 21
        Me.txtrate.Text = "0"
        Me.txtrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtrate.Value = 0R
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(8, 92)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel6.TabIndex = 22
        Me.MyLabel6.Text = "Total Solid Rate"
        '
        'txtefctdate
        '
        Me.txtefctdate.CalculationExpression = Nothing
        Me.txtefctdate.CustomFormat = "dd/MM/yyyy"
        Me.txtefctdate.FieldCode = Nothing
        Me.txtefctdate.FieldDesc = Nothing
        Me.txtefctdate.FieldMaxLength = 0
        Me.txtefctdate.FieldName = Nothing
        Me.txtefctdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtefctdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtefctdate.isCalculatedField = False
        Me.txtefctdate.IsSourceFromTable = False
        Me.txtefctdate.IsSourceFromValueList = False
        Me.txtefctdate.IsUnique = False
        Me.txtefctdate.Location = New System.Drawing.Point(96, 70)
        Me.txtefctdate.MendatroryField = True
        Me.txtefctdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtefctdate.MyLinkLable1 = Me.MyLabel1
        Me.txtefctdate.MyLinkLable2 = Nothing
        Me.txtefctdate.Name = "txtefctdate"
        Me.txtefctdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtefctdate.ReferenceFieldDesc = Nothing
        Me.txtefctdate.ReferenceFieldName = Nothing
        Me.txtefctdate.ReferenceTableName = Nothing
        Me.txtefctdate.Size = New System.Drawing.Size(125, 18)
        Me.txtefctdate.TabIndex = 17
        Me.txtefctdate.TabStop = False
        Me.txtefctdate.Text = "13/06/2011"
        Me.txtefctdate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(8, 71)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel1.TabIndex = 18
        Me.MyLabel1.Text = "Effective Date"
        '
        'txtname
        '
        Me.txtname.CalculationExpression = Nothing
        Me.txtname.FieldCode = Nothing
        Me.txtname.FieldDesc = Nothing
        Me.txtname.FieldMaxLength = 0
        Me.txtname.FieldName = Nothing
        Me.txtname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtname.isCalculatedField = False
        Me.txtname.IsSourceFromTable = False
        Me.txtname.IsSourceFromValueList = False
        Me.txtname.IsUnique = False
        Me.txtname.Location = New System.Drawing.Point(96, 29)
        Me.txtname.MaxLength = 150
        Me.txtname.MendatroryField = True
        Me.txtname.MyLinkLable1 = Me.MyLabel1
        Me.txtname.MyLinkLable2 = Nothing
        Me.txtname.Name = "txtname"
        Me.txtname.ReferenceFieldDesc = Nothing
        Me.txtname.ReferenceFieldName = Nothing
        Me.txtname.ReferenceTableName = Nothing
        Me.txtname.Size = New System.Drawing.Size(321, 18)
        Me.txtname.TabIndex = 15
        '
        'lblvendorname
        '
        Me.lblvendorname.FieldName = Nothing
        Me.lblvendorname.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblvendorname.Location = New System.Drawing.Point(8, 30)
        Me.lblvendorname.Name = "lblvendorname"
        Me.lblvendorname.Size = New System.Drawing.Size(63, 16)
        Me.lblvendorname.TabIndex = 16
        Me.lblvendorname.Text = "Description"
        '
        'lblvandorno
        '
        Me.lblvandorno.FieldName = Nothing
        Me.lblvandorno.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblvandorno.Location = New System.Drawing.Point(8, 8)
        Me.lblvandorno.Name = "lblvandorno"
        Me.lblvandorno.Size = New System.Drawing.Size(62, 16)
        Me.lblvandorno.TabIndex = 14
        Me.lblvandorno.Text = "Price Code"
        '
        'fndcode
        '
        Me.fndcode.FieldName = Nothing
        Me.fndcode.Location = New System.Drawing.Point(96, 6)
        Me.fndcode.MendatroryField = True
        Me.fndcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndcode.MyLinkLable1 = Me.lblvandorno
        Me.fndcode.MyLinkLable2 = Nothing
        Me.fndcode.MyMaxLength = 30
        Me.fndcode.MyReadOnly = False
        Me.fndcode.Name = "fndcode"
        Me.fndcode.Size = New System.Drawing.Size(302, 21)
        Me.fndcode.TabIndex = 12
        Me.fndcode.TabStop = False
        Me.fndcode.Value = ""
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(395, 6)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(20, 21)
        Me.btnnew.TabIndex = 13
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuClose})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(668, 20)
        Me.RadMenu1.TabIndex = 12
        Me.RadMenu1.Visible = False
        '
        'MenuClose
        '
        Me.MenuClose.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.MenuClose.Name = "MenuClose"
        Me.MenuClose.Text = "File"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Import"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(592, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 29
        Me.btnclose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Location = New System.Drawing.Point(165, 3)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 28
        Me.btnPost.Text = "Post"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(84, 3)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 27
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(3, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 26
        Me.btnsave.Text = "Save"
        '
        'FrmTankerDispatchPrice_Master
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(668, 517)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmTankerDispatchPrice_Master"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Tanker Dispatch Price Master"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.penalItemCode.ResumeLayout(False)
        Me.penalItemCode.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtrate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtefctdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvendorname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents MenuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents lblvandorno As common.Controls.MyLabel
    Friend WithEvents fndcode As common.UserControls.txtNavigator
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtname As common.Controls.MyTextBox
    Friend WithEvents lblvendorname As common.Controls.MyLabel
    Friend WithEvents txtefctdate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtrate As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtMCC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents chkJobWork As System.Windows.Forms.CheckBox
    Friend WithEvents lblItemCode As common.Controls.MyLabel
    Friend WithEvents txtItemCode As common.UserControls.txtFinder
    Friend WithEvents penalItemCode As System.Windows.Forms.Panel
End Class

