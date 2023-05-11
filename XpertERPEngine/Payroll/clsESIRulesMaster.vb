Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsESIRulesMaster

#Region "Variables"
    Public Code As String
    Public APPLICABLE_FROM As DateTime
    Public COESI_PER As Double
    Public EMPESI_PER As Double
    Public COESI_ROUNDOFF_YPE As String
    Public TOTALEARNING_MAX As Double

#End Region


    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_ESI_RULE_MASTER.ESIRULE_CODE as [Code] ,TSPL_ESI_RULE_MASTER.APPLICABLE_FROM as [Applicable From] ,TSPL_ESI_RULE_MASTER.COESI_PER as [Coesi Per] ,TSPL_ESI_RULE_MASTER.EMPESI_PER as [Empesi Per] ,TSPL_ESI_RULE_MASTER.COESI_ROUNDOFF_YPE as [Coesi Roundoff Ype] ,TSPL_ESI_RULE_MASTER.TOTALEARNING_MAX as [Totalearning Max] ,TSPL_ESI_RULE_MASTER.Created_By as [Created By] ,TSPL_ESI_RULE_MASTER.Created_Date as [Created Date] ,TSPL_ESI_RULE_MASTER.Modified_By as [Modified By] ,TSPL_ESI_RULE_MASTER.Modified_Date as [Modified Date]  From TSPL_ESI_RULE_MASTER   "
        str = clsCommon.ShowSelectForm("ESIRULEMST", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
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
            qry = "delete from TSPL_ESI_RULE_MASTER where ESIRULE_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsESIRulesMaster
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsESIRulesMaster
        Dim obj As clsESIRulesMaster = Nothing
        Dim qry As String = "select * from TSPL_ESI_RULE_MASTER where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and ESIRULE_CODE = (select MIN(ESIRULE_CODE) from TSPL_ESI_RULE_MASTER)"
            Case NavigatorType.Last
                qry += " and ESIRULE_CODE = (select Max(ESIRULE_CODE) from TSPL_ESI_RULE_MASTER)"
            Case NavigatorType.Next
                qry += " and ESIRULE_CODE = (select Min(ESIRULE_CODE) from TSPL_ESI_RULE_MASTER where  ESIRULE_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and ESIRULE_CODE = (select Max(ESIRULE_CODE) from TSPL_ESI_RULE_MASTER where ESIRULE_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and ESIRULE_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsESIRulesMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("ESIRULE_CODE"))
            obj.APPLICABLE_FROM = clsCommon.myCstr(dt.Rows(0)("APPLICABLE_FROM"))
            obj.COESI_PER = clsCommon.myCdbl(dt.Rows(0)("COESI_PER"))
            obj.COESI_ROUNDOFF_YPE = clsCommon.myCstr(dt.Rows(0)("COESI_ROUNDOFF_YPE"))
            obj.EMPESI_PER = clsCommon.myCdbl(dt.Rows(0)("EMPESI_PER"))
            obj.TOTALEARNING_MAX = clsCommon.myCdbl(dt.Rows(0)("TOTALEARNING_MAX"))
        End If
        Return obj


    End Function

    Public Function SaveData(ByVal obj As clsESIRulesMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "APPLICABLE_FROM ", clsCommon.GetPrintDate(obj.APPLICABLE_FROM, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "COESI_PER", obj.COESI_PER)
            clsCommon.AddColumnsForChange(coll, "COESI_ROUNDOFF_YPE", obj.COESI_ROUNDOFF_YPE)
            clsCommon.AddColumnsForChange(coll, "EMPESI_PER", obj.EMPESI_PER)
            clsCommon.AddColumnsForChange(coll, "TOTALEARNING_MAX", obj.TOTALEARNING_MAX)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_ESI_RULE_MASTER where ESIRULE_CODE='" & obj.Code & "'")
                    If ChkNewEntry = 0 Then
                        obj.Code = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(obj.APPLICABLE_FROM, "dd/MM/yyyy"), clsDocType.ESIRulesMaster, "", "")
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "ESIRULE_CODE", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_ESI_RULE_MASTER where ESIRULE_CODE= '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ESI_RULE_MASTER", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ESI_RULE_MASTER", OMInsertOrUpdate.Update, "ESIRULE_CODE='" + obj.Code + "'")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select ESIRULE_CODE from TSPL_ESI_RULE_MASTER where ESIRULE_CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Shared Function GetRecentESIRule(ByVal PAY_PERIOD_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As clsESIRulesMaster
        Dim obj As clsESIRulesMaster = Nothing
        Dim qry As String = " select ESIRULE_CODE,max(APPLICABLE_FROM) AS APPLICABLE_FROM from TSPL_ESI_RULE_MASTER " & _
                            " where APPLICABLE_FROM<=(select DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "')" & _
                            " GROUP BY ESIRULE_CODE HAVING MAX(APPLICABLE_FROM)<=(select DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "')"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("PF Rule not found for pay period " & PAY_PERIOD_CODE & "")
        Else
            obj = clsESIRulesMaster.GetData(dt.Rows(0).Item("ESIRULE_CODE"), NavigatorType.Current, trans)
        End If
        Return obj
    End Function
End Class
