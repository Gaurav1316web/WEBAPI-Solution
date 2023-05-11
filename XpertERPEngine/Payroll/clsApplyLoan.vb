Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsApplyLoan


#Region "Variables"

    Public LOAN_CODE As String
    Public LOAN_DATE As Date
    Public LOAN_BY_CODE As String
    Public LOAN_BY_NAME As String
    Public EMP_CODE As String
    Public EMP_NAME As String
    Public LOAN_TYPE As String
    Public LOAN_AMOUNT As Decimal
    Public PERIOD_MONTH As Integer
    Public PERIOD_DAY As Integer
    Public PAYMENT_STARTDATE As Date
    Public NO_OF_EMI As Integer
    Public INTEREST_APPLIED As Boolean
    Public INTEREST_TYPE As String
    Public INTEREST_PERIODICITY As String
    Public INTEREST_RATE As Decimal
    Public INTEREST_AMOUNT As Decimal
    Public TOTALPAYABLE_AMOUNT As Decimal
    Public LOAN_DESCRIPTION As String
    Public POSTED As Decimal
    Public Posting_Date As DateTime
    Public Location As String
    Public Division As String
    Public Payment_EndDate As Date
    Public Gross_Salary As Decimal
    Public Loan_Status As String
    '' grid columns

    Public EMI_NO As Integer
    Public EMI_AMOUNT As Decimal
    Public PAID As Decimal

    Public Shared ObjList As List(Of clsApplyLoan)
    Public Arr As New List(Of clsLoanEMIDetail)

#End Region
    '=====finder
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select Loan_Code as Code ,convert(varchAR,Loan_Date,103) as Date,Emp_Code as [Emp Code],Loan_Type as [Loan Type] ,Loan_Amount as [Loan Amount],PERIOD_MONTH ,PERIOD_DAY ,convert(varchar,PAYMENT_STARTDATE,103)as [Payment Start Date],convert(varchar,Payment_EndDate,103) as [Payment End Date] ,NO_OF_EMI as [No. Of EMI],INTEREST_APPLIED as [Interest Applied],INTEREST_TYPE as [Interest Type] ,INTEREST_PERIODICITY as [Interest Periodicity],INTEREST_RATE as [Interest Rate],INTEREST_AMOUNT as [Interest Amount] ,TOTALPAYABLE_AMOUNT as [Total Amount],Location,Division ,Gross_Salary as [Gross Salary] ,Loan_Status as [Loan Status]   from TSPL_LOAN_APPLICATION "
        str = clsCommon.ShowSelectForm("Loan", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsApplyLoan
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_LOANEMI_DETAIL where LOAN_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_LOAN_APPLICATION where LOAN_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsApplyLoan
        Dim obj As New clsApplyLoan()
        Dim objtr As New clsApplyLoan()

        ObjList = New List(Of clsApplyLoan)

        Dim qry As String = "SELECT TLA.*,EMP.Emp_Name AS EMP_NAME,EMP1.Emp_Name AS LOAN_BY_NAME  " _
                        & " FROM TSPL_LOAN_APPLICATION TLA" _
                        & " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON TLA.EMP_CODE=EMP.EMP_CODE " _
                        & " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP1 ON TLA.LOAN_BY=EMP1.EMP_CODE  where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TLA.LOAN_CODE = (select MIN(LOAN_CODE) from TSPL_LOAN_APPLICATION)"
            Case NavigatorType.Last
                qry += " and TLA.LOAN_CODE = (select Max(LOAN_CODE) from TSPL_LOAN_APPLICATION)"
            Case NavigatorType.Next
                qry += " and TLA.LOAN_CODE = (select Min(LOAN_CODE) from TSPL_LOAN_APPLICATION where  LOAN_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TLA.LOAN_CODE = (select Max(LOAN_CODE) from TSPL_LOAN_APPLICATION where LOAN_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TLA.LOAN_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            'obj.SALARY_STRUCTURE_NAME = dt.Rows(0)("SALARY_STRUCTURE_NAME")
            obj.LOAN_CODE = dt.Rows(0)("LOAN_CODE")
            obj.LOAN_DATE = dt.Rows(0)("LOAN_DATE")
            strCode = dt.Rows(0)("LOAN_CODE")
            obj.LOAN_BY_CODE = clsCommon.myCstr(dt.Rows(0)("LOAN_BY"))
            obj.LOAN_BY_NAME = clsCommon.myCstr(dt.Rows(0)("LOAN_BY_NAME"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.EMP_NAME = clsCommon.myCstr(dt.Rows(0)("EMP_NAME"))
            obj.LOAN_TYPE = clsCommon.myCstr(dt.Rows(0)("LOAN_TYPE"))
            obj.LOAN_AMOUNT = clsCommon.myCdbl(dt.Rows(0)("LOAN_AMOUNT"))
            obj.PERIOD_MONTH = clsCommon.myCdbl(dt.Rows(0)("PERIOD_MONTH"))
            obj.PERIOD_DAY = clsCommon.myCdbl(dt.Rows(0)("PERIOD_DAY"))

            obj.PAYMENT_STARTDATE = clsCommon.myCDate(dt.Rows(0)("PAYMENT_STARTDATE"))
            obj.NO_OF_EMI = clsCommon.myCdbl(dt.Rows(0)("NO_OF_EMI"))

            obj.INTEREST_APPLIED = clsCommon.myCBool(dt.Rows(0)("INTEREST_APPLIED"))
            obj.INTEREST_TYPE = clsCommon.myCstr(dt.Rows(0)("INTEREST_TYPE"))
            obj.INTEREST_PERIODICITY = clsCommon.myCstr(dt.Rows(0)("INTEREST_PERIODICITY"))
            obj.INTEREST_RATE = clsCommon.myCdbl(dt.Rows(0)("INTEREST_RATE"))
            obj.INTEREST_AMOUNT = clsCommon.myCdbl(dt.Rows(0)("INTEREST_AMOUNT"))

            obj.TOTALPAYABLE_AMOUNT = clsCommon.myCdbl(dt.Rows(0)("TOTALPAYABLE_AMOUNT"))
            obj.LOAN_DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("LOAN_DESCRIPTION"))
            obj.Location = clsCommon.myCstr(dt.Rows(0)("Location"))
            obj.Division = clsCommon.myCstr(dt.Rows(0)("Division"))
            obj.Payment_EndDate = clsCommon.myCDate(dt.Rows(0)("Payment_EndDate"))
            obj.Gross_Salary = clsCommon.myCdbl(dt.Rows(0)("LOAN_DESCRIPTION"))
            obj.Loan_Status = clsCommon.myCstr(dt.Rows(0)("Loan_Status"))


            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If

        qry = "select TLA.LOAN_CODE,TLD.EMI_NO,TLD.EMI_AMOUNT" _
             & "  FROM TSPL_LOANEMI_DETAIL TLD " _
             & " INNER JOIN  TSPL_LOAN_APPLICATION TLA ON TLD.LOAN_CODE=TLA.LOAN_CODE where 2=2"

        qry += " and TLA.LOAN_CODE = '" + strCode + "'"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsApplyLoan()
                objtr.LOAN_CODE = clsCommon.myCstr(dr("LOAN_CODE"))
                objtr.EMI_NO = clsCommon.myCdbl(dr("EMI_NO"))
                objtr.EMI_AMOUNT = clsCommon.myCdbl(dr("EMI_AMOUNT"))
                ObjList.Add(objtr)
            Next
        End If

        clsApplyLoan.ObjList = ObjList
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsApplyLoan, ByVal objList As List(Of clsApplyLoan), ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Payroll", "Apply Loan", obj.Location, obj.LOAN_DATE, trans)
            If isNewEntry Then
                If strCode = "" Then
                    obj.LOAN_CODE = clsERPFuncationality.GetNextCode(trans, obj.LOAN_DATE, clsDocType.ApplyLoan, "", "")
                Else
                    obj.LOAN_CODE = strCode
                End If
            End If
            Dim qry As String = "delete from TSPL_LOANEMI_DETAIL where LOAN_CODE='" + obj.LOAN_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""

            If (clsCommon.myLen(obj.LOAN_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "LOAN_CODE", obj.LOAN_CODE)
            clsCommon.AddColumnsForChange(coll, "LOAN_DATE", Format(obj.LOAN_DATE, "dd MMM yyyy"))
            clsCommon.AddColumnsForChange(coll, "LOAN_BY", obj.LOAN_BY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
            clsCommon.AddColumnsForChange(coll, "LOAN_TYPE", obj.LOAN_TYPE)
            clsCommon.AddColumnsForChange(coll, "LOAN_AMOUNT", obj.LOAN_AMOUNT)
            clsCommon.AddColumnsForChange(coll, "PERIOD_MONTH", obj.PERIOD_MONTH)
            clsCommon.AddColumnsForChange(coll, "PERIOD_DAY", obj.PERIOD_DAY)
            clsCommon.AddColumnsForChange(coll, "PAYMENT_STARTDATE", Format(obj.PAYMENT_STARTDATE, "dd MMM yyyy"))
            clsCommon.AddColumnsForChange(coll, "NO_OF_EMI", obj.NO_OF_EMI)
            clsCommon.AddColumnsForChange(coll, "INTEREST_APPLIED", obj.INTEREST_APPLIED)
            clsCommon.AddColumnsForChange(coll, "INTEREST_TYPE", obj.INTEREST_TYPE)
            clsCommon.AddColumnsForChange(coll, "INTEREST_PERIODICITY", obj.INTEREST_PERIODICITY)
            clsCommon.AddColumnsForChange(coll, "INTEREST_RATE", obj.INTEREST_RATE)
            clsCommon.AddColumnsForChange(coll, "INTEREST_AMOUNT", obj.INTEREST_AMOUNT)
            clsCommon.AddColumnsForChange(coll, "TOTALPAYABLE_AMOUNT", obj.TOTALPAYABLE_AMOUNT)
            clsCommon.AddColumnsForChange(coll, "LOAN_DESCRIPTION", obj.LOAN_DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
            clsCommon.AddColumnsForChange(coll, "Division", obj.Division)
            clsCommon.AddColumnsForChange(coll, "Payment_EndDate", obj.Payment_EndDate)
            clsCommon.AddColumnsForChange(coll, "Gross_Salary", obj.Gross_Salary)
            clsCommon.AddColumnsForChange(coll, "Loan_Status", obj.Loan_Status)
            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "PAID", obj.PAID, True)
            'clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            'clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then

                'clsCommon.AddColumnsForChange(coll, "LOAN_CODE", obj.LOAN_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOAN_APPLICATION", OMInsertOrUpdate.Insert, "", trans)
            Else

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOAN_APPLICATION", OMInsertOrUpdate.Update, "TSPL_LOAN_APPLICATION.LOAN_CODE='" + obj.LOAN_CODE + "'", trans)
            End If


            isSaved = isSaved AndAlso clsLoanEMIDetail.SaveData(obj.LOAN_CODE, objList, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(err.Message)
            Return False
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsApplyLoan = clsApplyLoan.GetData(strDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.LOAN_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim qry As String = "Update TSPL_LOAN_APPLICATION set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where LOAN_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsLoanEMIDetail
#Region "Variables"
    Public EMINo As String
    Public EMIAmount As String
    Public Shared ObjList As List(Of clsAdjustmentVoucher)
    'Public Const AttendanceCode As String = "MT"
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsApplyLoan), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsApplyLoan In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "LOAN_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "EMI_NO", obj.EMI_NO)
                clsCommon.AddColumnsForChange(coll, "EMI_AMOUNT", obj.EMI_AMOUNT)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOANEMI_DETAIL", OMInsertOrUpdate.Insert, "TSPL_LOANEMI_DETAIL.LOAN_CODE='" + strDocNo + "'", trans)
            Next

        End If

        Return True
    End Function

End Class
