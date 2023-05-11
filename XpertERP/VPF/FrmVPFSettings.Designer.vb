<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVPFSettings
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyNumBox1 = New common.MyNumBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LblOvalBlinkColor = New common.Controls.MyLabel()
        Me.LblOvalColor = New common.Controls.MyLabel()
        Me.BtnOvalBlinkColor = New Telerik.WinControls.UI.RadButton()
        Me.BtnOvalColor = New Telerik.WinControls.UI.RadButton()
        Me.ChkApplicableForAll = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.ChkOvalUnderOval = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.LblModuleName = New common.Controls.MyLabel()
        Me.TxtModuleCode = New common.UserControls.txtFinder()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.txtdigits = New common.MyNumBox()
        Me.LblVPFScreenCode = New common.Controls.MyLabel()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.LblOvlBorderClr = New common.Controls.MyLabel()
        Me.BtnOvlBorderClr = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.LblBlinkBorderClr = New common.Controls.MyLabel()
        Me.BtnBlinkBorderClr = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel9 = New common.Controls.MyLabel()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyNumBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.LblOvalBlinkColor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblOvalColor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnOvalBlinkColor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnOvalColor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkApplicableForAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkOvalUnderOval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblModuleName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdigits, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblVPFScreenCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblOvlBorderClr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnOvlBorderClr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblBlinkBorderClr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnBlinkBorderClr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(17, 40)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel5.TabIndex = 62
        Me.MyLabel5.Text = "Oval Size"
        '
        'MyNumBox1
        '
        Me.MyNumBox1.BackColor = System.Drawing.Color.White
        Me.MyNumBox1.DecimalPlaces = 2
        Me.MyNumBox1.Location = New System.Drawing.Point(118, 62)
        Me.MyNumBox1.MaxLength = 10
        Me.MyNumBox1.MendatroryField = False
        Me.MyNumBox1.MyLinkLable1 = Nothing
        Me.MyNumBox1.MyLinkLable2 = Nothing
        Me.MyNumBox1.Name = "MyNumBox1"
        Me.MyNumBox1.Size = New System.Drawing.Size(81, 20)
        Me.MyNumBox1.TabIndex = 64
        Me.MyNumBox1.Text = "0"
        Me.MyNumBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.MyNumBox1.Value = 0.0R
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ChkOvalUnderOval)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblModuleName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtModuleCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel22)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyNumBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtdigits)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.LblVPFScreenCode)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(821, 219)
        Me.SplitContainer1.SplitterDistance = 169
        Me.SplitContainer1.TabIndex = 65
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LblBlinkBorderClr)
        Me.GroupBox1.Controls.Add(Me.BtnBlinkBorderClr)
        Me.GroupBox1.Controls.Add(Me.MyLabel9)
        Me.GroupBox1.Controls.Add(Me.LblOvlBorderClr)
        Me.GroupBox1.Controls.Add(Me.BtnOvlBorderClr)
        Me.GroupBox1.Controls.Add(Me.MyLabel7)
        Me.GroupBox1.Controls.Add(Me.LblOvalBlinkColor)
        Me.GroupBox1.Controls.Add(Me.LblOvalColor)
        Me.GroupBox1.Controls.Add(Me.BtnOvalBlinkColor)
        Me.GroupBox1.Controls.Add(Me.BtnOvalColor)
        Me.GroupBox1.Controls.Add(Me.ChkApplicableForAll)
        Me.GroupBox1.Controls.Add(Me.MyLabel3)
        Me.GroupBox1.Controls.Add(Me.MyLabel4)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(9, 86)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(552, 68)
        Me.GroupBox1.TabIndex = 77
        Me.GroupBox1.TabStop = False
        '
        'LblOvalBlinkColor
        '
        Me.LblOvalBlinkColor.AutoSize = False
        Me.LblOvalBlinkColor.BorderVisible = True
        Me.LblOvalBlinkColor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblOvalBlinkColor.Location = New System.Drawing.Point(200, 43)
        Me.LblOvalBlinkColor.Name = "LblOvalBlinkColor"
        Me.LblOvalBlinkColor.Size = New System.Drawing.Size(24, 19)
        Me.LblOvalBlinkColor.TabIndex = 84
        Me.LblOvalBlinkColor.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblOvalBlinkColor.TextWrap = False
        '
        'LblOvalColor
        '
        Me.LblOvalColor.AutoSize = False
        Me.LblOvalColor.BorderVisible = True
        Me.LblOvalColor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblOvalColor.Location = New System.Drawing.Point(200, 18)
        Me.LblOvalColor.Name = "LblOvalColor"
        Me.LblOvalColor.Size = New System.Drawing.Size(24, 19)
        Me.LblOvalColor.TabIndex = 83
        Me.LblOvalColor.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblOvalColor.TextWrap = False
        '
        'BtnOvalBlinkColor
        '
        Me.BtnOvalBlinkColor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnOvalBlinkColor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOvalBlinkColor.Location = New System.Drawing.Point(113, 42)
        Me.BtnOvalBlinkColor.Name = "BtnOvalBlinkColor"
        Me.BtnOvalBlinkColor.Size = New System.Drawing.Size(81, 20)
        Me.BtnOvalBlinkColor.TabIndex = 82
        Me.BtnOvalBlinkColor.Text = "Select"
        '
        'BtnOvalColor
        '
        Me.BtnOvalColor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnOvalColor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOvalColor.Location = New System.Drawing.Point(113, 17)
        Me.BtnOvalColor.Name = "BtnOvalColor"
        Me.BtnOvalColor.Size = New System.Drawing.Size(81, 20)
        Me.BtnOvalColor.TabIndex = 78
        Me.BtnOvalColor.Text = "Select"
        '
        'ChkApplicableForAll
        '
        Me.ChkApplicableForAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkApplicableForAll.Location = New System.Drawing.Point(6, 0)
        Me.ChkApplicableForAll.Name = "ChkApplicableForAll"
        Me.ChkApplicableForAll.Size = New System.Drawing.Size(116, 16)
        Me.ChkApplicableForAll.TabIndex = 78
        Me.ChkApplicableForAll.Text = "Applicable For All"
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(17, 20)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel3.TabIndex = 70
        Me.MyLabel3.Text = "Oval Color"
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(17, 45)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel4.TabIndex = 72
        Me.MyLabel4.Text = "Oval Blink Color"
        '
        'ChkOvalUnderOval
        '
        Me.ChkOvalUnderOval.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkOvalUnderOval.Location = New System.Drawing.Point(249, 41)
        Me.ChkOvalUnderOval.Name = "ChkOvalUnderOval"
        Me.ChkOvalUnderOval.Size = New System.Drawing.Size(104, 16)
        Me.ChkOvalUnderOval.TabIndex = 74
        Me.ChkOvalUnderOval.Text = "Oval Under Oval"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(202, 64)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(14, 16)
        Me.MyLabel2.TabIndex = 69
        Me.MyLabel2.Text = "Y"
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(202, 40)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(14, 16)
        Me.MyLabel1.TabIndex = 68
        Me.MyLabel1.Text = "X"
        '
        'LblModuleName
        '
        Me.LblModuleName.AutoSize = False
        Me.LblModuleName.BorderVisible = True
        Me.LblModuleName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblModuleName.Location = New System.Drawing.Point(249, 13)
        Me.LblModuleName.Name = "LblModuleName"
        Me.LblModuleName.Size = New System.Drawing.Size(312, 19)
        Me.LblModuleName.TabIndex = 67
        Me.LblModuleName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblModuleName.TextWrap = False
        '
        'TxtModuleCode
        '
        Me.TxtModuleCode.Location = New System.Drawing.Point(118, 13)
        Me.TxtModuleCode.MendatroryField = True
        Me.TxtModuleCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtModuleCode.MyLinkLable1 = Nothing
        Me.TxtModuleCode.MyLinkLable2 = Nothing
        Me.TxtModuleCode.MyReadOnly = False
        Me.TxtModuleCode.MyShowMasterFormButton = False
        Me.TxtModuleCode.Name = "TxtModuleCode"
        Me.TxtModuleCode.Size = New System.Drawing.Size(125, 19)
        Me.TxtModuleCode.TabIndex = 65
        Me.TxtModuleCode.Value = ""
        '
        'MyLabel22
        '
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(17, 13)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel22.TabIndex = 66
        Me.MyLabel22.Text = "Module Name"
        '
        'txtdigits
        '
        Me.txtdigits.BackColor = System.Drawing.Color.White
        Me.txtdigits.DecimalPlaces = 2
        Me.txtdigits.Location = New System.Drawing.Point(118, 38)
        Me.txtdigits.MaxLength = 10
        Me.txtdigits.MendatroryField = False
        Me.txtdigits.MyLinkLable1 = Nothing
        Me.txtdigits.MyLinkLable2 = Nothing
        Me.txtdigits.Name = "txtdigits"
        Me.txtdigits.Size = New System.Drawing.Size(81, 20)
        Me.txtdigits.TabIndex = 63
        Me.txtdigits.Text = "0"
        Me.txtdigits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtdigits.Value = 0.0R
        '
        'LblVPFScreenCode
        '
        Me.LblVPFScreenCode.AutoSize = False
        Me.LblVPFScreenCode.BorderVisible = True
        Me.LblVPFScreenCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVPFScreenCode.Location = New System.Drawing.Point(95, 19)
        Me.LblVPFScreenCode.Name = "LblVPFScreenCode"
        Me.LblVPFScreenCode.Size = New System.Drawing.Size(104, 19)
        Me.LblVPFScreenCode.TabIndex = 85
        Me.LblVPFScreenCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblVPFScreenCode.TextWrap = False
        Me.LblVPFScreenCode.Visible = False
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(7, 19)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(82, 18)
        Me.btnsave.TabIndex = 2
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(735, 19)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(83, 18)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'LblOvlBorderClr
        '
        Me.LblOvlBorderClr.AutoSize = False
        Me.LblOvlBorderClr.BorderVisible = True
        Me.LblOvlBorderClr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblOvlBorderClr.Location = New System.Drawing.Point(522, 16)
        Me.LblOvlBorderClr.Name = "LblOvlBorderClr"
        Me.LblOvlBorderClr.Size = New System.Drawing.Size(24, 19)
        Me.LblOvlBorderClr.TabIndex = 87
        Me.LblOvlBorderClr.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblOvlBorderClr.TextWrap = False
        '
        'BtnOvlBorderClr
        '
        Me.BtnOvlBorderClr.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnOvlBorderClr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOvlBorderClr.Location = New System.Drawing.Point(435, 15)
        Me.BtnOvlBorderClr.Name = "BtnOvlBorderClr"
        Me.BtnOvlBorderClr.Size = New System.Drawing.Size(81, 20)
        Me.BtnOvlBorderClr.TabIndex = 86
        Me.BtnOvlBorderClr.Text = "Select"
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(358, 17)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel7.TabIndex = 85
        Me.MyLabel7.Text = "Border Color"
        '
        'LblBlinkBorderClr
        '
        Me.LblBlinkBorderClr.AutoSize = False
        Me.LblBlinkBorderClr.BorderVisible = True
        Me.LblBlinkBorderClr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBlinkBorderClr.Location = New System.Drawing.Point(522, 40)
        Me.LblBlinkBorderClr.Name = "LblBlinkBorderClr"
        Me.LblBlinkBorderClr.Size = New System.Drawing.Size(24, 19)
        Me.LblBlinkBorderClr.TabIndex = 90
        Me.LblBlinkBorderClr.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblBlinkBorderClr.TextWrap = False
        '
        'BtnBlinkBorderClr
        '
        Me.BtnBlinkBorderClr.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnBlinkBorderClr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnBlinkBorderClr.Location = New System.Drawing.Point(435, 39)
        Me.BtnBlinkBorderClr.Name = "BtnBlinkBorderClr"
        Me.BtnBlinkBorderClr.Size = New System.Drawing.Size(81, 20)
        Me.BtnBlinkBorderClr.TabIndex = 89
        Me.BtnBlinkBorderClr.Text = "Select"
        '
        'MyLabel9
        '
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(358, 41)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel9.TabIndex = 88
        Me.MyLabel9.Text = "Border Color"
        '
        'FrmVPFSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(821, 219)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmVPFSettings"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "VPF Settings"
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyNumBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.LblOvalBlinkColor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblOvalColor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnOvalBlinkColor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnOvalColor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkApplicableForAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkOvalUnderOval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblModuleName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdigits, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblVPFScreenCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblOvlBorderClr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnOvlBorderClr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblBlinkBorderClr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnBlinkBorderClr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyNumBox1 As common.MyNumBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents LblModuleName As common.Controls.MyLabel
    Friend WithEvents TxtModuleCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents txtdigits As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents ChkOvalUnderOval As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ChkApplicableForAll As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents BtnOvalBlinkColor As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnOvalColor As Telerik.WinControls.UI.RadButton
    Friend WithEvents LblOvalBlinkColor As common.Controls.MyLabel
    Friend WithEvents LblOvalColor As common.Controls.MyLabel
    Friend WithEvents LblVPFScreenCode As common.Controls.MyLabel
    Friend WithEvents LblBlinkBorderClr As common.Controls.MyLabel
    Friend WithEvents BtnBlinkBorderClr As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents LblOvlBorderClr As common.Controls.MyLabel
    Friend WithEvents BtnOvlBorderClr As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
End Class
