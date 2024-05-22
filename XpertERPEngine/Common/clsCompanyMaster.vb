Imports common
Imports System.Data.SqlClient

Public Class clsNumberValidate
    Public Shared Function FloatValidate(ByVal keyChar As Char) As Boolean
        If (keyChar >= Chr(48) And keyChar <= Chr(57)) Or keyChar = "." Or keyChar = Chr(8) Or keyChar = Chr(46) Then
            Return True
        Else
            Return False

        End If

    End Function
    Public Shared Function IntValidate(ByVal keyChar As Char) As Boolean
        If (keyChar >= Chr(48) And keyChar <= Chr(57)) Or keyChar = Chr(8) Or keyChar = Chr(46) Then
            Return True
        Else
            Return False

        End If

    End Function
    
End Class
Public Class MSSQLTOORACLE
    Shared isNewFiled As Boolean = False

    Private Shared Function findNextPunch(ByVal str As String, ByVal spos As Integer) As Integer
        Dim pos As Integer = -1
        If spos < 0 Or spos > Len(str) Then
            Return pos
        Else
            Dim s As String = ""
            For j As Integer = spos To Len(str)
                s = Microsoft.VisualBasic.Mid(str, j, 1)
                If s = "(" Or s = "," Or s = "[" Or s = "]" Or s = ")" Or s = "'" Or s = " " Then
                    pos = j
                    If s = "[" Or (s = "'" And isNewFiled = False) Then
                        isNewFiled = True
                    Else
                        isNewFiled = False
                    End If
                    Return pos
                End If
            Next
            Return pos
        End If
    End Function

    Public Shared Function Covert(ByVal sqry As String) As String
        Dim str As String = ""
        Dim s As String = ""
        Dim s1 As String = ""
        Dim pos1 As Integer = 1
        Dim pos2 As Integer = 1
        For i As Integer = 1 To Len(sqry)
            pos2 = findNextPunch(sqry, pos1)
            If pos2 <> -1 Then
                If isNewFiled Then
                    s = Microsoft.VisualBasic.Mid(sqry, pos1, pos2 - pos1)
                    pos1 = pos2 + 1
                    s1 = s1 & s & Microsoft.VisualBasic.Mid(sqry, pos2, 1) & vbCrLf
                Else
                    s = Microsoft.VisualBasic.Mid(sqry, pos1, pos2 - pos1)
                    pos1 = pos2 + 1
                    s1 = s1 & s & Microsoft.VisualBasic.Mid(sqry, pos2, 1) & vbCrLf
                End If

            Else
                s = Microsoft.VisualBasic.Mid(sqry, pos1, (Len(sqry) - pos1) + 1)
                s1 = s1 & s & vbCrLf
                Return s1
            End If
        Next
        str = s1
        Return str
    End Function
End Class
Public Class clsCustomerAccountSet
#Region "Varaibles"
    Public CustCode As String = ""
    Public CustName As String = ""
    Public CustGroup As String = ""
    Public AccountCode As String = ""
    Public AccountDesc As String = ""
    Public Account1 As String = ""
    Public Account1Desc As String = ""
    Public Account2 As String = ""
    Public Account2Desc As String = ""

    Public Advance As String = ""
    Public AdvanceDesc As String = ""
    Public BankOtherCharges As String = ""
    Public BankOtherChargesDesc As String = ""
    Public BankGuarantee As String = ""
    Public BankGuaranteeDesc As String = ""

    Public ConsignmentAcct As String = ""
    Public ContainerDeposit As String = ""
    Public ContainerDepositDesc As String = ""
    Public CrateSecurity As String = ""
    Public CrateSecurityDesc As String = ""
    Public ExchangeGain As String = ""
    Public ExchangeGainDesc As String = ""
    Public ExchangeLoss As String = ""
    Public ExchangeLossDesc As String = ""
    Public ForeignBankCharges As String = ""
    Public ForeignBankChargesDesc As String = ""

    Public GainAcct As String = ""
    Public GainAcctDesc As String = ""
    Public GOSC As String = ""
    Public GOSCDesc As String = ""
    Public LeakageDeduction As String = ""
    Public LeakageDeductionDesc As String = ""
    Public PenaltyCharges As String = ""
    Public PenaltyChargesDesc As String = ""
    Public Receipt As String = ""
    Public ReceiptDesc As String = ""
    Public DebtorControl As String = ""
    Public DebtorControlDesc As String = ""
    Public Security As String = ""
    Public SecurityDesc As String = ""
    Public Subsidy As String = ""
    Public SubsidyDesc As String = ""
    Public Writeoffs As String = ""
    Public WriteoffsDesc As String = ""
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Cust_Account as [Code],Cust_Acct_Desc as [Customer Account Description],Receivable_Control_acct as [Receivable Control Account],Receipts_Discount_acct as [Receipt Discount Account],Advance_acct as [Advance Account],Write_Offs as [Write Offs],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],Container_Deposit as [Container Deposit],CURRENCY_CODE as [Currency Code],EXCHANGE_LOSS_ACCOUNT as [Exchange Loss Account],EXCHANGE_GAIN_ACCOUNT as [Exchange Gain Account] from TSPL_CUSTOMER_ACCOUNT_SET   "
        str = clsCommon.ShowSelectForm("CUSTACSET", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'

    Public Shared Function UpdateData(ByVal Arr As List(Of clsCustomerAccountSet)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsCustomerAccountSet In Arr
                    If clsCommon.myLen(obj.CustCode) > 0 Then
                        Dim qry As String = ""
                        If clsCommon.myLen(obj.Account1) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set Account1='" & obj.Account1 & "'" & _
                                 " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.Account2) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set Account2='" & obj.Account2 & "'" & _
                                        " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.Advance) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set Advance_acct='" & obj.Advance & "'" & _
                                        " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.BankGuarantee) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set BANK_GUARANTEE='" & obj.BankGuarantee & "'" & _
                                        " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.BankOtherCharges) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set Bank_Charges_Other_Account='" & obj.BankOtherCharges & "'" & _
                                        " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.ContainerDeposit) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set Container_Deposit='" & obj.ContainerDeposit & "'" & _
                                        " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.ConsignmentAcct) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set Consignment_Acct='" & obj.ConsignmentAcct & "'" & _
                                        " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.CrateSecurity) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set Consignment_Acct='" & obj.CrateSecurity & "'" & _
                                        " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.DebtorControl) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set Receivable_Control_acct='" & obj.DebtorControl & "'" & _
                                        " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.ExchangeGain) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set EXCHANGE_GAIN_ACCOUNT='" & obj.ExchangeGain & "'" & _
                                        " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.ExchangeLoss) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set EXCHANGE_LOSS_ACCOUNT='" & obj.ExchangeLoss & "'" & _
                                        " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.ForeignBankCharges) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set Foreign_Bank_Charges_Account='" & obj.ForeignBankCharges & "'" & _
                                        " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.GainAcct) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set Gain_Acct='" & obj.GainAcct & "'" & _
                                        " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
              
                        If clsCommon.myLen(obj.GOSC) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set GSOC_Acct='" & obj.GOSC & "'" & _
                                        " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.LeakageDeduction) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set Leakage_Deduction='" & obj.LeakageDeduction & "'" & _
                                        " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.PenaltyCharges) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set Penalty_Charges_Account='" & obj.PenaltyCharges & "'" & _
                                        " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.Receipt) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set Receipts_Discount_acct='" & obj.Receipt & "'" & _
                                        " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.Security) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set SECURITY_ACCOUNT='" & obj.Security & "'" & _
                                        " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.Subsidy) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set SubSidy_Account='" & obj.Subsidy & "'" & _
                                        " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.Writeoffs) > 0 Then
                            qry = "update TSPL_CUSTOMER_ACCOUNT_SET set Write_Offs='" & obj.Writeoffs & "'" & _
                                        " where Cust_Account='" & obj.AccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                       
                    End If
                Next
            End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
            Return False
        End Try
        Return True
    End Function

End Class
Public Class clsGLSourceCode
#Region "Variables"
    Public SourceCode As String = Nothing
    Public SourceLedger As String = Nothing
    Public SourceType As String = Nothing
    Public SourceDescription As String = Nothing
    Public TallyName As String = Nothing
    Public CustomerArrTr As List(Of clsCustomerAccountSet) = Nothing

#End Region

    Public Function CustomerAccountUpdate(ByVal obj As clsGLSourceCode) As Boolean
        Dim isSaved As Boolean = True
        isSaved = clsCustomerAccountSet.UpdateData(obj.CustomerArrTr)
        Return isSaved
    End Function

    Public Function SaveData(ByVal obj As clsGLSourceCode, ByVal isNewEntry As Boolean) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "SourceLedger", obj.SourceLedger)
            clsCommon.AddColumnsForChange(coll, "SourceType", obj.SourceType)
            clsCommon.AddColumnsForChange(coll, "SourceDescription", obj.SourceDescription)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "SourceCode", obj.SourceCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_SOURCECODE", OMInsertOrUpdate.Insert, "")
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_SOURCECODE", OMInsertOrUpdate.Update, "SourceCode='" + obj.SourceCode + "'")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select SourceCode as [Code] ,SourceLedger as [Source Ledger] ,SourceType as [Source Type] ,SourceDescription as [Source Description] ,Created_By as [Created By] ,Created_Date as [Created Date] ,Modify_By as [Modify By] ,Modify_Date as [Modify Date] ,Comp_Code as [Company Code] ,TallyName as [Tally Name]  from TSPL_GL_SOURCECODE   "
        str = clsCommon.ShowSelectForm("GLSRCCDFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'

    Public Shared Function SaveMergeSourceCode(ByVal arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "update TSPL_GL_SOURCECODE set Do_Not_Merge =0"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            qry = "update TSPL_GL_SOURCECODE set Do_Not_Merge =1 where SourceCode in (" + clsCommon.GetMulcallString(arr) + ")"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        End If
        Return True
    End Function


    Public Shared Function GetMergeSourceCode() As ArrayList
        Dim qry As String = "select SourceCode from TSPL_GL_SOURCECODE where Do_Not_Merge=1"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim arr As ArrayList = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New ArrayList
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr("SourceCode")))
            Next
        End If
        Return arr
    End Function
End Class
Public Class clsproductionItemCategory
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_MF_PRODUCTION_ITEM_CATEGORY.PROD_ITEM_CATEGORY_CODE as [Code] ,TSPL_MF_PRODUCTION_ITEM_CATEGORY.PROD_ITEM_CATEGORY_NAME as [Item Category Name] ,TSPL_MF_PRODUCTION_ITEM_CATEGORY.DESCRIPTION as [Description] ,TSPL_MF_PRODUCTION_ITEM_CATEGORY.Created_By as [Created By] ,TSPL_MF_PRODUCTION_ITEM_CATEGORY.Created_Date as [Created Date] ,TSPL_MF_PRODUCTION_ITEM_CATEGORY.Modified_By as [Modified By] ,TSPL_MF_PRODUCTION_ITEM_CATEGORY.Modified_Date as [Modified Date] ,TSPL_MF_PRODUCTION_ITEM_CATEGORY.ITEM_GROUP as [Item Group]  From TSPL_MF_PRODUCTION_ITEM_CATEGORY   "
        str = clsCommon.ShowSelectForm("MFITMCAT", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class

Public Class clsToolType
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_MF_tool_type.TOOL_TYPE_CODE as [Code] ,TSPL_MF_tool_type.DESCRIPTION as [Description] ,TSPL_MF_tool_type.STATUS as [Status] ,TSPL_MF_tool_type.INACTIVE_DATE as [Inactive Date] ,TSPL_MF_tool_type.UNIT_CODE as [Unit Code] ,TSPL_MF_tool_type.COST_PER_UNIT as [Cost Per Unit] ,TSPL_MF_tool_type.COMMENTS as [Comments] ,TSPL_MF_tool_type.Created_By as [Created By] ,TSPL_MF_tool_type.Created_Date as [Created Date] ,TSPL_MF_tool_type.Modified_By as [Modified By] ,TSPL_MF_tool_type.Modified_Date as [Modified Date]  From TSPL_MF_tool_type   "
        str = clsCommon.ShowSelectForm("TOOLTYPEFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class

Public Class clsAccountSetting
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_MF_ACCOUNTSETS.ACCOUNT_SET_CODE as [Code] ,TSPL_MF_ACCOUNTSETS.DESCRIPTION as [Description] ,TSPL_MF_ACCOUNTSETS.WIP_CATEGORY as [Wip Category] ,TSPL_MF_ACCOUNTSETS.GL_WIP as [GL Wip] ,TSPL_MF_ACCOUNTSETS.GL_SETUP_LABOR as [GL Setup Labor] ,TSPL_MF_ACCOUNTSETS.GL_RUN_LABOR as [GL Run Labor] ,TSPL_MF_ACCOUNTSETS.GL_SUBCONTRACT as [GL Subcontract] ,TSPL_MF_ACCOUNTSETS.GL_OVERHEAD as [GL Overhead] ,TSPL_MF_ACCOUNTSETS.GL_MATERIAL_VARIANCE as [GL Material Variance] ,TSPL_MF_ACCOUNTSETS.GL_PRODUCTION_VARIANCE as [GL Production Variance] ,TSPL_MF_ACCOUNTSETS.Created_By as [Created By] ,TSPL_MF_ACCOUNTSETS.Created_Date as [Created Date] ,TSPL_MF_ACCOUNTSETS.Modified_By as [Modified By] ,TSPL_MF_ACCOUNTSETS.Modified_Date as [Modified Date]  From TSPL_MF_ACCOUNTSETS   "
        str = clsCommon.ShowSelectForm("ACCSETFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class

Public Class clsToolMaster
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_MF_TOOL_MASTER.TOOL_CODE as [Code] ,TSPL_MF_TOOL_MASTER.DESCRIPTION as [Description] ,TSPL_MF_TOOL_MASTER.STATUS as [Status] ,TSPL_MF_TOOL_MASTER.INACTIVE_DATE as [Inactive Date] ,TSPL_MF_TOOL_MASTER.TOOL_TYPE_CODE as [Tool Type Code] ,TSPL_MF_TOOL_MASTER.RECEIPT_DATE as [Receipt Date] ,TSPL_MF_TOOL_MASTER.RECEIVED_BY as [Received By] ,TSPL_MF_TOOL_MASTER.SUPPLIER as [Supplier] ,TSPL_MF_TOOL_MASTER.RECEIPT_NUMBER as [Receipt Number] ,TSPL_MF_TOOL_MASTER.PO_NUMBER as [PO Number] ,TSPL_MF_TOOL_MASTER.SERIAL_NUMBER as [Serial Number] ,TSPL_MF_TOOL_MASTER.CUSTODIAN as [Custodian] ,TSPL_MF_TOOL_MASTER.REPLACEMENT_DATE as [Replacement Date] ,TSPL_MF_TOOL_MASTER.COMMENTS as [Comments] ,TSPL_MF_TOOL_MASTER.COST_PER_UNIT as [Cost Per Unit] ,TSPL_MF_TOOL_MASTER.ORIGINAL_QUANTITY as [Original Quantity] ,TSPL_MF_TOOL_MASTER.CONSUMED as [Consumed] ,TSPL_MF_TOOL_MASTER.ON_HAND_QUANTITY as [On Hand Quantity] ,TSPL_MF_TOOL_MASTER.ON_HAND_COST as [On Hand Cost] ,TSPL_MF_TOOL_MASTER.UNIT_CODE as [Unit Code] ,TSPL_MF_TOOL_MASTER.Created_By as [Created By] ,TSPL_MF_TOOL_MASTER.Created_Date as [Created Date] ,TSPL_MF_TOOL_MASTER.Modified_By as [Modified By] ,TSPL_MF_TOOL_MASTER.Modified_Date as [Modified Date] ,TSPL_MF_TOOL_MASTER.MAINTAINED_BY as [Maintained By]  From TSPL_MF_TOOL_MASTER   "
        str = clsCommon.ShowSelectForm("TOOLMSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class

''Sanjeet(02/02/2017)============
Public Class clsPasswordCheckForMasters
    Public Shared Function CheckMasterPwd(ByVal StrFormId As String, ByVal strCode As String) As Boolean
        Try
            ''check setting first, "MasterModificationWithSecurity", if yes the below code procced
            
            If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowMasterModificationWithSecurity, clsFixedParameterCode.AllowMasterModificationWithSecurity, Nothing))) Then
                Dim strRemarks = Nothing
                Dim frm As New FrmPWD(Nothing)
                frm.strType = clsFixedParameterType.AllowToSaveAndUpdatePasswordBased
                frm.strCode = clsFixedParameterCode.AllowToSaveAndUpdatePasswordBased
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    Dim frm1 As New FrmFreeTxtBox1
                    frm1.Text = "Remarks for Save/Update"
                    frm1.ShowDialog()
                    If clsCommon.myLen(frm1.strRmks) <= 0 Then
                        clsCommon.MyMessageBoxShow("Please enter remarks.")
                        Return False
                    Else
                        strRemarks = frm1.strRmks
                        clsSaveUpdateMasterPwdBased.SaveData(StrFormId, strCode, strRemarks)
                        Return True
                    End If
                Else
                    Return False
                End If
            Else
                Return True
            End If
           

            ''otheriwse when no setting on then Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function
End Class
'==================

Public Class clsGLStructure
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Str_Code as [Code] ,Str_Description as [Description] ,Default_Structure as [Default Structure] ,Seg_No1 as [Segment No1] ,Seg_Name1 as [Segment Name1] ,Seg_length1 as [Segment Length1] ,Seg_No2 as [Segment No2] ,Seg_Name2 as [Segment Name2] ,Seg_length2 as [Segment Length2] ,Seg_No3 as [Segment No3] ,Seg_Name3 as [Segment Name3] ,Seg_length3 as [Segment Length3] ,Seg_No4 as [Segment No4] ,Seg_Name4 as [Segment Name4] ,Seg_length4 as [Segment Length4] ,Seg_No5 as [Segment No5] ,Seg_Name5 as [Segment Name5] ,Seg_length5 as [Segment Length5] ,Seg_No6 as [Segment No6] ,Seg_Name6 as [Segment Name6] ,Seg_length6 as [Segment Length6] ,Seg_No7 as [Segment No7] ,Seg_Name7 as [Segment Name7] ,Seg_length7 as [Segment Length7] ,Seg_No8 as [Segment No8] ,Seg_Name8 as [Segment Name8] ,Seg_length8 as [Segment Length8] ,Seg_No9 as [Segment No9] ,Seg_Name9 as [Segment Name9] ,Seg_length9 as [Segment Length9] ,Seg_No10 as [Segment No10] ,Seg_Name10 as [Segment Name10] ,Seg_length10 as [Segment Length10] ,Created_By as [Created By] ,Created_Date as [Created Date] ,Modify_By as [Modify By] ,Modify_Date as [Modify Date] ,Comp_Code as [Company Code]  from TSPL_GL_STRUCTURE   "
        str = clsCommon.ShowSelectForm("GLSTRUFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsRouteTypeMaster

#Region "variables"
    Public Route_Type_Id As String = ""
    Public Route_Type_Desc As String = ""
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As Date? = Nothing
    Public Comp_Code As String = ""
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Route_Type_Id as [Code] ,Route_Type_Desc as [Route Type Description] ,Created_By as [Created By] ,Created_Date as [Created Date] ,Modify_By as [Modify By] ,Modify_Date as [Modify Date] ,Comp_Code as [Company Code]  From TSPL_ROUTE_TYPE   "
        str = clsCommon.ShowSelectForm("RTTYPEFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------Save Data ------------------------------------------------------------------------------'
    Public Function save_data(ByVal obj As clsRouteTypeMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            If SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsRouteTypeMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Route_Type_Id", obj.Route_Type_Id)
            clsCommon.AddColumnsForChange(coll, "Route_Type_Desc", obj.Route_Type_Desc)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ROUTE_TYPE", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ROUTE_TYPE", OMInsertOrUpdate.Update, "Route_Type_Id='" + obj.Route_Type_Id + "' ", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    ''
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsRouteMaster
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Route_No as [Code] ,Route_Desc as [Route Description] ,Type as [Type] ,Employee_Code as [Employee Code] ,Off_Day as [Off Day] ,City_Code as [City Code] ,District as [District] ,Category_Code as [Category Code] ,Length as [Length] ,Employee_Name as [Employee Name] ,Depot_Id as [Depot Id] ,Price_Code as [Price Code] ,Price_Code_Desc as [Price Code Description] ,Created_By as [Created By] ,Created_Date as [Created Date] ,Modify_By as [Modify By] ,Modify_Date as [Modify Date] ,Comp_Code as [Company Code] ,vehicle_code as [Vehicle Code] ,NonPrice_Code as [Non Price Code] ,Status as [Status] ,SDate as [Start Date] ,RoutePrice_Code as [Route Price Code]  From TSPL_Route_Master   "
        str = clsCommon.ShowSelectForm("RTMSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function GetName(ByVal strRoute As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select Route_Desc from TSPL_Route_Master where Route_No='" + strRoute + "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsResponsiblePerson
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_TDS_RESP_PERSON.Person_Code as [Code] ,TSPL_TDS_RESP_PERSON.Person_Name as [Person Name] ,TSPL_TDS_RESP_PERSON.Father_Name as [Father Name] ,TSPL_TDS_RESP_PERSON.Designation as [Designation] ,TSPL_TDS_RESP_PERSON.Address1 as [Address1] ,TSPL_TDS_RESP_PERSON.Address2 as [Address2] ,TSPL_TDS_RESP_PERSON.City as [City] ,TSPL_TDS_RESP_PERSON.Branch_Code as [Branch Code] ,TSPL_TDS_RESP_PERSON.State_Code as [State Code] ,TSPL_TDS_RESP_PERSON.Country as [Country] ,TSPL_TDS_RESP_PERSON.Pincode as [Pin Code] ,TSPL_TDS_RESP_PERSON.Telephone as [Telephone] ,TSPL_TDS_RESP_PERSON.Fax as [Fax] ,TSPL_TDS_RESP_PERSON.Email as [Email] ,TSPL_TDS_RESP_PERSON.Signature as [Signature] ,TSPL_TDS_RESP_PERSON.Active as [Active] ,TSPL_TDS_RESP_PERSON.Created_By as [Created By] ,TSPL_TDS_RESP_PERSON.Created_Date as [Created Date] ,TSPL_TDS_RESP_PERSON.Modify_By as [Modify By] ,TSPL_TDS_RESP_PERSON.Modify_Date as [Modify Date] ,TSPL_TDS_RESP_PERSON.Comp_Code as [Company Code]  From TSPL_TDS_RESP_PERSON   "
        str = clsCommon.ShowSelectForm("RESPERFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsItemStructureMaster
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Structure_Code as [Code] ,Structure_Descq as [Structure Description] ,Item_Structure as [Item Structure] ,Total_Length as [Total Length] ,Default_Struct as [Default Structure] ,Created_By as [Created By] ,Create_Date as [Create Date] ,Modify_By as [Modify By] ,Modify_Date as [Modify Date] ,Comp_Code as [Company Code]  From TSPL_STRUCTURE_MASTER   "
        str = clsCommon.ShowSelectForm("ITMSTRMST", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Structure_Descq from TSPL_STRUCTURE_MASTER where Structure_Code='" + strCode + "'"))
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsPurchaseAccountSet
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Purchase_Class_Code as [Code] ,Purchase_Class_Desc as [Purchase Class Description] ,Inv_Control_Account as [Inventory Control Account] ,Inv_Payable_Clearing as [Inventory Payable Clearing] ,Adjustment_Account as [Adjustment Account] ,Assembly_Cost_Credit as [Assembly Cost Credit] ,Non_Stock_Clearing as [Non Stock Clearing] ,Transfer_Clearing as [Transfer Clearing] ,Shipment_Clearing as [Shipment Clearing] ,Disassembly_Expense as [Disassembly Expense] ,Physical_Inv_Adjustment as [Physical Inventory Adjustment] ,Credit_Debit_Note_Clearing as [Credit Debit Note Clearing] ,Reserve_Stock as [Reserve Stock] ,Created_By as [Created By] ,Created_Date as [Created Date] ,Modify_By as [Modify By] ,Modify_Date as [Modify Date] ,Comp_Code as [Company Code] ,Breakage_Gl_Account as [Breakage GL Account] ,Costing_Method as [Costing Method]  From tspl_purchase_accounts   "
        str = clsCommon.ShowSelectForm("PURACSETFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsSalesAccountSet
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Sales_Class_Code as [Code] ,Sales_Class_Desc as [Sales Class Description] ,Sales_Account as [Sales Account] ,Sales_Return_Account as [Sales Return Account] ,Cost_Of_Goods_Sold as [Cost Of Goods Sold] ,Cost_Variance as [Cost Variance] ,Damaged_Goods as [Damaged Goods] ,Internal_Usage as [Internal Usage] ,Returnable_Container as [Returnable Container] ,Created_By as [Created By] ,Created_Date as [Created Date] ,Modify_By as [Modify By] ,Modify_Date as [Modify Date] ,Comp_Code as [Company Code] ,Schemes as [Schemes] ,Promotional as [Promotional] ,Cogs_InterBranch as [Cogs Inter Branch] ,Suspence_Account as [Suspence Account]  From tspl_sales_accounts  "
        str = clsCommon.ShowSelectForm("SALEACSET", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsAccountGroups
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Account_Group_Code as [Code] ,Account_Group_Desc as [Account Group Description] ,TSPL_ACCOUNT_GROUPS.Comp_Code as [Company Code] ,TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code as [Main Group Code],TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Desc as [Account Main Group Description],TSPL_ACCOUNT_MAIN_GROUPS.Group_Type as [Group Type] ,TSPL_ACCOUNT_GROUPS.Created_By as [Created By] ,TSPL_ACCOUNT_GROUPS.Created_Date as [Created Date] ,TSPL_ACCOUNT_GROUPS.Modify_By as [Modify By] ,TSPL_ACCOUNT_GROUPS.Modify_Date as [Modify Date]   from TSPL_ACCOUNT_GROUPS left outer join  TSPL_ACCOUNT_MAIN_GROUPS on TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code=TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code"
        str = clsCommon.ShowSelectForm("ACGRPFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsVendorAccountSet
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Acct_Set_Code as [Code],Acct_Set_Desc as [Account Set Description],Payable_Account as [Payable Account],Discount_Account as [Discount Account],Advance_Account as [Advance Account],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],CURRENCY_CODE as [Currency Code],EXCHANGE_LOSS_ACCOUNT as [Exchange Loss Account],EXCHANGE_GAIN_ACCOUNT as [Exchange Gain Account] from TSPL_VENDOR_ACCOUNT_SET    "
        str = clsCommon.ShowSelectForm("VENDACSET", qry, "Code", whrcls, curcode, "Code", isButtonClicked, "TSPL_VENDOR_ACCOUNT_SET.Created_Date")
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsCustomerCategory
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select CUST_CATEGORY_CODE as [Code],CUST_CATEGORY_DESC as [Customer Category Description],Price_Code as [Price Code],Price_Code_Desc as [Price Code Description],Route_No as [Route No],Route_Desc as [Route Description],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],Shelf_Life as [Shelf Life],price_CodeNon as [Price Code Non],Price_Code_DescNon as [Price Code Description Non] from tspl_Customer_Category_Master   "
        str = clsCommon.ShowSelectForm("CUSTCATFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsCustomerGroupMaster
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Cust_Group_Code as [Code],Cust_Group_Desc as [Customer Group Description],Tax_Group as [Tax Group],Cust_Account as [Customer Account],Terms_Code as [Terms Code],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],Shelf_Life as [Shelf Life],CASE WHEN ShowGroupOnReport=1 THEN 'Y' ELSE 'N' END AS [Show Group On Report] from TSPL_CUSTOMER_GROUP_MASTER   "
        str = clsCommon.ShowSelectForm("CUSTGRPMST", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsVendorGroupMaster
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Ven_Group_Code as [Code] ,Group_Desc as [Vendor Group Description] ,Acct_Set_Code as [Account Set Code] ,Acct_Set_Desc as [Account Set Description] ,Terms_Code as [Terms code] ,Terms_Desc as [Terms Description] ,Payment_Code as [Payment Code] ,Payment_Desc as [Payment Description] ,Bank_Code as [Bank Code] ,Description as [Description] ,Tax_Group_Code as [Tax Group Code] ,Tax_Group_Desc as [Tax Group Description] ,Created_By as [Created By] ,Created_Date as [Created Date] ,Modify_By as [Modify By] ,Modify_Date as [Modify Date] ,Comp_Code as [Company Code]  from TSPL_VENDOR_GROUP   "
        str = clsCommon.ShowSelectForm("VENDGRPMST", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsCompanyMaster
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Comp_Code as [Code],Comp_Name as [Company Name],Add1,Add2,Add3,City_Code as [City Code],Phone1,Phone2,Fax,Email,Pincode,State,Tin_No as [Tin No],CST_LST as [CST LST],Regn_No as [Registration No],Cform as [C Form],Mode_of_Trans as [Mode Of Trans],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code1 as [Company Code1],DataBase_Name as [Database Name],Vat_Reg_No as [VAT Registration No],ServiceTax_Reg_No as [Service Tax Registration No],Ecc_No as [ECC No],CE_Range as [CE Range],CE_Commissionerate as [CE Commission Rate],CE_Division as [CE Division],Pan_No as [PAN No],Tan_No as [TAN NO],Tcan_No as [TCAN No],Circle_No as [Circle No],Ward_No as [Ward No],Access_Officer as [Access Officer],NameInTally as [Name In Tally],IntegrateWithTally as [Integrate With Tally],BaseCurrencyCode as [Base Currency Code],ApplyMultiCurrency  as [Apply Multi Currency],GSTReg_No as [GST Regisrtation No],GSTINNo as [GSTIn No] from TSPL_COMPANY_MASTER   "
        str = clsCommon.ShowSelectForm("RPTCPMSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function CheckExistence(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select COMP_CODE from TSPL_COMPANY_MASTER where COMP_CODE='" & strCode & "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function UpdateInventorySummarySetting(ByVal UpdateInvSummary As String, ByVal FatSNFControl As String, ByVal CheckBalanceFromInvSummary As String, ByVal ItemwiseFatSNFControl As String) As Boolean
        Dim trans As SqlTransaction = Nothing
        Dim qry As String = ""
        Try
            trans = clsDBFuncationality.GetTransactin()
            '' check that summary is already updated
            qry = "select count(*) from TSPL_INV_MOVE_DL"
            Dim tot As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            If tot > 0 Then
                Throw New Exception("Inventory Summary is already Updated")
            End If
            If clsCommon.CompairString(UpdateInvSummary, "1") = CompairStringResult.Equal Then
                clsInventoryMovement.InsertIntoGITTable(True, Nothing, Nothing, trans)
                clsInventoryMovement.UpdateInvSummaryData("", trans)
                clsFixedParameter.UpdateData(clsFixedParameterType.UpdateInventorySummaryTable, clsFixedParameterCode.UpdateInventorySummaryTable, UpdateInvSummary, trans)
                If clsCommon.CompairString(FatSNFControl, "1") = CompairStringResult.Equal Then
                    clsFixedParameter.UpdateData(clsFixedParameterType.FatSNFStockControl, clsFixedParameterCode.FatSNFStockControl, FatSNFControl, trans)
                End If
                If clsCommon.CompairString(CheckBalanceFromInvSummary, "1") = CompairStringResult.Equal Then
                    clsFixedParameter.UpdateData(clsFixedParameterType.CheckBalanceFromInvMoveSummry, clsFixedParameterCode.CheckBalanceFromInvMoveSummry, CheckBalanceFromInvSummary, trans)
                End If
                If clsCommon.CompairString(ItemwiseFatSNFControl, "1") = CompairStringResult.Equal Then
                    clsFixedParameter.UpdateData(clsFixedParameterType.ItemwiseFatSNFStockControl, clsFixedParameterCode.ItemwiseFatSNFStockControl, ItemwiseFatSNFControl, trans)
                End If
            Else
                clsFixedParameter.UpdateData(clsFixedParameterType.UpdateInventorySummaryTable, clsFixedParameterCode.UpdateInventorySummaryTable, "0", trans)
                clsFixedParameter.UpdateData(clsFixedParameterType.FatSNFStockControl, clsFixedParameterCode.FatSNFStockControl, "0", trans)
                clsFixedParameter.UpdateData(clsFixedParameterType.CheckBalanceFromInvMoveSummry, clsFixedParameterCode.CheckBalanceFromInvMoveSummry, "0", trans)
                clsFixedParameter.UpdateData(clsFixedParameterType.ItemwiseFatSNFStockControl, clsFixedParameterCode.ItemwiseFatSNFStockControl, "0", trans)
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsFormMaster
#Region "veriables"
    Public Form_Name As String
    Public Form_Type As String
    Public Remarks As String
    Public Modified_By As String
    Public Modified_Date As String
    Public Comp_Code As String
    Public Form_Code As String
    Public Created_By As String
    Public Created_Date As String
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Form_code as [Code],Form_name as [Form Name],Remarks,Created_By as [Created By],Created_Date as [Created Date],Modified_By as [Modify By],Modified_Date as [Modify Date],Comp_Code as [Company Code],Form_Type as [Form Type]  from TSPL_Form_Master    "
        str = clsCommon.ShowSelectForm("FRMMSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Function save_data(ByVal obj As clsFormMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            If SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetFormTypes() As DataTable
        Try
            Dim dt As DataTable = New DataTable()
            dt.Columns.Add("Code", GetType(String))
            dt.Columns.Add("Name", GetType(String))
            dt.Rows.Add("C", "C")
            dt.Rows.Add("F", "F")
            dt.Rows.Add("IN", "38-Inward")
            dt.Rows.Add("OU", "38-Outward")
            dt.Rows.Add("E", "E")
            dt.Rows.Add("D", "D")
            dt.Rows.Add("E1", "E1")
            dt.Rows.Add("H", "H")
            dt.Rows.Add("OH", "Others")
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function SaveData(ByVal obj As clsFormMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Form_Name", obj.Form_Name)
            clsCommon.AddColumnsForChange(coll, "Form_Type", obj.Form_Type)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Form_Code", obj.Form_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Form_Master", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Form_Master", OMInsertOrUpdate.Update, "Form_Code='" + obj.Form_Code + "' ", trans)
            End If
            'If isSaved Then
            '    trans.Commit()
            'End If
        Catch err As Exception
            'trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsGLSegmentCode
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Seg_No [Segment No],Segment_name as [Segment Name],Segment_code as [Code],Description ,Account_Code as [Account Code],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code]  from TSPL_GL_SEGMENT_CODE     "
        str = clsCommon.ShowSelectForm("GLSGMNTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class

Public Class clsBankMaster
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        '' Richa Againt Ticket No. BM00000003641 on 27/08/2014 add FDPercentage,LCCreditLimit
        Dim str As String = ""
        Dim qry As String = " select BANK_CODE as [Code],DESCRIPTION as [Description],ADD1 as [Add1],ADD2 as [Add2],ADD3 as [Add3],ADD4 as [Add4],CITY as [City],STATE as [State],POSTAL as [Postal],COUNTRY as [Country],CONTACT as [Contact],PHONE as [Phone],FAX as [Fax],INACTIVE as [Inactive],BANKACCNUMBER as [Bank Account Number],BANKACC as [Bank Account],WRITEOFFACC as [Write Off Account],CREDITACC as [Credit Account],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],Bank_type as [Bank Type],Cheque_Validity_In_Days as [Cheque Validity In Days],LCCreditLimit as [LC Credit Limit], FDPercentage as [FD Percentage]   from TSPL_Bank_Master    "
        str = clsCommon.ShowSelectForm("BNKMSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '==============Added By preeti gupta[4/5/2016]==========
    Public Shared Function getDefaultBankFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String

        Dim str As String = ""
        Dim qry As String = " select BANK_CODE as [Code],DESCRIPTION as [Description],ADD1 as [Add1],ADD2 as [Add2],ADD3 as [Add3],ADD4 as [Add4],CITY as [City],STATE as [State],POSTAL as [Postal],COUNTRY as [Country],CONTACT as [Contact],PHONE as [Phone],FAX as [Fax],INACTIVE as [Inactive],BANKACCNUMBER as [Bank Account Number],BANKACC as [Bank Account],WRITEOFFACC as [Write Off Account],CREDITACC as [Credit Account],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],Bank_type as [Bank Type],Cheque_Validity_In_Days as [Cheque Validity In Days],LCCreditLimit as [LC Credit Limit], FDPercentage as [FD Percentage]   from TSPL_Bank_Master where Default_Bank=1   "
        str = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return str
    End Function
    '============================================

    Public Shared Function GetBankType(ByVal Bank_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String        
        Dim qry As String = " select Bank_Type from TSPL_Bank_Master  where Bank_Code='" & Bank_Code & "'  "
        Dim Bank_Type As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Return Bank_Type
    End Function
    Public Shared Function GetMainBank(ByVal Bank_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = " select Main_Bank_Code from TSPL_Bank_Master  where Bank_Code='" & Bank_Code & "'  "
        Dim Main_Bank_Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Return Main_Bank_Code
    End Function
    Public Shared Function GetName(ByVal Bank_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = " select Description from TSPL_Bank_Master  where Bank_Code='" & Bank_Code & "'  "
        Dim Description As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Return Description
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class

Public Class clsNatureOfDeduction
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_TDS_DEDUCTION_HEAD.Deduction_Code as [Code] ,TSPL_TDS_DEDUCTION_HEAD.Description as [Description] ,TSPL_TDS_DEDUCTION_HEAD.TDS_Section as [TDS Section] ,TSPL_TDS_DEDUCTION_HEAD.Cumm_Cutoff as [Cumm Cutoff] ,TSPL_TDS_DEDUCTION_HEAD.Percent_Amount as [Percent Amount] ,TSPL_TDS_DEDUCTION_HEAD.Inactive as [Inactive] ,TSPL_TDS_DEDUCTION_HEAD.Comment as [Comment] ,TSPL_TDS_DEDUCTION_HEAD.Created_By as [Created By] ,TSPL_TDS_DEDUCTION_HEAD.Created_Date as [Created Date] ,TSPL_TDS_DEDUCTION_HEAD.Modify_By as [Modify By] ,TSPL_TDS_DEDUCTION_HEAD.Modify_Date as [Modify Date] ,TSPL_TDS_DEDUCTION_HEAD.Comp_Code as [Company Code] ,TSPL_TDS_DEDUCTION_HEAD.Gl_Account as [GL Account]  From TSPL_TDS_DEDUCTION_HEAD    "
        str = clsCommon.ShowSelectForm("NATOFDEDC", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsStateCode
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_TDS_STATE_MASTER.State_Code as [Code] ,TSPL_TDS_STATE_MASTER.State_Name as [State Name] ,TSPL_TDS_STATE_MASTER.Created_By as [Created By] ,TSPL_TDS_STATE_MASTER.Created_Date as [Created Date] ,TSPL_TDS_STATE_MASTER.Modify_By as [Modify By] ,TSPL_TDS_STATE_MASTER.Modify_Date as [Modify Date] ,TSPL_TDS_STATE_MASTER.Comp_Code as [Company Code]  From TSPL_TDS_STATE_MASTER    "
        str = clsCommon.ShowSelectForm("STCODEFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsBranchDetails
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_TDS_BRANCH_MASTER.Branch_Code as [Code] ,TSPL_TDS_BRANCH_MASTER.Branch_Name as [Branch Name] ,TSPL_TDS_BRANCH_MASTER.Tax_Account as [Tax Account] ,TSPL_TDS_BRANCH_MASTER.TaxAcct_Description as [Tax Account Description] ,TSPL_TDS_BRANCH_MASTER.Interest_Account as [Interest Account] ,TSPL_TDS_BRANCH_MASTER.Interest_Acc_Desc as [Interest Account Description] ,TSPL_TDS_BRANCH_MASTER.Others_Account as [Others Account] ,TSPL_TDS_BRANCH_MASTER.Other_Acct_Desc as [Other Account Description] ,TSPL_TDS_BRANCH_MASTER.Penalty_Account as [Penality Account] ,TSPL_TDS_BRANCH_MASTER.Penalty_Acct_Desc as [Penality Acct Description] ,TSPL_TDS_BRANCH_MASTER.Circle_Code as [Circle Code] ,TSPL_TDS_BRANCH_MASTER.Bank_Code as [Bank Code] ,TSPL_TDS_BRANCH_MASTER.Bank_Name as [Bank Name] ,TSPL_TDS_BRANCH_MASTER.Remit_To as [Remit To] ,TSPL_TDS_BRANCH_MASTER.Resp_Person as [Responsible Person] ,TSPL_TDS_BRANCH_MASTER.Person_Name as [Person Name] ,TSPL_TDS_BRANCH_MASTER.State_Code as [State Code] ,TSPL_TDS_BRANCH_MASTER.State_Name as [State Name] ,TSPL_TDS_BRANCH_MASTER.Inactive as [Inactive] ,TSPL_TDS_BRANCH_MASTER.Created_By as [Created By] ,TSPL_TDS_BRANCH_MASTER.Created_Date as [Created Date] ,TSPL_TDS_BRANCH_MASTER.Modify_By as [Modify By] ,TSPL_TDS_BRANCH_MASTER.Modify_Date as [Modify Date] ,TSPL_TDS_BRANCH_MASTER.Comp_Code as [Company Code]  From TSPL_TDS_BRANCH_MASTER   "
        str = clsCommon.ShowSelectForm("BRNCHDETA", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsSalaryAccountSetting
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_PAYROLL_ACCOUNTSETS.ACCOUNT_SET_CODE as [Code] ,TSPL_PAYROLL_ACCOUNTSETS.DESCRIPTION as [Description] ,TSPL_PAYROLL_ACCOUNTSETS.GL_SALARY_PAYABLE as [GL Salary Payable] ,TSPL_PAYROLL_ACCOUNTSETS.BANK_CODE as [Bank Code] ,TSPL_PAYROLL_ACCOUNTSETS.SourceCode as [Source Code] ,TSPL_PAYROLL_ACCOUNTSETS.Created_By as [Created By] ,TSPL_PAYROLL_ACCOUNTSETS.Created_Date as [Created Date] ,TSPL_PAYROLL_ACCOUNTSETS.Modified_By as [Modified By] ,TSPL_PAYROLL_ACCOUNTSETS.Modified_Date as [Modified Date]  From TSPL_PAYROLL_ACCOUNTSETS    "
        str = clsCommon.ShowSelectForm("SALACSETFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsDepAccountSet
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "  select TSPL_Dep_AccountSet.AcSet_Code as [Code] ,TSPL_Dep_AccountSet.AcSet_Desc as [Account set Description] ,TSPL_Dep_AccountSet.Inactive as [Inactive] ,TSPL_Dep_AccountSet.Ac_Control as [Account Control] ,TSPL_Dep_AccountSet.Ac_Control_YearDisposal as [Account Control Year Disposal] ,TSPL_Dep_AccountSet.Ac_Control_YearAdj as [Account Control Year Adj] ,TSPL_Dep_AccountSet.Ac_WIP as [Account WIP] ,TSPL_Dep_AccountSet.Ac_Accum_Dep as [Account Accum Dep] ,TSPL_Dep_AccountSet.Ac_Accum_Dep_YearDisposal as [Ac Accum Dep Year Disposal] ,TSPL_Dep_AccountSet.Ac_Accum_Dep_YearAdj as [Ac Accum Dep Year Adj] ,TSPL_Dep_AccountSet.Created_By as [Created By] ,TSPL_Dep_AccountSet.Created_Date as [Created Date] ,TSPL_Dep_AccountSet.Modified_By as [Modified By] ,TSPL_Dep_AccountSet.Modified_Date as [Modified Date] ,TSPL_Dep_AccountSet.Comp_Code as [Company Code] ,TSPL_Dep_AccountSet.Ac_Dep_Account as [Ac Dep Account] ,TSPL_Dep_AccountSet.Disposal_Account as [Disposal Account] ,TSPL_Dep_AccountSet.Disposal_Proceed_Account as [Disposal Proceed Account] ,TSPL_Dep_AccountSet.Transfer_Clearing_Account as [Transfer Clearing Account] ,TSPL_Dep_AccountSet.Disposal_Cost_Account as [Disposal Cost Account]  From TSPL_Dep_AccountSet   "
        str = clsCommon.ShowSelectForm("ACSETFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class
Public Class clsBlankChequeDetails
    Public Form_ID As String = Nothing
    Public Prog_Code As String = Nothing
    Public Check_No As String = Nothing
    Public Check_date As String = Nothing

    Public Shared Function LoadData(ByVal pcode As String, ByVal FormID As String) As List(Of clsBlankChequeDetails)
        Dim arr As New List(Of clsBlankChequeDetails)
        Try
            ' Dim arr As New List(Of clsBlankChequeDetails)
            Dim obj As New clsBlankChequeDetails
            Dim q As String = "select * from TSPL_Blank_Cheque_Detail where Prog_Code='" & pcode & "' and Form_ID='" & FormID & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsBlankChequeDetails
                    obj.Form_ID = clsCommon.myCstr(dtbl.Rows(i)("Form_ID"))
                    obj.Prog_Code = clsCommon.myCstr(dtbl.Rows(i)("Prog_Code"))
                    obj.Check_No = clsCommon.myCstr(dtbl.Rows(i)("Cheque_No"))
                    obj.Check_date = clsCommon.myCDate(dtbl.Rows(i)("Cheque_Date"))
                    arr.Add(obj)
                Next
            End If
            ' Return arr
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arr
    End Function
    Public Shared Function SaveData(ByVal arr As List(Of clsBlankChequeDetails), ByVal Trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim i As Integer = 0

            If arr.Count > 0 Then
                deleteData(arr.Item(0).Prog_Code, arr.Item(0).Form_ID, Trans)
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Form_ID", arr.Item(0).Form_ID)
                    clsCommon.AddColumnsForChange(coll, "Prog_Code", arr.Item(i).Prog_Code)
                    clsCommon.AddColumnsForChange(coll, "Cheque_No", arr.Item(i).Check_No)
                    clsCommon.AddColumnsForChange(coll, "Cheque_Date", clsCommon.GetPrintDate(arr.Item(i).Check_date, "dd-MMM-yyyy"))
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Blank_Cheque_Detail", OMInsertOrUpdate.Insert, "", Trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function deleteData(ByVal pcode As String, ByVal FormID As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_Blank_Cheque_Detail where prog_code='" & pcode & "' and form_ID='" & FormID & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function SaveData(ByVal obj As clsBlankChequeDetails, ByVal Trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Form_ID", obj.Form_ID)
            clsCommon.AddColumnsForChange(coll, "Prog_Code", obj.Prog_Code)
            clsCommon.AddColumnsForChange(coll, "Cheque_No", obj.Check_No)
            clsCommon.AddColumnsForChange(coll, "Cheque_date", clsCommon.GetPrintDate(obj.Check_date, "dd-MMM-yyyy"))

            If clsDBFuncationality.getSingleValue("select count(*) from TSPL_Blank_Cheque_Detail where prog_code='" & obj.Prog_Code & "' and form_id='" & obj.Form_ID & "'", Trans) = 0 Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Blank_Cheque_Detail", OMInsertOrUpdate.Insert, "", Trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Blank_Cheque_Detail", OMInsertOrUpdate.Update, "  prog_code='" & obj.Prog_Code & "' and Form_ID='" & obj.Form_ID & "'", Trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsPTMDeductionRange
    Public PTM_Code As String = Nothing
    Public Minutes As Integer
    Public Amount As Decimal

    Public Shared Function SaveData(ByVal srtPTMCode As String, ByVal arr As List(Of clsPTMDeductionRange), ByVal Trans As SqlTransaction) As Boolean
        DeleteData(srtPTMCode, Trans)
        Try
            Dim i As Integer = 0
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each obj As clsPTMDeductionRange In arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "PTM_Code", srtPTMCode)
                    clsCommon.AddColumnsForChange(coll, "Minutes", obj.Minutes)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PTM_DEDCUTION_RANGE", OMInsertOrUpdate.Insert, "", Trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal srtPTMCode As String) As List(Of clsPTMDeductionRange)
        Dim arr As New List(Of clsPTMDeductionRange)
        Try
            Dim obj As New clsPTMDeductionRange
            Dim qry As String = "select * from TSPL_PTM_DEDCUTION_RANGE where PTM_Code='" & srtPTMCode & "' order by Minutes "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    obj = New clsPTMDeductionRange
                    obj.PTM_Code = clsCommon.myCstr(dr("PTM_Code"))
                    obj.Minutes = clsCommon.myCdbl(dr("Minutes"))
                    obj.Amount = clsCommon.myCdbl(dr("Amount"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arr
    End Function
    
    Public Shared Function DeleteData(ByVal srtPTMCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_PTM_DEDCUTION_RANGE where PTM_Code='" & srtPTMCode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
     
End Class
