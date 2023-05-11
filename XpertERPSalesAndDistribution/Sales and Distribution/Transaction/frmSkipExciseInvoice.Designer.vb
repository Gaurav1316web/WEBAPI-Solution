<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSkipExciseInvoice
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
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.txtLocation = New common.UserControls.txtFinder
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.lblNextCodeAfterSkip = New common.Controls.MyLabel
        Me.lblNextCode = New common.Controls.MyLabel
        Me.txtNoOfInvoiceToSkip = New common.MyNumBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.lblLocationName = New common.Controls.MyLabel
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNextCodeAfterSkip, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNextCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoOfInvoiceToSkip, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(1, 98)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(61, 21)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(327, 98)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(61, 21)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'txtLocation
        '
        Me.txtLocation.Location = New System.Drawing.Point(175, 4)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyLinkLable1 = Me.MyLabel4
        Me.txtLocation.MyLinkLable2 = Me.lblLocationName
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(72, 18)
        Me.txtLocation.TabIndex = 0
        Me.txtLocation.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.Location = New System.Drawing.Point(1, 4)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel4.TabIndex = 9
        Me.MyLabel4.Text = "Location"
        '
        'lblNextCodeAfterSkip
        '
        Me.lblNextCodeAfterSkip.BorderVisible = True
        Me.lblNextCodeAfterSkip.Location = New System.Drawing.Point(175, 70)
        Me.lblNextCodeAfterSkip.Name = "lblNextCodeAfterSkip"
        Me.lblNextCodeAfterSkip.Size = New System.Drawing.Size(114, 18)
        Me.lblNextCodeAfterSkip.TabIndex = 4
        Me.lblNextCodeAfterSkip.Text = "Next Generated Code"
        '
        'lblNextCode
        '
        Me.lblNextCode.BorderVisible = True
        Me.lblNextCode.Location = New System.Drawing.Point(175, 26)
        Me.lblNextCode.Name = "lblNextCode"
        Me.lblNextCode.Size = New System.Drawing.Size(114, 18)
        Me.lblNextCode.TabIndex = 5
        Me.lblNextCode.Text = "Next Generated Code"
        '
        'txtNoOfInvoiceToSkip
        '
        Me.txtNoOfInvoiceToSkip.BackColor = System.Drawing.Color.White
        Me.txtNoOfInvoiceToSkip.DecimalPlaces = 0
        Me.txtNoOfInvoiceToSkip.Location = New System.Drawing.Point(175, 47)
        Me.txtNoOfInvoiceToSkip.MendatroryField = False
        Me.txtNoOfInvoiceToSkip.MyLinkLable1 = Me.MyLabel2
        Me.txtNoOfInvoiceToSkip.MyLinkLable2 = Nothing
        Me.txtNoOfInvoiceToSkip.Name = "txtNoOfInvoiceToSkip"
        Me.txtNoOfInvoiceToSkip.Size = New System.Drawing.Size(52, 20)
        Me.txtNoOfInvoiceToSkip.TabIndex = 1
        Me.txtNoOfInvoiceToSkip.Text = "0"
        Me.txtNoOfInvoiceToSkip.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNoOfInvoiceToSkip.Value = 0
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(1, 48)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(118, 18)
        Me.MyLabel2.TabIndex = 7
        Me.MyLabel2.Text = "No Of Invoices to Skip"
        '
        'MyLabel3
        '
        Me.MyLabel3.Location = New System.Drawing.Point(1, 70)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(164, 18)
        Me.MyLabel3.TabIndex = 6
        Me.MyLabel3.Text = "After Skip Document No will be"
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(1, 26)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(114, 18)
        Me.MyLabel1.TabIndex = 8
        Me.MyLabel1.Text = "Next Generated Code"
        '
        'lblLocationName
        '
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.Location = New System.Drawing.Point(251, 4)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(49, 18)
        Me.lblLocationName.TabIndex = 6
        Me.lblLocationName.Text = "Location"
        '
        'FrmSkipExciseInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(388, 119)
        Me.Controls.Add(Me.lblLocationName)
        Me.Controls.Add(Me.txtLocation)
        Me.Controls.Add(Me.MyLabel4)
        Me.Controls.Add(Me.lblNextCodeAfterSkip)
        Me.Controls.Add(Me.lblNextCode)
        Me.Controls.Add(Me.txtNoOfInvoiceToSkip)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.MyLabel3)
        Me.Controls.Add(Me.MyLabel2)
        Me.Controls.Add(Me.MyLabel1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSkipExciseInvoice"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Skip Excise Invoice"
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNextCodeAfterSkip, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNextCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoOfInvoiceToSkip, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtNoOfInvoiceToSkip As common.MyNumBox
    Friend WithEvents lblNextCode As common.Controls.MyLabel
    Friend WithEvents lblNextCodeAfterSkip As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents lblLocationName As common.Controls.MyLabel
End Class

