<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAutoSTN
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
        Me.gvHead = New common.UserControls.MyRadGridView
        Me.btnCancel = New Telerik.WinControls.UI.RadButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton
        Me.chkForm38 = New Telerik.WinControls.UI.RadCheckBox
        Me.chkAgainst_Form = New Telerik.WinControls.UI.RadCheckBox
        Me.lblInvoiceType = New common.Controls.MyLabel
        Me.ddlInvoiceType = New common.Controls.MyComboBox
        Me.lblVehicleNo = New common.Controls.MyLabel
        Me.RadLabel20 = New common.Controls.MyLabel
        Me.txtVehicleCode = New common.UserControls.txtFinder
        Me.MyLabel9 = New common.Controls.MyLabel
        Me.lblTotRAmt1 = New common.Controls.MyLabel
        Me.RadLabel6 = New common.Controls.MyLabel
        Me.txtRemarks = New common.Controls.MyTextBox
        Me.lblToLoc = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtToLoc = New common.UserControls.txtFinder
        Me.RadLabel18 = New common.Controls.MyLabel
        Me.txtTransferNo = New common.UserControls.txtFinder
        Me.RadLabel4 = New common.Controls.MyLabel
        Me.txtTransferDate = New common.Controls.MyDateTimePicker
        Me.btnGo = New Telerik.WinControls.UI.RadButton
        Me.lblFromLoc = New common.Controls.MyLabel
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.txtFromLoc = New common.UserControls.txtFinder
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.btnOk = New Telerik.WinControls.UI.RadButton
        CType(Me.gvHead, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvHead.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkForm38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAgainst_Form, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvoiceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlInvoiceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTransferDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gvHead
        '
        Me.gvHead.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvHead.Location = New System.Drawing.Point(0, 0)
        Me.gvHead.Name = "gvHead"
        Me.gvHead.Size = New System.Drawing.Size(203, 326)
        Me.gvHead.TabIndex = 0
        Me.gvHead.TabStop = False
        Me.gvHead.Text = "RadGridView1"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnCancel.Location = New System.Drawing.Point(511, 2)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(130, 24)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Esc : Cancel"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnAddNew)
        Me.Panel1.Controls.Add(Me.chkForm38)
        Me.Panel1.Controls.Add(Me.chkAgainst_Form)
        Me.Panel1.Controls.Add(Me.lblInvoiceType)
        Me.Panel1.Controls.Add(Me.ddlInvoiceType)
        Me.Panel1.Controls.Add(Me.lblVehicleNo)
        Me.Panel1.Controls.Add(Me.RadLabel20)
        Me.Panel1.Controls.Add(Me.txtVehicleCode)
        Me.Panel1.Controls.Add(Me.MyLabel9)
        Me.Panel1.Controls.Add(Me.lblTotRAmt1)
        Me.Panel1.Controls.Add(Me.RadLabel6)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.lblToLoc)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.txtToLoc)
        Me.Panel1.Controls.Add(Me.RadLabel18)
        Me.Panel1.Controls.Add(Me.txtTransferNo)
        Me.Panel1.Controls.Add(Me.RadLabel4)
        Me.Panel1.Controls.Add(Me.txtTransferDate)
        Me.Panel1.Controls.Add(Me.btnGo)
        Me.Panel1.Controls.Add(Me.lblFromLoc)
        Me.Panel1.Controls.Add(Me.RadLabel2)
        Me.Panel1.Controls.Add(Me.txtFromLoc)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1016, 109)
        Me.Panel1.TabIndex = 0
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(203, 4)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 18)
        Me.btnAddNew.TabIndex = 1
        '
        'chkForm38
        '
        Me.chkForm38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkForm38.Location = New System.Drawing.Point(531, 8)
        Me.chkForm38.Name = "chkForm38"
        Me.chkForm38.Size = New System.Drawing.Size(59, 16)
        Me.chkForm38.TabIndex = 5
        Me.chkForm38.Text = "Form38"
        '
        'chkAgainst_Form
        '
        Me.chkAgainst_Form.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAgainst_Form.Location = New System.Drawing.Point(411, 7)
        Me.chkAgainst_Form.Name = "chkAgainst_Form"
        Me.chkAgainst_Form.Size = New System.Drawing.Size(98, 16)
        Me.chkAgainst_Form.TabIndex = 4
        Me.chkAgainst_Form.Text = "Against F Form"
        '
        'lblInvoiceType
        '
        Me.lblInvoiceType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceType.Location = New System.Drawing.Point(232, 4)
        Me.lblInvoiceType.Name = "lblInvoiceType"
        Me.lblInvoiceType.Size = New System.Drawing.Size(70, 16)
        Me.lblInvoiceType.TabIndex = 2
        Me.lblInvoiceType.Text = "Invoice Type"
        Me.lblInvoiceType.Visible = False
        '
        'ddlInvoiceType
        '
        Me.ddlInvoiceType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlInvoiceType.Location = New System.Drawing.Point(305, 3)
        Me.ddlInvoiceType.MendatroryField = True
        Me.ddlInvoiceType.MyLinkLable1 = Nothing
        Me.ddlInvoiceType.MyLinkLable2 = Nothing
        Me.ddlInvoiceType.Name = "ddlInvoiceType"
        Me.ddlInvoiceType.Size = New System.Drawing.Size(98, 20)
        Me.ddlInvoiceType.TabIndex = 3
        Me.ddlInvoiceType.Visible = False
        '
        'lblVehicleNo
        '
        Me.lblVehicleNo.AutoSize = False
        Me.lblVehicleNo.BorderVisible = True
        Me.lblVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicleNo.Location = New System.Drawing.Point(261, 68)
        Me.lblVehicleNo.Name = "lblVehicleNo"
        Me.lblVehicleNo.Size = New System.Drawing.Size(423, 18)
        Me.lblVehicleNo.TabIndex = 13
        Me.lblVehicleNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVehicleNo.TextWrap = False
        '
        'RadLabel20
        '
        Me.RadLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel20.Location = New System.Drawing.Point(8, 65)
        Me.RadLabel20.Name = "RadLabel20"
        Me.RadLabel20.Size = New System.Drawing.Size(74, 16)
        Me.RadLabel20.TabIndex = 138
        Me.RadLabel20.Text = "Vehicle Code"
        '
        'txtVehicleCode
        '
        Me.txtVehicleCode.Location = New System.Drawing.Point(117, 66)
        Me.txtVehicleCode.MendatroryField = False
        Me.txtVehicleCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleCode.MyLinkLable1 = Me.RadLabel20
        Me.txtVehicleCode.MyLinkLable2 = Me.lblVehicleNo
        Me.txtVehicleCode.MyReadOnly = False
        Me.txtVehicleCode.Name = "txtVehicleCode"
        Me.txtVehicleCode.Size = New System.Drawing.Size(143, 18)
        Me.txtVehicleCode.TabIndex = 12
        Me.txtVehicleCode.Value = ""
        '
        'MyLabel9
        '
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(698, 86)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(100, 16)
        Me.MyLabel9.TabIndex = 15
        Me.MyLabel9.Text = "Document Amount"
        '
        'lblTotRAmt1
        '
        Me.lblTotRAmt1.AutoSize = False
        Me.lblTotRAmt1.BorderVisible = True
        Me.lblTotRAmt1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt1.Location = New System.Drawing.Point(804, 84)
        Me.lblTotRAmt1.Name = "lblTotRAmt1"
        Me.lblTotRAmt1.Size = New System.Drawing.Size(124, 18)
        Me.lblTotRAmt1.TabIndex = 16
        Me.lblTotRAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel6
        '
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(8, 84)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel6.TabIndex = 34
        Me.RadLabel6.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(117, 86)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.RadLabel6
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(567, 18)
        Me.txtRemarks.TabIndex = 14
        '
        'lblToLoc
        '
        Me.lblToLoc.AutoSize = False
        Me.lblToLoc.BorderVisible = True
        Me.lblToLoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToLoc.Location = New System.Drawing.Point(261, 48)
        Me.lblToLoc.Name = "lblToLoc"
        Me.lblToLoc.Size = New System.Drawing.Size(423, 18)
        Me.lblToLoc.TabIndex = 11
        Me.lblToLoc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblToLoc.TextWrap = False
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(8, 46)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(97, 16)
        Me.MyLabel2.TabIndex = 32
        Me.MyLabel2.Text = "Order/To Location"
        '
        'txtToLoc
        '
        Me.txtToLoc.Location = New System.Drawing.Point(117, 46)
        Me.txtToLoc.MendatroryField = True
        Me.txtToLoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToLoc.MyLinkLable1 = Me.MyLabel2
        Me.txtToLoc.MyLinkLable2 = Me.lblToLoc
        Me.txtToLoc.MyReadOnly = False
        Me.txtToLoc.Name = "txtToLoc"
        Me.txtToLoc.Size = New System.Drawing.Size(143, 18)
        Me.txtToLoc.TabIndex = 10
        Me.txtToLoc.Value = ""
        '
        'RadLabel18
        '
        Me.RadLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel18.Location = New System.Drawing.Point(637, 8)
        Me.RadLabel18.Name = "RadLabel18"
        Me.RadLabel18.Size = New System.Drawing.Size(66, 16)
        Me.RadLabel18.TabIndex = 6
        Me.RadLabel18.Text = "Transfer No"
        Me.RadLabel18.Visible = False
        '
        'txtTransferNo
        '
        Me.txtTransferNo.Location = New System.Drawing.Point(746, 6)
        Me.txtTransferNo.MendatroryField = False
        Me.txtTransferNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransferNo.MyLinkLable1 = Me.RadLabel18
        Me.txtTransferNo.MyLinkLable2 = Nothing
        Me.txtTransferNo.MyReadOnly = False
        Me.txtTransferNo.Name = "txtTransferNo"
        Me.txtTransferNo.Size = New System.Drawing.Size(143, 18)
        Me.txtTransferNo.TabIndex = 7
        Me.txtTransferNo.Value = ""
        Me.txtTransferNo.Visible = False
        '
        'RadLabel4
        '
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(8, 5)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel4.TabIndex = 27
        Me.RadLabel4.Text = "Transfer Date"
        '
        'txtTransferDate
        '
        Me.txtTransferDate.CustomFormat = "dd/MM/yyyy"
        Me.txtTransferDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransferDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtTransferDate.Location = New System.Drawing.Point(120, 4)
        Me.txtTransferDate.MendatroryField = False
        Me.txtTransferDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtTransferDate.MyLinkLable1 = Me.RadLabel4
        Me.txtTransferDate.MyLinkLable2 = Nothing
        Me.txtTransferDate.Name = "txtTransferDate"
        Me.txtTransferDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtTransferDate.Size = New System.Drawing.Size(77, 18)
        Me.txtTransferDate.TabIndex = 0
        Me.txtTransferDate.TabStop = False
        Me.txtTransferDate.Text = "13/06/2011"
        Me.txtTransferDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'btnGo
        '
        Me.btnGo.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnGo.Location = New System.Drawing.Point(942, 81)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(67, 20)
        Me.btnGo.TabIndex = 17
        Me.btnGo.Text = ">>"
        '
        'lblFromLoc
        '
        Me.lblFromLoc.AutoSize = False
        Me.lblFromLoc.BorderVisible = True
        Me.lblFromLoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromLoc.Location = New System.Drawing.Point(261, 28)
        Me.lblFromLoc.Name = "lblFromLoc"
        Me.lblFromLoc.Size = New System.Drawing.Size(423, 18)
        Me.lblFromLoc.TabIndex = 9
        Me.lblFromLoc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFromLoc.TextWrap = False
        '
        'RadLabel2
        '
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(8, 27)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(79, 16)
        Me.RadLabel2.TabIndex = 24
        Me.RadLabel2.Text = "From Location"
        '
        'txtFromLoc
        '
        Me.txtFromLoc.Location = New System.Drawing.Point(117, 26)
        Me.txtFromLoc.MendatroryField = True
        Me.txtFromLoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromLoc.MyLinkLable1 = Me.RadLabel2
        Me.txtFromLoc.MyLinkLable2 = Me.lblFromLoc
        Me.txtFromLoc.MyReadOnly = False
        Me.txtFromLoc.Name = "txtFromLoc"
        Me.txtFromLoc.Size = New System.Drawing.Size(143, 18)
        Me.txtFromLoc.TabIndex = 8
        Me.txtFromLoc.Value = ""
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnOk)
        Me.SplitContainer1.Size = New System.Drawing.Size(1016, 468)
        Me.SplitContainer1.SplitterDistance = 435
        Me.SplitContainer1.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 109)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.gvHead)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(1016, 326)
        Me.SplitContainer2.SplitterDistance = 203
        Me.SplitContainer2.TabIndex = 0
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(809, 326)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'btnOk
        '
        Me.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnOk.Location = New System.Drawing.Point(375, 2)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(130, 24)
        Me.btnOk.TabIndex = 0
        Me.btnOk.Text = "F5 : OK"
        '
        'FrmAutoSTN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 468)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmAutoSTN"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Auto STN"
        CType(Me.gvHead.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvHead, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkForm38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAgainst_Form, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInvoiceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlInvoiceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToLoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTransferDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromLoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gvHead As common.UserControls.MyRadGridView
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblFromLoc As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents txtFromLoc As common.UserControls.txtFinder
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnOk As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents txtTransferDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToLoc As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtToLoc As common.UserControls.txtFinder
    Friend WithEvents RadLabel18 As common.Controls.MyLabel
    Friend WithEvents txtTransferNo As common.UserControls.txtFinder
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt1 As common.Controls.MyLabel
    Friend WithEvents lblVehicleNo As common.Controls.MyLabel
    Friend WithEvents RadLabel20 As common.Controls.MyLabel
    Friend WithEvents txtVehicleCode As common.UserControls.txtFinder
    Friend WithEvents lblInvoiceType As common.Controls.MyLabel
    Friend WithEvents ddlInvoiceType As common.Controls.MyComboBox
    Friend WithEvents chkForm38 As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkAgainst_Form As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
End Class

