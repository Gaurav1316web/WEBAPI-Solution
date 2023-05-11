Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmHireEmployee
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.UsLock1 = New common.usLock
        Me.txtAccNo = New common.MyNumBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.MyLabel27 = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtMonths = New common.MyNumBox
        Me.txtDays = New common.MyNumBox
        Me.MyLabel29 = New common.Controls.MyLabel
        Me.lblBankName = New common.Controls.MyLabel
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.TxtIFSCCode = New common.Controls.MyTextBox
        Me.txtRemarks = New common.Controls.MyTextBox
        Me.MyLabel35 = New common.Controls.MyLabel
        Me.txtPanNo = New common.Controls.MyTextBox
        Me.MyLabel41 = New common.Controls.MyLabel
        Me.txtBank = New common.UserControls.txtFinder
        Me.MyLabel13 = New common.Controls.MyLabel
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.txtAppcode = New common.UserControls.txtNavigator
        Me.UcRequisitionDetail1 = New XpertERPHRandPayroll.ucRequisitionDetail
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnpost = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtAccNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMonths, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtIFSCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAccNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBankName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtIFSCCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel35)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPanNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel41)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBank)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel13)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAppcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UcRequisitionDetail1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnpost)
        Me.SplitContainer1.Size = New System.Drawing.Size(717, 305)
        Me.SplitContainer1.SplitterDistance = 269
        Me.SplitContainer1.TabIndex = 0
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(611, 14)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 141
        '
        'txtAccNo
        '
        Me.txtAccNo.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtAccNo.DecimalPlaces = 0
        Me.txtAccNo.Location = New System.Drawing.Point(108, 152)
        Me.txtAccNo.MaxLength = 18
        Me.txtAccNo.MendatroryField = True
        Me.txtAccNo.MyLinkLable1 = Nothing
        Me.txtAccNo.MyLinkLable2 = Nothing
        Me.txtAccNo.Name = "txtAccNo"
        Me.txtAccNo.Size = New System.Drawing.Size(220, 20)
        Me.txtAccNo.TabIndex = 140
        Me.txtAccNo.Text = "0"
        Me.txtAccNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAccNo.Value = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.MyLabel27)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.txtMonths)
        Me.Panel1.Controls.Add(Me.txtDays)
        Me.Panel1.Controls.Add(Me.MyLabel29)
        Me.Panel1.Location = New System.Drawing.Point(11, 218)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(317, 26)
        Me.Panel1.TabIndex = 7
        '
        'MyLabel27
        '
        Me.MyLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel27.Location = New System.Drawing.Point(4, 5)
        Me.MyLabel27.Name = "MyLabel27"
        Me.MyLabel27.Size = New System.Drawing.Size(97, 16)
        Me.MyLabel27.TabIndex = 148
        Me.MyLabel27.Text = "Probation Period :"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(113, 6)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel2.TabIndex = 147
        Me.MyLabel2.Text = "Months"
        '
        'txtMonths
        '
        Me.txtMonths.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMonths.DecimalPlaces = 0
        Me.txtMonths.Location = New System.Drawing.Point(158, 2)
        Me.txtMonths.MaxLength = 12
        Me.txtMonths.MendatroryField = True
        Me.txtMonths.MyLinkLable1 = Nothing
        Me.txtMonths.MyLinkLable2 = Nothing
        Me.txtMonths.Name = "txtMonths"
        Me.txtMonths.Size = New System.Drawing.Size(43, 20)
        Me.txtMonths.TabIndex = 8
        Me.txtMonths.Text = "0"
        Me.txtMonths.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMonths.Value = 0
        '
        'txtDays
        '
        Me.txtDays.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtDays.DecimalPlaces = 0
        Me.txtDays.Location = New System.Drawing.Point(269, 3)
        Me.txtDays.MendatroryField = False
        Me.txtDays.MyLinkLable1 = Nothing
        Me.txtDays.MyLinkLable2 = Nothing
        Me.txtDays.Name = "txtDays"
        Me.txtDays.Size = New System.Drawing.Size(43, 20)
        Me.txtDays.TabIndex = 9
        Me.txtDays.Text = "0"
        Me.txtDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDays.Value = 0
        '
        'MyLabel29
        '
        Me.MyLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel29.Location = New System.Drawing.Point(233, 5)
        Me.MyLabel29.Name = "MyLabel29"
        Me.MyLabel29.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel29.TabIndex = 145
        Me.MyLabel29.Text = "Days"
        '
        'lblBankName
        '
        Me.lblBankName.AutoSize = False
        Me.lblBankName.BorderVisible = True
        Me.lblBankName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankName.Location = New System.Drawing.Point(337, 131)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(348, 18)
        Me.lblBankName.TabIndex = 139
        Me.lblBankName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBankName.TextWrap = False
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(13, 154)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel1.TabIndex = 137
        Me.MyLabel1.Text = "A/C No."
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(375, 154)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel5.TabIndex = 138
        Me.MyLabel5.Text = "IFSC Code"
        '
        'TxtIFSCCode
        '
        Me.TxtIFSCCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIFSCCode.Location = New System.Drawing.Point(447, 153)
        Me.TxtIFSCCode.MaxLength = 11
        Me.TxtIFSCCode.MendatroryField = True
        Me.TxtIFSCCode.MyLinkLable1 = Me.MyLabel5
        Me.TxtIFSCCode.MyLinkLable2 = Nothing
        Me.TxtIFSCCode.Name = "TxtIFSCCode"
        Me.TxtIFSCCode.Size = New System.Drawing.Size(238, 18)
        Me.TxtIFSCCode.TabIndex = 4
        '
        'txtRemarks
        '
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(108, 196)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.MyLabel35
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(577, 18)
        Me.txtRemarks.TabIndex = 6
        '
        'MyLabel35
        '
        Me.MyLabel35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel35.Location = New System.Drawing.Point(13, 197)
        Me.MyLabel35.Name = "MyLabel35"
        Me.MyLabel35.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel35.TabIndex = 134
        Me.MyLabel35.Text = "Remarks"
        '
        'txtPanNo
        '
        Me.txtPanNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPanNo.Location = New System.Drawing.Point(108, 175)
        Me.txtPanNo.MaxLength = 20
        Me.txtPanNo.MendatroryField = False
        Me.txtPanNo.MyLinkLable1 = Me.MyLabel41
        Me.txtPanNo.MyLinkLable2 = Nothing
        Me.txtPanNo.Name = "txtPanNo"
        Me.txtPanNo.Size = New System.Drawing.Size(220, 18)
        Me.txtPanNo.TabIndex = 5
        '
        'MyLabel41
        '
        Me.MyLabel41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel41.Location = New System.Drawing.Point(13, 175)
        Me.MyLabel41.Name = "MyLabel41"
        Me.MyLabel41.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel41.TabIndex = 133
        Me.MyLabel41.Text = "Pan No"
        '
        'txtBank
        '
        Me.txtBank.Location = New System.Drawing.Point(108, 131)
        Me.txtBank.MendatroryField = True
        Me.txtBank.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBank.MyLinkLable1 = Me.MyLabel13
        Me.txtBank.MyLinkLable2 = Nothing
        Me.txtBank.MyReadOnly = False
        Me.txtBank.MyShowMasterFormButton = False
        Me.txtBank.Name = "txtBank"
        Me.txtBank.Size = New System.Drawing.Size(219, 19)
        Me.txtBank.TabIndex = 2
        Me.txtBank.Value = ""
        '
        'MyLabel13
        '
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel13.Location = New System.Drawing.Point(13, 132)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel13.TabIndex = 130
        Me.MyLabel13.Text = "Bank Name"
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(11, 14)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(83, 16)
        Me.MyLabel3.TabIndex = 128
        Me.MyLabel3.Text = "Applicant Code"
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(304, 12)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(14, 21)
        Me.btnnew.TabIndex = 1
        '
        'txtAppcode
        '
        Me.txtAppcode.Location = New System.Drawing.Point(102, 12)
        Me.txtAppcode.MendatroryField = True
        Me.txtAppcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAppcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtAppcode.MyLinkLable1 = Nothing
        Me.txtAppcode.MyLinkLable2 = Nothing
        Me.txtAppcode.MyMaxLength = 30
        Me.txtAppcode.MyReadOnly = False
        Me.txtAppcode.Name = "txtAppcode"
        Me.txtAppcode.Size = New System.Drawing.Size(202, 21)
        Me.txtAppcode.TabIndex = 0
        Me.txtAppcode.TabStop = False
        Me.txtAppcode.Value = ""
        '
        'UcRequisitionDetail1
        '
        Me.UcRequisitionDetail1.AppCode = ""
        Me.UcRequisitionDetail1.AppDate = ""
        Me.UcRequisitionDetail1.AppName = ""
        Me.UcRequisitionDetail1.DateofBirth = ""
        Me.UcRequisitionDetail1.Email = ""
        Me.UcRequisitionDetail1.Location = New System.Drawing.Point(5, 33)
        Me.UcRequisitionDetail1.Name = "UcRequisitionDetail1"
        Me.UcRequisitionDetail1.ReqCode = ""
        Me.UcRequisitionDetail1.Size = New System.Drawing.Size(704, 93)
        Me.UcRequisitionDetail1.TabIndex = 127
        Me.UcRequisitionDetail1.TelephoneNo = ""
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(639, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 12
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(13, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 10
        Me.btnsave.Text = "Save"
        '
        'btnpost
        '
        Me.btnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnpost.Location = New System.Drawing.Point(83, 8)
        Me.btnpost.Name = "btnpost"
        Me.btnpost.Size = New System.Drawing.Size(66, 18)
        Me.btnpost.TabIndex = 11
        Me.btnpost.Text = "Post"
        '
        'FrmHireEmployee
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(717, 305)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmHireEmployee"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Hire Employee"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtAccNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMonths, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtIFSCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtAppcode As common.UserControls.txtNavigator
    Friend WithEvents UcRequisitionDetail1 As XpertERPHRandPayroll.ucRequisitionDetail
    Friend WithEvents txtBank As common.UserControls.txtFinder
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents MyLabel35 As common.Controls.MyLabel
    Friend WithEvents txtPanNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel41 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents TxtIFSCCode As common.Controls.MyTextBox
    Friend WithEvents lblBankName As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents MyLabel27 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtMonths As common.MyNumBox
    Friend WithEvents txtDays As common.MyNumBox
    Friend WithEvents MyLabel29 As common.Controls.MyLabel
    Friend WithEvents txtAccNo As common.MyNumBox
    Friend WithEvents UsLock1 As common.usLock
End Class

