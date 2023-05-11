Imports common
Imports System.Data.SqlClient

Public Class FrmChangePrntOrdr_ACGroup

    Private Sub FrmChangePrntOrdr_ACGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillAccountGroup()
    End Sub

    Sub FillAccountGroup()
        Dim Qry As String = "Select Account_Group_Code as [Group Code], Account_Group_Desc as [Description], Print_Order as [Original Order No], Print_Order as [Duplicate Order No] from TSPL_ACCOUNT_GROUPS"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        dgvAccountGroup.DataSource = dt
        formatGrid()
    End Sub

    Sub formatGrid()
        dgvAccountGroup.Columns("Group Code").Width = 100
        dgvAccountGroup.Columns("Group Code").ReadOnly = True
        dgvAccountGroup.Columns("Description").Width = 300
        dgvAccountGroup.Columns("Description").ReadOnly = True
        dgvAccountGroup.Columns("Original Order No").Width = 121
        dgvAccountGroup.Columns("Original Order No").ReadOnly = True
        dgvAccountGroup.Columns("Duplicate Order No").Width = 121
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        UpdatePrntOrderNo()
    End Sub

    Private Function AllowToUpdate() As Boolean
        For ii As Integer = 0 To dgvAccountGroup.Rows.Count - 1
            For jj As Integer = ii + 1 To dgvAccountGroup.Rows.Count - 1
                If clsCommon.myCdbl(dgvAccountGroup.Rows(ii).Cells("Duplicate Order No").Value) = clsCommon.myCdbl(dgvAccountGroup.Rows(jj).Cells("Duplicate Order No").Value) Then
                    Throw New Exception("Same Order No Exist at Row No " + clsCommon.myCstr(ii + 1) + " && " + clsCommon.myCstr(jj + 1) + "")
                End If
            Next
        Next
        Return True

    End Function

    Sub UpdatePrntOrderNo()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowToUpdate() Then


                Dim ii As Integer
                Dim currentdate As Date = Date.Today
                For ii = 0 To dgvAccountGroup.Rows.Count - 1
                    Dim AccGrpCode As String = clsCommon.myCstr(dgvAccountGroup.Rows(ii).Cells("Group Code").Value)
                    Dim PrntOrdrNo As Integer = CInt(dgvAccountGroup.Rows(ii).Cells("Duplicate Order No").Value)
                    If PrntOrdrNo > 0 Then
                        Dim Qry As String = "Update TSPL_ACCOUNT_GROUPS set Print_Order=" + clsCommon.myCstr(PrntOrdrNo) + " Where Account_Group_Code='" + AccGrpCode + "'"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    End If
                Next
                trans.Commit()
                myMessages.update()
            End If
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub

    ''--21/06/2012--Added Bby--[Panakj Kumar]-----This Code opens new form(Rollup Sequence)---That Handles Rollup Sequence-------Req By===Rakesh Sir---
    Private Sub dgvAccountGroup_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvAccountGroup.DoubleClick
        If dgvAccountGroup.CurrentRow IsNot Nothing Then
            Dim strACode As String = clsCommon.myCstr(dgvAccountGroup.CurrentRow.Cells("Group Code").Value)
            If clsCommon.myLen(strACode) > 0 Then
                Try
                    Dim frm As New FrmRollUpSequence
                    frm.AccGroupCode = strACode
                    frm.ShowDialog()
                Catch ex As Exception
                    common.clsCommon.MyMessageBoxShow(ex.Message)
                End Try
            End If
        End If
    End Sub
    ''----------------------------------------------------------Code Ends Here-------------------------------------------------------------------------
End Class
