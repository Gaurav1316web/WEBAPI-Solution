Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
'========shivani Tyagi=========='
Public Class RptCSASaleRegister

    Inherits FrmMainTranScreen
    Dim isInsideLoadData As Boolean = False
    Dim atchqry As String = ""
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptCSASaleRegister)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

        btnExport.Visible = MyBase.isExport
    End Sub
#End Region

    Private Sub RptCSASaleRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        rmSend.Visibility = ElementVisibility.Collapsed
    End Sub

    Sub LoadCategory()
        Dim qry As String = "select Code,Name,Parent from ("
        qry += " select ITEM_CATEGORY_STRUCT_CODE as Code,DESCRIPTION as Name, null as Parent,0 as Sno from TSPL_ITEM_CATEGORY_STRUCTURE"
        qry += " union all"
        qry += " select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE as Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as Name,ITEM_CATEGORY_STRUCT_CODE as Parent,TSPL_ITEM_CATEGORY_STRUCT_DETAIL.CATEGORY_LEVEL as SNo from TSPL_ITEM_CATEGORY_STRUCT_DETAIL"
        qry += " left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE"
        qry += " Union all"
        qry += " select CODE,DESCRIPTION as Name,ITEM_CATEGORY_CODE as Parent,100 as SNo from TSPL_ITEM_CATEGORY_LEVEL_VALUES"
        qry += " )xxx order by Sno"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        tvCategory.DataSource = Nothing
        tvCategory.TreeViewElement.AutoSizeItems = True
        tvCategory.ShowLines = True
        tvCategory.ShowRootLines = True
        tvCategory.TreeViewElement.ViewElement.Margin = New Padding(4)
        tvCategory.ShowExpandCollapse = True
        tvCategory.TreeIndent = 15
        tvCategory.FullRowSelect = False
        tvCategory.ShowLines = True
        tvCategory.LineStyle = TreeLineStyle.Dot
        tvCategory.LineColor = Color.FromArgb(110, 153, 210)
        tvCategory.ExpandAnimation = ExpandAnimation.Opacity
        tvCategory.AllowEdit = False
        tvCategory.ShowRootLines = False
        tvCategory.TreeViewElement.AllowAlternatingRowColor = True
        tvCategory.TreeViewElement.AlternatingRowColor = Color.AliceBlue

        tvCategory.TreeViewElement.DrawBorder = True
        tvCategory.ValueMember = "Code"
        tvCategory.DisplayMember = "Name"
        tvCategory.ChildMember = "Code"
        tvCategory.ParentMember = "Parent"
        tvCategory.DataSource = dt
        tvCategory.CheckBoxes = True

        'tvCategory.ExpandAll()
    End Sub
    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Sub LoadLocation()
        Dim qry As String = "select Location_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location"
        cbgLocation.DisplayMember = "Location Description"
    End Sub
    Sub LoadItem()
        Dim qry As String = " select item_code ,item_Desc  from TSPL_ITEM_MASTER order by Item_Code "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "item_code"
        cbgItem.DisplayMember = "item_Desc"
    End Sub
    Sub LoadCustomerGroup()
        Dim Qry As String = "select Cust_Group_Code ,Cust_Group_Desc  from TSPL_CUSTOMER_GROUP_MASTER  "
        cbgCustomerGroup.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgCustomerGroup.ValueMember = "Cust_Group_Code"
        cbgCustomerGroup.DisplayMember = "Cust_Group_Desc"
    End Sub
    Sub Print(ByVal IsPrint As Exporter)
        Try
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()

            If rbtnItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one item")
            End If
            If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one location")
            End If
            If rbtnCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one customer")
            End If
            If rbtnCustomerGroupSelect.IsChecked AndAlso cbgCustomerGroup.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Customer Group")
            End If

            If clsCommon.myLen(ddlSaleType.SelectedIndex) < 0 Then
                Throw New Exception("Please select at least one Sales Type")
            End If
            If chkSerializeInv.Checked AndAlso rdbDetail.IsChecked = False Then
                Throw New Exception("Please Select Detail Type Report")
            End If
            Dim str As String = ""
            Dim strSaleInvoice As String = ""
            'Dim strSaleReturn As String = ""
            Dim strInvoicePosted As String = ""
            'Dim strReturnPosted As String = ""
            If btnAll.IsChecked Then
                strInvoicePosted = ""
                'strReturnPosted = ""
            ElseIf btnPosted.IsChecked Then
                strInvoicePosted = " TSPL_SD_SALE_INVOICE_HEAD.status=1 and "
                'strReturnPosted = " TSPL_SD_SALE_RETURN_HEAD.status=1 and "
            ElseIf btnUnposted.IsChecked Then
                strInvoicePosted = " TSPL_SD_SALE_INVOICE_HEAD.status=0 and "
                'strReturnPosted = " TSPL_SD_SALE_RETURN_HEAD.status=0 and "
            End If

            strSaleInvoice = "SELECT TSPL_SD_SALE_INVOICE_DETAIL.Conv_Factor as OldConvF,OrgRate,'" & fromDate.Value & "' as Fdate,'" & ToDate.Value & "' as Tdate, case when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='CSA' then 'Sale Invoice' end as Type, TSPL_SD_SALE_INVOICE_HEAD.Document_Code, " & _
                      "TSPL_SD_SALE_INVOICE_HEAD.Document_Date, TSPL_SD_SALE_INVOICE_HEAD.Customer_Code, " & _
                      "TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Parent_Customer_No,case when isnull (TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location ,'')='' then (TSPL_CUSTOMER_MASTER.add1+' '+TSPL_CUSTOMER_MASTER.add2+' '+TSPL_CUSTOMER_MASTER.add3)  else TSPL_SHIP_TO_LOCATION.Add1 +' ' + TSPL_SHIP_TO_LOCATION.Add2 +' ' + TSPL_SHIP_TO_LOCATION.Add3  end as Address,tspl_city_master.city_name,tspl_state_master.state_name,(tspl_customer_master.phone1+' '+tspl_customer_master.phone2) as Telephone, TSPL_SD_SALE_INVOICE_HEAD.Salesman_Code, " & _
                      "TSPL_SD_SALE_INVOICE_HEAD.Salesman_Name,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location ,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, " & _
                      "TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code, TSPL_SD_SALE_INVOICE_DETAIL.Qty as InvoiceQty, 0 as ShippedQty, " & _
                      "TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,Item_Cost as rate,Amount,Disc_Per,Disc_Amt, " & _
                      " ((case when TSPL_SD_SALE_INVOICE_DETAIL.FOC_Item=0 then 1 else 0 end)*(case when TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt=0 then 0 else 1 end) * TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount) as Amt_Less_Discount,((case when TSPL_SD_SALE_INVOICE_DETAIL.FOC_Item=0 then 1 else 0 end) *TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt) as Total_Tax_Amt, " & _
                      "TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt as TotalAmt,TSPL_SD_SALE_INVOICE_HEAD.CURRENCY_CODE," & _
                      "(case when TSPL_SD_SALE_INVOICE_HEAD.ConvRate=0 then 1 else TSPL_SD_SALE_INVOICE_HEAD.ConvRate end) as ConvRate, " & _
                      "(case when TSPL_SD_SALE_INVOICE_HEAD.ConvRate=0 then 1 else TSPL_SD_SALE_INVOICE_HEAD.ConvRate end)*TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt as Total_BaseCurr,FOC_Item,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code,Cust_Group_Desc FROM TSPL_CUSTOMER_MASTER RIGHT OUTER JOIN " & _
                      "TSPL_SD_SALE_INVOICE_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code RIGHT OUTER JOIN " & _
                      "TSPL_ITEM_MASTER RIGHT OUTER JOIN " & _
                      "TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ON " & _
                      "TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code " & _
                      " left OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER .Location_Code  = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location left outer join tspl_state_master on tspl_state_master.state_code=tspl_customer_master.state left outer join tspl_city_master on tspl_city_master.city_code=tspl_customer_master.city_code  left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code  " & _
                      " where TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='CSA' and " & strInvoicePosted & " TSPL_SD_SALE_INVOICE_HEAD.Document_Date  >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") & "' and " & _
                      " TSPL_SD_SALE_INVOICE_HEAD.Document_Date  <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") & "'  "
            If rbtnItemSelect.IsChecked Then
                strSaleInvoice += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
            End If
            If rbtnLocationSelect.IsChecked Then
                strSaleInvoice += " and  TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If rbtnCustomerSelect.IsChecked Then
                strSaleInvoice += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            End If
            If rbtnCustomerGroupSelect.IsChecked Then
                strSaleInvoice += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            End If
            Dim strInvType As String = ""
            If chkTax.IsChecked Then
                strInvType = " and TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type='T'"
            ElseIf chkRetail.IsChecked Then
                strInvType = " and TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type='R'"
            Else
                strInvType = " "
            End If
            strSaleInvoice += strInvType

            str = strSaleInvoice
         

            If clsCommon.myLen(txtUOM.Value) > 0 Then
                str = "select Fdate, Tdate,Type, Document_Code, Document_Date, Customer_Code, Customer_Name,Parent_Customer_No,address,city_name,state_name,telephone, Salesman_Code,Salesman_Name,Bill_To_Location ,Location_Desc ,xxxx.Item_Code, Item_Desc,itf_code,CONVERT(decimal(18,2), InvoiceQty) as InvoiceQty, ShippedQty, '" + txtUOM.Value + "' as Unit_code, rate,Amount,Disc_Per,Disc_Amt, Amt_Less_Discount,Total_Tax_Amt,TotalAmt,currency_code,convrate,total_basecurr,xxxx.Cust_Group_Code,xxxx.Cust_Group_Desc,xxxx.FOC_Item from ( select  Fdate, Tdate,Type, Document_Code, Document_Date, Customer_Code, Customer_Name,Parent_Customer_No,address,city_name,state_name,telephone, Salesman_Code,Salesman_Name,Bill_To_Location ,Location_Desc ,xxx.Item_Code, Item_Desc,itf_code,round(case when isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0) > 0 then  (InvoiceQty * TSPL_ITEM_UOM_DETAILForDivide.Conversion_Factor / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else InvoiceQty end,2) as InvoiceQty, ShippedQty,TSPL_ITEM_UOM_DETAIL.UOM_code as Unit_code,InvoiceQty*OrgRate as rate,Amount,Disc_Per,Disc_Amt, Amt_Less_Discount,Total_Tax_Amt,TotalAmt,currency_code,convrate,total_basecurr,xxx.Cust_Group_Code,xxx.Cust_Group_Desc,xxx.FOC_Item  from(" + str + " ) xxx  Inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xxx.Item_Code  and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + txtUOM.Value + "'  left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAILForDivide on TSPL_ITEM_UOM_DETAILForDivide.Item_Code=xxx.Item_Code and TSPL_ITEM_UOM_DETAILForDivide.UOM_Code=xxx.unit_code  )xxxx where xxxx.unit_code='" + txtUOM.Value + "' " ''and DivideConverstionFactor is not null
            End If
            'Dim qry As String
            'str = qry
            str = "select  fdate,tdate,type,document_code,document_date,Parent_Customer_No,customer_code,customer_name,address,city_name,state_name,telephone,salesman_code,salesman_name,bill_to_location,location_desc,axa.item_code,axa.item_desc,axa.itf_code,invoiceqty,shippedqty,axa.unit_code,axa.rate,amount,disc_per,disc_amt,amt_less_discount,total_tax_amt,totalamt,currency_code,convrate,total_basecurr,FOC_Item,case when FOC_Item='1' then  '0'  else TotalAmt end as TotalAmtNew,axa.Cust_Group_Code,axa.Cust_Group_Desc  from (" + str + ")axa left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=axa.item_code) as ada"



            If rdbSummary.IsChecked Then
                str = "select  Type, Document_Code,convert(varchar,Document_Date,103)as Document_Date ,MAX(ada1.Parent_Customer_No) as Parent_Customer_No, MAX(Parent_Master.Customer_Name) as ParentName,Customer_Code,ada1.Customer_Name,ada1.Cust_Group_Code,ada1.Cust_Group_Desc,address,city_name,state_name,telephone,ada1.Salesman_Code,Salesman_Name,Bill_To_Location ,Location_Desc,sum(Amount) as Amount, " & _
               "sum(Disc_Amt) as Disc_Amt,sum(Amt_Less_Discount) as Amt_Less_Discount,sum(Total_Tax_Amt) as Total_Tax_Amt,sum(TotalAmt) as TotalAmt,convert(decimal(18,2),SUM(Total_BaseCurr)) as Total_BaseCurr,sum(TotalAmtNew)as TotalAmtNew  " & _
               "from (select ada.* from( " & str & ") ada1 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=ada1.Parent_Customer_No group by Type, Document_Code,Document_Date,Customer_Code,ada1.Customer_Name,ada1.Cust_Group_Code,ada1.Cust_Group_Desc,address,city_name,state_name,telephone,ada1.Salesman_Code,Salesman_Name,Bill_To_Location ,Location_Desc  "
            ElseIf rdbDetail.IsChecked = True Then
                str = "select   Type, Fdate,Tdate,Document_Code,convert(varchar,Document_Date,103)as Document_Date ,(ada1.Parent_Customer_No) as Parent_Customer_No, (Parent_Master.Customer_Name) as ParentName,Customer_Code,ada1.Customer_Name,ada1.Cust_Group_Code,ada1.Cust_Group_Desc,address,city_name,state_name,telephone,ada1.Salesman_Code,Salesman_Name,Bill_To_Location ,Location_Desc,Item_Code,Item_Desc,cast('ITF code-'+ (itf_code) as varchar)as Itf_code,(InvoiceQty) as InvoiceQty,(ShippedQty) as ShippedQty,Unit_Code,Rate,(Amount) as Amount, Disc_Per,(Disc_Amt) as Disc_Amt,(Amt_Less_Discount) as Amt_Less_Discount,(Total_Tax_Amt) as Total_Tax_Amt,(TotalAmt) as TotalAmt,ada1.Currency_Code,ConvRate,convert(decimal(18,2),(Total_BaseCurr) )as Total_BaseCurr ,FOC_Item,TotalAmtNew  " & _
                       "from ( select ada.*  from (" & str & ")ada1 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=ada1.Parent_Customer_No "
            End If

            If chkSerializeInv.Checked Then
                str = "select final.Type, final.Fdate, final.Tdate, final.Document_Code, final.Document_Date,final.Customer_Code,final.Customer_Name,final.Cust_Group_Code,final.Cust_Group_Desc,final.Parent_Customer_No,final.ParentName,final.Address, final.city_name, final.STATE_NAME,final.telephone, final.Salesman_Code, final.Salesman_Name,final.Bill_To_Location,final.Location_Desc,TSPL_SERIAL_ITEM.Auto_Sr_No [Item Serial No],final.Item_Code, final.Item_Desc, final.Unit_code,final.TotalAmt,Final.FOC_Item ,final.TotalAmtNew from ( " + str + ") as final left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = final.Document_Code left outer join TSPL_SERIAL_ITEM on TSPL_SERIAL_ITEM.Item_Code= final.Item_Code and TSPL_SERIAL_ITEM.Document_Code= TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No "
                str += " order by convert(date,final.Document_Date,103)  "
            Else
                str += " order by convert(date,Document_Date,103)  "
            End If


            atchqry = str


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(str)
            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True

           
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt
                SetGridFormationOFGV1()
            End If
            ReStoreGridLayout()
            Gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If rbtnLocationSelect.IsChecked Then
                Dim strLocationName As String = ""
                For Each StrName As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strLocationName) > 0 Then
                        strLocationName += ", "
                    End If
                    strLocationName += StrName
                Next
                Dim strLocationCode As String = ""
                For Each StrCode As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strLocationCode) > 0 Then
                        strLocationCode += ", "
                    End If
                    strLocationCode += StrCode
                Next
                arrHeader.Add(("Location: " + strLocationName + " "))
            End If

            If IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid("Sale Register" + IIf(rdbDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, Me.Text)
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Sale Register" + IIf(rdbDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, "Sale Register", True)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub
    Sub SetGridFormationOFGV1()


        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next

        If rdbDetail.IsChecked = True Then

            Gv1.Columns("Type").IsVisible = True
            Gv1.Columns("Type").Width = 70
            Gv1.Columns("Type").HeaderText = "Type"

            Gv1.Columns("Document_Code").IsVisible = True
            Gv1.Columns("Document_Code").Width = 100
            Gv1.Columns("Document_Code").HeaderText = "Document Code"

            Gv1.Columns("Document_Date").IsVisible = True
            Gv1.Columns("Document_Date").Width = 100
            Gv1.Columns("Document_Date").HeaderText = "Document Date"

            Gv1.Columns("Parent_Customer_No").IsVisible = True
            Gv1.Columns("Parent_Customer_No").Width = 100
            Gv1.Columns("Parent_Customer_No").HeaderText = "Parent Customer Code"

            Gv1.Columns("ParentName").IsVisible = True
            Gv1.Columns("ParentName").Width = 100
            Gv1.Columns("ParentName").HeaderText = "Parent Customer Name"


            Gv1.Columns("Customer_Code").IsVisible = True
            Gv1.Columns("Customer_Code").Width = 100
            Gv1.Columns("Customer_Code").HeaderText = "Customer Code"

            Gv1.Columns("Customer_Name").IsVisible = True
            Gv1.Columns("Customer_Name").Width = 150
            Gv1.Columns("Customer_Name").HeaderText = "Customer Name"

            Gv1.Columns("Cust_Group_Code").IsVisible = True
            Gv1.Columns("Cust_Group_Code").Width = 150
            Gv1.Columns("Cust_Group_Code").HeaderText = "Customer Group Code"

            Gv1.Columns("Cust_Group_Desc").IsVisible = True
            Gv1.Columns("Cust_Group_Desc").Width = 150
            Gv1.Columns("Cust_Group_Desc").HeaderText = "Customer Group Name"


            Gv1.Columns("address").IsVisible = True
            Gv1.Columns("address").Width = 150
            Gv1.Columns("address").HeaderText = "Address"

            Gv1.Columns("city_name").IsVisible = True
            Gv1.Columns("city_name").Width = 100
            Gv1.Columns("city_name").HeaderText = "City"

            Gv1.Columns("state_name").IsVisible = True
            Gv1.Columns("state_name").Width = 100
            Gv1.Columns("state_name").HeaderText = "State"

            Gv1.Columns("telephone").IsVisible = True
            Gv1.Columns("telephone").Width = 120
            Gv1.Columns("telephone").HeaderText = "Telephone Number"

            Gv1.Columns("Salesman_Code").IsVisible = True
            Gv1.Columns("Salesman_Code").Width = 100
            Gv1.Columns("Salesman_Code").HeaderText = "Salesman Code"

            Gv1.Columns("Salesman_Name").IsVisible = True
            Gv1.Columns("Salesman_Name").Width = 100
            Gv1.Columns("Salesman_Name").HeaderText = "Salesman Name"

            Gv1.Columns("Bill_To_Location").IsVisible = True
            Gv1.Columns("Bill_To_Location").Width = 100
            Gv1.Columns("Bill_To_Location").HeaderText = "Location Code"

            Gv1.Columns("Location_Desc").IsVisible = True
            Gv1.Columns("Location_Desc").Width = 100
            Gv1.Columns("Location_Desc").HeaderText = "Location Desc"

            Gv1.Columns("Item_Code").IsVisible = True
            Gv1.Columns("Item_Code").Width = 100
            Gv1.Columns("Item_Code").HeaderText = "Item Code"

            Gv1.Columns("Item_Desc").IsVisible = True
            Gv1.Columns("Item_Desc").Width = 100
            Gv1.Columns("Item_Desc").HeaderText = "Item Desc"

            Gv1.Columns("Unit_code").IsVisible = True
            Gv1.Columns("Unit_code").Width = 100
            Gv1.Columns("Unit_code").HeaderText = "Unit_code"


            Gv1.Columns("FOC_Item").IsVisible = True
            Gv1.Columns("FOC_Item").Width = 80
            Gv1.Columns("FOC_Item").HeaderText = "Scheme Item"


            Gv1.Columns("TotalAmt").IsVisible = True
            Gv1.Columns("TotalAmt").Width = 100
            Gv1.Columns("TotalAmt").HeaderText = "Gross Amt "

            Gv1.Columns("TotalAmtNew").IsVisible = True
            Gv1.Columns("TotalAmtNew").Width = 150
            Gv1.Columns("TotalAmtNew").HeaderText = "Total Amount"

            If chkSerializeInv.Checked Then
                Gv1.Columns("Item Serial No").IsVisible = True
                Gv1.Columns("Item Serial No").Width = 80
            Else
                Gv1.Columns("itf_code").IsVisible = True
                Gv1.Columns("itf_code").Width = 70
                Gv1.Columns("itf_code").HeaderText = "ITF Code"

                Gv1.Columns("InvoiceQty").IsVisible = True
                Gv1.Columns("InvoiceQty").Width = 100
                Gv1.Columns("InvoiceQty").HeaderText = "Qty"

                If clsCommon.myLen(txtUOM.Value) > 0 Then
                    Gv1.Columns("rate").IsVisible = False
                Else
                    Gv1.Columns("rate").IsVisible = True
                End If
                Gv1.Columns("rate").Width = 100
                Gv1.Columns("rate").HeaderText = "Item Rate"

                Gv1.Columns("Amount").IsVisible = True
                Gv1.Columns("Amount").Width = 100
                Gv1.Columns("Amount").HeaderText = "Amount"

                Gv1.Columns("Disc_Per").IsVisible = True
                Gv1.Columns("Disc_Per").Width = 100
                Gv1.Columns("Disc_Per").HeaderText = "Disc %"

                Gv1.Columns("Disc_Amt").IsVisible = True
                Gv1.Columns("Disc_Amt").Width = 100
                Gv1.Columns("Disc_Amt").HeaderText = "Disc Amt"

                Gv1.Columns("Amt_Less_Discount").IsVisible = True
                Gv1.Columns("Amt_Less_Discount").Width = 100
                Gv1.Columns("Amt_Less_Discount").HeaderText = "Amt Less Discount"

                Gv1.Columns("Total_Tax_Amt").IsVisible = True
                Gv1.Columns("Total_Tax_Amt").Width = 100
                Gv1.Columns("Total_Tax_Amt").HeaderText = "Total Tax Amt"


                '' currency details
                If objCommonVar.IsDemoERP Then
                    Gv1.Columns("Currency_Code").IsVisible = True
                    Gv1.Columns("Currency_Code").Width = 100
                    Gv1.Columns("Currency_Code").HeaderText = "Currency Code"

                    Gv1.Columns("ConvRate").IsVisible = True
                    Gv1.Columns("ConvRate").Width = 100
                    Gv1.Columns("ConvRate").HeaderText = "Conversion Rate"

                    Gv1.Columns("Total_BaseCurr").IsVisible = True
                    Gv1.Columns("Total_BaseCurr").Width = 100
                    Gv1.Columns("Total_BaseCurr").HeaderText = "Total In Base Currency"

                End If


            End If
        ElseIf rdbSummary.IsChecked Then

            Gv1.Columns("Type").IsVisible = True
            Gv1.Columns("Type").Width = 130
            Gv1.Columns("Type").HeaderText = "Type"

            Gv1.Columns("Document_Code").IsVisible = True
            Gv1.Columns("Document_Code").Width = 100
            Gv1.Columns("Document_Code").HeaderText = "Document Code"

            Gv1.Columns("Document_Date").IsVisible = True
            Gv1.Columns("Document_Date").Width = 100
            Gv1.Columns("Document_Date").HeaderText = "Document Date"

            Gv1.Columns("Parent_Customer_No").IsVisible = True
            Gv1.Columns("Parent_Customer_No").Width = 100
            Gv1.Columns("Parent_Customer_No").HeaderText = "Parent Customer Code"

            Gv1.Columns("ParentName").IsVisible = True
            Gv1.Columns("ParentName").Width = 100
            Gv1.Columns("ParentName").HeaderText = "Parent Customer Name"

            Gv1.Columns("Customer_Code").IsVisible = True
            Gv1.Columns("Customer_Code").Width = 100
            Gv1.Columns("Customer_Code").HeaderText = "Customer Code"

            Gv1.Columns("Customer_Name").IsVisible = True
            Gv1.Columns("Customer_Name").Width = 150
            Gv1.Columns("Customer_Name").HeaderText = "Customer Name"

            Gv1.Columns("Cust_Group_Code").IsVisible = True
            Gv1.Columns("Cust_Group_Code").Width = 150
            Gv1.Columns("Cust_Group_Code").HeaderText = "Customer Group Code"

            Gv1.Columns("Cust_Group_Desc").IsVisible = True
            Gv1.Columns("Cust_Group_Desc").Width = 150
            Gv1.Columns("Cust_Group_Desc").HeaderText = "Customer Group Name"

            Gv1.Columns("address").IsVisible = True
            Gv1.Columns("address").Width = 120
            Gv1.Columns("address").HeaderText = "Address"

            Gv1.Columns("city_name").IsVisible = True
            Gv1.Columns("city_name").Width = 100
            Gv1.Columns("city_name").HeaderText = "City"

            Gv1.Columns("state_name").IsVisible = True
            Gv1.Columns("state_name").Width = 100
            Gv1.Columns("state_name").HeaderText = "State"

            Gv1.Columns("telephone").IsVisible = True
            Gv1.Columns("telephone").Width = 120
            Gv1.Columns("telephone").HeaderText = "Telephone Number"

            Gv1.Columns("Salesman_Code").IsVisible = True
            Gv1.Columns("Salesman_Code").Width = 100
            Gv1.Columns("Salesman_Code").HeaderText = "Salesman Code"

            Gv1.Columns("Salesman_Name").IsVisible = True
            Gv1.Columns("Salesman_Name").Width = 100
            Gv1.Columns("Salesman_Name").HeaderText = "Salesman Name"

            Gv1.Columns("Bill_To_Location").IsVisible = True
            Gv1.Columns("Bill_To_Location").Width = 100
            Gv1.Columns("Bill_To_Location").HeaderText = "Location Code"

            Gv1.Columns("Location_Desc").IsVisible = True
            Gv1.Columns("Location_Desc").Width = 100
            Gv1.Columns("Location_Desc").HeaderText = "Location Desc"

            Gv1.Columns("Amount").IsVisible = True
            Gv1.Columns("Amount").Width = 100
            Gv1.Columns("Amount").HeaderText = "Amount"

            'Gv1.Columns("TotalAmtNew").IsVisible = True
            'Gv1.Columns("TotalAmtNew").Width = 50
            'Gv1.Columns("TotalAmtNew").HeaderText = "Disc %"

            Gv1.Columns("Disc_Amt").IsVisible = True
            Gv1.Columns("Disc_Amt").Width = 100
            Gv1.Columns("Disc_Amt").HeaderText = "Disc Amt"

            Gv1.Columns("Amt_Less_Discount").IsVisible = True
            Gv1.Columns("Amt_Less_Discount").Width = 100
            Gv1.Columns("Amt_Less_Discount").HeaderText = "Amt Less Discount"

            Gv1.Columns("Total_Tax_Amt").IsVisible = True
            Gv1.Columns("Total_Tax_Amt").Width = 100
            Gv1.Columns("Total_Tax_Amt").HeaderText = "Total Tax Amt"

            Gv1.Columns("TotalAmt").IsVisible = True
            Gv1.Columns("TotalAmt").Width = 150
            Gv1.Columns("TotalAmt").HeaderText = "Gross Amt"

            Gv1.Columns("TotalAmtNew").IsVisible = True
            Gv1.Columns("TotalAmtNew").Width = 150
            Gv1.Columns("TotalAmtNew").HeaderText = "Total Amount"
            If objCommonVar.IsDemoERP Then
                Gv1.Columns("Total_BaseCurr").IsVisible = True
                Gv1.Columns("Total_BaseCurr").Width = 100
                Gv1.Columns("Total_BaseCurr").HeaderText = "Total In Base Currency"
            End If



        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item4 As New GridViewSummaryItem("TotalAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item11 As New GridViewSummaryItem("TotalAmtNew", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)

        If Not chkSerializeInv.Checked Then
            Dim item5 As New GridViewSummaryItem("Total_Tax_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)

            Dim item6 As New GridViewSummaryItem("Amt_Less_Discount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Disc_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)


            If Not rdbSummary.IsChecked AndAlso clsCommon.myLen(txtUOM.Value) > 0 Then
                Dim item8 As New GridViewSummaryItem("InvoiceQty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item8)
            End If



            Dim item9 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)

            Dim item10 As New GridViewSummaryItem("Total_BaseCurr", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)



            'gv1.GroupDescriptors.Add(New GridGroupByExpression("Item_Code as Item format ""{0}: {1}"" Group By Item_Code"))
            'gv1.GroupDescriptors.Add(New GridGroupByExpression("Item_Desc as Item format ""{0}: {1}"" Group By Item_Desc"))
            'gv1.ShowGroupPanel = False
            'gv1.MasterTemplate.AutoExpandGroups = True
        End If
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = False
    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        LoadCustomer()
        LoadLocation()
        LoadItem()
        LoadCategory()
        LoadCustomerGroup()
        rbtnCategoryAll.IsChecked = True
        rbtnCustomerAll.IsChecked = True
        rbtnLocationAll.IsChecked = True
        rbtnItemAll.IsChecked = True
        rbtnCustomerGroupAll.IsChecked = True
        rdbSummary.IsChecked = True
        'ddlSaleType.SelectedIndex = 0
        btnPosted.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Print(Exporter.Refresh)
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub txtUOM__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtUOM._MYValidating
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
    End Sub

    Private Sub rbtnCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCustomerAll.ToggleStateChanged
        cbgCustomer.Enabled = rbtnCustomerSelect.IsChecked
    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged
        tvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    Private Sub rbtnCustomerGroupAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCustomerGroupAll.ToggleStateChanged
        cbgCustomerGroup.Enabled = rbtnCustomerGroupSelect.IsChecked
    End Sub

    Private Sub rbtnLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged
        cbgLocation.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub rbtnItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnItemAll.ToggleStateChanged
        cbgItem.Enabled = rbtnItemSelect.IsChecked
    End Sub



    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub



    Private Sub Gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        If rdbSummary.IsChecked Then
            If Gv1.Rows.Count > 0 Then
                Dim strTransType As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Type").Value)
                Dim strDoc = Gv1.CurrentRow.Cells("Document_Code").Value
                If strTransType = "Sale Invoice" Then
                    strTransType = "SD-IN"
                    'ElseIf clsCommon.CompairString(strTransType, "Product Sale") = CompairStringResult.Equal Then
                    '    strTransType = "Product_Sale"
                Else
                    strTransType = "Sale Return"
                End If
                Select Case strTransType
                    Case "SD-IN"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASaleInvoice, strDoc)
                   Case "Sale Return"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleReturnProductSale, strDoc)
                End Select
            End If
        End If
    End Sub

    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
            Exit Sub
        End If
        Print(Exporter.Excel)
    End Sub

    Private Sub rmPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmPDF.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
            Exit Sub
        End If
        Print(Exporter.PDF)
    End Sub

    Private Sub rmSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.RptCSASaleRegister
        frm.ShowDialog()
    End Sub

    Private Sub rmSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSend.Click
        'Try
        '    Dim repotype As String = ""
        '    Dim invtype As String = ""
        '    If Gv1.Rows.Count <= 0 Then
        '        clsCommon.MyMessageBoxShow("No Data Found To Send Mail", Me.Text)
        '        Return
        '    End If

        '    Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.FrmSaleRegisterDemo)

        '    If obj Is Nothing Then
        '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
        '        Return
        '    End If
        '    If clsCommon.myLen(obj.mailsubjct) <= 0 Then
        '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
        '        Return
        '    End If

        '    Try

        '        Dim strEmail As String = ""


        '        If Process.GetProcessesByName("OutLook").Length < 1 Then
        '            'restarts the Process
        '            Process.Start("OutLook.exe")
        '        End If
        '        Dim oApp As New Outlook.Application()
        '        Dim oMsg As Outlook.MailItem

        '        If chkAll.IsChecked Then
        '            invtype = ""
        '        ElseIf chkTax.IsChecked Then
        '            invtype = "Tax Invoice"
        '        ElseIf chkRetail.IsChecked Then
        '            invtype = "Retail Invoice"
        '        End If

        '        If rdbDetail.IsChecked Then
        '            repotype = "Detail Report"
        '        Else
        '            repotype = "Summary Report"
        '        End If

        '        oMsg = DirectCast(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
        '        strEmail = clsDBFuncationality.getSingleValue("select distinct (select ','+Email_id from tspl_employee_master where emp_code in ('" & obj.usercode & "') for xml path('')) ")

        '        Try
        '            If strEmail.Substring(0, 1) = "," Then
        '                strEmail = strEmail.Substring(1, strEmail.Length - 1)
        '            End If
        '        Catch ex As Exception
        '        End Try

        '        If clsCommon.myLen(strEmail) <= 0 Then
        '            clsCommon.MyMessageBoxShow("No Mail ID Found for Sending Mail,Please Fill E-Mail Id In Employee Master", Me.Text)
        '            Return
        '        End If

        '        oMsg.Body = obj.mailbody

        '        oMsg.Body = oMsg.Body.Replace("'", " ").Replace("`", "/")

        '        If oMsg.Body.Contains(clsEmailSMSConstants.FromDate) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Body.Contains(clsEmailSMSConstants.ToDate) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Body.Contains(clsEmailSMSConstants.ReportType) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ReportType, repotype)
        '        End If
        '        If oMsg.Body.Contains(clsEmailSMSConstants.InvoiceType) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.InvoiceType, invtype)
        '        End If


        '        oMsg.Subject = obj.mailsubjct

        '        oMsg.Subject = oMsg.Subject.Replace("'", " ").Replace("`", "/")

        '        If oMsg.Subject.Contains(clsEmailSMSConstants.FromDate) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Subject.Contains(clsEmailSMSConstants.ToDate) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Subject.Contains(clsEmailSMSConstants.ReportType) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.ReportType, repotype)
        '        End If
        '        If oMsg.Subject.Contains(clsEmailSMSConstants.InvoiceType) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.InvoiceType, invtype)
        '        End If

        '        '------------------------code for attchament-------------------------------------
        '        If obj.atchmnt = "Y" Then
        '            Dim sDisplayname As [String] = "MyAttachment"
        '            If oMsg.Body Is Nothing Then
        '                oMsg.Body = " "
        '            End If
        '            Dim iPosition As Integer = CInt(oMsg.Body.Length) + 1
        '            Dim iAtchmentType As Integer = CInt(Outlook.OlAttachmentType.olByValue)

        '            Dim strRptPath As String = ""

        '            Dim oAttachment As Outlook.Attachment = Nothing
        '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(atchqry)

        '            If dt.Rows.Count > 0 Then
        '                Dim subPath As String = Application.StartupPath + "\Mail Reports"

        '                Dim IsExists As Boolean = System.IO.Directory.Exists(subPath)

        '                If (IsExists = False) Then

        '                    System.IO.Directory.CreateDirectory(subPath)
        '                End If
        '                strRptPath = Application.StartupPath + "\Mail Reports\Sale Register.xls"
        '                transportSql.exportdata(Gv1, strRptPath, "Sheet1", False, Nothing, False, False, False, True)
        '                oAttachment = oMsg.Attachments.Add(strRptPath, iAtchmentType, iPosition, sDisplayname)
        '            End If
        '        End If
        '        '---------------------------------------------------------------------------


        '        oMsg.Recipients.Add(strEmail)
        '        oMsg.CC = "ranjana.sinha@tecxpert.in;rakesh.sharma@tecxpert.in"
        '        oMsg.Send()
        '        oMsg = Nothing
        '        oApp = Nothing

        '        clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
        '    Catch ex As Exception
        '        Throw New Exception(ex.Message)
        '    End Try

        '    Try
        '        Dim client As New System.Net.WebClient()

        '        If clsCommon.myLen(obj.smsbody) <= 0 Then
        '            Throw New Exception("Please Set First SMS Body In SMS/Email Setting")
        '        End If

        '        Dim strMes As String = ""

        '        strMes = obj.smsbody
        '        strMes = strMes.Replace("'", " ").Replace("`", "/")

        '        If strMes.Contains(clsEmailSMSConstants.FromDate) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If strMes.Contains(clsEmailSMSConstants.ToDate) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If strMes.Contains(clsEmailSMSConstants.ReportType) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.ReportType, repotype)
        '        End If
        '        If strMes.Contains(clsEmailSMSConstants.InvoiceType) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.InvoiceType, invtype)
        '        End If


        '        Dim strphone As String = clsDBFuncationality.getSingleValue("select distinct (select ','+Phone from tspl_employee_master where emp_code in ('" & obj.usercode & "') for xml path(''))  ")

        '        Try
        '            If strphone.Substring(0, 1) = "," Then
        '                strphone = strphone.Substring(1, strphone.Length - 1)
        '            End If
        '        Catch ex As Exception
        '        End Try

        '        Dim baseurl As String = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=tecxpert&password=1818948263&sendername=vipin&mobileno=91" + strphone + "&message=" + strMes
        '        Dim data As Stream = client.OpenRead(baseurl)
        '        Dim reader As StreamReader = New StreamReader(data)
        '        Dim s As String = reader.ReadToEnd()
        '        data.Close()
        '        reader.Close()

        '        clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
        '    Catch ex As Exception
        '        Throw New Exception(ex.Message)
        '    End Try
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try

    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

 
    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnQuickExport.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptCSASaleRegister & "'"))
            If rbtnLocationSelect.IsChecked Then
                Dim strLocationName As String = ""
                For Each StrName As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strLocationName) > 0 Then
                        strLocationName += ", "
                    End If
                    strLocationName += StrName
                Next
                Dim strLocationCode As String = ""
                For Each StrCode As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strLocationCode) > 0 Then
                        strLocationCode += ", "
                    End If
                    strLocationCode += StrCode
                Next
                arrHeader.Add(("Location: " + strLocationName + " "))
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
            'transportSql.exportdata(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
