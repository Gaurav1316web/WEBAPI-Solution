'--Created By---[Pankaj Kumar Chaudhary]---Against Ticket No--[BM00000000908]
Imports common
Imports System.Data.SqlClient

Public Class FrmFixedParameters
    Inherits FrmMainTranScreen

    Private Sub FrmFixedParameters_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        gvInvoice.AllowAddNewRow = False
        LoadData()
    End Sub

    Public Sub LoadData()
        Try
            gvInvoice.DataSource = Nothing
            Dim dt As DataTable = clsFixedParameter.GetFixedParameter(Nothing)
            If dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No data found.")
            Else
                gvInvoice.DataSource = dt
                FormatGrid()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FormatGrid()

        gvInvoice.Columns("Type").ReadOnly = True
        gvInvoice.Columns("Type").Width = 200

        gvInvoice.Columns("Code").ReadOnly = True
        gvInvoice.Columns("Code").Width = 200

        gvInvoice.Columns("Description").ReadOnly = False
        gvInvoice.Columns("Description").Width = 300

        gvInvoice.Columns("Specification").ReadOnly = True
        gvInvoice.Columns("Specification").Width = 400

    End Sub

    Private Sub btnChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            For Each grow As GridViewRowInfo In gvInvoice.Rows
                Dim obj As clsFixedParameter = New clsFixedParameter()
                obj.Code = clsCommon.myCstr(grow.Cells("Code").Value)
                obj.Type = clsCommon.myCstr(grow.Cells("Type").Value)
                obj.Description = clsCommon.myCstr(grow.Cells("Description").Value)
                obj.Specification = clsCommon.myCstr(grow.Cells("Specification").Value)
                clsFixedParameter.UpdateFixedParameter(obj, trans, False)
            Next
            '' check inv summary update setting
            Dim InvSummary As String = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.UpdateInventorySummaryTable, clsFixedParameterCode.UpdateInventorySummaryTable, trans))
            If clsCommon.CompairString(InvSummary, "0") = CompairStringResult.Equal Then
                Dim obj As clsFixedParameter = New clsFixedParameter()
                obj.Code = clsFixedParameterCode.FatSNFStockControl
                obj.Type = clsFixedParameterCode.FatSNFStockControl
                obj.Description = "0"
                obj.Specification = "0:Off,1:On"
                clsFixedParameter.UpdateFixedParameter(obj, trans, False)

                obj = New clsFixedParameter
                obj.Code = clsFixedParameterCode.CheckBalanceFromInvMoveSummry
                obj.Type = clsFixedParameterCode.CheckBalanceFromInvMoveSummry
                obj.Description = "0"
                obj.Specification = "0:Off,1:On"
                clsFixedParameter.UpdateFixedParameter(obj, trans, False)
            End If

            clsCommon.MyMessageBoxShow("Data updated successfully.")
            trans.Commit()
            objCommonVar.RefreshCommonVar()
            LoadData()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            trans.Rollback()
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class

