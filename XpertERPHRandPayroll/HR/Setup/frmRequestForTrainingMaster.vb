Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmRequestForTrainingMaster

    Dim isnewentry As Boolean
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim isFlag As Boolean = False
    Dim strQry As String = ""
    Dim IsCustLoad As Boolean = False

    Function AllowTosave() As Boolean
        'If clsCommon.myLen(txt_Code.Value) <= 0 Then
        '    txt_Code.Focus()
        '    Throw New Exception("Code cannot be left blank")
        'End If
        If clsCommon.myLen(FndTrainingCourse.Value) <= 0 Then
            myMessages.blankValue("Course")

            FndTrainingCourse.Focus()
            FndTrainingCourse.Select()
            Errorcontrol.SetError(FndTrainingCourse, "Course")
            Return False
        Else
            Errorcontrol.ResetError(FndTrainingCourse)
        End If

        If cbgDept.CheckedValue.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please select department.")
            Return False
        End If
        If cbgEmp.CheckedValue.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please select employee.")
            Return False
        End If
        'If clsCommon.myLen(FndEmployee.Value) <= 0 Then
        '    myMessages.blankValue("Employee Code")

        '    FndEmployee.Focus()
        '    FndEmployee.Select()
        '    Errorcontrol.SetError(FndEmployee, "Employee Code")
        '    Return False
        'Else
        '    Errorcontrol.ResetError(FndEmployee)
        'End If

        'If txt_Code.Value = "" Then
        '    MessageBox.Show("Code cannot be blank")
        '    Return False
        'ElseIf txt_Name.Text = "" Then

        '    MessageBox.Show("Name cannot be blank")
        '    Return False
        'End If
        Return True
    End Function
    Sub SaveData()
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowTosave() Then
                Dim obj As New ClsRequestForTrainingMaster

                obj.Code = txt_Code.Value
                obj.Remark = txt_remarks.Text
                obj.Doc_Date = DtpDate.Value
                obj.Training_Code = FndTrainingCourse.Value
                obj.Employee_Code = FndEmployee.Value
                obj.ObjDept = New List(Of ClsRequestForTrainingMasterDept)
                obj.ObjEmp = New List(Of ClsRequestForTrainingMasterEmp)
                ' For Each grow As GridViewRowInfo In cbgDept.CheckedValue
                For Each strv As String In cbgDept.CheckedValue
                    Dim objTr As New ClsRequestForTrainingMasterDept()
                    objTr.Dept_Code = clsCommon.myCstr(strv)
                    '  objTr.Dept_Name = clsCommon.myCstr(grow.Cells(ColUnvClg).Value)
                    objTr.Request_Code = clsCommon.myCstr(obj.Code)

                    obj.ObjDept.Add(objTr)
                Next
                For Each strv1 As String In cbgEmp.CheckedValue
                    Dim objTr1 As New ClsRequestForTrainingMasterEmp()
                    objTr1.Emp_Code = clsCommon.myCstr(strv1)
                    '  objTr.Dept_Name = clsCommon.myCstr(grow.Cells(ColUnvClg).Value)
                    ' objTr1.Request_Code = clsCommon.myCstr(obj.Code)
                    '   objTr1.Dept_Code = clsCommon.myCstr()
                    obj.ObjEmp.Add(objTr1)
                Next
                If (ClsRequestForTrainingMaster.SaveData(obj, isnewentry)) Then
                    If Not isFlag Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                        LoadData(obj.Code, NavigatorType.Current)
                    Else
                        clsCommon.MyMessageBoxShow("Data posted successfully")
                    End If
                End If
            End If

        Catch ex As Exception
            'trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub butnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnsave.Click
        SaveData()

    End Sub
    Sub DeleteData()

        Try
            If clsCommon.myLen(txt_Code.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("Are you sure? Do you want to Delete this Code ('" + txt_Code.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim obj As New ClsRequestForTrainingMaster
                If obj.DeleteData(txt_Code.Value) Then
                    clsCommon.MyMessageBoxShow("Deleted Successfully", Me.Text)
                    ResetData()
                End If
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Code is in use")
            Else
                clsCommon.MyMessageBoxShow(ex.Message)
            End If
        End Try


    End Sub
    Sub ResetData()
        txt_Code.Value = ""
        txt_remarks.Text = ""
        DtpDate.Value = Today.Date
        FndEmployee.Value = Nothing
        FndTrainingCourse.Value = Nothing
        LblEmployeeName.Text = ""
        lblTrainingCourseName.Text = ""
        Btnsave.Enabled = True
        BtnPost.Enabled = False
        BtnDelete.Enabled = False
        Btnsave.Text = "Save"
        UsLock1.Status = ERPTransactionStatus.Pending
        cbgDept.DataSource = Nothing
        cbgEmp.DataSource = Nothing
        LoadDept()
        'If IsCustLoad = True Then
        '    LoadEmp()
        'End If
        'IsCustLoad = False
    End Sub
    Private Sub butnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        DeleteData()
        ResetData()
    End Sub

    Private Sub butnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Sub LoadData(ByVal obj As String, ByVal NavigatorType As NavigatorType)

        Btnsave.Text = "Save"
        Dim cls As ClsRequestForTrainingMaster = ClsRequestForTrainingMaster.GetData(obj, NavigatorType)

        If cls IsNot Nothing Then
            IsCustLoad = True
            ResetData()
            txt_Code.Value = cls.Code
            txt_remarks.Text = cls.Remark
            FndTrainingCourse.Value = cls.Training_Code
            FndEmployee.Value = cls.Employee_Code
            DtpDate.Value = cls.Doc_Date
            lblTrainingCourseName.Text = clsDBFuncationality.getSingleValue("select name from tspl_Training_master where code='" & FndTrainingCourse.Value & "'")
            LblEmployeeName.Text = clsDBFuncationality.getSingleValue("select emp_nAME from tspl_Employee_master where emp_code='" & FndEmployee.Value & "'")
            Btnsave.Text = "Update"
            BtnDelete.Enabled = True
            BtnPost.Enabled = True
            isnewentry = False
            If cls.Posted = ERPTransactionStatus.Approved Then
                Btnsave.Enabled = False
                BtnPost.Enabled = False
                BtnDelete.Enabled = False
            End If
            UsLock1.Status = cls.Posted
            '' Department Grid
            Dim j As Int16 = 0
            Dim lstDept As New ArrayList()
            Dim lstEmp As New ArrayList()
            '' Dept
            If cls.ObjDept IsNot Nothing AndAlso cls.ObjDept.Count > 0 Then
                LoadDept()
                For Each objTr As ClsRequestForTrainingMasterDept In cls.ObjDept
                    lstDept.Add(objTr.Dept_Code)
                Next
            End If
            cbgDept.CheckedValue = lstDept
            '' Emp
            If cls.ObjEmp IsNot Nothing AndAlso cls.ObjEmp.Count > 0 Then
                ' LoadEmp()
                For Each objTr As ClsRequestForTrainingMasterEmp In cls.ObjEmp
                    lstEmp.Add(objTr.Emp_Code)
                Next
            End If
            cbgEmp.CheckedValue = lstEmp
        End If
    End Sub

    Private Sub txt_Code__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txt_Code._MYNavigator
        LoadData(txt_Code.Value, NavType)
        Btnsave.Text = "Update"
    End Sub

    Private Sub txt_Code__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txt_Code._MYValidating
        Dim qry As String
        If isButtonClicked Then
            qry = "select tm.Code,ttm.name as [Training Course],Emp_name as [Employee],Remark from Tspl_Request_For_Training_Master tm left join tspl_employee_master " _
            & " em on Emp_code=Employee_code  left join tspl_Training_master ttm on ttm.code=training_code"
            txt_Code.Value = clsCommon.ShowSelectForm("id", qry, "Code", "", txt_Code.Value, "", isButtonClicked)
            If clsCommon.myLen(txt_Code.Value) > 0 Then
                LoadData(txt_Code.Value, NavigatorType.Current)

            End If
        End If
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        ResetData()
    End Sub

    Private Sub RMImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMImport.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Dim sQuery As String = ""
        Dim dt As DataTable
        'Dim trans As SqlTransaction
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Code", "Remark", "Training Code", "Doc Date", "Employee Code") Then
            Dim linno As Integer = 1
            Try
                'trans = clsDBFuncationality.GetTransactin()
                'connectSql.OpenConnection()
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New ClsRequestForTrainingMaster()
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    If clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception("Code should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If

                    Dim strRemark As String = clsCommon.myCstr(grow.Cells("Remark").Value)

                    Dim strTraining_Code As String = clsCommon.myCstr(grow.Cells("Training Code").Value)
                    If clsCommon.myLen(strTraining_Code) <= 0 Then
                        Throw New Exception("Training Code should not be left blank" + clsCommon.myCstr(linno) + ".")
                    Else
                        sQuery = "select * from tspl_training_master where code='" & strTraining_Code & "'"
                        dt = clsDBFuncationality.GetDataTable(sQuery)
                        If dt.Rows.Count <= 0 Then
                            Throw New Exception("Training Code dose not Exits in Training Master Row No " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    Dim strEmployee_Code As String = clsCommon.myCstr(grow.Cells("Employee Code").Value)
                    If clsCommon.myLen(strEmployee_Code) <= 0 Then
                        Throw New Exception("Employee Code should not be left blank" + clsCommon.myCstr(linno) + ".")
                    Else
                        sQuery = "select * from tspl_employee_master where emp_code='" & strEmployee_Code & "'"
                        dt = clsDBFuncationality.GetDataTable(sQuery)
                        If dt.Rows.Count <= 0 Then
                            Throw New Exception("Employee Code dose not Exits in Employee Master Row No " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    Dim strDoc_Date As Date = clsCommon.myCDate(grow.Cells("Doc Date").Value)
                    If clsCommon.myLen(strDoc_Date) <= 0 Then
                        Throw New Exception("Doc Date should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If


                    obj.Code = strCode
                    obj.Remark = strRemark
                    obj.Doc_Date = strDoc_Date
                    obj.Training_Code = strTraining_Code
                    obj.Employee_Code = strEmployee_Code

                    ClsRequestForTrainingMaster.SaveData(obj, IsNewEntry)
                    linno += 1
                Next
                'trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                'trans.Rollback()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RMExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMExport.Click
        Dim str As String
        str = "select tm.Code,Doc_Date as [Doc Date],ttm.code as [Training Code],ttm.name as [Training Course],Employee_Code as [Employee Code]," _
        & " Emp_name as [Employee Name],Remark from Tspl_Request_For_Training_Master tm left join tspl_employee_master  em on Emp_code=Employee_code " _
        & " left join tspl_Training_master ttm on ttm.code=training_code "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub frmRequestForTrainingMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso Btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso BtnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            ResetData()
        End If
    End Sub

    Private Sub frmRequestForTrainingMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            SetUserMgmtNew()
            isnewentry = True
            ButtonToolTip.SetToolTip(Btnsave, "Press Alt+S for Save/Update ")
            ButtonToolTip.SetToolTip(BtnDelete, "Press Alt+D  for Delete ")
            ButtonToolTip.SetToolTip(BtnClose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
            ResetData()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.TrainingRequestMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        Btnsave.Visible = MyBase.isModifyFlag
        BtnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadDept()
        strQry = "SELECT DEPARTMENT_CODE As [Department Code],DEPARTMENT_NAME As [Department Name] FROM tspl_department_master "

        cbgDept.DataSource = clsDBFuncationality.GetDataTable(strQry)
        cbgDept.ValueMember = "Department Code"
        cbgDept.DisplayMember = "Department Name"
    End Sub
    Sub LoadEmp()
        strQry = "SELECT EMP_CODE AS [Emp Code],Emp_Name As [Emp Name] FROM TSPL_EMPLOYEE_MASTER"

        cbgEmp.DataSource = clsDBFuncationality.GetDataTable(strQry)
        cbgEmp.ValueMember = "Emp Code"
        cbgEmp.DisplayMember = "Emp Name"

    End Sub
    Private Sub FndTrainingCourse__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndTrainingCourse._MYValidating
        Try
            Dim sQuery As String = "select code as [Code],name as [Name] from tspl_Training_master"
            FndTrainingCourse.Value = clsCommon.ShowSelectForm("Training_Master", sQuery, "Code", "", FndTrainingCourse.Value, "Code", isButtonClicked)
            If FndTrainingCourse.Value <> "" Then
                lblTrainingCourseName.Text = clsDBFuncationality.getSingleValue("select name from tspl_Training_master where code='" & FndTrainingCourse.Value & "'")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub FndEmployee__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndEmployee._MYValidating
        Try
            FndEmployee.Value = clsEmployeeMaster.getFinder("", FndEmployee.Value, isButtonClicked)
            If FndEmployee.Value <> "" Then
                Dim Trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                LblEmployeeName.Text = clsEmployeeMaster.GetName(FndEmployee.Value, Trans)
                Trans.Commit()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub
    Sub PostData()
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim Code As String = ""
            isFlag = True
            If clsCommon.myLen(txt_Code.Value) > 0 Then
                Code = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) AS Doc_Code from Tspl_Request_For_Training_Master  where Code='" + txt_Code.Value + "'"))
                If Code > 0 Then
                    If (myMessages.postConfirm()) Then
                        SaveData()
                        If (ClsRequestForTrainingMaster.PostData(MyBase.Form_ID, txt_Code.Value)) Then
                            'msg = "Successfully Posted"
                            'common.clsCommon.MyMessageBoxShow(msg)
                            LoadData(txt_Code.Value, NavigatorType.Current)
                        End If
                    End If
                Else
                    Throw New Exception("You cannot post this entry before entering code")
                End If

            Else
                Throw New Exception("Code not found to Post")
            End If
            'isFlag = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isFlag = False
        End Try
    End Sub
    Private Sub BtnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnPost.Click
        If clsCommon.myLen(txt_Code.Value) > 0 Then
            'If (myMessages.postConfirm()) Then
            PostData()
            'End If
        Else
            clsCommon.MyMessageBoxShow("code not found to post")
        End If
    End Sub

    Private Sub ChkDeptAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        cbgDept.Enabled = False
    End Sub

    Private Sub ChkDeptSelect_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        cbgDept.Enabled = True
    End Sub

    Private Sub cmbgEmp__MyCheckChanged(sender As Object, e As EventArgs) Handles cbgEmp._MyCheckChanged

    End Sub

    Private Sub cbgDept__MyCheckChanged(sender As Object, e As EventArgs) Handles cbgDept._MyCheckChanged
        Try
            '    If IsCustLoad = False Then
            If cbgDept.CheckedValue IsNot Nothing AndAlso cbgDept.CheckedValue.Count > 0 Then
                strQry = clsCommon.GetMulcallString(cbgDept.CheckedValue)

                strQry = " Select EMP_CODE AS [Code],Emp_Name As [Emp Name],TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE AS [Dept Code],TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME AS [Dept Name] From TSPL_EMPLOYEE_MASTER " & _
                         " LEFT OUTER JOIN TSPL_DEPARTMENT_MASTER ON TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE = TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE " & _
                         " Where TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE IN (" + strQry + ") "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    cbgEmp.DataSource = dt
                    cbgEmp.DisplayMember = "Emp Name"
                    cbgEmp.ValueMember = "Code"
                Else
                    cbgEmp.DataSource = Nothing
                End If
            Else
                cbgEmp.DataSource = Nothing
            End If
            'End If
        Catch ex As Exception
            cbgEmp.DataSource = Nothing
        End Try
    End Sub

    Private Sub ChkEmpAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        cbgEmp.Enabled = False
    End Sub

    Private Sub ChkEmpSelect_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        cbgEmp.Enabled = True
    End Sub
End Class
