Imports common
Imports System.Data.SqlClient

Public Class FrmAccountMainGroup
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndaccgp__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndaccgp._MYValidating
        Dim str As String = "select count(*) from TSPL_ACCOUNT_MAIN_GROUPS where Account_Main_Group_Code ='" + fndaccgp.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            fndaccgp.MyReadOnly = False
        Else
            fndaccgp.MyReadOnly = True
        End If
        If fndaccgp.MyReadOnly OrElse isButtonClicked Then
            fndaccgp.Value = ClsAccountMainGroup.getFinder("", fndaccgp.Value, isButtonClicked)
            If fndaccgp.Value <> "" Then
                LoadData(fndaccgp.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub FrmAccountMainGroup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.AccountMainGroup)
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
            myMessages.blankValue(Me, "Account Group", Me.Text)
            fndaccgp.Focus()
            Return False
        ElseIf Not IsNumeric(strcode) Then
            clsCommon.MyMessageBoxShow(Me, "Please enter numeric value in account main group code", Me.Text)
            fndaccgp.Focus()
            Return False
        ElseIf clsCommon.myLen(txtdes.Text) <= 0 Then
            myMessages.blankValue(Me, "Description", Me.Text)
            txtdes.Focus()
            Return False
        ElseIf clsCommon.myLen(ddlaccounttype.Text) <= 0 Then
            myMessages.blankValue(Me, "Type", Me.Text)
            ddlaccounttype.Focus()
            Return False
        ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlaccounttype.Text), "Retained Earnings") = CompairStringResult.Equal Then
            Dim sQuery As String = "select count(*) from TSPL_ACCOUNT_MAIN_GROUPS where Group_type='" & ddlaccounttype.Text & "' and account_main_group_code<>'" & clsCommon.myCstr(fndaccgp.Value) & "'"
            Dim Check As Integer = clsDBFuncationality.getSingleValue(sQuery)
            If Check > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Retained Earnings Group is Already Exits.you can not create it again.", Me.Text)
                ddlaccounttype.Focus()
                Return False
            End If
        End If
        
        Return True
    End Function
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (ClsAccountMainGroup.DeleteData(fndaccgp.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub funReset()
        isNewEntry = True
        fndaccgp.MyReadOnly = False
        fndaccgp.Value = Nothing
        fndaccgp.Focus()
        txtdes.Text = ""
        ddlaccounttype.Text = "Balance Sheet"
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Dim obj As New ClsAccountMainGroup()
        obj = ClsAccountMainGroup.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Account_Main_Group_Code) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            fndaccgp.Value = clsCommon.myCstr(obj.Account_Main_Group_Code)
            txtdes.Text = clsCommon.myCstr(obj.Account_Main_Group_Desc)
            If clsCommon.myLen(obj.Group_Type) > 0 Then
                If clsCommon.CompairString(obj.Group_Type, "Balance Sheet") = CompairStringResult.Equal Then
                    Me.ddlaccounttype.Text = "Balance Sheet"
                ElseIf clsCommon.CompairString(obj.Group_Type, "Income Statement") = CompairStringResult.Equal Then
                    Me.ddlaccounttype.Text = "Income Statement"
                ElseIf clsCommon.CompairString(obj.Group_Type, "Retained Earnings") = CompairStringResult.Equal Then
                    Me.ddlaccounttype.Text = "Retained Earnings"
                End If
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
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.AccountMainGroup, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim obj As New ClsAccountMainGroup()
                obj.Account_Main_Group_Code = clsCommon.myCstr(fndaccgp.Value)
                obj.Account_Main_Group_Desc = clsCommon.myCstr(txtdes.Text)
                obj.Group_Type = clsCommon.myCstr(ddlaccounttype.Text)
                If (ClsAccountMainGroup.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Account_Main_Group_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
        Dim inputs() As String = {}
        inputs = {"Account Main Group Code", "Description", "Type"}
        Dim Strs As List(Of String) = New List(Of String)(inputs)
        If transportSql.importExcel(gv, Strs.ToArray()) Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New ClsAccountMainGroup()
                    linno += 1

                    Dim strcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If strcode.Length <= 0 Or (String.IsNullOrEmpty(strcode)) Then
                        Throw New Exception("Account main group code can't be blank at line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf strcode.Length > 12 Then
                        Throw New Exception("Account main group length can not be more than 12 at line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf Not IsNumeric(strcode) Then
                        Throw New Exception("Please enter numeric value in account main group code at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Account_Main_Group_Code = strcode

                    Dim strdes As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If strdes.Length <= 0 Or (String.IsNullOrEmpty(strdes)) Then
                        Throw New Exception("Description can't be blank at line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf strdes.Length > 50 Then
                        Throw New Exception("Description length can not be more than 50 at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Account_Main_Group_Desc = strdes

                    Dim Type As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If clsCommon.myLen(Type) > 0 Then
                        If clsCommon.CompairString(Type, "Balance Sheet") = CompairStringResult.Equal Or clsCommon.CompairString(Type, "Income Statement") = CompairStringResult.Equal Or clsCommon.CompairString(Type, "Retained Earnings") = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("Type should be amoung 'Balance Sheet','Income Statement','Retained Earnings' at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    Else
                        Throw New Exception("Type can not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Group_Type = Type

                    ClsAccountMainGroup.SaveData(obj, ClsAccountMainGroup.CheckNewEntry(obj.Account_Main_Group_Code, trans), trans)
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
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
        str = "select Account_Main_Group_Code As [Account Main Group Code],Account_Main_Group_Desc as [Description],GROUP_TYPE AS 'Type' from TSPL_ACCOUNT_MAIN_GROUPS"
        ListImpExpColumnsMandatory = New List(Of String)({"Account Main Group Code", "Description", "Type"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Account Main Group Code"})
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
End Class
