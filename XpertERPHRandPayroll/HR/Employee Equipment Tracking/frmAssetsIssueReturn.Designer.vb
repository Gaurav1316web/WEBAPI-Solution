Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAssetsIssueReturn
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAssetsIssueReturn))
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.UsLock1 = New common.usLock()
        Me.FndIssueReturn = New common.UserControls.txtFinder()
        Me.lblIssueTo = New common.Controls.MyLabel()
        Me.lblReturnNo = New common.Controls.MyLabel()
        Me.cboType = New common.Controls.MyComboBox()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.lblIssueToName = New common.Controls.MyLabel()
        Me.txtIssueTo = New common.UserControls.txtFinder()
        Me.lblIncrementType = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblRemarks = New common.Controls.MyLabel()
        Me.lblDate = New common.Controls.MyLabel()
        Me.dtpDate = New common.Controls.MyDateTimePicker()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.gvIncrement = New common.UserControls.MyRadGridView()
        Me.BtnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblIssueTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReturnNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIssueToName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIncrementType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvIncrement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvIncrement.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.SplitContainer1)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(807, 502)
        Me.RadGroupBox3.TabIndex = 65
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(10, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.FndIssueReturn)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblReturnNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblIssueToName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtIssueTo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblIssueTo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblIncrementType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gvIncrement)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(787, 472)
        Me.SplitContainer1.SplitterDistance = 424
        Me.SplitContainer1.TabIndex = 157
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(565, 20)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(212, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 205
        '
        'FndIssueReturn
        '
        Me.FndIssueReturn.Location = New System.Drawing.Point(378, 63)
        Me.FndIssueReturn.MendatroryField = True
        Me.FndIssueReturn.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndIssueReturn.MyLinkLable1 = Me.lblIssueTo
        Me.FndIssueReturn.MyLinkLable2 = Nothing
        Me.FndIssueReturn.MyReadOnly = False
        Me.FndIssueReturn.MyShowMasterFormButton = False
        Me.FndIssueReturn.Name = "FndIssueReturn"
        Me.FndIssueReturn.Size = New System.Drawing.Size(163, 19)
        Me.FndIssueReturn.TabIndex = 204
        Me.FndIssueReturn.Value = ""
        '
        'lblIssueTo
        '
        Me.lblIssueTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIssueTo.Location = New System.Drawing.Point(22, 42)
        Me.lblIssueTo.Name = "lblIssueTo"
        Me.lblIssueTo.Size = New System.Drawing.Size(50, 16)
        Me.lblIssueTo.TabIndex = 196
        Me.lblIssueTo.Text = "Issue To"
        '
        'lblReturnNo
        '
        Me.lblReturnNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReturnNo.Location = New System.Drawing.Point(318, 64)
        Me.lblReturnNo.Name = "lblReturnNo"
        Me.lblReturnNo.Size = New System.Drawing.Size(64, 16)
        Me.lblReturnNo.TabIndex = 203
        Me.lblReturnNo.Text = "Return No. "
        '
        'cboType
        '
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.Location = New System.Drawing.Point(431, 19)
        Me.cboType.MendatroryField = True
        Me.cboType.MyLinkLable1 = Nothing
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(111, 20)
        Me.cboType.TabIndex = 202
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(320, 18)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 21)
        Me.btnNew.TabIndex = 201
        Me.btnNew.Text = " "
        '
        'lblIssueToName
        '
        Me.lblIssueToName.AutoSize = False
        Me.lblIssueToName.BorderVisible = True
        Me.lblIssueToName.Location = New System.Drawing.Point(319, 41)
        Me.lblIssueToName.Name = "lblIssueToName"
        Me.lblIssueToName.Size = New System.Drawing.Size(222, 19)
        Me.lblIssueToName.TabIndex = 198
        Me.lblIssueToName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtIssueTo
        '
        Me.txtIssueTo.Location = New System.Drawing.Point(98, 41)
        Me.txtIssueTo.MendatroryField = True
        Me.txtIssueTo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIssueTo.MyLinkLable1 = Me.lblIssueTo
        Me.txtIssueTo.MyLinkLable2 = Nothing
        Me.txtIssueTo.MyReadOnly = False
        Me.txtIssueTo.MyShowMasterFormButton = False
        Me.txtIssueTo.Name = "txtIssueTo"
        Me.txtIssueTo.Size = New System.Drawing.Size(219, 19)
        Me.txtIssueTo.TabIndex = 1
        Me.txtIssueTo.Value = ""
        '
        'lblIncrementType
        '
        Me.lblIncrementType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIncrementType.Location = New System.Drawing.Point(394, 20)
        Me.lblIncrementType.Name = "lblIncrementType"
        Me.lblIncrementType.Size = New System.Drawing.Size(31, 16)
        Me.lblIncrementType.TabIndex = 184
        Me.lblIncrementType.Text = "Type"
        '
        'txtDescription
        '
        Me.txtDescription.AutoSize = False
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(98, 85)
        Me.txtDescription.MaxLength = 49
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.lblRemarks
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(444, 48)
        Me.txtDescription.TabIndex = 7
        '
        'lblRemarks
        '
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(22, 85)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(51, 16)
        Me.lblRemarks.TabIndex = 177
        Me.lblRemarks.Text = "Remarks"
        '
        'lblDate
        '
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(21, 64)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(60, 16)
        Me.lblDate.TabIndex = 164
        Me.lblDate.Text = "Issue Date"
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate.Location = New System.Drawing.Point(98, 63)
        Me.dtpDate.MendatroryField = False
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.MyLinkLable1 = Me.lblDate
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.Size = New System.Drawing.Size(219, 18)
        Me.dtpDate.TabIndex = 2
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "10/07/2013"
        Me.dtpDate.Value = New Date(2013, 7, 10, 0, 0, 0, 0)
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(98, 18)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(219, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(22, 20)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(57, 16)
        Me.lblCode.TabIndex = 161
        Me.lblCode.Text = "Issue No. "
        '
        'gvIncrement
        '
        Me.gvIncrement.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvIncrement.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvIncrement.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvIncrement.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvIncrement.ForeColor = System.Drawing.Color.Black
        Me.gvIncrement.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvIncrement.Location = New System.Drawing.Point(8, 149)
        '
        'gvIncrement
        '
        Me.gvIncrement.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvIncrement.MasterTemplate.AllowAddNewRow = False
        Me.gvIncrement.MasterTemplate.AllowDragToGroup = False
        Me.gvIncrement.MasterTemplate.AutoGenerateColumns = False
        Me.gvIncrement.MasterTemplate.EnableGrouping = False
        Me.gvIncrement.Name = "gvIncrement"
        Me.gvIncrement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvIncrement.Size = New System.Drawing.Size(769, 272)
        Me.gvIncrement.TabIndex = 8
        Me.gvIncrement.Text = "RadGridView4"
        '
        'BtnPost
        '
        Me.BtnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPost.Location = New System.Drawing.Point(153, 17)
        Me.BtnPost.Name = "BtnPost"
        Me.BtnPost.Size = New System.Drawing.Size(66, 18)
        Me.BtnPost.TabIndex = 4
        Me.BtnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 17)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(712, 17)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(81, 17)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'frmAssetsIssueReturn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(807, 502)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Name = "frmAssetsIssueReturn"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Asset Issue Return"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblIssueTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReturnNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIssueToName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIncrementType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvIncrement.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvIncrement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents dtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents gvIncrement As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblIncrementType As common.Controls.MyLabel
    Friend WithEvents lblIssueToName As common.Controls.MyLabel
    Friend WithEvents txtIssueTo As common.UserControls.txtFinder
    Friend WithEvents lblIssueTo As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents cboType As common.Controls.MyComboBox
    Friend WithEvents lblReturnNo As common.Controls.MyLabel
    Friend WithEvents FndIssueReturn As common.UserControls.txtFinder
    Friend WithEvents BtnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
End Class
