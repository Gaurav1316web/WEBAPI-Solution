<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucMilkAnalyzer
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.CboMachine = New common.Controls.MyComboBox
        Me.MyLabel15 = New common.Controls.MyLabel
        Me.BtnStart = New Telerik.WinControls.UI.RadButton
        Me.cboComPort = New common.Controls.MyComboBox
        Me.lblComPort = New common.Controls.MyLabel
        Me.LblSnf = New common.Controls.MyLabel
        Me.LblFAT = New common.Controls.MyLabel
        CType(Me.CboMachine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboComPort, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblComPort, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblSnf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CboMachine
        '
        Me.CboMachine.AllowShowFocusCues = False
        Me.CboMachine.AutoCompleteDisplayMember = Nothing
        Me.CboMachine.AutoCompleteValueMember = Nothing
        Me.CboMachine.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboMachine.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboMachine.ForeColor = System.Drawing.Color.Lime
        RadListDataItem1.Text = "COM1"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "COM2"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "COM3"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "COM4"
        RadListDataItem4.TextWrap = True
        Me.CboMachine.Items.Add(RadListDataItem1)
        Me.CboMachine.Items.Add(RadListDataItem2)
        Me.CboMachine.Items.Add(RadListDataItem3)
        Me.CboMachine.Items.Add(RadListDataItem4)
        Me.CboMachine.Location = New System.Drawing.Point(117, 120)
        Me.CboMachine.MendatroryField = True
        Me.CboMachine.MyLinkLable1 = Me.MyLabel15
        Me.CboMachine.MyLinkLable2 = Nothing
        Me.CboMachine.Name = "CboMachine"
        '
        '
        '
        Me.CboMachine.RootElement.StretchVertically = True
        Me.CboMachine.Size = New System.Drawing.Size(142, 18)
        Me.CboMachine.TabIndex = 70
        '
        'MyLabel15
        '
        Me.MyLabel15.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.ForeColor = System.Drawing.Color.Yellow
        Me.MyLabel15.Location = New System.Drawing.Point(46, 118)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(64, 20)
        Me.MyLabel15.TabIndex = 71
        Me.MyLabel15.Text = "Machine"
        '
        'BtnStart
        '
        Me.BtnStart.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.BtnStart.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnStart.Location = New System.Drawing.Point(264, 120)
        Me.BtnStart.Name = "BtnStart"
        Me.BtnStart.Size = New System.Drawing.Size(102, 37)
        Me.BtnStart.TabIndex = 69
        Me.BtnStart.Text = "START"
        '
        'cboComPort
        '
        Me.cboComPort.AllowShowFocusCues = False
        Me.cboComPort.AutoCompleteDisplayMember = Nothing
        Me.cboComPort.AutoCompleteValueMember = Nothing
        Me.cboComPort.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboComPort.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboComPort.ForeColor = System.Drawing.Color.Lime
        RadListDataItem5.Text = "COM1"
        RadListDataItem5.TextWrap = True
        RadListDataItem6.Text = "COM2"
        RadListDataItem6.TextWrap = True
        RadListDataItem7.Text = "COM3"
        RadListDataItem7.TextWrap = True
        RadListDataItem8.Text = "COM4"
        RadListDataItem8.TextWrap = True
        Me.cboComPort.Items.Add(RadListDataItem5)
        Me.cboComPort.Items.Add(RadListDataItem6)
        Me.cboComPort.Items.Add(RadListDataItem7)
        Me.cboComPort.Items.Add(RadListDataItem8)
        Me.cboComPort.Location = New System.Drawing.Point(117, 140)
        Me.cboComPort.MendatroryField = True
        Me.cboComPort.MyLinkLable1 = Me.lblComPort
        Me.cboComPort.MyLinkLable2 = Nothing
        Me.cboComPort.Name = "cboComPort"
        '
        '
        '
        Me.cboComPort.RootElement.StretchVertically = True
        Me.cboComPort.Size = New System.Drawing.Size(143, 18)
        Me.cboComPort.TabIndex = 65
        '
        'lblComPort
        '
        Me.lblComPort.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComPort.ForeColor = System.Drawing.Color.Yellow
        Me.lblComPort.Location = New System.Drawing.Point(44, 139)
        Me.lblComPort.Name = "lblComPort"
        Me.lblComPort.Size = New System.Drawing.Size(71, 20)
        Me.lblComPort.TabIndex = 66
        Me.lblComPort.Text = "Com Port"
        '
        'LblSnf
        '
        Me.LblSnf.BackColor = System.Drawing.Color.Transparent
        Me.LblSnf.BorderVisible = True
        Me.LblSnf.Font = New System.Drawing.Font("Agency FB", 65.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSnf.ForeColor = System.Drawing.Color.Red
        Me.LblSnf.Location = New System.Drawing.Point(2, 3)
        Me.LblSnf.Name = "LblSnf"
        Me.LblSnf.Size = New System.Drawing.Size(215, 114)
        Me.LblSnf.TabIndex = 68
        Me.LblSnf.Text = "00.00"
        '
        'LblFAT
        '
        Me.LblFAT.BackColor = System.Drawing.Color.Transparent
        Me.LblFAT.BorderVisible = True
        Me.LblFAT.Font = New System.Drawing.Font("Agency FB", 65.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFAT.ForeColor = System.Drawing.Color.Red
        Me.LblFAT.Location = New System.Drawing.Point(216, 3)
        Me.LblFAT.Name = "LblFAT"
        Me.LblFAT.Size = New System.Drawing.Size(215, 114)
        Me.LblFAT.TabIndex = 67
        Me.LblFAT.Text = "00.00"
        '
        'ucMilkAnalyzer
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.AutoScroll = True
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Controls.Add(Me.CboMachine)
        Me.Controls.Add(Me.LblSnf)
        Me.Controls.Add(Me.BtnStart)
        Me.Controls.Add(Me.MyLabel15)
        Me.Controls.Add(Me.LblFAT)
        Me.Controls.Add(Me.lblComPort)
        Me.Controls.Add(Me.cboComPort)
        Me.Name = "ucMilkAnalyzer"
        Me.Size = New System.Drawing.Size(432, 161)
        CType(Me.CboMachine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboComPort, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblComPort, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblSnf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblFAT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CboMachine As common.Controls.MyComboBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents BtnStart As Telerik.WinControls.UI.RadButton
    Friend WithEvents cboComPort As common.Controls.MyComboBox
    Friend WithEvents lblComPort As common.Controls.MyLabel
    Friend WithEvents LblSnf As common.Controls.MyLabel
    Friend WithEvents LblFAT As common.Controls.MyLabel

End Class
