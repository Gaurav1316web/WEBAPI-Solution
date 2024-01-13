Imports common
Imports System.Reflection
Imports System.Windows.Forms
Imports Telerik.WinControls.UI
Imports System.Drawing

Public Class frmClientFormLableDetails
    Const Current_Label_Name As String = "Current_Label_Name"
    Const Lable_Id As String = "Lable_Id"
    Const colFormName As String = "Form_Name"
    Const colNew_Label_Name As String = "New_Label_Name"
    Const colNew_Label_Font As String = "Label_Font"
    Const colNew_Label_Fore_Color As String = "Label_Fore_Color"
    Const colNew_Label_Back_Color As String = "Label_Back_Color"
    Const colNew_Label_Font_Size As String = "Label_Font_Size"
    Const Is_Reset As String = "Reset"


    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim obj As New clsClientFormLableDetails
    Private ObjList As New List(Of clsClientFormLableDetails)
    Private isCellValueChangedOpen As Boolean = False
    Public formnam As XpertERPEngine.FrmMainTranScreen
    Public formcode As String

    Shared dtLabelData As DataTable = Nothing
    Shared sQuery As String = String.Empty
    Shared is_Cancel_Allowed_Posted As String = String.Empty


    Sub LoadGridColumns()
        Try
            If gvLabelSetting.Rows.Count > 0 Then
                Exit Sub
            End If
            gvLabelSetting.Rows.Clear()
            gvLabelSetting.Columns.Clear()



            Dim ModuleCode As New GridViewTextBoxColumn()
            Dim SubModuleCode As New GridViewTextBoxColumn()
            Dim FormName As New GridViewTextBoxColumn()
            Dim Lable_Id As New GridViewTextBoxColumn()
            Dim IsReset As New GridViewCheckBoxColumn
            Dim Label_Font As GridViewComboBoxColumn = New GridViewComboBoxColumn()
            Dim Lable_Font_Size As New GridViewTextBoxColumn()
            Dim Label_Fore_Color As GridViewComboBoxColumn = New GridViewComboBoxColumn()
            Dim Label_back_Color As GridViewComboBoxColumn = New GridViewComboBoxColumn()
            'Dim fore_color As GridViewTextBoxColumn = New ColorDialog
            'Dim Back_color As ColorDialog = New ColorDialog

            IsReset.FormatString = ""
            IsReset.HeaderText = "Reset"
            IsReset.Name = "Reset"
            IsReset.Width = 60
            gvLabelSetting.Columns.Add(IsReset)

            FormName.FormatString = ""
            FormName.HeaderText = "Form_Name"
            FormName.Name = "Form_Name"
            FormName.Width = 0
            FormName.IsVisible = False
            'SubModuleCode.ReadOnly = True
            FormName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gvLabelSetting.Columns.Add(FormName)

            Lable_Id.FormatString = ""
            Lable_Id.HeaderText = "Lable_Id"
            Lable_Id.Name = "Lable_Id"
            Lable_Id.Width = 0
            Lable_Id.IsVisible = False
            'SubModuleCode.ReadOnly = True
            Lable_Id.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gvLabelSetting.Columns.Add(Lable_Id)

            ModuleCode.FormatString = ""
            ModuleCode.HeaderText = "Current Label Name"
            ModuleCode.Name = "Current_Label_Name"
            ModuleCode.Width = 300
            ModuleCode.ReadOnly = True
            ModuleCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gvLabelSetting.Columns.Add(ModuleCode)

            SubModuleCode.FormatString = ""
            SubModuleCode.HeaderText = "New Label Name"
            SubModuleCode.Name = "New_Label_Name"
            SubModuleCode.Width = 300
            'SubModuleCode.ReadOnly = True
            SubModuleCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gvLabelSetting.Columns.Add(SubModuleCode)

            Label_Font.FormatString = ""
            Label_Font.HeaderText = "Label Font"
            Label_Font.Name = "Label_Font"
            Label_Font.Width = 150
            Label_Font.ReadOnly = False
            Label_Font.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            Label_Font.DataSource = GetFont()
            Label_Font.ValueMember = "Code"
            Label_Font.DisplayMember = "Code"
            gvLabelSetting.MasterTemplate.Columns.Add(Label_Font)


            Lable_Font_Size.FormatString = ""
            Lable_Font_Size.HeaderText = "Label Font Size"
            Lable_Font_Size.Name = "Label_Font_Size"
            Lable_Font_Size.Width = 100
            'SubModuleCode.ReadOnly = True
            Lable_Font_Size.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gvLabelSetting.Columns.Add(Lable_Font_Size)

            Label_Fore_Color.FormatString = ""
            Label_Fore_Color.HeaderText = "Label Font Color"
            Label_Fore_Color.Name = "Label_Font_Color"
            Label_Fore_Color.Width = 150
            Label_Fore_Color.ReadOnly = False
            Label_Fore_Color.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            ' Label_Fore_Color.DataSource = fore_color
            'Label_Fore_Color.ValueMember = "Code"
            'Label_Fore_Color.DisplayMember = "Code"
            gvLabelSetting.MasterTemplate.Columns.Add(Label_Fore_Color)

            Label_back_Color.FormatString = ""
            Label_back_Color.HeaderText = "Label Back Color"
            Label_back_Color.Name = "Label_Back_Color"
            Label_back_Color.Width = 150
            Label_back_Color.ReadOnly = False
            Label_back_Color.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            ' Label_back_Color.DataSource = Back_color
            'Label_back_Color.ValueMember = "Code"
            'Label_back_Color.DisplayMember = "Code"
            gvLabelSetting.MasterTemplate.Columns.Add(Label_back_Color)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Function GetFont() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim objFontFamily As FontFamily
        Dim objFontCollection As System.Drawing.Text.FontCollection
        ' Dim tempFont As Font
        objFontCollection = New System.Drawing.Text.InstalledFontCollection()
        For Each objFontFamily In objFontCollection.Families
            If objFontFamily.IsStyleAvailable(FontStyle.Regular) Then
                dt.Rows.Add(objFontFamily.Name)
            End If
        Next
        Return dt
    End Function

    Private Function GetColor() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        ' Dim cc As ColorDialog
        'Dim ColorList As ArrayList = New ArrayList()
        'Dim colorType As type =Color.
        'Dim propInfoList() As PropertyInfo = colorType.GetProperties(BindingFlags.Static Or BindingFlags.DeclaredOnly Or BindingFlags.Public)

        'For Each c As Color In cc
        '    dt.Rows.Add(c.Name)
        'Next
        'For Each c As Color In System.Drawing.Text.FontCollection
        '    dt.Rows.Add(c.Name)
        'Next
        Return dt
    End Function

    Private Sub frmClientFormLableDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            SavingData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub
    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmClientFormLableDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadGridColumns()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        ' LoadModuleType()
        LoadLableData(formnam)

        'Dim ds As New DataSet
        'ds.Tables.Add(dt)
        'gvLabelSetting.DataSource = ds.Tables(0)
    End Sub

    Public Sub LoadModuleType()
        Try
            Dim ds As DataSet = New DataSet()
            Dim dt As DataTable = New DataTable
            Dim qry As String = "select Program_Code as code,Program_Name as name from TSPL_PROGRAM_MASTER where Type='M'"
            dt = clsDBFuncationality.GetDataTable(qry)
            ds.Tables.Add(dt)
            ds.Tables(0).TableName = "Module"

            qry = "select Program_Code as code,Program_Name as name,parent_code from TSPL_PROGRAM_MASTER where Type='SM'"
            dt = clsDBFuncationality.GetDataTable(qry)
            ds.Tables.Add(dt)
            ds.Tables(1).TableName = "SubModule"

            qry = "select Program_Code as code,Program_Name,parent_code as name from TSPL_PROGRAM_MASTER where Type='M'"
            dt = clsDBFuncationality.GetDataTable(qry)
            ds.Tables.Add(dt)
            ds.Tables(2).TableName = "FormName"


            ds.Relations.Add("modulerelation", ds.Tables(0).Columns("code"), ds.Tables(1).Columns("parent_code"))
            ds.Relations.Add("formrelation", ds.Tables(1).Columns("code"), ds.Tables(1).Columns("parent_code"))

            'cboModule.DataSource = ds.Tables(0)
            'cboModule.DisplayMember = "name"
            'cboModule.ValueMember = "code"

            'cboModule.DataSource = ds.Tables(1)
            'cboModule.DisplayMember = "name"
            'cboModule.ValueMember = "code"

            'cboModule.DataSource = ds.Tables(2)
            'cboModule.DisplayMember = "name"
            'cboModule.ValueMember = "code"
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

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
        'txtCode.MyReadOnly = False
        'txtCode.Value = Nothing
        'txtCode.Focus()
        ''txtAdjustBy.Value = Nothing
        'cboModule.SelectedValue = Nothing
        'txtDescription.Text = ""
        btnsave.Text = "Save"
        '"SCRAP-SALE"1.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btndelete.Enabled = True
        Me.gvLabelSetting.Rows.Clear()
        Me.gvLabelSetting.Rows.AddNew()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType)
        Try
            ' LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        ' btnsave.Enabled = True
        'btndelete.Enabled = True
        obj = clsClientFormLableDetails.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.REIMBURSEMENT_CODE) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            If obj.POSTED Then
                btnsave.Enabled = False
                btndelete.Enabled = False
                '         "SCRAP-SALE"1.Status = ERPTransactionStatus.Approved
            Else
                btnsave.Enabled = True
                btndelete.Enabled = True
                '        "SCRAP-SALE"1.Status = ERPTransactionStatus.Pending
            End If

            Dim ii As Int16 = 0
            LoadGridColumns()
            'txtCode.Value = obj.REIMBURSEMENT_CODE
            'cboModule.SelectedValue = obj.PAY_PERIOD_CODE
            ''txtAdjustBy.Value = obj.ADJUSTMENT_BY_Code
            ''lblAdjustmentByName.Text = obj.ADJUSTMENT_BY_Name
            'txtDescription.Text = obj.REIMBURSEMENT_REMARK
            'txtCode.MyReadOnly = True

            If (clsClientFormLableDetails.ObjList IsNot Nothing AndAlso clsClientFormLableDetails.ObjList.Count > 0) Then
                For Each obj As clsClientFormLableDetails In clsClientFormLableDetails.ObjList
                    gvLabelSetting.Rows.AddNew()

                    gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells(Current_Label_Name).Value = obj.CURRENT_LABEL_NAME
                    gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells(Lable_Id).Value = obj.LABEL_ID
                    gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells(colFormName).Value = obj.FORM_CODE
                    gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells(colNew_Label_Name).Value = obj.NEW_LABEL_NAME
                    gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells(colNew_Label_Font).Value = obj.NEW_LABEL_Font
                    gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells(colNew_Label_Font_Size).Value = obj.NEW_LABEL_Font_Size
                    gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells(colNew_Label_Fore_Color).Value = obj.NEW_LABEL_Fore_Color
                    gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells(colNew_Label_Back_Color).Value = obj.NEW_LABEL_Back_Color
                    'gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells(colNew_Label_Fore_Color).Value = obj.NEW_LABEL_Fore_Color
                    ' gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells(colNew_Label_Back_Color).Value = obj.NEW_LABEL_Back_Color
                    'gvAdjustmentVoucher.Rows(gvAdjustmentVoucher.Rows.Count - 1).Cells(coladjustMinus).Value = obj.AdjustMinus

                Next
            Else
                gvLabelSetting.Rows.AddNew()
            End If
        End If

    End Sub


    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub

    Public Function Save() As Boolean
        Try
            Dim qry As String = "select * from TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME like '%" & formcode & "%'"
            dtLabelData = clsDBFuncationality.GetDataTable(qry)
            If AllowToSave() Then
                Dim obj As clsClientFormLableDetails = Nothing
                ObjList = New List(Of clsClientFormLableDetails)

                For Each grow As GridViewRowInfo In gvLabelSetting.Rows
                    If clsCommon.myCstr(grow.Cells(colNew_Label_Name).Value) <> clsCommon.myCstr(grow.Cells(Current_Label_Name).Value) And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colNew_Label_Name).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(Current_Label_Name).Value)) > 0 Then
                        obj = New clsClientFormLableDetails()

                        obj.FORM_CODE = clsCommon.myCstr(grow.Cells("FORM_NAME").Value)
                        obj.CURRENT_LABEL_NAME = clsCommon.myCstr(grow.Cells(Current_Label_Name).Value)
                        obj.LABEL_ID = clsCommon.myCstr(grow.Cells(Lable_Id).Value)
                        obj.NEW_LABEL_NAME = clsCommon.myCstr(grow.Cells(colNew_Label_Name).Value)
                        obj.NEW_LABEL_Font = clsCommon.myCstr(grow.Cells(colNew_Label_Font).Value)
                        obj.NEW_LABEL_Font_Size = clsCommon.myCstr(grow.Cells(colNew_Label_Font_Size).Value)
                        'obj.NEW_LABEL_Fore_Color = clsCommon.myCstr(grow.Cells(colNew_Label_Fore_Color).Value)
                        'obj.NEW_LABEL_Back_Color = clsCommon.myCstr(grow.Cells(colNew_Label_Back_Color).Value)
                        'obj.NEW_LABEL_Fore_Color = clsCommon.myCstr(grow.Cells(colNew_Label_Fore_Color).Value)
                        ' obj.NEW_LABEL_Back_Color = clsCommon.myCstr(grow.Cells(colNew_Label_Back_Color).Value)
                        ObjList.Add(obj)
                    End If
                Next
                If Not IsNothing(obj) Then
                    obj.DT = New DataTable
                    obj.DT = dtLabelData
                    If (obj.SaveData(obj, ObjList, isNewEntry, "")) Then
                        Me.Close()
                        Return True
                        'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Function AllowToSave() As Boolean
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

        Dim ii As Int16 = 0
        For Each grow As GridViewRowInfo In gvLabelSetting.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colNew_Label_Name).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(Current_Label_Name).Value)) > 0 Then
                ii += 1
                'If clsCommon.myCdbl(grow.Cells(Current_Label_Name).Value) = 0 Then

                '    Return False

                'End If
                ObjList.Add(obj)
            End If

        Next
        If ObjList Is Nothing Then
            Return False
        End If
        Return True
    End Function

    Private Sub cboModule__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
        Dim qry As String = "select Program_code,Program_Name from TSPL_PROGRAM_MASTER where Type='M'"
        'Dim qry As String = "select PAY_PERIOD_CODE as Code , PAY_PERIOD_NAME as Name, DATE_FROM as 'From Date', DATE_TO AS 'To Date', DESCRIPTION as Description  from TSPL_PAYPERIOD_MASTER"
        findPayperiod.Value = clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "Code", "POSTED=1 and FREEZED=0", findPayperiod.Value, "PAY_PERIOD_CODE", isButtonClicked)
        If clsCommon.myLen(findPayperiod.Value) > 0 Then
            Dim clspp As clsPayPeriodMaster
            clspp = clsPayPeriodMaster.GetData(findPayperiod.Value, NavigatorType.Current)
            lblPayPeriodName.Text = clspp.Name

        Else
            lblPayPeriodName.Text = ""

        End If

    End Sub

    Private Sub gvLabelSetting_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvLabelSetting.CellDoubleClick
        If e.Column Is gvLabelSetting.Columns(colNew_Label_Fore_Color) Then
            Dim cc As New RadColorDialogForm
            cc.ShowDialog()
            gvLabelSetting.CurrentRow.Cells(colNew_Label_Fore_Color).Value = cc.ForeColor
        End If
        If e.Column Is gvLabelSetting.Columns(colNew_Label_Fore_Color) Then
            Dim cc As New RadColorDialogForm
            cc.ShowDialog()
            gvLabelSetting.CurrentRow.Cells(colNew_Label_Fore_Color).Value = cc.BackColor
        End If
    End Sub

    Private Sub gvMonthlyAttendance_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvLabelSetting.CellEndEdit

    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
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
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                Try
                    Dim qry As String = "select * from TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME like '%" & formcode & "%'"
                    dtLabelData = clsDBFuncationality.GetDataTable(qry)
                    If AllowToSave() Then
                        Dim obj As clsClientFormLableDetails
                        ObjList = New List(Of clsClientFormLableDetails)

                        For Each grow As GridViewRowInfo In gvLabelSetting.Rows()
                            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(Is_Reset).Value)) > 0 Then
                                obj = New clsClientFormLableDetails()

                                obj.FORM_CODE = clsCommon.myCstr(grow.Cells("FORM_NAME").Value)
                                obj.CURRENT_LABEL_NAME = clsCommon.myCstr(grow.Cells(Current_Label_Name).Value)
                                obj.LABEL_ID = clsCommon.myCstr(grow.Cells(Lable_Id).Value)
                                obj.NEW_LABEL_NAME = clsCommon.myCstr(grow.Cells(colNew_Label_Name).Value)
                                obj.NEW_LABEL_Font = clsCommon.myCstr(grow.Cells(colNew_Label_Font).Value)
                                obj.NEW_LABEL_Font_Size = clsCommon.myCstr(grow.Cells(colNew_Label_Font_Size).Value)
                                obj.NEW_LABEL_Fore_Color = clsCommon.myCstr(grow.Cells(colNew_Label_Fore_Color).Value)
                                obj.NEW_LABEL_Back_Color = clsCommon.myCstr(grow.Cells(colNew_Label_Back_Color).Value)
                                'obj.NEW_LABEL_Fore_Color = clsCommon.myCstr(grow.Cells(colNew_Label_Fore_Color).Value)
                                ' obj.NEW_LABEL_Back_Color = clsCommon.myCstr(grow.Cells(colNew_Label_Back_Color).Value)
                                If (clsClientFormLableDetails.DeleteData(clsCommon.myCstr(obj.LABEL_ID), clsCommon.myCstr(obj.FORM_CODE))) Then
                                    'Me.Close()
                                    'Return True
                                    'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                                End If
                            End If
                            'End If
                        Next
                    End If
                    If Not IsNothing(obj) Then
                        MessageBox.Show(Me, "Data Deleted Successfully..", Me.Text)
                    End If
                Catch ex As Exception
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    'Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
    '    'Dim obj As New clsCancelLog
    '    'obj.Program_Code = Form_ID
    '    'obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
    '    'obj.REASON = Reason
    '    'obj.ACTIVITY_TYPE = Activity_Type
    '    'Return clsCancelLog.SaveData(obj, True, trans)
    'End Function

    Private Sub txtModuleCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)

    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        PostData()
    End Sub

    Sub PostData()
        Try
            'If (myMessages.postConfirm()) Then
            '    SavingData(True)
            '    If (clsClientFormLableDetails.PostData(txtCode.Value, True)) Then
            '        common.clsCommon.MyMessageBoxShow("Successfully Posted")
            '        LoadData(txtCode.Value, NavigatorType.Current)
            '    End If
            'End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub LoadLableData(ByRef formname As XpertERPEngine.FrmMainTranScreen, Optional ByVal contrl As Control = Nothing)
        LoadGridColumns()


        If IsNothing(contrl) Then
            For Each ctrl As Control In formname.Controls
                If ctrl.HasChildren = True Then
                    LoadLableData(Me, ctrl)
                End If
                If TypeOf (ctrl) Is common.Controls.MyLabel Or TypeOf (ctrl) Is Label Or TypeOf (ctrl) Is RadLabel Then
                    Try
                        If ctrl.Text.Length > 2 And Not ctrl.Text.Contains("0") Then
                            gvLabelSetting.Rows.AddNew()
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Lable_Id").Value = ctrl.Name.ToString
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Current_Label_Name").Value = ctrl.Text
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("New_Label_Name").Value = ctrl.Text '""
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Form_Name").Value = formcode
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font").Value = ctrl.Font.FontFamily.Name
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font_Size").Value = ctrl.Font.Size
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font_Color").Value = ctrl.ForeColor
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Back_Color").Value = ctrl.BackColor
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                ElseIf TypeOf (ctrl) Is RadGridView Or TypeOf (ctrl) Is DataGridView Then
                    Try
                        Dim cttrl = CType(ctrl, RadGridView)
                        If cttrl.Columns.Count > 0 Then
                            For Each col As GridViewColumn In cttrl.Columns
                                gvLabelSetting.Rows.AddNew()
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Lable_Id").Value = col.Name.ToString
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Current_Label_Name").Value = col.HeaderText '& "(" & cttrl.text & ")"
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("New_Label_Name").Value = col.HeaderText '""
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Form_Name").Value = formcode
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font").Value = ctrl.Font.FontFamily.Name
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font_Size").Value = ctrl.Font.Size
                            Next
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                ElseIf TypeOf (ctrl) Is DataGridView Then
                    Try
                        Dim cttrl = CType(ctrl, DataGridView)
                        If cttrl.Columns.Count > 0 Then
                            For Each col As GridViewColumn In cttrl.Columns
                                gvLabelSetting.Rows.AddNew()
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Lable_Id").Value = col.Name.ToString
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Current_Label_Name").Value = col.HeaderText '& "(" & cttrl.text & ")"
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("New_Label_Name").Value = col.HeaderText '""
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Form_Name").Value = formcode
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font").Value = ctrl.Font.FontFamily.Name
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font_Size").Value = ctrl.Font.Size
                            Next
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                ElseIf TypeOf (ctrl) Is common.Controls.MyRadioButton Or TypeOf (ctrl) Is RadioButton Or TypeOf (ctrl) Is RadRadioButton Then
                    Try
                        If ctrl.Text.Length > 2 And Not ctrl.Text.Contains("0") Then
                            gvLabelSetting.Rows.AddNew()
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Lable_Id").Value = ctrl.Name.ToString
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Current_Label_Name").Value = ctrl.Text
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("New_Label_Name").Value = ctrl.Text '""
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Form_Name").Value = formcode
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font").Value = ctrl.Font.FontFamily.Name
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font_Size").Value = ctrl.Font.Size
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font_Color").Value = ctrl.ForeColor
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Back_Color").Value = ctrl.BackColor
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                    '=====================================================================================================
                ElseIf TypeOf (ctrl) Is common.Controls.MyCheckBox Or TypeOf (ctrl) Is CheckBox Or TypeOf (ctrl) Is RadCheckBox Then
                    Try
                        If ctrl.Text.Length > 2 And Not ctrl.Text.Contains("0") Then
                            gvLabelSetting.Rows.AddNew()
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Lable_Id").Value = ctrl.Name.ToString
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Current_Label_Name").Value = ctrl.Text
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("New_Label_Name").Value = ctrl.Text '""
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Form_Name").Value = formcode
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font").Value = ctrl.Font.FontFamily.Name
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font_Size").Value = ctrl.Font.Size
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font_Color").Value = ctrl.ForeColor
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Back_Color").Value = ctrl.BackColor
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                    '=====================================================================================================
                End If
            Next
        Else
            For Each ctrl As Control In contrl.Controls
                If ctrl.HasChildren = True Then
                    LoadLableData(Me, ctrl)
                End If
                If TypeOf (ctrl) Is common.Controls.MyLabel Or TypeOf (ctrl) Is Label Or TypeOf (ctrl) Is RadLabel Then
                    Try
                        If ctrl.Text.Length > 2 And Not ctrl.Text.Contains("0") Then
                            gvLabelSetting.Rows.AddNew()
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Lable_Id").Value = ctrl.Name.ToString
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Current_Label_Name").Value = ctrl.Text
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("New_Label_Name").Value = ctrl.Text '""
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Form_Name").Value = formcode
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font").Value = ctrl.Font.FontFamily.Name
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font_Size").Value = ctrl.Font.Size
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font_Color").Value = ctrl.ForeColor
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Back_Color").Value = ctrl.BackColor
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                ElseIf TypeOf (ctrl) Is RadGridView Then 'Or TypeOf (ctrl) Is DataGridView Then
                    Try
                        Dim cttrl = CType(ctrl, RadGridView)
                        If cttrl.Columns.Count > 0 Then
                            For Each col As GridViewColumn In cttrl.Columns
                                gvLabelSetting.Rows.AddNew()
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Lable_Id").Value = col.Name.ToString
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Current_Label_Name").Value = col.HeaderText '& "(" & cttrl.text & ")"
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("New_Label_Name").Value = col.HeaderText '""
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Form_Name").Value = formcode
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font").Value = ctrl.Font.FontFamily.Name
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font_Size").Value = ctrl.Font.Size
                            Next
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                ElseIf TypeOf (ctrl) Is DataGridView Then
                    Try
                        Dim cttrl = CType(ctrl, DataGridView)
                        If cttrl.Columns.Count > 0 Then
                            For Each col As GridViewColumn In cttrl.Columns
                                gvLabelSetting.Rows.AddNew()
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Lable_Id").Value = col.Name.ToString
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Current_Label_Name").Value = col.HeaderText '& "(" & cttrl.text & ")"
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("New_Label_Name").Value = col.HeaderText ' ""
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Form_Name").Value = formcode
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font").Value = ctrl.Font.FontFamily.Name
                                gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font_Size").Value = ctrl.Font.Size
                            Next
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                ElseIf TypeOf (ctrl) Is common.Controls.MyRadioButton Or TypeOf (ctrl) Is RadioButton Or TypeOf (ctrl) Is RadRadioButton Then
                    Try
                        If ctrl.Text.Length > 2 And Not ctrl.Text.Contains("0") Then
                            gvLabelSetting.Rows.AddNew()
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Lable_Id").Value = ctrl.Name.ToString
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Current_Label_Name").Value = ctrl.Text
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("New_Label_Name").Value = ctrl.Text ' ""
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Form_Name").Value = formcode
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font").Value = ctrl.Font.FontFamily.Name
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font_Size").Value = ctrl.Font.Size
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font_Color").Value = ctrl.ForeColor
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Back_Color").Value = ctrl.BackColor
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                    '===============================================================
                ElseIf TypeOf (ctrl) Is common.Controls.MyCheckBox Or TypeOf (ctrl) Is CheckBox Or TypeOf (ctrl) Is RadCheckBox Then
                    Try
                        If ctrl.Text.Length > 2 And Not ctrl.Text.Contains("0") Then
                            gvLabelSetting.Rows.AddNew()
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Lable_Id").Value = ctrl.Name.ToString
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Current_Label_Name").Value = ctrl.Text
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("New_Label_Name").Value = ctrl.Text ' ""
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Form_Name").Value = formcode
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font").Value = ctrl.Font.FontFamily.Name
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font_Size").Value = ctrl.Font.Size
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Font_Color").Value = ctrl.ForeColor
                            gvLabelSetting.Rows(gvLabelSetting.Rows.Count - 1).Cells("Label_Back_Color").Value = ctrl.BackColor
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try

                    '=============================================================

                End If
               
            Next
        End If
        gvLabelSetting.EnableFiltering = True
        'gvLabelSetting.DataSource = dt
    End Sub

    Public Sub LoadLableChanged(ByRef formname As XpertERPEngine.FrmMainTranScreen, Optional ByVal contrl As Control = Nothing, Optional ByVal loaddt As Boolean = False)
        Try
            If loaddt = True Then
                Dim qry As String = "select * from TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME like '%" & formcode & "%'"
                dtLabelData = clsDBFuncationality.GetDataTable(qry)
                If dtLabelData.Rows.Count <= 0 Then
                    Exit Sub
                End If
            End If

            If IsNothing(contrl) Then
                For Each ctrl As Control In formname.Controls
                    If ctrl.HasChildren = True Then
                        LoadLableChanged(Me, ctrl)
                    End If
                    If TypeOf (ctrl) Is common.Controls.MyLabel Or TypeOf (ctrl) Is Label Then
                        Try
                            If dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'").Length > 0 Then
                                ctrl.Text = dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("New_Label_Name")
                                ' ctrl.ForeColor = dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font_Color")
                                ' ctrl.BackColor = dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Back_Color")
                                If clsCommon.myCstr(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font").ToString) <> "" Then
                                    ctrl.Font = New System.Drawing.Font(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font").ToString, Convert.ToSingle(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font_Size")), FontStyle.Regular)
                                End If
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.ToString)
                        End Try
                    ElseIf TypeOf (ctrl) Is RadGridView Or TypeOf (ctrl) Is DataGridView Then
                        Try
                            Dim cttrl = CType(ctrl, RadGridView)
                            If cttrl.Columns.Count > 0 Then
                                For Each col As GridViewColumn In cttrl.Columns
                                    If dtLabelData.Select("LABEL_ID='" & col.Name.ToString & "'").Length > 0 Then
                                        col.HeaderText = dtLabelData.Select("LABEL_ID='" & col.Name.ToString & "'")(0)("New_Label_Name")
                                        If clsCommon.myCstr(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font").ToString) <> "" Then
                                            ' cttrl.columns(col).font = New System.Drawing.Font(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font").ToString, Convert.ToSingle(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font_Size")), FontStyle.Regular)
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.ToString)
                        End Try
                    ElseIf TypeOf (ctrl) Is common.Controls.MyRadioButton Or TypeOf (ctrl) Is RadioButton Or TypeOf (ctrl) Is RadRadioButton Then
                        Try
                            If dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'").Length > 0 Then
                                ctrl.Text = dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("New_Label_Name")
                                ' ctrl.ForeColor = dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font_Color")
                                ' ctrl.BackColor = dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Back_Color")

                                If clsCommon.myCstr(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font").ToString) <> "" Then
                                    ctrl.Font = New System.Drawing.Font(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font").ToString, Convert.ToSingle(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font_Size")), FontStyle.Regular)
                                End If
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.ToString)
                        End Try
                        '==============================================================================================
                    ElseIf TypeOf (ctrl) Is common.Controls.MyCheckBox Or TypeOf (ctrl) Is CheckBox Or TypeOf (ctrl) Is RadCheckBox Then
                        Try
                            If dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'").Length > 0 Then
                                ctrl.Text = dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("New_Label_Name")
                                ' ctrl.ForeColor = dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font_Color")
                                ' ctrl.BackColor = dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Back_Color")

                                If clsCommon.myCstr(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font").ToString) <> "" Then
                                    ctrl.Font = New System.Drawing.Font(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font").ToString, Convert.ToSingle(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font_Size")), FontStyle.Regular)
                                End If
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.ToString)
                        End Try
                        '==============================================================================================
                    End If
                Next
            Else
                For Each ctrl As Control In contrl.Controls
                    If ctrl.HasChildren = True Then
                        LoadLableChanged(Me, ctrl)
                    End If
                    If TypeOf (ctrl) Is common.Controls.MyLabel Or TypeOf (ctrl) Is Label Then
                        Try
                            If dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'").Length > 0 Then
                                ctrl.Text = dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("New_Label_Name")
                                ' ctrl.ForeColor = dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font_Color")
                                ' ctrl.BackColor = dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Back_Color")

                                If clsCommon.myCstr(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font").ToString) <> "" Then
                                    ctrl.Font = New System.Drawing.Font(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font").ToString, Convert.ToSingle(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font_Size")), FontStyle.Regular)
                                End If
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.ToString)
                        End Try
                    ElseIf TypeOf (ctrl) Is RadGridView Or TypeOf (ctrl) Is DataGridView Then
                        Try
                            Dim cttrl = CType(ctrl, RadGridView)
                            If cttrl.Columns.Count > 0 Then
                                For Each col As GridViewColumn In cttrl.Columns
                                    If dtLabelData.Select("LABEL_ID='" & col.Name.ToString & "'").Length > 0 Then
                                        col.HeaderText = dtLabelData.Select("LABEL_ID='" & col.Name.ToString & "'")(0)("New_Label_Name")
                                        If clsCommon.myCstr(dtLabelData.Select("LABEL_ID='" & col.Name.ToString & "'")(0)("Label_Font").ToString) <> "" Then
                                            ' cttrl.columns(col.Name).font = New System.Drawing.Font(dtLabelData.Select("LABEL_ID='" & col.Name.ToString & "'")(0)("Label_Font").ToString, Convert.ToSingle(dtLabelData.Select("LABEL_ID='" & col.Name.ToString & "'")(0)("Label_Font_Size")), FontStyle.Regular)
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.ToString)
                        End Try
                    ElseIf TypeOf (ctrl) Is common.Controls.MyRadioButton Or TypeOf (ctrl) Is RadioButton Or TypeOf (ctrl) Is RadRadioButton Then
                        Try
                            If dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'").Length > 0 Then
                                ctrl.Text = dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("New_Label_Name")
                                '  ctrl.ForeColor = dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font_Color")
                                ' ctrl.BackColor = dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Back_Color")

                                If clsCommon.myCstr(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font").ToString) <> "" Then
                                    ctrl.Font = New System.Drawing.Font(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font").ToString, Convert.ToSingle(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font_Size")), FontStyle.Regular)
                                End If
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.ToString)
                        End Try
                        '=================================================
                    ElseIf TypeOf (ctrl) Is common.Controls.MyCheckBox Or TypeOf (ctrl) Is CheckBox Or TypeOf (ctrl) Is RadCheckBox Then
                        Try
                            If dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'").Length > 0 Then
                                ctrl.Text = dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("New_Label_Name")
                                '  ctrl.ForeColor = dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font_Color")
                                ' ctrl.BackColor = dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Back_Color")

                                If clsCommon.myCstr(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font").ToString) <> "" Then
                                    ctrl.Font = New System.Drawing.Font(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font").ToString, Convert.ToSingle(dtLabelData.Select("LABEL_ID='" & ctrl.Name.ToString & "'")(0)("Label_Font_Size")), FontStyle.Regular)
                                End If
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.ToString)
                        End Try
                        '===================================================
                    End If
                   
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If
        End If
    End Sub

    Private Sub cboFormName_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs)
        Try

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Public Shared Sub CancelDocument(ByRef formname As XpertERPEngine.FrmMainTranScreen, ByVal FormId As String, ByVal Trans As SqlClient.SqlTransaction, ByVal cancel_after_Posting_Date As Date, Optional ByVal contrl As Control = Nothing, Optional ByVal AllowDeleteAfterPosting As Boolean = False, Optional ByVal CheckDate As Boolean = False)
        is_Cancel_Allowed_Posted = clsFixedParameter.GetData(clsFixedParameterType.is_Allow_cancel_Posted_Transaction, clsFixedParameterCode.is_Allow_cancel_Posted_Transaction, Trans)
        If is_Cancel_Allowed_Posted = False Or AllowDeleteAfterPosting = False Then
            If CancelDocumentStatus(formname, FormId, Trans, cancel_after_Posting_Date, contrl, AllowDeleteAfterPosting) = True Then
                Trans.Rollback()
                clsCommon.MyMessageBoxShow("Posted Transaction can not be cancel")
                Exit Sub
            End If
        End If
        If CheckDate = True Then
            If CancelDocumentStatusForDate(formname, FormId, Trans, cancel_after_Posting_Date, contrl, AllowDeleteAfterPosting) = True Then
                Trans.Rollback()
                clsCommon.MyMessageBoxShow("Please Check starting Date for Posted Transaction cancel")
                Exit Sub
            End If
        End If
        If IsNothing(contrl) Then
            For Each ctrl As Control In formname.Controls
                If ctrl.HasChildren = True Then
                    CancelDocument(formname, FormId, Trans, cancel_after_Posting_Date, ctrl, AllowDeleteAfterPosting)
                End If
                If TypeOf (ctrl) Is common.UserControls.txtNavigator Then
                    Try
                        If GetDocumentExits(formname, FormId, Trans, ctrl) = True Then
                            Exit For
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
            Next
        Else
            For Each ctrl As Control In contrl.Controls
                If ctrl.HasChildren = True Then
                    CancelDocument(formname, FormId, Trans, cancel_after_Posting_Date, ctrl, AllowDeleteAfterPosting)
                End If
                If TypeOf (ctrl) Is common.UserControls.txtNavigator Then
                    Try
                        If GetDocumentExits(formname, FormId, Trans, ctrl) = True Then
                            Exit For
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
            Next
        End If
    End Sub

    Public Shared Function CancelDocumentStatus(ByRef formname As XpertERPEngine.FrmMainTranScreen, ByVal FormId As String, ByVal Trans As SqlClient.SqlTransaction, ByVal cancel_after_Posting_Date As Date, Optional ByVal contrl As Control = Nothing, Optional ByVal AllowDeleteAfterPosting As Boolean = False)
        If IsNothing(contrl) Then
            For Each ctrl As Control In formname.Controls
                If ctrl.HasChildren = True Then
                    If CancelDocumentStatus(formname, FormId, Trans, cancel_after_Posting_Date, ctrl, AllowDeleteAfterPosting) = True Then
                        Return True
                    End If
                End If
                If TypeOf (ctrl) Is common.usLock Then
                    Try
                        If TryCast(ctrl, common.usLock).Status = ERPTransactionStatus.Approved And AllowDeleteAfterPosting = False Then
                            Return True
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                ElseIf TypeOf (ctrl) Is common.Controls.MyDateTimePicker And clsCommon.myLen(cancel_after_Posting_Date) > 5 Then
                    Try
                        If TryCast(ctrl, common.Controls.MyDateTimePicker).Value <= cancel_after_Posting_Date Then
                            Return True
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
            Next
        Else
            For Each ctrl As Control In contrl.Controls
                If ctrl.HasChildren = True Then
                    If CancelDocumentStatus(formname, FormId, Trans, cancel_after_Posting_Date, ctrl, AllowDeleteAfterPosting) = True Then
                        Return True
                    End If
                End If
                If TypeOf (ctrl) Is common.usLock Then
                    Try
                        If TryCast(ctrl, common.usLock).Status = ERPTransactionStatus.Approved And AllowDeleteAfterPosting = False Then
                            Return True
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                ElseIf TypeOf (ctrl) Is common.Controls.MyDateTimePicker And clsCommon.myLen(cancel_after_Posting_Date) > 5 Then
                    Try
                        If TryCast(ctrl, common.Controls.MyDateTimePicker).Value <= cancel_after_Posting_Date Then
                            Return True
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
            Next
        End If
        Return False
    End Function

    Public Shared Function CancelDocumentStatusForDate(ByRef formname As XpertERPEngine.FrmMainTranScreen, ByVal FormId As String, ByVal Trans As SqlClient.SqlTransaction, ByVal cancel_after_Posting_Date As Date, Optional ByVal contrl As Control = Nothing, Optional ByVal AllowDeleteAfterPosting As Boolean = False)
         '========Check Cancel Date================
        If IsNothing(contrl) Then
            For Each ctrl As Control In formname.Controls
                If ctrl.HasChildren = True Then
                    If CancelDocumentStatusForDate(formname, FormId, Trans, cancel_after_Posting_Date, ctrl, AllowDeleteAfterPosting) = True Then
                        Return True
                    End If
                End If
                If TypeOf (ctrl) Is common.Controls.MyDateTimePicker And clsCommon.myLen(cancel_after_Posting_Date) > 5 Then
                    Try
                        If TryCast(ctrl, common.Controls.MyDateTimePicker).Value <= cancel_after_Posting_Date And ctrl.Visible = True Then
                            Return True
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
            Next
        Else
            For Each ctrl As Control In contrl.Controls
                If ctrl.HasChildren = True Then
                    If CancelDocumentStatusForDate(formname, FormId, Trans, cancel_after_Posting_Date, ctrl, AllowDeleteAfterPosting) = True Then
                        Return True
                    End If
                End If
                If TypeOf (ctrl) Is common.Controls.MyDateTimePicker And clsCommon.myLen(cancel_after_Posting_Date) > 5 Then
                    Try
                        If TryCast(ctrl, common.Controls.MyDateTimePicker).Value <= cancel_after_Posting_Date And ctrl.Visible = True Then
                            Return True
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
            Next
        End If
        '=========================================================
        Return False
    End Function

    Public Shared Function GetDocumentExits(ByRef formname As XpertERPEngine.FrmMainTranScreen, ByVal Formid As String, ByVal trans As SqlClient.SqlTransaction, Optional ByVal contrl As common.UserControls.txtNavigator = Nothing)
        Try
            Dim isReturn As Boolean = False
            Dim condition As String = String.Empty
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_Cancel_Table_Validate_Details where Form_Id='" & Formid & "'", trans)
            If dt.Rows.Count > 0 Then
                For Each row_main As DataRow In dt.Rows
                    condition = GetCancelConditionTableQuery(Formid, clsCommon.myCstr(row_main("Level_Id")), contrl.Value, trans, True)
                    If ValidateTable(clsCommon.myCstr(row_main("Valicate_tb_name")), clsCommon.myCstr(row_main("Valicate_column_name")), contrl.Value, clsCommon.myCstr(row_main("Type_col_name")), clsCommon.myCstr(row_main("Type_Col_Value")), trans, condition) = False Then
                        If clsCommon.myLen(clsCommon.myCstr(row_main("REturn_QUery"))) > 0 Then
                            'Dim sQuery As String = clsCommon.myCstr(row_main("REturn_QUery")) & "'" & contrl.Value & "')"
                            Dim sQuery As String = Replace(clsCommon.myCstr(row_main("REturn_QUery")), "@Value", contrl.Value)
                            Dim cc As Integer = clsDBFuncationality.getSingleValue(sQuery, trans)
                            If cc > 0 Then
                                isReturn = True
                                sQuery = Nothing
                                cc = Nothing
                                GoTo a
                            End If
                        End If
                        trans.Rollback()
                        clsCommon.ProgressBarHide()
                        clsCommon.MyMessageBoxShow("This Record is in Used in " & clsCommon.myCstr(row_main("Valicate_Form_name")))
                        Return False
                    End If
                Next
            End If
a:          Dim objtxtbox As New FrmFreeTxtBox1
            objtxtbox.ShowDialog()
            clsCommon.ProgressBarShow()
            dt = clsDBFuncationality.GetDataTable("select * from TSPL_Cancel_Table_Details where Form_Id='" & Formid & "' order by Level_Id desc", trans)
            If dt.Rows.Count > 0 Then
                For Each row_main As DataRow In dt.Rows
                    condition = GetCancelConditionTableQuery(Formid, clsCommon.myCstr(row_main("Level_Id")), contrl.Value, trans)

                    If clsCommon.CompairString(clsCommon.myCstr(row_main("tb_name")), "tspl_journal_details") = CompairStringResult.Equal Then
                        If clsCommon.myLen(clsCommon.myCstr(condition)) > 0 Then
                            If clsCommon.myLen(clsCommon.myCstr(row_main("Type_col_name"))) > 0 Then
                                condition &= ") and " & clsCommon.myCstr(row_main("Type_col_name")) & "='" & clsCommon.myCstr(row_main("Type_col_Value")) & "'"
                            Else
                                condition &= ") "
                            End If
                            sQuery = "Delete from " & row_main("tb_Name") & " where " & row_main("Column_Name") & "=(" & condition & ""
                        Else
                            If clsCommon.myLen(clsCommon.myCstr(row_main("Type_col_name"))) <= 0 Then
                                sQuery = "Delete from " & row_main("tb_Name") & " where " & row_main("Column_Name") & "='" & contrl.Value & "' "
                            Else
                                sQuery = "Delete from " & row_main("tb_Name") & " where " & row_main("Column_Name") & "='" & contrl.Value & "'  and " & clsCommon.myCstr(row_main("Type_col_name")) & "='" & clsCommon.myCstr(row_main("Type_col_Value")) & "'"
                            End If
                        End If
                        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                        Continue For
                    End If

                    'If ValidateTable(clsCommon.myCstr(row_main("Validate_tb_name")), clsCommon.myCstr(row_main("Validate_column_name")), contrl.Value, trans) = True Then
                    If condition = "" Then
                        If clsCommon.myLen(clsCommon.myCstr(row_main("Type_col_name"))) <= 0 Then
                            sQuery = "select count(*) from " & row_main("tb_Name") & " where " & row_main("Column_Name") & "='" & contrl.Value & "'"
                        Else
                            sQuery = "select count(*) from " & row_main("tb_Name") & " where " & row_main("Column_Name") & "='" & contrl.Value & "' and " & clsCommon.myCstr(row_main("Type_col_name")) & "='" & clsCommon.myCstr(row_main("Type_col_Value")) & "'"
                        End If

                        Dim isExits As Integer = clsDBFuncationality.getSingleValue(sQuery, trans)
                        If isExits > 0 Then
                            sQuery = "select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='" & row_main("tb_Name") & "'"
                            Dim dtColumns As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
                            Dim column_Name As String = ""
                            If dtColumns.Rows.Count > 0 Then
                                For Each row As DataRow In dtColumns.Rows
                                    If clsCommon.myLen(column_Name) <= 0 Then
                                        column_Name = clsCommon.myCstr(row.Item("Column_Name"))
                                    Else
                                        column_Name = column_Name & "," & clsCommon.myCstr(row.Item("Column_Name"))
                                    End If
                                Next
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(row_main("Type_col_name"))) <= 0 Then
                                sQuery = "Insert into " & row_main("tb_Name_History") & " (" & column_Name & ") select " & column_Name & " from " & row_main("tb_Name") & " where " & row_main("Column_Name") & "='" & contrl.Value & "' "
                            Else
                                sQuery = "Insert into " & row_main("tb_Name_History") & " (" & column_Name & ") select " & column_Name & " from " & row_main("tb_Name") & " where " & row_main("Column_Name") & "='" & contrl.Value & "' and " & clsCommon.myCstr(row_main("Type_col_name")) & "='" & clsCommon.myCstr(row_main("Type_col_Value")) & "'"
                            End If

                            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                            Try
                                If clsCommon.myLen(clsCommon.myCstr(row_main("Type_col_name"))) <= 0 Then
                                    Try
                                        sQuery = "update  " & row_main("tb_Name_History") & "  set  Version_Id=(select coalesce(Max(Version_Id),1) " _
                                                                          & " from " & row_main("tb_Name_History") & " where  " & row_main("Column_Name") & "='" & contrl.Value & "')  where  " & row_main("Column_Name") & "='" & contrl.Value & "'"
                                        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                                    Catch ex As Exception
                                    End Try
                                    sQuery = "update  " & row_main("tb_Name_History") & "  set History_By='" & objCommonVar.CurrentUserCode & "',History_Date=" _
                                        & " '" & clsCommon.GETSERVERDATE(trans) & "' where  " & row_main("Column_Name") & "='" & contrl.Value & "'"
                                Else
                                    Try
                                        sQuery = "update  " & row_main("tb_Name_History") & "  set  Version_Id=(select coalesce(Max(Version_Id),1) " _
                                                                        & " from " & row_main("tb_Name_History") & " where  " & row_main("Column_Name") & "='" & contrl.Value & "')  where  " & row_main("Column_Name") & "='" & contrl.Value & "' and " & clsCommon.myCstr(row_main("Type_col_name")) & "='" & clsCommon.myCstr(row_main("Type_col_Value")) & "'"
                                        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                                    Catch ex As Exception
                                    End Try
                                    sQuery = "update  " & row_main("tb_Name_History") & "  set History_By='" & objCommonVar.CurrentUserCode & "',History_Date=" _
                                        & " '" & clsCommon.GETSERVERDATE(trans) & "' where  " & row_main("Column_Name") & "='" & contrl.Value & "' and " & clsCommon.myCstr(row_main("Type_col_name")) & "=" & clsCommon.myCstr(row_main("Type_col_Value")) & ""
                                End If

                                clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                                Try
                                    sQuery = "update  " & row_main("tb_Name_History") & "  set  Cancel_Remark='" & objtxtbox.strRmks & "'  where  " & row_main("Column_Name") & "='" & contrl.Value & "'"
                                    clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                                Catch ex As Exception
                                End Try
                            Catch ex As Exception
                            End Try
                            If Not isReturn Then
                                If clsCommon.myLen(clsCommon.myCstr(row_main("Type_col_name"))) <= 0 Then
                                    sQuery = "Delete from " & row_main("tb_Name") & " where " & row_main("Column_Name") & "='" & contrl.Value & "' "
                                Else
                                    sQuery = "Delete from " & row_main("tb_Name") & " where " & row_main("Column_Name") & "='" & contrl.Value & "'  and " & clsCommon.myCstr(row_main("Type_col_name")) & "='" & clsCommon.myCstr(row_main("Type_col_Value")) & "'"
                                End If
                            Else
                                If clsCommon.myLen(clsCommon.myCstr(row_main("Type_col_name"))) <= 0 Then
                                    sQuery = "Update " & row_main("tb_Name") & " set " & row_main("Update_Column_Name") & " where " & row_main("Column_Name") & "='" & contrl.Value & "' "
                                Else
                                    sQuery = "Update " & row_main("tb_Name") & " set " & row_main("Update_Column_Name") & " where " & row_main("Column_Name") & "='" & contrl.Value & "'  and " & clsCommon.myCstr(row_main("Type_col_name")) & "='" & clsCommon.myCstr(row_main("Type_col_Value")) & "'"
                                End If
                            End If
                           

                            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                        End If

                    Else
                        If clsCommon.myLen(clsCommon.myCstr(row_main("Type_col_name"))) > 0 Then
                            condition &= ") and " & clsCommon.myCstr(row_main("Type_col_name")) & "='" & clsCommon.myCstr(row_main("Type_col_Value")) & "'"
                        Else
                            condition &= ") "
                        End If
                        sQuery = "select count(*) from " & row_main("tb_Name") & " where " & row_main("Column_Name") & "=(" & condition & ""
                        Dim isExits As Integer = clsDBFuncationality.getSingleValue(sQuery, trans)
                        If isExits > 0 Then
                            sQuery = "select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='" & row_main("tb_Name") & "'"
                            Dim dtColumns As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
                            Dim column_Name As String = ""
                            If dtColumns.Rows.Count > 0 Then
                                For Each row As DataRow In dtColumns.Rows
                                    If clsCommon.myLen(column_Name) <= 0 Then
                                        column_Name = clsCommon.myCstr(row.Item("Column_Name"))
                                    Else
                                        column_Name = column_Name & "," & clsCommon.myCstr(row.Item("Column_Name"))
                                    End If
                                Next
                            End If
                            sQuery = "Insert into " & row_main("tb_Name_History") & " (" & column_Name & ") select " & column_Name & " from " & row_main("tb_Name") & " where " & row_main("Column_Name") & "=(" & condition & ""
                            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                            Try
                                sQuery = "update  " & row_main("tb_Name_History") & "  set  Version_Id=(select coalesce(Max(Version_Id),1) " _
                                   & " from " & row_main("tb_Name_History") & " where  " & row_main("Column_Name") & "=(" & condition & ")  where  " & row_main("Column_Name") & "=(" & condition & ""
                                clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                                sQuery = "update  " & row_main("tb_Name_History") & "  set History_By='" & objCommonVar.CurrentUserCode & "',History_Date=" _
                                    & " '" & clsCommon.GETSERVERDATE(trans) & "' where  " & row_main("Column_Name") & "=(" & condition & ""
                                clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                            Catch ex As Exception
                            End Try
                            Try
                                sQuery = "update  " & row_main("tb_Name_History") & "  set   Cancel_Remark='" & objtxtbox.strRmks & "'  where  " & row_main("Column_Name") & "=(" & condition & ""
                                clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                            Catch ex As Exception
                            End Try
                            If Not isReturn Then
                                sQuery = "Delete from " & row_main("tb_Name") & " where " & row_main("Column_Name") & "=(" & condition & ""
                                clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                            Else
                                sQuery = "Update " & row_main("tb_Name") & " set " & row_main("Update_Column_Name") & " where " & row_main("Column_Name") & "=(" & condition & ""
                            End If
                        End If
                    End If
                    'Else
                    'clsCommon.MyMessageBoxShow("Document can not be Cancelled.")
                    'Return False
                    'End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow("Document Cancelled Successfully.")
                formname.ResetText()
                ResetControls(formname, Formid, trans, contrl)
                Return True
            Else
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow("Please Make entry in cancel table for Cancel document")
                Return False
            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.ToString)
            Return False
        End Try
    End Function

    Public Shared Function ValidateTable(ByVal tb_name As String, ByVal col_name As String, ByVal val As String, ByVal Type_col_name As String, ByVal Type_val As String, ByVal trans As SqlClient.SqlTransaction, ByVal Condition As String)
        Try
            If clsCommon.myLen(tb_name) <= 0 Then
                Return True
            End If
            If clsCommon.myLen(clsCommon.myCstr(Condition)) <= 0 Then
                If clsCommon.myLen(Type_col_name) <= 0 Then
                    sQuery = "select count(*) from " & tb_name & " where " & col_name & "='" & val & "'"
                Else
                    sQuery = "select count(*) from " & tb_name & " where " & col_name & "='" & val & "' and " & Type_col_name & "='" & Type_val & "'"
                End If
            Else
                If clsCommon.myLen(Type_col_name) <= 0 Then
                    sQuery = "select count(*) from " & tb_name & " where " & col_name & "=(" & Condition & ")"
                Else
                    sQuery = "select count(*) from " & tb_name & " where " & col_name & "=(" & Condition & ") and " & Type_col_name & "='" & Type_val & "'"
                End If
            End If

            Dim isExits As Integer = clsDBFuncationality.getSingleValue(sQuery, trans)
            If isExits > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
            Return False
        End Try
    End Function

    Public Shared Function GetCancelConditionTableQuery(ByVal Form_Id As String, ByVal Level_Id As String, ByVal val As String, ByVal trans As SqlClient.SqlTransaction, Optional ByVal is_for_validate As Boolean = False)
        Try
            sQuery = "select * from TSPL_Cancel_Condition_Tables_Details where base_Level_Id='" & Level_Id & "' and form_Id='" & Form_Id & "' " & IIf(is_for_validate = True, "and is_for_validate='1'", "and is_for_validate=''") & "  order by Condition_Level_Id desc"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
            sQuery = ""
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    If clsCommon.myLen(clsCommon.myCstr(row.Item("Type_col_name"))) <= 0 Then
                        If sQuery = "" Then
                            sQuery = "select " & clsCommon.myCstr(row.Item("Condition_col_name")) & " from " & clsCommon.myCstr(row.Item("Condition_tb_name")) & " where " & clsCommon.myCstr(row.Item("Condition_Foreign_col_name")) & "='" & val & "'"
                        Else
                            sQuery = "select  " & clsCommon.myCstr(row.Item("Condition_col_name")) & " from " & clsCommon.myCstr(row.Item("Condition_tb_name")) & " where " & clsCommon.myCstr(row.Item("Condition_Foreign_col_name")) & "=(" & sQuery & ")"
                        End If
                    Else
                        If sQuery = "" Then
                            sQuery = "select " & clsCommon.myCstr(row.Item("Condition_col_name")) & " from " & clsCommon.myCstr(row.Item("Condition_tb_name")) & " where " & clsCommon.myCstr(row.Item("Condition_Foreign_col_name")) & "='" & val & "'  and " & clsCommon.myCstr(row("Type_col_name")) & "='" & clsCommon.myCstr(row("Type_col_Value")) & "'"
                        Else
                            sQuery = "select  " & clsCommon.myCstr(row.Item("Condition_col_name")) & " from " & clsCommon.myCstr(row.Item("Condition_tb_name")) & " where " & clsCommon.myCstr(row.Item("Condition_Foreign_col_name")) & "=(" & sQuery & ")  and " & clsCommon.myCstr(row("Type_col_name")) & "='" & clsCommon.myCstr(row("Type_col_Value")) & "'"
                        End If
                    End If
                Next
            End If
            Return sQuery
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
            Return ""
        End Try
    End Function

    Public Shared Sub HideDeleteButon(ByRef formname As XpertERPEngine.FrmMainTranScreen, ByVal Formid As String, ByVal trans As SqlClient.SqlTransaction)
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_Cancel_Table_Details where Form_Id='" & Formid & "'", trans)
            If dt.Rows.Count > 0 Then
                HideDeleteButtons(formname, Formid, trans, Nothing)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Public Shared Sub HideDeleteButtons(ByRef formname As XpertERPEngine.FrmMainTranScreen, ByVal FormId As String, ByVal Trans As SqlClient.SqlTransaction, Optional ByVal contrl As Control = Nothing)
        If IsNothing(contrl) Then
            For Each ctrl As Control In formname.Controls
                If ctrl.HasChildren = True Then
                    HideDeleteButtons(formname, FormId, Trans, ctrl)
                End If
                If TypeOf (ctrl) Is RadButton Then
                    Try
                        If ctrl.Name.ToLower.Contains("delete") Or ctrl.Name.ToLower.Contains("dlt") Then
                            ctrl.Visible = False
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
            Next
        Else
            For Each ctrl As Control In contrl.Controls
                If ctrl.HasChildren = True Then
                    HideDeleteButtons(formname, FormId, Trans, ctrl)
                End If
                If TypeOf (ctrl) Is RadButton Then
                    Try
                        If ctrl.Name.ToLower.Contains("delete") Or ctrl.Name.ToLower.Contains("dlt") Then
                            ctrl.Visible = False
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
            Next
        End If
    End Sub

    Public Shared Sub ResetControls(ByRef formname As XpertERPEngine.FrmMainTranScreen, ByVal FormId As String, ByVal Trans As SqlClient.SqlTransaction, Optional ByVal contrl As Control = Nothing)
        If IsNothing(contrl) Then
            For Each ctrl As Control In formname.Controls
                If ctrl.HasChildren = True Then
                    ResetControls(formname, FormId, Trans, ctrl)
                End If
                If TypeOf (ctrl) Is RadTextBox Then
                    Try
                        ctrl.Text = ""
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
                If TypeOf (ctrl) Is common.UserControls.txtNavigator Then
                    Try
                        TryCast(ctrl, common.UserControls.txtNavigator).Value = ""
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
                If TypeOf (ctrl) Is common.UserControls.txtFinder Then
                    Try
                        TryCast(ctrl, common.UserControls.txtFinder).Value = ""
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
            Next
        Else
            For Each ctrl As Control In contrl.Controls
                If ctrl.HasChildren = True Then
                    ResetControls(formname, FormId, Trans, ctrl)
                End If
                If TypeOf (ctrl) Is RadTextBox Then
                    Try
                        ctrl.Text = ""
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
                If TypeOf (ctrl) Is common.UserControls.txtNavigator Then
                    Try
                        TryCast(ctrl, common.UserControls.txtNavigator).Value = ""
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
                If TypeOf (ctrl) Is common.UserControls.txtFinder Then
                    Try
                        TryCast(ctrl, common.UserControls.txtFinder).Value = ""
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
            Next
        End If
    End Sub


End Class