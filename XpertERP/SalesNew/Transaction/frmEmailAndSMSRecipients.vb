Imports common

Public Class FrmEmailAndSMSRecipients
    'Public strTransTrype As String = "POS"

    Const colEmpCode As String = "COLEMPCODE"
    Const colEmpName As String = "COLEMPNAME"
    Const colPhoneNo As String = "COLPHONENO"
    Const colEmailID As String = "COLEMAILID"
    Const colIsSendEmail As String = "COLISSENDEMAIL"
    Const colToOrCC As String = "COLTOORCC"
    Const colIsSendSMS As String = "COLISSENDSMS"

    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmEmailAndSMSRecipients_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LoadData()
        gv1.Rows.AddNew()
    End Sub

    Sub LoadData()
        LoadBlankGrid()

        Dim Arr As List(Of clsEmailSMSRecipients) = clsEmailSMSRecipients.GetData(clsEmailAndSMSRecipients.strTransTrype)
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            For Each objTr As clsEmailSMSRecipients In Arr
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colEmpCode).Value = objTr.Emp_code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colEmpName).Value = objTr.Emp_Name
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPhoneNo).Value = objTr.Phone
                gv1.Rows(gv1.Rows.Count - 1).Cells(colEmailID).Value = objTr.EMail_ID
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSendEmail).Value = objTr.Is_Send_Email
                gv1.Rows(gv1.Rows.Count - 1).Cells(colToOrCC).Value = objTr.To_Or_CC
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSendSMS).Value = objTr.Is_Send_SMS
            Next
        End If
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoEmpCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEmpCode.FormatString = ""
        repoEmpCode.HeaderText = "Employee Code"
        repoEmpCode.Name = colEmpCode
        repoEmpCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoEmpCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoEmpCode.Width = 150
        gv1.MasterTemplate.Columns.Add(repoEmpCode)

        Dim repoEmpName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEmpName.FormatString = ""
        repoEmpName.HeaderText = "Employee Name"
        repoEmpName.Name = colEmpName
        repoEmpName.Width = 180
        repoEmpName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoEmpName)

        Dim repoPhoneNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPhoneNo.FormatString = ""
        repoPhoneNo.HeaderText = "Phone No"
        repoPhoneNo.Name = colPhoneNo
        repoPhoneNo.Width = 100
        repoPhoneNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPhoneNo)

        Dim repoEmailID As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEmailID.FormatString = ""
        repoEmailID.HeaderText = "EMail ID"
        repoEmailID.Name = colEmailID
        repoEmailID.Width = 150
        repoEmailID.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoEmailID)

        Dim repoIsSendEMail As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSendEMail.Width = 100
        repoIsSendEMail.HeaderText = "Send EMail"
        repoIsSendEMail.Name = colIsSendEmail
        repoIsSendEMail.ReadOnly = False
        repoIsSendEMail.IsVisible = True
        repoIsSendEMail.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSendEMail)

        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Row Type"
        repoRowType.Name = colToOrCC
        repoRowType.Width = 100
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = GetItemType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Code"
        gv1.MasterTemplate.Columns.Add(repoRowType)


        Dim repoIsSendSMS As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSendSMS.Width = 100
        repoIsSendSMS.HeaderText = "Send SMS"
        repoIsSendSMS.Name = colIsSendSMS
        repoIsSendSMS.ReadOnly = False
        repoIsSendSMS.IsVisible = True
        repoIsSendSMS.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSendSMS)

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 35

    End Sub


    Public Shared Function GetItemType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "To"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "CC"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim Arr As New List(Of clsEmailSMSRecipients)
            For Each grow As GridViewRowInfo In gv1.Rows
                Dim objTr As New clsEmailSMSRecipients()
                objTr.Emp_code = clsCommon.myCstr(grow.Cells(colEmpCode).Value)
                objTr.Is_Send_Email = clsCommon.myCBool(grow.Cells(colIsSendEmail).Value)
                objTr.Is_Send_SMS = clsCommon.myCBool(grow.Cells(colIsSendSMS).Value)
                objTr.To_Or_CC = clsCommon.myCstr(grow.Cells(colToOrCC).Value)

                If clsCommon.myLen(objTr.Emp_code) > 0 Then
                    Arr.Add(objTr)
                End If
            Next
            If clsEmailSMSRecipients.SaveData(Arr, clsEmailAndSMSRecipients.strTransTrype) Then
                clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colEmpCode) OrElse e.Column Is gv1.Columns(colIsSendEmail) OrElse e.Column Is gv1.Columns(colIsSendSMS) OrElse e.Column Is gv1.Columns(colToOrCC) Then
                        If e.Column Is gv1.Columns(colEmpCode) Then
                            OpenEmployee(False)
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub OpenEmployee(ByVal isButtonClick As Boolean)
        Dim qry As String = "select EMP_CODE as Code,Emp_Name as Name,Phone as [Mobile No],EMail_ID as [Email ID] from TSPL_EMPLOYEE_MASTER "
        gv1.CurrentRow.Cells(colEmpCode).Value = clsCommon.ShowSelectForm("EMailSmsReceiptEmp", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colEmpCode).Value), "Code", isButtonClick)

        qry = "select Emp_Name,Phone,EMail_ID from TSPL_EMPLOYEE_MASTER where  EMP_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colEmpCode).Value) + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv1.CurrentRow.Cells(colEmpName).Value = clsCommon.myCstr(dt.Rows(0)("Emp_Name"))
            gv1.CurrentRow.Cells(colPhoneNo).Value = clsCommon.myCstr(dt.Rows(0)("Phone"))
            gv1.CurrentRow.Cells(colEmailID).Value = clsCommon.myCstr(dt.Rows(0)("EMail_ID"))
        End If
    End Sub


    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
            End If
        End If

    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow

    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
End Class
