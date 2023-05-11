<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDistanceMappingMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDistanceMappingMaster))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.txtActiveDate = New common.Controls.MyDateTimePicker
        Me.RadLabel3 = New Telerik.WinControls.UI.RadLabel
        Me.txtDate = New common.Controls.MyDateTimePicker
        Me.lblDate = New Telerik.WinControls.UI.RadLabel
        Me.txtCalculation = New common.MyNumBox
        Me.lblCalculation = New Telerik.WinControls.UI.RadLabel
        Me.txtFreight = New common.MyNumBox
        Me.lblFreight = New Telerik.WinControls.UI.RadLabel
        Me.txtVehicleCapacity = New common.MyNumBox
        Me.lblVehicleCapacity = New Telerik.WinControls.UI.RadLabel
        Me.lblFromLocation = New common.Controls.MyLabel
        Me.fndToCity = New common.UserControls.txtFinder
        Me.lblToCityName = New common.Controls.MyLabel
        Me.fndFromLocation = New common.UserControls.txtFinder
        Me.lblFromLocationName = New common.Controls.MyLabel
        Me.lblToCity = New common.Controls.MyLabel
        Me.fndCode = New common.UserControls.txtNavigator
        Me.lblCode = New common.Controls.MyLabel
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtActiveDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCalculation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCalculation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFreight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFreight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleCapacity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleCapacity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToCityName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtActiveDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCalculation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCalculation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFreight)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblFreight)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtVehicleCapacity)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblVehicleCapacity)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblFromLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndToCity)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblToCityName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndFromLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblFromLocationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblToCity)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1179, 418)
        Me.SplitContainer1.SplitterDistance = 378
        Me.SplitContainer1.TabIndex = 0
        '
        'txtActiveDate
        '
        Me.txtActiveDate.Checked = True
        Me.txtActiveDate.CustomFormat = "dd/MM/yyyy"
        Me.txtActiveDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtActiveDate.Location = New System.Drawing.Point(560, 20)
        Me.txtActiveDate.MendatroryField = True
        Me.txtActiveDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtActiveDate.MyLinkLable1 = Nothing
        Me.txtActiveDate.MyLinkLable2 = Nothing
        Me.txtActiveDate.Name = "txtActiveDate"
        Me.txtActiveDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtActiveDate.Size = New System.Drawing.Size(79, 18)
        Me.txtActiveDate.TabIndex = 329
        Me.txtActiveDate.TabStop = False
        Me.txtActiveDate.Text = "03/05/2011"
        Me.txtActiveDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(524, 20)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(30, 18)
        Me.RadLabel3.TabIndex = 328
        Me.RadLabel3.Text = "Date"
        '
        'txtDate
        '
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.Location = New System.Drawing.Point(430, 18)
        Me.txtDate.MendatroryField = True
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Nothing
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.Size = New System.Drawing.Size(79, 18)
        Me.txtDate.TabIndex = 327
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "03/05/2011"
        Me.txtDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblDate
        '
        Me.lblDate.Location = New System.Drawing.Point(394, 17)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 18)
        Me.lblDate.TabIndex = 326
        Me.lblDate.Text = "Date"
        '
        'txtCalculation
        '
        Me.txtCalculation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCalculation.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCalculation.DecimalPlaces = 2
        Me.txtCalculation.Location = New System.Drawing.Point(116, 140)
        Me.txtCalculation.MendatroryField = False
        Me.txtCalculation.MyLinkLable1 = Nothing
        Me.txtCalculation.MyLinkLable2 = Nothing
        Me.txtCalculation.Name = "txtCalculation"
        Me.txtCalculation.Size = New System.Drawing.Size(125, 20)
        Me.txtCalculation.TabIndex = 325
        Me.txtCalculation.Text = "0"
        Me.txtCalculation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCalculation.Value = 0
        '
        'lblCalculation
        '
        Me.lblCalculation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCalculation.Location = New System.Drawing.Point(22, 140)
        Me.lblCalculation.Name = "lblCalculation"
        Me.lblCalculation.Size = New System.Drawing.Size(61, 18)
        Me.lblCalculation.TabIndex = 324
        Me.lblCalculation.Text = "Calculation"
        '
        'txtFreight
        '
        Me.txtFreight.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFreight.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtFreight.DecimalPlaces = 2
        Me.txtFreight.Location = New System.Drawing.Point(116, 116)
        Me.txtFreight.MendatroryField = False
        Me.txtFreight.MyLinkLable1 = Nothing
        Me.txtFreight.MyLinkLable2 = Nothing
        Me.txtFreight.Name = "txtFreight"
        Me.txtFreight.Size = New System.Drawing.Size(125, 20)
        Me.txtFreight.TabIndex = 323
        Me.txtFreight.Text = "0"
        Me.txtFreight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFreight.Value = 0
        '
        'lblFreight
        '
        Me.lblFreight.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFreight.Location = New System.Drawing.Point(22, 116)
        Me.lblFreight.Name = "lblFreight"
        Me.lblFreight.Size = New System.Drawing.Size(41, 18)
        Me.lblFreight.TabIndex = 322
        Me.lblFreight.Text = "Freight"
        '
        'txtVehicleCapacity
        '
        Me.txtVehicleCapacity.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtVehicleCapacity.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtVehicleCapacity.DecimalPlaces = 2
        Me.txtVehicleCapacity.Location = New System.Drawing.Point(116, 90)
        Me.txtVehicleCapacity.MendatroryField = False
        Me.txtVehicleCapacity.MyLinkLable1 = Nothing
        Me.txtVehicleCapacity.MyLinkLable2 = Nothing
        Me.txtVehicleCapacity.Name = "txtVehicleCapacity"
        Me.txtVehicleCapacity.Size = New System.Drawing.Size(125, 20)
        Me.txtVehicleCapacity.TabIndex = 321
        Me.txtVehicleCapacity.Text = "0"
        Me.txtVehicleCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtVehicleCapacity.Value = 0
        '
        'lblVehicleCapacity
        '
        Me.lblVehicleCapacity.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVehicleCapacity.Location = New System.Drawing.Point(18, 91)
        Me.lblVehicleCapacity.Name = "lblVehicleCapacity"
        Me.lblVehicleCapacity.Size = New System.Drawing.Size(88, 18)
        Me.lblVehicleCapacity.TabIndex = 320
        Me.lblVehicleCapacity.Text = "Vehicle Capacity"
        '
        'lblFromLocation
        '
        Me.lblFromLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromLocation.Location = New System.Drawing.Point(18, 44)
        Me.lblFromLocation.Name = "lblFromLocation"
        Me.lblFromLocation.Size = New System.Drawing.Size(79, 16)
        Me.lblFromLocation.TabIndex = 87
        Me.lblFromLocation.Text = "From Location"
        '
        'fndToCity
        '
        Me.fndToCity.Location = New System.Drawing.Point(116, 66)
        Me.fndToCity.MendatroryField = True
        Me.fndToCity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndToCity.MyLinkLable1 = Nothing
        Me.fndToCity.MyLinkLable2 = Me.lblToCityName
        Me.fndToCity.MyReadOnly = False
        Me.fndToCity.MyShowMasterFormButton = False
        Me.fndToCity.Name = "fndToCity"
        Me.fndToCity.Size = New System.Drawing.Size(215, 18)
        Me.fndToCity.TabIndex = 85
        Me.fndToCity.Value = ""
        '
        'lblToCityName
        '
        Me.lblToCityName.AutoSize = False
        Me.lblToCityName.BorderVisible = True
        Me.lblToCityName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToCityName.Location = New System.Drawing.Point(337, 64)
        Me.lblToCityName.Name = "lblToCityName"
        Me.lblToCityName.Size = New System.Drawing.Size(302, 18)
        Me.lblToCityName.TabIndex = 86
        Me.lblToCityName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblToCityName.TextWrap = False
        '
        'fndFromLocation
        '
        Me.fndFromLocation.Location = New System.Drawing.Point(116, 42)
        Me.fndFromLocation.MendatroryField = True
        Me.fndFromLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndFromLocation.MyLinkLable1 = Nothing
        Me.fndFromLocation.MyLinkLable2 = Me.lblFromLocationName
        Me.fndFromLocation.MyReadOnly = False
        Me.fndFromLocation.MyShowMasterFormButton = False
        Me.fndFromLocation.Name = "fndFromLocation"
        Me.fndFromLocation.Size = New System.Drawing.Size(215, 18)
        Me.fndFromLocation.TabIndex = 83
        Me.fndFromLocation.Value = ""
        '
        'lblFromLocationName
        '
        Me.lblFromLocationName.AutoSize = False
        Me.lblFromLocationName.BorderVisible = True
        Me.lblFromLocationName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromLocationName.Location = New System.Drawing.Point(337, 44)
        Me.lblFromLocationName.Name = "lblFromLocationName"
        Me.lblFromLocationName.Size = New System.Drawing.Size(302, 18)
        Me.lblFromLocationName.TabIndex = 84
        Me.lblFromLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFromLocationName.TextWrap = False
        '
        'lblToCity
        '
        Me.lblToCity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToCity.Location = New System.Drawing.Point(18, 66)
        Me.lblToCity.Name = "lblToCity"
        Me.lblToCity.Size = New System.Drawing.Size(42, 16)
        Me.lblToCity.TabIndex = 82
        Me.lblToCity.Text = "To City"
        '
        'fndCode
        '
        Me.fndCode.Location = New System.Drawing.Point(116, 17)
        Me.fndCode.MendatroryField = True
        Me.fndCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCode.MyLinkLable1 = Me.lblCode
        Me.fndCode.MyLinkLable2 = Nothing
        Me.fndCode.MyMaxLength = 32767
        Me.fndCode.MyReadOnly = False
        Me.fndCode.Name = "fndCode"
        Me.fndCode.Size = New System.Drawing.Size(254, 18)
        Me.fndCode.TabIndex = 73
        Me.fndCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCode.Location = New System.Drawing.Point(22, 18)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(38, 16)
        Me.lblCode.TabIndex = 75
        Me.lblCode.Text = " Code"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(369, 16)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 21)
        Me.btnNew.TabIndex = 74
        Me.btnNew.Text = " "
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(90, 10)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(79, 19)
        Me.btnDelete.TabIndex = 35
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(11, 10)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(73, 19)
        Me.btnSave.TabIndex = 34
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(1090, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(78, 19)
        Me.btnClose.TabIndex = 33
        Me.btnClose.Text = "Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1179, 20)
        Me.RadMenu1.TabIndex = 4
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmImport, Me.rmExport})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmImport
        '
        Me.rmImport.AccessibleDescription = "Import"
        Me.rmImport.AccessibleName = "Import"
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        Me.rmImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmExport
        '
        Me.rmExport.AccessibleDescription = "Export"
        Me.rmExport.AccessibleName = "Export"
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export"
        Me.rmExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'FrmDistanceMappingMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1179, 438)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmDistanceMappingMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmDistanceMappingMaster"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtActiveDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCalculation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCalculation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFreight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFreight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleCapacity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleCapacity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToCityName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndCode As common.UserControls.txtNavigator
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblFromLocation As common.Controls.MyLabel
    Friend WithEvents fndToCity As common.UserControls.txtFinder
    Friend WithEvents lblToCityName As common.Controls.MyLabel
    Friend WithEvents fndFromLocation As common.UserControls.txtFinder
    Friend WithEvents lblFromLocationName As common.Controls.MyLabel
    Friend WithEvents lblToCity As common.Controls.MyLabel
    Friend WithEvents txtActiveDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel3 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDate As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtCalculation As common.MyNumBox
    Friend WithEvents lblCalculation As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtFreight As common.MyNumBox
    Friend WithEvents lblFreight As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtVehicleCapacity As common.MyNumBox
    Friend WithEvents lblVehicleCapacity As Telerik.WinControls.UI.RadLabel
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
End Class

