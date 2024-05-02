'=================BM00000003241,Updated By Rohit(remark : add code to set alias when columns are added.)..=============
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Threading
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.UI.Export
Imports System.Windows.Forms

Public Class frmFilterColumnsToExport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public isCancel As Boolean = True
    Dim DtColumn As New DataTable

#Region "Variable"
    Public qry As String = ""
    Public whrCls As String = ""
    Public orderByClause As String = ""
    Dim dict As New Dictionary(Of String, ArrayList)
    Public isLoad As Boolean = False
    Public strFileName As String = ""
    Public pivotCols As String = ""
    Public formid As String = ""
    Public ListIEColumns As List(Of String) = Nothing
    Public ListIEColumnsMandatory As List(Of String) = Nothing
#End Region
    '=======Updated By Rohit July 1,2014 .It was Throwing Issue in Employee Master Export(HR and Payroll).============

    'Sub LoadData()
    '    Try
    '        If qry.ToUpper.Contains("FROM") Then
    '            Dim sQuery As String = "Select * " & Trim(qry.Substring(qry.ToUpper.LastIndexOf(" FROM "), qry.Length - qry.ToUpper.LastIndexOf(" FROM ")))
    '            If Trim(sQuery.Substring(sQuery.ToUpper.LastIndexOf("FROM") + 5, sQuery.Length - sQuery.ToUpper.LastIndexOf("FROM") - 5)).Contains(" ") Then
    '                sQuery = "Select * From " & sQuery.Substring(sQuery.ToUpper.LastIndexOf(" FROM") + 6, sQuery.Substring(sQuery.ToUpper.IndexOf("FROM") + 5, sQuery.Length - sQuery.ToUpper.LastIndexOf("FROM") - 5).IndexOf(" "))
    '            End If
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery) '(qry)
    '            DtColumn.Rows.Clear()
    '            DtColumn.Columns.Clear()
    '            If dt IsNot Nothing And dt.Columns.Count > 0 Then
    '                Dim lstColumns As New List(Of String)
    '                DtColumn.Columns.Add("Field Name", GetType(String))
    '                For Each dc As DataColumn In dt.Columns
    '                    If Not qry.ToUpper.Substring(0, qry.ToUpper.IndexOf(" FROM")).Contains(dc.ColumnName.ToUpper) Then
    '                        Dim dr As DataRow = DtColumn.NewRow
    '                        dr(0) = dc.ColumnName
    '                        DtColumn.Rows.Add(dr)
    '                        DtColumn.AcceptChanges()
    '                        'lstColumns.Add(dc.ColumnName)
    '                    End If
    '                Next
    '                cbgCommonGrid.DataSource = DtColumn
    '                cbgCommonGrid.ValueMember = "Field Name"
    '                cbgCommonGrid.DisplayMember = "Field Name"
    '            End If
    '        Else
    '            Dim sQuery As String = qry
    '            Dim ee As System.EventArgs
    '            RadButton1_Click("", ee)
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
    '    End Try
    'End Sub

    '=======Updated By Rohit July 1,2014 .It was Throwing Issue in Employee Master Export(HR and Payroll).============
    '' Anubhooti 24-July-2014 (BM00000003135:Exception on Financial Year screen while exporting data)
    Sub LoadData()
        Try
            If qry.ToUpper.Contains("FROM") Then
                Dim sQuery As String = "Select top 1 * " & Trim(qry.Substring(qry.ToUpper.LastIndexOf(" FROM "), qry.Length - qry.ToUpper.LastIndexOf(" FROM ")))
                If Trim(sQuery.Substring(sQuery.ToUpper.LastIndexOf("FROM") + 5, sQuery.Length - sQuery.ToUpper.LastIndexOf("FROM") - 5)).Contains(" ") Then
                    sQuery = "Select top 1 * From " & sQuery.Substring(sQuery.ToUpper.LastIndexOf(" FROM") + 6, sQuery.Substring(sQuery.ToUpper.IndexOf("FROM") + 5, sQuery.Length - sQuery.ToUpper.LastIndexOf("FROM") - 5).IndexOf(" "))
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery) '(qry)
                DtColumn.Rows.Clear()
                DtColumn.Columns.Clear()
                If dt IsNot Nothing And dt.Columns.Count > 0 Then
                    DtColumn.Columns.Add("Field Name", GetType(String))
                    For Each dc As DataColumn In dt.Columns
                        If Not qry.ToUpper.Substring(0, qry.ToUpper.IndexOf(" FROM")).Contains(dc.ColumnName.ToUpper) Then
                            Dim dr As DataRow = DtColumn.NewRow
                            dr(0) = dc.ColumnName
                            DtColumn.Rows.Add(dr)
                            DtColumn.AcceptChanges()
                        End If
                    Next
                    If clsCommon.myLen(pivotCols) > 0 Then
                        Dim arrCol() As String = pivotCols.Split(",")
                        For Each col As String In arrCol
                            Dim dr As DataRow = DtColumn.NewRow
                            'dr(0) = col
                            dr(0) = col & " As " & col.Replace(".", "_")
                            DtColumn.Rows.Add(dr)
                            DtColumn.AcceptChanges()
                        Next
                    End If
                    cbgCommonGrid.DataSource = DtColumn
                    cbgCommonGrid.ValueMember = "Field Name"
                    cbgCommonGrid.DisplayMember = "Field Name"

                    If ListIEColumnsMandatory IsNot Nothing AndAlso ListIEColumnsMandatory.Count > 0 AndAlso clsCommon.myLen(formid) > 0 Then
                        Dim Tempdt As DataTable = clsDBFuncationality.GetDataTable(qry + " where 1=2 ")
                        If Tempdt IsNot Nothing AndAlso Tempdt.Columns.Count > 0 Then
                            ListIEColumns = New List(Of String)
                            For Each Tempdc As DataColumn In Tempdt.Columns
                                ListIEColumns.Add(Tempdc.ColumnName)
                            Next
                        End If
                        If ListIEColumns IsNot Nothing AndAlso ListIEColumns.Count > 0 Then
                            btnTemplate.Visible = True
                        End If
                    End If
                End If
            Else
                Dim sQuery As String = qry
                Dim ee As New System.EventArgs
                RadButton1_Click("", ee)
            End If
        Catch ex As Exception
            ' clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try
    End Sub

    Private Sub frmFilterColumnsToExport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isLoad = True
        LoadData()
    End Sub

    Private Sub frmFilterColumnsToExport_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'LoadData()
    End Sub

    Private Sub cboColumns_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs)
        'LoadGrid()
    End Sub

    'Sub LoadGrid()
    '    Try
    '        cbgCommonGrid.Rows.Clear()
    '        cbgCommonGrid.Columns.Clear()

    '        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
    '        repoLineNo = New GridViewDecimalColumn()
    '        repoLineNo.FormatString = ""
    '        repoLineNo.HeaderText = "Line No"
    '        repoLineNo.Name = colLineNo
    '        repoLineNo.Width = 50
    '        repoLineNo.ReadOnly = True
    '        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        cbgCommonGrid.MasterTemplate.Columns.Add(repoLineNo)
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
    '    End Try
    'End Sub

    'Private Sub cboColumns_SelectedIndexChanging(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangingCancelEventArgs)

    '    Try
    '        If dict.ContainsKey(clsCommon.myCstr(cboColumns.SelectedValue)) Then
    '            dict.Remove(clsCommon.myCstr(cboColumns.SelectedValue))
    '        End If
    '        dict.Add(clsCommon.myCstr(cboColumns.SelectedValue), cbgCommonGrid.CheckedValue)
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
    '    End Try

    'End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            'If dict.ContainsKey(clsCommon.myCstr(cboColumns.SelectedValue)) Then
            '    dict.Remove(clsCommon.myCstr(cboColumns.SelectedValue))
            'End If
            'If cbgCommonGrid IsNot Nothing AndAlso cbgCommonGrid.AllValue IsNot Nothing AndAlso cbgCommonGrid.CheckedValue.Count > 0 Then
            '    dict.Add(clsCommon.myCstr(cboColumns.SelectedValue), cbgCommonGrid.CheckedValue)
            'End Iff
            Dim ss As String = Trim(qry.Substring(qry.ToUpper.LastIndexOf(" FROM"), qry.Length - qry.ToUpper.LastIndexOf(" FROM")))
            If Trim(ss.Substring(ss.ToUpper.LastIndexOf("FROM") + 5, ss.Length - ss.ToUpper.LastIndexOf("FROM") - 5)).Contains(" ") Then
                Try
                    ss = ss.Substring(ss.ToUpper.LastIndexOf(" FROM") + 6, ss.Substring(ss.ToUpper.IndexOf("FROM") + 5, ss.Length - ss.ToUpper.LastIndexOf("FROM") - 5).IndexOf("as") + 5)
                    If ss.Length < 5 Or ss.ToUpper.Contains("LEFT") Or ss.ToUpper.Contains("RIGHT") Or ss.ToUpper.Contains("INNER") Then
                        Throw New Exception("Wrong Value")
                    End If
                    ss = ss.Substring(ss.ToUpper.LastIndexOf("AS") + 2, ss.Length - ss.ToUpper.LastIndexOf("AS") - 2)
                Catch ex As Exception
                    'ss = ss.Substring(ss.ToUpper.LastIndexOf(" FROM") + 6, ss.Substring(ss.ToUpper.IndexOf("FROM") + 5, ss.Length - ss.ToUpper.LastIndexOf("FROM") - 5).IndexOf(" "))
                    If ss.Length < 5 Then
                        ss = "FROM"
                    Else
                        ss = ss.Substring(0, ss.IndexOf(" "))
                    End If

                End Try
            End If
            Dim strcol As String = ""
            For Each row As String In cbgCommonGrid.CheckedValue
                strcol &= "," & IIf(ss.ToUpper.Contains("FROM"), "", ss & ".") & row.ToString
            Next
            'Dim query As String = qry.Substring(0, qry.ToUpper.LastIndexOf(" FROM")) & strcol & qry.Substring(qry.ToUpper.LastIndexOf(" FROM"), qry.Length - qry.ToUpper.LastIndexOf(" FROM"))
            Dim query As String = qry.Substring(0, qry.ToUpper.IndexOf(" FROM")) & strcol & qry.Substring(qry.ToUpper.IndexOf(" FROM"), qry.Length - qry.ToUpper.IndexOf(" FROM"))
            If clsCommon.myLen(whrCls) > 0 Then
                query += whrCls
            End If
            'If clsCommon.myLen(orderByClause) > 0 Then
            '    query += orderByClause
            'End If

            qry = "Select * from (" + query + " ) xx "
            'qry = query
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
                whereCls = " where  1=1 and (" + whereCls + ") "
            End If

            qry += whereCls
            '===================Rohit on june 05,2014 to Load Blank Sheet of Excel===========
            'If chkBlankSheet.Checked Then
            '    qry += IIf(whereCls = "", " where 1=2 ", " and 1=2 ")
            'End If
            '========================================================================
            qry += orderByClause
            Me.isCancel = False
            Me.Close()
        Catch ex As Exception
            'clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Me.isCancel = True
        Me.Close()
    End Sub

    Private Sub btnTemplate_Click(sender As Object, e As EventArgs) Handles btnTemplate.Click
        Try

            Dim frm As New frmTemplateExpImp(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, formid)
            frm.ListImpExpColumns1 = ListIEColumns
            frm.ListImpExpColumnsMandatory1 = ListIEColumnsMandatory
            frm.dtReportType = Nothing
            frm.Report_Type = ""
            frm.ShowDialog()

        Catch ex As Exception

        End Try
    End Sub
End Class
