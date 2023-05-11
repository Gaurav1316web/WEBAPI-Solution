Imports common
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.UI

Public Class FrmMediclaimEntry
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim obj As New clsMediclaimEntry()
    Dim iscellvaluechanged As Boolean = False
    Dim saveclick As Boolean = False
#End Region

    Sub Reset()
        txtCode.Value = ""
        txtdate.Text = ""
        txtdesc.Text = ""
        txtempname.Text = ""
        txtempcode.Value = ""
        txtdesig.Text = ""
        txtdepart.Text = ""
        txtdoj.Text = ""

        txtfromdate.Text = ""
        txttodate.Text = ""
        txtclaimamt.Text = ""
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        txtCode.MyReadOnly = False
        saveclick = False
        txtempcode.Enabled = True
        btngo.Enabled = True
        iscellvaluechanged = True
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnPost.Enabled = False
        btnprint.Enabled = False
        btnsave.Text = "Save"
        UsLock1.Status = ERPTransactionStatus.Pending
        txtdate.Text = clsCommon.GETSERVERDATE()
    End Sub

    Function AllowToSave() As Boolean
        Try
            'If clsCommon.myLen(txtCode.Value) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please Fill Mediclaim Code", Me.Text)
            '    txtCode.Focus()
            '    txtCode.Select()
            '    Return False
            'End If

            If clsCommon.myLen(txtempcode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Employee", Me.Text)
                txtempcode.Focus()
                txtempcode.Select()
                Return False
            End If

            If clsCommon.myLen(txtfromdate.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select From Claim Period", Me.Text)
                txtfromdate.Focus()
                txtfromdate.Select()
                Return False
            End If

            If clsCommon.myLen(txttodate.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select To Claim Date", Me.Text)
                txttodate.Focus()
                txttodate.Select()
                Return False
            End If

            If gv1.Rows.Count < 1 Then
                clsCommon.MyMessageBoxShow("Please Fill Claim Period And Press Go Button For Proper Mediclaim Entry", Me.Text)
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Sub SaveData()
        Try
            saveclick = False
            obj = New clsMediclaimEntry()


            obj.docno = txtCode.Value
            obj.docdate = clsCommon.myCDate(txtdate.Text).ToString("dd/MM/yyyy")
            obj.description = txtdesc.Text

            If clsCommon.myLen(obj.description) > 100 Then
                obj.description = obj.description.Substring(0, 100)
            End If

            obj.emp_code = txtempcode.Value
            obj.frmdate = clsCommon.myCDate(txtfromdate.Text).ToString("dd/MM/yyyy")
            obj.todate = clsCommon.myCDate(txttodate.Text).ToString("dd/MM/yyyy")

            '-----------------grid data---------------------------------
            obj.Arr = New List(Of clsMediclaimEntry)

            Dim totalamt As Integer = 0

            For Each grow As GridViewRowInfo In gv1.Rows
                Dim objtr As New clsMediclaimEntry()
                objtr.docno = txtCode.Value
                objtr.emp_code = txtempcode.Value
                objtr.yearname = clsCommon.myCstr(grow.Cells(0).Value)
                objtr.monthname = clsCommon.myCstr(grow.Cells(1).Value)
                objtr.monthdays = clsCommon.myCdbl(grow.Cells(2).Value)
                objtr.days = clsCommon.myCdbl(grow.Cells(3).Value)
                objtr.basicamt = clsCommon.myCdbl(grow.Cells(4).Value)
                objtr.claimamt = clsCommon.myCdbl(grow.Cells(5).Value)

                totalamt = totalamt + clsCommon.myCdbl(objtr.claimamt)

                If clsCommon.myLen(objtr.yearname) > 0 Then
                    obj.Arr.Add(objtr)
                End If
            Next
            '----------------------------------------------------------------
            obj.toalamount = totalamt



            If clsMediclaimEntry.SaveData(obj, obj.docno, obj.Arr) Then
                clsCommon.MyMessageBoxShow("Data Save Successfully", Me.Text)
                txtCode.Value = obj.docno
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
                btnsave.Text = "Update"
                txtCode.MyReadOnly = True
            Else
                clsCommon.MyMessageBoxShow("Data Can Not Saved", Me.Text)
                btnsave.Enabled = True
                btndelete.Enabled = False
                btnPost.Enabled = False
                btnsave.Text = "Save"
                txtCode.MyReadOnly = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        saveclick = True
        If AllowToSave() Then SaveData()
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Try
            saveclick = False
            Dim qry As String = ""

            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please First Select Mediclaim Code For Posting", Me.Text)
                Return
            End If

            If clsCommon.myLen(txtCode.Value) > 0 Then
                qry = "select count(*) from TSPL_MEDICLAIM_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and document_code='" + txtCode.Value + "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                If check <= 0 Then
                    clsCommon.MyMessageBoxShow("No Data Found For Posting", Me.Text)
                    Return
                End If
            End If

            If Not (common.clsCommon.MyMessageBoxShow("Post the Employee Mediclaim Code " + txtCode.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If

            qry = "update TSPL_MEDICLAIM_HEAD set status='Y' where comp_code='" + objCommonVar.CurrentCompanyCode + "' and document_code='" + txtCode.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)


            clsCommon.MyMessageBoxShow("Data Posted Successfully", Me.Text)
            btnsave.Enabled = False
            btndelete.Enabled = False
            btnPost.Enabled = False
            UsLock1.Status = ERPTransactionStatus.Approved

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            saveclick = False
            Dim qry As String = ""

            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please First Select Mediclaim Code For Deletion", Me.Text)
                Return
            End If

            If clsCommon.myLen(txtCode.Value) > 0 Then
                qry = "select count(*) from TSPL_MEDICLAIM_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and document_code='" + txtCode.Value + "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                If check <= 0 Then
                    clsCommon.MyMessageBoxShow("No Data Found For Deletion", Me.Text)
                    Return
                End If
            End If

            If Not (common.clsCommon.MyMessageBoxShow("Delete the Employee Mediclaim Code " + txtCode.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If

            qry = "delete from TSPL_MEDICLAIM_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and document_code='" + txtCode.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)

            qry = "delete from tspl_mediclaim_detail where document_code='" + txtCode.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)

            clsCommon.MyMessageBoxShow("Data Deleted Successfully", Me.Text)
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FrmMediclaimEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        LoadBlankGrid()
        saveclick = False
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmMediclaimEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub FrmMediclaimEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled Then
            btnPost.PerformClick()
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Reset()
    End Sub

    Sub LoadBlankGrid()
        Dim qry As String = "select '' as Year,'' as Month,0.00 as Days,0.00 as [Atten Days],0.00 as [Basic Amount],0.00 as [Claim Amount]"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
        gv1.DataSource = dt1

        gv1.ReadOnly = False
        gv1.ShowGroupPanel = False
        gv1.ShowGroupedColumns = False


        gv1.Columns("Year").Width = 100
        gv1.Columns("Year").ReadOnly = True

        gv1.Columns("Month").Width = 100
        gv1.Columns("Month").ReadOnly = True

        gv1.Columns("Days").Width = 150
        gv1.Columns("Days").ReadOnly = True

        gv1.Columns("Atten Days").Width = 150
        gv1.Columns("Atten Days").ReadOnly = True

        gv1.Columns("Basic Amount").Width = 150
        gv1.Columns("Basic Amount").ReadOnly = True

        gv1.Columns("Claim Amount").Width = 150
        gv1.Columns("Claim Amount").ReadOnly = True
    End Sub

    Sub LoadData(ByVal strcode As String, ByVal NavTyp As NavigatorType)
        Try
            If clsCommon.myLen(strcode) > 0 Then
                LoadBlankGrid()

                Dim obj As New clsMediclaimEntry
                obj = clsMediclaimEntry.GetData(strcode, NavTyp)

                If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.docno) > 0) Then

                    If obj.post_status = "Y" Then
                        btnsave.Enabled = False
                        btndelete.Enabled = False
                        btnPost.Enabled = False
                        btnprint.Enabled = True
                        UsLock1.Status = ERPTransactionStatus.Approved
                    Else
                        btnsave.Enabled = True
                        btndelete.Enabled = True
                        btnPost.Enabled = True
                        btnprint.Enabled = True
                        UsLock1.Status = ERPTransactionStatus.Pending
                        btnsave.Text = "Update"
                    End If

                    txtCode.Value = clsCommon.myCstr(obj.docno)
                    txtCode.MyReadOnly = True
                    txtempcode.Enabled = False
                    btngo.Enabled = False
                    txtdate.Text = clsCommon.myCDate(obj.docdate).ToString("dd/MM/yyyy")
                    txtdesc.Text = clsCommon.myCstr(obj.description)
                    txtempcode.Value = clsCommon.myCstr(obj.emp_code)
                    txtempname.Text = clsCommon.myCstr(obj.empname)
                    txtdesig.Text = clsCommon.myCstr(obj.desig)
                    txtdepart.Text = clsCommon.myCstr(obj.depart)
                    txtdoj.Text = clsCommon.myCstr(obj.doj)
                    txtfromdate.Text = clsCommon.myCDate(obj.frmdate).ToString("dd/MM/yyyy")
                    txttodate.Text = clsCommon.myCDate(obj.todate).ToString("dd/MM/yyyy")
                    txtclaimamt.Text = clsCommon.myCdbl(obj.toalamount)

                    Dim i As Integer = 0

                    If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                        'Dim objtr As New clsMediclaimEntry

                        For Each objtr As clsMediclaimEntry In obj.Arr
                            gv1.Rows(gv1.Rows.Count - 1).Cells(0).Value = objtr.yearname
                            gv1.Rows(gv1.Rows.Count - 1).Cells(1).Value = objtr.monthname
                            gv1.Rows(gv1.Rows.Count - 1).Cells(2).Value = objtr.monthdays
                            gv1.Rows(gv1.Rows.Count - 1).Cells(3).Value = objtr.days
                            gv1.Rows(gv1.Rows.Count - 1).Cells(4).Value = objtr.basicamt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(5).Value = objtr.claimamt
                            gv1.Rows.AddNew()
                            i += 1
                        Next
                    End If
                Else
                    Reset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_MEDICLAIM_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and document_code='" + txtCode.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check <= 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If

            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select TSPL_MEDICLAIM_HEAD.document_code as Code,TSPL_MEDICLAIM_HEAD.Date,TSPL_MEDICLAIM_HEAD.Description,TSPL_MEDICLAIM_HEAD.emp_code as [Employee Code],TSPL_EMPLOYEE_MASTER.emp_name as [Employee Name],TSPL_EMPLOYEE_MASTER.birth_date as [DOB],TSPL_EMPLOYEE_MASTER.joining_date as [DOJ],TSPL_MEDICLAIM_HEAD.fromdate as [From Claim Period],TSPL_MEDICLAIM_HEAD.todate as [To Claim Period],TSPL_MEDICLAIM_HEAD.total_amount as [Claim Amount] from TSPL_MEDICLAIM_HEAD left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.emp_code=TSPL_MEDICLAIM_HEAD.emp_code"
        'Reset()

        LoadData(clsCommon.ShowSelectForm("MEDFND", qry, "Code", "", txtCode.Value, "Code", isButtonClicked), NavigatorType.Current)

    End Sub

    Private Sub fndempcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtempcode._MYValidating
        Dim qry As String = "select TSPL_EMPLOYEE_MASTER.emp_code as Code,TSPL_EMPLOYEE_MASTER.emp_name as Name,TSPL_EMPLOYEE_MASTER.birth_date as DOB,TSPL_EMPLOYEE_MASTER.joining_date as DOJ,TSPL_DESIGNATION_MASTER.designation_desc as Designation,TSPL_EMPLOYEE_MASTER.emp_type as [Empolyeement],(TSPL_EMPLOYEE_MASTER.add1+' '+TSPL_EMPLOYEE_MASTER.add2) as Address from TSPL_EMPLOYEE_MASTER left outer join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.designation_id=TSPL_EMPLOYEE_MASTER.designation"
        Dim whrcls As String = "" '" TSPL_EMPLOYEE_MASTER.rel_date>='" + txtdate.Text + "'"
        saveclick = False
        txtempcode.Value = clsCommon.ShowSelectForm("EMPFND", qry, "Code", whrcls, txtempcode.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtempcode.Value) > 0 Then
            txtempname.Text = clsDBFuncationality.getSingleValue("select distinct emp_name from TSPL_EMPLOYEE_MASTER where emp_code='" + txtempcode.Value + "'")
            txtdepart.Text = clsDBFuncationality.getSingleValue("select department_name from TSPL_DEPARTMENT_MASTER left outer join TSPL_EMPLOYEE_MASTER on tspl_employee_master.DEPARTMENT_CODE=TSPL_DEPARTMENT_MASTER.department_code where tspl_employee_master.emp_code='" + txtempcode.Value + "'")
            txtdesig.Text = clsDBFuncationality.getSingleValue("select tspl_designation_master.designation_desc from tspl_designation_master left outer join TSPL_EMPLOYEE_MASTER on tspl_employee_master.designation=tspl_designation_master.designation_id where tspl_employee_master.emp_code='" + txtempcode.Value + "'")
            txtdoj.Text = clsDBFuncationality.getSingleValue("select joining_date from TSPL_EMPLOYEE_MASTER where emp_code='" + txtempcode.Value + "'")
        End If
    End Sub


    Sub LoadGrid()
        Dim fromdate As String = clsCommon.myCDate(txtfromdate.Text).ToString("dd/MMM/yyyy")
        Dim todate As String = clsCommon.myCDate(txttodate.Text).ToString("dd/MMM/yyyy")

        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        'Dim qry As String = "DECLARE @start DATE = '" + fromdate + "', @end DATE = '" + todate + "';WITH cte AS (SELECT dt = DATEADD(DAY, -(DAY(@start) - 1), @start) UNION ALL SELECT DATEADD(MONTH, 1, dt) FROM cte WHERE dt < DATEADD(DAY, -(DAY(@end) - 1), @end)) SELECT CONVERT(CHAR(4), dt, 120) as Year,CONVERT(char(4), dt, 100) as Month,0 as [Present Days],0 as [Basic Amount] FROM cte"
        'Dim qry As String = "DECLARE @start DATE = '" + fromdate + "', @end DATE = '" + todate + "';WITH cte AS (SELECT dt = DATEADD(DAY, -(DAY(@start) - 1), @start) UNION ALL SELECT DATEADD(MONTH, 1, dt) FROM cte WHERE dt < DATEADD(DAY, -(DAY(@end) - 1), @end)) SELECT CONVERT(CHAR(4), dt, 120) as Year,CONVERT(char(3), dt, 100) as Month,0 as [Present Days],b.basic as [Basic Amount],0 as [Claim Amount] FROM cte a left outer join (select convert(char(3),datename(month,TSPL_EMPLOYEE_SALARY.APPLICABLE_FROM),100) as MonthName,TSPL_EMPLOYEE_SALARY_PAYHEADS.ACTUAL_AMOUNT as basic from TSPL_EMPLOYEE_SALARY left outer join TSPL_EMPLOYEE_SALARY_PAYHEADS on TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE=TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE and TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE like '%basic%'  where TSPL_EMPLOYEE_SALARY.EMP_CODE='" + txtempcode.Value + "')b on b.MonthName=substring(datename(month,a.dt),1,3)"

        Dim qry As String = "declare @StartDate datetime='" + fromdate + "' " & _
                                "declare @EndDate datetime=  '" + todate + "' " & _
                                "declare @FromDate datetime=  '" + fromdate + "' " & _
                                "declare @EmpCode varchar(20)= '" + txtempcode.Value + "' " & _
                                "select @StartDate= @StartDate-(DATEPART(DD,@StartDate)-1) " & _
                                "declare @temp  table " & _
                                "( TheDate DateTime  ) " & _
                                "while (@StartDate<=@EndDate) " & _
                                "begin " & _
                                "insert into @temp " & _
                                "values (@StartDate ) " & _
                                "select @StartDate=DATEADD(MM,1,@StartDate) " & _
                                "End " & _
                                "SELECT YEARMONTH.Year,YEARMONTH.Month,YEARMONTH.Days as Days,payable_days as [Atten Days],isnull(SAL.Basic,0) as [Basic Amount],round((isnull(sal.basic,0)/datediff(day,'" + fromdate + "','" + todate + "')*isnull(YEARMONTH.Days,0)),0) as [Claim Amount] FROM ( " & _
                                "select TheDate, YEAR(TheDate) as Year, DATENAME(MM, TheDate) as Month, DATEDIFF ( DD , TheDate , DATEADD(MONTH,1, TheDate)) as Days " & _
                                "from @temp " & _
                                ") AS YEARMONTH LEFT JOIN ( " & _
                                "select TSPL_GENERATE_SALARY_ATTENDANCE.payable_days,TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE, TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT [Basic],TSPL_PAYPERIOD_MASTER.DATE_FROM, " & _
                                "TSPL_PAYPERIOD_MASTER.DATE_TO,TSPL_GENERATE_SALARY.payperiod_days " & _
                                "from TSPL_GENERATE_SALARY_PAYHEADS " & _
                                "inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE " & _
                                "inner join TSPL_PAYPERIOD_MASTER on TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE  " & _
                                " inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.emp_code=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE and TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE" & _
                                " where TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=@EmpCode  " & _
                                "and TSPL_PAYPERIOD_MASTER.DATE_FROM >= @FromDate " & _
                                "AND TSPL_PAYPERIOD_MASTER.DATE_TO <= @EndDate " & _
                                "AND TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='BASIC') AS SAL on  Year(YEARMONTH.TheDate)=YEAR(SAL.DATE_TO) AND " & _
                                "Month(YEARMONTH.TheDate) = Month(SAL.DATE_TO) "

        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
        gv1.DataSource = dt1

        gv1.ReadOnly = False
        gv1.ShowGroupPanel = False
        gv1.ShowGroupedColumns = False


        gv1.Columns("Year").Width = 100
        gv1.Columns("Year").ReadOnly = True

        gv1.Columns("Month").Width = 100
        gv1.Columns("Month").ReadOnly = True

        gv1.Columns("Days").Width = 100
        gv1.Columns("Days").ReadOnly = True

        gv1.Columns("Atten Days").Width = 150
        gv1.Columns("Atten Days").ReadOnly = True

        gv1.Columns("Basic Amount").Width = 150
        gv1.Columns("Basic Amount").ReadOnly = True

        gv1.Columns("Claim Amount").Width = 150
        gv1.Columns("Claim Amount").ReadOnly = True



        Dim tolamt As Object = dt1.Compute("sum([Claim Amount])", "")

        txtclaimamt.Text = clsCommon.myCstr(tolamt)
    End Sub

    Private Sub btngo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngo.Click
        saveclick = False
        If clsCommon.myLen(txtempcode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select Employee First", Me.Text)
            txtempcode.Focus()
            txtempcode.Select()
            Return
        End If

        If Convert.ToDateTime(txttodate.Text) <= Convert.ToDateTime(txtfromdate.Text) Then
            clsCommon.MyMessageBoxShow("To Claim Period Should Be Greater Than From Claim Period", Me.Text)
            txttodate.Focus()
            txttodate.Select()
            Return
        End If

        LoadGrid()
    End Sub

    Sub GridCalculation()
        Dim XR As Integer = 0
        Dim claimamt As String = "0"
        Dim period As Integer = 0
        Dim days As String = Nothing
        Dim amt As String = Nothing

        Try
            XR = gv1.CurrentCell.RowIndex
        Catch ex As Exception
            XR = 0
        End Try

        Try
            If gv1.CurrentCell.ColumnIndex >= 2 AndAlso gv1.CurrentCell.ColumnIndex <= 3 Then

                days = ""
                amt = ""
                claimamt = "0"
                period = 0

                Dim date1 As Date = clsCommon.myCDate(txtfromdate.Text)
                Dim date2 As Date = clsCommon.myCDate(txttodate.Text)
                period = DateDiff(DateInterval.Day, date1, date2)

                days = clsCommon.myCstr(gv1.Rows(XR).Cells(3).Value)
                amt = clsCommon.myCstr(gv1.Rows(XR).Cells(4).Value)

                Try
                    Convert.ToDecimal(days)
                Catch ex As Exception
                    days = "0"
                End Try

                Try
                    Convert.ToDecimal(amt)
                Catch ex As Exception
                    amt = "0"
                End Try

                If period > 0 Then
                    claimamt = clsCommon.myCstr(System.Math.Round(Convert.ToDecimal(amt) / period * Convert.ToDecimal(days), 0))
                Else
                    claimamt = "0"
                End If
            End If
        Catch ex As Exception
        End Try

        Try
            iscellvaluechanged = False
            gv1.Rows(XR).Cells(5).Value = claimamt
            ClaimTotalAmt()
        Catch ex1 As Exception
            clsCommon.MyMessageBoxShow(ex1.Message)
        End Try
    End Sub

    Sub ClaimTotalAmt()
        Try
            txtclaimamt.Text = "0"

            Dim ii As Integer = 0
            Dim amount As Integer = 0
            For ii = 0 To gv1.Rows.Count - 1
                Try
                    amount = Convert.ToInt32(gv1.Rows(ii).Cells(5).Value)
                Catch ex As Exception
                    amount = 0
                End Try

                txtclaimamt.Text = clsCommon.myCstr(Convert.ToInt32(txtclaimamt.Text) + amount)
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        If iscellvaluechanged Then
            GridCalculation()
        End If
    End Sub

    Private Sub gv1_CurrentCellChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentCellChangedEventArgs) Handles gv1.CurrentCellChanged
        iscellvaluechanged = True

    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        iscellvaluechanged = True
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index

            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub SplitContainer2_Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Print()
    End Sub
    '--------------------------------Preeti gupta-------BM00000002907------24/06/2014
    Private Sub Print()

        Try

            Dim qry As String = ""
            'Dim Year As String = clsDBFuncationality.getSingleValue("select SUBSTRING(x.a,2,len(x.a)) from (select (select distinct ',['+TSPL_MEDICLAIM_DETAIL.YearName+']' from TSPL_MEDICLAIM_DETAIL for xml path ('')) as a)x")



            qry = "  (select  DatePart(MM, MonthName+' 01 2014') as MM, TSPL_MEDICLAIM_DETAIL.basicamt,TSPL_MEDICLAIM_DETAIL.MonthName,TSPL_MEDICLAIM_DETAIL.PresntDays,TSPL_MEDICLAIM_DETAIL.YearName,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code )>0 then ', '+TSPL_COMPANY_MASTER.State  else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as CompanyAddress,TSPL_MEDICLAIM_HEAD.EMP_CODE,tspl_employee_master.Joining_date ,TSPL_MEDICLAIM_DETAIL.MonthDays ,ClaimAmt, TSPL_EMPLOYEE_MASTER.Emp_Name  as Employeename,Designation_Desc as DesignatonDesc ,DEPARTMENT_NAME ,FromDate ,ToDate   from TSPL_MEDICLAIM_HEAD "
            qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_MEDICLAIM_HEAD .EMP_CODE =TSPL_EMPLOYEE_MASTER .EMP_CODE "
            qry += " left outer join tspl_designation_master on TSPL_EMPLOYEE_MASTER.Designation  =tspl_designation_master.Designation_id"
            qry += " left outer join TSPL_MEDICLAIM_DETAIL on TSPL_MEDICLAIM_DETAIL.DOCUMENT_CODE =TSPL_MEDICLAIM_HEAD.DOCUMENT_CODE "
            qry += " left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER .DEPARTMENT_CODE =TSPL_EMPLOYEE_MASTER .DEPARTMENT_CODE "
            qry += " left outer join TSPL_COMPANY_MASTER on TSPL_MEDICLAIM_HEAD.Comp_code =TSPL_COMPANY_MASTER.Comp_Code where TSPL_MEDICLAIM_HEAD.Document_Code = '" + txtCode.Value + "')order by YearName ,DatePart(MM, MonthName+' 01 2014')  "

            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)

            qry = "Select DOCUMENT_CODE,EMP_CODE ,YearName ,MonthName ,PresntDays ,cast(MonthDays as integer ) as MonthDays,BasicAmt ,ClaimAmt ,DatePart(MM, MonthName+' 01 2014') as MM  from TSPL_MEDICLAIM_DETAIL   WHERE DOCUMENT_CODE='" + txtCode.Value + "' ORDER BY YearName,DatePart(MM, MonthName+' 01 2014')"
            Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim Count As Integer = 0
            Dim Month As String = ""

            Dim dt2 As New DataTable
            dt2.Columns.Add("Period", GetType(String))
            dt2.Columns.Add("Days", GetType(Integer))
            dt2.Columns.Add("Basic", GetType(Decimal))
            dt2.Columns.Add("Payable", GetType(Decimal))
            Dim drdt2 As DataRow = dt2.NewRow()
            Dim BSC As Double = 0
            Dim intStrtIndex As Integer = 0
            Dim intEndIndex As Integer = 0
            Dim intDays As Integer = 0
            Dim flag As Boolean = False
            Dim dblBasicAmt As Double = 0
            Dim fnlBasicAmt As Double = 0
            Dim fnlClaimAmt As Double = 0
            Dim ClaimAmt As Double = 0
            Dim strtIndex As Integer = 0
            Dim totalIntDays As Integer = 0
            For I As Integer = 0 To dtTemp.Rows.Count - 1
                If I = 0 Then
                    BSC = clsCommon.myCdbl(dtTemp.Rows(I)("BasicAmt"))
                    intStrtIndex = I
                    intEndIndex = I
                    intDays = clsCommon.myCdbl(dtTemp.Rows(I)("MonthDays"))
                    dblBasicAmt = clsCommon.myCdbl(dtTemp.Rows(I)("BasicAmt"))
                    ClaimAmt = clsCommon.myCdbl(dtTemp.Rows(I)("ClaimAmt"))
                    flag = False
                ElseIf (BSC <> clsCommon.myCdbl(dtTemp.Rows(I)("BasicAmt"))) OrElse I = dtTemp.Rows.Count - 1 Then
                    intEndIndex = I - 1
                    strtIndex = intStrtIndex
                    intStrtIndex = I
                    totalIntDays = intDays
                    intDays = clsCommon.myCdbl(dtTemp.Rows(I)("MonthDays"))
                    fnlBasicAmt = dblBasicAmt
                    dblBasicAmt = clsCommon.myCdbl(dtTemp.Rows(I)("BasicAmt"))
                    BSC = dblBasicAmt
                    fnlClaimAmt = ClaimAmt
                    ClaimAmt = clsCommon.myCdbl(dtTemp.Rows(I)("ClaimAmt"))
                    flag = True
                Else
                    flag = False
                    intDays = intDays + clsCommon.myCdbl(dtTemp.Rows(I)("MonthDays"))
                    ClaimAmt = ClaimAmt + clsCommon.myCdbl(dtTemp.Rows(I)("ClaimAmt"))
                End If
                If flag Then
                    dt2.Rows.Add("01/" & dtTemp.Rows(strtIndex)("MM") & "/" & dtTemp.Rows(strtIndex)("YearName") & " To " & dtTemp.Rows(intEndIndex)("MonthDays") & "/" & dtTemp.Rows(intEndIndex)("MM") & "/" & dtTemp.Rows(intEndIndex)("YearName"), totalIntDays, fnlBasicAmt, fnlClaimAmt)
                    flag = False
                End If
            Next
            If dt1.Rows.Count > 0 Then
                'PayRoll_HR_ReportViewer.funsubreportByDataTable(dt1, dt2, "crptMediclaim", "Mediclaim Entry", "crptMediclaim2.rpt")
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

End Class
