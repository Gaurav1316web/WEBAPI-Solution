Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class rptVehicleWiseReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    'Dim Refresh As Boolean = False
    Public Shared ArrInvoice_Arr As New ArrayList()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptVehicleWiseReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Private Sub FrmPrintFreshInvoice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
    End Sub
    'Sub LoadLocation()
    '    Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER  "
    '    cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
    '    cbgLocation.ValueMember = "Code"
    '    cbgLocation.DisplayMember = "Name"

    'End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        loadReport()
    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        'LoadLocation()
        'chkLocationAll.CheckState = CheckState.Checked
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.Rows.Clear()

    End Sub
    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If txtLocation1Mult.arrValueMember IsNot Nothing AndAlso txtLocation1Mult.arrValueMember.Count > 0 Then
                Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(txtLocation1Mult.arrValueMember)
                arrHeader.Add((" Location : " + strLocationName + " "))
            Else
                arrHeader.Add((" Location : All"))
            End If

            If txtCustomerMult.arrValueMember IsNot Nothing AndAlso txtCustomerMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomerMult.arrValueMember))
            Else
                arrHeader.Add((" Customer: All"))
            End If
            If txtVehicleNoMult.arrValueMember IsNot Nothing AndAlso txtVehicleNoMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Vehicle : " + clsCommon.GetMulcallStringWithComma(txtVehicleNoMult.arrValueMember))
            Else
                arrHeader.Add((" Vehicle: All"))
            End If



            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Fresh Invoice", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Fresh Invoice", gv, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Public Sub loadReport()
        ''changes by Shivani [BM00000007756]
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If
        'If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select atleast single Location or select all.")
        '    Exit Sub
        'End If
        Dim sQuery As String = " select  Cast(1 as BIT) as 'Check',TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code,TSPL_SD_SALE_INVOICE_HEAD.vehicleNo, TSPL_COMPANY_MASTER.Comp_Name,TSPL_LOCATION_MASTER.Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2  as Loc_Add2,TSPL_LOCATION_MASTER.Add3  as Loc_Add3,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2    , TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_For_Comp.City_Name )>0 then ', '+TSPL_CITY_MASTER_For_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER.STATE_NAME  )>0 then TSPL_STATE_MASTER.STATE_NAME  else '' end +    case when LEN(TSPL_COMPANY_MASTER.Tin_No  )>0 then ', '+TSPL_COMPANY_MASTER.Tin_No else ' ' end  as Comp_address  ,ISNULL(tspl_company_Master.ADD2,'')   as Compaddress2,ISNULL(tspl_company_Master.ADD3,'') as Compaddress3,  TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo,convert(varchar ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as InvoiceDate , TSPL_LOCATION_MASTER.Location_Desc , TSPL_COMPANY_MASTER.Comp_Name as CompName,  TSPL_CUSTOMER_MASTER.Customer_Name as CustName,cust_category_desc,TSPL_CUSTOMER_MASTER.Add1 as Customer_Add1,TSPL_CUSTOMER_MASTER.add2 as   customer_Add2,TSPL_CUSTOMER_MASTER.Add3 as customer_Add3 , TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as DocAmt ,Against_Shipment_No    from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code   left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.comp_code  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_COMPANY_MASTER.State  left outer join TSPL_CITY_MASTER  as TSPL_CITY_MASTER_For_Comp on TSPL_CITY_MASTER_For_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code  left join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code =TSPL_CUSTOMER_MASTER.cust_category_code where  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='FS' "

        sQuery += " and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        If txtLocation1Mult.arrValueMember IsNot Nothing AndAlso txtLocation1Mult.arrValueMember.Count > 0 Then
            sQuery += " and TSPL_LOCATION_MASTER. Location_Code   IN (" + clsCommon.GetMulcallString(txtLocation1Mult.arrValueMember) + ") "
        End If
        If txtCustomerMult.arrValueMember IsNot Nothing AndAlso txtCustomerMult.arrValueMember.Count > 0 Then
            sQuery += "and TSPL_SD_SALE_INVOICE_HEAD.customer_code in (" + clsCommon.GetMulcallString(txtCustomerMult.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtVehicleNoMult.arrValueMember IsNot Nothing AndAlso txtVehicleNoMult.arrValueMember.Count > 0 Then
            sQuery += "and TSPL_SD_SALE_INVOICE_HEAD.vehicle_code in (" + clsCommon.GetMulcallString(txtVehicleNoMult.arrValueMember) + ") " + Environment.NewLine
        End If
        'If txtCustomerCategoryMult.arrValueMember IsNot Nothing AndAlso txtCustomerCategoryMult.arrValueMember.Count > 0 Then
        '    sQuery += " AND TSPL_CUSTOMER_MASTER.cust_category_code in (" + clsCommon.GetMulcallString(txtCustomerCategoryMult.arrValueMember) + ") " + Environment.NewLine
        'End If

        sQuery += " order by convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) "
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
            gv.DataSource = Nothing
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
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

        gv.Columns("vehicleNo").IsVisible = True
        gv.Columns("vehicleNo").Width = 100
        gv.Columns("vehicleNo").HeaderText = "Vehicle No "
        gv.Columns("vehicleNo").ReadOnly = False

        gv.Columns("Comp_Name").IsVisible = False
        gv.Columns("Comp_Name").Width = 100
        gv.Columns("Comp_Name").HeaderText = "Comp Name"

        gv.Columns("Comp_address").IsVisible = False
        gv.Columns("Comp_address").Width = 100
        gv.Columns("Comp_address").HeaderText = "Comp address"


        gv.Columns("InvoiceNo").IsVisible = True
        gv.Columns("InvoiceNo").Width = 100
        gv.Columns("InvoiceNo").HeaderText = "Sale Invoice No."



        gv.Columns("InvoiceDate").IsVisible = True
        gv.Columns("InvoiceDate").Width = 100
        gv.Columns("InvoiceDate").HeaderText = " Date"
        gv.Columns("InvoiceDate").FormatString = "{0:d}"

        gv.Columns("Location_Desc").IsVisible = True
        gv.Columns("Location_Desc").Width = 100
        gv.Columns("Location_Desc").HeaderText = "Location"

        gv.Columns("CustName").IsVisible = True
        gv.Columns("CustName").Width = 150
        gv.Columns("CustName").HeaderText = "Customer"

        gv.Columns("Customer_Add1").IsVisible = True
        gv.Columns("Customer_Add1").Width = 100
        gv.Columns("Customer_Add1").HeaderText = " Customer Address"

        gv.Columns("DocAmt").IsVisible = True
        gv.Columns("DocAmt").Width = 100
        gv.Columns("DocAmt").HeaderText = "Amount"

        gv.Columns("Loc_Add1").IsVisible = False
        gv.Columns("Loc_Add1").Width = 100
        gv.Columns("Loc_Add1").HeaderText = "Location Address1"
        gv.Columns("Loc_Add2").IsVisible = False
        gv.Columns("Loc_Add2").Width = 100
        gv.Columns("Loc_Add2").HeaderText = "Location Address2" '

        gv.Columns("Loc_Add3").IsVisible = False
        gv.Columns("Loc_Add3").Width = 100
        gv.Columns("Loc_Add3").HeaderText = "Location Address3"

        gv.Columns("Logo_Img").IsVisible = False
        gv.Columns("Logo_Img").Width = 100
        gv.Columns("Logo_Img").HeaderText = "Logo Img1"
        gv.Columns("Logo_Img2").IsVisible = False
        gv.Columns("Logo_Img2").Width = 100
        gv.Columns("Logo_Img2").HeaderText = "Logo Img2"

        gv.Columns("Compaddress2").IsVisible = False
        gv.Columns("Compaddress2").Width = 100
        gv.Columns("Compaddress2").HeaderText = "Company address2"
        gv.Columns("Compaddress3").IsVisible = False
        gv.Columns("Compaddress3").Width = 100
        gv.Columns("Compaddress3").HeaderText = "Company address3"



        gv.Columns("customer_Add2").IsVisible = False
        gv.Columns("customer_Add2").Width = 100
        gv.Columns("customer_Add2").HeaderText = "Customer address2"


        gv.Columns("customer_Add3").IsVisible = False
        gv.Columns("customer_Add3").Width = 100
        gv.Columns("customer_Add3").HeaderText = "Customer address3"

        gv.Columns("cust_category_desc").IsVisible = True
        gv.Columns("cust_category_desc").Width = 150
        gv.Columns("cust_category_desc").HeaderText = "Customer Category"



        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("DocAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)



        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        loadData()

    End Sub



    Sub loadData()


        ArrInvoice_Arr = New ArrayList


        Dim InvoiceNo As String = ""
        If gv.Columns("Against_Shipment_No") Is Nothing Then
            clsCommon.MyMessageBoxShow(Me, "Please refresh report again", Me.Text)
            Exit Sub
        End If
        For Each grow As GridViewRowInfo In gv.Rows
            If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                InvoiceNo = InvoiceNo + "','" + clsCommon.myCstr(grow.Cells("Against_Shipment_No").Value)
            End If
        Next

        If clsCommon.myLen(InvoiceNo) > 0 AndAlso clsCommon.myCstr(InvoiceNo).Substring(0, 3) = "','" Then
            InvoiceNo = InvoiceNo.Substring(3, InvoiceNo.Length - 3)
        End If




        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If
        'If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select atleast single Location or select all.")
        '    Exit Sub
        'End If

        Dim Qry As String = LoadPrintQuery(InvoiceNo)

        'Qry += "   ('" + InvoiceNo + "')  and TSPL_SD_SHIPMENT_DETAIL.Scheme_Item='N'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

        If dt.Rows.Count > 0 Then

            'KwalitySalesReportViewer.funreport(dt, "crptFreshInvoicePrintStatement", "Fresh Invoice Statement")
            ''KwalitySalesReportViewer.funreport(dt, "ggg", "Fresh Invoice Statement")
            'KwalitySalesReportViewer.funsubreportWithdt(dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoice(New)", "Fresh Invoice Statement", "rptCompanyAddress.rpt")
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoice(New)", "Fresh Invoice Statement", "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
            frmCRV = Nothing
        End If



    End Sub
    '==============Changed By Rohit on Apr 03,2014==========================
    ''changes by Shivani [BM00000007756]
    Public Function LoadPrintQuery(ByVal strinvoiceNo) As String
        Dim Qry As String = " select * from (select ITEMDETAIL.Conversion_factor,case when coalesce(ITEMDETAIL.Conversion_factor,0)=0 then 0 else convert(Decimal(18,2), TSPL_SD_SHIPMENT_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor/coalesce(ITEMDETAIL.Conversion_factor,1)) end as QTY_Box,TSPL_SD_SHIPMENT_Head.Sale_Invoice_No, Case When ISNULL(TSPL_SD_SHIPMENT_Head.ManualVehicle,'')<>'' Then TSPL_SD_SHIPMENT_Head.ManualVehicle WHEN ISNULL(TSPL_SD_SHIPMENT_Head.AlternateVehicle,'')<>'' Then TSPL_VEHICLE_MASTER.Number Else TSPL_SD_SHIPMENT_Head.vehicleNo End as vehicleNo, Convert(varchar,TSPL_SD_SHIPMENT_Head.Sale_Invoice_Date,103) as Sale_Invoice_Date, TSPL_SD_SHIPMENT_HEAD.RoundOffAmount, TSPL_LOCATION_MASTER.Add1  as Loc_ADd1,TSPL_LOCATION_MASTER.Add2  as LOC_ADD2,TSPL_LOCATION_MASTER.Add3 as LOC_ADD3, TSPL_STATE_MASTER.State_Name as LocationState, case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as LOCPhone,TSPL_LOCATION_MASTER.TIN_No as Loc_TIN_NO,  TSPL_SD_SHIPMENT_HEAD.Document_Code ,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date , TSPL_SD_SHIPMENT_HEAD.Lorry_No,TSPL_ITEM_MASTER.Sku_Seq ,TSPL_SD_SHIPMENT_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc as Particulars, TSPL_SD_SHIPMENT_DETAIL.Crate as QtyCrates ,TSPL_SD_SHIPMENT_DETAIL.Unit_code ,convert(Decimal(18,2), TSPL_SD_SHIPMENT_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as QtyPCS"
        Qry += " , coalesce( case when TSPL_SD_SHIPMENT_DETAIL.Sampling=1 then 0 else SUB_QTY end,0) as free_qty,"
        Qry += " TSPL_SD_SHIPMENT_DETAIL.Scheme_Item as FreeSchemeInLitres ,convert(DECIMAL(18,2),(case when TSPL_SD_SHIPMENT_DETAIL.Sampling=1then 0 else  TSPL_SD_SHIPMENT_DETAIL.Amount end)/ (TSPL_SD_SHIPMENT_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) as RatePerPcs,"
        Qry += " (case when TSPL_SD_SHIPMENT_DETAIL.Sampling=1then 0 else  TSPL_SD_SHIPMENT_DETAIL.Amount end)  as valueInRs,coalesce(TSPL_SD_SHIPMENT_DETAIL.Cash_Scheme_Amount,0) as Cash_Scheme_Amount,isnull(schemeInCrates,0)as schemeInCrates,  "
        Qry += " '' GrandTotalCrates , TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1 as comp_add1 ,TSPL_COMPANY_MASTER.Add2 as  comp_add2 ,TSPL_COMPANY_MASTER.Add3 as comp_add3 ,TSPL_COMPANY_MASTER.Fax as comp_Fax ,TSPL_COMPANY_MASTER.Email as comp_Email, case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone , TSPL_COMPANY_MASTER.Tin_No as comp_tinNo ,TSPL_SD_SHIPMENT_HEAD. Customer_Code  as cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as cust_add1 ,TSPL_CUSTOMER_MASTER. Add2 as cust_add2 ,TSPL_CUSTOMER_MASTER.Add3 cust_add3,case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' "
        Qry += " else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+  TSPL_CUSTOMER_MASTER.Phone2 Else'' End as CustPhone ,TSPL_CUSTOMER_MASTER.Fax as cust_fax ,TSPL_CUSTOMER_MASTER.Email as  cust_Email,TSPL_CUSTOMER_MASTER.WebSite as cust_website   from TSPL_SD_SHIPMENT_DETAIL   LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD .Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " & _
    " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL .UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_code" & _
    " LEFT OUTER JOIN TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code" & _
        " left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='BOX') as ITEMDETAIL on ITEMDETAIL.Item_code=TSPL_SD_SHIPMENT_DETAIL.Item_Code " & _
    " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_SD_SHIPMENT_HEAD.Comp_Code " & _
    " left outer join TSPL_CUSTOMER_MASTER  on TSPL_CUSTOMER_MASTER .Cust_Code  =TSPL_SD_SHIPMENT_HEAD .Customer_Code" & _
    " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_SD_SHIPMENT_HEAD .Bill_To_Location" & _
    " LEFT OUTER JOIN TSPL_STATE_MASTER On TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State" & _
    " Full join  (select DOCUMENT_CODE,Item_Code as Scheme_Item_Code,SUM(Qty ) AS SUB_QTY,SUM(Crate) AS schemeInCrates from TSPL_SD_SHIPMENT_DETAIL as inn where DOCUMENT_CODE in ('" + strinvoiceNo + "') and inn.Scheme_Item='Y'   group by DOCUMENT_CODE,Item_Code) " & _
         " TSPL_SD_SHIPMENT_DETAIL_Sub on TSPL_SD_SHIPMENT_DETAIL_sub.DOCUMENT_CODE=TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE and TSPL_SD_SHIPMENT_DETAIL_sub.Scheme_Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code " & _
         " LEFT OUTER JOIN TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle" & _
         " where 2=2 and  TSPL_SD_SHIPMENT_HEAD.Document_Code in   ('" + strinvoiceNo + "')  " & _
        "  and TSPL_SD_SHIPMENT_DETAIL.Scheme_Item='N'"

        Qry += " UNION aLL"

        Qry += " select ITEMDETAIL.Conversion_factor,case when coalesce(ITEMDETAIL.Conversion_factor,0)=0 then null else 0 end as QTY_Box,TSPL_SD_SHIPMENT_Head.Sale_Invoice_No, Case When ISNULL(TSPL_SD_SHIPMENT_Head.ManualVehicle,'')<>'' Then TSPL_SD_SHIPMENT_Head.ManualVehicle WHEN ISNULL(TSPL_SD_SHIPMENT_Head.AlternateVehicle,'')<>'' Then TSPL_VEHICLE_MASTER.Number Else TSPL_SD_SHIPMENT_Head.vehicleNo End as vehicleNo, Convert(varchar,TSPL_SD_SHIPMENT_Head.Sale_Invoice_Date,103) as Sale_Invoice_Date, TSPL_SD_SHIPMENT_HEAD.RoundOffAmount, TSPL_LOCATION_MASTER.Add1  as Loc_ADd1,TSPL_LOCATION_MASTER.Add2  as LOC_ADD2,TSPL_LOCATION_MASTER.Add3 as LOC_ADD3, TSPL_STATE_MASTER.State_Name as LocationState, case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as LOCPhone,TSPL_LOCATION_MASTER.TIN_No as Loc_TIN_NO,  TSPL_SD_SHIPMENT_HEAD.Document_Code ,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date , TSPL_SD_SHIPMENT_HEAD.Lorry_No, TSPL_ITEM_MASTER.Sku_Seq,TSPL_SD_SHIPMENT_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc as Particulars, TSPL_SD_SHIPMENT_DETAIL.Crate as QtyCrates ,TSPL_SD_SHIPMENT_DETAIL.Unit_code ,0 as QtyPCS"
        Qry += "  , coalesce( case when TSPL_SD_SHIPMENT_DETAIL.Sampling=1 then 0 else SUB_QTY end,0) as free_qty,"
        Qry += "  TSPL_SD_SHIPMENT_DETAIL.Scheme_Item as FreeSchemeInLitres ,0 as RatePerPcs,0  as valueInRs,0 as Cash_Scheme_Amount,isnull(schemeInCrates,0)as schemeInCrates,  "
        Qry += " '' GrandTotalCrates , TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1 as comp_add1 ,TSPL_COMPANY_MASTER.Add2 as  comp_add2 ,TSPL_COMPANY_MASTER.Add3 as comp_add3 ,TSPL_COMPANY_MASTER.Fax as comp_Fax ,TSPL_COMPANY_MASTER.Email as comp_Email, case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone , TSPL_COMPANY_MASTER.Tin_No as comp_tinNo ,TSPL_SD_SHIPMENT_HEAD. Customer_Code  as cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as cust_add1 ,TSPL_CUSTOMER_MASTER. Add2 as cust_add2 ,TSPL_CUSTOMER_MASTER.Add3 cust_add3,case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' " & _
         " else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+  TSPL_CUSTOMER_MASTER.Phone2 Else'' End as CustPhone ,TSPL_CUSTOMER_MASTER.Fax as cust_fax ,TSPL_CUSTOMER_MASTER.Email as  cust_Email,TSPL_CUSTOMER_MASTER.WebSite as cust_website   from TSPL_SD_SHIPMENT_DETAIL   LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD .Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " & _
     " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL .UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_code" & _
     " LEFT OUTER JOIN TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code" & _
      " left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='BOX' ) as ITEMDETAIL on ITEMDETAIL.Item_code=TSPL_SD_SHIPMENT_DETAIL.Item_Code " & _
     " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_SD_SHIPMENT_HEAD.Comp_Code " & _
     " left outer join TSPL_CUSTOMER_MASTER  on TSPL_CUSTOMER_MASTER .Cust_Code  =TSPL_SD_SHIPMENT_HEAD .Customer_Code" & _
     " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_SD_SHIPMENT_HEAD .Bill_To_Location" & _
     " LEFT OUTER JOIN TSPL_STATE_MASTER On TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State" & _
     " Full join  (select DOCUMENT_CODE,Item_Code as Scheme_Item_Code,SUM(Qty ) AS SUB_QTY,SUM(Crate) AS schemeInCrates from TSPL_SD_SHIPMENT_DETAIL as inn where DOCUMENT_CODE in ('" + strinvoiceNo + "') and inn.Scheme_Item='Y'   group by DOCUMENT_CODE,Item_Code) "
        Qry += " TSPL_SD_SHIPMENT_DETAIL_Sub on TSPL_SD_SHIPMENT_DETAIL_sub.DOCUMENT_CODE=TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE and TSPL_SD_SHIPMENT_DETAIL_sub.Scheme_Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code " & _
            " LEFT OUTER JOIN TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle" & _
            " where 2=2 and  TSPL_SD_SHIPMENT_HEAD.Document_Code in   ('" + strinvoiceNo + "')  "
        Qry += "  and TSPL_SD_SHIPMENT_DETAIL.Scheme_Item='y' "
        Qry += " AND TSPL_SD_SHIPMENT_DETAIL.item_CODE NOT IN (select  TSPL_SD_SHIPMENT_DETAIL.Item_Code    from TSPL_SD_SHIPMENT_DETAIL   LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD .Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " & _
    " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL .UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_code" & _
    " LEFT OUTER JOIN TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code" & _
    " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_SD_SHIPMENT_HEAD.Comp_Code " & _
    " left outer join TSPL_CUSTOMER_MASTER  on TSPL_CUSTOMER_MASTER .Cust_Code  =TSPL_SD_SHIPMENT_HEAD .Customer_Code" & _
    " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_SD_SHIPMENT_HEAD .Bill_To_Location" & _
    " LEFT OUTER JOIN TSPL_STATE_MASTER On TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State" & _
    " Full join  (select DOCUMENT_CODE,Item_Code as Scheme_Item_Code,SUM(Qty ) AS SUB_QTY,SUM(Crate) AS schemeInCrates from TSPL_SD_SHIPMENT_DETAIL as inn where DOCUMENT_CODE in ('" + strinvoiceNo + "') and inn.Scheme_Item='Y'   group by DOCUMENT_CODE,Item_Code) " & _
        " TSPL_SD_SHIPMENT_DETAIL_Sub on TSPL_SD_SHIPMENT_DETAIL_sub.DOCUMENT_CODE=TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE and TSPL_SD_SHIPMENT_DETAIL_sub.Scheme_Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code " & _
        " where 2=2 and  TSPL_SD_SHIPMENT_HEAD.Document_Code in   ('" + strinvoiceNo + "')" & _
        "  and TSPL_SD_SHIPMENT_DETAIL.Scheme_Item='N')) as final order by final.Sku_Seq "
        Return Qry
    End Function
    '========================================
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

   

    Private Sub FrmPrintFreshInvoice_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

  

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnQuickExport.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmPrintFreshInvoice & "'"))
            If txtLocation1Mult.arrValueMember IsNot Nothing AndAlso txtLocation1Mult.arrValueMember.Count > 0 Then
                Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(txtLocation1Mult.arrValueMember)
                arrHeader.Add((" Location : " + strLocationName + " "))
            Else
                arrHeader.Add((" Location : All"))
            End If

            If txtCustomerMult.arrValueMember IsNot Nothing AndAlso txtCustomerMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomerMult.arrValueMember))
            Else
                arrHeader.Add((" Customer: All"))
            End If
            If txtVehicleNoMult.arrValueMember IsNot Nothing AndAlso txtVehicleNoMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Vehicle : " + clsCommon.GetMulcallStringWithComma(txtVehicleNoMult.arrValueMember))
            Else
                arrHeader.Add((" Vehicle: All"))
            End If
            
            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub txtCustomerMult__My_Click(sender As Object, e As EventArgs) Handles txtCustomerMult._My_Click
        Dim qry As String = "select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_master order by Cust_Code"
        txtCustomerMult.arrValueMember = clsCommon.ShowMultipleSelectForm("Cust", qry, "Code", "Name", txtCustomerMult.arrValueMember, txtCustomerMult.arrDispalyMember)
    End Sub

   
    Private Sub txtLocation1Mult__My_Click(sender As Object, e As EventArgs) Handles txtLocation1Mult._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER  "
        txtLocation1Mult.arrValueMember = clsCommon.ShowMultipleSelectForm("Cust", qry, "Code", "Name", txtLocation1Mult.arrValueMember, txtLocation1Mult.arrDispalyMember)
    End Sub

    Private Sub txtVehicleNoMult__My_Click(sender As Object, e As EventArgs) Handles txtVehicleNoMult._My_Click
        Dim qry As String = "select Vehicle_Id as Code,Description as Name,Vehicle_Name,Number,Vehicle_No from TSPL_VEHICLE_MASTER order by Vehicle_Id"
        txtVehicleNoMult.arrValueMember = clsCommon.ShowMultipleSelectForm("Cust", qry, "Code", "Name", txtVehicleNoMult.arrValueMember, txtVehicleNoMult.arrDispalyMember)
    End Sub

    Private Sub btnUnSelect_Click(sender As Object, e As EventArgs) Handles btnUnSelect.Click
        Dim dt As DataTable = gv.DataSource
        For Each dr As DataRow In dt.Rows
            dr.Item("Check") = False
        Next
       
    End Sub

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        Dim dt As DataTable = gv.DataSource
        For Each dr As DataRow In dt.Rows
            dr.Item("Check") = True
        Next

    End Sub
End Class
