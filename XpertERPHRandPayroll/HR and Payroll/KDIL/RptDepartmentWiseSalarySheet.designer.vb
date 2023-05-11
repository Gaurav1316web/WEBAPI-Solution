Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptDepartmentWiseSalarySheet
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtDivisionMult = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtNum = New System.Windows.Forms.NumericUpDown()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtLocationMult = New common.UserControls.txtMultiSelectFinder()
        Me.txtPayHeadMult = New common.UserControls.txtMultiSelectFinder()
        Me.txtDepartmentMult = New common.UserControls.txtMultiSelectFinder()
        Me.lblType = New common.Controls.MyLabel()
        Me.ddlReportType = New Telerik.WinControls.UI.RadDropDownList()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtFromPP = New common.UserControls.txtFinder()
        Me.lblFrompp = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFrompp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(938, 508)
        Me.SplitContainer1.SplitterDistance = 465
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Location = New System.Drawing.Point(3, 3)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(932, 459)
        Me.RadPageView1.TabIndex = 72
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtDivisionMult)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtNum)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocationMult)
        Me.RadPageViewPage1.Controls.Add(Me.txtPayHeadMult)
        Me.RadPageViewPage1.Controls.Add(Me.txtDepartmentMult)
        Me.RadPageViewPage1.Controls.Add(Me.lblType)
        Me.RadPageViewPage1.Controls.Add(Me.ddlReportType)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromPP)
        Me.RadPageViewPage1.Controls.Add(Me.lblFrompp)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(911, 411)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'txtDivisionMult
        '
        Me.txtDivisionMult.arrDispalyMember = Nothing
        Me.txtDivisionMult.arrValueMember = Nothing
        Me.txtDivisionMult.Location = New System.Drawing.Point(193, 71)
        Me.txtDivisionMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDivisionMult.MyLinkLable1 = Nothing
        Me.txtDivisionMult.MyLinkLable2 = Nothing
        Me.txtDivisionMult.MyNullText = "All"
        Me.txtDivisionMult.Name = "txtDivisionMult"
        Me.txtDivisionMult.Size = New System.Drawing.Size(404, 19)
        Me.txtDivisionMult.TabIndex = 341
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(17, 72)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(46, 18)
        Me.MyLabel4.TabIndex = 340
        Me.MyLabel4.Text = "Division"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(17, 139)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(170, 18)
        Me.MyLabel3.TabIndex = 338
        Me.MyLabel3.Text = "No of Pay Heads in Each Column"
        '
        'txtNum
        '
        Me.txtNum.Location = New System.Drawing.Point(193, 138)
        Me.txtNum.Name = "txtNum"
        Me.txtNum.Size = New System.Drawing.Size(120, 20)
        Me.txtNum.TabIndex = 337
        Me.txtNum.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(17, 117)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(53, 18)
        Me.MyLabel2.TabIndex = 336
        Me.MyLabel2.Text = "Pay Head"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(17, 94)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(66, 18)
        Me.MyLabel1.TabIndex = 335
        Me.MyLabel1.Text = "Department"
        '
        'txtLocationMult
        '
        Me.txtLocationMult.arrDispalyMember = Nothing
        Me.txtLocationMult.arrValueMember = Nothing
        Me.txtLocationMult.Location = New System.Drawing.Point(193, 49)
        Me.txtLocationMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationMult.MyLinkLable1 = Nothing
        Me.txtLocationMult.MyLinkLable2 = Nothing
        Me.txtLocationMult.MyNullText = "All"
        Me.txtLocationMult.Name = "txtLocationMult"
        Me.txtLocationMult.Size = New System.Drawing.Size(404, 19)
        Me.txtLocationMult.TabIndex = 334
        '
        'txtPayHeadMult
        '
        Me.txtPayHeadMult.arrDispalyMember = Nothing
        Me.txtPayHeadMult.arrValueMember = Nothing
        Me.txtPayHeadMult.Location = New System.Drawing.Point(193, 116)
        Me.txtPayHeadMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayHeadMult.MyLinkLable1 = Nothing
        Me.txtPayHeadMult.MyLinkLable2 = Nothing
        Me.txtPayHeadMult.MyNullText = "All"
        Me.txtPayHeadMult.Name = "txtPayHeadMult"
        Me.txtPayHeadMult.Size = New System.Drawing.Size(404, 19)
        Me.txtPayHeadMult.TabIndex = 333
        '
        'txtDepartmentMult
        '
        Me.txtDepartmentMult.arrDispalyMember = Nothing
        Me.txtDepartmentMult.arrValueMember = Nothing
        Me.txtDepartmentMult.Location = New System.Drawing.Point(193, 94)
        Me.txtDepartmentMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepartmentMult.MyLinkLable1 = Nothing
        Me.txtDepartmentMult.MyLinkLable2 = Nothing
        Me.txtDepartmentMult.MyNullText = "All"
        Me.txtDepartmentMult.Name = "txtDepartmentMult"
        Me.txtDepartmentMult.Size = New System.Drawing.Size(404, 19)
        Me.txtDepartmentMult.TabIndex = 332
        '
        'lblType
        '
        Me.lblType.FieldName = Nothing
        Me.lblType.Location = New System.Drawing.Point(17, 5)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(67, 18)
        Me.lblType.TabIndex = 325
        Me.lblType.Text = "Report Type"
        '
        'ddlReportType
        '
        Me.ddlReportType.AutoCompleteDisplayMember = Nothing
        Me.ddlReportType.AutoCompleteValueMember = Nothing
        RadListDataItem1.Text = "Departmentwise"
        RadListDataItem2.Text = "Employeewise"
        Me.ddlReportType.Items.Add(RadListDataItem1)
        Me.ddlReportType.Items.Add(RadListDataItem2)
        Me.ddlReportType.Location = New System.Drawing.Point(193, 3)
        Me.ddlReportType.Name = "ddlReportType"
        Me.ddlReportType.Size = New System.Drawing.Size(219, 20)
        Me.ddlReportType.TabIndex = 324
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(17, 27)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 214
        Me.RadLabel1.Text = "Pay Period"
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(17, 50)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 221
        Me.lblLocation.Text = "Location"
        '
        'txtFromPP
        '
        Me.txtFromPP.CalculationExpression = Nothing
        Me.txtFromPP.FieldCode = Nothing
        Me.txtFromPP.FieldDesc = Nothing
        Me.txtFromPP.FieldMaxLength = 0
        Me.txtFromPP.FieldName = Nothing
        Me.txtFromPP.isCalculatedField = False
        Me.txtFromPP.IsSourceFromTable = False
        Me.txtFromPP.IsSourceFromValueList = False
        Me.txtFromPP.IsUnique = False
        Me.txtFromPP.Location = New System.Drawing.Point(193, 26)
        Me.txtFromPP.MendatroryField = True
        Me.txtFromPP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromPP.MyLinkLable1 = Me.RadLabel1
        Me.txtFromPP.MyLinkLable2 = Me.lblFrompp
        Me.txtFromPP.MyReadOnly = False
        Me.txtFromPP.MyShowMasterFormButton = False
        Me.txtFromPP.Name = "txtFromPP"
        Me.txtFromPP.ReferenceFieldDesc = Nothing
        Me.txtFromPP.ReferenceFieldName = Nothing
        Me.txtFromPP.ReferenceTableName = Nothing
        Me.txtFromPP.Size = New System.Drawing.Size(404, 19)
        Me.txtFromPP.TabIndex = 215
        Me.txtFromPP.Value = ""
        '
        'lblFrompp
        '
        Me.lblFrompp.AutoSize = False
        Me.lblFrompp.BorderVisible = True
        Me.lblFrompp.FieldName = Nothing
        Me.lblFrompp.Location = New System.Drawing.Point(406, 27)
        Me.lblFrompp.Name = "lblFrompp"
        Me.lblFrompp.Size = New System.Drawing.Size(191, 19)
        Me.lblFrompp.TabIndex = 216
        Me.lblFrompp.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(911, 424)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.Name = "Gv1"
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(911, 424)
        Me.Gv1.TabIndex = 0
        Me.Gv1.Text = "RadGridView1"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnPrint.Location = New System.Drawing.Point(194, 16)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(92, 18)
        Me.btnPrint.TabIndex = 336
        Me.btnPrint.Text = "Print"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.btnExport.Location = New System.Drawing.Point(111, 16)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(77, 18)
        Me.btnExport.TabIndex = 335
        Me.btnExport.Text = "Export"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Excel"
        Me.RadMenuItem1.AccessibleName = "Excel"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Excel"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "PDF"
        Me.RadMenuItem2.AccessibleName = "PDF"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "PDF"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnSave.Location = New System.Drawing.Point(13, 17)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(92, 18)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = ">>>"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(867, 16)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'RptDepartmentWiseSalarySheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(938, 508)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptDepartmentWiseSalarySheet"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Salary Register"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlReportType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFrompp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblFrompp As common.Controls.MyLabel
    Friend WithEvents txtFromPP As common.UserControls.txtFinder
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblType As common.Controls.MyLabel
    Friend WithEvents ddlReportType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents txtLocationMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtPayHeadMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtDepartmentMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtNum As System.Windows.Forms.NumericUpDown
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDivisionMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
End Class



