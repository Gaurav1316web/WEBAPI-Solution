<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPortSettings
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem9 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem10 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem11 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem12 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.grpMachineType = New Telerik.WinControls.UI.RadGroupBox
        Me.chkEkoProMachine = New Telerik.WinControls.UI.RadRadioButton
        Me.chkWeighingMachine = New Telerik.WinControls.UI.RadRadioButton
        Me.ddlBaudRate = New common.Controls.MyComboBox
        Me.lblBaudRate = New common.Controls.MyLabel
        Me.ddlParity = New common.Controls.MyComboBox
        Me.lblParity = New common.Controls.MyLabel
        Me.ddlDataBits = New common.Controls.MyComboBox
        Me.lblDataBits = New common.Controls.MyLabel
        Me.ddlStopBits = New common.Controls.MyComboBox
        Me.lblStopBits = New common.Controls.MyLabel
        Me.RadSplitContainer1 = New Telerik.WinControls.UI.RadSplitContainer
        Me.SplitPanel1 = New Telerik.WinControls.UI.SplitPanel
        Me.CboDataForm = New common.Controls.MyComboBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.CboMachine = New common.Controls.MyComboBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.SplitPanel2 = New Telerik.WinControls.UI.SplitPanel
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        CType(Me.grpMachineType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMachineType.SuspendLayout()
        CType(Me.chkEkoProMachine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkWeighingMachine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlBaudRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBaudRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlParity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblParity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlDataBits, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDataBits, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlStopBits, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStopBits, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadSplitContainer1.SuspendLayout()
        CType(Me.SplitPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel1.SuspendLayout()
        CType(Me.CboDataForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboMachine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel2.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpMachineType
        '
        Me.grpMachineType.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpMachineType.Controls.Add(Me.chkEkoProMachine)
        Me.grpMachineType.Controls.Add(Me.chkWeighingMachine)
        Me.grpMachineType.HeaderText = "Select Type Of Machine"
        Me.grpMachineType.Location = New System.Drawing.Point(6, 3)
        Me.grpMachineType.Name = "grpMachineType"
        Me.grpMachineType.Size = New System.Drawing.Size(331, 44)
        Me.grpMachineType.TabIndex = 11
        Me.grpMachineType.Text = "Select Type Of Machine"
        '
        'chkEkoProMachine
        '
        Me.chkEkoProMachine.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEkoProMachine.Location = New System.Drawing.Point(5, 18)
        Me.chkEkoProMachine.Name = "chkEkoProMachine"
        Me.chkEkoProMachine.Size = New System.Drawing.Size(87, 18)
        Me.chkEkoProMachine.TabIndex = 0
        Me.chkEkoProMachine.TabStop = True
        Me.chkEkoProMachine.Text = "Milk Analyzer"
        Me.chkEkoProMachine.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'chkWeighingMachine
        '
        Me.chkWeighingMachine.Location = New System.Drawing.Point(209, 19)
        Me.chkWeighingMachine.Name = "chkWeighingMachine"
        Me.chkWeighingMachine.Size = New System.Drawing.Size(114, 18)
        Me.chkWeighingMachine.TabIndex = 1
        Me.chkWeighingMachine.Text = "Weighing Machine"
        '
        'ddlBaudRate
        '
        Me.ddlBaudRate.AllowShowFocusCues = False
        Me.ddlBaudRate.AutoCompleteDisplayMember = Nothing
        Me.ddlBaudRate.AutoCompleteValueMember = Nothing
        Me.ddlBaudRate.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "Yes"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "No"
        RadListDataItem2.TextWrap = True
        Me.ddlBaudRate.Items.Add(RadListDataItem1)
        Me.ddlBaudRate.Items.Add(RadListDataItem2)
        Me.ddlBaudRate.Location = New System.Drawing.Point(93, 98)
        Me.ddlBaudRate.MendatroryField = True
        Me.ddlBaudRate.MyLinkLable1 = Me.lblBaudRate
        Me.ddlBaudRate.MyLinkLable2 = Nothing
        Me.ddlBaudRate.Name = "ddlBaudRate"
        Me.ddlBaudRate.Size = New System.Drawing.Size(244, 20)
        Me.ddlBaudRate.TabIndex = 1
        '
        'lblBaudRate
        '
        Me.lblBaudRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBaudRate.Location = New System.Drawing.Point(11, 101)
        Me.lblBaudRate.Name = "lblBaudRate"
        Me.lblBaudRate.Size = New System.Drawing.Size(60, 16)
        Me.lblBaudRate.TabIndex = 66
        Me.lblBaudRate.Text = "Baud Rate"
        '
        'ddlParity
        '
        Me.ddlParity.AllowShowFocusCues = False
        Me.ddlParity.AutoCompleteDisplayMember = Nothing
        Me.ddlParity.AutoCompleteValueMember = Nothing
        Me.ddlParity.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem3.Text = "Yes"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "No"
        RadListDataItem4.TextWrap = True
        Me.ddlParity.Items.Add(RadListDataItem3)
        Me.ddlParity.Items.Add(RadListDataItem4)
        Me.ddlParity.Location = New System.Drawing.Point(93, 121)
        Me.ddlParity.MendatroryField = True
        Me.ddlParity.MyLinkLable1 = Me.lblParity
        Me.ddlParity.MyLinkLable2 = Nothing
        Me.ddlParity.Name = "ddlParity"
        Me.ddlParity.Size = New System.Drawing.Size(244, 20)
        Me.ddlParity.TabIndex = 2
        '
        'lblParity
        '
        Me.lblParity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblParity.Location = New System.Drawing.Point(11, 125)
        Me.lblParity.Name = "lblParity"
        Me.lblParity.Size = New System.Drawing.Size(35, 16)
        Me.lblParity.TabIndex = 68
        Me.lblParity.Text = "Parity"
        '
        'ddlDataBits
        '
        Me.ddlDataBits.AllowShowFocusCues = False
        Me.ddlDataBits.AutoCompleteDisplayMember = Nothing
        Me.ddlDataBits.AutoCompleteValueMember = Nothing
        Me.ddlDataBits.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem5.Text = "Yes"
        RadListDataItem5.TextWrap = True
        RadListDataItem6.Text = "No"
        RadListDataItem6.TextWrap = True
        Me.ddlDataBits.Items.Add(RadListDataItem5)
        Me.ddlDataBits.Items.Add(RadListDataItem6)
        Me.ddlDataBits.Location = New System.Drawing.Point(93, 143)
        Me.ddlDataBits.MendatroryField = True
        Me.ddlDataBits.MyLinkLable1 = Me.lblDataBits
        Me.ddlDataBits.MyLinkLable2 = Nothing
        Me.ddlDataBits.Name = "ddlDataBits"
        Me.ddlDataBits.Size = New System.Drawing.Size(244, 20)
        Me.ddlDataBits.TabIndex = 3
        '
        'lblDataBits
        '
        Me.lblDataBits.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDataBits.Location = New System.Drawing.Point(11, 147)
        Me.lblDataBits.Name = "lblDataBits"
        Me.lblDataBits.Size = New System.Drawing.Size(52, 16)
        Me.lblDataBits.TabIndex = 70
        Me.lblDataBits.Text = "Data Bits"
        '
        'ddlStopBits
        '
        Me.ddlStopBits.AllowShowFocusCues = False
        Me.ddlStopBits.AutoCompleteDisplayMember = Nothing
        Me.ddlStopBits.AutoCompleteValueMember = Nothing
        Me.ddlStopBits.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem7.Text = "Yes"
        RadListDataItem7.TextWrap = True
        RadListDataItem8.Text = "No"
        RadListDataItem8.TextWrap = True
        Me.ddlStopBits.Items.Add(RadListDataItem7)
        Me.ddlStopBits.Items.Add(RadListDataItem8)
        Me.ddlStopBits.Location = New System.Drawing.Point(93, 166)
        Me.ddlStopBits.MendatroryField = True
        Me.ddlStopBits.MyLinkLable1 = Me.lblStopBits
        Me.ddlStopBits.MyLinkLable2 = Nothing
        Me.ddlStopBits.Name = "ddlStopBits"
        Me.ddlStopBits.Size = New System.Drawing.Size(244, 20)
        Me.ddlStopBits.TabIndex = 4
        '
        'lblStopBits
        '
        Me.lblStopBits.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStopBits.Location = New System.Drawing.Point(11, 170)
        Me.lblStopBits.Name = "lblStopBits"
        Me.lblStopBits.Size = New System.Drawing.Size(52, 16)
        Me.lblStopBits.TabIndex = 72
        Me.lblStopBits.Text = "Stop Bits"
        '
        'RadSplitContainer1
        '
        Me.RadSplitContainer1.Controls.Add(Me.SplitPanel1)
        Me.RadSplitContainer1.Controls.Add(Me.SplitPanel2)
        Me.RadSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadSplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.RadSplitContainer1.Name = "RadSplitContainer1"
        Me.RadSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        '
        '
        Me.RadSplitContainer1.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.RadSplitContainer1.Size = New System.Drawing.Size(556, 432)
        Me.RadSplitContainer1.TabIndex = 75
        Me.RadSplitContainer1.TabStop = False
        Me.RadSplitContainer1.Text = "RadSplitContainer1"
        '
        'SplitPanel1
        '
        Me.SplitPanel1.Controls.Add(Me.CboDataForm)
        Me.SplitPanel1.Controls.Add(Me.MyLabel2)
        Me.SplitPanel1.Controls.Add(Me.CboMachine)
        Me.SplitPanel1.Controls.Add(Me.MyLabel1)
        Me.SplitPanel1.Controls.Add(Me.grpMachineType)
        Me.SplitPanel1.Controls.Add(Me.lblBaudRate)
        Me.SplitPanel1.Controls.Add(Me.ddlBaudRate)
        Me.SplitPanel1.Controls.Add(Me.ddlStopBits)
        Me.SplitPanel1.Controls.Add(Me.lblParity)
        Me.SplitPanel1.Controls.Add(Me.lblStopBits)
        Me.SplitPanel1.Controls.Add(Me.ddlParity)
        Me.SplitPanel1.Controls.Add(Me.ddlDataBits)
        Me.SplitPanel1.Controls.Add(Me.lblDataBits)
        Me.SplitPanel1.Location = New System.Drawing.Point(0, 0)
        Me.SplitPanel1.Name = "SplitPanel1"
        '
        '
        '
        Me.SplitPanel1.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel1.Size = New System.Drawing.Size(556, 387)
        Me.SplitPanel1.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0.0!, 0.4040248!)
        Me.SplitPanel1.SizeInfo.SplitterCorrection = New System.Drawing.Size(0, 141)
        Me.SplitPanel1.TabIndex = 0
        Me.SplitPanel1.TabStop = False
        Me.SplitPanel1.Text = "SplitPanel1"
        '
        'CboDataForm
        '
        Me.CboDataForm.AllowShowFocusCues = False
        Me.CboDataForm.AutoCompleteDisplayMember = Nothing
        Me.CboDataForm.AutoCompleteValueMember = Nothing
        Me.CboDataForm.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem9.Text = "Yes"
        RadListDataItem9.TextWrap = True
        RadListDataItem10.Text = "No"
        RadListDataItem10.TextWrap = True
        Me.CboDataForm.Items.Add(RadListDataItem9)
        Me.CboDataForm.Items.Add(RadListDataItem10)
        Me.CboDataForm.Location = New System.Drawing.Point(93, 75)
        Me.CboDataForm.MendatroryField = True
        Me.CboDataForm.MyLinkLable1 = Me.MyLabel2
        Me.CboDataForm.MyLinkLable2 = Nothing
        Me.CboDataForm.Name = "CboDataForm"
        Me.CboDataForm.Size = New System.Drawing.Size(244, 20)
        Me.CboDataForm.TabIndex = 75
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(11, 79)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel2.TabIndex = 76
        Me.MyLabel2.Text = "Data Format"
        '
        'CboMachine
        '
        Me.CboMachine.AllowShowFocusCues = False
        Me.CboMachine.AutoCompleteDisplayMember = Nothing
        Me.CboMachine.AutoCompleteValueMember = Nothing
        Me.CboMachine.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem11.Text = "Yes"
        RadListDataItem11.TextWrap = True
        RadListDataItem12.Text = "No"
        RadListDataItem12.TextWrap = True
        Me.CboMachine.Items.Add(RadListDataItem11)
        Me.CboMachine.Items.Add(RadListDataItem12)
        Me.CboMachine.Location = New System.Drawing.Point(93, 53)
        Me.CboMachine.MendatroryField = True
        Me.CboMachine.MyLinkLable1 = Me.MyLabel1
        Me.CboMachine.MyLinkLable2 = Nothing
        Me.CboMachine.Name = "CboMachine"
        Me.CboMachine.Size = New System.Drawing.Size(244, 20)
        Me.CboMachine.TabIndex = 73
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(11, 57)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(80, 16)
        Me.MyLabel1.TabIndex = 74
        Me.MyLabel1.Text = "Machine Make"
        '
        'SplitPanel2
        '
        Me.SplitPanel2.Controls.Add(Me.btnClose)
        Me.SplitPanel2.Controls.Add(Me.btnDelete)
        Me.SplitPanel2.Controls.Add(Me.btnSave)
        Me.SplitPanel2.Location = New System.Drawing.Point(0, 391)
        Me.SplitPanel2.Name = "SplitPanel2"
        '
        '
        '
        Me.SplitPanel2.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel2.Size = New System.Drawing.Size(556, 41)
        Me.SplitPanel2.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0.0!, -0.4040248!)
        Me.SplitPanel2.SizeInfo.SplitterCorrection = New System.Drawing.Size(0, -141)
        Me.SplitPanel2.TabIndex = 1
        Me.SplitPanel2.TabStop = False
        Me.SplitPanel2.Text = "SplitPanel2"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(485, 20)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(74, 20)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(4, 19)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'FrmPortSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(556, 432)
        Me.Controls.Add(Me.RadSplitContainer1)
        Me.Name = "FrmPortSettings"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmPortSettings"
        CType(Me.grpMachineType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMachineType.ResumeLayout(False)
        Me.grpMachineType.PerformLayout()
        CType(Me.chkEkoProMachine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkWeighingMachine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlBaudRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBaudRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlParity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblParity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlDataBits, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDataBits, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlStopBits, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStopBits, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadSplitContainer1.ResumeLayout(False)
        CType(Me.SplitPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel1.ResumeLayout(False)
        Me.SplitPanel1.PerformLayout()
        CType(Me.CboDataForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboMachine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel2.ResumeLayout(False)
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpMachineType As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkEkoProMachine As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkWeighingMachine As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents ddlBaudRate As common.Controls.MyComboBox
    Friend WithEvents lblBaudRate As common.Controls.MyLabel
    Friend WithEvents ddlParity As common.Controls.MyComboBox
    Friend WithEvents lblParity As common.Controls.MyLabel
    Friend WithEvents ddlDataBits As common.Controls.MyComboBox
    Friend WithEvents lblDataBits As common.Controls.MyLabel
    Friend WithEvents ddlStopBits As common.Controls.MyComboBox
    Friend WithEvents lblStopBits As common.Controls.MyLabel
    Friend WithEvents RadSplitContainer1 As Telerik.WinControls.UI.RadSplitContainer
    Friend WithEvents SplitPanel1 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents SplitPanel2 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents CboMachine As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents CboDataForm As common.Controls.MyComboBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
End Class

