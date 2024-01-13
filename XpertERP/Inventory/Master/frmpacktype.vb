Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports common
'' CREATED BY : SURAJ
''Start Date: 10-05-2011
'' End Date:10-05-2011
Public Class Frmpacktype
    Inherits FrmMainTranScreen
    Dim dr As DataTable
    Dim userCode, companyCode As String
    Dim ds As New DataSet()
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.packType)
        If Not (MyBase.isReadFlag) Then
            '--Preeti gupta-Ticket_no-[BM00000003145]
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
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for Delete")

    End Sub
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

    Private Sub Frmpacktype_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub Frmpacktype_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress

    End Sub
    Private Sub Frmpacktype_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Dim check As String = "" 'clsERPFuncationality.glaccountquery
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        'globalFunc.mandatoryText(fndclasstype.Value)
        globalFunc.mandatoryDropdown(ddlfinishedgood)
        fndclasstype.MyReadOnly = True
        fndclasstype.BackColor = Color.White
        'fndfinishedgood.txtValue.ReadOnly = True
        'fndfinishedgood.txtValue.BackColor = Color.White
        'Dim str As String = "select inv_class_code from TSPL_INV_CLASS_DETAILS where Inv_Class_Name NOT IN('" + fndfinishedgood.txtValue.Text.Trim() + "')"
        'Dim dt As GridViewComboBoxColumn = TryCast(dgvpacktype.Columns(0), GridViewComboBoxColumn)
        'ds = connectSql.RunSQLReturnDS(str)
        'dt.DataSource = ds.Tables(0)
        'dt.DisplayMember = "inv_class_code"
        'dt.ValueMember = "inv_class_code"
        AddHandler fndclasstype.TextChanged, AddressOf text_changed
        btndelete.Enabled = False
    End Sub
    ''To Authorised the user 
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "PACKTYPE-M"
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

    ''To call the function to fill the grid 
    Private Sub text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funfillgrid()
        funfill()

    End Sub
    ''To Insert the data into the table(TSPL_PACKTYPE_MASTER) and the table(TSPL_PACKTYPE_DETAIL)
    Private Sub funinsert()
        Try
            Dim strfathercode As String
            Dim strmothercode As String
            If ddlfathercode.Text = "Select" Or ddlfathercode.Text = "select" Then
                strfathercode = "NIL"
            Else
                strfathercode = ddlfathercode.Text
            End If
            If ddlmothercode.Text = "Select" Or ddlmothercode.Text = "select" Then
                strmothercode = "NIL"
            Else
                strmothercode = ddlmothercode.Text
            End If

            connectSql.RunSp("sp_TSPL_PACKTYPE_MASTER_insert", New SqlParameter("@class_type", fndclasstype.Value), New SqlParameter("@finishedgood", ddlfinishedgood.Text), New SqlParameter("@mothercode", strmothercode), New SqlParameter("@fathercode", strfathercode), New SqlParameter("@created_by", userCode), New SqlParameter("@created_date", connectSql.serverDate()), New SqlParameter("@modify_by", userCode), New SqlParameter("@modify_date", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            myMessages.insert()
        Catch ex As Exception
            ' If common.clsCommon.MyMessageBoxShow(ex.Message.ToString()) Then
            common.clsCommon.MyMessageBoxShow(Me, "Record Already Exist", Me.Text)
            ' End If
        End Try
    End Sub
    '' To delete the data from the table(TSPL_PACKTYPE_MASTER) and table(TSPL_PACKTYPE_DETAIL)
    Private Sub fundelete()
        Try
            connectSql.RunSp("sp_TSPL_PACKTYPE_MASTER_delete", New SqlParameter("@classtype", fndclasstype.Value), New SqlParameter("@finishedgood", ddlfinishedgood.Text))
            myMessages.delete()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub
    ''To update the data according to the Class type 
    Private Sub funupdate()
        Try
            Dim currentdate As Date = Date.Today
            Dim modify_date As Date = Date.Today
            connectSql.RunSp("sp_TSPL_PACKTYPE_MASTER_update", New SqlParameter("@class_type", fndclasstype.Value), New SqlParameter("@finishedgood", ddlfinishedgood.Text), New SqlParameter("@mothercode", ddlmothercode.Text), New SqlParameter("@fathercode", ddlfathercode.Text), New SqlParameter("@created_by", userCode), New SqlParameter("@created_date", currentdate), New SqlParameter("@modify_by", userCode), New SqlParameter("@modify_date", modify_date), New SqlParameter("@compcode", companyCode))
            myMessages.update()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub
    '' To reset all the field of the pack type screen
    Private Sub funreset()
        fndclasstype.Value = ""
        ddlfinishedgood.Text = "Select"
        ddlmothercode.Text = "Select"
        ddlfathercode.Text = "Select"
        btnsave.Text = "Save"
        btndelete.Enabled = False
    End Sub
    '' To Fill the grid according to the class type and finishd good
    Private Sub funfillgrid()
        Try
            Dim str As String = "select inv_class_code from TSPL_INV_CLASS_DETAILS where inv_class_name = '" + fndclasstype.Value.Trim() + "'"
            'transportSql.FillComboBox(str, ddlfinishedgood, "inv_class_code", "inv_class_code")
            ds = connectSql.RunSQLReturnDS(str)
            ddlfinishedgood.DataSource = ds.Tables(0)

            ddlfinishedgood.ValueMember = "inv_class_code"
            ddlfinishedgood.DisplayMember = "inv_class_code"
            ddlfinishedgood.Text = "Select"
            ddlfathercode.Text = "Select"
            ddlmothercode.Text = "Select"
            btnsave.Text = "Save"
            btndelete.Enabled = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message.ToString())
        End Try
    End Sub
    'Private Sub fndclasstype_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndclasstype.ConnectionString = connectSql.SqlCon()
    '    fndclasstype.Query = "select inv_class_name as [Class Name], Inv_Class_No as [Class No]  from TSPL_INV_CLASS order by Inv_Class_No"
    '    fndclasstype.ValueToSelect = "Class Name"
    '    fndclasstype.Caption = "Class Detail"
    '    fndclasstype.ValueToSelect1 = "Class No"
    'End Sub
    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Dim strpack As String = "select Parent, Child  from TSPL_INV_CLASS_DETAILS where Inv_Class_Name = '" + fndclasstype.Value + "' and Inv_Class_Code = '" + ddlfinishedgood.Text + "'"
        dr = clsDBFuncationality.GetDataTable(strpack)
        Dim father As String = ""
        Dim child As String = ""
        If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then

            father = dr.Rows(0)(0).ToString()
            child = dr.Rows(0)(1).ToString()

        End If
        If father = "Y" And child = "Y" Then
            If fndclasstype.Value = "" Then
                myMessages.blankValue(" Class Type")
                fndclasstype.Focus()
            ElseIf ddlfinishedgood.Text = "Select" Then
                myMessages.blankValue("finished good")
            ElseIf ddlmothercode.Text = "Select" Then
                myMessages.blankValue("Mother Code")
            ElseIf ddlfathercode.Text = "Select" Then
                myMessages.blankValue("Father Code")
            Else
                If btnsave.Text = "Save" Then
                    funinsert()
                    funfill()
                Else : btnsave.Text = "Update"
                    funupdate()
                    funfill()
                End If
            End If

        ElseIf father = "Y" And child = "N" Then
            If fndclasstype.Value = "" Then
                myMessages.blankValue(" Class Type")
                fndclasstype.Focus()
            ElseIf ddlfinishedgood.Text = "Select" Then
                myMessages.blankValue("finished good")
            ElseIf ddlfathercode.Text = "Select" Then
                myMessages.blankValue("Father Code")
            Else
                If btnsave.Text = "Save" Then
                    funinsert()
                    funfill()
                Else : btnsave.Text = "Update"
                    funupdate()
                    funfill()
                End If
            End If
        ElseIf child = "Y" And father = "N" Then
            If fndclasstype.Value = "" Then
                myMessages.blankValue(" Class Type")
                fndclasstype.Focus()
            ElseIf ddlfinishedgood.Text = "Select" Then
                myMessages.blankValue("finished good")
            ElseIf ddlmothercode.Text = "Select" Then
                myMessages.blankValue("Mother Code")
            Else
                If btnsave.Text = "Save" Then
                    funinsert()
                    funfill()
                Else : btnsave.Text = "Update"
                    funupdate()
                    funfill()
                End If
            End If
        Else
            If fndclasstype.Value = "" Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select the Class Type", Me.Text)
                fndclasstype.Focus()
            ElseIf ddlfinishedgood.Text = "Select" Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select the finished good", Me.Text)
            Else
                If btnsave.Text = "Save" Then
                    funinsert()
                    funfill()
                Else : btnsave.Text = "Update"
                    funupdate()
                    funfill()
                End If
            End If
        End If
    End Sub
    '' To call the funreset function to clear all the field of the screen
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funreset()
    End Sub
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If myMessages.deleteConfirm() Then
            fundelete()
            funreset()
        End If
    End Sub
    Private Sub ddlfathercode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlfathercode.SelectedIndexChanged
        Dim str2 As String = "select inv_class_code from TSPL_INV_CLASS_DETAILS where inv_class_name ='" + fndclasstype.Value + "' and Inv_Class_Code NOT IN ('" + ddlfinishedgood.Text + "') and Inv_Class_Code NOT IN('" + ddlfathercode.Text + "')"
        transportSql.FillComboBox(str2, ddlmothercode, "inv_class_code", "inv_class_code")
    End Sub
    Private Sub ddlfinishedgood_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlfinishedgood.SelectedIndexChanged
        ddlfathercode.DataSource = Nothing
        Dim str12 As String = "select inv_class_code from TSPL_INV_CLASS_DETAILS where inv_class_name ='" + fndclasstype.Value + "' and Inv_Class_Code <> '" + ddlfinishedgood.Text + "'"
        transportSql.FillComboBox(str12, ddlfathercode, "inv_class_code", "inv_class_code")
        ddlfathercode.Text = "Select"
        ddlmothercode.Text = "Select"
        If fndclasstype.Value <> "" And ddlfathercode.Text <> "" And ddlfinishedgood.Text <> "" And ddlmothercode.Text <> "" And btnsave.Text = "&Update" Then
            btndelete.Enabled = True
        Else
            btndelete.Enabled = False
        End If

    End Sub
    Private Sub btnreset_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        funreset()
    End Sub

    Private Sub mnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnclose.Click
        Me.Close()
    End Sub

    Private Sub mnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnexport.Click
        Dim str As String = "select Class_Type as [Class Type] , Finished_Goods as [Finished Good], Mother_Code as [Mother Code], Father_Code as [Father Code] , Created_By as [Created By], Created_Date as [Created Date], Modify_By as [Modify By], Modify_Date as [Modify Date], Comp_Code as [Company Code]  from TSPL_PACKTYPE_MASTER "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub mnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Class Type", "Finished Good", "Mother Code", "Father Code") Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strclasstype As String = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim strfinishedgood As String = clsCommon.myCstr(grow.Cells(1).Value)
                    Dim strmothercode As String = clsCommon.myCstr(grow.Cells(2).Value)
                    Dim strfathercode As String = clsCommon.myCstr(grow.Cells(3).Value)
                    If String.IsNullOrEmpty(strclasstype) Then
                        Throw New Exception("Class Type has some incorrect values")

                    End If
                    If String.IsNullOrEmpty(strfinishedgood) Then
                        Throw New Exception("Finished Good has some incorrect values")

                    End If
                    If String.IsNullOrEmpty(strmothercode) Then
                        Throw New Exception("Mother Code has some incorrect values")

                    End If
                    If String.IsNullOrEmpty(strfathercode) Then
                        Throw New Exception("Father Code has some incorrect values")

                    End If
                    Dim sql1 As String = "select count(*) from TSPL_PACKTYPE_MASTER where Class_Type ='" + strclasstype + "' and Finished_Goods = '" + strfinishedgood + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        Dim Sql As String = "insert into TSPL_PACKTYPE_MASTER (Class_Type, Finished_Goods, Mother_Code, Father_Code, Created_By, Created_Date, Modify_By, Modify_Date, Comp_Code ) values ('" + strclasstype + "', '" + strfinishedgood + "', '" + strmothercode + "', '" + strfathercode + "','" + userCode + "','" + connectSql.serverDate(trans) + "', '" + userCode + "', '" + connectSql.serverDate(trans) + "', '" + companyCode + "')"
                        connectSql.RunSqlTransaction(trans, Sql)
                    Else
                        Dim sql As String = "update TSPL_PACKTYPE_MASTER set Mother_Code = '" + strmothercode + "',Finished_Goods= '" + strfinishedgood + "', Father_Code = '" + strfathercode + "' where Class_Type = '" + strclasstype + "' "
                        connectSql.RunSqlTransaction(trans, sql)
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
    Private Sub funfill()
        If fndclasstype.Value <> "" And ddlfinishedgood.Text <> "select" Then

            Try
                '            Dim str As String = "select finished_goods, mother_code, father_code from TSPl_packtype_master where class_type = '" + fndclasstype.txtValue.Text + "' and finished_goods='" + ddlfinishedgood.Text + "'"
                Dim str As String = "select finished_goods, (CASE mother_code WHEN 'NIL' THEN 'select' else Mother_Code  end ) as [Mother_Code], (case father_code when 'NIL' THEN 'select' else Father_Code  end )as [Father_Code] from TSPl_packtype_master where class_type = '" + fndclasstype.Value + "' and finished_goods='" + ddlfinishedgood.Text + "'"
                dr = clsDBFuncationality.GetDataTable(str)
                If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    ddlfinishedgood.Text = dr.Rows(0)(0).ToString()
                    ddlmothercode.Text = dr.Rows(0)(1).ToString()
                    ddlfathercode.Text = dr.Rows(0)(2).ToString()
                End If
                If fndclasstype.Value <> "" And ddlfathercode.Text <> "Select" And ddlfinishedgood.Text <> "Select" Or ddlmothercode.Text <> "Select" Then
                    btnsave.Text = "Update"
                Else
                    btnsave.Text = "Save"
                End If
                'If userCode <> "ADMIN" Then
                '    If funSetUserAccess() = False Then Exit Sub
                'End If
            Catch ex As Exception

            End Try
        End If
        If fndclasstype.Value <> "" And ddlfathercode.Text <> "" And ddlfinishedgood.Text <> "" And ddlmothercode.Text <> "" And btnsave.Text = "&Update" Then
            btndelete.Enabled = True
        Else
            btndelete.Enabled = False
        End If
    End Sub

    Private Sub ddlfinishedgood_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlfinishedgood.SelectedValueChanged
        If fndclasstype.Value <> "" And ddlfinishedgood.Text <> "Select" Then
            funfill()
        End If
        If fndclasstype.Value <> "" And ddlfathercode.Text <> "" And ddlfinishedgood.Text <> "" And ddlmothercode.Text <> "" And btnsave.Text = "Update" Then
            btndelete.Enabled = True
        Else
            btndelete.Enabled = False
        End If
        Dim checkdata As Integer = connectSql.RunScalar("select COUNT(*) from TSPL_PACKTYPE_MASTER where Class_Type = '" + fndclasstype.Value + "' and Finished_Goods = '" + ddlfinishedgood.Text + "'")
        If checkdata = 0 Then
            btnsave.Text = "Save"
        Else
            btnsave.Text = "Update"
            btndelete.Enabled = True
        End If


    End Sub

    
    Private Sub fndclasstype__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndclasstype._MYValidating
        Dim qry As String = "select inv_class_name as Name, Inv_Class_No as [Class No]  from TSPL_INV_CLASS "
        fndclasstype.Value = clsCommon.ShowSelectForm("classTypefd", qry, "Name", "", fndclasstype.Value, "Inv_Class_No", isButtonClicked)
        LoadData()

    End Sub
    Sub LoadData()
        funfillgrid()
        funfill()
    End Sub
End Class
