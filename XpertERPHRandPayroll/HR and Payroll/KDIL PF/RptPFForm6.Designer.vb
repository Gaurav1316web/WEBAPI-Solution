Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptPFForm6
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtLocCode = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtToYear = New common.Controls.MyDateTimePicker()
        Me.lblToYear = New common.Controls.MyLabel()
        Me.txtFromYear = New common.Controls.MyDateTimePicker()
        Me.lblYear = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.FndLocationCode = New common.UserControls.txtFinder()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.txtDivisionMult = New common.UserControls.txtMultiSelectFinder()
        Me.lblDivision = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(1120, 429)
        Me.SplitContainer1.SplitterDistance = 391
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        '
        '
        '
        Me.RadPageView1.RootElement.AccessibleDescription = "Report1"
        Me.RadPageView1.RootElement.AccessibleName = "Report1"
        Me.RadPageView1.RootElement.Text = "Report1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1120, 391)
        Me.RadPageView1.TabIndex = 9
        Me.RadPageView1.TabStop = False
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocationName)
        Me.RadPageViewPage1.Controls.Add(Me.FndLocationCode)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1099, 343)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtDivisionMult)
        Me.GroupBox1.Controls.Add(Me.lblDivision)
        Me.GroupBox1.Controls.Add(Me.txtLocCode)
        Me.GroupBox1.Controls.Add(Me.MyLabel2)
        Me.GroupBox1.Controls.Add(Me.txtToYear)
        Me.GroupBox1.Controls.Add(Me.lblToYear)
        Me.GroupBox1.Controls.Add(Me.txtFromYear)
        Me.GroupBox1.Controls.Add(Me.lblYear)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(450, 104)
        Me.GroupBox1.TabIndex = 60
        Me.GroupBox1.TabStop = False
        '
        'txtLocCode
        '
        Me.txtLocCode.arrDispalyMember = Nothing
        Me.txtLocCode.arrValueMember = Nothing
        Me.txtLocCode.Location = New System.Drawing.Point(80, 44)
        Me.txtLocCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocCode.MyLinkLable1 = Nothing
        Me.txtLocCode.MyLinkLable2 = Nothing
        Me.txtLocCode.MyNullText = "All"
        Me.txtLocCode.Name = "txtLocCode"
        Me.txtLocCode.Size = New System.Drawing.Size(365, 19)
        Me.txtLocCode.TabIndex = 347
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(14, 47)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel2.TabIndex = 346
        Me.MyLabel2.Text = "Location"
        '
        'txtToYear
        '
        Me.txtToYear.CalculationExpression = Nothing
        Me.txtToYear.CustomFormat = "yyyy"
        Me.txtToYear.FieldCode = Nothing
        Me.txtToYear.FieldDesc = Nothing
        Me.txtToYear.FieldMaxLength = 0
        Me.txtToYear.FieldName = Nothing
        Me.txtToYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToYear.isCalculatedField = False
        Me.txtToYear.IsSourceFromTable = False
        Me.txtToYear.IsSourceFromValueList = False
        Me.txtToYear.IsUnique = False
        Me.txtToYear.Location = New System.Drawing.Point(245, 21)
        Me.txtToYear.MendatroryField = False
        Me.txtToYear.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToYear.MyLinkLable1 = Nothing
        Me.txtToYear.MyLinkLable2 = Nothing
        Me.txtToYear.Name = "txtToYear"
        Me.txtToYear.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToYear.ReferenceFieldDesc = Nothing
        Me.txtToYear.ReferenceFieldName = Nothing
        Me.txtToYear.ReferenceTableName = Nothing
        Me.txtToYear.Size = New System.Drawing.Size(82, 20)
        Me.txtToYear.TabIndex = 67
        Me.txtToYear.TabStop = False
        Me.txtToYear.Text = "2011"
        Me.txtToYear.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblToYear
        '
        Me.lblToYear.FieldName = Nothing
        Me.lblToYear.Location = New System.Drawing.Point(179, 22)
        Me.lblToYear.Name = "lblToYear"
        Me.lblToYear.Size = New System.Drawing.Size(47, 18)
        Me.lblToYear.TabIndex = 66
        Me.lblToYear.Text = " To Year"
        '
        'txtFromYear
        '
        Me.txtFromYear.CalculationExpression = Nothing
        Me.txtFromYear.CustomFormat = "yyyy"
        Me.txtFromYear.FieldCode = Nothing
        Me.txtFromYear.FieldDesc = Nothing
        Me.txtFromYear.FieldMaxLength = 0
        Me.txtFromYear.FieldName = Nothing
        Me.txtFromYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromYear.isCalculatedField = False
        Me.txtFromYear.IsSourceFromTable = False
        Me.txtFromYear.IsSourceFromValueList = False
        Me.txtFromYear.IsUnique = False
        Me.txtFromYear.Location = New System.Drawing.Point(80, 20)
        Me.txtFromYear.MendatroryField = False
        Me.txtFromYear.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromYear.MyLinkLable1 = Nothing
        Me.txtFromYear.MyLinkLable2 = Nothing
        Me.txtFromYear.Name = "txtFromYear"
        Me.txtFromYear.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromYear.ReferenceFieldDesc = Nothing
        Me.txtFromYear.ReferenceFieldName = Nothing
        Me.txtFromYear.ReferenceTableName = Nothing
        Me.txtFromYear.Size = New System.Drawing.Size(82, 20)
        Me.txtFromYear.TabIndex = 62
        Me.txtFromYear.TabStop = False
        Me.txtFromYear.Text = "2011"
        Me.txtFromYear.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblYear
        '
        Me.lblYear.FieldName = Nothing
        Me.lblYear.Location = New System.Drawing.Point(14, 21)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(60, 18)
        Me.lblYear.TabIndex = 61
        Me.lblYear.Text = " From Year"
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblLocation.Location = New System.Drawing.Point(83, 160)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 64
        Me.lblLocation.Text = "Location"
        Me.lblLocation.Visible = False
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblLocationName.Location = New System.Drawing.Point(309, 158)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(183, 18)
        Me.lblLocationName.TabIndex = 65
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocationName.TextWrap = False
        Me.lblLocationName.Visible = False
        '
        'FndLocationCode
        '
        Me.FndLocationCode.CalculationExpression = Nothing
        Me.FndLocationCode.FieldCode = Nothing
        Me.FndLocationCode.FieldDesc = Nothing
        Me.FndLocationCode.FieldMaxLength = 0
        Me.FndLocationCode.FieldName = Nothing
        Me.FndLocationCode.isCalculatedField = False
        Me.FndLocationCode.IsSourceFromTable = False
        Me.FndLocationCode.IsSourceFromValueList = False
        Me.FndLocationCode.IsUnique = False
        Me.FndLocationCode.Location = New System.Drawing.Point(144, 158)
        Me.FndLocationCode.MendatroryField = False
        Me.FndLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndLocationCode.MyLinkLable1 = Me.lblLocation
        Me.FndLocationCode.MyLinkLable2 = Me.lblLocationName
        Me.FndLocationCode.MyReadOnly = False
        Me.FndLocationCode.MyShowMasterFormButton = False
        Me.FndLocationCode.Name = "FndLocationCode"
        Me.FndLocationCode.ReferenceFieldDesc = Nothing
        Me.FndLocationCode.ReferenceFieldName = Nothing
        Me.FndLocationCode.ReferenceTableName = Nothing
        Me.FndLocationCode.Size = New System.Drawing.Size(159, 18)
        Me.FndLocationCode.TabIndex = 63
        Me.FndLocationCode.Value = ""
        Me.FndLocationCode.Visible = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1025, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(84, 22)
        Me.btnclose.TabIndex = 25
        Me.btnclose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(8, 6)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 24
        Me.btnGo.Text = ">>>"
        '
        'txtDivisionMult
        '
        Me.txtDivisionMult.arrDispalyMember = Nothing
        Me.txtDivisionMult.arrValueMember = Nothing
        Me.txtDivisionMult.Location = New System.Drawing.Point(80, 68)
        Me.txtDivisionMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDivisionMult.MyLinkLable1 = Nothing
        Me.txtDivisionMult.MyLinkLable2 = Nothing
        Me.txtDivisionMult.MyNullText = "All"
        Me.txtDivisionMult.Name = "txtDivisionMult"
        Me.txtDivisionMult.Size = New System.Drawing.Size(365, 19)
        Me.txtDivisionMult.TabIndex = 358
        '
        'lblDivision
        '
        Me.lblDivision.FieldName = Nothing
        Me.lblDivision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDivision.Location = New System.Drawing.Point(13, 71)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.Size = New System.Drawing.Size(50, 16)
        Me.lblDivision.TabIndex = 357
        Me.lblDivision.Text = "Division"
        '
        'RptPFForm6
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1120, 429)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptPFForm6"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptPFForm6"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtFromYear As common.Controls.MyDateTimePicker
    Friend WithEvents lblYear As common.Controls.MyLabel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents FndLocationCode As common.UserControls.txtFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents txtToYear As common.Controls.MyDateTimePicker
    Friend WithEvents lblToYear As common.Controls.MyLabel
    Friend WithEvents txtLocCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtDivisionMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblDivision As common.Controls.MyLabel
End Class

