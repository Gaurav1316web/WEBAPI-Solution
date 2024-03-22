'***** Created By:- Raghvendra Kumar Vats *****
'*****Table :- TSPL_GL_STRUCTURE
'Column - Structure code(Primary Key),Structure desc,Seg No1-10,Seg Name1-10,Seg Length1-10,
'Created By,Created Date,Modified By,Modified date,Company Code...
'Start Date -- 21st May
'End Date -- 22nd May

Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports exz = Microsoft.Office.Interop.Excel
Imports common
Public Class frmGLStructure
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim dr As SqlDataReader
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.accountStructure)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btn_save.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btn_save.Visible = True Then
            radmenu_export.Enabled = True
            radmenu_import.Enabled = True
        Else
            radmenu_import.Enabled = False
            radmenu_export.Enabled = False
        End If
        '--------------------------------------------------

        'btnPost.Visible = MyBase.isPostFlag
        btn_delete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmGLStructure_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btn_save.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btn_delete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        End If
    End Sub
    '***** On Form Load,Only Structure Code And save and Close Buttons Are Enabled.Segments Added In ListBox1 from TSPL_GL_SEGMENT.....
    Private Sub frm_Gl_AccountStructure_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'globalFunc.mandatoryText(fnd_structurecode.txtValue)
        'globalFunc.mandatoryDropdown()
        ToolTipStructure.SetToolTip(btn_reset, "New")
        'fnd_structurecode.txtValue.MaxLength = 10
        'fnd_structurecode.txtValue.TabIndex = 0
        radtxt_structcode.TabIndex = 1
        'fnd_structurecode.txtValue.BackColor = Color.White
        radtxt_structcode.Enabled = False
        btn_save.Enabled = True
        btn_delete.Enabled = False
        btn_include.Enabled = False
        btn_exclude.Enabled = False
        listbox1.Enabled = False
        listbox2.Enabled = False
        fnd_structurecode.Focus()
        binddata()
        'AddHandler fnd_structurecode.ValueChanged, AddressOf TextChanged1
        'AddHandler fnd_structurecode.txtValue.KeyPress, AddressOf KeyPress2
        AddHandler radtxt_structcode.KeyPress, AddressOf keypress1
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        ButtonToolTip.SetToolTip(btn_save, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btn_delete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btn_close, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btn_reset, "Press Alt+N Adding New Trasnaction")
        SetLenght()
    End Sub


    Public Sub SetLenght()
        fnd_structurecode.MyMaxLength = 12
        radtxt_structcode.MaxLength = 60
    End Sub
    Private Sub keypress1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'If (Microsoft.VisualBasic.Asc(e.KeyChar) < 48) _
        '         Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 57) _
        '             And (Microsoft.VisualBasic.Asc(e.KeyChar) < 65) _
        '             Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 90) _
        '             And (Microsoft.VisualBasic.Asc(e.KeyChar) < 97) _
        '             Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 122) Then
        '    e.Handled = True
        '    If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
        '        e.Handled = False
        '    End If
        '    If (Microsoft.VisualBasic.Asc(e.KeyChar) = 46) Then
        '        e.Handled = False
        '    End If
        '    If (Microsoft.VisualBasic.Asc(e.KeyChar) = 32) Then
        '        e.Handled = False
        '    End If
        'End If
    End Sub
    '******** Structure Code Typed in upper case....
    Private Sub KeyPress2(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'fnd_structurecode.txtValue.CharacterCasing = CharacterCasing.Upper
    End Sub

    '***** Use Handler To Fill Structure Code.If structure code is not blank then fill the corresponding data
    Private Sub TextChanged1(ByVal sender As System.Object, ByVal e As System.EventArgs)

        LoadData()
    End Sub
    Sub LoadData()
        Try
            '' added by abhishek as on 12/10/2012
            Dim qry As String = "select Str_Code from TSPL_GL_STRUCTURE where [Str_Code] ='" + fnd_structurecode.Value + "'"
            Dim s As String
            s = clsDBFuncationality.getSingleValue(qry)
            If s <> "" Then
                funfill()
            Else
                radtxt_structcode.Text = ""
                btn_save.Enabled = True
                btn_save.Text = "Save"
                'chk_defaultstruct.Checked = False
                listbox1.Items.Clear()
                listbox2.Items.Clear()
                '' added by abhishek as on 12/10/2012
                Dim qryseg As String = "select seg_name from tspl_gl_segment where seg_name <> ''"
                dt = clsDBFuncationality.GetDataTable(qryseg)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows
                        listbox1.Items.Add(dr(0).ToString())
                    Next
                End If

            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    '***** Fill segment on adjact location.If segments are in listbox2 then that segments removed from the segment1....  
    Private Sub funfill()
        Dim strsegname1 As String = "n"
        Dim strsegname2 As String = "n"
        Dim strsegname3 As String = "n"
        Dim strsegname4 As String = "n"
        Dim strsegname5 As String = "n"
        Dim strsegname6 As String = "n"
        Dim strsegname7 As String = "n"
        Dim strsegname8 As String = "n"
        Dim strsegname9 As String = "n"
        Dim strsegname10 As String = "n"
        Try
            If fnd_structurecode.Value <> "" Then
                listbox2.Items.Clear()
                Dim qry As String = "select Str_Description,Default_Structure,seg_name1,seg_name2,seg_name3,seg_name4,seg_name5,seg_name6,seg_name7,seg_name8,seg_name9,seg_name10 from TSPL_GL_Structure where [Str_Code]='" + fnd_structurecode.Value + "'"
                dt = clsDBFuncationality.GetDataTable(qry)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                    For Each dr As DataRow In dt.Rows

                        radtxt_structcode.Text = dr(0).ToString()
                        'Dim strstructure As String = dr(1).ToString()
                        'If strstructure = "yes" Then
                        '    chk_defaultstruct.Enabled = False
                        '    chk_defaultstruct.Checked = True
                        'Else : strstructure = "no"
                        '    chk_defaultstruct.Enabled = True
                        '    chk_defaultstruct.Checked = False
                        'End If
                        strsegname1 = dr(2).ToString()
                        strsegname2 = dr(3).ToString()
                        strsegname3 = dr(4).ToString()
                        strsegname4 = dr(5).ToString()
                        strsegname5 = dr(6).ToString()
                        strsegname6 = dr(7).ToString()
                        strsegname7 = dr(8).ToString()
                        strsegname8 = dr(9).ToString()
                        strsegname9 = dr(10).ToString()
                        strsegname10 = dr(11).ToString()

                        Dim i As Integer
                        For i = 2 To 11
                            If Not String.IsNullOrEmpty(dr(i).ToString()) Then
                                listbox2.Items.Add(dr(i))
                            End If
                        Next
                        listbox1.Items.Clear()
                        Dim strquery As String = "select seg_name from tspl_gl_segment where seg_name  not in('" + strsegname1 + "', '" + strsegname2 + "','" + strsegname3 + "','" + strsegname4 + "','" + strsegname5 + "','" + strsegname6 + "','" + strsegname7 + "','" + strsegname8 + "','" + strsegname9 + "','" + strsegname10 + "') "
                        '' Added by abhishek as on 12/10/2012
                        Dim dt1 As DataTable
                        dt1 = clsDBFuncationality.GetDataTable(strquery)
                        If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                            For Each dr1 As DataRow In dt1.Rows
                                listbox1.Items.Add(dr1(0).ToString())
                            Next
                        End If


                        'For Each litem As RadListDataItem In listbox2.Items
                        '    For Each litem1 As RadListDataItem In listbox2.Items
                        '        If litem.Text = litem1.Text Then
                        '            listbox1.Items.Remove(litem)
                        '        End If
                        '    Next
                        'Next
                        btn_save.Enabled = True
                        btn_save.Text = "Update"
                        btn_delete.Enabled = True
                        radtxt_structcode.Enabled = True
                        listbox1.Enabled = True
                        listbox2.Enabled = True
                        radtxt_structcode.Enabled = True
                        'chk_defaultstruct.Enabled = True
                        btn_save.Enabled = True
                        btn_delete.Enabled = True
                        btn_include.Enabled = True
                        btn_exclude.Enabled = True
                    Next
                End If
            Else
                btn_save.Enabled = True
                btn_save.Text = "Save"
                radtxt_structcode.Enabled = True
            End If
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    '***** Bind segments in listbox1 on the form load by this Binddata method....
    Private Sub binddata()
        Try
            ''added by abhishek as on 12/10/2012
            Dim qry As String = "select seg_name from tspl_gl_segment"
            dt = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    listbox1.Items.Add(dr(0).ToString())
                Next
            End If

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    '***** Segments Transfer from listbox1 to listbox2....
    Private Sub btn_include_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_include.Click
        Try
            listbox2.Items.Add(listbox1.SelectedItem)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, "Select segment from listbox1", Me.Text)
        End Try
    End Sub
    '***** Segments Transfer from listbox2 to listbox1....

    Private Sub btn_exclude_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_exclude.Click
        Try
            listbox1.Items.Add(listbox2.SelectedItem)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, "Select segment from listbox2", Me.Text)
        End Try
    End Sub
    '***** If text of save button is 'save' then call the savebutton method or the text of save button is 'update' 
    'then call updatebutton method....
    Private Sub btn_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_save.Click

        SaveData()
    End Sub
    '***** insert row in the table from this method....
    Sub SaveData()
        If allowtosave() Then
            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.accountStructure, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            If fnd_structurecode.Value = "" Then
                myMessages.blankValue(Me, "Structure Code", Me.Text)
                fnd_structurecode.Focus()
            Else
                If btn_save.Text = "Save" Then
                    savebutton()
                ElseIf btn_save.Text = "Update" Then
                    updatebutton()
                End If
            End If
        End If
    End Sub
    Private Sub savebutton()
        Dim trans As SqlTransaction = Nothing
        Try
            'Dim i As Integer = listbox1.Items.Count
            'For Each litem As RadListDataItem In listbox1.Items
            '    Dim str As String = litem.Text
            '    Dim ii As Integer = litem.RowIndex
            'Next
            'Dim chk As String
            'If chk_defaultstruct.Checked = True Then
            '    chk = "yes"
            'ElseIf chk_defaultstruct.Checked = False Then
            '    chk = "No"
            'End If
            trans = clsDBFuncationality.GetTransactin()
            clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_GL_STRUCTURE_INSERT", New SqlParameter("@Str_Code", fnd_structurecode.Value), New SqlParameter("@Str_Description", radtxt_structcode.Text), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
            Dim strsegno As String
            Dim strseglength As String
            For Each litem As RadListDataItem In listbox2.Items
                Dim s As String = "select seg_no, seg_length from tspl_gl_segment where seg_name = '" + litem.Text + "'"
                '' added by abhishek as on 12/10/2012
                dt = clsDBFuncationality.GetDataTable(s, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows
                        strsegno = dr(0).ToString()
                        strseglength = dr(1).ToString()
                
                Dim strupdate As String = "update TSPL_GL_STRUCTURE set seg_no" + (litem.RowIndex + 1).ToString() + " = '" + strsegno.ToString() + "', seg_name" + (litem.RowIndex + 1).ToString() + " = '" + litem.Text + "', seg_length" + (litem.RowIndex + 1).ToString() + " = '" + strseglength + "' where str_code = '" + fnd_structurecode.Value.Trim() + "'"
                        connectSql.RunSqlTransaction(trans, strupdate)
                    Next
                End If

            Next


            clsDBFuncationality.ExecuteNonQuery("update TSPL_GL_STRUCTURE set Seg_No1=isnull(Seg_No1,0),Seg_No2=isnull(Seg_No2,0),Seg_No3=isnull(Seg_No3,0),Seg_No4=isnull(Seg_No4,0),Seg_No5=isnull(Seg_No5,0),Seg_No6=isnull(Seg_No6,0),Seg_No7=isnull(Seg_No7,0),Seg_No8=isnull(Seg_No8,0),Seg_No9=isnull(Seg_No9,0),Seg_No10=isnull(Seg_No10,0) where Str_Code='" + fnd_structurecode.Value.Trim() + "'", trans)

            trans.Commit()
            myMessages.insert()
            btn_delete.Enabled = True
            btn_save.Text = "Update"
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Function allowtosave() As Boolean
        Try
            Dim isFound As Boolean = False
            For Each li As RadListDataItem In listbox2.Items
                If (clsCommon.CompairString("Accounts", li.Text) = CompairStringResult.Equal) Then
                    isFound = True
                    Exit For
                End If
            Next
            If Not isFound Then
                common.clsCommon.MyMessageBoxShow(Me, " Accounts must contain in Structure Code '" + fnd_structurecode.Value + "' ")
                fnd_structurecode.Focus()
                Return False
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
        Return True
    End Function
    '***** Update data from the table by this method....

    Private Sub updatebutton()
        Dim trans As SqlTransaction = Nothing
        Try
            'Dim str As String = "delete from TSPL_GL_STRUCTURE where str_code = '" + fnd_structurecode.Value + "'"
            'connectSql.RunSql(str)
            'Dim chk As String
            'If chk_defaultstruct.Checked = True Then
            '    chk = "yes"
            'ElseIf chk_defaultstruct.Checked = False Then
            '    chk = "No"
            'End If
            trans = clsDBFuncationality.GetTransactin()
            clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_GL_STRUCTURE_UPDATE", New SqlParameter("@Str_Code", fnd_structurecode.Value), New SqlParameter("@Str_Description", radtxt_structcode.Text), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
            Dim strsegno As String
            Dim strseglength As String
            Dim i As Integer = listbox2.Items.Count
            For Each litem As RadListDataItem In listbox2.Items
                '' added by abhishek as on 12/10/2012
                Dim s As String = "select seg_no, seg_length from tspl_gl_segment where seg_name = '" + litem.Text + "'"
                dt = clsDBFuncationality.GetDataTable(s, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows
                        strsegno = dr(0).ToString()
                        strseglength = dr(1).ToString()
                        Dim strupdate As String = "update TSPL_GL_STRUCTURE set seg_no" + (litem.RowIndex + 1).ToString() + " = '" + strsegno.ToString() + "', seg_name" + (litem.RowIndex + 1).ToString() + " = '" + litem.Text + "', seg_length" + (litem.RowIndex + 1).ToString() + " = '" + strseglength + "' where str_code = '" + fnd_structurecode.Value.Trim() + "'"
                        connectSql.RunSqlTransaction(trans, strupdate)
                    Next
                End If
            Next
            For seg As Integer = i To 9
                strsegno = connectSql.RunScalar(trans, "select seg_no, seg_length from tspl_gl_segment where seg_name = null")
                strseglength = connectSql.RunScalar(trans, "select seg_no, seg_length from tspl_gl_segment where seg_name = null")
                Dim strupdate As String = "update TSPL_GL_STRUCTURE set seg_no" + (seg + 1).ToString() + " = null, seg_name" + (seg + 1).ToString() + " = null, seg_length" + (seg + 1).ToString() + " = null where str_code = '" + fnd_structurecode.Value.Trim() + "'"
                connectSql.RunSqlTransaction(trans, strupdate)
                ' seg = seg + 1
            Next
            allowtosave()
            clsDBFuncationality.ExecuteNonQuery("update TSPL_GL_STRUCTURE set Seg_No1=isnull(Seg_No1,0),Seg_No2=isnull(Seg_No2,0),Seg_No3=isnull(Seg_No3,0),Seg_No4=isnull(Seg_No4,0),Seg_No5=isnull(Seg_No5,0),Seg_No6=isnull(Seg_No6,0),Seg_No7=isnull(Seg_No7,0),Seg_No8=isnull(Seg_No8,0),Seg_No9=isnull(Seg_No9,0),Seg_No10=isnull(Seg_No10,0) where Str_Code='" + fnd_structurecode.Value.Trim() + "'", trans)

            trans.Commit()
            myMessages.update()
            btn_delete.Enabled = True
            btn_save.Text = "Update"
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub
    '***** When leave the structure code then enabled the button,listbox and textbox
    Private Sub fnd_structurecode_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        leavecode()
    End Sub
    Sub leavecode()
        If fnd_structurecode.Value <> "" Then
            radtxt_structcode.Enabled = True
            'chk_defaultstruct.Enabled = True
            btn_save.Enabled = True
            btn_include.Enabled = True
            btn_exclude.Enabled = True
            listbox1.Enabled = True
            listbox2.Enabled = True
        End If
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    '***** Close the application....
    Private Sub btn_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Me.Close()
    End Sub
    '***** Load event of the finder,a new window open and i pick the structure code from there.Caption of the window is 'Structure Code'....
    Private Sub fnd_structurecode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fnd_structurecode.ConnectionString = connectSql.SqlCon()
        'fnd_structurecode.Query = "select [Str_Code] as [Structure Code],[Str_Description] as [Description] from TSPL_GL_STRUCTURE"
        'fnd_structurecode.ValueToSelect = "Structure Code"
        'fnd_structurecode.Caption = "Structure Code"
        'fnd_structurecode.ValueToSelect1 = "Structure Code"
    End Sub
    '*****After confirm the delete message , user delete a row from the table if data are not used by any other form.
    'if the particular row used in other form then a message display 'The Data Cannot be Deleted'
    Private Sub btn_delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        DeleteData()

    End Sub
    Sub DeleteData()
        Try
            If myMessages.deleteConfirm() Then
                funDelete()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    '***** Delete Method Call from Here....
    Private Sub funDelete()
        Try
            connectSql.RunSp("SP_TSPL_GL_STRUCTURE_DELETE", New SqlParameter("@Str_Code", fnd_structurecode.Value))
            myMessages.delete()
            btn_save.Text = "Save"
            'fnd_structurecode.Value = ""
            'radtxt_structcode.Enabled = False
            'radtxt_structcode.Text = ""
            'chk_defaultstruct.Enabled = False
            'chk_defaultstruct.Checked = False
            'btn_save.Enabled = False
            btn_delete.Enabled = False
            'btn_include.Enabled = False
            'btn_exclude.Enabled = False
            'listbox1.Enabled = False
            'listbox2.Enabled = False
            'listbox1.Items.Clear()
            'listbox2.Items.Clear()
            fnd_structurecode.Focus()
            'dr = connectSql.RunSqlReturnDR("select seg_name from tspl_gl_segment where seg_name <> ''")
            'While dr.Read()
            '    listbox1.Items.Add(dr(0).ToString())
            'End While
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    '***** Import data from excel seat in a proper way.

    Private Sub radmenu_import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radmenu_import.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Str_Code", "Str_Description", "Seg_No1", "Seg_Name1", "Seg_length1", "Seg_No2", "Seg_Name2", "Seg_length2", "Seg_No3", "Seg_Name3", "Seg_length3", "Seg_No4", "Seg_Name4", "Seg_length4", "Seg_No5", "Seg_Name5", "Seg_length5", "Seg_No6", "Seg_Name6", "Seg_length6", "Seg_No7", "Seg_Name7", "Seg_length7", "Seg_No8", "Seg_Name8", "Seg_length8", "Seg_No9", "Seg_Name9", "Seg_length9", "Seg_No10", "Seg_Name10", "Seg_length10") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim strsructurecode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim strstructuredesc As String = clsCommon.myCstr(grow.Cells(1).Value)
                    Dim strsegno1 As String = clsCommon.myCstr(grow.Cells(2).Value)
                    Dim strsegname1 As String = clsCommon.myCstr(grow.Cells(3).Value)
                    Dim strseglength1 As String = clsCommon.myCstr(grow.Cells(4).Value)
                    Dim strsegno2 As String = clsCommon.myCstr(grow.Cells(5).Value)
                    Dim strsegname2 As String = clsCommon.myCstr(grow.Cells(6).Value)
                    Dim strseglength2 As String = clsCommon.myCstr(grow.Cells(7).Value)
                    Dim strsegno3 As String = clsCommon.myCstr(grow.Cells(8).Value)
                    Dim strsegname3 As String = clsCommon.myCstr(grow.Cells(9).Value)
                    Dim strseglength3 As String = clsCommon.myCstr(grow.Cells(10).Value)
                    Dim strsegno4 As String = clsCommon.myCstr(grow.Cells(11).Value)
                    Dim strsegname4 As String = clsCommon.myCstr(grow.Cells(12).Value)
                    Dim strseglength4 As String = clsCommon.myCstr(grow.Cells(13).Value)
                    Dim strsegno5 As String = clsCommon.myCstr(grow.Cells(14).Value)
                    Dim strsegname5 As String = clsCommon.myCstr(grow.Cells(15).Value)
                    Dim strseglength5 As String = clsCommon.myCstr(grow.Cells(16).Value)
                    Dim strsegno6 As String = clsCommon.myCstr(grow.Cells(17).Value)
                    Dim strsegname6 As String = clsCommon.myCstr(grow.Cells(18).Value)
                    Dim strseglength6 As String = clsCommon.myCstr(grow.Cells(19).Value)
                    Dim strsegno7 As String = clsCommon.myCstr(grow.Cells(20).Value)
                    Dim strsegname7 As String = clsCommon.myCstr(grow.Cells(21).Value)
                    Dim strseglength7 As String = clsCommon.myCstr(grow.Cells(22).Value)
                    Dim strsegno8 As String = clsCommon.myCstr(grow.Cells(23).Value)
                    Dim strsegname8 As String = clsCommon.myCstr(grow.Cells(24).Value)
                    Dim strseglength8 As String = clsCommon.myCstr(grow.Cells(25).Value)
                    Dim strsegno9 As String = clsCommon.myCstr(grow.Cells(26).Value)
                    Dim strsegname9 As String = clsCommon.myCstr(grow.Cells(27).Value)
                    Dim strseglength9 As String = clsCommon.myCstr(grow.Cells(28).Value)
                    Dim strsegno10 As String = clsCommon.myCstr(grow.Cells(29).Value)
                    Dim strsegname10 As String = clsCommon.myCstr(grow.Cells(30).Value)
                    Dim strseglength10 As String = clsCommon.myCstr(grow.Cells(31).Value)
                    'Dim str32 As String = grow.Cells(32).Value.ToString()
                    If strsructurecode = String.Empty Then
                        Throw New Exception("Structure Code can not Be left Blank")
                    End If

                    If clsCommon.myLen(strsructurecode) > 12 Then
                        Throw New Exception("Structure Code length cannot be more than 10")
                    End If

                    If clsCommon.myLen(strstructuredesc) > 60 Then
                        Throw New Exception("Structure Code Description cannot be more than 50")
                    End If

                    If clsCommon.myLen(strsegname1) > 30 Then
                        Throw New Exception("Length Of Seg_name1 cannot be more than 30")
                    End If

                    If clsCommon.myLen(strsegname2) > 30 Then
                        Throw New Exception("Length Of Seg_name2 cannot be more than 30")
                    End If

                    If clsCommon.myLen(strsegname3) > 30 Then
                        Throw New Exception("Length Of Seg_name3 cannot be more than 30")
                    End If

                    If clsCommon.myLen(strsegname4) > 30 Then
                        Throw New Exception("Length Of Seg_name4 cannot be more than 30")
                    End If

                    If clsCommon.myLen(strsegname5) > 30 Then
                        Throw New Exception("Length Of Seg_name5 cannot be more than 30")
                    End If

                    If clsCommon.myLen(strsegname6) > 30 Then
                        Throw New Exception("Length Of Seg_name6 cannot be more than 30")
                    End If

                    If clsCommon.myLen(strsegname7) > 30 Then
                        Throw New Exception("Length Of Seg_name7 cannot be more than 30")
                    End If

                    If clsCommon.myLen(strsegname8) > 30 Then
                        Throw New Exception("Length Of Seg_name8 cannot be more than 30")
                    End If

                    If clsCommon.myLen(strsegname9) > 30 Then
                        Throw New Exception("Length Of Seg_name9 cannot be more than 30")
                    End If

                    If clsCommon.myLen(strsegname10) > 30 Then
                        Throw New Exception("Length Of Seg_name10 cannot be more than 30")
                    End If

                    'If str5 > 6 Then
                    '    myMessages.myExceptions("Seg_length1 cannot Be More Than 6")
                    '    trans.Rollback()
                    '    Exit Sub
                    'End If
                    If String.IsNullOrEmpty(strseglength1) Then
                        strseglength1 = "0"
                    End If
                    Dim rg As Regex = New Regex("^[0-6]{1}$")
                    If Not rg.IsMatch(strseglength1) Then

                        Throw New Exception("Seg_length1 must between 0-6")
                    End If

                    If String.IsNullOrEmpty(strseglength2) Then
                        strseglength2 = "0"
                    End If
                    Dim rg1 As Regex = New Regex("^[0-5]{1}$")
                    If Not rg1.IsMatch(strseglength2) Then
                        Throw New Exception("Seg_length2 must between 0-5")
                    End If

                    If String.IsNullOrEmpty(strseglength3) Then
                        strseglength3 = "0"
                    End If
                    Dim rg2 As Regex = New Regex("^[0-5]{1}$")
                    If Not rg2.IsMatch(strseglength3) Then
                        Throw New Exception("Seg_length3 must between 0-5")
                    End If

                    If String.IsNullOrEmpty(strseglength4) Then
                        strseglength4 = "0"
                    End If
                    Dim rg3 As Regex = New Regex("^[0-5]{1}$")
                    If Not rg3.IsMatch(strseglength4) Then
                        Throw New Exception("Seg_length4 must between 0-5")
                    End If

                    If String.IsNullOrEmpty(strseglength5) Then
                        strseglength5 = "0"
                    End If
                    Dim rg4 As Regex = New Regex("^[0-5]{1}$")
                    If Not rg4.IsMatch(strseglength5) Then
                        Throw New Exception("Seg_length5 must between 0-5")
                    End If

                    If String.IsNullOrEmpty(strseglength6) Then
                        strseglength6 = "0"
                    End If
                    Dim rg5 As Regex = New Regex("^[0-5]{1}$")
                    If Not rg5.IsMatch(strseglength6) Then
                        Throw New Exception("Seg_length6 must between 0-5")
                    End If

                    If String.IsNullOrEmpty(strseglength7) Then
                        strseglength7 = "0"
                    End If
                    Dim rg6 As Regex = New Regex("^[0-5]{1}$")
                    If Not rg6.IsMatch(strseglength7) Then
                        Throw New Exception("Seg_length7 must between 0-5")
                    End If

                    If String.IsNullOrEmpty(strseglength8) Then
                        strseglength8 = "0"
                    End If
                    Dim rg7 As Regex = New Regex("^[0-5]{1}$")
                    If Not rg7.IsMatch(strseglength8) Then
                        Throw New Exception("Seg_length8 must between 0-5")
                    End If

                    If String.IsNullOrEmpty(strseglength9) Then
                        strseglength9 = "0"
                    End If
                    Dim rg8 As Regex = New Regex("^[0-5]{1}$")
                    If Not rg8.IsMatch(strseglength9) Then
                        Throw New Exception("Seg_length9 must between 0-5")
                    End If

                    If String.IsNullOrEmpty(strseglength10) Then
                        strseglength10 = "0"
                    End If
                    Dim rg9 As Regex = New Regex("^[0-5]{1}$")
                    If Not rg9.IsMatch(strseglength10) Then
                        Throw New Exception("Seg_length10 must between 0-5")
                    End If

                    Dim sql1 As String = "select count(*) from TSPL_GL_STRUCTURE where Str_Code='" + strsructurecode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        'Dim Sql As String = "insert into TSPL_GL_STRUCTURE values('" + str + "','" + str1 + "','" + str2 + "','" + str3 + "','" + str4 + "','" + str5 + "','" + str6 + "','" + str7 + "','" + str8 + "','" + str9 + "','" + str10 + "','" + str11 + "','" + str12 + "','" + str13 + "','" + str14 + "','" + str15 + "','" + str16 + "','" + str17 + "','" + str18 + "','" + str19 + "','" + str20 + "','" + str21 + "','" + str22 + "','" + str23 + "','" + str24 + "','" + str25 + "','" + str26 + "','" + str27 + "','" + str28 + "','" + str29 + "','" + str30 + "','" + str31 + "','" + str32 + "','" + userCode + "','" + connectSql.serverDate() + "','" + userCode + "','" + connectSql.serverDate() + "','" + companyCode + "')"
                        'connectSql.RunSqlTransaction(trans, Sql)
                        connectSql.RunSpTransaction(trans, "SP_TSPL_GL_STRUCTURE_INSERT1", New SqlParameter("@Str_Code", strsructurecode), New SqlParameter("@Str_Description", strstructuredesc), New SqlParameter("@Seg_No1", strsegno1), New SqlParameter("@Seg_Name1", strsegname1), New SqlParameter("@Seg_length1", strseglength1), New SqlParameter("@Seg_No2", strsegno2), New SqlParameter("@Seg_Name2", strsegname2), New SqlParameter("@Seg_length2", strseglength2), New SqlParameter("@Seg_No3", strsegno3), New SqlParameter("@Seg_Name3", strsegname3), New SqlParameter("@Seg_length3", strseglength3), New SqlParameter("@Seg_No4", strsegno4), New SqlParameter("@Seg_Name4", strsegname4), New SqlParameter("@Seg_length4", strseglength4), New SqlParameter("@Seg_No5", strsegno5), New SqlParameter("@Seg_Name5", strsegname5), New SqlParameter("@Seg_length5", strseglength5), New SqlParameter("@Seg_No6", strsegno6), New SqlParameter("@Seg_Name6", strsegname6), New SqlParameter("@Seg_length6", strseglength6), New SqlParameter("@Seg_No7", strsegno7), New SqlParameter("@Seg_Name7", strsegname7), New SqlParameter("@Seg_length7", strseglength7), New SqlParameter("@Seg_No8", strsegno8), New SqlParameter("@Seg_Name8", strsegname8), New SqlParameter("@Seg_length8", strseglength8), New SqlParameter("@Seg_No9", strsegno9), New SqlParameter("@Seg_Name9", strsegname9), New SqlParameter("@Seg_length9", strseglength9), New SqlParameter("@Seg_No10", strsegno10), New SqlParameter("@Seg_Name10", strsegname10), New SqlParameter("@Seg_length10", strseglength10), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                    Else
                        connectSql.RunSpTransaction(trans, "SP_TSPL_GL_STRUCTURE_UPDATE1", New SqlParameter("@Str_Code", strsructurecode), New SqlParameter("@Str_Description", strstructuredesc), New SqlParameter("@Seg_No1", strsegno1), New SqlParameter("@Seg_Name1", strsegname1), New SqlParameter("@Seg_length1", strseglength1), New SqlParameter("@Seg_No2", strsegno2), New SqlParameter("@Seg_Name2", strsegname2), New SqlParameter("@Seg_length2", strseglength2), New SqlParameter("@Seg_No3", strsegno3), New SqlParameter("@Seg_Name3", strsegname3), New SqlParameter("@Seg_length3", strseglength3), New SqlParameter("@Seg_No4", strsegno4), New SqlParameter("@Seg_Name4", strsegname4), New SqlParameter("@Seg_length4", strseglength4), New SqlParameter("@Seg_No5", strsegno5), New SqlParameter("@Seg_Name5", strsegname5), New SqlParameter("@Seg_length5", strseglength5), New SqlParameter("@Seg_No6", strsegno6), New SqlParameter("@Seg_Name6", strsegname6), New SqlParameter("@Seg_length6", strseglength6), New SqlParameter("@Seg_No7", strsegno7), New SqlParameter("@Seg_Name7", strsegname7), New SqlParameter("@Seg_length7", strseglength7), New SqlParameter("@Seg_No8", strsegno8), New SqlParameter("@Seg_Name8", strsegname8), New SqlParameter("@Seg_length8", strseglength8), New SqlParameter("@Seg_No9", strsegno9), New SqlParameter("@Seg_Name9", strsegname9), New SqlParameter("@Seg_length9", strseglength9), New SqlParameter("@Seg_No10", strsegno10), New SqlParameter("@Seg_Name10", strsegname10), New SqlParameter("@Seg_length10", strseglength10), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
    '***** Export Table Data in Excelsheet....
    Private Sub radmenu_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radmenu_export.Click
        Dim Sql As String = "Select Str_Code, Str_Description, Seg_No1, Seg_Name1, Seg_length1, Seg_No2, Seg_Name2, Seg_length2, Seg_No3, Seg_Name3, Seg_length3, Seg_No4, Seg_Name4, Seg_length4, Seg_No5, Seg_Name5, Seg_length5, Seg_No6, Seg_Name6, Seg_length6, Seg_No7, Seg_Name7, Seg_length7, Seg_No8, Seg_Name8, Seg_length8, Seg_No9, Seg_Name9, Seg_length9, Seg_No10, Seg_Name10, Seg_length10 from TSPL_GL_STRUCTURE"
        ListImpExpColumnsMandatory = New List(Of String)({"Str_Code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Str_Code"})
        transportSql.ExporttoExcel(Sql, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub
    '***** After Click of the reset button except close button and structure code ,all the button ,listbox 
    'and textbox are desabled.

    Private Sub btn_reset_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_reset.Click
        funreset()
    End Sub
    Sub funreset()
        Try
            fnd_structurecode.MyReadOnly = False
            fnd_structurecode.Value = ""
            'radtxt_structcode.Enabled = False
            radtxt_structcode.Text = ""
            'chk_defaultstruct.Enabled = False
            'chk_defaultstruct.Checked = False
            'btn_save.Enabled = False
            btn_delete.Enabled = False
            btn_include.Enabled = False
            btn_exclude.Enabled = False
            listbox1.Enabled = False
            listbox2.Enabled = False
            listbox1.Items.Clear()
            listbox2.Items.Clear()
            fnd_structurecode.Focus()
            '' added by abhishek as on 12/10/2012
            Dim qry As String = "select seg_name from tspl_gl_segment where seg_name <> ''"
            dt = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    listbox1.Items.Add(dr(0).ToString())
                Next
            End If
            btn_save.Text = "Save"
            fnd_structurecode.Focus()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'priti mam added on 01-06-2011 --- To implement the access control
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "ACCT-STRUC-M"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btn_save.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btn_delete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    Private Sub fnd_structurecode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fnd_structurecode._MYValidating
        Dim str As String = "select count(*) from TSPL_GL_STRUCTURE   where  Str_Code ='" + fnd_structurecode.Value + "' "

        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))

        If no = 0 Then
            fnd_structurecode.MyReadOnly = False
        Else
            fnd_structurecode.MyReadOnly = True
        End If
        If fnd_structurecode.MyReadOnly OrElse isButtonClicked Then

            'Dim qry As String = "select [Str_Code] as [StructureCode],[Str_Description] as [Description] from TSPL_GL_STRUCTURE"
            'fnd_structurecode.Value = clsCommon.ShowSelectForm("fmSTR_code", qry, "StructureCode", "", fnd_structurecode.Value, "", isButtonClicked)
            fnd_structurecode.Value = clsGLStructure.getFinder("", fnd_structurecode.Value, isButtonClicked)
            LoadData()
        End If
        leavecode()
    End Sub

    Private Sub fnd_structurecode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fnd_structurecode._MYNavigator
        Dim qst As String = "select [Str_Code] as [StructureCode],[Str_Description] as [Description] from TSPL_GL_STRUCTURE where 2=2 "
        Select Case NavType
            Case NavigatorType.Current
                qst += " and TSPL_GL_STRUCTURE .Str_Code in ('" + fnd_structurecode.Value + "')"
            Case NavigatorType.Next
                qst += " and TSPL_GL_STRUCTURE .Str_Code in (select min(Str_Code ) from TSPL_GL_STRUCTURE where Str_Code  >'" + fnd_structurecode.Value + "')"
            Case NavigatorType.First
                qst += " and TSPL_GL_STRUCTURE .Str_Code in (select MIN(Str_Code ) from TSPL_GL_STRUCTURE)"

            Case NavigatorType.Last
                qst += " and TSPL_GL_STRUCTURE .Str_Code in (select Max(Str_Code ) from TSPL_GL_STRUCTURE)"
            Case NavigatorType.Previous
                qst += " and TSPL_GL_STRUCTURE .Str_Code in (select Max(Str_Code ) from TSPL_GL_STRUCTURE where Str_Code  <'" + fnd_structurecode.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fnd_structurecode.Value = clsCommon.myCstr(dt.Rows(0)("StructureCode"))
            radtxt_structcode.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        LoadData()
    End Sub

    
End Class
