<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransportMaster
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
        Me.components = New System.ComponentModel.Container
        Me.btnclear = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.txtemail = New common.Controls.MyTextBox
        Me.lblemail = New common.Controls.MyLabel
        Me.txtphone = New common.Controls.MyTextBox
        Me.lblphone = New common.Controls.MyLabel
        Me.txtadd2 = New common.Controls.MyTextBox
        Me.lbladd2 = New common.Controls.MyLabel
        Me.txtadd1 = New common.Controls.MyTextBox
        Me.lbladd1 = New common.Controls.MyLabel
        Me.txtname = New common.Controls.MyTextBox
        Me.lblname = New common.Controls.MyLabel
        Me.lblid = New common.Controls.MyLabel
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.transimport = New Telerik.WinControls.UI.RadMenuItem
        Me.transexport = New Telerik.WinControls.UI.RadMenuItem
        Me.transclose = New Telerik.WinControls.UI.RadMenuItem
        Me.menuprint = New Telerik.WinControls.UI.RadMenuItem
        Me.ToolTiptrans = New System.Windows.Forms.ToolTip(Me.components)
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.gbtransport = New Telerik.WinControls.UI.RadGroupBox
        Me.fndtrans = New common.UserControls.txtNavigator
        Me.txtpan = New common.Controls.MyTextBox
        Me.lblpan = New common.Controls.MyLabel
        Me.txtpin = New common.Controls.MyTextBox
        Me.lblpinno = New common.Controls.MyLabel
        Me.txtstate = New common.Controls.MyTextBox
        Me.lblstate = New common.Controls.MyLabel
        Me.txtcity = New common.Controls.MyTextBox
        Me.lblcity = New common.Controls.MyLabel
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.btnclear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtemail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblemail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtphone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblphone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtadd2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbladd2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtadd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbladd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbtransport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbtransport.SuspendLayout()
        CType(Me.txtpan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtpin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpinno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtstate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblstate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnclear
        '
        Me.btnclear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.Location = New System.Drawing.Point(577, 13)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(66, 18)
        Me.btnclear.TabIndex = 2
        Me.btnclear.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(88, 13)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(12, 13)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'txtemail
        '
        Me.txtemail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtemail.Location = New System.Drawing.Point(383, 86)
        Me.txtemail.MaxLength = 49
        Me.txtemail.MendatroryField = False
        Me.txtemail.MyLinkLable1 = Me.lblemail
        Me.txtemail.MyLinkLable2 = Nothing
        Me.txtemail.Name = "txtemail"
        Me.txtemail.Size = New System.Drawing.Size(230, 18)
        Me.txtemail.TabIndex = 8
        '
        'lblemail
        '
        Me.lblemail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblemail.Location = New System.Drawing.Point(326, 86)
        Me.lblemail.Name = "lblemail"
        Me.lblemail.Size = New System.Drawing.Size(34, 16)
        Me.lblemail.TabIndex = 13
        Me.lblemail.Text = "Email"
        '
        'txtphone
        '
        Me.txtphone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtphone.Location = New System.Drawing.Point(112, 86)
        Me.txtphone.MaxLength = 18
        Me.txtphone.MendatroryField = False
        Me.txtphone.MyLinkLable1 = Me.lblphone
        Me.txtphone.MyLinkLable2 = Nothing
        Me.txtphone.Name = "txtphone"
        Me.txtphone.Size = New System.Drawing.Size(186, 18)
        Me.txtphone.TabIndex = 7
        '
        'lblphone
        '
        Me.lblphone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblphone.Location = New System.Drawing.Point(13, 86)
        Me.lblphone.Name = "lblphone"
        Me.lblphone.Size = New System.Drawing.Size(82, 16)
        Me.lblphone.TabIndex = 11
        Me.lblphone.Text = "Phone Number"
        '
        'txtadd2
        '
        Me.txtadd2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtadd2.Location = New System.Drawing.Point(112, 132)
        Me.txtadd2.MaxLength = 49
        Me.txtadd2.MendatroryField = False
        Me.txtadd2.MyLinkLable1 = Me.lbladd2
        Me.txtadd2.MyLinkLable2 = Nothing
        Me.txtadd2.Name = "txtadd2"
        '
        '
        '
        Me.txtadd2.RootElement.StretchVertically = True
        Me.txtadd2.Size = New System.Drawing.Size(344, 20)
        Me.txtadd2.TabIndex = 10
        '
        'lbladd2
        '
        Me.lbladd2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladd2.Location = New System.Drawing.Point(13, 132)
        Me.lbladd2.Name = "lbladd2"
        Me.lbladd2.Size = New System.Drawing.Size(54, 16)
        Me.lbladd2.TabIndex = 9
        Me.lbladd2.Text = "Address2"
        '
        'txtadd1
        '
        Me.txtadd1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtadd1.Location = New System.Drawing.Point(112, 109)
        Me.txtadd1.MaxLength = 49
        Me.txtadd1.MendatroryField = False
        Me.txtadd1.MyLinkLable1 = Me.lbladd1
        Me.txtadd1.MyLinkLable2 = Nothing
        Me.txtadd1.Name = "txtadd1"
        '
        '
        '
        Me.txtadd1.RootElement.StretchVertically = True
        Me.txtadd1.Size = New System.Drawing.Size(344, 20)
        Me.txtadd1.TabIndex = 9
        '
        'lbladd1
        '
        Me.lbladd1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladd1.Location = New System.Drawing.Point(13, 108)
        Me.lbladd1.Name = "lbladd1"
        Me.lbladd1.Size = New System.Drawing.Size(54, 16)
        Me.lbladd1.TabIndex = 10
        Me.lbladd1.Text = "Address1"
        '
        'txtname
        '
        Me.txtname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtname.Location = New System.Drawing.Point(383, 18)
        Me.txtname.MaxLength = 49
        Me.txtname.MendatroryField = False
        Me.txtname.MyLinkLable1 = Me.lblname
        Me.txtname.MyLinkLable2 = Nothing
        Me.txtname.Name = "txtname"
        Me.txtname.ReadOnly = True
        Me.txtname.Size = New System.Drawing.Size(230, 18)
        Me.txtname.TabIndex = 2
        Me.txtname.TabStop = False
        '
        'lblname
        '
        Me.lblname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblname.Location = New System.Drawing.Point(327, 18)
        Me.lblname.Name = "lblname"
        Me.lblname.Size = New System.Drawing.Size(36, 16)
        Me.lblname.TabIndex = 17
        Me.lblname.Text = "Name"
        '
        'lblid
        '
        Me.lblid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblid.Location = New System.Drawing.Point(9, 18)
        Me.lblid.Name = "lblid"
        Me.lblid.Size = New System.Drawing.Size(104, 16)
        Me.lblid.TabIndex = 15
        Me.lblid.Text = "Transporter  Code"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(306, 16)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 1
        Me.btnnew.Text = "&r"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.transimport, Me.transexport, Me.transclose, Me.menuprint})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'transimport
        '
        Me.transimport.AccessibleDescription = "Import"
        Me.transimport.AccessibleName = "Import"
        Me.transimport.Name = "transimport"
        Me.transimport.Text = "Import"
        Me.transimport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'transexport
        '
        Me.transexport.AccessibleDescription = "Export"
        Me.transexport.AccessibleName = "Export"
        Me.transexport.Name = "transexport"
        Me.transexport.Text = "Export"
        Me.transexport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'transclose
        '
        Me.transclose.AccessibleDescription = "Close"
        Me.transclose.AccessibleName = "Close"
        Me.transclose.Name = "transclose"
        Me.transclose.Text = "Close"
        Me.transclose.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'menuprint
        '
        Me.menuprint.AccessibleDescription = "Print"
        Me.menuprint.AccessibleName = "Print"
        Me.menuprint.Name = "menuprint"
        Me.menuprint.Text = "Print"
        Me.menuprint.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(653, 20)
        Me.RadMenu1.TabIndex = 18
        Me.RadMenu1.Text = "RadMenu1"
        '
        'gbtransport
        '
        Me.gbtransport.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbtransport.Controls.Add(Me.fndtrans)
        Me.gbtransport.Controls.Add(Me.txtpan)
        Me.gbtransport.Controls.Add(Me.txtpin)
        Me.gbtransport.Controls.Add(Me.txtstate)
        Me.gbtransport.Controls.Add(Me.txtcity)
        Me.gbtransport.Controls.Add(Me.lblpan)
        Me.gbtransport.Controls.Add(Me.lblpinno)
        Me.gbtransport.Controls.Add(Me.lblstate)
        Me.gbtransport.Controls.Add(Me.lblcity)
        Me.gbtransport.Controls.Add(Me.lblid)
        Me.gbtransport.Controls.Add(Me.lblname)
        Me.gbtransport.Controls.Add(Me.lblemail)
        Me.gbtransport.Controls.Add(Me.btnnew)
        Me.gbtransport.Controls.Add(Me.lblphone)
        Me.gbtransport.Controls.Add(Me.lbladd2)
        Me.gbtransport.Controls.Add(Me.lbladd1)
        Me.gbtransport.Controls.Add(Me.txtname)
        Me.gbtransport.Controls.Add(Me.txtemail)
        Me.gbtransport.Controls.Add(Me.txtadd1)
        Me.gbtransport.Controls.Add(Me.txtphone)
        Me.gbtransport.Controls.Add(Me.txtadd2)
        Me.gbtransport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbtransport.HeaderText = ""
        Me.gbtransport.Location = New System.Drawing.Point(17, 13)
        Me.gbtransport.Name = "gbtransport"
        Me.gbtransport.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbtransport.Size = New System.Drawing.Size(626, 170)
        Me.gbtransport.TabIndex = 0
        '
        'fndtrans
        '
        Me.fndtrans.Location = New System.Drawing.Point(112, 15)
        Me.fndtrans.MendatroryField = False
        Me.fndtrans.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndtrans.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndtrans.MyLinkLable1 = Me.lblid
        Me.fndtrans.MyLinkLable2 = Nothing
        Me.fndtrans.MyMaxLength = 32767
        Me.fndtrans.MyReadOnly = False
        Me.fndtrans.Name = "fndtrans"
        Me.fndtrans.Size = New System.Drawing.Size(191, 21)
        Me.fndtrans.TabIndex = 0
        Me.fndtrans.TabStop = False
        Me.fndtrans.Value = ""
        '
        'txtpan
        '
        Me.txtpan.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpan.Location = New System.Drawing.Point(383, 63)
        Me.txtpan.MaxLength = 20
        Me.txtpan.MendatroryField = False
        Me.txtpan.MyLinkLable1 = Me.lblpan
        Me.txtpan.MyLinkLable2 = Nothing
        Me.txtpan.Name = "txtpan"
        Me.txtpan.Size = New System.Drawing.Size(230, 18)
        Me.txtpan.TabIndex = 6
        '
        'lblpan
        '
        Me.lblpan.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpan.Location = New System.Drawing.Point(327, 62)
        Me.lblpan.Name = "lblpan"
        Me.lblpan.Size = New System.Drawing.Size(50, 16)
        Me.lblpan.TabIndex = 21
        Me.lblpan.Text = "PAN No."
        '
        'txtpin
        '
        Me.txtpin.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpin.Location = New System.Drawing.Point(112, 63)
        Me.txtpin.MaxLength = 20
        Me.txtpin.MendatroryField = False
        Me.txtpin.MyLinkLable1 = Me.lblpinno
        Me.txtpin.MyLinkLable2 = Nothing
        Me.txtpin.Name = "txtpin"
        Me.txtpin.Size = New System.Drawing.Size(186, 18)
        Me.txtpin.TabIndex = 5
        '
        'lblpinno
        '
        Me.lblpinno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpinno.Location = New System.Drawing.Point(13, 66)
        Me.lblpinno.Name = "lblpinno"
        Me.lblpinno.Size = New System.Drawing.Size(40, 16)
        Me.lblpinno.TabIndex = 20
        Me.lblpinno.Text = "Pin No"
        '
        'txtstate
        '
        Me.txtstate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtstate.Location = New System.Drawing.Point(383, 40)
        Me.txtstate.MaxLength = 20
        Me.txtstate.MendatroryField = False
        Me.txtstate.MyLinkLable1 = Me.lblstate
        Me.txtstate.MyLinkLable2 = Nothing
        Me.txtstate.Name = "txtstate"
        Me.txtstate.Size = New System.Drawing.Size(230, 18)
        Me.txtstate.TabIndex = 4
        '
        'lblstate
        '
        Me.lblstate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstate.Location = New System.Drawing.Point(327, 40)
        Me.lblstate.Name = "lblstate"
        Me.lblstate.Size = New System.Drawing.Size(33, 16)
        Me.lblstate.TabIndex = 19
        Me.lblstate.Text = "State"
        '
        'txtcity
        '
        Me.txtcity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcity.Location = New System.Drawing.Point(112, 40)
        Me.txtcity.MaxLength = 20
        Me.txtcity.MendatroryField = False
        Me.txtcity.MyLinkLable1 = Me.lblcity
        Me.txtcity.MyLinkLable2 = Nothing
        Me.txtcity.Name = "txtcity"
        Me.txtcity.Size = New System.Drawing.Size(186, 18)
        Me.txtcity.TabIndex = 3
        '
        'lblcity
        '
        Me.lblcity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcity.Location = New System.Drawing.Point(13, 40)
        Me.lblcity.Name = "lblcity"
        Me.lblcity.Size = New System.Drawing.Size(26, 16)
        Me.lblcity.TabIndex = 18
        Me.lblcity.Text = "City"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbtransport)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclear)
        Me.SplitContainer1.Size = New System.Drawing.Size(653, 230)
        Me.SplitContainer1.SplitterDistance = 187
        Me.SplitContainer1.TabIndex = 0
        '
        'frmTransportMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(653, 250)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmTransportMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Transport Master"
        CType(Me.btnclear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtemail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblemail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtphone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblphone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtadd2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbladd2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtadd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbladd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbtransport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbtransport.ResumeLayout(False)
        Me.gbtransport.PerformLayout()
        CType(Me.txtpan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtpin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpinno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtstate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblstate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclear As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtemail As common.Controls.MyTextBox
    Friend WithEvents txtphone As common.Controls.MyTextBox
    Friend WithEvents txtadd2 As common.Controls.MyTextBox
    Friend WithEvents txtadd1 As common.Controls.MyTextBox
    Friend WithEvents txtname As common.Controls.MyTextBox
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ToolTiptrans As System.Windows.Forms.ToolTip
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents transimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents transexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents transclose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents gbtransport As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents menuprint As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtcity As common.Controls.MyTextBox
    Friend WithEvents txtpan As common.Controls.MyTextBox
    Friend WithEvents txtpin As common.Controls.MyTextBox
    Friend WithEvents txtstate As common.Controls.MyTextBox
    Friend WithEvents lbladd1 As common.Controls.MyLabel
    Friend WithEvents lbladd2 As common.Controls.MyLabel
    Friend WithEvents lblphone As common.Controls.MyLabel
    Friend WithEvents lblemail As common.Controls.MyLabel
    Friend WithEvents lblname As common.Controls.MyLabel
    Friend WithEvents lblid As common.Controls.MyLabel
    Friend WithEvents lblstate As common.Controls.MyLabel
    Friend WithEvents lblcity As common.Controls.MyLabel
    Friend WithEvents lblpan As common.Controls.MyLabel
    Friend WithEvents lblpinno As common.Controls.MyLabel
    Friend WithEvents fndtrans As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

