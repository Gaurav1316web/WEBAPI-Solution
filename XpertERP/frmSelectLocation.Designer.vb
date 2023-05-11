<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectLocation
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.lblLocation = New common.Controls.MyLabel
        Me.RadLabel15 = New common.Controls.MyLabel
        Me.txtLocation = New common.UserControls.txtFinder
        Me.rdoMultiple = New common.Controls.MyRadioButton
        Me.rdoSingle = New common.Controls.MyRadioButton
        Me.btnOK = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdoMultiple, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdoSingle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel15)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdoMultiple)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdoSingle)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnOK)
        Me.SplitContainer1.Size = New System.Drawing.Size(462, 144)
        Me.SplitContainer1.SplitterDistance = 113
        Me.SplitContainer1.TabIndex = 0
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(215, 47)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(235, 18)
        Me.lblLocation.TabIndex = 31
        Me.lblLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocation.TextWrap = False
        '
        'RadLabel15
        '
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(9, 48)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 32
        Me.RadLabel15.Text = "Location"
        '
        'txtLocation
        '
        Me.txtLocation.Enabled = False
        Me.txtLocation.Location = New System.Drawing.Point(68, 47)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtLocation.MyLinkLable2 = Me.lblLocation
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(144, 19)
        Me.txtLocation.TabIndex = 30
        Me.txtLocation.Value = ""
        '
        'rdoMultiple
        '
        Me.rdoMultiple.Location = New System.Drawing.Point(116, 12)
        Me.rdoMultiple.MyLinkLable1 = Nothing
        Me.rdoMultiple.MyLinkLable2 = Nothing
        Me.rdoMultiple.Name = "rdoMultiple"
        Me.rdoMultiple.Size = New System.Drawing.Size(107, 18)
        Me.rdoMultiple.TabIndex = 4
        Me.rdoMultiple.Text = "Multiple Location"
        '
        'rdoSingle
        '
        Me.rdoSingle.Location = New System.Drawing.Point(12, 12)
        Me.rdoSingle.MyLinkLable1 = Nothing
        Me.rdoSingle.MyLinkLable2 = Nothing
        Me.rdoSingle.Name = "rdoSingle"
        Me.rdoSingle.Size = New System.Drawing.Size(96, 18)
        Me.rdoSingle.TabIndex = 3
        Me.rdoSingle.Text = "Single Location"
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Location = New System.Drawing.Point(12, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(77, 21)
        Me.btnOK.TabIndex = 14
        Me.btnOK.Text = "F5 : OK"
        '
        'frmSelectLocation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(462, 144)
        Me.ControlBox = False
        Me.Controls.Add(Me.SplitContainer1)
        Me.KeyPreview = True
        Me.MaximumSize = New System.Drawing.Size(470, 174)
        Me.MinimumSize = New System.Drawing.Size(470, 174)
        Me.Name = "frmSelectLocation"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.RootElement.MaxSize = New System.Drawing.Size(470, 174)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Location"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdoMultiple, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdoSingle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnOK As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdoMultiple As common.Controls.MyRadioButton
    Friend WithEvents rdoSingle As common.Controls.MyRadioButton
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
End Class

