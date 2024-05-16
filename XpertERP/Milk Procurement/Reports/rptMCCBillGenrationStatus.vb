Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class rptMCCBillGenrationStatus
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
    Dim arrLoc As String = Nothing
    Dim TankerFromMaster As Integer
    Dim isShowTreeView As Boolean = True

    Private Sub FrmMCCMilkRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LOCATIONRIGTHS()
        SetUserMgmtNew()
        'ChkDetailWise.Checked = True

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        setFromAndToDate()
        'txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        'txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")

        'isShowTreeView = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsShowTreeView, clsFixedParameterCode.IsShowTreeView, Nothing)) = 1
        chkShowVLCUploaderData.Checked = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVLCUploaderData, clsFixedParameterCode.ShowVLCUploaderData, Nothing)) = 1

        SplitContainer2.Panel1Collapsed = True
            SplitContainer2.Panel2Collapsed = False


        LoadShiftFrom()
        LoadShiftTo()
        LoadSRNAmountType()
        Reset()
        fndPlant.Visible = False
        txtMccForStatus.Visible = False
        MyLabel2.Visible = False
        fndMCC.Visible = False
    End Sub
    Sub setFromAndToDate()

        txtToDate.Enabled = False
        Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
        txtFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1) 'New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1)
        SetToDate()
    End Sub
    Sub SetToDate()

        Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0

        'If clsCommon.myLen(txtMCC.Value) <= 0 Then
        '        clsCommon.MyMessageBoxShow("Please select the Location first")
        '        Exit Sub
        '    End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TOP 1 TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   order by TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  desc ") ' where TSPL_MCC_MASTER.MCC_Code ='" & txtMCC.Value & "'")
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found", Me.Text)
            Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                txtFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                'txtToDate.Value = txtFromDate.Value
                txtToDate.Value = txtFromDate.Value.AddDays(PaymentCycleValue - 1)
                Exit Sub
                End If
                txtToDate.Value = txtFromDate.Value.AddDays(PaymentCycleValue - 1)

                If txtFromDate.Value.Month <> txtToDate.Value.Month Then
                    txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Dim dtNxtPay As DateTime = txtToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If txtFromDate.Value.Month <> dtNxtPay.Month Then
                    txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type", Me.Text)
                txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                txtToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, txtFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type", Me.Text)
                txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                txtToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, txtFromDate.Value)
            End If

    End Sub

    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.rptMCCBillGenrationStatus)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")

            End If
        End If
        radbtnBulkExp.Visible = MyBase.isExport
    End Sub

    'Sub LoadMCCRouteVLCTree()
    '    Dim qry As String = Nothing
    '    Dim dt As DataTable = Nothing
    '    If clsCommon.myLen(arrLoc) > 0 Then
    '        qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Name,TSPL_VLC_MASTER_HEAD.Route_Code as ParentCode,3 as Lvl from TSPL_VLC_MASTER_HEAD where len(isnull(TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 union all   select TSPL_MCC_ROUTE_MASTER.Route_Code as Code,TSPL_MCC_ROUTE_MASTER.Route_Name as Name,TSPL_MCC_ROUTE_MASTER.MCC_Code as ParentCode,2 as Lvl from TSPL_MCC_ROUTE_MASTER where len(isnull(TSPL_MCC_ROUTE_MASTER.MCC_Code,''))>0  union all   select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name,null as ParentCode,1 as Lvl from TSPL_MCC_MASTER   where TSPL_MCC_MASTER.MCC_Code in (" + arrLoc + ") "
    '        dt = clsDBFuncationality.GetDataTable(qry)
    '    End If

    '    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '        btnGo.Enabled = False
    '    Else
    '        cbtMCCRouteVLCC.DataSource = dt
    '        cbtMCCRouteVLCC.ValueMember = "Code"
    '        cbtMCCRouteVLCC.DisplayMember = "Name"
    '        cbtMCCRouteVLCC.ParentValue = "ParentCode"
    '    End If
    'End Sub

    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        'dr = dt.NewRow
        'dr("Code") = "E"
        'dr("Shift") = "Evening"
        'dt.Rows.Add(dr)

        txtFromShift.DataSource = dt
        txtFromShift.ValueMember = "Code"

    End Sub

    Sub LoadSRNAmountType()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")

        Dim dr As DataRow = dt.NewRow

        dr = dt.NewRow
        dr("Code") = "ZeroAndNonZero"
        dr("Name") = "Zero And Non Zero"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "Zero"
        dr("Name") = "Zero"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "NonZero"
        dr("Name") = "Non Zero"
        dt.Rows.Add(dr)

        cboSRNAmounType.DataSource = dt
        cboSRNAmounType.ValueMember = "Code"
        cboSRNAmounType.DisplayMember = "Name"

        cboSRNAmounType.SelectedValue = "ZeroAndNonZero"
    End Sub

    Sub LoadShiftTo()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        'dr("Code") = "M"
        'dr("Shift") = "Morning"
        'dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtToShift.DataSource = dt
        txtToShift.ValueMember = "Code"
        'cbgShift.DisplayMember = "Shift"
    End Sub

    Sub FormatGrid()
        Dim summaryItem As New GridViewSummaryItem()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True
            gv.Columns(ii).FormatString = "{0:n2}"
        Next

        If gv.Columns.Contains("Handling_Charges_Amount") = True Then
            gv.Columns("Handling_Charges_Amount").IsVisible = True
            gv.Columns("Handling_Charges_Amount").Width = 100
            gv.Columns("Handling_Charges_Amount").HeaderText = "Handling Charges"
        End If

        If gv.Columns.Contains("Transporter Code") = True Then
            gv.Columns("Transporter Code").IsVisible = True
            gv.Columns("Transporter Code").Width = 75
            gv.Columns("Transporter Code").HeaderText = "Transporter Code"
        End If

        If gv.Columns.Contains("Transporter Name") = True Then
            gv.Columns("Transporter Name").IsVisible = True
            gv.Columns("Transporter Name").Width = 75
            gv.Columns("Transporter Name").HeaderText = "Transporter Name"
        End If

        If gv.Columns.Contains("price_code") = True Then
            gv.Columns("price_code").IsVisible = True
            gv.Columns("price_code").Width = 100
            gv.Columns("price_code").HeaderText = "Price Code"
        End If

        If gv.Columns.Contains("Plant Code") = True Then
            gv.Columns("Plant Code").IsVisible = False
            gv.Columns("Plant Name").IsVisible = False
        End If


        gv.Columns("Plant Code").IsVisible = True
            gv.Columns("Plant Code").Width = 100
            gv.Columns("Plant Code").HeaderText = "Plant Code"

            gv.Columns("Plant Name").IsVisible = True
            gv.Columns("Plant Name").Width = 100
            gv.Columns("Plant Name").HeaderText = "Plant Name"

            gv.Columns("MCC Code").IsVisible = True
            gv.Columns("MCC Code").Width = 100
            gv.Columns("MCC Code").HeaderText = "MCC Code"

            gv.Columns("MCC Name").IsVisible = True
            gv.Columns("MCC Name").Width = 100
            gv.Columns("MCC Name").HeaderText = "MCC Name"

            gv.Columns("Milk Weight").IsVisible = True
            gv.Columns("Milk Weight").Width = 100
            gv.Columns("Milk Weight").HeaderText = "Milk Weight"

            gv.Columns("Milk Weight(KG)").IsVisible = True
            gv.Columns("Milk Weight(KG)").Width = 100
            gv.Columns("Milk Weight(KG)").HeaderText = "Milk Weight(KG)"

            gv.Columns("Milk Weight(LTR)").IsVisible = True
            gv.Columns("Milk Weight(LTR)").Width = 100
            gv.Columns("Milk Weight(LTR)").HeaderText = "Milk Weight(LTR)"

            gv.Columns("FAT(%)").IsVisible = True
            gv.Columns("FAT(%)").Width = 100
            gv.Columns("FAT(%)").HeaderText = " FAT(%)"

            gv.Columns("SNF(%)").IsVisible = True
            gv.Columns("SNF(%)").Width = 100
            gv.Columns("SNF(%)").HeaderText = "SNF(%)"

            gv.Columns("FAT(KG)").IsVisible = True
            gv.Columns("FAT(KG)").Width = 100
            gv.Columns("FAT(KG)").HeaderText = " FAT(KG)"
            gv.Columns("FAT(KG)").FormatString = "{0:n3}"

            gv.Columns("SNF(KG)").IsVisible = True
            gv.Columns("SNF(KG)").Width = 100
            gv.Columns("SNF(KG)").HeaderText = "SNF(KG)"
            gv.Columns("SNF(KG)").FormatString = "{0:n3}"

            gv.Columns("Total Solid").IsVisible = True
            gv.Columns("Total Solid").Width = 100
            gv.Columns("Total Solid").HeaderText = "Total Solid"
            gv.Columns("Total Solid").FormatString = "{0:n3}"

            gv.Columns("Cow Milk Qty (KG)").IsVisible = True
            gv.Columns("Cow Milk Qty (KG)").Width = 100
            gv.Columns("Cow Milk Qty (KG)").HeaderText = "Cow Milk Qty (KG)"

            gv.Columns("Cow FAT(%)").IsVisible = True
            gv.Columns("Cow FAT(%)").Width = 100
            gv.Columns("Cow FAT(%)").HeaderText = "Cow FAT(%)"

            gv.Columns("Cow SNF(%)").IsVisible = True
            gv.Columns("Cow SNF(%)").Width = 100
            gv.Columns("Cow SNF(%)").HeaderText = "Cow SNF(%)"

            gv.Columns("Cow FAT (KG)").IsVisible = True
            gv.Columns("Cow FAT (KG)").Width = 100
            gv.Columns("Cow FAT (KG)").HeaderText = "Cow FAT (KG)"

            gv.Columns("Cow SNF (KG)").IsVisible = True
            gv.Columns("Cow SNF (KG)").Width = 100
            gv.Columns("Cow SNF (KG)").HeaderText = "Cow SNF (KG)"

            gv.Columns("Cow Total Solid").IsVisible = True
            gv.Columns("Cow Total Solid").Width = 100
            gv.Columns("Cow Total Solid").HeaderText = "Cow Total Solid"

            gv.Columns("Buffalo Milk Qty (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo Milk Qty (KG)").Width = 100
            gv.Columns("Buffalo Milk Qty (KG)").HeaderText = "Buffalo Milk Qty (KG)"

            gv.Columns("Buffalo SNF(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo SNF(%)").Width = 100
            gv.Columns("Buffalo SNF(%)").HeaderText = "Buffalo SNF(%)"

            gv.Columns("Buffalo FAT(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo FAT(%)").Width = 100

            gv.Columns("Buffalo FAT (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo FAT (KG)").Width = 100
            gv.Columns("Buffalo FAT (KG)").HeaderText = "Buffalo FAT (KG)"

            gv.Columns("Buffalo Total Solid").IsVisible = True
            gv.Columns("Buffalo Total Solid").Width = 100
            gv.Columns("Buffalo Total Solid").HeaderText = "Buffalo Total Solid"

            gv.Columns("SRN Qty").IsVisible = True
            gv.Columns("SRN Qty").Width = 100
            gv.Columns("SRN Qty").HeaderText = "SRN Qty"


            gv.Columns("SRN Amount").IsVisible = True
            gv.Columns("SRN Amount").Width = 100
            gv.Columns("SRN Amount").HeaderText = "SRN Amount"

            gv.Columns("Head_Load_Amount").IsVisible = True
            gv.Columns("Head_Load_Amount").Width = 100
            gv.Columns("Head_Load_Amount").HeaderText = "Head Load Amount"

            gv.Columns("EMP_Amount").IsVisible = True
            gv.Columns("EMP_Amount").Width = 100
            gv.Columns("EMP_Amount").HeaderText = "SRN EMP Amount"

            gv.Columns("TIP_Amount").IsVisible = True
            gv.Columns("TIP_Amount").Width = 100
            gv.Columns("TIP_Amount").HeaderText = "SRN TIP Amount"

        gv.Columns("Payment_Status").IsVisible = True
        gv.Columns("Payment_Status").Width = 100
        gv.Columns("Payment_Status").HeaderText = "Payment Status"


        gv.Columns("Bill_Status").IsVisible = True
        gv.Columns("Bill_Status").Width = 100
        gv.Columns("Bill_Status").HeaderText = "Bill Status"


        gv.Columns("SNF_Ded_Amount").IsVisible = False
            gv.Columns("SNF_Ded_Amount").HeaderText = "SNF Deduction Amount"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0
            'TankerFromMaster = 0 AndAlso
            If chkRejection.Checked = False Then
                Try


                    gv.Columns("NET_AMOUNT").IsVisible = True
                    gv.Columns("NET_AMOUNT").Width = 100
                    gv.Columns("NET_AMOUNT").HeaderText = "SRN Net Amount"

                    gv.Columns("Round_Off").IsVisible = True
                    gv.Columns("Round_Off").Width = 100
                    gv.Columns("Round_Off").HeaderText = "SRN Round Off"

                    gv.Columns("Handling_Charges_Amount").IsVisible = True
                    gv.Columns("Handling_Charges_Amount").Width = 100
                    gv.Columns("Handling_Charges_Amount").HeaderText = "Handling Charges"


                    Dim item112 As New GridViewSummaryItem("NET_AMOUNT", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item112)
                    Dim item113 As New GridViewSummaryItem("Round_Off", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item113)
                    Dim item114 As New GridViewSummaryItem("Handling_Charges_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item114)
                Catch ex As Exception

                End Try

            End If

            Dim item1 As New GridViewSummaryItem("Milk Weight", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Milk Weight(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Milk Weight(LTR)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("FAT(KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item101 As New GridViewSummaryItem("Head_Load_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item101)
            Dim abcd As New GridViewSummaryItem("EMP_AMOUNT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(abcd)
            Dim abcde As New GridViewSummaryItem("TIP_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(abcde)
            Dim item5 As New GridViewSummaryItem("SNF(KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item51 As New GridViewSummaryItem("Total Solid", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item51)
            Dim summaryItem1 As New GridViewSummaryItem()
            summaryItem1.FormatString = "{0:F2}"
            summaryItem1.Name = "FAT(%)"
            summaryItem1.AggregateExpression = "sum([FAT(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem1)

            Dim summaryItem2 As New GridViewSummaryItem()
            summaryItem2.FormatString = "{0:F2}"
            summaryItem2.Name = "SNF(%)"
            summaryItem2.AggregateExpression = "sum([SNF(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem2)

            Dim item6 As New GridViewSummaryItem("Cow Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Cow FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)

            Dim item8 As New GridViewSummaryItem("Cow SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)

            summaryRowItem.Add(item8)

            Dim item81 As New GridViewSummaryItem("Cow Total Solid", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item81)

            Dim summaryItem3 As New GridViewSummaryItem()
            summaryItem3.FormatString = "{0:F2}"
            summaryItem3.Name = "Cow SNF(%)"
            summaryItem3.AggregateExpression = "IIf(sum([Cow SNF (KG)])>0,sum([Cow SNF (KG)])*100/sum([Cow Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem3)

            Dim summaryItem4 As New GridViewSummaryItem()
            summaryItem4.FormatString = "{0:F2}"
            summaryItem4.Name = "Cow FAT(%)"
            summaryItem4.AggregateExpression = "IIf(sum([Cow FAT (KG)])>0,sum([Cow FAT (KG)])*100/sum([Cow Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem4)

            Dim item9 As New GridViewSummaryItem("Buffalo Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item10 As New GridViewSummaryItem("Buffalo FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item11 As New GridViewSummaryItem("Buffalo SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)
            Dim item1Baff As New GridViewSummaryItem("Buffalo Total Solid", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1Baff)
            Dim summaryItem5 As New GridViewSummaryItem()
            summaryItem5.FormatString = "{0:F2}"
            summaryItem5.Name = "Buffalo FAT(%)"
            summaryItem5.AggregateExpression = "IIf(sum([Buffalo FAT (KG)])>0,sum([Buffalo FAT (KG)])*100/sum([Buffalo Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem5)

            Dim summaryItem6 As New GridViewSummaryItem()
            summaryItem6.FormatString = "{0:F2}"
            summaryItem6.Name = "Buffalo SNF(%)"
            summaryItem6.AggregateExpression = "IIf(sum([Buffalo SNF (KG)])>0,sum([Buffalo SNF (KG)])*100/sum([Buffalo Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem6)
            Dim item13 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item13)
            Dim item12 As New GridViewSummaryItem("SRN Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)

            Dim item15 As New GridViewSummaryItem("SNF_Ded_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item15)

            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Sub Reset()
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        'cbtMCCRouteVLCC.Enabled = rbtnMCCRouteVLCCSelect.IsChecked
        EnableDisableControl(True)
        'If chkRoutewise.Checked = True AndAlso chkShiftWise.Checked = True AndAlso chkRejection.Checked = False AndAlso chkShowVLCUploaderData.Checked = False Then
        '    RadButton1.Enabled = True
        'Else
        '    RadButton1.Enabled = False
        'End If
        'If ChkDetailWise.Checked = True Then
        '    btnPrintMccDetails.Visible = True
        'Else
        '    btnPrintMccDetails.Visible = False
        'End If
        'btnPrintMccDetails.Enabled = False
        chkBillPaymentStatus.Checked = False
        fndPlant.Visible = False
        txtMccForStatus.Visible = False
        MyLabel2.Visible = False
        fndMCC.Visible = False
        fndPlant.arrValueMember = Nothing
        fndPlant.arrDispalyMember = Nothing
        txtMccForStatus.arrValueMember = Nothing
        txtMccForStatus.arrDispalyMember = Nothing
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val

        'RadGroupBox2.Enabled = val
    End Sub

    Private Sub LoadData(Optional ByVal BulkExport As Integer = 0)
        Try
            'If chkRoutewise.Checked = True AndAlso chkShiftWise.Checked = True AndAlso chkRejection.Checked = False AndAlso chkShowVLCUploaderData.Checked = False Then
            '    RadButton1.Enabled = True
            'Else
            '    RadButton1.Enabled = False
            'End If
            'If ChkDetailWise.Checked = True Then
            '    btnPrintMccDetails.Enabled = True
            'Else
            '    btnPrintMccDetails.Enabled = False
            'End If

            If txtFromDate.Value > txtToDate.Value Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If

            'If isShowTreeView Then
            '    If cbtMCCRouteVLCC.CheckedValue.Count = 0 Then
            '        clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.")
            '        Exit Sub
            '    End If
            'End If
            Dim FinalQuery As String = Nothing

            Dim qry As String = Nothing
            If chkBillPaymentStatus.Checked = True Then
                Dim whr As String = ""
                If fndPlant.arrValueMember IsNot Nothing AndAlso fndPlant.arrValueMember.Count > 0 Then
                    whr += " and Loc_Segment_Code IN (" + clsCommon.GetMulcallString(fndPlant.arrValueMember) + ") "
                End If
                If txtMccForStatus.arrValueMember IsNot Nothing AndAlso txtMccForStatus.arrValueMember.Count > 0 Then
                    whr += " and TSPL_Location_MASTER.Location_Code IN (" + clsCommon.GetMulcallString(txtMccForStatus.arrValueMember) + ") "
                End If
                FinalQuery = "   select TBL_PLANT_DETAILS.PlantCode , TBL_PLANT_DETAILS.PlantName , TBL_PLANT_DETAILS.Location_Code  as [MCC Code], TBL_PLANT_DETAILS.Location_Desc as [MCC Name], case when len (isnull (TBL_Bill_Payment_Details.Bill_Genrate_Status,'')) > 0 then TBL_Bill_Payment_Details.Bill_Genrate_Status else 'No' end [Bill Generated Status] , case when len ( isnull (TBL_Bill_Payment_Details.[Payment Process Genrate],'')) > 0 then TBL_Bill_Payment_Details.[Payment Process Genrate] else 'No' end as  [Payment Process Status]  from (select distinct Loc_Segment_Code as PlantCode, TSPL_GL_SEGMENT_CODE.description as [PlantName],TSPL_Location_MASTER.Location_Code, TSPL_Location_MASTER.Location_Desc from TSPL_Location_MASTER inner join TSPL_GL_SEGMENT_CODE on TSPL_Location_MASTER.Loc_Segment_Code = TSPL_GL_SEGMENT_CODE.Segment_code  and TSPL_Location_MASTER.Rejected_Type = 'N' and TSPL_Location_MASTER.Location_Category='MCC'  " + whr + ") TBL_PLANT_DETAILS left outer Join (
                                 select Xfinal.[Plant Code], Xfinal.MCC_CODE, Xfinal.Bill_Genrate_Status, Xfinal.[Payment Process Genrate] from (
                                 select isnull(TSPL_Location_MASTER.Loc_Segment_Code,'') As [Plant Code], TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE, case when len ( isnull (TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE,'')) > 0 then 'Yes'  else 'No' end as Bill_Genrate_Status, case when len( isnull (TSPL_PAYMENT_PROCESS_INVOICE.Doc_No,'')) > 0 then case when isnull (TSPL_PAYMENT_PROCESS_HEAD.isPosted,0) = 1 then 'Yes' else 'WIP' end  else 'No'  end as  [Payment Process Genrate], tspl_vendor_master.Vendor_Code from TSPL_VENDOR_INVOICE_HEAD 

                                 left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE= TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No 
                                 left outer join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE 
 
                                 left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE
                                 left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SRN_HEAD.VLC_CODE 
                                 left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  
                                 left outer join TSPL_Vendor_Bank_MASTER as jointBank on jointBank.Bank_Code =TSPL_VENDOR_MASTER .Joint_Bank_Code  
                                 left outer join TSPL_Vendor_Bank_MASTER as SelfBank on SelfBank .Bank_Code =TSPL_VENDOR_MASTER.Bank_Name  
                                 left outer join TSPL_MP_MASTER mp on mp.MP_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_Code 
                                 left outer join TSPL_VLC_MASTER_HEAD mp_vlc on mp_vlc.Vlc_Code=mp.VLC_Code 
                                 Left outer join tspl_vendor_master Mp_V on mp_V.Vendor_Code=mp.MP_Code 
                                 left outer join TSPL_Vendor_Bank_MASTER as jointBank_Mp on jointBank_Mp.Bank_Code =Mp_V .Joint_Bank_Code   
                                 left outer join TSPL_Vendor_Bank_MASTER as SelfBank_Mp on SelfBank .Bank_Code =Mp_V.Bank_Name 
                                 
                                 left outer join TSPL_Location_MASTER on TSPL_Location_MASTER.Location_Code = TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE and TSPL_Location_MASTER.Rejected_Type = 'N' and TSPL_Location_MASTER.Location_Category='MCC' 
                                 left outer join TSPL_PAYMENT_PROCESS_INVOICE on TSPL_PAYMENT_PROCESS_INVOICE.AP_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Document_No
                                 left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_INVOICE.Doc_No

                                 where cast ( TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE as Date)  between '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy") + "' 
                                 and ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No,'')<>''  "
                'and TSPL_VENDOR_INVOICE_HEAD.Balance_Amt>0 and TSPL_VENDOR_INVOICE_HEAD.document_type='I' and TSPL_VENDOR_INVOICE_HEAD.Invoice_Type='AP' and TSPL_VENDOR_INVOICE_HEAD.REFDocType='MI-PI'
                FinalQuery += "  ) Xfinal group by Xfinal.[Plant Code], Xfinal.MCC_CODE, Xfinal.Bill_Genrate_Status, Xfinal.[Payment Process Genrate] 
                                ) as TBL_Bill_Payment_Details on TBL_Bill_Payment_Details.[Plant Code] = TBL_PLANT_DETAILS.PlantCode and TBL_Bill_Payment_Details.MCC_CODE = TBL_PLANT_DETAILS.Location_Code inner join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TBL_PLANT_DETAILS.Location_Code and TSPL_MCC_MASTER.In_active =0  order by TBL_PLANT_DETAILS.PlantCode,TBL_PLANT_DETAILS.Location_Code  "


            Else

                'ChkDetailWise.Checked = False,TankerFromMaster = 0 AndAlso
                If chkRejection.Checked = False AndAlso chkShiftWise.Checked = False Then
                    qry = "Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.[MCC Type] ,final.[Chilling Center],final.[Plant Code],final.[Plant Name] ,final.Date ,final.[Doc Date] ,final.Shift ," &
                "final.[Route Code],final.[Route Name] ,final.[Vehicle Code] ,final.[VSP Code],final.[VSP Name],final.[Vendor Group Code] ,final.[Vendor Group Desc],final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] ," &
                " final.[Sample No] ,final.[No Of Cans] ,final.Item_Code,final.Item_Desc,final.[Milk Weight],final.[Milk Weight(KG)]," &
                " final.[Milk Weight(LTR)]  as [Milk Weight(LTR)]," &
                " final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)],final.[FAT(LTR)],final.[SNF(LTR)] ,final.[Cow Milk Qty (KG)],final.[Cow FAT(%)], Case When final.[FAT(%)] <= 5 Then final.CLR Else 0 End [Cow CLR],final.[Cow SNF(%)] , Case When final.[FAT(%)] <= 5 Then final.[FAT(KG)] Else 0 End [Cow FAT (KG)], Case When final.[FAT(%)] <= 5 Then final.[SNF(KG)] Else 0 End [Cow SNF (KG)]," &
                " final.[Buffalo Milk Qty (KG)], Case When final.[FAT(%)] > 5 Then final.CLR Else 0 End [Buffalo CLR],final.[Buffalo SNF(%)],final.[Buffalo FAT(%)], Case When final.[FAT(%)] > 5 Then final.[FAT(KG)] Else 0 End [Buffalo FAT (KG)], Case When final.[FAT(%)] > 5 Then final.[SNF(KG)] Else 0 End [Buffalo SNF (KG)],final.[Milk Type],final.[SRN No],final.[SRN Amount]," &
                " final.[SRN Qty],final.[SRN Rate],final.[Shift Status] ,Invoice_no ,Invoice_Date , IS_MANUAL, MACHINE_NO,(CASE WHEN [Sample Status]='Auto' THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL,[Transporter Code],[Transporter Name],EMP_Amount,TIP_Amount,NET_AMOUNT,Round_Off,Handling_Charges_Amount,final.[Price Code],final.Purchase_Order_No,final.Head_Load_Amount,final.SNF_Ded_Value,final.SNF_Ded_Rate,final.SNF_Ded_Amount   " &
                " From (Select TSPL_MCC_MASTER.MCC_Type as [MCC Type],case when TSPL_MCC_MASTER.is_Mcc=1 then 'MCC' else 'BMCC' end [Chilling Center] ,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.FAT_PER Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.SNF_PER Else 0 End [Cow SNF(%)]," &
                " Case When TSPL_MILK_SRN_DETAIL.FAT_PER > 5 Then TSPL_MILK_SRN_DETAIL.FAT_PER Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER > 5 Then TSPL_MILK_SRN_DETAIL.SNF_PER Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.ACC_Qty Else 0 End [Cow Milk Qty (KG)]," &
                " Case When TSPL_MILK_SRN_DETAIL.FAT_PER > 5 Then TSPL_MILK_SRN_DETAIL.ACC_Qty Else 0 End [Buffalo Milk Qty (KG)]" + Environment.NewLine
                    If objCommonVar.DisplayTypeInMilkReceipt Then
                        qry += ",TSPL_MILK_SAMPLE_DETAIL.TYPE  As [Milk Type]"
                    Else
                        qry += " , Case When Coalesce(TSPL_MILK_SRN_DETAIL.FAT_PER, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SRN_DETAIL.FAT_PER, 0) <= 5 Then 'C' Else 'B' End As [Milk Type]"
                    End If
                    qry += ", TSPL_MILK_SRN_HEAD.DOC_CODE As [Milk Receipt Code]," &
                " TSPL_MILK_SRN_HEAD.MCC_Code As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name], Convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As Date, " &
                " Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_SRN_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift, " &
                " TSPL_MILK_SRN_HEAD.ROUTE_CODE As [Route Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_SRN_HEAD.VEHICLE_CODE As [Vehicle Code]," &
                " TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Vendor Group Desc],TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code]," &
                " TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_SRN_HEAD.SAMPLE_NO As [Sample No], " &
                " TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_SRN_DETAIL.Qty As [Milk Weight], TSPL_MILK_SRN_DETAIL.ACC_Qty As [Milk Weight(KG)]," &
                " TSPL_MILK_SRN_DETAIL.ACC_Qty_LTR As [Milk Weight(LTR)], TSPL_MILK_SRN_DETAIL.FAT_PER As [FAT(%)], TSPL_MILK_SRN_DETAIL.SNF_PER As [SNF(%)], TSPL_MILK_SRN_DETAIL.CLR, " &
                " Convert(decimal(18," + clsCommon.myCstr(objCommonVar.MilkSRNFATSNFDecimalPlaces) + "), TSPL_MILK_SRN_DETAIL.FAT_PER * TSPL_MILK_SRN_DETAIL.ACC_Qty / 100) As [FAT(KG)]," &
                " Convert(decimal(18," + clsCommon.myCstr(objCommonVar.MilkSRNFATSNFDecimalPlaces) + "),TSPL_MILK_SRN_DETAIL.SNF_PER * TSPL_MILK_SRN_DETAIL.ACC_Qty / 100) As [SNF(KG)]," &
                " Convert(decimal(18," + clsCommon.myCstr(objCommonVar.MilkSRNFATSNFDecimalPlaces) + "), ROUND(TSPL_MILK_SRN_DETAIL.FAT_PER * TSPL_MILK_SRN_DETAIL.ACC_Qty_LTR / 100," + clsCommon.myCstr(objCommonVar.MilkSRNFATSNFDecimalPlaces) + ",1)) As [FAT(LTR)]," &
                " Convert(decimal(18," + clsCommon.myCstr(objCommonVar.MilkSRNFATSNFDecimalPlaces) + "),ROUND(TSPL_MILK_SRN_DETAIL.SNF_PER * TSPL_MILK_SRN_DETAIL.ACC_Qty_LTR / 100," + clsCommon.myCstr(objCommonVar.MilkSRNFATSNFDecimalPlaces) + ",1)) As [SNF(LTR)]," &
                " Case When TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample  = '' Then 'Auto' Else TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample End As [Sample Status]," &
                " TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_SRN_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no," &
                " convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample as IS_MANUAL, '' as MACHINE_NO,[Transporter Code],[Transporter Name],isnull( TSPL_MILK_SRN_DETAIL.EMP_Amount,0) as EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,isnull(TSPL_MILK_SRN_DETAIL.NET_AMOUNT,0) as NET_AMOUNT,isnull(TSPL_MILK_SRN_DETAIL.Round_Off,0) as Round_Off,isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount,TSPL_MILK_SRN_DETAIL.Price_Code as [Price Code],TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount " & Environment.NewLine +
                ",TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount " + Environment.NewLine +
                " From TSPL_MILK_SRN_DETAIL " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No = TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No " + Environment.NewLine +
                " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE " + Environment.NewLine +
                " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_Code " + Environment.NewLine +
                " Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE" + Environment.NewLine +
                " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE" + Environment.NewLine +
                " left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code " + Environment.NewLine +
                " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_SRN_HEAD.ROUTE_CODE" + Environment.NewLine +
                " Left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
                " Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
                " left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code " + Environment.NewLine +
                " left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per " + Environment.NewLine +
                " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code " &
                " where 2 = 2 "
                    qry += " and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy") + "'"
                    If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                        qry += " and 2=( case when Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and TSPL_MILK_SRN_HEAD.SHIFT='M' then 3 else 2 end  )"
                    End If
                    If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                        qry += " and 2=( case when Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy") + "' and TSPL_MILK_SRN_HEAD.SHIFT='E' then 3 else 2 end  )"
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(cboSRNAmounType.SelectedValue), "Zero") = CompairStringResult.Equal Then
                        qry += " and TSPL_MILK_SRN_DETAIL.AMOUNT = 0 "
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboSRNAmounType.SelectedValue), "NonZero") = CompairStringResult.Equal Then
                        qry += " and TSPL_MILK_SRN_DETAIL.AMOUNT <> 0 "
                    End If

                    If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                        qry += "and TSPL_MILK_SRN_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
                    End If
                    If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                        qry += " and TSPL_MILK_SRN_HEAD.ROUTE_CODE in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
                    End If
                    If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                        qry += " and TSPL_MILK_SRN_HEAD.VLC_CODE in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  "
                    End If

                    qry += " ) As final where 2=2 "

                    FinalQuery = " select TSPL_MCC_MASTER.plant_code as [Plant Code],TSPL_MCC_MASTER_Plant.MCC_Name as [Plant Name] ,TSPL_MCC_MASTER.Mcc_Code as [MCC Code], TSPL_MCC_MASTER.Mcc_Name as [MCC Name],isnull (XXX.[Milk Weight],0) as [Milk Weight] ,isnull (XXX.[Milk Weight(KG)],0) as [Milk Weight(KG)]	,isnull (XXX.[Milk Weight(LTR)],0) as [Milk Weight(LTR)] ,isnull (XXX.[FAT(%)],0) as [FAT(%)] ,isnull (XXX.CLR,0 ) as CLR,isnull (XXX.[SNF(%)],0) as [SNF(%)] ,isnull (XXX.[FAT(KG)],0) as [FAT(KG)] ,isnull ( XXX.[SNF(KG)],0) as [SNF(KG)],isnull (XXX.[Total Solid],0) as [Total Solid] ,isnull (XXX.[Cow Milk Qty (KG)],0) as [Cow Milk Qty (KG)] ,isnull (XXX.[Cow FAT(%)],0) as [Cow FAT(%)],isnull (XXX.[Cow CLR],0) as [Cow CLR] ,isnull (XXX.[Cow SNF(%)],0) as [Cow SNF(%)]  , "
                    FinalQuery += " isnull(XXX.[Cow FAT (KG)],0) as [Cow FAT (KG)] ,isnull (XXX.[Cow SNF (KG)],0) as [Cow SNF (KG)] , isnull (XXX.[Cow Total Solid],0) as [Cow Total Solid] ,isnull(XXX.[Buffalo Milk Qty (KG)],0) as [Buffalo Milk Qty (KG)] ,isnull (XXX.[Buffalo FAT(%)],0) as [Buffalo FAT(%)] ,isnull (XXX.[Buffalo CLR],0) as [Buffalo CLR] ,isnull (XXX.[Buffalo SNF(%)],0) as [Buffalo SNF(%)] ,isnull (XXX.[Buffalo FAT (KG)],0) as [Buffalo FAT (KG)] ,isnull (XXX.[Buffalo SNF (KG)],0) as [Buffalo SNF (KG)],isnull (XXX. [Buffalo Total Solid],0) as [Buffalo Total Solid] ,isnull (XXX.[SRN Qty],0) as [SRN Qty],isnull (XXX.[SRN Amount],0) as [SRN Amount],isnull (XXX.EMP_Amount,0) as EMP_Amount ,isnull (XXX.TIP_Amount,0) as TIP_Amount,isnull(XXX.NET_AMOUNT,0) as NET_AMOUNT ,isnull (XXX.Round_Off,0) as Round_Off,isnull(XXX.Handling_Charges_Amount,0) as Handling_Charges_Amount,isnull(XXX.Head_Load_Amount,0) as Head_Load_Amount ,isnull (XXX.SNF_Ded_Amount,0) as SNF_Ded_Amount , case when len( isnull (TBL_Payment_Status.MCC_Code_Selected,'')) > 0 then 'Completed ' else 'Pending' end as Payment_Status , case when len( isnull( TBL_BILL_Status.MCC_Code,'') ) > 0 then 'Completed ' else 'Pending' end as Bill_Status from TSPL_MCC_MASTER left outer Join TSPL_MCC_MASTER as TSPL_MCC_MASTER_Plant on    TSPL_MCC_MASTER_Plant.MCC_Code = TSPL_MCC_MASTER.MCC_Code  left outer join ( "
                    FinalQuery += "select aa.[Plant Code],aa.[Plant Name],aa.[MCC Code],aa.[MCC Name] ,aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(%)],aa.CLR ,aa.[SNF(%)] ,aa.[FAT(KG)] ,aa.[SNF(KG)],aa.[FAT(KG)]+aa.[SNF(KG)] as [Total Solid] ,aa.[Cow Milk Qty (KG)] ,aa.[Cow FAT(%)],aa.[Cow CLR] ,aa.[Cow SNF(%)] ,aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)],aa.[Cow FAT (KG)]+aa.[Cow SNF (KG)] as [Cow Total Solid] ,aa.[Buffalo Milk Qty (KG)] ,aa.[Buffalo FAT(%)],aa.[Buffalo CLR] ,aa.[Buffalo SNF(%)] ,aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)],aa.[Buffalo FAT (KG)]+aa.[Buffalo SNF (KG)] as [Buffalo Total Solid] ,aa.[SRN Qty],aa.[SRN Amount],aa.EMP_Amount,aa.TIP_Amount,aa.NET_AMOUNT,aa.Round_Off,aa.Handling_Charges_Amount,aa.Head_Load_Amount,aa.SNF_Ded_Amount from ( "
                    FinalQuery += " select xxx.* ,"
                    FinalQuery += "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)],"
                    FinalQuery += " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)],"
                    FinalQuery += "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)],"
                    FinalQuery += " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]"
                    FinalQuery += " from ("
                    FinalQuery += " select xx.*"
                    FinalQuery += " from ( "
                    FinalQuery += "select pp.[Plant Code]  as [Plant Code],max(pp.[Plant Name]) as [Plant Name],pp.[MCC Code] as [MCC Code] ,max(pp.[MCC Name]) as [MCC Name] ,sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)],"
                    FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)],"
                    FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]"
                    FinalQuery += " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)],"
                    FinalQuery += " sum([FAT(LTR)] ) as [FAT(LTR)] ,sum([SNF(LTR)] ) as [SNF(LTR)],"
                    FinalQuery += " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)],"
                    FinalQuery += " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)],"
                    FinalQuery += " sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR],sum(EMP_Amount) as EMP_Amount,sum(TIP_Amount) as TIP_Amount,sum(NET_AMOUNT) as NET_AMOUNT,sum(Round_Off) as Round_Off,sum(Handling_Charges_Amount) as Handling_Charges_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount  from ("
                    FinalQuery += "" + Environment.NewLine + Environment.NewLine + qry + Environment.NewLine + Environment.NewLine + ""
                    FinalQuery += " ) as  pp group by pp.[Plant Code],pp.[MCC Code] "
                    FinalQuery += " )as xx"
                    FinalQuery += " ) as xxx"
                    If BulkExport = 4 Then
                        FinalQuery += " ) as aa  "
                    Else

                        FinalQuery += " ) as aa "
                        FinalQuery += "  ) XXX on   TSPL_MCC_MASTER.MCC_Code = XXX.[MCC Code] and TSPL_MCC_MASTER.plant_code = XXX.[Plant Code]  "
                        FinalQuery += " left outer join (select distinct MCC_Code_Selected from TSPL_PAYMENT_PROCESS_HEAD where convert (date,from_Date,103) = convert (date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MM/yyyy") + "',103)  and convert (date, To_Date,103) = convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MM/yyyy") + "',103)  and isPosted = 1 ) TBL_Payment_Status on TBL_Payment_Status .MCC_Code_Selected = TSPL_MCC_MASTER.MCC_Code "
                        FinalQuery += " left outer join (select distinct  MCC_Code from TSPL_VENDOR_INVOICE_HEAD where convert (date,Vendor_Invoice_Date,103) = convert (date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MM/yyyy") + "',103) ) TBL_BILL_Status on TBL_BILL_Status.MCC_Code =  TSPL_MCC_MASTER.MCC_Code  order by [Plant Code],[MCC Code] "

                    End If

                Else
                    Dim strRejection As String = Nothing
                    Dim strSRNQuery As String = Nothing
                    Dim strRejectionQuery As String = Nothing
                    If chkRejection.Checked = True Then
                        strRejection = ",'' as RejectType,'' as RejectReason,'' as Defaulter"
                    Else
                        strRejection = ""
                    End If
                    strSRNQuery = "Select  TSPL_MCC_MASTER.MCC_Type as [MCC Type],case when TSPL_MCC_MASTER.is_Mcc=1 then 'MCC' else 'BMCC' end [Chilling Center] ,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc, TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount,Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.FAT_PER Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.SNF_PER Else 0 End [Cow SNF(%)]," &
                " Case When TSPL_MILK_SRN_DETAIL.FAT_PER > 5 Then TSPL_MILK_SRN_DETAIL.FAT_PER Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER > 5 Then TSPL_MILK_SRN_DETAIL.SNF_PER Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.ACC_Qty Else 0 End [Cow Milk Qty (KG)]," &
                " Case When TSPL_MILK_SRN_DETAIL.FAT_PER > 5 Then TSPL_MILK_SRN_DETAIL.ACC_Qty Else 0 End [Buffalo Milk Qty (KG)]" + Environment.NewLine
                    If objCommonVar.DisplayTypeInMilkReceipt Then
                        strSRNQuery += ",TSPL_MILK_SAMPLE_DETAIL.TYPE  As [Milk Type] "
                    Else
                        strSRNQuery += ", Case When Coalesce(TSPL_MILK_SRN_DETAIL.FAT_PER, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SRN_DETAIL.FAT_PER, 0) <= 5 Then 'C' Else 'B' End As [Milk Type]"
                    End If
                    strSRNQuery += ", TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code]," &
                " TSPL_MILK_SRN_HEAD.MCC_Code As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name], Convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As Date, " &
                " Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_SRN_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift, " &
                " TSPL_MILK_SRN_HEAD.ROUTE_CODE As [Route Code],tspl_mcc_route_master.Supervisor_Name as [SuperVisor Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE As [Vehicle Code]," &
                " TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Vendor Group Desc] ,TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code]," &
                " TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_SRN_HEAD.SAMPLE_NO As [Sample No], " &
                " TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_SRN_DETAIL.Qty As [Milk Weight], TSPL_MILK_SRN_DETAIL.ACC_Qty As [Milk Weight(KG)]," &
                " TSPL_MILK_SRN_DETAIL.ACC_Qty_LTR As [Milk Weight(LTR)], TSPL_MILK_SRN_DETAIL.FAT_PER As [FAT(%)], TSPL_MILK_SRN_DETAIL.SNF_PER As [SNF(%)], TSPL_MILK_SRN_DETAIL.CLR,  " &
                " TSPL_MILK_SRN_DETAIL.FAT_kg As [FAT(KG)], TSPL_MILK_SRN_DETAIL.SNF_kg As [SNF(KG)], Case When TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample = '' Then 'Auto' Else TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample End As [Sample Status]," &
                " TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_SRN_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no," &
                " convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample as IS_MANUAL, '' as MACHINE_NO,(CASE WHEN TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample='Auto' THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL,TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount " & strRejection & "  " &
                " ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount " + Environment.NewLine +
                " ,TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code,[Transporter Code], [Transporter Name],isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount " &
                " From TSPL_MILK_SRN_DETAIL " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No = TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE " + Environment.NewLine +
                " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE " &
                " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_Code " + Environment.NewLine +
                " Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE" + Environment.NewLine +
                " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE" + Environment.NewLine +
                " left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code " + Environment.NewLine +
                " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_SRN_HEAD.ROUTE_CODE" + Environment.NewLine +
                " left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
                " Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
                "left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code" + Environment.NewLine +
                "left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per" + Environment.NewLine +
                " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code " &
                " where 2 = 2 "
                    strSRNQuery += " and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as date) <='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'"
                    If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                        strSRNQuery += " and 2=( case when Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and TSPL_MILK_SRN_HEAD.SHIFT='M' then 3 else 2 end  )"
                    End If
                    If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                        strSRNQuery += " and 2=( case when Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' and TSPL_MILK_SRN_HEAD.SHIFT='E' then 3 else 2 end  )"
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(cboSRNAmounType.SelectedValue), "Zero") = CompairStringResult.Equal Then
                        strSRNQuery += " and  TSPL_MILK_SRN_DETAIL.AMOUNT = 0 "
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboSRNAmounType.SelectedValue), "NonZero") = CompairStringResult.Equal Then
                        strSRNQuery += " and TSPL_MILK_SRN_DETAIL.AMOUNT <> 0 "
                    End If

                    If Not chkShowVLCUploaderData.Checked Then
                        strSRNQuery += " and TSPL_MILK_RECEIPT_DETAIL.Against_Uploader_TR_No is null "
                    End If



                    If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                        strSRNQuery += "and TSPL_MILK_SRN_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
                    End If
                    If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                        strSRNQuery += " and TSPL_MILK_RECEIPT_DETAIL .Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
                    End If
                    If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                        strSRNQuery += " and TSPL_MILK_SRN_HEAD.VLC_CODE in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  "
                    End If


                    strRejectionQuery = "  Select TSPL_MCC_MASTER.MCC_Type as [MCC Type],case when TSPL_MCC_MASTER.is_Mcc=1 then 'MCC' else 'BMCC' end [Chilling Center] ,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount,Case When TSPL_MILK_REJECT_DETAIL.FAT < 5 Then TSPL_MILK_REJECT_DETAIL.FAT Else 0 End [Cow FAT(%)], " &
                " Case When TSPL_MILK_REJECT_DETAIL.FAT < 5 Then TSPL_MILK_REJECT_DETAIL.SNF Else 0 End [Cow SNF(%)], " &
                " Case When TSPL_MILK_REJECT_DETAIL.FAT > 5 Then TSPL_MILK_REJECT_DETAIL.FAT Else 0 End [Buffalo FAT(%)], " &
                " Case When TSPL_MILK_REJECT_DETAIL.FAT > 5 Then TSPL_MILK_REJECT_DETAIL.SNF Else 0 End [Buffalo SNF(%)], " &
                " Case When TSPL_MILK_REJECT_DETAIL.FAT <= 5 Then TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG Else 0 End [Cow Milk Qty (KG)], " &
                " Case When TSPL_MILK_REJECT_DETAIL.FAT > 5 Then TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_LTR Else 0 End [Buffalo Milk Qty (KG)], "
                    'strRejectionQuery += " Case When Coalesce(TSPL_MILK_REJECT_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_REJECT_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], "
                    strRejectionQuery += " case when TSPL_MILK_REJECT_TYPE.Type is not null  then TSPL_MILK_REJECT_TYPE.Type When Coalesce(TSPL_MILK_REJECT_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_REJECT_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], "
                    strRejectionQuery += " TSPL_MILK_REJECT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_REJECT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name], " &
                " Convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_REJECT_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_MILK_REJECT_DETAIL.ROUTE_CODE As [Route Code],tspl_mcc_route_master.Supervisor_Name as [SuperVisor Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_REJECT_DETAIL.VEHICLE_CODE As [Vehicle Code], TSPL_MILK_REJECT_DETAIL.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name],TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Vendor Group Desc] ,TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_REJECT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_REJECT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT As [Milk Weight], TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG As [Milk Weight(KG)], TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG As [Milk Weight(LTR)], TSPL_MILK_REJECT_DETAIL.FAT As [FAT(%)], TSPL_MILK_REJECT_DETAIL.SNF As [SNF(%)],0 as CLR, Convert(decimal(18,3), TSPL_MILK_REJECT_DETAIL.FAT * TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG / 100) As [FAT(KG)], " &
                " Convert(decimal(18,3),TSPL_MILK_REJECT_DETAIL.SNF * TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG / 100) As [SNF(KG)], '' As [Sample Status], " &
                " TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], " &
                " TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_SRN_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status], " &
                " TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , " &
                " '' as IS_MANUAL ,'' as MACHINE_NO ,'' as IS_MILK_SAMPLE_MANUAL,TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount,TSPL_MILK_REJECT_TYPE.description as RejectType, " &
                " case when TSPL_MILK_REJECT_DETAIL.Is_Return=0 then '' when TSPL_MILK_REJECT_DETAIL.Is_Return=1 then 'Return' when TSPL_MILK_REJECT_DETAIL.Is_Return=2 then 'Drain' end as ReajectReason,TSPL_MILK_REJECT_DETAIL.Defaulter  " + Environment.NewLine +
                " ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount " + Environment.NewLine +
                " ,TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code,[Transporter Code], [Transporter Name],isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount From   TSPL_MILK_REJECT_DETAIL " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_REJECT_HEAD On TSPL_MILK_REJECT_HEAD.DOC_CODE = TSPL_MILK_REJECT_DETAIL.DOC_CODE " + Environment.NewLine +
                " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODe=TSPL_MILK_SRN_HEAD.Against_Reject_No and TSPL_MILK_SRN_HEAD.SAMPLE_NO=TSPL_MILK_REJECT_DETAIL.SAMPLE_NO " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE " + Environment.NewLine +
                " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code" + Environment.NewLine +
                " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE " + Environment.NewLine +
                " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_REJECT_HEAD.MCC_CODE " + Environment.NewLine +
                " Left Outer Join TSPL_VLC_MASTER_HEAD On  TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_REJECT_DETAIL.VLC_CODE " + Environment.NewLine +
                "   " + Environment.NewLine +
                " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_REJECT_DETAIL.VSP_CODE " + Environment.NewLine +
                "  left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code " + Environment.NewLine +
                " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_REJECT_DETAIL.ROUTE_CODE " + Environment.NewLine +
                " Left Outer Join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_REJECT_HEAD.MCC_CODE " &
                " And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) " &
                " And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_REJECT_HEAD.SHIFT " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE " &
                " And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code " &
                " left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code " + Environment.NewLine +
                " left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per " + Environment.NewLine +
                " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code " &
                " left join TSPL_MILK_REJECT_TYPE on TSPL_MILK_REJECT_TYPE.code=TSPL_MILK_REJECT_DETAIL.Reject_Type " &
                " where  Against_Reject_No <> ''"
                    strRejectionQuery += " and TSPL_MILK_REJECT_HEAD.DOC_DATE >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.DOC_DATE <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
                If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                    strRejectionQuery += " and 2=( case when TSPL_MILK_REJECT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.SHIFT='M' then 3 else 2 end  )"
                End If
                If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                    strRejectionQuery += " and 2=( case when TSPL_MILK_REJECT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.SHIFT='E' then 3 else 2 end  )"
                End If
                Dim arr1 As List(Of String) = Nothing

                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                        strRejectionQuery += " and TSPL_MILK_REJECT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
                    End If
                    If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                        strRejectionQuery += " and TSPL_MILK_REJECT_DETAIL .Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
                    End If
                    If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                        strRejectionQuery += " and TSPL_MILK_REJECT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  "
                    End If




                If chkRejection.Checked = True Then
                    qry = "Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.[MCC Type] ,final.[Chilling Center],final.[Plant Code],final.[Plant Name] ,final.Date ,final.[Doc Date] ,final.Shift ," &
                " final.[Route Code],final.[Route Name] ,final.[Vehicle Code] ,final.[VSP Code],final.[VSP Name], final.[Vendor Group Code],final.[Vendor Group Desc] ,final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] ," &
                " final.[Sample No] ,final.[No Of Cans],final.Item_Code,final.Item_Desc,final.[Milk Weight],final.[Milk Weight(KG)]," &
                " final.[Milk Weight(LTR)]  as [Milk Weight(LTR)]," &
                " final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)] ,final.[Cow Milk Qty (KG)],final.[Cow FAT(%)], Case When final.[FAT(%)] <= 5 Then CLR Else 0 End [Cow CLR],final.[Cow SNF(%)] , Case When final.[FAT(%)] <= 5 Then final.[FAT(KG)] Else 0 End [Cow FAT (KG)], Case When final.[FAT(%)] <= 5 Then final.[SNF(KG)] Else 0 End [Cow SNF (KG)]," &
                " final.[Buffalo Milk Qty (KG)], Case When final.[FAT(%)] > 5 Then CLR Else 0 End [Buffalo CLR],final.[Buffalo SNF(%)],final.[Buffalo FAT(%)], Case When final.[FAT(%)] > 5 Then final.[FAT(KG)] Else 0 End [Buffalo FAT (KG)], Case When final.[FAT(%)] > 5 Then final.[SNF(KG)] Else 0 End [Buffalo SNF (KG)],final.[Milk Type],final.[SRN No],final.[SRN Amount]," &
                " final.[SRN Qty],final.[SRN Rate],final.[Shift Status] ,Invoice_no ,Invoice_Date , IS_MANUAL, MACHINE_NO,IS_MILK_SAMPLE_MANUAL,RejectType,RejectReason,Defaulter, " &
                " final.EMP_Amount,final.TIP_Amount,final.Service_Charge_Amount ,([SRN Amount]+EMP_Amount+TIP_Amount-Service_Charge_Amount) as NetAmount,final.Purchase_Order_No,final.Head_Load_Amount ,final.SNF_Ded_Value,final.SNF_Ded_Rate,final.SNF_Ded_Amount, final.price_code,final.[Transporter Code],final.[Transporter Name],final.Handling_Charges_Amount  From ( " & strSRNQuery & " Union All " & strRejectionQuery & ") As final where 2=2 "
                Else
                    qry = "Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.[MCC Type] ,final.[Chilling Center],final.[Plant Code],final.[Plant Name] ,final.Date ,final.[Doc Date] ,final.Shift ," &
                "final.[Route Code],final.[Route Name] ,final.[Vehicle Code] ,final.[VSP Code],final.[VSP Name],final.[Vendor Group Code],final.[Vendor Group Desc] ,final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] ," &
                " final.[Sample No] ,final.[No Of Cans],final.Item_Code,final.Item_Desc ,final.[Milk Weight],final.[Milk Weight(KG)]," &
                " final.[Milk Weight(LTR)]  as [Milk Weight(LTR)]," &
                " final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)] ,final.[Cow Milk Qty (KG)],final.[Cow FAT(%)], Case When final.[FAT(%)] <= 5 Then CLR Else 0 End [Cow CLR],final.[Cow SNF(%)] , Case When final.[FAT(%)] <= 5 Then final.[FAT(KG)] Else 0 End [Cow FAT (KG)], Case When final.[FAT(%)] <= 5 Then final.[SNF(KG)] Else 0 End [Cow SNF (KG)]," &
                " final.[Buffalo Milk Qty (KG)],final.[Buffalo FAT(%)],Case When final.[FAT(%)] > 5 Then CLR Else 0 End [Buffalo CLR],final.[Buffalo SNF(%)], Case When final.[FAT(%)] > 5 Then final.[FAT(KG)] Else 0 End [Buffalo FAT (KG)], Case When final.[FAT(%)] > 5 Then final.[SNF(KG)] Else 0 End [Buffalo SNF (KG)],final.[Milk Type],final.[SRN No],final.[SRN Amount]," &
                " final.[SRN Qty],final.[SRN Rate],final.[Shift Status] ,Invoice_no ,Invoice_Date , IS_MANUAL, MACHINE_NO,IS_MILK_SAMPLE_MANUAL, " &
                " final.EMP_Amount,final.TIP_Amount ,final.Service_Charge_Amount ,([SRN Amount]+EMP_Amount+final.TIP_Amount-Service_Charge_Amount) as NetAmount,final.[SuperVisor Code] as [SuperVisor Code],final.Purchase_Order_No,final.Head_Load_Amount,final.SNF_Ded_Value,final.SNF_Ded_Rate,final.SNF_Ded_Amount, final.price_code,final.[Transporter Code],final.[Transporter Name],final.Handling_Charges_Amount   From ( " & strSRNQuery & " ) As final where 2=2  "
                End If








                FinalQuery = "select aa.[Plant Code],aa.[Plant Name],aa.[MCC Code],aa.[MCC Name],aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(%)] ,aa.CLR,aa.[SNF(%)] ,aa.[FAT(KG)] ,aa.[SNF(KG)],aa.[FAT(KG)]+aa.[SNF(KG)] as [Total Solid],aa.[Cow Milk Qty (KG)] ,aa.[Cow FAT(%)] ,aa.[Cow CLR] ,aa.[Cow SNF(%)] ,aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)],aa.[Cow FAT (KG)]+aa.[Cow SNF (KG)] as [Cow Total Solid] ,aa.[Buffalo Milk Qty (KG)] ,aa.[Buffalo FAT(%)]  ,aa.[Buffalo CLR],aa.[Buffalo SNF(%)] ,aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)],aa.[Buffalo FAT (KG)]+aa.[Buffalo SNF (KG)] as [Buffalo Total Solid] ,aa.[SRN Qty],aa.[SRN Amount],aa.EMP_AMOUNT,aa.TIP_Amount,aa.Head_Load_Amount , aa.SNF_Ded_Amount,aa.price_code,aa.[Transporter Code],aa.[Transporter Name],aa.Handling_Charges_Amount from ( " & Environment.NewLine &
                    " select xxx.* ," & Environment.NewLine &
                    "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)]," & Environment.NewLine &
                    " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)]," & Environment.NewLine &
                    "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)]," & Environment.NewLine &
                    " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]" & Environment.NewLine &
                    " from (" & Environment.NewLine &
                    " select xx.*" & Environment.NewLine &
                    " from ( " & Environment.NewLine &
                    "select pp.[Plant Code]  as [Plant Code],max(pp.[Plant Name] )  as [Plant Name],pp.[MCC Code] as [MCC Code] ,max(pp.[MCC Name]) as [MCC Name] ,sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)]," & Environment.NewLine &
                    " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)]," & Environment.NewLine &
                    " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]" & Environment.NewLine &
                    " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)]," & Environment.NewLine &
                    " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)]," & Environment.NewLine &
                    " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)]," & Environment.NewLine &
                    " sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR],sum(EMP_AMOUNT) as EMP_AMOUNT,sum(TIP_Amount) as TIP_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount, max(price_code) as price_code,max([Transporter Code]) as [Transporter Code],max([Transporter Name]) as [Transporter Name],sum(Handling_Charges_Amount) as Handling_Charges_Amount from (" & Environment.NewLine &
                    "" + Environment.NewLine + Environment.NewLine + qry + Environment.NewLine + Environment.NewLine + "" & Environment.NewLine &
                    " ) as  pp group by pp.[Plant Code],pp.[MCC Code] " & Environment.NewLine &
                    " )as xx" & Environment.NewLine &
                    " ) as xxx" & Environment.NewLine &
                    " ) as aa" & Environment.NewLine
                    If BulkExport <> 4 Then
                        FinalQuery += " order by [Plant Code],[MCC Code] "
                    End If

                End If

            '' bulk export
            End If
            If BulkExport = 1 Then
                transportSql.BulkExport("MCC Bill Genration Status", FinalQuery, "", "csv")
                Exit Sub
            ElseIf BulkExport = 2 Then
                transportSql.BulkExport("MCC Bill Genration Status", FinalQuery, "", "xls")
                Exit Sub
            ElseIf BulkExport = 3 Then
                'Dim FinalQueryFinal As String = "  select kkkk.Date,max(kkkk.[Doc Date]) as [Doc Date],kkkk.[MCC Code],max(kkkk.[MCC Name]) as[MCC Name],max(kkkk.[Plant Code])  as [Plant Code],max(kkkk.[Plant Name] )  as [Plant Name] ,kkkk.[Route Code],max(kkkk.[Route Name]) as [Route Name] ,sum(kkkk.Mrn_qty) as Mrn_qty , sum(kkkk.eve_qty) as eve_qty ,sum (kkkk.Mrn_Fat) as Mrn_Fat ,sum(kkkk.eve_Fat) as eve_Fat,sum(kkkk.Mrn_Snf) as Mrn_Snf ,sum(kkkk.eve_Snf) as eve_Snf,( sum (kkkk.Mrn_qty) + sum (kkkk.eve_qty)) as Total_Qty, (sum (kkkk.Mrn_Fat) +sum (kkkk.eve_Fat))/2 as Total_Fat, (sum (kkkk.Mrn_Snf )+sum (kkkk.eve_Snf))/2 as Total_Snf  from (  select tttt.Date , convert (varchar, tttt.Date,103) as [Doc Date],tttt.[MCC Code], max (tttt.[MCC Name]) as [MCC Name],max(tttt.[Plant Code])  as [Plant Code],max(tttt.[Plant Name] )  as [Plant Name] ,tttt.[Route Code],max(tttt.[Route Name]) as [Route Name],sum (tttt.Mrn_qty) as Mrn_qty, sum (tttt.eve_qty) as eve_qty , case when sum(tttt.mrn_qty) =0 then 0 else  round((sum(tttt.Mrn_Fat)*100)/sum(tttt.mrn_qty),4) end as Mrn_Fat ,   case when sum(tttt.eve_qty) =0 then 0 else round((sum(tttt.eve_Fat)*100) / sum(tttt.eve_qty),4) end as eve_Fat ,   case when sum(tttt.mrn_qty) =0 then 0 else  round((sum(tttt.Mrn_Snf)*100)/sum(tttt.mrn_qty),4) end as Mrn_Snf ,  case when sum(tttt.eve_qty) =0 then 0 else  round((sum(tttt.eve_Snf)*100)/sum(tttt.eve_qty),4) end as eve_Snf   from (  select Date, [MCC Code],max([MCC Name]) as [MCC Name],max([Plant Code])  as [Plant Code],max([Plant Name] )  as [Plant Name] ,[Route Code],max([Route Name]) as [Route Name],  case when ([Shift])='Evening' then sum([Milk Weight(LTR)]) else 0 end as eve_qty, case when (shift)='Morning' then sum([Milk Weight(LTR)]) else 0 end as Mrn_qty ,  case when [Shift]='Evening' then sum( [FAT(KG)]) else 0 end as eve_Fat  ,case when shift='Morning' then sum([FAT(KG)]) else 0 end as Mrn_Fat,    case when [Shift]='Evening' then sum( [SNF(KG)]) else 0 end as eve_Snf,   case when shift='Morning' then sum([SNF(KG)]) else 0 end as Mrn_Snf     from (select finallll.Date,finallll.[MCC Code],max( finallll.[MCC Name]) as [MCC Name],max(finallll.[Plant Code])  as [Plant Code],max(finallll.[Plant Name] )  as [Plant Name]  ,finallll.[Route Code],max(finallll.[Route Name]) as [Route Name], finallll.Shift, max (finallll.[Milk Weight(LTR)]) as [Milk Weight(LTR)] , max (finallll.[FAT(KG)])  as [FAT(KG)] ,max(finallll.[SNF(KG)]) as [SNF(KG)],max(finallll.[Head_Load_Amount]) as [Head_Load_Amount]  from (  " &
                '                                "  " + FinalQuery + "  " &
                '                                " )finallll group by finallll.Date,[MCC Code],[Route Code],finallll.Shift ) pppp group by pppp.Date,pppp.[MCC Code],pppp.[Route Code],pppp.Shift) tttt group by tttt.Date ,tttt .[MCC Code],tttt.[Route Code]  ) kkkk group by kkkk.Date ,kkkk.[MCC Code],kkkk.[Route Code]  "

                'dt = clsDBFuncationality.GetDataTable(FinalQueryFinal)
                'Dim frmCRV As New frmCrystalReportViewer()
                'frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, clsERPFuncationality.CompanyAddresShowinHeader(), "rptMccMilkRegisterRoutShiftWise", "MCC Milk Register Route/Shift wise Report", "Address.rpt")
                'frmCRV = Nothing
                'Exit Sub
            ElseIf BulkExport = 4 Then
                'Dim QtyforMccDetailsPrart1 As String = " select '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "'  as FromDate,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy") + "' as ToDate , XXXX.[MCC Code] , Max(XXXX.[MCC Name]) as [MCC Name],max(XXXX.[Plant Code])  as [Plant Code],max(XXXX.[Plant Name] )  as [Plant Name] ,XXXX.[Doc Date],sum( XXXX.[Milk Weight]) as [Milk Weight]  ,sum(XXXX.[Milk Weight(KG)] ) as [Milk Weight(KG)] , sum(XXXX.[Milk Weight(LTR)]) as [Milk Weight(LTR)] ,Sum(XXXX.[FAT(KG)]) as [FAT(KG)], sum(XXXX.[SNF(KG)]) as [SNF(KG)] ,sum(XXXX.[SRN Amount]) as NetAmount,sum(XXXX.Head_Load_Amount) as Head_Load_Amount  from ( " &
                '                                 " " + FinalQuery + " " &
                '                                 "  )XXXX group By XXXX.[Doc Date], XXXX.[MCC Code] order by convert (date,  XXXX.[Doc Date] , 103) , XXXX.[MCC Code] "

                'Dim QtyforMccDetailsPrart2 As String = " Select case when  XXXFinal.[Milk Type]  = 'C' then 'Cow' else  'Buffalo' end as [Milk Type], Cast ((cast ( XXXFinal.TFAT as decimal(18,2)) * 100 / XXXFinal.QTY ) as decimal(10,2)) as FAT ,Cast( (cast ( XXXFinal.TSNF as decimal(18,2)) * 100 / XXXFinal.QTY )as decimal(10,2)) as SNF ,XXXFinal.QTY , XXXFinal.TFAT ,XXXFinal.TSNF , XXXFinal.NetAmount  from ( select XXX.[Milk Type]  ,Sum ( XXX.[Cow Milk Qty (KG)])  + sum (XXX.[Buffalo Milk Qty (KG)]) as QTY, sum( XXX.[Cow FAT (KG)]) + sum(XXX.[Buffalo FAT (KG)]) as TFAT , sum (XXX.[Cow SNF (KG)]) + Sum(XXX.[Buffalo SNF (KG)]) as TSNF ,sum( XXX.[SRN Amount]) as NetAmount,sum( XXX.Head_Load_Amount) as Head_Load_Amount   from ( " &
                '                                       " " + FinalQuery + " " &
                '                                       "  ) XXX group by XXX.[Milk Type] ) XXXFinal "
                'Dim dtPart1 As DataTable = clsDBFuncationality.GetDataTable(QtyforMccDetailsPrart1)
                'Dim dtPart2 As DataTable = clsDBFuncationality.GetDataTable(QtyforMccDetailsPrart2)
                'Dim frmCRV As New frmCrystalReportViewer()
                'frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dtPart1, clsERPFuncationality.CompanyAddresShowinHeader(), "rptMccMilkRegisterDetail", "MCC Milk Register Report", "Address.rpt", "rptMccMilkRegisterDetailTypeWise.rpt", dtPart2)
                ''frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dtPart1,dtPart2, "rptMccMilkRegisterDetail", "MCC Milk Register Report", , )
                'frmCRV = Nothing
                'Exit Sub
            End If


            dt = clsDBFuncationality.GetDataTable(FinalQuery)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dt
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            If chkBillPaymentStatus.Checked = False Then
                FormatGrid()
            End If


            gv.ShowGroupPanel = True
            gv.MasterTemplate.AutoExpandGroups = True

            RadPageView1.SelectedPage = RadPageViewPage2
            ReStoreGridLayout()
            gv.BestFitColumns()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Function GetReportID() As String
        Dim ReportID As String = MyBase.Form_ID
        ReportID += "P"

        If chkRejection.Checked = False AndAlso chkShiftWise.Checked = False Then
            ReportID += "1"
        End If
        If chkBillPaymentStatus.Checked = True Then
            ReportID += "_BS"
        End If
        Return ReportID
    End Function

    Private Sub ReStoreGridLayout()
        Try
            'Dim ReportID As String = GetReportID()
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub FrmMCCMilkRegister_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Control AndAlso e.Alt AndAlso e.Shift AndAlso e.KeyCode = Keys.U Then
            If chkShowVLCUploaderData.Visible Then
                chkShowVLCUploaderData.Visible = False
            Else
                Dim pwd As New FrmPWD(Nothing)
                pwd.strCode = clsFixedParameterCode.MilkProcurementUploader
                pwd.strType = clsFixedParameterType.MilkProcurementUploader
                pwd.ShowDialog()
                If pwd.isPasswordCorrect Then
                    chkShowVLCUploaderData.Visible = True
                End If
            End If
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = GetReportID()
        TemplateGridview = gv
        LoadData()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        ' Dim ReportID As String = GetReportID()
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub rbtnMCCRouteVLCCAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        'cbtMCCRouteVLCC.Enabled = rbtnMCCRouteVLCCSelect.IsChecked
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
            arrHeader.Add(CompName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")


            ''If rbtnMCCRouteVLCCSelect.IsChecked Then
            'Dim arr As List(Of String)
            'If cbtMCCRouteVLCC.CheckedText.Count > 0 Then
            '    arr = cbtMCCRouteVLCC.CheckedText(1)
            '    If arr IsNot Nothing AndAlso arr.Count > 0 Then
            '        arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
            '    End If
            'End If
            'If cbtMCCRouteVLCC.CheckedText.Count > 1 Then
            '    arr = cbtMCCRouteVLCC.CheckedText(2)
            '    If arr IsNot Nothing AndAlso arr.Count > 0 Then
            '        arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
            '    End If
            'End If
            'If cbtMCCRouteVLCC.CheckedText.Count > 2 Then
            '    arr = cbtMCCRouteVLCC.CheckedText(3)
            '    If arr IsNot Nothing AndAlso arr.Count > 0 Then
            '        arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
            '    End If
            'End If
            ''End If

            'Dim arr As List(Of String)

            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember) + " "))
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember) + " "))
                End If
                If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(txtVLC.arrDispalyMember) + " "))
                End If


            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("MCC Milk Register", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("MCC Milk Register", gv, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(me,ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error,Me.text)
        End Try
    End Sub

    Private Sub BulkExportCSV_Click(sender As Object, e As EventArgs) Handles BulkExportCSV.Click
        LoadData(1)
    End Sub

    Private Sub BulkExportXls_Click(sender As Object, e As EventArgs) Handles BulkExportXls.Click
        LoadData(2)
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCC", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
        RefreshRoute()
        RefreshVLC()
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            'If txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
            '    txtMCC.Focus()
            '    Throw New Exception("Please select MCC")
            'End If
            Dim qry As String = "select Route_Code,Route_Name from TSPL_MCC_ROUTE_MASTER where 2=2 "
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                qry += "  and MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            End If

            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("PCURoute", qry, "Route_Code", "Route_Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
            RefreshVLC()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVLC__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
        Try
            'If txtRoute.arrValueMember Is Nothing OrElse txtRoute.arrValueMember.Count <= 0 Then
            '    txtRoute.Focus()
            '    Throw New Exception("Please select at least route")
            'End If
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code where 2=2 and TSPL_VLC_MASTER_HEAD.Active='1' "
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_VLC_MASTER_HEAD.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            End If

            txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC", qry, "VLC_Code", "VLC_Name", txtVLC.arrValueMember, txtVLC.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub RefreshRoute()
        If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            Dim qry As String = "select Route_Code from TSPL_MCC_ROUTE_MASTER where Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  and MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtRoute.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("Route_Code")))
                Next
                txtRoute.arrValueMember = arr
            End If
        End If
    End Sub

    Sub RefreshVLC()
        If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
            Dim qry As String = "select VLC_Code from TSPL_VLC_MASTER_HEAD where  VLC_Code in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  and Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtVLC.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("VLC_Code")))
                Next
                txtVLC.arrValueMember = arr
            End If
        End If
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs)
        LoadData(3)
    End Sub

    Private Sub btnPrintMccDetails_Click(sender As Object, e As EventArgs)
        ' Ticket No : BHA/15/08/18-000428 By Prabhakar 
        LoadData(4)
    End Sub

    Private Sub Excel_Click(sender As Object, e As EventArgs) Handles Excel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMCCBillGenrationStatus & "'"))


            'If rbtnMCCRouteVLCCSelect.IsChecked Then
            'Dim arr As List(Of String)

            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember) + " "))
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember) + " "))
                End If
                If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(txtVLC.arrDispalyMember) + " "))
                End If


            'End If


            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            clsCommon.MyExportToExcelGrid("MCC Bill Genration Status", gv, arrHeader, Me.Text)
            common.clsCommon.MyMessageBoxShow(Me, "Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMCCBillGenrationStatus & "'"))

            'Dim arr As List(Of String)

            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember) + " "))
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember) + " "))
                End If
                If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(txtVLC.arrDispalyMember) + " "))
                End If


            transportSql.applyExportTemplate(gv, PageSetupReport_ID)

            clsCommon.MyExportToPDF("MCC Bill Genration Status", gv, arrHeader, "MCC Bill Genration Status", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        SetToDate()
    End Sub

    Private Sub chkBillPaymentStatus_CheckedChanged(sender As Object, e As EventArgs) Handles chkBillPaymentStatus.CheckedChanged
        Try
            If chkBillPaymentStatus.Checked = True Then
                fndPlant.Visible = True
                txtMccForStatus.Visible = True
                MyLabel2.Visible = True
                fndMCC.Visible = True
            Else
                fndPlant.Visible = False
                txtMccForStatus.Visible = False
                MyLabel2.Visible = False
                fndMCC.Visible = False
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub fndPlant__My_Click(sender As Object, e As EventArgs) Handles fndPlant._My_Click
        Dim qry As String = "select distinct Loc_Segment_Code as PlantCode, TSPL_GL_SEGMENT_CODE.description as [PlantName] from TSPL_Location_MASTER inner join TSPL_GL_SEGMENT_CODE on TSPL_Location_MASTER.Loc_Segment_Code = TSPL_GL_SEGMENT_CODE.Segment_code  and TSPL_Location_MASTER.Rejected_Type = 'N' and TSPL_Location_MASTER.Location_Category='MCC' "
        fndPlant.arrValueMember = clsCommon.ShowMultipleSelectForm("PlantCode@MCCBillStatus", qry, "PlantCode", "PlantName", fndPlant.arrValueMember, fndPlant.arrDispalyMember)
    End Sub

    Private Sub txtMccForStatus__My_Click(sender As Object, e As EventArgs) Handles txtMccForStatus._My_Click


        If fndPlant.arrValueMember IsNot Nothing AndAlso fndPlant.arrValueMember.Count > 0 Then
        Else

            clsCommon.MyMessageBoxShow(Me, "Please first select Plant code.", Me.Text)
            Return
        End If
        Dim qry As String = "select distinct TSPL_Location_MASTER.Location_Code as Code, TSPL_Location_MASTER.Location_Desc as Name,Loc_Segment_Code as PlantCode, TSPL_GL_SEGMENT_CODE.description as [PlantName] from TSPL_Location_MASTER inner join TSPL_GL_SEGMENT_CODE on TSPL_Location_MASTER.Loc_Segment_Code = TSPL_GL_SEGMENT_CODE.Segment_code  and TSPL_Location_MASTER.Rejected_Type = 'N' and TSPL_Location_MASTER.Location_Category='MCC' and Loc_Segment_Code in (" + clsCommon.GetMulcallString(fndPlant.arrValueMember) + ") "
        txtMccForStatus.arrValueMember = clsCommon.ShowMultipleSelectForm("MCCCode@MCCBillStatus", qry, "Code", "Name", txtMccForStatus.arrValueMember, txtMccForStatus.arrDispalyMember)
    End Sub
End Class
