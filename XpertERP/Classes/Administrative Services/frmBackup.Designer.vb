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
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCmp = New common.MyCheckBoxGrid
        Me.btnBackup = New Telerik.WinControls.UI.RadButton
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.bDestination = New Telerik.WinControls.UI.RadButton
        Me.tbDestination = New Telerik.WinControls.UI.RadTextBox
        Me.label2 = New Telerik.WinControls.UI.RadLabel
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.txtServerPath = New Telerik.WinControls.UI.RadTextBox
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel
        Me.txtServerName = New Telerik.WinControls.UI.RadTextBox
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.btnBackup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bDestination, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbDestination, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.label2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtServerPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtServerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgCmp)
        Me.RadGroupBox2.HeaderText = "Company"
        Me.RadGroupBox2.Location = New System.Drawing.Point(4, 106)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(425, 307)
        Me.RadGroupBox2.TabIndex = 4
        Me.RadGroupBox2.Text = "Company"
        '
        'cbgCmp
        '
        Me.cbgCmp.CheckedValue = Nothing
        Me.cbgCmp.DataSource = Nothing
        Me.cbgCmp.DisplayMember = "Name"
        Me.cbgCmp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCmp.Location = New System.Drawing.Point(10, 20)
        Me.cbgCmp.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCmp.MyShowHeadrText = False
        Me.cbgCmp.Name = "cbgCmp"
        Me.cbgCmp.Size = New System.Drawing.Size(405, 277)
        Me.cbgCmp.TabIndex = 1
        Me.cbgCmp.ValueMember = "Code"
        '
        'btnBackup
        '
        Me.btnBackup.Location = New System.Drawing.Point(3, 431)
        Me.btnBackup.Name = "btnBackup"
        Me.btnBackup.Size = New System.Drawing.Size(87, 18)
        Me.btnBackup.TabIndex = 6
        Me.btnBackup.Text = "Take Backup"
        '
        'RadButton1
        '
        Me.RadButton1.Location = New System.Drawing.Point(361, 431)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(68, 18)
        Me.RadButton1.TabIndex = 7
        Me.RadButton1.Text = "Close"
        '
        'bDestination
        '
        Me.bDestination.Location = New System.Drawing.Point(351, 72)
        Me.bDestination.Name = "bDestination"
        Me.bDestination.Size = New System.Drawing.Size(78, 24)
        Me.bDestination.TabIndex = 15
        Me.bDestination.Text = "Browse"
        '
        'tbDestination
        '
        Me.tbDestination.Location = New System.Drawing.Point(73, 74)
        Me.tbDestination.Name = "tbDestination"
        Me.tbDestination.Size = New System.Drawing.Size(272, 20)
        Me.tbDestination.TabIndex = 14
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(4, 75)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(60, 18)
        Me.label2.TabIndex = 16
        Me.label2.Text = "Client Path"
        '
        'FolderBrowserDialog1
        '
        '
        'txtServerPath
        '
        Me.txtServerPath.Location = New System.Drawing.Point(73, 42)
        Me.txtServerPath.Name = "txtServerPath"
        Me.txtServerPath.Size = New System.Drawing.Size(272, 20)
        Me.txtServerPath.TabIndex = 17
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(4, 43)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel1.TabIndex = 18
        Me.RadLabel1.Text = "Server Path"
        '
        'txtServerName
        '
        Me.txtServerName.Location = New System.Drawing.Point(73, 12)
        Me.txtServerName.Name = "txtServerName"
        Me.txtServerName.Size = New System.Drawing.Size(272, 20)
        Me.txtServerName.TabIndex = 19
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(4, 13)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(70, 18)
        Me.RadLabel2.TabIndex = 20
        Me.RadLabel2.Text = "Server Name"
        '
        'FrmBackup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(432, 456)
        Me.Controls.Add(Me.txtServerName)
        Me.Controls.Add(Me.RadLabel2)
        Me.Controls.Add(Me.txtServerPath)
        Me.Controls.Add(Me.bDestination)
        Me.Controls.Add(Me.RadLabel1)
        Me.Controls.Add(Me.tbDestination)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.RadButton1)
        Me.Controls.Add(Me.btnBackup)
        Me.Controls.Add(Me.RadGroupBox2)
        Me.Name = "FrmBackup"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Backup"
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.btnBackup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bDestination, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbDestination, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.label2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtServerPath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtServerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCmp As common.MyCheckBoxGrid
    Friend WithEvents btnBackup As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Private WithEvents bDestination As Telerik.WinControls.UI.RadButton
    Private WithEvents tbDestination As Telerik.WinControls.UI.RadTextBox
    Private WithEvents label2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Private WithEvents txtServerPath As Telerik.WinControls.UI.RadTextBox
    Private WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Private WithEvents txtServerName As Telerik.WinControls.UI.RadTextBox
    Private WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
End Class

