Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsMainGLAccount
#Region "Variables"
    Public Main_GL_Account As String = Nothing
    Public Main_GL_Account_Desc As String = Nothing
    Public Sub_Group_Code As String = Nothing
    Public IsControlAcct As Integer = 0
#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account as [Code] ,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account_Desc AS [Description],TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code As [Sub Group Code],TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Desc AS [Sub Group Description],TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code As [Account Group Code],TSPL_ACCOUNT_GROUPS.Account_Group_Desc as [Account Group Desc],TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code as [Main Group Code],TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Desc as [Account Main Group Description],TSPL_ACCOUNT_MAIN_GROUPS.Group_Type as [Group Type],TSPL_ACCOUNT_MAIN_GL_ACCOUNT.IsControlAcct,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Created_By as [Created By] ,Convert(varchar,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Created_Date,103) as [Created Date] ,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Modify_By as [Modified By] ,Convert(varchar,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Modified_Date,103) as [Modified Date]   From TSPL_ACCOUNT_MAIN_GL_ACCOUNT  left outer join  TSPL_ACCOUNT_SUB_GROUPS on TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code  left outer join TSPL_ACCOUNT_GROUPS on  TSPL_ACCOUNT_GROUPS.Account_Group_Code= TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code  left outer join  TSPL_ACCOUNT_MAIN_GROUPS on TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code=TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code"
        str = clsCommon.ShowSelectForm("AccMainGrp", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsMainGLAccount
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            If clsMainGLAccount.IsMainGLaccountUsedOrNot(strCode) = True Then
                Throw New Exception("You can not delete this code because Code used in another Document.")
            End If
            Dim qry As String
            qry = "delete from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMainGLAccount
        Dim obj As clsMainGLAccount = Nothing
        Dim qry As String = "select * from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and Main_GL_Account = (select MIN(Main_GL_Account) from TSPL_ACCOUNT_MAIN_GL_ACCOUNT)"
            Case NavigatorType.Last
                qry += " and Main_GL_Account = (select Max(Main_GL_Account) from TSPL_ACCOUNT_MAIN_GL_ACCOUNT)"
            Case NavigatorType.Next
                qry += " and Main_GL_Account = (select Min(Main_GL_Account) from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where  Main_GL_Account>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and Main_GL_Account = (select Max(Main_GL_Account) from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and Main_GL_Account = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMainGLAccount()
            obj.Main_GL_Account = clsCommon.myCstr(dt.Rows(0)("Main_GL_Account"))
            obj.Main_GL_Account_Desc = clsCommon.myCstr(dt.Rows(0)("Main_GL_Account_Desc"))
            obj.Sub_Group_Code = clsCommon.myCstr(dt.Rows(0)("Sub_Group_Code"))
            obj.IsControlAcct = clsCommon.myCdbl(dt.Rows(0)("IsControlAcct"))
        End If
        Return obj
    End Function

    Public Shared Function SaveData(ByVal obj As clsMainGLAccount, ByVal isNewEntry As Boolean) As Boolean
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

    Public Shared Function SaveData(ByVal obj As clsMainGLAccount, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            clsGLAccount.GetLinkAccountWithGroup(4, obj.Main_GL_Account, obj.Sub_Group_Code, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Sub_Group_Code", obj.Sub_Group_Code)
            clsCommon.AddColumnsForChange(coll, "Main_GL_Account_Desc", obj.Main_GL_Account_Desc)
            clsCommon.AddColumnsForChange(coll, "IsControlAcct", obj.IsControlAcct)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" & obj.Main_GL_Account & "'", trans)
                    If ChkNewEntry = 0 Then
                        obj.Main_GL_Account = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.MainGLAccount, "", "")
                        If clsCommon.myLen(obj.Main_GL_Account) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "Main_GL_Account", obj.Main_GL_Account)

                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account= '" & obj.Main_GL_Account & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACCOUNT_MAIN_GL_ACCOUNT", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACCOUNT_MAIN_GL_ACCOUNT", OMInsertOrUpdate.Update, "Main_GL_Account='" + obj.Main_GL_Account + "'", trans)
                Dim strIsContrilAccount As Char = "N"
                If clsCommon.CompairString(obj.IsControlAcct, "1") = CompairStringResult.Equal Then
                    strIsContrilAccount = "Y"
                End If
                UpdateGLAccount(obj.Main_GL_Account, strIsContrilAccount, trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select Main_GL_Account from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Shared Function UpdateGLAccount(ByVal strGLMainCode As String, ByVal strControlAccount As Char, Optional ByVal trans As SqlTransaction = Nothing)
        Dim qry As String = "update TSPL_GL_ACCOUNTS set ControlAccount = '" + strControlAccount + "' where  GL_Main_Code = '" + strGLMainCode + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Return True
    End Function
    'Ticket No : TEC/08/07/19-000935 By prabakar
    Public Shared Function IsMainGLaccountUsedOrNot(ByVal strGLMainAccount As String, Optional ByVal trans As SqlTransaction = Nothing)
        Dim Qry As String = " select sum(No_of_Record) from ( Select Count (TSPL_VENDOR_ACCOUNT_SET.Payable_Account) as No_of_Record  from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.Payable_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.EXCHANGE_LOSS_ACCOUNT) as No_of_Record from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.EXCHANGE_LOSS_ACCOUNT where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.EXCHANGE_GAIN_ACCOUNT) as No_of_Record  from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.EXCHANGE_GAIN_ACCOUNT where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.SECURITY_ACCOUNT)  as No_of_Record from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.SECURITY_ACCOUNT where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.Head_Load_ACCOUNT) as No_of_Record from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.Head_Load_ACCOUNT where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.Own_Asset_ACCOUNT) as No_of_Record from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.Own_Asset_ACCOUNT where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.Deduction_ACCOUNT) as No_of_Record from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.Deduction_ACCOUNT where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.Commission_ACCOUNT) as No_of_Record from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.Commission_ACCOUNT where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.Incentive_ACCOUNT) as No_of_Record  from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.Incentive_ACCOUNT where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary) as No_of_Record  from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.Employee_Salary)  as No_of_Record from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.Employee_Salary where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Travelling)  as No_of_Record from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Travelling where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Imprest)  as No_of_Record from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Imprest where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.Discount_Account) as No_of_Record  from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.Discount_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.Advance_Account)  as No_of_Record from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.Advance_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.Freight_Provision) as No_of_Record  from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.Freight_Provision where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.Handling_Charges) as No_of_Record  from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.Handling_Charges where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.Round_Off) as No_of_Record  from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.Round_Off where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.Short_Excess) as No_of_Record  from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.Short_Excess where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.Opening_Clearing) as No_of_Record  from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.Opening_Clearing where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.Security_Opening_Clearing)  as No_of_Record from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.Security_Opening_Clearing where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_VENDOR_ACCOUNT_SET.Monthly_Rent_Account)  as No_of_Record from TSPL_VENDOR_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_ACCOUNT_SET.Monthly_Rent_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.Container_Deposit)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.Container_Deposit where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.EXCHANGE_LOSS_ACCOUNT)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.EXCHANGE_LOSS_ACCOUNT where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.EXCHANGE_GAIN_ACCOUNT)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.EXCHANGE_GAIN_ACCOUNT where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.SECURITY_ACCOUNT)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.SECURITY_ACCOUNT where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.CREATE_SECURITY_ACCOUNT)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.CREATE_SECURITY_ACCOUNT where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.BANK_GUARANTEE)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.BANK_GUARANTEE where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.ACCOUNT1)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.ACCOUNT1 where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.ACCOUNT2)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.ACCOUNT2 where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.GSOC_Acct)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.GSOC_Acct where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.Consignment_Acct)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.Consignment_Acct where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.Gain_Acct)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.Gain_Acct where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.Loss_Acct)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.Loss_Acct where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.Receipts_Discount_acct)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.Receipts_Discount_acct where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.Advance_acct)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.Advance_acct where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.Write_Offs)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.Write_Offs where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.Foreign_Bank_Charges_Account)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.Foreign_Bank_Charges_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.Bank_Charges_Other_Account)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.Bank_Charges_Other_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.SubSidy_Account)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.SubSidy_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.Leakage_Deduction)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.Leakage_Deduction where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.Customer_Opening_Clearing_AC)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.Customer_Opening_Clearing_AC where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_CUSTOMER_ACCOUNT_SET.Customer_Security_Opening_Clearing_AC)  as No_of_Record from TSPL_CUSTOMER_ACCOUNT_SET inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_CUSTOMER_ACCOUNT_SET.Customer_Security_Opening_Clearing_AC where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Adjustment_Account)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Adjustment_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Assembly_Cost_Credit)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Assembly_Cost_Credit where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Transfer_Clearing)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Transfer_Clearing where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "'  " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Shipment_Clearing)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Shipment_Clearing where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Disassembly_Expense)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Disassembly_Expense where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Physical_Inv_Adjustment)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Physical_Inv_Adjustment where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Reserve_Stock)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Reserve_Stock where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Breakage_Gl_Account)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Breakage_Gl_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.WIP_Account)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.WIP_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.RM_Consumption)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.RM_Consumption where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Other_1)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Other_1 where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Other_2)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Other_2 where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Loss_Ac)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Loss_Ac where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Purchase_Control_Account)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Purchase_Control_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Transfer_Gain_Loss_Ac)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Transfer_Gain_Loss_Ac where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Job_Work_Ac)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Job_Work_Ac where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Stock_Transfer_In)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Stock_Transfer_In where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Stock_Transfer_Acc)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Stock_Transfer_Acc where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Provision_Clearing)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Provision_Clearing where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Purchase_Account)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Purchase_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Purchase_Set_Off)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Purchase_Set_Off where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Purchase_JobWork)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Purchase_JobWork where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Difference_Account)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Difference_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Stock_Transfer_JobWork)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Stock_Transfer_JobWork where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Handling_Charge)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Handling_Charge where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Store_Consumption_Acc)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Store_Consumption_Acc where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.FA_CLEARING_AC)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.FA_CLEARING_AC where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Purchase_Loss)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Purchase_Loss where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Wrekage_Account)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Wrekage_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Item_Opening_Clearing)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Item_Opening_Clearing where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Chilling_Charges)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Chilling_Charges where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.Freight_Charges)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.Freight_Charges where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_PURCHASE_ACCOUNTS.EMP)  as No_of_Record from TSPL_PURCHASE_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_ACCOUNTS.EMP where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_SALES_ACCOUNTS.Sales_Account)  as No_of_Record from TSPL_SALES_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_SALES_ACCOUNTS.Sales_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_SALES_ACCOUNTS.Sales_Return_Account)  as No_of_Record from TSPL_SALES_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_SALES_ACCOUNTS.Sales_Return_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_SALES_ACCOUNTS.Cost_Of_Goods_Sold)  as No_of_Record from TSPL_SALES_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_SALES_ACCOUNTS.Cost_Of_Goods_Sold where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_SALES_ACCOUNTS.Cost_Variance)  as No_of_Record from TSPL_SALES_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_SALES_ACCOUNTS.Cost_Variance where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_SALES_ACCOUNTS.Damaged_Goods)  as No_of_Record from TSPL_SALES_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_SALES_ACCOUNTS.Damaged_Goods where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_SALES_ACCOUNTS.Internal_Usage)  as No_of_Record from TSPL_SALES_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_SALES_ACCOUNTS.Internal_Usage where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_SALES_ACCOUNTS.Returnable_Container)  as No_of_Record from TSPL_SALES_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_SALES_ACCOUNTS.Returnable_Container where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_SALES_ACCOUNTS.Schemes)  as No_of_Record from TSPL_SALES_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_SALES_ACCOUNTS.Schemes where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_SALES_ACCOUNTS.Promotional)  as No_of_Record from TSPL_SALES_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_SALES_ACCOUNTS.Promotional where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_SALES_ACCOUNTS.Cogs_InterBranch)  as No_of_Record from TSPL_SALES_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_SALES_ACCOUNTS.Cogs_InterBranch where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_SALES_ACCOUNTS.Suspence_Account)  as No_of_Record from TSPL_SALES_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_SALES_ACCOUNTS.Suspence_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_SALES_ACCOUNTS.Gain_Loss_Account)  as No_of_Record from TSPL_SALES_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_SALES_ACCOUNTS.Gain_Loss_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_SALES_ACCOUNTS.Stock_Transfer_AC)  as No_of_Record from TSPL_SALES_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_SALES_ACCOUNTS.Stock_Transfer_AC where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_SALES_ACCOUNTS.COGT_AC)  as No_of_Record from TSPL_SALES_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_SALES_ACCOUNTS.COGT_AC where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_SALES_ACCOUNTS.DisplayPurpose_Account)  as No_of_Record from TSPL_SALES_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_SALES_ACCOUNTS.DisplayPurpose_Account where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all  " & _
                            " Select Count (TSPL_SALES_ACCOUNTS.Cost_Of_Goods_Scheme)  as No_of_Record from TSPL_SALES_ACCOUNTS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_SALES_ACCOUNTS.Cost_Of_Goods_Scheme where TSPL_GL_ACCOUNTS.GL_Main_Code =  '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account)  as No_of_Record from TSPL_BRANCH_ACCOUNT_MAPPING inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_BANK_MASTER.BANKACC)  as No_of_Record from TSPL_BANK_MASTER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_BANK_MASTER.BANKACC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_BANK_MASTER.WRITEOFFACC)  as No_of_Record from TSPL_BANK_MASTER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_BANK_MASTER.WRITEOFFACC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_BANK_MASTER.CREDITACC)  as No_of_Record from TSPL_BANK_MASTER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_BANK_MASTER.CREDITACC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_BANK_MASTER.TRANSFER_CLEARING_ACCOUNT)  as No_of_Record from TSPL_BANK_MASTER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_BANK_MASTER.TRANSFER_CLEARING_ACCOUNT where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_BANK_MASTER.BANK_Opening_Clearing_Account)  as No_of_Record from TSPL_BANK_MASTER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_BANK_MASTER.BANK_Opening_Clearing_Account where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all" & _
                            " Select Count (TSPL_TAX_MASTER.Tax_Recoverable_Account)  as No_of_Record from TSPL_TAX_MASTER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_TAX_MASTER.Tax_Recoverable_Account where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "'  " & _
                            " Union all " & _
                            " Select Count (TSPL_TAX_MASTER.Tax_Recoverable_Account2)  as No_of_Record from TSPL_TAX_MASTER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_TAX_MASTER.Tax_Recoverable_Account2 where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_TAX_MASTER.Tax_Recoverable_Account3)  as No_of_Record from TSPL_TAX_MASTER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_TAX_MASTER.Tax_Recoverable_Account3 where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_TAX_MASTER.Tax_Recoverable_Account4)  as No_of_Record from TSPL_TAX_MASTER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_TAX_MASTER.Tax_Recoverable_Account4 where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_TAX_MASTER.Tax_Recoverable_Account5)  as No_of_Record from TSPL_TAX_MASTER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_TAX_MASTER.Tax_Recoverable_Account5 where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_TAX_MASTER.Tax_Liability_Account)  as No_of_Record from TSPL_TAX_MASTER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_TAX_MASTER.Tax_Liability_Account where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_TAX_MASTER.Tax_Net_Payable)  as No_of_Record from TSPL_TAX_MASTER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_TAX_MASTER.Tax_Net_Payable where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_GL_SEGMENT_CODE.Account_Code)  as No_of_Record from TSPL_GL_SEGMENT_CODE inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_GL_SEGMENT_CODE.Account_Code where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_ITEM_MASTER.GL_account)  as No_of_Record from TSPL_ITEM_MASTER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_ITEM_MASTER.GL_account where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_ITEM_LOCATION_MAPPING.GL_Acc)  as No_of_Record from TSPL_ITEM_LOCATION_MAPPING inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_ITEM_LOCATION_MAPPING.GL_Acc where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_PURCHASE_SETTINGS.Job_Work_Account)  as No_of_Record from TSPL_PURCHASE_SETTINGS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PURCHASE_SETTINGS.Job_Work_Account where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_TDS_DEDUCTION_HEAD.Gl_Account)  as No_of_Record from TSPL_TDS_DEDUCTION_HEAD inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_TDS_DEDUCTION_HEAD.Gl_Account where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_PAYROLL_ACCOUNTSETS.GL_SALARY_PAYABLE)  as No_of_Record from TSPL_PAYROLL_ACCOUNTSETS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PAYROLL_ACCOUNTSETS.GL_SALARY_PAYABLE where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_PAYROLL_ACCOUNTSETS.GL_Employer_PF_PAYABLE)  as No_of_Record from TSPL_PAYROLL_ACCOUNTSETS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PAYROLL_ACCOUNTSETS.GL_Employer_PF_PAYABLE where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "'  " & _
                            " Union all " & _
                            " Select Count (TSPL_PAYROLL_ACCOUNTSETS.GL_Employer_ESI_PAYABLE)  as No_of_Record from TSPL_PAYROLL_ACCOUNTSETS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PAYROLL_ACCOUNTSETS.GL_Employer_ESI_PAYABLE where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "'  " & _
                            " Union all " & _
                            " Select Count (TSPL_PAYROLL_ACCOUNTSETS.GL_EMPLOYER_OTHERS_PAYABLE)  as No_of_Record from TSPL_PAYROLL_ACCOUNTSETS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PAYROLL_ACCOUNTSETS.GL_EMPLOYER_OTHERS_PAYABLE where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_JOURNAL_DETAILS.Account_code)  as No_of_Record from TSPL_JOURNAL_DETAILS inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_JOURNAL_DETAILS.Account_code where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC)  as No_of_Record from TSPL_VENDOR_INVOICE_HEAD inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_VENDOR_INVOICE_HEAD.Discount_GL_AC)  as No_of_Record from TSPL_VENDOR_INVOICE_HEAD inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_INVOICE_HEAD.Discount_GL_AC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_VENDOR_INVOICE_HEAD.TAX1_GLAC)  as No_of_Record from TSPL_VENDOR_INVOICE_HEAD inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_INVOICE_HEAD.TAX1_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_VENDOR_INVOICE_HEAD.TAX2_GLAC)  as No_of_Record from TSPL_VENDOR_INVOICE_HEAD inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_INVOICE_HEAD.TAX2_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_VENDOR_INVOICE_HEAD.TAX3_GLAC)  as No_of_Record from TSPL_VENDOR_INVOICE_HEAD inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_INVOICE_HEAD.TAX3_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_VENDOR_INVOICE_HEAD.TAX4_GLAC)  as No_of_Record from TSPL_VENDOR_INVOICE_HEAD inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_INVOICE_HEAD.TAX4_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_VENDOR_INVOICE_HEAD.TAX5_GLAC)  as No_of_Record from TSPL_VENDOR_INVOICE_HEAD inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_INVOICE_HEAD.TAX5_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_VENDOR_INVOICE_HEAD.TAX6_GLAC)  as No_of_Record from TSPL_VENDOR_INVOICE_HEAD inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_INVOICE_HEAD.TAX6_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_VENDOR_INVOICE_HEAD.TAX7_GLAC)  as No_of_Record from TSPL_VENDOR_INVOICE_HEAD inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_INVOICE_HEAD.TAX7_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_VENDOR_INVOICE_HEAD.TAX8_GLAC)  as No_of_Record from TSPL_VENDOR_INVOICE_HEAD inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_INVOICE_HEAD.TAX8_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_VENDOR_INVOICE_HEAD.TAX9_GLAC)  as No_of_Record from TSPL_VENDOR_INVOICE_HEAD inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_INVOICE_HEAD.TAX9_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_VENDOR_INVOICE_HEAD.TAX10_GLAC)  as No_of_Record from TSPL_VENDOR_INVOICE_HEAD inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_VENDOR_INVOICE_HEAD.TAX10_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_PAYMENT_HEADER.Debit_Account)  as No_of_Record from TSPL_PAYMENT_HEADER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PAYMENT_HEADER.Debit_Account where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_PAYMENT_HEADER.Credit_Account)  as No_of_Record from TSPL_PAYMENT_HEADER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PAYMENT_HEADER.Credit_Account where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_ACCOUNT)  as No_of_Record from TSPL_PAYMENT_HEADER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_ACCOUNT where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_ACCOUNT)  as No_of_Record from TSPL_PAYMENT_HEADER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_ACCOUNT where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_Customer_Invoice_Head.Customer_Control_AC)  as No_of_Record from TSPL_Customer_Invoice_Head inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_Customer_Invoice_Head.Customer_Control_AC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_Customer_Invoice_Head.TAX1_GLAC)  as No_of_Record from TSPL_Customer_Invoice_Head inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_Customer_Invoice_Head.TAX1_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_Customer_Invoice_Head.TAX2_GLAC)  as No_of_Record from TSPL_Customer_Invoice_Head inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_Customer_Invoice_Head.TAX2_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_Customer_Invoice_Head.TAX3_GLAC)  as No_of_Record from TSPL_Customer_Invoice_Head inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_Customer_Invoice_Head.TAX3_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_Customer_Invoice_Head.TAX4_GLAC)  as No_of_Record from TSPL_Customer_Invoice_Head inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_Customer_Invoice_Head.TAX4_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_Customer_Invoice_Head.TAX5_GLAC)  as No_of_Record from TSPL_Customer_Invoice_Head inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_Customer_Invoice_Head.TAX5_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_Customer_Invoice_Head.TAX6_GLAC)  as No_of_Record from TSPL_Customer_Invoice_Head inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_Customer_Invoice_Head.TAX6_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_Customer_Invoice_Head.TAX7_GLAC)  as No_of_Record from TSPL_Customer_Invoice_Head inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_Customer_Invoice_Head.TAX7_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_Customer_Invoice_Head.TAX8_GLAC)  as No_of_Record from TSPL_Customer_Invoice_Head inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_Customer_Invoice_Head.TAX8_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_Customer_Invoice_Head.TAX9_GLAC)  as No_of_Record from TSPL_Customer_Invoice_Head inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_Customer_Invoice_Head.TAX9_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_Customer_Invoice_Head.TAX10_GLAC)  as No_of_Record from TSPL_Customer_Invoice_Head inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_Customer_Invoice_Head.TAX10_GLAC where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_RECEIPT_HEADER.Dr_Account)  as No_of_Record from TSPL_RECEIPT_HEADER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_RECEIPT_HEADER.Dr_Account where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_RECEIPT_HEADER.Cr_Account)  as No_of_Record from TSPL_RECEIPT_HEADER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_RECEIPT_HEADER.Cr_Account where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_ACCOUNT)  as No_of_Record from TSPL_RECEIPT_HEADER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_ACCOUNT where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " Union all " & _
                            " Select Count (TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_ACCOUNT)  as No_of_Record from TSPL_RECEIPT_HEADER inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_ACCOUNT where TSPL_GL_ACCOUNTS.GL_Main_Code = '" + strGLMainAccount + "' " & _
                            " " & _
                            " ) Final	"
        Dim isAccountUsed As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(Qry, trans))
        Return isAccountUsed
    End Function

End Class
