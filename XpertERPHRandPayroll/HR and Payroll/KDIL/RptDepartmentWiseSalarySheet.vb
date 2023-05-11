''changes by shivani [BM00000008157]
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls

Public Class RptDepartmentWiseSalarySheet
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim Print As Boolean = True
    Dim EarningTotalColNo As Integer = 0
    Dim DeductionTotalColNo As Integer = 0
    Dim EarningRateTotalColNo As Integer = 0
    Dim NoOfPayHeadsInEachCol As Integer = 4
    Dim Comp_PF_No As String = ""
    Dim Comp_ESI_No As String = ""

#End Region
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Print = True
        PageSetupReport_ID = MyBase.Form_ID
        PrintData()
    End Sub

    Private Sub frmPaySlip_Reports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        FunReset()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmDepartmentwiseSalarySheetRpt)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnExport.Visible = MyBase.isExport
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub FunReset()
        GetCompanyPFESI_Numbers()
        txtFromPP.Value = Nothing
        lblFrompp.Text = ""
        ddlReportType.SelectedText = "Departmentwise"
        txtLocationMult.arrValueMember = Nothing
        txtDepartmentMult.arrValueMember = Nothing
        txtPayHeadMult.arrValueMember = Nothing

    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromPP._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
            & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        txtFromPP.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtFromPP.Value, "", isButtonClicked)
        lblFrompp.Text = clsPayPeriodMaster.GetName(txtFromPP.Value, Nothing)

    End Sub

    Sub PrintData()
        'Try
        Dim font As System.Drawing.Font = Gv1.Font
        'font.Size = 8.25
        Gv1.DataSource = Nothing
        If clsCommon.myLen(txtFromPP.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Pay Period.")
            Return
        End If
        NoOfPayHeadsInEachCol = Me.txtNum.Value
        'If clsCommon.myLen(txtLocationMult.Value) <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please Select Location.")
        '    Return
        'End If

        'If cbgDepartment.CheckedValue.Count <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please Select AtLeast Single Department Or Select All")
        '    Return
        'End If
        'If cbgPayHeads.CheckedValue.Count <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please Select AtLeast Single Pay Head Or Select All")
        '    Return
        'End If

        '' variables
        Dim PayHeadUpperSelect As String = ""
        Dim PayHeadUpperSelect1 As String = ""
        Dim PayHeadPivot As String = ""
        Dim objPFRule As clsPFRulesMaster
        Dim objESIRule As clsESIRulesMaster
        Dim Cond As String = ""
        Dim EarningTotal As String = ""
        'Dim DedTotal As String = ""
        Dim NetAmountSelect As String = ""

        '' get pf rule 
        objPFRule = clsPFRulesMaster.GetRecentPFRule(txtFromPP.Value)
        If objPFRule Is Nothing Then
            Exit Sub
        End If
        '' get ESI rule 
        objESIRule = clsESIRulesMaster.GetRecentESIRule(txtFromPP.Value)
        If objESIRule Is Nothing Then
            Exit Sub
        End If

        Dim PHQuery As String = " SELECT DISTINCT  PAY_HEAD_CODE FROM TSPL_SALSTRUCT_PAYHEADS WHERE SALARY_STRUCTURE_CODE IN (select distinct SALARY_STRUCTURE_CODE from TSPL_GENERATE_SALARY_ATTENDANCE GSA INNER JOIN TSPL_GENERATE_SALARY GS ON GSA.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE WHERE GS.PAY_PERIOD_CODE='" & txtFromPP.Value & "') "
        PHQuery = " select Pay_Head_Code,IsEarning,Head_Type,(ROW_NUMBER() over (partition by IsEarning order by Group_Seq)) Group_Seq,(Group_Seq % " & NoOfPayHeadsInEachCol & ") as Seq from ( select PAY_HEAD_CODE,PAY_HEAD_NAME,ISEARNING,Head_Type,PRINT_GROUP_SEQ AS  Group_Seq " & _
                  " from TSPL_PAYHEAD_MASTER) TSPL_PAYHEAD_MASTER WHERE PAY_HEAD_CODE IN (" & PHQuery & ")  "


        Dim CompName As String = clsCommon.myCstr(objCommonVar.CurrentCompanyName)
        Dim LocAdd As String = ""
        Dim LocationFirstTime1 As Integer = 0
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count = 1 Then
            LocationFirstTime1 += 1
            LocAdd = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.City_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.City_Code) End End End as Location_Address from tspl_location_master where Location_Code  in (" & clsCommon.GetMulcallString(txtLocationMult.arrValueMember) & ")")
        Else
            LocAdd = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Comp_Name +','+ TSPL_COMPANY_MASTER.Add1+Case When ISNULL(TSPL_COMPANY_MASTER.Add2,'')='' Then '' else ', '+TSPL_COMPANY_MASTER.Add2+ Case When ISNULL(TSPL_COMPANY_MASTER.Add3,'')='' Then '' Else ', '+TSPL_COMPANY_MASTER.Add3 + Case When ISNULL(TSPL_COMPANY_MASTER.City_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_COMPANY_MASTER.City_Code) End End End as Comp_Address from TSPL_COMPANY_MASTER "))
        End If
        'Dim LocAdd As String = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.City_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.City_Code) End End End as Location_Address from tspl_location_master where Location_Code  in (" & clsCommon.GetMulcallString(txtLocationMult.arrValueMember) & ")")
        Dim PayPeriod As String = clsCommon.myCstr(txtFromPP.Value)
        Dim LocDesc As String = ""
        Dim LocationFirstTime As Integer = 0
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count = 1 Then
            LocationFirstTime += 1
            LocDesc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Location_Desc FROM TSPL_LOCATION_MASTER WHERE Location_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"))
        Else
            LocDesc = "All"
        End If
        Dim DivDes As String = ""
        Dim DivisionFirstTime As Integer = 0
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count = 1 Then
            DivisionFirstTime += 1
            DivDes = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  DEVISION_NAME  from TSPL_DEVISION_MASTER WHERE DEVISION_CODE in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ")"))
        Else
            DivDes = "All"
        End If

        Dim dtFinal As DataTable = GetCommonDT(PHQuery, Me.ddlReportType.Text, txtDepartmentMult.arrValueMember)
        Dim GrandTotal As Decimal = 0.0

        For Each row As DataRow In dtFinal.Rows
            GrandTotal = GrandTotal + clsCommon.myCdbl(row.Item("Net_Payment"))
        Next
        Dim drGrand As DataRow = dtFinal.NewRow
        drGrand(0) = "Total:"
        drGrand(dtFinal.Columns.Count - 1) = GrandTotal
        dtFinal.Rows.Add(drGrand)
        Gv1.DataSource = Nothing
        Gv1.DataSource = dtFinal
        '' changes by shivani[8265]
        If Print = False Then
            If clsCommon.CompairString(ddlReportType.Text, "Departmentwise") = CompairStringResult.Equal Then
                Dim dtFinalCR As New DataTable
                dtFinalCR = GetFinalDtCR("", dtFinalCR, dtFinal)
                dtFinalCR.Merge(dtFinal)
                dtFinalCR.Columns.Add("CompName", GetType(String))
                dtFinalCR.Columns.Add("LocAdd", GetType(String))
                dtFinalCR.Columns.Add("PayPeriod", GetType(String))
                For ix As Integer = 0 To dtFinalCR.Rows.Count - 1
                    dtFinalCR.Rows(ix).Item("CompName") = clsCommon.myCstr(CompName)
                    dtFinalCR.Rows(ix).Item("LocAdd") = clsCommon.myCstr(LocAdd)
                    dtFinalCR.Rows(ix).Item("PayPeriod") = clsCommon.myCstr(PayPeriod)

                Next
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtFinalCR, "DepartmentWiseSalarySlip", "DepartmentWise Salary Sheet Report")
            Else
                'Dim dtFinalCR As DataTable = GetFinalDtCR(dtFinal)

                Dim dtFinalCRR As New DataTable
                Dim ArrDepartment As New ArrayList

                If txtDepartmentMult.arrValueMember Is Nothing Then
                    ArrDepartment = GetAllDepartment()

                ElseIf txtDepartmentMult.arrValueMember.Count = 0 Then
                    ArrDepartment = GetAllDepartment()
                Else
                    ArrDepartment = txtDepartmentMult.arrValueMember
                End If
                For Each Department As String In ArrDepartment
                    Dim arrlstDept As New ArrayList
                    arrlstDept.Add(Department)
                    Dim dtEmp As DataTable = GetCommonDT(PHQuery, "Employeewise", arrlstDept)
                    If dtEmp.Rows.Count > 0 Then
                        dtFinalCRR = GetFinalDtCR(clsDepartmentMaster.GetName(Department, Nothing), dtFinalCRR, dtEmp)
                        dtFinalCRR.Merge(dtEmp)

                        Dim dtDept As DataTable = GetCommonDT(PHQuery, "Departmentwise", arrlstDept)
                        If dtDept.Rows.Count > 0 Then
                            Dim drDEpt As DataRow = dtDept.Rows(0)
                            drDEpt("Serial_No") = ""
                            drDEpt("Employees") = "Total"
                            dtFinalCRR.Rows.Add(drDEpt.ItemArray)
                        End If
                    End If
                Next
                dtFinalCRR.Columns.Add("CompName", GetType(String))
                dtFinalCRR.Columns.Add("LocAdd", GetType(String))
                dtFinalCRR.Columns.Add("PayPeriod", GetType(String))
                dtFinalCRR.Columns.Add("LocDesc", GetType(String))
                dtFinalCRR.Columns.Add("DivDes", GetType(String))
                For ix As Integer = 0 To dtFinalCRR.Rows.Count - 1
                    dtFinalCRR.Rows(ix).Item("CompName") = clsCommon.myCstr(CompName)
                    dtFinalCRR.Rows(ix).Item("LocAdd") = clsCommon.myCstr(LocAdd)
                    dtFinalCRR.Rows(ix).Item("PayPeriod") = clsCommon.myCstr(PayPeriod)
                    dtFinalCRR.Rows(ix).Item("LocDesc") = clsCommon.myCstr(LocDesc)
                    dtFinalCRR.Rows(ix).Item("DivDes") = clsCommon.myCstr(DivDes)
                Next

                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtFinalCRR, "EmployeeWiseSalarySlip", "EmployeeWise Salary Sheet Report")

            End If


        End If
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
            If Gv1.Columns(ii).Index > 1 Then
                Gv1.Columns(ii).TextAlignment = ContentAlignment.MiddleRight
            Else
                Gv1.Columns(ii).TextAlignment = ContentAlignment.MiddleLeft
            End If

        Next

        Gv1.AllowAddNewRow = False
        Gv1.AutoSizeRows = True
        Gv1.AllowAutoSizeColumns = True
        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.EnableGrouping = True
        Gv1.EnableSorting = True
        Gv1.EnableFiltering = True
        'Gv1.EnablePaging = True
        'Gv1.ReadOnly = True

        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try

    End Sub
    Function GetAllDepartment() As ArrayList
        Dim qry As String
        Dim ArrDept As New ArrayList
        qry = "select Department_Code,DEPARTMENT_NAME from tspl_department_master "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        For Each dr As DataRow In dt.Rows
            ArrDept.Add(dr.Item("Department_Code"))
        Next
        Return ArrDept
    End Function
    Function GetCommonDT(ByVal PHQuery As String, ByVal ReportType As String, ByVal ArrDepartments As ArrayList) As DataTable
        Dim Cond As String = ""
        '' assign filter condition
        Cond = " AND GS.PAY_PERIOD_CODE='" & txtFromPP.Value & "'"
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            Cond = Cond & " and  GS.Location_Code in (" & clsCommon.GetMulcallString(txtLocationMult.arrValueMember) & ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            Cond = Cond & " and EMP.DEVISION_CODE in (" & clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) & " )"
        End If
        If ArrDepartments IsNot Nothing AndAlso ArrDepartments.Count > 0 Then
            Cond = Cond & " and EMP.Department_Code in (" & clsCommon.GetMulcallString(ArrDepartments) & ")"
        End If
        If txtPayHeadMult.arrValueMember IsNot Nothing AndAlso txtPayHeadMult.arrValueMember.Count > 0 Then
            Cond = Cond & " and GSP.Pay_Head_Code in (" & clsCommon.GetMulcallString(txtPayHeadMult.arrValueMember) & ")"
        End If

        Dim dtPH As DataTable
        dtPH = GetPayHeadDT()
        Dim SalQry As String = ""
        Dim InnerQry As String = ""
        SalQry = "select GSA.EMP_CODE,coalesce(EMP.DEPARTMENT_CODE,'') as DEPARTMENT_CODE,coalesce(DPT.DEPARTMENT_NAME,'') as DEPARTMENT_NAME,GSP.PAY_HEAD_CODE,PHM.ISEARNING,PHM.Head_Type,PHM.Group_Seq,(PHM.Group_Seq % " & NoOfPayHeadsInEachCol & ") as Seq,count(GSA.EMP_CODE) AS Total, " & _
            " SUM(case when GSP.SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 THEN 1 ELSE 0 END) AS PF_Count, " & _
            " SUM(case when GSP.SUB_HEAD_TYPE='EMPESI' AND ACTUAL_AMOUNT>0 THEN 1 ELSE 0 END) AS ESI_Count, " & _
            " SUM(case when GSP.SUB_HEAD_TYPE='EPF' and ACTUAL_AMOUNT>0 THEN CoEPS_AMT_AC10 ELSE 0 END) AS Pension, " & _
            " SUM(case when GSP.SUB_HEAD_TYPE='EPF' and ACTUAL_AMOUNT>0 THEN CoEPF_AMT_AC01 ELSE 0 END) as Comp_PF, " & _
            " SUM(case when GSP.SUB_HEAD_TYPE='EMPESI' and ACTUAL_AMOUNT>0 THEN Co_ESI_AMT ELSE 0 END) " & _
            " as Comp_ESI,SUM(case when GSP.SUB_HEAD_TYPE='LWF' AND ACTUAL_AMOUNT>0 THEN 2*ACTUAL_AMOUNT ELSE 0 END) AS LWFER ,SUM(GSP.ACTUAL_AMOUNT) AS ACTUAL_AMOUNT,SUM(Payable_Amount) AS Payable_Amount " & _
            " from TSPL_GENERATE_SALARY_PAYHEADS GSP " & _
            " INNER JOIN TSPL_GENERATE_SALARY GS ON GS.SALARY_GENERATION_CODE=GSP.SALARY_GENERATION_CODE " & _
            " INNER JOIN TSPL_GENERATE_SALARY_ATTENDANCE GSA ON GSA.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE AND GSA.EMP_CODE=GSP.EMP_CODE " & _
            " INNER JOIN (" & PHQuery & ") PHM ON GSP.PAY_HEAD_CODE=PHM.PAY_HEAD_CODE " & _
            " INNER JOIN TSPL_EMPLOYEE_MASTER EMP ON GSA.EMP_CODE=EMP.EMP_CODE " & _
            " INNER JOIN TSPL_DEPARTMENT_MASTER DPT ON EMP.DEPARTMENT_CODE=DPT.DEPARTMENT_CODE where 2=2 " & Cond & "" & _
            " GROUP BY GSA.EMP_CODE,EMP.DEPARTMENT_CODE,DPT.DEPARTMENT_NAME,GSP.PAY_HEAD_CODE,PHM.Group_Seq,PHM.ISEARNING,PHM.Head_Type "

        InnerQry = " SELECT InnerFinal.EMP_CODE,Emp_Name,Joining_date,EMP.FATHERS_NAME,DESG.Designation_Desc,EMP.PF_NO,EMP.ESI_NO,EMP.BANK_ACC_NO,BANK.DESCRIPTION AS BANK_NAME, " & _
            " EMP.Bank_Branch AS IFSC,InnerFinal.DEPARTMENT_CODE,InnerFinal.DEPARTMENT_NAME,PAY_HEAD_CODE,ISEARNING,Head_Type,Group_Seq,Seq,Total,PF_Count,ESI_Count, " & _
            " cast(round(Pension,0) as int) as  Pension , cast(round(Comp_PF,0) as int) as Comp_PF,cast(round(Comp_ESI,0) as int) as Comp_ESI,cast(round(LWFER,0) as int) as LWFER,cast(round(ACTUAL_AMOUNT,0) as int) as ACTUAL_AMOUNT,cast(round(Payable_Amount,0) as int) as  Payable_Amount " & _
            " FROM ( " & SalQry & " ) AS InnerFinal " & _
            " LEFT join TSPL_EMPLOYEE_MASTER EMP ON InnerFinal.EMP_CODE=EMP.EMP_CODE " & _
            " LEFT JOIN TSPL_DESIGNATION_MASTER DESG ON EMP.Designation=DESG.DESIGNATION_ID " & _
            " LEFT JOIN TSPL_BANK_MASTER BANK ON EMP.BANK_CODE=BANK.BANK_CODE "

        InnerQry = "select * from (" & InnerQry & " Union All " & _
            " select EMP.EMP_CODE,EMP.EMP_NAME,null as Joining_date,'' AS FATHERS_NAME,'' AS Designation_Desc,'' as PF_NO, '' as ESI_NO,'' as BANK_ACC_NO,'' as BANK_NAME, " & _
            " '' as IFSC,EMP.DEPARTMENT_CODE as DEPARTMENT_CODE,EMP.DEPARTMENT_CODE as DEPARTMENT_NAME,'Total' as PAY_HEAD_CODE,'True' as ISEARNING,'' as Head_Type," & EarningTotalColNo & " as Group_Seq," & (NoOfPayHeadsInEachCol - 1) & " as Seq,0 as Total," & _
            " 0 as PF_Count,0 as ESI_Count,0 as Pension,0 as Comp_PF,0 as Comp_ESI,0 as LWFER,0 as ACTUAL_AMOUNT,0 AS Payable_Amount " & _
            " from TSPL_EMPLOYEE_MASTER EMP WHERE EMP_CODE IN (select distinct EMP_CODE from (" & InnerQry & ") as Dept) " & _
            " union all " & _
            " select EMP.EMP_CODE,EMP.EMP_NAME,null as Joining_date,'' AS FATHERS_NAME,'' AS Designation_Desc,'' as PF_NO, '' as ESI_NO,'' as BANK_ACC_NO,'' as BANK_NAME, " & _
            " '' as IFSC, EMP.DEPARTMENT_CODE,EMP.DEPARTMENT_CODE AS DEPARTMENT_NAME,'Total' as PAY_HEAD_CODE,'False' as ISEARNING,'' as Head_Type," & DeductionTotalColNo & " as Group_Seq," & (NoOfPayHeadsInEachCol - 1) & " as Seq,0 as Total, " & _
            " 0 as PF_Count,0 as ESI_Count,0 as Pension,0 as Comp_PF,0 as Comp_ESI,0 as LWFER,0 as ACTUAL_AMOUNT,0 AS Payable_Amount " & _
            " from TSPL_EMPLOYEE_MASTER EMP WHERE EMP_CODE IN (select distinct EMP_CODE from (" & InnerQry & ") as Dept)) as Final "
        '" select EMP.EMP_CODE,EMP.EMP_NAME,null as Joining_date,'' AS FATHERS_NAME,'' AS Designation_Desc,'' as PF_NO, '' as ESI_NO,'' as BANK_ACC_NO,'' as BANK_NAME, " & _
        '    " '' as IFSC,EMP.DEPARTMENT_CODE as DEPARTMENT_CODE,EMP.DEPARTMENT_CODE as DEPARTMENT_NAME,'Total' as PAY_HEAD_CODE,'True' as ISEARNING,'ATTN' as Head_Type," & EarningRateTotalColNo & " as Group_Seq,2 as Seq,0 as Total," & _
        '    " 0 as PF_Count,0 as ESI_Count,0 as Pension,0 as Comp_PF,0 as Comp_ESI,0 as LWFER,0 as ACTUAL_AMOUNT,0 AS Payable_Amount " & _
        '    " from TSPL_EMPLOYEE_MASTER EMP WHERE EMP_CODE IN (select distinct EMP_CODE from (" & InnerQry & ") as Dept) " & _
        '    " union all " & _

        Dim LEAVE_QRY As String = ""
        Dim CondLeave As String = ""
        CondLeave = " AND GS.PAY_PERIOD_CODE='" & txtFromPP.Value & "'"
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            CondLeave = CondLeave & " and  GS.Location_Code in (" & clsCommon.GetMulcallString(txtLocationMult.arrValueMember) & ") "
        End If
        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            CondLeave = CondLeave & " and EMP.DEVISION_CODE in (" & clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) & " )"
        End If
        'If txtDepartmentMult.arrValueMember IsNot Nothing AndAlso txtDepartmentMult.arrValueMember.Count > 0 Then
        '    CondLeave = CondLeave & " and EMP.Department_Code in (" & clsCommon.GetMulcallString(txtDepartmentMult.arrValueMember) & ")"
        'End If
        If ArrDepartments IsNot Nothing AndAlso ArrDepartments.Count > 0 Then
            CondLeave = CondLeave & " and EMP.Department_Code in (" & clsCommon.GetMulcallString(ArrDepartments) & ")"
        End If
        ''changes by shivani[BM00000008213], changed by Panch raj against Ticket: BM00000008299 , changes by parteek weekly off employee in 04/04/2017
        LEAVE_QRY = "SELECT GSA.EMP_CODE,EMP.DEPARTMENT_CODE,GSA.PAYPERIOD_DAYS,GSA.PRESENT_DAYS,GSA.ABSENT_DAYS,GSA.HOLIDAY_DAYS," _
            & " (GSA.PAYPERIOD_DAYS-GSA.PRESENT_DAYS-GSA.LEAVE_DAYS-GSA.HOLIDAY_DAYS-GSA.LOP_DAYS) AS WEEKLY_OFF, " _
            & " GSA.PAYABLE_DAYS,COALESCE(LEDGER.EL,0) AS EL,COALESCE(LEDGER.CL,0) AS CL,COALESCE(LEDGER.SL,0) AS SL, " _
            & " COALESCE(LEDGER.CH,0) AS CH,COALESCE(LEDGER.ML,0) AS ML,COALESCE(LEDGER.OTHER,0) AS OTHER " _
            & " FROM TSPL_GENERATE_SALARY_ATTENDANCE  GSA INNER JOIN TSPL_GENERATE_SALARY GS ON GS.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE " _
            & " LEFT JOIN (SELECT EMP_CODE,SUM(EL) AS EL,SUM(CL) AS CL,SUM(SL) AS SL,SUM(CH) AS CH,SUM(ML)AS ML,SUM(OTHER) AS OTHER,PAY_PERIOD_CODE FROM ( " _
            & " select LEDGER.EMP_CODE,(CASE WHEN LEAVE.LEAVE_TYPE='EL' THEN AVAILED ELSE 0 END) AS EL,(CASE WHEN LEAVE.LEAVE_TYPE='CL' THEN AVAILED ELSE 0 END) AS CL, " _
            & " (CASE WHEN LEAVE.LEAVE_TYPE='MED' THEN AVAILED ELSE 0 END) AS SL,(CASE WHEN LEAVE.LEAVE_TYPE='COFF' THEN AVAILED ELSE 0 END) AS CH, " _
            & " (CASE WHEN LEAVE.LEAVE_TYPE='MATRL' THEN AVAILED ELSE 0 END) AS ML,(CASE WHEN LEAVE.LEAVE_TYPE='Other' THEN AVAILED ELSE 0 END) AS OTHER,PAY_PERIOD_CODE " _
            & " from TSPL_VIEW_LEAVE_LEDGER LEDGER  " _
            & " LEFT JOIN TSPL_LEAVE_MASTER LEAVE ON LEDGER.LEAVE_CODE=LEAVE.LEAVE_CODE " _
            & " ) AS LEAVES GROUP BY EMP_CODE,PAY_PERIOD_CODE) AS LEDGER ON GSA.EMP_CODE=LEDGER.EMP_CODE  and  GS.PAY_PERIOD_CODE=LEDGER.PAY_PERIOD_CODE " _
            & " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON GSA.EMP_CODE=EMP.EMP_CODE " _
            & " WHERE 2=2  " & CondLeave & ""



        If clsCommon.CompairString(ReportType, "Departmentwise") = CompairStringResult.Equal Then
            Qry = "select DEPARTMENT_CODE,DEPARTMENT_NAME,PAY_HEAD_CODE,ISEARNING,Group_Seq,Seq,sum(Total) as Total,sum(PF_Count) as PF_Count,sum(ESI_Count) as ESI_Count,sum(Pension) as Pension," & _
                " sum(Comp_PF) as Comp_PF,sum(Comp_ESI) as Comp_ESI,sum(LWFER) as LWFER,sum(ACTUAL_AMOUNT) as ACTUAL_AMOUNT,SUM(Payable_Amount) AS Payable_Amount from (" & InnerQry & ") InnerTable " & _
                " GROUP BY DEPARTMENT_CODE,DEPARTMENT_NAME,PAY_HEAD_CODE,Group_Seq,Seq,ISEARNING order by DEPARTMENT_CODE,ISEARNING desc,Group_Seq "

            'Qry = " SELECT MAIN.*,LEAVE.PAYPERIOD_DAYS,LEAVE.PRESENT_DAYS,LEAVE.ABSENT_DAYS,LEAVE.HOLIDAY_DAYS,LEAVE.WEEKLY_OFF, " & _
            '    " LEAVE.PAYABLE_DAYS,LEAVE.EL,LEAVE.CL,LEAVE.SL,LEAVE.CH,LEAVE.ML,LEAVE.OTHER FROM (" & Qry & ") AS MAIN " & _
            '    " LEFT JOIN (" & LEAVE_QRY & ") LEAVE ON MAIN.DEPARTMENT_CODE=LEAVE.DEPARTMENT_CODE order by MAIN.DEPARTMENT_CODE,MAIN.ISEARNING desc,MAIN.Group_Seq "
        Else

            Qry = "select EMP_CODE,Emp_Name,Joining_date,FATHERS_NAME,Designation_Desc as Designation,PF_NO,ESI_NO,BANK_ACC_NO,BANK_NAME, " & _
               " IFSC,DEPARTMENT_CODE,DEPARTMENT_NAME,PAY_HEAD_CODE,ISEARNING,Head_Type,Group_Seq,Seq,sum(Total) as Total,sum(PF_Count) as PF_Count,sum(ESI_Count) as ESI_Count,sum(Pension) as Pension," & _
               " sum(Comp_PF) as Comp_PF,sum(Comp_ESI) as Comp_ESI,sum(LWFER) as LWFER,sum(ACTUAL_AMOUNT) as ACTUAL_AMOUNT,SUM(Payable_Amount) AS Payable_Amount from (" & InnerQry & ") InnerTable " & _
               " GROUP BY EMP_CODE,Emp_Name,Joining_date,FATHERS_NAME,Designation_Desc,PF_NO,ESI_NO,BANK_ACC_NO,BANK_NAME, " & _
               " IFSC,DEPARTMENT_CODE,DEPARTMENT_NAME,PAY_HEAD_CODE,ISEARNING,Head_Type,Group_Seq,Seq "

            Qry = " SELECT MAIN.*,LEAVE.PAYPERIOD_DAYS,cast(LEAVE.PRESENT_DAYS as Float) as PRESENT_DAYS,cast(LEAVE.ABSENT_DAYS as Float) as ABSENT_DAYS,cast(LEAVE.HOLIDAY_DAYS as Float) as HOLIDAY_DAYS,cast(LEAVE.WEEKLY_OFF as Float) as WEEKLY_OFF, " & _
                " cast(LEAVE.PAYABLE_DAYS as Float) as PAYABLE_DAYS,cast(LEAVE.EL as Float) as EL,cast(LEAVE.CL as Float) as CL,cast(LEAVE.SL as Float) as SL,cast(LEAVE.CH as Float) as CH,cast(LEAVE.ML as Float) as ML,cast(LEAVE.OTHER as Float) as OTHER FROM (" & Qry & ") AS MAIN " & _
                " LEFT JOIN (" & LEAVE_QRY & ") LEAVE ON MAIN.EMP_CODE=LEAVE.EMP_CODE order by MAIN.DEPARTMENT_CODE,MAIN.EMP_CODE,MAIN.ISEARNING desc,MAIN.Group_Seq "
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

        If clsCommon.CompairString(ReportType, "Departmentwise") = CompairStringResult.Equal Then
            Qry = " select 0 as Serial_No,'' as Department_Code,'' as Department_Name,'' as Employees,'' as Earnings1,'' as Earnings2,'' as Earnings3," & _
              " '' as Earnings4,'' as Earnings5,'' as Earnings6,'' as Deductions1,'' as Deductions2,'' as Deductions3,'' as Deductions4, " & _
              " '' as Deductions5,'' as Deductions6,'' as EmployerShare1,'' as EmployerShare2, '' as Net_Payment"
        Else
            Qry = " select 0 as Serial_No,'' as Employees,'' as Department_Name,'' as EarningsRate1,'' as EarningsRate2,'' as EarningsRate3," & _
              " '' as EarningsRate4,'' as EarningsRate5,'' as EarningsRate6,'' as Attendance1,'' as Attendance2,'' as Earnings1,'' as Earnings2,'' as Earnings3," & _
              " '' as Earnings4,'' as Earnings5,'' as Earnings6,'' as Deductions1,'' as Deductions2,'' as Deductions3,'' as Deductions4, " & _
              " '' as Deductions5,'' as Deductions6,'' as EmployerShare1,'' as EmployerShare2, '' as Net_Payment"
        End If

        Dim dtFinal As DataTable
        dtFinal = clsDBFuncationality.GetDataTable(Qry)
        ReturnFinalDTWithCols(dtFinal, dtPH)
        Dim NewDept As String = ""
        Dim RunningDept As String = ""
        Dim OldDept As String = ""

        Dim EarningsRateTotal(5) As Decimal
        Dim EarningsTotal(5) As Decimal

        Dim DedTotal(5) As Decimal

        Dim Total_Total As Integer = 0
        Dim PF_Count_Total As Integer = 0
        Dim ESI_Count_Total As Integer = 0
        Dim Pension_Total As Decimal = 0
        Dim Comp_PF_Total As Decimal = 0
        Dim Comp_ESI_Total As Decimal = 0
        Dim LWF_Total As Decimal = 0
        'Dim GrandTotal As Decimal = 0.0
        For Each dr As DataRow In dt.Rows
            PF_Count_Total = PF_Count_Total + clsCommon.myCdbl(dr.Item("PF_Count"))
            ESI_Count_Total = ESI_Count_Total + clsCommon.myCdbl(dr.Item("ESI_Count"))
            Pension_Total = Pension_Total + clsCommon.myCdbl(dr.Item("Pension"))
            Comp_PF_Total = Comp_PF_Total + clsCommon.myCdbl(dr.Item("Comp_PF"))
            Comp_ESI_Total = Comp_ESI_Total + clsCommon.myCdbl(dr.Item("Comp_ESI"))
            LWF_Total = LWF_Total + clsCommon.myCdbl(dr.Item("LWFER"))

            If clsCommon.CompairString(dr.Item("Pay_Head_Code"), "Total") = CompairStringResult.Equal Then
            Else
                Total_Total = dr.Item("Total")
            End If


            If clsCommon.CompairString(ReportType, "Departmentwise") = CompairStringResult.Equal Then
                RunningDept = dr.Item("DEPARTMENT_CODE")
            Else
                RunningDept = dr.Item("EMP_CODE")
            End If

            If dt.Rows.IndexOf(dr) = 0 Then
                If clsCommon.CompairString(ReportType, "Departmentwise") = CompairStringResult.Equal Then
                    NewDept = dr.Item("DEPARTMENT_CODE")
                    OldDept = ""
                Else
                    NewDept = dr.Item("EMP_CODE")
                End If

                dtFinal.Rows.Add()
                If clsCommon.CompairString(ReportType, "Departmentwise") = CompairStringResult.Equal Then
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Serial_No") = dtFinal.Rows.Count
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Department_Name") = dr.Item("Department_Name")
                Else
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Serial_No") = dtFinal.Rows.Count & Environment.NewLine & dr.Item("EMP_CODE")
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Employees") = clsCommon.myCstr(dr.Item("EMP_NAME")) & Environment.NewLine & clsCommon.myCstr(dr.Item("FATHERS_NAME")) & Environment.NewLine & clsCommon.myCstr(dr.Item("Joining_date")) & Environment.NewLine & clsCommon.myCstr(dr.Item("Designation")) & Environment.NewLine & clsCommon.myCstr(dr.Item("PF_NO")) & Environment.NewLine & clsCommon.myCstr(dr.Item("ESI_NO")) & Environment.NewLine & clsCommon.myCstr(dr.Item("BANK_ACC_NO")) & Environment.NewLine & clsCommon.myCstr(dr.Item("Bank_Name")) & Environment.NewLine & clsCommon.myCstr(dr.Item("IFSC"))

                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Department_Name") = dr.Item("Department_Name")

                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Attendance1") = clsCommon.myCstr(dr.Item("PRESENT_DAYS")) & Environment.NewLine & clsCommon.myCstr(dr.Item("HOLIDAY_DAYS") + dr.Item("WEEKLY_OFF")) & Environment.NewLine & clsCommon.myCstr(dr.Item("CL")) & Environment.NewLine & clsCommon.myCstr(dr.Item("EL"))
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Attendance2") = clsCommon.myCstr(dr.Item("SL")) & Environment.NewLine & clsCommon.myCstr(dr.Item("CH")) & Environment.NewLine & clsCommon.myCstr(dr.Item("ABSENT_DAYS")) & Environment.NewLine & clsCommon.myCstr(dr.Item("PAYABLE_DAYS"))
                End If

                'dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Employees") = dr.Item("Total") & Environment.NewLine & PF_Count_Total & Environment.NewLine & ESI_Count_Total

                If dr.Item("ISEARNING") = True Then
                    updateEarningHeads(dtFinal, dr, EarningsTotal, EarningsRateTotal)
                Else
                    updateDeductionHeads(dtFinal, dr, DedTotal)
                End If
            ElseIf clsCommon.CompairString(RunningDept, NewDept) = CompairStringResult.Equal Then
                OldDept = NewDept
                If dr.Item("ISEARNING") = True Then
                    updateEarningHeads(dtFinal, dr, EarningsTotal, EarningsRateTotal)
                Else
                    updateDeductionHeads(dtFinal, dr, DedTotal)
                End If
            Else
                For intLoop As Integer = 0 To EarningsRateTotal.Length - 1
                    EarningsRateTotal(intLoop) = 0
                Next

                For intLoop As Integer = 0 To EarningsTotal.Length - 1
                    EarningsTotal(intLoop) = 0
                Next
                For intLoop As Integer = 0 To DedTotal.Length - 1
                    DedTotal(intLoop) = 0
                Next
                Total_Total = 0
                PF_Count_Total = 0
                ESI_Count_Total = 0
                Pension_Total = 0
                Comp_PF_Total = 0
                Comp_ESI_Total = 0
                LWF_Total = 0


                If clsCommon.CompairString(ReportType, "Departmentwise") = CompairStringResult.Equal Then
                    NewDept = dr.Item("DEPARTMENT_CODE")
                    OldDept = ""
                Else
                    NewDept = dr.Item("EMP_CODE")
                End If
                dtFinal.Rows.Add()

                If clsCommon.CompairString(ReportType, "Departmentwise") = CompairStringResult.Equal Then
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Serial_No") = dtFinal.Rows.Count
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Department_Name") = dr.Item("Department_Name")
                Else
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Serial_No") = dtFinal.Rows.Count & Environment.NewLine & dr.Item("EMP_CODE")
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Employees") = clsCommon.myCstr(dr.Item("EMP_NAME")) & Environment.NewLine & clsCommon.myCstr(dr.Item("FATHERS_NAME")) & Environment.NewLine & clsCommon.myCstr(dr.Item("Joining_date")) & Environment.NewLine & clsCommon.myCstr(dr.Item("Designation")) & Environment.NewLine & clsCommon.myCstr(dr.Item("PF_NO")) & Environment.NewLine & clsCommon.myCstr(dr.Item("ESI_NO")) & Environment.NewLine & clsCommon.myCstr(dr.Item("BANK_ACC_NO")) & Environment.NewLine & clsCommon.myCstr(dr.Item("Bank_Name")) & Environment.NewLine & clsCommon.myCstr(dr.Item("IFSC"))

                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Department_Name") = dr.Item("Department_Name")

                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Attendance1") = clsCommon.myCstr(dr.Item("PRESENT_DAYS")) & Environment.NewLine & clsCommon.myCstr(clsCommon.myCdbl(dr.Item("HOLIDAY_DAYS")) + clsCommon.myCdbl(dr.Item("WEEKLY_OFF"))) & Environment.NewLine & clsCommon.myCstr(dr.Item("CL")) & Environment.NewLine & clsCommon.myCstr(dr.Item("EL"))
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Attendance2") = clsCommon.myCstr(dr.Item("SL")) & Environment.NewLine & clsCommon.myCstr(dr.Item("CH")) & Environment.NewLine & clsCommon.myCstr(dr.Item("ABSENT_DAYS")) & Environment.NewLine & clsCommon.myCstr(dr.Item("PAYABLE_DAYS"))
                End If
                If dr.Item("ISEARNING") = True Then
                    updateEarningHeads(dtFinal, dr, EarningsTotal, EarningsRateTotal)
                Else
                    updateDeductionHeads(dtFinal, dr, DedTotal)
                End If
            End If

            If clsCommon.CompairString(ReportType, "Departmentwise") = CompairStringResult.Equal Then
                dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Employees") = Total_Total & Environment.NewLine & PF_Count_Total & Environment.NewLine & ESI_Count_Total
            Else

            End If
            dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EmployerShare1") = Pension_Total & Environment.NewLine & Comp_PF_Total & Environment.NewLine & Comp_ESI_Total
            dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EmployerShare2") = LWF_Total
            dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Net_Payment") = (EarningsTotal(0) + EarningsTotal(1) + EarningsTotal(2) + EarningsTotal(3) + EarningsTotal(4) + EarningsTotal(5) - DedTotal(0) - DedTotal(1) - DedTotal(2) - DedTotal(3) - DedTotal(4) - DedTotal(5))

        Next
        'For Each row As DataRow In dtFinal.Rows
        '    GrandTotal = GrandTotal + clsCommon.myCdbl(row.Item("Net_Payment"))
        'Next
        'Dim drGrand As DataRow = dtFinal.NewRow
        'drGrand(0) = "Total:"
        'drGrand(dtFinal.Columns.Count - 1) = GrandTotal
        'dtFinal.Rows.Add(drGrand)
        'Gv1.DataSource = Nothing
        'Gv1.DataSource = dtFinal
        Return dtFinal
    End Function
    Function GetFinalDtCR(ByVal Department_Name As String, ByVal dtFinalCRR As DataTable, dtFinal As DataTable) As DataTable
        'Dim dtFinalCR As DataTable
        'dtFinalCR = New DataTable
        Dim dr As DataRow = dtFinal.NewRow
        For Each col As DataColumn In dtFinal.Columns
            If dtFinalCRR.Rows.Count <= 0 Then
                dtFinalCRR.Columns.Add(col.ColumnName.ToString)
            End If
            dr(col.ColumnName) = col.Caption
        Next
        If clsCommon.myLen(Department_Name) > 0 Then
            dr("Department_Name") = Department_Name
        End If

        dtFinalCRR.Rows.Add(dr.ItemArray)
        'dtFinalCR.Merge(dtFinal)

        Return dtFinalCRR
    End Function
    Function updateEarningHeads(ByVal dtFinal As DataTable, ByVal dr As DataRow, ByVal EarningsTotal() As Decimal, ByVal EarningsRateTotal() As Decimal) As Decimal()
        If dr.Item("Seq") < (NoOfPayHeadsInEachCol - 1) Then
            If dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 1 Then
                dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings1") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings1") & Environment.NewLine & dr.Item("Actual_Amount")
                EarningsTotal(0) = EarningsTotal(0) + dr.Item("Actual_Amount")
                If clsCommon.CompairString(Me.ddlReportType.Text, "Employeewise") = CompairStringResult.Equal Then
                    'If clsCommon.CompairString(dr.Item("Head_Type"), "ATTN") = CompairStringResult.Equal Then
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate1") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate1") & Environment.NewLine & dr.Item("Payable_Amount")
                    EarningsRateTotal(0) = EarningsRateTotal(0) + dr.Item("Payable_Amount")
                    'End If
                End If

            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 2 Then
                dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings2") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings2") & Environment.NewLine & dr.Item("Actual_Amount")
                EarningsTotal(1) = EarningsTotal(1) + dr.Item("Actual_Amount")
                If clsCommon.CompairString(Me.ddlReportType.Text, "Employeewise") = CompairStringResult.Equal Then
                    'If clsCommon.CompairString(dr.Item("Head_Type"), "ATTN") = CompairStringResult.Equal Then
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate2") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate2") & Environment.NewLine & dr.Item("Payable_Amount")
                    EarningsRateTotal(1) = EarningsRateTotal(1) + dr.Item("Payable_Amount")
                    'End If
                End If

            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 3 Then
                dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings3") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings3") & Environment.NewLine & dr.Item("Actual_Amount")
                EarningsTotal(2) = EarningsTotal(2) + dr.Item("Actual_Amount")
                If clsCommon.CompairString(Me.ddlReportType.Text, "Employeewise") = CompairStringResult.Equal Then
                    'If clsCommon.CompairString(dr.Item("Head_Type"), "ATTN") = CompairStringResult.Equal Then
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate3") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate3") & Environment.NewLine & dr.Item("Payable_Amount")
                    EarningsRateTotal(2) = EarningsRateTotal(2) + dr.Item("Payable_Amount")
                    'End If
                End If

            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 4 Then
                dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings4") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings4") & Environment.NewLine & dr.Item("Actual_Amount")
                EarningsTotal(3) = EarningsTotal(3) + dr.Item("Actual_Amount")
                If clsCommon.CompairString(Me.ddlReportType.Text, "Employeewise") = CompairStringResult.Equal Then
                    'If clsCommon.CompairString(dr.Item("Head_Type"), "ATTN") = CompairStringResult.Equal Then
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate4") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate4") & Environment.NewLine & dr.Item("Payable_Amount")
                    EarningsRateTotal(3) = EarningsRateTotal(3) + dr.Item("Payable_Amount")
                    'End If
                End If

            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 5 Then
                dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings5") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings5") & Environment.NewLine & dr.Item("Actual_Amount")
                EarningsTotal(4) = EarningsTotal(4) + dr.Item("Actual_Amount")
                If clsCommon.CompairString(Me.ddlReportType.Text, "Employeewise") = CompairStringResult.Equal Then
                    'If clsCommon.CompairString(dr.Item("Head_Type"), "ATTN") = CompairStringResult.Equal Then
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate5") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate5") & Environment.NewLine & dr.Item("Payable_Amount")
                    EarningsRateTotal(4) = EarningsRateTotal(4) + dr.Item("Payable_Amount")
                    'End If
                End If

            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 6 Then
                dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings6") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings6") & Environment.NewLine & dr.Item("Actual_Amount")
                EarningsTotal(5) = EarningsTotal(5) + dr.Item("Actual_Amount")
                'If clsCommon.CompairString(dr.Item("Head_Type"), "ATTN") = CompairStringResult.Equal Then
                dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate6") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate6") & Environment.NewLine & dr.Item("Payable_Amount")
                EarningsRateTotal(5) = EarningsRateTotal(5) + dr.Item("Payable_Amount")
                'End If
            End If

        ElseIf dr.Item("Seq") = (NoOfPayHeadsInEachCol - 1) Then
            If dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 1 Then
                If dr.Item("Group_Seq") = EarningTotalColNo Then
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings1") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings1") & Environment.NewLine & (EarningsTotal(0) + EarningsTotal(1) + EarningsTotal(2) + EarningsTotal(3) + EarningsTotal(4) + EarningsTotal(5))
                Else
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings1") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings1") & Environment.NewLine & dr.Item("Actual_Amount")
                    EarningsTotal(0) = EarningsTotal(0) + dr.Item("Actual_Amount")
                End If
                If clsCommon.CompairString(Me.ddlReportType.Text, "Employeewise") = CompairStringResult.Equal Then
                    'If clsCommon.CompairString(dr.Item("Head_Type"), "ATTN") = CompairStringResult.Equal Then
                    If dr.Item("Group_Seq") = EarningRateTotalColNo Then
                        dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate1") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate1") & Environment.NewLine & (EarningsRateTotal(0) + EarningsRateTotal(1) + EarningsRateTotal(2) + EarningsRateTotal(3) + EarningsRateTotal(4) + EarningsRateTotal(5))
                    Else
                        dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate1") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate1") & Environment.NewLine & dr.Item("Payable_Amount")
                        EarningsRateTotal(0) = EarningsRateTotal(0) + dr.Item("Payable_Amount")
                    End If
                    'End If
                End If

            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 2 Then
                If dr.Item("Group_Seq") = EarningTotalColNo Then
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings2") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings2") & Environment.NewLine & (EarningsTotal(0) + EarningsTotal(1) + EarningsTotal(2) + EarningsTotal(3) + EarningsTotal(4) + EarningsTotal(5))
                Else
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings2") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings2") & Environment.NewLine & dr.Item("Actual_Amount")
                    EarningsTotal(1) = EarningsTotal(1) + dr.Item("Actual_Amount")
                End If
                If clsCommon.CompairString(Me.ddlReportType.Text, "Employeewise") = CompairStringResult.Equal Then
                    'If clsCommon.CompairString(dr.Item("Head_Type"), "ATTN") = CompairStringResult.Equal Then
                    If dr.Item("Group_Seq") = EarningRateTotalColNo Then
                        dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate2") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate2") & Environment.NewLine & (EarningsRateTotal(0) + EarningsRateTotal(1) + EarningsRateTotal(2) + EarningsRateTotal(3) + EarningsRateTotal(4) + EarningsRateTotal(5))
                    Else
                        dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate2") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate2") & Environment.NewLine & dr.Item("Payable_Amount")
                        EarningsRateTotal(1) = EarningsRateTotal(1) + dr.Item("Payable_Amount")
                    End If
                    'End If
                End If

            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 3 Then
                If dr.Item("Group_Seq") = EarningTotalColNo Then
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings3") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings3") & Environment.NewLine & (EarningsTotal(0) + EarningsTotal(1) + EarningsTotal(2) + EarningsTotal(3) + EarningsTotal(4) + EarningsTotal(5))
                Else
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings3") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings3") & Environment.NewLine & dr.Item("Actual_Amount")
                    EarningsTotal(2) = EarningsTotal(2) + dr.Item("Actual_Amount")
                End If
                If clsCommon.CompairString(Me.ddlReportType.Text, "Employeewise") = CompairStringResult.Equal Then
                    'If clsCommon.CompairString(dr.Item("Head_Type"), "ATTN") = CompairStringResult.Equal Then
                    If dr.Item("Group_Seq") = EarningRateTotalColNo Then
                        dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate3") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate3") & Environment.NewLine & (EarningsRateTotal(0) + EarningsRateTotal(1) + EarningsRateTotal(2) + EarningsRateTotal(3) + EarningsRateTotal(4) + EarningsRateTotal(5))
                    Else
                        dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate3") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate3") & Environment.NewLine & dr.Item("Payable_Amount")
                        EarningsRateTotal(2) = EarningsRateTotal(2) + dr.Item("Payable_Amount")
                    End If
                    'End If
                End If

            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 4 Then
                If dr.Item("Group_Seq") = EarningTotalColNo Then
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings4") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings4") & Environment.NewLine & (EarningsTotal(0) + EarningsTotal(1) + EarningsTotal(2) + EarningsTotal(3) + EarningsTotal(4) + EarningsTotal(5))
                Else
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings4") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings4") & Environment.NewLine & dr.Item("Actual_Amount")
                    EarningsTotal(3) = EarningsTotal(3) + dr.Item("Actual_Amount")
                End If
                If clsCommon.CompairString(Me.ddlReportType.Text, "Employeewise") = CompairStringResult.Equal Then
                    'If clsCommon.CompairString(dr.Item("Head_Type"), "ATTN") = CompairStringResult.Equal Then
                    If dr.Item("Group_Seq") = EarningRateTotalColNo Then
                        dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate4") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate4") & Environment.NewLine & (EarningsRateTotal(0) + EarningsRateTotal(1) + EarningsRateTotal(2) + EarningsRateTotal(3) + EarningsRateTotal(4) + EarningsRateTotal(5))
                    Else
                        dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate4") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate4") & Environment.NewLine & dr.Item("Payable_Amount")
                        EarningsRateTotal(3) = EarningsRateTotal(3) + dr.Item("Payable_Amount")
                    End If
                    'End If
                End If

            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 5 Then
                If dr.Item("Group_Seq") = EarningTotalColNo Then
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings5") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings5") & Environment.NewLine & (EarningsTotal(0) + EarningsTotal(1) + EarningsTotal(2) + EarningsTotal(3) + EarningsTotal(4) + EarningsTotal(5))
                Else
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings5") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings5") & Environment.NewLine & dr.Item("Actual_Amount")
                    EarningsTotal(4) = EarningsTotal(4) + dr.Item("Actual_Amount")
                End If
                If clsCommon.CompairString(Me.ddlReportType.Text, "Employeewise") = CompairStringResult.Equal Then
                    'If clsCommon.CompairString(dr.Item("Head_Type"), "ATTN") = CompairStringResult.Equal Then
                    If dr.Item("Group_Seq") = EarningRateTotalColNo Then
                        dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate5") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate5") & Environment.NewLine & (EarningsRateTotal(0) + EarningsRateTotal(1) + EarningsRateTotal(2) + EarningsRateTotal(3) + EarningsRateTotal(4) + EarningsRateTotal(5))
                    Else
                        dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate5") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate5") & Environment.NewLine & dr.Item("Payable_Amount")
                        EarningsRateTotal(4) = EarningsRateTotal(4) + dr.Item("Payable_Amount")
                    End If
                    'End If
                End If

            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 6 Then
                If dr.Item("Group_Seq") = EarningTotalColNo Then
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings6") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings6") & Environment.NewLine & (EarningsTotal(0) + EarningsTotal(1) + EarningsTotal(2) + EarningsTotal(3) + EarningsTotal(4) + EarningsTotal(5))
                Else
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings6") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Earnings6") & Environment.NewLine & dr.Item("Actual_Amount")
                    EarningsTotal(5) = EarningsTotal(5) + dr.Item("Actual_Amount")
                End If
                If clsCommon.CompairString(Me.ddlReportType.Text, "Employeewise") = CompairStringResult.Equal Then
                    'If clsCommon.CompairString(dr.Item("Head_Type"), "ATTN") = CompairStringResult.Equal Then
                    If dr.Item("Group_Seq") = EarningRateTotalColNo Then
                        dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate6") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate6") & Environment.NewLine & (EarningsRateTotal(0) + EarningsRateTotal(1) + EarningsRateTotal(2) + EarningsRateTotal(3) + EarningsRateTotal(4) + EarningsRateTotal(5))
                    Else
                        dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate6") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("EarningsRate6") & Environment.NewLine & dr.Item("Payable_Amount")
                        EarningsRateTotal(5) = EarningsRateTotal(5) + dr.Item("Payable_Amount")
                    End If
                    'End If
                End If

            End If
        End If
        Return EarningsTotal
    End Function
    Function updateDeductionHeads(ByVal dtFinal As DataTable, ByVal dr As DataRow, ByVal DedTotal() As Decimal) As Decimal()
        If dr.Item("Seq") < (NoOfPayHeadsInEachCol - 1) Then
            If dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 1 Then
                dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions1") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions1") & Environment.NewLine & dr.Item("Actual_Amount")
                DedTotal(0) = DedTotal(0) + dr.Item("Actual_Amount")
            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 2 Then
                dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions2") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions2") & Environment.NewLine & dr.Item("Actual_Amount")
                DedTotal(1) = DedTotal(1) + dr.Item("Actual_Amount")
            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 3 Then
                dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions3") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions3") & Environment.NewLine & dr.Item("Actual_Amount")
                DedTotal(2) = DedTotal(2) + dr.Item("Actual_Amount")
            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 4 Then
                dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions4") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions4") & Environment.NewLine & dr.Item("Actual_Amount")
                DedTotal(3) = DedTotal(3) + dr.Item("Actual_Amount")
            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 5 Then
                dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions5") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions5") & Environment.NewLine & dr.Item("Actual_Amount")
                DedTotal(4) = DedTotal(4) + dr.Item("Actual_Amount")
            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 6 Then
                dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions6") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions6") & Environment.NewLine & dr.Item("Actual_Amount")
                DedTotal(5) = DedTotal(5) + dr.Item("Actual_Amount")
            End If

        ElseIf dr.Item("Seq") = (NoOfPayHeadsInEachCol - 1) Then
            If dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 1 Then
                If dr.Item("Group_Seq") = DeductionTotalColNo Then
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions1") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions1") & Environment.NewLine & (DedTotal(0) + DedTotal(1) + DedTotal(2) + DedTotal(3) + DedTotal(4) + DedTotal(5))
                Else
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions1") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions1") & Environment.NewLine & dr.Item("Actual_Amount")
                    DedTotal(0) = DedTotal(0) + dr.Item("Actual_Amount")
                End If

            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 2 Then
                If dr.Item("Group_Seq") = DeductionTotalColNo Then
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions2") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions2") & Environment.NewLine & (DedTotal(0) + DedTotal(1) + DedTotal(2) + DedTotal(3) + DedTotal(4) + DedTotal(5))
                Else
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions2") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions2") & Environment.NewLine & dr.Item("Actual_Amount")
                    DedTotal(1) = DedTotal(1) + dr.Item("Actual_Amount")
                End If


            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 3 Then
                If dr.Item("Group_Seq") = DeductionTotalColNo Then
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions3") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions3") & Environment.NewLine & (DedTotal(0) + DedTotal(1) + DedTotal(2) + DedTotal(3) + DedTotal(4) + DedTotal(5))
                Else
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions3") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions3") & Environment.NewLine & dr.Item("Actual_Amount")
                    DedTotal(2) = DedTotal(2) + dr.Item("Actual_Amount")
                End If

            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 4 Then
                If dr.Item("Group_Seq") = DeductionTotalColNo Then
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions4") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions4") & Environment.NewLine & (DedTotal(0) + DedTotal(1) + DedTotal(2) + DedTotal(3) + DedTotal(4) + DedTotal(5))
                Else
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions4") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions4") & Environment.NewLine & dr.Item("Actual_Amount")
                    DedTotal(3) = DedTotal(3) + dr.Item("Actual_Amount")
                End If

            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 5 Then
                If dr.Item("Group_Seq") = DeductionTotalColNo Then
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions5") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions5") & Environment.NewLine & (DedTotal(0) + DedTotal(1) + DedTotal(2) + DedTotal(3) + DedTotal(4) + DedTotal(5))
                Else
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions5") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions5") & Environment.NewLine & dr.Item("Actual_Amount")
                    DedTotal(4) = DedTotal(4) + dr.Item("Actual_Amount")
                End If

            ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 6 Then
                If dr.Item("Group_Seq") = DeductionTotalColNo Then
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions6") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions6") & Environment.NewLine & (DedTotal(0) + DedTotal(1) + DedTotal(2) + DedTotal(3) + DedTotal(4) + DedTotal(5))
                Else
                    dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions6") = dtFinal.Rows(dtFinal.Rows.Count - 1).Item("Deductions6") & Environment.NewLine & dr.Item("Actual_Amount")
                    DedTotal(5) = DedTotal(5) + dr.Item("Actual_Amount")
                End If
            End If
        End If
        Return DedTotal
    End Function

    Function GetPayHeadDT() As DataTable
        Dim PHQuery As String = " SELECT DISTINCT  PAY_HEAD_CODE FROM TSPL_SALSTRUCT_PAYHEADS WHERE SALARY_STRUCTURE_CODE IN (select distinct SALARY_STRUCTURE_CODE from TSPL_GENERATE_SALARY_ATTENDANCE GSA INNER JOIN TSPL_GENERATE_SALARY GS ON GSA.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE WHERE GS.PAY_PERIOD_CODE='" & txtFromPP.Value & "') "
        PHQuery = " select Pay_Head_Name as Pay_Head_Code,IsEarning,Head_Type,(ROW_NUMBER() over (partition by IsEarning order by Group_Seq)) Group_Seq,(Group_Seq % " & NoOfPayHeadsInEachCol & ") as Seq from ( select PAY_HEAD_CODE,PAY_HEAD_NAME,ISEARNING,Head_Type, PRINT_GROUP_SEQ AS Group_Seq " & _
                  " from TSPL_PAYHEAD_MASTER) TSPL_PAYHEAD_MASTER WHERE PAY_HEAD_CODE IN (" & PHQuery & ")  ORDER BY IsEarning Desc,Group_Seq"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(PHQuery)

        Dim EarningCount As Decimal = 0
        Dim DedCount As Decimal = 0
        Dim EarningRateCount As Decimal = 0

        Dim arr() As DataRow
        arr = dt.Select("IsEarning=1") ''" union all " & _
        EarningRateCount = arr.Length

        arr = dt.Select("IsEarning=1")
        EarningCount = arr.Length

        arr = dt.Select("IsEarning=0")
        DedCount = arr.Length

        dt.Rows.Add()
        dt.Rows(dt.Rows.Count - 1).Item("Pay_Head_Code") = "Total"
        dt.Rows(dt.Rows.Count - 1).Item("IsEarning") = False
        dt.Rows(dt.Rows.Count - 1).Item("Head_Type") = ""
        dt.Rows(dt.Rows.Count - 1).Item("Group_Seq") = DedCount + 1
        dt.Rows(dt.Rows.Count - 1).Item("Seq") = (NoOfPayHeadsInEachCol - 1)
        DeductionTotalColNo = DedCount + 1

        dt.Rows.Add()
        dt.Rows(dt.Rows.Count - 1).Item("Pay_Head_Code") = "Total"
        dt.Rows(dt.Rows.Count - 1).Item("IsEarning") = True
        dt.Rows(dt.Rows.Count - 1).Item("Head_Type") = ""
        dt.Rows(dt.Rows.Count - 1).Item("Group_Seq") = EarningCount + 1
        dt.Rows(dt.Rows.Count - 1).Item("Seq") = (NoOfPayHeadsInEachCol - 1)
        EarningTotalColNo = EarningCount + 1

        'dt.Rows.Add()
        'dt.Rows(dt.Rows.Count - 1).Item("Pay_Head_Code") = "Total"
        'dt.Rows(dt.Rows.Count - 1).Item("IsEarning") = True
        'dt.Rows(dt.Rows.Count - 1).Item("Head_Type") = "ATTN"
        'dt.Rows(dt.Rows.Count - 1).Item("Group_Seq") = EarningRateCount + 1
        'dt.Rows(dt.Rows.Count - 1).Item("Seq") = 2
        EarningRateTotalColNo = EarningRateCount + 1
        Return dt
    End Function
    Function ReturnFinalDTWithCols(ByVal dtFinal As DataTable, ByVal PayHeadDt As DataTable) As DataTable

        dtFinal.Rows.Clear()
        dtFinal.Columns.Clear()
        Dim SerialCol As String = "Serial No"
        Dim DepartmentCol As String = "Department Name"
        Dim EmployeesCol As String = ""
        Dim AttendanceCol1 As String = ""
        Dim AttendanceCol2 As String = ""

        If clsCommon.CompairString(Me.ddlReportType.Text, "Departmentwise") = CompairStringResult.Equal Then
            SerialCol = "Serial No"
            EmployeesCol = "Total" & Environment.NewLine & "PF_Count" & Environment.NewLine & "ESI_Count"
        Else
            SerialCol = "Serial No" & Environment.NewLine & "Employee Code"
            EmployeesCol = "Employee Name" & Environment.NewLine & "F/H Name" & Environment.NewLine & "Date Of Joining" & Environment.NewLine & "Designation" & Environment.NewLine & "PF Number" & Environment.NewLine & "Insurance Number" & Environment.NewLine & "Bank Name" & Environment.NewLine & "IFSC"
            AttendanceCol1 = "W.D" & Environment.NewLine & "H.D" & Environment.NewLine & "C.L" & Environment.NewLine & "E.L"
            AttendanceCol2 = "S.L" & Environment.NewLine & "C.H" & Environment.NewLine & "W.P" & Environment.NewLine & "P.D"
        End If

        Dim EarningsRateCol1 As String = ""
        Dim EarningsRateCol2 As String = ""
        Dim EarningsRateCol3 As String = ""
        Dim EarningsRateCol4 As String = ""
        Dim EarningsRateCol5 As String = ""
        Dim EarningsRateCol6 As String = ""

        Dim EarningsCol1 As String = ""
        Dim EarningsCol2 As String = ""
        Dim EarningsCol3 As String = ""
        Dim EarningsCol4 As String = ""
        Dim EarningsCol5 As String = ""
        Dim EarningsCol6 As String = ""
        Dim DedCol1 As String = ""
        Dim DedCol2 As String = ""
        Dim DedCol3 As String = ""
        Dim DedCol4 As String = ""
        Dim DedCol5 As String = ""
        Dim DedCol6 As String = ""
        Dim EmployeeSahreCol1 As String = "Pension" & Environment.NewLine & "Difference" & Environment.NewLine & "E.S.I.C"
        Dim EmployeeSahreCol2 As String = "LWFER"
        Dim NetPaymentCol As String = "Net Payment"



        'dtFinal.Rows.Add()
        'dtFinal.Rows(0).Item("Serial_No") = 1
        'dtFinal.Rows(0).Item("Department_Code") = "Department_Code"
        'dtFinal.Rows(0).Item("Department_Name") = "Department_Name"
        'dtFinal.Rows(0).Item("Employees") = "Total" & Environment.NewLine & "PF_Count" & Environment.NewLine & "ESI_Count"
        'dtFinal.Rows(0).Item("EmployerShare1") = "Pension" & Environment.NewLine & "Difference" & Environment.NewLine & "E.SI.C"
        'dtFinal.Rows(0).Item("EmployerShare2") = "LWFER"
        'dtFinal.Rows(0).Item("Net_Payment") = "Net_Payment"
        For Each dr As DataRow In PayHeadDt.Rows
            If dr.Item("IsEarning") = True Then
                'If dr.Item("Seq") = 0 Then
                If dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 1 Then
                    EarningsCol1 = EarningsCol1 & Environment.NewLine & dr.Item("Pay_Head_Code")
                    If clsCommon.CompairString(Me.ddlReportType.Text, "Employeewise") = CompairStringResult.Equal Then
                        'If clsCommon.CompairString(dr.Item("Head_Type"), "ATTN") = CompairStringResult.Equal Then
                        EarningsRateCol1 = EarningsRateCol1 & Environment.NewLine & dr.Item("Pay_Head_Code")
                        'End If
                    End If

                ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 2 Then
                    EarningsCol2 = EarningsCol2 & Environment.NewLine & dr.Item("Pay_Head_Code")
                    If clsCommon.CompairString(Me.ddlReportType.Text, "Employeewise") = CompairStringResult.Equal Then
                        'If clsCommon.CompairString(dr.Item("Head_Type"), "ATTN") = CompairStringResult.Equal Then
                        EarningsRateCol2 = EarningsRateCol2 & Environment.NewLine & dr.Item("Pay_Head_Code")
                        'End If
                    End If

                ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 3 Then
                    EarningsCol3 = EarningsCol3 & Environment.NewLine & dr.Item("Pay_Head_Code")
                    If clsCommon.CompairString(Me.ddlReportType.Text, "Employeewise") = CompairStringResult.Equal Then
                        'If clsCommon.CompairString(dr.Item("Head_Type"), "ATTN") = CompairStringResult.Equal Then
                        EarningsRateCol3 = EarningsRateCol3 & Environment.NewLine & dr.Item("Pay_Head_Code")
                        'End If
                    End If
                ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 4 Then
                    EarningsCol4 = EarningsCol4 & Environment.NewLine & dr.Item("Pay_Head_Code")
                    If clsCommon.CompairString(Me.ddlReportType.Text, "Employeewise") = CompairStringResult.Equal Then
                        'If clsCommon.CompairString(dr.Item("Head_Type"), "ATTN") = CompairStringResult.Equal Then
                        EarningsRateCol4 = EarningsRateCol4 & Environment.NewLine & dr.Item("Pay_Head_Code")
                        'End If
                    End If

                ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 5 Then
                    EarningsCol5 = EarningsCol5 & Environment.NewLine & dr.Item("Pay_Head_Code")
                    If clsCommon.CompairString(Me.ddlReportType.Text, "Employeewise") = CompairStringResult.Equal Then
                        'If clsCommon.CompairString(dr.Item("Head_Type"), "ATTN") = CompairStringResult.Equal Then
                        EarningsRateCol5 = EarningsRateCol5 & Environment.NewLine & dr.Item("Pay_Head_Code")
                        'End If
                    End If
                ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 6 Then
                    EarningsCol6 = EarningsCol6 & Environment.NewLine & dr.Item("Pay_Head_Code")
                    If clsCommon.CompairString(Me.ddlReportType.Text, "Employeewise") = CompairStringResult.Equal Then
                        'If clsCommon.CompairString(dr.Item("Head_Type"), "ATTN") = CompairStringResult.Equal Then
                        EarningsRateCol6 = EarningsRateCol6 & Environment.NewLine & dr.Item("Pay_Head_Code")
                        'End If
                    End If
                End If

            Else
                'If dr.Item("Seq") = 0 Then
                If dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 1 Then
                    DedCol1 = DedCol1 & Environment.NewLine & dr.Item("Pay_Head_Code")
                ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 2 Then
                    DedCol2 = DedCol2 & Environment.NewLine & dr.Item("Pay_Head_Code")
                ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 3 Then
                    DedCol3 = DedCol3 & Environment.NewLine & dr.Item("Pay_Head_Code")
                ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 4 Then
                    DedCol4 = DedCol4 & Environment.NewLine & dr.Item("Pay_Head_Code")
                ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 5 Then
                    DedCol5 = DedCol5 & Environment.NewLine & dr.Item("Pay_Head_Code")
                ElseIf dr.Item("Group_Seq") <= (NoOfPayHeadsInEachCol) * 6 Then
                    DedCol6 = DedCol6 & Environment.NewLine & dr.Item("Pay_Head_Code")
                End If

            End If
        Next
        '' ad serial column
        Dim dcSerialCol As New DataColumn
        dcSerialCol.ColumnName = "Serial_No"
        dcSerialCol.Caption = SerialCol
        dtFinal.Columns.Add(dcSerialCol)

        'If clsCommon.CompairString(ddlReportType.Text, "Departmentwise") = CompairStringResult.Equal Then
        '' add department cols
        Dim dcDeptCol As New DataColumn
        dcDeptCol.ColumnName = "Department_Name"
        dcDeptCol.Caption = DepartmentCol
        dtFinal.Columns.Add(dcDeptCol)
        'End If


        '' add employee cols
        Dim dcEmpCol As New DataColumn
        dcEmpCol.ColumnName = "Employees"
        dcEmpCol.Caption = EmployeesCol
        dtFinal.Columns.Add(dcEmpCol)


        '' adding earning Rate cols
        '' add Earnings1 cols
        If clsCommon.myLen(EarningsRateCol1) > 0 Then
            Dim dcEarningsRate1Col As New DataColumn
            dcEarningsRate1Col.ColumnName = "EarningsRate1"
            dcEarningsRate1Col.Caption = EarningsRateCol1
            dtFinal.Columns.Add(dcEarningsRate1Col)
        End If


        '' add EarningsRate2 cols
        If clsCommon.myLen(EarningsRateCol2) > 0 Then
            Dim dcEarningsRate2Col As New DataColumn
            dcEarningsRate2Col.ColumnName = "EarningsRate2"
            dcEarningsRate2Col.Caption = EarningsRateCol2
            dtFinal.Columns.Add(dcEarningsRate2Col)
        End If

        '' add EarningsRate3 cols
        If clsCommon.myLen(EarningsRateCol3) > 0 Then
            Dim dcEarningsRate3Col As New DataColumn
            dcEarningsRate3Col.ColumnName = "EarningsRate3"
            dcEarningsRate3Col.Caption = EarningsRateCol3
            dtFinal.Columns.Add(dcEarningsRate3Col)
        End If

        '' add EarningsRate4 cols
        If clsCommon.myLen(EarningsRateCol4) > 0 Then
            Dim dcEarningsRate4Col As New DataColumn
            dcEarningsRate4Col.ColumnName = "EarningsRate4"
            dcEarningsRate4Col.Caption = EarningsRateCol4
            dtFinal.Columns.Add(dcEarningsRate4Col)
        End If

        '' add EarningsRate5 cols
        If clsCommon.myLen(EarningsRateCol5) > 0 Then
            Dim dcEarningsRate5Col As New DataColumn
            dcEarningsRate5Col.ColumnName = "EarningsRate5"
            dcEarningsRate5Col.Caption = EarningsRateCol5
            dtFinal.Columns.Add(dcEarningsRate5Col)
        End If

        '' add EarningsRate6 cols
        If clsCommon.myLen(EarningsRateCol6) > 0 Then
            Dim dcEarningsRate6Col As New DataColumn
            dcEarningsRate6Col.ColumnName = "EarningsRate6"
            dcEarningsRate6Col.Caption = EarningsRateCol6
            dtFinal.Columns.Add(dcEarningsRate6Col)
        End If

        If clsCommon.CompairString(ddlReportType.Text, "Employeewise") = CompairStringResult.Equal Then
            '' add department cols
            Dim dcAttendance1Col As New DataColumn
            dcAttendance1Col.ColumnName = "Attendance1"
            dcAttendance1Col.Caption = AttendanceCol1
            dtFinal.Columns.Add(dcAttendance1Col)

            Dim dcAttendance2Col As New DataColumn
            dcAttendance2Col.ColumnName = "Attendance2"
            dcAttendance2Col.Caption = AttendanceCol2
            dtFinal.Columns.Add(dcAttendance2Col)
        End If

        '' add Earnings1 cols
        If clsCommon.myLen(EarningsCol1) > 0 Then
            Dim dcEarnings1Col As New DataColumn
            dcEarnings1Col.ColumnName = "Earnings1"
            dcEarnings1Col.Caption = EarningsCol1
            dtFinal.Columns.Add(dcEarnings1Col)
        End If


        '' add Earnings2 cols
        If clsCommon.myLen(EarningsCol2) > 0 Then
            Dim dcEarnings2Col As New DataColumn
            dcEarnings2Col.ColumnName = "Earnings2"
            dcEarnings2Col.Caption = EarningsCol2
            dtFinal.Columns.Add(dcEarnings2Col)
        End If

        '' add Earnings3 cols
        If clsCommon.myLen(EarningsCol3) > 0 Then
            Dim dcEarnings3Col As New DataColumn
            dcEarnings3Col.ColumnName = "Earnings3"
            dcEarnings3Col.Caption = EarningsCol3
            dtFinal.Columns.Add(dcEarnings3Col)
        End If

        '' add Earnings4 cols
        If clsCommon.myLen(EarningsCol4) > 0 Then
            Dim dcEarnings4Col As New DataColumn
            dcEarnings4Col.ColumnName = "Earnings4"
            dcEarnings4Col.Caption = EarningsCol4
            dtFinal.Columns.Add(dcEarnings4Col)
        End If

        '' add Earnings5 cols
        If clsCommon.myLen(EarningsCol5) > 0 Then
            Dim dcEarnings5Col As New DataColumn
            dcEarnings5Col.ColumnName = "Earnings5"
            dcEarnings5Col.Caption = EarningsCol5
            dtFinal.Columns.Add(dcEarnings5Col)
        End If

        '' add Earnings6 cols
        If clsCommon.myLen(EarningsCol6) > 0 Then
            Dim dcEarnings6Col As New DataColumn
            dcEarnings6Col.ColumnName = "Earnings6"
            dcEarnings6Col.Caption = EarningsCol6
            dtFinal.Columns.Add(dcEarnings6Col)
        End If

        '' add deduction1 cols
        If clsCommon.myLen(DedCol1) > 0 Then
            Dim dcDed1Col As New DataColumn
            dcDed1Col.ColumnName = "Deductions1"
            dcDed1Col.Caption = DedCol1
            dtFinal.Columns.Add(dcDed1Col)
        End If

        '' add deduction2 cols
        If clsCommon.myLen(DedCol2) > 0 Then
            Dim dcDed2Col As New DataColumn
            dcDed2Col.ColumnName = "Deductions2"
            dcDed2Col.Caption = DedCol2
            dtFinal.Columns.Add(dcDed2Col)
        End If

        '' add deduction1 cols
        If clsCommon.myLen(DedCol3) > 0 Then
            Dim dcDed3Col As New DataColumn
            dcDed3Col.ColumnName = "Deductions3"
            dcDed3Col.Caption = DedCol3
            dtFinal.Columns.Add(dcDed3Col)
        End If

        '' add deduction4 cols
        If clsCommon.myLen(DedCol4) > 0 Then
            Dim dcDed4Col As New DataColumn
            dcDed4Col.ColumnName = "Deductions4"
            dcDed4Col.Caption = DedCol4
            dtFinal.Columns.Add(dcDed4Col)
        End If

        '' add deduction5 cols
        If clsCommon.myLen(DedCol5) > 0 Then
            Dim dcDed5Col As New DataColumn
            dcDed5Col.ColumnName = "Deductions5"
            dcDed5Col.Caption = DedCol5
            dtFinal.Columns.Add(dcDed5Col)
        End If

        If clsCommon.myLen(DedCol6) > 0 Then
            Dim dcDed6Col As New DataColumn
            dcDed6Col.ColumnName = "Deductions6"
            dcDed6Col.Caption = DedCol6
            dtFinal.Columns.Add(dcDed6Col)
        End If

        '' add dcEmployeeSahreCol1 cols
        Dim dcEmployeeSahreCol1 As New DataColumn
        dcEmployeeSahreCol1.ColumnName = "EmployerShare1"
        dcEmployeeSahreCol1.Caption = EmployeeSahreCol1
        dtFinal.Columns.Add(dcEmployeeSahreCol1)

        '' add dcEmployeeSahreCol2 cols
        Dim dcEmployeeSahreCol2 As New DataColumn
        dcEmployeeSahreCol2.ColumnName = "EmployerShare2"
        dcEmployeeSahreCol2.Caption = EmployeeSahreCol2
        dtFinal.Columns.Add(dcEmployeeSahreCol2)

        '' add netpayment cols
        Dim dcNetPaymentCol As New DataColumn
        dcNetPaymentCol.ColumnName = "Net_Payment"
        dcNetPaymentCol.Caption = NetPaymentCol
        dtFinal.Columns.Add(dcNetPaymentCol)


        Return dtFinal
    End Function

    Private Sub lblPrintFormat2_Click(sender As Object, e As EventArgs)
        Print = False
        PrintData()
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        'Dim arr As New List(Of String)()
        'arr.Add(objCommonVar.CurrentCompanyName)
        ''arr.Add(objCommonVar.CurrentCompanyName & ": " & GetLocationMult())
        'arr.Add(" Pay Period: " + txtFromPP.Value)
        'If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
        '    arr.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMult.arrDispalyMember))
        'End If
        'arr.Add("Salary/Wages Register of the Month : " & txtFromPP.Value & " on date :" + clsCommon.GETSERVERDATE() + " ")
        'arr.Add("Firm PF No :" & Comp_PF_No & " ")
        'arr.Add("Firm ESI No :" & Comp_ESI_No & " ")
        ''clsCommon.MyExportToExcel("Salary Register", gv1, arr, "Salary Sheet")
        'If Gv1.Rows.Count <= 0 Then
        '    Gv1.Focus()
        '    clsCommon.MyMessageBoxShow("Data not found.")
        'Else
        '    clsCommon.MyExportToExcelGrid("Salary Sheet", Gv1, arr, "Salary Sheet", False)
        'End If
        ExportGrid(EnumExportTo.Excel)
    End Sub
    Function GetLocationMult() As String
        Dim qry As String = ""
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            qry = "select ','+ Location_Desc +'' from TSPL_LOCATION_MASTER where Location_Code in (" & clsCommon.GetMulcallString(txtLocationMult.arrValueMember) & ") for xml path('')"
        Else
            qry = "select ','+ Location_Desc +'' from TSPL_LOCATION_MASTER  for xml path('')"
        End If

        Dim strLocation As String = clsDBFuncationality.getSingleValue(qry)
        strLocation = strLocation.Remove(0, 1)
        If clsCommon.myLen(strLocation) <= 0 Then
            strLocation = "All"
        End If
        Return strLocation
    End Function
    Sub GetCompanyPFESI_Numbers() ''-----As String
        Dim qry As String = "select coalesce(Comp_PF_NO,'') as Comp_PF_NO ,coalesce(Comp_ESIC_NO,'') as Comp_ESIC_NO from TSPL_COMPANY_MASTER where Comp_Code='" & objCommonVar.CurrentCompanyCode & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Comp_PF_No = dt.Rows(0).Item("Comp_PF_NO")
            Comp_ESI_No = dt.Rows(0).Item("Comp_ESIC_NO")
        End If
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        'Dim arr As New List(Of String)()
        'arr.Add(objCommonVar.CurrentCompanyName & ": " & GetLocationMult())
        'arr.Add("Salary/Wages Register of the Month : " & txtFromPP.Value & " on date :" + clsCommon.GETSERVERDATE() + " ")
        'arr.Add("Firm PF No :" & Comp_PF_No & " ")
        'arr.Add("Firm ESI No :" & Comp_ESI_No & " ")
        'clsCommon.MyExportToPDF("Salary Sheet", Gv1, arr, "Salary Sheet", True)
        ExportGrid(EnumExportTo.PDF)
    End Sub
    Private Sub txtLocationMult_My_Click(sender As Object, e As EventArgs) Handles txtLocationMult._My_Click
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where LOCATION_CODE IN (select DISTINCT LOCATION_CODE from TSPL_GENERATE_SALARY where PAY_PERIOD_CODE='" & txtFromPP.Value & "') "
        txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
    End Sub

    Private Sub txtDepartmentMult__My_Click(sender As Object, e As EventArgs) Handles txtDepartmentMult._My_Click
        Dim qry As String = " select DEPARTMENT_CODE as Code,DEPARTMENT_NAME as [Name] from TSPL_DEPARTMENT_MASTER  "
        txtDepartmentMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DeptMulSel", qry, "Code", "Name", txtDepartmentMult.arrValueMember, txtDepartmentMult.arrDispalyMember)
    End Sub

    Private Sub txtPayHeadMult__My_Click(sender As Object, e As EventArgs) Handles txtPayHeadMult._My_Click
        Dim qry As String = " select PAY_HEAD_CODE as Code,PAY_HEAD_NAME as [Name] from TSPL_PAYHEAD_MASTER order by IsEarning desc ,Group_Seq  "
        txtPayHeadMult.arrValueMember = clsCommon.ShowMultipleSelectForm("PHtMulSel", qry, "Code", "Name", txtPayHeadMult.arrValueMember, txtPayHeadMult.arrDispalyMember)
    End Sub
    Private Sub txtDivisionMult_My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
    End Sub

    Private Sub ddlReportType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddlReportType.SelectedIndexChanged
        If clsCommon.CompairString(ddlReportType.Text, "Employeewise") = CompairStringResult.Equal Then
            Me.txtNum.Value = 4
        Else
            Me.txtNum.Value = 3
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        Print = False
        PrintData()
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                'arrHeader.Add("Date :" + clsCommon.GETSERVERDATE() + " ")
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmDepartmentwiseSalarySheetRpt & "'"))
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("From Pay Period: " + txtFromPP.Value))
                arrHeader.Add("Firm PF No :" & Comp_PF_No & " ")
                arrHeader.Add("Firm ESI No :" & Comp_ESI_No & " ")
                If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMult.arrDispalyMember))
                End If
                If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                    arrHeader.Add("Division : " + clsCommon.GetMulcallStringWithComma(txtDivisionMult.arrDispalyMember))
                End If
                If txtDepartmentMult.arrValueMember IsNot Nothing AndAlso txtDepartmentMult.arrValueMember.Count > 0 Then
                    arrHeader.Add("Department : " + clsCommon.GetMulcallStringWithComma(txtDepartmentMult.arrDispalyMember))
                End If
                If txtPayHeadMult.arrValueMember IsNot Nothing AndAlso txtPayHeadMult.arrValueMember.Count > 0 Then
                    arrHeader.Add("Pay Head : " + clsCommon.GetMulcallStringWithComma(txtPayHeadMult.arrDispalyMember))
                End If
                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                    transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                Else
                    clsCommon.MyExportToPDF("Salary Sheet", Gv1, arrHeader, "Salary Sheet", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
