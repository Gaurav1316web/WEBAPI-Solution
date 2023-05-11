Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsLeaveOpeningBalance

#Region "Variables"
    Public CODE As String
    Public EMP_CODE As String
    Public LEAVE_CODE As String
    Public OPENING_DATE As DateTime
    Public OPENING_BAL As Double
    Public Emp_Name As String
    Public PAY_PERIOD_CODE As String
#End Region


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsLeaveOpeningBalance
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
            qry = "delete from TSPL_LEAVE_OPENINGBAL where CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_LEAVE_OPENINGBAL.CODE AS Code,TSPL_LEAVE_OPENINGBAL.EMP_CODE as [Employee Code],TSPL_EMPLOYEE_MASTER.Emp_Name as [Employee Name]," & _
                            " LEAVE_CODE as [Leave Code],OPENING_DATE as [Opening Date],OPENING_BAL AS [Opening Balance],TSPL_LEAVE_OPENINGBAL.Created_By as [Created By], " & _
                            " TSPL_LEAVE_OPENINGBAL.Created_Date as [Created Date] from TSPL_LEAVE_OPENINGBAL " & _
                            " inner join TSPL_EMPLOYEE_MASTER on TSPL_LEAVE_OPENINGBAL.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE "

        str = clsCommon.ShowSelectForm("LVOB", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsLeaveOpeningBalance
        Dim obj As clsLeaveOpeningBalance = Nothing
        Dim qry As String = "select TSPL_LEAVE_OPENINGBAL.CODE AS Code,TSPL_LEAVE_OPENINGBAL.EMP_CODE ,TSPL_EMPLOYEE_MASTER.Emp_Name ," & _
                            " LEAVE_CODE ,OPENING_DATE,OPENING_BAL,TSPL_LEAVE_OPENINGBAL.Created_By, " & _
                            " TSPL_LEAVE_OPENINGBAL.Created_Date,TSPL_LEAVE_OPENINGBAL.PAY_PERIOD_CODE from TSPL_LEAVE_OPENINGBAL " & _
                            " inner join TSPL_EMPLOYEE_MASTER on TSPL_LEAVE_OPENINGBAL.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE WHERE 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_LEAVE_OPENINGBAL.CODE = (select MIN(CODE) from TSPL_LEAVE_OPENINGBAL)"
            Case NavigatorType.Last
                qry += " and TSPL_LEAVE_OPENINGBAL.CODE = (select Max(CODE) from TSPL_LEAVE_OPENINGBAL)"
            Case NavigatorType.Next
                qry += " and TSPL_LEAVE_OPENINGBAL.CODE = (select Min(CODE) from TSPL_LEAVE_OPENINGBAL where  CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_LEAVE_OPENINGBAL.CODE = (select Max(CODE) from TSPL_LEAVE_OPENINGBAL where CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_LEAVE_OPENINGBAL.CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsLeaveOpeningBalance()
            obj.CODE = clsCommon.myCstr(dt.Rows(0)("CODE"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.PAY_PERIOD_CODE = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_CODE"))
            If clsCommon.myLen(dt.Rows(0)("OPENING_DATE")) > 0 Then
                obj.OPENING_DATE = clsCommon.GetPrintDate(dt.Rows(0)("OPENING_DATE"), "dd/MM/yyyy")
            Else
                obj.OPENING_DATE = Nothing
            End If
            obj.LEAVE_CODE = clsCommon.myCstr(dt.Rows(0)("LEAVE_CODE"))
            obj.OPENING_BAL = clsCommon.myCdbl(dt.Rows(0)("OPENING_BAL"))
            obj.Emp_Name = clsCommon.myCstr(dt.Rows(0)("Emp_Name"))
        End If
        Return obj


    End Function

    Public Function SaveData(ByVal obj As clsLeaveOpeningBalance, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
            clsCommon.AddColumnsForChange(coll, "LEAVE_CODE", obj.LEAVE_CODE)
            clsCommon.AddColumnsForChange(coll, "OPENING_BAL", obj.OPENING_BAL)
            clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE)
            clsCommon.AddColumnsForChange(coll, "OPENING_DATE", clsCommon.GetPrintDate(obj.OPENING_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            '' CHECK FOR NEW ENTRY
            isNewEntry = CheckNewEntry(obj.EMP_CODE, obj.LEAVE_CODE)
            'Dim qry As String = "SELECT Count(*) FROM TSPL_LEAVE_OPENINGBAL where CODE= '" & obj.CODE & "'"
            'Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            If isNewEntry = True Then
                obj.CODE = GetCode()
                clsCommon.AddColumnsForChange(coll, "CODE", obj.CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LEAVE_OPENINGBAL", OMInsertOrUpdate.Insert, "")
            Else
                'obj.CODE = IIf(clsCommon.myLen(obj.CODE) <= 0, GetCode(), obj.CODE)
                'clsCommon.AddColumnsForChange(coll, "CODE", obj.CODE)
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LEAVE_OPENINGBAL", OMInsertOrUpdate.Update, "CODE='" + obj.CODE + "'")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function CheckNewEntry(ByVal EMP_CODE As String, ByVal LEAVE_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String = "select CODE from TSPL_LEAVE_OPENINGBAL where EMP_CODE ='" + EMP_CODE + "' AND LEAVE_CODE='" & LEAVE_CODE & "'  "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Shared Function GetCode(Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select COALESCE(MAX(CODE),'') AS CODE from TSPL_LEAVE_OPENINGBAL  "
        Dim code As String = clsDBFuncationality.getSingleValue(qry, trans)
        If clsCommon.myLen(code) <= 0 Then
            code = "LOB000001"
        Else
            code = clsCommon.incval(code)
        End If
        Return code
    End Function
    Public Shared Function GetLeaveOpeningBalance(ByVal EMP_CODE As String, ByVal LEAVE_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        Dim qry As String = "select OPENING_BAL from TSPL_LEAVE_OPENINGBAL where EMP_CODE ='" + EMP_CODE + "' AND LEAVE_CODE='" & LEAVE_CODE & "'  "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
End Class
