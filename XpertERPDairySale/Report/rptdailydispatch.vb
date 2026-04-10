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
        'txtrouteno.arrValueMember = Nothing
        'txtLocation.arrValueMember = Nothing
        'txtCustomer.arrValueMember = Nothing
        rbtnproduct.Checked = False
        rbtnMilk.Checked = True
        rbtnBoth.Checked = False
        rbtnDispatch.Checked = True
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        funreset()
        'txtLocCode.Value = clsCommon.ShowSelectForm("LocationSegGP", strQuery, "Code", "screen_type='DS'", txtLocCode.Value, "Code", isButtonClicked)
        'txtLocDesc.Text = clsDBFuncationality.getSingleValue("select  Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" & txtLocCode.Value & "'")
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub Print()
        Try
            Dim dt As DataTable = Nothing
            Dim qry As String = ""
            Dim GpCode As String = Nothing

            Dim whr As String = " where Document_No in (select Against_Delivery_Code from TSPL_SD_SHIPMENT_HEAD where DO_Item_Type='NT') "
            If txtrouteno.arrValueMember IsNot Nothing AndAlso txtrouteno.arrValueMember.Count>0Then
                whr+= "  and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No in (" + clsCommon.GetMulcallString(txtrouteno.arrValueMember) + ") "
            End If
          If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count >0 Then
                whr += "  and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code IN (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
            End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count >0 Then
                whr += "  and Customer_Code IN (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
            End If

            whr += " AND convert (date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtfromdate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txttodate.Value) + "' "
            'If rbtnMilk.Checked = True Then
            '    whr += " And TSPL_ITEM_MASTER.Is_FreshItem ='1' "
            'End If
            'If rbtnproduct.Checked = True Then
            '    whr += " and TSPL_ITEM_MASTER.Is_Ambient='1' "
            'End If
            Dim batch As String = "  "
            If rbtnDispatch.Checked Then
                batch = " select right(Booking_No,6) as gpcode from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE   " + whr + "union all "
                batch += "  select right(Document_Code,6) as gpcode from TSPL_SD_SHIPMENT_HEAD where Is_Create_Auto_Invoice = 1 and Status = 1 and Is_Taxable = 0 "
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    batch += "  and Bill_To_Location IN (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
                End If
                If txtrouteno.arrValueMember IsNot Nothing AndAlso txtrouteno.arrValueMember.Count > 0 Then
                    batch += "  and Route_No IN (" + clsCommon.GetMulcallString(txtrouteno.arrValueMember) + ") "
                End If

                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    batch += "  and Customer_Code IN (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
                End If
                batch += " and convert (date,Document_Date,103)>= '" + clsCommon.GetPrintDate(txtfromdate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txttodate.Value) + "'  AND Against_Delivery_Code IS NULL "

            ElseIf rbtnInvoice.Checked Then
                batch = " select right(Booking_No,5) as gpcode from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE   " + whr + "union all "
                batch += "  select right(TSPL_SD_SALE_INVOICE_HEAD.Document_Code,5) as gpcode from TSPL_SD_SALE_INVOICE_HEAD LEFT JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No  where Is_Create_Auto_Invoice = 1 and TSPL_SD_SALE_INVOICE_HEAD.Status = 1 and TSPL_SD_SALE_INVOICE_HEAD.Is_Taxable = 0 "
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    batch += "  and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location IN (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
                End If
                If txtrouteno.arrValueMember IsNot Nothing AndAlso txtrouteno.arrValueMember.Count > 0 Then
                    batch += "  and TSPL_SD_SALE_INVOICE_HEAD.Route_No IN (" + clsCommon.GetMulcallString(txtrouteno.arrValueMember) + ") "
                End If

                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    batch += "  and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code IN (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
                End If
                batch += " and convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>= '" + clsCommon.GetPrintDate(txtfromdate.Value) + "' and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(txttodate.Value) + "'  AND Against_Delivery_Code IS NULL "

            End If
            dt = clsDBFuncationality.GetDataTable(batch)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    For Each btch In dt.Rows
            '        If clsCommon.myLen(GpCode) > 0 Then
            '            GpCode += "," + (clsCommon.myCstr(btch("gpcode")))
            '        Else
            '            GpCode = (clsCommon.myCstr(btch("gpcode")))
            '        End If
            '    Next
            'End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each btch In dt.Rows
                    If clsCommon.myLen(GpCode) > 0 Then
                        GpCode += "," + (clsCommon.myCstr(btch("gpcode")))
                    Else
                        GpCode = (clsCommon.myCstr(btch("gpcode")))
                    End If
                Next
            End If

            qry = " select '" + GpCode + "' as [GP_Code],'" + clsCommon.GetPrintDate(txtfromdate.Value) + "' as 
                    fromdate,'" + clsCommon.GetPrintDate(txttodate.Value) + "' as Todate,sum(yyy.QTYINPOUCH)QTYINPOUCH,sum(QTYinltr)QTYinltr,yyy.Item_Code,yyy.Item_Desc,max(HSN_Code)HSN_Code,sum(Qty)Qty,Item_Cost,sum(amount)amount,max(yyy.unit_code)unit_code,max(yyy.Customer_Name)Customer_Name,max(yyy.custGSTNO)custGSTNO,max(yyy.State)State,max(yyy.PIN_Code)PIN_Code,max(yyy.locGSTNO)locGSTNO,max(yyy.Location_Desc)Location_Desc,max(yyy.Location_Code)Location_Code,max(yyy.Add1)Add1,max(yyy.Add2)Add2,
                    max(yyy.LOCSTATE)LOCSTATE,max(yyy.LOCPIN)LOCPIN,max(yyy.Telphone)Telphone,max(yyy.Phone1)Phone1,max(yyy.Phone2)Phone2,(yyy.GatePass_No)GatePass_No,
                    max(yyy.Route_No)Route_No,max(yyy.Bill_To_Location)Bill_To_Location,max(yyy.Comp_Name)Comp_Name,max(yyy.comp_add1)comp_add1,max(yyy.comp_add2)comp_add2,
                    max(comp_add3)comp_add3,max(CompPhone)CompPhone,sum(yyy.Distributor_Commission_TotalAmt)Distributor_Commission_TotalAmt,MAX(GSTINNo)COMGSTINNo,
                    MAX(Pan_No)COMPan_No,max(custAdd1)custAdd1,max(ship_to_location)ship_to_location,'1' as CopyType,max(sale_invoice_no)sale_invoice_no,max(document_date) as invoicedate,
                    SUM(yyy.TAX2_Amt) AS TCS,MAX(yyy.Access_Officer) AS fssai_Lic_No,max(TAX2)TAX2,max(TAX2_Rate)TAX2_Rate,max(Rate)Rate  from ( "

            If rbtnDispatch.Checked Then
                qry += " Select  CASE WHEN  TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'LTR' then qty * ItemConversionInLTR.Conversion_Factor / ItemConversionInPouch.Conversion_Factor WHEN  TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'CRATE' then qty * ItemConversionCrate.Conversion_Factor / ItemConversionInPouch.Conversion_Factor  WHEN  TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'POUCH' then qty * ItemConversionInPouch.Conversion_Factor / ItemConversionInPouch.Conversion_Factor ELSE 0 END AS QTYINPOUCH,
                CASE WHEN    TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'CRATE' then qty * ItemConversionCrate.Conversion_Factor / ItemConversionInLTR.Conversion_Factor  WHEN  TSPL_SD_SHIPMENT_DETAIL.Unit_code = 'POUCH' then qty * ItemConversionInPouch.Conversion_Factor / ItemConversionInLTR.Conversion_Factor ELSE 0 END AS QTYinltr
                ,TSPL_SD_SHIPMENT_DETAIL.Item_Code,tspl_item_master.Item_Desc,HSN_Code,Qty,Item_Cost,amount,TSPL_SD_SHIPMENT_DETAIL.unit_code,
                    TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.GSTNO as custGSTNO,TSPL_CUSTOMER_MASTER.State,TSPL_CUSTOMER_MASTER.PIN_Code,
                    TSPL_LOCATION_MASTER.GSTNO as locGSTNO,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Add1,
                    TSPL_LOCATION_MASTER.Add2,TSPL_LOCATION_MASTER.State AS LOCSTATE,TSPL_LOCATION_MASTER.Pin_Code AS LOCPIN,TSPL_LOCATION_MASTER.Telphone,
                    TSPL_LOCATION_MASTER.Phone1,TSPL_LOCATION_MASTER.Phone2,TSPL_SD_SHIPMENT_head.GatePass_No,tspl_sd_shipment_head.Route_No,tspl_sd_shipment_head.Bill_To_Location,
                    TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1 as comp_add1 , TSPL_COMPANY_MASTER.Add2 as  comp_add2 ,TSPL_COMPANY_MASTER.Add3 as comp_add3 , 
                    case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone,
                    isnull(tspl_sd_shipment_head.Distributor_Commission_TotalAmt,0) as Distributor_Commission_TotalAmt,TSPL_COMPANY_MASTER.GSTINNo,TSPL_COMPANY_MASTER.Pan_No,
                    TSPL_CUSTOMER_MASTER.Add1 as  custAdd1 ,tspl_sd_shipment_head.ship_to_location,tspl_sd_shipment_head.Sale_Invoice_No,tspl_sd_sale_invoice_head.document_date,
                    TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt,TSPL_COMPANY_MASTER.Access_Officer, TSPL_SD_SHIPMENT_DETAIL.TAX2,TSPL_SD_SHIPMENT_DETAIL.TAX2_Rate,TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Rate
                    from TSPL_SD_SHIPMENT_DETAIL
                left outer join tspl_sd_shipment_head on tspl_sd_shipment_head.document_code=TSPL_SD_SHIPMENT_DETAIL.document_code
                LEFT OUTER JOIN TSPL_DISTRIBUTOR_COMMISSION_DETAIL ON TSPL_DISTRIBUTOR_COMMISSION_DETAIL.pk_id  =TSPL_SD_SHIPMENT_DETAIL.Distributor_Commission_PKID 		
                AND TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Route_Code = TSPL_SD_SHIPMENT_HEAD.Route_No
                left outer join tspl_item_master  on tspl_item_master.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code
                left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=tspl_sd_shipment_head.Customer_Code
                left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=tspl_sd_shipment_head.Bill_To_Location
                left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=tspl_sd_shipment_head.Comp_Code
                left outer join tspl_sd_sale_invoice_head on tspl_sd_sale_invoice_head.against_shipment_no =tspl_sd_shipment_head.document_code
                left join (select Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code from  TSPL_ITEM_UOM_DETAIL where UOM_code = 'Crate') as ItemConversionCrate on ItemConversionCrate.Item_code =  TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                left join (select Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code = 'Pouch' ) as ItemConversionInPouch on ItemConversionInPouch.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code
                left join ( select Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code from  TSPL_ITEM_UOM_DETAIL where UOM_code = 'LTR' ) as ItemConversionInLTR on ItemConversionInLTR.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                where convert (date,tspl_sd_shipment_head.Document_Date,103)>='" + clsCommon.GetPrintDate(txtfromdate.Value) + "' and convert(date,tspl_sd_shipment_head.Document_Date,103)<='" + clsCommon.GetPrintDate(txttodate.Value) + "' "

                If txtrouteno.arrValueMember IsNot Nothing AndAlso txtrouteno.arrValueMember.Count > 0 Then
                    qry += "  and tspl_sd_shipment_head.Route_No IN (" + clsCommon.GetMulcallString(txtrouteno.arrValueMember) + ") "
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += "  and tspl_sd_shipment_head.Bill_To_Location IN (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
                End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    qry += "   and tspl_sd_shipment_head.customer_code IN (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
                End If
                qry += " and tspl_sd_shipment_head.is_taxable=0 "

            ElseIf rbtnInvoice.Checked Then
                qry += "select  CASE WHEN  TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = 'LTR' then qty * ItemConversionInLTR.Conversion_Factor / ItemConversionInPouch.Conversion_Factor WHEN  TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = 'CRATE' then qty * ItemConversionCrate.Conversion_Factor / ItemConversionInPouch.Conversion_Factor  WHEN  TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = 'POUCH' then qty * ItemConversionInPouch.Conversion_Factor / ItemConversionInPouch.Conversion_Factor ELSE 0 END AS QTYINPOUCH,
                CASE WHEN    TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = 'CRATE' then qty * ItemConversionCrate.Conversion_Factor / ItemConversionInLTR.Conversion_Factor  WHEN  TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = 'POUCH' then qty * ItemConversionInPouch.Conversion_Factor / ItemConversionInLTR.Conversion_Factor ELSE 0 END AS QTYinltr,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,tspl_item_master.Item_Desc,HSN_Code,Qty,Item_Cost,amount,TSPL_SD_SALE_INVOICE_DETAIL.unit_code,
                    TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.GSTNO as custGSTNO,TSPL_CUSTOMER_MASTER.State,TSPL_CUSTOMER_MASTER.PIN_Code,TSPL_LOCATION_MASTER.GSTNO as locGSTNO,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Add1,
                    TSPL_LOCATION_MASTER.Add2,TSPL_LOCATION_MASTER.State AS LOCSTATE,TSPL_LOCATION_MASTER.Pin_Code AS LOCPIN,TSPL_LOCATION_MASTER.Telphone,TSPL_LOCATION_MASTER.Phone1,TSPL_LOCATION_MASTER.Phone2,TSPL_SD_SHIPMENT_head.GatePass_No,TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1 as comp_add1 , TSPL_COMPANY_MASTER.Add2 as  comp_add2 ,TSPL_COMPANY_MASTER.Add3 as comp_add3 , case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone,
                    isnull(TSPL_SD_SALE_INVOICE_HEAD.Distributor_Commission_TotalAmt,0) as Distributor_Commission_TotalAmt,TSPL_COMPANY_MASTER.GSTINNo,TSPL_COMPANY_MASTER.Pan_No,
                    TSPL_CUSTOMER_MASTER.Add1 as  custAdd1 ,TSPL_SD_SALE_INVOICE_HEAD.ship_to_location,TSPL_SD_SALE_INVOICE_HEAD.Document_Code AS  Sale_Invoice_No,tspl_sd_sale_invoice_head.document_date,
                    TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt,TSPL_COMPANY_MASTER.Access_Officer, TSPL_SD_SALE_INVOICE_DETAIL.TAX2,TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate,TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Rate
                    from TSPL_SD_SALE_INVOICE_DETAIL
                left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code=TSPL_SD_SALE_INVOICE_DETAIL.document_code
                LEFT OUTER JOIN TSPL_DISTRIBUTOR_COMMISSION_DETAIL ON TSPL_DISTRIBUTOR_COMMISSION_DETAIL.pk_id  =TSPL_SD_SALE_INVOICE_DETAIL.Distributor_Commission_PKID AND TSPL_DISTRIBUTOR_COMMISSION_DETAIL.Route_Code = TSPL_SD_SALE_INVOICE_HEAD.Route_No
                left outer join tspl_item_master  on tspl_item_master.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location
                left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SD_SALE_INVOICE_HEAD.Comp_Code
                left outer join TSPL_SD_SHIPMENT_head on TSPL_SD_SHIPMENT_head.document_code = tspl_sd_sale_invoice_head.against_shipment_no
                left join (select Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code from  TSPL_ITEM_UOM_DETAIL where UOM_code = 'Crate') as ItemConversionCrate on ItemConversionCrate.Item_code =  TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                left join (select Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code = 'Pouch' ) as ItemConversionInPouch on ItemConversionInPouch.Item_code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                left join ( select Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code from  TSPL_ITEM_UOM_DETAIL where UOM_code = 'LTR' ) as ItemConversionInLTR on ItemConversionInLTR.Item_code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  
               where convert (date,tspl_sd_sale_invoice_head.Document_Date,103)>='" + clsCommon.GetPrintDate(txtfromdate.Value) + "' and convert(date,tspl_sd_sale_invoice_head.Document_Date,103)<='" + clsCommon.GetPrintDate(txttodate.Value) + "' "

                If txtrouteno.arrValueMember IsNot Nothing AndAlso txtrouteno.arrValueMember.Count > 0 Then
                    qry += "  and tspl_sd_sale_invoice_head.Route_No IN (" + clsCommon.GetMulcallString(txtrouteno.arrValueMember) + ") "
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += "  and tspl_sd_sale_invoice_head.Bill_To_Location IN (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
                End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    qry += "   and tspl_sd_sale_invoice_head.customer_code IN (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
                End If
                qry += " and tspl_sd_sale_invoice_head.is_taxable=0 "
            End If

            If rbtnMilk.Checked = True Then
                qry += " And TSPL_ITEM_MASTER.Is_FreshItem ='1' "
            ElseIf rbtnproduct.Checked = True Then
                qry += " and TSPL_ITEM_MASTER.Is_Ambient='1' "
            End If
            qry += " ) yyy group by Item_Code,Item_Desc,Item_Cost,GatePass_No "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, "rptdailydispatch", "")
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to print", Me.Text)
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
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        GetReportID()
        griddata()
    End Sub
    Sub GetReportID()
        Dim VarID As String = ""
        If rbtnMilk.Checked Then
            VarID += "_MT"
        ElseIf rbtnproduct.Checked Then
            VarID += "_PT"
        ElseIf rbtnBoth.Checked Then
            VarID += "_BT"
        End If
        gv1.VarID = VarID

    End Sub

    Sub griddata()
        Try
            Dim dt As DataTable = Nothing
            Dim qry As String = ""
            If rbtnDispatch.Checked Then
                qry = "  select  yyy.document_code,yyy.Sale_Invoice_No,yyy.Customer_Code,yyy.Customer_Name,yyy.Item_Code,yyy.Item_Desc,yyy.unit_code,yyy.Qty,yyy.QTYINPOUCH,yyy.QTYinltr,CASE WHEN  yyy.Unit_code = 'LTR' then Amount/QTYINPOUCH WHEN  yyy.Unit_code = 'CRATE' then Amount/QTYINPOUCH   WHEN  yyy.Unit_code = 'POUCH' then Item_cost ELSE 0 END AS rateinpouch,yyy.Item_Cost, yyy.amount,GPCode  from  (select TSPL_SD_SALE_INVOICE_HEAD.document_code AS Sale_Invoice_No,TSPL_SD_SALE_INVOICE_DETAIL.Shipment_Code as Document_Code ,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,tspl_item_master.Item_Desc,TSPL_SD_SALE_INVOICE_DETAIL.unit_code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,CASE WHEN  TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = 'CRATE' then TSPL_SD_SALE_INVOICE_DETAIL.qty * ItemConversionCrate.Conversion_Factor / ItemConversionInLTR.Conversion_Factor  WHEN  TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = 'POUCH' then TSPL_SD_SALE_INVOICE_DETAIL.qty * ItemConversionInPouch.Conversion_Factor / ItemConversionInLTR.Conversion_Factor ELSE 0 END AS QTYinltr, CASE WHEN  TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = 'LTR' then TSPL_SD_SALE_INVOICE_DETAIL.qty * ItemConversionInLTR.Conversion_Factor / ItemConversionInPouch.Conversion_Factor WHEN  TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = 'CRATE' then TSPL_SD_SALE_INVOICE_DETAIL.qty * ItemConversionCrate.Conversion_Factor / ItemConversionInPouch.Conversion_Factor  WHEN  TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = 'POUCH' then TSPL_SD_SALE_INVOICE_DETAIL.qty * ItemConversionInPouch.Conversion_Factor / ItemConversionInPouch.Conversion_Factor ELSE 0 END AS QTYINPOUCH,TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost,TSPL_SD_SALE_INVOICE_DETAIL.amount,TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GPCode
					from TSPL_SD_SALE_INVOICE_DETAIL
                    left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code=TSPL_SD_SALE_INVOICE_DETAIL.document_code
                    left outer join tspl_item_master  on tspl_item_master.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                    left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
					left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No
					left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.Document_Code = TSPL_SD_SHIPMENT_HEAD.Document_Code
					and TSPL_SD_SHIPMENT_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                    left join TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL on TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.PK_ID=10
                    left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location
                    left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SD_SALE_INVOICE_HEAD.Comp_Code
                    left join (select Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code from  TSPL_ITEM_UOM_DETAIL where UOM_code = 'Crate') as ItemConversionCrate on ItemConversionCrate.Item_code =  TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                    left join (select Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code = 'Pouch' ) as ItemConversionInPouch on ItemConversionInPouch.Item_code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                    left join ( select Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code from  TSPL_ITEM_UOM_DETAIL where UOM_code = 'LTR' ) as ItemConversionInLTR on ItemConversionInLTR.Item_code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                    where convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>='" + clsCommon.GetPrintDate(txtfromdate.Value) + "' and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(txttodate.Value) + "' "

                If txtrouteno.arrValueMember IsNot Nothing AndAlso txtrouteno.arrValueMember.Count > 0 Then
                    qry += "  and TSPL_SD_SALE_INVOICE_HEAD.Route_No IN (" + clsCommon.GetMulcallString(txtrouteno.arrValueMember) + ") "
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += "  and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location IN (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
                End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    qry += "   and TSPL_SD_SALE_INVOICE_HEAD.customer_code IN (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
                End If

                qry += " and TSPL_SD_SALE_INVOICE_HEAD.is_taxable=0 "

            ElseIf rbtnInvoice.Checked Then
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
                                                left join ( select Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code from  TSPL_ITEM_UOM_DETAIL where UOM_code = 'LTR' ) as ItemConversionInLTR on ItemConversionInLTR.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code
                    where convert (date,tspl_sd_shipment_head.Document_Date,103)>='" + clsCommon.GetPrintDate(txtfromdate.Value) + "' and convert(date,tspl_sd_shipment_head.Document_Date,103)<='" + clsCommon.GetPrintDate(txttodate.Value) + "' "

                If txtrouteno.arrValueMember IsNot Nothing AndAlso txtrouteno.arrValueMember.Count > 0 Then
                    qry += "  and tspl_sd_shipment_head.Route_No IN (" + clsCommon.GetMulcallString(txtrouteno.arrValueMember) + ") "
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += "  and tspl_sd_shipment_head.Bill_To_Location IN (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
                End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    qry += "   and tspl_sd_shipment_head.customer_code IN (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
                End If

                qry += " and is_taxable=0 "
            End If


            If rbtnMilk.Checked = True Then
                qry += " and TSPL_ITEM_MASTER.Is_FreshItem='1' "
            ElseIf rbtnproduct.Checked = True Then
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

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
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

    Private Sub txtrouteno__My_Click(sender As Object, e As EventArgs) Handles txtrouteno._My_Click
        Try
            Dim qry As String = ""
            qry = " Select TSPL_ROUTE_MASTER.Route_No as Code,Route_Desc as Description from TSPL_ROUTE_MASTER"
            txtrouteno.arrValueMember = clsCommon.ShowMultipleSelectForm("routeno", qry, "Code", "Description", txtrouteno.arrValueMember, txtrouteno.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Try
            Dim qry As String = "select Cust_Code as Code,Customer_Name as Name from tspl_customer_master  "
            txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustFilter", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Try
            Dim qry As String = ""
            qry = " select location_code as Code ,location_desc as Name  from TSPL_LOCATION_MASTER "
            txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocFilter", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class