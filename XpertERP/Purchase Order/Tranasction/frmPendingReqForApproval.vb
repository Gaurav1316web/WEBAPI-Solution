'-- Ticket No - BM00000002517 by Puran Singh Negi

Imports common
Public Class FrmPendingReqForApproval
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnPendingApprovalOfReq)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnApprove.Visible = MyBase.isPostFlag
    End Sub

    Private Sub FrmPendingReqForApproval_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnApprove, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")

        txttoDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txttoDate.Value.AddMonths(-1)
        LoadData()
    End Sub

    Private Sub FrmPendingReqForApproval_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Sub PostData()

    End Sub
    Function GetPendingRequitionForApproval(ByVal fromdate As Date, ByVal todate As Date) As DataTable
        Dim FDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromdate), "dd/MMM/yyyy hh:mm tt")
        Dim TDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(todate), "dd/MMM/yyyy hh:mm tt")
        Dim qry As String = "select CAST(0 as bit) as Sel,xxx.Requisition_Id,xxx.Requisition_Date,xxx.Request_By,xxx.Description,xxx.Total_RQ_Amt,LastApprovalUser.User_Name as LastApprovedBy,LastApprovalUser.Dept,TSPL_GL_SEGMENT_CODE.Description as LastApprovalDepartment from ("
        qry += " select  TSPL_REQUISITION_HEAD.Requisition_Id,TSPL_REQUISITION_HEAD.Requisition_Date,TSPL_REQUISITION_HEAD.Request_By,TSPL_REQUISITION_HEAD.Description,TSPL_REQUISITION_HEAD.Total_RQ_Amt,case when Level5_Approval_Status=1 then Level5_Approval_By else  case when Level4_Approval_Status=1 then Level4_Approval_By else   case when Level3_Approval_Status=1 then Level3_Approval_By else  case when Level2_Approval_Status=1 then Level2_Approval_By else   case when Level1_Approval_Status=1 then Level1_Approval_By else ''  end  end end end end as LastApprovedByCode"
        qry += " from TSPL_REQUISITION_HEAD "
        qry += " left outer join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "'"
        qry += " where(TSPL_USER_MASTER.Level <= TSPL_REQUISITION_HEAD.Approvel_Level_Required)"
        qry += " and  ((TSPL_USER_MASTER.Level-1) = TSPL_REQUISITION_HEAD.Level1_Approval_Status+Level2_Approval_Status+Level3_Approval_Status+Level4_Approval_Status+Level5_Approval_Status)"
        qry += " and TSPL_REQUISITION_HEAD.Requisition_Date >='" + FDate + "' and TSPL_REQUISITION_HEAD.Requisition_Date<='" + TDate + "'"
        qry += " )xxx"
        qry += " left outer join TSPL_USER_MASTER as LastApprovalUser on LastApprovalUser.User_Code=xxx.LastApprovedByCode"
        qry += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=LastApprovalUser.Dept"
        Return clsDBFuncationality.GetDataTable(qry)
    End Function
    Sub LoadData()
        Try
            Dim dt As DataTable = GetPendingRequitionForApproval(txtFromDate.Value, txttoDate.Value)
            gv1.DataSource = dt

            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = False
            Next
            gv1.Columns("Sel").IsVisible = True
            gv1.Columns("Sel").Width = 40
            gv1.Columns("Sel").HeaderText = " "
            gv1.Columns("Sel").ReadOnly = False

            gv1.Columns("Requisition_Id").IsVisible = True
            gv1.Columns("Requisition_Id").Width = 100
            gv1.Columns("Requisition_Id").HeaderText = "Requisition ID"

            gv1.Columns("Requisition_Date").IsVisible = True
            gv1.Columns("Requisition_Date").Width = 80
            gv1.Columns("Requisition_Date").HeaderText = "Requisition Date"

            gv1.Columns("Request_By").IsVisible = True
            gv1.Columns("Request_By").Width = 100
            gv1.Columns("Request_By").HeaderText = "Request By"

            gv1.Columns("Description").IsVisible = True
            gv1.Columns("Description").Width = 200
            gv1.Columns("Description").HeaderText = "Description"

            gv1.Columns("Total_RQ_Amt").IsVisible = True
            gv1.Columns("Total_RQ_Amt").Width = 150
            gv1.Columns("Total_RQ_Amt").HeaderText = "Requisition Amount"
            gv1.Columns("Total_RQ_Amt").FormatString = "{0:F2}"



            gv1.Columns("LastApprovedBy").IsVisible = True
            gv1.Columns("LastApprovedBy").Width = 100
            gv1.Columns("LastApprovedBy").HeaderText = "Approved By"

            gv1.Columns("Dept").IsVisible = False
            gv1.Columns("Dept").Width = 150
            gv1.Columns("Dept").HeaderText = "Last Approval Department Code"

            gv1.Columns("LastApprovalDepartment").IsVisible = True
            gv1.Columns("LastApprovalDepartment").Width = 200
            gv1.Columns("LastApprovalDepartment").HeaderText = "Last Approval Department"

            gv1.AllowDeleteRow = True
            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = False
            gv1.AllowRowReorder = False
            gv1.EnableSorting = False
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            gv1.TableElement.TableHeaderHeight = 40

            If dt.Rows.Count = 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found To Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadData()
        
    End Sub

    Private Sub btnApprove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        Try
            Dim isPosted As Boolean = False
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myCBool(gv1.Rows(ii).Cells("Sel").Value) Then
                    clsRequistionHead.PostData(gv1.Rows(ii).Cells("Requisition_Id").Value)
                    isPosted = True
                End If
            Next
            If isPosted Then
                clsCommon.MyMessageBoxShow(Me, "Successfully Approved", Me.Text)
                LoadData()
            Else

                clsCommon.MyMessageBoxShow(Me, "Please select at least one Requisition to Approve", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells("Requisition_Id").Value)) > 0 Then
                Dim frm As New frmPurchaseRequistion()
                frm.SetUserMgmt(clsUserMgtCode.mbtnPurchaseRequistion)
                frm.strDocumentNo = clsCommon.myCstr(gv1.CurrentRow.Cells("Requisition_Id").Value)
                frm.Show()
            End If
        Catch ex As Exception

        End Try
        

    End Sub
End Class
