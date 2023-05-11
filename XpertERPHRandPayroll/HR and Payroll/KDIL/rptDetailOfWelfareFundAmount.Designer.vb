Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptDetailOfWelfareFundAmount
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
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.fndLocationCode = New common.UserControls.txtFinder()
        Me.txtTodate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtfromDate = New common.Controls.MyDateTimePicker()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.txtDivisionMult = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtEmployeeMult = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.btnClosee = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClosee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndLocationCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTodate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtfromDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblfromDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDivisionMult)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEmployeeMult)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocation)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClosee)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(522, 292)
        Me.SplitContainer1.SplitterDistance = 256
        Me.SplitContainer1.TabIndex = 0
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.Location = New System.Drawing.Point(314, 37)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(191, 19)
        Me.lblLocationName.TabIndex = 351
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fndLocationCode
        '
        Me.fndLocationCode.Location = New System.Drawing.Point(101, 37)
        Me.fndLocationCode.MendatroryField = True
        Me.fndLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocationCode.MyLinkLable1 = Nothing
        Me.fndLocationCode.MyLinkLable2 = Me.lblLocationName
        Me.fndLocationCode.MyReadOnly = False
        Me.fndLocationCode.MyShowMasterFormButton = False
        Me.fndLocationCode.Name = "fndLocationCode"
        Me.fndLocationCode.Size = New System.Drawing.Size(208, 18)
        Me.fndLocationCode.TabIndex = 350
        Me.fndLocationCode.Value = ""
        '
        'txtTodate
        '
        Me.txtTodate.CustomFormat = "dd-MM-yyyy"
        Me.txtTodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtTodate.Location = New System.Drawing.Point(280, 13)
        Me.txtTodate.MendatroryField = False
        Me.txtTodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtTodate.MyLinkLable1 = Nothing
        Me.txtTodate.MyLinkLable2 = Nothing
        Me.txtTodate.Name = "txtTodate"
        Me.txtTodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtTodate.Size = New System.Drawing.Size(95, 20)
        Me.txtTodate.TabIndex = 348
        Me.txtTodate.TabStop = False
        Me.txtTodate.Text = "17-12-2011"
        Me.txtTodate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'MyLabel3
        '
        Me.MyLabel3.Location = New System.Drawing.Point(200, 14)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(78, 18)
        Me.MyLabel3.TabIndex = 349
        Me.MyLabel3.Text = "Pay Period To "
        '
        'txtfromDate
        '
        Me.txtfromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtfromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtfromDate.Location = New System.Drawing.Point(101, 12)
        Me.txtfromDate.MendatroryField = False
        Me.txtfromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtfromDate.MyLinkLable1 = Nothing
        Me.txtfromDate.MyLinkLable2 = Nothing
        Me.txtfromDate.Name = "txtfromDate"
        Me.txtfromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtfromDate.Size = New System.Drawing.Size(95, 20)
        Me.txtfromDate.TabIndex = 346
        Me.txtfromDate.TabStop = False
        Me.txtfromDate.Text = "17-12-2011"
        Me.txtfromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblfromDate
        '
        Me.lblfromDate.Location = New System.Drawing.Point(13, 13)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(89, 18)
        Me.lblfromDate.TabIndex = 347
        Me.lblfromDate.Text = "Pay Period from "
        '
        'txtDivisionMult
        '
        Me.txtDivisionMult.arrDispalyMember = Nothing
        Me.txtDivisionMult.arrValueMember = Nothing
        Me.txtDivisionMult.Location = New System.Drawing.Point(101, 58)
        Me.txtDivisionMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDivisionMult.MyLinkLable1 = Nothing
        Me.txtDivisionMult.MyLinkLable2 = Nothing
        Me.txtDivisionMult.MyNullText = "All"
        Me.txtDivisionMult.Name = "txtDivisionMult"
        Me.txtDivisionMult.Size = New System.Drawing.Size(404, 19)
        Me.txtDivisionMult.TabIndex = 345
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(12, 60)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(46, 18)
        Me.MyLabel2.TabIndex = 344
        Me.MyLabel2.Text = "Division"
        '
        'txtEmployeeMult
        '
        Me.txtEmployeeMult.arrDispalyMember = Nothing
        Me.txtEmployeeMult.arrValueMember = Nothing
        Me.txtEmployeeMult.Location = New System.Drawing.Point(101, 79)
        Me.txtEmployeeMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmployeeMult.MyLinkLable1 = Nothing
        Me.txtEmployeeMult.MyLinkLable2 = Nothing
        Me.txtEmployeeMult.MyNullText = "All"
        Me.txtEmployeeMult.Name = "txtEmployeeMult"
        Me.txtEmployeeMult.Size = New System.Drawing.Size(404, 19)
        Me.txtEmployeeMult.TabIndex = 343
        Me.txtEmployeeMult.Visible = False
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(12, 81)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel1.TabIndex = 342
        Me.MyLabel1.Text = "Employee"
        Me.MyLabel1.Visible = False
        '
        'lblLocation
        '
        Me.lblLocation.Location = New System.Drawing.Point(12, 37)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 340
        Me.lblLocation.Text = "Location"
        '
        'btnClosee
        '
        Me.btnClosee.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClosee.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClosee.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClosee.Location = New System.Drawing.Point(439, 3)
        Me.btnClosee.Name = "btnClosee"
        Me.btnClosee.Size = New System.Drawing.Size(73, 18)
        Me.btnClosee.TabIndex = 5
        Me.btnClosee.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnPrint.Location = New System.Drawing.Point(13, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(73, 18)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(657, -20)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'RptDetailOfWelfareFundAmount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(522, 292)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptDetailOfWelfareFundAmount"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptDetailOfWelfareFundAmount"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClosee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtDivisionMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtEmployeeMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents btnClosee As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtTodate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtfromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents fndLocationCode As common.UserControls.txtFinder
End Class

