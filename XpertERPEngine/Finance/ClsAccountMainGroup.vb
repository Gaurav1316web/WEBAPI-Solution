Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsAccountMainGroup
#Region "Variables"
    Public Account_Main_Group_Code As String = Nothing
    Public Account_Main_Group_Desc As String = Nothing
    Public Group_Type As String = Nothing
#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code as [Code] ,TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Desc AS [Description],TSPL_ACCOUNT_MAIN_GROUPS.Group_Type As [Group Type],TSPL_ACCOUNT_MAIN_GROUPS.Created_By as [Created By] ,Convert(varchar,TSPL_ACCOUNT_MAIN_GROUPS.Created_Date,103) as [Created Date] ,TSPL_ACCOUNT_MAIN_GROUPS.Modify_By as [Modified By] ,Convert(varchar,TSPL_ACCOUNT_MAIN_GROUPS.Modified_Date,103) as [Modified Date]  From TSPL_ACCOUNT_MAIN_GROUPS "
        str = clsCommon.ShowSelectForm("AccMainGrp", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsAccountMainGroup
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
            qry = "delete from TSPL_ACCOUNT_MAIN_GROUPS where Account_Main_Group_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsAccountMainGroup
        Dim obj As ClsAccountMainGroup = Nothing
        Dim qry As String = "select * from TSPL_ACCOUNT_MAIN_GROUPS where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and Account_Main_Group_Code = (select MIN(Account_Main_Group_Code) from TSPL_ACCOUNT_MAIN_GROUPS)"
            Case NavigatorType.Last
                qry += " and Account_Main_Group_Code = (select Max(Account_Main_Group_Code) from TSPL_ACCOUNT_MAIN_GROUPS)"
            Case NavigatorType.Next
                qry += " and Account_Main_Group_Code = (select Min(Account_Main_Group_Code) from TSPL_ACCOUNT_MAIN_GROUPS where  Account_Main_Group_Code>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and Account_Main_Group_Code = (select Max(Account_Main_Group_Code) from TSPL_ACCOUNT_MAIN_GROUPS where Account_Main_Group_Code<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and Account_Main_Group_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsAccountMainGroup()
            obj.Account_Main_Group_Code = clsCommon.myCstr(dt.Rows(0)("Account_Main_Group_Code"))
            obj.Account_Main_Group_Desc = clsCommon.myCstr(dt.Rows(0)("Account_Main_Group_Desc"))
            obj.Group_Type = clsCommon.myCstr(dt.Rows(0)("Group_Type"))
        End If
        Return obj
    End Function

    Public Shared Function SaveData(ByVal obj As ClsAccountMainGroup, ByVal isNewEntry As Boolean) As Boolean
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

    Public Shared Function SaveData(ByVal obj As ClsAccountMainGroup, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            clsGLAccount.GetLinkAccountWithGroup(1, obj.Account_Main_Group_Code, obj.Group_Type, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Group_Type", obj.Group_Type)
            clsCommon.AddColumnsForChange(coll, "Account_Main_Group_Desc", obj.Account_Main_Group_Desc)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Account_Main_Group_Code", obj.Account_Main_Group_Code)

                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_ACCOUNT_MAIN_GROUPS where Account_Main_Group_Code= '" & obj.Account_Main_Group_Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACCOUNT_MAIN_GROUPS", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACCOUNT_MAIN_GROUPS", OMInsertOrUpdate.Update, "Account_Main_Group_Code='" + obj.Account_Main_Group_Code + "'", trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select Account_Main_Group_Code from TSPL_ACCOUNT_MAIN_GROUPS where Account_Main_Group_Code ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
End Class
