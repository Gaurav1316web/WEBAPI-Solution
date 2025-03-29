Imports common
Imports System.ComponentModel
Imports System.IO
Public Class rptBankSavingAdvice
    Inherits FrmMainTranScreen
    Private Sub rptBankSavingAdvice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            fromDate.Value = clsCommon.GETSERVERDATE()
            ToDate.Value = clsCommon.GETSERVERDATE()
            txtFiscalYear.Value = objCommonVar.CurrFiscalYear

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        ' rbtBankSummary.Visible = True
        btnSummary.Visible = False
    End Sub

    Private Sub txtFiscalYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFiscalYear._MYValidating
        Try
            Dim qry As String = "select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER"
            txtFiscalYear.Value = clsCommon.ShowSelectForm("LRFY", qry, "Fiscal_Code", "", txtFiscalYear.Value, "", isButtonClicked)
            'fromDate.Value = clsCommon.GETSERVERDATE()
            'ToDate.Value = clsCommon.GETSERVERDATE()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub fromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles fromDate.Validating

        '  SetToDate()
    End Sub
    Sub SetToDate()
        'If MultipleFinderFillAuto Then
        Dim PaymentCycleType As String = ""
        Dim PaymentCycleValue As Integer = 0

        'If clsCommon.myLen(fndLoc.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
        '    clsCommon.MyMessageBoxShow("Please select the Location first")
        '    Exit Sub
        'End If
        'If MultipleFinderFillAuto = True Then
        '    If mfndMcc.arrValueMember Is Nothing OrElse mfndMcc.arrValueMember.Count <= 0 Then
        '        clsCommon.MyMessageBoxShow("Please select the Location first")
        '        Exit Sub
        '    End If
        'End If
        'Dim strMCCcode = ""
        'If clsCommon.myLen(txtMCC.Value) Then
        '    strMCCcode = " location_Code in ( '" + clsCommon.myCstr(txtMCC.Value) + "')  and "
        'End If


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code  in (select Location_Code  from TSPL_LOCATION_MASTER where  Location_Category='MCC' and Rejected_Type='N') ")
        'If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
        '    clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
        '    Exit Sub
        'End If
        PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
        PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
        Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
        If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
            If fromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                fromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                ToDate.Value = fromDate.Value
                Exit Sub
            End If
            ToDate.Value = fromDate.Value.AddDays(PaymentCycleValue - 1)

            If fromDate.Value.Month <> ToDate.Value.Month Then
                ToDate.Value = New Date(fromDate.Value.Year, fromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
            End If
            Dim dtNxtPay As DateTime = ToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
            If fromDate.Value.Month <> dtNxtPay.Month Then
                ToDate.Value = New Date(fromDate.Value.Year, fromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
            End If
        ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsCommon.GetPrintDate(fromDate.Value, "dd")) <> 1 Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                fromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                ToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                Exit Sub
            End If
            ToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, fromDate.Value)
        ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsCommon.GetPrintDate(fromDate.Value, "dd")) <> 1 Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                fromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                ToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                Exit Sub
            End If
            ToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, fromDate.Value)
        ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
            Dim today As Date = fromDate.Value
            Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
            fromDate.Value = today.AddDays(-dayDiff)
            ToDate.Value = fromDate.Value.AddDays(6)
        End If
        ' End If
        'If clsCommon.myLen(txtMCC.Text) > 0 Then
        '    txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtMCC.Text, dtpToDate.Value)
        '    txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtMCC.Text, dtpToDate.Value)
        'Else
        '    txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(fndLoc.Value, dtpToDate.Value)
        '    txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(fndLoc.Value, dtpToDate.Value)
        'End If
        'End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Print(True)
    End Sub
    Sub Print(ByVal isPrint As Boolean)
        Try
            If clsCommon.GetDateWithStartTime(fromDate.Value) > clsCommon.GetDateWithStartTime(ToDate.Value) Then
                fromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()
            PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
            TemplateGridview = Gv1
            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Fiscal Year First.", Me.Text)
                txtFiscalYear.Focus()
                Exit Sub
            End If
            Dim Qry As String = " ( select ROW_NUMBER() OVER (ORDER BY [DCS Code]) AS SerialNumber, (x.[DCS Code])[DCSCode],max([DCS Name])[DCSName],(x.Code)Code,max(MCC_CODE)MCC_CODE
,max(Mcc_Name)Mcc_Name,max(comp_code1)comp_code1,max(Area_location_code)Area_location_code,
max(x.[DCS Type])[DCSType],max(x.[Is Own BMC])[IsOwnBMC],([Apply On])[ApplyOn],([Apply Type])[ApplyType],
                                 (x.[Formula])Formula  ,convert(decimal(18,2),FLOOR(sum(x.[Addition/Deduction Amount]) )) FloR,convert(decimal(18,2),sum(x.[Addition/Deduction Amount]))[Addition/DeductionAmount]  ,max(x.[Addition/Deduction Description])[Addition/DeductionDescription] 
						         ,AccNo2,BankBranch2,BankName2,AccountType2,BankCode2
,max(Add3)Add3,max(Add2)Add2,max(Add1)Add1,max(Comp_Name)Comp_Name,max(Comp_Code)Comp_Code   "
            If chkIfscno.Checked Then
                Qry += ",IFSCCode2"
            End If
            Qry += "          from (select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Code],TSPL_VLC_MASTER_HEAD.VLC_Name  as [DCS Name]
                                 ,TSPL_VLC_MASTER_HEAD.VSP_Code as [Code],CASE WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=0 THEN 'All'
                                    WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=1 THEN 'DCS'
                                    WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=2 THEN 'PDCS'
                                    WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=3 THEN 'BMC'
                                    WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=4 THEN 'Cluster'
                                    End As [DCS Type],Case When TSPL_VLC_MASTER_HEAD.isOwnBMC=1 Then 'Yes' else 'No' end as [Is Own BMC]
									  ,case when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=1 then 'Amount'
                                    when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=0 then 'Quantity' else '' end as [Apply On]
                                    ,case when TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=1 then 'Percentage'
                                    when TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=0 then 'Rate' else '' end as [Apply Type],Applicable_Value as [Formula]
									   ,CASE when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=1 And TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=1 then
                                    cast(TSPL_MILK_SRN_DETAIL.NET_AMOUNT As Decimal(18, 2)) 
                                     when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=0 And TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=0 then
                                    cast(TSPL_MILK_SRN_DETAIL.Qty As Decimal(18, 2)) 
                                    Else 0 end AS [Base Amount/Quantity]
									,TSPL_MILK_SRN_DETAIL.AMOUNT, TSPL_MILK_SRN_DETAIL.Qty ,TSPL_MILK_SRN_DETAIL.NET_AMOUNT"
            If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.RoundOffBankAdvice, clsFixedParameterCode.RoundOffBankAdvice, Nothing)) = "1" Then
                Qry += " ,(Round(TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED.Amt,0)) as [Addition/Deduction Amount]"
            Else
                Qry += ",TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED.Amt As [Addition/Deduction Amount] "
            End If
            Qry += " ,TSPL_DCS_ADDITION_DEDUCTION.Description As [Addition/Deduction Description]
									,TSPL_VLC_MASTER_HEAD.AccNo2"
            If chkIfscno.Checked Then
                Qry += ",TSPL_VLC_MASTER_HEAD.IFSCCode2 "
            End If
            Qry += ",TSPL_VLC_MASTER_HEAD.BankBranch2
									,TSPL_VLC_MASTER_HEAD.BankName2
									,TSPL_VLC_MASTER_HEAD.AccountType2
									,TSPL_VLC_MASTER_HEAD.BankCode2
,tspl_company_master.Comp_Code
									,tspl_company_master.Comp_Name
									,tspl_company_master.Add1
									,tspl_company_master.Add2
									,tspl_company_master.Add3
									,TSPL_MILK_SRN_HEAD.MCC_CODE
	,TSPL_MCC_MASTER.Mcc_Name
	,TSPL_MCC_MASTER.Area_location_code
									,tspl_company_master.comp_code1

                                     from TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED
                                    LEFT OUTER JOIN TSPL_MILK_PURCHASE_INVOICE_HEAD ON TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED.invoiceNo
                                    left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED.SRN_CODE
                                    left outer join TSPL_MILK_SRN_DETAIL ON TSPL_MILK_SRN_DETAIL.DOC_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE
                                    LEFT OUTER JOIN TSPL_DCS_ADDITION_DEDUCTION ON TSPL_DCS_ADDITION_DEDUCTION.CODE=ISNULL(TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED.Against_DCS_ADDITION_DEDUCTION,'')
                                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_Code
									left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VLC_MASTER_HEAD.VSP_Code
                                    left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
left outer join tspl_company_master on 2 = 2
                                    WHERE   ISNULL(TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED.Against_DCS_ADDITION_DEDUCTION,'')<>'' and
                                    CONVERT(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and CONVERT(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)<= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' 
                                    and TSPL_DCS_ADDITION_DEDUCTION.Nature_Type=0  and TSPL_DCS_ADDITION_DEDUCTION.MarginDCS=1 "

            If clsCommon.myLen(txtMCC.Value) > 0 Then
                Qry += " AND  TSPL_MILK_SRN_HEAD.MCC_CODE = '" + txtMCC.Value + "' "
            End If
            Qry +=	")x  group by x.[DCS Code],x.[Code],x.[Apply On],x.[Apply Type],x.Formula,x.AccNo2,"
            If chkIfscno.Checked Then
                Qry += " x.IFSCCode2,"
            End If
            Qry += " x.BankBranch2,x.BankName2,x.AccountType2,x.BankCode2 ,x.Add3,x.Add2,x.Add1,x.Comp_Name,x.Comp_Code) order by cast([DCS Code] as int)"


            Dim dt As DataTable = Nothing
            dt = clsDBFuncationality.GetDataTable(Qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data Found to Display")
            End If
            Gv1.DataSource = dt
            RadPageView1.SelectedPage = RadPageViewPage2
            SetGridFormationOFGV1Collection()
            'If rbtnBankAdvice.IsChecked Then
            'If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            '    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            '    Exit Sub
            'ElseIf DT.Rows.Count > 0 Then
            '    Dim frmCRV As New frmCrystalReportViewer()
            '    If chkIfscno.IsChecked = True Then
            '        frmCRV.funreport(False, CrystalReportFolder.SalesReport, dt, "rptBankSavingAdviceIFSC", "Bank Saving")
            '    Else
            '        frmCRV.funreport(False, CrystalReportFolder.SalesReport, dt, "rptBankSavingAdvice", "Bank Saving")
            '    End If
            '    frmCRV = Nothing
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormationOFGV1Collection()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            'gv1.Columns("Document_No").HeaderText = "Document No."
            Gv1.Columns("DCSCode").IsVisible = True
            Gv1.Columns("DCSCode").HeaderText = "DCS Code"
            Gv1.Columns("DCSName").IsVisible = True
            Gv1.Columns("DCSName").HeaderText = "DCS Name"
            Gv1.Columns("DCSType").IsVisible = False
            Gv1.Columns("DCSType").HeaderText = "DCS Type"
            Gv1.Columns("Code").IsVisible = True
            Gv1.Columns("Code").HeaderText = "Code"
            Gv1.Columns("IsOwnBMC").IsVisible = False
            Gv1.Columns("IsOwnBMC").HeaderText = "Is Own BMC"
            'gv1.Columns("ROUTE_NAME").HeaderText = "Route Description"
            'gv1.Columns("Tanker_No").HeaderText = "Tanker No."
            Gv1.Columns("ApplyOn").IsVisible = False
            Gv1.Columns("ApplyOn").HeaderText = "Apply On"
            Gv1.Columns("ApplyType").IsVisible = False
            Gv1.Columns("ApplyType").HeaderText = "Apply Type"
            Gv1.Columns("Formula").IsVisible = False
            Gv1.Columns("Formula").HeaderText = "Formula"
            Gv1.Columns("FloR").IsVisible = False
            Gv1.Columns("FloR").HeaderText = "FloR"
            Gv1.Columns("Addition/DeductionAmount").IsVisible = True
            Gv1.Columns("Addition/DeductionAmount").HeaderText = "Addition/Deduction Amount"
            Gv1.Columns("Addition/DeductionDescription").IsVisible = True
            Gv1.Columns("Addition/DeductionDescription").HeaderText = "Addition/Deduction Description"
            If chkIfscno.Checked Then
                Gv1.Columns("IFSCCode2").IsVisible = True
                Gv1.Columns("IFSCCode2").HeaderText = "IFSC Code"
            End If
            Gv1.Columns("BankBranch2").IsVisible = True
            Gv1.Columns("BankBranch2").HeaderText = "Bank Branch"
            Gv1.Columns("BankName2").IsVisible = True
            Gv1.Columns("BankName2").HeaderText = "Bank Name"
            Gv1.Columns("AccNo2").IsVisible = True
            Gv1.Columns("AccNo2").HeaderText = "Account No"
            Gv1.Columns("BankCode2").IsVisible = True
            Gv1.Columns("BankCode2").HeaderText = "Bank Code"
            Gv1.Columns("AccountType2").IsVisible = True
            Gv1.Columns("AccountType2").HeaderText = "Account Type"

            Gv1.Columns("Area_location_code").IsVisible = False
            Gv1.Columns("Area_location_code").HeaderText = "Area Location Code"
            Gv1.Columns("Mcc_Name").IsVisible = True
            Gv1.Columns("Mcc_Name").HeaderText = "Mcc Name"

            Gv1.Columns("Comp_Code").IsVisible = False
            Gv1.Columns("Comp_Code").HeaderText = "Comp Code"
            Gv1.Columns("Comp_Name").IsVisible = False
            Gv1.Columns("Comp_Name").HeaderText = "Comp_Name"
            Gv1.Columns("Add1").IsVisible = False
            Gv1.Columns("Add1").HeaderText = "Add1"
            Gv1.Columns("Add2").IsVisible = False
            Gv1.Columns("Add2").HeaderText = "Add2"
            Gv1.Columns("Add3").IsVisible = False
            Gv1.Columns("Add3").HeaderText = "Add3"
            Gv1.Columns("comp_code1").IsVisible = False
            Gv1.Columns("comp_code1").HeaderText = "comp_code1"
        Next
        Dim summaryRowItemB As New GridViewSummaryRowItem()
        'Dim MilkTypeB As New GridViewSummaryItem("Payable_Amount", "{0:n0}", GridAggregateFunction.Sum)
        Dim AdditionDeductionAmount As New GridViewSummaryItem("Addition/DeductionAmount", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(AdditionDeductionAmount)
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Gv1.AutoSizeRows = True
        Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs)
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Gv1.MasterTemplate.FilterDescriptors.Clear()
                Dim obj As New clsGridLayout()
                obj.ReportID = PageSetupReport_ID
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                Gv1.SaveLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                obj.GridColumns = Gv1.ColumnCount
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
                End If
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub EnableDisaableControls(ByVal flag As Boolean)
        txtMCC.Enabled = flag
        txtFiscalYear.Enabled = flag

        chkIfscno.Enabled = flag
    End Sub
    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs)
        Try
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
            common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        Try
            'Dim qry As String = "select MCC_Code as Code,MCC_NAME as Name,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code"

            Dim qry As String = "select TSPL_MCC_MASTER.MCC_Code as Code,MCC_NAME as Name,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER 
            Left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code "

            Dim strWhrcls As String = "location_code ='" + clsCommon.myCstr(txtMCC.Value) + "'"
            txtMCC.Value = clsCommon.ShowSelectForm("vendorBadvice", qry, "Code", "", txtMCC.Value, "Code", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtMCC.Value = ""
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = clsCommon.GETSERVERDATE()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        chkIfscno.Checked = False
        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim FromDates As String = clsCommon.myCstr(fromDate.Text)
        Dim TODates As String = clsCommon.myCstr(ToDate.Text)
        Try
            Dim Qry As String = " ( select ROW_NUMBER() OVER (ORDER BY [BankName2]) AS SerialNumber, (x.[DCS Code])[DCSCode],max([DCS Name])[DCSName],(x.Code)Code,max(MCC_CODE)MCC_CODE,max(Area_location_code)Area_location_code,max(Mcc_Name)Mcc_Name,
                                 max(x.[DCS Type])[DCSType],max(x.[Is Own BMC])[IsOwnBMC],([Apply On])[ApplyOn],([Apply Type])[ApplyType],
                                 (x.[Formula])Formula  ,convert(decimal(18,2),FLOOR(sum(x.[Addition/Deduction Amount]) )) FloR,convert(decimal(18,2),sum(x.[Addition/Deduction Amount]))[Addition/DeductionAmount]  ,max(x.[Addition/Deduction Description])[Addition/DeductionDescription] 
						         ,AccNo2,BankBranch2,BankName2,AccountType2,BankCode2
                                 ,max(Add3)Add3,max(Add2)Add2,max(Add1)Add1,max(Comp_Name)Comp_Name,max(comp_code1)comp_code1 ,max(comp_code)comp_code ,MAX(GSTReg_No)GSTReg_No,max(FromDate)FromDate,max(TODate)TODate "
            If chkIfscno.Checked Then
                Qry += ",IFSCCode2"
            End If
            Qry += "              from (select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Code],TSPL_VLC_MASTER_HEAD.VLC_Name  as [DCS Name]
                                 ,TSPL_VLC_MASTER_HEAD.VSP_Code as [Code],CASE WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=0 THEN 'All'
                                    WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=1 THEN 'DCS'
                                    WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=2 THEN 'PDCS'
                                    WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=3 THEN 'BMC'
                                    WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=4 THEN 'Cluster'
                                    End As [DCS Type],Case When TSPL_VLC_MASTER_HEAD.isOwnBMC=1 Then 'Yes' else 'No' end as [Is Own BMC]
									  ,case when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=1 then 'Amount'
                                    when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=0 then 'Quantity' else '' end as [Apply On]
                                    ,case when TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=1 then 'Percentage'
                                    when TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=0 then 'Rate' else '' end as [Apply Type],Applicable_Value as [Formula]
									   ,CASE when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=1 And TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=1 then
                                    cast(TSPL_MILK_SRN_DETAIL.NET_AMOUNT As Decimal(18, 2)) 
                                     when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=0 And TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=0 then
                                    cast(TSPL_MILK_SRN_DETAIL.Qty As Decimal(18, 2)) 
                                    Else 0 end AS [Base Amount/Quantity]
									,TSPL_MILK_SRN_DETAIL.AMOUNT, TSPL_MILK_SRN_DETAIL.Qty ,TSPL_MILK_SRN_DETAIL.NET_AMOUNT "
            If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.RoundOffBankAdvice, clsFixedParameterCode.RoundOffBankAdvice, Nothing)) = "1" Then
                Qry += " ,(Round(TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED.Amt,0)) as [Addition/Deduction Amount]"
            Else
                Qry += ",TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED.Amt As [Addition/Deduction Amount]"
            End If
            Qry += "  	,TSPL_DCS_ADDITION_DEDUCTION.Description As [Addition/Deduction Description]
									,TSPL_VLC_MASTER_HEAD.AccNo2"
            If chkIfscno.Checked Then
                Qry += ",TSPL_VLC_MASTER_HEAD.IFSCCode2 "
            End If
            Qry += ",TSPL_VLC_MASTER_HEAD.BankBranch2
									,TSPL_VLC_MASTER_HEAD.BankName2
									,TSPL_VLC_MASTER_HEAD.AccountType2
									,TSPL_VLC_MASTER_HEAD.BankCode2
,tspl_company_master.Comp_Code
									,tspl_company_master.Comp_Name
									,tspl_company_master.Add1
									,tspl_company_master.Add2
									,tspl_company_master.Add3
									,TSPL_MILK_SRN_HEAD.MCC_CODE
							        ,TSPL_MCC_MASTER.Mcc_Name
									,TSPL_MCC_MASTER.Area_location_code
,tspl_company_master.comp_code1
,									tspl_company_master.GSTReg_No
,'" + FromDates + "' AS FromDate,'" + TODates + " ' as ToDate

                                     from TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED
                                    LEFT OUTER JOIN TSPL_MILK_PURCHASE_INVOICE_HEAD ON TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED.invoiceNo
                                    left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED.SRN_CODE
                                    left outer join TSPL_MILK_SRN_DETAIL ON TSPL_MILK_SRN_DETAIL.DOC_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE
                                    LEFT OUTER JOIN TSPL_DCS_ADDITION_DEDUCTION ON TSPL_DCS_ADDITION_DEDUCTION.CODE=ISNULL(TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED.Against_DCS_ADDITION_DEDUCTION,'')
                                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_Code
									left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VLC_MASTER_HEAD.VSP_Code
                                    left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
left outer join tspl_company_master on 2 = 2
                                    WHERE   ISNULL(TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED.Against_DCS_ADDITION_DEDUCTION,'')<>'' and
                                    CONVERT(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>= '" + clsCommon.GetPrintDate(FromDates, "dd/MMM/yyyy") + "' and CONVERT(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)<= '" + clsCommon.GetPrintDate(TODates, "dd/MMM/yyyy") + "' 
                                    and TSPL_DCS_ADDITION_DEDUCTION.Nature_Type=0  and TSPL_DCS_ADDITION_DEDUCTION.MarginDCS=1 "
            If clsCommon.myLen(txtMCC.Value) > 0 Then
                Qry += " AND  TSPL_MILK_SRN_HEAD.MCC_CODE = '" + txtMCC.Value + "' "
            End If
            Qry += "	)x  group by x.[DCS Code],x.[Code],x.[Apply On],x.[Apply Type],x.Formula,x.AccNo2,"
            If chkIfscno.Checked Then
                Qry += " x.IFSCCode2,"
            End If
            Qry += " x.BankBranch2,x.BankName2,x.AccountType2,x.BankCode2 ,x.Add3,x.Add2,x.Add1,x.Comp_Name,x.Comp_Code) order by  cast([DCS Code] as int)"
            Dim dt As DataTable = Nothing
            dt = clsDBFuncationality.GetDataTable(Qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data Found to Display")
            End If
            'Gv1.DataSource = dt
            'RadPageView1.SelectedPage = RadPageViewPage2
            ' SetGridFormationOFGV1Collection()
            'If rbtnBankAdvice.IsChecked Then
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            ElseIf dt.Rows.Count > 0 Then
                ''Note IF You do any changes than change in function clsBankAdvise.CreateEmailContent(ByVal strDateRange As String, trans As SqlTransaction)
                Dim frmCRV As New frmCrystalReportViewer()
                If chkIfscno.IsChecked = True Then
                    frmCRV.funreport(False, CrystalReportFolder.SalesReport, dt, "rptBankSavingAdviceIFSC", "Bank Saving")

                Else
                    frmCRV.funreport(False, CrystalReportFolder.SalesReport, dt, "rptBankSavingAdvice", "Bank Saving")

                End If
                'If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                'frmCRV.funreport(False, CrystalReportFolder.MilkProcurement, dt, "crptBankAdviceSavingNEW", "Bank Saving")
                'frmCRV.funreport(False, CrystalReportFolder.MilkProcurement, dt, "rptTankerGainLossReport", "Tanker Gain Loss Report")

                'End If
                frmCRV = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Print Date (" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm:ss tt") + ")")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBankSavingAdvice & "'"))
                arrHeader.Add("Date Range : " & clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "  To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
                If clsCommon.myLen(txtMCC.Value) > 0 Then
                    arrHeader.Add("MCC : " & txtMCC.Value)
                End If
                'If chkIfscno.Checked Then
                '    arrHeader.Add("IFSC Code : " & clsCommon.myCstr(chkIfscno.Text))

                'End If
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            Else
                Throw New Exception("No data found to export.")

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles btnSummary.Click
        Try
            Dim FromDate1 As String = clsCommon.myCstr(fromDate.Text)
            Dim TODate1 As String = clsCommon.myCstr(ToDate.Text)

            Dim Qry As String = "  ( select ROW_NUMBER() OVER (ORDER BY BankCode2) AS SerialNumber, max(x.[DCS Code])[DCSCode],max([DCS Name])[DCSName],max(x.Code)Code,max(MCC_CODE)MCC_CODE--,max 
							   ,convert(decimal(18,2),
							   FLOOR(sum(x.[Addition/Deduction Amount]) )) FloR,convert(decimal(18,2),sum(x.[Addition/Deduction Amount]))[Addition/DeductionAmount]  
								 
								 
								 ,max(x.[Addition/Deduction Description])[Addition/DeductionDescription] 
						         ,max(AccNo2)AccNo2,max(BankBranch2)BankBranch2,BankName2,max(AccountType2)AccountType2,BankCode2
								 		 ,max(Add3)Add3,max(Add2)Add2,max(Add1)Add1,max(Comp_Name)Comp_Name,max(comp_code1)comp_code1 ,max(comp_code)comp_code
								 ,max(FromDate)FromDate,max(ToDate)ToDate
								 from 
								 
								 
								 (select 
'" + FromDate1 + "' AS FromDate, '" + TODate1 + " ' as ToDate,
TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Code],TSPL_VLC_MASTER_HEAD.VLC_Name  as [DCS Name]
                                 ,TSPL_VLC_MASTER_HEAD.VSP_Code as [Code],CASE WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=0 THEN 'All'
                                    WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=1 THEN 'DCS'
                                    WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=2 THEN 'PDCS'
                                    WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=3 THEN 'BMC'
                                    WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=4 THEN 'Cluster'
                                    End As [DCS Type],Case When TSPL_VLC_MASTER_HEAD.isOwnBMC=1 Then 'Yes' else 'No' end as [Is Own BMC]
									  ,case when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=1 then 'Amount'
                                    when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=0 then 'Quantity' else '' end as [Apply On]
                                    ,case when TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=1 then 'Percentage'
                                    when TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=0 then 'Rate' else '' end as [Apply Type],Applicable_Value as [Formula]
									   ,CASE when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=1 And TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=1 then
                                    cast(TSPL_MILK_SRN_DETAIL.NET_AMOUNT As Decimal(18, 2)) 
                                     when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=0 And TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=0 then
                                    cast(TSPL_MILK_SRN_DETAIL.Qty As Decimal(18, 2)) 
                                    Else 0 end AS [Base Amount/Quantity]
									,TSPL_MILK_SRN_DETAIL.AMOUNT, TSPL_MILK_SRN_DETAIL.Qty ,TSPL_MILK_SRN_DETAIL.NET_AMOUNT"
            If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.RoundOffBankAdvice, clsFixedParameterCode.RoundOffBankAdvice, Nothing)) = "1" Then
                Qry += " ,(Round(TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED.Amt,0)) as [Addition/Deduction Amount]"
            Else

                Qry += "		,TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED.Amt As [Addition/Deduction Amount]"
            End If
            Qry += "		,TSPL_DCS_ADDITION_DEDUCTION.Description As [Addition/Deduction Description]
									,TSPL_VLC_MASTER_HEAD.AccNo2,TSPL_VLC_MASTER_HEAD.BankBranch2
									,TSPL_VLC_MASTER_HEAD.BankName2
									,TSPL_VLC_MASTER_HEAD.AccountType2
									,TSPL_VLC_MASTER_HEAD.BankCode2
,tspl_company_master.Comp_Code
									,tspl_company_master.Comp_Name
									,tspl_company_master.Add1
									,tspl_company_master.Add2
									,tspl_company_master.Add3
									,TSPL_MILK_SRN_HEAD.MCC_CODE
							        ,TSPL_MCC_MASTER.Mcc_Name
									,TSPL_MCC_MASTER.Area_location_code
,tspl_company_master.comp_code1

                                     from TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED
                                    LEFT OUTER JOIN TSPL_MILK_PURCHASE_INVOICE_HEAD ON TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED.invoiceNo
                                    left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED.SRN_CODE
                                    left outer join TSPL_MILK_SRN_DETAIL ON TSPL_MILK_SRN_DETAIL.DOC_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE
                                    LEFT OUTER JOIN TSPL_DCS_ADDITION_DEDUCTION ON TSPL_DCS_ADDITION_DEDUCTION.CODE=ISNULL(TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED.Against_DCS_ADDITION_DEDUCTION,'')
                                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_Code
									left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VLC_MASTER_HEAD.VSP_Code
                                    left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
left outer join tspl_company_master on 2 = 2
                                    WHERE   ISNULL(TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED.Against_DCS_ADDITION_DEDUCTION,'')<>'' and
                                    CONVERT(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>= '" + clsCommon.GetPrintDate(FromDate1, "dd/MMM/yyyy") + "' and CONVERT(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)<= '" + clsCommon.GetPrintDate(TODate1, "dd/MMM/yyyy") + "' 
                                    and TSPL_DCS_ADDITION_DEDUCTION.Nature_Type=0  and TSPL_DCS_ADDITION_DEDUCTION.MarginDCS=1 	)x 
									
									group by  BankName2,BankCode2)"
            Dim dt As DataTable = Nothing
            dt = clsDBFuncationality.GetDataTable(Qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data Found to Display")
            End If
            'Gv1.DataSource = dt
            'RadPageView1.SelectedPage = RadPageViewPage2
            ' SetGridFormationOFGV1Collection()
            'If rbtnBankAdvice.IsChecked Then
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            ElseIf dt.Rows.Count > 0 Then
                ''Note IF You do any changes than change in function clsBankAdvise.CreateEmailContent(ByVal strDateRange As String, trans As SqlTransaction)
                Dim frmCRV As New frmCrystalReportViewer()

                frmCRV.funreport(False, CrystalReportFolder.SalesReport, dt, "crptBankSavingDraftSummary", "Bank Saving")
                'End If
                frmCRV = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub rbtBankSummary_Click_1(sender As Object, e As EventArgs) Handles rbtBankSummary.Click
        If rbtBankSummary.Checked Then
            btnSummary.Visible = True
        End If
    End Sub
End Class