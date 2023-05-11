Imports common
Imports System.Data.SqlClient

Public Class FrmAccountSubGroup
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
#End Region
    Private Sub fndaccgp__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndaccgp._MYNavigator
        Try
            LoadData(fndaccgp.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndaccgp__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndaccgp._MYValidating
        Dim str As String = "select count(*) from TSPL_ACCOUNT_SUB_GROUPS where Account_Sub_Group_Code ='" + fndaccgp.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            fndaccgp.MyReadOnly = False
        Else
            fndaccgp.MyReadOnly = True
        End If
        If fndaccgp.MyReadOnly OrElse isButtonClicked Then
            fndaccgp.Value = ClsAccountSubGroup.getFinder("", fndaccgp.Value, isButtonClicked)
            If fndaccgp.Value <> "" Then
                LoadData(fndaccgp.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub FrmAccountMainGroup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub FrmAccountMainGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        funReset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N for New Transaction")
    End Sub
#Region "Functions"
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.AccountSubGroup)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag

        If btnsave.Visible = True Then
            rmExport.Enabled = True
            rmImport.Enabled = True
        Else
            rmExport.Enabled = False
            rmImport.Enabled = False
        End If

        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Function AllowToSave() As Boolean
        Dim strcode As String = fndaccgp.Value.ToString()
        If clsCommon.myLen(fndaccgp.Value) <= 0 Then
            myMessages.blankValue("Sub group")
            fndaccgp.Focus()
            Return False
            'ElseIf Not IsNumeric(strcode) Then
            '    clsCommon.MyMessageBoxShow("Please enter numeric value in account main group code")
            '    fndaccgp.Focus()
            '    Return False
        ElseIf clsCommon.myLen(txtdes.Text) <= 0 Then
            myMessages.blankValue("Description")
            txtdes.Focus()
            Return False
        ElseIf clsCommon.myLen(txtAccGrp.Value) <= 0 Then
            myMessages.blankValue("Account group")
            txtAccGrp.Focus()
            Return False
        End If
        Dim qry As String = "select count(*) As Row from TSPL_ACCOUNT_GROUPS where Account_Group_Code='" + clsCommon.myCstr(txtAccGrp.Value) + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        If check <= 0 Then
            clsCommon.MyMessageBoxShow("'" & clsCommon.myCstr(txtAccGrp.Value) & "' code does not exists.First create account group entry")
            txtAccGrp.Focus()
            Return False
        End If

        Return True
    End Function
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (ClsAccountSubGroup.DeleteData(fndaccgp.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub funReset()
        isNewEntry = True
        fndaccgp.MyReadOnly = False
        fndaccgp.Value = Nothing
        fndaccgp.Focus()
        txtdes.Text = ""
        txtAccGrp.Value = ""
        lblAccGrp.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Dim obj As New ClsAccountSubGroup()
        obj = ClsAccountSubGroup.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Account_Sub_Group_Code) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            fndaccgp.Value = clsCommon.myCstr(obj.Account_Sub_Group_Code)
            txtdes.Text = clsCommon.myCstr(obj.Account_Sub_Group_Desc)
            txtAccGrp.Value = clsCommon.myCstr(obj.Account_Group_Code)
            If clsCommon.myLen(txtAccGrp.Value) Then
                lblAccGrp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Account_Group_Desc,'') As [Description] From TSPL_ACCOUNT_GROUPS  WHERE Account_Group_Code='" & clsCommon.myCstr(txtAccGrp.Value) & "'"))
            End If

            btnsave.Enabled = True
            btndelete.Enabled = True
            fndaccgp.MyReadOnly = True
        End If

    End Sub
    Public Sub SaveData()
        Try
            If AllowToSave() Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.AccountSubGroup, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim obj As New ClsAccountSubGroup()
                obj.Account_Sub_Group_Code = clsCommon.myCstr(fndaccgp.Value)
                obj.Account_Sub_Group_Desc = clsCommon.myCstr(txtdes.Text)
                obj.Account_Group_Code = clsCommon.myCstr(txtAccGrp.Value)
                'If (obj.SaveData(obj, isNewEntry)) Then
                If (ClsAccountSubGroup.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Account_Sub_Group_Code, NavigatorType.Current)
                End If
                'End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub funClose()
        Me.Close()
        GC.Collect()
    End Sub
#End Region

    Private Sub rmImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim linno As Integer = 0
        If transportSql.importExcel(gv, "Account Sub Group Code", "Description", "Account Group Code") Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New ClsAccountSubGroup()
                    linno += 1

                    Dim strcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If strcode.Length <= 0 Or (String.IsNullOrEmpty(strcode)) Then
                        Throw New Exception("Account sub group code can't be blank at line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf strcode.Length > 30 Then
                        Throw New Exception("Account sub group length can not be more than 30 at line no. " + clsCommon.myCstr(linno) + ".")
                        'ElseIf Not IsNumeric(strcode) Then
                        '    Throw New Exception("Please enter numeric value in account main group code at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Account_Sub_Group_Code = strcode

                    Dim strdes As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If strdes.Length <= 0 Or (String.IsNullOrEmpty(strdes)) Then
                        Throw New Exception("Description can't be blank at line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf strdes.Length > 100 Then
                        Throw New Exception("Description length can not be more than 100 at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Account_Sub_Group_Desc = strdes

                    'Dim Type As String = clsCommon.myCstr(grow.Cells(2).Value)
                    Dim strAccMainGrp As String = clsCommon.myCstr(grow.Cells("Account Group Code").Value)
                    If strAccMainGrp.Length <= 0 Or (String.IsNullOrEmpty(strAccMainGrp)) Then
                        Throw New Exception("Account main group code can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                        'ElseIf Not IsNumeric(strAccMainGrp) Then
                        '    Throw New Exception("Please enter numeric value in account main group code at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strAccMainGrp) > 0 Then
                        Dim qry As String = "select count(*) As Row from TSPL_ACCOUNT_GROUPS where Account_Group_Code='" + strAccMainGrp + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("'" & clsCommon.myCstr(strAccMainGrp) & "' code does not exists at line no. " + clsCommon.myCstr(linno) + ".First create account group entry")
                        End If
                    End If
                    obj.Account_Group_Code = strAccMainGrp

                    ClsAccountSubGroup.SaveData(obj, ClsAccountSubGroup.CheckNewEntry(obj.Account_Sub_Group_Code, trans), trans)
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub rmExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim str As String
        str = "select Account_Sub_Group_Code As [Account Sub Group Code],Account_Sub_Group_Desc as [Description],Account_Group_Code AS [Account Group Code] from TSPL_ACCOUNT_SUB_GROUPS"
        ListImpExpColumnsMandatory = New List(Of String)({"Account Sub Group Code", "Description", "Account Group Code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Account Sub Group Code"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub txtAccGrp__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtAccGrp._MYValidating
        Dim Qry As String = " Select Account_Group_Code AS Code ,ISNULL(Account_Group_Desc,'') As [Description],ISNULL(GROUP_TYPE,'') As [Group Type]  From TSPL_ACCOUNT_GROUPS "
        txtAccGrp.Value = clsCommon.ShowSelectForm("AccGrpFin", Qry, "Code", "", txtAccGrp.Value, "Code", isButtonClicked)
        If clsCommon.myLen(txtAccGrp.Value) > 0 Then
            lblAccGrp.Text = clsDBFuncationality.getSingleValue("Select ISNULL(Account_Group_Desc,'') As Account_Group_Desc from TSPL_ACCOUNT_GROUPS Where Account_Group_Code='" + clsCommon.myCstr(txtAccGrp.Value) + "' ")
        Else
            lblAccGrp.Text = ""
        End If
    End Sub
End Class
