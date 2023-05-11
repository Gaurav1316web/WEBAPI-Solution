<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMailSMSSettingNew2
    Inherits Telerik.WinControls.UI.RadForm

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
        Me.components = New System.ComponentModel.Container()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.ChkAttachment = New common.Controls.MyCheckBox()
        Me.txtEmailText = New System.Windows.Forms.RichTextBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.txtmailsub = New common.Controls.MyTextBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtsms = New System.Windows.Forms.RichTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbguser = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbtnSelect = New common.Controls.MyRadioButton()
        Me.rbtnAll = New common.Controls.MyRadioButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.ChkAttachment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtmailsub, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        CType(Me.cbguser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbguser.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.rbtnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer1.Size = New System.Drawing.Size(615, 460)
        Me.SplitContainer1.SplitterDistance = 419
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(3, 3)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(609, 413)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.ChkAttachment)
        Me.RadPageViewPage1.Controls.Add(Me.txtEmailText)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.txtmailsub)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(72.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(588, 365)
        Me.RadPageViewPage1.Text = "E-Mail Text"
        '
        'ChkAttachment
        '
        Me.ChkAttachment.Location = New System.Drawing.Point(476, 349)
        Me.ChkAttachment.MyLinkLable1 = Nothing
        Me.ChkAttachment.MyLinkLable2 = Nothing
        Me.ChkAttachment.Name = "ChkAttachment"
        Me.ChkAttachment.Size = New System.Drawing.Size(102, 18)
        Me.ChkAttachment.TabIndex = 6
        Me.ChkAttachment.Tag1 = Nothing
        Me.ChkAttachment.Text = "Attached Report"
        '
        'txtEmailText
        '
        Me.txtEmailText.ContextMenuStrip = Me.ContextMenuStrip1
        Me.txtEmailText.Location = New System.Drawing.Point(3, 30)
        Me.txtEmailText.Name = "txtEmailText"
        Me.txtEmailText.Size = New System.Drawing.Size(575, 313)
        Me.txtEmailText.TabIndex = 112
        Me.txtEmailText.Text = ""
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'RadLabel12
        '
        Me.RadLabel12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel12.Location = New System.Drawing.Point(0, 349)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(167, 16)
        Me.RadLabel12.TabIndex = 111
        Me.RadLabel12.Text = "Right Click for Add Constants"
        '
        'txtmailsub
        '
        Me.txtmailsub.CalculationExpression = Nothing
        Me.txtmailsub.ContextMenuStrip = Me.ContextMenuStrip1
        Me.txtmailsub.FieldCode = Nothing
        Me.txtmailsub.FieldDesc = Nothing
        Me.txtmailsub.FieldMaxLength = 0
        Me.txtmailsub.FieldName = Nothing
        Me.txtmailsub.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmailsub.isCalculatedField = False
        Me.txtmailsub.IsSourceFromTable = False
        Me.txtmailsub.IsSourceFromValueList = False
        Me.txtmailsub.IsUnique = False
        Me.txtmailsub.Location = New System.Drawing.Point(53, 7)
        Me.txtmailsub.MaxLength = 50
        Me.txtmailsub.MendatroryField = False
        Me.txtmailsub.MyLinkLable1 = Me.MyLabel6
        Me.txtmailsub.MyLinkLable2 = Nothing
        Me.txtmailsub.Name = "txtmailsub"
        Me.txtmailsub.ReferenceFieldDesc = Nothing
        Me.txtmailsub.ReferenceFieldName = Nothing
        Me.txtmailsub.ReferenceTableName = Nothing
        Me.txtmailsub.Size = New System.Drawing.Size(525, 18)
        Me.txtmailsub.TabIndex = 110
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(3, 8)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel6.TabIndex = 109
        Me.MyLabel6.Text = "Subject"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.txtsms)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(62.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(588, 365)
        Me.RadPageViewPage2.Text = "SMS Text"
        '
        'txtsms
        '
        Me.txtsms.ContextMenuStrip = Me.ContextMenuStrip1
        Me.txtsms.Location = New System.Drawing.Point(8, 7)
        Me.txtsms.Name = "txtsms"
        Me.txtsms.Size = New System.Drawing.Size(575, 337)
        Me.txtsms.TabIndex = 114
        Me.txtsms.Text = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.ForeColor = System.Drawing.SystemColors.Highlight
        Me.MyLabel1.Location = New System.Drawing.Point(0, 349)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(588, 16)
        Me.MyLabel1.TabIndex = 113
        Me.MyLabel1.Text = "Right Click for Add Constants"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(87.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(588, 365)
        Me.RadPageViewPage3.Text = "User Selection"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox8)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(582, 359)
        Me.RadGroupBox1.TabIndex = 0
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.cbguser)
        Me.RadGroupBox8.Controls.Add(Me.Panel1)
        Me.RadGroupBox8.HeaderText = "Users"
        Me.RadGroupBox8.Location = New System.Drawing.Point(5, 9)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox8.Size = New System.Drawing.Size(563, 345)
        Me.RadGroupBox8.TabIndex = 306
        Me.RadGroupBox8.Text = "Users"
        '
        'cbguser
        '
        Me.cbguser.Location = New System.Drawing.Point(10, 47)
        '
        '
        '
        Me.cbguser.MasterTemplate.ShowHeaderCellButtons = True
        Me.cbguser.Name = "cbguser"
        Me.cbguser.ShowHeaderCellButtons = True
        Me.cbguser.Size = New System.Drawing.Size(543, 290)
        Me.cbguser.TabIndex = 1
        Me.cbguser.Text = "RadGridView1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbtnSelect)
        Me.Panel1.Controls.Add(Me.rbtnAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(543, 23)
        Me.Panel1.TabIndex = 0
        '
        'rbtnSelect
        '
        Me.rbtnSelect.Location = New System.Drawing.Point(298, 3)
        Me.rbtnSelect.MyLinkLable1 = Nothing
        Me.rbtnSelect.MyLinkLable2 = Nothing
        Me.rbtnSelect.Name = "rbtnSelect"
        Me.rbtnSelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnSelect.TabIndex = 1
        Me.rbtnSelect.Text = "Select"
        '
        'rbtnAll
        '
        Me.rbtnAll.Location = New System.Drawing.Point(164, 3)
        Me.rbtnAll.MyLinkLable1 = Nothing
        Me.rbtnAll.MyLinkLable2 = Nothing
        Me.rbtnAll.Name = "rbtnAll"
        Me.rbtnAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnAll.TabIndex = 0
        Me.rbtnAll.Text = "All"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(6, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(69, 22)
        Me.btnsave.TabIndex = 5
        Me.btnsave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(540, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'FrmMailSMSSettingNew2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(615, 460)
        Me.Controls.Add(Me.SplitContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "FrmMailSMSSettingNew2"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Email/SMS Setting"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.ChkAttachment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtmailsub, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        CType(Me.cbguser.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbguser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.rbtnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtmailsub As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtEmailText As System.Windows.Forms.RichTextBox
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents txtsms As System.Windows.Forms.RichTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ChkAttachment As common.Controls.MyCheckBox
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox8 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbtnSelect As common.Controls.MyRadioButton
    Friend WithEvents rbtnAll As common.Controls.MyRadioButton
    Friend WithEvents cbguser As common.UserControls.MyRadGridView
End Class

