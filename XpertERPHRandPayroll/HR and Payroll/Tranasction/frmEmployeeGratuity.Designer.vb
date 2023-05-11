Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEmployeeGratuity
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEmployeeGratuity))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.txtNOF = New common.MyNumBox
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.txtLastDrawnSalary = New common.MyNumBox
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.MyDateTimePicker2 = New common.Controls.MyDateTimePicker
        Me.MyDateTimePicker1 = New common.Controls.MyDateTimePicker
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.txtCode = New common.UserControls.txtNavigator
        Me.lblEmpName = New common.Controls.MyLabel
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.txtGratuity = New common.MyNumBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.lblOpeningDate = New common.Controls.MyLabel
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtNOF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLastDrawnSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyDateTimePicker2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyDateTimePicker1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGratuity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOpeningDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtNOF)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLastDrawnSalary)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyDateTimePicker2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyDateTimePicker1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmpName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtGratuity)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblOpeningDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(631, 209)
        Me.SplitContainer1.SplitterDistance = 169
        Me.SplitContainer1.TabIndex = 0
        '
        'txtNOF
        '
        Me.txtNOF.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtNOF.DecimalPlaces = 2
        Me.txtNOF.Location = New System.Drawing.Point(133, 106)
        Me.txtNOF.MaxLength = 6
        Me.txtNOF.MendatroryField = True
        Me.txtNOF.MyLinkLable1 = Me.MyLabel4
        Me.txtNOF.MyLinkLable2 = Nothing
        Me.txtNOF.Name = "txtNOF"
        Me.txtNOF.ReadOnly = True
        Me.txtNOF.Size = New System.Drawing.Size(197, 20)
        Me.txtNOF.TabIndex = 6
        Me.txtNOF.TabStop = False
        Me.txtNOF.Text = "0"
        Me.txtNOF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNOF.Value = 0
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(12, 35)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel4.TabIndex = 153
        Me.MyLabel4.Text = "Employee Code"
        '
        'txtLastDrawnSalary
        '
        Me.txtLastDrawnSalary.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtLastDrawnSalary.DecimalPlaces = 2
        Me.txtLastDrawnSalary.Location = New System.Drawing.Point(133, 82)
        Me.txtLastDrawnSalary.MaxLength = 6
        Me.txtLastDrawnSalary.MendatroryField = True
        Me.txtLastDrawnSalary.MyLinkLable1 = Me.MyLabel4
        Me.txtLastDrawnSalary.MyLinkLable2 = Nothing
        Me.txtLastDrawnSalary.Name = "txtLastDrawnSalary"
        Me.txtLastDrawnSalary.ReadOnly = True
        Me.txtLastDrawnSalary.Size = New System.Drawing.Size(197, 20)
        Me.txtLastDrawnSalary.TabIndex = 5
        Me.txtLastDrawnSalary.TabStop = False
        Me.txtLastDrawnSalary.Text = "0"
        Me.txtLastDrawnSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLastDrawnSalary.Value = 0
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(343, 64)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel5.TabIndex = 171
        Me.MyLabel5.Text = "DOL"
        '
        'MyDateTimePicker2
        '
        Me.MyDateTimePicker2.CustomFormat = "dd/MM/yyyy"
        Me.MyDateTimePicker2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyDateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.MyDateTimePicker2.Location = New System.Drawing.Point(398, 62)
        Me.MyDateTimePicker2.MendatroryField = False
        Me.MyDateTimePicker2.MinDate = New Date(1950, 1, 1, 0, 0, 0, 0)
        Me.MyDateTimePicker2.MyLinkLable1 = Nothing
        Me.MyDateTimePicker2.MyLinkLable2 = Nothing
        Me.MyDateTimePicker2.Name = "MyDateTimePicker2"
        Me.MyDateTimePicker2.NullDate = New Date(1950, 1, 1, 0, 0, 0, 0)
        Me.MyDateTimePicker2.ReadOnly = True
        Me.MyDateTimePicker2.Size = New System.Drawing.Size(197, 18)
        Me.MyDateTimePicker2.TabIndex = 4
        Me.MyDateTimePicker2.TabStop = False
        Me.MyDateTimePicker2.Text = "03/05/2011"
        Me.MyDateTimePicker2.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyDateTimePicker1
        '
        Me.MyDateTimePicker1.CustomFormat = "dd/MM/yyyy"
        Me.MyDateTimePicker1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.MyDateTimePicker1.Location = New System.Drawing.Point(133, 59)
        Me.MyDateTimePicker1.MendatroryField = False
        Me.MyDateTimePicker1.MinDate = New Date(1950, 1, 1, 0, 0, 0, 0)
        Me.MyDateTimePicker1.MyLinkLable1 = Nothing
        Me.MyDateTimePicker1.MyLinkLable2 = Nothing
        Me.MyDateTimePicker1.Name = "MyDateTimePicker1"
        Me.MyDateTimePicker1.NullDate = New Date(1950, 1, 1, 0, 0, 0, 0)
        Me.MyDateTimePicker1.ReadOnly = True
        Me.MyDateTimePicker1.Size = New System.Drawing.Size(197, 18)
        Me.MyDateTimePicker1.TabIndex = 3
        Me.MyDateTimePicker1.TabStop = False
        Me.MyDateTimePicker1.Text = "03/05/2011"
        Me.MyDateTimePicker1.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 62)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel1.TabIndex = 167
        Me.MyLabel1.Text = "DOJ"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(358, 33)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(133, 33)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.MyLabel4
        Me.txtCode.MyLinkLable2 = Me.lblEmpName
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(222, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.Location = New System.Drawing.Point(398, 33)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(195, 19)
        Me.lblEmpName.TabIndex = 2
        Me.lblEmpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(12, 132)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel3.TabIndex = 162
        Me.MyLabel3.Text = "Gratuity"
        '
        'txtGratuity
        '
        Me.txtGratuity.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtGratuity.DecimalPlaces = 2
        Me.txtGratuity.Location = New System.Drawing.Point(133, 130)
        Me.txtGratuity.MaxLength = 6
        Me.txtGratuity.MendatroryField = True
        Me.txtGratuity.MyLinkLable1 = Me.MyLabel4
        Me.txtGratuity.MyLinkLable2 = Nothing
        Me.txtGratuity.Name = "txtGratuity"
        Me.txtGratuity.ReadOnly = True
        Me.txtGratuity.Size = New System.Drawing.Size(197, 20)
        Me.txtGratuity.TabIndex = 7
        Me.txtGratuity.TabStop = False
        Me.txtGratuity.Text = "0"
        Me.txtGratuity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtGratuity.Value = 0
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(12, 85)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(99, 16)
        Me.MyLabel2.TabIndex = 147
        Me.MyLabel2.Text = "Last Drawn Salary"
        '
        'lblOpeningDate
        '
        Me.lblOpeningDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOpeningDate.Location = New System.Drawing.Point(12, 110)
        Me.lblOpeningDate.Name = "lblOpeningDate"
        Me.lblOpeningDate.Size = New System.Drawing.Size(66, 16)
        Me.lblOpeningDate.TabIndex = 155
        Me.lblOpeningDate.Text = "No of Years"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(147, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(66, 22)
        Me.btnPrint.TabIndex = 2
        Me.btnPrint.Text = "Print"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(75, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(3, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(558, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 22)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'FrmEmployeeGratuity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(631, 209)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmEmployeeGratuity"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmEmployeeGratuity"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtNOF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLastDrawnSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyDateTimePicker2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyDateTimePicker1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGratuity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOpeningDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtGratuity As common.MyNumBox
    Friend WithEvents lblOpeningDate As common.Controls.MyLabel
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyDateTimePicker1 As common.Controls.MyDateTimePicker
    Friend WithEvents MyDateTimePicker2 As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtLastDrawnSalary As common.MyNumBox
    Friend WithEvents txtNOF As common.MyNumBox
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
End Class

