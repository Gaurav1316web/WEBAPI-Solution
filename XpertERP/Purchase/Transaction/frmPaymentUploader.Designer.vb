<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPaymentUploader
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPaymentUploader))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblBankDesc = New common.Controls.MyLabel()
        Me.txtBankCode = New common.UserControls.txtFinder()
        Me.lblbankcode = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.dtpToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.dtpFromDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnUploader = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.TxtNeftUploader = New common.UserControls.txtMultiSelectFinder()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUploader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtNeftUploader)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBankDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBankCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblbankcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpFromDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnUploader)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(517, 316)
        Me.SplitContainer1.SplitterDistance = 276
        Me.SplitContainer1.TabIndex = 0
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(9, 64)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(115, 16)
        Me.MyLabel2.TabIndex = 222
        Me.MyLabel2.Text = "NEFT Uploader Code"
        '
        'lblBankDesc
        '
        Me.lblBankDesc.AutoSize = False
        Me.lblBankDesc.BorderVisible = True
        Me.lblBankDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankDesc.Location = New System.Drawing.Point(241, 40)
        Me.lblBankDesc.Name = "lblBankDesc"
        Me.lblBankDesc.Size = New System.Drawing.Size(240, 18)
        Me.lblBankDesc.TabIndex = 223
        Me.lblBankDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBankCode
        '
        Me.txtBankCode.Location = New System.Drawing.Point(74, 39)
        Me.txtBankCode.MendatroryField = True
        Me.txtBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankCode.MyLinkLable1 = Nothing
        Me.txtBankCode.MyLinkLable2 = Nothing
        Me.txtBankCode.MyReadOnly = False
        Me.txtBankCode.MyShowMasterFormButton = False
        Me.txtBankCode.Name = "txtBankCode"
        Me.txtBankCode.Size = New System.Drawing.Size(159, 19)
        Me.txtBankCode.TabIndex = 3
        Me.txtBankCode.Value = ""
        '
        'lblbankcode
        '
        Me.lblbankcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbankcode.Location = New System.Drawing.Point(9, 39)
        Me.lblbankcode.Name = "lblbankcode"
        Me.lblbankcode.Size = New System.Drawing.Size(62, 16)
        Me.lblbankcode.TabIndex = 221
        Me.lblbankcode.Text = "Bank Code"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(296, 16)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 17)
        Me.btnNew.TabIndex = 2
        Me.btnNew.Text = " "
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(216, 15)
        Me.dtpToDate.MendatroryField = True
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Nothing
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(80, 18)
        Me.dtpToDate.TabIndex = 1
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "03/05/2011"
        Me.dtpToDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(165, 15)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel1.TabIndex = 220
        Me.MyLabel1.Text = "To Date"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(74, 15)
        Me.dtpFromDate.MendatroryField = True
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.MyLinkLable1 = Nothing
        Me.dtpFromDate.MyLinkLable2 = Nothing
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.Size = New System.Drawing.Size(82, 18)
        Me.dtpFromDate.TabIndex = 0
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "03/05/2011"
        Me.dtpFromDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(9, 15)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 218
        Me.RadLabel1.Text = "From Date"
        '
        'btnUploader
        '
        Me.btnUploader.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUploader.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUploader.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnUploader.Location = New System.Drawing.Point(392, 14)
        Me.btnUploader.Name = "btnUploader"
        Me.btnUploader.Size = New System.Drawing.Size(113, 21)
        Me.btnUploader.TabIndex = 4
        Me.btnUploader.Text = "Create Uploader"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(434, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(71, 21)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "Close"
        '
        'TxtNeftUploader
        '
        Me.TxtNeftUploader.arrDispalyMember = Nothing
        Me.TxtNeftUploader.arrValueMember = Nothing
        Me.TxtNeftUploader.Location = New System.Drawing.Point(130, 64)
        Me.TxtNeftUploader.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNeftUploader.MyLinkLable1 = Nothing
        Me.TxtNeftUploader.MyLinkLable2 = Nothing
        Me.TxtNeftUploader.MyNullText = "All"
        Me.TxtNeftUploader.Name = "TxtNeftUploader"
        Me.TxtNeftUploader.Size = New System.Drawing.Size(351, 19)
        Me.TxtNeftUploader.TabIndex = 224
        '
        'FrmPaymentUploader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(517, 316)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmPaymentUploader"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Payment Uploader"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUploader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnUploader As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents dtpFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblBankDesc As common.Controls.MyLabel
    Friend WithEvents txtBankCode As common.UserControls.txtFinder
    Friend WithEvents lblbankcode As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents TxtNeftUploader As common.UserControls.txtMultiSelectFinder
End Class

