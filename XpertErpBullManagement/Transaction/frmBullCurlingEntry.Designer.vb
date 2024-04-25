Imports XpertERPEngineFine
Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBullCurlingEntry
    'Inherits System.Windows.Forms.Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBullCurlingEntry))
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.UsLock1 = New common.usLock()
        Me.lblDate = New common.Controls.MyLabel()
        Me.dtpDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fndCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.txtremarks = New common.Controls.MyTextBox()
        Me.lblName = New common.Controls.MyLabel()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtremarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(800, 20)
        Me.RadMenu1.TabIndex = 69
        Me.RadMenu1.Visible = False
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.UseCompatibleTextRendering = False
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export"
        Me.RadMenuItem2.UseCompatibleTextRendering = False
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Import"
        Me.RadMenuItem4.UseCompatibleTextRendering = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gv1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtremarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblName)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 430)
        Me.SplitContainer1.SplitterDistance = 395
        Me.SplitContainer1.TabIndex = 70
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(514, 15)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(101, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 76
        '
        'lblDate
        '
        Me.lblDate.FieldName = Nothing
        Me.lblDate.Location = New System.Drawing.Point(369, 16)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 18)
        Me.lblDate.TabIndex = 75
        Me.lblDate.Text = "Date"
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(408, 15)
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.Size = New System.Drawing.Size(85, 20)
        Me.dtpDate.TabIndex = 74
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "23/04/2024"
        Me.dtpDate.Value = New Date(2024, 4, 23, 0, 0, 0, 0)
        '
        'fndCode
        '
        Me.fndCode.FieldName = Nothing
        Me.fndCode.Location = New System.Drawing.Point(77, 16)
        Me.fndCode.MendatroryField = True
        Me.fndCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCode.MyLinkLable1 = Me.lblCode
        Me.fndCode.MyLinkLable2 = Nothing
        Me.fndCode.MyMaxLength = 30
        Me.fndCode.MyReadOnly = False
        Me.fndCode.Name = "fndCode"
        Me.fndCode.Size = New System.Drawing.Size(253, 21)
        Me.fndCode.TabIndex = 66
        Me.fndCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(12, 18)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(33, 16)
        Me.lblCode.TabIndex = 69
        Me.lblCode.Text = "Code"
        '
        'gv1
        '
        Me.gv1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(3, 103)
        '
        '
        '
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(794, 289)
        Me.gv1.TabIndex = 68
        Me.gv1.TabStop = False
        '
        'txtremarks
        '
        Me.txtremarks.CalculationExpression = Nothing
        Me.txtremarks.FieldCode = Nothing
        Me.txtremarks.FieldDesc = Nothing
        Me.txtremarks.FieldMaxLength = 0
        Me.txtremarks.FieldName = Nothing
        Me.txtremarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtremarks.isCalculatedField = False
        Me.txtremarks.IsSourceFromTable = False
        Me.txtremarks.IsSourceFromValueList = False
        Me.txtremarks.IsUnique = False
        Me.txtremarks.Location = New System.Drawing.Point(77, 40)
        Me.txtremarks.MaxLength = 200
        Me.txtremarks.MendatroryField = True
        Me.txtremarks.MyLinkLable1 = Me.lblName
        Me.txtremarks.MyLinkLable2 = Nothing
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.ReferenceFieldDesc = Nothing
        Me.txtremarks.ReferenceFieldName = Nothing
        Me.txtremarks.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtremarks.RootElement.StretchVertically = True
        Me.txtremarks.Size = New System.Drawing.Size(269, 33)
        Me.txtremarks.TabIndex = 67
        '
        'lblName
        '
        Me.lblName.FieldName = Nothing
        Me.lblName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblName.Location = New System.Drawing.Point(12, 42)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(54, 16)
        Me.lblName.TabIndex = 68
        Me.lblName.Text = "Remarks"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnreset.Location = New System.Drawing.Point(229, 6)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(66, 18)
        Me.btnreset.TabIndex = 74
        Me.btnreset.Text = "Reset"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(157, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 73
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(12, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 70
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(85, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 71
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(714, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 72
        Me.btnclose.Text = "Close"
        '
        'btnNew
        '
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(329, 16)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(18, 21)
        Me.btnNew.TabIndex = 77
        '
        'frmBullCurlingEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmBullCurlingEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmBullCurlingEntry"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtremarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents fndCode As common.UserControls.txtNavigator
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents txtremarks As common.Controls.MyTextBox
    Friend WithEvents lblName As common.Controls.MyLabel
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents dtpDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
End Class
