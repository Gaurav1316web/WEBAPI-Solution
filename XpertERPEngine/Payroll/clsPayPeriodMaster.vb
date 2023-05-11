Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsPayPeriodMaster

#Region "Variables"
    Public Code As String
    Public Description As String
    Public Name As String
    Public DATE_FROM As DateTime
    Public DATE_TO As DateTime
    Public POSTED As Boolean
    Public FREEZED As Boolean
    Public Posting_Date As DateTime
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE as [Code] ,TSPL_PAYPERIOD_MASTER.PAY_PERIOD_NAME as [Pay Period Name] ,TSPL_PAYPERIOD_MASTER.DATE_FROM as [Date From] ,TSPL_PAYPERIOD_MASTER.DATE_TO as [Date To] ,TSPL_PAYPERIOD_MASTER.DESCRIPTION as [Description] ,TSPL_PAYPERIOD_MASTER.POSTED as [Posted] ,TSPL_PAYPERIOD_MASTER.FREEZED as [Freezed] ,TSPL_PAYPERIOD_MASTER.Posting_Date as [Posting Date] ,TSPL_PAYPERIOD_MASTER.Created_By as [Created By] ,TSPL_PAYPERIOD_MASTER.Created_Date as [Created Date] ,TSPL_PAYPERIOD_MASTER.Modified_By as [Modified By] ,TSPL_PAYPERIOD_MASTER.Modified_Date as [Modified Date]  From TSPL_PAYPERIOD_MASTER  "
        str = clsCommon.ShowSelectForm("PAYPRDMST", qry, "Code", whrcls, curcode, "DATE_FROM", isButtonClicked)
        Return str
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "select POSTED from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) as cnt from TSPL_MONTHLY_ATTENDANCE where PAY_PERIOD_CODE='" & strCode & "'", trans)

            If check > 0 Then
                Throw New Exception("Pay Periods is already used to Transaction.")
            Else
                Qry = "Update TSPL_PAYPERIOD_MASTER set POSTED = 0 where PAY_PERIOD_CODE='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsPayPeriodMaster
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String
            qry = "delete from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsPayPeriodMaster
        Dim obj As clsPayPeriodMaster = Nothing
        Dim qry As String = "select PAY_PERIOD_CODE, PAY_PERIOD_NAME, DATE_FROM, DATE_TO, DESCRIPTION,POSTED,Posting_Date  from TSPL_PAYPERIOD_MASTER where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and PAY_PERIOD_CODE = (select MIN(PAY_PERIOD_CODE) from TSPL_PAYPERIOD_MASTER)"
            Case NavigatorType.Last
                qry += " and PAY_PERIOD_CODE = (select Max(PAY_PERIOD_CODE) from TSPL_PAYPERIOD_MASTER)"
            Case NavigatorType.Next
                qry += " and PAY_PERIOD_CODE = (select Min(PAY_PERIOD_CODE) from TSPL_PAYPERIOD_MASTER where  PAY_PERIOD_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and PAY_PERIOD_CODE = (select Max(PAY_PERIOD_CODE) from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and PAY_PERIOD_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPayPeriodMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.Name = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_NAME"))
            obj.DATE_FROM = clsCommon.myCDate(dt.Rows(0)("DATE_FROM"))
            obj.DATE_TO = clsCommon.myCDate(dt.Rows(0)("DATE_TO"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsPayPeriodMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim strTest As String = CheckNameExistness(obj.Name, obj.Code, Nothing)
            If clsCommon.myLen(strTest) > 0 Then
                Throw New Exception("Name Allready Exist in Pay Period Code : " + strTest + ". Please Choose another  Name.")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_NAME", obj.Name)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.Description)
            clsCommon.AddColumnsForChange(coll, "DATE_FROM", clsCommon.GetPrintDate(obj.DATE_FROM, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "DATE_TO", clsCommon.GetPrintDate(obj.DATE_TO, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & obj.Code & "'")
                    If ChkNewEntry = 0 Then
                        obj.Code = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.PayPeriodMaster, "", "")
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE= '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYPERIOD_MASTER", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYPERIOD_MASTER", OMInsertOrUpdate.Update, "PAY_PERIOD_CODE='" + obj.Code + "'")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsPayPeriodMaster = clsPayPeriodMaster.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim qry As String = "Update TSPL_PAYPERIOD_MASTER set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where PAY_PERIOD_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetName(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select PAY_PERIOD_NAME from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + strCode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function CheckNameExistness(ByVal strName As String, ByVal strExCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select PAY_PERIOD_CODE from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_NAME ='" + strName + "'  and PAY_PERIOD_CODE <> '" + strExCode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetLastDay(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select DAY(DATE_TO) as 'Day' from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + strCode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetToDate(ByVal strCode As String, ByVal trans As SqlTransaction) As Date
        Dim qry As String = "select DATE_TO  from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + strCode + "' "
        Return clsCommon.myCDate(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetFromDate(ByVal strCode As String, ByVal trans As SqlTransaction) As Date
        Dim qry As String = "select DATE_FROM  from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + strCode + "' "
        Return clsCommon.myCDate(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function CheckNewPayPeriod(ByVal PayPeriodCode As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select PAY_PERIOD_CODE from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + PayPeriodCode + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Shared Function GetPreviousPayPeriod(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "SELECT TOP 1 PAY_PERIOD_CODE FROM TSPL_PAYPERIOD_MASTER WHERE DATE_FROM< " & _
            " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strCode & "') ORDER BY DATE_FROM DESC"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
End Class
