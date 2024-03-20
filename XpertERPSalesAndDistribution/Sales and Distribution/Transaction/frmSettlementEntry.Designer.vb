<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSettlementEntry
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.btnEmptySettlement = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.lblunappliedamt = New common.Controls.MyLabel()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.txtSettlementAmt = New common.Controls.MyTextBox()
        Me.txtAppliedAmt = New common.Controls.MyTextBox()
        Me.lblTotPayment = New common.Controls.MyLabel()
        Me.txtBalanceAmt = New common.Controls.MyTextBox()
        Me.txtLocationDesc = New common.Controls.MyTextBox()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtRouteDesc = New common.Controls.MyTextBox()
        Me.fndRouteNo = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtSalesManName = New common.Controls.MyTextBox()
        Me.fndSalesMan = New common.UserControls.txtFinder()
        Me.lblSalesman = New common.Controls.MyLabel()
        Me.fndloadno = New common.UserControls.txtFinder()
        Me.rdlblloadoutno = New common.Controls.MyLabel()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.fndSettlementEntry = New common.UserControls.txtNavigator()
        Me.lblOrderNo = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.lblOrderDate = New common.Controls.MyLabel()
        Me.dtSettlement = New common.Controls.MyDateTimePicker()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        CType(Me.btnEmptySettlement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblunappliedamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSettlementAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAppliedAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBalanceAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalesManName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblloadoutno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOrderDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtSettlement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnEmptySettlement
        '
        Me.btnEmptySettlement.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEmptySettlement.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEmptySettlement.Location = New System.Drawing.Point(342, 3)
        Me.btnEmptySettlement.Name = "btnEmptySettlement"
        Me.btnEmptySettlement.Size = New System.Drawing.Size(127, 18)
        Me.btnEmptySettlement.TabIndex = 4
        Me.btnEmptySettlement.Text = "Settlement For Empty"
        Me.btnEmptySettlement.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(657, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(82, 18)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(257, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(82, 18)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(756, 314)
        Me.gv1.TabIndex = 0
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(172, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(82, 18)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(521, 147)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel3.TabIndex = 22
        Me.MyLabel3.Text = "Balance Amt."
        Me.MyLabel3.Visible = False
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(87, 3)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(82, 18)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'lblunappliedamt
        '
        Me.lblunappliedamt.FieldName = Nothing
        Me.lblunappliedamt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblunappliedamt.Location = New System.Drawing.Point(16, 146)
        Me.lblunappliedamt.Name = "lblunappliedamt"
        Me.lblunappliedamt.Size = New System.Drawing.Size(87, 16)
        Me.lblunappliedamt.TabIndex = 20
        Me.lblunappliedamt.Text = "Settlement Amt."
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(2, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(82, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'txtSettlementAmt
        '
        Me.txtSettlementAmt.CalculationExpression = Nothing
        Me.txtSettlementAmt.Enabled = False
        Me.txtSettlementAmt.FieldCode = Nothing
        Me.txtSettlementAmt.FieldDesc = Nothing
        Me.txtSettlementAmt.FieldMaxLength = 0
        Me.txtSettlementAmt.FieldName = Nothing
        Me.txtSettlementAmt.isCalculatedField = False
        Me.txtSettlementAmt.IsSourceFromTable = False
        Me.txtSettlementAmt.IsSourceFromValueList = False
        Me.txtSettlementAmt.IsUnique = False
        Me.txtSettlementAmt.Location = New System.Drawing.Point(106, 143)
        Me.txtSettlementAmt.MendatroryField = False
        Me.txtSettlementAmt.MyLinkLable1 = Nothing
        Me.txtSettlementAmt.MyLinkLable2 = Nothing
        Me.txtSettlementAmt.Name = "txtSettlementAmt"
        Me.txtSettlementAmt.ReferenceFieldDesc = Nothing
        Me.txtSettlementAmt.ReferenceFieldName = Nothing
        Me.txtSettlementAmt.ReferenceTableName = Nothing
        Me.txtSettlementAmt.Size = New System.Drawing.Size(159, 20)
        Me.txtSettlementAmt.TabIndex = 11
        Me.txtSettlementAmt.Text = "0"
        Me.txtSettlementAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAppliedAmt
        '
        Me.txtAppliedAmt.CalculationExpression = Nothing
        Me.txtAppliedAmt.Enabled = False
        Me.txtAppliedAmt.FieldCode = Nothing
        Me.txtAppliedAmt.FieldDesc = Nothing
        Me.txtAppliedAmt.FieldMaxLength = 0
        Me.txtAppliedAmt.FieldName = Nothing
        Me.txtAppliedAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAppliedAmt.isCalculatedField = False
        Me.txtAppliedAmt.IsSourceFromTable = False
        Me.txtAppliedAmt.IsSourceFromValueList = False
        Me.txtAppliedAmt.IsUnique = False
        Me.txtAppliedAmt.Location = New System.Drawing.Point(360, 145)
        Me.txtAppliedAmt.MaxLength = 18
        Me.txtAppliedAmt.MendatroryField = False
        Me.txtAppliedAmt.Multiline = True
        Me.txtAppliedAmt.MyLinkLable1 = Nothing
        Me.txtAppliedAmt.MyLinkLable2 = Nothing
        Me.txtAppliedAmt.Name = "txtAppliedAmt"
        Me.txtAppliedAmt.ReadOnly = True
        Me.txtAppliedAmt.ReferenceFieldDesc = Nothing
        Me.txtAppliedAmt.ReferenceFieldName = Nothing
        Me.txtAppliedAmt.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtAppliedAmt.RootElement.StretchVertically = True
        Me.txtAppliedAmt.Size = New System.Drawing.Size(148, 20)
        Me.txtAppliedAmt.TabIndex = 12
        Me.txtAppliedAmt.Text = "0"
        Me.txtAppliedAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAppliedAmt.Visible = False
        '
        'lblTotPayment
        '
        Me.lblTotPayment.FieldName = Nothing
        Me.lblTotPayment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotPayment.Location = New System.Drawing.Point(286, 147)
        Me.lblTotPayment.Name = "lblTotPayment"
        Me.lblTotPayment.Size = New System.Drawing.Size(70, 16)
        Me.lblTotPayment.TabIndex = 21
        Me.lblTotPayment.Text = "Applied Amt."
        Me.lblTotPayment.Visible = False
        '
        'txtBalanceAmt
        '
        Me.txtBalanceAmt.CalculationExpression = Nothing
        Me.txtBalanceAmt.Enabled = False
        Me.txtBalanceAmt.FieldCode = Nothing
        Me.txtBalanceAmt.FieldDesc = Nothing
        Me.txtBalanceAmt.FieldMaxLength = 0
        Me.txtBalanceAmt.FieldName = Nothing
        Me.txtBalanceAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalanceAmt.isCalculatedField = False
        Me.txtBalanceAmt.IsSourceFromTable = False
        Me.txtBalanceAmt.IsSourceFromValueList = False
        Me.txtBalanceAmt.IsUnique = False
        Me.txtBalanceAmt.Location = New System.Drawing.Point(600, 145)
        Me.txtBalanceAmt.MaxLength = 18
        Me.txtBalanceAmt.MendatroryField = False
        Me.txtBalanceAmt.Multiline = True
        Me.txtBalanceAmt.MyLinkLable1 = Nothing
        Me.txtBalanceAmt.MyLinkLable2 = Nothing
        Me.txtBalanceAmt.Name = "txtBalanceAmt"
        Me.txtBalanceAmt.ReferenceFieldDesc = Nothing
        Me.txtBalanceAmt.ReferenceFieldName = Nothing
        Me.txtBalanceAmt.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtBalanceAmt.RootElement.StretchVertically = True
        Me.txtBalanceAmt.Size = New System.Drawing.Size(143, 20)
        Me.txtBalanceAmt.TabIndex = 13
        Me.txtBalanceAmt.Text = "0"
        Me.txtBalanceAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBalanceAmt.Visible = False
        '
        'txtLocationDesc
        '
        Me.txtLocationDesc.CalculationExpression = Nothing
        Me.txtLocationDesc.Enabled = False
        Me.txtLocationDesc.FieldCode = Nothing
        Me.txtLocationDesc.FieldDesc = Nothing
        Me.txtLocationDesc.FieldMaxLength = 0
        Me.txtLocationDesc.FieldName = Nothing
        Me.txtLocationDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationDesc.isCalculatedField = False
        Me.txtLocationDesc.IsSourceFromTable = False
        Me.txtLocationDesc.IsSourceFromValueList = False
        Me.txtLocationDesc.IsUnique = False
        Me.txtLocationDesc.Location = New System.Drawing.Point(287, 121)
        Me.txtLocationDesc.MaxLength = 200
        Me.txtLocationDesc.MendatroryField = False
        Me.txtLocationDesc.MyLinkLable1 = Me.lblDescription
        Me.txtLocationDesc.MyLinkLable2 = Nothing
        Me.txtLocationDesc.Name = "txtLocationDesc"
        Me.txtLocationDesc.ReferenceFieldDesc = Nothing
        Me.txtLocationDesc.ReferenceFieldName = Nothing
        Me.txtLocationDesc.ReferenceTableName = Nothing
        Me.txtLocationDesc.Size = New System.Drawing.Size(456, 18)
        Me.txtLocationDesc.TabIndex = 10
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Location = New System.Drawing.Point(14, 28)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 18)
        Me.lblDescription.TabIndex = 15
        Me.lblDescription.Text = "Description"
        '
        'fndLocation
        '
        Me.fndLocation.CalculationExpression = Nothing
        Me.fndLocation.Enabled = False
        Me.fndLocation.FieldCode = Nothing
        Me.fndLocation.FieldDesc = Nothing
        Me.fndLocation.FieldMaxLength = 0
        Me.fndLocation.FieldName = Nothing
        Me.fndLocation.isCalculatedField = False
        Me.fndLocation.IsSourceFromTable = False
        Me.fndLocation.IsSourceFromValueList = False
        Me.fndLocation.IsUnique = False
        Me.fndLocation.Location = New System.Drawing.Point(107, 121)
        Me.fndLocation.MendatroryField = False
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.MyLabel2
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(174, 19)
        Me.fndLocation.TabIndex = 9
        Me.fndLocation.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(16, 122)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel2.TabIndex = 19
        Me.MyLabel2.Text = "Location"
        '
        'txtRouteDesc
        '
        Me.txtRouteDesc.CalculationExpression = Nothing
        Me.txtRouteDesc.Enabled = False
        Me.txtRouteDesc.FieldCode = Nothing
        Me.txtRouteDesc.FieldDesc = Nothing
        Me.txtRouteDesc.FieldMaxLength = 0
        Me.txtRouteDesc.FieldName = Nothing
        Me.txtRouteDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteDesc.isCalculatedField = False
        Me.txtRouteDesc.IsSourceFromTable = False
        Me.txtRouteDesc.IsSourceFromValueList = False
        Me.txtRouteDesc.IsUnique = False
        Me.txtRouteDesc.Location = New System.Drawing.Point(287, 97)
        Me.txtRouteDesc.MaxLength = 200
        Me.txtRouteDesc.MendatroryField = False
        Me.txtRouteDesc.MyLinkLable1 = Me.lblDescription
        Me.txtRouteDesc.MyLinkLable2 = Nothing
        Me.txtRouteDesc.Name = "txtRouteDesc"
        Me.txtRouteDesc.ReferenceFieldDesc = Nothing
        Me.txtRouteDesc.ReferenceFieldName = Nothing
        Me.txtRouteDesc.ReferenceTableName = Nothing
        Me.txtRouteDesc.Size = New System.Drawing.Size(456, 18)
        Me.txtRouteDesc.TabIndex = 8
        '
        'fndRouteNo
        '
        Me.fndRouteNo.CalculationExpression = Nothing
        Me.fndRouteNo.Enabled = False
        Me.fndRouteNo.FieldCode = Nothing
        Me.fndRouteNo.FieldDesc = Nothing
        Me.fndRouteNo.FieldMaxLength = 0
        Me.fndRouteNo.FieldName = Nothing
        Me.fndRouteNo.isCalculatedField = False
        Me.fndRouteNo.IsSourceFromTable = False
        Me.fndRouteNo.IsSourceFromValueList = False
        Me.fndRouteNo.IsUnique = False
        Me.fndRouteNo.Location = New System.Drawing.Point(107, 97)
        Me.fndRouteNo.MendatroryField = False
        Me.fndRouteNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRouteNo.MyLinkLable1 = Me.MyLabel1
        Me.fndRouteNo.MyLinkLable2 = Nothing
        Me.fndRouteNo.MyReadOnly = False
        Me.fndRouteNo.MyShowMasterFormButton = False
        Me.fndRouteNo.Name = "fndRouteNo"
        Me.fndRouteNo.ReferenceFieldDesc = Nothing
        Me.fndRouteNo.ReferenceFieldName = Nothing
        Me.fndRouteNo.ReferenceTableName = Nothing
        Me.fndRouteNo.Size = New System.Drawing.Size(174, 19)
        Me.fndRouteNo.TabIndex = 7
        Me.fndRouteNo.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(16, 98)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(56, 18)
        Me.MyLabel1.TabIndex = 18
        Me.MyLabel1.Text = "Route No."
        '
        'txtSalesManName
        '
        Me.txtSalesManName.CalculationExpression = Nothing
        Me.txtSalesManName.Enabled = False
        Me.txtSalesManName.FieldCode = Nothing
        Me.txtSalesManName.FieldDesc = Nothing
        Me.txtSalesManName.FieldMaxLength = 0
        Me.txtSalesManName.FieldName = Nothing
        Me.txtSalesManName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesManName.isCalculatedField = False
        Me.txtSalesManName.IsSourceFromTable = False
        Me.txtSalesManName.IsSourceFromValueList = False
        Me.txtSalesManName.IsUnique = False
        Me.txtSalesManName.Location = New System.Drawing.Point(287, 73)
        Me.txtSalesManName.MaxLength = 200
        Me.txtSalesManName.MendatroryField = False
        Me.txtSalesManName.MyLinkLable1 = Me.lblDescription
        Me.txtSalesManName.MyLinkLable2 = Nothing
        Me.txtSalesManName.Name = "txtSalesManName"
        Me.txtSalesManName.ReferenceFieldDesc = Nothing
        Me.txtSalesManName.ReferenceFieldName = Nothing
        Me.txtSalesManName.ReferenceTableName = Nothing
        Me.txtSalesManName.Size = New System.Drawing.Size(456, 18)
        Me.txtSalesManName.TabIndex = 6
        '
        'fndSalesMan
        '
        Me.fndSalesMan.CalculationExpression = Nothing
        Me.fndSalesMan.Enabled = False
        Me.fndSalesMan.FieldCode = Nothing
        Me.fndSalesMan.FieldDesc = Nothing
        Me.fndSalesMan.FieldMaxLength = 0
        Me.fndSalesMan.FieldName = Nothing
        Me.fndSalesMan.isCalculatedField = False
        Me.fndSalesMan.IsSourceFromTable = False
        Me.fndSalesMan.IsSourceFromValueList = False
        Me.fndSalesMan.IsUnique = False
        Me.fndSalesMan.Location = New System.Drawing.Point(107, 73)
        Me.fndSalesMan.MendatroryField = False
        Me.fndSalesMan.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSalesMan.MyLinkLable1 = Me.lblSalesman
        Me.fndSalesMan.MyLinkLable2 = Nothing
        Me.fndSalesMan.MyReadOnly = False
        Me.fndSalesMan.MyShowMasterFormButton = False
        Me.fndSalesMan.Name = "fndSalesMan"
        Me.fndSalesMan.ReferenceFieldDesc = Nothing
        Me.fndSalesMan.ReferenceFieldName = Nothing
        Me.fndSalesMan.ReferenceTableName = Nothing
        Me.fndSalesMan.Size = New System.Drawing.Size(174, 19)
        Me.fndSalesMan.TabIndex = 5
        Me.fndSalesMan.Value = ""
        '
        'lblSalesman
        '
        Me.lblSalesman.FieldName = Nothing
        Me.lblSalesman.Location = New System.Drawing.Point(16, 74)
        Me.lblSalesman.Name = "lblSalesman"
        Me.lblSalesman.Size = New System.Drawing.Size(83, 18)
        Me.lblSalesman.TabIndex = 17
        Me.lblSalesman.Text = "SalesMan Code"
        '
        'fndloadno
        '
        Me.fndloadno.CalculationExpression = Nothing
        Me.fndloadno.FieldCode = Nothing
        Me.fndloadno.FieldDesc = Nothing
        Me.fndloadno.FieldMaxLength = 0
        Me.fndloadno.FieldName = Nothing
        Me.fndloadno.isCalculatedField = False
        Me.fndloadno.IsSourceFromTable = False
        Me.fndloadno.IsSourceFromValueList = False
        Me.fndloadno.IsUnique = False
        Me.fndloadno.Location = New System.Drawing.Point(107, 51)
        Me.fndloadno.MendatroryField = True
        Me.fndloadno.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndloadno.MyLinkLable1 = Nothing
        Me.fndloadno.MyLinkLable2 = Nothing
        Me.fndloadno.MyReadOnly = False
        Me.fndloadno.MyShowMasterFormButton = False
        Me.fndloadno.Name = "fndloadno"
        Me.fndloadno.ReferenceFieldDesc = Nothing
        Me.fndloadno.ReferenceFieldName = Nothing
        Me.fndloadno.ReferenceTableName = Nothing
        Me.fndloadno.Size = New System.Drawing.Size(174, 20)
        Me.fndloadno.TabIndex = 4
        Me.fndloadno.Value = ""
        '
        'rdlblloadoutno
        '
        Me.rdlblloadoutno.FieldName = Nothing
        Me.rdlblloadoutno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblloadoutno.Location = New System.Drawing.Point(16, 52)
        Me.rdlblloadoutno.Name = "rdlblloadoutno"
        Me.rdlblloadoutno.Size = New System.Drawing.Size(70, 16)
        Me.rdlblloadoutno.TabIndex = 16
        Me.rdlblloadoutno.Text = "Load Out No"
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(107, 28)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.lblDescription
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(636, 18)
        Me.txtDesc.TabIndex = 3
        '
        'fndSettlementEntry
        '
        Me.fndSettlementEntry.FieldName = Nothing
        Me.fndSettlementEntry.Location = New System.Drawing.Point(107, 2)
        Me.fndSettlementEntry.MendatroryField = True
        Me.fndSettlementEntry.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndSettlementEntry.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndSettlementEntry.MyLinkLable1 = Me.lblOrderNo
        Me.fndSettlementEntry.MyLinkLable2 = Nothing
        Me.fndSettlementEntry.MyMaxLength = 30
        Me.fndSettlementEntry.MyReadOnly = False
        Me.fndSettlementEntry.Name = "fndSettlementEntry"
        Me.fndSettlementEntry.Size = New System.Drawing.Size(262, 20)
        Me.fndSettlementEntry.TabIndex = 0
        Me.fndSettlementEntry.Value = ""
        '
        'lblOrderNo
        '
        Me.lblOrderNo.FieldName = Nothing
        Me.lblOrderNo.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblOrderNo.Location = New System.Drawing.Point(12, 3)
        Me.lblOrderNo.Name = "lblOrderNo"
        Me.lblOrderNo.Size = New System.Drawing.Size(89, 18)
        Me.lblOrderNo.TabIndex = 14
        Me.lblOrderNo.Text = "Settlement Entry"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(375, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(20, 18)
        Me.btnReset.TabIndex = 1
        '
        'lblOrderDate
        '
        Me.lblOrderDate.FieldName = Nothing
        Me.lblOrderDate.Location = New System.Drawing.Point(411, 4)
        Me.lblOrderDate.Name = "lblOrderDate"
        Me.lblOrderDate.Size = New System.Drawing.Size(87, 18)
        Me.lblOrderDate.TabIndex = 23
        Me.lblOrderDate.Text = "Settlement Date"
        '
        'dtSettlement
        '
        Me.dtSettlement.CalculationExpression = Nothing
        Me.dtSettlement.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtSettlement.Enabled = False
        Me.dtSettlement.FieldCode = Nothing
        Me.dtSettlement.FieldDesc = Nothing
        Me.dtSettlement.FieldMaxLength = 0
        Me.dtSettlement.FieldName = Nothing
        Me.dtSettlement.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtSettlement.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtSettlement.isCalculatedField = False
        Me.dtSettlement.IsSourceFromTable = False
        Me.dtSettlement.IsSourceFromValueList = False
        Me.dtSettlement.IsUnique = False
        Me.dtSettlement.Location = New System.Drawing.Point(504, 4)
        Me.dtSettlement.MendatroryField = False
        Me.dtSettlement.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtSettlement.MyLinkLable1 = Me.lblOrderDate
        Me.dtSettlement.MyLinkLable2 = Nothing
        Me.dtSettlement.Name = "dtSettlement"
        Me.dtSettlement.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtSettlement.ReferenceFieldDesc = Nothing
        Me.dtSettlement.ReferenceFieldName = Nothing
        Me.dtSettlement.ReferenceTableName = Nothing
        Me.dtSettlement.Size = New System.Drawing.Size(97, 18)
        Me.dtSettlement.TabIndex = 2
        Me.dtSettlement.TabStop = False
        Me.dtSettlement.Text = "18/05/2011 02:11 PM"
        Me.dtSettlement.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblOrderNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtSettlement)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblOrderDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndSettlementEntry)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblunappliedamt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdlblloadoutno)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSettlementAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndloadno)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAppliedAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSalesman)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTotPayment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndSalesMan)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBalanceAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocationDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSalesManName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndRouteNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRouteDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer1.Size = New System.Drawing.Size(756, 486)
        Me.SplitContainer1.SplitterDistance = 168
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPanel1
        '
        Me.RadPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPanel1.Controls.Add(Me.btnEmptySettlement)
        Me.RadPanel1.Controls.Add(Me.btnPost)
        Me.RadPanel1.Controls.Add(Me.btnClose)
        Me.RadPanel1.Controls.Add(Me.btnSave)
        Me.RadPanel1.Controls.Add(Me.btnPrint)
        Me.RadPanel1.Controls.Add(Me.btnDelete)
        Me.RadPanel1.Location = New System.Drawing.Point(0, 462)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(756, 24)
        Me.RadPanel1.TabIndex = 1
        Me.RadPanel1.Text = "RadPanel1"
        '
        'FrmSettlementEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(756, 486)
        Me.Controls.Add(Me.RadPanel1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmSettlementEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Settlement Entry"
        CType(Me.btnEmptySettlement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblunappliedamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSettlementAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAppliedAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotPayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBalanceAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalesManName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblloadoutno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOrderDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtSettlement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents fndSettlementEntry As common.UserControls.txtNavigator
    Friend WithEvents lblOrderNo As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblOrderDate As common.Controls.MyLabel
    Friend WithEvents dtSettlement As common.Controls.MyDateTimePicker
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents fndloadno As common.UserControls.txtFinder
    Friend WithEvents rdlblloadoutno As common.Controls.MyLabel
    Friend WithEvents txtLocationDesc As common.Controls.MyTextBox
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtRouteDesc As common.Controls.MyTextBox
    Friend WithEvents fndRouteNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtSalesManName As common.Controls.MyTextBox
    Friend WithEvents fndSalesMan As common.UserControls.txtFinder
    Friend WithEvents lblSalesman As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblunappliedamt As common.Controls.MyLabel
    Friend WithEvents txtSettlementAmt As common.Controls.MyTextBox
    Friend WithEvents txtAppliedAmt As common.Controls.MyTextBox
    Friend WithEvents lblTotPayment As common.Controls.MyLabel
    Friend WithEvents txtBalanceAmt As common.Controls.MyTextBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnEmptySettlement As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
End Class

