' '' '' ''Created By Preeti Gupta for MPD [13/01/2017]
Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Public Class FrmMultCustBookingDispatch
#Region "Variables"
    Dim blnPageLoad As Boolean = False
    Dim intChangeColumn As Integer = 0
    Public StrDocNo As String = ""
    Public arrBookingItem As List(Of clsBookingTemp) = Nothing
    Dim blnSaveTotalQTy As Boolean = False
    Dim DOmsg As String = ""
    Private isNewEntry As Boolean = False
    Private DOCreated As Boolean = False
    Dim AllowWo_Outstanding As Boolean
    Dim CheckOutstandingOnbooking As Integer = 0
    Dim ShowItemLocationWiseonBooking As Integer = 0
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Dim isLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Const colLineNo As String = "COLLNO"
    Const colCustCode As String = "colCustCode"
    Const colCustName As String = "colCustName"
    Const colLocCode As String = "colLocCode"
    Const colLocName As String = "colLocName"
    Const ReportID As String = "DairyBookingGrid"
    Const colQty As String = "colQty"
    Const colICode As String = "colICode"
    Const colIDesc As String = "colIDesc"
    Const colUnit As String = "colUnit"
    Const colTotalQty As String = "colTotalQty"
    Const colISeqNo As String = "colISeqNo"
    Const colIGroup As String = "colIGroup"
    Dim strSql As String
    Dim dblCustOutstandingAmt As Double = 0

#End Region

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As common.NavigatorType)

    End Sub
    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)

    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)

    End Sub
    Private Sub txtCustGrp__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)

    End Sub
    Private Sub btnCopyOrder_Click(sender As Object, e As EventArgs)

    End Sub

   
    Private Sub SplitContainer1_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub
End Class
