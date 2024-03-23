<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmQuickSettlement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmQuickSettlement))
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewDecimalColumn1 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn()
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.grp1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtQSDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.btnPostfinnancial = New Telerik.WinControls.UI.RadButton()
        Me.txtNetSaleAmount = New Telerik.WinControls.UI.RadTextBox()
        Me.txtSchemeAmt = New Telerik.WinControls.UI.RadTextBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtProvisionalSaleAmt = New Telerik.WinControls.UI.RadTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtCashMemo = New Telerik.WinControls.UI.RadTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndSalesmanCode = New common.UserControls.txtFinder()
        Me.lblTransferNumber = New common.Controls.MyLabel()
        Me.btnLoadOutView = New Telerik.WinControls.UI.RadButton()
        Me.fndTransferNumber = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.txtBalanceAmount = New Telerik.WinControls.UI.RadTextBox()
        Me.txtempty = New Telerik.WinControls.UI.RadTextBox()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.txtLoadInAmount = New Telerik.WinControls.UI.RadTextBox()
        Me.lblComments = New common.Controls.MyLabel()
        Me.txtComments = New Telerik.WinControls.UI.RadTextBox()
        Me.lblBalanceAmount = New common.Controls.MyLabel()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.txtVehicleNo = New Telerik.WinControls.UI.RadTextBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.txtRouteNo = New Telerik.WinControls.UI.RadTextBox()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.lblRouteDescription = New common.Controls.MyLabel()
        Me.lblSalesman = New common.Controls.MyLabel()
        Me.txtRoutedescription = New Telerik.WinControls.UI.RadTextBox()
        Me.txtSalesman = New Telerik.WinControls.UI.RadTextBox()
        Me.txtTotalAmount = New Telerik.WinControls.UI.RadTextBox()
        Me.lblTotalAmount = New common.Controls.MyLabel()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.fndQuickSettlement = New common.UserControls.txtNavigator()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.txtTransferDate = New Telerik.WinControls.UI.RadTextBox()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.dgvQuickSettleMent = New common.UserControls.MyRadGridView()
        Me.lblAmount = New common.Controls.MyLabel()
        Me.lblTranserDate = New common.Controls.MyLabel()
        Me.txtAmount = New Telerik.WinControls.UI.RadTextBox()
        CType(Me.grp1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grp1.SuspendLayout()
        CType(Me.txtQSDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPostfinnancial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNetSaleAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSchemeAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProvisionalSaleAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCashMemo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransferNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnLoadOutView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBalanceAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtempty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLoadInAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblComments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBalanceAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRouteNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRoutedescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalesman, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTransferDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvQuickSettleMent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvQuickSettleMent.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTranserDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grp1
        '
        Me.grp1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grp1.Controls.Add(Me.txtQSDate)
        Me.grp1.Controls.Add(Me.btnPostfinnancial)
        Me.grp1.Controls.Add(Me.txtNetSaleAmount)
        Me.grp1.Controls.Add(Me.txtSchemeAmt)
        Me.grp1.Controls.Add(Me.MyLabel5)
        Me.grp1.Controls.Add(Me.MyLabel4)
        Me.grp1.Controls.Add(Me.txtProvisionalSaleAmt)
        Me.grp1.Controls.Add(Me.MyLabel3)
        Me.grp1.Controls.Add(Me.txtCashMemo)
        Me.grp1.Controls.Add(Me.MyLabel1)
        Me.grp1.Controls.Add(Me.MyLabel2)
        Me.grp1.Controls.Add(Me.fndSalesmanCode)
        Me.grp1.Controls.Add(Me.btnLoadOutView)
        Me.grp1.Controls.Add(Me.fndTransferNumber)
        Me.grp1.Controls.Add(Me.UsLock1)
        Me.grp1.Controls.Add(Me.txtBalanceAmount)
        Me.grp1.Controls.Add(Me.txtempty)
        Me.grp1.Controls.Add(Me.RadLabel7)
        Me.grp1.Controls.Add(Me.RadLabel6)
        Me.grp1.Controls.Add(Me.RadLabel4)
        Me.grp1.Controls.Add(Me.txtLoadInAmount)
        Me.grp1.Controls.Add(Me.lblComments)
        Me.grp1.Controls.Add(Me.txtComments)
        Me.grp1.Controls.Add(Me.lblBalanceAmount)
        Me.grp1.Controls.Add(Me.RadLabel5)
        Me.grp1.Controls.Add(Me.txtVehicleNo)
        Me.grp1.Controls.Add(Me.RadLabel2)
        Me.grp1.Controls.Add(Me.txtRouteNo)
        Me.grp1.Controls.Add(Me.RadLabel1)
        Me.grp1.Controls.Add(Me.lblRouteDescription)
        Me.grp1.Controls.Add(Me.lblSalesman)
        Me.grp1.Controls.Add(Me.txtRoutedescription)
        Me.grp1.Controls.Add(Me.txtSalesman)
        Me.grp1.Controls.Add(Me.txtTotalAmount)
        Me.grp1.Controls.Add(Me.lblTotalAmount)
        Me.grp1.Controls.Add(Me.btnPrint)
        Me.grp1.Controls.Add(Me.btnPost)
        Me.grp1.Controls.Add(Me.fndQuickSettlement)
        Me.grp1.Controls.Add(Me.RadLabel3)
        Me.grp1.Controls.Add(Me.btnNew)
        Me.grp1.Controls.Add(Me.btnDelete)
        Me.grp1.Controls.Add(Me.txtTransferDate)
        Me.grp1.Controls.Add(Me.btnSave)
        Me.grp1.Controls.Add(Me.btnClose)
        Me.grp1.Controls.Add(Me.dgvQuickSettleMent)
        Me.grp1.Controls.Add(Me.lblAmount)
        Me.grp1.Controls.Add(Me.lblTranserDate)
        Me.grp1.Controls.Add(Me.txtAmount)
        Me.grp1.Controls.Add(Me.lblTransferNumber)
        Me.grp1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grp1.HeaderText = ""
        Me.grp1.Location = New System.Drawing.Point(0, 0)
        Me.grp1.Name = "grp1"
        Me.grp1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grp1.Size = New System.Drawing.Size(781, 549)
        Me.grp1.TabIndex = 0
        '
        'txtQSDate
        '
        Me.txtQSDate.CalculationExpression = Nothing
        Me.txtQSDate.CustomFormat = "dd/MM/yyyy"
        Me.txtQSDate.FieldCode = Nothing
        Me.txtQSDate.FieldDesc = Nothing
        Me.txtQSDate.FieldMaxLength = 0
        Me.txtQSDate.FieldName = Nothing
        Me.txtQSDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQSDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtQSDate.isCalculatedField = False
        Me.txtQSDate.IsSourceFromTable = False
        Me.txtQSDate.IsSourceFromValueList = False
        Me.txtQSDate.IsUnique = False
        Me.txtQSDate.Location = New System.Drawing.Point(155, 39)
        Me.txtQSDate.MendatroryField = False
        Me.txtQSDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtQSDate.MyLinkLable1 = Me.RadLabel4
        Me.txtQSDate.MyLinkLable2 = Nothing
        Me.txtQSDate.Name = "txtQSDate"
        Me.txtQSDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtQSDate.ReferenceFieldDesc = Nothing
        Me.txtQSDate.ReferenceFieldName = Nothing
        Me.txtQSDate.ReferenceTableName = Nothing
        Me.txtQSDate.Size = New System.Drawing.Size(79, 18)
        Me.txtQSDate.TabIndex = 44
        Me.txtQSDate.TabStop = False
        Me.txtQSDate.Text = "13/06/2011"
        Me.txtQSDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Location = New System.Drawing.Point(508, 10)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(12, 18)
        Me.RadLabel4.TabIndex = 20
        Me.RadLabel4.Text = "0"
        Me.RadLabel4.Visible = False
        '
        'btnPostfinnancial
        '
        Me.btnPostfinnancial.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPostfinnancial.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPostfinnancial.Location = New System.Drawing.Point(313, 525)
        Me.btnPostfinnancial.Name = "btnPostfinnancial"
        Me.btnPostfinnancial.Size = New System.Drawing.Size(113, 18)
        Me.btnPostfinnancial.TabIndex = 24
        Me.btnPostfinnancial.Text = "Post Financial Entry"
        Me.btnPostfinnancial.Visible = False
        '
        'txtNetSaleAmount
        '
        Me.txtNetSaleAmount.Location = New System.Drawing.Point(506, 203)
        Me.txtNetSaleAmount.Name = "txtNetSaleAmount"
        Me.txtNetSaleAmount.ReadOnly = True
        Me.txtNetSaleAmount.Size = New System.Drawing.Size(231, 20)
        Me.txtNetSaleAmount.TabIndex = 17
        Me.txtNetSaleAmount.Text = " 0.0"
        Me.txtNetSaleAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSchemeAmt
        '
        Me.txtSchemeAmt.Location = New System.Drawing.Point(506, 178)
        Me.txtSchemeAmt.Name = "txtSchemeAmt"
        Me.txtSchemeAmt.ReadOnly = True
        Me.txtSchemeAmt.Size = New System.Drawing.Size(231, 20)
        Me.txtSchemeAmt.TabIndex = 15
        Me.txtSchemeAmt.Text = " 0.0"
        Me.txtSchemeAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(365, 203)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(92, 16)
        Me.MyLabel5.TabIndex = 43
        Me.MyLabel5.Text = "Net Sale Amount"
        Me.MyLabel5.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(365, 177)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(117, 16)
        Me.MyLabel4.TabIndex = 41
        Me.MyLabel4.Text = "FOC/Scheme Amount"
        Me.MyLabel4.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        '
        'txtProvisionalSaleAmt
        '
        Me.txtProvisionalSaleAmt.Location = New System.Drawing.Point(506, 153)
        Me.txtProvisionalSaleAmt.Name = "txtProvisionalSaleAmt"
        Me.txtProvisionalSaleAmt.ReadOnly = True
        Me.txtProvisionalSaleAmt.Size = New System.Drawing.Size(231, 20)
        Me.txtProvisionalSaleAmt.TabIndex = 13
        Me.txtProvisionalSaleAmt.Text = " 0.0"
        Me.txtProvisionalSaleAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(365, 153)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(127, 16)
        Me.MyLabel3.TabIndex = 39
        Me.MyLabel3.Text = "Provisonal Sale Amount"
        Me.MyLabel3.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        '
        'txtCashMemo
        '
        Me.txtCashMemo.Location = New System.Drawing.Point(506, 130)
        Me.txtCashMemo.Name = "txtCashMemo"
        Me.txtCashMemo.Size = New System.Drawing.Size(230, 20)
        Me.txtCashMemo.TabIndex = 11
        Me.txtCashMemo.Text = " 0"
        Me.txtCashMemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(16, 85)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel1.TabIndex = 18
        Me.MyLabel1.Text = "Salesman Code"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(365, 131)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(101, 16)
        Me.MyLabel2.TabIndex = 34
        Me.MyLabel2.Text = "Cash Memo Count"
        '
        'fndSalesmanCode
        '
        Me.fndSalesmanCode.CalculationExpression = Nothing
        Me.fndSalesmanCode.FieldCode = Nothing
        Me.fndSalesmanCode.FieldDesc = Nothing
        Me.fndSalesmanCode.FieldMaxLength = 0
        Me.fndSalesmanCode.FieldName = Nothing
        Me.fndSalesmanCode.isCalculatedField = False
        Me.fndSalesmanCode.IsSourceFromTable = False
        Me.fndSalesmanCode.IsSourceFromValueList = False
        Me.fndSalesmanCode.IsUnique = False
        Me.fndSalesmanCode.Location = New System.Drawing.Point(155, 84)
        Me.fndSalesmanCode.MendatroryField = True
        Me.fndSalesmanCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSalesmanCode.MyLinkLable1 = Me.lblTransferNumber
        Me.fndSalesmanCode.MyLinkLable2 = Nothing
        Me.fndSalesmanCode.MyReadOnly = False
        Me.fndSalesmanCode.MyShowMasterFormButton = False
        Me.fndSalesmanCode.Name = "fndSalesmanCode"
        Me.fndSalesmanCode.ReferenceFieldDesc = Nothing
        Me.fndSalesmanCode.ReferenceFieldName = Nothing
        Me.fndSalesmanCode.ReferenceTableName = Nothing
        Me.fndSalesmanCode.Size = New System.Drawing.Size(158, 19)
        Me.fndSalesmanCode.TabIndex = 6
        Me.fndSalesmanCode.Value = ""
        '
        'lblTransferNumber
        '
        Me.lblTransferNumber.FieldName = Nothing
        Me.lblTransferNumber.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransferNumber.Location = New System.Drawing.Point(16, 62)
        Me.lblTransferNumber.Name = "lblTransferNumber"
        Me.lblTransferNumber.Size = New System.Drawing.Size(96, 16)
        Me.lblTransferNumber.TabIndex = 15
        Me.lblTransferNumber.Text = "Load Out Number"
        '
        'btnLoadOutView
        '
        Me.btnLoadOutView.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoadOutView.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoadOutView.Location = New System.Drawing.Point(582, 525)
        Me.btnLoadOutView.Name = "btnLoadOutView"
        Me.btnLoadOutView.Size = New System.Drawing.Size(113, 18)
        Me.btnLoadOutView.TabIndex = 25
        Me.btnLoadOutView.Text = "View Transfer Detail"
        '
        'fndTransferNumber
        '
        Me.fndTransferNumber.CalculationExpression = Nothing
        Me.fndTransferNumber.FieldCode = Nothing
        Me.fndTransferNumber.FieldDesc = Nothing
        Me.fndTransferNumber.FieldMaxLength = 0
        Me.fndTransferNumber.FieldName = Nothing
        Me.fndTransferNumber.isCalculatedField = False
        Me.fndTransferNumber.IsSourceFromTable = False
        Me.fndTransferNumber.IsSourceFromValueList = False
        Me.fndTransferNumber.IsUnique = False
        Me.fndTransferNumber.Location = New System.Drawing.Point(155, 62)
        Me.fndTransferNumber.MendatroryField = True
        Me.fndTransferNumber.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTransferNumber.MyLinkLable1 = Me.lblTransferNumber
        Me.fndTransferNumber.MyLinkLable2 = Nothing
        Me.fndTransferNumber.MyReadOnly = False
        Me.fndTransferNumber.MyShowMasterFormButton = False
        Me.fndTransferNumber.Name = "fndTransferNumber"
        Me.fndTransferNumber.ReferenceFieldDesc = Nothing
        Me.fndTransferNumber.ReferenceFieldName = Nothing
        Me.fndTransferNumber.ReferenceTableName = Nothing
        Me.fndTransferNumber.Size = New System.Drawing.Size(158, 19)
        Me.fndTransferNumber.TabIndex = 4
        Me.fndTransferNumber.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(401, 7)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(91, 21)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 35
        '
        'txtBalanceAmount
        '
        Me.txtBalanceAmount.Location = New System.Drawing.Point(155, 205)
        Me.txtBalanceAmount.Name = "txtBalanceAmount"
        Me.txtBalanceAmount.ReadOnly = True
        Me.txtBalanceAmount.Size = New System.Drawing.Size(157, 20)
        Me.txtBalanceAmount.TabIndex = 16
        Me.txtBalanceAmount.Text = " 0.0"
        Me.txtBalanceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtempty
        '
        Me.txtempty.Location = New System.Drawing.Point(155, 154)
        Me.txtempty.Name = "txtempty"
        Me.txtempty.ReadOnly = True
        Me.txtempty.Size = New System.Drawing.Size(158, 20)
        Me.txtempty.TabIndex = 12
        Me.txtempty.Text = " 0.0"
        Me.txtempty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(16, 156)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(82, 16)
        Me.RadLabel7.TabIndex = 24
        Me.RadLabel7.Text = "Empty Load In "
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(17, 181)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(86, 16)
        Me.RadLabel6.TabIndex = 23
        Me.RadLabel6.Text = "Load In Amount"
        '
        'txtLoadInAmount
        '
        Me.txtLoadInAmount.Location = New System.Drawing.Point(155, 179)
        Me.txtLoadInAmount.Name = "txtLoadInAmount"
        Me.txtLoadInAmount.ReadOnly = True
        Me.txtLoadInAmount.Size = New System.Drawing.Size(157, 20)
        Me.txtLoadInAmount.TabIndex = 14
        Me.txtLoadInAmount.Text = " 0.0"
        Me.txtLoadInAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblComments
        '
        Me.lblComments.FieldName = Nothing
        Me.lblComments.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComments.Location = New System.Drawing.Point(17, 229)
        Me.lblComments.Name = "lblComments"
        Me.lblComments.Size = New System.Drawing.Size(51, 16)
        Me.lblComments.TabIndex = 26
        Me.lblComments.Text = "Remarks"
        '
        'txtComments
        '
        Me.txtComments.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.Location = New System.Drawing.Point(155, 229)
        Me.txtComments.MaxLength = 500
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        '
        '
        '
        Me.txtComments.RootElement.StretchVertically = True
        Me.txtComments.Size = New System.Drawing.Size(582, 35)
        Me.txtComments.TabIndex = 18
        Me.txtComments.Text = " "
        '
        'lblBalanceAmount
        '
        Me.lblBalanceAmount.FieldName = Nothing
        Me.lblBalanceAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalanceAmount.Location = New System.Drawing.Point(17, 204)
        Me.lblBalanceAmount.Name = "lblBalanceAmount"
        Me.lblBalanceAmount.Size = New System.Drawing.Size(129, 16)
        Me.lblBalanceAmount.TabIndex = 25
        Me.lblBalanceAmount.Text = "Balance Amount(S/E)+/-"
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(365, 40)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(61, 16)
        Me.RadLabel5.TabIndex = 18
        Me.RadLabel5.Text = "Vehicle No"
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.Location = New System.Drawing.Point(506, 38)
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.ReadOnly = True
        Me.txtVehicleNo.Size = New System.Drawing.Size(231, 20)
        Me.txtVehicleNo.TabIndex = 3
        Me.txtVehicleNo.Text = " "
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(16, 107)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(57, 16)
        Me.RadLabel2.TabIndex = 19
        Me.RadLabel2.Text = "Route No."
        '
        'txtRouteNo
        '
        Me.txtRouteNo.Location = New System.Drawing.Point(155, 107)
        Me.txtRouteNo.Name = "txtRouteNo"
        Me.txtRouteNo.ReadOnly = True
        Me.txtRouteNo.Size = New System.Drawing.Size(158, 20)
        Me.txtRouteNo.TabIndex = 8
        Me.txtRouteNo.Text = " "
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(17, 40)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(87, 16)
        Me.RadLabel1.TabIndex = 14
        Me.RadLabel1.Text = "Settlement Date"
        '
        'lblRouteDescription
        '
        Me.lblRouteDescription.FieldName = Nothing
        Me.lblRouteDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRouteDescription.Location = New System.Drawing.Point(365, 110)
        Me.lblRouteDescription.Name = "lblRouteDescription"
        Me.lblRouteDescription.Size = New System.Drawing.Size(96, 16)
        Me.lblRouteDescription.TabIndex = 21
        Me.lblRouteDescription.Text = "Route Description"
        '
        'lblSalesman
        '
        Me.lblSalesman.FieldName = Nothing
        Me.lblSalesman.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesman.Location = New System.Drawing.Point(365, 87)
        Me.lblSalesman.Name = "lblSalesman"
        Me.lblSalesman.Size = New System.Drawing.Size(36, 16)
        Me.lblSalesman.TabIndex = 17
        Me.lblSalesman.Text = "Name"
        '
        'txtRoutedescription
        '
        Me.txtRoutedescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoutedescription.Location = New System.Drawing.Point(506, 108)
        Me.txtRoutedescription.Name = "txtRoutedescription"
        Me.txtRoutedescription.ReadOnly = True
        Me.txtRoutedescription.Size = New System.Drawing.Size(231, 18)
        Me.txtRoutedescription.TabIndex = 9
        Me.txtRoutedescription.Text = " "
        '
        'txtSalesman
        '
        Me.txtSalesman.Location = New System.Drawing.Point(506, 85)
        Me.txtSalesman.Name = "txtSalesman"
        Me.txtSalesman.ReadOnly = True
        Me.txtSalesman.Size = New System.Drawing.Size(231, 20)
        Me.txtSalesman.TabIndex = 7
        Me.txtSalesman.Text = " "
        '
        'txtTotalAmount
        '
        Me.txtTotalAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTotalAmount.Location = New System.Drawing.Point(658, 499)
        Me.txtTotalAmount.Name = "txtTotalAmount"
        Me.txtTotalAmount.ReadOnly = True
        Me.txtTotalAmount.Size = New System.Drawing.Size(113, 20)
        Me.txtTotalAmount.TabIndex = 27
        Me.txtTotalAmount.Text = " 0.0"
        Me.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalAmount.FieldName = Nothing
        Me.lblTotalAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalAmount.Location = New System.Drawing.Point(569, 503)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Size = New System.Drawing.Size(74, 16)
        Me.lblTotalAmount.TabIndex = 32
        Me.lblTotalAmount.Text = "Total Amount"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Enabled = False
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(235, 525)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 23
        Me.btnPrint.Text = "Print"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Enabled = False
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(161, 525)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 18)
        Me.btnPost.TabIndex = 22
        Me.btnPost.Text = "Post"
        '
        'fndQuickSettlement
        '
        Me.fndQuickSettlement.FieldName = Nothing
        Me.fndQuickSettlement.Location = New System.Drawing.Point(155, 10)
        Me.fndQuickSettlement.MendatroryField = True
        Me.fndQuickSettlement.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndQuickSettlement.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndQuickSettlement.MyLinkLable1 = Me.RadLabel3
        Me.fndQuickSettlement.MyLinkLable2 = Nothing
        Me.fndQuickSettlement.MyMaxLength = 30
        Me.fndQuickSettlement.MyReadOnly = True
        Me.fndQuickSettlement.Name = "fndQuickSettlement"
        Me.fndQuickSettlement.Size = New System.Drawing.Size(212, 21)
        Me.fndQuickSettlement.TabIndex = 0
        Me.fndQuickSettlement.Value = ""
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(17, 15)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(123, 16)
        Me.RadLabel3.TabIndex = 12
        Me.RadLabel3.Text = "Quick Settlement Code"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(367, 10)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(16, 21)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Enabled = False
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(87, 525)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 21
        Me.btnDelete.Text = "Delete"
        '
        'txtTransferDate
        '
        Me.txtTransferDate.Location = New System.Drawing.Point(506, 61)
        Me.txtTransferDate.Name = "txtTransferDate"
        Me.txtTransferDate.ReadOnly = True
        Me.txtTransferDate.Size = New System.Drawing.Size(231, 20)
        Me.txtTransferDate.TabIndex = 5
        Me.txtTransferDate.Text = " "
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(13, 525)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 20
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(701, 525)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 26
        Me.btnClose.Text = "Close"
        '
        'dgvQuickSettleMent
        '
        Me.dgvQuickSettleMent.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvQuickSettleMent.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvQuickSettleMent.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvQuickSettleMent.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.dgvQuickSettleMent.ForeColor = System.Drawing.Color.Black
        Me.dgvQuickSettleMent.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvQuickSettleMent.Location = New System.Drawing.Point(9, 273)
        '
        '
        '
        Me.dgvQuickSettleMent.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.dgvQuickSettleMent.MasterTemplate.AllowAddNewRow = False
        GridViewTextBoxColumn1.FieldName = "SettleMentCode"
        GridViewTextBoxColumn1.HeaderText = "Settlement Code"
        GridViewTextBoxColumn1.Name = "SettleMentCode"
        GridViewTextBoxColumn1.ReadOnly = True
        GridViewTextBoxColumn1.Width = 167
        GridViewTextBoxColumn2.HeaderText = "Description"
        GridViewTextBoxColumn2.Name = "Description"
        GridViewTextBoxColumn2.ReadOnly = True
        GridViewTextBoxColumn2.Width = 206
        GridViewDecimalColumn1.HeaderText = "Amount"
        GridViewDecimalColumn1.Minimum = New Decimal(New Integer() {0, 0, 0, 0})
        GridViewDecimalColumn1.Name = "Amount1"
        GridViewDecimalColumn1.ShowUpDownButtons = False
        GridViewDecimalColumn1.Step = New Decimal(New Integer() {0, 0, 0, 0})
        GridViewDecimalColumn1.Width = 106
        GridViewTextBoxColumn3.HeaderText = "Remarks"
        GridViewTextBoxColumn3.Name = "Remarks"
        GridViewTextBoxColumn3.Width = 239
        Me.dgvQuickSettleMent.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewDecimalColumn1, GridViewTextBoxColumn3})
        Me.dgvQuickSettleMent.MasterTemplate.EnableGrouping = False
        Me.dgvQuickSettleMent.MasterTemplate.EnableSorting = False
        Me.dgvQuickSettleMent.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.dgvQuickSettleMent.MasterTemplate.ShowHeaderCellButtons = True
        Me.dgvQuickSettleMent.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.dgvQuickSettleMent.MyStopExport = False
        Me.dgvQuickSettleMent.Name = "dgvQuickSettleMent"
        Me.dgvQuickSettleMent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvQuickSettleMent.ShowHeaderCellButtons = True
        Me.dgvQuickSettleMent.Size = New System.Drawing.Size(762, 216)
        Me.dgvQuickSettleMent.TabIndex = 19
        '
        'lblAmount
        '
        Me.lblAmount.FieldName = Nothing
        Me.lblAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmount.Location = New System.Drawing.Point(16, 131)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(95, 16)
        Me.lblAmount.TabIndex = 22
        Me.lblAmount.Text = "Load Out Amount"
        '
        'lblTranserDate
        '
        Me.lblTranserDate.FieldName = Nothing
        Me.lblTranserDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTranserDate.Location = New System.Drawing.Point(364, 64)
        Me.lblTranserDate.Name = "lblTranserDate"
        Me.lblTranserDate.Size = New System.Drawing.Size(83, 16)
        Me.lblTranserDate.TabIndex = 16
        Me.lblTranserDate.Text = "Load Out  Date"
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(155, 131)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.ReadOnly = True
        Me.txtAmount.Size = New System.Drawing.Size(158, 20)
        Me.txtAmount.TabIndex = 10
        Me.txtAmount.Text = " 0.0"
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'FrmQuickSettlement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(781, 549)
        Me.Controls.Add(Me.grp1)
        Me.Name = "FrmQuickSettlement"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = " Quick SettleMent"
        CType(Me.grp1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grp1.ResumeLayout(False)
        Me.grp1.PerformLayout()
        CType(Me.txtQSDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPostfinnancial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNetSaleAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSchemeAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProvisionalSaleAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCashMemo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransferNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnLoadOutView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBalanceAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtempty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLoadInAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblComments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBalanceAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRouteNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRoutedescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalesman, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTransferDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvQuickSettleMent.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvQuickSettleMent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTranserDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grp1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtAmount As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents dgvQuickSettleMent As common.UserControls.MyRadGridView
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents txtTransferDate As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndQuickSettlement As common.UserControls.txtNavigator
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtTotalAmount As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtRoutedescription As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtSalesman As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtRouteNo As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtComments As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtVehicleNo As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtBalanceAmount As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtLoadInAmount As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtempty As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents lblAmount As common.Controls.MyLabel
    Friend WithEvents lblTranserDate As common.Controls.MyLabel
    Friend WithEvents lblTransferNumber As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents lblTotalAmount As common.Controls.MyLabel
    Friend WithEvents lblRouteDescription As common.Controls.MyLabel
    Friend WithEvents lblSalesman As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents lblComments As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents lblBalanceAmount As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents fndTransferNumber As common.UserControls.txtFinder
    Friend WithEvents btnLoadOutView As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndSalesmanCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtCashMemo As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtProvisionalSaleAmt As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtSchemeAmt As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtNetSaleAmount As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents btnPostfinnancial As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtQSDate As common.Controls.MyDateTimePicker
End Class

