<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmZoneMaster
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblRSM = New common.Controls.MyLabel()
        Me.fndRSM = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblCity = New common.Controls.MyLabel()
        Me.txtCity = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lbldesid = New common.Controls.MyLabel()
        Me.txtdes = New common.Controls.MyTextBox()
        Me.lbldes = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.Import = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export = New Telerik.WinControls.UI.RadMenuItem()
        Me.txtdesHindi = New common.Controls.MyTextBox()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblRSM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldesid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesHindi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtdesHindi)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRSM)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndRSM)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCity)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCity)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbldesid)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtdes)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbldes)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(685, 135)
        Me.SplitContainer1.SplitterDistance = 103
        Me.SplitContainer1.TabIndex = 0
        '
        'lblRSM
        '
        Me.lblRSM.AutoSize = False
        Me.lblRSM.BorderVisible = True
        Me.lblRSM.FieldName = Nothing
        Me.lblRSM.Location = New System.Drawing.Point(198, 77)
        Me.lblRSM.Name = "lblRSM"
        Me.lblRSM.Size = New System.Drawing.Size(181, 18)
        Me.lblRSM.TabIndex = 29
        '
        'fndRSM
        '
        Me.fndRSM.CalculationExpression = Nothing
        Me.fndRSM.FieldCode = Nothing
        Me.fndRSM.FieldDesc = Nothing
        Me.fndRSM.FieldMaxLength = 0
        Me.fndRSM.FieldName = Nothing
        Me.fndRSM.isCalculatedField = False
        Me.fndRSM.IsSourceFromTable = False
        Me.fndRSM.IsSourceFromValueList = False
        Me.fndRSM.IsUnique = False
        Me.fndRSM.Location = New System.Drawing.Point(80, 76)
        Me.fndRSM.MendatroryField = True
        Me.fndRSM.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRSM.MyLinkLable1 = Me.lblRSM
        Me.fndRSM.MyLinkLable2 = Me.MyLabel2
        Me.fndRSM.MyReadOnly = False
        Me.fndRSM.MyShowMasterFormButton = False
        Me.fndRSM.Name = "fndRSM"
        Me.fndRSM.ReferenceFieldDesc = Nothing
        Me.fndRSM.ReferenceFieldName = Nothing
        Me.fndRSM.ReferenceTableName = Nothing
        Me.fndRSM.Size = New System.Drawing.Size(114, 18)
        Me.fndRSM.TabIndex = 28
        Me.fndRSM.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(9, 76)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel2.TabIndex = 30
        Me.MyLabel2.Text = "RSM"
        '
        'lblCity
        '
        Me.lblCity.AutoSize = False
        Me.lblCity.BorderVisible = True
        Me.lblCity.FieldName = Nothing
        Me.lblCity.Location = New System.Drawing.Point(198, 55)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(181, 18)
        Me.lblCity.TabIndex = 26
        '
        'txtCity
        '
        Me.txtCity.CalculationExpression = Nothing
        Me.txtCity.FieldCode = Nothing
        Me.txtCity.FieldDesc = Nothing
        Me.txtCity.FieldMaxLength = 0
        Me.txtCity.FieldName = Nothing
        Me.txtCity.isCalculatedField = False
        Me.txtCity.IsSourceFromTable = False
        Me.txtCity.IsSourceFromValueList = False
        Me.txtCity.IsUnique = False
        Me.txtCity.Location = New System.Drawing.Point(80, 54)
        Me.txtCity.MendatroryField = True
        Me.txtCity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCity.MyLinkLable1 = Me.lblCity
        Me.txtCity.MyLinkLable2 = Me.MyLabel3
        Me.txtCity.MyReadOnly = False
        Me.txtCity.MyShowMasterFormButton = False
        Me.txtCity.Name = "txtCity"
        Me.txtCity.ReferenceFieldDesc = Nothing
        Me.txtCity.ReferenceFieldName = Nothing
        Me.txtCity.ReferenceTableName = Nothing
        Me.txtCity.Size = New System.Drawing.Size(114, 18)
        Me.txtCity.TabIndex = 25
        Me.txtCity.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(9, 54)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(25, 18)
        Me.MyLabel3.TabIndex = 27
        Me.MyLabel3.Text = "City"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(80, 8)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Nothing
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(202, 21)
        Me.txtCode.TabIndex = 19
        Me.txtCode.Value = ""
        '
        'lbldesid
        '
        Me.lbldesid.FieldName = Nothing
        Me.lbldesid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldesid.Location = New System.Drawing.Point(9, 10)
        Me.lbldesid.Name = "lbldesid"
        Me.lbldesid.Size = New System.Drawing.Size(32, 16)
        Me.lbldesid.TabIndex = 23
        Me.lbldesid.Text = "Zone"
        '
        'txtdes
        '
        Me.txtdes.CalculationExpression = Nothing
        Me.txtdes.FieldCode = Nothing
        Me.txtdes.FieldDesc = Nothing
        Me.txtdes.FieldMaxLength = 0
        Me.txtdes.FieldName = Nothing
        Me.txtdes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdes.isCalculatedField = False
        Me.txtdes.IsSourceFromTable = False
        Me.txtdes.IsSourceFromValueList = False
        Me.txtdes.IsUnique = False
        Me.txtdes.Location = New System.Drawing.Point(80, 32)
        Me.txtdes.MaxLength = 150
        Me.txtdes.MendatroryField = False
        Me.txtdes.MyLinkLable1 = Nothing
        Me.txtdes.MyLinkLable2 = Nothing
        Me.txtdes.Name = "txtdes"
        Me.txtdes.ReferenceFieldDesc = Nothing
        Me.txtdes.ReferenceFieldName = Nothing
        Me.txtdes.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtdes.RootElement.StretchVertically = True
        Me.txtdes.Size = New System.Drawing.Size(297, 19)
        Me.txtdes.TabIndex = 21
        '
        'lbldes
        '
        Me.lbldes.FieldName = Nothing
        Me.lbldes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldes.Location = New System.Drawing.Point(9, 33)
        Me.lbldes.Name = "lbldes"
        Me.lbldes.Size = New System.Drawing.Size(63, 16)
        Me.lbldes.TabIndex = 22
        Me.lbldes.Text = "Description"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(282, 8)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(16, 21)
        Me.btnnew.TabIndex = 20
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(12, 2)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 6
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(91, 2)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 7
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(606, 2)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 8
        Me.btnclose.Text = "Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(685, 20)
        Me.RadMenu1.TabIndex = 2
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Import, Me.Export})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'Import
        '
        Me.Import.Name = "Import"
        Me.Import.Text = "Import"
        '
        'Export
        '
        Me.Export.Name = "Export"
        Me.Export.Text = "Export"
        '
        'txtdesHindi
        '
        Me.txtdesHindi.CalculationExpression = Nothing
        Me.txtdesHindi.FieldCode = Nothing
        Me.txtdesHindi.FieldDesc = Nothing
        Me.txtdesHindi.FieldMaxLength = 0
        Me.txtdesHindi.FieldName = Nothing
        Me.txtdesHindi.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdesHindi.isCalculatedField = False
        Me.txtdesHindi.IsSourceFromTable = False
        Me.txtdesHindi.IsSourceFromValueList = False
        Me.txtdesHindi.IsUnique = False
        Me.txtdesHindi.Location = New System.Drawing.Point(383, 32)
        Me.txtdesHindi.MaxLength = 150
        Me.txtdesHindi.MendatroryField = False
        Me.txtdesHindi.MyLinkLable1 = Nothing
        Me.txtdesHindi.MyLinkLable2 = Nothing
        Me.txtdesHindi.Name = "txtdesHindi"
        Me.txtdesHindi.ReferenceFieldDesc = Nothing
        Me.txtdesHindi.ReferenceFieldName = Nothing
        Me.txtdesHindi.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtdesHindi.RootElement.StretchVertically = True
        Me.txtdesHindi.Size = New System.Drawing.Size(297, 19)
        Me.txtdesHindi.TabIndex = 31
        '
        'FrmZoneMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(685, 155)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmZoneMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Zone Master"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblRSM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldesid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesHindi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lbldesid As common.Controls.MyLabel
    Friend WithEvents txtdes As common.Controls.MyTextBox
    Friend WithEvents lbldes As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblCity As common.Controls.MyLabel
    Friend WithEvents txtCity As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblRSM As common.Controls.MyLabel
    Friend WithEvents fndRSM As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtdesHindi As common.Controls.MyTextBox
End Class

