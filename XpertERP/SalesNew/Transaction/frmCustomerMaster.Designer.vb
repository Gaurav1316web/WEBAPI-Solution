<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCustomerMaster
    Inherits Telerik.WinControls.UI.RadForm

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
        Me.txtCustPINCode = New common.MyNumBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.txtShipPINCode = New common.MyNumBox
        Me.txtShipEmailID = New common.Controls.MyTextBox
        Me.MyLabel13 = New common.Controls.MyLabel
        Me.txtShipContactPerson = New common.Controls.MyTextBox
        Me.MyLabel15 = New common.Controls.MyLabel
        Me.txtShipCountry = New common.Controls.MyTextBox
        Me.MyLabel7 = New common.Controls.MyLabel
        Me.txtShipState = New common.UserControls.txtFinder
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.lblShipState = New common.Controls.MyLabel
        Me.txtShipCity = New common.UserControls.txtFinder
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.lblShipCity = New common.Controls.MyLabel
        Me.txtShipAdd2 = New common.Controls.MyTextBox
        Me.txtShipAdd1 = New common.Controls.MyTextBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtShipContactNo = New common.Controls.MyTextBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtShipDescription = New common.Controls.MyTextBox
        Me.MyLabel16 = New common.Controls.MyLabel
        Me.txtShipCode = New common.UserControls.txtNavigator
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.chkSameAddressForShipping = New Telerik.WinControls.UI.RadCheckBox
        Me.MyLabel14 = New common.Controls.MyLabel
        Me.cboCustOccation = New common.Controls.MyComboBox
        Me.RadLabel29 = New common.Controls.MyLabel
        Me.cboCustGender = New common.Controls.MyComboBox
        Me.txtCustEmailID = New common.Controls.MyTextBox
        Me.txtCustAnniversaryDate = New common.Controls.MyDateTimePicker
        Me.MyLabel12 = New common.Controls.MyLabel
        Me.txtCustSpouseDOB = New common.Controls.MyDateTimePicker
        Me.MyLabel11 = New common.Controls.MyLabel
        Me.txtCustDOB = New common.Controls.MyDateTimePicker
        Me.MyLabel10 = New common.Controls.MyLabel
        Me.MyLabel8 = New common.Controls.MyLabel
        Me.txtCustContactPerson = New common.Controls.MyTextBox
        Me.txtCustCountry = New common.Controls.MyTextBox
        Me.txtCustState = New common.UserControls.txtFinder
        Me.lblCustState = New common.Controls.MyLabel
        Me.txtCustCity = New common.UserControls.txtFinder
        Me.lblCustCity = New common.Controls.MyLabel
        Me.txtCustAdd2 = New common.Controls.MyTextBox
        Me.txtCustAdd1 = New common.Controls.MyTextBox
        Me.txtCustContactNo = New common.Controls.MyTextBox
        Me.txtCustDescription = New common.Controls.MyTextBox
        Me.txtCustCode = New common.UserControls.txtNavigator
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtCustPINCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtShipPINCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtShipEmailID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtShipContactPerson, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtShipCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShipState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShipCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtShipAdd2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtShipAdd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtShipContactNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtShipDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSameAddressForShipping, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboCustOccation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboCustGender, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustEmailID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustAnniversaryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustSpouseDOB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustDOB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustContactPerson, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustAdd2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustAdd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustContactNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustPINCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel14)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboCustOccation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel29)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboCustGender)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel13)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustEmailID)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustAnniversaryDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel12)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustSpouseDOB)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel11)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustDOB)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel10)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel15)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustContactPerson)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustCountry)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustState)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCustState)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustCity)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCustCity)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustAdd2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustAdd1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustContactNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel16)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(745, 384)
        Me.SplitContainer1.SplitterDistance = 352
        Me.SplitContainer1.TabIndex = 0
        '
        'txtCustPINCode
        '
        Me.txtCustPINCode.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCustPINCode.DecimalPlaces = 2
        Me.txtCustPINCode.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustPINCode.Location = New System.Drawing.Point(122, 197)
        Me.txtCustPINCode.MendatroryField = True
        Me.txtCustPINCode.MyLinkLable1 = Me.MyLabel3
        Me.txtCustPINCode.MyLinkLable2 = Nothing
        Me.txtCustPINCode.Name = "txtCustPINCode"
        Me.txtCustPINCode.Size = New System.Drawing.Size(298, 23)
        Me.txtCustPINCode.TabIndex = 8
        Me.txtCustPINCode.Text = "0"
        Me.txtCustPINCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCustPINCode.Value = 0
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(5, 109)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(69, 19)
        Me.MyLabel3.TabIndex = 54
        Me.MyLabel3.Text = "Address 2"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.txtShipPINCode)
        Me.RadGroupBox1.Controls.Add(Me.txtShipEmailID)
        Me.RadGroupBox1.Controls.Add(Me.txtShipContactPerson)
        Me.RadGroupBox1.Controls.Add(Me.txtShipCountry)
        Me.RadGroupBox1.Controls.Add(Me.txtShipState)
        Me.RadGroupBox1.Controls.Add(Me.lblShipState)
        Me.RadGroupBox1.Controls.Add(Me.txtShipCity)
        Me.RadGroupBox1.Controls.Add(Me.lblShipCity)
        Me.RadGroupBox1.Controls.Add(Me.txtShipAdd2)
        Me.RadGroupBox1.Controls.Add(Me.txtShipAdd1)
        Me.RadGroupBox1.Controls.Add(Me.txtShipContactNo)
        Me.RadGroupBox1.Controls.Add(Me.txtShipDescription)
        Me.RadGroupBox1.Controls.Add(Me.txtShipCode)
        Me.RadGroupBox1.Controls.Add(Me.RadButton1)
        Me.RadGroupBox1.Controls.Add(Me.chkSameAddressForShipping)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Shipping Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(427, -3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(314, 277)
        Me.RadGroupBox1.TabIndex = 16
        Me.RadGroupBox1.Text = "Shipping Details"
        '
        'txtShipPINCode
        '
        Me.txtShipPINCode.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtShipPINCode.DecimalPlaces = 2
        Me.txtShipPINCode.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipPINCode.Location = New System.Drawing.Point(6, 201)
        Me.txtShipPINCode.MendatroryField = True
        Me.txtShipPINCode.MyLinkLable1 = Me.MyLabel3
        Me.txtShipPINCode.MyLinkLable2 = Nothing
        Me.txtShipPINCode.Name = "txtShipPINCode"
        Me.txtShipPINCode.Size = New System.Drawing.Size(298, 23)
        Me.txtShipPINCode.TabIndex = 9
        Me.txtShipPINCode.Text = "0"
        Me.txtShipPINCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtShipPINCode.Value = 0
        '
        'txtShipEmailID
        '
        Me.txtShipEmailID.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipEmailID.Location = New System.Drawing.Point(6, 225)
        Me.txtShipEmailID.MaxLength = 200
        Me.txtShipEmailID.MendatroryField = False
        Me.txtShipEmailID.MyLinkLable1 = Me.MyLabel13
        Me.txtShipEmailID.MyLinkLable2 = Nothing
        Me.txtShipEmailID.Name = "txtShipEmailID"
        Me.txtShipEmailID.Size = New System.Drawing.Size(298, 21)
        Me.txtShipEmailID.TabIndex = 10
        '
        'MyLabel13
        '
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(5, 223)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(41, 19)
        Me.MyLabel13.TabIndex = 71
        Me.MyLabel13.Text = "Email"
        '
        'txtShipContactPerson
        '
        Me.txtShipContactPerson.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipContactPerson.Location = New System.Drawing.Point(6, 249)
        Me.txtShipContactPerson.MaxLength = 200
        Me.txtShipContactPerson.MendatroryField = False
        Me.txtShipContactPerson.MyLinkLable1 = Me.MyLabel15
        Me.txtShipContactPerson.MyLinkLable2 = Nothing
        Me.txtShipContactPerson.Name = "txtShipContactPerson"
        Me.txtShipContactPerson.Size = New System.Drawing.Size(298, 21)
        Me.txtShipContactPerson.TabIndex = 11
        '
        'MyLabel15
        '
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(5, 247)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(102, 19)
        Me.MyLabel15.TabIndex = 63
        Me.MyLabel15.Text = "Contact Person"
        '
        'txtShipCountry
        '
        Me.txtShipCountry.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipCountry.Location = New System.Drawing.Point(6, 179)
        Me.txtShipCountry.MaxLength = 200
        Me.txtShipCountry.MendatroryField = False
        Me.txtShipCountry.MyLinkLable1 = Me.MyLabel7
        Me.txtShipCountry.MyLinkLable2 = Nothing
        Me.txtShipCountry.Name = "txtShipCountry"
        Me.txtShipCountry.Size = New System.Drawing.Size(298, 21)
        Me.txtShipCountry.TabIndex = 8
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(5, 177)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(55, 19)
        Me.MyLabel7.TabIndex = 61
        Me.MyLabel7.Text = "Country"
        '
        'txtShipState
        '
        Me.txtShipState.Location = New System.Drawing.Point(6, 155)
        Me.txtShipState.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtShipState.MendatroryField = False
        Me.txtShipState.MyFont = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipState.MyLinkLable1 = Me.MyLabel5
        Me.txtShipState.MyLinkLable2 = Me.lblShipState
        Me.txtShipState.MyReadOnly = False
        Me.txtShipState.Name = "txtShipState"
        Me.txtShipState.Size = New System.Drawing.Size(116, 21)
        Me.txtShipState.TabIndex = 7
        Me.txtShipState.Value = ""
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(5, 153)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(39, 19)
        Me.MyLabel5.TabIndex = 59
        Me.MyLabel5.Text = "State"
        '
        'lblShipState
        '
        Me.lblShipState.AutoSize = False
        Me.lblShipState.BorderVisible = True
        Me.lblShipState.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShipState.Location = New System.Drawing.Point(128, 154)
        Me.lblShipState.Name = "lblShipState"
        Me.lblShipState.Size = New System.Drawing.Size(175, 22)
        Me.lblShipState.TabIndex = 84
        Me.lblShipState.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblShipState.TextWrap = False
        '
        'txtShipCity
        '
        Me.txtShipCity.Location = New System.Drawing.Point(6, 133)
        Me.txtShipCity.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtShipCity.MendatroryField = False
        Me.txtShipCity.MyFont = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipCity.MyLinkLable1 = Me.MyLabel4
        Me.txtShipCity.MyLinkLable2 = Me.lblShipCity
        Me.txtShipCity.MyReadOnly = False
        Me.txtShipCity.Name = "txtShipCity"
        Me.txtShipCity.Size = New System.Drawing.Size(116, 21)
        Me.txtShipCity.TabIndex = 6
        Me.txtShipCity.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(5, 131)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(31, 19)
        Me.MyLabel4.TabIndex = 56
        Me.MyLabel4.Text = "City"
        '
        'lblShipCity
        '
        Me.lblShipCity.AutoSize = False
        Me.lblShipCity.BorderVisible = True
        Me.lblShipCity.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShipCity.Location = New System.Drawing.Point(128, 132)
        Me.lblShipCity.Name = "lblShipCity"
        Me.lblShipCity.Size = New System.Drawing.Size(175, 22)
        Me.lblShipCity.TabIndex = 76
        Me.lblShipCity.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblShipCity.TextWrap = False
        '
        'txtShipAdd2
        '
        Me.txtShipAdd2.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipAdd2.Location = New System.Drawing.Point(6, 111)
        Me.txtShipAdd2.MaxLength = 200
        Me.txtShipAdd2.MendatroryField = False
        Me.txtShipAdd2.MyLinkLable1 = Me.MyLabel3
        Me.txtShipAdd2.MyLinkLable2 = Nothing
        Me.txtShipAdd2.Name = "txtShipAdd2"
        Me.txtShipAdd2.Size = New System.Drawing.Size(298, 21)
        Me.txtShipAdd2.TabIndex = 5
        '
        'txtShipAdd1
        '
        Me.txtShipAdd1.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipAdd1.Location = New System.Drawing.Point(6, 89)
        Me.txtShipAdd1.MaxLength = 200
        Me.txtShipAdd1.MendatroryField = False
        Me.txtShipAdd1.MyLinkLable1 = Me.MyLabel2
        Me.txtShipAdd1.MyLinkLable2 = Nothing
        Me.txtShipAdd1.Name = "txtShipAdd1"
        Me.txtShipAdd1.Size = New System.Drawing.Size(298, 21)
        Me.txtShipAdd1.TabIndex = 4
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(5, 87)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(69, 19)
        Me.MyLabel2.TabIndex = 54
        Me.MyLabel2.Text = "Address 1"
        '
        'txtShipContactNo
        '
        Me.txtShipContactNo.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipContactNo.Location = New System.Drawing.Point(6, 67)
        Me.txtShipContactNo.MaxLength = 200
        Me.txtShipContactNo.MendatroryField = False
        Me.txtShipContactNo.MyLinkLable1 = Me.MyLabel1
        Me.txtShipContactNo.MyLinkLable2 = Nothing
        Me.txtShipContactNo.Name = "txtShipContactNo"
        Me.txtShipContactNo.Size = New System.Drawing.Size(298, 21)
        Me.txtShipContactNo.TabIndex = 3
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(5, 65)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(76, 19)
        Me.MyLabel1.TabIndex = 52
        Me.MyLabel1.Text = "Contact No"
        '
        'txtShipDescription
        '
        Me.txtShipDescription.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipDescription.Location = New System.Drawing.Point(6, 45)
        Me.txtShipDescription.MaxLength = 200
        Me.txtShipDescription.MendatroryField = False
        Me.txtShipDescription.MyLinkLable1 = Me.MyLabel16
        Me.txtShipDescription.MyLinkLable2 = Nothing
        Me.txtShipDescription.Name = "txtShipDescription"
        Me.txtShipDescription.Size = New System.Drawing.Size(298, 21)
        Me.txtShipDescription.TabIndex = 2
        '
        'MyLabel16
        '
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(5, 43)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(76, 19)
        Me.MyLabel16.TabIndex = 50
        Me.MyLabel16.Text = "Description"
        '
        'txtShipCode
        '
        Me.txtShipCode.Location = New System.Drawing.Point(6, 21)
        Me.txtShipCode.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtShipCode.MendatroryField = False
        Me.txtShipCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtShipCode.MyFont = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtShipCode.MyLinkLable1 = Me.RadLabel1
        Me.txtShipCode.MyLinkLable2 = Nothing
        Me.txtShipCode.MyMaxLength = 32767
        Me.txtShipCode.MyReadOnly = True
        Me.txtShipCode.Name = "txtShipCode"
        Me.txtShipCode.Size = New System.Drawing.Size(278, 23)
        Me.txtShipCode.TabIndex = 1
        Me.txtShipCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.RadLabel1.Location = New System.Drawing.Point(5, 20)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(40, 19)
        Me.RadLabel1.TabIndex = 48
        Me.RadLabel1.Text = "Code"
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Image = Global.ERP.My.Resources.Resources._new
        Me.RadButton1.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton1.Location = New System.Drawing.Point(283, 21)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(21, 23)
        Me.RadButton1.TabIndex = 78
        '
        'chkSameAddressForShipping
        '
        Me.chkSameAddressForShipping.Location = New System.Drawing.Point(107, 0)
        Me.chkSameAddressForShipping.Name = "chkSameAddressForShipping"
        Me.chkSameAddressForShipping.Size = New System.Drawing.Size(156, 18)
        Me.chkSameAddressForShipping.TabIndex = 0
        Me.chkSameAddressForShipping.Text = "Same Address for Shipping"
        '
        'MyLabel14
        '
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(5, 320)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(71, 19)
        Me.MyLabel14.TabIndex = 75
        Me.MyLabel14.Text = "Occassion"
        '
        'cboCustOccation
        '
        Me.cboCustOccation.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboCustOccation.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCustOccation.Location = New System.Drawing.Point(123, 318)
        Me.cboCustOccation.MendatroryField = True
        Me.cboCustOccation.MyLinkLable1 = Me.MyLabel14
        Me.cboCustOccation.MyLinkLable2 = Nothing
        Me.cboCustOccation.Name = "cboCustOccation"
        Me.cboCustOccation.Size = New System.Drawing.Size(298, 23)
        Me.cboCustOccation.TabIndex = 15
        '
        'RadLabel29
        '
        Me.RadLabel29.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel29.Location = New System.Drawing.Point(228, 295)
        Me.RadLabel29.Name = "RadLabel29"
        Me.RadLabel29.Size = New System.Drawing.Size(53, 19)
        Me.RadLabel29.TabIndex = 73
        Me.RadLabel29.Text = "Gender"
        '
        'cboCustGender
        '
        Me.cboCustGender.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboCustGender.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCustGender.Location = New System.Drawing.Point(320, 293)
        Me.cboCustGender.MendatroryField = True
        Me.cboCustGender.MyLinkLable1 = Me.RadLabel29
        Me.cboCustGender.MyLinkLable2 = Nothing
        Me.cboCustGender.Name = "cboCustGender"
        Me.cboCustGender.Size = New System.Drawing.Size(100, 23)
        Me.cboCustGender.TabIndex = 14
        '
        'txtCustEmailID
        '
        Me.txtCustEmailID.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustEmailID.Location = New System.Drawing.Point(123, 222)
        Me.txtCustEmailID.MaxLength = 200
        Me.txtCustEmailID.MendatroryField = False
        Me.txtCustEmailID.MyLinkLable1 = Me.MyLabel13
        Me.txtCustEmailID.MyLinkLable2 = Nothing
        Me.txtCustEmailID.Name = "txtCustEmailID"
        Me.txtCustEmailID.Size = New System.Drawing.Size(298, 21)
        Me.txtCustEmailID.TabIndex = 9
        '
        'txtCustAnniversaryDate
        '
        Me.txtCustAnniversaryDate.CustomFormat = "dd/MM/yyyy"
        Me.txtCustAnniversaryDate.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustAnniversaryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtCustAnniversaryDate.Location = New System.Drawing.Point(123, 294)
        Me.txtCustAnniversaryDate.MendatroryField = False
        Me.txtCustAnniversaryDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtCustAnniversaryDate.MyLinkLable1 = Me.MyLabel12
        Me.txtCustAnniversaryDate.MyLinkLable2 = Nothing
        Me.txtCustAnniversaryDate.Name = "txtCustAnniversaryDate"
        Me.txtCustAnniversaryDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtCustAnniversaryDate.ShowCheckBox = True
        Me.txtCustAnniversaryDate.Size = New System.Drawing.Size(100, 21)
        Me.txtCustAnniversaryDate.TabIndex = 13
        Me.txtCustAnniversaryDate.TabStop = False
        Me.txtCustAnniversaryDate.Text = "13/06/2011"
        Me.txtCustAnniversaryDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel12
        '
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(5, 295)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(112, 19)
        Me.MyLabel12.TabIndex = 69
        Me.MyLabel12.Text = "Anniversary Date"
        '
        'txtCustSpouseDOB
        '
        Me.txtCustSpouseDOB.CustomFormat = "dd/MM/yyyy"
        Me.txtCustSpouseDOB.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustSpouseDOB.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtCustSpouseDOB.Location = New System.Drawing.Point(321, 270)
        Me.txtCustSpouseDOB.MendatroryField = False
        Me.txtCustSpouseDOB.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtCustSpouseDOB.MyLinkLable1 = Me.MyLabel11
        Me.txtCustSpouseDOB.MyLinkLable2 = Nothing
        Me.txtCustSpouseDOB.Name = "txtCustSpouseDOB"
        Me.txtCustSpouseDOB.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtCustSpouseDOB.ShowCheckBox = True
        Me.txtCustSpouseDOB.Size = New System.Drawing.Size(100, 21)
        Me.txtCustSpouseDOB.TabIndex = 12
        Me.txtCustSpouseDOB.TabStop = False
        Me.txtCustSpouseDOB.Text = "13/06/2011"
        Me.txtCustSpouseDOB.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel11
        '
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(228, 271)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(87, 19)
        Me.MyLabel11.TabIndex = 67
        Me.MyLabel11.Text = "Spouse DOB"
        '
        'txtCustDOB
        '
        Me.txtCustDOB.CustomFormat = "dd/MM/yyyy"
        Me.txtCustDOB.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustDOB.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtCustDOB.Location = New System.Drawing.Point(123, 270)
        Me.txtCustDOB.MendatroryField = False
        Me.txtCustDOB.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtCustDOB.MyLinkLable1 = Me.MyLabel10
        Me.txtCustDOB.MyLinkLable2 = Nothing
        Me.txtCustDOB.Name = "txtCustDOB"
        Me.txtCustDOB.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtCustDOB.ShowCheckBox = True
        Me.txtCustDOB.Size = New System.Drawing.Size(100, 21)
        Me.txtCustDOB.TabIndex = 11
        Me.txtCustDOB.TabStop = False
        Me.txtCustDOB.Text = "13/06/2011"
        Me.txtCustDOB.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel10
        '
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(5, 273)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(37, 19)
        Me.MyLabel10.TabIndex = 65
        Me.MyLabel10.Text = "DOB"
        '
        'MyLabel8
        '
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(5, 199)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(66, 19)
        Me.MyLabel8.TabIndex = 63
        Me.MyLabel8.Text = "PIN Code"
        '
        'txtCustContactPerson
        '
        Me.txtCustContactPerson.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustContactPerson.Location = New System.Drawing.Point(123, 246)
        Me.txtCustContactPerson.MaxLength = 200
        Me.txtCustContactPerson.MendatroryField = False
        Me.txtCustContactPerson.MyLinkLable1 = Me.MyLabel15
        Me.txtCustContactPerson.MyLinkLable2 = Nothing
        Me.txtCustContactPerson.Name = "txtCustContactPerson"
        Me.txtCustContactPerson.Size = New System.Drawing.Size(298, 21)
        Me.txtCustContactPerson.TabIndex = 10
        '
        'txtCustCountry
        '
        Me.txtCustCountry.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustCountry.Location = New System.Drawing.Point(123, 176)
        Me.txtCustCountry.MaxLength = 200
        Me.txtCustCountry.MendatroryField = False
        Me.txtCustCountry.MyLinkLable1 = Me.MyLabel7
        Me.txtCustCountry.MyLinkLable2 = Nothing
        Me.txtCustCountry.Name = "txtCustCountry"
        Me.txtCustCountry.Size = New System.Drawing.Size(298, 21)
        Me.txtCustCountry.TabIndex = 7
        '
        'txtCustState
        '
        Me.txtCustState.Location = New System.Drawing.Point(123, 152)
        Me.txtCustState.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtCustState.MendatroryField = False
        Me.txtCustState.MyFont = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustState.MyLinkLable1 = Me.MyLabel5
        Me.txtCustState.MyLinkLable2 = Me.lblCustState
        Me.txtCustState.MyReadOnly = False
        Me.txtCustState.Name = "txtCustState"
        Me.txtCustState.Size = New System.Drawing.Size(116, 21)
        Me.txtCustState.TabIndex = 6
        Me.txtCustState.Value = ""
        '
        'lblCustState
        '
        Me.lblCustState.AutoSize = False
        Me.lblCustState.BorderVisible = True
        Me.lblCustState.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustState.Location = New System.Drawing.Point(245, 151)
        Me.lblCustState.Name = "lblCustState"
        Me.lblCustState.Size = New System.Drawing.Size(175, 22)
        Me.lblCustState.TabIndex = 58
        Me.lblCustState.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustState.TextWrap = False
        '
        'txtCustCity
        '
        Me.txtCustCity.Location = New System.Drawing.Point(123, 130)
        Me.txtCustCity.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtCustCity.MendatroryField = False
        Me.txtCustCity.MyFont = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustCity.MyLinkLable1 = Me.MyLabel4
        Me.txtCustCity.MyLinkLable2 = Me.lblCustCity
        Me.txtCustCity.MyReadOnly = False
        Me.txtCustCity.Name = "txtCustCity"
        Me.txtCustCity.Size = New System.Drawing.Size(116, 21)
        Me.txtCustCity.TabIndex = 5
        Me.txtCustCity.Value = ""
        '
        'lblCustCity
        '
        Me.lblCustCity.AutoSize = False
        Me.lblCustCity.BorderVisible = True
        Me.lblCustCity.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustCity.Location = New System.Drawing.Point(245, 129)
        Me.lblCustCity.Name = "lblCustCity"
        Me.lblCustCity.Size = New System.Drawing.Size(175, 22)
        Me.lblCustCity.TabIndex = 31
        Me.lblCustCity.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustCity.TextWrap = False
        '
        'txtCustAdd2
        '
        Me.txtCustAdd2.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustAdd2.Location = New System.Drawing.Point(123, 108)
        Me.txtCustAdd2.MaxLength = 200
        Me.txtCustAdd2.MendatroryField = False
        Me.txtCustAdd2.MyLinkLable1 = Me.MyLabel3
        Me.txtCustAdd2.MyLinkLable2 = Nothing
        Me.txtCustAdd2.Name = "txtCustAdd2"
        Me.txtCustAdd2.Size = New System.Drawing.Size(298, 21)
        Me.txtCustAdd2.TabIndex = 4
        '
        'txtCustAdd1
        '
        Me.txtCustAdd1.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustAdd1.Location = New System.Drawing.Point(123, 86)
        Me.txtCustAdd1.MaxLength = 200
        Me.txtCustAdd1.MendatroryField = False
        Me.txtCustAdd1.MyLinkLable1 = Me.MyLabel2
        Me.txtCustAdd1.MyLinkLable2 = Nothing
        Me.txtCustAdd1.Name = "txtCustAdd1"
        Me.txtCustAdd1.Size = New System.Drawing.Size(298, 21)
        Me.txtCustAdd1.TabIndex = 3
        '
        'txtCustContactNo
        '
        Me.txtCustContactNo.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustContactNo.Location = New System.Drawing.Point(123, 64)
        Me.txtCustContactNo.MaxLength = 200
        Me.txtCustContactNo.MendatroryField = False
        Me.txtCustContactNo.MyLinkLable1 = Me.MyLabel1
        Me.txtCustContactNo.MyLinkLable2 = Nothing
        Me.txtCustContactNo.Name = "txtCustContactNo"
        Me.txtCustContactNo.Size = New System.Drawing.Size(298, 21)
        Me.txtCustContactNo.TabIndex = 2
        '
        'txtCustDescription
        '
        Me.txtCustDescription.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustDescription.Location = New System.Drawing.Point(123, 42)
        Me.txtCustDescription.MaxLength = 200
        Me.txtCustDescription.MendatroryField = False
        Me.txtCustDescription.MyLinkLable1 = Me.MyLabel16
        Me.txtCustDescription.MyLinkLable2 = Nothing
        Me.txtCustDescription.Name = "txtCustDescription"
        Me.txtCustDescription.Size = New System.Drawing.Size(298, 21)
        Me.txtCustDescription.TabIndex = 1
        '
        'txtCustCode
        '
        Me.txtCustCode.Location = New System.Drawing.Point(123, 18)
        Me.txtCustCode.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtCustCode.MendatroryField = False
        Me.txtCustCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCustCode.MyFont = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtCustCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCustCode.MyLinkLable2 = Nothing
        Me.txtCustCode.MyMaxLength = 32767
        Me.txtCustCode.MyReadOnly = False
        Me.txtCustCode.Name = "txtCustCode"
        Me.txtCustCode.Size = New System.Drawing.Size(278, 23)
        Me.txtCustCode.TabIndex = 0
        Me.txtCustCode.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(400, 18)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(21, 23)
        Me.btnAddNew.TabIndex = 47
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnClose.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(673, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        '
        'FrmCustomerMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(745, 384)
        Me.ControlBox = False
        Me.Controls.Add(Me.SplitContainer1)
        Me.MaximumSize = New System.Drawing.Size(753, 414)
        Me.MinimumSize = New System.Drawing.Size(753, 414)
        Me.Name = "FrmCustomerMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.RootElement.MaxSize = New System.Drawing.Size(753, 414)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Customer Master"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtCustPINCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtShipPINCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtShipEmailID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtShipContactPerson, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtShipCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShipState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShipCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtShipAdd2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtShipAdd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtShipContactNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtShipDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSameAddressForShipping, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboCustOccation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboCustGender, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustEmailID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustAnniversaryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustSpouseDOB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustDOB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustContactPerson, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustAdd2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustAdd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustContactNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtCustCode As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtCustDescription As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtCustAdd2 As common.Controls.MyTextBox
    Friend WithEvents txtCustAdd1 As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtCustContactNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtCustCountry As common.Controls.MyTextBox
    Friend WithEvents txtCustState As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblCustState As common.Controls.MyLabel
    Friend WithEvents txtCustCity As common.UserControls.txtFinder
    Friend WithEvents lblCustCity As common.Controls.MyLabel
    Friend WithEvents txtCustAnniversaryDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtCustSpouseDOB As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtCustDOB As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents RadLabel29 As common.Controls.MyLabel
    Friend WithEvents cboCustGender As common.Controls.MyComboBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents cboCustOccation As common.Controls.MyComboBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents txtCustContactPerson As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtShipEmailID As common.Controls.MyTextBox
    Friend WithEvents txtShipContactPerson As common.Controls.MyTextBox
    Friend WithEvents txtShipCountry As common.Controls.MyTextBox
    Friend WithEvents txtShipState As common.UserControls.txtFinder
    Friend WithEvents lblShipState As common.Controls.MyLabel
    Friend WithEvents txtShipCity As common.UserControls.txtFinder
    Friend WithEvents lblShipCity As common.Controls.MyLabel
    Friend WithEvents txtShipAdd2 As common.Controls.MyTextBox
    Friend WithEvents txtShipAdd1 As common.Controls.MyTextBox
    Friend WithEvents txtShipContactNo As common.Controls.MyTextBox
    Friend WithEvents txtShipDescription As common.Controls.MyTextBox
    Friend WithEvents txtShipCode As common.UserControls.txtNavigator
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkSameAddressForShipping As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtShipPINCode As common.MyNumBox
    Friend WithEvents txtCustPINCode As common.MyNumBox
    Friend WithEvents txtCustEmailID As common.Controls.MyTextBox
End Class

