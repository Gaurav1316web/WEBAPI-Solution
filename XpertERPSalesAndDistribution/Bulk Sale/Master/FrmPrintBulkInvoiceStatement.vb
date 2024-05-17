Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class FrmPrintBulkInvoiceStatement
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    'Dim Refresh As Boolean = False
    Public Shared ArrInvoice_Arr As New ArrayList()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmPrintBulkInvoiceStatement)
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
    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadLocation()
        chkLocationAll.CheckState = CheckState.Checked
        RadPageView1.SelectedPage = RadPageViewPage1
        'gv.Rows.Clear()

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

        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Location or select all.")
            Exit Sub
        End If
        Dim sQuery As String = " select Cast(1 as BIT) as 'Check', Document_No ,convert(varchar,Document_Date,103) as Document_Date ,Total_Amt,Customer_Name ,Location_Desc  from TSPL_INVOICE_MASTER_BULKSALE left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_INVOICE_MASTER_BULKSALE.Location_Code left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_INVOICE_MASTER_BULKSALE.Customer_Code where"

        sQuery += "  convert(date,TSPL_INVOICE_MASTER_BULKSALE.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_INVOICE_MASTER_BULKSALE.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) "
        If chkLocationSelect.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
            sQuery += " and TSPL_LOCATION_MASTER. Location_Code   IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
        End If
        sQuery += " order by convert(date,TSPL_INVOICE_MASTER_BULKSALE.Document_Date,103) "
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



        gv.Columns("Document_No").IsVisible = True
        gv.Columns("Document_No").Width = 100
        gv.Columns("Document_No").HeaderText = "Sale Invoice No."



        gv.Columns("Document_Date").IsVisible = True
        gv.Columns("Document_Date").Width = 100
        gv.Columns("Document_Date").HeaderText = " Date"
        gv.Columns("Document_Date").FormatString = "{0:d}"

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
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Sub loadData()
        'Ticket No-ERO/19/06/19-000648, sanjay, add CLR and other field as per print from Transaction screen

        ArrInvoice_Arr = New ArrayList


        Dim InvoiceNo As String = ""
        Dim DispatchNo As String = ""

        For Each grow As GridViewRowInfo In gv.Rows
            If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                InvoiceNo = InvoiceNo + "','" + clsCommon.myCstr(grow.Cells("Document_No").Value)
                'DispatchNo = DispatchNo + "','" + clsCommon.myCstr(grow.Cells("DespatchDocumentNo").Value)
            End If
        Next

        If clsCommon.myLen(InvoiceNo) > 0 AndAlso clsCommon.myCstr(InvoiceNo).Substring(0, 3) = "','" Then
            InvoiceNo = InvoiceNo.Substring(3, InvoiceNo.Length - 3)
            'DispatchNo = DispatchNo.Substring(3, DispatchNo.Length - 3)
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
        ''For Each grow As GridViewRowInfo In gv.Rows
        ''    If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
        ''        InvoiceNo = clsCommon.myCstr(grow.Cells("Document_No").Value)
        ''        'DispatchNo = DispatchNo + "','" + clsCommon.myCstr(grow.Cells("DespatchDocumentNo").Value)
        ''    End If


        'Dim Qry As String = "select InvoiceNo ,MAX( Company_Name) as Company_Name,MAX(Address1)as Address1,max(Address2) as Address2,MAX(Address3)as Address3,MAX(PinNo)as PinNo, max(CinNo)as CinNo,max(Invoicedate)as Invoicedate,max(Suppliersref)as Suppliersref,max(DespatchDocumentNo)as DespatchDocumentNo,max(Despatchedthrough) as Despatchedthrough,max(Consignee) as Consignee ,max(Consignee_Address) as Consignee_Address ,max(SL_No) as SL_No ,max(despatchDate) as despatchDate ,max(TankerNo) as TankerNo ,max(MilkQty) as MilkQty ,max(Fatper) as Fatper,max(Snfper) as Snfper,max(Rate) as Rate,max(Amount) as Amount,max(RoundOffAmount)as RoundOffAmount,max(CreatedBy) as CreatedBy,max(ModifiedBy) as ModifiedBy ,max(DocumentAmount) as DocumentAmount,max(itemDesc) as itemDesc,max(fatweightage) as fatweightage, max(snfweightage)as snfweightage,max(fatratio) as fatratio,max(snfratio)as snfratio ,max(Posted) as Posted,max(ConsigneeTinno) as ConsigneeTinno,max(ConsigneePin) as ConsigneePin  from(Select TSPL_COMPANY_MASTER.Comp_Name as Company_Name,TSPL_COMPANY_MASTER.Add1 as Address1,TSPL_COMPANY_MASTER.Add2 as Address2,TSPL_COMPANY_MASTER.Add3 as Address3," & _
        ' " TSPL_COMPANY_MASTER .Pincode as PinNo,TSPL_COMPANY_MASTER.CINNo as CinNo,TSPL_INVOICE_MASTER_BULKSALE.Document_No as InvoiceNo,Convert(varchar,TSPL_INVOICE_MASTER_BULKSALE.Document_Date,106) as Invoicedate,'' as Suppliersref," & _
        ' " (SELECT STUFF((SELECT ',' + Dispatch_Code FROM TSPL_INVOICE_DETAIL_BULKSALE WHERE Document_No in('" + InvoiceNo + "') ORDER BY Dispatch_Code FOR XML PATH('')), 1, 1, '')) AS DespatchDocumentNo," & _
        ' " TSPL_LOCATION_MASTER.Location_Desc as Despatchedthrough,TSPL_CUSTOMER_MASTER.Customer_Name as Consignee,TSPL_CUSTOMER_MASTER.Add1 +case when len(TSPL_CUSTOMER_MASTER.Add2)>0 then ', '+TSPL_CUSTOMER_MASTER.Add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end as Consignee_Address,0 as SL_No,Convert(varchar,TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Date,103) as despatchDate," & _
        ' " TSPL_INVOICE_DETAIL_BULKSALE.Tanker_Code as TankerNo,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceQty as MilkQty,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceFatPer as Fatper,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceSNFPer as Snfper," & _
        ' " TSPL_INVOICE_DETAIL_BULKSALE.InvoiceRate as Rate,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount as Amount,TSPL_INVOICE_MASTER_BULKSALE.RoundOffAmount,TSPL_INVOICE_MASTER_BULKSALE.Created_By as CreatedBy,TSPL_INVOICE_MASTER_BULKSALE.Modified_By as ModifiedBy," & _
        ' " TSPL_INVOICE_MASTER_BULKSALE.Total_Amt as DocumentAmount,TSPL_ITEM_MASTER.Item_Desc as itemDesc,TSPL_BulkSalePrice_MASTER.Fat_Weightage as fatweightage ,TSPL_BulkSalePrice_MASTER.Snf_Weightage as snfweightage,TSPL_BulkSalePrice_MASTER.Fat_Ratio as fatratio,TSPL_BulkSalePrice_MASTER.Snf_Ratio as snfratio,TSPL_INVOICE_MASTER_BULKSALE.Posted,TSPL_CUSTOMER_MASTER.Tin_No as ConsigneeTinno,isnull(TSPL_CUSTOMER_MASTER.PIN_Code,'') as ConsigneePin  " & _
        ' " from TSPL_INVOICE_DETAIL_BULKSALE Left Outer Join TSPL_INVOICE_MASTER_BULKSALE  on TSPL_INVOICE_DETAIL_BULKSALE.Document_No =TSPL_INVOICE_MASTER_BULKSALE.Document_No" & _
        ' " Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_INVOICE_MASTER_BULKSALE.Comp_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_INVOICE_MASTER_BULKSALE.Customer_Code" & _
        ' " left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVOICE_DETAIL_BULKSALE .Item_Code Left outer Join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_INVOICE_DETAIL_BULKSALE .Dispatch_Code " & _
        ' " left Outer Join TSPL_BulkSalePrice_MASTER on TSPL_BulkSalePrice_MASTER.Price_Code=TSPL_Dispatch_BulkSale.Price_Code  " & _
        ' " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_INVOICE_MASTER_BULKSALE.Location_Code " & _
        ' " where 1=1 and TSPL_INVOICE_MASTER_BULKSALE .Document_No in('" + InvoiceNo + "'))as Y group by InvoiceNo"

        'Dim Qry As String = "select InvoiceNo ,MAX( Company_Name) as Company_Name,MAX(Address1)as Address1,max(Address2) as Address2,MAX(Address3)as Address3,MAX(PinNo)as PinNo, max(CinNo)as CinNo,max(Invoicedate)as Invoicedate,max(Suppliersref)as Suppliersref,max(DespatchDocumentNo)as DespatchDocumentNo,max(Despatchedthrough) as Despatchedthrough,max(Consignee) as Consignee ,max(Consignee_Address) as Consignee_Address ,max(SL_No) as SL_No ,max(despatchDate) as despatchDate ,max(TankerNo) as TankerNo ,max(MilkQty) as MilkQty ,max(Fatper) as Fatper,max(Snfper) as Snfper,max(Rate) as Rate,max(Amount) as Amount,max(RoundOffAmount)as RoundOffAmount,max(CreatedBy) as CreatedBy,max(ModifiedBy) as ModifiedBy ,max(DocumentAmount) as DocumentAmount,max(itemDesc) as itemDesc,max(fatweightage) as fatweightage, max(snfweightage)as snfweightage,max(fatratio) as fatratio,max(snfratio)as snfratio ,max(Posted) as Posted,max(ConsigneeTinno) as ConsigneeTinno,max(ConsigneePin) as ConsigneePin  from(Select TSPL_COMPANY_MASTER.Comp_Name as Company_Name,TSPL_LOCATION_MASTER.ADD1 as Address1,TSPL_LOCATION_MASTER.ADD2 as Address2,TSPL_LOCATION_MASTER.ADD4 as Address3, TSPL_COMPANY_MASTER .Pincode as PinNo,TSPL_COMPANY_MASTER.CINNo as CinNo,TSPL_INVOICE_MASTER_BULKSALE.Document_No as InvoiceNo,Convert(varchar,TSPL_INVOICE_MASTER_BULKSALE.Document_Date,106) as Invoicedate,'' as Suppliersref, Dispatch_Code as DespatchDocumentNo,  TSPL_LOCATION_MASTER.Location_Desc as Despatchedthrough,TSPL_CUSTOMER_MASTER.Customer_Name as Consignee,TSPL_CUSTOMER_MASTER.Add1 +case when len(TSPL_CUSTOMER_MASTER.Add2)>0 then ', '+TSPL_CUSTOMER_MASTER.Add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end as Consignee_Address,0 as SL_No,Convert(varchar,TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Date,103) as despatchDate, TSPL_INVOICE_DETAIL_BULKSALE.Tanker_Code as TankerNo,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceQty as MilkQty,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceFatPer as Fatper,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceSNFPer as Snfper, TSPL_INVOICE_DETAIL_BULKSALE.InvoiceRate as Rate,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount as Amount,TSPL_INVOICE_MASTER_BULKSALE.RoundOffAmount,TSPL_INVOICE_MASTER_BULKSALE.Created_By as CreatedBy,TSPL_INVOICE_MASTER_BULKSALE.Modified_By as ModifiedBy, TSPL_INVOICE_MASTER_BULKSALE.Total_Amt as DocumentAmount,TSPL_ITEM_MASTER.Item_Desc as itemDesc,TSPL_BulkSalePrice_MASTER.Fat_Weightage as fatweightage ,TSPL_BulkSalePrice_MASTER.Snf_Weightage as snfweightage,TSPL_BulkSalePrice_MASTER.Fat_Ratio as fatratio,TSPL_BulkSalePrice_MASTER.Snf_Ratio as snfratio,TSPL_INVOICE_MASTER_BULKSALE.Posted,TSPL_CUSTOMER_MASTER.Tin_No as ConsigneeTinno,isnull(TSPL_CUSTOMER_MASTER.PIN_Code,'') as ConsigneePin   from TSPL_INVOICE_DETAIL_BULKSALE Left Outer Join TSPL_INVOICE_MASTER_BULKSALE  on TSPL_INVOICE_DETAIL_BULKSALE.Document_No =TSPL_INVOICE_MASTER_BULKSALE.Document_No Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_INVOICE_MASTER_BULKSALE.Comp_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_INVOICE_MASTER_BULKSALE.Customer_Code left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVOICE_DETAIL_BULKSALE .Item_Code Left outer Join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_INVOICE_DETAIL_BULKSALE .Dispatch_Code  left Outer Join TSPL_BulkSalePrice_MASTER on TSPL_BulkSalePrice_MASTER.Price_Code=TSPL_Dispatch_BulkSale.Price_Code   left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_INVOICE_MASTER_BULKSALE.Location_Code  where 1=1 and TSPL_INVOICE_MASTER_BULKSALE .Document_No in('" + InvoiceNo + "'))as Y group by InvoiceNo"
        Dim Qry As String = "Select TSPL_INVOICE_DETAIL_BULKSALE.unit_code, case when ISNULL(tspl_company_master.Phone1,'')='(+__)__________' then '' " & _
                " else tspl_company_master.Phone1 end +  Case When ISNULL(tspl_company_master.Phone2,'')<>'(+__)__________' Then ', '+  tspl_company_master.Phone2 Else'' End as CompPhone, tspl_route_master.route_no,TSPL_LOCATION_MASTER.GSTNO as LOC_GSTIN,tspl_company_master.CINNo as Comp_CINNo,tspl_company_master.Pan_No  as Com_PAN_No,tspl_company_master.Access_Officer as Comp_FSSAI,TSPL_CUSTOMER_MASTER.pin_no as Cust_Pin_no,TSPL_CUSTOMER_MASTER.FSSAI_NO as Cust_FSSAI,TSPL_VEHICLE_MASTER.employee_id as Driver_code,tspl_employee_master.emp_Name as Driver_Name,tspl_vehicle_master.vehicle_id,tspl_vehicle_master.Description as Vehicle_Name," & _
                    " (select top 1 tspl_employee_master.emp_name from tspl_salesman_detail" & _
                    " left join tspl_employee_master on tspl_employee_master.emp_code=tspl_salesman_detail.salesman_code" & _
                    " where route_code=tspl_route_master.route_no) as  Sales_Name,case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' " & _
                    " else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+  TSPL_CUSTOMER_MASTER.Phone2 Else'' End as CustPhone,TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code," & _
            " tspl_company_master.cst_lst as Comp_CSt_LST,TSPL_LOCATION_MASTER.Tin_No as Comp_tin,TSPL_COMPANY_MASTER.Comp_Name as Company_Name, " & _
            " TSPL_COMPANY_MASTER.Comp_Name as Company_Name,TSPL_CUSTOMER_MASTER.Add1 as ConAddress1,TSPL_CUSTOMER_MASTER.Add2 as ConAddress2," & _
            " TSPL_CUSTOMER_MASTER.Add3  as ConAddress3, STATEMASTER_CUSTOMER.STATE_NAME as State_Customer,TSPL_LOCATION_MASTER.ADD1 as Address1," & _
            " TSPL_LOCATION_MASTER.ADD2 as Address2,TSPL_LOCATION_MASTER.ADD3 as Address3 , STATEMASTER_LOCATION.State_Name as State_Location," & _
            " STATEMASTER_COMPANY.STATE_NAME as State_Company, TSPL_COMPANY_MASTER .Pincode as PinNo,TSPL_COMPANY_MASTER.CINNo as CinNo,TSPL_INVOICE_MASTER_BULKSALE.Document_No as InvoiceNo," & _
             " TSPL_LOCATION_MASTER.GSTNO as GSTIN_Comp,STATEMASTER_LOCATION.GST_STATE_Code as State_Code,STATEMASTER_CUSTOMER.GST_STATE_Code  as State_Code_receiver,isnull(TSPL_CUSTOMER_MASTER.GSTNO,'') as GSTIN_Receiver," & _
            " TSPL_INVOICE_MASTER_BULKSALE.EWayBillNo as Ewaybillno,Convert(varchar,TSPL_INVOICE_MASTER_BULKSALE.EWayBillDate,106) as EwaybillDate,TSPL_ITEM_MASTER.HSN_Code as HSN ," & _
            " Convert(varchar,TSPL_INVOICE_MASTER_BULKSALE.Document_Date,106) as Invoicedate,'' as Suppliersref,TSPL_Dispatch_Detail_BulkSale.StandardRate," & _
            " Dispatch_Code as DespatchDocumentNo,  CityMaster.City_Name as Despatchedthrough,TSPL_CUSTOMER_MASTER.Customer_Name as Consignee," & _
            " TSPL_CUSTOMER_MASTER.Add1 +case when len(TSPL_CUSTOMER_MASTER.Add2)>0 then ', '+TSPL_CUSTOMER_MASTER.Add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end as Consignee_Address,TSPL_CUSTOMER_MASTER.PAN as Customer_Pan,0 as SL_No,Convert(varchar,TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Date,103) as despatchDate, case when isnull(TSPL_INVOICE_DETAIL_BULKSALE.Tanker_Code,'')<>'' then isnull(TSPL_INVOICE_DETAIL_BULKSALE.Tanker_Code,'') else isnull(TSPL_INVOICE_DETAIL_BULKSALE.TradeTanker_No ,'') end as  TankerNo ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceQty as MilkQty,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceFatPer as Fatper,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceSNFPer as Snfper, TSPL_INVOICE_DETAIL_BULKSALE.InvoiceRate as Rate,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount as Amount,TSPL_INVOICE_MASTER_BULKSALE.RoundOffAmount,TSPL_INVOICE_MASTER_BULKSALE.Created_By as CreatedBy,TSPL_INVOICE_MASTER_BULKSALE.Modified_By as ModifiedBy, TSPL_INVOICE_MASTER_BULKSALE.Total_Amt as DocumentAmount,TSPL_ITEM_MASTER.Item_Desc as itemDesc,TSPL_BulkSalePrice_MASTER.Fat_Weightage as fatweightage ,TSPL_BulkSalePrice_MASTER.Snf_Weightage as snfweightage,TSPL_BulkSalePrice_MASTER.Fat_Ratio as fatratio,TSPL_BulkSalePrice_MASTER.Snf_Ratio as snfratio,TSPL_INVOICE_MASTER_BULKSALE.Posted,TSPL_CUSTOMER_MASTER.Tin_No as ConsigneeTinno,isnull(TSPL_CUSTOMER_MASTER.PIN_Code,'') as ConsigneePin " & _
            ",TSPL_INVOICE_MASTER_BULKSAlE.Comments,tspl_company_master.Logo_Img,tspl_company_master.Logo_Img2 , TSPL_INVOICE_MASTER_BULKSALE.Electronic_Ref_No,ISNULL(TSPL_INVOICE_DETAIL_BULKSALE.CLR,0) AS CLR,TSPL_COMPANY_MASTER.Bank_Name as Comp_Bank_Name , TSPL_COMPANY_MASTER.BankAccountNo as Comp_BankAccountNo,TSPL_COMPANY_MASTER.BankIFSCCode as Comp_BankIFSCCode , TSPL_COMPANY_MASTER.BankBranchAddress as Comp_BankBranchAddress " & _
            "  from TSPL_INVOICE_DETAIL_BULKSALE Left Outer Join TSPL_INVOICE_MASTER_BULKSALE  on TSPL_INVOICE_DETAIL_BULKSALE.Document_No =TSPL_INVOICE_MASTER_BULKSALE.Document_No Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_INVOICE_MASTER_BULKSALE.Comp_Code " & _
            " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_INVOICE_MASTER_BULKSALE.Customer_Code " & _
            " left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVOICE_DETAIL_BULKSALE .Item_Code " & _
            " Left outer Join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_INVOICE_DETAIL_BULKSALE .Dispatch_Code "
        Qry += "   left Outer Join TSPL_Dispatch_Detail_BulkSale  on TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code  =TSPL_Dispatch_Detail_BulkSale.Document_No "
        Qry += " left Outer Join TSPL_BulkSalePrice_MASTER on TSPL_BulkSalePrice_MASTER.Price_Code=TSPL_Dispatch_BulkSale.Price_Code  " & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_INVOICE_MASTER_BULKSALE.Location_Code " & _
            " left outer join TSPL_CITY_MASTER as CityMaster on CityMaster.City_Code=TSPL_CUSTOMER_MASTER .City_Code " & _
            " LEFT OUTER JOIN TSPL_STATE_MASTER STATEMASTER_COMPANY ON STATEMASTER_COMPANY.State_Code=TSPL_COMPANY_MASTER.State " & _
             " LEFT OUTER JOIN TSPL_STATE_MASTER STATEMASTER_CUSTOMER ON STATEMASTER_CUSTOMER.State_Code=TSPL_CUSTOMER_MASTER.State " & _
             " LEFT OUTER JOIN TSPL_STATE_MASTER STATEMASTER_LOCATION ON STATEMASTER_LOCATION.State_Code=TSPL_LOCATION_MASTER.State " & _
             " left join tspl_route_master on tspl_route_master.route_no= tspl_customer_master.route_no " & _
             " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=tspl_route_master.vehicle_code" & _
              " left join tspl_employee_master on tspl_employee_master.emp_code=TSPL_VEHICLE_MASTER.employee_id" & _
             " where 1=1 and TSPL_INVOICE_MASTER_BULKSALE .Document_No in('" + InvoiceNo + "')"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

        If dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptInvoiceBulkSale", "Bulk Invoice Statement", clsCommon.myCDate(dt.Rows(0)("Invoicedate")), "rptCompanyAddress.rpt")
            frmCRV = Nothing
        End If
        'Next

    End Sub
    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        loadData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        loadReport()
    End Sub

    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
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

    Private Sub FrmPrintBulkInvoiceStatement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub FrmPrintBulkInvoiceStatement_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
            For Each grow As GridViewRowInfo In gv.MasterView.Rows
                grow.Cells(0).Value = False

            Next
            btnUnSelect.Text = "Select All"
        Else
            For Each grow As GridViewRowInfo In gv.MasterView.Rows
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
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmPrintBulkInvoiceStatement & "'"))
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
                clsCommon.MyExportToExcelGrid("Bulk Sale Invoice", gv, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Bulk Sale Invoice", gv, arrHeader, "Bulk Sale Invoice", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        If gv.Rows.Count > 0 Then
            Try

                Dim arrHeader As List(Of String) = New List(Of String)()

                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmPrintBulkInvoiceStatement & "'"))
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
            Catch ex As Exception
                common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub


    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        ExportToExcel(EnumExportTo.PDF)
    End Sub
End Class
