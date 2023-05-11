<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmShipToLocationReport_vb
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.fndToCust = New common.UserControls.txtFinder
        Me.fndFromCust = New common.UserControls.txtFinder
        Me.lblToCustomerNo = New common.Controls.MyLabel
        Me.lblFromCustomerNo = New common.Controls.MyLabel
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.menuPrint = New Telerik.WinControls.UI.RadMenuItem
        Me.menuClose = New Telerik.WinControls.UI.RadMenuItem
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblToCustomerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromCustomerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.fndToCust)
        Me.RadGroupBox1.Controls.Add(Me.fndFromCust)
        Me.RadGroupBox1.Controls.Add(Me.lblToCustomerNo)
        Me.RadGroupBox1.Controls.Add(Me.lblFromCustomerNo)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(13, 23)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(535, 45)
        Me.RadGroupBox1.TabIndex = 0
        '
        'fndToCust
        '
        Me.fndToCust.AccessibleName = "fndToCust"
        Me.fndToCust.Location = New System.Drawing.Point(374, 15)
        Me.fndToCust.MendatroryField = True
        Me.fndToCust.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndToCust.MyLinkLable1 = Nothing
        Me.fndToCust.MyLinkLable2 = Nothing
        Me.fndToCust.MyReadOnly = False
        Me.fndToCust.Name = "fndToCust"
        Me.fndToCust.Size = New System.Drawing.Size(140, 20)
        Me.fndToCust.TabIndex = 52
        Me.fndToCust.Value = ""
        '
        'fndFromCust
        '
        Me.fndFromCust.AccessibleName = "fndFromCust"
        Me.fndFromCust.Location = New System.Drawing.Point(130, 15)
        Me.fndFromCust.MendatroryField = True
        Me.fndFromCust.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndFromCust.MyLinkLable1 = Nothing
        Me.fndFromCust.MyLinkLable2 = Nothing
        Me.fndFromCust.MyReadOnly = False
        Me.fndFromCust.Name = "fndFromCust"
        Me.fndFromCust.Size = New System.Drawing.Size(140, 20)
        Me.fndFromCust.TabIndex = 51
        Me.fndFromCust.Value = ""
        '
        'lblToCustomerNo
        '
        Me.lblToCustomerNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToCustomerNo.Location = New System.Drawing.Point(276, 16)
        Me.lblToCustomerNo.Name = "lblToCustomerNo"
        Me.lblToCustomerNo.Size = New System.Drawing.Size(92, 16)
        Me.lblToCustomerNo.TabIndex = 2
        Me.lblToCustomerNo.Text = "To Customer No."
        '
        'lblFromCustomerNo
        '
        Me.lblFromCustomerNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromCustomerNo.Location = New System.Drawing.Point(13, 16)
        Me.lblFromCustomerNo.Name = "lblFromCustomerNo"
        Me.lblFromCustomerNo.Size = New System.Drawing.Size(106, 16)
        Me.lblFromCustomerNo.TabIndex = 0
        Me.lblFromCustomerNo.Text = "From Customer No."
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(480, 76)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(13, 74)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = "Print"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(582, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.menuPrint, Me.menuClose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'menuPrint
        '
        Me.menuPrint.AccessibleDescription = "Print.."
        Me.menuPrint.AccessibleName = "Print.."
        Me.menuPrint.Name = "menuPrint"
        Me.menuPrint.Text = "Print.."
        Me.menuPrint.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'menuClose
        '
        Me.menuClose.AccessibleDescription = "Close"
        Me.menuClose.AccessibleName = "Close"
        Me.menuClose.Name = "menuClose"
        Me.menuClose.Text = "Close"
        Me.menuClose.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.btnReset)
        Me.RadGroupBox2.Controls.Add(Me.RadGroupBox1)
        Me.RadGroupBox2.Controls.Add(Me.btnClose)
        Me.RadGroupBox2.Controls.Add(Me.btnPrint)
        Me.RadGroupBox2.HeaderText = " "
        Me.RadGroupBox2.Location = New System.Drawing.Point(12, 26)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(561, 98)
        Me.RadGroupBox2.TabIndex = 6
        Me.RadGroupBox2.Text = " "
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(96, 74)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 44
        Me.btnReset.Text = "Reset"
        '
        'FrmShipToLocationReport_vb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(582, 131)
        Me.Controls.Add(Me.RadGroupBox2)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmShipToLocationReport_vb"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Ship To Location"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblToCustomerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromCustomerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuPrint As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndToCust As common.UserControls.txtFinder
    Friend WithEvents fndFromCust As common.UserControls.txtFinder
    Friend WithEvents lblToCustomerNo As common.Controls.MyLabel
    Friend WithEvents lblFromCustomerNo As common.Controls.MyLabel
End Class

