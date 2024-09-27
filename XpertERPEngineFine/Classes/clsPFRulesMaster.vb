Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsPFRulesMaster

#Region "Variables"
    Public Code As String
    Public APPLICABLE_FROM As DateTime
    Public COEPF_PER As Double
    Public COEPF_ROUNDOFF_YPE As String      '' COMPANY PF SHARE
    Public COEPS_PER As Double               '' COMPANY EPS SHARE
    Public EPS_MAX As Double                '' MAXIMUM LIMIT OF EPS AMOUNT
    Public EMPEPF_PER As Double              '' EMPLOYEE PF SHARE
    Public EMPEPF_MAX As Double             '' MAXIMUM LIMIT OF EMPLOYEE SHARE
    Public EMPEPF_ROUNDOFF_YPE As String     '' ROUND OFF TYPE OF EMPLOYEE PF SHARE
    Public ACCOEPF_PER As Double             '' ADMIN CHARGES ON COMPANY PF SHARE
    Public ACCOEPF_MAX As Double            '' MAXIMUM LIMIT OF ADMIN CHARGES ON COMPANY PF SHARE
    Public COEDLI_PER As Double              '' EMPLOYEE DEPOSIT LINKED INSURANCE PAID BY COMPANY
    Public COEDLI_MAX As Double             '' MAXIMUM LIMIT OF EMPLOYEE DEPOSIT LINKED INSURANCE PAID BY COMPANY
    Public ACCOEDLI_PER As Double            '' ADMIN CHARGES ON EMPLOYEE DEPOSIT LINKED INSURANCE PAID BY COMPANY
    Public ACCOEDLI_MAX As Double           '' MAXIMUM LIMIT OF ADMIN CHARGES ON EMPLOYEE DEPOSIT LINKED INSURANCE PAID BY COMPANY
    Public ACCOEDLI_MIN As Double
    Public OC As Double                     '' OTHER CHARGES
    Public OC_MAX As Double                 '' MAXIMUM LIMIT OF OTHER CHARGES
    Public OTH_ROUNDOFF_YPE As String
    Public Pf_Type As String
    '' ROUND OFF TYPE OF OTHER PF SHARE

#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_PF_RULE_MASTER.PFRULE_CODE as [Code] ,TSPL_PF_RULE_MASTER.APPLICABLE_FROM as [Applicable From] ,TSPL_PF_RULE_MASTER.COEPF_PER as [Coepf Per] ,TSPL_PF_RULE_MASTER.COEPF_ROUNDOFF_YPE as [Coepf Roundoff Ype] ,TSPL_PF_RULE_MASTER.COEPS_PER as [Coeps Per] ,TSPL_PF_RULE_MASTER.EPS_MAX as [Eps Max] ,TSPL_PF_RULE_MASTER.EMPEPF_PER as [Empepf Per] ,TSPL_PF_RULE_MASTER.EMPEPF_MAX as [Empepf Max] ,TSPL_PF_RULE_MASTER.EMPEPF_ROUNDOFF_YPE as [Empepf Roundoff Ype] ,TSPL_PF_RULE_MASTER.ACCOEPF_PER as [Accoepf Per] ,TSPL_PF_RULE_MASTER.ACCOEPF_MAX as [Accoepf Max] ,TSPL_PF_RULE_MASTER.COEDLI_PER as [Coedli Per] ,TSPL_PF_RULE_MASTER.COEDLI_MAX as [Coedli Max] ,TSPL_PF_RULE_MASTER.ACCOEDLI_PER as [Accoedli Per] ,TSPL_PF_RULE_MASTER.ACCOEDLI_MAX as [Accoedli Max] ,TSPL_PF_RULE_MASTER.OC as [Oc] ,TSPL_PF_RULE_MASTER.OC_MAX as [Oc Max] ,TSPL_PF_RULE_MASTER.OTH_ROUNDOFF_YPE as [Oth Roundoff Ype] ,TSPL_PF_RULE_MASTER.Created_By as [Created By] ,TSPL_PF_RULE_MASTER.Created_Date as [Created Date] ,TSPL_PF_RULE_MASTER.Modified_By as [Modified By] ,TSPL_PF_RULE_MASTER.Modified_Date as [Modified Date]  From TSPL_PF_RULE_MASTER  "
        str = clsCommon.ShowSelectForm("PFRULEMST", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_PF_RULE_MASTER where PFRULE_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsPFRulesMaster
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsPFRulesMaster
        Dim obj As clsPFRulesMaster = Nothing
        Dim qry As String = "select * from TSPL_PF_RULE_MASTER where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and PFRULE_CODE = (select MIN(PFRULE_CODE) from TSPL_PF_RULE_MASTER)"
            Case NavigatorType.Last
                qry += " and PFRULE_CODE = (select Max(PFRULE_CODE) from TSPL_PF_RULE_MASTER)"
            Case NavigatorType.Next
                qry += " and PFRULE_CODE = (select Min(PFRULE_CODE) from TSPL_PF_RULE_MASTER where  PFRULE_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and PFRULE_CODE = (select Max(PFRULE_CODE) from TSPL_PF_RULE_MASTER where PFRULE_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and PFRULE_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPFRulesMaster()

            obj.Code = clsCommon.myCstr(dt.Rows(0)("PFRULE_CODE"))
            obj.APPLICABLE_FROM = clsCommon.myCstr(dt.Rows(0)("APPLICABLE_FROM"))
            obj.COEPF_PER = clsCommon.myCdbl(dt.Rows(0)("COEPF_PER"))
            obj.COEPF_ROUNDOFF_YPE = clsCommon.myCstr(dt.Rows(0)("COEPF_ROUNDOFF_YPE"))
            obj.COEPS_PER = clsCommon.myCdbl(dt.Rows(0)("COEPS_PER"))
            obj.EPS_MAX = clsCommon.myCdbl(dt.Rows(0)("EPS_MAX"))
            obj.EMPEPF_PER = clsCommon.myCdbl(dt.Rows(0)("EMPEPF_PER"))
            obj.EMPEPF_MAX = clsCommon.myCdbl(dt.Rows(0)("EMPEPF_MAX"))
            obj.EMPEPF_ROUNDOFF_YPE = clsCommon.myCstr(dt.Rows(0)("EMPEPF_ROUNDOFF_YPE"))
            obj.ACCOEPF_PER = clsCommon.myCdbl(dt.Rows(0)("ACCOEPF_PER"))
            obj.ACCOEPF_MAX = clsCommon.myCdbl(dt.Rows(0)("ACCOEPF_MAX"))
            obj.COEDLI_PER = clsCommon.myCdbl(dt.Rows(0)("COEDLI_PER"))
            obj.COEDLI_MAX = clsCommon.myCdbl(dt.Rows(0)("COEDLI_MAX"))
            obj.ACCOEDLI_PER = clsCommon.myCdbl(dt.Rows(0)("ACCOEDLI_PER"))
            obj.ACCOEDLI_MAX = clsCommon.myCdbl(dt.Rows(0)("ACCOEDLI_MAX"))
            obj.ACCOEDLI_MIN = clsCommon.myCdbl(dt.Rows(0)("ACCOEDLI_MIN"))
            obj.OC = clsCommon.myCdbl(dt.Rows(0)("OC"))
            obj.OC_MAX = clsCommon.myCdbl(dt.Rows(0)("OC_MAX"))
            obj.OTH_ROUNDOFF_YPE = clsCommon.myCstr(dt.Rows(0)("OTH_ROUNDOFF_YPE"))
            obj.Pf_Type = clsCommon.myCstr(dt.Rows(0)("Pf_Type"))

        End If
        Return obj


    End Function

    Public Function SaveData(ByVal obj As clsPFRulesMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "APPLICABLE_FROM ", clsCommon.GetPrintDate(obj.APPLICABLE_FROM, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "COEPF_PER", obj.COEPF_PER)
            clsCommon.AddColumnsForChange(coll, "COEPF_ROUNDOFF_YPE", obj.COEPF_ROUNDOFF_YPE)
            clsCommon.AddColumnsForChange(coll, "COEPS_PER", obj.COEPS_PER)
            clsCommon.AddColumnsForChange(coll, "EPS_MAX", obj.EPS_MAX)
            clsCommon.AddColumnsForChange(coll, "EMPEPF_PER", obj.EMPEPF_PER)
            clsCommon.AddColumnsForChange(coll, "EMPEPF_MAX", obj.EMPEPF_MAX)
            clsCommon.AddColumnsForChange(coll, "EMPEPF_ROUNDOFF_YPE", obj.EMPEPF_ROUNDOFF_YPE)
            clsCommon.AddColumnsForChange(coll, "ACCOEPF_PER", obj.ACCOEPF_PER)
            clsCommon.AddColumnsForChange(coll, "ACCOEPF_MAX", obj.ACCOEPF_MAX)
            clsCommon.AddColumnsForChange(coll, "COEDLI_PER", obj.COEDLI_PER)
            clsCommon.AddColumnsForChange(coll, "COEDLI_MAX", obj.COEDLI_MAX)
            clsCommon.AddColumnsForChange(coll, "ACCOEDLI_PER", obj.ACCOEDLI_PER)
            clsCommon.AddColumnsForChange(coll, "ACCOEDLI_MAX", obj.ACCOEDLI_MAX)
            clsCommon.AddColumnsForChange(coll, "ACCOEDLI_MIN", obj.ACCOEDLI_MIN)
            clsCommon.AddColumnsForChange(coll, "OC", obj.OC)
            clsCommon.AddColumnsForChange(coll, "OC_MAX", obj.OC_MAX)
            clsCommon.AddColumnsForChange(coll, "OTH_ROUNDOFF_YPE", obj.OTH_ROUNDOFF_YPE)
            clsCommon.AddColumnsForChange(coll, "Pf_Type", obj.Pf_Type)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_PF_RULE_MASTER where PFRULE_CODE='" & obj.Code & "'")
                    If ChkNewEntry = 0 Then
                        obj.Code = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(obj.APPLICABLE_FROM, "dd/MM/yyyy"), clsDocType.PFRulesMaster, "", "")
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "PFRULE_CODE", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_PF_RULE_MASTER where PFRULE_CODE= '" & obj.Code & "'"
                Dim check As Double = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PF_RULE_MASTER", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PF_RULE_MASTER", OMInsertOrUpdate.Update, "PFRULE_CODE='" + obj.Code + "'")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select PFRULE_CODE from TSPL_PF_RULE_MASTER where PFRULE_CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Shared Function GetRecentPFRule(ByVal PAY_PERIOD_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As clsPFRulesMaster
        Dim obj As clsPFRulesMaster = Nothing
        Dim qry As String = " select PFRULE_CODE,max(APPLICABLE_FROM) AS APPLICABLE_FROM from TSPL_PF_RULE_MASTER " & _
                            " where APPLICABLE_FROM<=(select DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "')" & _
                            " GROUP BY PFRULE_CODE HAVING MAX(APPLICABLE_FROM)<=(select DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "')"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("PF Rule not found for pay period " & PAY_PERIOD_CODE & "")
        Else
            obj = clsPFRulesMaster.GetData(dt.Rows(0).Item("PFRULE_CODE"), NavigatorType.Current, trans)
        End If
        Return obj
    End Function
    Public Shared Function GetRecentPFRuleMult(ByVal PAY_PERIOD_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As clsPFRulesMaster
        Dim obj As clsPFRulesMaster = Nothing
        Dim qry As String = " select PFRULE_CODE,max(APPLICABLE_FROM) AS APPLICABLE_FROM from TSPL_PF_RULE_MASTER " & _
                            " where APPLICABLE_FROM<=(select top 1 DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE in (" & PAY_PERIOD_CODE.Replace("[", "").Replace("]", "") & "))" & _
                            " GROUP BY PFRULE_CODE HAVING MAX(APPLICABLE_FROM)<=(select top 1 DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE in (" & PAY_PERIOD_CODE.Replace("[", "").Replace("]", "") & "))"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("PF Rule not found for pay period " & PAY_PERIOD_CODE & "")
        Else
            obj = clsPFRulesMaster.GetData(dt.Rows(0).Item("PFRULE_CODE"), NavigatorType.Current, trans)
        End If
        Return obj
    End Function
End Class
