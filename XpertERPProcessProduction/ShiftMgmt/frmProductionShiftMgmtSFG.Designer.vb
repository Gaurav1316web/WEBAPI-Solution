<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmProductionShiftMgmtSFG
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnShowInventory = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtDocNo = New common.Controls.MyLabel()
        Me.txtShift = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtShiftEnd = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtShiftStart = New common.Controls.MyDateTimePicker()
        Me.lblLocationFG = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadPageView3 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage10 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvProSFG = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage8 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvProRM = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel43 = New common.Controls.MyLabel()
        Me.MyLabel42 = New common.Controls.MyLabel()
        Me.MyLabel41 = New common.Controls.MyLabel()
        Me.MyLabel40 = New common.Controls.MyLabel()
        Me.MyLabel39 = New common.Controls.MyLabel()
        Me.MyLabel38 = New common.Controls.MyLabel()
        Me.lblTotNetSNFKG = New common.Controls.MyLabel()
        Me.lblTotNetFATKG = New common.Controls.MyLabel()
        Me.MyLabel37 = New common.Controls.MyLabel()
        Me.lblTotAddedSNFKG = New common.Controls.MyLabel()
        Me.lblTotAddedFATKG = New common.Controls.MyLabel()
        Me.lblTotDifferenceSNFKG = New common.Controls.MyLabel()
        Me.lblTotDifferenceFATKG = New common.Controls.MyLabel()
        Me.MyLabel33 = New common.Controls.MyLabel()
        Me.MyLabel29 = New common.Controls.MyLabel()
        Me.lblTotIssueSNFKG = New common.Controls.MyLabel()
        Me.lblTotProduceSNFKG = New common.Controls.MyLabel()
        Me.lblTotIssueFATKG = New common.Controls.MyLabel()
        Me.lblTotProduceFATKG = New common.Controls.MyLabel()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.lblTotRemoveSNFKG = New common.Controls.MyLabel()
        Me.lblTotRemoveFATKG = New common.Controls.MyLabel()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.Panel2.SuspendLayout()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.txtDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtShiftEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtShiftStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationFG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView3.SuspendLayout()
        Me.RadPageViewPage10.SuspendLayout()
        CType(Me.gvProSFG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvProSFG.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage8.SuspendLayout()
        CType(Me.gvProRM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvProRM.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotNetSNFKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotNetFATKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotAddedSNFKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotAddedFATKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotDifferenceSNFKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotDifferenceFATKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotIssueSNFKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotProduceSNFKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotIssueFATKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotProduceFATKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRemoveSNFKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRemoveFATKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnShowInventory)
        Me.Panel2.Controls.Add(Me.btnReverse)
        Me.Panel2.Controls.Add(Me.btnHistory)
        Me.Panel2.Controls.Add(Me.btnPost)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Controls.Add(Me.btnClose)
        Me.Panel2.Controls.Add(Me.btnSave)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 479)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1118, 37)
        Me.Panel2.TabIndex = 1
        '
        'btnShowInventory
        '
        Me.btnShowInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowInventory.Location = New System.Drawing.Point(287, 8)
        Me.btnShowInventory.Name = "btnShowInventory"
        Me.btnShowInventory.Size = New System.Drawing.Size(92, 22)
        Me.btnShowInventory.TabIndex = 41
        Me.btnShowInventory.Text = "Show Inventory"
        Me.btnShowInventory.Visible = False
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(380, 8)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(92, 22)
        Me.btnReverse.TabIndex = 40
        Me.btnReverse.Text = "Reverse"
        Me.btnReverse.Visible = False
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(474, 8)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(92, 22)
        Me.btnHistory.TabIndex = 39
        Me.btnHistory.Text = "&History"
        Me.btnHistory.Visible = False
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(190, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(92, 22)
        Me.btnPost.TabIndex = 3
        Me.btnPost.Text = "Post"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(97, 8)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(92, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1018, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(97, 22)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(4, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(92, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtDocNo)
        Me.Panel1.Controls.Add(Me.txtShift)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.txtShiftEnd)
        Me.Panel1.Controls.Add(Me.MyLabel1)
        Me.Panel1.Controls.Add(Me.txtShiftStart)
        Me.Panel1.Controls.Add(Me.MyLabel4)
        Me.Panel1.Controls.Add(Me.lblLocationFG)
        Me.Panel1.Controls.Add(Me.MyLabel5)
        Me.Panel1.Controls.Add(Me.txtLocation)
        Me.Panel1.Controls.Add(Me.UsLock1)
        Me.Panel1.Controls.Add(Me.RadLabel4)
        Me.Panel1.Controls.Add(Me.RadLabel1)
        Me.Panel1.Controls.Add(Me.txtDate)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1118, 74)
        Me.Panel1.TabIndex = 0
        '
        'txtDocNo
        '
        Me.txtDocNo.AutoSize = False
        Me.txtDocNo.BorderVisible = True
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(88, 5)
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(207, 19)
        Me.txtDocNo.TabIndex = 67
        '
        'txtShift
        '
        Me.txtShift.CalculationExpression = Nothing
        Me.txtShift.FieldCode = Nothing
        Me.txtShift.FieldDesc = Nothing
        Me.txtShift.FieldMaxLength = 0
        Me.txtShift.FieldName = Nothing
        Me.txtShift.isCalculatedField = False
        Me.txtShift.IsSourceFromTable = False
        Me.txtShift.IsSourceFromValueList = False
        Me.txtShift.IsUnique = False
        Me.txtShift.Location = New System.Drawing.Point(86, 27)
        Me.txtShift.MendatroryField = True
        Me.txtShift.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShift.MyLinkLable1 = Me.MyLabel4
        Me.txtShift.MyLinkLable2 = Nothing
        Me.txtShift.MyReadOnly = False
        Me.txtShift.MyShowMasterFormButton = False
        Me.txtShift.Name = "txtShift"
        Me.txtShift.ReferenceFieldDesc = Nothing
        Me.txtShift.ReferenceFieldName = Nothing
        Me.txtShift.ReferenceTableName = Nothing
        Me.txtShift.Size = New System.Drawing.Size(209, 19)
        Me.txtShift.TabIndex = 66
        Me.txtShift.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(7, 28)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel4.TabIndex = 58
        Me.MyLabel4.Text = "Shift"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(526, 28)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel2.TabIndex = 65
        Me.MyLabel2.Text = "Shift End Date"
        '
        'txtShiftEnd
        '
        Me.txtShiftEnd.CalculationExpression = Nothing
        Me.txtShiftEnd.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtShiftEnd.FieldCode = Nothing
        Me.txtShiftEnd.FieldDesc = Nothing
        Me.txtShiftEnd.FieldMaxLength = 0
        Me.txtShiftEnd.FieldName = Nothing
        Me.txtShiftEnd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShiftEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtShiftEnd.isCalculatedField = False
        Me.txtShiftEnd.IsSourceFromTable = False
        Me.txtShiftEnd.IsSourceFromValueList = False
        Me.txtShiftEnd.IsUnique = False
        Me.txtShiftEnd.Location = New System.Drawing.Point(613, 27)
        Me.txtShiftEnd.MendatroryField = False
        Me.txtShiftEnd.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtShiftEnd.MyLinkLable1 = Me.MyLabel2
        Me.txtShiftEnd.MyLinkLable2 = Nothing
        Me.txtShiftEnd.Name = "txtShiftEnd"
        Me.txtShiftEnd.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtShiftEnd.ReadOnly = True
        Me.txtShiftEnd.ReferenceFieldDesc = Nothing
        Me.txtShiftEnd.ReferenceFieldName = Nothing
        Me.txtShiftEnd.ReferenceTableName = Nothing
        Me.txtShiftEnd.Size = New System.Drawing.Size(123, 18)
        Me.txtShiftEnd.TabIndex = 64
        Me.txtShiftEnd.TabStop = False
        Me.txtShiftEnd.Text = "13/06/2011 11:29 AM"
        Me.txtShiftEnd.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(308, 28)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(83, 16)
        Me.MyLabel1.TabIndex = 63
        Me.MyLabel1.Text = "Shift Start Date"
        '
        'txtShiftStart
        '
        Me.txtShiftStart.CalculationExpression = Nothing
        Me.txtShiftStart.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtShiftStart.FieldCode = Nothing
        Me.txtShiftStart.FieldDesc = Nothing
        Me.txtShiftStart.FieldMaxLength = 0
        Me.txtShiftStart.FieldName = Nothing
        Me.txtShiftStart.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShiftStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtShiftStart.isCalculatedField = False
        Me.txtShiftStart.IsSourceFromTable = False
        Me.txtShiftStart.IsSourceFromValueList = False
        Me.txtShiftStart.IsUnique = False
        Me.txtShiftStart.Location = New System.Drawing.Point(394, 27)
        Me.txtShiftStart.MendatroryField = False
        Me.txtShiftStart.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtShiftStart.MyLinkLable1 = Me.MyLabel1
        Me.txtShiftStart.MyLinkLable2 = Nothing
        Me.txtShiftStart.Name = "txtShiftStart"
        Me.txtShiftStart.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtShiftStart.ReadOnly = True
        Me.txtShiftStart.ReferenceFieldDesc = Nothing
        Me.txtShiftStart.ReferenceFieldName = Nothing
        Me.txtShiftStart.ReferenceTableName = Nothing
        Me.txtShiftStart.Size = New System.Drawing.Size(126, 18)
        Me.txtShiftStart.TabIndex = 62
        Me.txtShiftStart.TabStop = False
        Me.txtShiftStart.Text = "13/06/2011 11:29 AM"
        Me.txtShiftStart.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblLocationFG
        '
        Me.lblLocationFG.AutoSize = False
        Me.lblLocationFG.BorderVisible = True
        Me.lblLocationFG.FieldName = Nothing
        Me.lblLocationFG.Location = New System.Drawing.Point(297, 49)
        Me.lblLocationFG.Name = "lblLocationFG"
        Me.lblLocationFG.Size = New System.Drawing.Size(438, 19)
        Me.lblLocationFG.TabIndex = 56
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(7, 50)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel5.TabIndex = 55
        Me.MyLabel5.Text = "Location"
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(86, 49)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.MyLabel5
        Me.txtLocation.MyLinkLable2 = Me.lblLocationFG
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(209, 19)
        Me.txtLocation.TabIndex = 54
        Me.txtLocation.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(613, 4)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(123, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 10
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(308, 6)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(85, 16)
        Me.RadLabel4.TabIndex = 9
        Me.RadLabel4.Text = "Document Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(7, 6)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel1.TabIndex = 7
        Me.RadLabel1.Text = "Document No"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
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
        Me.txtDate.Location = New System.Drawing.Point(394, 5)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(83, 18)
        Me.txtDate.TabIndex = 0
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadPageView3
        '
        Me.RadPageView3.Controls.Add(Me.RadPageViewPage10)
        Me.RadPageView3.Controls.Add(Me.RadPageViewPage8)
        Me.RadPageView3.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView3.Location = New System.Drawing.Point(0, 74)
        Me.RadPageView3.Name = "RadPageView3"
        Me.RadPageView3.SelectedPage = Me.RadPageViewPage10
        Me.RadPageView3.Size = New System.Drawing.Size(1118, 405)
        Me.RadPageView3.TabIndex = 1
        CType(Me.RadPageView3.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView3.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        '
        'RadPageViewPage10
        '
        Me.RadPageViewPage10.Controls.Add(Me.gvProSFG)
        Me.RadPageViewPage10.ItemSize = New System.Drawing.SizeF(110.0!, 28.0!)
        Me.RadPageViewPage10.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage10.Name = "RadPageViewPage10"
        Me.RadPageViewPage10.Size = New System.Drawing.Size(1097, 357)
        Me.RadPageViewPage10.Text = "Produce SFG Items"
        '
        'gvProSFG
        '
        Me.gvProSFG.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvProSFG.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvProSFG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvProSFG.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvProSFG.ForeColor = System.Drawing.Color.Black
        Me.gvProSFG.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvProSFG.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvProSFG.MasterTemplate.AllowDeleteRow = False
        Me.gvProSFG.MasterTemplate.EnableAlternatingRowColor = True
        Me.gvProSFG.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvProSFG.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvProSFG.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvProSFG.MyExportAPI = False
        Me.gvProSFG.MyExportFilePath = ""
        Me.gvProSFG.MyStopExport = False
        Me.gvProSFG.Name = "gvProSFG"
        Me.gvProSFG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvProSFG.ShowGroupPanel = False
        Me.gvProSFG.ShowHeaderCellButtons = True
        Me.gvProSFG.Size = New System.Drawing.Size(1097, 357)
        Me.gvProSFG.TabIndex = 3
        Me.gvProSFG.VarID = ""
        '
        'RadPageViewPage8
        '
        Me.RadPageViewPage8.Controls.Add(Me.gvProRM)
        Me.RadPageViewPage8.ItemSize = New System.Drawing.SizeF(95.0!, 28.0!)
        Me.RadPageViewPage8.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage8.Name = "RadPageViewPage8"
        Me.RadPageViewPage8.Size = New System.Drawing.Size(1097, 357)
        Me.RadPageViewPage8.Text = "Issue Raw Items"
        '
        'gvProRM
        '
        Me.gvProRM.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvProRM.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvProRM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvProRM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvProRM.ForeColor = System.Drawing.Color.Black
        Me.gvProRM.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvProRM.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvProRM.MasterTemplate.AllowDeleteRow = False
        Me.gvProRM.MasterTemplate.EnableAlternatingRowColor = True
        Me.gvProRM.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvProRM.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvProRM.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gvProRM.MyExportAPI = False
        Me.gvProRM.MyExportFilePath = ""
        Me.gvProRM.MyStopExport = False
        Me.gvProRM.Name = "gvProRM"
        Me.gvProRM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvProRM.ShowGroupPanel = False
        Me.gvProRM.ShowHeaderCellButtons = True
        Me.gvProRM.Size = New System.Drawing.Size(1097, 357)
        Me.gvProRM.TabIndex = 4
        Me.gvProRM.VarID = ""
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel43)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel42)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel41)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel40)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel39)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel38)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotNetSNFKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotNetFATKG)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel37)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotAddedSNFKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotAddedFATKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotDifferenceSNFKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotDifferenceFATKG)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel33)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel29)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotIssueSNFKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotProduceSNFKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotIssueFATKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotProduceFATKG)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel25)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotRemoveSNFKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotRemoveFATKG)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel21)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1097, 357)
        Me.RadPageViewPage1.Text = "Total"
        '
        'MyLabel43
        '
        Me.MyLabel43.FieldName = Nothing
        Me.MyLabel43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel43.Location = New System.Drawing.Point(435, 149)
        Me.MyLabel43.Name = "MyLabel43"
        Me.MyLabel43.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel43.TabIndex = 80
        Me.MyLabel43.Text = "F [C+D-E]"
        '
        'MyLabel42
        '
        Me.MyLabel42.FieldName = Nothing
        Me.MyLabel42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel42.Location = New System.Drawing.Point(435, 129)
        Me.MyLabel42.Name = "MyLabel42"
        Me.MyLabel42.Size = New System.Drawing.Size(14, 16)
        Me.MyLabel42.TabIndex = 81
        Me.MyLabel42.Text = "E"
        '
        'MyLabel41
        '
        Me.MyLabel41.FieldName = Nothing
        Me.MyLabel41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel41.Location = New System.Drawing.Point(435, 105)
        Me.MyLabel41.Name = "MyLabel41"
        Me.MyLabel41.Size = New System.Drawing.Size(14, 16)
        Me.MyLabel41.TabIndex = 82
        Me.MyLabel41.Text = "D"
        '
        'MyLabel40
        '
        Me.MyLabel40.FieldName = Nothing
        Me.MyLabel40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel40.Location = New System.Drawing.Point(435, 85)
        Me.MyLabel40.Name = "MyLabel40"
        Me.MyLabel40.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel40.TabIndex = 83
        Me.MyLabel40.Text = "C [B-A]"
        '
        'MyLabel39
        '
        Me.MyLabel39.FieldName = Nothing
        Me.MyLabel39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel39.Location = New System.Drawing.Point(435, 61)
        Me.MyLabel39.Name = "MyLabel39"
        Me.MyLabel39.Size = New System.Drawing.Size(14, 16)
        Me.MyLabel39.TabIndex = 84
        Me.MyLabel39.Text = "B"
        '
        'MyLabel38
        '
        Me.MyLabel38.FieldName = Nothing
        Me.MyLabel38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel38.Location = New System.Drawing.Point(435, 39)
        Me.MyLabel38.Name = "MyLabel38"
        Me.MyLabel38.Size = New System.Drawing.Size(14, 16)
        Me.MyLabel38.TabIndex = 107
        Me.MyLabel38.Text = "A"
        '
        'lblTotNetSNFKG
        '
        Me.lblTotNetSNFKG.AutoSize = False
        Me.lblTotNetSNFKG.BorderVisible = True
        Me.lblTotNetSNFKG.FieldName = Nothing
        Me.lblTotNetSNFKG.Location = New System.Drawing.Point(281, 148)
        Me.lblTotNetSNFKG.Name = "lblTotNetSNFKG"
        Me.lblTotNetSNFKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotNetSNFKG.TabIndex = 105
        Me.lblTotNetSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotNetFATKG
        '
        Me.lblTotNetFATKG.AutoSize = False
        Me.lblTotNetFATKG.BorderVisible = True
        Me.lblTotNetFATKG.FieldName = Nothing
        Me.lblTotNetFATKG.Location = New System.Drawing.Point(125, 148)
        Me.lblTotNetFATKG.Name = "lblTotNetFATKG"
        Me.lblTotNetFATKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotNetFATKG.TabIndex = 106
        Me.lblTotNetFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel37
        '
        Me.MyLabel37.FieldName = Nothing
        Me.MyLabel37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel37.Location = New System.Drawing.Point(45, 149)
        Me.MyLabel37.Name = "MyLabel37"
        Me.MyLabel37.Size = New System.Drawing.Size(24, 16)
        Me.MyLabel37.TabIndex = 103
        Me.MyLabel37.Text = "Net"
        '
        'lblTotAddedSNFKG
        '
        Me.lblTotAddedSNFKG.AutoSize = False
        Me.lblTotAddedSNFKG.BorderVisible = True
        Me.lblTotAddedSNFKG.FieldName = Nothing
        Me.lblTotAddedSNFKG.Location = New System.Drawing.Point(281, 104)
        Me.lblTotAddedSNFKG.Name = "lblTotAddedSNFKG"
        Me.lblTotAddedSNFKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotAddedSNFKG.TabIndex = 101
        Me.lblTotAddedSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotAddedFATKG
        '
        Me.lblTotAddedFATKG.AutoSize = False
        Me.lblTotAddedFATKG.BorderVisible = True
        Me.lblTotAddedFATKG.FieldName = Nothing
        Me.lblTotAddedFATKG.Location = New System.Drawing.Point(125, 104)
        Me.lblTotAddedFATKG.Name = "lblTotAddedFATKG"
        Me.lblTotAddedFATKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotAddedFATKG.TabIndex = 102
        Me.lblTotAddedFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotDifferenceSNFKG
        '
        Me.lblTotDifferenceSNFKG.AutoSize = False
        Me.lblTotDifferenceSNFKG.BorderVisible = True
        Me.lblTotDifferenceSNFKG.FieldName = Nothing
        Me.lblTotDifferenceSNFKG.Location = New System.Drawing.Point(281, 82)
        Me.lblTotDifferenceSNFKG.Name = "lblTotDifferenceSNFKG"
        Me.lblTotDifferenceSNFKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotDifferenceSNFKG.TabIndex = 97
        Me.lblTotDifferenceSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotDifferenceFATKG
        '
        Me.lblTotDifferenceFATKG.AutoSize = False
        Me.lblTotDifferenceFATKG.BorderVisible = True
        Me.lblTotDifferenceFATKG.FieldName = Nothing
        Me.lblTotDifferenceFATKG.Location = New System.Drawing.Point(125, 82)
        Me.lblTotDifferenceFATKG.Name = "lblTotDifferenceFATKG"
        Me.lblTotDifferenceFATKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotDifferenceFATKG.TabIndex = 98
        Me.lblTotDifferenceFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel33
        '
        Me.MyLabel33.FieldName = Nothing
        Me.MyLabel33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel33.Location = New System.Drawing.Point(45, 107)
        Me.MyLabel33.Name = "MyLabel33"
        Me.MyLabel33.Size = New System.Drawing.Size(39, 16)
        Me.MyLabel33.TabIndex = 99
        Me.MyLabel33.Text = "Added"
        '
        'MyLabel29
        '
        Me.MyLabel29.FieldName = Nothing
        Me.MyLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel29.Location = New System.Drawing.Point(45, 83)
        Me.MyLabel29.Name = "MyLabel29"
        Me.MyLabel29.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel29.TabIndex = 95
        Me.MyLabel29.Text = "Difference"
        '
        'lblTotIssueSNFKG
        '
        Me.lblTotIssueSNFKG.AutoSize = False
        Me.lblTotIssueSNFKG.BorderVisible = True
        Me.lblTotIssueSNFKG.FieldName = Nothing
        Me.lblTotIssueSNFKG.Location = New System.Drawing.Point(281, 60)
        Me.lblTotIssueSNFKG.Name = "lblTotIssueSNFKG"
        Me.lblTotIssueSNFKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotIssueSNFKG.TabIndex = 93
        Me.lblTotIssueSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotProduceSNFKG
        '
        Me.lblTotProduceSNFKG.AutoSize = False
        Me.lblTotProduceSNFKG.BorderVisible = True
        Me.lblTotProduceSNFKG.FieldName = Nothing
        Me.lblTotProduceSNFKG.Location = New System.Drawing.Point(281, 38)
        Me.lblTotProduceSNFKG.Name = "lblTotProduceSNFKG"
        Me.lblTotProduceSNFKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotProduceSNFKG.TabIndex = 89
        Me.lblTotProduceSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotIssueFATKG
        '
        Me.lblTotIssueFATKG.AutoSize = False
        Me.lblTotIssueFATKG.BorderVisible = True
        Me.lblTotIssueFATKG.FieldName = Nothing
        Me.lblTotIssueFATKG.Location = New System.Drawing.Point(125, 60)
        Me.lblTotIssueFATKG.Name = "lblTotIssueFATKG"
        Me.lblTotIssueFATKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotIssueFATKG.TabIndex = 94
        Me.lblTotIssueFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotProduceFATKG
        '
        Me.lblTotProduceFATKG.AutoSize = False
        Me.lblTotProduceFATKG.BorderVisible = True
        Me.lblTotProduceFATKG.FieldName = Nothing
        Me.lblTotProduceFATKG.Location = New System.Drawing.Point(125, 38)
        Me.lblTotProduceFATKG.Name = "lblTotProduceFATKG"
        Me.lblTotProduceFATKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotProduceFATKG.TabIndex = 90
        Me.lblTotProduceFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel25
        '
        Me.MyLabel25.FieldName = Nothing
        Me.MyLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel25.Location = New System.Drawing.Point(45, 61)
        Me.MyLabel25.Name = "MyLabel25"
        Me.MyLabel25.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel25.TabIndex = 91
        Me.MyLabel25.Text = "Issue"
        '
        'lblTotRemoveSNFKG
        '
        Me.lblTotRemoveSNFKG.AutoSize = False
        Me.lblTotRemoveSNFKG.BorderVisible = True
        Me.lblTotRemoveSNFKG.FieldName = Nothing
        Me.lblTotRemoveSNFKG.Location = New System.Drawing.Point(281, 126)
        Me.lblTotRemoveSNFKG.Name = "lblTotRemoveSNFKG"
        Me.lblTotRemoveSNFKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotRemoveSNFKG.TabIndex = 86
        Me.lblTotRemoveSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotRemoveFATKG
        '
        Me.lblTotRemoveFATKG.AutoSize = False
        Me.lblTotRemoveFATKG.BorderVisible = True
        Me.lblTotRemoveFATKG.FieldName = Nothing
        Me.lblTotRemoveFATKG.Location = New System.Drawing.Point(125, 126)
        Me.lblTotRemoveFATKG.Name = "lblTotRemoveFATKG"
        Me.lblTotRemoveFATKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotRemoveFATKG.TabIndex = 85
        Me.lblTotRemoveFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(45, 39)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel21.TabIndex = 87
        Me.MyLabel21.Text = "Produce"
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(45, 127)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel15.TabIndex = 78
        Me.MyLabel15.Text = "Removed"
        '
        'MyLabel12
        '
        Me.MyLabel12.AutoSize = False
        Me.MyLabel12.BorderVisible = True
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Location = New System.Drawing.Point(281, 16)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(153, 19)
        Me.MyLabel12.TabIndex = 77
        Me.MyLabel12.Text = "SNF KG"
        Me.MyLabel12.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MyLabel7
        '
        Me.MyLabel7.AutoSize = False
        Me.MyLabel7.BorderVisible = True
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(125, 16)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(153, 19)
        Me.MyLabel7.TabIndex = 75
        Me.MyLabel7.Text = "FAT KG"
        Me.MyLabel7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmProductionShiftMgmtSFG
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1118, 516)
        Me.ControlBox = False
        Me.Controls.Add(Me.RadPageView3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "frmProductionShiftMgmtSFG"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Produce SFG Items"
        Me.Panel2.ResumeLayout(False)
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.txtDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtShiftEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtShiftStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationFG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView3.ResumeLayout(False)
        Me.RadPageViewPage10.ResumeLayout(False)
        CType(Me.gvProSFG.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvProSFG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage8.ResumeLayout(False)
        CType(Me.gvProRM.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvProRM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotNetSNFKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotNetFATKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotAddedSNFKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotAddedFATKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotDifferenceSNFKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotDifferenceFATKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotIssueSNFKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotProduceSNFKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotIssueFATKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotProduceFATKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRemoveSNFKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRemoveFATKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents lblLocationFG As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReverse As RadButton
    Friend WithEvents btnShowInventory As RadButton
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents RadPageView3 As RadPageView
    Friend WithEvents RadPageViewPage8 As RadPageViewPage
    Friend WithEvents gvProRM As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtShiftStart As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtShiftEnd As common.Controls.MyDateTimePicker
    Friend WithEvents RadPageViewPage10 As RadPageViewPage
    Friend WithEvents gvProSFG As common.UserControls.MyRadGridView
    Friend WithEvents txtShift As common.UserControls.txtFinder
    Friend WithEvents txtDocNo As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents MyLabel43 As common.Controls.MyLabel
    Friend WithEvents MyLabel42 As common.Controls.MyLabel
    Friend WithEvents MyLabel41 As common.Controls.MyLabel
    Friend WithEvents MyLabel40 As common.Controls.MyLabel
    Friend WithEvents MyLabel39 As common.Controls.MyLabel
    Friend WithEvents MyLabel38 As common.Controls.MyLabel
    Friend WithEvents lblTotNetSNFKG As common.Controls.MyLabel
    Friend WithEvents lblTotNetFATKG As common.Controls.MyLabel
    Friend WithEvents MyLabel37 As common.Controls.MyLabel
    Friend WithEvents lblTotAddedSNFKG As common.Controls.MyLabel
    Friend WithEvents lblTotAddedFATKG As common.Controls.MyLabel
    Friend WithEvents lblTotDifferenceSNFKG As common.Controls.MyLabel
    Friend WithEvents lblTotDifferenceFATKG As common.Controls.MyLabel
    Friend WithEvents MyLabel33 As common.Controls.MyLabel
    Friend WithEvents MyLabel29 As common.Controls.MyLabel
    Friend WithEvents lblTotIssueSNFKG As common.Controls.MyLabel
    Friend WithEvents lblTotProduceSNFKG As common.Controls.MyLabel
    Friend WithEvents lblTotIssueFATKG As common.Controls.MyLabel
    Friend WithEvents lblTotProduceFATKG As common.Controls.MyLabel
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents lblTotRemoveSNFKG As common.Controls.MyLabel
    Friend WithEvents lblTotRemoveFATKG As common.Controls.MyLabel
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
End Class

