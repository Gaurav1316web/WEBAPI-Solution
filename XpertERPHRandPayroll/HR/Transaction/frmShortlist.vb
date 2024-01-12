' ----------------- Created By Anubhooti On 11-Aug-2014 Against -------------------- '
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
Imports System.IO
Imports XpertERPEngine

Public Class FrmShortlist
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False

    Const colApplicantCode As String = "Applicant Code"
    Const colName As String = "Name"
    Const colShort As String = "Short"
    Const colRejected As String = "Rejected"
    Const colHideRejected As String = "HideRejected"
    Const colHideShort As String = "HideShort"
    Const colApp_Desp As String = "Desp"
    Const colAdd As String = "Address"
    Const colPost As String = "Posted"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmShortlist)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnpost.Visible = MyBase.isPostFlag
    End Sub

    Private Function AllowToSave() As Boolean
        btnsave.Focus()
        If clsCommon.myLen(txtrequisitioncode.Value) < 1 Then
            clsCommon.MyMessageBoxShow(Me, "Please select a requisition code ", Me.Text)
            Return False
        End If
        Return True
    End Function
    Public Sub Reset()
        txtrequisitioncode.MyReadOnly = False
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        txtrequisitioncode.Value = ""
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim strquery As String = ""
        txtrequisitioncode.MyReadOnly = True
        Dim obj As New ClsShortlist
        obj = obj.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Requisition_Code) > 0) Then
            txtrequisitioncode.Value = obj.Requisition_Code
            If clsCommon.myLen(txtrequisitioncode.Value) > 0 Then
                strquery += " SELECT CAST(ISNULL(Short,0) as Bit) as [Short],CAST(ISNULL(Rejected,0) as Bit) as [Rejected], APPLICANT_CODE As [Applicant Code],First_Name + ' ' + Middle_Name + ' ' + Last_Name As [Name] ,Applicant_Description As Desp,"
                strquery += " Add1 + ' ' + Add2 + ' ' + Add3 + ' ' + Add4 As Address ,Posted,CASE When Maritial_Status ='M' Then 'Married' When Maritial_Status ='U' Then 'Unmarried' When Maritial_Status ='D' Then 'Divorced' End As [Maritial Status] ,Telephone_No As [Telephone No],Email,CAST(ISNULL(Short,0) as Bit)  As HideShort,CAST(ISNULL(Rejected,0) as Bit)  As HideRejected FROM TSPL_HR_APPLICANT_ENTRY Where Requisition_Code ='" + txtrequisitioncode.Value + "'"
            End If
            gv1.DataSource = clsDBFuncationality.GetDataTable(strquery)
            FormatGrid()

            btnpost.Enabled = True
            btnsave.Enabled = True
            btnsave.Text = "Save"
        End If
    End Sub
    Public Sub savedata()
        Try
            Dim currentdate As Date = Date.Today
            Dim IsShort As Integer
            Dim qry1 As String
            Dim SHORT_DATE As String
            Dim SHORT_BY As String
            Dim IsReject As Integer
            'Dim RejQry As String
            Dim Reject_Date As String
            Dim Reject_By As String
            Dim ifposted As Integer = 0

            If AllowToSave() Then

                For i As Integer = 0 To gv1.Rows.Count - 1
                    '' Short Listed Applicants
                    If CBool(gv1.Rows(i).Cells(colShort).Value) = True Then
                        If (gv1.Rows(i).Cells(colShort).Value <> gv1.Rows(i).Cells(colHideShort).Value) Then
                            IsShort = 1
                            SHORT_DATE = "'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "'"
                            SHORT_BY = "'" + objCommonVar.CurrentUserCode + "'"

                            qry1 = "UPDATE TSPL_HR_APPLICANT_ENTRY SET SHORT =" + clsCommon.myCstr(IsShort) + ", SHORT_DATE = " + SHORT_DATE + " ,SHORT_BY = " + SHORT_BY + ",REJECTED =" + clsCommon.myCstr("0") + ", REJECTED_DATE = NULL ,REJECTED_BY = NULL WHERE APPLICANT_CODE ='" + clsCommon.myCstr(gv1.Rows(i).Cells(colApplicantCode).Value) + "' AND Requisition_Code ='" + txtrequisitioncode.Value + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry1)
                            'myMessages.insert()
                            ifposted = ifposted + 1

                        End If
                        '' Rejected Applicants
                    ElseIf CBool(gv1.Rows(i).Cells(colRejected).Value) = True Then
                        If (gv1.Rows(i).Cells(colRejected).Value <> gv1.Rows(i).Cells(colHideRejected).Value) Then
                            IsReject = 1
                            Reject_Date = "'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "'"
                            Reject_By = "'" + objCommonVar.CurrentUserCode + "'"

                            qry1 = "UPDATE TSPL_HR_APPLICANT_ENTRY SET REJECTED =" + clsCommon.myCstr(IsReject) + ", REJECTED_DATE = " + Reject_Date + " ,REJECTED_BY = " + Reject_By + ",SHORT =" + clsCommon.myCstr("0") + ", SHORT_DATE = NULL ,SHORT_BY = NULL WHERE APPLICANT_CODE ='" + clsCommon.myCstr(gv1.Rows(i).Cells(colApplicantCode).Value) + "' AND Requisition_Code ='" + txtrequisitioncode.Value + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry1)
                            'myMessages.insert()
                            ifposted = ifposted + 1

                        End If
                    Else
                        qry1 = "UPDATE TSPL_HR_APPLICANT_ENTRY SET SHORT =" + clsCommon.myCstr("0") + ", SHORT_DATE = NULL ,SHORT_BY = NULL, REJECTED =" + clsCommon.myCstr("0") + ", REJECTED_DATE = NULL ,REJECTED_BY = NULL  WHERE APPLICANT_CODE ='" + clsCommon.myCstr(gv1.Rows(i).Cells(colApplicantCode).Value) + "' AND Requisition_Code ='" + txtrequisitioncode.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry1)

                        ifposted = ifposted + 1
                    End If
                Next
            ''For Post Working 20-Aug
            LoadData(txtrequisitioncode.Value, NavigatorType.Current)
                'If ifposted = 0 Then
                ' clsCommon.MyMessageBoxShow("You can not save!either all entries are already posted or no shortlisted applicant found.")
                'Else
                myMessages.insert()
                'End If
            End If

            btnsave.Text = "Save"
            btnpost.Enabled = True

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub Postdata()
        Try
            Dim currentdate As Date = Date.Today
            Dim IsPost As Integer
            Dim qry As String
            Dim GridRow As Integer = 0
            If AllowToSave() Then
                If (myMessages.postConfirm()) Then
                    For i As Integer = 0 To gv1.Rows.Count - 1
                        '' Short Listed Applicants
                        If (CBool(gv1.Rows(i).Cells(colShort).Value) = True AndAlso CBool(gv1.Rows(i).Cells(colHideShort).Value)) = True Then
                            If CBool(gv1.Rows(i).Cells(colShort).Value) = True AndAlso clsCommon.myCdbl(gv1.Rows(i).Cells(colPost).Value) = 0 Then
                                GridRow = GridRow + 1
                                IsPost = 1
                                qry = "UPDATE TSPL_HR_APPLICANT_ENTRY SET Posted=" + clsCommon.myCstr(IsPost) + ",Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "',Posted_By='" + objCommonVar.CurrentUserCode + "' Where APPLICANT_CODE ='" + clsCommon.myCstr(gv1.Rows(i).Cells(colApplicantCode).Value) + "' and Requisition_Code='" + txtrequisitioncode.Value + "'"
                                clsDBFuncationality.ExecuteNonQuery(qry)
                                common.clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully", Me.Text)
                                'ElseIf (gv1.Rows(i).Cells(colShort).Value <> gv1.Rows(i).Cells(colHideShort).Value) Then
                                '    common.clsCommon.MyMessageBoxShow("please save your entry ( Applicant Code :" + clsCommon.myCstr(gv1.Rows(i).Cells(colApplicantCode).Value) + ") first")
                            End If
                        End If
                        '' Rejected Applicants
                        If (CBool(gv1.Rows(i).Cells(colRejected).Value) = True AndAlso CBool(gv1.Rows(i).Cells(colHideRejected).Value)) = True Then
                            If CBool(gv1.Rows(i).Cells(colRejected).Value) = True AndAlso clsCommon.myCdbl(gv1.Rows(i).Cells(colPost).Value) = 0 Then
                                GridRow = GridRow + 1
                                IsPost = 1
                                qry = "UPDATE TSPL_HR_APPLICANT_ENTRY SET Posted=" + clsCommon.myCstr(IsPost) + ",Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "',Posted_By='" + objCommonVar.CurrentUserCode + "' Where APPLICANT_CODE ='" + clsCommon.myCstr(gv1.Rows(i).Cells(colApplicantCode).Value) + "' and Requisition_Code='" + txtrequisitioncode.Value + "'"
                                clsDBFuncationality.ExecuteNonQuery(qry)
                                common.clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully", Me.Text)
                            End If
                        End If
                    Next
                    If GridRow <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "No data found to post", Me.Text)
                    End If
                End If
            End If

            btnsave.Text = "Save"
            btnpost.Enabled = True

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Sub FormatGrid()
        If gv1.Rows.Count > 0 Then
            gv1.Columns("Short").Width = 50
            gv1.Columns("Rejected").Width = 50
            gv1.Columns("Applicant Code").Width = 120
            gv1.Columns("Name").Width = 120
            gv1.Columns("Desp").Width = 150
            gv1.Columns("Address").Width = 200
            gv1.Columns("Telephone No").Width = 120
            gv1.Columns("Maritial Status").Width = 120
            gv1.Columns("Email").Width = 120
            gv1.Columns("Short").ReadOnly = False
            gv1.Columns("Applicant Code").ReadOnly = True
            gv1.Columns("Name").ReadOnly = True
            gv1.Columns("Desp").ReadOnly = True
            gv1.Columns("Address").ReadOnly = True
            gv1.Columns("Maritial Status").ReadOnly = True
            gv1.Columns("TELEPHONE NO").ReadOnly = True
            gv1.Columns("Email").ReadOnly = True
            gv1.Columns("Posted").ReadOnly = True
            gv1.Columns("Posted").IsVisible = False
            gv1.Columns("HideShort").IsVisible = False
            gv1.Columns("HideRejected").IsVisible = False
            'gv1.MasterTemplate.BestFitColumns()
        End If
    End Sub
    Private Sub txtrequisitioncode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtrequisitioncode._MYValidating
        'If txtrequisitioncode.MyReadOnly OrElse isButtonClicked Then
        '    Dim qry As String = "select distinct Requisition_Code As Code from  TSPL_HR_APPLICANT_ENTRY"
        '    txtrequisitioncode.Value = clsCommon.ShowSelectForm("TSPL_HR_APPLICANT_ENTRY", qry, "Code", "", txtrequisitioncode.Value, "", isButtonClicked)
        '    'funfill()
        '    LoadData(txtrequisitioncode.Value, NavigatorType.Current)
        'End If
        Dim qry As String = "select distinct Requisition_Code As Code from  TSPL_HR_APPLICANT_ENTRY"
        txtrequisitioncode.Value = clsCommon.ShowSelectForm("HRAppShort", qry, "Code", "", txtrequisitioncode.Value, "", isButtonClicked)
        If clsCommon.myLen(txtrequisitioncode.Value) > 0 Then
            LoadData(txtrequisitioncode.Value, NavigatorType.Current)
        Else
            Reset()
        End If
    End Sub

    Private Sub FrmShortlist_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnpost.Enabled Then
            Postdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
            GC.Collect()
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtrequisitioncode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtrequisitioncode._MYNavigator
        Try
            LoadData(txtrequisitioncode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmShortlist_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save Trasnaction")
        ButtonToolTip.SetToolTip(btnpost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N New Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        
        If e.Column Is gv1.Columns(colShort) OrElse e.Column Is gv1.Columns(colRejected) Then
            gv1.CurrentRow.Cells(colShort).ReadOnly = False
            gv1.CurrentRow.Cells(colRejected).ReadOnly = False

            If CBool(gv1.CurrentRow.Cells(colShort).Value) = True Then
                gv1.CurrentRow.Cells(colRejected).ReadOnly = True
                gv1.CurrentRow.Cells(colRejected).Value = 0
                gv1.CurrentRow.Cells(colShort).ReadOnly = False
            ElseIf CBool(gv1.CurrentRow.Cells(colRejected).Value) = True Then
                gv1.CurrentRow.Cells(colShort).ReadOnly = True
                gv1.CurrentRow.Cells(colShort).Value = 0
                gv1.CurrentRow.Cells(colRejected).ReadOnly = False
                ' Else
                'gv1.CurrentRow.Cells(colRejected).ReadOnly = False
                'gv1.CurrentRow.Cells(colShort).ReadOnly = False
                ' gv1.CurrentRow.Cells(colRejected).Value = 0
                'gv1.CurrentRow.Cells(colShort).Value = 0
            End If
            If CBool(gv1.CurrentRow.Cells(colPost).Value) = True Then
                gv1.CurrentRow.Cells(colRejected).ReadOnly = True
                gv1.CurrentRow.Cells(colShort).ReadOnly = True
            End If
        End If
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        savedata()
    End Sub

    Private Sub btnpost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpost.Click
        Postdata()
    End Sub

    Private Sub gv1_RowFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.RowFormattingEventArgs) Handles gv1.RowFormatting

        If e.RowElement.RowInfo.Cells(colHideShort).Value = False AndAlso e.RowElement.RowInfo.Cells(colPost).Value = 0 Then
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = Color.PapayaWhip
            ' e.RowElement.RowInfo.Cells(colShort).ReadOnly = False
        ElseIf e.RowElement.RowInfo.Cells(colHideShort).Value = True AndAlso e.RowElement.RowInfo.Cells(colPost).Value = 1 Then
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = Color.LightGreen
            'e.RowElement.RowInfo.Cells(colShort).ReadOnly = True
        ElseIf e.RowElement.RowInfo.Cells(colHideShort).Value = True AndAlso e.RowElement.RowInfo.Cells(colPost).Value = 0 Then
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = Color.PapayaWhip
            'e.RowElement.RowInfo.Cells(colShort).ReadOnly = False
        ElseIf e.RowElement.RowInfo.Cells(colHideRejected).Value = False AndAlso e.RowElement.RowInfo.Cells(colPost).Value = 0 Then
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = Color.PapayaWhip
            'e.RowElement.RowInfo.Cells(colRejected).ReadOnly = False
        ElseIf e.RowElement.RowInfo.Cells(colHideRejected).Value = True AndAlso e.RowElement.RowInfo.Cells(colPost).Value = 1 Then
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = Color.LightSalmon
            'e.RowElement.RowInfo.Cells(colRejected).ReadOnly = True
        ElseIf e.RowElement.RowInfo.Cells(colHideRejected).Value = True AndAlso e.RowElement.RowInfo.Cells(colPost).Value = 0 Then
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = Color.Tan
            'e.RowElement.RowInfo.Cells(colRejected).ReadOnly = False
        Else
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = Color.AliceBlue
            'e.RowElement.RowInfo.Cells(colShort).ReadOnly = True
        End If

    End Sub
    
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

   
    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub ChkShortListed_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        
    End Sub

    Private Sub ChkRejected_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ChkNotDefined_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
End Class
