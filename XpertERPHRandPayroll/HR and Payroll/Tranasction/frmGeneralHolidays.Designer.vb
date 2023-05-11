Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGeneralHolidays
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGeneralHolidays))
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblAttendName = New common.Controls.MyLabel()
        Me.txtAttendCode = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.chkIsNationalNational = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblcardno = New common.Controls.MyLabel()
        Me.dtpHolidayDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.lblDivisionName = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.lblLocationCode = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.FndDivision = New common.UserControls.txtFinder()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblAttendName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsNationalNational, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcardno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpHolidayDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivisionName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.SplitContainer1)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(713, 454)
        Me.RadGroupBox3.TabIndex = 60
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(10, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.FndDivision)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDivisionName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAttendName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAttendCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkIsNationalNational)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblcardno)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpHolidayDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(693, 424)
        Me.SplitContainer1.SplitterDistance = 385
        Me.SplitContainer1.TabIndex = 0
        '
        'lblAttendName
        '
        Me.lblAttendName.AutoSize = False
        Me.lblAttendName.BorderVisible = True
        Me.lblAttendName.Location = New System.Drawing.Point(332, 47)
        Me.lblAttendName.Name = "lblAttendName"
        Me.lblAttendName.Size = New System.Drawing.Size(159, 19)
        Me.lblAttendName.TabIndex = 3
        Me.lblAttendName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAttendCode
        '
        Me.txtAttendCode.Location = New System.Drawing.Point(135, 47)
        Me.txtAttendCode.MendatroryField = True
        Me.txtAttendCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAttendCode.MyLinkLable1 = Me.MyLabel4
        Me.txtAttendCode.MyLinkLable2 = Me.lblAttendName
        Me.txtAttendCode.MyReadOnly = False
        Me.txtAttendCode.MyShowMasterFormButton = False
        Me.txtAttendCode.Name = "txtAttendCode"
        Me.txtAttendCode.Size = New System.Drawing.Size(195, 19)
        Me.txtAttendCode.TabIndex = 2
        Me.txtAttendCode.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(11, 50)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel4.TabIndex = 153
        Me.MyLabel4.Text = "Attendance"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(361, 18)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(135, 18)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(222, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(11, 19)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 157
        Me.RadLabel1.Text = "Code"
        '
        'chkIsNationalNational
        '
        Me.chkIsNationalNational.Location = New System.Drawing.Point(249, 96)
        Me.chkIsNationalNational.Name = "chkIsNationalNational"
        Me.chkIsNationalNational.Size = New System.Drawing.Size(115, 18)
        Me.chkIsNationalNational.TabIndex = 6
        Me.chkIsNationalNational.Text = "Is National Holiday"
        '
        'txtDescription
        '
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(135, 72)
        Me.txtDescription.MaxLength = 49
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.lblcardno
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(356, 18)
        Me.txtDescription.TabIndex = 4
        '
        'lblcardno
        '
        Me.lblcardno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcardno.Location = New System.Drawing.Point(11, 72)
        Me.lblcardno.Name = "lblcardno"
        Me.lblcardno.Size = New System.Drawing.Size(63, 16)
        Me.lblcardno.TabIndex = 155
        Me.lblcardno.Text = "Description"
        '
        'dtpHolidayDate
        '
        Me.dtpHolidayDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpHolidayDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpHolidayDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpHolidayDate.Location = New System.Drawing.Point(135, 96)
        Me.dtpHolidayDate.MendatroryField = False
        Me.dtpHolidayDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpHolidayDate.MyLinkLable1 = Me.MyLabel2
        Me.dtpHolidayDate.MyLinkLable2 = Nothing
        Me.dtpHolidayDate.Name = "dtpHolidayDate"
        Me.dtpHolidayDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpHolidayDate.Size = New System.Drawing.Size(108, 18)
        Me.dtpHolidayDate.TabIndex = 5
        Me.dtpHolidayDate.TabStop = False
        Me.dtpHolidayDate.Text = "03/May/2011"
        Me.dtpHolidayDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(11, 95)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel2.TabIndex = 147
        Me.MyLabel2.Text = "Holiday Date"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(618, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(81, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'lblDivisionName
        '
        Me.lblDivisionName.AutoSize = False
        Me.lblDivisionName.BorderVisible = True
        Me.lblDivisionName.Location = New System.Drawing.Point(332, 137)
        Me.lblDivisionName.Name = "lblDivisionName"
        Me.lblDivisionName.Size = New System.Drawing.Size(159, 19)
        Me.lblDivisionName.TabIndex = 208
        Me.lblDivisionName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(11, 139)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel5.TabIndex = 207
        Me.MyLabel5.Text = "Division Code"
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.Location = New System.Drawing.Point(332, 115)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(159, 19)
        Me.lblLocationName.TabIndex = 206
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLocationCode
        '
        Me.lblLocationCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationCode.Location = New System.Drawing.Point(11, 117)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(79, 16)
        Me.lblLocationCode.TabIndex = 205
        Me.lblLocationCode.Text = "Location Code"
        '
        'fndLocation
        '
        Me.fndLocation.Location = New System.Drawing.Point(135, 117)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Nothing
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.Size = New System.Drawing.Size(195, 19)
        Me.fndLocation.TabIndex = 209
        Me.fndLocation.Value = ""
        '
        'FndDivision
        '
        Me.FndDivision.Location = New System.Drawing.Point(135, 139)
        Me.FndDivision.MendatroryField = True
        Me.FndDivision.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndDivision.MyLinkLable1 = Nothing
        Me.FndDivision.MyLinkLable2 = Nothing
        Me.FndDivision.MyReadOnly = False
        Me.FndDivision.MyShowMasterFormButton = False
        Me.FndDivision.Name = "FndDivision"
        Me.FndDivision.Size = New System.Drawing.Size(195, 19)
        Me.FndDivision.TabIndex = 210
        Me.FndDivision.Value = ""
        '
        'frmGeneralHolidays
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(713, 454)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Name = "frmGeneralHolidays"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "General Holidays"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblAttendName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsNationalNational, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcardno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpHolidayDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivisionName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpHolidayDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblcardno As common.Controls.MyLabel
    Friend WithEvents chkIsNationalNational As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblAttendName As common.Controls.MyLabel
    Friend WithEvents txtAttendCode As common.UserControls.txtFinder
    Friend WithEvents FndDivision As common.UserControls.txtFinder
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents lblDivisionName As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents lblLocationCode As common.Controls.MyLabel
End Class
