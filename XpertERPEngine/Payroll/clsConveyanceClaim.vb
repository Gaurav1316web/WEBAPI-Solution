Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsConveyanceClaim
#Region "Variables"
    Public Code As String
    Public EMP_CODE As String
    Public CONV_RATE_CODE As String
    Public Emp_Name As String
    Public Conv_Rate_Desc As String
    Public CONV_TYPE As String
    Public CLAIM_DISTANCE As Decimal
    Public CONV_RATE As Decimal
    Public CLAIM_AMOUNT As Decimal
    Public Pay_Period_Code As String
    Public PAY_PERIOD_NAME As String
    Public Location_Code As String

#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_CONVEYANCE_CLAIM.CLAIM_CODE as Code,TSPL_CONVEYANCE_CLAIM.Pay_Period_Code as [Pay Period Code],TSPL_CONVEYANCE_CLAIM.EMP_CODE as [Employee Code],TSPL_CONVEYANCE_CLAIM.CONV_RATE_CODE as [Rate Code]," &
                            " TSPL_CONVEYANCE_CLAIM.CONV_TYPE as [Conveyance Type], TSPL_CONVEYANCE_CLAIM.CLAIM_DISTANCE as [Claim Distance],TSPL_CONVEYANCE_CLAIM.CONV_RATE as [Conveyance Rate],TSPL_CONVEYANCE_CLAIM.CLAIM_AMOUNT as [Claim Amount]," &
                            " TSPL_CONVEYANCE_CLAIM.Created_By as [Created By],TSPL_CONVEYANCE_CLAIM.Created_Date as [Created Date],TSPL_CONVEYANCE_CLAIM.Modified_By as [Modified By], TSPL_CONVEYANCE_CLAIM.Modified_Date as [Modified Date] " &
                            " from TSPL_CONVEYANCE_CLAIM Inner Join TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_Code=TSPL_CONVEYANCE_CLAIM.EMP_Code"
        str = clsCommon.ShowSelectForm("ODSHT", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsConveyanceClaim
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
            qry = "delete from TSPL_CONVEYANCE_CLAIM where CLAIM_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsConveyanceClaim
        Dim obj As clsConveyanceClaim = Nothing
        Dim qry As String = ""
        qry += " select TSPL_CONVEYANCE_CLAIM.*, TSPL_CONVEYANCE_RATE_MASTER.Description as Conv_Rate_Desc, TSPL_EMPLOYEE_MASTER.Emp_Name, TSPL_PAYPERIOD_MASTER.PAY_PERIOD_NAME from TSPL_CONVEYANCE_CLAIM "
        qry += " left outer join TSPL_CONVEYANCE_RATE_MASTER on TSPL_CONVEYANCE_RATE_MASTER.CONV_RATE_CODE= TSPL_CONVEYANCE_CLAIM.CONV_RATE_CODE  "
        qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE= TSPL_CONVEYANCE_CLAIM.EMP_CODE "
        qry += " left outer join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.Pay_Period_Code= TSPL_CONVEYANCE_CLAIM.Pay_Period_Code where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " and CLAIM_CODE = (select MIN(CLAIM_CODE) from TSPL_CONVEYANCE_CLAIM)"
            Case NavigatorType.Last
                qry += " and CLAIM_CODE = (select Max(CLAIM_CODE) from TSPL_CONVEYANCE_CLAIM)"
            Case NavigatorType.Next
                qry += " and CLAIM_CODE = (select Min(CLAIM_CODE) from TSPL_CONVEYANCE_CLAIM where  CLAIM_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and CLAIM_CODE = (select Max(CLAIM_CODE) from TSPL_CONVEYANCE_CLAIM where CLAIM_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and CLAIM_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsConveyanceClaim()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("CLAIM_CODE"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.CONV_RATE_CODE = clsCommon.myCstr(dt.Rows(0)("CONV_RATE_CODE"))
            obj.Emp_Name = clsCommon.myCstr(dt.Rows(0)("Emp_Name"))
            obj.Conv_Rate_Desc = clsCommon.myCstr(dt.Rows(0)("Conv_Rate_Desc"))

            obj.Pay_Period_Code = clsCommon.myCstr(dt.Rows(0)("Pay_Period_Code"))
            obj.PAY_PERIOD_NAME = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_NAME"))

            obj.CONV_TYPE = clsCommon.myCstr(dt.Rows(0)("CONV_TYPE"))
            obj.CLAIM_DISTANCE = clsCommon.myCdbl(dt.Rows(0)("CLAIM_DISTANCE"))
            obj.CONV_RATE_CODE = clsCommon.myCstr(dt.Rows(0)("CONV_RATE_CODE"))
            obj.CONV_RATE = clsCommon.myCdbl(dt.Rows(0)("CONV_RATE"))
            obj.CLAIM_AMOUNT = clsCommon.myCdbl(dt.Rows(0)("CLAIM_AMOUNT"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
        End If
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsConveyanceClaim, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "COMP_CODE", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
            clsCommon.AddColumnsForChange(coll, "CONV_TYPE", obj.CONV_TYPE)
            clsCommon.AddColumnsForChange(coll, "CONV_RATE_CODE", obj.CONV_RATE_CODE, True)
            clsCommon.AddColumnsForChange(coll, "CLAIM_DISTANCE", obj.CLAIM_DISTANCE)
            clsCommon.AddColumnsForChange(coll, "CONV_RATE", obj.CONV_RATE)
            clsCommon.AddColumnsForChange(coll, "CLAIM_AMOUNT", obj.CLAIM_AMOUNT)
            clsCommon.AddColumnsForChange(coll, "Pay_Period_Code", obj.Pay_Period_Code, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            If isNewEntry Then
                If clsCommon.myLen(obj.Code) <= 0 Then
                    obj.Code = clsERPFuncationality.GetNextCode(Nothing, clsCommon.myCDate(clsCommon.GETSERVERDATE()), clsDocType.ConveyanceClaim, "", "")
                    If clsCommon.myLen(obj.Code) <= 0 Then
                        Throw New Exception("Error in Code Genration")
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "CLAIM_CODE", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_CONVEYANCE_CLAIM where CLAIM_CODE= '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CONVEYANCE_CLAIM", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CONVEYANCE_CLAIM", OMInsertOrUpdate.Update, "CLAIM_CODE='" + obj.Code + "'")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Function CheckOTCodeExist(ByVal CLAIM_CODE As String) As Boolean
        Dim strq As String
        strq = "SELECT CLAIM_CODE FROM TSPL_CONVEYANCE_CLAIM WHERE CLAIM_CODE='" & CLAIM_CODE & "'"
        ' Dim isNew As Boolean
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq)
        If dt.Rows.Count > 0 Then
            Return False
        End If
        Return True
    End Function
End Class
