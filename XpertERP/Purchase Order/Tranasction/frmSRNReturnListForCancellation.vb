Imports common
Public Class frmSRNReturnListForCancellation
    Inherits FrmMainTranScreen

#Region "Variables"

#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPurchaseOrderAmd)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub frmSRNReturnListForCancellation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Sub LoadData()
        Dim qry As String = Nothing
        Dim dt As DataTable = New DataTable()

        qry = "select TSPL_SRN_RETURN.Document_No from TSPL_SRN_RETURN where convert(date,TSPL_SRN_RETURN.Document_Date,103)<='" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "' AND convert(date,TSPL_SRN_RETURN.Document_Date,103)>='" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "' "
        dt = clsDBFuncationality.GetDataTable(qry)
        gv.DataSource = Nothing
        gv.Columns.Clear()
        gv.Rows.Clear()
        gv.GroupDescriptors.Clear()
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv.DataSource = dt
            gv.BestFitColumns()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
