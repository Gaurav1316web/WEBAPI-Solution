Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAssetSegment
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
        Me.gbSegmentCode = New Telerik.WinControls.UI.RadGroupBox
        Me.txtIdSegment = New common.Controls.MyTextBox
        Me.lblIdSegment = New common.Controls.MyLabel
        Me.lblNoOfSegment = New common.Controls.MyLabel
        Me.cmbSegmentSeperator = New common.Controls.MyComboBox
        Me.lblsegmentSeperator = New common.Controls.MyLabel
        Me.txtNoOfSegment = New common.MyNumBox
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.dgvAssetSegment = New common.UserControls.MyRadGridView
        CType(Me.gbSegmentCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbSegmentCode.SuspendLayout()
        CType(Me.txtIdSegment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIdSegment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNoOfSegment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSegmentSeperator, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblsegmentSeperator, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoOfSegment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.dgvAssetSegment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAssetSegment.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gbSegmentCode
        '
        Me.gbSegmentCode.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbSegmentCode.Controls.Add(Me.txtIdSegment)
        Me.gbSegmentCode.Controls.Add(Me.lblIdSegment)
        Me.gbSegmentCode.Controls.Add(Me.lblNoOfSegment)
        Me.gbSegmentCode.Controls.Add(Me.cmbSegmentSeperator)
        Me.gbSegmentCode.Controls.Add(Me.lblsegmentSeperator)
        Me.gbSegmentCode.Controls.Add(Me.txtNoOfSegment)
        Me.gbSegmentCode.HeaderText = "Segment Code"
        Me.gbSegmentCode.Location = New System.Drawing.Point(6, 4)
        Me.gbSegmentCode.Name = "gbSegmentCode"
        Me.gbSegmentCode.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbSegmentCode.Size = New System.Drawing.Size(334, 94)
        Me.gbSegmentCode.TabIndex = 0
        Me.gbSegmentCode.Text = "Segment Code"
        '
        'txtIdSegment
        '
        Me.txtIdSegment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdSegment.Location = New System.Drawing.Point(125, 43)
        Me.txtIdSegment.MendatroryField = False
        Me.txtIdSegment.MyLinkLable1 = Me.lblIdSegment
        Me.txtIdSegment.MyLinkLable2 = Nothing
        Me.txtIdSegment.Name = "txtIdSegment"
        Me.txtIdSegment.Size = New System.Drawing.Size(201, 18)
        Me.txtIdSegment.TabIndex = 1
        '
        'lblIdSegment
        '
        Me.lblIdSegment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIdSegment.Location = New System.Drawing.Point(13, 44)
        Me.lblIdSegment.Name = "lblIdSegment"
        Me.lblIdSegment.Size = New System.Drawing.Size(64, 16)
        Me.lblIdSegment.TabIndex = 35
        Me.lblIdSegment.Text = "Id Segment"
        '
        'lblNoOfSegment
        '
        Me.lblNoOfSegment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoOfSegment.Location = New System.Drawing.Point(13, 20)
        Me.lblNoOfSegment.Name = "lblNoOfSegment"
        Me.lblNoOfSegment.Size = New System.Drawing.Size(84, 16)
        Me.lblNoOfSegment.TabIndex = 36
        Me.lblNoOfSegment.Text = "No Of Segment"
        '
        'cmbSegmentSeperator
        '
        Me.cmbSegmentSeperator.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbSegmentSeperator.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "Payment"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Advance"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "Misc Payment"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "Apply Document"
        RadListDataItem4.TextWrap = True
        RadListDataItem5.Text = "On Account"
        RadListDataItem5.TextWrap = True
        RadListDataItem6.Text = "Receipt"
        RadListDataItem6.TextWrap = True
        Me.cmbSegmentSeperator.Items.Add(RadListDataItem1)
        Me.cmbSegmentSeperator.Items.Add(RadListDataItem2)
        Me.cmbSegmentSeperator.Items.Add(RadListDataItem3)
        Me.cmbSegmentSeperator.Items.Add(RadListDataItem4)
        Me.cmbSegmentSeperator.Items.Add(RadListDataItem5)
        Me.cmbSegmentSeperator.Items.Add(RadListDataItem6)
        Me.cmbSegmentSeperator.Location = New System.Drawing.Point(125, 66)
        Me.cmbSegmentSeperator.MendatroryField = False
        Me.cmbSegmentSeperator.MyLinkLable1 = Me.lblsegmentSeperator
        Me.cmbSegmentSeperator.MyLinkLable2 = Nothing
        Me.cmbSegmentSeperator.Name = "cmbSegmentSeperator"
        Me.cmbSegmentSeperator.Size = New System.Drawing.Size(159, 18)
        Me.cmbSegmentSeperator.TabIndex = 2
        '
        'lblsegmentSeperator
        '
        Me.lblsegmentSeperator.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsegmentSeperator.Location = New System.Drawing.Point(13, 66)
        Me.lblsegmentSeperator.Name = "lblsegmentSeperator"
        Me.lblsegmentSeperator.Size = New System.Drawing.Size(104, 16)
        Me.lblsegmentSeperator.TabIndex = 34
        Me.lblsegmentSeperator.Text = "Segment Seperator"
        '
        'txtNoOfSegment
        '
        Me.txtNoOfSegment.BackColor = System.Drawing.Color.White
        Me.txtNoOfSegment.DecimalPlaces = 2
        Me.txtNoOfSegment.Location = New System.Drawing.Point(125, 19)
        Me.txtNoOfSegment.MendatroryField = False
        Me.txtNoOfSegment.MyLinkLable1 = Me.lblNoOfSegment
        Me.txtNoOfSegment.MyLinkLable2 = Nothing
        Me.txtNoOfSegment.Name = "txtNoOfSegment"
        Me.txtNoOfSegment.Size = New System.Drawing.Size(201, 20)
        Me.txtNoOfSegment.TabIndex = 0
        Me.txtNoOfSegment.Text = "0"
        Me.txtNoOfSegment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNoOfSegment.Value = 0
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(6, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(602, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(673, 363)
        Me.SplitContainer1.SplitterDistance = 334
        Me.SplitContainer1.TabIndex = 1
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.gbSegmentCode)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.dgvAssetSegment)
        Me.SplitContainer2.Size = New System.Drawing.Size(673, 334)
        Me.SplitContainer2.SplitterDistance = 103
        Me.SplitContainer2.TabIndex = 1
        '
        'dgvAssetSegment
        '
        Me.dgvAssetSegment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAssetSegment.Location = New System.Drawing.Point(0, 0)
        Me.dgvAssetSegment.Name = "dgvAssetSegment"
        Me.dgvAssetSegment.Size = New System.Drawing.Size(673, 227)
        Me.dgvAssetSegment.TabIndex = 0
        Me.dgvAssetSegment.Text = "RadGridView1"
        '
        'FrmAssetSegment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(673, 363)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmAssetSegment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Asset Segment"
        CType(Me.gbSegmentCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbSegmentCode.ResumeLayout(False)
        Me.gbSegmentCode.PerformLayout()
        CType(Me.txtIdSegment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIdSegment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNoOfSegment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSegmentSeperator, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblsegmentSeperator, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoOfSegment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.dgvAssetSegment.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAssetSegment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents gbSegmentCode As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtNoOfSegment As common.MyNumBox
    Friend WithEvents lblIdSegment As common.Controls.MyLabel
    Friend WithEvents lblNoOfSegment As common.Controls.MyLabel
    Friend WithEvents cmbSegmentSeperator As common.Controls.MyComboBox
    Friend WithEvents lblsegmentSeperator As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgvAssetSegment As common.UserControls.MyRadGridView
    Friend WithEvents txtIdSegment As common.Controls.MyTextBox
End Class

