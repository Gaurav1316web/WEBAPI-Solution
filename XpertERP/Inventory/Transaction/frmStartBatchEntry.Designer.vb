<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmStartBatchEntry
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtDefaultBatch = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtItem = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtItemType = New common.UserControls.txtMultiSelectFinder()
        Me.lblItemType = New common.Controls.MyLabel()
        Me.lblBatch = New common.Controls.MyLabel()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.txtDocumentDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.lblStatus = New common.usLock()
        Me.txtDocumentNo = New common.UserControls.txtNavigator()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.btnReverseDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSaveAndPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtDefaultBatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverseDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSaveAndPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Gv1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverseDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSaveAndPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(766, 436)
        Me.SplitContainer1.SplitterDistance = 377
        Me.SplitContainer1.TabIndex = 3
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.Gv1.Location = New System.Drawing.Point(0, 133)
        '
        '
        '
        Me.Gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.Gv1.MyExportFilePath = ""
        Me.Gv1.MyStopExport = False
        Me.Gv1.Name = "Gv1"
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(766, 244)
        Me.Gv1.TabIndex = 1
        Me.Gv1.VarID = ""
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnReset)
        Me.Panel1.Controls.Add(Me.btnAddNew)
        Me.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.Panel1.Controls.Add(Me.btnGo)
        Me.Panel1.Controls.Add(Me.txtDocumentDate)
        Me.Panel1.Controls.Add(Me.RadLabel4)
        Me.Panel1.Controls.Add(Me.RadLabel1)
        Me.Panel1.Controls.Add(Me.lblStatus)
        Me.Panel1.Controls.Add(Me.txtDocumentNo)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(766, 133)
        Me.Panel1.TabIndex = 0
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.btnReset.Location = New System.Drawing.Point(531, 38)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(100, 19)
        Me.btnReset.TabIndex = 1096
        Me.btnReset.Text = "Reset"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(346, 10)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 20)
        Me.btnAddNew.TabIndex = 1578
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtDefaultBatch)
        Me.RadGroupBox1.Controls.Add(Me.txtItem)
        Me.RadGroupBox1.Controls.Add(Me.txtItemType)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox1.Controls.Add(Me.lblItemType)
        Me.RadGroupBox1.Controls.Add(Me.lblBatch)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(5, 34)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(517, 73)
        Me.RadGroupBox1.TabIndex = 1577
        '
        'txtDefaultBatch
        '
        Me.txtDefaultBatch.CalculationExpression = Nothing
        Me.txtDefaultBatch.FieldCode = Nothing
        Me.txtDefaultBatch.FieldDesc = Nothing
        Me.txtDefaultBatch.FieldMaxLength = 0
        Me.txtDefaultBatch.FieldName = Nothing
        Me.txtDefaultBatch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDefaultBatch.isCalculatedField = False
        Me.txtDefaultBatch.IsSourceFromTable = False
        Me.txtDefaultBatch.IsSourceFromValueList = False
        Me.txtDefaultBatch.IsUnique = False
        Me.txtDefaultBatch.Location = New System.Drawing.Point(93, 49)
        Me.txtDefaultBatch.MaxLength = 200
        Me.txtDefaultBatch.MendatroryField = False
        Me.txtDefaultBatch.MyLinkLable1 = Me.MyLabel3
        Me.txtDefaultBatch.MyLinkLable2 = Nothing
        Me.txtDefaultBatch.Name = "txtDefaultBatch"
        Me.txtDefaultBatch.ReferenceFieldDesc = Nothing
        Me.txtDefaultBatch.ReferenceFieldName = Nothing
        Me.txtDefaultBatch.ReferenceTableName = Nothing
        Me.txtDefaultBatch.Size = New System.Drawing.Size(419, 18)
        Me.txtDefaultBatch.TabIndex = 1099
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(7, 110)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel3.TabIndex = 24
        Me.MyLabel3.Text = "Remarks"
        '
        'txtItem
        '
        Me.txtItem.arrDispalyMember = Nothing
        Me.txtItem.arrValueMember = Nothing
        Me.txtItem.Location = New System.Drawing.Point(93, 28)
        Me.txtItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.MyLinkLable1 = Me.MyLabel6
        Me.txtItem.MyLinkLable2 = Nothing
        Me.txtItem.MyNullText = "All"
        Me.txtItem.Name = "txtItem"
        Me.txtItem.Size = New System.Drawing.Size(418, 19)
        Me.txtItem.TabIndex = 1098
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(2, 27)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(28, 16)
        Me.MyLabel6.TabIndex = 364
        Me.MyLabel6.Text = "Item"
        '
        'txtItemType
        '
        Me.txtItemType.arrDispalyMember = Nothing
        Me.txtItemType.arrValueMember = Nothing
        Me.txtItemType.Location = New System.Drawing.Point(93, 6)
        Me.txtItemType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemType.MyLinkLable1 = Me.lblItemType
        Me.txtItemType.MyLinkLable2 = Nothing
        Me.txtItemType.MyNullText = "All"
        Me.txtItemType.Name = "txtItemType"
        Me.txtItemType.Size = New System.Drawing.Size(418, 19)
        Me.txtItemType.TabIndex = 1097
        '
        'lblItemType
        '
        Me.lblItemType.FieldName = Nothing
        Me.lblItemType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemType.Location = New System.Drawing.Point(2, 6)
        Me.lblItemType.Name = "lblItemType"
        Me.lblItemType.Size = New System.Drawing.Size(57, 16)
        Me.lblItemType.TabIndex = 5
        Me.lblItemType.Text = "Item Type"
        '
        'lblBatch
        '
        Me.lblBatch.FieldName = Nothing
        Me.lblBatch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBatch.Location = New System.Drawing.Point(2, 51)
        Me.lblBatch.Name = "lblBatch"
        Me.lblBatch.Size = New System.Drawing.Size(74, 16)
        Me.lblBatch.TabIndex = 51
        Me.lblBatch.Text = "Default Batch"
        Me.lblBatch.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        '
        'btnGo
        '
        Me.btnGo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnGo.Location = New System.Drawing.Point(531, 65)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(100, 19)
        Me.btnGo.TabIndex = 52
        Me.btnGo.Text = ">>"
        '
        'txtDocumentDate
        '
        Me.txtDocumentDate.CalculationExpression = Nothing
        Me.txtDocumentDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtDocumentDate.FieldCode = Nothing
        Me.txtDocumentDate.FieldDesc = Nothing
        Me.txtDocumentDate.FieldMaxLength = 0
        Me.txtDocumentDate.FieldName = Nothing
        Me.txtDocumentDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocumentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDocumentDate.isCalculatedField = False
        Me.txtDocumentDate.IsSourceFromTable = False
        Me.txtDocumentDate.IsSourceFromValueList = False
        Me.txtDocumentDate.IsUnique = False
        Me.txtDocumentDate.Location = New System.Drawing.Point(398, 10)
        Me.txtDocumentDate.MendatroryField = True
        Me.txtDocumentDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDocumentDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDocumentDate.MyLinkLable2 = Nothing
        Me.txtDocumentDate.Name = "txtDocumentDate"
        Me.txtDocumentDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDocumentDate.ReferenceFieldDesc = Nothing
        Me.txtDocumentDate.ReferenceFieldName = Nothing
        Me.txtDocumentDate.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtDocumentDate.RootElement.StretchVertically = True
        Me.txtDocumentDate.Size = New System.Drawing.Size(124, 19)
        Me.txtDocumentDate.TabIndex = 1576
        Me.txtDocumentDate.TabStop = False
        Me.txtDocumentDate.Text = "13/06/2011 11:29 AM"
        Me.txtDocumentDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(368, 11)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 1573
        Me.RadLabel4.Text = "Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(7, 13)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel1.TabIndex = 1575
        Me.RadLabel1.Text = "Document No"
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblStatus.Location = New System.Drawing.Point(531, 10)
        Me.lblStatus.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(101, 19)
        Me.lblStatus.Status = common.ERPTransactionStatus.Pending
        Me.lblStatus.TabIndex = 1574
        '
        'txtDocumentNo
        '
        Me.txtDocumentNo.FieldName = Nothing
        Me.txtDocumentNo.Location = New System.Drawing.Point(97, 10)
        Me.txtDocumentNo.MendatroryField = False
        Me.txtDocumentNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocumentNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocumentNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocumentNo.MyLinkLable2 = Nothing
        Me.txtDocumentNo.MyMaxLength = 30
        Me.txtDocumentNo.MyReadOnly = False
        Me.txtDocumentNo.Name = "txtDocumentNo"
        Me.txtDocumentNo.Size = New System.Drawing.Size(247, 20)
        Me.txtDocumentNo.TabIndex = 1572
        Me.txtDocumentNo.TabStop = False
        Me.txtDocumentNo.Value = ""
        '
        'txtRemarks
        '
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(97, 109)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.MyLabel3
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(534, 18)
        Me.txtRemarks.TabIndex = 6
        '
        'btnReverseDelete
        '
        Me.btnReverseDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverseDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverseDelete.Location = New System.Drawing.Point(144, 28)
        Me.btnReverseDelete.Name = "btnReverseDelete"
        Me.btnReverseDelete.Size = New System.Drawing.Size(111, 24)
        Me.btnReverseDelete.TabIndex = 7
        Me.btnReverseDelete.Text = "Reverse And Delete"
        Me.btnReverseDelete.Visible = False
        '
        'btnSaveAndPost
        '
        Me.btnSaveAndPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSaveAndPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveAndPost.Location = New System.Drawing.Point(6, 28)
        Me.btnSaveAndPost.Name = "btnSaveAndPost"
        Me.btnSaveAndPost.Size = New System.Drawing.Size(134, 24)
        Me.btnSaveAndPost.TabIndex = 0
        Me.btnSaveAndPost.Text = "Save And Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(696, 27)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 24)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(766, 20)
        Me.RadMenu1.TabIndex = 0
        '
        'frmStartBatchEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(766, 456)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmStartBatchEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Customer Penalty"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtDefaultBatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverseDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSaveAndPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSaveAndPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents btnReverseDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents btnReset As RadButton
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents lblBatch As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents btnGo As RadButton
    Friend WithEvents lblItemType As common.Controls.MyLabel
    Friend WithEvents txtDocumentDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblStatus As common.usLock
    Friend WithEvents txtDocumentNo As common.UserControls.txtNavigator
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents txtItemType As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtItem As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtDefaultBatch As common.Controls.MyTextBox
End Class

