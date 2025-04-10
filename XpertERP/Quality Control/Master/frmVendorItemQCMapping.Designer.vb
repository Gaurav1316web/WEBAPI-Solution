<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVendorItemQCMapping
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.MenuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.TxtVendor_desc = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndVendor_code = New common.UserControls.txtFinder()
        Me.lblvandorno = New common.Controls.MyLabel()
        Me.txtdesc = New common.Controls.MyTextBox()
        Me.lblvendorname = New common.Controls.MyLabel()
        Me.fndCode = New common.UserControls.txtNavigator()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtVendor_desc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvendorname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(762, 483)
        Me.SplitContainer1.SplitterDistance = 451
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(1, 21)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(760, 429)
        Me.RadPageView1.TabIndex = 1
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(143.0!, 24.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 33)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(739, 385)
        Me.RadPageViewPage1.Text = "QC Vendor-Item Mapping"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtVendor_desc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndVendor_code)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblvandorno)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtdesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblvendorname)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndCode)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(1)
        Me.SplitContainer2.Size = New System.Drawing.Size(739, 385)
        Me.SplitContainer2.SplitterDistance = 93
        Me.SplitContainer2.TabIndex = 0
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(381, 20)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 0
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(1, 1)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(1, 16, 1, 3)
        Me.RadGroupBox2.Size = New System.Drawing.Size(737, 286)
        Me.RadGroupBox2.TabIndex = 0
        Me.RadGroupBox2.Text = "Item Details"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(71.0!, 24.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(739, 381)
        Me.RadPageViewPage2.Text = "Attachment"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuClose})
        Me.RadMenu1.Location = New System.Drawing.Point(1, 1)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(760, 20)
        Me.RadMenu1.TabIndex = 17
        '
        'MenuClose
        '
        Me.MenuClose.Name = "MenuClose"
        Me.MenuClose.Text = "File"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(597, 3)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(78, 21)
        Me.btnHistory.TabIndex = 3
        Me.btnHistory.Text = "History"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(681, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(78, 21)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(87, 3)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(78, 21)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(3, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(78, 21)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(438, 20)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MyLinkLable1 = Me.MyLabel1
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(80, 20)
        Me.txtDate.TabIndex = 1
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "19/02/2015"
        Me.txtDate.Value = New Date(2015, 2, 19, 16, 6, 50, 663)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(402, 23)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel1.TabIndex = 48
        Me.MyLabel1.Text = "Date"
        '
        'TxtVendor_desc
        '
        Me.TxtVendor_desc.AutoSize = False
        Me.TxtVendor_desc.BorderVisible = True
        Me.TxtVendor_desc.FieldName = Nothing
        Me.TxtVendor_desc.Location = New System.Drawing.Point(249, 66)
        Me.TxtVendor_desc.Name = "TxtVendor_desc"
        Me.TxtVendor_desc.Size = New System.Drawing.Size(269, 19)
        Me.TxtVendor_desc.TabIndex = 46
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(11, 67)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel2.TabIndex = 47
        Me.MyLabel2.Text = "Vendor"
        '
        'fndVendor_code
        '
        Me.fndVendor_code.CalculationExpression = Nothing
        Me.fndVendor_code.FieldCode = Nothing
        Me.fndVendor_code.FieldDesc = Nothing
        Me.fndVendor_code.FieldMaxLength = 0
        Me.fndVendor_code.FieldName = Nothing
        Me.fndVendor_code.isCalculatedField = False
        Me.fndVendor_code.IsSourceFromTable = False
        Me.fndVendor_code.IsSourceFromValueList = False
        Me.fndVendor_code.IsUnique = False
        Me.fndVendor_code.Location = New System.Drawing.Point(105, 66)
        Me.fndVendor_code.MendatroryField = True
        Me.fndVendor_code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVendor_code.MyLinkLable1 = Me.MyLabel2
        Me.fndVendor_code.MyLinkLable2 = Me.TxtVendor_desc
        Me.fndVendor_code.MyReadOnly = False
        Me.fndVendor_code.MyShowMasterFormButton = False
        Me.fndVendor_code.Name = "fndVendor_code"
        Me.fndVendor_code.ReferenceFieldDesc = Nothing
        Me.fndVendor_code.ReferenceFieldName = Nothing
        Me.fndVendor_code.ReferenceTableName = Nothing
        Me.fndVendor_code.Size = New System.Drawing.Size(142, 19)
        Me.fndVendor_code.TabIndex = 3
        Me.fndVendor_code.Value = ""
        '
        'lblvandorno
        '
        Me.lblvandorno.FieldName = Nothing
        Me.lblvandorno.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblvandorno.Location = New System.Drawing.Point(11, 21)
        Me.lblvandorno.Name = "lblvandorno"
        Me.lblvandorno.Size = New System.Drawing.Size(88, 16)
        Me.lblvandorno.TabIndex = 37
        Me.lblvandorno.Text = "Document Code"
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
        Me.txtdesc.Location = New System.Drawing.Point(105, 44)
        Me.txtdesc.MaxLength = 200
        Me.txtdesc.MendatroryField = False
        Me.txtdesc.MyLinkLable1 = Me.lblvendorname
        Me.txtdesc.MyLinkLable2 = Nothing
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.ReferenceFieldDesc = Nothing
        Me.txtdesc.ReferenceFieldName = Nothing
        Me.txtdesc.ReferenceTableName = Nothing
        Me.txtdesc.Size = New System.Drawing.Size(413, 18)
        Me.txtdesc.TabIndex = 2
        Me.txtdesc.TabStop = False
        '
        'lblvendorname
        '
        Me.lblvendorname.FieldName = Nothing
        Me.lblvendorname.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblvendorname.Location = New System.Drawing.Point(11, 43)
        Me.lblvendorname.Name = "lblvendorname"
        Me.lblvendorname.Size = New System.Drawing.Size(63, 16)
        Me.lblvendorname.TabIndex = 41
        Me.lblvendorname.Text = "Description"
        '
        'fndCode
        '
        Me.fndCode.FieldName = Nothing
        Me.fndCode.Location = New System.Drawing.Point(105, 19)
        Me.fndCode.MendatroryField = True
        Me.fndCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCode.MyLinkLable1 = Me.lblvandorno
        Me.fndCode.MyLinkLable2 = Nothing
        Me.fndCode.MyMaxLength = 32767
        Me.fndCode.MyReadOnly = False
        Me.fndCode.Name = "fndCode"
        Me.fndCode.Size = New System.Drawing.Size(276, 21)
        Me.fndCode.TabIndex = 39
        Me.fndCode.TabStop = False
        Me.fndCode.Value = ""
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(1, 16)
        '
        '
        '
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(735, 267)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.VarID = ""
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(739, 381)
        Me.UcAttachment1.TabIndex = 2
        '
        'FrmVendorItemQCMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(762, 483)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmVendorItemQCMapping"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmVendorItemQCMapping"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtVendor_desc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvendorname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents MenuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents TxtVendor_desc As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents fndVendor_code As common.UserControls.txtFinder
    Friend WithEvents lblvandorno As common.Controls.MyLabel
    Friend WithEvents txtdesc As common.Controls.MyTextBox
    Friend WithEvents lblvendorname As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndCode As common.UserControls.txtNavigator
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents btnHistory As RadButton
End Class

