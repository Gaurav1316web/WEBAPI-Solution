Imports common
Imports XpertERPEngine

Public Class FrmLeakageBreakage
    Inherits FrmMainTranScreen


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmLeakageBreakage)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Private Sub FrmLeakageBreakage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
        LoadCustomer()
        LoadItem()
        LoadRoute()
        Loadlocation()
        chklocAll.IsChecked = True
        chkCustAll.IsChecked = True
        chkRouteAll.IsChecked = True
        chkItemAll1.IsChecked = True
        rdbSku.IsChecked = True
        SetUserMgmtNew()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        print()
    End Sub
    Sub Loadlocation()     
        cbglocation.DataSource = clsLocation.GetLocationSegments()
        cbglocation.ValueMember = "Code"
        cbglocation.DisplayMember = "Name"
    End Sub

    Sub LoadCustomer()
        Dim qry As String = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name],Customer_Class as [Customer Class] from TSPL_CUSTOMER_MASTER"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)

        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Code"
    End Sub

    Sub LoadItem()
        Dim qry As String = "select Item_Code as [Item Code],Item_Desc as [Item Description] from TSPL_ITEM_MASTER"
        cbgItem1.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem1.ValueMember = "Item Code"
        cbgItem1.DisplayMember = "Item Description"
    End Sub
    Sub LoadRoute()
        Dim qry As String = "select route_no,route_desc from TSPL_ROUTE_MASTER order by route_no"
        cbgroute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgroute.ValueMember = "route_no"
        cbgroute.DisplayMember = "route_desc"

    End Sub
    Private Sub chklocAll_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chklocAll.ToggleStateChanged
        cbglocation.Enabled = Not chklocAll.IsChecked
    End Sub
    Private Sub chkCustAll_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustAll.IsChecked
    End Sub
  
    Sub print()
        Try

            'Dim locationArr As ArrayList
            'Dim RouteArr As ArrayList
            'Dim CustomerArr As ArrayList
            'Dim CategoryArr As ArrayList
            'Dim ChannelArr As ArrayList
            'Dim TemplateArr As ArrayList
            'Dim CompanyArr As ArrayList

            If chklocSelect.IsChecked = True AndAlso cbglocation.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one location")
                Return
            End If
            If chkChkSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Customer")
                Return
            End If
            If chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Route")
                Return
            End If
            If chkItemSelect1.IsChecked = True AndAlso cbgItem1.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Item")
                Return
            End If






            Dim qry As String = ""
            Dim qry1 As String = ""
            Dim finalqry As String = ""
            Dim TDMCOdecolumn As String = String.Empty
            Dim group1 As String = String.Empty
            'Dim additional As String = String.Empty
            Dim strOrderColumn As String = String.Empty
            Dim strOrderBy As String = String.Empty
            Dim postingdata As String = String.Empty
            Dim strClass As String = String.Empty
            Dim strSQGroupLK As String = String.Empty
            Dim strSQGroupBk As String = String.Empty


            'If rdoalldata.IsChecked = True Then
            '    strPost = ""
            '    strReturnPost = ""
            '    strTransPost = ""
            'ElseIf rdoposted.IsChecked = True Then
            '    strPost = " and Is_Post='Y' "
            '    strReturnPost = " and TSPL_SALE_RETURN_HEAD.Is_Post='Y'"
            '    strTransPost = " and Post='Y'    "
            'End If


            '-----------------------------------------------------------------------------------------

            If rdbSku.IsChecked = True Then
                strSQGroupLK = "a.Item_Code + '_Lk'  + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP ) ) +  ' ) ' "
                strSQGroupBk = "a.Item_Code + '_Bk'  + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP ) ) +  ' ) ' "
                strOrderColumn = " TSPL_ITEM_MASTER.Sku_Seq"
                strOrderBy = " where xxx.QTY<>0  Order By xxx.OrderBy"

            ElseIf rdbFlavour.IsChecked = True Then
                strSQGroupLK = "TSPL_ITEM_DETAILS.Class_Desc + '_Lk' +'('+convert(varchar,TSPL_ITEM_MASTER.Flavour_Seq)+')'  + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP) ) +  ' ) ' "
                strSQGroupBk = "TSPL_ITEM_DETAILS.Class_Desc + '_Bk' +'('+convert(varchar,TSPL_ITEM_MASTER.Flavour_Seq)+')'  + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP) ) +  ' ) ' "

                strOrderColumn = " TSPL_ITEM_MASTER.Flavour_Seq"
                strOrderBy = " Order By xxx.OrderBy"
                strClass = "Flavour"
            ElseIf rdbPack.IsChecked = True Then
                strSQGroupLK = "TSPL_ITEM_DETAILS.Class_Desc + '_Lk' +'('+convert(varchar,TSPL_ITEM_MASTER.Pack_Seq)+')'  + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP) ) +  ' ) ' "
                strSQGroupBk = "TSPL_ITEM_DETAILS.Class_Desc + '_Bk' +'('+convert(varchar,TSPL_ITEM_MASTER.Pack_Seq)+')'  + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP) ) +  ' ) ' "

                strOrderColumn = " TSPL_ITEM_MASTER.Pack_Seq"
                strOrderBy = " Order By xxx.OrderBy"
                strClass = "Size"
            End If

            qry = "select mrp *  Conversion_Factor as MRP,TSPL_ADJUSTMENT_DETAIL.Item_Code,Loc_Desc as Loc,TSPL_SALE_INVOICE_HEAD.Route_No, " & _
            "TSPL_ROUTE_MASTER.Route_Desc,TSPL_ADJUSTMENT_HEADER.Adjustment_No as DocNo,Adjustment_Date as Docdate, " & _
            "TSPL_ADJUSTMENT_HEADER.Customer_CODE as CustCode,TSPL_CUSTOMER_MASTER.Customer_Name as CustName, " & _
            "LeakageQty/Conversion_Factor as Leak,Breakage/Conversion_Factor as breakage from TSPL_ADJUSTMENT_HEADER left outer join " & _
            "TSPL_ADJUSTMENT_DETAIL on " & _
            "TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No left outer join TSPL_SALE_INVOICE_HEAD on " & _
            "TSPL_ADJUSTMENT_HEADER.Document_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No left outer join TSPL_ROUTE_MASTER on " & _
            "TSPL_SALE_INVOICE_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_CUSTOMER_MASTER on " & _
            "TSPL_ADJUSTMENT_HEADER.Customer_CODE=TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_ITEM_UOM_DETAIL on " & _
            "TSPL_ADJUSTMENT_DETAIL.item_code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ADJUSTMENT_DETAIL.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
            "where (LeakageQty > 0 or Breakage > 0)  and Reference_Document <> 'Load out/Transfer' "

            If chklocSelect.IsChecked = True Then
                qry += " and  TSPL_ADJUSTMENT_HEADER.Loc_Code in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
            End If
            If chkChkSelect.IsChecked = True Then
                qry += " and TSPL_ADJUSTMENT_HEADER.Customer_CODE in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            End If
            If chkItemSelect1.IsChecked = True Then
                qry += " and TSPL_ADJUSTMENT_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem1.CheckedValue) + ") "
            End If
            If chkRouteSelect.IsChecked = True Then
                qry += " and TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            qry += "union all " & _
            "select MRP *  Conversion_Factor as MRP,TSPL_TRANSFER_DETAIL.Item_Code,ToLoc_Desc as Loc,TSPL_TRANSFER_HEAD.Route_No,TSPL_ROUTE_MASTER.Route_Desc, " & _
            "TSPL_TRANSFER_HEAD.Transfer_No as DocNo,Transfer_Date as Docdate,'' as CustCode,'' as Custname,Leak/Conversion_Factor as Leak,Breakage/Conversion_Factor as breakage " & _
            "from TSPL_TRANSFER_HEAD left outer join TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join " & _
            "TSPL_ROUTE_MASTER on TSPL_TRANSFER_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_ITEM_UOM_DETAIL on " & _
            "TSPL_TRANSFER_DETAIL.item_code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_TRANSFER_DETAIL.Uom=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
            "where (Leak > 0 or Breakage > 0)  "

            If chklocSelect.IsChecked = True Then
                qry += " and  TSPL_TRANSFER_HEAD.To_Location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
            End If
            If chkChkSelect.IsChecked = True Then
                qry += " and  1=2 "
            End If
            If chkItemSelect1.IsChecked = True Then
                qry += " and TSPL_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem1.CheckedValue) + ") "
            End If
            If chkRouteSelect.IsChecked = True Then
                qry += " and TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            qry += "union all " & _
            "select MRP_Amt*  Conversion_Factor as mrp,TSPL_SALE_RETURN_DETAIL.Item_Code,Location_Desc as Loc,TSPL_SALE_RETURN_HEAD.Route_No, " & _
            "TSPL_ROUTE_MASTER.Route_Desc,TSPL_SALE_RETURN_HEAD.Sale_Return_No as DocNo,Sale_Return_Date as Docdate, " & _
            "TSPL_SALE_RETURN_HEAD.Cust_Code as CustCode,TSPL_CUSTOMER_MASTER.Customer_Name as CustName,Leak_Qty/Conversion_Factor as Leak, " & _
            "Burst_Qty/Conversion_Factor as breakage from TSPL_SALE_RETURN_HEAD left outer join TSPL_SALE_return_DETAIL on " & _
            "TSPL_SALE_RETURN_HEAD.Sale_Return_No=TSPL_SALE_RETURN_DETAIL.Sale_Return_No  left outer join " & _
            "TSPL_ROUTE_MASTER on TSPL_SALE_RETURN_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join " & _
            "TSPL_CUSTOMER_MASTER on TSPL_SALE_RETURN_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  left outer join " & _
            "TSPL_LOCATION_MASTER on TSPL_SALE_RETURN_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code left outer join " & _
            "TSPL_ITEM_UOM_DETAIL on TSPL_SALE_RETURN_DETAIL.item_code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
            "TSPL_SALE_RETURN_DETAIL.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code  where (Leak_Qty > 0 or Burst_Qty > 0) "

            If chklocSelect.IsChecked = True Then
                qry += " and  TSPL_SALE_RETURN_HEAD.Location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
            End If
            If chkChkSelect.IsChecked = True Then
                qry += " and TSPL_SALE_RETURN_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            End If
            If chkItemSelect1.IsChecked = True Then
                qry += " and TSPL_SALE_RETURN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem1.CheckedValue) + ") "
            End If
            If chkRouteSelect.IsChecked = True Then
                qry += " and TSPL_SALE_RETURN_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            qry += "union all " & _
            "select mrp *  Conversion_Factor as MRP ,TSPL_WH_BREAKAGE_DETAIL.Item_Code,Location_Desc as Loc,'' as Route_No,'' as Route_Desc, " & _
            "TSPL_WH_BREAKAGE_HEAD.Document_No as DocNo,Document_Date as Docdate,'' as CustCode,'' as CustName,Leakage_Qty/Conversion_Factor as Leak, " & _
            "Breakage_Qty/Conversion_Factor as breakage from TSPL_WH_BREAKAGE_HEAD left outer join TSPL_WH_BREAKAGE_DETAIL on " & _
            "TSPL_WH_BREAKAGE_HEAD.Document_No=TSPL_WH_BREAKAGE_DETAIL.Document_No   left outer join TSPL_LOCATION_MASTER on " & _
            "TSPL_WH_BREAKAGE_HEAD.Loc_Code=TSPL_LOCATION_MASTER.Location_Code left outer join TSPL_ITEM_UOM_DETAIL on  " & _
            "TSPL_WH_BREAKAGE_DETAIL.item_code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_WH_BREAKAGE_DETAIL.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
            "where (Leakage_Qty > 0 or Breakage_Qty > 0) "

            If chklocSelect.IsChecked = True Then
                qry += " and  TSPL_WH_BREAKAGE_HEAD.Loc_Code in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
            End If
            If chkChkSelect.IsChecked = True Then
                qry += " and  1=2 "
            End If
            If chkItemSelect1.IsChecked = True Then
                qry += " and TSPL_WH_BREAKAGE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem1.CheckedValue) + ") "
            End If
            If chkRouteSelect.IsChecked = True Then
                qry += " and 1=2"
            End If


            qry += "union all " & _
            "select MRP *  Conversion_Factor as MRP,TSPL_SRN_DETAIL.Item_Code,Location_Desc as Loc,'' as Route_No,'' as Route_Desc,TSPL_SRN_HEAD.SRN_No as DocNo, " & _
            "SRN_Date as Docdate,TSPL_SRN_HEAD.Vendor_Code as CustCode,TSPL_VENDOR_MASTER.Vendor_Name as CustName,Leak_Qty/Conversion_Factor as Leak, " & _
            "Burst_Qty/Conversion_Factor as breakage from TSPL_SRN_HEAD left outer join TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No left outer join " & _
            "TSPL_VENDOR_MASTER on TSPL_SRN_HEAD.Vendor_Code=TSPL_vendor_MASTER.Vendor_Code  left outer join TSPL_LOCATION_MASTER on " & _
            "TSPL_SRN_HEAD.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code left outer join TSPL_ITEM_UOM_DETAIL on " & _
            "TSPL_SRN_DETAIL.item_code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_SRN_DETAIL.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
            "where (Leak_Qty > 0 or Burst_Qty > 0)  "

            If chklocSelect.IsChecked = True Then
                qry += " and  TSPL_SRN_HEAD.Bill_To_Location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
            End If
            If chkChkSelect.IsChecked = True Then
                qry += " and  1=2 "
            End If
            If chkItemSelect1.IsChecked = True Then
                qry += " and TSPL_SRN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem1.CheckedValue) + ") "
            End If
            If chkRouteSelect.IsChecked = True Then
                qry += " and 1=2"
            End If


            qry1 = "select convert(decimal(18,2),Leak) as Leak,convert(decimal(18,2),breakage) as breakage,Loc,Route_No,Route_Desc,DocNo,Docdate,CustCode,CustName,a.Item_Code , " & _
            "" & strSQGroupLK & " as Lkgrouping," & strSQGroupBk & " as Bkgrouping," & strOrderColumn & " as OrderBy  " & _
            "from ( " & qry & " ) a left outer join TSPL_ITEM_MASTER on " & _
           " a.Item_Code=TSPL_ITEM_MASTER.Item_Code left outer join TSPL_ITEM_DETAILS on  " & _
           " a.Item_Code=TSPL_ITEM_DETAILS.Item_Code and TSPL_ITEM_DETAILS.Class_Name='" & strClass & "' "



            '-----------------for grid code----------------------
            Dim strItemCodestringLk As String = String.Empty
            Dim strItemCodestringBk As String = String.Empty
            Dim strItemCodeLk As String = String.Empty
            Dim strItemCodeBk As String = String.Empty
            Dim strMainItemCode As String = String.Empty
            Dim strmainItemCodeString As String = String.Empty
            Dim strPivot As String = String.Empty
            Dim strsumLk As String = String.Empty
            Dim strsumBk As String = String.Empty
            Dim itemcount As String = String.Empty

            If rdbSku.IsChecked = True Then
                itemcount = " select  distinct Lkgrouping,Bkgrouping,OrderBy  from (" + qry1 + ") abc order by OrderBy "

            ElseIf rdbFlavour.IsChecked = True Then
                itemcount = " select  distinct Lkgrouping,Bkgrouping,OrderBy  from (" + qry1 + ") abc order by OrderBy "

            ElseIf rdbPack.IsChecked = True Then
                itemcount = " select  distinct Lkgrouping,Bkgrouping,OrderBy  from (" + qry1 + ") abc order by OrderBy "
            End If


            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(itemcount)


            Dim arritem As New ArrayList
            'While dr.Read
            If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                For Each dr As DataRow In dt1.Rows
                    strItemCodeLk = CStr(dr(0).ToString())
                    strItemCodestringLk = strItemCodestringLk & "[" & strItemCodeLk & "]" & ","
                    strmainItemCodeString = strmainItemCodeString & "  isnull(" & "[" & strItemCodeLk & "]" & ",0)  " & "as  " & "[" & strItemCodeLk & "]" & ","

                    strItemCodeBk = CStr(dr(1).ToString())
                    strItemCodestringBk = strItemCodestringBk & "[" & strItemCodeBk & "]" & ","
                    strmainItemCodeString = strmainItemCodeString & "  isnull(" & "[" & strItemCodeBk & "]" & ",0)  " & "as  " & "[" & strItemCodeBk & "]" & ","
                    strMainItemCode = CStr(dr(0).ToString())

                    strsumLk = strsumLk & "  isnull(" & "[" & strItemCodeLk & "]" & ",0)" & "+"
                    strsumBk = strsumBk & "  isnull(" & "[" & strItemCodeBk & "]" & ",0)" & "+"
                Next
            End If
            ' End While


            If strItemCodeLk <> "" Then
                strItemCodestringLk = strItemCodestringLk.Substring(0, strItemCodestringLk.Length - 1)
                strItemCodestringBk = strItemCodestringBk.Substring(0, strItemCodestringBk.Length - 1)

                strmainItemCodeString = strmainItemCodeString.Substring(0, strmainItemCodeString.Length - 1)
                strsumLk = strsumLk.Substring(0, strsumLk.Length - 1)
                strsumBk = strsumBk.Substring(0, strsumBk.Length - 1)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If


            finalqry = "  select Loc,Route_No,Route_Desc,DocNo,Docdate,CustCode,CustName, " & _
            "" & strmainItemCodeString & " from  ( " & qry1 & ") aa  " & _
            "pivot (sum(leak) for Lkgrouping in (" & strItemCodestringLk & ")) as pvt1 " & _
            "pivot (sum(breakage) for Bkgrouping in (" & strItemCodestringBk & ")) as pvt2"

            Dim strmain As String = String.Empty

            'strmain = " select  comp_name as [Company Name],locdesc as [Depot], TDMName as Name,Vehicle_No,Route as [Route],routedesc as [Route Desc],invoiceno as [Invoice],InvoiceDate as [Invoice Date],cust as [Customer],custdesc as [Customer Desc] ,(" + strsum + ")as Total," & strmainItemCodeString & "  from(select Vehicle_No,Comp_Name,locDesc,TDMName,Route,routedesc,invoiceno,InvoiceDate,cust,custdesc,sum(Qty) as Qty,grouping from (" + finalqry + ")aaa group by aaa.grouping,aaa.Comp_Name,aaa.locDesc,aaa.TDMName,aaa.Route,aaa.routedesc,aaa.invoiceno,aaa.InvoiceDate,aaa.cust,aaa.custdesc,aaa.Vehicle_No) down pivot (SUM(qty) FOR grouping IN ( " & strItemCodestring & ")) AS pvt1 "


            Dim dt As DataTable

            dt = clsDBFuncationality.GetDataTable(finalqry)
            GV1.DataSource = Nothing
            GV1.Columns.Clear()
            GV1.Rows.Clear()
            GV1.GroupDescriptors.Clear()
            GV1.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If
            GV1.DataSource = dt
            SetGridFormationOFGV1()
            RadPageView1.Visible = True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub SetGridFormationOFGV1()
        Dim strItemCode As String = String.Empty

        GV1.TableElement.TableHeaderHeight = 40
        GV1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To GV1.Columns.Count - 1
            GV1.Columns(ii).ReadOnly = True
            GV1.Columns(ii).IsVisible = False
        Next

        GV1.Columns("Loc").IsVisible = True
        GV1.Columns("Loc").Width = 100
        GV1.Columns("Loc").HeaderText = "Location"

        GV1.Columns("Route_No").IsVisible = True
        GV1.Columns("Route_No").Width = 100
        GV1.Columns("Route_No").HeaderText = "Route"

        GV1.Columns("Route_Desc").IsVisible = True
        GV1.Columns("Route_Desc").Width = 100
        GV1.Columns("Route_Desc").HeaderText = "Route Desc"

        GV1.Columns("DocNo").IsVisible = True
        GV1.Columns("DocNo").Width = 100
        GV1.Columns("DocNo").HeaderText = "Doc No"

        GV1.Columns("Docdate").IsVisible = True
        GV1.Columns("Docdate").Width = 70
        GV1.Columns("Docdate").HeaderText = "Docdate"
        'GV1.Columns("Location").BestFit()

        GV1.Columns("CustCode").IsVisible = True
        GV1.Columns("CustCode").Width = 120
        GV1.Columns("CustCode").HeaderText = "Customer Code"
        ''GV1.Columns("Customer Group").BestFit()

        GV1.Columns("CustName").IsVisible = True
        GV1.Columns("CustName").Width = 120
        GV1.Columns("CustName").HeaderText = "Customer Name"
        ''GV1.Columns("Customer Type").BestFit()

        For ii As Integer = 7 To GV1.Columns.Count - 1
            strItemCode = GV1.Columns(ii).FieldName
            GV1.Columns("" & strItemCode & "").IsVisible = True
            GV1.Columns("" & strItemCode & "").Width = 80
            GV1.Columns("" & strItemCode & "").HeaderText = "" & strItemCode & ""
        Next


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 7

        For ii As Integer = intCount To GV1.Columns.Count - 1
            intCount = intCount + 1
            strItemCode = GV1.Columns(ii).FieldName
            Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item16)
        Next
        GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub chkItemAll1_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll1.ToggleStateChanged
        cbgItem1.Enabled = Not chkItemAll1.IsChecked
    End Sub

    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click

    End Sub

    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If GV1.Rows.Count > 0 Then
            ExporttoExcel(EnumExportTo.Excel)
        Else
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub btnPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        If GV1.Rows.Count > 0 Then
            ExporttoExcel(EnumExportTo.PDF)
        Else
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
        End If
    End Sub
    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try


            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(dtpstart.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpend.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)

            If chklocSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbglocation.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location : " + strtemp)
            End If
            If chkItemSelect1.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgItem1.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Item : " + strtemp)
            End If

            If chkRouteSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgRoute.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Route : " + strtemp)
            End If

            If chkChkSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Customer : " + strtemp)
            End If
           


            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Leakage Breakage Report ", GV1, arrHeader, Me.Text)

            Else
                clsCommon.MyExportToPDF("Leakage Breakage Report ", GV1, arrHeader, "Trade Discount Report", True)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
       
        chklocAll.IsChecked = True
        chkCustAll.IsChecked = True
        chkRouteAll.IsChecked = True
        chkItemAll1.IsChecked = True
        rdbSku.IsChecked = True
        GV1.DataSource = Nothing
        GV1.Columns.Clear()
        GV1.Rows.Clear()
        RadPageView1.Visible = True
    End Sub
End Class
