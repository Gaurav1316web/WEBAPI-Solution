Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class FrmApprovalSetting
    Inherits FrmMainTranScreen
    Dim InSideLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnPendingApprovalOfReq)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
        btnApprove.Visible = MyBase.isPostFlag
    End Sub

    Private Sub FrmApprovalSetting_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub
    Private Sub FrmApprovalSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadScreenName()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnApprove, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")

        txttoDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txttoDate.Value.AddMonths(-1)
        'LoadData()
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub
    Sub CloseForm()
        Me.Close()
    End Sub
    Private Sub LoadScreenName()
        Dim dtType As New DataTable
        dtType.Columns.Add("Code", GetType(String))
        dtType.Columns.Add("Desc", GetType(String))
        dtType.Rows.Add("Select", "Select")
        dtType.Rows.Add("Sales Order", "Sales Order")
        dtType.Rows.Add("Sales Shipment", "Sales Shipment")
        ddlType.DataSource = dtType
        ddlType.ValueMember = "Code"
        ddlType.DisplayMember = "Desc"
    End Sub

    
    Sub LoadData()
        InSideLoadData = True
        Try
            Dim ScreenName As String = clsCommon.myCstr(ddlType.Text)
            Dim FDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt")
            Dim TDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txttoDate.Value), "dd/MMM/yyyy hh:mm tt")
            Dim qry As String = ""

            If ddlType.Text = "Sales Order" Then
                qry = "select CAST(0 as bit) as [Select],TSPL_SD_SALES_ORDER_HEAD.Document_Code, "
                qry += " TSPL_SD_SALES_ORDER_HEAD.Document_Date,TSPL_SD_SALES_ORDER_HEAD.Created_By, "
                qry += " '" & objCommonVar.CurrentUser & "' as ApprovedBy, "
                qry += " '" & clsCommon.GETSERVERDATE & "' as ApprovedDate from TSPL_SD_SALES_ORDER_HEAD "
                qry += " left outer join TSPL_SD_SALES_ORDER_DETAIL on TSPL_SD_SALES_ORDER_HEAD.Document_Code=TSPL_SD_SALES_ORDER_DETAIL.Document_Code "
                qry += " where TSPL_SD_SALES_ORDER_HEAD.Document_Date  >='" + FDate + "'   "
                qry += " and TSPL_SD_SALES_ORDER_HEAD.Document_Date<='" + TDate + "' "
                qry += " AND TSPL_SD_SALES_ORDER_HEAD.Approvel_Required >= 1 "
                qry += " AND TSPL_SD_SALES_ORDER_HEAD.Is_Approved <> 1 "
            ElseIf ddlType.Text = "Sales Shipment" Then
                qry = "select CAST(0 as bit) as [Select],TSPL_SD_SHIPMENT_HEAD.Document_Code, "
                qry += " TSPL_SD_SHIPMENT_HEAD.Document_Date,TSPL_SD_SHIPMENT_HEAD.Created_By, "
                qry += " '" & objCommonVar.CurrentUser & "' as ApprovedBy, "
                qry += " '" & clsCommon.GETSERVERDATE & "' as ApprovedDate from TSPL_SD_SHIPMENT_HEAD "
                qry += " left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.Document_Code "
                qry += " where TSPL_SD_SHIPMENT_HEAD.Document_Date  >='" + FDate + "'   "
                qry += " and TSPL_SD_SHIPMENT_HEAD.Document_Date<='" + TDate + "' "
                qry += " AND TSPL_SD_SHIPMENT_HEAD.Approvel_Required >= 1 "
                qry += " AND TSPL_SD_SHIPMENT_DETAIL.Is_Approved <> 1 "
            End If

            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(qry)

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Record Found")
            Else
                gv1.DataSource = dt

                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                    gv1.Columns(ii).IsVisible = False
                Next

                gv1.Columns("Select").IsVisible = True
                gv1.Columns("Select").Width = 50
                gv1.Columns("Select").HeaderText = " "
                gv1.Columns("Select").ReadOnly = False

                gv1.Columns("Document_Code").IsVisible = True
                gv1.Columns("Document_Code").Width = 100
                gv1.Columns("Document_Code").HeaderText = "Document No"

                gv1.Columns("Document_Date").IsVisible = True
                gv1.Columns("Document_Date").Width = 90
                gv1.Columns("Document_Date").HeaderText = "Document Date"

                gv1.Columns("Created_By").IsVisible = True
                gv1.Columns("Created_By").Width = 80
                gv1.Columns("Created_By").HeaderText = "Requested By"

                gv1.Columns("ApprovedBy").IsVisible = True
                gv1.Columns("ApprovedBy").Width = 100
                gv1.Columns("ApprovedBy").HeaderText = "Approved By"

                gv1.Columns("ApprovedDate").IsVisible = True
                gv1.Columns("ApprovedDate").Width = 100
                gv1.Columns("ApprovedDate").HeaderText = "Approved Date"


                gv1.ShowGroupPanel = False
                gv1.AllowDeleteRow = True
                gv1.AllowAddNewRow = False
                gv1.ShowGroupPanel = False
                gv1.AllowColumnReorder = False
                gv1.AllowRowReorder = False
                gv1.EnableSorting = False
                gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
                gv1.MasterTemplate.ShowRowHeaderColumn = False
                gv1.TableElement.TableHeaderHeight = 40
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        InSideLoadData = False
    End Sub

    Private Sub btnApprove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        Try
            Dim isPosted As Boolean = False
            Dim Qry As String
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myCBool(gv1.Rows(ii).Cells("Select").Value) Then
                    If ddlType.Text = "Sales Order" Then
                        Qry = "update TSPL_SD_SALES_ORDER_HEAD set is_approved=1,Approved_Date='" & gv1.Rows(ii).Cells("ApprovedDate").Value & "',Approved_By='" & gv1.Rows(ii).Cells("ApprovedBy").Value & "' where Document_Code='" & gv1.Rows(ii).Cells("Document_Code").Value & "'"
                        clsDBFuncationality.ExecuteNonQuery(Qry)
                    ElseIf ddlType.Text = "Sales Shipment" Then
                        Qry = "update TSPL_SD_SHIPMENT_HEAD set is_approved=1,Approved_Date='" & gv1.Rows(ii).Cells("ApprovedDate").Value & "',Approved_By='" & gv1.Rows(ii).Cells("ApprovedBy").Value & "' where Document_Code='" & gv1.Rows(ii).Cells("Document_Code").Value & "'"
                        clsDBFuncationality.ExecuteNonQuery(Qry)
                    End If

                    isPosted = True
                End If
            Next

            If isPosted Then
                clsCommon.MyMessageBoxShow("Successfully Approved")
                'LoadData()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        If ddlType.Text <> "Select" Then
            LoadData()
        Else
            clsCommon.MyMessageBoxShow("Please select Type")
        End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        gv1.DataSource = Nothing
        'dgvItem.Columns.Clear()
    End Sub
End Class
