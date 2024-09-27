Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsITSection
#Region "Variables"
    Public IT_SECTION_CODE As String
    Public Description As String
    Public INCOME_TAX_ACT As String

    Public MINIMUM_AMOUNT As Double
    Public MAXIMUM_AMOUNT As Double
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsITSection
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction
        trans = clsDBFuncationality.GetTransactin()
        Try
            ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String
            qry = "delete from TSPL_IT_SECTION where IT_SECTION_CODE ='" + strCode + "'"
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
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsITSection
        Dim obj As ClsITSection = Nothing
        Dim qry As String = "select IT_SECTION_CODE, Description,INCOME_TAX_ACT,MINIMUM_AMOUNT,MAXIMUM_AMOUNT from TSPL_IT_SECTION where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and IT_SECTION_CODE = (select MIN(IT_SECTION_CODE) from TSPL_IT_SECTION)"
            Case NavigatorType.Last
                qry += " and IT_SECTION_CODE = (select Max(IT_SECTION_CODE) from TSPL_IT_SECTION)"
            Case NavigatorType.Next
                qry += " and IT_SECTION_CODE = (select Min(IT_SECTION_CODE) from TSPL_IT_SECTION where  IT_SECTION_CODE >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and IT_SECTION_CODE = (select Max(IT_SECTION_CODE) from TSPL_IT_SECTION where IT_SECTION_CODE <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and IT_SECTION_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsITSection()
            obj.IT_SECTION_CODE = clsCommon.myCstr(dt.Rows(0)("IT_SECTION_CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.INCOME_TAX_ACT = clsCommon.myCstr(dt.Rows(0)("INCOME_TAX_ACT"))
            obj.MINIMUM_AMOUNT = clsCommon.myCdbl(dt.Rows(0)("MINIMUM_AMOUNT"))
            obj.MAXIMUM_AMOUNT = clsCommon.myCdbl(dt.Rows(0)("MAXIMUM_AMOUNT"))
        End If
        Return obj


    End Function

    Public Function SaveData(ByVal obj As clsITSection, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "INCOME_TAX_ACT", obj.INCOME_TAX_ACT)
            clsCommon.AddColumnsForChange(coll, "MINIMUM_AMOUNT", obj.MINIMUM_AMOUNT)
            clsCommon.AddColumnsForChange(coll, "MAXIMUM_AMOUNT", obj.MAXIMUM_AMOUNT)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "IT_SECTION_CODE", obj.IT_SECTION_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_IT_SECTION where IT_SECTION_CODE= '" & obj.IT_SECTION_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_IT_SECTION", OMInsertOrUpdate.Insert, "", trans)
                Else
                    common.clsCommon.MyMessageBoxShow("This Code Is Already Exist")
                    Return False
                End If

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_IT_SECTION", OMInsertOrUpdate.Update, "IT_SECTION_CODE='" + obj.IT_SECTION_CODE + "'", trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
        Return True
    End Function
    Public Shared Function CheckNewEntry(ByVal Code As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select IT_SECTION_CODE from TSPL_IT_SECTION where IT_SECTION_CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

End Class
