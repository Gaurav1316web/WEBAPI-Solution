Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class rptLoanstatement
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.fndBankCode = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblBankCode = New common.Controls.MyLabel()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.lblPayPeriodName = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.findPayperiod = New common.UserControls.txtFinder()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnprint = New Telerik.WinControls.UI.RadButton()
        Me.btngo = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btngo)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 418
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(800, 418)
        Me.RadPageView1.TabIndex = 1
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.fndBankCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblBankCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocationName)
        Me.RadPageViewPage1.Controls.Add(Me.lblPayPeriodName)
        Me.RadPageViewPage1.Controls.Add(Me.fndLocation)
        Me.RadPageViewPage1.Controls.Add(Me.findPayperiod)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 370)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'fndBankCode
        '
        Me.fndBankCode.CalculationExpression = Nothing
        Me.fndBankCode.FieldCode = Nothing
        Me.fndBankCode.FieldDesc = Nothing
        Me.fndBankCode.FieldMaxLength = 0
        Me.fndBankCode.FieldName = Nothing
        Me.fndBankCode.isCalculatedField = False
        Me.fndBankCode.IsSourceFromTable = False
        Me.fndBankCode.IsSourceFromValueList = False
        Me.fndBankCode.IsUnique = False
        Me.fndBankCode.Location = New System.Drawing.Point(115, 68)
        Me.fndBankCode.MendatroryField = True
        Me.fndBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBankCode.MyLinkLable1 = Me.lblLocation
        Me.fndBankCode.MyLinkLable2 = Nothing
        Me.fndBankCode.MyReadOnly = False
        Me.fndBankCode.MyShowMasterFormButton = False
        Me.fndBankCode.Name = "fndBankCode"
        Me.fndBankCode.ReferenceFieldDesc = Nothing
        Me.fndBankCode.ReferenceFieldName = Nothing
        Me.fndBankCode.ReferenceTableName = Nothing
        Me.fndBankCode.Size = New System.Drawing.Size(221, 19)
        Me.fndBankCode.TabIndex = 356
        Me.fndBankCode.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(17, 42)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 344
        Me.lblLocation.Text = "Location"
        '
        'lblBankCode
        '
        Me.lblBankCode.AutoSize = False
        Me.lblBankCode.BorderVisible = True
        Me.lblBankCode.FieldName = Nothing
        Me.lblBankCode.Location = New System.Drawing.Point(342, 68)
        Me.lblBankCode.Name = "lblBankCode"
        Me.lblBankCode.Size = New System.Drawing.Size(222, 19)
        Me.lblBankCode.TabIndex = 355
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Location = New System.Drawing.Point(342, 42)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(222, 19)
        Me.lblLocationName.TabIndex = 354
        '
        'lblPayPeriodName
        '
        Me.lblPayPeriodName.AutoSize = False
        Me.lblPayPeriodName.BorderVisible = True
        Me.lblPayPeriodName.FieldName = Nothing
        Me.lblPayPeriodName.Location = New System.Drawing.Point(342, 18)
        Me.lblPayPeriodName.Name = "lblPayPeriodName"
        Me.lblPayPeriodName.Size = New System.Drawing.Size(222, 19)
        Me.lblPayPeriodName.TabIndex = 353
        '
        'fndLocation
        '
        Me.fndLocation.CalculationExpression = Nothing
        Me.fndLocation.FieldCode = Nothing
        Me.fndLocation.FieldDesc = Nothing
        Me.fndLocation.FieldMaxLength = 0
        Me.fndLocation.FieldName = Nothing
        Me.fndLocation.isCalculatedField = False
        Me.fndLocation.IsSourceFromTable = False
        Me.fndLocation.IsSourceFromValueList = False
        Me.fndLocation.IsUnique = False
        Me.fndLocation.Location = New System.Drawing.Point(115, 43)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.lblLocation
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(221, 19)
        Me.fndLocation.TabIndex = 352
        Me.fndLocation.Value = ""
        '
        'findPayperiod
        '
        Me.findPayperiod.CalculationExpression = Nothing
        Me.findPayperiod.FieldCode = Nothing
        Me.findPayperiod.FieldDesc = Nothing
        Me.findPayperiod.FieldMaxLength = 0
        Me.findPayperiod.FieldName = Nothing
        Me.findPayperiod.isCalculatedField = False
        Me.findPayperiod.IsSourceFromTable = False
        Me.findPayperiod.IsSourceFromValueList = False
        Me.findPayperiod.IsUnique = False
        Me.findPayperiod.Location = New System.Drawing.Point(115, 18)
        Me.findPayperiod.MendatroryField = True
        Me.findPayperiod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findPayperiod.MyLinkLable1 = Nothing
        Me.findPayperiod.MyLinkLable2 = Nothing
        Me.findPayperiod.MyReadOnly = False
        Me.findPayperiod.MyShowMasterFormButton = False
        Me.findPayperiod.Name = "findPayperiod"
        Me.findPayperiod.ReferenceFieldDesc = Nothing
        Me.findPayperiod.ReferenceFieldName = Nothing
        Me.findPayperiod.ReferenceTableName = Nothing
        Me.findPayperiod.Size = New System.Drawing.Size(221, 19)
        Me.findPayperiod.TabIndex = 351
        Me.findPayperiod.Value = ""
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(17, 18)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(92, 16)
        Me.RadLabel2.TabIndex = 350
        Me.RadLabel2.Text = "Pay Period Code"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(17, 68)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(60, 18)
        Me.MyLabel2.TabIndex = 346
        Me.MyLabel2.Text = "Bank Code"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(779, 370)
        Me.RadPageViewPage2.Text = "Report"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gv1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = " "
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(779, 370)
        Me.RadGroupBox1.TabIndex = 1
        Me.RadGroupBox1.Text = " "
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowEditRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(759, 340)
        Me.gv1.TabIndex = 146
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnclose.Location = New System.Drawing.Point(714, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(77, 21)
        Me.btnclose.TabIndex = 342
        Me.btnclose.Text = "Close"
        '
        'btnprint
        '
        Me.btnprint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnprint.Location = New System.Drawing.Point(96, 3)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(77, 21)
        Me.btnprint.TabIndex = 341
        Me.btnprint.Text = "Print"
        '
        'btngo
        '
        Me.btngo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btngo.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btngo.Location = New System.Drawing.Point(13, 3)
        Me.btngo.Name = "btngo"
        Me.btngo.Size = New System.Drawing.Size(77, 21)
        Me.btngo.TabIndex = 340
        Me.btngo.Text = "<<<"
        '
        'rptLoanstatement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "rptLoanstatement"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "rptLoanstatement"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btngo As Telerik.WinControls.UI.RadButton
    Friend WithEvents findPayperiod As common.UserControls.txtFinder
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents fndBankCode As common.UserControls.txtFinder
    Friend WithEvents lblBankCode As common.Controls.MyLabel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents lblPayPeriodName As common.Controls.MyLabel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
End Class
