Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports common
'' CREATED BY : SURAJ
''Start Date: 10-05-2011
'' End Date:10-05-2011
Public Class frmGLAccountDetails
    Dim userCode, companyCode As String
    Dim dr As DataTable
    Dim ds As New DataSet()
    Dim dt As New DataTable()
    Dim btntooltip As ToolTip = New ToolTip()

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    'To insert the data into the table(TSPL_GL_ACCOUNTS)
    Private Sub funinsert()
        Dim strstatus As String
        Dim strautoallocation As String
        Dim strrollup As String
        Dim strcontrolaccount As String
        Dim strmulticurrency As String
        If rdactive.IsChecked = True Then
            strstatus = "Y"
        Else
            strstatus = "N"
        End If
        If chkautoallocation.Checked = True Then
            strautoallocation = "Y"
        Else
            strautoallocation = "N"
        End If
        If chkcontrolaccount.Checked = True Then
            strcontrolaccount = "Y"
        Else
            strcontrolaccount = "N"
        End If
        If chkrollup.Checked = True Then
            strrollup = "Y"
        Else
            strrollup = "N"
        End If
        If chkmulticurrency.Checked = True Then
            strmulticurrency = "Y"
        Else
            strmulticurrency = "N"
        End If
        Dim strdesc1, strdesc2, strdesc3, strdesc4, strdesc5, strdesc6, strdesc7, strdesc8, strdesc9, strdesc10 As String
        Dim strcode1, strcode2, strcode3, strcode4, strcode5, strcode6, strcode7, strcode8, strcode9, strcode10 As String
        strcode1 = fndaccount.txtValue.Text
        ' Dim i As Integer = connectSql.RunScalar("select Seg_Length  from TSPL_GL_SEGMENT where Seg_No = '1'")
        ' Dim strcode1 As String = t.Substring(0, i)
        'this is for test
        strcode10 = ""
        strdesc10 = ""
        strdesc1 = txtdesc.Text
        strcode6 = ""
        strdesc6 = ""
        strcode5 = ""
        strdesc5 = ""
        strcode4 = ""
        strdesc4 = ""
        strcode2 = ""
        strdesc2 = ""
        strcode3 = ""
        strdesc3 = ""
        strcode7 = ""
        strdesc7 = ""
        strcode8 = ""
        strdesc9 = ""
        strdesc8 = ""
        strcode9 = ""
        If dgvsegment.RowCount = 5 Then
            strcode2 = dgvsegment.Rows(0).Cells(1).Value
            strdesc2 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode2 + "'")
            strcode3 = dgvsegment.Rows(1).Cells(1).Value
            strdesc3 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode3 + "'")
            strcode4 = dgvsegment.Rows(2).Cells(1).Value
            strdesc4 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode4 + "'")
            strcode5 = dgvsegment.Rows(3).Cells(1).Value
            strdesc5 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode5 + "'")
            strcode6 = dgvsegment.Rows(4).Cells(1).Value
            strdesc6 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode6 + "'")
        ElseIf dgvsegment.RowCount = 4 Then
            strcode2 = dgvsegment.Rows(0).Cells(1).Value
            strdesc2 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode2 + "'")
            strcode3 = dgvsegment.Rows(1).Cells(1).Value
            strdesc3 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode3 + "'")
            strcode4 = dgvsegment.Rows(2).Cells(1).Value
            strdesc4 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode4 + "'")
            strcode5 = dgvsegment.Rows(3).Cells(1).Value
            strdesc5 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode5 + "'")
        ElseIf dgvsegment.RowCount = 3 Then
            strcode2 = dgvsegment.Rows(0).Cells(1).Value
            strdesc2 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode2 + "'")
            strcode3 = dgvsegment.Rows(1).Cells(1).Value
            strdesc3 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode3 + "'")
            strcode4 = dgvsegment.Rows(2).Cells(1).Value
            strdesc4 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode4 + "'")
        ElseIf dgvsegment.RowCount = 2 Then
            strcode2 = dgvsegment.Rows(0).Cells(1).Value
            strdesc2 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode2 + "'")
            strcode3 = dgvsegment.Rows(1).Cells(1).Value
            strdesc3 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode3 + "'")
        ElseIf dgvsegment.RowCount = 1 Then
            strcode2 = dgvsegment.Rows(0).Cells(1).Value
            strdesc2 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode2 + "'")
        ElseIf dgvsegment.RowCount = 6 Then
            strcode2 = dgvsegment.Rows(0).Cells(1).Value
            strdesc2 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode2 + "'")
            strcode3 = dgvsegment.Rows(1).Cells(1).Value
            strdesc3 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode3 + "'")
            strcode4 = dgvsegment.Rows(2).Cells(1).Value
            strdesc4 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode4 + "'")
            strcode5 = dgvsegment.Rows(3).Cells(1).Value
            strdesc5 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode5 + "'")
            strcode6 = dgvsegment.Rows(4).Cells(1).Value
            strdesc6 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode6 + "'")
            strcode7 = dgvsegment.Rows(5).Cells(1).Value
            strdesc7 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode7 + "'")
        ElseIf dgvsegment.RowCount = 7 Then
            strcode2 = dgvsegment.Rows(0).Cells(1).Value
            strdesc2 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode2 + "'")
            strcode3 = dgvsegment.Rows(1).Cells(1).Value
            strdesc3 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode3 + "'")
            strcode4 = dgvsegment.Rows(2).Cells(1).Value
            strdesc4 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode4 + "'")
            strcode5 = dgvsegment.Rows(3).Cells(1).Value
            strdesc5 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode5 + "'")
            strcode6 = dgvsegment.Rows(4).Cells(1).Value
            strdesc6 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode6 + "'")
            strcode7 = dgvsegment.Rows(5).Cells(1).Value
            strdesc7 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode7 + "'")
            strcode8 = dgvsegment.Rows(6).Cells(1).Value
            strdesc8 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode8 + "'")
        ElseIf dgvsegment.RowCount = 8 Then
            strcode2 = dgvsegment.Rows(0).Cells(1).Value
            strdesc2 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode2 + "'")
            strcode3 = dgvsegment.Rows(1).Cells(1).Value
            strdesc3 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode3 + "'")
            strcode4 = dgvsegment.Rows(2).Cells(1).Value
            strdesc4 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode4 + "'")
            strcode5 = dgvsegment.Rows(3).Cells(1).Value
            strdesc5 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode5 + "'")
            strcode6 = dgvsegment.Rows(4).Cells(1).Value
            strdesc6 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode6 + "'")
            strcode7 = dgvsegment.Rows(5).Cells(1).Value
            strdesc7 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode7 + "'")
            strcode8 = dgvsegment.Rows(6).Cells(1).Value
            strdesc8 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode8 + "'")
            strcode9 = dgvsegment.Rows(7).Cells(1).Value
            strdesc9 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode9 + "'")
        ElseIf dgvsegment.RowCount = 9 Then
            strcode2 = dgvsegment.Rows(0).Cells(1).Value
            strdesc2 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode2 + "'")
            strcode3 = dgvsegment.Rows(1).Cells(1).Value
            strdesc3 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode3 + "'")
            strcode4 = dgvsegment.Rows(2).Cells(1).Value
            strdesc4 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode4 + "'")
            strcode5 = dgvsegment.Rows(3).Cells(1).Value
            strdesc5 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode5 + "'")
            strcode6 = dgvsegment.Rows(4).Cells(1).Value
            strdesc6 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode6 + "'")
            strcode7 = dgvsegment.Rows(5).Cells(1).Value
            strdesc7 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode7 + "'")
            strcode8 = dgvsegment.Rows(6).Cells(1).Value
            strdesc8 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode8 + "'")
            strcode9 = dgvsegment.Rows(7).Cells(1).Value
            strdesc9 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode9 + "'")
            strcode10 = dgvsegment.Rows(8).Cells(1).Value
            strdesc10 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode10 + "'")

        End If
        Dim op As Integer = connectSql.RunScalar("select Seg_Length  from TSPL_GL_SEGMENT where Seg_No = '1'")

        Dim strsegname1 As String = strcode1.Substring(0, op)
        Dim straccount As String = ""
        For m As Integer = 0 To dgvsegment.Rows.Count - 1
            If String.IsNullOrEmpty(dgvsegment.Rows(m).Cells(1).Value) Then
            Else
                straccount += "-" + dgvsegment.Rows(m).Cells(1).Value
            End If
        Next
        Dim account As String = strsegname1 + straccount
        Try
            connectSql.RunSp("SP_TSPL_GL_ACCOUNTS_INSERT", New SqlParameter("@accountcode", account), New SqlParameter("@description", txtdesc.Text), New SqlParameter("@strcode", fndstructurecode.txtValue.Text), New SqlParameter("@strdesc", txtstrdesc.Text), New SqlParameter("@accbal", ddlnormalbal.Text), New SqlParameter("@acctype", ddlaccounttype.Text), New SqlParameter("@accgroupcode", fndaccountgroup.txtValue.Text), New SqlParameter("@accgroupdesc", acctdesc.Text), New SqlParameter("@status", strstatus), New SqlParameter("@controlaccount", strcontrolaccount), New SqlParameter("@autoallocation", strautoallocation), New SqlParameter("@rollup", strrollup), New SqlParameter("@multicurrency", strmulticurrency), New SqlParameter("@accsegcode1", strsegname1), New SqlParameter("@accsegdesc1", txtdesc.Text), New SqlParameter("@accsegcode2", strcode2), New SqlParameter("@accsegdesc2", strdesc2), New SqlParameter("@accsegcode3", strcode3), New SqlParameter("@accsegdesc3", strdesc3), New SqlParameter("@accsegcode4", strcode4), New SqlParameter("@accsegdesc4", strdesc4), New SqlParameter("@accsegcode5", strcode5), New SqlParameter("@accsegdesc5", strdesc5), New SqlParameter("@accsegcode6", strcode6), New SqlParameter("@accsegdesc6", strdesc6), New SqlParameter("@accsegcode7", strcode7), New SqlParameter("@accsegdesc7", strdesc7), New SqlParameter("@accsegcode8", strcode8), New SqlParameter("@accsegdesc8", strdesc8), New SqlParameter("@accsegcode9", strcode9), New SqlParameter("@accsegdesc9", strdesc9), New SqlParameter("@accsegcode10", strcode10), New SqlParameter("@accsegdesc10", strdesc10), New SqlParameter("@closetoseg", ddlclosetosegment.Text), New SqlParameter("@closetoaccount", txtclosetoaccount.Text), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            fndaccount.txtValue.Text = fndaccount.txtValue.Text + straccount
            myMessages.insert()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    '' To Count the length of account
    Private Function funaccountcodelength() As String
        Dim strsegname1 As String = fndaccount.txtValue.Text
        Dim straccount As String = ""
        For m As Integer = 0 To dgvsegment.Rows.Count - 1
            If String.IsNullOrEmpty(dgvsegment.Rows(m).Cells(1).Value) Then
            Else
                straccount += "-" + dgvsegment.Rows(m).Cells(1).Value
            End If
        Next
        Dim account As String = strsegname1 + straccount
        Return account
    End Function
    ''To Update the data into the table(TSPL_GL_ACCOUNTS)
    Private Sub funupdate()
        Dim strstatus As String
        Dim strautoallocation As String
        Dim strrollup As String
        Dim strcontrolaccount As String
        Dim strmulticurrency As String
        If rdactive.IsChecked = True Then
            strstatus = "Y"
        Else
            strstatus = "N"
        End If
        If chkautoallocation.Checked = True Then
            strautoallocation = "Y"
        Else
            strautoallocation = "N"
        End If
        If chkcontrolaccount.Checked = True Then
            strcontrolaccount = "Y"
        Else
            strcontrolaccount = "N"
        End If
        If chkrollup.Checked = True Then
            strrollup = "Y"
        Else
            strrollup = "N"
        End If
        If chkmulticurrency.Checked = True Then
            strmulticurrency = "Y"
        Else
            strmulticurrency = "N"
        End If
        Dim strdesc1, strdesc2, strdesc3, strdesc4, strdesc5, strdesc6, strdesc7, strdesc8, strdesc9, strdesc10 As String
        Dim strcode1, strcode2, strcode3, strcode4, strcode5, strcode6, strcode7, strcode8, strcode9, strcode10 As String
        strdesc9 = ""
        strdesc10 = ""
        strcode10 = ""
        strcode9 = ""
        strcode8 = ""
        strdesc8 = ""
        strdesc7 = ""
        strcode7 = ""
        strdesc2 = ""
        strdesc3 = ""
        strdesc4 = ""
        strdesc5 = ""
        strdesc6 = ""
        strcode2 = ""
        strcode3 = ""
        strcode4 = ""
        strcode5 = ""
        strcode6 = ""
        strcode1 = fndaccount.txtValue.Text
        strdesc1 = txtdesc.Text
        If dgvsegment.RowCount = 5 Then
            strcode2 = dgvsegment.Rows(0).Cells(1).Value
            strdesc2 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode2 + "'")
            strcode3 = dgvsegment.Rows(1).Cells(1).Value
            strdesc3 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode3 + "'")
            strcode4 = dgvsegment.Rows(2).Cells(1).Value
            strdesc4 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode4 + "'")
            strcode5 = dgvsegment.Rows(3).Cells(1).Value
            strdesc5 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode5 + "'")
            strcode6 = dgvsegment.Rows(4).Cells(1).Value
            strdesc6 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode6 + "'")
        ElseIf dgvsegment.RowCount = 4 Then
            strcode2 = dgvsegment.Rows(0).Cells(1).Value
            strdesc2 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode2 + "'")
            strcode3 = dgvsegment.Rows(1).Cells(1).Value
            strdesc3 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode3 + "'")
            strcode4 = dgvsegment.Rows(2).Cells(1).Value
            strdesc4 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode4 + "'")
            strcode5 = dgvsegment.Rows(3).Cells(1).Value
            strdesc5 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode5 + "'")
        ElseIf dgvsegment.RowCount = 3 Then
            strcode2 = dgvsegment.Rows(0).Cells(1).Value
            strdesc2 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode2 + "'")
            strcode3 = dgvsegment.Rows(1).Cells(1).Value
            strdesc3 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode3 + "'")
            strcode4 = dgvsegment.Rows(2).Cells(1).Value
            strdesc4 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode4 + "'")
        ElseIf dgvsegment.RowCount = 2 Then
            strcode2 = dgvsegment.Rows(0).Cells(1).Value
            strdesc2 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode2 + "'")
            strcode3 = dgvsegment.Rows(1).Cells(1).Value
            strdesc3 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode3 + "'")
        ElseIf dgvsegment.RowCount = 1 Then
            strcode2 = dgvsegment.Rows(0).Cells(1).Value
            strdesc2 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode2 + "'")
        ElseIf dgvsegment.RowCount = 6 Then
            strcode2 = dgvsegment.Rows(0).Cells(1).Value
            strdesc2 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode2 + "'")
            strcode3 = dgvsegment.Rows(1).Cells(1).Value
            strdesc3 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode3 + "'")
            strcode4 = dgvsegment.Rows(2).Cells(1).Value
            strdesc4 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode4 + "'")
            strcode5 = dgvsegment.Rows(3).Cells(1).Value
            strdesc5 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode5 + "'")
            strcode6 = dgvsegment.Rows(4).Cells(1).Value
            strdesc6 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode6 + "'")
            strcode7 = dgvsegment.Rows(5).Cells(1).Value
            strdesc7 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode7 + "'")
        ElseIf dgvsegment.RowCount = 7 Then
            strcode2 = dgvsegment.Rows(0).Cells(1).Value
            strdesc2 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode2 + "'")
            strcode3 = dgvsegment.Rows(1).Cells(1).Value
            strdesc3 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode3 + "'")
            strcode4 = dgvsegment.Rows(2).Cells(1).Value
            strdesc4 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode4 + "'")
            strcode5 = dgvsegment.Rows(3).Cells(1).Value
            strdesc5 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode5 + "'")
            strcode6 = dgvsegment.Rows(4).Cells(1).Value
            strdesc6 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode6 + "'")
            strcode7 = dgvsegment.Rows(5).Cells(1).Value
            strdesc7 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode7 + "'")
            strcode8 = dgvsegment.Rows(6).Cells(1).Value
            strdesc8 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode8 + "'")
        ElseIf dgvsegment.RowCount = 8 Then
            strcode2 = dgvsegment.Rows(0).Cells(1).Value
            strdesc2 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode2 + "'")
            strcode3 = dgvsegment.Rows(1).Cells(1).Value
            strdesc3 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode3 + "'")
            strcode4 = dgvsegment.Rows(2).Cells(1).Value
            strdesc4 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode4 + "'")
            strcode5 = dgvsegment.Rows(3).Cells(1).Value
            strdesc5 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode5 + "'")
            strcode6 = dgvsegment.Rows(4).Cells(1).Value
            strdesc6 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode6 + "'")
            strcode7 = dgvsegment.Rows(5).Cells(1).Value
            strdesc7 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode7 + "'")
            strcode8 = dgvsegment.Rows(6).Cells(1).Value
            strdesc8 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode8 + "'")
            strcode9 = dgvsegment.Rows(7).Cells(1).Value
            strdesc9 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode9 + "'")
        ElseIf dgvsegment.RowCount = 9 Then
            strcode2 = dgvsegment.Rows(0).Cells(1).Value
            strdesc2 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode2 + "'")
            strcode3 = dgvsegment.Rows(1).Cells(1).Value
            strdesc3 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode3 + "'")
            strcode4 = dgvsegment.Rows(2).Cells(1).Value
            strdesc4 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode4 + "'")
            strcode5 = dgvsegment.Rows(3).Cells(1).Value
            strdesc5 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode5 + "'")
            strcode6 = dgvsegment.Rows(4).Cells(1).Value
            strdesc6 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode6 + "'")
            strcode7 = dgvsegment.Rows(5).Cells(1).Value
            strdesc7 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode7 + "'")
            strcode8 = dgvsegment.Rows(6).Cells(1).Value
            strdesc8 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode8 + "'")
            strcode9 = dgvsegment.Rows(7).Cells(1).Value
            strdesc9 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode9 + "'")
            strcode10 = dgvsegment.Rows(8).Cells(1).Value
            strdesc10 = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Segment_code = '" + strcode10 + "'")
        End If
        Dim t As String = fndaccount.txtValue.Text
        Dim i As Integer = connectSql.RunScalar("select Seg_Length  from TSPL_GL_SEGMENT where Seg_No = '1'")
        Dim strsegname1 As String = t.Substring(0, i)
        Try
            connectSql.RunSp("SP_TSPL_GL_ACCOUNTS_UPDATE", New SqlParameter("@accountcode", fndaccount.txtValue.Text), New SqlParameter("@description", txtdesc.Text), New SqlParameter("@strcode", fndstructurecode.txtValue.Text), New SqlParameter("@strdesc", txtstrdesc.Text), New SqlParameter("@accbal", ddlnormalbal.Text), New SqlParameter("@acctype", ddlaccounttype.Text), New SqlParameter("@accgroupcode", fndaccountgroup.txtValue.Text), New SqlParameter("@accgroupdesc", acctdesc.Text), New SqlParameter("@status", strstatus), New SqlParameter("@controlaccount", strcontrolaccount), New SqlParameter("@autoallocation", strautoallocation), New SqlParameter("@rollup", strrollup), New SqlParameter("@multicurrency", strmulticurrency), New SqlParameter("@accsegcode1", strsegname1), New SqlParameter("@accsegdesc1", txtdesc.Text), New SqlParameter("@accsegcode2", strcode2), New SqlParameter("@accsegdesc2", strdesc2), New SqlParameter("@accsegcode3", strcode3), New SqlParameter("@accsegdesc3", strdesc3), New SqlParameter("@accsegcode4", strcode4), New SqlParameter("@accsegdesc4", strdesc4), New SqlParameter("@accsegcode5", strcode5), New SqlParameter("@accsegdesc5", strdesc5), New SqlParameter("@accsegcode6", strcode6), New SqlParameter("@accsegdesc6", strdesc6), New SqlParameter("@accsegcode7", strcode7), New SqlParameter("@accsegdesc7", strdesc7), New SqlParameter("@accsegcode8", strcode8), New SqlParameter("@accsegdesc8", strdesc8), New SqlParameter("@accsegcode9", strcode9), New SqlParameter("@accsegdesc9", strdesc9), New SqlParameter("@accsegcode10", strcode10), New SqlParameter("@accsegdesc10", strdesc10), New SqlParameter("@closetoseg", ddlclosetosegment.Text), New SqlParameter("@closetoaccount", txtclosetoaccount.Text), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", objCommonVar.CurrentUserCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
            myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    ''To Delete the data from the table(TSPL_GL_ACCOUNTS)
    Private Sub fundelete(Optional ByVal Reason As String = "")
        Try
            connectSql.RunSp("SP_TSPL_GL_ACCOUNTS_DELETE", New SqlParameter("accountcode", fndaccount.txtValue.Text))
            myMessages.delete()
            saveCancelLog(Reason, "Delete", Nothing)
            funreset()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    ''To Reset all the field on the screen
    Private Sub funreset()
        fndaccount.txtValue.Text = ""
        fndaccountgroup.txtValue.Text = ""
        fndstructurecode.txtValue.Text = ""
        fndstructurecode.Enabled = True
        fndfromacct.txtValue.Text = ""
        fndtoacct.txtValue.Text = ""
        fndtoacct.Enabled = False
        ddlnormalbal.Text = "select"
        ddlaccounttype.Text = "select"
        txtdesc.Text = ""
        txtstrdesc.Text = ""
        acctdesc.Text = ""
        rdactive.IsChecked = True
        chkautoallocation.Checked = False
        chkcontrolaccount.Checked = False
        chkmulticurrency.Checked = False
        chkrollup.Checked = False
        btnsave.Text = "&Save"
        btndelete.Enabled = False
        dgvsegment.DataSource = Nothing
        ddlclosetosegment.Text = ""
        txtclosetoaccount.Text = ""
        chkcontrolaccount.Enabled = False
        dgvsubledger.DataSource = Nothing
        'dgvsegment.AutoGenerateColumns = False
        'dgvsegment.Rows.Clear()
        Dim strsegname As String = "select seg_name from TSPL_GL_SEGMENT where seg_no <> 1"
        transportSql.FillGridView(strsegname, dgvsegment)
        dgvsegment.Columns(0).FieldName = "seg_name"
        'dgvsegment.Columns(1).ReadOnly = True
        For k As Integer = 0 To dgvsegment.Rows.Count - 1
            If dgvsegment.Rows(k).Cells(1).ReadOnly = False Then
                dgvsegment.Rows(k).Cells(1).ReadOnly = True
            End If
            If Not String.IsNullOrEmpty(dgvsegment.Rows(k).Cells(1).Value) Then
                String.IsNullOrEmpty(dgvsegment.Rows(k).Cells(1).Value)
            End If
        Next
        chkrollup.Enabled = True
        chkcontrolaccount.Enabled = True
        dgvrollup.DataSource = Nothing
        RadTreeView1.Nodes.Clear()
        chkautoallocation.Enabled = True
        fndsourcecode.txtValue.Text = ""
        dgvallocation.DataSource = Nothing
        dgvallocation.Rows.Clear()
        dgvallocation.AllowAddNewRow = True


    End Sub
    '' To Fill all the record
    Private Sub funfill()
        Try
            Dim straccount As String = "select Description, Str_Code, Str_Description, Account_Balance, Account_Type, Account_Group_Code, Account_Group_Desc, Status, ControlAccount, AutoAllocation, Rollup, multicurrency,Close_To_Seg , Close_To_Acct   from TSPL_GL_ACCOUNTS  where Account_Code = '" + fndaccount.txtValue.Text + "'"
            'dr = connectSql.RunSqlReturnDR(straccount)
            'While dr.Read()
            ds = connectSql.RunSQLReturnDS(straccount)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow = ds.Tables(0).Rows(0)
                txtdesc.Text = dr(0).ToString()
                fndstructurecode.txtValue.Text = dr(1).ToString()
                fndstructurecode.Enabled = False
                fndstructurecode.txtValue.BackColor = Color.White
                txtstrdesc.Text = dr(2).ToString()
                txtstrdesc.ReadOnly = True
                ddlnormalbal.Text = dr(3).ToString()
                ddlaccounttype.Text = dr(4).ToString().Trim()
                If ddlaccounttype.Text = "Income Statement" Then
                    RadGroupBox1.Visible = True
                End If
                fndaccountgroup.txtValue.Text = dr(5).ToString()
                acctdesc.Text = dr(6).ToString()
                Dim strstatus As String = dr(7).ToString().Trim()
                If strstatus = "Y" Then
                    rdactive.IsChecked = True
                Else
                    rdinactive.IsChecked = True
                End If
                Dim strcontrolaccount As String = dr(8).ToString().Trim()
                If strcontrolaccount = "Y" Then
                    chkcontrolaccount.Checked = True
                    chkcontrolaccount.Enabled = False
                    funfillcontrolaccount()

                Else
                    dgvsubledger.DataSource = Nothing
                    chkcontrolaccount.Checked = False
                End If
                Dim strautoallocation As String = dr(9).ToString()
                If strautoallocation = "Y" Then
                    chkautoallocation.Checked = True
                    dgvallocation.AutoGenerateColumns = False
                    Dim strsourcecode As String = "select Source_Code from TSPL_GL_ALLOCATIONS where Main_Account_Code ='" + fndaccount.txtValue.Text + "'"
                    Dim strallocationgrid As String = "select  account_code, account_desc, Reference , description, percentage from TSPL_GL_ALLOCATIONS where Main_Account_Code ='" + fndaccount.txtValue.Text + "'"
                    fndsourcecode.txtValue.Text = clsDBFuncationality.getSingleValue(strsourcecode)
                    'While row.Read()
                    '    = row(0).ToString()
                    'End While
                    'row.Close()
                    transportSql.FillGridView(strallocationgrid, dgvallocation)
                    dgvallocation.Columns(0).FieldName = "account_code"
                    dgvallocation.Columns(1).FieldName = "account_desc"
                    dgvallocation.Columns(2).FieldName = "Reference"
                    dgvallocation.Columns(3).FieldName = "description"
                    dgvallocation.Columns(4).FieldName = "percentage"
                    chkautoallocation.Enabled = False
                Else
                    chkautoallocation.Checked = False
                End If
                Dim strrollup As String = dr(10).ToString().Trim()
                If strrollup = "Y" Then
                    chkrollup.Checked = True
                    dgvrollup.AutoGenerateColumns = False
                    Dim str As String = "select account, desct, rollup, accountgroup, accounttype, (case status when 'A' then 'Active' else 'Inactive' end) as [Status], account_balance ,multicurrency from TSPL_GL_ROLLUP where Account_Code ='" + fndaccount.txtValue.Text + "' "
                    transportSql.FillGridView(str, dgvrollup)
                    dgvrollup.Columns(0).FieldName = "account"
                    dgvrollup.Columns(1).FieldName = "desct"
                    dgvrollup.Columns(2).FieldName = "rollup"
                    dgvrollup.Columns(3).FieldName = "accountgroup"
                    dgvrollup.Columns(4).FieldName = "accounttype"
                    dgvrollup.Columns(5).FieldName = "Status"
                    dgvrollup.Columns(6).FieldName = "account_balance"
                    dgvrollup.Columns(7).FieldName = "multicurrency"
                    funfilltreeview()
                    '   chkrollup.Enabled = False
                Else
                    chkrollup.Checked = False
                End If
                Dim strmulticurrency As String = dr(11).ToString().Trim()
                If strmulticurrency = "Y" Then
                    chkmulticurrency.Checked = True
                Else
                    chkmulticurrency.Checked = False
                End If
                ddlclosetosegment.Text = dr(12).ToString()
                txtclosetoaccount.Text = dr(13).ToString()
            End If
            dgvsegment.DataSource = Nothing
            Dim strsegname As String = "select seg_name from TSPL_GL_SEGMENT where seg_no <> 1"
            transportSql.FillGridView(strsegname, dgvsegment)
            dgvsegment.Columns(0).FieldName = "seg_name"
            'dgvsegment.Columns(1).ReadOnly = True
            For k As Integer = 0 To dgvsegment.Rows.Count - 1
                If dgvsegment.Rows(k).Cells(1).ReadOnly = False Then
                    dgvsegment.Rows(k).Cells(1).ReadOnly = True
                End If
                If Not String.IsNullOrEmpty(dgvsegment.Rows(k).Cells(1).Value) Then
                    String.IsNullOrEmpty(dgvsegment.Rows(k).Cells(1).Value)
                End If
            Next
            Dim strsegcode As String = "select Account_Seg_Code2, Account_Seg_Code3, Account_Seg_Code4 , Account_Seg_Code5 , Account_Seg_Code6 , Account_Seg_Code7 , Account_Seg_Code8 , Account_Seg_Code9 , Account_Seg_Code10  from TSPL_GL_ACCOUNTS  where Account_Code = '" + fndaccount.txtValue.Text + "'"
            dr = clsDBFuncationality.GetDataTable(strsegcode)
            For Each row As DataRow In dr.Rows
                For i As Integer = 0 To 8
                    If Not String.IsNullOrEmpty(row(i).ToString()) Then
                        dgvsegment.Rows(i).Cells(2).Value = connectSql.RunScalar("select description from tspl_gl_segment_code where segment_code = '" + row(i).ToString() + "' ")

                        dgvsegment.Rows(i).Cells(1).Value = row(i).ToString()

                    End If
                Next
            Next
            btnsave.Text = "&Update"
            btndelete.Enabled = True

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    ''To Retrieve the value of control account tabe
    Private Sub funfillcontrolaccount()

        Try
            dgvsubledger.AutoGenerateColumns = False
            dgvsubledger.DataSource = Nothing
            Dim strsourcecode As String = "select source_code from TSPL_GL_CONTROL_ACC where Account_Code ='" + fndaccount.txtValue.Text + "'"

            transportSql.FillGridView(strsourcecode, dgvsubledger)
            dgvsubledger.Columns(0).FieldName = "source_code"
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fndaccountgroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndaccountgroup.Load
        fndaccountgroup.ConnectionString = connectSql.SqlCon()
        fndaccountgroup.Query = "select Account_group_code as [Account Group Code], Account_group_desc as [Description] from dbo.TSPL_ACCOUNT_GROUPS"
        fndaccountgroup.ValueToSelect = "Account Group Code"
        fndaccountgroup.ValueToSelect1 = "Description"
        fndaccountgroup.Caption = " Account Details"
    End Sub
    Private Sub Frmglaccount1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        btntooltip.SetToolTip(btnsave, "Press Alt+S for save/update the data")
        btntooltip.SetToolTip(btndelete, "Press Alt+D for Delete the data")
        btntooltip.SetToolTip(btnreset, "Press Alt+N for new Transaction")
        btntooltip.SetToolTip(btnclose, "Press Esc for close the screen")
        funbindsubledger()
        RadPageView1.Controls.Remove(RadPageViewPage2)
        RadPageView1.Controls.Remove(RadPageViewPage3)
        RadPageView1.Controls.Remove(RadPageViewPage4)
        fndaccountgroup.txtValue.MaxLength = 10
        fndstructurecode.txtValue.MaxLength = 10
        fndaccount.txtValue.MaxLength = 45
        rdactive.IsChecked = True
        btndelete.Enabled = False
        AddHandler fndaccount.txtValue.TextChanged, AddressOf account_textchanged
        AddHandler fndstructurecode.txtValue.TextChanged, AddressOf structure_changed
        AddHandler fndaccountgroup.txtValue.TextChanged, AddressOf group_changed
        AddHandler fndfromacct.txtValue.TextChanged, AddressOf fromacct_changed
        AddHandler fndtoacct.txtValue.TextChanged, AddressOf toacct_changed
        dgvsegment.AutoGenerateColumns = False
        Dim strsegname As String = "select seg_name from TSPL_GL_SEGMENT where seg_no <> 1"
        transportSql.FillGridView(strsegname, dgvsegment)
        dgvsegment.Columns(0).FieldName = "seg_name"
        dgvsegment.AllowAddNewRow = False
        For i As Integer = 0 To dgvsegment.Rows.Count - 1
            dgvsegment.Rows(i).Cells(0).ReadOnly = True
            dgvsegment.Rows(i).Cells(1).ReadOnly = True
        Next
        Dim strlength As String = "select seg_length from tspl_gl_segment where seg_no = 1"
        'dr = connectSql.RunSqlReturnDR(strlength)
        Dim lenth As Integer = connectSql.RunScalar(strlength)
        'While dr.Read()
        '    lenth = dr(0)
        'End While
        fndaccount.txtValue.MaxLength = lenth
        fndaccountgroup.txtValue.ReadOnly = True
        fndstructurecode.txtValue.ReadOnly = True
        Dim straccount As String = "SELECT ACCOUNT_CODE AS [Account Code], DESCRIPTION AS [Description] from tspl_gl_accounts where Account_Code not in('" + fndaccount.txtValue.Text.Trim() + "')"
        ds = connectSql.RunSQLReturnDS(straccount)
        Dim dtaccount As GridViewComboBoxColumn = TryCast(dgvallocation.Columns(0), GridViewComboBoxColumn)
        dtaccount.DataSource = ds.Tables(0)
        dtaccount.ValueMember = "Account_Code"
        dtaccount.DisplayMember = "Account_Code"
        'Dim dtaccount1 As GridViewMultiComboBoxColumn = TryCast(dgvrollup.Columns(0), GridViewMultiComboBoxColumn)
        'dtaccount1.DataSource = ds.Tables(0)
        'dtaccount1.ValueMember = "Account Code"
        fndstructurecode.txtValue.ReadOnly = True
        fndaccountgroup.txtValue.ReadOnly = True
        funcheckstructurecode()
        fndtoacct.Enabled = False
      
    End Sub
    ''To select the from gl account
    Private Sub fromacct_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If String.IsNullOrEmpty(fndfromacct.txtValue.Text) And String.IsNullOrEmpty(fndtoacct.txtValue.Text) Then
        Else
            fndtoacct.Enabled = True
            dgvrollup.DataSource = Nothing
            dgvrollup.Rows.Clear()
            dgvrollup.AutoGenerateColumns = False
            Dim strquery As String = "select Account_Code, Description, (case rollup when 'Y' THEN 'YES' ELSE 'NO' END ) AS [Roll Up], Account_Group_Code , Account_Type , (case Status  when 'Y' THEN 'YES' ELSE 'NO' END ) as [Status] , Account_Balance , (case multicurrency  when 'Y' THEN 'YES' ELSE 'NO' END ) as [Multicurrency] from TSPL_GL_ACCOUNTS where Account_Code  between  '" + fndfromacct.txtValue.Text + "'and '" + fndtoacct.txtValue.Text + "' and Account_Code <> '" + fndaccount.txtValue.Text + "'"
            transportSql.FillGridView(strquery, dgvrollup)
            dgvrollup.Columns(0).FieldName = "Account_Code"
            dgvrollup.Columns(1).FieldName = "Description"
            dgvrollup.Columns(2).FieldName = "Roll Up"
            dgvrollup.Columns(3).FieldName = "Account_Group_Code"
            dgvrollup.Columns(4).FieldName = "Account_Type"
            dgvrollup.Columns(5).FieldName = "Status"
            dgvrollup.Columns(6).FieldName = "Account_Balance"
            dgvrollup.Columns(7).FieldName = "Multicurrency"
        End If
    End Sub
    ''To select the to gl account
    Private Sub toacct_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If String.IsNullOrEmpty(fndfromacct.txtValue.Text) And String.IsNullOrEmpty(fndtoacct.txtValue.Text) Then
        Else
            dgvrollup.DataSource = Nothing
            dgvrollup.Rows.Clear()
            dgvrollup.AutoGenerateColumns = False
            Dim strquery As String = "select Account_Code, Description, (case rollup when 'Y' THEN 'YES' ELSE 'NO' END ) AS [Roll Up], Account_Group_Code , Account_Type , (case Status  when 'Y' THEN 'YES' ELSE 'NO' END ) as [Status] , Account_Balance , (case multicurrency  when 'Y' THEN 'YES' ELSE 'NO' END ) as [Multicurrency] from TSPL_GL_ACCOUNTS where Account_Code  between  '" + fndfromacct.txtValue.Text + "'and '" + fndtoacct.txtValue.Text + "' and Account_Code <> '" + fndaccount.txtValue.Text + "'"
            transportSql.FillGridView(strquery, dgvrollup)
            dgvrollup.Columns(0).FieldName = "Account_Code"
            dgvrollup.Columns(1).FieldName = "Description"
            dgvrollup.Columns(2).FieldName = "Roll Up"
            dgvrollup.Columns(3).FieldName = "Account_Group_Code"
            dgvrollup.Columns(4).FieldName = "Account_Type"
            dgvrollup.Columns(5).FieldName = "Status"
            dgvrollup.Columns(6).FieldName = "Account_Balance"
            dgvrollup.Columns(7).FieldName = "Multicurrency"
        End If

    End Sub
    ''To bind subledger grid(show the value of source ledger)
    Private Sub funbindsubledger()
        Dim strsourcledger As String = "select SourceCode as [Source Code], SourceLedger as [Source Ledger], SourceType as [Source Type], SourceDescription as [Description] from TSPL_GL_SOURCECODE"
        ds = connectSql.RunSQLReturnDS(strsourcledger)
        Dim grdmcsourcecode As GridViewMultiComboBoxColumn = TryCast(dgvsubledger.Columns(0), GridViewMultiComboBoxColumn)
        grdmcsourcecode.DataSource = ds.Tables(0)
        grdmcsourcecode.ValueMember = "Source Code"
        grdmcsourcecode.DisplayMember = "Source Code"
    End Sub
    ''To Authorised the user 
    Private Function funSetUserAccess() As Boolean
        Try
            'If funCheckLoginStatus() = False Then Exit Function
            Dim strRights As String
            Dim strTemp() As String
            Dim strProgCode = "GL-ACCT"
            strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
            strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
            strTemp = Split(strRights, ",")
            If strTemp(0) = "0" Then
                MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
                funSetUserAccess = False
                blnRead = False
                Me.Close()
                Exit Function
            Else
                blnRead = True
            End If
            If strTemp(1) = "0" Then 'Grant modify access
                'rdbtnSave.Enabled = False
            End If
            If strTemp(2) = "0" Then 'Grant modify access
                'rdbtndelete.Enabled = False
            End If

            funSetUserAccess = True
        Catch er As Exception

        End Try
    End Function

    '' To check the default structure code is available or not 
    Private Sub funcheckstructurecode()
        Try
            Dim strcode As String = connectSql.RunScalar("select structure_code from tspl_glsetting")
            If Not String.IsNullOrEmpty(strcode) Then
                fndstructurecode.txtValue.Text = strcode
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    ''To call the funfill function to fill all the record according to the account 
    Private Sub account_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If fndaccount.txtValue.Text <> "" Then
        '    Dim straccount2 As String = "SELECT ACCOUNT_CODE AS [Account Code], DESCRIPTION AS [Description] from tspl_gl_accounts where Account_Code not in('" + fndaccount.txtValue.Text.Trim() + "')"
        '    ds = connectSql.RunSQLReturnDS(straccount2)
        '    Dim dtaccount As GridViewComboBoxColumn = TryCast(dgvallocation.Columns(0), GridViewComboBoxColumn)
        '    dtaccount.DataSource = ds.Tables(0)
        '    dtaccount.ValueMember = "Account Code"
        'Else
        '    Dim straccount1 As String = "SELECT ACCOUNT_CODE AS [Account Code], DESCRIPTION AS [Description] from tspl_gl_accounts where Account_Code"
        '    ds = connectSql.RunSQLReturnDS(straccount1)
        '    Dim dtaccount As GridViewComboBoxColumn = TryCast(dgvallocation.Columns(0), GridViewComboBoxColumn)
        '    dtaccount.DataSource = ds.Tables(0)
        '    dtaccount.ValueMember = "Account Code"

        'End If
        Dim str As String = "select account_code from TSPL_GL_ACCOUNTS where account_code = '" + fndaccount.txtValue.Text + "'"
        Dim straccount As String = clsDBFuncationality.getSingleValue(str)

        'While dr.Read()
        '    straccount = dr(0)
        'End While
        If straccount <> "" Then
            funfill()
        Else
            fndaccountgroup.txtValue.Text = ""
            '  fndstructurecode.txtValue.Text = ""
            ddlnormalbal.Text = "select"
            ddlaccounttype.Text = "select"
            txtdesc.Text = ""
            ' txtstrdesc.Text = ""
            acctdesc.Text = ""
            rdactive.IsChecked = True
            chkautoallocation.Checked = False
            chkcontrolaccount.Checked = False
            chkmulticurrency.Checked = False
            chkrollup.Checked = False
            btnsave.Text = "&Save"
            btndelete.Enabled = False
        End If
    End Sub
    ''To text change the structure finder
    Private Sub structure_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        fndstructurecode.txtValue.Tag = txtstrdesc.Text
        Dim strsegmentname As String = "select seg_name2, seg_name3, seg_name4, seg_name5, seg_name6, seg_name7, seg_name8, seg_name9, seg_name10 from tspl_gl_structure where str_code = '" + fndstructurecode.txtValue.Text + "'"
        dr = clsDBFuncationality.GetDataTable(strsegmentname)
        Dim STRSEG1 As String
        Dim j As Integer = dgvsegment.RowCount
        For k As Integer = 0 To dgvsegment.Rows.Count - 1
            If dgvsegment.Rows(k).Cells(1).ReadOnly = False Then
                dgvsegment.Rows(k).Cells(1).ReadOnly = True
            End If
            If Not String.IsNullOrEmpty(dgvsegment.Rows(k).Cells(1).Value) Then
                String.IsNullOrEmpty(dgvsegment.Rows(k).Cells(1).Value)
            End If
        Next
        For Each row As DataRow In dr.Rows
            For i As Integer = 0 To 8
                STRSEG1 = row(i).ToString()
                If STRSEG1 = String.Empty Or STRSEG1 = "ACCOUNT" Then
                    ' Exit For
                Else
                    For h As Integer = 0 To j - 1
                        If dgvsegment.Rows(h).Cells(0).Value = STRSEG1 Then
                            'dgvsegment.Rows(h).Cells(0).ReadOnly = False
                            dgvsegment.Rows(h).Cells(1).ReadOnly = False
                        End If
                    Next
                End If
            Next
        Next

        Dim strdesc As String = "select Str_Description  from TSPL_GL_STRUCTURE  where Str_Code = '" + fndstructurecode.txtValue.Text.Trim() + "'"
        txtstrdesc.Text = connectSql.RunScalar(strdesc)

    End Sub
    Private Sub group_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndaccountgroup.txtValue.Tag = acctdesc.Text
        Dim straccdesc As String = "select Account_Group_Desc  from TSPL_ACCOUNT_GROUPS  where Account_Group_Code = '" + fndaccountgroup.txtValue.Text.Trim() + "'"
        acctdesc.Text = connectSql.RunScalar(straccdesc)
    End Sub
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        funreset()
    End Sub
    ''To call The delete function to delete account id
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        deletedata()
    End Sub
    Public Sub deletedata()
        Dim str As String = "select account_code from TSPL_GL_ACCOUNTS where account_code = '" + fndaccount.txtValue.Text + "'"
        Dim straccount As String = clsDBFuncationality.getSingleValue(str)


        'If dr.HasRows Then
        '    While dr.Read()
        '        straccount = dr(0)
        '    End While
        'End If
        If fndaccount.txtValue.Text = "" Then
            common.clsCommon.MyMessageBoxShow("Please Enter the Account Id")
            fndaccount.txtValue.Focus()
        Else
            Dim Reason As String = ""
            If straccount <> "" Then
                If myMessages.deleteConfirm() Then
                    If clsCancelLog.CheckForReasonOnDelete() Then
                        '' REASON FOR DELETE 
                        Dim frm As New FrmFreeTxtBox1
                        frm.Text = "Remarks for Delete"
                        frm.ShowDialog()
                        If clsCommon.myLen(frm.strRmks) <= 0 Then
                            Exit Sub
                        Else
                            Reason = frm.strRmks
                        End If
                    End If
                    fundelete(Reason)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("This Account doesn't exist")
        End If
        End If
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = "GL Account"
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.fndaccount.txtValue.Text)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function
    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        closeform()
    End Sub
    Public Sub closeform()
        Me.Close()
    End Sub
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        savedata()
    End Sub
    Public Sub savedata()
        'Dim jlength As Integer = connectSql.RunScalar("select total_length from TSPL_STRUCTURE_MASTER where Structure_Code ='" + fndstructurecode.txtValue.Text + "'")
        Dim str As String = funaccountcodelength()

        Dim length As Integer = str.Length
        Dim length1 As Integer = funsegmentcodevalidation()
        If fndaccount.txtValue.Text = "" Then
            common.clsCommon.MyMessageBoxShow("Please Enter the Account id")
            fndaccount.txtValue.Focus()
        ElseIf fndstructurecode.txtValue.Text = "" Then
            common.clsCommon.MyMessageBoxShow("Please Enter the Structure Code")
            fndstructurecode.txtValue.Focus()
        ElseIf fndaccountgroup.txtValue.Text = "" Then
            common.clsCommon.MyMessageBoxShow("Please Enter the Account Group Code")
            fndaccountgroup.txtValue.Focus()
        ElseIf ddlnormalbal.Text = "select" Then
            common.clsCommon.MyMessageBoxShow("Please select the Account Balance")
            ddlnormalbal.Focus()
        ElseIf ddlaccounttype.Text = "select" Then
            common.clsCommon.MyMessageBoxShow("Please select the account type")
            ddlaccounttype.Focus()
        Else
            If btnsave.Text = "&Save" Then
                If chkautoallocation.Checked = False And chkrollup.Checked = False And chkcontrolaccount.Checked = False Then
                    If length = length1 Then
                        funinsert()
                        funfill()
                    Else
                        common.clsCommon.MyMessageBoxShow("Please select all the segment code")
                    End If

                ElseIf chkrollup.Checked = True And chkautoallocation.Checked = True And chkcontrolaccount.Checked = False Then

                    If length = length1 Then
                        funinsertrollup()
                        funinsertallocation()
                        funinsert()
                    Else
                        common.clsCommon.MyMessageBoxShow("Please select all the segment code")
                    End If
                ElseIf chkcontrolaccount.Checked = True And chkautoallocation.Checked And chkrollup.Checked = False Then
                    Dim total As Decimal
                    Dim introwcount As Integer = dgvallocation.Rows.Count
                    Dim introwcount1 As Integer = dgvsubledger.Rows.Count


                    For Each grow As GridViewRowInfo In dgvallocation.Rows
                        total = total + grow.Cells(4).Value
                    Next
                    If total <> 100 Then
                        common.clsCommon.MyMessageBoxShow("Percentage should be 100")
                    ElseIf fndsourcecode.txtValue.Text = "" Then
                        common.clsCommon.MyMessageBoxShow("Please select the source Code")
                    ElseIf introwcount = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select at least one account")
                    ElseIf introwcount1 = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select at least one source code")
                    Else
                        If length = length1 Then
                            funinsertallocation()
                            funinsertcontrolaccount()
                            funinsert()
                            funfill()
                        Else
                            common.clsCommon.MyMessageBoxShow("Please select all the segment code")
                        End If

                    End If
                ElseIf chkcontrolaccount.Checked = True And chkrollup.Checked = True And chkautoallocation.Checked = False Then
                    Dim introwcount1 As Integer = dgvsubledger.Rows.Count
                    Dim introwcountrollup As Integer = dgvrollup.Rows.Count
                    If length <> length1 Then
                        common.clsCommon.MyMessageBoxShow("Please select all the segment code")
                    ElseIf introwcount1 = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select at least one source code")
                    ElseIf introwcountrollup = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select at least one account")
                    Else
                        funinsertrollup()
                        funinsertcontrolaccount()
                        funinsert()
                        funfill()
                    End If
                ElseIf chkautoallocation.Checked = True And chkrollup.Checked = True And chkcontrolaccount.Checked = True Then
                    Dim total As Decimal
                    Dim introwcount As Integer = dgvallocation.Rows.Count
                    Dim introwcount1 As Integer = dgvsubledger.Rows.Count

                    For Each grow As GridViewRowInfo In dgvallocation.Rows
                        total = total + grow.Cells(4).Value
                    Next
                    If total <> 100 Then
                        common.clsCommon.MyMessageBoxShow("Percentage should be 100")
                    ElseIf fndsourcecode.txtValue.Text = "" Then
                        common.clsCommon.MyMessageBoxShow("Please select the source Code")
                    ElseIf introwcount = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select at least one account")
                    ElseIf introwcount1 = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select at least one Source Code")

                    Else
                        If length = length1 Then
                            funinsertallocation()
                            funinsertrollup()
                            funinsertcontrolaccount()
                            funinsert()
                            funfill()
                        Else
                            common.clsCommon.MyMessageBoxShow("Please select all the segment code")
                        End If
                    End If

                ElseIf chkautoallocation.Checked = True Then
                    Dim total As Decimal
                    Dim introwcount As Integer = dgvallocation.Rows.Count

                    For Each grow As GridViewRowInfo In dgvallocation.Rows
                        total = total + grow.Cells(4).Value
                    Next
                    If fndsourcecode.txtValue.Text = "" Then
                        common.clsCommon.MyMessageBoxShow("Please select the source Code")
                        fndsourcecode.txtValue.Focus()
                    ElseIf introwcount = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select at least one account")
                    ElseIf total <> 100 Then
                        common.clsCommon.MyMessageBoxShow("Percentage should be 100")

                    Else
                        If length = length1 Then
                            funinsertallocation()
                            funinsert()
                            funfill()
                        Else
                            common.clsCommon.MyMessageBoxShow("Please select all the segment code")
                        End If

                    End If
                ElseIf chkrollup.Checked = True Then
                    Dim introwcountrollup As Integer = dgvrollup.Rows.Count
                    If length <> length1 Then
                        common.clsCommon.MyMessageBoxShow("Please select all the segment code")
                    ElseIf introwcountrollup = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select at least one account")
                    Else
                        funinsertrollup()
                        funinsert()
                        funfill()
                    End If
                ElseIf chkcontrolaccount.Checked = True Then
                    Dim introwcount As Integer = dgvsubledger.Rows.Count
                    If length <> length1 Then
                        common.clsCommon.MyMessageBoxShow("Please select all the segment code")
                    ElseIf introwcount = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select at least one source code")
                    Else
                        If Not String.IsNullOrEmpty(dgvsubledger.Rows(0).Cells(0).Value.ToString()) Then
                            funinsertcontrolaccount()
                            funinsert()
                            funfill()
                        Else
                            common.clsCommon.MyMessageBoxShow("Please Select at least one GL source code")
                        End If
                    End If
                End If
            Else : btnsave.Text = "&Update"
                '    If chkautoallocation.Checked = False And chkrollup.Checked = False Then
                '        funupdate()
                '    ElseIf chkrollup.Checked = True And chkautoallocation.Checked = True Then
                '        fundeleteallocation()
                '        fundeleterollup()
                '        funinsertrollup()
                '        funinsertallocation()
                '        funupdate()
                '        funfill()
                '    ElseIf chkautoallocation.Checked = True Then
                '        Dim total As Decimal
                '        For Each grow As GridViewRowInfo In dgvallocation.Rows
                '            If Not String.IsNullOrEmpty(grow.Cells(4).Value.ToString()) Then
                '                total = total + grow.Cells(4).Value

                '            End If
                '        Next
                '        If total <> 100 Then
                '            common.clsCommon.MyMessageBoxShow("Percentage should be 100")
                '        ElseIf fndsourcecode.txtValue.Text = "" Then
                '            common.clsCommon.MyMessageBoxShow("Please select the source Code")
                '        Else
                '            fundeleteallocation()
                '            funinsertallocation()
                '            funupdate()
                '            funfill()
                '        End If
                '    ElseIf chkrollup.Checked = True Then
                '        fundeleterollup()
                '        funinsertrollup()
                '        funupdate()
                '        funfill()
                '    End If
                'End If
                If chkautoallocation.Checked = False And chkrollup.Checked = False And chkcontrolaccount.Checked = False Then
                    If length = length1 Then
                        funupdate()
                        funfill()
                    Else
                        common.clsCommon.MyMessageBoxShow("Please select all the segment code")
                    End If

                ElseIf chkrollup.Checked = True And chkautoallocation.Checked = True And chkcontrolaccount.Checked = False Then

                    If length = length1 Then
                        fundeleterollup()
                        funinsertrollup()
                        fundeleteallocation()
                        funinsertallocation()
                        funupdate()
                    Else
                        common.clsCommon.MyMessageBoxShow("Please select all the segment code")
                    End If
                ElseIf chkcontrolaccount.Checked = True And chkautoallocation.Checked And chkrollup.Checked = False Then
                    Dim total As Decimal
                    Dim introwcount As Integer = dgvallocation.Rows.Count
                    Dim introwcount1 As Integer = dgvsubledger.Rows.Count


                    For Each grow As GridViewRowInfo In dgvallocation.Rows
                        total = total + grow.Cells(4).Value
                    Next
                    If total <> 100 Then
                        common.clsCommon.MyMessageBoxShow("Percentage should be 100")
                    ElseIf fndsourcecode.txtValue.Text = "" Then
                        common.clsCommon.MyMessageBoxShow("Please select the source Code")
                    ElseIf introwcount = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select at least one account")
                    ElseIf introwcount1 = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select at least one source code")
                    Else
                        If length = length1 Then
                            fundeleteallocation()
                            funinsertallocation()
                            fundeletecontrolaccount()
                            funinsertcontrolaccount()
                            funupdate()
                            funfill()
                        Else
                            common.clsCommon.MyMessageBoxShow("Please select all the segment code")
                        End If

                    End If
                ElseIf chkcontrolaccount.Checked = True And chkrollup.Checked = True And chkautoallocation.Checked = False Then
                    Dim introwcount1 As Integer = dgvsubledger.Rows.Count
                    Dim introwcountrollup As Integer = dgvrollup.Rows.Count
                    If length <> length1 Then
                        common.clsCommon.MyMessageBoxShow("Please select all the segment code")
                    ElseIf introwcount1 = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select at least one source code")
                    ElseIf introwcountrollup = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select at least one account")
                    Else
                        fundeleterollup()
                        funinsertrollup()
                        fundeletecontrolaccount()
                        funinsertcontrolaccount()
                        funupdate()
                        funfill()
                    End If
                ElseIf chkautoallocation.Checked = True And chkrollup.Checked = True And chkcontrolaccount.Checked = True Then
                    Dim total As Decimal
                    Dim introwcount As Integer = dgvallocation.Rows.Count
                    Dim introwcount1 As Integer = dgvsubledger.Rows.Count

                    For Each grow As GridViewRowInfo In dgvallocation.Rows
                        total = total + grow.Cells(4).Value
                    Next
                    If total <> 100 Then
                        common.clsCommon.MyMessageBoxShow("Percentage should be 100")
                    ElseIf fndsourcecode.txtValue.Text = "" Then
                        common.clsCommon.MyMessageBoxShow("Please select the source Code")
                    ElseIf introwcount = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select at least one account")
                    ElseIf introwcount1 = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select at least one Source Code")

                    Else
                        If length = length1 Then
                            fundeleteallocation()
                            funinsertallocation()
                            fundeleterollup()
                            funinsertrollup()
                            fundeletecontrolaccount()
                            funinsertcontrolaccount()
                            funupdate()
                            funfill()
                        Else
                            common.clsCommon.MyMessageBoxShow("Please select all the segment code")
                        End If
                    End If

                ElseIf chkautoallocation.Checked = True Then
                    Dim total As Decimal
                    Dim introwcount As Integer = dgvallocation.Rows.Count

                    For Each grow As GridViewRowInfo In dgvallocation.Rows
                        total = total + grow.Cells(4).Value
                    Next
                    If fndsourcecode.txtValue.Text = "" Then
                        common.clsCommon.MyMessageBoxShow("Please select the source Code")
                        fndsourcecode.txtValue.Focus()
                    ElseIf introwcount = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select at least one account")
                    ElseIf total <> 100 Then
                        common.clsCommon.MyMessageBoxShow("Percentage should be 100")

                    Else
                        If length = length1 Then
                            fundeleteallocation()
                            funinsertallocation()
                            funupdate()
                            funfill()
                        Else
                            common.clsCommon.MyMessageBoxShow("Please select all the segment code")
                        End If

                    End If
                ElseIf chkrollup.Checked = True Then
                    Dim introwcountrollup As Integer = dgvrollup.Rows.Count
                    If length <> length1 Then
                        common.clsCommon.MyMessageBoxShow("Please select all the segment code")
                    ElseIf introwcountrollup = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select at least one account")
                    Else
                        fundeleterollup()
                        funinsertrollup()
                        funupdate()
                        funfill()
                    End If
                ElseIf chkcontrolaccount.Checked = True Then
                    Dim introwcount As Integer = dgvsubledger.Rows.Count
                    If length <> length1 Then
                        common.clsCommon.MyMessageBoxShow("Please select all the segment code")
                    ElseIf introwcount = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select at least one source code")
                    Else
                        If Not String.IsNullOrEmpty(dgvsubledger.Rows(0).Cells(0).Value.ToString()) Then
                            fundeletecontrolaccount()
                            funinsertcontrolaccount()
                            funupdate()
                            funfill()
                        Else
                            common.clsCommon.MyMessageBoxShow("Please Select at least one GL source code")
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub fndaccount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndaccount.Load
        fndaccount.ConnectionString = connectSql.SqlCon()
        fndaccount.Query = "select Account_Code as [Account Code], Description as [Description] from tspl_gl_accounts"
        fndaccount.ValueToSelect = "Account Code"
        fndaccount.Caption = "Account Details"
        fndaccount.ValueToSelect1 = "Description"
    End Sub
    ''To export the data into the excel sheet
    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click

        Dim str As String = "select Account_Code as [Account Code], Description as [Description], Str_Code as [Structure Code], Str_Description as [Structure Description], Account_Balance as [Account Balance], Account_Type as [Account Type], Account_Group_Code as [Account Group Code], Account_Group_Desc as [Account Group Code Description], Status as [Status], ControlAccount as [Control Account], autoallocation as [Auto Allocation], Rollup as [Rollup], multicurrency as [Multicurrency], account_seg_code1 as [Segment Code 1], account_seg_desc1 as [Segment Description 1], account_seg_code2 as [Segment Code 2], account_seg_desc2 as [Segment Description 2],Account_Seg_Code3 as [Segment Code 3],Account_Seg_Desc3 as [Segment Description 3],Account_Seg_Code4 as [Segment Code 4],Account_Seg_Desc4 as [Segment Description 4],Account_Seg_Code5 as [Segment Code 5],Account_Seg_Desc5 as [Segment Description 5] ,Account_Seg_Code6 as [Segment Code 6],Account_Seg_Desc6 as [Segment Description 6],Account_Seg_Code7 as [Segment Code 7],Account_Seg_Desc7 as [Segment Description 7],Account_Seg_Code8  as [Segment Code 8],Account_Seg_Desc8 as [Segment Description 8],Account_Seg_Code9 as [Segment Code 9],Account_Seg_Desc9 as [Segment Description 9],Account_Seg_Code10 as [Segment Code 10],Account_Seg_Desc10 as [Segment Description 10], Close_To_Seg as [Close To Segment], Close_To_Acct as [Close To Account], Created_By as [Created By], Created_Date as [Created Date], Modify_By as [Modify By], Modify_Date as [Modify Date], Comp_Code as [Company Code]  from TSPL_GL_ACCOUNTS "
        transportSql.ExporttoExcel(str, Me)

    End Sub
    ''tO CALL THE IMPORT 
    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        funimport()
    End Sub
    ''imports function 
    Private Sub funimport()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Account Code", "Description", "Structure Code", "Structure Description", "Account Balance", "Account Type", "Account Group Code", "Account Group Code Description", "Status", "Control Account", "Auto Allocation", "Rollup", "Multicurrency", "Segment Code 1", "Segment Description 1", "Segment Code 2", "Segment Description 2", "Segment Code 3", "Segment Description 3", "Segment Code 4", "Segment Description 4", "Segment Code 5", "Segment Description 5", "Segment Code 6", "Segment Description 6", "Segment Code 7", "Segment Description 7", "Segment Code 8", "Segment Description 8", "Segment Code 9", "Segment Description 9", "Segment Code 10", "Segment Description 10", "Close To Segment", "Close To Account") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim straccountcode As String = grow.Cells(0).Value.ToString().Trim()
                    Dim strdesc As String = grow.Cells(1).Value.ToString().Trim()
                    Dim strstructcode As String = grow.Cells(2).Value.ToString().Trim()
                    Dim strstructdesc As String = grow.Cells(3).Value.ToString().Trim()
                    Dim stracctbalance As String = grow.Cells(4).Value.ToString().Trim()
                    Dim straccttype As String = grow.Cells(5).Value.ToString().Trim()
                    Dim stracctgroupcode As String = grow.Cells(6).Value.ToString().Trim()
                    Dim stracctgroupdesc As String = grow.Cells(7).Value.ToString()
                    Dim strstatus As String = grow.Cells(8).Value.ToString()
                    Dim strcontrolacct As String = grow.Cells(9).Value.ToString()
                    Dim strautoallocation As String = grow.Cells(10).Value.ToString()
                    Dim strrollup As String = grow.Cells(11).Value.ToString()
                    Dim strmulticurrency As String = grow.Cells(12).Value.ToString()
                    Dim strsegcode1 As String = grow.Cells(13).Value.ToString()
                    Dim strsegdesc1 As String = grow.Cells(14).Value.ToString()
                    Dim strsegcode2 As String = grow.Cells(15).Value.ToString()
                    Dim strsegdesc2 As String = grow.Cells(16).Value.ToString()
                    Dim strsegcode3 As String = grow.Cells(17).Value.ToString()
                    Dim strsegdesc3 As String = grow.Cells(18).Value.ToString()
                    Dim strsegcode4 As String = grow.Cells(19).Value.ToString()
                    Dim strsegdesc4 As String = grow.Cells(20).Value.ToString()
                    Dim strsegcode5 As String = grow.Cells(21).Value.ToString()
                    Dim strsegdesc5 As String = grow.Cells(22).Value.ToString()
                    Dim strsegcode6 As String = grow.Cells(23).Value.ToString()
                    Dim strsegdesc6 As String = grow.Cells(24).Value.ToString()
                    Dim strsegcode7 As String = grow.Cells(25).Value.ToString()
                    Dim strsegdesc7 As String = grow.Cells(26).Value.ToString()
                    Dim strsegcode8 As String = grow.Cells(27).Value.ToString()
                    Dim strsegdesc8 As String = grow.Cells(28).Value.ToString()
                    Dim strsegcode9 As String = grow.Cells(29).Value.ToString()
                    Dim strsegdesc9 As String = grow.Cells(30).Value.ToString()
                    Dim strsegcode10 As String = grow.Cells(31).Value.ToString()
                    Dim strsegdesc10 As String = grow.Cells(32).Value.ToString()
                    Dim strclosetoseg As String = grow.Cells(33).Value.ToString()
                    Dim strclosetoacct As String = grow.Cells(34).Value.ToString()


                    If String.IsNullOrEmpty(straccountcode) And straccountcode.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow("Account Code has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(strdesc) And strdesc.Length > 65 Then
                        common.clsCommon.MyMessageBoxShow("Account Description has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(strstructcode) And strstructcode.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Structure Code has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(strstructdesc) And strstructdesc.Length > 60 Then
                        common.clsCommon.MyMessageBoxShow("Structure Description has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(stracctbalance) And stracctbalance.Length > 10 Then
                        common.clsCommon.MyMessageBoxShow("Account Balance has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(straccttype) And straccttype.Length > 20 Then
                        common.clsCommon.MyMessageBoxShow("Account Type has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(stracctgroupcode) And stracctgroupcode.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Account Group Code has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(stracctgroupdesc) And stracctgroupdesc.Length > 100 Then
                        common.clsCommon.MyMessageBoxShow("Account Description has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(strstatus) And strstatus.Length > 1 Then
                        common.clsCommon.MyMessageBoxShow("status has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(strcontrolacct) And strcontrolacct.Length > 1 Then
                        common.clsCommon.MyMessageBoxShow("Control Account has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(strautoallocation) And strautoallocation.Length > 1 Then
                        common.clsCommon.MyMessageBoxShow("Auto Allocation has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(strrollup) And strrollup.Length > 1 Then
                        common.clsCommon.MyMessageBoxShow("Rollup has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(strmulticurrency) And strmulticurrency.Length > 1 Then
                        common.clsCommon.MyMessageBoxShow("Multicurrency has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegcode1.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Segment Code 1 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegdesc1.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow("Segment Description 1 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegcode2.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Segment Code 2 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegdesc2.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow("Segment Description 2 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegcode3.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Segment Code 3 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegdesc3.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow("Segment Description 3 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegcode4.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Segment Code 4 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegdesc4.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow("Segment Description 4 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegcode5.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Segment Code 5 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegdesc5.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow("Segment Description 5 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegcode6.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Segment Code 6 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegdesc6.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow("Segment Description 6 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegcode7.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Segment Code 7 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegdesc7.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow("Segment Description 7 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegcode8.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Segment Code 8 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegdesc8.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow("Segment Description 8 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegcode9.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Segment Code 9 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegdesc9.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow("Segment Description 9 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegcode10.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Segment Code 10 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strsegdesc10.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow("Segment Description 10 has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strclosetoseg.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Close To Segment has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strclosetoacct.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow("Close To Account has some incorrect values")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim sql2 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code = '" + straccountcode + "'"
                    Dim i2 As Integer = CInt(connectSql.RunScalar(sql2))
                    If (i2 = 0) Then
                        connectSql.RunSpTransaction(trans, "SP_TSPL_GL_ACCOUNTS_INSERT", New SqlParameter("@accountcode", straccountcode), New SqlParameter("@description", strdesc), New SqlParameter("@strcode", strstructcode), New SqlParameter("@strdesc", strstructdesc), New SqlParameter("@accbal", stracctbalance), New SqlParameter("@acctype", straccttype), New SqlParameter("@accgroupcode", stracctgroupcode), New SqlParameter("@accgroupdesc", stracctgroupdesc), New SqlParameter("@status", strstatus), New SqlParameter("@controlaccount", strcontrolacct), New SqlParameter("@autoallocation", strautoallocation), New SqlParameter("@rollup", strrollup), New SqlParameter("@multicurrency", strmulticurrency), New SqlParameter("@accsegcode1", strsegcode1), New SqlParameter("@accsegdesc1", strsegdesc1), New SqlParameter("@accsegcode2", strsegcode2), New SqlParameter("@accsegdesc2", strsegdesc2), New SqlParameter("@accsegcode3", strsegcode3), New SqlParameter("@accsegdesc3", strsegdesc3), New SqlParameter("@accsegcode4", strsegcode4), New SqlParameter("@accsegdesc4", strsegdesc4), New SqlParameter("@accsegcode5", strsegcode5), New SqlParameter("@accsegdesc5", strsegdesc5), New SqlParameter("@accsegcode6", strsegcode6), New SqlParameter("@accsegdesc6", strsegdesc6), New SqlParameter("@accsegcode7", strsegcode7), New SqlParameter("@accsegdesc7", strsegdesc7), New SqlParameter("@accsegcode8", strsegcode8), New SqlParameter("@accsegdesc8", strsegdesc8), New SqlParameter("@accsegcode9", strsegcode9), New SqlParameter("@accsegdesc9", strsegdesc9), New SqlParameter("@accsegcode10", strsegcode10), New SqlParameter("@accsegdesc10", strsegdesc10), New SqlParameter("@closetoseg", strclosetoseg), New SqlParameter("@closetoaccount", strclosetoacct), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
                    Else
                        connectSql.RunSpTransaction(trans, "SP_TSPL_GL_ACCOUNTS_UPDATE", New SqlParameter("@accountcode", straccountcode), New SqlParameter("@description", strdesc), New SqlParameter("@strcode", strstructcode), New SqlParameter("@strdesc", strstructdesc), New SqlParameter("@accbal", stracctbalance), New SqlParameter("@acctype", straccttype), New SqlParameter("@accgroupcode", stracctgroupcode), New SqlParameter("@accgroupdesc", stracctgroupdesc), New SqlParameter("@status", strstatus), New SqlParameter("@controlaccount", strcontrolacct), New SqlParameter("@autoallocation", strautoallocation), New SqlParameter("@rollup", strrollup), New SqlParameter("@multicurrency", strmulticurrency), New SqlParameter("@accsegcode1", strsegcode1), New SqlParameter("@accsegdesc1", strsegdesc1), New SqlParameter("@accsegcode2", strsegcode2), New SqlParameter("@accsegdesc2", strsegdesc2), New SqlParameter("@accsegcode3", strsegcode3), New SqlParameter("@accsegdesc3", strsegdesc3), New SqlParameter("@accsegcode4", strsegcode4), New SqlParameter("@accsegdesc4", strsegdesc4), New SqlParameter("@accsegcode5", strsegcode5), New SqlParameter("@accsegdesc5", strsegdesc5), New SqlParameter("@accsegcode6", strsegcode6), New SqlParameter("@accsegdesc6", strsegdesc6), New SqlParameter("@accsegcode7", strsegcode7), New SqlParameter("@accsegdesc7", strsegdesc7), New SqlParameter("@accsegcode8", strsegcode8), New SqlParameter("@accsegdesc8", strsegdesc8), New SqlParameter("@accsegcode9", strsegcode9), New SqlParameter("@accsegdesc9", strsegdesc9), New SqlParameter("@accsegcode10", strsegcode10), New SqlParameter("@accsegdesc10", strsegdesc10), New SqlParameter("@closetoseg", strclosetoseg), New SqlParameter("@closetoaccount", strclosetoacct), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
                    End If
                Next
                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Imported Successfully!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
                trans.Rollback()

            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
    Private Sub fndstructurecode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndstructurecode.Load
        fndstructurecode.ConnectionString = connectSql.SqlCon()
        fndstructurecode.Query = "select str_code as [Structure Code], str_description as [Description] from tspl_gl_structure"
        fndstructurecode.ValueToSelect = "Structure Code"
        fndstructurecode.ValueToSelect1 = "Description"
        fndstructurecode.Caption = "Structure Detail"
    End Sub

    Private Sub dgvsegment_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles dgvsegment.EditorRequired
        Dim strsegmentcode As String = "select segment_code  from tspl_gl_segment_code where segment_name = '" + dgvsegment.CurrentRow.Cells(0).Value + "'"
        Dim dt As GridViewComboBoxColumn = TryCast(dgvsegment.Columns(1), GridViewComboBoxColumn)
        ds = connectSql.RunSQLReturnDS(strsegmentcode)
        dt.DataSource = ds.Tables(0)
        dt.ValueMember = "segment_code"
        ' dgvsegment.CurrentRow.Cells(2).Value = connectSql.RunScalar("select Description   from tspl_gl_segment_code where Segment_code = '" + dgvsegment.CurrentRow.Cells(1).Value + "'")
    End Sub

    Private Sub dgvsegment_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvsegment.Leave
        Dim straccount As String = fndaccount.txtValue.Text
        Dim strfullaccount As String = ""
        Dim i As Integer
        For i = 0 To dgvsegment.Rows.Count - 1
            If Not IsDBNull(dgvsegment.Rows(i).Cells(1).Value) Then
                strfullaccount += "-" + dgvsegment.Rows(i).Cells(1).Value
            End If
        Next
        'fndaccount.txtValue.Text = ""
        'fndaccount.txtValue.Text = straccount + strfullaccount
    End Sub

    Private Sub chkcontrolaccount_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkcontrolaccount.ToggleStateChanged
        If chkcontrolaccount.Checked = True Then
            RadPageView1.Controls.Add(RadPageViewPage3)
        Else : chkcontrolaccount.Checked = False
            RadPageView1.Controls.Remove(RadPageViewPage3)
        End If
    End Sub
    Private Sub chkautoallocation_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkautoallocation.ToggleStateChanged
        If chkautoallocation.Checked = True Then
            RadPageView1.Controls.Add(RadPageViewPage4)
        Else : chkautoallocation.Checked = False
            RadPageView1.Controls.Remove(RadPageViewPage4)
        End If
    End Sub
    Private Sub chkrollup_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkrollup.ToggleStateChanged
        If chkrollup.Checked = True Then
            RadPageView1.Controls.Add(RadPageViewPage2)
        Else : chkrollup.Checked = False
            RadPageView1.Controls.Remove(RadPageViewPage2)
        End If
    End Sub



    Private Sub ddlaccounttype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlaccounttype.SelectedIndexChanged
        Dim arraccount As New ArrayList()
        ' Dim straccount As String
        If ddlaccounttype.Text = "Income Statement" Then
            RadGroupBox1.Visible = True
            For i As Integer = 0 To dgvsegment.Rows.Count - 1
                If Not String.IsNullOrEmpty(dgvsegment.Rows(i).Cells(1).Value) Then
                    Dim Acc As String = clsDBFuncationality.getSingleValue("select distinct Segment_name  from tspl_gl_segment_code where segment_code ='" + dgvsegment.Rows(i).Cells(1).Value + "' and segment_name in (select seg_name from tspl_gl_segment where seg_useinclosing = 'Y')")
                    'While dr.Read()
                    arraccount.Add(Acc)
                    'End While
                End If
            Next
        Else
            RadGroupBox1.Visible = False
        End If
        ddlclosetosegment.DataSource = arraccount
        ddlclosetosegment.Text = ""
        txtclosetoaccount.Text = ""


    End Sub

    Private Sub RadPageView1_SelectedPageChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadPageView1.SelectedPageChanged

    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub Finder3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndsourcecode.Load
        fndsourcecode.ConnectionString = connectSql.SqlCon()
        fndsourcecode.Query = "select sourcecode as [Source Code], sourcedescription as [Description] from TSPL_GL_SOURCECODE where SourceLedger='GL'"
        fndsourcecode.ValueToSelect1 = "Description"
        fndsourcecode.ValueToSelect = "Source Code"
        fndsourcecode.Caption = "Source Detail"
    End Sub

    Private Sub MasterTemplate_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvallocation.CellValueChanged
        If e.Column.Index = 0 Or e.RowIndex >= 0 Then
            Dim str As String = "select description from TSPL_GL_ACCOUNTS where Account_Code = '" + dgvallocation.CurrentRow.Cells(0).Value + "'"
            dgvallocation.CurrentRow.Cells(1).Value = clsDBFuncationality.getSingleValue(str)
            'While dr.Read()
            '   = dr(0).ToString()
            'End While
        End If
    End Sub

    Private Sub dgvrollup_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvrollup.CellValueChanged
        If e.Column.Index = 0 Or e.RowIndex >= 0 Then
            If Not String.IsNullOrEmpty(dgvrollup.CurrentRow.Cells(0).Value) Then
                Dim str As String = "select description, (CASE ROLLUP WHEN 'N' THEN 'NO' ELSE 'YES' END) AS [RollUp], Account_Group_Code, Account_Type ,(CASE Status WHEN 'Y' THEN 'Active' else 'InActive' end ) as [Status],Account_Balance , (case multicurrency when 'Y' THEN 'YES' ELSE 'NO' END) AS [Multicurrency]  from TSPL_GL_ACCOUNTS where Account_Code  = '" + dgvrollup.CurrentRow.Cells(0).Value + "'"
                dr = clsDBFuncationality.GetDataTable(str)
                For Each row As DataRow In dr.Rows
                    dgvrollup.CurrentRow.Cells(1).Value = row(0).ToString()
                    dgvrollup.CurrentRow.Cells(2).Value = row(1).ToString()
                    dgvrollup.CurrentRow.Cells(3).Value = row(2).ToString()
                    dgvrollup.CurrentRow.Cells(4).Value = row(3).ToString()
                    dgvrollup.CurrentRow.Cells(5).Value = row(4).ToString()
                    dgvrollup.CurrentRow.Cells(6).Value = row(5).ToString()
                    dgvrollup.CurrentRow.Cells(7).Value = row(6).ToString()
                Next
            End If
        End If

    End Sub
    Private Sub funfilltreeview()
        RadTreeView1.Nodes.Clear()
        'Try
        '    Dim straccount As String = "select account from TSPL_GL_ROLLUP where Account_Code ='" + fndaccount.txtValue.Text + "'"
        '    ds = connectSql.RunSQLReturnDS(straccount)
        '    Dim node As RadTreeNode
        '    Dim row As DataRow
        '    RadTreeView1.Nodes.Add(fndaccount.txtValue.Text)
        '    For Each row In ds.Tables(0).Rows
        '        node = New RadTreeNode
        '        node.Text = row(0).ToString()
        '        RadTreeView1.Nodes.Add(node)
        '    Next
        'Catch ex As Exception
        '    myMessages.myExceptions(ex.ToString())
        'End Try
        Try
            Dim straccount As String = "select account from TSPL_GL_ROLLUP where Account_Code ='" + fndaccount.txtValue.Text + "'"
            ds = connectSql.RunSQLReturnDS(straccount)
            Dim node As New RadTreeNode()
            node.Text = fndaccount.txtValue.Text
            ' node.BackColor = Color.Blue
            RadTreeView1.Nodes.Add(node)
            Dim row As DataRow
            For Each row In ds.Tables(0).Rows
                Dim node1 As New RadTreeNode()
                node1.Text = row(0).ToString()
                node.Nodes.Add(node1)
            Next
            node.ExpandAll()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub funinsertallocation()
        Dim account As String
        If btnsave.Text = "&Save" Then
            Dim strsegname1 As String = fndaccount.txtValue.Text
            Dim straccount As String = ""
            For m As Integer = 0 To dgvsegment.Rows.Count - 1
                If String.IsNullOrEmpty(dgvsegment.Rows(m).Cells(1).Value) Then
                Else
                    straccount += "-" + dgvsegment.Rows(m).Cells(1).Value
                End If
            Next
            account = strsegname1 + straccount
        Else
            account = fndaccount.txtValue.Text
        End If
        Try
            For i As Integer = 0 To dgvallocation.Rows.Count - 1
                connectSql.RunSp("SP_TSPL_GL_ALLOCATIONS_INSERT", New SqlParameter("@mainaccountcode", account), New SqlParameter("@sourcecode", fndsourcecode.txtValue.Text), New SqlParameter("@account_code", dgvallocation.Rows(i).Cells(0).Value), New SqlParameter("@accountdesc", dgvallocation.Rows(i).Cells(1).Value), New SqlParameter("@reference", dgvallocation.Rows(i).Cells(2).Value), New SqlParameter("@description", dgvallocation.Rows(i).Cells(3).Value), New SqlParameter("@percentage", dgvallocation.Rows(i).Cells(4).Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            Next
            ' myMessages.insert()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub funupdateallocation()
        Try
            For i As Integer = 0 To dgvallocation.Rows.Count - 1
                connectSql.RunSp("SP_TSPL_GL_ALLOCATIONS_UPDATE", New SqlParameter("@mainaccountcode", fndaccount.txtValue.Text), New SqlParameter("@sourcecode", fndsourcecode.txtValue.Text), New SqlParameter("@account_code", dgvallocation.Rows(i).Cells(0).Value), New SqlParameter("@accountdesc", dgvallocation.Rows(i).Cells(1).Value), New SqlParameter("@reference", dgvallocation.Rows(i).Cells(2).Value), New SqlParameter("@description", dgvallocation.Rows(i).Cells(3).Value), New SqlParameter("@percentage", dgvallocation.Rows(i).Cells(4).Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            Next
            ' myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub fundeleteallocation()
        Try
            'For i As Integer = 0 To dgvallocation.Rows.Count - 1
            '    If Not String.IsNullOrEmpty(dgvallocation.Rows(i).Cells(0).Value) Then
            '        connectSql.RunSp("SP_TSPL_GL_ALLOCATIONS_DELETE", New SqlParameter("@mainaccountcode", fndaccount.txtValue.Text), New SqlParameter("@account_code", dgvallocation.Rows(i).Cells(0).Value))
            '    End If
            'Next
            connectSql.RunSp("SP_TSPL_GL_ALLOCATIONS_DELETE", New SqlParameter("@mainaccountcode", fndaccount.txtValue.Text))

            ' myMessages.delete()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub funinsertrollup()
        Try
            Dim account As String
            If btnsave.Text = "&Save" Then
                Dim strsegname1 As String = fndaccount.txtValue.Text
                Dim straccount As String = ""
                For m As Integer = 0 To dgvsegment.Rows.Count - 1
                    If String.IsNullOrEmpty(dgvsegment.Rows(m).Cells(1).Value) Then
                    Else
                        straccount += "-" + dgvsegment.Rows(m).Cells(1).Value
                    End If
                Next
                account = strsegname1 + straccount
            Else
                account = fndaccount.txtValue.Text
            End If

            For i As Integer = 0 To dgvrollup.Rows.Count - 1
                If Not String.IsNullOrEmpty(dgvrollup.Rows(i).Cells(0).Value.ToString()) Then
                    connectSql.RunSp("SP_TSPL_GL_ROLLUP_INSERT", New SqlParameter("@accountcode", account), New SqlParameter("@description", txtdesc.Text), New SqlParameter("@account", dgvrollup.Rows(i).Cells(0).Value.ToString()), New SqlParameter("@desct", dgvrollup.Rows(i).Cells(1).Value.ToString()), New SqlParameter("@rollup", dgvrollup.Rows(i).Cells(2).Value.ToString()), New SqlParameter("@accountgroup", dgvrollup.Rows(i).Cells(3).Value), New SqlParameter("@accounttype", dgvrollup.Rows(i).Cells(4).Value), New SqlParameter("@status", dgvrollup.Rows(i).Cells(5).Value), New SqlParameter("@accountbalance", dgvrollup.Rows(i).Cells(6).Value), New SqlParameter("@multicurrency", dgvrollup.Rows(i).Cells(7).Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@comp_code", companyCode))
                End If
            Next
            ' myMessages.insert()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub funupdaterollup()
        Try
            For i As Integer = 0 To dgvrollup.Rows.Count - 1
                If Not String.IsNullOrEmpty(dgvrollup.Rows(i).Cells(0).Value.ToString()) Then
                    connectSql.RunSp("SP_TSPL_GL_ROLLUP_UPDATE", New SqlParameter("@accountcode", fndaccount.txtValue.Text), New SqlParameter("@description", txtdesc.Text), New SqlParameter("@account", dgvrollup.Rows(i).Cells(0).Value.ToString()), New SqlParameter("@desct", dgvrollup.Rows(i).Cells(1).Value.ToString()), New SqlParameter("@rollup", dgvrollup.Rows(i).Cells(2).Value.ToString()), New SqlParameter("@accountgroup", dgvrollup.Rows(i).Cells(3).Value), New SqlParameter("@accounttype", dgvrollup.Rows(i).Cells(4).Value), New SqlParameter("@status", dgvrollup.Rows(i).Cells(5).Value), New SqlParameter("@accountbalance", dgvrollup.Rows(i).Cells(6).Value), New SqlParameter("@multicurrency", dgvrollup.Rows(i).Cells(7).Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", userCode), New SqlParameter("@comp_code", companyCode))
                End If
            Next
            ' myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub fundeleterollup()
        Try
            'For i As Integer = 0 To dgvrollup.Rows.Count - 1
            '    If Not String.IsNullOrEmpty(dgvrollup.Rows(i).Cells(0).Value.ToString()) Then
            '        connectSql.RunSp("SP_TSPL_GL_ROLLUP_DELETE", New SqlParameter("@accountcode", dgvrollup.Rows(i).Cells(0).Value.ToString()), New SqlParameter("@account", )
            '    End If
            'Next
            connectSql.RunSp("SP_TSPL_GL_ROLLUP_DELETE", New SqlParameter("@accountcode", fndaccount.txtValue.Text))

            ' myMessages.delete()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub dgvallocation_CellValidating(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellValidatingEventArgs) Handles dgvallocation.CellValidating
        Dim column As GridViewDataColumn = e.Column
        If TypeOf e.Row Is GridViewRowInfo Then
            If column.HeaderText = "Account" Then
                For Each grow As GridViewDataRowInfo In dgvallocation.Rows
                    If e.RowIndex <> grow.Index Then
                        If e.Value = grow.Cells(0).Value Then
                            e.Cancel = True
                        End If
                    End If
                Next
            End If
        End If
        Dim total As Decimal
        For Each grow As GridViewRowInfo In dgvallocation.Rows
            If Not IsDBNull(grow.Cells(4).Value) Then
                total = total + grow.Cells(4).Value
            End If
        Next
        Dim remainder As Decimal = total - 100
        Dim j As Integer = dgvallocation.Rows.Count
        Dim totalafterremainder As Decimal
        If j > 0 Then
            totalafterremainder = dgvallocation.Rows(j - 1).Cells(4).Value - remainder
        End If
        If remainder >= 0 Then
            dgvallocation.Rows(j - 1).Cells(4).Value = totalafterremainder
            dgvallocation.AllowAddNewRow = False
            dgvallocation.CurrentRow.Cells(4).ReadOnly = False

        Else
            dgvallocation.AllowAddNewRow = True
        End If
    End Sub
    Private Sub dgvrollup_CellValidating(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellValidatingEventArgs) Handles dgvrollup.CellValidating
        Dim column As GridViewDataColumn = e.Column
        If TypeOf e.Row Is GridViewRowInfo Then
            If column.HeaderText = "Account" Then
                For Each grow As GridViewDataRowInfo In dgvrollup.Rows
                    If e.RowIndex <> grow.Index Then
                        If e.Value = grow.Cells(0).Value Then
                            e.Cancel = True
                        End If
                    End If
                Next
            End If
        End If
    End Sub
    ''check segment code validation 
    Private Function funsegmentcodevalidation() As Integer
        Try
            Dim strsegment As String = "select Seg_Name2 , Seg_Name3, Seg_Name4, Seg_Name5,Seg_Name6,Seg_Name7,Seg_Name8, Seg_Name9,Seg_Name10 from TSPL_GL_STRUCTURE where Str_Code = '" + fndstructurecode.txtValue.Text + "'"
            dr = clsDBFuncationality.GetDataTable(strsegment)
            ' Dim j As Integer
            Dim s As String
            Dim kj As String = ""
            Dim l As Integer
            For Each row As DataRow In dr.Rows
                For i As Integer = 0 To 8
                    If Not String.IsNullOrEmpty(row(i).ToString()) Then
                        Dim lkj As String = row(i).ToString()
                        s = connectSql.RunScalar("select Segment_code  from TSPL_GL_SEGMENT_CODE where Segment_name = '" + row(i).ToString() + "'")
                        s += "+"
                        kj = s + kj
                    End If

                Next
            Next
            Dim total As String = fndaccount.txtValue.Text + "+" + kj
            l = total.Length

            Return l - 1
        Catch ex As Exception
        Finally
        End Try
    End Function


    Private Sub dgvsubledger_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvsubledger.CellEditorInitialized
        If TypeOf Me.dgvsubledger.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.dgvsubledger.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
            editor.EditorControl.MasterTemplate.BestFitColumns()
            editor.DropDownStyle = RadDropDownStyle.DropDown
            editor.AutoFilter = True
            If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
                Dim autoFilter As FilterDescriptor = New FilterDescriptor("Source Code", FilterOperator.StartsWith, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
                'Dim autoFilter1 As FilterDescriptor = New FilterDescriptor(editor.EditorControl.MasterTemplate.Columns.Item(3).FieldName, FilterOperator.StartsWith, "")
                'autoFilter1.IsFilterEditor = True
                'editor.EditorControl.FilterDescriptors.Add(autoFilter1)


            End If
            'If editor.Text = "" Then
            '    editor.EditorControl.HideSelection = True
            'End If
        End If
    End Sub
    ''To insert the data into tspl_gl_control_acc table
    Private Sub funinsertcontrolaccount()
        Try
            Dim account As String
            If btnsave.Text = "&Save" Then
                Dim strsegname1 As String = fndaccount.txtValue.Text
                Dim straccount As String = ""
                For m As Integer = 0 To dgvsegment.Rows.Count - 1
                    If String.IsNullOrEmpty(dgvsegment.Rows(m).Cells(1).Value) Then
                    Else
                        straccount += "-" + dgvsegment.Rows(m).Cells(1).Value
                    End If
                Next
                account = strsegname1 + straccount
            Else
                account = fndaccount.txtValue.Text
            End If
            Dim strsourceledger, strsourcetype, strdesc, strquery As String
            strsourcetype = ""
            strsourceledger = ""
            strdesc = ""
            For i As Integer = 0 To dgvsubledger.Rows.Count - 1
                If Not String.IsNullOrEmpty(dgvsubledger.Rows(i).Cells(0).Value.ToString()) Then
                    strquery = "select  SourceLedger, SourceType , SourceDescription from TSPL_GL_SOURCECODE where SourceCode = '" + dgvsubledger.Rows(i).Cells(0).Value.ToString() + "'"
                    dr = clsDBFuncationality.GetDataTable(strquery)
                    For Each row As DataRow In dr.Rows
                        'dr.Read()
                        strsourceledger = row(0)
                        strsourcetype = row(1).ToString()
                        strdesc = row(2).ToString()
                    Next
                    connectSql.RunSp("SP_TSPL_GL_CONTROL_ACC_INSERT", New SqlParameter("@accountcode", account), New SqlParameter("@description", txtdesc.Text), New SqlParameter("@sourcecode", dgvsubledger.Rows(i).Cells(0).Value), New SqlParameter("@sourceledger", strsourceledger), New SqlParameter("@sourcetype", strsourcetype), New SqlParameter("@sourcedesc", strdesc), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
                End If
            Next
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    ''To delete the data from gl control account
    Private Sub fundeletecontrolaccount()
        Try
            connectSql.RunSp("SP_TSPL_GL_CONTROL_ACC_DELETE", New SqlParameter("@accountcode", fndaccount.txtValue.Text))
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    ''To Update the data in gl control account
    Private Sub funupdatecontrolaccount()
        Dim trans As SqlTransaction = Nothing
        Dim strsourceledger, strsourcetype, strdesc, strquery As String
        strsourceledger = ""
        strdesc = ""
        strsourcetype = ""
        Try
            connectSql.OpenConnection()
            trans = connectSql.OpenConnection.BeginTransaction()
            connectSql.RunSpTransaction(trans, "SP_TSPL_GL_CONTROL_ACC_DELETE", New SqlParameter("@accountcode", fndaccount.txtValue.Text))
            For i As Integer = 0 To dgvsubledger.Rows.Count - 1
                If Not String.IsNullOrEmpty(dgvsubledger.Rows(i).Cells(0).Value.ToString()) Then
                    strquery = "select  SourceLedger, SourceType , SourceDescription from TSPL_GL_SOURCECODE where SourceCode = '" + dgvsubledger.Rows(i).Cells(0).Value.ToString() + "'"
                    dr = clsDBFuncationality.GetDataTable(strquery)
                    For Each row As DataRow In dr.Rows
                        'dr.Read()
                        strsourceledger = row(0)
                        strsourcetype = row(1).ToString()
                        strdesc = row(2).ToString()
                    Next
                    connectSql.RunSpTransaction(trans, "SP_TSPL_GL_CONTROL_ACC_INSERT", New SqlParameter("@accountcode", fndaccount.txtValue.Text), New SqlParameter("@description", txtdesc.Text), New SqlParameter("@sourcecode", dgvsubledger.Rows(i).Cells(0).Value), New SqlParameter("@sourceledger", strsourceledger), New SqlParameter("@sourcetype", strsourcetype), New SqlParameter("@sourcedesc", strdesc), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
                End If
            Next
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)

        End Try
    End Sub
    ''To Avoid the same again in the first cell
    Private Sub dgvsubledger_CellValidating(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellValidatingEventArgs) Handles dgvsubledger.CellValidating
        Dim column As GridViewDataColumn = e.Column
        If TypeOf e.Row Is GridViewRowInfo Then
            If column.HeaderText = "Sub Ledger" Then
                For Each grow As GridViewDataRowInfo In dgvsubledger.Rows
                    If e.RowIndex <> grow.Index Then
                        If e.Value = grow.Cells(0).Value Then
                            e.Cancel = True
                        End If
                    End If
                Next
            End If
        End If
    End Sub

    'Private Sub ddlclosetosegment_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlclosetosegment.SelectedIndexChanged

    'End Sub

    Private Sub ddlclosetosegment_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlclosetosegment.SelectedValueChanged
        Dim straccountcode As String = ""
        If ddlclosetosegment.Text <> "" Then
            For i As Integer = 0 To dgvsegment.Rows.Count - 1
                If Not String.IsNullOrEmpty(dgvsegment.Rows(i).Cells(1).Value) Then
                    straccountcode = connectSql.RunScalar("select account_code from tspl_gl_segment_code where segment_name ='" + ddlclosetosegment.Text + "' and segment_code = '" + dgvsegment.Rows(i).Cells(1).Value + "'")
                    If Not String.IsNullOrEmpty(straccountcode) Then
                        Exit For
                    End If
                End If
            Next
            txtclosetoaccount.Text = straccountcode
        End If
    End Sub

    Private Sub dgvsegment_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvsegment.CellValueChanged
        If e.ColumnIndex = 1 Then
            Dim straccountcode As String = ""
            If ddlclosetosegment.Text <> "" Then
                For i As Integer = 0 To dgvsegment.Rows.Count - 1
                    If Not String.IsNullOrEmpty(dgvsegment.Rows(i).Cells(1).Value) Then
                        straccountcode = connectSql.RunScalar("select account_code from tspl_gl_segment_code where segment_name ='" + ddlclosetosegment.Text + "' and segment_code = '" + dgvsegment.Rows(i).Cells(1).Value + "'")
                        If Not String.IsNullOrEmpty(straccountcode) Then
                            Exit For
                        End If
                    End If
                Next
                txtclosetoaccount.Text = straccountcode
            End If
        End If
        If e.ColumnIndex = 1 Then
            Dim sr As String
            sr = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No = '" + dgvsegment.CurrentRow.Cells(1).Value + "'")
            dgvsegment.CurrentRow.Cells(2).Value = sr
        End If

    End Sub
    ''to Update the data of control account and roll up
    Private Sub funupdatecontrolacctrollup()
        Try

        Catch ex As Exception

        End Try
    End Sub
    ''To update the data of control account and auto allocation 
    Private Sub funupdatecontrolacctallocation()
        Dim trans As SqlTransaction
        Try
            connectSql.OpenConnection()
            trans = clsDBFuncationality.GetTransactin()



        Catch ex As Exception

        End Try
    End Sub
    ''To update the data of roll up and auot allocation
    Private Sub funupdaterollupallocation()
        Try

        Catch ex As Exception

        End Try
    End Sub
    ''To update the data of control account and roll up and auto allocation 
    Private Sub funupdatecontrolacctrollupallocation()
        Try

        Catch ex As Exception

        End Try
    End Sub


    Private Sub MasterTemplate_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvrollup.CellEditorInitialized
        If TypeOf Me.dgvrollup.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.dgvrollup.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
            editor.EditorControl.MasterTemplate.BestFitColumns()
            editor.DropDownStyle = RadDropDownStyle.DropDown
            editor.AutoFilter = True
            If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
                Dim autoFilter As FilterDescriptor = New FilterDescriptor("description", FilterOperator.StartsWith, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If
        End If
    End Sub


    Private Sub dgvrollup_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles dgvrollup.EditorRequired
        Dim straccount As String = "SELECT ACCOUNT_CODE AS [Account Code], DESCRIPTION AS [Description] from tspl_gl_accounts where Account_Code <>'" + fndaccount.txtValue.Text.Trim() + "'"
        ds = connectSql.RunSQLReturnDS(straccount)
        Dim dtaccount1 As GridViewMultiComboBoxColumn = TryCast(dgvrollup.Columns(0), GridViewMultiComboBoxColumn)
        dtaccount1.DataSource = ds.Tables(0)
        dtaccount1.ValueMember = "Account Code"
    End Sub

    Private Sub RadGroupBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox2.Click

    End Sub

    Private Sub fndfromacct_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndfromacct.Load
        fndfromacct.ConnectionString = connectSql.SqlCon()
        fndfromacct.Query = "select Account_Code as [Account Code], Description  from TSPL_GL_ACCOUNTS"
        fndfromacct.ValueToSelect = "Account Code"
        fndfromacct.ValueToSelect1 = "Description"
        fndfromacct.Caption = "Account Details"
    End Sub

    Private Sub fndtoacct_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndtoacct.Load
        fndtoacct.ConnectionString = connectSql.SqlCon()
        fndtoacct.Query = "select Account_Code as [Account Code], Description  from TSPL_GL_ACCOUNTS"
        fndtoacct.ValueToSelect = "Account Code"
        fndtoacct.ValueToSelect1 = "Description"
        fndtoacct.Caption = "Account Details"
    End Sub

  
    Private Sub frmGLAccountDetails_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled Then
            deletedata()
        ElseIf e.KeyCode = Keys.Escape Then
            closeform()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnreset.Enabled Then
            funreset()
        End If
    End Sub
End Class
