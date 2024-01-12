
Imports System
Imports System.Data.SqlClient
Imports common

'' Created By Pradeep Sharma as on 24/10/2013
Public Class frmMapLedgerAccToTally
    Inherits FrmMainTranScreen

    Private Sub frmMapLedgerAccToTally_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadAcc()
        SetUserMgmtNew()
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmMapLedgerAccToTally)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        Btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If Btnsave.Visible = True Then
            RadMenuImport.Enabled = True
            RadMenuExport.Enabled = True
        Else
            RadMenuImport.Enabled = False
            RadMenuExport.Enabled = False
        End If
        '--------------------------------------------------

    End Sub
    Public Sub LoadAcc()
        Dim Qry As String = "select Account_Code as [Account Code], Description, tallyaccname as [Name In Tally]  from TSPL_GL_ACCOUNTS"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(Qry)
        dgvAccountMap.DataSource = Nothing
        If dt.Rows.Count > 0 Then
            dgvAccountMap.DataSource = dt
            GridFormat()
        End If
    End Sub
    Public Sub GridFormat()
        dgvAccountMap.Columns("Account Code").ReadOnly = True
        dgvAccountMap.Columns("Account Code").Width = 150
        dgvAccountMap.Columns("Description").ReadOnly = True
        dgvAccountMap.Columns("Description").Width = 300
        dgvAccountMap.Columns("Name In Tally").ReadOnly = False
        dgvAccountMap.Columns("Name In Tally").Width = 300
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub Btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnsave.Click
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            If dgvAccountMap.Rows.Count > 0 Then
                Dim strqry As String = ""
                For Each dr As GridViewRowInfo In dgvAccountMap.Rows
                    If clsCommon.myLen(dr.Cells("Name In Tally").Value) > 0 Then
                        strqry = ""
                        strqry = " UPDATE TSPL_GL_ACCOUNTS SET tallyaccname = '" + clsCommon.myCstr(dr.Cells("Name In Tally").Value) + "' WHERE Account_Code = '" + clsCommon.myCstr(dr.Cells("Account Code").Value) + "' "
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(strqry, trans)
                    End If
                Next
                If isSaved Then
                    trans.Commit()
                End If
            End If
        Catch err As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
        common.clsCommon.MyMessageBoxShow(Me, "Account Maped SucessFully.", Me.Text)
        LoadAcc()
    End Sub

    Private Sub dgvAccountMap_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs)
        If common.clsCommon.MyMessageBoxShow(Me, "Do you want to delete current row?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub MenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuImport.Click

        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim strqry As String = ""

        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Account Code", "Description", "Name In Tally") Then
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    If clsCommon.myLen(grow.Cells(0).Value) > 0 Then

                        Dim strCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                        If strCode.Length > 50 Or (String.IsNullOrEmpty(strCode)) Then
                            Throw New Exception("Code can not be blank or incorrect.")
                        End If

                        'Dim strDes As String = clsCommon.myCstr(grow.Cells(1).Value)
                        'If strDes.Length > 100 Or (String.IsNullOrEmpty(strDes)) Then
                        '    Throw New Exception("Description can not be blank or incorrect.")
                        'End If

                        Dim strName As String = clsCommon.myCstr(grow.Cells(2).Value)
                        If strName.Length > 50 Then
                            Throw New Exception(" Length of Name In Tally can not be Grater then 50 for Account : " + strCode + ".")
                        End If

                        strqry = ""
                        strqry = " UPDATE TSPL_GL_ACCOUNTS SET tallyaccname = '" + strName + "' WHERE Account_Code = '" + strCode + "' "
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                    End If
                Next
                If isSaved Then
                    trans.Commit()
                End If

                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                LoadAcc()
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub MenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuExport.Click
        Dim str As String
        str = "select Account_Code as [Account Code], Description, tallyaccname as [Name In Tally]  from TSPL_GL_ACCOUNTS"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuClose.Click
        Me.Close()
    End Sub

    Private Sub dgvAccountMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvAccountMap.Click

    End Sub
End Class
