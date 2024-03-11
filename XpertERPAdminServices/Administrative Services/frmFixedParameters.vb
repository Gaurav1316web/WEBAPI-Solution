'--Created By---[Pankaj Kumar Chauselect * from TSPL_FIXED_PARAMETERdhary]---Against Ticket No--[BM00000000908]
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
            Dim whr As String = " type not in ('" + clsFixedParameterType.IsConsiderOutTypeDocForBalance + "')"
            Dim dt As DataTable = clsFixedParameter.GetFixedParameter(Nothing, whr)
            If dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No data found.", Me.Text)
            Else
                gvInvoice.DataSource = dt
                FormatGrid()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

                Dim CheckObjectValue As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where code='" + clsCommon.myCstr(grow.Cells("Code").Value) + "' and Type='" + clsCommon.myCstr(grow.Cells("Type").Value) + "'", trans))

                If clsCommon.CompairString(obj.Description, CheckObjectValue) = CompairStringResult.Equal Then
                Else
                    clsFixedParameter.UpdateFixedParameter(obj, trans, False)
                End If

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

            ''richa TEC/15/05/19-000481
            trans.Commit()
            clsCommon.MyMessageBoxShow(Me, "Data updated successfully.", Me.Text)
            objCommonVar.RefreshCommonVar()
            LoadData()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            trans.Rollback()
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub gvInvoice_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvInvoice.CellDoubleClick
        clsERPFuncationalityOLD.ShowHistoryData(gvInvoice.CurrentRow.Cells("Type").Value, "Type", "TSPL_FIXED_PARAMETER", "", Nothing, "Code", gvInvoice.CurrentRow.Cells("Code").Value)
    End Sub

    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        Dim gvTemp As New common.UserControls.MyRadGridView
        Me.Controls.Add(gvTemp)
        Try
            If gvInvoice.Rows.Count > 0 Then
                'Prevent to export company code related setting
                'transportSql.QuickExportToExcel(gvInvoice, "", Me.Text)
                Dim dtData As DataTable = gvInvoice.DataSource
                Dim StrFilter As String = " Type <> '" & clsFixedParameterCode.BigValidity & "' and Type <> '" & clsFixedParameterCode.LicenceExpiryDate & "' and Type <> '" & clsFixedParameterCode.LicenceNoOfExeConnection & "' and Type <> '" & clsFixedParameterCode.LicenceNoOfJournalEntry & "' and Type <> '" & clsFixedParameterCode.LicenceNoOfUser & "'"
                Dim dr As DataRow() = dtData.Select(StrFilter)
                Dim dt As DataTable = dr.CopyToDataTable()
                gvTemp.DataSource = dt
                transportSql.QuickExportToExcel(gvTemp, "", Me.Text)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gvTemp)
        End Try
    End Sub
    'Ticket No-TEC/16/12/19-001049
    Private Sub btn_import_Click(sender As Object, e As EventArgs) Handles btn_import.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)

        Dim inputs() As String = {}
        inputs = {"Type", "Code", "Description", "Specification"}
    
        Dim Strs As List(Of String) = New List(Of String)(inputs)
        If transportSql.importExcel(gv, Strs.ToArray()) Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As clsFixedParameter = New clsFixedParameter()
                    obj.Code = clsCommon.myCstr(grow.Cells("Code").Value)
                    obj.Type = clsCommon.myCstr(grow.Cells("Type").Value)
                    obj.Description = clsCommon.myCstr(grow.Cells("Description").Value)
                    obj.Specification = clsCommon.myCstr(grow.Cells("Specification").Value)

                    Dim CheckObjectValue As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where code='" + clsCommon.myCstr(grow.Cells("Code").Value) + "' and Type='" + clsCommon.myCstr(grow.Cells("Type").Value) + "'", trans))

                    If clsCommon.CompairString(obj.Description, CheckObjectValue) = CompairStringResult.Equal Then
                    Else
                        clsFixedParameter.UpdateFixedParameter(obj, trans, False)
                    End If

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


                trans.Commit()
                clsCommon.MyMessageBoxShow(Me, "Data updated successfully.", Me.Text)
                objCommonVar.RefreshCommonVar()
                LoadData()
            Catch ex As Exception
                common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                trans.Rollback()
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
End Class

