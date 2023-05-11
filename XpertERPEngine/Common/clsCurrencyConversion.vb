Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsCurrencyConversion

#Region "Variables"
    Public Code As String
    Public FROM_CURRENCY As String
    Public TO_CURRENCY As String
    Public FROM_DATE As Date
    Public TO_DATE As Date
    Public Rate As Double
    Public DESCRIPTION As String
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Code as [Code],FROM_CURRENCY as [From Currency],TO_CURRENCY as [To Currency],FROM_DATE as [From Date],TO_DATE as [To Date],Rate as [Rate],DESCRIPTION as [Description]  from TSPL_CURRENCY_CONVERSION_RATE    "
        str = clsCommon.ShowSelectForm("CURCNVRTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsCurrencyConversion
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
            qry = "delete from TSPL_CURRENCY_CONVERSION_RATE where Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCurrencyConversion
        Dim obj As clsCurrencyConversion = Nothing
        Dim qry As String = "select Code, FROM_CURRENCY,TO_CURRENCY,FROM_DATE,TO_DATE,Rate, DESCRIPTION from TSPL_CURRENCY_CONVERSION_RATE where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and Code = (select MIN(Code) from TSPL_CURRENCY_CONVERSION_RATE)"
            Case NavigatorType.Last
                qry += " and Code = (select Max(Code) from TSPL_CURRENCY_CONVERSION_RATE)"
            Case NavigatorType.Next
                qry += " and Code = (select Min(Code) from TSPL_CURRENCY_CONVERSION_RATE where  Code>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and Code = (select Max(Code) from TSPL_CURRENCY_CONVERSION_RATE where Code<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCurrencyConversion()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.FROM_CURRENCY = clsCommon.myCstr(dt.Rows(0)("FROM_CURRENCY"))
            obj.TO_CURRENCY = clsCommon.myCstr(dt.Rows(0)("TO_CURRENCY"))
            obj.FROM_DATE = clsCommon.myCDate(dt.Rows(0)("FROM_DATE"))
            obj.TO_DATE = clsCommon.myCDate(dt.Rows(0)("TO_DATE"))
            obj.Rate = clsCommon.myCdbl(dt.Rows(0)("Rate"))
        End If
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsCurrencyConversion, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = True
        Dim qry As String = ""
        Try
            'If clsCommon.myLen(obj.Code) <= 0 Then
            '    isNewEntry = True
            '    qry = "select  MAX(Code)  from TSPL_CURRENCY_CONVERSION_RATE "
            '    obj.Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            '    If clsCommon.myLen(obj.Code) <= 0 Then
            '        obj.Code = "CC000001"
            '    Else
            '        obj.Code = clsCommon.incval(obj.Code)
            '    End If
            'End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "FROM_CURRENCY", obj.FROM_CURRENCY)
            clsCommon.AddColumnsForChange(coll, "TO_CURRENCY", obj.TO_CURRENCY)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "TO_DATE", clsCommon.GetPrintDate(obj.TO_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "FROM_DATE", clsCommon.GetPrintDate(obj.FROM_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_CURRENCY_CONVERSION_RATE where Code='" & obj.Code & "'", trans)
                    If ChkNewEntry = 0 Then
                        obj.Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.CurrencyConversion, "", "")
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CURRENCY_CONVERSION_RATE", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CURRENCY_CONVERSION_RATE", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If

            'qry = "SELECT Count(*) FROM TSPL_CURRENCY_CONVERSION_RATE where Code= '" & obj.Code & "'"
            'Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            'If check = 0 Then
            '    clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
            '    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CURRENCY_CONVERSION_RATE", OMInsertOrUpdate.Insert, "")
            'Else
            '    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CURRENCY_CONVERSION_RATE", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'")
            'End If

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

End Class
