''''-01/11/2012-4:15PM-Updation By [Pankaj Kumar] In Case of Route Shifting Whether Customer Has Visi Or Not he can be shifted to another Route---Fwd by--Ranjana Mam
'''' '--preeti gupta-ticket no.[BM00000003133]
Imports System
Imports System.Data.SqlClient
Imports common
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports System.Globalization
Imports System.Threading
' Developed by : Abhishek Kumar
'Started Date : 12/12/2011
'Ending Date : 13/12/2011
Public Class FrmRoute_Shifting
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim obj As New clsRouteSifting
    Dim dt As New DataTable
    Dim myDs As DataSet
    Public Sub SetLength()
        txtNewRouteId.MyMaxLength = 12
        txtRouteId.MyMaxLength = 12
        txtNewDesc.MaxLength = 50
        txtRouteDesc.MaxLength = 50
    End Sub

    Private Sub FrmRoute_Shifting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetLength()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ' ButtonToolTip.SetToolTip(, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
        ddlstatus.SelectedIndex = 0
        lblNewRouteId.Visible = False
        lblNewRouteDesc.Visible = False
        txtNewRouteId.Visible = False
        txtNewDesc.Visible = False
        reset()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmRouteShifting)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")
        '    Me.Close()
        '    Exit Sub
        'End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    '---------------------Added By -----Pankaj Kumar-------------on--29/03/2012-------------
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "OTLT-SIFT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btnSave.Enabled = False
    '        End If
    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    '    '-----------------------------------Code Ends Here-------------------------------
    'End Function
    Private Sub txtRouteId__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
     
    End Sub
    Public Sub getdata()
        isCellValueChangedOpen = True
        Dim route_id As String = txtRouteId.Value

        dt = clsRouteSifting.GetDatafromRoute_master(route_id)

        Try
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim gro As GridViewRowInfo = MasterTemplate.Rows.AddNew()
                gro.Cells(0).Value = dt.Rows(i)("CustCode").ToString()
                gro.Cells(1).Value = dt.Rows(i)("Cust_Name").ToString()
                gro.Cells(2).Value = dt.Rows(i)("Route_No").ToString()
                gro.Cells(3).Value = dt.Rows(i)("Route_Group").ToString()
                'gro.Cells(4).Value = dt.Rows(i)("Route_group").ToString()
                'dtpSatrtDate.Value = CDate(dt.Rows(i)("Fromdate")).ToString("dd/MM/yyyy")

                Dim chkVisi As Integer = clsCommon.myCdbl(dt.Rows(i)("Visi_Id"))
                If chkVisi <= 0 Then
                    'MasterTemplate.CurrentRow.Cells("VisiId").Value = False
                    gro.Cells(6).Value = "No"
                Else
                    'MasterTemplate.CurrentRow.Cells("VisiId").Value = True
                    'gro.Cells(6).IsSelected = True
                    gro.Cells(6).Value = "Yes"
                    If ddlstatus.SelectedIndex <> 1 Then
                        MasterTemplate.CurrentRow.Cells("YesNo").ReadOnly = True
                    End If

                End If
            Next

        Catch Err As Exception
            Throw New Exception(Err.Message)
        End Try

        isCellValueChangedOpen = False
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub grdviewRouteDetail_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles MasterTemplate.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is MasterTemplate.Columns("FromDate")) Then
                    MasterTemplate.Columns("FromDate").FormatString = "{0:d}"
                End If
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try
    End Sub
    Private Function validatefun() As Boolean
        If txtRouteId.Value = "" Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select the Route Id", Me.Text)
            txtRouteId.Focus()
            Return False
        ElseIf ddlstatus.SelectedIndex = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select the status", Me.Text)
            ddlstatus.Focus()
            Return False
        Else
            Return True
        End If

    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        If validatefun() Then
            Try
                Dim arrlst As New List(Of clsRouteSifting)
                Dim RouteDesc As String


                For i As Integer = 0 To MasterTemplate.Rows.Count - 1
                    If MasterTemplate.Rows(i).Cells("YesNo").Value = True Then
                        obj = New clsRouteSifting()
                        obj.route_no = MasterTemplate.Rows(i).Cells("ExistingRoute").Value
                        obj.new_route_id = MasterTemplate.Rows(i).Cells("NewRoute").Value
                        Dim qrydesc As String = "select Route_Desc  from TSPL_ROUTE_MASTER where Route_No ='" & Convert.ToString(MasterTemplate.Rows(i).Cells("NewRoute").Value) & "'"
                        RouteDesc = clsDBFuncationality.getSingleValue(qrydesc)
                        obj.desc = RouteDesc
                        'obj.fromdate = MasterTemplate.Rows(i).Cells("FromDate").Value
                        obj.fromdate = dtpStartDate.Value.Date
                        obj.customer_id = MasterTemplate.Rows(i).Cells("Customer").Value
                        obj.Customer_name = MasterTemplate.Rows(i).Cells("Name").Value

                        If clsCommon.myLen(ddlRouteGroup.SelectedValue) <= 0 Then
                            If ddlstatus.SelectedIndex = 2 Then
                            Else
                                'Throw New Exception("Select the Route Group for Customer  " + MasterTemplate.Rows(i).Cells("Customer").Value + " ")
                                Throw New Exception("Select the Route Group  ")
                            End If

                        Else
                            obj.Route_group = ddlRouteGroup.SelectedValue
                        End If


                        If ddlstatus.SelectedIndex = 1 Then
                            arrlst.Add(obj)
                        ElseIf ddlstatus.SelectedIndex = 2 Then
                            Dim visid As String = connectSql.RunScalar("select  Visi_Id  from tspl_customer_master where Cust_Code = '" + Convert.ToString(MasterTemplate.Rows(i).Cells("Customer").Value) + "'")
                            If Not String.IsNullOrEmpty(visid) Then
                                Dim strmessage As String = "Customer " + obj.Customer_name + Environment.NewLine
                                strmessage += "has visi " + visid + " attached"
                                common.clsCommon.MyMessageBoxShow(Me, strmessage, Me.Text)
                                Return
                            Else
                                arrlst.Add(obj)
                            End If
                        End If
                    End If
                Next
                If arrlst.Count > 0 Then
                    obj.SaveData(arrlst, obj.route_no, ddlstatus)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    reset()
                Else
                    common.clsCommon.MyMessageBoxShow(Me, "Select at least one Customer", Me.Text)

                End If
            Catch ex As Exception
                common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
    End Sub
    Dim isCellValueChangedOpen As Boolean = False
    Private Sub MasterTemplate_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles MasterTemplate.CellValueChanged
        Try

            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If e.Column Is MasterTemplate.Columns("NewRoute") Then
                    OpenICodeList(False)
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub
    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select route_no as Code ,Route_desc as Description from TSPL_ROUTE_MASTER"
        Dim WhrCls As String = ""
        MasterTemplate.CurrentRow.Cells("NewRoute").Value = clsCommon.ShowSelectForm("NewRouteFinder", qry, "Code", WhrCls, clsCommon.myCstr(MasterTemplate.CurrentRow.Cells("NewRoute").Value), "Code", False)
    End Sub

    Private Sub txtNewRouteId__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
       
    End Sub

    Public Sub FillRouteGroup()
        ddlRouteGroup.DataSource = Nothing
        'ddlRouteGroup.Items.Clear()
        If txtNewRouteId.Value <> "" Then
            Dim str As String = "select Group_Id [Route Group] from TSPL_ROUTE_GROUP_MASTER where Route_Code = '" + txtNewRouteId.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(str)
            ddlRouteGroup.DataSource = dt
            ddlRouteGroup.ValueMember = "Route Group"
            ddlRouteGroup.DisplayMember = "Route Group"
            'For Each dr As DataRow In dt.Rows
            '    ddlRouteGroup.Items.Add(dr("Route Group"))
            'Next
        End If
        ddlRouteGroup.SelectedIndex = 0
    End Sub
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub

    Public Sub reset()
        dtpStartDate.Value = clsCommon.GETSERVERDATE
        txtRouteId.Value = ""
        txtRouteId.MyReadOnly = False
        txtNewRouteId.Value = ""
        txtNewRouteId.MyReadOnly = False
        txtNewDesc.Text = ""
        txtRouteDesc.Text = ""
        MasterTemplate.DataSource = Nothing
        MasterTemplate.Rows.Clear()
        ddlRouteGroup.SelectedIndex = -1
    End Sub

    Private Sub ddlstatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlstatus.SelectedIndexChanged
        
        If ddlstatus.SelectedIndex = 0 Then
            lblNewRouteId.Visible = False
            lblNewRouteDesc.Visible = False
            txtNewRouteId.Visible = False
            txtNewDesc.Visible = False
            MasterTemplate.MasterTemplate.Columns("NewRoute").IsVisible = True
            RadLabel1.Visible = True
            ddlRouteGroup.Visible = True
        ElseIf ddlstatus.SelectedIndex = 1 Then
            lblNewRouteId.Visible = True
            lblNewRouteDesc.Visible = True
            txtNewRouteId.Visible = True
            txtNewDesc.Visible = True
            MasterTemplate.MasterTemplate.Columns("NewRoute").IsVisible = True
            RadLabel1.Visible = True
            ddlRouteGroup.Visible = True
            For i As Integer = 0 To MasterTemplate.Rows.Count - 1
                MasterTemplate.Rows(i).Cells("YesNo").Value = False
                MasterTemplate.Rows(i).Cells("YesNo").ReadOnly = False
            Next
        ElseIf ddlstatus.SelectedIndex = 2 Then
            lblNewRouteId.Visible = False
            lblNewRouteDesc.Visible = False
            txtNewRouteId.Visible = False
            txtNewDesc.Visible = False
            MasterTemplate.MasterTemplate.Columns("NewRoute").IsVisible = False
            RadLabel1.Visible = False
            ddlRouteGroup.Visible = False
            For i As Integer = 0 To MasterTemplate.Rows.Count - 1
                MasterTemplate.Rows(i).Cells("YesNo").Value = False
                If MasterTemplate.Rows(i).Cells(6).Value = "Yes" Then
                    MasterTemplate.Rows(i).Cells("YesNo").ReadOnly = True
                Else
                    MasterTemplate.Rows(i).Cells("YesNo").ReadOnly = False
                End If
            Next
        End If
    End Sub

    Private Sub MasterTemplate_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles MasterTemplate.EditorRequired
        'If MasterTemplate.CurrentRow.Cells(3).Value <> "" Then
        '    Dim str As String = "select Group_Id [Route Group] from TSPL_ROUTE_GROUP_MASTER where Route_Code = '" + MasterTemplate.CurrentRow.Cells(3).Value + "'"
        '    Dim gvMultiComboColum As GridViewComboBoxColumn = TryCast(MasterTemplate.Columns(4), GridViewComboBoxColumn)
        '    myDs = connectSql.RunSQLReturnDS(str)
        '    gvMultiComboColum.DataSource = myDs.Tables(0)
        '    gvMultiComboColum.ValueMember = "Route Group"

        'End If
    End Sub

    Private Sub FrmRoute_Shifting_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            reset()
        End If
    End Sub

    Private Sub txtRouteId__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRouteId._MYValidating
        'Dim str As String = "select count(*) from TSPL_ROUTE_MASTER where route_no ='" + txtRouteId.Value + "' "
        'Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        'If no = 0 AndAlso isButtonClicked = False Then
        '    txtRouteId.MyReadOnly = False
        '    txtRouteId.Value = ""
        '    'common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        'Else
        '    txtRouteId.MyReadOnly = True
        'End If
        'If txtRouteId.MyReadOnly OrElse isButtonClicked Then
        Dim qry As String = "select route_no as Code ,Route_desc as Description from TSPL_ROUTE_MASTER "
        txtRouteId.Value = clsCommon.ShowSelectForm("RouteMastFND", qry, "Code", " Status='A' ", txtRouteId.Value, "Code", isButtonClicked)
        txtRouteDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_desc from TSPL_ROUTE_MASTER where route_no ='" + txtRouteId.Value + "'"))
        'If txtRouteId.Value = String.Empty Then
        'Else
        MasterTemplate.DataSource = Nothing
        MasterTemplate.Rows.Clear()
        getdata()
        'End If

    End Sub

    Private Sub txtRouteId__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles txtRouteId._MYNavigator
        Dim qst As String = "select route_no as Code ,Route_desc as Description from TSPL_ROUTE_MASTER  where  2=2 "
        Select Case NavigatorType
            Case NavigatorType.Current
                '  qst += "and assign_to='" + txtassign.Value + "' "
                ' qst += "and job_code in ('" + txtcode1.Value + "')"
            Case NavigatorType.Next
                qst += "and route_no in (select min(route_no) from TSPL_ROUTE_MASTER where route_no>'" + txtRouteId.Value + "' and Status='A' ) "
            Case NavigatorType.First
                qst += "and route_no in (select MIN(route_no) from TSPL_ROUTE_MASTER where Status='A' )"
            Case NavigatorType.Last
                qst += "and route_no in (select Max(route_no) from TSPL_ROUTE_MASTER where Status='A' )"
            Case NavigatorType.Previous
                qst += "and route_no in (select max(route_no) from TSPL_ROUTE_MASTER where route_no<'" + txtRouteId.Value + "' and Status='A' )"
        End Select
        ' fun_gridfill()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            txtRouteId.Value = clsCommon.myCstr(dt.Rows(0)("Code"))
            txtRouteDesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        'TextChanged()
        MasterTemplate.DataSource = Nothing
        MasterTemplate.Rows.Clear()
        getdata()
    End Sub

    Private Sub txtNewRouteId__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtNewRouteId._MYValidating
        Dim str As String = "select count(*) from TSPL_ROUTE_MASTER where route_no ='" + txtRouteId.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtRouteId.MyReadOnly = False
            txtNewRouteId.Value = ""
        Else
            txtRouteId.MyReadOnly = True
        End If
        If txtRouteId.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select route_no as Code ,Route_desc as Description from TSPL_ROUTE_MASTER "
            txtNewRouteId.Value = clsCommon.ShowSelectForm("RouteMastFND", qry, "Code", "", txtNewRouteId.Value, "Code", isButtonClicked)
            txtNewDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_desc from TSPL_ROUTE_MASTER where route_no ='" + txtNewRouteId.Value + "'"))
            isCellValueChangedOpen = True
            If MasterTemplate.Rows.Count >= 0 Then
                For i As Integer = 0 To MasterTemplate.Rows.Count - 1
                    MasterTemplate.Rows(i).Cells("NewRoute").Value = txtNewRouteId.Value
                Next
            End If
           
        End If
        FillRouteGroup()
        isCellValueChangedOpen = False
    End Sub

    Private Sub txtNewRouteId__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles txtNewRouteId._MYNavigator
        Dim qst As String = "select route_no as Code ,Route_desc as Description from TSPL_ROUTE_MASTER  where  2=2 "
        Select Case NavigatorType
            Case NavigatorType.Current
                '  qst += "and assign_to='" + txtassign.Value + "' "
                ' qst += "and job_code in ('" + txtcode1.Value + "')"
            Case NavigatorType.Next
                qst += "and route_no in (select min(route_no) from TSPL_ROUTE_MASTER where route_no>'" + txtNewRouteId.Value + "'  ) "
            Case NavigatorType.First
                qst += "and route_no in (select MIN(route_no) from TSPL_ROUTE_MASTER )"
            Case NavigatorType.Last
                qst += "and route_no in (select Max(route_no) from TSPL_ROUTE_MASTER  )"
            Case NavigatorType.Previous
                qst += "and route_no in (select max(route_no) from TSPL_ROUTE_MASTER where route_no<'" + txtNewRouteId.Value + "' )"
        End Select
        ' fun_gridfill()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            txtNewRouteId.Value = clsCommon.myCstr(dt.Rows(0)("Code"))
            txtNewDesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        isCellValueChangedOpen = True
        If MasterTemplate.Rows.Count >= 0 Then
            For i As Integer = 0 To MasterTemplate.Rows.Count - 1
                MasterTemplate.Rows(i).Cells("NewRoute").Value = txtNewRouteId.Value
            Next
        End If
        'TextChanged()
        FillRouteGroup()
        isCellValueChangedOpen = False
    End Sub
End Class
