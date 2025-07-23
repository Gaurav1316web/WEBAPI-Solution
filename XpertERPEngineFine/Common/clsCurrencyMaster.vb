Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsCurrencyMaster
#Region "Variables"
    Public Code As String
    Public Description As String
    Public Name As String
    Public CURRENCY_SIGN As String
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select  CURRENCY_CODE as [Code],CURRENCY_NAME as [Currency Name],DESCRIPTION as [Description],Created_By as [Created By],Created_Date as [Created Date] ,Modified_By as [Modified By],Modified_Date as [Modified Date],CURRENCY_SIGN as [Currency Sign]  from TSPL_CURRENCY_MASTER     "
        str = clsCommon.ShowSelectForm("CURMSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsCurrencyMaster
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
            qry = "delete from TSPL_CURRENCY_MASTER where CURRENCY_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCurrencyMaster
        Dim obj As clsCurrencyMaster = Nothing
        Dim qry As String = " select CURRENCY_CODE, CURRENCY_NAME, DESCRIPTION, CURRENCY_SIGN from TSPL_CURRENCY_MASTER where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and CURRENCY_CODE = (select MIN(CURRENCY_CODE) from TSPL_CURRENCY_MASTER)"
            Case NavigatorType.Last
                qry += " and CURRENCY_CODE = (select Max(CURRENCY_CODE) from TSPL_CURRENCY_MASTER)"
            Case NavigatorType.Next
                qry += " and CURRENCY_CODE = (select Min(CURRENCY_CODE) from TSPL_CURRENCY_MASTER where  CURRENCY_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and CURRENCY_CODE = (select Max(CURRENCY_CODE) from TSPL_CURRENCY_MASTER where CURRENCY_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and CURRENCY_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCurrencyMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.Name = clsCommon.myCstr(dt.Rows(0)("CURRENCY_NAME"))
            obj.CURRENCY_SIGN = clsCommon.myCstr(dt.Rows(0)("CURRENCY_SIGN"))
        End If
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsCurrencyMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "CURRENCY_NAME", obj.Name)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.Description)
            clsCommon.AddColumnsForChange(coll, "CURRENCY_SIGN", obj.CURRENCY_SIGN)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_CURRENCY_MASTER where CURRENCY_CODE= '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CURRENCY_MASTER", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CURRENCY_MASTER", OMInsertOrUpdate.Update, "CURRENCY_CODE='" + obj.Code + "'")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetCurrencyName(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " select CURRENCY_NAME from TSPL_CURRENCY_MASTER where 2=2 and CURRENCY_CODE = '" + strCode + "'"
        Dim strName As String = clsDBFuncationality.getSingleValue(qry, trans)
        Return strName
    End Function
    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select CURRENCY_CODE from TSPL_CURRENCY_MASTER where CURRENCY_CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

End Class
