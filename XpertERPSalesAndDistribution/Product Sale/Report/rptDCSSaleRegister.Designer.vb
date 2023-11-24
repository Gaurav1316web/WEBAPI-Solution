<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class rptDCSSaleRegister
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
        Dim TableViewDefinition5 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblDCS = New common.Controls.MyLabel()
        Me.txtDCS = New common.UserControls.txtFinder()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblBilltoLocation = New common.Controls.MyLabel()
        Me.txtBilltoLocation = New common.UserControls.txtFinder()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblDocumentNo = New common.Controls.MyLabel()
        Me.txtDocumentNo = New common.UserControls.txtFinder()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New Telerik.WinControls.UI.RadGridView()
        Me.btnSplitExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblDCS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBilltoLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocumentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSplitExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSplitExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 405
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(800, 405)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.lblDCS)
        Me.RadPageViewPage1.Controls.Add(Me.txtDCS)
        Me.RadPageViewPage1.Controls.Add(Me.Label5)
        Me.RadPageViewPage1.Controls.Add(Me.lblBilltoLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtBilltoLocation)
        Me.RadPageViewPage1.Controls.Add(Me.Label4)
        Me.RadPageViewPage1.Controls.Add(Me.lblDocumentNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocumentNo)
        Me.RadPageViewPage1.Controls.Add(Me.Label3)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 357)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'lblDCS
        '
        Me.lblDCS.AutoSize = False
        Me.lblDCS.BorderVisible = True
        Me.lblDCS.FieldName = Nothing
        Me.lblDCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDCS.Location = New System.Drawing.Point(250, 105)
        Me.lblDCS.Name = "lblDCS"
        Me.lblDCS.Size = New System.Drawing.Size(187, 18)
        Me.lblDCS.TabIndex = 50
        Me.lblDCS.TextWrap = False
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
        Me.txtDCS.Location = New System.Drawing.Point(89, 105)
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
        Me.txtDCS.Size = New System.Drawing.Size(155, 18)
        Me.txtDCS.TabIndex = 49
        Me.txtDCS.Value = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 81)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 13)
        Me.Label5.TabIndex = 48
        Me.Label5.Text = "Docoment No"
        '
        'lblBilltoLocation
        '
        Me.lblBilltoLocation.AutoSize = False
        Me.lblBilltoLocation.BorderVisible = True
        Me.lblBilltoLocation.FieldName = Nothing
        Me.lblBilltoLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBilltoLocation.Location = New System.Drawing.Point(250, 129)
        Me.lblBilltoLocation.Name = "lblBilltoLocation"
        Me.lblBilltoLocation.Size = New System.Drawing.Size(187, 18)
        Me.lblBilltoLocation.TabIndex = 47
        Me.lblBilltoLocation.TextWrap = False
        '
        'txtBilltoLocation
        '
        Me.txtBilltoLocation.CalculationExpression = Nothing
        Me.txtBilltoLocation.FieldCode = Nothing
        Me.txtBilltoLocation.FieldDesc = Nothing
        Me.txtBilltoLocation.FieldMaxLength = 0
        Me.txtBilltoLocation.FieldName = Nothing
        Me.txtBilltoLocation.isCalculatedField = False
        Me.txtBilltoLocation.IsSourceFromTable = False
        Me.txtBilltoLocation.IsSourceFromValueList = False
        Me.txtBilltoLocation.IsUnique = False
        Me.txtBilltoLocation.Location = New System.Drawing.Point(89, 129)
        Me.txtBilltoLocation.MendatroryField = True
        Me.txtBilltoLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBilltoLocation.MyLinkLable1 = Nothing
        Me.txtBilltoLocation.MyLinkLable2 = Nothing
        Me.txtBilltoLocation.MyReadOnly = False
        Me.txtBilltoLocation.MyShowMasterFormButton = False
        Me.txtBilltoLocation.Name = "txtBilltoLocation"
        Me.txtBilltoLocation.ReferenceFieldDesc = Nothing
        Me.txtBilltoLocation.ReferenceFieldName = Nothing
        Me.txtBilltoLocation.ReferenceTableName = Nothing
        Me.txtBilltoLocation.Size = New System.Drawing.Size(155, 18)
        Me.txtBilltoLocation.TabIndex = 46
        Me.txtBilltoLocation.Value = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 106)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 45
        Me.Label4.Text = "DCS"
        '
        'lblDocumentNo
        '
        Me.lblDocumentNo.AutoSize = False
        Me.lblDocumentNo.BorderVisible = True
        Me.lblDocumentNo.FieldName = Nothing
        Me.lblDocumentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocumentNo.Location = New System.Drawing.Point(250, 81)
        Me.lblDocumentNo.Name = "lblDocumentNo"
        Me.lblDocumentNo.Size = New System.Drawing.Size(187, 18)
        Me.lblDocumentNo.TabIndex = 44
        Me.lblDocumentNo.TextWrap = False
        '
        'txtDocumentNo
        '
        Me.txtDocumentNo.CalculationExpression = Nothing
        Me.txtDocumentNo.FieldCode = Nothing
        Me.txtDocumentNo.FieldDesc = Nothing
        Me.txtDocumentNo.FieldMaxLength = 0
        Me.txtDocumentNo.FieldName = Nothing
        Me.txtDocumentNo.isCalculatedField = False
        Me.txtDocumentNo.IsSourceFromTable = False
        Me.txtDocumentNo.IsSourceFromValueList = False
        Me.txtDocumentNo.IsUnique = False
        Me.txtDocumentNo.Location = New System.Drawing.Point(89, 81)
        Me.txtDocumentNo.MendatroryField = True
        Me.txtDocumentNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocumentNo.MyLinkLable1 = Nothing
        Me.txtDocumentNo.MyLinkLable2 = Nothing
        Me.txtDocumentNo.MyReadOnly = False
        Me.txtDocumentNo.MyShowMasterFormButton = False
        Me.txtDocumentNo.Name = "txtDocumentNo"
        Me.txtDocumentNo.ReferenceFieldDesc = Nothing
        Me.txtDocumentNo.ReferenceFieldName = Nothing
        Me.txtDocumentNo.ReferenceTableName = Nothing
        Me.txtDocumentNo.Size = New System.Drawing.Size(155, 18)
        Me.txtDocumentNo.TabIndex = 43
        Me.txtDocumentNo.Value = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 131)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Location"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.Label2)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 16)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(352, 52)
        Me.RadGroupBox1.TabIndex = 0
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
        Me.txtToDate.Location = New System.Drawing.Point(238, 16)
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
        Me.txtToDate.TabIndex = 5
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13-07-2023"
        Me.txtToDate.Value = New Date(2023, 7, 13, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(186, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "To Date"
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
        Me.txtFromDate.Location = New System.Drawing.Point(71, 16)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 3
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13-07-2023"
        Me.txtFromDate.Value = New Date(2023, 7, 13, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(779, 357)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition5
        Me.Gv1.Name = "Gv1"
        Me.Gv1.Size = New System.Drawing.Size(779, 357)
        Me.Gv1.TabIndex = 0
        '
        'btnSplitExport
        '
        Me.btnSplitExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSplitExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnSplitExport.Location = New System.Drawing.Point(143, 12)
        Me.btnSplitExport.Name = "btnSplitExport"
        Me.btnSplitExport.Size = New System.Drawing.Size(95, 17)
        Me.btnSplitExport.TabIndex = 46
        Me.btnSplitExport.Text = "Export"
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
        'BtnReset
        '
        Me.BtnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReset.Location = New System.Drawing.Point(79, 12)
        Me.BtnReset.Name = "BtnReset"
        Me.BtnReset.Size = New System.Drawing.Size(62, 17)
        Me.BtnReset.TabIndex = 44
        Me.BtnReset.Text = "Reset"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(709, 12)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(71, 17)
        Me.btnclose.TabIndex = 45
        Me.btnclose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(20, 12)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(57, 17)
        Me.btnGo.TabIndex = 43
        Me.btnGo.Text = ">>"
        '
        'rptDCSSaleRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "rptDCSSaleRegister"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "rptDCSSaleRegister"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblDCS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBilltoLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocumentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSplitExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents lblDCS As common.Controls.MyLabel
    Friend WithEvents txtDCS As common.UserControls.txtFinder
    Friend WithEvents Label5 As Label
    Friend WithEvents lblBilltoLocation As common.Controls.MyLabel
    Friend WithEvents txtBilltoLocation As common.UserControls.txtFinder
    Friend WithEvents Label4 As Label
    Friend WithEvents lblDocumentNo As common.Controls.MyLabel
    Friend WithEvents txtDocumentNo As common.UserControls.txtFinder
    Friend WithEvents Label3 As Label
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents Gv1 As RadGridView
    Friend WithEvents btnSplitExport As RadSplitButton
    Friend WithEvents rmiExcel As RadMenuItem
    Friend WithEvents rmiPDF As RadMenuItem
    Friend WithEvents BtnReset As RadButton
    Friend WithEvents btnclose As RadButton
    Friend WithEvents btnGo As RadButton
End Class
