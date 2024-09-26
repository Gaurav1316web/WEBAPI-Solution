Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsInvestmentType
#Region "Variables"
    Public CODE As String
    Public Description As String
    Public IT_SECTION_CODE As String
    Public IT_SECTION_Name As String
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsInvestmentType
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
            qry = "delete from TSPL_INVESTMENT_TYPE where CODE ='" + strCode + "'"
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
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsInvestmentType
        Dim obj As ClsInvestmentType = Nothing
        Dim qry As String = "select CODE,TSPL_INVESTMENT_TYPE.Description,TSPL_INVESTMENT_TYPE.IT_SECTION_CODE,TSPL_IT_SECTION.DESCRIPTION as IT_SECTION_Name from TSPL_INVESTMENT_TYPE LEFT OUTER JOIN TSPL_IT_SECTION ON TSPL_IT_SECTION.IT_SECTION_CODE=TSPL_INVESTMENT_TYPE.IT_SECTION_CODE where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and CODE = (select MIN(CODE) from TSPL_INVESTMENT_TYPE)"
            Case NavigatorType.Last
                qry += " and CODE = (select Max(CODE) from TSPL_INVESTMENT_TYPE)"
            Case NavigatorType.Next
                qry += " and CODE = (select Min(CODE) from TSPL_INVESTMENT_TYPE where  CODE >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and CODE = (select Max(CODE) from TSPL_INVESTMENT_TYPE where CODE <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsInvestmentType()
            obj.CODE = clsCommon.myCstr(dt.Rows(0)("CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.IT_SECTION_CODE = clsCommon.myCstr(dt.Rows(0)("IT_SECTION_CODE"))
            obj.IT_SECTION_Name = clsCommon.myCstr(dt.Rows(0)("IT_SECTION_Name"))
        End If
        Return obj

    End Function

    Public Function SaveData(ByVal obj As ClsInvestmentType, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "IT_SECTION_CODE", obj.IT_SECTION_CODE)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "CODE", obj.CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_INVESTMENT_TYPE where CODE= '" & obj.CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVESTMENT_TYPE", OMInsertOrUpdate.Insert, "", trans)
                Else
                    common.clsCommon.MyMessageBoxShow("This Code Is Already Exist")
                    Return False
                End If

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVESTMENT_TYPE", OMInsertOrUpdate.Update, "CODE='" + obj.CODE + "'", trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function CheckNewEntry(ByVal Code As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select CODE from TSPL_INVESTMENT_TYPE where CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

End Class
