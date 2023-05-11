<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShipmentImportExport
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.SplitContainer1 = New Telerik.WinControls.UI.RadSplitContainer
        Me.SplitPanel1 = New Telerik.WinControls.UI.SplitPanel
        Me.RadLabel3 = New Telerik.WinControls.UI.RadLabel
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel
        Me.BtnCLose = New Telerik.WinControls.UI.RadButton
        Me.btnValidate = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.Shipment = New Telerik.WinControls.UI.RadPageView
        Me.ShipmentHeadTab = New Telerik.WinControls.UI.RadPageViewPage
        Me.BtnExportHead = New Telerik.WinControls.UI.RadButton
        Me.btnDetailImport = New Telerik.WinControls.UI.RadButton
        Me.gvHead = New common.UserControls.MyRadGridView
        Me.ShipmentDetailTab = New Telerik.WinControls.UI.RadPageViewPage
        Me.BtnExportDetail = New Telerik.WinControls.UI.RadButton
        Me.HeadImport = New Telerik.WinControls.UI.RadButton
        Me.gvDetail = New common.UserControls.MyRadGridView
        Me.RadLabel4 = New Telerik.WinControls.UI.RadLabel
        Me.RadLabel5 = New Telerik.WinControls.UI.RadLabel
        Me.RadLabel6 = New Telerik.WinControls.UI.RadLabel
        Me.Panel1.SuspendLayout()
        'CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel1.SuspendLayout()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnCLose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnValidate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Shipment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Shipment.SuspendLayout()
        Me.ShipmentHeadTab.SuspendLayout()
        CType(Me.BtnExportHead, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDetailImport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvHead, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvHead.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ShipmentDetailTab.SuspendLayout()
        CType(Me.BtnExportDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HeadImport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(778, 458)
        Me.Panel1.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Controls.Add(Me.SplitPanel1)
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        '
        '
        Me.SplitContainer1.RootElement.MinSize = New System.Drawing.Size(25, 25)
        Me.SplitContainer1.Size = New System.Drawing.Size(778, 458)
        Me.SplitContainer1.SplitterWidth = 4
        Me.SplitContainer1.TabIndex = 0
        Me.SplitContainer1.TabStop = False
        Me.SplitContainer1.Text = "SplitContainer1"
        '
        'SplitPanel1
        '
        Me.SplitPanel1.Controls.Add(Me.RadLabel6)
        Me.SplitPanel1.Controls.Add(Me.RadLabel5)
        Me.SplitPanel1.Controls.Add(Me.RadLabel4)
        Me.SplitPanel1.Controls.Add(Me.RadLabel3)
        Me.SplitPanel1.Controls.Add(Me.RadLabel2)
        Me.SplitPanel1.Controls.Add(Me.RadLabel1)
        Me.SplitPanel1.Controls.Add(Me.BtnCLose)
        Me.SplitPanel1.Controls.Add(Me.btnValidate)
        Me.SplitPanel1.Controls.Add(Me.btnSave)
        Me.SplitPanel1.Controls.Add(Me.Shipment)
        Me.SplitPanel1.Location = New System.Drawing.Point(0, 0)
        Me.SplitPanel1.Name = "SplitPanel1"
        '
        '
        '
        Me.SplitPanel1.RootElement.MinSize = New System.Drawing.Size(25, 25)
        Me.SplitPanel1.Size = New System.Drawing.Size(778, 458)
        Me.SplitPanel1.TabIndex = 0
        Me.SplitPanel1.TabStop = False
        Me.SplitPanel1.Text = "Panel1"
        '
        'RadLabel3
        '
        Me.RadLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadLabel3.Location = New System.Drawing.Point(515, 428)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(166, 18)
        Me.RadLabel3.TabIndex = 8
        Me.RadLabel3.Text = "This Color Data is Not Validated"
        '
        'RadLabel2
        '
        Me.RadLabel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadLabel2.Location = New System.Drawing.Point(349, 428)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(142, 18)
        Me.RadLabel2.TabIndex = 7
        Me.RadLabel2.Text = "This Color Data is InCorrect"
        '
        'RadLabel1
        '
        Me.RadLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadLabel1.BackColor = System.Drawing.Color.LightGreen
        Me.RadLabel1.BorderVisible = True
        Me.RadLabel1.Location = New System.Drawing.Point(170, 428)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel1.TabIndex = 6
        Me.RadLabel1.Text = "    "
        '
        'BtnCLose
        '
        Me.BtnCLose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCLose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCLose.Location = New System.Drawing.Point(692, 427)
        Me.BtnCLose.Name = "BtnCLose"
        Me.BtnCLose.Size = New System.Drawing.Size(69, 20)
        Me.BtnCLose.TabIndex = 5
        Me.BtnCLose.Text = "Close"
        '
        'btnValidate
        '
        Me.btnValidate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnValidate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnValidate.Location = New System.Drawing.Point(88, 427)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(69, 20)
        Me.btnValidate.TabIndex = 3
        Me.btnValidate.Text = "Validate"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(13, 427)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 20)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save"
        '
        'Shipment
        '
        Me.Shipment.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Shipment.Controls.Add(Me.ShipmentHeadTab)
        Me.Shipment.Controls.Add(Me.ShipmentDetailTab)
        Me.Shipment.Location = New System.Drawing.Point(3, 5)
        Me.Shipment.Name = "Shipment"
        Me.Shipment.SelectedPage = Me.ShipmentHeadTab
        Me.Shipment.Size = New System.Drawing.Size(758, 419)
        Me.Shipment.TabIndex = 0
        Me.Shipment.Text = "Shipment"
        CType(Me.Shipment.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'ShipmentHeadTab
        '
        Me.ShipmentHeadTab.Controls.Add(Me.BtnExportHead)
        Me.ShipmentHeadTab.Controls.Add(Me.btnDetailImport)
        Me.ShipmentHeadTab.Controls.Add(Me.gvHead)
        Me.ShipmentHeadTab.Location = New System.Drawing.Point(10, 37)
        Me.ShipmentHeadTab.Name = "ShipmentHeadTab"
        Me.ShipmentHeadTab.Size = New System.Drawing.Size(737, 371)
        Me.ShipmentHeadTab.Text = "Shipment Head"
        '
        'BtnExportHead
        '
        Me.BtnExportHead.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnExportHead.Location = New System.Drawing.Point(88, 1)
        Me.BtnExportHead.Name = "BtnExportHead"
        Me.BtnExportHead.Size = New System.Drawing.Size(101, 20)
        Me.BtnExportHead.TabIndex = 5
        Me.BtnExportHead.Text = "Export Data"
        '
        'btnDetailImport
        '
        Me.btnDetailImport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDetailImport.Location = New System.Drawing.Point(3, 1)
        Me.btnDetailImport.Name = "btnDetailImport"
        Me.btnDetailImport.Size = New System.Drawing.Size(69, 20)
        Me.btnDetailImport.TabIndex = 4
        Me.btnDetailImport.Text = "Import"
        '
        'gvHead
        '
        Me.gvHead.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvHead.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvHead.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvHead.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvHead.ForeColor = System.Drawing.Color.Black
        Me.gvHead.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvHead.Location = New System.Drawing.Point(0, 26)
        '
        'gvHead
        '
        Me.gvHead.MasterTemplate.AllowAddNewRow = False
        Me.gvHead.MasterTemplate.AllowDeleteRow = False
        Me.gvHead.Name = "gvHead"
        Me.gvHead.ReadOnly = True
        Me.gvHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvHead.ShowGroupPanel = False
        Me.gvHead.Size = New System.Drawing.Size(734, 343)
        Me.gvHead.TabIndex = 1
        Me.gvHead.TabStop = False
        Me.gvHead.Text = "RadGridView1"
        '
        'ShipmentDetailTab
        '
        Me.ShipmentDetailTab.Controls.Add(Me.BtnExportDetail)
        Me.ShipmentDetailTab.Controls.Add(Me.HeadImport)
        Me.ShipmentDetailTab.Controls.Add(Me.gvDetail)
        Me.ShipmentDetailTab.Location = New System.Drawing.Point(10, 37)
        Me.ShipmentDetailTab.Name = "ShipmentDetailTab"
        Me.ShipmentDetailTab.Size = New System.Drawing.Size(737, 371)
        Me.ShipmentDetailTab.Text = "Shipment Detail"
        '
        'BtnExportDetail
        '
        Me.BtnExportDetail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnExportDetail.Location = New System.Drawing.Point(93, 3)
        Me.BtnExportDetail.Name = "BtnExportDetail"
        Me.BtnExportDetail.Size = New System.Drawing.Size(101, 20)
        Me.BtnExportDetail.TabIndex = 6
        Me.BtnExportDetail.Text = "Export Data"
        '
        'HeadImport
        '
        Me.HeadImport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HeadImport.Location = New System.Drawing.Point(3, 3)
        Me.HeadImport.Name = "HeadImport"
        Me.HeadImport.Size = New System.Drawing.Size(69, 20)
        Me.HeadImport.TabIndex = 5
        Me.HeadImport.Text = "Import"
        '
        'gvDetail
        '
        Me.gvDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvDetail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvDetail.ForeColor = System.Drawing.Color.Black
        Me.gvDetail.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvDetail.Location = New System.Drawing.Point(0, 27)
        '
        '
        '
        Me.gvDetail.MasterTemplate.AllowAddNewRow = False
        Me.gvDetail.MasterTemplate.AllowDeleteRow = False
        Me.gvDetail.Name = "gvDetail"
        Me.gvDetail.ReadOnly = True
        Me.gvDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvDetail.ShowGroupPanel = False
        Me.gvDetail.Size = New System.Drawing.Size(737, 343)
        Me.gvDetail.TabIndex = 2
        Me.gvDetail.TabStop = False
        Me.gvDetail.Text = "RadGridView1"
        '
        'RadLabel4
        '
        Me.RadLabel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadLabel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadLabel4.Location = New System.Drawing.Point(189, 428)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(133, 18)
        Me.RadLabel4.TabIndex = 7
        Me.RadLabel4.Text = "This Color Data is Correct"
        '
        'RadLabel5
        '
        Me.RadLabel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadLabel5.BackColor = System.Drawing.Color.LightCoral
        Me.RadLabel5.BorderVisible = True
        Me.RadLabel5.Location = New System.Drawing.Point(326, 428)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel5.TabIndex = 8
        Me.RadLabel5.Text = "    "
        '
        'RadLabel6
        '
        Me.RadLabel6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadLabel6.BackColor = System.Drawing.Color.Snow
        Me.RadLabel6.BorderVisible = True
        Me.RadLabel6.Location = New System.Drawing.Point(496, 428)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel6.TabIndex = 9
        Me.RadLabel6.Text = "    "
        '
        'frmShipmentImportExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(778, 458)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmShipmentImportExport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmShipmentImportExport"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        'CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.SplitPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel1.ResumeLayout(False)
        Me.SplitPanel1.PerformLayout()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnCLose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnValidate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Shipment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Shipment.ResumeLayout(False)
        Me.ShipmentHeadTab.ResumeLayout(False)
        CType(Me.BtnExportHead, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDetailImport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvHead.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvHead, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ShipmentDetailTab.ResumeLayout(False)
        CType(Me.BtnExportDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HeadImport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As Telerik.WinControls.UI.RadSplitContainer
    Friend WithEvents SplitPanel1 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents Shipment As Telerik.WinControls.UI.RadPageView
    Friend WithEvents ShipmentHeadTab As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents ShipmentDetailTab As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvHead As common.UserControls.MyRadGridView
    Friend WithEvents gvDetail As common.UserControls.MyRadGridView
    Friend WithEvents btnValidate As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDetailImport As Telerik.WinControls.UI.RadButton
    Friend WithEvents HeadImport As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnCLose As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnExportHead As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnExportDetail As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel3 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel4 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel6 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel5 As Telerik.WinControls.UI.RadLabel
End Class
