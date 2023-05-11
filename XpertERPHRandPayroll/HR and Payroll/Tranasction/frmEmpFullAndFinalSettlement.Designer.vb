Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmpFullAndFinalSettlement
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
        Me.UsLock1 = New common.usLock()
        Me.lblEmpName = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblempcode = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadSplitContainer1 = New Telerik.WinControls.UI.RadSplitContainer()
        Me.SplitPanel2 = New Telerik.WinControls.UI.SplitPanel()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.pageGeneral = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtPayPeriod = New common.UserControls.txtFinder()
        Me.dtpChequeDated = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtChequeNo = New common.Controls.MyTextBox()
        Me.DocDate = New common.Controls.MyLabel()
        Me.txtdocdate = New common.Controls.MyDateTimePicker()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.txtNoofDaysLastSal = New common.Controls.MyTextBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.lblLastSalaryMonth = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.lblServiceRenderedPeriod = New common.Controls.MyLabel()
        Me.txtReasonForLeaving = New common.Controls.MyTextBox()
        Me.lblReasonofLeaving = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.lblShortFallDays = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.dtpLastWDay = New common.Controls.MyDateTimePicker()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.dtpResignSubmitDate = New common.Controls.MyDateTimePicker()
        Me.lblDOJ = New common.Controls.MyLabel()
        Me.dtpDoJ = New common.Controls.MyDateTimePicker()
        Me.lblDepartmentId = New common.Controls.MyLabel()
        Me.lbl = New common.Controls.MyLabel()
        Me.lblDesignationId = New common.Controls.MyLabel()
        Me.lblLastDrawnSalUpTo = New common.Controls.MyLabel()
        Me.dtpLastSalUptoDate = New common.Controls.MyDateTimePicker()
        Me.lblDesignation = New common.Controls.MyLabel()
        Me.fndLastSalaryPayperiodCode = New common.UserControls.txtFinder()
        Me.lblOrderDate = New common.Controls.MyLabel()
        Me.dtpActualLastWDay = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblNoticePeriod = New common.Controls.MyLabel()
        Me.pageUnpaidSalary = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.gvSalStructAndUnpaidSalAmt = New common.UserControls.MyRadGridView()
        Me.pageOthers = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvOthers = New common.UserControls.MyRadGridView()
        Me.pageLessDeductions = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvDeductions = New common.UserControls.MyRadGridView()
        Me.pageTotal = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPanel4 = New Telerik.WinControls.UI.RadPanel()
        Me.txtNetAmountPayable = New common.Controls.MyLabel()
        Me.lblNetAmountPayable = New common.Controls.MyLabel()
        Me.txtTotalDuction = New common.Controls.MyLabel()
        Me.lblTotalDeduction = New common.Controls.MyLabel()
        Me.lblTotalEarnings = New common.Controls.MyLabel()
        Me.lblTotalOthrEarnings = New common.Controls.MyLabel()
        Me.txtTotalUnpaidSalary = New common.Controls.MyLabel()
        Me.lblTotalUnpaidSalary = New common.Controls.MyLabel()
        Me.btnPrintFinalDeclaration = New Telerik.WinControls.UI.RadButton()
        Me.BtnPrintNoDues = New Telerik.WinControls.UI.RadButton()
        Me.btnPrintResignation = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.lblchqAmt = New common.Controls.MyLabel()
        Me.lblClearanceDate = New common.Controls.MyLabel()
        Me.dtpClearDate = New common.Controls.MyDateTimePicker()
        Me.txtChequeAmt = New common.MyNumBox()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadSplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadSplitContainer1.SuspendLayout()
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel2.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.pageGeneral.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpChequeDated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChequeNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdocdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoofDaysLastSal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLastSalaryMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblServiceRenderedPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReasonForLeaving, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReasonofLeaving, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShortFallDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpLastWDay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpResignSubmitDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDOJ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDoJ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartmentId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDesignationId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLastDrawnSalUpTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpLastSalUptoDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDesignation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOrderDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpActualLastWDay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNoticePeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageUnpaidSalary.SuspendLayout()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.gvSalStructAndUnpaidSalAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSalStructAndUnpaidSalAmt.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageOthers.SuspendLayout()
        CType(Me.gvOthers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvOthers.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageLessDeductions.SuspendLayout()
        CType(Me.gvDeductions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDeductions.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageTotal.SuspendLayout()
        CType(Me.RadPanel4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel4.SuspendLayout()
        CType(Me.txtNetAmountPayable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNetAmountPayable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalDuction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalEarnings, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalOthrEarnings, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalUnpaidSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalUnpaidSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintFinalDeclaration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnPrintNoDues, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintResignation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblchqAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblClearanceDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpClearDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChequeAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(865, 13)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(91, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 208
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.FieldName = Nothing
        Me.lblEmpName.Location = New System.Drawing.Point(376, 14)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(483, 19)
        Me.lblEmpName.TabIndex = 207
        Me.lblEmpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(100, 13)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblempcode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(255, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblempcode
        '
        Me.lblempcode.FieldName = Nothing
        Me.lblempcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblempcode.Location = New System.Drawing.Point(7, 15)
        Me.lblempcode.Name = "lblempcode"
        Me.lblempcode.Size = New System.Drawing.Size(87, 16)
        Me.lblempcode.TabIndex = 206
        Me.lblempcode.Text = "Employee Code"
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(356, 13)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(16, 20)
        Me.btnnew.TabIndex = 205
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadSplitContainer1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrintFinalDeclaration)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnPrintNoDues)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrintResignation)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(986, 548)
        Me.SplitContainer1.SplitterDistance = 509
        Me.SplitContainer1.TabIndex = 203
        '
        'RadSplitContainer1
        '
        Me.RadSplitContainer1.Controls.Add(Me.SplitPanel2)
        Me.RadSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadSplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.RadSplitContainer1.Name = "RadSplitContainer1"
        Me.RadSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        '
        '
        Me.RadSplitContainer1.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.RadSplitContainer1.Size = New System.Drawing.Size(986, 509)
        Me.RadSplitContainer1.TabIndex = 230
        Me.RadSplitContainer1.TabStop = False
        Me.RadSplitContainer1.Text = "RadSplitContainer1"
        '
        'SplitPanel2
        '
        Me.SplitPanel2.Controls.Add(Me.RadPageView1)
        Me.SplitPanel2.Controls.Add(Me.UsLock1)
        Me.SplitPanel2.Controls.Add(Me.lblEmpName)
        Me.SplitPanel2.Controls.Add(Me.btnnew)
        Me.SplitPanel2.Controls.Add(Me.lblempcode)
        Me.SplitPanel2.Controls.Add(Me.txtCode)
        Me.SplitPanel2.Location = New System.Drawing.Point(0, 0)
        Me.SplitPanel2.Name = "SplitPanel2"
        '
        '
        '
        Me.SplitPanel2.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel2.Size = New System.Drawing.Size(986, 509)
        Me.SplitPanel2.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0.0!, 0.1736402!)
        Me.SplitPanel2.SizeInfo.SplitterCorrection = New System.Drawing.Size(0, 70)
        Me.SplitPanel2.TabIndex = 1
        Me.SplitPanel2.TabStop = False
        Me.SplitPanel2.Text = "SplitPanel2"
        '
        'RadPageView1
        '
        Me.RadPageView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPageView1.Controls.Add(Me.pageGeneral)
        Me.RadPageView1.Controls.Add(Me.pageUnpaidSalary)
        Me.RadPageView1.Controls.Add(Me.pageOthers)
        Me.RadPageView1.Controls.Add(Me.pageLessDeductions)
        Me.RadPageView1.Controls.Add(Me.pageTotal)
        Me.RadPageView1.Location = New System.Drawing.Point(8, 40)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.pageGeneral
        Me.RadPageView1.Size = New System.Drawing.Size(975, 466)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'pageGeneral
        '
        Me.pageGeneral.Controls.Add(Me.txtChequeAmt)
        Me.pageGeneral.Controls.Add(Me.dtpClearDate)
        Me.pageGeneral.Controls.Add(Me.lblClearanceDate)
        Me.pageGeneral.Controls.Add(Me.lblchqAmt)
        Me.pageGeneral.Controls.Add(Me.MyLabel3)
        Me.pageGeneral.Controls.Add(Me.txtPayPeriod)
        Me.pageGeneral.Controls.Add(Me.dtpChequeDated)
        Me.pageGeneral.Controls.Add(Me.MyLabel2)
        Me.pageGeneral.Controls.Add(Me.MyLabel5)
        Me.pageGeneral.Controls.Add(Me.txtChequeNo)
        Me.pageGeneral.Controls.Add(Me.DocDate)
        Me.pageGeneral.Controls.Add(Me.txtdocdate)
        Me.pageGeneral.Controls.Add(Me.txtDescription)
        Me.pageGeneral.Controls.Add(Me.lblDescription)
        Me.pageGeneral.Controls.Add(Me.txtNoofDaysLastSal)
        Me.pageGeneral.Controls.Add(Me.MyLabel12)
        Me.pageGeneral.Controls.Add(Me.lblLastSalaryMonth)
        Me.pageGeneral.Controls.Add(Me.MyLabel10)
        Me.pageGeneral.Controls.Add(Me.lblServiceRenderedPeriod)
        Me.pageGeneral.Controls.Add(Me.txtReasonForLeaving)
        Me.pageGeneral.Controls.Add(Me.lblReasonofLeaving)
        Me.pageGeneral.Controls.Add(Me.MyLabel9)
        Me.pageGeneral.Controls.Add(Me.lblShortFallDays)
        Me.pageGeneral.Controls.Add(Me.MyLabel7)
        Me.pageGeneral.Controls.Add(Me.dtpLastWDay)
        Me.pageGeneral.Controls.Add(Me.MyLabel6)
        Me.pageGeneral.Controls.Add(Me.dtpResignSubmitDate)
        Me.pageGeneral.Controls.Add(Me.lblDOJ)
        Me.pageGeneral.Controls.Add(Me.dtpDoJ)
        Me.pageGeneral.Controls.Add(Me.lblDepartmentId)
        Me.pageGeneral.Controls.Add(Me.lbl)
        Me.pageGeneral.Controls.Add(Me.lblDesignationId)
        Me.pageGeneral.Controls.Add(Me.lblLastDrawnSalUpTo)
        Me.pageGeneral.Controls.Add(Me.dtpLastSalUptoDate)
        Me.pageGeneral.Controls.Add(Me.lblDesignation)
        Me.pageGeneral.Controls.Add(Me.fndLastSalaryPayperiodCode)
        Me.pageGeneral.Controls.Add(Me.lblOrderDate)
        Me.pageGeneral.Controls.Add(Me.dtpActualLastWDay)
        Me.pageGeneral.Controls.Add(Me.MyLabel1)
        Me.pageGeneral.Controls.Add(Me.lblNoticePeriod)
        Me.pageGeneral.ItemSize = New System.Drawing.SizeF(55.0!, 28.0!)
        Me.pageGeneral.Location = New System.Drawing.Point(10, 37)
        Me.pageGeneral.Name = "pageGeneral"
        Me.pageGeneral.Size = New System.Drawing.Size(954, 418)
        Me.pageGeneral.Text = "General"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(445, 259)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(103, 16)
        Me.MyLabel3.TabIndex = 252
        Me.MyLabel3.Text = "Current Pay Period"
        '
        'txtPayPeriod
        '
        Me.txtPayPeriod.CalculationExpression = Nothing
        Me.txtPayPeriod.Enabled = False
        Me.txtPayPeriod.FieldCode = Nothing
        Me.txtPayPeriod.FieldDesc = Nothing
        Me.txtPayPeriod.FieldMaxLength = 0
        Me.txtPayPeriod.FieldName = Nothing
        Me.txtPayPeriod.isCalculatedField = False
        Me.txtPayPeriod.IsSourceFromTable = False
        Me.txtPayPeriod.IsSourceFromValueList = False
        Me.txtPayPeriod.IsUnique = False
        Me.txtPayPeriod.Location = New System.Drawing.Point(548, 256)
        Me.txtPayPeriod.MendatroryField = True
        Me.txtPayPeriod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayPeriod.MyLinkLable1 = Me.MyLabel3
        Me.txtPayPeriod.MyLinkLable2 = Nothing
        Me.txtPayPeriod.MyReadOnly = False
        Me.txtPayPeriod.MyShowMasterFormButton = False
        Me.txtPayPeriod.Name = "txtPayPeriod"
        Me.txtPayPeriod.ReferenceFieldDesc = Nothing
        Me.txtPayPeriod.ReferenceFieldName = Nothing
        Me.txtPayPeriod.ReferenceTableName = Nothing
        Me.txtPayPeriod.Size = New System.Drawing.Size(219, 19)
        Me.txtPayPeriod.TabIndex = 251
        Me.txtPayPeriod.Value = ""
        '
        'dtpChequeDated
        '
        Me.dtpChequeDated.CalculationExpression = Nothing
        Me.dtpChequeDated.CustomFormat = "dd/MM/yyyy"
        Me.dtpChequeDated.FieldCode = Nothing
        Me.dtpChequeDated.FieldDesc = Nothing
        Me.dtpChequeDated.FieldMaxLength = 0
        Me.dtpChequeDated.FieldName = Nothing
        Me.dtpChequeDated.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpChequeDated.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChequeDated.isCalculatedField = False
        Me.dtpChequeDated.IsSourceFromTable = False
        Me.dtpChequeDated.IsSourceFromValueList = False
        Me.dtpChequeDated.IsUnique = False
        Me.dtpChequeDated.Location = New System.Drawing.Point(592, 29)
        Me.dtpChequeDated.MendatroryField = True
        Me.dtpChequeDated.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChequeDated.MyLinkLable1 = Nothing
        Me.dtpChequeDated.MyLinkLable2 = Nothing
        Me.dtpChequeDated.Name = "dtpChequeDated"
        Me.dtpChequeDated.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChequeDated.ReferenceFieldDesc = Nothing
        Me.dtpChequeDated.ReferenceFieldName = Nothing
        Me.dtpChequeDated.ReferenceTableName = Nothing
        Me.dtpChequeDated.Size = New System.Drawing.Size(147, 18)
        Me.dtpChequeDated.TabIndex = 250
        Me.dtpChequeDated.TabStop = False
        Me.dtpChequeDated.Text = "06/07/2013"
        Me.dtpChequeDated.Value = New Date(2013, 7, 6, 0, 0, 0, 0)
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(458, 29)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(71, 18)
        Me.MyLabel2.TabIndex = 249
        Me.MyLabel2.Text = "Cheque Date"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(458, 6)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel5.TabIndex = 248
        Me.MyLabel5.Text = "Cheque No"
        '
        'txtChequeNo
        '
        Me.txtChequeNo.CalculationExpression = Nothing
        Me.txtChequeNo.FieldCode = Nothing
        Me.txtChequeNo.FieldDesc = Nothing
        Me.txtChequeNo.FieldMaxLength = 0
        Me.txtChequeNo.FieldName = Nothing
        Me.txtChequeNo.isCalculatedField = False
        Me.txtChequeNo.IsSourceFromTable = False
        Me.txtChequeNo.IsSourceFromValueList = False
        Me.txtChequeNo.IsUnique = False
        Me.txtChequeNo.Location = New System.Drawing.Point(592, 3)
        Me.txtChequeNo.MaxLength = 55
        Me.txtChequeNo.MendatroryField = False
        Me.txtChequeNo.MyLinkLable1 = Nothing
        Me.txtChequeNo.MyLinkLable2 = Nothing
        Me.txtChequeNo.Name = "txtChequeNo"
        Me.txtChequeNo.ReferenceFieldDesc = Nothing
        Me.txtChequeNo.ReferenceFieldName = Nothing
        Me.txtChequeNo.ReferenceTableName = Nothing
        Me.txtChequeNo.Size = New System.Drawing.Size(147, 20)
        Me.txtChequeNo.TabIndex = 247
        '
        'DocDate
        '
        Me.DocDate.FieldName = Nothing
        Me.DocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DocDate.Location = New System.Drawing.Point(6, 53)
        Me.DocDate.Name = "DocDate"
        Me.DocDate.Size = New System.Drawing.Size(85, 16)
        Me.DocDate.TabIndex = 65
        Me.DocDate.Text = "Document Date"
        '
        'txtdocdate
        '
        Me.txtdocdate.CalculationExpression = Nothing
        Me.txtdocdate.CustomFormat = "dd/MM/yyyy"
        Me.txtdocdate.FieldCode = Nothing
        Me.txtdocdate.FieldDesc = Nothing
        Me.txtdocdate.FieldMaxLength = 0
        Me.txtdocdate.FieldName = Nothing
        Me.txtdocdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdocdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtdocdate.isCalculatedField = False
        Me.txtdocdate.IsSourceFromTable = False
        Me.txtdocdate.IsSourceFromValueList = False
        Me.txtdocdate.IsUnique = False
        Me.txtdocdate.Location = New System.Drawing.Point(219, 50)
        Me.txtdocdate.MendatroryField = True
        Me.txtdocdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdocdate.MyLinkLable1 = Me.DocDate
        Me.txtdocdate.MyLinkLable2 = Nothing
        Me.txtdocdate.Name = "txtdocdate"
        Me.txtdocdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdocdate.ReferenceFieldDesc = Nothing
        Me.txtdocdate.ReferenceFieldName = Nothing
        Me.txtdocdate.ReferenceTableName = Nothing
        Me.txtdocdate.Size = New System.Drawing.Size(143, 18)
        Me.txtdocdate.TabIndex = 64
        Me.txtdocdate.TabStop = False
        Me.txtdocdate.Text = "03/05/2011"
        Me.txtdocdate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtDescription
        '
        Me.txtDescription.AutoSize = False
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(220, 332)
        Me.txtDescription.MaxLength = 49
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.lblDescription
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(482, 48)
        Me.txtDescription.TabIndex = 63
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(9, 328)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(51, 16)
        Me.lblDescription.TabIndex = 62
        Me.lblDescription.Text = "Remarks"
        '
        'txtNoofDaysLastSal
        '
        Me.txtNoofDaysLastSal.CalculationExpression = Nothing
        Me.txtNoofDaysLastSal.FieldCode = Nothing
        Me.txtNoofDaysLastSal.FieldDesc = Nothing
        Me.txtNoofDaysLastSal.FieldMaxLength = 0
        Me.txtNoofDaysLastSal.FieldName = Nothing
        Me.txtNoofDaysLastSal.isCalculatedField = False
        Me.txtNoofDaysLastSal.IsSourceFromTable = False
        Me.txtNoofDaysLastSal.IsSourceFromValueList = False
        Me.txtNoofDaysLastSal.IsUnique = False
        Me.txtNoofDaysLastSal.Location = New System.Drawing.Point(219, 306)
        Me.txtNoofDaysLastSal.MaxLength = 50
        Me.txtNoofDaysLastSal.MendatroryField = False
        Me.txtNoofDaysLastSal.MyLinkLable1 = Me.MyLabel12
        Me.txtNoofDaysLastSal.MyLinkLable2 = Nothing
        Me.txtNoofDaysLastSal.Name = "txtNoofDaysLastSal"
        Me.txtNoofDaysLastSal.ReferenceFieldDesc = Nothing
        Me.txtNoofDaysLastSal.ReferenceFieldName = Nothing
        Me.txtNoofDaysLastSal.ReferenceTableName = Nothing
        Me.txtNoofDaysLastSal.Size = New System.Drawing.Size(218, 20)
        Me.txtNoofDaysLastSal.TabIndex = 13
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(9, 306)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(181, 16)
        Me.MyLabel12.TabIndex = 61
        Me.MyLabel12.Text = "Work done after Last Salary Month"
        '
        'lblLastSalaryMonth
        '
        Me.lblLastSalaryMonth.FieldName = Nothing
        Me.lblLastSalaryMonth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastSalaryMonth.Location = New System.Drawing.Point(9, 259)
        Me.lblLastSalaryMonth.Name = "lblLastSalaryMonth"
        Me.lblLastSalaryMonth.Size = New System.Drawing.Size(97, 16)
        Me.lblLastSalaryMonth.TabIndex = 59
        Me.lblLastSalaryMonth.Text = "Last Salary Month"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(8, 237)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(148, 16)
        Me.MyLabel10.TabIndex = 58
        Me.MyLabel10.Text = "Period Of Service Rendered"
        '
        'lblServiceRenderedPeriod
        '
        Me.lblServiceRenderedPeriod.AutoSize = False
        Me.lblServiceRenderedPeriod.BorderVisible = True
        Me.lblServiceRenderedPeriod.FieldName = Nothing
        Me.lblServiceRenderedPeriod.Location = New System.Drawing.Point(220, 237)
        Me.lblServiceRenderedPeriod.Name = "lblServiceRenderedPeriod"
        Me.lblServiceRenderedPeriod.Size = New System.Drawing.Size(218, 19)
        Me.lblServiceRenderedPeriod.TabIndex = 10
        Me.lblServiceRenderedPeriod.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtReasonForLeaving
        '
        Me.txtReasonForLeaving.CalculationExpression = Nothing
        Me.txtReasonForLeaving.FieldCode = Nothing
        Me.txtReasonForLeaving.FieldDesc = Nothing
        Me.txtReasonForLeaving.FieldMaxLength = 0
        Me.txtReasonForLeaving.FieldName = Nothing
        Me.txtReasonForLeaving.isCalculatedField = False
        Me.txtReasonForLeaving.IsSourceFromTable = False
        Me.txtReasonForLeaving.IsSourceFromValueList = False
        Me.txtReasonForLeaving.IsUnique = False
        Me.txtReasonForLeaving.Location = New System.Drawing.Point(220, 213)
        Me.txtReasonForLeaving.MaxLength = 50
        Me.txtReasonForLeaving.MendatroryField = False
        Me.txtReasonForLeaving.MyLinkLable1 = Me.lblReasonofLeaving
        Me.txtReasonForLeaving.MyLinkLable2 = Nothing
        Me.txtReasonForLeaving.Name = "txtReasonForLeaving"
        Me.txtReasonForLeaving.ReferenceFieldDesc = Nothing
        Me.txtReasonForLeaving.ReferenceFieldName = Nothing
        Me.txtReasonForLeaving.ReferenceTableName = Nothing
        Me.txtReasonForLeaving.Size = New System.Drawing.Size(482, 20)
        Me.txtReasonForLeaving.TabIndex = 9
        '
        'lblReasonofLeaving
        '
        Me.lblReasonofLeaving.FieldName = Nothing
        Me.lblReasonofLeaving.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReasonofLeaving.Location = New System.Drawing.Point(6, 213)
        Me.lblReasonofLeaving.Name = "lblReasonofLeaving"
        Me.lblReasonofLeaving.Size = New System.Drawing.Size(103, 16)
        Me.lblReasonofLeaving.TabIndex = 56
        Me.lblReasonofLeaving.Text = "Reason Of Leaving"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(8, 190)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel9.TabIndex = 54
        Me.MyLabel9.Text = "Short Fall Days"
        '
        'lblShortFallDays
        '
        Me.lblShortFallDays.AutoSize = False
        Me.lblShortFallDays.BorderVisible = True
        Me.lblShortFallDays.FieldName = Nothing
        Me.lblShortFallDays.Location = New System.Drawing.Point(220, 190)
        Me.lblShortFallDays.Name = "lblShortFallDays"
        Me.lblShortFallDays.Size = New System.Drawing.Size(218, 19)
        Me.lblShortFallDays.TabIndex = 8
        Me.lblShortFallDays.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(7, 142)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(96, 16)
        Me.MyLabel7.TabIndex = 52
        Me.MyLabel7.Text = "Last Working Day"
        '
        'dtpLastWDay
        '
        Me.dtpLastWDay.CalculationExpression = Nothing
        Me.dtpLastWDay.CustomFormat = "dd/MM/yyyy"
        Me.dtpLastWDay.Enabled = False
        Me.dtpLastWDay.FieldCode = Nothing
        Me.dtpLastWDay.FieldDesc = Nothing
        Me.dtpLastWDay.FieldMaxLength = 0
        Me.dtpLastWDay.FieldName = Nothing
        Me.dtpLastWDay.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpLastWDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLastWDay.isCalculatedField = False
        Me.dtpLastWDay.IsSourceFromTable = False
        Me.dtpLastWDay.IsSourceFromValueList = False
        Me.dtpLastWDay.IsUnique = False
        Me.dtpLastWDay.Location = New System.Drawing.Point(220, 142)
        Me.dtpLastWDay.MendatroryField = True
        Me.dtpLastWDay.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLastWDay.MyLinkLable1 = Me.MyLabel6
        Me.dtpLastWDay.MyLinkLable2 = Nothing
        Me.dtpLastWDay.Name = "dtpLastWDay"
        Me.dtpLastWDay.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLastWDay.ReferenceFieldDesc = Nothing
        Me.dtpLastWDay.ReferenceFieldName = Nothing
        Me.dtpLastWDay.ReferenceTableName = Nothing
        Me.dtpLastWDay.Size = New System.Drawing.Size(143, 18)
        Me.dtpLastWDay.TabIndex = 6
        Me.dtpLastWDay.TabStop = False
        Me.dtpLastWDay.Text = "03/05/2011"
        Me.dtpLastWDay.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(7, 97)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(155, 16)
        Me.MyLabel6.TabIndex = 50
        Me.MyLabel6.Text = "Resignation Submission Date"
        '
        'dtpResignSubmitDate
        '
        Me.dtpResignSubmitDate.CalculationExpression = Nothing
        Me.dtpResignSubmitDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpResignSubmitDate.FieldCode = Nothing
        Me.dtpResignSubmitDate.FieldDesc = Nothing
        Me.dtpResignSubmitDate.FieldMaxLength = 0
        Me.dtpResignSubmitDate.FieldName = Nothing
        Me.dtpResignSubmitDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpResignSubmitDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpResignSubmitDate.isCalculatedField = False
        Me.dtpResignSubmitDate.IsSourceFromTable = False
        Me.dtpResignSubmitDate.IsSourceFromValueList = False
        Me.dtpResignSubmitDate.IsUnique = False
        Me.dtpResignSubmitDate.Location = New System.Drawing.Point(220, 94)
        Me.dtpResignSubmitDate.MendatroryField = True
        Me.dtpResignSubmitDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpResignSubmitDate.MyLinkLable1 = Me.MyLabel6
        Me.dtpResignSubmitDate.MyLinkLable2 = Nothing
        Me.dtpResignSubmitDate.Name = "dtpResignSubmitDate"
        Me.dtpResignSubmitDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpResignSubmitDate.ReadOnly = True
        Me.dtpResignSubmitDate.ReferenceFieldDesc = Nothing
        Me.dtpResignSubmitDate.ReferenceFieldName = Nothing
        Me.dtpResignSubmitDate.ReferenceTableName = Nothing
        Me.dtpResignSubmitDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpResignSubmitDate.TabIndex = 4
        Me.dtpResignSubmitDate.TabStop = False
        Me.dtpResignSubmitDate.Text = "03/05/2011"
        Me.dtpResignSubmitDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblDOJ
        '
        Me.lblDOJ.FieldName = Nothing
        Me.lblDOJ.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDOJ.Location = New System.Drawing.Point(7, 75)
        Me.lblDOJ.Name = "lblDOJ"
        Me.lblDOJ.Size = New System.Drawing.Size(82, 16)
        Me.lblDOJ.TabIndex = 48
        Me.lblDOJ.Text = "Date of Joining"
        '
        'dtpDoJ
        '
        Me.dtpDoJ.CalculationExpression = Nothing
        Me.dtpDoJ.CustomFormat = "dd/MM/yyyy"
        Me.dtpDoJ.FieldCode = Nothing
        Me.dtpDoJ.FieldDesc = Nothing
        Me.dtpDoJ.FieldMaxLength = 0
        Me.dtpDoJ.FieldName = Nothing
        Me.dtpDoJ.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDoJ.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDoJ.isCalculatedField = False
        Me.dtpDoJ.IsSourceFromTable = False
        Me.dtpDoJ.IsSourceFromValueList = False
        Me.dtpDoJ.IsUnique = False
        Me.dtpDoJ.Location = New System.Drawing.Point(220, 72)
        Me.dtpDoJ.MendatroryField = True
        Me.dtpDoJ.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDoJ.MyLinkLable1 = Me.lblDOJ
        Me.dtpDoJ.MyLinkLable2 = Nothing
        Me.dtpDoJ.Name = "dtpDoJ"
        Me.dtpDoJ.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDoJ.ReadOnly = True
        Me.dtpDoJ.ReferenceFieldDesc = Nothing
        Me.dtpDoJ.ReferenceFieldName = Nothing
        Me.dtpDoJ.ReferenceTableName = Nothing
        Me.dtpDoJ.Size = New System.Drawing.Size(143, 18)
        Me.dtpDoJ.TabIndex = 3
        Me.dtpDoJ.TabStop = False
        Me.dtpDoJ.Text = "03/05/2011"
        Me.dtpDoJ.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblDepartmentId
        '
        Me.lblDepartmentId.AutoSize = False
        Me.lblDepartmentId.BorderVisible = True
        Me.lblDepartmentId.FieldName = Nothing
        Me.lblDepartmentId.Location = New System.Drawing.Point(217, 30)
        Me.lblDepartmentId.Name = "lblDepartmentId"
        Me.lblDepartmentId.Size = New System.Drawing.Size(218, 19)
        Me.lblDepartmentId.TabIndex = 2
        Me.lblDepartmentId.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl
        '
        Me.lbl.FieldName = Nothing
        Me.lbl.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lbl.Location = New System.Drawing.Point(7, 30)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(66, 18)
        Me.lbl.TabIndex = 45
        Me.lbl.Text = "Department"
        '
        'lblDesignationId
        '
        Me.lblDesignationId.AutoSize = False
        Me.lblDesignationId.BorderVisible = True
        Me.lblDesignationId.FieldName = Nothing
        Me.lblDesignationId.Location = New System.Drawing.Point(218, 5)
        Me.lblDesignationId.Name = "lblDesignationId"
        Me.lblDesignationId.Size = New System.Drawing.Size(218, 19)
        Me.lblDesignationId.TabIndex = 1
        Me.lblDesignationId.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLastDrawnSalUpTo
        '
        Me.lblLastDrawnSalUpTo.FieldName = Nothing
        Me.lblLastDrawnSalUpTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastDrawnSalUpTo.Location = New System.Drawing.Point(9, 283)
        Me.lblLastDrawnSalUpTo.Name = "lblLastDrawnSalUpTo"
        Me.lblLastDrawnSalUpTo.Size = New System.Drawing.Size(151, 16)
        Me.lblLastDrawnSalUpTo.TabIndex = 29
        Me.lblLastDrawnSalUpTo.Text = "Last Drawn Salary upto Date"
        '
        'dtpLastSalUptoDate
        '
        Me.dtpLastSalUptoDate.CalculationExpression = Nothing
        Me.dtpLastSalUptoDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpLastSalUptoDate.Enabled = False
        Me.dtpLastSalUptoDate.FieldCode = Nothing
        Me.dtpLastSalUptoDate.FieldDesc = Nothing
        Me.dtpLastSalUptoDate.FieldMaxLength = 0
        Me.dtpLastSalUptoDate.FieldName = Nothing
        Me.dtpLastSalUptoDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpLastSalUptoDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLastSalUptoDate.isCalculatedField = False
        Me.dtpLastSalUptoDate.IsSourceFromTable = False
        Me.dtpLastSalUptoDate.IsSourceFromValueList = False
        Me.dtpLastSalUptoDate.IsUnique = False
        Me.dtpLastSalUptoDate.Location = New System.Drawing.Point(219, 283)
        Me.dtpLastSalUptoDate.MendatroryField = True
        Me.dtpLastSalUptoDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLastSalUptoDate.MyLinkLable1 = Me.lblLastDrawnSalUpTo
        Me.dtpLastSalUptoDate.MyLinkLable2 = Nothing
        Me.dtpLastSalUptoDate.Name = "dtpLastSalUptoDate"
        Me.dtpLastSalUptoDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLastSalUptoDate.ReferenceFieldDesc = Nothing
        Me.dtpLastSalUptoDate.ReferenceFieldName = Nothing
        Me.dtpLastSalUptoDate.ReferenceTableName = Nothing
        Me.dtpLastSalUptoDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpLastSalUptoDate.TabIndex = 12
        Me.dtpLastSalUptoDate.TabStop = False
        Me.dtpLastSalUptoDate.Text = "03/05/2011"
        Me.dtpLastSalUptoDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblDesignation
        '
        Me.lblDesignation.FieldName = Nothing
        Me.lblDesignation.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblDesignation.Location = New System.Drawing.Point(7, 6)
        Me.lblDesignation.Name = "lblDesignation"
        Me.lblDesignation.Size = New System.Drawing.Size(66, 18)
        Me.lblDesignation.TabIndex = 41
        Me.lblDesignation.Text = "Designation"
        '
        'fndLastSalaryPayperiodCode
        '
        Me.fndLastSalaryPayperiodCode.CalculationExpression = Nothing
        Me.fndLastSalaryPayperiodCode.Enabled = False
        Me.fndLastSalaryPayperiodCode.FieldCode = Nothing
        Me.fndLastSalaryPayperiodCode.FieldDesc = Nothing
        Me.fndLastSalaryPayperiodCode.FieldMaxLength = 0
        Me.fndLastSalaryPayperiodCode.FieldName = Nothing
        Me.fndLastSalaryPayperiodCode.isCalculatedField = False
        Me.fndLastSalaryPayperiodCode.IsSourceFromTable = False
        Me.fndLastSalaryPayperiodCode.IsSourceFromValueList = False
        Me.fndLastSalaryPayperiodCode.IsUnique = False
        Me.fndLastSalaryPayperiodCode.Location = New System.Drawing.Point(220, 259)
        Me.fndLastSalaryPayperiodCode.MendatroryField = True
        Me.fndLastSalaryPayperiodCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLastSalaryPayperiodCode.MyLinkLable1 = Me.lblLastSalaryMonth
        Me.fndLastSalaryPayperiodCode.MyLinkLable2 = Nothing
        Me.fndLastSalaryPayperiodCode.MyReadOnly = False
        Me.fndLastSalaryPayperiodCode.MyShowMasterFormButton = False
        Me.fndLastSalaryPayperiodCode.Name = "fndLastSalaryPayperiodCode"
        Me.fndLastSalaryPayperiodCode.ReferenceFieldDesc = Nothing
        Me.fndLastSalaryPayperiodCode.ReferenceFieldName = Nothing
        Me.fndLastSalaryPayperiodCode.ReferenceTableName = Nothing
        Me.fndLastSalaryPayperiodCode.Size = New System.Drawing.Size(219, 19)
        Me.fndLastSalaryPayperiodCode.TabIndex = 11
        Me.fndLastSalaryPayperiodCode.Value = ""
        '
        'lblOrderDate
        '
        Me.lblOrderDate.FieldName = Nothing
        Me.lblOrderDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrderDate.Location = New System.Drawing.Point(6, 168)
        Me.lblOrderDate.Name = "lblOrderDate"
        Me.lblOrderDate.Size = New System.Drawing.Size(130, 16)
        Me.lblOrderDate.TabIndex = 33
        Me.lblOrderDate.Text = "Actual Last Working Day"
        '
        'dtpActualLastWDay
        '
        Me.dtpActualLastWDay.CalculationExpression = Nothing
        Me.dtpActualLastWDay.CustomFormat = "dd/MM/yyyy"
        Me.dtpActualLastWDay.Enabled = False
        Me.dtpActualLastWDay.FieldCode = Nothing
        Me.dtpActualLastWDay.FieldDesc = Nothing
        Me.dtpActualLastWDay.FieldMaxLength = 0
        Me.dtpActualLastWDay.FieldName = Nothing
        Me.dtpActualLastWDay.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpActualLastWDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpActualLastWDay.isCalculatedField = False
        Me.dtpActualLastWDay.IsSourceFromTable = False
        Me.dtpActualLastWDay.IsSourceFromValueList = False
        Me.dtpActualLastWDay.IsUnique = False
        Me.dtpActualLastWDay.Location = New System.Drawing.Point(220, 166)
        Me.dtpActualLastWDay.MendatroryField = True
        Me.dtpActualLastWDay.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpActualLastWDay.MyLinkLable1 = Me.lblOrderDate
        Me.dtpActualLastWDay.MyLinkLable2 = Nothing
        Me.dtpActualLastWDay.Name = "dtpActualLastWDay"
        Me.dtpActualLastWDay.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpActualLastWDay.ReferenceFieldDesc = Nothing
        Me.dtpActualLastWDay.ReferenceFieldName = Nothing
        Me.dtpActualLastWDay.ReferenceTableName = Nothing
        Me.dtpActualLastWDay.Size = New System.Drawing.Size(143, 18)
        Me.dtpActualLastWDay.TabIndex = 7
        Me.dtpActualLastWDay.TabStop = False
        Me.dtpActualLastWDay.Text = "03/05/2011"
        Me.dtpActualLastWDay.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(8, 117)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel1.TabIndex = 37
        Me.MyLabel1.Text = "Notice Period"
        '
        'lblNoticePeriod
        '
        Me.lblNoticePeriod.AutoSize = False
        Me.lblNoticePeriod.BorderVisible = True
        Me.lblNoticePeriod.FieldName = Nothing
        Me.lblNoticePeriod.Location = New System.Drawing.Point(220, 117)
        Me.lblNoticePeriod.Name = "lblNoticePeriod"
        Me.lblNoticePeriod.Size = New System.Drawing.Size(218, 19)
        Me.lblNoticePeriod.TabIndex = 5
        Me.lblNoticePeriod.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pageUnpaidSalary
        '
        Me.pageUnpaidSalary.Controls.Add(Me.RadPanel1)
        Me.pageUnpaidSalary.ItemSize = New System.Drawing.SizeF(190.0!, 28.0!)
        Me.pageUnpaidSalary.Location = New System.Drawing.Point(10, 37)
        Me.pageUnpaidSalary.Name = "pageUnpaidSalary"
        Me.pageUnpaidSalary.Size = New System.Drawing.Size(954, 418)
        Me.pageUnpaidSalary.Text = "Salary Structure And Unpaid Salary"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.gvSalStructAndUnpaidSalAmt)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(954, 418)
        Me.RadPanel1.TabIndex = 6
        Me.RadPanel1.Text = "RadPanel1"
        '
        'gvSalStructAndUnpaidSalAmt
        '
        Me.gvSalStructAndUnpaidSalAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvSalStructAndUnpaidSalAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvSalStructAndUnpaidSalAmt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvSalStructAndUnpaidSalAmt.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvSalStructAndUnpaidSalAmt.ForeColor = System.Drawing.Color.Black
        Me.gvSalStructAndUnpaidSalAmt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvSalStructAndUnpaidSalAmt.Location = New System.Drawing.Point(0, 0)
        '
        'gvSalStructAndUnpaidSalAmt
        '
        Me.gvSalStructAndUnpaidSalAmt.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvSalStructAndUnpaidSalAmt.MasterTemplate.AllowAddNewRow = False
        Me.gvSalStructAndUnpaidSalAmt.MasterTemplate.AutoGenerateColumns = False
        Me.gvSalStructAndUnpaidSalAmt.MasterTemplate.EnableGrouping = False
        Me.gvSalStructAndUnpaidSalAmt.MasterTemplate.EnableSorting = False
        Me.gvSalStructAndUnpaidSalAmt.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvSalStructAndUnpaidSalAmt.Name = "gvSalStructAndUnpaidSalAmt"
        Me.gvSalStructAndUnpaidSalAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvSalStructAndUnpaidSalAmt.ShowHeaderCellButtons = True
        Me.gvSalStructAndUnpaidSalAmt.Size = New System.Drawing.Size(954, 418)
        Me.gvSalStructAndUnpaidSalAmt.TabIndex = 5
        Me.gvSalStructAndUnpaidSalAmt.TabStop = False
        Me.gvSalStructAndUnpaidSalAmt.Text = "RadGridView1"
        '
        'pageOthers
        '
        Me.pageOthers.Controls.Add(Me.gvOthers)
        Me.pageOthers.ItemSize = New System.Drawing.SizeF(90.0!, 28.0!)
        Me.pageOthers.Location = New System.Drawing.Point(10, 37)
        Me.pageOthers.Name = "pageOthers"
        Me.pageOthers.Size = New System.Drawing.Size(954, 418)
        Me.pageOthers.Text = "Other Earnings"
        '
        'gvOthers
        '
        Me.gvOthers.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvOthers.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvOthers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvOthers.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvOthers.ForeColor = System.Drawing.Color.Black
        Me.gvOthers.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvOthers.Location = New System.Drawing.Point(0, 0)
        '
        'gvOthers
        '
        Me.gvOthers.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvOthers.MasterTemplate.AllowAddNewRow = False
        Me.gvOthers.MasterTemplate.AutoGenerateColumns = False
        Me.gvOthers.MasterTemplate.EnableGrouping = False
        Me.gvOthers.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvOthers.Name = "gvOthers"
        Me.gvOthers.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvOthers.ShowHeaderCellButtons = True
        Me.gvOthers.Size = New System.Drawing.Size(954, 418)
        Me.gvOthers.TabIndex = 4
        Me.gvOthers.TabStop = False
        Me.gvOthers.Text = "RadGridView1"
        '
        'pageLessDeductions
        '
        Me.pageLessDeductions.Controls.Add(Me.gvDeductions)
        Me.pageLessDeductions.ItemSize = New System.Drawing.SizeF(97.0!, 28.0!)
        Me.pageLessDeductions.Location = New System.Drawing.Point(10, 37)
        Me.pageLessDeductions.Name = "pageLessDeductions"
        Me.pageLessDeductions.Size = New System.Drawing.Size(954, 418)
        Me.pageLessDeductions.Text = "Less Deductions"
        '
        'gvDeductions
        '
        Me.gvDeductions.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvDeductions.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvDeductions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDeductions.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvDeductions.ForeColor = System.Drawing.Color.Black
        Me.gvDeductions.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvDeductions.Location = New System.Drawing.Point(0, 0)
        '
        'gvDeductions
        '
        Me.gvDeductions.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvDeductions.MasterTemplate.AllowAddNewRow = False
        Me.gvDeductions.MasterTemplate.AutoGenerateColumns = False
        Me.gvDeductions.MasterTemplate.EnableGrouping = False
        Me.gvDeductions.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDeductions.Name = "gvDeductions"
        Me.gvDeductions.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvDeductions.ShowHeaderCellButtons = True
        Me.gvDeductions.Size = New System.Drawing.Size(954, 418)
        Me.gvDeductions.TabIndex = 5
        Me.gvDeductions.TabStop = False
        Me.gvDeductions.Text = "RadGridView1"
        '
        'pageTotal
        '
        Me.pageTotal.Controls.Add(Me.RadPanel4)
        Me.pageTotal.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.pageTotal.Location = New System.Drawing.Point(10, 37)
        Me.pageTotal.Name = "pageTotal"
        Me.pageTotal.Size = New System.Drawing.Size(954, 418)
        Me.pageTotal.Text = "Total"
        '
        'RadPanel4
        '
        Me.RadPanel4.Controls.Add(Me.txtNetAmountPayable)
        Me.RadPanel4.Controls.Add(Me.lblNetAmountPayable)
        Me.RadPanel4.Controls.Add(Me.txtTotalDuction)
        Me.RadPanel4.Controls.Add(Me.lblTotalDeduction)
        Me.RadPanel4.Controls.Add(Me.lblTotalEarnings)
        Me.RadPanel4.Controls.Add(Me.lblTotalOthrEarnings)
        Me.RadPanel4.Controls.Add(Me.txtTotalUnpaidSalary)
        Me.RadPanel4.Controls.Add(Me.lblTotalUnpaidSalary)
        Me.RadPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel4.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel4.Name = "RadPanel4"
        Me.RadPanel4.Size = New System.Drawing.Size(954, 418)
        Me.RadPanel4.TabIndex = 231
        '
        'txtNetAmountPayable
        '
        Me.txtNetAmountPayable.AutoSize = False
        Me.txtNetAmountPayable.BorderVisible = True
        Me.txtNetAmountPayable.FieldName = Nothing
        Me.txtNetAmountPayable.Location = New System.Drawing.Point(164, 97)
        Me.txtNetAmountPayable.Name = "txtNetAmountPayable"
        Me.txtNetAmountPayable.Size = New System.Drawing.Size(218, 19)
        Me.txtNetAmountPayable.TabIndex = 51
        Me.txtNetAmountPayable.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNetAmountPayable
        '
        Me.lblNetAmountPayable.FieldName = Nothing
        Me.lblNetAmountPayable.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblNetAmountPayable.Location = New System.Drawing.Point(31, 97)
        Me.lblNetAmountPayable.Name = "lblNetAmountPayable"
        Me.lblNetAmountPayable.Size = New System.Drawing.Size(110, 18)
        Me.lblNetAmountPayable.TabIndex = 53
        Me.lblNetAmountPayable.Text = "Net Amount Payable"
        '
        'txtTotalDuction
        '
        Me.txtTotalDuction.AutoSize = False
        Me.txtTotalDuction.BorderVisible = True
        Me.txtTotalDuction.FieldName = Nothing
        Me.txtTotalDuction.Location = New System.Drawing.Point(165, 72)
        Me.txtTotalDuction.Name = "txtTotalDuction"
        Me.txtTotalDuction.Size = New System.Drawing.Size(218, 19)
        Me.txtTotalDuction.TabIndex = 50
        Me.txtTotalDuction.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalDeduction
        '
        Me.lblTotalDeduction.FieldName = Nothing
        Me.lblTotalDeduction.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblTotalDeduction.Location = New System.Drawing.Point(31, 73)
        Me.lblTotalDeduction.Name = "lblTotalDeduction"
        Me.lblTotalDeduction.Size = New System.Drawing.Size(110, 18)
        Me.lblTotalDeduction.TabIndex = 52
        Me.lblTotalDeduction.Text = "Less Total Deduction"
        '
        'lblTotalEarnings
        '
        Me.lblTotalEarnings.AutoSize = False
        Me.lblTotalEarnings.BorderVisible = True
        Me.lblTotalEarnings.FieldName = Nothing
        Me.lblTotalEarnings.Location = New System.Drawing.Point(165, 47)
        Me.lblTotalEarnings.Name = "lblTotalEarnings"
        Me.lblTotalEarnings.Size = New System.Drawing.Size(218, 19)
        Me.lblTotalEarnings.TabIndex = 47
        Me.lblTotalEarnings.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalOthrEarnings
        '
        Me.lblTotalOthrEarnings.FieldName = Nothing
        Me.lblTotalOthrEarnings.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblTotalOthrEarnings.Location = New System.Drawing.Point(31, 47)
        Me.lblTotalOthrEarnings.Name = "lblTotalOthrEarnings"
        Me.lblTotalOthrEarnings.Size = New System.Drawing.Size(108, 18)
        Me.lblTotalOthrEarnings.TabIndex = 49
        Me.lblTotalOthrEarnings.Text = "Total Other Earnings"
        '
        'txtTotalUnpaidSalary
        '
        Me.txtTotalUnpaidSalary.AutoSize = False
        Me.txtTotalUnpaidSalary.BorderVisible = True
        Me.txtTotalUnpaidSalary.FieldName = Nothing
        Me.txtTotalUnpaidSalary.Location = New System.Drawing.Point(166, 22)
        Me.txtTotalUnpaidSalary.Name = "txtTotalUnpaidSalary"
        Me.txtTotalUnpaidSalary.Size = New System.Drawing.Size(218, 19)
        Me.txtTotalUnpaidSalary.TabIndex = 46
        Me.txtTotalUnpaidSalary.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalUnpaidSalary
        '
        Me.lblTotalUnpaidSalary.FieldName = Nothing
        Me.lblTotalUnpaidSalary.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblTotalUnpaidSalary.Location = New System.Drawing.Point(31, 23)
        Me.lblTotalUnpaidSalary.Name = "lblTotalUnpaidSalary"
        Me.lblTotalUnpaidSalary.Size = New System.Drawing.Size(103, 18)
        Me.lblTotalUnpaidSalary.TabIndex = 48
        Me.lblTotalUnpaidSalary.Text = "Total Unpaid Salary"
        '
        'btnPrintFinalDeclaration
        '
        Me.btnPrintFinalDeclaration.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrintFinalDeclaration.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintFinalDeclaration.Location = New System.Drawing.Point(596, 9)
        Me.btnPrintFinalDeclaration.Name = "btnPrintFinalDeclaration"
        Me.btnPrintFinalDeclaration.Size = New System.Drawing.Size(140, 22)
        Me.btnPrintFinalDeclaration.TabIndex = 7
        Me.btnPrintFinalDeclaration.Text = "Print Final Declaration"
        '
        'BtnPrintNoDues
        '
        Me.BtnPrintNoDues.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnPrintNoDues.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrintNoDues.Location = New System.Drawing.Point(450, 9)
        Me.BtnPrintNoDues.Name = "BtnPrintNoDues"
        Me.BtnPrintNoDues.Size = New System.Drawing.Size(140, 22)
        Me.BtnPrintNoDues.TabIndex = 6
        Me.BtnPrintNoDues.Text = "Print No Dues"
        '
        'btnPrintResignation
        '
        Me.btnPrintResignation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrintResignation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintResignation.Location = New System.Drawing.Point(304, 9)
        Me.btnPrintResignation.Name = "btnPrintResignation"
        Me.btnPrintResignation.Size = New System.Drawing.Size(140, 22)
        Me.btnPrintResignation.TabIndex = 5
        Me.btnPrintResignation.Text = "Print Resignation Letter"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(228, 9)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(156, 9)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(8, 9)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(911, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 22)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(82, 9)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(68, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'lblchqAmt
        '
        Me.lblchqAmt.FieldName = Nothing
        Me.lblchqAmt.Location = New System.Drawing.Point(458, 51)
        Me.lblchqAmt.Name = "lblchqAmt"
        Me.lblchqAmt.Size = New System.Drawing.Size(88, 18)
        Me.lblchqAmt.TabIndex = 253
        Me.lblchqAmt.Text = "Cheque Amount"
        '
        'lblClearanceDate
        '
        Me.lblClearanceDate.FieldName = Nothing
        Me.lblClearanceDate.Location = New System.Drawing.Point(458, 72)
        Me.lblClearanceDate.Name = "lblClearanceDate"
        Me.lblClearanceDate.Size = New System.Drawing.Size(81, 18)
        Me.lblClearanceDate.TabIndex = 254
        Me.lblClearanceDate.Text = "Clearance Date"
        '
        'dtpClearDate
        '
        Me.dtpClearDate.CalculationExpression = Nothing
        Me.dtpClearDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpClearDate.FieldCode = Nothing
        Me.dtpClearDate.FieldDesc = Nothing
        Me.dtpClearDate.FieldMaxLength = 0
        Me.dtpClearDate.FieldName = Nothing
        Me.dtpClearDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpClearDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpClearDate.isCalculatedField = False
        Me.dtpClearDate.IsSourceFromTable = False
        Me.dtpClearDate.IsSourceFromValueList = False
        Me.dtpClearDate.IsUnique = False
        Me.dtpClearDate.Location = New System.Drawing.Point(592, 73)
        Me.dtpClearDate.MendatroryField = True
        Me.dtpClearDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpClearDate.MyLinkLable1 = Nothing
        Me.dtpClearDate.MyLinkLable2 = Nothing
        Me.dtpClearDate.Name = "dtpClearDate"
        Me.dtpClearDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpClearDate.ReferenceFieldDesc = Nothing
        Me.dtpClearDate.ReferenceFieldName = Nothing
        Me.dtpClearDate.ReferenceTableName = Nothing
        Me.dtpClearDate.Size = New System.Drawing.Size(147, 18)
        Me.dtpClearDate.TabIndex = 255
        Me.dtpClearDate.TabStop = False
        Me.dtpClearDate.Text = "06/07/2013"
        Me.dtpClearDate.Value = New Date(2013, 7, 6, 0, 0, 0, 0)
        '
        'txtChequeAmt
        '
        Me.txtChequeAmt.BackColor = System.Drawing.Color.White
        Me.txtChequeAmt.CalculationExpression = Nothing
        Me.txtChequeAmt.DecimalPlaces = 2
        Me.txtChequeAmt.FieldCode = Nothing
        Me.txtChequeAmt.FieldDesc = Nothing
        Me.txtChequeAmt.FieldMaxLength = 0
        Me.txtChequeAmt.FieldName = Nothing
        Me.txtChequeAmt.isCalculatedField = False
        Me.txtChequeAmt.IsSourceFromTable = False
        Me.txtChequeAmt.IsSourceFromValueList = False
        Me.txtChequeAmt.IsUnique = False
        Me.txtChequeAmt.Location = New System.Drawing.Point(592, 50)
        Me.txtChequeAmt.MendatroryField = False
        Me.txtChequeAmt.MyLinkLable1 = Nothing
        Me.txtChequeAmt.MyLinkLable2 = Nothing
        Me.txtChequeAmt.Name = "txtChequeAmt"
        Me.txtChequeAmt.ReferenceFieldDesc = Nothing
        Me.txtChequeAmt.ReferenceFieldName = Nothing
        Me.txtChequeAmt.ReferenceTableName = Nothing
        Me.txtChequeAmt.Size = New System.Drawing.Size(147, 20)
        Me.txtChequeAmt.TabIndex = 256
        Me.txtChequeAmt.Text = "0"
        Me.txtChequeAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtChequeAmt.Value = 0.0R
        '
        'frmEmpFullAndFinalSettlement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(986, 548)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmEmpFullAndFinalSettlement"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Full And Final Settlement"
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadSplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadSplitContainer1.ResumeLayout(False)
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel2.ResumeLayout(False)
        Me.SplitPanel2.PerformLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.pageGeneral.ResumeLayout(False)
        Me.pageGeneral.PerformLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpChequeDated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChequeNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdocdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoofDaysLastSal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLastSalaryMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblServiceRenderedPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReasonForLeaving, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReasonofLeaving, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShortFallDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpLastWDay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpResignSubmitDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDOJ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDoJ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartmentId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDesignationId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLastDrawnSalUpTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpLastSalUptoDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDesignation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOrderDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpActualLastWDay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNoticePeriod, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageUnpaidSalary.ResumeLayout(False)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.gvSalStructAndUnpaidSalAmt.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSalStructAndUnpaidSalAmt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageOthers.ResumeLayout(False)
        CType(Me.gvOthers.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvOthers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageLessDeductions.ResumeLayout(False)
        CType(Me.gvDeductions.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDeductions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageTotal.ResumeLayout(False)
        CType(Me.RadPanel4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel4.ResumeLayout(False)
        Me.RadPanel4.PerformLayout()
        CType(Me.txtNetAmountPayable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNetAmountPayable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalDuction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalEarnings, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalOthrEarnings, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalUnpaidSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalUnpaidSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintFinalDeclaration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnPrintNoDues, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintResignation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblchqAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblClearanceDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpClearDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChequeAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblOrderDate As common.Controls.MyLabel
    Friend WithEvents dtpActualLastWDay As common.Controls.MyDateTimePicker
    Friend WithEvents gvOthers As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblNoticePeriod As common.Controls.MyLabel
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents pageGeneral As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pageUnpaidSalary As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pageOthers As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadSplitContainer1 As Telerik.WinControls.UI.RadSplitContainer
    Friend WithEvents SplitPanel2 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents gvSalStructAndUnpaidSalAmt As common.UserControls.MyRadGridView
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents pageTotal As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPanel4 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents lblDesignation As common.Controls.MyLabel
    Friend WithEvents fndLastSalaryPayperiodCode As common.UserControls.txtFinder
    Friend WithEvents lblLastDrawnSalUpTo As common.Controls.MyLabel
    Friend WithEvents dtpLastSalUptoDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblempcode As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents lblDesignationId As common.Controls.MyLabel
    Friend WithEvents lblDepartmentId As common.Controls.MyLabel
    Friend WithEvents lbl As common.Controls.MyLabel
    Friend WithEvents lblDOJ As common.Controls.MyLabel
    Friend WithEvents dtpDoJ As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents dtpResignSubmitDate As common.Controls.MyDateTimePicker
    Friend WithEvents pageLessDeductions As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents dtpLastWDay As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents lblShortFallDays As common.Controls.MyLabel
    Friend WithEvents txtReasonForLeaving As common.Controls.MyTextBox
    Friend WithEvents lblReasonofLeaving As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents lblServiceRenderedPeriod As common.Controls.MyLabel
    Friend WithEvents lblLastSalaryMonth As common.Controls.MyLabel
    Friend WithEvents txtNoofDaysLastSal As common.Controls.MyTextBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents gvDeductions As common.UserControls.MyRadGridView
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtNetAmountPayable As common.Controls.MyLabel
    Friend WithEvents lblNetAmountPayable As common.Controls.MyLabel
    Friend WithEvents txtTotalDuction As common.Controls.MyLabel
    Friend WithEvents lblTotalDeduction As common.Controls.MyLabel
    Friend WithEvents lblTotalEarnings As common.Controls.MyLabel
    Friend WithEvents lblTotalOthrEarnings As common.Controls.MyLabel
    Friend WithEvents txtTotalUnpaidSalary As common.Controls.MyLabel
    Friend WithEvents lblTotalUnpaidSalary As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents btnPrintResignation As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrintFinalDeclaration As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnPrintNoDues As Telerik.WinControls.UI.RadButton
    Friend WithEvents DocDate As common.Controls.MyLabel
    Friend WithEvents txtdocdate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpChequeDated As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtChequeNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtPayPeriod As common.UserControls.txtFinder
    Friend WithEvents dtpClearDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblClearanceDate As common.Controls.MyLabel
    Friend WithEvents lblchqAmt As common.Controls.MyLabel
    Friend WithEvents txtChequeAmt As common.MyNumBox
End Class
