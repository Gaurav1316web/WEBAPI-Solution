Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptPayrollPerformaforcontribution
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtTodate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtfromDate = New common.Controls.MyDateTimePicker()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.FndLocationCode = New common.UserControls.txtFinder()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.txtMultDivision = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Size = New System.Drawing.Size(474, 343)
        Me.SplitContainer1.SplitterDistance = 288
        Me.SplitContainer1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtMultDivision)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.txtLocation)
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Controls.Add(Me.lblLocation)
        Me.Panel1.Controls.Add(Me.txtTodate)
        Me.Panel1.Controls.Add(Me.MyLabel1)
        Me.Panel1.Controls.Add(Me.txtfromDate)
        Me.Panel1.Controls.Add(Me.lblfromDate)
        Me.Panel1.Controls.Add(Me.lblLocationName)
        Me.Panel1.Controls.Add(Me.FndLocationCode)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(474, 288)
        Me.Panel1.TabIndex = 227
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(43, 215)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 231
        Me.lblLocation.Text = "Location"
        Me.lblLocation.Visible = False
        '
        'txtTodate
        '
        Me.txtTodate.CalculationExpression = Nothing
        Me.txtTodate.CustomFormat = "dd-MM-yyyy"
        Me.txtTodate.FieldCode = Nothing
        Me.txtTodate.FieldDesc = Nothing
        Me.txtTodate.FieldMaxLength = 0
        Me.txtTodate.FieldName = Nothing
        Me.txtTodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtTodate.isCalculatedField = False
        Me.txtTodate.IsSourceFromTable = False
        Me.txtTodate.IsSourceFromValueList = False
        Me.txtTodate.IsUnique = False
        Me.txtTodate.Location = New System.Drawing.Point(365, 12)
        Me.txtTodate.MendatroryField = False
        Me.txtTodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtTodate.MyLinkLable1 = Nothing
        Me.txtTodate.MyLinkLable2 = Nothing
        Me.txtTodate.Name = "txtTodate"
        Me.txtTodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtTodate.ReferenceFieldDesc = Nothing
        Me.txtTodate.ReferenceFieldName = Nothing
        Me.txtTodate.ReferenceTableName = Nothing
        Me.txtTodate.Size = New System.Drawing.Size(95, 20)
        Me.txtTodate.TabIndex = 229
        Me.txtTodate.TabStop = False
        Me.txtTodate.Text = "17-12-2011"
        Me.txtTodate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(239, 13)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(101, 18)
        Me.MyLabel1.TabIndex = 230
        Me.MyLabel1.Text = "Pay Period To Date"
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
        Me.txtfromDate.Location = New System.Drawing.Point(138, 11)
        Me.txtfromDate.MendatroryField = False
        Me.txtfromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtfromDate.MyLinkLable1 = Nothing
        Me.txtfromDate.MyLinkLable2 = Nothing
        Me.txtfromDate.Name = "txtfromDate"
        Me.txtfromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtfromDate.ReferenceFieldDesc = Nothing
        Me.txtfromDate.ReferenceFieldName = Nothing
        Me.txtfromDate.ReferenceTableName = Nothing
        Me.txtfromDate.Size = New System.Drawing.Size(95, 20)
        Me.txtfromDate.TabIndex = 227
        Me.txtfromDate.TabStop = False
        Me.txtfromDate.Text = "17-12-2011"
        Me.txtfromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.Location = New System.Drawing.Point(12, 12)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(113, 18)
        Me.lblfromDate.TabIndex = 228
        Me.lblfromDate.Text = "Pay Period from Date"
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblLocationName.Location = New System.Drawing.Point(115, 215)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(10, 19)
        Me.lblLocationName.TabIndex = 226
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.FndLocationCode.Location = New System.Drawing.Point(96, 215)
        Me.FndLocationCode.MendatroryField = True
        Me.FndLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndLocationCode.MyLinkLable1 = Nothing
        Me.FndLocationCode.MyLinkLable2 = Me.lblLocationName
        Me.FndLocationCode.MyReadOnly = False
        Me.FndLocationCode.MyShowMasterFormButton = False
        Me.FndLocationCode.Name = "FndLocationCode"
        Me.FndLocationCode.ReferenceFieldDesc = Nothing
        Me.FndLocationCode.ReferenceFieldName = Nothing
        Me.FndLocationCode.ReferenceTableName = Nothing
        Me.FndLocationCode.Size = New System.Drawing.Size(10, 18)
        Me.FndLocationCode.TabIndex = 225
        Me.FndLocationCode.Value = ""
        Me.FndLocationCode.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(393, 16)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 18)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnPrint.Location = New System.Drawing.Point(12, 21)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 18)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = "Print"
        '
        'txtMultDivision
        '
        Me.txtMultDivision.arrDispalyMember = Nothing
        Me.txtMultDivision.arrValueMember = Nothing
        Me.txtMultDivision.Location = New System.Drawing.Point(73, 59)
        Me.txtMultDivision.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultDivision.MyLinkLable1 = Nothing
        Me.txtMultDivision.MyLinkLable2 = Nothing
        Me.txtMultDivision.MyNullText = "All"
        Me.txtMultDivision.Name = "txtMultDivision"
        Me.txtMultDivision.Size = New System.Drawing.Size(335, 19)
        Me.txtMultDivision.TabIndex = 235
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(12, 59)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel2.TabIndex = 234
        Me.MyLabel2.Text = "Division"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(71, 36)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Nothing
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(335, 19)
        Me.txtLocation.TabIndex = 233
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(12, 36)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel3.TabIndex = 232
        Me.MyLabel3.Text = "Location "
        '
        'RptPayrollPerformaforcontribution
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(474, 343)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptPayrollPerformaforcontribution"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptPayrollPerformaforcontribution"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents FndLocationCode As common.UserControls.txtFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtTodate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtfromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtMultDivision As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
End Class

