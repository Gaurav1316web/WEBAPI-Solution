Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptPFForm5
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
        Me.FndLocationCode = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtLocCode = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndFromPeriod = New common.UserControls.txtFinder()
        Me.lblfromPeriod = New common.Controls.MyLabel()
        Me.lblFromPeriodName = New common.Controls.MyLabel()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.txtDivisionMult = New common.UserControls.txtMultiSelectFinder()
        Me.lblDevision = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromPeriodName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDevision, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Size = New System.Drawing.Size(1143, 430)
        Me.SplitContainer1.SplitterDistance = 393
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
        Me.RadPageView1.Size = New System.Drawing.Size(1143, 393)
        Me.RadPageView1.TabIndex = 5
        Me.RadPageView1.TabStop = False
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.FndLocationCode)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocationName)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1122, 345)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'FndLocationCode
        '
        Me.FndLocationCode.Location = New System.Drawing.Point(93, 143)
        Me.FndLocationCode.MendatroryField = False
        Me.FndLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndLocationCode.MyLinkLable1 = Me.lblLocation
        Me.FndLocationCode.MyLinkLable2 = Me.lblLocationName
        Me.FndLocationCode.MyReadOnly = False
        Me.FndLocationCode.MyShowMasterFormButton = False
        Me.FndLocationCode.Name = "FndLocationCode"
        Me.FndLocationCode.Size = New System.Drawing.Size(180, 18)
        Me.FndLocationCode.TabIndex = 59
        Me.FndLocationCode.Value = ""
        Me.FndLocationCode.Visible = False
        '
        'lblLocation
        '
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblLocation.Location = New System.Drawing.Point(17, 145)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 60
        Me.lblLocation.Text = "Location"
        Me.lblLocation.Visible = False
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblLocationName.Location = New System.Drawing.Point(279, 143)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(243, 18)
        Me.lblLocationName.TabIndex = 61
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocationName.TextWrap = False
        Me.lblLocationName.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtDivisionMult)
        Me.GroupBox1.Controls.Add(Me.lblDevision)
        Me.GroupBox1.Controls.Add(Me.txtLocCode)
        Me.GroupBox1.Controls.Add(Me.MyLabel2)
        Me.GroupBox1.Controls.Add(Me.fndFromPeriod)
        Me.GroupBox1.Controls.Add(Me.lblfromPeriod)
        Me.GroupBox1.Controls.Add(Me.lblFromPeriodName)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(526, 104)
        Me.GroupBox1.TabIndex = 60
        Me.GroupBox1.TabStop = False
        '
        'txtLocCode
        '
        Me.txtLocCode.arrDispalyMember = Nothing
        Me.txtLocCode.arrValueMember = Nothing
        Me.txtLocCode.Location = New System.Drawing.Point(90, 42)
        Me.txtLocCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocCode.MyLinkLable1 = Nothing
        Me.txtLocCode.MyLinkLable2 = Nothing
        Me.txtLocCode.MyNullText = "All"
        Me.txtLocCode.Name = "txtLocCode"
        Me.txtLocCode.Size = New System.Drawing.Size(429, 19)
        Me.txtLocCode.TabIndex = 345
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(17, 44)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel2.TabIndex = 344
        Me.MyLabel2.Text = "Location"
        '
        'fndFromPeriod
        '
        Me.fndFromPeriod.Location = New System.Drawing.Point(90, 21)
        Me.fndFromPeriod.MendatroryField = True
        Me.fndFromPeriod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndFromPeriod.MyLinkLable1 = Me.lblfromPeriod
        Me.fndFromPeriod.MyLinkLable2 = Me.lblFromPeriodName
        Me.fndFromPeriod.MyReadOnly = False
        Me.fndFromPeriod.MyShowMasterFormButton = False
        Me.fndFromPeriod.Name = "fndFromPeriod"
        Me.fndFromPeriod.Size = New System.Drawing.Size(180, 18)
        Me.fndFromPeriod.TabIndex = 56
        Me.fndFromPeriod.Value = ""
        '
        'lblfromPeriod
        '
        Me.lblfromPeriod.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblfromPeriod.Location = New System.Drawing.Point(17, 23)
        Me.lblfromPeriod.Name = "lblfromPeriod"
        Me.lblfromPeriod.Size = New System.Drawing.Size(62, 16)
        Me.lblfromPeriod.TabIndex = 57
        Me.lblfromPeriod.Text = "Pay Period"
        '
        'lblFromPeriodName
        '
        Me.lblFromPeriodName.AutoSize = False
        Me.lblFromPeriodName.BorderVisible = True
        Me.lblFromPeriodName.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblFromPeriodName.Location = New System.Drawing.Point(276, 21)
        Me.lblFromPeriodName.Name = "lblFromPeriodName"
        Me.lblFromPeriodName.Size = New System.Drawing.Size(243, 18)
        Me.lblFromPeriodName.TabIndex = 58
        Me.lblFromPeriodName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFromPeriodName.TextWrap = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1049, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(84, 22)
        Me.btnclose.TabIndex = 17
        Me.btnclose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(9, 5)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 16
        Me.btnGo.Text = "Print"
        '
        'txtDivisionMult
        '
        Me.txtDivisionMult.arrDispalyMember = Nothing
        Me.txtDivisionMult.arrValueMember = Nothing
        Me.txtDivisionMult.Location = New System.Drawing.Point(90, 67)
        Me.txtDivisionMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDivisionMult.MyLinkLable1 = Nothing
        Me.txtDivisionMult.MyLinkLable2 = Nothing
        Me.txtDivisionMult.MyNullText = "All"
        Me.txtDivisionMult.Name = "txtDivisionMult"
        Me.txtDivisionMult.Size = New System.Drawing.Size(429, 19)
        Me.txtDivisionMult.TabIndex = 347
        '
        'lblDevision
        '
        Me.lblDevision.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDevision.Location = New System.Drawing.Point(17, 68)
        Me.lblDevision.Name = "lblDevision"
        Me.lblDevision.Size = New System.Drawing.Size(46, 16)
        Me.lblDevision.TabIndex = 346
        Me.lblDevision.Text = "Division"
        '
        'RptPFForm5
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1143, 430)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptPFForm5"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptPFForm5"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromPeriodName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDevision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents fndFromPeriod As common.UserControls.txtFinder
    Friend WithEvents lblfromPeriod As common.Controls.MyLabel
    Friend WithEvents lblFromPeriodName As common.Controls.MyLabel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents FndLocationCode As common.UserControls.txtFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents txtLocCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtDivisionMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblDevision As common.Controls.MyLabel
End Class

