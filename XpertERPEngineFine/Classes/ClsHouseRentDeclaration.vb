Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class ClsHouseRentDeclaration
#Region "Variables"
    Public CODE As String
    Public Description As String
    Public Financial_Year_Code As String
    Public Pay_Period_Code As String
    Public Employee_Code As String
    Public PAY_PERIOD_NAME As String
    Public Emp_Name As String
    Public House_Rent_Amount As Double
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsHouseRentDeclaration
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
            qry = "delete from TSPL_HOUSE_RENT_DECLARATION where CODE ='" + strCode + "'"
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
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsHouseRentDeclaration
        Dim obj As ClsHouseRentDeclaration = Nothing
        Dim qry As String = "select CODE,TSPL_HOUSE_RENT_DECLARATION.Description,FINANCIAL_YEAR_CODE As [Financial Year],TSPL_HOUSE_RENT_DECLARATION.EMP_CODE,TSPL_HOUSE_RENT_DECLARATION.Pay_Period_Code, HOUSE_RENT_AMOUNT,TSPL_PAYPERIOD_MASTER.PAY_PERIOD_NAME,TSPL_EMPLOYEE_MASTER.Emp_Name from TSPL_HOUSE_RENT_DECLARATION LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_HOUSE_RENT_DECLARATION.EMP_CODE LEFT OUTER JOIN TSPL_PAYPERIOD_MASTER ON TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE =TSPL_HOUSE_RENT_DECLARATION.PAY_PERIOD_CODE where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and CODE = (select MIN(CODE) from TSPL_HOUSE_RENT_DECLARATION)"
            Case NavigatorType.Last
                qry += " and CODE = (select Max(CODE) from TSPL_HOUSE_RENT_DECLARATION)"
            Case NavigatorType.Next
                qry += " and CODE = (select Min(CODE) from TSPL_HOUSE_RENT_DECLARATION where  CODE >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and CODE = (select Max(CODE) from TSPL_HOUSE_RENT_DECLARATION where CODE <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsHouseRentDeclaration()
            obj.CODE = clsCommon.myCstr(dt.Rows(0)("CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Financial_Year_Code = clsCommon.myCstr(dt.Rows(0)("Financial Year"))
            obj.Pay_Period_Code = clsCommon.myCstr(dt.Rows(0)("Pay_Period_Code"))
            obj.House_Rent_Amount = clsCommon.myCdbl(dt.Rows(0)("HOUSE_RENT_AMOUNT"))
            obj.PAY_PERIOD_NAME = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_NAME"))
            obj.Emp_Name = clsCommon.myCstr(dt.Rows(0)("Emp_Name"))
            obj.Employee_Code = clsCommon.myCstr(dt.Rows(0)("Emp_Code"))
        End If
        Return obj

    End Function

    Public Function SaveData(ByVal obj As ClsHouseRentDeclaration, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Financial_Year_Code", obj.Financial_Year_Code)
            clsCommon.AddColumnsForChange(coll, "Pay_Period_Code", obj.Pay_Period_Code)
            clsCommon.AddColumnsForChange(coll, "Emp_Code", obj.Employee_Code)
            clsCommon.AddColumnsForChange(coll, "HOUSE_RENT_AMOUNT", obj.House_Rent_Amount)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "CODE", obj.CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_HOUSE_RENT_DECLARATION where CODE= '" & obj.CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HOUSE_RENT_DECLARATION", OMInsertOrUpdate.Insert, "", trans)
                Else
                    common.clsCommon.MyMessageBoxShow("This Code Is Already Exist")
                    Return False
                End If

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HOUSE_RENT_DECLARATION", OMInsertOrUpdate.Update, "CODE='" + obj.CODE + "'", trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function CheckNewEntry(ByVal Code As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select CODE from TSPL_HOUSE_RENT_DECLARATION where CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

End Class
