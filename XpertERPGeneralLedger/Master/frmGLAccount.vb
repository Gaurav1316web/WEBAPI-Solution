Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports common
 
Public Class frmGLAccount
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim qry As String = ""
    Dim whrcls As String = ""
    Dim userCode, companyCode As String
    Dim dr As SqlDataReader
    Dim ds As New DataSet()
    Dim dt As New DataTable()
    Dim increment As Integer
    Dim arrlist As New ArrayList()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
    Public strFormName As String = ""
    Dim RollUpmessage As String = ""

    Dim ds1 As New DataSet()

    Public strAccountCode As String = ""
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.glAccount)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            RadMenuItem2.Enabled = True
            RadMenuItem3.Enabled = True
        Else
            RadMenuItem2.Enabled = False
            RadMenuItem3.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

    Private Sub funloaddata()
        Try
            Dim da As New SqlDataAdapter("select account_code, description from tspl_gl_accounts where account_code='" + fndaccount.Value + "' ", connectSql.SqlCon())
            da.Fill(ds1, "tspl_gl_accounts")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub

    Private Sub funshowdata()
        Try
            funloaddata()
            fndaccount.Value = ds1.Tables(0).Rows(increment)(0).ToString()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub

    Private Sub funnext()
        funloaddata()
        Try
            If ds1.Tables(0).Rows.Count > 0 Then
                If (increment < ds1.Tables(0).Rows.Count - 1) Then
                    increment = increment + 1
                    funshowdata()
                Else
                    common.clsCommon.MyMessageBoxShow("This is the last data")
                End If
            Else
                common.clsCommon.MyMessageBoxShow("This is the last data")

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub funprevious()
        funloaddata()
        Try
            If ds1.Tables(0).Rows.Count > 0 Then
                If (increment > 0) Then
                    increment = increment - 1
                    funshowdata()
                Else
                    common.clsCommon.MyMessageBoxShow("This is the first data")
                End If
            Else
                common.clsCommon.MyMessageBoxShow("This is the first data")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub funfirst()
        funloaddata()
        Try
            If ds1.Tables(0).Rows.Count > 0 Then
                increment = 0
                funshowdata()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub

    Private Sub funlast()
        funloaddata()
        Try
            increment = ds1.Tables(0).Rows.Count - 1
            funshowdata()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub

    Private Sub funinsert()
        Dim strstatus As String
        Dim strautoallocation As String
        Dim strrollup As String = "N"
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

        If chkmulticurrency.Checked = True Then
            strmulticurrency = "Y"
        Else
            strmulticurrency = "N"
        End If
        Dim strdesc1, strdesc2, strdesc3, strdesc4, strdesc5, strdesc6, strdesc7, strdesc8, strdesc9, strdesc10 As String
        Dim strcode1, strcode2, strcode3, strcode4, strcode5, strcode6, strcode7, strcode8, strcode9, strcode10 As String
        strcode2 = ""
        strdesc2 = ""
        strcode3 = ""
        strdesc3 = ""
        strcode4 = ""
        strdesc4 = ""
        strcode5 = ""
        strdesc5 = ""
        strcode6 = ""
        strdesc6 = ""
        strcode7 = ""
        strdesc7 = ""
        strcode8 = ""
        strdesc8 = ""
        strcode9 = ""
        strdesc9 = ""
        strcode10 = ""
        strdesc10 = ""
        strcode1 = fndaccount.Value
        ' Dim i As Integer = connectSql.RunScalar("select Seg_Length  from TSPL_GL_SEGMENT where Seg_No = '1'")
        ' Dim strcode1 As String = t.Substring(0, i)
        'this is for test
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
        Dim op As Integer = clsDBFuncationality.getSingleValue("select Seg_Length  from TSPL_GL_SEGMENT where Seg_No = '1'")

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
            connectSql.RunSp("SP_TSPL_GL_ACCOUNTS_INSERT", New SqlParameter("@accountcode", account), New SqlParameter("@description", txtdesc.Text), New SqlParameter("@strcode", fndstructurecode.Value), New SqlParameter("@strdesc", txtstrdesc.Text), New SqlParameter("@accbal", ddlnormalbal.Text), New SqlParameter("@acctype", ""), New SqlParameter("@accgroupcode", ""), New SqlParameter("@accgroupdesc", ""), New SqlParameter("@status", strstatus), New SqlParameter("@controlaccount", strcontrolaccount), New SqlParameter("@autoallocation", strautoallocation), New SqlParameter("@rollup", strrollup), New SqlParameter("@multicurrency", strmulticurrency), New SqlParameter("@accsegcode1", strsegname1), New SqlParameter("@accsegdesc1", txtdesc.Text), New SqlParameter("@accsegcode2", strcode2), New SqlParameter("@accsegdesc2", strdesc2), New SqlParameter("@accsegcode3", strcode3), New SqlParameter("@accsegdesc3", strdesc3), New SqlParameter("@accsegcode4", strcode4), New SqlParameter("@accsegdesc4", strdesc4), New SqlParameter("@accsegcode5", strcode5), New SqlParameter("@accsegdesc5", strdesc5), New SqlParameter("@accsegcode6", strcode6), New SqlParameter("@accsegdesc6", strdesc6), New SqlParameter("@accsegcode7", strcode7), New SqlParameter("@accsegdesc7", strdesc7), New SqlParameter("@accsegcode8", strcode8), New SqlParameter("@accsegdesc8", strdesc8), New SqlParameter("@accsegcode9", strcode9), New SqlParameter("@accsegdesc9", strdesc9), New SqlParameter("@accsegcode10", strcode10), New SqlParameter("@accsegdesc10", strdesc10), New SqlParameter("@closetoseg", ddlclosetosegment.Text), New SqlParameter("@closetoaccount", txtclosetoaccount.Text), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            fndaccount.Value = fndaccount.Value + straccount
            UpdateColumns()
            myMessages.insert()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Function funaccountcodelength() As String
        Dim strsegname1 As String = fndaccount.Value
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

    Private Sub funupdate()
        Dim strstatus As String
        Dim strautoallocation As String
        'Dim strrollup As String
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

        If chkmulticurrency.Checked = True Then
            strmulticurrency = "Y"
        Else
            strmulticurrency = "N"
        End If
        Dim strdesc1, strdesc2, strdesc3, strdesc4, strdesc5, strdesc6, strdesc7, strdesc8, strdesc9, strdesc10 As String
        Dim strcode1, strcode2, strcode3, strcode4, strcode5, strcode6, strcode7, strcode8, strcode9, strcode10 As String
        strcode2 = ""
        strcode3 = ""
        strcode4 = ""
        strcode5 = ""
        strcode6 = ""
        strcode7 = ""
        strcode8 = ""
        strcode9 = ""
        strcode10 = ""
        strdesc2 = ""
        strdesc3 = ""
        strdesc4 = ""
        strdesc5 = ""
        strdesc6 = ""
        strdesc7 = ""
        strdesc8 = ""
        strdesc9 = ""
        strdesc10 = ""
        strcode1 = fndaccount.Value
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
        Dim t As String = fndaccount.Value
        Dim i As Integer = connectSql.RunScalar("select Seg_Length  from TSPL_GL_SEGMENT where Seg_No = '1'")
        Dim strsegname1 As String = t.Substring(0, i)
        Try
            connectSql.RunSp("SP_TSPL_GL_ACCOUNTS_UPDATE", New SqlParameter("@accountcode", fndaccount.Value), New SqlParameter("@description", txtdesc.Text), New SqlParameter("@strcode", fndstructurecode.Value), New SqlParameter("@strdesc", txtstrdesc.Text), New SqlParameter("@accbal", ddlnormalbal.Text), New SqlParameter("@acctype", ""), New SqlParameter("@accgroupcode", ""), New SqlParameter("@accgroupdesc", ""), New SqlParameter("@status", strstatus), New SqlParameter("@controlaccount", strcontrolaccount), New SqlParameter("@autoallocation", strautoallocation), New SqlParameter("@rollup", ""), New SqlParameter("@multicurrency", strmulticurrency), New SqlParameter("@accsegcode1", strsegname1), New SqlParameter("@accsegdesc1", txtdesc.Text), New SqlParameter("@accsegcode2", strcode2), New SqlParameter("@accsegdesc2", strdesc2), New SqlParameter("@accsegcode3", strcode3), New SqlParameter("@accsegdesc3", strdesc3), New SqlParameter("@accsegcode4", strcode4), New SqlParameter("@accsegdesc4", strdesc4), New SqlParameter("@accsegcode5", strcode5), New SqlParameter("@accsegdesc5", strdesc5), New SqlParameter("@accsegcode6", strcode6), New SqlParameter("@accsegdesc6", strdesc6), New SqlParameter("@accsegcode7", strcode7), New SqlParameter("@accsegdesc7", strdesc7), New SqlParameter("@accsegcode8", strcode8), New SqlParameter("@accsegdesc8", strdesc8), New SqlParameter("@accsegcode9", strcode9), New SqlParameter("@accsegdesc9", strdesc9), New SqlParameter("@accsegcode10", strcode10), New SqlParameter("@accsegdesc10", strdesc10), New SqlParameter("@closetoseg", ddlclosetosegment.Text), New SqlParameter("@closetoaccount", txtclosetoaccount.Text), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", objCommonVar.CurrentUserCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
            UpdateColumns()
            myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Sub UpdateColumns()
        Dim qry As String = "update TSPL_GL_ACCOUNTS set GL_Main_Code='" + txtAccountSubGroup.Value + "',  Tax_Type='" + clsCommon.myCstr(cboTaxType.SelectedValue) + "',Purchase_Sale_Type= "
        If clsCommon.myLen(cboTaxType.SelectedValue) > 0 Then
            If rbtnPurhcase.IsChecked Then
                qry += "'1'"
            Else
                qry += "'2'"
            End If
        Else
            qry += "'0' "
        End If

        qry += " where Account_Code='" + fndaccount.Value + "'"
        clsDBFuncationality.ExecuteNonQuery(qry)

        qry = "Update TSPL_GL_ACCOUNTS set Description='" & txtdesc.Text & "' where Account_Code='" & fndaccount.Value & "'"
        clsDBFuncationality.ExecuteNonQuery(qry)

    End Sub

    Private Sub fundelete(ByVal Reason As String)
        Try
            '-----for payment screen------- 
            Dim qry As String = "select count(*) from TSPL_PAYMENT_DETAIL where Account_Code='" + fndaccount.Value + "'"
            Dim count As Integer = clsDBFuncationality.getSingleValue(qry)

            If count = 0 Then
                connectSql.RunSp("SP_TSPL_GL_ACCOUNTS_DELETE", New SqlParameter("accountcode", fndaccount.Value))
                saveCancelLog(Reason, "Delete", Nothing)
                myMessages.delete()
                funreset()
            Else
                common.clsCommon.MyMessageBoxShow("This Record Cannot be deleted. It is used by another Process")
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.fndaccount.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub funreset()
        fndaccount.Value = ""
        fndaccount.MyReadOnly = False '' Done By abhishek As on 22 aug 2012
        'fndaccountgroup.Value = ""
        fndstructurecode.Value = ""
        fndstructurecode.Enabled = True

        ddlnormalbal.Text = "select"
        'ddlaccounttype.Text = "select"
        txtdesc.Text = ""
        txtstrdesc.Text = ""
        'acctdesc.Text = ""
        rdactive.IsChecked = True
        chkautoallocation.Checked = False
        chkcontrolaccount.Checked = False
        chkmulticurrency.Checked = False

        btnsave.Text = "Save"
        btndelete.Enabled = False
        dgvsegment.DataSource = Nothing
        ddlclosetosegment.Text = ""
        txtclosetoaccount.Text = ""
        chkcontrolaccount.Enabled = False
        'dgvsubledger.DataSource = Nothing

        txtAccountSubGroup.Value = ""
        lblSubGroup.Text = ""
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

        'chkcontrolaccount.Enabled = True

        chkautoallocation.Enabled = True
        fndsourcecode.Value = ""
        dgvallocation.DataSource = Nothing
        dgvallocation.Rows.Clear()
        dgvallocation.AllowAddNewRow = True

        cboTaxType.SelectedValue = ""
        rbtnNA.IsChecked = True
        'ddlaccounttype.Enabled = True
    End Sub

    Private Sub funfill()
        Try
            Dim straccount As String = "select Description, Str_Code, Str_Description, Account_Balance, Account_Type, Account_Group_Code, Account_Group_Desc, Status, ControlAccount, AutoAllocation, Rollup, multicurrency,Close_To_Seg , Close_To_Acct,Tax_Type,Purchase_Sale_Type,GL_Main_Code from TSPL_GL_ACCOUNTS  where Account_Code = '" + fndaccount.Value + "'"
            'dr = connectSql.RunSqlReturnDR(straccount)
            'While dr.Read()
            ds = connectSql.RunSQLReturnDS(straccount)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow = ds.Tables(0).Rows(0)
                txtdesc.Text = dr(0).ToString()
                fndstructurecode.Value = dr(1).ToString()
                fndstructurecode.Enabled = False
                fndstructurecode.BackColor = Color.White
                txtstrdesc.Text = dr(2).ToString()
                txtstrdesc.ReadOnly = True
                cboTaxType.SelectedValue = clsCommon.myCstr(dr("Tax_Type"))
                ddlnormalbal.Text = dr(3).ToString()
                'ddlaccounttype.Text = dr(4).ToString().Trim()
                'If ddlaccounttype.Text = "Income Statement" Then
                '    RadGroupBox1.Visible = True
                'End If

                Dim intPurchaseSaleType As Integer = clsCommon.myCdbl(dr("Purchase_Sale_Type"))
                If intPurchaseSaleType = 1 Then
                    rbtnPurhcase.IsChecked = True
                ElseIf intPurchaseSaleType = 2 Then
                    rbtnSale.IsChecked = True
                Else
                    rbtnNA.IsChecked = True
                End If

                txtAccountSubGroup.Value = clsCommon.myCstr(dr("GL_Main_Code"))
                lblSubGroup.Text = clsDBFuncationality.getSingleValue("select Main_GL_Account_Desc from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account ='" + txtAccountSubGroup.Value + "'")

                'fndaccountgroup.Value = dr(5).ToString()
                'acctdesc.Text = dr(6).ToString()
                Dim strstatus As String = dr(7).ToString().Trim()
                If strstatus = "Y" Then
                    rdactive.IsChecked = True
                Else
                    rdinactive.IsChecked = True
                End If
                Dim strcontrolaccount As String = dr(8).ToString().Trim()
                If strcontrolaccount = "Y" Then
                    chkcontrolaccount.Checked = True
                    ''chkcontrolaccount.Enabled = False Done By abhishek as on 19 Nov 2012
                    funfillcontrolaccount()

                Else
                    'dgvsubledger.DataSource = Nothing
                    chkcontrolaccount.Checked = False
                End If
                Dim strautoallocation As String = dr(9).ToString()
                If strautoallocation = "Y" Then
                    chkautoallocation.Checked = True
                    dgvallocation.AutoGenerateColumns = False
                    Dim strsourcecode As String = "select Source_Code from TSPL_GL_ALLOCATIONS where Main_Account_Code ='" + fndaccount.Value + "'"
                    Dim strallocationgrid As String = "select  account_code, account_desc, Reference , description, percentage from TSPL_GL_ALLOCATIONS where Main_Account_Code ='" + fndaccount.Value + "'"
                    '' added by abhishek as on 12/10/2012
                    Dim srccode As String = clsDBFuncationality.getSingleValue(strallocationgrid)
                    If clsCommon.myLen(srccode) > 0 Then
                        fndsourcecode.Value = srccode
                    End If
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
            Dim strsegcode As String = "select Account_Seg_Code2, Account_Seg_Code3, Account_Seg_Code4 , Account_Seg_Code5 , Account_Seg_Code6 , Account_Seg_Code7 , Account_Seg_Code8 , Account_Seg_Code9 , Account_Seg_Code10  from TSPL_GL_ACCOUNTS  where Account_Code = '" + fndaccount.Value + "'"
            '' Added by abhishek as on 12/10/2012
            dt = clsDBFuncationality.GetDataTable(strsegcode)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    For i As Integer = 0 To 8
                        If Not String.IsNullOrEmpty(dr(i).ToString()) Then
                            dgvsegment.Rows(i).Cells(2).Value = connectSql.RunScalar("select description from tspl_gl_segment_code where segment_code = '" + dr(i).ToString() + "' ")

                            dgvsegment.Rows(i).Cells(1).Value = dr(i).ToString()

                        End If
                    Next
                Next
            End If

            btnsave.Text = "Update"
            btndelete.Enabled = True
            fndaccount.MyReadOnly = True
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub funfillcontrolaccount()

        'Try
        '    dgvsubledger.AutoGenerateColumns = False
        '    dgvsubledger.DataSource = Nothing
        '    Dim strsourcecode As String = "select source_code from TSPL_GL_CONTROL_ACC where Account_Code ='" + fndaccount.Value + "'"

        '    transportSql.FillGridView(strsourcecode, dgvsubledger)
        '    dgvsubledger.Columns(0).FieldName = "source_code"
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub fndaccountgroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndaccountgroup.ConnectionString = connectSql.SqlCon()
        'fndaccountgroup.Query = "select Account_group_code as [Account Group Code], Account_group_desc as [Description] from dbo.TSPL_ACCOUNT_GROUPS"
        'fndaccountgroup.ValueToSelect = "Account Group Code"
        'fndaccountgroup.ValueToSelect1 = "Description"
        'fndaccountgroup.Caption = " Account Details"
    End Sub

    Private Sub frmGLAccount_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus

    End Sub

    Private Sub frmGLAccount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnreset.Enabled Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()

        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub Frmglaccount1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        chkmulticurrency.Visible = False '' Done By abhishek as On 22 aug 2012 
        chkcontrolaccount.Visible = True   '' Done By abhishek as On 22 aug 2012 
        funbindsubledger()

        RadPageView1.Controls.Remove(RadPageViewPage3)
        RadPageView1.Controls.Remove(RadPageViewPage4)
        'fndaccountgroup.txtValue.MaxLength = 10
        'fndstructurecode.txtValue.MaxLength = 10
        'fndaccount1.txtValue.MaxLength = 45
        rdactive.IsChecked = True
        btndelete.Enabled = False
        'AddHandler fndaccount.ValueChanged, AddressOf account_textchanged
        'AddHandler fndstructurecode.ValueChanged, AddressOf structure_changed
        'AddHandler fndaccountgroup.ValueChanged, AddressOf group_changed

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
        Dim checksegment As Integer = connectSql.RunScalar("select count(*) from TSPL_GL_SEGMENT_PERMISSION where User_Code = '" + userCode + "'")
        Dim checkaccount As Integer = connectSql.RunScalar("select count(*) from TSPL_GL_ACCOUNT_PERMISSION where User_Code = '" + userCode + "'")
        'fndaccount1.txtValue.MaxLength = lenth
        'fndaccountgroup.MyReadOnly = True
        fndstructurecode.MyReadOnly = True
        arrlist = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
        qry = arrlist.Item(0)
        whrcls = arrlist.Item(1)
        If objCommonVar.CurrentUserCode = "ADMIN" Then
            qry = qry + " where " + " Account_Code not in('" + fndaccount.Value.Trim() + "') "
        ElseIf checksegment = 0 Or checkaccount = 0 Then
            qry = qry + " where " + " Account_Code not in('" + fndaccount.Value.Trim() + "') "
        Else
            qry = qry + " where " + whrcls + " "
        End If
        'Dim straccount As String = "SELECT ACCOUNT_CODE AS [Account Code], DESCRIPTION AS [Description] from tspl_gl_accounts where Account_Code not in('" + fndaccount.txtValue.Text.Trim() + "')"
        ds = connectSql.RunSQLReturnDS(qry)
        Dim dtaccount As GridViewComboBoxColumn = TryCast(dgvallocation.Columns(0), GridViewComboBoxColumn)
        dtaccount.DataSource = ds.Tables(0)
        dtaccount.ValueMember = "Account_Code"
        dtaccount.DisplayMember = "Account_Code"
        'Dim dtaccount1 As GridViewMultiComboBoxColumn = TryCast(dgvrollup.Columns(0), GridViewMultiComboBoxColumn)
        'dtaccount1.DataSource = ds.Tables(0)
        'dtaccount1.ValueMember = "Account Code"
        fndstructurecode.MyReadOnly = True
        'fndaccountgroup.MyReadOnly = True
        funcheckstructurecode()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N Adding New Trasnaction")
        SetLength()
        LoadTaxType()

        If strAccountCode <> "" Then
            fndaccount.Value = strAccountCode
            funfill()
        End If
        chkcontrolaccount.Enabled = False
    End Sub

    Private Sub LoadTaxType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "VAT"
        dr("Name") = "VAT"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "CST"
        dr("Name") = "CST"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "EXCISE"
        dr("Name") = "EXCISE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "ADDTAX"
        dr("Name") = "ADDTAX"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "OTHER"
        dr("Name") = "OTHER"
        dt.Rows.Add(dr)

        cboTaxType.DataSource = dt
        cboTaxType.ValueMember = "Code"
        cboTaxType.DisplayMember = "Name"
    End Sub

    Public Sub SetLength()
        fndaccount.MyMaxLength = 50
        txtdesc.MaxLength = 100

    End Sub

    Private Sub funbindsubledger()
        'Dim strsourcledger As String = "select SourceCode as [Source Code], SourceLedger as [Source Ledger], SourceType as [Source Type], SourceDescription as [Description] from TSPL_GL_SOURCECODE"
        'ds = connectSql.RunSQLReturnDS(strsourcledger)
        'Dim grdmcsourcecode As GridViewMultiComboBoxColumn = TryCast(dgvsubledger.Columns(0), GridViewMultiComboBoxColumn)
        'grdmcsourcecode.DataSource = ds.Tables(0)
        'grdmcsourcecode.ValueMember = "Source Code"
        'grdmcsourcecode.DisplayMember = "Source Code"
    End Sub

    Private Sub funcheckstructurecode()
        Try
            Dim strcode As String = connectSql.RunScalar("select structure_code from tspl_glsetting")
            If Not String.IsNullOrEmpty(strcode) Then
                fndstructurecode.Value = strcode
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Sub accountTextchanged()
        Dim str As String = clsDBFuncationality.getSingleValue("select account_code from TSPL_GL_ACCOUNTS where account_code = '" + fndaccount.Value + "'")
        Dim straccount As String = ""
        If clsCommon.myLen(str) > 0 Then
            straccount = str
        End If
        If straccount <> "" Then
            funfill()
        Else
            'fndaccountgroup.Value = ""
            '  fndstructurecode.Value = ""
            ddlnormalbal.Text = "select"
            'ddlaccounttype.Text = "select"
            txtdesc.Text = ""
            ' txtstrdesc.Text = ""
            'acctdesc.Text = ""
            rdactive.IsChecked = True
            chkautoallocation.Checked = False
            chkcontrolaccount.Checked = False
            chkmulticurrency.Checked = False

            btnsave.Text = "Save"
            btndelete.Enabled = False
        End If
    End Sub

    Private Sub structure_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        strchang()

    End Sub

    Sub strchang()
        fndstructurecode.Tag = txtstrdesc.Text
        Dim strsegmentname As String = "select seg_name2, seg_name3, seg_name4, seg_name5, seg_name6, seg_name7, seg_name8, seg_name9, seg_name10 from tspl_gl_structure where str_code = '" + fndstructurecode.Value + "'"
        '' added by abhishek as on 12/10/2012
        dt = clsDBFuncationality.GetDataTable(strsegmentname)
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
        '' added by abhishek as on 12/10/2012
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                For i As Integer = 0 To 8
                    STRSEG1 = dr(i).ToString()
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
        End If


        Dim strdesc As String = "select Str_Description  from TSPL_GL_STRUCTURE  where Str_Code = '" + fndstructurecode.Value.Trim() + "'"
        txtstrdesc.Text = connectSql.RunScalar(strdesc)
    End Sub

    Private Sub group_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndaccountgroup.txtValue.Tag = acctdesc.Text
        'Dim straccdesc As String = "select Account_Group_Desc  from TSPL_ACCOUNT_GROUPS  where Account_Group_Code = '" + fndaccountgroup.Value.Trim() + "'"
        'acctdesc.Text = connectSql.RunScalar(straccdesc)
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        funreset()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Dim qst As String
        Dim dpt As String
        Dim qry As String
        Dim count As Integer
        Dim StrctureCodeLength As Integer = strcCodeLength(fndstructurecode.Value)
        If StrctureCodeLength > 1 Then
            qry = "select COUNT(*) from TSPL_GL_ROLLUP  where account ='" + fndaccount.Value + "'"
            count = clsDBFuncationality.getSingleValue(qry)
            If count > 0 Then
                common.clsCommon.MyMessageBoxShow("This Account Code Cannot be deleted." + Environment.NewLine + "This is already Roll up")
                Return
            End If
        End If
        qst = "Select Account_No from TSPL_Receipt_Adjustment_Detail where Account_No = '" + fndaccount.Value + "'"
        dpt = clsDBFuncationality.getSingleValue(qst)
        If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
            common.clsCommon.MyMessageBoxShow("This Account Code Cannot be deleted." + Environment.NewLine + "This is already in Process")
            Return
        End If
        qst = "Select Account_Code from TSPL_RECEIPT_DETAIL where Account_Code = '" + fndaccount.Value + "'"
        dpt = clsDBFuncationality.getSingleValue(qst)
        If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
            common.clsCommon.MyMessageBoxShow("This Account Code Cannot be deleted." + Environment.NewLine + "This is already in Process")
            Return
        End If


        Dim str As String = clsDBFuncationality.getSingleValue("select account_code from TSPL_GL_ACCOUNTS where account_code = '" + fndaccount.Value + "'")
        Dim straccount As String = ""
        '' added by abhishek as 0n 12/10/2012
        If clsCommon.myLen(str) > 0 Then
            straccount = str
        End If
        If fndaccount.Value = "" Then
            common.clsCommon.MyMessageBoxShow("Please Enter the Account Id")
            'fndaccount1.txtValue.Focus()
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

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        Me.Close()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        'Dim jlength As Integer = connectSql.RunScalar("select total_length from TSPL_STRUCTURE_MASTER where Structure_Code ='" + fndstructurecode.Value + "'")
        Save()
    End Sub

    Sub Save()
        Try
            Dim str As String = funaccountcodelength()
            Dim StrctureCodeLength As Integer = strcCodeLength(fndstructurecode.Value)
            Dim length As Integer = str.Length
            Dim length1 As Integer = funsegmentcodevalidation()
            If clsCommon.myLen(cboTaxType.SelectedValue) > 0 AndAlso rbtnNA.IsChecked Then
                common.clsCommon.MyMessageBoxShow("Please select Tax Type i.e Purchase or sale")
                RadGroupBox3.Focus()
                Exit Sub
            End If
            clsGLAccount.GetLinkAccountWithGroup(5, fndaccount.Value, txtAccountSubGroup.Value, Nothing)
            If fndaccount.Value = "" Then
                common.clsCommon.MyMessageBoxShow(" Account id can not be left blank  ")
                fndaccount.Focus()
                Exit Sub
            ElseIf fndstructurecode.Value = "" Then
                common.clsCommon.MyMessageBoxShow(" Structure Code can not be left blank ")
                fndstructurecode.Focus()
                Exit Sub

            ElseIf ddlnormalbal.Text = "select" Then
                common.clsCommon.MyMessageBoxShow(" Account Balance can not be left blank")
                ddlnormalbal.Focus()
                Exit Sub

            ElseIf clsCommon.myLen(txtAccountSubGroup.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("GL Main Account can not be left blank")
                txtAccountSubGroup.Focus()
                Exit Sub
            Else
                Try

                    If MyBase.isModifyonPasswordFlag Then
                        If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.glAccount, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                        Else
                            Return
                        End If
                    End If
                    clsGLAccount.CheckControlAccount(fndaccount.Value, chkcontrolaccount.Checked, Nothing)
                    Dim qry As String = "delete from TSPL_GL_ROLLUP where Account_Code='" + fndaccount.Value + "'"
                    common.clsDBFuncationality.ExecuteNonQuery(qry)
                    If btnsave.Text = "Save" Then
                        If chkautoallocation.Checked = True Then
                            Dim total As Decimal
                            Dim introwcount As Integer = dgvallocation.Rows.Count

                            For Each grow As GridViewRowInfo In dgvallocation.Rows
                                total = total + grow.Cells(4).Value
                            Next
                            If fndsourcecode.Value = "" Then
                                common.clsCommon.MyMessageBoxShow("Please select the source Code")
                                fndsourcecode.Focus()
                            ElseIf introwcount = 0 Then
                                common.clsCommon.MyMessageBoxShow("Please select at least one account")
                            ElseIf total <> 100 Then
                                common.clsCommon.MyMessageBoxShow("Percentage should be 100")

                            Else
                                If length = length1 Then
                                    If StrctureCodeLength > 1 Then
                                        funinsertallocation()
                                        funinsert()
                                        funfill()
                                    Else
                                        funinsertallocation()
                                        funinsert()
                                        funfill()
                                    End If
                                Else
                                    common.clsCommon.MyMessageBoxShow("Please select all the segment code")
                                End If
                            End If
                        ElseIf chkcontrolaccount.Checked = True Then
                        Else
                            funinsert()
                            funfill()
                        End If
                    Else : btnsave.Text = "Update"
                        If clsCommon.myLen(fndaccount.Value) > 0 Then
                            If clsCommon.myLen(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct Account_Seg_Code1  from TSPL_GL_ACCOUNTS where Account_Seg_Code1 ='" + fndaccount.Value + "'"))) < 0 Then
                                common.clsCommon.MyMessageBoxShow("This Account Code does not exist")
                                fndaccount.Focus()
                            End If
                        End If

                        If chkautoallocation.Checked = True Then
                            Dim total As Decimal
                            Dim introwcount As Integer = dgvallocation.Rows.Count

                            For Each grow As GridViewRowInfo In dgvallocation.Rows
                                total = total + grow.Cells(4).Value
                            Next
                            If fndsourcecode.Value = "" Then
                                common.clsCommon.MyMessageBoxShow("Please select the source Code")
                                fndsourcecode.Focus()
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
                        Else
                            funupdate()
                            funfill()
                        End If
                    End If
                Catch ex As Exception
                    clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndaccount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndaccount1.ConnectionString = connectSql.SqlCon()
        'fndaccount1.Query = clsERPFuncationality.glaccountquery
        '' objCommonVar.strCurrUserGLAccount
        'fndaccount1.ValueToSelect = "Account_Code"
        'fndaccount1.Caption = "Account Details"
        'fndaccount1.ValueToSelect1 = "Description"
    End Sub

    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click

        'Dim str As String = "select Account_Code as [Account Code], Description as [Description], Str_Code as [Structure Code], Str_Description as [Structure Description], Account_Balance as [Account Balance], Account_Type as [Account Type], Account_Group_Code as [Account Group Code], Account_Group_Desc as [Account Group Code Description], Status as [Status], ControlAccount as [Control Account], autoallocation as [Auto Allocation], Rollup as [Rollup], multicurrency as [Multicurrency], account_seg_code1 as [Segment Code 1], account_seg_desc1 as [Segment Description 1], account_seg_code2 as [Segment Code 2], account_seg_desc2 as [Segment Description 2],Account_Seg_Code3 as [Segment Code 3],Account_Seg_Desc3 as [Segment Description 3],Account_Seg_Code4 as [Segment Code 4],Account_Seg_Desc4 as [Segment Description 4],Account_Seg_Code5 as [Segment Code 5],Account_Seg_Desc5 as [Segment Description 5] ,Account_Seg_Code6 as [Segment Code 6],Account_Seg_Desc6 as [Segment Description 6],Account_Seg_Code7 as [Segment Code 7],Account_Seg_Desc7 as [Segment Description 7],Account_Seg_Code8  as [Segment Code 8],Account_Seg_Desc8 as [Segment Description 8],Account_Seg_Code9 as [Segment Code 9],Account_Seg_Desc9 as [Segment Description 9],Account_Seg_Code10 as [Segment Code 10],Account_Seg_Desc10 as [Segment Description 10], Close_To_Seg as [Close To Segment], Close_To_Acct as [Close To Account], Created_By as [Created By], Created_Date as [Created Date], Modify_By as [Modify By], Modify_Date as [Modify Date], Comp_Code as [Company Code]  from TSPL_GL_ACCOUNTS "
        'transportSql.ExporttoExcel(str, Me)

    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        'funimport()
    End Sub

    Private Sub funimport()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Account Code", "Description", "Structure Code", "Structure Description", "Account Balance", "Status", "Control Account", "Auto Allocation", "Multicurrency", "Segment Code 1", "Segment Description 1", "Segment Code 2", "Segment Description 2", "Segment Code 3", "Segment Description 3", "Segment Code 4", "Segment Description 4", "Segment Code 5", "Segment Description 5", "Segment Code 6", "Segment Description 6", "Segment Code 7", "Segment Description 7", "Segment Code 8", "Segment Description 8", "Segment Code 9", "Segment Description 9", "Segment Code 10", "Segment Description 10", "Close To Segment", "Close To Account", "GL Main Account") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim straccountcode As String = clsCommon.myCstr(grow.Cells(0).Value).Trim()
                    Dim strdesc As String = clsCommon.myCstr(grow.Cells(1).Value).Trim()
                    Dim strstructcode As String = clsCommon.myCstr(grow.Cells(2).Value).Trim()
                    Dim strstructdesc As String = clsCommon.myCstr(grow.Cells(3).Value).Trim()
                    Dim stracctbalance As String = clsCommon.myCstr(grow.Cells(4).Value).Trim()
                    'Dim straccttype As String = clsCommon.myCstr(grow.Cells(5).Value).Trim()
                    'Dim stracctgroupcode As String = clsCommon.myCstr(grow.Cells(6).Value).Trim()
                    'Dim stracctgroupdesc As String = clsCommon.myCstr(grow.Cells(7).Value)
                    Dim strstatus As String = clsCommon.myCstr(grow.Cells(5).Value)
                    Dim strcontrolacct As String = clsCommon.myCstr(grow.Cells(6).Value)
                    Dim strautoallocation As String = clsCommon.myCstr(grow.Cells(7).Value)
                    'Dim strrollup As String = clsCommon.myCstr(grow.Cells(11).Value)
                    Dim strmulticurrency As String = clsCommon.myCstr(grow.Cells(8).Value)
                    Dim strsegcode1 As String = clsCommon.myCstr(grow.Cells(9).Value)
                    Dim strsegdesc1 As String = clsCommon.myCstr(grow.Cells(10).Value)
                    Dim strsegcode2 As String = clsCommon.myCstr(grow.Cells(11).Value)
                    Dim strsegdesc2 As String = clsCommon.myCstr(grow.Cells(12).Value)
                    Dim strsegcode3 As String = clsCommon.myCstr(grow.Cells(13).Value)
                    Dim strsegdesc3 As String = clsCommon.myCstr(grow.Cells(14).Value)
                    Dim strsegcode4 As String = clsCommon.myCstr(grow.Cells(15).Value)
                    Dim strsegdesc4 As String = clsCommon.myCstr(grow.Cells(16).Value)
                    Dim strsegcode5 As String = clsCommon.myCstr(grow.Cells(17).Value)
                    Dim strsegdesc5 As String = clsCommon.myCstr(grow.Cells(18).Value)
                    Dim strsegcode6 As String = clsCommon.myCstr(grow.Cells(19).Value)
                    Dim strsegdesc6 As String = clsCommon.myCstr(grow.Cells(20).Value)
                    Dim strsegcode7 As String = clsCommon.myCstr(grow.Cells(21).Value)
                    Dim strsegdesc7 As String = clsCommon.myCstr(grow.Cells(22).Value)
                    Dim strsegcode8 As String = clsCommon.myCstr(grow.Cells(23).Value)
                    Dim strsegdesc8 As String = clsCommon.myCstr(grow.Cells(24).Value)
                    Dim strsegcode9 As String = clsCommon.myCstr(grow.Cells(25).Value)
                    Dim strsegdesc9 As String = clsCommon.myCstr(grow.Cells(26).Value)
                    Dim strsegcode10 As String = clsCommon.myCstr(grow.Cells(27).Value)
                    Dim strsegdesc10 As String = clsCommon.myCstr(grow.Cells(28).Value)
                    Dim strGLMainAccountCode As String = clsCommon.myCstr(grow.Cells(29).Value)

                    If String.IsNullOrEmpty(straccountcode) And clsCommon.myLen(straccountcode) > 50 Then
                        Throw New Exception("Account Code has some incorrect values")
                    End If

                    If String.IsNullOrEmpty(strdesc) And clsCommon.myLen(strdesc) > 65 Then
                        Throw New Exception("Account Description has some incorrect values")
                    End If

                    If String.IsNullOrEmpty(strstructcode) And clsCommon.myLen(strstructcode) > 12 Then
                        Throw New Exception("Structure Code has some incorrect values")
                    End If

                    If String.IsNullOrEmpty(strstructdesc) And clsCommon.myLen(strstructdesc) > 60 Then
                        Throw New Exception("Structure Description has some incorrect values")
                    End If

                    If String.IsNullOrEmpty(stracctbalance) And clsCommon.myLen(stracctbalance) > 10 Then
                        Throw New Exception("Account Balance has some incorrect values")
                    End If

                    If String.IsNullOrEmpty(strstatus) And clsCommon.myLen(strstatus) > 1 Then
                        Throw New Exception("status has some incorrect values")
                    End If

                    If String.IsNullOrEmpty(strcontrolacct) And clsCommon.myLen(strcontrolacct) > 1 Then
                        Throw New Exception("Control Account has some incorrect values")
                    End If
                    clsGLAccount.CheckControlAccount(straccountcode, clsCommon.CompairString("Y", strcontrolacct) = CompairStringResult.Equal, trans)

                    If String.IsNullOrEmpty(strautoallocation) And clsCommon.myLen(strautoallocation) > 1 Then
                        Throw New Exception("Auto Allocation has some incorrect values")
                    End If

                    If String.IsNullOrEmpty(strmulticurrency) And clsCommon.myLen(strmulticurrency) > 1 Then
                        Throw New Exception("Multicurrency has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegcode1) > 12 Then
                        Throw New Exception("Segment Code 1 has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegdesc1) > 50 Then
                        Throw New Exception("Segment Description 1 has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegcode2) > 12 Then
                        Throw New Exception("Segment Code 2 has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegdesc2) > 50 Then
                        Throw New Exception("Segment Description 2 has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegcode3) > 12 Then
                        Throw New Exception("Segment Code 3 has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegdesc3) > 50 Then
                        Throw New Exception("Segment Description 3 has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegcode4) > 12 Then
                        Throw New Exception("Segment Code 4 has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegdesc4) > 50 Then
                        Throw New Exception("Segment Description 4 has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegcode5) > 12 Then
                        Throw New Exception("Segment Code 5 has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegdesc5) > 50 Then
                        Throw New Exception("Segment Description 5 has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegcode6) > 12 Then
                        Throw New Exception("Segment Code 6 has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegdesc6) > 50 Then
                        Throw New Exception("Segment Description 6 has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegcode7) > 12 Then
                        Throw New Exception("Segment Code 7 has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegdesc7) > 50 Then
                        Throw New Exception("Segment Description 7 has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegcode8) > 12 Then
                        Throw New Exception("Segment Code 8 has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegdesc8) > 50 Then
                        Throw New Exception("Segment Description 8 has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegcode9) > 12 Then
                        Throw New Exception("Segment Code 9 has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegdesc9) > 50 Then
                        Throw New Exception("Segment Description 9 has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegcode10) > 12 Then
                        Throw New Exception("Segment Code 10 has some incorrect values")
                    End If

                    If clsCommon.myLen(strsegdesc10) > 50 Then
                        Throw New Exception("Segment Description 10 has some incorrect values")
                    End If

                    If clsCommon.myLen(strGLMainAccountCode) > 30 Then
                        Throw New Exception("Main GL Account has some incorrect values")
                    End If

                    Dim sql2 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code = '" + straccountcode + "'"
                    Dim i2 As Integer = CInt(connectSql.RunScalar(trans, sql2))
                    If (i2 = 0) Then
                        connectSql.RunSpTransaction(trans, "SP_TSPL_GL_ACCOUNTS_INSERT", New SqlParameter("@accountcode", straccountcode), New SqlParameter("@description", strdesc), New SqlParameter("@strcode", strstructcode), New SqlParameter("@strdesc", strstructdesc), New SqlParameter("@accbal", stracctbalance), New SqlParameter("@acctype", ""), New SqlParameter("@accgroupcode", ""), New SqlParameter("@accgroupdesc", ""), New SqlParameter("@status", strstatus), New SqlParameter("@controlaccount", strcontrolacct), New SqlParameter("@autoallocation", strautoallocation), New SqlParameter("@rollup", ""), New SqlParameter("@multicurrency", strmulticurrency), New SqlParameter("@accsegcode1", strsegcode1), New SqlParameter("@accsegdesc1", strsegdesc1), New SqlParameter("@accsegcode2", strsegcode2), New SqlParameter("@accsegdesc2", strsegdesc2), New SqlParameter("@accsegcode3", strsegcode3), New SqlParameter("@accsegdesc3", strsegdesc3), New SqlParameter("@accsegcode4", strsegcode4), New SqlParameter("@accsegdesc4", strsegdesc4), New SqlParameter("@accsegcode5", strsegcode5), New SqlParameter("@accsegdesc5", strsegdesc5), New SqlParameter("@accsegcode6", strsegcode6), New SqlParameter("@accsegdesc6", strsegdesc6), New SqlParameter("@accsegcode7", strsegcode7), New SqlParameter("@accsegdesc7", strsegdesc7), New SqlParameter("@accsegcode8", strsegcode8), New SqlParameter("@accsegdesc8", strsegdesc8), New SqlParameter("@accsegcode9", strsegcode9), New SqlParameter("@accsegdesc9", strsegdesc9), New SqlParameter("@accsegcode10", strsegcode10), New SqlParameter("@accsegdesc10", strsegdesc10), New SqlParameter("@closetoseg", ""), New SqlParameter("@closetoaccount", ""), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                    Else
                        connectSql.RunSpTransaction(trans, "SP_TSPL_GL_ACCOUNTS_UPDATE", New SqlParameter("@accountcode", straccountcode), New SqlParameter("@description", strdesc), New SqlParameter("@strcode", strstructcode), New SqlParameter("@strdesc", strstructdesc), New SqlParameter("@accbal", stracctbalance), New SqlParameter("@acctype", ""), New SqlParameter("@accgroupcode", ""), New SqlParameter("@accgroupdesc", ""), New SqlParameter("@status", strstatus), New SqlParameter("@controlaccount", strcontrolacct), New SqlParameter("@autoallocation", strautoallocation), New SqlParameter("@rollup", ""), New SqlParameter("@multicurrency", strmulticurrency), New SqlParameter("@accsegcode1", strsegcode1), New SqlParameter("@accsegdesc1", strsegdesc1), New SqlParameter("@accsegcode2", strsegcode2), New SqlParameter("@accsegdesc2", strsegdesc2), New SqlParameter("@accsegcode3", strsegcode3), New SqlParameter("@accsegdesc3", strsegdesc3), New SqlParameter("@accsegcode4", strsegcode4), New SqlParameter("@accsegdesc4", strsegdesc4), New SqlParameter("@accsegcode5", strsegcode5), New SqlParameter("@accsegdesc5", strsegdesc5), New SqlParameter("@accsegcode6", strsegcode6), New SqlParameter("@accsegdesc6", strsegdesc6), New SqlParameter("@accsegcode7", strsegcode7), New SqlParameter("@accsegdesc7", strsegdesc7), New SqlParameter("@accsegcode8", strsegcode8), New SqlParameter("@accsegdesc8", strsegdesc8), New SqlParameter("@accsegcode9", strsegcode9), New SqlParameter("@accsegdesc9", strsegdesc9), New SqlParameter("@accsegcode10", strsegcode10), New SqlParameter("@accsegdesc10", strsegdesc10), New SqlParameter("@closetoseg", ""), New SqlParameter("@closetoaccount", ""), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                    End If
                    Dim qry As String = "update TSPL_GL_ACCOUNTS set GL_Main_Code='" + strGLMainAccountCode + "'  where Account_Code='" + straccountcode + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Imported Successfully!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub fndstructurecode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndstructurecode.ConnectionString = connectSql.SqlCon()
        'fndstructurecode.Query = "select str_code as [Structure Code], str_description as [Description] from tspl_gl_structure"
        'fndstructurecode.ValueToSelect = "Structure Code"
        'fndstructurecode.ValueToSelect1 = "Description"
        'fndstructurecode.Caption = "Structure Detail"
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
        Dim straccount As String = fndaccount.Value
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
        'If chkcontrolaccount.Checked = True Then
        '    RadPageView1.Controls.Add(RadPageViewPage3)
        'Else : chkcontrolaccount.Checked = False
        '    RadPageView1.Controls.Remove(RadPageViewPage3)
        'End If
    End Sub

    Private Sub chkautoallocation_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkautoallocation.ToggleStateChanged
        If chkautoallocation.Checked = True Then
            RadPageView1.Controls.Add(RadPageViewPage4)
        Else : chkautoallocation.Checked = False
            RadPageView1.Controls.Remove(RadPageViewPage4)
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub CloseForm()
        Try
            Dim qry As String = "select Account_Code as [Account Code],Description from TSPL_GL_ACCOUNTS where Str_Code not in ('ACCSET') "
            qry += " and not Exists (select 1 from TSPL_GL_ROLLUP where TSPL_GL_ROLLUP.account=TSPL_GL_ACCOUNTS.Account_Code)"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If (common.clsCommon.MyMessageBoxShow("There are some GL Accounts that need to be Rollup before  viewing the Trial Balance." + Environment.NewLine + "Do you want ot view those GL Account", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                    Dim frm As New FrmFreeGrid()
                    frm.strFormName = "Pending Account that should be rollup"
                    frm.dt = dt
                    frm.ReportID = "PendingGLACRollup1"
                    frm.ShowDialog()
                End If
                Me.Close()
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub MasterTemplate_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvallocation.CellValueChanged
        If e.Column.Index = 0 Or e.RowIndex >= 0 Then
            Dim str As String = "select description from TSPL_GL_ACCOUNTS where Account_Code = '" + dgvallocation.CurrentRow.Cells(0).Value + "'"
            dt = clsDBFuncationality.GetDataTable(str)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    dgvallocation.CurrentRow.Cells(1).Value = dr(0).ToString()
                Next

            End If

        End If
    End Sub

    Private Sub funinsertallocation()
        Dim account As String
        If btnsave.Text = "Save" Then
            Dim strsegname1 As String = fndaccount.Value
            Dim straccount As String = ""
            For m As Integer = 0 To dgvsegment.Rows.Count - 1
                If String.IsNullOrEmpty(dgvsegment.Rows(m).Cells(1).Value) Then
                Else
                    straccount += "-" + dgvsegment.Rows(m).Cells(1).Value
                End If
            Next
            account = strsegname1 + straccount
        Else
            account = fndaccount.Value
        End If
        Try
            For i As Integer = 0 To dgvallocation.Rows.Count - 1
                connectSql.RunSp("SP_TSPL_GL_ALLOCATIONS_INSERT", New SqlParameter("@mainaccountcode", account), New SqlParameter("@sourcecode", fndsourcecode.Value), New SqlParameter("@account_code", dgvallocation.Rows(i).Cells(0).Value), New SqlParameter("@accountdesc", dgvallocation.Rows(i).Cells(1).Value), New SqlParameter("@reference", dgvallocation.Rows(i).Cells(2).Value), New SqlParameter("@description", dgvallocation.Rows(i).Cells(3).Value), New SqlParameter("@percentage", dgvallocation.Rows(i).Cells(4).Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", clsCommon.GETSERVERDATE()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", clsCommon.GETSERVERDATE()), New SqlParameter("@compcode", companyCode))
            Next
            ' myMessages.insert()
        Catch ex As Exception
            ' myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub funupdateallocation()
        Try
            For i As Integer = 0 To dgvallocation.Rows.Count - 1
                connectSql.RunSp("SP_TSPL_GL_ALLOCATIONS_UPDATE", New SqlParameter("@mainaccountcode", fndaccount.Value), New SqlParameter("@sourcecode", fndsourcecode.Value), New SqlParameter("@account_code", dgvallocation.Rows(i).Cells(0).Value), New SqlParameter("@accountdesc", dgvallocation.Rows(i).Cells(1).Value), New SqlParameter("@reference", dgvallocation.Rows(i).Cells(2).Value), New SqlParameter("@description", dgvallocation.Rows(i).Cells(3).Value), New SqlParameter("@percentage", dgvallocation.Rows(i).Cells(4).Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
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
            connectSql.RunSp("SP_TSPL_GL_ALLOCATIONS_DELETE", New SqlParameter("@mainaccountcode", fndaccount.Value))

            ' myMessages.delete()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub funinsertrollup()
        Try
            'Dim j As Integer
            Dim account As String
            If btnsave.Text = "Save" Then
                Dim strsegname1 As String = fndaccount.Value
                Dim straccount As String = ""
                For m As Integer = 0 To dgvsegment.Rows.Count - 1
                    If String.IsNullOrEmpty(dgvsegment.Rows(m).Cells(1).Value) Then
                    Else
                        straccount += "-" + dgvsegment.Rows(m).Cells(1).Value
                    End If
                Next
                account = strsegname1 + straccount
            Else
                account = fndaccount.Value
            End If



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
            connectSql.RunSp("SP_TSPL_GL_ROLLUP_DELETE", New SqlParameter("@accountcode", fndaccount.Value))

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

    Private Sub dgvrollup_CellValidating(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellValidatingEventArgs)
        'Dim column As GridViewDataColumn = e.Column
        'If TypeOf e.Row Is GridViewRowInfo Then
        '    If column.HeaderText = "Account" And dgvrollup.CurrentRow.Index >= 0 Then
        '        For Each grow As GridViewDataRowInfo In dgvrollup.Rows
        '            If e.RowIndex <> grow.Index Then
        '                If clsCommon.CompairString(clsCommon.myCstr(e.Value), clsCommon.myCstr(grow.Cells(0).Value)) = CompairStringResult.Equal And clsCommon.myLen(e.Value) > 0 Then
        '                    e.Cancel = True
        '                End If
        '            End If
        '        Next
        '    End If
        'End If
    End Sub

    Private Function funsegmentcodevalidation() As Integer
        Try
            Dim strsegment As String = "select Seg_Name2 , Seg_Name3, Seg_Name4, Seg_Name5,Seg_Name6,Seg_Name7,Seg_Name8, Seg_Name9,Seg_Name10 from TSPL_GL_STRUCTURE where Str_Code = '" + fndstructurecode.Value + "'"
            '' added by abhishek as on 12/10/2012
            dt = clsDBFuncationality.GetDataTable(strsegment)
            'Dim j As Integer
            Dim s As String = ""
            Dim kj As String = ""
            Dim l As Integer
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then '' added by abhishek as on 12/10/2012
                For Each dr As DataRow In dt.Rows
                    For i As Integer = 0 To 8
                        If Not String.IsNullOrEmpty(dr(i).ToString()) Then
                            Dim lkj As String = dr(i).ToString()
                            s = connectSql.RunScalar("select Segment_code  from TSPL_GL_SEGMENT_CODE where Segment_name = '" + dr(i).ToString() + "'")
                            s += "+"
                            kj = s + kj
                        End If

                    Next
                Next
            End If

            Dim total As String = fndaccount.Value + "+" + kj
            l = total.Length

            Return l - 1
        Catch ex As Exception
        Finally
        End Try
        Return True
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

    Private Sub funinsertcontrolaccount()
        Try
            Dim account As String
            If btnsave.Text = "Save" Then
                Dim strsegname1 As String = fndaccount.Value
                Dim straccount As String = ""
                For m As Integer = 0 To dgvsegment.Rows.Count - 1
                    If String.IsNullOrEmpty(dgvsegment.Rows(m).Cells(1).Value) Then
                    Else
                        straccount += "-" + dgvsegment.Rows(m).Cells(1).Value
                    End If
                Next
                account = strsegname1 + straccount
            Else
                account = fndaccount.Value
            End If
            Dim strsourcetype As String = ""
            'For i As Integer = 0 To dgvsubledger.Rows.Count - 1
            '    If Not String.IsNullOrEmpty(dgvsubledger.Rows(i).Cells(0).Value.ToString()) Then
            '        strquery = "select  SourceLedger, SourceType , SourceDescription from TSPL_GL_SOURCECODE where SourceCode = '" + dgvsubledger.Rows(i).Cells(0).Value.ToString() + "'"
            '        dt = clsDBFuncationality.GetDataTable(strquery)
            '        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            '            For Each dr As DataRow In dt.Rows
            '                strsourceledger = dr(0)
            '                strsourcetype = dr(1).ToString()
            '                strdesc = dr(2).ToString()
            '            Next
            '        End If
            '        connectSql.RunSp("SP_TSPL_GL_CONTROL_ACC_INSERT", New SqlParameter("@accountcode", account), New SqlParameter("@description", txtdesc.Text), New SqlParameter("@sourcecode", dgvsubledger.Rows(i).Cells(0).Value), New SqlParameter("@sourceledger", strsourceledger), New SqlParameter("@sourcetype", strsourcetype), New SqlParameter("@sourcedesc", strdesc), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            '    End If
            'Next
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub fundeletecontrolaccount()
        Try
            connectSql.RunSp("SP_TSPL_GL_CONTROL_ACC_DELETE", New SqlParameter("@accountcode", fndaccount.Value))
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub funupdatecontrolaccount()
        'Dim trans As SqlTransaction
        'Dim strsourceledger, strsourcetype, strdesc, strquery As String
        'Try
        '    connectSql.OpenConnection()
        '    trans = clsDBFuncationality.GetTransactin()
        '    connectSql.RunSpTransaction(trans, "SP_TSPL_GL_CONTROL_ACC_DELETE", New SqlParameter("@accountcode", fndaccount.Value))
        '    For i As Integer = 0 To dgvsubledger.Rows.Count - 1
        '        If Not String.IsNullOrEmpty(dgvsubledger.Rows(i).Cells(0).Value.ToString()) Then
        '            strquery = "select  SourceLedger, SourceType , SourceDescription from TSPL_GL_SOURCECODE where SourceCode = '" + dgvsubledger.Rows(i).Cells(0).Value.ToString() + "'"
        '            '' added by abhishek as on 12/10/2012
        '            dt = clsDBFuncationality.GetDataTable(strquery)
        '            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '                For Each dr As DataRow In dt.Rows
        '                    strsourceledger = dr(0)
        '                    strsourcetype = dr(1).ToString()
        '                    strdesc = dr(2).ToString()
        '                Next
        '            End If
        '            connectSql.RunSpTransaction(trans, "SP_TSPL_GL_CONTROL_ACC_INSERT", New SqlParameter("@accountcode", fndaccount.Value), New SqlParameter("@description", txtdesc.Text), New SqlParameter("@sourcecode", dgvsubledger.Rows(i).Cells(0).Value), New SqlParameter("@sourceledger", strsourceledger), New SqlParameter("@sourcetype", strsourcetype), New SqlParameter("@sourcedesc", strdesc), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
        '        End If
        '    Next
        '    trans.Commit()
        'Catch ex As Exception
        '    trans.Rollback()
        '    myMessages.myExceptions(ex)

        'End Try
    End Sub

    Private Sub dgvsubledger_CellValidating(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellValidatingEventArgs) Handles dgvsubledger.CellValidating
        'Dim column As GridViewDataColumn = e.Column
        'If TypeOf e.Row Is GridViewRowInfo Then
        '    If column.HeaderText = "Sub Ledger" Then
        '        For Each grow As GridViewDataRowInfo In dgvsubledger.Rows
        '            If e.RowIndex <> grow.Index Then
        '                If e.Value = grow.Cells(0).Value Then
        '                    e.Cancel = True
        '                End If
        '            End If
        '        Next
        '    End If
        'End If
    End Sub

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
            sr = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where segment_code = '" + dgvsegment.CurrentRow.Cells(1).Value + "'")
            dgvsegment.CurrentRow.Cells(2).Value = sr
        End If

    End Sub

    Private Sub funupdatecontrolacctrollup()
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub funupdatecontrolacctallocation()
        Dim trans As SqlTransaction
        Try
            connectSql.OpenConnection()
            trans = clsDBFuncationality.GetTransactin()



        Catch ex As Exception

        End Try
    End Sub

    Private Sub funupdaterollupallocation()
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub funupdatecontrolacctrollupallocation()
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub MasterTemplate_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)
        ''If TypeOf Me.dgvrollup.CurrentColumn Is GridViewTextBoxColumn Then
        ''    Dim editor As RadTextBoxEditorElement = DirectCast(Me.dgvrollup.ActiveEditor, RadTextBoxEditorElement)
        ''    'editor.AutoSizeDropDownToBestFit = True
        ''    editor.EditorControl.MasterTemplate.BestFitColumns()
        ''    'editor.DropDownStyle = RadDropDownStyle.DropDown
        ''    editor.AutoFilter = True
        ''    If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
        ''        Dim autoFilter As FilterDescriptor = New FilterDescriptor("description", FilterOperator.StartsWith, "")
        ''        autoFilter.IsFilterEditor = True
        ''        editor.EditorControl.FilterDescriptors.Add(autoFilter)
        ''    End If
        ''End If
    End Sub

    Private Sub dgvrollup_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs)
        ''Dim straccount As String = "SELECT ACCOUNT_CODE AS [Account Code], DESCRIPTION AS [Description] from tspl_gl_accounts where Account_Code <>'" + fndaccount.txtValue.Text.Trim() + "'"
        ''ds = connectSql.RunSQLReturnDS(straccount)
        ''Dim dtaccount1 As GridViewTextBoxColumn = TryCast(dgvrollup.Columns(0), GridViewTextBoxColumn)
        'dtaccount1.DataSource = ds.Tables(0)
        'dtaccount1.ValueMember = "Account Code"

    End Sub

    Private Sub RadGroupBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox2.Click

    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndaccount.Value = "" Then
            funfirst()
        Else
            funnext()
        End If

    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        funlast()
    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndaccount.Value = "" Then
            funfirst()
        Else
            funprevious()
        End If
    End Sub

    Private Sub RadButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funfirst()
    End Sub

    Private Sub Basic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Basic.Click
        funimport()
    End Sub

    Private Sub BasicEx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BasicEx.Click
        Dim str As String = "select Account_Code as [Account Code], Description as [Description], Str_Code as [Structure Code], Str_Description as [Structure Description], Account_Balance as [Account Balance], Account_Type as [Account Type], Account_Group_Code as [Account Group Code], Account_Group_Desc as [Account Group Code Description], Status as [Status], ControlAccount as [Control Account], autoallocation as [Auto Allocation], Rollup as [Rollup], multicurrency as [Multicurrency], account_seg_code1 as [Segment Code 1], account_seg_desc1 as [Segment Description 1], account_seg_code2 as [Segment Code 2], account_seg_desc2 as [Segment Description 2],Account_Seg_Code3 as [Segment Code 3],Account_Seg_Desc3 as [Segment Description 3],Account_Seg_Code4 as [Segment Code 4],Account_Seg_Desc4 as [Segment Description 4],Account_Seg_Code5 as [Segment Code 5],Account_Seg_Desc5 as [Segment Description 5] ,Account_Seg_Code6 as [Segment Code 6],Account_Seg_Desc6 as [Segment Description 6],Account_Seg_Code7 as [Segment Code 7],Account_Seg_Desc7 as [Segment Description 7],Account_Seg_Code8  as [Segment Code 8],Account_Seg_Desc8 as [Segment Description 8],Account_Seg_Code9 as [Segment Code 9],Account_Seg_Desc9 as [Segment Description 9],Account_Seg_Code10 as [Segment Code 10],Account_Seg_Desc10 as [Segment Description 10], Close_To_Seg as [Close To Segment], Close_To_Acct as [Close To Account], Created_By as [Created By], Created_Date as [Created Date], Modify_By as [Modify By], Modify_Date as [Modify Date], Comp_Code as [Company Code]  from TSPL_GL_ACCOUNTS "
        ListImpExpColumnsMandatory = New List(Of String)({"Account Code", "Description", "Structure Code", "Structure Description", "Account Balance", "Status", "Control Account", "Auto Allocation", "Multicurrency"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Account Code"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub Rollup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rollup.Click
        funrollupImpor()
    End Sub

    Private Sub RollUpex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RollUpex.Click
        funexportRollup()
    End Sub

    Public Sub funexportRollup()
        'Dim str As String = "select Account_Code as [Account Code], Description as [Description], account as [RollUp Account Code], desct as [RollUp Account Description], RollUp as [RollUp], Accountgroup as [Account Group], Accounttype as [Account Type], Status as [Status], account_balance as [Account Balance], multicurrency as [Multicurrency] from tspl_gl_rollup"
        'Dim str As String = "select Account_Code as [Account Code], Description as [Description], account as [RollUp Account Code], desct as [RollUp Account Description], RollUp as [RollUp], Accountgroup as [Account Group], Accounttype as [Account Type],  case when Status='Y' Then 'Active' else 'Inactive' end as [Status], account_balance as [Account Balance], multicurrency as [Multicurrency] from tspl_gl_rollup "
        Dim str As String = "select Account_Code as [Account Code], Description as [Description], account as [RollUp Account Code], desct as [RollUp Account Description], RollUp as [RollUp], Accountgroup as [Account Group],g.Account_Group_Desc as [Account Group Description], Accounttype as [Account Type],  case when Status='Y' Then 'Active' else 'Inactive' end as [Status], account_balance as [Account Balance], multicurrency as [Multicurrency] from tspl_gl_rollup r left outer join TSPL_ACCOUNT_GROUPS g on r.accountgroup=g.Account_Group_Code"
        ListImpExpColumnsMandatory = New List(Of String)({"Account Code", "RollUp Account Code", "Account Group"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Account Code", "Account Group"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID + "RollUp")
    End Sub

    Public Sub funrollupImpor()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)

        If transportSql.importExcel(gv, "Account Code", "Description", "RollUp Account Code", "RollUp Account Description", "RollUp", "Account Group", "Account Group Description", "Account Type", "Status", "Account Balance", "Multicurrency") Then
            Dim trans As SqlTransaction = Nothing
            Try
                'connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim accountcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If clsCommon.myLen(accountcode) <= 0 Then
                        Continue For
                    End If


                    Dim acc1 As String = "select count(*) from tspl_gl_accounts where account_code='" + accountcode + "'"
                    Dim no As Integer = CInt(connectSql.RunScalar(trans, acc1))
                    If no = 0 Then
                        Throw New Exception("This  " + accountcode + "  Account does not exist")
                    End If

                    If clsCommon.myLen(accountcode) > 50 Then
                        Throw New Exception("Check the lenght of Account No.")
                    End If

                    Dim desc1 As String = clsCommon.myCstr(grow.Cells(1).Value)
                    Dim desc As String = desc1.Replace("'", "''")
                    If clsCommon.myLen(desc) > 200 Then
                        Throw New Exception("Check the lenght of Description")
                    End If

                    Dim RollupCode As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If clsCommon.myLen(RollupCode) > 50 Then
                        Throw New Exception("Check the lenght of RollUp Account Code")
                    End If

                    Dim acc2 As String = "select count(*) from tspl_gl_accounts where account_code='" + RollupCode + "'"
                    Dim no2 As Integer = CInt(connectSql.RunScalar(trans, acc1))
                    If no2 = 0 Then
                        Throw New Exception("This  " + RollupCode + "  Account does not exist")
                    End If

                    Dim rollupDes1 As String = clsCommon.myCstr(grow.Cells(3).Value)
                    Dim rollupDes As String = rollupDes1.Replace("'", "''")
                    If clsCommon.myLen(rollupDes) > 200 Then
                        Throw New Exception("Check the lenght of Roll Up Account Description")
                    End If

                    Dim rollup As String = clsCommon.myCstr(grow.Cells(4).Value)

                    If (rollup = "Y" Or rollup = "N") Then
                    Else
                        Throw New Exception("Value for Inactive should be  Y or N")
                    End If

                    Dim AccountGroup As String = clsCommon.myCstr(grow.Cells(5).Value)
                    If clsCommon.myLen(AccountGroup) > 12 Then
                        Throw New Exception("Check the lenght of Account Group")
                    End If

                    Dim accounttype As String = clsCommon.myCstr(grow.Cells(7).Value)
                    If clsCommon.myLen(accounttype) > 20 Then
                        Throw New Exception("Check the lenght of Account Type ")
                    End If

                    Dim statusg As String = clsCommon.myCstr(grow.Cells(8).Value)
                    Dim status As String
                    If statusg = "Active" Then
                        status = "Y"
                    Else
                        status = "I"
                    End If
                    'If (status = "Y" Or status = "I") Then
                    'Else
                    '    common.clsCommon.MyMessageBoxShow("Value for Status should be  Y or N")
                    '    trans.Rollback()
                    '    Exit Sub
                    'End If
                    Dim accbalance As String = clsCommon.myCstr(grow.Cells(9).Value)
                    If clsCommon.myLen(accbalance) > 10 Then
                        Throw New Exception("Check the lenght of Account Balance")
                    End If

                    Dim multi As String = clsCommon.myCstr(grow.Cells(10).Value)
                    If (multi = "Y" Or multi = "N") Then
                    Else
                        Throw New Exception("Value for MultiCurrency should be  Y or N")
                    End If

                    If String.IsNullOrEmpty(accountcode) Then
                        Throw New Exception("GL Account can not be blank")
                    End If

                    If String.IsNullOrEmpty(RollupCode) Then
                        Throw New Exception(" Roll Up Account can not be blank")
                    End If

                    'Dim sql1 As String = "select count(*) from tspl_gl_rollup  where  Account_code='" + accountcode + "' and account='" + RollupCode + "'"
                    Dim sql1 As String = "delete from tspl_gl_rollup where account='" + RollupCode + "' "
                    clsDBFuncationality.ExecuteNonQuery(sql1, trans)
                    'If (i = 0) Then
                    Dim strcmd As String = "Insert into tspl_gl_rollup(Account_Code,description,account,desct,rollup,accountgroup,accounttype,status,account_balance,multicurrency,created_by,created_date,modify_by,modify_date,comp_code) values('" + accountcode + "','" + desc + "','" + RollupCode + "','" + rollupDes + "','" + rollup + "','" + AccountGroup + "','" + accounttype + "','" + status + "','" + accbalance + "','" + multi + "','" + userCode + "','" + connectSql.serverDate(trans) + "','" + userCode + "','" + connectSql.serverDate(trans) + "','" + companyCode + "')"
                    connectSql.RunSqlTransaction(trans, strcmd)
                    Dim stracc As String = "update tspl_gl_accounts set RollUp='Y' where account_code='" + accountcode + "' "
                    connectSql.RunSqlTransaction(trans, stracc)

                    'Else

                    '    Dim strcmd As String = "Update  tspl_gl_rollup set Account_Code='" + accountcode + "',description='" + desc + "',account='" + RollupCode + "',desct='" + rollupDes + "',rollup='" + rollup + "' ,accountgroup='" + AccountGroup + "',accounttype='" + accounttype + "' ,status='" + status + "', account_balance='" + accbalance + "',multicurrency='" + multi + "' ,modify_by='" + userCode + "',modify_date='" + connectSql.serverDate(trans) + "',comp_code='" + userCode + "' where Account_code='" + accountcode + "' and account='" + RollupCode + "'"
                    '    connectSql.RunSqlTransaction(trans, strcmd)
                    '    Dim stracc As String = "update tspl_gl_accounts set RollUp='Y' where account_code='" + accountcode + "' "
                    '    connectSql.RunSqlTransaction(trans, stracc)
                    'End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub fndstructurecode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndstructurecode._MYValidating

        'Dim qry As String = "select str_code as [StructureCode], str_description as [Description] from tspl_gl_structure"
        'fndstructurecode.Value = clsCommon.ShowSelectForm("GL_str_code", qry, "StructureCode", "", fndstructurecode.Value, "", isButtonClicked)
        fndstructurecode.Value = clsGLStructure.getFinder("", fndstructurecode.Value, isButtonClicked)
        txtstrdesc.Text = clsDBFuncationality.getSingleValue("select  str_description  from tspl_gl_structure where str_code='" + fndstructurecode.Value + "'")
        strchang()
    End Sub

    Private Sub fndsourcecode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndsourcecode._MYValidating
        'Dim qry As String = "select sourcecode as [SourceCode], sourcedescription as [Description] from TSPL_GL_SOURCECODE  "
        'fndsourcecode.Value = clsCommon.ShowSelectForm("SOU_COde", qry, "SourceCode", "SourceLedger='GL'", fndsourcecode.Value, "", isButtonClicked)
        fndsourcecode.Value = clsGLSourceCode.getFinder("SourceLedger='GL'", fndsourcecode.Value, isButtonClicked)

    End Sub

    Private Sub ExportRollupSorting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportRollupSorting.Click
        Dim str As String = "Select Account_Code as [Account Code], Description, '' as [Seq No]  from TSPL_GL_ACCOUNTS "
        Dim whrCls As String = "and Rollup='Y' "

        transportSql.ExporttoExcel(str, whrCls, Me)
    End Sub

    Private Sub ImportRollupSeq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportRollupSeq.Click
        ImportrollupGLAccSeq()
    End Sub

    Public Sub ImportrollupGLAccSeq()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)

        If transportSql.importExcel(gv, "Account Code", "Description", "Seq No") Then
            Try
                clsCommon.ProgressBarShow()
                Dim LineNo As Integer = 1
                For Each grow As GridViewRowInfo In gv.Rows
                    LineNo = LineNo + 1
                    Dim accountcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim acc1 As String = "select count(*) from tspl_gl_accounts where account_code='" + accountcode + "'"
                    Dim no As Integer = clsDBFuncationality.getSingleValue(acc1)
                    If no = 0 Then
                        Throw New Exception("The Account Code " + accountcode + " at the Line No " + clsCommon.myCstr(LineNo) + " does not exist")
                    End If

                    Dim desc As String = clsDBFuncationality.getSingleValue("Select Description  from TSPL_GL_ACCOUNTS Where Rollup='Y' AND Account_Code='" + accountcode + "'")
                    If clsCommon.myLen(grow.Cells(2).Value) > 0 Then
                        Dim i As Integer = clsDBFuncationality.getSingleValue("Select COUNT(*)  from TSPL_ROLLUP_GL_ACCOUNTS Where Account_Code='" + accountcode + "' ")
                        If (i = 0) Then
                            clsDBFuncationality.ExecuteNonQuery("Insert Into TSPL_ROLLUP_GL_ACCOUNTS Values ('" + accountcode + "', '" + desc + "', " + clsCommon.myCstr(grow.Cells(2).Value) + " )")
                        Else
                            clsDBFuncationality.ExecuteNonQuery("Update TSPL_ROLLUP_GL_ACCOUNTS Set Seq_No=" + clsCommon.myCstr(grow.Cells(2).Value) + " Where Account_Code='" + accountcode + "'")
                        End If
                    End If
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub fndaccount__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndaccount._MYNavigator
        Dim whrcls As String = "Account_Code in (" + objCommonVar.strCurrUserGLAccount + ")"

        Dim qst As String = "select Account_Code as AccountCode , Description  from TSPL_GL_ACCOUNTS where   2=2 "

        Select Case NavType

            Case NavigatorType.Current
                qst += "and TSPL_GL_ACCOUNTS.Account_Code in ('" + fndaccount.Value + "' and " + IIf(clsCommon.myLen(objCommonVar.strCurrUserGLAccount) > 0, whrcls, "2=2") + ") "
            Case NavigatorType.Next
                qst += "and TSPL_GL_ACCOUNTS.Account_Code in (select min(Account_Code) from TSPL_GL_ACCOUNTS where Account_Code >'" + fndaccount.Value + "' and " + IIf(clsCommon.myLen(objCommonVar.strCurrUserGLAccount) > 0, whrcls, "2=2") + ")"
            Case NavigatorType.First
                qst += "and TSPL_GL_ACCOUNTS.Account_Code in (select MIN(Account_Code) from TSPL_GL_ACCOUNTS where  " + IIf(clsCommon.myLen(objCommonVar.strCurrUserGLAccount) > 0, whrcls, "2=2") + ") "

            Case NavigatorType.Last
                qst += "and TSPL_GL_ACCOUNTS.Account_Code in (select Max(Account_Code) from TSPL_GL_ACCOUNTS where  " + IIf(clsCommon.myLen(objCommonVar.strCurrUserGLAccount) > 0, whrcls, "2=2") + ") "
            Case NavigatorType.Previous
                qst += "and TSPL_GL_ACCOUNTS.Account_Code in (select max(Account_Code) from TSPL_GL_ACCOUNTS where Account_Code<'" + fndaccount.Value + "' and " + IIf(clsCommon.myLen(objCommonVar.strCurrUserGLAccount) > 0, whrcls, "2=2") + " )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        ' Return clsDBFuncationality.GetDataTable(qst)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            txtdesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
            fndaccount.Value = clsCommon.myCstr(dt.Rows(0)("AccountCode"))
            accountTextchanged()
        End If

    End Sub

    Private Sub fndaccount__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndaccount._MYValidating
        '' Added By Abhishek As on 22/8/2012 Beacuse Single account was not creating
        Try

        Dim str As String = "select count(*) from TSPL_GL_ACCOUNTS   where  Account_Code ='" + fndaccount.Value + "' "

        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))

        If no = 0 Then
            fndaccount.MyReadOnly = False
        Else
            fndaccount.MyReadOnly = True
        End If
        Dim op As Integer = clsDBFuncationality.getSingleValue("select Seg_Length  from TSPL_GL_SEGMENT where Seg_No = '1'")
        ' Ticket No : TEC/19/06/19-000555 by prabhakar
        'If clsCommon.myLen(fndaccount.Value) <> op AndAlso Not isButtonClicked Then
        '    common.clsCommon.MyMessageBoxShow("Please insert Account Code of minimum length '" + clsCommon.myCstr(op) + "' ")
        '    fndaccount.Focus()
        '    Exit Sub
        'End If
        ''----codes ends here ----
        'If fndaccount.MyReadOnly OrElse isButtonClicked Then '' This check also done by abhishek For above mentioned reason

        Dim whrcls As String = "Account_Code in (" + objCommonVar.strCurrUserGLAccount + ")"
        'Dim QRy As String = "select Account_Code as AccountCode , Description,Str_Code as [Structure Code],Str_Description as [Structure Description],Account_Group_Code as [Account Group Code],Account_Group_Desc as [Account Group Description],ControlAccount,(select case when MIN(TSPL_GL_ROLLUP.Account_Code)=MAX(TSPL_GL_ROLLUP.Account_Code) then MIN(TSPL_GL_ROLLUP.Account_Code) else MIN(TSPL_GL_ROLLUP.Account_Code)+'*' end as RollupAccount from TSPL_GL_ROLLUP where TSPL_GL_ROLLUP.account=TSPL_GL_ACCOUNTS.Account_Code) as [Rollup Account]  from TSPL_GL_ACCOUNTS"

        'fndaccount.Value = clsCommon.ShowSelectForm("GlAccountFinder", QRy, "AccountCode", IIf(clsCommon.myLen(objCommonVar.strCurrUserGLAccount) > 0, whrcls, ""), fndaccount.Value, "", isButtonClicked)
        fndaccount.Value = clsGLAccount.getFinder(IIf(clsCommon.myLen(objCommonVar.strCurrUserGLAccount) > 0, whrcls, ""), fndaccount.Value, isButtonClicked)
        txtdesc.Text = clsDBFuncationality.getSingleValue("select Description from tspl_gl_accounts where account_code='" + fndaccount.Value + "'")

        accountTextchanged()
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Function strcCodeLength(ByVal strCode As String) As Integer
        Dim length As Integer
        Dim StrcCode() As String
        Dim str As String
        Dim query As String = "select seg_name1,seg_name2,seg_name3,seg_name4,seg_name5,seg_name6,seg_name7,seg_name8,seg_name9,seg_name10 from TSPL_GL_STRUCTURE where Str_Code='" + fndstructurecode.Value + "'"
        dt = clsDBFuncationality.GetDataTable(query)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows

                str = dr(0).ToString()
                Dim i As Integer
                For i = 0 To 9
                    If String.IsNullOrEmpty(dr(i).ToString()) Then
                        Exit For
                    Else
                        If i = 0 Then


                        Else
                            str = str + "+" + dr(i).ToString()
                            StrcCode = str.Split("+")
                            length = StrcCode.Length()
                        End If
                    End If
                Next
            Next
        End If
        Return length
    End Function



    Private Sub RadMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem5.Click
        Dim str As String = "select Account_Code as [Account Code], Description as [Description], Str_Code as [Structure Code],Str_Description as [Structure Description], Account_Balance as [Account Balance], Status as [Status], ControlAccount as [Control Account], autoallocation as [Auto Allocation],multicurrency as [Multicurrency], account_seg_code1 as [Segment Code 1], account_seg_desc1 as [Segment Description 1], account_seg_code2 as [Segment Code 2], account_seg_desc2 as [Segment Description 2],Account_Seg_Code3 as [Segment Code 3],Account_Seg_Desc3 as [Segment Description 3],Account_Seg_Code4 as [Segment Code 4],Account_Seg_Desc4 as [Segment Description 4],Account_Seg_Code5 as [Segment Code 5],Account_Seg_Desc5 as [Segment Description 5] ,Account_Seg_Code6 as [Segment Code 6],Account_Seg_Desc6 as [Segment Description 6],Account_Seg_Code7 as [Segment Code 7],Account_Seg_Desc7 as [Segment Description 7],Account_Seg_Code8  as [Segment Code 8],Account_Seg_Desc8 as [Segment Description 8],Account_Seg_Code9 as [Segment Code 9],Account_Seg_Desc9 as [Segment Description 9],Account_Seg_Code10 as [Segment Code 10],Account_Seg_Desc10 as [Segment Description 10],Close_To_Seg as [Close To Segment], Close_To_Acct as [Close To Account], GL_Main_Code from TSPL_GL_ACCOUNTS"
        ListImpExpColumnsMandatory = New List(Of String)({"Account Code", "Description", "Structure Code", "Structure Description", "Account Balance", "Status", "Auto Allocation", "Multicurrency", "GL_Main_Code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Account Code"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID + "GL_ACCOUNTS")
    End Sub

    Private Sub RadMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem6.Click
        funimportCombined()

    End Sub

    Private Sub funimportCombined()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Account Code", "Description", "Structure Code", "Structure Description", "Account Balance", "Status", "Control Account", "Auto Allocation", "Multicurrency", "Segment Code 1", "Segment Description 1", "Segment Code 2", "Segment Description 2", "Segment Code 3", "Segment Description 3", "Segment Code 4", "Segment Description 4", "Segment Code 5", "Segment Description 5", "Segment Code 6", "Segment Description 6", "Segment Code 7", "Segment Description 7", "Segment Code 8", "Segment Description 8", "Segment Code 9", "Segment Description 9", "Segment Code 10", "Segment Description 10", "Close To Segment", "Close To Account", "GL_Main_Code") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                Dim lineNo As String = 0
                For Each grow As GridViewRowInfo In gv.Rows
                    lineNo = clsCommon.myCstr(grow.Index + 2)
                    Dim straccountcode As String = clsCommon.myCstr(grow.Cells("Account Code").Value)
                    If clsCommon.myLen(straccountcode) > 0 Then
                        Dim strdesc As String = clsCommon.myCstr(grow.Cells("Description").Value)
                        Dim strstructcode As String = clsCommon.myCstr(grow.Cells("Structure Code").Value)
                        Dim strstructdesc As String = clsCommon.myCstr(grow.Cells("Structure Description").Value)
                        Dim stracctbalance As String = clsCommon.myCstr(grow.Cells("Account Balance").Value)
                        Dim strstatus As String = clsCommon.myCstr(grow.Cells("Status").Value)
                        Dim strcontrolacct As String = clsCommon.myCstr(grow.Cells("Control Account").Value)
                        Dim strautoallocation As String = clsCommon.myCstr(grow.Cells("Auto Allocation").Value)
                        Dim strmulticurrency As String = clsCommon.myCstr(grow.Cells("Multicurrency").Value)
                        Dim strsegcode1 As String = clsCommon.myCstr(grow.Cells("Segment Code 1").Value)
                        Dim strsegdesc1 As String = clsCommon.myCstr(grow.Cells("Segment Description 1").Value)
                        Dim strsegcode2 As String = clsCommon.myCstr(grow.Cells("Segment Code 2").Value)
                        Dim strsegdesc2 As String = clsCommon.myCstr(grow.Cells("Segment Description 2").Value)
                        Dim strsegcode3 As String = clsCommon.myCstr(grow.Cells("Segment Code 3").Value)
                        Dim strsegdesc3 As String = clsCommon.myCstr(grow.Cells("Segment Description 3").Value)
                        Dim strsegcode4 As String = clsCommon.myCstr(grow.Cells("Segment Code 4").Value)
                        Dim strsegdesc4 As String = clsCommon.myCstr(grow.Cells("Segment Description 4").Value)
                        Dim strsegcode5 As String = clsCommon.myCstr(grow.Cells("Segment Code 5").Value)
                        Dim strsegdesc5 As String = clsCommon.myCstr(grow.Cells("Segment Description 5").Value)
                        Dim strsegcode6 As String = clsCommon.myCstr(grow.Cells("Segment Code 6").Value)
                        Dim strsegdesc6 As String = clsCommon.myCstr(grow.Cells("Segment Description 6").Value)
                        Dim strsegcode7 As String = clsCommon.myCstr(grow.Cells("Segment Code 7").Value)
                        Dim strsegdesc7 As String = clsCommon.myCstr(grow.Cells("Segment Description 7").Value)
                        Dim strsegcode8 As String = clsCommon.myCstr(grow.Cells("Segment Code 8").Value)
                        Dim strsegdesc8 As String = clsCommon.myCstr(grow.Cells("Segment Description 8").Value)
                        Dim strsegcode9 As String = clsCommon.myCstr(grow.Cells("Segment Code 9").Value)
                        Dim strsegdesc9 As String = clsCommon.myCstr(grow.Cells("Segment Description 9").Value)
                        Dim strsegcode10 As String = clsCommon.myCstr(grow.Cells("Segment Code 10").Value)
                        Dim strsegdesc10 As String = clsCommon.myCstr(grow.Cells("Segment Description 10").Value)
                        Dim strclosetoseg As String = clsCommon.myCstr(grow.Cells("Close To Segment").Value)
                        Dim strclosetoacct As String = clsCommon.myCstr(grow.Cells("Close To Account").Value)
                        Dim GLMainAccountCode As String = clsCommon.myCstr(grow.Cells("GL_Main_Code").Value)
                         

                        If String.IsNullOrEmpty(straccountcode) And clsCommon.myLen(straccountcode) > 50 Then
                            Throw New Exception("Account Code has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If String.IsNullOrEmpty(strdesc) And clsCommon.myLen(strdesc) > 100 Then
                            Throw New Exception("Account Description has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If String.IsNullOrEmpty(strstructcode) And clsCommon.myLen(strstructcode) > 12 Then
                            Throw New Exception("Structure Code has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If String.IsNullOrEmpty(strstructdesc) And clsCommon.myLen(strstructdesc) > 60 Then
                            Throw New Exception("Structure Description has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If String.IsNullOrEmpty(stracctbalance) And clsCommon.myLen(stracctbalance) > 10 Then
                            Throw New Exception("Account Balance has some incorrect values on Line '" + lineNo + "'")
                        End If


                        If String.IsNullOrEmpty(strstatus) And clsCommon.myLen(strstatus) > 1 Then
                            Throw New Exception("status has some incorrect values on Line '" + lineNo + "'")
                        End If

                        'If String.IsNullOrEmpty(strcontrolacct) And clsCommon.myLen(strcontrolacct) > 1 Then
                        '    Throw New Exception("Control Account has some incorrect values on Line '" + lineNo + "'")
                        'End If
                        'clsGLAccount.CheckControlAccount(straccountcode, clsCommon.CompairString("Y", strcontrolacct) = CompairStringResult.Equal, trans)

                        If String.IsNullOrEmpty(strautoallocation) And clsCommon.myLen(strautoallocation) > 1 Then
                            Throw New Exception("Auto Allocation has some incorrect values on Line '" + lineNo + "'")
                        End If


                        If String.IsNullOrEmpty(strmulticurrency) And clsCommon.myLen(strmulticurrency) > 1 Then
                            Throw New Exception("Multicurrency has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegcode1) > 12 Then
                            Throw New Exception("Segment Code 1 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegdesc1) > 100 Then
                            Throw New Exception("Segment Description 1 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegcode2) > 12 Then
                            Throw New Exception("Segment Code 2 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegdesc2) > 50 Then
                            Throw New Exception("Segment Description 2 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegcode3) > 12 Then
                            Throw New Exception("Segment Code 3 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegdesc3) > 50 Then
                            Throw New Exception("Segment Description 3 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegcode4) > 12 Then
                            Throw New Exception("Segment Code 4 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegdesc4) > 50 Then
                            Throw New Exception("Segment Description 4 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegcode5) > 12 Then
                            Throw New Exception("Segment Code 5 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegdesc5) > 50 Then
                            Throw New Exception("Segment Description 5 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegcode6) > 12 Then
                            Throw New Exception("Segment Code 6 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegdesc6) > 50 Then
                            Throw New Exception("Segment Description 6 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegcode7) > 12 Then
                            Throw New Exception("Segment Code 7 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegdesc7) > 50 Then
                            Throw New Exception("Segment Description 7 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegcode8) > 12 Then
                            Throw New Exception("Segment Code 8 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegdesc8) > 50 Then
                            Throw New Exception("Segment Description 8 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegcode9) > 12 Then
                            Throw New Exception("Segment Code 9 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegdesc9) > 50 Then
                            Throw New Exception("Segment Description 9 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegcode10) > 12 Then
                            Throw New Exception("Segment Code 10 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strsegdesc10) > 50 Then
                            Throw New Exception("Segment Description 10 has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strclosetoseg) > 12 Then
                            Throw New Exception("Close To Segment has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(strclosetoacct) > 50 Then
                            Throw New Exception("Close To Account has some incorrect values on Line '" + lineNo + "'")
                        End If

                        If clsCommon.myLen(GLMainAccountCode) <= 0 Then
                            Throw New Exception("GL Main Account not found on Line '" + lineNo + "'")
                        End If
                        GLMainAccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Main_GL_Account from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + GLMainAccountCode + "'", trans))
                        If clsCommon.myLen(GLMainAccountCode) <= 0 Then
                            Throw New Exception("GL Main Account is not exist in master on Line '" + lineNo + "'")
                        End If
                        strcontrolacct = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select case when  IsControlAcct = 1 then 'Y' else 'N' end IsControlAcct from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account = '" + GLMainAccountCode + "'", trans))
                        If String.IsNullOrEmpty(strcontrolacct) And clsCommon.myLen(strcontrolacct) > 1 Then
                            Throw New Exception("Control Account has some incorrect values on Line '" + lineNo + "'")
                        End If
                        Dim sql2 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code = '" + straccountcode + "'"
                        Dim i2 As Integer = CInt(connectSql.RunScalar(trans, sql2))
                        If (i2 = 0) Then
                            connectSql.RunSpTransaction(trans, "SP_TSPL_GL_ACCOUNTS_INSERT", New SqlParameter("@accountcode", straccountcode), New SqlParameter("@description", strdesc), New SqlParameter("@strcode", strstructcode), New SqlParameter("@strdesc", strstructdesc), New SqlParameter("@accbal", stracctbalance), New SqlParameter("@acctype", ""), New SqlParameter("@accgroupcode", ""), New SqlParameter("@accgroupdesc", ""), New SqlParameter("@status", strstatus), New SqlParameter("@controlaccount", strcontrolacct), New SqlParameter("@autoallocation", strautoallocation), New SqlParameter("@rollup", "N"), New SqlParameter("@multicurrency", strmulticurrency), New SqlParameter("@accsegcode1", strsegcode1), New SqlParameter("@accsegdesc1", strsegdesc1), New SqlParameter("@accsegcode2", strsegcode2), New SqlParameter("@accsegdesc2", strsegdesc2), New SqlParameter("@accsegcode3", strsegcode3), New SqlParameter("@accsegdesc3", strsegdesc3), New SqlParameter("@accsegcode4", strsegcode4), New SqlParameter("@accsegdesc4", strsegdesc4), New SqlParameter("@accsegcode5", strsegcode5), New SqlParameter("@accsegdesc5", strsegdesc5), New SqlParameter("@accsegcode6", strsegcode6), New SqlParameter("@accsegdesc6", strsegdesc6), New SqlParameter("@accsegcode7", strsegcode7), New SqlParameter("@accsegdesc7", strsegdesc7), New SqlParameter("@accsegcode8", strsegcode8), New SqlParameter("@accsegdesc8", strsegdesc8), New SqlParameter("@accsegcode9", strsegcode9), New SqlParameter("@accsegdesc9", strsegdesc9), New SqlParameter("@accsegcode10", strsegcode10), New SqlParameter("@accsegdesc10", strsegdesc10), New SqlParameter("@closetoseg", strclosetoseg), New SqlParameter("@closetoaccount", strclosetoacct), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                        Else
                            connectSql.RunSpTransaction(trans, "SP_TSPL_GL_ACCOUNTS_UPDATE", New SqlParameter("@accountcode", straccountcode), New SqlParameter("@description", strdesc), New SqlParameter("@strcode", strstructcode), New SqlParameter("@strdesc", strstructdesc), New SqlParameter("@accbal", stracctbalance), New SqlParameter("@acctype", ""), New SqlParameter("@accgroupcode", ""), New SqlParameter("@accgroupdesc", ""), New SqlParameter("@status", strstatus), New SqlParameter("@controlaccount", strcontrolacct), New SqlParameter("@autoallocation", strautoallocation), New SqlParameter("@rollup", "N"), New SqlParameter("@multicurrency", strmulticurrency), New SqlParameter("@accsegcode1", strsegcode1), New SqlParameter("@accsegdesc1", strsegdesc1), New SqlParameter("@accsegcode2", strsegcode2), New SqlParameter("@accsegdesc2", strsegdesc2), New SqlParameter("@accsegcode3", strsegcode3), New SqlParameter("@accsegdesc3", strsegdesc3), New SqlParameter("@accsegcode4", strsegcode4), New SqlParameter("@accsegdesc4", strsegdesc4), New SqlParameter("@accsegcode5", strsegcode5), New SqlParameter("@accsegdesc5", strsegdesc5), New SqlParameter("@accsegcode6", strsegcode6), New SqlParameter("@accsegdesc6", strsegdesc6), New SqlParameter("@accsegcode7", strsegcode7), New SqlParameter("@accsegdesc7", strsegdesc7), New SqlParameter("@accsegcode8", strsegcode8), New SqlParameter("@accsegdesc8", strsegdesc8), New SqlParameter("@accsegcode9", strsegcode9), New SqlParameter("@accsegdesc9", strsegdesc9), New SqlParameter("@accsegcode10", strsegcode10), New SqlParameter("@accsegdesc10", strsegdesc10), New SqlParameter("@closetoseg", strclosetoseg), New SqlParameter("@closetoaccount", strclosetoacct), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                        End If
                        Dim qry As String = "update TSPL_GL_ACCOUNTS set GL_Main_Code='" + GLMainAccountCode + "'  where Account_Code='" + straccountcode + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Imported Successfully!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RadPageViewPage1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles RadPageViewPage1.Paint

    End Sub

    Private Sub txtAccountSubGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtAccountSubGroup._MYValidating
        Try
            txtAccountSubGroup.Value = clsMainGLAccount.getFinder("", txtAccountSubGroup.Value, isButtonClicked)
            lblSubGroup.Text = clsDBFuncationality.getSingleValue("select Main_GL_Account_Desc from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account ='" + txtAccountSubGroup.Value + "'")
            ' Ticket No : TEC/03/07/19-000925 By prabhakar
            chkcontrolaccount.Checked = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select IsControlAcct from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account = '" + txtAccountSubGroup.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString())
        End Try
        
    End Sub
End Class
