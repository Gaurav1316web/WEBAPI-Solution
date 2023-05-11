Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCashDiscountReport
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
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.Route = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgRoute = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkSelect = New common.Controls.MyRadioButton
        Me.chkSelectAll = New common.Controls.MyRadioButton
        Me.lblToRouteDesc = New common.Controls.MyLabel
        Me.lblfrmRouteDesc = New common.Controls.MyLabel
        Me.RadioBtnSummary = New Telerik.WinControls.UI.RadRadioButton
        Me.RadioBtnDetail = New Telerik.WinControls.UI.RadRadioButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.dtpend = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadLabel7 = New common.Controls.MyLabel
        Me.dtpstart = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadLabel8 = New common.Controls.MyLabel
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.Route, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Route.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSelectAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfrmRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioBtnSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioBtnDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpstart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.Route)
        Me.RadGroupBox5.Controls.Add(Me.lblToRouteDesc)
        Me.RadGroupBox5.Controls.Add(Me.lblfrmRouteDesc)
        Me.RadGroupBox5.Controls.Add(Me.RadioBtnSummary)
        Me.RadGroupBox5.Controls.Add(Me.RadioBtnDetail)
        Me.RadGroupBox5.Controls.Add(Me.btnclose)
        Me.RadGroupBox5.Controls.Add(Me.btnreset)
        Me.RadGroupBox5.Controls.Add(Me.btnprint)
        Me.RadGroupBox5.Controls.Add(Me.dtpend)
        Me.RadGroupBox5.Controls.Add(Me.RadLabel7)
        Me.RadGroupBox5.Controls.Add(Me.dtpstart)
        Me.RadGroupBox5.Controls.Add(Me.RadLabel8)
        Me.RadGroupBox5.HeaderText = ""
        Me.RadGroupBox5.Location = New System.Drawing.Point(3, 6)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(526, 373)
        Me.RadGroupBox5.TabIndex = 4
        '
        'Route
        '
        Me.Route.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Route.Controls.Add(Me.cbgRoute)
        Me.Route.Controls.Add(Me.Panel3)
        Me.Route.HeaderText = "Route"
        Me.Route.Location = New System.Drawing.Point(9, 55)
        Me.Route.Name = "Route"
        Me.Route.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Route.Size = New System.Drawing.Size(504, 283)
        Me.Route.TabIndex = 49
        Me.Route.Text = "Route"
        '
        'cbgRoute
        '
        Me.cbgRoute.CheckedValue = Nothing
        Me.cbgRoute.DataSource = Nothing
        Me.cbgRoute.DisplayMember = "Name"
        Me.cbgRoute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgRoute.Location = New System.Drawing.Point(10, 40)
        Me.cbgRoute.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgRoute.MyShowHeadrText = False
        Me.cbgRoute.Name = "cbgRoute"
        Me.cbgRoute.Size = New System.Drawing.Size(484, 233)
        Me.cbgRoute.TabIndex = 1
        Me.cbgRoute.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkSelect)
        Me.Panel3.Controls.Add(Me.chkSelectAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(484, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkSelect
        '
        Me.chkSelect.Location = New System.Drawing.Point(196, 1)
        Me.chkSelect.MyLinkLable1 = Nothing
        Me.chkSelect.MyLinkLable2 = Nothing
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkSelect.TabIndex = 1
        Me.chkSelect.Text = "Select"
        '
        'chkSelectAll
        '
        Me.chkSelectAll.Location = New System.Drawing.Point(145, 1)
        Me.chkSelectAll.MyLinkLable1 = Nothing
        Me.chkSelectAll.MyLinkLable2 = Nothing
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(33, 18)
        Me.chkSelectAll.TabIndex = 0
        Me.chkSelectAll.Text = "All"
        '
        'lblToRouteDesc
        '
        Me.lblToRouteDesc.BorderVisible = True
        Me.lblToRouteDesc.Location = New System.Drawing.Point(182, 82)
        Me.lblToRouteDesc.Name = "lblToRouteDesc"
        Me.lblToRouteDesc.Size = New System.Drawing.Size(2, 2)
        Me.lblToRouteDesc.TabIndex = 0
        '
        'lblfrmRouteDesc
        '
        Me.lblfrmRouteDesc.BorderVisible = True
        Me.lblfrmRouteDesc.Location = New System.Drawing.Point(182, 58)
        Me.lblfrmRouteDesc.Name = "lblfrmRouteDesc"
        Me.lblfrmRouteDesc.Size = New System.Drawing.Size(2, 2)
        Me.lblfrmRouteDesc.TabIndex = 25
        '
        'RadioBtnSummary
        '
        Me.RadioBtnSummary.Location = New System.Drawing.Point(12, 31)
        Me.RadioBtnSummary.Name = "RadioBtnSummary"
        Me.RadioBtnSummary.Size = New System.Drawing.Size(67, 18)
        Me.RadioBtnSummary.TabIndex = 2
        Me.RadioBtnSummary.Text = "Summary"
        '
        'RadioBtnDetail
        '
        Me.RadioBtnDetail.Location = New System.Drawing.Point(95, 31)
        Me.RadioBtnDetail.Name = "RadioBtnDetail"
        Me.RadioBtnDetail.Size = New System.Drawing.Size(49, 18)
        Me.RadioBtnDetail.TabIndex = 3
        Me.RadioBtnDetail.Text = "Detail"
        '
        'btnclose
        '
        Me.btnclose.Location = New System.Drawing.Point(435, 344)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 6
        Me.btnclose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Location = New System.Drawing.Point(11, 344)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 5
        Me.btnreset.Text = "Reset"
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(85, 344)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(68, 18)
        Me.btnprint.TabIndex = 4
        Me.btnprint.Text = "Print"
        '
        'dtpend
        '
        Me.dtpend.CustomFormat = "dd/MM/yyyy"
        Me.dtpend.Location = New System.Drawing.Point(311, 4)
        Me.dtpend.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpend.Name = "dtpend"
        Me.dtpend.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpend.Size = New System.Drawing.Size(134, 20)
        Me.dtpend.TabIndex = 1
        Me.dtpend.TabStop = False
        Me.dtpend.Text = "Wednesday, November 16, 2011"
        Me.dtpend.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'RadLabel7
        '
        Me.RadLabel7.Location = New System.Drawing.Point(10, 6)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(56, 18)
        Me.RadLabel7.TabIndex = 11
        Me.RadLabel7.Text = "Start Date"
        '
        'dtpstart
        '
        Me.dtpstart.CustomFormat = "dd/MM/yyyy"
        Me.dtpstart.Location = New System.Drawing.Point(80, 6)
        Me.dtpstart.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpstart.Name = "dtpstart"
        Me.dtpstart.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpstart.Size = New System.Drawing.Size(134, 20)
        Me.dtpstart.TabIndex = 0
        Me.dtpstart.TabStop = False
        Me.dtpstart.Text = "Wednesday, November 16, 2011"
        Me.dtpstart.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'RadLabel8
        '
        Me.RadLabel8.Location = New System.Drawing.Point(245, 6)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(52, 18)
        Me.RadLabel8.TabIndex = 12
        Me.RadLabel8.Text = "End Date"
        '
        'FrmCashDiscountReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(533, 385)
        Me.Controls.Add(Me.RadGroupBox5)
        Me.Name = "FrmCashDiscountReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmCashDiscountReport"
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.Route, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Route.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSelectAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfrmRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioBtnSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioBtnDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpstart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadioBtnSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadioBtnDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpend As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpstart As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents Route As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgRoute As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkSelect As common.Controls.MyRadioButton
    Friend WithEvents chkSelectAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents lblToRouteDesc As common.Controls.MyLabel
    Friend WithEvents lblfrmRouteDesc As common.Controls.MyLabel
End Class

