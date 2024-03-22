Imports System.Collections.Generic
Imports XpertERPEngine
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
Imports System.IO
'========shivani
Public Class FrmEmployeeTransfer

    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isnewentry As Boolean
    Dim isAllowValueChanged As Boolean = False
    Dim isFlag As Boolean = False
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Public Shared save_structure_code As String = String.Empty

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmEmployeeTransfer)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Function allowtosave()
        If clsCommon.myLen(clsCommon.myCstr(txtDescription.Text)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtDescription.Text)) > 200 Then
            myMessages.blankValue(Me, "Description ", Me.Text)
            txtDescription.Focus()
            txtDescription.Select()
            Errorcontrol.SetError(txtDescription, "Description ")
            Return False
        Else
            Errorcontrol.ResetError(txtDescription)
        End If

        If clsCommon.myLen(clsCommon.myCstr(fndEmployeeCode.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(fndEmployeeCode.Value)) > 12 Then
            myMessages.blankValue(Me, "Employee Code ", Me.Text)
            txtDescription.Focus()
            txtDescription.Select()
            Errorcontrol.SetError(txtDescription, "Employee Code ")
            Return False
        Else
            Errorcontrol.ResetError(txtDescription)
        End If
        Return Nothing
    End Function



    Public Sub SaveData()
        Try

            Dim entry As String
            Dim count As Integer = 0
            Dim i As Integer = 0
            Dim qry As String = "select count(*) from TSPL_EMPLOYEE_TRANSFER  where Document_Code ='" + txtCode.Value + "'"
            count = clsDBFuncationality.getSingleValue(qry)
            If count = 0 Then
                isnewentry = True
            Else
                isnewentry = False

            End If
            Dim obj As New ClsEmployeeTransfer()
            If cboDocType.Text = "Transfer Letter(For Department)" Then
                obj.Document_Code = clsCommon.myCstr(txtCode.Value)
                obj.Document_Date = clsCommon.myCstr(txtDate.Text)
                obj.Effective_Date = clsCommon.myCstr(txtEffDate.Text)
                obj.Document_Type = clsCommon.myCstr(cboDocType.Text)
                obj.Salary_Affected = clsCommon.myCstr(cboSalary.Text)
                If cboSalary.Text = "Yes" Then
                    obj.Salary_Code = clsCommon.myCstr(lblSalaryCode.Text)
                End If
                obj.Previous_Salary_Code = clsCommon.myCstr(lblPreviousSalary.Text)
                obj.Description = clsCommon.myCstr(txtDescription.Text)
                obj.Emp_Code = clsCommon.myCstr(fndEmployeeCode.Value)
                obj.Transfer_Department = clsCommon.myCstr(fndChangedDepartment.Value)
                obj.Current_Department = clsCommon.myCstr(lblDepartment.Text)
                obj.Current_Designation = clsCommon.myCstr(lblDesignation.Text)
                obj.Current_Location = clsCommon.myCstr(lblLocation.Text)
                obj.Current_Division = clsCommon.myCstr(lblCurrentDivisionCode.Text)
            ElseIf cboDocType.Text = "Promotion Letter" Then
                obj.Document_Code = clsCommon.myCstr(txtCode.Value)
                obj.Document_Date = clsCommon.myCstr(txtDate.Text)
                obj.Effective_Date = clsCommon.myCstr(txtEffDate.Text)
                obj.Document_Type = clsCommon.myCstr(cboDocType.Text)
                obj.Salary_Affected = clsCommon.myCstr(cboSalary.Text)
                If cboSalary.Text = "Yes" Then
                    obj.Salary_Code = clsCommon.myCstr(lblSalaryCode.Text)
                End If
                obj.Previous_Salary_Code = clsCommon.myCstr(lblPreviousSalary.Text)
                obj.Description = clsCommon.myCstr(txtDescription.Text)
                obj.Current_Department = clsCommon.myCstr(lblDepartment.Text)
                obj.Current_Designation = clsCommon.myCstr(lblDesignation.Text)
                obj.Current_Location = clsCommon.myCstr(lblLocation.Text)
                obj.Current_Division = clsCommon.myCstr(lblCurrentDivisionCode.Text)
                obj.Emp_Code = clsCommon.myCstr(fndEmployeeCode.Value)
                obj.Transfer_Department = clsCommon.myCstr(fndChangedDepartment.Value)
                obj.Transfer_Designation = clsCommon.myCstr(fndChangedDesignation.Value)
                'obj.Transfer_Location = clsCommon.myCstr(fndChangedLocation.Value)
            Else
                obj.Document_Code = clsCommon.myCstr(txtCode.Value)
                obj.Document_Date = clsCommon.myCstr(txtDate.Text)
                obj.Effective_Date = clsCommon.myCstr(txtEffDate.Text)
                obj.Document_Type = clsCommon.myCstr(cboDocType.Text)
                obj.Salary_Affected = clsCommon.myCstr(cboSalary.Text)
                If cboSalary.Text = "Yes" Then
                    obj.Salary_Code = clsCommon.myCstr(lblSalaryCode.Text)
                End If
                obj.Previous_Salary_Code = clsCommon.myCstr(lblPreviousSalary.Text)
                obj.Description = clsCommon.myCstr(txtDescription.Text)
                obj.Current_Department = clsCommon.myCstr(lblDepartment.Text)
                obj.Current_Designation = clsCommon.myCstr(lblDesignation.Text)
                obj.Current_Location = clsCommon.myCstr(lblLocation.Text)
                obj.Current_Division = clsCommon.myCstr(lblCurrentDivisionCode.Text)
                obj.Emp_Code = clsCommon.myCstr(fndEmployeeCode.Value)
                obj.Transfer_Department = clsCommon.myCstr(fndChangedDepartment.Value)
                obj.Transfer_Designation = clsCommon.myCstr(fndChangedDesignation.Value)
                obj.Transfer_Location = clsCommon.myCstr(fndChangedLocation.Value)
                obj.Transfer_Division = clsCommon.myCstr(fndChangedDivision.Value)
            End If

            If ClsEmployeeTransfer.SaveData(obj, isnewentry) Then
                If Not isFlag Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    entry = obj.Document_Code
                    LoadData(obj.Document_Code, NavigatorType.Current)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                End If
            End If


        Catch ex As Exception
            RadMessageBox.Show(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()

    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsEmployeeTransfer = ClsEmployeeTransfer.GetData(strCode, Nothing, NavTyep)

        If obj IsNot Nothing Then
            AddNew()
            isnewentry = False
            txtCode.Value = obj.Document_Code
            txtDate.Text = obj.Document_Date
            fndEmployeeCode.Value = obj.Emp_Code
            txtEffDate.Text = obj.Effective_Date
            lblEmployeeName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Emp_Name  from tspl_employee_master where Emp_Code='" & fndEmployeeCode.Value & "'"))
            lblDepartment.Text = obj.Current_Department
            lblDepartmentName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Department_Name from TSPL_EMPLOYEE_TRANSFER left join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER.Department_Code=TSPL_EMPLOYEE_TRANSFER.Current_Department where Current_Department='" & lblDepartment.Text & "'"))
            lblDesignation.Text = obj.Current_Designation
            lblDesignationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Designation_Desc from TSPL_EMPLOYEE_TRANSFER left join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_Id=TSPL_EMPLOYEE_TRANSFER.Current_Designation where Current_Designation='" & lblDesignation.Text & "'"))
            lblLocation.Text = obj.Current_Location
            lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_EMPLOYEE_TRANSFER left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_EMPLOYEE_TRANSFER.Current_Location where Current_Location='" & lblLocation.Text & "'"))
            lblCurrentDivisionCode.Text = obj.Current_Division
            lblCurrentDivisionName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DEVISION_NAME  from TSPL_DEVISION_MASTER where DEVISION_CODE ='" & lblCurrentDivisionCode.Text & "'"))
            fndChangedDepartment.Value = obj.Transfer_Department
            fndChangedDesignation.Value = obj.Transfer_Designation
            fndChangedLocation.Value = obj.Transfer_Location
            fndChangedDivision.Value = obj.Transfer_Division
            lblChangedDepartmentName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Department_Name from TSPL_EMPLOYEE_TRANSFER left join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER.Department_Code =TSPL_EMPLOYEE_TRANSFER.Transfer_Department where transfer_Department='" & fndChangedDepartment.Value & "'"))
            lblChangedDesignationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Designation_Desc from TSPL_EMPLOYEE_TRANSFER left join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_Id=TSPL_EMPLOYEE_TRANSFER.Transfer_Designation where Transfer_Designation='" & fndChangedDesignation.Value & "'"))
            lblChangedLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_EMPLOYEE_TRANSFER left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_EMPLOYEE_TRANSFER.Transfer_Location where Transfer_Location='" & fndChangedLocation.Value & "'"))
            lblChangedDivisionName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DEVISION_NAME  from TSPL_DEVISION_MASTER where DEVISION_CODE ='" & fndChangedDivision.Value & "'"))
            cboDocType.Text = obj.Document_Type
            isAllowValueChanged = True
            cboSalary.Text = obj.Salary_Affected
            If cboSalary.Text = "Yes" Then
                lblSalaryCode.Text = obj.Salary_Code

            End If
            isAllowValueChanged = False
            lblPreviousSalary.Text = obj.Previous_Salary_Code
            txtDescription.Text = obj.Description
            txtCode.MyReadOnly = True
            btnsave.Text = "Update"


            btnPost.Enabled = True

            If obj.POSTED = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                btnPost.Enabled = False
                btnPrint.Enabled = True
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
                If (ClsEmployeeTransfer.PostData(txtCode.Value, True)) Then
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



    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
        'If UsLock1.Status = ERPTransactionStatus.Approved Then
        '    If cboDocType.Text = "Transfer Letter (For Location)" Then

        '        Dim Qry As String = " Update TSPL_EMPLOYEE_MASTER set Location_Code='" & fndChangedLocation.Value & "',DEVISION_CODE = '" & fndChangedDivision.Value & "' , Designation='" & fndChangedDesignation.Value & "' , DEPARTMENT_CODE='" & fndChangedDepartment.Value & "' where Emp_Code = '" & fndEmployeeCode.Value & "'"
        '        clsDBFuncationality.ExecuteNonQuery(Qry)
        '    End If
        '    If cboDocType.Text = "Promotion Letter" Then
        '        Dim Qry1 As String = " Update TSPL_EMPLOYEE_MASTER set Designation='" & fndChangedDesignation.Value & "' , DEPARTMENT_CODE='" & fndChangedDepartment.Value & "' where Emp_Code = '" & fndEmployeeCode.Value & "'"
        '        clsDBFuncationality.ExecuteNonQuery(Qry1)
        '    End If
        '    If cboDocType.Text = "Transfer Letter(For Department)" Then
        '        Dim Qry2 As String = " Update TSPL_EMPLOYEE_MASTER set  DEPARTMENT_CODE='" & fndChangedDepartment.Value & "' where Emp_Code = '" & fndEmployeeCode.Value & "'"

        '        clsDBFuncationality.ExecuteNonQuery(Qry2)
        '    End If
        'End If

    End Sub
    Sub PrintData()
        If clsCommon.CompairString(cboDocType.Text, "Transfer Letter(For Location )") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboDocType.Text, "Transfer Letter(For Location)") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboDocType.Text, "Transfer Letter (For Location)") = CompairStringResult.Equal Then
            Dim Qry As String
            Qry = "select TSPL_EMPLOYEE_TRANSFER.Emp_Code,Current_Designation,Designation_Desc,(TSPL_EMPLOYEE_MASTER.Add1 + TSPL_EMPLOYEE_MASTER.Add2 )as Address,Emp_Name,Transfer_Location,Current_Location,convert(varchar,Document_date,103)as Document_date,convert(varchar,Effective_Date,103)as Effective_Date,Trans_Loc.Location_Desc as TransferLocation, Curr_Loc.Location_Desc as CurrentLocation from TSPL_EMPLOYEE_TRANSFER left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.Emp_Code=TSPL_EMPLOYEE_TRANSFER.Emp_Code "
            Qry += "left join TSPL_LOCATION_MASTER Trans_Loc on TSPL_EMPLOYEE_TRANSFER.Transfer_Location=Trans_Loc.Location_Code "
            Qry += "left join TSPL_LOCATION_MASTER Curr_Loc on TSPL_EMPLOYEE_TRANSFER.Current_Location=Curr_Loc.Location_Code	"
            Qry += "left join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_id=TSPL_EMPLOYEE_TRANSFER.current_Designation where Document_Code='" & txtCode.Value & "'"
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(Qry)

            Dim frmcrsytal As New frmCrystalReportViewer
            frmcrsytal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptLocationtransferletter", "Location Letter")
        ElseIf clsCommon.CompairString(cboDocType.Text, "Promotion Letter") = CompairStringResult.Equal Then
            Dim Qry1 As String
            'Qry1 = "select TOP 1 Revision_No,tt.Emp_Code,Rate_Amount,Document_Code, Document_Date,Emp_Name,ttt.* from (select max(Revision_No)as Revision_No,max(TSPL_EMPLOYEE_TRANSFER.EMP_CODE)as EMP_CODE,sum(Rate_Amount)as Rate_Amount,max(TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE)as EMP_SAL_CODE,max(Document_Code)as Document_Code,max(Document_Date)as Document_Date,max(Emp_Name)as Emp_Name from TSPL_EMPLOYEE_SALARY left join TSPL_EMPLOYEE_SALARY_PAYHEADS on TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE=TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE left join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE=TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE  left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.Emp_Code=TSPL_EMPLOYEE_SALARY.Emp_Code left join TSPL_EMPLOYEE_TRANSFER on TSPL_EMPLOYEE_MASTER.Emp_Code=TSPL_EMPLOYEE_TRANSFER.Emp_Code and  TSPL_EMPLOYEE_TRANSFER.salary_code=TSPL_EMPLOYEE_SALARY.emp_sal_code where HEAD_TYPE <> 'F' and ISEARNING='1' and TSPL_EMPLOYEE_TRANSFER.Emp_Code='" & fndEmployeeCode.Value & "' group by TSPL_EMPLOYEE_TRANSFER.EMP_CODE,TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE,Document_Code    )as tt"
            'Qry1 += " left join ( select TOP 1 Revision_No as R_n,tt.Emp_Code as Em_C,Rate_Amount AS Previous_Amt from (select max(Revision_No)as Revision_No,max(EMP_CODE)as EMP_CODE,sum(Rate_Amount)as Rate_Amount,max(TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE)as EMP_SAL_CODE from TSPL_EMPLOYEE_SALARY inner join(select MAx(Revision_No) as R_N from TSPL_EMPLOYEE_SALARY where Revision_No not in (select max(Revision_No)from TSPL_EMPLOYEE_SALARY where  Emp_Code='" & fndEmployeeCode.Value & "') and  Emp_Code='" & fndEmployeeCode.Value & "') tt on tt.R_N=Revision_No left join TSPL_EMPLOYEE_SALARY_PAYHEADS on TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE=TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE left join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE=TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE where HEAD_TYPE <> 'F' and ISEARNING='1' and Emp_Code='" & fndEmployeeCode.Value & "' group by EMP_CODE,TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE    )as tt)"
            'Qry1 += " ttt on ttt.Em_C=tt.emp_code"
            Qry1 = "select Current_Amt,Previous_Amt,Emp_Code,Emp_Name,Document_Code,convert(varchar,Document_Date,103)as Document_Date,Designation_Desc from (select sum(Rate_Amount)as Previous_Amt ,TSPL_EMPLOYEE_SALARY.Emp_Code,TSPL_DESIGNATION_MASTER.Designation_Desc   from	TSPL_EMPLOYEE_SALARY "
            Qry1 += " left join TSPL_EMPLOYEE_SALARY_PAYHEADS on TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE=TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE left join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE= TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE "
            Qry1 += " left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.Emp_Code=TSPL_EMPLOYEE_SALARY.Emp_Code left join TSPL_DESIGNATION_MASTER  on  TSPL_DESIGNATION_MASTER.Designation_id = TSPL_EMPLOYEE_MASTER.Designation where TSPL_EMPLOYEE_SALARY.Emp_Code='" & fndEmployeeCode.Value & "' and TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE='" & lblPreviousSalary.Text & "' and  HEAD_TYPE <> 'F' and ISEARNING='1'  "
            Qry1 += " group by TSPL_EMPLOYEE_SALARY.EMP_CODE,TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE,Designation_Desc   )as n left join (select * from (select sum(Rate_Amount)as Current_Amt,TSPL_EMPLOYEE_TRANSFER.Emp_Code  as Em_C ,max(Emp_Name)as Emp_Name ,Document_Code,max(Document_Date)as  Document_Date from	"
            Qry1 += " TSPL_EMPLOYEE_SALARY left join TSPL_EMPLOYEE_SALARY_PAYHEADS on TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE=TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE left join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE= TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.Emp_Code=TSPL_EMPLOYEE_SALARY.Emp_Code "
            Qry1 += "left join TSPL_EMPLOYEE_TRANSFER on TSPL_EMPLOYEE_MASTER.Emp_Code=TSPL_EMPLOYEE_TRANSFER.Emp_Code and  TSPL_EMPLOYEE_TRANSFER.salary_code=TSPL_EMPLOYEE_SALARY.emp_sal_code where TSPL_EMPLOYEE_TRANSFER.Emp_Code='" & fndEmployeeCode.Value & "' and TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE='" & lblSalaryCode.Text & "' and  HEAD_TYPE <> 'F' and ISEARNING='1' group by TSPL_EMPLOYEE_TRANSFER.EMP_CODE,TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE,Document_Code  )as n)  d on d.Em_C=n.emp_code"

            Dim dtgv1 As New DataTable
            dtgv1 = clsDBFuncationality.GetDataTable(Qry1)
            Dim frmcrsytal As New frmCrystalReportViewer
            frmcrsytal.funreport(CrystalReportFolder.HRPayroll, dtgv1, "crptPromotionLetter", "Promotion Letter")
            'ElseIf cboDocType.Text = "Transfer Letter(For Department)" Then
            '    ' PayRoll_HR_ReportViewer.funreport(dtgv, "crptESICChallan", "ESIC Challan Report")
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Try
            Dim qry As String = "select *  from TSPL_EMPLOYEE_TRANSFER"
            txtCode.Value = clsCommon.ShowSelectForm("EMP", qry, "Document_Code", "", txtCode.Value, "", isButtonClicked)

            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub

    Private Sub fndChangedLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndChangedLocation._MYValidating
        Dim qry As String = " select LOCATION_CODE as [Code],LOCATION_DESC from TSPL_LOCATION_MASTER  "
        fndChangedLocation.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("EMP", qry, "Code", "Location_Type='PHYSICAL'", fndChangedLocation.Value, "Code", isButtonClicked))
        If clsCommon.myLen(clsCommon.myCstr(fndChangedLocation.Value)) > 0 Then
            lblChangedLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select LOCATION_DESC from TSPL_LOCATION_MASTER where LOCATION_CODE='" & fndChangedLocation.Value & "'"))
        End If

    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub FrmEmployeeTransfer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

    Private Sub FrmEmployeeTransfer_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Sub AddNew()
        btnProceed.Enabled = False
        cboDocType.Text = "Transfer Letter(For Location) "
        txtDate.Text = clsCommon.GETSERVERDATE()
        btnPrint.Enabled = False
        btnPost.Enabled = False
        isAllowValueChanged = False
        btnsave.Text = "Save"
        btnsave.Enabled = True
        cboSalary.Text = "No"
        txtCode.Value = ""
        txtEffDate.Text = clsCommon.GETSERVERDATE()
        fndEmployeeCode.Value = ""
        txtDescription.Text = ""
        fndChangedDepartment.Value = ""
        fndChangedDesignation.Value = ""
        fndChangedLocation.Value = ""
        lblSalaryCode.Text = ""
        lblEmployeeName.Text = ""
        lblDepartmentName.Text = ""
        lblDesignationName.Text = ""
        lblLocationName.Text = ""
        lblChangedDepartmentName.Text = ""
        lblChangedDesignationName.Text = ""
        lblChangedLocationName.Text = ""
        lblDepartment.Text = ""
        lblDesignation.Text = ""
        lblLocation.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        lblPreviousSalary.Text = ""
        save_structure_code = ""
        lblCurrentDivisionCode.Text = ""
        lblCurrentDivisionName.Text = ""
        fndChangedDivision.Value = ""
        lblChangedDivisionName.Text = ""
    End Sub
    Private Sub fndEmployeeCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndEmployeeCode._MYValidating

        Dim qry As String = " Select Emp_Code AS Code ,Emp_Name  From TSPL_EMPLOYEE_MASTER "
        fndEmployeeCode.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("EMP", qry, "Code", "LEAVING_REASON = ''", fndEmployeeCode.Value, "Code", isButtonClicked))

        If clsCommon.myLen(clsCommon.myCstr(fndEmployeeCode.Value)) > 0 Then
            lblEmployeeName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where Emp_Code='" & fndEmployeeCode.Value & "'"))

            lblDepartment.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  DEPARTMENT_CODE as [Department Code] from tspl_employee_master where Emp_Code='" & fndEmployeeCode.Value & "'"))
            lblDepartmentName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Department_Name from tspl_employee_master left join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER.Department_Code = tspl_employee_master.Department_Code where Emp_Code='" & fndEmployeeCode.Value & "'"))
            lblDesignation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Designation from tspl_employee_master where Emp_Code='" & fndEmployeeCode.Value & "'"))
            lblDesignationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Designation_Desc from tspl_employee_master left join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_id = tspl_employee_master.Designation where Emp_Code='" & fndEmployeeCode.Value & "'"))
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code from TSPL_EMPLOYEE_MASTER where Emp_Code='" & fndEmployeeCode.Value & "'"))
            lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_EMPLOYEE_MASTER left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_EMPLOYEE_MASTER.Location_Code where Emp_Code='" & fndEmployeeCode.Value & "'"))
            lblCurrentDivisionCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  DEVISION_CODE as [Dvision Code] from tspl_employee_master where Emp_Code='" & fndEmployeeCode.Value & "'"))
            lblCurrentDivisionName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  DEVISION_NAME as [Dvision Name] from TSPL_DEVISION_MASTER where DEVISION_CODE='" & lblCurrentDivisionCode.Text & "'"))
        Else
            lblEmployeeName.Text = ""
        End If

    End Sub


    Private Sub fndChangedDepartment__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndChangedDepartment._MYValidating
        Dim qry As String = " select DEPARTMENT_CODE as [Code] ,DEPARTMENT_NAME from TSPL_DEPARTMENT_MASTER"
        fndChangedDepartment.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("EMP", qry, "Code", "", fndChangedDepartment.Value, "Code", isButtonClicked))
        If clsCommon.myLen(clsCommon.myCstr(fndChangedDepartment.Value)) > 0 Then
            lblChangedDepartmentName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DEPARTMENT_NAME from TSPL_DEPARTMENT_MASTER where DEPARTMENT_CODE='" & fndChangedDepartment.Value & "'"))
        End If

    End Sub

    Private Sub fndChangedDesignation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndChangedDesignation._MYValidating
        Dim qry As String = " select Designation_id as [Code],Designation_Desc from TSPL_DESIGNATION_MASTER "
        fndChangedDesignation.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("EMP", qry, "Code", "", fndChangedDesignation.Value, "Code", isButtonClicked))
        If clsCommon.myLen(clsCommon.myCstr(fndChangedDesignation.Value)) > 0 Then
            lblChangedDesignationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Designation_Desc from TSPL_DESIGNATION_MASTER where Designation_id='" & fndChangedDesignation.Value & "'"))
        End If

    End Sub

    Private Sub btnProceed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProceed.Click
        Dim objsal As New frmEmployee_Salary
        Dim Qry As String = "select TSPL_EMPLOYEE_SALARY.Emp_Code,emp_name,R_Version,TSPL_EMPLOYEE_SALARY.salary_structure_code,Salary_Structure_name,emp_sal_code from TSPL_EMPLOYEE_SALARY left join tspl_employee_master on tspl_employee_master.emp_code=TSPL_EMPLOYEE_SALARY.Emp_Code left join (select Emp_Code,max(Revision_no) as R_Version from TSPL_EMPLOYEE_SALARY  where emp_code='" & fndEmployeeCode.Value & "' group by Emp_Code) tt on tt.emp_code=TSPL_EMPLOYEE_SALARY.emp_code and tt.R_Version=TSPL_EMPLOYEE_SALARY.Revision_no left join  TSPL_SALARY_STRUCTURE on TSPL_SALARY_STRUCTURE.Salary_Structure_Code= TSPL_EMPLOYEE_SALARY.Salary_Structure_Code where tt.emp_code='" & fndEmployeeCode.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count > 0 Then
            ' frmEmployee_Salary.LoadData(dt.Rows(0)("emp_sal_code"), NavigatorType.Current)

            objsal.sal_structure_code = clsCommon.myCstr(dt.Rows(0)("emp_sal_code"))
            'frmEmployee_Salary.txtRevisionNo.Text = clsCommon.myCdbl(dt.Rows(0).Item("R_Version")) + 1
            'frmEmployee_Salary.txtCode.Value = Nothing
        Else

            objsal.sal_structure_code = ""
            objsal.txtRevisionNo.Text = 1
            objsal.txtSalaryStruct.Value = ""
            objsal.lblSalStructName.Text = ""
            objsal.dtpApplicableFrom.Value = clsCommon.GETSERVERDATE
        End If
        objsal.ShowDialog()
        lblSalaryCode.Text = save_structure_code

    End Sub


    Private Sub cboDocType_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDocType.TextChanged
        If cboDocType.Text = "Transfer Letter(For Department)" Then
            lblChangedDesignation.Enabled = False
            fndChangedDesignation.Enabled = False
            lblChangedDesignationName.Enabled = False
            lblChangedLocation.Enabled = False
            lblChangedLocationName.Enabled = False
            fndChangedLocation.Enabled = False
            fndChangedDivision.Enabled = False
            lblChangedDivisionName.Enabled = False
            lblChangedDivision.Enabled = False
        ElseIf cboDocType.Text = "Promotion Letter" Then
            lblChangedLocation.Enabled = False
            lblChangedLocationName.Enabled = False
            fndChangedLocation.Enabled = False
            fndChangedDivision.Enabled = False
            lblChangedDivisionName.Enabled = False
            lblChangedDivision.Enabled = False
        Else
            lblChangedDesignation.Enabled = True
            fndChangedDesignation.Enabled = True
            lblChangedDesignationName.Enabled = True
            lblChangedLocation.Enabled = True
            lblChangedLocationName.Enabled = True
            fndChangedLocation.Enabled = True
            fndChangedDivision.Enabled = True
            lblChangedDivisionName.Enabled = True
            lblChangedDivision.Enabled = True
        End If
    End Sub

    Private Sub cboSalary_TextChanged(sender As Object, e As EventArgs) Handles cboSalary.TextChanged
        If isAllowValueChanged = False Then
            If cboSalary.Text = "No" Then
                btnProceed.Enabled = False

            ElseIf cboSalary.Text = "Yes" Then
                btnProceed.Enabled = True
                lblSalaryCode.Enabled = True
                Dim Qry As String = "select TSPL_EMPLOYEE_SALARY.Emp_Code,emp_name,R_Version,TSPL_EMPLOYEE_SALARY.salary_structure_code,Salary_Structure_name,emp_sal_code from TSPL_EMPLOYEE_SALARY left join tspl_employee_master on tspl_employee_master.emp_code=TSPL_EMPLOYEE_SALARY.Emp_Code left join (select Emp_Code,max(Revision_no) as R_Version from TSPL_EMPLOYEE_SALARY  where emp_code='" & fndEmployeeCode.Value & "' group by Emp_Code) tt on tt.emp_code=TSPL_EMPLOYEE_SALARY.emp_code and tt.R_Version=TSPL_EMPLOYEE_SALARY.Revision_no left join  TSPL_SALARY_STRUCTURE on TSPL_SALARY_STRUCTURE.Salary_Structure_Code= TSPL_EMPLOYEE_SALARY.Salary_Structure_Code where tt.emp_code='" & fndEmployeeCode.Value & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                If dt.Rows.Count > 0 Then
                    'frmEmployee_Salary.sal_structure_code = clsCommon.myCstr(dt.Rows(0)("emp_sal_code"))
                    lblPreviousSalary.Text = clsCommon.myCstr(dt.Rows(0)("emp_sal_code"))
              
                End If
            End If
        End If
    End Sub

    Private Sub fndChangedDivision__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndChangedDivision._MYValidating
        Dim qry As String = " select DEVISION_CODE as [Code],DEVISION_NAME as [Name]  from TSPL_DEVISION_MASTER "
        fndChangedDivision.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("DIV", qry, "Code", "", fndChangedDivision.Value, "Code", isButtonClicked))
        If clsCommon.myLen(clsCommon.myCstr(fndChangedDivision.Value)) > 0 Then
            lblChangedDivisionName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DEVISION_NAME  from TSPL_DEVISION_MASTER where DEVISION_CODE ='" & fndChangedDivision.Value & "'"))
        End If
    End Sub
    
End Class
