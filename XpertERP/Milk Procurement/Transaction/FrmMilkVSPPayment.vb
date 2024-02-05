'-28/11/2012-3:07PM--Updation By--Pankaj Kumar--Viewed New Transaction [VCGL Entry] in Module [General Ledger]---
'-28/11/2012-3:07PM--Updation By--Pankaj Kumar--Viewed New Transaction [VCGL Entry] in Module [General Ledger]---
''''05/12/2012-12:57PM--Updation by --[Pankaj kumar]-- Viewed New Transaction [Empty Transactions] in Module [Material Management]-----From-Ranjana Sinha
''''06/12/2012-01:25PM--Updation by --[Pankaj kumar]-- Viewed New Transaction [Sale return (Inter Company)] in Module [Sales n Disribution]-----From-Ranjana Sinha
Imports common
Imports System.Data.SqlClient

Public Class FrmMilkVSPPayment
    Inherits FrmMainTranScreen
#Region "Variables"
    Const Modul As String = "Module"
    Const Transaction As String = "Transaction"
    Const ApprovedDoc As String = "No of Approved Doc"
    Const UnApprovedDoc As String = "No of UnApproved Doc"
    Dim ButtonTooltip As New ToolTip()
    Dim DtIncentive As DataTable
    Dim Formcode As String
    Dim Is_Load As Boolean = False
    Dim AllowDateChanged As Boolean = False
    Dim isPickPendingMilkSRNinNextPaymentCycle As Boolean = False
    Dim isStopVSPBillIfSomethingWrong As Boolean = False
    Dim IsRoundOffPaiseAmount As Boolean = False
    Dim settDoNotIncludeIncentiveInMilkPurchaseInvoice As Boolean = False
    Dim SettMultipleMCCFinder As Boolean = False
    Dim MultipleFinderFillAuto As Boolean = False
    Dim ApplyUnpaidBank As Boolean = False
    Dim CompanyVSPDeduction As Decimal = 0
    Dim NonCompanyVSPDeduction As Decimal = 0
    Dim AreaWiseBilling As Boolean = False


#End Region

    Public Sub New(ByVal FormId As String)
        InitializeComponent()
        Formcode = FormId
    End Sub

    Private Sub SetUserMgmtNew()
        If Formcode = clsUserMgtCode.MilkVSPIssuePayment Or Formcode = clsUserMgtCode.MPBillGeneration Then
            If Not (MyBase.isReadFlag) Then
                Throw New Exception("Permission Denied")
            End If
        Else
            If Not (MyBase.isReadFlag) Then
                Throw New Exception("Permission Denied")
            End If
        End If
    End Sub

    Private Sub FrmMilkVSPPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Is_Load = True
        ButtonTooltip.SetToolTip(btnClose, "Press Alt+C for Close the Window")
        ButtonTooltip.SetToolTip(btnGenerateBill, "Press Alt+R for Refresh the Data")
        SetUserMgmtNew()
        BtnIncentive.Visible = False
        txtMonth.Value = clsCommon.GETSERVERDATE()
        If Formcode = clsUserMgtCode.MilkVSPIssuePayment Then
            btnGenerateBill.Text = "Generate Debit Note"
            btnGenerateBill.Size = New Point(btnGenerateBill.Size.Width + 10, btnGenerateBill.Size.Height)
        End If
        Is_Load = False
        AllowDateChanged = True
        isPickPendingMilkSRNinNextPaymentCycle = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickPendingMilkSRNinNextPaymentCycle, clsFixedParameterCode.PickPendingMilkSRNinNextPaymentCycle, Nothing)) = 1
        isStopVSPBillIfSomethingWrong = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.StopVSPBillIfSomethingWrong, clsFixedParameterCode.StopVSPBillIfSomethingWrong, Nothing)) = 1
        IsRoundOffPaiseAmount = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RoundOffPaiseAmount, clsFixedParameterCode.RoundOffPaiseAmount, Nothing)) = 1)
        settDoNotIncludeIncentiveInMilkPurchaseInvoice = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotIncludeIncentiveInMilkPurchaseInvoice, clsFixedParameterCode.DoNotIncludeIncentiveInMilkPurchaseInvoice, Nothing)) = 1)
        SettMultipleMCCFinder = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MultipleMCCFinder, clsFixedParameterCode.MultipleMCCFinder, Nothing)) = 1)
        MultipleFinderFillAuto = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MultipleFinderFillAuto, clsFixedParameterCode.MultipleFinderFillAuto, Nothing)) = 1)
        ApplyUnpaidBank = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyUnpaidBank, clsFixedParameterCode.ApplyUnpaidBank, Nothing)) = 1)
        AreaWiseBilling = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AreaWiseBilling, clsFixedParameterCode.AreaWiseBilling, Nothing)) = 1)

        If SettMultipleMCCFinder Then
            SplitContainer1.Panel1Collapsed = True
            If MultipleFinderFillAuto Then
                FillAllMCCDefault()
                AutoFillAllVSP()
            End If
        Else
            SplitContainer1.Panel2Collapsed = True
        End If
    End Sub

    Sub UpdateSTDQtyOFSRN()
        Try
            Dim qry As String = "update TSPL_MILK_SRN_DETAIL set Std_Qty=xxxx.STDQTY" +
            " from(" +
            " select xx.DOC_CODE,convert(decimal(18,2), (select  top 1 (  FATKG *CAST(Ratio as decimal)/FAT_Pers)+(SNFKg*SNF_Ratio/SNF_Pers) as Qty  " +
            " from TSPL_MILK_PRICE_MASTER " +
            " where Effective_Date<=xx.doc_date order by Effective_Date desc))  as STDQTY  from (" +
            " select TSPL_MILK_SRN_DETAIL.DOC_CODE,TSPL_MILK_SRN_HEAD.DOC_DATE,TSPL_MILK_SRN_DETAIL.ACC_Qty,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER  " +
            " ,TSPL_MILK_SRN_DETAIL.ACC_Qty*TSPL_MILK_SRN_DETAIL.FAT_PER/100 as FATKG,TSPL_MILK_SRN_DETAIL.ACC_Qty*TSPL_MILK_SRN_DETAIL.SNF_PER/100 as SNFKg " +
            " from TSPL_MILK_SRN_DETAIL " +
            " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE " +
            " where TSPL_MILK_SRN_DETAIL.Std_Qty = 0 " +
            " )xx " +
            " )xxxx inner join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE=xxxx.DOC_CODE"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
        End Try
    End Sub

    Public Function CreateMCCChillingProvision(ByVal strMCCCode As String, ByVal strMCCName As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try


            Dim qry As String = "select is_Chilling_Provision_Monthly from TSPL_MCC_MASTER where MCC_Type='Chilling Basis' and MCC_Code='" + strMCCCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.myCdbl(dt.Rows(0)("is_Chilling_Provision_Monthly")) = 0 Then
                    qry = "select * FROM ExplodeDates( " + "'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'" + ",'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')"
                    Dim dtDateRange As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtDateRange IsNot Nothing AndAlso dtDateRange.Rows.Count > 0 Then
                        For Each drDateRange As DataRow In dtDateRange.Rows
                            CreateMCCChillingProvisionDaily(clsCommon.myCDate(drDateRange(0)), strMCCCode, strMCCName, trans)
                        Next
                    End If
                ElseIf clsCommon.myCdbl(dt.Rows(0)("is_Chilling_Provision_Monthly")) = 1 Then
                    CreateMCCChillingProvisionMonthly(strMCCCode, strMCCName, trans)
                End If
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function CreateMCCChillingProvisionDaily(ByVal DOC_DATE As Date, ByVal strMCCCode As String, ByVal strMCCName As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select 1 from TSPL_PROVISION_ENTRY where Prov_type='Chilling Charge' and Loc_Code='" + strMCCCode + "' and convert(date, Doc_Date,103)='" + clsCommon.GetPrintDate(DOC_DATE, "dd/MMM/yyyy") + "' and Prog_Code='" + clsUserMgtCode.MilkVSPPayment + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Dim sQuery As String = "select (case when Qty<Chilling_Assure_Qty then Chilling_Assure_Qty else Qty end)*Chilling_Rate as Amount  from (" + Environment.NewLine +
                " select TSPL_MCC_MASTER.Chilling_Assure_Qty,TSPL_MCC_MASTER.Chilling_Rate,TSPL_MCC_MASTER.Unit_ChillingOn,xxx.ACC_Qty" + Environment.NewLine +
                " ,( case when Unit_ChillingOn='K' then ACC_Qty else Qty end ) as Qty from " + Environment.NewLine +
                " ( " + Environment.NewLine +
                " select max(MCC_CODE) as MCC_CODE, sum(ACC_Qty) as ACC_Qty,sum(Qty) as Qty from ( " + Environment.NewLine +
                " select TSPL_MILK_SRN_HEAD.MCC_CODE,TSPL_MILK_SRN_DETAIL.ACC_Qty,Qty from TSPL_MILK_SRN_DETAIL" + Environment.NewLine +
                " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine +
                " where TSPL_MILK_SRN_HEAD.MCC_CODE='" + strMCCCode + "' and TSPL_MILK_SRN_HEAD.Posted=1 and DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(DOC_DATE), "dd/MMM/yyyy hh:mm tt") + "' and DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(DOC_DATE), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine +
                " )xx" + Environment.NewLine +
                " )xxx left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=xxx.MCC_CODE " + Environment.NewLine +
                " where  TSPL_MCC_MASTER.MCC_Type='Chilling Basis'" + Environment.NewLine +
                " )xxxx"
            Dim dtChilling As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
            Dim Provision_Amt As Decimal = 0
            If dtChilling IsNot Nothing AndAlso dtChilling.Rows.Count > 0 Then
                Provision_Amt = Math.Round(clsCommon.myCdbl(dtChilling.Rows(0)("Amount")), 2)
            End If
            If Provision_Amt <= 0 Then
                Provision_Amt = 0
            End If
            ChillingProvision(Provision_Amt, strMCCCode, strMCCName, trans, DOC_DATE)

        End If
        Return True
    End Function
    Public Function CreateMCCChillingProvisionMonthly(ByVal strMCCCode As String, ByVal strMCCName As String, ByVal trans As SqlTransaction) As Boolean
        Dim FirstDate As DateTime = New DateTime(txtToDate.Value.Year, txtToDate.Value.Month, 1)
        Dim lastDate As DateTime = FirstDate.AddMonths(1).AddDays(-1)
        If txtToDate.Value.Day = lastDate.Day Then
            Dim qry As String = "select 1 from TSPL_PROVISION_ENTRY where Prov_type='Chilling Charge' and Loc_Code='" + strMCCCode + "' and Prov_Year='" + clsCommon.myCstr(lastDate.Year) + "' and Prov_Month='" + clsCommon.myCstr(lastDate.Month) + "' and Prog_Code='" + clsUserMgtCode.MilkVSPPayment + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Try
                    qry = "select (case when Qty<Chilling_Assure_Qty then Chilling_Assure_Qty else Qty end)*Chilling_Rate as Amount  from (" + Environment.NewLine +
                        " select TSPL_MCC_MASTER.Chilling_Assure_Qty,TSPL_MCC_MASTER.Chilling_Rate,TSPL_MCC_MASTER.Unit_ChillingOn,xxx.ACC_Qty" + Environment.NewLine +
                        " ,( case when Unit_ChillingOn='K' then ACC_Qty else Qty end ) as Qty from " + Environment.NewLine +
                        " ( " + Environment.NewLine +
                        " select max(MCC_CODE) as MCC_CODE, sum(ACC_Qty) as ACC_Qty,sum(Qty) as Qty from ( " + Environment.NewLine +
                        " select TSPL_MILK_SRN_HEAD.MCC_CODE,TSPL_MILK_SRN_DETAIL.ACC_Qty,TSPL_MILK_SRN_DETAIL.Qty from TSPL_MILK_SRN_DETAIL" + Environment.NewLine +
                        " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine +
                        " where TSPL_MILK_SRN_HEAD.MCC_CODE='" + strMCCCode + "' and TSPL_MILK_SRN_HEAD.Posted=1 and DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FirstDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(lastDate), "dd/MMM/yyyy hh:mm:ss tt") + "'" + Environment.NewLine +
                        " )xx" + Environment.NewLine +
                        " )xxx left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=xxx.MCC_CODE " + Environment.NewLine +
                        " where  TSPL_MCC_MASTER.MCC_Type='Chilling Basis' and TSPL_MCC_MASTER.is_Chilling_Provision_Monthly='1'" + Environment.NewLine +
                        " )xxxx"

                    Dim dtChilling As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    Dim Provision_Amt = 0
                    If dtChilling IsNot Nothing AndAlso dtChilling.Rows.Count > 0 Then
                        Provision_Amt = Math.Round(clsCommon.myCdbl(dtChilling.Rows(0)("Amount")), 2)
                    End If
                    If Provision_Amt <= 0 Then
                        Provision_Amt = 0
                    End If
                    ChillingProvision(Provision_Amt, strMCCCode, strMCCName, trans, lastDate)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End If
        End If
        Return True
    End Function
    Sub ChillingProvision(ByVal Provision_Amt As Decimal, ByVal strMCCCode As String, ByVal strMCCName As String, ByVal trans As SqlTransaction, ByVal lastDate As Date)
        If Provision_Amt > 0 Then
            Dim dtmcc As DataTable = clsDBFuncationality.GetDataTable("select tspl_mcc_master.*,vendor_name from tspl_mcc_master inner join " _
                    & " tspl_vendor_master on vendor_code=chilling_vendor where  tspl_mcc_master.mcc_code='" + strMCCCode + "'", trans)
            If dtmcc.Rows.Count > 0 Then
                Dim obj As New clsProvisionEntry()
                obj = New clsProvisionEntry()
                obj.Doc_Date = lastDate
                obj.Vendor_Code = dtmcc.Rows(0)("Chilling_Vendor")
                obj.Vendor_Desc = dtmcc.Rows(0)("Vendor_Name")
                obj.Vendor_Type = "MCC Lease Vendor"
                obj.Status = "No"
                obj.Loc_Code = strMCCCode
                obj.Loc_Desc = strMCCName
                obj.Ref_Doc_No = ""
                obj.Prov_type = "Chilling Charge"
                obj.Amount = Provision_Amt
                obj.Prog_Code = clsUserMgtCode.MilkVSPPayment
                obj.Prov_Month = Month(lastDate)
                obj.Prov_Year = Year(lastDate)
                obj.Modified_Date = clsCommon.GETSERVERDATE(trans)
                obj.isNewEntry = True
                clsProvisionEntry.SaveData(obj, trans)
                clsProvisionEntry.PostData(obj.Doc_No, trans, False)
            End If
        End If
    End Sub

    Private Sub btnGenerateBill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateBill.Click
        Try
            If ApplyUnpaidBank Then

                Dim ChkQryUnpaidBank As String = "select count(*) from TSPL_BANK_MASTER where Unpaid='1' "
                Dim count As Integer = clsDBFuncationality.getSingleValue(ChkQryUnpaidBank)
                If count = 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Define Unpaid Bank in Bank Master", Me.Text)
                    Exit Sub
                End If

                Dim strVLC As String = clsCommon.GetMulcallString(txtVSP.arrValueMember)
                Dim DCSCountBank1 As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_VENDOR_MASTER where Form_Type = 'VSP' and Bank_Code is null or Bank_Code = '' AND Vendor_Code IN (" & strVLC & ") ")
                Dim DCSCountBank2 As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_VENDOR_MASTER where Form_Type = 'VSP' and BankCode2 is null or BankCode2 = '' AND Vendor_Code IN (" & strVLC & ") ")
                Dim TotalDcsCount As Integer = DCSCountBank1 + DCSCountBank2
                If TotalDcsCount > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "" & TotalDcsCount & " DCS have blank Bank Details", Me.Text)
                    If common.clsCommon.MyMessageBoxShow("We will update UNPAID Bank Details on these DCS. Do you want to proceed?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        If DCSCountBank1 > 0 Then
                            clsDBFuncationality.ExecuteNonQuery("UPDATE  TSPL_VENDOR_MASTER SET Bank_Code = 'UNPAID' , Bank_Name = 'UNPAID', IFSC_Code = 'UNPAID', Account_No = 'UNPAID ' where Form_Type = 'VSP' AND Bank_Code IS NULL or Bank_Code = '' AND Vendor_Code IN (" & strVLC & ")")
                        End If
                        If DCSCountBank2 > 0 Then
                            clsDBFuncationality.ExecuteNonQuery("UPDATE  TSPL_VENDOR_MASTER SET BankCode2 = 'UNPAID' ,BankName2 = 'UNPAID', IFSCCode2 = 'UNPAID', AccNo2 = 'UNPAID' where Form_Type = 'VSP' AND BankCode2 IS NULL or BankCode2 = '' AND Vendor_Code IN (" & strVLC & ")")
                        End If

                        GenerateBill()
                    Else
                        Exit Sub
                    End If
                Else
                    GenerateBill()
                End If
            Else
                GenerateBill()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub GenerateBill()
        Try
            Dim obj As New clsVSPBillAndIncentiveCalculation()
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkVSPPayment, txtMCC.Value, txtFromDate.Value, Nothing)
            If SettMultipleMCCFinder Then
                For Each strMCC As String In txtMCCMultiple.arrValueMember
                    If MultipleFinderFillAuto Then
                        Dim qry As String = "select VSP_Code from TSPL_VLC_MASTER_HEAD where MCC='" + strMCC + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Dim arrVSP As New ArrayList
                            For Each dr As DataRow In dt.Rows
                                arrVSP.Add(clsCommon.myCstr(dr("VSP_Code")))
                            Next

                            obj.BillGenerationMCCWise(Me, strMCC, txtFromDate.Value, txtToDate.Value, Formcode, arrVSP, lblPaymentType.Text, clsCommon.myCdbl(lblPaymentType.Tag))
                        End If
                    Else
                        obj.BillGenerationMCCWise(Me, strMCC, txtFromDate.Value, txtToDate.Value, Formcode, txtVSP.arrValueMember, lblPaymentType.Text, clsCommon.myCdbl(lblPaymentType.Tag))
                    End If
                Next
            Else
                obj.BillGenerationMCCWise(Me, txtMCC.Value, txtFromDate.Value, txtToDate.Value, Formcode, txtVSP.arrValueMember, lblPaymentType.Text, clsCommon.myCdbl(lblPaymentType.Tag))
            End If
            txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtMCC.Value, txtToDate.Value)
            txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtMCC.Value, txtToDate.Value)
            clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully.", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    'Private Sub btnGenerateBill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateBill.Click
    '    Try


    '        UpdateSTDQtyOFSRN()
    '        If SettMultipleMCCFinder Then
    '            For Each strMCC As String In txtMCCMultiple.arrValueMember
    '                BillGenerationMCCWise(strMCC, clsMccMaster.GetName(strMCC, Nothing))
    '            Next
    '        Else
    '            BillGenerationMCCWise(txtMCC.Value, lblMCC.Text)
    '        End If
    '        clsCommon.MyMessageBoxShow("Data Saved Successfully.")
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '    End Try
    'End Sub

    'Sub BillGenerationMCCWise(ByVal strMCCCode As String, ByVal strMCCName As String)
    '    If clsCommon.myLen(strMCCCode) <= 0 Then
    '        Throw New Exception("Please Select MCC To Generate Bill")
    '    End If
    '    If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
    '        Throw New Exception("Please Select VSP To Generate Bill")
    '    End If
    '    'If txtFromDate.Value.Date = txtToDate.Value.Date Then
    '    '    Throw New Exception("From Date and To Date can't be same")
    '    'End If
    '    CreateMCCChillingProvision(strMCCCode, strMCCName)

    '    If clsCommon.CompairString(Formcode, clsUserMgtCode.MPBillGeneration) = CompairStringResult.Equal Then
    '        Dim QRY As String = "SELECT LOCK_CODE FROM TSPL_LOCK_MP_PC WHERE MCC_Code='" & strMCCCode & "' AND Posted='Y' AND FROM_DATE='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") & "' AND TO_DATE='" & clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") & "'"
    '        Dim Lock_Code As String = clsDBFuncationality.getSingleValue(QRY)
    '        If clsCommon.myLen(Lock_Code) <= 0 Then
    '            Throw New Exception("Selected MCC is not locked for selected From and To date. Please lock it first.")
    '        End If
    '    End If
    '    If Formcode = clsUserMgtCode.MilkVSPIssuePayment Then
    '        Generate_Vsp_Issue_Debit_Note(strMCCCode, "D")
    '        Generate_Vsp_Issue_Debit_Note(strMCCCode, "C")
    '    Else
    '        If isStopVSPBillIfSomethingWrong Then
    '            Dim qry As String = "select DOC_CODE,FAT_PER,SNF_PER,VLC_Code_VLC_Uploader as VLCUploaderCode  from (" + Environment.NewLine + _
    '            " select TSPL_MILK_SRN_DETAIL.DOC_CODE,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER ,TSPL_MILK_SRN_HEAD.VLC_CODE," + Environment.NewLine + _
    '            "(select top 1 Rate" + Environment.NewLine + _
    '             " from TSPL_FAT_SNF_UPLOADER_MASTER" + Environment.NewLine + _
    '            "inner join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code=TSPL_MILK_SRN_HEAD.MCC_CODE and  TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_MCC.Code " + Environment.NewLine + _
    '            " inner join TSPL_FAT_SNF_UPLOADER_VLC on VLC_Code=TSPL_MILK_SRN_HEAD.VLC_CODE  and  TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_VLC.Code " + Environment.NewLine + _
    '            " where  posted='1' and  fat=TSPL_MILK_SRN_DETAIL.FAT_PER and SNF=TSPL_MILK_SRN_DETAIL.SNF_PER and (date< convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) or (date= convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) and Price_code_shift>=TSPL_MILK_SRN_HEAD.SHIFT)) " + Environment.NewLine + _
    '            " order by date desc ,TSPL_FAT_SNF_UPLOADER_MASTER.code desc) as RATE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader " + Environment.NewLine + _
    '            " from TSPL_MILK_SRN_DETAIL" + Environment.NewLine + _
    '            " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine + _
    '            " left outer join TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL on TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_SRN_Code=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine + _
    '            " left outer join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine + _
    '            " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SRN_HEAD.VLC_CODE " + Environment.NewLine + _
    '            " where TSPL_MILK_SRN_DETAIL.AMOUNT <= 0 And Against_Reject_No Is null" + Environment.NewLine + _
    '            " and TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.Incentive_Amount is null" + Environment.NewLine + _
    '            " and TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  is null" + Environment.NewLine + _
    '            " and TSPL_MILK_SRN_HEAD.MCC_CODE = '" + strMCCCode + "' " + Environment.NewLine + _
    '            " and TSPL_MILK_SRN_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine + _
    '            " and TSPL_MILK_SRN_HEAD.VSP_coDE in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")" + Environment.NewLine + _
    '            " )xxx where isnull(RATE,0)>0 "
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                If clsCommon.MyMessageBoxShow("There are some SRN With Wrong Price.Do You want to export these documents", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
    '                    transportSql.ExporttoExcelWithoutFilter(qry, "", "", Me)
    '                End If
    '                Exit Sub
    '            End If

    '            qry = "select TSPL_MILK_REJECT_HEAD.DOC_CODE,TSPL_MILK_REJECT_DETAIL.SAMPLE_NO,TSPL_MILK_REJECT_DETAIL.Defaulter from TSPL_MILK_REJECT_DETAIL " + Environment.NewLine + _
    '            " left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODE=TSPL_MILK_REJECT_DETAIL.DOC_CODE " + Environment.NewLine + _
    '            " where " + Environment.NewLine + _
    '            " TSPL_MILK_REJECT_HEAD.MCC_CODE='" + strMCCCode + "' " + Environment.NewLine + _
    '            " and TSPL_MILK_REJECT_HEAD.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  " + Environment.NewLine + _
    '            " and TSPL_MILK_REJECT_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine + _
    '            " and TSPL_MILK_REJECT_DETAIL.VSP_CODE in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")" + Environment.NewLine + _
    '            " and TSPL_MILK_REJECT_DETAIL.Amount<=0 "
    '            dt = clsDBFuncationality.GetDataTable(qry)
    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                If clsCommon.MyMessageBoxShow("There are some Wrong Milk rejection having amount zero.Do You want to export these documents", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
    '                    transportSql.ExporttoExcelWithoutFilter(qry, "", "", Me)
    '                End If
    '                Exit Sub
    '            End If
    '        End If
    '        Generate_Bill_Payment_cycle_wise(strMCCCode, True)
    '    End If
    'End Sub

    'Sub Generate_Bill(ByVal strMCCCode As String, ByVal is_with_bill As Boolean)
    '    Dim trans As SqlTransaction = Nothing
    '    Try
    '        clsCommon.ProgressBarShow()
    '        If clsCommon.myLen(strMCCCode) <= 0 Then
    '            Throw New Exception("Please Select MCC To Generate Bill")
    '        End If
    '        If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
    '            Throw New Exception("Please Select VSP To Generate Bill")
    '        End If

    '        Dim Dt As DataTable
    '        Dim sQuery As String = String.Empty
    '        Dim Frm_Date As Date = CDate("01-" & MonthName(txtMonth.Value.Month, True) & "-" & txtMonth.Value.Year)
    '        Dim To_Date As Date
    '        If txtMonth.Value.Month = 1 Then
    '            To_Date = CDate("" & Date.DaysInMonth(txtMonth.Value.Year - 1, txtMonth.Value.Month) & "-" & MonthName(12, True) & "-" & txtMonth.Value.Year - 1)
    '        Else
    '            To_Date = CDate("" & Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month - 1) & "-" & MonthName(txtMonth.Value.Month - 1, True) & "-" & txtMonth.Value.Year)
    '        End If

    '        Dim Payment_Cycle_value As Integer = 0
    '        Dim Srn_No_List As New List(Of String)

    '        If (clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal) Then
    '            objCommonVar.SelectedUser = "All"
    '            objCommonVar.SelectedUser = clsCommon.GetMulcallString(txtVSP.arrValueMember)
    '        Else
    '            objCommonVar.SelectedUser = objCommonVar.CurrentUserCode
    '        End If
    '        Dim counter As Integer = 0
    '        For Each VSP As String In txtVSP.arrValueMember
    '            counter += 1
    '            sQuery = "select case when Pc_Type='Day' then PC_VALUE when PC_Type='Month' then PC_Value * " & Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & " end as Pc_Value from tspl_vendor_master inner join TSPL_PAYMENT_CYCLE_MASTER " _
    '            & " on tspl_vendor_master.PC_CODE=TSPL_PAYMENT_CYCLE_MASTER.PC_CODE where vendor_code='" & VSP & "'"
    '            Payment_Cycle_value = clsDBFuncationality.getSingleValue(sQuery, trans)
    '            trans = clsDBFuncationality.GetTransactin()
    '            For ix As Integer = 1 To Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) Step Payment_Cycle_value
    '                If Payment_Cycle_value <= Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) Then
    '                    Srn_No_List.Clear()
    '                    Frm_Date = To_Date.AddDays(1)
    '                    To_Date = To_Date.AddDays(Payment_Cycle_value)
    '                    If Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) - To_Date.Day <= 1 Then
    '                        To_Date = To_Date.AddDays(1)
    '                    End If
    '                    If clsCommon.myCDate(To_Date) > clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)) Then
    '                        Exit For
    '                    End If
    '                    If is_with_bill Then
    '                        sQuery = "select Distinct TSPL_MILK_SRN_Head. DOC_CODE from TSPL_MILK_SRN_Head inner join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_Head.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE " _
    '                           & " left join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE " _
    '                           & " and TSPL_MILK_PURCHASE_INVOICE_DETAIL.item_code=TSPL_MILK_srn_DETAIL.Item_Code where  SRN_CODE is null and Coalesce(Is_Incentive_Created,'N')='N' and " _
    '                           & " VSP_coDE='" & VSP & "' AND TSPL_MILK_SRN_Head.mcc_coDE='" & strMCCCode & "' AND convert(date,DOC_DATE,103) >=convert(date,'" & Frm_Date & "',103) " _
    '                           & " and convert(date,DOC_DATE,103) <=convert(date,'" & To_Date & "',103)"
    '                    Else
    '                        sQuery = "select Distinct TSPL_MILK_SRN_Head. DOC_CODE from TSPL_MILK_SRN_Head inner join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_Head.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE " _
    '                           & " left join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE " _
    '                           & " and TSPL_MILK_PURCHASE_INVOICE_DETAIL.item_code=TSPL_MILK_srn_DETAIL.Item_Code where  SRN_CODE is Not null and Coalesce(Is_Incentive_Created,'N')='N' and " _
    '                           & " VSP_coDE='" & VSP & "' AND TSPL_MILK_SRN_Head.mcc_coDE='" & strMCCCode & "' AND convert(date,DOC_DATE,103) >=convert(date,'" & Frm_Date & "',103) " _
    '                           & " and convert(date,DOC_DATE,103) <=convert(date,'" & To_Date & "',103)"
    '                    End If
    '                    Dt = clsDBFuncationality.GetDataTable(sQuery, trans)
    '                    If Dt.Rows.Count > 0 Then
    '                        Dim strNo As String = ""
    '                        For Each row As DataRow In Dt.Rows()
    '                            Srn_No_List.Add(row("Doc_Code"))
    '                            strNo += clsCommon.myCstr(row("Doc_Code")) + ","
    '                        Next
    '                        clsCommon.ProgressBarUpdate("VSP-" & counter & "/" & txtVSP.arrValueMember.Count & " VSP-" + VSP + " Payment Cycle-" + clsCommon.myCstr(ix) + "/" + clsCommon.myCstr(Payment_Cycle_value) + " SRN-" + strNo)
    '                        SelectMilkSRNItemsForVspPayment(strMCCCode, Srn_No_List, VSP, Frm_Date, To_Date, is_with_bill, trans)
    '                        sQuery = "update tspl_Milk_srn_Head set is_Incentive_Created='Y' where Doc_Code in (" + clsCommon.GetMulcallString(Srn_No_List) + ")"
    '                        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
    '                    End If
    '                End If
    '            Next
    '            trans.Commit()
    '            Frm_Date = CDate("01-" & MonthName(txtMonth.Value.Month, True) & "-" & txtMonth.Value.Year)
    '            If txtMonth.Value.Month = 1 Then
    '                To_Date = CDate("" & Date.DaysInMonth(txtMonth.Value.Year - 1, txtMonth.Value.Month) & "-" & MonthName(12, True) & "-" & txtMonth.Value.Year - 1)
    '            Else
    '                To_Date = CDate("" & Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month - 1) & "-" & MonthName(txtMonth.Value.Month - 1, True) & "-" & txtMonth.Value.Year)
    '            End If
    '        Next
    '        clsCommon.ProgressBarHide()
    '    Catch ex As Exception
    '        clsCommon.ProgressBarHide()
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    'Sub Generate_Bill_Payment_cycle_wise(ByVal strMCCCode As String, ByVal is_with_bill As Boolean)
    '    Dim trans As SqlTransaction = Nothing
    '    Try
    '        clsCommon.ProgressBarPercentShow()
    '        If clsCommon.myLen(strMCCCode) <= 0 Then
    '            Throw New Exception("Please Select MCC To Generate Bill")
    '        End If
    '        If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
    '            Throw New Exception("Please Select VSP To Generate Bill")
    '        End If
    '        '' get MPPayment setting
    '        Dim IsMP As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isFarmerPaymentCycle, clsFixedParameterCode.isFarmerPaymentCycle, trans))
    '        If IsMP = 1 Then
    '            Dim arrVSP As ArrayList = clsMilkPurchaseInvoiceMCC.GetVSPWithoutMPData(txtFromDate.Value, txtToDate.Value, strMCCCode, txtVSP.arrValueMember, trans)
    '            If Not arrVSP Is Nothing AndAlso arrVSP.Count > 0 Then
    '                Dim strVSP As String = clsCommon.GetMulcallStringWithComma(arrVSP)
    '                If clsCommon.MyMessageBoxShow("Some of the VSP dont have MP Collection Data. Still do you want to proceed Bill Generation ?", "Validate MP Data", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
    '                    Throw New Exception("VSP without MP Data: " & strVSP)
    '                End If
    '            End If
    '        End If

    '        Dim dt As DataTable
    '        Dim qry As String = String.Empty
    '        Dim Payment_Cycle_value As Integer = 0
    '        Dim Srn_No_List As New List(Of String)

    '        If (clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal) Then
    '            objCommonVar.SelectedUser = "All"
    '            objCommonVar.SelectedUser = clsCommon.GetMulcallString(txtVSP.arrValueMember)
    '        Else
    '            objCommonVar.SelectedUser = objCommonVar.CurrentUserCode
    '        End If

    '        Dim counter As Integer = 0
    '        If clsCommon.CompairString(lblPaymentType.Text, "Week") = CompairStringResult.Equal Then
    '            If Not txtFromDate.Value.DayOfWeek = IIf(lblPaymentType.Tag = 1, DayOfWeek.Sunday, IIf(lblPaymentType.Tag = 2, DayOfWeek.Monday, IIf(lblPaymentType.Tag = 3, DayOfWeek.Tuesday, IIf(lblPaymentType.Tag = 4, DayOfWeek.Wednesday, IIf(lblPaymentType.Tag = 5, DayOfWeek.Thursday, IIf(lblPaymentType.Tag = 6, DayOfWeek.Friday, DayOfWeek.Saturday)))))) Then
    '                Throw New Exception("From Date Day of week should be " + IIf(lblPaymentType.Tag = 1, "Sunday", IIf(lblPaymentType.Tag = 2, "Monday", IIf(lblPaymentType.Tag = 3, "Tuesday", IIf(lblPaymentType.Tag = 4, "Wednesday", IIf(lblPaymentType.Tag = 5, "Thursday", IIf(lblPaymentType.Tag = 6, "Friday", "Saturday")))))))
    '            End If
    '        Else
    '            qry = "select case when Pc_Type='Day' then PC_VALUE when PC_Type='Month' then PC_Value * " & Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & " end " _
    '        & " as Pc_Value from tspl_Mcc_master inner join TSPL_PAYMENT_CYCLE_MASTER  on tspl_Mcc_master.payment_cycle=TSPL_PAYMENT_CYCLE_MASTER.PC_CODE where Mcc_code='" & strMCCCode & "'"
    '            Payment_Cycle_value = clsDBFuncationality.getSingleValue(qry, trans)
    '            If Payment_Cycle_value <= 0 Then
    '                Throw New Exception("Please Set Payment Cycle on Mcc [" & strMCCCode & "]")
    '            End If
    '        End If

    '        GeneratePONumber(strMCCCode, trans)
    '        If txtToDate.Value.Date > clsCommon.GETSERVERDATE(trans).Date Then
    '            Throw New Exception("To Date is Greate than Server Date")
    '        End If

    '        Dim arrMCC As New ArrayList
    '        arrMCC.Add(strMCCCode)
    '        Dim dtVSP As DataTable = clsDBFuncationality.GetDataTable(VSPQry(arrMCC, txtVSP.arrValueMember))
    '        If dtVSP IsNot Nothing AndAlso dtVSP.Rows.Count > 0 Then
    '            'For Each VSP As String In txtVSP.arrValueMember
    '            For Each drVSP As DataRow In dtVSP.Rows
    '                Dim VSP As String = clsCommon.myCstr(drVSP("Code"))
    '                counter += 1
    '                trans = clsDBFuncationality.GetTransactin()
    '                Try
    '                    Srn_No_List.Clear()
    '                    'aaaaaaaaaaaaaaaaaa()
    '                    qry = "select Distinct TSPL_MILK_SRN_Head. DOC_CODE from TSPL_MILK_SRN_Head inner join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_Head.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE " _
    '                           & " left join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE " _
    '                           & " and TSPL_MILK_PURCHASE_INVOICE_DETAIL.item_code=TSPL_MILK_srn_DETAIL.Item_Code where  Coalesce(Is_Incentive_Created,'N')='N' and " _
    '                           & " VSP_coDE='" & VSP & "' AND TSPL_MILK_SRN_Head.mcc_coDE='" & strMCCCode & "' "
    '                    If isPickPendingMilkSRNinNextPaymentCycle Then
    '                        ''Comment by balwinder on 24/Jan/2017 at  gajraula becuase pick Zero amount SRN in Milk Purchase Invoice
    '                        'qry += " and TSPL_MILK_SRN_DETAIL.AMOUNT>0 " 
    '                    Else
    '                        qry += " AND convert(date,DOC_DATE,103) >=convert(date,'" & txtFromDate.Value.Date & "',103) "
    '                    End If
    '                    qry += " and convert(date,DOC_DATE,103) <=convert(date,'" & txtToDate.Value.Date & "',103)"
    '                    If is_with_bill Then
    '                        qry += "and SRN_CODE is null "
    '                    Else
    '                        qry += "and SRN_CODE is Not null "
    '                    End If

    '                    dt = clsDBFuncationality.GetDataTable(qry, trans)
    '                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                        Dim strNo As String = ""
    '                        For Each row As DataRow In dt.Rows()
    '                            Srn_No_List.Add(row("Doc_Code"))
    '                            strNo += clsCommon.myCstr(row("Doc_Code")) + ","
    '                        Next
    '                        clsCommon.ProgressBarPercentUpdate(((counter - 1) * 100 / txtVSP.arrValueMember.Count), "MCC-"+strMCCCode+" VSP-" & counter & "/" & txtVSP.arrValueMember.Count & " VSP-" + VSP + " SRN-" + strNo)
    '                        Dim strr As String = clsDBFuncationality.getSingleValue("select coalesce(vsp_farmer_billing,0) FROM TSPL_Vendor_master where vendor_Code='" & VSP & "'", trans)
    '                        If (Formcode = clsUserMgtCode.MilkVSPPayment Or Formcode = clsUserMgtCode.MPBillGeneration Or strr <> "1") Then
    '                            SelectMilkSRNItemsForVspPayment(strMCCCode, Srn_No_List, VSP, txtFromDate.Value.Date, txtToDate.Value.Date, is_with_bill, trans)
    '                        Else
    '                            SelectMilkSRNItemsForMPPayment(Srn_No_List, VSP, txtFromDate.Value.Date, txtToDate.Value.Date, is_with_bill, trans)
    '                        End If
    '                        qry = "update tspl_Milk_srn_Head set is_Incentive_Created='Y' where Doc_Code  in (" + clsCommon.GetMulcallString(Srn_No_List) + ")"
    '                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '                        CreateAssetEMIOFVSP(strMCCCode, VSP, trans)
    '                        CreateVSPDebitNoteOfSecurityDeduction(VSP, txtToDate.Value, strMCCCode, trans)
    '                        CreateDebitNoteForAdvanceInterestAmt(strMCCCode, VSP, trans) ''by balwinder on 27/04/2017
    '                        CreateVSPDebitNoteOfTIP(VSP, txtToDate.Value, strMCCCode, trans) ''By Balwinder on 16/12/2019
    '                    End If

    '                    qry = " select DOC_CODE from ( select TSPL_MILK_REJECT_DETAIL.DOC_CODE  from TSPL_MILK_REJECT_DETAIL" + Environment.NewLine + _
    '                    " left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODE=TSPL_MILK_REJECT_DETAIL.DOC_CODE " + Environment.NewLine + _
    '                    " where " + Environment.NewLine + _
    '                    " TSPL_MILK_REJECT_HEAD.POSTED=1 and TSPL_MILK_REJECT_DETAIL.Defaulter in ('Transporter','VSP') and TSPL_MILK_REJECT_HEAD.MCC_CODE ='" + strMCCCode + "' and TSPL_MILK_REJECT_DETAIL.VSP_CODE in ('" + VSP + "') and  " + Environment.NewLine + _
    '                    " TSPL_MILK_REJECT_HEAD.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and not exists (select 1 from TSPL_VENDOR_INVOICE_HEAD where TSPL_VENDOR_INVOICE_HEAD.RefDocType='MILK-REJ' and TSPL_VENDOR_INVOICE_HEAD.RefDocNo=TSPL_MILK_REJECT_DETAIL.DOC_CODE and TSPL_VENDOR_INVOICE_HEAD.Ref_SNo=TSPL_MILK_REJECT_DETAIL.SAMPLE_NO))xx group by DOC_CODE"
    '                    dt = clsDBFuncationality.GetDataTable(qry, trans)
    '                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                        For Each dr As DataRow In dt.Rows
    '                            clsMilkRejectHead.CreateDebitNoteForRejection(txtFromDate.Value, txtToDate.Value, clsCommon.myCstr(dr("DOC_CODE")), trans)
    '                        Next
    '                    End If
    '                    'Throw New Exception("Balwinder singh premi")
    '                    trans.Commit()
    '                Catch ex As Exception
    '                    trans.Rollback()
    '                    Throw New Exception(ex.Message)
    '                End Try
    '            Next
    '        End If
    '        counter = 0

    '        clsCommon.ProgressBarPercentHide()
    '    Catch ex As Exception
    '        clsCommon.ProgressBarPercentHide()
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    'Sub GeneratePONumber(ByVal strMCCCode As String, ByVal tran As SqlTransaction)
    '    Dim qry As String = "select TSPL_MILK_SRN_HEAD.DOC_CODE,TSPL_MILK_SRN_HEAD.DOC_DATE,TSPL_MILK_SRN_HEAD.MCC_CODE " + Environment.NewLine +
    '    "from TSPL_MILK_SRN_DETAIL" + Environment.NewLine +
    '    "left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE =TSPL_MILK_SRN_DETAIL.DOC_CODE " + Environment.NewLine +
    '    "left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_MILK_SRN_HEAD.mcc_code" + Environment.NewLine +
    '    "where TSPL_MILK_SRN_Head.MCC_CODE='" + strMCCCode + "' and isnull(Purchase_Order_No,'')='' and isnull( TSPL_MILK_SRN_head.Against_Reject_No,'')='' and  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) Between convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103) and convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "',103) " + Environment.NewLine +
    '    "and isnull(TSPL_MCC_MASTER.Failed_Sample_Apply,0)=1 and TSPL_MILK_SRN_DETAIL.FAT_PER>=TSPL_MCC_MASTER.Failed_Sample_FAT and TSPL_MILK_SRN_DETAIL.SNF_PER>=TSPL_MCC_MASTER.Failed_Sample_SNF order by TSPL_MILK_SRN_HEAD.DOC_DATE "
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
    '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '        For Each dr As DataRow In dt.Rows
    '            Dim strCode As String = clsERPFuncationality.GetNextCode(tran, clsCommon.myCDate(dr("DOC_DATE")), clsDocType.MilkPO, "", clsCommon.myCstr(dr("MCC_CODE")), False, True, False, False, objCommonVar.ShowMCCFinderInPaymentProcess)
    '            If clsCommon.myLen(strCode) <= 0 Then
    '                Throw New Exception("Error in code generation of Milk purchae order")
    '            End If
    '            Dim coll As New Hashtable()
    '            clsCommon.AddColumnsForChange(coll, "Purchase_Order_No", strCode)
    '            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_HEAD", OMInsertOrUpdate.Update, "DOC_CODE='" + clsCommon.myCstr(dr("DOC_CODE")) + "'", tran)
    '            coll = Nothing
    '        Next
    '        dt = Nothing
    '    End If

    'End Sub

    Private Sub CreateDebitNoteForAdvanceInterestAmt(ByVal strMCCCode As String, ByVal strVSPCode As String, ByVal trans As SqlTransaction)
        Dim qry As String = clsAPInvoiceAdvanceInterest.GetAdvancePaymentQry("'" + strVSPCode + "'", Nothing, txtToDate.Value, False, trans)
        Dim dtMain As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dtMain IsNot Nothing AndAlso dtMain.Rows.Count > 0 Then
            qry = "select PC_VALUE from TSPL_MCC_MASTER  left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle where MCC_Code='" + strMCCCode + "'"
            Dim dblPCDays As Integer = clsDBFuncationality.getSingleValue(qry, trans)


            Dim objVendorInvHead As New clsVedorInvoiceHead()
            'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            objVendorInvHead.Vendor_Code = strVSPCode
            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(strVSPCode, trans)
            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
            objVendorInvHead.Invoice_Type = "AP"
            objVendorInvHead.Vendor_Invoice_Date = txtToDate.Value
            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(strMCCCode, trans) 'obj.MCC_CODE
            'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE
            objVendorInvHead.Description = "AP Debit Note Against Installment Amount of VSP : " & objVendorInvHead.Vendor_Name & "(" + objVendorInvHead.Vendor_Code + ")"
            'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
            End If

            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
            ''objVendorInvHead.PO_Number = obj.p
            objVendorInvHead.isDeduction = 1

            '' ''added by priti
            ''objVendorInvHead.RefDocType = clsCommon.myCstr(cmbRefType.SelectedValue)
            ''objVendorInvHead.RefDocNo = txtRefDocNo.Text
            '' '' priti ends here
            'objVendorInvHead.Order_No = txtOrderNo.Text
            ' objVendorInvHead.Total_Tax = 0

            objVendorInvHead.On_Hold = False
            'Dim srndate As String = ""
            'Dim srncode As String = ""
            'Dim Vlc_Code As String = ""
            'Dim Vlc_Name As String = ""
            'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
            '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
            '        Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
            '        Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
            '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"))
            '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
            '        Vlc_Code = IIf(Vlc_Code = "", Dt_SRN.Rows(0).Item("VLC_Code").ToString, Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
            '        Vlc_Name = IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
            '    End If
            'Next



            'objVendorInvHead.Description = "VSP : " + obj.VSP_CODE + "/" + vendor_name + "VLC : " + Vlc_Code + "/" + Vlc_Name + " .Against PI Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
            'objVendorInvHead.Tax_Calculation_Type = Nothing
            'objVendorInvHead.Tax_Group = Nothing
            'If (clsCommon.myLen(obj.TAX1) > 0) Then
            '    objVendorInvHead.TAX1 = obj.TAX1
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
            '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
            '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
            '    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
            '    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX2) > 0) Then
            '    objVendorInvHead.TAX2 = obj.TAX2
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
            '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
            '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
            '    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
            '    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX3) > 0) Then
            '    objVendorInvHead.TAX3 = obj.TAX3
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
            '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
            '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
            '    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
            '    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX4) > 0) Then
            '    objVendorInvHead.TAX4 = obj.TAX4
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
            '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
            '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
            '    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
            '    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX5) > 0) Then
            '    objVendorInvHead.TAX5 = obj.TAX5
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
            '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
            '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
            '    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
            '    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX6) > 0) Then
            '    objVendorInvHead.TAX6 = obj.TAX6
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
            '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
            '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
            '    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
            '    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX7) > 0) Then
            '    objVendorInvHead.TAX7 = obj.TAX7
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
            '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
            '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
            '    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
            '    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX8) > 0) Then
            '    objVendorInvHead.TAX8 = obj.TAX8
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
            '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
            '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
            '    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
            '    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX9) > 0) Then
            '    objVendorInvHead.TAX9 = obj.TAX9
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
            '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
            '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
            '    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
            '    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX10) > 0) Then
            '    objVendorInvHead.TAX10 = obj.TAX10
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
            '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
            '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
            '    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
            '    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
            'End If

            'objVendorInvHead.Terms_Code = obj.Terms_Code
            'objVendorInvHead.Terms_Description = obj.TermsName
            objVendorInvHead.Due_Date = txtToDate.Value

            'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
            'objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE
            objVendorInvHead.RefDocType = "VSP-INT"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, strMCCCode, trans)
                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, strMCCCode, trans)
                End If
            End If
            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If

            'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

            'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
            'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
            'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

            'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
            'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
            'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

            'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
            'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
            'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

            'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
            'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
            'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

            'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
            'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
            'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

            'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
            'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
            'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

            'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
            'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
            'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

            'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
            'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
            'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

            'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
            'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
            'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

            'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
            'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
            'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
            Dim ii As Integer = 0
            Dim isFirstTime As Boolean = True
            ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
            'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
            objVendorInvHead.Total_Landed_Amt = 0

            objVendorInvHead.ArrAdvanceInterest = New List(Of clsAPInvoiceAdvanceInterest)()

            For Each dr As DataRow In dtMain.Rows

                ''Set Installment amount
                Dim objAEMI As New clsAPInvoiceAdvanceInterest
                objAEMI.Payment_No = clsCommon.myCstr(dr("Payment_No"))
                objAEMI.Interest_Amount = Math.Round((clsCommon.myCdbl(dr("Payment_Amount")) * clsCommon.myCdbl(dr("Interest_Rate")) * dblPCDays) / (365 * 100), 2, MidpointRounding.ToEven)  ''BHA/20/06/18-000059 By balwinder change 360 days to 365 
                objVendorInvHead.ArrAdvanceInterest.Add(objAEMI)
                ''end of Set Installment amount

                ''Set AP Invvoice Detail Table




                Dim objVendorInvDetail As New clsVedorInvoiceDetail()

                Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select Code,Description,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Default_Advance_Interest=1", trans)
                If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                    Throw New Exception("Please set default Advance Interest in deduction Master")
                End If
                objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                objVendorInvDetail.Deduction_Name = clsCommon.myCstr(dtDed.Rows(0)("Description"))

                Dim strInvCtrlAC As String = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, strMCCCode, trans)
                If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                    Throw New Exception("Please set GL Account Code for deduction code " + objVendorInvDetail.Deduction_Code)
                End If


                ii = ii + 1
                objVendorInvDetail.Detail_Line_No = ii
                objVendorInvDetail.GL_Account_Code = strInvCtrlAC
                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(strInvCtrlAC, trans)
                objVendorInvDetail.Amount = objAEMI.Interest_Amount
                objVendorInvDetail.Discount_Per = 0
                objVendorInvDetail.Discount = 0
                objVendorInvDetail.Amount_less_Discount = objVendorInvDetail.Amount
                objVendorInvDetail.Total_Tax = 0
                objVendorInvDetail.Total_Amount = objVendorInvDetail.Amount
                objVendorInvDetail.Landed_Amount = objVendorInvDetail.Amount
                ''End of Set AP Invvoice Detail Table

                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                End If

                ''Set AP Invvoice Header Table
                objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount
                objVendorInvHead.Discount_Base += objVendorInvDetail.Amount
                objVendorInvHead.Discount_Amount += 0
                objVendorInvHead.Amount_Less_Discount += objVendorInvDetail.Amount
                objVendorInvHead.Document_Total += objVendorInvDetail.Amount
                objVendorInvHead.Balance_Amt += objVendorInvDetail.Amount
                ''End of Set AP Invvoice Header Table
            Next


            objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
            If objVendorInvHead.Empty_Amount > 0 Then
                If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                    Throw New Exception("Please set Inventory Control Empties")
                End If
                objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
            End If


            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                Throw New Exception("No GL Account Found For AP Invoice")
            End If
            ''multicurrency
            'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
            'objVendorInvHead.ConvRate = 1
            objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            ''end multicurrency
            '' skip entry of installment if amount i zero
            If objVendorInvHead.Document_Total > 0 Then
                objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, txtToDate.Value)
            End If
            objVendorInvHead = Nothing
        End If
    End Sub

    Public Shared Sub CreateVSPDebitNoteOfTIP(ByVal strVSPCode As String, ByVal dtDocDate As DateTime, ByVal strMCC As String, ByVal trans As SqlTransaction) ''VIJ/11/12/19-000118 by balwinder on 27/01/2019
        Dim qry As String = "select max(TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE) as DOC_CODE, sum(TSPL_MILK_SRN_DETAIL.TIP_Amount) as TIP_Amount from TSPL_MILK_PURCHASE_INVOICE_DETAIL" + Environment.NewLine +
        "left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD  on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE" + Environment.NewLine +
        "left outer join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE" + Environment.NewLine +
        "where TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE='" + strVSPCode + "' and TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE='" + strMCC + "' and convert(date, TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)='" + clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy") + "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            Dim dblAmount As Decimal = clsCommon.myCdbl(dt1.Rows(0)("TIP_Amount"))
            If dblAmount > 0 Then
                Dim strVendor As String = strVSPCode
                Dim dt As DataTable
                Dim objVendorInvHead As New clsVedorInvoiceHead()
                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                objVendorInvHead.isDeduction = 1
                objVendorInvHead.Security = 1


                'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
                objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy")
                objVendorInvHead.Vendor_Code = strVendor
                objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(strVendor, trans)
                objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                objVendorInvHead.Invoice_Type = "AP"
                objVendorInvHead.Vendor_Invoice_Date = dtDocDate
                objVendorInvHead.loc_code = clsLocation.GetSegmentCode(strMCC, trans) 'obj.MCC_CODE
                'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE
                objVendorInvHead.Description = "AP Debit Note Against TIP Deduction"
                'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                    Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                End If

                objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                ''objVendorInvHead.PO_Number = obj.p

                '' ''added by priti
                objVendorInvHead.RefDocType = "TIP-DED"
                objVendorInvHead.RefDocNo = clsCommon.myCstr(dt1.Rows(0)("DOC_CODE"))
                'objVendorInvHead.Ref_SNo = ""
                '' '' priti ends here
                'objVendorInvHead.Order_No = txtOrderNo.Text
                ' objVendorInvHead.Total_Tax = 0

                objVendorInvHead.On_Hold = False
                'Dim srndate As String = ""
                'Dim srncode As String = ""
                'Dim Vlc_Code As String = ""
                'Dim Vlc_Name As String = ""
                'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
                '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
                '        Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
                '        Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
                '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"))
                '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
                '        Vlc_Code = IIf(Vlc_Code = "", Dt_SRN.Rows(0).Item("VLC_Code").ToString, Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
                '        Vlc_Name = IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
                '    End If
                'Next



                'objVendorInvHead.Description = "VSP : " + obj.VSP_CODE + "/" + vendor_name + "VLC : " + Vlc_Code + "/" + Vlc_Name + " .Against PI Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
                'objVendorInvHead.Tax_Calculation_Type = Nothing
                'objVendorInvHead.Tax_Group = Nothing
                'If (clsCommon.myLen(obj.TAX1) > 0) Then
                '    objVendorInvHead.TAX1 = obj.TAX1
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
                '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
                '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.MCC_CODE, trans)
                '    End If
                '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
                '    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
                '    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX2) > 0) Then
                '    objVendorInvHead.TAX2 = obj.TAX2
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
                '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
                '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.MCC_CODE, trans)
                '    End If
                '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
                '    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
                '    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX3) > 0) Then
                '    objVendorInvHead.TAX3 = obj.TAX3
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
                '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
                '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.MCC_CODE, trans)
                '    End If
                '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
                '    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
                '    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX4) > 0) Then
                '    objVendorInvHead.TAX4 = obj.TAX4
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
                '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
                '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.MCC_CODE, trans)
                '    End If
                '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
                '    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
                '    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX5) > 0) Then
                '    objVendorInvHead.TAX5 = obj.TAX5
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
                '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
                '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.MCC_CODE, trans)

                '    End If
                '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
                '    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
                '    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX6) > 0) Then
                '    objVendorInvHead.TAX6 = obj.TAX6
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
                '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
                '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.MCC_CODE, trans)
                '    End If
                '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
                '    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
                '    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX7) > 0) Then
                '    objVendorInvHead.TAX7 = obj.TAX7
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
                '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
                '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.MCC_CODE, trans)

                '    End If
                '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
                '    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
                '    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX8) > 0) Then
                '    objVendorInvHead.TAX8 = obj.TAX8
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
                '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
                '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.MCC_CODE, trans)
                '    End If
                '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
                '    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
                '    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX9) > 0) Then
                '    objVendorInvHead.TAX9 = obj.TAX9
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
                '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
                '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.MCC_CODE, trans)
                '    End If
                '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
                '    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
                '    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX10) > 0) Then
                '    objVendorInvHead.TAX10 = obj.TAX10
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
                '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
                '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.MCC_CODE, trans)
                '    End If
                '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
                '    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
                '    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
                'End If

                'objVendorInvHead.Terms_Code = obj.Terms_Code
                'objVendorInvHead.Terms_Description = obj.TermsName
                objVendorInvHead.Due_Date = dtDocDate

                'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
                'objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE

                dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, strMCC, trans)
                    If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                        objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, strMCC, trans)
                    End If
                End If
                If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                    Throw New Exception("Please set the vendor payable Account")
                End If

                'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

                'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
                'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
                'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

                'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
                'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
                'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

                'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
                'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
                'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

                'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
                'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
                'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

                'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
                'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
                'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

                'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
                'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
                'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

                'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
                'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
                'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

                'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
                'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
                'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

                'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
                'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
                'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

                'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
                'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
                'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


                objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                Dim ii As Integer = 0
                Dim isFirstTime As Boolean = True
                ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
                'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
                objVendorInvHead.Total_Landed_Amt = 0

                objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()





                ''Set AP Invvoice Detail Table
                Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select Code,Description,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Default_TIP=1", trans)
                If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                    Throw New Exception("Please set default TIP deduction code")
                End If
                objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                objVendorInvDetail.Deduction_Name = clsCommon.myCstr(dtDed.Rows(0)("Description"))

                Dim strInvCtrlAC As String = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, strMCC, trans)
                If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                    Throw New Exception("Please set GL Account Code for deduction code " + objVendorInvDetail.Deduction_Code)
                End If
                objVendorInvDetail.GL_Account_Code = strInvCtrlAC
                ii = ii + 1
                objVendorInvDetail.Detail_Line_No = ii
                objVendorInvDetail.GL_Account_Code = strInvCtrlAC
                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(strInvCtrlAC, trans)
                objVendorInvDetail.Amount = dblAmount

                objVendorInvDetail.Discount_Per = 0
                objVendorInvDetail.Discount = 0
                objVendorInvDetail.Amount_less_Discount = dblAmount
                objVendorInvDetail.Total_Tax = 0
                objVendorInvDetail.Total_Amount = dblAmount
                objVendorInvDetail.Landed_Amount = dblAmount
                ''End of Set AP Invvoice Detail Table

                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                End If

                ''Set AP Invvoice Header Table
                objVendorInvHead.Total_Landed_Amt += dblAmount
                objVendorInvHead.Discount_Base += dblAmount
                objVendorInvHead.Discount_Amount += 0
                objVendorInvHead.Amount_Less_Discount += dblAmount
                objVendorInvHead.Document_Total += dblAmount
                objVendorInvHead.Balance_Amt += dblAmount
                ''End of Set AP Invvoice Header Table

                objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                If objVendorInvHead.Empty_Amount > 0 Then
                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                        Throw New Exception("Please set Inventory Control Empties")
                    End If
                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                End If
                If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                    Throw New Exception("No GL Account Found For AP Invoice")
                End If
                ''multicurrency
                'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
                'objVendorInvHead.ConvRate = 1
                objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy")
                ''end multicurrency

                objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, dtDocDate)
            End If
        End If


    End Sub

    Public Shared Sub CreateVSPDebitNoteOfSecurityDeduction(ByVal strVSPCode As String, ByVal dtDocDate As DateTime, ByVal strMCC As String, ByVal trans As SqlTransaction)
        Dim dblAmount As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Security_Deduction_Amount from TSPL_VENDOR_MASTER where Vendor_Code='" + strVSPCode + "'", trans))
        If dblAmount > 0 Then
            Dim strVendor As String = strVSPCode
            Dim dt As DataTable
            Dim objVendorInvHead As New clsVedorInvoiceHead()
            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
            objVendorInvHead.isDeduction = 1
            objVendorInvHead.Security = 1
            Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code from TSPL_DEDUCTION_MASTER  where Is_Default_Security_Deduction=1", trans)
            If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                Throw New Exception("Please set default Security deduction code")
            End If
            objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))

            'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy")
            objVendorInvHead.Vendor_Code = strVendor
            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(strVendor, trans)
            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
            objVendorInvHead.Invoice_Type = "AP"
            objVendorInvHead.Vendor_Invoice_Date = dtDocDate
            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(strMCC, trans) 'obj.MCC_CODE
            'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE
            objVendorInvHead.Description = "AP Debit Note Against VSP security Deduction"
            'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
            End If

            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
            ''objVendorInvHead.PO_Number = obj.p

            '' ''added by priti
            objVendorInvHead.RefDocType = "SEC-DED"
            'objVendorInvHead.RefDocNo = ""
            'objVendorInvHead.Ref_SNo = ""
            '' '' priti ends here
            'objVendorInvHead.Order_No = txtOrderNo.Text
            ' objVendorInvHead.Total_Tax = 0

            objVendorInvHead.On_Hold = False
            'Dim srndate As String = ""
            'Dim srncode As String = ""
            'Dim Vlc_Code As String = ""
            'Dim Vlc_Name As String = ""
            'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
            '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
            '        Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
            '        Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
            '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"))
            '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
            '        Vlc_Code = IIf(Vlc_Code = "", Dt_SRN.Rows(0).Item("VLC_Code").ToString, Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
            '        Vlc_Name = IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
            '    End If
            'Next



            'objVendorInvHead.Description = "VSP : " + obj.VSP_CODE + "/" + vendor_name + "VLC : " + Vlc_Code + "/" + Vlc_Name + " .Against PI Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
            'objVendorInvHead.Tax_Calculation_Type = Nothing
            'objVendorInvHead.Tax_Group = Nothing
            'If (clsCommon.myLen(obj.TAX1) > 0) Then
            '    objVendorInvHead.TAX1 = obj.TAX1
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
            '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
            '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
            '    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
            '    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX2) > 0) Then
            '    objVendorInvHead.TAX2 = obj.TAX2
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
            '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
            '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
            '    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
            '    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX3) > 0) Then
            '    objVendorInvHead.TAX3 = obj.TAX3
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
            '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
            '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
            '    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
            '    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX4) > 0) Then
            '    objVendorInvHead.TAX4 = obj.TAX4
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
            '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
            '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
            '    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
            '    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX5) > 0) Then
            '    objVendorInvHead.TAX5 = obj.TAX5
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
            '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
            '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
            '    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
            '    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX6) > 0) Then
            '    objVendorInvHead.TAX6 = obj.TAX6
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
            '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
            '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
            '    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
            '    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX7) > 0) Then
            '    objVendorInvHead.TAX7 = obj.TAX7
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
            '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
            '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
            '    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
            '    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX8) > 0) Then
            '    objVendorInvHead.TAX8 = obj.TAX8
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
            '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
            '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
            '    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
            '    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX9) > 0) Then
            '    objVendorInvHead.TAX9 = obj.TAX9
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
            '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
            '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
            '    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
            '    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX10) > 0) Then
            '    objVendorInvHead.TAX10 = obj.TAX10
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
            '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
            '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
            '    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
            '    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
            'End If

            'objVendorInvHead.Terms_Code = obj.Terms_Code
            'objVendorInvHead.Terms_Description = obj.TermsName
            objVendorInvHead.Due_Date = dtDocDate

            'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
            'objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE

            dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, strMCC, trans)
                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, strMCC, trans)
                End If
            End If
            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If

            'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

            'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
            'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
            'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

            'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
            'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
            'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

            'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
            'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
            'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

            'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
            'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
            'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

            'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
            'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
            'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

            'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
            'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
            'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

            'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
            'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
            'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

            'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
            'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
            'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

            'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
            'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
            'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

            'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
            'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
            'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
            Dim ii As Integer = 0
            Dim isFirstTime As Boolean = True
            ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
            'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
            objVendorInvHead.Total_Landed_Amt = 0

            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()



            ''Set AP Invvoice Detail Table

            Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Deduction_ACCOUNT"))
            If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                Throw New Exception("Please set Deduction Account for Vendor Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Acct_Set_Code")))
            End If
            strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, strMCC, trans)




            ii = ii + 1
            objVendorInvDetail.Detail_Line_No = ii
            objVendorInvDetail.GL_Account_Code = strInvCtrlAC
            objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(strInvCtrlAC, trans)
            objVendorInvDetail.Amount = dblAmount

            objVendorInvDetail.Discount_Per = 0
            objVendorInvDetail.Discount = 0
            objVendorInvDetail.Amount_less_Discount = dblAmount
            objVendorInvDetail.Total_Tax = 0
            objVendorInvDetail.Total_Amount = dblAmount
            objVendorInvDetail.Landed_Amount = dblAmount
            ''End of Set AP Invvoice Detail Table

            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                objVendorInvHead.Arr.Add(objVendorInvDetail)
            End If

            ''Set AP Invvoice Header Table
            objVendorInvHead.Total_Landed_Amt += dblAmount
            objVendorInvHead.Discount_Base += dblAmount
            objVendorInvHead.Discount_Amount += 0
            objVendorInvHead.Amount_Less_Discount += dblAmount
            objVendorInvHead.Document_Total += dblAmount
            objVendorInvHead.Balance_Amt += dblAmount
            ''End of Set AP Invvoice Header Table

            objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
            If objVendorInvHead.Empty_Amount > 0 Then
                If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                    Throw New Exception("Please set Inventory Control Empties")
                End If
                objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
            End If
            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                Throw New Exception("No GL Account Found For AP Invoice")
            End If
            ''multicurrency
            'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
            'objVendorInvHead.ConvRate = 1
            objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy")
            ''end multicurrency

            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, dtDocDate)
        End If
    End Sub

    'Public Sub SelectMilkSRNItemsForVspPayment(ByVal strMCCCode As String, ByVal strSRN_No As List(Of String), ByVal Vsp_Name As String, ByVal frm_date As Date, ByVal End_date As Date, ByVal Is_With_Bill As Boolean, ByVal trans As SqlTransaction)
    '    Dim obj_SRN As New clsMilkSRNMCC
    '    Dim frm As New frmMILKPendingSRN()
    '    frm.VendorCode = Vsp_Name
    '    frm.Frm_date = frm_date
    '    frm.To_date = End_date
    '    Dim StrDoc As New List(Of String)
    '    If Is_With_Bill Then
    '        If Not frm.LoaDHeadDataQuery(trans) Then
    '            Exit Sub
    '        End If
    '    Else
    '        If Not frm.LoaDHeadDataQueryVsp(trans) Then
    '            Exit Sub
    '        End If
    '    End If
    '    For Each row As GridViewRowInfo In frm.gvHead.Rows()
    '        If strSRN_No.Contains(clsCommon.myCstr(row.Cells(frmMILKPendingSRN.colHCode).Value)) Then
    '            frm.gvHead.CurrentRow = row
    '            row.Cells(frmMILKPendingSRN.colHSelect).Value = True
    '        End If
    '    Next
    '    frm.btnOKPressed()
    '    If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
    '        If clsCommon.myLen(frm.ArrReturn(0).DOC_CODE) > 0 Then
    '            obj_SRN = clsMilkSRNMCC.GetData(frm.ArrReturn(0).DOC_CODE, NavigatorType.Current, trans)
    '            If obj_SRN IsNot Nothing AndAlso clsCommon.myLen(obj_SRN.DOC_CODE) > 0 Then
    '                Dim TotOwnAsset As Double = 0
    '                Dim TotDeduction_Amount As Double = 0
    '                Dim objHead As New clsMilkPurchaseInvoiceMCC
    '                objHead.Program_Code = Formcode
    '                objHead.FROM_DATE = txtFromDate.Value
    '                objHead.TO_DATE = txtToDate.Value
    '                objHead.DOC_CODE = ""
    '                objHead.DOC_DATE = clsCommon.myCDate(End_date)
    '                objHead.Description = ""
    '                objHead.ROUTE_CODE = clsCommon.myCstr(obj_SRN.ROUTE_CODE)
    '                objHead.VSP_CODE = clsCommon.myCstr(obj_SRN.VSP_CODE)
    '                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select vsp_Payment,Handling_Charges_Per from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & obj_SRN.VSP_CODE & "'", trans)
    '                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '                    Throw New Exception("VSP- " + obj_SRN.VSP_CODE + "Not exists in vsp master")
    '                End If
    '                objHead.Payment = clsCommon.myCstr(dt.Rows(0)("vsp_Payment"))
    '                objHead.Handling_Charges_Per = clsCommon.myCdbl(dt.Rows(0)("Handling_Charges_Per"))
    '                objHead.Handling_Charges_Amount = 0
    '                objHead.SRN_Net_Amount = 0
    '                objHead.SRN_RO_Amount = 0
    '                objHead.Irregular_MCC_CODE = clsCommon.myCstr(obj_SRN.MCC_CODE)

    '                Dim objList As New List(Of clsMilkPurchaseInvoiceMCCDetail)
    '                Dim objDetail As clsMilkPurchaseInvoiceMCCDetail = Nothing
    '                Dim sQuery As String = "select TSPL_MILK_Shift_End_DETAIL.*,TSPL_MILK_SRN_HEAD.doc_code as srn_code from TSPL_MILK_Shift_End_DETAIL inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_head.VLC_DOC_CODE=TSPL_MILK_Shift_End_DETAIL.VLC_DOC_CODE where  TSPL_MILK_Shift_End_DETAIL.MCC_CODE='" & clsCommon.myCstr(obj_SRN.MCC_CODE) & "' " _
    '                & "and convert(date,TSPL_MILK_Shift_End_DETAIL.DOC_DATE,103)='" & clsCommon.GetPrintDate(obj_SRN.DOC_DATE, "dd-MMM-yyyy") & "' and TSPL_MILK_Shift_End_DETAIL.SHIFT='" & IIf(clsCommon.myCstr(obj_SRN.SHIFT) = "M", "Morning", "Evening") & "'"

    '                Dim DtShiftEnd As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
    '                Dim totAmount As Double = 0
    '                Dim totCommssion As Double = 0
    '                objHead.Total_PaymentCommission = 0
    '                Dim totAmountwithPaymentCommssion As Double = 0
    '                Dim totAmountIncentive As Double = 0
    '                Dim totAmountIncentiveEMP As Double = 0
    '                Dim totBasicAmount As Double = 0
    '                objHead.Total_Head_Load_Amount = 0
    '                For Each obj1 As clsMilkSRNMCCDetail In frm.ArrReturn
    '                    objDetail = New clsMilkPurchaseInvoiceMCCDetail
    '                    objDetail.DOC_CODE = ""
    '                    objDetail.AMOUNT = clsCommon.myCdbl(obj1.AMOUNT)
    '                    objDetail.Cans = clsCommon.myCdbl(obj1.Cans)
    '                    objDetail.CLR = clsCommon.myCdbl(obj1.CLR)
    '                    objDetail.COMMISSION = clsCommon.myCdbl(obj1.Commission)
    '                    objDetail.Payment_COMMISSION = clsCommon.myCdbl(obj1.Payment_Commission)
    '                    If DtShiftEnd.Rows.Count > 0 Then
    '                        Dim dr() As DataRow = DtShiftEnd.Select("vlc_code='" & clsCommon.myCstr(obj1.VlC_Code) & "' and srn_code='" & clsCommon.myCstr(obj1.DOC_CODE) & "'")
    '                        If dr.Length > 0 Then
    '                            objDetail.Deduction = IIf(clsCommon.myCstr(dr(0)("A_Or_R")) = "R", clsCommon.myCdbl(obj1.AMOUNT) * clsCommon.myCdbl(dr(0)("Deduction_of_VSP")) / 100, clsCommon.myCdbl(dr(0)("Deduction_of_VSP")))
    '                        End If
    '                    End If
    '                    objDetail.Own_Asset_Amount = clsCommon.myCdbl(obj1.Own_Asset_Amount)
    '                    objDetail.Correction_Factor = clsCommon.myCdbl(obj1.Correction_Factor)
    '                    objDetail.FAT_PER = clsCommon.myCdbl(obj1.FAT)
    '                    objDetail.Item_Code = clsCommon.myCstr(obj1.Item_CODE)
    '                    objDetail.MCC_CODE = strMCCCode
    '                    objDetail.Qty = clsCommon.myCdbl(obj1.MILK_Qty)
    '                    objDetail.Acc_Qty = clsCommon.myCdbl(obj1.ACC_Qty)
    '                    objDetail.Service_Charge = clsCommon.myCstr(obj1.Service_Charge_Type)
    '                    objDetail.RATE = clsCommon.myCdbl(obj1.RATE)
    '                    objDetail.SNF_PER = clsCommon.myCdbl(obj1.SNF)
    '                    objDetail.Head_Load_Amount = clsCommon.myCdbl(obj1.Head_Load_Amount)
    '                    Dim Commission_AMount As Double = 0
    '                    objDetail.Service_Charge_Amount = Math.Round(obj1.Service_Charge_Amount, 2)
    '                    ''Extra column of SRN
    '                    dt = clsDBFuncationality.GetDataTable("select NET_AMOUNT,Round_Off from TSPL_MILK_SRN_DETAIL where DOC_CODE='" + obj1.DOC_CODE + "'", trans)
    '                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '                        Throw New Exception("Milk SRN No " + obj1.DOC_CODE + " not found")
    '                    End If

    '                    obj1.NET_AMOUNT = Math.Round(clsCommon.myCdbl(dt.Rows(0)("NET_AMOUNT")), 2)
    '                    obj1.Round_Off = Math.Round(clsCommon.myCdbl(dt.Rows(0)("Round_Off")), 2)

    '                    objDetail.SRN_Net_Amount = obj1.NET_AMOUNT
    '                    objDetail.SRN_RO_Amount = obj1.Round_Off

    '                    ''End of Extra column of SRN
    '                    objDetail.Net_AMOUNT = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select NET_AMOUNT from TSPL_MILK_SRN_DETAIL where DOC_CODE='" + obj1.DOC_CODE + "'", trans)), 2) ''Not coming in object and only one row is exists 
    '                    objDetail.Handling_Charges_Amount = Math.Round((objDetail.Net_AMOUNT) * objHead.Handling_Charges_Per / 100, 2)
    '                    objDetail.Net_AMOUNT += objDetail.Handling_Charges_Amount

    '                    objDetail.TOTAL_AMOUNT = Math.Round(objDetail.Net_AMOUNT + objDetail.Service_Charge_Amount, 2)
    '                    objDetail.SRN_CODE = clsCommon.myCstr(obj1.DOC_CODE)
    '                    objDetail.VEHICLE_NO = clsCommon.myCstr(obj_SRN.VEHICLE_CODE)
    '                    objDetail.VLC_NO = clsCommon.myCstr(obj1.VlC_Code)
    '                    objHead.MCC_CODE = clsMilkPurchaseInvoiceMCC.GetIrregular_Location(objDetail.SRN_CODE, trans)
    '                    objDetail.MCC_CODE = clsMilkPurchaseInvoiceMCC.GetIrregular_Location(objDetail.SRN_CODE, trans)
    '                    If clsCommon.CompairString(objHead.MCC_CODE, objHead.Irregular_MCC_CODE) = CompairStringResult.Equal Then
    '                        objHead.Irregular_MCC_CODE = ""
    '                    End If

    '                    objList.Add(objDetail)
    '                    objHead.Total_Head_Load_Amount += objDetail.Head_Load_Amount
    '                    TotOwnAsset += objDetail.Own_Asset_Amount
    '                    TotDeduction_Amount += (objDetail.Deduction)

    '                    totAmount += objDetail.AMOUNT
    '                    totBasicAmount += objDetail.AMOUNT
    '                    totCommssion += 0
    '                    Dim TotPaycomm As Decimal = Math.Round((objDetail.TOTAL_AMOUNT - objDetail.AMOUNT - objDetail.Handling_Charges_Amount + obj1.Round_Off), 2, MidpointRounding.ToEven)
    '                    If Math.Abs(TotPaycomm) > 0.1 Then
    '                        objHead.Total_PaymentCommission += TotPaycomm
    '                    End If
    '                    totAmountwithPaymentCommssion += objDetail.Net_AMOUNT
    '                    objHead.Handling_Charges_Amount += objDetail.Handling_Charges_Amount
    '                    objHead.SRN_Net_Amount += objDetail.SRN_Net_Amount
    '                    objHead.SRN_RO_Amount += objDetail.SRN_RO_Amount
    '                Next
    '                objHead.Handling_Charges_Amount = Math.Round(objHead.Handling_Charges_Amount, 2, MidpointRounding.ToEven)
    '                objHead.Total_Head_Load_Amount = Math.Round(objHead.Total_Head_Load_Amount, 2, MidpointRounding.ToEven)
    '                If IsRoundOffPaiseAmount Then
    '                    objHead.Handling_Charges_RO_Amount = (objHead.Handling_Charges_Amount Mod 1)
    '                    objHead.Handling_Charges_Amount = objHead.Handling_Charges_Amount - objHead.Handling_Charges_RO_Amount


    '                    objHead.Total_Head_Load_RO_Amount = (objHead.Total_Head_Load_Amount Mod 1)
    '                    objHead.Total_Head_Load_Amount = objHead.Total_Head_Load_Amount - objHead.Total_Head_Load_RO_Amount
    '                End If


    '                objHead.Total_Own_Asset_Amount = TotOwnAsset
    '                objHead.Total_Deduction_Amount = TotDeduction_Amount
    '                objHead.VENDOR_INVOICE_NO = ""
    '                objHead.VENDOR_INVOICE_DATE = obj_SRN.DOC_DATE
    '                objHead.Amount = clsCommon.myCdbl(totAmount)
    '                objHead.Basic_Amount = Math.Round(clsCommon.myCdbl(totAmount) - clsCommon.myCdbl(totCommssion), 2)
    '                objHead.Commission = clsCommon.myCdbl(totCommssion)
    '                objHead.Total_Amount_Acc = clsCommon.myCdbl(totAmountwithPaymentCommssion) - objHead.Handling_Charges_RO_Amount

    '                objHead.Program_Code = Formcode
    '                objHead.No_Of_Asset = 0
    '                If CompanyVSPDeduction > 0 Or NonCompanyVSPDeduction > 0 Then
    '                    sQuery = "select Issue_To,sum(Issued_Qty*RI) as NoOFAsset from (" + Environment.NewLine +
    '                            "select TSPL_VSPAsset_HEAD.Doc_No,TSPL_VSPAsset_HEAD.Issue_To,TSPL_VSPAsset_detail.Item_Code,case when Doc_Type='Issue' then Issued_Qty else Issued_Qty_againstret end as Issued_Qty,case when Doc_Type='Issue' then 1 else -1 end as RI from TSPL_VSPAsset_detail" + Environment.NewLine +
    '                            "left outer join TSPL_VSPAsset_HEAD on TSPL_VSPAsset_HEAD.Doc_No=TSPL_VSPAsset_detail.Doc_No" + Environment.NewLine +
    '                            "where convert(date ,Doc_Date,103)<'" + clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy") + "' and Issue_To='" + objHead.VSP_CODE + "' and Status=1" + Environment.NewLine +
    '                            ")x group by Issue_To"
    '                    Dim dtAsset As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
    '                    If dtAsset IsNot Nothing AndAlso dtAsset.Rows.Count > 0 Then
    '                        If clsCommon.myCdbl(dtAsset.Rows(0)("NoOFAsset")) > 0 Then
    '                            objHead.No_Of_Asset = clsCommon.myCdbl(dtAsset.Rows(0)("NoOFAsset"))
    '                        End If
    '                    End If
    '                End If
    '                If clsMilkPurchaseInvoiceMCC.SaveData(objHead, objList, trans) Then
    '                    clsMilkPurchaseInvoiceMCC.SaveMPData(objHead.DOC_CODE, objHead.FROM_DATE, objHead.TO_DATE, objHead.MCC_CODE, objHead.VSP_CODE, trans)
    '                    If Not settDoNotIncludeIncentiveInMilkPurchaseInvoice Then
    '                        Dim incentive As ArrayList = clsMilkPurchaseInvoiceMCC.LoadDataQuery_For_Incentive(objHead.DOC_CODE, objHead.VSP_CODE, objHead.MCC_CODE, frm_date, Today.Date, False, trans, (End_date.Day - frm_date.Day) + 1)
    '                        clsMilkPurchaseInvoiceMCC.LoadDataQuery_For_Incentive_MP(objHead.DOC_CODE, objHead.VSP_CODE, objHead.MCC_CODE, frm_date, Today.Date, False, trans, (End_date.Day - frm_date.Day) + 1)
    '                        Dim totincentiveEMP As Double = 0
    '                        Dim totincentive As Double = 0
    '                        totAmount = 0
    '                        totBasicAmount = 0
    '                        totAmountwithPaymentCommssion = 0
    '                        Dim is_processed As Integer = 0
    '                        Dim is_Emp_On_Amount_Only As String = clsDBFuncationality.getSingleValue("select EmpOnAMountOnly from tspl_Mcc_Master where Mcc_Code='" & objDetail.MCC_CODE & "'", trans)
    '                        If incentive.Count > 0 Then
    '                            If incentive(1) > 0 Then
    '                                For Each obj1 As clsMilkSRNMCCDetail In frm.ArrReturn
    '                                    If is_processed = 0 Then
    '                                        totincentiveEMP = Math.Round(clsCommon.myCdbl(incentive(1)) * clsCommon.myCdbl(obj1.Payment_Commission) / 100, 2)
    '                                        totAmount += objDetail.AMOUNT + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
    '                                        totBasicAmount += objDetail.AMOUNT + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
    '                                        objDetail.Net_AMOUNT += +IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
    '                                        totAmountwithPaymentCommssion += objDetail.Net_AMOUNT '+ totincentiveEMP '+ incentive(1)
    '                                        sQuery = "Update tspl_Milk_Purchase_Invoice_Detail set Total_Amount='" & clsCommon.myCdbl(objDetail.AMOUNT) & "',Total_Amount_Acc='" & clsCommon.myCdbl(objDetail.Net_AMOUNT) & "',Net_Amount='" & clsCommon.myCdbl(objDetail.Net_AMOUNT) & "',incentive='" & incentive(1) & "' , incentiveEMP='" & totincentiveEMP & "' where srn_code='" & objDetail.SRN_CODE & "'"
    '                                        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
    '                                        is_processed = 1
    '                                    End If
    '                                    'Exit For
    '                                Next
    '                                is_processed = 0
    '                                totAmount = objHead.Amount + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
    '                                totAmountwithPaymentCommssion = objHead.Total_Amount_Acc + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
    '                                sQuery = "select * from tspl_Milk_Purchase_Invoice_Head where doc_code='" & clsCommon.myCstr(objHead.DOC_CODE) & "'"
    '                                dt = clsDBFuncationality.GetDataTable(sQuery, trans)
    '                                sQuery = "Update tspl_Milk_Purchase_Invoice_Head set Total_Amount='" & clsCommon.myCdbl(totAmount) & "',Total_Amount_Acc='" & clsCommon.myCdbl(totAmountwithPaymentCommssion) & "' ,incentive_Head='" & incentive(1) & "' , incentiveEMP_Head='" & totincentiveEMP & "' where doc_code='" & clsCommon.myCstr(objHead.DOC_CODE) & "'"
    '                                clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
    '                            End If
    '                        End If
    '                    End If
    '                    clsMilkPurchaseInvoiceMCC.PostData("M-PURINVOICE", objHead.DOC_CODE, trans)
    '                    'CreateHandlingCharges(objHead, trans) ''GKD/02/05/18-000126.By Balwinder On 03/05/2018 .No Need To make adjustment Entry of Handling Charges 
    '                    CreateDebitNoteForDeductionMapping(objHead, objList, trans)
    '                    VSPCommissionAndDeduction(frm_date, End_date, objHead, strSRN_No, trans)
    '                    CreateVSPDebitNoteOfAsset(objHead, txtToDate.Value, strMCCCode, trans)
    '                End If
    '            End If
    '        End If
    '    End If
    'End Sub

    Public Sub CreateVSPDebitNoteOfAsset(ByVal objHead As clsMilkPurchaseInvoiceMCC, ByVal dtDocDate As DateTime, ByVal strMCC As String, ByVal trans As SqlTransaction)
        Dim dblAmount As Decimal = NonCompanyVSPDeduction
        If objHead.No_Of_Asset > 0 Then
            dblAmount = CompanyVSPDeduction
        End If

        If dblAmount > 0 Then
            Dim strVendor As String = objHead.VSP_CODE
            Dim dt As DataTable
            Dim objVendorInvHead As New clsVedorInvoiceHead()
            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
            objVendorInvHead.isDeduction = 1
            objVendorInvHead.Security = 1
            Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code from TSPL_DEDUCTION_MASTER  where Is_Default_VSP_Deduction=1", trans)
            If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                Throw New Exception("Please set default VSP deduction code")
            End If
            objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))

            'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy")
            objVendorInvHead.Vendor_Code = strVendor
            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(strVendor, trans)
            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
            objVendorInvHead.Invoice_Type = "AP"
            objVendorInvHead.Vendor_Invoice_Date = dtDocDate
            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(strMCC, trans) 'obj.MCC_CODE
            'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE

            'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
            End If

            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
            objVendorInvHead.Description = "AP Debit Note For Non Company Assets Deduction"
            objVendorInvHead.RefDocType = "NCM-DED"
            If objHead.No_Of_Asset > 0 Then
                objVendorInvHead.RefDocType = "CM-DED"
                objVendorInvHead.Description = "AP Debit Note For Company Assets Deduction"
            End If
            objVendorInvHead.RefDocNo = objHead.DOC_CODE
            'objVendorInvHead.Ref_SNo = ""
            '' '' priti ends here
            'objVendorInvHead.Order_No = txtOrderNo.Text
            ' objVendorInvHead.Total_Tax = 0

            objVendorInvHead.On_Hold = False
            'Dim srndate As String = ""
            'Dim srncode As String = ""
            'Dim Vlc_Code As String = ""
            'Dim Vlc_Name As String = ""
            'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
            '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
            '        Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
            '        Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
            '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"))
            '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
            '        Vlc_Code = IIf(Vlc_Code = "", Dt_SRN.Rows(0).Item("VLC_Code").ToString, Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
            '        Vlc_Name = IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
            '    End If
            'Next



            'objVendorInvHead.Description = "VSP : " + obj.VSP_CODE + "/" + vendor_name + "VLC : " + Vlc_Code + "/" + Vlc_Name + " .Against PI Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
            'objVendorInvHead.Tax_Calculation_Type = Nothing
            'objVendorInvHead.Tax_Group = Nothing
            'If (clsCommon.myLen(obj.TAX1) > 0) Then
            '    objVendorInvHead.TAX1 = obj.TAX1
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
            '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
            '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
            '    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
            '    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX2) > 0) Then
            '    objVendorInvHead.TAX2 = obj.TAX2
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
            '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
            '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
            '    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
            '    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX3) > 0) Then
            '    objVendorInvHead.TAX3 = obj.TAX3
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
            '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
            '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
            '    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
            '    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX4) > 0) Then
            '    objVendorInvHead.TAX4 = obj.TAX4
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
            '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
            '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
            '    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
            '    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX5) > 0) Then
            '    objVendorInvHead.TAX5 = obj.TAX5
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
            '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
            '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
            '    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
            '    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX6) > 0) Then
            '    objVendorInvHead.TAX6 = obj.TAX6
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
            '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
            '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
            '    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
            '    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX7) > 0) Then
            '    objVendorInvHead.TAX7 = obj.TAX7
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
            '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
            '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
            '    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
            '    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX8) > 0) Then
            '    objVendorInvHead.TAX8 = obj.TAX8
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
            '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
            '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
            '    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
            '    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX9) > 0) Then
            '    objVendorInvHead.TAX9 = obj.TAX9
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
            '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
            '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
            '    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
            '    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX10) > 0) Then
            '    objVendorInvHead.TAX10 = obj.TAX10
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
            '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
            '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
            '    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
            '    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
            'End If

            'objVendorInvHead.Terms_Code = obj.Terms_Code
            'objVendorInvHead.Terms_Description = obj.TermsName
            objVendorInvHead.Due_Date = dtDocDate

            'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
            'objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE

            dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, strMCC, trans)
                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, strMCC, trans)
                End If
            End If
            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If

            'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

            'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
            'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
            'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

            'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
            'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
            'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

            'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
            'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
            'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

            'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
            'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
            'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

            'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
            'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
            'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

            'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
            'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
            'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

            'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
            'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
            'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

            'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
            'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
            'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

            'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
            'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
            'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

            'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
            'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
            'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
            Dim ii As Integer = 0
            Dim isFirstTime As Boolean = True
            ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
            'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
            objVendorInvHead.Total_Landed_Amt = 0

            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()



            ''Set AP Invvoice Detail Table

            Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Deduction_ACCOUNT"))
            If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                Throw New Exception("Please set Deduction Account for Vendor Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Acct_Set_Code")))
            End If
            strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, strMCC, trans)




            ii = ii + 1
            objVendorInvDetail.Detail_Line_No = ii
            objVendorInvDetail.GL_Account_Code = strInvCtrlAC
            objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(strInvCtrlAC, trans)
            objVendorInvDetail.Amount = dblAmount

            objVendorInvDetail.Discount_Per = 0
            objVendorInvDetail.Discount = 0
            objVendorInvDetail.Amount_less_Discount = dblAmount
            objVendorInvDetail.Total_Tax = 0
            objVendorInvDetail.Total_Amount = dblAmount
            objVendorInvDetail.Landed_Amount = dblAmount
            ''End of Set AP Invvoice Detail Table

            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                objVendorInvHead.Arr.Add(objVendorInvDetail)
            End If

            ''Set AP Invvoice Header Table
            objVendorInvHead.Total_Landed_Amt += dblAmount
            objVendorInvHead.Discount_Base += dblAmount
            objVendorInvHead.Discount_Amount += 0
            objVendorInvHead.Amount_Less_Discount += dblAmount
            objVendorInvHead.Document_Total += dblAmount
            objVendorInvHead.Balance_Amt += dblAmount
            ''End of Set AP Invvoice Header Table

            objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
            If objVendorInvHead.Empty_Amount > 0 Then
                If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                    Throw New Exception("Please set Inventory Control Empties")
                End If
                objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
            End If
            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                Throw New Exception("No GL Account Found For AP Invoice")
            End If
            ''multicurrency
            'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
            'objVendorInvHead.ConvRate = 1
            objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy")
            ''end multicurrency

            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, dtDocDate)
        End If
    End Sub

    Function getSRN(ByVal strSRNNo As String, ByVal tran As SqlTransaction) As DataTable
        Dim qry As String = "select Price_Code,Qty,FAT_PER,SNF_PER  from TSPL_MILK_SRN_DETAIL where doc_code='" + strSRNNo + "' "
        Return clsDBFuncationality.GetDataTable(qry, tran)
    End Function

    Sub VSPCommissionAndDeduction(ByVal FromDate As Date, ByVal ToDate As Date, ByVal objHead As clsMilkPurchaseInvoiceMCC, ByVal strSRN_No As List(Of String), ByVal trans As SqlTransaction)
        '        If strSRN_No IsNot Nothing AndAlso strSRN_No.Count > 0 Then
        '            Dim settVSPDayWiseIncentiveAtSRN As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VSPDayWiseIncentiveAtSRN, clsFixedParameterCode.VSPDayWiseIncentiveAtSRN, trans)) > 0)

        '            Dim qry As String = "update TSPL_MILK_SRN_DETAIL set VSP_Commission_Apply=0, VSP_Deduction_Apply=0 "
        '            If Not settVSPDayWiseIncentiveAtSRN Then
        '                qry += " ,VSP_Day_Wise_Incentive=0,VSP_Day_Wise_Incentive_Rate=0 "
        '            End If
        '            qry += " ,Farmer_Pro_Code=null,VSP_Mapping_Code_Day_Wise_Incentive=null,VSP_Mapping_Code=null where DOC_CODE In (" + clsCommon.GetMulcallString(strSRN_No) + ")  "
        '            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        '            Dim objVSPMapping As clsVSPMapping = clsVSPMapping.GetMappingCode(objHead.MCC_CODE, objHead.VSP_CODE, objHead.DOC_DATE, trans)
        '            Dim ArrFarmerPro As New List(Of String)
        '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(clsFarmerPro.GetLatestCodeByDate(objHead.MCC_CODE, objHead.VSP_CODE, FromDate, ToDate), trans)
        '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '                For Each dr As DataRow In dt.Rows
        '                    qry = clsCommon.GetPrintDate(clsCommon.myCDate(dr("thedate")), "dd/MMM/yyyy")
        '                    If Not ArrFarmerPro.Contains(qry) Then
        '                        ArrFarmerPro.Add(qry)
        '                    End If
        '                    If clsCommon.myLen(clsCommon.myCstr(dr("PriceCode"))) > 0 Then
        '                        qry = "update TSPL_MILK_SRN_DETAIL Set Farmer_Pro_Code='" + clsCommon.myCstr(dr("PriceCode")) + "' where DOC_CODE in (" +
        '                          "select DOC_CODE from TSPL_MILK_SRN_HEAD where DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ") and  convert(date, DOC_DATE,103)='" + clsCommon.GetPrintDate(dr("thedate"), "dd/MMM/yyyy") + "' and SHIFT='" + clsCommon.myCstr(dr("SHIFT")) + "')"
        '                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        '                    End If
        '                Next
        '            End If
        '            Dim BaseQry As String = "select  REPLACE( convert(varchar, DOC_DATE,106),' ','/') as DOC_DATE,SHIFT,Qty  from (select  TSPL_MILK_SRN_HEAD.DOC_DATE,SHIFT,sum(Qty) as Qty " +
        '                        "from TSPL_MILK_SRN_DETAIL " +
        '                        "left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE " +
        '                        " where TSPL_MILK_SRN_HEAD.DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ") " +
        '                        "group by DOC_DATE,SHIFT )x  where 2=2 "
        '            If objVSPMapping IsNot Nothing Then
        '                Dim objVSPComm As clsVSPCommission = Nothing
        '                If clsCommon.myLen(objVSPMapping.Commission_Code) > 0 Then
        '                    objVSPComm = clsVSPCommission.GetData(objVSPMapping.Commission_Code, NavigatorType.Current, trans)
        '                End If
        '                Dim objVSPDeduction As clsVSPDeduction = Nothing
        '                If clsCommon.myLen(objVSPMapping.Deduction_Code) > 0 Then
        '                    objVSPDeduction = clsVSPDeduction.GetData(objVSPMapping.Deduction_Code, NavigatorType.Current, trans)
        '                End If

        '                For Each strSRN As String In strSRN_No
        '                    Dim dtSRN As DataTable = Nothing
        '                    Dim dclVSP_Commission_Amount As Decimal = 0
        '                    Dim dclVSP_Deduction_Amount As Decimal = 0
        '                    If clsCommon.myLen(objVSPMapping.Commission_Code) > 0 Then
        '                        dtSRN = getSRN(strSRN, trans)
        '                        If dtSRN IsNot Nothing AndAlso dtSRN.Rows.Count > 0 Then
        '                            If objVSPComm.Commission_Rate > 0 Then
        '                                Dim strPMCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Price_Code  from TSPL_FAT_SNF_UPLOADER_MASTER where Code='" + clsCommon.myCstr(dtSRN.Rows(0)("Price_Code")) + "'", trans))
        '                                dclVSP_Commission_Amount = clsEkoPro.GetRateCalculated(strPMCode, clsCommon.myCdbl(dtSRN.Rows(0)("Qty")), clsCommon.myCdbl(dtSRN.Rows(0)("FAT_PER")), clsCommon.myCdbl(dtSRN.Rows(0)("SNF_PER")), trans, objVSPComm.Commission_Rate)
        '                            End If
        '                        End If
        '                    End If
        '                    If clsCommon.myLen(objVSPMapping.Deduction_Code) > 0 Then
        '                        If dtSRN Is Nothing OrElse dtSRN.Rows.Count <= 0 Then
        '                            dtSRN = getSRN(strSRN, trans)
        '                        End If
        '                        If dtSRN IsNot Nothing AndAlso dtSRN.Rows.Count > 0 Then
        '                            If clsCommon.myCdbl(dtSRN.Rows(0)("FAT_PER")) < objVSPDeduction.Deduction_Minimum_FAT_Per OrElse clsCommon.myCdbl(dtSRN.Rows(0)("SNF_PER")) < objVSPDeduction.Deduction_Minimum_SNF_Per Then
        '                                If objVSPDeduction.Deduction_Rate > 0 Then
        '                                    dclVSP_Deduction_Amount = Math.Round(objVSPDeduction.Deduction_Rate * clsCommon.myCdbl(dtSRN.Rows(0)("Qty")), 0, MidpointRounding.AwayFromZero)
        '                                End If
        '                            End If
        '                        End If
        '                    End If
        '                    Dim coll As New Hashtable()
        '                    clsCommon.AddColumnsForChange(coll, "VSP_Commission_Amount", dclVSP_Commission_Amount)
        '                    clsCommon.AddColumnsForChange(coll, "VSP_Deduction_Amount", dclVSP_Deduction_Amount)
        '                    clsCommon.AddColumnsForChange(coll, "VSP_Mapping_Code", objVSPMapping.Code)
        '                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_DETAIL", OMInsertOrUpdate.Update, "TSPL_MILK_SRN_DETAIL.DOC_CODE='" + strSRN + "'", trans)
        '                Next

        '                qry = "select count(*) from TSPL_MILK_PURCHASE_INVOICE_HEAD where vsp_code='" + objHead.VSP_CODE + "' and DOC_CODE not in ('" + objHead.DOC_CODE + "')"
        '                Dim countInv As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        '                If objVSPComm IsNot Nothing Then
        '                    If countInv >= clsCommon.myCdbl(objVSPComm.Commission_No_Of_Payment_Cycle_For_New_VSP) Then
        '                        dt = clsDBFuncationality.GetDataTable(BaseQry, trans)
        '                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '                            If dt.Rows.Count >= clsCommon.myCdbl(objVSPComm.Commission_Minimum_Shift_In_Payment_Cycle) Then
        '                                qry = BaseQry + " and Qty >=" + clsCommon.myCstr(objVSPComm.Commission_Minimum_Qty_In_Shift) + ""
        '                                dt = clsDBFuncationality.GetDataTable(qry, trans)
        '                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '                                    qry = "update TSPL_MILK_SRN_DETAIL set VSP_Commission_Apply=1 where DOC_CODE in (" +
        '                                "select  DOC_CODE from TSPL_MILK_SRN_HEAD where DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ") and ("
        '                                    For ii As Integer = 0 To dt.Rows.Count - 1
        '                                        qry += " (convert(date, DOC_DATE,103)='" + clsCommon.myCstr(dt.Rows(ii)("DOC_DATE")) + "' and SHIFT='" + clsCommon.myCstr(dt.Rows(ii)("SHIFT")) + "') "
        '                                        If ii = dt.Rows.Count - 1 Then
        '                                            qry += " )) "
        '                                        Else
        '                                            qry += " or "
        '                                        End If
        '                                    Next
        '                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
        '                                End If
        '                            End If
        '                        End If
        '                    Else
        '                        qry = "update TSPL_MILK_SRN_DETAIL set VSP_Commission_Apply=1 where isnull(VSP_Commission_Amount,0)>0  and DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ")"
        '                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        '                    End If
        '                End If
        '                If objVSPDeduction IsNot Nothing Then
        '                    If countInv >= objVSPDeduction.Deduction_No_Of_Payment_Cycle_For_New_VSP Then
        '                        qry = "update TSPL_MILK_SRN_DETAIL set VSP_Deduction_Apply=1 where DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ") and isnull( VSP_Deduction_Amount,0)>0"
        '                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        '                    End If
        '                End If
        '            End If


        '#Region "VSPDayWiseIncentiveCalculatioin"
        '            If Not settVSPDayWiseIncentiveAtSRN Then
        '                qry = "select DOC_DATE,sum(Qty) as Qty  from (" + BaseQry + ")x group by DOC_DATE"
        '                dt = clsDBFuncationality.GetDataTable(qry, trans)
        '                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '                    For Each dr As DataRow In dt.Rows
        '                        If ArrFarmerPro.Contains(clsCommon.GetPrintDate(clsCommon.myCDate(dr("DOC_DATE")), "dd/MMM/yyyy")) Then
        '                            Continue For
        '                        End If
        '                        Dim dtVSPdayWiseIncentive As DataTable = Nothing
        '                        objVSPMapping = clsVSPMapping.GetMappingCode(objHead.MCC_CODE, objHead.VSP_CODE, clsCommon.myCDate(dr("DOC_DATE")), trans)
        '                        If objVSPMapping IsNot Nothing Then
        '                            If clsCommon.myLen(objVSPMapping.Day_Wise_Incentive_Code) > 0 Then
        '                                qry = "select * from TSPL_VSP_DAY_WISE_INCENTIVE_MASTER where Day_Wise_Incentive_Code='" + objVSPMapping.Day_Wise_Incentive_Code + "'"
        '                                dtVSPdayWiseIncentive = clsDBFuncationality.GetDataTable(qry, trans)
        '                                If dtVSPdayWiseIncentive IsNot Nothing AndAlso dtVSPdayWiseIncentive.Rows.Count > 0 Then
        '                                    For ii As Integer = 5 To 1 Step -1
        '                                        If clsCommon.myCdbl(dtVSPdayWiseIncentive.Rows(0)("Day_Wise_Incentive_Rate_" + clsCommon.myCstr(ii) + "")) > 0 Then
        '                                            If clsCommon.myCdbl(dr("Qty")) >= clsCommon.myCdbl(dtVSPdayWiseIncentive.Rows(0)("Day_Wise_Incentive_From_" + clsCommon.myCstr(ii) + "")) AndAlso clsCommon.myCdbl(dr("Qty")) <= clsCommon.myCdbl(dtVSPdayWiseIncentive.Rows(0)("Day_Wise_Incentive_To_" + clsCommon.myCstr(ii) + "")) Then
        '                                                qry = "select DOC_CODE,Qty,FAT_PER,SNF_PER,Price_Code,NET_AMOUNT from TSPL_MILK_SRN_DETAIL where DOC_CODE in (" +
        '                                                    "select  DOC_CODE from TSPL_MILK_SRN_HEAD where DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ") and  convert(date, DOC_DATE,103)='" + clsCommon.myCstr(dr("DOC_DATE")) + "')"
        '                                                Dim dtSRND As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        '                                                If dtSRND IsNot Nothing AndAlso dtSRND.Rows.Count > 0 Then
        '                                                    For Each drSRND As DataRow In dtSRND.Rows
        '                                                        Dim strPMCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Price_Code  from TSPL_FAT_SNF_UPLOADER_MASTER where Code='" + clsCommon.myCstr(drSRND("Price_Code")) + "'", trans))
        '                                                        Dim IncAmt As Decimal = clsEkoPro.GetRateCalculatedExact(strPMCode, clsCommon.myCdbl(drSRND("Qty")), clsCommon.myCdbl(drSRND("FAT_PER")), clsCommon.myCdbl(drSRND("SNF_PER")), trans, clsCommon.myCdbl(dtVSPdayWiseIncentive.Rows(0)("Day_Wise_Incentive_Rate_" + clsCommon.myCstr(ii) + "")))
        '                                                        IncAmt -= clsCommon.myCdbl(drSRND("NET_AMOUNT"))
        '                                                        qry = "update TSPL_MILK_SRN_DETAIL set VSP_Day_Wise_Incentive_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(dtVSPdayWiseIncentive.Rows(0)("Day_Wise_Incentive_Rate_" + clsCommon.myCstr(ii) + ""))) + "', VSP_Day_Wise_Incentive=" + clsCommon.myCstr(IncAmt) + ",VSP_Mapping_Code_Day_Wise_Incentive='" + objVSPMapping.Code + "' where DOC_CODE='" + clsCommon.myCstr(drSRND("DOC_CODE")) + "'"
        '                                                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        '                                                    Next
        '                                                End If
        '                                                Exit For
        '                                            End If
        '                                        End If
        '                                    Next
        '                                End If
        '                            End If
        '                        End If
        '                    Next
        '                End If
        '            End If
        '#End Region

        '            qry = "select sum( VSP_Commission_Amount*VSP_Commission_Apply) as VSP_Commission_Amount,sum(VSP_Deduction_Amount *  VSP_Deduction_Apply) as VSP_Deduction_Amount,sum(VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive from TSPL_MILK_SRN_DETAIL where DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ") "
        '            Dim dtAmt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        '            If dtAmt IsNot Nothing AndAlso dtAmt.Rows.Count > 0 Then
        '                If clsCommon.myCdbl(dtAmt.Rows(0)("VSP_Commission_Amount")) > 0 Then
        '#Region "CreateCreditNotForCommision"
        '                    Dim objVendorInvHead As New clsVedorInvoiceHead()
        '                    objVendorInvHead.isDeduction = 0
        '                    objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
        '                    objVendorInvHead.Vendor_Code = objHead.VSP_CODE
        '                    objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
        '                    objVendorInvHead.Vendor_Invoice_No = ""
        '                    objVendorInvHead.Invoice_Type = "AP"
        '                    objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
        '                    objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans)
        '                    objVendorInvHead.Description = "AP Credit Note For VSP Commission"
        '                    objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
        '                    If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
        '                        Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
        '                    End If
        '                    objVendorInvHead.Document_Type = "C"
        '                    objVendorInvHead.RefDocType = "VSP-COM"
        '                    objVendorInvHead.RefDocNo = objHead.DOC_CODE
        '                    objVendorInvHead.On_Hold = False
        '                    objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
        '                    dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT,Commission_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
        '                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '                        objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
        '                        objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
        '                        If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
        '                            objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
        '                            objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
        '                        End If
        '                    End If
        '                    If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
        '                        Throw New Exception("Please set the vendor payable Account")
        '                    End If
        '                    objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
        '                    Dim ii As Integer = 0
        '                    Dim isFirstTime As Boolean = True
        '                    objVendorInvHead.Total_Landed_Amt = 0
        '                    objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()
        '                    If True Then
        '                        ''Set AP Invvoice Detail Table
        '                        ii = ii + 1
        '                        Dim objVendorInvDetail As New clsVedorInvoiceDetail()
        '                        objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Commission_ACCOUNT"))
        '                        If clsCommon.myLen(objVendorInvDetail.GL_Account_Code) <= 0 Then
        '                            Throw New Exception("Please set Commission Account for Vendor Account set  [" + objVendorInvHead.Account_Set + "]")
        '                        End If
        '                        objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
        '                        objVendorInvDetail.Detail_Line_No = ii
        '                        objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)
        '                        Dim dblAmount As Decimal = clsERPFuncationality.myFloor(clsCommon.myCdbl(dtAmt.Rows(0)("VSP_Commission_Amount")), 0)


        '                        objVendorInvDetail.Amount = dblAmount
        '                        objVendorInvDetail.Discount_Per = 0
        '                        objVendorInvDetail.Discount = 0
        '                        objVendorInvDetail.Amount_less_Discount = dblAmount
        '                        objVendorInvDetail.Total_Tax = 0
        '                        objVendorInvDetail.Total_Amount = dblAmount
        '                        objVendorInvDetail.Landed_Amount = dblAmount
        '                        ''End of Set AP Invvoice Detail Table

        '                        If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
        '                            objVendorInvHead.Arr.Add(objVendorInvDetail)
        '                        End If

        '                        ''Set AP Invvoice Header Table
        '                        objVendorInvHead.Total_Landed_Amt += dblAmount
        '                        objVendorInvHead.Discount_Base += dblAmount
        '                        objVendorInvHead.Discount_Amount += 0
        '                        objVendorInvHead.Amount_Less_Discount += dblAmount
        '                        objVendorInvHead.Document_Total += dblAmount
        '                        objVendorInvHead.Balance_Amt += dblAmount
        '                        ''End of Set AP Invvoice Header Table
        '                    End If
        '                    If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
        '                        Throw New Exception("No GL Account Found For AP Invoice")
        '                    End If
        '                    objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
        '                    objVendorInvHead.SaveData(objVendorInvHead, True, trans)
        '                    clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
        '#End Region
        '                End If
        '                If clsCommon.myCdbl(dtAmt.Rows(0)("VSP_Deduction_Amount")) > 0 Then
        '#Region "CreateDebitNotForDeduction"
        '                    'Dim objDedMapping As New clsDeductionMappingHead
        '                    'objDedMapping = objDedMapping.GetLatestMappingCode(objHead.MCC_CODE, objHead.VSP_CODE, objHead.DOC_DATE, trans)
        '                    If True Then
        '                        Dim objVendorInvHead As New clsVedorInvoiceHead()
        '                        objVendorInvHead.isDeduction = 1
        '                        objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
        '                        objVendorInvHead.Vendor_Code = objHead.VSP_CODE
        '                        objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
        '                        objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
        '                        objVendorInvHead.Invoice_Type = "AP"
        '                        objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
        '                        objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans) 'obj.MCC_CODE
        '                        objVendorInvHead.Description = "AP Debit Note Against VSP Quality Deduction"
        '                        objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
        '                        If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
        '                            Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
        '                        End If
        '                        objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
        '                        objVendorInvHead.RefDocType = "VSP-QLT"
        '                        objVendorInvHead.RefDocNo = objHead.DOC_CODE
        '                        objVendorInvHead.On_Hold = False
        '                        objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
        '                        dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
        '                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '                            objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
        '                            objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
        '                            If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
        '                                objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
        '                                objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
        '                            End If
        '                        End If
        '                        If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
        '                            Throw New Exception("Please set the vendor payable Account")
        '                        End If
        '                        objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
        '                        Dim ii As Integer = 0
        '                        Dim isFirstTime As Boolean = True
        '                        objVendorInvHead.Total_Landed_Amt = 0
        '                        objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()
        '                        If True Then
        '                            ''Set AP Invvoice Detail Table
        '                            Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Default_VSP_Quality_Deduction=1", trans)
        '                            If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
        '                                Throw New Exception("Please set default VSP Quality deduction in Deduction Master")
        '                            End If
        '                            If clsCommon.myLen(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))) <= 0 Then
        '                                Throw New Exception("Please set GL Account for deduction [" + clsCommon.myCstr(dtDed.Rows(0)("code")) + "]")
        '                            End If

        '                            ii = ii + 1
        '                            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
        '                            objVendorInvDetail.Detail_Line_No = ii
        '                            objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
        '                            objVendorInvDetail.Deduction_Name = ClsDeductionMaster.GetName(objVendorInvDetail.DeductionCode, trans)
        '                            objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
        '                            objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
        '                            objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)

        '                            Dim dblAmount As Decimal = Math.Round(clsCommon.myCdbl(dtAmt.Rows(0)("VSP_Deduction_Amount")), 0)
        '                            objVendorInvDetail.Amount = dblAmount
        '                            objVendorInvDetail.Discount_Per = 0
        '                            objVendorInvDetail.Discount = 0
        '                            objVendorInvDetail.Amount_less_Discount = dblAmount
        '                            objVendorInvDetail.Total_Tax = 0
        '                            objVendorInvDetail.Total_Amount = dblAmount
        '                            objVendorInvDetail.Landed_Amount = dblAmount
        '                            ''End of Set AP Invvoice Detail Table
        '                            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
        '                                objVendorInvHead.Arr.Add(objVendorInvDetail)
        '                            End If

        '                            ''Set AP Invvoice Header Table
        '                            objVendorInvHead.Total_Landed_Amt += dblAmount
        '                            objVendorInvHead.Discount_Base += dblAmount
        '                            objVendorInvHead.Discount_Amount += 0
        '                            objVendorInvHead.Amount_Less_Discount += dblAmount
        '                            objVendorInvHead.Document_Total += dblAmount
        '                            objVendorInvHead.Balance_Amt += dblAmount
        '                            ''End of Set AP Invvoice Header Table

        '                            objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
        '                            If objVendorInvHead.Empty_Amount > 0 Then
        '                                If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
        '                                    Throw New Exception("Please set Inventory Control Empties")
        '                                End If
        '                                objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
        '                            End If
        '                        End If
        '                        If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
        '                            Throw New Exception("No GL Account Found For AP Invoice")
        '                        End If
        '                        objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
        '                        objVendorInvHead.SaveData(objVendorInvHead, True, trans)
        '                        clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)

        '                    End If
        '#End Region
        '                End If

        '                If clsCommon.myCdbl(dtAmt.Rows(0)("VSP_Day_Wise_Incentive")) > 0 Then
        '#Region "CreateCreditNotForVSPDayWiseIncentive"
        '                    Dim objVendorInvHead As New clsVedorInvoiceHead()
        '                    objVendorInvHead.isDeduction = 0
        '                    objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
        '                    objVendorInvHead.Vendor_Code = objHead.VSP_CODE
        '                    objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
        '                    objVendorInvHead.Vendor_Invoice_No = ""
        '                    objVendorInvHead.Invoice_Type = "AP"
        '                    objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
        '                    objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans)
        '                    objVendorInvHead.Description = "AP Credit Note For VSP Commission"
        '                    objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
        '                    If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
        '                        Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
        '                    End If
        '                    objVendorInvHead.Document_Type = "C"
        '                    objVendorInvHead.RefDocType = "VSP-DIT"
        '                    objVendorInvHead.RefDocNo = objHead.DOC_CODE
        '                    objVendorInvHead.On_Hold = False
        '                    objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
        '                    dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT,Incentive_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
        '                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '                        objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
        '                        objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
        '                        If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
        '                            objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
        '                            objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
        '                        End If
        '                    End If
        '                    If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
        '                        Throw New Exception("Please set the vendor payable Account")
        '                    End If
        '                    objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
        '                    Dim ii As Integer = 0
        '                    Dim isFirstTime As Boolean = True
        '                    objVendorInvHead.Total_Landed_Amt = 0
        '                    objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()
        '                    If True Then
        '                        ''Set AP Invvoice Detail Table
        '                        ii = ii + 1
        '                        Dim objVendorInvDetail As New clsVedorInvoiceDetail()
        '                        objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Incentive_ACCOUNT"))
        '                        If clsCommon.myLen(objVendorInvDetail.GL_Account_Code) <= 0 Then
        '                            Throw New Exception("Please set Incentive Account for Vendor Account set  [" + objVendorInvHead.Account_Set + "]")
        '                        End If
        '                        objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
        '                        objVendorInvDetail.Detail_Line_No = ii
        '                        objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)
        '                        Dim dblAmount As Decimal = clsERPFuncationality.myFloor(clsCommon.myCdbl(dtAmt.Rows(0)("VSP_Day_Wise_Incentive")), 0)
        '                        objVendorInvDetail.Amount = dblAmount
        '                        objVendorInvDetail.Discount_Per = 0
        '                        objVendorInvDetail.Discount = 0
        '                        objVendorInvDetail.Amount_less_Discount = dblAmount
        '                        objVendorInvDetail.Total_Tax = 0
        '                        objVendorInvDetail.Total_Amount = dblAmount
        '                        objVendorInvDetail.Landed_Amount = dblAmount
        '                        ''End of Set AP Invvoice Detail Table

        '                        If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
        '                            objVendorInvHead.Arr.Add(objVendorInvDetail)
        '                        End If

        '                        ''Set AP Invvoice Header Table
        '                        objVendorInvHead.Total_Landed_Amt += dblAmount
        '                        objVendorInvHead.Discount_Base += dblAmount
        '                        objVendorInvHead.Discount_Amount += 0
        '                        objVendorInvHead.Amount_Less_Discount += dblAmount
        '                        objVendorInvHead.Document_Total += dblAmount
        '                        objVendorInvHead.Balance_Amt += dblAmount
        '                        ''End of Set AP Invvoice Header Table
        '                    End If
        '                    If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
        '                        Throw New Exception("No GL Account Found For AP Invoice")
        '                    End If
        '                    objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
        '                    objVendorInvHead.SaveData(objVendorInvHead, True, trans)
        '                    clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
        '#End Region
        '                End If

        '            End If

        '            If ArrFarmerPro IsNot Nothing Then
        '                Dim strVLCCode As String = clsDBFuncationality.getSingleValue("select VLC_Code from TSPL_VLC_MASTER_HEAD where VSP_Code='" + objHead.VSP_CODE + "'", trans)
        '                Dim SettLocalSaleAllowedPer As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.LocalSaleAllowedPer, clsFixedParameterCode.LocalSaleAllowedPer, trans))
        '                Dim SettLocalSaleRate As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.LocalSaleAllowedRate, clsFixedParameterCode.LocalSaleAllowedRate, trans))

        '                qry = clsMilkPurchaseInvoiceProvisionHead.GetQryProData(objHead.DOC_CODE, strVLCCode, strSRN_No, FromDate, ToDate, SettLocalSaleAllowedPer, SettLocalSaleRate)
        '                clsDBFuncationality.ExecuteNonQuery(qry, trans)

        '                qry = "select   sum(NoteAmt) as NoteAmt from TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS where InvoiceNo='" + objHead.DOC_CODE + "' "
        '                dtAmt = clsDBFuncationality.GetDataTable(qry, trans)
        '                If dtAmt IsNot Nothing AndAlso dtAmt.Rows.Count > 0 Then
        '                    If clsCommon.myCdbl(dtAmt.Rows(0)("NoteAmt")) < 0 Then
        '#Region "CreateCreditNotForPROData"
        '                        Dim objVendorInvHead As New clsVedorInvoiceHead()
        '                        objVendorInvHead.isDeduction = 0
        '                        objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
        '                        objVendorInvHead.Vendor_Code = objHead.VSP_CODE
        '                        objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
        '                        objVendorInvHead.Vendor_Invoice_No = ""
        '                        objVendorInvHead.Invoice_Type = "AP"
        '                        objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
        '                        objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans)
        '                        objVendorInvHead.Description = "AP Credit Note For Farmer PRO"
        '                        objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
        '                        If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
        '                            Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
        '                        End If
        '                        objVendorInvHead.Document_Type = "C"
        '                        objVendorInvHead.RefDocType = "PRO-VFC"
        '                        objVendorInvHead.RefDocNo = objHead.DOC_CODE
        '                        objVendorInvHead.On_Hold = False
        '                        objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
        '                        dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,PRO_DATA_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
        '                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '                            objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
        '                            objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
        '                            If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
        '                                objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
        '                                objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
        '                            End If
        '                        End If
        '                        If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
        '                            Throw New Exception("Please set the vendor payable Account")
        '                        End If
        '                        objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
        '                        Dim ii As Integer = 0
        '                        Dim isFirstTime As Boolean = True
        '                        objVendorInvHead.Total_Landed_Amt = 0
        '                        objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()
        '                        If True Then
        '                            ''Set AP Invvoice Detail Table
        '                            ii = ii + 1
        '                            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
        '                            objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("PRO_DATA_ACCOUNT"))
        '                            If clsCommon.myLen(objVendorInvDetail.GL_Account_Code) <= 0 Then
        '                                Throw New Exception("Please set PRO Data Account for Vendor Account set  [" + objVendorInvHead.Account_Set + "]")
        '                            End If
        '                            objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
        '                            objVendorInvDetail.Detail_Line_No = ii
        '                            objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)
        '                            Dim dblAmount As Decimal = Math.Abs(clsERPFuncationality.myFloor(clsCommon.myCdbl(dtAmt.Rows(0)("NoteAmt")), 0))


        '                            objVendorInvDetail.Amount = dblAmount
        '                            objVendorInvDetail.Discount_Per = 0
        '                            objVendorInvDetail.Discount = 0
        '                            objVendorInvDetail.Amount_less_Discount = dblAmount
        '                            objVendorInvDetail.Total_Tax = 0
        '                            objVendorInvDetail.Total_Amount = dblAmount
        '                            objVendorInvDetail.Landed_Amount = dblAmount
        '                            ''End of Set AP Invvoice Detail Table

        '                            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
        '                                objVendorInvHead.Arr.Add(objVendorInvDetail)
        '                            End If

        '                            ''Set AP Invvoice Header Table
        '                            objVendorInvHead.Total_Landed_Amt += dblAmount
        '                            objVendorInvHead.Discount_Base += dblAmount
        '                            objVendorInvHead.Discount_Amount += 0
        '                            objVendorInvHead.Amount_Less_Discount += dblAmount
        '                            objVendorInvHead.Document_Total += dblAmount
        '                            objVendorInvHead.Balance_Amt += dblAmount
        '                            ''End of Set AP Invvoice Header Table
        '                        End If
        '                        If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
        '                            Throw New Exception("No GL Account Found For AP Invoice")
        '                        End If
        '                        objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
        '                        objVendorInvHead.SaveData(objVendorInvHead, True, trans)
        '                        clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
        '#End Region
        '                    ElseIf clsCommon.myCdbl(dtAmt.Rows(0)("NoteAmt")) > 0 Then
        '#Region "CreateDebitNotForProDATA"
        '                        If True Then
        '                            Dim objVendorInvHead As New clsVedorInvoiceHead()
        '                            objVendorInvHead.isDeduction = 1
        '                            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
        '                            objVendorInvHead.Vendor_Code = objHead.VSP_CODE
        '                            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
        '                            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
        '                            objVendorInvHead.Invoice_Type = "AP"
        '                            objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
        '                            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans) 'obj.MCC_CODE
        '                            objVendorInvHead.Description = "AP Debit Note Against VLC Farmer PRO Data"
        '                            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
        '                            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
        '                                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
        '                            End If
        '                            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
        '                            objVendorInvHead.RefDocType = "PRO-VFD"
        '                            objVendorInvHead.RefDocNo = objHead.DOC_CODE
        '                            objVendorInvHead.On_Hold = False
        '                            objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
        '                            dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
        '                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '                                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
        '                                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
        '                                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
        '                                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
        '                                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
        '                                End If
        '                            End If
        '                            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
        '                                Throw New Exception("Please set the vendor payable Account")
        '                            End If
        '                            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
        '                            Dim ii As Integer = 0
        '                            Dim isFirstTime As Boolean = True
        '                            objVendorInvHead.Total_Landed_Amt = 0
        '                            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()
        '                            If True Then
        '                                ''Set AP Invvoice Detail Table
        '                                Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Default_PRO_Data=1", trans)
        '                                If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
        '                                    Throw New Exception("Please set default PRO Data deduction in Deduction Master")
        '                                End If
        '                                If clsCommon.myLen(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))) <= 0 Then
        '                                    Throw New Exception("Please set GL Account for deduction [" + clsCommon.myCstr(dtDed.Rows(0)("code")) + "]")
        '                                End If

        '                                ii = ii + 1
        '                                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
        '                                objVendorInvDetail.Detail_Line_No = ii
        '                                objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
        '                                objVendorInvDetail.Deduction_Name = ClsDeductionMaster.GetName(objVendorInvDetail.DeductionCode, trans)
        '                                objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
        '                                objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
        '                                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)

        '                                Dim dblAmount As Decimal = Math.Round(clsCommon.myCdbl(dtAmt.Rows(0)("NoteAmt")), 0)
        '                                objVendorInvDetail.Amount = dblAmount
        '                                objVendorInvDetail.Discount_Per = 0
        '                                objVendorInvDetail.Discount = 0
        '                                objVendorInvDetail.Amount_less_Discount = dblAmount
        '                                objVendorInvDetail.Total_Tax = 0
        '                                objVendorInvDetail.Total_Amount = dblAmount
        '                                objVendorInvDetail.Landed_Amount = dblAmount
        '                                ''End of Set AP Invvoice Detail Table
        '                                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
        '                                    objVendorInvHead.Arr.Add(objVendorInvDetail)
        '                                End If

        '                                ''Set AP Invvoice Header Table
        '                                objVendorInvHead.Total_Landed_Amt += dblAmount
        '                                objVendorInvHead.Discount_Base += dblAmount
        '                                objVendorInvHead.Discount_Amount += 0
        '                                objVendorInvHead.Amount_Less_Discount += dblAmount
        '                                objVendorInvHead.Document_Total += dblAmount
        '                                objVendorInvHead.Balance_Amt += dblAmount
        '                                ''End of Set AP Invvoice Header Table

        '                                objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
        '                                If objVendorInvHead.Empty_Amount > 0 Then
        '                                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
        '                                        Throw New Exception("Please set Inventory Control Empties")
        '                                    End If
        '                                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
        '                                End If
        '                            End If
        '                            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
        '                                Throw New Exception("No GL Account Found For AP Invoice")
        '                            End If
        '                            objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
        '                            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
        '                            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
        '                        End If
        '#End Region
        '                    End If
        '                End If
        '            End If
        '        End If
    End Sub
    Sub CreateDebitNoteForDeductionMapping(ByVal objHead As clsMilkPurchaseInvoiceMCC, ByVal objList As List(Of clsMilkPurchaseInvoiceMCCDetail), ByVal trans As SqlTransaction)
        Dim objDedMapping As New clsDeductionMappingHead
        objDedMapping = objDedMapping.GetLatestMappingCode(objHead.MCC_CODE, objHead.VSP_CODE, objHead.DOC_DATE, trans)
        If objDedMapping IsNot Nothing AndAlso clsCommon.myLen(objDedMapping.Doc_Code) > 0 Then

            Dim objVendorInvHead As New clsVedorInvoiceHead()
            objVendorInvHead.isDeduction = 1
            'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
            objVendorInvHead.Vendor_Code = objHead.VSP_CODE
            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
            objVendorInvHead.Invoice_Type = "AP"
            objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans) 'obj.MCC_CODE
            'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE
            objVendorInvHead.Description = "AP Debit Note Against Deduction Mapping No:" + objDedMapping.Doc_Code
            'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
            End If
            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
            ''objVendorInvHead.PO_Number = obj.p
            '' ''added by priti
            objVendorInvHead.RefDocType = "DED-MAP"
            objVendorInvHead.RefDocNo = objHead.DOC_CODE
            'objVendorInvHead.Ref_SNo = ""
            '' '' priti ends here
            'objVendorInvHead.Order_No = txtOrderNo.Text
            ' objVendorInvHead.Total_Tax = 0
            objVendorInvHead.On_Hold = False
            'Dim srndate As String = ""
            'Dim srncode As String = ""
            'Dim Vlc_Code As String = ""
            'Dim Vlc_Name As String = ""
            'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
            '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
            '        Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
            '        Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
            '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"))
            '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
            '        Vlc_Code = IIf(Vlc_Code = "", Dt_SRN.Rows(0).Item("VLC_Code").ToString, Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
            '        Vlc_Name = IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
            '    End If
            'Next



            'objVendorInvHead.Description = "VSP : " + obj.VSP_CODE + "/" + vendor_name + "VLC : " + Vlc_Code + "/" + Vlc_Name + " .Against PI Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
            'objVendorInvHead.Tax_Calculation_Type = Nothing
            'objVendorInvHead.Tax_Group = Nothing
            'If (clsCommon.myLen(obj.TAX1) > 0) Then
            '    objVendorInvHead.TAX1 = obj.TAX1
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
            '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
            '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
            '    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
            '    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX2) > 0) Then
            '    objVendorInvHead.TAX2 = obj.TAX2
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
            '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
            '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
            '    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
            '    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX3) > 0) Then
            '    objVendorInvHead.TAX3 = obj.TAX3
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
            '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
            '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
            '    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
            '    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX4) > 0) Then
            '    objVendorInvHead.TAX4 = obj.TAX4
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
            '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
            '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
            '    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
            '    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX5) > 0) Then
            '    objVendorInvHead.TAX5 = obj.TAX5
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
            '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
            '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
            '    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
            '    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX6) > 0) Then
            '    objVendorInvHead.TAX6 = obj.TAX6
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
            '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
            '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
            '    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
            '    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX7) > 0) Then
            '    objVendorInvHead.TAX7 = obj.TAX7
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
            '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
            '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
            '    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
            '    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX8) > 0) Then
            '    objVendorInvHead.TAX8 = obj.TAX8
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
            '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
            '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
            '    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
            '    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX9) > 0) Then
            '    objVendorInvHead.TAX9 = obj.TAX9
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
            '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
            '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
            '    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
            '    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX10) > 0) Then
            '    objVendorInvHead.TAX10 = obj.TAX10
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
            '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
            '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
            '    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
            '    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
            'End If

            'objVendorInvHead.Terms_Code = obj.Terms_Code
            'objVendorInvHead.Terms_Description = obj.TermsName
            objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date

            'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
            'objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                End If
            End If
            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If

            'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

            'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
            'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
            'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

            'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
            'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
            'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

            'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
            'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
            'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

            'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
            'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
            'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

            'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
            'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
            'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

            'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
            'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
            'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

            'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
            'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
            'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

            'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
            'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
            'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

            'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
            'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
            'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

            'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
            'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
            'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
            Dim ii As Integer = 0
            Dim isFirstTime As Boolean = True
            ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
            'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
            objVendorInvHead.Total_Landed_Amt = 0

            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()

            For Each objDedMappingDetail As clsDeductionMappingDetail In objDedMapping.Arr
                ''Set AP Invvoice Detail Table
                If clsCommon.myLen(objDedMappingDetail.DeductionGLAccount) <= 0 Then
                    Throw New Exception("Please set GL Account for deduction  :" + objDedMappingDetail.Deduction_Code)
                End If
                objDedMappingDetail.DeductionGLAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(objDedMappingDetail.DeductionGLAccount, objHead.MCC_CODE, trans)

                ii = ii + 1
                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                objVendorInvDetail.Detail_Line_No = ii
                objVendorInvDetail.DeductionCode = objDedMappingDetail.Deduction_Code
                objVendorInvDetail.Deduction_Name = objDedMappingDetail.DeductionName
                objVendorInvDetail.GL_Account_Code = objDedMappingDetail.DeductionGLAccount
                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objDedMappingDetail.DeductionGLAccount, trans)
                Dim dblAmount As Decimal = 0
                If clsCommon.CompairString(objDedMappingDetail.Type, "Qty") = CompairStringResult.Equal Then
                    Dim dblqty As Decimal = 0
                    For Each objdet As clsMilkPurchaseInvoiceMCCDetail In objList
                        dblqty += objdet.Qty
                    Next
                    dblAmount = dblqty * objDedMappingDetail.Per / 100
                End If
                If clsCommon.CompairString(objDedMappingDetail.Type, "Amt") = CompairStringResult.Equal Then
                    dblAmount = objHead.SRN_Net_Amount * objDedMappingDetail.Per / 100
                End If
                If objDedMapping.Is_Round_Down Then
                    dblAmount = dblAmount - (dblAmount Mod 1)
                End If

                objVendorInvDetail.Amount = dblAmount
                objVendorInvDetail.Discount_Per = 0
                objVendorInvDetail.Discount = 0
                objVendorInvDetail.Amount_less_Discount = dblAmount
                objVendorInvDetail.Total_Tax = 0
                objVendorInvDetail.Total_Amount = dblAmount
                objVendorInvDetail.Landed_Amount = dblAmount
                ''End of Set AP Invvoice Detail Table

                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                End If

                ''Set AP Invvoice Header Table
                objVendorInvHead.Total_Landed_Amt += dblAmount
                objVendorInvHead.Discount_Base += dblAmount
                objVendorInvHead.Discount_Amount += 0
                objVendorInvHead.Amount_Less_Discount += dblAmount
                objVendorInvHead.Document_Total += dblAmount
                objVendorInvHead.Balance_Amt += dblAmount
                ''End of Set AP Invvoice Header Table

                objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                If objVendorInvHead.Empty_Amount > 0 Then
                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                        Throw New Exception("Please set Inventory Control Empties")
                    End If
                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                End If
            Next






            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                Throw New Exception("No GL Account Found For AP Invoice")
            End If
            ''multicurrency
            'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
            'objVendorInvHead.ConvRate = 1
            objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
            ''end multicurrency

            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
        End If
    End Sub

    Sub CreateHandlingCharges(ByVal objHead As clsMilkPurchaseInvoiceMCC, ByVal trans As SqlTransaction)
        If objHead.Handling_Charges_Amount > 0 Then
            ''Handling Charges 
            Dim objAdj As New ClsAdjustments
            objAdj.Trans_Type = "In"
            objAdj.Adjustment_Date = objHead.DOC_DATE
            objAdj.Posting_Date = objHead.DOC_DATE
            objAdj.EntryDateTime = objHead.DOC_DATE
            objAdj.IsMilkType = 1
            objAdj.Loc_Code = objHead.MCC_CODE
            objAdj.Loc_Desc = clsLocation.GetName(objAdj.Loc_Code, trans)
            objAdj.Description = "Cost Adjustment for handling charges.MCC :" & objAdj.Loc_Code & " Milk Purchase Invoice No: " + objHead.DOC_CODE
            objAdj.Reference_Document = "MPI-HAN-CHA"
            objAdj.Document_No = objHead.DOC_CODE
            objAdj.Adjustment_Type = "ADJ"
            objAdj.Arr = New List(Of ClsAdjustmentsDetails)

            Dim objAdjTR As New ClsAdjustmentsDetails()
            objAdjTR.Adjustment_Line_No = 1
            objAdjTR.Item_Code = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans))
            objAdjTR.Item_Description = clsItemMaster.GetItemName(objAdjTR.Item_Code, trans)
            objAdjTR.Adjustment_Type = "CI"
            objAdjTR.Item_Quantity = 0
            objAdjTR.Item_Cost = objHead.Handling_Charges_Amount
            objAdjTR.mrp = 0
            objAdjTR.Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" & objAdjTR.Item_Code & "' and Default_UOM='1' ", trans))
            If clsCommon.myLen(objAdjTR.Unit_Code) <= 0 Then
                objAdjTR.Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" & objAdjTR.Item_Code & "' and Stocking_Unit='Y' ", trans))
            End If
            objAdjTR.fat_pers = 0
            objAdjTR.fat_kg = 0
            objAdjTR.snf_pers = 0
            objAdjTR.snf_kg = 0
            objAdjTR.Unit_Cost = 0
            objAdjTR.fat_Rate = 0
            objAdjTR.snf_Rate = 0
            objAdjTR.snf_Amt = 0
            objAdjTR.fat_Amt = 0
            'Dim qry As String = "select top 1 Fat_Amt/(Fat_Amt+SNF_Amt) as FATRatio,SNF_Amt/(Fat_Amt+SNF_Amt) as SNFRatio from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No in (select SRN_CODE from TSPL_MILK_PURCHASE_INVOICE_DETAIL where DOC_CODE='" + objHead.DOC_CODE + "'   ) and (Fat_Amt+SNF_Amt)>0"
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    objAdjTR.snf_Amt = Math.Round(objHead.Handling_Charges_Amount * clsCommon.myCdbl(dt.Rows(0)("SNFRatio")), 2, MidpointRounding.ToEven)
            '    objAdjTR.fat_Amt = Math.Round(objHead.Handling_Charges_Amount * clsCommon.myCdbl(dt.Rows(0)("FATRatio")), 2, MidpointRounding.ToEven)
            'End If
            'dt = Nothing
            objAdj.Arr.Add(objAdjTR)
            objAdj.SaveData(objAdj, True, "", trans)
            ClsAdjustments.PostData(objAdj.Adjustment_No, "Store Adjustment", trans)
            ''End Handling Charges
        End If
    End Sub

    '    Public Sub SelectMilkSRNItemsForMPPayment(ByVal strSRN_No As List(Of String), ByVal Vsp_Name As String, ByVal frm_date As Date, ByVal End_date As Date, ByVal Is_With_Bill As Boolean, ByVal trans As SqlTransaction)
    '        'Dim sQuery As String = "select Distinct tspl_mp_master.MP_Code from TSPL_VLC_DATA_UPLOADER inner join tspl_mp_master on tspl_mp_master.MP_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.MP_CODE inner join tspl_vendor_master on tspl_mp_master.MP_CODE=Vendor_Code where Parent_Vendor_Code='" & Vsp_Name & "' and convert(date,tspl_Vlc_Data_Uploader.DOC_DATE,103) Between  convert(date,'" & clsCommon.GetPrintDate(frm_date, "dd-MMM-yyyy") & "',103) and convert(date,'" & clsCommon.GetPrintDate(End_date, "dd-MMM-yyyy") & "',103) "
    '        Dim sQuery As String = "select distinct TSPL_MP_MASTER.mp_Code,vsp_COde from TSPL_VLC_DATA_UPLOADER  inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_VLC_DATA_UPLOADER.VLC_CODE and VSP_Code='" & Vsp_Name & "' and convert(date,tspl_Vlc_Data_Uploader.DOC_DATE,103) Between  convert(date,'" & clsCommon.GetPrintDate(frm_date, "dd-MMM-yyyy") & "',103) and convert(date,'" & clsCommon.GetPrintDate(End_date, "dd-MMM-yyyy") & "',103)   inner join TSPL_MP_MASTER on MP_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.MP_CODE and TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_MASTER.VLC_Code"
    '        Dim DT_Mp As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
    '        For Each row_MP As DataRow In DT_Mp.Rows
    '            Dim obj_SRN As New clsMilkSRNMCC

    '            Dim frm As New frmMILKPendingSRN()
    '            frm.VendorCode = Vsp_Name
    '            frm.MpCode = clsCommon.myCstr(row_MP.Item("MP_Code"))
    '            frm.isForMP = True
    '            frm.fORMCode = clsUserMgtCode.MilkMPPayment
    '            frm.stran = trans
    '            'frm.strCurrCode = FndSRNNO.Value
    '            frm.Frm_date = frm_date
    '            frm.To_date = End_date
    '            Dim StrDoc As New List(Of String)
    '            If Is_With_Bill Then
    '                If Not frm.LoaDHeadDataQuery(trans) Then
    '                    GoTo a
    '                End If
    '            Else
    '                If Not frm.LoaDHeadDataQueryVsp(trans) Then
    '                    GoTo a
    '                End If
    '            End If
    '            'frm.ShowDialog()
    '            'For Each Get_srn_no As String In strSRN_No
    '            For Each row As GridViewRowInfo In frm.gvHead.Rows()
    '                If strSRN_No.Contains(clsCommon.myCstr(row.Cells(frmMILKPendingSRN.colHCode).Value)) Then
    '                    frm.gvHead.CurrentRow = row
    '                    row.Cells(frmMILKPendingSRN.colHSelect).Value = True
    '                    'frm.LoadDetailData(True, clsCommon.myCstr(row.Cells(frm.colHCode).Value))
    '                End If
    '            Next
    '            'Next
    '            frm.btnOKPressed()
    '            If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
    '                If clsCommon.myLen(frm.ArrReturn(0).DOC_CODE) > 0 Then
    '                    obj_SRN = clsMilkSRNMCC.GetData(frm.ArrReturn(0).DOC_CODE, NavigatorType.Current, trans)
    '                    If obj_SRN IsNot Nothing AndAlso clsCommon.myLen(obj_SRN.DOC_CODE) > 0 Then
    '                        '            txtCode.Value = obj.DOC_CODE
    '                        '  If dtpDocDate.MinDate < obj.DOC_DATE Then
    '                        '      dtpDocDate.MinDate = obj.DOC_DATE
    '                        '  End If
    '                        '  FndMccCode.Value = obj.MCC_CODE
    '                        '  'If clsCommon.myLen(obj.MCC_CODE) > 0 Then
    '                        '  '    Payment_Cycle_value = clsDBFuncationality.getSingleValue("SELECT payment_cycle from tspl_mcc_master where mcc_Code='" & obj.MCC_CODE & "'")
    '                        '  'End If
    '                        '  DtMCC = clsDBFuncationality.GetDataTable("select * from tspl_Mcc_Master where Mcc_Code='" & clsCommon.myCstr(FndMccCode.Value) & "'", trans)
    '                        '  lblMccName.Text = DtMCC.Rows(0).Item("mcc_name") 'clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_Code='" & obj.MCC_CODE & "'")

    '                        '  'dtpDocDate.Value = obj.DOC_DATE

    '                        '  FndSRNNO.Value = ""
    '                        '  fndVSPCode.Value = obj.VSP_CODE

    '                        '  txtPayment.Text = clsDBFuncationality.getSingleValue("select vsp_Payment from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVSPCode.Value & "'", trans)
    '                        '  fndRouteCOde.Text = obj.ROUTE_CODE


    '                        '  lblRouteDesc.Text = clsDBFuncationality.getSingleValue("select Route_Name from TSPL_MCC_ROUTE_MASTER where Route_Code='" & fndRouteCOde.Text & "'", trans)
    '                        '  ' If LCase(txtPayment.Text) = "different" Then
    '                        '  '   lblVSPDesc.Text = clsDBFuncationality.getSingleValue("select joint_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVSPCode.text & "'")
    '                        '  'Else
    '                        '  lblVSPDesc.Text = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVSPCode.Value & "'", trans)
    '                        '  'End If

    '                        '  'If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
    '                        '  LoadBlankGridVSpPay()
    '                        '  ' Dim sQuery As String = "select * from TSPL_MILK_Shift_End_DETAIL where  MCC_CODE='" & clsCommon.myCstr(obj.MCC_CODE) & "' " _
    '                        '  '& "and convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(obj.DOC_DATE, "dd-MMM-yyyy") & "' and SHIFT='" & IIf(clsCommon.myCstr(obj.SHIFT) = "M", "Morning", "Evening") & "'"
    '                        '  Dim sQuery As String = "select TSPL_MILK_Shift_End_DETAIL.*,TSPL_MILK_SRN_HEAD.doc_code as srn_code from TSPL_MILK_Shift_End_DETAIL inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_head.VLC_DOC_CODE=TSPL_MILK_Shift_End_DETAIL.VLC_DOC_CODE where  TSPL_MILK_Shift_End_DETAIL.MCC_CODE='" & clsCommon.myCstr(obj.MCC_CODE) & "' " _
    '                        '& "and convert(date,TSPL_MILK_Shift_End_DETAIL.DOC_DATE,103)='" & clsCommon.GetPrintDate(obj.DOC_DATE, "dd-MMM-yyyy") & "' and TSPL_MILK_Shift_End_DETAIL.SHIFT='" & IIf(clsCommon.myCstr(obj.SHIFT) = "M", "Morning", "Evening") & "'"

    '                        '  Dim DtShiftEnd As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
    '                        '  For Each obj1 As clsMilkSRNMCCDetail In frm.ArrReturn
    '                        '      gv1.Rows.AddNew()

    '                        '      FndSRNNO.Value = IIf(FndSRNNO.Value = "", obj1.DOC_CODE, FndSRNNO.Value & "," & obj1.DOC_CODE)
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colCans).Value = obj1.Cans
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colCLR).Value = obj1.CLR
    '                        '      ' gv1.Rows(gv1.Rows.Count - 1).Cells(colCode).Value = obj1.DOC_CODE
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj1.MILK_Qty
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colAcc_Qty).Value = obj1.ACC_Qty
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colService_Charge).Value = obj1.Service_Charge_Type


    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colCorrection_Factor).Value = obj1.Correction_Factor
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colFAT_PER).Value = obj1.FAT
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colIncentive).Value = 0 '0
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colIncentiveEMP).Value = 0
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Code).Value = obj1.Item_CODE
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Desc).Value = obj1.Item_Desc

    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colRATE).Value = obj1.RATE
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF_PER).Value = obj1.SNF
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colSRN_CODE).Value = obj1.DOC_CODE
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colSrn_Date).Value = obj.DOC_DATE
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colTOTAL_AMOUNT).Value = obj1.AMOUNT
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = obj1.UOM
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colVEHICLE_NO).Value = obj.VEHICLE_CODE
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value = obj1.VlC_Code
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colAMOUNT).Value = obj1.AMOUNT
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colHead_Load_Amount).Value = obj1.Head_Load_Amount
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colOwn_Asset_Amount).Value = obj1.Own_Asset_Amount

    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentCOMMISSION).Value = obj1.Payment_Commission
    '                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colCOMMISSION).Value = obj1.Commission
    '                        '      If DtShiftEnd.Rows.Count > 0 Then
    '                        '          Dim dr() As DataRow = DtShiftEnd.Select("vlc_code='" & clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value) & "' and srn_code='" & clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colSRN_CODE).Value) & "'")
    '                        '          'Dim dr() As DataRow = DtShiftEnd.Select("vlc_code='" & clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value) & "'")
    '                        '          If dr.Length > 0 Then
    '                        '              gv1.Rows(gv1.Rows.Count - 1).Cells(colDeduction).Value = IIf(clsCommon.myCstr(dr(0)("A_Or_R")) = "R", clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colAMOUNT).Value) * clsCommon.myCdbl(dr(0)("Deduction_of_VSP")) / 100, clsCommon.myCdbl(dr(0)("Deduction_of_VSP")))
    '                        '          End If
    '                        '      End If
    '                        '      If Not Is_With_Bill Then
    '                        '          If Not StrDoc.Contains(obj1.Invoice_Code) Then
    '                        '              StrDoc.Add(obj1.Invoice_Code)
    '                        '          End If
    '                        '      End If
    '                        '      ' gv1.Rows(gv1.Rows.Count - 1).Cells(colDeduction).Value = 0
    '                        '  Next
    '                        'Else
    '                        '    gv1.Rows.AddNew()
    '                        'End If



    '                        'If rbtnTaxCalManual.IsChecked Then ''For Calcuation custom tax according to ratio of amount
    '                        '    For ii As Integer = 0 To gv1.RowCount - 1
    '                        '        UpdateCurrentRow(ii)
    '                        '    Next
    '                        'End If

    '                        Dim objHead As clsMilkPurchaseInvoiceMCC
    '                        Dim TotHeadLoad As Double = 0
    '                        Dim TotOwnAsset As Double = 0
    '                        Dim TotDeduction_Amount As Double = 0
    '                        '' asign screen vaules in objHead
    '                        objHead = New clsMilkPurchaseInvoiceMCC
    '                        objHead.DOC_CODE = ""
    '                        objHead.DOC_DATE = clsCommon.myCDate(End_date) 'obj_SRN.DOC_DATE)
    '                        objHead.Description = ""
    '                        objHead.ROUTE_CODE = clsCommon.myCstr(obj_SRN.ROUTE_CODE)
    '                        objHead.VSP_CODE = clsCommon.myCstr(frm.VendorCode) 'obj_SRN.VSP_CODE)
    '                        objHead.Payment = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vsp_Payment from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & obj_SRN.VSP_CODE & "'", trans))
    '                        objHead.Irregular_MCC_CODE = clsCommon.myCstr(obj_SRN.MCC_CODE)
    '                        objHead.Program_Code = Formcode


    '                        Dim objList As New List(Of clsMilkPurchaseInvoiceMCCDetail)

    '                        Dim obj As clsMilkPurchaseInvoiceMCCDetail = Nothing
    '                        sQuery = "select TSPL_MILK_Shift_End_DETAIL.*,TSPL_MILK_SRN_HEAD.doc_code as srn_code from TSPL_MILK_Shift_End_DETAIL inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_head.VLC_DOC_CODE=TSPL_MILK_Shift_End_DETAIL.VLC_DOC_CODE where  TSPL_MILK_Shift_End_DETAIL.MCC_CODE='" & clsCommon.myCstr(obj_SRN.MCC_CODE) & "' " _
    '                     & "and convert(date,TSPL_MILK_Shift_End_DETAIL.DOC_DATE,103)='" & clsCommon.GetPrintDate(obj_SRN.DOC_DATE, "dd-MMM-yyyy") & "' and TSPL_MILK_Shift_End_DETAIL.SHIFT='" & IIf(clsCommon.myCstr(obj_SRN.SHIFT) = "M", "Morning", "Evening") & "'"

    '                        Dim DtShiftEnd As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)

    '                        '========================Total==================
    '                        Dim totAmount As Double = 0
    '                        Dim totCommssion As Double = 0
    '                        Dim totPaymentCommssion As Double = 0
    '                        Dim totAmountwithPaymentCommssion As Double = 0
    '                        Dim totAmountIncentive As Double = 0
    '                        Dim totAmountIncentiveEMP As Double = 0
    '                        Dim totBasicAmount As Double = 0

    '                        '==============================================
    '                        For Each obj1 As clsMilkSRNMCCDetail In frm.ArrReturn
    '                            obj = New clsMilkPurchaseInvoiceMCCDetail
    '                            obj.DOC_CODE = ""
    '                            obj.AMOUNT = clsCommon.myCdbl(obj1.AMOUNT)
    '                            obj.Cans = clsCommon.myCdbl(obj1.Cans)
    '                            obj.CLR = clsCommon.myCdbl(obj1.CLR)
    '                            obj.COMMISSION = clsCommon.myCdbl(obj1.Commission)
    '                            obj.Payment_COMMISSION = clsCommon.myCdbl(obj1.Payment_Commission)
    '                            If DtShiftEnd.Rows.Count > 0 Then
    '                                Dim dr() As DataRow = DtShiftEnd.Select("vlc_code='" & clsCommon.myCstr(obj1.VlC_Code) & "' and srn_code='" & clsCommon.myCstr(obj1.DOC_CODE) & "'")
    '                                'Dim dr() As DataRow = DtShiftEnd.Select("vlc_code='" & clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value) & "'")
    '                                If dr.Length > 0 Then
    '                                    obj.Deduction = IIf(clsCommon.myCstr(dr(0)("A_Or_R")) = "R", clsCommon.myCdbl(obj1.AMOUNT) * clsCommon.myCdbl(dr(0)("Deduction_of_VSP")) / 100, clsCommon.myCdbl(dr(0)("Deduction_of_VSP")))
    '                                End If
    '                            End If
    '                            'obj.Deduction = clsCommon.myCdbl(grow.Cells(colDeduction).Value)
    '                            obj.Own_Asset_Amount = clsCommon.myCdbl(obj1.Own_Asset_Amount)
    '                            obj.Head_Load_Amount = clsCommon.myCdbl(obj1.Head_Load_Amount)
    '                            obj.Correction_Factor = clsCommon.myCdbl(obj1.Correction_Factor)
    '                            obj.FAT_PER = clsCommon.myCdbl(obj1.FAT)
    '                            'obj.Incentive = clsCommon.myCdbl(grow.Cells(colIncentive).Value)
    '                            'obj.IncentiveEMP = clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value)
    '                            obj.Item_Code = clsCommon.myCstr(obj1.Item_CODE)
    '                            obj.MCC_CODE = obj1.MCC_CODE
    '                            obj.Qty = clsCommon.myCdbl(obj1.MILK_Qty)
    '                            obj.Acc_Qty = clsCommon.myCdbl(obj1.ACC_Qty)
    '                            obj.Service_Charge = clsCommon.myCstr(obj1.Service_Charge_Type)
    '                            obj.RATE = clsCommon.myCdbl(obj1.RATE)
    '                            obj.SNF_PER = clsCommon.myCdbl(obj1.SNF)
    '                            obj.Head_Load_Amount = clsCommon.myCdbl(obj1.Head_Load_Amount)
    '                            'obj.Own_Asset_Amount = clsCommon.myCdbl(obj1.Own_Asset_Amount)
    '                            '=====================================
    '                            Dim Commission_AMount As Double = 0
    '                            Dim Payment_Commission_AMount As Double = 0
    '                            If clsCommon.myCstr(obj1.Service_Charge_Type) = "%(Percentage)" Then
    '                                'grow.Cells(colCOMMISSIONAmount).Value = grow.Cells(colAMOUNT).Value * grow.Cells(colCOMMISSION).Value / 100
    '                                'grow.Cells(colPaymentCOMMISSIONAmount).Value = grow.Cells(colAMOUNT).Value * grow.Cells(colPaymentCOMMISSION).Value / 100
    '                                'grow.Cells(colIncentiveEMP).Value = clsCommon.myCdbl(grow.Cells(colIncentive).Value) * grow.Cells(colPaymentCOMMISSION).Value / 100

    '                                Commission_AMount = Math.Round(clsCommon.myCdbl(obj1.AMOUNT) * clsCommon.myCdbl(obj1.Commission) / 100, 2)
    '                                Payment_Commission_AMount = Math.Round(clsCommon.myCdbl(obj1.AMOUNT) * clsCommon.myCdbl(obj1.Payment_Commission) / 100, 2)
    '                                ' obj1.i = Math.Round(clsCommon.myCdbl(obj1.AMOUNT) * clsCommon.myCdbl(obj1.Payment_Commission) / 100, 2)
    '                            ElseIf clsCommon.myCstr(obj1.Service_Charge_Type) = "Rate/Kg" Then
    '                                'grow.Cells(colCOMMISSIONAmount).Value = grow.Cells(colAcc_Qty).Value * grow.Cells(colCOMMISSION).Value
    '                                'grow.Cells(colPaymentCOMMISSIONAmount).Value = grow.Cells(colAcc_Qty).Value * grow.Cells(colPaymentCOMMISSION).Value
    '                                'grow.Cells(colIncentiveEMP).Value = clsCommon.myCdbl(grow.Cells(colIncentive).Value) * grow.Cells(colPaymentCOMMISSION).Value / 100

    '                                Commission_AMount = Math.Round(clsCommon.myCdbl(obj1.ACC_Qty) * clsCommon.myCdbl(obj1.Commission), 2)
    '                                Payment_Commission_AMount = Math.Round(clsCommon.myCdbl(obj1.ACC_Qty) * clsCommon.myCdbl(obj1.Payment_Commission), 2)
    '                            ElseIf clsCommon.myCstr(obj1.Service_Charge_Type) = "Rate/Ltr" And clsCommon.myCstr(obj1.UOM) = "LTR" Then
    '                                'grow.Cells(colCOMMISSIONAmount).Value = grow.Cells(colQty).Value * grow.Cells(colCOMMISSION).Value
    '                                'grow.Cells(colPaymentCOMMISSIONAmount).Value = grow.Cells(colQty).Value * grow.Cells(colPaymentCOMMISSION).Value
    '                                'grow.Cells(colIncentiveEMP).Value = clsCommon.myCdbl(grow.Cells(colIncentive).Value) * grow.Cells(colPaymentCOMMISSION).Value / 100

    '                                Commission_AMount = Math.Round(clsCommon.myCdbl(obj1.MILK_Qty) * clsCommon.myCdbl(obj1.Commission), 2)
    '                                Payment_Commission_AMount = Math.Round(clsCommon.myCdbl(obj1.MILK_Qty) * clsCommon.myCdbl(obj1.Payment_Commission), 2)
    '                            End If
    '                            'grow.Cells(colDocument_AMOUNT).Value = clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
    '                            'grow.Cells(colTOTAL_AMOUNT).Value = clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
    '                            'grow.Cells(colNetAMOUNT).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value)) ' - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_CALC"))))
    '                            'grow.Cells(colNetsaveAMOUNT).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value)) ' - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_SAVE"))))

    '                            'obj.do = clsCommon.myCdbl(obj1.AMOUNT) + clsCommon.myCdbl(Payment_Commission_AMount) '+ clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
    '                            obj.TOTAL_AMOUNT = Math.Round(clsCommon.myCdbl(obj1.AMOUNT) + clsCommon.myCdbl(Payment_Commission_AMount), 2) '+ clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
    '                            'grow.Cells(colNetAMOUNT).Value = Math.Round(clsCommon.myCdbl(obj1.AMOUNT) + clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value)) ' - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_CALC"))))
    '                            obj.Net_AMOUNT = Math.Round(clsCommon.myCdbl(obj1.AMOUNT) + clsCommon.myCdbl(Payment_Commission_AMount), 2) '+ clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value)) ' - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_SAVE"))))

    '                            '===============================================================
    '                            obj.SRN_CODE = clsCommon.myCstr(obj1.DOC_CODE)
    '                            obj.TOTAL_AMOUNT = clsCommon.myCdbl(obj.TOTAL_AMOUNT)
    '                            obj.VEHICLE_NO = clsCommon.myCstr(obj_SRN.VEHICLE_CODE)
    '                            obj.VLC_NO = clsCommon.myCstr(obj1.VlC_Code)
    '                            obj.Net_AMOUNT = clsCommon.myCdbl(obj.Net_AMOUNT)
    '                            obj1.NET_AMOUNT = clsCommon.myCdbl(obj.Net_AMOUNT)
    '                            objHead.MCC_CODE = clsMilkPurchaseInvoiceMCC.GetIrregular_Location(obj.SRN_CODE, trans)
    '                            obj.MCC_CODE = clsMilkPurchaseInvoiceMCC.GetIrregular_Location(obj.SRN_CODE, trans)
    '                            If clsCommon.CompairString(objHead.MCC_CODE, objHead.Irregular_MCC_CODE) = CompairStringResult.Equal Then
    '                                objHead.Irregular_MCC_CODE = ""
    '                            End If

    '                            objList.Add(obj)
    '                            TotHeadLoad += obj.Head_Load_Amount
    '                            TotOwnAsset += obj.Own_Asset_Amount
    '                            TotDeduction_Amount += obj.Deduction

    '                            totAmount += obj.AMOUNT
    '                            totBasicAmount += obj.AMOUNT
    '                            totCommssion += Commission_AMount
    '                            totPaymentCommssion += Payment_Commission_AMount
    '                            totAmountwithPaymentCommssion += obj.Net_AMOUNT
    '                        Next
    '                        objHead.Total_Head_Load_Amount = TotHeadLoad
    '                        objHead.Total_Own_Asset_Amount = TotOwnAsset
    '                        objHead.Total_Deduction_Amount = TotDeduction_Amount


    '                        objHead.VENDOR_INVOICE_NO = "" 'clsCommon.myCstr(TxtVendorInvoiceNo.Text)
    '                        objHead.VENDOR_INVOICE_DATE = obj_SRN.DOC_DATE 'clsCommon.myCDate(VENDOR_INVOICE_DATE.Value)
    '                        objHead.Amount = clsCommon.myCdbl(totAmount)
    '                        objHead.Basic_Amount = Math.Round(clsCommon.myCdbl(totAmount) - clsCommon.myCdbl(totCommssion), 2)
    '                        objHead.Commission = clsCommon.myCdbl(totCommssion)
    '                        objHead.Total_Amount_Acc = clsCommon.myCdbl(totAmountwithPaymentCommssion)
    '                        objHead.Total_PaymentCommission = clsCommon.myCdbl(totPaymentCommssion)
    '                        objHead.Program_Code = Formcode
    '                        'End If
    '                        'Next
    '                        ' ''For Custom Fields
    '                        ''Dim obj As New clsMilkPurchaseInvoiceMCC()
    '                        'obj = New clsMilkPurchaseInvoiceMCC
    '                        'obj.Form_ID = MyBase.Form_ID
    '                        'obj.arrCustomFields = New List(Of clsCustomFieldValues)
    '                        'If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
    '                        '    UcCustomFields1.GetData(obj.arrCustomFields)
    '                        'End If
    '                        'If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
    '                        '    clsCustomFieldGrid.GetData(obj.arrCustomFields, gv1, MyBase.ArrDetailFields, colCode)
    '                        'End If
    '                        ' ''End of For Custom Fields

    '                        If clsMilkPurchaseInvoiceMCC.SaveData(objHead, objList, trans) Then
    '                            ' trans.Commit()
    '                            'Dim transs As SqlTransaction
    '                            'UcAttachment1.SaveData(objHead.DOC_CODE)
    '                            'Dim incentive As ArrayList = clsMilkPurchaseInvoiceMCC.LoadDataQuery_For_Incentive(objHead.DOC_CODE, objHead.VSP_CODE, objHead.MCC_CODE, frm_date, Today.Date, False, trans, (End_date.Day - frm_date.Day) + 1)
    '                            'Dim totincentiveEMP As Double = 0
    '                            'Dim totincentive As Double = 0
    '                            'totAmount = 0
    '                            'totBasicAmount = 0
    '                            'totAmountwithPaymentCommssion = 0
    '                            'Dim is_processed As Integer = 0
    '                            'Dim is_Emp_On_Amount_Only As String = clsDBFuncationality.getSingleValue("select EmpOnAMountOnly from tspl_Mcc_Master where Mcc_Code='" & obj.MCC_CODE & "'", trans)
    '                            'If incentive.Count > 0 Then
    '                            '    If incentive(1) > 0 Then
    '                            '        For Each obj1 As clsMilkSRNMCCDetail In frm.ArrReturn
    '                            '            If is_processed = 0 Then
    '                            '                totincentiveEMP = Math.Round(clsCommon.myCdbl(incentive(1)) * clsCommon.myCdbl(obj1.Payment_Commission) / 100, 2)
    '                            '                totAmount += obj.AMOUNT + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
    '                            '                totBasicAmount += obj.AMOUNT + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
    '                            '                obj.Net_AMOUNT += +IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
    '                            '                totAmountwithPaymentCommssion += obj.Net_AMOUNT '+ totincentiveEMP '+ incentive(1)
    '                            '                sQuery = "Update tspl_Milk_Purchase_Invoice_Detail set Total_Amount='" & clsCommon.myCdbl(obj.AMOUNT) & "',Total_Amount_Acc='" & clsCommon.myCdbl(obj.Net_AMOUNT) & "',Net_Amount='" & clsCommon.myCdbl(obj.Net_AMOUNT) & "',incentive='" & incentive(1) & "' , incentiveEMP='" & totincentiveEMP & "' where srn_code='" & obj.SRN_CODE & "'"
    '                            '                clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
    '                            '                is_processed = 1
    '                            '            End If
    '                            '            'Exit For
    '                            '        Next
    '                            '        is_processed = 0
    '                            '        totAmount = objHead.Amount + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
    '                            '        totAmountwithPaymentCommssion = objHead.ACC_Amount + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)

    '                            '        'totincentiveEMP += clsCommon.myCdbl(gv1.Rows(0).Cells(colIncentiveEMP).Value)
    '                            '        sQuery = "select * from tspl_Milk_Purchase_Invoice_Head where doc_code='" & clsCommon.myCstr(objHead.DOC_CODE) & "'"
    '                            '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
    '                            '        'sQuery = "Update tspl_Milk_Purchase_Invoice_Head set Total_Amount=convert(Total_Amount as float)+" & clsCommon.myCdbl(totAmount) & ",Total_Amount_Acc=convert(Total_Amount_Acc  as float)+" & clsCommon.myCdbl(totAmountwithPaymentCommssion) & " ,incentive_Head='" & incentive(1) & "' , incentiveEMP_Head='" & totincentiveEMP & "' where doc_code='" & clsCommon.myCstr(objHead.DOC_CODE) & "'"
    '                            '        sQuery = "Update tspl_Milk_Purchase_Invoice_Head set Total_Amount='" & clsCommon.myCdbl(totAmount) & "',Total_Amount_Acc='" & clsCommon.myCdbl(totAmountwithPaymentCommssion) & "' ,incentive_Head='" & incentive(1) & "' , incentiveEMP_Head='" & totincentiveEMP & "' where doc_code='" & clsCommon.myCstr(objHead.DOC_CODE) & "'"
    '                            '        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
    '                            '    End If
    '                            'End If
    '                            clsMilkPurchaseInvoiceMCC.PostData("M-PURINVOICE", objHead.DOC_CODE, trans)
    '                        End If
    '                        ' Return True
    '                    End If


    '                End If
    '            End If
    'a:      Next


    '    End Sub

    Sub Generate_Vsp_Issue_Debit_Note(ByVal strMCCCode As String, ByVal DebitCreditNoteType As String)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strMCCCode) <= 0 Then
                Throw New Exception("Please Select MCC To Generate Bill")
            End If
            If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
                Throw New Exception("Please Select VSP To Generate Bill")
            End If

            Dim Dt As DataTable
            Dim sQuery As String = String.Empty
            Dim Frm_Date As Date = CDate("01-" & MonthName(txtMonth.Value.Month, True) & "-" & txtMonth.Value.Year)
            Dim To_Date As Date
            If txtMonth.Value.Month = 1 Then
                To_Date = CDate("" & Date.DaysInMonth(txtMonth.Value.Year - 1, txtMonth.Value.Month) & "-" & MonthName(12, True) & "-" & txtMonth.Value.Year - 1)
            Else
                To_Date = CDate("" & Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month - 1) & "-" & MonthName(txtMonth.Value.Month - 1, True) & "-" & txtMonth.Value.Year)
            End If
            Dim Srn_No_List As New List(Of String)
            If (clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal) Then
                objCommonVar.SelectedUser = "All"
                objCommonVar.SelectedUser = clsCommon.GetMulcallString(txtVSP.arrValueMember)
            Else
                objCommonVar.SelectedUser = objCommonVar.CurrentUserCode
            End If

            Dim doc_type As String = ""
            If clsCommon.CompairString(DebitCreditNoteType, "D") = CompairStringResult.Equal Then
                doc_type = "Issue"
            ElseIf clsCommon.CompairString(DebitCreditNoteType, "C") = CompairStringResult.Equal Then
                doc_type = "Return"
            Else
                Throw New Exception("Document Type must be D or C")
            End If
            For Each VSP As String In txtVSP.arrValueMember
                Srn_No_List.Clear()
                Frm_Date = To_Date.AddDays(1)
                To_Date = To_Date.AddDays(Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month))
                sQuery = "select Distinct TSPL_VSPItem_HEAD. Doc_No as doc_code from TSPL_VSPItem_HEAD  where  Coalesce(Is_debit_Note_Created,0)=0 and " _
                & " Issue_To='" & VSP & "' AND From_Location='" & strMCCCode & "' AND convert(date,DOC_DATE,103) >=convert(date,'" & Frm_Date & "',103)  and " _
                & " convert(date,DOC_DATE,103) <=convert(date,'" & To_Date & "',103) and doc_type='" & doc_type & "' and TSPL_VSPItem_HEAD.status=1"
                Dt = clsDBFuncationality.GetDataTable(sQuery, trans)
                If Dt.Rows.Count > 0 Then
                    For Each row As DataRow In Dt.Rows()
                        Dim objvsp As New clsVSPItemIssue
                        objvsp.SaveDebitNoteEntry(clsCommon.myCstr(row("Doc_Code")), DebitCreditNoteType, trans)
                        sQuery = "update TSPL_VSPItem_HEAD set is_debit_note_Created=1 where Doc_No='" & clsCommon.myCstr(row("Doc_Code")) & "'"
                        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                    Next
                End If

                Frm_Date = CDate("01-" & MonthName(txtMonth.Value.Month, True) & "-" & txtMonth.Value.Year)
                If txtMonth.Value.Month = 1 Then
                    To_Date = CDate("" & Date.DaysInMonth(txtMonth.Value.Year - 1, txtMonth.Value.Month) & "-" & MonthName(12, True) & "-" & txtMonth.Value.Year - 1)
                Else
                    To_Date = CDate("" & Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month - 1) & "-" & MonthName(txtMonth.Value.Month - 1, True) & "-" & txtMonth.Value.Year)
                End If
            Next
            trans.Commit()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            trans.Rollback()
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmMilkVSPPayment_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnDeleteVSPBill.Visible = True
            End If
        End If
    End Sub

    Private Sub BtnIncentive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnIncentive.Click
        'Try
        '    If SettMultipleMCCFinder Then
        '        For Each strMCC As String In txtMCCMultiple.arrValueMember
        '            IncentiveGenerationMCCWise(strMCC)
        '        Next
        '    Else
        '        IncentiveGenerationMCCWise(txtMCC.Value)
        '    End If
        '    clsCommon.MyMessageBoxShow("Data Saved Successfully.")
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        'End Try
    End Sub

    'Sub IncentiveGenerationMCCWise(ByVal strMCCCode As String)
    '    For Each row As String In txtVSP.arrValueMember
    '        If DtIncentive.Select("code='" & row & "'").Count > 0 Then
    '            If DtIncentive.Select("code='" & row & "'")(0)("Scheme_For") = "Month" Or DtIncentive.Select("code='" & row & "'")(0)("Scheme_For") = "Year" Then
    '                If clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy") < CDate("" & Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & "-" & MonthName(txtMonth.Value.Month, True) & "-" & txtMonth.Value.Year) Then
    '                    clsCommon.MyMessageBoxShow("Vsp : [" & row & "] Incentive is Month Based.You can Generate it After Completion of Month.")
    '                Else
    '                    Generate_Bill_Month_Year_Wise(strMCCCode)
    '                End If
    '            Else
    '                Generate_Bill(strMCCCode, False)
    '            End If
    '        End If
    '    Next
    'End Sub

    'Sub Generate_Bill_Month_Year_Wise(ByVal strMCCCode As String)
    '    Try
    '        If clsCommon.myLen(strMCCCode) <= 0 Then
    '            Throw New Exception("Please Select MCC To Generate Bill")
    '        End If
    '        If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
    '            Throw New Exception("Please Select VSP To Generate Bill")
    '        End If
    '        Dim Dt As DataTable
    '        Dim sQuery As String = String.Empty
    '        Dim Frm_Date As Date = CDate("01-" & MonthName(txtMonth.Value.Month, True) & "-" & txtMonth.Value.Year)
    '        Dim To_Date As Date = CDate("" & Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & "-" & MonthName(txtMonth.Value.Month, True) & "-" & txtMonth.Value.Year)
    '        Dim Srn_No_List As New List(Of String)

    '        If (clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal) Then
    '            objCommonVar.SelectedUser = "All"
    '            If (txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0) Then
    '                objCommonVar.SelectedUser = clsCommon.GetMulcallString(txtVSP.arrValueMember)
    '            End If
    '        Else
    '            objCommonVar.SelectedUser = objCommonVar.CurrentUserCode
    '        End If

    '        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '        For Each VSP As String In txtVSP.arrValueMember
    '            Srn_No_List.Clear()
    '            sQuery = "select TSPL_MILK_SRN_Head. DOC_CODE from TSPL_MILK_SRN_Head inner join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_Head.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE " _
    '            & " left join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE " _
    '            & " and TSPL_MILK_PURCHASE_INVOICE_DETAIL.item_code=TSPL_MILK_srn_DETAIL.Item_Code where  SRN_CODE is Not null and Is_Incentive_Created='N' and  " _
    '            & " VSP_coDE='" & VSP & "' AND TSPL_MILK_SRN_Head.mcc_coDE='" & strMCCCode & "' AND convert(date,DOC_DATE,103) >=convert(date,'" & Frm_Date & "',103) " _
    '            & " and convert(date,DOC_DATE,103) <=convert(date,'" & To_Date & "',103)"
    '            Dt = clsDBFuncationality.GetDataTable(sQuery, trans)
    '            If Dt IsNot Nothing AndAlso Dt.Rows.Count > 0 Then
    '                For Each row As DataRow In Dt.Rows()
    '                    Srn_No_List.Add(row("Doc_Code"))
    '                Next
    '                Dim objMilkPI As New frmMilkPurchaseInvoiceMCC
    '                objMilkPI.SelectMilkSRNItemsForVspPayment(Srn_No_List, VSP, Frm_Date, To_Date, False, trans)
    '                For Each row As DataRow In Dt.Rows()
    '                    sQuery = "update tspl_Milk_srn_Head set is_Incentive_Created='Y' where Doc_Code='" & row("Doc_Code") & "'"
    '                    clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
    '                Next
    '            End If

    '            Frm_Date = CDate("01-" & MonthName(txtMonth.Value.Month, True) & "-" & txtMonth.Value.Year)
    '            To_Date = CDate("" & Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & "-" & MonthName(txtMonth.Value.Month, True) & "-" & txtMonth.Value.Year)
    '        Next
    '        trans.Commit()

    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Private Sub DtpDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMonth.ValueChanged
        Try
            AllowDateChanged = False
            txtFromDate.MinDate = "01-Jan-0001"
            txtFromDate.MaxDate = Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & "-" & txtMonth.Value.Month & "-" & txtMonth.Value.Year
            txtFromDate.MinDate = "01-" & txtMonth.Value.Month & "-" & txtMonth.Value.Year
            txtToDate.Value = Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & "-" & txtMonth.Value.Month & "-" & txtMonth.Value.Year
            AllowDateChanged = True
            txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtMCC.Value, txtToDate.Value)
            txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtMCC.Value, txtToDate.Value)
            If SettMultipleMCCFinder Then
                If MultipleFinderFillAuto Then
                    AutoFillAllVSP()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DtpFromDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFromDate.ValueChanged
        SetToDate()
    End Sub

    Sub SetToDate()
        Try
            If AllowDateChanged Then
                If Is_Load = False Then
                    If clsCommon.myLen(txtMCC.Value) <= 0 Then
                        txtMCC.Focus()
                        Throw New Exception("Please select Mcc First.")
                    End If
                End If

                Dim sQuery As String = "select Pc_Type as Type,PC_VALUE as Value, case when Pc_Type='Day' then PC_VALUE when PC_Type='Month' then PC_Value * " & Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & " end " _
              & " as Pc_Value from tspl_Mcc_master inner join TSPL_PAYMENT_CYCLE_MASTER  on tspl_Mcc_master.payment_cycle=TSPL_PAYMENT_CYCLE_MASTER.PC_CODE where Mcc_code='" & txtMCC.Value & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please set payment cycle in Mcc master")
                End If
                lblPaymentType.Text = clsCommon.myCstr(dt.Rows(0)("Type"))
                lblPaymentType.Tag = clsCommon.myCdbl(dt.Rows(0)("Value"))
                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Type")), "Week") = CompairStringResult.Equal Then
                    AllowDateChanged = False
                    txtMonth.Enabled = False
                    txtFromDate.MinDate = New Date(2000, 1, 1)
                    txtFromDate.MaxDate = New Date(3000, 1, 1).AddDays(-1)
                    Dim today As Date = txtFromDate.Value
                    Dim dayDiff As Integer = today.DayOfWeek - IIf(lblPaymentType.Tag = 1, DayOfWeek.Sunday, IIf(lblPaymentType.Tag = 2, DayOfWeek.Monday, IIf(lblPaymentType.Tag = 3, DayOfWeek.Tuesday, IIf(lblPaymentType.Tag = 4, DayOfWeek.Wednesday, IIf(lblPaymentType.Tag = 5, DayOfWeek.Thursday, IIf(lblPaymentType.Tag = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                    txtFromDate.Value = today.AddDays(-dayDiff)
                    txtToDate.Value = txtFromDate.Value.AddDays(6)
                    AllowDateChanged = True
                Else
                    txtMonth.Enabled = True
                    Dim PaymentCycleValue As Integer = dt.Rows(0)("Pc_Value")
                    If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                        AllowDateChanged = False
                        clsCommon.MyMessageBoxShow("Invalid date.Date should be multiple of " & clsCommon.myCstr(PaymentCycleValue) & " + 1 ")
                        txtFromDate.Value = txtFromDate.MinDate
                        txtFromDate.Text = txtFromDate.MinDate
                        AllowDateChanged = True
                    End If
                    txtToDate.Value = txtFromDate.Value.AddDays(PaymentCycleValue - 1)
                    If txtFromDate.Value.Month <> txtToDate.Value.Month Then
                        txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                    End If
                    Dim dtNxtPay As DateTime = txtToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                    If txtFromDate.Value.Month <> dtNxtPay.Month Then
                        txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                    End If
                End If
                txtVSP.arrValueMember = Nothing
            End If
            fndDocNo.Value = txtMCC.Value + "#$#" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtMCC.Value, txtToDate.Value)
            txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtMCC.Value, txtToDate.Value)
            If SettMultipleMCCFinder Then
                If MultipleFinderFillAuto Then
                    AutoFillAllVSP()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        Dim qry As String = ""
        Dim arrLoc As String = ""
        Dim obj As New clsMCCCodes()
        obj = clsMCCCodes.GetData(True)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
            arrLoc = obj.arrLocCodes
        End If

        qry = "select * from ( select Mcc_Code as [Code],MCC_Name as [Name] from tspl_mcc_master inner join tspl_location_master on tspl_location_master.location_Code= tspl_mcc_master.mcc_Code " _
        & " and (tspl_location_master.loc_segment_Code in (" & arrLoc & ") or tspl_mcc_master.mcc_Code in (" & arrLoc & ")))xx "
        If AreaWiseBilling Then
            qry += " and tspl_mcc_master.Area_Location_Code ='" + clsCommon.myCstr(fndArea.Value) + "' "

        End If
        txtMCC.Value = clsCommon.ShowSelectForm("VSPPMCCFn", qry, "Code", "", txtMCC.Value, "", isButtonClicked)
        qry = "select Non_Company_VSP_Deduction,Company_VSP_Deduction,MCC_Name from tspl_mcc_master where mcc_Code='" + txtMCC.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            lblMCC.Text = clsCommon.myCstr(dt.Rows(0)("MCC_Name"))
            CompanyVSPDeduction = clsCommon.myCdbl(dt.Rows(0)("Company_VSP_Deduction"))
            NonCompanyVSPDeduction = clsCommon.myCdbl(dt.Rows(0)("Non_Company_VSP_Deduction"))
        Else
            lblMCC.Text = ""
            CompanyVSPDeduction = 0
            NonCompanyVSPDeduction = 0
        End If
        SetToDate()
        txtVSP.arrValueMember = Nothing
        txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtMCC.Value, txtToDate.Value)
        txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtMCC.Value, txtToDate.Value)

    End Sub


    Private Sub txtMainGroup__My_Click(sender As Object, e As EventArgs) Handles txtVSP._My_Click
        Try
            If SettMultipleMCCFinder Then
                If txtMCCMultiple.arrValueMember IsNot Nothing AndAlso txtMCCMultiple.arrValueMember.Count <= 0 Then
                    txtMCCMultiple.Focus()
                    Throw New Exception("Please select MCC")
                End If
            Else
                If clsCommon.myLen(txtMCC.Value) <= 0 Then
                    txtMCC.Focus()
                    Throw New Exception("Please select MCC")
                End If
            End If
            Dim arrMCC As New ArrayList
            If SettMultipleMCCFinder Then
                arrMCC = txtMCCMultiple.arrValueMember
            Else
                arrMCC.Add(txtMCC.Value)
            End If
            Dim qry As String = VSPQry(arrMCC)
            txtVSP.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "VSPPVLF", qry, "Code", "", txtVSP.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function VSPQry(ByVal arrMCC As ArrayList) As String
        Return VSPQry(arrMCC, Nothing)
    End Function
    Function VSPQry(ByVal arrMCC As ArrayList, ByVal arrVSP As ArrayList) As String
        Return clsVSPBillAndIncentiveCalculation.VSPQry(arrMCC, arrVSP, isPickPendingMilkSRNinNextPaymentCycle, txtFromDate.Value, txtToDate.Value, Formcode)
    End Function

    Private Sub txtFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        SetToDate()
    End Sub

    Private Sub CreateAssetEMIOFVSP(ByVal strMCCCode As String, ByVal strVSPCode As String, ByVal trans As SqlTransaction)
        Dim dtMain As DataTable = clsDBFuncationality.GetDataTable(clsAPInvoiceAssetEMIDetails.GetVSPAssetEMIQuery(strVSPCode), trans)
        If dtMain IsNot Nothing AndAlso dtMain.Rows.Count > 0 Then
            Dim objVendorInvHead As New clsVedorInvoiceHead()
            'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            objVendorInvHead.Vendor_Code = strVSPCode
            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(strVSPCode, trans)
            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
            objVendorInvHead.Invoice_Type = "AP"
            objVendorInvHead.Vendor_Invoice_Date = txtToDate.Value
            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(strMCCCode, trans) 'obj.MCC_CODE
            'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE
            objVendorInvHead.Description = "AP Debit Note Against Installment Amount of VSP : " & objVendorInvHead.Vendor_Name & "(" + objVendorInvHead.Vendor_Code + ")"
            'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
            End If

            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
            ''objVendorInvHead.PO_Number = obj.p
            objVendorInvHead.isDeduction = 1

            '' ''added by priti
            ''objVendorInvHead.RefDocType = clsCommon.myCstr(cmbRefType.SelectedValue)
            ''objVendorInvHead.RefDocNo = txtRefDocNo.Text
            '' '' priti ends here
            'objVendorInvHead.Order_No = txtOrderNo.Text
            ' objVendorInvHead.Total_Tax = 0

            objVendorInvHead.On_Hold = False
            'Dim srndate As String = ""
            'Dim srncode As String = ""
            'Dim Vlc_Code As String = ""
            'Dim Vlc_Name As String = ""
            'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
            '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
            '        Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
            '        Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
            '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"))
            '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
            '        Vlc_Code = IIf(Vlc_Code = "", Dt_SRN.Rows(0).Item("VLC_Code").ToString, Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
            '        Vlc_Name = IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
            '    End If
            'Next



            'objVendorInvHead.Description = "VSP : " + obj.VSP_CODE + "/" + vendor_name + "VLC : " + Vlc_Code + "/" + Vlc_Name + " .Against PI Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
            'objVendorInvHead.Tax_Calculation_Type = Nothing
            'objVendorInvHead.Tax_Group = Nothing
            'If (clsCommon.myLen(obj.TAX1) > 0) Then
            '    objVendorInvHead.TAX1 = obj.TAX1
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
            '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
            '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
            '    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
            '    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX2) > 0) Then
            '    objVendorInvHead.TAX2 = obj.TAX2
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
            '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
            '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
            '    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
            '    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX3) > 0) Then
            '    objVendorInvHead.TAX3 = obj.TAX3
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
            '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
            '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
            '    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
            '    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX4) > 0) Then
            '    objVendorInvHead.TAX4 = obj.TAX4
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
            '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
            '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
            '    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
            '    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX5) > 0) Then
            '    objVendorInvHead.TAX5 = obj.TAX5
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
            '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
            '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
            '    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
            '    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX6) > 0) Then
            '    objVendorInvHead.TAX6 = obj.TAX6
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
            '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
            '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
            '    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
            '    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX7) > 0) Then
            '    objVendorInvHead.TAX7 = obj.TAX7
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
            '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
            '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
            '    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
            '    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX8) > 0) Then
            '    objVendorInvHead.TAX8 = obj.TAX8
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
            '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
            '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
            '    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
            '    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX9) > 0) Then
            '    objVendorInvHead.TAX9 = obj.TAX9
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
            '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
            '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
            '    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
            '    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX10) > 0) Then
            '    objVendorInvHead.TAX10 = obj.TAX10
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
            '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
            '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
            '    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
            '    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
            'End If

            'objVendorInvHead.Terms_Code = obj.Terms_Code
            'objVendorInvHead.Terms_Description = obj.TermsName
            objVendorInvHead.Due_Date = txtToDate.Value

            'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
            'objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE
            objVendorInvHead.RefDocType = "VSP-AIE"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, strMCCCode, trans)
                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, strMCCCode, trans)
                End If
            End If
            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If

            'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

            'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
            'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
            'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

            'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
            'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
            'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

            'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
            'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
            'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

            'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
            'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
            'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

            'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
            'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
            'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

            'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
            'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
            'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

            'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
            'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
            'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

            'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
            'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
            'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

            'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
            'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
            'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

            'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
            'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
            'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
            Dim ii As Integer = 0
            Dim isFirstTime As Boolean = True
            ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
            'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
            objVendorInvHead.Total_Landed_Amt = 0

            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()

            For Each dr As DataRow In dtMain.Rows
                ''Set Installment amount
                Dim objAEMI As New clsAPInvoiceAssetEMIDetails
                objAEMI.Asset_Issue_No = clsCommon.myCstr(dr("Doc_No"))
                objAEMI.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objAEMI.Installment_Amount = clsCommon.myCstr(dr("Installment_Amt"))
                objVendorInvHead.ArrAssetEMI.Add(objAEMI)
                ''end of Set Installment amount

                ''Set AP Invvoice Detail Table
                Dim qry As String = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_ITEM_MASTER.Two_Count_Status as isEmpty,TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing where TSPL_ITEM_MASTER.Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please set Purchase Account set for item " + clsCommon.myCstr(dr("Item_Code")))
                End If
                Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
                strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, strMCCCode, trans)
                If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                    Throw New Exception("Please set Inventory Control Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + clsCommon.myCstr(dr("Item_Code")))
                End If


                Dim objVendorInvDetail As New clsVedorInvoiceDetail()

                Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code from TSPL_DEDUCTION_MASTER  where Is_Default_Asset_Installment=1", trans)
                If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                    Throw New Exception("Please set default Asset Installment deduction code")
                End If
                objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))


                ii = ii + 1
                objVendorInvDetail.Detail_Line_No = ii
                objVendorInvDetail.GL_Account_Code = strInvCtrlAC
                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(strInvCtrlAC, trans)
                objVendorInvDetail.Amount = clsCommon.myCdbl(dr("Installment_Amt"))
                objVendorInvDetail.Discount_Per = 0
                objVendorInvDetail.Discount = 0
                objVendorInvDetail.Amount_less_Discount = objVendorInvDetail.Amount
                objVendorInvDetail.Total_Tax = 0
                objVendorInvDetail.Total_Amount = objVendorInvDetail.Amount
                objVendorInvDetail.Landed_Amount = objVendorInvDetail.Amount
                ''End of Set AP Invvoice Detail Table

                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                End If

                ''Set AP Invvoice Header Table
                objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount
                objVendorInvHead.Discount_Base += objVendorInvDetail.Amount
                objVendorInvHead.Discount_Amount += 0
                objVendorInvHead.Amount_Less_Discount += objVendorInvDetail.Amount
                objVendorInvHead.Document_Total += objVendorInvDetail.Amount
                objVendorInvHead.Balance_Amt += objVendorInvDetail.Amount
                ''End of Set AP Invvoice Header Table
            Next


            objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
            If objVendorInvHead.Empty_Amount > 0 Then
                If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                    Throw New Exception("Please set Inventory Control Empties")
                End If
                objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
            End If


            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                Throw New Exception("No GL Account Found For AP Invoice")
            End If
            ''multicurrency
            'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
            'objVendorInvHead.ConvRate = 1
            objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            ''end multicurrency

            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, txtToDate.Value)


        End If
    End Sub

    Private Sub btnDeleteVSPBill_Click(sender As Object, e As EventArgs) Handles btnDeleteVSPBill.Click
        Try
            UpdateSTDQtyOFSRN()
            If SettMultipleMCCFinder Then
                For Each strMCC As String In txtMCCMultiple.arrValueMember
                    BillDeleteMCCWise(strMCC)
                Next
            Else
                BillDeleteMCCWise(txtMCC.Value)
            End If
            clsCommon.MyMessageBoxShow(Me, "Task Completed", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub BillDeleteMCCWise(ByVal strMCCCode As String)
        Try
            If clsCommon.myLen(strMCCCode) <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please select MCC")
            End If

            Dim qry As String = clsPaymentProcessHead.GetNoOfDoc(strMCCCode, txtToDate.Value)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If MultipleFinderFillAuto Then
                    clsPaymentProcessHead.DeleteOnlyBill(strMCCCode, txtToDate.Value)
                Else
                    If clsCommon.MyMessageBoxShow("Delete " + clsCommon.myCstr(dt.Rows.Count) + "Milk Invoice of MCC [" + strMCCCode + "] which is not used in Payment Process" + Environment.NewLine + "Are You sure", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        clsPaymentProcessHead.DeleteOnlyBill(strMCCCode, txtToDate.Value)
                    End If
                End If
            Else
                Throw New Exception("No Milk Invoice found to delete")
            End If
        Catch ex As Exception
            If Not MultipleFinderFillAuto Then
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub

    Private Sub txtMCCMultiple__My_Click(sender As Object, e As EventArgs) Handles txtMCCMultiple._My_Click
        Try
            Dim qry As String = ""
            Dim arrLoc As String = ""
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData(True)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

            'qry = "select * from ( select Mcc_Code as [Code],MCC_Name as [Name], Plant_Code as PlantCode,tabPlantName.Location_Desc as Plant from tspl_mcc_master left outer join TSPL_LOCATION_MASTER as tabPlantName on tabPlantName.Location_Code=TSPL_MCC_MASTER.Plant_Code inner join tspl_location_master on tspl_location_master.location_Code= tspl_mcc_master.mcc_Code " _
            '& " and (tspl_location_master.loc_segment_Code in (" & arrLoc & ") or tspl_mcc_master.mcc_Code in (" & arrLoc & ")))xx "

            qry = "select Mcc_Code as Code,MCC_Name,Plant_Code,tabPlantName.Location_Desc as Plant
                    from tspl_mcc_master left outer join TSPL_LOCATION_MASTER as tabPlantName on tabPlantName.Location_Code=TSPL_MCC_MASTER.Plant_Code  where tspl_mcc_master.mcc_Code in (" & arrLoc & ")"
            If AreaWiseBilling Then
                qry += " and tspl_mcc_master.

='" + clsCommon.myCstr(fndArea.Value) + "' "

            End If
            'If fndArea.Value IsNot Nothing AndAlso fndArea.Value.Count > 0 Then
            '    qry += " and tspl_mcc_master.Area_Location_Code ='" + clsCommon.myCstr(fndArea.Value) + "' "
            'End If
            'txtMCCMultiple.arrValueMember = clsCommon.ShowMultipleSelectForm("MULVSPPMF", qry, "Code", "", txtMCCMultiple.arrValueMember, txtMCCMultiple.arrDispalyMember)


            txtMCCMultiple.arrValueMember = clsCommon.ShowMultipleSelectForm("MULVSPPMF", qry, "Code", "", txtMCCMultiple.arrValueMember, txtMCCMultiple.arrDispalyMember)
            If txtMCCMultiple.arrValueMember IsNot Nothing AndAlso txtMCCMultiple.arrValueMember.Count > 0 Then
                qry = "select TSPL_PAYMENT_CYCLE_MASTER.PC_CODE  from tspl_Mcc_master inner join TSPL_PAYMENT_CYCLE_MASTER  on tspl_Mcc_master.payment_cycle=TSPL_PAYMENT_CYCLE_MASTER.PC_CODE  where Mcc_code in (" + clsCommon.GetMulcallString(txtMCCMultiple.arrValueMember) + ") group by TSPL_PAYMENT_CYCLE_MASTER.PC_CODE"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 1 Then
                    txtMCCMultiple.arrValueMember = Nothing
                    Throw New Exception("Payment Cycle should be same for all MCC")
                Else
                    txtMCC.Value = txtMCCMultiple.arrValueMember(0)
                    SetToDate()
                End If
            End If
            txtVSP.arrValueMember = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub FillAllMCCDefault()
        Try
            Dim arr As ArrayList = Nothing
            Dim qry As String = ""
            Dim arrLoc As String = ""
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData(True)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

            qry = "select * from ( select Mcc_Code as [Code],MCC_Name as [Name], Plant_Code as PlantCode,tabPlantName.Location_Desc as Plant from tspl_mcc_master left outer join TSPL_LOCATION_MASTER as tabPlantName on tabPlantName.Location_Code=TSPL_MCC_MASTER.Plant_Code inner join tspl_location_master on tspl_location_master.location_Code= tspl_mcc_master.mcc_Code " _
            & " and (tspl_location_master.loc_segment_Code in (" & arrLoc & ") or tspl_mcc_master.mcc_Code in (" & arrLoc & ")))xx "

            Dim dtMCC As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dtMCC IsNot Nothing AndAlso dtMCC.Rows.Count > 0 Then
                arr = New ArrayList()
                For Each dr As DataRow In dtMCC.Rows
                    arr.Add(clsCommon.myCstr(dr("Code")))
                Next
                txtMCCMultiple.arrValueMember = arr
            End If
            'txtMCCMultiple.arrValueMember = clsCommon.ShowMultipleSelectForm("MULVSPPMF", qry, "Code", "", txtMCCMultiple.arrValueMember, Nothing)
            If txtMCCMultiple.arrValueMember IsNot Nothing AndAlso txtMCCMultiple.arrValueMember.Count > 0 Then
                qry = "select TSPL_PAYMENT_CYCLE_MASTER.PC_CODE  from tspl_Mcc_master inner join TSPL_PAYMENT_CYCLE_MASTER  on tspl_Mcc_master.payment_cycle=TSPL_PAYMENT_CYCLE_MASTER.PC_CODE  where Mcc_code in (" + clsCommon.GetMulcallString(txtMCCMultiple.arrValueMember) + ") group by TSPL_PAYMENT_CYCLE_MASTER.PC_CODE"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 1 Then
                    txtMCCMultiple.arrValueMember = Nothing
                    Throw New Exception("Payment Cycle should be same for all MCC")
                Else
                    txtMCC.Value = txtMCCMultiple.arrValueMember(0)
                    SetToDate()
                End If
            End If
            txtVSP.arrValueMember = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub AutoFillAllVSP()
        Dim arr As ArrayList = Nothing
        Try
            If SettMultipleMCCFinder Then
                If txtMCCMultiple.arrValueMember IsNot Nothing AndAlso txtMCCMultiple.arrValueMember.Count <= 0 Then
                    'txtMCCMultiple.Focus()
                    'Throw New Exception("Please select MCC")
                    Return
                End If
            Else
                If clsCommon.myLen(txtMCC.Value) <= 0 Then
                    'txtMCC.Focus()
                    Return
                End If
            End If
            Dim arrMCC As New ArrayList
            If SettMultipleMCCFinder Then
                arrMCC = txtMCCMultiple.arrValueMember
            Else
                arrMCC.Add(txtMCC.Value)
            End If
            Dim qry As String = VSPQry(arrMCC)

            Dim dtVSP As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dtVSP IsNot Nothing AndAlso dtVSP.Rows.Count > 0 Then
                arr = New ArrayList()
                For Each dr As DataRow In dtVSP.Rows
                    arr.Add(clsCommon.myCstr(dr("Code")))
                Next
                txtVSP.arrValueMember = arr
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnProvisionBill_Click(sender As Object, e As EventArgs) Handles btnProvisionBill.Click
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkVSPPayment, txtMCC.Value, txtFromDate.Value, Nothing)

            'UpdateSTDQtyOFSRN()
            'If SettMultipleMCCFinder Then
            '    For Each strMCC As String In txtMCCMultiple.arrValueMember
            '        BillGenerationMCCWiseProvision(strMCC, clsMccMaster.GetName(strMCC, Nothing))
            '    Next
            'Else
            '    BillGenerationMCCWiseProvision(txtMCC.Value, lblMCC.Text)
            'End If
            clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully.", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Sub BillGenerationMCCWiseProvision(ByVal strMCCCode As String, ByVal strMCCName As String)
    '    If clsCommon.myLen(strMCCCode) <= 0 Then
    '        Throw New Exception("Please Select MCC To Generate Bill")
    '    End If
    '    If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
    '        Throw New Exception("Please Select VSP To Generate Bill")
    '    End If
    '    Generate_Bill_Payment_cycle_wiseProvision(strMCCCode, True)
    'End Sub

    'Sub Generate_Bill_Payment_cycle_wiseProvision(ByVal strMCCCode As String, ByVal is_with_bill As Boolean)
    '    Dim trans As SqlTransaction = Nothing
    '    Try
    '        clsCommon.ProgressBarPercentShow()
    '        If clsCommon.myLen(strMCCCode) <= 0 Then
    '            Throw New Exception("Please Select MCC To Generate Bill")
    '        End If
    '        If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
    '            Throw New Exception("Please Select VSP To Generate Bill")
    '        End If
    '        Dim dt As DataTable
    '        Dim qry As String = String.Empty
    '        Dim Payment_Cycle_value As Integer = 0
    '        Dim Srn_No_List As New List(Of String)

    '        If (clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal) Then
    '            objCommonVar.SelectedUser = "All"
    '            objCommonVar.SelectedUser = clsCommon.GetMulcallString(txtVSP.arrValueMember)
    '        Else
    '            objCommonVar.SelectedUser = objCommonVar.CurrentUserCode
    '        End If

    '        Dim counter As Integer = 0
    '        If clsCommon.CompairString(lblPaymentType.Text, "Week") = CompairStringResult.Equal Then
    '            If Not txtFromDate.Value.DayOfWeek = IIf(lblPaymentType.Tag = 1, DayOfWeek.Sunday, IIf(lblPaymentType.Tag = 2, DayOfWeek.Monday, IIf(lblPaymentType.Tag = 3, DayOfWeek.Tuesday, IIf(lblPaymentType.Tag = 4, DayOfWeek.Wednesday, IIf(lblPaymentType.Tag = 5, DayOfWeek.Thursday, IIf(lblPaymentType.Tag = 6, DayOfWeek.Friday, DayOfWeek.Saturday)))))) Then
    '                Throw New Exception("From Date Day of week should be " + IIf(lblPaymentType.Tag = 1, "Sunday", IIf(lblPaymentType.Tag = 2, "Monday", IIf(lblPaymentType.Tag = 3, "Tuesday", IIf(lblPaymentType.Tag = 4, "Wednesday", IIf(lblPaymentType.Tag = 5, "Thursday", IIf(lblPaymentType.Tag = 6, "Friday", "Saturday")))))))
    '            End If
    '        Else
    '            qry = "select case when Pc_Type='Day' then PC_VALUE when PC_Type='Month' then PC_Value * " & Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & " end " _
    '        & " as Pc_Value from tspl_Mcc_master inner join TSPL_PAYMENT_CYCLE_MASTER  on tspl_Mcc_master.payment_cycle=TSPL_PAYMENT_CYCLE_MASTER.PC_CODE where Mcc_code='" & strMCCCode & "'"
    '            Payment_Cycle_value = clsDBFuncationality.getSingleValue(qry, trans)
    '            If Payment_Cycle_value <= 0 Then
    '                Throw New Exception("Please Set Payment Cycle on Mcc [" & strMCCCode & "]")
    '            End If
    '        End If

    '        If txtToDate.Value.Date > clsCommon.GETSERVERDATE(trans).Date Then
    '            Throw New Exception("To Date is Greate than Server Date")
    '        End If

    '        Dim arrMCC As New ArrayList
    '        arrMCC.Add(strMCCCode)
    '        Dim dtVSP As DataTable = clsDBFuncationality.GetDataTable(VSPQry(arrMCC, txtVSP.arrValueMember))
    '        If dtVSP IsNot Nothing AndAlso dtVSP.Rows.Count > 0 Then
    '            For Each drVSP As DataRow In dtVSP.Rows
    '                Dim VSP As String = clsCommon.myCstr(drVSP("Code"))
    '                counter += 1
    '                trans = clsDBFuncationality.GetTransactin()
    '                Try
    '                    Srn_No_List.Clear()
    '                    qry = "select Distinct TSPL_MILK_SRN_Head. DOC_CODE from TSPL_MILK_SRN_Head inner join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_Head.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE " _
    '                           & "  where  Coalesce(TSPL_MILK_SRN_Head.Is_Incentive_Created,'N')='N' and " _
    '                           & " TSPL_MILK_SRN_Head.VSP_coDE='" & VSP & "' AND TSPL_MILK_SRN_Head.mcc_coDE='" & strMCCCode & "' "
    '                    If isPickPendingMilkSRNinNextPaymentCycle Then
    '                    Else
    '                        qry += " AND convert(date,DOC_DATE,103) >=convert(date,'" & txtFromDate.Value.Date & "',103) "
    '                    End If
    '                    qry += " and convert(date,DOC_DATE,103) <=convert(date,'" & txtToDate.Value.Date & "',103)"


    '                    dt = clsDBFuncationality.GetDataTable(qry, trans)
    '                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                        Dim strNo As String = ""
    '                        For Each row As DataRow In dt.Rows()
    '                            Srn_No_List.Add(row("Doc_Code"))
    '                            strNo += clsCommon.myCstr(row("Doc_Code")) + ","
    '                        Next
    '                        clsCommon.ProgressBarPercentUpdate(((counter - 1) * 100 / txtVSP.arrValueMember.Count), "MCC-" + strMCCCode + " VSP-" & counter & "/" & txtVSP.arrValueMember.Count & " VSP-" + VSP + " SRN-" + strNo)
    '                        Dim strr As String = clsDBFuncationality.getSingleValue("select coalesce(vsp_farmer_billing,0) FROM TSPL_Vendor_master where vendor_Code='" & VSP & "'", trans)
    '                        If (Formcode = clsUserMgtCode.MilkVSPPayment Or Formcode = clsUserMgtCode.MPBillGeneration Or strr <> "1") Then
    '                            SelectMilkSRNItemsForVspPaymentProvision(strMCCCode, Srn_No_List, VSP, txtFromDate.Value.Date, txtToDate.Value.Date, is_with_bill, trans)
    '                        End If
    '                    End If
    '                    trans.Commit()
    '                Catch ex As Exception
    '                    trans.Rollback()
    '                    Throw New Exception(ex.Message)
    '                End Try
    '            Next
    '        End If
    '        counter = 0
    '        clsCommon.ProgressBarPercentHide()
    '    Catch ex As Exception
    '        clsCommon.ProgressBarPercentHide()
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    'Public Sub SelectMilkSRNItemsForVspPaymentProvision(ByVal strMCCCode As String, ByVal strSRN_No As List(Of String), ByVal Vsp_Name As String, ByVal frm_date As Date, ByVal End_date As Date, ByVal Is_With_Bill As Boolean, ByVal trans As SqlTransaction)
    '    Dim obj_SRN As New clsMilkSRNMCC
    '    Dim frm As New frmMILKPendingSRN()
    '    frm.VendorCode = Vsp_Name
    '    frm.Frm_date = frm_date
    '    frm.To_date = End_date
    '    Dim StrDoc As New List(Of String)
    '    If Is_With_Bill Then
    '        If Not frm.LoaDHeadDataQuery(trans) Then
    '            Exit Sub
    '        End If
    '    Else
    '        If Not frm.LoaDHeadDataQueryVsp(trans) Then
    '            Exit Sub
    '        End If
    '    End If
    '    For Each row As GridViewRowInfo In frm.gvHead.Rows()
    '        If strSRN_No.Contains(clsCommon.myCstr(row.Cells(frmMILKPendingSRN.colHCode).Value)) Then
    '            frm.gvHead.CurrentRow = row
    '            row.Cells(frmMILKPendingSRN.colHSelect).Value = True
    '        End If
    '    Next
    '    frm.btnOKPressed()
    '    If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
    '        If clsCommon.myLen(frm.ArrReturn(0).DOC_CODE) > 0 Then
    '            obj_SRN = clsMilkSRNMCC.GetData(frm.ArrReturn(0).DOC_CODE, NavigatorType.Current, trans)
    '            If obj_SRN IsNot Nothing AndAlso clsCommon.myLen(obj_SRN.DOC_CODE) > 0 Then
    '                Dim TotOwnAsset As Double = 0
    '                Dim TotDeduction_Amount As Double = 0
    '                Dim objHead As New clsMilkPurchaseInvoiceMCC
    '                objHead.Program_Code = Formcode
    '                objHead.FROM_DATE = txtFromDate.Value
    '                objHead.TO_DATE = txtToDate.Value
    '                objHead.DOC_CODE = ""
    '                objHead.DOC_DATE = clsCommon.myCDate(End_date)
    '                objHead.Description = ""
    '                objHead.ROUTE_CODE = clsCommon.myCstr(obj_SRN.ROUTE_CODE)
    '                objHead.VSP_CODE = clsCommon.myCstr(obj_SRN.VSP_CODE)
    '                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select vsp_Payment,Handling_Charges_Per from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & obj_SRN.VSP_CODE & "'", trans)
    '                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '                    Throw New Exception("VSP- " + obj_SRN.VSP_CODE + "Not exists in vsp master")
    '                End If
    '                objHead.Payment = clsCommon.myCstr(dt.Rows(0)("vsp_Payment"))
    '                objHead.Handling_Charges_Per = clsCommon.myCdbl(dt.Rows(0)("Handling_Charges_Per"))
    '                objHead.Handling_Charges_Amount = 0
    '                objHead.SRN_Net_Amount = 0
    '                objHead.SRN_RO_Amount = 0
    '                objHead.Irregular_MCC_CODE = clsCommon.myCstr(obj_SRN.MCC_CODE)

    '                Dim objList As New List(Of clsMilkPurchaseInvoiceMCCDetail)
    '                Dim objDetail As clsMilkPurchaseInvoiceMCCDetail = Nothing
    '                Dim sQuery As String = "select TSPL_MILK_Shift_End_DETAIL.*,TSPL_MILK_SRN_HEAD.doc_code as srn_code from TSPL_MILK_Shift_End_DETAIL inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_head.VLC_DOC_CODE=TSPL_MILK_Shift_End_DETAIL.VLC_DOC_CODE where  TSPL_MILK_Shift_End_DETAIL.MCC_CODE='" & clsCommon.myCstr(obj_SRN.MCC_CODE) & "' " _
    '                & "and convert(date,TSPL_MILK_Shift_End_DETAIL.DOC_DATE,103)='" & clsCommon.GetPrintDate(obj_SRN.DOC_DATE, "dd-MMM-yyyy") & "' and TSPL_MILK_Shift_End_DETAIL.SHIFT='" & IIf(clsCommon.myCstr(obj_SRN.SHIFT) = "M", "Morning", "Evening") & "'"

    '                Dim DtShiftEnd As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
    '                Dim totAmount As Double = 0
    '                Dim totCommssion As Double = 0
    '                objHead.Total_PaymentCommission = 0
    '                Dim totAmountwithPaymentCommssion As Double = 0
    '                Dim totAmountIncentive As Double = 0
    '                Dim totAmountIncentiveEMP As Double = 0
    '                Dim totBasicAmount As Double = 0
    '                objHead.Total_Head_Load_Amount = 0
    '                For Each obj1 As clsMilkSRNMCCDetail In frm.ArrReturn
    '                    objDetail = New clsMilkPurchaseInvoiceMCCDetail
    '                    objDetail.DOC_CODE = ""
    '                    objDetail.AMOUNT = clsCommon.myCdbl(obj1.AMOUNT)
    '                    objDetail.Cans = clsCommon.myCdbl(obj1.Cans)
    '                    objDetail.CLR = clsCommon.myCdbl(obj1.CLR)
    '                    objDetail.COMMISSION = clsCommon.myCdbl(obj1.Commission)
    '                    objDetail.Payment_COMMISSION = clsCommon.myCdbl(obj1.Payment_Commission)
    '                    If DtShiftEnd.Rows.Count > 0 Then
    '                        Dim dr() As DataRow = DtShiftEnd.Select("vlc_code='" & clsCommon.myCstr(obj1.VlC_Code) & "' and srn_code='" & clsCommon.myCstr(obj1.DOC_CODE) & "'")
    '                        If dr.Length > 0 Then
    '                            objDetail.Deduction = IIf(clsCommon.myCstr(dr(0)("A_Or_R")) = "R", clsCommon.myCdbl(obj1.AMOUNT) * clsCommon.myCdbl(dr(0)("Deduction_of_VSP")) / 100, clsCommon.myCdbl(dr(0)("Deduction_of_VSP")))
    '                        End If
    '                    End If
    '                    objDetail.Own_Asset_Amount = clsCommon.myCdbl(obj1.Own_Asset_Amount)
    '                    objDetail.Correction_Factor = clsCommon.myCdbl(obj1.Correction_Factor)
    '                    objDetail.FAT_PER = clsCommon.myCdbl(obj1.FAT)
    '                    objDetail.Item_Code = clsCommon.myCstr(obj1.Item_CODE)
    '                    objDetail.MCC_CODE = strMCCCode
    '                    objDetail.Qty = clsCommon.myCdbl(obj1.MILK_Qty)
    '                    objDetail.Acc_Qty = clsCommon.myCdbl(obj1.ACC_Qty)
    '                    objDetail.Service_Charge = clsCommon.myCstr(obj1.Service_Charge_Type)
    '                    objDetail.RATE = clsCommon.myCdbl(obj1.RATE)
    '                    objDetail.SNF_PER = clsCommon.myCdbl(obj1.SNF)
    '                    objDetail.Head_Load_Amount = clsCommon.myCdbl(obj1.Head_Load_Amount)
    '                    Dim Commission_AMount As Double = 0
    '                    objDetail.Service_Charge_Amount = Math.Round(obj1.Service_Charge_Amount, 2)
    '                    ''Extra column of SRN
    '                    dt = clsDBFuncationality.GetDataTable("select NET_AMOUNT,Round_Off from TSPL_MILK_SRN_DETAIL where DOC_CODE='" + obj1.DOC_CODE + "'", trans)
    '                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '                        Throw New Exception("Milk SRN No " + obj1.DOC_CODE + " not found")
    '                    End If

    '                    obj1.NET_AMOUNT = Math.Round(clsCommon.myCdbl(dt.Rows(0)("NET_AMOUNT")), 2)
    '                    obj1.Round_Off = Math.Round(clsCommon.myCdbl(dt.Rows(0)("Round_Off")), 2)

    '                    objDetail.SRN_Net_Amount = obj1.NET_AMOUNT
    '                    objDetail.SRN_RO_Amount = obj1.Round_Off

    '                    ''End of Extra column of SRN
    '                    objDetail.Net_AMOUNT = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select NET_AMOUNT from TSPL_MILK_SRN_DETAIL where DOC_CODE='" + obj1.DOC_CODE + "'", trans)), 2) ''Not coming in object and only one row is exists 
    '                    objDetail.Handling_Charges_Amount = Math.Round((objDetail.Net_AMOUNT) * objHead.Handling_Charges_Per / 100, 2)
    '                    objDetail.Net_AMOUNT += objDetail.Handling_Charges_Amount

    '                    objDetail.TOTAL_AMOUNT = Math.Round(objDetail.Net_AMOUNT + objDetail.Service_Charge_Amount, 2)
    '                    objDetail.SRN_CODE = clsCommon.myCstr(obj1.DOC_CODE)
    '                    objDetail.VEHICLE_NO = clsCommon.myCstr(obj_SRN.VEHICLE_CODE)
    '                    objDetail.VLC_NO = clsCommon.myCstr(obj1.VlC_Code)
    '                    objHead.MCC_CODE = clsMilkPurchaseInvoiceMCC.GetIrregular_Location(objDetail.SRN_CODE, trans)
    '                    objDetail.MCC_CODE = clsMilkPurchaseInvoiceMCC.GetIrregular_Location(objDetail.SRN_CODE, trans)
    '                    If clsCommon.CompairString(objHead.MCC_CODE, objHead.Irregular_MCC_CODE) = CompairStringResult.Equal Then
    '                        objHead.Irregular_MCC_CODE = ""
    '                    End If

    '                    objList.Add(objDetail)
    '                    objHead.Total_Head_Load_Amount += objDetail.Head_Load_Amount
    '                    TotOwnAsset += objDetail.Own_Asset_Amount
    '                    TotDeduction_Amount += (objDetail.Deduction)

    '                    totAmount += objDetail.AMOUNT
    '                    totBasicAmount += objDetail.AMOUNT
    '                    totCommssion += 0
    '                    Dim TotPaycomm As Decimal = Math.Round((objDetail.TOTAL_AMOUNT - objDetail.AMOUNT - objDetail.Handling_Charges_Amount + obj1.Round_Off), 2, MidpointRounding.ToEven)
    '                    If Math.Abs(TotPaycomm) > 0.1 Then
    '                        objHead.Total_PaymentCommission += TotPaycomm
    '                    End If
    '                    totAmountwithPaymentCommssion += objDetail.Net_AMOUNT
    '                    objHead.Handling_Charges_Amount += objDetail.Handling_Charges_Amount
    '                    objHead.SRN_Net_Amount += objDetail.SRN_Net_Amount
    '                    objHead.SRN_RO_Amount += objDetail.SRN_RO_Amount
    '                Next
    '                objHead.Handling_Charges_Amount = Math.Round(objHead.Handling_Charges_Amount, 2, MidpointRounding.ToEven)
    '                objHead.Total_Head_Load_Amount = Math.Round(objHead.Total_Head_Load_Amount, 2, MidpointRounding.ToEven)
    '                If IsRoundOffPaiseAmount Then
    '                    objHead.Handling_Charges_RO_Amount = (objHead.Handling_Charges_Amount Mod 1)
    '                    objHead.Handling_Charges_Amount = objHead.Handling_Charges_Amount - objHead.Handling_Charges_RO_Amount

    '                    objHead.Total_Head_Load_RO_Amount = (objHead.Total_Head_Load_Amount Mod 1)
    '                    objHead.Total_Head_Load_Amount = objHead.Total_Head_Load_Amount - objHead.Total_Head_Load_RO_Amount
    '                End If


    '                objHead.Total_Own_Asset_Amount = TotOwnAsset
    '                objHead.Total_Deduction_Amount = TotDeduction_Amount
    '                objHead.VENDOR_INVOICE_NO = ""
    '                objHead.VENDOR_INVOICE_DATE = obj_SRN.DOC_DATE
    '                objHead.Amount = clsCommon.myCdbl(totAmount)
    '                objHead.Basic_Amount = Math.Round(clsCommon.myCdbl(totAmount) - clsCommon.myCdbl(totCommssion), 2)
    '                objHead.Commission = clsCommon.myCdbl(totCommssion)
    '                objHead.Total_Amount_Acc = clsCommon.myCdbl(totAmountwithPaymentCommssion) - objHead.Handling_Charges_RO_Amount

    '                objHead.Program_Code = Formcode
    '                If clsMilkPurchaseInvoiceProvisionHead.SaveData(objHead, objList, trans) Then
    '                    If Not settDoNotIncludeIncentiveInMilkPurchaseInvoice Then
    '                        Dim incentive As ArrayList = clsMilkPurchaseInvoiceMCC.LoadDataQuery_For_Incentive(True, objHead.DOC_CODE, objHead.VSP_CODE, objHead.MCC_CODE, frm_date, Today.Date, False, trans, (End_date.Day - frm_date.Day) + 1)
    '                        Dim totincentiveEMP As Double = 0
    '                        Dim totincentive As Double = 0
    '                        totAmount = 0
    '                        totBasicAmount = 0
    '                        totAmountwithPaymentCommssion = 0
    '                        Dim is_processed As Integer = 0
    '                        Dim is_Emp_On_Amount_Only As String = clsDBFuncationality.getSingleValue("select EmpOnAMountOnly from tspl_Mcc_Master where Mcc_Code='" & objDetail.MCC_CODE & "'", trans)
    '                        If incentive.Count > 0 Then
    '                            If incentive(1) > 0 Then
    '                                For Each obj1 As clsMilkSRNMCCDetail In frm.ArrReturn
    '                                    If is_processed = 0 Then
    '                                        totincentiveEMP = Math.Round(clsCommon.myCdbl(incentive(1)) * clsCommon.myCdbl(obj1.Payment_Commission) / 100, 2)
    '                                        totAmount += objDetail.AMOUNT + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
    '                                        totBasicAmount += objDetail.AMOUNT + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
    '                                        objDetail.Net_AMOUNT += +IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
    '                                        totAmountwithPaymentCommssion += objDetail.Net_AMOUNT '+ totincentiveEMP '+ incentive(1)
    '                                        sQuery = "Update TSPL_MILK_PURCHASE_INVOICE_PROVISION_DETAIL set Total_Amount='" & clsCommon.myCdbl(objDetail.AMOUNT) & "',Total_Amount_Acc='" & clsCommon.myCdbl(objDetail.Net_AMOUNT) & "',Net_Amount='" & clsCommon.myCdbl(objDetail.Net_AMOUNT) & "',incentive='" & incentive(1) & "' , incentiveEMP='" & totincentiveEMP & "' where srn_code='" & objDetail.SRN_CODE & "'"
    '                                        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
    '                                        is_processed = 1
    '                                    End If
    '                                    'Exit For
    '                                Next
    '                                is_processed = 0
    '                                totAmount = objHead.Amount + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
    '                                totAmountwithPaymentCommssion = objHead.Total_Amount_Acc + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
    '                                sQuery = "select * from TSPL_MILK_PURCHASE_INVOICE_PROVISION_HEAD where doc_code='" & clsCommon.myCstr(objHead.DOC_CODE) & "'"
    '                                dt = clsDBFuncationality.GetDataTable(sQuery, trans)
    '                                sQuery = "Update TSPL_MILK_PURCHASE_INVOICE_PROVISION_HEAD set Total_Amount='" & clsCommon.myCdbl(totAmount) & "',Total_Amount_Acc='" & clsCommon.myCdbl(totAmountwithPaymentCommssion) & "' ,incentive_Head='" & incentive(1) & "' , incentiveEMP_Head='" & totincentiveEMP & "' where doc_code='" & clsCommon.myCstr(objHead.DOC_CODE) & "'"
    '                                clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub btnJE_Click(sender As Object, e As EventArgs) Handles btnJE.Click
        MyBase.ShowJE(MyBase.Form_ID, fndDocNo.Value)
    End Sub

    Private Sub fndArea__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndArea._MYValidating
        '        Try
        '            Dim sQuery As String = "select TSPL_VLC_MASTER_HEAD.VSP_Code [Code],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name from TSPL_VLC_MASTER_HEAD 
        'left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code "
        '            fndArea.Value = clsCommon.ShowSelectForm("Location@Area@Master", sQuery, "Code", "", fndArea.Value, "Code", isButtonClicked)
        '        Catch ex As Exception
        '            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        '        End Try
        Try
            Dim sQuery As String = " Select TSPL_LOCATION_MASTER.Location_Code as Code ,  TSPL_LOCATION_MASTER.Location_Desc, Type from TSPL_LOCATION_MASTER
     "
            fndArea.Value = clsCommon.ShowSelectForm("Location@Plant@Master", sQuery, "Code", "TSPL_LOCATION_MASTER.Type <> 'PLANT' OR TSPL_LOCATION_MASTER.Location_Category <> 'Mcc'", fndArea.Value, "Code", isButtonClicked)
            'If fndLocation.Value <> "" Then
            '    lblLocation.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndArea.Value & "'")
            'Else
            '    lblLocation.Text = ""
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub

End Class
