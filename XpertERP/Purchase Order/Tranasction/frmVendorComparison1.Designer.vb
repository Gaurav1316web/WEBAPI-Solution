<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVendorComparison1
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
        Me.UsLock1 = New common.usLock()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtReqDate = New common.Controls.MyTextBox()
        Me.txtRFQDate = New common.Controls.MyTextBox()
        Me.txtReqNo = New common.Controls.MyTextBox()
        Me.lblReq = New common.Controls.MyLabel()
        Me.txtRFQNo = New common.UserControls.txtFinder()
        Me.lblRFQno = New common.Controls.MyLabel()
        Me.RadLabel20 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReqDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRFQDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReqNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReq, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRFQno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtReqDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRFQDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtReqNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblReq)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRFQNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel20)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRFQno)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(705, 472)
        Me.SplitContainer1.SplitterDistance = 439
        Me.SplitContainer1.TabIndex = 1
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(588, 3)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(114, 22)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 45
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(232, 5)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 19)
        Me.btnAddNew.TabIndex = 1
        '
        'txtReqDate
        '
        Me.txtReqDate.CalculationExpression = Nothing
        Me.txtReqDate.FieldCode = Nothing
        Me.txtReqDate.FieldDesc = Nothing
        Me.txtReqDate.FieldMaxLength = 0
        Me.txtReqDate.FieldName = Nothing
        Me.txtReqDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReqDate.isCalculatedField = False
        Me.txtReqDate.IsSourceFromTable = False
        Me.txtReqDate.IsSourceFromValueList = False
        Me.txtReqDate.IsUnique = False
        Me.txtReqDate.Location = New System.Drawing.Point(289, 26)
        Me.txtReqDate.MaxLength = 50
        Me.txtReqDate.MendatroryField = False
        Me.txtReqDate.MyLinkLable1 = Nothing
        Me.txtReqDate.MyLinkLable2 = Nothing
        Me.txtReqDate.Name = "txtReqDate"
        Me.txtReqDate.ReadOnly = True
        Me.txtReqDate.ReferenceFieldDesc = Nothing
        Me.txtReqDate.ReferenceFieldName = Nothing
        Me.txtReqDate.ReferenceTableName = Nothing
        Me.txtReqDate.Size = New System.Drawing.Size(143, 18)
        Me.txtReqDate.TabIndex = 4
        '
        'txtRFQDate
        '
        Me.txtRFQDate.CalculationExpression = Nothing
        Me.txtRFQDate.FieldCode = Nothing
        Me.txtRFQDate.FieldDesc = Nothing
        Me.txtRFQDate.FieldMaxLength = 0
        Me.txtRFQDate.FieldName = Nothing
        Me.txtRFQDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRFQDate.isCalculatedField = False
        Me.txtRFQDate.IsSourceFromTable = False
        Me.txtRFQDate.IsSourceFromValueList = False
        Me.txtRFQDate.IsUnique = False
        Me.txtRFQDate.Location = New System.Drawing.Point(289, 5)
        Me.txtRFQDate.MaxLength = 50
        Me.txtRFQDate.MendatroryField = False
        Me.txtRFQDate.MyLinkLable1 = Nothing
        Me.txtRFQDate.MyLinkLable2 = Nothing
        Me.txtRFQDate.Name = "txtRFQDate"
        Me.txtRFQDate.ReadOnly = True
        Me.txtRFQDate.ReferenceFieldDesc = Nothing
        Me.txtRFQDate.ReferenceFieldName = Nothing
        Me.txtRFQDate.ReferenceTableName = Nothing
        Me.txtRFQDate.Size = New System.Drawing.Size(143, 18)
        Me.txtRFQDate.TabIndex = 2
        '
        'txtReqNo
        '
        Me.txtReqNo.CalculationExpression = Nothing
        Me.txtReqNo.FieldCode = Nothing
        Me.txtReqNo.FieldDesc = Nothing
        Me.txtReqNo.FieldMaxLength = 0
        Me.txtReqNo.FieldName = Nothing
        Me.txtReqNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReqNo.isCalculatedField = False
        Me.txtReqNo.IsSourceFromTable = False
        Me.txtReqNo.IsSourceFromValueList = False
        Me.txtReqNo.IsUnique = False
        Me.txtReqNo.Location = New System.Drawing.Point(88, 26)
        Me.txtReqNo.MaxLength = 50
        Me.txtReqNo.MendatroryField = False
        Me.txtReqNo.MyLinkLable1 = Nothing
        Me.txtReqNo.MyLinkLable2 = Nothing
        Me.txtReqNo.Name = "txtReqNo"
        Me.txtReqNo.ReadOnly = True
        Me.txtReqNo.ReferenceFieldDesc = Nothing
        Me.txtReqNo.ReferenceFieldName = Nothing
        Me.txtReqNo.ReferenceTableName = Nothing
        Me.txtReqNo.Size = New System.Drawing.Size(163, 18)
        Me.txtReqNo.TabIndex = 3
        '
        'lblReq
        '
        Me.lblReq.FieldName = Nothing
        Me.lblReq.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReq.Location = New System.Drawing.Point(1, 26)
        Me.lblReq.Name = "lblReq"
        Me.lblReq.Size = New System.Drawing.Size(80, 16)
        Me.lblReq.TabIndex = 19
        Me.lblReq.Text = "Requisition No"
        '
        'txtRFQNo
        '
        Me.txtRFQNo.CalculationExpression = Nothing
        Me.txtRFQNo.FieldCode = Nothing
        Me.txtRFQNo.FieldDesc = Nothing
        Me.txtRFQNo.FieldMaxLength = 0
        Me.txtRFQNo.FieldName = Nothing
        Me.txtRFQNo.isCalculatedField = False
        Me.txtRFQNo.IsSourceFromTable = False
        Me.txtRFQNo.IsSourceFromValueList = False
        Me.txtRFQNo.IsUnique = False
        Me.txtRFQNo.Location = New System.Drawing.Point(88, 5)
        Me.txtRFQNo.MendatroryField = True
        Me.txtRFQNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRFQNo.MyLinkLable1 = Me.lblRFQno
        Me.txtRFQNo.MyLinkLable2 = Nothing
        Me.txtRFQNo.MyReadOnly = False
        Me.txtRFQNo.MyShowMasterFormButton = False
        Me.txtRFQNo.Name = "txtRFQNo"
        Me.txtRFQNo.ReferenceFieldDesc = Nothing
        Me.txtRFQNo.ReferenceFieldName = Nothing
        Me.txtRFQNo.ReferenceTableName = Nothing
        Me.txtRFQNo.Size = New System.Drawing.Size(143, 19)
        Me.txtRFQNo.TabIndex = 0
        Me.txtRFQNo.Value = ""
        '
        'lblRFQno
        '
        Me.lblRFQno.FieldName = Nothing
        Me.lblRFQno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRFQno.Location = New System.Drawing.Point(1, 6)
        Me.lblRFQno.Name = "lblRFQno"
        Me.lblRFQno.Size = New System.Drawing.Size(48, 16)
        Me.lblRFQno.TabIndex = 17
        Me.lblRFQno.Text = "RFQ No"
        '
        'RadLabel20
        '
        Me.RadLabel20.FieldName = Nothing
        Me.RadLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel20.Location = New System.Drawing.Point(250, 28)
        Me.RadLabel20.Name = "RadLabel20"
        Me.RadLabel20.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel20.TabIndex = 23
        Me.RadLabel20.Text = " Date"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(255, 6)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 15
        Me.RadLabel4.Text = "Date"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 48)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(697, 388)
        Me.RadGroupBox2.TabIndex = 5
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(677, 358)
        Me.gv1.TabIndex = 0
        Me.gv1.Text = "RadGridView1"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(116, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Send for Approval"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(632, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(125, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(86, 22)
        Me.btnPrint.TabIndex = 2
        Me.btnPrint.Text = "Print"
        '
        'FrmVendorComparison1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(705, 472)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmVendorComparison1"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Vendor Quotation Comparison"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReqDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRFQDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReqNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReq, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRFQno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblReq As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents txtRFQNo As common.UserControls.txtFinder
    Friend WithEvents lblRFQno As common.Controls.MyLabel
    Friend WithEvents RadLabel20 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtReqNo As common.Controls.MyTextBox
    Friend WithEvents txtReqDate As common.Controls.MyTextBox
    Friend WithEvents txtRFQDate As common.Controls.MyTextBox
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
End Class

