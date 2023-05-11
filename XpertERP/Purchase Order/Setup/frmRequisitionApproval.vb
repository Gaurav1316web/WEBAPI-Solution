Imports Telerik.WinControls.UI
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common

Public Class FrmRequisitionApproval

    Private Sub FrmRequisitionApproval_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtLevel3.Enabled = False
        Reset()
        LoadData()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    '--------richa Ticket no.BM00000003123 on  15/07/2014
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmRequisitionApproval)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag

    End Sub

    ' -----------------------------------------------
    Private Sub SaveData()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmRequisitionApproval, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim obj As New clsRequisitionApproval()
                obj.Level1 = clsCommon.myCdbl(txtLevel1.Text)
                obj.Level2 = clsCommon.myCdbl(txtLevel2.Text)
                obj.Level3 = clsCommon.myCdbl(txtLevel3.Text)
                If rbtnOff.IsChecked Then
                    obj.Approval_Level = 0
                ElseIf rbtnLBL1.IsChecked Then
                    obj.Approval_Level = 1
                ElseIf rbtnLBL2.IsChecked Then
                    obj.Approval_Level = 2
                ElseIf rbtnLBL3.IsChecked Then
                    obj.Approval_Level = 3
                ElseIf rbtnLBL4.IsChecked Then
                    obj.Approval_Level = 4
                Else
                    obj.Approval_Level = 5
                End If
                If (obj.SaveData(obj)) Then
                    RadMessageBox.Show("Data Saved Successfully")
                    LoadData()
                End If
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        If Not (rbtnOff.IsChecked Or rbtnLBL1.IsChecked Or rbtnLBL2.IsChecked) Then
            If clsCommon.myCdbl(txtLevel1.Text) >= clsCommon.myCdbl(txtLevel2.Text) Then
                clsCommon.MyMessageBoxShow("Level1 user amount can not be greater/equal to level2 user amount")
                Return False
            End If
        End If
        Return True
    End Function

    Sub LoadData()
        Try
            Reset()
            Dim obj As New clsRequisitionApproval()
            obj = clsRequisitionApproval.GetData()
            If (obj IsNot Nothing) Then
                txtLevel1.Text = obj.Level1
                txtLevel2.Text = obj.Level2
                txtLevel3.Text = obj.Level3
                If obj.Approval_Level = 0 Then
                    rbtnOff.IsChecked = True
                ElseIf obj.Approval_Level = 1 Then
                    rbtnLBL1.IsChecked = True
                ElseIf obj.Approval_Level = 2 Then
                    rbtnLBL2.IsChecked = True
                ElseIf obj.Approval_Level = 3 Then
                    rbtnLBL3.IsChecked = True
                ElseIf obj.Approval_Level = 4 Then
                    rbtnLBL4.IsChecked = True
                Else
                    rbtnLBL5.IsChecked = True
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub Reset()
        txtLevel1.Text = "0"
        txtLevel2.Text = "0"
        txtLevel3.Text = "0"
        rbtnLBL1.IsChecked = True
    End Sub

    Private Sub txtLevel2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLevel2.TextChanged
        txtLevel3.Text = txtLevel2.Text
    End Sub

End Class
