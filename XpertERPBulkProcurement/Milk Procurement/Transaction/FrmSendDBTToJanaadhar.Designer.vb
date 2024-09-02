<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSendDBTToJanaadhar
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
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.MyLabel34 = New common.Controls.MyLabel()
        Me.txtUnion = New common.UserControls.txtMultiSelectFinder()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.txtMonth = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.gvDetail = New common.UserControls.MyRadGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnGenerateBill = New Telerik.WinControls.UI.RadButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gvDetail.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGenerateBill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.SplitContainer2)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(612, 279)
        Me.RadGroupBox3.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(10, 20)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel12)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel34)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtUnion)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadButton1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtMonth)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvDetail)
        Me.SplitContainer2.Size = New System.Drawing.Size(592, 249)
        Me.SplitContainer2.SplitterDistance = 67
        Me.SplitContainer2.TabIndex = 15
        '
        'RadLabel12
        '
        Me.RadLabel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel12.Location = New System.Drawing.Point(197, 51)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(390, 13)
        Me.RadLabel12.TabIndex = 26
        Me.RadLabel12.Text = "Double click on [ DBT NEFT No ] To View Attachments || Reference No for Pay Manag" &
    "er"
        '
        'MyLabel34
        '
        Me.MyLabel34.FieldName = Nothing
        Me.MyLabel34.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel34.Location = New System.Drawing.Point(7, 6)
        Me.MyLabel34.Name = "MyLabel34"
        Me.MyLabel34.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel34.TabIndex = 16
        Me.MyLabel34.Text = "Union"
        '
        'txtUnion
        '
        Me.txtUnion.arrDispalyMember = Nothing
        Me.txtUnion.arrValueMember = Nothing
        Me.txtUnion.Location = New System.Drawing.Point(56, 6)
        Me.txtUnion.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnion.MyLinkLable1 = Nothing
        Me.txtUnion.MyLinkLable2 = Nothing
        Me.txtUnion.MyNullText = "All"
        Me.txtUnion.Name = "txtUnion"
        Me.txtUnion.Size = New System.Drawing.Size(290, 19)
        Me.txtUnion.TabIndex = 15
        '
        'RadButton1
        '
        Me.RadButton1.Location = New System.Drawing.Point(167, 29)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(71, 20)
        Me.RadButton1.TabIndex = 14
        Me.RadButton1.Text = ">>"
        '
        'txtMonth
        '
        Me.txtMonth.CalculationExpression = Nothing
        Me.txtMonth.CustomFormat = "MMM - yyyy"
        Me.txtMonth.FieldCode = Nothing
        Me.txtMonth.FieldDesc = Nothing
        Me.txtMonth.FieldMaxLength = 0
        Me.txtMonth.FieldName = Nothing
        Me.txtMonth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtMonth.isCalculatedField = False
        Me.txtMonth.IsSourceFromTable = False
        Me.txtMonth.IsSourceFromValueList = False
        Me.txtMonth.IsUnique = False
        Me.txtMonth.Location = New System.Drawing.Point(56, 30)
        Me.txtMonth.MendatroryField = True
        Me.txtMonth.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMonth.MyLinkLable1 = Me.MyLabel1
        Me.txtMonth.MyLinkLable2 = Nothing
        Me.txtMonth.Name = "txtMonth"
        Me.txtMonth.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMonth.ReferenceFieldDesc = Nothing
        Me.txtMonth.ReferenceFieldName = Nothing
        Me.txtMonth.ReferenceTableName = Nothing
        Me.txtMonth.ShowCheckBox = True
        Me.txtMonth.Size = New System.Drawing.Size(105, 18)
        Me.txtMonth.TabIndex = 1
        Me.txtMonth.TabStop = False
        Me.txtMonth.Text = "Sep - 2014"
        Me.txtMonth.Value = New Date(2014, 9, 29, 0, 0, 0, 0)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(7, 31)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(38, 16)
        Me.MyLabel1.TabIndex = 9
        Me.MyLabel1.Text = "Month"
        '
        'gvDetail
        '
        Me.gvDetail.Controls.Add(Me.GroupBox1)
        Me.gvDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDetail.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvDetail.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvDetail.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDetail.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvDetail.MyStopExport = False
        Me.gvDetail.Name = "gvDetail"
        Me.gvDetail.ShowHeaderCellButtons = True
        Me.gvDetail.Size = New System.Drawing.Size(592, 178)
        Me.gvDetail.TabIndex = 5
        Me.gvDetail.VarID = ""
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.RadButton2)
        Me.GroupBox1.Controls.Add(Me.UcAttachment1)
        Me.GroupBox1.Location = New System.Drawing.Point(65, 18)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(471, 145)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Visible = False
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton2.Location = New System.Drawing.Point(386, 26)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(75, 20)
        Me.RadButton2.TabIndex = 3
        Me.RadButton2.Text = "Close"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(3, 18)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(465, 124)
        Me.UcAttachment1.TabIndex = 2
        Me.UcAttachment1.TabStop = False
        '
        'btnGenerateBill
        '
        Me.btnGenerateBill.Location = New System.Drawing.Point(3, 5)
        Me.btnGenerateBill.Name = "btnGenerateBill"
        Me.btnGenerateBill.Size = New System.Drawing.Size(75, 23)
        Me.btnGenerateBill.TabIndex = 0
        Me.btnGenerateBill.Text = "Post"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnGenerateBill)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 279)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(612, 33)
        Me.Panel1.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(534, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'FrmSendDBTToJanaadhar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(612, 312)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FrmSendDBTToJanaadhar"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Send DBT to Janaadhar"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gvDetail.ResumeLayout(False)
        Me.gvDetail.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGenerateBill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnGenerateBill As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtMonth As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents gvDetail As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel34 As common.Controls.MyLabel
    Friend WithEvents txtUnion As common.UserControls.txtMultiSelectFinder
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadButton2 As RadButton
    Friend WithEvents UcAttachment1 As ucAttachment
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
End Class

