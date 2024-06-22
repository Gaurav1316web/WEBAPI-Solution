Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
'===============================CREATED BY PREETI GUPTA====================
Public Class RptPayrollPerformaforcontribution
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim Print As Boolean = True
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptPerformaForContributiondetail)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnPrint.Visible = MyBase.isModifyFlag
    End Sub
#End Region

    Private Sub RptPayrollPerformaforcontribution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        txtfromDate.Value = clsCommon.GETSERVERDATE()
        txtTodate.Value = clsCommon.GETSERVERDATE()
    End Sub

    



    Private Sub FndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndLocationCode._MYValidating
      
        FndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.FndLocationCode.Value, isButtonClicked)
        If clsCommon.myLen(FndLocationCode.Value) > 0 Then
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & FndLocationCode.Value & "'")
        Else
            lblLocationName.Text = ""
        End If

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        'If clsCommon.myLen(FndLocationCode.Value) <= 0 Then
        '    myMessages.blankValue("Please select Location")
        'Else
        funPrint(FndLocationCode.Value)
        'End If


    End Sub
    Public Sub funPrint(ByVal strDocNo As String)

        'Dim FromDate As String = clsCommon.myCDate(txtfromDate.Value, "dd/MMM/YYYY")
        'Dim ToDate As String = clsCommon.myCDate(txtTodate.Value, "dd/MMM/yyyy")
        Dim FromDate As String = txtfromDate.Value.Month & "/" & txtfromDate.Value.Year
        Dim ToDate As String = txtTodate.Value.Month & "/" & txtfromDate.Value.Year
        Dim CompCode As String = clsDBFuncationality.getSingleValue("select TSPL_COMPANY_MASTER.Comp_Code from tspl_company_master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' ")

        Dim CompName As String = clsDBFuncationality.getSingleValue("select TSPL_COMPANY_MASTER.Comp_Name from tspl_company_master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' ")
        Dim CompanyAdress As String = clsDBFuncationality.getSingleValue(" select  TSPL_COMPANY_MASTER.Add1+Case When ISNULL(TSPL_COMPANY_MASTER.Add2,'')='' Then '' else ', '+TSPL_COMPANY_MASTER.Add2+ Case When ISNULL(TSPL_COMPANY_MASTER.Add3,'')='' Then '' Else ', '+TSPL_COMPANY_MASTER.Add3+ Case When ISNULL(TSPL_COMPANY_MASTER.City_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_COMPANY_MASTER.City_Code) End End End as Comp_Address from tspl_company_master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' ")

        Try
            Dim qry As String = " select '" + CompCode + "' as Comp_Code,'" + CompName + "' as Comp_Name,'" + CompanyAdress + "' as Comp_Address,* ,'" + (FromDate) + "' as FromDate,'" + (ToDate) + "' as ToDate,(isnull([January],0)+isnull([February],0)+isnull([March],0)+isnull([April],0)+isnull([May],0)+isnull([June],0)+isnull([July],0)+isnull([August],0)+isnull([September],0)+isnull( [October],0)+isnull( [November],0)+isnull([December],0)) as total_Employee_share from(select distinct "
            qry += "TSPL_LOCATION_MASTER.Location_Code ,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+"
            qry += "  Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_STATE_MASTER.State_Name ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_STATE_MASTER.State_Name) End End End as Loc_Address,TSPL_EMPLOYEE_MASTER.EMP_CODE ,TSPL_EMPLOYEE_MASTER.Emp_Name ,TSPL_EMPLOYEE_MASTER.FATHERS_NAME ,TSPL_EMPLOYEE_MASTER.Designation ,TSPL_DESIGNATION_MASTER.Designation_Desc ,TSPL_EMPLOYEE_MASTER.Joining_date ,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE ,TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT ,datepart(dd,TSPL_PAYPERIOD_MASTER.DATE_FROM) as Datewise,datename(month,TSPL_PAYPERIOD_MASTER.DATE_FROM) as Monthwise ,datepart(yy,TSPL_PAYPERIOD_MASTER.DATE_FROM) as Yearwise "
            qry += "   from TSPL_GENERATE_SALARY_PAYHEADS"
            qry += " left outer join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE "
            qry += " left outer join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE =TSPL_GENERATE_SALARY.PAY_PERIOD_CODE "
            qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE "
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_GENERATE_SALARY.LOCATION_CODE "
            qry += " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER .State_Code =TSPL_LOCATION_MASTER.State "
            qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER  .Comp_Code =TSPL_LOCATION_MASTER.Comp_code "
            qry += " left join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER .Designation_id =TSPL_EMPLOYEE_MASTER.Designation "
            qry += " left join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE =TSPL_Employee_Master.DEVISION_CODE"
            qry += " where"
            qry += " TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE ='LWF'  and "
            qry += "  convert(date,TSPL_PAYPERIOD_MASTER.DATE_FROM,103)>=convert(date,('" + txtfromDate.Value + "'),103) and convert(date,TSPL_PAYPERIOD_MASTER.DATE_FROM,103) <=convert(date,('" + txtTodate.Value + "'),103) "
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_LOCATION_MASTER .Location_Code  in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
            End If
            If txtMultDivision.arrValueMember IsNot Nothing AndAlso txtMultDivision.arrValueMember.Count > 0 Then
                qry += " and TSPL_DEVISION_MASTER.DEVISION_CODE  in (" + clsCommon.GetMulcallString(txtMultDivision.arrValueMember) + ") "
            End If

            qry += " ) as S"
            qry += " Pivot(max(ACTUAL_AMOUNT) FOR [Monthwise] IN ([January],[February],[March],[April],[May],[June],[July],[August],[September], [October], [November],[December]) ) as u"


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt.Rows.Count > 0 Then

                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dt, "crptPerormaForContributiondetails", "Performa for Contribution details")
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)

            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RptPayrollPerformaforcontribution_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical' "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Dim frmpending As New FrmPendingRequisitionQty()
        frmpending.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtMultDivision__My_Click(sender As Object, e As EventArgs) Handles txtMultDivision._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtMultDivision.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtMultDivision.arrValueMember, txtMultDivision.arrDispalyMember)
    End Sub
End Class
