<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVLCRouteShiftMaster
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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.lblRouteCode = New common.Controls.MyLabel()
        Me.fndRouteCode = New common.UserControls.txtFinder()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.MenuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.txtdesc = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.fndcode = New common.UserControls.txtNavigator()
        Me.txtdate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.cmbstatus = New common.Controls.MyComboBox()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbstatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(5)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(872, 482)
        Me.SplitContainer1.SplitterDistance = 445
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(5, 5)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRouteCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndRouteCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtdesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndcode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtdate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmbstatus)
        Me.SplitContainer2.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(6)
        Me.SplitContainer2.Size = New System.Drawing.Size(862, 435)
        Me.SplitContainer2.SplitterDistance = 99
        Me.SplitContainer2.TabIndex = 54
        '
        'lblRouteCode
        '
        Me.lblRouteCode.FieldName = Nothing
        Me.lblRouteCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRouteCode.Location = New System.Drawing.Point(8, 74)
        Me.lblRouteCode.Name = "lblRouteCode"
        Me.lblRouteCode.Size = New System.Drawing.Size(51, 16)
        Me.lblRouteCode.TabIndex = 248
        Me.lblRouteCode.Text = "Route ID"
        '
        'fndRouteCode
        '
        Me.fndRouteCode.CalculationExpression = Nothing
        Me.fndRouteCode.FieldCode = Nothing
        Me.fndRouteCode.FieldDesc = Nothing
        Me.fndRouteCode.FieldMaxLength = 0
        Me.fndRouteCode.FieldName = Nothing
        Me.fndRouteCode.isCalculatedField = False
        Me.fndRouteCode.IsSourceFromTable = False
        Me.fndRouteCode.IsSourceFromValueList = False
        Me.fndRouteCode.IsUnique = False
        Me.fndRouteCode.Location = New System.Drawing.Point(83, 74)
        Me.fndRouteCode.MendatroryField = True
        Me.fndRouteCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRouteCode.MyLinkLable1 = Me.lblRouteCode
        Me.fndRouteCode.MyLinkLable2 = Nothing
        Me.fndRouteCode.MyReadOnly = False
        Me.fndRouteCode.MyShowMasterFormButton = False
        Me.fndRouteCode.Name = "fndRouteCode"
        Me.fndRouteCode.ReferenceFieldDesc = Nothing
        Me.fndRouteCode.ReferenceFieldName = Nothing
        Me.fndRouteCode.ReferenceTableName = Nothing
        Me.fndRouteCode.Size = New System.Drawing.Size(167, 19)
        Me.fndRouteCode.TabIndex = 247
        Me.fndRouteCode.Value = ""
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuClose})
        Me.RadMenu1.Location = New System.Drawing.Point(3, 3)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(856, 20)
        Me.RadMenu1.TabIndex = 11
        '
        'MenuClose
        '
        Me.MenuClose.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexport, Me.btnimport})
        Me.MenuClose.Name = "MenuClose"
        Me.MenuClose.Text = "File"
        '
        'btnexport
        '
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Text = "Export"
        '
        'btnimport
        '
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Text = "Import"
        '
        'txtdesc
        '
        Me.txtdesc.CalculationExpression = Nothing
        Me.txtdesc.FieldCode = Nothing
        Me.txtdesc.FieldDesc = Nothing
        Me.txtdesc.FieldMaxLength = 0
        Me.txtdesc.FieldName = Nothing
        Me.txtdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdesc.isCalculatedField = False
        Me.txtdesc.IsSourceFromTable = False
        Me.txtdesc.IsSourceFromValueList = False
        Me.txtdesc.IsUnique = False
        Me.txtdesc.Location = New System.Drawing.Point(84, 52)
        Me.txtdesc.MaxLength = 100
        Me.txtdesc.MendatroryField = True
        Me.txtdesc.MyLinkLable1 = Me.MyLabel4
        Me.txtdesc.MyLinkLable2 = Nothing
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.ReferenceFieldDesc = Nothing
        Me.txtdesc.ReferenceFieldName = Nothing
        Me.txtdesc.ReferenceTableName = Nothing
        Me.txtdesc.Size = New System.Drawing.Size(391, 18)
        Me.txtdesc.TabIndex = 2
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(8, 52)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel4.TabIndex = 52
        Me.MyLabel4.Text = "Description"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(8, 28)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel1.TabIndex = 11
        Me.MyLabel1.Text = "Doc No"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(308, 26)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 0
        '
        'fndcode
        '
        Me.fndcode.FieldName = Nothing
        Me.fndcode.Location = New System.Drawing.Point(84, 25)
        Me.fndcode.MendatroryField = True
        Me.fndcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndcode.MyLinkLable1 = Me.MyLabel1
        Me.fndcode.MyLinkLable2 = Nothing
        Me.fndcode.MyMaxLength = 30
        Me.fndcode.MyReadOnly = False
        Me.fndcode.Name = "fndcode"
        Me.fndcode.Size = New System.Drawing.Size(222, 21)
        Me.fndcode.TabIndex = 4
        Me.fndcode.TabStop = False
        Me.fndcode.Value = ""
        '
        'txtdate
        '
        Me.txtdate.CalculationExpression = Nothing
        Me.txtdate.CustomFormat = "dd/MM/yyyy"
        Me.txtdate.FieldCode = Nothing
        Me.txtdate.FieldDesc = Nothing
        Me.txtdate.FieldMaxLength = 0
        Me.txtdate.FieldName = Nothing
        Me.txtdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtdate.isCalculatedField = False
        Me.txtdate.IsSourceFromTable = False
        Me.txtdate.IsSourceFromValueList = False
        Me.txtdate.IsUnique = False
        Me.txtdate.Location = New System.Drawing.Point(367, 25)
        Me.txtdate.MendatroryField = True
        Me.txtdate.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.txtdate.MyLinkLable1 = Me.MyLabel3
        Me.txtdate.MyLinkLable2 = Nothing
        Me.txtdate.Name = "txtdate"
        Me.txtdate.NullText = "01/01/1973"
        Me.txtdate.ReferenceFieldDesc = Nothing
        Me.txtdate.ReferenceFieldName = Nothing
        Me.txtdate.ReferenceTableName = Nothing
        Me.txtdate.Size = New System.Drawing.Size(108, 20)
        Me.txtdate.TabIndex = 1
        Me.txtdate.TabStop = False
        Me.txtdate.Text = "04/06/2014"
        Me.txtdate.Value = New Date(2014, 6, 4, 17, 42, 23, 958)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(334, 27)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel3.TabIndex = 49
        Me.MyLabel3.Text = "Date"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(255, 77)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(38, 16)
        Me.MyLabel2.TabIndex = 36
        Me.MyLabel2.Text = "Status"
        '
        'cmbstatus
        '
        Me.cmbstatus.AutoCompleteDisplayMember = Nothing
        Me.cmbstatus.AutoCompleteValueMember = Nothing
        Me.cmbstatus.CalculationExpression = Nothing
        Me.cmbstatus.DropDownAnimationEnabled = True
        Me.cmbstatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbstatus.FieldCode = Nothing
        Me.cmbstatus.FieldDesc = Nothing
        Me.cmbstatus.FieldMaxLength = 0
        Me.cmbstatus.FieldName = Nothing
        Me.cmbstatus.isCalculatedField = False
        Me.cmbstatus.IsSourceFromTable = False
        Me.cmbstatus.IsSourceFromValueList = False
        Me.cmbstatus.IsUnique = False
        Me.cmbstatus.Location = New System.Drawing.Point(299, 74)
        Me.cmbstatus.MendatroryField = True
        Me.cmbstatus.MyLinkLable1 = Me.MyLabel2
        Me.cmbstatus.MyLinkLable2 = Nothing
        Me.cmbstatus.Name = "cmbstatus"
        Me.cmbstatus.ReferenceFieldDesc = Nothing
        Me.cmbstatus.ReferenceFieldName = Nothing
        Me.cmbstatus.ReferenceTableName = Nothing
        Me.cmbstatus.Size = New System.Drawing.Size(177, 20)
        Me.cmbstatus.TabIndex = 3
        Me.cmbstatus.Text = "MyComboBox1"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(6, 6)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(850, 320)
        Me.RadPageView1.TabIndex = 52
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(829, 272)
        Me.RadPageViewPage1.Text = "Detail"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gv)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(7)
        Me.RadGroupBox1.Size = New System.Drawing.Size(829, 272)
        Me.RadGroupBox1.TabIndex = 51
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(7, 7)
        '
        '
        '
        Me.gv.MasterTemplate.AllowDragToGroup = False
        Me.gv.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv.MyStopExport = False
        Me.gv.Name = "gv"
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(815, 258)
        Me.gv.TabIndex = 0
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(940, 256)
        Me.RadPageViewPage2.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(940, 256)
        Me.UcAttachment1.TabIndex = 2
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(784, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(76, 22)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "&Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(90, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(76, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "&Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(8, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(76, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "&Save"
        '
        'FrmVLCRouteShiftMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(872, 482)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmVLCRouteShiftMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmVLCRouteShiftMaster"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbstatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents MenuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents fndcode As common.UserControls.txtNavigator
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents cmbstatus As common.Controls.MyComboBox
    Friend WithEvents txtdate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtdesc As common.Controls.MyTextBox
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents lblRouteCode As common.Controls.MyLabel
    Friend WithEvents fndRouteCode As common.UserControls.txtFinder
End Class

