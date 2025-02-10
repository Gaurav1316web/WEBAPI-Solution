<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptCattleFeedProduction
    'Inherits System.Windows.Forms.Form
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
        Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btngo = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.todate = New common.Controls.MyDateTimePicker()
        Me.txtfromDate = New common.Controls.MyDateTimePicker()
        Me.cvProdution = New Telerik.WinControls.UI.RadChartView()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.todate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cvProdution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(67, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(67, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.SplitContainer1.Panel1.Controls.Add(Me.btngo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.todate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtfromDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(67, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.SplitContainer1.Panel2.Controls.Add(Me.cvProdution)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 35
        Me.SplitContainer1.TabIndex = 0
        '
        'btngo
        '
        Me.btngo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btngo.Location = New System.Drawing.Point(326, 8)
        Me.btngo.Name = "btngo"
        Me.btngo.Size = New System.Drawing.Size(54, 22)
        Me.btngo.TabIndex = 31
        Me.btngo.Text = ">>"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.MyLabel1.Location = New System.Drawing.Point(176, 8)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel1.TabIndex = 16
        Me.MyLabel1.Text = "To Date"
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblToDate.Location = New System.Drawing.Point(12, 8)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(59, 18)
        Me.lblToDate.TabIndex = 15
        Me.lblToDate.Text = "From Date"
        '
        'todate
        '
        Me.todate.CalculationExpression = Nothing
        Me.todate.CustomFormat = "dd-MM-yyyy"
        Me.todate.FieldCode = Nothing
        Me.todate.FieldDesc = Nothing
        Me.todate.FieldMaxLength = 0
        Me.todate.FieldName = Nothing
        Me.todate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.todate.isCalculatedField = False
        Me.todate.IsSourceFromTable = False
        Me.todate.IsSourceFromValueList = False
        Me.todate.IsUnique = False
        Me.todate.Location = New System.Drawing.Point(225, 8)
        Me.todate.MendatroryField = False
        Me.todate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.todate.MyLinkLable1 = Nothing
        Me.todate.MyLinkLable2 = Nothing
        Me.todate.Name = "todate"
        Me.todate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.todate.ReferenceFieldDesc = Nothing
        Me.todate.ReferenceFieldName = Nothing
        Me.todate.ReferenceTableName = Nothing
        Me.todate.Size = New System.Drawing.Size(82, 20)
        Me.todate.TabIndex = 3
        Me.todate.TabStop = False
        Me.todate.Text = "17-12-2011"
        Me.todate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'txtfromDate
        '
        Me.txtfromDate.CalculationExpression = Nothing
        Me.txtfromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtfromDate.FieldCode = Nothing
        Me.txtfromDate.FieldDesc = Nothing
        Me.txtfromDate.FieldMaxLength = 0
        Me.txtfromDate.FieldName = Nothing
        Me.txtfromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtfromDate.isCalculatedField = False
        Me.txtfromDate.IsSourceFromTable = False
        Me.txtfromDate.IsSourceFromValueList = False
        Me.txtfromDate.IsUnique = False
        Me.txtfromDate.Location = New System.Drawing.Point(77, 7)
        Me.txtfromDate.MendatroryField = False
        Me.txtfromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtfromDate.MyLinkLable1 = Nothing
        Me.txtfromDate.MyLinkLable2 = Nothing
        Me.txtfromDate.Name = "txtfromDate"
        Me.txtfromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtfromDate.ReferenceFieldDesc = Nothing
        Me.txtfromDate.ReferenceFieldName = Nothing
        Me.txtfromDate.ReferenceTableName = Nothing
        Me.txtfromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtfromDate.TabIndex = 2
        Me.txtfromDate.TabStop = False
        Me.txtfromDate.Text = "17-12-2011"
        Me.txtfromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'cvProdution
        '
        Me.cvProdution.AreaDesign = CartesianArea1
        Me.cvProdution.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cvProdution.Location = New System.Drawing.Point(0, 0)
        Me.cvProdution.Name = "cvProdution"
        Me.cvProdution.ShowGrid = False
        Me.cvProdution.Size = New System.Drawing.Size(800, 411)
        Me.cvProdution.TabIndex = 6
        '
        'rptCattleFeedProduction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "rptCattleFeedProduction"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "rptCattleFeedProduction"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.todate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cvProdution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents txtfromDate As common.Controls.MyDateTimePicker
    Friend WithEvents todate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents cvProdution As RadChartView
    Friend WithEvents btngo As RadButton
End Class
