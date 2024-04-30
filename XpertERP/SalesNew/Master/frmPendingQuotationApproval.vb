Imports common
Imports System.Data.SqlClient

Public Class FrmPendingQuotationApproval
    Inherits FrmMainTranScreen
    Dim InSideLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPendingQuotationApproval)
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
        If e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnApprove.Enabled Then
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
    Function GetPendingQuotationForApproval(ByVal fromdate As Date, ByVal todate As Date) As DataTable
        Dim UserLevel As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select TSPL_USER_MASTER.ApprovalLevel from TSPL_USER_MASTER WHERE User_Code='" + objCommonVar.CurrentUserCode + "' "))
        Dim FDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromdate), "dd/MMM/yyyy hh:mm tt")
        Dim TDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(todate), "dd/MMM/yyyy hh:mm tt")
        'Dim qry As String = "select CAST(0 as bit) as [Select], TSPL_SD_QUOTATION_HEAD.Document_Code, TSPL_SD_QUOTATION_HEAD.Document_Date ,"
        'qry += " TSPL_SD_QUOTATION_HEAD.Request_By,  TSPL_SD_QUOTATION_HEAD.Description,   "
        'qry += " case when TSPL_SD_QUOTATION_HEAD.Level3_Approval_Status=1 then TSPL_SD_QUOTATION_HEAD.Level3_Approval_By else  case when TSPL_SD_QUOTATION_HEAD.Level2_Approval_Status=1 then TSPL_SD_QUOTATION_HEAD.Level2_Approval_By else   case when TSPL_SD_QUOTATION_HEAD.Level1_Approval_Status=1 then TSPL_SD_QUOTATION_HEAD.Level1_Approval_By else ''  end  end end as LastApprovedByCode, "
        'qry += " TSPL_SD_QUOTATION_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc, TSPL_SD_QUOTATION_DETAIL.Approval_Level_Required"
        'qry += " From TSPL_SD_QUOTATION_DETAIL "
        'qry += " Left Outer Join TSPL_SD_QUOTATION_HEAD ON  TSPL_SD_QUOTATION_DETAIL.Document_Code=TSPL_SD_QUOTATION_HEAD.Document_Code"
        'qry += " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_QUOTATION_DETAIL.Item_Code "
        'qry += " WHERE TSPL_SD_QUOTATION_HEAD.Status <> 1 "
        'qry += " AND TSPL_SD_QUOTATION_HEAD.Document_Date  >='" + FDate + "'   "
        'qry += " and TSPL_SD_QUOTATION_HEAD.Document_Date<='" + TDate + "' "
        'qry += " AND TSPL_SD_QUOTATION_DETAIL.Approval_Level_Required >= " + UserLevel + ""
        'qry += " AND TSPL_SD_QUOTATION_DETAIL.Is_Approved <> 1 "
        'If UserLevel = "1" Then
        '    qry += " And TSPL_SD_QUOTATION_DETAIL.Level1_Approval_Status <> 1 "
        'ElseIf UserLevel = "2" Then
        '    qry += " And TSPL_SD_QUOTATION_DETAIL.Level2_Approval_Status <> 1 AND TSPL_SD_QUOTATION_DETAIL.Level1_Approval_Status = 1 "
        'ElseIf UserLevel = "3" Then
        '    qry += " And TSPL_SD_QUOTATION_DETAIL.Level3_Approval_Status <> 1 AND TSPL_SD_QUOTATION_DETAIL.Level2_Approval_Status = 1 "
        'End If
        Dim qry As String = "select CAST(0 as bit) as [Select], TSPL_SD_SALES_Quotation_HEAD.Document_Code, TSPL_SD_SALES_Quotation_HEAD.Document_Date , TSPL_SD_SALES_Quotation_HEAD.Description,TSPL_SD_SALES_Quotation_HEAD.Created_By as Request_By   , '' as LastApprovedByCode,  TSPL_SD_SALES_Quotation_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc,  TSPL_SD_SALES_Quotation_HEAD.Approval_Level as Approval_Level_Required  From TSPL_SD_SALES_Quotation_DETAIL  Left Outer Join TSPL_SD_SALES_Quotation_HEAD ON  TSPL_SD_SALES_Quotation_DETAIL.Document_Code=TSPL_SD_SALES_Quotation_HEAD.Document_Code LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALES_Quotation_DETAIL.Item_Code " & _
        " Left outer Join tspl_user_master on TSPL_USER_MASTER.User_Code =TSPL_SD_SALES_Quotation_HEAD.Created_By " & _
        " WHERE TSPL_SD_SALES_Quotation_HEAD.Status <> 1  AND TSPL_SD_SALES_Quotation_HEAD.Document_Date   >='" + FDate + "'    and TSPL_SD_SALES_Quotation_HEAD.Document_Date<='" + TDate + "'   and TSPL_SD_SALES_Quotation_HEAD.Is_Approved <> 1 and  TSPL_SD_SALES_Quotation_HEAD.Approval_Level=(Select case when ApprovalLevel=0 then 'No Level' else case when ApprovalLevel=3 then 'Level1'   else 'Level2' end end as ApprovalLevel from TSPL_user_master where user_code='" & objCommonVar.CurrentUserCode & "') "
        Return clsDBFuncationality.GetDataTable(qry)
    End Function

    Sub LoadData()
        InSideLoadData = True
        Try
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()

            Dim dt As DataTable = GetPendingQuotationForApproval(txtFromDate.Value, txttoDate.Value)
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
            gv1.Columns("Document_Code").HeaderText = "Quotation ID"

            gv1.Columns("Document_Date").IsVisible = True
            gv1.Columns("Document_Date").Width = 90
            gv1.Columns("Document_Date").HeaderText = "Quotation Date"

            gv1.Columns("Request_By").IsVisible = True
            gv1.Columns("Request_By").Width = 80
            gv1.Columns("Request_By").HeaderText = "Request By"

            gv1.Columns("LastApprovedByCode").IsVisible = True
            gv1.Columns("LastApprovedByCode").Width = 100
            gv1.Columns("LastApprovedByCode").HeaderText = "Approved By"

            gv1.Columns("Item_Code").IsVisible = True
            gv1.Columns("Item_Code").Width = 100
            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").Width = 200
            gv1.Columns("Item_Desc").HeaderText = "Item Description"

            gv1.Columns("Approval_Level_Required").IsVisible = True
            gv1.Columns("Approval_Level_Required").Width = 150
            gv1.Columns("Approval_Level_Required").HeaderText = "Approval Required Level"

            gv1.GroupDescriptors.Add(New GridGroupByExpression("Document_Code as Quotation  format ""{0}: {1}"" group by Document_Code"))
            gv1.MasterTemplate.ExpandAllGroups()

            gv1.ShowGroupPanel = False
            gv1.MasterTemplate.AutoExpandGroups = True
            gv1.AllowDeleteRow = True
            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = False
            gv1.AllowRowReorder = False
            gv1.EnableSorting = False
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            gv1.TableElement.TableHeaderHeight = 40
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        InSideLoadData = False
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadData()

    End Sub

    Private Sub btnApprove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        Try
            Dim isPosted As Boolean = False
            Dim DocCode As String = ""
            Dim ItemCode As String = ""
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myCBool(gv1.Rows(ii).Cells("Select").Value) Then
                    DocCode = clsCommon.myCstr(gv1.Rows(ii).Cells("Document_Code").Value)
                    ItemCode = clsCommon.myCstr(gv1.Rows(ii).Cells("Item_Code").Value)

                    clsDBFuncationality.ExecuteNonQuery("update TSPL_SD_SALES_Quotation_HEAD set Is_Approved=1 ,Approved_By='" & objCommonVar.CurrentUserCode & "' ,Approved_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "' where Document_Code='" & DocCode & "' ")
                    '  isPosted = clsSalesQuotationsHead.PostData(DocCode, ItemCode)
                    isPosted = True
                End If
            Next
           
            If isPosted Then
                clsCommon.MyMessageBoxShow("Successfully Approved")
                LoadData()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        'Dim DocNo As String
        'If Not InSideLoadData Then
        '    If e.Column Is gv1.Columns("Select") And gv1.CurrentRow.Cells("Select").Value = True Then
        '        DocNo = clsCommon.myCstr(gv1.CurrentRow.Cells("Document_Code").Value)
        '    End If
        'End If
        'InSideLoadData = True
        'For Each grow As GridViewRowInfo In gv1.Rows
        '    If clsCommon.myCstr(grow.Cells("Document_Code").Value) = DocNo Then
        '        grow.Cells("Select").Value = True
        '    End If
        'Next
        'InSideLoadData = False
    End Sub

    Private Sub gv1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gv1.MouseDoubleClick
        Try
            Dim DocNo As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Document_Code").Value)
            Dim frm As New frmSNSalesQuotation()
            frm.StrDocNo = DocNo
            frm.ShowDialog()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

