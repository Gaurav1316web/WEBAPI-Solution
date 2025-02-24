<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShortSupplyPenalty
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblShortQty = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.lblPenaltyAmt = New common.Controls.MyLabel()
        Me.txtPenaltyRate = New common.MyNumBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.lblRate = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblApplicable = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblPenaltyQty = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblSRNQty = New common.Controls.MyLabel()
        Me.lblRALQty = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.SRN_PI = New common.Controls.MyLabel()
        Me.txtSRN_PI = New common.UserControls.txtFinder()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.txtTenderNo = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.lblBillToLocation = New common.Controls.MyLabel()
        Me.txtItem = New common.UserControls.txtFinder()
        Me.RadLabel18 = New common.Controls.MyLabel()
        Me.lblItem = New common.Controls.MyLabel()
        Me.txtBillToLocation = New common.UserControls.txtFinder()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.txtVendorNo = New common.UserControls.txtFinder()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnAPInvoice = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnReverseAndUnpost = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblShortQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPenaltyAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPenaltyRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApplicable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPenaltyQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSRNQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRALQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SRN_PI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAPInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverseAndUnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblShortQty)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPenaltyAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPenaltyRate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel10)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblApplicable)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPenaltyQty)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSRNQty)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRALQty)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.SRN_PI)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSRN_PI)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel14)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDocNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTenderNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBillToLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtItem)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBillToLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtVendorNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel15)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblItem)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel18)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblVendorName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAPInvoice)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverseAndUnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(935, 388)
        Me.SplitContainer1.SplitterDistance = 356
        Me.SplitContainer1.TabIndex = 0
        '
        'lblShortQty
        '
        Me.lblShortQty.AutoSize = False
        Me.lblShortQty.BorderVisible = True
        Me.lblShortQty.FieldName = Nothing
        Me.lblShortQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShortQty.Location = New System.Drawing.Point(835, 32)
        Me.lblShortQty.Name = "lblShortQty"
        Me.lblShortQty.Size = New System.Drawing.Size(95, 19)
        Me.lblShortQty.TabIndex = 392
        Me.lblShortQty.TextWrap = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(737, 34)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(54, 16)
        Me.MyLabel6.TabIndex = 391
        Me.MyLabel6.Text = "Short Qty"
        '
        'lblPenaltyAmt
        '
        Me.lblPenaltyAmt.AutoSize = False
        Me.lblPenaltyAmt.BorderVisible = True
        Me.lblPenaltyAmt.FieldName = Nothing
        Me.lblPenaltyAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPenaltyAmt.Location = New System.Drawing.Point(835, 96)
        Me.lblPenaltyAmt.Name = "lblPenaltyAmt"
        Me.lblPenaltyAmt.Size = New System.Drawing.Size(95, 19)
        Me.lblPenaltyAmt.TabIndex = 390
        Me.lblPenaltyAmt.TextWrap = False
        '
        'txtPenaltyRate
        '
        Me.txtPenaltyRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtPenaltyRate.CalculationExpression = Nothing
        Me.txtPenaltyRate.DecimalPlaces = 0
        Me.txtPenaltyRate.FieldCode = Nothing
        Me.txtPenaltyRate.FieldDesc = Nothing
        Me.txtPenaltyRate.FieldMaxLength = 0
        Me.txtPenaltyRate.FieldName = Nothing
        Me.txtPenaltyRate.isCalculatedField = False
        Me.txtPenaltyRate.IsSourceFromTable = False
        Me.txtPenaltyRate.IsSourceFromValueList = False
        Me.txtPenaltyRate.IsUnique = False
        Me.txtPenaltyRate.Location = New System.Drawing.Point(833, 74)
        Me.txtPenaltyRate.MendatroryField = False
        Me.txtPenaltyRate.MyLinkLable1 = Nothing
        Me.txtPenaltyRate.MyLinkLable2 = Nothing
        Me.txtPenaltyRate.Name = "txtPenaltyRate"
        Me.txtPenaltyRate.ReferenceFieldDesc = Nothing
        Me.txtPenaltyRate.ReferenceFieldName = Nothing
        Me.txtPenaltyRate.ReferenceTableName = Nothing
        Me.txtPenaltyRate.Size = New System.Drawing.Size(97, 20)
        Me.txtPenaltyRate.TabIndex = 388
        Me.txtPenaltyRate.Text = "0"
        Me.txtPenaltyRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPenaltyRate.Value = 0R
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(738, 98)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel10.TabIndex = 389
        Me.MyLabel10.Text = "Penalty Amount"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(737, 76)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(92, 16)
        Me.MyLabel9.TabIndex = 387
        Me.MyLabel9.Text = "Penalty Rate(%) "
        '
        'lblRate
        '
        Me.lblRate.AutoSize = False
        Me.lblRate.BorderVisible = True
        Me.lblRate.FieldName = Nothing
        Me.lblRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRate.Location = New System.Drawing.Point(835, 53)
        Me.lblRate.Name = "lblRate"
        Me.lblRate.Size = New System.Drawing.Size(95, 19)
        Me.lblRate.TabIndex = 386
        Me.lblRate.TextWrap = False
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(737, 55)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel7.TabIndex = 385
        Me.MyLabel7.Text = "Rate"
        '
        'lblApplicable
        '
        Me.lblApplicable.AutoSize = False
        Me.lblApplicable.BorderVisible = True
        Me.lblApplicable.FieldName = Nothing
        Me.lblApplicable.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApplicable.Location = New System.Drawing.Point(681, 97)
        Me.lblApplicable.Name = "lblApplicable"
        Me.lblApplicable.Size = New System.Drawing.Size(52, 19)
        Me.lblApplicable.TabIndex = 384
        Me.lblApplicable.TextWrap = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(532, 98)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(145, 16)
        Me.MyLabel5.TabIndex = 383
        Me.MyLabel5.Text = "Penalty Applicable to be(%)"
        '
        'lblPenaltyQty
        '
        Me.lblPenaltyQty.AutoSize = False
        Me.lblPenaltyQty.BorderVisible = True
        Me.lblPenaltyQty.FieldName = Nothing
        Me.lblPenaltyQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPenaltyQty.Location = New System.Drawing.Point(633, 76)
        Me.lblPenaltyQty.Name = "lblPenaltyQty"
        Me.lblPenaltyQty.Size = New System.Drawing.Size(100, 19)
        Me.lblPenaltyQty.TabIndex = 382
        Me.lblPenaltyQty.TextWrap = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(532, 76)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel3.TabIndex = 381
        Me.MyLabel3.Text = "Short/Excess Qty"
        '
        'lblSRNQty
        '
        Me.lblSRNQty.AutoSize = False
        Me.lblSRNQty.BorderVisible = True
        Me.lblSRNQty.FieldName = Nothing
        Me.lblSRNQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSRNQty.Location = New System.Drawing.Point(633, 55)
        Me.lblSRNQty.Name = "lblSRNQty"
        Me.lblSRNQty.Size = New System.Drawing.Size(100, 19)
        Me.lblSRNQty.TabIndex = 380
        Me.lblSRNQty.TextWrap = False
        '
        'lblRALQty
        '
        Me.lblRALQty.AutoSize = False
        Me.lblRALQty.BorderVisible = True
        Me.lblRALQty.FieldName = Nothing
        Me.lblRALQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRALQty.Location = New System.Drawing.Point(633, 34)
        Me.lblRALQty.Name = "lblRALQty"
        Me.lblRALQty.Size = New System.Drawing.Size(100, 19)
        Me.lblRALQty.TabIndex = 379
        Me.lblRALQty.TextWrap = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(532, 55)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel4.TabIndex = 195
        Me.MyLabel4.Text = "SRN Qty"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(532, 34)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel1.TabIndex = 193
        Me.MyLabel1.Text = "RAL Qty"
        '
        'SRN_PI
        '
        Me.SRN_PI.FieldName = Nothing
        Me.SRN_PI.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SRN_PI.Location = New System.Drawing.Point(532, 14)
        Me.SRN_PI.Name = "SRN_PI"
        Me.SRN_PI.Size = New System.Drawing.Size(34, 16)
        Me.SRN_PI.TabIndex = 192
        Me.SRN_PI.Text = "PI No"
        '
        'txtSRN_PI
        '
        Me.txtSRN_PI.CalculationExpression = Nothing
        Me.txtSRN_PI.FieldCode = Nothing
        Me.txtSRN_PI.FieldDesc = Nothing
        Me.txtSRN_PI.FieldMaxLength = 0
        Me.txtSRN_PI.FieldName = Nothing
        Me.txtSRN_PI.isCalculatedField = False
        Me.txtSRN_PI.IsSourceFromTable = False
        Me.txtSRN_PI.IsSourceFromValueList = False
        Me.txtSRN_PI.IsUnique = False
        Me.txtSRN_PI.Location = New System.Drawing.Point(582, 12)
        Me.txtSRN_PI.MendatroryField = False
        Me.txtSRN_PI.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSRN_PI.MyLinkLable1 = Nothing
        Me.txtSRN_PI.MyLinkLable2 = Nothing
        Me.txtSRN_PI.MyReadOnly = False
        Me.txtSRN_PI.MyShowMasterFormButton = False
        Me.txtSRN_PI.Name = "txtSRN_PI"
        Me.txtSRN_PI.ReferenceFieldDesc = Nothing
        Me.txtSRN_PI.ReferenceFieldName = Nothing
        Me.txtSRN_PI.ReferenceTableName = Nothing
        Me.txtSRN_PI.Size = New System.Drawing.Size(204, 18)
        Me.txtSRN_PI.TabIndex = 191
        Me.txtSRN_PI.Value = ""
        '
        'btnGo
        '
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(532, 118)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(63, 21)
        Me.btnGo.TabIndex = 190
        Me.btnGo.Text = ">>>"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(12, 12)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel1.TabIndex = 189
        Me.RadLabel1.Text = "Document No"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(402, 11)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(126, 18)
        Me.txtDate.TabIndex = 173
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(367, 12)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 181
        Me.RadLabel4.Text = "Date"
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(12, 55)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel14.TabIndex = 188
        Me.MyLabel14.Text = "RAL No"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(91, 11)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 30
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 19)
        Me.txtDocNo.TabIndex = 172
        Me.txtDocNo.Value = ""
        '
        'txtTenderNo
        '
        Me.txtTenderNo.CalculationExpression = Nothing
        Me.txtTenderNo.FieldCode = Nothing
        Me.txtTenderNo.FieldDesc = Nothing
        Me.txtTenderNo.FieldMaxLength = 0
        Me.txtTenderNo.FieldName = Nothing
        Me.txtTenderNo.isCalculatedField = False
        Me.txtTenderNo.IsSourceFromTable = False
        Me.txtTenderNo.IsSourceFromValueList = False
        Me.txtTenderNo.IsUnique = False
        Me.txtTenderNo.Location = New System.Drawing.Point(91, 54)
        Me.txtTenderNo.MendatroryField = True
        Me.txtTenderNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTenderNo.MyLinkLable1 = Me.MyLabel14
        Me.txtTenderNo.MyLinkLable2 = Nothing
        Me.txtTenderNo.MyReadOnly = False
        Me.txtTenderNo.MyShowMasterFormButton = False
        Me.txtTenderNo.Name = "txtTenderNo"
        Me.txtTenderNo.ReferenceFieldDesc = Nothing
        Me.txtTenderNo.ReferenceFieldName = Nothing
        Me.txtTenderNo.ReferenceTableName = Nothing
        Me.txtTenderNo.Size = New System.Drawing.Size(211, 19)
        Me.txtTenderNo.TabIndex = 187
        Me.txtTenderNo.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(826, 13)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(76, 19)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 182
        '
        'lblBillToLocation
        '
        Me.lblBillToLocation.AutoSize = False
        Me.lblBillToLocation.BorderVisible = True
        Me.lblBillToLocation.FieldName = Nothing
        Me.lblBillToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillToLocation.Location = New System.Drawing.Point(304, 33)
        Me.lblBillToLocation.Name = "lblBillToLocation"
        Me.lblBillToLocation.Size = New System.Drawing.Size(223, 18)
        Me.lblBillToLocation.TabIndex = 178
        Me.lblBillToLocation.TextWrap = False
        '
        'txtItem
        '
        Me.txtItem.CalculationExpression = Nothing
        Me.txtItem.FieldCode = Nothing
        Me.txtItem.FieldDesc = Nothing
        Me.txtItem.FieldMaxLength = 0
        Me.txtItem.FieldName = Nothing
        Me.txtItem.isCalculatedField = False
        Me.txtItem.IsSourceFromTable = False
        Me.txtItem.IsSourceFromValueList = False
        Me.txtItem.IsUnique = False
        Me.txtItem.Location = New System.Drawing.Point(91, 97)
        Me.txtItem.MendatroryField = True
        Me.txtItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.MyLinkLable1 = Me.RadLabel18
        Me.txtItem.MyLinkLable2 = Me.lblItem
        Me.txtItem.MyReadOnly = False
        Me.txtItem.MyShowMasterFormButton = False
        Me.txtItem.Name = "txtItem"
        Me.txtItem.ReferenceFieldDesc = Nothing
        Me.txtItem.ReferenceFieldName = Nothing
        Me.txtItem.ReferenceTableName = Nothing
        Me.txtItem.Size = New System.Drawing.Size(211, 18)
        Me.txtItem.TabIndex = 177
        Me.txtItem.Value = ""
        '
        'RadLabel18
        '
        Me.RadLabel18.FieldName = Nothing
        Me.RadLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel18.Location = New System.Drawing.Point(12, 98)
        Me.RadLabel18.Name = "RadLabel18"
        Me.RadLabel18.Size = New System.Drawing.Size(28, 16)
        Me.RadLabel18.TabIndex = 186
        Me.RadLabel18.Text = "Item"
        '
        'lblItem
        '
        Me.lblItem.AutoSize = False
        Me.lblItem.BorderVisible = True
        Me.lblItem.FieldName = Nothing
        Me.lblItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItem.Location = New System.Drawing.Point(304, 97)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(223, 19)
        Me.lblItem.TabIndex = 179
        Me.lblItem.TextWrap = False
        '
        'txtBillToLocation
        '
        Me.txtBillToLocation.CalculationExpression = Nothing
        Me.txtBillToLocation.FieldCode = Nothing
        Me.txtBillToLocation.FieldDesc = Nothing
        Me.txtBillToLocation.FieldMaxLength = 0
        Me.txtBillToLocation.FieldName = Nothing
        Me.txtBillToLocation.isCalculatedField = False
        Me.txtBillToLocation.IsSourceFromTable = False
        Me.txtBillToLocation.IsSourceFromValueList = False
        Me.txtBillToLocation.IsUnique = False
        Me.txtBillToLocation.Location = New System.Drawing.Point(91, 33)
        Me.txtBillToLocation.MendatroryField = True
        Me.txtBillToLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillToLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtBillToLocation.MyLinkLable2 = Me.lblBillToLocation
        Me.txtBillToLocation.MyReadOnly = False
        Me.txtBillToLocation.MyShowMasterFormButton = False
        Me.txtBillToLocation.Name = "txtBillToLocation"
        Me.txtBillToLocation.ReferenceFieldDesc = Nothing
        Me.txtBillToLocation.ReferenceFieldName = Nothing
        Me.txtBillToLocation.ReferenceTableName = Nothing
        Me.txtBillToLocation.Size = New System.Drawing.Size(211, 18)
        Me.txtBillToLocation.TabIndex = 176
        Me.txtBillToLocation.Value = ""
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(12, 34)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 184
        Me.RadLabel15.Text = "Location"
        '
        'txtVendorNo
        '
        Me.txtVendorNo.CalculationExpression = Nothing
        Me.txtVendorNo.FieldCode = Nothing
        Me.txtVendorNo.FieldDesc = Nothing
        Me.txtVendorNo.FieldMaxLength = 0
        Me.txtVendorNo.FieldName = Nothing
        Me.txtVendorNo.isCalculatedField = False
        Me.txtVendorNo.IsSourceFromTable = False
        Me.txtVendorNo.IsSourceFromValueList = False
        Me.txtVendorNo.IsUnique = False
        Me.txtVendorNo.Location = New System.Drawing.Point(91, 76)
        Me.txtVendorNo.MendatroryField = True
        Me.txtVendorNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorNo.MyLinkLable1 = Me.RadLabel2
        Me.txtVendorNo.MyLinkLable2 = Me.lblVendorName
        Me.txtVendorNo.MyReadOnly = False
        Me.txtVendorNo.MyShowMasterFormButton = False
        Me.txtVendorNo.Name = "txtVendorNo"
        Me.txtVendorNo.ReferenceFieldDesc = Nothing
        Me.txtVendorNo.ReferenceFieldName = Nothing
        Me.txtVendorNo.ReferenceTableName = Nothing
        Me.txtVendorNo.Size = New System.Drawing.Size(211, 18)
        Me.txtVendorNo.TabIndex = 174
        Me.txtVendorNo.Value = ""
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(12, 77)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(43, 16)
        Me.RadLabel2.TabIndex = 183
        Me.RadLabel2.Text = "Vendor"
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorName.Location = New System.Drawing.Point(304, 76)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(223, 18)
        Me.lblVendorName.TabIndex = 175
        Me.lblVendorName.TextWrap = False
        '
        'txtRemarks
        '
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(91, 118)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.RadLabel6
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(437, 18)
        Me.txtRemarks.TabIndex = 180
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(12, 119)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel6.TabIndex = 185
        Me.RadLabel6.Text = "Remarks"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPPurchase.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(343, 11)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 19)
        Me.btnAddNew.TabIndex = 171
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
        Me.RadGroupBox2.Location = New System.Drawing.Point(6, 141)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(926, 213)
        Me.RadGroupBox2.TabIndex = 20
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
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(906, 183)
        Me.gv1.TabIndex = 0
        Me.gv1.VarID = ""
        '
        'btnAPInvoice
        '
        Me.btnAPInvoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAPInvoice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAPInvoice.Location = New System.Drawing.Point(717, 3)
        Me.btnAPInvoice.Name = "btnAPInvoice"
        Me.btnAPInvoice.Size = New System.Drawing.Size(102, 22)
        Me.btnAPInvoice.TabIndex = 16
        Me.btnAPInvoice.Text = "AP Invoice Entry"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(855, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 15
        Me.btnClose.Text = "Close"
        '
        'btnReverseAndUnpost
        '
        Me.btnReverseAndUnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverseAndUnpost.Location = New System.Drawing.Point(301, 4)
        Me.btnReverseAndUnpost.Name = "btnReverseAndUnpost"
        Me.btnReverseAndUnpost.Size = New System.Drawing.Size(125, 22)
        Me.btnReverseAndUnpost.TabIndex = 12
        Me.btnReverseAndUnpost.Text = "Reverse And Unpost"
        Me.btnReverseAndUnpost.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(230, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 11
        Me.btnPrint.Text = "Print"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(159, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 10
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(88, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 9
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(17, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 8
        Me.btnSave.Text = "Save"
        '
        'btnHistory
        '
        Me.btnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.Location = New System.Drawing.Point(427, 3)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(55, 22)
        Me.btnHistory.TabIndex = 17
        Me.btnHistory.Text = "History"
        Me.btnHistory.Visible = False
        '
        'frmShortSupplyPenalty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(935, 388)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmShortSupplyPenalty"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmShortSupplyPenalty"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblShortQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPenaltyAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPenaltyRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApplicable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPenaltyQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSRNQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRALQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SRN_PI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAPInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverseAndUnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnReverseAndUnpost As RadButton
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents btnSave As RadButton
    Friend WithEvents SRN_PI As common.Controls.MyLabel
    Friend WithEvents txtSRN_PI As common.UserControls.txtFinder
    Friend WithEvents btnGo As RadButton
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents txtTenderNo As common.UserControls.txtFinder
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents lblBillToLocation As common.Controls.MyLabel
    Friend WithEvents txtItem As common.UserControls.txtFinder
    Friend WithEvents RadLabel18 As common.Controls.MyLabel
    Friend WithEvents lblItem As common.Controls.MyLabel
    Friend WithEvents txtBillToLocation As common.UserControls.txtFinder
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents txtVendorNo As common.UserControls.txtFinder
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblSRNQty As common.Controls.MyLabel
    Friend WithEvents lblRALQty As common.Controls.MyLabel
    Friend WithEvents lblPenaltyQty As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents lblRate As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents lblApplicable As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtPenaltyRate As common.MyNumBox
    Friend WithEvents lblPenaltyAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents lblShortQty As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnAPInvoice As RadButton
    Friend WithEvents btnHistory As RadButton
End Class
