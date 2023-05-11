<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMachineIntegration
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.MenuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkAnalyzer = New common.Controls.MyRadioButton()
        Me.chkEWS = New common.Controls.MyRadioButton()
        Me.fndCode = New common.UserControls.txtNavigator()
        Me.txtdesc = New common.Controls.MyTextBox()
        Me.lblvendorname = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.lblvandorno = New common.Controls.MyLabel()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtInput = New common.Controls.MyTextBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtFracNoOfChar = New common.Controls.MyTextBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtFracStartPos = New common.Controls.MyTextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtIntNoOfChar = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtIntFromPos = New common.Controls.MyTextBox()
        Me.txtStartStopSymbol = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtDataSample = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.chkIsContinuousReading = New System.Windows.Forms.CheckBox()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.chkCheckForZero = New System.Windows.Forms.CheckBox()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.chkAnalyzer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEWS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvendorname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.txtInput, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.txtFracNoOfChar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFracStartPos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtIntNoOfChar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIntFromPos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStartStopSymbol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDataSample, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuClose})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(659, 20)
        Me.RadMenu1.TabIndex = 17
        Me.RadMenu1.Text = "RadMenu1"
        '
        'MenuClose
        '
        Me.MenuClose.AccessibleDescription = "File"
        Me.MenuClose.AccessibleName = "File"
        Me.MenuClose.Name = "MenuClose"
        Me.MenuClose.Text = "File"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(659, 464)
        Me.SplitContainer1.SplitterDistance = 428
        Me.SplitContainer1.TabIndex = 18
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtdesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblvendorname)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblvandorno)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Size = New System.Drawing.Size(659, 428)
        Me.SplitContainer2.SplitterDistance = 97
        Me.SplitContainer2.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkAnalyzer)
        Me.GroupBox1.Controls.Add(Me.chkEWS)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 48)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(170, 42)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Machine Type"
        '
        'chkAnalyzer
        '
        Me.chkAnalyzer.Location = New System.Drawing.Point(85, 18)
        Me.chkAnalyzer.MyLinkLable1 = Nothing
        Me.chkAnalyzer.MyLinkLable2 = Nothing
        Me.chkAnalyzer.Name = "chkAnalyzer"
        '
        '
        '
        Me.chkAnalyzer.RootElement.StretchHorizontally = True
        Me.chkAnalyzer.RootElement.StretchVertically = True
        Me.chkAnalyzer.Size = New System.Drawing.Size(83, 18)
        Me.chkAnalyzer.TabIndex = 8
        Me.chkAnalyzer.Text = "Analyzer"
        '
        'chkEWS
        '
        Me.chkEWS.Location = New System.Drawing.Point(10, 18)
        Me.chkEWS.MyLinkLable1 = Nothing
        Me.chkEWS.MyLinkLable2 = Nothing
        Me.chkEWS.Name = "chkEWS"
        '
        '
        '
        Me.chkEWS.RootElement.StretchHorizontally = True
        Me.chkEWS.RootElement.StretchVertically = True
        Me.chkEWS.Size = New System.Drawing.Size(69, 18)
        Me.chkEWS.TabIndex = 7
        Me.chkEWS.Text = "EWS"
        '
        'fndCode
        '
        Me.fndCode.FieldName = Nothing
        Me.fndCode.Location = New System.Drawing.Point(111, 5)
        Me.fndCode.MendatroryField = False
        Me.fndCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCode.MyLinkLable1 = Nothing
        Me.fndCode.MyLinkLable2 = Nothing
        Me.fndCode.MyMaxLength = 32767
        Me.fndCode.MyReadOnly = False
        Me.fndCode.Name = "fndCode"
        Me.fndCode.Size = New System.Drawing.Size(304, 21)
        Me.fndCode.TabIndex = 9
        Me.fndCode.Value = ""
        '
        'txtdesc
        '
        Me.txtdesc.CalculationExpression = Nothing
        Me.txtdesc.FieldCode = Nothing
        Me.txtdesc.FieldDesc = Nothing
        Me.txtdesc.FieldMaxLength = 0
        Me.txtdesc.FieldName = Nothing
        Me.txtdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdesc.isCalculatedField = False
        Me.txtdesc.IsSourceFromTable = False
        Me.txtdesc.IsSourceFromValueList = False
        Me.txtdesc.IsUnique = False
        Me.txtdesc.Location = New System.Drawing.Point(110, 30)
        Me.txtdesc.MaxLength = 150
        Me.txtdesc.MendatroryField = True
        Me.txtdesc.MyLinkLable1 = Me.lblvendorname
        Me.txtdesc.MyLinkLable2 = Nothing
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.ReferenceFieldDesc = Nothing
        Me.txtdesc.ReferenceFieldName = Nothing
        Me.txtdesc.ReferenceTableName = Nothing
        Me.txtdesc.Size = New System.Drawing.Size(321, 18)
        Me.txtdesc.TabIndex = 6
        Me.txtdesc.TabStop = False
        '
        'lblvendorname
        '
        Me.lblvendorname.FieldName = Nothing
        Me.lblvendorname.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblvendorname.Location = New System.Drawing.Point(13, 32)
        Me.lblvendorname.Name = "lblvendorname"
        Me.lblvendorname.Size = New System.Drawing.Size(63, 16)
        Me.lblvendorname.TabIndex = 5
        Me.lblvendorname.Text = "Description"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(417, 6)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 3
        '
        'lblvandorno
        '
        Me.lblvandorno.FieldName = Nothing
        Me.lblvandorno.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblvandorno.Location = New System.Drawing.Point(12, 6)
        Me.lblvandorno.Name = "lblvandorno"
        Me.lblvandorno.Size = New System.Drawing.Size(33, 16)
        Me.lblvandorno.TabIndex = 1
        Me.lblvandorno.Text = "Code"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(659, 327)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "EWS"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.chkCheckForZero)
        Me.RadPageViewPage1.Controls.Add(Me.txtInput)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.txtStartStopSymbol)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtDataSample)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.chkIsContinuousReading)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(38.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(638, 279)
        Me.RadPageViewPage1.Text = "EWS"
        '
        'txtInput
        '
        Me.txtInput.CalculationExpression = Nothing
        Me.txtInput.FieldCode = Nothing
        Me.txtInput.FieldDesc = Nothing
        Me.txtInput.FieldMaxLength = 0
        Me.txtInput.FieldName = Nothing
        Me.txtInput.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInput.isCalculatedField = False
        Me.txtInput.IsSourceFromTable = False
        Me.txtInput.IsSourceFromValueList = False
        Me.txtInput.IsUnique = False
        Me.txtInput.Location = New System.Drawing.Point(461, 120)
        Me.txtInput.MaxLength = 150
        Me.txtInput.MendatroryField = False
        Me.txtInput.MyLinkLable1 = Me.MyLabel8
        Me.txtInput.MyLinkLable2 = Nothing
        Me.txtInput.Name = "txtInput"
        Me.txtInput.ReferenceFieldDesc = Nothing
        Me.txtInput.ReferenceFieldName = Nothing
        Me.txtInput.ReferenceTableName = Nothing
        Me.txtInput.Size = New System.Drawing.Size(80, 18)
        Me.txtInput.TabIndex = 26
        Me.txtInput.TabStop = False
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(416, 121)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel8.TabIndex = 25
        Me.MyLabel8.Text = "Input"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtFracNoOfChar)
        Me.GroupBox3.Controls.Add(Me.MyLabel6)
        Me.GroupBox3.Controls.Add(Me.MyLabel5)
        Me.GroupBox3.Controls.Add(Me.txtFracStartPos)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 70)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(632, 46)
        Me.GroupBox3.TabIndex = 24
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Fractional Part"
        '
        'txtFracNoOfChar
        '
        Me.txtFracNoOfChar.CalculationExpression = Nothing
        Me.txtFracNoOfChar.FieldCode = Nothing
        Me.txtFracNoOfChar.FieldDesc = Nothing
        Me.txtFracNoOfChar.FieldMaxLength = 0
        Me.txtFracNoOfChar.FieldName = Nothing
        Me.txtFracNoOfChar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFracNoOfChar.isCalculatedField = False
        Me.txtFracNoOfChar.IsSourceFromTable = False
        Me.txtFracNoOfChar.IsSourceFromValueList = False
        Me.txtFracNoOfChar.IsUnique = False
        Me.txtFracNoOfChar.Location = New System.Drawing.Point(318, 18)
        Me.txtFracNoOfChar.MaxLength = 150
        Me.txtFracNoOfChar.MendatroryField = True
        Me.txtFracNoOfChar.MyLinkLable1 = Me.MyLabel5
        Me.txtFracNoOfChar.MyLinkLable2 = Nothing
        Me.txtFracNoOfChar.Name = "txtFracNoOfChar"
        Me.txtFracNoOfChar.ReferenceFieldDesc = Nothing
        Me.txtFracNoOfChar.ReferenceFieldName = Nothing
        Me.txtFracNoOfChar.ReferenceTableName = Nothing
        Me.txtFracNoOfChar.Size = New System.Drawing.Size(79, 18)
        Me.txtFracNoOfChar.TabIndex = 22
        Me.txtFracNoOfChar.TabStop = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(194, 19)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(118, 16)
        Me.MyLabel5.TabIndex = 21
        Me.MyLabel5.Text = "Number of Characters"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(12, 19)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel6.TabIndex = 19
        Me.MyLabel6.Text = "From Position"
        '
        'txtFracStartPos
        '
        Me.txtFracStartPos.CalculationExpression = Nothing
        Me.txtFracStartPos.FieldCode = Nothing
        Me.txtFracStartPos.FieldDesc = Nothing
        Me.txtFracStartPos.FieldMaxLength = 0
        Me.txtFracStartPos.FieldName = Nothing
        Me.txtFracStartPos.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFracStartPos.isCalculatedField = False
        Me.txtFracStartPos.IsSourceFromTable = False
        Me.txtFracStartPos.IsSourceFromValueList = False
        Me.txtFracStartPos.IsUnique = False
        Me.txtFracStartPos.Location = New System.Drawing.Point(109, 17)
        Me.txtFracStartPos.MaxLength = 150
        Me.txtFracStartPos.MendatroryField = True
        Me.txtFracStartPos.MyLinkLable1 = Me.MyLabel6
        Me.txtFracStartPos.MyLinkLable2 = Nothing
        Me.txtFracStartPos.Name = "txtFracStartPos"
        Me.txtFracStartPos.ReferenceFieldDesc = Nothing
        Me.txtFracStartPos.ReferenceFieldName = Nothing
        Me.txtFracStartPos.ReferenceTableName = Nothing
        Me.txtFracStartPos.Size = New System.Drawing.Size(79, 18)
        Me.txtFracStartPos.TabIndex = 20
        Me.txtFracStartPos.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtIntNoOfChar)
        Me.GroupBox2.Controls.Add(Me.MyLabel3)
        Me.GroupBox2.Controls.Add(Me.MyLabel4)
        Me.GroupBox2.Controls.Add(Me.txtIntFromPos)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 24)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(632, 46)
        Me.GroupBox2.TabIndex = 23
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Integer Part"
        '
        'txtIntNoOfChar
        '
        Me.txtIntNoOfChar.CalculationExpression = Nothing
        Me.txtIntNoOfChar.FieldCode = Nothing
        Me.txtIntNoOfChar.FieldDesc = Nothing
        Me.txtIntNoOfChar.FieldMaxLength = 0
        Me.txtIntNoOfChar.FieldName = Nothing
        Me.txtIntNoOfChar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIntNoOfChar.isCalculatedField = False
        Me.txtIntNoOfChar.IsSourceFromTable = False
        Me.txtIntNoOfChar.IsSourceFromValueList = False
        Me.txtIntNoOfChar.IsUnique = False
        Me.txtIntNoOfChar.Location = New System.Drawing.Point(318, 18)
        Me.txtIntNoOfChar.MaxLength = 150
        Me.txtIntNoOfChar.MendatroryField = True
        Me.txtIntNoOfChar.MyLinkLable1 = Me.MyLabel4
        Me.txtIntNoOfChar.MyLinkLable2 = Nothing
        Me.txtIntNoOfChar.Name = "txtIntNoOfChar"
        Me.txtIntNoOfChar.ReferenceFieldDesc = Nothing
        Me.txtIntNoOfChar.ReferenceFieldName = Nothing
        Me.txtIntNoOfChar.ReferenceTableName = Nothing
        Me.txtIntNoOfChar.Size = New System.Drawing.Size(79, 18)
        Me.txtIntNoOfChar.TabIndex = 22
        Me.txtIntNoOfChar.TabStop = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(194, 19)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(118, 16)
        Me.MyLabel4.TabIndex = 21
        Me.MyLabel4.Text = "Number of Characters"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(12, 19)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel3.TabIndex = 19
        Me.MyLabel3.Text = "From Position"
        '
        'txtIntFromPos
        '
        Me.txtIntFromPos.CalculationExpression = Nothing
        Me.txtIntFromPos.FieldCode = Nothing
        Me.txtIntFromPos.FieldDesc = Nothing
        Me.txtIntFromPos.FieldMaxLength = 0
        Me.txtIntFromPos.FieldName = Nothing
        Me.txtIntFromPos.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIntFromPos.isCalculatedField = False
        Me.txtIntFromPos.IsSourceFromTable = False
        Me.txtIntFromPos.IsSourceFromValueList = False
        Me.txtIntFromPos.IsUnique = False
        Me.txtIntFromPos.Location = New System.Drawing.Point(109, 17)
        Me.txtIntFromPos.MaxLength = 150
        Me.txtIntFromPos.MendatroryField = True
        Me.txtIntFromPos.MyLinkLable1 = Me.MyLabel3
        Me.txtIntFromPos.MyLinkLable2 = Nothing
        Me.txtIntFromPos.Name = "txtIntFromPos"
        Me.txtIntFromPos.ReferenceFieldDesc = Nothing
        Me.txtIntFromPos.ReferenceFieldName = Nothing
        Me.txtIntFromPos.ReferenceTableName = Nothing
        Me.txtIntFromPos.Size = New System.Drawing.Size(79, 18)
        Me.txtIntFromPos.TabIndex = 20
        Me.txtIntFromPos.TabStop = False
        '
        'txtStartStopSymbol
        '
        Me.txtStartStopSymbol.CalculationExpression = Nothing
        Me.txtStartStopSymbol.FieldCode = Nothing
        Me.txtStartStopSymbol.FieldDesc = Nothing
        Me.txtStartStopSymbol.FieldMaxLength = 0
        Me.txtStartStopSymbol.FieldName = Nothing
        Me.txtStartStopSymbol.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStartStopSymbol.isCalculatedField = False
        Me.txtStartStopSymbol.IsSourceFromTable = False
        Me.txtStartStopSymbol.IsSourceFromValueList = False
        Me.txtStartStopSymbol.IsUnique = False
        Me.txtStartStopSymbol.Location = New System.Drawing.Point(322, 120)
        Me.txtStartStopSymbol.MaxLength = 150
        Me.txtStartStopSymbol.MendatroryField = True
        Me.txtStartStopSymbol.MyLinkLable1 = Me.MyLabel2
        Me.txtStartStopSymbol.MyLinkLable2 = Nothing
        Me.txtStartStopSymbol.Name = "txtStartStopSymbol"
        Me.txtStartStopSymbol.ReferenceFieldDesc = Nothing
        Me.txtStartStopSymbol.ReferenceFieldName = Nothing
        Me.txtStartStopSymbol.ReferenceTableName = Nothing
        Me.txtStartStopSymbol.Size = New System.Drawing.Size(80, 18)
        Me.txtStartStopSymbol.TabIndex = 18
        Me.txtStartStopSymbol.TabStop = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(240, 121)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel2.TabIndex = 17
        Me.MyLabel2.Text = "Stop Symbol"
        '
        'txtDataSample
        '
        Me.txtDataSample.CalculationExpression = Nothing
        Me.txtDataSample.FieldCode = Nothing
        Me.txtDataSample.FieldDesc = Nothing
        Me.txtDataSample.FieldMaxLength = 0
        Me.txtDataSample.FieldName = Nothing
        Me.txtDataSample.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDataSample.isCalculatedField = False
        Me.txtDataSample.IsSourceFromTable = False
        Me.txtDataSample.IsSourceFromValueList = False
        Me.txtDataSample.IsUnique = False
        Me.txtDataSample.Location = New System.Drawing.Point(243, 4)
        Me.txtDataSample.MaxLength = 150
        Me.txtDataSample.MendatroryField = True
        Me.txtDataSample.MyLinkLable1 = Me.MyLabel1
        Me.txtDataSample.MyLinkLable2 = Nothing
        Me.txtDataSample.Name = "txtDataSample"
        Me.txtDataSample.ReferenceFieldDesc = Nothing
        Me.txtDataSample.ReferenceFieldName = Nothing
        Me.txtDataSample.ReferenceTableName = Nothing
        Me.txtDataSample.Size = New System.Drawing.Size(392, 18)
        Me.txtDataSample.TabIndex = 16
        Me.txtDataSample.TabStop = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(3, 6)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(232, 16)
        Me.MyLabel1.TabIndex = 15
        Me.MyLabel1.Text = "Data Sample (Including Space and Symbols)"
        '
        'chkIsContinuousReading
        '
        Me.chkIsContinuousReading.AutoSize = True
        Me.chkIsContinuousReading.Location = New System.Drawing.Point(4, 121)
        Me.chkIsContinuousReading.Name = "chkIsContinuousReading"
        Me.chkIsContinuousReading.Size = New System.Drawing.Size(144, 17)
        Me.chkIsContinuousReading.TabIndex = 14
        Me.chkIsContinuousReading.Text = "Is Continuous Reading"
        Me.chkIsContinuousReading.UseVisualStyleBackColor = True
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(59.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(638, 279)
        Me.RadPageViewPage2.Text = "Analyzer"
        '
        'MyLabel7
        '
        Me.MyLabel7.BorderVisible = True
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Comic Sans MS", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(184, 118)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(270, 43)
        Me.MyLabel7.TabIndex = 6
        Me.MyLabel7.Text = "Under Development"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(578, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(78, 21)
        Me.btnclose.TabIndex = 5
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(87, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(78, 21)
        Me.btndelete.TabIndex = 4
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(3, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(78, 21)
        Me.btnsave.TabIndex = 3
        Me.btnsave.Text = "Save"
        '
        'chkCheckForZero
        '
        Me.chkCheckForZero.AutoSize = True
        Me.chkCheckForZero.Location = New System.Drawing.Point(4, 144)
        Me.chkCheckForZero.Name = "chkCheckForZero"
        Me.chkCheckForZero.Size = New System.Drawing.Size(103, 17)
        Me.chkCheckForZero.TabIndex = 27
        Me.chkCheckForZero.Text = "Check For Zero"
        Me.chkCheckForZero.UseVisualStyleBackColor = True
        '
        'frmMachineIntegration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(659, 484)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmMachineIntegration"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmMachineIntegration"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.chkAnalyzer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEWS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvendorname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.txtInput, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.txtFracNoOfChar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFracStartPos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.txtIntNoOfChar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIntFromPos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStartStopSymbol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDataSample, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents MenuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblvandorno As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtdesc As common.Controls.MyTextBox
    Friend WithEvents lblvendorname As common.Controls.MyLabel
    Friend WithEvents chkAnalyzer As common.Controls.MyRadioButton
    Friend WithEvents chkEWS As common.Controls.MyRadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents fndCode As common.UserControls.txtNavigator
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkIsContinuousReading As System.Windows.Forms.CheckBox
    Friend WithEvents txtDataSample As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtFracNoOfChar As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtFracStartPos As common.Controls.MyTextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtIntNoOfChar As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtIntFromPos As common.Controls.MyTextBox
    Friend WithEvents txtStartStopSymbol As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtInput As common.Controls.MyTextBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents chkCheckForZero As System.Windows.Forms.CheckBox
End Class
