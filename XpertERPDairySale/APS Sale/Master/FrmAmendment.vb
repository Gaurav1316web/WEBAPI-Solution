Imports common

Public Class FrmAmendment
    Inherits FrmMainTranScreen
#Region "Variables"
    Public strDocCode As String = ""
    Public strToDate As DateTime? = Nothing
    Public strtotalQty As Double = 0
#End Region

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Try
            Dim qry As String = "update TSPL_CUSTOMER_TENDER set Total_Qty='" & clsCommon.myCstr(txtTotalQty.Text) & "',To_Date='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' where Document_Code='" & lblDocuemntCode.Text & "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, lblDocuemntCode.Text, "TSPL_CUSTOMER_TENDER", "Document_Code", "TSPL_CUSTOMER_TENDER_DETAIL", "Document_Code", Nothing)
            clsCommon.MyMessageBoxShow(Me, "Successfully Amended", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmAmendment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub
    Private Sub LoadData()
        lblDocuemntCode.Text = strDocCode
        txtTotalQty.Text = strtotalQty
        txtToDate.Value = strToDate
    End Sub
End Class