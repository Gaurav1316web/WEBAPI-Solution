Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports common
Imports Telerik.WinControls.UI
Imports OrgChartGenerator

Public Class rptCollectionCenterChart
    Inherits FrmMainTranScreen
    Dim myOrgChart As OrgChartGenerator.OrgChart
    Sub LoadData()
        Try

            If Me.cboRootDesg.SelectedValue Is Nothing Then
                clsCommon.MyMessageBoxShow(Me, "Please select any Root Collection Center", Me.Text)
                Exit Sub
            End If
            If clsCommon.myLen(Me.cboRootDesg.SelectedValue.ToString) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select any Root Collection Center", Me.Text)
                Exit Sub
            End If
            Dim myOrgData As OrgChartGenerator.OrgData.OrgDetailsDataTable = New OrgChartGenerator.OrgData.OrgDetailsDataTable()
            'Dim strq As String = "select LEVEL_CODE as Code,DESCRIPTION as Name,PARENT_LEVEL_CODE as Higher from TSPL_MilkCollectionLevels where PARENT_LEVEL_CODE is not null "
            Dim strq = "select TSPL_MilkCollectioncenter.COLLECTION_CENTER_CODE as Code,TSPL_MilkCollectioncenter.DESCRIPTION as Name," & _
                       " TSPL_MilkCollectioncenter_Parent.COLLECTION_CENTER_CODE as Higher from TSPL_MilkCollectioncenter " & _
                       " left join TSPL_MilkCollectionLevels on TSPL_MilkCollectioncenter.LEVEL_CODE=TSPL_MilkCollectionLevels.LEVEL_CODE " & _
                       " left join TSPL_MilkCollectioncenter as TSPL_MilkCollectioncenter_Parent on TSPL_MilkCollectionLevels.PARENT_LEVEL_CODE=TSPL_MilkCollectioncenter_Parent.LEVEL_CODE " & _
                       " where TSPL_MilkCollectioncenter_Parent.COLLECTION_CENTER_CODE is not null"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strq)

            myOrgData.AddOrgDetailsRow(Me.cboRootDesg.SelectedValue, Me.cboRootDesg.Text, "", Me.cboRootDesg.Text)
            For Each row As DataRow In dt.Rows
                myOrgData.AddOrgDetailsRow(row.Item("Code"), row.Item("Name"), row.Item("Higher"), row.Item("Name"))
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Text)
        End Try
    End Sub
    Function GetUsersNameInDesignation(ByVal DesignationCode As String) As String
        Dim strq As String = ""
        Dim UsersName As String = ""
        'strq = " DECLARE @CodeNameString varchar(100)" & _
        '       " SELECT " & _
        '       " @CodeNameString = STUFF( (SELECT ',' + User_Name " & _
        '       " FROM dbo.USER_MASTER where  LEVEL_CODE='" & DesignationCode & "' " & _
        '       " ORDER BY LEVEL_CODE " & _
        '       " FOR XML PATH('')), 1, 1, '') " & _
        '       " select @CodeNameString as OfficersName"
        strq = " select (User_Name + '( ' + coalesce(TSPL_Department_Master.Department_Name,'') +' )') as UserName from USER_MASTER " & _
       " left join TSPL_Department_Master on USER_MASTER.DepartmentId=TSPL_Department_Master.Department_Code  where LEVEL_CODE='" & DesignationCode & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq)

        For intloop As Integer = 0 To dt.Rows.Count - 1
            If intloop = 0 Then
                UsersName = clsCommon.myCstr(dt.Rows(intloop).Item("UserName"))
            Else
                UsersName = UsersName & Environment.NewLine & clsCommon.myCstr(dt.Rows(intloop).Item("UserName"))
            End If
        Next

        If clsCommon.myLen(UsersName) > 0 Then
            UsersName = UsersName
        Else
            UsersName = ""
        End If

        'If dt.Rows.Count > 0 Then
        '    Return dt.Rows(0).Item("OfficersName").ToString
        'Else
        '    Return ""
        'End If
        Return UsersName
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

    Private Sub rptCollectionCenterChart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
        'qry = "select LEVEL_CODE as Code,DESCRIPTION as Name from TSPL_MilkCollectionLevels where PARENT_LEVEL_CODE is null " & _
        '      " and  Comp_Code='" & objCommonVar.CurrentCompanyCode & "'"
        qry = "select TSPL_MilkCollectioncenter.COLLECTION_CENTER_CODE as Code,TSPL_MilkCollectioncenter.DESCRIPTION as Name," & _
              " TSPL_MilkCollectioncenter_Parent.COLLECTION_CENTER_CODE as Higher from TSPL_MilkCollectioncenter " & _
              " left join TSPL_MilkCollectionLevels on TSPL_MilkCollectioncenter.LEVEL_CODE=TSPL_MilkCollectionLevels.LEVEL_CODE " & _
              " left join TSPL_MilkCollectioncenter as TSPL_MilkCollectioncenter_Parent " & _
              " on TSPL_MilkCollectionLevels.PARENT_LEVEL_CODE=TSPL_MilkCollectioncenter_Parent.LEVEL_CODE " & _
              " where TSPL_MilkCollectionLevels.PARENT_LEVEL_CODE is  null and  TSPL_MilkCollectioncenter.Comp_Code='" & objCommonVar.CurrentCompanyCode & "'"
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptCollectionCenterChart)
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
