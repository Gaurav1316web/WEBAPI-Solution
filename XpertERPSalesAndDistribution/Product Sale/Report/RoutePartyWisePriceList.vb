Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports Telerik.WinControls.UI.Export

Public Class RoutePartyWisePriceList
    Inherits FrmMainTranScreen


    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub RoutePartyWisePriceList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funreset()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            Dim dtFreshItem As DataTable = New DataTable()
            Dim dtProductItem As DataTable = New DataTable()

            Dim qry As String = ""
            Dim whrcls As String = ""
            Dim Itemqry As String = ""
            Dim itemNames1 As String = Nothing
            Dim itemNamesProduct As String = Nothing
            If rdbMilk.IsChecked = False AndAlso rdbProduct.IsChecked = False Then
                clsCommon.MyMessageBoxShow(Me, "You must select at least one Checkbox of Milk Or Product", Me.Text)
                Exit Sub
            End If
            If rdbBooth.IsChecked = False AndAlso rdbDistributor.IsChecked = False Then
                clsCommon.MyMessageBoxShow(Me, "You must select at least one Checkbox of Booth Or Distributor", Me.Text)
                Exit Sub
            End If

            If rdbMilk.IsChecked = True Then
                Itemqry = " Select TSPL_ITEM_MASTER.Item_Code from TSPL_ITEM_MASTER
                            left outer join TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code
                            WHERE Is_FreshItem = 1 and Report_UOM=1 "
            ElseIf rdbProduct.IsChecked = True Then
                Itemqry = "Select TSPL_ITEM_MASTER.Item_Code from TSPL_ITEM_MASTER 
                            left outer join TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code
                            WHERE Is_Ambient = 1 AND Item_Type='F' AND Report_UOM=1"
            End If
            Dim dtitemName As DataTable = clsDBFuncationality.GetDataTable(Itemqry)
            If dtitemName.Rows.Count > 0 Then
                For i As Integer = 0 To dtitemName.Rows.Count - 1
                    If i = 0 Then
                        itemNamesProduct += " ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code")) + "],0) as [" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code")) + "]"
                        itemNames1 += "[" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code")) + "] "
                    Else
                        itemNamesProduct += ", " + "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code")) + "],0) as [" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code")) + "]"
                        itemNames1 += ", [" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code")) + "] "
                    End If
                Next
            End If

            qry = " Select Item_Price_ID,Code,Route_No,Name,[Price Code]," & itemNamesProduct & " 
from (  select  TSPL_ITEM_PRICE_MASTER.against_Plan_Tr_Code, TSPL_ITEM_PRICE_MASTER.Item_Price_ID,  tspl_customer_master.Cust_Code as [Code],
(tspl_customer_master.Customer_Name) as [Name],TSPL_CUSTOMER_MASTER.ROUTE_NO,(case when tspl_customer_master.IsDistributor='Y' then 'Distributor' else '' end) as [Type],
TSPL_ITEM_PRICE_MASTER.price_code as [Price Code],(select top 1 (TSPL_PRICE_COMPONENT_MAPPING.price_code_desc) as price_code_desc from TSPL_PRICE_COMPONENT_MAPPING where TSPL_PRICE_COMPONENT_MAPPING.price_code=TSPL_ITEM_PRICE_MASTER.price_code) as [Price Name],
TSPL_ITEM_PRICE_MASTER.location_code as [LocationCode],(tspl_location_master.location_desc) as [LocationName]  ,(TSPL_ITEM_MASTER.CSA_TYPE) as [GroupName],TSPL_ITEM_PRICE_MASTER.Item_Code as [Product Code],
(TSPL_ITEM_MASTER.Item_Desc) as [Product Name] ,TSPL_ITEM_UOM_Detail.UOM_Code as [Product Unit] ,(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price) as [Rate],TSPL_ITEM_UOM_DETAIL.Item_Cost , convert(date,TSPL_ITEM_PRICE_MASTER.[Start_Date],103) as [RateWef],
( round(case when isnull(Scheme.CashDisc_Amount,0)<>0 then isnull(Scheme.CashDisc_Amount,0) else ((ISNULL(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price,0)*isnull(Scheme.CasdDisc_Percentage,0))/100) end,2) ) as [Discount],
( case when isnull(Scheme.CasdDisc_Percentage,0)<>0 then 'By Percentage' when isnull(Scheme.CashDisc_Amount,0)<>0 then 'By Value' else 'NA' end) as [Discount Type], 
Scheme.effct_date as DissWef ,( round(case when isnull(Scheme.CashDisc_Amount,0)>0 then ISNULL(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price,0)-   isnull(Scheme.CashDisc_Amount,0) else ISNULL(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price,0)- ((ISNULL(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price,0)*isnull(Scheme.CasdDisc_Percentage,0))/100) end,2)) as [NetRate], 
tspl_User_Master_Modified_Name.User_Name as [Modified By], convert (varchar,TSPL_ITEM_PRICE_MASTER.Modify_Date ,103) as [Modified On] 
from TSPL_ITEM_PRICE_MASTER 
inner join TSPL_CUSTOMER_MASTER on TSPL_ITEM_PRICE_MASTER.Price_Code=tspl_customer_master.Price_CodeNon 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code  
left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code   and Report_UOM=1  and TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code 
left outer join  (select case when len(isnull(TSPL_SCHEME_MASTER_NEW.MaxlimitStart_Date,''))<=0 then TSPL_SCHEME_MASTER_NEW.Start_Date else TSPL_SCHEME_MASTER_NEW.MaxlimitStart_Date end as effct_date,TSPL_SCHEME_BENEFICIARY.Cust_Code,TSPL_SCHEME_DETAIL_NEW.MainItem_Code as Item_Code,TSPL_SCHEME_DETAIL_NEW.MainUnit_Code as Unit_Code,TSPL_SCHEME_DETAIL_NEW.CashDisc_Amount,TSPL_SCHEME_DETAIL_NEW.CasdDisc_Percentage from TSPL_SCHEME_MASTER_NEW  left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_BENEFICIARY.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code 
left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_MASTER_NEW.Scheme_Code=TSPL_SCHEME_DETAIL_NEW.Scheme_Code where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Discount' )Scheme on Scheme.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code and TSPL_ITEM_MASTER.Item_Code=Scheme.Item_Code and TSPL_ITEM_PRICE_MASTER.UOM= case when len(isnull(Scheme.Unit_Code,''))<=0 then tspl_item_master.unit_code else Scheme.Unit_Code end  
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.location_code=TSPL_ITEM_PRICE_MASTER.Location_Code 
left outer join tspl_customer_location_mapping on tspl_customer_location_mapping.customer_code=tspl_customer_master.Cust_Code and tspl_customer_location_mapping.Location_Code=TSPL_ITEM_PRICE_MASTER.Location_code
left outer Join tspl_User_Master as tspl_User_Master_Modified_Name  on tspl_User_Master_Modified_Name.User_Code = TSPL_ITEM_PRICE_MASTER.Modify_By 
left outer join TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No=TSPL_CUSTOMER_MASTER.Route_No and tspl_customer_master.IsDistributor='N'
where 1=1 
and  TSPL_ITEM_PRICE_MASTER.Is_Active=1 and TSPL_item_master.Active=1  and  convert(date,TSPL_ITEM_PRICE_MASTER.Start_Date ,103)>=convert(date,'" & fromDate.Value & "',103) 
and convert(date,TSPL_ITEM_PRICE_MASTER.Start_Date ,103)<=convert(date,'" & ToDate.Value & "',103) and  REPORT_UOM=1
"
            If txtCustomer.arrValueMember IsNot Nothing Then
                qry += " AND TSPL_CUSTOMER_MASTER.Cust_Code In (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
            End If
            If txtRoute.arrValueMember IsNot Nothing Then
                qry += " AND TSPL_ROUTE_MASTER.Route_No In (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            End If
            qry += " ) Tab1 Pivot (SUM(Rate) FOR [product Code] IN (" & itemNames1 & ")) AS Tab2 
  "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.EnableFiltering = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.BestFitColumns()
                SetGridFormation()
                RadPageView2.SelectedPage = RadPageViewPage5
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).VisibleInColumnChooser = False
        Next

        gv1.Columns("Item_Price_ID").IsVisible = False
        gv1.Columns("Item_Price_ID").VisibleInColumnChooser = True
        gv1.Columns("Code").IsVisible = False
        gv1.Columns("Code").VisibleInColumnChooser = True
        gv1.Columns("Price Code").IsVisible = False
        gv1.Columns("Price Code").VisibleInColumnChooser = True
        gv1.Columns("Name").HeaderText = "Dealer Name"
        gv1.Columns("Route_No").HeaderText = "Route Name"
        'gv1.Columns("Cust_Code").IsVisible = False
        'gv1.Columns("Cust_Code").IsVisible = False
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        EnableDisableControls(True)
        gv1.DataSource = Nothing
        txtRoute.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        RadPageView2.SelectedPage = RadPageViewPage4
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox10.Enabled = val
        RadGroupBox8.Enabled = val
    End Sub


End Class