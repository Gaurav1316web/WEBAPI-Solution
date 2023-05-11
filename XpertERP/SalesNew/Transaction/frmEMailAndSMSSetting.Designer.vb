<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEMailAndSMSSetting
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
        Me.components = New System.ComponentModel.Container
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.txtSMSText = New System.Windows.Forms.TextBox
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MyLabel9 = New common.Controls.MyLabel
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.txtEmailText = New System.Windows.Forms.RichTextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.MyLabel11 = New common.Controls.MyLabel
        Me.txtSubject = New common.Controls.MyTextBox
        Me.RadLabel12 = New common.Controls.MyLabel
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.MyLabel10 = New common.Controls.MyLabel
        Me.txtEmailTo = New common.Controls.MyTextBox
        Me.btnSendTestEMail = New Telerik.WinControls.UI.RadButton
        Me.chkMailEnableSSL = New Telerik.WinControls.UI.RadCheckBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtMailPwd = New common.Controls.MyTextBox
        Me.txtMailID = New common.Controls.MyTextBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtMailPort = New common.Controls.MyTextBox
        Me.RadLabel5 = New common.Controls.MyLabel
        Me.txtMailSMTPClient = New common.Controls.MyTextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.MyLabel8 = New common.Controls.MyLabel
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.txtSMSMobileNo = New common.Controls.MyTextBox
        Me.txtSMSSenderName = New common.Controls.MyTextBox
        Me.txtSMSPWD = New common.Controls.MyTextBox
        Me.MyLabel6 = New common.Controls.MyLabel
        Me.txtSMSUserName = New common.Controls.MyTextBox
        Me.MyLabel7 = New common.Controls.MyLabel
        Me.txtSMSString = New common.Controls.MyTextBox
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnSaveConfiguration = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmailTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSendTestEMail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMailEnableSSL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMailPwd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMailID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMailPort, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMailSMTPClient, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSMSMobileNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSMSSenderName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSMSPWD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSMSUserName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSMSString, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSaveConfiguration, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSaveConfiguration)
        Me.SplitContainer1.Size = New System.Drawing.Size(566, 444)
        Me.SplitContainer1.SplitterDistance = 404
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(566, 404)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.txtSMSText)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(545, 356)
        Me.RadPageViewPage2.Text = "SMS Text"
        '
        'txtSMSText
        '
        Me.txtSMSText.ContextMenuStrip = Me.ContextMenuStrip1
        Me.txtSMSText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSMSText.Location = New System.Drawing.Point(0, 0)
        Me.txtSMSText.Multiline = True
        Me.txtSMSText.Name = "txtSMSText"
        Me.txtSMSText.Size = New System.Drawing.Size(545, 340)
        Me.txtSMSText.TabIndex = 27
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'MyLabel9
        '
        Me.MyLabel9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.ForeColor = System.Drawing.SystemColors.Highlight
        Me.MyLabel9.Location = New System.Drawing.Point(0, 340)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(167, 16)
        Me.MyLabel9.TabIndex = 26
        Me.MyLabel9.Text = "Right Click for Add Constants"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtEmailText)
        Me.RadPageViewPage1.Controls.Add(Me.Panel1)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel12)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(545, 356)
        Me.RadPageViewPage1.Text = "E-Mail Text"
        '
        'txtEmailText
        '
        Me.txtEmailText.ContextMenuStrip = Me.ContextMenuStrip1
        Me.txtEmailText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtEmailText.Location = New System.Drawing.Point(0, 24)
        Me.txtEmailText.Name = "txtEmailText"
        Me.txtEmailText.Size = New System.Drawing.Size(545, 316)
        Me.txtEmailText.TabIndex = 38
        Me.txtEmailText.Text = ""
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.MyLabel11)
        Me.Panel1.Controls.Add(Me.txtSubject)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(545, 24)
        Me.Panel1.TabIndex = 27
        '
        'MyLabel11
        '
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(3, 4)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel11.TabIndex = 39
        Me.MyLabel11.Text = "Subject"
        '
        'txtSubject
        '
        Me.txtSubject.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSubject.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubject.Location = New System.Drawing.Point(89, 3)
        Me.txtSubject.MaxLength = 50
        Me.txtSubject.MendatroryField = False
        Me.txtSubject.MyLinkLable1 = Me.MyLabel11
        Me.txtSubject.MyLinkLable2 = Nothing
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Size = New System.Drawing.Size(452, 18)
        Me.txtSubject.TabIndex = 38
        '
        'RadLabel12
        '
        Me.RadLabel12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel12.Location = New System.Drawing.Point(0, 340)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(167, 16)
        Me.RadLabel12.TabIndex = 25
        Me.RadLabel12.Text = "Right Click for Add Constants"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(545, 356)
        Me.RadPageViewPage3.Text = "Configuration"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Panel2Collapsed = True
        Me.SplitContainer2.Size = New System.Drawing.Size(545, 356)
        Me.SplitContainer2.SplitterDistance = 326
        Me.SplitContainer2.TabIndex = 2
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.GroupBox2)
        Me.SplitContainer3.Size = New System.Drawing.Size(545, 356)
        Me.SplitContainer3.SplitterDistance = 156
        Me.SplitContainer3.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MyLabel10)
        Me.GroupBox1.Controls.Add(Me.txtEmailTo)
        Me.GroupBox1.Controls.Add(Me.btnSendTestEMail)
        Me.GroupBox1.Controls.Add(Me.chkMailEnableSSL)
        Me.GroupBox1.Controls.Add(Me.MyLabel3)
        Me.GroupBox1.Controls.Add(Me.MyLabel2)
        Me.GroupBox1.Controls.Add(Me.txtMailPwd)
        Me.GroupBox1.Controls.Add(Me.txtMailID)
        Me.GroupBox1.Controls.Add(Me.MyLabel1)
        Me.GroupBox1.Controls.Add(Me.txtMailPort)
        Me.GroupBox1.Controls.Add(Me.RadLabel5)
        Me.GroupBox1.Controls.Add(Me.txtMailSMTPClient)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(545, 156)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "SMTP Setting"
        '
        'MyLabel10
        '
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(6, 105)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(73, 16)
        Me.MyLabel10.TabIndex = 50
        Me.MyLabel10.Text = "Email ID (To)"
        '
        'txtEmailTo
        '
        Me.txtEmailTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmailTo.Location = New System.Drawing.Point(92, 104)
        Me.txtEmailTo.MaxLength = 50
        Me.txtEmailTo.MendatroryField = False
        Me.txtEmailTo.MyLinkLable1 = Me.MyLabel10
        Me.txtEmailTo.MyLinkLable2 = Nothing
        Me.txtEmailTo.Name = "txtEmailTo"
        Me.txtEmailTo.Size = New System.Drawing.Size(210, 18)
        Me.txtEmailTo.TabIndex = 49
        '
        'btnSendTestEMail
        '
        Me.btnSendTestEMail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSendTestEMail.Location = New System.Drawing.Point(92, 125)
        Me.btnSendTestEMail.Name = "btnSendTestEMail"
        Me.btnSendTestEMail.Size = New System.Drawing.Size(210, 22)
        Me.btnSendTestEMail.TabIndex = 48
        Me.btnSendTestEMail.Text = "Send A Test E-Mail"
        '
        'chkMailEnableSSL
        '
        Me.chkMailEnableSSL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMailEnableSSL.Location = New System.Drawing.Point(6, 126)
        Me.chkMailEnableSSL.Name = "chkMailEnableSSL"
        Me.chkMailEnableSSL.Size = New System.Drawing.Size(80, 16)
        Me.chkMailEnableSSL.TabIndex = 47
        Me.chkMailEnableSSL.Text = "Enable SSL"
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(6, 84)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel3.TabIndex = 41
        Me.MyLabel3.Text = "Password"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(6, 63)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel2.TabIndex = 41
        Me.MyLabel2.Text = "Email ID (From)"
        '
        'txtMailPwd
        '
        Me.txtMailPwd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMailPwd.Location = New System.Drawing.Point(92, 83)
        Me.txtMailPwd.MaxLength = 50
        Me.txtMailPwd.MendatroryField = False
        Me.txtMailPwd.MyLinkLable1 = Me.MyLabel3
        Me.txtMailPwd.MyLinkLable2 = Nothing
        Me.txtMailPwd.Name = "txtMailPwd"
        Me.txtMailPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtMailPwd.Size = New System.Drawing.Size(210, 18)
        Me.txtMailPwd.TabIndex = 40
        '
        'txtMailID
        '
        Me.txtMailID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMailID.Location = New System.Drawing.Point(92, 62)
        Me.txtMailID.MaxLength = 50
        Me.txtMailID.MendatroryField = False
        Me.txtMailID.MyLinkLable1 = Me.MyLabel2
        Me.txtMailID.MyLinkLable2 = Nothing
        Me.txtMailID.Name = "txtMailID"
        Me.txtMailID.Size = New System.Drawing.Size(210, 18)
        Me.txtMailID.TabIndex = 40
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 42)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(27, 16)
        Me.MyLabel1.TabIndex = 39
        Me.MyLabel1.Text = "Port"
        '
        'txtMailPort
        '
        Me.txtMailPort.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMailPort.Location = New System.Drawing.Point(92, 41)
        Me.txtMailPort.MaxLength = 50
        Me.txtMailPort.MendatroryField = False
        Me.txtMailPort.MyLinkLable1 = Me.MyLabel1
        Me.txtMailPort.MyLinkLable2 = Nothing
        Me.txtMailPort.Name = "txtMailPort"
        Me.txtMailPort.Size = New System.Drawing.Size(210, 18)
        Me.txtMailPort.TabIndex = 38
        '
        'RadLabel5
        '
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(6, 21)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(70, 16)
        Me.RadLabel5.TabIndex = 37
        Me.RadLabel5.Text = "SMTP Client"
        '
        'txtMailSMTPClient
        '
        Me.txtMailSMTPClient.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMailSMTPClient.Location = New System.Drawing.Point(92, 20)
        Me.txtMailSMTPClient.MaxLength = 50
        Me.txtMailSMTPClient.MendatroryField = False
        Me.txtMailSMTPClient.MyLinkLable1 = Me.RadLabel5
        Me.txtMailSMTPClient.MyLinkLable2 = Nothing
        Me.txtMailSMTPClient.Name = "txtMailSMTPClient"
        Me.txtMailSMTPClient.Size = New System.Drawing.Size(210, 18)
        Me.txtMailSMTPClient.TabIndex = 36
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RadButton1)
        Me.GroupBox2.Controls.Add(Me.MyLabel8)
        Me.GroupBox2.Controls.Add(Me.MyLabel4)
        Me.GroupBox2.Controls.Add(Me.MyLabel5)
        Me.GroupBox2.Controls.Add(Me.txtSMSMobileNo)
        Me.GroupBox2.Controls.Add(Me.txtSMSSenderName)
        Me.GroupBox2.Controls.Add(Me.txtSMSPWD)
        Me.GroupBox2.Controls.Add(Me.MyLabel6)
        Me.GroupBox2.Controls.Add(Me.txtSMSUserName)
        Me.GroupBox2.Controls.Add(Me.MyLabel7)
        Me.GroupBox2.Controls.Add(Me.txtSMSString)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(545, 196)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "SMS Setting"
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(92, 137)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(210, 22)
        Me.RadButton1.TabIndex = 50
        Me.RadButton1.Text = "Send A Test SMS"
        '
        'MyLabel8
        '
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(6, 114)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel8.TabIndex = 48
        Me.MyLabel8.Text = "Mobile No"
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(6, 90)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel4.TabIndex = 48
        Me.MyLabel4.Text = "Sender Name"
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(6, 66)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel5.TabIndex = 49
        Me.MyLabel5.Text = "Password"
        '
        'txtSMSMobileNo
        '
        Me.txtSMSMobileNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMSMobileNo.Location = New System.Drawing.Point(92, 113)
        Me.txtSMSMobileNo.MaxLength = 10
        Me.txtSMSMobileNo.MendatroryField = False
        Me.txtSMSMobileNo.MyLinkLable1 = Me.MyLabel8
        Me.txtSMSMobileNo.MyLinkLable2 = Nothing
        Me.txtSMSMobileNo.Name = "txtSMSMobileNo"
        Me.txtSMSMobileNo.Size = New System.Drawing.Size(210, 18)
        Me.txtSMSMobileNo.TabIndex = 46
        '
        'txtSMSSenderName
        '
        Me.txtSMSSenderName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMSSenderName.Location = New System.Drawing.Point(92, 89)
        Me.txtSMSSenderName.MaxLength = 50
        Me.txtSMSSenderName.MendatroryField = False
        Me.txtSMSSenderName.MyLinkLable1 = Me.MyLabel4
        Me.txtSMSSenderName.MyLinkLable2 = Nothing
        Me.txtSMSSenderName.Name = "txtSMSSenderName"
        Me.txtSMSSenderName.Size = New System.Drawing.Size(210, 18)
        Me.txtSMSSenderName.TabIndex = 46
        '
        'txtSMSPWD
        '
        Me.txtSMSPWD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMSPWD.Location = New System.Drawing.Point(92, 65)
        Me.txtSMSPWD.MaxLength = 50
        Me.txtSMSPWD.MendatroryField = False
        Me.txtSMSPWD.MyLinkLable1 = Me.MyLabel5
        Me.txtSMSPWD.MyLinkLable2 = Nothing
        Me.txtSMSPWD.Name = "txtSMSPWD"
        Me.txtSMSPWD.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSMSPWD.Size = New System.Drawing.Size(210, 18)
        Me.txtSMSPWD.TabIndex = 47
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(6, 44)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel6.TabIndex = 45
        Me.MyLabel6.Text = "User Name"
        '
        'txtSMSUserName
        '
        Me.txtSMSUserName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMSUserName.Location = New System.Drawing.Point(92, 43)
        Me.txtSMSUserName.MaxLength = 50
        Me.txtSMSUserName.MendatroryField = False
        Me.txtSMSUserName.MyLinkLable1 = Me.MyLabel6
        Me.txtSMSUserName.MyLinkLable2 = Nothing
        Me.txtSMSUserName.Name = "txtSMSUserName"
        Me.txtSMSUserName.Size = New System.Drawing.Size(210, 18)
        Me.txtSMSUserName.TabIndex = 44
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(6, 21)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel7.TabIndex = 43
        Me.MyLabel7.Text = "SMS String"
        '
        'txtSMSString
        '
        Me.txtSMSString.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMSString.Location = New System.Drawing.Point(92, 20)
        Me.txtSMSString.MaxLength = 2000
        Me.txtSMSString.MendatroryField = False
        Me.txtSMSString.MyLinkLable1 = Me.MyLabel7
        Me.txtSMSString.MyLinkLable2 = Nothing
        Me.txtSMSString.Name = "txtSMSString"
        Me.txtSMSString.Size = New System.Drawing.Size(210, 18)
        Me.txtSMSString.TabIndex = 42
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(490, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 50
        Me.btnClose.Text = "Save"
        '
        'btnSaveConfiguration
        '
        Me.btnSaveConfiguration.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveConfiguration.Location = New System.Drawing.Point(3, 9)
        Me.btnSaveConfiguration.Name = "btnSaveConfiguration"
        Me.btnSaveConfiguration.Size = New System.Drawing.Size(69, 22)
        Me.btnSaveConfiguration.TabIndex = 49
        Me.btnSaveConfiguration.Text = "Save"
        '
        'frmEMailAndSMSSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(566, 444)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmEMailAndSMSSetting"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "E-Mail/SMS Setting"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmailTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSendTestEMail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMailEnableSSL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMailPwd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMailID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMailPort, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMailSMTPClient, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSMSMobileNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSMSSenderName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSMSPWD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSMSUserName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSMSString, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSaveConfiguration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents txtMailSMTPClient As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtMailPwd As common.Controls.MyTextBox
    Friend WithEvents txtMailID As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtMailPort As common.Controls.MyTextBox
    Friend WithEvents chkMailEnableSSL As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSendTestEMail As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSaveConfiguration As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtSMSSenderName As common.Controls.MyTextBox
    Friend WithEvents txtSMSPWD As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtSMSUserName As common.Controls.MyTextBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtSMSString As common.Controls.MyTextBox
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtSMSMobileNo As common.Controls.MyTextBox
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents txtEmailText As System.Windows.Forms.RichTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents txtSMSText As System.Windows.Forms.TextBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtEmailTo As common.Controls.MyTextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtSubject As common.Controls.MyTextBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
End Class

