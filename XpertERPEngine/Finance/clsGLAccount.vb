'Imports common
'Imports System.Data.SqlClient
'Imports Telerik.WinControls.UI
'Imports Telerik.WinControls
'Imports System.Data
'Imports System.Windows.Forms
'Imports System.Configuration
'Imports Telerik.Collections.Generic
'Imports System.Globalization

'Public Class clsGLAccount
'#Region "Variables"
'    Public Account_Code As String = Nothing
'    Public Description As String = Nothing
'    Public Str_Code As String = Nothing
'    Public Str_Description As String = Nothing
'    Public Account_Balance As String = Nothing
'    Public Status As String = Nothing
'    Public ControlAccount As String = Nothing
'    Public AutoAllocation As String = Nothing
'    Public multicurrency As String = Nothing
'    Public Account_Seg_Code1 As String = Nothing
'    Public Account_Seg_Desc1 As String = Nothing
'    Public Account_Seg_Code2 As String = Nothing
'    Public Account_Seg_Desc2 As String = Nothing
'    Public Account_Seg_Code3 As String = Nothing
'    Public Account_Seg_Desc3 As String = Nothing
'    Public Account_Seg_Code4 As String = Nothing
'    Public Account_Seg_Desc4 As String = Nothing
'    Public Account_Seg_Code5 As String = Nothing
'    Public Account_Seg_Desc5 As String = Nothing
'    Public Account_Seg_Code6 As String = Nothing
'    Public Account_Seg_Desc6 As String = Nothing
'    Public Account_Seg_Code7 As String = Nothing
'    Public Account_Seg_Desc7 As String = Nothing
'    Public Account_Seg_Code8 As String = Nothing
'    Public Account_Seg_Desc8 As String = Nothing
'    Public Account_Seg_Code9 As String = Nothing
'    Public Account_Seg_Desc9 As String = Nothing
'    Public Account_Seg_Code10 As String = Nothing
'    Public Account_Seg_Desc10 As String = Nothing
'    Public Close_To_Seg As String = Nothing
'    Public Close_To_Acct As String = Nothing
'    Public TallyAccName As String = Nothing
'    Public Tax_Type As String = Nothing
'    Public Purchase_Sale_Type As Integer = 0
'    Public GL_Main_Code As String = Nothing
'#End Region

'    Public Shared Function SaveData(ByVal obj As clsGLAccount) As Boolean
'        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
'        Try
'            SaveData(obj, trans)
'            trans.Commit()
'        Catch ex As Exception
'            trans.Rollback()
'            Throw New Exception(ex.Message)
'        End Try
'        Return True
'    End Function

'    Public Shared Function SaveData(ByVal obj As clsGLAccount, ByVal trans As SqlTransaction) As Boolean
'        clsGLAccount.GetLinkAccountWithGroup(5, obj.Account_Code, obj.GL_Main_Code, trans)

'        Dim coll As New Hashtable()
'        clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
'        clsCommon.AddColumnsForChange(coll, "Str_Code", obj.Str_Code)
'        clsCommon.AddColumnsForChange(coll, "Str_Description", obj.Str_Description)
'        clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
'        clsCommon.AddColumnsForChange(coll, "ControlAccount", obj.ControlAccount)
'        clsCommon.AddColumnsForChange(coll, "AutoAllocation", obj.AutoAllocation)
'        clsCommon.AddColumnsForChange(coll, "multicurrency", obj.multicurrency)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code1", obj.Account_Seg_Code1)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc1", obj.Account_Seg_Desc1)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code2", obj.Account_Seg_Code2)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc2", obj.Account_Seg_Desc2)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code3", obj.Account_Seg_Code3)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc3", obj.Account_Seg_Desc3)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code4", obj.Account_Seg_Code4)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc4", obj.Account_Seg_Desc4)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code5", obj.Account_Seg_Code5)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc5", obj.Account_Seg_Desc5)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code6", obj.Account_Seg_Code6)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc6", obj.Account_Seg_Desc6)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code7", obj.Account_Seg_Code7)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc7", obj.Account_Seg_Desc7)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code8", obj.Account_Seg_Code8)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc8", obj.Account_Seg_Desc8)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code9", obj.Account_Seg_Code9)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc9", obj.Account_Seg_Desc9)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code10", obj.Account_Seg_Code10)
'        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc10", obj.Account_Seg_Desc10)

'        clsCommon.AddColumnsForChange(coll, "Close_To_Seg", obj.Close_To_Seg)
'        clsCommon.AddColumnsForChange(coll, "Close_To_Acct", obj.Close_To_Acct)
'        clsCommon.AddColumnsForChange(coll, "TallyAccName", obj.TallyAccName)
'        clsCommon.AddColumnsForChange(coll, "Tax_Type", obj.Tax_Type)
'        clsCommon.AddColumnsForChange(coll, "Purchase_Sale_Type", obj.Purchase_Sale_Type)
'        clsCommon.AddColumnsForChange(coll, "GL_Main_Code", obj.GL_Main_Code)
'        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
'        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
'        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

'        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select 1 from TSPL_GL_ACCOUNTS where Account_Code='" + obj.Account_Code + "'", trans)
'        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
'            clsCommon.AddColumnsForChange(coll, "Account_Code", obj.Account_Code)
'            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
'            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
'            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_ACCOUNTS", OMInsertOrUpdate.Insert, "", trans)
'        Else
'            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_ACCOUNTS", OMInsertOrUpdate.Update, "Account_Code='" + obj.Account_Code + "'", trans)
'        End If
'        Return True
'    End Function

'    Public Shared Function GetName(ByVal Code As String) As String
'        Return GetName(Code, Nothing)
'    End Function

'    Public Shared Function GetName(ByVal Code As String, ByVal trans As SqlTransaction) As String
'        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + Code + "'", trans))
'    End Function

'    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
'        Dim str As String = " select Account_Code as [Code] ,Description as [Description],TSPL_GL_ACCOUNTS.GL_Main_Code as [GL Main Account Code],TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account_Desc AS [GL Main Account Description],TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code As [Sub Group Code],TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Desc AS [Sub Group Description],TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code As [Account Group Code],TSPL_ACCOUNT_GROUPS.Account_Group_Desc as [Account Group Desc],TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code as [Main Group Code],TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Desc as [Account Main Group Description],TSPL_ACCOUNT_MAIN_GROUPS.Group_Type as [Group Type] ,Str_Code as [Account Structure Code] ,Str_Description as [Account Structure Description] ,(case when status='Y' then 'Active' else 'In Active' end) as [Status] ,ControlAccount as [Control Account]  ,multicurrency as [Multi Currency] ,Account_Seg_Code1 as [Account Segment Code1] ,Account_Seg_Desc1 as [Account Segment Description1] ,Account_Seg_Code2 as [Account Segment Code2] ,Account_Seg_Desc2 as [Account Segment Description2] ,Account_Seg_Code3 as [Account Segment Code3] ,Account_Seg_Desc3 as [Account Segment Description3] ,Account_Seg_Code4 as [Account Segment Code4] ,Account_Seg_Desc4 as [Account Segment Description4] ,Account_Seg_Code5 as [Account Segment Code5] ,Account_Seg_Desc5 as [Account Segment Description5] ,Account_Seg_Code6 as [Account Segment Code6] ,Account_Seg_Desc6 as [Account Segment Description6] ,Account_Seg_Code7 as [Account Segment Code7] ,Account_Seg_Desc7 as [Account Segment Description7] ,Account_Seg_Code8 as [Account Segment Code8] ,Account_Seg_Desc8 as [Account Segment Description8] ,Account_Seg_Code9 as [Account Segment Code9] ,Account_Seg_Desc9 as [Account Segment Description9] ,Account_Seg_Code10 as [Account Segment Code10] ,Account_Seg_Desc10 as [Account Segment Description10] ,Close_To_Seg as [Close To Segment] ,Close_To_Acct as [Close To Account]  ,Rollup_Seq as [Roll Up Sequence] ,TallyAccName as [Tally Account Name] ,Tax_Type as [Tax Type] ,Purchase_Sale_Type as [Purchase Sale Type],TSPL_GL_ACCOUNTS.Created_By as [Created By] ,TSPL_GL_ACCOUNTS.Created_Date as [Created Date] ,TSPL_GL_ACCOUNTS.Modify_By as [Modify By] ,TSPL_GL_ACCOUNTS.Modify_Date as [Modify Date] ,TSPL_GL_ACCOUNTS.Comp_Code as [Company Code]  from TSPL_GL_ACCOUNTS  left outer join TSPL_ACCOUNT_MAIN_GL_ACCOUNT on TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account=TSPL_GL_ACCOUNTS.GL_Main_Code  left outer join TSPL_ACCOUNT_SUB_GROUPS on TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code  left outer join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code= TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code left outer join  TSPL_ACCOUNT_MAIN_GROUPS on TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code=TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code "
'        str = clsCommon.ShowSelectForm("RPTGLACFND", str, "Code", whrcls, curcode, "Code", isButtonClicked)
'        Return str
'    End Function

'    Public Shared Function Get_Location_Segment(ByVal Code As String, ByVal trans As SqlTransaction) As String
'        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code='" + Code + "'", trans))
'    End Function

'    Public Shared Function CheckControlAccount(ByVal strGLCode As String, ByVal ControlAccountStatus As Boolean, ByVal trans As SqlTransaction) As Boolean
'        ''Ticket no BM00000009848 by balwinder on 24/10/2016
'        Dim qry As String = "select ControlAccount from TSPL_GL_ACCOUNTS where Account_Code='" + strGLCode + "'"
'        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
'        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
'            Dim res As Boolean = clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("ControlAccount")), "Y") = CompairStringResult.Equal
'            If Not res = ControlAccountStatus Then
'                ''IF Before save status is control account than only check it is used in GL Entry or any account set table
'                qry = "select 1 from TSPL_JOURNAL_DETAILS where Account_code='" + strGLCode + "'"
'                dt = clsDBFuncationality.GetDataTable(qry, trans)
'                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
'                    Throw New Exception("Account - " + strGLCode + Environment.NewLine + " Can't change control account to Normal account becuase it is used in GL Entry")
'                End If
'                qry = "select 'select '''+COLUMN_NAME+''' as FaultCol,''Customer Account Set'' as FaultAccountSet  from '+TABLE_NAME+' where '+ COLUMN_NAME +'='+'''" + strGLCode + "'''+'' from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME ='TSPL_CUSTOMER_ACCOUNT_SET'  and COLUMN_NAME not in ('CURRENCY_CODE','Created_By','Created_Date','Modify_By','Modify_Date','Comp_Code','Cust_Account','Cust_Acct_Desc') and DATA_TYPE like '%char%' " + Environment.NewLine + _
'                " union all " + Environment.NewLine + _
'                " select 'select '''+COLUMN_NAME+''' as FaultCol,''Vendor Account Set'' as FaultAccountSet  from '+TABLE_NAME+' where '+ COLUMN_NAME +'='+'''" + strGLCode + "'''+''  from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME ='TSPL_VENDOR_ACCOUNT_SET'  and COLUMN_NAME not in ('CURRENCY_CODE','Created_By','Created_Date','Modify_By','Modify_Date','Comp_Code','Acct_Set_Code','Acct_Set_Desc') and DATA_TYPE like '%char%' " + Environment.NewLine + _
'                " union all " + Environment.NewLine + _
'                " select 'select '''+COLUMN_NAME+''' as FaultCol,''Purchase Account Set'' as FaultAccountSet  from '+TABLE_NAME+' where '+ COLUMN_NAME +'='+'''" + strGLCode + "'''+''  from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME ='TSPL_PURCHASE_ACCOUNTS'  and COLUMN_NAME not in ('Costing_Method','Created_By','Created_Date','Modify_By','Modify_Date','Comp_Code','Purchase_Class_Code','Purchase_Class_Desc') and DATA_TYPE like '%char%' " + Environment.NewLine + _
'                " union all " + Environment.NewLine + _
'                " select 'select '''+COLUMN_NAME+''' as FaultCol,''Sales Account Set'' as FaultAccountSet  from '+TABLE_NAME+' where '+ COLUMN_NAME +'='+'''" + strGLCode + "'''+''  from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME ='TSPL_SALES_ACCOUNTS'  and COLUMN_NAME not in ('Created_By','Created_Date','Modify_By','Modify_Date','Comp_Code','Sales_Class_Code','Sales_Class_Desc') and DATA_TYPE like '%char%' " + Environment.NewLine + _
'                " union all " + Environment.NewLine + _
'                " select 'select '''+COLUMN_NAME+''' as FaultCol,''Fixed Asset Account Set'' as FaultAccountSet  from '+TABLE_NAME+' where '+ COLUMN_NAME +'='+'''" + strGLCode + "'''+''  from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME ='TSPL_Dep_AccountSet' and COLUMN_NAME not in ('AcSet_Code','AcSet_Desc','Inactive','Created_By','Created_Date','Modified_By','Modified_Date','Comp_Code') and DATA_TYPE like '%char%' " + Environment.NewLine + _
'                " union all " + Environment.NewLine + _
'                " select 'select '''+COLUMN_NAME+''' as FaultCol,''Payroll Account Set'' as FaultAccountSet  from '+TABLE_NAME+' where '+ COLUMN_NAME +'='+'''" + strGLCode + "'''+''  from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME ='TSPL_PAYROLL_ACCOUNTSETS'  and COLUMN_NAME not in ('ACCOUNT_SET_CODE','DESCRIPTION','BANK_CODE','Created_By','Created_Date','Modified_By','Modified_Date') and DATA_TYPE like '%char%'"
'                dt = clsDBFuncationality.GetDataTable(qry, trans)
'                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
'                    qry = ""
'                    Dim isFirstTime As Boolean = True
'                    For Each dr As DataRow In dt.Rows
'                        If Not isFirstTime Then
'                            qry += " Union all " + Environment.NewLine
'                        End If
'                        qry += clsCommon.myCstr(dr(0))
'                        isFirstTime = False
'                    Next
'                    qry += "union all " + Environment.NewLine + _
'                    " select 'BANKACC' as FaultCol,'Bank Master' as FaultAccountSet  from TSPL_BANK_MASTER where BANKACC='" + strGLCode + "'" + Environment.NewLine + _
'                    " union all " + Environment.NewLine + _
'                    " select 'WRITEOFFACC' as FaultCol,'Bank Master' as FaultAccountSet  from TSPL_BANK_MASTER where WRITEOFFACC='" + strGLCode + "'" + Environment.NewLine + _
'                    " union all " + Environment.NewLine + _
'                    " select 'CREDITACC' as FaultCol,'Bank Master' as FaultAccountSet  from TSPL_BANK_MASTER where CREDITACC='" + strGLCode + "'" + Environment.NewLine + _
'                    " union all " + Environment.NewLine + _
'                    " select 'Transfer_Clearing_Account' as FaultCol,'Bank Master' as FaultAccountSet  from TSPL_BANK_MASTER where Transfer_Clearing_Account='" + strGLCode + "'" + Environment.NewLine + _
'                    "union all " + Environment.NewLine + _
'                    " select 'Closing_Account' as FaultCol,'GL Option' as FaultAccountSet  from TSPL_GLSETTING where Closing_Account='" + strGLCode + "' " + Environment.NewLine + _
'                    " union all " + Environment.NewLine + _
'                    " select 'Clearing_Account' as FaultCol,'GL Option' as FaultAccountSet  from TSPL_GLSETTING where Clearing_Account='" + strGLCode + "'" + Environment.NewLine + _
'                    " union all " + Environment.NewLine + _
'                    " select 'Gl_Account' as FaultCol,'Nature of Deduction' as FaultAccountSet  from TSPL_TDS_DEDUCTION_HEAD where Gl_Account='" + strGLCode + "'"
'                    dt = clsDBFuncationality.GetDataTable(qry, trans)
'                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
'                        qry = "Account - " + strGLCode + Environment.NewLine + " Can't change control account to Normal account because Account is mapped with following account Set"
'                        For Each dr As DataRow In dt.Rows
'                            qry += Environment.NewLine + clsCommon.myCstr(dr("FaultAccountSet")) + " [" + clsCommon.myCstr(dr("FaultCol")) + "]"
'                        Next
'                        Throw New Exception(qry)
'                    End If
'                End If
'            End If
'            qry = Nothing
'            dt = Nothing
'        End If
'        Return True
'    End Function

'    Public Shared Function GetLinkAccountWithGroup(ByVal level As Integer, ByVal strCode As String, ByVal strNewValue As String, ByVal trans As SqlTransaction) As Boolean
'        Dim qry As String = "select TSPL_ACCOUNT_MAIN_GROUPS.Group_Type,TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code ,TSPL_ACCOUNT_GROUPS.Account_Group_Code,TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account,TSPL_GL_ACCOUNTS.Account_Code " + Environment.NewLine + _
'        " from TSPL_GL_ACCOUNTS" + Environment.NewLine + _
'        "inner join TSPL_ACCOUNT_MAIN_GL_ACCOUNT on TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account=TSPL_GL_ACCOUNTS.GL_Main_Code  " + Environment.NewLine + _
'        "inner join TSPL_ACCOUNT_SUB_GROUPS on TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code  " + Environment.NewLine + _
'        "inner join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code= TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code " + Environment.NewLine + _
'        "inner join  TSPL_ACCOUNT_MAIN_GROUPS on TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code=TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code " + Environment.NewLine + _
'        "inner join (select TSPL_GL_SEGMENT_CODE.Account_Code as AccCode from TSPL_GL_SEGMENT_CODE where TSPL_GL_SEGMENT_CODE.Seg_No='7' and len(isnull(TSPL_GL_SEGMENT_CODE.Account_Code,''))>0 ) as segTable  on segTable.AccCode=TSPL_GL_ACCOUNTS.Account_Code" + Environment.NewLine + _
'        "where 2=2"
'        If level = 1 Then
'            qry += " and TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code='" + strCode + "'"
'        ElseIf level = 2 Then
'            qry += " and TSPL_ACCOUNT_GROUPS.Account_Group_Code='" + strCode + "'"
'        ElseIf level = 3 Then
'            qry += " and TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code='" + strCode + "'"
'        ElseIf level = 4 Then
'            qry += " and TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account='" + strCode + "'"
'        ElseIf level = 5 Then
'            qry += " and TSPL_GL_ACCOUNTS.Account_Code='" + strCode + "'"
'        Else
'            Throw New Exception("Wrong Level it should be from 1-5")
'        End If
'        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
'        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
'            If Not clsCommon.CompairString(strNewValue, clsCommon.myCstr(dt.Rows(0)(level - 1))) = CompairStringResult.Equal Then
'                Throw New Exception("Sorry ! You cannot change Value [" + clsCommon.myCstr(dt.Rows(0)(level - 1)) + "] To [" + strNewValue + "] For " + strCode + Environment.NewLine + " Becuase account is used in GL Segment Account for Fiscal End year")
'            End If
'        End If
'        Return True
'    End Function

'    Public Shared Function CheckYearEndAccountFilledInSegment(ByVal strVoucherNo As String, ByVal trans As SqlTransaction) As Boolean
'        Dim qry As String = " select distinct TSPL_JOURNAL_DETAILS.Account_Seg_Code7 from TSPL_JOURNAL_DETAILS" + Environment.NewLine +
'        "TSPL_JOURNAL_DETAILS" + Environment.NewLine +
'        "left outer join (select TSPL_GL_SEGMENT_CODE.Account_Code as AccCode,TSPL_GL_SEGMENT_CODE.Segment_code as SegCode from TSPL_GL_SEGMENT_CODE where TSPL_GL_SEGMENT_CODE.Seg_No='7' and len(isnull(TSPL_GL_SEGMENT_CODE.Account_Code,''))>0 ) as segTable  on segTable.SegCode=TSPL_JOURNAL_DETAILS.Account_Seg_Code7" + Environment.NewLine +
'        "where TSPL_JOURNAL_DETAILS.Voucher_No='" + strVoucherNo + "' and len(isnull( segTable.AccCode,''))=0 "
'        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
'        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
'            Throw New Exception("First set Fiscal End year Account in GL Segment for Segment Code" + clsCommon.myCstr(dt.Rows(0)("Account_Seg_Code7")))
'        End If
'        Return True
'    End Function
'    Public Shared Function GetQueryGLAccountsUsedInAllAccountSet() As String
'        ''select Account_Code from TSPL_GL_ACCOUNTS where substring(Account_Code,0,10) in (SELECT substring(Payable_Account,0,10) as Control_Account from TSPL_VENDOR_ACCOUNT_SET)
'        Dim qry As String = ""
'        qry = "SELECT Payable_Account as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(Payable_Account,''))>0" & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Discount_Account as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(Discount_Account,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Advance_Account as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(Advance_Account,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT EXCHANGE_GAIN_ACCOUNT as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(EXCHANGE_GAIN_ACCOUNT,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT EXCHANGE_LOSS_ACCOUNT as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(EXCHANGE_LOSS_ACCOUNT,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Commission_ACCOUNT as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(Commission_ACCOUNT,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Incentive_ACCOUNT as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(Incentive_ACCOUNT,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT SECURITY_ACCOUNT as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(SECURITY_ACCOUNT,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Head_Load_ACCOUNT as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(Head_Load_ACCOUNT,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Own_Asset_ACCOUNT as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(Own_Asset_ACCOUNT,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Deduction_ACCOUNT as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(Deduction_ACCOUNT,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Advance_Against_Salary as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(Advance_Against_Salary,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Inv_Control_Account as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Inv_Control_Account,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Inv_Payable_Clearing as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Inv_Payable_Clearing,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Adjustment_Account as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Adjustment_Account,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Assembly_Cost_Credit as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Assembly_Cost_Credit,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Non_Stock_Clearing as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Non_Stock_Clearing,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Transfer_Clearing as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Transfer_Clearing,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Shipment_Clearing as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Shipment_Clearing,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Disassembly_Expense as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Disassembly_Expense,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Physical_Inv_Adjustment as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Physical_Inv_Adjustment,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Credit_Debit_Note_Clearing as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Credit_Debit_Note_Clearing,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Reserve_Stock as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Reserve_Stock,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Breakage_Gl_Account as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Breakage_Gl_Account,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT WIP_Account as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(WIP_Account,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT RM_Consumption as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(RM_Consumption,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Other_1 as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Other_1,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Other_2 as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Other_2,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Purchase_Control_Account as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Purchase_Control_Account,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Transfer_Gain_Loss_Ac as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Transfer_Gain_Loss_Ac,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Job_Work_Ac as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Job_Work_Ac,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Stock_Transfer_In as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Stock_Transfer_In,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Stock_Transfer_Acc as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Stock_Transfer_Acc,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Provision_Clearing as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Provision_Clearing,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Chilling_Charges as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Chilling_Charges,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Freight_Charges as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Freight_Charges,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Purchase_Account as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Purchase_Account,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Purchase_Set_Off as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Purchase_Set_Off,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Receivable_Control_acct as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Receivable_Control_acct,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Receipts_Discount_acct as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Receipts_Discount_acct,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Advance_acct as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Advance_acct,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Write_Offs as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Write_Offs,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Container_Deposit as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Container_Deposit,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT EXCHANGE_LOSS_ACCOUNT as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(EXCHANGE_LOSS_ACCOUNT,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT EXCHANGE_GAIN_ACCOUNT as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(EXCHANGE_GAIN_ACCOUNT,''))>0 " & Environment.NewLine &
'         " union" & Environment.NewLine &
'         " SELECT SECURITY_ACCOUNT as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(SECURITY_ACCOUNT,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT CREATE_SECURITY_ACCOUNT as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(CREATE_SECURITY_ACCOUNT,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT BANK_GUARANTEE as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(BANK_GUARANTEE,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT ACCOUNT1 as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(ACCOUNT1,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT ACCOUNT2 as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(ACCOUNT2,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT GSOC_Acct as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(GSOC_Acct,''))>0 " & Environment.NewLine &
'         " union  " & Environment.NewLine &
'         " SELECT Consignment_Acct as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Consignment_Acct,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Gain_Acct as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Gain_Acct,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Loss_Acct as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Loss_Acct,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Foreign_Bank_Charges_Account as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Foreign_Bank_Charges_Account,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Bank_Charges_Other_Account as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Bank_Charges_Other_Account,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Sales_Account as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Sales_Account,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Sales_Return_Account as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Sales_Return_Account,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Cost_Of_Goods_Sold as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Cost_Of_Goods_Sold,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Cost_Variance as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Cost_Variance,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT  Damaged_Goods as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Damaged_Goods,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Internal_Usage as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Internal_Usage,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Returnable_Container as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Returnable_Container,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Schemes as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Schemes,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Promotional as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Promotional,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Cogs_InterBranch as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Cogs_InterBranch,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Suspence_Account as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Suspence_Account,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Gain_Loss_Account as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Gain_Loss_Account,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT Stock_Transfer_AC as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Stock_Transfer_AC,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT COGT_AC as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(COGT_AC,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " SELECT DisplayPurpose_Account as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(DisplayPurpose_Account,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " select BANKACC as Control_Account FROM TSPL_BANK_MASTER where len(coalesce(BANKACC,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " select WRITEOFFACC as Control_Account FROM TSPL_BANK_MASTER where len(coalesce(WRITEOFFACC,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " select CREDITACC as Control_Account FROM TSPL_BANK_MASTER where len(coalesce(CREDITACC,''))>0 " & Environment.NewLine &
'         " union " & Environment.NewLine &
'         " select Transfer_Clearing_Account as Control_Account FROM TSPL_BANK_MASTER where len(coalesce(Transfer_Clearing_Account,''))>0"
'        qry = "select Account_Code as Control_Account from TSPL_GL_ACCOUNTS where substring(Account_Code,0,10) in (SELECT substring(Control_Account,0,10) as Control_Account from (" & qry & ") as Acc)"
'        Return qry
'    End Function
'    Public Shared Function GetGLAccountsUsedInAllAccountSet(ByVal trans As SqlTransaction) As ArrayList
'        Dim arr As New ArrayList
'        Dim qry As String = GetQueryGLAccountsUsedInAllAccountSet()
'        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
'        For Each dr As DataRow In dt.Rows
'            arr.Add(clsCommon.myCstr(dr.Item("Control_Account")))
'        Next
'        Return arr
'    End Function
'End Class