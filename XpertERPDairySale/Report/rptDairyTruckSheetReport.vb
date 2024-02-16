Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common

Imports System.IO

Public Class rptDairyTruckSheetReport
    Inherits FrmMainTranScreen
    Dim strQry As String = ""
    Dim IsFormLoad As Boolean = False
    Public PrintTruckSheetAfterGenerate As Boolean = False
    Public isTruckSheetPriview As Boolean = False
    Dim trnsLstCustomer As New List(Of String)
    Dim trnsLst As New List(Of String)
    Dim countPostedDoc As Integer = 0
    Dim DtError As DataTable
    Dim settTCSRateforCustomerWithoutPanNo As Decimal = 0
    Dim settTCSRateforCustomerWithPanNo As Decimal = 0
    Dim ApplyTCSAmtOnAbstractReportDotMatrix As Integer = 0
    Dim ApplyIncludeTCSAmountInRouteTotalOnTruckSheet As Boolean = False
    Dim ShowEarlyRouteOnTruckSheet As Boolean = False
    Dim StrEarlyRoute As String = ""
    Dim StrNonEarlyRoute As String = ""
    'Dim RecordCount As Integer = 0
    'Dim strDocNo As String = Nothing
    'Dim strCustomerCode As String = Nothing
    '===========update by preeti gupta against ticket no[ERO/10/05/18-000304][changes only in print format],ERO/19/06/18-000349,ERO/13/03/19-000512,ERO/08/05/19-000591,ERO/08/05/19-000590,ERO/08/05/19-000589,ERO/03/06/19-000628,ERO/25/06/19-000655,ERO/25/07/19-000964,ERO/26/07/19-000965,ERO/29/07/19-000967,ERO/29/07/19-000968,
    Private Sub rptDairyTruckSheetReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PrintTruckSheetAfterGenerate = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PrintTruckSheetAfterGenerate, clsFixedParameterCode.PrintTruckSheetAfterGenerate, Nothing)) = 1, True, False)
        ShowEarlyRouteOnTruckSheet = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowEarlyRouteOnTruckSheet, clsFixedParameterCode.ShowEarlyRouteOnTruckSheet, Nothing)) = 1, True, False)
        settTCSRateforCustomerWithoutPanNo = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
        settTCSRateforCustomerWithPanNo = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
        ApplyTCSAmtOnAbstractReportDotMatrix = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyTCSAmtOnAbstractReportDotMatrix, clsFixedParameterCode.ApplyTCSAmtOnAbstractReportDotMatrix, Nothing))
        ApplyIncludeTCSAmountInRouteTotalOnTruckSheet = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyIncludeTCSAmountInRouteTotalOnTruckSheet, clsFixedParameterCode.ApplyIncludeTCSAmountInRouteTotalOnTruckSheet, Nothing)) = "1", True, False))
        RadGroupBox1.Visible = True
        IsFormLoad = False
        fromDate.Value = clsCommon.GetDateWithStartTime(clsCommon.GETSERVERDATE)
        'ToDate.Value = clsCommon.GETSERVERDATE(obj.Document_Date, "dd/MMM/yyyy hh:mm tt")
        ToDate.Value = clsCommon.GetDateWithEndTime(fromDate.Value)
        TSP_Date.Value = clsCommon.GETSERVERDATE()
        txtInvFromDate.Value = clsCommon.GETSERVERDATE()
        txtInvToDate.Value = clsCommon.GETSERVERDATE()
        dtpIDSfromdate.Value = clsCommon.GetDateWithStartTime(clsCommon.GETSERVERDATE)
        dtpIDStodate.Value = clsCommon.GetDateWithStartTime(clsCommon.GETSERVERDATE)
        cmbCustomerCategory.Text = "Select"
        txtABSDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") ' 
        Reset()
        IsFormLoad = True
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal Then
            btnDotMatrixPrinter.Visible = True
        Else
            btnDotMatrixPrinter.Visible = False
        End If
        LoadABSCombo()
        cboABSReportType.SelectedValue = "Milk Abstract"
        If PrintTruckSheetAfterGenerate = True Then
            btn_CancelTruckSheet.Visible = True
            btn_TruckSheetGenerated.Visible = True
            btn_PrintGatePass.Visible = True
            btn_DistributorTS.Visible = True
        Else
            btn_CancelTruckSheet.Visible = False
            btn_TruckSheetGenerated.Visible = False
            btn_PrintGatePass.Visible = False
            btn_DistributorTS.Visible = False
        End If
        '===================== Net Sale Report =====================
        LoadNSRCombo()
        cboNSRType.SelectedValue = "Daywise Net Sales"
        dtpNSRToDate.Visible = False
        MyLabel13.Visible = False
        MyLabel17.Text = "Date"

        dtpNSRFromDate.Value = clsCommon.GetDateWithStartTime(clsCommon.GETSERVERDATE)
        dtpNSRToDate.Value = clsCommon.GetDateWithStartTime(clsCommon.GETSERVERDATE)
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal Then
            gbNetSalesReport.Visible = True
            gbInstitutionDailySalesReport.Visible = True
        Else
            gbNetSalesReport.Visible = False
            gbInstitutionDailySalesReport.Visible = False
        End If
        If ShowEarlyRouteOnTruckSheet = True Then
            chkShowEarlyRoute.Visible = True
        Else
            chkShowEarlyRoute.Visible = False
        End If
        StrEarlyRoute = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @temp VARCHAR(MAX) SET @temp =(SELECT ',''' + cast(Route_No as varchar) + '''' FROM TSPL_ROUTE_MASTER where IsEarlyRoute=1 order by Route_No FOR XML PATH('')) select SUBSTRING(@temp, 2, 200000) AS Route"))
        StrNonEarlyRoute = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @temp VARCHAR(MAX) SET @temp =(SELECT ',''' + cast(Route_No as varchar) + '''' FROM TSPL_ROUTE_MASTER where IsEarlyRoute=0 order by Route_No FOR XML PATH('')) select SUBSTRING(@temp, 2, 200000) AS Route"))
        '===========================================================
    End Sub

    Sub EnableDisableControl()

    End Sub
    Sub Reset()
        'ToDate.Value = clsCommon.GETSERVERDATE()
        'fromDate.Value = clsCommon.GETSERVERDATE() 'ToDate.Value.AddMonths(-1)

        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.DataSource = Nothing
        btnGo.Enabled = True
        btnReset.Enabled = False
        RadSplitButton1.Enabled = False
        EnableDisableControl()
        txtMultCustomer.arrValueMember = Nothing
        txtMultLocation.arrValueMember = Nothing
        txtRoute.arrValueMember = Nothing
    End Sub


    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        loaddata()

    End Sub
    Sub FormatGrid()
        ' Dim strItemCode, head2 As String
        Dim summaryItem As New GridViewSummaryItem()
        Gv1.TableElement.TableHeaderHeight = 150
        Gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            Gv1.Columns(ii).FormatString = "{0:n2}"
            Gv1.Columns(ii).WrapText = True
        Next


        Dim summaryRowItem As New GridViewSummaryRowItem()
        If Gv1.Rows.Count > 0 Then

            For i As Integer = 6 To Gv1.Columns.Count - 1
                Dim aa = Gv1.Columns(i).HeaderText()
                Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Gv1.Columns(i).WrapText = True
            Next
        End If


        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub
    Sub FormatGridUDL()
        ' Dim strItemCode, head2 As String
        Dim summaryItem As New GridViewSummaryItem()
        Gv1.TableElement.TableHeaderHeight = 150
        Gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            Gv1.Columns(ii).WrapText = True
        Next

        Gv1.Columns("Invoice Date").FormatString = "{0:dd/MM/yyyy}"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        If Gv1.Rows.Count > 0 Then

            For i As Integer = 6 To Gv1.Columns.Count - 1
                Gv1.Columns(i).FormatString = "{0:n2}"
                Dim aa = Gv1.Columns(i).HeaderText()
                Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Gv1.Columns(i).WrapText = True
            Next
        End If


        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub


    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try

            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Truck Sheet Report")

                Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
                arrHeader.Add(strtemp)

                If txtMultCustomer.arrValueMember IsNot Nothing AndAlso txtMultCustomer.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtMultCustomer.arrDispalyMember))
                End If

                If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtMultLocation.arrDispalyMember))
                End If

                clsCommon.MyExportToExcelGrid("Truck Sheet Report", Gv1, arrHeader, "Truck Sheet Report")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    'Sub Loaddata()
    '    Try
    '        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    '        Dim strshortDesp As String = String.Empty
    '        Dim strtotalShort As String = String.Empty
    '        Dim strtotalShortTotal As String = String.Empty
    '        Dim strCustCodeString As String = ""
    '        Dim status As Integer = 1
    '        If RadioButton1.Checked = True Then
    '            status = 1
    '        End If
    '        If RadioButton2.Checked = True Then
    '            status = 0
    '        End If

    '        Dim qry As String = ""
    '        Dim dt As DataTable = Nothing
    '        Dim CNFDistributor As String = " and 2=2 "
    '        qry = " select Item_Desc from tspl_item_master "
    '        'qry = " select  case when len(tspl_item_master.Short_Description) <=0 then tspl_item_master.Item_Desc else tspl_item_master.Short_Description end  as Item_Code  from tspl_item_master "
    '        dt = clsDBFuncationality.GetDataTable(qry)


    '        For Each dr As DataRow In dt.Rows
    '            strshortDesp = clsCommon.myCstr(dr("Item_Desc"))
    '            If clsCommon.myLen(strshortDesp) > 0 Then
    '                strtotalShort = strtotalShort + "," + "[" + strshortDesp + "]"
    '                strCustCodeString = strCustCodeString + "," + "  isnull([" & strshortDesp & "],0)  " & "as  " & "[" & strshortDesp & "]" & ""
    '                strtotalShortTotal = strtotalShortTotal + "+" + "  isnull([" & strshortDesp & "],0)  " & "  "
    '            End If
    '        Next
    '        If strtotalShort.Substring(0, 1) = "," Then
    '            strtotalShort = strtotalShort.Substring(1, strtotalShort.Length - 1)
    '            strCustCodeString = strCustCodeString.Substring(1, strCustCodeString.Length - 1)

    '        End If


    '        ' remove + 
    '        strtotalShortTotal = strtotalShortTotal.Remove(0, 1)





    '        If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
    '            CNFDistributor += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ") "
    '        End If

    '        If TxtMultiSelectFinder1.arrValueMember IsNot Nothing AndAlso TxtMultiSelectFinder1.arrValueMember.Count > 0 Then
    '            CNFDistributor += " and TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code  in (" + clsCommon.GetMulcallString(TxtMultiSelectFinder1.arrValueMember) + ") "
    '        End If

    '        If TxtMultiSelectFinder2.arrValueMember IsNot Nothing AndAlso TxtMultiSelectFinder2.arrValueMember.Count > 0 Then
    '            CNFDistributor += " and TSPL_BOOKING_DETAIL.Loc_Code  in (" + clsCommon.GetMulcallString(TxtMultiSelectFinder2.arrValueMember) + ") "
    '        End If

    '        qry = " select CNF_Code,CNF_Name,Distributor_Code,Loc_Code,Distributor_Name,Location_Desc, " & strCustCodeString & " , (" & strtotalShortTotal & ") as Total from (  " & _
    '              " select TSPL_BOOKING_DETAIL.Cust_Code as CNF_Code,max(TSPL_CUSTOMER_MASTER.Customer_Name) as CNF_Name, TSPL_BOOKING_MATSER.Distributor_Code  , max(TSPL_SECONDARY_CUSTOMER_MASTER.Customer_Name) as Distributor_Name , max( TSPL_ITEM_MASTER.Item_Desc) as Item_Desc  ,TSPL_BOOKING_DETAIL.Loc_Code,max (TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc, sum (TSPL_BOOKING_DETAIL.Booking_Qty ) as Qty  from TSPL_BOOKING_DETAIL LEFT OUTER JOIN TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No " & _
    '              " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code  =TSPL_BOOKING_DETAIL.Loc_Code " & _
    '              " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code " & _
    '              " left outer join TSPL_SECONDARY_CUSTOMER_MASTER on TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_MATSER.Distributor_Code " & _
    '              " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code " & _
    '              " where TSPL_BOOKING_MATSER.Posted =" & status & " and TSPL_BOOKING_DETAIL.Unit_code= 'CRATE' and convert(date,TSPL_BOOKING_MATSER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103)   and  convert(date,TSPL_BOOKING_MATSER.Document_Date,103)<= convert(date,'" + ToDate.Value + "',103)  " + CNFDistributor + "  group by TSPL_BOOKING_DETAIL.Cust_Code ,TSPL_BOOKING_MATSER.Distributor_Code,TSPL_BOOKING_DETAIL.Item_Code,Loc_Code )  aaa " & _
    '              " pivot  (sum (aaa.Qty) for Item_Desc in (" & strtotalShort & ")) as ItemName "


    '        Gv1.DataSource = Nothing
    '        Gv1.Rows.Clear()
    '        Gv1.Columns.Clear()
    '        Gv1.DataSource = clsDBFuncationality.GetDataTable(qry)


    '        FormatGrid()
    '        'Gv1.BestFitColumns()
    '        Gv1.Columns(0).IsPinned = True
    '        Gv1.Columns(1).IsPinned = True
    '        Gv1.Columns(2).IsPinned = True
    '        Gv1.Columns(3).IsPinned = True
    '        Gv1.Columns(4).IsPinned = True
    '        Gv1.Columns(5).IsPinned = True


    '        RadPageView1.SelectedPage = RadPageViewPage2
    '        btnGo.Enabled = False
    '        btnReset.Enabled = True
    '        RadSplitButton1.Enabled = True
    '        Gv1.BestFitColumns()
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try

    'End Sub
    Sub loaddata()
        LoaddataDairySaleInvoice()
    End Sub



    Sub LoaddataDairySaleInvoice()
        Try
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim strshortDesp As String = String.Empty
            Dim strItemCode As String = String.Empty
            Dim strtotalShort As String = String.Empty
            Dim strtotalShortTotal As String = String.Empty
            Dim strCustCodeString As String = ""
            Dim Scheme As String = Nothing
            Dim status As Integer = 1


            Dim dtqry As DataTable = Nothing
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim WhrCls As String = " and 2=2 "
            qry = " select  distinct TSPL_ITEM_MASTER.Item_Code,tspl_item_master.short_Description as Item_Desc   from TSPL_SD_SALE_INVOICE_DETAIL " &
                 " left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code =TSPL_SD_SALE_INVOICE_DETAIL.document_code " &
                 " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " &
                " where 2=2 and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >=convert(date,'" & fromDate.Value & "',103)   and  convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,'" & ToDate.Value & "',103)"
            'qry = " select  case when len(tspl_item_master.Short_Description) <=0 then tspl_item_master.Item_Desc else tspl_item_master.Short_Description end  as Item_Code  from tspl_item_master "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            For Each dr As DataRow In dt.Rows
                strshortDesp = clsCommon.myCstr(dr("Item_Desc"))
                strItemCode = clsCommon.myCstr(dr("Item_Code"))
                If clsCommon.myLen(strshortDesp) > 0 Then
                    strtotalShort = strtotalShort + "," + "[" + strItemCode + " - " + strshortDesp + "]"
                    strCustCodeString = strCustCodeString + "," + "  isnull([" & strItemCode + " - " + strshortDesp & "],0)  " & "as  " & "[" & strItemCode + " - " + strshortDesp & "]" & ""
                    strtotalShortTotal = strtotalShortTotal + "+" + "  isnull([" & strItemCode + " - " + strshortDesp & "],0)  " & "  "
                End If
            Next
            If strtotalShort.Substring(0, 1) = "," Then
                strtotalShort = strtotalShort.Substring(1, strtotalShort.Length - 1)
                strCustCodeString = strCustCodeString.Substring(1, strCustCodeString.Length - 1)

            End If


            ' remove + 
            strtotalShortTotal = strtotalShortTotal.Remove(0, 1)


            'WhrCls = " and TSPL_SD_SALE_INVOICE_DETAIL.scheme_item = 'Y' "



            If txtMultCustomer.arrValueMember IsNot Nothing AndAlso txtMultCustomer.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(txtMultCustomer.arrValueMember) + ") "
            End If

            If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_SD_SALE_INVOICE_HEAD.bill_to_location  in (" + clsCommon.GetMulcallString(txtMultLocation.arrValueMember) + ") "
            End If

            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                'select vehicle_code  from TSPL_ROUTE_MASTER  where Route_No = '572'
                WhrCls += " and TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  in ( select vehicle_code from TSPL_ROUTE_MASTER where Route_No  in  ( " + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")) "
            End If

            qry = "  select ROW_NUMBER() OVER (ORDER BY xxx.Priority_Level) as [Sno],xxx.* from ( select ItemName.Document_Code AS [Invoice No],convert(date,Document_Date,103) [Invoice Date],CNF_Code as [Cust Code],CNF_Name as [Cust Name],Route_Name as [Route Name], " & strCustCodeString & " , (" & strtotalShortTotal & ") as Total,isnull(aa.Scheme_Qty,0) as [Free Qty],Priority_Level from (  " &
                  " select TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Document_Date ,  TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  as CNF_Code,max(TSPL_CUSTOMER_MASTER.Customer_Name) as CNF_Name,Max(tspl_route_master.route_desc) as Route_Name,  max(TSPL_ITEM_MASTER.Item_Code  + ' - ' + TSPL_ITEM_MASTER.short_Description) as Item_Desc  ,TSPL_SD_SALE_INVOICE_HEAD.bill_to_location as Location_Code ,  sum (TSPL_SD_SALE_INVOICE_DETAIL.Qty  ) as Qty  " &
                             " ,(TSPL_CUSTOMER_MASTER.Priority_Level) as Priority_Level from TSPL_SD_SALE_INVOICE_DETAIL " &
                    " LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code = TSPL_SD_SALE_INVOICE_DETAIL.document_code " &
                    " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code  =TSPL_SD_SALE_INVOICE_HEAD.bill_to_location   " &
                     " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  " &
                    " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  " &
                     " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no " &
                    " left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code " &
                   " left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no " &
                    " where TSPL_SD_SALE_INVOICE_HEAD.status =" & status & " and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='N' and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and TSPL_SD_SALE_INVOICE_HEAD.Document_Date  >= '" & clsCommon.GetPrintDate((fromDate.Value), "dd/MMM/yyyy hh:mm tt") & "'   and  TSPL_SD_SALE_INVOICE_HEAD.Document_Date <= '" + clsCommon.GetPrintDate((ToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'   " + WhrCls + "   group by TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, TSPL_SD_SALE_INVOICE_HEAD.bill_to_location,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_CUSTOMER_MASTER.Priority_Level )  aaa " &
                    " pivot  (sum (aaa.Qty) for Item_Desc in (" & strtotalShort & ")) as ItemName " &
                   " left  join ( " &
                     " select isnull(sum(TSPL_SD_SALE_INVOICE_DETAIL.qty),0) as Scheme_Qty,TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " &
                     " from TSPL_SD_SALE_INVOICE_DETAIL " &
                     " where TSPL_SD_SALE_INVOICE_DETAIL.scheme_item = 'Y' " &
                     " group by TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " &
                     " )aa " &
                     " ON aa.DOCUMENT_CODE=ItemName.Document_Code )as xxx order by xxx.Priority_Level,xxx.[Cust Code] asc "


            dtqry = clsDBFuncationality.GetDataTable(qry)
            If dtqry Is Nothing OrElse dtqry.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtqry


            FormatGridUDL()
            'Gv1.BestFitColumns()
            Gv1.Columns(0).IsPinned = True
            Gv1.Columns(1).IsPinned = True
            Gv1.Columns(2).IsPinned = True
            Gv1.Columns(3).IsPinned = True
            Gv1.Columns(4).IsPinned = True
            Gv1.Columns(5).IsPinned = True
            'Gv1.Columns(5).IsPinned = True
            'Gv1.Columns(6).IsPinned = True
            'Gv1.Columns(7).IsPinned = True
            'Gv1.Columns(8).IsPinned = True
            'Gv1.Columns(9).IsPinned = True
            Gv1.Columns("Priority_Level").IsVisible = False
            RadPageView1.SelectedPage = RadPageViewPage2

            btnGo.Enabled = False
            btnReset.Enabled = True
            RadSplitButton1.Enabled = True
            Gv1.BestFitColumns()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub txtMultCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultCustomer._My_Click
        strQry = " select Cust_Code as [Code] , Customer_Name as [Name] from TSPL_CUSTOMER_MASTER "
        txtMultCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultCustomer", strQry, "Code", "Name", txtMultCustomer.arrValueMember, txtMultCustomer.arrDispalyMember)
    End Sub

    Private Sub txtMultLocation__My_Click(sender As Object, e As EventArgs) Handles txtMultLocation._My_Click
        strQry = "select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER"
        txtMultLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultLocation", strQry, "Code", "Name", txtMultLocation.arrValueMember, txtMultLocation.arrDispalyMember)
    End Sub

    Private Sub txtLorryNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLorryNo._MYValidating
        Dim wrcls As String = ""
        Dim qry As String = "Select Vehicle_Id,Description,model As Model,Vehicle_Type,Vehicle_Brand,Vehicle_Name ,Location  From TSPL_VEHICLE_MASTER"
        wrcls = " InOut='I'"
        'If clsCommon.myLen(txtRouteNo.Value) > 0 Then
        '    wrcls += " and Vehicle_Id  in ( select vehicle_code from TSPL_ROUTE_MASTER where Route_No ='" + txtRouteNo.Value + "')"
        'End If
        txtLorryNo.Value = clsCommon.ShowSelectForm("DSGatePassVehicle", qry, "Vehicle_Id", wrcls, txtLorryNo.Value, "Vehicle_Id", isButtonClicked)
        If clsCommon.myLen(txtLorryNo.Value) > 0 Then
            lblVehicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Description as Name FROM TSPL_VEHICLE_MASTER Where Vehicle_Id='" + txtLorryNo.Value + "'"))
        Else
            lblVehicleNo.Text = ""
        End If
    End Sub
    Public Shared Function GetPrintQryChange(ByVal Lorry As String, ByVal Route As String, ByVal Docdate As DateTime, ByVal DocTimeRange As String) As String
        Dim StrQry As String = ""
        '============Added by preeti Gupta Against ticket no[]================

        StrQry = " select 3 as Sno, 'Normal' as  Invoice_Type,TSPL_SD_SALE_INVOICE_detail.qty,tspl_item_master.Alies_Name+char(13)+TSPL_SD_SALE_INVOICE_detail.unit_code as short_description,case when TSPL_ITEM_MASTER.SKU_Seq = 0 then 10000 else  TSPL_ITEM_MASTER.SKU_Seq end  SKU_Seq,0 as QtyCrateForTotal," &
 " convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date, TSPL_SD_SALE_INVOICE_HEAD.Manual_Driver_Name,TSPL_SD_SALE_INVOICE_HEAD.Manual_Salesman_Name, tspl_customer_master.Priority_Level," &
" TSPL_VEHICLE_MASTER.employee_id as Driver_code,tspl_employee_master.emp_Name as Driver_Name,tspl_route_master.Route_No ,tspl_route_master.route_desc, TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3, TSPL_COMPANY_MASTER.Pincode as Comp_Pin , (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then AlterNateVehicle.Number   else TSPL_VEHICLE_MASTER.Number end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)  as Number,TSPL_SD_SALE_INVOICE_HEAD.Document_Code as BillNo,  tspl_customer_master.cust_code,tspl_customer_master.Customer_Name as PartyName " &
" ,TSPL_SD_SALE_INVOICE_detail.Crate,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt  , " &
" TSPL_SD_SALE_INVOICE_detail.item_code ,(select top 1 salesman_code from tspl_salesman_detail where route_code=tspl_route_master.route_no) as salesman_code" &
" ,TSPL_SD_SALE_INVOICE_detail.Crate as Crate_Qty " &
" ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CanUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as CAN_Qty " &
" ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/BoxUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Box_Qty " &
 " ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/BoxCrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor)  as Box_Crate_Qty  " &
" ,0 as Free_Box_Crate_Qty" &
" , 0  as free_Qty_In_Ltr,0 as Free_Ltr_Crate_Qty,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/ltrCrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor)  as Ltr_Crate_Qty" &
" ,TSPL_SD_SALE_INVOICE_detail.Crate as Main_Qty_in_Crate,0 as free_Qty_in_Crate" &
" ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/BoxUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as main_Qty_in_box,0 as Free_qty_in_box" &
" from TSPL_SD_SALE_INVOICE_HEAD  left join TSPL_SD_SALE_INVOICE_detail on TSPL_SD_SALE_INVOICE_detail.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " &
" left join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SD_SALE_INVOICE_HEAD.customer_code  " &
" left join tspl_company_master on tspl_company_master.Comp_Code=TSPL_SD_SALE_INVOICE_HEAD.Comp_Code  " &
" left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  " &
" left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no   " &
" left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle  " &
" left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  " &
" left join tspl_employee_master on tspl_employee_master.emp_code=TSPL_VEHICLE_MASTER.employee_id " &
" left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no" &
" left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and CrateUnit.uom_code=" &
" (select unit_code from  TSPL_UNIT_MASTER   where TSPL_UNIT_MASTER.unit_code=CrateUnit.uom_code and TSPL_UNIT_MASTER.Crate_Type ='Y')" &
" left join tspl_item_uom_detail CanUnit on CanUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and CanUnit.uom_code=(select unit_code from  TSPL_UNIT_MASTER   where TSPL_UNIT_MASTER.unit_code=CanUnit.uom_code and TSPL_UNIT_MASTER.Can_Type ='Y')" &
" left join tspl_item_uom_detail BoxUnit on BoxUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and BoxUnit.uom_code=(select unit_code from  TSPL_UNIT_MASTER   where TSPL_UNIT_MASTER.unit_code=BoxUnit.uom_code and TSPL_UNIT_MASTER.Box_Type ='Y')" &
" left join tspl_item_uom_detail as BoxCrateUnit on BoxCrateUnit.item_code=BoxUnit.item_code  and 	BoxCrateUnit.uom_code=	(select unit_code from  TSPL_UNIT_MASTER   where TSPL_UNIT_MASTER.unit_code=BoxCrateUnit.uom_code and TSPL_UNIT_MASTER.Crate_Type ='Y')" &
" left join tspl_item_uom_detail LtrUnit on LtrUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and LtrUnit.uom_code=(select unit_code from  TSPL_UNIT_MASTER   where TSPL_UNIT_MASTER.unit_code=LtrUnit.uom_code and TSPL_UNIT_MASTER.Ltr_Type  ='Y')" &
" left join tspl_item_uom_detail LtrCrateUnit on LtrCrateUnit.item_code=LtrUnit.item_code and LtrCrateUnit.uom_code=(select unit_code from  TSPL_UNIT_MASTER   where TSPL_UNIT_MASTER.unit_code=LtrCrateUnit.uom_code and TSPL_UNIT_MASTER.Crate_Type  ='Y')" &
" left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	and  StockUnit.stocking_unit='Y'  " &
" left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code" &
" left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_INVOICE_detail.item_code" &
" where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + "   and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and TSPL_SD_SALE_INVOICE_HEAD.isSampling =0 and TSPL_SD_SALE_INVOICE_detail.Scheme_item='N'  and (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "' and tspl_route_master.route_no in (" & Route & ")  " &
        " union all" &
" select 1 as Sno, 'Free' as  Invoice_Type,TSPL_SD_SALE_INVOICE_detail.qty,tspl_item_master.Alies_Name+char(13)+TSPL_SD_SALE_INVOICE_detail.unit_code+ ' (F)' as short_description,case when TSPL_ITEM_MASTER.SKU_Seq = 0 then 10000 else  TSPL_ITEM_MASTER.SKU_Seq end  SKU_Seq,0 as QtyCrateForTotal," &
 " convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date, TSPL_SD_SALE_INVOICE_HEAD.Manual_Driver_Name,TSPL_SD_SALE_INVOICE_HEAD.Manual_Salesman_Name, tspl_customer_master.Priority_Level," &
" TSPL_VEHICLE_MASTER.employee_id as Driver_code,tspl_employee_master.emp_Name as Driver_Name,tspl_route_master.Route_No ,tspl_route_master.route_desc, TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3, TSPL_COMPANY_MASTER.Pincode as Comp_Pin , (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then AlterNateVehicle.Number   else TSPL_VEHICLE_MASTER.Number end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)  as Number,TSPL_SD_SALE_INVOICE_HEAD.Document_Code as BillNo,  tspl_customer_master.cust_code,tspl_customer_master.Customer_Name as PartyName " &
" ,TSPL_SD_SALE_INVOICE_detail.Crate,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt  , " &
" TSPL_SD_SALE_INVOICE_detail.item_code ,(select top 1 salesman_code from tspl_salesman_detail where route_code=tspl_route_master.route_no) as salesman_code" &
",0 as  Crate_Qty " &
",convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CanUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as CAN_Qty " &
",convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/BoxUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Box_Qty " &
" ,0 as Box_Crate_Qty " &
" ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/BoxCrateUnit .conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as   Free_Box_Crate_Qty" &
" , convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as    free_Qty_In_Ltr," &
" convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/LtrCrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor)as Free_Ltr_Crate_Qty, 0 as Ltr_Crate_Qty" &
" ,0 as Main_Qty_in_Crate" &
" ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as  free_Qty_in_Crate,0 as main_Qty_in_box" &
" , convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/BoxUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Free_qty_in_box" &
" from TSPL_SD_SALE_INVOICE_HEAD  left join TSPL_SD_SALE_INVOICE_detail on TSPL_SD_SALE_INVOICE_detail.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " &
" left join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SD_SALE_INVOICE_HEAD.customer_code  " &
" left join tspl_company_master on tspl_company_master.Comp_Code=TSPL_SD_SALE_INVOICE_HEAD.Comp_Code  " &
" left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  " &
" left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no   " &
" left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle  " &
" left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  " &
" left join tspl_employee_master on tspl_employee_master.emp_code=TSPL_VEHICLE_MASTER.employee_id " &
" left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no" &
" left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and CrateUnit.uom_code=" &
" (select unit_code from  TSPL_UNIT_MASTER   where TSPL_UNIT_MASTER.unit_code=CrateUnit.uom_code and TSPL_UNIT_MASTER.Crate_Type ='Y')" &
" left join tspl_item_uom_detail CanUnit on CanUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and CanUnit.uom_code=(select unit_code from  TSPL_UNIT_MASTER   where TSPL_UNIT_MASTER.unit_code=CanUnit.uom_code and TSPL_UNIT_MASTER.Can_Type ='Y')" &
" left join tspl_item_uom_detail BoxUnit on BoxUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and BoxUnit.uom_code=(select unit_code from  TSPL_UNIT_MASTER   where TSPL_UNIT_MASTER.unit_code=BoxUnit.uom_code and TSPL_UNIT_MASTER.Box_Type ='Y')" &
" left join tspl_item_uom_detail as BoxCrateUnit on BoxCrateUnit.item_code=BoxUnit.item_code  and 	BoxCrateUnit.uom_code=	(select unit_code from  TSPL_UNIT_MASTER   where TSPL_UNIT_MASTER.unit_code=BoxCrateUnit.uom_code and TSPL_UNIT_MASTER.Crate_Type ='Y')" &
" left join tspl_item_uom_detail LtrUnit on LtrUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and LtrUnit.uom_code=(select unit_code from  TSPL_UNIT_MASTER   where TSPL_UNIT_MASTER.unit_code=LtrUnit.uom_code and TSPL_UNIT_MASTER.Ltr_Type  ='Y')" &
" left join tspl_item_uom_detail LtrCrateUnit on LtrCrateUnit.item_code=LtrUnit.item_code and LtrCrateUnit.uom_code=(select unit_code from  TSPL_UNIT_MASTER   where TSPL_UNIT_MASTER.unit_code=LtrCrateUnit.uom_code and TSPL_UNIT_MASTER.Crate_Type  ='Y')" &
" left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	and  StockUnit.stocking_unit='Y'  " &
" left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code" &
" left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_INVOICE_detail.item_code" &
" where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + "  and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS'   and TSPL_SD_SALE_INVOICE_detail.Scheme_item='Y'   and (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "' and tspl_route_master.route_no in (" & Route & ")  " &
        " union all" &
    " select 2 as Sno, 'Sample' as  Invoice_Type,TSPL_SD_SALE_INVOICE_detail.qty,tspl_item_master.Alies_Name+char(13)+TSPL_SD_SALE_INVOICE_detail.unit_code + ' (S)' as short_description,case when TSPL_ITEM_MASTER.SKU_Seq = 0 then 10000 else  TSPL_ITEM_MASTER.SKU_Seq end  SKU_Seq,0 as QtyCrateForTotal," &
 " convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date, TSPL_SD_SALE_INVOICE_HEAD.Manual_Driver_Name,TSPL_SD_SALE_INVOICE_HEAD.Manual_Salesman_Name, tspl_customer_master.Priority_Level," &
" TSPL_VEHICLE_MASTER.employee_id as Driver_code,tspl_employee_master.emp_Name as Driver_Name,tspl_route_master.Route_No ,tspl_route_master.route_desc, TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3, TSPL_COMPANY_MASTER.Pincode as Comp_Pin , (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then AlterNateVehicle.Number   else TSPL_VEHICLE_MASTER.Number end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)  as Number,TSPL_SD_SALE_INVOICE_HEAD.Document_Code as BillNo,  tspl_customer_master.cust_code,tspl_customer_master.Customer_Name as PartyName " &
" ,TSPL_SD_SALE_INVOICE_detail.Crate,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt  , " &
" TSPL_SD_SALE_INVOICE_detail.item_code ,(select top 1 salesman_code from tspl_salesman_detail where route_code=tspl_route_master.route_no) as salesman_code" &
" ,0 as Crate_Qty " &
" ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CanUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as CAN_Qty " &
" ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/BoxUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Box_Qty " &
 " ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/BoxCrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor)  as Box_Crate_Qty  " &
" ,0 as Free_Box_Crate_Qty" &
" , convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor)   as free_Qty_In_Ltr,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/LtrCrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Free_Ltr_Crate_Qty,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/ltrCrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor)  as Ltr_Crate_Qty" &
 " ,0 as Main_Qty_in_Crate,0 as free_Qty_in_Crate" &
 "  ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/BoxUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as main_Qty_in_box,0 as Free_qty_in_box" &
" from TSPL_SD_SALE_INVOICE_HEAD  left join TSPL_SD_SALE_INVOICE_detail on TSPL_SD_SALE_INVOICE_detail.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " &
" left join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SD_SALE_INVOICE_HEAD.customer_code  " &
" left join tspl_company_master on tspl_company_master.Comp_Code=TSPL_SD_SALE_INVOICE_HEAD.Comp_Code  " &
" left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  " &
" left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no   " &
" left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle  " &
" left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  " &
" left join tspl_employee_master on tspl_employee_master.emp_code=TSPL_VEHICLE_MASTER.employee_id " &
" left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no" &
" left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and CrateUnit.uom_code=" &
" (select unit_code from  TSPL_UNIT_MASTER   where TSPL_UNIT_MASTER.unit_code=CrateUnit.uom_code and TSPL_UNIT_MASTER.Crate_Type ='Y')" &
" left join tspl_item_uom_detail CanUnit on CanUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and CanUnit.uom_code=(select unit_code from  TSPL_UNIT_MASTER   where TSPL_UNIT_MASTER.unit_code=CanUnit.uom_code and TSPL_UNIT_MASTER.Can_Type ='Y')" &
" left join tspl_item_uom_detail BoxUnit on BoxUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and BoxUnit.uom_code=(select unit_code from  TSPL_UNIT_MASTER   where TSPL_UNIT_MASTER.unit_code=BoxUnit.uom_code and TSPL_UNIT_MASTER.Box_Type ='Y')" &
" left join tspl_item_uom_detail as BoxCrateUnit on BoxCrateUnit.item_code=BoxUnit.item_code  and 	BoxCrateUnit.uom_code=	(select unit_code from  TSPL_UNIT_MASTER   where TSPL_UNIT_MASTER.unit_code=BoxCrateUnit.uom_code and TSPL_UNIT_MASTER.Crate_Type ='Y')" &
" left join tspl_item_uom_detail LtrUnit on LtrUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and LtrUnit.uom_code=(select unit_code from  TSPL_UNIT_MASTER   where TSPL_UNIT_MASTER.unit_code=LtrUnit.uom_code and TSPL_UNIT_MASTER.Ltr_Type  ='Y')" &
" left join tspl_item_uom_detail LtrCrateUnit on LtrCrateUnit.item_code=LtrUnit.item_code and LtrCrateUnit.uom_code=(select unit_code from  TSPL_UNIT_MASTER   where TSPL_UNIT_MASTER.unit_code=LtrCrateUnit.uom_code and TSPL_UNIT_MASTER.Crate_Type  ='Y')" &
" left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	and  StockUnit.stocking_unit='Y'  " &
" left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code" &
" left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_INVOICE_detail.item_code" &
" where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + "   and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and TSPL_SD_SALE_INVOICE_HEAD.isSampling =1 and TSPL_SD_SALE_INVOICE_detail.Scheme_item='N'  and (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "' and tspl_route_master.route_no in (" & Route & ")  "

        '=====================================================================


        '        StrQry = " select TotalBoxSch.Jaali,TotalBoxSch.box,TotalBoxSch.Crate,* from ( select  max(Route_No ) as Route_No, Cust_code,ISNULL(max(ReceDoc),'')  AS ReceDoc,ISNULL(max(RecDate),NULL) as RecDate,(select total_Amt from tspl_sd_sale_invoice_head where tspl_sd_sale_invoice_head.document_code=max(final.ReceDoc)  )as Rec_Total_Amt,sum(Crate_Qty) as Crate_Qty," & _
        '" sum(Can_Qty) as Can_Qty,sum(Box_Qty) as Box_Qty ,sum(Box_Crate_Qty) as Box_Crate_Qty,sum(Box_Crate_QtywithCeling) as Box_Crate_QtywithCeling ,sum(free_Qty_In_Ltr) as free_Qty_In_Ltr,sum(Ltr_Crate_Qty) as Ltr_Crate_Qty,sum(Ltr_Crate_QtywithCeling) as Ltr_Crate_QtywithCeling,max(Driver_code) as Driver_code,max(Driver_Name) as Driver_Name," & _
        '" max(route_desc) as route_desc,max(Comp_Name) as Comp_Name,max(Comp_Add1) as Comp_Add1,max(Comp_Add3) as Comp_Add3,max(Comp_Pin) as Comp_Pin,max(Number) as Number," & _
        '" max(convert(varchar,final.Document_Date,103)) as Document_Date,max(PartyName) as PartyName,max(Priority_Level) as Priority_Level ,max(tspl_employee_master.emp_name) as emp_name,max(Manual_Driver_Name) as Manual_Driver_Name,max(Manual_Salesman_Name) as Manual_Salesman_Name,billNo,max(Total_Amt) as Total_Amt,sum(Free_Box_Crate_Qty) as Free_Box_Crate_Qty,sum(free_Box_Crate_QtywithCeling) as free_Box_Crate_QtywithCeling from (" & _
        '" select TSPL_SD_SALE_INVOICE_HEAD.Manual_Driver_Name,TSPL_SD_SALE_INVOICE_HEAD.Manual_Salesman_Name, tspl_customer_master.Priority_Level,pending.ReceDoc  AS ReceDoc,pending.RecDate as RecDate,0 as Rec_Total_Amt,isnull(CrateQty.Crate_Qty,0) as Crate_Qty, isnull(CanQty.Can_Qty,0) as Can_Qty ,isnull(BoxQty.Box_Qty,0) as Box_Qty ,isnull(BoxQty.Box_Crate_Qty,0) as Box_Crate_Qty , round(isnull(BoxQty.Box_Crate_QtywithCeling,0),0) as Box_Crate_QtywithCeling, " & _
        '" 0  as free_Qty_In_Ltr,0 as Ltr_Crate_Qty,0 as Ltr_Crate_QtywithCeling,TSPL_VEHICLE_MASTER.employee_id as Driver_code,tspl_employee_master.emp_Name as Driver_Name,tspl_route_master.Route_No ,tspl_route_master.route_desc," & _
        '" TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3," & _
        '" TSPL_COMPANY_MASTER.Pincode as Comp_Pin , (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then AlterNateVehicle.Number   else TSPL_VEHICLE_MASTER.Number end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)  as Number,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_SD_SALE_INVOICE_HEAD.Document_Code as BillNo, " & _
        '" tspl_customer_master.cust_code,tspl_customer_master.Customer_Name as PartyName ,TSPL_SD_SALE_INVOICE_HEAD.Crate,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt ," & _
        '        " TSPL_SD_SALE_INVOICE_detail.item_code ,(select top 1 salesman_code from tspl_salesman_detail where route_code=tspl_route_master.route_no) as salesman_code,0 as Free_Box_Crate_Qty,0 as free_Box_Crate_QtywithCeling from TSPL_SD_SALE_INVOICE_HEAD " & _
        '" left join TSPL_SD_SALE_INVOICE_detail on TSPL_SD_SALE_INVOICE_detail.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code" & _
        '" left join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SD_SALE_INVOICE_HEAD.customer_code " & _
        '" left join tspl_company_master on tspl_company_master.Comp_Code=TSPL_SD_SALE_INVOICE_HEAD.Comp_Code " & _
        '" left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code " & _
        ' " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  " & _
        '" left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle " & _
        '" left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code " & _
        '" left join tspl_employee_master on tspl_employee_master.emp_code=TSPL_VEHICLE_MASTER.employee_id" & _
        ' " left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no" & _
        '" left join (select max(Document_Code) as Document_Code,max(delivery_code) as delivery_code,Sch_Item,sum(Box_Qty) as Box_Qty ,sum(Box_Crate_Qty) as Box_Crate_Qty ,ceiling(sum(Box_Crate_Qty)) as Box_Crate_QtywithCeling from (select TSPL_SD_SALE_INVOICE_detail.delivery_code,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/BoxUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Box_Qty  ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor)  as Box_Crate_Qty  " & _
        '        " from tspl_sd_sale_invoice_head" & _
        ' " left join tspl_sd_sale_invoice_detail on tspl_sd_sale_invoice_head.Document_Code=tspl_sd_sale_invoice_detail.Document_Code" & _
        '" left join tspl_item_uom_detail BoxUnit on BoxUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" & _
        ' " left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=BoxUnit.uom_code  " & _
        ' " left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 " & _
        '" left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code " & _
        '"left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code  and 	CrateUnit.uom_code=	'Crate'" & _
        ' " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  " & _
        '  " left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code" & _
        '" left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no" & _
        '" where TSPL_SD_SALE_INVOICE_detail.Scheme_item='N' and TSPL_SD_SALE_INVOICE_HEAD.isSampling =0 and tspl_unit_master.Box_Type ='Y' and StockUnit.stocking_unit='Y'" & _
        '" and (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "'and tspl_route_master.route_no='" & Route & "' and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + "" & _
        '" )as dd group by Sch_Item ) as BoxQty on  BoxQty.document_code=tspl_sd_sale_invoice_head.document_code and BoxQty.Sch_Item=TSPL_SD_SALE_INVOICE_detail.item_code  " & _
        '" left join (select TSPL_SD_SALE_INVOICE_detail.delivery_code,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CanUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as CAN_Qty " & _
        '       " from tspl_sd_sale_invoice_head" & _
        ' " left join tspl_sd_sale_invoice_detail on tspl_sd_sale_invoice_head.Document_Code=tspl_sd_sale_invoice_detail.Document_Code" & _
        '" left join tspl_item_uom_detail CanUnit on CanUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" & _
        ' " left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=CanUnit.uom_code  " & _
        ' " left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 " & _
        ' " left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code " & _
        '       " where TSPL_SD_SALE_INVOICE_detail.Scheme_item='N' and TSPL_SD_SALE_INVOICE_HEAD.isSampling =0 and tspl_unit_master.CAN_Type ='Y' and StockUnit.stocking_unit='Y'" & _
        '" ) as CANQty on  CANQty.document_code=tspl_sd_sale_invoice_head.document_code and CANQty.Sch_Item=TSPL_SD_SALE_INVOICE_detail.item_code and CANQty.delivery_code=TSPL_SD_SALE_INVOICE_detail.Delivery_Code" & _
        '" left join (select TSPL_SD_SALE_INVOICE_detail.delivery_code,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Crate_Qty " & _
        '       "  from tspl_sd_sale_invoice_head" & _
        ' " left join tspl_sd_sale_invoice_detail on tspl_sd_sale_invoice_head.Document_Code=tspl_sd_sale_invoice_detail.Document_Code" & _
        '" left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and CrateUnit.uom_code=TSPL_SD_SALE_INVOICE_detail.unit_code	" & _
        '" left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=CrateUnit.uom_code  " & _
        ' " left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 " & _
        ' " left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code " & _
        '        " where TSPL_SD_SALE_INVOICE_detail.Scheme_item='N' and TSPL_SD_SALE_INVOICE_HEAD.isSampling =0 and tspl_unit_master.Crate_Type ='Y' and StockUnit.stocking_unit='Y'" & _
        '" ) as CrateQty on  CrateQty.document_code=tspl_sd_sale_invoice_head.document_code and CrateQty.Sch_Item=TSPL_SD_SALE_INVOICE_detail.item_code and CrateQty.delivery_code=TSPL_SD_SALE_INVOICE_detail.Delivery_Code" & _
        '    " left join ( SELECT max(Priority_Level) as Priority_Level, MIN(document_code) AS ReceDoc,MIN(document_date) AS RecDate,SUM(Total_Amt) AS Rec_Total_Amt" & _
        '" ,0  AS Crate_Qty, 0   as Can_Qty,0 as Box_Qty,0 as free_Qty_In_Ltr,'' as Driver_code,'' as Driver_Name, '' as route_desc,'' as Comp_Name,'' as Comp_Add1,'' as comp_Add2,'' as Comp_Add3,'' as Comp_Pin,'' as Number, nULL as Document_Date,'' AS BilNo,customer_code,'' as PartyName ,0 as Crate,0 as Total_Amt,'' as Item_code,'' as salesman_code FROM (select tspl_customer_master.Priority_Level, tspl_sd_sale_invoice_head.customer_code,  tspl_sd_sale_invoice_head.document_code ,CONVERT(VARCHAR,tspl_sd_sale_invoice_head.document_date,103) AS document_date,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt from tspl_sd_sale_invoice_head left join tspl_customer_invoice_head on tspl_customer_invoice_head.against_sale_no=tspl_sd_sale_invoice_head.document_code left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=tspl_sd_sale_invoice_head.customer_code where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)<convert(date,'" + Docdate + "',103) " + DocTimeRange + " and  isnull(tspl_customer_invoice_head.balance_amt,0)>0  and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "'and tspl_route_master.route_no='" & Route & "' " & _
        ' " ) AS XX " & _
        ' " GROUP BY customer_code ) as pending on pending. customer_code=TSPL_SD_SALE_INVOICE_HEAD.customer_code" & _
        '" where TSPL_SD_SALE_INVOICE_detail.Scheme_item='N' and TSPL_SD_SALE_INVOICE_HEAD.isSampling =0 and " & _
        '" TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "'and tspl_route_master.route_no='" & Route & "'AND " & _
        '" convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + " " & _
        ' " union all" & _
        '" select '' as Manual_Driver_Name,'' as Manual_Salesman_Name,Priority_Level,''  AS ReceDoc,null as RecDate,0 as Rec_Total_Amt,0 as Crate_Qty,0 as Can_Qty,0 as Box_Qty,0 as Box_Crate_Qty,0 as Box_Crate_QtywithCeling,isnull(freeScheme.Ltr_Qty,0) as Ltr_Qty,isnull(freeScheme.Ltr_Crate_Qty,0) as Ltr_Crate_Qty,isnull(freeScheme.Ltr_Crate_QtywithCeling,0) as Ltr_Crate_QtywithCeling,Driver_code,Driver_Name,Route_No,route_desc,Comp_Name,Comp_Add1,Comp_Add2,Comp_Add3,Comp_Pin,Number,Document_Date,BillNo,cust_code,PartyName ,Crate,Total_Amt,item_code,salesman_code ," & _
        '       " Free_Box_Crate_Qty, Free_Box_Crate_QtywithCeling" & _
        '" from  (select tspl_customer_master.Priority_Level, TSPL_VEHICLE_MASTER.employee_id as Driver_code,tspl_employee_master.emp_Name as Driver_Name,tspl_route_master.Route_No ,tspl_route_master.route_desc,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin , (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then AlterNateVehicle.Number   else TSPL_VEHICLE_MASTER.Number end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)  as Number,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_SD_SALE_INVOICE_HEAD.Document_Code as BillNo, tspl_customer_master.cust_code,tspl_customer_master.Customer_Name as PartyName ,TSPL_SD_SALE_INVOICE_HEAD.Crate,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt ,TSPL_SD_SALE_INVOICE_detail.item_code, TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item ,Free_QtyINLtr.Ltr_Qty as Ltr_Qty ,Ltr_Crate_Qty,Ltr_Crate_QtywithCeling,TSPL_SD_SALE_INVOICE_detail.Scheme_item,TSPL_SD_SALE_INVOICE_detail.scheme_code,(select top 1 salesman_code from tspl_salesman_detail where route_code=tspl_route_master.route_no) as salesman_code" & _
        '" ,FreeBoxQty.Box_Crate_Qty as Free_Box_Crate_Qty,FreeBoxQty.Box_Crate_QtywithCeling as Free_Box_Crate_QtywithCeling" & _
        ' " from tspl_sd_sale_invoice_head left join TSPL_SD_SALE_INVOICE_detail on TSPL_SD_SALE_INVOICE_detail.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code left join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SD_SALE_INVOICE_HEAD.customer_code  left join tspl_company_master on tspl_company_master.Comp_Code=TSPL_SD_SALE_INVOICE_HEAD.Comp_Code  left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  " & _
        '" left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no   left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  left join tspl_employee_master on tspl_employee_master.emp_code=TSPL_VEHICLE_MASTER.employee_id left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no " & _
        ' " left join (select max(Document_Code) as Document_Code,max(delivery_code) as delivery_code,Sch_Item,sum(Box_Qty) as Box_Qty ,sum(Box_Crate_Qty) as Box_Crate_Qty ,ceiling(sum(Box_Crate_Qty)) as Box_Crate_QtywithCeling from (select TSPL_SD_SALE_INVOICE_detail.delivery_code, TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/BoxUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Box_Qty  ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor)  as Box_Crate_Qty   from tspl_sd_sale_invoice_head left join tspl_sd_sale_invoice_detail on tspl_sd_sale_invoice_head.Document_Code=tspl_sd_sale_invoice_detail.Document_Code left join tspl_item_uom_detail BoxUnit on BoxUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=BoxUnit.uom_code   left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	  left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code  and 	CrateUnit.uom_code=	'Crate'  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  " & _
        ' " left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code" & _
        ' " left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no where TSPL_SD_SALE_INVOICE_detail.Scheme_item='Y' and TSPL_SD_SALE_INVOICE_HEAD.isSampling =0 and tspl_unit_master.Box_Type ='Y' and StockUnit.stocking_unit='Y' and (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "'and tspl_route_master.route_no='" & Route & "' and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + " )as dd group by Sch_Item) as FreeBoxQty on  FreeBoxQty.document_code=tspl_sd_sale_invoice_head.document_code and FreeBoxQty.Sch_Item=TSPL_SD_SALE_INVOICE_detail.item_code " & _
        '" left join (select max(Document_Code) as Document_Code,max(delivery_code) as delivery_code,Sch_Item,sum(Ltr_Qty) as Ltr_Qty,sum(Ltr_Crate_Qty) as Ltr_Crate_Qty,round(sum(Ltr_Crate_Qty),0) as Ltr_Crate_QtywithCeling from (select TSPL_SD_SALE_INVOICE_detail.delivery_code,LtrUnit.uom_code,LtrUnit.conversion_factor as LtrUnitCF,StockUnit.conversion_factor as StockUnitCF,CurrentUnit.conversion_factor as CurrentUnitCF,TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ,TSPL_SD_SALE_INVOICE_detail.Scheme_Item_Code as Sch_Item ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Ltr_Qty , convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Ltr_Crate_Qty  from TSPL_SD_SALE_INVOICE_detail left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no  left join tspl_item_uom_detail LtrUnit on LtrUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" & _
        '" left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=LtrUnit.uom_code  " & _
        '" left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code  left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code  and 	CrateUnit.uom_code=	'Crate'  where Scheme_item='Y' and tspl_unit_master.ltr_Type ='Y' and StockUnit.stocking_unit='Y' and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "'and tspl_route_master.route_no='" & Route & "' AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + " ) as dd group by Sch_Item ) as Free_QtyINLtr  on  Free_QtyINLtr.document_code=tspl_sd_sale_invoice_head.document_code and Free_QtyINLtr.Sch_Item=TSPL_SD_SALE_INVOICE_detail.Scheme_Item_Code and Free_QtyINLtr.delivery_code=TSPL_SD_SALE_INVOICE_detail.Delivery_Code " & _
        '" where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + " " & _
        '" and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and TSPL_SD_SALE_INVOICE_detail.Scheme_item='Y' and TSPL_SD_SALE_INVOICE_HEAD.isSampling =0 " & _
        '" and (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "'and tspl_route_master.route_no='" & Route & "' " & _
        '" ) as freeScheme" & _
        ' " union all" & _
        '" select TSPL_SD_SALE_INVOICE_HEAD.Manual_Driver_Name,TSPL_SD_SALE_INVOICE_HEAD.Manual_Salesman_Name, tspl_customer_master.Priority_Level,pending.ReceDoc  AS ReceDoc,pending.RecDate as RecDate,0 as Rec_Total_Amt,isnull(CrateQty.Crate_Qty,0) as Crate_Qty, isnull(CanQty.Can_Qty,0) as Can_Qty ,isnull(BoxQty.Box_Qty,0) as Box_Qty ,isnull(BoxQty.Box_Crate_Qty,0) as Box_Crate_Qty , ceiling(isnull(BoxQty.Box_Crate_QtywithCeling,0)) as Box_Crate_QtywithCeling," & _
        '" 0  as free_Qty_In_Ltr,0 as Ltr_Crate_Qty,0 as Ltr_Crate_QtywithCeling,TSPL_VEHICLE_MASTER.employee_id as Driver_code,tspl_employee_master.emp_Name as Driver_Name,tspl_route_master.Route_No ,tspl_route_master.route_desc," & _
        '" TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3," & _
        '" TSPL_COMPANY_MASTER.Pincode as Comp_Pin , (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then AlterNateVehicle.Number   else TSPL_VEHICLE_MASTER.Number end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)  as Number,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_SD_SALE_INVOICE_HEAD.Document_Code as BillNo, " & _
        '" tspl_customer_master.cust_code,tspl_customer_master.Customer_Name as PartyName ,TSPL_SD_SALE_INVOICE_HEAD.Crate,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt ," & _
        '        " TSPL_SD_SALE_INVOICE_detail.item_code ,(select top 1 salesman_code from tspl_salesman_detail where route_code=tspl_route_master.route_no) as salesman_code,0 as Free_Box_Crate_Qty,0 as free_Box_Crate_QtywithCeling from TSPL_SD_SALE_INVOICE_HEAD " & _
        '" left join TSPL_SD_SALE_INVOICE_detail on TSPL_SD_SALE_INVOICE_detail.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code" & _
        '" left join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SD_SALE_INVOICE_HEAD.customer_code " & _
        '" left join tspl_company_master on tspl_company_master.Comp_Code=TSPL_SD_SALE_INVOICE_HEAD.Comp_Code " & _
        '" left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code " & _
        ' " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  " & _
        '" left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle " & _
        '" left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code " & _
        '" left join tspl_employee_master on tspl_employee_master.emp_code=TSPL_VEHICLE_MASTER.employee_id" & _
        ' " left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no" & _
        '" left join (select max(Document_Code) as Document_Code,max(delivery_code) as delivery_code,Sch_Item,sum(Box_Qty) as Box_Qty,sum(Box_Crate_Qty) as Box_Crate_Qty,ceiling(sum(Box_Crate_Qty)) as Box_Crate_QtywithCeling  from (select TSPL_SD_SALE_INVOICE_detail.delivery_code, TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/BoxUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Box_Qty " & _
        '",convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Box_Crate_Qty  " & _
        '        " from tspl_sd_sale_invoice_head" & _
        ' " left join tspl_sd_sale_invoice_detail on tspl_sd_sale_invoice_head.Document_Code=tspl_sd_sale_invoice_detail.Document_Code" & _
        '" left join tspl_item_uom_detail BoxUnit on BoxUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" & _
        ' " left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=BoxUnit.uom_code  " & _
        ' " left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 " & _
        '" left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code " & _
        '"left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code  and 	CrateUnit.uom_code=	'Crate'" & _
        '" left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  " & _
        '  " left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code" & _
        '" left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no" & _
        '" where TSPL_SD_SALE_INVOICE_detail.Scheme_item='N' and TSPL_SD_SALE_INVOICE_HEAD.isSampling =1 and tspl_unit_master.Box_Type ='Y' and StockUnit.stocking_unit='Y'" & _
        '" and (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "'and tspl_route_master.route_no='" & Route & "' and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + "" & _
        '" ) as dd group by Sch_Item )as BoxQty on  BoxQty.document_code=tspl_sd_sale_invoice_head.document_code and BoxQty.Sch_Item=TSPL_SD_SALE_INVOICE_detail.item_code " & _
        '" left join (select TSPL_SD_SALE_INVOICE_detail.delivery_code, TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CanUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as CAN_Qty " & _
        '       " from tspl_sd_sale_invoice_head" & _
        ' " left join tspl_sd_sale_invoice_detail on tspl_sd_sale_invoice_head.Document_Code=tspl_sd_sale_invoice_detail.Document_Code" & _
        '" left join tspl_item_uom_detail CanUnit on CanUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" & _
        ' " left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=CanUnit.uom_code  " & _
        ' " left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 " & _
        ' " left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code " & _
        '       " where TSPL_SD_SALE_INVOICE_detail.Scheme_item='N' and TSPL_SD_SALE_INVOICE_HEAD.isSampling =1 and tspl_unit_master.CAN_Type ='Y' and StockUnit.stocking_unit='Y'" & _
        '" ) as CANQty on  CANQty.document_code=tspl_sd_sale_invoice_head.document_code and CANQty.Sch_Item=TSPL_SD_SALE_INVOICE_detail.item_code and CANQty.delivery_code=TSPL_SD_SALE_INVOICE_detail.Delivery_Code" & _
        '" left join (select TSPL_SD_SALE_INVOICE_detail.delivery_code,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Crate_Qty " & _
        '       "  from tspl_sd_sale_invoice_head" & _
        ' " left join tspl_sd_sale_invoice_detail on tspl_sd_sale_invoice_head.Document_Code=tspl_sd_sale_invoice_detail.Document_Code" & _
        '" left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and CrateUnit.uom_code=TSPL_SD_SALE_INVOICE_detail.unit_code	" & _
        '" left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=CrateUnit.uom_code  " & _
        ' " left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 " & _
        ' " left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code " & _
        '        " where TSPL_SD_SALE_INVOICE_detail.Scheme_item='N' and TSPL_SD_SALE_INVOICE_HEAD.isSampling =1 and tspl_unit_master.Crate_Type ='Y' and StockUnit.stocking_unit='Y'" & _
        '" ) as CrateQty on  CrateQty.document_code=tspl_sd_sale_invoice_head.document_code and CrateQty.Sch_Item=TSPL_SD_SALE_INVOICE_detail.item_code and CrateQty.delivery_code=TSPL_SD_SALE_INVOICE_detail.Delivery_Code" & _
        '    " left join ( SELECT max(Priority_Level) as Priority_Level, MIN(document_code) AS ReceDoc,MIN(document_date) AS RecDate,SUM(Total_Amt) AS Rec_Total_Amt" & _
        '" ,0  AS Crate_Qty, 0   as Can_Qty,0 as Box_Qty,0 as free_Qty_In_Ltr,'' as Driver_code,'' as Driver_Name, '' as route_desc,'' as Comp_Name,'' as Comp_Add1,'' as comp_Add2,'' as Comp_Add3,'' as Comp_Pin,'' as Number, nULL as Document_Date,'' AS BilNo,customer_code,'' as PartyName ,0 as Crate,0 as Total_Amt,'' as Item_code,'' as salesman_code FROM (select tspl_customer_master.Priority_Level, tspl_sd_sale_invoice_head.customer_code,  tspl_sd_sale_invoice_head.document_code ,CONVERT(VARCHAR,tspl_sd_sale_invoice_head.document_date,103) AS document_date,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt from tspl_sd_sale_invoice_head left join tspl_customer_invoice_head on tspl_customer_invoice_head.against_sale_no=tspl_sd_sale_invoice_head.document_code left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=tspl_sd_sale_invoice_head.customer_code where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)<convert(date,'" + Docdate + "',103) " + DocTimeRange + " and  isnull(tspl_customer_invoice_head.balance_amt,0)>0  and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "'and tspl_route_master.route_no='" & Route & "' " & _
        ' " ) AS XX " & _
        ' " GROUP BY customer_code ) as pending on pending. customer_code=TSPL_SD_SALE_INVOICE_HEAD.customer_code" & _
        '" where TSPL_SD_SALE_INVOICE_detail.Scheme_item='N' and TSPL_SD_SALE_INVOICE_HEAD.isSampling =1 and " & _
        '" TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "'and tspl_route_master.route_no='" & Route & "'AND " & _
        '" convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + "" & _
        '" ) as final" & _
        '" left join tspl_employee_master on tspl_employee_master.emp_code=final.salesman_code" & _
        ' " group by Cust_code,billno" & _
        '" ) as xx" & _
        '" left join (select customer_code, sum(Box) as Box,sum(jaali) as Jaali,sum(Crate) as Crate from (select  TSPL_SD_SHIPMENT_HEAD.crate,TSPL_SD_SHIPMENT_HEAD.document_code,TSPL_SD_SHIPMENT_HEAD.customer_code,(TSPL_SD_SHIPMENT_HEAD.jaali) as jaali,(TSPL_SD_SHIPMENT_HEAD.box) as box  from tspl_sd_sale_invoice_head  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=tspl_sd_sale_invoice_head.customer_code where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + "  and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "' and tspl_route_master.route_no='" & Route & "' ) as xx group by customer_code" & _
        ' " ) as TotalBoxSch on TotalBoxSch.customer_code=xx.Cust_code left join TSPL_ROUTE_CUSTOMER_SEQUENCE on TSPL_ROUTE_CUSTOMER_SEQUENCE.customer_code=xx.cust_code and TSPL_ROUTE_CUSTOMER_SEQUENCE.route_no=xx.route_no Order by TSPL_ROUTE_CUSTOMER_SEQUENCE.Sno"
        Return StrQry

    End Function
    Public Shared Function GetPrintQry(ByVal Lorry As String, ByVal Route As String, ByVal Docdate As DateTime) As String
        Dim StrQry As String = ""

        StrQry = "select  tspl_route_master.route_desc,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin , (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then AlterNateVehicle.Number   else TSPL_VEHICLE_MASTER.Number end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)  as Number,TSPL_SD_SALE_INVOICE_HEAD.Document_Date," &
                    "TSPL_SD_SALE_INVOICE_HEAD.Document_Code as BillNo, tspl_customer_master.Customer_Name as PartyName " &
                    ",TSPL_SD_SALE_INVOICE_HEAD.Crate,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt " &
                    "from TSPL_SD_SALE_INVOICE_HEAD " &
                    "left join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SD_SALE_INVOICE_HEAD.customer_code " &
                    "left join tspl_company_master on tspl_company_master.Comp_Code=TSPL_SD_SALE_INVOICE_HEAD.Comp_Code " &
                    "left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code " &
                    " left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle " &
                   " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no " &
                    " left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code " &
                   " left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no " &
                    " where TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' " &
                    "and (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "'" &
                     "and tspl_route_master.route_no='" & Route & "'" &
                    "AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " &
                    " Order by tspl_customer_master.Priority_Level desc"
        Return StrQry

    End Function
    Public Shared Function GetPrintQryOrderWise(ByVal Lorry As String, ByVal Route As String, ByVal Docdate As DateTime, ByVal DocTimeRange As String) As String
        Dim StrQry As String = ""
        '==update by preeti gupta against ticket no[ERO/28/12/18-000453,ERO/13/03/19-000512,ERO/05/04/19-000549,ERO/31/07/19-000973,ERO/01/08/19-000977,ERO/03/09/19-001015,ERO/03/09/19-001014,ERO/03/09/19-001015]
        ' Ticket No : ERO/28/01/19-000478 by Prabhakar 
        ''ERO/23/09/19-001033,ERO/23/09/19-001034 By Balwinder on 23/9/2019
        StrQry = "select XXXXXXXXXXXX.*,Grand_Total.QtyCrateForTotal,TSPL_ROUTE_CUSTOMER_SEQUENCE.SNO as Priority_Level  from ( select CAN.Qty_in_Can, tspl_item_master.Item_Code ,tspl_route_master.route_no,tspl_customer_master.Cust_Code ,  3 as Sno, 'Normal' as Invoice_Type, TSPL_SD_SALE_INVOICE_detail.unit_code ,isnull(TotalBoxSch.Jaali,0) as Jaali,isnull(TotalBoxSch.box,0) as box,isnull(TotalBoxSch.Crate,0) as Crate,  tspl_item_master.Alies_Name+char(13)+TSPL_SD_SALE_INVOICE_detail.unit_code as short_description,case when TSPL_ITEM_MASTER.SKU_Seq = 0 then 10000 else  TSPL_ITEM_MASTER.SKU_Seq end  SKU_Seq,TSPL_SD_SALE_INVOICE_detail.qty ,isnull(freeQty.free_Qty,0) as free_Qty,isnull(freeQty.Ltr_Crate_Qty,0) as Ltr_Crate_Qty,isnull(freeQty.Ltr_Crate_QtywithCeling,0) as Ltr_Crate_QtywithCeling,(isnull(freeQty.free_qty_in_Ltr,0)) as free_qty_in_Ltr,isnull(MainQtyInBox.qty_in_box,0) as Main_qty_in_box,isnull(MainQtyInCrate.qty_in_crate,0) as Main_qty_in_crate,0 as Free_qty_in_box,0 as Free_Box_Crate_qtyWithCeling,0 as Free_qty_in_crate,tspl_route_master.route_desc,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin , (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then AlterNateVehicle.Number   else TSPL_VEHICLE_MASTER.Number end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)  as Number,convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date," &
                    " TSPL_SD_SALE_INVOICE_HEAD.Document_Code as BillNo, tspl_customer_master.Customer_Name as PartyName " &
                    ",TSPL_SD_SALE_INVOICE_HEAD.Crate as Crate1 ,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt,'' as Scheme_short_description,0 as Scheme_short_description_Qty,Box_Crate_Qty,Box_Crate_QtywithCeling " &
                    "from TSPL_SD_SALE_INVOICE_HEAD " &
                    " left join TSPL_SD_SALE_INVOICE_detail on  TSPL_SD_SALE_INVOICE_detail.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code" &
                    " left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_INVOICE_detail.item_code" &
                    " left join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SD_SALE_INVOICE_HEAD.customer_code " &
                    "left join tspl_company_master on tspl_company_master.Comp_Code=TSPL_SD_SALE_INVOICE_HEAD.Comp_Code " &
                    " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code " &
                    " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no " &
                        " left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle" &
                    " left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code " &
                    " left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no " &
                    " left join (select sum(free_qty) as free_qty,sum(free_qty_in_Ltr) as free_qty_in_Ltr,sum(Ltr_Crate_Qty)as Ltr_Crate_Qty,round(sum(Ltr_Crate_QtywithCeling),0) as Ltr_Crate_QtywithCeling,Document_Code from (select sum(free_qty) as free_qty,sum(Ltr_Qty) as free_qty_in_Ltr,sum(Ltr_Crate_Qty)as Ltr_Crate_Qty,round(sum(Ltr_Crate_Qty),0) as Ltr_Crate_QtywithCeling,max(Document_Code) as Document_Code from (" &
                    " select LtrUnit.uom_code,LtrUnit.conversion_factor as LtrUnitCF,StockUnit.conversion_factor as StockUnitCF,CurrentUnit.conversion_factor as CurrentUnitCF,TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item" &
                    " ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Ltr_Qty " &
                   " ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Ltr_Crate_Qty" &
                    " ,'' as Scheme_short_description,0 as Scheme_short_description_Qty from TSPL_SD_SALE_INVOICE_detail" &
                    " left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code" &
                    " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code " &
                    " left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle" &
                    " left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no " &
                    " left join tspl_item_uom_detail LtrUnit on LtrUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" &
                    " left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=LtrUnit.uom_code " &
                    " left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" &
                    " left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code" &
                    " and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code" &
                   " left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code  and 	CrateUnit.uom_code=	'Crate'" &
                    " where Scheme_item='Y'  and TSPL_SD_SALE_INVOICE_HEAD.isSampling =0 and tspl_unit_master.ltr_Type ='Y' and StockUnit.stocking_unit='Y'" &
                    " and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' " &
                    " and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "'" &
                    " and tspl_route_master.route_no='" & Route & "'" &
                    " AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103)  " + DocTimeRange + " " &
                    " ) as dd group by Sch_item ) as Free_Qty_In_Crate group by Free_Qty_In_Crate.document_code) as freeQty on freeQty.Document_Code=TSPL_SD_SALE_INVOICE_detail.Document_Code" &
                    "  " &
                    "  " &
                    " left join (select sum(free_qty) as free_qty,sum(Ltr_Qty) as free_qty_in_KG,Document_Code from (" &
                    " select LtrUnit.uom_code,LtrUnit.conversion_factor as LtrUnitCF,StockUnit.conversion_factor as StockUnitCF,CurrentUnit.conversion_factor as CurrentUnitCF,TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item" &
                    " ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Ltr_Qty " &
                    " ,'' as Scheme_short_description,0 as Scheme_short_description_Qty from TSPL_SD_SALE_INVOICE_detail" &
                    " left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code" &
                    " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code " &
                    " left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle" &
                    " left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no " &
                    " left join tspl_item_uom_detail LtrUnit on LtrUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" &
                    " left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=LtrUnit.uom_code " &
                    " left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" &
                    " left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code" &
                    " and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code" &
                    " where Scheme_item='Y'  and TSPL_SD_SALE_INVOICE_HEAD.isSampling =0 and  tspl_unit_master.unit_code='KG'  and StockUnit.stocking_unit='Y'" &
                    " and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' " &
                    "and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "'" &
                     "and tspl_route_master.route_no='" & Route & "'" &
                    "AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + " " &
                    " ) as Free_Qty_In_Crate group by Free_Qty_In_Crate.document_code) as freeQtyInkg on freeQtyInkg.Document_Code=TSPL_SD_SALE_INVOICE_detail.Document_Code" &
                    " left join (select sum(free_qty) as free_qty,sum(Ltr_Qty) as free_qty_in_TIN,Document_Code from (" &
                    " select LtrUnit.uom_code,LtrUnit.conversion_factor as LtrUnitCF,StockUnit.conversion_factor as StockUnitCF,CurrentUnit.conversion_factor as CurrentUnitCF,TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item" &
                    " ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Ltr_Qty " &
                    " ,'' as Scheme_short_description,0 as Scheme_short_description_Qty from TSPL_SD_SALE_INVOICE_detail" &
                    " left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code" &
                    " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code " &
                    " left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle" &
                    " left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no " &
                    " left join tspl_item_uom_detail LtrUnit on LtrUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" &
                    " left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=LtrUnit.uom_code " &
                    " left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" &
                    " left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code" &
                    " and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code" &
                    " where Scheme_item='Y'  and TSPL_SD_SALE_INVOICE_HEAD.isSampling =0 and  tspl_unit_master.unit_code='Tin'  and StockUnit.stocking_unit='Y'" &
                    " and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' " &
                    "and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "'" &
                     "and tspl_route_master.route_no='" & Route & "'" &
                    "AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + " " &
                    " ) as Free_Qty_In_Crate group by Free_Qty_In_Crate.document_code) as freeQtyInTin on freeQtyInTin.Document_Code=TSPL_SD_SALE_INVOICE_detail.Document_Code" &
                    " left join (select sum(crate_qty) as qty_in_crate,Document_Code from (" &
                    " select CrateUnit.uom_code,CrateUnit.conversion_factor as CrateUnitCF,StockUnit.conversion_factor as StockUnitCF,CurrentUnit.conversion_factor as CurrentUnitCF,TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item" &
                    " ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Crate_Qty " &
                    " from TSPL_SD_SALE_INVOICE_detail" &
                    " left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code" &
                    " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code " &
                    " left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle" &
                    " left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no " &
                    " left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" &
                    " left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=CrateUnit.uom_code " &
                    " left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" &
                    " left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code" &
                    " and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code" &
                    " where Scheme_item='N'  and TSPL_SD_SALE_INVOICE_HEAD.isSampling =0 and tspl_unit_master.Crate_Type ='Y' and StockUnit.stocking_unit='Y'" &
                    " and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' " &
                    "and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "'" &
                     "and tspl_route_master.route_no='" & Route & "'" &
                    "AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + " " &
                    " ) as Free_Qty_In_Crate group by Free_Qty_In_Crate.document_code) as MainQtyInCrate on MainQtyInCrate.Document_Code=TSPL_SD_SALE_INVOICE_detail.Document_Code" &
                                        " left join (select sum(Box_qty) as qty_in_box,Document_Code,sum(Box_Crate_Qty)as Box_Crate_Qty,ceiling(sum(Box_Crate_QtywithCeling)) as Box_Crate_QtywithCeling from ( select Sch_Item, sum(Box_qty) as Box_qty,max(Document_Code) as Document_Code,sum(Box_Crate_Qty) as Box_Crate_Qty, ceiling(sum(Box_Crate_Qty)) as Box_Crate_QtywithCeling from (" &
                    " select BoxUnit.uom_code,BoxUnit.conversion_factor as BoxUnitCF,StockUnit.conversion_factor as StockUnitCF,CurrentUnit.conversion_factor as CurrentUnitCF,TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item" &
                    " ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/BoxUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Box_Qty " &
                    " ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Box_Crate_Qty  " &
                    " from TSPL_SD_SALE_INVOICE_detail" &
                    " left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code" &
                    " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code " &
                    " left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle" &
                    " left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no " &
                    " left join tspl_item_uom_detail BoxUnit on BoxUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" &
                    " left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=BoxUnit.uom_code " &
                    " left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" &
                    " left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code" &
                    " and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code" &
                    " left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code " &
                    " and 	CrateUnit.uom_code=	'Crate'" &
                    " where Scheme_item='N'  and TSPL_SD_SALE_INVOICE_HEAD.isSampling =0 and tspl_unit_master.box_type ='Y' and StockUnit.stocking_unit='Y'" &
                    " and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' " &
                    " and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "'" &
                    " and tspl_route_master.route_no='" & Route & "'" &
                    " AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + " " &
                    " ) as dd group by Sch_item ) as Qty_In_Box group by Qty_In_Box.document_code) as MainQtyInBox on MainQtyInBox.Document_Code=TSPL_SD_SALE_INVOICE_detail.Document_Code" &
                    " left join (select Document_Code, sum(Box) as Box,sum(jaali) as Jaali,sum(Crate) as Crate from (select TSPL_SD_SHIPMENT_HEAD.crate,tspl_sd_sale_invoice_head.document_code,TSPL_SD_SHIPMENT_HEAD.customer_code,(TSPL_SD_SHIPMENT_HEAD.jaali) as jaali,(TSPL_SD_SHIPMENT_HEAD.box) as box  from tspl_sd_sale_invoice_head  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=tspl_sd_sale_invoice_head.customer_code where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103)  " + DocTimeRange + "  and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "' and tspl_route_master.route_no='" & Route & "') as xx group by xx.Document_Code  ) as TotalBoxSch on TotalBoxSch.Document_Code=TSPL_SD_SALE_INVOICE_head.Document_Code" &
                    " " &
                    " left join  (select TSPL_ROUTE_CUSTOMER_SEQUENCE.SNO,TSPL_ROUTE_CUSTOMER_SEQUENCE.CUSTOMER_CODE from  TSPL_ROUTE_CUSTOMER_SEQUENCE inner join tspl_route_master on tspl_route_master.route_no =TSPL_ROUTE_CUSTOMER_SEQUENCE. route_no where TSPL_ROUTE_CUSTOMER_SEQUENCE. route_no ='" & Route & "' ) as TBL_ROUTE_WISE_CUSTOMER_SEQUENCE on TBL_ROUTE_WISE_CUSTOMER_SEQUENCE.CUSTOMER_CODE = TSPL_SD_SALE_INVOICE_HEAD.customer_code " &
                    " left join (select sum(CAN_Qty) as Qty_in_Can,Document_Code from ( select CANUnit.uom_code,CANUnit.conversion_factor as CANUnitCF,StockUnit.conversion_factor as StockUnitCF,CurrentUnit.conversion_factor as CurrentUnitCF,TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item ," &
                     " convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CANUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as CAN_Qty " &
                    " from TSPL_SD_SALE_INVOICE_detail left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no  left join tspl_item_uom_detail CANUnit on CANUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=CANUnit.uom_code  left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code where Scheme_item='N'  and TSPL_SD_SALE_INVOICE_HEAD.isSampling =0 and tspl_unit_master.CAN_Type ='Y' and StockUnit.stocking_unit='Y' and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "' and tspl_route_master.route_no='" & Route & "' AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103)  " + DocTimeRange + "   ) as xx group by xx.document_code) as CAN on CAN.Document_Code=TSPL_SD_SALE_INVOICE_detail.Document_Code " &
                    " where TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and Scheme_item='N'  and TSPL_SD_SALE_INVOICE_HEAD.isSampling =0 " &
                    " and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "'" &
                    " and tspl_route_master.route_no='" & Route & "'" &
                    " AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + " "
        StrQry += Environment.NewLine + Environment.NewLine + " Union All " + Environment.NewLine + Environment.NewLine
        StrQry += " select CAN.Qty_in_Can, tspl_item_master.Item_Code ,tspl_route_master.route_no,tspl_customer_master.Cust_Code , 2 as Sno,'Sample' as Invoice_Type, TSPL_SD_SALE_INVOICE_detail.unit_code ,isnull(TotalBoxSch.Jaali,0) as Jaali,isnull(TotalBoxSch.box,0) as box,isnull(TotalBoxSch.Crate,0) as Crate,  tspl_item_master.Alies_Name+char(13)+TSPL_SD_SALE_INVOICE_detail.unit_code + ' (S)' as short_description,case when TSPL_ITEM_MASTER.SKU_Seq = 0 then 10000 else  TSPL_ITEM_MASTER.SKU_Seq end  SKU_Seq,TSPL_SD_SALE_INVOICE_detail.qty ,isnull(freeQty.free_Qty,0) as free_Qty,isnull(freeQty.Ltr_Crate_Qty,0) as Ltr_Crate_Qty,isnull(freeQty.Ltr_Crate_QtywithCeling,0) as Ltr_Crate_QtywithCeling,(isnull(freeQty.free_qty_in_Ltr,0)) as free_qty_in_Ltr,isnull(MainQtyInBox.qty_in_box,0) as Main_qty_in_box,isnull(MainQtyInCrate.qty_in_crate,0) as Main_qty_in_crate,0 as Free_qty_in_box,0 as Free_Crate_Box_qtyWithCeling,0 as Free_qty_in_crate,tspl_route_master.route_desc,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin , (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then AlterNateVehicle.Number   else TSPL_VEHICLE_MASTER.Number end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)  as Number,convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date, TSPL_SD_SALE_INVOICE_HEAD.Document_Code as BillNo, tspl_customer_master.Customer_Name as PartyName ,TSPL_SD_SALE_INVOICE_HEAD.Crate as Crate1 ,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt,'' as Scheme_short_description,0 as Scheme_short_description_Qty,Box_Crate_Qty ,Box_Crate_QtywithCeling from TSPL_SD_SALE_INVOICE_HEAD  " &
                  " left join TSPL_SD_SALE_INVOICE_detail on  TSPL_SD_SALE_INVOICE_detail.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " &
                  " left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_INVOICE_detail.item_code left join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SD_SALE_INVOICE_HEAD.customer_code " &
                  " left join tspl_company_master on tspl_company_master.Comp_Code=TSPL_SD_SALE_INVOICE_HEAD.Comp_Code " &
                  " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code " &
" left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no " &
 " left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle" &
                  " left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code " &
                  " left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no " &
                  " left join (select sum(free_qty) as free_qty,sum(free_qty_in_Ltr) as free_qty_in_Ltr,sum(Ltr_Crate_Qty)as Ltr_Crate_Qty,round(sum(Ltr_Crate_QtywithCeling),0) as Ltr_Crate_QtywithCeling,(Document_Code) as Document_Code from (select sum(free_qty) as free_qty,sum(Ltr_Qty) as free_qty_in_Ltr,sum(Ltr_Crate_Qty)as Ltr_Crate_Qty,round(sum(Ltr_Crate_Qty),0) as Ltr_Crate_QtywithCeling,max(Document_Code) as Document_Code from ( " &
                  " select LtrUnit.uom_code,LtrUnit.conversion_factor as LtrUnitCF,StockUnit.conversion_factor as StockUnitCF,CurrentUnit.conversion_factor as CurrentUnitCF,TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Ltr_Qty  ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Ltr_Crate_Qty  ,'' as Scheme_short_description,0 as Scheme_short_description_Qty from TSPL_SD_SALE_INVOICE_detail left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no  left join tspl_item_uom_detail LtrUnit on LtrUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=LtrUnit.uom_code  left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code " &
                  " left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle" &
                  " left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code  and 	CrateUnit.uom_code=	'Crate'" &
                  " where Scheme_item='Y' and TSPL_SD_SALE_INVOICE_HEAD.isSampling=1 and tspl_unit_master.ltr_Type ='Y' and StockUnit.stocking_unit='Y' and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' " &
                  " and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "' and tspl_route_master.route_no='" & Route & "' AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103)  " + DocTimeRange + " ) as dd group by Sch_item) as Free_Qty_In_Crate group by Free_Qty_In_Crate.document_code) as freeQty on freeQty.Document_Code=TSPL_SD_SALE_INVOICE_detail.Document_Code " &
                  " left join (select sum(free_qty) as free_qty,sum(Ltr_Qty) as free_qty_in_KG,Document_Code from ( select LtrUnit.uom_code,LtrUnit.conversion_factor as LtrUnitCF,StockUnit.conversion_factor as StockUnitCF,CurrentUnit.conversion_factor as CurrentUnitCF,TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ," &
                  " TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Ltr_Qty  ,'' as Scheme_short_description,0 as Scheme_short_description_Qty from TSPL_SD_SALE_INVOICE_detail left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code  " &
                  " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no  " &
                  " left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle" &
                  " left join tspl_item_uom_detail LtrUnit on LtrUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=LtrUnit.uom_code  left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code " &
                  " left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code where Scheme_item='Y' and TSPL_SD_SALE_INVOICE_HEAD.isSampling=1 and  tspl_unit_master.unit_code='KG'  and StockUnit.stocking_unit='Y' and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' " &
                  " and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "' and tspl_route_master.route_no='" & Route & "' AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103)  " + DocTimeRange + " ) as Free_Qty_In_Crate group by Free_Qty_In_Crate.document_code) as freeQtyInkg on freeQtyInkg.Document_Code=TSPL_SD_SALE_INVOICE_detail.Document_Code " &
                  " left join (select sum(free_qty) as free_qty,sum(Ltr_Qty) as free_qty_in_TIN,Document_Code from ( select LtrUnit.uom_code,LtrUnit.conversion_factor as LtrUnitCF,StockUnit.conversion_factor as StockUnitCF,CurrentUnit.conversion_factor as CurrentUnitCF,TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Ltr_Qty  ,'' as Scheme_short_description,0 as Scheme_short_description_Qty from TSPL_SD_SALE_INVOICE_detail  " &
                  " left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  " &
                  " left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle" &
                  " left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no  left join tspl_item_uom_detail LtrUnit on LtrUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code " &
                  " left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=LtrUnit.uom_code  left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code where Scheme_item='Y' and TSPL_SD_SALE_INVOICE_HEAD.isSampling =1 and  tspl_unit_master.unit_code='Tin'  and StockUnit.stocking_unit='Y' and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' " &
                  " and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "' and tspl_route_master.route_no='" & Route & "' AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + " ) as Free_Qty_In_Crate group by Free_Qty_In_Crate.document_code) as freeQtyInTin on freeQtyInTin.Document_Code=TSPL_SD_SALE_INVOICE_detail.Document_Code " &
                  " left join (select sum(crate_qty) as qty_in_crate,Document_Code from ( select CrateUnit.uom_code,CrateUnit.conversion_factor as CrateUnitCF,StockUnit.conversion_factor as StockUnitCF,CurrentUnit.conversion_factor as CurrentUnitCF,TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Crate_Qty  from TSPL_SD_SALE_INVOICE_detail " &
                  " left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  " &
                  " left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle" &
                  " left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no  left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" &
                  " left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=CrateUnit.uom_code  left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code where Scheme_item='N' and TSPL_SD_SALE_INVOICE_HEAD.isSampling =1 and tspl_unit_master.Crate_Type ='Y' and StockUnit.stocking_unit='Y' and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' " &
                  " and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "' and tspl_route_master.route_no='" & Route & "'  AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + " ) as Free_Qty_In_Crate group by Free_Qty_In_Crate.document_code) as MainQtyInCrate on MainQtyInCrate.Document_Code=TSPL_SD_SALE_INVOICE_detail.Document_Code " &
                  " left join (select sum(Box_qty) as qty_in_box,Document_Code,sum(Box_Crate_Qty)as Box_Crate_Qty,ceiling(sum(Box_Crate_QtywithCeling)) as Box_Crate_QtywithCeling from (select Sch_Item, sum(Box_qty) as Box_qty,max(Document_Code) as Document_Code,sum(Box_Crate_Qty) as Box_Crate_Qty, ceiling(sum(Box_Crate_Qty)) as Box_Crate_QtywithCeling from ( select BoxUnit.uom_code,BoxUnit.conversion_factor as BoxUnitCF,StockUnit.conversion_factor as StockUnitCF,CurrentUnit.conversion_factor as CurrentUnitCF,TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/BoxUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Box_Qty  ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Box_Crate_Qty   from TSPL_SD_SALE_INVOICE_detail " &
                  " left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no  left join tspl_item_uom_detail BoxUnit on BoxUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code " &
                  " left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle" &
                  "	left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=BoxUnit.uom_code  left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code  and 	CrateUnit.uom_code=	'Crate' where Scheme_item='N' and TSPL_SD_SALE_INVOICE_HEAD.isSampling =1 and tspl_unit_master.box_type ='Y' and StockUnit.stocking_unit='Y' and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS'  " &
                  " and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "' and tspl_route_master.route_no='" & Route & "' AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + " )  as dd group by Sch_item  )as Qty_In_Box group by Qty_In_Box.document_code) as MainQtyInBox on MainQtyInBox.Document_Code=TSPL_SD_SALE_INVOICE_detail.Document_Code left join (select Document_Code, sum(Box) as Box,sum(jaali) as Jaali,sum(Crate) as Crate from (select TSPL_SD_SHIPMENT_HEAD.crate,tspl_sd_sale_invoice_head.document_code,TSPL_SD_SHIPMENT_HEAD.customer_code,(TSPL_SD_SHIPMENT_HEAD.jaali) as jaali,(TSPL_SD_SHIPMENT_HEAD.box) as box  from tspl_sd_sale_invoice_head  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no " &
                  " left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=tspl_sd_sale_invoice_head.customer_code where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + "   and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' " &
                  " and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "' and tspl_route_master.route_no='" & Route & "' ) as xx group by xx.Document_Code  ) as TotalBoxSch on TotalBoxSch.Document_Code=TSPL_SD_SALE_INVOICE_head.Document_Code " &
                  " " &
                  " left join  (select TSPL_ROUTE_CUSTOMER_SEQUENCE.SNO,TSPL_ROUTE_CUSTOMER_SEQUENCE.CUSTOMER_CODE from  TSPL_ROUTE_CUSTOMER_SEQUENCE inner join tspl_route_master on tspl_route_master.route_no =TSPL_ROUTE_CUSTOMER_SEQUENCE. route_no where TSPL_ROUTE_CUSTOMER_SEQUENCE. route_no ='" & Route & "' ) as TBL_ROUTE_WISE_CUSTOMER_SEQUENCE on TBL_ROUTE_WISE_CUSTOMER_SEQUENCE.CUSTOMER_CODE = TSPL_SD_SALE_INVOICE_HEAD.customer_code " &
                  " left join (select sum(CAN_Qty) as Qty_in_Can,Document_Code from ( select CANUnit.uom_code,CANUnit.conversion_factor as CANUnitCF,StockUnit.conversion_factor as StockUnitCF,CurrentUnit.conversion_factor as CurrentUnitCF,TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item ," &
                     " convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CANUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as CAN_Qty " &
                    " from TSPL_SD_SALE_INVOICE_detail left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no  left join tspl_item_uom_detail CANUnit on CANUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=CANUnit.uom_code  left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code where Scheme_item='Y'  and TSPL_SD_SALE_INVOICE_HEAD.isSampling =1 and tspl_unit_master.CAN_Type ='Y' and StockUnit.stocking_unit='Y' and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "' and tspl_route_master.route_no='" & Route & "' AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103)  " + DocTimeRange + "   ) as xx group by xx.document_code) as CAN on CAN.Document_Code=TSPL_SD_SALE_INVOICE_detail.Document_Code " &
                  " where TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and Scheme_item='Y' and TSPL_SD_SALE_INVOICE_HEAD.isSampling =1 " &
                  " and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "' and tspl_route_master.route_no='" & Route & "' AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + "  "
        StrQry += Environment.NewLine + Environment.NewLine + " Union All " + Environment.NewLine + Environment.NewLine
        StrQry += " select CAN.Qty_in_Can, tspl_item_master.Item_Code ,tspl_route_master.route_no,tspl_customer_master.Cust_Code , 1 as Sno,'Free' as Invoice_Type,TSPL_SD_SALE_INVOICE_detail.unit_code, isnull(TotalBoxSch.Jaali,0) as Jaali,isnull(TotalBoxSch.box,0) as box,isnull(TotalBoxSch.Crate,0) as Crate,   tspl_item_master.Alies_Name+char(13)+TSPL_SD_SALE_INVOICE_detail.unit_code + ' (F)' as short_description,case when TSPL_ITEM_MASTER.SKU_Seq = 0 then 10000 else  TSPL_ITEM_MASTER.SKU_Seq end  SKU_Seq ,TSPL_SD_SALE_INVOICE_detail.qty,isnull(freeQty.free_Qty,0) as free_Qty,isnull(freeQty.Ltr_Crate_Qty,0) as Ltr_Crate_Qty,isnull(freeQty.Ltr_Crate_QtywithCeling,0) as Ltr_Crate_QtywithCeling,(isnull(freeQty.free_qty_in_Ltr,0)) as free_qty_in_Ltr,0 as Main_qty_in_box,0 as Main_qty_in_crate ,isnull(FreeQtyInBox.qty_in_box,0) as Free_qty_in_box,isnull(FreeQtyInBox.Box_Crate_qtyWithCeling,0) as Free_crate_Box_qtyWithCeling,isnull(FreeQtyInBox.Box_Crate_Qty,0) as Free_Box_qty_in_crate,tspl_route_master.route_desc,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin , (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then AlterNateVehicle.Number   else TSPL_VEHICLE_MASTER.Number end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)  as Number,convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date, TSPL_SD_SALE_INVOICE_HEAD.Document_Code as BillNo, tspl_customer_master.Customer_Name as PartyName ,TSPL_SD_SALE_INVOICE_HEAD.Crate as Crate1,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt "
        StrQry += " ,tspl_item_master.Alies_Name+char(10)+TSPL_SD_SALE_INVOICE_detail.unit_code + ' (F)' as Scheme_short_description ,isnull(freeQty.free_Qty,0) as Scheme_short_description_Qty,0 as Box_Crate_Qty ,0 as Box_Crate_QtywithCeling"
        StrQry += "  from TSPL_SD_SALE_INVOICE_HEAD  left join TSPL_SD_SALE_INVOICE_detail on  TSPL_SD_SALE_INVOICE_detail.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code  left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_INVOICE_detail.item_code  left join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SD_SALE_INVOICE_HEAD.customer_code  left join tspl_company_master on tspl_company_master.Comp_Code=TSPL_SD_SALE_INVOICE_HEAD.Comp_Code  left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no  left join  (select sum(free_qty) as free_qty,sum(free_qty_in_Ltr) as free_qty_in_Ltr,sum(Ltr_Crate_Qty)as Ltr_Crate_Qty,round(sum(Ltr_Crate_Qty),0) as Ltr_Crate_QtywithCeling,Document_Code from (select sum(free_qty) as free_qty,sum(Ltr_Qty) as free_qty_in_Ltr,sum(Ltr_Crate_Qty)as Ltr_Crate_Qty,round(sum(Ltr_Crate_Qty),0) as Ltr_Crate_QtywithCeling,max(Document_Code) as Document_Code from ( select LtrUnit.uom_code,LtrUnit.conversion_factor as LtrUnitCF,StockUnit.conversion_factor as StockUnitCF,CurrentUnit.conversion_factor as CurrentUnitCF,TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Ltr_Qty ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Ltr_Crate_Qty   from TSPL_SD_SALE_INVOICE_detail left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no  left join tspl_item_uom_detail LtrUnit on LtrUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=LtrUnit.uom_code  left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code  and 	CrateUnit.uom_code=	'Crate' where Scheme_item='Y' and tspl_unit_master.ltr_Type ='Y' and StockUnit.stocking_unit='Y' and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "'and tspl_route_master.route_no='" & Route & "'AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" & Docdate & "',103) " + DocTimeRange + " ) as dd group by Sch_item ) as Free_Qty_In_Crate group by Free_Qty_In_Crate.document_code) as freeQty on freeQty.Document_Code=TSPL_SD_SALE_INVOICE_detail.Document_Code " &
            " left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle" &
        " left join (select sum(free_qty) as free_qty,sum(Ltr_Qty) as free_qty_in_KG,Document_Code from (" &
                    " select LtrUnit.uom_code,LtrUnit.conversion_factor as LtrUnitCF,StockUnit.conversion_factor as StockUnitCF,CurrentUnit.conversion_factor as CurrentUnitCF,TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item" &
                    " ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Ltr_Qty " &
                    " ,'' as Scheme_short_description,0 as Scheme_short_description_Qty from TSPL_SD_SALE_INVOICE_detail" &
                    " left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code" &
                    " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code " &
                    " left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle" &
                    " left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no " &
                    " left join tspl_item_uom_detail LtrUnit on LtrUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" &
                    " left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=LtrUnit.uom_code " &
                    " left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" &
                    " left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code" &
                    " and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code" &
                    " where Scheme_item='Y'  and TSPL_SD_SALE_INVOICE_HEAD.isSampling =0 and  tspl_unit_master.unit_code='KG'  and StockUnit.stocking_unit='Y'" &
                    " and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' " &
                    "and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "'" &
                     "and tspl_route_master.route_no='" & Route & "'" &
                    "AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + " " &
                    " ) as Free_Qty_In_Crate group by Free_Qty_In_Crate.document_code) as freeQtyInkg on freeQtyInkg.Document_Code=TSPL_SD_SALE_INVOICE_detail.Document_Code" &
                    " left join (select sum(free_qty) as free_qty,sum(Ltr_Qty) as free_qty_in_TIN,Document_Code from (" &
                    " select LtrUnit.uom_code,LtrUnit.conversion_factor as LtrUnitCF,StockUnit.conversion_factor as StockUnitCF,CurrentUnit.conversion_factor as CurrentUnitCF,TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item" &
                    " ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Ltr_Qty " &
                    " ,'' as Scheme_short_description,0 as Scheme_short_description_Qty from TSPL_SD_SALE_INVOICE_detail" &
                    " left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code" &
                    " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code " &
                    " left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle" &
                    " left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no " &
                    " left join tspl_item_uom_detail LtrUnit on LtrUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" &
                    " left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=LtrUnit.uom_code " &
                    " left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	" &
                    " left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code" &
                    " and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code" &
                    " where Scheme_item='Y'  and TSPL_SD_SALE_INVOICE_HEAD.isSampling =0 and  tspl_unit_master.unit_code='Tin'  and StockUnit.stocking_unit='Y'" &
                    " and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' " &
                    "and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "'" &
                     "and tspl_route_master.route_no='" & Route & "'" &
                    "AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + " " &
                    " ) as Free_Qty_In_Crate group by Free_Qty_In_Crate.document_code) as freeQtyInTin on freeQtyInTin.Document_Code=TSPL_SD_SALE_INVOICE_detail.Document_Code"
        StrQry += " left join (select sum(crate_qty) as qty_in_crate,Document_Code from ( select CrateUnit.uom_code,CrateUnit.conversion_factor as CrateUnitCF,StockUnit.conversion_factor as StockUnitCF,CurrentUnit.conversion_factor as CurrentUnitCF,TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Crate_Qty  from TSPL_SD_SALE_INVOICE_detail left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no  left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=CrateUnit.uom_code  left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code where Scheme_item='Y' and tspl_unit_master.Crate_Type ='Y' and StockUnit.stocking_unit='Y' and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "' and tspl_route_master.route_no='" & Route & "' AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" & Docdate & "',103) " + DocTimeRange + "  ) as Free_Qty_In_Crate group by Free_Qty_In_Crate.document_code) as FreeQtyInCrate on FreeQtyInCrate.Document_Code=TSPL_SD_SALE_INVOICE_detail.Document_Code  "
        StrQry += " left join (select sum(Box_qty) as qty_in_box,Document_Code,sum(Box_Crate_qtyWithCeling) as Box_Crate_qtyWithCeling,sum(Box_Crate_Qty) as Box_Crate_Qty from (select ceiling(sum(Box_qty)) as Box_qty,max(Document_Code) as Document_Code,ceiling(sum(Box_Crate_Qty)) as Box_Crate_qtyWithCeling,sum(Box_Crate_Qty) as Box_Crate_Qty  from ( select BoxUnit.uom_code,BoxUnit.conversion_factor as BoxUnitCF,StockUnit.conversion_factor as StockUnitCF,CurrentUnit.conversion_factor as CurrentUnitCF,TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/BoxUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Box_Qty ,convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CrateUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Box_Crate_Qty  from TSPL_SD_SALE_INVOICE_detail left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no  left join tspl_item_uom_detail BoxUnit on BoxUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=BoxUnit.uom_code  left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code  and 	CrateUnit.uom_code=	'Crate' where Scheme_item='Y' and tspl_unit_master.box_type ='Y' and StockUnit.stocking_unit='Y' and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "' and tspl_route_master.route_no='" & Route & "' AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" & Docdate & "',103) " + DocTimeRange + " )as dd group by Sch_item ) as Qty_In_Box group by Qty_In_Box.document_code) as FreeQtyInBox on FreeQtyInBox.Document_Code=TSPL_SD_SALE_INVOICE_detail.Document_Code "
        StrQry += " left join (select Document_Code, sum(Box) as Box,sum(jaali) as Jaali,sum(Crate) as Crate from (select TSPL_SD_SHIPMENT_HEAD.crate,tspl_sd_sale_invoice_head.document_code,TSPL_SD_SHIPMENT_HEAD.customer_code,(TSPL_SD_SHIPMENT_HEAD.jaali) as jaali,(TSPL_SD_SHIPMENT_HEAD.box) as box  from tspl_sd_sale_invoice_head  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=tspl_sd_sale_invoice_head.customer_code where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" & Docdate & "',103)  " + DocTimeRange + "  and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "' and tspl_route_master.route_no='" & Route & "') as xx group by xx.Document_Code  ) as TotalBoxSch on TotalBoxSch.Document_Code=TSPL_SD_SALE_INVOICE_head.Document_Code " &
                  " " &
                  " left join  (select TSPL_ROUTE_CUSTOMER_SEQUENCE.SNO,TSPL_ROUTE_CUSTOMER_SEQUENCE.CUSTOMER_CODE from  TSPL_ROUTE_CUSTOMER_SEQUENCE inner join tspl_route_master on tspl_route_master.route_no =TSPL_ROUTE_CUSTOMER_SEQUENCE. route_no where TSPL_ROUTE_CUSTOMER_SEQUENCE. route_no ='" & Route & "' ) as TBL_ROUTE_WISE_CUSTOMER_SEQUENCE on TBL_ROUTE_WISE_CUSTOMER_SEQUENCE.CUSTOMER_CODE = TSPL_SD_SALE_INVOICE_HEAD.customer_code " &
                 " left join (select sum(CAN_Qty) as Qty_in_Can,Document_Code from ( select CANUnit.uom_code,CANUnit.conversion_factor as CANUnitCF,StockUnit.conversion_factor as StockUnitCF,CurrentUnit.conversion_factor as CurrentUnitCF,TSPL_SD_SALE_INVOICE_detail.qty as free_qty,TSPL_SD_SALE_INVOICE_detail.Document_Code,TSPL_SD_SALE_INVOICE_detail.unit_code as Scheme_Uom ,TSPL_SD_SALE_INVOICE_detail.item_code as Sch_Item ," &
                     " convert(decimal(18,2),(TSPL_SD_SALE_INVOICE_detail.qty/CANUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as CAN_Qty " &
                    " from TSPL_SD_SALE_INVOICE_detail left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code  left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no  left join tspl_item_uom_detail CANUnit on CANUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=CANUnit.uom_code  left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code	 left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_detail.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_detail.unit_code where Scheme_item='Y'  and TSPL_SD_SALE_INVOICE_HEAD.isSampling =1 and tspl_unit_master.CAN_Type ='Y' and StockUnit.stocking_unit='Y' and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "' and tspl_route_master.route_no='" & Route & "' AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103)  " + DocTimeRange + "   ) as xx group by xx.document_code) as CAN on CAN.Document_Code=TSPL_SD_SALE_INVOICE_detail.Document_Code " &
                  " where TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and Scheme_item='Y'  and TSPL_SD_SALE_INVOICE_HEAD.isSampling =0 and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & Lorry & "' and tspl_route_master.route_no='" & Route & "' AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103)  " + DocTimeRange + " "
        StrQry += " ) XXXXXXXXXXXX left join TSPL_ROUTE_CUSTOMER_SEQUENCE on TSPL_ROUTE_CUSTOMER_SEQUENCE.customer_code=XXXXXXXXXXXX.cust_code and TSPL_ROUTE_CUSTOMER_SEQUENCE.route_no=XXXXXXXXXXXX.route_no  " &
                  " " &
                  " left outer join (select sum(Qty) as QtyCrateForTotal,max(unit_Code) as UOMCrateForTotal,Customer_Code from (  " &
                  " select TSPL_SD_SALE_INVOICE_head.Customer_Code , TSPL_SD_SALE_INVOICE_detail.Qty,TSPL_SD_SALE_INVOICE_detail.unit_Code,TSPL_SD_SALE_INVOICE_detail.Item_Code,TSPL_SD_SALE_INVOICE_detail.Document_Code from TSPL_SD_SALE_INVOICE_detail " &
                  " left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code  " &
                  " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no " &
                  " left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code " &
                  " left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no  " &
                  " where TSPL_SD_SALE_INVOICE_detail.unit_Code ='Crate'  and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and  " &
                  "  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)= '" & Lorry & "' and tspl_route_master.route_no='" & Route & "'  " &
                  "  AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + Docdate + "',103) " + DocTimeRange + " " &
                  " ) as Total_Qty_For_Crate " &
                  " group by  Total_Qty_For_Crate.Customer_Code ) as  Grand_Total on Grand_Total.Customer_Code=XXXXXXXXXXXX.Cust_Code  "
        '" " & _
        '"  Order by XXXXXXXXXXXX.SKU_Seq asc     "
        Return StrQry

    End Function

    Private Sub btn_TSPrint_Click(sender As Object, e As EventArgs) Handles btn_TSPrint.Click
        Try
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal Then
                Dim qry As String = " Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty , Convert (int,CrateQty) as CrateQty_New , (XXXFinal.Total) - ((StockPcs.Conversion_Factor * Convert (int,CrateQty))) as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New from ( " &
                                    " Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from ( " &
                                    " select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code, " &
                                    " sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme, " &
                                    " sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from ( " &
                                    " select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from ( " &
                                    " Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.OldName as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item " &
                                    " from TSPL_BOOKING_DETAIL " &
                                    " left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No " &
                                    " left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code " &
                                    " left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code " &
                                    " left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No " &
                                    " left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code " &
                                    " where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " &
                                    " )  Final " &
                                    " pivot " &
                                    " ( " &
                                    " max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])  " &
                                    " )as pivots " &
                                    " Union All " &
                                    " select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   isnull([Cash_Scheme],0) as [Cash_Scheme] ,isnull([CD_Scheme],0) as [CD_Scheme] ,isnull([CR_Scheme],0) as [CR_Scheme] ,isnull([SO_Scheme],0) as [SO_Scheme] ,isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from ( " &
                                    " Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item " &
                                    " from TSPL_BOOKING_DETAIL " &
                                    " left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No " &
                                    " left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code " &
                                    " left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code " &
                                    " left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No " &
                                    " left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code " &
                                    " where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " &
                                    "  )  Final " &
                                    "  pivot " &
                                    " ( " &
                                    " max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])  " &
                                    " )as pivots " &
                                    " ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code " &
                                    " ) XXFinal  " &
                                    " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code " &
                                    " left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code " &
                                    " ) XXXFinal " &
                                    " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code " &
                                    " left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code " &
                                    " "

                Dim dtMain As DataTable = clsDBFuncationality.GetDataTable(qry)
                qry = " Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty ,  'Scheme '+XXXFinal.Alies_Name as  Alies_Name_on_Name from ( " &
                      " Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from ( " &
                      " select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code, " &
                      " sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme, " &
                      " sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from ( " &
                      " select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   [Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme],isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from ( " &
                      " Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item " &
                      " from TSPL_BOOKING_DETAIL " &
                      " left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No " &
                      "  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code " &
                      "  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code " &
                      "  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No " &
                      "  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code " &
                      "  where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " &
                      "  )  Final " &
                      "  pivot " &
                      " ( " &
                      "  max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])  " &
                      " )as pivots " &
                      " ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code " &
                      "  ) XXFinal " &
                      "  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code " &
                      " left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code " &
                      " ) XXXFinal " &
                      "  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code " &
                      " left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code " &
                     " "
                Dim dtSub As DataTable = clsDBFuncationality.GetDataTable(qry)
                qry = " Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity," &
                      " (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots  " &
                      "  ) XFinal Group by  XFinal.route_no, XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code "
                Dim dtSubForRouteWise As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dtMain IsNot Nothing And dtMain.Rows.Count > 0 Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dtMain, dtSub, "rptTruckSheetReport", "Truck Sheet Report", "rptSubReportTruckSheet.rpt", "rptSubReportTruckSheetForRouteWise.rpt", dtSubForRouteWise)
                    frmCRV = Nothing
                    '=====================================================================

                    '=====================================================================
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                End If
            Else
                Dim StrQry As String = ""
                If clsCommon.myLen(txtLorryNo.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select Vehicle No", Me.Text)
                    txtLorryNo.Focus()
                    Exit Sub
                End If
                If txtRouteNo.arrValueMember Is Nothing OrElse txtRouteNo.arrValueMember.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select Route No", Me.Text)
                    txtRouteNo.Focus()
                    Exit Sub
                End If
                If dtpFromTime.Checked = True AndAlso dtpToTime.Checked = True Then
                    If clsCommon.GetPrintDate(dtpFromTime.Value, "HH:mm") > clsCommon.GetPrintDate(dtpToTime.Value, "HH:mm") Then
                        dtpFromTime.Focus()
                        Throw New Exception("From time can not be greater then To Time")
                    End If
                End If
                Dim wherTimeRange As String = ""
                Dim wherFromTime As String = ""
                Dim wherToTime As String = ""
                If dtpFromTime.Checked = True Then
                    wherFromTime = " and CONVERT(time(0),TSPL_SD_SALE_INVOICE_HEAD.Document_Date)  >= CONVERT(time(0), '" + dtpFromTime.Value + "') "
                End If
                If dtpToTime.Checked = True Then
                    wherToTime = " and CONVERT(time(0),TSPL_SD_SALE_INVOICE_HEAD.Document_Date)  <= CONVERT(time(0), '" + dtpToTime.Value + "') "
                End If
                wherTimeRange = wherFromTime + wherToTime


                StrQry = GetPrintQryChange(txtLorryNo.Value, clsCommon.GetMulcallString(txtRouteNo.arrValueMember), clsCommon.myCDate(TSP_Date.Value), wherTimeRange)
                Dim StrQry1 As String = " select xx.* from (select max(Manual_Driver_Name) as Manual_Driver_Name, max(Manual_Salesman_Name) as Manual_Salesman_Name,max(Priority_Level) as Priority_Level ,max(Driver_code) as Driver_code,max(Driver_Name) as Driver_Name,max(Route_No) as Route_No,max(route_desc) as route_desc, max(Comp_Name) as Comp_Name,max(Comp_Add1) as Comp_Add1,max(Comp_Add2) as Comp_Add2,max(Comp_Add3) as Comp_Add3, max(Comp_Pin) as Comp_Pin,max(Number) as Number, max(Document_Date) as Document_Date,BillNo ,max(cust_code) as cust_code,max(PartyName) as PartyName,sum(Crate) as Crate,max(Total_Amt) as Total_Amt, max(salesman_code) as salesman_code,sum(Crate_Qty) as Crate_Qty,sum(CAN_Qty) as CAN_Qty,sum( Box_Qty) as Box_Qty ,sum(Box_Crate_Qty) as Box_Crate_Qty  , sum(Free_Box_Crate_Qty) as Free_Box_Crate_Qty, sum(free_Qty_In_Ltr)   as free_Qty_In_Ltr,round(sum(Free_Ltr_Crate_Qty),0) as Free_Ltr_Crate_Qty from   (select max(Manual_Driver_Name) as Manual_Driver_Name, max(Manual_Salesman_Name) as Manual_Salesman_Name,max(Priority_Level) as Priority_Level " &
                        ",max(Driver_code) as Driver_code,max(Driver_Name) as Driver_Name,max(Route_No) as Route_No,max(route_desc) as route_desc," &
                        " max(Comp_Name) as Comp_Name,max(Comp_Add1) as Comp_Add1,max(Comp_Add2) as Comp_Add2,max(Comp_Add3) as Comp_Add3, max(Comp_Pin) as Comp_Pin,max(Number) as Number," &
                       " max(Document_Date) as Document_Date,BillNo ,max(cust_code) as cust_code,max(PartyName) as PartyName,sum(Crate) as Crate,max(Total_Amt) as Total_Amt," &
                       " max(salesman_code) as salesman_code,sum(Crate_Qty) as Crate_Qty,sum(CAN_Qty) as CAN_Qty,sum( Box_Qty) as Box_Qty ,sum(Box_Crate_Qty) as Box_Crate_Qty  ," &
                       " sum(Free_Box_Crate_Qty) as Free_Box_Crate_Qty, sum(free_Qty_In_Ltr)   as free_Qty_In_Ltr,round(sum(Free_Ltr_Crate_Qty),0) as Free_Ltr_Crate_Qty from "
                StrQry1 += "(" & StrQry & ")"
                StrQry1 += " as billWise group by BillNo,Item_Code ) as Billise  group by BillNo) as xx left join TSPL_ROUTE_CUSTOMER_SEQUENCE on TSPL_ROUTE_CUSTOMER_SEQUENCE.customer_code=xx.cust_code and TSPL_ROUTE_CUSTOMER_SEQUENCE.route_no=xx.route_no Order by TSPL_ROUTE_CUSTOMER_SEQUENCE.Sno "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQry1)

                ''richa agarwal 12 Nov,2019 remove round off and ceiling from selection of columns ERO/06/11/19-001089

                'Dim StrQry2 As String = "select max(Manual_Driver_Name) as Manual_Driver_Name, max(Manual_Salesman_Name) as Manual_Salesman_Name," & _
                '" max(Priority_Level) as Priority_Level " & _
                '" ,max(Driver_code) as Driver_code,max(Driver_Name) as Driver_Name,max(Route_No) as Route_No,max(route_desc) as route_desc,max(Comp_Name) as Comp_Name," & _
                '"  max(Comp_Add1) as Comp_Add1,max(Comp_Add2) as Comp_Add2,max(Comp_Add3) as Comp_Add3, max(Comp_Pin) as Comp_Pin,max(Number) as Number," & _
                ' " max(Document_Date) as Document_Date,BillNo,item_code ,max(cust_code) as cust_code,max(PartyName) as PartyName,sum(Crate) as Crate,max(Total_Amt) as Total_Amt," & _
                '" max(salesman_code) as salesman_code,sum(Crate_Qty) as Crate_Qty,sum(CAN_Qty) as CAN_Qty,sum( Box_Qty) as Box_Qty ,sum(Box_Crate_Qty) as Box_Crate_Qty ," & _
                '" ceiling(sum(Box_Crate_Qty)) as Box_Crate_QtywithCeling,sum(Free_Box_Crate_Qty) as Free_Box_Crate_Qty, " & _
                '" ceiling(sum(Free_Box_Crate_Qty)) as Free_Box_Crate_QtywithCeling," & _
                '" sum(free_Qty_In_Ltr)   as free_Qty_In_Ltr,sum(Free_Ltr_Crate_Qty) as Free_Ltr_Crate_Qty,round(sum(Free_Ltr_Crate_Qty),0) as Ltr_Crate_QtywithCeling from "

                Dim StrQry2 As String = "select max(Manual_Driver_Name) as Manual_Driver_Name, max(Manual_Salesman_Name) as Manual_Salesman_Name," &
        " max(Priority_Level) as Priority_Level " &
        " ,max(Driver_code) as Driver_code,max(Driver_Name) as Driver_Name,max(Route_No) as Route_No,max(route_desc) as route_desc,max(Comp_Name) as Comp_Name," &
        "  max(Comp_Add1) as Comp_Add1,max(Comp_Add2) as Comp_Add2,max(Comp_Add3) as Comp_Add3, max(Comp_Pin) as Comp_Pin,max(Number) as Number," &
         " max(Document_Date) as Document_Date,BillNo,item_code ,max(cust_code) as cust_code,max(PartyName) as PartyName,sum(Crate) as Crate,max(Total_Amt) as Total_Amt," &
        " max(salesman_code) as salesman_code,sum(Crate_Qty) as Crate_Qty,sum(CAN_Qty) as CAN_Qty,sum( Box_Qty) as Box_Qty ,sum(Box_Crate_Qty) as Box_Crate_Qty ," &
        " sum(isnull(Box_Crate_Qty,0)) as Box_Crate_QtywithCeling,sum(Free_Box_Crate_Qty) as Free_Box_Crate_Qty, " &
        " sum(isnull(Free_Box_Crate_Qty,0)) as Free_Box_Crate_QtywithCeling," &
        " sum(free_Qty_In_Ltr)   as free_Qty_In_Ltr,sum(Free_Ltr_Crate_Qty) as Free_Ltr_Crate_Qty,sum(isnull(Free_Ltr_Crate_Qty,0)) as Ltr_Crate_QtywithCeling from "

                StrQry2 += "(" & StrQry & ")"
                StrQry2 += " as billWise group by BillNo,Item_Code  "
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(StrQry2)

                If dt.Rows.Count > 0 Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, dt1, "rptTruckSheet", "Truck Sheet", clsCommon.myCDate(TSP_Date.Value), "SubRptforTruckSheetReport.rpt")
                    'frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptTruckSheet", "Truck Sheet", clsCommon.myCDate(TSP_Date.Value))
                    frmCRV = Nothing
                Else
                    Throw New Exception("No data found to print")
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TSP_Reset_Click(sender As Object, e As EventArgs) Handles TSP_Reset.Click
        txtLorryNo.Value = Nothing
        lblVehicleNo.Text = ""
        txtRouteNo.arrValueMember = Nothing
        'lblRouteName.Text = ""
        btn_TruckSheetGenerated.Enabled = True
        StrEarlyRoute = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @temp VARCHAR(MAX) SET @temp =(SELECT ',''' + cast(Route_No as varchar) + '''' FROM TSPL_ROUTE_MASTER where IsEarlyRoute=1 order by Route_No FOR XML PATH('')) select SUBSTRING(@temp, 2, 200000) AS Route"))
        StrNonEarlyRoute = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @temp VARCHAR(MAX) SET @temp =(SELECT ',''' + cast(Route_No as varchar) + '''' FROM TSPL_ROUTE_MASTER where IsEarlyRoute=0 order by Route_No FOR XML PATH('')) select SUBSTRING(@temp, 2, 200000) AS Route"))
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        strQry = "select Route_No  as [Code],Route_Desc as [Name] from TSPL_ROUTE_MASTER"
        txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("txtRoute@Master", strQry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
    End Sub
    Private Sub TxtRouteNo__My_Click(sender As Object, e As EventArgs) Handles txtRouteNo._My_Click
        If chkShowEarlyRoute.Checked = True Then
            strQry = "select Route_No  as [Code],Route_Desc as [Name] from TSPL_ROUTE_MASTER where IsEarlyRoute=1"
        Else
            strQry = "select Route_No  as [Code],Route_Desc as [Name] from TSPL_ROUTE_MASTER where IsEarlyRoute=0"
        End If
        txtRouteNo.arrValueMember = clsCommon.ShowMultipleSelectForm("txtRouteNo@Master", strQry, "Code", "Name", txtRouteNo.arrValueMember, txtRouteNo.arrDispalyMember)
    End Sub
    'Private Sub txtRouteNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRouteNo._MYValidating
    '    Dim wrcls As String = ""
    '    Dim qry As String = "select tspl_route_master.route_no as Code,tspl_route_master.route_desc as Name from tspl_route_master "
    '    'If clsCommon.myLen(txtLorryNo.Value) > 0 Then
    '    '    wrcls = "vehicle_code='" + txtLorryNo.Value + "'"
    '    'End If
    '    txtRouteNo.Value = clsCommon.ShowSelectForm("DSGatePassRoute", qry, "Code", wrcls, txtRouteNo.Value, "Code", isButtonClicked)
    '    If clsCommon.myLen(txtRouteNo.Value) > 0 Then
    '        lblRouteName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_route_master.route_desc  from tspl_route_master Where route_no='" + txtRouteNo.Value + "'"))
    '    Else
    '        lblRouteName.Text = ""
    '    End If
    'End Sub

    Private Sub btnordersheetprint_Click(sender As Object, e As EventArgs) Handles btnordersheetprint.Click
        Try
            Dim StrQry As String = ""
            Dim StrQry1 As String = ""
            If clsCommon.myLen(txtLorryNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Vechile No", Me.Text)
                txtLorryNo.Focus()
                Exit Sub
            End If
            If txtRouteNo.arrValueMember Is Nothing OrElse txtRouteNo.arrValueMember.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Route No", Me.Text)
                txtRouteNo.Focus()
                Exit Sub
            End If
            ' Ticket No : ERO/09/04/19-000553 by Prabhakar 
            If dtpFromTime.Checked = True AndAlso dtpToTime.Checked = True Then
                If clsCommon.GetPrintDate(dtpFromTime.Value, "HH:mm") > clsCommon.GetPrintDate(dtpToTime.Value, "HH:mm") Then
                    dtpFromTime.Focus()
                    Throw New Exception("From time can not be greater then To Time")
                End If
            End If
            '=====================================================================================================================================================
            Dim wherTimeRange As String = ""
            Dim wherFromTime As String = ""
            Dim wherToTime As String = ""
            If dtpFromTime.Checked = True Then
                wherFromTime = " and CONVERT(time(0),TSPL_SD_SALE_INVOICE_HEAD.Document_Date)  >= CONVERT(time(0), '" + dtpFromTime.Value + "') "
            End If
            If dtpToTime.Checked = True Then
                wherToTime = " and CONVERT(time(0),TSPL_SD_SALE_INVOICE_HEAD.Document_Date)  <= CONVERT(time(0), '" + dtpToTime.Value + "') "
            End If
            wherTimeRange = wherFromTime + wherToTime
            '=====================================================================================================================================================
            'StrQry = GetPrintQryChange(txtLorryNo.Value, txtRouteNo.Value, clsCommon.myCDate(TSP_Date.Value), wherTimeRange)
            StrQry = GetPrintQryChange(txtLorryNo.Value, clsCommon.GetMulcallString(txtRouteNo.arrValueMember), clsCommon.myCDate(TSP_Date.Value), wherTimeRange)
            StrQry1 = "select xx.*,TSPL_ROUTE_CUSTOMER_SEQUENCE.SNO as Priority_Level ,(Grand_Total.QtyCrateForTotal) as QtyCrateForTotal from (select sum(Crate_Qty) as Crate_Qty, (Route_No) as Route_No,(Cust_Code) as Cust_Code,  max(Sno) as Sno,  (Invoice_Type) as Invoice_Type,  max(short_description) as short_description, max(SKU_Seq) as SKU_Seq,sum(qty) as qty ,max(route_desc) as route_desc,max(Comp_Name) as Comp_Name, max(Comp_Add1) as Comp_Add1, max(Comp_Add2) as Comp_Add2,max(Comp_Add3) as Comp_Add3, max(Comp_Pin) as Comp_Pin,  max(Number) as Number, max(Document_Date) as Document_Date,  max ( PartyName) as PartyName" &
                    " from (" &
                    "" & StrQry & "" &
                    ") as final group by Cust_Code,Route_No,Item_Code ,Priority_Level ,Invoice_Type ) as xx left join TSPL_ROUTE_CUSTOMER_SEQUENCE on TSPL_ROUTE_CUSTOMER_SEQUENCE.customer_code=xx.cust_code and TSPL_ROUTE_CUSTOMER_SEQUENCE.route_no=xx.route_no  " &
                   " left outer join (select sum(Qty) as QtyCrateForTotal,max(unit_Code) as UOMCrateForTotal,Customer_Code from (  " &
                  " select TSPL_SD_SALE_INVOICE_head.Customer_Code , TSPL_SD_SALE_INVOICE_detail.Qty,TSPL_SD_SALE_INVOICE_detail.unit_Code,TSPL_SD_SALE_INVOICE_detail.Item_Code,TSPL_SD_SALE_INVOICE_detail.Document_Code from TSPL_SD_SALE_INVOICE_detail " &
                  " left join TSPL_SD_SALE_INVOICE_head on TSPL_SD_SALE_INVOICE_head.document_code=TSPL_SD_SALE_INVOICE_detail.document_code  " &
                  " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code= TSPL_SD_SALE_INVOICE_HEAD.against_shipment_no " &
                  " left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code " &
                  " left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no  " &
                  " where TSPL_SD_SALE_INVOICE_detail.unit_Code ='Crate'  and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and  " &
                  "  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)= '" & txtLorryNo.Value & "' and tspl_route_master.route_no in (" & clsCommon.GetMulcallString(txtRouteNo.arrValueMember) & ")  " &
                  "  AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + TSP_Date.Value + "',103) " + wherTimeRange + " " &
                  " ) as Total_Qty_For_Crate " &
                  " group by  Total_Qty_For_Crate.Customer_Code ) as  Grand_Total on Grand_Total.Customer_Code=xx.Cust_Code  "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQry1)

            ''richa agarwal 12 Nov,2019 remove round off and ceiling from selection of columns ERO/06/11/19-001089

            'StrQry = "select sum(CAN_Qty ) as Qty_In_CAN ,sum(Free_qty_in_ltr) as Free_qty_in_ltr," & _
            '        " sum(Ltr_Crate_Qty)as Ltr_Crate_Qty ,round(sum(Free_Ltr_Crate_Qty),0) as Free_Ltr_Crate_QtywithCeling,  sum(Main_Qty_in_Crate) as Main_Qty_in_Crate," & _
            '        " sum(free_Qty_in_Crate) as free_Qty_in_Crate, sum(Free_qty_in_box) as Free_qty_in_box,sum(main_Qty_in_box) as main_Qty_in_box,billno," & _
            '        " sum(Box_Crate_Qty) as Box_Crate_Qty,ceiling(sum(Box_Crate_Qty)) as Box_Crate_QtywithCeling," & _
            '        " ceiling(sum(Free_Box_Crate_Qty)) as Free_Box_Crate_qtyWithCeling from  (" & StrQry & ")as BillWise    group by billno,Item_Code "

            StrQry = "select sum(CAN_Qty ) as Qty_In_CAN ,sum(Free_qty_in_ltr) as Free_qty_in_ltr," &
                   " sum(Ltr_Crate_Qty)as Ltr_Crate_Qty ,sum(isnull(Free_Ltr_Crate_Qty,0)) as Free_Ltr_Crate_QtywithCeling,  sum(Main_Qty_in_Crate) as Main_Qty_in_Crate," &
                   " sum(free_Qty_in_Crate) as free_Qty_in_Crate, sum(Free_qty_in_box) as Free_qty_in_box,sum(main_Qty_in_box) as main_Qty_in_box,billno," &
                   " sum(Box_Crate_Qty) as Box_Crate_Qty,sum(isnull(Box_Crate_Qty,0)) as Box_Crate_QtywithCeling," &
                   " sum(isnull(Free_Box_Crate_Qty,0)) as Free_Box_Crate_qtyWithCeling from  (" & StrQry & ")as BillWise    group by billno,Item_Code "


            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(StrQry)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, dt1, "rptTruckSheetOrderwise", "Truck Sheet", clsCommon.myCDate(dt.Rows(0)("Document_Date")), "SubRptforTruckSheetReport.rpt")
                frmCRV = Nothing
            Else
                Throw New Exception("No data found to print")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Truck Sheet Report")

                Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
                arrHeader.Add(strtemp)

                If txtMultCustomer.arrValueMember IsNot Nothing AndAlso txtMultCustomer.arrValueMember.Count > 0 Then
                    arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtMultCustomer.arrDispalyMember))
                End If

                If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtMultLocation.arrDispalyMember))
                End If

                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    arrHeader.Add("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember))
                End If
                clsCommon.MyExportToPDF("Truck Sheet Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub WriteDataToFile(ByVal submittedDataTable As DataTable, ByVal submittedFilePath As String, Optional isGatePass As Boolean = False)
        Try
            Dim i As Integer = 0
            Dim ii As Integer = 0
            Dim iH As Integer = 0
            Dim PageNo As Integer = 1
            Dim OnePageTotalLine As Integer = 60
            Dim TotalLineNoPrint As Integer = 0
            Dim NextTotalNoOnPage As Integer = 0
            Dim PrvTotalNoOnPage As Integer = 0
            Dim sw As StreamWriter = Nothing
            sw = New StreamWriter(submittedFilePath, False)
            Dim strPrvRouteNo As String = ""
            Dim strPrvRouteDesc As String = ""
            Dim strPrvZoneCode As String = ""
            Dim strPrvZoneDesc As String = ""
            Dim strPrvCustomerNo As String = ""
            Dim strPrvCustomerDesc As String = ""
            Dim strPrvCustomerDescForRoutetotal As String = ""
            Dim strPrvDocDate As String = ""
            Dim SumOfCrate As Integer = 0
            Dim SumOfAmount As Double = 0.0
            Dim SumOfTCSAmount As Double = 0.0
            Dim SumOfTotlQty As Double = 0.0
            Dim DataSNO As String = ""
            Dim strCustomerLR_PQ_ES As String = ""
            Dim strTotalRouteLR_PQ_ES As String = ""
            Dim strTotalRouteSummary As String = ""
            Dim strXCratesRouteDetails As String = ""
            Dim rowsCount As Integer = 0
            Dim rowsCustLR_PQ_ES_Count As Integer = 0
            Dim strCustomerScheme As String = ""
            Dim rowsCustomerSchemeCount As Integer = 0
            Dim checkTotalRoutWise As Boolean = False
            Dim qry As String
            Dim dblTotalTCS As Decimal = 0

            For Each rowH As DataRow In submittedDataTable.Rows
                Dim arrayH As Object() = rowH.ItemArray
                Dim strItemCode As String = ""
                Dim strItemDesc As String = ""
                Dim strCR As String = ""
                Dim strCD As String = ""
                Dim strSO As String = ""
                Dim strCash As String = ""
                Dim strTotal As String = ""
                Dim strCrate As String = ""
                Dim strAmount As String = ""
                Dim strtotalCrateQty As Integer = 0
                Dim strTotalPendingPcsQty As Integer = 0
                Dim strTotalAmount As Double = 0.0
                Dim strTCSTotalAmount As Double = 0.0

                Dim strCurRouteNo As String = ""
                Dim strCurRouteDesc As String = ""
                Dim strCurZoneCode As String = ""
                Dim strCurZoneDesc As String = ""
                Dim strCurCustomerNo As String = ""
                Dim strCurCustomerDesc As String = ""
                Dim strCurDocDate As String = ""
                Dim strCurUnitCode As String = ""
                Dim strCurItemRowNo As String = ""
                Dim strLR_PQ_ES As String = ""
                Dim strOpencrateQty As String = ""
                Dim strCrateQtyRecd As String = ""
                Dim strCrateOutQty As String = ""
                Dim strCrateAdjQty As String = ""
                Dim strCrateQtyClosing As String = ""
                Dim strSchemeStar As String = ""
                Dim strTotal_Qty_In_LTR As Double = 0
                Dim strTotal_Qty_In_KG As Double = 0


                For iH = 0 To arrayH.Length - 1
                    If iH = 3 Then
                        strCurRouteNo = arrayH(3).ToString()
                    ElseIf iH = 4 Then
                        strCurRouteDesc = arrayH(4).ToString()
                    ElseIf iH = 5 Then
                        strCurZoneCode = arrayH(5).ToString()
                    ElseIf iH = 6 Then
                        strCurZoneDesc = arrayH(6).ToString()
                    ElseIf iH = 1 Then
                        strCurCustomerNo = arrayH(1).ToString()
                    ElseIf iH = 2 Then
                        strCurCustomerDesc = arrayH(2).ToString()
                    ElseIf iH = 0 Then
                        strCurDocDate = arrayH(0).ToString()
                    ElseIf iH = 8 Then
                        strItemDesc = arrayH(8).ToString()
                    ElseIf iH = 9 Then
                        strCR = arrayH(9).ToString()
                    ElseIf iH = 10 Then
                        strCD = arrayH(10).ToString()
                    ElseIf iH = 11 Then
                        strSO = arrayH(11).ToString()
                    ElseIf iH = 12 Then
                        strCash = arrayH(12).ToString()
                    ElseIf iH = 13 Then
                        strTotal = arrayH(13).ToString()
                    ElseIf iH = 14 Then
                        strtotalCrateQty = clsCommon.myCdbl(arrayH(14).ToString())
                    ElseIf iH = 15 Then
                        strTotalPendingPcsQty = clsCommon.myCdbl(arrayH(15).ToString())
                    ElseIf iH = 16 Then
                        strTotalAmount = clsCommon.myCdbl(arrayH(16).ToString())
                    ElseIf iH = 17 Then
                        DataSNO = arrayH(17).ToString()
                    ElseIf iH = 18 Then
                        strCurUnitCode = arrayH(18).ToString()
                    ElseIf iH = 19 Then
                        strCurItemRowNo = arrayH(19).ToString()
                    ElseIf iH = 20 Then
                        strLR_PQ_ES = arrayH(20).ToString()
                    ElseIf iH = 21 Then
                        strOpencrateQty = arrayH(21).ToString()
                    ElseIf iH = 22 Then
                        strCrateQtyRecd = arrayH(22).ToString()
                    ElseIf iH = 23 Then
                        strCrateOutQty = arrayH(23).ToString()
                    ElseIf iH = 24 Then
                        strCrateAdjQty = arrayH(24).ToString()
                    ElseIf iH = 25 Then
                        strCrateQtyClosing = arrayH(25).ToString()
                    ElseIf iH = 26 Then
                        strSchemeStar = arrayH(26).ToString()
                    ElseIf iH = 27 Then
                        strTotal_Qty_In_KG = arrayH(27).ToString()
                    ElseIf iH = 28 Then
                        strTotal_Qty_In_LTR = arrayH(28).ToString()
                    ElseIf iH = 29 Then
                        strTCSTotalAmount = clsCommon.myCdbl(arrayH(29).ToString())
                    End If
                Next
                '=============================================================================================================================
                If clsCommon.CompairString(strCurCustomerNo, strPrvCustomerNo) <> CompairStringResult.Equal Then
                    If NextTotalNoOnPage = 0 And clsCommon.CompairString(DataSNO, "1") = CompairStringResult.Equal Then
                        NextTotalNoOnPage = NextTotalNoOnPage + 3 + 2 + 3 + clsCommon.myCdbl(strCurItemRowNo) + 1 + 1 ' add 1 for Line 
                        PrvTotalNoOnPage = 3 + 2 + 3 + clsCommon.myCdbl(strCurItemRowNo) + 1 + 1 ' add 1 for Line 
                    ElseIf clsCommon.CompairString(DataSNO, "1") = CompairStringResult.Equal Then
                        NextTotalNoOnPage = NextTotalNoOnPage + 0 + 1 + clsCommon.myCdbl(strCurItemRowNo) + 1 ' 3 replace 2
                        PrvTotalNoOnPage = 0 + 1 + clsCommon.myCdbl(strCurItemRowNo) + 1  ' 3 replace 2
                    ElseIf clsCommon.CompairString(DataSNO, "2") = CompairStringResult.Equal Then
                        NextTotalNoOnPage = NextTotalNoOnPage + 1
                        PrvTotalNoOnPage = 1
                    ElseIf clsCommon.CompairString(DataSNO, "3") = CompairStringResult.Equal Then
                        NextTotalNoOnPage = NextTotalNoOnPage + 0 + 1 + clsCommon.myCdbl(strCurItemRowNo) + 1 + 6 + 2 ' 6 for additional 5 line below the route Total plus one dot line, 2 for blank line and (Q.P.S        LOADED BY          SECURITY               RMRD)
                        PrvTotalNoOnPage = 0 + 1 + clsCommon.myCdbl(strCurItemRowNo) + 1 + 6 + 2 ' 6 for additional 5 line below the route Total plus one dot line , 2 for blank line and (Q.P.S        LOADED BY          SECURITY               RMRD)
                    End If
                End If
                If clsCommon.CompairString(strCurCustomerNo, strPrvCustomerNo) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(DataSNO, "2") = CompairStringResult.Equal Then
                    NextTotalNoOnPage = NextTotalNoOnPage + clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select (" + clsCommon.myCstr(clsCommon.myCdbl(strCurItemRowNo)) + " /4) + case when  (" + clsCommon.myCstr(clsCommon.myCdbl(strCurItemRowNo)) + " %4) > 0  then 1 else 0 end"))
                    PrvTotalNoOnPage = PrvTotalNoOnPage + clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select (" + clsCommon.myCstr(clsCommon.myCdbl(strCurItemRowNo)) + " /4) + case when  (" + clsCommon.myCstr(clsCommon.myCdbl(strCurItemRowNo)) + " %4) > 0  then 1 else 0 end"))
                End If
                If clsCommon.CompairString(strCurCustomerDesc, "Customer Leakage") = CompairStringResult.Equal AndAlso clsCommon.CompairString(strCurCustomerNo, strPrvCustomerNo) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strCurCustomerDesc, strPrvCustomerDesc) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(DataSNO, "22") = CompairStringResult.Equal Then
                    NextTotalNoOnPage = NextTotalNoOnPage + clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select (" + clsCommon.myCstr(clsCommon.myCdbl(strCurItemRowNo)) + " /4) + case when  (" + clsCommon.myCstr(clsCommon.myCdbl(strCurItemRowNo)) + " %4) > 0  then 1 else 0 end")) '1
                    PrvTotalNoOnPage = PrvTotalNoOnPage + clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select (" + clsCommon.myCstr(clsCommon.myCdbl(strCurItemRowNo)) + " /4) + case when  (" + clsCommon.myCstr(clsCommon.myCdbl(strCurItemRowNo)) + " %4) > 0  then 1 else 0 end")) '1
                End If
                If NextTotalNoOnPage > OnePageTotalLine OrElse (clsCommon.CompairString(strCurRouteNo, strPrvRouteNo) <> CompairStringResult.Equal AndAlso clsCommon.myLen(strPrvRouteNo) > 0) Then
                    Dim BlankSpace As Integer = PrvTotalNoOnPage - (NextTotalNoOnPage - OnePageTotalLine)
                    TotalLineNoPrint = 0
                    NextTotalNoOnPage = PrvTotalNoOnPage + 7  ' for 3 heading Total
                    ' Frist total
                    If (SumOfAmount > 0 Or SumOfCrate > 0) Or SumOfTotlQty > 0 Then

                        qry = "select IsTCSnotApplicable,PAN from TSPL_CUSTOMER_MASTER where Cust_Code='" + strPrvCustomerNo + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        qry = ""
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            If clsCommon.myCdbl(dt.Rows(0)("IsTCSnotApplicable")) = 0 Then
                                Dim TCSAmt As Decimal = 0
                                If clsCommon.myLen(dt.Rows(0)("PAN")) > 0 Then
                                    TCSAmt = Math.Round(SumOfTCSAmount * settTCSRateforCustomerWithPanNo / 100, 0)
                                    qry = "( Including TCS Amount Rs: " + clsCommon.myCstr(TCSAmt) + " )"
                                Else
                                    TCSAmt = Math.Round(SumOfTCSAmount * settTCSRateforCustomerWithoutPanNo / 100, 0)
                                    qry = "( Including TCS Amount Rs: " + clsCommon.myCstr(TCSAmt) + " )"
                                End If
                                SumOfAmount += TCSAmt
                                dblTotalTCS += TCSAmt
                            End If
                        End If

                        If checkTotalRoutWise Then
                            If ApplyIncludeTCSAmountInRouteTotalOnTruckSheet = True Then
                                SumOfAmount += dblTotalTCS
                                strTotalAmount += dblTotalTCS
                                qry = "( Including TCS Amount Rs: " + clsCommon.myCstr(dblTotalTCS) + " )"
                            Else
                                qry = "( Route TCS Amount Rs: " + clsCommon.myCstr(dblTotalTCS) + " )"
                            End If

                            sw.Write(GetFormateColumnValue(qry, 35) + GetFormateColumnValue(SumOfCrate, 30, "R") + "-0" + GetFormateColumnValue(SumOfAmount.ToString("N2").Replace(",", String.Empty), 12, "R") + "  ")
                            dblTotalTCS = 0
                            'sw.Write(GetFormateColumnValue(SumOfCrate, 65, "R") + "-0" + GetFormateColumnValue(SumOfAmount.ToString("N2").Replace(",", String.Empty), 12, "R") + "  ")
                        Else
                            sw.Write(GetFormateColumnValue(qry, 35) + GetFormateColumnValue(SumOfCrate, 30, "R") + "-0" + GetFormateColumnValue(Math.Round(SumOfAmount, 0), 12, "R") + "  ")
                            'sw.Write(GetFormateColumnValue(SumOfCrate, 65, "R") + "-0" + GetFormateColumnValue(Math.Round(SumOfAmount, 0), 12, "R") + "  ")
                        End If
                        sw.WriteLine()
                        SumOfCrate = 0
                        SumOfAmount = 0.0
                        SumOfTCSAmount = 0.0
                        SumOfTotlQty = 0
                        If clsCommon.myLen(strCustomerScheme) > 0 Then
                            sw.Write(strCustomerScheme)
                            sw.WriteLine()
                            strCustomerScheme = ""
                            rowsCustomerSchemeCount = 0
                        End If

                        If clsCommon.myLen(strCustomerLR_PQ_ES) > 0 Then
                            sw.Write(strCustomerLR_PQ_ES)
                            sw.WriteLine()
                            strCustomerLR_PQ_ES = ""
                            rowsCustLR_PQ_ES_Count = 0
                        End If
                    End If
                    If clsCommon.myLen(strPrvRouteNo) > 0 Then
                        Dim iii As Integer = 0
                        For iii = 0 To BlankSpace - 1
                        Next
                    End If
                End If
                If TotalLineNoPrint = 0 Then ' TotalLineNoPrint = 0
                    Dim iHeader As Integer = 0
                    If PageNo <> 1 AndAlso clsCommon.CompairString(strCurCustomerNo, "Route Total") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(strPrvCustomerNo, "Route Total") <> CompairStringResult.Equal Then
                        NextTotalNoOnPage = NextTotalNoOnPage + 7 + 1 ' add 1 for Line  ' 7 for Logo space
                    End If
                    If PageNo = 1 Then
                        sw.Write(".           THE TELANGANA STATE DAIRY DEV. CO-OP. FEDN. LTD -MPF: HYDERABAD")
                    Else
                        sw.Write(".           THE TELANGANA STATE DAIRY DEV. CO-OP. FEDN. LTD -MPF: HYDERABAD")
                    End If
                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 1
                    sw.Write("Page: " + clsCommon.myCstr(PageNo) + " ")
                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 1
                    PageNo = PageNo + 1
                    sw.Write("ZONE : " + clsCommon.myCstr(strCurZoneDesc) + "                   DRIVER:            CASHIER: ")
                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 1
                    If isGatePass = True Then
                        sw.Write("ROUTE : " + clsCommon.myCstr(strCurRouteDesc) + "                " + IIf(cmbGatePassType.Text = "AM", "MORNING", "EVENING") + " TRUCK SHEET OF " + clsCommon.myCstr(strCurDocDate) + " Time :")
                    Else
                        sw.Write("ROUTE : " + clsCommon.myCstr(strCurRouteDesc) + "                MORNING TRUCK SHEET OF " + clsCommon.myCstr(strCurDocDate) + " Time :")
                    End If

                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 1
                    sw.Write("----------------------------------------------------------------------------------")
                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 1
                    For i = 0 To submittedDataTable.Columns.Count - 1
                        Dim strColumn_Name As String = ""
                        If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "Customer_Name") = CompairStringResult.Equal Then
                            strColumn_Name = "Booth Name          " '20 8+7 +4
                        End If
                        If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "Alies_Name") = CompairStringResult.Equal Then
                            strColumn_Name = "Type    " '8
                        End If
                        If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "CR") = CompairStringResult.Equal Then
                            strColumn_Name = "    CR" '7
                        End If
                        If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "CD") = CompairStringResult.Equal Then
                            strColumn_Name = "    CD" '7
                        End If
                        If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "SO") = CompairStringResult.Equal Then
                            strColumn_Name = "   S.O" '7
                        End If
                        If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "CASH") = CompairStringResult.Equal Then
                            strColumn_Name = "  Cash" '7
                        End If
                        If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "Total") = CompairStringResult.Equal Then
                            strColumn_Name = "   Total" '8
                        End If

                        If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "CrateQty_New") = CompairStringResult.Equal Then
                            strColumn_Name = "  Crates" '8
                        End If
                        If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "Amount_with_Tax") = CompairStringResult.Equal Then
                            strColumn_Name = "     Amount" '11
                        End If
                        If clsCommon.myLen(strColumn_Name) > 0 Then
                            sw.Write(strColumn_Name)
                        End If
                    Next
                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 1
                    sw.Write("----------------------------------------------------------------------------------")
                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 1
                End If
                ' Second total
                If clsCommon.CompairString(strCurCustomerNo, strPrvCustomerNo) <> CompairStringResult.Equal Then  'clsCommon.CompairString(strCurRouteNo, strPrvRouteNo) <> CompairStringResult.Equal AndAlso
                    If SumOfAmount > 0 Or SumOfTotlQty > 0 Then
                        qry = "select IsTCSnotApplicable,PAN from TSPL_CUSTOMER_MASTER where Cust_Code='" + strPrvCustomerNo + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        qry = ""
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            If clsCommon.myCdbl(dt.Rows(0)("IsTCSnotApplicable")) = 0 Then
                                Dim TCSAmt As Decimal = 0
                                If clsCommon.myLen(dt.Rows(0)("PAN")) > 0 Then
                                    TCSAmt = Math.Round(SumOfTCSAmount * settTCSRateforCustomerWithPanNo / 100, 0)
                                    qry = "( Including TCS Amount Rs: " + clsCommon.myCstr(TCSAmt) + " )"
                                Else
                                    TCSAmt = Math.Round(SumOfTCSAmount * settTCSRateforCustomerWithoutPanNo / 100, 0)
                                    qry = "( Including TCS Amount Rs: " + clsCommon.myCstr(TCSAmt) + " )"
                                End If
                                SumOfAmount += TCSAmt
                                dblTotalTCS += TCSAmt
                            End If
                        End If
                        If checkTotalRoutWise Then
                            If ApplyIncludeTCSAmountInRouteTotalOnTruckSheet = True Then
                                SumOfAmount += dblTotalTCS
                                strTotalAmount += dblTotalTCS
                                qry = "( Including TCS Amount Rs: " + clsCommon.myCstr(dblTotalTCS) + " )"
                            Else
                                qry = "( Route TCS Amount Rs: " + clsCommon.myCstr(dblTotalTCS) + " )"
                            End If
                            sw.Write(GetFormateColumnValue(qry, 35) + GetFormateColumnValue(SumOfCrate, 30, "R") + "-0" + GetFormateColumnValue(SumOfAmount.ToString("N2").Replace(",", String.Empty), 12, "R") + "  ")
                            dblTotalTCS = 0
                        Else
                            sw.Write(GetFormateColumnValue(qry, 35) + GetFormateColumnValue(SumOfCrate, 30, "R") + "-0" + GetFormateColumnValue(Math.Round(SumOfAmount, 0), 12, "R") + "  ")
                        End If
                        sw.WriteLine()
                        SumOfCrate = 0
                        SumOfAmount = 0.0
                        SumOfTCSAmount = 0
                        SumOfTotlQty = 0
                        If clsCommon.myLen(strCustomerScheme) > 0 Then
                            sw.Write(strCustomerScheme)
                            sw.WriteLine()
                            strCustomerScheme = ""
                            rowsCustomerSchemeCount = 0
                        End If
                        If clsCommon.myLen(strCustomerLR_PQ_ES) > 0 Then
                            sw.Write(strCustomerLR_PQ_ES)
                            sw.WriteLine()
                            strCustomerLR_PQ_ES = ""
                            rowsCustLR_PQ_ES_Count = 0
                        End If
                        If checkTotalRoutWise = False Then
                            sw.Write("----------------------------------------------------------------------------------")
                            sw.WriteLine()
                            TotalLineNoPrint = TotalLineNoPrint + 1
                        End If
                    End If
                End If
                If clsCommon.CompairString(DataSNO, "2") = CompairStringResult.Equal Then
                    If clsCommon.myLen(strCustomerScheme) <= 0 Then
                        strCustomerScheme = "Scheme "
                    End If
                    If rowsCustomerSchemeCount = 4 OrElse rowsCustomerSchemeCount = 8 OrElse rowsCustomerSchemeCount = 12 OrElse rowsCustomerSchemeCount = 16 OrElse rowsCustomerSchemeCount = 20 Then
                        rowsCustomerSchemeCount += Environment.NewLine
                    End If
                    If rowsCustomerSchemeCount = 0 Then
                        strCustomerScheme += "" + strItemDesc + " " + strCurUnitCode + " : " + strTotal + "" + " "
                    Else
                        strCustomerScheme += "," + strItemDesc + " " + strCurUnitCode + " : " + strTotal + "" + " "
                    End If

                    rowsCustomerSchemeCount += 1

                ElseIf clsCommon.CompairString(DataSNO, "22") = CompairStringResult.Equal Then
                    If clsCommon.myLen(strCustomerLR_PQ_ES) <= 0 Then
                        strCustomerLR_PQ_ES = "LR/PQ/ES "
                    End If
                    If rowsCustLR_PQ_ES_Count = 4 OrElse rowsCustLR_PQ_ES_Count = 8 OrElse rowsCustLR_PQ_ES_Count = 12 OrElse rowsCustLR_PQ_ES_Count = 16 OrElse rowsCustLR_PQ_ES_Count = 20 Then
                        strCustomerLR_PQ_ES += Environment.NewLine
                    End If
                    strCustomerLR_PQ_ES += "" + strItemDesc + "  : " + strLR_PQ_ES + "" + " "
                    rowsCustLR_PQ_ES_Count += 1
                ElseIf clsCommon.CompairString(DataSNO, "4") = CompairStringResult.Equal Then
                    If clsCommon.myLen(strTotalRouteLR_PQ_ES) <= 0 Then
                        strTotalRouteLR_PQ_ES = "Leakage Total "
                    End If
                    If rowsCount = 4 OrElse rowsCount = 8 OrElse rowsCount = 12 OrElse rowsCount = 16 OrElse rowsCount = 20 Then
                        strTotalRouteLR_PQ_ES += Environment.NewLine
                    End If

                    strTotalRouteLR_PQ_ES += "" + strItemDesc + "  : " + strLR_PQ_ES + "" + " "
                    rowsCount += 1
                    If strCurItemRowNo = rowsCount Then
                        sw.Write(strTotalRouteLR_PQ_ES)
                        sw.WriteLine()
                        strTotalRouteLR_PQ_ES = ""
                        rowsCount = 0
                    End If
                ElseIf clsCommon.CompairString(DataSNO, "5") = CompairStringResult.Equal Then
                    strTotalRouteSummary = ""
                    sw.Write("----------------------------------------------------------------------------------")
                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 1
                    Dim dblTotalLTRAndKg As Double = clsCommon.myCdbl(strTotal_Qty_In_LTR) + clsCommon.myCdbl(strTotal_Qty_In_KG)
                    'strTotalRouteSummary += "TOT LTRS: " + "" + strTotal_Qty_In_LTR.ToString("N2") + "   TOT KG: " + strTotal_Qty_In_KG.ToString("N2") + "   TOT CRATES:" + clsCommon.myCstr(strtotalCrateQty) + "-0   " + "" + "   TOT CASH: " + clsCommon.myCstr(strTotalAmount)
                    strTotalRouteSummary += "TOT LTRS: " + "" + dblTotalLTRAndKg.ToString("N2") + "            TOT CRATES:" + clsCommon.myCstr(strtotalCrateQty) + "-0   " + "" + "         TOT CASH: " + clsCommon.myCstr(strTotalAmount)
                    sw.Write(strTotalRouteSummary)
                    sw.WriteLine()
                    sw.Write("----------------------------------------------------------------------------------")
                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 1
                    '===================================
                    sw.Write("|LoadingTmgs(Min) |Veh Bottom Cond.| Top| Logo| Self| Light| No.of Loaders|Other|")
                    sw.WriteLine()
                    sw.Write("| Strt| End| TmTkn|")
                    sw.WriteLine()
                    sw.Write("----------------------------------------------------------------------------------")
                    sw.WriteLine()
                    sw.Write("|      |    |      |                |    |        |   |     |               |      |")
                    sw.WriteLine()
                    sw.Write("----------------------------------------------------------------------------------")
                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 5

                    '==================================
                ElseIf clsCommon.CompairString(DataSNO, "6") = CompairStringResult.Equal Then
                    strXCratesRouteDetails = ""
                    Dim dbloutStdAM As Double = clsCommon.myCdbl(strCrateQtyRecd) - clsCommon.myCdbl(strCrateOutQty)
                    strXCratesRouteDetails += "Date= : " + "" + strCurDocDate + "   Crts-sent=:" + clsCommon.myCstr(strCrateOutQty) + " " + "   Recvd: " + clsCommon.myCstr(strCrateQtyRecd) + "    outStdAM= " + clsCommon.myCstr(dbloutStdAM) + "     Bal=0"
                    sw.Write(strXCratesRouteDetails)
                    sw.WriteLine()
                    sw.Write("")
                    sw.WriteLine()
                    sw.Write("Q.P.S        LOADED BY          SECURITY               RMRD")
                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 3
                Else
                    If (clsCommon.CompairString(strCurCustomerNo, strPrvCustomerNo) = CompairStringResult.Equal OrElse clsCommon.myLen(strPrvCustomerNo) <= 0) Then
                        If clsCommon.myLen(strPrvCustomerNo) <= 0 Then
                        ElseIf clsCommon.myLen(strPrvCustomerDesc) <= 0 Then
                            strCurCustomerDesc = ""
                        ElseIf clsCommon.CompairString(strPrvCustomerDesc, strCurCustomerNo + " (" + strCurDocDate + ")") <> CompairStringResult.Equal Then
                            strCurCustomerDesc = strCurCustomerNo + " (" + strCurDocDate + ")"
                        Else
                            strCurCustomerDesc = ""
                        End If


                    End If
                    If clsCommon.CompairString(DataSNO, "3") = CompairStringResult.Equal Then
                        If (clsCommon.CompairString(strCurRouteNo, strPrvRouteNo) = CompairStringResult.Equal) Then
                            If (clsCommon.CompairString(strCurCustomerNo, strPrvCustomerNo) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(strCurCustomerNo, "Route Total") = CompairStringResult.Equal) Then
                                strCurCustomerDesc = "ROUTE TOTAL"
                                'ElseIf clsCommon.CompairString(strPrvCustomerDescForRoutetotal, "ROUTE TOTAL") <> CompairStringResult.Equal Then
                                '    strCurCustomerDesc = "ROUTE TOTAL"
                            Else
                                strCurCustomerDesc = ""
                            End If
                        End If
                    End If
                    sw.Write(GetFormateColumnValue(strCurCustomerDesc, 20))
                    sw.Write(GetFormateColumnValue(strItemDesc, 8))
                    sw.Write(GetFormateColumnValue(strCR.Replace(",", String.Empty), 6, "R"))
                    sw.Write(GetFormateColumnValue(strCD.Replace(",", String.Empty), 6, "R"))
                    sw.Write(GetFormateColumnValue(strSO.Replace(",", String.Empty), 6, "R"))
                    sw.Write(GetFormateColumnValue(strCash.Replace(",", String.Empty), 6, "R"))
                    sw.Write(GetFormateColumnValue(strTotal.Replace(",", String.Empty), 8, "R"))
                    'strSchemeStar
                    sw.Write(GetFormateColumnValue(strSchemeStar, 1, "R"))
                    'sw.Write(GetFormateColumnValue(clsCommon.myCstr(strtotalCrateQty) + "-" + GetFormateColumnValue(clsCommon.myCstr(strTotalPendingPcsQty), 2, "R"), 8, "R"))
                    'sw.Write(GetFormateColumnValue(strTotalAmount.ToString("N2"), 11, "R"))
                    If clsCommon.CompairString(DataSNO, "3") = CompairStringResult.Equal Then
                        sw.Write(GetFormateColumnValue(clsCommon.myCstr(strtotalCrateQty) + "-" + GetFormateColumnValue(clsCommon.myCstr(strTotalPendingPcsQty), 2, "R"), 7, "R"))
                    Else
                        'sw.Write(GetFormateColumnValue(clsCommon.myCstr(strtotalCrateQty) + "." + GetFormateColumnValue(strTotalPendingPcsQty.ToString("00"), 2, "R"), 7, "R"))
                        sw.Write(GetFormateColumnValue(clsCommon.myCstr(strtotalCrateQty) + "." + GetFormateColumnValue(clsCommon.myCstr(strTotalPendingPcsQty), 2, "R"), 7, "R"))
                    End If

                    sw.Write(GetFormateColumnValue(strTotalAmount.ToString("N2").Replace(",", String.Empty), 11, "R"))

                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 1
                    SumOfCrate += strtotalCrateQty
                    SumOfAmount += strTotalAmount
                    SumOfTCSAmount += strTCSTotalAmount
                    SumOfTotlQty += clsCommon.myCdbl(strTotal)
                    If clsCommon.CompairString(DataSNO, "3") = CompairStringResult.Equal Then
                        checkTotalRoutWise = True
                    Else
                        checkTotalRoutWise = False
                    End If
                End If

                strPrvRouteNo = strCurRouteNo
                strPrvZoneCode = strCurZoneCode
                strPrvCustomerNo = strCurCustomerNo
                strPrvCustomerDesc = strCurCustomerDesc
                If clsCommon.CompairString(DataSNO, "3") = CompairStringResult.Equal Then
                    strPrvCustomerDescForRoutetotal = strCurCustomerDesc
                End If
            Next
            ' Third total
            If SumOfAmount > 0 Or SumOfTotlQty > 0 Then
                If checkTotalRoutWise = False Then
                    sw.Write(GetFormateColumnValue(SumOfCrate, 65, "R") + "-0" + GetFormateColumnValue(Math.Round(SumOfAmount, 0), 12, "R") + "  ")
                Else
                    sw.Write(GetFormateColumnValue(SumOfCrate, 65, "R") + "-0" + GetFormateColumnValue(SumOfAmount.ToString("N2").Replace(",", String.Empty), 12, "R") + "  ")
                End If
                sw.WriteLine()
                SumOfCrate = 0
                SumOfAmount = 0.0
                SumOfTotlQty = 0.0
                If clsCommon.myLen(strCustomerScheme) > 0 Then
                    sw.Write(strCustomerScheme)
                    sw.WriteLine()
                    strCustomerScheme = ""
                    rowsCustomerSchemeCount = 0
                End If
                If clsCommon.myLen(strCustomerLR_PQ_ES) > 0 Then
                    sw.Write(strCustomerLR_PQ_ES)
                    sw.WriteLine()
                    strCustomerLR_PQ_ES = ""
                    rowsCustLR_PQ_ES_Count = 0
                End If
            End If
            '=========================================
            sw.Close()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub


    'Public Shared Sub WriteDataToFile(ByVal submittedDataTable As DataTable, ByVal submittedFilePath As String)
    '    Try
    '        Dim i As Integer = 0
    '        Dim ii As Integer = 0
    '        Dim iH As Integer = 0
    '        Dim PageNo As Integer = 1
    '        Dim OnePageTotalLine As Integer = 60 '47
    '        Dim TotalLineNoPrint As Integer = 0
    '        Dim NextTotalNoOnPage As Integer = 0
    '        Dim PrvTotalNoOnPage As Integer = 0
    '        Dim sw As StreamWriter = Nothing
    '        sw = New StreamWriter(submittedFilePath, False)
    '        '=============================================================================================================================
    '        Dim strPrvRouteNo As String = ""
    '        Dim strPrvRouteDesc As String = ""
    '        Dim strPrvZoneCode As String = ""
    '        Dim strPrvZoneDesc As String = ""
    '        Dim strPrvCustomerNo As String = ""
    '        Dim strPrvCustomerDesc As String = ""
    '        Dim strPrvDocDate As String = ""
    '        Dim SumOfCrate As Integer = 0
    '        Dim SumOfAmount As Double = 0.0
    '        Dim DataSNO As String = ""
    '        For Each rowH As DataRow In submittedDataTable.Rows
    '            Dim arrayH As Object() = rowH.ItemArray
    '            'Dim strPrvRouteNo As String = ""
    '            'Dim strPrvRouteDesc As String = ""
    '            'Dim strPrvZoneCode As String = ""
    '            'Dim strPrvZoneDesc As String = ""
    '            'Dim strPrvCustomerNo As String = ""
    '            'Dim strPrvCustomerDesc As String = ""
    '            'Dim strPrvDocDate As String = ""
    '            Dim strItemCode As String = ""
    '            Dim strItemDesc As String = ""
    '            Dim strCR As String = ""
    '            Dim strCD As String = ""
    '            Dim strSO As String = ""
    '            Dim strCash As String = ""
    '            Dim strTotal As String = ""
    '            Dim strCrate As String = ""
    '            Dim strAmount As String = ""
    '            Dim strtotalCrateQty As Integer = 0
    '            Dim strTotalPendingPcsQty As Integer = 0
    '            Dim strTotalAmount As Double = 0.0

    '            Dim strCurRouteNo As String = ""
    '            Dim strCurRouteDesc As String = ""
    '            Dim strCurZoneCode As String = ""
    '            Dim strCurZoneDesc As String = ""
    '            Dim strCurCustomerNo As String = ""
    '            Dim strCurCustomerDesc As String = ""
    '            Dim strCurDocDate As String = ""
    '            Dim strCurUnitCode As String = ""
    '            Dim strCurItemRowNo As String = ""




    '            For iH = 0 To arrayH.Length - 1
    '                If iH = 3 Then
    '                    strCurRouteNo = arrayH(3).ToString()
    '                ElseIf iH = 4 Then
    '                    strCurRouteDesc = arrayH(4).ToString()
    '                ElseIf iH = 5 Then
    '                    strCurZoneCode = arrayH(5).ToString()
    '                ElseIf iH = 6 Then
    '                    strCurZoneDesc = arrayH(6).ToString()
    '                ElseIf iH = 1 Then
    '                    strCurCustomerNo = arrayH(1).ToString()
    '                ElseIf iH = 2 Then
    '                    strCurCustomerDesc = arrayH(2).ToString()
    '                ElseIf iH = 0 Then
    '                    strCurDocDate = arrayH(0).ToString()

    '                ElseIf iH = 8 Then
    '                    strItemDesc = arrayH(8).ToString()
    '                    'sw.Write(GetFormateColumnValue(arrayH(8).ToString(), 18))
    '                ElseIf iH = 9 Then
    '                    strCR = arrayH(9).ToString()
    '                    'sw.Write(GetFormateColumnValue(arrayH(9).ToString(), 12))
    '                ElseIf iH = 10 Then
    '                    strCD = arrayH(10).ToString()
    '                    'sw.Write(GetFormateColumnValue(arrayH(10).ToString(), 12))
    '                ElseIf iH = 11 Then
    '                    strSO = arrayH(11).ToString()
    '                    'sw.Write(GetFormateColumnValue(arrayH(11).ToString(), 12))
    '                ElseIf iH = 12 Then
    '                    strCash = arrayH(12).ToString()
    '                    'sw.Write(GetFormateColumnValue(arrayH(12).ToString(), 12))
    '                ElseIf iH = 13 Then
    '                    strTotal = arrayH(13).ToString()
    '                    'sw.Write(GetFormateColumnValue(arrayH(13).ToString(), 12))
    '                ElseIf iH = 14 Then
    '                    strtotalCrateQty = clsCommon.myCdbl(arrayH(14).ToString())
    '                    'sw.Write(GetFormateColumnValue(arrayH(14).ToString(), 12))
    '                ElseIf iH = 15 Then
    '                    strTotalPendingPcsQty = clsCommon.myCdbl(arrayH(15).ToString())
    '                    'sw.Write(GetFormateColumnValue(arrayH(15).ToString(), 12))
    '                ElseIf iH = 16 Then
    '                    strTotalAmount = clsCommon.myCdbl(arrayH(16).ToString())
    '                    'sw.Write(GetFormateColumnValue(arrayH(16).ToString(), 12))
    '                ElseIf iH = 17 Then
    '                    DataSNO = arrayH(17).ToString()
    '                ElseIf iH = 18 Then
    '                    strCurUnitCode = arrayH(18).ToString()
    '                ElseIf iH = 19 Then
    '                    strCurItemRowNo = arrayH(19).ToString()
    '                End If
    '                '    Next
    '                'Next
    '            Next
    '            '=============================================================================================================================

    '            If clsCommon.CompairString(strCurCustomerNo, strPrvCustomerNo) <> CompairStringResult.Equal Then
    '                If NextTotalNoOnPage = 0 And clsCommon.CompairString(DataSNO, "1") = CompairStringResult.Equal Then
    '                    NextTotalNoOnPage = NextTotalNoOnPage + 3 + 3 + 3 + clsCommon.myCdbl(strCurItemRowNo) + 1
    '                    PrvTotalNoOnPage = 3 + 3 + 3 + clsCommon.myCdbl(strCurItemRowNo) + 1
    '                ElseIf clsCommon.CompairString(DataSNO, "1") = CompairStringResult.Equal Then
    '                    NextTotalNoOnPage = NextTotalNoOnPage + 3 + 3 + clsCommon.myCdbl(strCurItemRowNo) + 1
    '                    PrvTotalNoOnPage = 3 + 3 + clsCommon.myCdbl(strCurItemRowNo) + 1
    '                ElseIf clsCommon.CompairString(DataSNO, "2") = CompairStringResult.Equal Then
    '                    NextTotalNoOnPage = NextTotalNoOnPage + 1
    '                    PrvTotalNoOnPage = 1
    '                ElseIf clsCommon.CompairString(DataSNO, "3") = CompairStringResult.Equal Then
    '                    NextTotalNoOnPage = NextTotalNoOnPage + 2 + 3 + clsCommon.myCdbl(strCurItemRowNo) + 1
    '                    PrvTotalNoOnPage = 2 + 3 + clsCommon.myCdbl(strCurItemRowNo) + 1
    '                End If
    '            End If
    '            If clsCommon.CompairString(strCurCustomerNo, strPrvCustomerNo) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(DataSNO, "2") = CompairStringResult.Equal Then
    '                NextTotalNoOnPage = NextTotalNoOnPage + clsCommon.myCdbl(strCurItemRowNo)
    '                PrvTotalNoOnPage = PrvTotalNoOnPage + clsCommon.myCdbl(strCurItemRowNo)
    '            End If
    '            If NextTotalNoOnPage > OnePageTotalLine Then ' TotalLineNoPrint = OnePageTotalLine
    '                Dim BlankSpace As Integer = PrvTotalNoOnPage - (NextTotalNoOnPage - OnePageTotalLine)
    '                TotalLineNoPrint = 0
    '                NextTotalNoOnPage = PrvTotalNoOnPage + 3 ' for 3 heading Total

    '                If SumOfAmount > 0 Or SumOfCrate > 0 Then
    '                    'sw.Write("                                                                              " + GetFormateColumnValue(SumOfCrate, 12) + "" + GetFormateColumnValue(SumOfAmount.ToString("N2"), 12) + "  ")
    '                    sw.Write(GetFormateColumnValue(SumOfCrate, 64, "R") + "" + GetFormateColumnValue(SumOfAmount.ToString("N2"), 15, "R") + "  ")
    '                    sw.WriteLine()
    '                    SumOfCrate = 0
    '                    SumOfAmount = 0.0
    '                End If
    '                Dim iii As Integer = 0
    '                For iii = 0 To BlankSpace - 1
    '                    sw.Write("")
    '                    sw.WriteLine()
    '                Next
    '            End If
    '            If TotalLineNoPrint = 0 Then ' TotalLineNoPrint = 0
    '                'If SumOfAmount > 0 Or SumOfCrate > 0 Then
    '                '    sw.Write("                                                                              " + GetFormateColumnValue(SumOfCrate, 12) + "" + GetFormateColumnValue(SumOfAmount, 12) + "  ")
    '                '    sw.WriteLine()
    '                '    SumOfCrate = 0
    '                '    SumOfAmount = 0
    '                'End If
    '                sw.Write("            THE TELANGANA STATE DAIRY DEV. CO-OP. FEDN. LTD -MPF: HYDERABAD")
    '                sw.WriteLine()
    '                TotalLineNoPrint = TotalLineNoPrint + 1
    '                sw.Write("                         TRUCK SHEET OF " + clsCommon.myCstr(strCurDocDate) + "")
    '                sw.WriteLine()
    '                TotalLineNoPrint = TotalLineNoPrint + 1
    '                sw.Write("Page: " + clsCommon.myCstr(PageNo) + " ")
    '                sw.WriteLine()
    '                TotalLineNoPrint = TotalLineNoPrint + 1
    '                PageNo = PageNo + 1
    '            End If
    '            If clsCommon.CompairString(strCurCustomerNo, strPrvCustomerNo) <> CompairStringResult.Equal Then  'clsCommon.CompairString(strCurRouteNo, strPrvRouteNo) <> CompairStringResult.Equal AndAlso
    '                If SumOfAmount > 0 Then
    '                    'sw.Write("                                                                              " + GetFormateColumnValue(SumOfCrate, 12) + "" + GetFormateColumnValue(SumOfAmount.ToString("N2"), 12) + "  ")
    '                    sw.Write(GetFormateColumnValue(SumOfCrate, 64, "R") + "" + GetFormateColumnValue(SumOfAmount.ToString("N2"), 15, "R") + "  ")
    '                    sw.WriteLine()
    '                    SumOfCrate = 0
    '                    SumOfAmount = 0.0
    '                End If
    '                If clsCommon.CompairString(DataSNO, "3") <> CompairStringResult.Equal Then
    '                    sw.Write("ZONE : (" + clsCommon.myCstr(strCurZoneCode) + ")" + clsCommon.myCstr(strCurZoneDesc) + " ")
    '                    sw.WriteLine()
    '                    TotalLineNoPrint = TotalLineNoPrint + 1
    '                End If
    '                sw.Write("ROUTE : (" + clsCommon.myCstr(strCurRouteNo) + ")" + clsCommon.myCstr(strCurRouteDesc) + " ")
    '                sw.WriteLine()
    '                TotalLineNoPrint = TotalLineNoPrint + 1
    '                If clsCommon.CompairString(DataSNO, "3") = CompairStringResult.Equal Then
    '                    sw.Write("ROUTE TOTAL : ")
    '                    sw.WriteLine()
    '                    TotalLineNoPrint = TotalLineNoPrint + 1
    '                Else
    '                    sw.Write("Customer : (" + clsCommon.myCstr(strCurCustomerNo) + ")" + clsCommon.myCstr(strCurCustomerDesc) + "  Date : " + strCurDocDate + " ")
    '                    sw.WriteLine()
    '                    TotalLineNoPrint = TotalLineNoPrint + 1
    '                End If

    '                sw.Write("----------------------------------------------------------------------------------")
    '                sw.WriteLine()
    '                TotalLineNoPrint = TotalLineNoPrint + 1

    '                For i = 0 To submittedDataTable.Columns.Count - 1
    '                    Dim strColumn_Name As String = ""
    '                    If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "Alies_Name") = CompairStringResult.Equal Then
    '                        strColumn_Name = "Type           " '18
    '                    End If
    '                    If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "CR") = CompairStringResult.Equal Then
    '                        'strColumn_Name = "CR          " ' 12
    '                        strColumn_Name = "      CR"
    '                    End If
    '                    If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "CD") = CompairStringResult.Equal Then
    '                        'strColumn_Name = "CD          " '12
    '                        strColumn_Name = "      CD" '12
    '                    End If
    '                    If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "SO") = CompairStringResult.Equal Then
    '                        strColumn_Name = "     S.O" '12
    '                    End If
    '                    If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "CASH") = CompairStringResult.Equal Then
    '                        strColumn_Name = "    Cash" '12
    '                    End If
    '                    If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "Total") = CompairStringResult.Equal Then
    '                        strColumn_Name = "       Total" '12
    '                    End If

    '                    If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "CrateQty_New") = CompairStringResult.Equal Then
    '                        strColumn_Name = "  Crates" '12
    '                    End If
    '                    If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "Amount_with_Tax") = CompairStringResult.Equal Then
    '                        strColumn_Name = "      Amount" '12
    '                    End If

    '                    If clsCommon.myLen(strColumn_Name) > 0 Then
    '                        sw.Write(strColumn_Name)
    '                    End If

    '                    'sw.Write(submittedDataTable.Columns(i).ColumnName & "             ")
    '                Next

    '                'sw.Write(submittedDataTable.Columns(i).ColumnName)
    '                sw.WriteLine()
    '                TotalLineNoPrint = TotalLineNoPrint + 1
    '                sw.Write("----------------------------------------------------------------------------------")
    '                sw.WriteLine()
    '                TotalLineNoPrint = TotalLineNoPrint + 1
    '            End If
    '            If clsCommon.CompairString(DataSNO, "2") = CompairStringResult.Equal Then
    '                sw.Write("" + strItemDesc + "  " + strCurUnitCode + " : " + strTotal + "")
    '                sw.WriteLine()
    '            Else
    '                sw.Write(GetFormateColumnValue(strItemDesc, 15))
    '                sw.Write(GetFormateColumnValue(strCR, 8, "R"))
    '                sw.Write(GetFormateColumnValue(strCD, 8, "R"))
    '                sw.Write(GetFormateColumnValue(strSO, 8, "R"))
    '                sw.Write(GetFormateColumnValue(strCash, 8, "R"))
    '                sw.Write(GetFormateColumnValue(strTotal, 12, "R"))
    '                sw.Write(GetFormateColumnValue(clsCommon.myCstr(strtotalCrateQty) + "-" + clsCommon.myCstr(strTotalPendingPcsQty), 8, "R"))
    '                sw.Write(GetFormateColumnValue(strTotalAmount.ToString("N2"), 12, "R"))
    '                sw.WriteLine()
    '                TotalLineNoPrint = TotalLineNoPrint + 1
    '                SumOfCrate += strtotalCrateQty
    '                SumOfAmount += strTotalAmount
    '            End If

    '            'For Each row As DataRow In submittedDataTable.Rows
    '            '    Dim array As Object() = row.ItemArray

    '            '    For i = 0 To array.Length - 1 - 1
    '            '        If i = 8 Or i = 9 Or i = 10 Or i = 11 Or i = 12 Or i = 13 Or i = 14 Or i = 15 Or i = 16 Then
    '            '            If i = 8 Then
    '            '                sw.Write(GetFormateColumnValue(array(8).ToString(), 18))
    '            '            ElseIf i = 9 Then
    '            '                sw.Write(GetFormateColumnValue(array(9).ToString(), 12))
    '            '            ElseIf i = 10 Then
    '            '                sw.Write(GetFormateColumnValue(array(10).ToString(), 12))
    '            '            ElseIf i = 11 Then
    '            '                sw.Write(GetFormateColumnValue(array(11).ToString(), 12))
    '            '            ElseIf i = 12 Then
    '            '                sw.Write(GetFormateColumnValue(array(12).ToString(), 12))
    '            '            ElseIf i = 13 Then
    '            '                sw.Write(GetFormateColumnValue(array(13).ToString(), 12))
    '            '            ElseIf i = 14 Then
    '            '                sw.Write(GetFormateColumnValue(array(14).ToString(), 12))
    '            '            ElseIf i = 15 Then
    '            '                sw.Write(GetFormateColumnValue(array(15).ToString(), 12))
    '            '            ElseIf i = 16 Then
    '            '                sw.Write(GetFormateColumnValue(array(16).ToString(), 12))
    '            '            End If
    '            '        End If
    '            '    Next
    '            '    'sw.Write(array(i).ToString())
    '            '    sw.WriteLine()
    '            'Next
    '            '=========================================
    '            strPrvRouteNo = strCurRouteNo
    '            strPrvZoneCode = strCurZoneCode
    '            strPrvCustomerNo = strCurCustomerNo


    '        Next
    '        If SumOfAmount > 0 Then
    '            'sw.Write("                                                                              " + GetFormateColumnValue(SumOfCrate, 12) + "" + GetFormateColumnValue(SumOfAmount.ToString("N2"), 12) + "  ")
    '            sw.Write(GetFormateColumnValue(SumOfCrate, 64, "R") + "" + GetFormateColumnValue(SumOfAmount.ToString("N2"), 15, "R") + "  ")
    '            sw.WriteLine()
    '            SumOfCrate = 0
    '            SumOfAmount = 0.0
    '        End If
    '        '=========================================
    '        sw.Close()
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try

    'End Sub

    Public Shared Function GetFormateColumnValue(ByVal strValue As String, ByVal colFixLength As Integer, Optional strSide As String = "L") As String
        'Dim collenth As Integer = clsCommon.myLen(strValue)
        'Dim typeLength As Integer = colFixLength
        'Dim collString = strValue
        'If collenth < typeLength Then
        '    Dim balanceSpace As Integer = typeLength - collenth
        '    For ii = 0 To balanceSpace - 1
        '        If clsCommon.CompairString(strSide, "L") = CompairStringResult.Equal Then
        '            collString = collString + " "
        '        Else
        '            collString = " " + collString
        '        End If
        '    Next
        'End Ifs
        'Return collString
        Dim collenth As Integer = clsCommon.myLen(strValue)
        Dim typeLength As Integer = colFixLength
        Dim collString = strValue
        If collenth < typeLength Then
            Dim balanceSpace As Integer = typeLength - collenth
            For ii = 0 To balanceSpace - 1
                If clsCommon.CompairString(strSide, "L") = CompairStringResult.Equal Then
                    collString = collString + " "
                Else
                    collString = " " + collString
                End If
            Next
        End If
        Return collString
    End Function

    Private Sub btnDotMatrixPrinter_Click(sender As Object, e As EventArgs) Handles btnDotMatrixPrinter.Click
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal Then
            If chkShowEarlyRoute.Checked = True Then
                StrEarlyRoute = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @temp VARCHAR(MAX) SET @temp =(SELECT ',''' + cast(Route_No as varchar) + '''' FROM TSPL_ROUTE_MASTER where IsEarlyRoute=1 order by Route_No FOR XML PATH('')) select SUBSTRING(@temp, 2, 200000) AS Route"))
            Else
                StrNonEarlyRoute = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @temp VARCHAR(MAX) SET @temp =(SELECT ',''' + cast(Route_No as varchar) + '''' FROM TSPL_ROUTE_MASTER where IsEarlyRoute=0 order by Route_No FOR XML PATH('')) select SUBSTRING(@temp, 2, 200000) AS Route"))
            End If
            If chkShowEarlyRoute.Checked = True AndAlso clsCommon.myLen(StrEarlyRoute) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Early Route Not Found", Me.Text)
                Exit Sub
            ElseIf chkShowEarlyRoute.Checked = False AndAlso clsCommon.myLen(StrNonEarlyRoute) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Non Early Route Not Found", Me.Text)
                Exit Sub
            End If

            Dim whr As String = " and tspl_booking_matser.TruckSheetGenerate=1 and TSPL_BOOKING_DETAIL.Booking_Qty >0  and tspl_booking_matser.AgainstGatePass=0 "
            Dim whrCustCategory As String = ""
            Dim whrVehicle As String = ""
            Dim whrRoute As String = ""
            Dim whrShipment As String = ""

            If clsCommon.myLen(clsCommon.myCstr(txtLorryNo.Value)) > 0 Then
                whr += "  and TSPL_ROUTE_MASTER.Vehicle_Code in ('" + txtLorryNo.Value + "') "
                whrVehicle = " and xx.Vehicle_Code in  ('" + txtLorryNo.Value + "') "
                whrShipment = " and TSPL_ROUTE_MASTER.Vehicle_Code in ('" + txtLorryNo.Value + "')  "
            End If

            If txtRouteNo.arrValueMember IsNot Nothing AndAlso txtRouteNo.arrValueMember.Count > 0 Then
                whr += "  and TSPL_BOOKING_DETAIL.route_No in (" + clsCommon.GetMulcallString(txtRouteNo.arrValueMember) + ") "
                whrRoute = " and TSPL_CUSTOMER_MASTER.Route_No in (" + clsCommon.GetMulcallString(txtRouteNo.arrValueMember) + ") "
                whrShipment = " and TSPL_SD_SHIPMENT_HEAD.Route_No in  (" + clsCommon.GetMulcallString(txtRouteNo.arrValueMember) + ") "
            Else
                If chkShowEarlyRoute.Checked = True Then
                    whr += "  and TSPL_BOOKING_DETAIL.route_No in (" + clsCommon.myCstr(StrEarlyRoute) + ") "
                    whrRoute = " and TSPL_CUSTOMER_MASTER.Route_No in (" + clsCommon.myCstr(StrEarlyRoute) + ") "
                    whrShipment = " and TSPL_SD_SHIPMENT_HEAD.Route_No in  (" + clsCommon.myCstr(StrEarlyRoute) + ") "
                Else
                    whr += "  and TSPL_BOOKING_DETAIL.route_No in (" + clsCommon.myCstr(StrNonEarlyRoute) + ") "
                    whrRoute = " and TSPL_CUSTOMER_MASTER.Route_No in (" + clsCommon.myCstr(StrNonEarlyRoute) + ") "
                    whrShipment = " and TSPL_SD_SHIPMENT_HEAD.Route_No in  (" + clsCommon.myCstr(StrNonEarlyRoute) + ") "
                End If
            End If

            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                whr += " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ")" + Environment.NewLine
                whrCustCategory = " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ") "
                whrShipment = " and   TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ") "
            End If
            If isTruckSheetPriview = False Then

                '================================
                trnsLst = New List(Of String)
                Dim trnsNo As Integer
                countPostedDoc = 0
                trnsLstCustomer = New List(Of String)

                DtError = New DataTable
                DtError.Columns.Add("Code", GetType(String))
                DtError.Columns.Add("Error", GetType(String))
                '=====================
                Dim strPendingBooking As String = " Select distinct TSPL_BOOKING_MATSER.Document_No from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  and tspl_booking_matser.TruckSheetGenerate=1 and TSPL_BOOKING_DETAIL.Booking_Qty >0  and TSPL_BOOKING_MATSER.posted = 0 " &
                                              "  " + whr + " "
                Dim dtPendingBooking As DataTable = clsDBFuncationality.GetDataTable(strPendingBooking)
                If dtPendingBooking IsNot Nothing And dtPendingBooking.Rows.Count > 0 Then

                    If common.clsCommon.MyMessageBoxShow(Me, "Please Post Pending Document First." + Environment.NewLine + "Do you want to Post Pending Document?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        'Dim strPostingDocument As String = " update TSPL_BOOKING_MATSER set posted = 1 where TSPL_BOOKING_MATSER.Document_No in (" + strPendingBooking + ")"
                        'clsDBFuncationality.ExecuteNonQuery(strPostingDocument)
                        '=======
                        For trnsNo = 0 To dtPendingBooking.Rows.Count - 1
                            'If Gv1.Rows(trnsNo).Cells("Status").Value = True Then
                            trnsLst.Add(dtPendingBooking.Rows(trnsNo)("Document_No"))  '' Insert The Document_Id in The StringList
                            Dim strCustomerCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1  Cust_Code  from TSPL_booking_detail where Document_no = '" + clsCommon.myCstr(dtPendingBooking.Rows(trnsNo)("Document_No")) + "'"))
                            trnsLstCustomer.Add(strCustomerCode) '' Insert The Customer Code in The StringList
                            'End If
                        Next
                        PostDairySale()
                        '============
                    End If
                    Exit Sub
                End If
            End If
            Dim dtCreateDetailsDate As DateTime = TSP_Date.Value.AddDays(-1) 'New Date(TSP_Date.Value.Day, TSP_Date.Value.Day, -1)
            Dim strCreateDetails As String = " With CTETemp as ( Select convert(varchar,Doc_Date,103) as Doc_Date, Route_No,Route_Desc,Vehicle_Code,Vehicle_Name,Customer_Code,Customer_Name,Customer_Phone, ZSM , ZSM_NAME,ZSM_Phone,ASM,ASM_Name,ASM_Phone,ASO , ASO_Name,ASO_Phone ,OpencrateQty, OpenJaaliQty, OpenBoxQty,OpenCanQty, CrateQtyRecd, JaaliQtyRecd, BoxQtyRecd ,CanQtyRecd,CrateOutQty, jaaliOutQty,boxOutQty  ,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty ,CanAdjQty,SUM(CrateQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as CrateQtyClosing,  SUM(JaaliQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as JaaliQtyClosing,  SUM(BoxQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as BoxQtyClosing, SUM(CanQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as CanQtyClosing , Row_Number() OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as RowNo from(  " &
                                             " select  pp.Doc_Date  as Doc_Date,TSPL_CUSTOMER_MASTER.Route_No,TSPL_CUSTOMER_MASTER.Route_Desc,pp.Vehicle_Code,tspl_vehicle_master.Number as Vehicle_Name ,pp.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Phone1 as Customer_Phone ,   TSPL_CUSTOMER_MASTER.ZSM,TSPL_EMPLOYEE_MASTER_ZSM.Emp_Name as ZSM_NAME,case when len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PHONE,'')) > 0 And  len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO,'')) > 0  then '['+ TSPL_EMPLOYEE_MASTER_ZSM.PHONE + '],['  + TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO +']' when  len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PHONE,'')) > 0 then TSPL_EMPLOYEE_MASTER_ZSM.PHONE when len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO,'')) > 0 then TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO else '' end  as ZSM_Phone ,TSPL_CUSTOMER_MASTER.ASM, TSPL_EMPLOYEE_MASTER_ASM.Emp_Name as ASM_NAME, case when len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PHONE,'')) > 0 And  len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO,'')) > 0  then '['+ TSPL_EMPLOYEE_MASTER_ASM.PHONE + '],['  + TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO +']' when  len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PHONE,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASM.PHONE when len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO else '' end  as ASM_Phone,TSPL_CUSTOMER_MASTER.ASO,TSPL_EMPLOYEE_MASTER_ASO.Emp_Name as ASO_NAME,   case when len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PHONE,'')) > 0 And  len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO,'')) > 0  then '['+ TSPL_EMPLOYEE_MASTER_ASO.PHONE + '],['  + TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO +']' when  len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PHONE,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASO.PHONE when len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO else '' end  as ASO_Phone,pp.OpencrateQty as OpencrateQty,pp.OpenJaaliQty  as OpenJaaliQty,pp.OpenBoxQty  as OpenBoxQty,pp.OpenCanQty  as OpenCanQty,pp.CrateQtyRecd  as CrateQtyRecd,pp.JaaliQtyRecd  as JaaliQtyRecd,pp.BoxQtyRecd  as BoxQtyRecd ,pp.CanQtyRecd  as CanQtyRecd,pp.CrateOutQty  as CrateOutQty,pp.jaaliOutQty  as jaaliOutQty ,pp.boxOutQty  as boxOutQty,pp.CanOutQty  as CanOutQty ,pp.CrateQtyClosing as CrateQtyClosing, pp.JaaliQtyClosing as  JaaliQtyClosing, pp.BoxQtyClosing as BoxQtyClosing, pp.CanQtyClosing as CanQtyClosing,pp.CrateAdjQty , pp.JaaliAdjQty  , pp.BoxAdjQty , pp.CanAdjQty from ( " &
                                             " select  max(convert(date,Doc_Date,103))  as Doc_Date,max(xx.Vehicle_Code) as Vehicle_Code,xx.Customer_Code,sum(xx.OpencrateQty) as OpencrateQty,sum(xx.OpenJaaliQty ) as OpenJaaliQty,sum(xx.OpenBoxQty )  as OpenBoxQty,sum(xx.OpenCanQty )  as OpenCanQty,sum(xx.CrateQtyRecd) as CrateQtyRecd,sum(xx.JaaliQtyRecd) as JaaliQtyRecd,sum(xx.BoxQtyRecd) as BoxQtyRecd,sum(xx.CanQtyRecd) as CanQtyRecd,sum(xx.CrateOutQty ) as CrateOutQty,sum(xx.jaaliOutQty ) as jaaliOutQty ,sum(xx.boxOutQty ) as boxOutQty,sum(xx.CanOutQty ) as CanOutQty, sum(xx.CrateAdjQty ) as CrateAdjQty ,sum(xx.JaaliAdjQty )as JaaliAdjQty  ,sum(xx.BoxAdjQty )  as BoxAdjQty ,sum(xx.CanAdjQty )  as CanAdjQty,(sum(xx.OpencrateQty)+sum(xx.CrateOutQty )-sum(xx.CrateQtyRecd)-sum(xx.CrateAdjQty )) as CrateQtyClosing, (sum(xx.OpenJaaliQty)+sum(xx.jaaliOutQty)-sum(xx.JaaliQtyRecd)-sum(xx.JaaliAdjQty )) as JaaliQtyClosing, (sum(xx.OpenBoxQty)+sum(xx.boxOutQty )-sum(xx.BoxQtyRecd)-sum(xx.BoxAdjQty )) as BoxQtyClosing  , (sum(xx.OpenCanQty)+sum(xx.CanOutQty )-sum(xx.CanQtyRecd)-sum(xx.CanAdjQty )) as CanQtyClosing  from (select  max(convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtCreateDetailsDate), "dd/MMM/yyyy") + "',103)) as Doc_Date,max(Opening.Vehicle_Code) as Vehicle_Code , Opening.Customer_Code ,sum(Opening.OpencrateQty*Type) as OpencrateQty,sum(Opening.OpenJaaliQty*Type ) as OpenJaaliQty,sum(Opening.OpenBoxQty *Type)  as OpenBoxQty,sum(Opening.OpenCanQty *Type)  as OpenCanQty,sum(Opening.CrateQtyRecd *Type) as CrateQtyRecd,sum(Opening.JaaliQtyRecd*Type ) as JaaliQtyRecd,sum(Opening.BoxQtyRecd*Type ) as BoxQtyRecd,sum(Opening.CanQtyRecd*Type ) as CanQtyRecd,sum(Opening.CrateOutQty*Type ) as CrateOutQty,sum(Opening.jaaliOutQty*Type ) as jaaliOutQty ,sum(Opening.boxOutQty*Type ) as boxOutQty,sum(Opening.CanOutQty*Type ) as CanOutQty,sum(Opening.CrateAdjQty*Type ) as CrateAdjQty,sum(Opening.JaaliAdjQty*Type ) as JaaliAdjQty,sum(Opening.BoxAdjQty *Type) as  BoxAdjQty ,sum(Opening.CanAdjQty *Type) as  canAdjQty from  ( select TSPL_SD_SHIPMENT_HEAD.Document_Date    as Document_Date,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) as Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.customer_code  ,1 as Type  ,'O' as Type1,TSPL_SD_SHIPMENT_HEAD.Crate as OpencrateQty,TSPL_SD_SHIPMENT_HEAD.jaali as OpenJaaliQty, TSPL_SD_SHIPMENT_HEAD.box  as OpenBoxQty , TSPL_SD_SHIPMENT_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS'  AND TSPL_SD_SHIPMENT_HEAD.Status =1  union all select TSPL_sd_SALE_RETURN_HEAD.Document_Date    as Document_Date,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.customer_code  ,-1 as Type  ,'I' as Type1,TSPL_sd_SALE_RETURN_HEAD.CrateQty as OpencrateQty,TSPL_sd_SALE_RETURN_HEAD.jaali as OpenJaaliQty, TSPL_sd_SALE_RETURN_HEAD.box  as OpenBoxQty , TSPL_sd_SALE_RETURN_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_sd_SALE_RETURN_HEAD  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS'  AND TSPL_sd_SALE_RETURN_HEAD.Status =1  union all  select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  , TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as OpencrateQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty   as OpenjaaliQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty  as OpenboxQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty as OpenCanQty , 0 as CrateQtyRecd,0 JaaliQtyRecd , 0 BoxQtyRecd , 0 CanQtyRecd , 0 as CrateOutQty, 0 jaaliOutQty, 0 boxoutqty, 0 Canoutqty, 0  as CrateAdjQty, 0  as JaaliAdjQty, 0  as BoxAdjQty, 0  as CanAdjQty from TSPL_CRATE_RECEIVED_detail_FRESHSALE left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  union all select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  , isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment,0)  as OpencrateQty, isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment,0)    as OpenjaaliQty , isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment,0)  as OpenboxQty, isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment,0)  as OpenCanQty, 0 as CrateQtyRecd,0 JaaliQtyRecd , 0 BoxQtyRecd , 0 CanQtyRecd , 0 as CrateOutQty, 0 jaaliOutQty, 0 boxoutqty, 0 Canoutqty, 0  as CrateAdjQty, 0  as JaaliAdjQty, 0  as BoxAdjQty, 0  as CanAdjQty from TSPL_CRATE_RECEIVED_detail_FRESHSALE left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1   )as Opening WHERE convert(date,Document_Date,103)<(convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtCreateDetailsDate), "dd/MMM/yyyy") + "',103)) group by Customer_Code  " &
                                             " UNION All " &
                                             " select Document_Date,Vehicle_Code,Customer_Code,0 as OpencrateQty,0 as OpenjaaliQty ,0 as OpenboxQty,0 as OpenCanQty,Case When [Type]=1 Then CrateQtyRecd Else 0 End as CrateQtyRecd,Case When [Type]=1 Then JaaliQtyRecd Else 0 End as JaaliQtyRecd,Case When [Type]=1 Then BoxQtyRecd Else 0 End as BoxQtyRecd,Case When [Type]=1 Then CANQtyRec Else 0 End as CANQtyRec, " &
                                             " Case When [Type]=-1 Then CrateOutQty Else 0 End as CrateOutQty,Case When [Type]=-1 Then jaaliOutQty Else 0 End as jaaliOutQty,Case When [Type]=-1 Then boxOutQty Else 0 End as boxOutQty,Case When [Type]=-1 Then CanOutQty Else 0 End as CanOutQty,Case When [Type]=1 Then CrateAdjQty Else 0 End as CrateAdjQty,Case When [Type]=1 Then JaaliAdjQty Else 0 End as JaaliAdjQty,Case When [Type]=1 Then BoxAdjQty Else 0 End as BoxAdjQty,Case When [Type]=1 Then CANAdjustment Else 0 End as CANAdjustment  " &
                                             " from ((select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCanQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE " &
                                             " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  " &
                                             " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1 ) union all  " &
                                             " (select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCANQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE " &
                                             " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  " &
                                             " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  union all  " &
                                             " select TSPL_SD_SHIPMENT_HEAD.Document_Date   as Document_Date,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) as Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.customer_code  ,-1 as Type  ,'O' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty, 0 as CrateQtyRecd, 0 JaaliQtyRecd ,  0  as BoxQtyRecd,0 CanQtyRecd  ,TSPL_SD_SHIPMENT_HEAD.Crate as CrateOutQty,jaali  as jaaliOutQty,TSPL_SD_SHIPMENT_HEAD.Box as boxOutQty,TSPL_SD_SHIPMENT_HEAD.ShippedCAN as CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty    from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS' AND TSPL_SD_SHIPMENT_HEAD.Status =1 union all  " &
                                             " select TSPL_sd_SALE_RETURN_HEAD.Document_Date   as Document_Date,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.customer_code  ,1 as Type  ,'I' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty, TSPL_sd_SALE_RETURN_HEAD.CrateQty as CrateQtyRecd, TSPL_sd_SALE_RETURN_HEAD.jaali as JaaliQtyRecd ,  TSPL_sd_SALE_RETURN_HEAD.Box  as BoxQtyRecd,TSPL_sd_SALE_RETURN_HEAD.ShippedCAN CanQtyRecd  ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty,0 as CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty    from TSPL_sd_SALE_RETURN_HEAD  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS' AND TSPL_sd_SALE_RETURN_HEAD.Status =1 )  ) as Closing  " &
                                             " WHERE convert(date,Document_Date ,103)>=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtCreateDetailsDate), "dd/MMM/yyyy") + "',103) AND convert(date,Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtCreateDetailsDate), "dd/MMM/yyyy") + "',103)  " &
                                             " ) as xx where 2=2   " &
                                             " and xx.Customer_Code in (Select distinct TSPL_BOOKING_DETAIL.Cust_Code  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  " &
                                             " left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code " &
                                             " where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  and tspl_booking_matser.TruckSheetGenerate=1  " &
                                             " " + whrCustCategory + " " &
                                             " ) " &
                                             " " + whrVehicle + " " &
                                             " GROUP BY Customer_Code " &
                                             " ) as pp   left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =pp.Customer_Code  " &
                                             " left outer Join  TSPL_EMPLOYEE_MASTER as TSPL_EMPLOYEE_MASTER_ZSM on TSPL_EMPLOYEE_MASTER_ZSM.EMP_CODE = TSPL_CUSTOMER_MASTER.ZSM  " &
                                             " left outer Join  TSPL_EMPLOYEE_MASTER as TSPL_EMPLOYEE_MASTER_ASM on TSPL_EMPLOYEE_MASTER_ASM.EMP_CODE = TSPL_CUSTOMER_MASTER.ASM  " &
                                             " left outer Join  TSPL_EMPLOYEE_MASTER as TSPL_EMPLOYEE_MASTER_ASO on TSPL_EMPLOYEE_MASTER_ASO.EMP_CODE = TSPL_CUSTOMER_MASTER.ASO  " &
                                             " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=pp.vehicle_code where 2=2   " + whrRoute + " " &
                                             " ) YYY )   "
            Dim qry = " " + strCreateDetails + " " &
                     " select XXXXXFinal.Document_Date,XXXXXFinal.Cust_Code,Case when  XXXXXFinal.DataSNO = 1 and No_Of_Item =1 then SUBSTRING((XXXXXFinal.Cust_Code + ' ('+XXXXXFinal.Document_Date + ')'),0,19) else  SUBSTRING ( XXXXXFinal.Customer_Name,0,19) end as Customer_Name, XXXXXFinal.route_no,XXXXXFinal.Route_Desc,XXXXXFinal.Zone_Code,XXXXXFinal.Zone_Name,XXXXXFinal.Item_Code,SUBSTRING (XXXXXFinal.Alies_Name,0,8) as Alies_Name, Convert (integer,XXXXXFinal.CR) as CR,Convert (Integer ,XXXXXFinal.CD) as CD,convert (Integer,XXXXXFinal.SO) as SO,Convert (Integer ,XXXXXFinal.Cash,103) as Cash ,Convert (Integer,XXXXXFinal.Total) as Total,XXXXXFinal.CrateQty_New as CrateQty_New , XXXXXFinal.PendingPcsQty_New as PendingPcsQty_New, XXXXXFinal.Amount_with_Tax, XXXXXFinal.  DataSNO,   XXXXXFinal. Unit_code,TBL_Count.No_Of_Item + (isnull (TBL_Count.No_Of_Item1,0) /4) + case when  (isnull (TBL_Count.No_Of_Item1,0) %4) > 0  then 1 else 0 end + isNull(TBL_Count.No_Of_Item2,0)+isnull (TBL_Count.No_Of_Item3,0) as No_Of_Item ,  XXXXXFinal.LR_PQ_ES ,isnull (XXXXXFinal.OpencrateQty,0) as OpencrateQty , isnull ( XXXXXFinal.CrateQtyRecd,0) as CrateQtyRecd ,isnull( XXXXXFinal.CrateOutQty,0) as CrateOutQty, isnull ( XXXXXFinal.CrateAdjQty,0) as CrateAdjQty , isnull (XXXXXFinal.CrateQtyClosing,0) as CrateQtyClosing, Total_Scheme_Star, isnull ( Qty_in_KG,0) as Qty_in_KG , isnull (Qty_in_Ltr,0) as Qty_in_Ltr,XXXXXFinal.BaseAmtForTCS  from (  " &
                     " select XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CrateQty_New as CrateQty_New , XXXXX.PendingPcsQty_New as PendingPcsQty_New,isnull (XXXXX.Amount_with_Tax,0) as Amount_with_Tax,isnull (XXXXX.BaseAmtForTCS,0) as BaseAmtForTCS, '1' as  DataSNO, '' as  Unit_code,0 as  LR_PQ_ES , 0 as OpencrateQty , 0 as CrateQtyRecd , 0 as CrateOutQty , 0 as CrateAdjQty , 0 as CrateQtyClosing, case when  isnull (XXXXX.Total_Scheme,0) > 0 then '*' else '' end as Total_Scheme_Star,0 as Qty_in_KG , 0 as Qty_in_Ltr  from (  Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty , Convert (int,CrateQty) as CrateQty_New , case when TSPL_ITEM_UOM_DETAIL_STOCKING_UOM_Conv .UOM_Code = 'ltr' then (XXXFinal.Total * finalStockConvPCS.Conversion_Factor/ TSPL_ITEM_UOM_DETAIL_STOCKING_UOM_Conv.Conversion_Factor  -   (StockPcs.Conversion_Factor * Convert (int,CrateQty))) * TSPL_ITEM_UOM_DETAIL_STOCKING_UOM_Conv.Conversion_Factor /finalStockConvPCS.Conversion_Factor  else (XXXFinal.Total) - ((StockPcs.Conversion_Factor * Convert (int,CrateQty)))  end  as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax ,sum (XFinal.BaseAmtForTCS)  as BaseAmtForTCS, max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,BaseAmtForTCS,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when TSPL_BOOKING_MATSER.Booking_Type  = 'FESTIVE ORDER' then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end  Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.OldName as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax, TSPL_BOOKING_DETAIL.Amount_with_Tax  as BaseAmtForTCS,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots  Union All  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   isnull([Cash_Scheme],0) as [Cash_Scheme] ,isnull([CD_Scheme],0) as [CD_Scheme] ,isnull([CR_Scheme],0) as [CR_Scheme] ,isnull([SO_Scheme],0) as [SO_Scheme] ,isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,BaseAmtForTCS ,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when TSPL_BOOKING_MATSER.Booking_Type  = 'FESTIVE ORDER' then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end +'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax as BaseAmtForTCS,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "   )  Final   pivot  (  max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_STOCKING_UOM_Conv on TSPL_ITEM_UOM_DETAIL_STOCKING_UOM_Conv.Item_Code =  XXXFinal.Item_Code and TSPL_ITEM_UOM_DETAIL_STOCKING_UOM_Conv.Stocking_Unit ='Y'  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='PCS') as finalStockConvPCS on XXXFinal.Item_Code =finalStockConvPCS.Item_Code    ) XXXXX   " &
                     " Union All " &
                     " select  XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax,0 as BaseAmtForTCS, '2' as  DataSNO, XXXXX.Unit_code as  Unit_code,  0 as  LR_PQ_ES , 0 as OpencrateQty , 0 as CrateQtyRecd , 0 as CrateOutQty , 0 as CrateAdjQty , 0 as CrateQtyClosing , '' as Total_Scheme_Star , 0 as Qty_in_KG , 0 as Qty_in_Ltr from (   Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty ,  'Scheme '+XXXFinal.Alies_Name as  Alies_Name_on_Name from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   [Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme],isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when TSPL_BOOKING_MATSER.Booking_Type  = 'FESTIVE ORDER' then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end +'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No   left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code   left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code   left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code   where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "   )  Final   pivot  (   max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code   ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code ) XXXXX " &
                     " Union All " &
                     " select  XXXXX.Document_Date, 'Route Total' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CEILING_Crate_Qty  as CrateQty_New , PendingPcsQty_New as PendingPcsQty_New,XXXXX.Amount_with_Tax,0 as BaseAmtForTCS, '3' as  DataSNO, '' as  Unit_code ,  0 as  LR_PQ_ES , 0 as OpencrateQty , 0 as CrateQtyRecd , 0 as CrateOutQty , 0 as CrateAdjQty , 0 as CrateQtyClosing, '' as Total_Scheme_Star , 0 as Qty_in_KG , 0 as Qty_in_Ltr from (    Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty, Convert (int,CrateQty) as CrateQty_New , case when TSPL_ITEM_UOM_DETAIL_STOCKING_UOM_Conv .UOM_Code = 'ltr' then (XXXFinal.Total * finalStockConvPCS.Conversion_Factor/ TSPL_ITEM_UOM_DETAIL_STOCKING_UOM_Conv.Conversion_Factor  -   (StockPcs.Conversion_Factor * Convert (int,CrateQty))) * TSPL_ITEM_UOM_DETAIL_STOCKING_UOM_Conv.Conversion_Factor /finalStockConvPCS.Conversion_Factor  else (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) end as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New  from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when TSPL_BOOKING_MATSER.Booking_Type  = 'FESTIVE ORDER' then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end  Booking_Type  ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by  XFinal.route_no, XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code   left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_STOCKING_UOM_Conv on TSPL_ITEM_UOM_DETAIL_STOCKING_UOM_Conv.Item_Code =  XXXFinal.Item_Code and TSPL_ITEM_UOM_DETAIL_STOCKING_UOM_Conv.Stocking_Unit ='Y'  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='PCS') as finalStockConvPCS on XXXFinal.Item_Code =finalStockConvPCS.Item_Code   )XXXXX " &
                     "  " &
                     " Union all  " &
                     " select  XXXXX.Document_Date, XXXXX.Cust_Code as Cust_Code,'Customer Leakage' as Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax,0 as BaseAmtForTCS, '22' as  DataSNO, XXXXX.Unit_code as  Unit_code  , LR_PQ_ES , 0 as OpencrateQty , 0 as CrateQtyRecd , 0 as CrateOutQty , 0 as CrateAdjQty , 0 as CrateQtyClosing, '' as Total_Scheme_Star, 0 as Qty_in_KG , 0 as Qty_in_Ltr from ( Select Document_Date,Cust_Code, max(Customer_Name) as Customer_Name ,Route_No,max(Route_Desc) as Route_Desc,Zone_Code,max(Zone_Name) as Zone_Name  , Item_Code, max (Alies_Name) as Alies_Name , sum (CR) as CR,sum (CD) as CD,Sum(SO) as SO ,sum (Cash) as Cash, sum(Total) as Total ,sum( CrateQty_New) as CrateQty_New,sum( PendingPcsQty_New) as PendingPcsQty_New, sum (Amount_with_Tax) as Amount_with_Tax,Unit_code,sum (LR_PQ_ES) as LR_PQ_ES  from ( Select convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 0 as CR,0 as CD,0 as SO,0 as Cash,0 as Total,0  as CrateQty_New , 0 as PendingPcsQty_New,0 as Amount_with_Tax, 'PCS' as  Unit_code , Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty) as LR_PQ_ES from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='PCS' inner join ( Select distinct TSPL_BOOKING_DETAIL.Cust_Code  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No    left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  and tspl_booking_matser.TruckSheetGenerate=1  " + whr + " ) TBL_VALID_Cust on TBL_VALID_Cust.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code where  TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1  and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) and TSPL_SD_SHIPMENT_HEAD.status = 1   " + whrShipment + " ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code ) XXXXX   " &
                     " Union all " &
                     " select  XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax,0 as BaseAmtForTCS, '4' as  DataSNO, XXXXX.Unit_code as  Unit_code  , LR_PQ_ES , 0 as OpencrateQty , 0 as CrateQtyRecd , 0 as CrateOutQty , 0 as CrateAdjQty , 0 as CrateQtyClosing, '' as Total_Scheme_Star , 0 as Qty_in_KG , 0 as Qty_in_Ltr from ( Select Document_Date, 'Total Route Leakage' as Cust_Code, 'Total Route Leakage' as Customer_Name ,Route_No,max(Route_Desc) as Route_Desc,Zone_Code,max(Zone_Name) as Zone_Name  , Item_Code, max (Alies_Name) as Alies_Name , sum (CR) as CR,sum (CD) as CD,Sum(SO) as SO ,sum (Cash) as Cash, sum(Total) as Total ,sum( CrateQty_New) as CrateQty_New,sum( PendingPcsQty_New) as PendingPcsQty_New, sum (Amount_with_Tax) as Amount_with_Tax,Unit_code,sum (LR_PQ_ES) as LR_PQ_ES  from ( Select convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 0 as CR,0 as CD,0 as SO,0 as Cash,0 as Total,0  as CrateQty_New , 0 as PendingPcsQty_New,0 as Amount_with_Tax, 'PCS' as  Unit_code , Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty) as LR_PQ_ES  from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='PCS' inner join ( Select distinct TSPL_BOOKING_DETAIL.Cust_Code  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No    left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  and tspl_booking_matser.TruckSheetGenerate=1  " + whr + ") TBL_VALID_Cust on TBL_VALID_Cust.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code where  TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others') and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1  and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) and TSPL_SD_SHIPMENT_HEAD.status = 1   " + whrShipment + " ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no,XFinal.Item_Code,XFinal.Unit_code ) XXXXX    " &
                     " Union all " &
                     " Select  XXXXX2.Document_Date,max( XXXXX2.Cust_Code) as Cust_Code ,max (XXXXX2.Customer_Name) as  Customer_Name,XXXXX2.route_no,max(XXXXX2.Route_Desc) as Route_Desc,max (XXXXX2.Zone_Code) as Zone_Code,max(XXXXX2.Zone_Name) as Zone_Name, max(XXXXX2.Item_Code) as Item_Code,max(XXXXX2.Alies_Name) as Alies_Name,sum(XXXXX2.CR) as CR ,Sum(XXXXX2.CD) as CD,sum(XXXXX2.SO) as SO,sum(XXXXX2.Cash) as Cash,sum(XXXXX2.Total) as Total,sum(XXXXX2.CrateQty_New)  as CrateQty_New , sum(XXXXX2.PendingPcsQty_New) as PendingPcsQty_New,sum(XXXXX2.Amount_with_Tax) as Amount_with_Tax,0 as BaseAmtForTCS, max(XXXXX2.DataSNO) as  DataSNO, max(XXXXX2.Unit_code) as  Unit_code, max(XXXXX2.LR_PQ_ES) as  LR_PQ_ES  , 0 as OpencrateQty , 0 as CrateQtyRecd , 0 as CrateOutQty , 0 as CrateAdjQty , 0 as CrateQtyClosing , '' as Total_Scheme_Star,  sum(XXXXX2.Qty_in_KG) as  Qty_in_KG, sum (Qty_in_Ltr) as Qty_in_Ltr from (  select  XXXXX.Document_Date, 'Total Route Summary' as Cust_Code,'Total Route Summary'as Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,'' as Item_Code,'' as Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CrateQty_New  as CrateQty_New , PendingPcsQty as PendingPcsQty_New,XXXXX.Amount_with_Tax, '5' as  DataSNO, '' as  Unit_code, 0 as  LR_PQ_ES, Total_KG as Qty_in_KG , Total_LTR as  Qty_in_Ltr from (    Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  ,Convert (int,CEILING_Crate_Qty) as CrateQty_New , (XXXFinal.Total) - ((StockPcs.Conversion_Factor * Convert (int,CrateQty))) as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New   from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,max (Unit_Code_For_Create) as Unit_Code_For_Create,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item, sum (Total_KG) as Total_KG, sum (Total_LTR) as Total_LTR from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,Unit_Code_For_Create as Unit_Code_For_Create,  isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] , isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0)  as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item, isnull ([Cash_KG],0)+isnull([CD_KG],0)+isnull([CR_KG],0)+isnull([SO_KG],0) as Total_KG,  isnull ([Cash_LTR],0)+isnull([CD_LTR],0)+isnull([CR_LTR],0)+isnull([SO_LTR],0) as Total_LTR from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code, case when TSPL_BOOKING_MATSER.Booking_Type  = 'FESTIVE ORDER' then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end +'_KG' as  Booking_Type_KG,	case when TSPL_BOOKING_MATSER.Booking_Type  = 'FESTIVE ORDER' then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end   +'_LTR'	as  Booking_Type_LTR, case when TSPL_BOOKING_MATSER.Booking_Type  = 'FESTIVE ORDER' then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end  Booking_Type ,TSPL_BOOKING_DETAIL.Booking_Qty ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name, Convert (decimal(18,2), round( ((isnull (Source_Conv.Conversion_Factor,0)/ nullif ( isnull (Target_Conv.Conversion_Factor,0),0) ) * TSPL_BOOKING_DETAIL.Booking_Qty),2,1)) as Booking_Qty_Ltr, case when isnull (Target_Conv.Conversion_Factor,0) = 0 then Convert (decimal(18,2),  round(((isnull (Source_Conv.Conversion_Factor,0)/ nullif ( isnull (Target_Conv_KG.Conversion_Factor,0),0) ) * TSPL_BOOKING_DETAIL.Booking_Qty),2,1)) end as Booking_Qty_Kg ,  TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,'LTR' as Unit_Code,TSPL_BOOKING_DETAIL.Unit_Code as Unit_Code_For_Create,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  Left outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on 	Source_Conv.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and 	Source_Conv.UOM_Code = TSPL_BOOKING_DETAIL.Unit_code Left Outer Join TSPL_ITEM_UOM_DETAIL as Target_Conv on   Target_Conv.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_Conv.Uom_code = 'LTR'  Left Outer Join TSPL_ITEM_UOM_DETAIL as Target_Conv_KG on   Target_Conv_KG.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_Conv_KG.Uom_code = 'KG'  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)   and tspl_booking_matser.TruckSheetGenerate=1  " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivotss  pivot  (  max(Booking_Qty_Kg) for Booking_Type_KG in ([Cash_KG],[CD_KG],[CR_KG],[SO_KG])   )as pivots  pivot  (  max(Booking_Qty_Ltr) for Booking_Type_LTR in ([Cash_LTR],[CD_LTR],[CR_LTR],[SO_LTR])   )as pivotsLtr ) XFinal Group by  XFinal.route_no, XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_Code_For_Create =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )  XXXXX  ) XXXXX2 group by Document_Date, route_no  " &
                     " Union All  " &
                     "  select max (XXXXX.Date) as Document_Date, max([Customer Code]) as  Cust_Code, max ([Customer Name]) as Customer_Name , XXXXX.[Route Code] as route_no ,max( XXXXX.[Route Name]) as Route_Desc,'' as Zone_Code,'' as Zone_Name,'ItemCode' as Item_Code,'' as Alies_Name,0 as CR,0 as CD,0 as SO,0 as Cash,0 as Total,0  as CrateQty_New , 0 as PendingPcsQty_New,0 as Amount_with_Tax,0 as BaseAmtForTCS, '6' as  DataSNO, '' as    Unit_code  , 1 as LR_PQ_ES     , sum (OpencrateQty) as OpencrateQty , sum (CrateQtyRecd) as CrateQtyRecd , sum (CrateOutQty) as CrateOutQty ,sum (CrateAdjQty) as CrateAdjQty , sum (CrateQtyClosing) as CrateQtyClosing ,'' as Total_Scheme_Star, 0 as Qty_in_KG , 0 as Qty_in_Ltr  from (   Select convert(varchar,Doc_Date,103) as Date,Route_No as [Route Code],Route_Desc as [Route Name], Vehicle_Code as [Vehicle Code],Vehicle_Name as [Vehicle Name], 'XCrates Route Details' as [Customer Code],'XCrates Route Details' as [Customer Name],Customer_Phone as [Customer Phone], ZSM as [ZSM Code], ZSM_Name as [ZSM Name],ZSM_Phone as [ZSM Phone],ASM as [ASM Code],ASM_NAME as [ASM Name],ASM_Phone as [ASM Phone],ASO as [ASO Code],ASO_Name as [ASO Name],ASO_Phone as [ASO Phone], OpencrateQty,OpenJaaliQty,OpenBoxQty,OpenCanQty, CrateQtyRecd, JaaliQtyRecd,BoxQtyRecd, CanQtyRecd  ,CrateOutQty, jaaliOutQty,boxOutQty,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty,CanAdjQty, OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty as CrateQtyClosing, OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty as CanQtyClosing , OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty as JaaliQtyClosing, OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty as BoxQtyClosing, (OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty)* 0 as CrateValueClosing, (OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty)* 0 as JaaliValueClosing, (OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty)* 0 as BoxValueClosing ,  (OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty)* 0 as CanValueClosing ,OpencrateQtySum,OpenCanQtySum ,OpencrateQtySumOfVehicleWise,OpencanQtySumOfVehicleWise  from (Select  case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySum, case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySum, case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySumOfVehicleWise, case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySumOfVehicleWise, CTETemp.RowNo,CTETemp.Doc_Date, CTETemp.Route_No,CTETemp.Route_Desc ,CTETemp.Vehicle_Code,CTETemp.Vehicle_Name ,CTETemp.Customer_Code,CTETemp.Customer_Name, CTETemp.Customer_Phone, CTETemp.ZSM,CTETemp.ZSM_NAME,CTETemp.ZSM_Phone,CTETemp.ASM,CTETemp.ASM_NAME,CTETemp.ASM_Phone,CTETemp.ASO,CTETemp.ASO_NAME,CTETemp.ASO_Phone, CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) as OpencrateQty,  CTETemp.OpenJaaliQty+ISNULL(CT1.JaaliQtyClosing,0) as OpenJaaliQty,  CTETemp.OpenBoxQty+ISNULL(CT1.BoxQtyClosing,0) as OpenBoxQty, CTETemp.OpenCanQty+ISNULL(CT1.CanQtyClosing,0) as OpenCanQty,  CTETemp.CrateQtyRecd, CTETemp.JaaliQtyRecd, CTETemp.BoxQtyRecd,CTETemp.CanQtyRecd, CTETemp.CrateOutQty, CTETemp.jaaliOutQty, CTETemp.boxOutQty, CTETemp.CanOutQty, CTETemp.CrateAdjQty , CTETemp.JaaliAdjQty  , CTETemp.BoxAdjQty, CTETemp.CanAdjQty  from CTETemp LEFt OUTER JOIN CTETemp CT1 ON  CT1.Customer_Code=CTETemp.Customer_Code and  CT1.Vehicle_Code = CTETemp.Vehicle_Code  and CT1.Route_No  = CTETemp.Route_No  AND (CTETemp.RowNo-CT1.RowNo)=1 ) ZZZ   ) XXXXX  Group by  XXXXX.[Route Code]   " &
                     "  " &
                     " )XXXXXFinal left Outer Join " &
                     " ( " &
                     "  select XXXXXFinal.DataSNO ,XXXXXFinal.route_no,XXXXXFinal.Cust_Code , Count (case when RowCountDivideByFour=0 then  Item_Code end) as No_Of_Item , Count (case when RowCountDivideByFour=1 then  Item_Code end) as No_Of_Item1, Count (case when RowCountDivideByFour=2 then  Item_Code end) as No_Of_Item2, Count (case when RowCountDivideByFour=3 then  Item_Code end) as No_Of_Item3 from ( " &
                     "  select '1' as  DataSNO, XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CrateQty_New as CrateQty_New , XXXXX.PendingPcsQty_New as PendingPcsQty_New,isnull (XXXXX.Amount_with_Tax,0) as Amount_with_Tax,'' as  Unit_code, 0 as RowCountDivideByFour from (  Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty , Convert (int,CrateQty) as CrateQty_New , (XXXFinal.Total) - ((StockPcs.Conversion_Factor * Convert (int,CrateQty))) as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when TSPL_BOOKING_MATSER.Booking_Type  = 'FESTIVE ORDER' then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end  Booking_Type  ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.OldName as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots  Union All  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   isnull([Cash_Scheme],0) as [Cash_Scheme] ,isnull([CD_Scheme],0) as [CD_Scheme] ,isnull([CR_Scheme],0) as [CR_Scheme] ,isnull([SO_Scheme],0) as [SO_Scheme] ,isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when TSPL_BOOKING_MATSER.Booking_Type  = 'FESTIVE ORDER' then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end  +'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  " + whr + "  )  Final   pivot  (  max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code   ) XXXXX  " &
                     " Union All " &
                     " select '2' as  DataSNO, XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, XXXXX.Unit_code as  Unit_code, 0 as RowCountDivideByFour from ( " &
                     " Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty ,  'Scheme '+XXXFinal.Alies_Name as  Alies_Name_on_Name from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   [Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme],isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when TSPL_BOOKING_MATSER.Booking_Type  = 'FESTIVE ORDER' then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end   +'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No   left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code   left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code   left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code   where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "   )  Final   pivot  (   max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code   ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code " &
                     " ) XXXXX " &
                     " Union All " &
                     " select '3' as  DataSNO, XXXXX.Document_Date, 'Route Total' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, '' as  Unit_code, 0 as RowCountDivideByFour from ( " &
                     " Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when TSPL_BOOKING_MATSER.Booking_Type  = 'FESTIVE ORDER' then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end  Booking_Type  ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by  XFinal.route_no, XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )   XXXXX  " &
                     "   " &
                     " Union all  " &
                     " select '22' as  DataSNO,  XXXXX.Document_Date, XXXXX.Cust_Code as Cust_Code,'Customer Leakage' as Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax , XXXXX.Unit_code as  Unit_code,0 as RowCountDivideByFour  from ( Select Document_Date,Cust_Code, max(Customer_Name) as Customer_Name ,Route_No,max(Route_Desc) as Route_Desc,Zone_Code,max(Zone_Name) as Zone_Name  , Item_Code, max (Alies_Name) as Alies_Name , sum (CR) as CR,sum (CD) as CD,Sum(SO) as SO ,sum (Cash) as Cash, sum(Total) as Total ,sum( CrateQty_New) as CrateQty_New,sum( PendingPcsQty_New) as PendingPcsQty_New, sum (Amount_with_Tax) as Amount_with_Tax,Unit_code,sum (LR_PQ_ES) as LR_PQ_ES  from ( Select convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 0 as CR,0 as CD,0 as SO,0 as Cash,0 as Total,0  as CrateQty_New , 0 as PendingPcsQty_New,0 as Amount_with_Tax, 'PCS' as  Unit_code , Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty) as LR_PQ_ES from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='PCS' inner join ( Select distinct TSPL_BOOKING_DETAIL.Cust_Code  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No   left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  and tspl_booking_matser.TruckSheetGenerate=1 " + whr + " ) TBL_VALID_Cust on TBL_VALID_Cust.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code where  TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others') and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1  and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) and TSPL_SD_SHIPMENT_HEAD.status = 1  " + whrShipment + "  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code ) XXXXX   " &
                     " Union all  " &
                     " select  '4' as  DataSNO,  XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax,  XXXXX.Unit_code as  Unit_code, 0 as RowCountDivideByFour from ( Select Document_Date, 'Total Route Leakage' as Cust_Code, max(Customer_Name) as Customer_Name ,Route_No,max(Route_Desc) as Route_Desc,Zone_Code,max(Zone_Name) as Zone_Name  , Item_Code, max (Alies_Name) as Alies_Name , sum (CR) as CR,sum (CD) as CD,Sum(SO) as SO ,sum (Cash) as Cash, sum(Total) as Total ,sum( CrateQty_New) as CrateQty_New,sum( PendingPcsQty_New) as PendingPcsQty_New, sum (Amount_with_Tax) as Amount_with_Tax,Unit_code,sum (LR_PQ_ES) as LR_PQ_ES  from ( Select convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 0 as CR,0 as CD,0 as SO,0 as Cash,0 as Total,0  as CrateQty_New , 0 as PendingPcsQty_New,0 as Amount_with_Tax, 'PCS' as  Unit_code  , Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty) as LR_PQ_ES from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='PCS' inner join ( Select distinct TSPL_BOOKING_DETAIL.Cust_Code  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  and tspl_booking_matser.TruckSheetGenerate=1  " + whr + " ) TBL_VALID_Cust on TBL_VALID_Cust.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code where  TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others') and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1  and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) and TSPL_SD_SHIPMENT_HEAD.status = 1  " + whrShipment + "  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no,XFinal.Item_Code,XFinal.Unit_code ) XXXXX   " &
                     " Union all  " &
                     " select  '3' as  DataSNO,  XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax,  XXXXX.Unit_code as  Unit_code, 1 as RowCountDivideByFour from ( Select Document_Date, 'Route Total' as Cust_Code, max(Customer_Name) as Customer_Name ,Route_No,max(Route_Desc) as Route_Desc,Zone_Code,max(Zone_Name) as Zone_Name  , Item_Code, max (Alies_Name) as Alies_Name , sum (CR) as CR,sum (CD) as CD,Sum(SO) as SO ,sum (Cash) as Cash, sum(Total) as Total ,sum( CrateQty_New) as CrateQty_New,sum( PendingPcsQty_New) as PendingPcsQty_New, sum (Amount_with_Tax) as Amount_with_Tax,Unit_code,sum (LR_PQ_ES) as LR_PQ_ES  from ( Select convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 0 as CR,0 as CD,0 as SO,0 as Cash,0 as Total,0  as CrateQty_New , 0 as PendingPcsQty_New,0 as Amount_with_Tax, 'PCS' as  Unit_code  , Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty) as LR_PQ_ES from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='PCS' inner join ( Select distinct TSPL_BOOKING_DETAIL.Cust_Code  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  and tspl_booking_matser.TruckSheetGenerate=1  " + whr + " ) TBL_VALID_Cust on TBL_VALID_Cust.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code where  TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others') and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1  and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) and TSPL_SD_SHIPMENT_HEAD.status = 1  " + whrShipment + "  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no,XFinal.Item_Code,XFinal.Unit_code ) XXXXX   " &
                     " Union all  " &
                     " Select  max(XXXXX2.DataSNO) as  DataSNO, XXXXX2.Document_Date,max( XXXXX2.Cust_Code) as Cust_Code ,max (XXXXX2.Customer_Name) as  Customer_Name,XXXXX2.route_no,max(XXXXX2.Route_Desc) as Route_Desc,max (XXXXX2.Zone_Code) as Zone_Code,max(XXXXX2.Zone_Name) as Zone_Name, max(XXXXX2.Item_Code) as Item_Code,max(XXXXX2.Alies_Name) as Alies_Name,sum(XXXXX2.CR) as CR ,Sum(XXXXX2.CD) as CD,sum(XXXXX2.SO) as SO,sum(XXXXX2.Cash) as Cash,sum(XXXXX2.Total) as Total,sum(XXXXX2.CrateQty_New)  as CrateQty_New , sum(XXXXX2.PendingPcsQty_New) as PendingPcsQty_New,sum(XXXXX2.Amount_with_Tax) as Amount_with_Tax, max(XXXXX2.Unit_code) as  Unit_code , 0 as RowCountDivideByFour  from (  select  XXXXX.Document_Date, 'Total Route Summary' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,'' as Item_Code,'' as Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CrateQty_New  as CrateQty_New , PendingPcsQty as PendingPcsQty_New,XXXXX.Amount_with_Tax, '5' as  DataSNO, '' as  Unit_code, 0 as  LR_PQ_ES from (    Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  ,Convert (int,CrateQty) as CrateQty_New , (XXXFinal.Total) - ((StockPcs.Conversion_Factor * Convert (int,CrateQty))) as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New  from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when TSPL_BOOKING_MATSER.Booking_Type  = 'FESTIVE ORDER' then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end  Booking_Type  ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name, Convert (decimal(18,2),(isnull (Source_Conv.Conversion_Factor,0)/ nullif ( isnull (Target_Conv.Conversion_Factor,0),0) ) * TSPL_BOOKING_DETAIL.Booking_Qty) as Booking_Qty,  TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,'LTR' as Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  Left outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on 	Source_Conv.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and 	Source_Conv.UOM_Code = TSPL_BOOKING_DETAIL.Unit_code Left Outer Join TSPL_ITEM_UOM_DETAIL as Target_Conv on   Target_Conv.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_Conv.Uom_code = 'LTR' where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  and tspl_booking_matser.TruckSheetGenerate=1  " + whr + " )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by  XFinal.route_no, XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )  XXXXX  ) XXXXX2 group by Document_Date, route_no   " &
                      " Union all  " &
                     " Select  max(XXXXX2.DataSNO) as  DataSNO, XXXXX2.Document_Date,max( XXXXX2.Cust_Code) as Cust_Code ,max (XXXXX2.Customer_Name) as  Customer_Name,XXXXX2.route_no,max(XXXXX2.Route_Desc) as Route_Desc,max (XXXXX2.Zone_Code) as Zone_Code,max(XXXXX2.Zone_Name) as Zone_Name, max(XXXXX2.Item_Code) as Item_Code,max(XXXXX2.Alies_Name) as Alies_Name,sum(XXXXX2.CR) as CR ,Sum(XXXXX2.CD) as CD,sum(XXXXX2.SO) as SO,sum(XXXXX2.Cash) as Cash,sum(XXXXX2.Total) as Total,sum(XXXXX2.CrateQty_New)  as CrateQty_New , sum(XXXXX2.PendingPcsQty_New) as PendingPcsQty_New,sum(XXXXX2.Amount_with_Tax) as Amount_with_Tax, max(XXXXX2.Unit_code) as  Unit_code , 2 as RowCountDivideByFour  from (  select  XXXXX.Document_Date, 'Route Total' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,'' as Item_Code,'' as Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CrateQty_New  as CrateQty_New , PendingPcsQty as PendingPcsQty_New,XXXXX.Amount_with_Tax, '3' as  DataSNO, '' as  Unit_code, 0 as  LR_PQ_ES from (    Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  ,Convert (int,CrateQty) as CrateQty_New , (XXXFinal.Total) - ((StockPcs.Conversion_Factor * Convert (int,CrateQty))) as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New  from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when TSPL_BOOKING_MATSER.Booking_Type  = 'FESTIVE ORDER' then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end  Booking_Type  ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name, Convert (decimal(18,2),(isnull (Source_Conv.Conversion_Factor,0)/ nullif ( isnull (Target_Conv.Conversion_Factor,0),0) ) * TSPL_BOOKING_DETAIL.Booking_Qty) as Booking_Qty,  TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,'LTR' as Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  Left outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on 	Source_Conv.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and 	Source_Conv.UOM_Code = TSPL_BOOKING_DETAIL.Unit_code Left Outer Join TSPL_ITEM_UOM_DETAIL as Target_Conv on   Target_Conv.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_Conv.Uom_code = 'LTR' where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  and tspl_booking_matser.TruckSheetGenerate=1  " + whr + " )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by  XFinal.route_no, XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )  XXXXX  ) XXXXX2 group by Document_Date, route_no   " &
                     " Union All  " &
                     " select '6' as  DataSNO,max (XXXXX.Date) as Document_Date, max([Customer Code]) as  Cust_Code, max ([Customer Name]) as Customer_Name , XXXXX.[Route Code] as route_no ,max( XXXXX.[Route Name]) as Route_Desc, '' as Zone_Code,'' as Zone_Name,'ItemCode' as Item_Code,'' as Alies_Name,0 as CR,0 as CD,0 as SO,0 as Cash,0 as Total,0  as CrateQty_New , 0 as PendingPcsQty_New,0 as Amount_with_Tax,  '' as    Unit_code ,  0 as RowCountDivideByFour   from (   Select convert(varchar,Doc_Date,103) as Date,Route_No as [Route Code],Route_Desc as [Route Name], Vehicle_Code as [Vehicle Code],Vehicle_Name as [Vehicle Name], 'XCrates Route Details' as [Customer Code],Customer_Name as [Customer Name],Customer_Phone as [Customer Phone], ZSM as [ZSM Code], ZSM_Name as [ZSM Name],ZSM_Phone as [ZSM Phone],ASM as [ASM Code],ASM_NAME as [ASM Name],ASM_Phone as [ASM Phone],ASO as [ASO Code],ASO_Name as [ASO Name],ASO_Phone as [ASO Phone], OpencrateQty,OpenJaaliQty,OpenBoxQty,OpenCanQty, CrateQtyRecd, JaaliQtyRecd,BoxQtyRecd, CanQtyRecd  ,CrateOutQty, jaaliOutQty,boxOutQty,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty,CanAdjQty, OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty as CrateQtyClosing, OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty as CanQtyClosing , OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty as JaaliQtyClosing, OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty as BoxQtyClosing, (OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty)* 0 as CrateValueClosing, (OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty)* 0 as JaaliValueClosing, (OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty)* 0 as BoxValueClosing ,  (OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty)* 0 as CanValueClosing ,OpencrateQtySum,OpenCanQtySum ,OpencrateQtySumOfVehicleWise,OpencanQtySumOfVehicleWise  from (Select  case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySum, case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySum, case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySumOfVehicleWise, case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySumOfVehicleWise, CTETemp.RowNo,CTETemp.Doc_Date, CTETemp.Route_No,CTETemp.Route_Desc ,CTETemp.Vehicle_Code,CTETemp.Vehicle_Name ,CTETemp.Customer_Code,CTETemp.Customer_Name, CTETemp.Customer_Phone, CTETemp.ZSM,CTETemp.ZSM_NAME,CTETemp.ZSM_Phone,CTETemp.ASM,CTETemp.ASM_NAME,CTETemp.ASM_Phone,CTETemp.ASO,CTETemp.ASO_NAME,CTETemp.ASO_Phone, CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) as OpencrateQty,  CTETemp.OpenJaaliQty+ISNULL(CT1.JaaliQtyClosing,0) as OpenJaaliQty,  CTETemp.OpenBoxQty+ISNULL(CT1.BoxQtyClosing,0) as OpenBoxQty, CTETemp.OpenCanQty+ISNULL(CT1.CanQtyClosing,0) as OpenCanQty,  CTETemp.CrateQtyRecd, CTETemp.JaaliQtyRecd, CTETemp.BoxQtyRecd,CTETemp.CanQtyRecd, CTETemp.CrateOutQty, CTETemp.jaaliOutQty, CTETemp.boxOutQty, CTETemp.CanOutQty, CTETemp.CrateAdjQty , CTETemp.JaaliAdjQty  , CTETemp.BoxAdjQty, CTETemp.CanAdjQty  from CTETemp LEFt OUTER JOIN CTETemp CT1 ON  CT1.Customer_Code=CTETemp.Customer_Code and  CT1.Vehicle_Code = CTETemp.Vehicle_Code  and CT1.Route_No  = CTETemp.Route_No  AND (CTETemp.RowNo-CT1.RowNo)=1 ) ZZZ   ) XXXXX  Group by  XXXXX.[Route Code]   " &
                     "  Union All " &
                     " select '3' as  DataSNO,max (XXXXX.Date) as Document_Date, max([Customer Code]) as  Cust_Code, max ([Customer Name]) as Customer_Name , XXXXX.[Route Code] as route_no ,max( XXXXX.[Route Name]) as Route_Desc, '' as Zone_Code,'' as Zone_Name,'ItemCode' as Item_Code,'' as Alies_Name,0 as CR,0 as CD,0 as SO,0 as Cash,0 as Total,0  as CrateQty_New , 0 as PendingPcsQty_New,0 as Amount_with_Tax,  '' as    Unit_code,  3 as RowCountDivideByFour    from (   Select convert(varchar,Doc_Date,103) as Date,Route_No as [Route Code],Route_Desc as [Route Name], Vehicle_Code as [Vehicle Code],Vehicle_Name as [Vehicle Name], 'Route Total' as [Customer Code],Customer_Name as [Customer Name],Customer_Phone as [Customer Phone], ZSM as [ZSM Code], ZSM_Name as [ZSM Name],ZSM_Phone as [ZSM Phone],ASM as [ASM Code],ASM_NAME as [ASM Name],ASM_Phone as [ASM Phone],ASO as [ASO Code],ASO_Name as [ASO Name],ASO_Phone as [ASO Phone], OpencrateQty,OpenJaaliQty,OpenBoxQty,OpenCanQty, CrateQtyRecd, JaaliQtyRecd,BoxQtyRecd, CanQtyRecd  ,CrateOutQty, jaaliOutQty,boxOutQty,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty,CanAdjQty, OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty as CrateQtyClosing, OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty as CanQtyClosing , OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty as JaaliQtyClosing, OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty as BoxQtyClosing, (OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty)* 0 as CrateValueClosing, (OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty)* 0 as JaaliValueClosing, (OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty)* 0 as BoxValueClosing ,  (OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty)* 0 as CanValueClosing ,OpencrateQtySum,OpenCanQtySum ,OpencrateQtySumOfVehicleWise,OpencanQtySumOfVehicleWise  from (Select  case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySum, case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySum, case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySumOfVehicleWise, case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySumOfVehicleWise, CTETemp.RowNo,CTETemp.Doc_Date, CTETemp.Route_No,CTETemp.Route_Desc ,CTETemp.Vehicle_Code,CTETemp.Vehicle_Name ,CTETemp.Customer_Code,CTETemp.Customer_Name, CTETemp.Customer_Phone, CTETemp.ZSM,CTETemp.ZSM_NAME,CTETemp.ZSM_Phone,CTETemp.ASM,CTETemp.ASM_NAME,CTETemp.ASM_Phone,CTETemp.ASO,CTETemp.ASO_NAME,CTETemp.ASO_Phone, CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) as OpencrateQty,  CTETemp.OpenJaaliQty+ISNULL(CT1.JaaliQtyClosing,0) as OpenJaaliQty,  CTETemp.OpenBoxQty+ISNULL(CT1.BoxQtyClosing,0) as OpenBoxQty, CTETemp.OpenCanQty+ISNULL(CT1.CanQtyClosing,0) as OpenCanQty,  CTETemp.CrateQtyRecd, CTETemp.JaaliQtyRecd, CTETemp.BoxQtyRecd,CTETemp.CanQtyRecd, CTETemp.CrateOutQty, CTETemp.jaaliOutQty, CTETemp.boxOutQty, CTETemp.CanOutQty, CTETemp.CrateAdjQty , CTETemp.JaaliAdjQty  , CTETemp.BoxAdjQty, CTETemp.CanAdjQty  from CTETemp LEFt OUTER JOIN CTETemp CT1 ON  CT1.Customer_Code=CTETemp.Customer_Code and  CT1.Vehicle_Code = CTETemp.Vehicle_Code  and CT1.Route_No  = CTETemp.Route_No  AND (CTETemp.RowNo-CT1.RowNo)=1 ) ZZZ   ) XXXXX  Group by  XXXXX.[Route Code]   " &
                     "   " &
                     "   " &
                     " )XXXXXFinal  group by XXXXXFinal.route_no, XXXXXFinal.Cust_Code, XXXXXFinal.DataSNO   " &
                     " ) " &
                     " TBL_Count on TBL_Count.DataSNO = XXXXXFinal.DataSNO and TBL_Count.route_no = XXXXXFinal.route_no and TBL_Count.Cust_Code = XXXXXFinal.Cust_Code " &
                     " left Outer  Join  TSPL_ITEM_MASTER  on  TSPL_ITEM_MASTER.Item_Code = XXXXXFinal.Item_Code " &
                     " Order By XXXXXFinal.route_no,XXXXXFinal.Cust_Code,XXXXXFinal.DataSNO , case when TSPL_ITEM_MASTER.SKU_Seq = 0 then 10000 else  TSPL_ITEM_MASTER.SKU_Seq end  "

            Dim dtMain As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dtMain IsNot Nothing And dtMain.Rows.Count > 0 Then
                Dim subPath As String = "C:\\ERPTempFolder"
                Dim IsExists As Boolean = System.IO.Directory.Exists(subPath)
                If (IsExists = False) Then
                    System.IO.Directory.CreateDirectory(subPath)
                End If
                subPath += "\\" & clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddhhmmsss") & ".Txt"
                IsExists = System.IO.File.Exists(subPath)
                If IsExists Then
                    System.IO.File.Delete(subPath)
                End If
                'WriteDataToFile(dtMain, "C:\ERPTempFolder\MyTestfile.Txt")
                WriteDataToFile(dtMain, subPath)
                Process.Start(subPath)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
        End If
    End Sub
    Public Sub PostDairySale()

        'clsCommon.ProgressBarHide()
        clsCommon.ProgressBarPercentShow()
        Try
            Dim x As Integer = 0
            Dim RecordCount As Integer = trnsLst.Count
            For j As Integer = 0 To trnsLst.Count - 1
                Try
                    Dim strDocNo As String = trnsLst.Item(j)
                    Dim strCustomerCode As String = trnsLstCustomer.Item(j)
                    Try
                        x = x + 1
                        clsCommon.ProgressBarPercentUpdate(x / RecordCount * 100, " Posting Record(s) " & j + 1 & " of Total " & RecordCount)
                        clsBulkPostingDairySale.PostingAndDOCreation(strDocNo, strCustomerCode)
                    Catch ex As Exception
                        Throw New Exception(ex.Message)
                    End Try


                    countPostedDoc = countPostedDoc + 1
                Catch ex As Exception
                    'dr = DtError.NewRow()
                    'dr("Code") = strDocNo
                    'dr("Error") = ex.Message
                    'DtError.Rows.Add(dr)
                End Try
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return
        End Try
        clsCommon.ProgressBarPercentHide()

    End Sub
    Sub LoadABSCombo()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Milk Abstract"
        'dr("Code") = "Milk Abstract Route Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Product Abstract"
        'dr("Code") = "Product Abstract Route Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Total Abstract"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        'dr("Code") = "Milk Abstract"
        dr("Code") = "Milk Abstract Route Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        'dr("Code") = "Product Abstract"
        dr("Code") = "Product Abstract Route Wise"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "Loading Report"
        dt.Rows.Add(dr)

        cboABSReportType.DataSource = dt.Copy()
        cboABSReportType.ValueMember = "Code"
        cboABSReportType.DisplayMember = "Code"

        dt.Rows.Clear()
        dr = dt.NewRow()
        dr("Code") = "NA"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Evening"
        dt.Rows.Add(dr)

        cboABSReportShift.DataSource = dt.Copy()
        cboABSReportShift.ValueMember = "Code"
        cboABSReportShift.DisplayMember = "Code"
        dt = Nothing
    End Sub

    Private Sub cboABSReportType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboABSReportType.SelectedValueChanged
        If clsCommon.CompairString(clsCommon.myCstr(cboABSReportType.SelectedValue), "Milk Abstract Route Wise") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboABSReportType.SelectedValue), "Product Abstract Route Wise") = CompairStringResult.Equal Then
            cboABSReportShift.SelectedValue = "Morning"
        Else
            cboABSReportShift.SelectedValue = "NA"
        End If
    End Sub

    Private Sub cboABSReportShift_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboABSReportShift.SelectedValueChanged
        If clsCommon.CompairString(clsCommon.myCstr(cboABSReportShift.SelectedValue), "Morning") = CompairStringResult.Equal Then
            txtABSDateFromTime.Checked = True
            txtABSDateFromTime.Value = New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0)

            txtABSDateToTime.Checked = True
            txtABSDateToTime.Value = New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 59, 0)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboABSReportShift.SelectedValue), "Evening") = CompairStringResult.Equal Then
            txtABSDateFromTime.Checked = True
            txtABSDateFromTime.Value = New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 0, 0)

            txtABSDateToTime.Checked = True
            txtABSDateToTime.Value = New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 59, 0)
        Else
            txtABSDateFromTime.Checked = False
            txtABSDateToTime.Checked = False
        End If
    End Sub

    Private Sub txtABSLocation__My_Click(sender As Object, e As EventArgs) Handles txtABSLocation._My_Click
        strQry = "select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER"
        txtABSLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultLocation", strQry, "Code", "Name", txtABSLocation.arrValueMember, txtABSLocation.arrDispalyMember)
    End Sub

    Private Sub txtABSZone__My_Click(sender As Object, e As EventArgs) Handles txtABSZone._My_Click
        strQry = "select zone_code as Code,Description as Name from TSPL_ZONE_MASTER"
        txtABSZone.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultLocation", strQry, "Code", "Name", txtABSZone.arrValueMember, txtABSZone.arrDispalyMember)
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        Try
            If chkShowEarlyRoute.Checked = True Then
                StrEarlyRoute = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @temp VARCHAR(MAX) SET @temp =(SELECT ',''' + cast(Route_No as varchar) + '''' FROM TSPL_ROUTE_MASTER where IsEarlyRoute=1 order by Route_No FOR XML PATH('')) SELECT SUBSTRING(@temp, 2, 200000) AS Route"))
            Else
                StrNonEarlyRoute = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @temp VARCHAR(MAX) SET @temp =(SELECT ',''' + cast(Route_No as varchar) + '''' FROM TSPL_ROUTE_MASTER where IsEarlyRoute=0 order by Route_No FOR XML PATH('')) SELECT SUBSTRING(@temp, 2, 200000) AS Route"))
            End If
            If chkShowEarlyRoute.Checked = True AndAlso clsCommon.myLen(StrEarlyRoute) <= 0 Then
                Throw New Exception("Early Route Not Found")
            ElseIf chkShowEarlyRoute.Checked = False AndAlso clsCommon.myLen(StrNonEarlyRoute) <= 0 Then
                Throw New Exception("Non Early Route Not Found")
            End If
            Dim whr As String = ""
            Dim whr_LR_PQ_ES As String = ""
            Dim whr_Valid_Cust As String = ""
            Dim strFromDate As String = ""
            Dim strToDate As String = ""
            Dim strFromDateCD As String = ""
            Dim strToDateCD As String = ""
            Dim strToDateForEvening As String = ""
            Dim strFromtime As String = ""
            Dim strToTime As String = ""
            Dim strShift As String = ""
            If txtABSDateFromTime.Checked = True Then
                strFromtime = clsCommon.GetPrintDate(txtABSDateFromTime.Value, "HH:mm")
            Else
                If clsCommon.CompairString(cboABSReportShift.SelectedValue, "NA") = CompairStringResult.Equal Then
                    strFromtime = "08:00"
                ElseIf clsCommon.CompairString(cboABSReportShift.SelectedValue, "Morning") = CompairStringResult.Equal Then
                    strFromtime = "08:00"
                ElseIf clsCommon.CompairString(cboABSReportShift.SelectedValue, "Evening") = CompairStringResult.Equal Then
                    strFromtime = "20:00"
                End If
            End If
            If txtABSDateToTime.Checked = True Then
                strToTime = clsCommon.GetPrintDate(txtABSDateToTime.Value, "HH:mm")
            Else
                If clsCommon.CompairString(cboABSReportShift.SelectedValue, "NA") = CompairStringResult.Equal Then
                    strToTime = "07:59"
                ElseIf clsCommon.CompairString(cboABSReportShift.SelectedValue, "Morning") = CompairStringResult.Equal Then
                    strToTime = "19:59"
                ElseIf clsCommon.CompairString(cboABSReportShift.SelectedValue, "Evening") = CompairStringResult.Equal Then
                    strToTime = "07:59"
                End If
            End If

            strFromDate = txtABSDate.Value + " " + strFromtime
            strToDate = txtABSDate.Value + " " + strToTime
            strFromDateCD = txtABSDate.Value + " " + "00:00"
            strToDateCD = txtABSDate.Value + " " + "23:59"
            strToDateForEvening = DateAdd("d", 1, txtABSDate.Value) + " " + strToTime


            'Dim strFromDate As String = txtABSDate.Value + " " + clsCommon.GetPrintDate(txtABSDateFromTime.Value, "HH:mm")
            'Dim strToDate As String = txtABSDate.Value + " " + clsCommon.GetPrintDate(txtABSDateToTime.Value, "HH:mm")
            ' Dim strToDateForEvening As String = DateAdd("d", 1, txtABSDate.Value) + " " + clsCommon.GetPrintDate(txtABSDateToTime.Value, "HH:mm")



            If clsCommon.CompairString(cboABSReportShift.SelectedValue, "Morning") = CompairStringResult.Equal Then
                'whr += " and  Document_Date  between  '" + clsCommon.GetPrintDate(strFromDate, "dd/MMM/yyyy HH:mm") + "'  and  '" + clsCommon.GetPrintDate(strToDate, "dd/MMM/yyyy HH:mm") + "' "
                whr += " and convert (datetime,Document_Date,103) >= case when TSPL_BOOKING_MATSER.Booking_Type = 'CD'  then convert (datetime,'" + clsCommon.GetPrintDate(strFromDateCD, "dd/MMM/yyyy HH:mm") + "',103) else convert (datetime, '" + clsCommon.GetPrintDate(strFromDate, "dd/MMM/yyyy HH:mm") + "',103 ) end  and convert (datetime,Document_Date,103) <= case when TSPL_BOOKING_MATSER.Booking_Type = 'CD'  then convert (datetime,'" + clsCommon.GetPrintDate(strToDateCD, "dd/MMM/yyyy HH:mm") + "' ,103) else convert (datetime, '" + clsCommon.GetPrintDate(strToDate, "dd/MMM/yyyy HH:mm") + "'  ) end  "
                strShift = "MORNING"
                'whr_LR_PQ_ES += " and  TSPL_SD_SHIPMENT_HEAD.Document_Date  between  '" + clsCommon.GetPrintDate(strFromDate, "dd/MMM/yyyy HH:mm") + "'  and  '" + clsCommon.GetPrintDate(strToDate, "dd/MMM/yyyy HH:mm") + "' "
                whr_LR_PQ_ES += " and  convert (datetime,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)  >= convert (datetime,'" + clsCommon.GetPrintDate(strFromDateCD, "dd/MMM/yyyy HH:mm") + "',103)  and convert (datetime,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert (datetime,'" + clsCommon.GetPrintDate(strToDateCD, "dd/MMM/yyyy HH:mm") + "',103) "
            ElseIf clsCommon.CompairString(cboABSReportShift.SelectedValue, "Evening") = CompairStringResult.Equal Then
                ' whr += " and  Document_Date  between  '" + clsCommon.GetPrintDate(strFromDate, "dd/MMM/yyyy HH:mm") + "'  and  '" + clsCommon.GetPrintDate(strToDateForEvening, "dd/MMM/yyyy HH:mm") + "' "
                whr += " and convert (datetime,Document_Date,103) >= case when TSPL_BOOKING_MATSER.Booking_Type = 'CD'  then convert (datetime,'" + clsCommon.GetPrintDate(strFromDateCD, "dd/MMM/yyyy HH:mm") + "',103) else convert (datetime, '" + clsCommon.GetPrintDate(strFromDate, "dd/MMM/yyyy HH:mm") + "',103 ) end  and convert (datetime,Document_Date,103) <= case when TSPL_BOOKING_MATSER.Booking_Type = 'CD'  then convert (datetime,'" + clsCommon.GetPrintDate(strToDateCD, "dd/MMM/yyyy HH:mm") + "' ,103) else convert (datetime, '" + clsCommon.GetPrintDate(strToDateForEvening, "dd/MMM/yyyy HH:mm") + "'  ) end  "
                strShift = "EVENING"
                'whr_LR_PQ_ES += " and  TSPL_SD_SHIPMENT_HEAD.Document_Date  between  '" + clsCommon.GetPrintDate(strFromDate, "dd/MMM/yyyy HH:mm") + "'  and  '" + clsCommon.GetPrintDate(strToDateForEvening, "dd/MMM/yyyy HH:mm") + "' "
                whr_LR_PQ_ES += " and  convert (datetime,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)  >= convert (datetime,'" + clsCommon.GetPrintDate(strFromDateCD, "dd/MMM/yyyy HH:mm") + "',103)  and convert (datetime,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert (datetime,'" + clsCommon.GetPrintDate(strToDateCD, "dd/MMM/yyyy HH:mm") + "',103) "
            ElseIf clsCommon.CompairString(cboABSReportShift.SelectedValue, "NA") = CompairStringResult.Equal Then
                'whr += " and  Document_Date  between  '" + clsCommon.GetPrintDate(strFromDate, "dd/MMM/yyyy HH:mm") + "'  and  '" + clsCommon.GetPrintDate(strToDateForEvening, "dd/MMM/yyyy HH:mm") + "' "
                whr += " and convert (datetime,Document_Date,103) >= case when TSPL_BOOKING_MATSER.Booking_Type = 'CD'  then convert (datetime,'" + clsCommon.GetPrintDate(strFromDateCD, "dd/MMM/yyyy HH:mm") + "',103) else convert (datetime, '" + clsCommon.GetPrintDate(strFromDate, "dd/MMM/yyyy HH:mm") + "',103 ) end  and convert (datetime,Document_Date,103) <= case when TSPL_BOOKING_MATSER.Booking_Type = 'CD'  then convert (datetime,'" + clsCommon.GetPrintDate(strToDateCD, "dd/MMM/yyyy HH:mm") + "' ,103) else convert (datetime, '" + clsCommon.GetPrintDate(strToDateForEvening, "dd/MMM/yyyy HH:mm") + "'  ) end  "
                'whr_LR_PQ_ES += " and  TSPL_SD_SHIPMENT_HEAD.Document_Date  between  '" + clsCommon.GetPrintDate(strFromDate, "dd/MMM/yyyy HH:mm") + "'  and  '" + clsCommon.GetPrintDate(strToDateForEvening, "dd/MMM/yyyy HH:mm") + "' "
                whr_LR_PQ_ES += " and  convert (datetime,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)  >= convert (datetime,'" + clsCommon.GetPrintDate(strFromDateCD, "dd/MMM/yyyy HH:mm") + "',103)  and convert (datetime,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert (datetime,'" + clsCommon.GetPrintDate(strToDateCD, "dd/MMM/yyyy HH:mm") + "',103) "
            End If
            If txtABSLocation.arrValueMember IsNot Nothing AndAlso txtABSLocation.arrValueMember.Count > 0 Then
                whr += " and TSPL_BOOKING_DETAIL.Loc_Code  in (" + clsCommon.GetMulcallString(txtABSLocation.arrValueMember) + ") "
                whr_LR_PQ_ES += " and TSPL_SD_SHIPMENT_HEAD.bill_to_location  in (" + clsCommon.GetMulcallString(txtABSLocation.arrValueMember) + ") "
            End If
            If txtABSZone.arrValueMember IsNot Nothing AndAlso txtABSZone.arrValueMember.Count > 0 Then
                whr += " and TSPL_CUSTOMER_MASTER.Zone_Code  in (" + clsCommon.GetMulcallString(txtABSZone.arrValueMember) + ") "
                whr_LR_PQ_ES += " and TSPL_CUSTOMER_MASTER.Zone_Code  in (" + clsCommon.GetMulcallString(txtABSZone.arrValueMember) + ") "
            End If
            If txtRouteABS.arrValueMember IsNot Nothing AndAlso txtRouteABS.arrValueMember.Count > 0 Then
                whr += " and TSPL_BOOKING_DETAIL.route_No  in (" + clsCommon.GetMulcallString(txtRouteABS.arrValueMember) + ") "
                whr_LR_PQ_ES += " and TSPL_SD_SHIPMENT_HEAD.route_No  in (" + clsCommon.GetMulcallString(txtRouteABS.arrValueMember) + ") "
            Else
                If chkShowEarlyRoute.Checked = True Then
                    whr += " and TSPL_BOOKING_DETAIL.route_No  in (" + clsCommon.myCstr(StrEarlyRoute) + ") "
                    whr_LR_PQ_ES += " and TSPL_SD_SHIPMENT_HEAD.route_No  in (" + clsCommon.myCstr(StrEarlyRoute) + ") "
                Else
                    whr += " and TSPL_BOOKING_DETAIL.route_No  in (" + clsCommon.myCstr(StrNonEarlyRoute) + ") "
                    whr_LR_PQ_ES += " and TSPL_SD_SHIPMENT_HEAD.route_No  in (" + clsCommon.myCstr(StrNonEarlyRoute) + ") "
                End If
            End If
            whr_Valid_Cust = whr
            If clsCommon.CompairString(cboABSReportType.SelectedValue, "Milk Abstract Route Wise") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboABSReportType.SelectedValue, "Milk Abstract") = CompairStringResult.Equal Then
                whr += " and TSPL_ITEM_MASTER. Is_Milk_Pouch =1  "
                whr_LR_PQ_ES += " and TSPL_ITEM_MASTER. Is_Milk_Pouch =1  "
            ElseIf clsCommon.CompairString(cboABSReportType.SelectedValue, "Product Abstract Route Wise") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboABSReportType.SelectedValue, "Product Abstract") = CompairStringResult.Equal Then
                whr += " and TSPL_ITEM_MASTER. Is_Milk_Pouch =0  "
                whr_LR_PQ_ES += " and TSPL_ITEM_MASTER. Is_Milk_Pouch =0  "

            End If
            Dim qryAllRouteTotal As String = ""
            Dim qry As String = " select distinct '-' as Desh, XXXXXFinal.Document_Date,XXXXXFinal.Cust_Code, XXXXXFinal.Customer_Name as Customer_Name,  XXXXXFinal.route_no  as route_no ,  XXXXXFinal.Route_Desc  as Route_Desc,XXXXXFinal.Zone_Code,XXXXXFinal.Zone_Name,XXXXXFinal.Item_Code,SUBSTRING (XXXXXFinal.Alies_Name,0,8) as Alies_Name, Convert (integer,XXXXXFinal.CR) as CR,Convert (Integer ,XXXXXFinal.CD) as CD,convert (Integer,XXXXXFinal.SO) as SO,Convert (Integer ,XXXXXFinal.Cash,103) as Cash ,Convert (Integer,XXXXXFinal.Total) as Total,XXXXXFinal.CrateQty_New as CrateQty_New , XXXXXFinal.PendingPcsQty_New as PendingPcsQty_New, XXXXXFinal.Amount_with_Tax, XXXXXFinal.  DataSNO,   XXXXXFinal. Unit_code, XXXXXFinal.LtrQty from (  "
            qryAllRouteTotal = "  select distinct '-' as Desh, XXXXXFinal.Document_Date,XXXXXFinal.Cust_Code, XXXXXFinal.Customer_Name as Customer_Name,  'Route Total'  as route_no ,  'Route Total'  as Route_Desc,XXXXXFinal.Zone_Code,XXXXXFinal.Zone_Name,XXXXXFinal.Item_Code,SUBSTRING (XXXXXFinal.Alies_Name,0,8) as Alies_Name, Convert (integer,XXXXXFinal.CR) as CR,Convert (Integer ,XXXXXFinal.CD) as CD,convert (Integer,XXXXXFinal.SO) as SO,Convert (Integer ,XXXXXFinal.Cash,103) as Cash ,Convert (Integer,XXXXXFinal.Total) as Total,XXXXXFinal.CrateQty_New as CrateQty_New , XXXXXFinal.PendingPcsQty_New as PendingPcsQty_New, XXXXXFinal.Amount_with_Tax, '4'  DataSNO,   XXXXXFinal. Unit_code, XXXXXFinal.LtrQty from ( "
            qry += " select  XXXXX.Document_Date, 'Route Total' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CEILING_Crate_Qty  as CrateQty_New , PendingPcsQty as PendingPcsQty_New,XXXXX.Amount_with_Tax, '3' as  DataSNO, '' as  Unit_code,  XXXXX.LtrQty from (    Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  from "
            qryAllRouteTotal += " select  XXXXX.Document_Date, 'Route Total' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CEILING_Crate_Qty  as CrateQty_New , PendingPcsQty as PendingPcsQty_New,XXXXX.Amount_with_Tax, '3' as  DataSNO, '' as  Unit_code,  XXXXX.LtrQty from (    Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  from "
            qry += " ( "
            qryAllRouteTotal += " ( "
            qry += " Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty ,   (case when coalesce(StockLtr.Conversion_Factor,0)=0 then case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else   round((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)) ,2,1) end else round((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockLtr.Conversion_Factor,1)) ,2,1) end) as LtrQty " ' (case when coalesce(StockLtr.Conversion_Factor,0)=0 then 0 else round((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockLtr.Conversion_Factor,1)) ,2,1) end) as LtrQty 
            qryAllRouteTotal += " Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty ,   (case when coalesce(StockLtr.Conversion_Factor,0)=0 then case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else   round((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)) ,2,1) end else round((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockLtr.Conversion_Factor,1)) ,2,1) end) as LtrQty "
            qry += " from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , "
            qryAllRouteTotal += " from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , "
            If clsCommon.CompairString(cboABSReportType.SelectedValue, "Total Abstract") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboABSReportType.SelectedValue, "Milk Abstract") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboABSReportType.SelectedValue, "Product Abstract") = CompairStringResult.Equal Then
                qry += " max(XFinal.route_no ) as route_no "

            Else
                qry += " XFinal.route_no  "
                qryAllRouteTotal += " max(XFinal.route_no ) as route_no  "
            End If
            If clsCommon.CompairString(cboABSReportType.SelectedValue, "Loading Report") = CompairStringResult.Equal Then
                qry += " , max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No, Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when TSPL_BOOKING_MATSER.Booking_Type  = 'FESTIVE ORDER' then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end  Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,case when len (isnull (tspl_transport_master.Transporter_Name,'')) > 0 then (TSPL_ROUTE_MASTER.Route_Desc+' ('+tspl_transport_master.Transporter_Name+')') else TSPL_ROUTE_MASTER.Route_Desc end as Route_Desc,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end + case when isnull (TSPL_CUSTOMER_MASTER.IsTCSnotApplicable,0) = 0 and " + clsCommon.myCstr(ApplyTCSAmtOnAbstractReportDotMatrix) + " = 1  then  case when len (isnull(TSPL_CUSTOMER_MASTER.PAN,'')) > 0 then   TSPL_BOOKING_DETAIL.Amount_with_Tax * " + clsCommon.myCstr(settTCSRateforCustomerWithPanNo) + " /100 else TSPL_BOOKING_DETAIL.Amount_with_Tax * " + clsCommon.myCstr(settTCSRateforCustomerWithoutPanNo) + " /100 end  else 0 end  as Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_ROUTE_MASTER.vehicle_code left join tspl_transport_master on tspl_transport_master.transport_id=tspl_vehicle_master.transport_id left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others') "
                qryAllRouteTotal += " , max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when TSPL_BOOKING_MATSER.Booking_Type  = 'FESTIVE ORDER' then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end  Booking_Type ,TSPL_BOOKING_DETAIL.Route_No, case when len (isnull (tspl_transport_master.Transporter_Name,'')) > 0 then (TSPL_ROUTE_MASTER.Route_Desc+' ('+tspl_transport_master.Transporter_Name+')') else TSPL_ROUTE_MASTER.Route_Desc end as Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end  + case when isnull ( TSPL_CUSTOMER_MASTER.IsTCSnotApplicable,0) = 0  and " + clsCommon.myCstr(ApplyTCSAmtOnAbstractReportDotMatrix) + " = 1   then  case when len (isnull(TSPL_CUSTOMER_MASTER.PAN,'')) > 0 then   TSPL_BOOKING_DETAIL.Amount_with_Tax * " + clsCommon.myCstr(settTCSRateforCustomerWithPanNo) + " /100 else TSPL_BOOKING_DETAIL.Amount_with_Tax * " + clsCommon.myCstr(settTCSRateforCustomerWithoutPanNo) + " /100 end  else 0 end  as Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code   left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_ROUTE_MASTER.vehicle_code left join tspl_transport_master on tspl_transport_master.transport_id=tspl_vehicle_master.transport_id left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others') "
            Else
                qry += " , max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when TSPL_BOOKING_MATSER.Booking_Type  = 'FESTIVE ORDER' then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end  Booking_Type,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end + case when isnull (TSPL_CUSTOMER_MASTER.IsTCSnotApplicable,0) = 0  and " + clsCommon.myCstr(ApplyTCSAmtOnAbstractReportDotMatrix) + " = 1   then  case when len (isnull(TSPL_CUSTOMER_MASTER.PAN,'')) > 0 then   TSPL_BOOKING_DETAIL.Amount_with_Tax * " + clsCommon.myCstr(settTCSRateforCustomerWithPanNo) + " /100 else TSPL_BOOKING_DETAIL.Amount_with_Tax * " + clsCommon.myCstr(settTCSRateforCustomerWithoutPanNo) + " /100 end  else 0 end  as Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others') "
                qryAllRouteTotal += " , max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when TSPL_BOOKING_MATSER.Booking_Type  = 'FESTIVE ORDER' then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end  Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end + case when  isnull (TSPL_CUSTOMER_MASTER.IsTCSnotApplicable,0) = 0  and " + clsCommon.myCstr(ApplyTCSAmtOnAbstractReportDotMatrix) + " = 1   then  case when len (isnull(TSPL_CUSTOMER_MASTER.PAN,'')) > 0 then   TSPL_BOOKING_DETAIL.Amount_with_Tax * " + clsCommon.myCstr(settTCSRateforCustomerWithPanNo) + " /100 else TSPL_BOOKING_DETAIL.Amount_with_Tax * " + clsCommon.myCstr(settTCSRateforCustomerWithoutPanNo) + " /100 end  else 0 end  as Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others') "
            End If
            qry += " " + whr + " "
            qryAllRouteTotal += " " + whr + " "
            qry += "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by   "
            qryAllRouteTotal += "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by   "
            If clsCommon.CompairString(cboABSReportType.SelectedValue, "Total Abstract") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboABSReportType.SelectedValue, "Milk Abstract") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboABSReportType.SelectedValue, "Product Abstract") = CompairStringResult.Equal Then

            Else
                qry += "  XFinal.route_no, "
            End If

            qry += "  XFinal.Item_Code,Unit_code  ) XXFinal "
            qryAllRouteTotal += "  XFinal.Item_Code,Unit_code  ) XXFinal "
            qry += " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  "
            qryAllRouteTotal += " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  "
            qry += " left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code "
            qryAllRouteTotal += " left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code "
            qry += " left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='Ltr') as StockLtr on XXFinal.Item_Code =StockLtr.Item_Code "
            qryAllRouteTotal += " left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='Ltr') as StockLtr on XXFinal.Item_Code =StockLtr.Item_Code "
            qry += " left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG') as StockKG on XXFinal.Item_Code =StockKG.Item_Code "
            qryAllRouteTotal += " left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG') as StockKG on XXFinal.Item_Code =StockKG.Item_Code "
            qry += " ) XXXFinal  "
            qryAllRouteTotal += " ) XXXFinal  "
            qry += " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code "
            qryAllRouteTotal += " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code "
            qry += " )XXXXX  "
            qryAllRouteTotal += " )XXXXX  "
            qry += " )XXXXXFinal "
            qryAllRouteTotal += " )XXXXXFinal "

            'qryAllRouteTotal += " where  2=2 Order By XXXXXFinal.DataSNO , XXXXXFinal.route_no "
            If clsCommon.CompairString(cboABSReportType.SelectedValue, "Milk Abstract Route Wise") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboABSReportType.SelectedValue, "Product Abstract Route Wise") = CompairStringResult.Equal Then
                Dim Qry_LR_PQ_ES As String = "  With CTETemp as (      "
                Qry_LR_PQ_ES += " select  XXXXX.route_no, XXXXX.Alies_Name + ' : ' + convert (varchar,LR_PQ_ES) as LR_PQ_ES_Desc  from ( Select  'Total Route Leakage' as Cust_Code, 'Total Route Leakage' as Customer_Name ,Route_No,max(Route_Desc) as Route_Desc  , Item_Code, max (Alies_Name) as Alies_Name , sum (CR) as CR,sum (CD) as CD,Sum(SO) as SO ,sum (Cash) as Cash, sum(Total) as Total ,sum( CrateQty_New) as CrateQty_New,sum( PendingPcsQty_New) as PendingPcsQty_New, sum (Amount_with_Tax) as Amount_with_Tax,Unit_code,sum (LR_PQ_ES) as LR_PQ_ES  from ( Select convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 0 as CR,0 as CD,0 as SO,0 as Cash,0 as Total,0  as CrateQty_New , 0 as PendingPcsQty_New,0 as Amount_with_Tax, 'PCS' as  Unit_code , Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty) as LR_PQ_ES  from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='PCS' inner join ( Select distinct TSPL_BOOKING_DETAIL.Cust_Code  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No    left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  " + whr_Valid_Cust + "   ) TBL_VALID_Cust on TBL_VALID_Cust.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code where  TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others') and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1  " + whr_LR_PQ_ES + "  and TSPL_SD_SHIPMENT_HEAD.status = 1    ) XFinal Group by XFinal.route_no,XFinal.Item_Code,XFinal.Unit_code ) XXXXX "
                Qry_LR_PQ_ES += " Union All "
                Qry_LR_PQ_ES += " select  XXXXX.route_no, XXXXX.Alies_Name + ' : ' + convert (varchar,LR_PQ_ES) as LR_PQ_ES_Desc  from ( Select  'Total Route Leakage' as Cust_Code, 'Total Route Leakage' as Customer_Name ,Route_No,max(Route_Desc) as Route_Desc  , Item_Code, max (Alies_Name) as Alies_Name , sum (CR) as CR,sum (CD) as CD,Sum(SO) as SO ,sum (Cash) as Cash, sum(Total) as Total ,sum( CrateQty_New) as CrateQty_New,sum( PendingPcsQty_New) as PendingPcsQty_New, sum (Amount_with_Tax) as Amount_with_Tax,Unit_code,sum (LR_PQ_ES) as LR_PQ_ES  from ( Select convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name , 'Route Total' as Route_No ,'Route Total' as Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 0 as CR,0 as CD,0 as SO,0 as Cash,0 as Total,0  as CrateQty_New , 0 as PendingPcsQty_New,0 as Amount_with_Tax, 'PCS' as  Unit_code , Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty) as LR_PQ_ES  from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='PCS' inner join ( Select distinct TSPL_BOOKING_DETAIL.Cust_Code  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No    left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  " + whr_Valid_Cust + "  ) TBL_VALID_Cust on TBL_VALID_Cust.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code where  TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others') and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1  " + whr_LR_PQ_ES + "  and TSPL_SD_SHIPMENT_HEAD.status = 1    ) XFinal Group by XFinal.route_no,XFinal.Item_Code,XFinal.Unit_code ) XXXXX "
                Qry_LR_PQ_ES += " ) "
                qry = Qry_LR_PQ_ES + " Select XXXXXXFinal.* ,(case when len(isnull (XXX_LR_PQ_ES.LR_PQ_ES_Desc,'')) >0 then 'Leakage : ' else '' end) + isnull (XXX_LR_PQ_ES.LR_PQ_ES_Desc,'') as LR_PQ_ES_Desc From ( " + qry + " Union All " + qryAllRouteTotal + "   ) XXXXXXFinal  Left Outer Join (select route_no,LR_PQ_ES_Desc from (  SELECT p1.route_no,( SELECT LR_PQ_ES_Desc + '  '  FROM CTETemp p2  WHERE p2.route_no = p1.route_no  ORDER BY LR_PQ_ES_Desc FOR XML PATH('') ) AS LR_PQ_ES_Desc  FROM CTETemp p1 GROUP BY route_no )TBL_LR_PQ_ES) XXX_LR_PQ_ES on XXX_LR_PQ_ES.route_no = XXXXXXFinal.route_no  where ( isnull(XXXXXXFinal.Total,0) > 0 or isnull (XXXXXXFinal.Amount_with_Tax,0) > 0)  Order By XXXXXXFinal.DataSNO , XXXXXXFinal.route_no  "
            ElseIf clsCommon.CompairString(cboABSReportType.SelectedValue, "Loading Report") = CompairStringResult.Equal Then
                Dim Qry_LR_PQ_ES As String = "  With CTETemp as (      "
                Qry_LR_PQ_ES += " select  XXXXX.route_no, XXXXX.Alies_Name + ' : ' + convert (varchar,LR_PQ_ES) as LR_PQ_ES_Desc  from ( Select  'Total Route Leakage' as Cust_Code, 'Total Route Leakage' as Customer_Name ,Route_No,max(Route_Desc) as Route_Desc  , Item_Code, max (Alies_Name) as Alies_Name , sum (CR) as CR,sum (CD) as CD,Sum(SO) as SO ,sum (Cash) as Cash, sum(Total) as Total ,sum( CrateQty_New) as CrateQty_New,sum( PendingPcsQty_New) as PendingPcsQty_New, sum (Amount_with_Tax) as Amount_with_Tax,Unit_code,sum (LR_PQ_ES) as LR_PQ_ES  from ( Select convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,(TSPL_ROUTE_MASTER.Route_Desc+' ('+tspl_transport_master.Transporter_Name+')') as Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 0 as CR,0 as CD,0 as SO,0 as Cash,0 as Total,0  as CrateQty_New , 0 as PendingPcsQty_New,0 as Amount_with_Tax, 'PCS' as  Unit_code , Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty) as LR_PQ_ES  from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_ROUTE_MASTER.vehicle_code left join tspl_transport_master on tspl_transport_master.transport_id=tspl_vehicle_master.transport_id left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='PCS' inner join ( Select distinct TSPL_BOOKING_DETAIL.Cust_Code  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No    left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  " + whr_Valid_Cust + "   ) TBL_VALID_Cust on TBL_VALID_Cust.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code where  TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others') and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1  " + whr_LR_PQ_ES + "  and TSPL_SD_SHIPMENT_HEAD.status = 1    ) XFinal Group by XFinal.route_no,XFinal.Item_Code,XFinal.Unit_code ) XXXXX "
                Qry_LR_PQ_ES += " Union All "
                Qry_LR_PQ_ES += " select  XXXXX.route_no, XXXXX.Alies_Name + ' : ' + convert (varchar,LR_PQ_ES) as LR_PQ_ES_Desc  from ( Select  'Total Route Leakage' as Cust_Code, 'Total Route Leakage' as Customer_Name ,Route_No,max(Route_Desc) as Route_Desc  , Item_Code, max (Alies_Name) as Alies_Name , sum (CR) as CR,sum (CD) as CD,Sum(SO) as SO ,sum (Cash) as Cash, sum(Total) as Total ,sum( CrateQty_New) as CrateQty_New,sum( PendingPcsQty_New) as PendingPcsQty_New, sum (Amount_with_Tax) as Amount_with_Tax,Unit_code,sum (LR_PQ_ES) as LR_PQ_ES  from ( Select convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name , 'Route Total' as Route_No ,'Route Total' as Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 0 as CR,0 as CD,0 as SO,0 as Cash,0 as Total,0  as CrateQty_New , 0 as PendingPcsQty_New,0 as Amount_with_Tax, 'PCS' as  Unit_code , Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty) as LR_PQ_ES  from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='PCS' inner join ( Select distinct TSPL_BOOKING_DETAIL.Cust_Code  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No    left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  " + whr_Valid_Cust + "  ) TBL_VALID_Cust on TBL_VALID_Cust.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code where  TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others') and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1  " + whr_LR_PQ_ES + "  and TSPL_SD_SHIPMENT_HEAD.status = 1    ) XFinal Group by XFinal.route_no,XFinal.Item_Code,XFinal.Unit_code ) XXXXX "
                Qry_LR_PQ_ES += " ) "
                qry = Qry_LR_PQ_ES + " Select XXXXXXFinal.* , isnull (XXX_LR_PQ_ES.LR_PQ_ES_Desc,'') as LR_PQ_ES_Desc From ( " + qry + " Union All " + qryAllRouteTotal + "   ) XXXXXXFinal  Left Outer Join (select route_no,LR_PQ_ES_Desc from (  SELECT p1.route_no,( SELECT LR_PQ_ES_Desc + '  '  FROM CTETemp p2  WHERE p2.route_no = p1.route_no  ORDER BY LR_PQ_ES_Desc FOR XML PATH('') ) AS LR_PQ_ES_Desc  FROM CTETemp p1 GROUP BY route_no )TBL_LR_PQ_ES) XXX_LR_PQ_ES on XXX_LR_PQ_ES.route_no = XXXXXXFinal.route_no where  ( isnull(XXXXXXFinal.Total,0) > 0 or isnull (XXXXXXFinal.Amount_with_Tax,0) > 0)  Order By XXXXXXFinal.DataSNO , XXXXXXFinal.route_no  "
            Else
                qry += " where  2=2  and ( isnull(XXXXXFinal.Total,0) > 0 or isnull (XXXXXFinal.Amount_with_Tax,0) > 0)  Order By XXXXXFinal.DataSNO , XXXXXFinal.route_no "
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then

                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = objCommonVar.CurrentCompanyName
                obj.ShowPageNo = True
                obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("TRUCK SHEET ABSTRACT OF ", clsCommon.GetPrintDate(txtABSDate.Value, "dd/MM/yyyy")))
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Shift ", clsCommon.myCstr(cboABSReportShift.SelectedValue)))
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Type", clsCommon.myCstr(cboABSReportType.SelectedValue)))

                obj.arrGroup = New List(Of clsDosPrintGroup)()
                If clsCommon.CompairString(cboABSReportType.SelectedValue, "Total Abstract") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboABSReportType.SelectedValue, "Milk Abstract") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboABSReportType.SelectedValue, "Product Abstract") = CompairStringResult.Equal Then
                Else
                    obj.arrGroup.Add(clsDosPrintGroup.GetObject("Route_Desc", "ROUTE", "", "LR_PQ_ES_Desc"))
                    'obj.arrGroup.Add(clsDosPrintGroup.GetObject("Zone_Code", "ZONE", ""))
                End If



                obj.arrColumn = New List(Of clsDosPrintColumn)()
                'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Route_Desc", "ROUTE", True, DosPrintAlignment.Left, 7, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Alies_Name", "TYPE", True, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("CR", "CR", False, DosPrintAlignment.Right, 9, True, DecimalPlaces.Zero))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("CD", "CD", False, DosPrintAlignment.Right, 9, True, DecimalPlaces.Zero))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SO", "SO", False, DosPrintAlignment.Right, 9, True, DecimalPlaces.Zero))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cash", "Cash", False, DosPrintAlignment.Right, 9, True, DecimalPlaces.Zero))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total", "Total", False, DosPrintAlignment.Right, 12, True, DecimalPlaces.Zero))

                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("CrateQty_New", "CRATES", False, DosPrintAlignment.Right, 9, True, DecimalPlaces.Zero))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Desh", "", False, DosPrintAlignment.Right, 3, True, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("PendingPcsQty_New", "   ", False, DosPrintAlignment.Left, 5, True, DecimalPlaces.Zero))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("LtrQty", "LITRES/KG", False, DosPrintAlignment.Right, 14, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Amount_with_Tax", "AMOUNT", False, DosPrintAlignment.Right, 14, True, DecimalPlaces.Two))

                If clsCommon.CompairString(cboABSReportType.SelectedValue, "Total Abstract") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboABSReportType.SelectedValue, "Milk Abstract") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboABSReportType.SelectedValue, "Product Abstract") = CompairStringResult.Equal Then
                    Dim strLRQty As Double = 0
                    Dim strPQQty As Double = 0
                    Dim strESQty As Double = 0
                    Dim strLRPQES_TotalQty As Double = 0


                    qry = " Select   1 as ID, isnull (sum(LR),0) as LR , isnull(sum (PQ),0) as PQ , isnull (Sum ( ES ),0) as ES ,isnull (sum(LR),0) + isnull(sum (PQ),0) + isnull (Sum ( ES ),0) as  Total_LR_PQ_ES_Qty from  " &
                          " ( " &
                          " Select TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_Code, convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 'LTR' as  Unit_code ,isnull( Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty),0) as LR , 0 as PQ , 0 as ES from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='LTR'  Left Outer Join TSPL_CUSTOMER_COMPLAINT_HEAD on TSPL_CUSTOMER_COMPLAINT_HEAD.complaint_no = TSPL_SD_SHIPMENT_HEAD.customer_complaint_no left outer join TSPL_CUSTOMER_COMPLAINT_detail on TSPL_CUSTOMER_COMPLAINT_detail.Complaint_no=TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_no and  TSPL_CUSTOMER_COMPLAINT_detail.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code  where   Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(txtABSDate.Value, "dd/MMM/yyyy") + "',103) and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(txtABSDate.Value, "dd/MMM/yyyy") + "',103)    and TSPL_SD_SHIPMENT_HEAD.status = 1     and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1 and TSPL_CUSTOMER_COMPLAINT_detail.Complaint_Code = 'LR' " + whr_LR_PQ_ES + " " &
                          " Union All " &
                          " Select TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_Code, convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 'LTR' as  Unit_code ,0 as LR , isnull( Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty),0) as PQ , 0 as ES from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='LTR'  Left Outer Join TSPL_CUSTOMER_COMPLAINT_HEAD on TSPL_CUSTOMER_COMPLAINT_HEAD.complaint_no = TSPL_SD_SHIPMENT_HEAD.customer_complaint_no left outer join TSPL_CUSTOMER_COMPLAINT_detail on TSPL_CUSTOMER_COMPLAINT_detail.Complaint_no=TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_no and  TSPL_CUSTOMER_COMPLAINT_detail.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code  where   Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(txtABSDate.Value, "dd/MMM/yyyy") + "',103) and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(txtABSDate.Value, "dd/MMM/yyyy") + "',103)    and TSPL_SD_SHIPMENT_HEAD.status = 1     and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1 and TSPL_CUSTOMER_COMPLAINT_detail.Complaint_Code = 'PQ' " + whr_LR_PQ_ES + " " &
                          " Union All " &
                          " Select TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_Code, convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 'LTR' as  Unit_code ,0 as LR , 0 as PQ , isnull( Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty),0) as ES from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='LTR'  Left Outer Join TSPL_CUSTOMER_COMPLAINT_HEAD on TSPL_CUSTOMER_COMPLAINT_HEAD.complaint_no = TSPL_SD_SHIPMENT_HEAD.customer_complaint_no left outer join TSPL_CUSTOMER_COMPLAINT_detail on TSPL_CUSTOMER_COMPLAINT_detail.Complaint_no=TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_no and  TSPL_CUSTOMER_COMPLAINT_detail.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code  where    Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(txtABSDate.Value, "dd/MMM/yyyy") + "',103) and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(txtABSDate.Value, "dd/MMM/yyyy") + "',103)   and TSPL_SD_SHIPMENT_HEAD.status = 1     and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1 and TSPL_CUSTOMER_COMPLAINT_detail.Complaint_Code = 'ES' " + whr_LR_PQ_ES + " " &
                          " )XXXX1 "
                    Dim dtLRPQES As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dtLRPQES IsNot Nothing And dtLRPQES.Rows.Count > 0 Then
                        strLRQty = clsCommon.myCstr(dtLRPQES.Rows(0)("LR"))
                        strPQQty = clsCommon.myCstr(dtLRPQES.Rows(0)("PQ"))
                        strESQty = clsCommon.myCstr(dtLRPQES.Rows(0)("ES"))
                        strLRPQES_TotalQty = clsCommon.myCstr(dtLRPQES.Rows(0)("Total_LR_PQ_ES_Qty"))
                    End If

                    obj.arrReportFooter = New List(Of clsDosPrintReportFooter)
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Scheme Quantity", "", "", "", ""))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("CASH Qty", "0", "", " ", ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Card Qty", "0", "", " ", ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Credit Qty", "0", "", " ", ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("SO Qty", "0", "", "", ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Credit Qty", "0", "", " ", ":"))

                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("-----------------------------------", "------------------------------------------------", "", "", ""))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Leakage Replacement Quantity(Ltrs)", strLRQty, "", "", ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Poor Quality Replacement", strPQQty, "", "", ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Market Returns Replacement", strESQty, "", "", ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("-----------------------------------", "---------------------", "------------", "", ""))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("TOTAL", strLRPQES_TotalQty, "", "", ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("-----------------------------------", "---------------------", "------------", "", ""))
                End If

                obj.Print(obj, dt, PageSetup.Potrate)



            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

        'Dim qry As String = " select XXXXXFinal.Document_Date,XXXXXFinal.Cust_Code, XXXXXFinal.Customer_Name as Customer_Name, case when XXXXXFinal.DataSNO = 4 then 'ITEM TOTAL' else XXXXXFinal.route_no end as route_no , case when XXXXXFinal.DataSNO = 4 then 'ITEM TOTAL' else XXXXXFinal.Route_Desc end as Route_Desc,XXXXXFinal.Zone_Code,XXXXXFinal.Zone_Name,XXXXXFinal.Item_Code,SUBSTRING (XXXXXFinal.Alies_Name,0,8) as Alies_Name, Convert (integer,XXXXXFinal.CR) as CR,Convert (Integer ,XXXXXFinal.CD) as CD,convert (Integer,XXXXXFinal.SO) as SO,Convert (Integer ,XXXXXFinal.Cash,103) as Cash ,Convert (Integer,XXXXXFinal.Total) as Total,XXXXXFinal.CrateQty_New as CrateQty_New , XXXXXFinal.PendingPcsQty_New as PendingPcsQty_New, XXXXXFinal.Amount_with_Tax, XXXXXFinal.  DataSNO,   XXXXXFinal. Unit_code,TBL_Count.No_Of_Item from (  " & _
        '              " select XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CrateQty_New as CrateQty_New , XXXXX.PendingPcsQty_New as PendingPcsQty_New,isnull (XXXXX.Amount_with_Tax,0) as Amount_with_Tax, '1' as  DataSNO, '' as  Unit_code from (  Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty , Convert (int,CrateQty) as CrateQty_New , (XXXFinal.Total) - ((StockPcs.Conversion_Factor * Convert (int,CrateQty))) as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.OldName as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots  Union All  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   isnull([Cash_Scheme],0) as [Cash_Scheme] ,isnull([CD_Scheme],0) as [CD_Scheme] ,isnull([CR_Scheme],0) as [CR_Scheme] ,isnull([SO_Scheme],0) as [SO_Scheme] ,isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  " + whr + "   )  Final   pivot  (  max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code   ) XXXXX   " & _
        '              " Union All " & _
        '              " select  XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, '2' as  DataSNO, XXXXX.Unit_code as  Unit_code from (   Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty ,  'Scheme '+XXXFinal.Alies_Name as  Alies_Name_on_Name from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   [Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme],isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No   left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code   left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code   left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code   where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  " + whr + "   )  Final   pivot  (   max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code   ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code ) XXXXX " & _
        '              " Union All " & _
        '              " select  XXXXX.Document_Date, 'Route Total' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CEILING_Crate_Qty  as CrateQty_New , PendingPcsQty as PendingPcsQty_New,XXXXX.Amount_with_Tax, '3' as  DataSNO, '' as  Unit_code from (    Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by  XFinal.route_no, XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )XXXXX " & _
        '              "  Union All  select  XXXXX.Document_Date, 'Item Total' as Cust_Code,XXXXX.Customer_Name, XXXXX.route_no as route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CEILING_Crate_Qty  as CrateQty_New , PendingPcsQty as PendingPcsQty_New,XXXXX.Amount_with_Tax, '4' as  DataSNO, '' as  Unit_code from (    Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , max(XFinal.route_no) as route_no , max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by   XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )XXXXX  " & _
        '              " )XXXXXFinal left Outer Join " & _
        '              " ( " & _
        '              "  select XXXXXFinal.DataSNO ,XXXXXFinal.route_no,XXXXXFinal.Cust_Code , Count (Item_Code) as No_Of_Item  from ( " & _
        '              "  select '1' as  DataSNO, XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CrateQty_New as CrateQty_New , XXXXX.PendingPcsQty_New as PendingPcsQty_New,isnull (XXXXX.Amount_with_Tax,0) as Amount_with_Tax,'' as  Unit_code from (  Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty , Convert (int,CrateQty) as CrateQty_New , (XXXFinal.Total) - ((StockPcs.Conversion_Factor * Convert (int,CrateQty))) as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.OldName as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots  Union All  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   isnull([Cash_Scheme],0) as [Cash_Scheme] ,isnull([CD_Scheme],0) as [CD_Scheme] ,isnull([CR_Scheme],0) as [CR_Scheme] ,isnull([SO_Scheme],0) as [SO_Scheme] ,isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  " + whr + "  )  Final   pivot  (  max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code   ) XXXXX  " & _
        '              " Union All " & _
        '              " select '2' as  DataSNO, XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, XXXXX.Unit_code as  Unit_code from ( " & _
        '              " Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty ,  'Scheme '+XXXFinal.Alies_Name as  Alies_Name_on_Name from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   [Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme],isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No   left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code   left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code   left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code   where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  " + whr + "   )  Final   pivot  (   max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code   ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code " & _
        '              " ) XXXXX " & _
        '              " Union All " & _
        '              " select '3' as  DataSNO, XXXXX.Document_Date, 'Route Total' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, '' as  Unit_code from ( " & _
        '              " Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by  XFinal.route_no, XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )   XXXXX  " & _
        '              " Union All  select '4' as  DataSNO, XXXXX.Document_Date, 'Item Total' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, '' as  Unit_code from (  Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , max(XFinal.route_no) as route_no, max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  " + whr + "   )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by  XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )   XXXXX  " & _
        '              " )XXXXXFinal  group by XXXXXFinal.route_no, XXXXXFinal.Cust_Code, XXXXXFinal.DataSNO   " & _
        '              " ) " & _
        '              " TBL_Count on TBL_Count.DataSNO = XXXXXFinal.DataSNO and TBL_Count.route_no = XXXXXFinal.route_no  " & _
        '              " where XXXXXFinal.DataSNO in   ('3','4')   Order By XXXXXFinal.DataSNO , XXXXXFinal.route_no  "

        'Dim dtMain As DataTable = clsDBFuncationality.GetDataTable(qry)
        'If dtMain IsNot Nothing And dtMain.Rows.Count > 0 Then
        '    Dim subPath As String = "C:\\ERPTempFolder"
        '    Dim IsExists As Boolean = System.IO.Directory.Exists(subPath)
        '    If (IsExists = False) Then
        '        System.IO.Directory.CreateDirectory(subPath)
        '    End If
        '    subPath += "\\" & clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddhhmmsss") & ".Txt"
        '    IsExists = System.IO.File.Exists(subPath)
        '    If IsExists Then
        '        System.IO.File.Delete(subPath)
        '    End If
        '    WriteDataToFile2(dtMain, subPath)
        '    Process.Start(subPath)
        'Else
        '    clsCommon.MyMessageBoxShow("No Data Found")
        'End If




    End Sub

    Public Shared Sub WriteDataToFile2(ByVal submittedDataTable As DataTable, ByVal submittedFilePath As String)
        Try
            Dim i As Integer = 0
            Dim ii As Integer = 0
            Dim iH As Integer = 0
            Dim PageNo As Integer = 1
            Dim OnePageTotalLine As Integer = 60 '47
            Dim TotalLineNoPrint As Integer = 0
            Dim NextTotalNoOnPage As Integer = 0
            Dim PrvTotalNoOnPage As Integer = 0
            Dim sw As StreamWriter = Nothing
            sw = New StreamWriter(submittedFilePath, False)
            '=============================================================================================================================
            Dim strPrvRouteNo As String = ""
            Dim strPrvRouteDesc As String = ""
            Dim strPrvZoneCode As String = ""
            Dim strPrvZoneDesc As String = ""
            Dim strPrvCustomerNo As String = ""
            Dim strPrvCustomerDesc As String = ""
            Dim strPrvCustomerDescForRoutetotal As String = ""
            Dim strPrvDocDate As String = ""
            Dim SumOfCrate As Integer = 0
            Dim SumOfAmount As Double = 0.0
            Dim DataSNO As String = ""
            For Each rowH As DataRow In submittedDataTable.Rows
                Dim arrayH As Object() = rowH.ItemArray
                'Dim strPrvRouteNo As String = ""
                'Dim strPrvRouteDesc As String = ""
                'Dim strPrvZoneCode As String = ""
                'Dim strPrvZoneDesc As String = ""
                'Dim strPrvCustomerNo As String = ""
                'Dim strPrvCustomerDesc As String = ""
                'Dim strPrvDocDate As String = ""
                Dim strItemCode As String = ""
                Dim strItemDesc As String = ""
                Dim strCR As String = ""
                Dim strCD As String = ""
                Dim strSO As String = ""
                Dim strCash As String = ""
                Dim strTotal As String = ""
                Dim strCrate As String = ""
                Dim strAmount As String = ""
                Dim strtotalCrateQty As Integer = 0
                Dim strTotalPendingPcsQty As Integer = 0
                Dim strTotalAmount As Double = 0.0

                Dim strCurRouteNo As String = ""
                Dim strCurRouteDesc As String = ""
                Dim strCurZoneCode As String = ""
                Dim strCurZoneDesc As String = ""
                Dim strCurCustomerNo As String = ""
                Dim strCurCustomerDesc As String = ""
                Dim strCurDocDate As String = ""
                Dim strCurUnitCode As String = ""
                Dim strCurItemRowNo As String = ""






                For iH = 0 To arrayH.Length - 1
                    If iH = 3 Then
                        strCurRouteNo = arrayH(3).ToString()
                    ElseIf iH = 4 Then
                        strCurRouteDesc = arrayH(4).ToString()
                    ElseIf iH = 5 Then
                        strCurZoneCode = arrayH(5).ToString()
                    ElseIf iH = 6 Then
                        strCurZoneDesc = arrayH(6).ToString()
                    ElseIf iH = 1 Then
                        strCurCustomerNo = arrayH(1).ToString()
                    ElseIf iH = 2 Then
                        strCurCustomerDesc = arrayH(2).ToString()
                    ElseIf iH = 0 Then
                        strCurDocDate = arrayH(0).ToString()

                    ElseIf iH = 8 Then
                        strItemDesc = arrayH(8).ToString()
                        'sw.Write(GetFormateColumnValue(arrayH(8).ToString(), 18))
                    ElseIf iH = 9 Then
                        strCR = arrayH(9).ToString()
                        'sw.Write(GetFormateColumnValue(arrayH(9).ToString(), 12))
                    ElseIf iH = 10 Then
                        strCD = arrayH(10).ToString()
                        'sw.Write(GetFormateColumnValue(arrayH(10).ToString(), 12))
                    ElseIf iH = 11 Then
                        strSO = arrayH(11).ToString()
                        'sw.Write(GetFormateColumnValue(arrayH(11).ToString(), 12))
                    ElseIf iH = 12 Then
                        strCash = arrayH(12).ToString()
                        'sw.Write(GetFormateColumnValue(arrayH(12).ToString(), 12))
                    ElseIf iH = 13 Then
                        strTotal = arrayH(13).ToString()
                        'sw.Write(GetFormateColumnValue(arrayH(13).ToString(), 12))
                    ElseIf iH = 14 Then
                        strtotalCrateQty = clsCommon.myCdbl(arrayH(14).ToString())
                        'sw.Write(GetFormateColumnValue(arrayH(14).ToString(), 12))
                    ElseIf iH = 15 Then
                        strTotalPendingPcsQty = clsCommon.myCdbl(arrayH(15).ToString())
                        'sw.Write(GetFormateColumnValue(arrayH(15).ToString(), 12))
                    ElseIf iH = 16 Then
                        strTotalAmount = clsCommon.myCdbl(arrayH(16).ToString())
                        'sw.Write(GetFormateColumnValue(arrayH(16).ToString(), 12))
                    ElseIf iH = 17 Then
                        DataSNO = arrayH(17).ToString()
                    ElseIf iH = 18 Then
                        strCurUnitCode = arrayH(18).ToString()
                    ElseIf iH = 19 Then
                        strCurItemRowNo = arrayH(19).ToString()
                    End If
                    '    Next
                    'Next
                Next
                '=============================================================================================================================

                If clsCommon.CompairString(strCurCustomerNo, strPrvCustomerNo) <> CompairStringResult.Equal Then
                    If NextTotalNoOnPage = 0 And clsCommon.CompairString(DataSNO, "1") = CompairStringResult.Equal Then
                        NextTotalNoOnPage = NextTotalNoOnPage + 3 + 2 + 3 + clsCommon.myCdbl(strCurItemRowNo) + 1
                        PrvTotalNoOnPage = 3 + 2 + 3 + clsCommon.myCdbl(strCurItemRowNo) + 1
                    ElseIf clsCommon.CompairString(DataSNO, "1") = CompairStringResult.Equal Then
                        NextTotalNoOnPage = NextTotalNoOnPage + 0 + 1 + clsCommon.myCdbl(strCurItemRowNo) + 1 ' 3 replace 2
                        PrvTotalNoOnPage = 0 + 1 + clsCommon.myCdbl(strCurItemRowNo) + 1  ' 3 replace 2
                    ElseIf clsCommon.CompairString(DataSNO, "2") = CompairStringResult.Equal Then
                        NextTotalNoOnPage = NextTotalNoOnPage + 1
                        PrvTotalNoOnPage = 1
                    ElseIf clsCommon.CompairString(DataSNO, "3") = CompairStringResult.Equal Then
                        NextTotalNoOnPage = NextTotalNoOnPage + 0 + 1 + clsCommon.myCdbl(strCurItemRowNo) + 1
                        PrvTotalNoOnPage = 0 + 1 + clsCommon.myCdbl(strCurItemRowNo) + 1
                    End If
                End If
                If clsCommon.CompairString(strCurCustomerNo, strPrvCustomerNo) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(DataSNO, "2") = CompairStringResult.Equal Then
                    NextTotalNoOnPage = NextTotalNoOnPage + clsCommon.myCdbl(strCurItemRowNo)
                    PrvTotalNoOnPage = PrvTotalNoOnPage + clsCommon.myCdbl(strCurItemRowNo)
                End If

                If NextTotalNoOnPage > OnePageTotalLine OrElse (clsCommon.CompairString(strCurRouteNo, strPrvRouteNo) <> CompairStringResult.Equal AndAlso clsCommon.myLen(strPrvRouteNo) > 0) Then ' OrElse (clsCommon.CompairString(strCurRouteNo, strPrvRouteNo) <> CompairStringResult.Equal AndAlso clsCommon.myLen(strPrvRouteNo) > 0)
                    Dim BlankSpace As Integer = PrvTotalNoOnPage - (NextTotalNoOnPage - OnePageTotalLine)
                    TotalLineNoPrint = 0
                    NextTotalNoOnPage = PrvTotalNoOnPage + 7 ' for 3 heading Total

                    If SumOfAmount > 0 Or SumOfCrate > 0 Then
                        'sw.Write("                                                                              " + GetFormateColumnValue(SumOfCrate, 12) + "" + GetFormateColumnValue(SumOfAmount.ToString("N2"), 12) + "  ")
                        sw.Write(GetFormateColumnValue(SumOfCrate, 65, "R") + "" + GetFormateColumnValue(SumOfAmount.ToString("N2"), 14, "R") + "  ")
                        sw.WriteLine()
                        SumOfCrate = 0
                        SumOfAmount = 0.0

                    End If
                    If clsCommon.myLen(strPrvRouteNo) > 0 Then
                        Dim iii As Integer = 0
                        For iii = 0 To BlankSpace - 1
                            sw.Write("")
                            sw.WriteLine()
                        Next
                    End If
                End If
                If TotalLineNoPrint = 0 Then ' TotalLineNoPrint = 0
                    'If SumOfAmount > 0 Or SumOfCrate > 0 Then
                    '    sw.Write("                                                                              " + GetFormateColumnValue(SumOfCrate, 12) + "" + GetFormateColumnValue(SumOfAmount, 12) + "  ")
                    '    sw.WriteLine()
                    '    SumOfCrate = 0
                    '    SumOfAmount = 0
                    'End If

                    Dim iHeader As Integer = 0
                    If PageNo <> 1 Then
                        NextTotalNoOnPage = NextTotalNoOnPage + 7 ' 7 for Logo space
                        For iHeader = 0 To 7 - 1
                            sw.Write("")
                            sw.WriteLine()
                        Next
                    End If
                    sw.Write("            THE TELANGANA STATE DAIRY DEV. CO-OP. FEDN. LTD -MPF: HYDERABAD")
                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 1
                    sw.Write("                         ABSTRACT OF " + clsCommon.myCstr(strCurDocDate) + "")
                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 1
                    sw.Write("Page: " + clsCommon.myCstr(PageNo) + " ")
                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 1
                    PageNo = PageNo + 1
                    '' ===========================
                    'If clsCommon.CompairString(DataSNO, "3") <> CompairStringResult.Equal Then
                    '    sw.Write("ZONE : (" + clsCommon.myCstr(strCurZoneCode) + ")" + clsCommon.myCstr(strCurZoneDesc) + " ")
                    '    sw.WriteLine()
                    '    TotalLineNoPrint = TotalLineNoPrint + 1
                    'End If
                    'sw.Write("ROUTE : (" + clsCommon.myCstr(strCurRouteNo) + ")" + clsCommon.myCstr(strCurRouteDesc) + " ")
                    'sw.WriteLine()
                    'TotalLineNoPrint = TotalLineNoPrint + 1
                    ''============================

                    sw.Write("ZONE : (" + clsCommon.myCstr(strCurZoneCode) + ")" + clsCommon.myCstr(strCurZoneDesc) + " ")
                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 1

                    sw.Write("ROUTE : (" + clsCommon.myCstr(strCurRouteNo) + ")" + clsCommon.myCstr(strCurRouteDesc) + " ")
                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 1
                    sw.Write("----------------------------------------------------------------------------------")
                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 1
                    For i = 0 To submittedDataTable.Columns.Count - 1
                        Dim strColumn_Name As String = ""
                        If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "Customer_Name") = CompairStringResult.Equal Then
                            strColumn_Name = "Booth Name          " '20 8+7 +4
                        End If
                        If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "Alies_Name") = CompairStringResult.Equal Then
                            strColumn_Name = "Type    " '8
                        End If
                        If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "CR") = CompairStringResult.Equal Then
                            'strColumn_Name = "CR          " ' 12
                            strColumn_Name = "    CR" '7
                        End If
                        If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "CD") = CompairStringResult.Equal Then
                            'strColumn_Name = "CD          " '12
                            strColumn_Name = "    CD" '7
                        End If
                        If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "SO") = CompairStringResult.Equal Then
                            strColumn_Name = "   S.O" '7
                        End If
                        If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "CASH") = CompairStringResult.Equal Then
                            strColumn_Name = "  Cash" '7
                        End If
                        If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "Total") = CompairStringResult.Equal Then
                            strColumn_Name = "   Total" '8
                        End If

                        If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "CrateQty_New") = CompairStringResult.Equal Then
                            strColumn_Name = "  Crates" '8
                        End If
                        If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "Amount_with_Tax") = CompairStringResult.Equal Then
                            strColumn_Name = "     Amount" '11
                        End If

                        If clsCommon.myLen(strColumn_Name) > 0 Then
                            sw.Write(strColumn_Name)
                        End If

                        'sw.Write(submittedDataTable.Columns(i).ColumnName & "             ")
                    Next

                    'sw.Write(submittedDataTable.Columns(i).ColumnName)
                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 1
                    sw.Write("----------------------------------------------------------------------------------")
                    'sw.WriteLine()
                    'TotalLineNoPrint = TotalLineNoPrint + 1

                End If
                If clsCommon.CompairString(strCurCustomerNo, strPrvCustomerNo) <> CompairStringResult.Equal Then  'clsCommon.CompairString(strCurRouteNo, strPrvRouteNo) <> CompairStringResult.Equal AndAlso
                    If SumOfAmount > 0 Then
                        'sw.Write("                                                                              " + GetFormateColumnValue(SumOfCrate, 12) + "" + GetFormateColumnValue(SumOfAmount.ToString("N2"), 12) + "  ")
                        sw.Write(GetFormateColumnValue(SumOfCrate, 65, "R") + "" + GetFormateColumnValue(SumOfAmount.ToString("N2"), 14, "R") + "  ")
                        sw.WriteLine()
                        SumOfCrate = 0
                        SumOfAmount = 0.0
                    End If
                    'If clsCommon.CompairString(DataSNO, "3") <> CompairStringResult.Equal Then
                    '    sw.Write("ZONE : (" + clsCommon.myCstr(strCurZoneCode) + ")" + clsCommon.myCstr(strCurZoneDesc) + " ")
                    '    sw.WriteLine()
                    '    TotalLineNoPrint = TotalLineNoPrint + 1
                    'End If

                    'If (clsCommon.CompairString(strCurRouteNo, strPrvRouteNo) <> CompairStringResult.Equal OrElse clsCommon.myLen(strPrvRouteNo) < 0) Then
                    '    sw.Write("ROUTE : (" + clsCommon.myCstr(strCurRouteNo) + ")" + clsCommon.myCstr(strCurRouteDesc) + " ")
                    '    sw.WriteLine()
                    '    TotalLineNoPrint = TotalLineNoPrint + 1
                    'End If


                    'If clsCommon.CompairString(DataSNO, "3") = CompairStringResult.Equal Then
                    '    sw.Write("ROUTE TOTAL : ")
                    '    sw.WriteLine()
                    '    TotalLineNoPrint = TotalLineNoPrint + 1
                    '    strCurCustomerDesc = ""
                    'Else
                    '    sw.Write("Customer : (" + clsCommon.myCstr(strCurCustomerNo) + ")" + clsCommon.myCstr(strCurCustomerDesc) + "  Date : " + strCurDocDate + " ")
                    '    sw.WriteLine()
                    '    TotalLineNoPrint = TotalLineNoPrint + 1
                    'End If

                    sw.Write("----------------------------------------------------------------------------------")
                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 1

                    'For i = 0 To submittedDataTable.Columns.Count - 1
                    '    Dim strColumn_Name As String = ""
                    '    If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "Customer_Name") = CompairStringResult.Equal Then
                    '        strColumn_Name = "Booth Name          " '20 8+7 +4
                    '    End If
                    '    If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "Alies_Name") = CompairStringResult.Equal Then
                    '        strColumn_Name = "Type    " '8
                    '    End If
                    '    If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "CR") = CompairStringResult.Equal Then
                    '        'strColumn_Name = "CR          " ' 12
                    '        strColumn_Name = "    CR" '7
                    '    End If
                    '    If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "CD") = CompairStringResult.Equal Then
                    '        'strColumn_Name = "CD          " '12
                    '        strColumn_Name = "    CD" '7
                    '    End If
                    '    If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "SO") = CompairStringResult.Equal Then
                    '        strColumn_Name = "   S.O" '7
                    '    End If
                    '    If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "CASH") = CompairStringResult.Equal Then
                    '        strColumn_Name = "  Cash" '7
                    '    End If
                    '    If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "Total") = CompairStringResult.Equal Then
                    '        strColumn_Name = "   Total" '8
                    '    End If

                    '    If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "CrateQty_New") = CompairStringResult.Equal Then
                    '        strColumn_Name = "  Crates" '8
                    '    End If
                    '    If clsCommon.CompairString(submittedDataTable.Columns(i).ColumnName.Trim(), "Amount_with_Tax") = CompairStringResult.Equal Then
                    '        strColumn_Name = "     Amount" '11
                    '    End If

                    '    If clsCommon.myLen(strColumn_Name) > 0 Then
                    '        sw.Write(strColumn_Name)
                    '    End If

                    '    'sw.Write(submittedDataTable.Columns(i).ColumnName & "             ")
                    'Next

                    ''sw.Write(submittedDataTable.Columns(i).ColumnName)
                    'sw.WriteLine()
                    'TotalLineNoPrint = TotalLineNoPrint + 1
                    'sw.Write("----------------------------------------------------------------------------------")
                    'sw.WriteLine()
                    'TotalLineNoPrint = TotalLineNoPrint + 1
                End If
                If clsCommon.CompairString(DataSNO, "2") = CompairStringResult.Equal Then
                    sw.Write("" + strItemDesc + "  " + strCurUnitCode + " : " + strTotal + "")
                    sw.WriteLine()
                Else
                    If (clsCommon.CompairString(strCurCustomerNo, strPrvCustomerNo) = CompairStringResult.Equal OrElse clsCommon.myLen(strPrvCustomerNo) <= 0) Then
                        If clsCommon.myLen(strPrvCustomerNo) <= 0 Then
                        ElseIf clsCommon.myLen(strPrvCustomerDesc) <= 0 Then
                            strCurCustomerDesc = ""
                        ElseIf clsCommon.CompairString(strPrvCustomerDesc, strCurCustomerNo + " (" + strCurDocDate + ")") <> CompairStringResult.Equal Then
                            strCurCustomerDesc = strCurCustomerNo + " (" + strCurDocDate + ")"
                        Else
                            strCurCustomerDesc = ""
                        End If


                    End If
                    If clsCommon.CompairString(DataSNO, "3") = CompairStringResult.Equal Then
                        If (clsCommon.CompairString(strCurRouteNo, strPrvRouteNo) = CompairStringResult.Equal) Then
                            If (clsCommon.CompairString(strCurCustomerNo, strPrvCustomerNo) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(strCurCustomerNo, "Route Total") = CompairStringResult.Equal) Then
                                strCurCustomerDesc = "ROUTE TOTAL"
                                'ElseIf clsCommon.CompairString(strPrvCustomerDescForRoutetotal, "ROUTE TOTAL") <> CompairStringResult.Equal Then
                                '    strCurCustomerDesc = "ROUTE TOTAL"
                            Else
                                strCurCustomerDesc = ""
                            End If
                        End If
                    End If
                    sw.Write(GetFormateColumnValue(strCurCustomerDesc, 20))
                    sw.Write(GetFormateColumnValue(strItemDesc, 8))
                    sw.Write(GetFormateColumnValue(strCR, 6, "R"))
                    sw.Write(GetFormateColumnValue(strCD, 6, "R"))
                    sw.Write(GetFormateColumnValue(strSO, 6, "R"))
                    sw.Write(GetFormateColumnValue(strCash, 6, "R"))
                    sw.Write(GetFormateColumnValue(strTotal, 8, "R"))
                    sw.Write(GetFormateColumnValue(clsCommon.myCstr(strtotalCrateQty) + "-" + GetFormateColumnValue(clsCommon.myCstr(strTotalPendingPcsQty), 2, "R"), 8, "R"))
                    sw.Write(GetFormateColumnValue(strTotalAmount.ToString("N2"), 11, "R"))
                    sw.WriteLine()
                    TotalLineNoPrint = TotalLineNoPrint + 1
                    SumOfCrate += strtotalCrateQty
                    SumOfAmount += strTotalAmount
                End If

                'For Each row As DataRow In submittedDataTable.Rows
                '    Dim array As Object() = row.ItemArray

                '    For i = 0 To array.Length - 1 - 1
                '        If i = 8 Or i = 9 Or i = 10 Or i = 11 Or i = 12 Or i = 13 Or i = 14 Or i = 15 Or i = 16 Then
                '            If i = 8 Then
                '                sw.Write(GetFormateColumnValue(array(8).ToString(), 18))
                '            ElseIf i = 9 Then
                '                sw.Write(GetFormateColumnValue(array(9).ToString(), 12))
                '            ElseIf i = 10 Then
                '                sw.Write(GetFormateColumnValue(array(10).ToString(), 12))
                '            ElseIf i = 11 Then
                '                sw.Write(GetFormateColumnValue(array(11).ToString(), 12))
                '            ElseIf i = 12 Then
                '                sw.Write(GetFormateColumnValue(array(12).ToString(), 12))
                '            ElseIf i = 13 Then
                '                sw.Write(GetFormateColumnValue(array(13).ToString(), 12))
                '            ElseIf i = 14 Then
                '                sw.Write(GetFormateColumnValue(array(14).ToString(), 12))
                '            ElseIf i = 15 Then
                '                sw.Write(GetFormateColumnValue(array(15).ToString(), 12))
                '            ElseIf i = 16 Then
                '                sw.Write(GetFormateColumnValue(array(16).ToString(), 12))
                '            End If
                '        End If
                '    Next
                '    'sw.Write(array(i).ToString())
                '    sw.WriteLine()
                'Next
                '=========================================
                strPrvRouteNo = strCurRouteNo
                strPrvZoneCode = strCurZoneCode
                strPrvCustomerNo = strCurCustomerNo
                strPrvCustomerDesc = strCurCustomerDesc
                If clsCommon.CompairString(DataSNO, "3") = CompairStringResult.Equal Then
                    strPrvCustomerDescForRoutetotal = strCurCustomerDesc
                End If



            Next
            If SumOfAmount > 0 Then
                'sw.Write("                                                                              " + GetFormateColumnValue(SumOfCrate, 12) + "" + GetFormateColumnValue(SumOfAmount.ToString("N2"), 12) + "  ")
                sw.Write(GetFormateColumnValue(SumOfCrate, 65, "R") + "" + GetFormateColumnValue(SumOfAmount.ToString("N2"), 14, "R") + "  ")
                sw.WriteLine()
                SumOfCrate = 0
                SumOfAmount = 0.0
            End If
            '=========================================
            sw.Close()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub


    'Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
    '    Dim qry As String = "  Select top 200 TSPL_BOOKING_DETAIL.Document_No ,Convert (varchar, TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_DETAIL.Cust_Code ,TSPL_BOOKING_MATSER.location_code,TSPL_BOOKING_DETAIL.Item_Code,TSPL_BOOKING_DETAIL.Booking_Qty,TSPL_BOOKING_DETAIL.DocumentAmount  from TSPL_BOOKING_DETAIL  Left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No  "
    '    Dim dtMain As DataTable = clsDBFuncationality.GetDataTable(qry)
    '    If dtMain IsNot Nothing And dtMain.Rows.Count > 0 Then

    '    End If

    '    Dim coll As Dictionary(Of String, Integer)
    '    coll = New Dictionary(Of String, Integer)()
    '    coll.Add("Document_No", 15)
    '    coll.Add("Document_Date", 15)
    '    coll.Add("Cust_Code", 15)
    '    coll.Add("Item_Code", 15)
    '    coll.Add("Booking_Qty", 20)
    '    coll.Add("DocumentAmount", 20)
    '    'clsCommonFunctionality.CreateOrAlterTable("TSPL_TABLE_METADATA", coll)


    'End Sub

    Private Sub btn_TruckSheetGenerated_Click(sender As Object, e As EventArgs) Handles btn_TruckSheetGenerated.Click
        Try
            If chkShowEarlyRoute.Checked = True Then
                StrEarlyRoute = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @temp VARCHAR(MAX) SET @temp =(SELECT ',''' + cast(Route_No as varchar) + '''' FROM TSPL_ROUTE_MASTER where IsEarlyRoute=1 order by Route_No FOR XML PATH('')) select SUBSTRING(@temp, 2, 200000) AS Route"))
            Else
                StrNonEarlyRoute = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @temp VARCHAR(MAX) SET @temp =(SELECT ',''' + cast(Route_No as varchar) + '''' FROM TSPL_ROUTE_MASTER where IsEarlyRoute=0 order by Route_No FOR XML PATH('')) select SUBSTRING(@temp, 2, 200000) AS Route"))
            End If
            If chkShowEarlyRoute.Checked = True AndAlso clsCommon.myLen(StrEarlyRoute) <= 0 Then
                Throw New Exception("Early Route Not Found")
            ElseIf chkShowEarlyRoute.Checked = False AndAlso clsCommon.myLen(StrNonEarlyRoute) <= 0 Then
                Throw New Exception("Non Early Route Not Found")
            End If
            If common.clsCommon.MyMessageBoxShow(Me, "No booking will be created for Date [" + clsCommon.GetPrintDate(TSP_Date.Value, "dd/MMM/yyyy") + "] after generating truck sheet" + Environment.NewLine + "Do you want to continue?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                'Dim Qry As String = "Update TSPL_BOOKING_MATSER set TruckSheetGenerate = 1 where Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) "
                Dim Qry As String = "Update TSPL_BOOKING_MATSER set TruckSheetGenerate = 1 from TSPL_BOOKING_MATSER left join TSPL_BOOKING_DETAIL on TSPL_BOOKING_DETAIL.Document_No=TSPL_BOOKING_MATSER.Document_No left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_BOOKING_DETAIL.cust_code where ISNULL(TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY,'') not in ('Others','Distributor','') and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) and TSPL_BOOKING_DETAIL.Booking_Status <>2 "
                clsDBFuncationality.ExecuteNonQuery(Qry)
                'common.clsCommon.MyMessageBoxShow("Truck Sheet Generated Successfully.", Me.Text)
                btn_TruckSheetGenerated.Enabled = False
                btn_CancelTruckSheet.Enabled = True
                If common.clsCommon.MyMessageBoxShow(Me, "Truck Sheet Generated Successfully." + Environment.NewLine + "Do you want to Open Truck Sheet Preview?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    isTruckSheetPriview = True
                    btnDotMatrixPrinter.PerformClick()
                    isTruckSheetPriview = False
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtMultiCustomerCategory__My_Click(sender As Object, e As EventArgs) Handles TxtMultiCustomerCategory._My_Click
        Try
            Dim qry As String = "select 'Vendor' as Code union all select 'Institution CR' as Code union all select 'Institution SO' as Code union all select 'Distributor' as Code"
            TxtMultiCustomerCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("CustCatMu@Sel", qry, "Code", "Code", TxtMultiCustomerCategory.arrValueMember, TxtMultiCustomerCategory.arrDispalyMember)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btn_PrintGatePass_Click(sender As Object, e As EventArgs) Handles btn_PrintGatePass.Click
        If (clsCommon.CompairString(cmbGatePassType.Text, "Select") = CompairStringResult.Equal Or clsCommon.CompairString(cmbGatePassType.Text, "") = CompairStringResult.Equal) Then
            clsCommon.MyMessageBoxShow(Me, "Please select Gate Pass type(AM/PM)", Me.Text)
            cmbGatePassType.Focus()
            Return
        End If

        GetTruckSheetQry(" tspl_booking_matser.AgainstGatePass=1 and tspl_booking_matser.GatePass_Type = '" + cmbGatePassType.Text + "' ", True)
        'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal Then
        '    Dim whr As String = " and tspl_booking_matser.AgainstGatePass=1 "
        '    If clsCommon.myLen(clsCommon.myCstr(txtLorryNo.Value)) > 0 Then
        '        whr += "  and TSPL_ROUTE_MASTER.Vehicle_Code in ('" + txtLorryNo.Value + "') "
        '    End If

        '    If clsCommon.myLen(clsCommon.myCstr(txtRouteNo.Value)) > 0 Then
        '        whr += "  and TSPL_BOOKING_DETAIL.route_No in ('" + txtRouteNo.Value + "') "
        '    End If

        '    If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
        '        whr += " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in(" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ")" + Environment.NewLine
        '    End If

        '    Dim qry = " select XXXXXFinal.Document_Date,XXXXXFinal.Cust_Code,Case when  XXXXXFinal.DataSNO = 1 and No_Of_Item =1 then SUBSTRING((XXXXXFinal.Cust_Code + ' ('+XXXXXFinal.Document_Date + ')'),0,19) else  SUBSTRING ( XXXXXFinal.Customer_Name,0,19) end as Customer_Name, XXXXXFinal.route_no,XXXXXFinal.Route_Desc,XXXXXFinal.Zone_Code,XXXXXFinal.Zone_Name,XXXXXFinal.Item_Code,SUBSTRING (XXXXXFinal.Alies_Name,0,8) as Alies_Name, Convert (integer,XXXXXFinal.CR) as CR,Convert (Integer ,XXXXXFinal.CD) as CD,convert (Integer,XXXXXFinal.SO) as SO,Convert (Integer ,XXXXXFinal.Cash,103) as Cash ,Convert (Integer,XXXXXFinal.Total) as Total,XXXXXFinal.CrateQty_New as CrateQty_New , XXXXXFinal.PendingPcsQty_New as PendingPcsQty_New, XXXXXFinal.Amount_with_Tax, XXXXXFinal.  DataSNO,   XXXXXFinal. Unit_code,TBL_Count.No_Of_Item from (  " & _
        '              " select XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CrateQty_New as CrateQty_New , XXXXX.PendingPcsQty_New as PendingPcsQty_New,isnull (XXXXX.Amount_with_Tax,0) as Amount_with_Tax, '1' as  DataSNO, '' as  Unit_code from (  Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty , Convert (int,CrateQty) as CrateQty_New , (XXXFinal.Total) - ((StockPcs.Conversion_Factor * Convert (int,CrateQty))) as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.OldName as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots  Union All  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   isnull([Cash_Scheme],0) as [Cash_Scheme] ,isnull([CD_Scheme],0) as [CD_Scheme] ,isnull([CR_Scheme],0) as [CR_Scheme] ,isnull([SO_Scheme],0) as [SO_Scheme] ,isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "   )  Final   pivot  (  max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code   ) XXXXX   " & _
        '              " Union All " & _
        '              " select  XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, '2' as  DataSNO, XXXXX.Unit_code as  Unit_code from (   Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty ,  'Scheme '+XXXFinal.Alies_Name as  Alies_Name_on_Name from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   [Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme],isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No   left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code   left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code   left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code   where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "   )  Final   pivot  (   max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code   ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code ) XXXXX " & _
        '              " Union All " & _
        '              " select  XXXXX.Document_Date, 'Route Total' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CEILING_Crate_Qty  as CrateQty_New , PendingPcsQty as PendingPcsQty_New,XXXXX.Amount_with_Tax, '3' as  DataSNO, '' as  Unit_code from (    Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by  XFinal.route_no, XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )XXXXX " & _
        '              " )XXXXXFinal left Outer Join " & _
        '              " ( " & _
        '              "  select XXXXXFinal.DataSNO ,XXXXXFinal.route_no,XXXXXFinal.Cust_Code , Count (Item_Code) as No_Of_Item  from ( " & _
        '              "  select '1' as  DataSNO, XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CrateQty_New as CrateQty_New , XXXXX.PendingPcsQty_New as PendingPcsQty_New,isnull (XXXXX.Amount_with_Tax,0) as Amount_with_Tax,'' as  Unit_code from (  Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty , Convert (int,CrateQty) as CrateQty_New , (XXXFinal.Total) - ((StockPcs.Conversion_Factor * Convert (int,CrateQty))) as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.OldName as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots  Union All  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   isnull([Cash_Scheme],0) as [Cash_Scheme] ,isnull([CD_Scheme],0) as [CD_Scheme] ,isnull([CR_Scheme],0) as [CR_Scheme] ,isnull([SO_Scheme],0) as [SO_Scheme] ,isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  " + whr + "  )  Final   pivot  (  max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code   ) XXXXX  " & _
        '              " Union All " & _
        '              " select '2' as  DataSNO, XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, XXXXX.Unit_code as  Unit_code from ( " & _
        '              " Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty ,  'Scheme '+XXXFinal.Alies_Name as  Alies_Name_on_Name from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   [Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme],isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No   left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code   left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code   left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code   where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "   )  Final   pivot  (   max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code   ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code " & _
        '              " ) XXXXX " & _
        '              " Union All " & _
        '              " select '3' as  DataSNO, XXXXX.Document_Date, 'Route Total' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, '' as  Unit_code from ( " & _
        '              " Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by  XFinal.route_no, XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )   XXXXX  " & _
        '              " )XXXXXFinal  group by XXXXXFinal.route_no, XXXXXFinal.Cust_Code, XXXXXFinal.DataSNO   " & _
        '              " ) " & _
        '              " TBL_Count on TBL_Count.DataSNO = XXXXXFinal.DataSNO and TBL_Count.route_no = XXXXXFinal.route_no and TBL_Count.Cust_Code = XXXXXFinal.Cust_Code " & _
        '              " Order By XXXXXFinal.route_no,XXXXXFinal.Cust_Code,XXXXXFinal.DataSNO  "

        '    Dim dtMain As DataTable = clsDBFuncationality.GetDataTable(qry)
        '    If dtMain IsNot Nothing And dtMain.Rows.Count > 0 Then
        '        Dim subPath As String = "C:\\ERPTempFolder"
        '        Dim IsExists As Boolean = System.IO.Directory.Exists(subPath)
        '        If (IsExists = False) Then
        '            System.IO.Directory.CreateDirectory(subPath)
        '        End If
        '        subPath += "\\" & clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddhhmmsss") & ".Txt"
        '        IsExists = System.IO.File.Exists(subPath)
        '        If IsExists Then
        '            System.IO.File.Delete(subPath)
        '        End If
        '        WriteDataToFile(dtMain, subPath)
        '        Process.Start(subPath)
        '    Else
        '        clsCommon.MyMessageBoxShow("No Data Found")
        '    End If
        'End If
    End Sub
    '===================================================================Net Sale Report =========================================================================
    Sub LoadNSRCombo()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Daywise Net Sales"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Milk Daywise Net Sales"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Product Daywise Net Sales"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Zonewise Net Sales(ALL)"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Zonewise Net Sales(Milk)"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Zonewise Net Sales(Product)"
        dt.Rows.Add(dr)

        cboNSRType.DataSource = dt.Copy()
        cboNSRType.ValueMember = "Code"
        cboNSRType.DisplayMember = "Code"
        dt = Nothing

    End Sub

    '============================================================================================================================================================
    Private Sub txtNSRLocation__My_Click(sender As Object, e As EventArgs) Handles txtNSRLocation._My_Click
        strQry = "select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER"
        txtNSRLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("txtNSRMultLocation", strQry, "Code", "Name", txtNSRLocation.arrValueMember, txtNSRLocation.arrDispalyMember)
    End Sub

    Private Sub txtNSRZone__My_Click(sender As Object, e As EventArgs) Handles txtNSRZone._My_Click
        strQry = "select zone_code as Code,Description as Name from TSPL_ZONE_MASTER"
        txtNSRZone.arrValueMember = clsCommon.ShowMultipleSelectForm("txtNSRMultLocation", strQry, "Code", "Name", txtNSRZone.arrValueMember, txtNSRZone.arrDispalyMember)
    End Sub

    Private Sub btnNSRPrint_Click(sender As Object, e As EventArgs) Handles btnNSRPrint.Click
        Try
            If chkConsiderShowEarlyRoute.Checked = True Then
                If chkShowEarlyRoute.Checked = True Then
                    StrEarlyRoute = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @temp VARCHAR(MAX) SET @temp =(SELECT ',''' + cast(Route_No as varchar) + '''' FROM TSPL_ROUTE_MASTER where IsEarlyRoute=1 order by Route_No FOR XML PATH('')) select SUBSTRING(@temp, 2, 200000) AS Route"))
                Else
                    StrNonEarlyRoute = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @temp VARCHAR(MAX) SET @temp =(SELECT ',''' + cast(Route_No as varchar) + '''' FROM TSPL_ROUTE_MASTER where IsEarlyRoute=0 order by Route_No FOR XML PATH('')) select SUBSTRING(@temp, 2, 200000) AS Route"))
                End If
                If chkShowEarlyRoute.Checked = True AndAlso clsCommon.myLen(StrEarlyRoute) <= 0 Then
                    Throw New Exception("Early Route Not Found")
                ElseIf chkShowEarlyRoute.Checked = False AndAlso clsCommon.myLen(StrNonEarlyRoute) <= 0 Then
                    Throw New Exception("Non Early Route Not Found")
                End If
            End If
            Dim whrForPM_Supply As String = " "
            Dim whr As String = " "
            Dim whrLR_PQ_ES As String = ""
            'Dim dtFromDatePrvDay2 As DateTime = New Date(dtpNSRFromDate.Value.Year, dtpNSRFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
            Dim dtFromDatePrvDay As DateTime = dtpNSRFromDate.Value.AddDays(-1)
            Dim dtFromDatePrvYear As DateTime = dtpNSRFromDate.Value.AddYears(-1)
            Dim dtFromCurrentDate As DateTime = dtpNSRFromDate.Value
            Dim strCR_CD_SO_CASH_strFromtime = "08:00"
            Dim strCR_CD_SO_CASH_strToTime = "07:59"
            Dim strCR_CD_SO_CASH_whre As String = ""

            Dim strCR_CD_SO_CASH_strFromDate As String = ""
            Dim strCR_CD_SO_CASH_strToDate As String = ""
            Dim strCR_CD_SO_CASH_strToDateForEvening As String = ""


            strCR_CD_SO_CASH_strFromDate = dtpNSRFromDate.Value + " " + strCR_CD_SO_CASH_strFromtime
            strCR_CD_SO_CASH_strToDate = dtpNSRFromDate.Value + " " + strCR_CD_SO_CASH_strToTime
            strCR_CD_SO_CASH_strToDateForEvening = DateAdd("d", 1, dtpNSRFromDate.Value) + " " + strCR_CD_SO_CASH_strToTime

            'clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFromDatePrvDay), "dd/MMM/yyyy") 
            If clsCommon.CompairString(cboNSRType.SelectedValue, "Daywise Net Sales") = CompairStringResult.Equal Then
                whr += " and Convert(date, Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MMM/yyyy") + "',103)  and Convert(date, Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MMM/yyyy") + "',103)  "
                whrForPM_Supply += " and Convert(date, Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(DateAdd("d", -1, dtpNSRFromDate.Value), "dd/MMM/yyyy") + "',103)  and Convert(date, Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(DateAdd("d", -1, dtpNSRFromDate.Value), "dd/MMM/yyyy") + "',103)  "
                strCR_CD_SO_CASH_whre += " and  Document_Date  between  '" + clsCommon.GetPrintDate(strCR_CD_SO_CASH_strFromDate, "dd/MMM/yyyy HH:mm") + "'  and  '" + clsCommon.GetPrintDate(strCR_CD_SO_CASH_strToDateForEvening, "dd/MMM/yyyy HH:mm") + "' "
            ElseIf clsCommon.CompairString(cboNSRType.SelectedValue, "Milk Daywise Net Sales") = CompairStringResult.Equal Then
                whr += "  and TSPL_ITEM_MASTER. Is_Milk_Pouch =1  and Convert(date, Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MMM/yyyy") + "',103)  and Convert(date, Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MMM/yyyy") + "',103)  "
                whrForPM_Supply += "  and TSPL_ITEM_MASTER. Is_Milk_Pouch =1  and Convert(date, Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(DateAdd("d", -1, dtpNSRFromDate.Value), "dd/MMM/yyyy") + "',103)  and Convert(date, Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(DateAdd("d", -1, dtpNSRFromDate.Value), "dd/MMM/yyyy") + "',103)  "
                strCR_CD_SO_CASH_whre += "  and TSPL_ITEM_MASTER. Is_Milk_Pouch =1  and  Document_Date  between  '" + clsCommon.GetPrintDate(strCR_CD_SO_CASH_strFromDate, "dd/MMM/yyyy HH:mm") + "'  and  '" + clsCommon.GetPrintDate(strCR_CD_SO_CASH_strToDateForEvening, "dd/MMM/yyyy HH:mm") + "' "
                whrLR_PQ_ES += " and TSPL_ITEM_MASTER. Is_Milk_Pouch =1 "
            ElseIf clsCommon.CompairString(cboNSRType.SelectedValue, "Product Daywise Net Sales") = CompairStringResult.Equal Then
                whr += "  and TSPL_ITEM_MASTER. Is_Milk_Pouch =0  and Convert(date, Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MMM/yyyy") + "',103)  and Convert(date, Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MMM/yyyy") + "',103)  "
                whrForPM_Supply += "  and TSPL_ITEM_MASTER. Is_Milk_Pouch =0  and Convert(date, Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(DateAdd("d", -1, dtpNSRFromDate.Value), "dd/MMM/yyyy") + "',103)  and Convert(date, Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(DateAdd("d", -1, dtpNSRFromDate.Value), "dd/MMM/yyyy") + "',103)  "
                strCR_CD_SO_CASH_whre += "  and TSPL_ITEM_MASTER. Is_Milk_Pouch =0  and  Document_Date  between  '" + clsCommon.GetPrintDate(strCR_CD_SO_CASH_strFromDate, "dd/MMM/yyyy HH:mm") + "'  and  '" + clsCommon.GetPrintDate(strCR_CD_SO_CASH_strToDateForEvening, "dd/MMM/yyyy HH:mm") + "' "
                whrLR_PQ_ES += "  and TSPL_ITEM_MASTER. Is_Milk_Pouch =0 "
            Else
                whr += " and Convert(date, Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MMM/yyyy") + "',103)  and Convert(date, Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRToDate.Value, "dd/MMM/yyyy") + "',103)  "
                whrForPM_Supply += " and Convert(date, Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(DateAdd("d", -1, dtpNSRFromDate.Value), "dd/MMM/yyyy") + "',103)  and Convert(date, Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(DateAdd("d", -1, dtpNSRToDate.Value), "dd/MMM/yyyy") + "',103)  "
            End If

            If txtNSRLocation.arrValueMember IsNot Nothing AndAlso txtNSRLocation.arrValueMember.Count > 0 Then
                whr += " and TSPL_BOOKING_DETAIL.Loc_Code  in (" + clsCommon.GetMulcallString(txtNSRLocation.arrValueMember) + ") "
                whrForPM_Supply += " and TSPL_BOOKING_DETAIL.Loc_Code  in (" + clsCommon.GetMulcallString(txtNSRLocation.arrValueMember) + ") "
                whrLR_PQ_ES += " and TSPL_SD_SHIPMENT_DETAIL.Location  in (" + clsCommon.GetMulcallString(txtNSRLocation.arrValueMember) + ") "
                strCR_CD_SO_CASH_whre += " and TSPL_SD_SHIPMENT_DETAIL.Location  in (" + clsCommon.GetMulcallString(txtNSRLocation.arrValueMember) + ") "
            End If
            If txtNSRZone.arrValueMember IsNot Nothing AndAlso txtNSRZone.arrValueMember.Count > 0 Then
                whr += " and TSPL_CUSTOMER_MASTER.Zone_Code  in (" + clsCommon.GetMulcallString(txtNSRZone.arrValueMember) + ") "
                whrForPM_Supply += " and TSPL_CUSTOMER_MASTER.Zone_Code  in (" + clsCommon.GetMulcallString(txtNSRZone.arrValueMember) + ") "
                whrLR_PQ_ES = " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtNSRZone.arrValueMember) + ")  "
                strCR_CD_SO_CASH_whre += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtNSRZone.arrValueMember) + ")  "
            End If
            If txtRouteNetSales.arrValueMember IsNot Nothing AndAlso txtRouteNetSales.arrValueMember.Count > 0 Then
                whr += " and TSPL_BOOKING_DETAIL.route_No  in (" + clsCommon.GetMulcallString(txtRouteNetSales.arrValueMember) + ") "
                whrForPM_Supply += " and TSPL_BOOKING_DETAIL.route_No  in (" + clsCommon.GetMulcallString(txtRouteNetSales.arrValueMember) + ") "
                whrLR_PQ_ES += " and TSPL_SD_SHIPMENT_HEAD.route_No  in (" + clsCommon.GetMulcallString(txtRouteNetSales.arrValueMember) + ") "
                strCR_CD_SO_CASH_whre += " and TSPL_BOOKING_DETAIL.route_No  in (" + clsCommon.GetMulcallString(txtRouteNetSales.arrValueMember) + ") "
            ElseIf chkConsiderShowEarlyRoute.Checked = True Then
                If chkShowEarlyRoute.Checked = True Then
                    whr += " and TSPL_BOOKING_DETAIL.route_No  in (" + clsCommon.myCstr(StrEarlyRoute) + ") "
                    whrForPM_Supply += " and TSPL_BOOKING_DETAIL.route_No  in (" + clsCommon.myCstr(StrEarlyRoute) + ") "
                    whrLR_PQ_ES += " and TSPL_SD_SHIPMENT_HEAD.route_No  in (" + clsCommon.myCstr(StrEarlyRoute) + ") "
                    strCR_CD_SO_CASH_whre += " and TSPL_BOOKING_DETAIL.route_No  in (" + clsCommon.myCstr(StrEarlyRoute) + ") "
                Else
                    whr += " and TSPL_BOOKING_DETAIL.route_No  in (" + clsCommon.myCstr(StrNonEarlyRoute) + ") "
                    whrForPM_Supply += " and TSPL_BOOKING_DETAIL.route_No  in (" + clsCommon.myCstr(StrNonEarlyRoute) + ") "
                    whrLR_PQ_ES += " and TSPL_SD_SHIPMENT_HEAD.route_No  in (" + clsCommon.myCstr(StrNonEarlyRoute) + ") "
                    strCR_CD_SO_CASH_whre += " and TSPL_BOOKING_DETAIL.route_No  in (" + clsCommon.myCstr(StrNonEarlyRoute) + ") "
                End If
            End If
            If clsCommon.CompairString(cboNSRType.SelectedValue, "Zonewise Net Sales(Milk)") = CompairStringResult.Equal Then
                whr += " and TSPL_ITEM_MASTER. Is_Milk_Pouch =1  "
                whrForPM_Supply += " and TSPL_ITEM_MASTER. Is_Milk_Pouch =1  "
                whrLR_PQ_ES += " and TSPL_ITEM_MASTER. Is_Milk_Pouch =1 "
            ElseIf clsCommon.CompairString(cboNSRType.SelectedValue, "Zonewise Net Sales(Product)") = CompairStringResult.Equal Then
                whr += " and TSPL_ITEM_MASTER. Is_Milk_Pouch =0  "
                whrForPM_Supply += " and TSPL_ITEM_MASTER. Is_Milk_Pouch =0  "
                whrLR_PQ_ES += "  and TSPL_ITEM_MASTER. Is_Milk_Pouch =0 "
            End If
            Dim TruckSheetQuery As String = "  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name, case when isnull (Target_UOM.Conversion_Factor,0) > 0 then  Convert (decimal(18,2), (isnull(Source_UOM.Conversion_Factor,0) /NULLIF (Target_UOM.Conversion_Factor,0)) *  isnull (TSPL_BOOKING_DETAIL.Booking_Qty,0) ) else  Convert (decimal(18,2), (isnull(Source_UOM.Conversion_Factor,0) /NULLIF (Target_UOM_KG.Conversion_Factor,0)) *  isnull (TSPL_BOOKING_DETAIL.Booking_Qty,0) )  end as Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  " &
                                            "  Left outer Join TSPL_ITEM_UOM_DETAIL as Source_UOM on Source_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Source_UOM.UOM_Code = TSPL_BOOKING_DETAIL.Unit_code  " &
                                            "  Left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM on Target_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_UOM.UOM_Code = 'LTR' " &
                                            "  Left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM_KG on Target_UOM_KG.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_UOM_KG.UOM_Code = 'KG' " &
                                            "  where TSPL_BOOKING_DETAIL.Booking_Qty>0 and Scheme_Item = 'N'  and TSPL_BOOKING_MATSER.againstgatepass=0   " &
                                            "  and TSPL_BOOKING_MATSER.TruckSheetGenerate = 1  " + whr + " "
            Dim AM_SupplyQuery As String = "  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name, case when isnull (Target_UOM.Conversion_Factor,0) > 0 then  Convert (decimal(18,2), (isnull(Source_UOM.Conversion_Factor,0) /NULLIF (Target_UOM.Conversion_Factor,0)) *  isnull (TSPL_BOOKING_DETAIL.Booking_Qty,0) ) else  Convert (decimal(18,2), (isnull(Source_UOM.Conversion_Factor,0) /NULLIF (Target_UOM_KG.Conversion_Factor,0)) *  isnull (TSPL_BOOKING_DETAIL.Booking_Qty,0) )  end as Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  " &
                                           "  Left outer Join TSPL_ITEM_UOM_DETAIL as Source_UOM on Source_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Source_UOM.UOM_Code = TSPL_BOOKING_DETAIL.Unit_code  " &
                                           "  Left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM on Target_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_UOM.UOM_Code = 'LTR' " &
                                           "  Left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM_KG on Target_UOM_KG.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_UOM_KG.UOM_Code = 'KG' " &
                                           "  where TSPL_BOOKING_DETAIL.Booking_Qty>0 and Scheme_Item = 'N' and TSPL_BOOKING_MATSER.Gatepass_type='AM' " &
                                           "  and TSPL_BOOKING_MATSER.AgainstGatePass = 1   " + whr + " "
            Dim PM_SupplyQuery As String = "  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name, case when isnull (Target_UOM.Conversion_Factor,0) > 0 then  Convert (decimal(18,2), (isnull(Source_UOM.Conversion_Factor,0) /NULLIF (Target_UOM.Conversion_Factor,0)) *  isnull (TSPL_BOOKING_DETAIL.Booking_Qty,0) ) else  Convert (decimal(18,2), (isnull(Source_UOM.Conversion_Factor,0) /NULLIF (Target_UOM_KG.Conversion_Factor,0)) *  isnull (TSPL_BOOKING_DETAIL.Booking_Qty,0) )  end as   Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  " &
                                           "  Left outer Join TSPL_ITEM_UOM_DETAIL as Source_UOM on Source_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Source_UOM.UOM_Code = TSPL_BOOKING_DETAIL.Unit_code  " &
                                           "  Left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM on Target_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_UOM.UOM_Code = 'LTR'  " &
                                           "  Left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM_KG on Target_UOM_KG.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_UOM_KG.UOM_Code = 'KG' " &
                                           "  where TSPL_BOOKING_DETAIL.Booking_Qty>0 and Scheme_Item = 'N' and TSPL_BOOKING_MATSER.Gatepass_type='PM' " &
                                           "  " + whrForPM_Supply + " " 'and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY  in ('Distributor') 
            Dim Institution_CR As String = "  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name, case when isnull (Target_UOM.Conversion_Factor,0) > 0 then  Convert (decimal(18,2), (isnull(Source_UOM.Conversion_Factor,0) /NULLIF (Target_UOM.Conversion_Factor,0)) *  isnull (TSPL_BOOKING_DETAIL.Booking_Qty,0) ) else  Convert (decimal(18,2), (isnull(Source_UOM.Conversion_Factor,0) /NULLIF (Target_UOM_KG.Conversion_Factor,0)) *  isnull (TSPL_BOOKING_DETAIL.Booking_Qty,0) )  end as   Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  " &
                                           "  Left outer Join TSPL_ITEM_UOM_DETAIL as Source_UOM on Source_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Source_UOM.UOM_Code = TSPL_BOOKING_DETAIL.Unit_code  " &
                                           "  Left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM on Target_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_UOM.UOM_Code = 'LTR'  " &
                                           "  Left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM_KG on Target_UOM_KG.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_UOM_KG.UOM_Code = 'KG' " &
                                           "  where TSPL_BOOKING_DETAIL.Booking_Qty>0 and Scheme_Item = 'N'  " &
                                           "  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY  in ('Institution CR')  " + whr + " "
            Dim Institution_SO As String = "  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name, case when isnull (Target_UOM.Conversion_Factor,0) > 0 then  Convert (decimal(18,2), (isnull(Source_UOM.Conversion_Factor,0) /NULLIF (Target_UOM.Conversion_Factor,0)) *  isnull (TSPL_BOOKING_DETAIL.Booking_Qty,0) ) else  Convert (decimal(18,2), (isnull(Source_UOM.Conversion_Factor,0) /NULLIF (Target_UOM_KG.Conversion_Factor,0)) *  isnull (TSPL_BOOKING_DETAIL.Booking_Qty,0) )  end as   Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  " &
                                           "  Left outer Join TSPL_ITEM_UOM_DETAIL as Source_UOM on Source_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Source_UOM.UOM_Code = TSPL_BOOKING_DETAIL.Unit_code  " &
                                           "  Left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM on Target_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_UOM.UOM_Code = 'LTR'  " &
                                           "  Left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM_KG on Target_UOM_KG.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_UOM_KG.UOM_Code = 'KG' " &
                                           "  where TSPL_BOOKING_DETAIL.Booking_Qty>0 and Scheme_Item = 'N'  " &
                                           "  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY  in ('Institution SO')  " + whr + " "


            Dim qry As String = ""
            If clsCommon.CompairString(cboNSRType.SelectedValue, "Daywise Net Sales") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboNSRType.SelectedValue, "Milk Daywise Net Sales") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboNSRType.SelectedValue, "Product Daywise Net Sales") = CompairStringResult.Equal Then
                qry = " select ROW_NUMBER() OVER (ORDER BY XXXFinal.Item_Code) AS SNO, XXXFinal.Item_Code , max (XXXFinal.Alies_Name) as Alies_Name, sum (TruckSheet) as TruckSheet , sum (AM_Supply) as AM_Supply , sum (PM_Supply) as PM_Supply ,  sum (TruckSheet) + sum (AM_Supply) + sum (PM_Supply) as Net_Amount from ( " &
                      " Select Final.Item_Code, Final.Alies_Name, isnull( Final.Booking_Qty,0) as TruckSheet, 0 as AM_Supply, 0 as PM_Supply    From ( " &
                      " " + TruckSheetQuery + " " &
                      " ) Final " &
                      " Union All " &
                      " Select Final.Item_Code, Final.Alies_Name, 0 as TruckSheet, isnull( Final.Booking_Qty,0)  as AM_Supply, 0 as PM_Supply    From (  " &
                      " " + AM_SupplyQuery + " " &
                      "  ) Final " &
                      " Union All  " &
                      " Select Final.Item_Code, Final.Alies_Name, 0 as TruckSheet, 0  as AM_Supply, isnull(Final.Booking_Qty,0) as PM_Supply   From ( " &
                      " " + PM_SupplyQuery + " " &
                      " ) Final " &
                      "  ) XXXFinal Group By XXXFinal.Item_Code " &
                      " "
            Else
                'qry = " select ROW_NUMBER() OVER (ORDER BY XXXFinal.Zone_Code) AS SNO, XXXFinal.Zone_Code , max (XXXFinal.Zone_Name) as Zone_Name, sum (TruckSheet) as TruckSheet , sum (AM_Supply) as AM_Supply , sum (PM_Supply) as PM_Supply ,  sum (TruckSheet) + sum (AM_Supply) + sum (PM_Supply) as Net_Amount from ( " & _
                '      " Select Final.Zone_Code, Final.Zone_Name, isnull (Final.Booking_Qty,0) as TruckSheet, 0 as AM_Supply, 0 as PM_Supply    From ( " & _
                '      " " + TruckSheetQuery + " " & _
                '      " ) Final " & _
                '      " Union All " & _
                '      " Select Final.Zone_Code, Final.Zone_Name, 0 as TruckSheet, isnull(Final.Booking_Qty,0)  as AM_Supply, 0 as PM_Supply    From (  " & _
                '      " " + AM_SupplyQuery + " " & _
                '      "  ) Final " & _
                '      " Union All  " & _
                '      " Select Final.Zone_Code, Final.Zone_Name, 0 as TruckSheet, 0  as AM_Supply, isnull(Final.Booking_Qty,0) as PM_Supply   From ( " & _
                '      " " + PM_SupplyQuery + " " & _
                '      " ) Final " & _
                '      "  ) XXXFinal Group By XXXFinal.Zone_Code " & _
                '      " "
                qry = " select max(isnull(XXXXFinalLR_PQ_ES.LR,0)) as LR ,max(isnull(XXXXFinalLR_PQ_ES.PQ,0)) as PQ, max(isnull(XXXXFinalLR_PQ_ES.ES,0)) as ES ,max(isnull(XXXXFinalLR_PQ_ES.Total_LR_PQ_ES_Qty,0)) as Total_LR_PQ_ES_Qty,  " &
                     "  ROW_NUMBER() OVER (ORDER BY XXXFinal.Zone_Code) AS SNO, XXXFinal.Zone_Code , max (XXXFinal.Zone_Name) as Zone_Name, sum (TruckSheet) as TruckSheet , sum (AM_Supply) as AM_Supply , sum (PM_Supply) as PM_Supply ,  sum (TruckSheet) + sum (AM_Supply) + sum (PM_Supply) as Net_Amount from ( " &
                     " Select 1 as ID, Final.Zone_Code, Final.Zone_Name, isnull (Final.Booking_Qty,0) as TruckSheet, 0 as AM_Supply, 0 as PM_Supply    From ( " &
                     " " + TruckSheetQuery + " " &
                     " ) Final " &
                     " Union All " &
                     " Select 1 as ID, Final.Zone_Code, Final.Zone_Name, 0 as TruckSheet, isnull(Final.Booking_Qty,0)  as AM_Supply, 0 as PM_Supply    From (  " &
                     " " + AM_SupplyQuery + " " &
                     "  ) Final " &
                     " Union All  " &
                     " Select 1 as ID, Final.Zone_Code, Final.Zone_Name, 0 as TruckSheet, 0  as AM_Supply, isnull(Final.Booking_Qty,0) as PM_Supply   From ( " &
                     " " + PM_SupplyQuery + " " &
                     " ) Final " &
                     "  ) XXXFinal " &
                     " Left Outer Join " &
                     " ( " &
                     " Select   1 as ID, sum(LR) as LR , sum (PQ) as PQ , Sum ( ES ) as ES ,sum(LR) + sum (PQ) + Sum ( ES )  Total_LR_PQ_ES_Qty from  " &
                     " ( " &
                     " Select TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_Code, convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 'LTR' as  Unit_code ,isnull( Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty),0) as LR , 0 as PQ , 0 as ES from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='LTR'  Left Outer Join TSPL_CUSTOMER_COMPLAINT_HEAD on TSPL_CUSTOMER_COMPLAINT_HEAD.complaint_no = TSPL_SD_SHIPMENT_HEAD.customer_complaint_no left outer join TSPL_CUSTOMER_COMPLAINT_detail on TSPL_CUSTOMER_COMPLAINT_detail.Complaint_no=TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_no and  TSPL_CUSTOMER_COMPLAINT_detail.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code where   Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MMM/yyyy") + "',103) and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRToDate.Value, "dd/MMM/yyyy") + "',103)    and TSPL_SD_SHIPMENT_HEAD.status = 1     and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1 and TSPL_CUSTOMER_COMPLAINT_detail.Complaint_Code = 'LR' " + whrLR_PQ_ES + " " &
                     " Union All " &
                     " Select TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_Code, convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 'LTR' as  Unit_code ,0 as LR , isnull( Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty),0) as PQ , 0 as ES from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='LTR'  Left Outer Join TSPL_CUSTOMER_COMPLAINT_HEAD on TSPL_CUSTOMER_COMPLAINT_HEAD.complaint_no = TSPL_SD_SHIPMENT_HEAD.customer_complaint_no left outer join TSPL_CUSTOMER_COMPLAINT_detail on TSPL_CUSTOMER_COMPLAINT_detail.Complaint_no=TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_no and  TSPL_CUSTOMER_COMPLAINT_detail.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code where   Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MMM/yyyy") + "',103) and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRToDate.Value, "dd/MMM/yyyy") + "',103)    and TSPL_SD_SHIPMENT_HEAD.status = 1     and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1 and TSPL_CUSTOMER_COMPLAINT_detail.Complaint_Code = 'PQ' " + whrLR_PQ_ES + " " &
                     " Union All " &
                     " Select TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_Code, convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 'LTR' as  Unit_code ,0 as LR , 0 as PQ , isnull( Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty),0) as ES from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='LTR'  Left Outer Join TSPL_CUSTOMER_COMPLAINT_HEAD on TSPL_CUSTOMER_COMPLAINT_HEAD.complaint_no = TSPL_SD_SHIPMENT_HEAD.customer_complaint_no left outer join TSPL_CUSTOMER_COMPLAINT_detail on TSPL_CUSTOMER_COMPLAINT_detail.Complaint_no=TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_no and  TSPL_CUSTOMER_COMPLAINT_detail.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code where    Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MMM/yyyy") + "',103) and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRToDate.Value, "dd/MMM/yyyy") + "',103)   and TSPL_SD_SHIPMENT_HEAD.status = 1     and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1 and TSPL_CUSTOMER_COMPLAINT_detail.Complaint_Code = 'ES' " + whrLR_PQ_ES + " " &
                     " )XXXX1  where Exists  " &
                     " ( " &
                     " Select Zone_Code from (  " &
                     " Select Zone_Code From ( " &
                     " Select TSPL_CUSTOMER_MASTER.Zone_Code from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code    Left outer Join TSPL_ITEM_UOM_DETAIL as Source_UOM on Source_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Source_UOM.UOM_Code = TSPL_BOOKING_DETAIL.Unit_code    Left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM on Target_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_UOM.UOM_Code = 'LTR'   where Scheme_Item = 'N'       and TSPL_BOOKING_MATSER.AgainstGatePass = 1    " + whr + " " &
                     " Union All " &
                     " Select TSPL_CUSTOMER_MASTER.Zone_Code  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code    Left outer Join TSPL_ITEM_UOM_DETAIL as Source_UOM on Source_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Source_UOM.UOM_Code = TSPL_BOOKING_DETAIL.Unit_code    Left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM on Target_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_UOM.UOM_Code = 'LTR'    where Scheme_Item = 'N'    and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY  in ('Distributor')    " + whr + " " &
                     " Union All " &
                     " Select TSPL_CUSTOMER_MASTER.Zone_Code  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code    Left outer Join TSPL_ITEM_UOM_DETAIL as Source_UOM on Source_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Source_UOM.UOM_Code = TSPL_BOOKING_DETAIL.Unit_code    Left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM on Target_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_UOM.UOM_Code = 'LTR'   where Scheme_Item = 'N'       and TSPL_BOOKING_MATSER.TruckSheetGenerate = 1    " + whr + " " &
                     " ) XXXXLR_PQ_ES group By Zone_Code having XXXX1.Zone_code = XXXXLR_PQ_ES.Zone_code " &
                     " )XXXXFinalQty " &
                     " ) " &
                     " ) XXXXFinalLR_PQ_ES on XXXXFinalLR_PQ_ES.ID = XXXFinal.ID " &
                     "  " &
                     "  " &
                     "  " &
                     "  " &
                     "  " &
                     " Group By XXXFinal.Zone_Code " &
                     " "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then

                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = objCommonVar.CurrentCompanyName
                If clsCommon.CompairString(cboNSRType.SelectedValue, "Daywise Net Sales") = CompairStringResult.Equal Then
                    obj.ReportName1 = "Day Wise Net Sales for On " + clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MM/yyyy") + " "
                ElseIf clsCommon.CompairString(cboNSRType.SelectedValue, "Milk Daywise Net Sales") = CompairStringResult.Equal Then
                    obj.ReportName1 = "Milk Day Wise Net Sales for On " + clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MM/yyyy") + " "
                ElseIf clsCommon.CompairString(cboNSRType.SelectedValue, "Product Daywise Net Sales") = CompairStringResult.Equal Then
                    obj.ReportName1 = "Product Day Wise Net Sales for On " + clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MM/yyyy") + " "
                Else
                    Dim strDateRange As String = clsCommon.myCstr(clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MM/yyyy")) + " To " + clsCommon.myCstr(clsCommon.GetPrintDate(dtpNSRToDate.Value, "dd/MM/yyyy"))
                    obj.ReportName1 = "Zone Wise net sales for MILK From " + strDateRange + " "
                End If

                obj.ShowPageNo = True
                obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
                'If clsCommon.CompairString(cboNSRType.SelectedValue, "Daywise Net Sales") = CompairStringResult.Equal Then
                '    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Day Wise Net Sales for On ", clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MM/yyyy")))
                'Else
                '    Dim strDateRange As String = clsCommon.myCstr(clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MM/yyyy")) + " To " + clsCommon.myCstr(clsCommon.GetPrintDate(dtpNSRToDate.Value, "dd/MM/yyyy"))
                '    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Zone Wise net sales for MILK From ", strDateRange))
                'End If

                'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Shift ", clsCommon.myCstr(cboABSReportShift.SelectedValue)))
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Type", clsCommon.myCstr(cboNSRType.SelectedValue)))

                'obj.arrGroup = New List(Of clsDosPrintGroup)()
                'If clsCommon.CompairString(cboABSReportType.SelectedValue, "Daywise Net Sales") = CompairStringResult.Equal Then

                'Else
                '    obj.arrGroup.Add(clsDosPrintGroup.GetObject("Route_Desc", "ROUTE", ""))
                '    'obj.arrGroup.Add(clsDosPrintGroup.GetObject("Zone_Code", "ZONE", ""))
                'End If

                obj.arrColumn = New List(Of clsDosPrintColumn)()
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SNO", "SNo", True, DosPrintAlignment.Left, 5, False, DecimalPlaces.NA))
                If clsCommon.CompairString(cboNSRType.SelectedValue, "Daywise Net Sales") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboNSRType.SelectedValue, "Milk Daywise Net Sales") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboNSRType.SelectedValue, "Product Daywise Net Sales") = CompairStringResult.Equal Then
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Alies_Name", "Type Of Milk", True, DosPrintAlignment.Left, 18, False, DecimalPlaces.NA))
                Else
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Zone_Name", "Zone", True, DosPrintAlignment.Left, 18, False, DecimalPlaces.NA))

                End If
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("TruckSheet", "Trucksheet", False, DosPrintAlignment.Right, 18, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("AM_Supply", "AM Supply", False, DosPrintAlignment.Right, 18, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("PM_Supply", "PM Supply", False, DosPrintAlignment.Right, 18, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Net_Amount", "NetSales(Ltr/Kg)", False, DosPrintAlignment.Right, 23, True, DecimalPlaces.Two))
                If clsCommon.CompairString(cboNSRType.SelectedValue, "Daywise Net Sales") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboNSRType.SelectedValue, "Milk Daywise Net Sales") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboNSRType.SelectedValue, "Product Daywise Net Sales") = CompairStringResult.Equal Then
                    Dim QryPMSupplyCustomerWise As String = " select XXXFinal.Cust_Code ,max(len(XXXFinal.Customer_Name)) as Customer_Name_Length ,max (XXXFinal.Customer_Name) as Customer_Name, sum (TruckSheet) as TruckSheet , sum (AM_Supply) as AM_Supply , sum (PM_Supply) as PM_Supply ,  sum (TruckSheet) + sum (AM_Supply) + sum (PM_Supply) as Net_Amount From (  " &
                                                            "  Select  Final.Cust_Code,Final.Customer_Name, Final.Item_Code, Final.Alies_Name, 0 as TruckSheet, 0  as AM_Supply, isnull(Final.Booking_Qty,0) as PM_Supply   From (  " &
                                                            " " + PM_SupplyQuery + " " &
                                                            "  ) Final  " &
                                                            " ) XXXFinal Group By XXXFinal.Cust_Code   "
                    Dim dtPMSupplyCustomerWise As DataTable = clsDBFuncationality.GetDataTable(QryPMSupplyCustomerWise)
                    obj.arrReportFooter = New List(Of clsDosPrintReportFooter)
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Evening Supply on " + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFromDatePrvDay), "dd/MMM/yyyy")), "", "", "", ""))
                    Dim dblTotalPMSupply As Double = 0
                    Dim dblMaxCustomerNameChar As Integer = 0
                    Dim strLineWithDes As String = ""
                    Dim strSpaceWithTotalFoter As String = ""

                    If dtPMSupplyCustomerWise IsNot Nothing And dtPMSupplyCustomerWise.Rows.Count > 0 Then
                        For Each dr As DataRow In dtPMSupplyCustomerWise.Rows
                            Dim strCustName As String = ""
                            Dim strPMSupplyValue As String = ""

                            strCustName = clsCommon.myCstr(dr("Customer_Name"))
                            strPMSupplyValue = clsCommon.myCstr(dr("PM_Supply"))
                            dblTotalPMSupply = dblTotalPMSupply + clsCommon.myCdbl(dr("PM_Supply"))
                            If clsCommon.myCdbl(dr("Customer_Name_Length")) > dblMaxCustomerNameChar Then
                                dblMaxCustomerNameChar = clsCommon.myCdbl(dr("Customer_Name_Length"))
                            End If
                            obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject(strCustName, clsCommon.myCstr(strPMSupplyValue), "", "", ":"))
                        Next
                    End If
                    If dblMaxCustomerNameChar < 29 Then
                        dblMaxCustomerNameChar = 29
                    End If
                    For value As Integer = 1 To dblMaxCustomerNameChar
                        strLineWithDes = strLineWithDes + "-"
                        If value > 5 Then
                            strSpaceWithTotalFoter = strSpaceWithTotalFoter + " "
                        End If
                    Next

                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject(strLineWithDes, "----------", "", "", " "))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("TOTAL" + strSpaceWithTotalFoter, clsCommon.myCstr(dblTotalPMSupply), "", "", ":"))
                    'obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject(strLineWithDes, "----------", "", "", " "))
                    '================================================================================================================
                    Dim qryInstitutionSO As String = "  Select isnull (sum (isnull(Final.Booking_Qty,0)),0) as PM_Supply_SO  from ( " &
                                                     " " + Institution_SO + " " &
                                                     "   ) Final " &
                                                     " " &
                                                     " "
                    Dim dtInstitutionSO As DataTable = clsDBFuncationality.GetDataTable(qryInstitutionSO)
                    Dim valueSo As String = 0
                    If dtInstitutionSO IsNot Nothing And dtInstitutionSO.Rows.Count > 0 Then
                        valueSo = clsCommon.myCstr(dtInstitutionSO.Rows(0)("PM_Supply_SO"))
                    End If
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Special Order  ", valueSo, "", "", ":"))
                    '================================================================================================================
                    Dim qryInstitutionCR As String = "  Select isnull (sum (isnull(Final.Booking_Qty,0)),0) as PM_Supply_CR  from ( " &
                                                     " " + Institution_CR + " " &
                                                     "   ) Final " &
                                                     " " &
                                                     " "
                    Dim dtInstitutionCR As DataTable = clsDBFuncationality.GetDataTable(qryInstitutionCR)
                    Dim valueCR As String = 0
                    If dtInstitutionCR IsNot Nothing And dtInstitutionCR.Rows.Count > 0 Then
                        valueCR = clsCommon.myCstr(dtInstitutionCR.Rows(0)("PM_Supply_CR"))
                    End If
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Credit         ", valueCR, "", "", ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject(strLineWithDes, "----------", "", "", " "))
                    '================================================================================================================
                    Dim QryPMSupplyPrvYear As String = " Select isnull (sum (isnull(Final.Booking_Qty,0)),0) as PM_Supply_Prv_YearSale  from ( " &
                                                       " " + PM_SupplyQuery + " " &
                                                       " ) Final " &
                                                       "  "
                    Dim strCurrentDate As String = clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MMM/yyyy")
                    Dim strPrvYearDate As String = clsCommon.GetPrintDate(dtFromDatePrvYear, "dd/MMM/yyyy")
                    QryPMSupplyPrvYear = QryPMSupplyPrvYear.Replace(strCurrentDate, strPrvYearDate)
                    Dim valuePrvYear As String = 0
                    Dim dtPMSupplyPrvYear As DataTable = clsDBFuncationality.GetDataTable(QryPMSupplyPrvYear)
                    If dtPMSupplyPrvYear IsNot Nothing And dtPMSupplyPrvYear.Rows.Count > 0 Then
                        valuePrvYear = clsCommon.myCstr(dtPMSupplyPrvYear.Rows(0)("PM_Supply_Prv_YearSale"))
                    End If
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Last Year Net  ", valuePrvYear, "", "", ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Difference     ", clsCommon.myCstr(clsCommon.myCdbl(dblTotalPMSupply) - clsCommon.myCdbl(valuePrvYear)), "", "", ":"))
                    '========================================== Crate Details Start ================================

                    Dim strAMDispatchCrateQty As Decimal = 0
                    Dim strPMDispatchCrateQty As Decimal = 0
                    Dim strAMPMTotalDistachCrateQty As Decimal = 0

                    Dim strMorningFromTime As String = "08:00"
                    Dim strMorningToTime As String = "20:00"
                    Dim strEveningFromTime As String = "19:59"
                    Dim strEveningToTime As String = "07:59"
                    Dim strFromDateMorning As String = dtFromDatePrvDay + " " + strMorningFromTime
                    Dim strToDateMorning As String = dtFromDatePrvDay + " " + strMorningToTime

                    Dim strFromDateEveing As String = dtFromDatePrvDay + " " + strEveningFromTime
                    Dim strToDateForEvening As String = DateAdd("d", 1, dtFromDatePrvDay) + " " + strEveningToTime

                    Dim whrCreateDetailsMorning As String = ""
                    Dim whrCreateDetailsEvening As String = ""
                    Dim whrCreateReceivedMorning As String = ""
                    Dim whrCreateReceivedEvening As String = ""
                    Dim whrOutStanding As String = ""

                    If clsCommon.CompairString(cboNSRType.SelectedValue, "Daywise Net Sales") = CompairStringResult.Equal Then
                        whrCreateDetailsMorning = " and  Document_Date  between  '" + clsCommon.GetPrintDate(strFromDateMorning, "dd/MMM/yyyy HH:mm") + "'  and  '" + clsCommon.GetPrintDate(strToDateMorning, "dd/MMM/yyyy HH:mm") + "' "
                        whrCreateDetailsEvening = " and  Document_Date  between  '" + clsCommon.GetPrintDate(strFromDateEveing, "dd/MMM/yyyy HH:mm") + "'  and  '" + clsCommon.GetPrintDate(strToDateForEvening, "dd/MMM/yyyy HH:mm") + "' "
                    ElseIf clsCommon.CompairString(cboNSRType.SelectedValue, "Milk Daywise Net Sales") = CompairStringResult.Equal Then
                        whrCreateDetailsMorning = "  and TSPL_ITEM_MASTER.Is_Milk_Pouch =1 and  Document_Date  between  '" + clsCommon.GetPrintDate(strFromDateMorning, "dd/MMM/yyyy HH:mm") + "'  and  '" + clsCommon.GetPrintDate(strToDateMorning, "dd/MMM/yyyy HH:mm") + "' "
                        whrCreateDetailsEvening = "  and TSPL_ITEM_MASTER.Is_Milk_Pouch =1 and  Document_Date  between  '" + clsCommon.GetPrintDate(strFromDateEveing, "dd/MMM/yyyy HH:mm") + "'  and  '" + clsCommon.GetPrintDate(strToDateForEvening, "dd/MMM/yyyy HH:mm") + "' "
                    ElseIf clsCommon.CompairString(cboNSRType.SelectedValue, "Product Daywise Net Sales") = CompairStringResult.Equal Then
                        whrCreateDetailsMorning = "  and TSPL_ITEM_MASTER.Is_Milk_Pouch =0 and  Document_Date  between  '" + clsCommon.GetPrintDate(strFromDateMorning, "dd/MMM/yyyy HH:mm") + "'  and  '" + clsCommon.GetPrintDate(strToDateMorning, "dd/MMM/yyyy HH:mm") + "' "
                        whrCreateDetailsEvening = " and TSPL_ITEM_MASTER.Is_Milk_Pouch =0 and  Document_Date  between  '" + clsCommon.GetPrintDate(strFromDateEveing, "dd/MMM/yyyy HH:mm") + "'  and  '" + clsCommon.GetPrintDate(strToDateForEvening, "dd/MMM/yyyy HH:mm") + "' "
                    End If
                    whrCreateReceivedMorning = " and  Document_Date  between  '" + clsCommon.GetPrintDate(strFromDateMorning, "dd/MMM/yyyy HH:mm") + "'  and  '" + clsCommon.GetPrintDate(strToDateMorning, "dd/MMM/yyyy HH:mm") + "' "
                    whrCreateReceivedEvening = " and  Document_Date  between  '" + clsCommon.GetPrintDate(strFromDateEveing, "dd/MMM/yyyy HH:mm") + "'  and  '" + clsCommon.GetPrintDate(strToDateForEvening, "dd/MMM/yyyy HH:mm") + "' "


                    If txtNSRLocation.arrValueMember IsNot Nothing AndAlso txtNSRLocation.arrValueMember.Count > 0 Then
                        whrCreateDetailsMorning += " and TSPL_BOOKING_DETAIL.Loc_Code  in (" + clsCommon.GetMulcallString(txtNSRLocation.arrValueMember) + ") "
                        whrCreateDetailsEvening += " and TSPL_BOOKING_DETAIL.Loc_Code  in (" + clsCommon.GetMulcallString(txtNSRLocation.arrValueMember) + ") "
                        whrCreateReceivedMorning += " and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE. Location_Code in (" + clsCommon.GetMulcallString(txtNSRLocation.arrValueMember) + ") "
                        whrCreateReceivedEvening += " and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE. Location_Code in (" + clsCommon.GetMulcallString(txtNSRLocation.arrValueMember) + ") "
                    End If
                    If txtNSRZone.arrValueMember IsNot Nothing AndAlso txtNSRZone.arrValueMember.Count > 0 Then
                        whrCreateDetailsMorning += " and TSPL_CUSTOMER_MASTER.Zone_Code  in (" + clsCommon.GetMulcallString(txtNSRZone.arrValueMember) + ") "
                        whrCreateDetailsEvening += " and TSPL_CUSTOMER_MASTER.Zone_Code  in (" + clsCommon.GetMulcallString(txtNSRZone.arrValueMember) + ") "
                        whrCreateReceivedMorning += " and TSPL_CUSTOMER_MASTER.Zone_Code  in (" + clsCommon.GetMulcallString(txtNSRZone.arrValueMember) + ") "
                        whrCreateReceivedEvening += " and TSPL_CUSTOMER_MASTER.Zone_Code  in (" + clsCommon.GetMulcallString(txtNSRZone.arrValueMember) + ") "
                        whrOutStanding += " and TSPL_CUSTOMER_MASTER.Zone_Code  in (" + clsCommon.GetMulcallString(txtNSRZone.arrValueMember) + ") "
                    End If
                    If txtRouteNetSales.arrValueMember IsNot Nothing AndAlso txtRouteNetSales.arrValueMember.Count > 0 Then
                        whrCreateDetailsMorning += " and TSPL_BOOKING_DETAIL.route_No  in (" + clsCommon.GetMulcallString(txtRouteNetSales.arrValueMember) + ") "
                        whrCreateDetailsEvening += " and TSPL_BOOKING_DETAIL.route_No  in (" + clsCommon.GetMulcallString(txtRouteNetSales.arrValueMember) + ") "
                        whrCreateReceivedMorning += " and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code in (" + clsCommon.GetMulcallString(txtRouteNetSales.arrValueMember) + ") "
                        whrCreateReceivedEvening += " and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code in (" + clsCommon.GetMulcallString(txtRouteNetSales.arrValueMember) + ") "
                    ElseIf chkConsiderShowEarlyRoute.Checked = True Then
                        If chkShowEarlyRoute.Checked = True Then
                            whrCreateDetailsMorning += " and TSPL_BOOKING_DETAIL.route_No  in (" + clsCommon.myCstr(StrEarlyRoute) + ") "
                            whrCreateDetailsEvening += " and TSPL_BOOKING_DETAIL.route_No  in (" + clsCommon.myCstr(StrEarlyRoute) + ") "
                            whrCreateReceivedMorning += " and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code in (" + clsCommon.myCstr(StrEarlyRoute) + ") "
                            whrCreateReceivedEvening += " and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code in (" + clsCommon.myCstr(StrEarlyRoute) + ") "
                        Else
                            whrCreateDetailsMorning += " and TSPL_BOOKING_DETAIL.route_No  in (" + clsCommon.myCstr(StrNonEarlyRoute) + ") "
                            whrCreateDetailsEvening += " and TSPL_BOOKING_DETAIL.route_No  in (" + clsCommon.myCstr(StrNonEarlyRoute) + ") "
                            whrCreateReceivedMorning += " and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code in (" + clsCommon.myCstr(StrNonEarlyRoute) + ") "
                            whrCreateReceivedEvening += " and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code in (" + clsCommon.myCstr(StrNonEarlyRoute) + ") "
                        End If
                    End If


                    qry = "  Select isnull (sum (XXXXXXFinal.CrateQty_New),0) as CrateQty from ( select '-' as Desh, XXXXXFinal.Document_Date,XXXXXFinal.Cust_Code, XXXXXFinal.Customer_Name as Customer_Name,  XXXXXFinal.route_no  as route_no ,  XXXXXFinal.Route_Desc  as Route_Desc,XXXXXFinal.Zone_Code,XXXXXFinal.Zone_Name,XXXXXFinal.Item_Code,SUBSTRING (XXXXXFinal.Alies_Name,0,8) as Alies_Name, Convert (integer,XXXXXFinal.CR) as CR,Convert (Integer ,XXXXXFinal.CD) as CD,convert (Integer,XXXXXFinal.SO) as SO,Convert (Integer ,XXXXXFinal.Cash,103) as Cash ,Convert (Integer,XXXXXFinal.Total) as Total,XXXXXFinal.CrateQty_New as CrateQty_New , XXXXXFinal.PendingPcsQty_New as PendingPcsQty_New, XXXXXFinal.Amount_with_Tax, XXXXXFinal.  DataSNO,   XXXXXFinal. Unit_code, XXXXXFinal.LtrQty from (  "
                    qry += " select  XXXXX.Document_Date, 'Route Total' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CEILING_Crate_Qty  as CrateQty_New , PendingPcsQty as PendingPcsQty_New,XXXXX.Amount_with_Tax, '3' as  DataSNO, '' as  Unit_code,  XXXXX.LtrQty from (    Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  from "
                    qry += " ( "
                    qry += " Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty ,   (case when coalesce(StockLtr.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockLtr.Conversion_Factor,1)) as numeric(18,3)) end) as LtrQty "
                    qry += " from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , "

                    qry += " max(XFinal.route_no ) as route_no "

                    qry += " , max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others') "
                    qry += " " + whrCreateDetailsMorning + " "
                    qry += "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by   "

                    qry += "  XFinal.Item_Code,Unit_code  ) XXFinal "
                    qry += " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  "
                    qry += " left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code "
                    qry += " left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='Ltr') as StockLtr on XXFinal.Item_Code =StockLtr.Item_Code "
                    qry += " ) XXXFinal  "
                    qry += " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code "
                    qry += " )XXXXX  "
                    qry += " )XXXXXFinal "
                    qry += " where  2=2  "
                    qry += " )  XXXXXXFinal "
                    Dim dtCreateDetailsMorning As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dtCreateDetailsMorning IsNot Nothing And dtCreateDetailsMorning.Rows.Count > 0 Then
                        strAMDispatchCrateQty = clsCommon.myCstr(dtCreateDetailsMorning.Rows(0)("CrateQty"))
                    End If

                    qry = "  Select isnull (sum (XXXXXXFinal.CrateQty_New),0) as CrateQty from ( select '-' as Desh, XXXXXFinal.Document_Date,XXXXXFinal.Cust_Code, XXXXXFinal.Customer_Name as Customer_Name,  XXXXXFinal.route_no  as route_no ,  XXXXXFinal.Route_Desc  as Route_Desc,XXXXXFinal.Zone_Code,XXXXXFinal.Zone_Name,XXXXXFinal.Item_Code,SUBSTRING (XXXXXFinal.Alies_Name,0,8) as Alies_Name, Convert (integer,XXXXXFinal.CR) as CR,Convert (Integer ,XXXXXFinal.CD) as CD,convert (Integer,XXXXXFinal.SO) as SO,Convert (Integer ,XXXXXFinal.Cash,103) as Cash ,Convert (Integer,XXXXXFinal.Total) as Total,XXXXXFinal.CrateQty_New as CrateQty_New , XXXXXFinal.PendingPcsQty_New as PendingPcsQty_New, XXXXXFinal.Amount_with_Tax, XXXXXFinal.  DataSNO,   XXXXXFinal. Unit_code, XXXXXFinal.LtrQty from (  "
                    qry += " select  XXXXX.Document_Date, 'Route Total' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CEILING_Crate_Qty  as CrateQty_New , PendingPcsQty as PendingPcsQty_New,XXXXX.Amount_with_Tax, '3' as  DataSNO, '' as  Unit_code,  XXXXX.LtrQty from (    Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  from "
                    qry += " ( "
                    qry += " Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty ,   (case when coalesce(StockLtr.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockLtr.Conversion_Factor,1)) as numeric(18,3)) end) as LtrQty "
                    qry += " from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , "

                    qry += " max(XFinal.route_no ) as route_no "

                    qry += " , max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others') "
                    qry += " " + whrCreateDetailsEvening + " "
                    qry += "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by   "

                    qry += "  XFinal.Item_Code,Unit_code  ) XXFinal "
                    qry += " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  "
                    qry += " left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code "
                    qry += " left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='Ltr') as StockLtr on XXFinal.Item_Code =StockLtr.Item_Code "
                    qry += " ) XXXFinal  "
                    qry += " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code "
                    qry += " )XXXXX  "
                    qry += " )XXXXXFinal "
                    qry += " where  2=2  "
                    qry += " )  XXXXXXFinal "
                    Dim dtCreateDetailsEvening As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dtCreateDetailsEvening IsNot Nothing And dtCreateDetailsEvening.Rows.Count > 0 Then
                        strPMDispatchCrateQty = clsCommon.myCstr(dtCreateDetailsEvening.Rows(0)("CrateQty"))
                    End If

                    strAMPMTotalDistachCrateQty = strAMDispatchCrateQty + strPMDispatchCrateQty
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject(strLineWithDes, "------------------------------------------", "", "", " "))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject(clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFromDatePrvDay), "dd/MMM/yyyy")), "AM ", "PM", "Total ", ""))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Crates Dispatched", strAMDispatchCrateQty, strPMDispatchCrateQty, strAMPMTotalDistachCrateQty, ""))

                    '========================================== Crate Details End =====Received Crate Qty Start ================================
                    Dim strAMReceivedCrateQty As Double = 0
                    Dim strPMReceivedCrateQty As Double = 0
                    Dim strTotalReceivedCreateQty As Double = 0

                    qry = " select isnull (sum (TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CrateQtyRecd),0) as CrateQtyRecd  " &
                          " from TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE  " &
                          " Left Outer Join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE  on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No = TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Document_No " &
                          " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code " &
                          " where 2= 2 " + whrCreateReceivedMorning + ""
                    Dim dtCreateReceivedMorning As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dtCreateReceivedMorning IsNot Nothing And dtCreateReceivedMorning.Rows.Count > 0 Then
                        strAMReceivedCrateQty = clsCommon.myCstr(dtCreateReceivedMorning.Rows(0)("CrateQtyRecd"))
                    End If

                    qry = " select isnull (sum (TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CrateQtyRecd),0) as CrateQtyRecd  " &
                        " from TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE  " &
                        " Left Outer Join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE  on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No = TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Document_No " &
                        " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code " &
                        " where 2= 2 " + whrCreateReceivedEvening + ""
                    Dim dtCreateReceivedEvening As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dtCreateReceivedEvening IsNot Nothing And dtCreateReceivedEvening.Rows.Count > 0 Then
                        strPMReceivedCrateQty = clsCommon.myCstr(dtCreateReceivedEvening.Rows(0)("CrateQtyRecd"))
                    End If
                    strTotalReceivedCreateQty = strAMReceivedCrateQty + strPMReceivedCrateQty
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Crates Received  ", strAMReceivedCrateQty, strPMReceivedCrateQty, strTotalReceivedCreateQty, ""))
                    'obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("PM Crates Diff   ", "NA", strPMDispatchCrateQty - strPMReceivedCrateQty, "NA", ""))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject(strLineWithDes, "------------------------------------------", "", "", " "))
                    '===============================Received Crate Qty End========OutStanding Start======================================================================
                    Dim dtOutStandingDate As DateTime = dtpNSRFromDate.Value.AddDays(-2) 'New Date(TSP_Date.Value.Day, TSP_Date.Value.Day, -1)
                    Dim strOutStandingCratesQty As Decimal = 0
                    Dim strCreateOutStandingDetails As String = " With CTETemp as ( Select convert(varchar,Doc_Date,103) as Doc_Date, Route_No,Route_Desc,Vehicle_Code,Vehicle_Name,Customer_Code,Customer_Name,Customer_Phone, ZSM , ZSM_NAME,ZSM_Phone,ASM,ASM_Name,ASM_Phone,ASO , ASO_Name,ASO_Phone ,OpencrateQty, OpenJaaliQty, OpenBoxQty,OpenCanQty, CrateQtyRecd, JaaliQtyRecd, BoxQtyRecd ,CanQtyRecd,CrateOutQty, jaaliOutQty,boxOutQty  ,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty ,CanAdjQty,SUM(CrateQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as CrateQtyClosing,  SUM(JaaliQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as JaaliQtyClosing,  SUM(BoxQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as BoxQtyClosing, SUM(CanQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as CanQtyClosing , Row_Number() OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as RowNo from(  " &
                                                     " select  pp.Doc_Date  as Doc_Date,TSPL_CUSTOMER_MASTER.Route_No,TSPL_CUSTOMER_MASTER.Route_Desc,pp.Vehicle_Code,tspl_vehicle_master.Number as Vehicle_Name ,pp.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Phone1 as Customer_Phone ,   TSPL_CUSTOMER_MASTER.ZSM,TSPL_EMPLOYEE_MASTER_ZSM.Emp_Name as ZSM_NAME,case when len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PHONE,'')) > 0 And  len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO,'')) > 0  then '['+ TSPL_EMPLOYEE_MASTER_ZSM.PHONE + '],['  + TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO +']' when  len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PHONE,'')) > 0 then TSPL_EMPLOYEE_MASTER_ZSM.PHONE when len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO,'')) > 0 then TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO else '' end  as ZSM_Phone ,TSPL_CUSTOMER_MASTER.ASM, TSPL_EMPLOYEE_MASTER_ASM.Emp_Name as ASM_NAME, case when len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PHONE,'')) > 0 And  len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO,'')) > 0  then '['+ TSPL_EMPLOYEE_MASTER_ASM.PHONE + '],['  + TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO +']' when  len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PHONE,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASM.PHONE when len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO else '' end  as ASM_Phone,TSPL_CUSTOMER_MASTER.ASO,TSPL_EMPLOYEE_MASTER_ASO.Emp_Name as ASO_NAME,   case when len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PHONE,'')) > 0 And  len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO,'')) > 0  then '['+ TSPL_EMPLOYEE_MASTER_ASO.PHONE + '],['  + TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO +']' when  len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PHONE,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASO.PHONE when len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO else '' end  as ASO_Phone,pp.OpencrateQty as OpencrateQty,pp.OpenJaaliQty  as OpenJaaliQty,pp.OpenBoxQty  as OpenBoxQty,pp.OpenCanQty  as OpenCanQty,pp.CrateQtyRecd  as CrateQtyRecd,pp.JaaliQtyRecd  as JaaliQtyRecd,pp.BoxQtyRecd  as BoxQtyRecd ,pp.CanQtyRecd  as CanQtyRecd,pp.CrateOutQty  as CrateOutQty,pp.jaaliOutQty  as jaaliOutQty ,pp.boxOutQty  as boxOutQty,pp.CanOutQty  as CanOutQty ,pp.CrateQtyClosing as CrateQtyClosing, pp.JaaliQtyClosing as  JaaliQtyClosing, pp.BoxQtyClosing as BoxQtyClosing, pp.CanQtyClosing as CanQtyClosing,pp.CrateAdjQty , pp.JaaliAdjQty  , pp.BoxAdjQty , pp.CanAdjQty from ( " &
                                                     " select  max(convert(date,Doc_Date,103))  as Doc_Date,max(xx.Vehicle_Code) as Vehicle_Code,xx.Customer_Code,sum(xx.OpencrateQty) as OpencrateQty,sum(xx.OpenJaaliQty ) as OpenJaaliQty,sum(xx.OpenBoxQty )  as OpenBoxQty,sum(xx.OpenCanQty )  as OpenCanQty,sum(xx.CrateQtyRecd) as CrateQtyRecd,sum(xx.JaaliQtyRecd) as JaaliQtyRecd,sum(xx.BoxQtyRecd) as BoxQtyRecd,sum(xx.CanQtyRecd) as CanQtyRecd,sum(xx.CrateOutQty ) as CrateOutQty,sum(xx.jaaliOutQty ) as jaaliOutQty ,sum(xx.boxOutQty ) as boxOutQty,sum(xx.CanOutQty ) as CanOutQty, sum(xx.CrateAdjQty ) as CrateAdjQty ,sum(xx.JaaliAdjQty )as JaaliAdjQty  ,sum(xx.BoxAdjQty )  as BoxAdjQty ,sum(xx.CanAdjQty )  as CanAdjQty,(sum(xx.OpencrateQty)+sum(xx.CrateOutQty )-sum(xx.CrateQtyRecd)-sum(xx.CrateAdjQty )) as CrateQtyClosing, (sum(xx.OpenJaaliQty)+sum(xx.jaaliOutQty)-sum(xx.JaaliQtyRecd)-sum(xx.JaaliAdjQty )) as JaaliQtyClosing, (sum(xx.OpenBoxQty)+sum(xx.boxOutQty )-sum(xx.BoxQtyRecd)-sum(xx.BoxAdjQty )) as BoxQtyClosing  , (sum(xx.OpenCanQty)+sum(xx.CanOutQty )-sum(xx.CanQtyRecd)-sum(xx.CanAdjQty )) as CanQtyClosing  from (select  max(convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtOutStandingDate), "dd/MMM/yyyy") + "',103)) as Doc_Date,max(Opening.Vehicle_Code) as Vehicle_Code , Opening.Customer_Code ,sum(Opening.OpencrateQty*Type) as OpencrateQty,sum(Opening.OpenJaaliQty*Type ) as OpenJaaliQty,sum(Opening.OpenBoxQty *Type)  as OpenBoxQty,sum(Opening.OpenCanQty *Type)  as OpenCanQty,sum(Opening.CrateQtyRecd *Type) as CrateQtyRecd,sum(Opening.JaaliQtyRecd*Type ) as JaaliQtyRecd,sum(Opening.BoxQtyRecd*Type ) as BoxQtyRecd,sum(Opening.CanQtyRecd*Type ) as CanQtyRecd,sum(Opening.CrateOutQty*Type ) as CrateOutQty,sum(Opening.jaaliOutQty*Type ) as jaaliOutQty ,sum(Opening.boxOutQty*Type ) as boxOutQty,sum(Opening.CanOutQty*Type ) as CanOutQty,sum(Opening.CrateAdjQty*Type ) as CrateAdjQty,sum(Opening.JaaliAdjQty*Type ) as JaaliAdjQty,sum(Opening.BoxAdjQty *Type) as  BoxAdjQty ,sum(Opening.CanAdjQty *Type) as  canAdjQty from  ( select TSPL_SD_SHIPMENT_HEAD.Document_Date    as Document_Date,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) as Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.customer_code  ,1 as Type  ,'O' as Type1,TSPL_SD_SHIPMENT_HEAD.Crate as OpencrateQty,TSPL_SD_SHIPMENT_HEAD.jaali as OpenJaaliQty, TSPL_SD_SHIPMENT_HEAD.box  as OpenBoxQty , TSPL_SD_SHIPMENT_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS'  AND TSPL_SD_SHIPMENT_HEAD.Status =1  union all select TSPL_sd_SALE_RETURN_HEAD.Document_Date    as Document_Date,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.customer_code  ,-1 as Type  ,'I' as Type1,TSPL_sd_SALE_RETURN_HEAD.CrateQty as OpencrateQty,TSPL_sd_SALE_RETURN_HEAD.jaali as OpenJaaliQty, TSPL_sd_SALE_RETURN_HEAD.box  as OpenBoxQty , TSPL_sd_SALE_RETURN_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_sd_SALE_RETURN_HEAD  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS'  AND TSPL_sd_SALE_RETURN_HEAD.Status =1  union all  select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  , TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as OpencrateQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty   as OpenjaaliQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty  as OpenboxQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty as OpenCanQty , 0 as CrateQtyRecd,0 JaaliQtyRecd , 0 BoxQtyRecd , 0 CanQtyRecd , 0 as CrateOutQty, 0 jaaliOutQty, 0 boxoutqty, 0 Canoutqty, 0  as CrateAdjQty, 0  as JaaliAdjQty, 0  as BoxAdjQty, 0  as CanAdjQty from TSPL_CRATE_RECEIVED_detail_FRESHSALE left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  union all select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  , isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment,0)  as OpencrateQty, isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment,0)    as OpenjaaliQty , isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment,0)  as OpenboxQty, isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment,0)  as OpenCanQty, 0 as CrateQtyRecd,0 JaaliQtyRecd , 0 BoxQtyRecd , 0 CanQtyRecd , 0 as CrateOutQty, 0 jaaliOutQty, 0 boxoutqty, 0 Canoutqty, 0  as CrateAdjQty, 0  as JaaliAdjQty, 0  as BoxAdjQty, 0  as CanAdjQty from TSPL_CRATE_RECEIVED_detail_FRESHSALE left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1   )as Opening WHERE convert(date,Document_Date,103)<(convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtOutStandingDate), "dd/MMM/yyyy") + "',103)) group by Customer_Code  " &
                                                     " UNION All " &
                                                     " select Document_Date,Vehicle_Code,Customer_Code,0 as OpencrateQty,0 as OpenjaaliQty ,0 as OpenboxQty,0 as OpenCanQty,Case When [Type]=1 Then CrateQtyRecd Else 0 End as CrateQtyRecd,Case When [Type]=1 Then JaaliQtyRecd Else 0 End as JaaliQtyRecd,Case When [Type]=1 Then BoxQtyRecd Else 0 End as BoxQtyRecd,Case When [Type]=1 Then CANQtyRec Else 0 End as CANQtyRec, " &
                                                     " Case When [Type]=-1 Then CrateOutQty Else 0 End as CrateOutQty,Case When [Type]=-1 Then jaaliOutQty Else 0 End as jaaliOutQty,Case When [Type]=-1 Then boxOutQty Else 0 End as boxOutQty,Case When [Type]=-1 Then CanOutQty Else 0 End as CanOutQty,Case When [Type]=1 Then CrateAdjQty Else 0 End as CrateAdjQty,Case When [Type]=1 Then JaaliAdjQty Else 0 End as JaaliAdjQty,Case When [Type]=1 Then BoxAdjQty Else 0 End as BoxAdjQty,Case When [Type]=1 Then CANAdjustment Else 0 End as CANAdjustment  " &
                                                     " from ((select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCanQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE " &
                                                     " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  " &
                                                     " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1 ) union all  " &
                                                     " (select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCANQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE " &
                                                     " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  " &
                                                     " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  union all  " &
                                                     " select TSPL_SD_SHIPMENT_HEAD.Document_Date   as Document_Date,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) as Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.customer_code  ,-1 as Type  ,'O' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty, 0 as CrateQtyRecd, 0 JaaliQtyRecd ,  0  as BoxQtyRecd,0 CanQtyRecd  ,TSPL_SD_SHIPMENT_HEAD.Crate as CrateOutQty,jaali  as jaaliOutQty,TSPL_SD_SHIPMENT_HEAD.Box as boxOutQty,TSPL_SD_SHIPMENT_HEAD.ShippedCAN as CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty    from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS' AND TSPL_SD_SHIPMENT_HEAD.Status =1 union all  " &
                                                     " select TSPL_sd_SALE_RETURN_HEAD.Document_Date   as Document_Date,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.customer_code  ,1 as Type  ,'I' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty, TSPL_sd_SALE_RETURN_HEAD.CrateQty as CrateQtyRecd, TSPL_sd_SALE_RETURN_HEAD.jaali as JaaliQtyRecd ,  TSPL_sd_SALE_RETURN_HEAD.Box  as BoxQtyRecd,TSPL_sd_SALE_RETURN_HEAD.ShippedCAN CanQtyRecd  ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty,0 as CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty    from TSPL_sd_SALE_RETURN_HEAD  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS' AND TSPL_sd_SALE_RETURN_HEAD.Status =1 )  ) as Closing  " &
                                                     " WHERE convert(date,Document_Date ,103)>=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtOutStandingDate), "dd/MMM/yyyy") + "',103) AND convert(date,Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtOutStandingDate), "dd/MMM/yyyy") + "',103)  " &
                                                     " ) as xx where 2=2   " &
                                                     " GROUP BY Customer_Code " &
                                                     " ) as pp   left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =pp.Customer_Code  " &
                                                     " left outer Join  TSPL_EMPLOYEE_MASTER as TSPL_EMPLOYEE_MASTER_ZSM on TSPL_EMPLOYEE_MASTER_ZSM.EMP_CODE = TSPL_CUSTOMER_MASTER.ZSM  " &
                                                     " left outer Join  TSPL_EMPLOYEE_MASTER as TSPL_EMPLOYEE_MASTER_ASM on TSPL_EMPLOYEE_MASTER_ASM.EMP_CODE = TSPL_CUSTOMER_MASTER.ASM  " &
                                                     " left outer Join  TSPL_EMPLOYEE_MASTER as TSPL_EMPLOYEE_MASTER_ASO on TSPL_EMPLOYEE_MASTER_ASO.EMP_CODE = TSPL_CUSTOMER_MASTER.ASO  " &
                                                     " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=pp.vehicle_code where 2=2   " + whrOutStanding + " " &
                                                     " ) YYY )   "
                    qry = " " + strCreateOutStandingDetails + "  " &
                          "  select   isnull (sum (OpencrateQty),0) as OpencrateQty , isnull ( sum (CrateQtyRecd),0) as CrateQtyRecd , isnull (sum (CrateOutQty),0) as CrateOutQty ,isnull ( sum (CrateAdjQty),0) as CrateAdjQty , isnull (sum (CrateQtyClosing),0) as CrateQtyClosing ,  isnull (sum (CrateQtyRecd),0) - isnull (sum (CrateOutQty),0) as OutStandingCrateQty  from (   Select convert(varchar,Doc_Date,103) as Date,Route_No as [Route Code],Route_Desc as [Route Name], Vehicle_Code as [Vehicle Code],Vehicle_Name as [Vehicle Name], 'XCrates Route Details' as [Customer Code],'XCrates Route Details' as [Customer Name],Customer_Phone as [Customer Phone], ZSM as [ZSM Code], ZSM_Name as [ZSM Name],ZSM_Phone as [ZSM Phone],ASM as [ASM Code],ASM_NAME as [ASM Name],ASM_Phone as [ASM Phone],ASO as [ASO Code],ASO_Name as [ASO Name],ASO_Phone as [ASO Phone], OpencrateQty,OpenJaaliQty,OpenBoxQty,OpenCanQty, CrateQtyRecd, JaaliQtyRecd,BoxQtyRecd, CanQtyRecd  ,CrateOutQty, jaaliOutQty,boxOutQty,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty,CanAdjQty, OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty as CrateQtyClosing, OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty as CanQtyClosing , OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty as JaaliQtyClosing, OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty as BoxQtyClosing, (OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty)* 0 as CrateValueClosing, (OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty)* 0 as JaaliValueClosing, (OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty)* 0 as BoxValueClosing ,  (OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty)* 0 as CanValueClosing ,OpencrateQtySum,OpenCanQtySum ,OpencrateQtySumOfVehicleWise,OpencanQtySumOfVehicleWise  from (Select  case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySum, case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySum, case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySumOfVehicleWise, case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySumOfVehicleWise, CTETemp.RowNo,CTETemp.Doc_Date, CTETemp.Route_No,CTETemp.Route_Desc ,CTETemp.Vehicle_Code,CTETemp.Vehicle_Name ,CTETemp.Customer_Code,CTETemp.Customer_Name, CTETemp.Customer_Phone, CTETemp.ZSM,CTETemp.ZSM_NAME,CTETemp.ZSM_Phone,CTETemp.ASM,CTETemp.ASM_NAME,CTETemp.ASM_Phone,CTETemp.ASO,CTETemp.ASO_NAME,CTETemp.ASO_Phone, CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) as OpencrateQty,  CTETemp.OpenJaaliQty+ISNULL(CT1.JaaliQtyClosing,0) as OpenJaaliQty,  CTETemp.OpenBoxQty+ISNULL(CT1.BoxQtyClosing,0) as OpenBoxQty, CTETemp.OpenCanQty+ISNULL(CT1.CanQtyClosing,0) as OpenCanQty,  CTETemp.CrateQtyRecd, CTETemp.JaaliQtyRecd, CTETemp.BoxQtyRecd,CTETemp.CanQtyRecd, CTETemp.CrateOutQty, CTETemp.jaaliOutQty, CTETemp.boxOutQty, CTETemp.CanOutQty, CTETemp.CrateAdjQty , CTETemp.JaaliAdjQty  , CTETemp.BoxAdjQty, CTETemp.CanAdjQty  from CTETemp LEFt OUTER JOIN CTETemp CT1 ON  CT1.Customer_Code=CTETemp.Customer_Code and  CT1.Vehicle_Code = CTETemp.Vehicle_Code  and CT1.Route_No  = CTETemp.Route_No  AND (CTETemp.RowNo-CT1.RowNo)=1 ) ZZZ   ) XXXXX  "
                    Dim dtOutStanding As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dtOutStanding IsNot Nothing And dtOutStanding.Rows.Count > 0 Then
                        strOutStandingCratesQty = clsCommon.myCstr(dtOutStanding.Rows(0)("OutStandingCrateQty"))
                    End If
                    Dim strCurrentFromDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFromCurrentDate), "dd/MMM/yyyy")
                    Dim strOutStandingDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtOutStandingDate), "dd/MMM/yyyy")
                    Dim sss As String = "Date " + strCurrentFromDate + " OutStdCrates(" + strOutStandingDate + ") "
                    'obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject(sss, clsCommon.myCstr(strOutStandingCratesQty), "", "", ":"))
                    'obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Date : " + strCurrentFromDate + " OutStdCrates(" + strOutStandingDate + ") : ", strOutStandingCratesQty, "", "", " "))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("OutStdCrates ( " + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtOutStandingDate), "dd/MMM/yyyy")) + ")", strOutStandingCratesQty, "", "", ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("PM Crates Diff          ", strPMDispatchCrateQty - strPMReceivedCrateQty, "", "", ":"))
                    '==============================OutStanding End ==============================================================================
                    Dim strLRQty As Double = 0
                    Dim strPQQty As Double = 0
                    Dim strESQty As Double = 0
                    Dim strLRPQES_TotalQty As Double = 0

                    qry = " Select   1 as ID, isnull (sum(LR),0) as LR , isnull(sum (PQ),0) as PQ , isnull (Sum ( ES ),0) as ES ,isnull (sum(LR),0) + isnull(sum (PQ),0) + isnull (Sum ( ES ),0) as  Total_LR_PQ_ES_Qty from  " &
                          " ( " &
                          " Select TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_Code, convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 'LTR' as  Unit_code ,isnull( Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty),0) as LR , 0 as PQ , 0 as ES from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='LTR'  Left Outer Join TSPL_CUSTOMER_COMPLAINT_HEAD on TSPL_CUSTOMER_COMPLAINT_HEAD.complaint_no = TSPL_SD_SHIPMENT_HEAD.customer_complaint_no left outer join TSPL_CUSTOMER_COMPLAINT_detail on TSPL_CUSTOMER_COMPLAINT_detail.Complaint_no=TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_no and  TSPL_CUSTOMER_COMPLAINT_detail.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code  where   Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MMM/yyyy") + "',103) and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MMM/yyyy") + "',103)    and TSPL_SD_SHIPMENT_HEAD.status = 1     and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1 and TSPL_CUSTOMER_COMPLAINT_detail.Complaint_Code = 'LR' " + whrLR_PQ_ES + " " &
                          " Union All " &
                          " Select TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_Code, convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 'LTR' as  Unit_code ,0 as LR , isnull( Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty),0) as PQ , 0 as ES from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='LTR'  Left Outer Join TSPL_CUSTOMER_COMPLAINT_HEAD on TSPL_CUSTOMER_COMPLAINT_HEAD.complaint_no = TSPL_SD_SHIPMENT_HEAD.customer_complaint_no left outer join TSPL_CUSTOMER_COMPLAINT_detail on TSPL_CUSTOMER_COMPLAINT_detail.Complaint_no=TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_no and  TSPL_CUSTOMER_COMPLAINT_detail.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code  where   Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MMM/yyyy") + "',103) and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MMM/yyyy") + "',103)    and TSPL_SD_SHIPMENT_HEAD.status = 1     and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1 and TSPL_CUSTOMER_COMPLAINT_detail.Complaint_Code = 'PQ' " + whrLR_PQ_ES + " " &
                          " Union All " &
                          " Select TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_Code, convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 'LTR' as  Unit_code ,0 as LR , 0 as PQ , isnull( Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty),0) as ES from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='LTR'  Left Outer Join TSPL_CUSTOMER_COMPLAINT_HEAD on TSPL_CUSTOMER_COMPLAINT_HEAD.complaint_no = TSPL_SD_SHIPMENT_HEAD.customer_complaint_no left outer join TSPL_CUSTOMER_COMPLAINT_detail on TSPL_CUSTOMER_COMPLAINT_detail.Complaint_no=TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_no and  TSPL_CUSTOMER_COMPLAINT_detail.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code  where    Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MMM/yyyy") + "',103) and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MMM/yyyy") + "',103)   and TSPL_SD_SHIPMENT_HEAD.status = 1     and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1 and TSPL_CUSTOMER_COMPLAINT_detail.Complaint_Code = 'ES' " + whrLR_PQ_ES + " " &
                          " )XXXX1 "
                    Dim dtLRPQES As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dtLRPQES IsNot Nothing And dtLRPQES.Rows.Count > 0 Then
                        strLRQty = clsCommon.myCstr(dtLRPQES.Rows(0)("LR"))
                        strPQQty = clsCommon.myCstr(dtLRPQES.Rows(0)("PQ"))
                        strESQty = clsCommon.myCstr(dtLRPQES.Rows(0)("ES"))
                        strLRPQES_TotalQty = clsCommon.myCstr(dtLRPQES.Rows(0)("Total_LR_PQ_ES_Qty"))
                    End If


                    '=========================== LQ/ES/PQ End =======CR CD CASH SO Start==================================================
                    Dim strCRQty As String = 0
                    Dim strCDQty As String = 0
                    Dim strSOQty As String = 0
                    Dim strCashQty As String = 0

                    qry = " select isnull (sum (Final.CR),0) as CR, isnull (sum(Final.CD),0) as CD,isnull (sum(Final.SO),0) as SO,isnull (sum(FINAL.Cash),0) as Cash from ( " &
                          " select '-' as Desh, XXXXXFinal.Document_Date,XXXXXFinal.Cust_Code, XXXXXFinal.Customer_Name as Customer_Name,  XXXXXFinal.route_no  as route_no ,  XXXXXFinal.Route_Desc  as Route_Desc,XXXXXFinal.Zone_Code,XXXXXFinal.Zone_Name,XXXXXFinal.Item_Code,SUBSTRING (XXXXXFinal.Alies_Name,0,8) as Alies_Name, Convert (integer,XXXXXFinal.CR) as CR,Convert (Integer ,XXXXXFinal.CD) as CD,convert (Integer,XXXXXFinal.SO) as SO,Convert (Integer ,XXXXXFinal.Cash,103) as Cash ,Convert (Integer,XXXXXFinal.Total) as Total,XXXXXFinal.CrateQty_New as CrateQty_New , XXXXXFinal.PendingPcsQty_New as PendingPcsQty_New, XXXXXFinal.Amount_with_Tax, XXXXXFinal.  DataSNO,   XXXXXFinal. Unit_code, XXXXXFinal.LtrQty from (   select  XXXXX.Document_Date, 'Route Total' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CEILING_Crate_Qty  as CrateQty_New , PendingPcsQty as PendingPcsQty_New,XXXXX.Amount_with_Tax, '3' as  DataSNO, '' as  Unit_code,  XXXXX.LtrQty from (    Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  from  (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty ,   (case when coalesce(StockLtr.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockLtr.Conversion_Factor,1)) as numeric(18,3)) end) as LtrQty  from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name ,  max(XFinal.route_no ) as route_no  , max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  " + strCR_CD_SO_CASH_whre + " )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by     XFinal.Item_Code,Unit_code  ) XXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code   left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='Ltr') as StockLtr on XXFinal.Item_Code =StockLtr.Item_Code  ) XXXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )XXXXX   )XXXXXFinal  where  2=2 " &
                          " ) Final"
                    Dim dtCR_CD_CASH_SO As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dtCR_CD_CASH_SO IsNot Nothing And dtCR_CD_CASH_SO.Rows.Count > 0 Then
                        strCRQty = clsCommon.myCstr(dtCR_CD_CASH_SO.Rows(0)("CR"))
                        strCDQty = clsCommon.myCstr(dtCR_CD_CASH_SO.Rows(0)("CD"))
                        strSOQty = clsCommon.myCstr(dtCR_CD_CASH_SO.Rows(0)("SO"))
                        strCashQty = clsCommon.myCstr(dtCR_CD_CASH_SO.Rows(0)("Cash"))
                    End If
                    '==================================================================================================
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject(strLineWithDes, "------------------------------------------", "", "", " "))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Credit", strCRQty, "Leakage Replacement", strLRQty, ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Cards", strCDQty, "Poor Quality Replacement", strPQQty, ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Spl Order", strSOQty, "Market Returns Replacement", strESQty, ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Cash", strCashQty, "Total Leakages", strLRPQES_TotalQty, ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("EX.Factory District Sales", valueSo, "", "", ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject(strLineWithDes, "------------------------------------------", "", "", " "))

                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject(" ", "", "", " ", ""))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject(" ", "", "", " ", ""))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject(" ", "", "", " ", ""))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Shift Incharge", "", "Dairy Manager(Dispatch)", "", ""))


                Else
                    obj.arrReportFooter = New List(Of clsDosPrintReportFooter)
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Leakage Replacement      ", clsCommon.myCstr(dt.Rows(0)("LR")), "", "", ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Poor Quality Replacmt    ", clsCommon.myCstr(dt.Rows(0)("PQ")), "", "", ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Market Returns Replacmt  ", clsCommon.myCstr(dt.Rows(0)("ES")), "", "", ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("TOTAL                    ", clsCommon.myCstr(dt.Rows(0)("Total_LR_PQ_ES_Qty")), "", "", ":"))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject(" ", "", "", " ", ""))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject(" ", "", "", " ", ""))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject(" ", "", "", " ", ""))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Dispatch Supervisor", "", "Dairy Manager(Dispatch)", "", ""))
                End If
                obj.Print(obj, dt, PageSetup.Potrate)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    Private Sub cboNSRType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboNSRType.SelectedValueChanged
        If clsCommon.CompairString(cboNSRType.Text, "Daywise Net Sales") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboNSRType.Text, "Milk Daywise Net Sales") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboNSRType.Text, "Product Daywise Net Sales") = CompairStringResult.Equal Then
            dtpNSRToDate.Visible = False
            MyLabel13.Visible = False
            MyLabel17.Text = "Date"
        Else
            dtpNSRToDate.Visible = True
            MyLabel13.Visible = True
            MyLabel17.Text = "From Date"
        End If
    End Sub

    Private Sub TSP_Date_ValueChanged(sender As Object, e As EventArgs) Handles TSP_Date.ValueChanged
        Try
            Dim TempTruckSheetGenerate As Integer = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT isnull(max(TruckSheetGenerate),0) as aa FROM TSPL_BOOKING_MATSER Where Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)"))
            If TempTruckSheetGenerate = 1 Then
                btn_TruckSheetGenerated.Enabled = False
                btn_CancelTruckSheet.Enabled = True
            Else
                btn_TruckSheetGenerated.Enabled = True
                btn_CancelTruckSheet.Enabled = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnIDSPrint_Click(sender As Object, e As EventArgs) Handles btnIDSPrint.Click
        Try
            Dim dtFrom As DateTime = dtpIDSfromdate.Value
            Dim dtTo As DateTime = dtpIDStodate.Value
            'If clsCommon.myLen(txtIDSItem.Value) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please Select Item First")
            '    Return
            'End If
            If clsCommon.myLen(txtIDSCustomer.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Customer First", Me.Text)
                Return
            End If
            'Dim Qry As String = "  Select TBL_Source.thedate, XXXFinal.Item_Selling_Price, isnull( XXXFinal.Booking_Qty,0) as Booking_Qty ,isnull (XXXFinal.Amount_with_Tax,0) as Amount_with_Tax, XXXFinal.Cust_Code, XXXFinal.customer_Name, XXXFinal.OldName, XXXFinal.Zone_Code, XXXFinal.Zone_Name from ( " & _
            '                    "  select  convert (varchar,thedate,103) as thedate, DATEPART (day,thedate) as [Day], '1' as  Code  from ExplodeDates (convert (date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy") + "',103) , Convert (date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MMM/yyyy") + "',103)) TBL_DATE ) TBL_Source  " & _
            '                    "  Left Outer Join " & _
            '                    "  ( " & _
            '                    "  Select  max(Code) as Code ,XXXFirst.Document_Date ,max(XXXFirst.Item_Selling_Price) as Item_Selling_Price , sum (Booking_Qty) as Booking_Qty ,sum (Amount_with_Tax) as Amount_with_Tax , max (Cust_Code) as Cust_Code, max (Customer_Name) as Customer_Name,max( OldName) as OldName,max(Zone_code) as Zone_code, max(Zone_Name) as Zone_Name  from ( " & _
            '                    "  Select '1' as  Code, TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.OldName, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Item_Selling_Price, Convert (decimal(18,2), (isnull(Source_UOM.Conversion_Factor,0) /NULLIF (Target_UOM.Conversion_Factor,0)) *  isnull (TSPL_BOOKING_DETAIL.Booking_Qty,0) ) as Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  " & _
            '                    "  Left outer Join TSPL_ITEM_UOM_DETAIL as Source_UOM on Source_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Source_UOM.UOM_Code = TSPL_BOOKING_DETAIL.Unit_code  " & _
            '                    "   Left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM on Target_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_UOM.UOM_Code = 'LTR' " & _
            '                    "  where Scheme_Item = 'N'           and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY  in ('Institution CR','Institution SO') " & _
            '                    " and  TSPL_BOOKING_DETAIL.Cust_Code = '" + txtIDSCustomer.Value + "' and TSPL_BOOKING_DETAIL.Item_Code ='" + txtIDSItem.Value + "'  and Convert(date, Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy") + "',103)  and Convert(date, Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MMM/yyyy") + "',103) ) XXXFirst Group By XXXFirst.Document_Date " & _
            '                    " ) XXXFinal on XXXFinal.Code = TBL_Source.Code " & _
            '                    " and XXXFinal.Document_Date= TBL_Source.thedate "

            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            'If dt IsNot Nothing And dt.Rows.Count > 0 Then

            '    Dim obj As clsDosPrint = New clsDosPrint()
            '    obj.ReportName = objCommonVar.CurrentCompanyName
            '    ' clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy")

            '    Dim strDateRange As String = clsCommon.myCstr(clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MM/yyyy")) + " To " + clsCommon.myCstr(clsCommon.GetPrintDate(dtpNSRToDate.Value, "dd/MM/yyyy"))
            '    obj.ReportName1 = "Spl.Order supply to institution for the month of " + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "MMM/yyyy")) + " "


            '    obj.ShowPageNo = True
            '    obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
            '    Dim strZone As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Zone_Code  from TSPL_Customer_MAster where Cust_code = '" + txtIDSCustomer.Value + "'"))
            '    Dim strOldName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select OldName  from TSPL_Customer_MAster where Cust_code = '" + txtIDSCustomer.Value + "'"))
            '    Dim strcustomerName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_Name  from TSPL_Customer_MAster where Cust_code = '" + txtIDSCustomer.Value + "'"))
            '    'Dim strPrice As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select max(Item_Selling_Price) from TSPL_BOOKING_DETAIL left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No where  Scheme_Item = 'N' and TSPL_BOOKING_DETAIL.Cust_Code = '" + txtIDSCustomer.Value + "' and TSPL_BOOKING_DETAIL.Item_Code ='" + txtIDSItem.Value + "'  and Convert(date, Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy") + "',103)  and Convert(date, Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MMM/yyyy") + "',103)  "))
            '    Dim strPrice As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select max(isnull ( Convert ( decimal(18,2), TSPL_BOOKING_DETAIL.Amount_with_Tax/ Convert (decimal(18,2), (isnull(Source_UOM.Conversion_Factor,0) /NULLIF (Target_UOM.Conversion_Factor,0)) *  isnull (TSPL_BOOKING_DETAIL.Booking_Qty,0) ) ),0)) from TSPL_BOOKING_DETAIL left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  Left outer Join TSPL_ITEM_UOM_DETAIL as Source_UOM on Source_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Source_UOM.UOM_Code = TSPL_BOOKING_DETAIL.Unit_code  Left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM on Target_UOM.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_UOM.UOM_Code = 'LTR'    where  Scheme_Item = 'N' and TSPL_BOOKING_DETAIL.Cust_Code = '" + txtIDSCustomer.Value + "' and TSPL_BOOKING_DETAIL.Item_Code ='" + txtIDSItem.Value + "'  and Convert(date, Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy") + "',103)  and Convert(date, Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MMM/yyyy") + "',103)  "))

            '    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Zone", clsCommon.myCstr(dt.Rows(0)("Zone_Name"))))
            '    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("BoothNo", clsCommon.myCstr(txtIDSCustomer.Value)))
            '    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Booth Name", strOldName))
            '    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Agent Name", strcustomerName))
            '    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Item", clsCommon.myCstr(txtIDSItem.Value))) '
            '    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Price", strPrice))

            '    obj.arrColumn = New List(Of clsDosPrintColumn)()
            '    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("thedate", "Day", True, DosPrintAlignment.Left, 20, False, DecimalPlaces.NA))
            '    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Booking_Qty", "Qty", False, DosPrintAlignment.Right, 40, True, DecimalPlaces.Two))
            '    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Amount_with_Tax", "Total Amount", False, DosPrintAlignment.Right, 40, True, DecimalPlaces.Two))

            '    obj.Print(obj, dt, PageSetup.Potrate)
            'Else
            '    clsCommon.MyMessageBoxShow("No Data Found")
            'End If

            Dim ItemInUse As String = " TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code    where Scheme_Item = 'N'   " &  'and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY  in ('Institution CR','Institution SO')
           " and  TSPL_BOOKING_DETAIL.Cust_Code = '" + txtIDSCustomer.Value + "' and Convert(date, Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy") + "',103)  and Convert(date, Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MMM/yyyy") + "',103) "
            If clsCommon.myLen(txtIDSItem.Value) > 0 Then
                ItemInUse += " and TSPL_BOOKING_DETAIL.Item_Code ='" + txtIDSItem.Value + "'"
            End If

            Dim dtDataExist As DataTable = clsDBFuncationality.GetDataTable("select isnull(TSPL_ITEM_MASTER.Alies_Name,'')  Alies_Name from " + ItemInUse)
            If dtDataExist Is Nothing OrElse dtDataExist.Rows.Count = 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                Exit Sub
            End If

            Dim strItem1 As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) +',0))' +' as ' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strItem2 As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")


            Dim Qry As String = "select  thedate, Cust_Code, customer_Name, OldName, Zone_Code, Zone_Name,  " + strItem1 + ",sum(isnull(Amount_with_Tax,0)) as Amount_with_Tax from (" &
                                "  Select XXXFinal.Alies_Name,TBL_Source.thedate, XXXFinal.Item_Selling_Price, isnull( XXXFinal.Booking_Qty,0) as Booking_Qty ,isnull (XXXFinal.Amount_with_Tax,0) as Amount_with_Tax, XXXFinal.Cust_Code, XXXFinal.customer_Name, XXXFinal.OldName, XXXFinal.Zone_Code, XXXFinal.Zone_Name from ( " &
                                "  select  convert (varchar,thedate,103) as thedate, DATEPART (day,thedate) as [Day], '1' as  Code  from ExplodeDates (convert (date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy") + "',103) , Convert (date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MMM/yyyy") + "',103)) TBL_DATE ) TBL_Source  " &
                                "  Left Outer Join " &
                                "  ( " &
                                "  Select  Alies_Name,max(Code) as Code ,XXXFirst.Document_Date ,max(XXXFirst.Item_Selling_Price) as Item_Selling_Price , sum (Booking_Qty) as Booking_Qty ,sum (Amount_with_Tax) as Amount_with_Tax , max (Cust_Code) as Cust_Code, max (Customer_Name) as Customer_Name,max( OldName) as OldName,max(Zone_code) as Zone_code, max(Zone_Name) as Zone_Name  from ( " &
                                "  Select '1' as  Code, TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.OldName, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Item_Selling_Price,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code    where Scheme_Item = 'N'   " &  '        and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY  in ('Institution CR','Institution SO') 
                                " and  TSPL_BOOKING_DETAIL.Cust_Code = '" + txtIDSCustomer.Value + "' and Convert(date, Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy") + "',103)  and Convert(date, Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MMM/yyyy") + "',103) "

            If clsCommon.myLen(txtIDSItem.Value) > 0 Then
                Qry += " and TSPL_BOOKING_DETAIL.Item_Code ='" + txtIDSItem.Value + "'"
            End If
            Qry += " ) XXXFirst Group By XXXFirst.Document_Date,XXXFirst.Alies_Name "
            Qry += " ) XXXFinal on XXXFinal.Code = TBL_Source.Code " &
                   " and XXXFinal.Document_Date= TBL_Source.thedate "

            Qry += ") as s pivot (  sum(Booking_Qty) for Alies_Name in ( " + strItem2 + " ) ) as zpivot group by thedate, Cust_Code, customer_Name, OldName, Zone_Code, Zone_Name "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then

                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = objCommonVar.CurrentCompanyName

                Dim strDateRange As String = clsCommon.myCstr(clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MM/yyyy")) + " To " + clsCommon.myCstr(clsCommon.GetPrintDate(dtpNSRToDate.Value, "dd/MM/yyyy"))
                'obj.ReportName1 = "Spl.Order supply to institution for the month of " + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "MMM/yyyy")) + " "
                obj.ReportName1 = "Spl.Order supply From " + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MM/yyyy")) + " To" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MM/yyyy"))

                obj.ShowPageNo = True
                obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
                Dim strZone As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Zone_Code  from TSPL_Customer_MAster where Cust_code = '" + txtIDSCustomer.Value + "'"))
                Dim strOldName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select OldName  from TSPL_Customer_MAster where Cust_code = '" + txtIDSCustomer.Value + "'"))
                Dim strcustomerName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_Name  from TSPL_Customer_MAster where Cust_code = '" + txtIDSCustomer.Value + "'"))
                Dim strPrice As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select max(Item_Selling_Price) from TSPL_BOOKING_DETAIL left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No where  Scheme_Item = 'N' and TSPL_BOOKING_DETAIL.Cust_Code = '" + txtIDSCustomer.Value + "' and TSPL_BOOKING_DETAIL.Item_Code ='" + txtIDSItem.Value + "'  and Convert(date, Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy") + "',103)  and Convert(date, Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MMM/yyyy") + "',103)  "))

                Dim QryItem As String = "  Select TSPL_ITEM_MASTER.Alies_Name as Item,max(TSPL_BOOKING_DETAIL.Item_Selling_Price) as Price from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code    where Scheme_Item = 'N'  " &  'and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY  in ('Institution CR','Institution SO') 
                                " and  TSPL_BOOKING_DETAIL.Cust_Code = '" + txtIDSCustomer.Value + "'  and Convert(date, Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy") + "',103)  and Convert(date, Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MMM/yyyy") + "',103)"
                If clsCommon.myLen(txtIDSItem.Value) > 0 Then
                    QryItem += " and TSPL_BOOKING_DETAIL.Item_Code ='" + txtIDSItem.Value + "'"
                End If
                QryItem += " group by TSPL_ITEM_MASTER.Alies_Name "

                Dim dtItem As DataTable = clsDBFuncationality.GetDataTable(QryItem)
                Dim TempItemCount As Integer = dtItem.Rows.Count
                Dim TempColWidth As Integer = Math.Ceiling(65 / TempItemCount)
                Dim TempReportWidth As Integer = 0
                If TempItemCount <= 5 Then
                    TempReportWidth = 80
                Else
                    TempReportWidth = 80 + (TempItemCount - 5) * 10
                End If
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Zone", clsCommon.myCstr(dt.Rows(0)("Zone_Name"))))
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("BoothNo", clsCommon.myCstr(txtIDSCustomer.Value)))
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Booth Name", strOldName))
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Agent Name", strcustomerName))


                obj.arrColumn = New List(Of clsDosPrintColumn)()
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("thedate", "Rate" + IIf(TempItemCount <= 5, "         ", "          ") + "Day", True, DosPrintAlignment.Left, IIf(TempItemCount <= 5, 15, Math.Floor((80 / TempReportWidth) * 15)), False, DecimalPlaces.NA))

                If TempItemCount <= 5 Then
                    Dim TempSpace As Integer = clsCommon.myCdbl(80 * TempColWidth) / 100
                    For i As Integer = 0 To dtItem.Rows.Count - 1
                        Dim TempHead As String = clsCommon.myCstr(dtItem.Rows(i).Item("Price"))
                        For j As Integer = 1 To TempSpace - clsCommon.myLen(dtItem.Rows(i).Item("Price"))
                            TempHead += " "
                        Next
                        TempHead += clsCommon.myCstr(dtItem.Rows(i).Item("Item"))
                        obj.arrColumn.Add(clsDosPrintColumn.SetColumn(clsCommon.myCstr(dtItem.Rows(i).Item("Item")), TempHead, False, DosPrintAlignment.Left, TempColWidth, True, DecimalPlaces.Two))
                    Next
                Else
                    For i As Integer = 0 To dtItem.Rows.Count - 1
                        Dim TempSpace As Integer = 10
                        Dim TempHead As String = clsCommon.myCstr(dtItem.Rows(i).Item("Price"))
                        For j As Integer = 1 To TempSpace - clsCommon.myLen(dtItem.Rows(i).Item("Price")) - 1
                            TempHead += " "
                        Next
                        TempHead += clsCommon.myCstr(dtItem.Rows(i).Item("Item"))
                        obj.arrColumn.Add(clsDosPrintColumn.SetColumn(clsCommon.myCstr(dtItem.Rows(i).Item("Item")), TempHead, False, DosPrintAlignment.Left, Math.Floor((80 / TempReportWidth) * 13), True, DecimalPlaces.Two))
                    Next
                End If

                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Amount_with_Tax", "Total Amount", False, DosPrintAlignment.Right, IIf(TempItemCount <= 5, 20, Math.Floor((80 / TempReportWidth) * 20)), True, DecimalPlaces.Two))

                '-------------------------------Footer
                Dim BaseQry As String = clsCustomerMaster.GetCustomerBaseQry(False, False, "", False, " ConvRate", "'" & txtIDSCustomer.Value & "'", False, clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MMM/yyyy"), False, False, False)
                Dim TempDT As DataTable = clsDBFuncationality.GetDataTable(BaseQry)
                Dim TempSales As Decimal = 0
                Dim TempReciepts As Decimal = 0
                If TempDT.Rows.Count > 0 Then
                    TempSales = Convert.ToDecimal(TempDT.Compute("SUM(DrAmt)", String.Empty))
                    TempReciepts = Convert.ToDecimal(TempDT.Compute("SUM(CrAmt)", String.Empty))
                End If

                Dim BaseQryOPENINGINCASEOFMIS As String = clsCustomerMaster.GetCustomerBaseQry(False, False, "", False, " ConvRate", "'" & txtIDSCustomer.Value & "'", True, clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MMM/yyyy"), False, False, False)
                Dim TempDTOpening As DataTable = clsDBFuncationality.GetDataTable(BaseQryOPENINGINCASEOFMIS)
                Dim TempOpening As Decimal = 0
                If TempDTOpening.Rows.Count > 0 Then
                    TempOpening = clsCommon.myCdbl(Convert.ToDecimal(TempDTOpening.Compute("sum(DrAmt)", String.Empty))) - clsCommon.myCdbl(Convert.ToDecimal(TempDTOpening.Compute("sum(CrAmt)", String.Empty)))
                End If


                Dim TempOutStanding As Decimal = TempOpening + TempSales - TempReciepts

                obj.arrReportFooter = New List(Of clsDosPrintReportFooter)()
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Opening Balance (Excess)", TempOpening, "", "", ":"))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Total Sale", TempSales, "", "", ":"))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Total Reciepts", TempReciepts, "", "", ":"))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Outstanding Amt", TempOutStanding, "", "", ":"))

                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("This is to certify that the quantities", "", "This is to certify that the quantities and", "", ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("are verified and found correct", "", "sales proceeds are verified and found correct", "", ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("DAIRY MANAGER(DES)", "", "RSM(LMS)", "", ""))

                '---------------------------------Footer

                If TempItemCount <= 5 Then
                    obj.Print(obj, dt, PageSetup.Potrate)
                Else
                    obj.LandscapPageSetupColumnsChar = TempReportWidth
                    obj.Print(obj, dt, PageSetup.Landscap)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub BtnIDSPrintABS_Click(sender As Object, e As EventArgs) Handles btnIDSPrintABS.Click
        Try
            Dim dtFrom As DateTime = dtpIDSfromdate.Value
            Dim dtTo As DateTime = dtpIDStodate.Value

            If clsCommon.myLen(txtIDSCustomer.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Customer First", Me.Text)
                Return
            End If

            Dim ItemInUse As String = " TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code    where Scheme_Item = 'N'   " &  'and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY  in ('Institution CR','Institution SO')
           " and  TSPL_BOOKING_DETAIL.Cust_Code = '" + txtIDSCustomer.Value + "' and Convert(date, Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy") + "',103)  and Convert(date, Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MMM/yyyy") + "',103) "
            If clsCommon.myLen(txtIDSItem.Value) > 0 Then
                ItemInUse += " and TSPL_BOOKING_DETAIL.Item_Code ='" + txtIDSItem.Value + "'"
            End If

            Dim dtDataExist As DataTable = clsDBFuncationality.GetDataTable("select isnull(TSPL_ITEM_MASTER.Alies_Name,'')  Alies_Name from " + ItemInUse)
            If dtDataExist Is Nothing OrElse dtDataExist.Rows.Count = 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                Exit Sub
            End If

            Dim strItem1 As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) +',0))' +' as ' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strItem2 As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")


            Dim Qry As String = "select  'Total' as [Total], Cust_Code, customer_Name, OldName, Zone_Code, Zone_Name,  " + strItem1 + ",sum(isnull(Amount_with_Tax,0)) as Amount_with_Tax from (" &
                                "  Select XXXFinal.Alies_Name,TBL_Source.thedate, XXXFinal.Item_Selling_Price, isnull( XXXFinal.Booking_Qty,0) as Booking_Qty ,isnull (XXXFinal.Amount_with_Tax,0) as Amount_with_Tax, XXXFinal.Cust_Code, XXXFinal.customer_Name, XXXFinal.OldName, XXXFinal.Zone_Code, XXXFinal.Zone_Name from ( " &
                                "  select  convert (varchar,thedate,103) as thedate, DATEPART (day,thedate) as [Day], '1' as  Code  from ExplodeDates (convert (date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy") + "',103) , Convert (date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MMM/yyyy") + "',103)) TBL_DATE ) TBL_Source  " &
                                "  Left Outer Join " &
                                "  ( " &
                                "  Select  Alies_Name,max(Code) as Code ,XXXFirst.Document_Date ,max(XXXFirst.Item_Selling_Price) as Item_Selling_Price , sum (Booking_Qty) as Booking_Qty ,sum (Amount_with_Tax) as Amount_with_Tax , max (Cust_Code) as Cust_Code, max (Customer_Name) as Customer_Name,max( OldName) as OldName,max(Zone_code) as Zone_code, max(Zone_Name) as Zone_Name  from ( " &
                                "  Select '1' as  Code, TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.OldName, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Item_Selling_Price,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code    where Scheme_Item = 'N'   " &  '        and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY  in ('Institution CR','Institution SO') 
                                " and  TSPL_BOOKING_DETAIL.Cust_Code = '" + txtIDSCustomer.Value + "' and Convert(date, Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy") + "',103)  and Convert(date, Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MMM/yyyy") + "',103) "

            If clsCommon.myLen(txtIDSItem.Value) > 0 Then
                Qry += " and TSPL_BOOKING_DETAIL.Item_Code ='" + txtIDSItem.Value + "'"
            End If
            Qry += " ) XXXFirst Group By XXXFirst.Document_Date,XXXFirst.Alies_Name "
            Qry += " ) XXXFinal on XXXFinal.Code = TBL_Source.Code " &
                   " and XXXFinal.Document_Date= TBL_Source.thedate "

            Qry += ") as s pivot (  sum(Booking_Qty) for Alies_Name in ( " + strItem2 + " ) ) as zpivot group by Cust_Code, customer_Name, OldName, Zone_Code, Zone_Name "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then

                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = objCommonVar.CurrentCompanyName

                Dim strDateRange As String = clsCommon.myCstr(clsCommon.GetPrintDate(dtpNSRFromDate.Value, "dd/MM/yyyy")) + " To " + clsCommon.myCstr(clsCommon.GetPrintDate(dtpNSRToDate.Value, "dd/MM/yyyy"))
                'obj.ReportName1 = "Spl.Order supply to institution for the month of " + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "MMM/yyyy")) + " "
                obj.ReportName1 = "Spl.Order supply From " + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MM/yyyy")) + " To" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MM/yyyy"))

                obj.ShowPageNo = True
                obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
                Dim strZone As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Zone_Code  from TSPL_Customer_MAster where Cust_code = '" + txtIDSCustomer.Value + "'"))
                Dim strOldName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select OldName  from TSPL_Customer_MAster where Cust_code = '" + txtIDSCustomer.Value + "'"))
                Dim strcustomerName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_Name  from TSPL_Customer_MAster where Cust_code = '" + txtIDSCustomer.Value + "'"))
                Dim strPrice As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select max(Item_Selling_Price) from TSPL_BOOKING_DETAIL left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No where  Scheme_Item = 'N' and TSPL_BOOKING_DETAIL.Cust_Code = '" + txtIDSCustomer.Value + "' and TSPL_BOOKING_DETAIL.Item_Code ='" + txtIDSItem.Value + "'  and Convert(date, Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy") + "',103)  and Convert(date, Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MMM/yyyy") + "',103)  "))

                Dim QryItem As String = "  Select TSPL_ITEM_MASTER.Alies_Name as Item,max(TSPL_BOOKING_DETAIL.Item_Selling_Price) as Price from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code    where Scheme_Item = 'N'  " &  'and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY  in ('Institution CR','Institution SO') 
                                " and  TSPL_BOOKING_DETAIL.Cust_Code = '" + txtIDSCustomer.Value + "'  and Convert(date, Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy") + "',103)  and Convert(date, Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MMM/yyyy") + "',103)"
                If clsCommon.myLen(txtIDSItem.Value) > 0 Then
                    QryItem += " and TSPL_BOOKING_DETAIL.Item_Code ='" + txtIDSItem.Value + "'"
                End If
                QryItem += " group by TSPL_ITEM_MASTER.Alies_Name "

                Dim dtItem As DataTable = clsDBFuncationality.GetDataTable(QryItem)
                Dim TempItemCount As Integer = dtItem.Rows.Count
                Dim TempColWidth As Integer = Math.Ceiling(65 / TempItemCount)
                Dim TempReportWidth As Integer = 0
                If TempItemCount <= 5 Then
                    TempReportWidth = 80
                Else
                    TempReportWidth = 80 + (TempItemCount - 5) * 10
                End If
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Zone", clsCommon.myCstr(dt.Rows(0)("Zone_Name"))))
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("BoothNo", clsCommon.myCstr(txtIDSCustomer.Value)))
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Booth Name", strOldName))
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Agent Name", strcustomerName))


                obj.arrColumn = New List(Of clsDosPrintColumn)()
                'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total", "Rate" + "            ", True, DosPrintAlignment.Left, 15, False, DecimalPlaces.NA))

                'Dim TempSpace As Integer = clsCommon.myCdbl(80 * TempColWidth) / 100
                'For i As Integer = 0 To dtItem.Rows.Count - 1
                '    Dim TempHead As String = clsCommon.myCstr(dtItem.Rows(i).Item("Price"))
                '    For j As Integer = 1 To TempSpace - clsCommon.myLen(dtItem.Rows(i).Item("Price"))
                '        TempHead += " "
                '    Next
                '    TempHead += clsCommon.myCstr(dtItem.Rows(i).Item("Item"))
                '    obj.arrColumn.Add(clsDosPrintColumn.SetColumn(clsCommon.myCstr(dtItem.Rows(i).Item("Item")), TempHead, True, DosPrintAlignment.Left, TempColWidth, False, DecimalPlaces.Two))
                'Next
                'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Amount_with_Tax", "Total Amount", False, DosPrintAlignment.Right, 20, False, DecimalPlaces.Two))

                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total", "Rate" + IIf(TempItemCount <= 5, "            ", "             "), True, DosPrintAlignment.Left, IIf(TempItemCount <= 5, 15, Math.Floor((80 / TempReportWidth) * 15)), False, DecimalPlaces.NA))

                If TempItemCount <= 5 Then
                    Dim TempSpace As Integer = clsCommon.myCdbl(80 * TempColWidth) / 100
                    For i As Integer = 0 To dtItem.Rows.Count - 1
                        Dim TempHead As String = clsCommon.myCstr(dtItem.Rows(i).Item("Price"))
                        For j As Integer = 1 To TempSpace - clsCommon.myLen(dtItem.Rows(i).Item("Price"))
                            TempHead += " "
                        Next
                        TempHead += clsCommon.myCstr(dtItem.Rows(i).Item("Item"))
                        obj.arrColumn.Add(clsDosPrintColumn.SetColumn(clsCommon.myCstr(dtItem.Rows(i).Item("Item")), TempHead, False, DosPrintAlignment.Left, TempColWidth, False, DecimalPlaces.Two))
                    Next
                Else
                    For i As Integer = 0 To dtItem.Rows.Count - 1
                        Dim TempSpace As Integer = 10
                        Dim TempHead As String = clsCommon.myCstr(dtItem.Rows(i).Item("Price"))
                        For j As Integer = 1 To TempSpace - clsCommon.myLen(dtItem.Rows(i).Item("Price")) - 1
                            TempHead += " "
                        Next
                        TempHead += clsCommon.myCstr(dtItem.Rows(i).Item("Item"))
                        obj.arrColumn.Add(clsDosPrintColumn.SetColumn(clsCommon.myCstr(dtItem.Rows(i).Item("Item")), TempHead, False, DosPrintAlignment.Left, Math.Floor((80 / TempReportWidth) * 13), False, DecimalPlaces.Two))
                    Next
                End If

                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Amount_with_Tax", "Total Amount", False, DosPrintAlignment.Right, IIf(TempItemCount <= 5, 20, Math.Floor((80 / TempReportWidth) * 20)), False, DecimalPlaces.Two))


                '-------------------------------Footer
                Dim BaseQry As String = clsCustomerMaster.GetCustomerBaseQry(False, False, "", False, " ConvRate", "'" & txtIDSCustomer.Value & "'", False, clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MMM/yyyy"), False, False, False)
                Dim TempDT As DataTable = clsDBFuncationality.GetDataTable(BaseQry)
                Dim TempSales As Decimal = 0
                Dim TempReciepts As Decimal = 0
                If TempDT.Rows.Count > 0 Then
                    TempSales = Convert.ToDecimal(TempDT.Compute("SUM(DrAmt)", String.Empty))
                    TempReciepts = Convert.ToDecimal(TempDT.Compute("SUM(CrAmt)", String.Empty))
                End If

                Dim BaseQryOPENINGINCASEOFMIS As String = clsCustomerMaster.GetCustomerBaseQry(False, False, "", False, " ConvRate", "'" & txtIDSCustomer.Value & "'", True, clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtTo), "dd/MMM/yyyy"), False, False, False)
                Dim TempDTOpening As DataTable = clsDBFuncationality.GetDataTable(BaseQryOPENINGINCASEOFMIS)
                Dim TempOpening As Decimal = 0
                If TempDTOpening.Rows.Count > 0 Then
                    TempOpening = clsCommon.myCdbl(Convert.ToDecimal(TempDTOpening.Compute("sum(DrAmt)", String.Empty))) - clsCommon.myCdbl(Convert.ToDecimal(TempDTOpening.Compute("sum(CrAmt)", String.Empty)))
                End If


                Dim TempOutStanding As Decimal = TempOpening + TempSales - TempReciepts

                obj.arrReportFooter = New List(Of clsDosPrintReportFooter)()
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Opening Balance (Excess)", TempOpening, "", "", ":"))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Total Sale", TempSales, "", "", ":"))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Total Reciepts", TempReciepts, "", "", ":"))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Outstanding Amt", TempOutStanding, "", "", ":"))

                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("This is to certify that the quantities", "", "This is to certify that the quantities and", "", ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("are verified and found correct", "", "sales proceeds are verified and found correct", "", ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("DAIRY MANAGER(DES)", "", "RSM(LMS)", "", ""))

                '---------------------------------Footer

                If TempItemCount <= 5 Then
                    obj.Print(obj, dt, PageSetup.Potrate)
                Else
                    obj.LandscapPageSetupColumnsChar = TempReportWidth
                    obj.Print(obj, dt, PageSetup.Landscap)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtIDSItem__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtIDSItem._MYValidating
        Dim qry As String = "Select TSPL_ITEM_MASTER.Item_code as Code, TSPL_ITEM_MASTER.Item_Desc as Name, TSPL_ITEM_MASTER.Alies_Name as [Alies Name] from TSPL_ITEM_MASTER"
        txtIDSItem.Value = clsCommon.ShowSelectForm("Item@IDSR", qry, "Code", "", txtIDSItem.Value, "", isButtonClicked)
        lblIDSItem.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_code = '" + txtIDSItem.Value + "' "))
    End Sub

    Private Sub txtIDSCustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtIDSCustomer._MYValidating
        Dim qry As String = " select TSPL_CUSTOMER_MASTER.Cust_Code as Code , TSPL_CUSTOMER_MASTER.Customer_Name as Name from TSPL_CUSTOMER_MASTER "
        Dim Strwhere As String = "" '"TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY  in ('Institution CR','Institution SO')"
        If clsCommon.CompairString(cmbCustomerCategory.Text, "Select") = CompairStringResult.Equal OrElse clsCommon.CompairString(cmbCustomerCategory.Text, "") = CompairStringResult.Equal Then
        Else
            Strwhere = " TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY = '" + clsCommon.myCstr(cmbCustomerCategory.Text) + "'"
        End If

        txtIDSCustomer.Value = clsCommon.ShowSelectForm("Customer1IDSR", qry, "Code", Strwhere, txtIDSCustomer.Value, "", isButtonClicked)
        lblIDSCustomer.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Cust_Code from TSPL_CUSTOMER_MASTER where Cust_Code = '" + txtIDSCustomer.Value + "' "))
    End Sub

    Private Sub btn_DistributorTS_Click(sender As Object, e As EventArgs) Handles btn_DistributorTS.Click
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal Then
            If chkShowEarlyRoute.Checked = True Then
                StrEarlyRoute = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @temp VARCHAR(MAX) SET @temp =(SELECT ',''' + cast(Route_No as varchar) + '''' FROM TSPL_ROUTE_MASTER where IsEarlyRoute=1 order by Route_No FOR XML PATH('')) select SUBSTRING(@temp, 2, 200000) AS Route"))
            Else
                StrNonEarlyRoute = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @temp VARCHAR(MAX) SET @temp =(SELECT ',''' + cast(Route_No as varchar) + '''' FROM TSPL_ROUTE_MASTER where IsEarlyRoute=0 order by Route_No FOR XML PATH('')) select SUBSTRING(@temp, 2, 200000) AS Route"))
            End If
            If chkShowEarlyRoute.Checked = True AndAlso clsCommon.myLen(StrEarlyRoute) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Early Route Not Found", Me.Text)
                Exit Sub
            ElseIf chkShowEarlyRoute.Checked = False AndAlso clsCommon.myLen(StrNonEarlyRoute) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Non Early Route Not Found", Me.Text)
                Exit Sub
            End If
            Dim whr As String = "" ' and tspl_booking_matser.AgainstGatePass=1 "
            If clsCommon.myLen(clsCommon.myCstr(txtLorryNo.Value)) > 0 Then
                whr += "  and TSPL_ROUTE_MASTER.Vehicle_Code in ('" + txtLorryNo.Value + "') "
            End If

            If txtRouteNo.arrValueMember IsNot Nothing AndAlso txtRouteNo.arrValueMember.Count > 0 Then
                whr += "  and TSPL_BOOKING_DETAIL.route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            Else
                If chkShowEarlyRoute.Checked = True Then
                    whr += "  and TSPL_BOOKING_DETAIL.route_No in (" + clsCommon.myCstr(StrEarlyRoute) + ") "
                Else
                    whr += "  and TSPL_BOOKING_DETAIL.route_No in (" + clsCommon.myCstr(StrNonEarlyRoute) + ") "
                End If
            End If

            If TxtMultiDistributor.arrValueMember IsNot Nothing AndAlso TxtMultiDistributor.arrValueMember.Count > 0 Then
                whr += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(TxtMultiDistributor.arrValueMember) + ") "
            End If

            whr += " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY ='Distributor' "

            'Dim qry = " select 'Route : '+XXXXXFinal.Route_Desc+' '+(Case when  XXXXXFinal.DataSNO = 1 and No_Of_Item =1 then SUBSTRING((XXXXXFinal.Cust_Code + ' ('+XXXXXFinal.Document_Date + ')'),0,19) else SUBSTRING ( XXXXXFinal.Customer_Name,0,19) end)+ ' ' +XXXXXFinal.Cust_Code +'('+XXXXXFinal.Document_Date+')'  as Booth_Name," & _
            '          " XXXXXFinal.Document_No,XXXXXFinal.Document_Date,XXXXXFinal.Cust_Code,Case when  XXXXXFinal.DataSNO = 1 and No_Of_Item =1 then SUBSTRING((XXXXXFinal.Cust_Code + ' ('+XXXXXFinal.Document_Date + ')'),0,19) else  SUBSTRING ( XXXXXFinal.Customer_Name,0,19) end as Customer_Name, XXXXXFinal.route_no,XXXXXFinal.Route_Desc,XXXXXFinal.Zone_Code,XXXXXFinal.Zone_Name,XXXXXFinal.Item_Code,SUBSTRING (XXXXXFinal.Alies_Name,0,8) as Alies_Name, Convert (integer,XXXXXFinal.CR) as CR,Convert (Integer ,XXXXXFinal.CD) as CD,convert (Integer,XXXXXFinal.SO) as SO,Convert (Integer ,XXXXXFinal.Cash,103) as Cash ,Convert (Integer,XXXXXFinal.Total) as Total,XXXXXFinal.CrateQty_New as CrateQty_New , XXXXXFinal.PendingPcsQty_New as PendingPcsQty_New, XXXXXFinal.Amount_with_Tax, XXXXXFinal.  DataSNO,   XXXXXFinal. Unit_code,TBL_Count.No_Of_Item from (  " & _
            '          " select XXXXX.Document_No,XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CrateQty_New as CrateQty_New , XXXXX.PendingPcsQty_New as PendingPcsQty_New,isnull (XXXXX.Amount_with_Tax,0) as Amount_with_Tax, '1' as  DataSNO, '' as  Unit_code from (  Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty , Convert (int,CrateQty) as CrateQty_New , (XXXFinal.Total) - ((StockPcs.Conversion_Factor * Convert (int,CrateQty))) as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_No,XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.OldName as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots  Union All  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   isnull([Cash_Scheme],0) as [Cash_Scheme] ,isnull([CD_Scheme],0) as [CD_Scheme] ,isnull([CR_Scheme],0) as [CR_Scheme] ,isnull([SO_Scheme],0) as [SO_Scheme] ,isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "   )  Final   pivot  (  max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_No,XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code   ) XXXXX   " & _
            '          " Union All " & _
            '          " select  XXXXX.Document_No,XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, '2' as  DataSNO, XXXXX.Unit_code as  Unit_code from (   Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty ,  'Scheme '+XXXFinal.Alies_Name as  Alies_Name_on_Name from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_No,XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   [Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme],isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No   left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code   left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code   left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code   where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "   )  Final   pivot  (   max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_No,XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code   ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code ) XXXXX " & _
            '          " Union All " & _
            '          " select  XXXXX.Document_No,XXXXX.Document_Date, 'Route Total' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CEILING_Crate_Qty  as CrateQty_New , PendingPcsQty as PendingPcsQty_New,XXXXX.Amount_with_Tax, '3' as  DataSNO, '' as  Unit_code from (    Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_No) as Document_No,max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by  XFinal.route_no, XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )XXXXX " & _
            '          " )XXXXXFinal left Outer Join " & _
            '          " ( " & _
            '          "  select XXXXXFinal.DataSNO ,XXXXXFinal.route_no,XXXXXFinal.Cust_Code , Count (Item_Code) as No_Of_Item  from ( " & _
            '          "  select '1' as  DataSNO, XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CrateQty_New as CrateQty_New , XXXXX.PendingPcsQty_New as PendingPcsQty_New,isnull (XXXXX.Amount_with_Tax,0) as Amount_with_Tax,'' as  Unit_code from (  Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty , Convert (int,CrateQty) as CrateQty_New , (XXXFinal.Total) - ((StockPcs.Conversion_Factor * Convert (int,CrateQty))) as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.OldName as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots  Union All  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   isnull([Cash_Scheme],0) as [Cash_Scheme] ,isnull([CD_Scheme],0) as [CD_Scheme] ,isnull([CR_Scheme],0) as [CR_Scheme] ,isnull([SO_Scheme],0) as [SO_Scheme] ,isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  " + whr + "  )  Final   pivot  (  max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code   ) XXXXX  " & _
            '          " Union All " & _
            '          " select '2' as  DataSNO, XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, XXXXX.Unit_code as  Unit_code from ( " & _
            '          " Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty ,  'Scheme '+XXXFinal.Alies_Name as  Alies_Name_on_Name from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   [Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme],isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No   left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code   left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code   left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code   where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "   )  Final   pivot  (   max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code   ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code " & _
            '          " ) XXXXX " & _
            '          " Union All " & _
            '          " select '3' as  DataSNO, XXXXX.Document_Date, 'Route Total' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, '' as  Unit_code from ( " & _
            '          " Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by  XFinal.route_no, XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )   XXXXX  " & _
            '          " )XXXXXFinal  group by XXXXXFinal.route_no, XXXXXFinal.Cust_Code, XXXXXFinal.DataSNO   " & _
            '          " ) " & _
            '          " TBL_Count on TBL_Count.DataSNO = XXXXXFinal.DataSNO and TBL_Count.route_no = XXXXXFinal.route_no and TBL_Count.Cust_Code = XXXXXFinal.Cust_Code " & _
            '          " Order By XXXXXFinal.route_no,XXXXXFinal.Cust_Code,XXXXXFinal.DataSNO  "

            Dim qry = " select XXXXXFinal.Vehicle_Code,Convert (decimal(18,2),isnull(XXXXXFinal.Booking_Qty,0)) as QtyInLtr, Convert (decimal(18,2),isnull (XXXXXFinal.Booking_Qty_KG,0)) as QtyInKg,'Route : '+XXXXXFinal.Route_Desc+' '+ SUBSTRING(XXXXXFinal.Customer_Name,0,19) + ' ' +XXXXXFinal.Cust_Code +'('+XXXXXFinal.Document_Date+')'  as Booth_Name," &
                      " XXXXXFinal.Document_No,XXXXXFinal.Document_Date,XXXXXFinal.Cust_Code,SUBSTRING(XXXXXFinal.Customer_Name,0,19) as Customer_Name, XXXXXFinal.route_no,XXXXXFinal.Route_Desc,XXXXXFinal.Zone_Code,XXXXXFinal.Zone_Name,XXXXXFinal.Item_Code,SUBSTRING (XXXXXFinal.Alies_Name,0,8) as Alies_Name, Convert (integer,XXXXXFinal.CR) as CR,Convert (Integer ,XXXXXFinal.CD) as CD,convert (Integer,XXXXXFinal.SO) as SO,Convert (Integer ,XXXXXFinal.Cash,103) as Cash ,Convert (Integer,XXXXXFinal.Total) as Total,XXXXXFinal.CrateQty_New as CrateQty_New , XXXXXFinal.PendingPcsQty_New as PendingPcsQty_New, XXXXXFinal.Amount_with_Tax, XXXXXFinal.  DataSNO,   XXXXXFinal. Unit_code from (  " &
                      " select XXXXX.Vehicle_Code,XXXXX.Booking_Qty, XXXXX.Booking_Qty_KG,XXXXX.Document_No,XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CrateQty_New as CrateQty_New , XXXXX.PendingPcsQty_New as PendingPcsQty_New,isnull (XXXXX.Amount_with_Tax,0) as Amount_with_Tax, '1' as  DataSNO, '' as  Unit_code from (  Select (XXXFinal.Total*Stock_SU.Conversion_Factor)/TSPL_ITEM_UOM_DETAIL_LTR.Conversion_Factor as Booking_Qty,case when isnull (TSPL_ITEM_UOM_DETAIL_LTR.Conversion_Factor,0) > 0 then 0 else  (XXXFinal.Total*Stock_SU.Conversion_Factor)/TSPL_ITEM_UOM_DETAIL_KG.Conversion_Factor   end  as Booking_Qty_KG,XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty , Convert (int,CrateQty) as CrateQty_New , (XXXFinal.Total) - ((StockPcs.Conversion_Factor * Convert (int,CrateQty))) as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_No,XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.OldName as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots  Union All  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   isnull([Cash_Scheme],0) as [Cash_Scheme] ,isnull([CD_Scheme],0) as [CD_Scheme] ,isnull([CR_Scheme],0) as [CR_Scheme] ,isnull([SO_Scheme],0) as [SO_Scheme] ,isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "   )  Final   pivot  (  max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_No,XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code   left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAIL_LTR on XXXFinal.Item_Code=TSPL_ITEM_UOM_DETAIL_LTR.Item_Code and TSPL_ITEM_UOM_DETAIL_LTR.UOM_Code='LTR'   left join TSPL_ITEM_UOM_DETAIL as  TSPL_ITEM_UOM_DETAIL_KG on XXXFinal.Item_Code=TSPL_ITEM_UOM_DETAIL_KG.Item_Code and TSPL_ITEM_UOM_DETAIL_KG.UOM_Code='KG') XXXXX   " &
                      " Union All " &
                      " select  XXXXX.Vehicle_Code,XXXXX.Booking_Qty,XXXXX.Booking_Qty_KG,XXXXX.Document_No,XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, '2' as  DataSNO, XXXXX.Unit_code as  Unit_code from (   Select (XXXFinal.Total*Stock_SU.Conversion_Factor)/TSPL_ITEM_UOM_DETAIL_LTR.Conversion_Factor as Booking_Qty,case when isnull (TSPL_ITEM_UOM_DETAIL_LTR.Conversion_Factor,0) > 0 then 0 else  (XXXFinal.Total*Stock_SU.Conversion_Factor)/TSPL_ITEM_UOM_DETAIL_KG.Conversion_Factor   end  as Booking_Qty_KG,XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty ,  'Scheme '+XXXFinal.Alies_Name as  Alies_Name_on_Name from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_No,XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   [Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme],isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No   left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code   left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code   left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code   where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "   )  Final   pivot  (   max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_No,XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code   ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAIL_LTR on XXXFinal.Item_Code=TSPL_ITEM_UOM_DETAIL_LTR.Item_Code and TSPL_ITEM_UOM_DETAIL_LTR.UOM_Code='LTR'  left join TSPL_ITEM_UOM_DETAIL as  TSPL_ITEM_UOM_DETAIL_KG on XXXFinal.Item_Code=TSPL_ITEM_UOM_DETAIL_KG.Item_Code and TSPL_ITEM_UOM_DETAIL_KG.UOM_Code='KG' ) XXXXX "
            '" Union All " & _
            '" select  XXXXX.Vehicle_Code,XXXXX.Booking_Qty,XXXXX.Document_No,XXXXX.Document_Date, 'Route Total' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CEILING_Crate_Qty  as CrateQty_New , PendingPcsQty as PendingPcsQty_New,XXXXX.Amount_with_Tax, '3' as  DataSNO, '' as  Unit_code from (    Select (XXXFinal.Total*Stock_SU.Conversion_Factor)/TSPL_ITEM_UOM_DETAIL_LTR.Conversion_Factor as Booking_Qty,XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_No) as Document_No,max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by  XFinal.route_no, XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAIL_LTR on XXXFinal.Item_Code=TSPL_ITEM_UOM_DETAIL_LTR.Item_Code and TSPL_ITEM_UOM_DETAIL_LTR.UOM_Code='LTR' )XXXXX " & _
            qry += " )XXXXXFinal " 'left Outer Join " & _
            '" ( " & _
            '"  select XXXXXFinal.DataSNO ,XXXXXFinal.route_no,XXXXXFinal.Cust_Code , Count (Item_Code) as No_Of_Item  from ( " & _
            '"  select '1' as  DataSNO, XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CrateQty_New as CrateQty_New , XXXXX.PendingPcsQty_New as PendingPcsQty_New,isnull (XXXXX.Amount_with_Tax,0) as Amount_with_Tax,'' as  Unit_code from (  Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty , Convert (int,CrateQty) as CrateQty_New , (XXXFinal.Total) - ((StockPcs.Conversion_Factor * Convert (int,CrateQty))) as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.OldName as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots  Union All  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   isnull([Cash_Scheme],0) as [Cash_Scheme] ,isnull([CD_Scheme],0) as [CD_Scheme] ,isnull([CR_Scheme],0) as [CR_Scheme] ,isnull([SO_Scheme],0) as [SO_Scheme] ,isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  " + whr + "  )  Final   pivot  (  max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code   ) XXXXX  " & _
            '" Union All " & _
            '" select '2' as  DataSNO, XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, XXXXX.Unit_code as  Unit_code from ( " & _
            '" Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty ,  'Scheme '+XXXFinal.Alies_Name as  Alies_Name_on_Name from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   [Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme],isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No   left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code   left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code   left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code   where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "   )  Final   pivot  (   max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code   ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code " & _
            '" ) XXXXX " & _
            '" Union All " & _
            '" select '3' as  DataSNO, XXXXX.Document_Date, 'Route Total' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, '' as  Unit_code from ( " & _
            '" Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,TSPL_BOOKING_MATSER.Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,TSPL_BOOKING_DETAIL.Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by  XFinal.route_no, XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )   XXXXX  " & _
            '" )XXXXXFinal  group by XXXXXFinal.route_no, XXXXXFinal.Cust_Code, XXXXXFinal.DataSNO   " & _
            '" ) " & _
            '" TBL_Count on TBL_Count.DataSNO = XXXXXFinal.DataSNO and TBL_Count.route_no = XXXXXFinal.route_no and TBL_Count.Cust_Code = XXXXXFinal.Cust_Code " & _
            '" Order By XXXXXFinal.route_no,XXXXXFinal.Cust_Code,XXXXXFinal.DataSNO  "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then

                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = objCommonVar.CurrentCompanyName

                obj.ReportName1 = "DISTRIBUTOR WISE EVENING TRUCK SHEET OF " + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(TSP_Date.Value), "dd/MM/yyyy")) + " "
                obj.ReportName2 = "Time:                                          Vehicle No: " + clsCommon.myCstr(dt.Rows(0).Item("Vehicle_Code"))

                obj.ShowPageNo = True
                obj.arrGroup = New List(Of clsDosPrintGroup)()
                obj.arrGroup.Add(clsDosPrintGroup.GetObject("Document_No", "TruckSheet No", ""))
                obj.arrGroup.Add(clsDosPrintGroup.GetObject("Booth_Name", "Booth Name", ""))

                obj.arrColumn = New List(Of clsDosPrintColumn)()
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Alies_Name", "Type", True, DosPrintAlignment.Left, 20, False, DecimalPlaces.NA))
                'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("CR", "CR", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("CD", "CD", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SO", "SO", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cash", "Cash", False, DosPrintAlignment.Right, 20, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total", "Total", False, DosPrintAlignment.Right, 20, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("CrateQty_New", "Crates", False, DosPrintAlignment.Right, 20, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Amount_with_Tax", "Amount", False, DosPrintAlignment.Right, 20, False, DecimalPlaces.Two))

                Dim TempQtyLtr As Decimal = Convert.ToDecimal(dt.Compute("SUM(QtyInLtr)", String.Empty))
                Dim TempQtyKG As Decimal = Convert.ToDecimal(dt.Compute("SUM(QtyInKg)", String.Empty))
                Dim strTotalLtrKg As String = "TOT LTRS : " + clsCommon.myCstr(TempQtyLtr) + "   TOT KG :" + clsCommon.myCstr(TempQtyKG)
                Dim TempQtyCrates As Decimal = Convert.ToDecimal(dt.Compute("SUM(CrateQty_New)", String.Empty))
                obj.arrReportFooter = New List(Of clsDosPrintReportFooter)()
                'obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("TOT LTRS :", clsCommon.myCstr(TempQtyLtr) + " TOT KG : " + clsCommon.myCstr(TempQtyKG) + "", "", "", ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject(strTotalLtrKg, "", "", "", ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("  ", "  ", "  ", "  ", ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Scheme Quantity(Included in the above)", "  ", "  ", "  ", "  "))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("  ", "  ", "  ", "  ", ""))

                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("LOADED BY           SECURITY            Shift-In Charge     RMD", "  ", "  ", "  ", "  "))
                'obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("LOADED BY", "SECURITY", "Shift-In Charge", "RMD", ""))
                obj.Print(obj, dt, PageSetup.Potrate)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
        End If
    End Sub

    Private Sub TxtMultiDistributor__My_Click(sender As Object, e As EventArgs) Handles TxtMultiDistributor._My_Click
        strQry = " select Cust_Code as [Code] , Customer_Name as [Name] from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY ='Distributor'"
        TxtMultiDistributor.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultDist", strQry, "Code", "Name", TxtMultiDistributor.arrValueMember, TxtMultiDistributor.arrDispalyMember)
    End Sub

    Private Sub btn_CancelTruckSheet_Click(sender As Object, e As EventArgs) Handles btn_CancelTruckSheet.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Do you want to Cancel Truck Sheet for Date [" + clsCommon.GetPrintDate(TSP_Date.Value, "dd/MMM/yyyy") + "]" + " ?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim Qry As String = "Update TSPL_BOOKING_MATSER set TruckSheetGenerate = 0 from TSPL_BOOKING_MATSER left join TSPL_BOOKING_DETAIL on TSPL_BOOKING_DETAIL.Document_No=TSPL_BOOKING_MATSER.Document_No left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_BOOKING_DETAIL.cust_code where ISNULL(TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY,'') not in ('Others','Distributor','') and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) "
                clsDBFuncationality.ExecuteNonQuery(Qry)
                common.clsCommon.MyMessageBoxShow(Me, "Truck Sheet Cancel Successfully.", Me.Text)
                btn_TruckSheetGenerated.Enabled = True
                btn_CancelTruckSheet.Enabled = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub GetTruckSheetQry(ByVal strCondition As String, Optional ByVal isGatePassTruckSheet As Boolean = False)
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal Then
            If chkShowEarlyRoute.Checked = True Then
                StrEarlyRoute = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @temp VARCHAR(MAX) SET @temp =(SELECT ',''' + cast(Route_No as varchar) + '''' FROM TSPL_ROUTE_MASTER where IsEarlyRoute=1 order by Route_No FOR XML PATH('')) select SUBSTRING(@temp, 2, 200000) AS Route"))
            Else
                StrNonEarlyRoute = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @temp VARCHAR(MAX) SET @temp =(SELECT ',''' + cast(Route_No as varchar) + '''' FROM TSPL_ROUTE_MASTER where IsEarlyRoute=0 order by Route_No FOR XML PATH('')) select SUBSTRING(@temp, 2, 200000) AS Route"))
            End If
            If chkShowEarlyRoute.Checked = True AndAlso clsCommon.myLen(StrEarlyRoute) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Early Route Not Found", Me.Text)
                Exit Sub
            ElseIf chkShowEarlyRoute.Checked = False AndAlso clsCommon.myLen(StrNonEarlyRoute) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Non Early Route Not Found", Me.Text)
                Exit Sub
            End If
            Dim whr As String = " and " + strCondition + " "
            Dim whrCustCategory As String = ""
            Dim whrVehicle As String = ""
            Dim whrRoute As String = ""
            Dim whrShipment As String = ""
            Dim isTrueFalse As String = " where 2=2"
            If isGatePassTruckSheet = True Then
                isTrueFalse = " where 2=3 "
            End If
            If clsCommon.myLen(clsCommon.myCstr(txtLorryNo.Value)) > 0 Then
                whr += "  and TSPL_ROUTE_MASTER.Vehicle_Code in ('" + txtLorryNo.Value + "') "
                whrVehicle = " and xx.Vehicle_Code in  ('" + txtLorryNo.Value + "') "
                whrShipment = " and TSPL_ROUTE_MASTER.Vehicle_Code in ('" + txtLorryNo.Value + "')  "
            End If

            If txtRouteNo.arrValueMember IsNot Nothing AndAlso txtRouteNo.arrValueMember.Count > 0 Then
                whr += "  and TSPL_BOOKING_DETAIL.route_No in (" + clsCommon.GetMulcallString(txtRouteNo.arrValueMember) + ") "
                whrRoute = " and TSPL_CUSTOMER_MASTER.Route_No in (" + clsCommon.GetMulcallString(txtRouteNo.arrValueMember) + ") "
                whrShipment = " and TSPL_SD_SHIPMENT_HEAD.Route_No in  (" + clsCommon.GetMulcallString(txtRouteNo.arrValueMember) + ") "
            Else
                If chkShowEarlyRoute.Checked = True Then
                    whr += "  and TSPL_BOOKING_DETAIL.route_No in (" + clsCommon.myCstr(StrEarlyRoute) + ") "
                    whrRoute = " and TSPL_CUSTOMER_MASTER.Route_No in (" + clsCommon.myCstr(StrEarlyRoute) + ") "
                    whrShipment = " and TSPL_SD_SHIPMENT_HEAD.Route_No in  (" + clsCommon.myCstr(StrEarlyRoute) + ") "
                Else
                    whr += "  and TSPL_BOOKING_DETAIL.route_No in (" + clsCommon.myCstr(StrNonEarlyRoute) + ") "
                    whrRoute = " and TSPL_CUSTOMER_MASTER.Route_No in (" + clsCommon.myCstr(StrNonEarlyRoute) + ") "
                    whrShipment = " and TSPL_SD_SHIPMENT_HEAD.Route_No in  (" + clsCommon.myCstr(StrNonEarlyRoute) + ") "
                End If
            End If

            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                whr += " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ")" + Environment.NewLine
                whrCustCategory = " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ") "
                whrShipment = " and   TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ") "
            End If

            Dim dtCreateDetailsDate As DateTime = TSP_Date.Value.AddDays(-1) 'New Date(TSP_Date.Value.Day, TSP_Date.Value.Day, -1)
            Dim strCreateDetails As String = " With CTETemp as ( Select convert(varchar,Doc_Date,103) as Doc_Date, Route_No,Route_Desc,Vehicle_Code,Vehicle_Name,Customer_Code,Customer_Name,Customer_Phone, ZSM , ZSM_NAME,ZSM_Phone,ASM,ASM_Name,ASM_Phone,ASO , ASO_Name,ASO_Phone ,OpencrateQty, OpenJaaliQty, OpenBoxQty,OpenCanQty, CrateQtyRecd, JaaliQtyRecd, BoxQtyRecd ,CanQtyRecd,CrateOutQty, jaaliOutQty,boxOutQty  ,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty ,CanAdjQty,SUM(CrateQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as CrateQtyClosing,  SUM(JaaliQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as JaaliQtyClosing,  SUM(BoxQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as BoxQtyClosing, SUM(CanQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as CanQtyClosing , Row_Number() OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as RowNo from(  " &
                                             " select  pp.Doc_Date  as Doc_Date,TSPL_CUSTOMER_MASTER.Route_No,TSPL_CUSTOMER_MASTER.Route_Desc,pp.Vehicle_Code,tspl_vehicle_master.Number as Vehicle_Name ,pp.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Phone1 as Customer_Phone ,   TSPL_CUSTOMER_MASTER.ZSM,TSPL_EMPLOYEE_MASTER_ZSM.Emp_Name as ZSM_NAME,case when len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PHONE,'')) > 0 And  len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO,'')) > 0  then '['+ TSPL_EMPLOYEE_MASTER_ZSM.PHONE + '],['  + TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO +']' when  len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PHONE,'')) > 0 then TSPL_EMPLOYEE_MASTER_ZSM.PHONE when len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO,'')) > 0 then TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO else '' end  as ZSM_Phone ,TSPL_CUSTOMER_MASTER.ASM, TSPL_EMPLOYEE_MASTER_ASM.Emp_Name as ASM_NAME, case when len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PHONE,'')) > 0 And  len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO,'')) > 0  then '['+ TSPL_EMPLOYEE_MASTER_ASM.PHONE + '],['  + TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO +']' when  len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PHONE,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASM.PHONE when len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO else '' end  as ASM_Phone,TSPL_CUSTOMER_MASTER.ASO,TSPL_EMPLOYEE_MASTER_ASO.Emp_Name as ASO_NAME,   case when len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PHONE,'')) > 0 And  len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO,'')) > 0  then '['+ TSPL_EMPLOYEE_MASTER_ASO.PHONE + '],['  + TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO +']' when  len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PHONE,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASO.PHONE when len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO else '' end  as ASO_Phone,pp.OpencrateQty as OpencrateQty,pp.OpenJaaliQty  as OpenJaaliQty,pp.OpenBoxQty  as OpenBoxQty,pp.OpenCanQty  as OpenCanQty,pp.CrateQtyRecd  as CrateQtyRecd,pp.JaaliQtyRecd  as JaaliQtyRecd,pp.BoxQtyRecd  as BoxQtyRecd ,pp.CanQtyRecd  as CanQtyRecd,pp.CrateOutQty  as CrateOutQty,pp.jaaliOutQty  as jaaliOutQty ,pp.boxOutQty  as boxOutQty,pp.CanOutQty  as CanOutQty ,pp.CrateQtyClosing as CrateQtyClosing, pp.JaaliQtyClosing as  JaaliQtyClosing, pp.BoxQtyClosing as BoxQtyClosing, pp.CanQtyClosing as CanQtyClosing,pp.CrateAdjQty , pp.JaaliAdjQty  , pp.BoxAdjQty , pp.CanAdjQty from ( " &
                                             " select  max(convert(date,Doc_Date,103))  as Doc_Date,max(xx.Vehicle_Code) as Vehicle_Code,xx.Customer_Code,sum(xx.OpencrateQty) as OpencrateQty,sum(xx.OpenJaaliQty ) as OpenJaaliQty,sum(xx.OpenBoxQty )  as OpenBoxQty,sum(xx.OpenCanQty )  as OpenCanQty,sum(xx.CrateQtyRecd) as CrateQtyRecd,sum(xx.JaaliQtyRecd) as JaaliQtyRecd,sum(xx.BoxQtyRecd) as BoxQtyRecd,sum(xx.CanQtyRecd) as CanQtyRecd,sum(xx.CrateOutQty ) as CrateOutQty,sum(xx.jaaliOutQty ) as jaaliOutQty ,sum(xx.boxOutQty ) as boxOutQty,sum(xx.CanOutQty ) as CanOutQty, sum(xx.CrateAdjQty ) as CrateAdjQty ,sum(xx.JaaliAdjQty )as JaaliAdjQty  ,sum(xx.BoxAdjQty )  as BoxAdjQty ,sum(xx.CanAdjQty )  as CanAdjQty,(sum(xx.OpencrateQty)+sum(xx.CrateOutQty )-sum(xx.CrateQtyRecd)-sum(xx.CrateAdjQty )) as CrateQtyClosing, (sum(xx.OpenJaaliQty)+sum(xx.jaaliOutQty)-sum(xx.JaaliQtyRecd)-sum(xx.JaaliAdjQty )) as JaaliQtyClosing, (sum(xx.OpenBoxQty)+sum(xx.boxOutQty )-sum(xx.BoxQtyRecd)-sum(xx.BoxAdjQty )) as BoxQtyClosing  , (sum(xx.OpenCanQty)+sum(xx.CanOutQty )-sum(xx.CanQtyRecd)-sum(xx.CanAdjQty )) as CanQtyClosing  from (select  max(convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtCreateDetailsDate), "dd/MMM/yyyy") + "',103)) as Doc_Date,max(Opening.Vehicle_Code) as Vehicle_Code , Opening.Customer_Code ,sum(Opening.OpencrateQty*Type) as OpencrateQty,sum(Opening.OpenJaaliQty*Type ) as OpenJaaliQty,sum(Opening.OpenBoxQty *Type)  as OpenBoxQty,sum(Opening.OpenCanQty *Type)  as OpenCanQty,sum(Opening.CrateQtyRecd *Type) as CrateQtyRecd,sum(Opening.JaaliQtyRecd*Type ) as JaaliQtyRecd,sum(Opening.BoxQtyRecd*Type ) as BoxQtyRecd,sum(Opening.CanQtyRecd*Type ) as CanQtyRecd,sum(Opening.CrateOutQty*Type ) as CrateOutQty,sum(Opening.jaaliOutQty*Type ) as jaaliOutQty ,sum(Opening.boxOutQty*Type ) as boxOutQty,sum(Opening.CanOutQty*Type ) as CanOutQty,sum(Opening.CrateAdjQty*Type ) as CrateAdjQty,sum(Opening.JaaliAdjQty*Type ) as JaaliAdjQty,sum(Opening.BoxAdjQty *Type) as  BoxAdjQty ,sum(Opening.CanAdjQty *Type) as  canAdjQty from  ( select TSPL_SD_SHIPMENT_HEAD.Document_Date    as Document_Date,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) as Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.customer_code  ,1 as Type  ,'O' as Type1,TSPL_SD_SHIPMENT_HEAD.Crate as OpencrateQty,TSPL_SD_SHIPMENT_HEAD.jaali as OpenJaaliQty, TSPL_SD_SHIPMENT_HEAD.box  as OpenBoxQty , TSPL_SD_SHIPMENT_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS'  AND TSPL_SD_SHIPMENT_HEAD.Status =1  union all select TSPL_sd_SALE_RETURN_HEAD.Document_Date    as Document_Date,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.customer_code  ,-1 as Type  ,'I' as Type1,TSPL_sd_SALE_RETURN_HEAD.CrateQty as OpencrateQty,TSPL_sd_SALE_RETURN_HEAD.jaali as OpenJaaliQty, TSPL_sd_SALE_RETURN_HEAD.box  as OpenBoxQty , TSPL_sd_SALE_RETURN_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_sd_SALE_RETURN_HEAD  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS'  AND TSPL_sd_SALE_RETURN_HEAD.Status =1  union all  select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  , TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as OpencrateQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty   as OpenjaaliQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty  as OpenboxQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty as OpenCanQty , 0 as CrateQtyRecd,0 JaaliQtyRecd , 0 BoxQtyRecd , 0 CanQtyRecd , 0 as CrateOutQty, 0 jaaliOutQty, 0 boxoutqty, 0 Canoutqty, 0  as CrateAdjQty, 0  as JaaliAdjQty, 0  as BoxAdjQty, 0  as CanAdjQty from TSPL_CRATE_RECEIVED_detail_FRESHSALE left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  union all select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  , isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment,0)  as OpencrateQty, isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment,0)    as OpenjaaliQty , isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment,0)  as OpenboxQty, isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment,0)  as OpenCanQty, 0 as CrateQtyRecd,0 JaaliQtyRecd , 0 BoxQtyRecd , 0 CanQtyRecd , 0 as CrateOutQty, 0 jaaliOutQty, 0 boxoutqty, 0 Canoutqty, 0  as CrateAdjQty, 0  as JaaliAdjQty, 0  as BoxAdjQty, 0  as CanAdjQty from TSPL_CRATE_RECEIVED_detail_FRESHSALE left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1   )as Opening WHERE convert(date,Document_Date,103)<(convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtCreateDetailsDate), "dd/MMM/yyyy") + "',103)) group by Customer_Code  " &
                                             " UNION All " &
                                             " select Document_Date,Vehicle_Code,Customer_Code,0 as OpencrateQty,0 as OpenjaaliQty ,0 as OpenboxQty,0 as OpenCanQty,Case When [Type]=1 Then CrateQtyRecd Else 0 End as CrateQtyRecd,Case When [Type]=1 Then JaaliQtyRecd Else 0 End as JaaliQtyRecd,Case When [Type]=1 Then BoxQtyRecd Else 0 End as BoxQtyRecd,Case When [Type]=1 Then CANQtyRec Else 0 End as CANQtyRec, " &
                                             " Case When [Type]=-1 Then CrateOutQty Else 0 End as CrateOutQty,Case When [Type]=-1 Then jaaliOutQty Else 0 End as jaaliOutQty,Case When [Type]=-1 Then boxOutQty Else 0 End as boxOutQty,Case When [Type]=-1 Then CanOutQty Else 0 End as CanOutQty,Case When [Type]=1 Then CrateAdjQty Else 0 End as CrateAdjQty,Case When [Type]=1 Then JaaliAdjQty Else 0 End as JaaliAdjQty,Case When [Type]=1 Then BoxAdjQty Else 0 End as BoxAdjQty,Case When [Type]=1 Then CANAdjustment Else 0 End as CANAdjustment  " &
                                             " from ((select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCanQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE " &
                                             " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  " &
                                             " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1 ) union all  " &
                                             " (select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCANQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE " &
                                             " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  " &
                                             " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  union all  " &
                                             " select TSPL_SD_SHIPMENT_HEAD.Document_Date   as Document_Date,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) as Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.customer_code  ,-1 as Type  ,'O' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty, 0 as CrateQtyRecd, 0 JaaliQtyRecd ,  0  as BoxQtyRecd,0 CanQtyRecd  ,TSPL_SD_SHIPMENT_HEAD.Crate as CrateOutQty,jaali  as jaaliOutQty,TSPL_SD_SHIPMENT_HEAD.Box as boxOutQty,TSPL_SD_SHIPMENT_HEAD.ShippedCAN as CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty    from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS' AND TSPL_SD_SHIPMENT_HEAD.Status =1 union all  " &
                                             " select TSPL_sd_SALE_RETURN_HEAD.Document_Date   as Document_Date,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.customer_code  ,1 as Type  ,'I' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty, TSPL_sd_SALE_RETURN_HEAD.CrateQty as CrateQtyRecd, TSPL_sd_SALE_RETURN_HEAD.jaali as JaaliQtyRecd ,  TSPL_sd_SALE_RETURN_HEAD.Box  as BoxQtyRecd,TSPL_sd_SALE_RETURN_HEAD.ShippedCAN CanQtyRecd  ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty,0 as CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty    from TSPL_sd_SALE_RETURN_HEAD  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS' AND TSPL_sd_SALE_RETURN_HEAD.Status =1 )  ) as Closing  " &
                                             " WHERE convert(date,Document_Date ,103)>=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtCreateDetailsDate), "dd/MMM/yyyy") + "',103) AND convert(date,Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtCreateDetailsDate), "dd/MMM/yyyy") + "',103)  " &
                                             " ) as xx where 2=2   " &
                                             " and xx.Customer_Code in (Select distinct TSPL_BOOKING_DETAIL.Cust_Code  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  " &
                                             " left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code " &
                                             " where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  and " + strCondition + "  " &
                                             " " + whrCustCategory + " " &
                                             " ) " &
                                             " " + whrVehicle + " " &
                                             " GROUP BY Customer_Code " &
                                             " ) as pp   left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =pp.Customer_Code  " &
                                             " left outer Join  TSPL_EMPLOYEE_MASTER as TSPL_EMPLOYEE_MASTER_ZSM on TSPL_EMPLOYEE_MASTER_ZSM.EMP_CODE = TSPL_CUSTOMER_MASTER.ZSM  " &
                                             " left outer Join  TSPL_EMPLOYEE_MASTER as TSPL_EMPLOYEE_MASTER_ASM on TSPL_EMPLOYEE_MASTER_ASM.EMP_CODE = TSPL_CUSTOMER_MASTER.ASM  " &
                                             " left outer Join  TSPL_EMPLOYEE_MASTER as TSPL_EMPLOYEE_MASTER_ASO on TSPL_EMPLOYEE_MASTER_ASO.EMP_CODE = TSPL_CUSTOMER_MASTER.ASO  " &
                                             " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=pp.vehicle_code where 2=2   " + whrRoute + " " &
                                             " ) YYY )   "
            Dim qry = " " + strCreateDetails + " " &
                     " select XXXXXFinal.Document_Date,XXXXXFinal.Cust_Code,Case when  XXXXXFinal.DataSNO = 1 and No_Of_Item =1 then SUBSTRING((XXXXXFinal.Cust_Code + ' ('+XXXXXFinal.Document_Date + ')'),0,19) else  SUBSTRING ( XXXXXFinal.Customer_Name,0,19) end as Customer_Name, XXXXXFinal.route_no,XXXXXFinal.Route_Desc,XXXXXFinal.Zone_Code,XXXXXFinal.Zone_Name,XXXXXFinal.Item_Code,SUBSTRING (XXXXXFinal.Alies_Name,0,8) as Alies_Name, Convert (integer,XXXXXFinal.CR) as CR,Convert (Integer ,XXXXXFinal.CD) as CD,convert (Integer,XXXXXFinal.SO) as SO,Convert (Integer ,XXXXXFinal.Cash,103) as Cash ,Convert (Integer,XXXXXFinal.Total) as Total,XXXXXFinal.CrateQty_New as CrateQty_New , XXXXXFinal.PendingPcsQty_New as PendingPcsQty_New, XXXXXFinal.Amount_with_Tax, XXXXXFinal.  DataSNO,   XXXXXFinal. Unit_code,TBL_Count.No_Of_Item ,  XXXXXFinal.LR_PQ_ES ,isnull (XXXXXFinal.OpencrateQty,0) as OpencrateQty , isnull ( XXXXXFinal.CrateQtyRecd,0) as CrateQtyRecd ,isnull( XXXXXFinal.CrateOutQty,0) as CrateOutQty, isnull ( XXXXXFinal.CrateAdjQty,0) as CrateAdjQty , isnull (XXXXXFinal.CrateQtyClosing,0) as CrateQtyClosing, Total_Scheme_Star, isnull ( Qty_in_KG,0) as Qty_in_KG , isnull (Qty_in_Ltr,0) as Qty_in_Ltr  from (  " &
                     " select XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CrateQty_New as CrateQty_New , XXXXX.PendingPcsQty_New as PendingPcsQty_New,isnull (XXXXX.Amount_with_Tax,0) as Amount_with_Tax, '1' as  DataSNO, '' as  Unit_code,0 as  LR_PQ_ES , 0 as OpencrateQty , 0 as CrateQtyRecd , 0 as CrateOutQty , 0 as CrateAdjQty , 0 as CrateQtyClosing, case when  isnull (XXXXX.Total_Scheme,0) > 0 then '*' else '' end as Total_Scheme_Star,0 as Qty_in_KG , 0 as Qty_in_Ltr  from (  Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty , Convert (int,CrateQty) as CrateQty_New , (XXXFinal.Total) - ((StockPcs.Conversion_Factor * Convert (int,CrateQty))) as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when  TSPL_BOOKING_MATSER.Booking_Type in ('FN','PS','UP') then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.OldName as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots  Union All  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   isnull([Cash_Scheme],0) as [Cash_Scheme] ,isnull([CD_Scheme],0) as [CD_Scheme] ,isnull([CR_Scheme],0) as [CR_Scheme] ,isnull([SO_Scheme],0) as [SO_Scheme] ,isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when  TSPL_BOOKING_MATSER.Booking_Type in ('FN','PS','UP') then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "   )  Final   pivot  (  max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code   ) XXXXX   " &
                     " Union All " &
                     " select  XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, '2' as  DataSNO, XXXXX.Unit_code as  Unit_code,  0 as  LR_PQ_ES , 0 as OpencrateQty , 0 as CrateQtyRecd , 0 as CrateOutQty , 0 as CrateAdjQty , 0 as CrateQtyClosing , '' as Total_Scheme_Star , 0 as Qty_in_KG , 0 as Qty_in_Ltr from (   Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty ,  'Scheme '+XXXFinal.Alies_Name as  Alies_Name_on_Name from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   [Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme],isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when  TSPL_BOOKING_MATSER.Booking_Type in ('FN','PS','UP') then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No   left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code   left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code   left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code   where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "   )  Final   pivot  (   max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code   ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code ) XXXXX " &
                     " Union All " &
                     " select  XXXXX.Document_Date, 'Route Total' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CEILING_Crate_Qty  as CrateQty_New , PendingPcsQty as PendingPcsQty_New,XXXXX.Amount_with_Tax, '3' as  DataSNO, '' as  Unit_code ,  0 as  LR_PQ_ES , 0 as OpencrateQty , 0 as CrateQtyRecd , 0 as CrateOutQty , 0 as CrateAdjQty , 0 as CrateQtyClosing, '' as Total_Scheme_Star , 0 as Qty_in_KG , 0 as Qty_in_Ltr from (    Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty, Convert (int,CrateQty) as CrateQty_New , (XXXFinal.Total) - ((StockPcs.Conversion_Factor * Convert (int,CrateQty))) as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New  from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when  TSPL_BOOKING_MATSER.Booking_Type in ('FN','PS','UP') then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by  XFinal.route_no, XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )XXXXX " &
                     "  " &
                     " Union all  " &
                     " select  XXXXX.Document_Date, XXXXX.Cust_Code as Cust_Code,'Customer Leakage' as Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, '22' as  DataSNO, XXXXX.Unit_code as  Unit_code  , LR_PQ_ES , 0 as OpencrateQty , 0 as CrateQtyRecd , 0 as CrateOutQty , 0 as CrateAdjQty , 0 as CrateQtyClosing, '' as Total_Scheme_Star, 0 as Qty_in_KG , 0 as Qty_in_Ltr from ( Select Document_Date,Cust_Code, max(Customer_Name) as Customer_Name ,Route_No,max(Route_Desc) as Route_Desc,Zone_Code,max(Zone_Name) as Zone_Name  , Item_Code, max (Alies_Name) as Alies_Name , sum (CR) as CR,sum (CD) as CD,Sum(SO) as SO ,sum (Cash) as Cash, sum(Total) as Total ,sum( CrateQty_New) as CrateQty_New,sum( PendingPcsQty_New) as PendingPcsQty_New, sum (Amount_with_Tax) as Amount_with_Tax,Unit_code,sum (LR_PQ_ES) as LR_PQ_ES  from ( Select convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 0 as CR,0 as CD,0 as SO,0 as Cash,0 as Total,0  as CrateQty_New , 0 as PendingPcsQty_New,0 as Amount_with_Tax, 'PCS' as  Unit_code , Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty) as LR_PQ_ES from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='PCS' inner join ( Select distinct TSPL_BOOKING_DETAIL.Cust_Code  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No    left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  and " + strCondition + "  " + whr + " ) TBL_VALID_Cust on TBL_VALID_Cust.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code where  TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1  and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) and TSPL_SD_SHIPMENT_HEAD.status = 1   " + whrShipment + " ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code ) XXXXX  " + isTrueFalse + "  " &
                     " Union all " &
                     " select  XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, '4' as  DataSNO, XXXXX.Unit_code as  Unit_code  , LR_PQ_ES , 0 as OpencrateQty , 0 as CrateQtyRecd , 0 as CrateOutQty , 0 as CrateAdjQty , 0 as CrateQtyClosing, '' as Total_Scheme_Star , 0 as Qty_in_KG , 0 as Qty_in_Ltr from ( Select Document_Date, 'Total Route Leakage' as Cust_Code, 'Total Route Leakage' as Customer_Name ,Route_No,max(Route_Desc) as Route_Desc,Zone_Code,max(Zone_Name) as Zone_Name  , Item_Code, max (Alies_Name) as Alies_Name , sum (CR) as CR,sum (CD) as CD,Sum(SO) as SO ,sum (Cash) as Cash, sum(Total) as Total ,sum( CrateQty_New) as CrateQty_New,sum( PendingPcsQty_New) as PendingPcsQty_New, sum (Amount_with_Tax) as Amount_with_Tax,Unit_code,sum (LR_PQ_ES) as LR_PQ_ES  from ( Select convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 0 as CR,0 as CD,0 as SO,0 as Cash,0 as Total,0  as CrateQty_New , 0 as PendingPcsQty_New,0 as Amount_with_Tax, 'PCS' as  Unit_code , Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty) as LR_PQ_ES  from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='PCS' inner join ( Select distinct TSPL_BOOKING_DETAIL.Cust_Code  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No    left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  and " + strCondition + "  " + whr + ") TBL_VALID_Cust on TBL_VALID_Cust.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code where  TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others') and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1  and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) and TSPL_SD_SHIPMENT_HEAD.status = 1   " + whrShipment + " ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no,XFinal.Item_Code,XFinal.Unit_code ) XXXXX  " + isTrueFalse + "  " &
                     " Union all " &
                     " Select  XXXXX2.Document_Date,max( XXXXX2.Cust_Code) as Cust_Code ,max (XXXXX2.Customer_Name) as  Customer_Name,XXXXX2.route_no,max(XXXXX2.Route_Desc) as Route_Desc,max (XXXXX2.Zone_Code) as Zone_Code,max(XXXXX2.Zone_Name) as Zone_Name, max(XXXXX2.Item_Code) as Item_Code,max(XXXXX2.Alies_Name) as Alies_Name,sum(XXXXX2.CR) as CR ,Sum(XXXXX2.CD) as CD,sum(XXXXX2.SO) as SO,sum(XXXXX2.Cash) as Cash,sum(XXXXX2.Total) as Total,sum(XXXXX2.CrateQty_New)  as CrateQty_New , sum(XXXXX2.PendingPcsQty_New) as PendingPcsQty_New,sum(XXXXX2.Amount_with_Tax) as Amount_with_Tax, max(XXXXX2.DataSNO) as  DataSNO, max(XXXXX2.Unit_code) as  Unit_code, max(XXXXX2.LR_PQ_ES) as  LR_PQ_ES  , 0 as OpencrateQty , 0 as CrateQtyRecd , 0 as CrateOutQty , 0 as CrateAdjQty , 0 as CrateQtyClosing , '' as Total_Scheme_Star,  sum(XXXXX2.Qty_in_KG) as  Qty_in_KG, sum (Qty_in_Ltr) as Qty_in_Ltr from (  select  XXXXX.Document_Date, 'Total Route Summary' as Cust_Code,'Total Route Summary'as Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,'' as Item_Code,'' as Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CrateQty_New  as CrateQty_New , PendingPcsQty as PendingPcsQty_New,XXXXX.Amount_with_Tax, '5' as  DataSNO, '' as  Unit_code, 0 as  LR_PQ_ES, Total_KG as Qty_in_KG , Total_LTR as  Qty_in_Ltr from (    Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  ,Convert (int,CEILING_Crate_Qty) as CrateQty_New , (XXXFinal.Total) - ((StockPcs.Conversion_Factor * Convert (int,CrateQty))) as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New   from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,max (Unit_Code_For_Create) as Unit_Code_For_Create,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item, sum (Total_KG) as Total_KG, sum (Total_LTR) as Total_LTR from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,Unit_Code_For_Create as Unit_Code_For_Create,  isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] , isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0)  as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item, isnull ([Cash_KG],0)+isnull([CD_KG],0)+isnull([CR_KG],0)+isnull([SO_KG],0) as Total_KG,  isnull ([Cash_LTR],0)+isnull([CD_LTR],0)+isnull([CR_LTR],0)+isnull([SO_LTR],0) as Total_LTR from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code, case when  TSPL_BOOKING_MATSER.Booking_Type in ('FN','PS','UP') then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end+'_KG' as  Booking_Type_KG,	case when  TSPL_BOOKING_MATSER.Booking_Type in ('FN','PS','UP') then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end+'_LTR'	as  Booking_Type_LTR, case when  TSPL_BOOKING_MATSER.Booking_Type in ('FN','PS','UP') then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end as Booking_Type,TSPL_BOOKING_DETAIL.Booking_Qty ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name, Convert (decimal(18,2), round( ((isnull (Source_Conv.Conversion_Factor,0)/ nullif ( isnull (Target_Conv.Conversion_Factor,0),0) ) * TSPL_BOOKING_DETAIL.Booking_Qty),2,1)) as Booking_Qty_Ltr, case when isnull (Target_Conv.Conversion_Factor,0) = 0 then Convert (decimal(18,2),  round(((isnull (Source_Conv.Conversion_Factor,0)/ nullif ( isnull (Target_Conv_KG.Conversion_Factor,0),0) ) * TSPL_BOOKING_DETAIL.Booking_Qty),2,1)) end as Booking_Qty_Kg ,  TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,'LTR' as Unit_Code,TSPL_BOOKING_DETAIL.Unit_Code as Unit_Code_For_Create,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  Left outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on 	Source_Conv.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and 	Source_Conv.UOM_Code = TSPL_BOOKING_DETAIL.Unit_code Left Outer Join TSPL_ITEM_UOM_DETAIL as Target_Conv on   Target_Conv.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_Conv.Uom_code = 'LTR'  Left Outer Join TSPL_ITEM_UOM_DETAIL as Target_Conv_KG on   Target_Conv_KG.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_Conv_KG.Uom_code = 'KG'  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)   and " + strCondition + " " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivotss  pivot  (  max(Booking_Qty_Kg) for Booking_Type_KG in ([Cash_KG],[CD_KG],[CR_KG],[SO_KG])   )as pivots  pivot  (  max(Booking_Qty_Ltr) for Booking_Type_LTR in ([Cash_LTR],[CD_LTR],[CR_LTR],[SO_LTR])   )as pivotsLtr ) XFinal Group by  XFinal.route_no, XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_Code_For_Create =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )  XXXXX  ) XXXXX2 group by Document_Date, route_no  " &
                     " Union All  " &
                     "  select max (XXXXX.Date) as Document_Date, max([Customer Code]) as  Cust_Code, max ([Customer Name]) as Customer_Name , XXXXX.[Route Code] as route_no ,max( XXXXX.[Route Name]) as Route_Desc,'' as Zone_Code,'' as Zone_Name,'ItemCode' as Item_Code,'' as Alies_Name,0 as CR,0 as CD,0 as SO,0 as Cash,0 as Total,0  as CrateQty_New , 0 as PendingPcsQty_New,0 as Amount_with_Tax, '6' as  DataSNO, '' as    Unit_code  , 1 as LR_PQ_ES     , sum (OpencrateQty) as OpencrateQty , sum (CrateQtyRecd) as CrateQtyRecd , sum (CrateOutQty) as CrateOutQty ,sum (CrateAdjQty) as CrateAdjQty , sum (CrateQtyClosing) as CrateQtyClosing ,'' as Total_Scheme_Star, 0 as Qty_in_KG , 0 as Qty_in_Ltr  from (   Select convert(varchar,Doc_Date,103) as Date,Route_No as [Route Code],Route_Desc as [Route Name], Vehicle_Code as [Vehicle Code],Vehicle_Name as [Vehicle Name], 'XCrates Route Details' as [Customer Code],'XCrates Route Details' as [Customer Name],Customer_Phone as [Customer Phone], ZSM as [ZSM Code], ZSM_Name as [ZSM Name],ZSM_Phone as [ZSM Phone],ASM as [ASM Code],ASM_NAME as [ASM Name],ASM_Phone as [ASM Phone],ASO as [ASO Code],ASO_Name as [ASO Name],ASO_Phone as [ASO Phone], OpencrateQty,OpenJaaliQty,OpenBoxQty,OpenCanQty, CrateQtyRecd, JaaliQtyRecd,BoxQtyRecd, CanQtyRecd  ,CrateOutQty, jaaliOutQty,boxOutQty,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty,CanAdjQty, OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty as CrateQtyClosing, OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty as CanQtyClosing , OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty as JaaliQtyClosing, OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty as BoxQtyClosing, (OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty)* 0 as CrateValueClosing, (OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty)* 0 as JaaliValueClosing, (OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty)* 0 as BoxValueClosing ,  (OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty)* 0 as CanValueClosing ,OpencrateQtySum,OpenCanQtySum ,OpencrateQtySumOfVehicleWise,OpencanQtySumOfVehicleWise  from (Select  case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySum, case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySum, case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySumOfVehicleWise, case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySumOfVehicleWise, CTETemp.RowNo,CTETemp.Doc_Date, CTETemp.Route_No,CTETemp.Route_Desc ,CTETemp.Vehicle_Code,CTETemp.Vehicle_Name ,CTETemp.Customer_Code,CTETemp.Customer_Name, CTETemp.Customer_Phone, CTETemp.ZSM,CTETemp.ZSM_NAME,CTETemp.ZSM_Phone,CTETemp.ASM,CTETemp.ASM_NAME,CTETemp.ASM_Phone,CTETemp.ASO,CTETemp.ASO_NAME,CTETemp.ASO_Phone, CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) as OpencrateQty,  CTETemp.OpenJaaliQty+ISNULL(CT1.JaaliQtyClosing,0) as OpenJaaliQty,  CTETemp.OpenBoxQty+ISNULL(CT1.BoxQtyClosing,0) as OpenBoxQty, CTETemp.OpenCanQty+ISNULL(CT1.CanQtyClosing,0) as OpenCanQty,  CTETemp.CrateQtyRecd, CTETemp.JaaliQtyRecd, CTETemp.BoxQtyRecd,CTETemp.CanQtyRecd, CTETemp.CrateOutQty, CTETemp.jaaliOutQty, CTETemp.boxOutQty, CTETemp.CanOutQty, CTETemp.CrateAdjQty , CTETemp.JaaliAdjQty  , CTETemp.BoxAdjQty, CTETemp.CanAdjQty  from CTETemp LEFt OUTER JOIN CTETemp CT1 ON  CT1.Customer_Code=CTETemp.Customer_Code and  CT1.Vehicle_Code = CTETemp.Vehicle_Code  and CT1.Route_No  = CTETemp.Route_No  AND (CTETemp.RowNo-CT1.RowNo)=1 ) ZZZ   ) XXXXX  " + isTrueFalse + " Group by  XXXXX.[Route Code]   " &
                     "  " &
                     " )XXXXXFinal left Outer Join " &
                     " ( " &
                     "  select XXXXXFinal.DataSNO ,XXXXXFinal.route_no,XXXXXFinal.Cust_Code , Count (Item_Code) as No_Of_Item  from ( " &
                     "  select '1' as  DataSNO, XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CrateQty_New as CrateQty_New , XXXXX.PendingPcsQty_New as PendingPcsQty_New,isnull (XXXXX.Amount_with_Tax,0) as Amount_with_Tax,'' as  Unit_code from (  Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty , Convert (int,CrateQty) as CrateQty_New , (XXXFinal.Total) - ((StockPcs.Conversion_Factor * Convert (int,CrateQty))) as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when  TSPL_BOOKING_MATSER.Booking_Type in ('FN','PS','UP') then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.OldName as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots  Union All  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   isnull([Cash_Scheme],0) as [Cash_Scheme] ,isnull([CD_Scheme],0) as [CD_Scheme] ,isnull([CR_Scheme],0) as [CR_Scheme] ,isnull([SO_Scheme],0) as [SO_Scheme] ,isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when  TSPL_BOOKING_MATSER.Booking_Type in ('FN','PS','UP') then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  " + whr + "  )  Final   pivot  (  max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code   ) XXXXX  " &
                     " Union All " &
                     " select '2' as  DataSNO, XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, XXXXX.Unit_code as  Unit_code from ( " &
                     " Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity,(StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty ,  'Scheme '+XXXFinal.Alies_Name as  Alies_Name_on_Name from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select XFinal.Document_Date,max (XFinal.Comp_Code) as Comp_Code,XFinal.Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,Zone_Code as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code,0 as Cash, 0 as CD, 0 as CR,0 as SO,0 as  Total,   [Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme],isnull ([Cash_Scheme],0)+isnull([CD_Scheme],0)+isnull([CR_Scheme],0)+isnull([SO_Scheme],0) as Total_Scheme,Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when  TSPL_BOOKING_MATSER.Booking_Type in ('FN','PS','UP') then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end+'_Scheme' as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No   left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code   left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code   left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code   where Scheme_Item = 'Y' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "   )  Final   pivot  (   max(Booking_Qty) for Booking_Type in ([Cash_Scheme],[CD_Scheme],[CR_Scheme],[SO_Scheme])   )as pivots  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code   ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code " &
                     " ) XXXXX " &
                     " Union All " &
                     " select '3' as  DataSNO, XXXXX.Document_Date, 'Route Total' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax, '' as  Unit_code from ( " &
                     " Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when  TSPL_BOOKING_MATSER.Booking_Type in ('FN','PS','UP') then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name,TSPL_BOOKING_DETAIL.Booking_Qty, TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,TSPL_BOOKING_DETAIL.Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) " + whr + "  )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by  XFinal.route_no, XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )   XXXXX  " &
                     "   " &
                     " Union all  " &
                     " select '22' as  DataSNO,  XXXXX.Document_Date, XXXXX.Cust_Code as Cust_Code,'Customer Leakage' as Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax , XXXXX.Unit_code as  Unit_code  from ( Select Document_Date,Cust_Code, max(Customer_Name) as Customer_Name ,Route_No,max(Route_Desc) as Route_Desc,Zone_Code,max(Zone_Name) as Zone_Name  , Item_Code, max (Alies_Name) as Alies_Name , sum (CR) as CR,sum (CD) as CD,Sum(SO) as SO ,sum (Cash) as Cash, sum(Total) as Total ,sum( CrateQty_New) as CrateQty_New,sum( PendingPcsQty_New) as PendingPcsQty_New, sum (Amount_with_Tax) as Amount_with_Tax,Unit_code,sum (LR_PQ_ES) as LR_PQ_ES  from ( Select convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 0 as CR,0 as CD,0 as SO,0 as Cash,0 as Total,0  as CrateQty_New , 0 as PendingPcsQty_New,0 as Amount_with_Tax, 'PCS' as  Unit_code , Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty) as LR_PQ_ES from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='PCS' inner join ( Select distinct TSPL_BOOKING_DETAIL.Cust_Code  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No   left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  and " + strCondition + " " + whr + " ) TBL_VALID_Cust on TBL_VALID_Cust.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code where  TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others') and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1  and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) and TSPL_SD_SHIPMENT_HEAD.status = 1  " + whrShipment + "  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no, XFinal.Cust_Code,XFinal.Item_Code,XFinal.Unit_code ) XXXXX  " + isTrueFalse + "  " &
                     " Union all  " &
                     " select  '4' as  DataSNO,  XXXXX.Document_Date, XXXXX.Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,XXXXX.Item_Code,XXXXX.Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,0  as CrateQty_New , 0 as PendingPcsQty_New,XXXXX.Amount_with_Tax,  XXXXX.Unit_code as  Unit_code from ( Select Document_Date, 'Total Route Leakage' as Cust_Code, max(Customer_Name) as Customer_Name ,Route_No,max(Route_Desc) as Route_Desc,Zone_Code,max(Zone_Name) as Zone_Name  , Item_Code, max (Alies_Name) as Alies_Name , sum (CR) as CR,sum (CD) as CD,Sum(SO) as SO ,sum (Cash) as Cash, sum(Total) as Total ,sum( CrateQty_New) as CrateQty_New,sum( PendingPcsQty_New) as PendingPcsQty_New, sum (Amount_with_Tax) as Amount_with_Tax,Unit_code,sum (LR_PQ_ES) as LR_PQ_ES  from ( Select convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 0 as CR,0 as CD,0 as SO,0 as Cash,0 as Total,0  as CrateQty_New , 0 as PendingPcsQty_New,0 as Amount_with_Tax, 'PCS' as  Unit_code  , Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty) as LR_PQ_ES from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='PCS' inner join ( Select distinct TSPL_BOOKING_DETAIL.Cust_Code  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  where Scheme_Item = 'N' and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  and " + strCondition + "  " + whr + " ) TBL_VALID_Cust on TBL_VALID_Cust.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code where  TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others') and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1  and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103) and TSPL_SD_SHIPMENT_HEAD.status = 1  " + whrShipment + "  ) XFinal Group by XFinal.Document_Date ,XFinal.Zone_Code,XFinal.route_no,XFinal.Item_Code,XFinal.Unit_code ) XXXXX  " + isTrueFalse + " " &
                     " Union all  " &
                     " Select  max(XXXXX2.DataSNO) as  DataSNO, XXXXX2.Document_Date,max( XXXXX2.Cust_Code) as Cust_Code ,max (XXXXX2.Customer_Name) as  Customer_Name,XXXXX2.route_no,max(XXXXX2.Route_Desc) as Route_Desc,max (XXXXX2.Zone_Code) as Zone_Code,max(XXXXX2.Zone_Name) as Zone_Name, max(XXXXX2.Item_Code) as Item_Code,max(XXXXX2.Alies_Name) as Alies_Name,sum(XXXXX2.CR) as CR ,Sum(XXXXX2.CD) as CD,sum(XXXXX2.SO) as SO,sum(XXXXX2.Cash) as Cash,sum(XXXXX2.Total) as Total,sum(XXXXX2.CrateQty_New)  as CrateQty_New , sum(XXXXX2.PendingPcsQty_New) as PendingPcsQty_New,sum(XXXXX2.Amount_with_Tax) as Amount_with_Tax, max(XXXXX2.Unit_code) as  Unit_code   from (  select  XXXXX.Document_Date, 'Total Route Summary' as Cust_Code,XXXXX.Customer_Name,XXXXX.route_no,XXXXX.Route_Desc,XXXXX.Zone_Code,XXXXX.Zone_Name,'' as Item_Code,'' as Alies_Name,XXXXX.CR,XXXXX.CD,XXXXX.SO,XXXXX.Cash,XXXXX.Total,XXXXX.CrateQty_New  as CrateQty_New , PendingPcsQty as PendingPcsQty_New,XXXXX.Amount_with_Tax, '5' as  DataSNO, '' as  Unit_code, 0 as  LR_PQ_ES from (    Select XXXFinal.*, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) as TotalCrateCapicity, (StockPcs.Conversion_Factor * CEILING_Crate_Qty) -  (XXXFinal.Total) as PendingPcsQty  ,Convert (int,CrateQty) as CrateQty_New , (XXXFinal.Total) - ((StockPcs.Conversion_Factor * Convert (int,CrateQty))) as PendingPcsQty_New, (StockPcs.Conversion_Factor * Convert (int,CrateQty)) as TotalCrateCapicity_New  from (  Select XXFinal.*, (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as CrateQty , CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.Total)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_Crate_Qty  from (  select max(XFinal.Document_Date) as Document_Date,max (XFinal.Comp_Code) as Comp_Code,max(XFinal.Cust_Code) as Cust_Code, max( XFinal.Customer_Name ) as Customer_Name , XFinal.route_no, max(XFinal.Route_Desc) as Route_Desc,max(Zone_Code) as Zone_Code ,max(Zone_Name) as Zone_Name ,max(XFinal.Vehicle_Code) as Vehicle_Code , XFinal.Item_Code,max (XFinal.Alies_Name) as Alies_Name , XFinal.Unit_code,  sum (XFinal.Cash_Scheme) as Cash_Scheme, sum(XFinal.CD_Scheme)  as CD_Scheme, sum(XFinal.CR_Scheme) as CR_Scheme , sum (XFinal.SO_Scheme) as SO_Scheme, sum (XFinal.Total_Scheme) as Total_Scheme,  sum (XFinal.Cash) as Cash, sum(XFinal.CD)  as CD, sum(XFinal.CR) as CR , sum (XFinal.SO) as SO, sum (XFinal.Total) + sum (XFinal.Total_Scheme) as Total ,sum (XFinal.Amount_with_Tax)  as Amount_with_Tax , max (Scheme_Item ) as Scheme_Item from (  select Document_No,Document_Date, Comp_Code,From_Screen_Code,Cust_Code,Customer_Name,Route_No,Route_Desc,Zone_Code,Zone_Name,Vehicle_Code,Loc_Code,Item_Code,Alies_Name, Unit_Code, isnull([Cash],0) as [Cash],isnull([CD],0) as [CD],isnull([CR],0) as [CR] ,isnull([SO],0) as [SO] ,isnull ([Cash],0)+isnull([CD],0)+isnull([CR],0)+isnull([SO],0) as Total,0 as [Cash_Scheme],0 as [CD_Scheme],0 as [CR_Scheme],0 as [SO_Scheme],0 as Total_Scheme , Amount_with_Tax,Scheme_Item from (  Select TSPL_BOOKING_MATSER.Document_No,Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_BOOKING_MATSER.From_Screen_Code,case when  TSPL_BOOKING_MATSER.Booking_Type in ('FN','PS','UP') then 'Cash' else TSPL_BOOKING_MATSER.Booking_Type end as Booking_Type ,TSPL_BOOKING_DETAIL.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_BOOKING_DETAIL.Loc_Code, TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name, Convert (decimal(18,2),(isnull (Source_Conv.Conversion_Factor,0)/ nullif ( isnull (Target_Conv.Conversion_Factor,0),0) ) * TSPL_BOOKING_DETAIL.Booking_Qty) as Booking_Qty,  TSPL_BOOKING_DETAIL.Price_with_Tax,Case when TSPL_BOOKING_MATSER.Booking_Type in ('CR','CD','SO') then 0 else TSPL_BOOKING_DETAIL.Amount_with_Tax end as Amount_with_Tax,'LTR' as Unit_Code,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name, TSPL_BOOKING_DETAIL.Scheme_Item  from TSPL_BOOKING_DETAIL  left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_DETAIL.Document_No = TSPL_BOOKING_MATSER.Document_No  left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code  left Outer Join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No  left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code  Left outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on 	Source_Conv.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and 	Source_Conv.UOM_Code = TSPL_BOOKING_DETAIL.Unit_code Left Outer Join TSPL_ITEM_UOM_DETAIL as Target_Conv on   Target_Conv.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and Target_Conv.Uom_code = 'LTR' where Scheme_Item = 'N'  and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY not in ('Others')  and Convert(date, Document_Date,103) =  Convert(date, '" + TSP_Date.Value + "',103)  and " + strCondition + "  " + whr + " )  Final  pivot  (  max(Booking_Qty) for Booking_Type in ([Cash],[CD],[CR],[SO])   )as pivots    ) XFinal Group by  XFinal.route_no, XFinal.Item_Code,Unit_code  ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_code =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )  XXXXX  ) XXXXX2 group by Document_Date, route_no   " &
                     " Union All  " &
                     " select max (XXXXX.Date) as Document_Date, max([Customer Code]) as  Cust_Code, max ([Customer Name]) as Customer_Name , XXXXX.[Route Code] as route_no ,max( XXXXX.[Route Name]) as Route_Desc, '' as Zone_Code,'' as Zone_Name,'ItemCode' as Item_Code,'' as Alies_Name,0 as CR,0 as CD,0 as SO,0 as Cash,0 as Total,0  as CrateQty_New , 0 as PendingPcsQty_New,0 as Amount_with_Tax, '6' as  DataSNO, '' as    Unit_code    from (   Select convert(varchar,Doc_Date,103) as Date,Route_No as [Route Code],Route_Desc as [Route Name], Vehicle_Code as [Vehicle Code],Vehicle_Name as [Vehicle Name], 'XCrates Route Details' as [Customer Code],Customer_Name as [Customer Name],Customer_Phone as [Customer Phone], ZSM as [ZSM Code], ZSM_Name as [ZSM Name],ZSM_Phone as [ZSM Phone],ASM as [ASM Code],ASM_NAME as [ASM Name],ASM_Phone as [ASM Phone],ASO as [ASO Code],ASO_Name as [ASO Name],ASO_Phone as [ASO Phone], OpencrateQty,OpenJaaliQty,OpenBoxQty,OpenCanQty, CrateQtyRecd, JaaliQtyRecd,BoxQtyRecd, CanQtyRecd  ,CrateOutQty, jaaliOutQty,boxOutQty,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty,CanAdjQty, OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty as CrateQtyClosing, OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty as CanQtyClosing , OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty as JaaliQtyClosing, OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty as BoxQtyClosing, (OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty)* 0 as CrateValueClosing, (OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty)* 0 as JaaliValueClosing, (OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty)* 0 as BoxValueClosing ,  (OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty)* 0 as CanValueClosing ,OpencrateQtySum,OpenCanQtySum ,OpencrateQtySumOfVehicleWise,OpencanQtySumOfVehicleWise  from (Select  case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySum, case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySum, case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySumOfVehicleWise, case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySumOfVehicleWise, CTETemp.RowNo,CTETemp.Doc_Date, CTETemp.Route_No,CTETemp.Route_Desc ,CTETemp.Vehicle_Code,CTETemp.Vehicle_Name ,CTETemp.Customer_Code,CTETemp.Customer_Name, CTETemp.Customer_Phone, CTETemp.ZSM,CTETemp.ZSM_NAME,CTETemp.ZSM_Phone,CTETemp.ASM,CTETemp.ASM_NAME,CTETemp.ASM_Phone,CTETemp.ASO,CTETemp.ASO_NAME,CTETemp.ASO_Phone, CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) as OpencrateQty,  CTETemp.OpenJaaliQty+ISNULL(CT1.JaaliQtyClosing,0) as OpenJaaliQty,  CTETemp.OpenBoxQty+ISNULL(CT1.BoxQtyClosing,0) as OpenBoxQty, CTETemp.OpenCanQty+ISNULL(CT1.CanQtyClosing,0) as OpenCanQty,  CTETemp.CrateQtyRecd, CTETemp.JaaliQtyRecd, CTETemp.BoxQtyRecd,CTETemp.CanQtyRecd, CTETemp.CrateOutQty, CTETemp.jaaliOutQty, CTETemp.boxOutQty, CTETemp.CanOutQty, CTETemp.CrateAdjQty , CTETemp.JaaliAdjQty  , CTETemp.BoxAdjQty, CTETemp.CanAdjQty  from CTETemp LEFt OUTER JOIN CTETemp CT1 ON  CT1.Customer_Code=CTETemp.Customer_Code and  CT1.Vehicle_Code = CTETemp.Vehicle_Code  and CT1.Route_No  = CTETemp.Route_No  AND (CTETemp.RowNo-CT1.RowNo)=1 ) ZZZ   ) XXXXX  " + isTrueFalse + "  Group by  XXXXX.[Route Code]   " &
                     "   " &
                     "   " &
                     "   " &
                     " )XXXXXFinal  group by XXXXXFinal.route_no, XXXXXFinal.Cust_Code, XXXXXFinal.DataSNO   " &
                     " ) " &
                     " TBL_Count on TBL_Count.DataSNO = XXXXXFinal.DataSNO and TBL_Count.route_no = XXXXXFinal.route_no and TBL_Count.Cust_Code = XXXXXFinal.Cust_Code " &
                     " left Outer  Join  TSPL_ITEM_MASTER  on  TSPL_ITEM_MASTER.Item_Code = XXXXXFinal.Item_Code " &
                     " Order By XXXXXFinal.route_no,XXXXXFinal.Cust_Code,XXXXXFinal.DataSNO , case when TSPL_ITEM_MASTER.SKU_Seq = 0 then 10000 else  TSPL_ITEM_MASTER.SKU_Seq end  "

            Dim dtMain As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dtMain IsNot Nothing And dtMain.Rows.Count > 0 Then
                Dim subPath As String = "C:\\ERPTempFolder"
                Dim IsExists As Boolean = System.IO.Directory.Exists(subPath)
                If (IsExists = False) Then
                    System.IO.Directory.CreateDirectory(subPath)
                End If
                subPath += "\\" & clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddhhmmsss") & ".Txt"
                IsExists = System.IO.File.Exists(subPath)
                If IsExists Then
                    System.IO.File.Delete(subPath)
                End If
                'WriteDataToFile(dtMain, "C:\ERPTempFolder\MyTestfile.Txt")
                WriteDataToFile(dtMain, subPath, True)
                Process.Start(subPath)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
        End If
    End Sub

    Private Sub TxtRouteABS__My_Click(sender As Object, e As EventArgs) Handles txtRouteABS._My_Click
        If chkShowEarlyRoute.Checked = True Then
            strQry = "select Route_No  as [Code],Route_Desc as [Name] from TSPL_ROUTE_MASTER where IsEarlyRoute=1"
        Else
            strQry = "select Route_No  as [Code],Route_Desc as [Name] from TSPL_ROUTE_MASTER where IsEarlyRoute=0"
        End If
        txtRouteABS.arrValueMember = clsCommon.ShowMultipleSelectForm("txtRoutABS@Master", strQry, "Code", "Name", txtRouteABS.arrValueMember, txtRouteABS.arrDispalyMember)
    End Sub

    Private Sub TxtRouteNetSales__My_Click(sender As Object, e As EventArgs) Handles txtRouteNetSales._My_Click
        If chkConsiderShowEarlyRoute.Checked = True Then
            If chkShowEarlyRoute.Checked = True Then
                strQry = "select Route_No  as [Code],Route_Desc as [Name] from TSPL_ROUTE_MASTER where IsEarlyRoute=1"
            Else
                strQry = "select Route_No  as [Code],Route_Desc as [Name] from TSPL_ROUTE_MASTER where IsEarlyRoute=0"
            End If
        Else
            strQry = "select Route_No  as [Code],Route_Desc as [Name] from TSPL_ROUTE_MASTER "
        End If

        txtRouteNetSales.arrValueMember = clsCommon.ShowMultipleSelectForm("txtRou1NS@Master", strQry, "Code", "Name", txtRouteNetSales.arrValueMember, txtRouteNetSales.arrDispalyMember)
    End Sub

    Private Sub CmbCustomerCategory_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cmbCustomerCategory.SelectedIndexChanged
        If clsCommon.CompairString(cmbCustomerCategory.Text, "Institution CR") = CompairStringResult.Equal OrElse clsCommon.CompairString(cmbCustomerCategory.Text, "Institution SO") = CompairStringResult.Equal Then
            btnIDSPrintABS.Enabled = True
        Else
            btnIDSPrintABS.Enabled = False
        End If
    End Sub

    Private Sub txtInvMultiCust__My_Click(sender As Object, e As EventArgs) Handles txtInvMultiCust._My_Click
        strQry = " select Cust_Code as [Code] , Customer_Name as [Name] from TSPL_CUSTOMER_MASTER "
        txtInvMultiCust.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultCustomer", strQry, "Code", "Name", txtInvMultiCust.arrValueMember, txtInvMultiCust.arrDispalyMember)
    End Sub

    Private Sub btnPrintInvoice_Click(sender As Object, e As EventArgs) Handles btnPrintInvoice.Click
        Try
            PrintInvoive()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub PrintInvoive()
        Try
            Dim Qry As String = Nothing
            Dim frmCRV As New frmCrystalReportViewer()
            Dim objMultPrintInvoice As New FrmPrintFreshInvoice
            Dim whrcls As String = " where Main_Final.TaxableNonTaxable='NT' "

            If clsCommon.myLen(txtInvFromDate.Value) > 0 AndAlso clsCommon.myLen(txtInvToDate.Value) Then
                whrcls += " And Main_Final.Invoice_Date>='" + txtInvFromDate.Value + "' And Main_Final.Invoice_Date<='" + txtInvToDate.Value + "' "
            Else
                Throw New Exception("Fill From Date and To Date")
            End If

            If clsCommon.myLen(txtInvMultiCust.arrValueMember) > 0 Then
                whrcls += " And  Main_Final.cust_Code In (" + clsCommon.GetMulcallString(txtInvMultiCust.arrValueMember) + ")"
            End If

            If txtMultItemCodeInv.arrValueMember.Count > 0 Then
                whrcls += " And Main_Final.Structure_Code IN (" + clsCommon.GetMulcallString(txtMultItemCodeInv.arrValueMember) + ")"
            End If

            If clsCommon.myLen(txtInvFromDate.Value) > 0 AndAlso clsCommon.myLen(txtInvToDate.Value) Then
                Qry = objMultPrintInvoice.PrintInvoiceForTruckSheetReport(txtInvFromDate.Value, txtInvToDate.Value, whrcls)
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptNonTaxableInvoice", "Bill of Supply", Nothing, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                frmCRV = Nothing
            Else
                myMessages.blankValue("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub txtMultItemCodeInv__My_Click(sender As Object, e As EventArgs) Handles txtMultItemCodeInv._My_Click
        Try
            Dim strQry As String = "Select TSPL_STRUCTURE_MASTER.Structure_Code As [Structure Code],TSPL_STRUCTURE_MASTER.Structure_Descq As [Description] from TSPL_STRUCTURE_MASTER
                                 Inner Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Structure_Code=TSPL_STRUCTURE_MASTER.Structure_Code Where TSPL_ITEM_MASTER.IsTaxable=0 Group By TSPL_STRUCTURE_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq "
            txtMultItemCodeInv.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultItemCodeInv", strQry, "Structure Code", "Description", txtMultItemCodeInv.arrValueMember, txtMultItemCodeInv.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
