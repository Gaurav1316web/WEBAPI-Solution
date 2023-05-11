Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Telerik.Collections.Generic
Imports Excel = Microsoft.Office.Interop.Excel
Imports common
Public Class frmFreshCreditLimit
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public dblCreditLimit As Double = 0
    Public dblSecurityAmount As Double = 0
    Public dblReverseSecurityAmount As Double = 0
    Public dblPendingDeliveryAmt As Double = 0
    Public dblOutstandingAmt As Double = 0
    Public dblRefundAmount As Double = 0
    Public dblReverseRefundAmount As Double = 0
    Public dblAmt As Double = 0
    Public dblShortCloseDoDispatch As Double = 0

    Private Sub frmVendorType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblCreditLimit.Text = dblCreditLimit
        lblAdvanceSecurity.Text = dblSecurityAmount
        lblReverseAdvanceSec.Text = dblReverseSecurityAmount
        lblPendingDO.Text = dblPendingDeliveryAmt
        lblLedgerOutstanding.Text = dblOutstandingAmt
        lblRefund.Text = dblRefundAmount
        lblReverseRefund.Text = dblReverseRefundAmount
        lblTotalOutstansing.Text = dblAmt
        lblShortcloseDO.Text = dblShortCloseDoDispatch
    End Sub
    Private Sub KeyPress1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
 
    Private Sub rdbtnClose_Click(sender As Object, e As EventArgs) Handles rdbtnClose.Click
        Me.Close()
    End Sub
End Class



