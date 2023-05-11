<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTargetMaster
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
        Me.lblCustGroupCode = New common.Controls.MyLabel
        Me.lblMonthYear = New common.Controls.MyLabel
        Me.lblCustCode = New common.Controls.MyLabel
        Me.lblDiscType = New common.Controls.MyLabel
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnGO = New Telerik.WinControls.UI.RadButton
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.dtpMonthnYear = New common.Controls.MyDateTimePicker
        Me.txtDiscTypeDesc = New common.Controls.MyTextBox
        Me.txtCustGroupCode = New common.Controls.MyTextBox
        Me.txtCustDesc = New common.Controls.MyTextBox
        Me.txtDiscType = New common.UserControls.txtFinder
        Me.txtCustGroupCodeDesc = New common.Controls.MyTextBox
        Me.txtCustCode = New common.UserControls.txtFinder
        Me.btnReplicate = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.txtAmount = New common.MyNumBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.lblBalAmount = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.txtNewAmount = New common.MyNumBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.lblCurrentAmount = New common.Controls.MyLabel
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RMIExport = New Telerik.WinControls.UI.RadMenuItem
        Me.RMIImport = New Telerik.WinControls.UI.RadMenuItem
        Me.RMIExit = New Telerik.WinControls.UI.RadMenuItem
        CType(Me.lblCustGroupCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMonthYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDiscType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpMonthnYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiscTypeDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustGroupCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustGroupCodeDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReplicate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNewAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrentAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblCustGroupCode
        '
        Me.lblCustGroupCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustGroupCode.Location = New System.Drawing.Point(9, 29)
        Me.lblCustGroupCode.Name = "lblCustGroupCode"
        Me.lblCustGroupCode.Size = New System.Drawing.Size(120, 16)
        Me.lblCustGroupCode.TabIndex = 33
        Me.lblCustGroupCode.Text = "Customer Group Code"
        '
        'lblMonthYear
        '
        Me.lblMonthYear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonthYear.Location = New System.Drawing.Point(9, 74)
        Me.lblMonthYear.Name = "lblMonthYear"
        Me.lblMonthYear.Size = New System.Drawing.Size(91, 16)
        Me.lblMonthYear.TabIndex = 32
        Me.lblMonthYear.Text = "Month And  Year"
        '
        'lblCustCode
        '
        Me.lblCustCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustCode.Location = New System.Drawing.Point(9, 7)
        Me.lblCustCode.Name = "lblCustCode"
        Me.lblCustCode.Size = New System.Drawing.Size(85, 16)
        Me.lblCustCode.TabIndex = 34
        Me.lblCustCode.Text = "Customer Code"
        '
        'lblDiscType
        '
        Me.lblDiscType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscType.Location = New System.Drawing.Point(9, 51)
        Me.lblDiscType.Name = "lblDiscType"
        Me.lblDiscType.Size = New System.Drawing.Size(79, 16)
        Me.lblDiscType.TabIndex = 37
        Me.lblDiscType.Text = "Discount Type"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(82, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(7, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(658, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnGO
        '
        Me.btnGO.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGO.Location = New System.Drawing.Point(277, 76)
        Me.btnGO.Name = "btnGO"
        Me.btnGO.Size = New System.Drawing.Size(69, 22)
        Me.btnGO.TabIndex = 12
        Me.btnGO.Text = "GO"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(291, 5)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(17, 20)
        Me.btnnew.TabIndex = 1
        '
        'dtpMonthnYear
        '
        Me.dtpMonthnYear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpMonthnYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMonthnYear.Location = New System.Drawing.Point(125, 76)
        Me.dtpMonthnYear.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpMonthnYear.MendatroryField = False
        Me.dtpMonthnYear.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpMonthnYear.MyLinkLable1 = Me.lblMonthYear
        Me.dtpMonthnYear.MyLinkLable2 = Nothing
        Me.dtpMonthnYear.Name = "dtpMonthnYear"
        Me.dtpMonthnYear.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpMonthnYear.Size = New System.Drawing.Size(146, 18)
        Me.dtpMonthnYear.TabIndex = 7
        Me.dtpMonthnYear.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'txtDiscTypeDesc
        '
        Me.txtDiscTypeDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscTypeDesc.Location = New System.Drawing.Point(224, 52)
        Me.txtDiscTypeDesc.MendatroryField = False
        Me.txtDiscTypeDesc.MyLinkLable1 = Nothing
        Me.txtDiscTypeDesc.MyLinkLable2 = Nothing
        Me.txtDiscTypeDesc.Name = "txtDiscTypeDesc"
        Me.txtDiscTypeDesc.Size = New System.Drawing.Size(503, 18)
        Me.txtDiscTypeDesc.TabIndex = 6
        '
        'txtCustGroupCode
        '
        Me.txtCustGroupCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustGroupCode.Location = New System.Drawing.Point(135, 28)
        Me.txtCustGroupCode.MendatroryField = False
        Me.txtCustGroupCode.MyLinkLable1 = Me.lblCustGroupCode
        Me.txtCustGroupCode.MyLinkLable2 = Nothing
        Me.txtCustGroupCode.Name = "txtCustGroupCode"
        Me.txtCustGroupCode.ReadOnly = True
        Me.txtCustGroupCode.Size = New System.Drawing.Size(83, 18)
        Me.txtCustGroupCode.TabIndex = 3
        '
        'txtCustDesc
        '
        Me.txtCustDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustDesc.Location = New System.Drawing.Point(314, 6)
        Me.txtCustDesc.MendatroryField = False
        Me.txtCustDesc.MyLinkLable1 = Nothing
        Me.txtCustDesc.MyLinkLable2 = Nothing
        Me.txtCustDesc.Name = "txtCustDesc"
        Me.txtCustDesc.ReadOnly = True
        Me.txtCustDesc.Size = New System.Drawing.Size(413, 18)
        Me.txtCustDesc.TabIndex = 2
        '
        'txtDiscType
        '
        Me.txtDiscType.Location = New System.Drawing.Point(125, 52)
        Me.txtDiscType.MendatroryField = True
        Me.txtDiscType.MyLinkLable1 = Me.lblDiscType
        Me.txtDiscType.MyLinkLable2 = Nothing
        Me.txtDiscType.MyReadOnly = False
        Me.txtDiscType.Name = "txtDiscType"
        Me.txtDiscType.Size = New System.Drawing.Size(93, 18)
        Me.txtDiscType.TabIndex = 5
        Me.txtDiscType.Value = ""
        '
        'txtCustGroupCodeDesc
        '
        Me.txtCustGroupCodeDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustGroupCodeDesc.Location = New System.Drawing.Point(224, 28)
        Me.txtCustGroupCodeDesc.MendatroryField = False
        Me.txtCustGroupCodeDesc.MyLinkLable1 = Nothing
        Me.txtCustGroupCodeDesc.MyLinkLable2 = Nothing
        Me.txtCustGroupCodeDesc.Name = "txtCustGroupCodeDesc"
        Me.txtCustGroupCodeDesc.ReadOnly = True
        Me.txtCustGroupCodeDesc.Size = New System.Drawing.Size(503, 18)
        Me.txtCustGroupCodeDesc.TabIndex = 4
        '
        'txtCustCode
        '
        Me.txtCustCode.Location = New System.Drawing.Point(125, 6)
        Me.txtCustCode.MendatroryField = True
        Me.txtCustCode.MyLinkLable1 = Me.lblCustCode
        Me.txtCustCode.MyLinkLable2 = Nothing
        Me.txtCustCode.MyReadOnly = False
        Me.txtCustCode.Name = "txtCustCode"
        Me.txtCustCode.Size = New System.Drawing.Size(164, 18)
        Me.txtCustCode.TabIndex = 0
        Me.txtCustCode.Value = ""
        '
        'btnReplicate
        '
        Me.btnReplicate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReplicate.Location = New System.Drawing.Point(169, 4)
        Me.btnReplicate.Name = "btnReplicate"
        Me.btnReplicate.Size = New System.Drawing.Size(235, 22)
        Me.btnReplicate.TabIndex = 2
        Me.btnReplicate.Text = "Replicate To All Customers Of Same Group"
        '
        'btnPrint
        '
        Me.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.ImageAlignment = System.Drawing.ContentAlignment.BottomLeft
        Me.btnPrint.Location = New System.Drawing.Point(410, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'txtAmount
        '
        Me.txtAmount.BackColor = System.Drawing.Color.White
        Me.txtAmount.DecimalPlaces = 0
        Me.txtAmount.Location = New System.Drawing.Point(125, 98)
        Me.txtAmount.MendatroryField = False
        Me.txtAmount.MyLinkLable1 = Me.MyLabel1
        Me.txtAmount.MyLinkLable2 = Nothing
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(146, 20)
        Me.txtAmount.TabIndex = 8
        Me.txtAmount.Text = "0"
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAmount.Value = 0
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 99)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(92, 16)
        Me.MyLabel1.TabIndex = 115
        Me.MyLabel1.Text = "Amount To Enter"
        '
        'lblBalAmount
        '
        Me.lblBalAmount.AutoSize = False
        Me.lblBalAmount.BackColor = System.Drawing.Color.White
        Me.lblBalAmount.BorderVisible = True
        Me.lblBalAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalAmount.Location = New System.Drawing.Point(397, 144)
        Me.lblBalAmount.Name = "lblBalAmount"
        Me.lblBalAmount.Size = New System.Drawing.Size(146, 18)
        Me.lblBalAmount.TabIndex = 11
        Me.lblBalAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblBalAmount.TextWrap = False
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(302, 144)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(89, 16)
        Me.MyLabel2.TabIndex = 117
        Me.MyLabel2.Text = "Balance Amount"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtNewAmount)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.lblCurrentAmount)
        Me.RadGroupBox1.Controls.Add(Me.lblCustCode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtCustGroupCodeDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblBalAmount)
        Me.RadGroupBox1.Controls.Add(Me.txtDiscType)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.lblMonthYear)
        Me.RadGroupBox1.Controls.Add(Me.txtAmount)
        Me.RadGroupBox1.Controls.Add(Me.lblCustGroupCode)
        Me.RadGroupBox1.Controls.Add(Me.txtCustDesc)
        Me.RadGroupBox1.Controls.Add(Me.txtCustGroupCode)
        Me.RadGroupBox1.Controls.Add(Me.txtCustCode)
        Me.RadGroupBox1.Controls.Add(Me.lblDiscType)
        Me.RadGroupBox1.Controls.Add(Me.btnnew)
        Me.RadGroupBox1.Controls.Add(Me.txtDiscTypeDesc)
        Me.RadGroupBox1.Controls.Add(Me.btnGO)
        Me.RadGroupBox1.Controls.Add(Me.dtpMonthnYear)
        Me.RadGroupBox1.FooterImageIndex = -1
        Me.RadGroupBox1.FooterImageKey = ""
        Me.RadGroupBox1.HeaderImageIndex = -1
        Me.RadGroupBox1.HeaderImageKey = ""
        Me.RadGroupBox1.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 27)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox1.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(735, 167)
        Me.RadGroupBox1.TabIndex = 0
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(9, 144)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel4.TabIndex = 121
        Me.MyLabel4.Text = "Current Amount"
        '
        'txtNewAmount
        '
        Me.txtNewAmount.BackColor = System.Drawing.Color.White
        Me.txtNewAmount.DecimalPlaces = 0
        Me.txtNewAmount.Location = New System.Drawing.Point(125, 121)
        Me.txtNewAmount.MendatroryField = False
        Me.txtNewAmount.MyLinkLable1 = Me.MyLabel3
        Me.txtNewAmount.MyLinkLable2 = Nothing
        Me.txtNewAmount.Name = "txtNewAmount"
        Me.txtNewAmount.Size = New System.Drawing.Size(146, 20)
        Me.txtNewAmount.TabIndex = 9
        Me.txtNewAmount.Text = "0"
        Me.txtNewAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNewAmount.Value = 0
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(9, 121)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel3.TabIndex = 119
        Me.MyLabel3.Text = "New Amount"
        '
        'lblCurrentAmount
        '
        Me.lblCurrentAmount.AutoSize = False
        Me.lblCurrentAmount.BackColor = System.Drawing.Color.White
        Me.lblCurrentAmount.BorderVisible = True
        Me.lblCurrentAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentAmount.Location = New System.Drawing.Point(125, 144)
        Me.lblCurrentAmount.Name = "lblCurrentAmount"
        Me.lblCurrentAmount.Size = New System.Drawing.Size(146, 18)
        Me.lblCurrentAmount.TabIndex = 10
        Me.lblCurrentAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCurrentAmount.TextWrap = False
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.btnSave)
        Me.RadGroupBox2.Controls.Add(Me.btnDelete)
        Me.RadGroupBox2.Controls.Add(Me.btnPrint)
        Me.RadGroupBox2.Controls.Add(Me.btnClose)
        Me.RadGroupBox2.Controls.Add(Me.btnReplicate)
        Me.RadGroupBox2.FooterImageIndex = -1
        Me.RadGroupBox2.FooterImageKey = ""
        Me.RadGroupBox2.HeaderImageIndex = -1
        Me.RadGroupBox2.HeaderImageKey = ""
        Me.RadGroupBox2.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 198)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox2.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(735, 30)
        Me.RadGroupBox2.TabIndex = 1
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(741, 20)
        Me.RadMenu1.TabIndex = 121
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMIExport, Me.RMIImport, Me.RMIExit})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RMIExport
        '
        Me.RMIExport.AccessibleDescription = "Import"
        Me.RMIExport.AccessibleName = "Import"
        Me.RMIExport.Name = "RMIExport"
        Me.RMIExport.Text = "Import"
        Me.RMIExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RMIImport
        '
        Me.RMIImport.AccessibleDescription = "Export"
        Me.RMIImport.AccessibleName = "Export"
        Me.RMIImport.Name = "RMIImport"
        Me.RMIImport.Text = "Export"
        Me.RMIImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RMIExit
        '
        Me.RMIExit.AccessibleDescription = "Exit"
        Me.RMIExit.AccessibleName = "Exit"
        Me.RMIExit.Name = "RMIExit"
        Me.RMIExit.Text = "Exit"
        Me.RMIExit.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'FrmTargetMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(741, 229)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.RadGroupBox2)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.KeyPreview = True
        Me.Name = "FrmTargetMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Target Master"
        CType(Me.lblCustGroupCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMonthYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDiscType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpMonthnYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiscTypeDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustGroupCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustGroupCodeDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReplicate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNewAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrentAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtDiscType As common.UserControls.txtFinder
    Friend WithEvents txtCustGroupCodeDesc As common.Controls.MyTextBox
    Friend WithEvents txtCustDesc As common.Controls.MyTextBox
    Friend WithEvents txtCustGroupCode As common.Controls.MyTextBox
    Friend WithEvents txtDiscTypeDesc As common.Controls.MyTextBox
    Friend WithEvents dtpMonthnYear As common.Controls.MyDateTimePicker
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGO As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCustCode As common.UserControls.txtFinder
    Friend WithEvents btnReplicate As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblCustGroupCode As common.Controls.MyLabel
    Friend WithEvents lblMonthYear As common.Controls.MyLabel
    Friend WithEvents lblCustCode As common.Controls.MyLabel
    Friend WithEvents lblDiscType As common.Controls.MyLabel
    Friend WithEvents txtAmount As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblBalAmount As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblCurrentAmount As common.Controls.MyLabel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMIExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMIImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMIExit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtNewAmount As common.MyNumBox
End Class

