Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class FrmPrintProductInvoiceStatement
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    'Dim Refresh As Boolean = False
    Public Shared ArrInvoice_Arr As New ArrayList()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmPrintProductInvoiceStatement)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Sub LoadLocation()
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER  "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"

    End Sub
    Public Sub loadReport()
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
            txtFromDate.Focus()
            Exit Sub
        End If
        If clsCommon.myCDate(txtFromDate.Value) < objCommonVar.GSTApplicableDate AndAlso clsCommon.myCDate(txtToDate.Value) > objCommonVar.GSTApplicableDate Then
            clsCommon.MyMessageBoxShow("Please Select From Date and To date range without GST or within GST", Me.Text)
            Exit Sub
        End If

        If clsCommon.myCDate(txtFromDate.Value) >= objCommonVar.GSTApplicableDate AndAlso clsCommon.myCDate(txtToDate.Value) >= objCommonVar.GSTApplicableDate Then
            If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("Please Select Report Type.", Me.Text)
                cboReportType.Focus()
                Exit Sub
            End If
        End If

        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Location or select all.")
            Exit Sub
        End If
        Dim sQuery As String = " select Cast(1 as BIT) as 'Check', Document_Code  ,convert(varchar,Document_Date,103) as Document_Date ,Customer_Name  ,Location_Desc ,Total_Amt  from TSPL_SD_SALE_INVOICE_HEAD left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location " & _
            "  left outer join TSPL_STATE_MASTER ON TSPL_CUSTOMER_MASTER.State=TSPL_STATE_MASTER.STATE_CODE " & _
            " where  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='PS'"

        If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "LT") = CompairStringResult.Equal Then
            sQuery += " and TSPL_SD_SALE_INVOICE_HEAD.is_taxable=1 and TSPL_LOCATION_MASTER.State=TSPL_CUSTOMER_MASTER.State  and 0= (select count(*) from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code=TSPL_SD_SALE_INVOICE_HEAD.Tax_Group and Tax_Code in(select Tax_Code from TSPL_TAX_MASTER where Is_Mandi_Tax='Y'))"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "LWM") = CompairStringResult.Equal Then
            sQuery += " and TSPL_SD_SALE_INVOICE_HEAD.is_taxable=1 and TSPL_LOCATION_MASTER.State=TSPL_CUSTOMER_MASTER.State and 1 <= (select count(*) from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code=TSPL_SD_SALE_INVOICE_HEAD.Tax_Group and Tax_Code in(select Tax_Code from TSPL_TAX_MASTER where Is_Mandi_Tax='Y'))"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "UT") = CompairStringResult.Equal Then
            sQuery += " and TSPL_SD_SALE_INVOICE_HEAD.is_taxable=1 and TSPL_LOCATION_MASTER.State=TSPL_CUSTOMER_MASTER.State and isnull(TSPL_STATE_MASTER.Is_GST_UT,0)=1"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "IT") = CompairStringResult.Equal Then
            sQuery += "and TSPL_SD_SALE_INVOICE_HEAD.is_taxable=1 and TSPL_LOCATION_MASTER.State<>TSPL_CUSTOMER_MASTER.State and 0= (select count(*) from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code=TSPL_SD_SALE_INVOICE_HEAD.Tax_Group and Tax_Code in(select Tax_Code from TSPL_TAX_MASTER where Is_Mandi_Tax='Y'))"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "IWM") = CompairStringResult.Equal Then
            sQuery += " and TSPL_SD_SALE_INVOICE_HEAD.is_taxable=1 and TSPL_LOCATION_MASTER.State<>TSPL_CUSTOMER_MASTER.State and 1 <= (select count(*) from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code=TSPL_SD_SALE_INVOICE_HEAD.Tax_Group and Tax_Code in(select Tax_Code from TSPL_TAX_MASTER where Is_Mandi_Tax='Y'))"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "NT") = CompairStringResult.Equal Then
            sQuery += " and TSPL_SD_SALE_INVOICE_HEAD.is_taxable=0 "
        End If

        sQuery += " and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        '==============update by preeti gupta Against Ticket No [BM00000005410]

        If chkLocationSelect.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
            sQuery += " and TSPL_LOCATION_MASTER. Location_Code   IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
        End If
        sQuery += " order by TSPL_SD_SALE_INVOICE_HEAD.Document_Date"

        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If
        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("Check").IsVisible = True
        gv.Columns("Check").Width = 100
        gv.Columns("Check").HeaderText = " "
        gv.Columns("Check").ReadOnly = False



        gv.Columns("Document_Code").IsVisible = True
        gv.Columns("Document_Code").Width = 100
        gv.Columns("Document_Code").HeaderText = "Sale Invoice No."



        gv.Columns("Document_Date").IsVisible = True
        gv.Columns("Document_Date").Width = 100
        gv.Columns("Document_Date").HeaderText = " Date"
        'gv.Columns("Document_Date").FormatString = "{0:d}"

        gv.Columns("Location_Desc").IsVisible = True
        gv.Columns("Location_Desc").Width = 100
        gv.Columns("Location_Desc").HeaderText = "Location"

        gv.Columns("Customer_Name").IsVisible = True
        gv.Columns("Customer_Name").Width = 150
        gv.Columns("Customer_Name").HeaderText = "Customer"



        gv.Columns("Total_Amt").IsVisible = True
        gv.Columns("Total_Amt").Width = 100
        gv.Columns("Total_Amt").HeaderText = "Amount"

        'gv.Columns("DespatchDocumentNo").IsVisible = False
        'gv.Columns("DespatchDocumentNo").Width = 100
        'gv.Columns("DespatchDocumentNo").HeaderText = "DespatchDocumentNo"



        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Total_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)



        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        loadReport()
    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadLocation()
        gv.DataSource = Nothing
        chkLocationAll.CheckState = CheckState.Checked
        RadPageView1.SelectedPage = RadPageViewPage1
        cboReportType.SelectedValue = ""

    End Sub
    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        loadData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub FrmPrintProductInvoiceStatement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        LoadReportType()
        Reset()
    End Sub
    Sub LoadReportType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "-Select-"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "LT"
        dr("Name") = "Local Taxable"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "LWM"
        dr("Name") = "Local With MandiTax"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "IT"
        dr("Name") = "Interstate Taxable"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "IWM"
        dr("Name") = "Interstate With MandiTax"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "UT"
        dr("Name") = "UT Taxable"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "NT"
        dr("Name") = "NonTaxable"
        dt.Rows.Add(dr)

        cboReportType.DataSource = dt
        cboReportType.ValueMember = "Code"
        cboReportType.DisplayMember = "Name"
        cboReportType.SelectedValue = ""
    End Sub

    Sub loadData()

        ArrInvoice_Arr = New ArrayList


        Dim InvoiceNo As String = ""
        Dim DispatchNo As String = ""

        For Each grow As GridViewRowInfo In gv.Rows
            If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                InvoiceNo = InvoiceNo + "','" + clsCommon.myCstr(grow.Cells("Document_Code").Value)

            End If
        Next

        If clsCommon.myLen(InvoiceNo) > 0 AndAlso clsCommon.myCstr(InvoiceNo).Substring(0, 3) = "','" Then
            InvoiceNo = InvoiceNo.Substring(3, InvoiceNo.Length - 3)

        End If




        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
            txtFromDate.Focus()
            Exit Sub
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Location or select all.")
            Exit Sub
        End If
     

        'Dim Qry As String = " select TSPL_SD_SHIPMENT_HEAD.Transport_Id,TSPL_SD_SHIPMENT_HEAD.Transporter_Name,TSPL_SD_SHIPMENT_HEAD.GRNo,convert(date,TSPL_SD_SHIPMENT_HEAD.GR_Date,103) as GR_Date , TSPL_SD_SALE_INVOICE_HEAD.VehicleNo,TSPL_SD_SALE_INVOICE_DETAIL.Alter_UnitQty,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_Amt ,TSPL_SD_SALE_INVOICE_HEAD.Modify_By,TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No,TSPL_ITEM_MASTER.Cheapter_Heads,convert(date,TSPL_SD_SHIPMENT_HEAD.RoadPermit_Date,103) as RoadPermit_Date ,"
        'Qry += " TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_For_Location.City_Name)>0 then ', '+TSPL_CITY_MASTER_For_Location .City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end  + case when len(TSPL_LOCATION_MASTER.Pin_Code   )>0 then ', Pin Code - '+ cast(TSPL_LOCATION_MASTER.Pin_Code  as varchar)  else ' ' end  + case when len(TSPL_LOCATION_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_LOCATION_MASTER.Tin_No as varchar)  else ' ' end  + Case when len(ISNULL(TSPL_LOCATION_MASTER.Phone1,''))>0 and TSPL_LOCATION_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_LOCATION_MASTER.Email    )>0 then ',Email - '+ TSPL_LOCATION_MASTER.Email else '' end  as Location_Address,TSPL_LOCATION_MASTER.CST_No as Loc_CSTNo,TSPL_LOCATION_MASTER.Excisable as loc_Excisable,TSPL_LOCATION_MASTER.Range_Address as Loc_range_Add,TSPL_LOCATION_MASTER.Division_Address  as loc_Division_Address,TSPL_LOCATION_MASTER.Commissionerate  as Loc_Commissionerate, TSPL_SD_SALE_INVOICE_HEAD.GRNo ,TSPL_SD_SALE_INVOICE_HEAD.Challan_No ,convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Challan_Date,103) as Challan_Date,TSPL_SD_SHIPMENT_HEAD.Removal_Date,"
        'Qry += "   TSPL_SD_SALE_INVOICE_HEAD.total_add_charge ,TSPL_SD_SALE_INVOICE_HEAD. WayBillNo,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end"

        'Qry += "  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end"
        'Qry += "  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end"
        'Qry += "  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end"
        'Qry += "  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end"

        'Qry += "+ Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_COMPANY_MASTER.Phone1 end "
        'Qry += "+  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End "
        'Qry += "  + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end "


        'Qry += " as Comp_Address, TSPL_LOCATION_MASTER .Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_ADd2,TSPL_LOCATION_MASTER.Add3  as Loc_Add3,TSPL_LOCATION_MASTER.Pin_Code as Loc_Pin_Code,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,Case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn,TSPL_LOCATION_MASTER.Email as Loc_Email,( case when TSPL_SD_SALE_INVOICE_head.Invoice_Type='R' then 'Retail Invoice' else 'Tax Invoice' end) as Invoice_Type,case when len(isnull(Alternate_UOM ,''))>0 then Qty *Conversion_Factor else null end  as Alternet_Qty,"
        'Qry += " TSPL_SD_SALE_INVOICE_DETAIL .Alternate_UOM,    TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item_UOM + case when Scheme_Item='Y' then ' (Free Scheme)' when Scheme_Item='N' then ''end as Scheme_Item_UOM  , TSPL_SD_SALE_INVOICE_HEAD.Discount_Base,case when len(isnull(TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No,''))>0 then TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No else TSPL_SD_SALE_INVOICE_HEAD.GRNo end AS Dis_Doc_No, TSPL_SD_SHIPMENT_HEAD.Description ,TSPL_SD_SALE_INVOICE_HEAD.Transporter_Name , TSPL_COMPANY_MASTER .State as Comp_State,coalesce(TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No,TSPL_SD_SALE_INVOICE_HEAD.GRNo) as Dis_doc_no, TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No as Buyer_order_no,convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_Date,103) as Buyer_order_date,case when (TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms )='FE' then 'Freight Extra' else  TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms  end  as Terms_of_delivery,TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date_Time_Invoice,convert(varchar ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as InvoiceDate "
        'Qry += " ,TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No as ShipmentNo,TSPL_SD_SALE_INVOICE_DETAIL.Alt_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Alt_UOM,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as ShipmentDate,"
        'Qry += " TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code as DeliveryOrderNo,TSPL_SD_SALE_INVOICE_HEAD.Terms_Code as TermCondition,"
        'Qry += " TSPL_LOCATION_MASTER.Location_Desc , TSPL_COMPANY_MASTER.Comp_Name as CompName, ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+ "
        'Qry += " Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone,"
        'Qry += " TSPL_COMPANY_MASTER.Fax as CompFax,TSPL_COMPANY_MASTER.Email as ComMail,TSPL_COMPANY_MASTER.CINNo as CompCinNo,TSPL_COMPANY_MASTER.Pan_No as ComPanNo,"

        'Qry += "  TSPL_COMPANY_MASTER.CST_LST as CompCSTLST,TSPL_COMPANY_MASTER.Pincode as ComPINCode,"
        'Qry += " TSPL_COMPANY_MASTER .Tin_No as ComTinNO,ISNULL(tspl_company_Master.ADD1,'') as Compaddress1,ISNULL(tspl_company_Master.ADD2,'') "
        'Qry += "  as Compaddress2,ISNULL(tspl_company_Master.ADD3,'') as Compaddress3,"

        'Qry += "   case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add1  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add1 end as P_Add1, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add2  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add2 end as P_Add2, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add3  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add3 end as P_Add3, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .PIN_Code   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Pin_No  end as P_PinNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .CST    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_CST_No   end as P_CstNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Tin_No     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_Tin_No   end as P_TinNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Email    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Email  end as P_Email,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Fax     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Fax   end as P_Fax, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .CST      when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_CST_No    end as P_CSTNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Lst_No     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_LST_No    end as P_LstNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Cust_Code      when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_code   end as P_CustCode, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Customer_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_name    end as P_Cust_Name,"
        'Qry += " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CITY_MASTER   .City_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_City_Name    end as P_City_Name,"
        'Qry += " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_state_Master.state_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_state_Name    end as P_State_Name,"


        'Qry += " case when coalesce(p_cust.P_cust_code,'')='' then     case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Phn    end as P_Cust_Phn,TSPL_CUSTOMER_MASTER.Cust_Code ,  TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as Cust_Add1,TSPL_CUSTOMER_MASTER.Add2 as Cust_add2,TSPL_CUSTOMER_MASTER.Add3 as cust_add3,  case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End  as Cust_Phn,TSPL_CUSTOMER_MASTER.Tin_No  as Cust_TinNo ,TSPL_CUSTOMER_MASTER.CST as Cust_CSTNo,TSPL_CUSTOMER_MASTER.Lst_No as Cust_LSTNo,TSPL_CUSTOMER_MASTER.Email as Cust_Email ,TSPL_CUSTOMER_MASTER.PIN_Code as Cust_PinCode,TSPL_CITY_MASTER.City_Name as Cust_City_Name,TSPL_CUSTOMER_MASTER.Fax as Cust_Fax,TSPL_STATE_MASTER .STATE_NAME  as Cust_State_Name, "


        'Qry += " TSPL_SD_SALE_INVOICE_DETAIL.item_code + case when Scheme_Item='Y' then ' (Free Scheme)' when Scheme_Item='N' then '' end as item_code,TSPL_ITEM_MASTER.Item_Desc + case when Scheme_Item='Y' then ' (Free Scheme)' when Scheme_Item='N' then '' end   as itemdesc,TSPL_SD_SALE_INVOICE_DETAIL.Qty as qty"
        'Qry += " ,TSPL_SD_SALE_INVOICE_DETAIL.mrp,TSPL_SD_SALE_INVOICE_DETAIL.amount as amount,TSPL_SD_SALE_INVOICE_DETAIL.unit_code as uom,TSPL_SD_SALE_INVOICE_DETAIL.RATE_UOM as RATE_UOM"
        'Qry += " ,TSPL_SD_SALE_INVOICE_DETAIL.item_cost as itemcost, tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax1_amt,0) as txt1amt,"
        'Qry += "   tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax2_amt,0) as txt2amt,   tax3.Tax_Code_Desc as tax3name,"
        'Qry += "   isnull (TSPL_SD_SALE_INVOICE_HEAD.tax3_amt,0) as txt3amt,   tax4.Tax_Code_Desc as tax4name,"
        'Qry += "   isnull (TSPL_SD_SALE_INVOICE_HEAD.tax4_amt,0) as txt4amt,   tax5.Tax_Code_Desc as tax5name,"
        'Qry += "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax5_amt,0) as txt5amt,   tax6.Tax_Code_Desc as tax6name,"
        'Qry += "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax6_amt,0) as txt6amt,   tax7.Tax_Code_Desc as tax7name,"
        'Qry += " isnull (TSPL_SD_SALE_INVOICE_HEAD.tax7_amt,0) as txt7amt,   tax8.Tax_Code_Desc as tax8name,"
        'Qry += " isnull (TSPL_SD_SALE_INVOICE_HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name,"
        'Qry += "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax9_amt,0) as txt9amt,   tax10.Tax_Code_Desc as tax10name,"
        'Qry += "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax10_amt,0) as txt10amt,TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX5_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX6_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX7_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX8_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX9_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX10_Rate,TSPL_SD_SALE_INVOICE_DETAIL.Disc_Per, isnull(TSPL_SD_SALE_INVOICE_HEAD.Discount_Amt,0) as Discount_Amt,isnull(TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount,0) as Amount_Less_Discount,  isnull(TSPL_SD_SALE_INVOICE_HEAD .Total_Tax_Amt,0) as Total_Tax_Amt, TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as Total_Amt "
        'Qry += "     from TSPL_SD_SALE_INVOICE_HEAD"
        'Qry += " left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE "
        'Qry += " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No"
        'Qry += " left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code "
        'Qry += "  left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.comp_code "
        'Qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code    LEFT join (select Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,PIN_Code as P_Pin_No,Tin_No as p_Tin_No ,State as P_state,Email as P_Email,fax as P_Fax,TSPL_CITY_MASTER.City_Name as  P_City_Name,TSPL_STATE_MASTER.STATE_NAME as P_State_Name  ,case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn,CST as P_CST_No,Terms_Code as P_Terms,Lst_No as P_LST_No  from TSPL_CUSTOMER_MASTER   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code "
        'Qry += " left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State =TSPL_STATE_MASTER.STATE_CODE  "
        'Qry += " ) p_cust on p_cust.P_cust_code=TSPL_CUSTOMER_MASTER.Parent_Customer_No and TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N'"
        'Qry += "  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_CUSTOMER_MASTER .State  "
        'Qry += " Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code "
        'Qry += " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_HEAD.tax1"
        'Qry += " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_HEAD.tax2  "
        'Qry += "  left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .TAX3  "
        'Qry += " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_INVOICE_HEAD .tax4  "
        'Qry += " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .tax5  "
        'Qry += " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX6  "
        'Qry += "  left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX7 "
        'Qry += "   left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX8 "
        'Qry += " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX9 "
        'Qry += "   left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX10  "
        'Qry += "left outer join TSPL_ITEM_UOM_DETAIL  on TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   TSPL_ITEM_UOM_DETAIL.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Unit_code "
        'Qry += "  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code "
        'Qry += " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code "
        'Qry += " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State "
        'Qry += "  left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Location on  TSPL_CITY_MASTER_For_Location.City_Code =TSPL_CITY_MASTER.City_Code "
        'Qry += " left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_State on TSPL_STATE_MASTER_For_State.STATE_CODE =TSPL_STATE_MASTER.STATE_CODE "
        'Qry += " where TSPL_SD_SALE_INVOICE_HEAD.Document_Code in ('" + InvoiceNo + "')"
        ''Qry += " where 2=2 and  TSPL_SD_SALE_INVOICE_HEAD.Document_Code = '" + strDocNo + "' order by TSPL_SD_SALE_INVOICE_DETAIL.line_no"

        Dim objInvoice As New frmSaleInvoiceProductSale
        Dim Qry As String = objInvoice.GetQuery("")
        Qry += " where TSPL_SD_SALE_INVOICE_HEAD.Document_Code in ('" + InvoiceNo + "') "
        Qry = "Select * from (" & Qry & ") XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL COPY' as CopyType1 UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 4 as COL2,  'QUADRUPLICATE COPY' as CopyType1) YYY ON YYY.COL1=XXX.CopyType ORDER BY YYY.COL2,XXX.Line_No"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

        Dim count As Integer = 0
        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            Dim UOMKG As String = String.Empty

            For i As Integer = 0 To dt.Rows.Count - 1
                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i)("UOM")), clsCommon.myCstr(dt.Rows(0)("UOM"))) = CompairStringResult.Equal Then
                    count = count + 1

                End If
            Next
        End If
        dt.Columns.Add("UOMKG")
        If dt.Rows.Count = count Then
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("UOMKG") = "T"
            Next
        Else
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("UOMKG") = "F"
            Next
        End If


        If dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "") = CompairStringResult.Equal Then
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice", "Product Invoice Statement", "rptCompanyAddress.rpt")
            Else
                If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "LT") = CompairStringResult.Equal Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice", "Product Invoice Statement", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt")
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "LWM") = CompairStringResult.Equal Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice_WithMandiTax", "Product Invoice Statement", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt")
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "IT") = CompairStringResult.Equal Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice_Interstate", "Product Invoice Statement", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt")
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "IWM") = CompairStringResult.Equal Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice_InterstateWithMandiTax", "Product Invoice Statement", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt")
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "UT") = CompairStringResult.Equal Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice_UT", "Product Invoice Statement", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt")
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "NT") = CompairStringResult.Equal Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice _NonTaxable", "Product Invoice Statement", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt")
                End If
            End If
            frmCRV = Nothing

            ' frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice", "Product Invoice Statement", "rptCompanyAddress.rpt")

        End If
        'Next

    End Sub
    Private Sub FrmPrintProductInvoiceStatement_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            loadReport()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            loadData()
        End If
    End Sub

    Private Sub btnUnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnSelect.Click
        If clsCommon.CompairString(btnUnSelect.Text, "UnSelect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gv.Rows
                grow.Cells(0).Value = False

            Next
            btnUnSelect.Text = "Select All"
        Else
            For Each grow As GridViewRowInfo In gv.Rows
                grow.Cells(0).Value = True
            Next
            btnUnSelect.Text = "UnSelect All"
        End If
    End Sub
    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If chkLocationSelect.IsChecked Then
                Dim stVSPName As String = ""
                For Each StrName As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(stVSPName) > 0 Then
                        stVSPName += ", "
                    End If
                    stVSPName += StrName
                Next
                Dim strVSPCode As String = ""
                For Each StrCode As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strVSPCode) > 0 Then
                        strVSPCode += ", "
                    End If
                    strVSPCode += StrCode
                Next
                arrHeader.Add(("Location: " + stVSPName + " "))
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Product Sale Invoice", gv, arrHeader, Me.Text)

            Else
                clsCommon.MyExportToPDF("Product Sale Invoice", gv, arrHeader, "Product Sale Invoice", True)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        'If gv.Rows.Count > 0 Then
        '    ExportToExcel(EnumExportTo.Excel)
        'Else
        '    RadMessageBox.Show("No Data Found to Display", Me.Text)
        'End If
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmPrintProductInvoiceStatement & "'"))
                arrHeader.Add("Report Type : " + cboReportType.Text)
                If chkLocationSelect.IsChecked Then
                    Dim stVSPName As String = ""
                    For Each StrName As String In cbgLocation.CheckedDisplayMember
                        If clsCommon.myLen(stVSPName) > 0 Then
                            stVSPName += ", "
                        End If
                        stVSPName += StrName
                    Next
                    Dim strVSPCode As String = ""
                    For Each StrCode As String In cbgLocation.CheckedValue
                        If clsCommon.myLen(strVSPCode) > 0 Then
                            strVSPCode += ", "
                        End If
                        strVSPCode += StrCode
                    Next
                    arrHeader.Add(("Location: " + stVSPName + " "))
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
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Product Sale Invoice", gv, arrHeader, "Product Sale Invoice", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                RadMessageBox.Show("No Data Found to Display", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
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

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        Dim ReportID As String = PageSetupReport_ID
        If clsCommon.myLen(ReportID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
End Class
