'***** Created By:- Raghvendra Kumar Vats *****
'*****Table :- TSPL_TERMS_MASTER
'Column - Terms code(Primary Key),Terms desc,No Of Days....
'Start Date -- 23rd May
'End Date -- 23rd May
''BM00000000544
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports exz = Microsoft.Office.Interop.Excel
Imports common

Public Class frmPaymentTerms
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
#Region "Variables"
    Dim sql As String
    Dim ds As DataSet
    Dim dt1 As DataTable
    Dim tableName As String = "TSPL_TERMS_MASTER"
    Dim tableCode As String = "TERMS_Code"
    Dim codePrefix As String = "TERMS"
    Dim dt As Date = Date.Today
#End Region




    Private Sub SetUserMgmtNew()
        '' Anubhooti 30-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.paymentTerms)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btn_save.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btn_save.Visible = True Then
            RadMenuItem2.Enabled = True
            RadMenuItem3.Enabled = True
        Else
            RadMenuItem2.Enabled = False
            RadMenuItem3.Enabled = False
        End If
        '--------------------------------------------------
        btn_delete.Visible = MyBase.isDeleteFlag
    End Sub



    '***** On the form load, only desabled the delete buttons.Fix the finder length and declare hendlers....
    Private Sub FrmTermsMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        SetDataBaseGrid()
        ButtonToolTip.SetToolTip(btn_save, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btn_delete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btn_close, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btn_reset, "Press Alt+N Adding New Trasnaction")
        'globalFunc.mandatoryText(fnd_termscode.Value, txt_desc, txt_no_of_days)
        ToolTipTerms.SetToolTip(btn_reset, "New")
        AddHandler fnd_termscode.KeyPress, AddressOf KeyPress1
        AddHandler txt_no_of_days.KeyPress, AddressOf keypress2
        AddHandler fnd_termscode.TextChanged, AddressOf TextChanged1
        AddHandler fnd_termscode.Leave, AddressOf Leave1
        'fnd_termscode.txtValue.CharacterCasing = CharacterCasing.Upper
        fnd_termscode.MyMaxLength = 12
        fnd_termscode.BackColor = Color.White
        'fnd_termscode.TabIndex = 0
        'txt_desc.TabIndex = 1
        'txt_no_of_days.TabIndex = 2
        btn_delete.Enabled = False
        btn_save.Enabled = True
        chkAdvance.Checked = False
        txtAdvancePer.Enabled = False
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        'LoadDataErrorType()
    End Sub
    '******** Structure Code Typed in upper case....
    Private Sub KeyPress1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    '***** keypress2 is the handler of no_of_days.No_of_days only accept numeric value ....
    Private Sub keypress2(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 48) _
                 Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 57) Then
            e.Handled = True
            If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
                e.Handled = False
            End If
            If (Microsoft.VisualBasic.Asc(e.KeyChar) = 46) Then
                e.Handled = False
            End If
            If (Microsoft.VisualBasic.Asc(e.KeyChar) = 32) Then
                e.Handled = False
            End If
        End If

    End Sub
    '***** Use Handler To Fill Structure Code.If structure code is not blank then fill the corresponding data

    Private Sub TextChanged1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim termscode As String
            termscode = clsDBFuncationality.getSingleValue("select * from TSPL_TERMS_MASTER where [Terms_Code] ='" + fnd_termscode.Value + "'")
            Dim terms_code As String = ""
            If clsCommon.myLen(termscode) > 0 Then
                terms_code = termscode
            End If
            If terms_code <> fnd_termscode.Value Then
                'fndCustomerId.txtValue.Text = ""
                txt_desc.Text = ""
                txt_no_of_days.Text = ""
                btn_save.Enabled = True
                btn_save.Text = "Save"
            Else
                funFill()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    '***** Fill the data based on terms code that are picked from the terms code finder....
    Private Sub funFill()
        Try
            If fnd_termscode.Value <> "" Then
                dt1 = clsDBFuncationality.GetDataTable("select * from TSPL_TERMS_MASTER where [Terms_Code]='" + fnd_termscode.Value + "'")
                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                    For Each dr As DataRow In dt1.Rows
                        txt_desc.Text = dr(1).ToString()
                        txt_no_of_days.Text = dr(2).ToString()
                        ''richa Ticket no.BM00000003438 on 20/08/2014
                        If clsCommon.myCdbl(dr("LCRequired").ToString()) = 0 Then
                            chkLCRequired.Checked = False
                        Else
                            chkLCRequired.Checked = True
                        End If
                        ''-----------------------------------
                        If clsCommon.myCdbl(dr("isAdvance").ToString()) = 0 Then
                            chkAdvance.Checked = False
                        Else
                            chkAdvance.Checked = True
                            txtAdvancePer.Text = clsCommon.myCstr(dr("Advance_Per").ToString())
                        End If
                        'If clsCommon.myLen(clsCommon.myCstr(dr("Due_Date_By").ToString())) > 0 Then
                        '    cboDueDate.SelectedValue = clsCommon.myCstr(dr("Due_Date_By").ToString())
                        'End If

                    Next
                End If
              
                btn_save.Enabled = True
                btn_save.Text = "Update"
                btn_delete.Enabled = True
                txt_desc.Enabled = True
            Else
                btn_save.Enabled = True
                btn_save.Text = "Save"
                txt_desc.Enabled = True
                ''richa Ticket no.BM00000003438 on 20/08/2014
                txt_desc.Text = ""
                chkLCRequired.Checked = False
                txt_no_of_days.Text = ""
                ''------------------------------------
            End If
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    '***** After Click of the reset button except close, save button and terms code ,all the button  
    'and textbox are desabled.
    Private Sub funreset()
        Try
            fnd_termscode.Value = ""
            fnd_termscode.MyReadOnly = False
            txt_desc.Text = ""
            txt_no_of_days.Text = ""
            ''richa Ticket no.BM00000003438 on 20/08/2014
            chkLCRequired.Checked = False
            ''-----------------------------------
            btn_save.Enabled = True
            'txt_desc.Enabled = False
            'btn_save.Enabled = False
            btn_delete.Enabled = False
            chkAdvance.Checked = False
            txtAdvancePer.Text = ""
            txtAdvancePer.Enabled = False
            'cboDueDate.SelectedIndex = 0
            btn_save.Text = "Save"
            fnd_termscode.Focus()
            SetDataBaseGrid()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    '***** Load event of the finder,a new window open and i pick the terms code from there.Caption of the window is 'Terms Code'....

    'Private Sub fnd_termscode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fnd_termscode.ConnectionString = connectSql.SqlCon()
    '    fnd_termscode.Query = "select [Terms_Code] as [Terms Code],[Terms_Desc] as [Description] from TSPL_TERMS_MASTER"
    '    fnd_termscode.ValueToSelect = "Terms Code"
    '    fnd_termscode.Caption = "Terms Code"
    '    fnd_termscode.ValueToSelect1 = "Terms Code"
    'End Sub
    '***** If text of save button is 'save' then call the funinsert method or the text of save button is 'update' 
    'then call funupdate method....
    Private Sub btn_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_save.Click
        SaveData()
    End Sub
    Public Sub SaveData()
        Try
            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.paymentTerms, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso clsCommon.myLen(fnd_termscode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Terms Code cannot be left blank", Me.Text)
                fnd_termscode.Focus()
                Return
            ElseIf txt_desc.Text = "" Then
                myMessages.blankValue("Terms Description")
                txt_desc.Focus()
            ElseIf txt_no_of_days.Text = "" Then
                myMessages.blankValue("No_of_days")
                txt_no_of_days.Focus()
            ElseIf chkAdvance.Checked = True AndAlso txtAdvancePer.Text = String.Empty Then
                myMessages.blankValue("Advance Percentage")
                txtAdvancePer.Focus()

                'ElseIf clsCommon.CompairString(cboDueDate.Text, "Select") = CompairStringResult.Equal Then
                '    clsCommon.MyMessageBoxShow("Please Select Due Date")
                '    cboDueDate.Focus()
                '    Return
            ElseIf chkAdvance.Checked = True AndAlso clsCommon.myCdbl(txtAdvancePer.Text) >= 100 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter Advance(%) Value less then 100 .", Me.Text)
                txtAdvancePer.Focus()
            ElseIf btn_save.Text = "Save" Then

                funInsert()
            Else
                funUpdate()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub btn_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_close.Click
        closeform()
    End Sub
    Public Sub closeform()
        Me.Close()
    End Sub
    '***** insert row in the table from this method....

    Private Sub funInsert()
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from tspl_terms_master where Terms_Code='" & fnd_termscode.Value & "'")
                If ChkNewEntry = 0 Then
                    fnd_termscode.Value = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.PaymentTerms, "", "")
                    If clsCommon.myLen(fnd_termscode.Value) <= 0 Then
                        Throw New Exception("Error in Terms Code Generation")
                    End If
                End If
            End If
            ''richa Ticket no.BM00000003438 on 20/08/2014 add LCRequired in procedure
            clsDBFuncationality.SaveAStorePorcedure("sp_tspl_terms_masterinsert", New SqlParameter("@code", fnd_termscode.Value), New SqlParameter("@desc", txt_desc.Text), New SqlParameter("@no_days", txt_no_of_days.Text), New SqlParameter("@crt_by", userCode), New SqlParameter("@crtdate", connectSql.serverDate()), New SqlParameter("@mod_by", userCode), New SqlParameter("@mod_date", connectSql.serverDate()), New SqlParameter("@comp_code", companyCode), New SqlParameter("@LCRequired", IIf(chkLCRequired.Checked, 1, 0)))
            'common.clsCommon.MyMessageBoxShow("Data Saved sucessfully")
            '=============================================================================
            ' Ticket No : BHA/09/05/18-000010 , BHA/09/05/18-000012 By Prabhakar Anand
            Dim coll As New Hashtable()
            If clsCommon.myLen(fnd_termscode.Value) > 0 Then
                clsCommon.AddColumnsForChange(coll, "isAdvance", IIf(chkAdvance.Checked, 1, 0))
                If chkAdvance.Checked = True Then
                    clsCommon.AddColumnsForChange(coll, "Advance_Per", txtAdvancePer.Text)
                End If

                ' clsCommon.AddColumnsForChange(coll, "Due_Date_By", cboDueDate.SelectedValue)
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_terms_master", OMInsertOrUpdate.Update, "Terms_Code='" + fnd_termscode.Value + "'")
            End If
            '=============================================================================
            myMessages.insert()

            '  MsgBox("Saved Successfully", MsgBoxStyle.Information, Me.Text)
            btn_save.Text = "Update"
            btn_delete.Enabled = True
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    '***** Update data from the table by this method....

    Private Sub funUpdate()
        Try
            ''richa Ticket no.BM00000003438 on 20/08/2014 add LCRequired in procedure
            clsDBFuncationality.SaveAStorePorcedure("sp_tspl_terms_masterUpdate", New SqlParameter("@code", fnd_termscode.Value), New SqlParameter("@desc", txt_desc.Text), New SqlParameter("@no_days", txt_no_of_days.Text), New SqlParameter("@crt_by", userCode), New SqlParameter("@crtdate", connectSql.serverDate()), New SqlParameter("@mod_by", userCode), New SqlParameter("@mod_date", connectSql.serverDate()), New SqlParameter("@comp_code", companyCode), New SqlParameter("@LCRequired", IIf(chkLCRequired.Checked, 1, 0)))
            'common.clsCommon.MyMessageBoxShow("Data Updated sucessfully")

            '=============================================================================
            ' Ticket No : BHA/09/05/18-000010 , BHA/09/05/18-000012 By Prabhakar Anand
            Dim coll As New Hashtable()
            If clsCommon.myLen(fnd_termscode.Value) > 0 Then
                clsCommon.AddColumnsForChange(coll, "isAdvance", IIf(chkAdvance.Checked, 1, 0))
                If chkAdvance.Checked = True Then
                    clsCommon.AddColumnsForChange(coll, "Advance_Per", txtAdvancePer.Text)
                End If

                ' clsCommon.AddColumnsForChange(coll, "Due_Date_By", cboDueDate.SelectedValue)
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_terms_master", OMInsertOrUpdate.Update, "Terms_Code='" + fnd_termscode.Value + "'")
            End If
            '=============================================================================

            myMessages.update()
            ' MsgBox("Updated Successfully", MsgBoxStyle.Information, Me.Text)
            btn_save.Text = "Update"
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    '*****After confirm the delete message , user delete a row from the table....
    Private Sub btn_delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        DeleteData()
    End Sub
    Public Sub DeleteData()
        If clsCommon.myLen(fnd_termscode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Fill the Terms Code", Me.Text)
            fnd_termscode.Focus()
            Return
        End If
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
            clsDBFuncationality.SaveAStorePorcedure("sp_tspl_terms_masterdelete", New SqlParameter("@code", fnd_termscode.Value))
            myMessages.delete()
            btn_save.Text = "Save"
            btn_delete.Enabled = False
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    'Private Sub fnd_termscode_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fnd_termscode.Leave
    '    Try
    '        If fnd_termscode.txtValue.Text <> "" Then
    '            txt_desc.Enabled = True
    '            btn_save.Enabled = True
    '        Else
    '            btn_close.Focus()
    '        End If
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex.Message)
    '    End Try

    'End Sub
    '***** When leave the Terms code then enabled the button and textbox
    Private Sub Leave1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If fnd_termscode.Value <> "" Then
                txt_desc.Enabled = True
                btn_save.Enabled = True
            Else
                btn_close.Focus()
            End If
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    '***** Export Table Data in Excelsheet....

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        ''richa Ticket No.BM00000003438 on 20/08/2014
        sql = "Select Terms_Code as [Terms Code],Terms_Desc as [Terms Desc],No_Days as [No Days],LCRequired as [LC Required],isAdvance as [isAdvance]  , convert(varchar,Advance_Per) as [Advance(%)]  from TSPL_TERMS_MASTER"
        ListImpExpColumnsMandatory = New List(Of String)({"Terms Code", "Terms Desc", "No Days"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Terms Code"})
        transportSql.ExporttoExcel(sql, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    'Private Sub SaveFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
    '    Try
    '        Dim xlapp As exz.Application
    '        Dim xlworkbook As exz.Workbook
    '        Dim xlworksheet As exz.Worksheet
    '        Dim mis As Object = System.Reflection.Missing.Value
    '        Dim i As Int32, j As Int32
    '        Dim data1 As String
    '        xlapp = New exz.ApplicationClass
    '        xlworkbook = xlapp.Workbooks.Add(mis)
    '        xlworksheet = xlworkbook.Sheets("sheet1")
    '        Dim da1 As New SqlDataAdapter("select column_name from  information_schema.columns where TABLE_NAME= 'TSPL_TERMS_MASTER'", connectSql.SqlCon)
    '        Dim dt1 As New DataTable
    '        da1.Fill(dt1)
    '        For j = 0 To dt1.Columns.Count - 1
    '            For i = 0 To dt1.Rows.Count - 1
    '                data1 = dt1.Rows(i).ItemArray(j).ToString()
    '                xlworksheet.Cells(j + 1, i + 1) = data1
    '            Next
    '        Next
    '        Dim da As New SqlDataAdapter("select * from TSPL_TERMS_MASTER ", connectSql.SqlCon)
    '        Dim dt As New DataTable()
    '        da.Fill(dt)
    '        For i = 0 To dt.Rows.Count - 1
    '            For j = 0 To dt.Columns.Count - 1
    '                xlworksheet.Cells(i + 2, j + 1) = dt.Rows(i).Item(j).ToString()
    '            Next
    '        Next
    '        xlworkbook.SaveAs(SaveFileDialog1.FileName, exz.XlFileFormat.xlWorkbookNormal)

    '        'static  -  xlworkbook.SaveAs("D:\VATS\raghav.xls", exz.XlFileFormat.xlWorkbookNormal)
    '        xlworkbook.Close()
    '        xlapp.Quit()
    '        MsgBox("data transfer succesfully")
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex.Message)
    '    End Try
    'End Sub

    'Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
    '    Dim con As String = "provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Documents and Settings\vats-pc\Desktop\bn.xls; Extended Properties=Excel 8.0;"
    '    Dim oldb As New OleDb.OleDbConnection(con)
    '    Dim exdata As New OleDb.OleDbDataAdapter()
    '    Dim cmd As New OleDb.OleDbCommand()
    '    cmd.Connection = oldb
    '    cmd.CommandType = CommandType.Text
    '    cmd.CommandText = "select * from [sheet1$]"
    '    exdata.SelectCommand = cmd
    '    Dim ds As New DataSet()
    '    exdata.Fill(ds)
    '    ' DataGridView1.DataSource = ds.Tables(0).DefaultView
    '    'Dim cn As New SqlConnection("server=TSPL-07\SQL2008; database=TSPLERP; userCode id=sa; password=tecxpert;")
    '    'cn.Open()
    '    connectSql.SqlCon()
    '    Dim sqlbulk As SqlClient.SqlBulkCopy = New SqlClient.SqlBulkCopy(connectSql.SqlCon)
    '    sqlbulk.DestinationTableName = "TSPL_TERMS_MASTER"
    '    sqlbulk.WriteToServer(ds.Tables(0).CreateDataReader)
    '    MsgBox("Data Imported Successfully")
    'End Sub



    '--------------------------------------------------------------------------------------------------------------------'
    '***** Import data from excel seat in a proper way.

    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        ''richa Ticket No.BM00000003438 on 20/08/2014
        If transportSql.importExcel(gv, "Terms Code", "Terms Desc", "No Days", "LC Required", "isAdvance", "Advance(%)") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strtermscode As String = grow.Cells(0).Value.ToString()
                    Dim strtermsdesc As String = grow.Cells(1).Value.ToString()
                    Dim strno_of_days As String = grow.Cells(2).Value.ToString()
                    ''richa Ticket No.BM00000003438 on 20/08/2014
                    Dim strLCrequired As String = grow.Cells(3).Value.ToString()
                    Dim strisAdvanc As String = grow.Cells(4).Value.ToString()
                    Dim strAdvancePer As String = grow.Cells(5).Value.ToString()
                    '-------------------------------------
                    If strtermscode = String.Empty Then
                        myMessages.blankValue("Terms Code")
                        trans.Rollback()
                        Exit Sub
                    End If

                    If strtermsdesc = String.Empty Then
                        myMessages.blankValue("Terms Description")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strno_of_days = String.Empty Then
                        myMessages.blankValue("No_Of_Days")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strtermscode.Length > 10 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Terms Code length cannot be more than 10", Me.Text)
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strtermsdesc.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Terms Description length cannot be more than 50", Me.Text)
                        trans.Rollback()
                        Exit Sub
                    End If
                    'If String.IsNullOrEmpty(str2) Then
                    '    str2 = "0"
                    'End If
                    'Dim rg As Regex = New Regex("^[0-999]{1-3}$")
                    'If rg.IsMatch(str2) Then

                    '    myMessages.myExceptions("No_Of_Days Between 0 to 999")
                    '    trans.Rollback()
                    '    Exit Sub
                    'End If
                    If IsNumeric(grow.Cells(2).Value) Then
                        'Dim gh As d = Integer.Parse(grow.Cells(2).Value)
                        Dim dc As Decimal = Decimal.Parse(grow.Cells(2).Value)
                        If dc > 999 Or dc < 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "No_Of_Days must between 0 - 999")
                            trans.Rollback()
                            Exit Sub
                        Else
                            strno_of_days = grow.Cells(2).Value.ToString()
                        End If
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "No_of_days only accept numeric value")
                        trans.Rollback()
                        Exit Sub
                    End If
                    ''richa Ticket No.BM00000003438 on 20/08/2014
                    If IsNumeric(grow.Cells("LC Required").Value) Then
                        'Dim gh As d = Integer.Parse(grow.Cells(2).Value)
                        Dim dcv As Decimal = Decimal.Parse(grow.Cells("LC Required").Value)
                        If dcv > 1 Or dcv < 0 Then
                            common.clsCommon.MyMessageBoxShow("LC Required must be 0 or 1")
                            trans.Rollback()
                            Exit Sub
                        Else
                            strLCrequired = grow.Cells("LC Required").Value.ToString()
                        End If
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "LC Required only accept numeric value", Me.Text)
                        trans.Rollback()
                        Exit Sub
                    End If

                    ' isAdvance
                    If IsNumeric(grow.Cells("isAdvance").Value) Then
                        Dim dcv As Decimal = Decimal.Parse(grow.Cells("isAdvance").Value)
                        If dcv > 1 Or dcv < 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "isAdvance must be 0 or 1", Me.Text)
                            trans.Rollback()
                            Exit Sub
                        Else
                            strisAdvanc = grow.Cells("isAdvance").Value.ToString()
                        End If
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "isAdvance only accept numeric value", Me.Text)
                        trans.Rollback()
                        Exit Sub
                    End If

                    If strisAdvanc = 1 Then
                        If IsNumeric(grow.Cells("Advance(%)").Value) Then
                            Dim dcv As Decimal = Decimal.Parse(grow.Cells("Advance(%)").Value)
                            If dcv > 99 Then
                                common.clsCommon.MyMessageBoxShow(Me, "Advance(%) less then 100", Me.Text)
                                trans.Rollback()
                                Exit Sub
                            Else
                                strAdvancePer = grow.Cells("Advance(%)").Value.ToString()
                            End If
                        Else
                            common.clsCommon.MyMessageBoxShow(Me, "Advance(%) only accept numeric value", Me.Text)
                            trans.Rollback()
                            Exit Sub
                        End If
                    Else
                        strAdvancePer = 0
                    End If
                    ''-------------------------------------

                    Dim sql1 As String = "select count(*) from TSPL_TERMS_MASTER where Terms_Code='" + strtermscode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        'sql = "insert into TSPL_TERMS_MASTER values('" + str + "','" + str1 + "','" + str2 + "','" + userCode + "','" + connectSql.serverDate() + "','" + userCode + "','" + connectSql.serverDate() + "','" + companyCode + "')"
                        'connectSql.RunSqlTransaction(trans, sql)
                        connectSql.RunSpTransaction(trans, "sp_tspl_terms_masterinsert", New SqlParameter("@code", strtermscode), New SqlParameter("@desc", strtermsdesc), New SqlParameter("@no_days", strno_of_days), New SqlParameter("@crt_by", userCode), New SqlParameter("@crtdate", connectSql.serverDate(trans)), New SqlParameter("@mod_by", userCode), New SqlParameter("@mod_date", connectSql.serverDate(trans)), New SqlParameter("@comp_code", companyCode), New SqlParameter("@LCRequired", strLCrequired))
                    Else
                        connectSql.RunSpTransaction(trans, "sp_tspl_terms_masterUpdate", New SqlParameter("@code", strtermscode), New SqlParameter("@desc", strtermsdesc), New SqlParameter("@no_days", strno_of_days), New SqlParameter("@crt_by", userCode), New SqlParameter("@crtdate", connectSql.serverDate(trans)), New SqlParameter("@mod_by", userCode), New SqlParameter("@mod_date", connectSql.serverDate(trans)), New SqlParameter("@comp_code", companyCode), New SqlParameter("@LCRequired", strLCrequired))
                    End If

                    Dim coll As New Hashtable()
                    If clsCommon.myLen(fnd_termscode.Value) > 0 Then
                        clsCommon.AddColumnsForChange(coll, "isAdvance", IIf(chkAdvance.Checked, 1, 0))
                        clsCommon.AddColumnsForChange(coll, "Advance_Per", txtAdvancePer.Text)
                        ' clsCommon.AddColumnsForChange(coll, "Due_Date_By", cboDueDate.SelectedValue)
                        clsCommonFunctionality.UpdateDataTable(coll, "tspl_terms_master", OMInsertOrUpdate.Update, "Terms_Code='" + strtermscode + "'", trans)
                    End If
                Next
                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
                trans.Rollback()

            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
    '***** No_of_days must be between 0 to 999.....
    Private Sub txt_no_of_days_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_no_of_days.Leave
        If txt_no_of_days.Text <> "" Then
            Dim j As Integer = Integer.Parse(txt_no_of_days.Text)
            If j > 999 Or j < 0 Then
                MsgBox("Tax Should be Between 0 To 999", MsgBoxStyle.OkOnly, Me.Text)
                txt_no_of_days.Text = 0
                txt_no_of_days.Focus()
            End If
        End If
    End Sub
    'priti mam added on 01-06-2011 --- To implement the access control
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "PAYM-TERM-M"
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

    Private Sub btn_reset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_reset.Click
        funreset()
    End Sub

    Private Function GetReplecateCompaniesDataBase() As List(Of String)
        Dim arrDBName As New List(Of String)
        arrDBName.Add(objCommonVar.CurrDatabase)
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If (clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) Then
                arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
            End If
        Next
        Return arrDBName
    End Function

    Sub SetDataBaseGrid()
        gvDB.Rows.Clear()
        gvDB.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDB.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCompCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompCode.FormatString = ""
        repoCompCode.HeaderText = "Company Code"
        repoCompCode.Name = colCompCode
        repoCompCode.Width = 150
        repoCompCode.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompCode)

        Dim repoCompName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompName.FormatString = ""
        repoCompName.HeaderText = "Company Name"
        repoCompName.Name = colCompName
        repoCompName.Width = 150
        repoCompName.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompName)

        Dim repoDB As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDB.FormatString = ""
        repoDB.HeaderText = "Database Name"
        repoDB.Name = colDataBaseName
        repoDB.IsVisible = False
        repoDB.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoDB)

        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0 and Comp_Code not in ('" + objCommonVar.CurrentCompanyCode + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gvDB.Rows.AddNew()
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colSelect).Value = False
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompCode).Value = clsCommon.myCstr(dr("Comp_Code"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompName).Value = clsCommon.myCstr(dr("Comp_Name"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colDataBaseName).Value = clsCommon.myCstr(dr("DataBase_Name"))
            Next
        End If
    End Sub

    Private Sub frmPaymentTerms_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        'If e.Alt AndAlso e.Control AndAlso e.KeyCode = Keys.T Then
        '    Dim frm As frmPaymentTerms = New frmPaymentTerms()
        '    frm.ShowDialog()
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btn_save.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btn_delete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            closeform()

        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        End If
    End Sub

   
    Private Sub fnd_termscode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fnd_termscode._MYValidating
        Dim qst As String = "select count(*) from TSPL_TERMS_MASTER where Terms_Code='" + fnd_termscode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
        If count = 0 Then
            fnd_termscode.MyReadOnly = False
        Else
            fnd_termscode.MyReadOnly = True
        End If
        If fnd_termscode.MyReadOnly OrElse isButtonClicked Then

            'Dim qry As String = "select [Terms_Code] as Code,[Terms_Desc] as [Description] from TSPL_TERMS_MASTER"
            'fnd_termscode.Value = clsCommon.ShowSelectForm("Payment Term", qry, "Code", "", fnd_termscode.Value, "Terms_Code", isButtonClicked)
            fnd_termscode.Value = clsPaymentTerms.getFinder("", fnd_termscode.Value, isButtonClicked)
            'qry = "select Terms_Desc from TSPL_TERMS_MASTER where Terms_Code ='" + fnd_termscode.Value + "'"
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            '    txt_desc.Text = clsCommon.myCstr(dt.Rows(0)("Terms_Desc"))
            'Else
            '    txt_desc.Text = ""
            'End If
            LoadData()
        End If
    End Sub

    Private Sub LoadData()
        Try
            Dim termscode As String = clsDBFuncationality.getSingleValue("select Terms_Code from TSPL_TERMS_MASTER where [Terms_Code] ='" + fnd_termscode.Value + "'")
            Dim terms_code As String = ""
            If clsCommon.myLen(termscode) > 0 Then
                terms_code = termscode

            End If


            If clsCommon.CompairString(terms_code, fnd_termscode.Value) <> CompairStringResult.Equal Then ' terms_code <> fnd_termscode.Value
                'fndCustomerId.txtValue.Text = ""
                txt_desc.Text = ""
                txt_no_of_days.Text = ""
                ''richa Ticket no.BM00000003438 on 20/08/2014
                chkLCRequired.Checked = False
                '----------------------------
                btn_save.Enabled = True
                btn_save.Text = "Save"
            Else
                funFill()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub fnd_termscode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fnd_termscode._MYNavigator
        Dim qst As String = "select [Terms_Code] as [Terms Code],[Terms_Desc] as [Description] from TSPL_TERMS_MASTER where 2=2"
        Select Case NavType
            Case NavigatorType.Current
                qst += " and TSPL_TERMS_MASTER .Terms_Code in ('" + fnd_termscode.Value + "')"
            Case NavigatorType.Next
                qst += " and TSPL_TERMS_MASTER .Terms_Code in (select min(Terms_Code) from TSPL_TERMS_MASTER where Terms_Code  >'" + fnd_termscode.Value + "')"
            Case NavigatorType.First
                qst += " and TSPL_TERMS_MASTER .Terms_Code in (select MIN(Terms_Code) from TSPL_TERMS_MASTER)"

            Case NavigatorType.Last
                qst += " and TSPL_TERMS_MASTER .Terms_Code in (select Max(Terms_Code) from TSPL_TERMS_MASTER)"
            Case NavigatorType.Previous
                qst += " and TSPL_TERMS_MASTER .Terms_Code in (select Max(Terms_Code) from TSPL_TERMS_MASTER where Terms_Code  <'" + fnd_termscode.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        SetDataBaseGrid()
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fnd_termscode.Value = clsCommon.myCstr(dt.Rows(0)("Terms Code"))
            txt_desc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        LoadData()
    End Sub

    Private Sub txt_no_of_days_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_no_of_days.TextChanged

    End Sub

    Private Sub RadLabel3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadLabel3.Click

    End Sub

    Private Sub fnd_termscode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fnd_termscode.Load

    End Sub

    Private Sub RadLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadLabel1.Click

    End Sub

    Private Sub gvDB_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvDB.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    'Sub LoadDataErrorType()
    '    cboDueDate.DataSource = LoadDueDateType()
    '    cboDueDate.ValueMember = "Code"
    '    cboDueDate.DisplayMember = "Name"
    '    cboDueDate.SelectedIndex = 0
    'End Sub

    'Public Shared Function LoadDueDateType() As DataTable
    '    Dim dt As DataTable = New DataTable()
    '    dt.Columns.Add("Code", GetType(String))
    '    dt.Columns.Add("Name", GetType(String))
    '    Dim dr As DataRow = dt.NewRow()
    '    dr("Code") = "Select"
    '    dr("Name") = "Select"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Code") = "PD"
    '    dr("Name") = "PO Date"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Code") = "DD"
    '    dr("Name") = "Delivery Date"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Code") = "ID"
    '    dr("Name") = "Invoice Date"
    '    dt.Rows.Add(dr)

    '    Return dt
    'End Function

    Private Sub chkAdvance_CheckedChanged(sender As Object, e As EventArgs) Handles chkAdvance.CheckedChanged
        If chkAdvance.Checked = True Then
            txtAdvancePer.Enabled = True
        Else
            txtAdvancePer.Text = ""
            txtAdvancePer.Enabled = False
        End If
    End Sub
End Class
