<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDBTPDAccountReport
    'Inherits System.Windows.Forms.Form
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
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMIALL = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtDocNo = New common.Controls.MyLabel()
        Me.txtDBTNEFTNo = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndUnion = New common.UserControls.txtFinder()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvData = New common.UserControls.MyRadGridView()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReport = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.txtDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvData.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem4, Me.RMIALL})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Settings"
        Me.RadMenuItem1.UseCompatibleTextRendering = False
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Save Layout"
        Me.RadMenuItem2.UseCompatibleTextRendering = False
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        Me.RadMenuItem4.UseCompatibleTextRendering = False
        '
        'RMIALL
        '
        Me.RMIALL.Name = "RMIALL"
        Me.RMIALL.Text = "ALL"
        Me.RMIALL.UseCompatibleTextRendering = False
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(851, 20)
        Me.RadMenu1.TabIndex = 75
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReport)
        Me.SplitContainer1.Size = New System.Drawing.Size(851, 430)
        Me.SplitContainer1.SplitterDistance = 389
        Me.SplitContainer1.TabIndex = 76
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(851, 389)
        Me.RadPageView1.TabIndex = 74
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDBTNEFTNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.fndUnion)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(830, 341)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(10, 35)
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(74, 18)
        Me.txtDocNo.TabIndex = 1080
        Me.txtDocNo.Text = "DBT NEFT No"
        '
        'txtDBTNEFTNo
        '
        Me.txtDBTNEFTNo.CalculationExpression = Nothing
        Me.txtDBTNEFTNo.FieldCode = Nothing
        Me.txtDBTNEFTNo.FieldDesc = Nothing
        Me.txtDBTNEFTNo.FieldMaxLength = 0
        Me.txtDBTNEFTNo.FieldName = Nothing
        Me.txtDBTNEFTNo.isCalculatedField = False
        Me.txtDBTNEFTNo.IsSourceFromTable = False
        Me.txtDBTNEFTNo.IsSourceFromValueList = False
        Me.txtDBTNEFTNo.IsUnique = False
        Me.txtDBTNEFTNo.Location = New System.Drawing.Point(88, 34)
        Me.txtDBTNEFTNo.MendatroryField = True
        Me.txtDBTNEFTNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDBTNEFTNo.MyLinkLable1 = Nothing
        Me.txtDBTNEFTNo.MyLinkLable2 = Nothing
        Me.txtDBTNEFTNo.MyReadOnly = False
        Me.txtDBTNEFTNo.MyShowMasterFormButton = False
        Me.txtDBTNEFTNo.Name = "txtDBTNEFTNo"
        Me.txtDBTNEFTNo.ReferenceFieldDesc = Nothing
        Me.txtDBTNEFTNo.ReferenceFieldName = Nothing
        Me.txtDBTNEFTNo.ReferenceTableName = Nothing
        Me.txtDBTNEFTNo.Size = New System.Drawing.Size(208, 18)
        Me.txtDBTNEFTNo.TabIndex = 1079
        Me.txtDBTNEFTNo.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(10, 11)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel2.TabIndex = 1078
        Me.MyLabel2.Text = "Union"
        '
        'fndUnion
        '
        Me.fndUnion.CalculationExpression = Nothing
        Me.fndUnion.FieldCode = Nothing
        Me.fndUnion.FieldDesc = Nothing
        Me.fndUnion.FieldMaxLength = 0
        Me.fndUnion.FieldName = Nothing
        Me.fndUnion.isCalculatedField = False
        Me.fndUnion.IsSourceFromTable = False
        Me.fndUnion.IsSourceFromValueList = False
        Me.fndUnion.IsUnique = False
        Me.fndUnion.Location = New System.Drawing.Point(88, 10)
        Me.fndUnion.MendatroryField = True
        Me.fndUnion.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndUnion.MyLinkLable1 = Nothing
        Me.fndUnion.MyLinkLable2 = Nothing
        Me.fndUnion.MyReadOnly = False
        Me.fndUnion.MyShowMasterFormButton = False
        Me.fndUnion.Name = "fndUnion"
        Me.fndUnion.ReferenceFieldDesc = Nothing
        Me.fndUnion.ReferenceFieldName = Nothing
        Me.fndUnion.ReferenceTableName = Nothing
        Me.fndUnion.Size = New System.Drawing.Size(208, 18)
        Me.fndUnion.TabIndex = 1077
        Me.fndUnion.Value = ""
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvData)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(830, 341)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gvData
        '
        Me.gvData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvData.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvData.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvData.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvData.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvData.MyStopExport = False
        Me.gvData.Name = "gvData"
        Me.gvData.ShowHeaderCellButtons = True
        Me.gvData.Size = New System.Drawing.Size(830, 341)
        Me.gvData.TabIndex = 0
        Me.gvData.VarID = ""
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(89, 9)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(70, 19)
        Me.btnReset.TabIndex = 1080
        Me.btnReset.Text = "Reset"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExport.Location = New System.Drawing.Point(164, 9)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(79, 19)
        Me.btnExport.TabIndex = 1079
        Me.btnExport.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        Me.rmiExcel.UseCompatibleTextRendering = False
        '
        'rmiPDF
        '
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        Me.rmiPDF.UseCompatibleTextRendering = False
        '
        'btnReport
        '
        Me.btnReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReport.Location = New System.Drawing.Point(13, 9)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(70, 19)
        Me.btnReport.TabIndex = 5
        Me.btnReport.Text = ">>>"
        '
        'frmDBTPDAccountReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(851, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmDBTPDAccountReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmDBTApprovalStatus"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.txtDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gvData.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenuItem1 As RadMenuItem
    Friend WithEvents RadMenuItem2 As RadMenuItem
    Friend WithEvents RadMenuItem4 As RadMenuItem
    Friend WithEvents RMIALL As RadMenuItem
    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents gvData As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents fndUnion As common.UserControls.txtFinder
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents btnReport As RadButton
    Friend WithEvents btnExport As RadSplitButton
    Friend WithEvents rmiExcel As RadMenuItem
    Friend WithEvents rmiPDF As RadMenuItem
    Friend WithEvents btnReset As RadButton
    Friend WithEvents txtDocNo As common.Controls.MyLabel
    Friend WithEvents txtDBTNEFTNo As common.UserControls.txtFinder
End Class
