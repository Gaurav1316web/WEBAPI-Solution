Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEmployeeOTEntry
    'Inherits System.Windows.Forms.Form
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
        Me.gbStatus = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtndouble = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnSingle = New Telerik.WinControls.UI.RadRadioButton()
        Me.UsLock1 = New common.usLock()
        Me.chkDCS = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.lblPayPeriodDesc = New common.Controls.MyLabel()
        Me.lblAbandonmentNo = New common.Controls.MyLabel()
        Me.lblTankerNo = New common.Controls.MyLabel()
        Me.txtPayPeriod = New common.UserControls.txtFinder()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.gbStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbStatus.SuspendLayout()
        CType(Me.rbtndouble, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSingle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDCS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriodDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAbandonmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 411
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(800, 411)
        Me.RadPageView1.TabIndex = 2
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.gbStatus)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.chkDCS)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblPayPeriodDesc)
        Me.RadPageViewPage1.Controls.Add(Me.lblAbandonmentNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblTankerNo)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.txtPayPeriod)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(66.0!, 22.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 31)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 369)
        Me.RadPageViewPage1.Text = "OT Details"
        '
        'gbStatus
        '
        Me.gbStatus.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbStatus.Controls.Add(Me.rbtndouble)
        Me.gbStatus.Controls.Add(Me.rbtnSingle)
        Me.gbStatus.HeaderText = ""
        Me.gbStatus.Location = New System.Drawing.Point(447, 34)
        Me.gbStatus.Name = "gbStatus"
        Me.gbStatus.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbStatus.Size = New System.Drawing.Size(133, 35)
        Me.gbStatus.TabIndex = 1556
        '
        'rbtndouble
        '
        Me.rbtndouble.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtndouble.Location = New System.Drawing.Point(60, 7)
        Me.rbtndouble.Name = "rbtndouble"
        Me.rbtndouble.Size = New System.Drawing.Size(56, 18)
        Me.rbtndouble.TabIndex = 1
        Me.rbtndouble.Text = "Double"
        Me.rbtndouble.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnSingle
        '
        Me.rbtnSingle.Location = New System.Drawing.Point(3, 7)
        Me.rbtnSingle.Name = "rbtnSingle"
        Me.rbtnSingle.Size = New System.Drawing.Size(51, 18)
        Me.rbtnSingle.TabIndex = 0
        Me.rbtnSingle.TabStop = False
        Me.rbtnSingle.Text = "Single"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(506, -2)
        Me.UsLock1.Margin = New System.Windows.Forms.Padding(4)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(118, 22)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1555
        '
        'chkDCS
        '
        Me.chkDCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDCS.Location = New System.Drawing.Point(802, 1)
        Me.chkDCS.Name = "chkDCS"
        Me.chkDCS.Size = New System.Drawing.Size(44, 16)
        Me.chkDCS.TabIndex = 1484
        Me.chkDCS.Text = "DCS"
        Me.chkDCS.Visible = False
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Employee OT Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 75)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(774, 280)
        Me.RadGroupBox2.TabIndex = 28
        Me.RadGroupBox2.Text = "Employee OT Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyExportAPI = False
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(754, 250)
        Me.gv1.TabIndex = 17
        Me.gv1.TabStop = False
        Me.gv1.VarID = ""
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
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
        Me.txtDate.Location = New System.Drawing.Point(373, 2)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(127, 18)
        Me.txtDate.TabIndex = 1524
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(341, 3)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 1
        Me.RadLabel4.Text = "Date"
        '
        'lblPayPeriodDesc
        '
        Me.lblPayPeriodDesc.AutoSize = False
        Me.lblPayPeriodDesc.BorderVisible = True
        Me.lblPayPeriodDesc.FieldName = Nothing
        Me.lblPayPeriodDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayPeriodDesc.Location = New System.Drawing.Point(199, 27)
        Me.lblPayPeriodDesc.Name = "lblPayPeriodDesc"
        Me.lblPayPeriodDesc.Size = New System.Drawing.Size(227, 19)
        Me.lblPayPeriodDesc.TabIndex = 144
        Me.lblPayPeriodDesc.TextWrap = False
        '
        'lblAbandonmentNo
        '
        Me.lblAbandonmentNo.FieldName = Nothing
        Me.lblAbandonmentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAbandonmentNo.Location = New System.Drawing.Point(764, 11)
        Me.lblAbandonmentNo.Name = "lblAbandonmentNo"
        Me.lblAbandonmentNo.Size = New System.Drawing.Size(2, 2)
        Me.lblAbandonmentNo.TabIndex = 27
        '
        'lblTankerNo
        '
        Me.lblTankerNo.FieldName = Nothing
        Me.lblTankerNo.Location = New System.Drawing.Point(-1, 25)
        Me.lblTankerNo.Name = "lblTankerNo"
        Me.lblTankerNo.Size = New System.Drawing.Size(59, 18)
        Me.lblTankerNo.TabIndex = 119
        Me.lblTankerNo.Text = "Pay Period"
        '
        'txtPayPeriod
        '
        Me.txtPayPeriod.CalculationExpression = Nothing
        Me.txtPayPeriod.FieldCode = Nothing
        Me.txtPayPeriod.FieldDesc = Nothing
        Me.txtPayPeriod.FieldMaxLength = 0
        Me.txtPayPeriod.FieldName = Nothing
        Me.txtPayPeriod.isCalculatedField = False
        Me.txtPayPeriod.IsSourceFromTable = False
        Me.txtPayPeriod.IsSourceFromValueList = False
        Me.txtPayPeriod.IsUnique = False
        Me.txtPayPeriod.Location = New System.Drawing.Point(78, 27)
        Me.txtPayPeriod.MendatroryField = False
        Me.txtPayPeriod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayPeriod.MyLinkLable1 = Me.lblTankerNo
        Me.txtPayPeriod.MyLinkLable2 = Nothing
        Me.txtPayPeriod.MyReadOnly = False
        Me.txtPayPeriod.MyShowMasterFormButton = False
        Me.txtPayPeriod.Name = "txtPayPeriod"
        Me.txtPayPeriod.ReferenceFieldDesc = Nothing
        Me.txtPayPeriod.ReferenceFieldName = Nothing
        Me.txtPayPeriod.ReferenceTableName = Nothing
        Me.txtPayPeriod.Size = New System.Drawing.Size(115, 19)
        Me.txtPayPeriod.TabIndex = 22
        Me.txtPayPeriod.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(-1, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel1.TabIndex = 22
        Me.RadLabel1.Text = "Code"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(46, 1)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(269, 22)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(698, 7)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(78, 22)
        Me.btnCancel.TabIndex = 163
        Me.btnCancel.Text = "Cancel"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(139, 7)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(60, 22)
        Me.btnPost.TabIndex = 25
        Me.btnPost.Text = "Post"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(73, 7)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.TabIndex = 24
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(10, 7)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(57, 22)
        Me.btnSave.TabIndex = 23
        Me.btnSave.Text = "Save"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(314, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'frmEmployeeOTEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmEmployeeOTEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmEmployeeOTEntry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.gbStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbStatus.ResumeLayout(False)
        Me.gbStatus.PerformLayout()
        CType(Me.rbtndouble, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSingle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDCS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriodDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAbandonmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents chkDCS As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents lblPayPeriodDesc As common.Controls.MyLabel
    Friend WithEvents lblAbandonmentNo As common.Controls.MyLabel
    Friend WithEvents lblTankerNo As common.Controls.MyLabel
    Friend WithEvents txtPayPeriod As common.UserControls.txtFinder
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
    Friend WithEvents gbStatus As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rbtndouble As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnSingle As Telerik.WinControls.UI.RadRadioButton
End Class
