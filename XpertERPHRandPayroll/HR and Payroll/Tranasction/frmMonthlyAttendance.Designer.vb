Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMonthlyAttendance
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMonthlyAttendance))
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.txtGo = New Telerik.WinControls.UI.RadButton()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.lblFromDate = New common.Controls.MyLabel()
        Me.txtPayPeriodName = New common.Controls.MyLabel()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtBranch = New common.UserControls.txtFinder()
        Me.gvMonthlyAttendance = New common.UserControls.MyRadGridView()
        Me.lblAttendanceCode = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.lblPayPeriod = New common.Controls.MyLabel()
        Me.llbPPName = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.findEnteredBy = New common.UserControls.txtFinder()
        Me.lblEnteredBy = New common.Controls.MyLabel()
        Me.txtPayPeriodDays = New common.Controls.MyTextBox()
        Me.findPayperiod = New common.UserControls.txtFinder()
        Me.lblRemarks = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.radPageAttachment = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New XpertERPHRandPayroll.ucCustomFields()
        Me.RadPageAttendanceDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvAttendanceDetail = New common.UserControls.MyRadGridView()
        Me.RadPageAttendanceSummary = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblCOFFBal = New common.Controls.MyLabel()
        Me.lblCLBal = New common.Controls.MyLabel()
        Me.lblELBal = New common.Controls.MyLabel()
        Me.lblOTHERBal = New common.Controls.MyLabel()
        Me.lblMEDBal = New common.Controls.MyLabel()
        Me.lblMATRLBal = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblMedicalLeave = New common.Controls.MyLabel()
        Me.lblCOff = New common.Controls.MyLabel()
        Me.lblCasualLeave = New common.Controls.MyLabel()
        Me.lblOtherLeave = New common.Controls.MyLabel()
        Me.lblMaternityLeave = New common.Controls.MyLabel()
        Me.lblEarnedLeave = New common.Controls.MyLabel()
        Me.gvAttendanceSummary = New common.UserControls.MyRadGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.mnuFile = New Telerik.WinControls.UI.RadMenuItem()
        Me.Save_Layout = New Telerik.WinControls.UI.RadMenuItem()
        Me.Delete_Layout = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export = New Telerik.WinControls.UI.RadMenuItem()
        Me.Import = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export_Grid = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnBiometric = New Telerik.WinControls.UI.RadButton()
        Me.btnDeleteDS = New Telerik.WinControls.UI.RadButton()
        Me.btnFillAttendance = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.ExcelAttendanceDetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.ExcelAttendanceSummary = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnAttendanceDetailExport = New Telerik.WinControls.UI.RadButton()
        Me.btnUpdateTime = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPayPeriodName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvMonthlyAttendance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvMonthlyAttendance.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAttendanceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.llbPPName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEnteredBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPayPeriodDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.radPageAttachment.SuspendLayout()
        Me.pvpCustomFields.SuspendLayout()
        Me.RadPageAttendanceDetail.SuspendLayout()
        CType(Me.gvAttendanceDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAttendanceDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageAttendanceSummary.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCOFFBal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCLBal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblELBal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOTHERBal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMEDBal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMATRLBal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMedicalLeave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCOff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCasualLeave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOtherLeave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMaternityLeave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEarnedLeave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAttendanceSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAttendanceSummary.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBiometric, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDeleteDS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnFillAttendance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAttendanceDetailExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpdateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(936, 538)
        Me.RadGroupBox3.TabIndex = 66
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnBiometric)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDeleteDS)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnFillAttendance)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAttendanceDetailExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUpdateTime)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(936, 538)
        Me.SplitContainer1.SplitterDistance = 504
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.radPageAttachment)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Controls.Add(Me.RadPageAttendanceDetail)
        Me.RadPageView1.Controls.Add(Me.RadPageAttendanceSummary)
        Me.RadPageView1.Location = New System.Drawing.Point(3, 23)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(930, 481)
        Me.RadPageView1.TabIndex = 211
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadButton1)
        Me.RadPageViewPage1.Controls.Add(Me.txtGo)
        Me.RadPageViewPage1.Controls.Add(Me.btnNew)
        Me.RadPageViewPage1.Controls.Add(Me.lblToDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblFromDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtPayPeriodName)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocationDesc)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.txtBranch)
        Me.RadPageViewPage1.Controls.Add(Me.gvMonthlyAttendance)
        Me.RadPageViewPage1.Controls.Add(Me.lblAttendanceCode)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.lblPayPeriod)
        Me.RadPageViewPage1.Controls.Add(Me.llbPPName)
        Me.RadPageViewPage1.Controls.Add(Me.txtCode)
        Me.RadPageViewPage1.Controls.Add(Me.findEnteredBy)
        Me.RadPageViewPage1.Controls.Add(Me.txtPayPeriodDays)
        Me.RadPageViewPage1.Controls.Add(Me.lblEnteredBy)
        Me.RadPageViewPage1.Controls.Add(Me.findPayperiod)
        Me.RadPageViewPage1.Controls.Add(Me.lblRemarks)
        Me.RadPageViewPage1.Controls.Add(Me.txtDescription)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(73.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(909, 433)
        Me.RadPageViewPage1.Text = "Attendance"
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(778, 126)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(66, 18)
        Me.RadButton1.TabIndex = 258
        Me.RadButton1.Text = "Biometric "
        '
        'txtGo
        '
        Me.txtGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGo.Location = New System.Drawing.Point(724, 126)
        Me.txtGo.Name = "txtGo"
        Me.txtGo.Size = New System.Drawing.Size(50, 18)
        Me.txtGo.TabIndex = 255
        Me.txtGo.Text = ">>"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(395, 1)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = False
        Me.lblToDate.BorderVisible = True
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(395, 67)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(244, 19)
        Me.lblToDate.TabIndex = 257
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = False
        Me.lblFromDate.BorderVisible = True
        Me.lblFromDate.FieldName = Nothing
        Me.lblFromDate.Location = New System.Drawing.Point(148, 68)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(244, 19)
        Me.lblFromDate.TabIndex = 256
        '
        'txtPayPeriodName
        '
        Me.txtPayPeriodName.AutoSize = False
        Me.txtPayPeriodName.BorderVisible = True
        Me.txtPayPeriodName.FieldName = Nothing
        Me.txtPayPeriodName.Location = New System.Drawing.Point(395, 45)
        Me.txtPayPeriodName.Name = "txtPayPeriodName"
        Me.txtPayPeriodName.Size = New System.Drawing.Size(323, 19)
        Me.txtPayPeriodName.TabIndex = 254
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.Location = New System.Drawing.Point(395, 25)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(323, 19)
        Me.lblLocationDesc.TabIndex = 253
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(27, 25)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel9.TabIndex = 252
        Me.MyLabel9.Text = "Location"
        '
        'txtBranch
        '
        Me.txtBranch.CalculationExpression = Nothing
        Me.txtBranch.FieldCode = Nothing
        Me.txtBranch.FieldDesc = Nothing
        Me.txtBranch.FieldMaxLength = 0
        Me.txtBranch.FieldName = Nothing
        Me.txtBranch.isCalculatedField = False
        Me.txtBranch.IsSourceFromTable = False
        Me.txtBranch.IsSourceFromValueList = False
        Me.txtBranch.IsUnique = False
        Me.txtBranch.Location = New System.Drawing.Point(148, 25)
        Me.txtBranch.MendatroryField = True
        Me.txtBranch.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBranch.MyLinkLable1 = Me.MyLabel9
        Me.txtBranch.MyLinkLable2 = Nothing
        Me.txtBranch.MyReadOnly = False
        Me.txtBranch.MyShowMasterFormButton = False
        Me.txtBranch.Name = "txtBranch"
        Me.txtBranch.ReferenceFieldDesc = Nothing
        Me.txtBranch.ReferenceFieldName = Nothing
        Me.txtBranch.ReferenceTableName = Nothing
        Me.txtBranch.Size = New System.Drawing.Size(244, 19)
        Me.txtBranch.TabIndex = 251
        Me.txtBranch.Value = ""
        '
        'gvMonthlyAttendance
        '
        Me.gvMonthlyAttendance.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvMonthlyAttendance.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvMonthlyAttendance.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvMonthlyAttendance.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvMonthlyAttendance.ForeColor = System.Drawing.Color.Black
        Me.gvMonthlyAttendance.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvMonthlyAttendance.Location = New System.Drawing.Point(0, 150)
        '
        '
        '
        Me.gvMonthlyAttendance.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvMonthlyAttendance.MasterTemplate.AllowAddNewRow = False
        Me.gvMonthlyAttendance.MasterTemplate.AutoGenerateColumns = False
        Me.gvMonthlyAttendance.MasterTemplate.EnableGrouping = False
        Me.gvMonthlyAttendance.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvMonthlyAttendance.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvMonthlyAttendance.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvMonthlyAttendance.MyStopExport = False
        Me.gvMonthlyAttendance.Name = "gvMonthlyAttendance"
        Me.gvMonthlyAttendance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvMonthlyAttendance.ShowHeaderCellButtons = True
        Me.gvMonthlyAttendance.Size = New System.Drawing.Size(906, 283)
        Me.gvMonthlyAttendance.TabIndex = 7
        '
        'lblAttendanceCode
        '
        Me.lblAttendanceCode.FieldName = Nothing
        Me.lblAttendanceCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAttendanceCode.Location = New System.Drawing.Point(27, 3)
        Me.lblAttendanceCode.Name = "lblAttendanceCode"
        Me.lblAttendanceCode.Size = New System.Drawing.Size(94, 16)
        Me.lblAttendanceCode.TabIndex = 161
        Me.lblAttendanceCode.Text = "Attendance Code"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(618, 3)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 21)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 209
        '
        'lblPayPeriod
        '
        Me.lblPayPeriod.FieldName = Nothing
        Me.lblPayPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayPeriod.Location = New System.Drawing.Point(27, 47)
        Me.lblPayPeriod.Name = "lblPayPeriod"
        Me.lblPayPeriod.Size = New System.Drawing.Size(92, 16)
        Me.lblPayPeriod.TabIndex = 153
        Me.lblPayPeriod.Text = "Pay Period Code"
        '
        'llbPPName
        '
        Me.llbPPName.FieldName = Nothing
        Me.llbPPName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbPPName.Location = New System.Drawing.Point(27, 91)
        Me.llbPPName.Name = "llbPPName"
        Me.llbPPName.Size = New System.Drawing.Size(60, 16)
        Me.llbPPName.TabIndex = 183
        Me.llbPPName.Text = "No of days"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(148, 1)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblAttendanceCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(244, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'findEnteredBy
        '
        Me.findEnteredBy.CalculationExpression = Nothing
        Me.findEnteredBy.FieldCode = Nothing
        Me.findEnteredBy.FieldDesc = Nothing
        Me.findEnteredBy.FieldMaxLength = 0
        Me.findEnteredBy.FieldName = Nothing
        Me.findEnteredBy.isCalculatedField = False
        Me.findEnteredBy.IsSourceFromTable = False
        Me.findEnteredBy.IsSourceFromValueList = False
        Me.findEnteredBy.IsUnique = False
        Me.findEnteredBy.Location = New System.Drawing.Point(457, 89)
        Me.findEnteredBy.MendatroryField = False
        Me.findEnteredBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findEnteredBy.MyLinkLable1 = Me.lblEnteredBy
        Me.findEnteredBy.MyLinkLable2 = Nothing
        Me.findEnteredBy.MyReadOnly = False
        Me.findEnteredBy.MyShowMasterFormButton = False
        Me.findEnteredBy.Name = "findEnteredBy"
        Me.findEnteredBy.ReferenceFieldDesc = Nothing
        Me.findEnteredBy.ReferenceFieldName = Nothing
        Me.findEnteredBy.ReferenceTableName = Nothing
        Me.findEnteredBy.Size = New System.Drawing.Size(261, 19)
        Me.findEnteredBy.TabIndex = 2
        Me.findEnteredBy.Value = ""
        '
        'lblEnteredBy
        '
        Me.lblEnteredBy.FieldName = Nothing
        Me.lblEnteredBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnteredBy.Location = New System.Drawing.Point(395, 90)
        Me.lblEnteredBy.Name = "lblEnteredBy"
        Me.lblEnteredBy.Size = New System.Drawing.Size(62, 16)
        Me.lblEnteredBy.TabIndex = 165
        Me.lblEnteredBy.Text = "Entered By"
        '
        'txtPayPeriodDays
        '
        Me.txtPayPeriodDays.CalculationExpression = Nothing
        Me.txtPayPeriodDays.FieldCode = Nothing
        Me.txtPayPeriodDays.FieldDesc = Nothing
        Me.txtPayPeriodDays.FieldMaxLength = 0
        Me.txtPayPeriodDays.FieldName = Nothing
        Me.txtPayPeriodDays.isCalculatedField = False
        Me.txtPayPeriodDays.IsSourceFromTable = False
        Me.txtPayPeriodDays.IsSourceFromValueList = False
        Me.txtPayPeriodDays.IsUnique = False
        Me.txtPayPeriodDays.Location = New System.Drawing.Point(148, 88)
        Me.txtPayPeriodDays.MaxLength = 50
        Me.txtPayPeriodDays.MendatroryField = False
        Me.txtPayPeriodDays.MyLinkLable1 = Me.lblPayPeriod
        Me.txtPayPeriodDays.MyLinkLable2 = Nothing
        Me.txtPayPeriodDays.Name = "txtPayPeriodDays"
        Me.txtPayPeriodDays.ReadOnly = True
        Me.txtPayPeriodDays.ReferenceFieldDesc = Nothing
        Me.txtPayPeriodDays.ReferenceFieldName = Nothing
        Me.txtPayPeriodDays.ReferenceTableName = Nothing
        Me.txtPayPeriodDays.Size = New System.Drawing.Size(244, 20)
        Me.txtPayPeriodDays.TabIndex = 4
        Me.txtPayPeriodDays.TabStop = False
        '
        'findPayperiod
        '
        Me.findPayperiod.CalculationExpression = Nothing
        Me.findPayperiod.FieldCode = Nothing
        Me.findPayperiod.FieldDesc = Nothing
        Me.findPayperiod.FieldMaxLength = 0
        Me.findPayperiod.FieldName = Nothing
        Me.findPayperiod.isCalculatedField = False
        Me.findPayperiod.IsSourceFromTable = False
        Me.findPayperiod.IsSourceFromValueList = False
        Me.findPayperiod.IsUnique = False
        Me.findPayperiod.Location = New System.Drawing.Point(148, 46)
        Me.findPayperiod.MendatroryField = True
        Me.findPayperiod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findPayperiod.MyLinkLable1 = Me.lblEnteredBy
        Me.findPayperiod.MyLinkLable2 = Nothing
        Me.findPayperiod.MyReadOnly = False
        Me.findPayperiod.MyShowMasterFormButton = False
        Me.findPayperiod.Name = "findPayperiod"
        Me.findPayperiod.ReferenceFieldDesc = Nothing
        Me.findPayperiod.ReferenceFieldName = Nothing
        Me.findPayperiod.ReferenceTableName = Nothing
        Me.findPayperiod.Size = New System.Drawing.Size(244, 19)
        Me.findPayperiod.TabIndex = 3
        Me.findPayperiod.Value = ""
        '
        'lblRemarks
        '
        Me.lblRemarks.FieldName = Nothing
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(27, 111)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(51, 16)
        Me.lblRemarks.TabIndex = 177
        Me.lblRemarks.Text = "Remarks"
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
        Me.txtDescription.Location = New System.Drawing.Point(148, 111)
        Me.txtDescription.MaxLength = 49
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.lblRemarks
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(570, 34)
        Me.txtDescription.TabIndex = 5
        '
        'radPageAttachment
        '
        Me.radPageAttachment.Controls.Add(Me.UcAttachment1)
        Me.radPageAttachment.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.radPageAttachment.Location = New System.Drawing.Point(10, 37)
        Me.radPageAttachment.Name = "radPageAttachment"
        Me.radPageAttachment.Size = New System.Drawing.Size(847, 433)
        Me.radPageAttachment.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(847, 433)
        Me.UcAttachment1.TabIndex = 2
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(847, 433)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(847, 433)
        Me.UcCustomFields1.TabIndex = 1
        '
        'RadPageAttendanceDetail
        '
        Me.RadPageAttendanceDetail.Controls.Add(Me.gvAttendanceDetail)
        Me.RadPageAttendanceDetail.ItemSize = New System.Drawing.SizeF(106.0!, 28.0!)
        Me.RadPageAttendanceDetail.Location = New System.Drawing.Point(10, 37)
        Me.RadPageAttendanceDetail.Name = "RadPageAttendanceDetail"
        Me.RadPageAttendanceDetail.Size = New System.Drawing.Size(847, 433)
        Me.RadPageAttendanceDetail.Text = "Attendance Detail"
        '
        'gvAttendanceDetail
        '
        Me.gvAttendanceDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvAttendanceDetail.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvAttendanceDetail.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvAttendanceDetail.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvAttendanceDetail.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gvAttendanceDetail.MyStopExport = False
        Me.gvAttendanceDetail.Name = "gvAttendanceDetail"
        Me.gvAttendanceDetail.ShowHeaderCellButtons = True
        Me.gvAttendanceDetail.Size = New System.Drawing.Size(847, 433)
        Me.gvAttendanceDetail.TabIndex = 2
        '
        'RadPageAttendanceSummary
        '
        Me.RadPageAttendanceSummary.Controls.Add(Me.SplitContainer2)
        Me.RadPageAttendanceSummary.ItemSize = New System.Drawing.SizeF(124.0!, 28.0!)
        Me.RadPageAttendanceSummary.Location = New System.Drawing.Point(10, 37)
        Me.RadPageAttendanceSummary.Name = "RadPageAttendanceSummary"
        Me.RadPageAttendanceSummary.Size = New System.Drawing.Size(847, 433)
        Me.RadPageAttendanceSummary.Text = "Attendance Summary"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer2.Panel1MinSize = 60
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvAttendanceSummary)
        Me.SplitContainer2.Size = New System.Drawing.Size(847, 433)
        Me.SplitContainer2.SplitterDistance = 70
        Me.SplitContainer2.TabIndex = 156
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MyLabel5)
        Me.GroupBox1.Controls.Add(Me.lblCOFFBal)
        Me.GroupBox1.Controls.Add(Me.lblCLBal)
        Me.GroupBox1.Controls.Add(Me.lblELBal)
        Me.GroupBox1.Controls.Add(Me.lblOTHERBal)
        Me.GroupBox1.Controls.Add(Me.lblMEDBal)
        Me.GroupBox1.Controls.Add(Me.lblMATRLBal)
        Me.GroupBox1.Controls.Add(Me.MyLabel4)
        Me.GroupBox1.Controls.Add(Me.MyLabel3)
        Me.GroupBox1.Controls.Add(Me.MyLabel2)
        Me.GroupBox1.Controls.Add(Me.MyLabel1)
        Me.GroupBox1.Controls.Add(Me.lblMedicalLeave)
        Me.GroupBox1.Controls.Add(Me.lblCOff)
        Me.GroupBox1.Controls.Add(Me.lblCasualLeave)
        Me.GroupBox1.Controls.Add(Me.lblOtherLeave)
        Me.GroupBox1.Controls.Add(Me.lblMaternityLeave)
        Me.GroupBox1.Controls.Add(Me.lblEarnedLeave)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(847, 70)
        Me.GroupBox1.TabIndex = 155
        Me.GroupBox1.TabStop = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(775, 24)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(66, 16)
        Me.MyLabel5.TabIndex = 170
        Me.MyLabel5.Text = "Present (P)"
        '
        'lblCOFFBal
        '
        Me.lblCOFFBal.FieldName = Nothing
        Me.lblCOFFBal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCOFFBal.Location = New System.Drawing.Point(299, 24)
        Me.lblCOFFBal.Name = "lblCOFFBal"
        Me.lblCOFFBal.Size = New System.Drawing.Size(22, 16)
        Me.lblCOFFBal.TabIndex = 169
        Me.lblCOFFBal.Text = "0.0"
        '
        'lblCLBal
        '
        Me.lblCLBal.FieldName = Nothing
        Me.lblCLBal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCLBal.Location = New System.Drawing.Point(122, 46)
        Me.lblCLBal.Name = "lblCLBal"
        Me.lblCLBal.Size = New System.Drawing.Size(22, 16)
        Me.lblCLBal.TabIndex = 168
        Me.lblCLBal.Text = "0.0"
        '
        'lblELBal
        '
        Me.lblELBal.FieldName = Nothing
        Me.lblELBal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblELBal.Location = New System.Drawing.Point(121, 24)
        Me.lblELBal.Name = "lblELBal"
        Me.lblELBal.Size = New System.Drawing.Size(22, 16)
        Me.lblELBal.TabIndex = 167
        Me.lblELBal.Text = "0.0"
        '
        'lblOTHERBal
        '
        Me.lblOTHERBal.FieldName = Nothing
        Me.lblOTHERBal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOTHERBal.Location = New System.Drawing.Point(474, 47)
        Me.lblOTHERBal.Name = "lblOTHERBal"
        Me.lblOTHERBal.Size = New System.Drawing.Size(22, 16)
        Me.lblOTHERBal.TabIndex = 166
        Me.lblOTHERBal.Text = "0.0"
        '
        'lblMEDBal
        '
        Me.lblMEDBal.FieldName = Nothing
        Me.lblMEDBal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMEDBal.Location = New System.Drawing.Point(474, 24)
        Me.lblMEDBal.Name = "lblMEDBal"
        Me.lblMEDBal.Size = New System.Drawing.Size(22, 16)
        Me.lblMEDBal.TabIndex = 165
        Me.lblMEDBal.Text = "0.0"
        '
        'lblMATRLBal
        '
        Me.lblMATRLBal.FieldName = Nothing
        Me.lblMATRLBal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMATRLBal.Location = New System.Drawing.Point(299, 47)
        Me.lblMATRLBal.Name = "lblMATRLBal"
        Me.lblMATRLBal.Size = New System.Drawing.Size(22, 16)
        Me.lblMATRLBal.TabIndex = 164
        Me.lblMATRLBal.Text = "0.0"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(553, 24)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(95, 16)
        Me.MyLabel4.TabIndex = 163
        Me.MyLabel4.Text = "Weekly Off (WO)"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(553, 47)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel3.TabIndex = 162
        Me.MyLabel3.Text = "Absent (A)"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(674, 47)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel2.TabIndex = 161
        Me.MyLabel2.Text = "Holiday (HO)"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(674, 24)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel1.TabIndex = 160
        Me.MyLabel1.Text = "Half Day (HD)"
        '
        'lblMedicalLeave
        '
        Me.lblMedicalLeave.FieldName = Nothing
        Me.lblMedicalLeave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMedicalLeave.Location = New System.Drawing.Point(360, 24)
        Me.lblMedicalLeave.Name = "lblMedicalLeave"
        Me.lblMedicalLeave.Size = New System.Drawing.Size(83, 16)
        Me.lblMedicalLeave.TabIndex = 159
        Me.lblMedicalLeave.Text = "Medical Leave"
        '
        'lblCOff
        '
        Me.lblCOff.FieldName = Nothing
        Me.lblCOff.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCOff.Location = New System.Drawing.Point(171, 24)
        Me.lblCOff.Name = "lblCOff"
        Me.lblCOff.Size = New System.Drawing.Size(31, 16)
        Me.lblCOff.TabIndex = 158
        Me.lblCOff.Text = "COff"
        '
        'lblCasualLeave
        '
        Me.lblCasualLeave.FieldName = Nothing
        Me.lblCasualLeave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCasualLeave.Location = New System.Drawing.Point(6, 46)
        Me.lblCasualLeave.Name = "lblCasualLeave"
        Me.lblCasualLeave.Size = New System.Drawing.Size(79, 16)
        Me.lblCasualLeave.TabIndex = 157
        Me.lblCasualLeave.Text = "Casual Leave"
        '
        'lblOtherLeave
        '
        Me.lblOtherLeave.FieldName = Nothing
        Me.lblOtherLeave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOtherLeave.Location = New System.Drawing.Point(360, 47)
        Me.lblOtherLeave.Name = "lblOtherLeave"
        Me.lblOtherLeave.Size = New System.Drawing.Size(72, 16)
        Me.lblOtherLeave.TabIndex = 156
        Me.lblOtherLeave.Text = "Other Leave"
        '
        'lblMaternityLeave
        '
        Me.lblMaternityLeave.FieldName = Nothing
        Me.lblMaternityLeave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMaternityLeave.Location = New System.Drawing.Point(171, 47)
        Me.lblMaternityLeave.Name = "lblMaternityLeave"
        Me.lblMaternityLeave.Size = New System.Drawing.Size(92, 16)
        Me.lblMaternityLeave.TabIndex = 155
        Me.lblMaternityLeave.Text = "Maternity Leave"
        '
        'lblEarnedLeave
        '
        Me.lblEarnedLeave.FieldName = Nothing
        Me.lblEarnedLeave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEarnedLeave.Location = New System.Drawing.Point(6, 24)
        Me.lblEarnedLeave.Name = "lblEarnedLeave"
        Me.lblEarnedLeave.Size = New System.Drawing.Size(80, 16)
        Me.lblEarnedLeave.TabIndex = 154
        Me.lblEarnedLeave.Text = "Earned Leave"
        '
        'gvAttendanceSummary
        '
        Me.gvAttendanceSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvAttendanceSummary.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvAttendanceSummary.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvAttendanceSummary.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvAttendanceSummary.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gvAttendanceSummary.MyStopExport = False
        Me.gvAttendanceSummary.Name = "gvAttendanceSummary"
        Me.gvAttendanceSummary.ShowHeaderCellButtons = True
        Me.gvAttendanceSummary.Size = New System.Drawing.Size(847, 359)
        Me.gvAttendanceSummary.TabIndex = 3
        '
        'RadMenu1
        '
        Me.RadMenu1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuFile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(936, 20)
        Me.RadMenu1.TabIndex = 210
        '
        'mnuFile
        '
        Me.mnuFile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Save_Layout, Me.Delete_Layout, Me.Export, Me.Import, Me.Export_Grid})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Text = "&File"
        '
        'Save_Layout
        '
        Me.Save_Layout.Name = "Save_Layout"
        Me.Save_Layout.Text = "Save Layout"
        '
        'Delete_Layout
        '
        Me.Delete_Layout.Name = "Delete_Layout"
        Me.Delete_Layout.Text = "Delete Layout"
        '
        'Export
        '
        Me.Export.Name = "Export"
        Me.Export.Text = "Export"
        '
        'Import
        '
        Me.Import.Name = "Import"
        Me.Import.Text = "Import"
        '
        'Export_Grid
        '
        Me.Export_Grid.AccessibleDescription = "Export Grid"
        Me.Export_Grid.AccessibleName = "Export Grid"
        Me.Export_Grid.Name = "Export_Grid"
        Me.Export_Grid.Text = "Export Employee"
        '
        'btnBiometric
        '
        Me.btnBiometric.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBiometric.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBiometric.Location = New System.Drawing.Point(279, 3)
        Me.btnBiometric.Name = "btnBiometric"
        Me.btnBiometric.Size = New System.Drawing.Size(66, 18)
        Me.btnBiometric.TabIndex = 259
        Me.btnBiometric.Text = "Biometric "
        '
        'btnDeleteDS
        '
        Me.btnDeleteDS.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteDS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeleteDS.Location = New System.Drawing.Point(465, 3)
        Me.btnDeleteDS.Name = "btnDeleteDS"
        Me.btnDeleteDS.Size = New System.Drawing.Size(127, 18)
        Me.btnDeleteDS.TabIndex = 9
        Me.btnDeleteDS.Text = "Delete Detail/Summary"
        '
        'btnFillAttendance
        '
        Me.btnFillAttendance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnFillAttendance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFillAttendance.Location = New System.Drawing.Point(698, 3)
        Me.btnFillAttendance.Name = "btnFillAttendance"
        Me.btnFillAttendance.Size = New System.Drawing.Size(82, 18)
        Me.btnFillAttendance.TabIndex = 8
        Me.btnFillAttendance.Text = "Fill Attendance"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.ExcelAttendanceDetail, Me.ExcelAttendanceSummary})
        Me.RadSplitButton1.Location = New System.Drawing.Point(782, 4)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(106, 17)
        Me.RadSplitButton1.TabIndex = 7
        Me.RadSplitButton1.Text = "Export To Excel"
        '
        'ExcelAttendanceDetail
        '
        Me.ExcelAttendanceDetail.Name = "ExcelAttendanceDetail"
        Me.ExcelAttendanceDetail.Text = "Attendance Detail"
        '
        'ExcelAttendanceSummary
        '
        Me.ExcelAttendanceSummary.Name = "ExcelAttendanceSummary"
        Me.ExcelAttendanceSummary.Text = "Attendance Summary"
        '
        'btnAttendanceDetailExport
        '
        Me.btnAttendanceDetailExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAttendanceDetailExport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAttendanceDetailExport.Location = New System.Drawing.Point(593, 3)
        Me.btnAttendanceDetailExport.Name = "btnAttendanceDetailExport"
        Me.btnAttendanceDetailExport.Size = New System.Drawing.Size(102, 18)
        Me.btnAttendanceDetailExport.TabIndex = 6
        Me.btnAttendanceDetailExport.Text = "Refresh Summary"
        '
        'btnUpdateTime
        '
        Me.btnUpdateTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateTime.Location = New System.Drawing.Point(347, 3)
        Me.btnUpdateTime.Name = "btnUpdateTime"
        Me.btnUpdateTime.Size = New System.Drawing.Size(116, 18)
        Me.btnUpdateTime.TabIndex = 5
        Me.btnUpdateTime.Text = "Save Detail/Summary"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(182, 3)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(66, 18)
        Me.btnReverse.TabIndex = 4
        Me.btnReverse.Text = "Reverse"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(66, 3)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(54, 18)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(54, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(890, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(42, 18)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(124, 3)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(53, 18)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'frmMonthlyAttendance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(936, 538)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Name = "frmMonthlyAttendance"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Monthly Attendance"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPayPeriodName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvMonthlyAttendance.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvMonthlyAttendance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAttendanceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.llbPPName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEnteredBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPayPeriodDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        Me.radPageAttachment.ResumeLayout(False)
        Me.pvpCustomFields.ResumeLayout(False)
        Me.RadPageAttendanceDetail.ResumeLayout(False)
        CType(Me.gvAttendanceDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAttendanceDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageAttendanceSummary.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCOFFBal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCLBal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblELBal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOTHERBal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMEDBal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMATRLBal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMedicalLeave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCOff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCasualLeave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOtherLeave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMaternityLeave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEarnedLeave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAttendanceSummary.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAttendanceSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBiometric, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDeleteDS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnFillAttendance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAttendanceDetailExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpdateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents lblEnteredBy As common.Controls.MyLabel
    Friend WithEvents findEnteredBy As common.UserControls.txtFinder
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblAttendanceCode As common.Controls.MyLabel
    Friend WithEvents lblPayPeriod As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents gvMonthlyAttendance As common.UserControls.MyRadGridView
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents findPayperiod As common.UserControls.txtFinder
    Friend WithEvents txtPayPeriodDays As common.Controls.MyTextBox
    Friend WithEvents llbPPName As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents mnuFile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ucCustomFields
    Friend WithEvents radPageAttachment As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtBranch As common.UserControls.txtFinder
    Friend WithEvents txtPayPeriodName As common.Controls.MyLabel
    Friend WithEvents txtGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblFromDate As common.Controls.MyLabel
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents Export_Grid As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Save_Layout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Delete_Layout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageAttendanceDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvAttendanceDetail As common.UserControls.MyRadGridView
    Friend WithEvents btnUpdateTime As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnAttendanceDetailExport As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageAttendanceSummary As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvAttendanceSummary As common.UserControls.MyRadGridView
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents ExcelAttendanceDetail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ExcelAttendanceSummary As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnFillAttendance As Telerik.WinControls.UI.RadButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblEarnedLeave As common.Controls.MyLabel
    Friend WithEvents lblMedicalLeave As common.Controls.MyLabel
    Friend WithEvents lblCOff As common.Controls.MyLabel
    Friend WithEvents lblCasualLeave As common.Controls.MyLabel
    Friend WithEvents lblOtherLeave As common.Controls.MyLabel
    Friend WithEvents lblMaternityLeave As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblCOFFBal As common.Controls.MyLabel
    Friend WithEvents lblCLBal As common.Controls.MyLabel
    Friend WithEvents lblELBal As common.Controls.MyLabel
    Friend WithEvents lblOTHERBal As common.Controls.MyLabel
    Friend WithEvents lblMEDBal As common.Controls.MyLabel
    Friend WithEvents lblMATRLBal As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents btnDeleteDS As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnBiometric As Telerik.WinControls.UI.RadButton
End Class
