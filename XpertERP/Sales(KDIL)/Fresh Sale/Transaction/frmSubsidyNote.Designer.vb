<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSubsidyNote
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
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgCustomer = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbtnCustomerSelect = New common.Controls.MyRadioButton()
        Me.rbtnCustomerAll = New common.Controls.MyRadioButton()
        Me.dtpPayment = New common.Controls.MyDateTimePicker()
        Me.lblpaymentdate = New common.Controls.MyLabel()
        Me.LblLocDesp = New common.Controls.MyLabel()
        Me.txtlocation = New common.UserControls.txtFinder()
        Me.RadLabel18 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.lblCustomer = New common.Controls.MyLabel()
        Me.txtCustomerMult = New common.UserControls.txtMultiSelectFinder()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.rbtnCustomerSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCustomerAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblLocDesp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpPayment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblpaymentdate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblLocDesp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtlocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel18)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDocNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCustomer)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustomerMult)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(731, 474)
        Me.SplitContainer1.SplitterDistance = 420
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox8.Controls.Add(Me.Panel1)
        Me.RadGroupBox8.HeaderText = "Customer"
        Me.RadGroupBox8.Location = New System.Drawing.Point(12, 57)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox8.Size = New System.Drawing.Size(339, 206)
        Me.RadGroupBox8.TabIndex = 306
        Me.RadGroupBox8.Text = "Customer"
        '
        'cbgCustomer
        '
        Me.cbgCustomer.CheckedValue = Nothing
        Me.cbgCustomer.DataSource = Nothing
        Me.cbgCustomer.DisplayMember = "Name"
        Me.cbgCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCustomer.Location = New System.Drawing.Point(10, 43)
        Me.cbgCustomer.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustomer.MyShowHeadrText = False
        Me.cbgCustomer.Name = "cbgCustomer"
        Me.cbgCustomer.Size = New System.Drawing.Size(319, 153)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbtnCustomerSelect)
        Me.Panel1.Controls.Add(Me.rbtnCustomerAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(319, 23)
        Me.Panel1.TabIndex = 0
        '
        'rbtnCustomerSelect
        '
        Me.rbtnCustomerSelect.Location = New System.Drawing.Point(193, 4)
        Me.rbtnCustomerSelect.MyLinkLable1 = Nothing
        Me.rbtnCustomerSelect.MyLinkLable2 = Nothing
        Me.rbtnCustomerSelect.Name = "rbtnCustomerSelect"
        Me.rbtnCustomerSelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnCustomerSelect.TabIndex = 1
        Me.rbtnCustomerSelect.Text = "Select"
        '
        'rbtnCustomerAll
        '
        Me.rbtnCustomerAll.Location = New System.Drawing.Point(147, 4)
        Me.rbtnCustomerAll.MyLinkLable1 = Nothing
        Me.rbtnCustomerAll.MyLinkLable2 = Nothing
        Me.rbtnCustomerAll.Name = "rbtnCustomerAll"
        Me.rbtnCustomerAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCustomerAll.TabIndex = 0
        Me.rbtnCustomerAll.Text = "All"
        '
        'dtpPayment
        '
        Me.dtpPayment.CalculationExpression = Nothing
        Me.dtpPayment.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpPayment.FieldCode = Nothing
        Me.dtpPayment.FieldDesc = Nothing
        Me.dtpPayment.FieldMaxLength = 0
        Me.dtpPayment.FieldName = Nothing
        Me.dtpPayment.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPayment.isCalculatedField = False
        Me.dtpPayment.IsSourceFromTable = False
        Me.dtpPayment.IsSourceFromValueList = False
        Me.dtpPayment.IsUnique = False
        Me.dtpPayment.Location = New System.Drawing.Point(454, 30)
        Me.dtpPayment.MendatroryField = False
        Me.dtpPayment.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPayment.MyLinkLable1 = Nothing
        Me.dtpPayment.MyLinkLable2 = Nothing
        Me.dtpPayment.Name = "dtpPayment"
        Me.dtpPayment.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPayment.ReferenceFieldDesc = Nothing
        Me.dtpPayment.ReferenceFieldName = Nothing
        Me.dtpPayment.ReferenceTableName = Nothing
        Me.dtpPayment.Size = New System.Drawing.Size(83, 20)
        Me.dtpPayment.TabIndex = 6
        Me.dtpPayment.TabStop = False
        Me.dtpPayment.Text = "10/06/2011 11:51 AM"
        Me.dtpPayment.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblpaymentdate
        '
        Me.lblpaymentdate.FieldName = Nothing
        Me.lblpaymentdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentdate.Location = New System.Drawing.Point(370, 32)
        Me.lblpaymentdate.Name = "lblpaymentdate"
        Me.lblpaymentdate.Size = New System.Drawing.Size(85, 16)
        Me.lblpaymentdate.TabIndex = 5
        Me.lblpaymentdate.Text = "Document Date"
        '
        'LblLocDesp
        '
        Me.LblLocDesp.AutoSize = False
        Me.LblLocDesp.BorderVisible = True
        Me.LblLocDesp.FieldName = Nothing
        Me.LblLocDesp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLocDesp.Location = New System.Drawing.Point(247, 279)
        Me.LblLocDesp.Name = "LblLocDesp"
        Me.LblLocDesp.Size = New System.Drawing.Size(410, 18)
        Me.LblLocDesp.TabIndex = 44
        Me.LblLocDesp.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblLocDesp.TextWrap = False
        '
        'txtlocation
        '
        Me.txtlocation.CalculationExpression = Nothing
        Me.txtlocation.FieldCode = Nothing
        Me.txtlocation.FieldDesc = Nothing
        Me.txtlocation.FieldMaxLength = 0
        Me.txtlocation.FieldName = Nothing
        Me.txtlocation.isCalculatedField = False
        Me.txtlocation.IsSourceFromTable = False
        Me.txtlocation.IsSourceFromValueList = False
        Me.txtlocation.IsUnique = False
        Me.txtlocation.Location = New System.Drawing.Point(98, 278)
        Me.txtlocation.MendatroryField = True
        Me.txtlocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlocation.MyLinkLable1 = Nothing
        Me.txtlocation.MyLinkLable2 = Nothing
        Me.txtlocation.MyReadOnly = False
        Me.txtlocation.MyShowMasterFormButton = False
        Me.txtlocation.Name = "txtlocation"
        Me.txtlocation.ReferenceFieldDesc = Nothing
        Me.txtlocation.ReferenceFieldName = Nothing
        Me.txtlocation.ReferenceTableName = Nothing
        Me.txtlocation.Size = New System.Drawing.Size(142, 18)
        Me.txtlocation.TabIndex = 42
        Me.txtlocation.Value = ""
        '
        'RadLabel18
        '
        Me.RadLabel18.Location = New System.Drawing.Point(10, 278)
        Me.RadLabel18.Name = "RadLabel18"
        Me.RadLabel18.Size = New System.Drawing.Size(49, 18)
        Me.RadLabel18.TabIndex = 43
        Me.RadLabel18.Text = "Location"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(12, 30)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel1.TabIndex = 35
        Me.RadLabel1.Text = "Document No"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(342, 28)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(19, 20)
        Me.btnAddNew.TabIndex = 34
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(100, 28)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(242, 20)
        Me.txtDocNo.TabIndex = 33
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'lblCustomer
        '
        Me.lblCustomer.FieldName = Nothing
        Me.lblCustomer.Location = New System.Drawing.Point(10, 303)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(55, 18)
        Me.lblCustomer.TabIndex = 16
        Me.lblCustomer.Text = "Customer"
        Me.lblCustomer.Visible = False
        '
        'txtCustomerMult
        '
        Me.txtCustomerMult.arrDispalyMember = Nothing
        Me.txtCustomerMult.arrValueMember = Nothing
        Me.txtCustomerMult.Location = New System.Drawing.Point(98, 303)
        Me.txtCustomerMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerMult.MyLinkLable1 = Nothing
        Me.txtCustomerMult.MyLinkLable2 = Nothing
        Me.txtCustomerMult.MyNullText = "All"
        Me.txtCustomerMult.Name = "txtCustomerMult"
        Me.txtCustomerMult.Size = New System.Drawing.Size(299, 19)
        Me.txtCustomerMult.TabIndex = 15
        Me.txtCustomerMult.Visible = False
        '
        'RadMenu1
        '
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(731, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(635, 16)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(12, 16)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 0
        Me.btnGo.Text = "Save"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(84, 16)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 1
        Me.btnReset.Text = "Reset"
        '
        'frmSubsidyNote
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(731, 474)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmSubsidyNote"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "rptCrateAccounting"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.rbtnCustomerSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCustomerAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpPayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblLocDesp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents txtCustomerMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents LblLocDesp As common.Controls.MyLabel
    Friend WithEvents txtlocation As common.UserControls.txtFinder
    Friend WithEvents RadLabel18 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents dtpPayment As common.Controls.MyDateTimePicker
    Friend WithEvents lblpaymentdate As common.Controls.MyLabel
    Friend WithEvents RadGroupBox8 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbtnCustomerSelect As common.Controls.MyRadioButton
    Friend WithEvents rbtnCustomerAll As common.Controls.MyRadioButton
End Class
