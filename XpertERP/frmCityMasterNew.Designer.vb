<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCityMasterNew
    Inherits common.frmBase

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
        Me.lblid = New common.Controls.MyLabel
        Me.txtdes = New common.Controls.MyTextBox
        Me.lbldes = New common.Controls.MyLabel
        Me.txtCode = New common.Controls.MyTextBox
        Me.toolStripContainer1.ContentPanel.SuspendLayout()
        Me.toolStripContainer1.SuspendLayout()
        CType(Me.lblid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'toolStripContainer1
        '
        '
        'toolStripContainer1.ContentPanel
        '
        Me.toolStripContainer1.ContentPanel.Controls.Add(Me.txtCode)
        Me.toolStripContainer1.ContentPanel.Controls.Add(Me.lblid)
        Me.toolStripContainer1.ContentPanel.Controls.Add(Me.txtdes)
        Me.toolStripContainer1.ContentPanel.Controls.Add(Me.lbldes)
        Me.toolStripContainer1.ContentPanel.Size = New System.Drawing.Size(797, 435)
        Me.toolStripContainer1.Size = New System.Drawing.Size(797, 435)
        '
        'lblid
        '
        Me.lblid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblid.Location = New System.Drawing.Point(6, 10)
        Me.lblid.Name = "lblid"
        Me.lblid.Size = New System.Drawing.Size(56, 16)
        Me.lblid.TabIndex = 32
        Me.lblid.Text = "City Code"
        '
        'txtdes
        '
        Me.txtdes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdes.Location = New System.Drawing.Point(79, 33)
        Me.txtdes.MaxLength = 50
        Me.txtdes.MendatroryField = False
        Me.txtdes.MyLinkLable1 = Nothing
        Me.txtdes.MyLinkLable2 = Nothing
        Me.txtdes.Name = "txtdes"
        '
        '
        '
        Me.txtdes.RootElement.StretchVertically = True
        Me.txtdes.Size = New System.Drawing.Size(303, 20)
        Me.txtdes.TabIndex = 30
        '
        'lbldes
        '
        Me.lbldes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldes.Location = New System.Drawing.Point(6, 35)
        Me.lbldes.Name = "lbldes"
        Me.lbldes.Size = New System.Drawing.Size(63, 16)
        Me.lbldes.TabIndex = 31
        Me.lbldes.Text = "Description"
        '
        'txtCode
        '
        Me.txtCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.Location = New System.Drawing.Point(79, 8)
        Me.txtCode.MaxLength = 12
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyLinkLable1 = Nothing
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.Name = "txtCode"
        '
        '
        '
        Me.txtCode.RootElement.StretchVertically = True
        Me.txtCode.Size = New System.Drawing.Size(148, 20)
        Me.txtCode.TabIndex = 31
        '
        'frmCityMasterNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(797, 435)
        Me.Name = "frmCityMasterNew"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "City Master New"
        Me.toolStripContainer1.ContentPanel.ResumeLayout(False)
        Me.toolStripContainer1.ContentPanel.PerformLayout()
        Me.toolStripContainer1.ResumeLayout(False)
        Me.toolStripContainer1.PerformLayout()
        CType(Me.lblid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtdes As common.Controls.MyTextBox
    Friend WithEvents txtCode As common.Controls.MyTextBox
    Friend WithEvents lblid As common.Controls.MyLabel
    Friend WithEvents lbldes As common.Controls.MyLabel
End Class
