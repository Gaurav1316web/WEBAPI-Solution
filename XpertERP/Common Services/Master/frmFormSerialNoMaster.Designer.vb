<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFormSerialNoMaster
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.btnexport = New Telerik.WinControls.UI.RadMenuItem
        Me.btnimport = New Telerik.WinControls.UI.RadMenuItem
        Me.txtprefix = New common.Controls.MyTextBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.txttotal_no = New common.MyNumBox
        Me.MyLabel6 = New common.Controls.MyLabel
        Me.txtend_no = New common.MyNumBox
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.txtstart_no = New common.MyNumBox
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.txtformtype = New common.Controls.MyLabel
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.RadLabel4 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.txtDocNo = New common.UserControls.txtNavigator
        Me.txtDate = New common.Controls.MyDateTimePicker
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.txtformdesc = New common.Controls.MyLabel
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.txtformcode = New common.UserControls.txtFinder
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtprefix, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttotal_no, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtend_no, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtstart_no, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtformtype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtformdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(607, 292)
        Me.SplitContainer1.SplitterDistance = 257
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtprefix)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txttotal_no)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtend_no)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtstart_no)
        Me.SplitContainer2.Panel2.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Panel2.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel2.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel2.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtformtype)
        Me.SplitContainer2.Panel2.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadLabel4)
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadLabel1)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtDocNo)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnNew)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtformdesc)
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadLabel2)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtformcode)
        Me.SplitContainer2.Size = New System.Drawing.Size(601, 251)
        Me.SplitContainer2.SplitterDistance = 25
        Me.SplitContainer2.TabIndex = 0
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(601, 20)
        Me.RadMenu1.TabIndex = 320
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexport, Me.btnimport})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Files"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnexport
        '
        Me.btnexport.AccessibleDescription = "Export"
        Me.btnexport.AccessibleName = "Export"
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Text = "Export"
        Me.btnexport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnimport
        '
        Me.btnimport.AccessibleDescription = "Import"
        Me.btnimport.AccessibleName = "Import"
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Text = "Import"
        Me.btnimport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'txtprefix
        '
        Me.txtprefix.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprefix.Location = New System.Drawing.Point(107, 80)
        Me.txtprefix.MaxLength = 3
        Me.txtprefix.MendatroryField = True
        Me.txtprefix.MyLinkLable1 = Me.MyLabel3
        Me.txtprefix.MyLinkLable2 = Nothing
        Me.txtprefix.Name = "txtprefix"
        Me.txtprefix.Size = New System.Drawing.Size(143, 18)
        Me.txtprefix.TabIndex = 3
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(9, 81)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel3.TabIndex = 56
        Me.MyLabel3.Text = "Form Prefix"
        '
        'txttotal_no
        '
        Me.txttotal_no.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txttotal_no.DecimalPlaces = 2
        Me.txttotal_no.Location = New System.Drawing.Point(107, 127)
        Me.txttotal_no.MendatroryField = True
        Me.txttotal_no.MyLinkLable1 = Me.MyLabel6
        Me.txttotal_no.MyLinkLable2 = Nothing
        Me.txttotal_no.Name = "txttotal_no"
        Me.txttotal_no.ReadOnly = True
        Me.txttotal_no.Size = New System.Drawing.Size(143, 20)
        Me.txttotal_no.TabIndex = 6
        Me.txttotal_no.Text = "0"
        Me.txttotal_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txttotal_no.Value = 0
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(9, 129)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel6.TabIndex = 55
        Me.MyLabel6.Text = "Total Nos."
        '
        'txtend_no
        '
        Me.txtend_no.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtend_no.DecimalPlaces = 2
        Me.txtend_no.Location = New System.Drawing.Point(350, 102)
        Me.txtend_no.MendatroryField = True
        Me.txtend_no.MyLinkLable1 = Me.MyLabel5
        Me.txtend_no.MyLinkLable2 = Nothing
        Me.txtend_no.Name = "txtend_no"
        Me.txtend_no.Size = New System.Drawing.Size(143, 20)
        Me.txtend_no.TabIndex = 5
        Me.txtend_no.Text = "0"
        Me.txtend_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtend_no.Value = 0
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(270, 104)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel5.TabIndex = 55
        Me.MyLabel5.Text = "Series End At"
        '
        'txtstart_no
        '
        Me.txtstart_no.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtstart_no.DecimalPlaces = 2
        Me.txtstart_no.Location = New System.Drawing.Point(107, 102)
        Me.txtstart_no.MendatroryField = True
        Me.txtstart_no.MyLinkLable1 = Me.MyLabel4
        Me.txtstart_no.MyLinkLable2 = Nothing
        Me.txtstart_no.Name = "txtstart_no"
        Me.txtstart_no.Size = New System.Drawing.Size(143, 20)
        Me.txtstart_no.TabIndex = 4
        Me.txtstart_no.Text = "0"
        Me.txtstart_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtstart_no.Value = 0
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(9, 103)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(95, 16)
        Me.MyLabel4.TabIndex = 55
        Me.MyLabel4.Text = "Series Start From"
        '
        'txtformtype
        '
        Me.txtformtype.AutoSize = False
        Me.txtformtype.BorderVisible = True
        Me.txtformtype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtformtype.Location = New System.Drawing.Point(107, 59)
        Me.txtformtype.Name = "txtformtype"
        Me.txtformtype.Size = New System.Drawing.Size(143, 18)
        Me.txtformtype.TabIndex = 1
        Me.txtformtype.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtformtype.TextWrap = False
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 59)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel1.TabIndex = 54
        Me.MyLabel1.Text = "Form Type"
        '
        'RadLabel4
        '
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(384, 16)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 53
        Me.RadLabel4.Text = "Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(9, 15)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(79, 16)
        Me.RadLabel1.TabIndex = 52
        Me.RadLabel1.Text = "Document No."
        '
        'txtDocNo
        '
        Me.txtDocNo.Location = New System.Drawing.Point(107, 14)
        Me.txtDocNo.MendatroryField = True
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 18)
        Me.txtDocNo.TabIndex = 1
        Me.txtDocNo.Value = ""
        '
        'txtDate
        '
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.Location = New System.Drawing.Point(416, 15)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.Size = New System.Drawing.Size(77, 18)
        Me.txtDate.TabIndex = 0
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'btnNew
        '
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(359, 14)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(17, 19)
        Me.btnNew.TabIndex = 50
        '
        'txtformdesc
        '
        Me.txtformdesc.AutoSize = False
        Me.txtformdesc.BorderVisible = True
        Me.txtformdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtformdesc.Location = New System.Drawing.Point(251, 37)
        Me.txtformdesc.Name = "txtformdesc"
        Me.txtformdesc.Size = New System.Drawing.Size(242, 18)
        Me.txtformdesc.TabIndex = 43
        Me.txtformdesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtformdesc.TextWrap = False
        '
        'RadLabel2
        '
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(9, 38)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel2.TabIndex = 42
        Me.RadLabel2.Text = "Form Code"
        '
        'txtformcode
        '
        Me.txtformcode.Location = New System.Drawing.Point(107, 37)
        Me.txtformcode.MendatroryField = True
        Me.txtformcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtformcode.MyLinkLable1 = Me.RadLabel2
        Me.txtformcode.MyLinkLable2 = Me.txtformdesc
        Me.txtformcode.MyReadOnly = False
        Me.txtformcode.Name = "txtformcode"
        Me.txtformcode.Size = New System.Drawing.Size(143, 18)
        Me.txtformcode.TabIndex = 2
        Me.txtformcode.Value = ""
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(521, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(76, 20)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "&Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(89, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(76, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "&Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(7, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(76, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "&Save"
        '
        'FrmFormSerialNoMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(607, 292)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmFormSerialNoMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmFormSerialNoMaster"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtprefix, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttotal_no, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtend_no, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtstart_no, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtformtype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtformdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtformdesc As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents txtformcode As common.UserControls.txtFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtformtype As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txttotal_no As common.MyNumBox
    Friend WithEvents txtend_no As common.MyNumBox
    Friend WithEvents txtstart_no As common.MyNumBox
    Friend WithEvents txtprefix As common.Controls.MyTextBox
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnimport As Telerik.WinControls.UI.RadMenuItem
End Class

