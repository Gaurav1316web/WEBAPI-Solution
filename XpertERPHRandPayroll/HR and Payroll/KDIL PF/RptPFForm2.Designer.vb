Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptPFForm2
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.fndDivisionCode = New common.UserControls.txtMultiSelectFinder()
        Me.fndLocationCode = New common.UserControls.txtMultiSelectFinder()
        Me.MylblEmployee = New common.Controls.MyLabel()
        Me.FndEmployeeMult = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblEmpCode = New common.Controls.MyLabel()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MylblEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpCode, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(1151, 416)
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
        Me.RadPageView1.RootElement.Text = "Report1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1151, 379)
        Me.RadPageView1.TabIndex = 7
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
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1130, 331)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.fndDivisionCode)
        Me.GroupBox1.Controls.Add(Me.fndLocationCode)
        Me.GroupBox1.Controls.Add(Me.MylblEmployee)
        Me.GroupBox1.Controls.Add(Me.FndEmployeeMult)
        Me.GroupBox1.Controls.Add(Me.lblLocation)
        Me.GroupBox1.Controls.Add(Me.lblEmpCode)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(551, 123)
        Me.GroupBox1.TabIndex = 60
        Me.GroupBox1.TabStop = False
        '
        'fndDivisionCode
        '
        Me.fndDivisionCode.arrDispalyMember = Nothing
        Me.fndDivisionCode.arrValueMember = Nothing
        Me.fndDivisionCode.Location = New System.Drawing.Point(112, 44)
        Me.fndDivisionCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDivisionCode.MyLinkLable1 = Nothing
        Me.fndDivisionCode.MyLinkLable2 = Nothing
        Me.fndDivisionCode.MyNullText = "All"
        Me.fndDivisionCode.Name = "fndDivisionCode"
        Me.fndDivisionCode.Size = New System.Drawing.Size(427, 19)
        Me.fndDivisionCode.TabIndex = 352
        '
        'fndLocationCode
        '
        Me.fndLocationCode.arrDispalyMember = Nothing
        Me.fndLocationCode.arrValueMember = Nothing
        Me.fndLocationCode.Location = New System.Drawing.Point(112, 17)
        Me.fndLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocationCode.MyLinkLable1 = Nothing
        Me.fndLocationCode.MyLinkLable2 = Nothing
        Me.fndLocationCode.MyNullText = "All"
        Me.fndLocationCode.Name = "fndLocationCode"
        Me.fndLocationCode.Size = New System.Drawing.Size(427, 19)
        Me.fndLocationCode.TabIndex = 352
        '
        'MylblEmployee
        '
        Me.MylblEmployee.FieldName = Nothing
        Me.MylblEmployee.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MylblEmployee.Location = New System.Drawing.Point(15, 70)
        Me.MylblEmployee.Name = "MylblEmployee"
        Me.MylblEmployee.Size = New System.Drawing.Size(57, 16)
        Me.MylblEmployee.TabIndex = 352
        Me.MylblEmployee.Text = "Employee"
        '
        'FndEmployeeMult
        '
        Me.FndEmployeeMult.arrDispalyMember = Nothing
        Me.FndEmployeeMult.arrValueMember = Nothing
        Me.FndEmployeeMult.Location = New System.Drawing.Point(112, 67)
        Me.FndEmployeeMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndEmployeeMult.MyLinkLable1 = Nothing
        Me.FndEmployeeMult.MyLinkLable2 = Nothing
        Me.FndEmployeeMult.MyNullText = "All"
        Me.FndEmployeeMult.Name = "FndEmployeeMult"
        Me.FndEmployeeMult.Size = New System.Drawing.Size(427, 19)
        Me.FndEmployeeMult.TabIndex = 351
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblLocation.Location = New System.Drawing.Point(15, 22)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 60
        Me.lblLocation.Text = "Location"
        '
        'lblEmpCode
        '
        Me.lblEmpCode.FieldName = Nothing
        Me.lblEmpCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblEmpCode.Location = New System.Drawing.Point(15, 44)
        Me.lblEmpCode.Name = "lblEmpCode"
        Me.lblEmpCode.Size = New System.Drawing.Size(76, 16)
        Me.lblEmpCode.TabIndex = 57
        Me.lblEmpCode.Text = "Division Code"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1055, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(84, 22)
        Me.btnclose.TabIndex = 21
        Me.btnclose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(13, 5)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 20
        Me.btnGo.Text = ">>>"
        '
        'RptPFForm2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1151, 416)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptPFForm2"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptPFForm2"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MylblEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblEmpCode As common.Controls.MyLabel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents MylblEmployee As common.Controls.MyLabel
    Friend WithEvents FndEmployeeMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents fndLocationCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents fndDivisionCode As common.UserControls.txtMultiSelectFinder
End Class

