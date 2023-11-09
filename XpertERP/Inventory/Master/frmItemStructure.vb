Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports Excel = Microsoft.Office.Interop.Excel
Imports common
'' CREATED BY : SURAJ
''Start Date: 10-05-2011
'' End Date:10-05-2011
Public Class frmItemStructure
    Inherits FrmMainTranScreen
    Dim dr As SqlDataReader
    Dim ds As New DataSet()
    Dim userCode, companyCode As String
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

    Private Sub frmItemStructure_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.itemStructure)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            mnimport.Enabled = True
            mnexport.Enabled = True
        Else
            mnimport.Enabled = False
            mnexport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub RadForm2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        'globalFunc.mandatoryText(fndstructurecode.Value)
        AddHandler fndstructurecode.TextChanged, AddressOf Text_Changed
        Dim strclassname As String = "select inv_class_name from TSPL_INV_CLASS "
        Dim dt As GridViewComboBoxColumn = TryCast(dgcaccountstrucuture.Columns(0), GridViewComboBoxColumn)
        ds = connectSql.RunSQLReturnDS(strclassname)
        dt.DataSource = ds.Tables(0)
        dt.DisplayMember = "inv_class_name"
        dt.ValueMember = "inv_class_name"
        AddHandler fndstructurecode.KeyPress, AddressOf key_press
        'fndstructurecode.Value.CharacterCasing = CharacterCasing.Upper
        fndstructurecode.MyMaxLength = 10

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''End of For Custom Fields
    End Sub
    ''To Authorised the user 
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "ITEM-STRUCT"
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
    '            btnsave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function
    ''To Convert character into the upper case
    Private Sub key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)


    End Sub
    ''To Call the funfill function (retrieve the information according to the structure code)
    Private Sub Text_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strstructurecode As String = "select structure_code from TSPL_STRUCTURE_MASTER where Structure_Code = '" + fndstructurecode.Value + "'"

        Dim strstructure As String = clsDBFuncationality.getSingleValue(strstructurecode)

        If strstructure <> "" Then
            funfill()
        Else
            txtdesc.Text = ""
            txtitemstructure.Text = ""
            txtlength.Text = ""
            dgcaccountstrucuture.DataSource = Nothing
            dgcaccountstrucuture.Rows.Clear()
            chkdefaultstructurecode.Checked = False
            btnsave.Text = "Save"
        End If
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
            If fndstructurecode.Value = "" Then
                clsCommon.MyMessageBoxShow(Me, "Structure Code cannot be blank")
                fndstructurecode.Focus()
                'ElseIf dgcaccountstrucuture.Rows(0).Cells(0).Value = String.Empty Then
                '    common.clsCommon.MyMessageBoxShow("Select one class ")
                Exit Sub
            End If
        End If
        
        If UcCustomFields1.AllowToSave() = False Then
            Exit Sub
        End If

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.itemStructure, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If

        If btnsave.Text = "Save" Then
            funinsert()
            funfill()
        Else : btnsave.Text = "Update"
            funupdate()
            funfill()
        End If

        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    ''To insert data into the table(TSPL_STRUCTURE_MASTER) by procedure
    Private Sub funinsert()
        Dim created_date As Date = Date.Today
        Dim modify_date As Date = Date.Today
        Dim strdefault As String
        If chkdefaultstructurecode.Checked = True Then
            strdefault = "Y"
        Else
            strdefault = "N"
        End If
        Dim trans As SqlTransaction = Nothing
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_STRUCTURE_MASTER where Structure_Code='" & fndstructurecode.Value.Trim() & "'", trans)
                If ChkNewEntry = 0 Then
                    fndstructurecode.Value = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.ItemStructure, "", "")
                    If clsCommon.myLen(fndstructurecode.Value) <= 0 Then
                        Throw New Exception("Error in Code Generation")
                    End If
                End If
            End If
            connectSql.OpenConnection()
            trans = clsDBFuncationality.GetTransactin()
            connectSql.RunSpTransaction(trans, "sp_TSPL_STRUCTURE_MASTER_insert", New SqlParameter("@structurecode", fndstructurecode.Value.Trim()), New SqlParameter("@structuredesc", txtdesc.Text.Trim()), New SqlParameter("@itemstructure", txtitemstructure.Text.Trim()), New SqlParameter("@totallength", txtlength.Text), New SqlParameter("@Default_struct", strdefault), New SqlParameter("@created_by", userCode), New SqlParameter("@created_date", connectSql.serverDate(trans)), New SqlParameter("@modify_by", userCode), New SqlParameter("@modify_date", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
            For i As Integer = 0 To dgcaccountstrucuture.Rows.Count - 1
                connectSql.RunSpTransaction(trans, "sp_TSPL_STRUCTURE_DETAIL_insert", New SqlParameter("@structurecode", fndstructurecode.Value.Trim()), New SqlParameter("@invclass", dgcaccountstrucuture.Rows(i).Cells(0).Value), New SqlParameter("@invdesc", dgcaccountstrucuture.Rows(i).Cells(1).Value), New SqlParameter("@created_by", userCode), New SqlParameter("@created_date", connectSql.serverDate(trans)), New SqlParameter("@modify_by", userCode), New SqlParameter("@modify_date", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
            Next

            ''For Custom Fields
            'obj.Form_ID = MyBase.Form_ID
            arrCustomFields = New List(Of clsCustomFieldValues)
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.GetData(arrCustomFields)
            End If
            '' custom fields
            clsCustomFieldValues.SaveData(MyBase.Form_ID, fndstructurecode.Value.Trim(), arrCustomFields, trans)
            ''End of For Custom Fields

            trans.Commit()
            myMessages.insert()
        Catch ex As Exception
            myMessages.myExceptions(ex)
            trans.Rollback()

        End Try
    End Sub
    ''To delete data from the table (TSPL_STRUCTURE_MASTER) and table (TSPL_STRUCTURE_Detail) by procedure
    Private Sub fundelete()
        Dim trans As SqlTransaction = Nothing
        Try
            connectSql.OpenConnection()
            trans = connectSql.OpenConnection.BeginTransaction()
            connectSql.RunSpTransaction(trans, "sp_TSPL_STRUCTURE_MASTER_delete", New SqlParameter("@structurecode", fndstructurecode.Value.Trim()))
            connectSql.RunSpTransaction(trans, "sp_TSPL_STRUCTURE_DETAIL_delete", New SqlParameter("@structurecode", fndstructurecode.Value.Trim()))

            '' custom fields
            clsCustomFieldValues.DeleteData(Me.Form_ID, fndstructurecode.Value.Trim(), trans)

            myMessages.delete()
            trans.Commit()
        Catch ex As Exception
            myMessages.myExceptions(ex)
            trans.Rollback()

        End Try
    End Sub
    ''To Reset all the field of the form
    Private Sub funreset()
        fndstructurecode.Value = ""
        fndstructurecode.MyReadOnly = False
        btnsave.Text = "Save"
        txtdesc.Text = ""
        txtitemstructure.Text = ""
        txtlength.Text = ""
        dgcaccountstrucuture.DataSource = Nothing
        dgcaccountstrucuture.Rows.Clear()
        chkdefaultstructurecode.Checked = False
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
    End Sub
    '' To Fill the value according to the structure code
    Private Sub funfill()
        Try
            Dim strstructurecode As String = "select structure_descq , item_structure, total_length, default_struct from TSPL_STRUCTURE_MASTER where Structure_Code = '" + fndstructurecode.Value.Trim() + "'"
            ' dr = connectSql.RunSqlReturnDR(strstructurecode)
            'Dim strlength As String
            ' Dim strintlength As Integer
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(strstructurecode)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    txtdesc.Text = dt.Rows(i)("structure_descq")
                    txtitemstructure.Text = dt.Rows(i)("item_structure").ToString().Trim()
                    txtlength.Text = dt.Rows(i)("total_length").ToString()
                    Dim strdefault As String = dt.Rows(i)("default_struct").ToString().Trim()
                    If strdefault = "Y" Then
                        chkdefaultstructurecode.Checked = True
                    Else
                        chkdefaultstructurecode.Checked = False
                    End If

                    ''For Custom Fields
                    If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                        UcCustomFields1.LoadData(fndstructurecode.Value.Trim())
                    End If
                    ''End of For Custom Fields
                Next
            End If



            'While dr.Read
            '    txtdesc.Text = dr(0).ToString()
            '    txtitemstructure.Text = dr(1).ToString().Trim()
            '    txtlength.Text = dr(2).ToString()
            '    Dim strdefault As String = dr(3).ToString().Trim()
            '    If strdefault = "Y" Then
            '        chkdefaultstructurecode.Checked = True
            '    Else
            '        chkdefaultstructurecode.Checked = False
            '    End If
            'End While
            Dim str As String = "select inv_class , inv_desc from TSPL_STRUCTURE_DETAIL where Structure_Code = '" + fndstructurecode.Value.Trim() + "'"
            dgcaccountstrucuture.AutoGenerateColumns = False
            transportSql.FillGridView(str, dgcaccountstrucuture)
            dgcaccountstrucuture.Columns(0).FieldName = "inv_class"
            dgcaccountstrucuture.Columns(1).FieldName = "inv_desc"
            'Dim da As New SqlDataAdapter("select inv_class , inv_desc from TSPL_STRUCTURE_DETAIL where Structure_Code = '" + fndstructurecode.Value.Text.Trim() + "'", connectSql.SqlCon())
            'Dim dt As New DataTable()
            'da.Fill(dt)
            'If dt.Rows.Count >= 0 Then
            '    Dim row As DataRow = dt.Rows(0)
            '    For Each r As Object In row.Table.Rows
            '        Dim rowcell As GridViewRowInfo = dgcaccountstrucuture.Rows.AddNew()
            '        rowcell.Cells(0).Value = r(0).ToString()
            '        rowcell.Cells(1).Value = r(1).ToString()
            '    Next
            'End If
            btnsave.Text = "Update"
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message.ToString())
        End Try
    End Sub
    Private Sub dgcaccountstrucuture_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgcaccountstrucuture.CellValueChanged
        If e.Column.Index = 0 Then
            Dim str As String = "select Inv_Class_Length  from TSPL_INV_CLASS where Inv_Class_Name = '" + dgcaccountstrucuture.CurrentRow.Cells(0).Value + "'"
            Dim InClLen As String = clsDBFuncationality.getSingleValue(str)
            dgcaccountstrucuture.CurrentRow.Cells(1).Value = InClLen
            txtlength.Text = InClLen
            'dr = connectSql.RunSqlReturnDR(str)
            'While dr.Read()
            '    dgcaccountstrucuture.CurrentRow.Cells(1).Value = dr(0)
            '    txtlength.Text = dr(0)
            'End While
        End If
        If e.ColumnIndex = 0 Or e.RowIndex >= 0 Then
            For Each row As GridViewRowInfo In dgcaccountstrucuture.Rows
                txtlength.Text = CInt(txtlength.Text) + row.Cells(1).Value
            Next
            Dim x As String = "X"
            For i As Integer = 0 To txtlength.Text - 2
                x = x + "X"
            Next
            txtitemstructure.Text = x
        End If
    End Sub
    ''To Update the data according to the structure code in the table
    Private Sub funupdate()
        Dim strdefault As String
        If chkdefaultstructurecode.Checked = True Then
            strdefault = "Y"
            Dim strdflt As String = connectSql.RunScalar("select structure_code from TSPL_STRUCTURE_MASTER where default_struct = 'Y'")
            If Not String.IsNullOrEmpty(strdflt) Then
                connectSql.RunSql("UPDATE TSPL_STRUCTURE_MASTER SET DEFAULT_STRUCT = 'N' WHERE structure_code='" + strdflt + "' ")
            End If
        Else
            strdefault = "N"
        End If
        Dim trans As SqlTransaction = Nothing
        Try
            connectSql.OpenConnection()
            trans = clsDBFuncationality.GetTransactin()
            connectSql.RunSpTransaction(trans, "sp_TSPL_STRUCTURE_MASTER_update", New SqlParameter("@structurecode", fndstructurecode.Value.Trim()), New SqlParameter("@structuredesc", txtdesc.Text.Trim()), New SqlParameter("@itemstructure", txtitemstructure.Text.Trim()), New SqlParameter("@totallength", txtlength.Text), New SqlParameter("@Default_struct", strdefault), New SqlParameter("@created_by", userCode), New SqlParameter("@created_date", connectSql.serverDate(trans)), New SqlParameter("@modify_by", userCode), New SqlParameter("@modify_date", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
            connectSql.RunSpTransaction(trans, "sp_TSPL_STRUCTURE_DETAIL_delete", New SqlParameter("@structurecode", fndstructurecode.Value.Trim()))
            For i As Integer = 0 To dgcaccountstrucuture.Rows.Count - 1
                connectSql.RunSpTransaction(trans, "sp_TSPL_STRUCTURE_DETAIL_insert", New SqlParameter("@structurecode", fndstructurecode.Value.Trim()), New SqlParameter("@invclass", dgcaccountstrucuture.Rows(i).Cells(0).Value), New SqlParameter("@invdesc", dgcaccountstrucuture.Rows(i).Cells(1).Value), New SqlParameter("@created_by", userCode), New SqlParameter("@created_date", connectSql.serverDate(trans)), New SqlParameter("@modify_by", userCode), New SqlParameter("@modify_date", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
            Next

            ''For Custom Fields
            'obj.Form_ID = MyBase.Form_ID
            arrCustomFields = New List(Of clsCustomFieldValues)
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.GetData(arrCustomFields)
            End If
            '' custom fields
            clsCustomFieldValues.SaveData(MyBase.Form_ID, fndstructurecode.Value.Trim(), arrCustomFields, trans)
            ''End of For Custom Fields
            trans.Commit()
            myMessages.update()

        Catch ex As Exception
            myMessages.myExceptions(ex)
            trans.Rollback()

        End Try
    End Sub
    ''to load the finder strucuture code
    'Private Sub fndstructurecode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndstructurecode.ConnectionString = connectSql.SqlCon()
    '    fndstructurecode.Query = "select structure_code as [Structure Code], structure_descq as [Description]from TSPL_STRUCTURE_MASTER"
    '    fndstructurecode.ValueToSelect = "Structure Code"
    '    fndstructurecode.Caption = "Structure Detail"
    '    fndstructurecode.ValueToSelect1 = "Description"
    'End Sub
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndstructurecode.Value = "" Then
            common.clsCommon.MyMessageBoxShow(Me, "Please enter the Structure Code")
            fndstructurecode.Focus()
        Else
            If myMessages.deleteConfirm() Then
                fundelete()
                funreset()
            End If
        End If
    End Sub
    Private Sub dgcaccountstrucuture_CellValidating(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellValidatingEventArgs) Handles dgcaccountstrucuture.CellValidating
        Dim column As GridViewDataColumn = e.Column
        If TypeOf e.Row Is GridViewRowInfo Then
            If column.HeaderText = "Class" Then
                For Each grow As GridViewDataRowInfo In dgcaccountstrucuture.Rows
                    If e.RowIndex <> grow.Index Then
                        If e.Value = grow.Cells(0).Value Then
                            e.Cancel = True
                        End If
                    End If
                Next
            End If
        End If
    End Sub
    ''to close the form
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    ''to avoid the ' enter in the textbox
    Private Sub txtdesc_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        Else
        End If
    End Sub
    ''To reset all the field in the form
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        funreset()
    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnclose.Click
        Me.Close()
    End Sub
    ''To export the data in the excel sheet
    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnexport.Click
        Dim str As String = "select m.Structure_Code as [Structure Code], m.Structure_Descq as [Description], m.Item_Structure as [Item Structure], m.Total_Length as [Total Length]  from TSPL_STRUCTURE_MASTER m "
        ListImpExpColumnsMandatory = New List(Of String)({"Structure Code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Structure Code"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub
    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Structure Code", "Description", "Item Structure", "Total Length") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()

                For Each grow As GridViewRowInfo In gv.Rows
                    Dim str As String = clsCommon.myCstr(grow.Cells("Structure Code").Value)
                    Dim str1 As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    Dim str2 As String = clsCommon.myCstr(grow.Cells("Item Structure").Value)
                    Dim Totallength As Integer = 0
                    Totallength = clsCommon.myCdbl(grow.Cells("Total Length").Value)
                    If String.IsNullOrEmpty(str) And clsCommon.myLen(str) > 12 Then
                        Throw New Exception("Structure Code has some incorrect values")
                    End If
                    If clsCommon.myLen(str1) > 50 Then
                        Throw New Exception("Description has some incorrect values")
                    End If
                    If clsCommon.myLen(str2) > 50 Then
                        Throw New Exception("Item Structure has some incorrect values")
                    End If
                    Dim sql1 As String = "select count(*) from TSPL_STRUCTURE_MASTER where Structure_Code ='" + str + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        Dim Sql As String = "insert into TSPL_STRUCTURE_MASTER (Structure_Code, Structure_Descq, Item_Structure, Total_Length , Created_By, Create_Date, Modify_By , Modify_Date, Comp_Code ) values ('" + str + "', '" + str1 + "', '" + str2 + "', '" & Totallength & "', '" + userCode + "', '" + connectSql.serverDate(trans) + "', '" + userCode + "', '" + connectSql.serverDate(trans) + "', '" + companyCode + "')"
                        connectSql.RunSqlTransaction(trans, Sql)
                    Else
                        Dim Sql As String = "UPDATE TSPL_STRUCTURE_MASTER set Structure_Descq='" & str1 & "', Item_Structure='" & str2 & "', Total_Length='" & Totallength & "' where Structure_Code='" & str & "' "
                        connectSql.RunSqlTransaction(trans, Sql)
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


    Private Sub dgcaccountstrucuture_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles dgcaccountstrucuture.UserDeletedRow
        Dim total As Decimal
        For j As Integer = 0 To dgcaccountstrucuture.Rows.Count - 1
            If Not String.IsNullOrEmpty(dgcaccountstrucuture.Rows(j).Cells(1).Value) Then
                total = total + dgcaccountstrucuture.Rows(j).Cells(1).Value
            End If
        Next
        txtlength.Text = total
        Dim x As String = "X"
        For i As Integer = 0 To total - 1
            x = x + "X"
        Next
        txtitemstructure.Text = x

    End Sub

    Private Sub fndstructurecode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndstructurecode._MYValidating
        Dim str As String = "select count(*) from TSPL_STRUCTURE_MASTER where structure_code ='" + fndstructurecode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fndstructurecode.MyReadOnly = False
        Else
            fndstructurecode.MyReadOnly = True
        End If

        If fndstructurecode.MyReadOnly OrElse isButtonClicked Then

            'Dim qry As String = "select structure_code as [Code], structure_descq as [Description]from TSPL_STRUCTURE_MASTER"
            'fndstructurecode.Value = clsCommon.ShowSelectForm("fnItemStructure", qry, "Code", "", fndstructurecode.Value, "", isButtonClicked)
            fndstructurecode.Value = clsItemStructureMaster.getFinder("", fndstructurecode.Value, isButtonClicked)
            LoadData()
        End If
    End Sub

    Private Sub fndstructurecode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndstructurecode._MYNavigator
        Dim qst As String = "select structure_code as [Code], structure_descq as [Description]from TSPL_STRUCTURE_MASTER where 2=2 "
        Select Case NavType
            Case NavigatorType.Current
                qst += " and TSPL_STRUCTURE_MASTER .structure_code in ('" + fndstructurecode.Value + "')"
            Case NavigatorType.Next
                qst += " and TSPL_STRUCTURE_MASTER .structure_code in (select min(structure_code ) from TSPL_STRUCTURE_MASTER where structure_code  >'" + fndstructurecode.Value + "')"
            Case NavigatorType.First
                qst += " and TSPL_STRUCTURE_MASTER .structure_code in (select MIN(structure_code ) from TSPL_STRUCTURE_MASTER)"

            Case NavigatorType.Last
                qst += " and TSPL_STRUCTURE_MASTER .structure_code in (select Max(structure_code ) from TSPL_STRUCTURE_MASTER)"
            Case NavigatorType.Previous
                qst += " and TSPL_STRUCTURE_MASTER .structure_code in (select Max(structure_code ) from TSPL_STRUCTURE_MASTER where structure_code  <'" + fndstructurecode.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndstructurecode.Value = clsCommon.myCstr(dt.Rows(0)("Code"))
            txtdesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        LoadData()
    End Sub
    Public Sub LoadData()
        Dim strstructurecode As String = "select structure_code from TSPL_STRUCTURE_MASTER where Structure_Code = '" + fndstructurecode.Value + "'"

        Dim strstructure As String = clsDBFuncationality.getSingleValue(strstructurecode)

        If strstructure <> "" Then
            funfill()
        Else
            txtdesc.Text = ""
            txtitemstructure.Text = ""
            txtlength.Text = ""
            dgcaccountstrucuture.DataSource = Nothing
            dgcaccountstrucuture.Rows.Clear()
            chkdefaultstructurecode.Checked = False
            btnsave.Text = "Save"
        End If
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    Private Sub lblCSaType_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub cboCSAType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs)

    End Sub
End Class
