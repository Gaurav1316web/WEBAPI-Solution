'----Panch Raj
Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class FrmMailReceipt
    Const colUserCode As String = "colUserCode"
    Const colUserName As String = "colUserName"
    Const colEmailId As String = "colEmailId"
    Const colmail As String = "colmail"
    Const colChecked As String = "colChecked"
    Dim isnewentry As Boolean
    Public Form_Id As String
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repochecked As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repochecked.FormatString = ""
        repochecked.HeaderText = ""
        repochecked.Name = colChecked
        repochecked.Width = 50
        repochecked.IsVisible = True
        repochecked.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repochecked)

        Dim repoUserCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUserCode.FormatString = ""
        repoUserCode.HeaderText = "User Code"
        repoUserCode.Name = colUserCode
        repoUserCode.Width = 100
        repoUserCode.IsVisible = True
        repoUserCode.WrapText = True
        repoUserCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUserCode)

        Dim repoUserName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUserName.FormatString = ""
        repoUserName.HeaderText = "User Name"
        repoUserName.Name = colUserName
        repoUserName.Width = 200
        repoUserName.IsVisible = True
        repoUserName.WrapText = True
        repoUserName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUserName)

        Dim repoEmailId As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEmailId.FormatString = ""
        repoEmailId.HeaderText = "Email Id"
        repoEmailId.Name = colEmailId
        repoEmailId.Width = 200
        repoEmailId.IsVisible = True
        repoEmailId.WrapText = True
        repoEmailId.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoEmailId)

        'Dim repocheck As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        'repocheck.FormatString = ""
        'repocheck.HeaderText = "colcheck"
        'repocheck.Name = colChecked
        'repocheck.Width = 200
        'repocheck.IsVisible = False
        'repocheck.WrapText = True
        'repocheck.ReadOnly = False
        'gv1.MasterTemplate.Columns.Add(repocheck)

        Dim repomail As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomail.FormatString = ""
        repomail.HeaderText = "mail"
        repomail.Name = colmail
        repomail.Width = 200
        repomail.IsVisible = False
        repomail.WrapText = True
        repomail.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repomail)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = True
        gv1.EnableSorting = True
        gv1.EnableFiltering = True

        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 20
    End Sub
    Sub LoadData(ByVal isShowMsg As Boolean)
        LoadBlankGrid()
        Dim qry As String = "select TSPL_USER_MASTER.User_Code ,User_Name ,TSPL_Mail_Receipt.User_Code as mail_chk,E_mail from TSPL_USER_MASTER Left Join TSPL_Mail_Receipt on TSPL_Mail_Receipt.user_code=TSPL_USER_MASTER.user_CODE "
  
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
            If isShowMsg Then
                clsCommon.MyMessageBoxShow(Me, "No Data found to Display", Me.Text)
            End If
        Else
           For Each dr As DataRow In dt.Rows
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells("colUserCode").Value = clsCommon.myCstr(dr("User_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells("colUserName").Value = clsCommon.myCstr(dr("User_Name"))
                gv1.Rows(gv1.Rows.Count - 1).Cells("colEmailId").Value = clsCommon.myCstr(dr("E_mail"))
                gv1.Rows(gv1.Rows.Count - 1).Cells("colmail").Value = clsCommon.myCstr(dr("mail_chk"))
                If clsCommon.myLen(gv1.CurrentRow.Cells("colmail").Value) <> 0 Then
                    gv1.Rows(gv1.Rows.Count - 1).Cells("colchecked").Value = True
                End If
            Next
            gv1.CurrentRow = gv1.Rows(0)
            
        End If
        
    End Sub
    Private Sub FrmMailReceipt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'LoadBlankGrid()
        LoadData(False)
    End Sub
    Sub SaveData()
        Try

            Dim arr As New List(Of ClsMailReceipt)
            For i As Integer = 0 To gv1.Rows.Count - 1
                Dim obj As New ClsMailReceipt
                If clsCommon.myCBool(gv1.Rows(i).Cells(colChecked).Value) = True Then
                    If clsCommon.myLen(gv1.Rows(i).Cells(colUserCode).Value) > 0 Then
                        obj.User_Code = clsCommon.myCstr(gv1.Rows(i).Cells(colUserCode).Value)

                        arr.Add(obj)
                    End If

                End If

            Next
            
            If (ClsMailReceipt.SaveData(arr, Form_Id)) Then
                clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class
