<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMilkCollectionMCCQC
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnCorrection = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnVerify = New Telerik.WinControls.UI.RadButton()
        Me.btnBrowse = New Telerik.WinControls.UI.RadButton()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView2 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnAllQCDone = New common.Controls.MyRadioButton()
        Me.rbtnPending = New common.Controls.MyRadioButton()
        Me.rbtnAll = New common.Controls.MyRadioButton()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtDateReport = New common.Controls.MyDateTimePicker()
        Me.RadButton3 = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnCorrection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnVerify, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadPageView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView2.SuspendLayout()
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnAllQCDone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnPending, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDateReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.gv1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel2Collapsed = True
        Me.SplitContainer1.Size = New System.Drawing.Size(738, 425)
        Me.SplitContainer1.SplitterDistance = 400
        Me.SplitContainer1.TabIndex = 0
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 32)
        '
        '
        '
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(738, 393)
        Me.gv1.TabIndex = 1
        Me.gv1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnCorrection)
        Me.Panel1.Controls.Add(Me.RadLabel4)
        Me.Panel1.Controls.Add(Me.txtDate)
        Me.Panel1.Controls.Add(Me.MyLabel8)
        Me.Panel1.Controls.Add(Me.MyLabel4)
        Me.Panel1.Controls.Add(Me.MyLabel10)
        Me.Panel1.Controls.Add(Me.MyLabel7)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.btnVerify)
        Me.Panel1.Controls.Add(Me.btnBrowse)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(738, 32)
        Me.Panel1.TabIndex = 1
        '
        'btnCorrection
        '
        Me.btnCorrection.Location = New System.Drawing.Point(418, 6)
        Me.btnCorrection.Name = "btnCorrection"
        Me.btnCorrection.Size = New System.Drawing.Size(86, 20)
        Me.btnCorrection.TabIndex = 4
        Me.btnCorrection.Text = "Correction"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(6, 8)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 74
        Me.RadLabel4.Text = "Date"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.Enabled = False
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
        Me.txtDate.Location = New System.Drawing.Point(45, 7)
        Me.txtDate.MendatroryField = True
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(82, 18)
        Me.txtDate.TabIndex = 73
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel8
        '
        Me.MyLabel8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel8.AutoSize = False
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(535, 7)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(46, 18)
        Me.MyLabel8.TabIndex = 71
        Me.MyLabel8.Text = "Correct"
        Me.MyLabel8.TextWrap = False
        '
        'MyLabel4
        '
        Me.MyLabel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel4.AutoSize = False
        Me.MyLabel4.BackColor = System.Drawing.Color.LightGreen
        Me.MyLabel4.BorderVisible = True
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.MyLabel4.Location = New System.Drawing.Point(517, 7)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(17, 18)
        Me.MyLabel4.TabIndex = 69
        Me.MyLabel4.TextWrap = False
        '
        'MyLabel10
        '
        Me.MyLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel10.AutoSize = False
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(607, 7)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(38, 18)
        Me.MyLabel10.TabIndex = 72
        Me.MyLabel10.Text = "Error"
        Me.MyLabel10.TextWrap = False
        '
        'MyLabel7
        '
        Me.MyLabel7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel7.AutoSize = False
        Me.MyLabel7.BackColor = System.Drawing.Color.MistyRose
        Me.MyLabel7.BorderVisible = True
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.MyLabel7.Location = New System.Drawing.Point(588, 7)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(17, 18)
        Me.MyLabel7.TabIndex = 70
        Me.MyLabel7.TextWrap = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(648, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(86, 20)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(330, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(86, 20)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        '
        'btnVerify
        '
        Me.btnVerify.Location = New System.Drawing.Point(281, 6)
        Me.btnVerify.Name = "btnVerify"
        Me.btnVerify.Size = New System.Drawing.Size(47, 20)
        Me.btnVerify.TabIndex = 2
        Me.btnVerify.Text = ">>"
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(129, 6)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(150, 20)
        Me.btnBrowse.TabIndex = 1
        Me.btnBrowse.Text = "Browse excel Sheet"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(759, 473)
        Me.RadPageView1.TabIndex = 1
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(85.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(738, 425)
        Me.RadPageViewPage1.Text = "Fill QC Details"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(74.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(738, 425)
        Me.RadPageViewPage2.Text = "View Status"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadPageView2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Panel2)
        Me.SplitContainer2.Panel2Collapsed = True
        Me.SplitContainer2.Size = New System.Drawing.Size(738, 425)
        Me.SplitContainer2.SplitterDistance = 400
        Me.SplitContainer2.TabIndex = 1
        '
        'RadPageView2
        '
        Me.RadPageView2.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView2.Controls.Add(Me.Attachments)
        Me.RadPageView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView2.Location = New System.Drawing.Point(0, 38)
        Me.RadPageView2.Name = "RadPageView2"
        Me.RadPageView2.SelectedPage = Me.RadPageViewPage5
        Me.RadPageView2.Size = New System.Drawing.Size(738, 387)
        Me.RadPageView2.TabIndex = 2
        CType(Me.RadPageView2.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.gv2)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(717, 339)
        Me.RadPageViewPage5.Text = "Report"
        '
        'gv2
        '
        Me.gv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv2.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv2.Name = "gv2"
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(717, 339)
        Me.gv2.TabIndex = 1
        Me.gv2.TabStop = False
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 28.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 37)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(717, 339)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(717, 339)
        Me.UcAttachment1.TabIndex = 1
        Me.UcAttachment1.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Controls.Add(Me.MyLabel1)
        Me.Panel2.Controls.Add(Me.txtDateReport)
        Me.Panel2.Controls.Add(Me.RadButton3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(738, 38)
        Me.Panel2.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnAllQCDone)
        Me.GroupBox1.Controls.Add(Me.rbtnPending)
        Me.GroupBox1.Controls.Add(Me.rbtnAll)
        Me.GroupBox1.Location = New System.Drawing.Point(133, -2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(182, 36)
        Me.GroupBox1.TabIndex = 75
        Me.GroupBox1.TabStop = False
        '
        'rbtnAllQCDone
        '
        Me.rbtnAllQCDone.Location = New System.Drawing.Point(113, 13)
        Me.rbtnAllQCDone.MyLinkLable1 = Nothing
        Me.rbtnAllQCDone.MyLinkLable2 = Nothing
        Me.rbtnAllQCDone.Name = "rbtnAllQCDone"
        Me.rbtnAllQCDone.Size = New System.Drawing.Size(66, 18)
        Me.rbtnAllQCDone.TabIndex = 2
        Me.rbtnAllQCDone.TabStop = False
        Me.rbtnAllQCDone.Text = "QC Done"
        '
        'rbtnPending
        '
        Me.rbtnPending.Location = New System.Drawing.Point(46, 13)
        Me.rbtnPending.MyLinkLable1 = Nothing
        Me.rbtnPending.MyLinkLable2 = Nothing
        Me.rbtnPending.Name = "rbtnPending"
        Me.rbtnPending.Size = New System.Drawing.Size(61, 18)
        Me.rbtnPending.TabIndex = 1
        Me.rbtnPending.TabStop = False
        Me.rbtnPending.Text = "Pending"
        '
        'rbtnAll
        '
        Me.rbtnAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnAll.Location = New System.Drawing.Point(7, 13)
        Me.rbtnAll.MyLinkLable1 = Nothing
        Me.rbtnAll.MyLinkLable2 = Nothing
        Me.rbtnAll.Name = "rbtnAll"
        Me.rbtnAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnAll.TabIndex = 0
        Me.rbtnAll.Text = "All"
        Me.rbtnAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 11)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel1.TabIndex = 74
        Me.MyLabel1.Text = "Date"
        '
        'txtDateReport
        '
        Me.txtDateReport.CalculationExpression = Nothing
        Me.txtDateReport.CustomFormat = "dd/MM/yyyy"
        Me.txtDateReport.FieldCode = Nothing
        Me.txtDateReport.FieldDesc = Nothing
        Me.txtDateReport.FieldMaxLength = 0
        Me.txtDateReport.FieldName = Nothing
        Me.txtDateReport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateReport.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDateReport.isCalculatedField = False
        Me.txtDateReport.IsSourceFromTable = False
        Me.txtDateReport.IsSourceFromValueList = False
        Me.txtDateReport.IsUnique = False
        Me.txtDateReport.Location = New System.Drawing.Point(45, 10)
        Me.txtDateReport.MendatroryField = True
        Me.txtDateReport.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDateReport.MyLinkLable1 = Me.MyLabel1
        Me.txtDateReport.MyLinkLable2 = Nothing
        Me.txtDateReport.Name = "txtDateReport"
        Me.txtDateReport.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDateReport.ReferenceFieldDesc = Nothing
        Me.txtDateReport.ReferenceFieldName = Nothing
        Me.txtDateReport.ReferenceTableName = Nothing
        Me.txtDateReport.Size = New System.Drawing.Size(82, 18)
        Me.txtDateReport.TabIndex = 73
        Me.txtDateReport.TabStop = False
        Me.txtDateReport.Text = "13/06/2011"
        Me.txtDateReport.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadButton3
        '
        Me.RadButton3.Location = New System.Drawing.Point(318, 8)
        Me.RadButton3.Name = "RadButton3"
        Me.RadButton3.Size = New System.Drawing.Size(47, 23)
        Me.RadButton3.TabIndex = 2
        Me.RadButton3.Text = ">>"
        '
        'frmMilkCollectionMCCQC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(759, 473)
        Me.Controls.Add(Me.RadPageView1)
        Me.MinimumSize = New System.Drawing.Size(767, 503)
        Me.Name = "frmMilkCollectionMCCQC"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.RootElement.MaxSize = New System.Drawing.Size(0, 0)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Prefix Import"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.btnCorrection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnVerify, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadPageView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView2.ResumeLayout(False)
        Me.RadPageViewPage5.ResumeLayout(False)
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnAllQCDone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnPending, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDateReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnBrowse As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnVerify As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDateReport As common.Controls.MyDateTimePicker
    Friend WithEvents RadButton3 As RadButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rbtnAllQCDone As common.Controls.MyRadioButton
    Friend WithEvents rbtnPending As common.Controls.MyRadioButton
    Friend WithEvents rbtnAll As common.Controls.MyRadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents RadPageView2 As RadPageView
    Friend WithEvents RadPageViewPage5 As RadPageViewPage
    Friend WithEvents Attachments As RadPageViewPage
    Friend WithEvents UcAttachment1 As ucAttachment
    Friend WithEvents Panel2 As Panel
    Friend WithEvents btnCorrection As RadButton
End Class

