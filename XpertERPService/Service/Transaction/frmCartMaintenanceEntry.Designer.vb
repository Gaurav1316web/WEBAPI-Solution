Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCartMaintenanceEntry
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.txtdesc = New common.Controls.MyTextBox()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.UsLock1 = New common.usLock()
        Me.txtdocno = New common.UserControls.txtNavigator()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btngo = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgCustomer = New common.UserControls.MyRadGridView()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.rdselect = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdall = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        CType(Me.cbgCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbgCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        CType(Me.rdselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.txtdesc.Location = New System.Drawing.Point(108, 33)
        Me.txtdesc.MaxLength = 50
        Me.txtdesc.MendatroryField = False
        Me.txtdesc.MyLinkLable1 = Me.MyLabel24
        Me.txtdesc.MyLinkLable2 = Nothing
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.ReferenceFieldDesc = Nothing
        Me.txtdesc.ReferenceFieldName = Nothing
        Me.txtdesc.ReferenceTableName = Nothing
        Me.txtdesc.Size = New System.Drawing.Size(403, 18)
        Me.txtdesc.TabIndex = 3
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel24.Location = New System.Drawing.Point(15, 33)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel24.TabIndex = 64
        Me.MyLabel24.Text = "Description"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(391, 11)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 62
        Me.RadLabel4.Text = "Date"
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
        Me.txtDate.Location = New System.Drawing.Point(424, 10)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(87, 18)
        Me.txtDate.TabIndex = 2
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "08/04/2014"
        Me.txtDate.Value = New Date(2014, 4, 8, 11, 28, 14, 0)
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(603, 6)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 65
        '
        'txtdocno
        '
        Me.txtdocno.FieldName = Nothing
        Me.txtdocno.Location = New System.Drawing.Point(108, 8)
        Me.txtdocno.MendatroryField = False
        Me.txtdocno.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtdocno.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtdocno.MyLinkLable1 = Me.RadLabel1
        Me.txtdocno.MyLinkLable2 = Nothing
        Me.txtdocno.MyMaxLength = 30
        Me.txtdocno.MyReadOnly = False
        Me.txtdocno.Name = "txtdocno"
        Me.txtdocno.Size = New System.Drawing.Size(252, 20)
        Me.txtdocno.TabIndex = 1
        Me.txtdocno.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(15, 10)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(79, 16)
        Me.RadLabel1.TabIndex = 1
        Me.RadLabel1.Text = "Document No."
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPService.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(362, 8)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(18, 21)
        Me.btnAddNew.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(6)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(6)
        Me.SplitContainer1.Size = New System.Drawing.Size(718, 639)
        Me.SplitContainer1.SplitterDistance = 599
        Me.SplitContainer1.TabIndex = 66
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(6, 6)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer2.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer2.Size = New System.Drawing.Size(706, 587)
        Me.SplitContainer2.SplitterDistance = 269
        Me.SplitContainer2.TabIndex = 1
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.btngo)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel24)
        Me.RadGroupBox1.Controls.Add(Me.txtDate)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox8)
        Me.RadGroupBox1.Controls.Add(Me.btnAddNew)
        Me.RadGroupBox1.Controls.Add(Me.txtdocno)
        Me.RadGroupBox1.Controls.Add(Me.txtdesc)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(582, 260)
        Me.RadGroupBox1.TabIndex = 0
        '
        'btngo
        '
        Me.btngo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btngo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btngo.Location = New System.Drawing.Point(512, 230)
        Me.btngo.Name = "btngo"
        Me.btngo.Size = New System.Drawing.Size(65, 22)
        Me.btngo.TabIndex = 4
        Me.btngo.Text = ">>"
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox8.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox8.Controls.Add(Me.Panel7)
        Me.RadGroupBox8.HeaderText = "Customer"
        Me.RadGroupBox8.Location = New System.Drawing.Point(15, 56)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox8.Size = New System.Drawing.Size(496, 196)
        Me.RadGroupBox8.TabIndex = 313
        Me.RadGroupBox8.TabStop = False
        Me.RadGroupBox8.Text = "Customer"
        '
        'cbgCustomer
        '
        Me.cbgCustomer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbgCustomer.Location = New System.Drawing.Point(10, 40)
        '
        '
        '
        Me.cbgCustomer.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.cbgCustomer.MasterTemplate.ShowHeaderCellButtons = True
        Me.cbgCustomer.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.cbgCustomer.MyStopExport = False
        Me.cbgCustomer.Name = "cbgCustomer"
        Me.cbgCustomer.ShowHeaderCellButtons = True
        Me.cbgCustomer.Size = New System.Drawing.Size(476, 146)
        Me.cbgCustomer.TabIndex = 0
        Me.cbgCustomer.TabStop = False
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.rdselect)
        Me.Panel7.Controls.Add(Me.rdall)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(10, 20)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(476, 20)
        Me.Panel7.TabIndex = 0
        '
        'rdselect
        '
        Me.rdselect.Location = New System.Drawing.Point(338, 1)
        Me.rdselect.Name = "rdselect"
        Me.rdselect.Size = New System.Drawing.Size(50, 18)
        Me.rdselect.TabIndex = 1
        Me.rdselect.Text = "Select"
        '
        'rdall
        '
        Me.rdall.Location = New System.Drawing.Point(91, 1)
        Me.rdall.Name = "rdall"
        Me.rdall.Size = New System.Drawing.Size(33, 18)
        Me.rdall.TabIndex = 0
        Me.rdall.Text = "All"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.RadPageView1.Location = New System.Drawing.Point(3, 3)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.Padding = New System.Windows.Forms.Padding(5)
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(700, 308)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.TabStop = False
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).Padding = New System.Windows.Forms.Padding(5)
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.gv1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(104.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(680, 261)
        Me.RadPageViewPage1.Text = "Spare Parts Detail"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(680, 261)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(159, 9)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(7, 9)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(83, 9)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(640, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'FrmCartMaintenanceEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(718, 639)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCartMaintenanceEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cart Maintenance Entry"
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        CType(Me.cbgCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbgCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.rdselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtdesc As common.Controls.MyTextBox
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtdocno As common.UserControls.txtNavigator
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox8 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents rdselect As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdall As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btngo As Telerik.WinControls.UI.RadButton
    Friend WithEvents cbgCustomer As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
End Class

