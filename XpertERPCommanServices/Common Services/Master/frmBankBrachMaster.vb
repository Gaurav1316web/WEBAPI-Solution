Imports common
Imports System.Data.SqlClient
Public Class FrmBankBrachMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ReasonCode, Description, CompGroupCode, CompDesc As String
    Dim obj As New clsBankBranchMaster
    Private Sub frmPrimaryReasonMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rbtnSave.Enabled Then
                If AllowToSave() Then saveData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rbtnDelete.Enabled Then
                deleteData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                Close()
            ElseIf e.Alt And e.KeyCode = Keys.N Then
                Reset()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub frmBankBrachMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Try
        ButtonToolTip.SetToolTip(rbtnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(rbtnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(rbtnClose, "Press Alt+C Close the Window")
        Reset()
        SetUserMgmtNew()
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub
    Private Sub Reset()
        Try
            fndBranchCode.Value = ""
            txtBranchName.Text = ""
            txtIFSCCode.Text = ""
            fndBankCode.Value = ""
            lblBankName.Text = ""
            rbtnDelete.Enabled = False
            fndBranchCode.Focus()
            rbtnSave.Text = "Save"
            fndBranchCode.MyReadOnly = False
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.bankBranchMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Sub
        End If
        rbtnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If rbtnSave.Visible = True Then
            rdmenuimport.Enabled = True
            rdmenuexport.Enabled = True
        Else
            rdmenuimport.Enabled = False
            rdmenuexport.Enabled = False
        End If
        '--------------------------------------------------
        rbtnDelete.Visible = MyBase.isDeleteFlag
    End Sub


    Private Sub rbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnClose.Click
        Me.Close()
    End Sub

    Private Sub rbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSave.Click

        If AllowToSave() Then saveData()
    End Sub
    Public Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(fndBranchCode.Value) = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Enter Branch Code", Me.Text)
                Return False
            End If
            If clsCommon.myLen(txtBranchName.Text) = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Enter Branch Name", Me.Text)
                Return False
            End If
            If clsCommon.myLen(txtIFSCCode.Text) = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Enter IFSC Code", Me.Text)
                Return False
            End If
            If clsDBFuncationality.getSingleValue("select count(*) from tspl_bank_Branch_master where IFSC_code='" & txtIFSCCode.Text.ToString.Trim & "'") > 0 AndAlso clsCommon.CompairString(rbtnSave.Text, "Save") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("Duplicate IFSC Code. This Code is Already Assigned to Other Branch  ")
                Return False
            End If
            If clsDBFuncationality.getSingleValue("select count(*) from tspl_bank_Branch_master where IFSC_code='" & txtIFSCCode.Text.ToString.Trim & "' and branch_code<>'" & fndBranchCode.Value & "'") > 0 AndAlso clsCommon.CompairString(rbtnSave.Text, "Update") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("Duplicate IFSC Code. This Code is Already Assigned to Other Branch  ")
                Return False
            End If
            If clsDBFuncationality.getSingleValue("select count(*) from tspl_bank_Branch_master where branch_name='" & txtBranchName.Text.ToString.Trim & "' and bank_code='" & fndBankCode.Value & "'") > 0 AndAlso clsCommon.CompairString(rbtnSave.Text, "Save") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("Duplicate Branch Name  Against Specified Bank Code   ")
                Return False
            End If
            If clsDBFuncationality.getSingleValue("select count(*) from tspl_bank_Branch_master where branch_name='" & txtBranchName.Text.ToString.Trim & "' and bank_code='" & fndBankCode.Value & "' and branch_code<>'" & fndBranchCode.Value & "'") > 0 AndAlso clsCommon.CompairString(rbtnSave.Text, "Update") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("Duplicate Branch Name  Against Specified Bank Code   ")
                Return False
            End If
            If clsCommon.myLen(fndBankCode.Value) = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select Bank Code", Me.Text)
                Return False
            End If
            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Sub saveData()
        Try
            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.bankBranchMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            obj = New clsBankBranchMaster()
            obj.Branch_Code = fndBranchCode.Value
            obj.Branch_Name = txtBranchName.Text.Trim
            obj.IFSCCode = txtIFSCCode.Text
            obj.BankCode = fndBankCode.Value
            Dim isSaved As Boolean = obj.SaveData(obj, Nothing)
            If isSaved Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                rbtnSave.Text = "Update"
                rbtnDelete.Enabled = True
            Else
                rbtnSave.Text = "Save"
                rbtnDelete.Enabled = False
                common.clsCommon.MyMessageBoxShow(Me, "Data Could Not Saved", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub deleteData()
        Try
            If clsCommon.myLen(fndBranchCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Branch Code found to Delete", Me.Name)
                'Return False
                Exit Sub
            ElseIf Not (common.clsCommon.MyMessageBoxShow("Delete the Branch Code " + fndBranchCode.Value + Environment.NewLine + " Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                'Return False
                Exit Sub
            End If
            If (clsBankBranchMaster.DeleteData(fndBranchCode.Value)) Then
                common.clsCommon.MyMessageBoxShow("Data Deleted Sucessfully", Me.Name)
                Reset()
                'Return True
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub loadData()
        Try
            obj = New clsBankBranchMaster()
            obj = clsBankBranchMaster.GetData(fndBranchCode.Value, NavigatorType.Current)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Branch_Code) > 0 Then
                fndBranchCode.Value = obj.Branch_Code
                txtBranchName.Text = obj.Branch_Name
                fndBankCode.Value = obj.BankCode
                txtIFSCCode.Text = obj.IFSCCode
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    

    Private Sub rbtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnDelete.Click
        deleteData()
    End Sub

    Private Sub rbtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnReset.Click
        Reset()
    End Sub



    Private Sub rdmenuexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuexit.Click
        Me.Close()
    End Sub

    Private Sub rdmenuexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuexport.Click
        Try
            Dim str As String
            str = "select Branch_Code as Code,Branch_Name as [Branch Name], IFSC_Code as [IFSC Code], tspl_bank_branch_Master.Bank_Code as [Bank Code],tspl_bank_master.description as [Bank Name] from tspl_bank_branch_Master left outer join tspl_bank_master on tspl_bank_master.bank_code=tspl_bank_branch_master.bank_code "
            transportSql.ExporttoExcel(str, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rdmenuimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim i As Integer = 0
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Branch Name", "IFSC Code", "Bank Code", "Bank Name") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsBankBranchMaster()
                    i = i + 1
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    If clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception(" Branch Code can not be blank ")
                    End If
                    obj.Branch_Code = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Branch Name").Value)
                    If clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception(" Branch Name can not be blank ")
                    End If
                    obj.Branch_Name = strCode
                    strCode = clsCommon.myCstr(grow.Cells("IFSC Code").Value)
                    If clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception(" IFSC Code can not be left blank  ")
                    End If
                    If clsDBFuncationality.getSingleValue("select count(*) from tspl_bank_Branch_master where IFSC_code='" & strCode & "' and branch_code<>'" & clsCommon.myCstr(obj.Branch_Code) & "'", trans) > 0 Then
                        Throw New Exception("Duplicate IFSC Code. This Code is Already Assigned to Other Branch  ")
                    End If
                    obj.IFSCCode = strCode
                    strCode = clsCommon.myCstr(grow.Cells("Bank Code").Value)
                    If clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception(" Bank Code can not be left blank  ")
                    End If
                    If clsDBFuncationality.getSingleValue("select count(*) from tspl_bank_master where bank_code='" & strCode & "'", trans) = 0 Then
                        Throw New Exception("Invalid  Bank Code . It Could Not Found in Master  ")
                    End If
                    obj.BankCode = strCode
                    obj.SaveData(obj, trans)
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                trans.Commit()
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                trans.Rollback()
                clsCommon.MyMessageBoxShow(ex.Message & " At Line  no : " & i)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub fndBranchCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndBranchCode._MYNavigator
        Try
            obj = New clsBankBranchMaster()
            obj = clsBankBranchMaster.GetData(fndBranchCode.Value, NavType)
            If obj IsNot Nothing Then
                fndBranchCode.Value = obj.Branch_Code
                txtBranchName.Text = obj.Branch_Name
                txtIFSCCode.Text = obj.IFSCCode
                fndBankCode.Value = obj.BankCode
                lblBankName.Text = clsDBFuncationality.getSingleValue("Select description from TSPL_Bank_MASTER where Bank_Code='" + obj.BankCode + "'")
                rbtnSave.Text = "Update"
                rbtnDelete.Enabled = True
                fndBranchCode.MyReadOnly = True

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub fndBranchCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndBranchCode._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_Bank_Branch_MASTER where Branch_code ='" + fndBranchCode.Value + "' "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 Then
                fndBranchCode.MyReadOnly = False
            Else
                fndBranchCode.MyReadOnly = True
            End If
            If fndBranchCode.MyReadOnly OrElse isButtonClicked Then
                fndBranchCode.Value = clsBankBranchMaster.getFinder("", fndBranchCode.Value, isButtonClicked)
                If clsCommon.myLen(fndBranchCode.Value) > 0 Then
                    Dim obj As clsBankBranchMaster = clsBankBranchMaster.GetData(clsCommon.myCstr(fndBranchCode.Value), NavigatorType.Current)
                    txtBranchName.Text = obj.Branch_Name
                    fndBankCode.Value = obj.BankCode
                    lblBankName.Text = clsDBFuncationality.getSingleValue("Select description from TSPL_Bank_MASTER where Bank_Code='" + fndBankCode.Value + "'")
                    txtIFSCCode.Text = obj.IFSCCode
                End If
                If clsCommon.myLen(fndBranchCode.Value) > 0 Then
                    rbtnDelete.Enabled = True
                    rbtnSave.Text = "Update"
                    fndBranchCode.MyReadOnly = True
                Else
                    rbtnSave.Text = "Save"
                    rbtnDelete.Enabled = False
                    fndBranchCode.MyReadOnly = False
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndBankCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndBankCode._MYValidating
        fndBankCode.Value = clsCommon.myCstr(clsBankMaster.getFinder("", fndBankCode.Value, isButtonClicked))
        If clsCommon.myLen(fndBankCode.Value) > 0 Then
            lblBankName.Text = clsDBFuncationality.getSingleValue("Select description from TSPL_Bank_MASTER where Bank_Code='" + fndBankCode.Value + "'")
        End If

    End Sub
End Class
