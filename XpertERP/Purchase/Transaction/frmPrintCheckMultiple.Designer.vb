<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintCheckMultiple
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.lblCheckDescription = New common.Controls.MyLabel
        Me.fndBankCode = New common.UserControls.txtFinder
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.ddlPaymentType = New common.Controls.MyComboBox
        Me.dtpToDate = New common.Controls.MyDateTimePicker
        Me.lblpaymentdate = New common.Controls.MyLabel
        Me.MyLabel8 = New common.Controls.MyLabel
        Me.dtpPrintDate = New common.Controls.MyDateTimePicker
        Me.dtpFromDate = New common.Controls.MyDateTimePicker
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.MyLabel7 = New common.Controls.MyLabel
        Me.lblCheckCode1 = New common.Controls.MyLabel
        Me.lblDocType = New common.Controls.MyLabel
        Me.fndCheckCode = New common.UserControls.txtFinder
        Me.gvDocs = New common.UserControls.MyRadGridView
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCheckDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlPaymentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpPrintDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCheckCode1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDocs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDocs.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(770, 513)
        Me.SplitContainer1.SplitterDistance = 481
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCheckDescription)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndBankCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadButton1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ddlPaymentType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpToDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblpaymentdate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpPrintDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpFromDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCheckCode1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDocType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndCheckCode)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvDocs)
        Me.SplitContainer2.Size = New System.Drawing.Size(770, 481)
        Me.SplitContainer2.SplitterDistance = 113
        Me.SplitContainer2.TabIndex = 614
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(21, 76)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(98, 16)
        Me.MyLabel1.TabIndex = 617
        Me.MyLabel1.Text = "Check Description"
        '
        'lblCheckDescription
        '
        Me.lblCheckDescription.AutoSize = False
        Me.lblCheckDescription.BorderVisible = True
        Me.lblCheckDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCheckDescription.Location = New System.Drawing.Point(130, 76)
        Me.lblCheckDescription.Name = "lblCheckDescription"
        Me.lblCheckDescription.Size = New System.Drawing.Size(416, 18)
        Me.lblCheckDescription.TabIndex = 616
        Me.lblCheckDescription.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCheckDescription.TextWrap = False
        '
        'fndBankCode
        '
        Me.fndBankCode.Location = New System.Drawing.Point(130, 29)
        Me.fndBankCode.MendatroryField = True
        Me.fndBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBankCode.MyLinkLable1 = Nothing
        Me.fndBankCode.MyLinkLable2 = Nothing
        Me.fndBankCode.MyReadOnly = False
        Me.fndBankCode.MyShowMasterFormButton = False
        Me.fndBankCode.Name = "fndBankCode"
        Me.fndBankCode.Size = New System.Drawing.Size(180, 19)
        Me.fndBankCode.TabIndex = 615
        Me.fndBankCode.Value = ""
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(552, 76)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(48, 18)
        Me.RadButton1.TabIndex = 614
        Me.RadButton1.Text = ">>"
        '
        'ddlPaymentType
        '
        Me.ddlPaymentType.AllowShowFocusCues = False
        Me.ddlPaymentType.AutoCompleteDisplayMember = Nothing
        Me.ddlPaymentType.AutoCompleteValueMember = Nothing
        Me.ddlPaymentType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlPaymentType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "Payment"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Advance"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "Misc Payment"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Enabled = False
        RadListDataItem4.Text = "Apply Document"
        RadListDataItem4.TextWrap = True
        RadListDataItem5.Text = "On Account"
        RadListDataItem5.TextWrap = True
        RadListDataItem6.Text = "Receipt"
        RadListDataItem6.TextWrap = True
        Me.ddlPaymentType.Items.Add(RadListDataItem1)
        Me.ddlPaymentType.Items.Add(RadListDataItem2)
        Me.ddlPaymentType.Items.Add(RadListDataItem3)
        Me.ddlPaymentType.Items.Add(RadListDataItem4)
        Me.ddlPaymentType.Items.Add(RadListDataItem5)
        Me.ddlPaymentType.Items.Add(RadListDataItem6)
        Me.ddlPaymentType.Location = New System.Drawing.Point(130, 9)
        Me.ddlPaymentType.MendatroryField = False
        Me.ddlPaymentType.MyLinkLable1 = Nothing
        Me.ddlPaymentType.MyLinkLable2 = Nothing
        Me.ddlPaymentType.Name = "ddlPaymentType"
        Me.ddlPaymentType.Size = New System.Drawing.Size(181, 18)
        Me.ddlPaymentType.TabIndex = 609
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(414, 51)
        Me.dtpToDate.MendatroryField = False
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Nothing
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(132, 20)
        Me.dtpToDate.TabIndex = 613
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "10/06/2011"
        Me.dtpToDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblpaymentdate
        '
        Me.lblpaymentdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentdate.Location = New System.Drawing.Point(341, 8)
        Me.lblpaymentdate.Name = "lblpaymentdate"
        Me.lblpaymentdate.Size = New System.Drawing.Size(57, 16)
        Me.lblpaymentdate.TabIndex = 14
        Me.lblpaymentdate.Text = "Print Date"
        '
        'MyLabel8
        '
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(341, 54)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel8.TabIndex = 612
        Me.MyLabel8.Text = "To Date"
        '
        'dtpPrintDate
        '
        Me.dtpPrintDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpPrintDate.Enabled = False
        Me.dtpPrintDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPrintDate.Location = New System.Drawing.Point(414, 7)
        Me.dtpPrintDate.MendatroryField = False
        Me.dtpPrintDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPrintDate.MyLinkLable1 = Nothing
        Me.dtpPrintDate.MyLinkLable2 = Nothing
        Me.dtpPrintDate.Name = "dtpPrintDate"
        Me.dtpPrintDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPrintDate.Size = New System.Drawing.Size(132, 20)
        Me.dtpPrintDate.TabIndex = 3
        Me.dtpPrintDate.TabStop = False
        Me.dtpPrintDate.Text = "10/06/2011 11:51 AM"
        Me.dtpPrintDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(414, 29)
        Me.dtpFromDate.MendatroryField = False
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.MyLinkLable1 = Nothing
        Me.dtpFromDate.MyLinkLable2 = Nothing
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.Size = New System.Drawing.Size(132, 20)
        Me.dtpFromDate.TabIndex = 611
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "10/06/2011"
        Me.dtpFromDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(21, 30)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel5.TabIndex = 11
        Me.MyLabel5.Text = "Bank Code"
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(341, 32)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel7.TabIndex = 610
        Me.MyLabel7.Text = "From Date"
        '
        'lblCheckCode1
        '
        Me.lblCheckCode1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCheckCode1.Location = New System.Drawing.Point(21, 54)
        Me.lblCheckCode1.Name = "lblCheckCode1"
        Me.lblCheckCode1.Size = New System.Drawing.Size(69, 16)
        Me.lblCheckCode1.TabIndex = 10
        Me.lblCheckCode1.Text = "Check Code"
        '
        'lblDocType
        '
        Me.lblDocType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocType.Location = New System.Drawing.Point(21, 8)
        Me.lblDocType.Name = "lblDocType"
        Me.lblDocType.Size = New System.Drawing.Size(86, 16)
        Me.lblDocType.TabIndex = 608
        Me.lblDocType.Text = "Document Type"
        '
        'fndCheckCode
        '
        Me.fndCheckCode.Location = New System.Drawing.Point(130, 52)
        Me.fndCheckCode.MendatroryField = True
        Me.fndCheckCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCheckCode.MyLinkLable1 = Nothing
        Me.fndCheckCode.MyLinkLable2 = Nothing
        Me.fndCheckCode.MyReadOnly = False
        Me.fndCheckCode.MyShowMasterFormButton = False
        Me.fndCheckCode.Name = "fndCheckCode"
        Me.fndCheckCode.Size = New System.Drawing.Size(180, 19)
        Me.fndCheckCode.TabIndex = 7
        Me.fndCheckCode.Value = ""
        '
        'gvDocs
        '
        Me.gvDocs.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvDocs.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvDocs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDocs.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvDocs.ForeColor = System.Drawing.Color.Black
        Me.gvDocs.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvDocs.Location = New System.Drawing.Point(0, 0)
        '
        'gvDocs
        '
        Me.gvDocs.MasterTemplate.AllowDeleteRow = False
        Me.gvDocs.MasterTemplate.EnableFiltering = True
        Me.gvDocs.Name = "gvDocs"
        Me.gvDocs.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvDocs.ShowGroupPanel = False
        Me.gvDocs.Size = New System.Drawing.Size(770, 364)
        Me.gvDocs.TabIndex = 607
        Me.gvDocs.TabStop = False
        Me.gvDocs.Text = "RadGridView1"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(3, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(66, 18)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "Print"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(701, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 1
        Me.btnclose.Text = "Close"
        '
        'frmPrintCheckMultiple
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(770, 513)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmPrintCheckMultiple"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Check Printing"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCheckDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlPaymentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpPrintDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCheckCode1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDocs.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDocs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpPrintDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblpaymentdate As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents fndCheckCode As common.UserControls.txtFinder
    Friend WithEvents lblCheckCode1 As common.Controls.MyLabel
    Friend WithEvents gvDocs As common.UserControls.MyRadGridView
    Friend WithEvents ddlPaymentType As common.Controls.MyComboBox
    Friend WithEvents lblDocType As common.Controls.MyLabel
    Friend WithEvents dtpFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndBankCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblCheckDescription As common.Controls.MyLabel
End Class

