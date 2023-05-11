<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFixedDeposit
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.fndGLAccount = New common.UserControls.txtFinder
        Me.Lblglaccount = New common.Controls.MyLabel
        Me.MyLabel10 = New common.Controls.MyLabel
        Me.TxtFDRNo = New common.Controls.MyTextBox
        Me.MyLabel9 = New common.Controls.MyLabel
        Me.cboDurationdescription = New common.Controls.MyComboBox
        Me.MyLabel8 = New common.Controls.MyLabel
        Me.TxtmaturityAmount = New common.MyNumBox
        Me.TxtDueDate = New common.Controls.MyLabel
        Me.MyLabel11 = New common.Controls.MyLabel
        Me.MyLabel6 = New common.Controls.MyLabel
        Me.TxtRateofInterest = New common.MyNumBox
        Me.FndLCRequestNo = New common.UserControls.txtFinder
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.LblBankName = New common.Controls.MyLabel
        Me.MyLabel7 = New common.Controls.MyLabel
        Me.TxtDuration = New common.MyNumBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.TxtAmount = New common.MyNumBox
        Me.fndBankCode = New common.UserControls.txtFinder
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtFddate = New common.Controls.MyDateTimePicker
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.lblFixedDeposit = New common.Controls.MyLabel
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.fndFixedDepositcode = New common.UserControls.txtNavigator
        Me.btnPost = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.UsLock1 = New common.usLock
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Lblglaccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFDRNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDurationdescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtmaturityAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDueDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtRateofInterest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDuration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFddate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFixedDeposit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndGLAccount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Lblglaccount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel10)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtFDRNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboDurationdescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtmaturityAmount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtDueDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel11)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtRateofInterest)
        Me.SplitContainer1.Panel1.Controls.Add(Me.FndLCRequestNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblBankName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtDuration)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtAmount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndBankCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFddate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblFixedDeposit)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndFixedDepositcode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(700, 316)
        Me.SplitContainer1.SplitterDistance = 283
        Me.SplitContainer1.TabIndex = 0
        '
        'fndGLAccount
        '
        Me.fndGLAccount.Location = New System.Drawing.Point(118, 192)
        Me.fndGLAccount.MendatroryField = True
        Me.fndGLAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndGLAccount.MyLinkLable1 = Nothing
        Me.fndGLAccount.MyLinkLable2 = Nothing
        Me.fndGLAccount.MyReadOnly = False
        Me.fndGLAccount.MyShowMasterFormButton = False
        Me.fndGLAccount.Name = "fndGLAccount"
        Me.fndGLAccount.Size = New System.Drawing.Size(181, 19)
        Me.fndGLAccount.TabIndex = 366
        Me.fndGLAccount.Value = ""
        '
        'Lblglaccount
        '
        Me.Lblglaccount.AutoSize = False
        Me.Lblglaccount.BorderVisible = True
        Me.Lblglaccount.Location = New System.Drawing.Point(304, 192)
        Me.Lblglaccount.Name = "Lblglaccount"
        Me.Lblglaccount.Size = New System.Drawing.Size(325, 19)
        Me.Lblglaccount.TabIndex = 365
        Me.Lblglaccount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel10
        '
        Me.MyLabel10.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel10.Location = New System.Drawing.Point(9, 192)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel10.TabIndex = 363
        Me.MyLabel10.Text = "GL Account"
        '
        'TxtFDRNo
        '
        Me.TxtFDRNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFDRNo.Location = New System.Drawing.Point(118, 124)
        Me.TxtFDRNo.MaxLength = 200
        Me.TxtFDRNo.MendatroryField = False
        Me.TxtFDRNo.MyLinkLable1 = Nothing
        Me.TxtFDRNo.MyLinkLable2 = Nothing
        Me.TxtFDRNo.Name = "TxtFDRNo"
        Me.TxtFDRNo.Size = New System.Drawing.Size(181, 18)
        Me.TxtFDRNo.TabIndex = 361
        '
        'MyLabel9
        '
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(9, 125)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(47, 16)
        Me.MyLabel9.TabIndex = 362
        Me.MyLabel9.Text = "FDR No"
        '
        'cboDurationdescription
        '
        Me.cboDurationdescription.AllowShowFocusCues = False
        Me.cboDurationdescription.AutoCompleteDisplayMember = Nothing
        Me.cboDurationdescription.AutoCompleteValueMember = Nothing
        Me.cboDurationdescription.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboDurationdescription.Location = New System.Drawing.Point(414, 78)
        Me.cboDurationdescription.MendatroryField = False
        Me.cboDurationdescription.MyLinkLable1 = Nothing
        Me.cboDurationdescription.MyLinkLable2 = Nothing
        Me.cboDurationdescription.Name = "cboDurationdescription"
        Me.cboDurationdescription.Size = New System.Drawing.Size(181, 20)
        Me.cboDurationdescription.TabIndex = 360
        '
        'MyLabel8
        '
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(304, 125)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(89, 16)
        Me.MyLabel8.TabIndex = 359
        Me.MyLabel8.Text = "Maturity Amount"
        '
        'TxtmaturityAmount
        '
        Me.TxtmaturityAmount.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtmaturityAmount.DecimalPlaces = 2
        Me.TxtmaturityAmount.Location = New System.Drawing.Point(414, 123)
        Me.TxtmaturityAmount.MendatroryField = True
        Me.TxtmaturityAmount.MyLinkLable1 = Nothing
        Me.TxtmaturityAmount.MyLinkLable2 = Nothing
        Me.TxtmaturityAmount.Name = "TxtmaturityAmount"
        Me.TxtmaturityAmount.Size = New System.Drawing.Size(181, 20)
        Me.TxtmaturityAmount.TabIndex = 358
        Me.TxtmaturityAmount.Text = "0"
        Me.TxtmaturityAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtmaturityAmount.Value = 0
        '
        'TxtDueDate
        '
        Me.TxtDueDate.AutoSize = False
        Me.TxtDueDate.BorderVisible = True
        Me.TxtDueDate.Location = New System.Drawing.Point(118, 169)
        Me.TxtDueDate.Name = "TxtDueDate"
        Me.TxtDueDate.Size = New System.Drawing.Size(181, 19)
        Me.TxtDueDate.TabIndex = 357
        Me.TxtDueDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel11
        '
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel11.Location = New System.Drawing.Point(9, 170)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(54, 16)
        Me.MyLabel11.TabIndex = 356
        Me.MyLabel11.Text = "Due Date"
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(9, 102)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel6.TabIndex = 58
        Me.MyLabel6.Text = "Rate of Interest"
        '
        'TxtRateofInterest
        '
        Me.TxtRateofInterest.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtRateofInterest.DecimalPlaces = 2
        Me.TxtRateofInterest.Location = New System.Drawing.Point(118, 100)
        Me.TxtRateofInterest.MendatroryField = True
        Me.TxtRateofInterest.MyLinkLable1 = Nothing
        Me.TxtRateofInterest.MyLinkLable2 = Nothing
        Me.TxtRateofInterest.Name = "TxtRateofInterest"
        Me.TxtRateofInterest.Size = New System.Drawing.Size(181, 20)
        Me.TxtRateofInterest.TabIndex = 57
        Me.TxtRateofInterest.Text = "0"
        Me.TxtRateofInterest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtRateofInterest.Value = 0
        '
        'FndLCRequestNo
        '
        Me.FndLCRequestNo.Location = New System.Drawing.Point(118, 146)
        Me.FndLCRequestNo.MendatroryField = True
        Me.FndLCRequestNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndLCRequestNo.MyLinkLable1 = Nothing
        Me.FndLCRequestNo.MyLinkLable2 = Nothing
        Me.FndLCRequestNo.MyReadOnly = False
        Me.FndLCRequestNo.MyShowMasterFormButton = False
        Me.FndLCRequestNo.Name = "FndLCRequestNo"
        Me.FndLCRequestNo.Size = New System.Drawing.Size(181, 19)
        Me.FndLCRequestNo.TabIndex = 56
        Me.FndLCRequestNo.Value = ""
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(9, 147)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel5.TabIndex = 55
        Me.MyLabel5.Text = "LC Request No"
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(304, 80)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(109, 16)
        Me.MyLabel4.TabIndex = 54
        Me.MyLabel4.Text = "Duration Description"
        '
        'LblBankName
        '
        Me.LblBankName.AutoSize = False
        Me.LblBankName.BorderVisible = True
        Me.LblBankName.Location = New System.Drawing.Point(304, 53)
        Me.LblBankName.Name = "LblBankName"
        Me.LblBankName.Size = New System.Drawing.Size(307, 19)
        Me.LblBankName.TabIndex = 4
        Me.LblBankName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(304, 102)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel7.TabIndex = 51
        Me.MyLabel7.Text = "Amount"
        '
        'TxtDuration
        '
        Me.TxtDuration.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtDuration.DecimalPlaces = 0
        Me.TxtDuration.Location = New System.Drawing.Point(118, 78)
        Me.TxtDuration.MendatroryField = True
        Me.TxtDuration.MyLinkLable1 = Nothing
        Me.TxtDuration.MyLinkLable2 = Nothing
        Me.TxtDuration.Name = "TxtDuration"
        Me.TxtDuration.Size = New System.Drawing.Size(181, 20)
        Me.TxtDuration.TabIndex = 5
        Me.TxtDuration.Text = "0"
        Me.TxtDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtDuration.Value = 0
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(9, 80)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel3.TabIndex = 50
        Me.MyLabel3.Text = "Duration"
        '
        'TxtAmount
        '
        Me.TxtAmount.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtAmount.DecimalPlaces = 2
        Me.TxtAmount.Location = New System.Drawing.Point(414, 100)
        Me.TxtAmount.MendatroryField = True
        Me.TxtAmount.MyLinkLable1 = Nothing
        Me.TxtAmount.MyLinkLable2 = Nothing
        Me.TxtAmount.Name = "TxtAmount"
        Me.TxtAmount.Size = New System.Drawing.Size(181, 20)
        Me.TxtAmount.TabIndex = 7
        Me.TxtAmount.Text = "0"
        Me.TxtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtAmount.Value = 0
        '
        'fndBankCode
        '
        Me.fndBankCode.Location = New System.Drawing.Point(118, 53)
        Me.fndBankCode.MendatroryField = True
        Me.fndBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBankCode.MyLinkLable1 = Nothing
        Me.fndBankCode.MyLinkLable2 = Nothing
        Me.fndBankCode.MyReadOnly = False
        Me.fndBankCode.MyShowMasterFormButton = False
        Me.fndBankCode.Name = "fndBankCode"
        Me.fndBankCode.Size = New System.Drawing.Size(181, 19)
        Me.fndBankCode.TabIndex = 3
        Me.fndBankCode.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(9, 32)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel2.TabIndex = 26
        Me.MyLabel2.Text = "FD Opening Date"
        '
        'txtFddate
        '
        Me.txtFddate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtFddate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFddate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFddate.Location = New System.Drawing.Point(118, 31)
        Me.txtFddate.MendatroryField = True
        Me.txtFddate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFddate.MyLinkLable1 = Me.MyLabel2
        Me.txtFddate.MyLinkLable2 = Nothing
        Me.txtFddate.Name = "txtFddate"
        Me.txtFddate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFddate.Size = New System.Drawing.Size(181, 18)
        Me.txtFddate.TabIndex = 2
        Me.txtFddate.TabStop = False
        Me.txtFddate.Text = "13/06/2011 11:29 AM"
        Me.txtFddate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(9, 54)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel1.TabIndex = 25
        Me.MyLabel1.Text = "Bank Code"
        '
        'lblFixedDeposit
        '
        Me.lblFixedDeposit.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblFixedDeposit.Location = New System.Drawing.Point(9, 9)
        Me.lblFixedDeposit.Name = "lblFixedDeposit"
        Me.lblFixedDeposit.Size = New System.Drawing.Size(39, 16)
        Me.lblFixedDeposit.TabIndex = 20
        Me.lblFixedDeposit.Text = "FD No"
        '
        'btnnew
        '
        Me.btnnew.Image = XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(422, 8)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 19)
        Me.btnnew.TabIndex = 1
        '
        'fndFixedDepositcode
        '
        Me.fndFixedDepositcode.Location = New System.Drawing.Point(118, 8)
        Me.fndFixedDepositcode.MendatroryField = True
        Me.fndFixedDepositcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndFixedDepositcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndFixedDepositcode.MyLinkLable1 = Me.lblFixedDeposit
        Me.fndFixedDepositcode.MyLinkLable2 = Nothing
        Me.fndFixedDepositcode.MyMaxLength = 32767
        Me.fndFixedDepositcode.MyReadOnly = False
        Me.fndFixedDepositcode.Name = "fndFixedDepositcode"
        Me.fndFixedDepositcode.Size = New System.Drawing.Size(298, 19)
        Me.fndFixedDepositcode.TabIndex = 0
        Me.fndFixedDepositcode.Value = ""
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Location = New System.Drawing.Point(169, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 3
        Me.btnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(618, 4)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(90, 4)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(9, 4)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(443, 6)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(92, 22)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 367
        '
        'FrmFixedDeposit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(700, 316)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmFixedDeposit"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Fixed Deposit"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Lblglaccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFDRNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDurationdescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtmaturityAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDueDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtRateofInterest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDuration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFddate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFixedDeposit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndFixedDepositcode As common.UserControls.txtNavigator
    Friend WithEvents fndBankCode As common.UserControls.txtFinder
    Friend WithEvents txtFddate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents TxtDuration As common.MyNumBox
    Friend WithEvents TxtAmount As common.MyNumBox
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents LblBankName As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents FndLCRequestNo As common.UserControls.txtFinder
    Friend WithEvents TxtRateofInterest As common.MyNumBox
    Friend WithEvents TxtDueDate As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents TxtmaturityAmount As common.MyNumBox
    Friend WithEvents cboDurationdescription As common.Controls.MyComboBox
    Friend WithEvents TxtFDRNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblFixedDeposit As common.Controls.MyLabel
    Friend WithEvents fndGLAccount As common.UserControls.txtFinder
    Friend WithEvents Lblglaccount As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
End Class

