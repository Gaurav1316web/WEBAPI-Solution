<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDCSSupervisorTagging
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtmccname = New common.Controls.MyLabel()
        Me.txtmcccode = New common.UserControls.txtFinder()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvVLC = New common.UserControls.MyRadGridView()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.MenuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtmccname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gvVLC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvVLC.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(932, 425)
        Me.SplitContainer1.SplitterDistance = 386
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(3, 3)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(926, 380)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(905, 332)
        Me.RadPageViewPage1.Text = "Detail"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtmccname)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtmcccode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnnew)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer2.Size = New System.Drawing.Size(905, 332)
        Me.SplitContainer2.SplitterDistance = 34
        Me.SplitContainer2.TabIndex = 0
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(5, 7)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel3.TabIndex = 69
        Me.MyLabel3.Text = "MCC Code"
        '
        'txtmccname
        '
        Me.txtmccname.AutoSize = False
        Me.txtmccname.BorderVisible = True
        Me.txtmccname.FieldName = Nothing
        Me.txtmccname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmccname.Location = New System.Drawing.Point(248, 6)
        Me.txtmccname.Name = "txtmccname"
        Me.txtmccname.Size = New System.Drawing.Size(239, 18)
        Me.txtmccname.TabIndex = 70
        Me.txtmccname.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtmccname.TextWrap = False
        '
        'txtmcccode
        '
        Me.txtmcccode.CalculationExpression = Nothing
        Me.txtmcccode.FieldCode = Nothing
        Me.txtmcccode.FieldDesc = Nothing
        Me.txtmcccode.FieldMaxLength = 0
        Me.txtmcccode.FieldName = Nothing
        Me.txtmcccode.isCalculatedField = False
        Me.txtmcccode.IsSourceFromTable = False
        Me.txtmcccode.IsSourceFromValueList = False
        Me.txtmcccode.IsUnique = False
        Me.txtmcccode.Location = New System.Drawing.Point(106, 6)
        Me.txtmcccode.MendatroryField = True
        Me.txtmcccode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmcccode.MyLinkLable1 = Me.MyLabel3
        Me.txtmcccode.MyLinkLable2 = Me.txtmccname
        Me.txtmcccode.MyReadOnly = False
        Me.txtmcccode.MyShowMasterFormButton = False
        Me.txtmcccode.Name = "txtmcccode"
        Me.txtmcccode.ReferenceFieldDesc = Nothing
        Me.txtmcccode.ReferenceFieldName = Nothing
        Me.txtmcccode.ReferenceTableName = Nothing
        Me.txtmcccode.Size = New System.Drawing.Size(137, 18)
        Me.txtmcccode.TabIndex = 1
        Me.txtmcccode.Value = ""
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(494, 4)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(20, 21)
        Me.btnnew.TabIndex = 0
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gvVLC)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.HeaderText = "DCS Detail"
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(899, 288)
        Me.RadGroupBox2.TabIndex = 0
        Me.RadGroupBox2.Text = "DCS Detail"
        '
        'gvVLC
        '
        Me.gvVLC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvVLC.Location = New System.Drawing.Point(2, 18)
        '
        'gvVLC
        '
        Me.gvVLC.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvVLC.Name = "gvVLC"
        Me.gvVLC.ShowHeaderCellButtons = True
        Me.gvVLC.Size = New System.Drawing.Size(895, 268)
        Me.gvVLC.TabIndex = 4
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(848, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(88, 9)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(7, 9)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'MenuClose
        '
        Me.MenuClose.AccessibleDescription = "File"
        Me.MenuClose.AccessibleName = "File"
        Me.MenuClose.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexport, Me.btnimport})
        Me.MenuClose.Name = "MenuClose"
        Me.MenuClose.Text = "File"
        '
        'btnexport
        '
        Me.btnexport.AccessibleDescription = "Export"
        Me.btnexport.AccessibleName = "Export"
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Text = "Export"
        '
        'btnimport
        '
        Me.btnimport.AccessibleDescription = "Import"
        Me.btnimport.AccessibleName = "Import"
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Text = "Import"
        '
        'frmDCSSupervisorTagging
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(932, 445)
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuClose})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(932, 20)
        Me.RadMenu1.TabIndex = 12
        Me.RadMenu1.Text = "RadMenu1"
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmDCSSupervisorTagging"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmDCSSupervisorTagging"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtmccname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gvVLC.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvVLC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents MenuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtmcccode As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtmccname As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvVLC As common.UserControls.MyRadGridView
    Friend WithEvents RadMenu1 As RadMenu
End Class

