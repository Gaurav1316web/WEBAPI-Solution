<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTaxDetails
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
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewComboBoxColumn1 As Telerik.WinControls.UI.GridViewComboBoxColumn = New Telerik.WinControls.UI.GridViewComboBoxColumn
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn4 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn5 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn6 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn7 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn8 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn9 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn10 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Me.gvTaxDetails = New common.UserControls.MyRadGridView
        Me.btnOk = New Telerik.WinControls.UI.RadButton
        Me.btnCancel = New Telerik.WinControls.UI.RadButton
        CType(Me.gvTaxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTaxDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gvTaxDetails
        '
        Me.gvTaxDetails.BackColor = System.Drawing.Color.White
        Me.gvTaxDetails.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvTaxDetails.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvTaxDetails.ForeColor = System.Drawing.Color.Black
        Me.gvTaxDetails.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvTaxDetails.Location = New System.Drawing.Point(12, 3)
        '
        'gvTaxDetails
        '
        Me.gvTaxDetails.MasterTemplate.AllowAddNewRow = False
        Me.gvTaxDetails.MasterTemplate.AllowColumnReorder = False
        Me.gvTaxDetails.MasterTemplate.AllowDeleteRow = False
        Me.gvTaxDetails.MasterTemplate.AutoGenerateColumns = False
        GridViewTextBoxColumn1.FieldName = "Tax_Code"
        GridViewTextBoxColumn1.HeaderText = "Tax Authority"
        GridViewTextBoxColumn1.Name = "taxAuthority"
        GridViewTextBoxColumn1.ReadOnly = True
        GridViewTextBoxColumn1.Width = 101
        GridViewTextBoxColumn2.FieldName = "Tax_Code_Desc"
        GridViewTextBoxColumn2.HeaderText = "Description"
        GridViewTextBoxColumn2.Name = "description"
        GridViewTextBoxColumn2.ReadOnly = True
        GridViewTextBoxColumn2.Width = 187
        GridViewComboBoxColumn1.DisplayMember = Nothing
        GridViewComboBoxColumn1.FieldName = "Tax_Rate"
        GridViewComboBoxColumn1.HeaderText = "Rate"
        GridViewComboBoxColumn1.Name = "taxRate"
        GridViewComboBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        GridViewComboBoxColumn1.ValueMember = Nothing
        GridViewComboBoxColumn1.Width = 77
        GridViewTextBoxColumn3.HeaderText = "Basic Amount"
        GridViewTextBoxColumn3.Name = "basicAmount"
        GridViewTextBoxColumn3.ReadOnly = True
        GridViewTextBoxColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        GridViewTextBoxColumn3.Width = 112
        GridViewTextBoxColumn4.HeaderText = "Assessible Amount"
        GridViewTextBoxColumn4.Name = "assessibleAmount"
        GridViewTextBoxColumn4.ReadOnly = True
        GridViewTextBoxColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        GridViewTextBoxColumn4.Width = 121
        GridViewTextBoxColumn5.HeaderText = "Tax Amount"
        GridViewTextBoxColumn5.Name = "taxAmount"
        GridViewTextBoxColumn5.ReadOnly = True
        GridViewTextBoxColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        GridViewTextBoxColumn5.Width = 94
        GridViewTextBoxColumn6.FieldName = "Taxable"
        GridViewTextBoxColumn6.HeaderText = "Taxable"
        GridViewTextBoxColumn6.IsVisible = False
        GridViewTextBoxColumn6.Name = "taxable"
        GridViewTextBoxColumn7.FieldName = "Surtax"
        GridViewTextBoxColumn7.HeaderText = "Surtax"
        GridViewTextBoxColumn7.IsVisible = False
        GridViewTextBoxColumn7.Name = "surtax"
        GridViewTextBoxColumn8.FieldName = "Surtax_Tax_Code"
        GridViewTextBoxColumn8.HeaderText = "Surtax_Code"
        GridViewTextBoxColumn8.IsVisible = False
        GridViewTextBoxColumn8.Name = "surtaxCode"
        GridViewTextBoxColumn9.HeaderText = "column1"
        GridViewTextBoxColumn9.IsVisible = False
        GridViewTextBoxColumn9.Name = "itemAssess"
        GridViewTextBoxColumn10.HeaderText = "column1"
        GridViewTextBoxColumn10.IsVisible = False
        GridViewTextBoxColumn10.Name = "itemTaxAmt"
        Me.gvTaxDetails.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewComboBoxColumn1, GridViewTextBoxColumn3, GridViewTextBoxColumn4, GridViewTextBoxColumn5, GridViewTextBoxColumn6, GridViewTextBoxColumn7, GridViewTextBoxColumn8, GridViewTextBoxColumn9, GridViewTextBoxColumn10})
        Me.gvTaxDetails.MasterTemplate.EnableGrouping = False
        Me.gvTaxDetails.Name = "gvTaxDetails"
        Me.gvTaxDetails.RightToLeft = System.Windows.Forms.RightToLeft.No
        '
        '
        '
        Me.gvTaxDetails.RootElement.ForeColor = System.Drawing.Color.Black
        Me.gvTaxDetails.ShowGroupPanel = False
        Me.gvTaxDetails.Size = New System.Drawing.Size(709, 277)
        Me.gvTaxDetails.TabIndex = 297
        Me.gvTaxDetails.Text = "GV Tax Details"
        '
        'btnOk
        '
        Me.btnOk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOk.Location = New System.Drawing.Point(322, 286)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(68, 18)
        Me.btnOk.TabIndex = 298
        Me.btnOk.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(406, 286)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(68, 18)
        Me.btnCancel.TabIndex = 299
        Me.btnCancel.Text = "Cancel"
        '
        'FrmTaxDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(733, 314)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.gvTaxDetails)
        Me.Name = "FrmTaxDetails"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Item Tax Details"
        CType(Me.gvTaxDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTaxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gvTaxDetails As common.UserControls.MyRadGridView
    Friend WithEvents btnOk As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
End Class

