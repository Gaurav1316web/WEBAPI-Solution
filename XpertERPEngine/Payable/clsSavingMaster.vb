Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsSavingMaster

#Region "Variables"
    Public SAVINGS_CODE As String
    Public Description As String
    Public Section_Code As String
    Public Section_Desc As String
#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_SAVINGS_MASTER.SAVINGS_CODE as [Code] ,TSPL_SAVINGS_MASTER.Description as [Description] ,TSPL_SAVINGS_MASTER.Section_Code as [Section Code],TSPL_SECTION_ALLOWANCE_MASTER.Description as [Section Desc],TSPL_SAVINGS_MASTER.Created_By as [Created By] ,TSPL_SAVINGS_MASTER.Created_Date as [Created Date] ,TSPL_SAVINGS_MASTER.Modified_By as [Modified By] ,TSPL_SAVINGS_MASTER.Modified_Date as [Modified Date]  From TSPL_SAVINGS_MASTER   " & _
                            " left join TSPL_SECTION_ALLOWANCE_MASTER on TSPL_SECTION_ALLOWANCE_MASTER.Code=TSPL_SAVINGS_MASTER.Section_Code "
        str = clsCommon.ShowSelectForm("SavingsMaster@FND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Function SaveData(ByVal arr As List(Of clsSavingMaster), ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal arr As List(Of clsSavingMaster), ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            For Each obj As clsSavingMaster In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                clsCommon.AddColumnsForChange(coll, "Section_Code", obj.Section_Code)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_SAVINGS_MASTER where SAVINGS_CODE='" + obj.SAVINGS_CODE + "' ", trans) <= 0 Then
                    clsCommon.AddColumnsForChange(coll, "SAVINGS_CODE", obj.SAVINGS_CODE)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SAVINGS_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SAVINGS_MASTER", OMInsertOrUpdate.Update, "TSPL_SAVINGS_MASTER.SAVINGS_CODE='" + obj.SAVINGS_CODE + "'", trans)
                End If
            Next
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSavingMaster
        Dim obj As clsSavingMaster = Nothing
        Dim qry As String = "SELECT TSPL_SAVINGS_MASTER.SAVINGS_CODE,TSPL_SAVINGS_MASTER.Description,TSPL_SAVINGS_MASTER.Section_Code,TSPL_SECTION_ALLOWANCE_MASTER.Description as Section_Desc FROM TSPL_SAVINGS_MASTER left join TSPL_SECTION_ALLOWANCE_MASTER on TSPL_SECTION_ALLOWANCE_MASTER.Code=TSPL_SAVINGS_MASTER.Section_Code where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SAVINGS_MASTER.SAVINGS_CODE = (select MIN(SAVINGS_CODE) from TSPL_SAVINGS_MASTER where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_SAVINGS_MASTER.SAVINGS_CODE = (select Max(SAVINGS_CODE) from TSPL_SAVINGS_MASTER where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_SAVINGS_MASTER.SAVINGS_CODE = (select Min(SAVINGS_CODE) from TSPL_SAVINGS_MASTER where SAVINGS_CODE>'" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_SAVINGS_MASTER.SAVINGS_CODE = (select Max(SAVINGS_CODE) from TSPL_SAVINGS_MASTER where SAVINGS_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_SAVINGS_MASTER.SAVINGS_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsSavingMaster()
            obj.SAVINGS_CODE = clsCommon.myCstr(dt.Rows(0)("SAVINGS_CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Section_Code = clsCommon.myCstr(dt.Rows(0)("Section_Code"))
            obj.Section_Desc = clsCommon.myCstr(dt.Rows(0)("Section_Desc"))
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Code not found to Delete")
        End If
        Dim qry As String = "delete from TSPL_SAVINGS_MASTER where SAVINGS_CODE='" + strCode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_SAVINGS_MASTER where SAVINGS_CODE='" + strCode + "'"))
    End Function
End Class
