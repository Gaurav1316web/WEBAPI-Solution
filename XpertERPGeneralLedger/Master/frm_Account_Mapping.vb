
Imports System
Imports System.Data.SqlClient
Imports common

'' Created By Abhishek as on 19/12/2012
Public Class Frm_Account_Mapping
    Inherits FrmMainTranScreen



    Private Sub Frm_Account_Mapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dgvAccountMap.AllowAddNewRow = False
        LoadAcctWithSegment()

    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frm_Account_Mapping)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If

    End Sub
    Public Sub LoadAcctWithSegment()
        Dim qrylngth As String
        qrylngth = "select seg_length from tspl_gl_segment where seg_no = 1"
        Dim lenth As Integer = connectSql.RunScalar(qrylngth)
        Dim Qry As String = "select cast( 0 as Bit) as [Select], Account_Code as AccountCode ,Description  from TSPL_GL_ACCOUNTS where len(Account_Code) <>" & lenth & ""
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(Qry)
        dgvAccountMap.DataSource = Nothing
        If dt.Rows.Count > 0 Then
            dgvAccountMap.DataSource = dt
            GridFormat()
        End If


    End Sub
    Public Sub GridFormat()
        dgvAccountMap.Columns("AccountCode").ReadOnly = True
        dgvAccountMap.Columns("AccountCode").Width = 150
        dgvAccountMap.Columns("AccountCode").HeaderText = "Account Code"

        dgvAccountMap.Columns("Description").ReadOnly = True
        dgvAccountMap.Columns("Description").Width = 320

    End Sub
    Private Sub txtfndAccountCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtfndAccountCode._MYValidating

        Dim qrylngth As String
        qrylngth = "select seg_length from tspl_gl_segment where seg_no = 1"
        Dim lenth As Integer = connectSql.RunScalar(qrylngth)
        'Dim Qry As String = "select Account_Code as Code ,Description  from TSPL_GL_ACCOUNTS  "
        'txtfndAccountCode.Value = clsCommon.ShowSelectForm("Account Code", Qry, "Code", "len(Account_Code) = " & lenth & "", txtfndAccountCode.Value, "Code", isButtonClicked)
        txtfndAccountCode.Value = clsGLAccount.getFinder("len(Account_Code) = " & lenth & "", txtfndAccountCode.Value, isButtonClicked)
        Dim acctname As String = "select description from TSPL_GL_ACCOUNTS where account_Code='" & txtfndAccountCode.Value & "' "
        lblAcctNme.Text = clsDBFuncationality.getSingleValue(acctname)
        LoadAcct(txtfndAccountCode.Value)


    End Sub
    Public Sub LoadAcct(ByVal acct As String)
        Dim i As Integer
        Dim Count As Integer = 0
        If clsCommon.myLen(txtfndAccountCode.Value) > 0 Then
            For i = 0 To dgvAccountMap.Rows.Count - 1
                Dim qrymap As String = "select Count(*)  from TSPL_GL_ACCOUNT_mapping where Mapped_account_Code ='" & dgvAccountMap.Rows(i).Cells("AccountCode").Value & "'  and Account_Code='" & acct & "' "
                Count = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qrymap))
                If Count > 0 Then
                    dgvAccountMap.Rows(i).Cells("Select").Value = True
                Else
                    dgvAccountMap.Rows(i).Cells("Select").Value = False
                End If
            Next
        End If
    End Sub
    Public Sub SelectAllAcct()
        If clsCommon.myLen(txtfndAccountCode.Value) > 0 Then
            Dim I As Integer
            For I = 0 To dgvAccountMap.ChildRows.Count - 1
                dgvAccountMap.ChildRows(I).Cells(0).Value = True

            Next
        Else
            common.clsCommon.MyMessageBoxShow("Please Select account")
            txtfndAccountCode.Focus()
            Exit Sub
        End If
    End Sub
    Public Sub UnselectAllAcct()
        If clsCommon.myLen(txtfndAccountCode.Value) > 0 Then
            Dim I As Integer
            For I = 0 To dgvAccountMap.Rows.Count - 1
                dgvAccountMap.Rows(I).Cells(0).Value = False

            Next
        Else
            common.clsCommon.MyMessageBoxShow("Please Select account")
            txtfndAccountCode.Focus()
            Exit Sub
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        SelectAllAcct()
    End Sub

    Private Sub btnUnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnSelectAll.Click
        UnselectAllAcct()
    End Sub
    Public Function AllowToSave() As Boolean
        If clsCommon.myLen(txtfndAccountCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Account Code firstly ")
            Return False
        End If
        Dim i As Integer
        'Dim CheckAcct As Integer
        Dim Arr As ArrayList = New ArrayList()

        For i = 0 To dgvAccountMap.Rows.Count - 1
            If clsCommon.myCBool(dgvAccountMap.Rows(i).Cells(0).Value) Then
                Arr.Add(clsCommon.myCstr(dgvAccountMap.Rows(i).Cells("AccountCode").Value))
            End If

        Next
        Dim qry As String = "select Account_Code,MApped_Account_Code From tspl_Gl_account_Mapping where Mapped_Account_Code in (" + clsCommon.GetMulcallString(Arr) + ") and Account_Code Not In ('" + txtfndAccountCode.Value + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim strmess As String = ""
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows
                If clsCommon.myLen(strmess) > 0 Then
                    strmess += "," + Environment.NewLine
                End If
                strmess += "Account " + clsCommon.myCstr(dr("Mapped_Account_Code")) + " already mapped with " + clsCommon.myCstr(dr("Account_Code")) + ""

            Next
            common.clsCommon.MyMessageBoxShow(strmess)

            Return False

        End If

        Return True
    End Function



    Private Sub Btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnsave.Click
        If AllowToSave() Then

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frm_Account_Mapping, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            Dim arr As List(Of ClsAccount_Mapping) = New List(Of ClsAccount_Mapping)

            For ii As Integer = 0 To dgvAccountMap.Rows.Count - 1
                Dim Obj As ClsAccount_Mapping = New ClsAccount_Mapping()
                If dgvAccountMap.Rows(ii).Cells(0).Value = True Then
                    Obj.Account_Code = txtfndAccountCode.Value
                    Obj.Mapped_account_Code = clsCommon.myCstr(dgvAccountMap.Rows(ii).Cells("AccountCode").Value)
                    arr.Add(Obj)
                End If
            Next

            If arr.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atlest Single account")
                Exit Sub

            End If
            If ClsAccount_Mapping.SaveData(txtfndAccountCode.Value, arr) Then
                common.clsCommon.MyMessageBoxShow("Data saved SucessFully")
            End If

        End If
    End Sub

    Private Sub dgvAccountMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvAccountMap.Click

    End Sub

    Private Sub dgvAccountMap_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles dgvAccountMap.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Do you want to delete current row?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
End Class
