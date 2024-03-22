Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmGrievanceLogging

    Dim isnewentry As Boolean
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim sQuery As String = String.Empty
    Dim dr As DataRow

    Function AllowTosave() As Boolean
        'If clsCommon.myLen(txt_Code.Value) <= 0 Then
        '    myMessages.blankValue("Code")

        '    txt_Code.Focus()
        '    txt_Code.Select()
        '    Errorcontrol.SetError(txt_Code, "Code")
        '    Return False
        'Else
        '    Errorcontrol.ResetError(txt_Code)
        'End If

        If clsCommon.myLen(txt_Name.Text) <= 0 Then
            myMessages.blankValue(Me, "Description", Me.Text)

            txt_Name.Focus()
            txt_Name.Select()
            Errorcontrol.SetError(txt_Name, "Description")
            Return False
        Else
            Errorcontrol.ResetError(txt_Name)
        End If

        If clsCommon.myLen(TxtGrievancceType.Value) <= 0 Then
            myMessages.blankValue(Me, "Grievance Type", Me.Text)

            TxtGrievancceType.Focus()
            TxtGrievancceType.Select()
            Errorcontrol.SetError(TxtGrievancceType, "Grievance Type")
            Return False
        Else
            Errorcontrol.ResetError(TxtGrievancceType)
        End If


        If clsCommon.myLen(TxtApplied_By.Value) <= 0 Then
            myMessages.blankValue(Me, "Applied By", Me.Text)

            TxtApplied_By.Focus()
            TxtApplied_By.Select()
            Errorcontrol.SetError(TxtApplied_By, "Applied By")
            Return False
        Else
            Errorcontrol.ResetError(TxtApplied_By)
        End If


        If clsCommon.myLen(TxtFrmDepartment.Value) <= 0 Then
            myMessages.blankValue(Me, "From Department", Me.Text)

            TxtFrmDepartment.Focus()
            TxtFrmDepartment.Select()
            Errorcontrol.SetError(TxtFrmDepartment, "From Department")
            Return False
        Else
            Errorcontrol.ResetError(TxtFrmDepartment)
        End If

        If clsCommon.myLen(TxtToDepartment.Value) <= 0 Then
            myMessages.blankValue(Me, "For Department", Me.Text)

            TxtToDepartment.Focus()
            TxtToDepartment.Select()
            Errorcontrol.SetError(TxtToDepartment, "For Department")
            Return False
        Else
            Errorcontrol.ResetError(TxtToDepartment)
        End If

        Return True
    End Function
    Sub SaveData()
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowTosave() Then
                Dim obj As New clsGrievanceLogging

                obj.Code = clsCommon.myCstr(txt_Code.Value)
                obj.Description = clsCommon.myCstr(txt_Name.Text)
                obj.Grievance_Type = clsCommon.myCstr(TxtGrievancceType.Value)
                obj.Doc_date = clsCommon.myCDate(txtDate.Value)
                obj.Applied_By = clsCommon.myCstr(TxtApplied_By.Value)
                obj.Txt_Frm_Department = clsCommon.myCstr(TxtFrmDepartment.Value)
                obj.Txt_For_Department = clsCommon.myCstr(TxtToDepartment.Value)
                obj.Remarks = TxtRemark.Text

                If (clsGrievanceLogging.SaveData(obj, isnewentry)) Then

                    clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If




        Catch ex As Exception
            'trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            If clsCommon.MyMessageBoxShow(Me, "Are you sure? Do you want to Delete this Code ('" + txt_Code.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim obj As New clsGrievanceLogging
                If obj.DeleteData(txt_Code.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Deleted Successfully", Me.Text)
                    ResetData()
                End If
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow(Me, "Current Code is in use", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try


    End Sub
    Sub ResetData()
        txt_Code.Value = ""
        txt_Name.Text = ""

        sQuery = "select tspl_employee_master.Emp_Code as Code,tspl_employee_master.Emp_Name as Name from tspl_employee_master inner join tspl_user_master on " _
            & " tspl_user_master.emp_code=tspl_employee_master.emp_code where tspl_user_master.User_code='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        If dt.Rows.Count > 0 Then
            TxtApplied_By.Value = clsCommon.myCstr(dt.Rows(0).Item("Code"))
            lblAppliedBy.Text = clsCommon.myCstr(dt.Rows(0).Item("Name"))
        End If
        TxtFrmDepartment.Value = Nothing
        TxtToDepartment.Value = Nothing
        LblFrmDepartment.Text = Nothing
        LblToDepartment.Text = Nothing
        LblGrievanceType.Text = Nothing
        TxtGrievancceType.Value = Nothing
        Btnsave.Text = "Save"
        TxtRemark.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()

        BtnPost.Enabled = True
        Btnsave.Enabled = True
        BtnDelete.Enabled = True
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
        Dim cls As clsGrievanceLogging = clsGrievanceLogging.GetData(obj, NavigatorType)

        If cls IsNot Nothing Then
            txt_Code.Value = cls.Code
            txt_Name.Text = cls.Description
            If clsCommon.myLen(cls.Doc_date) > 0 Then
                txtDate.Value = cls.Doc_date
            Else
                txtDate.Value = clsCommon.GETSERVERDATE()
            End If
            TxtGrievancceType.Value = cls.Grievance_Type
            LblGrievanceType.Text = cls.Grievance_Name
            TxtApplied_By.Value = cls.Applied_By
            lblAppliedBy.Text = cls.Applied_By_Name
            TxtFrmDepartment.Value = cls.Txt_Frm_Department
            LblFrmDepartment.Text = cls.Txt_Frm_Department_Name

            TxtToDepartment.Value = cls.Txt_For_Department
            LblToDepartment.Text = cls.Txt_For_Department_Name
            TxtRemark.Text = clsCommon.myCstr(cls.Remarks)

            Btnsave.Text = "Update"
            isnewentry = False
            If cls.POSTED = ERPTransactionStatus.Approved Then
                Btnsave.Enabled = False
                BtnPost.Enabled = False
                BtnDelete.Enabled = False
            End If

        End If


    End Sub

    Private Sub txt_Code__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txt_Code._MYNavigator
        LoadData(txt_Code.Value, NavType)
        Btnsave.Text = "Update"
    End Sub

    Private Sub txt_Code__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txt_Code._MYValidating
        Dim qry As String
        If isButtonClicked Then
            qry = "select Code,Description,Doc_date as [Date] from tspl_Grievance_logging_detail"
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
        'Dim trans As SqlTransaction
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Code", "Name") Then
            Dim linno As Integer = 1
            Try
                'trans = clsDBFuncationality.GetTransactin()
                'connectSql.OpenConnection()
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New clsGrievanceLogging()
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    If clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception("Code should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If

                    Dim strName As String = clsCommon.myCstr(grow.Cells("Name").Value)
                    If clsCommon.myLen(strName) <= 0 Then
                        Throw New Exception("Name should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Code = strCode
                    'obj.Name = strName
                    'obj.tb_Name = tb_name
                    clsGrievanceLogging.SaveData(obj, IsNewEntry)
                    linno += 1
                Next
                'trans.Commit()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                'trans.Rollback()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RMExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMExport.Click
        Dim str As String
        str = "select Code as [Code],Name As [Name]  from tspl_Grievance_logging_detail "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub frmGrievanceLogging_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub frmGrievanceLogging_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ';If MyBase.Text = "Training Master" Then
            ' tb_name = "Tspl_Grievance_Type_Master"
            'End If
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
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSourceTypeMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        Btnsave.Visible = MyBase.isModifyFlag
        BtnDelete.Visible = MyBase.isDeleteFlag
    End Sub


    Private Sub TxtFrmDepartment__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtFrmDepartment._MYValidating
        Try
            sQuery = "select department_code as Code,Department_name as Name from tspl_department_Master"
            dr = clsCommon.ShowSelectFormForRow("Frm_DDP", sQuery, "Code", clsCommon.myCstr(TxtFrmDepartment.Value))
            If Not IsNothing(dr) Then
                TxtFrmDepartment.Value = clsCommon.myCstr(dr("Code"))
                LblFrmDepartment.Text = clsCommon.myCstr(dr("Name"))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub

    Private Sub TxtToDepartment__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtToDepartment._MYValidating
        Try
            sQuery = "select department_code as Code,Department_name as Name from tspl_department_Master"
            dr = clsCommon.ShowSelectFormForRow("Frm_DDP", sQuery, "Code", clsCommon.myCstr(TxtToDepartment.Value))
            If Not IsNothing(dr) Then
                TxtToDepartment.Value = clsCommon.myCstr(dr("Code"))
                LblToDepartment.Text = clsCommon.myCstr(dr("Name"))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub

    Private Sub TxtApplied_By__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtApplied_By._MYValidating
        Try
            sQuery = "select Emp_Code as Code,Emp_Name as Name from tspl_employee_master "
            dr = clsCommon.ShowSelectFormForRow("Frm_DDP", sQuery, "Code", clsCommon.myCstr(TxtFrmDepartment.Value))
            If Not IsNothing(dr) Then
                TxtApplied_By.Value = clsCommon.myCstr(dr("Code"))
                lblAppliedBy.Text = clsCommon.myCstr(dr("Name"))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub

    Private Sub TxtGrievancceType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtGrievancceType._MYValidating
        Try
            sQuery = "select Code,Name from tspl_Grievance_type_master where posted='1'"
            dr = clsCommon.ShowSelectFormForRow("Frm_DDP", sQuery, "Code", clsCommon.myCstr(TxtFrmDepartment.Value))
            If Not IsNothing(dr) Then
                TxtGrievancceType.Value = clsCommon.myCstr(dr("Code"))
                LblGrievanceType.Text = clsCommon.myCstr(dr("Name"))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub

    Private Sub BtnPost_Click(sender As Object, e As EventArgs) Handles BtnPost.Click
        Try
            Dim str As String = "update tspl_Grievance_Logging_Detail set posted='1' where code='" & clsCommon.myCstr(txt_Code.Value) & "'"
            clsDBFuncationality.ExecuteNonQuery(str)
            clsCommon.MyMessageBoxShow(Me, "Code Posted Successfully...", Me.Text)
            BtnPost.Enabled = False
            Btnsave.Enabled = False
            BtnDelete.Enabled = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub
End Class
