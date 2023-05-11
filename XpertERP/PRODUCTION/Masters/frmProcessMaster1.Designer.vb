<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProcessMaster1
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
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.RMImport = New Telerik.WinControls.UI.RadMenuItem
        Me.RMHeadImport = New Telerik.WinControls.UI.RadMenuItem
        Me.RMExport = New Telerik.WinControls.UI.RadMenuItem
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.PJCExpense = New Telerik.WinControls.UI.RadPageViewPage
        Me.txtProcessNo = New common.UserControls.txtFinder
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.fndEmployee = New common.UserControls.txtFinder
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.txtReference = New common.Controls.MyTextBox
        Me.RadLabel6 = New common.Controls.MyLabel
        Me.txtTotal = New common.Controls.MyTextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.RMHeadExport = New Telerik.WinControls.UI.RadMenuItem
        Me.RMDetailExport = New Telerik.WinControls.UI.RadMenuItem
        Me.RMDetailImport = New Telerik.WinControls.UI.RadMenuItem
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.PJCExpense.SuspendLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadLabel1.SuspendLayout()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReference, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(80, 2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(674, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(749, 435)
        Me.SplitContainer1.SplitterDistance = 403
        Me.SplitContainer1.TabIndex = 0
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Size = New System.Drawing.Size(749, 403)
        Me.SplitContainer2.SplitterDistance = 25
        Me.SplitContainer2.TabIndex = 0
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(749, 20)
        Me.RadMenu1.TabIndex = 321
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMImport, Me.RMExport})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RMImport
        '
        Me.RMImport.AccessibleDescription = "Import"
        Me.RMImport.AccessibleName = "Import"
        Me.RMImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMHeadImport, Me.RMDetailImport})
        Me.RMImport.Name = "RMImport"
        Me.RMImport.Text = "Import"
        Me.RMImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RMHeadImport
        '
        Me.RMHeadImport.AccessibleDescription = "Head"
        Me.RMHeadImport.AccessibleName = "Head"
        Me.RMHeadImport.Name = "RMHeadImport"
        Me.RMHeadImport.Text = "Head"
        Me.RMHeadImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RMExport
        '
        Me.RMExport.AccessibleDescription = "Export"
        Me.RMExport.AccessibleName = "Export"
        Me.RMExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMHeadExport, Me.RMDetailExport})
        Me.RMExport.Name = "RMExport"
        Me.RMExport.Text = "Export"
        Me.RMExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.PJCExpense)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.PJCExpense
        Me.RadPageView1.Size = New System.Drawing.Size(749, 374)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'PJCExpense
        '
        Me.PJCExpense.Controls.Add(Me.txtProcessNo)
        Me.PJCExpense.Controls.Add(Me.RadLabel1)
        Me.PJCExpense.Controls.Add(Me.btnAddNew)
        Me.PJCExpense.Controls.Add(Me.RadGroupBox2)
        Me.PJCExpense.Controls.Add(Me.txtReference)
        Me.PJCExpense.Controls.Add(Me.txtTotal)
        Me.PJCExpense.Controls.Add(Me.RadLabel6)
        Me.PJCExpense.Location = New System.Drawing.Point(10, 37)
        Me.PJCExpense.Name = "PJCExpense"
        Me.PJCExpense.Size = New System.Drawing.Size(728, 326)
        Me.PJCExpense.Text = "Process Details"
        '
        'txtProcessNo
        '
        Me.txtProcessNo.Location = New System.Drawing.Point(87, 5)
        Me.txtProcessNo.MendatroryField = False
        Me.txtProcessNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProcessNo.MyLinkLable1 = Nothing
        Me.txtProcessNo.MyLinkLable2 = Nothing
        Me.txtProcessNo.MyReadOnly = False
        Me.txtProcessNo.Name = "txtProcessNo"
        Me.txtProcessNo.Size = New System.Drawing.Size(247, 20)
        Me.txtProcessNo.TabIndex = 0
        Me.txtProcessNo.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Controls.Add(Me.fndEmployee)
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(3, 7)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(77, 16)
        Me.RadLabel1.TabIndex = 5
        Me.RadLabel1.Text = "Process Code"
        '
        'fndEmployee
        '
        Me.fndEmployee.Location = New System.Drawing.Point(97, -2)
        Me.fndEmployee.MendatroryField = False
        Me.fndEmployee.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndEmployee.MyLinkLable1 = Nothing
        Me.fndEmployee.MyLinkLable2 = Nothing
        Me.fndEmployee.MyReadOnly = False
        Me.fndEmployee.Name = "fndEmployee"
        Me.fndEmployee.Size = New System.Drawing.Size(143, 20)
        Me.fndEmployee.TabIndex = 0
        Me.fndEmployee.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(340, 5)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Process Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 54)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(725, 270)
        Me.RadGroupBox2.TabIndex = 3
        Me.RadGroupBox2.Text = "Process Details"
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
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.Size = New System.Drawing.Size(705, 240)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'txtReference
        '
        Me.txtReference.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReference.Location = New System.Drawing.Point(87, 30)
        Me.txtReference.MaxLength = 200
        Me.txtReference.MendatroryField = False
        Me.txtReference.MyLinkLable1 = Me.RadLabel6
        Me.txtReference.MyLinkLable2 = Nothing
        Me.txtReference.Name = "txtReference"
        Me.txtReference.Size = New System.Drawing.Size(541, 18)
        Me.txtReference.TabIndex = 2
        '
        'RadLabel6
        '
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(2, 29)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(76, 16)
        Me.RadLabel6.TabIndex = 4
        Me.RadLabel6.Text = "Process Desc"
        '
        'txtTotal
        '
        Me.txtTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.Location = New System.Drawing.Point(752, 112)
        Me.txtTotal.MaxLength = 10
        Me.txtTotal.MendatroryField = False
        Me.txtTotal.MyLinkLable1 = Nothing
        Me.txtTotal.MyLinkLable2 = Nothing
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(139, 18)
        Me.txtTotal.TabIndex = 39
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(749, 435)
        Me.Panel1.TabIndex = 7
        '
        'RMHeadExport
        '
        Me.RMHeadExport.AccessibleDescription = "Head"
        Me.RMHeadExport.AccessibleName = "Head"
        Me.RMHeadExport.Name = "RMHeadExport"
        Me.RMHeadExport.Text = "Head"
        Me.RMHeadExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RMDetailExport
        '
        Me.RMDetailExport.AccessibleDescription = "Detail"
        Me.RMDetailExport.AccessibleName = "Detail"
        Me.RMDetailExport.Name = "RMDetailExport"
        Me.RMDetailExport.Text = "Detail"
        Me.RMDetailExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RMDetailImport
        '
        Me.RMDetailImport.AccessibleDescription = "Detail"
        Me.RMDetailImport.AccessibleName = "Detail"
        Me.RMDetailImport.Name = "RMDetailImport"
        Me.RMDetailImport.Text = "Detail"
        Me.RMDetailImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'FrmProcessMaster1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(749, 435)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FrmProcessMaster1"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Process Master"
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.PJCExpense.ResumeLayout(False)
        Me.PJCExpense.PerformLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadLabel1.ResumeLayout(False)
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReference, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents PJCExpense As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtProcessNo As common.UserControls.txtFinder
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents fndEmployee As common.UserControls.txtFinder
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents txtReference As common.Controls.MyTextBox
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents txtTotal As common.Controls.MyTextBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMHeadImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMHeadExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMDetailExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMDetailImport As Telerik.WinControls.UI.RadMenuItem
End Class

