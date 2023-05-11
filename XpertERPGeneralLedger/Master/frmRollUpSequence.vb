''--22/06/2012---Created By --[Panakj Kumar]---This Screen sets the Rollup Sequence on Slection of Account Group From Print Order Screen------Req By=Rakesh Sir
Imports common
Imports System.Data.SqlClient
Public Class FrmRollUpSequence
    Public AccGroupCode As String

    Private Sub FrmRollUpSequence_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillAccount()
    End Sub
    Sub FillAccount()
        Dim Qry As String = "Select Account_Code as [Account Code], Description, Rollup_Seq as [Original Rollup Seq], Rollup_Seq as [Duplicate Rollup Seq] from TSPL_GL_ACCOUNTS Where Rollup='Y' AND Account_Group_Code='" + AccGroupCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        dgvAccountCode.DataSource = dt
        txtAccGroupCode.Text = AccGroupCode
        txtAccGroupDesc.Text = clsDBFuncationality.getSingleValue("Select Account_Group_Desc from TSPL_GL_ACCOUNTS Where Rollup='Y' AND Account_Group_Code='" + AccGroupCode + "'")
        formatGrid()
    End Sub

    Sub formatGrid()
        dgvAccountCode.Columns("Account Code").Width = 100
        dgvAccountCode.Columns("Account Code").ReadOnly = True
        dgvAccountCode.Columns("Description").Width = 300
        dgvAccountCode.Columns("Description").ReadOnly = True
        dgvAccountCode.Columns("Original Rollup Seq").Width = 121
        dgvAccountCode.Columns("Original Rollup Seq").ReadOnly = True
        dgvAccountCode.Columns("Duplicate Rollup Seq").Width = 121
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        UpdateRollupSeq()
    End Sub

    Private Function AllowToUpdate() As Boolean
        For ii As Integer = 0 To dgvAccountCode.Rows.Count - 1
            For jj As Integer = ii + 1 To dgvAccountCode.Rows.Count - 1
                If clsCommon.myCdbl(dgvAccountCode.Rows(ii).Cells("Duplicate Rollup Seq").Value) <> 0 Then
                    If clsCommon.myCdbl(dgvAccountCode.Rows(ii).Cells("Duplicate Rollup Seq").Value) = clsCommon.myCdbl(dgvAccountCode.Rows(jj).Cells("Duplicate Rollup Seq").Value) Then
                        Throw New Exception("Same Sequence No Exist at Row No " + clsCommon.myCstr(ii + 1) + " && " + clsCommon.myCstr(jj + 1) + "")
                    End If
                End If
            Next
        Next
        Return True
    End Function

    Sub UpdateRollupSeq()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowToUpdate() Then
                Dim ii As Integer
                Dim currentdate As Date = Date.Today
                For ii = 0 To dgvAccountCode.Rows.Count - 1
                    Dim AccCode As String = clsCommon.myCstr(dgvAccountCode.Rows(ii).Cells("Account Code").Value)
                    Dim RollupSeqNo As Integer = clsCommon.myCdbl(dgvAccountCode.Rows(ii).Cells("Duplicate Rollup Seq").Value)
                    If RollupSeqNo > 0 Then
                        Dim Qry As String = "Update TSPL_GL_ACCOUNTS set Rollup_Seq=" + clsCommon.myCstr(RollupSeqNo) + " Where Account_Code='" + AccCode + "' AND Account_Group_Code='" + AccGroupCode + "'"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    End If
                Next
                trans.Commit()
                myMessages.update()
                FillAccount()
            End If
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
