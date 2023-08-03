<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class QualitySummaryReport
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageVieww1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPagee1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lablBillToLocation = New common.Controls.MyLabel()
        Me.txttBillToLocation = New common.UserControls.txtFinder()
        Me.TxttRAL = New common.UserControls.txtMultiSelectFinder()
        Me.Labell4 = New System.Windows.Forms.Label()
        Me.Labell3 = New System.Windows.Forms.Label()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyDateTimePicker1 = New common.Controls.MyDateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.MyDateTimePicker2 = New common.Controls.MyDateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gvv1 = New Telerik.WinControls.UI.RadGridView()
        Me.btnnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnnSplitExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnnGo = New Telerik.WinControls.UI.RadButton()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageVieww1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageVieww1.SuspendLayout()
        Me.RadPageViewPagee1.SuspendLayout()
        CType(Me.lablBillToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyDateTimePicker1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyDateTimePicker2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gvv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gvv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnSplitExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageVieww1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnnSplitExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 402
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageVieww1
        '
        Me.RadPageVieww1.Controls.Add(Me.RadPageViewPagee1)
        Me.RadPageVieww1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageVieww1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageVieww1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageVieww1.Name = "RadPageVieww1"
        Me.RadPageVieww1.SelectedPage = Me.RadPageViewPagee1
        Me.RadPageVieww1.Size = New System.Drawing.Size(800, 402)
        Me.RadPageVieww1.TabIndex = 1
        CType(Me.RadPageVieww1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPagee1
        '
        Me.RadPageViewPagee1.Controls.Add(Me.lablBillToLocation)
        Me.RadPageViewPagee1.Controls.Add(Me.txttBillToLocation)
        Me.RadPageViewPagee1.Controls.Add(Me.TxttRAL)
        Me.RadPageViewPagee1.Controls.Add(Me.Labell4)
        Me.RadPageViewPagee1.Controls.Add(Me.Labell3)
        Me.RadPageViewPagee1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPagee1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPagee1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPagee1.Name = "RadPageViewPagee1"
        Me.RadPageViewPagee1.Size = New System.Drawing.Size(779, 354)
        Me.RadPageViewPagee1.Text = "Filter"
        '
        'lablBillToLocation
        '
        Me.lablBillToLocation.AutoSize = False
        Me.lablBillToLocation.BorderVisible = True
        Me.lablBillToLocation.FieldName = Nothing
        Me.lablBillToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lablBillToLocation.Location = New System.Drawing.Point(180, 33)
        Me.lablBillToLocation.Name = "lablBillToLocation"
        Me.lablBillToLocation.Size = New System.Drawing.Size(187, 18)
        Me.lablBillToLocation.TabIndex = 40
        Me.lablBillToLocation.TextWrap = False
        '
        'txttBillToLocation
        '
        Me.txttBillToLocation.CalculationExpression = Nothing
        Me.txttBillToLocation.FieldCode = Nothing
        Me.txttBillToLocation.FieldDesc = Nothing
        Me.txttBillToLocation.FieldMaxLength = 0
        Me.txttBillToLocation.FieldName = Nothing
        Me.txttBillToLocation.isCalculatedField = False
        Me.txttBillToLocation.IsSourceFromTable = False
        Me.txttBillToLocation.IsSourceFromValueList = False
        Me.txttBillToLocation.IsUnique = False
        Me.txttBillToLocation.Location = New System.Drawing.Point(75, 33)
        Me.txttBillToLocation.MendatroryField = True
        Me.txttBillToLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttBillToLocation.MyLinkLable1 = Nothing
        Me.txttBillToLocation.MyLinkLable2 = Nothing
        Me.txttBillToLocation.MyReadOnly = False
        Me.txttBillToLocation.MyShowMasterFormButton = False
        Me.txttBillToLocation.Name = "txttBillToLocation"
        Me.txttBillToLocation.ReferenceFieldDesc = Nothing
        Me.txttBillToLocation.ReferenceFieldName = Nothing
        Me.txttBillToLocation.ReferenceTableName = Nothing
        Me.txttBillToLocation.Size = New System.Drawing.Size(99, 18)
        Me.txttBillToLocation.TabIndex = 29
        Me.txttBillToLocation.Value = ""
        '
        'TxttRAL
        '
        Me.TxttRAL.arrDispalyMember = Nothing
        Me.TxttRAL.arrValueMember = Nothing
        Me.TxttRAL.Location = New System.Drawing.Point(75, 57)
        Me.TxttRAL.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxttRAL.MyLinkLable1 = Nothing
        Me.TxttRAL.MyLinkLable2 = Nothing
        Me.TxttRAL.MyNullText = "All"
        Me.TxttRAL.Name = "TxttRAL"
        Me.TxttRAL.Size = New System.Drawing.Size(292, 21)
        Me.TxttRAL.TabIndex = 26
        '
        'Labell4
        '
        Me.Labell4.AutoSize = True
        Me.Labell4.Location = New System.Drawing.Point(18, 60)
        Me.Labell4.Name = "Labell4"
        Me.Labell4.Size = New System.Drawing.Size(26, 13)
        Me.Labell4.TabIndex = 25
        Me.Labell4.Text = "RAL"
        '
        'Labell3
        '
        Me.Labell3.AutoSize = True
        Me.Labell3.Location = New System.Drawing.Point(18, 33)
        Me.Labell3.Name = "Labell3"
        Me.Labell3.Size = New System.Drawing.Size(51, 13)
        Me.Labell3.TabIndex = 3
        Me.Labell3.Text = "Location"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.MyDateTimePicker1)
        Me.RadGroupBox1.Controls.Add(Me.Label2)
        Me.RadGroupBox1.Controls.Add(Me.MyDateTimePicker2)
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(411, 297)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(346, 37)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Visible = False
        '
        'MyDateTimePicker1
        '
        Me.MyDateTimePicker1.CalculationExpression = Nothing
        Me.MyDateTimePicker1.CustomFormat = "dd-MM-yyyy"
        Me.MyDateTimePicker1.FieldCode = Nothing
        Me.MyDateTimePicker1.FieldDesc = Nothing
        Me.MyDateTimePicker1.FieldMaxLength = 0
        Me.MyDateTimePicker1.FieldName = Nothing
        Me.MyDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.MyDateTimePicker1.isCalculatedField = False
        Me.MyDateTimePicker1.IsSourceFromTable = False
        Me.MyDateTimePicker1.IsSourceFromValueList = False
        Me.MyDateTimePicker1.IsUnique = False
        Me.MyDateTimePicker1.Location = New System.Drawing.Point(238, 9)
        Me.MyDateTimePicker1.MendatroryField = False
        Me.MyDateTimePicker1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.MyDateTimePicker1.MyLinkLable1 = Nothing
        Me.MyDateTimePicker1.MyLinkLable2 = Nothing
        Me.MyDateTimePicker1.Name = "MyDateTimePicker1"
        Me.MyDateTimePicker1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.MyDateTimePicker1.ReferenceFieldDesc = Nothing
        Me.MyDateTimePicker1.ReferenceFieldName = Nothing
        Me.MyDateTimePicker1.ReferenceTableName = Nothing
        Me.MyDateTimePicker1.Size = New System.Drawing.Size(82, 20)
        Me.MyDateTimePicker1.TabIndex = 6
        Me.MyDateTimePicker1.TabStop = False
        Me.MyDateTimePicker1.Text = "13-07-2023"
        Me.MyDateTimePicker1.Value = New Date(2023, 7, 13, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(186, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "To Date"
        '
        'MyDateTimePicker2
        '
        Me.MyDateTimePicker2.CalculationExpression = Nothing
        Me.MyDateTimePicker2.CustomFormat = "dd-MM-yyyy"
        Me.MyDateTimePicker2.FieldCode = Nothing
        Me.MyDateTimePicker2.FieldDesc = Nothing
        Me.MyDateTimePicker2.FieldMaxLength = 0
        Me.MyDateTimePicker2.FieldName = Nothing
        Me.MyDateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.MyDateTimePicker2.isCalculatedField = False
        Me.MyDateTimePicker2.IsSourceFromTable = False
        Me.MyDateTimePicker2.IsSourceFromValueList = False
        Me.MyDateTimePicker2.IsUnique = False
        Me.MyDateTimePicker2.Location = New System.Drawing.Point(71, 9)
        Me.MyDateTimePicker2.MendatroryField = False
        Me.MyDateTimePicker2.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.MyDateTimePicker2.MyLinkLable1 = Nothing
        Me.MyDateTimePicker2.MyLinkLable2 = Nothing
        Me.MyDateTimePicker2.Name = "MyDateTimePicker2"
        Me.MyDateTimePicker2.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.MyDateTimePicker2.ReferenceFieldDesc = Nothing
        Me.MyDateTimePicker2.ReferenceFieldName = Nothing
        Me.MyDateTimePicker2.ReferenceTableName = Nothing
        Me.MyDateTimePicker2.Size = New System.Drawing.Size(82, 20)
        Me.MyDateTimePicker2.TabIndex = 4
        Me.MyDateTimePicker2.TabStop = False
        Me.MyDateTimePicker2.Text = "13-07-2023"
        Me.MyDateTimePicker2.Value = New Date(2023, 7, 13, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gvv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(779, 354)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gvv1
        '
        Me.Gvv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gvv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gvv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.Gvv1.Name = "Gvv1"
        Me.Gvv1.Size = New System.Drawing.Size(779, 354)
        Me.Gvv1.TabIndex = 0
        '
        'btnnclose
        '
        Me.btnnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnclose.Location = New System.Drawing.Point(717, 13)
        Me.btnnclose.Name = "btnnclose"
        Me.btnnclose.Size = New System.Drawing.Size(71, 17)
        Me.btnnclose.TabIndex = 54
        Me.btnnclose.Text = "Close"
        '
        'btnnPrint
        '
        Me.btnnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnPrint.Location = New System.Drawing.Point(237, 13)
        Me.btnnPrint.Name = "btnnPrint"
        Me.btnnPrint.Size = New System.Drawing.Size(62, 17)
        Me.btnnPrint.TabIndex = 53
        Me.btnnPrint.Text = "Print"
        '
        'btnnSplitExport
        '
        Me.btnnSplitExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnnSplitExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnnSplitExport.Location = New System.Drawing.Point(138, 13)
        Me.btnnSplitExport.Name = "btnnSplitExport"
        Me.btnnSplitExport.Size = New System.Drawing.Size(95, 17)
        Me.btnnSplitExport.TabIndex = 52
        Me.btnnSplitExport.Text = "Export"
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
        'BtnnReset
        '
        Me.BtnnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnnReset.Location = New System.Drawing.Point(72, 13)
        Me.BtnnReset.Name = "BtnnReset"
        Me.BtnnReset.Size = New System.Drawing.Size(62, 17)
        Me.BtnnReset.TabIndex = 50
        Me.BtnnReset.Text = "Reset"
        '
        'btnnGo
        '
        Me.btnnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnGo.Location = New System.Drawing.Point(11, 13)
        Me.btnnGo.Name = "btnnGo"
        Me.btnnGo.Size = New System.Drawing.Size(57, 17)
        Me.btnnGo.TabIndex = 49
        Me.btnnGo.Text = ">>"
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
        Me.txtFromDate.Location = New System.Drawing.Point(71, 9)
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
        Me.txtFromDate.TabIndex = 4
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13-07-2023"
        Me.txtFromDate.Value = New Date(2023, 7, 13, 0, 0, 0, 0)
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
        Me.txtToDate.Location = New System.Drawing.Point(238, 9)
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
        Me.txtToDate.TabIndex = 6
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13-07-2023"
        Me.txtToDate.Value = New Date(2023, 7, 13, 0, 0, 0, 0)
        '
        'QualitySummaryReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "QualitySummaryReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "QualitySummaryReport"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageVieww1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageVieww1.ResumeLayout(False)
        Me.RadPageViewPagee1.ResumeLayout(False)
        Me.RadPageViewPagee1.PerformLayout()
        CType(Me.lablBillToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyDateTimePicker1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyDateTimePicker2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gvv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gvv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnSplitExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnnGo As RadButton
    Friend WithEvents BtnnReset As RadButton
    Friend WithEvents btnnSplitExport As RadSplitButton
    Friend WithEvents rmiExcel As RadMenuItem
    Friend WithEvents rmiPDF As RadMenuItem
    Friend WithEvents btnnPrint As RadButton
    Friend WithEvents btnnclose As RadButton
    Friend WithEvents RadPageVieww1 As RadPageView
    Friend WithEvents RadPageViewPagee1 As RadPageViewPage
    Friend WithEvents lablBillToLocation As common.Controls.MyLabel
    Friend WithEvents txttBillToLocation As common.UserControls.txtFinder
    Friend WithEvents TxttRAL As common.UserControls.txtMultiSelectFinder
    Friend WithEvents Labell4 As Label
    Friend WithEvents Labell3 As Label
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents MyDateTimePicker1 As common.Controls.MyDateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents MyDateTimePicker2 As common.Controls.MyDateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents Gvv1 As RadGridView
End Class
