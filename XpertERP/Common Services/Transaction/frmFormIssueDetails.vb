Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmFormIssueDetails
    Inherits FrmMainTranScreen

    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Public Sub SetLength()
        fndFormIssueNo.MyMaxLength = 30
        txtDemandNo.MaxLength = 30
        txtFormSeries.MaxLength = 6
        txtRemarks.MaxLength = 100
        txtCommetns.MaxLength = 100
    End Sub
    Private Sub FrmFormIssueDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetLength()
        dtpDemandDate.Value = clsCommon.GETSERVERDATE
        SetUserMgmtNew()
        fndFormIssueNo.MyReadOnly = True
        AddNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New Trasnaction")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FormIssue)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Dim intcount, intFromN0, intToNo, intLineNo As Integer
        Dim strFormNo As String
        If txtFromNo.Text = "" Then
            common.clsCommon.MyMessageBoxShow("Please enter From No")
            Exit Sub
        ElseIf txtToNo.Text = "" Then
            common.clsCommon.MyMessageBoxShow("Please enter To No")
            Exit Sub
        ElseIf Val(txtFromNo.Text) > Val(txtToNo.Text) Then
            common.clsCommon.MyMessageBoxShow("To No should be greater than From No")
            txtFromNo.Focus()
            Exit Sub
        End If
        Dim intTotalNo As Integer
        intFromN0 = txtFromNo.Text
        intToNo = txtToNo.Text
        intTotalNo = (txtToNo.Text - txtFromNo.Text) + 1
        txtFormIssued.Text = intTotalNo
        intcount = intcount + txtFromNo.Text

        intLineNo = 0
        DgvFormIssue.Rows.Clear()
        DgvFormIssue.DataSource = Nothing
        For intcount = intFromN0 To intToNo
            'intcount = intcount + 1
            intcount = intcount
            intLineNo = intLineNo + 1
            strFormNo = txtFormSeries.Text & intcount
            Dim grow As GridViewRowInfo = DgvFormIssue.Rows.AddNew()
            grow.Cells("lineno").Value = intLineNo
            grow.Cells("No").Value = strFormNo
            grow.Cells("Date").Value = dtpDemandDate.Value
            grow.Cells("remarks").Value = ""

        Next
        intcount = intcount
        blnLoaddata = False
    End Sub

    Function AllowToSave() As Boolean
        'If clsCommon.myLen(fndFormIssueNo.Value) <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please Enter Form Issue no")
        '    fndFormIssueNo.Focus()
        '    Return False
        'End If
        If clsCommon.myLen(txtDemandNo.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Enter Demand no")
            fndFormIssueNo.Focus()
            Return False
        End If
        If clsCommon.myLen(ddlFromType.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Form type")
            ddlFromType.Focus()
            Return False
        End If
        If clsCommon.myLen(txtFormSeries.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please enter Form Series")
            txtFormSeries.Focus()
            Return False
        End If
        If clsCommon.myLen(txtFromNo.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please enter Form No")
            txtFromNo.Focus()
            Return False
        End If
        If clsCommon.myLen(txtToNo.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please enter To No")
            txtToNo.Focus()
            Return False
        End If
        If (txtFromNo.Text) > txtToNo.Text Then
            common.clsCommon.MyMessageBoxShow("To No should be greater than From No")
            txtFromNo.Focus()
            Return False
        End If
        If clsCommon.myLen(txtFormIssued.Text) = 0 Then
            common.clsCommon.MyMessageBoxShow("Please Click on Go button")
            txtToNo.Focus()
            Return False
        End If
        Dim intLineNo As Integer
        Dim strFormNo As String
        For Each grow As GridViewRowInfo In DgvFormIssue.Rows
            intLineNo = clsCommon.myCdbl(grow.Cells("LineNo").Value)
            strFormNo = clsCommon.myCstr(grow.Cells("No").Value)
            If clsCommon.myLen(grow.Cells("Expirydate").Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please enter Expiry Date for '" + strFormNo + "'")
                Return False
            End If
        Next
        Return True
    End Function


    Sub SaveData()
        Try
            ' Dim strFormIssueNo As String
            If (AllowToSave()) Then
                Dim obj As New clsFormIssuehead()
                obj.FormIssue_no = fndFormIssueNo.Value
                obj.DemandNo = txtDemandNo.Text
                obj.Demand_Date = clsCommon.GetPrintDate(dtpDemandDate.Value, "dd/MMM/yyyy")
                obj.FormCode = ddlFromType.Value
                obj.FormName = txtFormTypeDesc.Text
                obj.FormSeries = txtFormSeries.Text
                obj.FromNo = clsCommon.myCdbl(txtFromNo.Text)
                obj.ToNo = clsCommon.myCdbl(txtToNo.Text)
                obj.TotalForms = clsCommon.myCdbl(txtFormIssued.Text)
                obj.Remarks = txtRemarks.Text
                obj.Comments = txtCommetns.Text


                obj.Arr = New List(Of clsFormIssueDetail)
                For Each grow As GridViewRowInfo In DgvFormIssue.Rows
                    Dim objTr As New clsFormIssueDetail()
                    objTr.Line_no = clsCommon.myCdbl(grow.Cells("LineNo").Value)
                    objTr.FormIssue_no = fndFormIssueNo.Value
                    objTr.FormNo = clsCommon.myCstr(grow.Cells("No").Value)
                    objTr.FormDate = clsCommon.GetPrintDate(grow.Cells("Date").Value, "dd/MMM/yyyy")
                    objTr.Expirydate = clsCommon.GetPrintDate(grow.Cells("ExpiryDate").Value, "dd/MMM/yyyy")
                    objTr.Remarks = clsCommon.myCstr(grow.Cells("Remarks").Value)

                    If (clsCommon.myLen(objTr.Line_no) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next

                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
                    Return
                End If

                If (obj.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.FormIssue_no, NavigatorType.Current)

                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try

            'BlankAllControls()
            'LoadBlankGrid()

            Dim obj As New clsFormIssuehead()
            obj = clsFormIssuehead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.FormIssue_no) > 0) Then
                'AddNew()
                DgvFormIssue.Rows.Clear()
                DgvFormIssue.DataSource = Nothing

                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                isInsideLoadData = True
                isNewEntry = False
                btnSave.Text = "Update"
                txtFormSeries.Enabled = False
                txtFromNo.Enabled = False
                txtToNo.Enabled = False


                fndFormIssueNo.Value = obj.FormIssue_no
                txtDemandNo.Text = obj.DemandNo
                dtpDemandDate.Value = obj.Demand_Date
                ddlFromType.Value = obj.FormCode
                txtFormTypeDesc.Text = obj.FormName
                txtFormSeries.Text = obj.FormSeries
                txtFromNo.Text = obj.FromNo
                txtToNo.Text = obj.ToNo
                txtFormIssued.Text = obj.TotalForms
                txtCommetns.Text = obj.Comments
                txtRemarks.Text = obj.Remarks


                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsFormIssueDetail In obj.Arr
                        DgvFormIssue.Rows.AddNew()
                        DgvFormIssue.Rows(DgvFormIssue.Rows.Count - 1).Cells("LineNo").Value = objTr.Line_no
                        DgvFormIssue.Rows(DgvFormIssue.Rows.Count - 1).Cells("No").Value = objTr.FormNo
                        If objTr.FormDate.HasValue Then
                            DgvFormIssue.Rows(DgvFormIssue.Rows.Count - 1).Cells("DATE").Value = objTr.FormDate
                        End If
                        If objTr.Expirydate.HasValue Then
                            DgvFormIssue.Rows(DgvFormIssue.Rows.Count - 1).Cells("Expirydate").Value = objTr.Expirydate
                        End If
                        DgvFormIssue.Rows(DgvFormIssue.Rows.Count - 1).Cells("Remarks").Value = objTr.Remarks
                    Next

                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Sub AddNew()
        BlankAllControls()

        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
    End Sub
    Private Sub ddlFromType__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles ddlFromType._MYValidating
        Dim qry As String = "select form_code as [Code],form_name as [Form Name] from TSPL_Form_Master"
        ddlFromType.Value = clsCommon.ShowSelectForm("fmForm_Type", qry, "Code", "", ddlFromType.Value, "Code", isButtonClicked)
        txtFormTypeDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select form_name  from TSPL_Form_Master where Form_Code='" + ddlFromType.Value + "'"))
    End Sub


    Private Sub fndFormIssueNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndFormIssueNo._MYNavigator
        Try
            LoadData(fndFormIssueNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub BlankAllControls()
        fndFormIssueNo.Value = ""
        txtDemandNo.Text = ""
        ddlFromType.Value = ""
        txtFormTypeDesc.Text = ""
        txtFormSeries.Text = ""
        txtFromNo.Text = ""
        txtToNo.Text = ""
        txtCommetns.Text = ""
        txtRemarks.Text = ""
        txtFormIssued.Text = ""
        txtFormSeries.Enabled = True
        txtFromNo.Enabled = True
        txtToNo.Enabled = True
        dtpDemandDate.Value = clsCommon.GETSERVERDATE()
    End Sub
    Private Sub fndFormIssueNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndFormIssueNo._MYValidating
        Try
            Dim qry As String = "select TSPL_formissue_head.FormIssue_no as [Code],TSPL_formissue_head.demandNo as [demand No],TSPL_formissue_head.demand_date as [Demand Date], " & _
            "TSPL_formissue_head.formcode as [Form type],TSPL_formissue_head.formname as [Form Name],TSPL_formissue_head.formseries as [Form Series], " & _
            "TSPL_formissue_head.fromno,TSPL_formissue_head.Tono,TSPL_formissue_head.totalforms  from TSPL_formissue_head"

            Dim whrClas As String = ""

            LoadData(clsCommon.ShowSelectForm("FormissueNoFilter", qry, "Code", whrClas, fndFormIssueNo.Value, "Code", isButtonClicked), NavigatorType.Current)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Dim blnLoaddata As Boolean = False
    Private Sub DgvFormIssue_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles DgvFormIssue.CellValueChanged

        Dim strDate As String
        If e.Column Is DgvFormIssue.Columns("Expirydate") And e.RowIndex = 0 And blnLoaddata = False Then
            If (clsCommon.myLen(DgvFormIssue.CurrentRow.Cells("Expirydate").Value)) > 0 Then
                strDate = DgvFormIssue.Rows(0).Cells("Expirydate").Value
                blnLoaddata = True
                For Each grow As GridViewRowInfo In DgvFormIssue.Rows
                    grow.Cells("ExpiryDate").Value = strDate
                Next
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        AddNew()
        DgvFormIssue.Rows.Clear()
        DgvFormIssue.DataSource = Nothing
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()

        Try
            If (myMessages.deleteConfirm()) Then
                If (clsFormIssuehead.DeleteData(fndFormIssueNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfndfully ")
                    AddNew()
                    DgvFormIssue.Rows.Clear()
                    DgvFormIssue.DataSource = Nothing
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub txtFromNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFromNo.KeyPress
        If ((e.KeyChar >= Chr(46)) And (e.KeyChar <= Chr(58))) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtToNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtToNo.KeyPress
        If ((e.KeyChar >= Chr(46)) And (e.KeyChar <= Chr(58))) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtFormSeries_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFormSeries.TextChanged
        DgvFormIssue.Rows.Clear()
        DgvFormIssue.DataSource = Nothing

    End Sub

    Private Sub txtFromNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFromNo.TextChanged
        DgvFormIssue.Rows.Clear()
        DgvFormIssue.DataSource = Nothing
        txtFormIssued.Text = ""
    End Sub

    Private Sub txtToNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtToNo.TextChanged
        DgvFormIssue.Rows.Clear()
        DgvFormIssue.DataSource = Nothing
        txtFormIssued.Text = ""
    End Sub

    Private Sub FrmFormIssueDetails_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            postdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()

        End If

    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        postdata()
    End Sub
    Sub postdata()

    End Sub
End Class
