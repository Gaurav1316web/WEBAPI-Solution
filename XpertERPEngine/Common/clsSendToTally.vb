Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Windows.Forms



Public Class clsSendToTally

    'Tally
    Public TallyCompany As String = Nothing
    Public TallyIP As String = Nothing
    Public TallyPort As String = Nothing
    Public TM As common.Tally
    Dim IsServerConnected As Boolean = True

    Public Sub New()
        'Set_TallyParameters()
    End Sub

    Public Function Set_TallyParameters() As Boolean
        Dim StrQry As String = " select * from TSPL_FIXED_PARAMETER " & _
                               " WHERE Code IN ('TallyCompany','TallyIP','TallyPort')"
        Dim DT As DataTable = clsDBFuncationality.GetDataTable(StrQry)

        If DT Is Nothing AndAlso DT.Rows.Count <= 0 Then
            Throw New Exception("Please set Tally Parameters.")
            Return False
        End If
        For Each dr As DataRow In DT.Rows
            If clsCommon.CompairString(dr("Code"), "TallyCompany") = CompairStringResult.Equal Then
                TallyCompany = clsCommon.myCstr(dr("Code"))
            End If

            If clsCommon.CompairString(dr("Code"), "TallyIP") = CompairStringResult.Equal Then
                TallyIP = clsCommon.myCstr(dr("Code"))
            End If

            If clsCommon.CompairString(dr("Code"), "TallyPort") = CompairStringResult.Equal Then
                TallyPort = clsCommon.myCstr(dr("Code"))
            End If

        Next
        Return True
    End Function

    Public Function SendToTally_JournalEntry(ByVal Voucher_No As String, ByVal trans As SqlTransaction) As Boolean



        If objCommonVar.IsSendToTally Then
            Dim ErrorLog As New IO.StreamWriter(My.Application.Info.DirectoryPath & "\Error.log", True)
            Try


                If objCommonVar.IsPromptForTally Then
                    If clsCommon.MyMessageBoxShow("Do you want to post into Tally ?", "Tally Integration", MessageBoxButtons.YesNo) = DialogResult.No Then
                        Return False
                    End If
                End If

                Dim strQry As String = " "
                Dim IsSend As Boolean = True

                strQry = " SELECT TSPL_JOURNAL_MASTER.Voucher_No, TSPL_JOURNAL_MASTER.Voucher_Date, TSPL_JOURNAL_MASTER.Voucher_Desc, TSPL_JOURNAL_DETAILS.Detail_Line_No as [Line No], TSPL_JOURNAL_DETAILS.Account_code as [Acc Code], " & _
                         " (CASE WHEN  ISNULL (TSPL_GL_ACCOUNTS.tallyaccname,'') <>'' THEN TSPL_GL_ACCOUNTS.tallyaccname ELSE TSPL_JOURNAL_DETAILS.Account_Desc END) AS tallyaccname, " & _
                         " (CASE WHEN  ISNULL (TSPL_GL_SOURCECODE.TallyName,'') <>'' THEN TSPL_GL_SOURCECODE.TallyName ELSE 'Journal' END) AS [Source], " & _
                         " TSPL_JOURNAL_DETAILS.Account_Desc as [Acc Desc], case  when TSPL_JOURNAL_DETAILS.Amount >=0 then TSPL_JOURNAL_DETAILS.Amount else 0 end as DrAmt , case  when TSPL_JOURNAL_DETAILS.Amount <0 then TSPL_JOURNAL_DETAILS.Amount*-1 else 0 end as CrAmt, TSPL_JOURNAL_MASTER.Authorized,TSPL_JOURNAL_MASTER.SendToTally " & _
                         " FROM TSPL_JOURNAL_MASTER " & _
                         " INNER JOIN TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No " & _
                         " INNER JOIN TSPL_GL_ACCOUNTS ON TSPL_GL_ACCOUNTS.Account_Code = TSPL_JOURNAL_DETAILS.Account_code " & _
                         " INNER JOIN TSPL_GL_SOURCECODE ON TSPL_GL_SOURCECODE.SourceCode = TSPL_JOURNAL_MASTER.Source_Code " & _
                         " WHERE TSPL_JOURNAL_MASTER.Voucher_No= '" + Voucher_No + "' order by [Line No] "
                Dim DT As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)

                If DT Is Nothing AndAlso DT.Rows.Count <= 0 Then
                    'Throw New Exception("No data found against Voucher No : " + Voucher_No + " .")
                    'clsCommon.MyMessageBoxShow("No data found against Voucher No : " + Voucher_No + " .")
                    IsSend = False
                    Return False
                End If
                'For Each Row As DataRow In DT.Rows
                '    If clsCommon.myLen(Row("tallyaccname")) < 1 Then
                '        Throw New Exception("Please Map Account in Tally against Account No : " + clsCommon.myCstr(Row("Acc Code")) + " .")
                '    End If
                'Next

                Dim DrBase As DataRow = DT.Rows(0)
                If clsCommon.myCBool(DrBase("SendToTally")) Then
                    'Throw New Exception("Voucher already Send to Tally.")
                    'clsCommon.MyMessageBoxShow("Voucher already Exported to Tally.")
                    IsSend = False
                    Return False
                End If

                'If clsCommon.CompairString(DrBase("Authorized"), "A") = CompairStringResult.Equal Then
                TM = New common.Tally(objCommonVar.CurrentCompanyName, objCommonVar.TallyIP & ":" & objCommonVar.TallyPort)
                'TM.CompanyName = objCommonVar.TallyCompany
                TM.CompanyName = objCommonVar.CurrentCompanyName
                TM.Tally_Destination = objCommonVar.TallyIP & ":" & objCommonVar.TallyPort
                Dim vch As New common.Voucher()
                ' Dim vchid As Integer
                With vch
                    .Action = common.TallyAction.Create
                    .ObjTally = TM
                    .VoucherNumber = clsCommon.myCstr(DrBase("Voucher_No"))
                    .VoucherDate = clsCommon.GetPrintDate(DrBase("Voucher_Date"), "dd/MMM/yyyy")
                    If clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Journal") = CompairStringResult.Equal Then
                        .VchType = common.VoucherType.Journal
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Payment") = CompairStringResult.Equal Then
                        .VchType = common.VoucherType.Payment
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Contra") = CompairStringResult.Equal Then
                        .VchType = common.VoucherType.Contra
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Receipt") = CompairStringResult.Equal Then
                        .VchType = common.VoucherType.Receipt
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Sales") = CompairStringResult.Equal Then
                        .VchType = common.VoucherType.Sales
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Purchase") = CompairStringResult.Equal Then
                        .VchType = common.VoucherType.Purchase
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "DebitNote") = CompairStringResult.Equal Then
                        .VchType = common.VoucherType.DebitNote
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "CreditNote") = CompairStringResult.Equal Then
                        .VchType = common.VoucherType.CreditNote
                    End If
                    .Narration = clsCommon.myCstr(DrBase("Voucher_Desc"))
                    For Each Row As DataRow In DT.Rows
                        If clsCommon.myLen(Row("tallyaccname")) > 0 Then
                            If clsCommon.myCdbl(Row("DrAmt")) > 0 Then '= "Debit" 
                                If clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Journal") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Current Assets") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Payment") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Current Assets") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Contra") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Current Assets") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Receipt") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Bank Accounts") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Sales") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Bank Accounts") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Purchase") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Purchase Accounts") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "DebitNote") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Bank Accounts") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "CreditNote") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Current Assets") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                End If
                                .DebitLedger.Add(New common.Debit(clsCommon.myCstr(Row("tallyaccname")), clsCommon.myCdbl(Row("DrAmt"))))
                            End If

                            If clsCommon.myCdbl(Row("CrAmt")) > 0 Then '= "Credit" 
                                If clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Journal") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Current Assets") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Payment") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Bank Accounts") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Contra") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Bank Accounts") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Receipt") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Current Assets") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Sales") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Sales Accounts") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Purchase") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Bank Accounts") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "DebitNote") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Current Assets") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "CreditNote") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Bank Accounts") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                End If
                                .CreditLedger.Add(New common.Credit(clsCommon.myCstr(Row("tallyaccname")), clsCommon.myCdbl(Row("CrAmt"))))
                            End If
                        End If
                    Next

                    .Create()
                    If .GetResult = common.TallyResult.Errors Or .GetResult = 0 Then
                        IsSend = False
                        'Throw New Exception(vch.GetErrorDetail & vbCrLf & vch.GetLastResponse)
                        'MsgBox(vch.GetErrorDetail & vbCrLf & vch.GetLastResponse)
                        ErrorLog.Write("-------" & Now & " - Journal Entry " & vbCrLf & vch.GetErrorDetail & vbCrLf)
                        ErrorLog.Close()
                        Return False
                    End If
                End With
                ErrorLog.Close()
                If IsSend Then
                    strQry = " Update TSPL_JOURNAL_MASTER set SendToTally = 1 " & _
                             " WHERE Voucher_No= '" + Voucher_No + "' "
                    clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                    'clsCommon.MyMessageBoxShow("Data Send To Tally Successfully.")
                End If
            Catch ex As Exception
                ErrorLog.Close()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function

    Public Function createLedger(ByVal LedgerName As String, ByVal LedgerAlias As String) As Boolean
        Try
            If objCommonVar.IsSendToTally Then
                Dim AlisList As New List(Of String)
                AlisList.Add(LedgerAlias)
                Dim sAppLedger As String
                Dim L1 As New common.Ledger()
                Dim sAppGroup As String = "Current Assets"
                L1.ObjTally.CompanyName = objCommonVar.TallyCompany
                L1.ObjTally.Tally_Destination = objCommonVar.TallyIP & ":" & objCommonVar.TallyPort
                sAppLedger = LedgerName
                L1.LedgerName = sAppLedger
                'L1.AdditionalName = sAppLedger
                L1.AliasList = AlisList
                L1.Parent = sAppGroup
                L1.Create()
                If L1.GetResult = TallyResult.Errors Or L1.GetResult = 0 Then
                    'Throw New Exception(L1.GetErrorDetail & vbCrLf & L1.GetLastResponse)
                    'MsgBox(L1.GetErrorDetail & vbCrLf & L1.GetLastResponse)
                    Return False
                End If
            End If
        Catch ex As Exception
            'Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SendToTally_JournalEntry_BulkPost(ByVal Voucher_No As String) As Boolean
        Dim ErrorLog As New IO.StreamWriter(My.Application.Info.DirectoryPath & "\Error.log", True)

        Try

            If objCommonVar.IsSendToTally Then

                Dim strQry As String = " "
                Dim IsSend As Boolean = True

                strQry = " SELECT TSPL_JOURNAL_MASTER.Voucher_No, TSPL_JOURNAL_MASTER.Voucher_Date, TSPL_JOURNAL_MASTER.Voucher_Desc, TSPL_JOURNAL_DETAILS.Detail_Line_No as [Line No], TSPL_JOURNAL_DETAILS.Account_code as [Acc Code], " & _
                         " (CASE WHEN  ISNULL (TSPL_GL_ACCOUNTS.tallyaccname,'') <>'' THEN TSPL_GL_ACCOUNTS.tallyaccname ELSE TSPL_JOURNAL_DETAILS.Account_Desc END) AS tallyaccname, " & _
                         " (CASE WHEN  ISNULL (TSPL_GL_SOURCECODE.TallyName,'') <>'' THEN TSPL_GL_SOURCECODE.TallyName ELSE 'Journal' END) AS [Source], " & _
                         " TSPL_JOURNAL_DETAILS.Account_Desc as [Acc Desc], case  when TSPL_JOURNAL_DETAILS.Amount >=0 then TSPL_JOURNAL_DETAILS.Amount else 0 end as DrAmt , case  when TSPL_JOURNAL_DETAILS.Amount <0 then TSPL_JOURNAL_DETAILS.Amount*-1 else 0 end as CrAmt, TSPL_JOURNAL_MASTER.Authorized,TSPL_JOURNAL_MASTER.SendToTally " & _
                         " FROM TSPL_JOURNAL_MASTER " & _
                         " INNER JOIN TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No " & _
                         " INNER JOIN TSPL_GL_ACCOUNTS ON TSPL_GL_ACCOUNTS.Account_Code = TSPL_JOURNAL_DETAILS.Account_code " & _
                         " INNER JOIN TSPL_GL_SOURCECODE ON TSPL_GL_SOURCECODE.SourceCode = TSPL_JOURNAL_MASTER.Source_Code " & _
                         " WHERE TSPL_JOURNAL_MASTER.Voucher_No= '" + Voucher_No + "' order by [Line No] "
                Dim DT As DataTable = clsDBFuncationality.GetDataTable(strQry)

                If DT Is Nothing Or DT.Rows.Count <= 0 Then
                    'Throw New Exception("No data found against Voucher No : " + Voucher_No + " .")
                    'clsCommon.MyMessageBoxShow("No data found against Voucher No : " + Voucher_No + " .")
                    IsSend = False
                    Return False
                End If

                Dim DrBase As DataRow = DT.Rows(0)
                If clsCommon.myCBool(DrBase("SendToTally")) Then
                    'Throw New Exception("Voucher already Send to Tally.")
                    'clsCommon.MyMessageBoxShow("Voucher already Exported to Tally.")
                    IsSend = False
                    Return False
                End If

                'If clsCommon.CompairString(DrBase("Authorized"), "A") = CompairStringResult.Equal Then
                TM = New common.Tally(objCommonVar.CurrentCompanyName, objCommonVar.TallyIP & ":" & objCommonVar.TallyPort)
                'TM.CompanyName = objCommonVar.TallyCompany
                TM.CompanyName = objCommonVar.CurrentCompanyName
                TM.Tally_Destination = objCommonVar.TallyIP & ":" & objCommonVar.TallyPort
                Dim vch As New common.Voucher()
                ' Dim vchid As Integer
                With vch
                    .Action = common.TallyAction.Create
                    .ObjTally = TM
                    .VoucherNumber = clsCommon.myCstr(DrBase("Voucher_No"))
                    .VoucherDate = clsCommon.GetPrintDate(DrBase("Voucher_Date"), "dd/MMM/yyyy")
                    If clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Journal") = CompairStringResult.Equal Then
                        .VchType = common.VoucherType.Journal
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Payment") = CompairStringResult.Equal Then
                        .VchType = common.VoucherType.Payment
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Contra") = CompairStringResult.Equal Then
                        .VchType = common.VoucherType.Contra
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Receipt") = CompairStringResult.Equal Then
                        .VchType = common.VoucherType.Receipt
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Sales") = CompairStringResult.Equal Then
                        .VchType = common.VoucherType.Sales
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Purchase") = CompairStringResult.Equal Then
                        .VchType = common.VoucherType.Purchase
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "DebitNote") = CompairStringResult.Equal Then
                        .VchType = common.VoucherType.DebitNote
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "CreditNote") = CompairStringResult.Equal Then
                        .VchType = common.VoucherType.CreditNote
                    End If
                    .Narration = clsCommon.myCstr(DrBase("Voucher_Desc"))
                    For Each Row As DataRow In DT.Rows

                        If clsCommon.myLen(Row("tallyaccname")) > 0 Then
                            'tallyaccname 'Acc Code

                            If clsCommon.myCdbl(Row("DrAmt")) > 0 Then '= "Debit" 
                                If clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Journal") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Current Assets") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Payment") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Current Assets") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Contra") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Current Assets") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Receipt") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Bank Accounts") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Sales") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Bank Accounts") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Purchase") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Purchase Accounts") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "DebitNote") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Bank Accounts") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "CreditNote") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Current Assets") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                End If
                                .DebitLedger.Add(New common.Debit(clsCommon.myCstr(Row("tallyaccname")), clsCommon.myCdbl(Row("DrAmt"))))
                            End If
                            If clsCommon.myCdbl(Row("CrAmt")) > 0 Then '= "Credit" 
                                If clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Journal") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Current Assets") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Payment") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Bank Accounts") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Contra") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Bank Accounts") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Receipt") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Current Assets") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Sales") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Sales Accounts") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "Purchase") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Bank Accounts") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "DebitNote") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Current Assets") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(DrBase("Source")), "CreditNote") = CompairStringResult.Equal Then
                                    If Create_BankAccount(clsCommon.myCstr(Row("tallyaccname")), "", "Bank Accounts") = False Then
                                        IsServerConnected = False
                                        Return False
                                    End If
                                End If
                                .CreditLedger.Add(New common.Credit(clsCommon.myCstr(Row("tallyaccname")), clsCommon.myCdbl(Row("CrAmt"))))
                            End If
                        End If
                    Next

                    .Create()
                    If .GetResult = common.TallyResult.Errors Or .GetResult = 0 Then
                        IsSend = False
                        'Throw New Exception(vch.GetErrorDetail & vbCrLf & vch.GetLastResponse)
                        MsgBox(vch.GetErrorDetail & vbCrLf & vch.GetLastResponse)
                        ErrorLog.Write("-------" & Now & " - Journal Entry " & vbCrLf & vch.GetErrorDetail & vbCrLf)
                        ErrorLog.Close()
                        Return False
                    End If
                End With
                ErrorLog.Close()
                If IsSend Then
                    strQry = " Update TSPL_JOURNAL_MASTER set SendToTally = 1 " & _
                             " WHERE Voucher_No= '" + Voucher_No + "' "
                    clsDBFuncationality.ExecuteNonQuery(strQry)
                    'clsCommon.MyMessageBoxShow("Data Send To Tally Successfully.")
                End If
            End If
        Catch ex As Exception
            ErrorLog.Close()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Function Create_BankAccount(ByVal LedgerName As String, ByVal LedgerAlias As String, ByVal LedgerGroup As String) As Boolean
        Try
            If objCommonVar.IsSendToTally Then

                Dim AlisList As New List(Of String)
                AlisList.Add(LedgerAlias)

                Dim TL As New common.Ledger()
                'TL.ObjTally.CompanyName = objCommonVar.TallyCompany
                TL.ObjTally.CompanyName = objCommonVar.CurrentCompanyName
                TL.ObjTally.Tally_Destination = objCommonVar.TallyIP & ":" & objCommonVar.TallyPort

                TL.LedgerName = LedgerName
                TL.Parent = LedgerGroup
                TL.AliasList = AlisList
                TL.Create()

                If TL.GetResult = common.TallyResult.Errors Or TL.GetResult = 0 Then
                    MsgBox(TL.GetErrorDetail & vbCrLf & TL.GetLastResponse)
                    Return False
                End If
            End If
        Catch ex As Exception
            'Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class
