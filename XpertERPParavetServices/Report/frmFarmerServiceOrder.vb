Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Threading
Imports System.Data.Sql
Imports common

Public Class frmFarmerServiceOrder
    Inherits FrmMainTranScreen

    Const colSMSDetails As String = "SMS Details"
    Const colSender As String = "Sender"
    Const colReceivedDateTime As String = "Received Date Time"
    Const colReplyDateTime As String = "Reply Date Time"
    Const colReplyDetails As String = "Reply Details"

    Private Sub rptSMSDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadBlankGrid()
        Reset()
    End Sub
    Sub LoadBlankGrid()

        Gv1.AddNewRowPosition = SystemRowPosition.Bottom
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.EnableFiltering = False

        Dim SMS_Details As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SMS_Details.FormatString = ""
        SMS_Details.HeaderText = "SMS Details"
        SMS_Details.Name = colSMSDetails
        SMS_Details.Width = 250
        SMS_Details.ReadOnly = False
        'SMS_Details.TextImageRelation = TextImageRelation.TextBeforeImage
        'SMS_Details.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'SMS_Details.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Gv1.MasterTemplate.Columns.Add(SMS_Details)

        Dim Sender_Details As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Sender_Details.FormatString = ""
        Sender_Details.HeaderText = "Sender"
        Sender_Details.Name = colSender
        Sender_Details.Width = 150
        Sender_Details.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(Sender_Details)

        Dim Received_Date_Time As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Received_Date_Time.FormatString = ""
        Received_Date_Time.HeaderText = "Received Date Time"
        Received_Date_Time.Name = colReceivedDateTime
        Received_Date_Time.Width = 150
        Received_Date_Time.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(Received_Date_Time)

        Dim Reply_Date_Time As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Reply_Date_Time.FormatString = ""
        Reply_Date_Time.HeaderText = "Reply Date Time"
        Reply_Date_Time.Name = colReplyDateTime
        Reply_Date_Time.Width = 150
        Reply_Date_Time.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(Reply_Date_Time)

        Dim Reply_Details As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Reply_Details.FormatString = ""
        Reply_Details.HeaderText = "Reply Details"
        Reply_Details.Name = colReplyDetails
        Reply_Details.Width = 250
        Reply_Details.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(Reply_Details)

        Gv1.AllowDeleteRow = True
        Gv1.AllowAddNewRow = True
        Gv1.ShowGroupPanel = False
        Gv1.AllowColumnReorder = False
        Gv1.AllowRowReorder = False
        Gv1.EnableSorting = False
        Gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Public Sub Reset()
        Gv1.DataSource = Nothing
        ' Gv1.Rows.Clear()
        'Gv1.Columns.Clear()
        'Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        fndSMS.arrValueMember = Nothing
        fndSender.arrValueMember = Nothing
        fndReplyDetails.arrValueMember = Nothing
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadBlankGrid()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub
End Class
