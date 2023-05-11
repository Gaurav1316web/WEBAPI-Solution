'--01/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmPF_ESI_Reports
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region

    Private Sub frmPF_ESI_Reports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPF_ESI_Reports)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnMonthlyPF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMonthlyPF.Click
        Dim frm As New frmMonthlyPF_Report()
        frm.Show()
        frm.Focus()
    End Sub

    Private Sub btnMonthlyESI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMonthlyESI.Click
        Dim frm As New frmMonthlyESI_Report()
        frm.Show()
        frm.Focus()
    End Sub

    Private Sub btnForm5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForm5.Click
        Dim frm As New frmForm5PF
        frm.Show()
        frm.Focus()
    End Sub

    Private Sub btnForm10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForm10.Click
        Dim frm As New frmForm10PF
        frm.Show()
        frm.Focus()
    End Sub

    Private Sub btnForm12A_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForm12A.Click
        Dim frm As New frmForm12APF
        frm.Show()
        frm.Focus()
    End Sub

    Private Sub btnForm3A_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForm3A.Click
        Dim frm As New frmFormA3_Report
        frm.Show()
        frm.Focus()
    End Sub

    Private Sub btnForm6A_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForm6A.Click
        Dim frm As New frmFormA6_Report
        frm.Show()
        frm.Focus()
    End Sub

    Private Sub BtnForm9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnForm9.Click
        Dim frm As New frmForm9A
        frm.Show()
        frm.WindowState = FormWindowState.Maximized
        frm.Focus()
    End Sub

    Private Sub btnESIOnline_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnESIOnline.Click
        Dim frm As New frmESIOnline
        frm.Show()
        frm.WindowState = FormWindowState.Maximized
        frm.Focus()
    End Sub

End Class
