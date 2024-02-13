
Imports common
Imports System.Data.SqlClient



Public Class frmRequestAproval
    Inherits FrmMainTranScreen
    Dim trnsLstCustomer As New List(Of String)
    Dim strCustomerCode As String = Nothing
    Dim dt1 As DataTable = New DataTable()
    Dim Isrefreshed As Boolean = False    '' Variable for Validate the btnPost(Enable/Disable) and GridView
    Dim IsSelected As Boolean = False     '' Variable for Validate the btnSelectAll(ChangeText)
    Dim qry As String
    Dim dt As DataTable
    Dim count As Integer = 0
    Dim strNoOfRecord As String
    Dim trnsLst As New List(Of String)
    Dim arrUser As New ArrayList()
    Dim arrSelectedUser As New ArrayList()
    Dim strDocNo As String = Nothing
    Dim countPostedDoc As Integer = 0
    Public IsPostBack As Boolean = False
    Dim DtError As DataTable
    Dim dr As DataRow
    Public fromdate As DateTime
    Public Todate As DateTime
    Public ModuleName As String = ""
    Public Transaction As String = ""
    Public IsOpenPsted As Boolean
    Dim ButtonToolTip As New ToolTip()
    Dim isInsideLoad As Boolean = False
    '' List for Storing The Data(Which Is Selected) For Bulk Posting
    '==Sanjeet==========================
    Dim dtAuthen As DataTable
    Dim StrQuery As String = Nothing
    Dim ChkAllowBulkPosting As Double
    Dim arrLoc As String = Nothing
    Dim IsInsideLoadData As Boolean = True
    Dim ShowDairySaleModuleOnBulkPosting As Integer
    Dim RecordCount As Integer = 0
    Dim CreateProvisionOfTransporterInDairyDispatch As Boolean = False

    Private Sub FrmPendingAproval_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for Closing The Window")
        SetUserMgmtNew()
        btnPost.Enabled = False
        LoadBlankGrid()
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
    End Sub
    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPost.Visible = MyBase.isPostFlag
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.AllowAddNewRow = False

    End Sub

    Sub gv1Format()
        Me.gv1.MasterTemplate.Columns("Status").Width = 50
        'Me.gv1.MasterTemplate.Columns("Status").ReadOnly = False
        Me.gv1.MasterTemplate.Columns("Request Code").Width = 150
        Me.gv1.MasterTemplate.Columns("Request Code").ReadOnly = True
        Me.gv1.MasterTemplate.Columns("Request Date").Width = 150
        Me.gv1.MasterTemplate.Columns("Request Date").ReadOnly = True
        Me.gv1.MasterTemplate.Columns("Posted Date").Width = 150
        Me.gv1.MasterTemplate.Columns("Posted Date").ReadOnly = True
        Me.gv1.MasterTemplate.Columns("Request By").Width = 150
        Me.gv1.MasterTemplate.Columns("Request By").ReadOnly = True
        Me.gv1.MasterTemplate.Columns("Request Name").Width = 150
        Me.gv1.MasterTemplate.Columns("Request Name").ReadOnly = True
    End Sub




#Region "Showing Details on GRID"
    Public Sub LoadPendingData(ByVal isApproved As Boolean)
        If clsCommon.myCDate(dtpFromDate.Value, "dd/MMM/yyyy") > clsCommon.myCDate(dtpToDate.Value, "dd/MMM/yyyy") AndAlso isApproved = False Then
            common.clsCommon.MyMessageBoxShow("'From date' Cann't Be Greater Than 'To Date'")
        Else
            Dim qry As String = " select CAST((0)as BIT) as Status, Request_Code as [Request Code] , convert (varchar, Request_Date,103) as [Request Date], convert (varchar,tspl_user_request_master.Posted_DATE,103) as [Posted Date] ,Request_By as [Request By],TSPL_USER_MASTER.User_Name as [Request Name]  from tspl_user_request_master 
                                  left outer join TSPL_USER_MASTER  on TSPL_USER_MASTER.User_Code = tspl_user_request_master.Request_By where tspl_user_request_master.POSTED=1 and Approved_status =0 and REQUEST_FOR = '" + objCommonVar.CurrentUserCode + "'"
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                qry += " and tspl_user_request_master.Request_By in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
            End If
            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1Format()
            If isApproved = False Then
                Validation()

                If dt.Rows.Count <= 0 Then
                    lblNoOfRecords.Text = "No Record Found"
                Else
                    strNoOfRecord = clsCommon.myCstr(dt.Rows.Count)
                    lblNoOfRecords.Text = "" + strNoOfRecord + " Records Found"
                End If
            End If


        End If
    End Sub
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        LoadPendingData(False)
        'If clsCommon.myCDate(dtpFromDate.Value, "dd/MMM/yyyy") > clsCommon.myCDate(dtpToDate.Value, "dd/MMM/yyyy") Then
        '    common.clsCommon.MyMessageBoxShow("'From date' Cann't Be Greater Than 'To Date'")
        'Else
        '    Dim qry As String = " select CAST((0)as BIT) as Status, Request_Code as [Request Code] , convert (varchar, Request_Date,103) as [Request Date], convert (varchar,tspl_user_request_master.Posted_DATE,103) as [Posted Date] ,Request_By as [Request By],TSPL_USER_MASTER.User_Name as [Request Name]  from tspl_user_request_master 
        '                          left outer join TSPL_USER_MASTER  on TSPL_USER_MASTER.User_Code = tspl_user_request_master.Request_By where tspl_user_request_master.POSTED=1 and Approved_status =0 and REQUEST_FOR = '" + objCommonVar.CurrentUserCode + "'"
        '    If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
        '        qry += " and tspl_user_request_master.Request_By in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
        '    End If
        '    dt = clsDBFuncationality.GetDataTable(qry)
        '    gv1.DataSource = dt
        '    gv1.MasterTemplate.SummaryRowsBottom.Clear()
        '    gv1Format()

        '    Validation()

        '    If dt.Rows.Count <= 0 Then
        '        lblNoOfRecords.Text = "No Record Found"
        '    Else
        '        strNoOfRecord = clsCommon.myCstr(dt.Rows.Count)
        '        lblNoOfRecords.Text = "" + strNoOfRecord + " Records Found"
        '    End If

        'End If


    End Sub

#End Region


#Region "Posting"

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        trnsLst = New List(Of String)
        Dim trnsNo As Integer
        countPostedDoc = 0
        trnsLstCustomer = New List(Of String)

        DtError = New DataTable
        DtError.Columns.Add("Code", GetType(String))
        DtError.Columns.Add("Error", GetType(String))

        For trnsNo = 0 To gv1.Rows.Count - 1
            If gv1.Rows(trnsNo).Cells("Status").Value = True Then
                trnsLst.Add(gv1.Rows(trnsNo).Cells("Request Code").Value)
            End If
        Next

        If trnsLst.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Select Atleast One Document", Me.Text)
        Else
            If myMessages.RequestApprovalConfirm Then
                For j As Integer = 0 To trnsLst.Count - 1
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Try
                        strDocNo = trnsLst.Item(j)
                        qry = " update tspl_user_request_master set APPROVED_STATUS = 1 , APPROVED_STATUS_BY = '" + objCommonVar.CurrentUserCode + "' , APPROVED_STATUS_DATE = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") + "'  where Request_Code in ('" + strDocNo + "') "
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        countPostedDoc = countPostedDoc + 1
                        trans.Commit()
                        LoadPendingData(True)
                    Catch ex As Exception
                        trans.Rollback()
                    End Try
                Next

            End If
        End If
    End Sub

#End Region

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm()
    End Sub


    Sub closeForm()
        Me.Close()
    End Sub



    Sub Validation()
        'If Isrefreshed = True Then
        '    Me.gv1.MasterTemplate.Columns("Status").ReadOnly = True
        '    btnPost.Enabled = False
        '    btnSlctAll.Enabled = False
        'ElseIf Isrefreshed = False Then
        Me.gv1.MasterTemplate.Columns("Status").ReadOnly = False
        btnPost.Enabled = True
        btnSlctAll.Enabled = True
        'End If
    End Sub




    Sub SelectUnselectAll()
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(i).Cells("Status").Value = False
        Next
    End Sub



    Private Sub FrmPendingAproval_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            closeForm()
        End If
    End Sub


    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim strQry As String = ""
        strQry = " select User_Code as Code , User_Name as Name  from TSPL_USER_MASTER where Level4_Code in ('Admin')"
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSelUSer@RequestApproval", strQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        txtCustomer.arrValueMember = Nothing
        gv1.DataSource = Nothing
    End Sub
    Private Sub btnSlctAll_Click_1(sender As Object, e As EventArgs) Handles btnSlctAll.Click
        SelectUnselectAll()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        trnsLst = New List(Of String)
        Dim trnsNo As Integer
        countPostedDoc = 0
        trnsLstCustomer = New List(Of String)

        DtError = New DataTable
        DtError.Columns.Add("Code", GetType(String))
        DtError.Columns.Add("Error", GetType(String))

        For trnsNo = 0 To gv1.Rows.Count - 1
            If gv1.Rows(trnsNo).Cells("Status").Value = True Then
                trnsLst.Add(gv1.Rows(trnsNo).Cells("Request Code").Value)
            End If
        Next

        If trnsLst.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Select Atleast One Document", Me.Text)
        Else
            If myMessages.RequestRejectConfirm Then
                For j As Integer = 0 To trnsLst.Count - 1
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Try
                        strDocNo = trnsLst.Item(j)
                        qry = " update tspl_user_request_master set APPROVED_STATUS = 2 , APPROVED_STATUS_BY = '" + objCommonVar.CurrentUserCode + "' , APPROVED_STATUS_DATE = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") + "'  where Request_Code in ('" + strDocNo + "') "
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        countPostedDoc = countPostedDoc + 1
                        trans.Commit()
                        common.clsCommon.MyMessageBoxShow(Me, "Data Canceled Successfully", Me.Text)
                        LoadPendingData(True)
                    Catch ex As Exception
                        trans.Rollback()
                    End Try
                Next

            End If
        End If
    End Sub

    Private Sub gv1_DoubleClick(sender As Object, e As EventArgs) Handles gv1.DoubleClick
        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmRequestMaster, clsCommon.myCstr(gv1.CurrentRow.Cells("Request Code").Value))
    End Sub
End Class




