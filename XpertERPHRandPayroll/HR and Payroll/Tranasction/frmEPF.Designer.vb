Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEPF
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
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEPF))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtdocdate = New common.Controls.MyDateTimePicker()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.lblPayPeriodName = New common.Controls.MyLabel()
        Me.fndPayperiod = New common.UserControls.txtFinder()
        Me.txtRemaks = New common.Controls.MyTextBox()
        Me.lblRemarks = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnreverse = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdocdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemaks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreverse, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtdocdate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gv1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriodName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndPayperiod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRemaks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 415
        Me.SplitContainer1.TabIndex = 0
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 63)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(92, 16)
        Me.MyLabel1.TabIndex = 283
        Me.MyLabel1.Text = "Pay Period Code"
        '
        'txtdocdate
        '
        Me.txtdocdate.CalculationExpression = Nothing
        Me.txtdocdate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtdocdate.FieldCode = Nothing
        Me.txtdocdate.FieldDesc = Nothing
        Me.txtdocdate.FieldMaxLength = 0
        Me.txtdocdate.FieldName = Nothing
        Me.txtdocdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdocdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtdocdate.isCalculatedField = False
        Me.txtdocdate.IsSourceFromTable = False
        Me.txtdocdate.IsSourceFromValueList = False
        Me.txtdocdate.IsUnique = False
        Me.txtdocdate.Location = New System.Drawing.Point(370, 12)
        Me.txtdocdate.MendatroryField = False
        Me.txtdocdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdocdate.MyLinkLable1 = Nothing
        Me.txtdocdate.MyLinkLable2 = Nothing
        Me.txtdocdate.Name = "txtdocdate"
        Me.txtdocdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdocdate.ReferenceFieldDesc = Nothing
        Me.txtdocdate.ReferenceFieldName = Nothing
        Me.txtdocdate.ReferenceTableName = Nothing
        Me.txtdocdate.Size = New System.Drawing.Size(79, 18)
        Me.txtdocdate.TabIndex = 282
        Me.txtdocdate.TabStop = False
        Me.txtdocdate.Text = "18/05/2011 02:11 PM"
        Me.txtdocdate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'gv1
        '
        Me.gv1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(3, 151)
        '
        '
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AutoGenerateColumns = False
        Me.gv1.MasterTemplate.EnableGrouping = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(794, 261)
        Me.gv1.TabIndex = 281
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.Location = New System.Drawing.Point(378, 37)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(298, 19)
        Me.lblLocationDesc.TabIndex = 278
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(12, 37)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel9.TabIndex = 277
        Me.MyLabel9.Text = "Location"
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(129, 37)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.MyLabel9
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(246, 19)
        Me.txtLocation.TabIndex = 276
        Me.txtLocation.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(477, 12)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 18)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 275
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(352, 11)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 273
        Me.btnNew.Text = " "
        '
        'lblPayPeriodName
        '
        Me.lblPayPeriodName.AutoSize = False
        Me.lblPayPeriodName.BorderVisible = True
        Me.lblPayPeriodName.FieldName = Nothing
        Me.lblPayPeriodName.Location = New System.Drawing.Point(378, 60)
        Me.lblPayPeriodName.Name = "lblPayPeriodName"
        Me.lblPayPeriodName.Size = New System.Drawing.Size(298, 19)
        Me.lblPayPeriodName.TabIndex = 272
        '
        'fndPayperiod
        '
        Me.fndPayperiod.CalculationExpression = Nothing
        Me.fndPayperiod.FieldCode = Nothing
        Me.fndPayperiod.FieldDesc = Nothing
        Me.fndPayperiod.FieldMaxLength = 0
        Me.fndPayperiod.FieldName = Nothing
        Me.fndPayperiod.isCalculatedField = False
        Me.fndPayperiod.IsSourceFromTable = False
        Me.fndPayperiod.IsSourceFromValueList = False
        Me.fndPayperiod.IsUnique = False
        Me.fndPayperiod.Location = New System.Drawing.Point(129, 60)
        Me.fndPayperiod.MendatroryField = True
        Me.fndPayperiod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPayperiod.MyLinkLable1 = Nothing
        Me.fndPayperiod.MyLinkLable2 = Nothing
        Me.fndPayperiod.MyReadOnly = False
        Me.fndPayperiod.MyShowMasterFormButton = False
        Me.fndPayperiod.Name = "fndPayperiod"
        Me.fndPayperiod.ReferenceFieldDesc = Nothing
        Me.fndPayperiod.ReferenceFieldName = Nothing
        Me.fndPayperiod.ReferenceTableName = Nothing
        Me.fndPayperiod.Size = New System.Drawing.Size(246, 19)
        Me.fndPayperiod.TabIndex = 263
        Me.fndPayperiod.Value = ""
        '
        'txtRemaks
        '
        Me.txtRemaks.AutoSize = False
        Me.txtRemaks.CalculationExpression = Nothing
        Me.txtRemaks.FieldCode = Nothing
        Me.txtRemaks.FieldDesc = Nothing
        Me.txtRemaks.FieldMaxLength = 0
        Me.txtRemaks.FieldName = Nothing
        Me.txtRemaks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemaks.isCalculatedField = False
        Me.txtRemaks.IsSourceFromTable = False
        Me.txtRemaks.IsSourceFromValueList = False
        Me.txtRemaks.IsUnique = False
        Me.txtRemaks.Location = New System.Drawing.Point(129, 85)
        Me.txtRemaks.MaxLength = 49
        Me.txtRemaks.MendatroryField = True
        Me.txtRemaks.Multiline = True
        Me.txtRemaks.MyLinkLable1 = Me.lblRemarks
        Me.txtRemaks.MyLinkLable2 = Nothing
        Me.txtRemaks.Name = "txtRemaks"
        Me.txtRemaks.ReferenceFieldDesc = Nothing
        Me.txtRemaks.ReferenceFieldName = Nothing
        Me.txtRemaks.ReferenceTableName = Nothing
        Me.txtRemaks.Size = New System.Drawing.Size(547, 21)
        Me.txtRemaks.TabIndex = 266
        '
        'lblRemarks
        '
        Me.lblRemarks.FieldName = Nothing
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(12, 85)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(51, 16)
        Me.lblRemarks.TabIndex = 270
        Me.lblRemarks.Text = "Remarks"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(129, 11)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(221, 21)
        Me.txtCode.TabIndex = 262
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCode.Location = New System.Drawing.Point(12, 14)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(88, 16)
        Me.lblCode.TabIndex = 267
        Me.lblCode.Text = "Document Code"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(84, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 5
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(12, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 4
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(156, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 6
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(722, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 7
        Me.btnclose.Text = "Close"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem4, Me.RadMenuItem1})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        Me.RadMenuItem3.UseCompatibleTextRendering = False
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Export Blank Sheet"
        Me.RadMenuItem4.UseCompatibleTextRendering = False
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Import"
        Me.RadMenuItem1.UseCompatibleTextRendering = False
        '
        'btnreverse
        '
        Me.btnreverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnreverse.Location = New System.Drawing.Point(228, 6)
        Me.btnreverse.Name = "btnreverse"
        Me.btnreverse.Size = New System.Drawing.Size(153, 18)
        Me.btnreverse.TabIndex = 8
        Me.btnreverse.Text = "Reverse and Unpost"
        Me.btnreverse.Visible = False
        '
        'frmEPF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmEPF"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmEPF"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdocdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemaks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblPayPeriodName As common.Controls.MyLabel
    Friend WithEvents fndPayperiod As common.UserControls.txtFinder
    Friend WithEvents txtRemaks As common.Controls.MyTextBox
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtdocdate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnreverse As Telerik.WinControls.UI.RadButton
End Class
