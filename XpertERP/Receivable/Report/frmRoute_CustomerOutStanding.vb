'--Creted by---[Pankaj Kumar Chaudhary]---Against Ticket No--[BM00000000925]
'--Updation by---[Pankaj Kumar Chaudhary]---Against Ticket No--[BM00000001080]
'' Anubhooti(1-July-2014) Added Export Permission Against BM00000003016 ''''''''
Imports common
Imports System.Data.SqlClient

Public Class FrmRoute_CustomerOutStanding
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim NoOfDays As Integer = 0

    Private Sub FrmRoute_CustomerOutStanding_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpMonth.Value = clsCommon.GETSERVERDATE()
        chkRouteAll.IsChecked = True
        chkCustomerAll.IsChecked = True
        chkCustGrpAll.IsChecked = True
        chkLOcALL.IsChecked = True
        chkActive.Checked = True
        LoadRoute()
        LoadCustomer()
        LoadCustomerGroup()
        LoadLocationCode()

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmRoute_CustomerOutstanding)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '' Anubhooti(1-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
    End Sub

    Private Sub frmRptCustomerLedger_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            print()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Sub LoadRoute()
        Dim strquery As String = "Select Route_No as [Code], Route_Desc as [Description] from TSPL_ROUTE_MASTER"
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgRoute.ValueMember = "Code"
        cbgRoute.DisplayMember = "Description"
    End Sub

    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master"
        If chkActive.Checked Then
            strquery += " WHERE Status='N'"
        ElseIf chkInactive.Checked Then
            strquery += " WHERE Status='Y'"
        ElseIf chkAll.Checked Then
            strquery += ""
        End If
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub

    Sub LoadCustomerGroup()
        Dim strquery As String = "Select Cust_Group_Code as [Code], Cust_Group_Desc as [Description] from TSPL_CUSTOMER_GROUP_MASTER"
        cbgCustGrp.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustGrp.ValueMember = "Code"
        cbgCustGrp.DisplayMember = "Description"
    End Sub

    Private Sub LoadLocationCode()
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Sub print()
        Try
            If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Customer.")
            End If
            If chkCustGrpSelect.IsChecked AndAlso cbgCustGrp.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Customer Group.")
            End If
            If chkRouteSelect.IsChecked AndAlso cbgRoute.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Route.")
            End If
            If chkLOcSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Location")
            End If
            Dim strBillCollection As String = ""
            ' Dim StrCollection As String
            Dim strBillsTotal As String = ""
            Dim strCollectionTotal As String = ""
            Dim FromDate As String = clsCommon.GetPrintDate("01/" + dtpMonth.Value.Month.ToString() + "/" + dtpMonth.Value.Year.ToString + "", "dd/MMM/yyyy")
            Dim ToDate As String = clsCommon.GetPrintDate(dtpMonth.Value.Date.AddDays(-(dtpMonth.Value.Day - 1)).AddMonths(1).AddDays(-1), "dd/MMM/yyyy")
            Dim RunDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
            NoOfDays = System.DateTime.DaysInMonth(dtpMonth.Value.Year, dtpMonth.Value.Month)
            Dim CurrentMonth As String = clsCommon.myCstr(dtpMonth.Value.Month)

            For ii As Integer = 1 To NoOfDays
                Dim strNoOfDays As String = clsCommon.myCstr(ii)
                If ii > 1 Then
                    strBillCollection += ", SUM(Case When (DATEPART(D,DocDate)=" + strNoOfDays + " AND DATEPART(MM,DocDate)=" + CurrentMonth + ") Then Bill Else 0 End) as B" + strNoOfDays + ", SUM(Case When (DATEPART(D,DocDate)=" + strNoOfDays + " AND DATEPART(MM,DocDate)=" + CurrentMonth + ") Then Collection Else 0 End) as C" + strNoOfDays + ""
                    strBillsTotal += "+B" + strNoOfDays + ""
                    strCollectionTotal += "+C" + strNoOfDays + ""
                Else
                    strBillCollection += "SUM(Case When (DATEPART(D,DocDate)=" + strNoOfDays + " AND DATEPART(MM,DocDate)=" + CurrentMonth + ") Then Bill Else 0 End) as B" + strNoOfDays + ", SUM(Case When (DATEPART(D,DocDate)=" + strNoOfDays + " AND DATEPART(MM,DocDate)=" + CurrentMonth + ") Then Collection Else 0 End) as C" + strNoOfDays + ""
                    strBillsTotal += "B" + strNoOfDays + ""
                    strCollectionTotal += "C" + strNoOfDays + ""
                End If
            Next

            Dim BaseQry As String = " select TSPL_SALE_INVOICE_HEAD.Cust_Code as ACode ,(cust_name) as AName, Sale_Invoice_No as DocNo, Sale_Invoice_Date  as DocDate, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location, Empty_Value as EmptyAmt, Total_Invoice_Amt as Bill, 0 as [Collection] from TSPL_SALE_INVOICE_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SALE_INVOICE_HEAD.Location  WHERE  TSPL_SALE_INVOICE_HEAD.Is_Post='y'  " '' Fetched Cust_Code With TSPL_SALE_INVOICE_HEAD
            BaseQry += " UNION ALL"
            BaseQry += " Select Cust_Code as ACode,Customer_Name as AName,Receipt_No as DocNo,Receipt_Date as DocDate, RIGHT(TSPL_RECEIPT_HEADER.Dr_Account,3) as [Location], 0 As EmptyAmt, 0 as Bill, case when Receipt_Type='F' Then Receipt_Amount*-1 Else Receipt_Amount End as [Collection] from TSPL_RECEIPT_HEADER   where  TSPL_RECEIPT_HEADER.Posted='Y' and TSPL_RECEIPT_HEADER.Receipt_Type not in ('M')   "
            BaseQry += " UNION ALL"
            BaseQry += " select TSPL_Customer_Invoice_Head.Customer_Code ,TSPL_Customer_Invoice_Head.Customer_Name ,case when len(TSPL_Customer_Invoice_Head.AgainstScrap)>0 then  TSPL_Customer_Invoice_Head.AgainstScrap else  TSPL_Customer_Invoice_Head.Document_No  end DocNo, CONVERT(Date,TSPL_Customer_Invoice_Head.Document_Date,103) as DocDate ,TSPL_Customer_Invoice_Head.Loc_Code, 0 as EmptyAmt, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then 0 Else TSPL_Customer_Invoice_Head.Document_Total end As Bill, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total else 0 end As [Collection] from   TSPL_Customer_Invoice_Head where TSPL_Customer_Invoice_Head.Status=1 "
            BaseQry += " UNION ALL"
            BaseQry += " select TSPL_SALE_RETURN_HEAD.Cust_Code as ACode ,(cust_name) as AName, Sale_Return_No  as DocNo, CONVERT(DATE,Sale_Return_Date,103) as DocDate, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location ,Empty_Value as EmptyAmt, Total_Invoice_Amt*-1 as Bill, 0 as [Collection] from TSPL_SALE_RETURN_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SALE_RETURN_HEAD.Location  where TSPL_SALE_RETURN_HEAD.Is_Post='Y'" '' Fetched Cust_Code With TSPL_SALE_RETURN_HEAD
            BaseQry += " UNION ALL"
            BaseQry += " select TSPL_SALE_RETURN_INTER_HEAD.Cust_Code as ACode ,(TSPL_SALE_RETURN_INTER_HEAD.cust_name) as AName, TSPL_SALE_RETURN_INTER_HEAD.Document_No  as DocNo, CONVERT(DATE,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)  as DocDate, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location, TSPL_SALE_RETURN_INTER_HEAD.Empty_Value as  EmptyAmt, TSPL_SALE_RETURN_INTER_HEAD.Total_Order_Amt*-1 as Bill, 0 as [Collection] from  TSPL_SALE_RETURN_INTER_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SALE_RETURN_INTER_HEAD.Location  where TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1  "
            BaseQry += " UNION ALL"
            BaseQry += " select TSPL_Receipt_Adjustment_Header.Customer_No as ACode,TSPL_CUSTOMER_MASTER.Customer_Name as AName,Adjustment_No as DocNo, CONVERT(DATE,Adjustment_Date,103) as DocDate, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location ,0 as EmptyAmt, 0 as Bill, TSPL_Receipt_Adjustment_Header.Adjustment_Amount as [Collection] from TSPL_Receipt_Adjustment_Header left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Receipt_Adjustment_Header.Customer_No left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_Receipt_Adjustment_Header.Doc_No left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SALE_INVOICE_HEAD.Location where TSPL_Receipt_Adjustment_Header.Is_Post='Y' "
            BaseQry += " UNION ALL"
            BaseQry += " select TSPL_BANK_REVERSE.Cust_Code as ACode, TSPL_BANK_REVERSE.Cust_Name as AName,TSPL_BANK_REVERSE.Reverse_Code as DocNo ,TSPL_BANK_REVERSE.Reversal_Date as DocDate, Right( TSPL_BANK_MASTER.BANKACC, 3) as [Location], 0 as EmptyAmt, 0 as Bill, TSPL_BANK_REVERSE.Amount*-1 as [Collection] from TSPL_BANK_REVERSE  left outer join TSPL_BANK_MASTER on TSPL_BANK_REVERSE .Bank_Code =TSPL_BANK_MASTER.BANK_CODE     where TSPL_BANK_REVERSE.Source_Type='AR' and TSPL_BANK_REVERSE.post='P'   "
            BaseQry += " UNION ALL"
            BaseQry += " select TSPL_VCGL_Head.VC_Code as ACode,TSPL_VCGL_Head.VC_Name as AName,TSPL_VCGL_Head.Document_No as DocNo, CONVERT(DATE,TSPL_VCGL_Head.Document_Date,103) as DocDate, TSPL_VCGL_Head.Location_Segment as Location, Case When TSPL_VCGL_Head.Is_Empty=1 Then TSPL_VCGL_Head.Amount Else 0 End as EmptyValue, Case When TSPL_VCGL_Head.Is_Empty<>1 Then (Case When TSPL_VCGL_Head.Amount_Type='Cr' then TSPL_VCGL_Head.Amount else 0 End) Else 0 End as Bill, Case When TSPL_VCGL_Head.Is_Empty<>1 Then (Case When TSPL_VCGL_Head.Amount_Type='Dr' then TSPL_VCGL_Head.Amount else 0 End) Else 0 End as [Collection] from  TSPL_VCGL_Head  where TSPL_VCGL_Head.Document_Type='C' and TSPL_VCGL_Head.Status=1 "
            BaseQry += " UNION ALL"
            BaseQry += " select TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_VCGL_Detail.VCGL_Name as AName,TSPL_VCGL_Head.Document_No as DocNo, CONVERT(DATE,TSPL_VCGL_Head.Document_Date,103) as DocDate, TSPL_VCGL_Head.Location_Segment as Location , 0 as EmptyAmt, TSPL_VCGL_Detail.Dr_Amount as Bill, TSPL_VCGL_Detail.Cr_Amount as [Collection] from TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head .Document_No=TSPL_VCGL_Detail.Document_No where  TSPL_VCGL_Head.Status=1 and TSPL_VCGL_Detail.Row_Type='Customer' "

            Dim Qry As String = " select TSPL_ROUTE_MASTER.Route_No, MAX(TSPL_ROUTE_MASTER.Route_Desc) As Route_Desc, ACode, MAX(AName) As AName, "
            Qry += " SUM(Case When DocDate<'" + FromDate + "' Then Bill-Collection Else 0 End) as OpeningBal, "
            Qry += " " + strBillCollection + " FROM ( " + BaseQry + ""
            Qry += " ) XXX left outer join TSPL_CUSTOMER_MASTER on ACode=TSPL_CUSTOMER_MASTER.Cust_Code"
            Qry += " LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No=TSPL_CUSTOMER_MASTER.Route_No "
            Qry += " Where DocDate<='" + ToDate + "' And LEN(ACode)>0 "

            If chkRouteSelect.IsChecked And cbgRoute.CheckedValue.Count > 0 Then
                Qry += " AND TSPL_ROUTE_MASTER.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"
            End If
            If chkCustGrpSelect.IsChecked And cbgCustGrp.CheckedValue.Count > 0 Then
                Qry += " AND TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustGrp.CheckedValue) + ")"
            End If
            If chkCustomerSelect.IsChecked And cbgCustomer.CheckedValue.Count > 0 Then
                Qry += " AND ACode in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
            End If
            If chkLOcSelect.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
                Qry += " AND Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
            End If
            If chkActive.Checked Then
                Qry += " AND TSPL_CUSTOMER_MASTER.Status='N'"
            ElseIf chkInactive.Checked Then
                Qry += " AND TSPL_CUSTOMER_MASTER.Status='Y'"
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                Qry += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ")" + Environment.NewLine
            End If
            Qry += " Group By TSPL_ROUTE_MASTER.Route_No, ACode "

            '--Here we calculate TOtal Bills And Total Collections
            Qry = "Select XXX1.*, (" + strBillsTotal + ") AS Bills, (" + strCollectionTotal + ") As Collections From ( " + Qry + " ) XXX1"

            '--Here we calculate Balance, OSInLacks, OverDue------
            Qry = "Select FINAL.*, (openingBal+Bills-Collections) As Balance, CONVERT(Decimal(18,2),(openingBal+Bills-Collections)/100000) As [OSinLacks], (openingBal-Collections) AS [OverDue] FROM ( " + Qry + " ) FINAL"

            Qry += " Order By Route_No, ACode "

            Dim dtMain As DataTable = clsDBFuncationality.GetDataTable(Qry)

            gvReport.MasterTemplate.SummaryRowsBottom.Clear()
            gvReport.DataSource = Nothing
            gvReport.Rows.Clear()
            gvReport.Columns.Clear()
            If dtMain.Rows.Count <= 0 Then
                Throw New Exception("No data found.")
            End If
            gvReport.DataSource = dtMain

            FormatGrid()

            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub FormatGrid()
        gvReport.AllowAddNewRow = False
        gvReport.TableElement.TableHeaderHeight = 40
        gvReport.MasterTemplate.ShowRowHeaderColumn = False

        gvReport.Columns("Route_No").IsVisible = True
        gvReport.Columns("Route_No").Width = 101
        gvReport.Columns("Route_No").HeaderText = "Route No"

        gvReport.Columns("Route_Desc").IsVisible = True
        gvReport.Columns("Route_Desc").Width = 221
        gvReport.Columns("Route_Desc").HeaderText = "Route Desc"

        gvReport.Columns("ACode").IsVisible = True
        gvReport.Columns("ACode").Width = 101
        gvReport.Columns("ACode").HeaderText = "Customer Code"

        gvReport.Columns("AName").IsVisible = True
        gvReport.Columns("AName").Width = 221
        gvReport.Columns("AName").HeaderText = "Customer Name"

        gvReport.Columns("OpeningBal").IsVisible = True
        gvReport.Columns("OpeningBal").Width = 150
        gvReport.Columns("OpeningBal").HeaderText = "Opening Balance"

        For ii As Integer = 1 To NoOfDays
            gvReport.Columns("B" + ii.ToString() + "").IsVisible = True
            gvReport.Columns("B" + ii.ToString() + "").Width = 81
            gvReport.Columns("B" + ii.ToString() + "").HeaderText = "Bill(" + ii.ToString() + ")"

            gvReport.Columns("C" + ii.ToString() + "").IsVisible = True
            gvReport.Columns("C" + ii.ToString() + "").Width = 81
            gvReport.Columns("C" + ii.ToString() + "").HeaderText = "Collection(" + ii.ToString() + ")"
        Next

        gvReport.Columns("Bills").IsVisible = True
        gvReport.Columns("Bills").Width = 150
        gvReport.Columns("Bills").HeaderText = "Total Bills"

        gvReport.Columns("Collections").IsVisible = True
        gvReport.Columns("Collections").Width = 150
        gvReport.Columns("Collections").HeaderText = "Total Collections"

        gvReport.Columns("Balance").IsVisible = True
        gvReport.Columns("Balance").Width = 150
        gvReport.Columns("Balance").HeaderText = "Balance"

        gvReport.Columns("OSinLacks").IsVisible = True
        gvReport.Columns("OSinLacks").Width = 100
        gvReport.Columns("OSinLacks").HeaderText = "OS In Lacks"

        gvReport.Columns("OverDue").IsVisible = True
        gvReport.Columns("OverDue").Width = 150
        gvReport.Columns("OverDue").HeaderText = "Over Due"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim OBal As New GridViewSummaryItem("OpeningBal", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(OBal)
        'Dim B1 As New GridViewSummaryItem("B1", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(B1)
        'Dim C1 As New GridViewSummaryItem("C1", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(C1)
        'Dim B2 As New GridViewSummaryItem("B2", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(B2)
        'Dim C2 As New GridViewSummaryItem("C2", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(C2)
        'Dim B3 As New GridViewSummaryItem("B3", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(B3)
        'Dim C3 As New GridViewSummaryItem("C3", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(C3)
        Dim Bills As New GridViewSummaryItem("Bills", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Bills)
        Dim Collections As New GridViewSummaryItem("Collections", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Collections)
        Dim Balance As New GridViewSummaryItem("Balance", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Balance)
        Dim OverDue As New GridViewSummaryItem("OverDue", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(OverDue)
        gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        dtpMonth.Value = clsCommon.GETSERVERDATE()
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        TxtMultiCustomerCategory.arrValueMember = Nothing
    End Sub

    Private Sub refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles refreshbtn.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gvReport
            print()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged
        LoadCustomer()
    End Sub

    Private Sub chkInactive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInactive.CheckedChanged
        LoadCustomer()
    End Sub

    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        LoadCustomer()
    End Sub

    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub

    Private Sub chkLOcALL_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLOcALL.ToggleStateChanged
        cbgLocation.Enabled = Not chkLOcALL.IsChecked
    End Sub

    Private Sub chkCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
    End Sub

    Private Sub chkCustGrpAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustGrpAll.ToggleStateChanged
        cbgCustGrp.Enabled = Not chkCustGrpAll.IsChecked
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Month : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "MMM/yyyy") + "")
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmRoute_CustomerOutstanding & "'"))

            If chkRouteSelect.IsChecked Then
                Dim stLocationName As String = ""
                For Each StrName As String In cbgRoute.CheckedDisplayMember
                    If clsCommon.myLen(stLocationName) > 0 Then
                        stLocationName += ", "
                    End If
                    stLocationName += StrName
                Next
                Dim strLocationCode As String = ""
                For Each StrCode As String In cbgRoute.CheckedValue
                    If clsCommon.myLen(strLocationCode) > 0 Then
                        strLocationCode += ", "
                    End If
                    strLocationCode += StrCode
                Next
                arrHeader.Add(("Route: " + stLocationName + " "))
            End If
            If chkLOcSelect.IsChecked Then
                Dim stLocationName As String = ""
                For Each StrName As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(stLocationName) > 0 Then
                        stLocationName += ", "
                    End If
                    stLocationName += StrName
                Next
                Dim strLocationCode As String = ""
                For Each StrCode As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strLocationCode) > 0 Then
                        strLocationCode += ", "
                    End If
                    strLocationCode += StrCode
                Next
                arrHeader.Add(("Location: " + stLocationName + " "))
            End If
            If chkCustomerSelect.IsChecked Then
                Dim stLocationName As String = ""
                For Each StrName As String In cbgCustomer.CheckedDisplayMember
                    If clsCommon.myLen(stLocationName) > 0 Then
                        stLocationName += ", "
                    End If
                    stLocationName += StrName
                Next
                Dim strLocationCode As String = ""
                For Each StrCode As String In cbgCustomer.CheckedValue
                    If clsCommon.myLen(strLocationCode) > 0 Then
                        strLocationCode += ", "
                    End If
                    strLocationCode += StrCode
                Next
                arrHeader.Add(("Customer: " + stLocationName + " "))
            End If
            If chkCustGrpSelect.IsChecked Then
                Dim stLocationName As String = ""
                For Each StrName As String In cbgCustGrp.CheckedDisplayMember
                    If clsCommon.myLen(stLocationName) > 0 Then
                        stLocationName += ", "
                    End If
                    stLocationName += StrName
                Next
                Dim strLocationCode As String = ""
                For Each StrCode As String In cbgCustGrp.CheckedValue
                    If clsCommon.myLen(strLocationCode) > 0 Then
                        strLocationCode += ", "
                    End If
                    strLocationCode += StrCode
                Next
                arrHeader.Add(("Customer Group: " + stLocationName + " "))
            End If

            'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            '    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            'End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                arrHeader.Add(("Customer Category : " + clsCommon.GetMulcallStringWithComma(TxtMultiCustomerCategory.arrValueMember) + " "))
            End If

            If exporter = EnumExportTo.Excel Then
                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(gvReport, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gvReport, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gvReport, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gvReport, PageSetupReport_ID)
                clsCommon.MyExportToPDF("ROUTE And CUSTOMERS WISE OUTSTANDING REPORT (CASH)", gvReport, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rExcel_Click(sender As Object, e As EventArgs) Handles rExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rPDF_Click(sender As Object, e As EventArgs) Handles rPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub TxtMultiCustomerCategory__My_Click(sender As Object, e As EventArgs) Handles TxtMultiCustomerCategory._My_Click
        Dim qry As String = " select cust_category_code as [Code], CUST_CATEGORY_DESC as [Desc] from TSPL_CUSTOMER_CATEGORY_MASTER "
        TxtMultiCustomerCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("CustCatMSel1", qry, "Code", "Desc", TxtMultiCustomerCategory.arrValueMember, TxtMultiCustomerCategory.arrDispalyMember)
    End Sub
End Class
