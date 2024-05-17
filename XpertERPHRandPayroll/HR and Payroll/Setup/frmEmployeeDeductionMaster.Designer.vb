Imports common
Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEmployeeDeductionMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmployeeDeductionMaster))
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtKKKLoanTotal = New System.Windows.Forms.TextBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtKKKInstalment = New System.Windows.Forms.TextBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtQuarterType = New System.Windows.Forms.TextBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtBankInstalment = New System.Windows.Forms.TextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtBankAccountNo = New System.Windows.Forms.TextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtLICPremiumAmt = New System.Windows.Forms.TextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtLICPolicyNo = New System.Windows.Forms.TextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.lblEmpName = New common.Controls.MyLabel()
        Me.txtEmpCode = New common.UserControls.txtFinder()
        Me.lblempcode = New common.Controls.MyLabel()
        Me.lblCode = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.txtQuarterAllotedDate = New common.Controls.MyDateTimePicker()
        Me.txtQuarterLeftDate = New common.Controls.MyDateTimePicker()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuarterAllotedDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuarterLeftDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(996, 20)
        Me.RadMenu1.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmpName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEmpCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblempcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(996, 430)
        Me.SplitContainer1.SplitterDistance = 393
        Me.SplitContainer1.TabIndex = 1
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.txtKKKLoanTotal)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox5.Controls.Add(Me.txtKKKInstalment)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox5.HeaderText = "Karmchari Kalyan Kosh"
        Me.RadGroupBox5.Location = New System.Drawing.Point(12, 251)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Size = New System.Drawing.Size(960, 46)
        Me.RadGroupBox5.TabIndex = 219
        Me.RadGroupBox5.Text = "Karmchari Kalyan Kosh"
        '
        'txtKKKLoanTotal
        '
        Me.txtKKKLoanTotal.Location = New System.Drawing.Point(406, 17)
        Me.txtKKKLoanTotal.Name = "txtKKKLoanTotal"
        Me.txtKKKLoanTotal.Size = New System.Drawing.Size(225, 20)
        Me.txtKKKLoanTotal.TabIndex = 216
        Me.txtKKKLoanTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(321, 20)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel9.TabIndex = 215
        Me.MyLabel9.Text = "Loan Total"
        '
        'txtKKKInstalment
        '
        Me.txtKKKInstalment.Location = New System.Drawing.Point(90, 17)
        Me.txtKKKInstalment.Name = "txtKKKInstalment"
        Me.txtKKKInstalment.Size = New System.Drawing.Size(225, 20)
        Me.txtKKKInstalment.TabIndex = 214
        Me.txtKKKInstalment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(5, 20)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel10.TabIndex = 213
        Me.MyLabel10.Text = "Instalment"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.TextBox7)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox4.Controls.Add(Me.TextBox8)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox4.HeaderText = "Light and Water"
        Me.RadGroupBox4.Location = New System.Drawing.Point(13, 203)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Size = New System.Drawing.Size(960, 46)
        Me.RadGroupBox4.TabIndex = 218
        Me.RadGroupBox4.Text = "Light and Water"
        '
        'TextBox7
        '
        Me.TextBox7.Location = New System.Drawing.Point(406, 17)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(225, 20)
        Me.TextBox7.TabIndex = 216
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(321, 20)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(78, 16)
        Me.MyLabel7.TabIndex = 215
        Me.MyLabel7.Text = "Premium Amt."
        '
        'TextBox8
        '
        Me.TextBox8.Location = New System.Drawing.Point(90, 17)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New System.Drawing.Size(225, 20)
        Me.TextBox8.TabIndex = 214
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(5, 20)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel8.TabIndex = 213
        Me.MyLabel8.Text = "Policy No."
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.txtQuarterLeftDate)
        Me.RadGroupBox3.Controls.Add(Me.txtQuarterAllotedDate)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel12)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox3.Controls.Add(Me.txtQuarterType)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox3.HeaderText = "Quarter "
        Me.RadGroupBox3.Location = New System.Drawing.Point(13, 155)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(960, 46)
        Me.RadGroupBox3.TabIndex = 217
        Me.RadGroupBox3.Text = "Quarter "
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(637, 20)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel12.TabIndex = 217
        Me.MyLabel12.Text = "Left Date"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(321, 20)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel5.TabIndex = 215
        Me.MyLabel5.Text = "Alloted Date"
        '
        'txtQuarterType
        '
        Me.txtQuarterType.Location = New System.Drawing.Point(90, 17)
        Me.txtQuarterType.Name = "txtQuarterType"
        Me.txtQuarterType.Size = New System.Drawing.Size(225, 20)
        Me.txtQuarterType.TabIndex = 214
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(5, 20)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(73, 16)
        Me.MyLabel6.TabIndex = 213
        Me.MyLabel6.Text = "Quarter Type"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.txtBankInstalment)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel11)
        Me.RadGroupBox2.Controls.Add(Me.txtBankAccountNo)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox2.Controls.Add(Me.txtBankName)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox2.HeaderText = "Bank Loan"
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 106)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(960, 46)
        Me.RadGroupBox2.TabIndex = 216
        Me.RadGroupBox2.Text = "Bank Loan"
        '
        'txtBankInstalment
        '
        Me.txtBankInstalment.Location = New System.Drawing.Point(719, 17)
        Me.txtBankInstalment.Name = "txtBankInstalment"
        Me.txtBankInstalment.Size = New System.Drawing.Size(225, 20)
        Me.txtBankInstalment.TabIndex = 218
        Me.txtBankInstalment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(637, 20)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel11.TabIndex = 217
        Me.MyLabel11.Text = "Instalment"
        '
        'txtBankAccountNo
        '
        Me.txtBankAccountNo.Location = New System.Drawing.Point(406, 17)
        Me.txtBankAccountNo.Name = "txtBankAccountNo"
        Me.txtBankAccountNo.Size = New System.Drawing.Size(225, 20)
        Me.txtBankAccountNo.TabIndex = 216
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(321, 20)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel3.TabIndex = 215
        Me.MyLabel3.Text = "Account No."
        '
        'txtBankName
        '
        Me.txtBankName.Location = New System.Drawing.Point(90, 17)
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.Size = New System.Drawing.Size(225, 20)
        Me.txtBankName.TabIndex = 214
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(5, 20)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel4.TabIndex = 213
        Me.MyLabel4.Text = "Bank Name"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtLICPremiumAmt)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtLICPolicyNo)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.HeaderText = "LIC"
        Me.RadGroupBox1.Location = New System.Drawing.Point(13, 60)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(960, 46)
        Me.RadGroupBox1.TabIndex = 215
        Me.RadGroupBox1.Text = "LIC"
        '
        'txtLICPremiumAmt
        '
        Me.txtLICPremiumAmt.Location = New System.Drawing.Point(406, 17)
        Me.txtLICPremiumAmt.Name = "txtLICPremiumAmt"
        Me.txtLICPremiumAmt.Size = New System.Drawing.Size(225, 20)
        Me.txtLICPremiumAmt.TabIndex = 216
        Me.txtLICPremiumAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(321, 20)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(78, 16)
        Me.MyLabel2.TabIndex = 215
        Me.MyLabel2.Text = "Premium Amt."
        '
        'txtLICPolicyNo
        '
        Me.txtLICPolicyNo.Location = New System.Drawing.Point(90, 17)
        Me.txtLICPolicyNo.Name = "txtLICPolicyNo"
        Me.txtLICPolicyNo.Size = New System.Drawing.Size(225, 20)
        Me.txtLICPolicyNo.TabIndex = 214
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(5, 20)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel1.TabIndex = 213
        Me.MyLabel1.Text = "Policy No."
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(330, 10)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(16, 20)
        Me.btnNew.TabIndex = 214
        Me.btnNew.Text = " "
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.FieldName = Nothing
        Me.lblEmpName.Location = New System.Drawing.Point(330, 34)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(278, 19)
        Me.lblEmpName.TabIndex = 213
        '
        'txtEmpCode
        '
        Me.txtEmpCode.CalculationExpression = Nothing
        Me.txtEmpCode.FieldCode = Nothing
        Me.txtEmpCode.FieldDesc = Nothing
        Me.txtEmpCode.FieldMaxLength = 0
        Me.txtEmpCode.FieldName = Nothing
        Me.txtEmpCode.isCalculatedField = False
        Me.txtEmpCode.IsSourceFromTable = False
        Me.txtEmpCode.IsSourceFromValueList = False
        Me.txtEmpCode.IsUnique = False
        Me.txtEmpCode.Location = New System.Drawing.Point(103, 34)
        Me.txtEmpCode.MendatroryField = True
        Me.txtEmpCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.MyLinkLable1 = Me.lblempcode
        Me.txtEmpCode.MyLinkLable2 = Nothing
        Me.txtEmpCode.MyReadOnly = False
        Me.txtEmpCode.MyShowMasterFormButton = False
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.ReferenceFieldDesc = Nothing
        Me.txtEmpCode.ReferenceFieldName = Nothing
        Me.txtEmpCode.ReferenceTableName = Nothing
        Me.txtEmpCode.Size = New System.Drawing.Size(225, 19)
        Me.txtEmpCode.TabIndex = 210
        Me.txtEmpCode.Value = ""
        '
        'lblempcode
        '
        Me.lblempcode.FieldName = Nothing
        Me.lblempcode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblempcode.Location = New System.Drawing.Point(11, 37)
        Me.lblempcode.Name = "lblempcode"
        Me.lblempcode.Size = New System.Drawing.Size(87, 16)
        Me.lblempcode.TabIndex = 211
        Me.lblempcode.Text = "Employee Code"
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(12, 15)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(33, 16)
        Me.lblCode.TabIndex = 212
        Me.lblCode.Text = "Code"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(103, 10)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Nothing
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(225, 21)
        Me.txtCode.TabIndex = 209
        Me.txtCode.Value = ""
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(916, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(80, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 18)
        Me.btnPost.TabIndex = 216
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(11, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 18)
        Me.btnsave.TabIndex = 215
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(149, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(68, 18)
        Me.btndelete.TabIndex = 217
        Me.btndelete.Text = "Delete"
        '
        'txtQuarterAllotedDate
        '
        Me.txtQuarterAllotedDate.CalculationExpression = Nothing
        Me.txtQuarterAllotedDate.CustomFormat = "dd/MM/yyyy"
        Me.txtQuarterAllotedDate.FieldCode = Nothing
        Me.txtQuarterAllotedDate.FieldDesc = Nothing
        Me.txtQuarterAllotedDate.FieldMaxLength = 0
        Me.txtQuarterAllotedDate.FieldName = Nothing
        Me.txtQuarterAllotedDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuarterAllotedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtQuarterAllotedDate.isCalculatedField = False
        Me.txtQuarterAllotedDate.IsSourceFromTable = False
        Me.txtQuarterAllotedDate.IsSourceFromValueList = False
        Me.txtQuarterAllotedDate.IsUnique = False
        Me.txtQuarterAllotedDate.Location = New System.Drawing.Point(406, 17)
        Me.txtQuarterAllotedDate.MendatroryField = False
        Me.txtQuarterAllotedDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtQuarterAllotedDate.MyLinkLable1 = Nothing
        Me.txtQuarterAllotedDate.MyLinkLable2 = Nothing
        Me.txtQuarterAllotedDate.Name = "txtQuarterAllotedDate"
        Me.txtQuarterAllotedDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtQuarterAllotedDate.ReferenceFieldDesc = Nothing
        Me.txtQuarterAllotedDate.ReferenceFieldName = Nothing
        Me.txtQuarterAllotedDate.ReferenceTableName = Nothing
        Me.txtQuarterAllotedDate.Size = New System.Drawing.Size(225, 18)
        Me.txtQuarterAllotedDate.TabIndex = 218
        Me.txtQuarterAllotedDate.TabStop = False
        Me.txtQuarterAllotedDate.Text = "17/05/2024"
        Me.txtQuarterAllotedDate.Value = New Date(2024, 5, 17, 0, 0, 0, 0)
        '
        'txtQuarterLeftDate
        '
        Me.txtQuarterLeftDate.CalculationExpression = Nothing
        Me.txtQuarterLeftDate.CustomFormat = "dd/MM/yyyy"
        Me.txtQuarterLeftDate.FieldCode = Nothing
        Me.txtQuarterLeftDate.FieldDesc = Nothing
        Me.txtQuarterLeftDate.FieldMaxLength = 0
        Me.txtQuarterLeftDate.FieldName = Nothing
        Me.txtQuarterLeftDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuarterLeftDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtQuarterLeftDate.isCalculatedField = False
        Me.txtQuarterLeftDate.IsSourceFromTable = False
        Me.txtQuarterLeftDate.IsSourceFromValueList = False
        Me.txtQuarterLeftDate.IsUnique = False
        Me.txtQuarterLeftDate.Location = New System.Drawing.Point(719, 17)
        Me.txtQuarterLeftDate.MendatroryField = False
        Me.txtQuarterLeftDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtQuarterLeftDate.MyLinkLable1 = Nothing
        Me.txtQuarterLeftDate.MyLinkLable2 = Nothing
        Me.txtQuarterLeftDate.Name = "txtQuarterLeftDate"
        Me.txtQuarterLeftDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtQuarterLeftDate.ReferenceFieldDesc = Nothing
        Me.txtQuarterLeftDate.ReferenceFieldName = Nothing
        Me.txtQuarterLeftDate.ReferenceTableName = Nothing
        Me.txtQuarterLeftDate.Size = New System.Drawing.Size(225, 18)
        Me.txtQuarterLeftDate.TabIndex = 219
        Me.txtQuarterLeftDate.TabStop = False
        Me.txtQuarterLeftDate.Text = "17/05/2024"
        Me.txtQuarterLeftDate.Value = New Date(2024, 5, 17, 0, 0, 0, 0)
        '
        'frmEmployeeDeductionMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(996, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmEmployeeDeductionMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmEmployeeDeductionMaster"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQuarterAllotedDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQuarterLeftDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblEmpName As Controls.MyLabel
    Friend WithEvents txtEmpCode As UserControls.txtFinder
    Friend WithEvents lblempcode As Controls.MyLabel
    Friend WithEvents lblCode As Controls.MyLabel
    Friend WithEvents txtCode As UserControls.txtNavigator
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel1 As Controls.MyLabel
    Friend WithEvents txtLICPremiumAmt As TextBox
    Friend WithEvents MyLabel2 As Controls.MyLabel
    Friend WithEvents txtLICPolicyNo As TextBox
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtKKKLoanTotal As TextBox
    Friend WithEvents MyLabel9 As Controls.MyLabel
    Friend WithEvents txtKKKInstalment As TextBox
    Friend WithEvents MyLabel10 As Controls.MyLabel
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents TextBox7 As TextBox
    Friend WithEvents MyLabel7 As Controls.MyLabel
    Friend WithEvents TextBox8 As TextBox
    Friend WithEvents MyLabel8 As Controls.MyLabel
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel5 As Controls.MyLabel
    Friend WithEvents txtQuarterType As TextBox
    Friend WithEvents MyLabel6 As Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtBankAccountNo As TextBox
    Friend WithEvents MyLabel3 As Controls.MyLabel
    Friend WithEvents txtBankName As TextBox
    Friend WithEvents MyLabel4 As Controls.MyLabel
    Friend WithEvents txtBankInstalment As TextBox
    Friend WithEvents MyLabel11 As Controls.MyLabel
    Friend WithEvents MyLabel12 As Controls.MyLabel
    Friend WithEvents txtQuarterLeftDate As Controls.MyDateTimePicker
    Friend WithEvents txtQuarterAllotedDate As Controls.MyDateTimePicker
End Class
