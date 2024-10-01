Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class ClsInvestmentDeclaration
#Region "Variables"
    Public CODE As String
    Public Description As String
    Public Financial_Year_Code As String
    Public Investment_Type_Code As String
    Public Employee_Code As String
    Public Emp_Name As String
    Public Provisional_Amount As Double
    Public Actual_Amount As Double
    Public Status As String
    Public Investment_Type_Name As String 'TSPL_INVESTMENT_TYPE
    Public Post_Date As DateTime? = Nothing
    Public lblStatus As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public On_Hold As Boolean = Nothing
#End Region
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsInvestmentDeclaration
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim IsPost As Integer
        Dim Post_Date As String
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            IsPost = clsDBFuncationality.getSingleValue("Select IS_Post from TSPL_INVESTMENT_DECLARATION_payroll where CODE ='" + strCode + "'", trans)
            Post_Date = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Post_Date from TSPL_INVESTMENT_DECLARATION_payroll where CODE ='" + strCode + "'", trans))
            If IsPost = 1 Then
                Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(Post_Date, "dd/MM/yyyy"))
            End If
            Dim qry As String
            qry = "delete from TSPL_INVESTMENT_DECLARATION_payroll where CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsInvestmentDeclaration
        Dim obj As ClsInvestmentDeclaration = Nothing
        Dim qry As String = "select TSPL_INVESTMENT_DECLARATION_payroll.CODE,TSPL_INVESTMENT_DECLARATION_payroll.Description,TSPL_INVESTMENT_DECLARATION_payroll.Status,TSPL_INVESTMENT_DECLARATION_payroll.Is_Post,FINANCIAL_YEAR_CODE As [Financial Year],TSPL_INVESTMENT_DECLARATION_payroll.EMP_CODE,TSPL_INVESTMENT_DECLARATION_payroll.Investment_Type_Code,TSPL_INVESTMENT_TYPE.Description as Investment_Type_description ,  PROVISIONAL_AMOUNT,ACTUAL_AMOUNT,TSPL_EMPLOYEE_MASTER.Emp_Name from TSPL_INVESTMENT_DECLARATION_payroll LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_INVESTMENT_DECLARATION_payroll.EMP_CODE left join TSPL_INVESTMENT_TYPE on TSPL_INVESTMENT_TYPE.CODE =TSPL_INVESTMENT_DECLARATION_payroll.Investment_Type_Code where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_INVESTMENT_DECLARATION_payroll.CODE = (select MIN(TSPL_INVESTMENT_DECLARATION_payroll.CODE) from TSPL_INVESTMENT_DECLARATION_payroll)"
            Case NavigatorType.Last
                qry += " and TSPL_INVESTMENT_DECLARATION_payroll.CODE = (select Max(TSPL_INVESTMENT_DECLARATION_payroll.CODE) from TSPL_INVESTMENT_DECLARATION_payroll)"
            Case NavigatorType.Next
                qry += " and TSPL_INVESTMENT_DECLARATION_payroll.CODE = (select Min(TSPL_INVESTMENT_DECLARATION_payroll.CODE) from TSPL_INVESTMENT_DECLARATION_payroll where  TSPL_INVESTMENT_DECLARATION_payroll.CODE >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_INVESTMENT_DECLARATION_payroll.CODE = (select Max(TSPL_INVESTMENT_DECLARATION_payroll.CODE) from TSPL_INVESTMENT_DECLARATION_payroll where TSPL_INVESTMENT_DECLARATION_payroll.CODE <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_INVESTMENT_DECLARATION_payroll.CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsInvestmentDeclaration()
            obj.CODE = clsCommon.myCstr(dt.Rows(0)("CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Financial_Year_Code = clsCommon.myCstr(dt.Rows(0)("Financial Year"))
            obj.Investment_Type_Code = clsCommon.myCstr(dt.Rows(0)("Investment_Type_Code"))
            obj.Investment_Type_Name = clsCommon.myCstr(dt.Rows(0)("Investment_Type_description"))
            obj.Provisional_Amount = clsCommon.myCdbl(dt.Rows(0)("PROVISIONAL_AMOUNT"))
            obj.Actual_Amount = clsCommon.myCdbl(dt.Rows(0)("ACTUAL_AMOUNT"))
            obj.Emp_Name = clsCommon.myCstr(dt.Rows(0)("Emp_Name"))
            obj.Employee_Code = clsCommon.myCstr(dt.Rows(0)("Emp_Code"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.lblStatus = IIf(clsCommon.myCstr(dt.Rows(0)("Is_Post")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
        End If
        Return obj

    End Function

    Public Function SaveData(ByVal obj As ClsInvestmentDeclaration, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            If Not isNewEntry Then
                Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Status from TSPL_INVESTMENT_DECLARATION_payroll Where Code='" + obj.CODE + "'", trans))
                If Status = 1 Then
                    Throw New Exception("This document is already posted.")
                End If
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Financial_Year_Code", obj.Financial_Year_Code)
            clsCommon.AddColumnsForChange(coll, "Investment_Type_Code", obj.Investment_Type_Code)
            clsCommon.AddColumnsForChange(coll, "Emp_Code", obj.Employee_Code)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "PROVISIONAL_AMOUNT", obj.Provisional_Amount)
            clsCommon.AddColumnsForChange(coll, "Actual_Amount", obj.Actual_Amount)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "code", obj.CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_INVESTMENT_DECLARATION_payroll where CODE= '" & obj.CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVESTMENT_DECLARATION_payroll", OMInsertOrUpdate.Insert, "", trans)
                Else
                    common.clsCommon.MyMessageBoxShow("This Code Is Already Exist")
                    Return True
                End If

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVESTMENT_DECLARATION_payroll", OMInsertOrUpdate.Update, "CODE='" + obj.CODE + "'", trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function CheckNewEntry(ByVal Code As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select CODE from TSPL_INVESTMENT_DECLARATION_payroll where CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As ClsInvestmentDeclaration = ClsInvestmentDeclaration.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.lblStatus = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.Post_Date, "dd/MM/yyyy"))
            End If

            qry = "Update TSPL_INVESTMENT_DECLARATION_payroll set Is_Post=1, Post_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class
