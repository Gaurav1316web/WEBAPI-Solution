<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptTransfer
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
        Me.grpExcisable = New Telerik.WinControls.UI.RadGroupBox
        Me.cbg = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkTransferSelect = New common.Controls.MyRadioButton
        Me.chkTransferAll = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.rbtnLoadin = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnNonExcisable = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnExcisable = New Telerik.WinControls.UI.RadRadioButton
        Me.txtToDate = New common.Controls.MyDateTimePicker
        Me.txtFromDate = New common.Controls.MyDateTimePicker
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.grpExcisable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpExcisable.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkTransferSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTransferAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.rbtnLoadin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnNonExcisable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnExcisable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox1.Controls.Add(Me.grpExcisable)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.btnClose)
        Me.RadGroupBox1.Controls.Add(Me.btnReset)
        Me.RadGroupBox1.Controls.Add(Me.btnPrint)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 4)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(529, 380)
        Me.RadGroupBox1.TabIndex = 0
        '
        'grpExcisable
        '
        Me.grpExcisable.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpExcisable.Controls.Add(Me.cbg)
        Me.grpExcisable.Controls.Add(Me.Panel1)
        Me.grpExcisable.HeaderText = "Select Transfer Number"
        Me.grpExcisable.Location = New System.Drawing.Point(9, 72)
        Me.grpExcisable.Name = "grpExcisable"
        Me.grpExcisable.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpExcisable.Size = New System.Drawing.Size(507, 273)
        Me.grpExcisable.TabIndex = 59
        Me.grpExcisable.Text = "Select Transfer Number"
        '
        'cbg
        '
        Me.cbg.CheckedValue = Nothing
        Me.cbg.DataSource = Nothing
        Me.cbg.DisplayMember = "Name"
        Me.cbg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbg.Location = New System.Drawing.Point(10, 41)
        Me.cbg.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbg.MyShowHeadrText = False
        Me.cbg.Name = "cbg"
        Me.cbg.Size = New System.Drawing.Size(487, 222)
        Me.cbg.TabIndex = 1
        Me.cbg.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkTransferSelect)
        Me.Panel1.Controls.Add(Me.chkTransferAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(487, 21)
        Me.Panel1.TabIndex = 0
        '
        'chkTransferSelect
        '
        Me.chkTransferSelect.Location = New System.Drawing.Point(256, 1)
        Me.chkTransferSelect.MyLinkLable1 = Nothing
        Me.chkTransferSelect.MyLinkLable2 = Nothing
        Me.chkTransferSelect.Name = "chkTransferSelect"
        Me.chkTransferSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkTransferSelect.TabIndex = 1
        Me.chkTransferSelect.Text = "Select"
        '
        'chkTransferAll
        '
        Me.chkTransferAll.Location = New System.Drawing.Point(205, 1)
        Me.chkTransferAll.MyLinkLable1 = Nothing
        Me.chkTransferAll.MyLinkLable2 = Nothing
        Me.chkTransferAll.Name = "chkTransferAll"
        Me.chkTransferAll.Size = New System.Drawing.Size(33, 18)
        Me.chkTransferAll.TabIndex = 0
        Me.chkTransferAll.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rbtnLoadin)
        Me.RadGroupBox2.Controls.Add(Me.rbtnNonExcisable)
        Me.RadGroupBox2.Controls.Add(Me.rbtnExcisable)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(11, 8)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(509, 35)
        Me.RadGroupBox2.TabIndex = 0
        '
        'rbtnLoadin
        '
        Me.rbtnLoadin.Location = New System.Drawing.Point(318, 8)
        Me.rbtnLoadin.Name = "rbtnLoadin"
        Me.rbtnLoadin.Size = New System.Drawing.Size(54, 18)
        Me.rbtnLoadin.TabIndex = 2
        Me.rbtnLoadin.Text = "Loadin"
        '
        'rbtnNonExcisable
        '
        Me.rbtnNonExcisable.Location = New System.Drawing.Point(174, 8)
        Me.rbtnNonExcisable.Name = "rbtnNonExcisable"
        Me.rbtnNonExcisable.Size = New System.Drawing.Size(134, 18)
        Me.rbtnNonExcisable.TabIndex = 1
        Me.rbtnNonExcisable.Text = "Non Excisable Loadout"
        '
        'rbtnExcisable
        '
        Me.rbtnExcisable.Location = New System.Drawing.Point(51, 8)
        Me.rbtnExcisable.Name = "rbtnExcisable"
        Me.rbtnExcisable.Size = New System.Drawing.Size(109, 18)
        Me.rbtnExcisable.TabIndex = 0
        Me.rbtnExcisable.TabStop = True
        Me.rbtnExcisable.Text = "Excisable Loadout"
        Me.rbtnExcisable.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(431, 49)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate.TabIndex = 2
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13-06-2011"
        Me.txtToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(97, 48)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 1
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13-06-2011"
        Me.txtFromDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(356, 50)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 10
        Me.RadLabel2.Text = "To Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(9, 49)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 10
        Me.RadLabel1.Text = "From Date"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(452, 350)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(9, 351)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 6
        Me.btnReset.Text = "Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(83, 351)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 5
        Me.btnPrint.Text = "Print"
        '
        'frmRptTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(541, 391)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "frmRptTransfer"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Transfer Report"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.grpExcisable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpExcisable.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkTransferSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTransferAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.rbtnLoadin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnNonExcisable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnExcisable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rbtnNonExcisable As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnExcisable As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnLoadin As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents grpExcisable As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkTransferSelect As common.Controls.MyRadioButton
    Friend WithEvents chkTransferAll As common.Controls.MyRadioButton
    Friend WithEvents cbg As common.MyCheckBoxGrid
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
End Class

