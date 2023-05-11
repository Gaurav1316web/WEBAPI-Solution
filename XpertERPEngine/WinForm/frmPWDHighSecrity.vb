Imports common
Imports System.Data.SqlClient

Public Class frmPWDHighSecrity
    Public isPasswordCorrect As Boolean = False
    Sub New(ByVal trans As SqlTransaction)
        InitializeComponent()
    End Sub

    Private Sub FrmPWD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblAnyText.Text = clsCommon.HighSecurityGetNumber(100000, 999999)
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        If clsCommon.myLen(txtPWd.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please enter password", Me.Text)
            Exit Sub
        End If

        If clsCommon.HighSecurityVerifyNumber(clsCommon.myCDecimal(lblAnyText.Text)) = clsCommon.myCDecimal(txtPWd.Text) Then
            isPasswordCorrect = True
            Me.Close()
        Else
            common.clsCommon.MyMessageBoxShow(Me, "Wrong password", Me.Text)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        isPasswordCorrect = False
        Me.Close()
    End Sub


End Class
