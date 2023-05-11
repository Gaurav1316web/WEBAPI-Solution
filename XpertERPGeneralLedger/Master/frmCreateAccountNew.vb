Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports System.Array
Imports Excel = Microsoft.Office.Interop.Excel
Imports common
'done by priti TEC/18/05/18-000239
Public Class frmCreateAccountNew
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim userCode, companyCode As String
    Const colFrom As String = "colFrom"
    Const colTo As String = "colTo"

    Const colSegment As String = "colSegment"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isCellValueChangedOpen As Boolean = False
    Dim dt As DataTable
    Dim lngthOfStrucCode As Integer
    Dim strlength1 As Integer
    Dim check As Integer = 0
    Dim dr As SqlDataReader
    Dim ds As DataSet
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.createAccounts)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If

        rdbtnprocess.Visible = MyBase.isModifyFlag
        If rdbtnprocess.Visible = True Then
            rdmenuimport.Enabled = True
            rdmenuexport.Enabled = True
        Else
            rdmenuimport.Enabled = False
            rdmenuexport.Enabled = False
        End If


    End Sub

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

    Private Sub frmCreateAccount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadBlankGrid()
        LoadMainAccount()
        rdtxtcreateaccctwithstrcode.Enabled = False
        rdtxtfromaccountwithstrcode.Enabled = False
        gv1.AllowAddNewRow = False
        rdbtnpreview.Enabled = True
        
        rdbtnprocess.Enabled = False
        ButtonToolTip.SetToolTip(rdbtnClose, "Press Alt+C Close the Window")
    End Sub

    Sub LoadMainAccount()
        Dim StrQry As String = "select Main_GL_Account as Code,Main_GL_Account_Desc as Name,IsControlAcct from TSPL_ACCOUNT_MAIN_GL_ACCOUNT"
        cbgMainGLAccount.DataSource = clsDBFuncationality.GetDataTable(StrQry)
        cbgMainGLAccount.ValueMember = "Code"
        cbgMainGLAccount.DisplayMember = "Name"
    End Sub

    Private Sub rdbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnClose.Click
        closeForm()
    End Sub

    Sub closeForm()
        Me.Close()
    End Sub

    Private Sub fndfromaccount__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndfromaccount._MYValidating
        fndfromaccount.Value = clsGLStructure.getFinder("", fndfromaccount.Value, isButtonClicked)
        fromaccountfunfill()
    End Sub

    Private Sub fndcreateacctwithstrcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcreateacctwithstrcode._MYValidating
        fndcreateacctwithstrcode.Value = clsGLStructure.getFinder("", fndcreateacctwithstrcode.Value, isButtonClicked)
        createaccountfunfill()
        If rdtxtfromaccountwithstrcode.Text = rdtxtcreateaccctwithstrcode.Text Then
            gridfunfill()

        ElseIf (rdtxtfromaccountwithstrcode.Text <> rdtxtcreateaccctwithstrcode.Text) Then
            gridfunfill()
        End If
    End Sub

    Public Sub fromaccountfunfill()
        Dim query As String = "select seg_name1,seg_name2,seg_name3,seg_name4,seg_name5,seg_name6,seg_name7,seg_name8,seg_name9,seg_name10 from TSPL_GL_STRUCTURE where Str_Code='" + fndfromaccount.Value + "'"
        dt = clsDBFuncationality.GetDataTable(query)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                Dim str As String
                str = dr(0).ToString()
                Dim i As Integer
                For i = 0 To 9
                    If String.IsNullOrEmpty(dr(i).ToString()) Then
                        Exit For
                    Else
                        If i = 0 Then
                            rdtxtfromaccountwithstrcode.Text = str.Trim()
                        Else

                            str = str + "+" + dr(i).ToString()
                            rdtxtfromaccountwithstrcode.Text = str.Trim()
                        End If
                    End If
                Next

            Next
        End If


    End Sub

    Public Sub createaccountfunfill()
        Dim query As String = "select seg_name1,seg_name2,seg_name3,seg_name4,seg_name5,seg_name6,seg_name7,seg_name8,seg_name9,seg_name10 from TSPL_GL_STRUCTURE where Str_Code='" + fndcreateacctwithstrcode.Value + "'"
        dt = clsDBFuncationality.GetDataTable(query)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                Dim str As String
                str = dr(0).ToString()
                Dim i As Integer
                For i = 0 To 9
                    If String.IsNullOrEmpty(dr(i).ToString()) Then
                        Exit For
                    Else
                        If i = 0 Then

                            rdtxtcreateaccctwithstrcode.Text = str.Trim()
                        Else
                            str = str + "+" + dr(i).ToString()
                            rdtxtcreateaccctwithstrcode.Text = str.Trim()
                        End If
                    End If
                Next
            Next
        End If

    End Sub

    Public Sub gridfunfill()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        Dim query As String = "select seg_name2,seg_name3,seg_name4,seg_name5,seg_name6,seg_name7,seg_name8,seg_name9,seg_name10 from TSPL_GL_STRUCTURE where Str_Code='" + fndcreateacctwithstrcode.Value + "'"
        dt = clsDBFuncationality.GetDataTable(query)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                Dim i As Integer
                For i = 0 To 8
                    If Not String.IsNullOrEmpty(dr(i).ToString()) Then
                        Dim r As GridViewRowInfo = gv1.Rows.AddNew()
                        r.Cells(0).Value = dr(i).ToString()
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub dgvcreateaccount_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged



        ''''Added By [Pankaj Kumar Chaudhary]
        If e.Column Is gv1.Columns(colFrom) Then
            OpenFromList(False)
        End If
        If e.Column Is gv1.Columns(colTo) Then
            OpenToList(False)
        End If
        ''''end 
    End Sub

    Public Function AllowToSave() As Boolean
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colSegment).Value) <= 0) Then
                Throw New Exception("Please prove segment value at row no " + clsCommon.myCstr(ii))
            End If
        Next
        Dim trunc As String = "truncate table tspl_gl_preview"
        connectSql.RunSql(trunc)
        Return True
    End Function

    Private Sub rdbtnpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnpreview.Click
        Try
            Dim str As String = rdtxtfromaccountwithstrcode.Text.Trim()
            Dim arraystruct() As String
            arraystruct = str.Split("+")
            Dim strlength As Integer = arraystruct.Length
            Dim str1 As String = rdtxtcreateaccctwithstrcode.Text.Trim()

            Dim arrystr1() As String
            arrystr1 = str1.Split("+")
            strlength1 = arrystr1.Length
            lngthOfStrucCode = arrystr1.Length
            If strlength1 = strlength Then
                common.clsCommon.MyMessageBoxShow("Create Account with structure code should not same as From Account with structure code ")
            ElseIf strlength1 > strlength Then
                If cbgMainGLAccount.CheckedValue Is Nothing OrElse cbgMainGLAccount.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Select Account Number")
                Else
                    If AllowToSave() Then
                        If strlength1 = 2 Then
                            struct2()
                        ElseIf strlength1 = 3 Then
                            struct3()
                        ElseIf strlength1 = 4 Then
                            struct4()
                        ElseIf strlength1 = 5 Then
                            struct5()
                        ElseIf strlength1 = 6 Then
                            struct6()
                        ElseIf strlength1 = 7 Then
                            struct7()
                        ElseIf strlength1 = 8 Then
                            struct8()
                        ElseIf strlength1 = 9 Then
                            struct9()
                        ElseIf strlength1 = 10 Then
                            struct10()
                        End If


                        Dim frmpreview As New Frmcreateaccountpreview(userCode, objCommonVar.CurrentCompanyCode)
                        Me.fndcreateacctwithstrcode.Enabled = False
                        Me.fndfromaccount.Enabled = False
                        Me.gv1.Enabled = False
                        Me.rdbtnpreview.Enabled = False
                        Me.rdbtnprocess.Enabled = True
                        Me.rdbtnClear.Enabled = True
                        frmpreview.Show()
                        rdbtnprocess.Enabled = True
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub struct2()

        '' Anubhooti 24-Nov-2014 BM00000004665
        Dim IsControlAcc As String = ""
        'If ChkCntrlAcc.Checked = True Then
        '    IsControlAcc = "Y"
        'Else
        '    IsControlAcc = "N"
        'End If
        ' done by priti TEC/19/06/18-000284
        For Each strMainAC As String In cbgMainGLAccount.CheckedValue
            Dim strMainACDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Main_GL_Account_Desc from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + strMainAC + "'"))
            IsControlAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select IsControlAcct from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + strMainAC + "'"))
            If IsControlAcc = 1 Then
                IsControlAcc = "Y"
            Else
                IsControlAcc = "N"
            End If
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(0).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.CurrentRow.Tag, ArrayList)) + ")")
            For Each dr1 As DataRow In dt1.Rows
                Dim createaccount As String = strMainAC + "~-" + clsCommon.myCstr(dr1("segment_code"))
                Dim accdesc As String = strMainACDesc + "~," + clsCommon.myCstr(dr1("description"))
                clsGLAccount.CheckControlAccount(createaccount.Replace("~", ""), clsCommon.CompairString("Y", IsControlAcc) = CompairStringResult.Equal, Nothing)
                Dim query As String = "insert into tspl_gl_preview(processed,create_acct,account_code,[description],[type],[normal balance],account_grp,status,Control_Account,GL_Main_Code,Structure_Desc)values('N','Y','" + createaccount + "','" + Replace(accdesc, "'", "''") + "','Balance Sheet','Credit','','Y','" & IsControlAcc & "','" + strMainAC + "','" + rdtxtcreateaccctwithstrcode.Text + "')"
                clsDBFuncationality.ExecuteNonQuery(query)
            Next
        Next
    End Sub

    Public Sub struct3()
        '' Anubhooti 24-Nov-2014 BM00000004665
        Dim IsControlAcc As String = ""
        'If ChkCntrlAcc.Checked = True Then
        '    IsControlAcc = "Y"
        'Else
        '    IsControlAcc = "N"
        'End If
        For Each strMainAC As String In cbgMainGLAccount.CheckedValue
            Dim strMainACDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Main_GL_Account_Desc from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + strMainAC + "'"))
            IsControlAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select IsControlAcct from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + strMainAC + "'"))
            If IsControlAcc = 1 Then
                IsControlAcc = "Y"
            Else
                IsControlAcc = "N"
            End If
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(0).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(0).Tag, ArrayList)) + ")")
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(1).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(1).Tag, ArrayList)) + ")")
            For Each dr2 As DataRow In dt2.Rows
                For Each dr1 As DataRow In dt1.Rows
                    Dim createaccount As String = strMainAC + "~-" + clsCommon.myCstr(dr1("segment_code")) + "~-" + clsCommon.myCstr(dr2("segment_code"))
                    Dim accdesc As String = strMainACDesc + "~," + clsCommon.myCstr(dr1("description")) + "~-" + clsCommon.myCstr(dr2("description"))
                    clsGLAccount.CheckControlAccount(createaccount.Replace("~", ""), clsCommon.CompairString("Y", IsControlAcc) = CompairStringResult.Equal, Nothing)
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "GL_Main_Code", strMainAC)
                    clsCommon.AddColumnsForChange(coll, "processed", "N")
                    clsCommon.AddColumnsForChange(coll, "create_acct", "Y")
                    clsCommon.AddColumnsForChange(coll, "account_code", createaccount)
                    clsCommon.AddColumnsForChange(coll, "description", accdesc)
                    clsCommon.AddColumnsForChange(coll, "type", "Balance Sheet")
                    clsCommon.AddColumnsForChange(coll, "[normal balance]", "Credit")
                    clsCommon.AddColumnsForChange(coll, "account_grp", "")
                    clsCommon.AddColumnsForChange(coll, "status", "Y")
                    clsCommon.AddColumnsForChange(coll, "Control_Account", IsControlAcc)
                    clsCommon.AddColumnsForChange(coll, "Structure_Desc", rdtxtcreateaccctwithstrcode.Text)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_PREVIEW", OMInsertOrUpdate.Insert, "")
                Next
            Next
        Next
    End Sub

    Public Sub struct4()
        '' Anubhooti 24-Nov-2014 BM00000004665
        Dim IsControlAcc As String = ""
        'If ChkCntrlAcc.Checked = True Then
        '    IsControlAcc = "Y"
        'Else
        '    IsControlAcc = "N"
        'End If
        For Each strMainAC As String In cbgMainGLAccount.CheckedValue
            Dim strMainACDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Main_GL_Account_Desc from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + strMainAC + "'"))
            IsControlAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select IsControlAcct from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + strMainAC + "'"))
            If IsControlAcc = 1 Then
                IsControlAcc = "Y"
            Else
                IsControlAcc = "N"
            End If
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(0).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(0).Tag, ArrayList)) + ")")
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(1).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(1).Tag, ArrayList)) + ")")
            Dim dt3 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(2).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(2).Tag, ArrayList)) + ")")
            For Each dr3 As DataRow In dt3.Rows
                For Each dr2 As DataRow In dt2.Rows
                    For Each dr1 As DataRow In dt1.Rows
                        Dim createaccount As String = strMainAC + "~-" + clsCommon.myCstr(dr1("segment_code")) + "~-" + clsCommon.myCstr(dr2("segment_code")) + "~-" + clsCommon.myCstr(dr3("segment_code"))
                        Dim accdesc As String = strMainACDesc + "~," + clsCommon.myCstr(dr1("description")) + "~-" + clsCommon.myCstr(dr2("description")) + "~-" + clsCommon.myCstr(dr3("description"))
                        clsGLAccount.CheckControlAccount(createaccount.Replace("~", ""), clsCommon.CompairString("Y", IsControlAcc) = CompairStringResult.Equal, Nothing)
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "GL_Main_Code", strMainAC)
                        clsCommon.AddColumnsForChange(coll, "processed", "N")
                        clsCommon.AddColumnsForChange(coll, "create_acct", "Y")
                        clsCommon.AddColumnsForChange(coll, "account_code", createaccount)
                        clsCommon.AddColumnsForChange(coll, "description", accdesc)
                        clsCommon.AddColumnsForChange(coll, "type", "Balance Sheet")
                        clsCommon.AddColumnsForChange(coll, "[normal balance]", "Credit")
                        clsCommon.AddColumnsForChange(coll, "account_grp", "")
                        clsCommon.AddColumnsForChange(coll, "status", "Y")
                        clsCommon.AddColumnsForChange(coll, "Control_Account", IsControlAcc)
                        clsCommon.AddColumnsForChange(coll, "Structure_Desc", rdtxtcreateaccctwithstrcode.Text)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_PREVIEW", OMInsertOrUpdate.Insert, "")
                    Next
                Next
            Next
        Next
    End Sub

    Public Sub struct5()
        '' Anubhooti 24-Nov-2014 BM00000004665
        Dim IsControlAcc As String = ""
        'If ChkCntrlAcc.Checked = True Then
        '    IsControlAcc = "Y"
        'Else
        '    IsControlAcc = "N"
        'End If
        For Each strMainAC As String In cbgMainGLAccount.CheckedValue
            Dim strMainACDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Main_GL_Account_Desc from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + strMainAC + "'"))
            IsControlAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select IsControlAcct from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + strMainAC + "'"))
            If IsControlAcc = 1 Then
                IsControlAcc = "Y"
            Else
                IsControlAcc = "N"
            End If
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(0).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(0).Tag, ArrayList)) + ")")
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(1).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(1).Tag, ArrayList)) + ")")
            Dim dt3 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(2).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(2).Tag, ArrayList)) + ")")
            Dim dt4 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(3).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(3).Tag, ArrayList)) + ")")
            For Each dr4 As DataRow In dt4.Rows
                For Each dr3 As DataRow In dt3.Rows
                    For Each dr2 As DataRow In dt2.Rows
                        For Each dr1 As DataRow In dt1.Rows
                            Dim createaccount As String = strMainAC + "~-" + clsCommon.myCstr(dr1("segment_code")) + "~-" + clsCommon.myCstr(dr2("segment_code")) + "~-" + clsCommon.myCstr(dr3("segment_code")) + "~-" + clsCommon.myCstr(dr4("segment_code"))
                            Dim accdesc As String = strMainACDesc + "~," + clsCommon.myCstr(dr1("description")) + "~-" + clsCommon.myCstr(dr2("description")) + "~-" + clsCommon.myCstr(dr3("description")) + "~-" + clsCommon.myCstr(dr4("description"))
                            clsGLAccount.CheckControlAccount(createaccount.Replace("~", ""), clsCommon.CompairString("Y", IsControlAcc) = CompairStringResult.Equal, Nothing)
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "GL_Main_Code", strMainAC)
                            clsCommon.AddColumnsForChange(coll, "processed", "N")
                            clsCommon.AddColumnsForChange(coll, "create_acct", "Y")
                            clsCommon.AddColumnsForChange(coll, "account_code", createaccount)
                            clsCommon.AddColumnsForChange(coll, "description", accdesc)
                            clsCommon.AddColumnsForChange(coll, "type", "Balance Sheet")
                            clsCommon.AddColumnsForChange(coll, "[normal balance]", "Credit")
                            clsCommon.AddColumnsForChange(coll, "account_grp", "")
                            clsCommon.AddColumnsForChange(coll, "status", "Y")
                            clsCommon.AddColumnsForChange(coll, "Control_Account", IsControlAcc)
                            clsCommon.AddColumnsForChange(coll, "Structure_Desc", rdtxtcreateaccctwithstrcode.Text)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_PREVIEW", OMInsertOrUpdate.Insert, "")
                        Next
                    Next
                Next
            Next
        Next
    End Sub

    Public Sub struct6()
        '' Anubhooti 24-Nov-2014 BM00000004665
        Dim IsControlAcc As String = ""
        'If ChkCntrlAcc.Checked = True Then
        '    IsControlAcc = "Y"
        'Else
        '    IsControlAcc = "N"
        'End If
        For Each strMainAC As String In cbgMainGLAccount.CheckedValue
            Dim strMainACDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Main_GL_Account_Desc from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + strMainAC + "'"))
            IsControlAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select IsControlAcct from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + strMainAC + "'"))
            If IsControlAcc = 1 Then
                IsControlAcc = "Y"
            Else
                IsControlAcc = "N"
            End If
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(0).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(0).Tag, ArrayList)) + ")")
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(1).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(1).Tag, ArrayList)) + ")")
            Dim dt3 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(2).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(2).Tag, ArrayList)) + ")")
            Dim dt4 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(3).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(3).Tag, ArrayList)) + ")")
            Dim dt5 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(4).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(4).Tag, ArrayList)) + ")")
            For Each dr5 As DataRow In dt5.Rows
                For Each dr4 As DataRow In dt4.Rows
                    For Each dr3 As DataRow In dt3.Rows
                        For Each dr2 As DataRow In dt2.Rows
                            For Each dr1 As DataRow In dt1.Rows
                                Dim createaccount As String = strMainAC + "~-" + clsCommon.myCstr(dr1("segment_code")) + "~-" + clsCommon.myCstr(dr2("segment_code")) + "~-" + clsCommon.myCstr(dr3("segment_code")) + "~-" + clsCommon.myCstr(dr4("segment_code")) + "~-" + clsCommon.myCstr(dr5("segment_code"))
                                Dim accdesc As String = strMainACDesc + "~," + clsCommon.myCstr(dr1("description")) + "~-" + clsCommon.myCstr(dr2("description")) + "~-" + clsCommon.myCstr(dr3("description")) + "~-" + clsCommon.myCstr(dr4("description")) + "~-" + clsCommon.myCstr(dr5("description"))
                                clsGLAccount.CheckControlAccount(createaccount.Replace("~", ""), clsCommon.CompairString("Y", IsControlAcc) = CompairStringResult.Equal, Nothing)
                                Dim query As String = "insert into tspl_gl_preview(processed,create_acct,account_code,[description],[type],[normal balance],account_grp,status,Control_Account,Structure_Desc,GL_Main_Code)values('N','Y','" + createaccount + "','" + Replace(accdesc, "'", "''") + "','Balance Sheet','Credit','N','Y','" & IsControlAcc & "','" + rdtxtcreateaccctwithstrcode.Text + "','" + strMainAC + "')"
                                clsDBFuncationality.ExecuteNonQuery(query)
                            Next
                        Next
                    Next
                Next
            Next
        Next
    End Sub

    Public Sub struct7()
        '' Anubhooti 24-Nov-2014 BM00000004665
        Dim IsControlAcc As String = ""
        'If ChkCntrlAcc.Checked = True Then
        '    IsControlAcc = "Y"
        'Else
        '    IsControlAcc = "N"
        'End If
        For Each strMainAC As String In cbgMainGLAccount.CheckedValue
            Dim strMainACDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Main_GL_Account_Desc from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + strMainAC + "'"))
            IsControlAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select IsControlAcct from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + strMainAC + "'"))
            If IsControlAcc = 1 Then
                IsControlAcc = "Y"
            Else
                IsControlAcc = "N"
            End If
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(0).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(0).Tag, ArrayList)) + ")")
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(1).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(1).Tag, ArrayList)) + ")")
            Dim dt3 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(2).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(2).Tag, ArrayList)) + ")")
            Dim dt4 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(3).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(3).Tag, ArrayList)) + ")")
            Dim dt5 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(4).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(4).Tag, ArrayList)) + ")")
            Dim dt6 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(5).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(5).Tag, ArrayList)) + ")")
            For Each dr6 As DataRow In dt6.Rows
                For Each dr5 As DataRow In dt5.Rows
                    For Each dr4 As DataRow In dt4.Rows
                        For Each dr3 As DataRow In dt3.Rows
                            For Each dr2 As DataRow In dt2.Rows
                                For Each dr1 As DataRow In dt1.Rows
                                    Dim createaccount As String = strMainAC + "~-" + clsCommon.myCstr(dr1("segment_code")) + "~-" + clsCommon.myCstr(dr2("segment_code")) + "~-" + clsCommon.myCstr(dr3("segment_code")) + "~-" + clsCommon.myCstr(dr4("segment_code")) + "~-" + clsCommon.myCstr(dr5("segment_code")) + "~-" + clsCommon.myCstr(dr6("segment_code"))
                                    Dim accdesc As String = strMainACDesc + "~," + clsCommon.myCstr(dr1("description")) + "~-" + clsCommon.myCstr(dr2("description")) + "~-" + clsCommon.myCstr(dr3("description")) + "~-" + clsCommon.myCstr(dr4("description")) + "~-" + clsCommon.myCstr(dr5("description")) + "~-" + clsCommon.myCstr(dr6("description"))
                                    clsGLAccount.CheckControlAccount(createaccount.Replace("~", ""), clsCommon.CompairString("Y", IsControlAcc) = CompairStringResult.Equal, Nothing)
                                    Dim coll As New Hashtable()
                                    clsCommon.AddColumnsForChange(coll, "GL_Main_Code", strMainAC)
                                    clsCommon.AddColumnsForChange(coll, "processed", "N")
                                    clsCommon.AddColumnsForChange(coll, "create_acct", "Y")
                                    clsCommon.AddColumnsForChange(coll, "account_code", createaccount)
                                    clsCommon.AddColumnsForChange(coll, "description", accdesc)
                                    clsCommon.AddColumnsForChange(coll, "type", "Balance Sheet")
                                    clsCommon.AddColumnsForChange(coll, "[normal balance]", "Credit")
                                    clsCommon.AddColumnsForChange(coll, "account_grp", "")
                                    clsCommon.AddColumnsForChange(coll, "status", "Y")
                                    clsCommon.AddColumnsForChange(coll, "Control_Account", IsControlAcc)
                                    clsCommon.AddColumnsForChange(coll, "Structure_Desc", rdtxtcreateaccctwithstrcode.Text)
                                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_PREVIEW", OMInsertOrUpdate.Insert, "")
                                Next
                            Next
                        Next
                    Next
                Next
            Next
        Next
    End Sub
    Public Sub struct8()
        '' Anubhooti 24-Nov-2014 BM00000004665
        Dim IsControlAcc As String = ""
        'If ChkCntrlAcc.Checked = True Then
        '    IsControlAcc = "Y"
        'Else
        '    IsControlAcc = "N"
        'End If
        For Each strMainAC As String In cbgMainGLAccount.CheckedValue
            Dim strMainACDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Main_GL_Account_Desc from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + strMainAC + "'"))
            IsControlAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select IsControlAcct from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + strMainAC + "'"))
            If IsControlAcc = 1 Then
                IsControlAcc = "Y"
            Else
                IsControlAcc = "N"
            End If
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(0).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(0).Tag, ArrayList)) + ")")
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(1).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(1).Tag, ArrayList)) + ")")
            Dim dt3 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(2).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(2).Tag, ArrayList)) + ")")
            Dim dt4 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(3).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(3).Tag, ArrayList)) + ")")
            Dim dt5 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(4).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(4).Tag, ArrayList)) + ")")
            Dim dt6 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(5).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(5).Tag, ArrayList)) + ")")
            Dim dt7 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(6).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(6).Tag, ArrayList)) + ")")
            For Each dr7 As DataRow In dt7.Rows
                For Each dr6 As DataRow In dt6.Rows
                    For Each dr5 As DataRow In dt5.Rows
                        For Each dr4 As DataRow In dt4.Rows
                            For Each dr3 As DataRow In dt3.Rows
                                For Each dr2 As DataRow In dt2.Rows
                                    For Each dr1 As DataRow In dt1.Rows
                                        Dim createaccount As String = strMainAC + "~-" + clsCommon.myCstr(dr1("segment_code")) + "~-" + clsCommon.myCstr(dr2("segment_code")) + "~-" + clsCommon.myCstr(dr3("segment_code")) + "~-" + clsCommon.myCstr(dr4("segment_code")) + "~-" + clsCommon.myCstr(dr5("segment_code")) + "~-" + clsCommon.myCstr(dr6("segment_code")) + "~-" + clsCommon.myCstr(dr7("segment_code"))
                                        Dim accdesc As String = strMainACDesc + "~," + clsCommon.myCstr(dr1("description")) + "~-" + clsCommon.myCstr(dr2("description")) + "~-" + clsCommon.myCstr(dr3("description")) + "~-" + clsCommon.myCstr(dr4("description")) + "~-" + clsCommon.myCstr(dr5("description")) + "~-" + clsCommon.myCstr(dr6("description")) + "~-" + clsCommon.myCstr(dr7("description"))
                                        clsGLAccount.CheckControlAccount(createaccount.Replace("~", ""), clsCommon.CompairString("Y", IsControlAcc) = CompairStringResult.Equal, Nothing)
                                        Dim coll As New Hashtable()
                                        clsCommon.AddColumnsForChange(coll, "GL_Main_Code", strMainAC)
                                        clsCommon.AddColumnsForChange(coll, "processed", "N")
                                        clsCommon.AddColumnsForChange(coll, "create_acct", "Y")
                                        clsCommon.AddColumnsForChange(coll, "account_code", createaccount)
                                        clsCommon.AddColumnsForChange(coll, "description", accdesc)
                                        clsCommon.AddColumnsForChange(coll, "type", "Balance Sheet")
                                        clsCommon.AddColumnsForChange(coll, "[normal balance]", "Credit")
                                        clsCommon.AddColumnsForChange(coll, "account_grp", "")
                                        clsCommon.AddColumnsForChange(coll, "status", "Y")
                                        clsCommon.AddColumnsForChange(coll, "Control_Account", IsControlAcc)
                                        clsCommon.AddColumnsForChange(coll, "Structure_Desc", rdtxtcreateaccctwithstrcode.Text)
                                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_PREVIEW", OMInsertOrUpdate.Insert, "")
                                    Next
                                Next
                            Next
                        Next
                    Next
                Next
            Next
        Next
    End Sub

    Public Sub struct9()
        '' Anubhooti 24-Nov-2014 BM00000004665
        Dim IsControlAcc As String = ""
        'If ChkCntrlAcc.Checked = True Then
        '    IsControlAcc = "Y"
        'Else
        '    IsControlAcc = "N"
        'End If
        For Each strMainAC As String In cbgMainGLAccount.CheckedValue
            Dim strMainACDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Main_GL_Account_Desc from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + strMainAC + "'"))
            IsControlAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select IsControlAcct from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + strMainAC + "'"))
            If IsControlAcc = 1 Then
                IsControlAcc = "Y"
            Else
                IsControlAcc = "N"
            End If
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(0).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(0).Tag, ArrayList)) + ")")
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(1).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(1).Tag, ArrayList)) + ")")
            Dim dt3 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(2).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(2).Tag, ArrayList)) + ")")
            Dim dt4 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(3).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(3).Tag, ArrayList)) + ")")
            Dim dt5 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(4).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(4).Tag, ArrayList)) + ")")
            Dim dt6 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(5).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(5).Tag, ArrayList)) + ")")
            Dim dt7 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(6).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(6).Tag, ArrayList)) + ")")
            Dim dt8 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(7).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(7).Tag, ArrayList)) + ")")
            For Each dr8 As DataRow In dt8.Rows
                For Each dr7 As DataRow In dt7.Rows
                    For Each dr6 As DataRow In dt6.Rows
                        For Each dr5 As DataRow In dt5.Rows
                            For Each dr4 As DataRow In dt4.Rows
                                For Each dr3 As DataRow In dt3.Rows
                                    For Each dr2 As DataRow In dt2.Rows
                                        For Each dr1 As DataRow In dt1.Rows
                                            Dim createaccount As String = strMainAC + "~-" + clsCommon.myCstr(dr1("segment_code")) + "~-" + clsCommon.myCstr(dr2("segment_code")) + "~-" + clsCommon.myCstr(dr3("segment_code")) + "~-" + clsCommon.myCstr(dr4("segment_code")) + "~-" + clsCommon.myCstr(dr5("segment_code")) + "~-" + clsCommon.myCstr(dr6("segment_code")) + "~-" + clsCommon.myCstr(dr7("segment_code")) + "~-" + clsCommon.myCstr(dr8("segment_code"))
                                            Dim accdesc As String = strMainACDesc + "~," + clsCommon.myCstr(dr1("description")) + "~-" + clsCommon.myCstr(dr2("description")) + "~-" + clsCommon.myCstr(dr3("description")) + "~-" + clsCommon.myCstr(dr4("description")) + "~-" + clsCommon.myCstr(dr5("description")) + "~-" + clsCommon.myCstr(dr6("description")) + "~-" + clsCommon.myCstr(dr7("description")) + "~-" + clsCommon.myCstr(dr8("description"))
                                            clsGLAccount.CheckControlAccount(createaccount.Replace("~", ""), clsCommon.CompairString("Y", IsControlAcc) = CompairStringResult.Equal, Nothing)
                                            Dim coll As New Hashtable()
                                            clsCommon.AddColumnsForChange(coll, "GL_Main_Code", strMainAC)
                                            clsCommon.AddColumnsForChange(coll, "processed", "N")
                                            clsCommon.AddColumnsForChange(coll, "create_acct", "Y")
                                            clsCommon.AddColumnsForChange(coll, "account_code", createaccount)
                                            clsCommon.AddColumnsForChange(coll, "description", accdesc)
                                            clsCommon.AddColumnsForChange(coll, "type", "Balance Sheet")
                                            clsCommon.AddColumnsForChange(coll, "[normal balance]", "Credit")
                                            clsCommon.AddColumnsForChange(coll, "account_grp", "")
                                            clsCommon.AddColumnsForChange(coll, "status", "Y")
                                            clsCommon.AddColumnsForChange(coll, "Control_Account", IsControlAcc)
                                            clsCommon.AddColumnsForChange(coll, "Structure_Desc", rdtxtcreateaccctwithstrcode.Text)
                                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_PREVIEW", OMInsertOrUpdate.Insert, "")
                                        Next
                                    Next
                                Next
                            Next
                        Next
                    Next
                Next
            Next
        Next
    End Sub

    Public Sub struct10()
        '' Anubhooti 24-Nov-2014 BM00000004665
        Dim IsControlAcc As String = ""
        'If ChkCntrlAcc.Checked = True Then
        '    IsControlAcc = "Y"
        'Else
        '    IsControlAcc = "N"
        'End If
        For Each strMainAC As String In cbgMainGLAccount.CheckedValue
            Dim strMainACDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Main_GL_Account_Desc from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + strMainAC + "'"))
            IsControlAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select IsControlAcct from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + strMainAC + "'"))
            If IsControlAcc = 1 Then
                IsControlAcc = "Y"
            Else
                IsControlAcc = "N"
            End If
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(0).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(0).Tag, ArrayList)) + ")")
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(1).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(1).Tag, ArrayList)) + ")")
            Dim dt3 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(2).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(2).Tag, ArrayList)) + ")")
            Dim dt4 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(3).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(3).Tag, ArrayList)) + ")")
            Dim dt5 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(4).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(4).Tag, ArrayList)) + ")")
            Dim dt6 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(5).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(5).Tag, ArrayList)) + ")")
            Dim dt7 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(6).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(6).Tag, ArrayList)) + ")")
            Dim dt8 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(7).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(7).Tag, ArrayList)) + ")")
            Dim dt9 As DataTable = clsDBFuncationality.GetDataTable("select segment_code,description from tspl_GL_segment_code where segment_name='" + clsCommon.myCstr(gv1.Rows(8).Cells(0).Value) + "' and segment_code  in (" + clsCommon.GetMulcallString(TryCast(gv1.Rows(8).Tag, ArrayList)) + ")")
            For Each dr9 As DataRow In dt9.Rows
                For Each dr8 As DataRow In dt8.Rows
                    For Each dr7 As DataRow In dt7.Rows
                        For Each dr6 As DataRow In dt6.Rows
                            For Each dr5 As DataRow In dt5.Rows
                                For Each dr4 As DataRow In dt4.Rows
                                    For Each dr3 As DataRow In dt3.Rows
                                        For Each dr2 As DataRow In dt2.Rows
                                            For Each dr1 As DataRow In dt1.Rows
                                                Dim createaccount As String = strMainAC + "~-" + clsCommon.myCstr(dr1("segment_code")) + "~-" + clsCommon.myCstr(dr2("segment_code")) + "~-" + clsCommon.myCstr(dr3("segment_code")) + "~-" + clsCommon.myCstr(dr4("segment_code")) + "~-" + clsCommon.myCstr(dr5("segment_code")) + "~-" + clsCommon.myCstr(dr6("segment_code")) + "~-" + clsCommon.myCstr(dr7("segment_code")) + "~-" + clsCommon.myCstr(dr8("segment_code")) + "~-" + clsCommon.myCstr(dr9("segment_code"))
                                                Dim accdesc As String = strMainACDesc + "~," + clsCommon.myCstr(dr1("description")) + "~-" + clsCommon.myCstr(dr2("description")) + "~-" + clsCommon.myCstr(dr3("description")) + "~-" + clsCommon.myCstr(dr4("description")) + "~-" + clsCommon.myCstr(dr5("description")) + "~-" + clsCommon.myCstr(dr6("description")) + "~-" + clsCommon.myCstr(dr7("description")) + "~-" + clsCommon.myCstr(dr8("description")) + "~-" + clsCommon.myCstr(dr9("description"))
                                                clsGLAccount.CheckControlAccount(createaccount.Replace("~", ""), clsCommon.CompairString("Y", IsControlAcc) = CompairStringResult.Equal, Nothing)
                                                Dim coll As New Hashtable()
                                                clsCommon.AddColumnsForChange(coll, "GL_Main_Code", strMainAC)
                                                clsCommon.AddColumnsForChange(coll, "processed", "N")
                                                clsCommon.AddColumnsForChange(coll, "create_acct", "Y")
                                                clsCommon.AddColumnsForChange(coll, "account_code", createaccount)
                                                clsCommon.AddColumnsForChange(coll, "description", accdesc)
                                                clsCommon.AddColumnsForChange(coll, "type", "Balance Sheet")
                                                clsCommon.AddColumnsForChange(coll, "[normal balance]", "Credit")
                                                clsCommon.AddColumnsForChange(coll, "account_grp", "")
                                                clsCommon.AddColumnsForChange(coll, "status", "Y")
                                                clsCommon.AddColumnsForChange(coll, "Control_Account", IsControlAcc)
                                                clsCommon.AddColumnsForChange(coll, "Structure_Desc", rdtxtcreateaccctwithstrcode.Text)
                                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_PREVIEW", OMInsertOrUpdate.Insert, "")
                                            Next
                                        Next
                                    Next
                                Next
                            Next
                        Next
                    Next
                Next
            Next
        Next
    End Sub

    Private Sub rdbtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnClear.Click
        Dim query As String = "Delete from  tspl_gl_preview"
        connectSql.RunSql(query)
        myMessages.EnableConfirm()
        Me.fndcreateacctwithstrcode.Enabled = True
        Me.fndfromaccount.Enabled = True
        Me.gv1.Enabled = True
        Me.rdbtnpreview.Enabled = True
        Me.rdbtnprocess.Enabled = False
        Me.rdbtnClear.Enabled = False
    End Sub

    Private Sub MasterTemplate_DefaultValuesNeeded(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.DefaultValuesNeeded
        Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
        grow.Cells(1).Value = "Yes"
    End Sub

    Private Sub rdbtnprocess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnprocess.Click
        If insertConfirm() Then
            funinsert()
        End If
    End Sub

    Public Sub funinsert()
        Try
            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.createAccounts, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            Dim strdesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select str_description from tspl_gl_structure where str_code='" + fndcreateacctwithstrcode.Value + "' ")).Trim()
            Dim queryretrive As String = "select account_code,description,type,[normal balance],account_grp,status,control_account,TSPL_ACCOUNT_GROUPS.Account_Group_Desc,GL_Main_Code from tspl_gl_preview left outer join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code= TSPL_GL_PREVIEW.Account_Grp order by account_code "
            dt = clsDBFuncationality.GetDataTable(queryretrive)
            Dim acc_code As String
            Dim desc1 As String
            Dim type As String
            Dim normal As String
            Dim accgrp As String
            Dim status As String = ""
            Dim ctr As String = ""
            Dim accdesc As String
            clsCommon.ProgressBarShow()
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    acc_code = dr(0).ToString()
                    desc1 = dr(1).ToString().Replace("~", "").Trim()
                    If desc1.Length > 100 Then
                        desc1 = desc1.Substring(0, 99).Replace("~", "")
                    Else
                        desc1.ToString().Replace("~", "")
                    End If

                    type = dr(2).ToString().Trim()
                    normal = dr(3).ToString().Trim()
                    accgrp = dr(4).ToString().Trim()
                    status = dr(5).ToString().Trim()
                    ctr = dr(6).ToString().Trim() '"N" 
                    accdesc = dr(7).ToString().Trim()

                    Dim var As String = "select COUNT(*) from tspl_gl_accounts where account_code='" + Replace(acc_code, "~", "") + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(var))
                    If Not (i = 1) Then
                        Dim accinsert As String = "insert into tspl_gl_accounts (account_code,description,str_code,str_description,account_balance,account_type,account_group_code,account_group_desc,status,controlaccount,created_by,created_date,modify_by,modify_date,comp_code,GL_Main_Code)values('" + Replace(acc_code, "~", "") + "','" + Replace(desc1, "'", "") + "','" + fndcreateacctwithstrcode.Value.Trim() + "','" + strdesc.Trim() + "','" + normal.Trim() + "','" + type.Trim() + "','" + accgrp.Trim() + "','" + accdesc.Trim() + "','" + status.Trim() + "','" + ctr.Trim() + "','" + userCode + "','" + connectSql.serverDate() + "','" + userCode + "','" + connectSql.serverDate() + "','" + objCommonVar.CurrentCompanyCode + "','" + clsCommon.myCstr(dr("GL_Main_Code")) + "')"
                        connectSql.RunSql(accinsert)
                    Else
                        Dim UpdateAcc As String = "UPDATE tspl_gl_accounts SET description='" + Replace(desc1, "'", "") + "',str_code='" + fndcreateacctwithstrcode.Value.Trim() + "',str_description='" + strdesc.Trim() + "',account_balance='" + normal.Trim() + "',account_type='" + type.Trim() + "',account_group_code='" + accgrp.Trim() + "',account_group_desc='" + accdesc.Trim() + "',status='" + status.Trim() + "',controlaccount='" + ctr.Trim() + "',modify_by='" + userCode + "',modify_date='" + connectSql.serverDate() + "',comp_code='" + objCommonVar.CurrentCompanyCode + "',GL_Main_Code='" + clsCommon.myCstr(dr("GL_Main_Code")) + "' WHERE account_code='" + Replace(acc_code, "~", "") + "'"
                        connectSql.RunSql(UpdateAcc)
                    End If
                Next
            End If


            Dim query4 As String = "select account_code from tspl_gl_preview "
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(query4)
            Dim acccode As String
            If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                For Each dr3 As DataRow In dt1.Rows
                    acccode = dr3(0).ToString()
                    Dim query As String = "select account_code,Description,structure_desc,control_account  from tspl_gl_preview where account_code='" + acccode + "'"
                    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
                    Dim arr() As String
                    Dim arr1() As String
                    Dim arrstr_desc() As String

                    If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                        For Each dr As DataRow In dt2.Rows
                            arr = dr(0).ToString.Split("~")
                            'THIS  line should be Change when then we have a new table.it is only alternate solution.
                            Dim descp As String = dr(1).ToString.Replace(",", ",")
                            arrstr_desc = dr(2).ToString.Split("+")
                            'this line is will be here  then we have new table.
                            arr1 = descp.ToString.Split("~")
                            Dim accdesc1 As String = arr1(0).ToString()
                            If accdesc1.Length > 50 Then
                                accdesc1 = accdesc1.Substring(0, 49)
                            Else
                                accdesc1.ToString()
                            End If
                            Dim accupdate As String = "update tspl_gl_accounts set  account_seg_code1='" + arr(0).ToString().TrimStart("-") + "',account_seg_desc1= '" + Replace(accdesc1.ToString.TrimStart(","), "'", "''") + "', controlaccount='" & clsCommon.myCstr(dr(3)) & "' where account_code='" + Replace(acccode, "~", "") + "'"
                            connectSql.RunSql(accupdate)
                            Dim i As Integer = arr.Length() - 1
                            Dim i1 As Integer = arr1.Length() - 1
                            Dim i2 As Integer = arrstr_desc.Length() - 1
                            For i = 1 To arr.Length - 1
                                Dim query1 As String = "select segment_name from tspl_gl_segment_code where segment_code='" + arr(i).ToString().TrimStart("-") + "'and segment_name='" + arrstr_desc(i).ToString().TrimStart(",") + "'"
                                Dim seg_name As String = connectSql.RunScalar(query1)
                                Dim query2 As String = "select seg_no from tspl_gl_segment where seg_name='" + seg_name + "'"
                                Dim seg_no As String = connectSql.RunScalar(query2)
                                Dim accsegdesc As String = arr1(i).ToString().TrimStart(",")
                                If accsegdesc.Length > 50 Then
                                    accsegdesc.Substring(0, 49)
                                Else
                                    accsegdesc.ToString()
                                End If
                                Dim query3 As String = "update tspl_gl_accounts set account_seg_code" + seg_no + "='" + arr(i).ToString().TrimStart("-") + "',account_seg_desc" + seg_no + "='" + Replace(accsegdesc.Trim(), "'", "''") + "'  where  account_code='" + Replace(acccode, "~", "") + "'"
                                connectSql.RunSql(query3)

                            Next
                        Next
                    End If
                Next
            End If
            Dim trunc As String = "truncate table tspl_gl_preview"
            connectSql.RunSql(trunc)
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub MasterTemplate_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellEditorInitialized
        If TypeOf Me.gv1.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gv1.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
            editor.EditorControl.MasterTemplate.BestFitColumns()
            editor.DropDownStyle = RadDropDownStyle.DropDown
            editor.AutoFilter = True
            If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
                Dim autofilter As FilterDescriptor = New FilterDescriptor("Description", FilterOperator.Contains, "")
                autofilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autofilter)

            End If

        End If
    End Sub

    Sub LoadBlankGrid()
        Dim repoSegment As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSegment.FormatString = ""
        repoSegment.HeaderText = "Segment"
        repoSegment.Name = colSegment
        repoSegment.Width = 150
        repoSegment.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoSegment)
    End Sub

    Private Sub frmCreateAccount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colFrom) Then
                'dgvcreateaccount.CurrentColumn = dgvcreateaccount.Columns(colTo)
                OpenFromList(True)
                gv1.CurrentColumn = gv1.Columns(colTo)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colTo) Then
                gv1.CurrentColumn = gv1.Columns(colFrom)
                OpenToList(True)
                gv1.CurrentColumn = gv1.Columns(colTo)
            End If
        End If
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then

        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Sub OpenFromList(ByVal isButtonClick As Boolean)
        Dim qry As String = "SELECT Segment_code as Code,Description from tspl_gl_segment_code "
        gv1.CurrentRow.Cells(colFrom).Value = clsCommon.ShowSelectForm("CAGLOPEN", qry, "Code", "Segment_name='" + clsCommon.myCstr(gv1.CurrentRow.Cells("colsegmentname").Value) + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colFrom).Value), "Code", isButtonClick)
    End Sub

    Sub OpenToList(ByVal isButtonClick As Boolean)
        Dim qry As String = "SELECT Segment_code as Code,Description from tspl_gl_segment_code "
        gv1.CurrentRow.Cells(colTo).Value = clsCommon.ShowSelectForm("CAGLOPEN", qry, "Code", "Segment_name='" + clsCommon.myCstr(gv1.CurrentRow.Cells("colsegmentname").Value) + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colTo).Value), "Code", isButtonClick)
        ''''end
    End Sub

    Private Sub dgvcreateaccount_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If gv1.Rows.Count > 0 Then
            Dim frm As FrmSelectSegment = New FrmSelectSegment()
            frm.strSegment = clsCommon.myCstr(gv1.CurrentRow.Cells("colsegmentname").Value)
            frm.arr = TryCast(gv1.CurrentRow.Tag, ArrayList)
            frm.ShowDialog()
            If Not frm.isCencelButtonClicked AndAlso frm.arr IsNot Nothing Then
                gv1.CurrentRow.Tag = frm.arr
                Dim strTemp As String = ""
                For Each Str As String In frm.arr
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ","
                    End If
                    strTemp += Str
                Next
                gv1.CurrentRow.Cells(colSegment).Value = strTemp
            End If
        End If
    End Sub

End Class
