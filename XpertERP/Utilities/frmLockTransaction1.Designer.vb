<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLockTransaction1
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.dgvDetails = New common.UserControls.MyRadGridView()
        Me.cmbCompany = New common.Controls.MyComboBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtlocation = New common.UserControls.txtFinder()
        Me.RadLabel4 = New Telerik.WinControls.UI.RadLabel()
        Me.btnLock = New Telerik.WinControls.UI.RadButton()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.btnLockUser = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.chkAllLoc = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkLocationCode = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkLocationSegment = New Telerik.WinControls.UI.RadRadioButton()
        Me.dtpToDate1 = New common.Controls.MyDateTimePicker()
        Me.dtpFromdate1 = New common.Controls.MyDateTimePicker()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.txtLocationMult = New common.UserControls.txtMultiSelectFinder()
        Me.chkread = New common.Controls.MyCheckBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.dgvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnLock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.btnLockUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationSegment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkread, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.dgvDetails)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Transaction Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(951, 310)
        Me.RadGroupBox2.TabIndex = 5
        Me.RadGroupBox2.Text = "Transaction Details"
        '
        'dgvDetails
        '
        Me.dgvDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgvDetails.Location = New System.Drawing.Point(7, 17)
        '
        '
        '
        Me.dgvDetails.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.dgvDetails.MasterTemplate.ShowHeaderCellButtons = True
        Me.dgvDetails.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.dgvDetails.Name = "dgvDetails"
        Me.dgvDetails.ShowHeaderCellButtons = True
        Me.dgvDetails.Size = New System.Drawing.Size(927, 280)
        Me.dgvDetails.TabIndex = 0
        '
        'cmbCompany
        '
        Me.cmbCompany.CalculationExpression = Nothing
        Me.cmbCompany.DropDownAnimationEnabled = True
        Me.cmbCompany.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbCompany.FieldCode = Nothing
        Me.cmbCompany.FieldDesc = Nothing
        Me.cmbCompany.FieldMaxLength = 0
        Me.cmbCompany.FieldName = Nothing
        Me.cmbCompany.isCalculatedField = False
        Me.cmbCompany.IsSourceFromTable = False
        Me.cmbCompany.IsSourceFromValueList = False
        Me.cmbCompany.IsUnique = False
        Me.cmbCompany.Location = New System.Drawing.Point(62, 9)
        Me.cmbCompany.MendatroryField = True
        Me.cmbCompany.MyLinkLable1 = Nothing
        Me.cmbCompany.MyLinkLable2 = Nothing
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.ReferenceFieldDesc = Nothing
        Me.cmbCompany.ReferenceFieldName = Nothing
        Me.cmbCompany.ReferenceTableName = Nothing
        Me.cmbCompany.Size = New System.Drawing.Size(264, 20)
        Me.cmbCompany.TabIndex = 0
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(3, 11)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel1.TabIndex = 7
        Me.MyLabel1.Text = "Company"
        '
        'txtlocation
        '
        Me.txtlocation.CalculationExpression = Nothing
        Me.txtlocation.FieldCode = Nothing
        Me.txtlocation.FieldDesc = Nothing
        Me.txtlocation.FieldMaxLength = 0
        Me.txtlocation.FieldName = Nothing
        Me.txtlocation.isCalculatedField = False
        Me.txtlocation.IsSourceFromTable = False
        Me.txtlocation.IsSourceFromValueList = False
        Me.txtlocation.IsUnique = False
        Me.txtlocation.Location = New System.Drawing.Point(719, 10)
        Me.txtlocation.MendatroryField = True
        Me.txtlocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlocation.MyLinkLable1 = Nothing
        Me.txtlocation.MyLinkLable2 = Nothing
        Me.txtlocation.MyReadOnly = False
        Me.txtlocation.MyShowMasterFormButton = False
        Me.txtlocation.Name = "txtlocation"
        Me.txtlocation.ReferenceFieldDesc = Nothing
        Me.txtlocation.ReferenceFieldName = Nothing
        Me.txtlocation.ReferenceTableName = Nothing
        Me.txtlocation.Size = New System.Drawing.Size(167, 19)
        Me.txtlocation.TabIndex = 3
        Me.txtlocation.Value = ""
        Me.txtlocation.Visible = False
        '
        'RadLabel4
        '
        Me.RadLabel4.Location = New System.Drawing.Point(3, 34)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(49, 18)
        Me.RadLabel4.TabIndex = 6
        Me.RadLabel4.Text = "Location"
        '
        'btnLock
        '
        Me.btnLock.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnLock.Location = New System.Drawing.Point(6, 4)
        Me.btnLock.Name = "btnLock"
        Me.btnLock.Size = New System.Drawing.Size(85, 20)
        Me.btnLock.TabIndex = 57
        Me.btnLock.Text = "Lock"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.btnLockUser)
        Me.RadPanel1.Controls.Add(Me.btnClose)
        Me.RadPanel1.Controls.Add(Me.btnLock)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadPanel1.Location = New System.Drawing.Point(0, 376)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(951, 29)
        Me.RadPanel1.TabIndex = 58
        '
        'btnLockUser
        '
        Me.btnLockUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnLockUser.Location = New System.Drawing.Point(97, 4)
        Me.btnLockUser.Name = "btnLockUser"
        Me.btnLockUser.Size = New System.Drawing.Size(85, 20)
        Me.btnLockUser.TabIndex = 59
        Me.btnLockUser.Text = "Lock All User"
        Me.btnLockUser.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(859, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(85, 20)
        Me.btnClose.TabIndex = 58
        Me.btnClose.Text = "Close"
        '
        'chkAllLoc
        '
        Me.chkAllLoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllLoc.Location = New System.Drawing.Point(332, 35)
        Me.chkAllLoc.Name = "chkAllLoc"
        Me.chkAllLoc.Size = New System.Drawing.Size(79, 16)
        Me.chkAllLoc.TabIndex = 4
        Me.chkAllLoc.Text = "All Location"
        '
        'chkLocationCode
        '
        Me.chkLocationCode.Location = New System.Drawing.Point(332, 10)
        Me.chkLocationCode.Name = "chkLocationCode"
        Me.chkLocationCode.Size = New System.Drawing.Size(92, 18)
        Me.chkLocationCode.TabIndex = 1
        Me.chkLocationCode.Text = "Location Code"
        '
        'chkLocationSegment
        '
        Me.chkLocationSegment.Location = New System.Drawing.Point(436, 10)
        Me.chkLocationSegment.Name = "chkLocationSegment"
        Me.chkLocationSegment.Size = New System.Drawing.Size(110, 18)
        Me.chkLocationSegment.TabIndex = 2
        Me.chkLocationSegment.Text = "Location Segment"
        '
        'dtpToDate1
        '
        Me.dtpToDate1.AccessibleName = "dtpToDate"
        Me.dtpToDate1.CalculationExpression = Nothing
        Me.dtpToDate1.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate1.FieldCode = Nothing
        Me.dtpToDate1.FieldDesc = Nothing
        Me.dtpToDate1.FieldMaxLength = 0
        Me.dtpToDate1.FieldName = Nothing
        Me.dtpToDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate1.isCalculatedField = False
        Me.dtpToDate1.IsSourceFromTable = False
        Me.dtpToDate1.IsSourceFromValueList = False
        Me.dtpToDate1.IsUnique = False
        Me.dtpToDate1.Location = New System.Drawing.Point(770, 33)
        Me.dtpToDate1.MendatroryField = False
        Me.dtpToDate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate1.MyLinkLable1 = Nothing
        Me.dtpToDate1.MyLinkLable2 = Nothing
        Me.dtpToDate1.Name = "dtpToDate1"
        Me.dtpToDate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate1.ReferenceFieldDesc = Nothing
        Me.dtpToDate1.ReferenceFieldName = Nothing
        Me.dtpToDate1.ReferenceTableName = Nothing
        Me.dtpToDate1.Size = New System.Drawing.Size(82, 20)
        Me.dtpToDate1.TabIndex = 60
        Me.dtpToDate1.TabStop = False
        Me.dtpToDate1.Text = "14-09-2011"
        Me.dtpToDate1.Value = New Date(2011, 9, 14, 0, 0, 0, 0)
        '
        'dtpFromdate1
        '
        Me.dtpFromdate1.AccessibleName = "dtpFromdate"
        Me.dtpFromdate1.CalculationExpression = Nothing
        Me.dtpFromdate1.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromdate1.FieldCode = Nothing
        Me.dtpFromdate1.FieldDesc = Nothing
        Me.dtpFromdate1.FieldMaxLength = 0
        Me.dtpFromdate1.FieldName = Nothing
        Me.dtpFromdate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromdate1.isCalculatedField = False
        Me.dtpFromdate1.IsSourceFromTable = False
        Me.dtpFromdate1.IsSourceFromValueList = False
        Me.dtpFromdate1.IsUnique = False
        Me.dtpFromdate1.Location = New System.Drawing.Point(631, 33)
        Me.dtpFromdate1.MendatroryField = False
        Me.dtpFromdate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.MyLinkLable1 = Nothing
        Me.dtpFromdate1.MyLinkLable2 = Nothing
        Me.dtpFromdate1.Name = "dtpFromdate1"
        Me.dtpFromdate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.ReferenceFieldDesc = Nothing
        Me.dtpFromdate1.ReferenceFieldName = Nothing
        Me.dtpFromdate1.ReferenceTableName = Nothing
        Me.dtpFromdate1.Size = New System.Drawing.Size(82, 20)
        Me.dtpFromdate1.TabIndex = 59
        Me.dtpFromdate1.TabStop = False
        Me.dtpFromdate1.Text = "01-01-2001"
        Me.dtpFromdate1.Value = New Date(2001, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.Visible = False
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Location = New System.Drawing.Point(719, 34)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel5.TabIndex = 62
        Me.RadLabel5.Text = "To Date"
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Location = New System.Drawing.Point(566, 34)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel6.TabIndex = 61
        Me.RadLabel6.Text = "From Date"
        Me.RadLabel6.Visible = False
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(858, 33)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(59, 20)
        Me.btnGo.TabIndex = 63
        Me.btnGo.Text = ">>"
        '
        'txtLocationMult
        '
        Me.txtLocationMult.arrDispalyMember = Nothing
        Me.txtLocationMult.arrValueMember = Nothing
        Me.txtLocationMult.Location = New System.Drawing.Point(62, 34)
        Me.txtLocationMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationMult.MyLinkLable1 = Nothing
        Me.txtLocationMult.MyLinkLable2 = Nothing
        Me.txtLocationMult.MyNullText = "All"
        Me.txtLocationMult.Name = "txtLocationMult"
        Me.txtLocationMult.Size = New System.Drawing.Size(264, 19)
        Me.txtLocationMult.TabIndex = 64
        '
        'chkread
        '
        Me.chkread.Location = New System.Drawing.Point(436, 34)
        Me.chkread.MyLinkLable1 = Nothing
        Me.chkread.MyLinkLable2 = Nothing
        Me.chkread.Name = "chkread"
        Me.chkread.Size = New System.Drawing.Size(111, 18)
        Me.chkread.TabIndex = 65
        Me.chkread.Tag1 = Nothing
        Me.chkread.Text = "Apply/Unapply All"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkread)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmbCompany)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocationMult)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtlocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpToDate1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkAllLoc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpFromdate1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkLocationCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkLocationSegment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel6)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Size = New System.Drawing.Size(951, 376)
        Me.SplitContainer1.SplitterDistance = 62
        Me.SplitContainer1.TabIndex = 66
        '
        'FrmLockTransaction1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(951, 405)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadPanel1)
        Me.Name = "FrmLockTransaction1"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Lock Transaction"
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.dgvDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnLock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.btnLockUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllLoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationSegment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkread, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cmbCompany As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtlocation As common.UserControls.txtFinder
    Friend WithEvents RadLabel4 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents btnLock As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkAllLoc As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents dgvDetails As common.UserControls.MyRadGridView
    Friend WithEvents chkLocationCode As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkLocationSegment As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents dtpToDate1 As common.Controls.MyDateTimePicker
    Friend WithEvents dtpFromdate1 As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtLocationMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents chkread As common.Controls.MyCheckBox
    Friend WithEvents btnLockUser As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As SplitContainer
End Class

