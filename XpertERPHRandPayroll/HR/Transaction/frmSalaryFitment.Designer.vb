Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSalaryFitment
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.dgv = New common.UserControls.MyRadGridView()
        Me.UsLock1 = New common.usLock()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.txtAppcode = New common.UserControls.txtNavigator()
        Me.UcRequisitionDetail1 = New XpertERPHRandPayroll.ucRequisitionDetail()
        Me.txtFixedCTC = New common.MyNumBox()
        Me.txtVariableAmt = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtVariablePay = New common.MyNumBox()
        Me.txtCTC = New common.MyNumBox()
        Me.MyLabel27 = New common.Controls.MyLabel()
        Me.MyLabel29 = New common.Controls.MyLabel()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnpost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFixedCTC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVariableAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVariablePay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCTC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAppcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UcRequisitionDetail1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFixedCTC)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtVariableAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtVariablePay)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCTC)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel27)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel29)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(754, 476)
        Me.SplitContainer1.SplitterDistance = 396
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.dgv)
        Me.RadGroupBox4.HeaderText = ""
        Me.RadGroupBox4.Location = New System.Drawing.Point(8, 222)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(496, 159)
        Me.RadGroupBox4.TabIndex = 122
        '
        'dgv
        '
        Me.dgv.BackColor = System.Drawing.Color.Transparent
        Me.dgv.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgv.ForeColor = System.Drawing.Color.Black
        Me.dgv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgv.Location = New System.Drawing.Point(10, 20)
        '
        'dgv
        '
        Me.dgv.MasterTemplate.AllowAddNewRow = False
        Me.dgv.MasterTemplate.EnableFiltering = True
        Me.dgv.MasterTemplate.ShowHeaderCellButtons = True
        Me.dgv.Name = "dgv"
        Me.dgv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgv.ShowGroupPanel = False
        Me.dgv.ShowHeaderCellButtons = True
        Me.dgv.Size = New System.Drawing.Size(476, 129)
        Me.dgv.TabIndex = 1
        Me.dgv.TabStop = False
        Me.dgv.Text = "RadGridView1"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(650, 12)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 121
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(13, 12)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(83, 16)
        Me.MyLabel3.TabIndex = 120
        Me.MyLabel3.Text = "Applicant Code"
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(307, 10)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(14, 21)
        Me.btnnew.TabIndex = 1
        '
        'txtAppcode
        '
        Me.txtAppcode.FieldName = Nothing
        Me.txtAppcode.Location = New System.Drawing.Point(105, 10)
        Me.txtAppcode.MendatroryField = True
        Me.txtAppcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAppcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtAppcode.MyLinkLable1 = Nothing
        Me.txtAppcode.MyLinkLable2 = Nothing
        Me.txtAppcode.MyMaxLength = 30
        Me.txtAppcode.MyReadOnly = False
        Me.txtAppcode.Name = "txtAppcode"
        Me.txtAppcode.Size = New System.Drawing.Size(202, 21)
        Me.txtAppcode.TabIndex = 0
        Me.txtAppcode.TabStop = False
        Me.txtAppcode.Value = ""
        '
        'UcRequisitionDetail1
        '
        Me.UcRequisitionDetail1.AppCode = ""
        Me.UcRequisitionDetail1.AppDate = ""
        Me.UcRequisitionDetail1.AppName = ""
        Me.UcRequisitionDetail1.DateofBirth = ""
        Me.UcRequisitionDetail1.Email = ""
        Me.UcRequisitionDetail1.Location = New System.Drawing.Point(8, 33)
        Me.UcRequisitionDetail1.Name = "UcRequisitionDetail1"
        Me.UcRequisitionDetail1.ReqCode = ""
        Me.UcRequisitionDetail1.Size = New System.Drawing.Size(742, 93)
        Me.UcRequisitionDetail1.TabIndex = 119
        Me.UcRequisitionDetail1.TelephoneNo = ""
        '
        'txtFixedCTC
        '
        Me.txtFixedCTC.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtFixedCTC.CalculationExpression = Nothing
        Me.txtFixedCTC.DecimalPlaces = 3
        Me.txtFixedCTC.FieldCode = Nothing
        Me.txtFixedCTC.FieldDesc = Nothing
        Me.txtFixedCTC.FieldMaxLength = 0
        Me.txtFixedCTC.FieldName = Nothing
        Me.txtFixedCTC.isCalculatedField = False
        Me.txtFixedCTC.IsSourceFromTable = False
        Me.txtFixedCTC.IsSourceFromValueList = False
        Me.txtFixedCTC.IsUnique = False
        Me.txtFixedCTC.Location = New System.Drawing.Point(175, 196)
        Me.txtFixedCTC.MaxLength = 10
        Me.txtFixedCTC.MendatroryField = True
        Me.txtFixedCTC.MyLinkLable1 = Nothing
        Me.txtFixedCTC.MyLinkLable2 = Nothing
        Me.txtFixedCTC.Name = "txtFixedCTC"
        Me.txtFixedCTC.ReadOnly = True
        Me.txtFixedCTC.ReferenceFieldDesc = Nothing
        Me.txtFixedCTC.ReferenceFieldName = Nothing
        Me.txtFixedCTC.ReferenceTableName = Nothing
        Me.txtFixedCTC.Size = New System.Drawing.Size(118, 20)
        Me.txtFixedCTC.TabIndex = 5
        Me.txtFixedCTC.Text = "0"
        Me.txtFixedCTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFixedCTC.Value = 0.0R
        '
        'txtVariableAmt
        '
        Me.txtVariableAmt.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtVariableAmt.CalculationExpression = Nothing
        Me.txtVariableAmt.DecimalPlaces = 3
        Me.txtVariableAmt.FieldCode = Nothing
        Me.txtVariableAmt.FieldDesc = Nothing
        Me.txtVariableAmt.FieldMaxLength = 0
        Me.txtVariableAmt.FieldName = Nothing
        Me.txtVariableAmt.isCalculatedField = False
        Me.txtVariableAmt.IsSourceFromTable = False
        Me.txtVariableAmt.IsSourceFromValueList = False
        Me.txtVariableAmt.IsUnique = False
        Me.txtVariableAmt.Location = New System.Drawing.Point(175, 174)
        Me.txtVariableAmt.MendatroryField = True
        Me.txtVariableAmt.MyLinkLable1 = Nothing
        Me.txtVariableAmt.MyLinkLable2 = Nothing
        Me.txtVariableAmt.Name = "txtVariableAmt"
        Me.txtVariableAmt.ReadOnly = True
        Me.txtVariableAmt.ReferenceFieldDesc = Nothing
        Me.txtVariableAmt.ReferenceFieldName = Nothing
        Me.txtVariableAmt.ReferenceTableName = Nothing
        Me.txtVariableAmt.Size = New System.Drawing.Size(118, 20)
        Me.txtVariableAmt.TabIndex = 4
        Me.txtVariableAmt.Text = "0"
        Me.txtVariableAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtVariableAmt.Value = 0.0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(13, 176)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(90, 16)
        Me.MyLabel1.TabIndex = 61
        Me.MyLabel1.Text = "Variable Amount"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(13, 199)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(159, 16)
        Me.MyLabel2.TabIndex = 4
        Me.MyLabel2.Text = "Enter fixed CTC (In Rs/Month)"
        '
        'txtVariablePay
        '
        Me.txtVariablePay.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtVariablePay.CalculationExpression = Nothing
        Me.txtVariablePay.DecimalPlaces = 3
        Me.txtVariablePay.FieldCode = Nothing
        Me.txtVariablePay.FieldDesc = Nothing
        Me.txtVariablePay.FieldMaxLength = 0
        Me.txtVariablePay.FieldName = Nothing
        Me.txtVariablePay.isCalculatedField = False
        Me.txtVariablePay.IsSourceFromTable = False
        Me.txtVariablePay.IsSourceFromValueList = False
        Me.txtVariablePay.IsUnique = False
        Me.txtVariablePay.Location = New System.Drawing.Point(175, 152)
        Me.txtVariablePay.MaxLength = 3
        Me.txtVariablePay.MendatroryField = True
        Me.txtVariablePay.MyLinkLable1 = Nothing
        Me.txtVariablePay.MyLinkLable2 = Nothing
        Me.txtVariablePay.Name = "txtVariablePay"
        Me.txtVariablePay.ReferenceFieldDesc = Nothing
        Me.txtVariablePay.ReferenceFieldName = Nothing
        Me.txtVariablePay.ReferenceTableName = Nothing
        Me.txtVariablePay.Size = New System.Drawing.Size(118, 20)
        Me.txtVariablePay.TabIndex = 3
        Me.txtVariablePay.Text = "0"
        Me.txtVariablePay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtVariablePay.Value = 0.0R
        '
        'txtCTC
        '
        Me.txtCTC.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCTC.CalculationExpression = Nothing
        Me.txtCTC.DecimalPlaces = 3
        Me.txtCTC.FieldCode = Nothing
        Me.txtCTC.FieldDesc = Nothing
        Me.txtCTC.FieldMaxLength = 0
        Me.txtCTC.FieldName = Nothing
        Me.txtCTC.isCalculatedField = False
        Me.txtCTC.IsSourceFromTable = False
        Me.txtCTC.IsSourceFromValueList = False
        Me.txtCTC.IsUnique = False
        Me.txtCTC.Location = New System.Drawing.Point(175, 130)
        Me.txtCTC.MaxLength = 10
        Me.txtCTC.MendatroryField = True
        Me.txtCTC.MyLinkLable1 = Nothing
        Me.txtCTC.MyLinkLable2 = Nothing
        Me.txtCTC.Name = "txtCTC"
        Me.txtCTC.ReferenceFieldDesc = Nothing
        Me.txtCTC.ReferenceFieldName = Nothing
        Me.txtCTC.ReferenceTableName = Nothing
        Me.txtCTC.Size = New System.Drawing.Size(118, 20)
        Me.txtCTC.TabIndex = 2
        Me.txtCTC.Text = "0"
        Me.txtCTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCTC.Value = 0.0R
        '
        'MyLabel27
        '
        Me.MyLabel27.FieldName = Nothing
        Me.MyLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel27.Location = New System.Drawing.Point(13, 132)
        Me.MyLabel27.Name = "MyLabel27"
        Me.MyLabel27.Size = New System.Drawing.Size(131, 16)
        Me.MyLabel27.TabIndex = 2
        Me.MyLabel27.Text = "Enter CTC (In Rs/Month)"
        '
        'MyLabel29
        '
        Me.MyLabel29.FieldName = Nothing
        Me.MyLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel29.Location = New System.Drawing.Point(13, 154)
        Me.MyLabel29.Name = "MyLabel29"
        Me.MyLabel29.Size = New System.Drawing.Size(88, 16)
        Me.MyLabel29.TabIndex = 56
        Me.MyLabel29.Text = "Variable Pay(%)"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(75, 47)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(66, 18)
        Me.btnDelete.TabIndex = 7
        Me.btnDelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(7, 47)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 6
        Me.btnsave.Text = "Save"
        '
        'btnpost
        '
        Me.btnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnpost.Location = New System.Drawing.Point(143, 47)
        Me.btnpost.Name = "btnpost"
        Me.btnpost.Size = New System.Drawing.Size(66, 18)
        Me.btnpost.TabIndex = 8
        Me.btnpost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(682, 47)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 9
        Me.btnclose.Text = "Close"
        '
        'FrmSalaryFitment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(754, 476)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmSalaryFitment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Salary Fitment"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.dgv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFixedCTC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVariableAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVariablePay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCTC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtFixedCTC As common.MyNumBox
    Friend WithEvents txtVariableAmt As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtVariablePay As common.MyNumBox
    Friend WithEvents txtCTC As common.MyNumBox
    Friend WithEvents MyLabel27 As common.Controls.MyLabel
    Friend WithEvents MyLabel29 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtAppcode As common.UserControls.txtNavigator
    Friend WithEvents UcRequisitionDetail1 As XpertERPHRandPayroll.ucRequisitionDetail
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dgv As common.UserControls.MyRadGridView
End Class

