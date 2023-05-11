<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSet_SegmentValue
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
        Me.btnCLose = New Telerik.WinControls.UI.RadButton
        Me.btnOk = New Telerik.WinControls.UI.RadButton
        Me.txtValue = New Telerik.WinControls.UI.RadTextBox
        Me.DrpOprator = New Telerik.WinControls.UI.RadDropDownList
        Me.lblCurrrentColName = New common.Controls.MyLabel
        Me.lblColumn = New common.Controls.MyLabel
        CType(Me.btnCLose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DrpOprator, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrrentColName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblColumn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCLose
        '
        Me.btnCLose.Location = New System.Drawing.Point(259, 87)
        Me.btnCLose.Name = "btnCLose"
        Me.btnCLose.Size = New System.Drawing.Size(66, 20)
        Me.btnCLose.TabIndex = 3
        Me.btnCLose.Text = "Close"
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(23, 87)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(66, 20)
        Me.btnOk.TabIndex = 2
        Me.btnOk.Text = "Ok"
        '
        'txtValue
        '
        Me.txtValue.Location = New System.Drawing.Point(121, 38)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(204, 20)
        Me.txtValue.TabIndex = 1
        '
        'DrpOprator
        '
        Me.DrpOprator.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.DrpOprator.Location = New System.Drawing.Point(23, 38)
        Me.DrpOprator.Name = "DrpOprator"
        Me.DrpOprator.Size = New System.Drawing.Size(70, 20)
        Me.DrpOprator.TabIndex = 0
        '
        'lblCurrrentColName
        '
        Me.lblCurrrentColName.Location = New System.Drawing.Point(121, 14)
        Me.lblCurrrentColName.Name = "lblCurrrentColName"
        Me.lblCurrrentColName.Size = New System.Drawing.Size(70, 18)
        Me.lblCurrrentColName.TabIndex = 4
        Me.lblCurrrentColName.Text = "--------------"
        '
        'lblColumn
        '
        Me.lblColumn.Location = New System.Drawing.Point(23, 14)
        Me.lblColumn.Name = "lblColumn"
        Me.lblColumn.Size = New System.Drawing.Size(70, 18)
        Me.lblColumn.TabIndex = 5
        Me.lblColumn.Text = "For Column :"
        '
        'frmSet_SegmentValue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 115)
        Me.Controls.Add(Me.btnCLose)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.txtValue)
        Me.Controls.Add(Me.DrpOprator)
        Me.Controls.Add(Me.lblCurrrentColName)
        Me.Controls.Add(Me.lblColumn)
        Me.Name = "frmSet_SegmentValue"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "SetSegmentValue"
        CType(Me.btnCLose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DrpOprator, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrrentColName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblColumn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCLose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnOk As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtValue As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents DrpOprator As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents lblCurrrentColName As common.Controls.MyLabel
    Friend WithEvents lblColumn As common.Controls.MyLabel
End Class

