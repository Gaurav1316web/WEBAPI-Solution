'-----------created by Monika
Imports common
Imports System.Data.SqlClient


Public Class FrmCSAAccountSet
    Inherits FrmMainTranScreen

#Region "Variable"
    Dim ButtonToolTip As New ToolTip()
    Dim isNewentry As Boolean = False
    Dim Errorcontrol As New clsErrorControl()
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCSAAccountSet)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        rdbtnsave.Visible = MyBase.isModifyFlag
        rdbtndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FunReset()
        fndaccountsetcode.Value = ""
        rdtxtdescription.Text = ""
        fndrecisvablecontrol.Value = ""
        rdtxtrecievablecontrol.Text = ""
        txtgsoc.Value = ""
        txtgsoc_name.Text = ""
        txtconsignmnt.Value = ""
        txtcongnmnt_name.Text = ""
        txtgain.Value = ""
        txtgian_name.Text = ""
        txtloss.Value = ""
        txtloss_name.Text = ""

        fndaccountsetcode.MyReadOnly = False

        rdbtnsave.Text = "Save"
        rdbtnsave.Enabled = True
        rdbtndelete.Enabled = False

        fndaccountsetcode.Focus()
        fndaccountsetcode.Select()
    End Sub

    Private Sub rdbtnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnnew.Click
        FunReset()
    End Sub

    Private Sub txtgsoc__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtgsoc._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        txtgsoc.Value = clsCommon.ShowSelectForm("GSOCfnd", qry, "AccountCode", "", txtgsoc.Value, "", isButtonClicked)
        txtgsoc_name.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + txtgsoc.Value + "' ")
    End Sub

    Private Sub txtconsignmnt__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtconsignmnt._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        txtconsignmnt.Value = clsCommon.ShowSelectForm("CNSfnd", qry, "AccountCode", "", txtconsignmnt.Value, "", isButtonClicked)
        txtcongnmnt_name.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + txtconsignmnt.Value + "' ")
    End Sub

    Private Sub txtgain__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtgain._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        txtgain.Value = clsCommon.ShowSelectForm("GAINfnd", qry, "AccountCode", "", txtgain.Value, "", isButtonClicked)
        txtgian_name.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + txtgain.Value + "' ")
    End Sub

    Private Sub txtloss__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtloss._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        txtloss.Value = clsCommon.ShowSelectForm("LOSSfnd", qry, "AccountCode", "", txtloss.Value, "", isButtonClicked)
        txtloss_name.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + txtloss.Value + "' ")
    End Sub

    Private Sub rdbtnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(fndaccountsetcode.Value) <= 0 Then
                fndaccountsetcode.Focus()
                fndaccountsetcode.Select()
                Errorcontrol.SetError(fndaccountsetcode, "Select account set for mapping CSA accounts.")
                Throw New Exception("Select account set for mapping CSA accounts.")
            Else
                Errorcontrol.ResetError(fndaccountsetcode)
            End If

            If clsCommon.myLen(txtgsoc.Value) <= 0 Then
                txtgsoc.Focus()
                txtgsoc.Select()
                Errorcontrol.SetError(txtgsoc_name, "Select GSOC A/c Detail.")
                Throw New Exception("Select GSOC A/c Detail.")
            Else
                Errorcontrol.ResetError(txtgsoc_name)
            End If

            If clsCommon.myLen(txtconsignmnt.Value) <= 0 Then
                txtconsignmnt.Focus()
                txtconsignmnt.Select()
                Errorcontrol.SetError(txtcongnmnt_name, "Select Consignment A/c Detail.")
                Throw New Exception("Select Consignment A/c Detail.")
            Else
                Errorcontrol.ResetError(txtcongnmnt_name)
            End If

            If clsCommon.myLen(txtgain.Value) <= 0 Then
                txtgain.Focus()
                txtgain.Select()
                Errorcontrol.SetError(txtgian_name, "Select Gain A/c Detail.")
                Throw New Exception("Select Gain A/c Detail.")
            Else
                Errorcontrol.ResetError(txtgian_name)
            End If

            If clsCommon.myLen(txtloss.Value) <= 0 Then
                txtloss.Focus()
                txtloss.Select()
                Errorcontrol.SetError(txtloss_name, "Select Loss A/c Detail.")
                Throw New Exception("Select Loss A/c Detail.")
            Else
                Errorcontrol.ResetError(txtloss_name)
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Private Sub SaveData()
        Try

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmCSAAccountSet, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            Dim obj As New clsCSAAccountSet()

            obj.acc_code = clsCommon.myCstr(fndaccountsetcode.Value)
            obj.desc = clsCommon.myCstr(rdtxtdescription.Text).Replace("'", "`")
            obj.gsoc_code = clsCommon.myCstr(txtgsoc.Value)
            obj.consignmnt = clsCommon.myCstr(txtconsignmnt.Value)
            obj.gain_code = clsCommon.myCstr(txtgain.Value)
            obj.loss_code = clsCommon.myCstr(txtloss.Value)

            If clsCommon.myLen(obj.acc_code) > 0 Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If clsCSAAccountSet.SaveData(obj, isNewentry, trans) Then
                    If clsCommon.CompairString(rdbtnsave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow("Data Updated Successfully", Me.Text)
                    End If

                    fndaccountsetcode.MyReadOnly = True

                    LoadData(fndaccountsetcode.Value, NavigatorType.Current)
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rdbtndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtndelete.Click
        Try
            If clsCommon.myLen(fndaccountsetcode.Value) <= 0 Then
                fndaccountsetcode.Focus()
                fndaccountsetcode.Select()
                Errorcontrol.SetError(fndaccountsetcode, "Select Account Set for deletion")
                Throw New Exception("Select Account Set for deletion")
            Else
                Errorcontrol.ResetError(fndaccountsetcode)
            End If

            
            If Not (myMessages.deleteConfirm()) Then
                Return
            End If

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsCSAAccountSet.DeleteData(fndaccountsetcode.Value, trans) Then
                clsCommon.MyMessageBoxShow("Data Deleted Successfully", Me.Text)
                FunReset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Private Sub fndaccountsetcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndaccountsetcode._MYNavigator
        LoadData(clsCommon.myCstr(fndaccountsetcode.Value), NavType)
    End Sub

    Private Sub fndaccountsetcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndaccountsetcode._MYValidating
        Try
            Dim qry As String = "select count(*) from Tspl_Customer_account_Set where cust_account='" + fndaccountsetcode.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                fndaccountsetcode.MyReadOnly = True
            Else
                fndaccountsetcode.MyReadOnly = False
            End If

            If fndaccountsetcode.MyReadOnly OrElse isButtonClicked Then
                fndaccountsetcode.Value = clsCSAAccountSet.GetFinder("", fndaccountsetcode.Value, isButtonClicked)
                If clsCommon.myLen(fndaccountsetcode.Value) > 0 Then
                    LoadData(fndaccountsetcode.Value, NavigatorType.Current)
                Else
                    FunReset()
                End If
            Else
                FunReset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FrmCSAAccountSet_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rdbtnsave.Enabled Then
            rdbtnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rdbtndelete.Enabled Then
            rdbtndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmCSAAccountSet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        FunReset()

        ButtonToolTip.SetToolTip(rdbtnnew, "Press Alt+N for refresh window.")
        ButtonToolTip.SetToolTip(rdbtnsave, "Press Alt+S for save/update record.")
        ButtonToolTip.SetToolTip(rdbtndelete, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C for close window.")

        RadMenu1.Visible = False
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As clsCSAAccountSet = clsCSAAccountSet.GetData(strCode, NavType)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.gsoc) > 0 Then
                fndaccountsetcode.Value = obj.acc_code
                rdtxtdescription.Text = obj.desc
                txtgsoc.Value = obj.gsoc_code
                txtgsoc_name.Text = obj.gsoc
                rdtxtrecievablecontrol.Text = obj.debtr_desc
                fndrecisvablecontrol.Value = obj.debtr_code
                txtconsignmnt.Value = obj.consignmnt
                txtcongnmnt_name.Text = obj.consignmnt_name
                txtgain.Value = obj.gain_code
                txtgian_name.Text = obj.gain
                txtloss.Value = obj.loss_code
                txtloss_name.Text = obj.loss

                fndaccountsetcode.MyReadOnly = True
                rdbtnsave.Text = "Update"
                rdbtndelete.Enabled = True
            Else
                'FunReset()
                fndaccountsetcode.Value = obj.acc_code
                rdtxtdescription.Text = obj.desc
                rdtxtrecievablecontrol.Text = obj.debtr_desc
                fndrecisvablecontrol.Value = obj.debtr_code
                txtgsoc.Value = ""
                txtgsoc_name.Text = ""
                txtconsignmnt.Value = ""
                txtcongnmnt_name.Text = ""
                txtgain.Value = ""
                txtgian_name.Text = ""
                txtloss.Value = ""
                txtloss_name.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click

    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click

    End Sub
End Class
