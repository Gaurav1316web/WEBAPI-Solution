Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVisi_Install_Pullout
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
        Me.components = New System.ComponentModel.Container()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.fndCustomer = New common.UserControls.txtNavigator()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.chkPullOut = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkInstall = New Telerik.WinControls.UI.RadRadioButton()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblRoute = New common.Controls.MyLabel()
        Me.lblCustomerName = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.dgvVisi = New common.UserControls.MyRadGridView()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.chkPullOut, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInstall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvVisi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvVisi.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(777, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(89, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.Visible = False
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
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(6, 5)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(76, 16)
        Me.RadLabel1.TabIndex = 85
        Me.RadLabel1.Text = "Customer No."
        '
        'fndCustomer
        '
        Me.fndCustomer.FieldName = Nothing
        Me.fndCustomer.Location = New System.Drawing.Point(107, 3)
        Me.fndCustomer.MendatroryField = True
        Me.fndCustomer.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCustomer.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCustomer.MyLinkLable1 = Nothing
        Me.fndCustomer.MyLinkLable2 = Nothing
        Me.fndCustomer.MyMaxLength = 30
        Me.fndCustomer.MyReadOnly = False
        Me.fndCustomer.Name = "fndCustomer"
        Me.fndCustomer.Size = New System.Drawing.Size(218, 20)
        Me.fndCustomer.TabIndex = 1
        Me.fndCustomer.Value = ""
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(864, 393)
        Me.SplitContainer1.SplitterDistance = 364
        Me.SplitContainer1.TabIndex = 91
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkPullOut)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkInstall)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRoute)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCustomerName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndCustomer)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnNew)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.dgvVisi)
        Me.SplitContainer2.Size = New System.Drawing.Size(864, 364)
        Me.SplitContainer2.SplitterDistance = 69
        Me.SplitContainer2.TabIndex = 90
        '
        'chkPullOut
        '
        Me.chkPullOut.Location = New System.Drawing.Point(459, 4)
        Me.chkPullOut.Name = "chkPullOut"
        Me.chkPullOut.Size = New System.Drawing.Size(60, 18)
        Me.chkPullOut.TabIndex = 3
        Me.chkPullOut.Text = "Pull Out"
        '
        'chkInstall
        '
        Me.chkInstall.Location = New System.Drawing.Point(375, 4)
        Me.chkInstall.Name = "chkInstall"
        Me.chkInstall.Size = New System.Drawing.Size(49, 18)
        Me.chkInstall.TabIndex = 2
        Me.chkInstall.Text = "Install"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(6, 27)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel3.TabIndex = 93
        Me.MyLabel3.Text = "Name"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(6, 48)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel2.TabIndex = 92
        Me.MyLabel2.Text = "Route"
        '
        'lblRoute
        '
        Me.lblRoute.AutoSize = False
        Me.lblRoute.BorderVisible = True
        Me.lblRoute.FieldName = Nothing
        Me.lblRoute.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoute.Location = New System.Drawing.Point(107, 47)
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.Size = New System.Drawing.Size(505, 20)
        Me.lblRoute.TabIndex = 90
        Me.lblRoute.TextWrap = False
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = False
        Me.lblCustomerName.BorderVisible = True
        Me.lblCustomerName.FieldName = Nothing
        Me.lblCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerName.Location = New System.Drawing.Point(107, 25)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(505, 20)
        Me.lblCustomerName.TabIndex = 88
        Me.lblCustomerName.TextWrap = False
        '
        'btnNew
        '
        Me.btnNew.Image = Global.XpertERPFixedAssets.My.Resources.Resources._new
        Me.btnNew.Location = New System.Drawing.Point(331, 4)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(15, 20)
        Me.btnNew.TabIndex = 0
        '
        'dgvVisi
        '
        Me.dgvVisi.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.dgvVisi.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvVisi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvVisi.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvVisi.ForeColor = System.Drawing.Color.Black
        Me.dgvVisi.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvVisi.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.dgvVisi.MasterTemplate.AllowDeleteRow = False
        Me.dgvVisi.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.dgvVisi.MasterTemplate.ShowHeaderCellButtons = True
        Me.dgvVisi.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.dgvVisi.MyStopExport = False
        Me.dgvVisi.Name = "dgvVisi"
        Me.dgvVisi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvVisi.ShowGroupPanel = False
        Me.dgvVisi.ShowHeaderCellButtons = True
        Me.dgvVisi.Size = New System.Drawing.Size(864, 291)
        Me.dgvVisi.TabIndex = 1
        '
        'FrmVisi_Install_Pullout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(864, 393)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmVisi_Install_Pullout"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Market Equipment"
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.chkPullOut, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInstall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvVisi.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvVisi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents fndCustomer As common.UserControls.txtNavigator
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgvVisi As common.UserControls.MyRadGridView
    Friend WithEvents lblCustomerName As common.Controls.MyLabel
    Friend WithEvents lblRoute As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents chkInstall As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents chkPullOut As Telerik.WinControls.UI.RadRadioButton
End Class

