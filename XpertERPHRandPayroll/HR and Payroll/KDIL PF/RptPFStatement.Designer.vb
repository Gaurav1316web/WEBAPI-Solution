Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptPFStatement
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
        Me.chkpfsno = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkTransPF = New Telerik.WinControls.UI.RadCheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.TxtPFSNO = New common.UserControls.txtFinder()
        Me.lblfromPeriod = New common.Controls.MyLabel()
        Me.lblFromPeriodName = New common.Controls.MyLabel()
        Me.txtDivisionMult = New common.UserControls.txtMultiSelectFinder()
        Me.lblDivision = New common.Controls.MyLabel()
        Me.txtLocationMult = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.fndFromPeriod = New common.UserControls.txtFinder()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.chkpfsno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTransPF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromPeriodName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(1100, 417)
        Me.SplitContainer1.SplitterDistance = 379
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
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1100, 379)
        Me.RadPageView1.TabIndex = 5
        Me.RadPageView1.TabStop = False
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.chkpfsno)
        Me.RadPageViewPage1.Controls.Add(Me.chkTransPF)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1079, 331)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'chkpfsno
        '
        Me.chkpfsno.Location = New System.Drawing.Point(524, 32)
        Me.chkpfsno.Name = "chkpfsno"
        Me.chkpfsno.Size = New System.Drawing.Size(89, 18)
        Me.chkpfsno.TabIndex = 175
        Me.chkpfsno.Text = "PF Serial Wise"
        '
        'chkTransPF
        '
        Me.chkTransPF.Location = New System.Drawing.Point(524, 8)
        Me.chkTransPF.Name = "chkTransPF"
        Me.chkTransPF.Size = New System.Drawing.Size(75, 18)
        Me.chkTransPF.TabIndex = 174
        Me.chkTransPF.Text = "Transfer PF"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MyLabel1)
        Me.GroupBox1.Controls.Add(Me.TxtPFSNO)
        Me.GroupBox1.Controls.Add(Me.txtDivisionMult)
        Me.GroupBox1.Controls.Add(Me.lblDivision)
        Me.GroupBox1.Controls.Add(Me.txtLocationMult)
        Me.GroupBox1.Controls.Add(Me.lblLocation)
        Me.GroupBox1.Controls.Add(Me.fndFromPeriod)
        Me.GroupBox1.Controls.Add(Me.lblfromPeriod)
        Me.GroupBox1.Controls.Add(Me.lblFromPeriodName)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(508, 113)
        Me.GroupBox1.TabIndex = 60
        Me.GroupBox1.TabStop = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.MyLabel1.Location = New System.Drawing.Point(14, 89)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel1.TabIndex = 347
        Me.MyLabel1.Text = "PF Serial "
        '
        'TxtPFSNO
        '
        Me.TxtPFSNO.CalculationExpression = Nothing
        Me.TxtPFSNO.FieldCode = Nothing
        Me.TxtPFSNO.FieldDesc = Nothing
        Me.TxtPFSNO.FieldMaxLength = 0
        Me.TxtPFSNO.FieldName = Nothing
        Me.TxtPFSNO.isCalculatedField = False
        Me.TxtPFSNO.IsSourceFromTable = False
        Me.TxtPFSNO.IsSourceFromValueList = False
        Me.TxtPFSNO.IsUnique = False
        Me.TxtPFSNO.Location = New System.Drawing.Point(90, 89)
        Me.TxtPFSNO.MendatroryField = True
        Me.TxtPFSNO.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPFSNO.MyLinkLable1 = Me.lblfromPeriod
        Me.TxtPFSNO.MyLinkLable2 = Me.lblFromPeriodName
        Me.TxtPFSNO.MyReadOnly = False
        Me.TxtPFSNO.MyShowMasterFormButton = False
        Me.TxtPFSNO.Name = "TxtPFSNO"
        Me.TxtPFSNO.ReferenceFieldDesc = Nothing
        Me.TxtPFSNO.ReferenceFieldName = Nothing
        Me.TxtPFSNO.ReferenceTableName = Nothing
        Me.TxtPFSNO.Size = New System.Drawing.Size(180, 18)
        Me.TxtPFSNO.TabIndex = 176
        Me.TxtPFSNO.Value = ""
        '
        'lblfromPeriod
        '
        Me.lblfromPeriod.FieldName = Nothing
        Me.lblfromPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblfromPeriod.Location = New System.Drawing.Point(14, 23)
        Me.lblfromPeriod.Name = "lblfromPeriod"
        Me.lblfromPeriod.Size = New System.Drawing.Size(65, 16)
        Me.lblfromPeriod.TabIndex = 57
        Me.lblfromPeriod.Text = "Pay Period"
        '
        'lblFromPeriodName
        '
        Me.lblFromPeriodName.AutoSize = False
        Me.lblFromPeriodName.BorderVisible = True
        Me.lblFromPeriodName.FieldName = Nothing
        Me.lblFromPeriodName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblFromPeriodName.Location = New System.Drawing.Point(276, 21)
        Me.lblFromPeriodName.Name = "lblFromPeriodName"
        Me.lblFromPeriodName.Size = New System.Drawing.Size(218, 18)
        Me.lblFromPeriodName.TabIndex = 58
        Me.lblFromPeriodName.TextWrap = False
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
        Me.txtDivisionMult.Size = New System.Drawing.Size(404, 19)
        Me.txtDivisionMult.TabIndex = 346
        '
        'lblDivision
        '
        Me.lblDivision.FieldName = Nothing
        Me.lblDivision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDivision.Location = New System.Drawing.Point(14, 67)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.Size = New System.Drawing.Size(50, 16)
        Me.lblDivision.TabIndex = 63
        Me.lblDivision.Text = "Division"
        '
        'txtLocationMult
        '
        Me.txtLocationMult.arrDispalyMember = Nothing
        Me.txtLocationMult.arrValueMember = Nothing
        Me.txtLocationMult.Location = New System.Drawing.Point(90, 45)
        Me.txtLocationMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationMult.MyLinkLable1 = Nothing
        Me.txtLocationMult.MyLinkLable2 = Nothing
        Me.txtLocationMult.MyNullText = "All"
        Me.txtLocationMult.Name = "txtLocationMult"
        Me.txtLocationMult.Size = New System.Drawing.Size(404, 19)
        Me.txtLocationMult.TabIndex = 345
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblLocation.Location = New System.Drawing.Point(14, 45)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(57, 16)
        Me.lblLocation.TabIndex = 60
        Me.lblLocation.Text = "Location "
        '
        'fndFromPeriod
        '
        Me.fndFromPeriod.CalculationExpression = Nothing
        Me.fndFromPeriod.FieldCode = Nothing
        Me.fndFromPeriod.FieldDesc = Nothing
        Me.fndFromPeriod.FieldMaxLength = 0
        Me.fndFromPeriod.FieldName = Nothing
        Me.fndFromPeriod.isCalculatedField = False
        Me.fndFromPeriod.IsSourceFromTable = False
        Me.fndFromPeriod.IsSourceFromValueList = False
        Me.fndFromPeriod.IsUnique = False
        Me.fndFromPeriod.Location = New System.Drawing.Point(90, 21)
        Me.fndFromPeriod.MendatroryField = True
        Me.fndFromPeriod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndFromPeriod.MyLinkLable1 = Me.lblfromPeriod
        Me.fndFromPeriod.MyLinkLable2 = Me.lblFromPeriodName
        Me.fndFromPeriod.MyReadOnly = False
        Me.fndFromPeriod.MyShowMasterFormButton = False
        Me.fndFromPeriod.Name = "fndFromPeriod"
        Me.fndFromPeriod.ReferenceFieldDesc = Nothing
        Me.fndFromPeriod.ReferenceFieldName = Nothing
        Me.fndFromPeriod.ReferenceTableName = Nothing
        Me.fndFromPeriod.Size = New System.Drawing.Size(180, 18)
        Me.fndFromPeriod.TabIndex = 56
        Me.fndFromPeriod.Value = ""
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnreset.Location = New System.Drawing.Point(85, 6)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(71, 22)
        Me.btnreset.TabIndex = 18
        Me.btnreset.Text = "Reset"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1005, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(84, 22)
        Me.btnclose.TabIndex = 17
        Me.btnclose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(11, 6)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 16
        Me.btnGo.Text = ">>>"
        '
        'RptPFStatement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1100, 417)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptPFStatement"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptPFStatement"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.chkpfsno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTransPF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromPeriodName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtDivisionMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblDivision As common.Controls.MyLabel
    Friend WithEvents txtLocationMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents fndFromPeriod As common.UserControls.txtFinder
    Friend WithEvents lblfromPeriod As common.Controls.MyLabel
    Friend WithEvents lblFromPeriodName As common.Controls.MyLabel
    Friend WithEvents chkTransPF As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkpfsno As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents TxtPFSNO As common.UserControls.txtFinder
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
End Class

