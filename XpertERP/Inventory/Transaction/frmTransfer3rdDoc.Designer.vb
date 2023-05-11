<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTransfer3rdDoc
    Inherits Telerik.WinControls.UI.RadForm

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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnCreateTransfer = New Telerik.WinControls.UI.RadButton
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        Me.txtDate = New common.Controls.MyDateTimePicker
        Me.rdlbltransferdate = New common.Controls.MyLabel
        Me.lblVehicle = New common.Controls.MyLabel
        Me.txtVehicle = New common.UserControls.txtFinder
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.lblSalesman = New common.Controls.MyLabel
        Me.txtSalesman = New common.UserControls.txtFinder
        Me.lblSalesmenCode = New common.Controls.MyLabel
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.gv2 = New common.UserControls.MyRadGridView
        Me.Panel1.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCreateTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbltransferdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesmenCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnReset)
        Me.Panel1.Controls.Add(Me.btnCreateTransfer)
        Me.Panel1.Controls.Add(Me.btnRefresh)
        Me.Panel1.Controls.Add(Me.txtDate)
        Me.Panel1.Controls.Add(Me.rdlbltransferdate)
        Me.Panel1.Controls.Add(Me.lblVehicle)
        Me.Panel1.Controls.Add(Me.txtVehicle)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.lblSalesman)
        Me.Panel1.Controls.Add(Me.txtSalesman)
        Me.Panel1.Controls.Add(Me.lblSalesmenCode)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(725, 73)
        Me.Panel1.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(571, 52)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(62, 18)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(398, 27)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(62, 18)
        Me.btnReset.TabIndex = 2
        Me.btnReset.Text = "Reset"
        Me.btnReset.Visible = False
        '
        'btnCreateTransfer
        '
        Me.btnCreateTransfer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCreateTransfer.Location = New System.Drawing.Point(466, 52)
        Me.btnCreateTransfer.Name = "btnCreateTransfer"
        Me.btnCreateTransfer.Size = New System.Drawing.Size(99, 18)
        Me.btnCreateTransfer.TabIndex = 5
        Me.btnCreateTransfer.Text = "Create Transfer"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(398, 52)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(62, 18)
        Me.btnRefresh.TabIndex = 4
        Me.btnRefresh.Text = ">>"
        '
        'txtDate
        '
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.Location = New System.Drawing.Point(74, 3)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Nothing
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.Size = New System.Drawing.Size(95, 20)
        Me.txtDate.TabIndex = 0
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "27/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 27, 0, 0, 0, 0)
        '
        'rdlbltransferdate
        '
        Me.rdlbltransferdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlbltransferdate.Location = New System.Drawing.Point(4, 5)
        Me.rdlbltransferdate.Name = "rdlbltransferdate"
        Me.rdlbltransferdate.Size = New System.Drawing.Size(30, 16)
        Me.rdlbltransferdate.TabIndex = 11
        Me.rdlbltransferdate.Text = "Date"
        '
        'lblVehicle
        '
        Me.lblVehicle.AutoSize = False
        Me.lblVehicle.BorderVisible = True
        Me.lblVehicle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicle.Location = New System.Drawing.Point(212, 52)
        Me.lblVehicle.Name = "lblVehicle"
        Me.lblVehicle.Size = New System.Drawing.Size(180, 18)
        Me.lblVehicle.TabIndex = 7
        Me.lblVehicle.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVehicle.TextWrap = False
        '
        'txtVehicle
        '
        Me.txtVehicle.Enabled = False
        Me.txtVehicle.Location = New System.Drawing.Point(74, 50)
        Me.txtVehicle.MendatroryField = True
        Me.txtVehicle.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicle.MyLinkLable1 = Nothing
        Me.txtVehicle.MyLinkLable2 = Nothing
        Me.txtVehicle.MyReadOnly = False
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.Size = New System.Drawing.Size(133, 19)
        Me.txtVehicle.TabIndex = 3
        Me.txtVehicle.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(4, 51)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel2.TabIndex = 8
        Me.MyLabel2.Text = "Vehicle"
        '
        'lblSalesman
        '
        Me.lblSalesman.AutoSize = False
        Me.lblSalesman.BorderVisible = True
        Me.lblSalesman.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesman.Location = New System.Drawing.Point(212, 28)
        Me.lblSalesman.Name = "lblSalesman"
        Me.lblSalesman.Size = New System.Drawing.Size(180, 18)
        Me.lblSalesman.TabIndex = 10
        Me.lblSalesman.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSalesman.TextWrap = False
        '
        'txtSalesman
        '
        Me.txtSalesman.Enabled = False
        Me.txtSalesman.Location = New System.Drawing.Point(74, 27)
        Me.txtSalesman.MendatroryField = True
        Me.txtSalesman.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesman.MyLinkLable1 = Nothing
        Me.txtSalesman.MyLinkLable2 = Nothing
        Me.txtSalesman.MyReadOnly = False
        Me.txtSalesman.Name = "txtSalesman"
        Me.txtSalesman.Size = New System.Drawing.Size(133, 19)
        Me.txtSalesman.TabIndex = 1
        Me.txtSalesman.Value = ""
        '
        'lblSalesmenCode
        '
        Me.lblSalesmenCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesmenCode.Location = New System.Drawing.Point(4, 27)
        Me.lblSalesmenCode.Name = "lblSalesmenCode"
        Me.lblSalesmenCode.Size = New System.Drawing.Size(57, 16)
        Me.lblSalesmenCode.TabIndex = 9
        Me.lblSalesmenCode.Text = "Salesman"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 73)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.gv1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.gv2)
        Me.SplitContainer1.Size = New System.Drawing.Size(725, 314)
        Me.SplitContainer1.SplitterDistance = 297
        Me.SplitContainer1.TabIndex = 1
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.EnableGrouping = False
        Me.gv1.MasterTemplate.EnableSorting = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.Size = New System.Drawing.Size(297, 314)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'gv2
        '
        Me.gv2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv2.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gv2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv2.Location = New System.Drawing.Point(0, 0)
        '
        'gv2
        '
        Me.gv2.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv2.MasterTemplate.AllowAddNewRow = False
        Me.gv2.MasterTemplate.EnableGrouping = False
        Me.gv2.MasterTemplate.EnableSorting = False
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.Size = New System.Drawing.Size(424, 314)
        Me.gv2.TabIndex = 0
        Me.gv2.TabStop = False
        '
        'FrmTransfer3rdDoc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(725, 387)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FrmTransfer3rdDoc"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Create Transfer"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCreateTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbltransferdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesmenCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtSalesman As common.UserControls.txtFinder
    Friend WithEvents lblSalesmenCode As common.Controls.MyLabel
    Friend WithEvents lblVehicle As common.Controls.MyLabel
    Friend WithEvents txtVehicle As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblSalesman As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents rdlbltransferdate As common.Controls.MyLabel
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnCreateTransfer As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
End Class

