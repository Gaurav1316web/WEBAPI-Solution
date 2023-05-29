Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports common
'Imports XpertERPHRandPayroll
Public Class frmNewDCSScreen
    Inherits FrmMainTranScreen


#Region "Variables"
    Dim userCode, companyCode As String
#End Region
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub



    Private Sub txtSupervisorCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        Try
            Dim qry As String = " select EMP_CODE as Code, Emp_Name  as Name from TSPL_EMPLOYEE_MASTER  "
            'Emp_type = 'Salesman' and Emp_Status = 'Active'
            'txtSupervisorCode.Value = clsCommon.ShowSelectForm("NDSSSupervisor", qry, "Code", "Emp_Status = 'Active'", txtSupervisorCode.Value, "Code", isButtonClicked)
            'txtSupervisorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  Emp_Name   from TSPL_EMPLOYEE_MASTER where EMP_CODE = '" + txtSupervisorCode.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub txtDistrict__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)

    End Sub

    Private Sub txtBlock__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        Try
            Dim qry As String = "select Block_Code As Code, Block_Name As Name from TSPL_BLOCK_MASTER "
            ' txtBlock.Value = clsCommon.ShowSelectForm("NDSSBlock", qry, "Code", "", txtDistrict.Value, "Code", isButtonClicked)
            'txtBlockName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Block_Name from TSPL_BLOCK_MASTER= '" + txtBlock.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub txtZone__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)

    End Sub

    Private Sub txtRevenueVillage__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)

    End Sub

    Private Sub txtGramPanchayat__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        Try
            Dim qry As String = " select GRAMPANCHAYAT_CODE as Code, GRAMPANCHAYAT_NAME as Name from TSPL_GRAMPANCHAYAT_MASTER  "
            ' txtGramPanchayat.Value = clsCommon.ShowSelectForm("DCS@Grampanchayat@Finder", qry, "Code", "", txtGramPanchayat.Value, "", isButtonClicked)
            ' txtGramPnchayatName.Text = clsGrampanchayatMaster.GetName(txtGramPanchayat.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub txtPanchayatSamiti__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)

    End Sub

    Private Sub txtVidhanSabha__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)

    End Sub

    Private Sub txtCompanyCode1__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        Try
            ' txtCompanyCode1.Value = clsBankMaster.getFinder("", txtCompanyCode1.Value, isButtonClicked)
            ' txtCompanyBankName1.Text = clsBankMaster.GetName(txtCompanyCode1.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCompanyCode2__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        Try
            ' txtCompanyCode2.Value = clsBankMaster.getFinder("", txtCompanyCode2.Value, isButtonClicked)
            ' txtCompanyBankName2.Text = clsBankMaster.GetName(txtCompanyCode2.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub InitializeComponent()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
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
        Me.SplitContainer1.Size = New System.Drawing.Size(1137, 524)
        Me.SplitContainer1.SplitterDistance = 479
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Location = New System.Drawing.Point(12, 57)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1113, 410)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ShowItemCloseButton = False
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(115.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1092, 362)
        Me.RadPageViewPage1.Text = "RadPageViewPage1"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(115.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1092, 362)
        Me.RadPageViewPage2.Text = "RadPageViewPage2"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(115.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(1092, 362)
        Me.RadPageViewPage3.Text = "RadPageViewPage3"
        '
        'frmNewDCSScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1137, 524)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmNewDCSScreen"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Try
            Me.Close()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub


End Class