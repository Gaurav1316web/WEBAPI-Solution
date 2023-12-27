'' Changes by Parteek for Screen Level Rights Ticket No : TEC/16/03/18-000101 on 01/05/2018
Imports common

Public Class frmModule
    Const IsAvailable As String = "IsAvailable"
    Const ModuleName As String = "ModuleName"
    Const ProgramCode As String = "ProgramCode"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim obj As New clsModuleScreenHead
    Private ObjList As New List(Of clsModuleScreenHead)
    Private isCellValueChangedOpen As Boolean = False



    Sub LoadGridColumns()
        gv.Columns.Clear()
        Dim IsAvailable As New GridViewCheckBoxColumn()
        Dim Module_Name As New GridViewTextBoxColumn()
        Dim Program_Code As New GridViewTextBoxColumn()




        IsAvailable.FormatString = ""
        IsAvailable.HeaderText = "IsAvailable"
        IsAvailable.Name = "IsAvailable"
        IsAvailable.Width = 100
        IsAvailable.ReadOnly = True
        IsAvailable.IsVisible = True
        gv.Columns.Add(IsAvailable)


        Module_Name.FormatString = ""
        Module_Name.HeaderText = "ScreenName"
        Module_Name.Name = "ScreenName"
        Module_Name.Width = 300
        Module_Name.IsVisible = True
        Module_Name.ReadOnly = True
        Module_Name.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.Columns.Add(Module_Name)

        Program_Code.FormatString = ""
        Program_Code.HeaderText = "ProgramCode"
        Program_Code.Name = "ProgramCode"
        Program_Code.Width = 200
        Program_Code.IsVisible = True
        Program_Code.ReadOnly = True
        Program_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.Columns.Add(Program_Code)

    End Sub

    Private Sub frmModuleScreen_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SavingData(False)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmModuleScreen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isNewEntry = False
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        ' LoadModuleType()
        LoadData()
        Me.CenterToParent()
        'Dim ds As New DataSet
        'ds.Tables.Add(dt)
        'gvLabelSetting.DataSource = ds.Tables(0)
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Try
            funReset()
        Catch ex As Exception

        End Try
    End Sub

    Sub funReset()
        isNewEntry = True
        btnsave.Text = "Save"
        btnsave.Enabled = True
    End Sub

    Sub LoadData()
        LoadGridColumns()
        Dim qry As String = ""

        'qry = "select Program_Code,Program_Name as Module  from  TSPL_PROGRAM_MASTER tpm Left join TSPL_Module_Permission tmp on " _
        '& " tmp.module_Name=tpm.program_Code where Type='M' and Program_Name<>'' and program_COde <>'Mutility' order by Program_Name"

        qry = "select Program_Code,Program_Name as Module  from  TSPL_PROGRAM_MASTER tpm  " _
    & "  where Type='M' and Program_Name<>'' and program_COde <>'Mutility' order by Program_Name"

        'Sanjay
        '  qry = "select distinct cast(isnull(tmpp.IsAvailable,0) as bit) as Selected,Program_Code,Program_Name as Module  from  TSPL_PROGRAM_MASTER tpm " & _
        '" Left join TSPL_Module_Permission tmpp on  tmpp.module_Name=tpm.program_Code " & _
        '" where Type='M' and Program_Name<>'' and program_COde <>'Mutility' order by Program_Name "
        'Sanjay

        'qry = "select * from (select cast( 0 as bit) as Selected,Program_Code,Program_Name,Parent_Code,SNo  from  TSPL_PROGRAM_MASTER tpm "
        'qry += " Left join TSPL_Module_Permission tmp on  tmp.module_Name=tpm.program_Code"
        'qry += "  where 2 = 2"
        'qry += " union "
        'qry += " select cast( 0 as bit) as Selected,Program_Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name,Parent_Code,SNo from TSPL_PROGRAM_MASTER "
        'qry += " where 2=2 and  Type In ('SM') and Program_Code in (select distinct Parent_Code from TSPL_PROGRAM_MASTER where 2=2  ))xx"
        'qry += " where 2 = 2 "
        'qry += " and Program_Name<>'' and program_COde <>'Mutility' "
        'qry += " order by SNo"

        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry)
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()

        gv.DataSource = dt
        'Dim arr As ArrayList = clsModuleScreenHead.GetData()
        SetGridFormationOFgv()
        gv.BestFitColumns()
        '  gv.DataSource = arr


    End Sub
    Sub SetGridFormationOFgv()
        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("Program_Code").IsVisible = True
        gv.Columns("Program_Code").Width = 200
        gv.Columns("Program_Code").HeaderText = "Module Code"

        gv.Columns("Module").IsVisible = True
        gv.Columns("Module").Width = 200
        gv.Columns("Module").HeaderText = "Module Name"

    End Sub
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub

    Public Function Save() As Boolean
        'Try
        '    Dim obj As clsModuleScreenHead = Nothing
        '    ObjList = New List(Of clsModuleScreenHead)
        '    For Each str As String In gvModule.CheckedValue
        '        obj = New clsModuleScreenHead()
        '        obj.IsAvailable = True
        '        obj.Module_Name = str
        '        ObjList.Add(obj)
        '    Next
        '    If Not IsNothing(obj) Then
        '        If (obj.SaveData(ObjList)) Then
        '            Me.Close()
        '            Return True
        '        End If
        '    End If
        '    Return False
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
        Return True
    End Function

    'Function AllowToSave() As Boolean
    'If btnsave.Text = "Update" Then
    '    Dim QryStr As String = "select DOCUMENT_ID from TSPL_CLIENT_FORM_LABEL_SETTING where DOCUMENT_ID = '" + txtCode.Value + "' "
    '    Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
    '    If chkpost = "1" Then
    '        clsCommon.MyMessageBoxShow("Transection already posted")
    '        Return False
    '    End If
    'End If

    'If clsCommon.myLen(txtCode.Value) <= 0 Then
    '    myMessages.blankValue("Code")
    '    txtCode.Focus()
    '    Return False
    'End If

    'If clsCommon.myLen(cboModule.SelectedValue) <= 0 Then
    '    myMessages.blankValue("Module")
    '    cboModule.Focus()
    '    Return False
    'End If

    'If clsCommon.myLen(cboSubModule.SelectedValue) <= 0 Then
    '    myMessages.blankValue("Sub Module")
    '    cboModule.Focus()
    '    Return False
    'End If

    'If clsCommon.myLen(cboFormName.SelectedValue) <= 0 Then
    '    myMessages.blankValue("Form Name")
    '    cboModule.Focus()
    '    Return False
    'End If
    'If gvLabelSetting.CheckedValue.Count <= 0 Then
    '    Return False
    'Else
    '    Return True
    'End If

    'Dim ii As Int16 = 0
    'For Each grow As GridViewRowInfo In gvLabelSetting.CheckedValue
    '    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(ModuleName).Value)) > 0 And clsCommon.myCstr(grow.Cells(IsAvailable).Value) <> "False" Then
    '        ii += 1
    '        'If clsCommon.myCdbl(grow.Cells(IsAvailable).Value) = 0 Then

    '        '    Return False

    '        'End If
    '        ObjList.Add(obj)
    '    End If

    'Next
    'If ObjList Is Nothing Then
    '    Return False
    'End If
    'Return True
    'End Function

    Private Sub gvMonthlyAttendance_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)

    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        DeleteData()
    End Sub

    Sub DeleteData()
        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
        '    Exit Sub
        'End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            'Dim Reason As String = ""
            'If (myMessages.deleteConfirm()) Then
            '    Try
            '        Dim qry As String = "select * from TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME like '%" & formcode & "%'"
            '        dtLabelData = clsDBFuncationality.GetDataTable(qry)
            '        If AllowToSave() Then
            '            Dim obj As clsModuleScreenHead
            '            ObjList = New List(Of clsModuleScreenHead)

            '            For Each grow As GridViewRowInfo In gvLabelSetting.CheckedValue()
            '                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(Is_Reset).Value)) > 0 Then
            '                    obj = New clsModuleScreenHead()

            '                    obj.IsAvailable = clsCommon.myCstr(grow.Cells(IsAvailable).Value)
            '                    obj.LABEL_ID = clsCommon.myCstr(grow.Cells(ModuleName).Value)
            '                    obj.NEW_LABEL_NAME = clsCommon.myCstr(grow.Cells(colNew_Label_Name).Value)
            '                    If (obj.DeleteData(clsCommon.myCstr(obj.LABEL_ID), clsCommon.myCstr(obj.FORM_CODE))) Then
            '                        'Me.Close()
            '                        'Return True
            '                        'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            '                    End If
            '                End If
            '                'End If
            '            Next
            '        End If
            '        If Not IsNothing(obj) Then
            '            MessageBox.Show("Data Deleted Successfully..")
            '        End If
            '    Catch ex As Exception
            '        clsCommon.MyMessageBoxShow(ex.Message)
            '    End Try
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        PostData()
    End Sub

    Sub PostData()
        Try
            'If (myMessages.postConfirm()) Then
            '    SavingData(True)
            '    If (clsModuleScreenHead.PostData(txtCode.Value, True)) Then
            '        common.clsCommon.MyMessageBoxShow("Successfully Posted")
            '        LoadData(txtCode.Value, NavigatorType.Current)
            '    End If
            'End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If
        End If
    End Sub

    Private Sub gv_DoubleClick(sender As Object, e As EventArgs) Handles gv.DoubleClick
        Try
            If gv.Rows.Count > 0 Then
                Dim strDoc As String = Nothing
                Dim strCodeColumn As String = ""
                Dim strTrans As String = Nothing
                strDoc = gv.CurrentRow.Cells("Program_Code").Value
                Dim qry As String = "select program_code,program_name,parent_code from TSPL_PROGRAM_MASTER where Parent_Code='" & strDoc & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii <> 0 Then
                        strCodeColumn += "','"
                    End If
                    strCodeColumn += "" + clsCommon.myCstr(dt.Rows(ii)("program_code")).Trim() + ""
                Next
                Dim frmScreen As New frmScreen
                frmScreen.ModuleName = strCodeColumn
                frmScreen.Modulecode = strDoc
                frmScreen.ShowDialog()

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class