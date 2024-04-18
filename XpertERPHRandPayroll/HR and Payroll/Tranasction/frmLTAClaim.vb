Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmLTAClaim
    Inherits FrmMainTranScreen
    Const colYear As String = "Year"
    Const colMonth As String = "Month"
    Const colAttendedDays As String = "Attended Days"
    Const colBasicSalary As String = "Basic Salary"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim obj As New clsLTAClaim
    Private ObjList As New List(Of clsApplyLoan)
    Private isCellValueChangedOpen As Boolean = False

    Sub LoadGridColumns()
        Dim Year As New GridViewTextBoxColumn
        Dim Month As New GridViewTextBoxColumn
        Dim Days As New GridViewTextBoxColumn
        Dim BasicSalary As New GridViewDecimalColumn

        'gvClaimPeriod.Rows.Clear()
        'gvClaimPeriod.Columns.Clear()

        Year.FormatString = ""
        Year.HeaderText = "Year"
        Year.Name = colYear
        Year.Width = 100
        Year.ReadOnly = True
        Year.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvClaimPeriod.Columns.Add(Year)

        Month.FormatString = ""
        Month.HeaderText = "Month"
        Month.Name = colMonth
        Month.Width = 100
        Month.ReadOnly = True
        Year.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvClaimPeriod.Columns.Add(Month)

        Days.FormatString = ""
        Days.HeaderText = "Attended Days"
        Days.Name = colAttendedDays
        Days.Width = 100
        Days.ReadOnly = True
        Days.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvClaimPeriod.Columns.Add(Days)

        BasicSalary.FormatString = ""
        BasicSalary.HeaderText = "Basic Salary"
        BasicSalary.Name = colBasicSalary
        BasicSalary.Width = 100
        BasicSalary.ReadOnly = True
        BasicSalary.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvClaimPeriod.Columns.Add(BasicSalary)

    End Sub


    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmMonthlyAttendance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        dtFDate.Value = clsCommon.GETSERVERDATE()
        dtToDate.Value = clsCommon.GETSERVERDATE()

        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmAdjustmentVoucher)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click

        Try
            funReset()
        Catch ex As Exception

        End Try
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        'txtAdjustBy.Value = Nothing
        txtEmpCode.Value = Nothing
        lblEmpName.Text = ""
        txtDOJ.Text = ""
        lblDesignationCode.Text = ""
        lblDesignationName.Text = ""
        lblDeptCode.Text = ""
        lblDeptName.Text = ""
        lblTotalAmount.Text = ""
        dtFDate.Value = clsCommon.GETSERVERDATE()
        dtToDate.Value = clsCommon.GETSERVERDATE()
        btnsave.Text = "Save"
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
        gvClaimPeriod.DataSource = Nothing
        gvClaimPeriod.Columns.Clear()
        gvClaimPeriod.Rows.Clear()
        gvClaimPeriod.GroupDescriptors.Clear()
        gvClaimPeriod.MasterTemplate.SummaryRowsBottom.Clear()
        gvClaimPeriod.EnableFiltering = False

        'Me.gvClaimPeriod.Rows.Clear()
        'Me.gvEMI.Rows.AddNew()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        'btnsave.Enabled = True
        'btndelete.Enabled = True
        obj = clsLTAClaim.GetData(strCode, NavTyep)
        funReset()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.LTA_Code) > 0) Then
            isNewEntry = False
            btnsave.Text = "Update"
            If obj.Posted = 1 Then
                btnPrint.Enabled = True
                btnsave.Enabled = False
                btnPost.Enabled = False
                btndelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnPrint.Enabled = False
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
            Dim ii As Int16 = 0
            'LoadGridColumns()
            txtCode.Value = obj.LTA_Code

            txtEmpCode.Value = clsCommon.myCstr(obj.Emp_Code)
            lblEmpName.Text = clsCommon.myCstr(obj.Emp_Name)
            txtDOJ.Text = clsCommon.myCstr(obj.Date_Of_Joining)
            lblDeptCode.Text = obj.Dept_Code
            lblDeptName.Text = obj.Dept_Name
            lblDesignationCode.Text = obj.Designation_Code
            lblDesignationName.Text = obj.Designation_Desc
            dtFDate.Value = clsCommon.GetPrintDate(obj.from_Date, "dd/MM/yyyy")
            dtToDate.Value = clsCommon.GetPrintDate(obj.to_Date, "dd/MM/yyyy")
            lblTotalAmount.Text = obj.Claim_Amount
            txtCode.MyReadOnly = True


            If (obj.objLTADetails IsNot Nothing AndAlso obj.objLTADetails.Count > 0) Then
                LoadGridColumns()
                For Each obj1 As clsLTAClaimDetail In obj.objLTADetails
                    gvClaimPeriod.Rows.AddNew()

                    gvClaimPeriod.Rows(gvClaimPeriod.Rows.Count - 1).Cells(colYear).Value = obj1.Attended_Year
                    gvClaimPeriod.Rows(gvClaimPeriod.Rows.Count - 1).Cells(colMonth).Value = obj1.Attended_Month
                    gvClaimPeriod.Rows(gvClaimPeriod.Rows.Count - 1).Cells(colAttendedDays).Value = obj1.Attended_Days
                    gvClaimPeriod.Rows(gvClaimPeriod.Rows.Count - 1).Cells(colBasicSalary).Value = obj1.Basic_Salary
                Next
            Else
                'gvEMI.Rows.AddNew()
            End If
        End If

    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_LTA_Claim_Head where LTA_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select LTA_CODE as Code from TSPL_LTA_Claim_Head "
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_LTA_Claim_Head", qry, "Code", "", txtCode.Value, "LTA_CODE", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Sub SaveData()
        If String.IsNullOrEmpty(txtEmpCode.Value) Then
            Throw New Exception("Employee Code Cannot be blank")
        End If

        If String.IsNullOrEmpty(dtFDate.Value) Then
            Throw New Exception("From Date Cannot be blank")
        End If

        If String.IsNullOrEmpty(dtToDate.Value) Then
            Throw New Exception("To Date Cannot be blank")
        End If


        Dim qry As String = "SELECT COUNT(1)Count FROM TSPL_LTA_Claim_Head " & _
                            "WHERE  EMP_CODE ='" + txtEmpCode.Value + "' and Posted=1 " & _
                            "and ( '" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFDate.Value)) + "' between Claim_From_Date and  Claim_To_Date or " & _
                            " '" + clsCommon.GetPrintDate(clsCommon.myCDate(dtToDate.Value)) + "' between Claim_From_Date and  Claim_To_Date ) "


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt.Rows(0)("Count") = 1) Then
            Throw New Exception("LTA Claim Already applied for this period")
        End If

        If clsCommon.myCdbl(lblTotalAmount.Text) <= 0 Then
            Throw New Exception("Total Claim Amount is Zero.")
        End If

        Dim objLTAClaim As New clsLTAClaim
        objLTAClaim.LTA_Code = txtCode.Value
        objLTAClaim.Emp_Code = txtEmpCode.Value
        objLTAClaim.from_Date = dtFDate.Value
        objLTAClaim.to_Date = dtToDate.Value
        objLTAClaim.Claim_Amount = lblTotalAmount.Text

        objLTAClaim.objLTADetails = New List(Of clsLTAClaimDetail)


        For Each grow As GridViewRowInfo In gvClaimPeriod.Rows
            Dim objTr As New clsLTAClaimDetail
            objTr.Attended_Year = clsCommon.myCdbl(grow.Cells("Year").Value)
            objTr.Attended_Month = clsCommon.myCstr(grow.Cells("Month").Value)
            objTr.Attended_Days = clsCommon.myCdbl(grow.Cells("Attended Days").Value)
            objTr.Basic_Salary = clsCommon.myCdbl(grow.Cells("Basic Salary").Value)

            objLTAClaim.objLTADetails.Add(objTr)
        Next

        If String.IsNullOrEmpty(txtCode.Value) Then
            isNewEntry = True
        Else
            isNewEntry = False
        End If
        If (objLTAClaim.SaveData(objLTAClaim, isNewEntry)) Then
            clsCommon.MyMessageBoxShow(Me, " Data Saved Successfully ", Me.Text)
            LoadData(objLTAClaim.LTA_Code, NavigatorType.Current)

        End If


    End Sub

    Private Sub txtEmpcode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtEmpCode._MYValidating
        Try
            Dim qry As String = "select EMP_CODE, Emp_Name,Joining_date, TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE,TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME, TSPL_EMPLOYEE_MASTER.Designation,  TSPL_DESIGNATION_MASTER.Designation_Desc " &
                                "from TSPL_EMPLOYEE_MASTER LEFT OUTER JOIN TSPL_DEPARTMENT_MASTER ON TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE = TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE " &
                                "LEFT OUTER JOIN TSPL_DESIGNATION_MASTER ON TSPL_DESIGNATION_MASTER.Designation_id = TSPL_EMPLOYEE_MASTER.Designation "
            txtEmpCode.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "EMP_CODE", " Emp_Status<>'Inactive'", txtEmpCode.Value, "", isButtonClicked)

            qry += "where EMP_CODE='" + txtEmpCode.Value + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing Then
                lblEmpName.Text = dt.Rows(0)("Emp_Name")
                txtDOJ.Text = dt.Rows(0)("Joining_date")
                lblDeptCode.Text = clsCommon.myCstr(dt.Rows(0)("DEPARTMENT_CODE"))
                lblDeptName.Text = clsCommon.myCstr(dt.Rows(0)("DEPARTMENT_NAME"))
                lblDesignationCode.Text = clsCommon.myCstr(dt.Rows(0)("Designation"))
                lblDesignationName.Text = clsCommon.myCstr(dt.Rows(0)("Designation_Desc"))
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If (clsApplyLoan.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function


    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                If (clsLTAClaim.PostData(txtCode.Value, True)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        'If (Save()) Then
        '    If ChekBtnPost = False Then
        '        common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
        '    End If
        'End If
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        ShowData()

    End Sub

    Sub ShowData()
        Try
            If String.IsNullOrEmpty(txtEmpCode.Value) Then
                Throw New Exception("Please Select Employee")
            End If

            If dtToDate.Value < dtFDate.Value Then
                Throw New Exception("To Date cannot be greater than From Date")
            End If

            Dim qry As String = "SELECT COUNT(1)Count FROM TSPL_LTA_Claim_Head " & _
                                "WHERE  EMP_CODE ='" + txtEmpCode.Value + "' " & _
                                "and ( '" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFDate.Value)) + "' between Claim_From_Date and  Claim_To_Date or " & _
                                " '" + clsCommon.GetPrintDate(clsCommon.myCDate(dtToDate.Value)) + "' between Claim_From_Date and  Claim_To_Date ) "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt.Rows(0)("Count") = 1) Then
                Throw New Exception("LTA Claim Already applied for this period")
            End If

            qry = "declare @StartDate datetime='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFDate.Value), "dd/MMM/yyyy") + "' " & _
                                "declare @EndDate datetime=  '" + clsCommon.GetPrintDate(clsCommon.myCDate(dtToDate.Value), "dd/MMM/yyyy") + "' " & _
                                "declare @FromDate datetime=  '" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFDate.Value), "dd/MMM/yyyy") + "' " & _
                                "declare @EmpCode varchar(20)= '" + txtEmpCode.Value + "' " & _
                                "select @StartDate= @StartDate-(DATEPART(DD,@StartDate)-1) " & _
                                "declare @temp  table " & _
                                "( TheDate DateTime  ) " & _
                                "while (@StartDate<=@EndDate) " & _
                                "begin " & _
                                "insert into @temp " & _
                                "values (@StartDate ) " & _
                                "select @StartDate=DATEADD(MM,1,@StartDate) " & _
                                "End " & _
                                "SELECT YEARMONTH.Year,YEARMONTH.Month,YEARMONTH.Days 'Attended Days',SAL.Basic 'Basic Salary' FROM ( " & _
                                "select TheDate, YEAR(TheDate) as Year,DATENAME(MM, TheDate) as Month, DATEDIFF ( DD , TheDate , DATEADD(MONTH,1, TheDate)) as Days " & _
                                "from @temp " & _
                                ") AS YEARMONTH LEFT JOIN ( " & _
                                "select TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE, TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT [Basic],TSPL_PAYPERIOD_MASTER.DATE_FROM, " & _
                                "TSPL_PAYPERIOD_MASTER.DATE_TO " & _
                                "from TSPL_GENERATE_SALARY_PAYHEADS " & _
                                "inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE " & _
                                "inner join TSPL_PAYPERIOD_MASTER on TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE  " & _
                                "where TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=@EmpCode  " & _
                                "and TSPL_PAYPERIOD_MASTER.DATE_FROM >= @FromDate " & _
                                "AND TSPL_PAYPERIOD_MASTER.DATE_TO <= @EndDate " & _
                                "AND TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='BASIC') AS SAL on  Year(YEARMONTH.TheDate)=YEAR(SAL.DATE_TO) AND " & _
                                "Month(YEARMONTH.TheDate) = Month(SAL.DATE_TO) "


            dt = clsDBFuncationality.GetDataTable(qry)



            gvClaimPeriod.DataSource = Nothing
            gvClaimPeriod.Columns.Clear()
            gvClaimPeriod.Rows.Clear()
            gvClaimPeriod.GroupDescriptors.Clear()
            gvClaimPeriod.MasterTemplate.SummaryRowsBottom.Clear()
            gvClaimPeriod.EnableFiltering = True

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Dim i As Integer = 0
                Dim count As Integer = 0
                Dim lastSalary As Double = 0.0
                Dim days As Integer = clsCommon.myCdbl(dt.Rows(0)("Attended Days")) - clsCommon.myCdbl(dtFDate.Value.Date.Day - 1)
                dt.Rows(0)("Attended Days") = days

                'days = 0
                'days = clsCommon.myCdbl(dt.Rows(dt.Rows.Count - 1)("Attended Days")) - clsCommon.myCdbl(dtToDate.Value.Date.Day)
                'days = clsCommon.myCdbl(dt.Rows(dt.Rows.Count - 1)("Attended Days")) - days
                dt.Rows(dt.Rows.Count - 1)("Attended Days") = clsCommon.myCdbl(dtToDate.Value.Date.Day)

                Dim basicSalary As Double = clsCommon.myCdbl(dt.Rows(0)("Basic Salary"))
                For i = 0 To dt.Rows.Count - 1
                    If clsCommon.myCdbl(dt.Rows(i)("Basic Salary")) <> 0 Then
                        basicSalary = clsCommon.myCdbl(dt.Rows(i)("Basic Salary"))
                    End If
                    dt.Rows(i)("Basic Salary") = basicSalary
                    If basicSalary > 0 And count = 0 Then
                        Dim j As Integer = 0
                        For j = 0 To i
                            If dt.Rows(j)("Basic Salary") = 0 Then
                                dt.Rows(j)("Basic Salary") = basicSalary
                            End If
                        Next
                        count = 1
                    End If
                Next
                'LoadGridColumns()
                gvClaimPeriod.DataSource = dt
                days = 0
                Dim total_Claim_Amount As Double = 0
                basicSalary = clsCommon.myCdbl(dt.Rows(0)("Basic Salary"))
                For i = 0 To dt.Rows.Count - 1
                    If clsCommon.myCdbl(dt.Rows(0)("Basic Salary")) <> basicSalary Then
                        total_Claim_Amount += (basicSalary / 365) * days
                        days = 0
                        basicSalary = clsCommon.myCdbl(dt.Rows(i)("Basic Salary"))
                    End If
                    days += clsCommon.myCdbl(dt.Rows(i)("Attended Days"))

                Next
                total_Claim_Amount += (basicSalary / 365) * days
                total_Claim_Amount = Math.Round(total_Claim_Amount, 0)
                lblTotalAmount.Text = total_Claim_Amount.ToString()

                'SetGridFormationOFGV1()
            End If

            gvClaimPeriod.BestFitColumns()

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If String.IsNullOrEmpty(txtEmpCode.Value) Then
                Throw New Exception("Please Select Employee")
            End If

            If dtToDate.Value < dtFDate.Value Then
                Throw New Exception("To Date cannot be greater than From Date")
            End If

            Dim qry As String = "SELECT TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2,  TSPL_LTA_Claim_Head.EMP_CODE, Emp_Name,Joining_date, TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE,TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME,TSPL_EMPLOYEE_MASTER.Designation,  TSPL_DESIGNATION_MASTER.Designation_Desc, convert(varchar(12),TSPL_LTA_Claim_Head.Claim_From_Date,106)Claim_From_Date ,convert(varchar(12),TSPL_LTA_Claim_Head.Claim_To_Date,106)Claim_To_Date, TSPL_LTA_Claim_Head.Claim_Amount, (case when isnull(Attended_Month,'')='January' then 1 			 when isnull(Attended_Month,'')='February' then 2 when isnull(Attended_Month,'')='March' then 3 when isnull(Attended_Month,'')='April' then 4 " & _
                                "when isnull(Attended_Month,'')='May' then 5 when isnull(Attended_Month,'')='June' then 6 when isnull(Attended_Month,'')='July' then 7 " & _
                                "when isnull(Attended_Month,'')='August' then 8  when isnull(Attended_Month,'')='September' then 9 when isnull(Attended_Month,'')='October' then 10 " & _
                                "when isnull(Attended_Month,'')='November' then 11 when isnull(Attended_Month,'')='December' then 12 end ) as [Months],[Attended_Month], [Attended_Days], [Attended_Year]  " & _
                                "FROM TSPL_LTA_Claim_Detail " & _
                                "LEFT OUTER JOIN TSPL_LTA_Claim_Head ON TSPL_LTA_Claim_Head.LTA_CODE = TSPL_LTA_Claim_Detail.LTA_CODE " & _
                                "LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_LTA_Claim_Head.EMP_CODE " & _
                                "LEFT OUTER JOIN TSPL_DEPARTMENT_MASTER ON TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE = TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE  " & _
                                "LEFT OUTER JOIN TSPL_DESIGNATION_MASTER ON TSPL_DESIGNATION_MASTER.Designation_id = TSPL_EMPLOYEE_MASTER.Designation " & _
                                "LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code = TSPL_LTA_Claim_Head.Comp_Code " & _
                                "where TSPL_LTA_Claim_Head.LTA_CODE='" + txtCode.Value.Trim() + "' " & _
                                "order by Months "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmcrsytal As New frmCrystalReportViewer
                frmcrsytal.funreport(CrystalReportFolder.HRPayroll, dt, "crptLTAClaim", "LTA Claim")
            Else
                Throw New Exception("No data found to display")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class