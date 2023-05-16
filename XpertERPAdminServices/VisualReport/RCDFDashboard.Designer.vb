<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RCDFDashboard
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
        Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.gvProdution = New Telerik.WinControls.UI.RadGridView()
        Me.cvProdution = New Telerik.WinControls.UI.RadChartView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.gvProdution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvProdution.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cvProdution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(67, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel15)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblfromDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFromDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtToDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Size = New System.Drawing.Size(859, 536)
        Me.SplitContainer1.SplitterDistance = 34
        Me.SplitContainer1.TabIndex = 0
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.RadLabel15.Location = New System.Drawing.Point(127, 8)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 33
        Me.RadLabel15.Text = "Location"
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(181, 7)
        Me.txtLocation.MendatroryField = False
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(143, 19)
        Me.txtLocation.TabIndex = 32
        Me.txtLocation.Value = ""
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(386, 5)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(55, 22)
        Me.btnReset.TabIndex = 31
        Me.btnReset.Text = "Reset"
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(328, 5)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(54, 22)
        Me.RadButton1.TabIndex = 30
        Me.RadButton1.Text = ">>"
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblfromDate.Location = New System.Drawing.Point(449, 9)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblfromDate.TabIndex = 13
        Me.lblfromDate.Text = "From Date"
        Me.lblfromDate.Visible = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(772, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(84, 22)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(512, 8)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReadOnly = True
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "17-12-2011"
        Me.txtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        Me.txtFromDate.Visible = False
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblToDate.Location = New System.Drawing.Point(6, 7)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(30, 18)
        Me.lblToDate.TabIndex = 14
        Me.lblToDate.Text = "Date"
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(41, 6)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "17-12-2011"
        Me.txtToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.PageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(67, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(859, 498)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.ViewMode = Telerik.WinControls.UI.PageViewMode.Backstage
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewBackstageElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Center
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewBackstageElement).ItemFitMode = Telerik.WinControls.UI.StripViewItemFitMode.Fill
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewBackstageElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewBackstageElement).ItemSizeMode = Telerik.WinControls.UI.PageViewItemSizeMode.EqualHeight
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewBackstageElement).ItemContentOrientation = Telerik.WinControls.UI.PageViewContentOrientation.Horizontal
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(183.0!, 45.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(5, 60)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(850, 434)
        Me.RadPageViewPage1.Text = "RAW MATERIAL"
        Me.RadPageViewPage1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Attachments
        '
        Me.Attachments.ItemSize = New System.Drawing.SizeF(179.0!, 45.0!)
        Me.Attachments.Location = New System.Drawing.Point(5, 60)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(850, 434)
        Me.Attachments.Text = "FINISH GOODS"
        Me.Attachments.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.RadPageViewPage2.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage2.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage2.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(173.0!, 45.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(5, 60)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(850, 434)
        Me.RadPageViewPage2.Text = "PRODUCTION"
        Me.RadPageViewPage2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.gvProdution)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.cvProdution)
        Me.SplitContainer2.Size = New System.Drawing.Size(846, 430)
        Me.SplitContainer2.SplitterDistance = 271
        Me.SplitContainer2.TabIndex = 0
        '
        'gvProdution
        '
        Me.gvProdution.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvProdution.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvProdution.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvProdution.Name = "gvProdution"
        Me.gvProdution.Size = New System.Drawing.Size(846, 271)
        Me.gvProdution.TabIndex = 6
        '
        'cvProdution
        '
        Me.cvProdution.AreaDesign = CartesianArea1
        Me.cvProdution.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cvProdution.Location = New System.Drawing.Point(0, 0)
        Me.cvProdution.Name = "cvProdution"
        Me.cvProdution.ShowGrid = False
        Me.cvProdution.Size = New System.Drawing.Size(846, 155)
        Me.cvProdution.TabIndex = 5
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(145.0!, 45.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(5, 60)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(850, 434)
        Me.RadPageViewPage3.Text = "QUALITY"
        Me.RadPageViewPage3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(153.0!, 45.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(5, 60)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(850, 434)
        Me.RadPageViewPage4.Text = "ACCOUNT"
        Me.RadPageViewPage4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RCDFDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(859, 536)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RCDFDashboard"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Sale Visual Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.gvProdution.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvProdution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cvProdution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents Attachments As RadPageViewPage
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents gvProdution As RadGridView
    Friend WithEvents cvProdution As RadChartView
    Friend WithEvents RadPageViewPage3 As RadPageViewPage
    Friend WithEvents RadPageViewPage4 As RadPageViewPage
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
End Class

