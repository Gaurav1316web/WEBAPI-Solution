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

Public Class RptProductDOStatus
    Inherits FrmMainTranScreen
    Dim isInsideLoadData As Boolean = False
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptProductDOStatus)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

        btnExport.Visible = MyBase.isExport
    End Sub
#End Region

    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Sub LoadLocation()
        Dim qry As String = "select Location_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER  "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location"
        cbgLocation.DisplayMember = "Location Description"
    End Sub
    Sub Print(ByVal IsPrint As Exporter)
        Try
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()


            If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one location")
            End If
            If rbtnCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one customer")
            End If
            'Dim Qry As String = "Select case when Delivery .Delivery_Status =2 then 'Pending'  when Delivery.Delivery_Status = 4 then 'Complete' when Delivery.Balance_Qty <> 0 then 'Partial' end as Status1,*From (select TSPL_CUSTOMER_MASTER.Customer_Name ,    TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.cust_Group_Code,Cust_Group_Desc ,   TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Bill_To_Location as Location_Code ,   TSPL_LOCATION_MASTER.Location_Desc ,    TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code As Delivery_OrderNo,convert(varchar,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Date,103) as Delivery_Date  , "
            'Qry += " TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code As DO_Item_Code,    TSPL_ITEM_MASTER_DO.Item_Desc As DO_Item_Desc,    IsNull(TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Qty, 0) As Delivery_qty,    TSPL_SD_SHIPMENT_HEAD.Ref_No   ,    TSPL_SD_SHIPMENT_HEAD.Document_Code As Dispatch_No,    TSPL_SD_SHIPMENT_DETAIL.Item_Code As SO_Item_Code,    TSPL_ITEM_MASTER_SO.Item_Desc As SO_Item_Desc,    Convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) As    Dispatch_Date,    IsNull(TSPL_SD_SHIPMENT_DETAIL.Qty,    0) As Dispatch_qty,    IsNull(TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Qty, 0) -    IsNull(TSPL_SD_SHIPMENT_DETAIL.Qty, 0) As  Balance_Qty,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Status as Delivery_Status,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Discount_Amt ,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Amount_Less_Discount ,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Total_Tax_Amt ,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Total_Amt,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close from  TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE left join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code =TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Document_Code left join "
            'Qry += " TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Delivery_Code_PS =TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE =TSPL_SD_SHIPMENT_HEAD.Document_Code and TSPL_SD_SHIPMENT_DETAIL.Item_Code =TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code  And      TSPL_SD_SHIPMENT_DETAIL.Scheme_Item = 'N' Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code =      TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Customer_Code left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code =      TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Bill_To_Location   Left Outer Join TSPL_ITEM_MASTER As TSPL_ITEM_MASTER_DO      On TSPL_ITEM_MASTER_DO.Item_Code = TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code"
            'Qry += " Left Outer Join TSPL_ITEM_MASTER As TSPL_ITEM_MASTER_SO      On TSPL_ITEM_MASTER_SO.Item_Code =      TSPL_SD_SHIPMENT_DETAIL.Item_Code ) Delivery Where 2=2  "

            'Qry += " and convert(date,Delivery.Delivery_Date,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,Delivery.Delivery_Date,103) <=convert(date,'" + ToDate.Value + "' ,103) and Delivery.Short_Close='N' "
            'If RbPending.IsChecked = True Then
            '    Qry += " and Delivery .Delivery_Status =2"
            'ElseIf chkPartial.IsChecked = True Then
            '    Qry += " and Delivery.Balance_Qty <> 0"
            'ElseIf chkComplete.IsChecked = True Then
            '    Qry += " and Delivery.Delivery_Status = 4"
            'End If
            'If rbtnLocationSelect.IsChecked Then
            '    Qry += " and  Delivery.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            'End If
            'If rbtnCustomerSelect.IsChecked Then
            '    Qry += " and Delivery.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            'End If
            'Qry += " order by convert(date,Delivery_Date,103)"

            
            Dim Qry As String = "SELECT "
            Qry += " (case when Status = 1 then 'Open' when Status = 2 then 'Pending' when Status = 3 then 'Approved'when Status = 4 then 'Posted' end)  as [Posting Status], "
            Qry += " (CASE WHEN CONVERT(decimal(18, 2), COALESCE([Delivery Qty], 0)) > 0 and CONVERT(decimal(18, 2), COALESCE([Dispatch Qty], 0))=0 and close_yn='N' THEN 'Pending' "
            Qry += " WHEN ( CONVERT(decimal(18, 2), COALESCE([Delivery Qty], 0)) > 0 and CONVERT(decimal(18, 2), COALESCE([Dispatch Qty],0))>0 and CONVERT(decimal(18, 2), COALESCE([Delivery Qty], 0))- CONVERT(decimal(18, 2), COALESCE([Dispatch Qty],0))<=0) or close_yn='Y' THEN 'Completed'  "
            Qry += " WHEN CONVERT(decimal(18, 2), COALESCE([Delivery Qty], 0)) > 0 and CONVERT(decimal(18, 2), COALESCE([Dispatch Qty],0))>0 and CONVERT(decimal(18, 2), COALESCE([Delivery Qty], 0))- CONVERT(decimal(18, 2), COALESCE([Dispatch Qty],0))>0 and close_yn='N' "
            Qry += " THEN 'Partial'"
            Qry += " WHEN CONVERT(decimal(18, 2), COALESCE([Delivery Qty], 0)) >= 0 and close_yn='Y' THEN 'Closed' END  ) AS [Document Status]"
            Qry += ",* from ("
            Qry += " SELECT max(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Status) as Status"


            Qry += " , MAX(COALESCE(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.customer_code, '')) AS [Customer Code] , MAX(COALESCE(TSPL_CUSTOMER_MASTER.Customer_NAME, '')) AS [Customer Name]  ,MAX(COALESCE(TSPL_CUSTOMER_MASTER.cust_Group_Code, '')) AS [Customer Group Code] ,MAX(COALESCE(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc, ''))  AS [Customer Group Name] , MAX(COALESCE(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Bill_To_Location, '')) AS [Location Code]  , MAX(COALESCE(TSPL_lOCATION_MASTER.LOCATION_DESC, '')) AS [Location Name]  , COALESCE(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code, '') AS [Delivery No]   , MAX(CONVERT(varchar, TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Delivery_date, 103)) AS [Delivery Date] "
            Qry += " ,stuff((select  ','+TT.DOCUMENT_CODE from TSPL_SD_SHIPMENT_DETAIL TT where TT.Delivery_Code_PS = TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code for xml path('')),"
            Qry += "  1,1,'') AS [Dispatch No]  , COALESCE(TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code, '') AS [Item Code]  "
            Qry += " , MAX(COALESCE(tspl_item_master.Item_Desc, '')) AS [Item Name] "
            Qry += " , CONVERT(decimal(18, 2), MAX(COALESCE(TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Qty, 0))) AS [Delivery Qty]  "
            Qry += " , CONVERT(decimal(18, 2), sum(COALESCE(SHIPMENT.Qty, 0))) AS [Dispatch Qty]  "
            Qry += " , CONVERT(decimal(18, 2), MAX(COALESCE(TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Qty, 0)))- CONVERT(decimal(18, 2), sum(COALESCE(SHIPMENT.Qty,0))) AS [Balance Qty] "
            Qry += " ,MAX(COALESCE(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close,'N')) close_yn from TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE left join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE  on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE .Document_Code "
            Qry += " left join (select TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE as DOCUMENT_CODE,TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS,TSPL_SD_SHIPMENT_DETAIL.item_code,sum(CONVERT(decimal(18, 2), COALESCE(TSPL_SD_SHIPMENT_DETAIL.Qty, 0))) as Qty"
            Qry += " from TSPL_SD_SHIPMENT_DETAIL "
            Qry += " group by TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE,TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS,TSPL_SD_SHIPMENT_DETAIL.Item_Code"
            Qry += " )SHIPMENT on SHIPMENT.Delivery_Code_PS  =TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code"
            Qry += " and SHIPMENT.Item_Code =TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code"
            Qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Customer_Code  LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Bill_To_Location left join tspl_item_master on tspl_item_master.Item_Code = TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code"
            Qry += " where 1=1 "
            Qry += " and convert(date,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Date,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Date,103) <=convert(date,'" + ToDate.Value + "' ,103)  "

            If rbtnLocationSelect.IsChecked Then
                Qry += " and  TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If rbtnCustomerSelect.IsChecked Then
                Qry += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            End If
            Qry += " group by TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Unit_code"
            Qry += " ,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close "


            If RbPending.IsChecked = True Then
                'Qry += " and len(isnull(TSPL_SD_SHIPMENT_HEAD.Document_Code,''))<=0 and COALESCE(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close,'N')='N'  "
                Qry += " having CONVERT(decimal(18, 2), max(COALESCE(TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Qty, 0))) > 0 and CONVERT(decimal(18, 2), sum(COALESCE(SHIPMENT.Qty, 0)))=0 and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close='N'"
            ElseIf chkPartial.IsChecked = True Then
                'Qry += " and Delivery.Balance_Qty <> 0"
                'Qry += " and len(isnull(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code,''))>0 and  CONVERT(decimal(18, 2), max(COALESCE(TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Qty, 0)))- CONVERT(decimal(18, 2), sum(COALESCE(TSPL_SD_SHIPMENT_DETAIL.Qty,0)))<>0 and COALESCE(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close,'N')='N'  "
                Qry += " HAVING CONVERT(decimal(18, 2), max(COALESCE(TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Qty, 0))) > 0 and CONVERT(decimal(18, 2), sum(COALESCE(SHIPMENT.Qty,0)))>0 and CONVERT(decimal(18, 2), max(COALESCE(TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Qty, 0)))- CONVERT(decimal(18, 2), sum(COALESCE(SHIPMENT.Qty,0)))>0 and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close='N'"
            ElseIf chkComplete.IsChecked = True Then
                'Qry += " and (len(isnull(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code,''))>0 and CONVERT(decimal(18, 2), max(COALESCE(TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Qty, 0)))- CONVERT(decimal(18, 2), sum(COALESCE(TSPL_SD_SHIPMENT_DETAIL.Qty,0)))=0   or " & _
                '      " ( CONVERT(decimal(18, 2), COALESCE(TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Qty, 0)) > 0 AND CONVERT(decimal(18, 2),COALESCE(TSPL_SD_SHIPMENT_DETAIL.Qty, 0)) > 0 and COALESCE(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close,'N')='Y')) "
                Qry += " HAVING (( CONVERT(decimal(18, 2), max(COALESCE(TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Qty, 0))) > 0 and CONVERT(decimal(18, 2), sum(COALESCE(SHIPMENT.Qty,0)))>0 and CONVERT(decimal(18, 2), max(COALESCE(TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Qty, 0)))- CONVERT(decimal(18, 2), sum(COALESCE(SHIPMENT.Qty,0)))<=0) or TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close='Y')"
                'Qry += " and Delivery.Delivery_Status = 4"
            End If

            'Qry += " order by [Delivery Date] "


            Qry += ")t order by [Delivery No] "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True



            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt
                SetGridFormationOFGV1()
                ReStoreGridLayout()
            End If

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
                clsCommon.MyExportToExcelGrid("Delivery Status", Gv1, arrHeader, Me.Text)
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Delivery Status", Gv1, arrHeader, "Delivery Status", True)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try


    End Sub
    Sub SetGridFormationOFGV1()


        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            'Gv1.Columns(ii).IsVisible = False
            Gv1.Columns(ii).Width = 100
            Gv1.Columns(ii).WrapText = True
        Next
        Gv1.Columns("close_yn").IsVisible = False
        Gv1.Columns("Status").IsVisible = False

        'Gv1.Columns("Status1").IsVisible = True
        'Gv1.Columns("Status1").Width = 100
        'Gv1.Columns("Status1").HeaderText = "Status"

        'Gv1.Columns("Cust_Code").IsVisible = True
        'Gv1.Columns("Cust_Code").Width = 100
        'Gv1.Columns("Cust_Code").HeaderText = "Customer Code"


        'Gv1.Columns("Customer_Name").IsVisible = True
        'Gv1.Columns("Customer_Name").Width = 70
        'Gv1.Columns("Customer_Name").HeaderText = "Customer Name"

        'Gv1.Columns("Cust_Group_Code").IsVisible = True
        'Gv1.Columns("Cust_Group_Code").Width = 100
        'Gv1.Columns("Cust_Group_Code").HeaderText = "Customer Group Code"

        'Gv1.Columns("Cust_Group_Desc").IsVisible = True
        'Gv1.Columns("Cust_Group_Desc").Width = 100
        'Gv1.Columns("Cust_Group_Desc").HeaderText = "Customer Group Name"

        'Gv1.Columns("Location_Code").IsVisible = True
        'Gv1.Columns("Location_Code").Width = 100
        'Gv1.Columns("Location_Code").HeaderText = "Location Code"

        'Gv1.Columns("Location_Desc").IsVisible = True
        'Gv1.Columns("Location_Desc").Width = 100
        'Gv1.Columns("Location_Desc").HeaderText = "Location Name"



        'Gv1.Columns("Delivery_OrderNo").IsVisible = True
        'Gv1.Columns("Delivery_OrderNo").Width = 100
        'Gv1.Columns("Delivery_OrderNo").HeaderText = "Delivery No"

        'Gv1.Columns("Delivery_Date").IsVisible = True
        'Gv1.Columns("Delivery_Date").Width = 100
        'Gv1.Columns("Delivery_Date").HeaderText = "Delivery Date"

        'Gv1.Columns("DO_Item_Code").IsVisible = True
        'Gv1.Columns("DO_Item_Code").Width = 100
        'Gv1.Columns("DO_Item_Code").HeaderText = " Item Code"

        'Gv1.Columns("DO_Item_Desc").IsVisible = True
        'Gv1.Columns("DO_Item_Desc").Width = 150
        'Gv1.Columns("DO_Item_Desc").HeaderText = " Item Name"

        'Gv1.Columns("Delivery_qty").IsVisible = True
        'Gv1.Columns("Delivery_qty").Width = 150
        'Gv1.Columns("Delivery_qty").HeaderText = "Delivery Qty"

        'Gv1.Columns("Discount_Amt").IsVisible = True
        'Gv1.Columns("Discount_Amt").Width = 100
        'Gv1.Columns("Discount_Amt").HeaderText = "Discount Amt"

        'Gv1.Columns("Amount_Less_Discount").IsVisible = True
        'Gv1.Columns("Amount_Less_Discount").Width = 100
        'Gv1.Columns("Amount_Less_Discount").HeaderText = "Amt. After Discount"

        'Gv1.Columns("Total_Tax_Amt").IsVisible = True
        'Gv1.Columns("Total_Tax_Amt").Width = 100
        'Gv1.Columns("Total_Tax_Amt").HeaderText = "Tax Amt"

        'Gv1.Columns("Total_Amt").IsVisible = True
        'Gv1.Columns("Total_Amt").Width = 100
        'Gv1.Columns("Total_Amt").HeaderText = "Total Amt"



        'Gv1.Columns("Dispatch_No").IsVisible = True
        'Gv1.Columns("Dispatch_No").Width = 100
        'Gv1.Columns("Dispatch_No").HeaderText = "Dispatch No."

        'Gv1.Columns("Dispatch_Date").IsVisible = True
        'Gv1.Columns("Dispatch_Date").Width = 100
        'Gv1.Columns("Dispatch_Date").HeaderText = "Dispatch Date"

        'Gv1.Columns("Dispatch_qty").IsVisible = True
        'Gv1.Columns("Dispatch_qty").Width = 120
        'Gv1.Columns("Dispatch_qty").HeaderText = "Dispatch Qty"

        'Gv1.Columns("Balance_Qty").IsVisible = True
        'Gv1.Columns("Balance_Qty").Width = 100
        'Gv1.Columns("Balance_Qty").HeaderText = "Balance Qty"

       
        Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim intCount As Integer = 0

        'Dim item1 As New GridViewSummaryItem("Total_Amt", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)

        'Dim item2 As New GridViewSummaryItem("Total_Tax_Amt", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)

        'Dim item3 As New GridViewSummaryItem("Amount_Less_Discount", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)

        'Dim item4 As New GridViewSummaryItem("Discount_Amt", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item4)
        'Dim item5 As New GridViewSummaryItem("Balance_Qty", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item5)
        'Dim item6 As New GridViewSummaryItem("Dispatch_qty", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item6)
        'Dim item7 As New GridViewSummaryItem("Delivery_qty", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item7)

        Dim item1 As New GridViewSummaryItem("Delivery Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Dispatch Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Balance Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

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
        rbtnCustomerAll.IsChecked = True
        rbtnLocationAll.IsChecked = True

        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum

    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        'If (Gv1.Rows.Count <= 0) Then
        '    common.clsCommon.MyMessageBoxShow("No Data To Export")
        '    Exit Sub
        'End If
        'Print(Exporter.Excel)
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmPDF.Click
        'If (Gv1.Rows.Count <= 0) Then
        '    common.clsCommon.MyMessageBoxShow("No Data To Export")
        '    Exit Sub
        'End If
        'Print(Exporter.PDF)
        ExportGrid(EnumExportTo.PDF)
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = Gv1
        Print(Exporter.Refresh)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rbtnCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCustomerAll.ToggleStateChanged
        cbgCustomer.Enabled = rbtnCustomerSelect.IsChecked
    End Sub

    Private Sub rbtnLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged
        cbgLocation.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub RptProductDOStatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
    End Sub

    Private Sub RptProductDOStatus_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If (Gv1.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow("No Data To Export")
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptProductDOStatus & "'"))
            If rbtnCustomerSelect.IsChecked Then
                Dim strLocationName As String = ""
                For Each StrName As String In cbgCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strLocationName) > 0 Then
                        strLocationName += ", "
                    End If
                    strLocationName += StrName
                Next
                Dim strLocationCode As String = ""
                For Each StrCode As String In cbgCustomer.CheckedValue
                    If clsCommon.myLen(strLocationCode) > 0 Then
                        strLocationCode += ", "
                    End If
                    strLocationCode += StrCode
                Next
                arrHeader.Add(("Customer: " + strLocationName + " "))
            End If
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
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Delivery Status", Gv1, arrHeader, "Delivery Status", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

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
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
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
