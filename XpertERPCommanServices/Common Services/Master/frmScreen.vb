'' Changes by Parteek for Screen Level Rights Ticket No : TEC/16/03/18-000101 on 01/05/2018
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class frmScreen
    Const IsAvailable As String = "IsAvailable"
    Const ScreenName As String = "ScreenName"
    Const ProgramCode As String = "ProgramCode"
    Public ModuleName As String = Nothing
    Public Modulecode As String = Nothing

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim obj As New clsModuleScreenHead
    Private ObjList As New List(Of clsModuleScreenHead)
    Private isCellValueChangedOpen As Boolean = False



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
    
        LoadData()
        Me.CenterToParent()
     
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
        Me.gvModule.CheckedValue.Clear()

    End Sub
    ' Tikcet No : TEC/25/07/19-000959 By Prabhakar
    Sub LoadData()
      
        Dim qry As String = ""


        qry = "select xx.Program_Code,xx.Program_Name,xx.Parent_Code as [Sub Module Code], TBL_SMODULE.Program_Name as [Sub Module Name] from (select Program_Code,Program_Name,Parent_Code,SNo  from  TSPL_PROGRAM_MASTER tpm "
        qry += " Left join TSPL_Module_Permission tmp on  tmp.module_Name=tpm.program_Code"
        qry += " where 2=2 and type in('M')"
        qry += " union select Program_Code,Program_Name,Parent_Code,SNo  from  TSPL_PROGRAM_MASTER tpm "
        qry += " Left join TSPL_Module_Permission tmp on  tmp.module_Name=tpm.program_Code"
        qry += "  where 2 = 2"
        qry += " union "
        qry += " select Program_Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name,Parent_Code,SNo from TSPL_PROGRAM_MASTER "
        qry += " where 2=2 and  Type In ('SM') and Program_Code in (select distinct Parent_Code from TSPL_PROGRAM_MASTER where 2=2  ))xx "
        qry += " left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('SM')) as TBL_SMODULE on TBL_SMODULE.Program_Code = XX.Parent_Code "
        qry += " where 2 = 2 "
        qry += "   and xx.Parent_Code in ('" & ModuleName & "') "
        qry += " and xx.Program_Name<>'' and xx.program_COde <>'Mutility' "
        qry += " order by xx.SNo"

        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry)

        gvModule.DataSource = dt
        gvModule.ValueMember = "Program_Code"
        gvModule.DisplayMember = "Program_Name"
        gvModule.CheckedAll()
        Dim arr As ArrayList = clsModuleScreenHead.GetDataForScreen(Modulecode, Nothing)
        gvModule.CheckedValue = arr
        'If (obj IsNot Nothing) Then
        '    funReset()
        '    isNewEntry = False
        '    btnsave.Text = "Update"

        '    btnsave.Enabled = True


        '    Dim ii As Int16 = 0
        '    LoadGridColumns()

        '    If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
        '        For Each obj As clsModuleScreenHead In obj.ObjList
        '            gvLabelSetting.CheckedValue.AddNew()

        '            gvLabelSetting.CheckedValue(gvLabelSetting.CheckedValue.Count - 1).Cells(IsAvailable).Value = IIf(obj.IsAvailable = 1, True, False)
        '            gvLabelSetting.CheckedValue(gvLabelSetting.CheckedValue.Count - 1).Cells(ModuleName).Value = obj.Module_Name
        '            gvLabelSetting.CheckedValue(gvLabelSetting.CheckedValue.Count - 1).Cells(ProgramCode).Value = obj.Program_code

        '        Next
        '    Else
        '        gvLabelSetting.CheckedValue.AddNew()
        '    End If
        'End If

    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub

    Public Function Save() As Boolean
        Try
            Dim obj As clsModuleScreenHead = Nothing
            ObjList = New List(Of clsModuleScreenHead)
            For Each str As String In gvModule.CheckedValue
                obj = New clsModuleScreenHead()
                obj.IsAvailable = True
                obj.Module_Name = Modulecode
                obj.Screen_Name = str
                Dim Parent_Code As String = clsDBFuncationality.getSingleValue("select Parent_Code from tspl_Program_Master where Program_code='" & str & "'")
                obj.Parent_Code = Parent_Code
                ObjList.Add(obj)
            Next
            If Not IsNothing(obj) Then
                If (obj.SaveDataForScreen(ObjList, Modulecode)) Then
                    Me.Close()
                    Return True
                End If
            Else
                Dim sQuery = "delete from TSPL_MODULE_Screen_PERMISSION where module_Name='" & Modulecode & "'"
                clsDBFuncationality.ExecuteNonQuery(sQuery)
                Me.Close()
                Return True
            End If
            'Return False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return False
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

   

End Class