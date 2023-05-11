<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSecondaryTranporterDispatch
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.btnOK = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblVendorCode = New common.Controls.MyLabel()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblFromDate = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblAmt = New common.Controls.MyLabel()
        Me.RadTextBoxControl1 = New Telerik.WinControls.UI.RadTextBoxControl()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblIDeductionCode = New common.Controls.MyLabel()
        Me.lblDeductionName = New common.Controls.MyLabel()
        Me.RadLabel28 = New common.Controls.MyLabel()
        Me.gv1 = New Telerik.WinControls.UI.RadGridView()
        Me.lblLocationSegmentCode = New common.Controls.MyLabel()
        Me.lblLocationSegmentName = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.Panel2.SuspendLayout()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.lblVendorCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBoxControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIDeductionCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDeductionName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationSegmentCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationSegmentName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.RadButton2)
        Me.Panel2.Controls.Add(Me.btnOK)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 484)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(595, 26)
        Me.Panel2.TabIndex = 1
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.RadButton2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton2.Location = New System.Drawing.Point(300, 2)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(97, 22)
        Me.RadButton2.TabIndex = 1
        Me.RadButton2.Text = "Esc : Cancel"
        '
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Location = New System.Drawing.Point(197, 2)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(97, 22)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "F5 : OK"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(595, 20)
        Me.RadMenu1.TabIndex = 3
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Save Layout"
        Me.RadMenuItem1.AccessibleName = "Save Layout"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem2.AccessibleName = "Delete Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Delete Layout"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblLocationSegmentCode)
        Me.Panel1.Controls.Add(Me.lblLocationSegmentName)
        Me.Panel1.Controls.Add(Me.MyLabel7)
        Me.Panel1.Controls.Add(Me.lblVendorCode)
        Me.Panel1.Controls.Add(Me.lblVendorName)
        Me.Panel1.Controls.Add(Me.MyLabel6)
        Me.Panel1.Controls.Add(Me.lblToDate)
        Me.Panel1.Controls.Add(Me.MyLabel5)
        Me.Panel1.Controls.Add(Me.lblFromDate)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.lblAmt)
        Me.Panel1.Controls.Add(Me.RadTextBoxControl1)
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Controls.Add(Me.lblIDeductionCode)
        Me.Panel1.Controls.Add(Me.lblDeductionName)
        Me.Panel1.Controls.Add(Me.RadLabel28)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(595, 102)
        Me.Panel1.TabIndex = 0
        '
        'lblVendorCode
        '
        Me.lblVendorCode.AutoSize = False
        Me.lblVendorCode.BorderVisible = True
        Me.lblVendorCode.FieldName = Nothing
        Me.lblVendorCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorCode.Location = New System.Drawing.Point(64, 54)
        Me.lblVendorCode.Name = "lblVendorCode"
        Me.lblVendorCode.Size = New System.Drawing.Size(126, 19)
        Me.lblVendorCode.TabIndex = 13
        Me.lblVendorCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVendorCode.TextWrap = False
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorName.Location = New System.Drawing.Point(192, 54)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(257, 19)
        Me.lblVendorName.TabIndex = 12
        Me.lblVendorName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVendorName.TextWrap = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(3, 55)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel6.TabIndex = 14
        Me.MyLabel6.Text = "Vendor"
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = False
        Me.lblToDate.BorderVisible = True
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(241, 12)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(126, 19)
        Me.lblToDate.TabIndex = 10
        Me.lblToDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblToDate.TextWrap = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(192, 13)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel5.TabIndex = 11
        Me.MyLabel5.Text = "To Date"
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = False
        Me.lblFromDate.BorderVisible = True
        Me.lblFromDate.FieldName = Nothing
        Me.lblFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.Location = New System.Drawing.Point(64, 12)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(126, 19)
        Me.lblFromDate.TabIndex = 8
        Me.lblFromDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFromDate.TextWrap = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(3, 13)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel2.TabIndex = 9
        Me.MyLabel2.Text = "From Date"
        '
        'lblAmt
        '
        Me.lblAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAmt.AutoSize = False
        Me.lblAmt.BorderVisible = True
        Me.lblAmt.FieldName = Nothing
        Me.lblAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmt.Location = New System.Drawing.Point(519, 12)
        Me.lblAmt.Name = "lblAmt"
        Me.lblAmt.Size = New System.Drawing.Size(69, 19)
        Me.lblAmt.TabIndex = 1
        Me.lblAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAmt.TextWrap = False
        '
        'RadTextBoxControl1
        '
        Me.RadTextBoxControl1.Location = New System.Drawing.Point(554, 12)
        Me.RadTextBoxControl1.Name = "RadTextBoxControl1"
        Me.RadTextBoxControl1.Size = New System.Drawing.Size(10, 16)
        Me.RadTextBoxControl1.TabIndex = 7
        Me.RadTextBoxControl1.Text = "RadTextBoxControl1"
        '
        'MyLabel3
        '
        Me.MyLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(461, 13)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel3.TabIndex = 2
        Me.MyLabel3.Text = "Amount"
        '
        'lblIDeductionCode
        '
        Me.lblIDeductionCode.AutoSize = False
        Me.lblIDeductionCode.BorderVisible = True
        Me.lblIDeductionCode.FieldName = Nothing
        Me.lblIDeductionCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIDeductionCode.Location = New System.Drawing.Point(64, 33)
        Me.lblIDeductionCode.Name = "lblIDeductionCode"
        Me.lblIDeductionCode.Size = New System.Drawing.Size(126, 19)
        Me.lblIDeductionCode.TabIndex = 4
        Me.lblIDeductionCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblIDeductionCode.TextWrap = False
        '
        'lblDeductionName
        '
        Me.lblDeductionName.AutoSize = False
        Me.lblDeductionName.BorderVisible = True
        Me.lblDeductionName.FieldName = Nothing
        Me.lblDeductionName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeductionName.Location = New System.Drawing.Point(192, 33)
        Me.lblDeductionName.Name = "lblDeductionName"
        Me.lblDeductionName.Size = New System.Drawing.Size(257, 19)
        Me.lblDeductionName.TabIndex = 3
        Me.lblDeductionName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDeductionName.TextWrap = False
        '
        'RadLabel28
        '
        Me.RadLabel28.FieldName = Nothing
        Me.RadLabel28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel28.Location = New System.Drawing.Point(3, 34)
        Me.RadLabel28.Name = "RadLabel28"
        Me.RadLabel28.Size = New System.Drawing.Size(57, 16)
        Me.RadLabel28.TabIndex = 5
        Me.RadLabel28.Text = "Deduction"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 122)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.EnableAlternatingRowColor = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.Size = New System.Drawing.Size(595, 362)
        Me.gv1.TabIndex = 2
        Me.gv1.Text = "RadGridView1"
        '
        'lblLocationSegmentCode
        '
        Me.lblLocationSegmentCode.AutoSize = False
        Me.lblLocationSegmentCode.BorderVisible = True
        Me.lblLocationSegmentCode.FieldName = Nothing
        Me.lblLocationSegmentCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationSegmentCode.Location = New System.Drawing.Point(64, 75)
        Me.lblLocationSegmentCode.Name = "lblLocationSegmentCode"
        Me.lblLocationSegmentCode.Size = New System.Drawing.Size(126, 19)
        Me.lblLocationSegmentCode.TabIndex = 16
        Me.lblLocationSegmentCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocationSegmentCode.TextWrap = False
        '
        'lblLocationSegmentName
        '
        Me.lblLocationSegmentName.AutoSize = False
        Me.lblLocationSegmentName.BorderVisible = True
        Me.lblLocationSegmentName.FieldName = Nothing
        Me.lblLocationSegmentName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationSegmentName.Location = New System.Drawing.Point(192, 75)
        Me.lblLocationSegmentName.Name = "lblLocationSegmentName"
        Me.lblLocationSegmentName.Size = New System.Drawing.Size(257, 19)
        Me.lblLocationSegmentName.TabIndex = 15
        Me.lblLocationSegmentName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocationSegmentName.TextWrap = False
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(3, 76)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel7.TabIndex = 17
        Me.MyLabel7.Text = "Location"
        '
        'frmSecondaryTranporterDispatch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(595, 510)
        Me.ControlBox = False
        Me.Controls.Add(Me.gv1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.Panel2)
        Me.KeyPreview = True
        Me.Name = "frmSecondaryTranporterDispatch"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Secondary Transporter Dispatch Deduction"
        Me.Panel2.ResumeLayout(False)
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.lblVendorCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBoxControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIDeductionCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDeductionName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationSegmentCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationSegmentName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnOK As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblIDeductionCode As common.Controls.MyLabel
    Friend WithEvents lblDeductionName As common.Controls.MyLabel
    Friend WithEvents RadLabel28 As common.Controls.MyLabel
    Friend WithEvents gv1 As Telerik.WinControls.UI.RadGridView
    Friend WithEvents RadTextBoxControl1 As Telerik.WinControls.UI.RadTextBoxControl
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblFromDate As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblVendorCode As common.Controls.MyLabel
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents lblLocationSegmentCode As common.Controls.MyLabel
    Friend WithEvents lblLocationSegmentName As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
End Class

