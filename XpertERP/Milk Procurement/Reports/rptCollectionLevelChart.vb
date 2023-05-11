Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports common
Imports Telerik.WinControls.UI
Imports OrgChartGenerator

Public Class rptCollectionLevelChart
    Inherits FrmMainTranScreen
    Dim myOrgChart As OrgChartGenerator.OrgChart
    Sub LoadData()
        Try

            If Me.cboRootDesg.SelectedValue Is Nothing Then
                clsCommon.MyMessageBoxShow("Please select any Root Collection Level")
                Exit Sub
            End If
            If clsCommon.myLen(Me.cboRootDesg.SelectedValue.ToString) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select any Root Collection Level")
                Exit Sub
            End If
            Dim myOrgData As OrgChartGenerator.OrgData.OrgDetailsDataTable = New OrgChartGenerator.OrgData.OrgDetailsDataTable()
            Dim strq As String = "select LEVEL_CODE as Code,DESCRIPTION as Name,PARENT_LEVEL_CODE as Higher from TSPL_MilkCollectionLevels where PARENT_LEVEL_CODE is not null "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strq)

            myOrgData.AddOrgDetailsRow(Me.cboRootDesg.SelectedValue, Me.cboRootDesg.Text, "", GetMCCNameInMCCLevel(Me.cboRootDesg.SelectedValue))
            For Each row As DataRow In dt.Rows
                myOrgData.AddOrgDetailsRow(row.Item("Code"), row.Item("Name"), row.Item("Higher"), GetMCCNameInMCCLevel(row.Item("Code")))
                'myOrgData.AddOrgDetailsRow(row)
            Next
            'GetUsersNameInDesignation(row.Item("Code"))

            ''instantiate the object
            myOrgChart = New OrgChartGenerator.OrgChart(myOrgData)
            ShowChart()


            'picOrgChart.Height = Me.SplitContainer1.Panel2.Height - 5
            'picOrgChart.Width = Me.SplitContainer1.Panel2.Width - 5
            'Me.picOrgChart.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            'Me.SplitContainer1.Width = 2000
            'Me.RadScrollablePanel1.Width = 2000
            'picOrgChart.Width = 2000 ''Me.SplitContainer1.Panel2.Width - 5
            'picOrgChart.Height = 1000 ''(150 * dtMst.Rows.Count) '' IIf(dt.Rows.Count <= 5, 250, 0) + (40 * dt.Rows.Count)
            Me.picOrgChart.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Text)
        End Try
    End Sub
    Function GetMCCNameInMCCLevel(ByVal Level_Code As String) As String
        Dim strq As String = ""
        Dim MCCName As String = ""

        strq = " select (Description) as MCCName from TSPL_MilkCollectioncenter where LEVEL_CODE='" & Level_Code & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq)

        For intloop As Integer = 0 To dt.Rows.Count - 1
            If intloop = 0 Then
                MCCName = clsCommon.myCstr(dt.Rows(intloop).Item("MCCName"))
            Else
                MCCName = MCCName & Environment.NewLine & clsCommon.myCstr(dt.Rows(intloop).Item("MCCName"))
            End If
        Next

        If clsCommon.myLen(MCCName) > 0 Then
            MCCName = MCCName
        Else
            MCCName = ""
        End If
        Return MCCName
    End Function
    Private Sub ShowChart()

        ''Bitmap bmp =new Bitmap(Image.FromStream(myOrgChart.GenerateOrgChart(640, 480, "1001", System.Drawing.Imaging.ImageFormat.Bmp)));
        Try
            ''bmp.Save(@"d:\temp\1.bmp");
            picOrgChart.Image = Image.FromStream(myOrgChart.GenerateOrgChart(-1, -1, Me.cboRootDesg.SelectedValue, System.Drawing.Imaging.ImageFormat.Bmp))
            ''picOrgChart.Image = Image.FromFile(@"d:\temp\1.bmp");
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub rptCollectionLevelChart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        FillRootDesignation()
        Try
            If cboRootDesg.Items.Count > 0 Then
                Me.cboRootDesg.SelectedIndex = 0
                LoadData()
            End If
        Catch ex As Exception

        End Try
        btnRefresh.Text = "Print"
        'LoadData()
    End Sub

    Sub FillRootDesignation()
        Dim qry As String
        qry = "select LEVEL_CODE as Code,DESCRIPTION as Name from TSPL_MilkCollectionLevels where PARENT_LEVEL_CODE is null " & _
              " and  Comp_Code='" & objCommonVar.CurrentCompanyCode & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Me.cboRootDesg.DataSource = dt
        Me.cboRootDesg.ValueMember = "Code"
        Me.cboRootDesg.DisplayMember = "Name"
    End Sub
    Private Sub TreeViewElement_CreateNodeElement(ByVal sender As Object, ByVal args As CreateTreeNodeElementEventArgs)
        args.NodeElement = New TreeNodeElement()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chkDepartment_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDepartment.ToggleStateChanged
        LoadData()
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptCollectionLevelChart)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        'btnSave.Visible = MyBase.isModifyFlag
        '    btnDelete.Visible = MyBase.isDeleteFlag

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadData()
    End Sub


    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        'Dim path As String
        Dim pic As Image
        pic = picOrgChart.Image
        Dim SaveFileDialog1 As New SaveFileDialog
        SaveFileDialog1.ShowDialog()
        pic.Save(SaveFileDialog1.FileName)
    End Sub
End Class
