<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBackup
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
        Me.btnBackup = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.txtServerPath = New Telerik.WinControls.UI.RadTextBox()
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        CType(Me.btnBackup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtServerPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnBackup
        '
        Me.btnBackup.Location = New System.Drawing.Point(351, 9)
        Me.btnBackup.Name = "btnBackup"
        Me.btnBackup.Size = New System.Drawing.Size(87, 18)
        Me.btnBackup.TabIndex = 6
        Me.btnBackup.Text = "Take Backup"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Location = New System.Drawing.Point(374, 50)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(68, 18)
        Me.RadButton1.TabIndex = 7
        Me.RadButton1.Text = "Close"
        '
        'txtServerPath
        '
        Me.txtServerPath.Location = New System.Drawing.Point(73, 8)
        Me.txtServerPath.Name = "txtServerPath"
        Me.txtServerPath.Size = New System.Drawing.Size(272, 20)
        Me.txtServerPath.TabIndex = 17
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(4, 9)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel1.TabIndex = 18
        Me.RadLabel1.Text = "Server Path"
        '
        'FrmBackup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(445, 70)
        Me.Controls.Add(Me.txtServerPath)
        Me.Controls.Add(Me.RadLabel1)
        Me.Controls.Add(Me.RadButton1)
        Me.Controls.Add(Me.btnBackup)
        Me.Name = "FrmBackup"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Backup"
        CType(Me.btnBackup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtServerPath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBackup As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Private WithEvents txtServerPath As Telerik.WinControls.UI.RadTextBox
    Private WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
End Class

