'' Created By Anubhooti BM00000006148 
Imports common
Imports System.Data.SqlClient
Imports System
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class FrmHRTravelBookedForMaster
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmHRTravelBookedForMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub Reset()
        txtCode.Value = ""
        txtRemarks.Text = ""
        txtempcode.Value = ""
        lblempname.Text = ""
        ChkIsApplicable.Checked = False

        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        txtCode.MyReadOnly = False

        txtCode.Focus()
        txtCode.Select()
    End Sub
    Function AllowToSave() As Boolean
        Try
            btnSave.Focus()
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill Code", Me.Text)
                txtCode.Focus()
                txtCode.Select()
                Return False
            End If
            If clsCommon.myLen(txtempcode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select emp code", Me.Text)
                txtempcode.Focus()
                txtempcode.Select()
                Return False
            End If
            If clsCommon.myLen(txtRemarks.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill remarks", Me.Text)
                txtRemarks.Focus()
                txtRemarks.Select()
                Return False
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return True
    End Function
    Sub SaveData()
        Try
            Dim obj As New ClsHRTravelBookedForMaster()

            obj.Travel_Booked_Code = clsCommon.myCstr(txtCode.Value)
            obj.Remarks = clsCommon.myCstr(txtRemarks.Text).Replace("'", "`")
            obj.Emp_Code = clsCommon.myCstr(txtempcode.Value)
            If ChkIsApplicable.Checked = True Then
                obj.Is_Applicable = 1
            Else
                obj.Is_Applicable = 0
            End If
            If ClsHRTravelBookedForMaster.SaveData(obj, txtCode.Value) Then
                clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                LoadData(obj.Travel_Booked_Code, NavigatorType.Current)
                txtCode.MyReadOnly = True
            Else
                txtCode.MyReadOnly = False
                btnSave.Text = "Save"
                btnDelete.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As New ClsHRTravelBookedForMaster()
            txtCode.Value = strCode
            obj = ClsHRTravelBookedForMaster.GetData(strCode, NavTyep)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Travel_Booked_Code) > 0 Then
                txtCode.Value = clsCommon.myCstr(obj.Travel_Booked_Code)
                txtRemarks.Text = clsCommon.myCstr(obj.Remarks)
                txtempcode.Value = clsCommon.myCstr(obj.Emp_Code)
                If clsCommon.myLen(clsCommon.myCstr(obj.Emp_Code)) > 0 Then
                    lblempname.Text = clsDBFuncationality.getSingleValue("Select ISNULL(TSPL_EMPLOYEE_MASTER.Emp_Name,'') AS [Emp Name] From TSPL_EMPLOYEE_MASTER Where EMP_CODE ='" + clsCommon.myCstr(txtempcode.Value) + "'")
                Else
                    lblempname.Text = ""
                End If
                If clsCommon.myCdbl(obj.Is_Applicable) = 1 Then
                    ChkIsApplicable.Checked = True
                Else
                    ChkIsApplicable.Checked = False
                End If

                txtCode.MyReadOnly = True
                btnSave.Text = "Update"
                btnDelete.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Code not found to delete")
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsHRTravelBookedForMaster.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully.")
                    Reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
#End Region
#Region "Events"
    Private Sub txtempcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtempcode._MYValidating
        txtempcode.Value = clsEmployeeMaster.getFinder("", txtempcode.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(txtempcode.Value) > 0 Then
            lblempname.Text = clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where Emp_code='" + txtempcode.Value + "'")
        Else
            lblempname.Text = ""
        End If
    End Sub
    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
        GC.Collect()
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Reset()
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If AllowToSave() Then
            SaveData()
        End If
    End Sub
    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Dim str As String
        str = "select TSPL_HR_TRAVEL_BOOKED_MASTER.Travel_Booked_Code AS Code, TSPL_HR_TRAVEL_BOOKED_MASTER.Emp_Code as [Emp Code],ISNULL(TSPL_EMPLOYEE_MASTER.Emp_Name,'') AS [Emp Name],TSPL_HR_TRAVEL_BOOKED_MASTER.Remarks as [Remarks],TSPL_HR_TRAVEL_BOOKED_MASTER.Is_Applicable AS [Is Applicable] FROM TSPL_HR_TRAVEL_BOOKED_MASTER LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_HR_TRAVEL_BOOKED_MASTER.EMP_CODE "
        transportSql.ExporttoExcel(str, Me)
    End Sub
    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim linno As Integer = 0

        If transportSql.importExcel(gv, "Code", "Emp Code", "Emp Name", "Remarks", "Is Applicable") Then

            Try
                clsCommon.ProgressBarPercentShow()

                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New ClsHRTravelBookedForMaster()
                    linno += 1
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value.ToString().Replace("'", ""))
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Code can not be blank or incorrect at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Travel_Booked_Code = strCode

                    Dim strEmpCode As String = clsCommon.myCstr(grow.Cells("Emp Code").Value.ToString().Replace("'", ""))
                    If (String.IsNullOrEmpty(strEmpCode)) Then
                        Throw New Exception("Emp Code can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    Dim sQuery As String = "select * from  TSPL_EMPLOYEE_MASTER where EMP_CODE='" + strEmpCode + "'"
                    Dim DTEmp As DataTable = clsDBFuncationality.GetDataTable(sQuery)
                    If DTEmp.Rows.Count <= 0 Then
                        Throw New Exception("Please check emp code '" + strEmpCode + "' .It dose not exits in employee master.")
                    End If
                    obj.Emp_Code = strEmpCode

                    Dim strRemarks As String = clsCommon.myCstr(grow.Cells("Remarks").Value.ToString().Replace("'", ""))
                    If (String.IsNullOrEmpty(strRemarks)) Then
                        Throw New Exception("Remarks can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf strRemarks.Length > 150 Then
                        Throw New Exception("Please check ! Remarks lenght should be 150 at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Remarks = strRemarks

                    Dim strIsApplicable As String = clsCommon.myCstr(grow.Cells("Is Applicable").Value)
                    If clsCommon.myLen(strIsApplicable) > 0 Then
                        If (clsCommon.CompairString(strIsApplicable, "1") <> CompairStringResult.Equal) AndAlso (clsCommon.CompairString(strIsApplicable, "0") <> CompairStringResult.Equal) Then
                            Throw New Exception("Is Applicable should be '1','0' at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    Else
                        Throw New Exception("Is Applicable can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Is_Applicable = strIsApplicable.ToUpper()

                    ClsHRTravelBookedForMaster.SaveData(obj, obj.Travel_Booked_Code)
                Next
                clsCommon.ProgressBarPercentHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarPercentHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
    Private Sub FrmHRTravelBookedForMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            If AllowToSave() Then
                SaveData()
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso BtnClose.Enabled Then
            Me.Close()
            GC.Collect()
        End If
    End Sub
    Private Sub FrmHRTravelBookedForMaster_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(BtnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_HR_TRAVEL_BOOKED_MASTER where Travel_Booked_Code='" + txtCode.Value + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtCode.MyReadOnly = True
            ElseIf check <= 0 Then
                txtCode.MyReadOnly = False
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_HR_TRAVEL_BOOKED_MASTER where Travel_Booked_Code ='" + txtCode.Value + "' "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            If txtCode.MyReadOnly OrElse isButtonClicked Then

                txtCode.Value = ClsHRTravelBookedForMaster.GetFinder("", txtCode.Value, isButtonClicked)
                If clsCommon.myLen(txtCode.Value) > 0 Then
                    btnDelete.Enabled = True
                    btnSave.Text = "Update"
                    txtCode.MyReadOnly = True
                Else
                    btnSave.Text = "Save"
                    btnDelete.Enabled = False
                    txtCode.MyReadOnly = False
                End If
            End If
            LoadData(txtCode.Value, NavigatorType.Current)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub txtempcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtempcode.KeyPress
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub
#End Region
End Class
