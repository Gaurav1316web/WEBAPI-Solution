Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsODSheet

#Region "Variables"
    Public Code As String
    Public EMP_CODE As String
    Public OD_CODE As String
    Public Emp_Name As String
    Public OD_Description As String

    Public FROM_Date As Date
    Public TO_Date As Date
   
    Public PAY_PERIOD_CODE As String
    Public PAY_PERIOD_NAME As String
    Public PURPOSE As String
    Public MATERIAL_CARRYING As String
#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select OD_SHEET_CODE as Code,PAY_PERIOD_CODE as [Pay Period Code],EMP_CODE as [Employee Code],OD_CODE as [OD Code]," & _
                            " From_Date as [From Date], To_Date as [To Date]," & _
                            " Created_By as [Created By],Created_Date as [Created Date],Modified_By as [Modified By], Modified_Date as [Modified Date] " & _
                            " from TSPL_OUTDUTY_SHEET "
        str = clsCommon.ShowSelectForm("ODSHT", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsODSheet
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
            qry = "delete from TSPL_OUTDUTY_SHEET where OD_SHEET_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsODSheet
        Dim obj As clsODSheet = Nothing
        Dim qry As String = ""
        qry += " select TSPL_OUTDUTY_SHEET.*, TSPL_OD_MASTER.Description as OD_Description, TSPL_EMPLOYEE_MASTER.Emp_Name, TSPL_PAYPERIOD_MASTER.PAY_PERIOD_NAME from TSPL_OUTDUTY_SHEET "
        qry += " left outer join TSPL_OD_MASTER on TSPL_OD_MASTER.OD_CODE= TSPL_OUTDUTY_SHEET.OD_CODE  "
        qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE= TSPL_OUTDUTY_SHEET.EMP_CODE "
        qry += " left outer join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE= TSPL_OUTDUTY_SHEET.PAY_PERIOD_CODE where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " and OD_SHEET_CODE = (select MIN(OD_SHEET_CODE) from TSPL_OUTDUTY_SHEET)"
            Case NavigatorType.Last
                qry += " and OD_SHEET_CODE = (select Max(OD_SHEET_CODE) from TSPL_OUTDUTY_SHEET)"
            Case NavigatorType.Next
                qry += " and OD_SHEET_CODE = (select Min(OD_SHEET_CODE) from TSPL_OUTDUTY_SHEET where  OD_SHEET_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and OD_SHEET_CODE = (select Max(OD_SHEET_CODE) from TSPL_OUTDUTY_SHEET where OD_SHEET_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and OD_SHEET_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsODSheet()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("OD_SHEET_CODE"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.OD_CODE = clsCommon.myCstr(dt.Rows(0)("OD_CODE"))
            obj.Emp_Name = clsCommon.myCstr(dt.Rows(0)("Emp_Name"))
            obj.OD_Description = clsCommon.myCstr(dt.Rows(0)("OD_Description"))

            obj.PAY_PERIOD_CODE = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_CODE"))
            obj.PAY_PERIOD_NAME = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_NAME"))

            obj.FROM_Date = clsCommon.myCDate(dt.Rows(0)("FROM_Date"))
            obj.TO_Date = clsCommon.myCDate(dt.Rows(0)("TO_Date"))
            obj.MATERIAL_CARRYING = clsCommon.myCstr(dt.Rows(0)("MATERIAL_CARRYING"))
            obj.PURPOSE = clsCommon.myCstr(dt.Rows(0)("PURPOSE"))
        End If
        Return obj


    End Function

    Public Function SaveData(ByVal obj As clsODSheet, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "COMP_CODE", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
            clsCommon.AddColumnsForChange(coll, "OD_CODE", obj.OD_CODE)

            clsCommon.AddColumnsForChange(coll, "FROM_DATE", clsCommon.GetPrintDate(obj.FROM_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "TO_Date", clsCommon.GetPrintDate(obj.TO_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "PURPOSE", obj.PURPOSE)
            clsCommon.AddColumnsForChange(coll, "MATERIAL_CARRYING", obj.MATERIAL_CARRYING)
            clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                If clsCommon.myLen(obj.Code) <= 0 Then
                    obj.Code = clsERPFuncationality.GetNextCode(Nothing, clsCommon.myCDate(clsCommon.GETSERVERDATE()), clsDocType.OTSheet, "", "")
                    If clsCommon.myLen(obj.Code) <= 0 Then
                        Throw New Exception("Error in Code Genration")
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "OD_SHEET_CODE", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_OUTDUTY_SHEET where OD_SHEET_CODE= '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OUTDUTY_SHEET", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OUTDUTY_SHEET", OMInsertOrUpdate.Update, "OD_SHEET_CODE='" + obj.Code + "'")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Function CheckOTCodeExist(ByVal OD_SHEET_CODE As String) As Boolean
        Dim strq As String
        strq = "SELECT OD_SHEET_CODE FROM TSPL_OUTDUTY_SHEET WHERE OD_SHEET_CODE='" & OD_SHEET_CODE & "'"
        '  Dim isNew As Boolean
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq)
        If dt.Rows.Count > 0 Then
            Return False
        End If
        Return True
    End Function
End Class
