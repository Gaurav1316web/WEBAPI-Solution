Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
Imports XpertERPEngine
Imports System.IO
'===================Created by shivani Tyagi[BM00000006310]
Public Class FrmDamageCaused
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isnewentry As Boolean
    Dim isAllowValueChanged As Boolean = False
    Dim isFlag As Boolean = False
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmDamageCaused)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag

    End Sub
    Function allowtosave()
        If clsCommon.myLen(clsCommon.myCstr(txtDescription.Text)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtDescription.Text)) > 150 Then
            myMessages.blankValue("Description ")
            txtDescription.Focus()
            txtDescription.Select()
            Errorcontrol.SetError(txtDescription, "Description ")
            Return False
        Else
            Errorcontrol.ResetError(txtDescription)
        End If

        If clsCommon.myLen(clsCommon.myCstr(fndEmployeeCode.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(fndEmployeeCode.Value)) > 12 Then
            myMessages.blankValue("Employee Code ")
            txtDescription.Focus()
            txtDescription.Select()
            Errorcontrol.SetError(fndEmployeeCode, "Employee Code ")
            Return False
        Else
            Errorcontrol.ResetError(fndEmployeeCode)
        End If
        If clsCommon.myLen(clsCommon.myCstr(txtDamageCode.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtDamageCode.Value)) > 30 Then
            myMessages.blankValue("Damage Caused ")
            txtDescription.Focus()
            txtDescription.Select()
            Errorcontrol.SetError(txtDamageCode, "Damage Caused ")
            Return False
        Else
            Errorcontrol.ResetError(txtDamageCode)
        End If
        Return True
    End Function

    Public Sub SaveData()
        Try

            Dim entry As String
            Dim count As Integer = 0
            Dim i As Integer = 0
            Dim qry As String = "select count(*) from TSPL_HR_DAMAGE_DETAIL  where Damage_Detail_Code ='" + txtCode.Value + "'"
            count = clsDBFuncationality.getSingleValue(qry)
            If count = 0 Then
                isnewentry = True
            Else
                isnewentry = False

            End If
            Dim obj As New ClsHRDamageDetail()

            obj.Damage_Detail_Code = clsCommon.myCstr(txtCode.Value)
            obj.Damage_Detail_Date = clsCommon.myCDate(txtDate.Text)
            obj.Damage_Code = clsCommon.myCstr(txtDamageCode.Value)
            obj.Emp_Code = clsCommon.myCstr(fndEmployeeCode.Value)
            obj.Amt_Realised_Date = clsCommon.myCDate(txtAmtDate.Text)
            obj.Damage_Detail_Description = clsCommon.myCstr(txtDescription.Text)
            obj.Deduction_Imposed = clsCommon.myCstr(txtDedImposed.Text)
            obj.No_Of_Installment = clsCommon.myCstr(txtNoOfInstallment.Text)
            'obj.PAY_PERIOD_CODE = clsCommon.myCstr(txtPayPeriod.Value)
            If ClsHRDamageDetail.SaveData(obj, isnewentry) Then
                If Not isFlag Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    entry = obj.Damage_Detail_Code
                    LoadData(obj.Damage_Detail_Code, NavigatorType.Current)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                End If
            End If


        Catch ex As Exception
            RadMessageBox.Show(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub AddNew()
        txtCode.Value = ""
        txtDate.Text = clsCommon.GETSERVERDATE()
        fndEmployeeCode.Value = ""
        lblEmployeeName.Text = ""
        txtDamageCode.Value = ""
        lblDamageName.Text = ""
        txtAmtDate.Text = clsCommon.GETSERVERDATE()
        txtDescription.Text = ""
        lblFatherName.Text = ""
        lblSexN.Text = ""
        lblDepartmentCode.Text = ""
        lblDepartmentName.Text = ""
        'lblDeductionImposed.Text = ""
        'lblNoOfInstallment.Text = ""
        txtDedImposed.Text = ""
        txtNoOfInstallment.Text = ""
        txtPayPeriod.Value = ""
        lblPayPeriod.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending

        btnsave.Enabled = True
        btnsave.Text = "Save"

    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsHRDamageDetail = ClsHRDamageDetail.GetData(strCode, Nothing, NavTyep)

        If obj IsNot Nothing Then
            AddNew()
            isnewentry = False
            txtCode.Value = obj.Damage_Detail_Code
            txtDate.Text = obj.Damage_Detail_Date
            fndEmployeeCode.Value = obj.Emp_Code
            txtDamageCode.Value = obj.Damage_Code
            txtAmtDate.Text = obj.Amt_Realised_Date
            txtDescription.Text = obj.Damage_Detail_Description
            txtDedImposed.Text = obj.Deduction_Imposed
            txtNoOfInstallment.Text = obj.No_Of_Installment
            lblDamageName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Damage_Type  from TSPL_HR_DAMAGE_MASTER where Damage_Code ='" & txtDamageCode.Value & "'"))
            lblEmployeeName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Emp_Name  from tspl_employee_master where Emp_Code='" & fndEmployeeCode.Value & "'"))
            lblDepartmentCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  DEPARTMENT_CODE as [Department Code] from tspl_employee_master where Emp_Code='" & fndEmployeeCode.Value & "'"))
            lblDepartmentName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Department_Name from tspl_employee_master left join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER.Department_Code=tspl_employee_master.Department_code where tspl_employee_master.Department_code='" & lblDepartmentCode.Text & "'"))
            lblFatherName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  FATHERS_NAME  from tspl_employee_master where Emp_Code='" & fndEmployeeCode.Value & "'"))
            lblSexN.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  SEX  from tspl_employee_master where Emp_Code='" & fndEmployeeCode.Value & "'"))
            'txtPayPeriod.Value = obj.PAY_PERIOD_CODE
            'lblPayPeriod.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select PAY_PERIOD_NAME from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + txtPayPeriod.Value + "' "))
            txtCode.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True

            btnPost.Enabled = True

            If obj.POSTED = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                btnPost.Enabled = False
                btnPrint.Enabled = True
                btndelete.Enabled = False
            End If
            UsLock1.Status = obj.POSTED
        End If

    End Sub
    Sub PostData()
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing

            If (myMessages.postConfirm()) Then
                'SaveData()
                If (ClsHRDamageDetail.PostData(txtCode.Value, True)) Then
                    msg = "Successfully Posted"
                Else
                    qry = "select No_Of_Level, LEVEL from TSPL_APPROVAL_LEVEL_SCREEN where User_Code='" + objCommonVar.CurrentUserCode + "' and Trans_Code='" + MyBase.Form_ID + "' "
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim level As String = dt.Rows(0)("LEVEL").ToString()
                        Dim NoOflevel As Integer = clsCommon.myCdbl(dt.Rows(0)("No_Of_Level"))
                        If clsCommon.CompairString(level, "Level1") = CompairStringResult.Equal Then
                            msg = "Level 1 Approval done. "
                            If NoOflevel = 1 Then
                                msg += "Successfully Posted. "
                            Else
                                msg += "Level 2 Approval Required."
                            End If
                        ElseIf clsCommon.CompairString(level, "Level2") = CompairStringResult.Equal Then
                            msg = "Level 2 Approval done. "
                            If NoOflevel = 2 Then
                                msg += "Successfully Posted "
                            Else
                                msg += "Level 3 Approval Required."
                            End If
                        Else
                            msg = "Level 3 Approval done. Successfully Posted. "
                        End If
                    End If
                End If
                common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                LoadData(txtCode.Value, NavigatorType.Current)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        'Try
        '    Dim qry As String = "select DAMAGE_DETAIL_CODE,*  from TSPL_HR_DAMAGE_DETAIL"
        '    txtCode.Value = clsCommon.ShowSelectForm("DAMAGE", qry, "Document_Code", "", txtCode.Value, "", isButtonClicked)

        '    If clsCommon.myLen(txtCode.Value) > 0 Then
        '        LoadData(txtCode.Value, NavigatorType.Current)

        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.ToString)
        'End Try
        Dim str As String = "select count(*) from TSPL_HR_DAMAGE_DETAIL where DAMAGE_DETAIL_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = ClsHRDamageDetail.getFinder("Comp_Code='" & objCommonVar.CurrentCompanyCode & "'", txtCode.ValidateChildren, isButtonClicked) 'clsCommon.ShowSelectForm("OT_SHEET", qry, "Code", "", txtCode.Value, "OD_SHEET_CODE", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                AddNew()
            End If
        End If
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndEmployeeCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndEmployeeCode._MYValidating
        Dim qry As String = " Select Emp_Code AS Code ,Emp_Name  From TSPL_EMPLOYEE_MASTER "
        fndEmployeeCode.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("EMP", qry, "Code", "LEAVING_REASON = ''", fndEmployeeCode.Value, "Code", isButtonClicked))

        If clsCommon.myLen(clsCommon.myCstr(fndEmployeeCode.Value)) > 0 Then
            lblEmployeeName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where Emp_Code='" & fndEmployeeCode.Value & "'"))

            lblDepartmentCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  DEPARTMENT_CODE as [Department Code] from tspl_employee_master where Emp_Code='" & fndEmployeeCode.Value & "'"))
            lblDepartmentName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Department_Name from tspl_employee_master left join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER.Department_Code = tspl_employee_master.Department_Code where Emp_Code='" & fndEmployeeCode.Value & "'"))
            lblFatherName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Fathers_Name from tspl_employee_master  where Emp_Code='" & fndEmployeeCode.Value & "'"))
            lblSexN.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select sex from tspl_employee_master  where Emp_Code='" & fndEmployeeCode.Value & "'"))
            
        Else
            lblEmployeeName.Text = ""
        End If

    End Sub

    Private Sub txtDamageCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDamageCode._MYValidating
        Dim qry As String = " select Damage_Code as Code,Damage_Type from TSPL_HR_DAMAGE_MASTER "
        txtDamageCode.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("EMP", qry, "Code", "", txtDamageCode.Value, "Code", isButtonClicked))

        If clsCommon.myLen(clsCommon.myCstr(txtDamageCode.Value)) > 0 Then
            lblDamageName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Damage_Type from TSPL_HR_DAMAGE_MASTER where Damage_Code='" & txtDamageCode.Value & "'"))

        Else
            lblDamageName.Text = ""
        End If
    End Sub

    Private Sub FrmDamageCaused_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")

        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post")
        AddNew()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub FrmDamageCaused_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()


        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtPayPeriod__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPayPeriod._MYValidating
        'Dim qry As String = "SELECT PAY_PERIOD_CODE AS Code,(DATEDIFF(DAY,date_from,date_to)+1) as Totaldays, " _
        '& " PAY_PERIOD_NAME as Name FROM TSPL_PAYPERIOD_MASTER"
        ''Dim qry As String = "select PAY_PERIOD_CODE as Code , PAY_PERIOD_NAME as Name, DATE_FROM as 'From Date', DATE_TO AS 'To Date', DESCRIPTION as Description  from TSPL_PAYPERIOD_MASTER"
        'txtPayPeriod.Value = clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "Code", "POSTED=1 and FREEZED=0", txtPayPeriod.Value, "PAY_PERIOD_CODE", isButtonClicked)
        'lblPayPeriod.Text = clsPayPeriodMaster.GetName(txtPayPeriod.Value, Nothing)
    End Sub
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("are you sure? do you want to delete this Code ('" + txtcode.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "DELETE FROM TSPL_HR_DAMAGE_DETAIL WHERE Damage_Detail_Code='" + txtCode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow(Me, "Deleted Successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow(Me, "Current Code is in use", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub
    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub
End Class