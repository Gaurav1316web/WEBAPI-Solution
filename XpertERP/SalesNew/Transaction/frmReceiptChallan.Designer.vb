<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReceiptChallan
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
        Me.components = New System.ComponentModel.Container
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.btnGo = New Telerik.WinControls.UI.RadButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.fndLocation = New common.UserControls.txtFinder
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.lblCustName = New common.Controls.MyLabel
        Me.fndCustCode = New common.UserControls.txtFinder
        Me.lblCustomerCode = New common.Controls.MyLabel
        Me.fndVehicleCode = New common.UserControls.txtFinder
        Me.lblVehicleCode = New common.Controls.MyLabel
        Me.dtpTo = New common.Controls.MyDateTimePicker
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel
        Me.dtpFrom = New common.Controls.MyDateTimePicker
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel
        Me.lblVehicleName = New common.Controls.MyLabel
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton
        Me.Export = New Telerik.WinControls.UI.RadMenuItem
        Me.PDF = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.LblLocationDesc = New common.Controls.MyLabel
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(893, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 18)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(980, 412)
        Me.SplitContainer2.SplitterDistance = 109
        Me.SplitContainer2.TabIndex = 90
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(928, 72)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(45, 18)
        Me.btnGo.TabIndex = 1
        Me.btnGo.Text = ">>"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LblLocationDesc)
        Me.GroupBox1.Controls.Add(Me.fndLocation)
        Me.GroupBox1.Controls.Add(Me.MyLabel1)
        Me.GroupBox1.Controls.Add(Me.lblCustName)
        Me.GroupBox1.Controls.Add(Me.fndCustCode)
        Me.GroupBox1.Controls.Add(Me.lblCustomerCode)
        Me.GroupBox1.Controls.Add(Me.fndVehicleCode)
        Me.GroupBox1.Controls.Add(Me.lblVehicleCode)
        Me.GroupBox1.Controls.Add(Me.dtpTo)
        Me.GroupBox1.Controls.Add(Me.RadLabel2)
        Me.GroupBox1.Controls.Add(Me.dtpFrom)
        Me.GroupBox1.Controls.Add(Me.RadLabel1)
        Me.GroupBox1.Controls.Add(Me.lblVehicleName)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 17)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(910, 88)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'fndLocation
        '
        Me.fndLocation.AccessibleName = "fndCustCode"
        Me.fndLocation.Location = New System.Drawing.Point(110, 63)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Nothing
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.Size = New System.Drawing.Size(151, 19)
        Me.fndLocation.TabIndex = 18
        Me.fndLocation.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(14, 66)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel1.TabIndex = 19
        Me.MyLabel1.Text = "Location Code"
        '
        'lblCustName
        '
        Me.lblCustName.AutoSize = False
        Me.lblCustName.BorderVisible = True
        Me.lblCustName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustName.Location = New System.Drawing.Point(267, 41)
        Me.lblCustName.Name = "lblCustName"
        Me.lblCustName.Size = New System.Drawing.Size(190, 18)
        Me.lblCustName.TabIndex = 3
        Me.lblCustName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustName.TextWrap = False
        '
        'fndCustCode
        '
        Me.fndCustCode.AccessibleName = "fndCustCode"
        Me.fndCustCode.Location = New System.Drawing.Point(110, 40)
        Me.fndCustCode.MendatroryField = True
        Me.fndCustCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCustCode.MyLinkLable1 = Nothing
        Me.fndCustCode.MyLinkLable2 = Nothing
        Me.fndCustCode.MyReadOnly = False
        Me.fndCustCode.Name = "fndCustCode"
        Me.fndCustCode.Size = New System.Drawing.Size(151, 19)
        Me.fndCustCode.TabIndex = 2
        Me.fndCustCode.Value = ""
        '
        'lblCustomerCode
        '
        Me.lblCustomerCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerCode.Location = New System.Drawing.Point(14, 43)
        Me.lblCustomerCode.Name = "lblCustomerCode"
        Me.lblCustomerCode.Size = New System.Drawing.Size(85, 16)
        Me.lblCustomerCode.TabIndex = 17
        Me.lblCustomerCode.Text = "Customer Code"
        '
        'fndVehicleCode
        '
        Me.fndVehicleCode.AccessibleName = "fndVehicleCode"
        Me.fndVehicleCode.Location = New System.Drawing.Point(557, 40)
        Me.fndVehicleCode.MendatroryField = True
        Me.fndVehicleCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVehicleCode.MyLinkLable1 = Nothing
        Me.fndVehicleCode.MyLinkLable2 = Nothing
        Me.fndVehicleCode.MyReadOnly = False
        Me.fndVehicleCode.Name = "fndVehicleCode"
        Me.fndVehicleCode.Size = New System.Drawing.Size(145, 19)
        Me.fndVehicleCode.TabIndex = 4
        Me.fndVehicleCode.Value = ""
        Me.fndVehicleCode.Visible = False
        '
        'lblVehicleCode
        '
        Me.lblVehicleCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicleCode.Location = New System.Drawing.Point(470, 43)
        Me.lblVehicleCode.Name = "lblVehicleCode"
        Me.lblVehicleCode.Size = New System.Drawing.Size(74, 16)
        Me.lblVehicleCode.TabIndex = 15
        Me.lblVehicleCode.Text = "Vehicle Code"
        Me.lblVehicleCode.Visible = False
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd/MM/yyyy"
        Me.dtpTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(557, 19)
        Me.dtpTo.MendatroryField = False
        Me.dtpTo.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTo.MyLinkLable1 = Nothing
        Me.dtpTo.MyLinkLable2 = Nothing
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTo.Size = New System.Drawing.Size(151, 18)
        Me.dtpTo.TabIndex = 1
        Me.dtpTo.TabStop = False
        Me.dtpTo.Text = "19/03/2012"
        Me.dtpTo.Value = New Date(2012, 3, 19, 0, 0, 0, 0)
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(525, 19)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 13
        Me.RadLabel2.Text = "To"
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd/MM/yyyy"
        Me.dtpFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(110, 19)
        Me.dtpFrom.MendatroryField = False
        Me.dtpFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFrom.MyLinkLable1 = Nothing
        Me.dtpFrom.MyLinkLable2 = Nothing
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFrom.Size = New System.Drawing.Size(145, 18)
        Me.dtpFrom.TabIndex = 0
        Me.dtpFrom.TabStop = False
        Me.dtpFrom.Text = "19/03/2012"
        Me.dtpFrom.Value = New Date(2012, 3, 19, 0, 0, 0, 0)
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(14, 19)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "From"
        '
        'lblVehicleName
        '
        Me.lblVehicleName.AutoSize = False
        Me.lblVehicleName.BorderVisible = True
        Me.lblVehicleName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicleName.Location = New System.Drawing.Point(708, 41)
        Me.lblVehicleName.Name = "lblVehicleName"
        Me.lblVehicleName.Size = New System.Drawing.Size(190, 18)
        Me.lblVehicleName.TabIndex = 5
        Me.lblVehicleName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVehicleName.TextWrap = False
        Me.lblVehicleName.Visible = False
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.Size = New System.Drawing.Size(980, 299)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(6, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(980, 441)
        Me.SplitContainer1.SplitterDistance = 412
        Me.SplitContainer1.TabIndex = 92
        '
        'btnExport
        '
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Export, Me.PDF})
        Me.btnExport.Location = New System.Drawing.Point(92, 3)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(83, 19)
        Me.btnExport.TabIndex = 94
        Me.btnExport.Text = "Export"
        Me.btnExport.Visible = False
        '
        'Export
        '
        Me.Export.AccessibleDescription = "Export"
        Me.Export.AccessibleName = "Export"
        Me.Export.Image = Global.ERP.My.Resources.Resources.MSE
        Me.Export.Name = "Export"
        Me.Export.Text = "Export"
        Me.Export.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'PDF
        '
        Me.PDF.AccessibleDescription = "PDF"
        Me.PDF.AccessibleName = "PDF"
        Me.PDF.Image = Global.ERP.My.Resources.Resources.pdf
        Me.PDF.Name = "PDF"
        Me.PDF.Text = "PDF"
        Me.PDF.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(980, 20)
        Me.RadMenu1.TabIndex = 93
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Save Layout"
        Me.RadMenuItem2.AccessibleName = "Save Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Save Layout"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem3.AccessibleName = "Delete Layout"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Delete Layout"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'LblLocationDesc
        '
        Me.LblLocationDesc.AutoSize = False
        Me.LblLocationDesc.BorderVisible = True
        Me.LblLocationDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLocationDesc.Location = New System.Drawing.Point(267, 64)
        Me.LblLocationDesc.Name = "LblLocationDesc"
        Me.LblLocationDesc.Size = New System.Drawing.Size(190, 18)
        Me.LblLocationDesc.TabIndex = 20
        Me.LblLocationDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblLocationDesc.TextWrap = False
        '
        'frmReceiptChallan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(980, 441)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmReceiptChallan"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Receipt Challan"
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblVehicleName As common.Controls.MyLabel
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents dtpTo As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents dtpFrom As common.Controls.MyDateTimePicker
    Friend WithEvents fndCustCode As common.UserControls.txtFinder
    Friend WithEvents lblCustomerCode As common.Controls.MyLabel
    Friend WithEvents fndVehicleCode As common.UserControls.txtFinder
    Friend WithEvents lblVehicleCode As common.Controls.MyLabel
    Friend WithEvents lblCustName As common.Controls.MyLabel
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents LblLocationDesc As common.Controls.MyLabel
End Class

