<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGatePassTransfer
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
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.TxtTotalQty = New common.MyNumBox()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.LblVehicleName = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.FndVehicleCode = New common.UserControls.txtFinder()
        Me.lblBranchName = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.FndBranch = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.FndBooking = New common.UserControls.txtFinder()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.fndLocationCode = New common.UserControls.txtFinder()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.BtnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtCrate = New common.MyNumBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtJaali = New common.MyNumBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtBox = New common.MyNumBox()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTotalQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblVehicleName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBranchName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtJaali, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBox, System.ComponentModel.ISupportInitialize).BeginInit()
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
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(908, 518)
        Me.SplitContainer1.SplitterDistance = 480
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(908, 480)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.txtBox)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.txtJaali)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtCrate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.TxtTotalQty)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Controls.Add(Me.LblVehicleName)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.FndVehicleCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblBranchName)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.FndBranch)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.FndBooking)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocationName)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.fndLocationCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(114.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(887, 434)
        Me.RadPageViewPage1.Text = "Gate Pass Transfer"
        '
        'MyLabel1
        '
        Me.MyLabel1.BackColor = System.Drawing.Color.Transparent
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(13, 129)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(52, 18)
        Me.MyLabel1.TabIndex = 21
        Me.MyLabel1.Text = "Total Qty"
        '
        'TxtTotalQty
        '
        Me.TxtTotalQty.BackColor = System.Drawing.Color.White
        Me.TxtTotalQty.CalculationExpression = Nothing
        Me.TxtTotalQty.DecimalPlaces = 2
        Me.TxtTotalQty.FieldCode = Nothing
        Me.TxtTotalQty.FieldDesc = Nothing
        Me.TxtTotalQty.FieldMaxLength = 0
        Me.TxtTotalQty.FieldName = Nothing
        Me.TxtTotalQty.isCalculatedField = False
        Me.TxtTotalQty.IsSourceFromTable = False
        Me.TxtTotalQty.IsSourceFromValueList = False
        Me.TxtTotalQty.IsUnique = False
        Me.TxtTotalQty.Location = New System.Drawing.Point(102, 128)
        Me.TxtTotalQty.MendatroryField = False
        Me.TxtTotalQty.MyLinkLable1 = Nothing
        Me.TxtTotalQty.MyLinkLable2 = Nothing
        Me.TxtTotalQty.Name = "TxtTotalQty"
        Me.TxtTotalQty.ReadOnly = True
        Me.TxtTotalQty.ReferenceFieldDesc = Nothing
        Me.TxtTotalQty.ReferenceFieldName = Nothing
        Me.TxtTotalQty.ReferenceTableName = Nothing
        Me.TxtTotalQty.Size = New System.Drawing.Size(171, 20)
        Me.TxtTotalQty.TabIndex = 22
        Me.TxtTotalQty.Text = "0"
        Me.TxtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtTotalQty.Value = 0.0R
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(394, 0)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'LblVehicleName
        '
        Me.LblVehicleName.AutoSize = False
        Me.LblVehicleName.BorderVisible = True
        Me.LblVehicleName.FieldName = Nothing
        Me.LblVehicleName.Location = New System.Drawing.Point(273, 78)
        Me.LblVehicleName.Name = "LblVehicleName"
        Me.LblVehicleName.Size = New System.Drawing.Size(307, 20)
        Me.LblVehicleName.TabIndex = 345
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(13, 77)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel6.TabIndex = 343
        Me.MyLabel6.Text = "Vehicle No"
        '
        'FndVehicleCode
        '
        Me.FndVehicleCode.CalculationExpression = Nothing
        Me.FndVehicleCode.FieldCode = Nothing
        Me.FndVehicleCode.FieldDesc = Nothing
        Me.FndVehicleCode.FieldMaxLength = 0
        Me.FndVehicleCode.FieldName = Nothing
        Me.FndVehicleCode.isCalculatedField = False
        Me.FndVehicleCode.IsSourceFromTable = False
        Me.FndVehicleCode.IsSourceFromValueList = False
        Me.FndVehicleCode.IsUnique = False
        Me.FndVehicleCode.Location = New System.Drawing.Point(101, 78)
        Me.FndVehicleCode.MendatroryField = True
        Me.FndVehicleCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndVehicleCode.MyLinkLable1 = Nothing
        Me.FndVehicleCode.MyLinkLable2 = Nothing
        Me.FndVehicleCode.MyReadOnly = False
        Me.FndVehicleCode.MyShowMasterFormButton = False
        Me.FndVehicleCode.Name = "FndVehicleCode"
        Me.FndVehicleCode.ReferenceFieldDesc = Nothing
        Me.FndVehicleCode.ReferenceFieldName = Nothing
        Me.FndVehicleCode.ReferenceTableName = Nothing
        Me.FndVehicleCode.Size = New System.Drawing.Size(172, 20)
        Me.FndVehicleCode.TabIndex = 5
        Me.FndVehicleCode.Value = ""
        '
        'lblBranchName
        '
        Me.lblBranchName.AutoSize = False
        Me.lblBranchName.BorderVisible = True
        Me.lblBranchName.FieldName = Nothing
        Me.lblBranchName.Location = New System.Drawing.Point(273, 52)
        Me.lblBranchName.Name = "lblBranchName"
        Me.lblBranchName.Size = New System.Drawing.Size(307, 20)
        Me.lblBranchName.TabIndex = 342
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(13, 52)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(42, 16)
        Me.MyLabel4.TabIndex = 340
        Me.MyLabel4.Text = "Branch"
        '
        'FndBranch
        '
        Me.FndBranch.CalculationExpression = Nothing
        Me.FndBranch.Enabled = False
        Me.FndBranch.FieldCode = Nothing
        Me.FndBranch.FieldDesc = Nothing
        Me.FndBranch.FieldMaxLength = 0
        Me.FndBranch.FieldName = Nothing
        Me.FndBranch.isCalculatedField = False
        Me.FndBranch.IsSourceFromTable = False
        Me.FndBranch.IsSourceFromValueList = False
        Me.FndBranch.IsUnique = False
        Me.FndBranch.Location = New System.Drawing.Point(101, 52)
        Me.FndBranch.MendatroryField = True
        Me.FndBranch.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndBranch.MyLinkLable1 = Nothing
        Me.FndBranch.MyLinkLable2 = Nothing
        Me.FndBranch.MyReadOnly = False
        Me.FndBranch.MyShowMasterFormButton = False
        Me.FndBranch.Name = "FndBranch"
        Me.FndBranch.ReferenceFieldDesc = Nothing
        Me.FndBranch.ReferenceFieldName = Nothing
        Me.FndBranch.ReferenceTableName = Nothing
        Me.FndBranch.Size = New System.Drawing.Size(172, 20)
        Me.FndBranch.TabIndex = 4
        Me.FndBranch.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(13, 28)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel3.TabIndex = 7
        Me.MyLabel3.Text = "Booking No"
        '
        'FndBooking
        '
        Me.FndBooking.CalculationExpression = Nothing
        Me.FndBooking.FieldCode = Nothing
        Me.FndBooking.FieldDesc = Nothing
        Me.FndBooking.FieldMaxLength = 0
        Me.FndBooking.FieldName = Nothing
        Me.FndBooking.isCalculatedField = False
        Me.FndBooking.IsSourceFromTable = False
        Me.FndBooking.IsSourceFromValueList = False
        Me.FndBooking.IsUnique = False
        Me.FndBooking.Location = New System.Drawing.Point(101, 26)
        Me.FndBooking.MendatroryField = True
        Me.FndBooking.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndBooking.MyLinkLable1 = Nothing
        Me.FndBooking.MyLinkLable2 = Nothing
        Me.FndBooking.MyReadOnly = False
        Me.FndBooking.MyShowMasterFormButton = False
        Me.FndBooking.Name = "FndBooking"
        Me.FndBooking.ReferenceFieldDesc = Nothing
        Me.FndBooking.ReferenceFieldName = Nothing
        Me.FndBooking.ReferenceTableName = Nothing
        Me.FndBooking.Size = New System.Drawing.Size(172, 20)
        Me.FndBooking.TabIndex = 3
        Me.FndBooking.Value = ""
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Location = New System.Drawing.Point(273, 104)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(307, 20)
        Me.lblLocationName.TabIndex = 6
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(599, -1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(92, 22)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 334
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel10.Location = New System.Drawing.Point(13, 2)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(80, 16)
        Me.MyLabel10.TabIndex = 0
        Me.MyLabel10.Text = "Gate Pass No."
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(13, 106)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel5.TabIndex = 4
        Me.MyLabel5.Text = "Location"
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
        Me.txtDate.Location = New System.Drawing.Point(454, 1)
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
        Me.txtDate.TabIndex = 2
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(423, 2)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 2
        Me.RadLabel4.Text = "Date"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 159)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(882, 270)
        Me.RadGroupBox2.TabIndex = 0
        Me.RadGroupBox2.Text = "Item Details"
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
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(862, 240)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'fndLocationCode
        '
        Me.fndLocationCode.CalculationExpression = Nothing
        Me.fndLocationCode.FieldCode = Nothing
        Me.fndLocationCode.FieldDesc = Nothing
        Me.fndLocationCode.FieldMaxLength = 0
        Me.fndLocationCode.FieldName = Nothing
        Me.fndLocationCode.isCalculatedField = False
        Me.fndLocationCode.IsSourceFromTable = False
        Me.fndLocationCode.IsSourceFromValueList = False
        Me.fndLocationCode.IsUnique = False
        Me.fndLocationCode.Location = New System.Drawing.Point(101, 104)
        Me.fndLocationCode.MendatroryField = True
        Me.fndLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocationCode.MyLinkLable1 = Nothing
        Me.fndLocationCode.MyLinkLable2 = Nothing
        Me.fndLocationCode.MyReadOnly = False
        Me.fndLocationCode.MyShowMasterFormButton = False
        Me.fndLocationCode.Name = "fndLocationCode"
        Me.fndLocationCode.ReferenceFieldDesc = Nothing
        Me.fndLocationCode.ReferenceFieldName = Nothing
        Me.fndLocationCode.ReferenceTableName = Nothing
        Me.fndLocationCode.Size = New System.Drawing.Size(172, 20)
        Me.fndLocationCode.TabIndex = 6
        Me.fndLocationCode.Value = ""
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(101, 0)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Nothing
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(287, 21)
        Me.txtDocNo.TabIndex = 1
        Me.txtDocNo.Value = ""
        '
        'BtnPrint
        '
        Me.BtnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrint.Location = New System.Drawing.Point(172, 7)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(73, 20)
        Me.BtnPrint.TabIndex = 8
        Me.BtnPrint.Text = "Print"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(249, 7)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 5
        Me.btnPost.Text = "Post"
        Me.btnPost.Visible = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(822, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 7
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(92, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 6
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(13, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 4
        Me.btnsave.Text = "Save"
        '
        'MyLabel2
        '
        Me.MyLabel2.BackColor = System.Drawing.Color.Transparent
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(276, 129)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel2.TabIndex = 347
        Me.MyLabel2.Text = "Crate"
        '
        'txtCrate
        '
        Me.txtCrate.BackColor = System.Drawing.Color.White
        Me.txtCrate.CalculationExpression = Nothing
        Me.txtCrate.DecimalPlaces = 0
        Me.txtCrate.FieldCode = Nothing
        Me.txtCrate.FieldDesc = Nothing
        Me.txtCrate.FieldMaxLength = 0
        Me.txtCrate.FieldName = Nothing
        Me.txtCrate.isCalculatedField = False
        Me.txtCrate.IsSourceFromTable = False
        Me.txtCrate.IsSourceFromValueList = False
        Me.txtCrate.IsUnique = False
        Me.txtCrate.Location = New System.Drawing.Point(315, 128)
        Me.txtCrate.MendatroryField = True
        Me.txtCrate.MyLinkLable1 = Me.MyLabel2
        Me.txtCrate.MyLinkLable2 = Nothing
        Me.txtCrate.Name = "txtCrate"
        Me.txtCrate.ReferenceFieldDesc = Nothing
        Me.txtCrate.ReferenceFieldName = Nothing
        Me.txtCrate.ReferenceTableName = Nothing
        Me.txtCrate.Size = New System.Drawing.Size(62, 20)
        Me.txtCrate.TabIndex = 7
        Me.txtCrate.Text = "0"
        Me.txtCrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCrate.Value = 0.0R
        '
        'MyLabel7
        '
        Me.MyLabel7.BackColor = System.Drawing.Color.Transparent
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(382, 129)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(27, 18)
        Me.MyLabel7.TabIndex = 349
        Me.MyLabel7.Text = "Jaali"
        '
        'txtJaali
        '
        Me.txtJaali.BackColor = System.Drawing.Color.White
        Me.txtJaali.CalculationExpression = Nothing
        Me.txtJaali.DecimalPlaces = 0
        Me.txtJaali.FieldCode = Nothing
        Me.txtJaali.FieldDesc = Nothing
        Me.txtJaali.FieldMaxLength = 0
        Me.txtJaali.FieldName = Nothing
        Me.txtJaali.isCalculatedField = False
        Me.txtJaali.IsSourceFromTable = False
        Me.txtJaali.IsSourceFromValueList = False
        Me.txtJaali.IsUnique = False
        Me.txtJaali.Location = New System.Drawing.Point(417, 128)
        Me.txtJaali.MendatroryField = True
        Me.txtJaali.MyLinkLable1 = Me.MyLabel7
        Me.txtJaali.MyLinkLable2 = Nothing
        Me.txtJaali.Name = "txtJaali"
        Me.txtJaali.ReferenceFieldDesc = Nothing
        Me.txtJaali.ReferenceFieldName = Nothing
        Me.txtJaali.ReferenceTableName = Nothing
        Me.txtJaali.Size = New System.Drawing.Size(62, 20)
        Me.txtJaali.TabIndex = 8
        Me.txtJaali.Text = "0"
        Me.txtJaali.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtJaali.Value = 0.0R
        '
        'MyLabel8
        '
        Me.MyLabel8.BackColor = System.Drawing.Color.Transparent
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Location = New System.Drawing.Point(488, 129)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(24, 18)
        Me.MyLabel8.TabIndex = 23
        Me.MyLabel8.Text = "Box"
        '
        'txtBox
        '
        Me.txtBox.BackColor = System.Drawing.Color.White
        Me.txtBox.CalculationExpression = Nothing
        Me.txtBox.DecimalPlaces = 0
        Me.txtBox.FieldCode = Nothing
        Me.txtBox.FieldDesc = Nothing
        Me.txtBox.FieldMaxLength = 0
        Me.txtBox.FieldName = Nothing
        Me.txtBox.isCalculatedField = False
        Me.txtBox.IsSourceFromTable = False
        Me.txtBox.IsSourceFromValueList = False
        Me.txtBox.IsUnique = False
        Me.txtBox.Location = New System.Drawing.Point(517, 128)
        Me.txtBox.MendatroryField = True
        Me.txtBox.MyLinkLable1 = Me.MyLabel8
        Me.txtBox.MyLinkLable2 = Nothing
        Me.txtBox.Name = "txtBox"
        Me.txtBox.ReferenceFieldDesc = Nothing
        Me.txtBox.ReferenceFieldName = Nothing
        Me.txtBox.ReferenceTableName = Nothing
        Me.txtBox.Size = New System.Drawing.Size(62, 20)
        Me.txtBox.TabIndex = 9
        Me.txtBox.Text = "0"
        Me.txtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBox.Value = 0.0R
        '
        'FrmGatePassTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(908, 518)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmGatePassTransfer"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Gate Pass Transfer"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTotalQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblVehicleName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBranchName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCrate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtJaali, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents FndBooking As common.UserControls.txtFinder
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents fndLocationCode As common.UserControls.txtFinder
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents LblVehicleName As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents FndVehicleCode As common.UserControls.txtFinder
    Friend WithEvents lblBranchName As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents FndBranch As common.UserControls.txtFinder
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents TxtTotalQty As common.MyNumBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtBox As common.MyNumBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtJaali As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtCrate As common.MyNumBox
End Class
