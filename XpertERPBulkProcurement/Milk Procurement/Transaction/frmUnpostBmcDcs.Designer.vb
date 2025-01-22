<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmUnpostBmcDcs
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.txtMultBmc = New common.UserControls.txtMultiSelectFinder()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.txtToDate1 = New common.Controls.MyDateTimePicker()
        Me.txtFromDate1 = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.txtToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtFromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMultBmc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblfromDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtToDate1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFromDate1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblToDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 399
        Me.SplitContainer1.TabIndex = 0
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(12, 30)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel1.TabIndex = 415
        Me.MyLabel1.Text = "BMC"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.gv1)
        Me.Panel1.Location = New System.Drawing.Point(3, 59)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(794, 337)
        Me.Panel1.TabIndex = 20
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.EnableAlternatingRowColor = True
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(794, 337)
        Me.gv1.TabIndex = 3
        Me.gv1.TabStop = False
        Me.gv1.VarID = ""
        '
        'txtMultBmc
        '
        Me.txtMultBmc.arrDispalyMember = Nothing
        Me.txtMultBmc.arrValueMember = Nothing
        Me.txtMultBmc.Location = New System.Drawing.Point(85, 30)
        Me.txtMultBmc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultBmc.MyLinkLable1 = Nothing
        Me.txtMultBmc.MyLinkLable2 = Nothing
        Me.txtMultBmc.MyNullText = "All"
        Me.txtMultBmc.Name = "txtMultBmc"
        Me.txtMultBmc.Size = New System.Drawing.Size(249, 19)
        Me.txtMultBmc.TabIndex = 414
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.Location = New System.Drawing.Point(12, 8)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblfromDate.TabIndex = 17
        Me.lblfromDate.Text = "From Date"
        '
        'btnGo
        '
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(343, 7)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(62, 42)
        Me.btnGo.TabIndex = 160
        Me.btnGo.Text = ">>>"
        '
        'txtToDate1
        '
        Me.txtToDate1.CalculationExpression = Nothing
        Me.txtToDate1.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate1.FieldCode = Nothing
        Me.txtToDate1.FieldDesc = Nothing
        Me.txtToDate1.FieldMaxLength = 0
        Me.txtToDate1.FieldName = Nothing
        Me.txtToDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate1.isCalculatedField = False
        Me.txtToDate1.IsSourceFromTable = False
        Me.txtToDate1.IsSourceFromValueList = False
        Me.txtToDate1.IsUnique = False
        Me.txtToDate1.Location = New System.Drawing.Point(85, 7)
        Me.txtToDate1.MendatroryField = False
        Me.txtToDate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate1.MyLinkLable1 = Nothing
        Me.txtToDate1.MyLinkLable2 = Nothing
        Me.txtToDate1.Name = "txtToDate1"
        Me.txtToDate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate1.ReferenceFieldDesc = Nothing
        Me.txtToDate1.ReferenceFieldName = Nothing
        Me.txtToDate1.ReferenceTableName = Nothing
        Me.txtToDate1.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate1.TabIndex = 15
        Me.txtToDate1.TabStop = False
        Me.txtToDate1.Text = "17-12-2011"
        Me.txtToDate1.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'txtFromDate1
        '
        Me.txtFromDate1.CalculationExpression = Nothing
        Me.txtFromDate1.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate1.FieldCode = Nothing
        Me.txtFromDate1.FieldDesc = Nothing
        Me.txtFromDate1.FieldMaxLength = 0
        Me.txtFromDate1.FieldName = Nothing
        Me.txtFromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate1.isCalculatedField = False
        Me.txtFromDate1.IsSourceFromTable = False
        Me.txtFromDate1.IsSourceFromValueList = False
        Me.txtFromDate1.IsUnique = False
        Me.txtFromDate1.Location = New System.Drawing.Point(252, 7)
        Me.txtFromDate1.MendatroryField = False
        Me.txtFromDate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate1.MyLinkLable1 = Nothing
        Me.txtFromDate1.MyLinkLable2 = Nothing
        Me.txtFromDate1.Name = "txtFromDate1"
        Me.txtFromDate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate1.ReferenceFieldDesc = Nothing
        Me.txtFromDate1.ReferenceFieldName = Nothing
        Me.txtFromDate1.ReferenceTableName = Nothing
        Me.txtFromDate1.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate1.TabIndex = 16
        Me.txtFromDate1.TabStop = False
        Me.txtFromDate1.Text = "17-12-2011"
        Me.txtFromDate1.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(201, 8)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 18
        Me.lblToDate.Text = "To Date"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(12, 10)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(71, 24)
        Me.btnPost.TabIndex = 163
        Me.btnPost.Text = "Unpost"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(704, 10)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(84, 24)
        Me.btnclose.TabIndex = 162
        Me.btnclose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(85, 10)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 24)
        Me.btnReset.TabIndex = 161
        Me.btnReset.Text = "Reset"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(174, 14)
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(78, 20)
        Me.txtToDate.TabIndex = 5
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "24/10/2011"
        Me.txtToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(49, 15)
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(78, 20)
        Me.txtFromDate.TabIndex = 4
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "24/10/2011"
        Me.txtFromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'frmUnpostBmcDcs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmUnpostBmcDcs"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmUnpostBmcDcs"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnclose As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents txtToDate As RadDateTimePicker
    Friend WithEvents txtFromDate As RadDateTimePicker
    Friend WithEvents txtMultBmc As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents txtFromDate1 As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents txtToDate1 As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
End Class
