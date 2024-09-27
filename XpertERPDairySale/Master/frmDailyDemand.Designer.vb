<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDailyDemand
    'Inherits System.Windows.Forms.Form
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.lblQty = New common.Controls.MyLabel()
        Me.txtTranspoterDeductionDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTranspoterDeductionDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtQty)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblQty)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTranspoterDeductionDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 407
        Me.SplitContainer1.TabIndex = 0
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(84, 75)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(100, 20)
        Me.txtQty.TabIndex = 71
        '
        'lblQty
        '
        Me.lblQty.FieldName = Nothing
        Me.lblQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQty.Location = New System.Drawing.Point(30, 77)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(48, 16)
        Me.lblQty.TabIndex = 70
        Me.lblQty.Text = "Quantity"
        '
        'txtTranspoterDeductionDate
        '
        Me.txtTranspoterDeductionDate.CalculationExpression = Nothing
        Me.txtTranspoterDeductionDate.CustomFormat = "dd/MM/yyyy"
        Me.txtTranspoterDeductionDate.FieldCode = Nothing
        Me.txtTranspoterDeductionDate.FieldDesc = Nothing
        Me.txtTranspoterDeductionDate.FieldMaxLength = 0
        Me.txtTranspoterDeductionDate.FieldName = Nothing
        Me.txtTranspoterDeductionDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTranspoterDeductionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtTranspoterDeductionDate.isCalculatedField = False
        Me.txtTranspoterDeductionDate.IsSourceFromTable = False
        Me.txtTranspoterDeductionDate.IsSourceFromValueList = False
        Me.txtTranspoterDeductionDate.IsUnique = False
        Me.txtTranspoterDeductionDate.Location = New System.Drawing.Point(83, 49)
        Me.txtTranspoterDeductionDate.MendatroryField = False
        Me.txtTranspoterDeductionDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtTranspoterDeductionDate.MyLinkLable1 = Me.RadLabel3
        Me.txtTranspoterDeductionDate.MyLinkLable2 = Nothing
        Me.txtTranspoterDeductionDate.Name = "txtTranspoterDeductionDate"
        Me.txtTranspoterDeductionDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtTranspoterDeductionDate.ReferenceFieldDesc = Nothing
        Me.txtTranspoterDeductionDate.ReferenceFieldName = Nothing
        Me.txtTranspoterDeductionDate.ReferenceTableName = Nothing
        Me.txtTranspoterDeductionDate.Size = New System.Drawing.Size(106, 18)
        Me.txtTranspoterDeductionDate.TabIndex = 69
        Me.txtTranspoterDeductionDate.TabStop = False
        Me.txtTranspoterDeductionDate.Text = "17/05/2011"
        Me.txtTranspoterDeductionDate.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(31, 51)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel3.TabIndex = 68
        Me.RadLabel3.Text = "Date"
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.Location = New System.Drawing.Point(30, 26)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(34, 13)
        Me.lblCode.TabIndex = 67
        Me.lblCode.Text = "Code"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(83, 23)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Nothing
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(254, 18)
        Me.txtCode.TabIndex = 66
        Me.txtCode.Value = ""
        '
        'btnNew
        '
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(335, 23)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(18, 18)
        Me.btnNew.TabIndex = 65
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(708, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 18)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(98, 9)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 18)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.Visible = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(12, 9)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 18)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(190, 79)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel1.TabIndex = 72
        Me.MyLabel1.Text = "In Ltr"
        '
        'frmDailyDemand
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmDailyDemand"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmDailyDemand"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTranspoterDeductionDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnNew As RadButton
    Friend WithEvents lblCode As Label
    Friend WithEvents txtTranspoterDeductionDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents lblQty As common.Controls.MyLabel
    Friend WithEvents txtQty As TextBox
    Friend WithEvents btnSave As RadButton
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
End Class
