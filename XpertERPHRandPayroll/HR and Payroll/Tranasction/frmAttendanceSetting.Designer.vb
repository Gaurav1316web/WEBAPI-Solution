Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAttendanceSetting
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
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.txtDescription = New common.Controls.MyTextBox
        Me.txtPayHeadName = New common.Controls.MyTextBox
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.MyLabel18 = New common.Controls.MyLabel
        Me.MyLabel19 = New common.Controls.MyLabel
        Me.ddlSalaryDepend = New common.Controls.MyComboBox
        Me.lblAttenCode = New common.Controls.MyLabel
        Me.ddlCalcBasis = New common.Controls.MyComboBox
        Me.findAttendanceCode = New common.UserControls.txtNavigator
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.ddlSalCalcOn = New common.Controls.MyComboBox
        Me.txtPrintName = New common.Controls.MyTextBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.ddlOTCode = New common.Controls.MyComboBox
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPayHeadName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlSalaryDepend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAttenCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlCalcBasis, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlSalCalcOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrintName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlOTCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.SplitContainer1)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(643, 302)
        Me.RadGroupBox3.TabIndex = 58
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(10, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPayHeadName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel18)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel19)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ddlSalaryDepend)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAttenCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ddlCalcBasis)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findAttendanceCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ddlSalCalcOn)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPrintName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ddlOTCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(623, 272)
        Me.SplitContainer1.SplitterDistance = 236
        Me.SplitContainer1.TabIndex = 133
        '
        'txtDescription
        '
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(183, 177)
        Me.txtDescription.MaxLength = 49
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Nothing
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(208, 18)
        Me.txtDescription.TabIndex = 7
        '
        'txtPayHeadName
        '
        Me.txtPayHeadName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayHeadName.Location = New System.Drawing.Point(183, 34)
        Me.txtPayHeadName.MaxLength = 49
        Me.txtPayHeadName.MendatroryField = False
        Me.txtPayHeadName.MyLinkLable1 = Nothing
        Me.txtPayHeadName.MyLinkLable2 = Nothing
        Me.txtPayHeadName.Name = "txtPayHeadName"
        Me.txtPayHeadName.Size = New System.Drawing.Size(208, 18)
        Me.txtPayHeadName.TabIndex = 1
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(8, 179)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel5.TabIndex = 132
        Me.MyLabel5.Text = "Description"
        '
        'MyLabel18
        '
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(8, 36)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(97, 16)
        Me.MyLabel18.TabIndex = 55
        Me.MyLabel18.Text = "Attendance Name"
        '
        'MyLabel19
        '
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(8, 84)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(105, 16)
        Me.MyLabel19.TabIndex = 107
        Me.MyLabel19.Text = "Salary Dependency"
        '
        'ddlSalaryDepend
        '
        Me.ddlSalaryDepend.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlSalaryDepend.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "Dependent"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Independent"
        RadListDataItem2.TextWrap = True
        Me.ddlSalaryDepend.Items.Add(RadListDataItem1)
        Me.ddlSalaryDepend.Items.Add(RadListDataItem2)
        Me.ddlSalaryDepend.Location = New System.Drawing.Point(183, 82)
        Me.ddlSalaryDepend.MendatroryField = False
        Me.ddlSalaryDepend.MyLinkLable1 = Nothing
        Me.ddlSalaryDepend.MyLinkLable2 = Nothing
        Me.ddlSalaryDepend.Name = "ddlSalaryDepend"
        Me.ddlSalaryDepend.Size = New System.Drawing.Size(208, 18)
        Me.ddlSalaryDepend.TabIndex = 3
        '
        'lblAttenCode
        '
        Me.lblAttenCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAttenCode.Location = New System.Drawing.Point(8, 9)
        Me.lblAttenCode.Name = "lblAttenCode"
        Me.lblAttenCode.Size = New System.Drawing.Size(94, 16)
        Me.lblAttenCode.TabIndex = 56
        Me.lblAttenCode.Text = "Attendance Code"
        '
        'ddlCalcBasis
        '
        Me.ddlCalcBasis.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlCalcBasis.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlCalcBasis.Location = New System.Drawing.Point(183, 153)
        Me.ddlCalcBasis.MendatroryField = False
        Me.ddlCalcBasis.MyLinkLable1 = Nothing
        Me.ddlCalcBasis.MyLinkLable2 = Nothing
        Me.ddlCalcBasis.Name = "ddlCalcBasis"
        Me.ddlCalcBasis.Size = New System.Drawing.Size(208, 18)
        Me.ddlCalcBasis.TabIndex = 6
        '
        'findAttendanceCode
        '
        Me.findAttendanceCode.Location = New System.Drawing.Point(182, 8)
        Me.findAttendanceCode.MendatroryField = True
        Me.findAttendanceCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.findAttendanceCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.findAttendanceCode.MyLinkLable1 = Nothing
        Me.findAttendanceCode.MyLinkLable2 = Nothing
        Me.findAttendanceCode.MyMaxLength = 12
        Me.findAttendanceCode.MyReadOnly = False
        Me.findAttendanceCode.Name = "findAttendanceCode"
        Me.findAttendanceCode.Size = New System.Drawing.Size(245, 21)
        Me.findAttendanceCode.TabIndex = 0
        Me.findAttendanceCode.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(7, 154)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(137, 16)
        Me.MyLabel4.TabIndex = 125
        Me.MyLabel4.Text = "Attendance Register Type"
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(8, 60)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel1.TabIndex = 119
        Me.MyLabel1.Text = "Print Name"
        '
        'ddlSalCalcOn
        '
        Me.ddlSalCalcOn.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlSalCalcOn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlSalCalcOn.Location = New System.Drawing.Point(182, 130)
        Me.ddlSalCalcOn.MendatroryField = False
        Me.ddlSalCalcOn.MyLinkLable1 = Nothing
        Me.ddlSalCalcOn.MyLinkLable2 = Nothing
        Me.ddlSalCalcOn.Name = "ddlSalCalcOn"
        Me.ddlSalCalcOn.Size = New System.Drawing.Size(208, 18)
        Me.ddlSalCalcOn.TabIndex = 5
        '
        'txtPrintName
        '
        Me.txtPrintName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrintName.Location = New System.Drawing.Point(183, 58)
        Me.txtPrintName.MaxLength = 49
        Me.txtPrintName.MendatroryField = False
        Me.txtPrintName.MyLinkLable1 = Nothing
        Me.txtPrintName.MyLinkLable2 = Nothing
        Me.txtPrintName.Name = "txtPrintName"
        Me.txtPrintName.Size = New System.Drawing.Size(208, 18)
        Me.txtPrintName.TabIndex = 2
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(7, 132)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(116, 16)
        Me.MyLabel3.TabIndex = 123
        Me.MyLabel3.Text = "Salary Calculation On"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(7, 108)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel2.TabIndex = 121
        Me.MyLabel2.Text = "OT Code"
        '
        'ddlOTCode
        '
        Me.ddlOTCode.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlOTCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlOTCode.Location = New System.Drawing.Point(182, 106)
        Me.ddlOTCode.MendatroryField = False
        Me.ddlOTCode.MyLinkLable1 = Nothing
        Me.ddlOTCode.MyLinkLable2 = Nothing
        Me.ddlOTCode.Name = "ddlOTCode"
        Me.ddlOTCode.Size = New System.Drawing.Size(208, 18)
        Me.ddlOTCode.TabIndex = 4
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(6, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 128
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(78, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 129
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(554, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 130
        Me.btnclose.Text = "Close"
        '
        'frmAttendanceSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(643, 302)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Name = "frmAttendanceSetting"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Attendance Setting"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPayHeadName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlSalaryDepend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAttenCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlCalcBasis, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlSalCalcOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrintName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlOTCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents ddlCalcBasis As common.Controls.MyComboBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents ddlSalCalcOn As common.Controls.MyComboBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents ddlOTCode As common.Controls.MyComboBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtPrintName As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents findAttendanceCode As common.UserControls.txtNavigator
    Friend WithEvents lblAttenCode As common.Controls.MyLabel
    Friend WithEvents ddlSalaryDepend As common.Controls.MyComboBox
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents txtPayHeadName As common.Controls.MyTextBox
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class
