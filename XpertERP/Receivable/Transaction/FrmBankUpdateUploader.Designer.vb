<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBankUpdateUploader
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox51 = New System.Windows.Forms.GroupBox()
        Me.RadButton145 = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox44 = New System.Windows.Forms.GroupBox()
        Me.RadButton113 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton111 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton112 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton114 = New Telerik.WinControls.UI.RadButton()
        Me.GroupBox46 = New System.Windows.Forms.GroupBox()
        Me.RadButton118 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton119 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton120 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton121 = New Telerik.WinControls.UI.RadButton()
        Me.GroupBox52 = New System.Windows.Forms.GroupBox()
        Me.txtPaymentMode = New common.UserControls.txtFinder()
        Me.lblpaymentcode = New common.Controls.MyLabel()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.txtRPLocation = New common.UserControls.txtFinder()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtRPBank = New common.UserControls.txtFinder()
        Me.RadButton148 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton149 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton150 = New Telerik.WinControls.UI.RadButton()
        Me.fndQcNo = New common.UserControls.txtFinder()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        Me.GroupBox51.SuspendLayout()
        CType(Me.RadButton145, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage1.SuspendLayout()
        Me.GroupBox44.SuspendLayout()
        CType(Me.RadButton113, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton111, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton112, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton114, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox46.SuspendLayout()
        CType(Me.RadButton118, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton119, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton120, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton121, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox52.SuspendLayout()
        CType(Me.lblpaymentcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton148, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton149, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton150, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Size = New System.Drawing.Size(894, 466)
        Me.SplitContainer1.SplitterDistance = 401
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(894, 401)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GroupBox51)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(223.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(873, 355)
        Me.RadPageViewPage2.Text = "Change Bank Name through Excel Sheet"
        '
        'GroupBox51
        '
        Me.GroupBox51.Controls.Add(Me.RadButton145)
        Me.GroupBox51.Location = New System.Drawing.Point(3, 15)
        Me.GroupBox51.Name = "GroupBox51"
        Me.GroupBox51.Size = New System.Drawing.Size(284, 76)
        Me.GroupBox51.TabIndex = 77
        Me.GroupBox51.TabStop = False
        Me.GroupBox51.Text = "Change Bank of Receipt/Payment Entry"
        '
        'RadButton145
        '
        Me.RadButton145.Location = New System.Drawing.Point(48, 33)
        Me.RadButton145.Name = "RadButton145"
        Me.RadButton145.Size = New System.Drawing.Size(191, 24)
        Me.RadButton145.TabIndex = 41
        Me.RadButton145.Text = "Browse"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox44)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox46)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox52)
        Me.RadPageViewPage1.Controls.Add(Me.fndQcNo)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(214.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(873, 355)
        Me.RadPageViewPage1.Text = "Convert Doc. From Payment to Receipt"
        '
        'GroupBox44
        '
        Me.GroupBox44.Controls.Add(Me.RadButton113)
        Me.GroupBox44.Controls.Add(Me.RadButton111)
        Me.GroupBox44.Controls.Add(Me.RadButton112)
        Me.GroupBox44.Controls.Add(Me.RadButton114)
        Me.GroupBox44.Location = New System.Drawing.Point(2, 212)
        Me.GroupBox44.Name = "GroupBox44"
        Me.GroupBox44.Size = New System.Drawing.Size(336, 77)
        Me.GroupBox44.TabIndex = 80
        Me.GroupBox44.TabStop = False
        Me.GroupBox44.Text = "Receipt Entry"
        '
        'RadButton113
        '
        Me.RadButton113.Location = New System.Drawing.Point(80, 44)
        Me.RadButton113.Name = "RadButton113"
        Me.RadButton113.Size = New System.Drawing.Size(147, 23)
        Me.RadButton113.TabIndex = 52
        Me.RadButton113.Text = "Import Doc to Delete RE"
        '
        'RadButton111
        '
        Me.RadButton111.Location = New System.Drawing.Point(6, 14)
        Me.RadButton111.Name = "RadButton111"
        Me.RadButton111.Size = New System.Drawing.Size(59, 24)
        Me.RadButton111.TabIndex = 43
        Me.RadButton111.Text = "Reset All"
        '
        'RadButton112
        '
        Me.RadButton112.Location = New System.Drawing.Point(69, 14)
        Me.RadButton112.Name = "RadButton112"
        Me.RadButton112.Size = New System.Drawing.Size(92, 24)
        Me.RadButton112.TabIndex = 41
        Me.RadButton112.Text = "Pick Receipt No"
        '
        'RadButton114
        '
        Me.RadButton114.Location = New System.Drawing.Point(165, 14)
        Me.RadButton114.Name = "RadButton114"
        Me.RadButton114.Size = New System.Drawing.Size(163, 24)
        Me.RadButton114.TabIndex = 13
        Me.RadButton114.Text = "Delete Selected Receipt Entry"
        '
        'GroupBox46
        '
        Me.GroupBox46.Controls.Add(Me.RadButton118)
        Me.GroupBox46.Controls.Add(Me.RadButton119)
        Me.GroupBox46.Controls.Add(Me.RadButton120)
        Me.GroupBox46.Controls.Add(Me.RadButton121)
        Me.GroupBox46.Location = New System.Drawing.Point(0, 120)
        Me.GroupBox46.Name = "GroupBox46"
        Me.GroupBox46.Size = New System.Drawing.Size(336, 77)
        Me.GroupBox46.TabIndex = 79
        Me.GroupBox46.TabStop = False
        Me.GroupBox46.Text = "Payment Entry"
        '
        'RadButton118
        '
        Me.RadButton118.Location = New System.Drawing.Point(80, 44)
        Me.RadButton118.Name = "RadButton118"
        Me.RadButton118.Size = New System.Drawing.Size(147, 23)
        Me.RadButton118.TabIndex = 52
        Me.RadButton118.Text = "Import Doc to Delete PE"
        '
        'RadButton119
        '
        Me.RadButton119.Location = New System.Drawing.Point(6, 14)
        Me.RadButton119.Name = "RadButton119"
        Me.RadButton119.Size = New System.Drawing.Size(59, 24)
        Me.RadButton119.TabIndex = 43
        Me.RadButton119.Text = "Reset All"
        '
        'RadButton120
        '
        Me.RadButton120.Location = New System.Drawing.Point(69, 14)
        Me.RadButton120.Name = "RadButton120"
        Me.RadButton120.Size = New System.Drawing.Size(92, 24)
        Me.RadButton120.TabIndex = 41
        Me.RadButton120.Text = "Pick Payment No"
        '
        'RadButton121
        '
        Me.RadButton121.Location = New System.Drawing.Point(165, 14)
        Me.RadButton121.Name = "RadButton121"
        Me.RadButton121.Size = New System.Drawing.Size(163, 24)
        Me.RadButton121.TabIndex = 13
        Me.RadButton121.Text = "Delete Selected Payment Entry"
        '
        'GroupBox52
        '
        Me.GroupBox52.Controls.Add(Me.txtPaymentMode)
        Me.GroupBox52.Controls.Add(Me.lblpaymentcode)
        Me.GroupBox52.Controls.Add(Me.MyLabel17)
        Me.GroupBox52.Controls.Add(Me.txtRPLocation)
        Me.GroupBox52.Controls.Add(Me.MyLabel16)
        Me.GroupBox52.Controls.Add(Me.txtRPBank)
        Me.GroupBox52.Controls.Add(Me.RadButton148)
        Me.GroupBox52.Controls.Add(Me.RadButton149)
        Me.GroupBox52.Controls.Add(Me.RadButton150)
        Me.GroupBox52.Location = New System.Drawing.Point(3, 12)
        Me.GroupBox52.Name = "GroupBox52"
        Me.GroupBox52.Size = New System.Drawing.Size(323, 88)
        Me.GroupBox52.TabIndex = 78
        Me.GroupBox52.TabStop = False
        Me.GroupBox52.Text = "Receipt/Payment Entry change Party,Date And Amount"
        '
        'txtPaymentMode
        '
        Me.txtPaymentMode.CalculationExpression = Nothing
        Me.txtPaymentMode.FieldCode = Nothing
        Me.txtPaymentMode.FieldDesc = Nothing
        Me.txtPaymentMode.FieldMaxLength = 0
        Me.txtPaymentMode.FieldName = Nothing
        Me.txtPaymentMode.isCalculatedField = False
        Me.txtPaymentMode.IsSourceFromTable = False
        Me.txtPaymentMode.IsSourceFromValueList = False
        Me.txtPaymentMode.IsUnique = False
        Me.txtPaymentMode.Location = New System.Drawing.Point(99, 66)
        Me.txtPaymentMode.MendatroryField = False
        Me.txtPaymentMode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentMode.MyLinkLable1 = Nothing
        Me.txtPaymentMode.MyLinkLable2 = Nothing
        Me.txtPaymentMode.MyReadOnly = False
        Me.txtPaymentMode.MyShowMasterFormButton = False
        Me.txtPaymentMode.Name = "txtPaymentMode"
        Me.txtPaymentMode.ReferenceFieldDesc = Nothing
        Me.txtPaymentMode.ReferenceFieldName = Nothing
        Me.txtPaymentMode.ReferenceTableName = Nothing
        Me.txtPaymentMode.Size = New System.Drawing.Size(210, 18)
        Me.txtPaymentMode.TabIndex = 50
        Me.txtPaymentMode.Value = ""
        '
        'lblpaymentcode
        '
        Me.lblpaymentcode.FieldName = Nothing
        Me.lblpaymentcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentcode.Location = New System.Drawing.Point(4, 64)
        Me.lblpaymentcode.Name = "lblpaymentcode"
        Me.lblpaymentcode.Size = New System.Drawing.Size(82, 16)
        Me.lblpaymentcode.TabIndex = 49
        Me.lblpaymentcode.Text = "Payment Mode"
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(149, 44)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel17.TabIndex = 48
        Me.MyLabel17.Text = "Location"
        '
        'txtRPLocation
        '
        Me.txtRPLocation.CalculationExpression = Nothing
        Me.txtRPLocation.FieldCode = Nothing
        Me.txtRPLocation.FieldDesc = Nothing
        Me.txtRPLocation.FieldMaxLength = 0
        Me.txtRPLocation.FieldName = Nothing
        Me.txtRPLocation.isCalculatedField = False
        Me.txtRPLocation.IsSourceFromTable = False
        Me.txtRPLocation.IsSourceFromValueList = False
        Me.txtRPLocation.IsUnique = False
        Me.txtRPLocation.Location = New System.Drawing.Point(205, 43)
        Me.txtRPLocation.MendatroryField = False
        Me.txtRPLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRPLocation.MyLinkLable1 = Me.MyLabel17
        Me.txtRPLocation.MyLinkLable2 = Nothing
        Me.txtRPLocation.MyReadOnly = False
        Me.txtRPLocation.MyShowMasterFormButton = False
        Me.txtRPLocation.Name = "txtRPLocation"
        Me.txtRPLocation.ReferenceFieldDesc = Nothing
        Me.txtRPLocation.ReferenceFieldName = Nothing
        Me.txtRPLocation.ReferenceTableName = Nothing
        Me.txtRPLocation.Size = New System.Drawing.Size(104, 18)
        Me.txtRPLocation.TabIndex = 47
        Me.txtRPLocation.Value = ""
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(4, 43)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel16.TabIndex = 46
        Me.MyLabel16.Text = "Bank"
        '
        'txtRPBank
        '
        Me.txtRPBank.CalculationExpression = Nothing
        Me.txtRPBank.FieldCode = Nothing
        Me.txtRPBank.FieldDesc = Nothing
        Me.txtRPBank.FieldMaxLength = 0
        Me.txtRPBank.FieldName = Nothing
        Me.txtRPBank.isCalculatedField = False
        Me.txtRPBank.IsSourceFromTable = False
        Me.txtRPBank.IsSourceFromValueList = False
        Me.txtRPBank.IsUnique = False
        Me.txtRPBank.Location = New System.Drawing.Point(39, 42)
        Me.txtRPBank.MendatroryField = False
        Me.txtRPBank.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRPBank.MyLinkLable1 = Me.MyLabel16
        Me.txtRPBank.MyLinkLable2 = Nothing
        Me.txtRPBank.MyReadOnly = False
        Me.txtRPBank.MyShowMasterFormButton = False
        Me.txtRPBank.Name = "txtRPBank"
        Me.txtRPBank.ReferenceFieldDesc = Nothing
        Me.txtRPBank.ReferenceFieldName = Nothing
        Me.txtRPBank.ReferenceTableName = Nothing
        Me.txtRPBank.Size = New System.Drawing.Size(104, 18)
        Me.txtRPBank.TabIndex = 45
        Me.txtRPBank.Value = ""
        '
        'RadButton148
        '
        Me.RadButton148.Location = New System.Drawing.Point(113, 14)
        Me.RadButton148.Name = "RadButton148"
        Me.RadButton148.Size = New System.Drawing.Size(97, 24)
        Me.RadButton148.TabIndex = 41
        Me.RadButton148.Text = "Pick"
        '
        'RadButton149
        '
        Me.RadButton149.Location = New System.Drawing.Point(6, 14)
        Me.RadButton149.Name = "RadButton149"
        Me.RadButton149.Size = New System.Drawing.Size(97, 24)
        Me.RadButton149.TabIndex = 42
        Me.RadButton149.Text = "Reset"
        '
        'RadButton150
        '
        Me.RadButton150.Location = New System.Drawing.Point(220, 14)
        Me.RadButton150.Name = "RadButton150"
        Me.RadButton150.Size = New System.Drawing.Size(97, 24)
        Me.RadButton150.TabIndex = 13
        Me.RadButton150.Text = "Apply"
        '
        'fndQcNo
        '
        Me.fndQcNo.CalculationExpression = Nothing
        Me.fndQcNo.Enabled = False
        Me.fndQcNo.FieldCode = Nothing
        Me.fndQcNo.FieldDesc = Nothing
        Me.fndQcNo.FieldMaxLength = 0
        Me.fndQcNo.FieldName = Nothing
        Me.fndQcNo.isCalculatedField = False
        Me.fndQcNo.IsSourceFromTable = False
        Me.fndQcNo.IsSourceFromValueList = False
        Me.fndQcNo.IsUnique = False
        Me.fndQcNo.Location = New System.Drawing.Point(927, 5)
        Me.fndQcNo.MendatroryField = True
        Me.fndQcNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndQcNo.MyLinkLable1 = Nothing
        Me.fndQcNo.MyLinkLable2 = Nothing
        Me.fndQcNo.MyReadOnly = False
        Me.fndQcNo.MyShowMasterFormButton = False
        Me.fndQcNo.Name = "fndQcNo"
        Me.fndQcNo.ReferenceFieldDesc = Nothing
        Me.fndQcNo.ReferenceFieldName = Nothing
        Me.fndQcNo.ReferenceTableName = Nothing
        Me.fndQcNo.Size = New System.Drawing.Size(52, 20)
        Me.fndQcNo.TabIndex = 6
        Me.fndQcNo.Value = ""
        Me.fndQcNo.Visible = False
        '
        'FrmBankUpdateUploader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(894, 466)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmBankUpdateUploader"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bank Update Uploader"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.GroupBox51.ResumeLayout(False)
        CType(Me.RadButton145, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.GroupBox44.ResumeLayout(False)
        CType(Me.RadButton113, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton111, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton112, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton114, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox46.ResumeLayout(False)
        CType(Me.RadButton118, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton119, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton120, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton121, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox52.ResumeLayout(False)
        Me.GroupBox52.PerformLayout()
        CType(Me.lblpaymentcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton148, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton149, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton150, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents fndQcNo As common.UserControls.txtFinder
    Friend WithEvents GroupBox52 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPaymentMode As common.UserControls.txtFinder
    Friend WithEvents lblpaymentcode As common.Controls.MyLabel
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents txtRPLocation As common.UserControls.txtFinder
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtRPBank As common.UserControls.txtFinder
    Friend WithEvents RadButton148 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton149 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton150 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GroupBox51 As System.Windows.Forms.GroupBox
    Friend WithEvents RadButton145 As Telerik.WinControls.UI.RadButton
    Friend WithEvents GroupBox46 As System.Windows.Forms.GroupBox
    Friend WithEvents RadButton118 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton119 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton120 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton121 As Telerik.WinControls.UI.RadButton
    Friend WithEvents GroupBox44 As System.Windows.Forms.GroupBox
    Friend WithEvents RadButton113 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton111 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton112 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton114 As Telerik.WinControls.UI.RadButton
End Class
