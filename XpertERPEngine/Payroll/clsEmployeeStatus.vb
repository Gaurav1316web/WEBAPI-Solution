Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsEmployeeStatus


#Region "Variables"
    Public Code As String
    Public EMP_CODE As String
    Public EMP_NAME As String
    Public APPLICABLE_FROM As String
    Public DESIGNATION_ID As String
    Public DESIGNATION_NAME As String
    Public DEPARTMENT_CODE As String
    Public DEPARTMENT_NAME As String
    Public REPORTING_PERSON As String
    Public BANK_ACC_NO As String
    Public PAYMENT_MODE As String
    Public LOCATION_CODE As String
    Public LOCATION_DESC As String
    Public DEVISION_CODE As String
    Public DEVISION_NAME As String
    Public GRADE_CODE As String
    Public GRADE_NAME As String
    Public ATTENDANCE_CODE As String
    Public ATTENDANCE_NAME As String
    Public NAME_IN_ACC As String
    Public BANK_CODE As String
    Public BANK_NAME As String
    Public PF_NO As String
    Public ESI_NO As String
    Public OT_CODE As String
    Public BONUS_CODE As String
    Public REVISION_NO As Integer
    Public PF_APPLICABLE As Boolean
    Public ESI_APPLICABLE As Boolean
    Public OT_APPLICABLE As Boolean
    Public Professional_Tax_Applicable As Boolean = False
    Public BONUS_APPLICABLE As Boolean
    Public EPS_TO_EPF As Boolean
    Public WORKING_STATUS As String
    Public Max_Amount_EPF As Double = 0
    Public Max_Amount_ESI As Double = 0
    '' for kdil and viney
    Public SHIFT_CODE As String
    Public SHIFT_CHANG_TYPE As String
    Public CONV_RATE_CODE As String
    Public CONV_TYPE As String
    Public IS_OD_APPL As String
    Public EPF_Rate As Decimal = 0
    Public ESI_Rate As Decimal = 0
    Public objList As New List(Of clsEmployeeStatusWeeklyOff)
    Public Pf_Calculation_Type As String


#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsEmployeeStatus
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            isSaved = True

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_EMPLOYEE_WEEKLY_OFF where EMP_STATUS_CODE='" & strCode & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_EMPLOYEE_STATUS where EMP_STATUS_CODE ='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsEmployeeStatus
        Dim obj As clsEmployeeStatus = Nothing
        Dim qry = "select ST.*,EMP.EMP_NAME,EMP1.EMP_NAME AS REPORTING_PERSON_NAME,DEP.DEPARTMENT_NAME,DES.Designation_Desc AS DESIGNATION_NAME, " _
        & " BR.LOCATION_DESC,DV.DEVISION_NAME," _
        & " GD.GRADE_NAME,ATTD.ATTENDANCE_NAME,BA.DESCRIPTION AS BANK_NAME,OT.OT_NAME,BO.BONUS_NAME,ST.Max_Amount_EPF,ST.Max_Amount_ESI from TSPL_EMPLOYEE_STATUS ST " _
        & " LEFT JOIN  TSPL_DESIGNATION_MASTER DES ON ST.DESIGNATION_ID=DES.DESIGNATION_ID " _
        & " LEFT JOIN  TSPL_DEPARTMENT_MASTER DEP ON ST.DEPARTMENT_CODE=DEP.DEPARTMENT_CODE " _
        & " LEFT JOIN  TSPL_EMPLOYEE_MASTER EMP ON ST.EMP_CODE=EMP.EMP_CODE " _
        & " LEFT JOIN  TSPL_EMPLOYEE_MASTER EMP1 ON ST.REPORTING_PERSON_CODE=EMP1.EMP_CODE " _
        & " LEFT JOIN  TSPL_LOCATION_MASTER BR ON ST.LOCATION_CODE=BR.LOCATION_CODE " _
        & " LEFT JOIN  TSPL_DEVISION_MASTER DV ON ST.DEVISION_CODE=DV.DEVISION_CODE " _
        & " LEFT JOIN  TSPL_GRADE_MASTER GD ON ST.GRADE_CODE=GD.GRADE_CODE  " _
        & " LEFT JOIN  TSPL_ATTENDANCE_MASTER ATTD ON ST.ATTENDANCE_CODE=ATTD.ATTENDANCE_CODE " _
        & " LEFT JOIN  TSPL_BANK_MASTER BA ON ST.BANK_CODE=BA.BANK_CODE  " _
        & " LEFT JOIN  TSPL_OT_MASTER OT ON ST.OT_CODE=OT.OT_CODE " _
        & " LEFT JOIN  TSPL_BONUS_MASTER BO ON ST.BONUS_CODE=BO.BONUS_CODE where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and EMP_STATUS_CODE = (select MIN(EMP_STATUS_CODE) from TSPL_EMPLOYEE_STATUS)"
            Case NavigatorType.Last
                qry += " and EMP_STATUS_CODE = (select Max(EMP_STATUS_CODE) from TSPL_EMPLOYEE_STATUS)"
            Case NavigatorType.Next
                qry += " and EMP_STATUS_CODE = (select Min(EMP_STATUS_CODE) from TSPL_EMPLOYEE_STATUS where  EMP_STATUS_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and EMP_STATUS_CODE = (select Max(EMP_STATUS_CODE) from TSPL_EMPLOYEE_STATUS where EMP_STATUS_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and EMP_STATUS_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsEmployeeStatus()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("EMP_STATUS_CODE"))

            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.EMP_NAME = clsCommon.myCstr(dt.Rows(0)("EMP_NAME"))
            obj.APPLICABLE_FROM = clsCommon.myCstr(dt.Rows(0)("APPLICABLE_FROM"))
            obj.DESIGNATION_ID = clsCommon.myCstr(dt.Rows(0)("DESIGNATION_ID"))
            obj.DESIGNATION_NAME = clsCommon.myCstr(dt.Rows(0)("DESIGNATION_NAME"))
            obj.DEPARTMENT_CODE = clsCommon.myCstr(dt.Rows(0)("DEPARTMENT_CODE"))
            obj.DEPARTMENT_NAME = clsCommon.myCstr(dt.Rows(0)("DEPARTMENT_NAME"))
            obj.REPORTING_PERSON = clsCommon.myCstr(dt.Rows(0)("REPORTING_PERSON_CODE"))
            obj.BANK_ACC_NO = clsCommon.myCstr(dt.Rows(0)("BANK_ACC_NO"))
            obj.PAYMENT_MODE = clsCommon.myCstr(dt.Rows(0)("PAYMENT_MODE"))
            obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
            obj.LOCATION_DESC = clsCommon.myCstr(dt.Rows(0)("LOCATION_DESC"))
            obj.DEVISION_CODE = clsCommon.myCstr(dt.Rows(0)("DEVISION_CODE"))
            obj.DEVISION_NAME = clsCommon.myCstr(dt.Rows(0)("DEVISION_NAME"))
            obj.GRADE_CODE = clsCommon.myCstr(dt.Rows(0)("GRADE_CODE"))
            obj.GRADE_NAME = clsCommon.myCstr(dt.Rows(0)("GRADE_NAME"))
            obj.ATTENDANCE_CODE = clsCommon.myCstr(dt.Rows(0)("ATTENDANCE_CODE"))
            obj.ATTENDANCE_NAME = clsCommon.myCstr(dt.Rows(0)("ATTENDANCE_NAME"))
            obj.NAME_IN_ACC = clsCommon.myCstr(dt.Rows(0)("NAME_IN_ACCOUNT"))
            obj.BANK_CODE = clsCommon.myCstr(dt.Rows(0)("BANK_CODE"))
            obj.BANK_NAME = clsCommon.myCstr(dt.Rows(0)("BANK_NAME"))
            obj.PF_NO = clsCommon.myCstr(dt.Rows(0)("PF_NO"))
            obj.ESI_NO = clsCommon.myCstr(dt.Rows(0)("ESI_NO"))
            obj.OT_CODE = clsCommon.myCstr(dt.Rows(0)("OT_CODE"))
            obj.BONUS_CODE = clsCommon.myCstr(dt.Rows(0)("BONUS_CODE"))
            obj.REVISION_NO = clsCommon.myCdbl(dt.Rows(0)("REVISION_NO"))
            obj.PF_APPLICABLE = clsCommon.myCBool(dt.Rows(0)("IS_PF_APPL"))
            obj.ESI_APPLICABLE = clsCommon.myCBool(dt.Rows(0)("IS_ESI_APPL"))
            obj.OT_APPLICABLE = clsCommon.myCBool(dt.Rows(0)("IS_OT_APPL"))
            obj.Professional_Tax_Applicable = (clsCommon.myCdbl(dt.Rows(0)("Professional_Tax_Applicable")) = 1)
            obj.BONUS_APPLICABLE = clsCommon.myCBool(dt.Rows(0)("IS_BONUS_APPL"))
            obj.EPS_TO_EPF = clsCommon.myCBool(dt.Rows(0)("EPS_TO_EPF"))
            obj.WORKING_STATUS = clsCommon.myCstr(dt.Rows(0)("WORKING_STATUS"))
            '' for kdil and viney
            obj.SHIFT_CODE = clsCommon.myCstr(dt.Rows(0)("SHIFT_CODE"))
            obj.SHIFT_CHANG_TYPE = clsCommon.myCstr(dt.Rows(0)("SHIFT_CHANG_TYPE"))
            obj.CONV_RATE_CODE = clsCommon.myCstr(dt.Rows(0)("CONV_RATE_CODE"))
            obj.CONV_TYPE = clsCommon.myCstr(dt.Rows(0)("CONV_TYPE"))
            obj.IS_OD_APPL = clsCommon.myCBool(dt.Rows(0)("IS_OD_APPL"))
            '' end for kdil and viney
            obj.Max_Amount_EPF = clsCommon.myCdbl(dt.Rows(0)("Max_Amount_EPF"))
            obj.Max_Amount_ESI = clsCommon.myCdbl(dt.Rows(0)("Max_Amount_ESI"))

            obj.EPF_Rate = clsCommon.myCdbl(dt.Rows(0)("EPF_Rate"))
            obj.ESI_Rate = clsCommon.myCdbl(dt.Rows(0)("ESI_Rate"))
            obj.Pf_Calculation_Type = clsCommon.myCstr(dt.Rows(0)("Pf_Calculation_Type"))
            obj.objList = clsEmployeeStatusWeeklyOff.GetData(obj.Code, trans)
        End If
        Return obj


    End Function
    Public Function SaveData(ByVal obj As clsEmployeeStatus, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            SaveData(obj, isNewEntry, "", trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function SaveData(ByVal obj As clsEmployeeStatus, ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "", Optional trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean = True

        If isNewEntry Then
            If clsCommon.myLen(strCode) <= 0 Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.APPLICABLE_FROM, "dd/MMM/yyyy"), clsDocType.EmployeeStatus, "", "")
            Else
                obj.Code = strCode
            End If
        End If

        Dim chkRevisionNo As Integer = clsDBFuncationality.getSingleValue("select REVISION_NO from TSPL_EMPLOYEE_STATUS where EMP_CODE='" & obj.EMP_CODE & "' and REVISION_NO>0 ", trans)
        If clsCommon.CompairString(chkRevisionNo, obj.REVISION_NO) <> CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow("Employee Status Code: " & obj.Code & " and Latest Revision No : " & chkRevisionNo & " Is Already Generated so Data not update")
            Return False
        End If

        If isNewEntry Then
            If clsCommon.CompairString(chkRevisionNo, obj.REVISION_NO) = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("Revision No : " & chkRevisionNo & " Is Already Generated for Employee : " & obj.EMP_CODE & "")
                Return False
            End If
        End If

        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
        clsCommon.AddColumnsForChange(coll, "APPLICABLE_FROM", clsCommon.GetPrintDate(obj.APPLICABLE_FROM, "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "DESIGNATION_ID", obj.DESIGNATION_ID, True)
        clsCommon.AddColumnsForChange(coll, "DEPARTMENT_CODE", obj.DEPARTMENT_CODE, True)
        clsCommon.AddColumnsForChange(coll, "WORKING_STATUS", obj.WORKING_STATUS)
        If clsCommon.myLen(obj.REPORTING_PERSON) > 0 Then
            clsCommon.AddColumnsForChange(coll, "REPORTING_PERSON_CODE", obj.REPORTING_PERSON)
        End If

        clsCommon.AddColumnsForChange(coll, "BANK_ACC_NO", obj.BANK_ACC_NO)
        clsCommon.AddColumnsForChange(coll, "NAME_IN_ACCOUNT", obj.NAME_IN_ACC)
        clsCommon.AddColumnsForChange(coll, "PAYMENT_MODE", obj.PAYMENT_MODE)
        If clsCommon.myLen(obj.BANK_CODE) > 0 Then
            clsCommon.AddColumnsForChange(coll, "BANK_CODE", obj.BANK_CODE)
        End If
        If clsCommon.myLen(obj.ATTENDANCE_CODE) > 0 Then
            clsCommon.AddColumnsForChange(coll, "ATTENDANCE_CODE", obj.ATTENDANCE_CODE)
        End If
        If clsCommon.myLen(obj.GRADE_CODE) > 0 Then
            clsCommon.AddColumnsForChange(coll, "GRADE_CODE", obj.GRADE_CODE)
        End If
        If clsCommon.myLen(obj.DEVISION_CODE) > 0 Then
            clsCommon.AddColumnsForChange(coll, "DEVISION_CODE", obj.DEVISION_CODE)
        End If

        If clsCommon.myLen(obj.LOCATION_CODE) > 0 Then
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE)
        End If

        clsCommon.AddColumnsForChange(coll, "REVISION_NO", obj.REVISION_NO)
        clsCommon.AddColumnsForChange(coll, "IS_PF_APPL", obj.PF_APPLICABLE)
        clsCommon.AddColumnsForChange(coll, "PF_NO", obj.PF_NO)

        clsCommon.AddColumnsForChange(coll, "IS_ESI_APPL", obj.ESI_APPLICABLE)
        clsCommon.AddColumnsForChange(coll, "ESI_NO", obj.ESI_NO)

        clsCommon.AddColumnsForChange(coll, "IS_OT_APPL", obj.OT_APPLICABLE)
        clsCommon.AddColumnsForChange(coll, "Professional_Tax_Applicable", IIf(obj.Professional_Tax_Applicable, 1, 0))
        If clsCommon.myLen(obj.OT_CODE) > 0 Then
            clsCommon.AddColumnsForChange(coll, "OT_CODE", obj.OT_CODE)
        End If

        clsCommon.AddColumnsForChange(coll, "EPS_TO_EPF", obj.EPS_TO_EPF)

        clsCommon.AddColumnsForChange(coll, "IS_BONUS_APPL", obj.BONUS_APPLICABLE)
        If clsCommon.myLen(obj.BONUS_CODE) > 0 Then
            clsCommon.AddColumnsForChange(coll, "BONUS_CODE", obj.BONUS_CODE)
        End If

        '' for viney and kdil
        clsCommon.AddColumnsForChange(coll, "SHIFT_CODE", obj.SHIFT_CODE, True)
        clsCommon.AddColumnsForChange(coll, "SHIFT_CHANG_TYPE", obj.SHIFT_CHANG_TYPE)
        clsCommon.AddColumnsForChange(coll, "CONV_RATE_CODE", obj.CONV_RATE_CODE, True)
        clsCommon.AddColumnsForChange(coll, "CONV_TYPE", obj.CONV_TYPE)
        clsCommon.AddColumnsForChange(coll, "IS_OD_APPL", obj.IS_OD_APPL)
        '' end vney and kdil

        '' new columns for max amount epf and esi 
        clsCommon.AddColumnsForChange(coll, "Max_Amount_EPF", obj.Max_Amount_EPF)
        clsCommon.AddColumnsForChange(coll, "Max_Amount_ESI", obj.Max_Amount_ESI)

        clsCommon.AddColumnsForChange(coll, "EPF_Rate", obj.EPF_Rate)
        clsCommon.AddColumnsForChange(coll, "ESI_Rate", obj.ESI_Rate)
        '===
        clsCommon.AddColumnsForChange(coll, "Pf_Calculation_Type", obj.Pf_Calculation_Type)
        '' new columns for max amount epf and esi

        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
        If isNewEntry Then
            If clsCommon.myLen(obj.Code) < 1 Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.EmployeeStatus, "", "")
            End If

            clsCommon.AddColumnsForChange(coll, "EMP_STATUS_CODE", obj.Code)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            Dim qry As String = "SELECT Count(*) FROM TSPL_EMPLOYEE_STATUS where EMP_STATUS_CODE= '" & obj.Code & "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            If check = 0 Then
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_STATUS", OMInsertOrUpdate.Insert, "", trans)

                ''Add Employee in Leave Allotment Detail
                'qry = "select LVALLOTMENT_CODE from TSPL_LEAVE_ALLOTMENT where ALLOTMENT_DATE>='" + clsCommon.GetPrintDate(obj.APPLICABLE_FROM, "dd/MMM/yyyy") + "' and ALLOTMENT_DATE<='" + clsCommon.GetPrintDate(obj.APPLICABLE_FROM, "dd/MMM/yyyy") + "' and Location_Code='" + obj.LOCATION_CODE + "' and Document_Type='O'"
                'Dim tDt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                'If tDt IsNot Nothing AndAlso tDt.Rows.Count > 0 Then
                '    Dim strAllocationCode As String = clsCommon.myCstr(tDt.Rows(0)("LVALLOTMENT_CODE"))
                '    qry = "select * from TSPL_LEAVE_ALLOTMENTDETAIL where LVALLOTMENT_CODE in ('" + strAllocationCode + "') and EMP_CODE='" + obj.EMP_CODE + "'"
                '    tDt = clsDBFuncationality.GetDataTable(qry, trans)
                '    If tDt IsNot Nothing AndAlso tDt.Rows.Count > 0 Then

                '    End If
                'End If
                '    ''End of Add Employee in Leave Allotment

            Else
                Throw New Exception("This Code Is Already Exist")
            End If
        Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_STATUS", OMInsertOrUpdate.Update, "EMP_STATUS_CODE='" + obj.Code + "'", trans)
        End If


        Dim strq As String

        strq = ""
        strq += "UPDATE TSPL_EMPLOYEE_MASTER SET Designation=T1.DESIGNATION_ID,DEPARTMENT_CODE=T1.DEPARTMENT_CODE,LOCATION_CODE=T1.LOCATION_CODE, "
        strq += "ATTENDANCE_CODE=T1.ATTENDANCE_CODE,BANK_ACC_NO=T1.BANK_ACC_NO,PAYMENT_MODE=T1.PAYMENT_MODE,DEVISION_CODE=T1.DEVISION_CODE, "
        strq += "BANK_CODE=T1.BANK_CODE,PF_NO=T1.PF_NO,ISPF=T1.IS_PF_APPL,ISESI=T1.IS_ESI_APPL,ESI_NO=T1.ESI_NO, "
        strq += "REL_DATE=(CASE WHEN T1.WORKING_STATUS='Resigned' THEN T1.APPLICABLE_FROM END),RELIEVING_DATE=CONVERT(DATE,(CASE WHEN T1.WORKING_STATUS='Resigned' THEN T1.APPLICABLE_FROM ELSE NULL END),103),CONV_TYPE=T1.CONV_TYPE,IS_OD_APPL=T1.IS_OD_APPL,Max_Amount_EPF = t1. Max_Amount_EPF,EPF_Rate = t1.EPF_Rate ,PF_Calculation_Type=t1.PF_Calculation_Type FROM ( "
        strq += "SELECT T1.EMP_CODE,T1.REVISION_NO,T2.LOCATION_CODE,T2.DESIGNATION_ID,T2.DEVISION_CODE,T2.DEPARTMENT_CODE, "
        strq += "T2.GRADE_CODE,T2.ATTENDANCE_CODE,T2.BANK_ACC_NO,T2.NAME_IN_ACCOUNT,T2.PAYMENT_MODE,T2.BANK_CODE,T2.IS_PF_APPL,T2.PF_NO,IS_ESI_APPL, "
        strq += "T2.ESI_NO,IS_OT_APPL,T2.OT_CODE,T2.IS_BONUS_APPL,T2.BONUS_CODE,T2.WORKING_STATUS,T2.EPS_TO_EPF,T2.APPLICABLE_FROM,T2.CONV_TYPE,T2.IS_OD_APPL,t2.Max_Amount_EPF ,t2.EPF_Rate ,t2.PF_Calculation_Type FROM ( "
        strq += "SELECT EMP_CODE,MAX(REVISION_NO) AS REVISION_NO FROM TSPL_EMPLOYEE_STATUS "
        strq += "GROUP BY EMP_CODE) AS T1 JOIN TSPL_EMPLOYEE_STATUS T2 ON T1.EMP_CODE=T2.EMP_CODE AND T1.REVISION_NO=T2.REVISION_NO) AS T1 "
        strq += "WHERE TSPL_EMPLOYEE_MASTER.EMP_CODE=T1.EMP_CODE AND TSPL_EMPLOYEE_MASTER.EMP_CODE='" & obj.EMP_CODE & "'"

        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(strq, trans)
        strq = "delete from TSPL_EMPLOYEE_WEEKLY_OFF where EMP_STATUS_CODE='" & obj.Code & "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(strq, trans)
        '' saving wekly off
        isSaved = isSaved AndAlso clsEmployeeStatusWeeklyOff.SaveData(obj.Code, obj.objList, trans)

        Return isSaved
    End Function

    Public Function SaveData_FromEmpMaster(ByVal obj As clsEmployeeMaster, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Dim strCode As String = ""
        Dim strStatusCode As String = ""
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
            clsCommon.AddColumnsForChange(coll, "APPLICABLE_FROM", clsCommon.GetPrintDate(obj.Joining_date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "DESIGNATION_ID", obj.Designation, True)
            clsCommon.AddColumnsForChange(coll, "DEPARTMENT_CODE", obj.DEPARTMENT_CODE, True)
            'clsCommon.AddColumnsForChange(coll, "WORKING_STATUS", obj.WORKING_STATUS)
            'If clsCommon.myLen(obj.REPORTING_PERSON) > 0 Then
            '    clsCommon.AddColumnsForChange(coll, "REPORTING_PERSON_CODE", obj.REPORTING_PERSON)
            'End If
            clsCommon.AddColumnsForChange(coll, "BANK_ACC_NO", obj.BANK_ACC_NO)
            'clsCommon.AddColumnsForChange(coll, "NAME_IN_ACCOUNT", obj.NAME_IN_ACC)
            clsCommon.AddColumnsForChange(coll, "PAYMENT_MODE", obj.PAYMENT_MODE)
            If clsCommon.myLen(obj.BANK_CODE) > 0 Then
                clsCommon.AddColumnsForChange(coll, "BANK_CODE", obj.BANK_CODE)
            End If
            If clsCommon.myLen(obj.ATTENDANCE_CODE) > 0 Then
                clsCommon.AddColumnsForChange(coll, "ATTENDANCE_CODE", obj.ATTENDANCE_CODE)
            End If
            If clsCommon.myLen(obj.GRADE_CODE) > 0 Then
                clsCommon.AddColumnsForChange(coll, "GRADE_CODE", obj.GRADE_CODE)
            End If
            If clsCommon.myLen(obj.DEVISION_CODE) > 0 Then
                clsCommon.AddColumnsForChange(coll, "DEVISION_CODE", obj.DEVISION_CODE)
            End If
            If clsCommon.myLen(obj.LOCATION_CODE) > 0 Then
                clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE)
            End If
            'clsCommon.AddColumnsForChange(coll, "REVISION_NO", 1)
            clsCommon.AddColumnsForChange(coll, "IS_PF_APPL", obj.ISPF)
            clsCommon.AddColumnsForChange(coll, "PF_NO", obj.PF_NO)
            clsCommon.AddColumnsForChange(coll, "IS_ESI_APPL", obj.ISESI)
            clsCommon.AddColumnsForChange(coll, "ESI_NO", obj.ESI_NO)
            clsCommon.AddColumnsForChange(coll, "IS_OT_APPL", 0)
            'If clsCommon.myLen(obj.OT_CODE) > 0 Then
            '    clsCommon.AddColumnsForChange(coll, "OT_CODE", obj.OT_CODE)
            'End If
            clsCommon.AddColumnsForChange(coll, "IS_BONUS_APPL", 0)
            'If clsCommon.myLen(obj.BONUS_CODE) > 0 Then
            '    clsCommon.AddColumnsForChange(coll, "BONUS_CODE", obj.BONUS_CODE)
            'End If

            '' for viney and kdil
            clsCommon.AddColumnsForChange(coll, "SHIFT_CODE", obj.SHIFT_CODE, True)
            clsCommon.AddColumnsForChange(coll, "CONV_TYPE", obj.CONV_TYPE)
            clsCommon.AddColumnsForChange(coll, "IS_OD_APPL", obj.IS_OD_APPL)
            ''===
            clsCommon.AddColumnsForChange(coll, "Pf_Calculation_Type", obj.Pf_Calculation_Type)
            clsCommon.AddColumnsForChange(coll, "Max_Amount_EPF", obj.Max_Amount_EPF)
            clsCommon.AddColumnsForChange(coll, "EPF_Rate", obj.EPF_Rate)

            '' end vney and kdil

            ' '' new columns for max amount epf and esi 
            'clsCommon.AddColumnsForChange(coll, "Max_Amount_EPF", obj.Max_Amount_EPF)
            'clsCommon.AddColumnsForChange(coll, "Max_Amount_ESI", obj.Max_Amount_ESI)
            ' '' new columns for max amount epf and esi

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            'strCode = clsERPFuncationality.GetNextCode(Nothing, clsCommon.myCDate(clsCommon.GETSERVERDATE()), clsDocType.EmployeeStatus, "", "")
            'clsCommon.AddColumnsForChange(coll, "EMP_STATUS_CODE", strCode)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            'Dim qry As String = "SELECT Count(*) FROM TSPL_EMPLOYEE_STATUS where EMP_STATUS_CODE= '" & strCode & "'"
            'Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            'If check = 0 Then
            '    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_STATUS", OMInsertOrUpdate.Insert, "")
            'Else
            '    common.clsCommon.MyMessageBoxShow("This Code Is Already Exist")
            '    Exit Function
            'End If

            Dim qry As String = "SELECT max(Revision_No) as Revision_No FROM TSPL_EMPLOYEE_STATUS where EMP_CODE= '" & obj.EMP_CODE & "' "
            REVISION_NO = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            If REVISION_NO = 0 Then
                REVISION_NO = 1
                clsCommon.AddColumnsForChange(coll, "REVISION_NO", REVISION_NO)
                strStatusCode = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.EmployeeStatus, "", "")
                clsCommon.AddColumnsForChange(coll, "EMP_STATUS_CODE", strStatusCode)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_STATUS", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_STATUS", OMInsertOrUpdate.Update, "EMP_CODE='" & obj.EMP_CODE & "' and REVISION_NO='" & REVISION_NO & "'", trans)
                'common.clsCommon.MyMessageBoxShow("This Code Is Already Exist")
                'Exit Function
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetCboShiftChangeTypeDataTable() As DataTable
        Dim DT As DataTable = New DataTable
        DT.Columns.Add("Code", GetType(String))
        DT.Columns.Add("Name", GetType(String))
        Dim DR As DataRow = DT.NewRow()
        DR("Code") = "Weekly"
        DR("Name") = "Weekly"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Code") = "Monthly"
        DR("Name") = "Monthly"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Code") = "Fort Nightly"
        DR("Name") = "Fort Nightly"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Code") = "Never"
        DR("Name") = "Never"
        DT.Rows.Add(DR)
        DT.AcceptChanges()
        Return DT
    End Function
    Public Shared Function GetEmployeeStatus(ByVal EMP_CODE As String, ByVal ApplicableFrom As Date, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String
        Dim EMP_STATUS_CODE As String = ""
        Dim dt As DataTable
        qry = "select MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE from TSPL_EMPLOYEE_STATUS  WHERE EMP_CODE='" & EMP_CODE & "' AND APPLICABLE_FROM<='" & clsCommon.GetPrintDate(ApplicableFrom, "dd/MMM/yyyy") & "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            EMP_STATUS_CODE = dt.Rows(0).Item("EMP_STATUS_CODE")
        Else
            EMP_STATUS_CODE = ""
        End If
        Return EMP_STATUS_CODE
    End Function
    Public Shared Function GetEmployeeLatestStatus(ByVal EMP_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String
        Dim EMP_STATUS_CODE As String = ""
        Dim dt As DataTable
        qry = "select top 1 EMP_STATUS_CODE from TSPL_EMPLOYEE_STATUS where EMP_CODE='" & EMP_CODE & "' order by REVISION_NO desc "
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            EMP_STATUS_CODE = dt.Rows(0).Item("EMP_STATUS_CODE")
        Else
            EMP_STATUS_CODE = ""
        End If
        Return EMP_STATUS_CODE
    End Function
End Class
Public Class clsEmployeeStatusWeeklyOff
#Region "Variables"
    Public EMP_STATUS_CODE As String
    Public WEEK_OFF_CODE As String
    Public EMP_CODE As String
    Public WKHOLIDAY_CODE As String
#End Region
#Region "Functions"
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsEmployeeStatusWeeklyOff)
        Dim objList As New List(Of clsEmployeeStatusWeeklyOff)
        Dim objTr As New clsEmployeeStatusWeeklyOff
        Dim qry As String = ""
        qry += " select * FROM TSPL_EMPLOYEE_WEEKLY_OFF WHERE EMP_STATUS_CODE='" & strCode & "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objTr = New clsEmployeeStatusWeeklyOff()
                objTr.WEEK_OFF_CODE = GetWKCode(trans)
                objTr.EMP_STATUS_CODE = clsCommon.myCstr(dr("EMP_STATUS_CODE"))
                objTr.EMP_CODE = clsCommon.myCstr(dr("EMP_CODE"))
                objTr.WKHOLIDAY_CODE = clsCommon.myCstr(dr("WKHOLIDAY_CODE"))
                objList.Add(objTr)
            Next

        End If
        Return objList
    End Function
    Public Shared Function GetWKCode(ByVal trans As SqlTransaction) As String
        Dim qry As String = "select (coalesce(max(WEEK_OFF_CODE),0)+1) as WEEK_OFF_CODE from TSPL_EMPLOYEE_WEEKLY_OFF "
        Return clsDBFuncationality.getSingleValue(qry, trans)
    End Function

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsEmployeeStatusWeeklyOff), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsEmployeeStatusWeeklyOff In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "EMP_STATUS_CODE", strCode)
                    clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
                    clsCommon.AddColumnsForChange(coll, "WEEK_OFF_CODE", GetWKCode(trans))
                    clsCommon.AddColumnsForChange(coll, "WKHOLIDAY_CODE", obj.WKHOLIDAY_CODE)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_WEEKLY_OFF", OMInsertOrUpdate.Insert, "TSPL_EMPLOYEE_WEEKLY_OFF.EMP_STATUS_CODE='" + strCode + "'", trans)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
#End Region
End Class
