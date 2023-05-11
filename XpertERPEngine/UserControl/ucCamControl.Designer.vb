<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucCamControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.PicBox = New System.Windows.Forms.PictureBox()
        Me.btnPickSavedPic = New Telerik.WinControls.UI.RadButton()
        Me.btnTakePic = New Telerik.WinControls.UI.RadButton()
        CType(Me.PicBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPickSavedPic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnTakePic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PicBox
        '
        Me.PicBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PicBox.Location = New System.Drawing.Point(82, 3)
        Me.PicBox.Name = "PicBox"
        Me.PicBox.Size = New System.Drawing.Size(161, 141)
        Me.PicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicBox.TabIndex = 0
        Me.PicBox.TabStop = False
        '
        'btnPickSavedPic
        '
        Me.btnPickSavedPic.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPickSavedPic.Location = New System.Drawing.Point(171, 148)
        Me.btnPickSavedPic.Name = "btnPickSavedPic"
        Me.btnPickSavedPic.Size = New System.Drawing.Size(151, 22)
        Me.btnPickSavedPic.TabIndex = 133
        Me.btnPickSavedPic.Text = "Pick Saved Image"
        '
        'btnTakePic
        '
        Me.btnTakePic.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnTakePic.Location = New System.Drawing.Point(18, 148)
        Me.btnTakePic.Name = "btnTakePic"
        Me.btnTakePic.Size = New System.Drawing.Size(151, 22)
        Me.btnTakePic.TabIndex = 132
        Me.btnTakePic.Text = "Take Picture From Camera"
        '
        'ucCamControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnPickSavedPic)
        Me.Controls.Add(Me.btnTakePic)
        Me.Controls.Add(Me.PicBox)
        Me.Name = "ucCamControl"
        Me.Size = New System.Drawing.Size(325, 181)
        CType(Me.PicBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPickSavedPic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnTakePic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnPickSavedPic As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnTakePic As Telerik.WinControls.UI.RadButton
    Public WithEvents PicBox As System.Windows.Forms.PictureBox

End Class
