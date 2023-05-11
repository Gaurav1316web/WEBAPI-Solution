Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFormulaSelection
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFormulaSelection))
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.btnAddNum = New Telerik.WinControls.UI.RadButton
        Me.txtnum = New common.MyNumBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.btnPer = New Telerik.WinControls.UI.RadButton
        Me.btnClBr = New Telerik.WinControls.UI.RadButton
        Me.btnOpBr = New Telerik.WinControls.UI.RadButton
        Me.btnItemAdd = New Telerik.WinControls.UI.RadButton
        Me.btndiv = New Telerik.WinControls.UI.RadButton
        Me.btnMul = New Telerik.WinControls.UI.RadButton
        Me.btnMin = New Telerik.WinControls.UI.RadButton
        Me.btnPul = New Telerik.WinControls.UI.RadButton
        Me.CboOperand = New common.Controls.MyComboBox
        Me.lblOperand = New common.Controls.MyLabel
        Me.txtFormula = New common.Controls.MyTextBox
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.RadLabel4 = New common.Controls.MyLabel
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.lblOperator = New common.Controls.MyLabel
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.RadMenuItemExport = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.btnValidate = New Telerik.WinControls.UI.RadButton
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.btnAddNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtnum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClBr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOpBr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnItemAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndiv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnMul, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPul, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboOperand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOperand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFormula, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOperator, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnValidate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.btnAddNum)
        Me.RadGroupBox1.Controls.Add(Me.txtnum)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.btnPer)
        Me.RadGroupBox1.Controls.Add(Me.btnClBr)
        Me.RadGroupBox1.Controls.Add(Me.btnOpBr)
        Me.RadGroupBox1.Controls.Add(Me.btnItemAdd)
        Me.RadGroupBox1.Controls.Add(Me.btndiv)
        Me.RadGroupBox1.Controls.Add(Me.btnMul)
        Me.RadGroupBox1.Controls.Add(Me.btnMin)
        Me.RadGroupBox1.Controls.Add(Me.btnPul)
        Me.RadGroupBox1.Controls.Add(Me.CboOperand)
        Me.RadGroupBox1.Controls.Add(Me.txtFormula)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.Controls.Add(Me.RadButton1)
        Me.RadGroupBox1.Controls.Add(Me.lblOperator)
        Me.RadGroupBox1.Controls.Add(Me.lblOperand)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = " "
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(529, 203)
        Me.RadGroupBox1.TabIndex = 1
        Me.RadGroupBox1.Text = " "
        '
        'btnAddNum
        '
        Me.btnAddNum.Location = New System.Drawing.Point(324, 44)
        Me.btnAddNum.Name = "btnAddNum"
        Me.btnAddNum.Size = New System.Drawing.Size(68, 18)
        Me.btnAddNum.TabIndex = 68
        Me.btnAddNum.Text = "Add"
        '
        'txtnum
        '
        Me.txtnum.BackColor = System.Drawing.Color.White
        Me.txtnum.DecimalPlaces = 2
        Me.txtnum.Location = New System.Drawing.Point(99, 43)
        Me.txtnum.MendatroryField = False
        Me.txtnum.MyLinkLable1 = Nothing
        Me.txtnum.MyLinkLable2 = Nothing
        Me.txtnum.Name = "txtnum"
        Me.txtnum.Size = New System.Drawing.Size(219, 20)
        Me.txtnum.TabIndex = 67
        Me.txtnum.Text = "0"
        Me.txtnum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtnum.Value = 0
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(12, 44)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(78, 18)
        Me.MyLabel1.TabIndex = 32
        Me.MyLabel1.Text = "Insert Number"
        '
        'btnPer
        '
        Me.btnPer.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPer.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnPer.Location = New System.Drawing.Point(203, 72)
        Me.btnPer.Name = "btnPer"
        Me.btnPer.Size = New System.Drawing.Size(23, 20)
        Me.btnPer.TabIndex = 31
        Me.btnPer.Text = "%"
        Me.btnPer.Visible = False
        '
        'btnClBr
        '
        Me.btnClBr.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClBr.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClBr.Location = New System.Drawing.Point(255, 72)
        Me.btnClBr.Name = "btnClBr"
        Me.btnClBr.Size = New System.Drawing.Size(23, 20)
        Me.btnClBr.TabIndex = 29
        Me.btnClBr.Text = ")"
        '
        'btnOpBr
        '
        Me.btnOpBr.DisplayStyle = Telerik.WinControls.DisplayStyle.Text
        Me.btnOpBr.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpBr.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnOpBr.Location = New System.Drawing.Point(229, 72)
        Me.btnOpBr.Name = "btnOpBr"
        Me.btnOpBr.Size = New System.Drawing.Size(23, 20)
        Me.btnOpBr.TabIndex = 28
        Me.btnOpBr.Text = "("
        '
        'btnItemAdd
        '
        Me.btnItemAdd.Location = New System.Drawing.Point(324, 16)
        Me.btnItemAdd.Name = "btnItemAdd"
        Me.btnItemAdd.Size = New System.Drawing.Size(68, 18)
        Me.btnItemAdd.TabIndex = 27
        Me.btnItemAdd.Text = "Add"
        '
        'btndiv
        '
        Me.btndiv.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndiv.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btndiv.Location = New System.Drawing.Point(177, 72)
        Me.btndiv.Name = "btndiv"
        Me.btndiv.Size = New System.Drawing.Size(23, 20)
        Me.btndiv.TabIndex = 26
        Me.btndiv.Text = "/"
        '
        'btnMul
        '
        Me.btnMul.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMul.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnMul.Location = New System.Drawing.Point(151, 72)
        Me.btnMul.Name = "btnMul"
        Me.btnMul.Size = New System.Drawing.Size(23, 20)
        Me.btnMul.TabIndex = 25
        Me.btnMul.Text = "X"
        '
        'btnMin
        '
        Me.btnMin.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMin.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnMin.Location = New System.Drawing.Point(125, 72)
        Me.btnMin.Name = "btnMin"
        Me.btnMin.Size = New System.Drawing.Size(23, 20)
        Me.btnMin.TabIndex = 24
        Me.btnMin.Text = "-"
        '
        'btnPul
        '
        Me.btnPul.DisplayStyle = Telerik.WinControls.DisplayStyle.Text
        Me.btnPul.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPul.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnPul.Location = New System.Drawing.Point(99, 72)
        Me.btnPul.Name = "btnPul"
        Me.btnPul.Size = New System.Drawing.Size(23, 20)
        Me.btnPul.TabIndex = 23
        Me.btnPul.Text = "+"
        '
        'CboOperand
        '
        Me.CboOperand.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboOperand.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboOperand.Location = New System.Drawing.Point(99, 16)
        Me.CboOperand.MendatroryField = True
        Me.CboOperand.MyLinkLable1 = Me.lblOperand
        Me.CboOperand.MyLinkLable2 = Nothing
        Me.CboOperand.Name = "CboOperand"
        Me.CboOperand.Size = New System.Drawing.Size(219, 18)
        Me.CboOperand.TabIndex = 21
        '
        'lblOperand
        '
        Me.lblOperand.Location = New System.Drawing.Point(12, 16)
        Me.lblOperand.Name = "lblOperand"
        Me.lblOperand.Size = New System.Drawing.Size(83, 18)
        Me.lblOperand.TabIndex = 0
        Me.lblOperand.Text = "Select Operand"
        '
        'txtFormula
        '
        Me.txtFormula.AutoSize = False
        Me.txtFormula.Location = New System.Drawing.Point(6, 140)
        Me.txtFormula.MaxLength = 50
        Me.txtFormula.MendatroryField = False
        Me.txtFormula.Multiline = True
        Me.txtFormula.MyLinkLable1 = Me.RadLabel3
        Me.txtFormula.MyLinkLable2 = Nothing
        Me.txtFormula.Name = "txtFormula"
        Me.txtFormula.ReadOnly = True
        Me.txtFormula.Size = New System.Drawing.Size(517, 51)
        Me.txtFormula.TabIndex = 3
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(6, 119)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(89, 18)
        Me.RadLabel3.TabIndex = 20
        Me.RadLabel3.Text = "Created Formula"
        '
        'RadLabel4
        '
        Me.RadLabel4.Location = New System.Drawing.Point(12, 89)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(2, 2)
        Me.RadLabel4.TabIndex = 19
        '
        'RadButton1
        '
        Me.RadButton1.Image = CType(resources.GetObject("RadButton1.Image"), System.Drawing.Image)
        Me.RadButton1.Location = New System.Drawing.Point(352, -422)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(14, 20)
        Me.RadButton1.TabIndex = 17
        Me.RadButton1.Text = " "
        '
        'lblOperator
        '
        Me.lblOperator.Location = New System.Drawing.Point(12, 73)
        Me.lblOperator.Name = "lblOperator"
        Me.lblOperator.Size = New System.Drawing.Size(84, 18)
        Me.lblOperator.TabIndex = 1
        Me.lblOperator.Text = "Select Operator"
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(324, 4)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(68, 18)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(460, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnSave.Location = New System.Drawing.Point(392, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Done"
        '
        'RadMenuItemExport
        '
        Me.RadMenuItemExport.AccessibleDescription = "File"
        Me.RadMenuItemExport.AccessibleName = "File"
        Me.RadMenuItemExport.Name = "RadMenuItemExport"
        Me.RadMenuItemExport.Text = "File"
        Me.RadMenuItemExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "File"
        Me.RadMenuItem2.AccessibleName = "File"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "File"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnValidate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Size = New System.Drawing.Size(529, 232)
        Me.SplitContainer1.SplitterDistance = 203
        Me.SplitContainer1.TabIndex = 0
        '
        'btnValidate
        '
        Me.btnValidate.Location = New System.Drawing.Point(6, 3)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(100, 18)
        Me.btnValidate.TabIndex = 69
        Me.btnValidate.Text = "Validate Formula"
        '
        'frmFormulaSelection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(529, 232)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmFormulaSelection"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Create Formula "
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.btnAddNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtnum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClBr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOpBr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnItemAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndiv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnMul, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPul, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboOperand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOperand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFormula, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOperator, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnValidate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtFormula As common.Controls.MyTextBox
    Friend WithEvents lblOperator As common.Controls.MyLabel
    Friend WithEvents lblOperand As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btndiv As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnMul As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnMin As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPul As Telerik.WinControls.UI.RadButton
    Friend WithEvents CboOperand As common.Controls.MyComboBox
    Friend WithEvents btnItemAdd As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPer As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClBr As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnOpBr As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnAddNum As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtnum As common.MyNumBox
    Friend WithEvents btnValidate As Telerik.WinControls.UI.RadButton
End Class

