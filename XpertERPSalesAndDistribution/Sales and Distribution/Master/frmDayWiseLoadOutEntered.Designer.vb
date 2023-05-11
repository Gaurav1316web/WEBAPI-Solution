<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDayWiseLoadOutEntered
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDayWiseLoadOutEntered))
        Me.grpbxAdditional = New Telerik.WinControls.UI.RadGroupBox
        Me.txttobePosted = New common.MyNumBox
        Me.lblBalancename = New common.Controls.MyLabel
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.txtTotalCount = New common.MyNumBox
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnrefresh = New Telerik.WinControls.UI.RadButton
        Me.DayPanel = New Telerik.WinControls.UI.RadPanel
        Me.lblloc = New common.Controls.MyLabel
        Me.FndLocation = New common.UserControls.txtFinder
        Me.lblLocation = New common.Controls.MyLabel
        Me.lblDate = New common.Controls.MyLabel
        Me.dtpLoadoutNo = New Telerik.WinControls.UI.RadDateTimePicker
        Me.lblLocationname = New common.Controls.MyLabel
        Me.DrpType = New Telerik.WinControls.UI.RadDropDownList
        Me.lblType = New common.Controls.MyLabel
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.txtBalance = New common.MyNumBox
        Me.txtLoadOutMade = New common.MyNumBox
        Me.txtRemarks = New System.Windows.Forms.TextBox
        Me.lblRemarks = New common.Controls.MyLabel
        Me.lblNoOfCashMemo = New common.Controls.MyLabel
        Me.txtNoOfLoadOutMake = New common.MyNumBox
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.grpbxAdditional, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpbxAdditional.SuspendLayout()
        CType(Me.txttobePosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBalancename, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnrefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DayPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DayPanel.SuspendLayout()
        CType(Me.lblloc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpLoadoutNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DrpType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLoadOutMade, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNoOfCashMemo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoOfLoadOutMake, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpbxAdditional
        '
        Me.grpbxAdditional.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpbxAdditional.Controls.Add(Me.txttobePosted)
        Me.grpbxAdditional.Controls.Add(Me.RadLabel3)
        Me.grpbxAdditional.Controls.Add(Me.RadLabel2)
        Me.grpbxAdditional.Controls.Add(Me.txtTotalCount)
        Me.grpbxAdditional.Controls.Add(Me.btnrefresh)
        Me.grpbxAdditional.Controls.Add(Me.DayPanel)
        Me.grpbxAdditional.Controls.Add(Me.btnreset)
        Me.grpbxAdditional.Controls.Add(Me.txtBalance)
        Me.grpbxAdditional.Controls.Add(Me.txtLoadOutMade)
        Me.grpbxAdditional.Controls.Add(Me.txtRemarks)
        Me.grpbxAdditional.Controls.Add(Me.lblRemarks)
        Me.grpbxAdditional.Controls.Add(Me.lblBalancename)
        Me.grpbxAdditional.Controls.Add(Me.RadLabel1)
        Me.grpbxAdditional.Controls.Add(Me.lblNoOfCashMemo)
        Me.grpbxAdditional.Controls.Add(Me.txtNoOfLoadOutMake)
        Me.grpbxAdditional.FooterImageIndex = -1
        Me.grpbxAdditional.FooterImageKey = ""
        Me.grpbxAdditional.HeaderImageIndex = -1
        Me.grpbxAdditional.HeaderImageKey = ""
        Me.grpbxAdditional.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.grpbxAdditional.HeaderText = ""
        Me.grpbxAdditional.Location = New System.Drawing.Point(13, 12)
        Me.grpbxAdditional.Name = "grpbxAdditional"
        Me.grpbxAdditional.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.grpbxAdditional.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpbxAdditional.Size = New System.Drawing.Size(470, 408)
        Me.grpbxAdditional.TabIndex = 0
        '
        'txttobePosted
        '
        Me.txttobePosted.BackColor = System.Drawing.Color.White
        Me.txttobePosted.DecimalPlaces = 0
        Me.txttobePosted.Location = New System.Drawing.Point(267, 141)
        Me.txttobePosted.MendatroryField = False
        Me.txttobePosted.MyLinkLable1 = Me.lblBalancename
        Me.txttobePosted.MyLinkLable2 = Nothing
        Me.txttobePosted.Name = "txttobePosted"
        Me.txttobePosted.Size = New System.Drawing.Size(36, 20)
        Me.txttobePosted.TabIndex = 6
        Me.txttobePosted.Text = "0"
        Me.txttobePosted.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txttobePosted.Value = 0
        '
        'lblBalancename
        '
        Me.lblBalancename.Location = New System.Drawing.Point(158, 141)
        Me.lblBalancename.Name = "lblBalancename"
        Me.lblBalancename.Size = New System.Drawing.Size(72, 18)
        Me.lblBalancename.TabIndex = 7
        Me.lblBalancename.Text = "To Be Posted"
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(7, 139)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(76, 18)
        Me.RadLabel3.TabIndex = 20
        Me.RadLabel3.Text = "To Be Entered"
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(158, 115)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(103, 18)
        Me.RadLabel2.TabIndex = 19
        Me.RadLabel2.Text = "ERP Count (Posted)"
        '
        'txtTotalCount
        '
        Me.txtTotalCount.BackColor = System.Drawing.Color.White
        Me.txtTotalCount.DecimalPlaces = 0
        Me.txtTotalCount.Location = New System.Drawing.Point(106, 110)
        Me.txtTotalCount.MendatroryField = False
        Me.txtTotalCount.MyLinkLable1 = Me.RadLabel1
        Me.txtTotalCount.MyLinkLable2 = Nothing
        Me.txtTotalCount.Name = "txtTotalCount"
        Me.txtTotalCount.Size = New System.Drawing.Size(46, 20)
        Me.txtTotalCount.TabIndex = 3
        Me.txtTotalCount.Text = "0"
        Me.txtTotalCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalCount.Value = 0
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(7, 110)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(93, 18)
        Me.RadLabel1.TabIndex = 5
        Me.RadLabel1.Text = "ERP Count (Total)"
        '
        'btnrefresh
        '
        Me.btnrefresh.Location = New System.Drawing.Point(414, 86)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(43, 19)
        Me.btnrefresh.TabIndex = 10
        Me.btnrefresh.Text = ">>"
        '
        'DayPanel
        '
        Me.DayPanel.Controls.Add(Me.lblloc)
        Me.DayPanel.Controls.Add(Me.FndLocation)
        Me.DayPanel.Controls.Add(Me.lblDate)
        Me.DayPanel.Controls.Add(Me.lblLocation)
        Me.DayPanel.Controls.Add(Me.dtpLoadoutNo)
        Me.DayPanel.Controls.Add(Me.lblLocationname)
        Me.DayPanel.Controls.Add(Me.DrpType)
        Me.DayPanel.Controls.Add(Me.lblType)
        Me.DayPanel.Location = New System.Drawing.Point(7, 12)
        Me.DayPanel.Name = "DayPanel"
        Me.DayPanel.Size = New System.Drawing.Size(450, 67)
        Me.DayPanel.TabIndex = 0
        '
        'lblloc
        '
        Me.lblloc.Location = New System.Drawing.Point(200, 35)
        Me.lblloc.Name = "lblloc"
        Me.lblloc.Size = New System.Drawing.Size(36, 18)
        Me.lblloc.TabIndex = 9
        Me.lblloc.Text = "Name"
        '
        'FndLocation
        '
        Me.FndLocation.Location = New System.Drawing.Point(260, 11)
        Me.FndLocation.MendatroryField = False
        Me.FndLocation.MyLinkLable1 = Me.lblLocation
        Me.FndLocation.MyLinkLable2 = Nothing
        Me.FndLocation.MyReadOnly = False
        Me.FndLocation.Name = "FndLocation"
        Me.FndLocation.Size = New System.Drawing.Size(120, 19)
        Me.FndLocation.TabIndex = 1
        Me.FndLocation.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.Location = New System.Drawing.Point(200, 11)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 8
        Me.lblLocation.Text = "Location"
        '
        'lblDate
        '
        Me.lblDate.Location = New System.Drawing.Point(6, 11)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(85, 18)
        Me.lblDate.TabIndex = 0
        Me.lblDate.Text = "Data Entry Date"
        '
        'dtpLoadoutNo
        '
        Me.dtpLoadoutNo.CustomFormat = "dd/MM/yyyy"
        Me.dtpLoadoutNo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLoadoutNo.Location = New System.Drawing.Point(99, 9)
        Me.dtpLoadoutNo.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpLoadoutNo.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLoadoutNo.Name = "dtpLoadoutNo"
        Me.dtpLoadoutNo.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLoadoutNo.Size = New System.Drawing.Size(95, 20)
        Me.dtpLoadoutNo.TabIndex = 0
        Me.dtpLoadoutNo.Text = "RadDateTimePicker1"
        Me.dtpLoadoutNo.Value = New Date(2012, 6, 6, 0, 0, 0, 0)
        '
        'lblLocationname
        '
        Me.lblLocationname.Location = New System.Drawing.Point(260, 36)
        Me.lblLocationname.Name = "lblLocationname"
        Me.lblLocationname.Size = New System.Drawing.Size(82, 18)
        Me.lblLocationname.TabIndex = 3
        Me.lblLocationname.Text = "Location Name"
        '
        'DrpType
        '
        Me.DrpType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.DrpType.Location = New System.Drawing.Point(99, 33)
        Me.DrpType.Name = "DrpType"
        Me.DrpType.ShowImageInEditorArea = True
        Me.DrpType.Size = New System.Drawing.Size(95, 20)
        Me.DrpType.TabIndex = 2
        '
        'lblType
        '
        Me.lblType.Location = New System.Drawing.Point(6, 35)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(91, 18)
        Me.lblType.TabIndex = 9
        Me.lblType.Text = "Transaction Type"
        '
        'btnreset
        '
        Me.btnreset.Image = CType(resources.GetObject("btnreset.Image"), System.Drawing.Image)
        Me.btnreset.Location = New System.Drawing.Point(158, 86)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(17, 21)
        Me.btnreset.TabIndex = 2
        '
        'txtBalance
        '
        Me.txtBalance.BackColor = System.Drawing.Color.White
        Me.txtBalance.DecimalPlaces = 0
        Me.txtBalance.Location = New System.Drawing.Point(106, 139)
        Me.txtBalance.MendatroryField = False
        Me.txtBalance.MyLinkLable1 = Me.RadLabel3
        Me.txtBalance.MyLinkLable2 = Nothing
        Me.txtBalance.Name = "txtBalance"
        Me.txtBalance.Size = New System.Drawing.Size(46, 20)
        Me.txtBalance.TabIndex = 5
        Me.txtBalance.Text = "0"
        Me.txtBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBalance.Value = 0
        '
        'txtLoadOutMade
        '
        Me.txtLoadOutMade.BackColor = System.Drawing.Color.White
        Me.txtLoadOutMade.DecimalPlaces = 0
        Me.txtLoadOutMade.Location = New System.Drawing.Point(267, 115)
        Me.txtLoadOutMade.MendatroryField = False
        Me.txtLoadOutMade.MyLinkLable1 = Me.RadLabel2
        Me.txtLoadOutMade.MyLinkLable2 = Nothing
        Me.txtLoadOutMade.Name = "txtLoadOutMade"
        Me.txtLoadOutMade.Size = New System.Drawing.Size(36, 20)
        Me.txtLoadOutMade.TabIndex = 4
        Me.txtLoadOutMade.Text = "0"
        Me.txtLoadOutMade.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLoadOutMade.Value = 0
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(7, 187)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(449, 204)
        Me.txtRemarks.TabIndex = 7
        '
        'lblRemarks
        '
        Me.lblRemarks.Location = New System.Drawing.Point(7, 163)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(49, 18)
        Me.lblRemarks.TabIndex = 9
        Me.lblRemarks.Text = "Remarks"
        '
        'lblNoOfCashMemo
        '
        Me.lblNoOfCashMemo.Location = New System.Drawing.Point(7, 86)
        Me.lblNoOfCashMemo.Name = "lblNoOfCashMemo"
        Me.lblNoOfCashMemo.Size = New System.Drawing.Size(69, 18)
        Me.lblNoOfCashMemo.TabIndex = 3
        Me.lblNoOfCashMemo.Text = "S && D Count"
        '
        'txtNoOfLoadOutMake
        '
        Me.txtNoOfLoadOutMake.BackColor = System.Drawing.Color.White
        Me.txtNoOfLoadOutMake.DecimalPlaces = 0
        Me.txtNoOfLoadOutMake.Location = New System.Drawing.Point(106, 86)
        Me.txtNoOfLoadOutMake.MendatroryField = False
        Me.txtNoOfLoadOutMake.MyLinkLable1 = Me.lblNoOfCashMemo
        Me.txtNoOfLoadOutMake.MyLinkLable2 = Nothing
        Me.txtNoOfLoadOutMake.Name = "txtNoOfLoadOutMake"
        Me.txtNoOfLoadOutMake.Size = New System.Drawing.Size(46, 20)
        Me.txtNoOfLoadOutMake.TabIndex = 1
        Me.txtNoOfLoadOutMake.Text = "0"
        Me.txtNoOfLoadOutMake.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNoOfLoadOutMake.Value = 0
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(99, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(66, 19)
        Me.btnPrint.TabIndex = 9
        Me.btnPrint.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(416, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 19)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(13, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 19)
        Me.btnSave.TabIndex = 8
        Me.btnSave.Text = "Save"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.grpbxAdditional)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(497, 466)
        Me.SplitContainer1.SplitterDistance = 431
        Me.SplitContainer1.TabIndex = 1
        '
        'FrmDayWiseLoadOutEntered
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(497, 466)
        Me.Controls.Add(Me.SplitContainer1)
        Me.KeyPreview = True
        Me.Name = "FrmDayWiseLoadOutEntered"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Data Entry Status"
        CType(Me.grpbxAdditional, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpbxAdditional.ResumeLayout(False)
        Me.grpbxAdditional.PerformLayout()
        CType(Me.txttobePosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBalancename, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnrefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DayPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DayPanel.ResumeLayout(False)
        Me.DayPanel.PerformLayout()
        CType(Me.lblloc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpLoadoutNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DrpType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBalance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLoadOutMade, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNoOfCashMemo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoOfLoadOutMake, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpbxAdditional As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblBalancename As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblNoOfCashMemo As common.Controls.MyLabel
    Friend WithEvents txtNoOfLoadOutMake As common.MyNumBox
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpLoadoutNo As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents lblType As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents FndLocation As common.UserControls.txtFinder
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents DrpType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents lblLocationname As common.Controls.MyLabel
    Friend WithEvents txtBalance As common.MyNumBox
    Friend WithEvents txtLoadOutMade As common.MyNumBox
    Friend WithEvents lblloc As common.Controls.MyLabel
    Friend WithEvents DayPanel As Telerik.WinControls.UI.RadPanel
    Friend WithEvents btnrefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents txttobePosted As common.MyNumBox
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents txtTotalCount As common.MyNumBox
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

