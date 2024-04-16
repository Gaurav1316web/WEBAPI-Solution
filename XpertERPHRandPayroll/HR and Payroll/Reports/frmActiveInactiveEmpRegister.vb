Imports common
Public Class frmActiveInactiveEmpRegister

    Private Sub frmActiveInactiveEmpRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.CenterToParent()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function LoadDetailData(ByVal strCode As String) As Boolean
        Dim check As Boolean = False
        Try
            Dim Qry As String = "Select Distinct EMP_CODE As [EMP Code],Emp_Name As [EMP Name],Designation,Convert(Varchar,Status_Active_Date,103) As [Active Date],Convert(varchar,Status_Inactive_Date,103) As [Inactive Date] from TSPL_EMPLOYEE_MASTER_Hist_Data Where EMP_CODE='" + strCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count > 0 Then
                LoadGrid()
                Gv1.DataSource = dt
                Gv1.BestFitColumns()
                check = True
            Else
                check = False
                clsCommon.MyMessageBoxShow(Me, "Data not found.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return check
    End Function

    Sub LoadGrid()
        Gv1.DataSource = Nothing
        Gv1.Columns.Clear()
        Gv1.Rows.Clear()
        Gv1.GroupDescriptors.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.EnableFiltering = True
        Gv1.AllowDragToGroup = False
        Gv1.ReadOnly = True
        Gv1.Refresh()
    End Sub



    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


End Class