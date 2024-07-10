Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls

'=============shivani Tyagi===================='against[BM00000007881]
Public Class RptActurialValuation
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim qry1 As Object
    Dim btnPrint As Boolean = False

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptActurialValuation)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnRefresh.Visible = MyBase.isModifyFlag
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Public Function Opening()
        Dim qry1 As String = ""
        Dim qry As String = "select PAY_PERIOD_CODE from TSPL_PAYPERIOD_MASTER where convert(date,date_from  ,103)>= convert(date,'" & dtpfromdate1.Value & "',103) and convert(date,DATE_TO ,103)<= convert(date,'" & todate.Value & "',103) ORDER BY date_from"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            qry1 += "select MAX('" & clsCommon.myCstr(dt.Rows(0).Item("PAY_PERIOD_CODE")) & "') AS PayPeriod,SUM(OPENING) AS OPENING,fun.EMP_CODE  from TSPL_FUN_LEAVE_STATUS('" & clsCommon.myCstr(dt.Rows(0).Item("PAY_PERIOD_CODE")) & "') as fun left join tspl_employee_master on tspl_employee_master.EMP_CODE =fun .EMP_CODE left join tspl_location_master on tspl_location_master.location_code=tspl_employee_master.LOCATION_CODE group by fun.EMP_CODE "
        End If
        Return qry1
    End Function
    Sub LoadData()
        Try
            If dtpfromdate1.Value > todate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
                dtpfromdate1.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(fndLeaveCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Select Leave Code.", Me.Text)
                fndLeaveCode.Focus()
                Exit Sub
            End If
            Dim datemonthdiff As Integer = clsDBFuncationality.getSingleValue("	SELECT DATEDIFF(MONTH,convert(date,'" & dtpfromdate1.Value & "',103),convert(date,'" & todate.Value & "',103)) ")
            If datemonthdiff <= 12 And datemonthdiff >= 0 Then
                Dim strSelect As String
                strSelect = Opening()
                If strSelect = "" Then
                    clsCommon.MyMessageBoxShow(Me, "Their is no Pay Period Exist in this Date range", Me.Text)
                    Exit Sub
                End If
                Dim DivisionFirstTime As Integer = 0
                Dim DivAddress As String = ""
                If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count = 1 Then
                    DivisionFirstTime += 1
                    DivAddress = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DEVISION_NAME  from TSPL_DEVISION_MASTER  WHERE DEVISION_CODE in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ")"))
                    DivAddress = objCommonVar.CurrentCompanyName & " - " & DivAddress
                Else
                    DivAddress = objCommonVar.CurrentCompanyName
                End If
                'Dim Innerqry As String = "select EMP_CODE  as [Emp Code],Emp_Name AS [Emp Name],Birth_date as [DOB], AprilWD, MayWD, JuneWD , JulyWD, AugustWD, SeptemberWD, OctoberWD, NovemberWD , DecemberWD, JanuaryWD, FebruaryWD,  MarchWD,(AprilWD+MayWD+JuneWD+JulyWD+AugustWD+SeptemberWD+OctoberWD+NovemberWD+JanuaryWD+FebruaryWD+MarchWD+DecemberWD)as TotalWD," & _
                '                    " AprilHD, MayHD, JuneHD , JulyHD, AugustHD, SeptemberHD, OctoberHD, NovemberHD , DecemberHD, JanuaryHD, FebruaryHD,  MarchHD,(AprilHD+MayHD+JuneHD+JulyHD+AugustHD+SeptemberHD+OctoberHD+NovemberHD+JanuaryHD+FebruaryHD+MarchHD+DecemberHD)as TotalHD," & _
                '                    " (AprilELA+AprilCLA+AprilSLA+AprilCHA+ AprilMLA+AprilOtherA) as April,(MayELA+MayCLA+ MayCHA+MaySLA+ MayOtherA)as May,(JuneELA+JuneCLA+JuneSLA+JuneCHA+JuneMLA+JuneOtherA) as June," & _
                '                    " (JulyELA+JulyCLA+JulySLA+JulyCHA+JulyMLA+JulyOtherA) as July,(AugustELA+AugustCLA+AugustSLA+AugustCHA+AugustMLA+AugustOtherA) as August,(SeptemberELA+SeptemberCLA+SeptemberSLA+SeptemberCHA+SeptemberMLA+SeptemberOtherA)as September,(OctoberELA+OctoberCLA+OctoberSLA+OctoberCHA+OctoberMLA+OctoberOtherA) as October,(NovemberELA+NovemberCLA+NovemberSLA+NovemberCHA+NovemberMLA+NovemberOtherA) as November,(DecemberELA+DecemberCLA+DecemberCHA+DecemberSLA+DecemberMLA+DecemberOtherA) as December,(JanuaryELA+JanuaryCLA+JanuarySLA+JanuaryCHA+JanuaryMLA+JanuaryOtherA) as January,(FebruaryELA+FebruarySLA+FebruaryCLA+FebruaryCHA+FebruaryMLA+FebruaryOtherA) as February,(MarchELA+MarchSLA+MarchCLA+MarchCHA+MarchMLA+MarchOtherA) as March" & _
                '                    " , AprilEL, MayEL, JuneEL , JulyEL, AugustEL, SeptemberEL, OctoberEL, NovemberEL , DecemberEL, JanuaryEL, FebruaryEL,  MarchEL,(AprilEL+MayEL+JuneEL+JulyEL+AugustEL+SeptemberEL+OctoberEL+NovemberEL+JanuaryEL+FebruaryEL+MarchEL+DecemberEL)as TotalEL, AprilCL, MayCL, JuneCL, JulyCL , AugustCL , SeptemberCL, OctoberCL, NovemberCL, DecemberCL, JanuaryCL,  FebruaryCL,  MarchCL,(AprilCL+MayCL+JuneCL+JulyCL+AugustCL+SeptemberCL+OctoberCL+NovemberCL+JanuaryCL+FebruaryCL+ MarchCL+DecemberCL)as TotalCL, AprilSL, MaySL, JuneSL, JulySL, AugustSL , SeptemberSL, OctoberSL, NovemberSL, DecemberSL, JanuarySL,  FebruarySL, MarchSL,(AprilSL+MaySL+JuneSL+JulySL+AugustSL+SeptemberSL+OctoberSL+NovemberSL+DecemberSL+JanuarySL+ FebruarySL+ MarchSL)as TotalSL, AprilCH, MayCH, JuneCH, JulyCH, AugustCH, SeptemberCH, OctoberCH, NovemberCH , JanuaryCH, FebruaryCH, MarchCH, DecemberCH,(AprilCH+MayCH+JuneCH+JulyCH+AugustCH+SeptemberCH+OctoberCH+NovemberCH+JanuaryCH+ FebruaryCH+ MarchCH+DecemberCH)as TotalCH, AprilML, MayML , JuneML, JulyML, AugustML, SeptemberML, OctoberML, NovemberML, DecemberML, JanuaryML ,  FebruaryML,  MarchML,(AprilML+MayML+JuneML+JulyML+AugustML+SeptemberML+OctoberML+NovemberML+JanuaryML+FebruaryML+ MarchML+DecemberML)as TotalML, AprilOther, MayOther, JuneOther, JulyOther, AugustOther, SeptemberOther, OctoberOther , NovemberOther, DecemberOther, JanuaryOther,  FebruaryOther, MarchOther,(AprilOther+MayOther+JuneOther+JulyOther+AugustOther+SeptemberOther+OctoberOther+NovemberOther+JanuaryOther+ FebruaryOther+ MarchOther+DecemberOther) as TotalOther,Joining_date , Location_Desc , DEVISION_NAME from (select EMP_CODE,max(Emp_Name) as Emp_Name,sum(isnull(AprilEL,0)) as AprilEL,sum(isnull(MayEL,0)) as MayEL,sum(isnull(JuneEL,0)) as JuneEL ,sum(isnull(JulyEL,0)) as JulyEL,sum(isnull(AugustEL,0)) as AugustEL,sum(isnull(SeptemberEL,0)) as SeptemberEL,sum(isnull(OctoberEL,0)) as OctoberEL,sum(isnull(NovemberEL,0)) as NovemberEL ,sum(isnull(JanuaryEL,0)) as JanuaryEL,sum( isnull(FebruaryEL,0)) as FebruaryEL, sum(isnull(MarchEL,0)) as MarchEL,sum(isnull(DecemberEL,0)) as DecemberEL,sum(isnull(AprilCL,0)) as AprilCL,sum(isnull(MayCL,0)) as MayCL,sum(isnull(JuneCL,0)) as JuneCL,sum(isnull(JulyCL,0)) as JulyCL ,sum(isnull(AugustCL,0)) as AugustCL ,sum(isnull(SeptemberCL,0)) as SeptemberCL,sum(isnull(OctoberCL,0)) as OctoberCL,sum(isnull(NovemberCL,0)) as NovemberCL,sum(isnull(JanuaryCL,0)) as JanuaryCL, sum(isnull(FebruaryCL,0)) as FebruaryCL, sum(isnull(MarchCL,0)) as MarchCL,sum(isnull(DecemberCL,0)) as DecemberCL,sum(isnull(AprilSL,0)) as AprilSL,sum(isnull(MaySL,0)) as MaySL,sum(isnull(JuneSL,0)) as JuneSL,sum(isnull(JulySL,0)) as JulySL,sum(isnull(AugustSL,0)) as AugustSL ,sum(isnull(SeptemberSL,0)) as SeptemberSL,sum(isnull(OctoberSL,0)) as OctoberSL,sum(isnull(NovemberSL,0)) as NovemberSL,sum(isnull(JanuarySL,0)) as JanuarySL, sum(isnull(FebruarySL,0)) as FebruarySL, sum(isnull(MarchSL,0)) as MarchSL,sum(isnull(DecemberSL,0)) as DecemberSL,sum(isnull(AprilCH,0)) as AprilCH,sum(isnull(MayCH,0)) as MayCH,sum(isnull(JuneCH,0)) as JuneCH,sum(isnull(JulyCH,0)) as JulyCH,sum(isnull(AugustCH,0)) as AugustCH,sum(isnull(SeptemberCH,0)) as SeptemberCH,sum(isnull(OctoberCH,0)) as OctoberCH,sum(isnull(NovemberCH,0)) as NovemberCH ,sum(isnull(JanuaryCH,0)) as JanuaryCH, sum(isnull(FebruaryCH,0)) as FebruaryCH, sum(isnull(MarchCH,0)) as MarchCH,sum(isnull(DecemberCH,0)) as DecemberCH,sum(isnull(AprilML,0)) as AprilML,sum(isnull(MayML,0)) as MayML ,sum(isnull(JuneML,0)) as JuneML,sum(isnull(JulyML,0)) as JulyML,sum(isnull(AugustML,0)) as AugustML,sum(isnull(SeptemberML,0)) as SeptemberML,sum(isnull(OctoberML,0)) as OctoberML,sum(isnull(NovemberML,0)) as NovemberML,sum(isnull(JanuaryML,0)) as JanuaryML , sum(isnull(FebruaryML,0)) as FebruaryML, sum(isnull(MarchML,0)) as MarchML,sum(isnull(DecemberML,0)) as DecemberML,sum(isnull(AprilOther,0)) as AprilOther,sum(isnull(MayOther,0)) as MayOther,sum(isnull(JuneOther,0)) as JuneOther,sum(isnull(JulyOther,0)) as JulyOther,sum(isnull(AugustOther,0)) as AugustOther,sum(isnull(SeptemberOther,0)) as SeptemberOther,sum(isnull(OctoberOther,0))as OctoberOther ,sum(isnull(NovemberOther,0)) as NovemberOther,sum(isnull(JanuaryOther,0)) as JanuaryOther, sum(isnull(FebruaryOther,0)) as FebruaryOther, sum(isnull(MarchOther,0)) as MarchOther,sum(isnull(DecemberOther,0)) as DecemberOther,max(Birth_date) as Birth_date" & _
                '                    " ,sum(isnull(AprilELA,0)) as AprilELA,sum(isnull(MayELA,0)) as MayELA,sum(isnull(JuneELA,0)) as JuneELA ,sum(isnull(JulyELA,0)) as JulyELA,sum(isnull(AugustELA,0)) as AugustELA,sum(isnull(SeptemberELA,0)) as SeptemberELA,sum(isnull(OctoberELA,0)) as OctoberELA,sum(isnull(NovemberELA,0)) as NovemberELA ,sum(isnull(JanuaryELA,0)) as JanuaryELA,sum( isnull(FebruaryELA,0)) as FebruaryELA, sum(isnull(MarchELA,0)) as MarchELA,sum(isnull(DecemberELA,0)) as DecemberELA,sum(isnull(AprilCLA,0)) as AprilCLA,sum(isnull(MayCLA,0)) as MayCLA,sum(isnull(JuneCLA,0)) as JuneCLA,sum(isnull(JulyCLA,0)) as JulyCLA ,sum(isnull(AugustCLA,0)) as AugustCLA ,sum(isnull(SeptemberCLA,0)) as SeptemberCLA,sum(isnull(OctoberCLA,0)) as OctoberCLA,sum(isnull(NovemberCLA,0)) as NovemberCLA,sum(isnull(JanuaryCLA,0)) as JanuaryCLA, sum(isnull(FebruaryCLA,0)) as FebruaryCLA, sum(isnull(MarchCLA,0)) as MarchCLA,sum(isnull(DecemberCLA,0)) as DecemberCLA,sum(isnull(AprilSLA,0)) as AprilSLA,sum(isnull(MaySLA,0)) as MaySLA,sum(isnull(JuneSLA,0)) as JuneSLA,sum(isnull(JulySLA,0)) as JulySLA,sum(isnull(AugustSLA,0)) as AugustSLA ,sum(isnull(SeptemberSLA,0)) as SeptemberSLA,sum(isnull(OctoberSLA,0)) as OctoberSLA,sum(isnull(NovemberSLA,0)) as NovemberSLA,sum(isnull(JanuarySLA,0)) as JanuarySLA, sum(isnull(FebruarySLA,0)) as FebruarySLA, sum(isnull(MarchSLA,0)) as MarchSLA,sum(isnull(DecemberSLA,0)) as DecemberSLA,sum(isnull(AprilCHA,0)) as AprilCHA,sum(isnull(MayCHA,0)) as MayCHA,sum(isnull(JuneCHA,0)) as JuneCHA,sum(isnull(JulyCHA,0)) as JulyCHA,sum(isnull(AugustCHA,0)) as AugustCHA,sum(isnull(SeptemberCHA,0)) as SeptemberCHA,sum(isnull(OctoberCHA,0)) as OctoberCHA,sum(isnull(NovemberCHA,0)) as NovemberCHA ,sum(isnull(JanuaryCHA,0)) as JanuaryCHA, sum(isnull(FebruaryCHA,0)) as FebruaryCHA, sum(isnull(MarchCHA,0)) as MarchCHA,sum(isnull(DecemberCHA,0)) as DecemberCHA,sum(isnull(AprilMLA,0)) as AprilMLA,sum(isnull(MayMLA,0)) as MayMLA ,sum(isnull(JuneMLA,0)) as JuneMLA,sum(isnull(JulyMLA,0)) as JulyMLA,sum(isnull(AugustMLA,0)) as AugustMLA,sum(isnull(SeptemberMLA,0)) as SeptemberMLA,sum(isnull(OctoberMLA,0)) as OctoberMLA,sum(isnull(NovemberMLA,0)) as NovemberMLA,sum(isnull(JanuaryMLA,0)) as JanuaryMLA , sum(isnull(FebruaryMLA,0)) as FebruaryMLA, sum(isnull(MarchMLA,0)) as MarchMLA,sum(isnull(DecemberMLA,0)) as DecemberMLA,sum(isnull(AprilOtherA,0)) as AprilOtherA,sum(isnull(MayOtherA,0)) as MayOtherA,sum(isnull(JuneOtherA,0)) as JuneOtherA,sum(isnull(JulyOtherA,0)) as JulyOtherA,sum(isnull(AugustOtherA,0)) as AugustOtherA,sum(isnull(SeptemberOtherA,0)) as SeptemberOtherA,sum(isnull(OctoberOtherA,0))as OctoberOtherA ,sum(isnull(NovemberOtherA,0)) as NovemberOtherA,sum(isnull(JanuaryOtherA,0)) as JanuaryOtherA, sum(isnull(FebruaryOtherA,0)) as FebruaryOtherA, sum(isnull(MarchOtherA,0)) as MarchOtherA,sum(isnull(DecemberOtherA,0)) as DecemberOtherA" & _
                '                    " ,sum(isnull(AprilWD,0)) as AprilWD,sum(isnull(MayWD,0)) as MayWD,sum(isnull(JuneWD,0)) as JuneWD ,sum(isnull(JulyWD,0)) as JulyWD,sum(isnull(AugustWD,0)) as AugustWD,sum(isnull(SeptemberWD,0)) as SeptemberWD,sum(isnull(OctoberWD,0)) as OctoberWD,sum(isnull(NovemberWD,0)) as NovemberWD ,sum(isnull(JanuaryWD,0)) as JanuaryWD,sum( isnull(FebruaryWD,0)) as FebruaryWD, sum(isnull(MarchWD,0)) as MarchWD,sum(isnull(DecemberWD,0)) as DecemberWD " & _
                '                    " ,sum(isnull(AprilHD,0)) as AprilHD,sum(isnull(MayHD,0)) as MayHD,sum(isnull(JuneHD,0)) as JuneHD ,sum(isnull(JulyHD,0)) as JulyHD,sum(isnull(AugustHD,0)) as AugustHD,sum(isnull(SeptemberHD,0)) as SeptemberHD,sum(isnull(OctoberHD,0)) as OctoberHD,sum(isnull(NovemberHD,0)) as NovemberHD ,sum(isnull(JanuaryHD,0)) as JanuaryHD,sum( isnull(FebruaryHD,0)) as FebruaryHD, sum(isnull(MarchHD,0)) as MarchHD,sum(isnull(DecemberHD,0)) as DecemberHD ,max(Joining_date) as Joining_date ,max(Location_Desc) as Location_Desc ,max(DEVISION_NAME) as DEVISION_NAME" & _
                '                    " from (select * from(select * from (select * from (select * from(select * from (select * from (select * from ( select * from(select * from(select * from(select * from (select * from (select * from(select * from  (select * from " & _
                '                    " (select EMP_CODE,Emp_Name,PAYPERIOD_DAYS,PRESENT_DAYS,ABSENT_DAYS,HOLIDAY_DAYS,  WEEKLY_OFF,  PAYABLE_DAYS,(PAYPERIOD_DAYS-WEEKLY_OFF-HOLIDAY_DAYS) as Working_Days, EL, CL, SL,   CH, ML, OTHER , ELA, CLA, SLA,  CHA,  MLA, OTHERA,MonthlyEL,MonthlyCL,MonthlySL,MonthlyCH,MonthlyML,MonthlyOther,MonthlyELA,MonthlyCLA,MonthlySLA,MonthlyCHA,MonthlyMLA,MonthlyOtherA,Birth_date,MonthlyWD,MonthlyHD,Joining_date ,Location_Desc ,DEVISION_NAME from " & _
                '                    " (SELECT  GSA.EMP_CODE,tspl_employee_master.Emp_Name ,EMP.DEPARTMENT_CODE,GSA.PAYPERIOD_DAYS,GSA.PRESENT_DAYS,GSA.ABSENT_DAYS,GSA.HOLIDAY_DAYS, (GSA.PAYPERIOD_DAYS-GSA.PRESENT_DAYS-GSA.ABSENT_DAYS-GSA.LEAVE_DAYS-GSA.HOLIDAY_DAYS) AS WEEKLY_OFF,  GSA.PAYABLE_DAYS,COALESCE(LEDGER.EL,0) AS EL,COALESCE(LEDGER.CL,0) AS CL,COALESCE(LEDGER.SL,0) AS SL,  COALESCE(LEDGER.CH,0) AS CH,COALESCE(LEDGER.ML,0) AS ML,COALESCE(LEDGER.OTHER,0) AS OTHER ,COALESCE(LEDGER.EL_A,0) AS ELA,COALESCE(LEDGER.CL_A,0) AS CLA,COALESCE(LEDGER.SL_A,0) AS SLA,  COALESCE(LEDGER.CH_A,0) AS CHA,COALESCE(LEDGER.ML_A,0) AS MLA,COALESCE(LEDGER.OTHER_A,0) AS OTHERA,MonthlyEL,MonthlyCL,MonthlySL,MonthlyCH,MonthlyML,MonthlyOther,MonthlyELA,MonthlyCLA,MonthlySLA,MonthlyCHA,MonthlyMLA,MonthlyOtherA,tspl_employee_master.Birth_date,MonthlyWD,MonthlyHD,tspl_employee_master.Joining_date ,Location_Desc ,DEVISION_NAME  FROM TSPL_GENERATE_SALARY_ATTENDANCE  GSA INNER JOIN TSPL_GENERATE_SALARY GS ON GS.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE  " & _
                '                    " left join tspl_employee_master on tspl_employee_master.EMP_CODE =GSA.EMP_CODE LEFT JOIN (SELECT EMP_CODE,SUM(EL) AS EL,SUM(CL) AS CL,SUM(SL) AS SL,SUM(CH) AS CH,SUM(ML)AS ML,SUM(OTHER) AS OTHER,SUM(EL_A ) AS EL_A,SUM(CL_A) AS CL_A,SUM(SL_A) AS SL_A,SUM(CH_A) AS CH_A,SUM(ML_A)AS ML_A,SUM(OTHER_A) AS OTHER_A,PAY_PERIOD_CODE,max(DATE_FROM) as DATE_FROM,max(DATE_TO) as DATE_TO,max(_Month+'EL')  as MonthlyEL ,max(_Month+'CL')  as MonthlyCL,max(_Month+'SL')  as MonthlySL,max(_Month+'CH')  as MonthlyCH,max(_Month+'ML')  as MonthlyML ,max(_Month+'Other')  as MonthlyOther,max(_Month+'ELA') as MonthlyELA,max(_Month+'CLA')  as MonthlyCLA,max(_Month+'SLA')  as MonthlySLA,max(_Month+'CHA')  as MonthlyCHA,max(_Month+'MLA')  as MonthlyMLA ,max(_Month+'OtherA')  as MonthlyOtherA,max(_month) as _Month,max(_month+'WD') as MonthlyWD,MAX(_month+'HD') as MonthlyHD  FROM " & _
                '                    " (  select  LEDGER.EMP_CODE,(CASE WHEN LEAVE.LEAVE_TYPE='EL' and TR_TYPE ='ALLOT' THEN ALLOTED  ELSE 0 END) AS EL_A,(CASE WHEN LEAVE.LEAVE_TYPE='CL' and TR_TYPE ='ALLOT' THEN ALLOTED ELSE 0 END) AS CL_A,  (CASE WHEN LEAVE.LEAVE_TYPE='MED' and TR_TYPE ='ALLOT' THEN ALLOTED ELSE 0 END) AS SL_A,(CASE WHEN LEAVE.LEAVE_TYPE='COFF' and TR_TYPE ='ALLOT' THEN ALLOTED ELSE 0 END) AS CH_A,  (CASE WHEN LEAVE.LEAVE_TYPE='MATRL'and TR_TYPE ='ALLOT' THEN ALLOTED ELSE 0 END) AS ML_A,(CASE WHEN LEAVE.LEAVE_TYPE='Other'and TR_TYPE ='ALLOT' THEN ALLOTED ELSE 0 END) AS OTHER_A " & _
                '                    " ,(CASE WHEN LEAVE.LEAVE_TYPE='EL' THEN AVAILED ELSE 0 END) AS EL,(CASE WHEN LEAVE.LEAVE_TYPE='CL' THEN AVAILED ELSE 0 END) AS CL,  (CASE WHEN LEAVE.LEAVE_TYPE='MED' THEN AVAILED ELSE 0 END) AS SL,(CASE WHEN LEAVE.LEAVE_TYPE='COFF' THEN AVAILED ELSE 0 END) AS CH,  (CASE WHEN LEAVE.LEAVE_TYPE='MATRL' THEN AVAILED ELSE 0 END) AS ML,(CASE WHEN LEAVE.LEAVE_TYPE='Other' THEN AVAILED ELSE 0 END) AS OTHER,PAY_PERIOD_CODE,LEDGER.DATE_FROM,LEDGER.DATE_TO, DATENAME (MONTH ,CONVERT(date,Date_from,103))as _Month  from TSPL_VIEW_LEAVE_LEDGER LEDGER " & _
                '                    " LEFT JOIN TSPL_LEAVE_MASTER LEAVE ON LEDGER.LEAVE_CODE=LEAVE.LEAVE_CODE ) AS LEAVES GROUP BY EMP_CODE,PAY_PERIOD_CODE) AS LEDGER ON GSA.EMP_CODE=LEDGER.EMP_CODE  and  GS.PAY_PERIOD_CODE=LEDGER.PAY_PERIOD_CODE  LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON GSA.EMP_CODE=EMP.EMP_CODE left join tspl_LOcation_Master on emp.LOCATION_CODE = tspl_LOcation_Master.Location_Code left join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE =emp.DEVISION_CODE   WHERE 2=2  and  " & _
                '                    " convert(date, LEDGER.date_from  ,103)>= convert(date,'" & dtpfromdate1.Value & "',103) and convert(date,LEDGER .DATE_TO ,103)<= convert(date,'" & todate.Value & "',103)  "
                'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                '    Innerqry += " and GS.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
                'End If
                'If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                '    Innerqry += " and EMP.DEVISION_CODE in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") " + Environment.NewLine
                'End If
                'If TxtMultiEmployee.arrValueMember IsNot Nothing AndAlso TxtMultiEmployee.arrValueMember.Count > 0 Then
                '    Innerqry += " and EMP.Emp_Code in (" + clsCommon.GetMulcallString(TxtMultiEmployee.arrValueMember) + ") " + Environment.NewLine
                'End If
                'Innerqry += "  )as hh) as pivo"
                'Innerqry += " Pivot( MAX(EL) FOR monthlyEL IN (AprilEL,MayEL,JuneEL,JulyEL,AugustEL,SeptemberEL,OctoberEL,NovemberEL,JanuaryEL, FebruaryEL, MarchEL,DecemberEL))AS pivott) as m Pivot( MAX(CL) FOR monthlyCL IN (AprilCL,MayCL,JuneCL,JulyCL,AugustCL,SeptemberCL,OctoberCL,NovemberCL,JanuaryCL, FebruaryCL, MarchCL,DecemberCL))AS pivott1) as gg "
                'Innerqry += " Pivot( MAX(SL) FOR monthlySL IN (AprilSL,MaySL,JuneSL,JulySL,AugustSL,SeptemberSL,OctoberSL,NovemberSL,JanuarySL, FebruarySL, MarchSL,DecemberSL))AS pivott2 ) as tt  Pivot( MAX(CH) FOR monthlyCH IN (AprilCH,MayCH,JuneCH,JulyCH,AugustCH,SeptemberCH,OctoberCH,NovemberCH,JanuaryCH, FebruaryCH, MarchCH,DecemberCH))AS pivott3 ) as tt1  Pivot( MAX(ML) FOR monthlyML IN (AprilML,MayML,JuneML,JulyML,AugustML,SeptemberML,OctoberML,NovemberML,JanuaryML, FebruaryML, MarchML,DecemberML))AS pivott4 ) as tt2 Pivot( MAX(Other) FOR monthlyOther IN (AprilOther,MayOther,JuneOther,JulyOther,AugustOther,SeptemberOther,OctoberOther,NovemberOther,JanuaryOther, FebruaryOther, MarchOther,DecemberOther))AS pivott5)as tt3 " & _
                '                    " Pivot( MAX(ELA) FOR MonthlyELA IN (AprilELA,MayELA,JuneELA,JulyELA,AugustELA,SeptemberELA,OctoberELA,NovemberELA,JanuaryELA, FebruaryELA, MarchELA,DecemberELA))AS pivott6 ) as tt4" & _
                '                    " Pivot( MAX(CLA) FOR monthlyCLA IN (AprilCLA,MayCLA,JuneCLA,JulyCLA,AugustCLA,SeptemberCLA,OctoberCLA,NovemberCLA,JanuaryCLA, FebruaryCLA, MarchCLA,DecemberCLA))AS pivott7) as tt5  Pivot( MAX(SLA) FOR monthlySLA IN (AprilSLA,MaySLA,JuneSLA,JulySLA,AugustSLA,SeptemberSLA,OctoberSLA,NovemberSLA,JanuarySLA, FebruarySLA, MarchSLA,DecemberSLA))AS pivott8) as tt6 " & _
                '                    " Pivot( MAX(CHA) FOR monthlyCHA IN (AprilCHA,MayCHA,JuneCHA,JulyCHA,AugustCHA,SeptemberCHA,OctoberCHA,NovemberCHA,JanuaryCHA, FebruaryCHA, MarchCHA,DecemberCHA))AS pivott9 ) as tt7  Pivot( MAX(MLA) FOR monthlyMLA IN (AprilMLA,MayMLA,JuneMLA,JulyMLA,AugustMLA,SeptemberMLA,OctoberMLA,NovemberMLA,JanuaryMLA, FebruaryMLA, MarchMLA,DecemberMLA))AS pivott10 ) as tt8 Pivot( MAX(OtherA) FOR MonthlyOtherA IN (AprilOtherA,MayOtherA,JuneOtherA,JulyOtherA,AugustOtherA,SeptemberOtherA,OctoberOtherA,NovemberOtherA,JanuaryOtherA, FebruaryOtherA, MarchOtherA,DecemberOtherA))AS pivott11)as tt39 " & _
                '                    " Pivot( MAX(HOLIDAY_DAYS) FOR MonthlyHD  IN (AprilHD,MayHD,JuneHD,JulyHD,AugustHD,SeptemberHD,OctoberHD,NovemberHD,JanuaryHD, FebruaryHD, MarchHD,DecemberHD))AS pivott12)as tt40" & _
                '                    " Pivot( MAX(Working_Days) FOR MonthlyWD IN (AprilWD,MayWD,JuneWD,JulyWD,AugustWD,SeptemberWD,OctoberWD,NovemberWD,JanuaryWD, FebruaryWD, MarchWD,DecemberWD))AS pivott11)as tt39 " & _
                '                    " )as dd group by EMP_CODE)as mm "

                'Dim comp_name As String = objCommonVar.CurrentCompanyName
                'Dim To_Date As Date = clsCommon.GetPrintDate(todate.Value, "dd/MMM/yyyy")
                'Dim FinalQry As String = "select row_number() over( order by [Emp Code])as [S.No],[Emp Code], [Emp Name], [DOB],Joining_date as [DOJ], Location_Desc as [Location], DEVISION_NAME as [Division],AprilWD as [WD (Apr]  , MayWD as [WD (May], JuneWD as [WD (Jun], JulyWD as [WD (Jul], AugustWD as [WD (Aug], SeptemberWD as [WD (Sep], OctoberWD as [WD (Oct], NovemberWD as [WD (Nov], DecemberWD as [WD (Dec], JanuaryWD as [WD (Jan], FebruaryWD as [WD (Feb] , MarchWD as [WD (Mar], TotalWD as [Total WD],"
                'FinalQry += " AprilHD as [HD (Apr], MayHD as [HD (May], JuneHD as [HD (Jun], JulyHD as [HD (Jul], AugustHD as [HD (Aug], SeptemberHD as [HD (Sep], OctoberHD as [HD (Oct], NovemberHD as [HD (Nov], DecemberHD as [HD (Dec], JanuaryHD as [HD (Jan], FebruaryHD as [HD (Feb], MarchHD as [HD (Mar], TotalHD as [Total HD], Opening"
                'FinalQry += " ,April as [(Apr], May as [(May], June as [(Jun],July as [(Jul], August as [(Aug], September as [(Sep], October as [(Oct], November as [(Nov], December as [(Dec], January as [(Jan], February as [(Feb], March as [(Mar],Total, AprilEL as [EL (Apr], MayEL as [EL (May], JuneEL as [EL (Jun] , JulyEL as [EL (Jul], AugustEL as [EL (Aug], SeptemberEL as [EL (Sep], OctoberEL as [EL (Oct], NovemberEL as [EL (Nov] , DecemberEL as [EL (Dec], JanuaryEL as [EL (Jan], FebruaryEL as [EL (Feb],  MarchEL as [EL (Mar], TotalEL as [Total EL], AprilCL as [CL (Apr], MayCL as [CL (May], JuneCL as [CL (Jun], JulyCL as [CL (Jul] , AugustCL as [CL (Aug] , SeptemberCL as [CL (Sep], OctoberCL as [CL (Oct], NovemberCL as [CL (Nov], DecemberCL as [CL (Dec], JanuaryCL as [CL (Jan],  FebruaryCL as [CL (Feb],  MarchCL as [CL (Mar], TotalCL as [Total CL], AprilSL as [SL (Apr], MaySL as [SL (May], JuneSL as [SL Jun], JulySL as [SL (Jul], AugustSL as [SL (Aug] , SeptemberSL as [SL (Sep], OctoberSL as [SL (Oct], NovemberSL as [SL (Nov], DecemberSL as [SL (Dec], JanuarySL as [SL (Jan],  FebruarySL as [SL (Feb], MarchSL as [SL (Mar],TotalSL as [Total SL], AprilCH as [CH (Apr], MayCH as [CH (May], JuneCH as [CH (Jun], JulyCH as [CH (July], AugustCH as [CH (Aug], SeptemberCH as [CH (Sep], OctoberCH as [CH (Oct], NovemberCH as [CH (Nov] , JanuaryCH as [CH (Jan], FebruaryCH as [CH (Feb], MarchCH as [CH (Mar], DecemberCH as [CH (Dec], TotalCH as [Total CH], AprilML as [ML (Apr], MayML as [ML (May] , JuneML as [ML (Jun], JulyML as [ML (Jul], AugustML as [ML (Aug], SeptemberML as [ML (Sep], OctoberML as [ML (Oct], NovemberML as [ML (Nov], DecemberML as [ML (Dec], JanuaryML as [ML (Jan] ,  FebruaryML as [ML (Feb],  MarchML as [ML (Mar], TotalML as [Total ML], AprilOther as [Other (Apr], MayOther as [Other (May], JuneOther as [Other (Jun], JulyOther as [Other (Jul], AugustOther as [Other (Aug], SeptemberOther as [Other (Sep], OctoberOther as [Other (Oct] , NovemberOther as [Other (Nov], DecemberOther as [Other (Dec], JanuaryOther as [Other (Jan],  FebruaryOther as [Other (Feb], MarchOther as [Other (Mar], TotalOther as [Total Other],[Total leave Taken],(Total-[Total leave Taken]) as [Balance Leave],RATE_AMOUNT  from (select [Emp Code], [Emp Name], [DOB], "
                'FinalQry += " AprilWD, MayWD, JuneWD, JulyWD, AugustWD, SeptemberWD, OctoberWD, NovemberWD, DecemberWD, JanuaryWD, FebruaryWD, MarchWD, TotalWD,"
                'FinalQry += " AprilHD, MayHD, JuneHD, JulyHD, AugustHD, SeptemberHD, OctoberHD, NovemberHD, DecemberHD, JanuaryHD, FebruaryHD, MarchHD, TotalHD, Opening"
                'FinalQry += " ,April, May, June,July, August, September, October, November, December, January, February, March,(OPENING + April+ May+ June+July+ August+ September+ October+ November+ December+ January+ February+ March)Total,"
                'FinalQry += " AprilEL, MayEL, JuneEL , JulyEL, AugustEL, SeptemberEL, OctoberEL, NovemberEL , DecemberEL, JanuaryEL, FebruaryEL,  MarchEL, TotalEL, AprilCL, MayCL, JuneCL, JulyCL , AugustCL , SeptemberCL, OctoberCL, NovemberCL, DecemberCL, JanuaryCL,  FebruaryCL,  MarchCL, TotalCL, AprilSL, MaySL, JuneSL, JulySL, AugustSL , SeptemberSL, OctoberSL, NovemberSL, DecemberSL, JanuarySL,  FebruarySL, MarchSL,TotalSL, AprilCH, MayCH, JuneCH, JulyCH, AugustCH, SeptemberCH, OctoberCH, NovemberCH , JanuaryCH, FebruaryCH, MarchCH, DecemberCH, TotalCH, AprilML, MayML , JuneML, JulyML, AugustML, SeptemberML, OctoberML, NovemberML, DecemberML, JanuaryML ,  FebruaryML,  MarchML, TotalML, AprilOther, MayOther, JuneOther, JulyOther, AugustOther, SeptemberOther, OctoberOther , NovemberOther, DecemberOther, JanuaryOther,  FebruaryOther, MarchOther, TotalOther,(TotalEL+TotalCL+TotalSL+TotalML+TotalCH+TotalOther)as [Total leave Taken],Joining_date , Location_Desc , DEVISION_NAME,RATE_AMOUNT  from(" & Innerqry & ")as jj left join (" & strSelect & " )as dd on jj.[Emp Code] =dd.emp_code left join (select maX(REVISION_NO) AS REVISION_NO ,EMP_CODE ,PAY_HEAD_CODE ,max(RATE_AMOUNT) AS RATE_AMOUNT  from TSPL_EMPLOYEE_SALARY"
                'FinalQry += " left join TSPL_EMPLOYEE_SALARY_PAYHEADS on TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE =TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE where PAY_HEAD_CODE ='BASIC' "
                'FinalQry += " GROUP BY EMP_CODE ,PAY_HEAD_CODE)as tt on tt.EMP_CODE = jj.[Emp Code] ) as zz"
                'Dim TotalLeaveTaken As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum([Total leave Taken]) from (" & FinalQry & ") as m"))
                'Dim BadiQry As String = "select *," & TotalLeaveTaken & " as TotalLeaveTaken,'" & comp_name & "' as [Company Name],'" & To_Date & "' as [TO Date],'" & DivAddress & "' as DivAddress  from (" & FinalQry & ") as m"
                Dim dt As DataTable
                If rbtnSummary.IsChecked = True Then
                    dt = clsSalaryGeneration.GetLeaveActuarialSummaryDT(fndLeaveCode.Value, txtLocation.arrValueMember, txtDivisionMult.arrValueMember, TxtMultiEmployee.arrValueMember, dtpfromdate1.Value, todate.Value)
                Else
                    dt = clsSalaryGeneration.GetLeaveActuarialDT(fndLeaveCode.Value, txtLocation.arrValueMember, txtDivisionMult.arrValueMember, TxtMultiEmployee.arrValueMember, dtpfromdate1.Value, todate.Value)
                End If


                If dt IsNot Nothing And dt.Rows.Count > 0 Then

                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.DataSource = dt
                    gv1.EnableFiltering = True
                    FormatGrid()
                    gv1.BestFitColumns()
                    gv1.ReadOnly = True
                    If btnPrint = False Then
                        If dt.Rows.Count <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                        Else

                            Dim frmcrystal As New frmCrystalReportViewer()
                            frmcrystal.funreport(CrystalReportFolder.HRPayroll, dt, "crptActuarialValuationSummary", "Acturial Valuation Summary")
                        End If
                    End If

                End If

                If gv1.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                    Exit Sub
                End If
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow(Me, "Please select date range within 12 months.", Me.Text)
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub FormatGrid()

        'Dim strStartMonth As String = clsDBFuncationality.getSingleValue("SELECT YEAR (convert(date,'" & dtpfromdate1.Value & "',103))")
        'Dim strEndMonth As String = clsDBFuncationality.getSingleValue("SELECT YEAR (convert(date,'" & todate.Value & "',103))")

        'Dim dateyeardiff As Integer = clsDBFuncationality.getSingleValue("	SELECT DATEDIFF(year,convert(date,'" & dtpfromdate1.Value & "',103),convert(date,'" & todate.Value & "',103)) ")
        'If dateyeardiff = 0 Then
        '    strEndMonth = clsDBFuncationality.getSingleValue("SELECT YEAR (dateadd(year,1,convert(date,'" & todate.Value & "',103)))")
        'End If
        'gv1.TableElement.TableHeaderHeight = 25
        'gv1.MasterTemplate.ShowRowHeaderColumn = False
        'Dim k As Integer = 0
        'Dim j As Integer = 0
        'For ii As Integer = 7 To gv1.Columns.Count - 8
        '    If (k < 9) Then
        '        If clsCommon.CompairString(gv1.Columns(ii).HeaderText, "Opening") = CompairStringResult.Equal Then
        '            j = -1
        '            k = -1
        '        Else
        '            gv1.Columns(ii).HeaderText = gv1.Columns(ii).HeaderText & "'" & strStartMonth.Substring(2, strStartMonth.Length - 2) & ")"
        '            gv1.Columns(ii).WrapText = True
        '        End If
        '    Else
        '        If j >= 12 Then
        '            j = -1
        '            k = -1
        '        Else
        '            gv1.Columns(ii).HeaderText = gv1.Columns(ii).HeaderText & "'" & strEndMonth.Substring(2, strEndMonth.Length - 2) & ")"
        '            gv1.Columns(ii).WrapText = True

        '        End If
        '    End If

        '    k = k + 1
        '    j = j + 1
        'Next
        'gv1.Columns("TotalLeaveTaken").IsVisible = False 
        'gv1.Columns("TotalLeaveTaken").Width = 100
        'gv1.Columns("TotalLeaveTaken").HeaderText = "TotalLeaveTaken"
        'gv1.Columns("Company Name").IsVisible = False
        'gv1.Columns("Company Name").Width = 100
        'gv1.Columns("Company Name").HeaderText = "[Company Name]"
        'gv1.Columns("TO Date").IsVisible = False
        'gv1.Columns("TO Date").Width = 100
        'gv1.Columns("TO Date").HeaderText = "[TO Date]"
        'gv1.Columns("RATE_AMOUNT").IsVisible = False
        'gv1.Columns("RATE_AMOUNT").Width = 100
        'gv1.Columns("RATE_AMOUNT").HeaderText = "RATE_AMOUNT"
        If rbtnDetail.IsChecked = True Then
            gv1.Columns("DivAddress").IsVisible = False
            gv1.Columns("DivAddress").Width = 100
            gv1.Columns("DivAddress").HeaderText = "DivAddress"
        End If

        'gv1.Visible = False
        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True

    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        btnPrint = True
        PageSetupReport_ID = MyBase.Form_ID
        LoadData()
    End Sub
    Sub reset()
        todate.Value = clsCommon.GETSERVERDATE()
        dtpfromdate1.Value = todate.Value.AddYears(-1)
        RadPageView1.SelectedPage = RadPageViewPage1
        gv1.Rows.Clear()
    End Sub
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        reset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        btnPrint = False
        LoadData()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RptActurialValuation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            reset()
            ButtonToolTip.SetToolTip(btnRefresh, "Press Alt+R for Refresh ")
            ButtonToolTip.SetToolTip(btnGo, "Press Alt+P for Print ")
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for Close the Window")
            ButtonToolTip.SetToolTip(BtnReset, "Press Alt+C for Reset")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Try
            Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER"
            txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDivisionMult__My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Try
            Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
            txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtMultiEmployee__My_Click(sender As Object, e As EventArgs) Handles TxtMultiEmployee._My_Click
        Try
            Dim qry As String = "select EMP_CODE AS [Code],Emp_Name as [Name] from tspl_employee_MASTER left join tspl_location_master on tspl_location_master.Location_Code =tspl_employee_MASTER.LOCATION_CODE  WHERE tspl_employee_MASTER.LOCATION_CODE IN (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & " )"
            TxtMultiEmployee.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", TxtMultiEmployee.arrValueMember, TxtMultiEmployee.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpfromdate1.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(todate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(txtLocation.arrValueMember)
                arrHeader.Add((" Location : " + strLocationName + " "))
            Else
                arrHeader.Add((" Location : All"))
            End If
            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Division : " + clsCommon.GetMulcallStringWithComma(txtDivisionMult.arrValueMember))
            Else
                arrHeader.Add((" Division: All"))
            End If
            If TxtMultiEmployee.arrValueMember IsNot Nothing AndAlso TxtMultiEmployee.arrValueMember.Count > 0 Then
                arrHeader.Add("Employee  : " + clsCommon.GetMulcallStringWithComma(TxtMultiEmployee.arrValueMember))
            Else
                arrHeader.Add(("Employee: All"))
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Acturail Valuation Report", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Acturail Valuation Report", gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    'Private Sub RadSplitButton1_Click(sender As Object, e As EventArgs) Handles RadSplitButton1.Click
    '    print(EnumExportTo.Excel)
    'End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpfromdate1.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(todate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptActurialValuation & "'"))

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(txtLocation.arrValueMember)
                    arrHeader.Add(("Location : " + strLocationName + " "))
                Else
                    arrHeader.Add(("Location : All"))
                End If
                If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                    arrHeader.Add("Division : " + clsCommon.GetMulcallStringWithComma(txtDivisionMult.arrDispalyMember))
                Else
                    arrHeader.Add(("Division : All"))
                End If
                If TxtMultiEmployee.arrValueMember IsNot Nothing AndAlso TxtMultiEmployee.arrValueMember.Count > 0 Then
                    arrHeader.Add("Employee : " + clsCommon.GetMulcallStringWithComma(TxtMultiEmployee.arrDispalyMember))
                Else
                    arrHeader.Add(("Employee : All"))
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
                    'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                Else
                    clsCommon.MyExportToPDF("Acturail Valuation Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                gv1.MasterTemplate.FilterDescriptors.Clear()
                Dim obj As New clsGridLayout()
                obj.ReportID = MyBase.Form_ID
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                gv1.SaveLayout(obj.GridLayout)
                obj.GridColumns = gv1.ColumnCount
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
                End If

                ''richa agarwal regarding memory leakage
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
                ''---------------
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Try
            clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
            common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndLeaveCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLeaveCode._MYValidating
        Try
            fndLeaveCode.Value = clsLeaveMaster.getFinder("", fndLeaveCode.Value, isButtonClicked)
            lblLeaveName.Text = clsLeaveMaster.GetName(fndLeaveCode.Value, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
End Class
