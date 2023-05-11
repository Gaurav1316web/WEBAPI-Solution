Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptRegisterOfDeduction
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
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem9 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem10 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem11 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem12 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem13 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem14 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem15 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem16 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem17 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem18 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Cboyear = New common.Controls.MyComboBox()
        Me.lblYear = New common.Controls.MyLabel()
        Me.cboMonth = New common.Controls.MyComboBox()
        Me.lblMonth = New common.Controls.MyLabel()
        Me.fndEmployeeCode = New common.UserControls.txtFinder()
        Me.lblEmpCode = New common.Controls.MyLabel()
        Me.lblEmployeeName = New common.Controls.MyLabel()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        'CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Cboyear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmployeeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(1127, 406)
        Me.SplitContainer1.SplitterDistance = 368
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
        Me.RadPageView1.Size = New System.Drawing.Size(1127, 368)
        Me.RadPageView1.TabIndex = 9
        Me.RadPageView1.TabStop = False
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1106, 320)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Cboyear)
        Me.GroupBox1.Controls.Add(Me.lblYear)
        Me.GroupBox1.Controls.Add(Me.cboMonth)
        Me.GroupBox1.Controls.Add(Me.lblMonth)
        Me.GroupBox1.Controls.Add(Me.fndEmployeeCode)
        Me.GroupBox1.Controls.Add(Me.lblEmpCode)
        Me.GroupBox1.Controls.Add(Me.lblEmployeeName)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(490, 80)
        Me.GroupBox1.TabIndex = 60
        Me.GroupBox1.TabStop = False
        '
        'Cboyear
        '
        Me.Cboyear.AutoCompleteDisplayMember = Nothing
        Me.Cboyear.AutoCompleteValueMember = Nothing
        Me.Cboyear.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.Cboyear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem4.Text = "Damage"
        RadListDataItem5.Text = "Loss"
        RadListDataItem6.Text = "Fine"
        Me.Cboyear.Items.Add(RadListDataItem4)
        Me.Cboyear.Items.Add(RadListDataItem5)
        Me.Cboyear.Items.Add(RadListDataItem6)
        Me.Cboyear.Location = New System.Drawing.Point(320, 23)
        Me.Cboyear.MendatroryField = True
        Me.Cboyear.MyLinkLable1 = Me.lblYear
        Me.Cboyear.MyLinkLable2 = Nothing
        Me.Cboyear.Name = "Cboyear"
        Me.Cboyear.Size = New System.Drawing.Size(155, 18)
        Me.Cboyear.TabIndex = 421
        '
        'lblYear
        '
        Me.lblYear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYear.Location = New System.Drawing.Point(284, 25)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(30, 16)
        Me.lblYear.TabIndex = 422
        Me.lblYear.Text = "Year"
        '
        'cboMonth
        '
        Me.cboMonth.AutoCompleteDisplayMember = Nothing
        Me.cboMonth.AutoCompleteValueMember = Nothing
        Me.cboMonth.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboMonth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem7.Text = "January"
        RadListDataItem8.Text = "Febuary"
        RadListDataItem9.Text = "March"
        RadListDataItem10.Text = "April"
        RadListDataItem11.Text = "May"
        RadListDataItem12.Text = "June"
        RadListDataItem13.Text = "July"
        RadListDataItem14.Text = "August"
        RadListDataItem15.Text = "September"
        RadListDataItem16.Text = "October"
        RadListDataItem17.Text = "November"
        RadListDataItem18.Text = "December"
        Me.cboMonth.Items.Add(RadListDataItem7)
        Me.cboMonth.Items.Add(RadListDataItem8)
        Me.cboMonth.Items.Add(RadListDataItem9)
        Me.cboMonth.Items.Add(RadListDataItem10)
        Me.cboMonth.Items.Add(RadListDataItem11)
        Me.cboMonth.Items.Add(RadListDataItem12)
        Me.cboMonth.Items.Add(RadListDataItem13)
        Me.cboMonth.Items.Add(RadListDataItem14)
        Me.cboMonth.Items.Add(RadListDataItem15)
        Me.cboMonth.Items.Add(RadListDataItem16)
        Me.cboMonth.Items.Add(RadListDataItem17)
        Me.cboMonth.Items.Add(RadListDataItem18)
        Me.cboMonth.Location = New System.Drawing.Point(112, 23)
        Me.cboMonth.MendatroryField = True
        Me.cboMonth.MyLinkLable1 = Me.lblMonth
        Me.cboMonth.MyLinkLable2 = Nothing
        Me.cboMonth.Name = "cboMonth"
        Me.cboMonth.Size = New System.Drawing.Size(155, 18)
        Me.cboMonth.TabIndex = 419
        '
        'lblMonth
        '
        Me.lblMonth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonth.Location = New System.Drawing.Point(14, 23)
        Me.lblMonth.Name = "lblMonth"
        Me.lblMonth.Size = New System.Drawing.Size(38, 16)
        Me.lblMonth.TabIndex = 420
        Me.lblMonth.Text = "Month"
        '
        'fndEmployeeCode
        '
        Me.fndEmployeeCode.Location = New System.Drawing.Point(112, 48)
        Me.fndEmployeeCode.MendatroryField = False
        Me.fndEmployeeCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndEmployeeCode.MyLinkLable1 = Me.lblEmpCode
        Me.fndEmployeeCode.MyLinkLable2 = Me.lblEmployeeName
        Me.fndEmployeeCode.MyReadOnly = False
        Me.fndEmployeeCode.MyShowMasterFormButton = False
        Me.fndEmployeeCode.Name = "fndEmployeeCode"
        Me.fndEmployeeCode.Size = New System.Drawing.Size(155, 18)
        Me.fndEmployeeCode.TabIndex = 56
        Me.fndEmployeeCode.Value = ""
        '
        'lblEmpCode
        '
        Me.lblEmpCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblEmpCode.Location = New System.Drawing.Point(14, 48)
        Me.lblEmpCode.Name = "lblEmpCode"
        Me.lblEmpCode.Size = New System.Drawing.Size(87, 16)
        Me.lblEmpCode.TabIndex = 57
        Me.lblEmpCode.Text = "Employee Code"
        '
        'lblEmployeeName
        '
        Me.lblEmployeeName.AutoSize = False
        Me.lblEmployeeName.BorderVisible = True
        Me.lblEmployeeName.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblEmployeeName.Location = New System.Drawing.Point(284, 47)
        Me.lblEmployeeName.Name = "lblEmployeeName"
        Me.lblEmployeeName.Size = New System.Drawing.Size(191, 18)
        Me.lblEmployeeName.TabIndex = 58
        Me.lblEmployeeName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblEmployeeName.TextWrap = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1035, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(84, 22)
        Me.btnclose.TabIndex = 25
        Me.btnclose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(10, 7)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 24
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(87, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 26
        Me.btnReset.Text = "Reset"
        '
        'RptRegisterOfDeduction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1127, 406)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptRegisterOfDeduction"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptRegisterOfDeduction"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        'CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.Cboyear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmployeeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents fndEmployeeCode As common.UserControls.txtFinder
    Friend WithEvents lblEmpCode As common.Controls.MyLabel
    Friend WithEvents lblEmployeeName As common.Controls.MyLabel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents Cboyear As common.Controls.MyComboBox
    Friend WithEvents lblYear As common.Controls.MyLabel
    Friend WithEvents cboMonth As common.Controls.MyComboBox
    Friend WithEvents lblMonth As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
End Class
