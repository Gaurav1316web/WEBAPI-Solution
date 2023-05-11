Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsAccountSubGroup
#Region "Variables"
    Public Account_Sub_Group_Code As String = Nothing
    Public Account_Sub_Group_Desc As String = Nothing
    Public Account_Group_Code As String = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = " select  TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code as [Code] ,TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Desc AS [Description],TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code As [Account Group Code],TSPL_ACCOUNT_GROUPS.Account_Group_Desc as [Account Group Desc],TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code as [Main Group Code],TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Desc as [Account Main Group Description],TSPL_ACCOUNT_MAIN_GROUPS.Group_Type as [Group Type],TSPL_ACCOUNT_SUB_GROUPS.Created_By as [Created By] ,Convert(varchar,TSPL_ACCOUNT_SUB_GROUPS.Created_Date,103) as [Created Date],TSPL_ACCOUNT_SUB_GROUPS.Modify_By as [Modified By] ,Convert(varchar,TSPL_ACCOUNT_SUB_GROUPS.Modified_Date,103) as [Modified Date] From TSPL_ACCOUNT_SUB_GROUPS  left outer join TSPL_ACCOUNT_GROUPS on  TSPL_ACCOUNT_GROUPS.Account_Group_Code= TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code  left outer join  TSPL_ACCOUNT_MAIN_GROUPS on TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code=TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code"
        Return clsCommon.myCstr(clsCommon.ShowSelectForm("AccSubGrp", qry, "Code", "", curcode, "Code", isButtonClicked))
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsAccountSubGroup
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
            qry = "delete from TSPL_ACCOUNT_SUB_GROUPS where Account_Sub_Group_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsAccountSubGroup
        Dim obj As ClsAccountSubGroup = Nothing
        Dim qry As String = "select * from TSPL_ACCOUNT_SUB_GROUPS where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and Account_Sub_Group_Code = (select MIN(Account_Sub_Group_Code) from TSPL_ACCOUNT_SUB_GROUPS)"
            Case NavigatorType.Last
                qry += " and Account_Sub_Group_Code = (select Max(Account_Sub_Group_Code) from TSPL_ACCOUNT_SUB_GROUPS)"
            Case NavigatorType.Next
                qry += " and Account_Sub_Group_Code = (select Min(Account_Sub_Group_Code) from TSPL_ACCOUNT_SUB_GROUPS where  Account_Sub_Group_Code>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and Account_Sub_Group_Code = (select Max(Account_Sub_Group_Code) from TSPL_ACCOUNT_SUB_GROUPS where Account_Sub_Group_Code<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and Account_Sub_Group_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsAccountSubGroup()
            obj.Account_Sub_Group_Code = clsCommon.myCstr(dt.Rows(0)("Account_Sub_Group_Code"))
            obj.Account_Sub_Group_Desc = clsCommon.myCstr(dt.Rows(0)("Account_Sub_Group_Desc"))
            obj.Account_Group_Code = clsCommon.myCstr(dt.Rows(0)("Account_Group_Code"))
        End If
        Return obj
    End Function
    Public Shared Function SaveData(ByVal obj As ClsAccountSubGroup, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
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
    Public Shared Function SaveData(ByVal obj As ClsAccountSubGroup, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            clsGLAccount.GetLinkAccountWithGroup(3, obj.Account_Sub_Group_Code, obj.Account_Group_Code, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Account_Group_Code", obj.Account_Group_Code, True)
            clsCommon.AddColumnsForChange(coll, "Account_Sub_Group_Desc", obj.Account_Sub_Group_Desc)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Account_Sub_Group_Code", obj.Account_Sub_Group_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_ACCOUNT_SUB_GROUPS where Account_Sub_Group_Code= '" & obj.Account_Sub_Group_Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACCOUNT_SUB_GROUPS", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACCOUNT_SUB_GROUPS", OMInsertOrUpdate.Update, "Account_Sub_Group_Code='" + obj.Account_Sub_Group_Code + "'", trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select Account_Sub_Group_Code from TSPL_ACCOUNT_SUB_GROUPS where Account_Sub_Group_Code ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
End Class
