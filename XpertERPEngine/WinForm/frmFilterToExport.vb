Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Threading
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.UI.Export
Imports System.Windows.Forms

Public Class frmFilterToExport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public isCancel As Boolean = True

#Region "Variable"
    Public qry As String = ""
    Public whrCls As String = ""
    Public orderByClause As String = ""
    Dim dict As New Dictionary(Of String, ArrayList)
    Public isLoad As Boolean = False
    Public strFileName As String = ""
#End Region

    Sub LoadData()
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Columns.Count > 0 Then
                Dim lstColumns As New List(Of String)
                For Each dc As DataColumn In dt.Columns
                    lstColumns.Add(dc.ColumnName)
                Next
                cboColumns.DataSource = lstColumns
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmFilterToExport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isLoad = True
        LoadData()
    End Sub

    Private Sub frmFilterToExport_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'LoadData()
    End Sub

    Private Sub cboColumns_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboColumns.SelectedIndexChanged
        LoadGrid()
    End Sub

    Sub LoadGrid()
        Try
            Dim query As String = qry
            If clsCommon.myLen(whrCls) > 0 Then
                query += whrCls
            End If
            If clsCommon.myLen(orderByClause) > 0 Then
                '            query += orderByClause
            End If

            If query.ToUpper().Contains("ORDER BY") Then
                query = "select distinct [" + cboColumns.Text + "] from ( " + query + " ) xx "
            Else
                query = "select distinct [" + cboColumns.Text + "] from ( " + query + " ) xx order by [" + cboColumns.Text + "] "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                cbgCommonGrid.DataSource = dt
                cbgCommonGrid.ValueMember = cboColumns.Text
                cbgCommonGrid.DisplayMember = cboColumns.Text

                If dict.ContainsKey(clsCommon.myCstr(cboColumns.SelectedValue)) Then
                    cbgCommonGrid.CheckedValue = dict(clsCommon.myCstr(cboColumns.SelectedValue))
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub cboColumns_SelectedIndexChanging(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangingCancelEventArgs) Handles cboColumns.SelectedIndexChanging

        Try
            If dict.ContainsKey(clsCommon.myCstr(cboColumns.SelectedValue)) Then
                dict.Remove(clsCommon.myCstr(cboColumns.SelectedValue))
            End If
            dict.Add(clsCommon.myCstr(cboColumns.SelectedValue), cbgCommonGrid.CheckedValue)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            If dict.ContainsKey(clsCommon.myCstr(cboColumns.SelectedValue)) Then
                dict.Remove(clsCommon.myCstr(cboColumns.SelectedValue))
            End If
            If cbgCommonGrid IsNot Nothing AndAlso cbgCommonGrid.AllValue IsNot Nothing AndAlso cbgCommonGrid.CheckedValue.Count > 0 Then
                dict.Add(clsCommon.myCstr(cboColumns.SelectedValue), cbgCommonGrid.CheckedValue)
            End If

            Dim query As String = qry
            If clsCommon.myLen(whrCls) > 0 Then
                query += whrCls
            End If
            If clsCommon.myLen(orderByClause) > 0 Then
                query += orderByClause
            End If

            qry = "Select * from (" + query + " ) xx "
            Dim whereCls As String = ""
            If dict IsNot Nothing AndAlso dict.Count > 0 Then
                For Each kvp As KeyValuePair(Of String, ArrayList) In dict
                    If clsCommon.myLen(kvp.Key) > 0 AndAlso kvp.Value.Count > 0 Then
                        If clsCommon.myLen(whereCls) = 0 Then
                            whereCls += " [" + kvp.Key + "] in (" + clsCommon.GetMulcallString(kvp.Value) + ")"
                        Else
                            whereCls += " and [" + kvp.Key + "] in (" + clsCommon.GetMulcallString(kvp.Value) + ") "
                        End If
                    End If

                Next kvp
            End If
            If clsCommon.myLen(whereCls) > 0 Then
                whereCls = "where 1=1 and (" + whereCls + ") "
            End If

            qry += whereCls
            '===================Rohit on june 05,2014 to Load Blank Sheet of Excel===========
            If chkBlankSheet.Checked Then
                qry += IIf(whereCls = "", " where 1=2 ", " and 1=2 ")
            End If
            '========================================================================
            qry += orderByClause
            Me.isCancel = False
            Me.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Me.isCancel = True
        Me.Close()
    End Sub
End Class
