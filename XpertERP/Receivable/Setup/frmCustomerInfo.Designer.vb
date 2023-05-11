<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCustomerInfo
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewComboBoxColumn1 As Telerik.WinControls.UI.GridViewComboBoxColumn = New Telerik.WinControls.UI.GridViewComboBoxColumn
        Dim SortDescriptor1 As Telerik.WinControls.Data.SortDescriptor = New Telerik.WinControls.Data.SortDescriptor
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.RadLabel4 = New common.Controls.MyLabel
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkInterBranch = New Telerik.WinControls.UI.RadCheckBox
        Me.RadSeparator2 = New Telerik.WinControls.UI.RadSeparator
        Me.fndCustomer = New common.UserControls.txtNavigator
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadLabel40 = New common.Controls.MyLabel
        Me.RadLabel36 = New common.Controls.MyLabel
        Me.txtAddInfo3 = New common.Controls.MyTextBox
        Me.RadLabel37 = New common.Controls.MyLabel
        Me.txtRemarks2 = New common.Controls.MyTextBox
        Me.RadLabel38 = New common.Controls.MyLabel
        Me.txtAddInfo2 = New common.Controls.MyTextBox
        Me.txtAddInfo1 = New common.Controls.MyTextBox
        Me.RadLabel39 = New common.Controls.MyLabel
        Me.txtRemarks1 = New common.Controls.MyTextBox
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage
        Me.fndPayCode = New common.UserControls.txtFinder
        Me.RadLabel26 = New common.Controls.MyLabel
        Me.fndAccntSet = New common.UserControls.txtFinder
        Me.RadLabel25 = New common.Controls.MyLabel
        Me.fndTrmsCode = New common.UserControls.txtFinder
        Me.RadLabel23 = New common.Controls.MyLabel
        Me.drpformtype = New Telerik.WinControls.UI.RadDropDownList
        Me.lbldivision = New common.Controls.MyLabel
        Me.txtdivision = New common.Controls.MyTextBox
        Me.lblpan = New common.Controls.MyLabel
        Me.txtpan = New common.Controls.MyTextBox
        Me.lblcollectorate = New common.Controls.MyLabel
        Me.txtcollect = New common.Controls.MyTextBox
        Me.lblrange = New common.Controls.MyLabel
        Me.lblecc = New common.Controls.MyLabel
        Me.txtrange = New common.Controls.MyTextBox
        Me.txtecc = New common.Controls.MyTextBox
        Me.lblcst = New common.Controls.MyLabel
        Me.txtcst = New common.Controls.MyTextBox
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.fndTxGrp = New common.UserControls.txtFinder
        Me.RadLabel35 = New common.Controls.MyLabel
        Me.grdTax = New common.UserControls.MyRadGridView
        Me.txtTxGrp = New common.Controls.MyTextBox
        Me.RadLabel30 = New common.Controls.MyLabel
        Me.RadLabel29 = New common.Controls.MyLabel
        Me.txtCredit = New common.Controls.MyTextBox
        Me.txtLstNo = New common.Controls.MyTextBox
        Me.RadLabel31 = New common.Controls.MyLabel
        Me.RadLabel28 = New common.Controls.MyLabel
        Me.RadLabel27 = New common.Controls.MyLabel
        Me.txtTinNo = New common.Controls.MyTextBox
        Me.txtStaxNo = New common.Controls.MyTextBox
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadLabel41 = New common.Controls.MyLabel
        Me.RadLabel16 = New common.Controls.MyLabel
        Me.txtContPhone = New common.Controls.MyTextBox
        Me.RadLabel14 = New common.Controls.MyLabel
        Me.txtContactFax = New common.Controls.MyTextBox
        Me.RadLabel15 = New common.Controls.MyLabel
        Me.txtContactWeb = New common.Controls.MyTextBox
        Me.txtContactEmail = New common.Controls.MyTextBox
        Me.RadLabel13 = New common.Controls.MyLabel
        Me.txtContactName = New common.Controls.MyTextBox
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.txtStateName = New common.Controls.MyTextBox
        Me.fndstate = New common.UserControls.txtFinder
        Me.RadLabel6 = New common.Controls.MyLabel
        Me.fndCity = New common.UserControls.txtFinder
        Me.RadLabel5 = New common.Controls.MyLabel
        Me.fndCusgrp = New common.UserControls.txtFinder
        Me.lblCusGrp = New common.Controls.MyLabel
        Me.chkcredit = New Telerik.WinControls.UI.RadCheckBox
        Me.RadLabel12 = New common.Controls.MyLabel
        Me.RadLabel11 = New common.Controls.MyLabel
        Me.txtWeb = New common.Controls.MyTextBox
        Me.txtfax = New common.Controls.MyTextBox
        Me.txtEmail = New common.Controls.MyTextBox
        Me.txtPhone2 = New common.Controls.MyTextBox
        Me.RadLabel8 = New common.Controls.MyLabel
        Me.RadLabel10 = New common.Controls.MyLabel
        Me.txtCountry = New common.Controls.MyTextBox
        Me.txtPhone1 = New common.Controls.MyTextBox
        Me.RadLabel9 = New common.Controls.MyLabel
        Me.RadLabel7 = New common.Controls.MyLabel
        Me.txtCity = New common.Controls.MyTextBox
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.chkHold = New Telerik.WinControls.UI.RadCheckBox
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.txtAdd2 = New common.Controls.MyTextBox
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.txtAdd1 = New common.Controls.MyTextBox
        Me.txtAdd3 = New common.Controls.MyTextBox
        Me.txtCusgrp = New common.Controls.MyTextBox
        Me.pageCus = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage
        Me.dgvVisi = New common.UserControls.MyRadGridView
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.ddlCustType = New common.Controls.MyComboBox
        Me.chkDistributer = New Telerik.WinControls.UI.RadCheckBox
        Me.fndcust = New common.UserControls.txtFinder
        Me.lblcust = New common.Controls.MyLabel
        Me.txtCustomerName = New Telerik.WinControls.UI.RadTextBox
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInterBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSeparator2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.RadLabel40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel36, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddInfo3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddInfo2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddInfo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.RadLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.drpformtype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldivision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdivision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtpan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcollectorate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcollect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblrange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblecc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtrange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtecc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcst, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcst, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTax.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTxGrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel30, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCredit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLstNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTinNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStaxNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadLabel41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtContPhone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtContactFax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtContactWeb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtContactEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtContactName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.txtStateName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCusGrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkcredit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWeb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPhone2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPhone1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkHold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCusgrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pageCus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageCus.SuspendLayout()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.dgvVisi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvVisi.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlCustType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDistributer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcust, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(735, 20)
        Me.RadMenu1.TabIndex = 74
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3, Me.RadMenuItem4, Me.RadMenuItem2})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Import"
        Me.RadMenuItem3.AccessibleName = "Import"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Import"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Export"
        Me.RadMenuItem4.AccessibleName = "Export"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Export"
        Me.RadMenuItem4.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Exit"
        Me.RadMenuItem2.AccessibleName = "Exit"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Exit"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(643, 571)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 18)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(91, 571)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 18)
        Me.btnDelete.TabIndex = 8
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(8, 571)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 18)
        Me.btnSave.TabIndex = 7
        Me.btnSave.Text = "Save"
        '
        'RadLabel4
        '
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(14, 74)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(89, 16)
        Me.RadLabel4.TabIndex = 85
        Me.RadLabel4.Text = "Customer Name"
        '
        'btnNew
        '
        Me.btnNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnNew.Location = New System.Drawing.Point(335, 48)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(15, 20)
        Me.btnNew.TabIndex = 2
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(12, 48)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(76, 16)
        Me.RadLabel1.TabIndex = 84
        Me.RadLabel1.Text = "Customer No."
        '
        'chkInterBranch
        '
        Me.chkInterBranch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInterBranch.Location = New System.Drawing.Point(392, 47)
        Me.chkInterBranch.Name = "chkInterBranch"
        Me.chkInterBranch.Size = New System.Drawing.Size(82, 16)
        Me.chkInterBranch.TabIndex = 1
        Me.chkInterBranch.Text = "Inter Branch"
        '
        'RadSeparator2
        '
        Me.RadSeparator2.Location = New System.Drawing.Point(3, 25)
        Me.RadSeparator2.Name = "RadSeparator2"
        Me.RadSeparator2.Size = New System.Drawing.Size(728, 17)
        Me.RadSeparator2.TabIndex = 0
        Me.RadSeparator2.Text = "RadSeparator2"
        '
        'fndCustomer
        '
        Me.fndCustomer.Location = New System.Drawing.Point(115, 47)
        Me.fndCustomer.MendatroryField = True
        Me.fndCustomer.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCustomer.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCustomer.MyLinkLable1 = Nothing
        Me.fndCustomer.MyLinkLable2 = Nothing
        Me.fndCustomer.MyMaxLength = 32767
        Me.fndCustomer.MyReadOnly = False
        Me.fndCustomer.Name = "fndCustomer"
        Me.fndCustomer.Size = New System.Drawing.Size(218, 21)
        Me.fndCustomer.TabIndex = 0
        Me.fndCustomer.Value = ""
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.RadLabel40)
        Me.RadPageViewPage5.Controls.Add(Me.RadLabel36)
        Me.RadPageViewPage5.Controls.Add(Me.txtAddInfo3)
        Me.RadPageViewPage5.Controls.Add(Me.RadLabel37)
        Me.RadPageViewPage5.Controls.Add(Me.txtRemarks2)
        Me.RadPageViewPage5.Controls.Add(Me.RadLabel38)
        Me.RadPageViewPage5.Controls.Add(Me.txtAddInfo2)
        Me.RadPageViewPage5.Controls.Add(Me.txtAddInfo1)
        Me.RadPageViewPage5.Controls.Add(Me.RadLabel39)
        Me.RadPageViewPage5.Controls.Add(Me.txtRemarks1)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(705, 422)
        Me.RadPageViewPage5.Text = "Additional Info."
        '
        'RadLabel40
        '
        Me.RadLabel40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel40.Location = New System.Drawing.Point(9, 109)
        Me.RadLabel40.Name = "RadLabel40"
        Me.RadLabel40.Size = New System.Drawing.Size(122, 16)
        Me.RadLabel40.TabIndex = 88
        Me.RadLabel40.Text = "Additional Information3"
        '
        'RadLabel36
        '
        Me.RadLabel36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel36.Location = New System.Drawing.Point(9, 33)
        Me.RadLabel36.Name = "RadLabel36"
        Me.RadLabel36.Size = New System.Drawing.Size(58, 16)
        Me.RadLabel36.TabIndex = 89
        Me.RadLabel36.Text = "Remarks2"
        '
        'txtAddInfo3
        '
        Me.txtAddInfo3.Location = New System.Drawing.Point(137, 107)
        Me.txtAddInfo3.MaxLength = 75
        Me.txtAddInfo3.MendatroryField = False
        Me.txtAddInfo3.MyLinkLable1 = Nothing
        Me.txtAddInfo3.MyLinkLable2 = Nothing
        Me.txtAddInfo3.Name = "txtAddInfo3"
        Me.txtAddInfo3.Size = New System.Drawing.Size(550, 20)
        Me.txtAddInfo3.TabIndex = 4
        '
        'RadLabel37
        '
        Me.RadLabel37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel37.Location = New System.Drawing.Point(9, 83)
        Me.RadLabel37.Name = "RadLabel37"
        Me.RadLabel37.Size = New System.Drawing.Size(122, 16)
        Me.RadLabel37.TabIndex = 86
        Me.RadLabel37.Text = "Additional Information2"
        '
        'txtRemarks2
        '
        Me.txtRemarks2.Location = New System.Drawing.Point(137, 29)
        Me.txtRemarks2.MaxLength = 75
        Me.txtRemarks2.MendatroryField = False
        Me.txtRemarks2.MyLinkLable1 = Nothing
        Me.txtRemarks2.MyLinkLable2 = Nothing
        Me.txtRemarks2.Name = "txtRemarks2"
        Me.txtRemarks2.Size = New System.Drawing.Size(550, 20)
        Me.txtRemarks2.TabIndex = 1
        '
        'RadLabel38
        '
        Me.RadLabel38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel38.Location = New System.Drawing.Point(9, 58)
        Me.RadLabel38.Name = "RadLabel38"
        Me.RadLabel38.Size = New System.Drawing.Size(122, 16)
        Me.RadLabel38.TabIndex = 87
        Me.RadLabel38.Text = "Additional Information1"
        '
        'txtAddInfo2
        '
        Me.txtAddInfo2.Location = New System.Drawing.Point(137, 81)
        Me.txtAddInfo2.MaxLength = 75
        Me.txtAddInfo2.MendatroryField = False
        Me.txtAddInfo2.MyLinkLable1 = Nothing
        Me.txtAddInfo2.MyLinkLable2 = Nothing
        Me.txtAddInfo2.Name = "txtAddInfo2"
        Me.txtAddInfo2.Size = New System.Drawing.Size(550, 20)
        Me.txtAddInfo2.TabIndex = 3
        '
        'txtAddInfo1
        '
        Me.txtAddInfo1.Location = New System.Drawing.Point(137, 55)
        Me.txtAddInfo1.MaxLength = 75
        Me.txtAddInfo1.MendatroryField = False
        Me.txtAddInfo1.MyLinkLable1 = Nothing
        Me.txtAddInfo1.MyLinkLable2 = Nothing
        Me.txtAddInfo1.Name = "txtAddInfo1"
        Me.txtAddInfo1.Size = New System.Drawing.Size(550, 20)
        Me.txtAddInfo1.TabIndex = 2
        '
        'RadLabel39
        '
        Me.RadLabel39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel39.Location = New System.Drawing.Point(9, 3)
        Me.RadLabel39.Name = "RadLabel39"
        Me.RadLabel39.Size = New System.Drawing.Size(58, 16)
        Me.RadLabel39.TabIndex = 83
        Me.RadLabel39.Text = "Remarks1"
        '
        'txtRemarks1
        '
        Me.txtRemarks1.Location = New System.Drawing.Point(137, 3)
        Me.txtRemarks1.MaxLength = 75
        Me.txtRemarks1.MendatroryField = False
        Me.txtRemarks1.MyLinkLable1 = Nothing
        Me.txtRemarks1.MyLinkLable2 = Nothing
        Me.txtRemarks1.Name = "txtRemarks1"
        Me.txtRemarks1.Size = New System.Drawing.Size(550, 20)
        Me.txtRemarks1.TabIndex = 0
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.fndPayCode)
        Me.RadPageViewPage4.Controls.Add(Me.fndAccntSet)
        Me.RadPageViewPage4.Controls.Add(Me.fndTrmsCode)
        Me.RadPageViewPage4.Controls.Add(Me.drpformtype)
        Me.RadPageViewPage4.Controls.Add(Me.lbldivision)
        Me.RadPageViewPage4.Controls.Add(Me.txtdivision)
        Me.RadPageViewPage4.Controls.Add(Me.lblpan)
        Me.RadPageViewPage4.Controls.Add(Me.txtpan)
        Me.RadPageViewPage4.Controls.Add(Me.lblcollectorate)
        Me.RadPageViewPage4.Controls.Add(Me.txtcollect)
        Me.RadPageViewPage4.Controls.Add(Me.lblrange)
        Me.RadPageViewPage4.Controls.Add(Me.lblecc)
        Me.RadPageViewPage4.Controls.Add(Me.txtrange)
        Me.RadPageViewPage4.Controls.Add(Me.txtecc)
        Me.RadPageViewPage4.Controls.Add(Me.lblcst)
        Me.RadPageViewPage4.Controls.Add(Me.txtcst)
        Me.RadPageViewPage4.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel30)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel29)
        Me.RadPageViewPage4.Controls.Add(Me.txtCredit)
        Me.RadPageViewPage4.Controls.Add(Me.txtLstNo)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel31)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel28)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel27)
        Me.RadPageViewPage4.Controls.Add(Me.txtTinNo)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel26)
        Me.RadPageViewPage4.Controls.Add(Me.txtStaxNo)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel25)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel23)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(705, 422)
        Me.RadPageViewPage4.Text = "Process"
        '
        'fndPayCode
        '
        Me.fndPayCode.Location = New System.Drawing.Point(115, 54)
        Me.fndPayCode.MendatroryField = False
        Me.fndPayCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPayCode.MyLinkLable1 = Me.RadLabel26
        Me.fndPayCode.MyLinkLable2 = Nothing
        Me.fndPayCode.MyReadOnly = False
        Me.fndPayCode.Name = "fndPayCode"
        Me.fndPayCode.Size = New System.Drawing.Size(143, 19)
        Me.fndPayCode.TabIndex = 4
        Me.fndPayCode.Value = ""
        '
        'RadLabel26
        '
        Me.RadLabel26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel26.Location = New System.Drawing.Point(10, 58)
        Me.RadLabel26.Name = "RadLabel26"
        Me.RadLabel26.Size = New System.Drawing.Size(81, 16)
        Me.RadLabel26.TabIndex = 69
        Me.RadLabel26.Text = "Payment Code"
        '
        'fndAccntSet
        '
        Me.fndAccntSet.Location = New System.Drawing.Point(115, 30)
        Me.fndAccntSet.MendatroryField = False
        Me.fndAccntSet.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndAccntSet.MyLinkLable1 = Me.RadLabel25
        Me.fndAccntSet.MyLinkLable2 = Nothing
        Me.fndAccntSet.MyReadOnly = False
        Me.fndAccntSet.Name = "fndAccntSet"
        Me.fndAccntSet.Size = New System.Drawing.Size(143, 19)
        Me.fndAccntSet.TabIndex = 2
        Me.fndAccntSet.Value = ""
        '
        'RadLabel25
        '
        Me.RadLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel25.Location = New System.Drawing.Point(10, 32)
        Me.RadLabel25.Name = "RadLabel25"
        Me.RadLabel25.Size = New System.Drawing.Size(67, 16)
        Me.RadLabel25.TabIndex = 67
        Me.RadLabel25.Text = "Account Set"
        '
        'fndTrmsCode
        '
        Me.fndTrmsCode.Location = New System.Drawing.Point(115, 4)
        Me.fndTrmsCode.MendatroryField = False
        Me.fndTrmsCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTrmsCode.MyLinkLable1 = Me.RadLabel23
        Me.fndTrmsCode.MyLinkLable2 = Nothing
        Me.fndTrmsCode.MyReadOnly = False
        Me.fndTrmsCode.Name = "fndTrmsCode"
        Me.fndTrmsCode.Size = New System.Drawing.Size(143, 19)
        Me.fndTrmsCode.TabIndex = 0
        Me.fndTrmsCode.Value = ""
        '
        'RadLabel23
        '
        Me.RadLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel23.Location = New System.Drawing.Point(10, 7)
        Me.RadLabel23.Name = "RadLabel23"
        Me.RadLabel23.Size = New System.Drawing.Size(68, 16)
        Me.RadLabel23.TabIndex = 65
        Me.RadLabel23.Text = "Terms Code"
        '
        'drpformtype
        '
        RadListDataItem1.Text = "Form C"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Form F"
        RadListDataItem2.TextWrap = True
        Me.drpformtype.Items.Add(RadListDataItem1)
        Me.drpformtype.Items.Add(RadListDataItem2)
        Me.drpformtype.Location = New System.Drawing.Point(115, 158)
        Me.drpformtype.Name = "drpformtype"
        Me.drpformtype.Size = New System.Drawing.Size(109, 20)
        Me.drpformtype.TabIndex = 12
        '
        'lbldivision
        '
        Me.lbldivision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldivision.Location = New System.Drawing.Point(350, 128)
        Me.lbldivision.Name = "lbldivision"
        Me.lbldivision.Size = New System.Drawing.Size(46, 16)
        Me.lbldivision.TabIndex = 101
        Me.lbldivision.Text = "Division"
        '
        'txtdivision
        '
        Me.txtdivision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdivision.Location = New System.Drawing.Point(423, 128)
        Me.txtdivision.MaxLength = 30
        Me.txtdivision.MendatroryField = False
        Me.txtdivision.MyLinkLable1 = Me.lbldivision
        Me.txtdivision.MyLinkLable2 = Nothing
        Me.txtdivision.Name = "txtdivision"
        Me.txtdivision.Size = New System.Drawing.Size(242, 18)
        Me.txtdivision.TabIndex = 11
        '
        'lblpan
        '
        Me.lblpan.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpan.Location = New System.Drawing.Point(350, 106)
        Me.lblpan.Name = "lblpan"
        Me.lblpan.Size = New System.Drawing.Size(50, 16)
        Me.lblpan.TabIndex = 99
        Me.lblpan.Text = "PAN No."
        '
        'txtpan
        '
        Me.txtpan.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpan.Location = New System.Drawing.Point(423, 106)
        Me.txtpan.MaxLength = 30
        Me.txtpan.MendatroryField = False
        Me.txtpan.MyLinkLable1 = Me.lblpan
        Me.txtpan.MyLinkLable2 = Nothing
        Me.txtpan.Name = "txtpan"
        Me.txtpan.Size = New System.Drawing.Size(242, 18)
        Me.txtpan.TabIndex = 9
        '
        'lblcollectorate
        '
        Me.lblcollectorate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcollectorate.Location = New System.Drawing.Point(350, 83)
        Me.lblcollectorate.Name = "lblcollectorate"
        Me.lblcollectorate.Size = New System.Drawing.Size(67, 16)
        Me.lblcollectorate.TabIndex = 95
        Me.lblcollectorate.Text = "Collectorate"
        '
        'txtcollect
        '
        Me.txtcollect.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcollect.Location = New System.Drawing.Point(423, 81)
        Me.txtcollect.MaxLength = 30
        Me.txtcollect.MendatroryField = False
        Me.txtcollect.MyLinkLable1 = Me.lblcollectorate
        Me.txtcollect.MyLinkLable2 = Nothing
        Me.txtcollect.Name = "txtcollect"
        Me.txtcollect.Size = New System.Drawing.Size(242, 18)
        Me.txtcollect.TabIndex = 7
        '
        'lblrange
        '
        Me.lblrange.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblrange.Location = New System.Drawing.Point(350, 59)
        Me.lblrange.Name = "lblrange"
        Me.lblrange.Size = New System.Drawing.Size(40, 16)
        Me.lblrange.TabIndex = 94
        Me.lblrange.Text = "Range"
        '
        'lblecc
        '
        Me.lblecc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblecc.Location = New System.Drawing.Point(350, 31)
        Me.lblecc.Name = "lblecc"
        Me.lblecc.Size = New System.Drawing.Size(48, 16)
        Me.lblecc.TabIndex = 97
        Me.lblecc.Text = "ECC No"
        '
        'txtrange
        '
        Me.txtrange.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtrange.Location = New System.Drawing.Point(423, 55)
        Me.txtrange.MaxLength = 30
        Me.txtrange.MendatroryField = False
        Me.txtrange.MyLinkLable1 = Me.lblrange
        Me.txtrange.MyLinkLable2 = Nothing
        Me.txtrange.Name = "txtrange"
        Me.txtrange.Size = New System.Drawing.Size(242, 18)
        Me.txtrange.TabIndex = 5
        '
        'txtecc
        '
        Me.txtecc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtecc.Location = New System.Drawing.Point(423, 29)
        Me.txtecc.MaxLength = 30
        Me.txtecc.MendatroryField = False
        Me.txtecc.MyLinkLable1 = Me.lblecc
        Me.txtecc.MyLinkLable2 = Nothing
        Me.txtecc.Name = "txtecc"
        Me.txtecc.Size = New System.Drawing.Size(242, 18)
        Me.txtecc.TabIndex = 3
        '
        'lblcst
        '
        Me.lblcst.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcst.Location = New System.Drawing.Point(350, 4)
        Me.lblcst.Name = "lblcst"
        Me.lblcst.Size = New System.Drawing.Size(46, 16)
        Me.lblcst.TabIndex = 96
        Me.lblcst.Text = "CST No"
        '
        'txtcst
        '
        Me.txtcst.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcst.Location = New System.Drawing.Point(423, 4)
        Me.txtcst.MaxLength = 30
        Me.txtcst.MendatroryField = False
        Me.txtcst.MyLinkLable1 = Me.lblcst
        Me.txtcst.MyLinkLable2 = Nothing
        Me.txtcst.Name = "txtcst"
        Me.txtcst.Size = New System.Drawing.Size(242, 18)
        Me.txtcst.TabIndex = 1
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.fndTxGrp)
        Me.RadGroupBox3.Controls.Add(Me.grdTax)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel35)
        Me.RadGroupBox3.Controls.Add(Me.txtTxGrp)
        Me.RadGroupBox3.HeaderText = "Tax Group"
        Me.RadGroupBox3.Location = New System.Drawing.Point(3, 184)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(696, 231)
        Me.RadGroupBox3.TabIndex = 80
        Me.RadGroupBox3.Text = "Tax Group"
        '
        'fndTxGrp
        '
        Me.fndTxGrp.Location = New System.Drawing.Point(112, 24)
        Me.fndTxGrp.MendatroryField = False
        Me.fndTxGrp.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTxGrp.MyLinkLable1 = Me.RadLabel35
        Me.fndTxGrp.MyLinkLable2 = Nothing
        Me.fndTxGrp.MyReadOnly = False
        Me.fndTxGrp.Name = "fndTxGrp"
        Me.fndTxGrp.Size = New System.Drawing.Size(143, 19)
        Me.fndTxGrp.TabIndex = 0
        Me.fndTxGrp.Value = ""
        '
        'RadLabel35
        '
        Me.RadLabel35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel35.Location = New System.Drawing.Point(13, 27)
        Me.RadLabel35.Name = "RadLabel35"
        Me.RadLabel35.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel35.TabIndex = 70
        Me.RadLabel35.Text = "Tax Group"
        '
        'grdTax
        '
        Me.grdTax.BackColor = System.Drawing.Color.White
        Me.grdTax.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdTax.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.grdTax.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grdTax.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.grdTax.Location = New System.Drawing.Point(13, 54)
        '
        '
        '
        Me.grdTax.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GridViewTextBoxColumn1.HeaderText = "Tax Authority"
        GridViewTextBoxColumn1.Name = "gdTxAuth"
        GridViewTextBoxColumn1.ReadOnly = True
        GridViewTextBoxColumn1.Width = 350
        GridViewComboBoxColumn1.HeaderText = "Tax Rate"
        GridViewComboBoxColumn1.Name = "gdTxRate"
        GridViewComboBoxColumn1.Width = 300
        Me.grdTax.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewComboBoxColumn1})
        Me.grdTax.MasterTemplate.EnableGrouping = False
        Me.grdTax.Name = "grdTax"
        Me.grdTax.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grdTax.Size = New System.Drawing.Size(670, 171)
        Me.grdTax.TabIndex = 2
        Me.grdTax.Text = "RadGridView1"
        '
        'txtTxGrp
        '
        Me.txtTxGrp.Location = New System.Drawing.Point(266, 23)
        Me.txtTxGrp.MendatroryField = False
        Me.txtTxGrp.MyLinkLable1 = Nothing
        Me.txtTxGrp.MyLinkLable2 = Nothing
        Me.txtTxGrp.Name = "txtTxGrp"
        Me.txtTxGrp.ReadOnly = True
        Me.txtTxGrp.Size = New System.Drawing.Size(417, 20)
        Me.txtTxGrp.TabIndex = 1
        '
        'RadLabel30
        '
        Me.RadLabel30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel30.Location = New System.Drawing.Point(10, 130)
        Me.RadLabel30.Name = "RadLabel30"
        Me.RadLabel30.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel30.TabIndex = 75
        Me.RadLabel30.Text = "Credit Limit"
        '
        'RadLabel29
        '
        Me.RadLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel29.Location = New System.Drawing.Point(10, 108)
        Me.RadLabel29.Name = "RadLabel29"
        Me.RadLabel29.Size = New System.Drawing.Size(48, 16)
        Me.RadLabel29.TabIndex = 71
        Me.RadLabel29.Text = "LST No."
        '
        'txtCredit
        '
        Me.txtCredit.Location = New System.Drawing.Point(116, 132)
        Me.txtCredit.MaxLength = 12
        Me.txtCredit.MendatroryField = False
        Me.txtCredit.MyLinkLable1 = Me.RadLabel30
        Me.txtCredit.MyLinkLable2 = Nothing
        Me.txtCredit.Name = "txtCredit"
        Me.txtCredit.Size = New System.Drawing.Size(165, 20)
        Me.txtCredit.TabIndex = 10
        Me.txtCredit.Text = "0.00"
        Me.txtCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtLstNo
        '
        Me.txtLstNo.Location = New System.Drawing.Point(115, 106)
        Me.txtLstNo.MaxLength = 15
        Me.txtLstNo.MendatroryField = False
        Me.txtLstNo.MyLinkLable1 = Me.RadLabel29
        Me.txtLstNo.MyLinkLable2 = Nothing
        Me.txtLstNo.Name = "txtLstNo"
        Me.txtLstNo.Size = New System.Drawing.Size(165, 20)
        Me.txtLstNo.TabIndex = 8
        '
        'RadLabel31
        '
        Me.RadLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel31.Location = New System.Drawing.Point(12, 156)
        Me.RadLabel31.Name = "RadLabel31"
        Me.RadLabel31.Size = New System.Drawing.Size(61, 16)
        Me.RadLabel31.TabIndex = 73
        Me.RadLabel31.Text = "Form Type"
        '
        'RadLabel28
        '
        Me.RadLabel28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel28.Location = New System.Drawing.Point(350, 155)
        Me.RadLabel28.Name = "RadLabel28"
        Me.RadLabel28.Size = New System.Drawing.Size(45, 16)
        Me.RadLabel28.TabIndex = 68
        Me.RadLabel28.Text = "TIN No."
        '
        'RadLabel27
        '
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(10, 83)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(87, 16)
        Me.RadLabel27.TabIndex = 66
        Me.RadLabel27.Text = "Service Tax No."
        '
        'txtTinNo
        '
        Me.txtTinNo.Location = New System.Drawing.Point(423, 152)
        Me.txtTinNo.MaxLength = 15
        Me.txtTinNo.MendatroryField = False
        Me.txtTinNo.MyLinkLable1 = Me.RadLabel28
        Me.txtTinNo.MyLinkLable2 = Nothing
        Me.txtTinNo.Name = "txtTinNo"
        Me.txtTinNo.Size = New System.Drawing.Size(242, 20)
        Me.txtTinNo.TabIndex = 13
        '
        'txtStaxNo
        '
        Me.txtStaxNo.Location = New System.Drawing.Point(115, 81)
        Me.txtStaxNo.MaxLength = 15
        Me.txtStaxNo.MendatroryField = False
        Me.txtStaxNo.MyLinkLable1 = Me.RadLabel27
        Me.txtStaxNo.MyLinkLable2 = Nothing
        Me.txtStaxNo.Name = "txtStaxNo"
        Me.txtStaxNo.Size = New System.Drawing.Size(165, 20)
        Me.txtStaxNo.TabIndex = 6
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel41)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel16)
        Me.RadPageViewPage2.Controls.Add(Me.txtContPhone)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel14)
        Me.RadPageViewPage2.Controls.Add(Me.txtContactFax)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel15)
        Me.RadPageViewPage2.Controls.Add(Me.txtContactWeb)
        Me.RadPageViewPage2.Controls.Add(Me.txtContactEmail)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel13)
        Me.RadPageViewPage2.Controls.Add(Me.txtContactName)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(705, 422)
        Me.RadPageViewPage2.Text = "Contact Person"
        '
        'RadLabel41
        '
        Me.RadLabel41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel41.Location = New System.Drawing.Point(8, 46)
        Me.RadLabel41.Name = "RadLabel41"
        Me.RadLabel41.Size = New System.Drawing.Size(39, 16)
        Me.RadLabel41.TabIndex = 83
        Me.RadLabel41.Text = "Phone"
        '
        'RadLabel16
        '
        Me.RadLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel16.Location = New System.Drawing.Point(8, 76)
        Me.RadLabel16.Name = "RadLabel16"
        Me.RadLabel16.Size = New System.Drawing.Size(25, 16)
        Me.RadLabel16.TabIndex = 81
        Me.RadLabel16.Text = "Fax"
        '
        'txtContPhone
        '
        Me.txtContPhone.Location = New System.Drawing.Point(100, 46)
        Me.txtContPhone.MaxLength = 15
        Me.txtContPhone.MendatroryField = False
        Me.txtContPhone.MyLinkLable1 = Me.RadLabel41
        Me.txtContPhone.MyLinkLable2 = Nothing
        Me.txtContPhone.Name = "txtContPhone"
        Me.txtContPhone.Size = New System.Drawing.Size(229, 20)
        Me.txtContPhone.TabIndex = 1
        '
        'RadLabel14
        '
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(8, 128)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel14.TabIndex = 78
        Me.RadLabel14.Text = "WebSite"
        '
        'txtContactFax
        '
        Me.txtContactFax.Location = New System.Drawing.Point(100, 73)
        Me.txtContactFax.MaxLength = 15
        Me.txtContactFax.MendatroryField = False
        Me.txtContactFax.MyLinkLable1 = Me.RadLabel16
        Me.txtContactFax.MyLinkLable2 = Nothing
        Me.txtContactFax.Name = "txtContactFax"
        Me.txtContactFax.Size = New System.Drawing.Size(229, 20)
        Me.txtContactFax.TabIndex = 2
        '
        'RadLabel15
        '
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(8, 102)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(38, 16)
        Me.RadLabel15.TabIndex = 79
        Me.RadLabel15.Text = "E-Mail"
        '
        'txtContactWeb
        '
        Me.txtContactWeb.Location = New System.Drawing.Point(100, 126)
        Me.txtContactWeb.MaxLength = 50
        Me.txtContactWeb.MendatroryField = False
        Me.txtContactWeb.MyLinkLable1 = Me.RadLabel14
        Me.txtContactWeb.MyLinkLable2 = Nothing
        Me.txtContactWeb.Name = "txtContactWeb"
        Me.txtContactWeb.Size = New System.Drawing.Size(586, 20)
        Me.txtContactWeb.TabIndex = 4
        '
        'txtContactEmail
        '
        Me.txtContactEmail.Location = New System.Drawing.Point(100, 99)
        Me.txtContactEmail.MaxLength = 50
        Me.txtContactEmail.MendatroryField = False
        Me.txtContactEmail.MyLinkLable1 = Me.RadLabel15
        Me.txtContactEmail.MyLinkLable2 = Nothing
        Me.txtContactEmail.Name = "txtContactEmail"
        Me.txtContactEmail.Size = New System.Drawing.Size(586, 20)
        Me.txtContactEmail.TabIndex = 3
        '
        'RadLabel13
        '
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(8, 19)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(79, 16)
        Me.RadLabel13.TabIndex = 66
        Me.RadLabel13.Text = "Contact Name"
        '
        'txtContactName
        '
        Me.txtContactName.Location = New System.Drawing.Point(100, 19)
        Me.txtContactName.MaxLength = 50
        Me.txtContactName.MendatroryField = False
        Me.txtContactName.MyLinkLable1 = Me.RadLabel13
        Me.txtContactName.MyLinkLable2 = Nothing
        Me.txtContactName.Name = "txtContactName"
        Me.txtContactName.Size = New System.Drawing.Size(586, 20)
        Me.txtContactName.TabIndex = 0
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtStateName)
        Me.RadPageViewPage1.Controls.Add(Me.fndstate)
        Me.RadPageViewPage1.Controls.Add(Me.fndCity)
        Me.RadPageViewPage1.Controls.Add(Me.fndCusgrp)
        Me.RadPageViewPage1.Controls.Add(Me.chkcredit)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.txtWeb)
        Me.RadPageViewPage1.Controls.Add(Me.txtfax)
        Me.RadPageViewPage1.Controls.Add(Me.txtEmail)
        Me.RadPageViewPage1.Controls.Add(Me.txtPhone2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.txtCountry)
        Me.RadPageViewPage1.Controls.Add(Me.txtPhone1)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.txtCity)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdd2)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdd1)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdd3)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.lblCusGrp)
        Me.RadPageViewPage1.Controls.Add(Me.txtCusgrp)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(705, 422)
        Me.RadPageViewPage1.Text = "Address"
        '
        'txtStateName
        '
        Me.txtStateName.Location = New System.Drawing.Point(254, 214)
        Me.txtStateName.MendatroryField = False
        Me.txtStateName.MyLinkLable1 = Nothing
        Me.txtStateName.MyLinkLable2 = Nothing
        Me.txtStateName.Name = "txtStateName"
        Me.txtStateName.ReadOnly = True
        Me.txtStateName.Size = New System.Drawing.Size(159, 20)
        Me.txtStateName.TabIndex = 8
        '
        'fndstate
        '
        Me.fndstate.Location = New System.Drawing.Point(100, 215)
        Me.fndstate.MendatroryField = False
        Me.fndstate.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndstate.MyLinkLable1 = Me.RadLabel6
        Me.fndstate.MyLinkLable2 = Nothing
        Me.fndstate.MyReadOnly = False
        Me.fndstate.Name = "fndstate"
        Me.fndstate.Size = New System.Drawing.Size(143, 19)
        Me.fndstate.TabIndex = 7
        Me.fndstate.Value = ""
        '
        'RadLabel6
        '
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(11, 216)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel6.TabIndex = 70
        Me.RadLabel6.Text = "State"
        '
        'fndCity
        '
        Me.fndCity.Location = New System.Drawing.Point(100, 185)
        Me.fndCity.MendatroryField = False
        Me.fndCity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCity.MyLinkLable1 = Me.RadLabel5
        Me.fndCity.MyLinkLable2 = Nothing
        Me.fndCity.MyReadOnly = False
        Me.fndCity.Name = "fndCity"
        Me.fndCity.Size = New System.Drawing.Size(143, 19)
        Me.fndCity.TabIndex = 6
        Me.fndCity.Value = ""
        '
        'RadLabel5
        '
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(11, 190)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(26, 16)
        Me.RadLabel5.TabIndex = 69
        Me.RadLabel5.Text = "City"
        '
        'fndCusgrp
        '
        Me.fndCusgrp.Location = New System.Drawing.Point(100, 30)
        Me.fndCusgrp.MendatroryField = False
        Me.fndCusgrp.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCusgrp.MyLinkLable1 = Me.lblCusGrp
        Me.fndCusgrp.MyLinkLable2 = Nothing
        Me.fndCusgrp.MyReadOnly = False
        Me.fndCusgrp.Name = "fndCusgrp"
        Me.fndCusgrp.Size = New System.Drawing.Size(143, 19)
        Me.fndCusgrp.TabIndex = 0
        Me.fndCusgrp.Value = ""
        '
        'lblCusGrp
        '
        Me.lblCusGrp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCusGrp.Location = New System.Drawing.Point(11, 32)
        Me.lblCusGrp.Name = "lblCusGrp"
        Me.lblCusGrp.Size = New System.Drawing.Size(68, 16)
        Me.lblCusGrp.TabIndex = 67
        Me.lblCusGrp.Text = "Group Code"
        '
        'chkcredit
        '
        Me.chkcredit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkcredit.Location = New System.Drawing.Point(244, 74)
        Me.chkcredit.Name = "chkcredit"
        '
        '
        '
        Me.chkcredit.RootElement.StretchHorizontally = True
        Me.chkcredit.RootElement.StretchVertically = True
        Me.chkcredit.Size = New System.Drawing.Size(103, 16)
        Me.chkcredit.TabIndex = 2
        Me.chkcredit.Text = "Credit Customer"
        '
        'RadLabel12
        '
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.Location = New System.Drawing.Point(11, 323)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel12.TabIndex = 75
        Me.RadLabel12.Text = "WebSite"
        '
        'RadLabel11
        '
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(11, 297)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(38, 16)
        Me.RadLabel11.TabIndex = 75
        Me.RadLabel11.Text = "E-Mail"
        '
        'txtWeb
        '
        Me.txtWeb.Location = New System.Drawing.Point(99, 319)
        Me.txtWeb.MaxLength = 50
        Me.txtWeb.MendatroryField = False
        Me.txtWeb.MyLinkLable1 = Me.RadLabel12
        Me.txtWeb.MyLinkLable2 = Nothing
        Me.txtWeb.Name = "txtWeb"
        Me.txtWeb.Size = New System.Drawing.Size(599, 20)
        Me.txtWeb.TabIndex = 14
        '
        'txtfax
        '
        Me.txtfax.Location = New System.Drawing.Point(423, 241)
        Me.txtfax.MaxLength = 15
        Me.txtfax.MendatroryField = False
        Me.txtfax.MyLinkLable1 = Nothing
        Me.txtfax.MyLinkLable2 = Nothing
        Me.txtfax.Name = "txtfax"
        Me.txtfax.Size = New System.Drawing.Size(276, 20)
        Me.txtfax.TabIndex = 11
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(99, 293)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.MendatroryField = False
        Me.txtEmail.MyLinkLable1 = Me.RadLabel11
        Me.txtEmail.MyLinkLable2 = Nothing
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(599, 20)
        Me.txtEmail.TabIndex = 13
        '
        'txtPhone2
        '
        Me.txtPhone2.Location = New System.Drawing.Point(99, 267)
        Me.txtPhone2.MaxLength = 15
        Me.txtPhone2.MendatroryField = False
        Me.txtPhone2.MyLinkLable1 = Me.RadLabel8
        Me.txtPhone2.MyLinkLable2 = Nothing
        Me.txtPhone2.Name = "txtPhone2"
        Me.txtPhone2.Size = New System.Drawing.Size(226, 20)
        Me.txtPhone2.TabIndex = 12
        '
        'RadLabel8
        '
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(11, 271)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(45, 16)
        Me.RadLabel8.TabIndex = 76
        Me.RadLabel8.Text = "Phone2"
        '
        'RadLabel10
        '
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.Location = New System.Drawing.Point(365, 245)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(25, 16)
        Me.RadLabel10.TabIndex = 76
        Me.RadLabel10.Text = "Fax"
        '
        'txtCountry
        '
        Me.txtCountry.Location = New System.Drawing.Point(423, 213)
        Me.txtCountry.MaxLength = 25
        Me.txtCountry.MendatroryField = False
        Me.txtCountry.MyLinkLable1 = Nothing
        Me.txtCountry.MyLinkLable2 = Nothing
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.Size = New System.Drawing.Size(276, 20)
        Me.txtCountry.TabIndex = 10
        '
        'txtPhone1
        '
        Me.txtPhone1.Location = New System.Drawing.Point(100, 241)
        Me.txtPhone1.MaxLength = 15
        Me.txtPhone1.MendatroryField = False
        Me.txtPhone1.MyLinkLable1 = Me.RadLabel9
        Me.txtPhone1.MyLinkLable2 = Nothing
        Me.txtPhone1.Name = "txtPhone1"
        Me.txtPhone1.Size = New System.Drawing.Size(225, 20)
        Me.txtPhone1.TabIndex = 11
        '
        'RadLabel9
        '
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(11, 243)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(45, 16)
        Me.RadLabel9.TabIndex = 74
        Me.RadLabel9.Text = "Phone1"
        '
        'RadLabel7
        '
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(364, 215)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(46, 16)
        Me.RadLabel7.TabIndex = 9
        Me.RadLabel7.Text = "Country"
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(254, 184)
        Me.txtCity.MendatroryField = False
        Me.txtCity.MyLinkLable1 = Nothing
        Me.txtCity.MyLinkLable2 = Nothing
        Me.txtCity.Name = "txtCity"
        Me.txtCity.ReadOnly = True
        Me.txtCity.Size = New System.Drawing.Size(197, 20)
        Me.txtCity.TabIndex = 7
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkHold)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(100, 66)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(125, 33)
        Me.RadGroupBox1.TabIndex = 1
        '
        'chkHold
        '
        Me.chkHold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHold.Location = New System.Drawing.Point(13, 8)
        Me.chkHold.Name = "chkHold"
        '
        '
        '
        Me.chkHold.RootElement.StretchHorizontally = True
        Me.chkHold.RootElement.StretchVertically = True
        Me.chkHold.Size = New System.Drawing.Size(59, 16)
        Me.chkHold.TabIndex = 0
        Me.chkHold.Text = "OnHold"
        '
        'RadLabel3
        '
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(11, 74)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(38, 16)
        Me.RadLabel3.TabIndex = 69
        Me.RadLabel3.Text = "Status"
        '
        'txtAdd2
        '
        Me.txtAdd2.Location = New System.Drawing.Point(100, 133)
        Me.txtAdd2.MaxLength = 75
        Me.txtAdd2.MendatroryField = False
        Me.txtAdd2.MyLinkLable1 = Me.RadLabel2
        Me.txtAdd2.MyLinkLable2 = Nothing
        Me.txtAdd2.Name = "txtAdd2"
        Me.txtAdd2.Size = New System.Drawing.Size(599, 20)
        Me.txtAdd2.TabIndex = 4
        '
        'RadLabel2
        '
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(11, 107)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(48, 16)
        Me.RadLabel2.TabIndex = 68
        Me.RadLabel2.Text = "Address"
        '
        'txtAdd1
        '
        Me.txtAdd1.Location = New System.Drawing.Point(100, 107)
        Me.txtAdd1.MaxLength = 75
        Me.txtAdd1.MendatroryField = False
        Me.txtAdd1.MyLinkLable1 = Me.RadLabel2
        Me.txtAdd1.MyLinkLable2 = Nothing
        Me.txtAdd1.Name = "txtAdd1"
        Me.txtAdd1.Size = New System.Drawing.Size(599, 20)
        Me.txtAdd1.TabIndex = 3
        '
        'txtAdd3
        '
        Me.txtAdd3.Location = New System.Drawing.Point(100, 159)
        Me.txtAdd3.MaxLength = 75
        Me.txtAdd3.MendatroryField = False
        Me.txtAdd3.MyLinkLable1 = Me.RadLabel2
        Me.txtAdd3.MyLinkLable2 = Nothing
        Me.txtAdd3.Name = "txtAdd3"
        Me.txtAdd3.Size = New System.Drawing.Size(599, 20)
        Me.txtAdd3.TabIndex = 5
        '
        'txtCusgrp
        '
        Me.txtCusgrp.Location = New System.Drawing.Point(249, 32)
        Me.txtCusgrp.MaxLength = 50
        Me.txtCusgrp.MendatroryField = False
        Me.txtCusgrp.MyLinkLable1 = Nothing
        Me.txtCusgrp.MyLinkLable2 = Nothing
        Me.txtCusgrp.Name = "txtCusgrp"
        Me.txtCusgrp.Size = New System.Drawing.Size(449, 20)
        Me.txtCusgrp.TabIndex = 1
        '
        'pageCus
        '
        Me.pageCus.AccessibleName = "txtParentCstmrNo"
        Me.pageCus.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pageCus.Controls.Add(Me.RadPageViewPage1)
        Me.pageCus.Controls.Add(Me.RadPageViewPage2)
        Me.pageCus.Controls.Add(Me.RadPageViewPage4)
        Me.pageCus.Controls.Add(Me.RadPageViewPage5)
        Me.pageCus.Controls.Add(Me.RadPageViewPage3)
        Me.pageCus.Location = New System.Drawing.Point(3, 96)
        Me.pageCus.Name = "pageCus"
        Me.pageCus.SelectedPage = Me.RadPageViewPage1
        Me.pageCus.Size = New System.Drawing.Size(726, 470)
        Me.pageCus.TabIndex = 6
        CType(Me.pageCus.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.dgvVisi)
        Me.RadPageViewPage3.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(705, 422)
        Me.RadPageViewPage3.Text = "Visi"
        '
        'dgvVisi
        '
        Me.dgvVisi.BackColor = System.Drawing.Color.White
        Me.dgvVisi.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvVisi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvVisi.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvVisi.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dgvVisi.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvVisi.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.dgvVisi.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.dgvVisi.MasterTemplate.AllowAddNewRow = False
        Me.dgvVisi.MasterTemplate.AllowDeleteRow = False
        Me.dgvVisi.MasterTemplate.EnableFiltering = True
        Me.dgvVisi.MasterTemplate.EnableGrouping = False
        SortDescriptor1.PropertyName = "column2"
        Me.dgvVisi.MasterTemplate.SortDescriptors.AddRange(New Telerik.WinControls.Data.SortDescriptor() {SortDescriptor1})
        Me.dgvVisi.Name = "dgvVisi"
        Me.dgvVisi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvVisi.ShowGroupPanel = False
        Me.dgvVisi.Size = New System.Drawing.Size(705, 422)
        Me.dgvVisi.TabIndex = 2
        Me.dgvVisi.Text = "RadGridView1"
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(492, 48)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(80, 16)
        Me.MyLabel1.TabIndex = 4
        Me.MyLabel1.Text = "Customer type"
        '
        'ddlCustType
        '
        Me.ddlCustType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlCustType.Location = New System.Drawing.Point(578, 44)
        Me.ddlCustType.MendatroryField = True
        Me.ddlCustType.MyLinkLable1 = Nothing
        Me.ddlCustType.MyLinkLable2 = Nothing
        Me.ddlCustType.Name = "ddlCustType"
        Me.ddlCustType.Size = New System.Drawing.Size(140, 20)
        Me.ddlCustType.TabIndex = 2
        '
        'chkDistributer
        '
        Me.chkDistributer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDistributer.Location = New System.Drawing.Point(392, 74)
        Me.chkDistributer.Name = "chkDistributer"
        Me.chkDistributer.Size = New System.Drawing.Size(72, 16)
        Me.chkDistributer.TabIndex = 4
        Me.chkDistributer.Text = "Distributor"
        '
        'fndcust
        '
        Me.fndcust.Location = New System.Drawing.Point(575, 71)
        Me.fndcust.MendatroryField = False
        Me.fndcust.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcust.MyLinkLable1 = Me.lblCusGrp
        Me.fndcust.MyLinkLable2 = Nothing
        Me.fndcust.MyReadOnly = False
        Me.fndcust.Name = "fndcust"
        Me.fndcust.Size = New System.Drawing.Size(143, 19)
        Me.fndcust.TabIndex = 5
        Me.fndcust.Value = ""
        '
        'lblcust
        '
        Me.lblcust.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcust.Location = New System.Drawing.Point(492, 71)
        Me.lblcust.Name = "lblcust"
        Me.lblcust.Size = New System.Drawing.Size(58, 16)
        Me.lblcust.TabIndex = 8
        Me.lblcust.Text = "Customer "
        '
        'txtCustomerName
        '
        Me.txtCustomerName.Location = New System.Drawing.Point(115, 72)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(218, 20)
        Me.txtCustomerName.TabIndex = 3
        '
        'FrmCustomerInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(735, 592)
        Me.Controls.Add(Me.lblcust)
        Me.Controls.Add(Me.txtCustomerName)
        Me.Controls.Add(Me.fndcust)
        Me.Controls.Add(Me.ddlCustType)
        Me.Controls.Add(Me.chkDistributer)
        Me.Controls.Add(Me.MyLabel1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.RadLabel4)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.RadLabel1)
        Me.Controls.Add(Me.chkInterBranch)
        Me.Controls.Add(Me.RadSeparator2)
        Me.Controls.Add(Me.pageCus)
        Me.Controls.Add(Me.fndCustomer)
        Me.Name = "FrmCustomerInfo"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Customer Information"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInterBranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSeparator2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        Me.RadPageViewPage5.PerformLayout()
        CType(Me.RadLabel40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel36, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddInfo3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddInfo2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddInfo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadPageViewPage4.PerformLayout()
        CType(Me.RadLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.drpformtype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldivision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdivision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtpan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcollectorate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcollect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblrange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblecc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtrange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtecc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcst, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcst, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTax.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTxGrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel30, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCredit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLstNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTinNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStaxNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.RadLabel41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtContPhone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtContactFax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtContactWeb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtContactEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtContactName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.txtStateName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCusGrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkcredit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWeb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPhone2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPhone1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkHold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCusgrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pageCus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageCus.ResumeLayout(False)
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.dgvVisi.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvVisi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlCustType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDistributer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcust, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents chkInterBranch As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadSeparator2 As Telerik.WinControls.UI.RadSeparator
    Friend WithEvents fndCustomer As common.UserControls.txtNavigator
    Friend WithEvents RadPageViewPage5 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadLabel40 As common.Controls.MyLabel
    Friend WithEvents RadLabel36 As common.Controls.MyLabel
    Friend WithEvents txtAddInfo3 As common.Controls.MyTextBox
    Friend WithEvents RadLabel37 As common.Controls.MyLabel
    Friend WithEvents txtRemarks2 As common.Controls.MyTextBox
    Friend WithEvents RadLabel38 As common.Controls.MyLabel
    Friend WithEvents txtAddInfo2 As common.Controls.MyTextBox
    Friend WithEvents txtAddInfo1 As common.Controls.MyTextBox
    Friend WithEvents RadLabel39 As common.Controls.MyLabel
    Friend WithEvents txtRemarks1 As common.Controls.MyTextBox
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents fndPayCode As common.UserControls.txtFinder
    Friend WithEvents RadLabel26 As common.Controls.MyLabel
    Friend WithEvents fndAccntSet As common.UserControls.txtFinder
    Friend WithEvents RadLabel25 As common.Controls.MyLabel
    Friend WithEvents fndTrmsCode As common.UserControls.txtFinder
    Friend WithEvents RadLabel23 As common.Controls.MyLabel
    Friend WithEvents drpformtype As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents lbldivision As common.Controls.MyLabel
    Friend WithEvents txtdivision As common.Controls.MyTextBox
    Friend WithEvents lblpan As common.Controls.MyLabel
    Friend WithEvents txtpan As common.Controls.MyTextBox
    Friend WithEvents lblcollectorate As common.Controls.MyLabel
    Friend WithEvents txtcollect As common.Controls.MyTextBox
    Friend WithEvents lblrange As common.Controls.MyLabel
    Friend WithEvents lblecc As common.Controls.MyLabel
    Friend WithEvents txtrange As common.Controls.MyTextBox
    Friend WithEvents txtecc As common.Controls.MyTextBox
    Friend WithEvents lblcst As common.Controls.MyLabel
    Friend WithEvents txtcst As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents fndTxGrp As common.UserControls.txtFinder
    Friend WithEvents RadLabel35 As common.Controls.MyLabel
    Friend WithEvents grdTax As common.UserControls.MyRadGridView
    Friend WithEvents txtTxGrp As common.Controls.MyTextBox
    Friend WithEvents RadLabel30 As common.Controls.MyLabel
    Friend WithEvents RadLabel29 As common.Controls.MyLabel
    Friend WithEvents txtCredit As common.Controls.MyTextBox
    Friend WithEvents txtLstNo As common.Controls.MyTextBox
    Friend WithEvents RadLabel31 As common.Controls.MyLabel
    Friend WithEvents RadLabel28 As common.Controls.MyLabel
    Friend WithEvents RadLabel27 As common.Controls.MyLabel
    Friend WithEvents txtTinNo As common.Controls.MyTextBox
    Friend WithEvents txtStaxNo As common.Controls.MyTextBox
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadLabel41 As common.Controls.MyLabel
    Friend WithEvents RadLabel16 As common.Controls.MyLabel
    Friend WithEvents txtContPhone As common.Controls.MyTextBox
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents txtContactFax As common.Controls.MyTextBox
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents txtContactWeb As common.Controls.MyTextBox
    Friend WithEvents txtContactEmail As common.Controls.MyTextBox
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents txtContactName As common.Controls.MyTextBox
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtStateName As common.Controls.MyTextBox
    Friend WithEvents fndstate As common.UserControls.txtFinder
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents fndCity As common.UserControls.txtFinder
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents fndCusgrp As common.UserControls.txtFinder
    Friend WithEvents lblCusGrp As common.Controls.MyLabel
    Friend WithEvents chkcredit As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents txtWeb As common.Controls.MyTextBox
    Friend WithEvents txtfax As common.Controls.MyTextBox
    Friend WithEvents txtEmail As common.Controls.MyTextBox
    Friend WithEvents txtPhone2 As common.Controls.MyTextBox
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents txtCountry As common.Controls.MyTextBox
    Friend WithEvents txtPhone1 As common.Controls.MyTextBox
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents txtCity As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkHold As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents txtAdd2 As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents txtAdd1 As common.Controls.MyTextBox
    Friend WithEvents txtAdd3 As common.Controls.MyTextBox
    Friend WithEvents txtCusgrp As common.Controls.MyTextBox
    Friend WithEvents pageCus As Telerik.WinControls.UI.RadPageView
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents ddlCustType As common.Controls.MyComboBox
    Friend WithEvents chkDistributer As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents fndcust As common.UserControls.txtFinder
    Friend WithEvents lblcust As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents dgvVisi As common.UserControls.MyRadGridView
    Friend WithEvents txtCustomerName As Telerik.WinControls.UI.RadTextBox
End Class

