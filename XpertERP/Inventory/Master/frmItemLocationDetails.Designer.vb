<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmItemLocationDetails
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
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn4 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn5 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn6 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn7 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.txtprincipledesc = New common.Controls.MyLabel
        Me.txtprinciplecode = New common.UserControls.txtFinder
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.fnditemCode = New common.UserControls.txtNavigator
        Me.lblitemCode = New common.Controls.MyLabel
        Me.rbtnClose = New Telerik.WinControls.UI.RadButton
        Me.grdv = New common.UserControls.MyRadGridView
        Me.txtDescription = New common.Controls.MyTextBox
        Me.lblDescription = New common.Controls.MyLabel
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtprincipledesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblitemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtprincipledesc)
        Me.RadGroupBox1.Controls.Add(Me.txtprinciplecode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.fnditemCode)
        Me.RadGroupBox1.Controls.Add(Me.rbtnClose)
        Me.RadGroupBox1.Controls.Add(Me.grdv)
        Me.RadGroupBox1.Controls.Add(Me.txtDescription)
        Me.RadGroupBox1.Controls.Add(Me.lblDescription)
        Me.RadGroupBox1.Controls.Add(Me.lblitemCode)
        Me.RadGroupBox1.Controls.Add(Me.btnReset)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(9, 5)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(967, 343)
        Me.RadGroupBox1.TabIndex = 0
        '
        'txtprincipledesc
        '
        Me.txtprincipledesc.AutoSize = False
        Me.txtprincipledesc.BorderVisible = True
        Me.txtprincipledesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprincipledesc.Location = New System.Drawing.Point(234, 39)
        Me.txtprincipledesc.Name = "txtprincipledesc"
        Me.txtprincipledesc.Size = New System.Drawing.Size(494, 18)
        Me.txtprincipledesc.TabIndex = 4
        Me.txtprincipledesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtprincipledesc.TextWrap = False
        '
        'txtprinciplecode
        '
        Me.txtprinciplecode.Location = New System.Drawing.Point(90, 39)
        Me.txtprinciplecode.MendatroryField = True
        Me.txtprinciplecode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprinciplecode.MyLinkLable1 = Me.MyLabel1
        Me.txtprinciplecode.MyLinkLable2 = Me.txtprincipledesc
        Me.txtprinciplecode.MyReadOnly = False
        Me.txtprinciplecode.Name = "txtprinciplecode"
        Me.txtprinciplecode.Size = New System.Drawing.Size(143, 18)
        Me.txtprinciplecode.TabIndex = 3
        Me.txtprinciplecode.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 39)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(50, 16)
        Me.MyLabel1.TabIndex = 6
        Me.MyLabel1.Text = "Principle"
        '
        'fnditemCode
        '
        Me.fnditemCode.Location = New System.Drawing.Point(90, 16)
        Me.fnditemCode.MendatroryField = True
        Me.fnditemCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fnditemCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fnditemCode.MyLinkLable1 = Me.lblitemCode
        Me.fnditemCode.MyLinkLable2 = Nothing
        Me.fnditemCode.MyMaxLength = 32767
        Me.fnditemCode.MyReadOnly = False
        Me.fnditemCode.Name = "fnditemCode"
        Me.fnditemCode.Size = New System.Drawing.Size(178, 18)
        Me.fnditemCode.TabIndex = 0
        Me.fnditemCode.Value = ""
        '
        'lblitemCode
        '
        Me.lblitemCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblitemCode.Location = New System.Drawing.Point(12, 17)
        Me.lblitemCode.Name = "lblitemCode"
        Me.lblitemCode.Size = New System.Drawing.Size(58, 16)
        Me.lblitemCode.TabIndex = 4
        Me.lblitemCode.Text = "Item Code"
        '
        'rbtnClose
        '
        Me.rbtnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnClose.Location = New System.Drawing.Point(13, 314)
        Me.rbtnClose.Name = "rbtnClose"
        Me.rbtnClose.Size = New System.Drawing.Size(68, 18)
        Me.rbtnClose.TabIndex = 6
        Me.rbtnClose.Text = "Close"
        '
        'grdv
        '
        Me.grdv.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdv.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdv.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.grdv.ForeColor = System.Drawing.Color.Black
        Me.grdv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.grdv.Location = New System.Drawing.Point(12, 73)
        '
        'grdv
        '
        Me.grdv.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GridViewTextBoxColumn1.HeaderText = "Location Code"
        GridViewTextBoxColumn1.Name = "Location Code"
        GridViewTextBoxColumn1.Width = 136
        GridViewTextBoxColumn2.HeaderText = "Location Name"
        GridViewTextBoxColumn2.Name = "Location Name"
        GridViewTextBoxColumn2.Width = 134
        GridViewTextBoxColumn3.HeaderText = "MRP"
        GridViewTextBoxColumn3.Name = "MRP"
        GridViewTextBoxColumn3.Width = 100
        GridViewTextBoxColumn4.HeaderText = "Batch Number"
        GridViewTextBoxColumn4.Name = "BatchNumber"
        GridViewTextBoxColumn4.Width = 140
        GridViewTextBoxColumn5.HeaderText = "Quantity On Hand"
        GridViewTextBoxColumn5.Name = "Quantity On Hand"
        GridViewTextBoxColumn5.Width = 132
        GridViewTextBoxColumn6.HeaderText = "Item Cost"
        GridViewTextBoxColumn6.Name = "Item Cost"
        GridViewTextBoxColumn6.Width = 145
        GridViewTextBoxColumn7.HeaderText = "Amount"
        GridViewTextBoxColumn7.Name = "Amount"
        GridViewTextBoxColumn7.Width = 139
        Me.grdv.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewTextBoxColumn3, GridViewTextBoxColumn4, GridViewTextBoxColumn5, GridViewTextBoxColumn6, GridViewTextBoxColumn7})
        Me.grdv.MasterTemplate.EnableFiltering = True
        Me.grdv.MasterTemplate.EnableGrouping = False
        Me.grdv.Name = "grdv"
        Me.grdv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grdv.Size = New System.Drawing.Size(942, 233)
        Me.grdv.TabIndex = 5
        Me.grdv.TabStop = False
        Me.grdv.Text = "RadGridView1"
        '
        'txtDescription
        '
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(361, 16)
        Me.txtDescription.MaxLength = 100
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.lblDescription
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReadOnly = True
        Me.txtDescription.Size = New System.Drawing.Size(367, 18)
        Me.txtDescription.TabIndex = 2
        '
        'lblDescription
        '
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(292, 17)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 5
        Me.lblDescription.Text = "Description"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnReset.Location = New System.Drawing.Point(270, 16)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(16, 18)
        Me.btnReset.TabIndex = 1
        '
        'frmItemLocationDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(982, 352)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "frmItemLocationDetails"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Item Location Details"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtprincipledesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblitemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents grdv As common.UserControls.MyRadGridView
    Friend WithEvents rbtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents lblitemCode As common.Controls.MyLabel
    Friend WithEvents fnditemCode As common.UserControls.txtNavigator
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtprincipledesc As common.Controls.MyLabel
    Friend WithEvents txtprinciplecode As common.UserControls.txtFinder
End Class

