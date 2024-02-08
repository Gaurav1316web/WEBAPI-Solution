<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFiscalYearEndProcess
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblNextFiscalYear = New common.Controls.MyLabel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.txtNxtFinancialYear = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.TxtFinder1 = New common.UserControls.txtFinder()
        Me.pnlYearEndRollback = New System.Windows.Forms.Panel()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNextFiscalYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlYearEndRollback.SuspendLayout()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MyLabel6)
        Me.GroupBox1.Controls.Add(Me.MyLabel5)
        Me.GroupBox1.Controls.Add(Me.lblNextFiscalYear)
        Me.GroupBox1.Controls.Add(Me.RadLabel15)
        Me.GroupBox1.Controls.Add(Me.txtNxtFinancialYear)
        Me.GroupBox1.Controls.Add(Me.MyLabel4)
        Me.GroupBox1.Controls.Add(Me.MyLabel3)
        Me.GroupBox1.Controls.Add(Me.MyLabel2)
        Me.GroupBox1.Controls.Add(Me.MyLabel1)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(360, 145)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Please verify these point berfore process"
        '
        'MyLabel6
        '
        Me.MyLabel6.BorderVisible = True
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(134, 100)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(120, 16)
        Me.MyLabel6.TabIndex = 39
        Me.MyLabel6.Text = "Current Financial Year"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(7, 100)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(120, 16)
        Me.MyLabel5.TabIndex = 38
        Me.MyLabel5.Text = "Current Financial Year"
        '
        'lblNextFiscalYear
        '
        Me.lblNextFiscalYear.FieldName = Nothing
        Me.lblNextFiscalYear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNextFiscalYear.Location = New System.Drawing.Point(280, 122)
        Me.lblNextFiscalYear.Name = "lblNextFiscalYear"
        Me.lblNextFiscalYear.Size = New System.Drawing.Size(63, 16)
        Me.lblNextFiscalYear.TabIndex = 36
        Me.lblNextFiscalYear.Text = "Fiscal Year"
        Me.lblNextFiscalYear.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblNextFiscalYear.TextWrap = False
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(7, 122)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(105, 16)
        Me.RadLabel15.TabIndex = 37
        Me.RadLabel15.Text = "Next Financial Year"
        '
        'txtNxtFinancialYear
        '
        Me.txtNxtFinancialYear.CalculationExpression = Nothing
        Me.txtNxtFinancialYear.FieldCode = Nothing
        Me.txtNxtFinancialYear.FieldDesc = Nothing
        Me.txtNxtFinancialYear.FieldMaxLength = 0
        Me.txtNxtFinancialYear.FieldName = Nothing
        Me.txtNxtFinancialYear.isCalculatedField = False
        Me.txtNxtFinancialYear.IsSourceFromTable = False
        Me.txtNxtFinancialYear.IsSourceFromValueList = False
        Me.txtNxtFinancialYear.IsUnique = False
        Me.txtNxtFinancialYear.Location = New System.Drawing.Point(134, 121)
        Me.txtNxtFinancialYear.MendatroryField = True
        Me.txtNxtFinancialYear.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNxtFinancialYear.MyLinkLable1 = Me.RadLabel15
        Me.txtNxtFinancialYear.MyLinkLable2 = Me.lblNextFiscalYear
        Me.txtNxtFinancialYear.MyReadOnly = False
        Me.txtNxtFinancialYear.MyShowMasterFormButton = False
        Me.txtNxtFinancialYear.Name = "txtNxtFinancialYear"
        Me.txtNxtFinancialYear.ReferenceFieldDesc = Nothing
        Me.txtNxtFinancialYear.ReferenceFieldName = Nothing
        Me.txtNxtFinancialYear.ReferenceTableName = Nothing
        Me.txtNxtFinancialYear.Size = New System.Drawing.Size(143, 18)
        Me.txtNxtFinancialYear.TabIndex = 35
        Me.txtNxtFinancialYear.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(7, 82)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(266, 18)
        Me.MyLabel4.TabIndex = 3
        Me.MyLabel4.Text = "4. All Tansactions of Financial year should be posted"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(7, 62)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(226, 18)
        Me.MyLabel3.TabIndex = 2
        Me.MyLabel3.Text = "3. No User will be active during this process."
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(7, 42)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(148, 18)
        Me.MyLabel2.TabIndex = 1
        Me.MyLabel2.Text = "2. Take backup of database ."
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(7, 22)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(264, 18)
        Me.MyLabel1.TabIndex = 0
        Me.MyLabel1.Text = "1. Please Verify you chart of account (account type)."
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(9, 154)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(246, 22)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Process Financial Year End Process"
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(261, 154)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(101, 22)
        Me.RadButton1.TabIndex = 2
        Me.RadButton1.Text = "Close"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(5, 4)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel7.TabIndex = 39
        Me.MyLabel7.Text = "Financial Year"
        '
        'TxtFinder1
        '
        Me.TxtFinder1.CalculationExpression = Nothing
        Me.TxtFinder1.FieldCode = Nothing
        Me.TxtFinder1.FieldDesc = Nothing
        Me.TxtFinder1.FieldMaxLength = 0
        Me.TxtFinder1.FieldName = Nothing
        Me.TxtFinder1.isCalculatedField = False
        Me.TxtFinder1.IsSourceFromTable = False
        Me.TxtFinder1.IsSourceFromValueList = False
        Me.TxtFinder1.IsUnique = False
        Me.TxtFinder1.Location = New System.Drawing.Point(89, 3)
        Me.TxtFinder1.MendatroryField = True
        Me.TxtFinder1.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinder1.MyLinkLable1 = Me.MyLabel7
        Me.TxtFinder1.MyLinkLable2 = Me.lblNextFiscalYear
        Me.TxtFinder1.MyReadOnly = False
        Me.TxtFinder1.MyShowMasterFormButton = False
        Me.TxtFinder1.Name = "TxtFinder1"
        Me.TxtFinder1.ReferenceFieldDesc = Nothing
        Me.TxtFinder1.ReferenceFieldName = Nothing
        Me.TxtFinder1.ReferenceTableName = Nothing
        Me.TxtFinder1.Size = New System.Drawing.Size(110, 18)
        Me.TxtFinder1.TabIndex = 38
        Me.TxtFinder1.Value = ""
        '
        'pnlYearEndRollback
        '
        Me.pnlYearEndRollback.Controls.Add(Me.RadButton2)
        Me.pnlYearEndRollback.Controls.Add(Me.TxtFinder1)
        Me.pnlYearEndRollback.Controls.Add(Me.MyLabel7)
        Me.pnlYearEndRollback.Location = New System.Drawing.Point(1, 185)
        Me.pnlYearEndRollback.Name = "pnlYearEndRollback"
        Me.pnlYearEndRollback.Size = New System.Drawing.Size(374, 25)
        Me.pnlYearEndRollback.TabIndex = 40
        Me.pnlYearEndRollback.Visible = False
        '
        'RadButton2
        '
        Me.RadButton2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton2.Location = New System.Drawing.Point(205, 1)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(168, 22)
        Me.RadButton2.TabIndex = 40
        Me.RadButton2.Text = "Rollback Year End Process"
        '
        'FrmFiscalYearEndProcess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 233)
        Me.Controls.Add(Me.pnlYearEndRollback)
        Me.Controls.Add(Me.RadButton1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FrmFiscalYearEndProcess"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Financial Year End Process"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNextFiscalYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlYearEndRollback.ResumeLayout(False)
        Me.pnlYearEndRollback.PerformLayout()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblNextFiscalYear As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents txtNxtFinancialYear As common.UserControls.txtFinder
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents TxtFinder1 As common.UserControls.txtFinder
    Friend WithEvents pnlYearEndRollback As System.Windows.Forms.Panel
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
End Class

