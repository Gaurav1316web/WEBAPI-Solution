Imports common
Public Class rptdailydispatch
    Inherits FrmMainTranScreen
    Public Trans_Id As String = Nothing
    Dim FormLoadData As Boolean = False
    Public FORMTYPE As String = Nothing
    Private Sub rptdailydispatch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtfromdate.Value = clsCommon.GETSERVERDATE()
        txttodate.Value = clsCommon.GETSERVERDATE()
        funreset()
        SetUserMgmtNew()
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Sub funreset()
        txtfromdate.Value = clsCommon.GETSERVERDATE()
        txttodate.Value = clsCommon.GETSERVERDATE()
        btngo.Enabled = True
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        txtrouteno.Value = ""
        lblRouteDesc.Text = ""
        txtlocation.Value = ""
        lbllocation.Text = ""
        txtcustomer.Value = ""
        lblcustomer.Text = ""
        ChkMilk.Checked = Nothing
        ChkProduct.Checked = Nothing
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        funreset()
        'txtLocCode.Value = clsCommon.ShowSelectForm("LocationSegGP", strQuery, "Code", "screen_type='DS'", txtLocCode.Value, "Code", isButtonClicked)
        'txtLocDesc.Text = clsDBFuncationality.getSingleValue("select  Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" & txtLocCode.Value & "'")
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub txtrouteno__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtrouteno._MYValidating
        Dim qry As String = ""
        qry = " Select TSPL_ROUTE_MASTER.Route_No as Code,Route_Desc as Description from TSPL_ROUTE_MASTER"
        txtrouteno.Value = clsCommon.ShowSelectForm("routeno", qry, "Code", "", txtrouteno.Value, "Code", isButtonClicked)
        lblRouteDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_Route_Master where Route_No='" + txtrouteno.Value + "' "))
    End Sub

    Private Sub txtcustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcustomer._MYValidating
        Dim qry As String = ""
        qry = " select Cust_Code as Code ,Customer_Name from TSPL_CUSTOMER_MASTER "
        txtcustomer.Value = clsCommon.ShowSelectForm("custcode", qry, "Code", "", txtcustomer.Value, "Code", isButtonClicked)
        lblcustomer.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtcustomer.Value + "' "))
    End Sub

    Private Sub txtlocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtlocation._MYValidating
        Dim qry As String = ""
        qry = " select location_code as Code ,location_desc from TSPL_LOCATION_MASTER "
        txtlocation.Value = clsCommon.ShowSelectForm("routeno", qry, "Code", "", txtlocation.Value, "Code", isButtonClicked)
        lbllocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from TSPL_LOCATION_MASTER where location_code='" + txtlocation.Value + "' "))

    End Sub
    Sub Print()
        Try
            Dim dt As DataTable = Nothing
            Dim qry As String = ""
            Dim GpCode As String = Nothing
            If clsCommon.myLen(txtrouteno.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select route no")
                Exit Sub
            End If
            If clsCommon.myLen(txtcustomer.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Customer")
                Exit Sub
            End If
            If clsCommon.myLen(txtlocation.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Location")
                Exit Sub
            End If
            Dim whr As String = " where tspl_sd_shipment_head.Route_No='" + txtrouteno.Value + "' and tspl_sd_shipment_head.Bill_To_Location='" + txtlocation.Value + "' and customer_code='" + txtcustomer.Value + "' AND convert
                                    (date,tspl_sd_shipment_head.Document_Date,103)>='" + clsCommon.GetPrintDate(txtfromdate.Value) + "' and convert(date,tspl_sd_shipment_head.Document_Date,103)<='" + clsCommon.GetPrintDate(txttodate.Value) + "' "
            Dim batch As String = " select DISTINCT right(TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.gpcode,5) as gpcode from TSPL_SD_SHIPMENT_DETAIL
				                    left outer join tspl_sd_shipment_head on tspl_sd_shipment_head.document_code=TSPL_SD_SHIPMENT_DETAIL.document_code
				                    left join TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL on TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.PK_ID=TSPL_SD_SHIPMENT_DETAIL.PK_ID " + whr
            dt = clsDBFuncationality.GetDataTable(batch)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each btch In dt.Rows
                    If clsCommon.myLen(GpCode) > 0 Then
                        GpCode += "," + (clsCommon.myCstr(btch("gpcode")))
                    Else
                        GpCode = (clsCommon.myCstr(btch("gpcode")))
                    End If
                Next
            End If

            qry = " select '" + GpCode + "' as [GP_Code],sum(yyy.QTYINPOUCH)QTYINPOUCH,sum(QTYinltr)QTYinltr,yyy.Item_Code,yyy.Item_Desc,max(HSN_Code)HSN_Code,sum(Qty)Qty,Item_Cost,sum(amount)amount,max(yyy.unit_code)unit_code,max(yyy.Customer_Name)Customer_Name,max(yyy.custGSTNO)custGSTNO,max(yyy.State)State,max(yyy.PIN_Code)PIN_Code,max(yyy.locGSTNO)locGSTNO,max(yyy.Location_Desc)Location_Desc,max(yyy.Location_Code)Location_Code,max(yyy.Add1)Add1,max(yyy.Add2)Add2,max(yyy.LOCSTATE)LOCSTATE,max(yyy.LOCPIN)LOCPIN,max(yyy.Telphone)Telphone,max(yyy.Phone1)Phone1,max(yyy.Phone2)Phone2,(yyy.GatePass_No)GatePass_No,max(yyy.Route_No)Route_No,max(yyy.Bill_To_Location)Bill_To_Location,max(yyy.Comp_Name)Comp_Name,max(yyy.comp_add1)comp_add1,max(yyy.comp_add2)comp_add2,max(comp_add3)comp_add3,max(CompPhone)CompPhone,sum(yyy.Distributor_Commission_TotalAmt)Distributor_Commission_TotalAmt,MAX(GSTINNo)COMGSTINNo,MAX(Pan_No)COMPan_No,max(custAdd1)custAdd1,max(ship_to_location)ship_to_location,'1' as CopyType,max(sale_invoice_no)sale_invoice_no  from (
                select  
                CASE WHEN  TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'LTR' then qty * ItemConversionInLTR.Conversion_Factor / ItemConversionInPouch.Conversion_Factor WHEN  TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'CRATE' then qty * ItemConversionCrate.Conversion_Factor / ItemConversionInPouch.Conversion_Factor  WHEN  TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'POUCH' then qty * ItemConversionInPouch.Conversion_Factor / ItemConversionInPouch.Conversion_Factor ELSE 0 END AS QTYINPOUCH,

                CASE WHEN    TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'CRATE' then qty * ItemConversionCrate.Conversion_Factor / ItemConversionInLTR.Conversion_Factor  WHEN  TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'POUCH' then qty * ItemConversionInPouch.Conversion_Factor / ItemConversionInLTR.Conversion_Factor ELSE 0 END AS QTYinltr

                ,TSPL_SD_SHIPMENT_DETAIL.Item_Code,tspl_item_master.Item_Desc,HSN_Code,Qty,Item_Cost,amount,TSPL_SD_SHIPMENT_DETAIL.unit_code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.GSTNO as custGSTNO,TSPL_CUSTOMER_MASTER.State,TSPL_CUSTOMER_MASTER.PIN_Code,TSPL_LOCATION_MASTER.GSTNO as locGSTNO,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add2,TSPL_LOCATION_MASTER.State AS LOCSTATE,TSPL_LOCATION_MASTER.Pin_Code AS LOCPIN,TSPL_LOCATION_MASTER.Telphone,TSPL_LOCATION_MASTER.Phone1,TSPL_LOCATION_MASTER.Phone2,TSPL_SD_SHIPMENT_head.GatePass_No,tspl_sd_shipment_head.Route_No,tspl_sd_shipment_head.Bill_To_Location,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1 as comp_add1 , TSPL_COMPANY_MASTER.Add2 as  comp_add2 ,TSPL_COMPANY_MASTER.Add3 as comp_add3 , case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone,isnull(tspl_sd_shipment_head.Distributor_Commission_TotalAmt,0) as Distributor_Commission_TotalAmt,TSPL_COMPANY_MASTER.GSTINNo,TSPL_COMPANY_MASTER.Pan_No,TSPL_CUSTOMER_MASTER.Add1 as  custAdd1 ,tspl_sd_shipment_head.ship_to_location,tspl_sd_shipment_head.Sale_Invoice_No  from TSPL_SD_SHIPMENT_DETAIL
                left outer join tspl_sd_shipment_head on tspl_sd_shipment_head.document_code=TSPL_SD_SHIPMENT_DETAIL.document_code
                left outer join tspl_item_master  on tspl_item_master.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code
                left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=tspl_sd_shipment_head.Customer_Code
                left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=tspl_sd_shipment_head.Bill_To_Location
                left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=tspl_sd_shipment_head.Comp_Code
                left join (select Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code from  TSPL_ITEM_UOM_DETAIL where UOM_code = 'Crate') as ItemConversionCrate on ItemConversionCrate.Item_code =  TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                            left join (select Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code = 'Pouch' ) as ItemConversionInPouch on ItemConversionInPouch.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code
                                            left join ( select Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code from  TSPL_ITEM_UOM_DETAIL where UOM_code = 'LTR' ) as ItemConversionInLTR on ItemConversionInLTR.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code where convert
                (date,tspl_sd_shipment_head.Document_Date,103)>='" + clsCommon.GetPrintDate(txtfromdate.Value) + "' and convert(date,tspl_sd_shipment_head.Document_Date,103)<='" + clsCommon.GetPrintDate(txttodate.Value) + "' and tspl_sd_shipment_head.Route_No='" + txtrouteno.Value + "' and tspl_sd_shipment_head.Bill_To_Location='" + txtlocation.Value + "' and is_taxable=0 and customer_code='" + txtcustomer.Value + "'"

            If ChkMilk.Checked = True Then
                qry += " And TSPL_ITEM_MASTER.Is_FreshItem ='1' "
            End If
            If ChkProduct.Checked = True Then
                qry += " and TSPL_ITEM_MASTER.Is_Ambient='1' "
            End If
            qry += " ) yyy group by Item_Code,Item_Desc,Item_Cost,GatePass_No "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptdailydispatch", "")
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub SetGridFormat()

        gv1.AutoExpandGroups = True
        gv1.ShowGroupPanel = True
        gv1.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True


        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).BestFit()
        Next
        'gv1.Columns("SNo").Name = "SNo"
        'gv1.Columns("SNo").IsVisible = True

        gv1.Columns("document_code").HeaderText = "Document Code"
        gv1.Columns("document_code").Width = 250
        gv1.Columns("document_code").IsVisible = False

        gv1.Columns("Sale_Invoice_No").HeaderText = "Invoice No."
        gv1.Columns("Sale_Invoice_No").Width = 250
        gv1.Columns("Sale_Invoice_No").IsVisible = True


        gv1.Columns("Customer_Code").HeaderText = "Customer Code"
        gv1.Columns("Customer_Code").Width = 250
        gv1.Columns("Customer_Code").IsVisible = True
        'gv1.Columns("Vlc Uploader Code").FormatString = "{0:n2}"

        gv1.Columns("Customer_Name").HeaderText = "Customer Name"
        gv1.Columns("Customer_Name").Width = 500


        gv1.Columns("Item_Code").HeaderText = "Item Code"
        gv1.Columns("Item_Code").Width = 250

        gv1.Columns("Item_Desc").HeaderText = "Item Name"
        gv1.Columns("Item_Desc").Width = 250

        gv1.Columns("unit_code").HeaderText = "unit code"
        gv1.Columns("unit_code").Width = 250

        gv1.Columns("Qty").HeaderText = "Qty"
        gv1.Columns("Qty").Width = 250
        gv1.Columns("Qty").FormatString = "{0:n2}"

        gv1.Columns("QTYinltr").HeaderText = "Qty in LTR"
        gv1.Columns("QTYinltr").Width = 250
        gv1.Columns("QTYinltr").FormatString = "{0:n2}"

        gv1.Columns("QTYINPOUCH").HeaderText = "Qty in Pouch"
        gv1.Columns("QTYINPOUCH").Width = 250
        gv1.Columns("QTYINPOUCH").FormatString = "{0:n2}"

        gv1.Columns("Item_Cost").HeaderText = "Item Cost"
        gv1.Columns("Item_Cost").Width = 250
        gv1.Columns("Item_Cost").IsVisible = False
        gv1.Columns("Item_Cost").FormatString = "{0:n2}"


        gv1.Columns("rateinpouch").HeaderText = "Rate in Pouch"
        gv1.Columns("rateinpouch").Width = 250
        gv1.Columns("rateinpouch").IsVisible = True
        gv1.Columns("rateinpouch").FormatString = "{0:n2}"

        gv1.Columns("amount").HeaderText = "Amount"
        gv1.Columns("amount").Width = 250
        gv1.Columns("amount").IsVisible = True
        gv1.Columns("amount").FormatString = "{0:n2}"

        gv1.Columns("GPCode").HeaderText = "GatePass No."
        gv1.Columns("GPCode").Width = 250
        gv1.Columns("GPCode").IsVisible = False



        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Qty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)


        Dim item2 As New GridViewSummaryItem("QTYINPOUCH", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)


        Dim item3 As New GridViewSummaryItem("Item_Cost", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)


        Dim item4 As New GridViewSummaryItem("amount", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        griddata()
    End Sub
    Sub griddata()
        Try
            Dim dt As DataTable = Nothing
            Dim qry As String = ""
            If clsCommon.myLen(txtrouteno.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select route no")
                Exit Sub
            End If
            If clsCommon.myLen(txtcustomer.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Customer")
                Exit Sub
            End If
            If clsCommon.myLen(txtlocation.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Location")
                Exit Sub
            End If

            qry = " select yyy.document_code,yyy.Sale_Invoice_No,yyy.Customer_Code,yyy.Customer_Name,yyy.Item_Code,yyy.Item_Desc,yyy.unit_code,yyy.Qty,yyy.QTYINPOUCH,yyy.QTYinltr,CASE WHEN  yyy.Unit_code = 'LTR' then Amount/QTYINPOUCH WHEN  yyy.Unit_code = 'CRATE' then Amount/QTYINPOUCH   WHEN  yyy.Unit_code = 'POUCH' then Item_cost ELSE 0 END AS rateinpouch,yyy.Item_Cost, yyy.amount,GPCode  from  (select tspl_sd_shipment_head.document_code,tspl_sd_shipment_head.Sale_Invoice_No,TSPL_SD_SHIPMENT_head.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SHIPMENT_DETAIL.Item_Code,tspl_item_master.Item_Desc,TSPL_SD_SHIPMENT_DETAIL.unit_code,Qty,CASE WHEN  TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'CRATE' then qty * ItemConversionCrate.Conversion_Factor / ItemConversionInLTR.Conversion_Factor  WHEN  TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'POUCH' then qty * ItemConversionInPouch.Conversion_Factor / ItemConversionInLTR.Conversion_Factor ELSE 0 END AS QTYinltr, CASE WHEN  TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'LTR' then qty * ItemConversionInLTR.Conversion_Factor / ItemConversionInPouch.Conversion_Factor WHEN  TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'CRATE' then qty * ItemConversionCrate.Conversion_Factor / ItemConversionInPouch.Conversion_Factor  WHEN  TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'POUCH' then qty * ItemConversionInPouch.Conversion_Factor / ItemConversionInPouch.Conversion_Factor ELSE 0 END AS QTYINPOUCH,Item_Cost,amount,TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GPCode
					from TSPL_SD_SHIPMENT_DETAIL
                    left outer join tspl_sd_shipment_head on tspl_sd_shipment_head.document_code=TSPL_SD_SHIPMENT_DETAIL.document_code
                    left outer join tspl_item_master  on tspl_item_master.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code
                    left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=tspl_sd_shipment_head.Customer_Code
                    left join TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL on TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.PK_ID=TSPL_SD_SHIPMENT_DETAIL.PK_ID
                    left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=tspl_sd_shipment_head.Bill_To_Location
                    left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=tspl_sd_shipment_head.Comp_Code
                    left join (select Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code from  TSPL_ITEM_UOM_DETAIL where UOM_code = 'Crate') as ItemConversionCrate on ItemConversionCrate.Item_code =  TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                                left join (select Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code = 'Pouch' ) as ItemConversionInPouch on ItemConversionInPouch.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code
                                                left join ( select Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code from  TSPL_ITEM_UOM_DETAIL where UOM_code = 'LTR' ) as ItemConversionInLTR on ItemConversionInLTR.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code where convert
                    (date,tspl_sd_shipment_head.Document_Date,103)>='" + clsCommon.GetPrintDate(txtfromdate.Value) + "' and convert(date,tspl_sd_shipment_head.Document_Date,103)<='" + clsCommon.GetPrintDate(txttodate.Value) + "' and tspl_sd_shipment_head.Route_No='" + txtrouteno.Value + "' and tspl_sd_shipment_head.Bill_To_Location='" + txtlocation.Value + "' and is_taxable=0 and Customer_Code='" + txtcustomer.Value + "' "

            If ChkMilk.Checked = True Then
                qry += " and TSPL_ITEM_MASTER.Is_FreshItem='1' "
            End If
            If ChkProduct.Checked = True Then
                qry += " and TSPL_ITEM_MASTER.Is_Ambient='1' "
            End If
            qry += "   )yyy "
            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()

            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next

                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
                SetGridFormat()
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Print()
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try

            If gv1.Rows.Count > 0 Then
                Dim docno As String = clsCommon.myCstr(gv1.CurrentRow.Cells("document_code").Value)
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleDispatchDairy, docno)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message(), Me.Text)
        End Try
    End Sub
End Class