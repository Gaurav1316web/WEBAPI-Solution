Imports common
Imports System
Imports Telerik.WinControls.UI
Imports System.Net.Mail
Imports System.Net
Imports Telerik.WinControls
Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient
Public Class frmNotificationDetail
#Region "Variables"
    Dim qry As String = Nothing
    Dim StrQuery As String = Nothing
    Dim dtAuthen As DataTable
    Dim arrUser As New ArrayList()
    Dim arrSelectedUser As New ArrayList()
    Dim dt As DataTable = Nothing
    Dim IsSelected As Boolean = False
    Public IsPostBack As Boolean = False
    Private isNewEntry As Boolean = True
    Public ChkOpenFromDate As Date? = Nothing
    Public ChkOpenToDate As Date? = Nothing

    Dim StrDepartment As String = Nothing
    Dim StrTankerDocType As String = Nothing
#End Region
    Public Sub New(ByVal TempDepartment As String, ByVal TempTankerDocType As String)
        InitializeComponent()
        StrDepartment = TempDepartment
        StrTankerDocType = TempTankerDocType
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub frmNotificationDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lblMsgs.Text = ""
            btnSubmit.Text = "Submit"

            ShowData()
            If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then
                dt = clsDBFuncationality.GetDataTable(qry)

                If dt.Rows.Count <= 0 Then
                    gv1.Text = "No data found to display "

                    btnSubmit.Text = "Close"

                Else
                    btnSubmit.Text = "Submit"
                    gv1.DataSource = dt
                    gv1Format()

                End If

            End If

        Catch ex As Exception
            lblMsgs.Text = ex.Message.ToString()
        End Try

    End Sub
    Sub gv1Format()
        Try
            For Each col As GridViewColumn In gv1.Columns

                If col.Name = "IsChecked" Then
                    'col.Width = 50
                    col.HeaderText = "Select"
                    col.ReadOnly = False
                Else
                    'col.Width = 150
                    col.ReadOnly = True
                End If
            Next

        Catch ex As Exception
            lblMsgs.Text = ex.Message.ToString()
        End Try
    End Sub
    Private Sub LoadData_BySqlReader(ByVal mySQLQuery As String)
        Dim connetionString As String
        Dim sqlCnn As SqlConnection
        Dim sqlCmd As SqlCommand
        Dim sql As String

        sql = mySQLQuery

        connetionString = objCommonVar.ConnString
        sqlCnn = New SqlConnection(connetionString)
        Try
            sqlCnn.Open()
            sqlCmd = New SqlCommand(sql, sqlCnn)
            'sqlCmd.CommandTimeout = 600
            Dim sqlReader As SqlDataReader = sqlCmd.ExecuteReader()
            Dim dt As New DataTable
            dt.Columns.Add("IsChecked", GetType(Boolean))
            dt.Columns.Add("Code", GetType(String))
            dt.Columns.Add("Caption", GetType(String))
            dt.Columns.Add("Description", GetType(String))

            While sqlReader.Read()
                Dim dr As DataRow = dt.NewRow()
                dr("IsChecked") = sqlReader("IsChecked")
                dr("Code") = sqlReader("Code")
                dr("Caption") = sqlReader("Caption")
                dr("Description") = sqlReader("Description")

                dt.Rows.Add(dr)
            End While
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.DataSource = dt
            End If
            gv1.Columns("Code").IsVisible = False
            'For Each col As GridViewColumn In gv1.Columns
            '    col.BestFit()
            'Next
            gv1.Columns("IsChecked").Width = 55
            gv1.Columns("Caption").Width = 190
            gv1.Columns("Description").Width = 650
            gv1.AutoSizeRows = True
            sqlReader.Close()
            sqlCmd.Dispose()
            sqlCnn.Close()
        Catch ex As Exception
            lblMsgs.Text = ex.Message
        End Try
    End Sub
    Sub ShowData()
        Try
            qry = "Select cast(0 as bit) as IsChecked ,TSPL_NOTIFICATION_HEAD.Code,TSPL_NOTIFICATION_HEAD.Notification_Caption as Caption,TSPL_NOTIFICATION_HEAD.Notification_Text as Description" &
                " From TSPL_NOTIFICATION_DETAIL LEFT OUTER Join TSPL_NOTIFICATION_HEAD On TSPL_NOTIFICATION_HEAD.Code = TSPL_NOTIFICATION_DETAIL.Code LEFT OUTER Join TSPL_EMPLOYEE_MASTER On TSPL_EMPLOYEE_MASTER.EMP_CODE  = TSPL_NOTIFICATION_DETAIL.User_Name " &
                " LEFT OUTER JOIN TSPL_USER_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE  = TSPL_USER_MASTER.EmployeeCode" &
                " left join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_Notification_HEAD.Notification_From_Department_Code and TSPL_GL_SEGMENT_CODE.Seg_No=3" &
             " where TSPL_NOTIFICATION_DETAIL.Sender_Replay=0 and TSPL_USER_MASTER.user_code='" + objCommonVar.CurrentUserCode + "' and TSPL_GL_SEGMENT_CODE.Description='" + StrDepartment + "'"

            If clsCommon.myLen(StrTankerDocType) > 0 Then
                qry += " and TSPL_NOTIFICATION_HEAD.Notification_Tanker_Doc_Type=(case when  '" + StrTankerDocType + "'='Plant/MCC' then 'MccProc' when  '" + StrTankerDocType + "'='Contractor' then 'BulkProc'  END)"
            End If

            qry += " order by TSPL_NOTIFICATION_HEAD.Code"
            gv1.DataSource = Nothing
            LoadData_BySqlReader(qry)

            If gv1.Rows.Count <= 0 Then
                Me.Close()
            Else
                gv1Format()
            End If

        Catch ex As Exception
            lblMsgs.Text = ex.Message.ToString()
        End Try
    End Sub
    Private Sub btnShow_Click(sender As Object, e As EventArgs)
        ShowData()
    End Sub

    Public Sub SaveData()
        Try

            For Each row As GridViewRowInfo In gv1.Rows
                If row.Cells("IsChecked").Value = True Then
                    Dim qryUpdate As String = "Update TSPL_NOTIFICATION_DETAIL set Sender_Replay=1 from TSPL_NOTIFICATION_DETAIL LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE  = TSPL_NOTIFICATION_DETAIL.User_Name LEFT OUTER JOIN TSPL_USER_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE  = TSPL_USER_MASTER.EmployeeCode WHERE TSPL_NOTIFICATION_DETAIL.code='" + clsCommon.myCstr(row.Cells("Code").Value) + "' and TSPL_USER_MASTER.user_code='" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qryUpdate)
                End If
            Next

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            Dim unchecks As Integer = 0
            For Each r As GridViewRowInfo In gv1.Rows
                If r.Cells("IsChecked").Value = False Then
                    unchecks += 1
                End If
            Next
            If unchecks = gv1.Rows.Count Then
                lblMsgs.Text = "You have not Selected any Notification !"
                Exit Sub
            Else
                SaveData()
                lblMsgs.Text = "Done"
            End If

        Catch ex As Exception
            lblMsgs.Text = ex.Message
        End Try
    End Sub

    Private Sub chkSelectAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkSelectAll.ToggleStateChanged
        Try
            If args.ToggleState = ToggleState.On Then
                For Each row As GridViewRowInfo In gv1.Rows
                    row.Cells("IsChecked").Value = True
                Next
            Else
                For Each row As GridViewRowInfo In gv1.Rows
                    row.Cells("IsChecked").Value = False
                Next
            End If

        Catch ex As Exception
        End Try
    End Sub



End Class
