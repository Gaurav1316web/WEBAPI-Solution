<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTimeTable
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.fndDocCode = New common.UserControls.txtNavigator()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.tpPmtToTime = New Telerik.WinControls.UI.RadTimePicker()
        Me.RadLabel10 = New Telerik.WinControls.UI.RadLabel()
        Me.tpPmtFrmTime = New Telerik.WinControls.UI.RadTimePicker()
        Me.RadLabel11 = New Telerik.WinControls.UI.RadLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.tpBkgToTime = New Telerik.WinControls.UI.RadTimePicker()
        Me.RadLabel9 = New Telerik.WinControls.UI.RadLabel()
        Me.tpBkgFrmTime = New Telerik.WinControls.UI.RadTimePicker()
        Me.RadLabel8 = New Telerik.WinControls.UI.RadLabel()
        Me.cmbDealer = New common.Controls.MyComboBox()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel7 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel4 = New Telerik.WinControls.UI.RadLabel()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.munuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.tpPmtToTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tpPmtFrmTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.tpBkgToTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tpBkgFrmTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbDealer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(599, 408)
        Me.SplitContainer1.SplitterDistance = 379
        Me.SplitContainer1.TabIndex = 3
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(599, 379)
        Me.RadPageView1.TabIndex = 18
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.fndDocCode)
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage2.Controls.Add(Me.cmbDealer)
        Me.RadPageViewPage2.Controls.Add(Me.txtDate)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel7)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage2.Controls.Add(Me.btnnew)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(71.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(578, 331)
        Me.RadPageViewPage2.Text = "Time Table"
        '
        'fndDocCode
        '
        Me.fndDocCode.FieldName = Nothing
        Me.fndDocCode.Location = New System.Drawing.Point(102, 2)
        Me.fndDocCode.MendatroryField = True
        Me.fndDocCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndDocCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocCode.MyLinkLable1 = Nothing
        Me.fndDocCode.MyLinkLable2 = Nothing
        Me.fndDocCode.MyMaxLength = 32767
        Me.fndDocCode.MyReadOnly = False
        Me.fndDocCode.Name = "fndDocCode"
        Me.fndDocCode.Size = New System.Drawing.Size(218, 21)
        Me.fndDocCode.TabIndex = 58
        Me.fndDocCode.Value = ""
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.tpPmtToTime)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel10)
        Me.RadGroupBox2.Controls.Add(Me.tpPmtFrmTime)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel11)
        Me.RadGroupBox2.HeaderText = "Payment Time"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 132)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(467, 56)
        Me.RadGroupBox2.TabIndex = 57
        Me.RadGroupBox2.Text = "Payment Time"
        '
        'tpPmtToTime
        '
        Me.tpPmtToTime.Location = New System.Drawing.Point(321, 24)
        Me.tpPmtToTime.MaxValue = New Date(9999, 12, 31, 23, 59, 59, 0)
        Me.tpPmtToTime.MinValue = New Date(CType(0, Long))
        Me.tpPmtToTime.Name = "tpPmtToTime"
        Me.tpPmtToTime.Size = New System.Drawing.Size(100, 20)
        Me.tpPmtToTime.TabIndex = 22
        Me.tpPmtToTime.TabStop = False
        Me.tpPmtToTime.Text = "1"
        Me.tpPmtToTime.Value = New Date(2016, 9, 22, 12, 36, 48, 2)
        '
        'RadLabel10
        '
        Me.RadLabel10.Location = New System.Drawing.Point(221, 24)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(46, 18)
        Me.RadLabel10.TabIndex = 21
        Me.RadLabel10.Text = "To Time"
        '
        'tpPmtFrmTime
        '
        Me.tpPmtFrmTime.Location = New System.Drawing.Point(90, 24)
        Me.tpPmtFrmTime.MaxValue = New Date(9999, 12, 31, 23, 59, 59, 0)
        Me.tpPmtFrmTime.MinValue = New Date(CType(0, Long))
        Me.tpPmtFrmTime.Name = "tpPmtFrmTime"
        Me.tpPmtFrmTime.Size = New System.Drawing.Size(116, 20)
        Me.tpPmtFrmTime.TabIndex = 20
        Me.tpPmtFrmTime.TabStop = False
        Me.tpPmtFrmTime.Text = "RadTimePicker4"
        Me.tpPmtFrmTime.Value = New Date(2016, 9, 22, 12, 36, 48, 2)
        '
        'RadLabel11
        '
        Me.RadLabel11.Location = New System.Drawing.Point(5, 24)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(60, 18)
        Me.RadLabel11.TabIndex = 19
        Me.RadLabel11.Text = "From Time"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.tpBkgToTime)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel9)
        Me.RadGroupBox1.Controls.Add(Me.tpBkgFrmTime)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel8)
        Me.RadGroupBox1.HeaderText = "Booking Time"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 74)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(467, 56)
        Me.RadGroupBox1.TabIndex = 56
        Me.RadGroupBox1.Text = "Booking Time"
        '
        'tpBkgToTime
        '
        Me.tpBkgToTime.Location = New System.Drawing.Point(324, 25)
        Me.tpBkgToTime.MaxValue = New Date(9999, 12, 31, 23, 59, 59, 0)
        Me.tpBkgToTime.MinValue = New Date(CType(0, Long))
        Me.tpBkgToTime.Name = "tpBkgToTime"
        Me.tpBkgToTime.Size = New System.Drawing.Size(100, 20)
        Me.tpBkgToTime.TabIndex = 18
        Me.tpBkgToTime.TabStop = False
        Me.tpBkgToTime.Text = "1"
        Me.tpBkgToTime.Value = New Date(2016, 9, 22, 12, 36, 48, 2)
        '
        'RadLabel9
        '
        Me.RadLabel9.Location = New System.Drawing.Point(224, 25)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(46, 18)
        Me.RadLabel9.TabIndex = 17
        Me.RadLabel9.Text = "To Time"
        '
        'tpBkgFrmTime
        '
        Me.tpBkgFrmTime.Location = New System.Drawing.Point(90, 23)
        Me.tpBkgFrmTime.MaxValue = New Date(9999, 12, 31, 23, 59, 59, 0)
        Me.tpBkgFrmTime.MinValue = New Date(CType(0, Long))
        Me.tpBkgFrmTime.Name = "tpBkgFrmTime"
        Me.tpBkgFrmTime.Size = New System.Drawing.Size(116, 20)
        Me.tpBkgFrmTime.TabIndex = 16
        Me.tpBkgFrmTime.TabStop = False
        Me.tpBkgFrmTime.Text = "RadTimePicker1"
        Me.tpBkgFrmTime.Value = New Date(2016, 9, 22, 12, 36, 48, 2)
        '
        'RadLabel8
        '
        Me.RadLabel8.Location = New System.Drawing.Point(5, 23)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(60, 18)
        Me.RadLabel8.TabIndex = 15
        Me.RadLabel8.Text = "From Time"
        '
        'cmbDealer
        '
        Me.cmbDealer.AutoCompleteDisplayMember = Nothing
        Me.cmbDealer.AutoCompleteValueMember = Nothing
        Me.cmbDealer.CalculationExpression = Nothing
        Me.cmbDealer.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbDealer.FieldCode = Nothing
        Me.cmbDealer.FieldDesc = Nothing
        Me.cmbDealer.FieldMaxLength = 0
        Me.cmbDealer.FieldName = Nothing
        Me.cmbDealer.isCalculatedField = False
        Me.cmbDealer.IsSourceFromTable = False
        Me.cmbDealer.IsSourceFromValueList = False
        Me.cmbDealer.IsUnique = False
        Me.cmbDealer.Location = New System.Drawing.Point(102, 51)
        Me.cmbDealer.MendatroryField = False
        Me.cmbDealer.MyLinkLable1 = Nothing
        Me.cmbDealer.MyLinkLable2 = Nothing
        Me.cmbDealer.Name = "cmbDealer"
        Me.cmbDealer.ReferenceFieldDesc = Nothing
        Me.cmbDealer.ReferenceFieldName = Nothing
        Me.cmbDealer.ReferenceTableName = Nothing
        Me.cmbDealer.Size = New System.Drawing.Size(221, 20)
        Me.cmbDealer.TabIndex = 55
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(102, 28)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Nothing
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(83, 18)
        Me.txtDate.TabIndex = 11
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel7
        '
        Me.RadLabel7.Location = New System.Drawing.Point(5, 53)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(66, 18)
        Me.RadLabel7.TabIndex = 4
        Me.RadLabel7.Text = "Dealer Type"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(5, 5)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(91, 18)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "Document Code "
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.XpertERPCommanServices.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(324, 3)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(18, 21)
        Me.btnnew.TabIndex = 7
        '
        'RadLabel4
        '
        Me.RadLabel4.Location = New System.Drawing.Point(5, 29)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(85, 18)
        Me.RadLabel4.TabIndex = 3
        Me.RadLabel4.Text = "Document Date"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(8, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(82, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(525, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.munuExport})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(599, 20)
        Me.RadMenu1.TabIndex = 2
        Me.RadMenu1.Text = "RadMenu1"
        '
        'munuExport
        '
        Me.munuExport.AccessibleDescription = "File"
        Me.munuExport.AccessibleName = "File"
        Me.munuExport.Name = "munuExport"
        Me.munuExport.Text = "File"
        '
        'frmTimeTable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(599, 428)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmTimeTable"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Time Table"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.tpPmtToTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tpPmtFrmTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.tpBkgToTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tpBkgFrmTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbDealer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents munuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel4 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadLabel7 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents tpPmtToTime As Telerik.WinControls.UI.RadTimePicker
    Friend WithEvents RadLabel10 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents tpPmtFrmTime As Telerik.WinControls.UI.RadTimePicker
    Friend WithEvents RadLabel11 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents tpBkgToTime As Telerik.WinControls.UI.RadTimePicker
    Friend WithEvents RadLabel9 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents tpBkgFrmTime As Telerik.WinControls.UI.RadTimePicker
    Friend WithEvents RadLabel8 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents cmbDealer As common.Controls.MyComboBox
    Friend WithEvents fndDocCode As common.UserControls.txtNavigator
End Class

