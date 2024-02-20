<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDocumentVersionReport
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnShow = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.ChkMilkType = New common.Controls.MyCheckBox()
        Me.dtpToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.dtpFromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.cboTransaction = New common.Controls.MyComboBox()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.cboModule = New common.Controls.MyComboBox()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtModifyedBy = New common.UserControls.txtMultiSelectFinder()
        Me.txtCreatedBy = New common.UserControls.txtMultiSelectFinder()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkMilkType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboModule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCreatedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtModifyedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnShow)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ChkMilkType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpFromDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboTransaction)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboModule)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel10)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer1.Size = New System.Drawing.Size(748, 600)
        Me.SplitContainer1.SplitterDistance = 112
        Me.SplitContainer1.TabIndex = 0
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(232, 68)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(55, 41)
        Me.RadButton1.TabIndex = 24
        Me.RadButton1.Text = "Close"
        '
        'btnShow
        '
        Me.btnShow.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShow.Location = New System.Drawing.Point(173, 68)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(59, 41)
        Me.btnShow.TabIndex = 23
        Me.btnShow.Text = ">>>"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 91)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel1.TabIndex = 22
        Me.MyLabel1.Text = "To Date"
        '
        'ChkMilkType
        '
        Me.ChkMilkType.Location = New System.Drawing.Point(295, 48)
        Me.ChkMilkType.MyLinkLable1 = Nothing
        Me.ChkMilkType.MyLinkLable2 = Nothing
        Me.ChkMilkType.Name = "ChkMilkType"
        Me.ChkMilkType.Size = New System.Drawing.Size(69, 18)
        Me.ChkMilkType.TabIndex = 21
        Me.ChkMilkType.Tag1 = Nothing
        Me.ChkMilkType.Text = "Milk Type"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpToDate.Location = New System.Drawing.Point(82, 89)
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(85, 20)
        Me.dtpToDate.TabIndex = 17
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "22/07/2011"
        Me.dtpToDate.Value = New Date(2011, 7, 22, 13, 55, 15, 703)
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFromDate.Location = New System.Drawing.Point(82, 68)
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.Size = New System.Drawing.Size(85, 20)
        Me.dtpFromDate.TabIndex = 16
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "22/07/2011"
        Me.dtpFromDate.Value = New Date(2011, 7, 22, 13, 55, 15, 703)
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(12, 70)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel2.TabIndex = 18
        Me.RadLabel2.Text = "From Date"
        '
        'cboTransaction
        '
        Me.cboTransaction.CalculationExpression = Nothing
        Me.cboTransaction.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboTransaction.FieldCode = Nothing
        Me.cboTransaction.FieldDesc = Nothing
        Me.cboTransaction.FieldMaxLength = 0
        Me.cboTransaction.FieldName = Nothing
        Me.cboTransaction.isCalculatedField = False
        Me.cboTransaction.IsSourceFromTable = False
        Me.cboTransaction.IsSourceFromValueList = False
        Me.cboTransaction.IsUnique = False
        Me.cboTransaction.Location = New System.Drawing.Point(81, 27)
        Me.cboTransaction.MendatroryField = True
        Me.cboTransaction.MyLinkLable1 = Nothing
        Me.cboTransaction.MyLinkLable2 = Nothing
        Me.cboTransaction.Name = "cboTransaction"
        Me.cboTransaction.ReferenceFieldDesc = Nothing
        Me.cboTransaction.ReferenceFieldName = Nothing
        Me.cboTransaction.ReferenceTableName = Nothing
        Me.cboTransaction.Size = New System.Drawing.Size(208, 20)
        Me.cboTransaction.TabIndex = 15
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(12, 29)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel1.TabIndex = 19
        Me.RadLabel1.Text = "Transaction"
        '
        'cboModule
        '
        Me.cboModule.CalculationExpression = Nothing
        Me.cboModule.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboModule.FieldCode = Nothing
        Me.cboModule.FieldDesc = Nothing
        Me.cboModule.FieldMaxLength = 0
        Me.cboModule.FieldName = Nothing
        Me.cboModule.isCalculatedField = False
        Me.cboModule.IsSourceFromTable = False
        Me.cboModule.IsSourceFromValueList = False
        Me.cboModule.IsUnique = False
        Me.cboModule.Location = New System.Drawing.Point(81, 7)
        Me.cboModule.MendatroryField = True
        Me.cboModule.MyLinkLable1 = Nothing
        Me.cboModule.MyLinkLable2 = Nothing
        Me.cboModule.Name = "cboModule"
        Me.cboModule.ReferenceFieldDesc = Nothing
        Me.cboModule.ReferenceFieldName = Nothing
        Me.cboModule.ReferenceTableName = Nothing
        Me.cboModule.Size = New System.Drawing.Size(208, 20)
        Me.cboModule.TabIndex = 14
        '
        'RadLabel10
        '
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.Location = New System.Drawing.Point(12, 9)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(43, 16)
        Me.RadLabel10.TabIndex = 20
        Me.RadLabel10.Text = "Module"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(748, 484)
        Me.gv1.TabIndex = 12
        Me.gv1.Text = "RadGridView1"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(12, 48)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel5.TabIndex = 401
        Me.MyLabel5.Text = "Location"
        '
        'txtModifyedBy
        '
        Me.txtModifyedBy.arrDispalyMember = Nothing
        Me.txtModifyedBy.arrValueMember = Nothing
        Me.txtModifyedBy.Location = New System.Drawing.Point(361, 28)
        Me.txtModifyedBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModifyedBy.MyLinkLable1 = Me.MyLabel5
        Me.txtModifyedBy.MyLinkLable2 = Nothing
        Me.txtModifyedBy.MyNullText = "All"
        Me.txtModifyedBy.Name = "txtModifyedBy"
        Me.txtModifyedBy.Size = New System.Drawing.Size(285, 19)
        Me.txtModifyedBy.TabIndex = 402
        '
        'txtCreatedBy
        '
        Me.txtCreatedBy.arrDispalyMember = Nothing
        Me.txtCreatedBy.arrValueMember = Nothing
        Me.txtCreatedBy.Location = New System.Drawing.Point(361, 8)
        Me.txtCreatedBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreatedBy.MyLinkLable1 = Me.MyLabel5
        Me.txtCreatedBy.MyLinkLable2 = Nothing
        Me.txtCreatedBy.MyNullText = "All"
        Me.txtCreatedBy.Name = "txtCreatedBy"
        Me.txtCreatedBy.Size = New System.Drawing.Size(285, 19)
        Me.txtCreatedBy.TabIndex = 403
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(81, 48)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.MyLabel5
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(208, 19)
        Me.txtLocation.TabIndex = 404
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(295, 8)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(60, 18)
        Me.MyLabel2.TabIndex = 405
        Me.MyLabel2.Text = "Created By"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(295, 28)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(66, 18)
        Me.MyLabel3.TabIndex = 406
        Me.MyLabel3.Text = "Modified By"
        '
        'FrmDocumentVersionReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(748, 600)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmDocumentVersionReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Document Version Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkMilkType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboModule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents ChkMilkType As common.Controls.MyCheckBox
    Friend WithEvents dtpToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpFromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents cboTransaction As common.Controls.MyComboBox
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents cboModule As common.Controls.MyComboBox
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents btnShow As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtCreatedBy As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtModifyedBy As common.UserControls.txtMultiSelectFinder
End Class

