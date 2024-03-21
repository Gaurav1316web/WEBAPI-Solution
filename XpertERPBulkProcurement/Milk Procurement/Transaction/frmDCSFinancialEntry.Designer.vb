<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDCSFinancialEntry
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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.cboType = New Telerik.WinControls.UI.RadDropDownList()
        Me.lblFiscalYear = New common.Controls.MyLabel()
        Me.txtFiscalYear = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblPending = New common.usLock()
        Me.lblDCSName = New common.Controls.MyLabel()
        Me.txtDCS = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtdate = New common.Controls.MyDateTimePicker()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.lblCode = New common.Controls.MyLabel()
        Me.txtDocumentNo = New common.UserControls.txtNavigator()
        Me.gvItem = New common.UserControls.MyRadGridView()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFiscalYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDCSName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(825, 509)
        Me.SplitContainer1.SplitterDistance = 472
        Me.SplitContainer1.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRemarks)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblFiscalYear)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtFiscalYear)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblPending)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDCSName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDCS)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtdate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocumentNo)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvItem)
        Me.SplitContainer2.Size = New System.Drawing.Size(825, 472)
        Me.SplitContainer2.SplitterDistance = 151
        Me.SplitContainer2.TabIndex = 0
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblDescription.Location = New System.Drawing.Point(7, 112)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(49, 18)
        Me.lblDescription.TabIndex = 15
        Me.lblDescription.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.AutoSize = False
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(80, 95)
        Me.txtRemarks.MaxLength = 2000
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.MyLinkLable1 = Nothing
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(509, 53)
        Me.txtRemarks.TabIndex = 14
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(7, 75)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel4.TabIndex = 7
        Me.MyLabel4.Text = "Type"
        '
        'cboType
        '
        Me.cboType.AutoCompleteDisplayMember = Nothing
        Me.cboType.AutoCompleteValueMember = Nothing
        Me.cboType.DropDownAnimationEnabled = True
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.Location = New System.Drawing.Point(80, 73)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(212, 20)
        Me.cboType.TabIndex = 3
        '
        'lblFiscalYear
        '
        Me.lblFiscalYear.AutoSize = False
        Me.lblFiscalYear.BorderVisible = True
        Me.lblFiscalYear.FieldName = Nothing
        Me.lblFiscalYear.Location = New System.Drawing.Point(297, 52)
        Me.lblFiscalYear.Name = "lblFiscalYear"
        Me.lblFiscalYear.Size = New System.Drawing.Size(292, 19)
        Me.lblFiscalYear.TabIndex = 12
        '
        'txtFiscalYear
        '
        Me.txtFiscalYear.CalculationExpression = Nothing
        Me.txtFiscalYear.FieldCode = Nothing
        Me.txtFiscalYear.FieldDesc = Nothing
        Me.txtFiscalYear.FieldMaxLength = 0
        Me.txtFiscalYear.FieldName = Nothing
        Me.txtFiscalYear.isCalculatedField = False
        Me.txtFiscalYear.IsSourceFromTable = False
        Me.txtFiscalYear.IsSourceFromValueList = False
        Me.txtFiscalYear.IsUnique = False
        Me.txtFiscalYear.Location = New System.Drawing.Point(80, 52)
        Me.txtFiscalYear.MendatroryField = True
        Me.txtFiscalYear.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFiscalYear.MyLinkLable1 = Nothing
        Me.txtFiscalYear.MyLinkLable2 = Nothing
        Me.txtFiscalYear.MyReadOnly = False
        Me.txtFiscalYear.MyShowMasterFormButton = False
        Me.txtFiscalYear.Name = "txtFiscalYear"
        Me.txtFiscalYear.ReferenceFieldDesc = Nothing
        Me.txtFiscalYear.ReferenceFieldName = Nothing
        Me.txtFiscalYear.ReferenceTableName = Nothing
        Me.txtFiscalYear.Size = New System.Drawing.Size(212, 19)
        Me.txtFiscalYear.TabIndex = 2
        Me.txtFiscalYear.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(7, 53)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel3.TabIndex = 6
        Me.MyLabel3.Text = "Fiscal Year"
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(492, 7)
        Me.lblPending.Margin = New System.Windows.Forms.Padding(4)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(97, 20)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 13
        '
        'lblDCSName
        '
        Me.lblDCSName.AutoSize = False
        Me.lblDCSName.BorderVisible = True
        Me.lblDCSName.FieldName = Nothing
        Me.lblDCSName.Location = New System.Drawing.Point(297, 30)
        Me.lblDCSName.Name = "lblDCSName"
        Me.lblDCSName.Size = New System.Drawing.Size(292, 19)
        Me.lblDCSName.TabIndex = 11
        '
        'txtDCS
        '
        Me.txtDCS.CalculationExpression = Nothing
        Me.txtDCS.FieldCode = Nothing
        Me.txtDCS.FieldDesc = Nothing
        Me.txtDCS.FieldMaxLength = 0
        Me.txtDCS.FieldName = Nothing
        Me.txtDCS.isCalculatedField = False
        Me.txtDCS.IsSourceFromTable = False
        Me.txtDCS.IsSourceFromValueList = False
        Me.txtDCS.IsUnique = False
        Me.txtDCS.Location = New System.Drawing.Point(80, 30)
        Me.txtDCS.MendatroryField = True
        Me.txtDCS.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDCS.MyLinkLable1 = Nothing
        Me.txtDCS.MyLinkLable2 = Nothing
        Me.txtDCS.MyReadOnly = False
        Me.txtDCS.MyShowMasterFormButton = False
        Me.txtDCS.Name = "txtDCS"
        Me.txtDCS.ReferenceFieldDesc = Nothing
        Me.txtDCS.ReferenceFieldName = Nothing
        Me.txtDCS.ReferenceTableName = Nothing
        Me.txtDCS.Size = New System.Drawing.Size(212, 19)
        Me.txtDCS.TabIndex = 1
        Me.txtDCS.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(7, 31)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel6.TabIndex = 5
        Me.MyLabel6.Text = "DCS"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(359, 9)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel2.TabIndex = 10
        Me.MyLabel2.Text = "Date"
        '
        'txtdate
        '
        Me.txtdate.CalculationExpression = Nothing
        Me.txtdate.CustomFormat = "dd/MM/yyyy"
        Me.txtdate.FieldCode = Nothing
        Me.txtdate.FieldDesc = Nothing
        Me.txtdate.FieldMaxLength = 0
        Me.txtdate.FieldName = Nothing
        Me.txtdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtdate.isCalculatedField = False
        Me.txtdate.IsSourceFromTable = False
        Me.txtdate.IsSourceFromValueList = False
        Me.txtdate.IsUnique = False
        Me.txtdate.Location = New System.Drawing.Point(395, 8)
        Me.txtdate.MendatroryField = True
        Me.txtdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdate.MyLinkLable1 = Me.MyLabel2
        Me.txtdate.MyLinkLable2 = Nothing
        Me.txtdate.Name = "txtdate"
        Me.txtdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdate.ReferenceFieldDesc = Nothing
        Me.txtdate.ReferenceFieldName = Nothing
        Me.txtdate.ReferenceTableName = Nothing
        Me.txtdate.Size = New System.Drawing.Size(90, 18)
        Me.txtdate.TabIndex = 0
        Me.txtdate.TabStop = False
        Me.txtdate.Text = "13/06/2011"
        Me.txtdate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPBulkProcurement.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(336, 8)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(21, 19)
        Me.btnReset.TabIndex = 9
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Location = New System.Drawing.Point(7, 8)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(32, 18)
        Me.lblCode.TabIndex = 4
        Me.lblCode.Text = "Code"
        '
        'txtDocumentNo
        '
        Me.txtDocumentNo.FieldName = Nothing
        Me.txtDocumentNo.Location = New System.Drawing.Point(80, 7)
        Me.txtDocumentNo.MendatroryField = False
        Me.txtDocumentNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocumentNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocumentNo.MyLinkLable1 = Me.lblCode
        Me.txtDocumentNo.MyLinkLable2 = Nothing
        Me.txtDocumentNo.MyMaxLength = 30
        Me.txtDocumentNo.MyReadOnly = False
        Me.txtDocumentNo.Name = "txtDocumentNo"
        Me.txtDocumentNo.Size = New System.Drawing.Size(256, 20)
        Me.txtDocumentNo.TabIndex = 8
        Me.txtDocumentNo.Value = ""
        '
        'gvItem
        '
        Me.gvItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvItem.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvItem.MasterTemplate.AllowAddNewRow = False
        Me.gvItem.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvItem.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvItem.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvItem.MyStopExport = False
        Me.gvItem.Name = "gvItem"
        Me.gvItem.ShowHeaderCellButtons = True
        Me.gvItem.Size = New System.Drawing.Size(825, 317)
        Me.gvItem.TabIndex = 0
        Me.gvItem.TabStop = False
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(159, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 20)
        Me.btnPost.TabIndex = 4
        Me.btnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(753, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(69, 20)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(83, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(7, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'frmDCSFinancialEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(825, 509)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmDCSFinancialEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "DCS FinancialEntry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFiscalYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDCSName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtdate As common.Controls.MyDateTimePicker
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents txtDocumentNo As common.UserControls.txtNavigator
    Friend WithEvents gvItem As common.UserControls.MyRadGridView
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDCSName As common.Controls.MyLabel
    Friend WithEvents txtDCS As common.UserControls.txtFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents btnPost As RadButton
    Friend WithEvents lblFiscalYear As common.Controls.MyLabel
    Friend WithEvents txtFiscalYear As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents cboType As RadDropDownList
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
End Class

