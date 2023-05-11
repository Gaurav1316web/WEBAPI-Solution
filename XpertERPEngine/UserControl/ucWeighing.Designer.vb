<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucWeighing
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucWeighing))
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem9 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem10 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem11 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem12 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadButton3 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.lblWeight = New common.Controls.MyLabel()
        Me.txtManualCheck = New common.MyNumBox()
        Me.CboMachine = New common.Controls.MyComboBox()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.BtnStart = New Telerik.WinControls.UI.RadButton()
        Me.cboComPort = New common.Controls.MyComboBox()
        Me.lblComPort = New common.Controls.MyLabel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtManualCheck, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboMachine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboComPort, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblComPort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.RadGroupBox3.Controls.Add(Me.RadButton3)
        Me.RadGroupBox3.Controls.Add(Me.RadButton2)
        Me.RadGroupBox3.Controls.Add(Me.RadButton1)
        Me.RadGroupBox3.Controls.Add(Me.lblWeight)
        Me.RadGroupBox3.Controls.Add(Me.txtManualCheck)
        Me.RadGroupBox3.Controls.Add(Me.CboMachine)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel15)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox3.Controls.Add(Me.BtnStart)
        Me.RadGroupBox3.Controls.Add(Me.cboComPort)
        Me.RadGroupBox3.Controls.Add(Me.lblComPort)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadGroupBox3.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(356, 64)
        Me.RadGroupBox3.TabIndex = 4
        '
        'RadButton3
        '
        Me.RadButton3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.RadButton3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton3.Image = CType(resources.GetObject("RadButton3.Image"), System.Drawing.Image)
        Me.RadButton3.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton3.Location = New System.Drawing.Point(220, 43)
        Me.RadButton3.Name = "RadButton3"
        Me.RadButton3.Size = New System.Drawing.Size(22, 18)
        Me.RadButton3.TabIndex = 70
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.RadButton2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton2.Location = New System.Drawing.Point(105, 40)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(22, 20)
        Me.RadButton2.TabIndex = 69
        Me.RadButton2.Text = "T"
        Me.RadButton2.Visible = False
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(196, 41)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(22, 20)
        Me.RadButton1.TabIndex = 68
        Me.RadButton1.Text = "A"
        Me.RadButton1.Visible = False
        '
        'lblWeight
        '
        Me.lblWeight.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblWeight.FieldName = Nothing
        Me.lblWeight.Font = New System.Drawing.Font("Arial", 20.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
        Me.lblWeight.ForeColor = System.Drawing.Color.Red
        Me.lblWeight.Location = New System.Drawing.Point(53, 3)
        Me.lblWeight.Name = "lblWeight"
        Me.lblWeight.Size = New System.Drawing.Size(80, 35)
        Me.lblWeight.TabIndex = 57
        Me.lblWeight.Text = "00.00"
        Me.lblWeight.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtManualCheck
        '
        Me.txtManualCheck.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtManualCheck.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtManualCheck.CalculationExpression = Nothing
        Me.txtManualCheck.DecimalPlaces = 2
        Me.txtManualCheck.FieldCode = Nothing
        Me.txtManualCheck.FieldDesc = Nothing
        Me.txtManualCheck.FieldMaxLength = 0
        Me.txtManualCheck.FieldName = Nothing
        Me.txtManualCheck.isCalculatedField = False
        Me.txtManualCheck.IsSourceFromTable = False
        Me.txtManualCheck.IsSourceFromValueList = False
        Me.txtManualCheck.IsUnique = False
        Me.txtManualCheck.Location = New System.Drawing.Point(128, 41)
        Me.txtManualCheck.MendatroryField = True
        Me.txtManualCheck.MyLinkLable1 = Nothing
        Me.txtManualCheck.MyLinkLable2 = Nothing
        Me.txtManualCheck.Name = "txtManualCheck"
        Me.txtManualCheck.ReferenceFieldDesc = Nothing
        Me.txtManualCheck.ReferenceFieldName = Nothing
        Me.txtManualCheck.ReferenceTableName = Nothing
        Me.txtManualCheck.Size = New System.Drawing.Size(67, 20)
        Me.txtManualCheck.TabIndex = 67
        Me.txtManualCheck.Text = "0"
        Me.txtManualCheck.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtManualCheck.Value = 0.0R
        Me.txtManualCheck.Visible = False
        '
        'CboMachine
        '
        Me.CboMachine.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.CboMachine.AutoCompleteDisplayMember = Nothing
        Me.CboMachine.AutoCompleteValueMember = Nothing
        Me.CboMachine.CalculationExpression = Nothing
        Me.CboMachine.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboMachine.FieldCode = Nothing
        Me.CboMachine.FieldDesc = Nothing
        Me.CboMachine.FieldMaxLength = 0
        Me.CboMachine.FieldName = Nothing
        Me.CboMachine.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboMachine.ForeColor = System.Drawing.Color.Lime
        Me.CboMachine.isCalculatedField = False
        Me.CboMachine.IsSourceFromTable = False
        Me.CboMachine.IsSourceFromValueList = False
        Me.CboMachine.IsUnique = False
        RadListDataItem5.Text = "COM1"
        RadListDataItem6.Text = "COM2"
        RadListDataItem7.Text = "COM3"
        RadListDataItem8.Text = "COM4"
        Me.CboMachine.Items.Add(RadListDataItem5)
        Me.CboMachine.Items.Add(RadListDataItem6)
        Me.CboMachine.Items.Add(RadListDataItem7)
        Me.CboMachine.Items.Add(RadListDataItem8)
        Me.CboMachine.Location = New System.Drawing.Point(243, 3)
        Me.CboMachine.MendatroryField = True
        Me.CboMachine.MyLinkLable1 = Me.MyLabel15
        Me.CboMachine.MyLinkLable2 = Nothing
        Me.CboMachine.Name = "CboMachine"
        Me.CboMachine.ReferenceFieldDesc = Nothing
        Me.CboMachine.ReferenceFieldName = Nothing
        Me.CboMachine.ReferenceTableName = Nothing
        '
        '
        '
        Me.CboMachine.RootElement.StretchVertically = True
        Me.CboMachine.Size = New System.Drawing.Size(109, 18)
        Me.CboMachine.TabIndex = 65
        '
        'MyLabel15
        '
        Me.MyLabel15.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.ForeColor = System.Drawing.Color.Lime
        Me.MyLabel15.Location = New System.Drawing.Point(191, 4)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel15.TabIndex = 66
        Me.MyLabel15.Text = "Machine"
        '
        'MyLabel5
        '
        Me.MyLabel5.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.ForeColor = System.Drawing.Color.Lime
        Me.MyLabel5.Location = New System.Drawing.Point(5, 42)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel5.TabIndex = 63
        Me.MyLabel5.Text = "Press F2 to Read"
        '
        'MyLabel8
        '
        Me.MyLabel8.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.ForeColor = System.Drawing.Color.Lime
        Me.MyLabel8.Location = New System.Drawing.Point(5, 11)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(53, 19)
        Me.MyLabel8.TabIndex = 58
        Me.MyLabel8.Text = "Weight"
        '
        'BtnStart
        '
        Me.BtnStart.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.BtnStart.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnStart.Location = New System.Drawing.Point(243, 43)
        Me.BtnStart.Name = "BtnStart"
        Me.BtnStart.Size = New System.Drawing.Size(109, 18)
        Me.BtnStart.TabIndex = 62
        Me.BtnStart.Text = "Start"
        '
        'cboComPort
        '
        Me.cboComPort.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cboComPort.AutoCompleteDisplayMember = Nothing
        Me.cboComPort.AutoCompleteValueMember = Nothing
        Me.cboComPort.CalculationExpression = Nothing
        Me.cboComPort.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboComPort.FieldCode = Nothing
        Me.cboComPort.FieldDesc = Nothing
        Me.cboComPort.FieldMaxLength = 0
        Me.cboComPort.FieldName = Nothing
        Me.cboComPort.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboComPort.isCalculatedField = False
        Me.cboComPort.IsSourceFromTable = False
        Me.cboComPort.IsSourceFromValueList = False
        Me.cboComPort.IsUnique = False
        RadListDataItem9.Text = "COM1"
        RadListDataItem10.Text = "COM2"
        RadListDataItem11.Text = "COM3"
        RadListDataItem12.Text = "COM4"
        Me.cboComPort.Items.Add(RadListDataItem9)
        Me.cboComPort.Items.Add(RadListDataItem10)
        Me.cboComPort.Items.Add(RadListDataItem11)
        Me.cboComPort.Items.Add(RadListDataItem12)
        Me.cboComPort.Location = New System.Drawing.Point(243, 23)
        Me.cboComPort.MendatroryField = True
        Me.cboComPort.MyLinkLable1 = Me.lblComPort
        Me.cboComPort.MyLinkLable2 = Nothing
        Me.cboComPort.Name = "cboComPort"
        Me.cboComPort.ReferenceFieldDesc = Nothing
        Me.cboComPort.ReferenceFieldName = Nothing
        Me.cboComPort.ReferenceTableName = Nothing
        Me.cboComPort.Size = New System.Drawing.Size(109, 18)
        Me.cboComPort.TabIndex = 42
        '
        'lblComPort
        '
        Me.lblComPort.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblComPort.FieldName = Nothing
        Me.lblComPort.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComPort.ForeColor = System.Drawing.Color.Lime
        Me.lblComPort.Location = New System.Drawing.Point(191, 24)
        Me.lblComPort.Name = "lblComPort"
        Me.lblComPort.Size = New System.Drawing.Size(27, 16)
        Me.lblComPort.TabIndex = 43
        Me.lblComPort.Text = "Port"
        '
        'Timer1
        '
        '
        'Timer2
        '
        Me.Timer2.Interval = 1000
        '
        'ucWeighing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Name = "ucWeighing"
        Me.Size = New System.Drawing.Size(356, 64)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtManualCheck, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboMachine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboComPort, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblComPort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents CboMachine As common.Controls.MyComboBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents BtnStart As Telerik.WinControls.UI.RadButton
    Friend WithEvents cboComPort As common.Controls.MyComboBox
    Friend WithEvents lblComPort As common.Controls.MyLabel
    Friend WithEvents txtManualCheck As common.MyNumBox
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents RadButton3 As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblWeight As common.Controls.MyLabel
    Friend WithEvents Timer2 As System.Windows.Forms.Timer

End Class
