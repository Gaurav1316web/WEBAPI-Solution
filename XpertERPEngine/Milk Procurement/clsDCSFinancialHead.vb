Imports System.Data.SqlClient
Imports common
Public Class clsDCSFinancialHead

#Region "Variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public Type As String = Nothing
    Public Sub_Type As String = Nothing
    Public Parent_Head As String = Nothing
    Public SNo As Decimal = 0
#End Region

    Public Function SaveData(ByVal obj As clsDCSFinancialHead, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsDCSFinancialHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description, False, True)
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
            clsCommon.AddColumnsForChange(coll, "Sub_Type", obj.Sub_Type)
            clsCommon.AddColumnsForChange(coll, "Parent_Head", obj.Parent_Head, True)
            clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.DSCFinancialHead, "", "")
                If (clsCommon.myLen(obj.Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_FINANCIAL_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_FINANCIAL_HEAD", OMInsertOrUpdate.Update, "TSPL_DCS_FINANCIAL_HEAD.Code='" + obj.Code + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String = ""
        Try
            qry = "Delete from TSPL_DCS_FINANCIAL_HEAD where Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
            tran.Commit()
        Catch err As Exception
            tran.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsDCSFinancialHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDCSFinancialHead
        Dim obj As clsDCSFinancialHead = Nothing
        Dim qry As String = ""
        qry = " select * from TSPL_DCS_FINANCIAL_HEAD where 2=2 "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DCS_FINANCIAL_HEAD.Code = (select MIN(Code) from TSPL_DCS_FINANCIAL_HEAD where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_DCS_FINANCIAL_HEAD.Code = (select Max(Code) from TSPL_DCS_FINANCIAL_HEAD where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_DCS_FINANCIAL_HEAD.Code = (select Min(Code) from TSPL_DCS_FINANCIAL_HEAD where Code>'" + strDocumentNo + "'" + whrClas + " )"
            Case NavigatorType.Previous
                qry += " and TSPL_DCS_FINANCIAL_HEAD.Code = (select Max(Code) from TSPL_DCS_FINANCIAL_HEAD where Code<'" + strDocumentNo + "'" + whrClas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_DCS_FINANCIAL_HEAD.Code = '" + strDocumentNo + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDCSFinancialHead()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))
            obj.Sub_Type = clsCommon.myCstr(dt.Rows(0)("Sub_Type"))
            obj.Parent_Head = clsCommon.myCstr(dt.Rows(0)("Parent_Head"))
            obj.SNo = clsCommon.myCdbl(dt.Rows(0)("SNo"))
        End If
        Return obj
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_DCS_FINANCIAL_HEAD.Code,TSPL_DCS_FINANCIAL_HEAD.Description,TSPL_DCS_FINANCIAL_HEAD.Type,TSPL_DCS_FINANCIAL_HEAD.Sub_Type,TSPL_DCS_FINANCIAL_HEAD.SNo  from TSPL_DCS_FINANCIAL_HEAD "
        str = clsCommon.ShowSelectForm("dscfIhE@f", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function

    Public Shared Function getFinderObeject(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As clsDCSFinancialHead
        Dim obj As clsDCSFinancialHead = Nothing
        Dim strCode As String = getFinder(whrcls, curcode, isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            obj = GetData(strCode, NavigatorType.Current)
        End If
        Return obj
    End Function

    Public Shared Function GetHeadType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select..."
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "TA"
        dr("Name") = "व्यापार खाता"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "P&L"
        dr("Name") = "हानि-लाभ खाता"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "BS"
        dr("Name") = "संतुलन चित्र"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Public Shared Function GetHeadSubType(ByVal strType As String) As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select..."
        dt.Rows.Add(dr)
        If clsCommon.CompairString(strType, "TA") = CompairStringResult.Equal Then
            dr = dt.NewRow()
            dr("Code") = "PURCH"
            dr("Name") = "क्रय"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "SALE"
            dr("Name") = "विक्रय"
            dt.Rows.Add(dr)
        ElseIf clsCommon.CompairString(strType, "P&L") = CompairStringResult.Equal Then
            dr = dt.NewRow()
            dr("Code") = "Loss"
            dr("Name") = "हानि"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "Proft"
            dr("Name") = "लाभ"
            dt.Rows.Add(dr)
        ElseIf clsCommon.CompairString(strType, "BS") = CompairStringResult.Equal Then
            dr = dt.NewRow()
            dr("Code") = "Asset"
            dr("Name") = "लेनदारी"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "Liab"
            dr("Name") = "देनदारी"
            dt.Rows.Add(dr)
        End If
        Return dt
    End Function
End Class
