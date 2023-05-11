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
Public Class frmItemGroup
    Inherits FrmMainTranScreen

    Private Sub frmItemGroup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            ' DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.itemGroups)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
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
        'btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub RadForm3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

        globalFunc.mandatoryDropdown(ddlclassname)
        funfillclass()
    End Sub
    Dim dr As SqlDataReader
    Dim userCode, companyCode As String
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    ''To Insert data into the Table(TSPL_INV_CLASS_DETAILS)
    Private Sub funinsert()
        Dim strpack As String = connectSql.RunScalar("select Inv_Class_Name  from TSPL_INV_CLASS  where Class_Type = 'Pack Type'")

        Dim trans As SqlTransaction = Nothing
        Try
            connectSql.OpenConnection()
            trans = clsDBFuncationality.GetTransactin()
            Dim strparent As String
            Dim strchild As String
            If ddlclassname.Text = strpack Then
                connectSql.RunSpTransaction(trans, "sp_TSPL_INV_CLASS_DETAILS_delete", New SqlParameter("@classname", ddlclassname.Text))
                For i As Integer = 0 To dgvclassgroup.Rows.Count - 1
                    If Not IsDBNull(dgvclassgroup.Rows(i).Cells(0).Value) Then
                        If dgvclassgroup.Rows(i).Cells(2).Value = True Then
                            strparent = "Y"
                        Else
                            strparent = "N"
                        End If
                        If dgvclassgroup.Rows(i).Cells(3).Value = True Then
                            strchild = "Y"
                        Else
                            strchild = "N"
                        End If
                        connectSql.RunSpTransaction(trans, "sp_TSPL_INV_CLASS_DETAILS_insert", New SqlParameter("@classname", ddlclassname.Text.ToString().Trim()), New SqlParameter("@classcode", dgvclassgroup.Rows(i).Cells(0).Value), New SqlParameter("@classdesc", dgvclassgroup.Rows(i).Cells(1).Value), New SqlParameter("@parent", strparent), New SqlParameter("@child", strchild), New SqlParameter("@created_by", userCode), New SqlParameter("@created_date", connectSql.serverDate(trans)), New SqlParameter("Modify_By", userCode), New SqlParameter("Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                    End If
                Next
            Else
                connectSql.RunSpTransaction(trans, "sp_TSPL_INV_CLASS_DETAILS_delete", New SqlParameter("@classname", ddlclassname.Text))
                For i As Integer = 0 To dgvclassgroup.Rows.Count - 1
                    If Not IsDBNull(dgvclassgroup.Rows(i).Cells(0).Value) Then
                        connectSql.RunSpTransaction(trans, "sp_TSPL_INV_CLASS_DETAILS_insert", New SqlParameter("@classname", ddlclassname.Text.ToString().Trim()), New SqlParameter("@classcode", dgvclassgroup.Rows(i).Cells(0).Value), New SqlParameter("@classdesc", dgvclassgroup.Rows(i).Cells(1).Value), New SqlParameter("@created_by", userCode), New SqlParameter("@created_date", connectSql.serverDate(trans)), New SqlParameter("Modify_By", userCode), New SqlParameter("Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                    End If
                Next
            End If
            trans.Commit()
            myMessages.insert()
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)

        End Try
    End Sub
    ''To Delete data from the table (TSPL_INV_CLASS_DETAILS)
    Private Sub fundelete()
        Try
            connectSql.RunSp("sp_TSPL_INV_CLASS_DETAILS_delete", New SqlParameter("@classname", ddlclassname.Text.ToString.Trim()))
            myMessages.delete()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub
    ''To Reset all The field of screen
    Private Sub funreset()
        dgvclassgroup.DataSource = Nothing
        dgvclassgroup.Rows.Clear()
        ddlclassname.Text = "Select"
        btnsave.Text = "Save"
    End Sub
    Private Sub funupdate()
        Dim strpack As String = connectSql.RunScalar("select Inv_Class_Name  from TSPL_INV_CLASS  where Class_Type = 'Pack Type'")

        Dim trans As SqlTransaction = Nothing
        Try
            connectSql.OpenConnection()
            trans = clsDBFuncationality.GetTransactin()
            Dim strparent As String
            Dim strchild As String
            If ddlclassname.Text = strpack Then
                connectSql.RunSpTransaction(trans, "sp_TSPL_INV_CLASS_DETAILS_delete", New SqlParameter("@classname", ddlclassname.Text))
                For i As Integer = 0 To dgvclassgroup.Rows.Count - 1
                    If Not IsDBNull(dgvclassgroup.Rows(i).Cells(0).Value) Then
                        If dgvclassgroup.Rows(i).Cells(2).Value = True Then
                            strparent = "Y"
                        Else
                            strparent = "N"
                        End If
                        If dgvclassgroup.Rows(i).Cells(3).Value = True Then
                            strchild = "Y"
                        Else
                            strchild = "N"
                        End If
                        connectSql.RunSpTransaction(trans, "sp_TSPL_INV_CLASS_DETAILS_insert", New SqlParameter("@classname", ddlclassname.Text.ToString().Trim()), New SqlParameter("@classcode", dgvclassgroup.Rows(i).Cells(0).Value), New SqlParameter("@classdesc", dgvclassgroup.Rows(i).Cells(1).Value), New SqlParameter("@parent", strparent), New SqlParameter("@child", strchild), New SqlParameter("@created_by", userCode), New SqlParameter("@created_date", connectSql.serverDate(trans)), New SqlParameter("Modify_By", userCode), New SqlParameter("Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                    End If
                Next
            Else
                connectSql.RunSpTransaction(trans, "sp_TSPL_INV_CLASS_DETAILS_delete", New SqlParameter("@classname", ddlclassname.Text))
                For i As Integer = 0 To dgvclassgroup.Rows.Count - 1
                    If Not IsDBNull(dgvclassgroup.Rows(i).Cells(0).Value) Then
                        connectSql.RunSpTransaction(trans, "sp_TSPL_INV_CLASS_DETAILS_insert", New SqlParameter("@classname", ddlclassname.Text.ToString().Trim()), New SqlParameter("@classcode", dgvclassgroup.Rows(i).Cells(0).Value), New SqlParameter("@classdesc", dgvclassgroup.Rows(i).Cells(1).Value), New SqlParameter("@created_by", userCode), New SqlParameter("@created_date", connectSql.serverDate(trans)), New SqlParameter("Modify_By", userCode), New SqlParameter("Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                    End If
                Next
            End If
            trans.Commit()
            myMessages.update()
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)

        End Try
    End Sub
    ''To FILL the combobox 
    Private Sub funfillclass()
        Try

            Dim strclass As String = "select inv_class_name from TSPL_INV_CLASS"
            transportSql.FillComboBox(strclass, ddlclassname, "inv_class_name", "inv_class_name")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub
    ''To Apply validation (Maximum Limit to Enter the character in the gridview cell according to the class name) whatever defined in Inventory setting screen
    Private Sub funvalidationgridcell()
        Try
            Dim strlength As String = "select inv_class_length from tspl_inv_class where inv_class_name = '" + ddlclassname.Text.ToString().Trim() + "'"
            Dim length As Integer = clsDBFuncationality.getSingleValue(strlength)
           


            'dr = connectSql.RunSqlReturnDR(strlength)
            'Dim length As Integer
            'While dr.Read()
            '    length = dr(0)
            'End While
            Dim dt As GridViewTextBoxColumn = TryCast(dgvclassgroup.Columns(0), GridViewColumn)
            dt.MaxLength = length
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
        dr.Close()
    End Sub
    ''Class Index Changed
    Private Sub ddlclassname_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlclassname.SelectedIndexChanged
        funvalidationgridcell()
        funfill()
        Dim str As String = "select inv_class_code, inv_class_desc from TSPL_INV_CLASS_DETAILS where inv_class_name = '" + ddlclassname.Text.ToString().Trim() + "'"

        Dim str12 As String = clsDBFuncationality.getSingleValue(str)
        
        If str12 <> "" Then
            btnsave.Text = "Update"
        End If
        Dim strpack As String = connectSql.RunScalar("select Inv_Class_Name  from TSPL_INV_CLASS  where Class_Type = 'Pack Type'")
        If ddlclassname.Text = strpack Then
            dgvclassgroup.Columns(2).IsVisible = True
            dgvclassgroup.Columns(3).IsVisible = True
        Else
            dgvclassgroup.Columns(2).IsVisible = False
            dgvclassgroup.Columns(3).IsVisible = False

        End If
    End Sub
    '' To Call the insert function
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        If ddlclassname.Text = "Select" Or ddlclassname.Text = "select" Then
            common.clsCommon.MyMessageBoxShow("Please select the Class")
            ddlclassname.Focus()
        Else
            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.itemGroups, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            If btnsave.Text = "&Save" Then
                funinsert()
                funfill()
            Else : btnsave.Text = "&Update"
                funupdate()
                funfill()
            End If
        End If
    End Sub
    '' To Fill the value the value according to the class name 
    Private Sub funfill()
        Dim strpack As String = connectSql.RunScalar("select Inv_Class_Name  from TSPL_INV_CLASS  where Class_Type = 'Pack Type'")
        If ddlclassname.Text = strpack Then
            Try
                dgvclassgroup.DataSource = Nothing
                dgvclassgroup.Rows.Clear()
                Dim strparent As String
                Dim strchild As String
                Dim str As String = "select inv_class_code, inv_class_desc, parent, child from TSPL_INV_CLASS_DETAILS where inv_class_name = '" + ddlclassname.Text.ToString().Trim() + "'"
                Dim ds As DataSet = connectSql.RunSQLReturnDS(str)
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim row As DataRow = ds.Tables(0).Rows(0)
                    For Each grow As Object In row.Table().Rows
                        Dim i As GridViewRowInfo = dgvclassgroup.Rows.AddNew()
                        i.Cells(0).Value = grow(0).ToString()
                        i.Cells(1).Value = grow(1).ToString()
                        strparent = grow(2).ToString().Trim()
                        If strparent = "Y" Then
                            i.Cells(2).Value = True
                        Else
                            i.Cells(2).Value = False

                        End If
                        strchild = grow(3).ToString().Trim()
                        If strchild = "Y" Then
                            i.Cells(3).Value = True
                        Else
                            i.Cells(3).Value = False

                        End If
                    Next
                End If
            Catch ex As Exception
                common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
            End Try
        Else
            dgvclassgroup.AutoGenerateColumns = False
            Try
                Dim str As String = "select inv_class_code, inv_class_desc from TSPL_INV_CLASS_DETAILS where inv_class_name = '" + ddlclassname.Text.ToString().Trim() + "'"
                transportSql.FillGridView(str, dgvclassgroup)
                dgvclassgroup.Columns(0).FieldName = "inv_class_code"
                dgvclassgroup.Columns(1).FieldName = "inv_class_desc"
            Catch ex As Exception
                common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
            End Try
        End If

        ' btnsave.Text = "Update"

    End Sub
    ''To Reset the all the field
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        funreset()
    End Sub
    ''To Export the data into excel
    Private Sub mnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnexport.Click
        Dim str As String = "select Inv_Class_Name as [Invoice Class Name], Inv_Class_Code as [Invoice Class Code], Inv_Class_Desc as [Description], Created_By as [Created By], Create_Date   as [Created Date], Modify_By as [Modify By], Modify_Date as [Modify Date],Comp_Code as [Company Code] from  TSPL_INV_CLASS_DETAILS"
        transportSql.ExporttoExcel(str, Me)

    End Sub
    ''To  Close the Form
    Private Sub mnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnclose.Click, btnclose.Click
        Me.Close()
    End Sub
    ''To Import the data from excel
    Private Sub mnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Invoice Class Name", "Invoice Class Code", "Description") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()

                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strClassName As String = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim strClassCode As String = clsCommon.myCstr(grow.Cells(1).Value)
                    Dim strDescription1 As String = clsCommon.myCstr(grow.Cells(2).Value)
                    Dim strDescription As String = strDescription1.Replace("'", "''")
                    If String.IsNullOrEmpty(strClassName) And clsCommon.myLen(strClassName) > 50 Then
                        Throw New Exception("Invoice Class Name has some error")

                    End If
                    If String.IsNullOrEmpty(strClassCode) And clsCommon.myLen(strClassCode) > 10 Then
                        Throw New Exception("Invoice Class code has some error")

                    End If
                    If clsCommon.myLen(strDescription) > 50 Then
                        Throw New Exception("Description has some problem")

                    End If
                    Dim sql1 As String = "select count(*) from TSPL_INV_CLASS_DETAILS where Inv_Class_Name='" + strClassName + "' and inv_class_code = '" + strClassCode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        Dim Sql As String = "insert into TSPL_INV_CLASS_DETAILS(Inv_Class_Name,Inv_Class_Code,Inv_Class_Desc,Created_By,Create_Date,Modify_By,Modify_Date,Comp_Code) values ('" + strClassName + "', '" + strClassCode + "', '" + strDescription + "','" + userCode + "', '" + connectSql.serverDate(trans) + "', '" + userCode + "', '" + connectSql.serverDate(trans) + "', '" + companyCode + "' )"
                        connectSql.RunSqlTransaction(trans, Sql)
                    Else
                        Dim sql As String = "update TSPL_INV_CLASS_DETAILS set inv_class_desc = '" + strDescription + "' where inv_class_name = '" + strClassName + "'and  inv_class_code = '" + strClassCode + "' "
                        connectSql.RunSqlTransaction(trans, sql)
                    End If
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
    ''Datagridview cell validating to check item length
    Private Sub dgvclassgroup_CellValidating(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellValidatingEventArgs) Handles dgvclassgroup.CellValidating
        If e.ColumnIndex = 0 Then
            Dim strlength As String = "select inv_class_length from tspl_inv_class where inv_class_name = '" + ddlclassname.Text.ToString().Trim() + "'"
            Dim length As Integer = clsDBFuncationality.getSingleValue(strlength)
          

            'dr = connectSql.RunSqlReturnDR(strlength)
            'Dim length As Integer
            'While dr.Read()
            '    length = dr(0)
            'End While
            If Not String.IsNullOrEmpty(e.Value) Then
                Dim str As String = e.Value.ToString()
                If str.Length <> length Then
                    common.clsCommon.MyMessageBoxShow("Item Code should be " + Convert.ToString(length) + "")
                    e.Cancel = True
                End If
            End If
        End If

    End Sub
    ''To Authorised the user 
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "ITEM-GROUP"
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
    '            'rdbtnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            'rdbtndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    Private Sub rdgrpbxitemgroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdgrpbxitemgroup.Click

    End Sub
End Class
