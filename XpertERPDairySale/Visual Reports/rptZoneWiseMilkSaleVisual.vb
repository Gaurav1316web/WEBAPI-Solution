Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
'Imports Outlook = Microsoft.Office.Interop.Outlook
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
'Ticket no-ERO/18/11/19-001107,total of COGS column 
Public Class rptZoneWiseMilkSaleVisual
    Inherits FrmMainTranScreen

    Dim atchqry As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable
    Dim AmountinLacs As Boolean = False
    '' new varables 
    Public isDataLoad As Boolean = False
    Public dtFrom As Date
    Public dtTo As Date
    Public strType As String
    Public arrItem As ArrayList
    Public arrTransaction As ArrayList
    Public arrCat As Dictionary(Of String, Object) = Nothing
    Public Unit_Code As String = Nothing
    Public arrLocation As ArrayList
    Public arrCustomer As ArrayList
    Public arrCustGroup As ArrayList
    Public arrItemGroup As ArrayList
    '' new filters

    Dim dtCategory As DataTable
    Dim strPivotForFinalOuterQuery As String
    Dim MIS_Item_Group As String = ""
    Dim arrBack As List(Of String)
    Dim Document_No As String = ""
    Dim Document_No_Old As String = ""
    Dim FORMTYPE As String = Nothing
    Dim IsFormLoad As Boolean = False
#Region "User Defined Functions and Subroutines"
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FORMTYPE = formid
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region
#Region "Functions"
    Private Sub SetUserMgmtNew()
        '=========Update By Preeti Gupta===============
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptsaleRegisterReport)
        'MyBase.SetUserMgmt(FORMTYPE)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

        RadSplitButton1.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isExport
        'radbtnBulkExp.Visible = MyBase.isQuickExportFlag
    End Sub
#End Region

    Sub LoadTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Total Sale")
        dt.Rows.Add("Location Wise")
        dt.Rows.Add("Item Group Wise")
        dt.Rows.Add("Customer Group Wise")
        dt.Rows.Add("Item Wise")
        dt.Rows.Add("Customer Wise")
        dt.Rows.Add("Document Wise")
        dt.Rows.Add("Document Detail")
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowParticluarColumnInSalesRegisterForGopalJee, clsFixedParameterCode.ShowParticluarColumnInSalesRegisterForGopalJee, Nothing)) = 0 Then
            dt.Rows.Add("Document Info Level")
        End If
        dt.Rows.Add("Sale Register With Purchase")
        dt.Rows.Add("Net Sale")
        ddlReportType.DataSource = dt
        ddlReportType.ValueMember = "Code"
        ddlReportType.DisplayMember = "Code"
    End Sub


    Sub LoadSubCategory()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("All")
        dt.Rows.Add("Cash Sale")
        dt.Rows.Add("Scrap Sale")

        ddlSubCategory.DataSource = dt
        ddlSubCategory.ValueMember = "Code"
        ddlSubCategory.DisplayMember = "Code"
    End Sub

    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM._MYValidating
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
    End Sub


    Sub Print(ByVal IsPrint As Exporter)
        Try
            Dim Sql As String = ""
            Dim str_UOM As String = ""
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")

            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid(" Sale Register Detail :" + ddlReportType.SelectedValue, Gv1, arrHeader, Me.Text)
                Exit Sub
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Sale Register Detail" + ddlReportType.SelectedValue, Gv1, arrHeader, "Sale Register Detail", True)
                Exit Sub
            End If

            clsCommon.ProgressBarShow()
            ddlReportType.Enabled = False
            txtState.Enabled = False
            txtLocation.Enabled = False
            txtTransaction.Enabled = False
            txtItemGroup.Enabled = False
            txtItem.Enabled = False
            txtCustomer.Enabled = False
            txtCustGroup.Enabled = False
            TxtRoute.Enabled = False
            txtmultSchemeType.Enabled = False
            chk_stockingunit.Enabled = False
            TxtMultiCustomerCategory.Enabled = False
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()


            Unit_Code = txtUOM.Value
            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim str As String = ""
            Dim dt As DataTable = Nothing

            RadPageViewPage2.Text = ddlReportType.SelectedValue

            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True

            Gv1.Tag = ddlReportType.SelectedValue

            If ddlReportType.SelectedValue = "Sale Register With Purchase" Then
                '''''''Ticket No- BHA/11/10/18-000618 Show all UOM define in Item master
                '     Sql = "select (select DISTINCT ',['+UOM_Code+']' " & _
                '" from TSPL_ITEM_UOM_DETAIL " & _
                '" inner join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                '" left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
                '" where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date,('" + fromDate.Value + "'),103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,('" & ToDate.Value & "'),103)" & _
                '" for xml path('')) "

                Sql = "select (select DISTINCT ',['+UOM_Code+']' " &
                      " from TSPL_ITEM_UOM_DETAIL " &
                      " left join  TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.item_code=TSPL_ITEM_UOM_DETAIL.item_code " &
                      " left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " &
                      " where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date,('" + fromDate.Value + "'),103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,('" & ToDate.Value & "'),103)" &
                      " for xml path('')) "

                str_UOM = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Sql))

                ''richa BHA/19/06/19-000908 20 June,2019
                If clsCommon.myLen(str_UOM) > 0 Then
                    str_UOM = str_UOM.Remove(0, 1)

                    Dim array() As String = str_UOM.Split(",")

                    Dim obj As clsSaleRegisterParameterType = ReturnFilterData()


                    Sql = "select TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item,TSPL_SD_SALE_INVOICE_HEAD.Document_Code as [Document Code],  convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103 ) as [Document Date]  " &
                        ",TSPL_CUSTOMER_MASTER.Customer_Name as [Party Name]" &
                         ",TSPL_ITEM_MASTER.Item_Desc As [Item Name] " &
                      ",TSPL_SD_SALE_INVOICE_DETAIL.Line_No  " &
                       ", TSPL_SD_SALE_INVOICE_DETAIL.Unit_code AS UOM " &
                      ",  (isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0)) as Qty " '*isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)


                    For Each value As String In array
                        Sql += ",round((isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I."
                        Sql += clsCommon.myCstr(value)
                        Sql += ",2) as "
                        Sql += clsCommon.myCstr(value)
                    Next

                    Sql += ",CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' THEN 0 ELSE TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) END as Rate " &
                       ",CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' THEN 0 ELSE (TSPL_SD_SALE_INVOICE_DETAIL.Amount *(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)- case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='FS' then coalesce(TSPL_SD_SALE_INVOICE_Detail.Cash_Scheme_Amount,0)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) else 0 end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) END as [Sale Amount] " &
                     ",CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' THEN 0 ELSE (TSPL_SD_SALE_INVOICE_detail.Total_Disc_Amt*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) - case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='FS' then coalesce(TSPL_SD_SALE_INVOICE_Detail.Cash_Scheme_Amount,0)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) else 0 end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) END as [Disc Amt] " &
                   ", CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' THEN 0 ELSE (case when TSPL_SD_SALE_INVOICE_DETAIL.Line_No=1 then (TSPL_SD_SALE_INVOICE_HEAD.Total_Add_Charge+coalesce(TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount,0))*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) else 0 end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) END as [Additional Amt] " &
                    ",CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' THEN 0 ELSE ((Amount-Total_Disc_Amt)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)- case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='AS' then coalesce(TSPL_SD_SALE_INVOICE_Detail.Cash_Scheme_Amount,0)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) else 0 end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) END as [Basic Sale]  " &
                   ", CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' THEN 0 ELSE (TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate+TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate+TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate) END as [Tax Rate] " &
                   ", CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' THEN 0 ELSE TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) END as [Total Tax Amt] " &
                   ", CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' THEN 0 ELSE (Amount+TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt-Total_Disc_Amt)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) END as [Total Sale] " &
                    ",CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' THEN 0 ELSE (CASE WHEN TAXM1.TYPE='IGST' THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX1_AMT ELSE 0 END+CASE WHEN TAXM2.TYPE='IGST' THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX2_AMT ELSE 0 END+CASE WHEN TAXM3.TYPE='IGST' THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX3_AMT ELSE 0 END) END AS [IGST Amt] " &
                   ", CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' THEN 0 ELSE (CASE WHEN TAXM1.TYPE='SGST' THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX1_AMT ELSE 0 END+CASE WHEN TAXM2.TYPE='SGST' THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX2_AMT ELSE 0 END+CASE WHEN TAXM3.TYPE='SGST' THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX3_AMT ELSE 0 END) END AS [SGST Amt] " &
                   ", CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' THEN 0 ELSE (CASE WHEN TAXM1.TYPE='CGST' THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX1_AMT ELSE 0 END+CASE WHEN TAXM2.TYPE='CGST' THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX2_AMT ELSE 0 END+CASE WHEN TAXM3.TYPE='CGST' THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX3_AMT ELSE 0 END) END AS [CGST Amt] "


                    Sql += ",CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' THEN 0 ELSE tspl_item_master.cost END as [Purcahse Rate] " &
                        ",CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' THEN 0 ELSE (tspl_item_master.cost*(isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/J.Conversion_Factor) END as [Purchase Value]" &
                        ",CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' THEN 0 ELSE (TSPL_SD_SALE_INVOICE_DETAIL.Amount *(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)- case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='FS' then coalesce(TSPL_SD_SALE_INVOICE_Detail.Cash_Scheme_Amount,0)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) else 0 end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end)-(tspl_item_master.cost*(isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/J.Conversion_Factor) END as [Margin]"

                    'SANJAY

                    Sql += ",isnull(TSPL_EMPLOYEE_MASTER.Emp_Name,'') as Executive,isnull(TSPL_ZONE_MASTER.Description,'') as Zone,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category] " &
                   " from TSPL_SD_SALE_INVOICE_DETAIL  left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE  left join TSPL_VEHICLE_MASTER on TSPL_SD_SALE_INVOICE_HEAD.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id  left join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code  left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No=TSPL_SD_SALE_INVOICE_DETAIL.Delivery_Code  left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " &
                    " LEFT JOIN TSPL_STATE_MASTER ON TSPL_CUSTOMER_MASTER.State=TSPL_STATE_MASTER.STATE_CODE " &
                    " LEFT JOIN TSPL_EX_COMMERCIAL_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Against_Com_Inv_No=TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code  LEFT JOIN TSPL_SD_SALE_INVOICE_HEAD SupplInvoice ON TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary=SupplInvoice.Document_Code  LEFT JOIN TSPL_TAX_MASTER TAXM1 ON TSPL_SD_SALE_INVOICE_DETAIL.TAX1=TAXM1.TAX_CODE  LEFT JOIN TSPL_TAX_MASTER TAXM2 ON TSPL_SD_SALE_INVOICE_DETAIL.TAX2=TAXM2.TAX_CODE  LEFT JOIN TSPL_TAX_MASTER TAXM3 ON TSPL_SD_SALE_INVOICE_DETAIL.TAX3=TAXM3.TAX_CODE  " &
                   " left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  " &
                   " and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_SALE_INVOICE_DETAIL.Unit_Code " &
                   " left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE= tspl_customer_master.Service_Dealer_Code " &
                   " left join TSPL_ZONE_MASTER on TSPL_CUSTOMER_MASTER.Zone_Code=TSPL_ZONE_MASTER.Zone_Code " &
                   " left join tspl_item_master on tspl_item_master.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " &
                   " left join (SELECT item_code,conversion_factor from TSPL_ITEM_UOM_DETAIL where Default_UOM=1) J ON J.item_code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code" &
                   " left join ( " &
                   " SELECT * FROM (select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I " &
                   " PIVOT (Max(conversion_factor) FOR uom_code IN ( "
                    Sql += str_UOM
                    Sql += " )) P ) I ON TSPL_SD_SALE_INVOICE_DETAIL.Item_Code = I.item_code " &
                    " where  isnull(TSPL_SD_SALE_INVOICE_HEAD.trans_type,'ALL') not in ('CSA')   and (case when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type  IN ('FS','PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' then 'Dairy Sale' when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='FS' and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='' then 'Fresh Sale' when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS' and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='' then 'Product Sale'  when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='MCC' then 'MCC Sale' when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='EXP' and TSPL_SD_SALE_INVOICE_HEAD.Document_Type <>'MT' then 'Export Sale' when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='EXP' and TSPL_SD_SALE_INVOICE_HEAD.Document_Type ='MT' then 'Merchant Trade' WHEN TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='SD' then 'General Sale'  else  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type end) "
                    Sql += " in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") "

                    '' filter conditions
                    If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                        Sql += " and TSPL_SD_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
                    End If
                    If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                        Sql += " and TSPL_SD_SALE_INVOICE_DETAIL.Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
                    End If

                    If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                        Sql += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
                    End If

                    If obj.State_List IsNot Nothing AndAlso obj.State_List.Count > 0 Then
                        Sql += " and TSPL_STATE_MASTER.STATE_CODE in (" + clsCommon.GetMulcallString(obj.State_List) + ") "
                    End If

                    If obj.Cust_Group_Code_List IsNot Nothing AndAlso obj.Cust_Group_Code_List.Count > 0 Then
                        Sql += " and coalesce(TSPL_CUSTOMER_MASTER.Cust_Group_Code,'') in (" + clsCommon.GetMulcallString(obj.Cust_Group_Code_List) + ") "
                    End If

                    If obj.Customer_Category_List IsNot Nothing AndAlso obj.Customer_Category_List.Count > 0 Then
                        Sql += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + clsCommon.GetMulcallString(obj.Customer_Category_List) + ") "
                    End If

                    If clsCommon.myLen(obj.Document_Code) > 0 Then
                        Sql += " and TSPL_SD_SALE_INVOICE_HEAD.Document_Code = '" & obj.Document_Code & "' "
                    End If

                    Sql += " and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date,('" + fromDate.Value + "'),103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,('" & ToDate.Value & "'),103)"

                    Sql += "  order by convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103),TSPL_SD_SALE_INVOICE_HEAD.Document_Code, Line_No "
                    dt = clsDBFuncationality.GetDataTable(Sql)
                    Me.Gv1.MasterTemplate.DataSource = dt
                Else
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow("No Data Found")
                    Exit Sub
                End If
            Else
                Dim rd As SqlClient.SqlDataReader = ReturnDataReader()
                Me.Gv1.MasterTemplate.LoadFrom(rd)
                rd.Close()
            End If


            SetGridFormationOFGV1()
            'FindAndRestoreGridLayout(Me)
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType.Text)
            ReStoreGridLayout()
            Gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2

        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub
    Function ReturnDataReader() As SqlClient.SqlDataReader
        Dim obj As clsSaleRegisterParameterType = ReturnFilterData()
        Dim rd As SqlClient.SqlDataReader = clsSaleRegisterDetail.GetReportDataReader(obj)
        strPivotForFinalOuterQuery = obj.strPivotForFinalOuterQuery
        Return rd
    End Function

    Function ReturnData() As DataTable
        Dim obj As clsSaleRegisterParameterType = ReturnFilterData()
        Dim dt As DataTable = clsSaleRegisterDetail.GetReportData(obj)
        Return dt
    End Function
    Function ReturnFilterData() As clsSaleRegisterParameterType
        Dim obj As New clsSaleRegisterParameterType
        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            obj.Item_Code_List = txtItem.arrValueMember
        End If
        If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
            obj.Trans_Type_List = txtTransaction.arrValueMember
            If obj.Trans_Type_List.Contains("MCC Sale Farmer") = True OrElse obj.Trans_Type_List.Contains("MCC Sale Return Farmer") = True Then
                chkFarmerSale.Checked = True
            End If
        Else
            Dim qry As String
            qry = clsSaleRegisterDetail.GetAllSaleTransactionTypeQuery()
            Dim dtTrans As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim arrTrans As New ArrayList
            For Each dr As DataRow In dtTrans.Rows
                arrTrans.Add(clsCommon.myCstr(dr.Item("Name")))
            Next
            obj.Trans_Type_List = arrTrans
        End If
        '======================================================================================
        Dim chkCustCategoryMappInUserMaster As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count ( distinct CUSTOMER_CATEGORY) as CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')"))
        If chkCustCategoryMappInUserMaster = True Then
            Dim qry As String = " select  distinct CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "') "
            Dim dtMappCustCategory As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim arrMappCustCategory As New ArrayList
            For Each dr As DataRow In dtMappCustCategory.Rows
                arrMappCustCategory.Add(clsCommon.myCstr(dr.Item("CUSTOMER_CATEGORY")))
            Next
            obj.Login_User_Mapped_Customer_Category_List = arrMappCustCategory
        End If
        '=========================================================================================
        If txtState.arrValueMember IsNot Nothing AndAlso txtState.arrValueMember.Count > 0 Then
            obj.State_List = txtState.arrValueMember
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            obj.Location_Code_List = txtLocation.arrValueMember
        End If

        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            obj.Customer_Code_List = txtCustomer.arrValueMember
        End If
        If txtItemGroup.arrValueMember IsNot Nothing AndAlso txtItemGroup.arrValueMember.Count > 0 Then
            obj.Item_Group_List = txtItemGroup.arrValueMember
        End If
        If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
            obj.Cust_Group_Code_List = txtCustGroup.arrValueMember
        End If
        If clsCommon.myLen(Document_No) > 0 Then
            obj.Document_Code = Document_No
        End If
        If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
            obj.Route_List = TxtRoute.arrValueMember
        End If
        If txtmultSchemeType.arrValueMember IsNot Nothing AndAlso txtmultSchemeType.arrValueMember.Count > 0 Then
            obj.Scheme_Type_List = txtmultSchemeType.arrValueMember
        End If

        If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
            obj.Customer_Category_List = TxtMultiCustomerCategory.arrValueMember
            'Asked By Ashok Sir
            obj.Trans_Type_List.Remove("MCC Transfer")
            obj.Trans_Type_List.Remove("Tanker Dispatch Return")
            obj.Trans_Type_List.Remove("Transfer")
            obj.Trans_Type_List.Remove("Transfer Return")
            'Asked By Ashok Sir
        End If

        Dim Other_Cond As String = ""
        Dim strWhrCatg As String = ""
        strWhrCatg = ""

        obj.rbtnCategorySected = False
        If rbtnCategorySelect.IsChecked Then
            Dim IsApplicable As Boolean = False
            For ii As Integer = 0 To gvCategory.RowCount - 1
                If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then
                    If IsApplicable Then
                        strWhrCatg += " and "
                    End If
                    IsApplicable = True
                    strWhrCatg += "("
                    Dim arr As Dictionary(Of String, Object) = gvCategory.Rows(ii).Tag
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        ' strWhrCatg += " [" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value) + "] in ("
                        strWhrCatg += " VirtualCategoryTabel.[" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value) + "] in ("
                        Dim isFirstTime As Boolean = True
                        For Each strInn As String In arr.Keys
                            If Not isFirstTime Then
                                strWhrCatg += ","
                            End If
                            strWhrCatg += "'" + strInn + "'"
                            isFirstTime = False
                        Next
                        strWhrCatg += ")"
                    Else
                        strWhrCatg += " 2=2  "
                    End If
                    strWhrCatg += ")"
                End If
            Next
            If Not IsApplicable Then
                Throw New Exception("Please select at least one category")
            End If
            Other_Cond += " and (" + strWhrCatg + ")"
            obj.rbtnCategorySected = True
        End If
        If btnPosted.IsChecked Then
            Other_Cond += " and xx.Status=1  "
        ElseIf btnUnposted.IsChecked Then
            Other_Cond += " and xx.Status=0  "
        End If

        obj.Other_Cond = Other_Cond
        obj.Unit_Code = txtUOM.Value
        obj.ReportType = ddlReportType.SelectedValue
        obj.From_Date = fromDate.Value
        obj.To_Date = ToDate.Value
        obj.stockinguom = chk_stockingunit.Checked
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            obj.MiscSaleSubCategory = IIf(ddlSubCategory.SelectedValue = "Cash Sale", "C", IIf(ddlSubCategory.SelectedValue = "Scrap Sale", "S", ""))
        End If
        obj.Include_Debit_Credit = chkIncludeDebitCredit.Checked
        obj.Include_MCCFarmerSale = chkFarmerSale.Checked
        obj.QuickLoad = chkQuickLoad.Checked
        Return obj
    End Function
    Function GetTaxQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select TAX" & intloop & " from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select TAX" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        Return qry
    End Function
    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
            If ddlReportType.SelectedValue = "Document Detail" AndAlso Gv1.Columns(ii).Name.Contains("TCS%") = True Then
                Gv1.Columns(ii).FormatString = "{0:n3}"
            Else
                Gv1.Columns(ii).FormatString = "{0:n2}"
            End If

        Next
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowParticluarColumnInSalesRegisterForGopalJee, clsFixedParameterCode.ShowParticluarColumnInSalesRegisterForGopalJee, Nothing)) = 1 Then
            If ddlReportType.SelectedValue = "Document Detail" Then

                Gv1.Columns("Trans Type").IsVisible = True
                Gv1.Columns("Trans Type").Width = 70
                Gv1.Columns("Trans Type").HeaderText = "Trans Type"

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    Else
                        Gv1.Columns(i).IsVisible = True
                    End If
                    If Gv1.Columns(i).Name.Contains("AR") = True Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Document Wise" Then

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    Else
                        Gv1.Columns(i).IsVisible = True
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Total Sale" Then
                Gv1.Columns("Total Amount").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Warehouse Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Location Wise" Then
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Warehouse Code").IsVisible = True
                Gv1.Columns("warehouse Name").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Warehouse Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Item Group Wise" Then
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Warehouse Code").IsVisible = True
                Gv1.Columns("warehouse Name").IsVisible = True

                Gv1.Columns("Product Group Code").IsVisible = True
                Gv1.Columns("Product Group Description").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Product Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Warehouse Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Customer Group Wise" Then
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Warehouse Code").IsVisible = True
                Gv1.Columns("warehouse Name").IsVisible = True

                Gv1.Columns("Product Group Code").IsVisible = True
                Gv1.Columns("Product Group Description").IsVisible = True

                Gv1.Columns("Customer Group Code").IsVisible = True
                Gv1.Columns("Customer Group Description").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Product Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Warehouse Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Item Wise" Then

                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Warehouse Code").IsVisible = True
                Gv1.Columns("warehouse Name").IsVisible = True

                Gv1.Columns("Product Group Code").IsVisible = True
                Gv1.Columns("Product Group Description").IsVisible = True

                Gv1.Columns("Customer Group Code").IsVisible = True
                Gv1.Columns("Customer Group Description").IsVisible = True

                Gv1.Columns("Product Code").IsVisible = True
                Gv1.Columns("Product Name").IsVisible = True

                Gv1.Columns("Quantity").IsVisible = True
                Gv1.Columns("UOM").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Product Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Warehouse Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Customer Wise" Then
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Warehouse Code").IsVisible = True
                Gv1.Columns("warehouse Name").IsVisible = True

                Gv1.Columns("Customer Group Code").IsVisible = True
                Gv1.Columns("Customer Group Description").IsVisible = True


                Gv1.Columns("Product Code").IsVisible = True
                Gv1.Columns("Product Name").IsVisible = True

                Gv1.Columns("Customer Code").IsVisible = True
                Gv1.Columns("Customer Name").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Product Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Warehouse Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            End If

            If chk_amtinlacs.Checked Then
                If ddlReportType.SelectedValue = "Total Sale" OrElse ddlReportType.SelectedValue = "Document Wise" OrElse ddlReportType.SelectedValue = "Location Wise" OrElse ddlReportType.SelectedValue = "Item Group Wise" OrElse ddlReportType.SelectedValue = "Customer Group Wise" OrElse ddlReportType.SelectedValue = "Item Wise" OrElse ddlReportType.SelectedValue = "Customer Wise" Then
                    Gv1.Columns("Total Amount").HeaderText = "Total Amount (in lacs)"
                    For i As Integer = 0 To Gv1.Rows.Count - 1
                        Gv1.Rows(i).Cells("Total Amount").Value = (clsCommon.myCdbl(Gv1.Rows(i).Cells("Total Amount").Value) / 100000)
                    Next
                End If
            End If

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0
            For Each col As GridViewColumn In Gv1.Columns
                If col.Name.Contains("Amount") = True Or col.Name.Contains("Amt") = True Or col.Name.Contains("Total") = True Or strPivotForFinalOuterQuery.Contains(col.Name) = True Then
                    Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item)
                ElseIf col.Name.Contains("Rate") = True Or col.Name.Contains("%") = True Then
                    Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Avg)
                    summaryRowItem.Add(item)
                End If
            Next
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Else
            If ddlReportType.SelectedValue = "Document Info Level" Then

                Gv1.Columns("Trans Type").IsVisible = True
                Gv1.Columns("Trans Type").Width = 70
                Gv1.Columns("Trans Type").HeaderText = "Trans Type"

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    Else
                        Gv1.Columns(i).IsVisible = True
                    End If
                    If Gv1.Columns(i).Name.Contains("AR") = True Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Document Detail" Then

                Gv1.Columns("Trans Type").IsVisible = True
                Gv1.Columns("Trans Type").Width = 70
                Gv1.Columns("Trans Type").HeaderText = "Trans Type"

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    Else
                        Gv1.Columns(i).IsVisible = True
                    End If
                    If Gv1.Columns(i).Name.Contains("AR") = True Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
                Gv1.Columns("Form Type").IsVisible = False
                Gv1.Columns("Form Type").Width = 70
                Gv1.Columns("Form Type").HeaderText = "Form Type"
                Gv1.Columns("Form Type").VisibleInColumnChooser = True

            ElseIf ddlReportType.SelectedValue = "Net Sale" Then


                Gv1.Columns("Trans Type").IsVisible = True
                Gv1.Columns("Trans Type").Width = 70
                Gv1.Columns("Trans Type").HeaderText = "Trans Type"

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True
                Next

                Gv1.Columns("Scheme Type").IsVisible = False

            ElseIf ddlReportType.SelectedValue = "Sale Register With Purchase" Then

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True
                Next
                Gv1.Columns("Line_No").IsVisible = False
                Gv1.Columns("Scheme_Item").IsVisible = False

            ElseIf ddlReportType.SelectedValue = "Document Wise" Then

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    Else
                        Gv1.Columns(i).IsVisible = True
                    End If
                    If Gv1.Columns(i).Name.Contains("AR") = True Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Total Sale" Then

                Gv1.Columns("Total FAT KG").IsVisible = True
                Gv1.Columns("Total SNF KG").IsVisible = True
                Gv1.Columns("Total Amount").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Location Wise" Then

                Gv1.Columns("Total FAT KG").IsVisible = True
                Gv1.Columns("Total SNF KG").IsVisible = True
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Location Code").IsVisible = True
                Gv1.Columns("Location Name").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Item Group Wise" Then

                Gv1.Columns("Total FAT KG").IsVisible = True
                Gv1.Columns("Total SNF KG").IsVisible = True
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Location Code").IsVisible = True
                Gv1.Columns("Location Name").IsVisible = True

                Gv1.Columns("Item Group Code").IsVisible = True
                Gv1.Columns("Item Group Description").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Customer Group Wise" Then

                Gv1.Columns("Total FAT KG").IsVisible = True
                Gv1.Columns("Total SNF KG").IsVisible = True
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Location Code").IsVisible = True
                Gv1.Columns("Location Name").IsVisible = True

                Gv1.Columns("Item Group Code").IsVisible = True
                Gv1.Columns("Item Group Description").IsVisible = True

                Gv1.Columns("Customer Group Code").IsVisible = True
                Gv1.Columns("Customer Group Description").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Item Wise" Then

                Gv1.Columns("Total FAT KG").IsVisible = True
                Gv1.Columns("Total SNF KG").IsVisible = True
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Location Code").IsVisible = True
                Gv1.Columns("Location Name").IsVisible = True

                Gv1.Columns("Item Group Code").IsVisible = True
                Gv1.Columns("Item Group Description").IsVisible = True

                Gv1.Columns("Customer Group Code").IsVisible = True
                Gv1.Columns("Customer Group Description").IsVisible = True

                Gv1.Columns("Item Code").IsVisible = True
                Gv1.Columns("Item Name").IsVisible = True

                ''KUNAL
                Gv1.Columns("Quantity").IsVisible = True
                Gv1.Columns("UOM").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Customer Wise" Then

                Gv1.Columns("Total FAT KG").IsVisible = True
                Gv1.Columns("Total SNF KG").IsVisible = True
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Location Code").IsVisible = True
                Gv1.Columns("Location Name").IsVisible = True

                Gv1.Columns("Customer Group Code").IsVisible = True
                Gv1.Columns("Customer Group Description").IsVisible = True


                Gv1.Columns("Item Code").IsVisible = True
                Gv1.Columns("Item Name").IsVisible = True

                Gv1.Columns("Customer Code").IsVisible = True
                Gv1.Columns("Customer Name").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            End If

            If chk_amtinlacs.Checked Then
                If ddlReportType.SelectedValue = "Total Sale" OrElse ddlReportType.SelectedValue = "Document Wise" OrElse ddlReportType.SelectedValue = "Location Wise" OrElse ddlReportType.SelectedValue = "Item Group Wise" OrElse ddlReportType.SelectedValue = "Customer Group Wise" OrElse ddlReportType.SelectedValue = "Item Wise" OrElse ddlReportType.SelectedValue = "Customer Wise" Then
                    Gv1.Columns("Total Amount").HeaderText = "Total Amount (in lacs)"
                    For i As Integer = 0 To Gv1.Rows.Count - 1
                        Gv1.Rows(i).Cells("Total Amount").Value = (clsCommon.myCdbl(Gv1.Rows(i).Cells("Total Amount").Value) / 100000)
                    Next
                End If
            End If

            If ddlReportType.SelectedValue <> "Sale Register With Purchase" Then
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0
                For Each col As GridViewColumn In Gv1.Columns
                    If clsCommon.CompairString(col.Name, "Total FAT KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Total SNF KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "FAT KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "SNF KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Quantity") = CompairStringResult.Equal OrElse clsCommon.CompairString(col.Name, "Ltr Qty") = CompairStringResult.Equal Then
                        Dim item1 As New GridViewSummaryItem(col.Name, "{0:F3}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    ElseIf col.Name.Contains("Amount") = True Or col.Name.Contains("Amt") = True Or col.Name.Contains("Total") = True Or strPivotForFinalOuterQuery.Contains(col.Name) = True Then
                        Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item)
                    ElseIf col.Name.Contains("Rate") = True Or col.Name.Contains("%") = True Then
                        Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Avg)
                        summaryRowItem.Add(item)
                    ElseIf col.Name.Contains("COGS") = True Then
                        Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item)
                    End If
                Next
                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If

            ''RICHA AGARWAL 23 jULY,2019 ERO/11/07/19-000682
            If clsCommon.CompairString(ddlReportType.SelectedValue, "Sale Register With Purchase") = CompairStringResult.Equal Then
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0
                Dim dblColumnIndex As Integer = Gv1.Columns.IndexOf("Rate")
                For i As Integer = 8 To dblColumnIndex - 1
                    Dim strColumnName As String = clsCommon.myCstr(Gv1.Columns(i).Name)
                    Dim item As New GridViewSummaryItem(strColumnName, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item)
                Next
                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If

        End If




        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = True
        Gv1.BestFitColumns()
    End Sub
    Sub Reset()
        txtUOM.Enabled = True
        chk_stockingunit.Enabled = True
        chk_stockingunit.Checked = False
        chk_amtinlacs.Checked = False
        Document_No = ""
        Document_No_Old = ""
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtUOM.Value = ""
        LoadTypes()
        LoadSubCategory()
        ddlReportType.SelectedValue = "Total Sale"
        lblSubCategory.Visible = False
        ddlSubCategory.Visible = False
        ddlSubCategory.SelectedValue = "All"
        LoadCategory()
        txtState.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtItemGroup.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtCustGroup.arrValueMember = Nothing
        TxtRoute.arrValueMember = Nothing
        rbtnCategoryAll.IsChecked = True
        txtmultSchemeType.arrValueMember = Nothing
        TxtMultiCustomerCategory.arrValueMember = Nothing
        ddlReportType.Enabled = True
        txtState.Enabled = True
        txtLocation.Enabled = True
        txtItemGroup.Enabled = True
        txtItem.Enabled = True
        txtCustomer.Enabled = True
        txtCustGroup.Enabled = True
        TxtRoute.Enabled = True
        txtmultSchemeType.Enabled = True
        TxtMultiCustomerCategory.Enabled = True
        If clsCommon.CompairString(clsUserMgtCode.RptFreshSaleRegister1, FORMTYPE) = CompairStringResult.Equal Then
            txtTransaction.Enabled = False
        Else
            txtTransaction.arrValueMember = Nothing
            txtTransaction.Enabled = True
        End If

        ddlReportType.SelectedIndex = 0
        btnPosted.IsChecked = True
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage2.Text = ddlReportType.SelectedValue
        gvCogsDetail.DataSource = Nothing
        gvCogsSummary.DataSource = Nothing
        RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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


    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Dim obj As New clsGridLayout()
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType)
        TemplateGridview = Gv1
        Print(Exporter.Refresh)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub



    Private Sub rmSetting_Click(sender As Object, e As EventArgs) Handles rmSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.RptFreshSaleRegister1
        frm.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rptSaleRegisterDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub rptSaleRegisterDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Visible = False
        arrBack = New List(Of String)
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
        GetMIS_ITem_GroupColumn()
        'If clsCommon.myLen(MIS_Item_Group) <= 0 Then
        '    clsCommon.MyMessageBoxShow("MIS Item Group Custom field is not create in Item Structure.")
        'End If
        AmountinLacs = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AmountInLacsOnMisSaleRegister, clsFixedParameterCode.AmountInLacsOnMisSaleRegister, Nothing)) = "1", True, False))
        If AmountinLacs Then
            chk_amtinlacs.Visible = True
        Else
            chk_amtinlacs.Visible = False
        End If

        Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.EnableGrouping = True
        Gv1.ShowGroupPanel = True
        If isDataLoad Then
            fromDate.Value = dtFrom
            ToDate.Value = dtTo
            txtUOM.Value = Unit_Code
            txtLocation.arrValueMember = arrLocation
            txtItem.arrValueMember = arrItem
            txtCustomer.arrValueMember = arrCustomer
            txtTransaction.arrValueMember = arrTransaction
            txtItemGroup.arrValueMember = arrItemGroup

            If arrCat IsNot Nothing AndAlso arrCat.Count > 0 Then
                rbtnCategorySelect.IsChecked = True
                For Each str As String In arrCat.Keys
                    For ii As Integer = 0 To gvCategory.RowCount - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
                            gvCategory.Rows(ii).Cells("SEL").Value = True
                            gvCategory.Rows(ii).Tag = arrCat(str)
                        End If
                    Next
                Next
            End If
            ddlReportType.SelectedValue = strType
            Print(True)
            Me.Visible = True
        End If
        If clsCommon.CompairString(clsUserMgtCode.RptFreshSaleRegister1, FORMTYPE) = CompairStringResult.Equal Then
            Dim arr As New ArrayList
            arr.Add("Fresh Sale")
            arr.Add("Fresh Sale Return")
            txtTransaction.arrValueMember = arr
            txtTransaction.Enabled = False
        End If
        RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
        chkFarmerSale.Visible = False
        chkIncludeDebitCredit.Visible = False
        chk_stockingunit.Visible = False
        IsFormLoad = True
    End Sub
    Private Sub rbtnCategoryAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged
        gvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub
    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        DrillDown()
    End Sub
    Sub DrillDown()
        Try
            If clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Total Sale") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Total Sale") Then
                    arrBack.Add("Total Sale")
                End If
                ddlReportType.SelectedValue = "Location Wise"
                Print(Exporter.Refresh)

            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Location Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Location Wise") Then
                    arrBack.Add("Location Wise")
                End If
                ddlReportType.SelectedValue = "Item Group Wise"
                arrLocation = New ArrayList()
                arrLocation = txtLocation.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Location Code").Value))
                txtLocation.arrValueMember = tmp
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Item Group Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Item Group Wise") Then
                    arrBack.Add("Item Group Wise")
                End If
                ddlReportType.SelectedValue = "Customer Group Wise"
                arrItemGroup = New ArrayList()
                arrItemGroup = txtItemGroup.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Group Code").Value))
                txtItemGroup.arrValueMember = tmp
                Print(Exporter.Refresh)

            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Customer Group Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Customer Group Wise") Then
                    arrBack.Add("Customer Group Wise")
                End If
                ddlReportType.SelectedValue = "Item Wise"
                arrCustGroup = New ArrayList()
                arrCustGroup = txtCustGroup.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Customer Group Code").Value))
                txtCustGroup.arrValueMember = tmp
                Print(Exporter.Refresh)

            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Item Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Item Wise") Then
                    arrBack.Add("Item Wise")
                End If
                ddlReportType.SelectedValue = "Customer Wise"
                arrItem = New ArrayList()
                arrItem = txtItem.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value))
                txtItem.arrValueMember = tmp
                Print(Exporter.Refresh)

            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Customer Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Customer Wise") Then
                    arrBack.Add("Customer Wise")
                End If
                ddlReportType.SelectedValue = "Document Wise"
                arrCustomer = New ArrayList()
                arrCustomer = txtCustomer.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Customer Code").Value))
                txtCustomer.arrValueMember = tmp
                Print(Exporter.Refresh)

            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Document Wise") Then
                    arrBack.Add("Document Wise")
                End If
                ddlReportType.SelectedValue = "Document Detail"
                Document_No_Old = Document_No
                Document_No = clsCommon.myCstr(Gv1.CurrentRow.Cells("Document No").Value)
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Detail") = CompairStringResult.Equal Then
                Dim strTransType As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value)
                Dim strTransCode As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Document No").Value)
                If clsCommon.myLen(strTransType) > 0 AndAlso clsCommon.myLen(strTransCode) > 0 Then
                    Select Case strTransType
                        Case "Fresh Sale", "Dairy Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmInvoiceFreshSale, strTransCode)
                        Case "Product Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleInvoiceProductSale, strTransCode)
                        Case "Export Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesInvoice, strTransCode)
                        Case "MCC Sale Farmer"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialFarmer, strTransCode)
                        Case "MCC Sale Return Farmer"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialSaleReturnFarmer, strTransCode)
                        Case "MCC Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterial, strTransCode)
                        Case "CSA Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, strTransCode)
                        Case "Fresh Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.saleReturn, strTransCode)
                        Case "Product Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleReturnProductSale, strTransCode)
                        Case "Export Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesReturn, strTransCode)
                        Case "CSA Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransferReturn, strTransCode)
                        Case "MCC Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialSaleReturn, strTransCode)
                        Case "Tanker Dispatch Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.MCCDispatchReturn, strTransCode)
                        Case "Bulk Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmInvoiceBulkSale, strTransCode)
                        Case "Bulk Sale Trade"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmInvoiceBulkSale, strTransCode)
                        Case "Bulk Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmBulkSaleReturn, strTransCode)
                        Case "Transfer"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strTransCode)
                        Case "Transfer Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.TransferReturn, strTransCode)
                        Case "Misc Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSale, strTransCode)
                        Case "MCC Transfer"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, strTransCode)
                        Case "Merchant Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesInvoiceMT, strTransCode)
                    End Select
                End If
                ''richa BHA/18/06/19-000904
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Net Sale") = CompairStringResult.Equal Then
                Dim strTransType As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value)
                Dim strTransCode As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Document No").Value)
                Dim stritemCode As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value)
                Dim strShipmentNo As String = String.Empty
                If clsCommon.myLen(strTransType) > 0 AndAlso clsCommon.myLen(strTransCode) > 0 Then
                    Select Case strTransType
                        Case "Fresh Sale", "Dairy Sale", "Product Sale"
                            strShipmentNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Against_Shipment_No from TSPL_SD_SALE_INVOICE_HEAD where Document_Code ='" & strTransCode & "'"))
                            CogsValueSummary(stritemCode, strShipmentNo)
                        Case "Fresh Sale Return", "Product Sale Return"
                            CogsValueSummary(stritemCode, strTransCode)
                    End Select
                End If
            End If
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    ''richa BHA/18/06/19-000904 
    Sub CogsValueSummary(ByVal strItemCode As String, ByVal strCode As String)
        Try
            RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Visible
            Dim strquery As String = "select [Batch No],max(ManualBatchNo) as [Manual Batch No],[Main Item],UOM, sum(Batch_Main_Qty) as [Batch Main Qty],sum(Item_Rate) as [Item Rate], " & Environment.NewLine &
        " sum(Main_item_cost) as [Main item cost],sum(Issued_Cost) as [Issued Cost],sum(Consumed_Value) as [Consumed Value],sum(Main_item_cost)-sum(Consumed_Value) as [Gain Loss Value]," & Environment.NewLine &
        " sum(Sale_Qty) as [Sale Qty],max(Sale_Uom) as [Sale UOM],convert(decimal(18,2),sum(SaleCost)/iif(sum(Sale_Qty)=0, 1,sum(Sale_Qty))) as [Sale Cogs Per Unit],sum(SaleCost) as [Sale Cogs],sum(Main_item_cost)-sum(SaleCost) as [Difference in Main item cost vs Sale Cogs]" & Environment.NewLine &
        " from (select Document_Code ,[Batch No],ManualBatchNo ,[Main Item],UOM,case when RowNumber=1 then [Batch Main Qty] else 0 end [Batch_Main_Qty]," & Environment.NewLine &
        " case when RowNumber=1 then Main_item_cost/iif([Batch Main Qty]=0,1,[Batch Main Qty]) else 0 end [Item_Rate]," & Environment.NewLine &
        " case when RowNumber=1 then Main_item_cost else 0 end Main_item_cost,[Total Cost] as Issued_Cost, [Consumed Value] as Consumed_Value,case when RowNumber=1 then convert(decimal(18,2),(Sale_Qty/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) else 0 end Sale_Qty," & Environment.NewLine &
        " case when UOM=Sale_Uom then Sale_Uom else UOM end as Sale_Uom ,    case when RowNumber=1 then SaleCost else 0 end SaleCost  from ( select row_number() over (partition by [Batch No] order by [Batch No]) as RowNumber, *,[Required Qty]-[Consumed Qty] as [Difference Qty] from (select distinct" & Environment.NewLine &
        " TSPL_BATCH_ITEM.Qty as [Sale_Qty],TSPL_INVENTORY_MOVEMENT.UOM as Sale_Uom ,TSPL_INVENTORY_MOVEMENT.Avg_Cost as SaleCost, TSPL_BATCH_ITEM.Document_Code ,  isnull(tspl_item_uom_detail.Item_Cost,0)*isnull(TSPL_PP_BATCH_ORDER_BOM_DETAIL.Quantity,0) as Main_item_cost ,TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code as [Batch No],TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code as [Main Item],TSPL_PP_PRODUCTION_PLAN_DETAIL.Unit_Code as UOM,TSPL_PP_BATCH_ORDER_BOM_DETAIL.Quantity  as [Batch Main Qty] ,TSPL_PP_BATCH_ORDER_BOM_DETAIL.BOM_Code as [BOM No],TSPL_PP_BOM_HEAD.PROD_QUANTITY as [BOM Qty],TSPL_PP_BATCH_ORDER_BOM_DETAIL.Unit_Code as [BOM UOM] ,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE as [BOM Item],TSPL_PP_BOM_ITEM_DETAIL.QUANTITY as [Standard Qty],TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE as[Item UOM] ,isnull([Required Qty],0) as [Required Qty],isnull(Issue_Item.Issue_Qty,0) as [Issue Qty],isnull(Issue_Item.[Total Cost],0) as [Total Cost] ,isnull(CONSM_QTY,0) as [Consumed Qty] ,isnull([Consumed Value],0) as [Consumed Value],TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Date,BatchRawItem.Location_Code ,BatchRawItem.ManualBatchNo  from " & Environment.NewLine &
        " TSPL_PP_PRODUCTION_PLAN_HEAD left outer join TSPL_PP_PRODUCTION_PLAN_DETAIL on TSPL_PP_PRODUCTION_PLAN_DETAIL.Plan_Code=TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code left outer join TSPL_PP_BATCH_ORDER_BOM_DETAIL on TSPL_PP_BATCH_ORDER_BOM_DETAIL.Plan_Code=TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TSPL_PP_BATCH_ORDER_BOM_DETAIL.BOM_Code left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE=TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE	  left outer join (select Item_Code as RequiredItem,Quantity as [Required Qty],TSPL_PP_BATCH_ORDER_HEAD.Batch_Code as [BatchCode],TSPL_PP_BATCH_ORDER_HEAD.Location_Code," & Environment.NewLine &
        " TSPL_PP_BATCH_ORDER_HEAD.ManualBatchNo from TSPL_PP_BATCH_ORDER_HEAD left outer join TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL on TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Batch_Code=TSPL_PP_BATCH_ORDER_HEAD.Batch_Code)BatchRawItem on BatchRawItem.BatchCode=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code and BatchRawItem.RequiredItem=TSPL_PP_BOM_ITEM_DETAIL.Item_Code left outer join (select CONSM_QTY,Batch_Code,CONSM_ITEM_CODE,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.Fat_Amt+TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_Amt as [Consumed Value] from TSPL_PP_PRODUCTION_ENTRY   left outer join TSPL_PP_PRODUCTION_ENTRY_DETAIL on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE left outer join  TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL on TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE =TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE)TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY.Batch_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code and TSPL_PP_PRODUCTION_ENTRY.CONSM_ITEM_CODE=TSPL_PP_BOM_ITEM_DETAIL.Item_Code left join ( select TSPL_PP_ISSUE_HEAD.Issue_Code ,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.Qty,0) as Issue_Qty,TSPL_PP_ISSUE_HEAD.Batch_Code,TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code ,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.Fat_Amt,0) +isnull(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Amt,0)  as [Total Cost]  from TSPL_PP_BATCH_ORDER_HEAD " & Environment.NewLine &
        " left outer join TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL on TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Batch_Code=TSPL_PP_BATCH_ORDER_HEAD.Batch_Code " & Environment.NewLine &
        " left join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Batch_Code=TSPL_PP_BATCH_ORDER_HEAD.Batch_Code " & Environment.NewLine &
        " left join TSPL_PP_ISSUE_ITEM_DETAIL on TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code =TSPL_PP_ISSUE_HEAD.Issue_Code  ) as Issue_Item on Issue_Item.Batch_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code and TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE =Issue_Item.Item_Code  " & Environment.NewLine &
        " left join tspl_item_master on tspl_item_master.item_code=TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code  " & Environment.NewLine &
        " left join tspl_item_uom_detail on tspl_item_uom_detail.item_code=TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code and tspl_item_uom_detail.Uom_code=TSPL_PP_PRODUCTION_PLAN_DETAIL.Unit_Code " & Environment.NewLine &
        " left outer join ( select Document_Code ,Batch_No ,Manual_BatchNo,Item_Code, sum(TSPL_BATCH_ITEM.Qty) as Qty from TSPL_BATCH_ITEM  group by Document_Code ,Item_Code,Batch_No,Manual_BatchNo) TSPL_BATCH_ITEM on   BatchRawItem.[BatchCode] =TSPL_BATCH_ITEM.Batch_No AND BatchRawItem.ManualBatchNo  =TSPL_BATCH_ITEM.Manual_BatchNo " & Environment.NewLine &
        " left outer join TSPL_INVENTORY_MOVEMENT on TSPL_INVENTORY_MOVEMENT.Source_Doc_No =TSPL_BATCH_ITEM .Document_Code and TSPL_INVENTORY_MOVEMENT.Item_Code =TSPL_BATCH_ITEM.Item_Code " & Environment.NewLine &
        " ) " & Environment.NewLine &
        " as xxx where 2=2   and [Batch No] is not null and xxx.Document_Code ='" & strCode & "' AND xxx.[Main Item] ='" & strItemCode & "'" & Environment.NewLine &
        " ) as final " & Environment.NewLine &
        " left join tspl_item_uom_detail LtrUnit on LtrUnit.item_code=final.[Main Item]	 and LtrUnit.UOM_Code=final.UOM" & Environment.NewLine &
        " left join tspl_item_uom_detail StockUnit on StockUnit.item_code=final.[Main Item] and StockUnit.Stocking_Unit ='Y'" & Environment.NewLine &
        " left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=final.[Main Item] and CurrentUnit.uom_code=	final.Sale_Uom" & Environment.NewLine &
        " )z " & Environment.NewLine &
        " group by [Batch No],Document_Code ,[Main Item] ,uom " & Environment.NewLine

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strquery)
            ''richa BHA/02/07/19-000918
            If dt.Rows.Count <= 0 Then
                strquery = "select  " & Environment.NewLine &
                " [Batch No],ManualBatchNo as [Manual Batch No],[Main Item],UOM ,[Batch Main Qty] ,[Item Rate] ,convert(decimal(18,2),([Batch Main Qty] *[Item Rate] )) as  [Main item cost], 0 as [Issued Cost],0 as [Consumed Value],0 as [Gain Loss Value], convert(decimal(18,2),(Sale_Qty/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor)  Sale_Qty,case when UOM=Sale_Uom then Sale_Uom else UOM end as Sale_Uom , " & Environment.NewLine &
                " convert(decimal(18,2),SaleCost/iif( convert(decimal(18,2),(Sale_Qty/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) =0, 1, convert(decimal(18,2),(Sale_Qty/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) )) as [Sale Cogs Per Unit],SaleCost as [Sale Cogs],convert(decimal(18,2),([Batch Main Qty] *[Item Rate] ))-SaleCost as [Difference in Main item cost vs Sale Cogs] " & Environment.NewLine &
                " from (SELECT FINAL.[BATCH NO]  ,MAX(FINAL.ManualBatchNo ) AS ManualBatchNo,MAX(FINAL.[Main Item]) AS [Main Item],MAX(FINAL.UOM ) AS UOM ,SUM(FINAL.[Batch Main Qty]) AS [Batch Main Qty], max(FINAL.[Item Rate]) as [Item Rate], " & Environment.NewLine &
                " MAX(FINAL.Sale_Uom ) AS SALE_UOM,SUM(FINAL.SaleQty)  as Sale_Qty ,sum(final.SaleCost) as  SaleCost  from  " & Environment.NewLine &
                " (select  " & Environment.NewLine &
                " 1 as RowNumber,TSPL_BATCH_ITEM.Batch_No as [Batch No],TSPL_BATCH_ITEM.Manual_BatchNo as ManualBatchNo,TSPL_BATCH_ITEM.Item_Code  as [Main Item],TSPL_BATCH_ITEM.UOM, " & Environment.NewLine &
                " TSPL_BATCH_ITEM. Qty as [Batch Main Qty],TSPL_INVENTORY_MOVEMENT.Basic_Cost   as [Item Rate],  " & Environment.NewLine &
                " 0 as [Main item cost],0 as [Issued Cost],0 as [Consumed Value],0 as [Gain Loss Value],NULL as Sale_Uom ,0 as SaleQty,0 as SaleCost " & Environment.NewLine &
                " from TSPL_BATCH_ITEM " & Environment.NewLine &
                " left outer join TSPL_INVENTORY_MOVEMENT on TSPL_INVENTORY_MOVEMENT.Source_Doc_No =TSPL_BATCH_ITEM .Document_Code and TSPL_INVENTORY_MOVEMENT.Item_Code =TSPL_BATCH_ITEM.Item_Code  " & Environment.NewLine &
                " where TSPL_BATCH_ITEM.Batch_No in (select batch_no from TSPL_BATCH_ITEM where Document_Code ='" & strCode & "' and Item_Code ='" & strItemCode & "') and TSPL_BATCH_ITEM.Item_Code ='" & strItemCode & "' AND TSPL_BATCH_ITEM.In_Out_Type ='I' AND TSPL_BATCH_ITEM.Document_Type ='IC-AD' " & Environment.NewLine &
                " UNION ALL " & Environment.NewLine &
                " SELECT " & Environment.NewLine &
                " 2 as RowNumber,TSPL_BATCH_ITEM.Batch_No as [Batch No],TSPL_BATCH_ITEM.Manual_BatchNo as ManualBatchNo,TSPL_INVENTORY_MOVEMENT.Item_Code  as [Main Item],NULL AS UOM, " & Environment.NewLine &
                " 0 as [Batch Main Qty],0  as [Item Rate],  " & Environment.NewLine &
                " 0 as [Main item cost],0 as [Issued Cost],0 as [Consumed Value],0 as [Gain Loss Value],TSPL_INVENTORY_MOVEMENT.UOM as Sale_Uom ,TSPL_INVENTORY_MOVEMENT.Qty  as SaleQty,TSPL_INVENTORY_MOVEMENT.Avg_Cost  as SaleCost   " & Environment.NewLine &
                " from TSPL_INVENTORY_MOVEMENT  " & Environment.NewLine &
                " left outer join TSPL_BATCH_ITEM on TSPL_INVENTORY_MOVEMENT.Source_Doc_No =TSPL_BATCH_ITEM .Document_Code and TSPL_INVENTORY_MOVEMENT.Item_Code =TSPL_BATCH_ITEM.Item_Code  " & Environment.NewLine &
                " where TSPL_INVENTORY_MOVEMENT.Source_Doc_No ='" & strCode & "' and TSPL_INVENTORY_MOVEMENT.iTEM_CODE ='" & strItemCode & "' " & Environment.NewLine &
                " )FINAL group by FINAL.[BATCH NO],FINAL.[Main Item]) z " & Environment.NewLine &
                " left join tspl_item_uom_detail LtrUnit on LtrUnit.item_code=z.[Main Item]	 and LtrUnit.UOM_Code=z.UOM " & Environment.NewLine &
                " left join tspl_item_uom_detail StockUnit on StockUnit.item_code=z.[Main Item] and StockUnit.Stocking_Unit ='Y' " & Environment.NewLine &
                " left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=z.[Main Item] and CurrentUnit.uom_code=	z.SALE_UOM  " & Environment.NewLine
                dt = clsDBFuncationality.GetDataTable(strquery)


            End If


            gvCogsSummary.DataSource = Nothing
            gvCogsSummary.DataSource = dt
            gvCogsSummary.BestFitColumns()
            GroupBox2.Visible = False
            RadPageView1.SelectedPage = RadPageViewPage3
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub gvCogsSummary_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvCogsSummary.CellDoubleClick
        Try
            Dim strBatchNo As String = clsCommon.myCstr(gvCogsSummary.CurrentRow.Cells("Batch No").Value)
            Dim strItemCode As String = clsCommon.myCstr(gvCogsSummary.CurrentRow.Cells("Main Item").Value)
            If clsCommon.myLen(strBatchNo) > 0 Then
                CogsValueDetail(strItemCode, strBatchNo)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub CogsValueDetail(ByVal strItemCode As String, ByVal StrBatchNo As String)
        Try
            Dim strquery As String = " select [Batch No],[Main Item],tspl_item_master.Item_Desc as [Main Item Name],UOM,case when RowNumber=1 then [Batch Main Qty] else 0 end [Batch Main Qty],case when RowNumber=1 then Main_item_cost else 0 end [Main item cost],[BOM UOM], [BOM Item],Bom_Item.Item_Desc as [Bom Item Name],[Issue Qty],[Total Cost], [Consumed Qty],[Consumed Value],Location_Code,convert(decimal(18,3),[Difference Qty]) as [Difference Qty] from ( select row_number() over (partition by [Batch No] order by [Batch No]) as RowNumber, *,[Required Qty]-[Consumed Qty] as [Difference Qty] from (select distinct isnull(tspl_item_uom_detail.Item_Cost,0)*isnull(TSPL_PP_BATCH_ORDER_BOM_DETAIL.Quantity,0) as Main_item_cost ,TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code as [Batch No],TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code as [Main Item],TSPL_PP_PRODUCTION_PLAN_DETAIL.Unit_Code as UOM,TSPL_PP_BATCH_ORDER_BOM_DETAIL.Quantity  as [Batch Main Qty] ,TSPL_PP_BATCH_ORDER_BOM_DETAIL.BOM_Code as [BOM No],TSPL_PP_BOM_HEAD.PROD_QUANTITY as [BOM Qty],TSPL_PP_BATCH_ORDER_BOM_DETAIL.Unit_Code as [BOM UOM] ,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE as [BOM Item],TSPL_PP_BOM_ITEM_DETAIL.QUANTITY as [Standard Qty],TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE as[Item UOM] ,isnull([Required Qty],0) as [Required Qty],isnull(Issue_Item.Issue_Qty,0) as [Issue Qty],isnull(Issue_Item.[Total Cost],0) as [Total Cost] ,isnull(CONSM_QTY,0) as [Consumed Qty] ,isnull([Consumed Value],0) as [Consumed Value],TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Date,BatchRawItem.Location_Code  from TSPL_PP_PRODUCTION_PLAN_HEAD left outer join TSPL_PP_PRODUCTION_PLAN_DETAIL on TSPL_PP_PRODUCTION_PLAN_DETAIL.Plan_Code=TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code left outer join TSPL_PP_BATCH_ORDER_BOM_DETAIL on TSPL_PP_BATCH_ORDER_BOM_DETAIL.Plan_Code=TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TSPL_PP_BATCH_ORDER_BOM_DETAIL.BOM_Code left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE=TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE	  left outer join (select Item_Code as RequiredItem,Quantity as [Required Qty],TSPL_PP_BATCH_ORDER_HEAD.Batch_Code as [BatchCode],TSPL_PP_BATCH_ORDER_HEAD.Location_Code from TSPL_PP_BATCH_ORDER_HEAD left outer join TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL on TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Batch_Code=TSPL_PP_BATCH_ORDER_HEAD.Batch_Code)BatchRawItem on BatchRawItem.BatchCode=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code and BatchRawItem.RequiredItem=TSPL_PP_BOM_ITEM_DETAIL.Item_Code left outer join (select CONSM_QTY,Batch_Code,CONSM_ITEM_CODE,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.Fat_Amt+TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_Amt as [Consumed Value] from TSPL_PP_PRODUCTION_ENTRY   left outer join TSPL_PP_PRODUCTION_ENTRY_DETAIL on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE left outer join  TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL on TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE =TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE)TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY.Batch_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code and TSPL_PP_PRODUCTION_ENTRY.CONSM_ITEM_CODE=TSPL_PP_BOM_ITEM_DETAIL.Item_Code left join ( select TSPL_PP_ISSUE_HEAD.Issue_Code ,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.Qty,0) as Issue_Qty,TSPL_PP_ISSUE_HEAD.Batch_Code,TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code ,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.Fat_Amt,0) +isnull(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Amt,0)  as [Total Cost]  from TSPL_PP_BATCH_ORDER_HEAD left outer join TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL on TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Batch_Code=TSPL_PP_BATCH_ORDER_HEAD.Batch_Code left join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Batch_Code=TSPL_PP_BATCH_ORDER_HEAD.Batch_Code left join TSPL_PP_ISSUE_ITEM_DETAIL on TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code =TSPL_PP_ISSUE_HEAD.Issue_Code  ) as Issue_Item on Issue_Item.Batch_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code and TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE =Issue_Item.Item_Code  left join tspl_item_master on tspl_item_master.item_code=TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code  left join tspl_item_uom_detail on tspl_item_uom_detail.item_code=TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code and tspl_item_uom_detail.Uom_code=TSPL_PP_PRODUCTION_PLAN_DETAIL.Unit_Code  )as xxx where 2=2   and [Batch No]='" & StrBatchNo & "' and [Main Item]='" & strItemCode & "' ) as final left outer join tspl_item_master on TSPL_ITEM_MASTER .Item_Code =final.[Main Item] left outer join tspl_item_master as Bom_Item on Bom_Item .Item_Code =final.[BOM Item] order by [Batch No]" & Environment.NewLine
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strquery)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                GroupBox2.Visible = True
                gvCogsDetail.DataSource = Nothing
                gvCogsDetail.DataSource = dt
                gvCogsDetail.BestFitColumns()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Code", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim stateCond As String = ""
        If txtState.arrValueMember IsNot Nothing AndAlso txtState.arrValueMember.Count > 0 Then
            stateCond = " and state in  (" + clsCommon.GetMulcallString(txtState.arrValueMember) + ") "
        End If
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where location_type IN ('Physical','Virtual') " & stateCond & " "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        'FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Dim Str As String = String.Empty

        Dim qry As String = clsSaleRegisterDetail.GetAllSaleTransactionTypeQuery()

        If clsCommon.CompairString(clsUserMgtCode.RptFreshSaleRegister1, FORMTYPE) <> CompairStringResult.Equal Then
            txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Name", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
        End If

        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            If txtTransaction.arrValueMember Is Nothing OrElse txtTransaction.arrValueMember.Contains("Misc Sale") Then
                lblSubCategory.Visible = True
                ddlSubCategory.Visible = True
                ddlSubCategory.SelectedValue = "All"
            Else
                lblSubCategory.Visible = False
                ddlSubCategory.Visible = False
                ddlSubCategory.SelectedValue = "All"
            End If
        End If

    End Sub
    Sub LoadCategory()
        dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")

        gvCategory.DataSource = Nothing
        Dim qry As String = "select cast( 0 as bit) as SEL,ITEM_CATEGORY_CODE AS Code,DESCRIPTION as NAME from TSPL_ITEM_CATEGORY_LEVEL ORDER BY CATEGORY_LEVEL"
        gvCategory.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvCategory.Columns("SEL").ReadOnly = False
        gvCategory.Columns("SEL").Width = 30
        gvCategory.Columns("SEL").HeaderText = " "

        gvCategory.Columns("CODE").ReadOnly = True
        gvCategory.Columns("CODE").Width = 100
        gvCategory.Columns("CODE").HeaderText = "Code"

        gvCategory.Columns("NAME").ReadOnly = True
        gvCategory.Columns("NAME").Width = 200
        gvCategory.Columns("NAME").HeaderText = "Description"

        gvCategory.ShowGroupPanel = False
        gvCategory.AllowAddNewRow = False
        gvCategory.AllowColumnReorder = False
        gvCategory.AllowRowReorder = False
        gvCategory.EnableSorting = False
        gvCategory.ShowFilteringRow = True
        gvCategory.EnableFiltering = True
        gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCategory.MasterTemplate.ShowRowHeaderColumn = True

    End Sub

    Private Sub gvCategory_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvCategory.CellDoubleClick
        If clsCommon.myCBool(gvCategory.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 2
            frm.strCode = clsCommon.myCstr(gvCategory.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvCategory.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvCategory.CurrentRow.Tag = frm.arrOut
            End If
        End If
    End Sub


    Function GetMIS_ITem_GroupColumn() As String
        Dim qry As String = ""
        qry = " select MAP.Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING MAP " &
            " left join TSPL_CUSTOM_FIELD_HEAD CF on MAP.Custom_Field_Code=CF.Code " &
            " where CF.Name='MIS Item Group' and MAP.PROGRAM_CODE='" & clsUserMgtCode.itemStructure & "'"
        MIS_Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return MIS_Item_Group
    End Function

    Private Sub txtItemGroup__My_Click(sender As Object, e As EventArgs) Handles txtItemGroup._My_Click
        Dim qry As String = " select Value as [Code],Description as Name from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "' "
        txtItemGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemGroupMulSel", qry, "Code", "Name", txtItemGroup.arrValueMember, txtItemGroup.arrDispalyMember)
    End Sub

    Private Sub Gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles Gv1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            DrillDown()
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Total Sale") = CompairStringResult.Equal Then

            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Location Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Total Sale") Then
                arrBack.Remove("Total Sale")
                ddlReportType.SelectedValue = "Total Sale"
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Item Group Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Location Wise") Then
                arrBack.Remove("Location Wise")
                ddlReportType.SelectedValue = "Location Wise"
                txtLocation.arrValueMember = arrLocation
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Customer Group Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Item Group Wise") Then
                arrBack.Remove("Item Group Wise")
                ddlReportType.SelectedValue = "Item Group Wise"
                txtItemGroup.arrValueMember = arrItemGroup
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Item Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Customer Group Wise") Then
                arrBack.Remove("Customer Group Wise")
                ddlReportType.SelectedValue = "Customer Group Wise"
                txtCustGroup.arrValueMember = arrCustGroup
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Customer Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Item Wise") Then
                arrBack.Remove("Item Wise")
                ddlReportType.SelectedValue = "Item Wise"
                txtItem.arrValueMember = arrItem
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Customer Wise") Then
                arrBack.Remove("Customer Wise")
                ddlReportType.SelectedValue = "Customer Wise"
                txtCustomer.arrValueMember = arrCustomer
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Detail") = CompairStringResult.Equal AndAlso arrBack.Contains("Document Wise") Then
                arrBack.Remove("Document Wise")
                ddlReportType.SelectedValue = "Document Wise"
                Document_No = Document_No_Old
                Print(Exporter.Refresh)
            End If
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCustGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustGroup._My_Click
        Dim qry As String = " select Cust_Group_Code as Code,Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER"
        txtCustGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("CustGroupMulSel", qry, "Code", "Name", txtCustGroup.arrValueMember, txtCustGroup.arrDispalyMember)
    End Sub


    Private Sub txtState__My_Click(sender As Object, e As EventArgs) Handles txtState._My_Click
        Dim qry As String = " select STATE_CODE as Code,STATE_NAME as Name from TSPL_STATE_MASTER"
        txtState.arrValueMember = clsCommon.ShowMultipleSelectForm("StateMulSel", qry, "Code", "Name", txtState.arrValueMember, txtState.arrDispalyMember)
    End Sub
    Sub BulkExport(ByVal FormatType As String)
        Try
            clsCommon.ProgressBarPercentShow()
            clsCommon.ProgressBarPercentUpdate(0, "Generating query for the report..")
            Dim obj As clsSaleRegisterParameterType = ReturnFilterData()
            Dim qry As String = clsSaleRegisterDetail.GetReportDataQuery(obj)

            clsCommon.ProgressBarPercentUpdate(10, "Query generated..starting bulk export..")
            If ddlReportType.SelectedValue = "Total Sale" Then
                qry = "select * from (" & qry & ") PP order by [Total FAT KG]"
                transportSql.BulkExport("Sale_Register", qry, "order by [Total FAT KG]", FormatType)
                Exit Sub
            ElseIf ddlReportType.SelectedValue = "Location Wise" Then
                qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name]"
                transportSql.BulkExport("Sale_Register", qry, "order by [Location Code],[Location Name]", FormatType)
                Exit Sub
            ElseIf ddlReportType.SelectedValue = "Item Group Wise" Then
                qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name],[Item Group Code],[Item Group Description]"
                transportSql.BulkExport("Sale_Register", qry, "order by [Location Code],[Location Name],[Item Group Code],[Item Group Description]", FormatType)
                Exit Sub
            ElseIf ddlReportType.SelectedValue = "Customer Group Wise" Then
                qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description]"
                transportSql.BulkExport("Sale_Register", qry, "order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description]", FormatType)
                Exit Sub
            ElseIf ddlReportType.SelectedValue = "Item Wise" Then
                qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name]"
                transportSql.BulkExport("Sale_Register", qry, " order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name]", FormatType)
                Exit Sub
            ElseIf ddlReportType.SelectedValue = "Customer Wise" Then
                qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name],[Customer Code],[Customer Name]"
                transportSql.BulkExport("Sale_Register", qry, "order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name],[Customer Code],[Customer Name]", FormatType)
                Exit Sub
            ElseIf ddlReportType.SelectedValue = "Document Wise" Then

                transportSql.BulkExport("Sale_Register", qry, "order by convert(date,[Document_Date],103),[Document No]", FormatType)
                Exit Sub
            ElseIf ddlReportType.SelectedValue = "Document Detail" Then
                transportSql.BulkExport("Sale_Register", qry, "order by convert(date,[Document_Date],103),[Document No]", FormatType)
                Exit Sub
            ElseIf obj.ReportType = "Document Info Level" Then
                transportSql.BulkExport("Sale_Register", qry, "order by convert(date,[Document_Date],103),[Document No]", FormatType)
                Exit Sub
            End If


            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Data exported successfully")
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try
    End Sub



    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptSaleRegisterDetail & "'"))
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
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
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            If Gv1.Groups.Count > 0 Then
                clsCommon.MyExportToExcelGrid(Me.Text, Gv1, arrHeader, Me.Text, True)
            Else
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            End If
            'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chk_stockingunit_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chk_stockingunit.ToggleStateChanged
        If chk_stockingunit.Checked Then
            txtUOM.Enabled = False
            txtUOM.Value = ""
        Else
            txtUOM.Enabled = True
        End If
    End Sub

    Private Sub ddlReportType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddlReportType.SelectedIndexChanged
        If IsFormLoad = False Then
            Exit Sub
        End If
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            If ddlReportType.SelectedValue = "Document Detail" AndAlso (txtTransaction.arrValueMember Is Nothing OrElse txtTransaction.arrValueMember.Contains("Misc Sale")) Then
                lblSubCategory.Visible = True
                ddlSubCategory.Visible = True
                ddlSubCategory.SelectedValue = "All"
            Else
                lblSubCategory.Visible = False
                ddlSubCategory.Visible = False
                ddlSubCategory.SelectedValue = "All"
            End If
        End If
    End Sub
    'added by richa  BHA/17/08/18-000441 
    Private Sub TxtRoute__My_Click(sender As Object, e As EventArgs) Handles TxtRoute._My_Click
        Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No AS Code,TSPL_ROUTE_MASTER.Route_Desc as Name from TSPL_ROUTE_MASTER  where 1=1 "
        TxtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMulSel", qry, "Code", "Name", TxtRoute.arrValueMember, TxtRoute.arrDispalyMember)
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptsaleRegisterReport & "'"))
            If clsCommon.myLen(txtUOM.Value) > 0 Then
                arrHeader.Add("UOM : " + txtUOM.Value)
            End If
            arrHeader.Add("Report Type : " + ddlReportType.Text)
            If txtTransaction.arrDispalyMember IsNot Nothing AndAlso txtTransaction.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember))
            End If
            If txtState.arrDispalyMember IsNot Nothing AndAlso txtState.arrDispalyMember.Count > 0 Then
                arrHeader.Add("State : " + clsCommon.GetMulcallStringWithComma(txtState.arrDispalyMember))
            End If
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If txtItemGroup.arrDispalyMember IsNot Nothing AndAlso txtItemGroup.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item Group : " + clsCommon.GetMulcallStringWithComma(txtItemGroup.arrDispalyMember))
            End If
            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            If txtCustGroup.arrDispalyMember IsNot Nothing AndAlso txtCustGroup.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustGroup.arrDispalyMember))
            End If
            If txtCustomer.arrDispalyMember IsNot Nothing AndAlso txtCustomer.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

            If TxtRoute.arrDispalyMember IsNot Nothing AndAlso TxtRoute.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Route : " + clsCommon.GetMulcallStringWithComma(TxtRoute.arrDispalyMember))
            End If
            If txtmultSchemeType.arrDispalyMember IsNot Nothing AndAlso txtmultSchemeType.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Scheme Type : " + clsCommon.GetMulcallStringWithComma(txtmultSchemeType.arrDispalyMember))
            End If

            If TxtMultiCustomerCategory.arrDispalyMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer Category : " + clsCommon.GetMulcallStringWithComma(TxtMultiCustomerCategory.arrDispalyMember))
            End If
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)

            clsCommon.MyExportToPDF("Sale Register Detail", Gv1, arrHeader, "Sale Register Detail", PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub BulkExcel_Click(sender As Object, e As EventArgs) Handles BulkExcel.Click
        BulkExport("xls")
    End Sub

    Private Sub BulkCSV_Click(sender As Object, e As EventArgs) Handles BulkCSV.Click
        BulkExport("csv")
    End Sub


    Private Sub txtmultSchemeType__My_Click(sender As Object, e As EventArgs) Handles txtmultSchemeType._My_Click
        Dim qry As String = "select distinct Scheme_Type as Code,Scheme_Type as Name from TSPL_SD_SALE_INVOICE_DETAIL where Scheme_Type<>'' "
        txtmultSchemeType.arrValueMember = clsCommon.ShowMultipleSelectForm("SchemeTypeMult", qry, "Code", "Name", txtmultSchemeType.arrValueMember, txtmultSchemeType.arrDispalyMember)
    End Sub

    Private Sub TxtMultiCustomerCategory__My_Click(sender As Object, e As EventArgs) Handles TxtMultiCustomerCategory._My_Click
        Dim qry As String = " select cust_category_code as [Code], CUST_CATEGORY_DESC as [Desc] from TSPL_CUSTOMER_CATEGORY_MASTER "
        TxtMultiCustomerCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("CustCaMulSel6", qry, "Code", "Desc", TxtMultiCustomerCategory.arrValueMember, TxtMultiCustomerCategory.arrDispalyMember)
    End Sub
End Class
