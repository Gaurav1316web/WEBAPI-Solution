Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsSectionAllowanceMaster

#Region "Variables"
    Public CODE As String = Nothing
    Public Description As String = Nothing
    Public Type As String = Nothing
    Public MAX_LIMIT As Decimal = Nothing
#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_SECTION_ALLOWANCE_MASTER.CODE as [Code] ,TSPL_SECTION_ALLOWANCE_MASTER.Description as [Description] , case when TSPL_SECTION_ALLOWANCE_MASTER.Type = 'S' then 'Section' else 'Allowances' end as [Type],TSPL_SECTION_ALLOWANCE_MASTER.MAX_LIMIT as [MAX Limit],TSPL_SECTION_ALLOWANCE_MASTER.Created_By as [Created By] ,TSPL_SECTION_ALLOWANCE_MASTER.Created_Date as [Created Date] ,TSPL_SECTION_ALLOWANCE_MASTER.Modified_By as [Modified By] ,TSPL_SECTION_ALLOWANCE_MASTER.Modified_Date as [Modified Date]  From TSPL_SECTION_ALLOWANCE_MASTER   "
        str = clsCommon.ShowSelectForm("Sectionallownce@FND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Function SaveData(ByVal arr As List(Of clsSectionAllowanceMaster), ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            isSaved = SaveData(arr, isNewEntry, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Function SaveData(ByVal arr As List(Of clsSectionAllowanceMaster), ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            For Each obj As clsSectionAllowanceMaster In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                clsCommon.AddColumnsForChange(coll, "MAX_LIMIT", obj.MAX_LIMIT)
                clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_SECTION_ALLOWANCE_MASTER where CODE='" + obj.CODE + "' ", trans) <= 0 Then
                    clsCommon.AddColumnsForChange(coll, "CODE", obj.CODE)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECTION_ALLOWANCE_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECTION_ALLOWANCE_MASTER", OMInsertOrUpdate.Update, "CODE='" + obj.CODE + "'", trans)
                End If
            Next
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSectionAllowanceMaster
        Dim obj As clsSectionAllowanceMaster = Nothing
        Dim qry As String = "SELECT TSPL_SECTION_ALLOWANCE_MASTER.CODE,TSPL_SECTION_ALLOWANCE_MASTER.Description,TSPL_SECTION_ALLOWANCE_MASTER.Type,TSPL_SECTION_ALLOWANCE_MASTER.MAX_LIMIT  FROM TSPL_SECTION_ALLOWANCE_MASTER  where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SECTION_ALLOWANCE_MASTER.CODE = (select MIN(CODE) from TSPL_SECTION_ALLOWANCE_MASTER where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_SECTION_ALLOWANCE_MASTER.CODE = (select Max(CODE) from TSPL_SECTION_ALLOWANCE_MASTER where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_SECTION_ALLOWANCE_MASTER.CODE = (select Min(CODE) from TSPL_SECTION_ALLOWANCE_MASTER where CODE>'" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_SECTION_ALLOWANCE_MASTER.CODE = (select Max(CODE) from TSPL_SECTION_ALLOWANCE_MASTER where CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_SECTION_ALLOWANCE_MASTER.CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsSectionAllowanceMaster()
            obj.CODE = clsCommon.myCstr(dt.Rows(0)("CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))
            obj.MAX_LIMIT = clsCommon.myCdbl(dt.Rows(0)("MAX_LIMIT"))
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Code not found to Delete")
        End If
        Dim qry As String = "delete from TSPL_SECTION_ALLOWANCE_MASTER where CODE='" + strCode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_SECTION_ALLOWANCE_MASTER where CODE='" + strCode + "'"))
    End Function
End Class
