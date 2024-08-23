
Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Imports XpertERPEngine

Public Class rptCattleFeedSaleReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim StrPermission As String

    Private Sub rptCattleFeedSaleReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        Reset()

    End Sub
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = ""
        Dim WhrCls As String = ""
        qry = "select Location_Code AS Code,Location_Desc as Name  from TSPL_LOCATION_MASTER"
        WhrCls = " Is_Sub_Location = 'N' AND Location_Category <> 'MCC' and GIT_Type  <> 'Y' "

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ") "
        End If

        txtLocation.Value = clsCommon.ShowSelectForm("CattleFeedSale", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
            txtSubLocation.Enabled = True
        Else
            txtSubLocation.Enabled = False
        End If

    End Sub

    Private Sub txtSubLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSubLocation._MYValidating
        If clsCommon.myLen(txtLocation.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Location code before sub location", Me.Text)
            Exit Sub
        End If

        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
            txtSubLocation.Value = clsLocation.getFinder(" (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Main_Location_Code='" + txtLocation.Value + "'", txtSubLocation.Value, isButtonClicked)
        End If

    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click

        Dim qry As String = ""
        qry = "select TSPL_CUSTOMER_MASTER.Cust_Code as Code,Customer_Name as Name,TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER.City_Name)>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_CUSTOMER_MASTER.State )>0 then TSPL_CUSTOMER_MASTER.State else '' end  as Address
           , TSPL_VENDOR_MASTER.Vendor_Code as [Secretary  Code],Vendor_Name as [Secretary  Name],VLC_Code_VLC_Uploader as [DCS Code]
            from TSPL_CUSTOMER_MASTER  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S'
            left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No  
            left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id 
            left join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code 
            left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code 
            left join TSPL_VLC_MASTER_HEAD on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code 
        where 2=2 and  TSPL_CUSTOMER_MASTER.CUSTOMER_FORM_TYPE='VSP'"

        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CattleFeedSale", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)


    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        GetReportID()
        LoadData()
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""
        If chkBalanceWise.Checked Then
            VarID += "_B"
        End If
        gv1.VarID = VarID
    End Sub
    Sub Reset()
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        txtLocation.Value = ""
        txtSubLocation.Value = ""
        txtCustomer.arrValueMember = Nothing
        txtItemCode.arrValueMember = Nothing
        ddCreditCash.SelectedIndex = 0
        chkBalanceWise.Checked = False
        EnableDisableControl(True)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val

    End Sub

    Private Sub LoadData()
        Try
            Dim dt As DataTable = New DataTable()
            If chkBalanceWise.Checked Then
                If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
                    If txtItemCode.arrValueMember.Count > 1 Then
                        clsCommon.MyMessageBoxShow(Me, "You can select only one item at a time when Balance Wise is checked", Me.Text)
                        Exit Sub
                    End If
                Else
                    clsCommon.MyMessageBoxShow(Me, "Please select only one item when Balance Wise is checked", Me.Text)
                    Exit Sub
                End If
                ddCreditCash.SelectedIndex = 2
                ddCreditCash.Enabled = False
                txtCustomer.Enabled = False
                dt = LoadBalanceWiseData()

            Else
                Dim finalQuery As String = ""
                Dim qry As String = ""

                Dim BaseQry As String = "select  TSPL_ITEM_MASTER.Item_Code, (VLC_Code_VLC_Uploader) as [DCS Code] , (VLC_Name) as [DCS Name],convert(varchar ,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Date,TSPL_SD_SHIPMENT_HEAD.Document_Date as Doc_Date ,
(TSPL_ITEM_MASTER.Short_Description) as [Short Description] , TSPL_SD_SHIPMENT_DETAIL. OrgUnit_code AS UOM , (TSPL_SD_SHIPMENT_DETAIL.Item_Cost) AS Rate , (TSPL_SD_SHIPMENT_DETAIL.Amount) as Amount, TSPL_SD_SHIPMENT_DETAIL.Qty,TSPL_SD_SHIPMENT_HEAD.Ref_No   from TSPL_SD_SHIPMENT_HEAD
left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE = TSPL_SD_SHIPMENT_HEAD.Document_Code
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code
left outer join tspl_customer_master on tspl_customer_master.Cust_Code = TSPL_SD_SHIPMENT_HEAD.customer_code
    left join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code 
            left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code 
            left join TSPL_VLC_MASTER_HEAD on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code 
			where 2 = 2  and TSPL_ITEM_MASTER.Item_Used_as = 'S' and convert( date ,TSPL_SD_SHIPMENT_HEAD.Document_Date , 103) >= CONVERT(date, '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") + "', 103)
              and convert( date ,TSPL_SD_SHIPMENT_HEAD.Document_Date , 103) <= CONVERT(date, '" + clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") + "', 103)"

                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    BaseQry += " And TSPL_SD_SHIPMENT_HEAD.Bill_To_Location = '" & txtLocation.Value & "' "
                End If

                If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                    BaseQry += " And TSPL_SD_SHIPMENT_HEAD.Sub_Location_code = '" & txtSubLocation.Value & "' "
                End If

                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    BaseQry += "  and TSPL_SD_SHIPMENT_HEAD.Customer_Code  in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
                End If

                If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
                    BaseQry += "  and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "

                End If
                If clsCommon.CompairString(ddCreditCash.SelectedItem.Text, "Credit") = CompairStringResult.Equal Then
                    qry = "select ROW_NUMBER() Over (Order By (Doc_Date)) As [SNo.], [DCS Name],[DCS Code] ,Doc_Date, Date , [Short Description],UOM ,Qty , Rate , Amount,[Challan No]   from (
              SELECT  [DCS Code] , [DCS Name] ,Doc_Date, Date , [Short Description] ,  UOM , Rate , Amount , Qty,Ref_No as[Challan No] FROM ("
                    BaseQry += " AND TSPL_SD_SHIPMENT_HEAD.Is_CashSale = 'N'"
                    BaseQry += " )XX ) xxx"
                    finalQuery = "" & qry & "   " & BaseQry & ""
                ElseIf clsCommon.CompairString(ddCreditCash.SelectedItem.Text, "Cash") = CompairStringResult.Equal Then
                    qry = "select ROW_NUMBER() Over (Order By (Doc_Date)) As [SNo.], [DCS Name],[DCS Code] ,Doc_Date, Date , [Short Description], UOM ,Qty , Rate , Amount,[Challan No]   from (
              SELECT  [DCS Code] , [DCS Name] ,Doc_Date, Date , [Short Description] ,  UOM , Rate , Amount , Qty,Item_Code,Ref_No as[Challan No] FROM ("
                    BaseQry += " AND TSPL_SD_SHIPMENT_HEAD.Is_CashSale = 'Y'"
                    BaseQry += " )XX ) xxx"
                    finalQuery = "" & qry & "   " & BaseQry & ""
                ElseIf clsCommon.CompairString(ddCreditCash.SelectedItem.Text, "Both") = CompairStringResult.Equal Then
                    qry = "select ROW_NUMBER() Over (Order By (Doc_Date)) As [SNo.], [DCS Name],[DCS Code] ,Doc_Date, Date , [Short Description] ,UOM,Qty , Rate , Amount   from 
(SELECT  [DCS Code] , max([DCS Name])[DCS Name] ,Doc_Date, Date , max([Short Description])[Short Description] , MAX(UOM) AS UOM , max(rate) as Rate , sum(Amount) Amount , sum(qty) Qty , Item_Code FROM ("

                    BaseQry += "  AND TSPL_SD_SHIPMENT_HEAD.Is_CashSale = 'N' OR TSPL_SD_SHIPMENT_HEAD.Is_CashSale = 'Y' "
                    BaseQry += " )XX "
                    If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
                        BaseQry += " where xx.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")"

                    End If
                    BaseQry += " GROUP  BY xx.Date , xx.[DCS Code], xx.Item_Code , xx.Doc_Date ) xxx "
                    finalQuery = "" & qry & "   " & BaseQry & " "
                End If
                finalQuery += " ORDER BY [SNo.]"

                dt = clsDBFuncationality.GetDataTable(finalQuery)
            End If
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.GroupDescriptors.Clear()
                gv1.EnableFiltering = True
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                EnableDisableControl(False)
                SetGridFormation()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Function LoadBalanceWiseData() As DataTable
        Dim BaseQry As String = ""

        BaseQry = "With CTE as (select VLC_Name , VLC_Code_VLC_Uploader , VLC_Code , Doc_Date , Date , [Short Description] , UOM , Qty , ActualQty , Rate , Amount ,OutQty , RecQty   from
       ( select  convert(varchar,Doc_Date , 103) Date,Doc_Date, VLC_Code , max(VLC_Code_VLC_Uploader)VLC_Code_VLC_Uploader , max(VLC_Name)VLC_Name, max([Short Description])[Short Description] , max(UOM_Code)UOM , 
       sum(Qty * case when Stock_Type = -1 and Transaction_Type = 'MCC-MSALE'  then 1 else 0 end ) as Qty, max(Rate * case when Stock_Type = -1 and Transaction_Type = 'MCC-MSALE'  then 1 else 0 end )  Rate ,sum(Amount * case when Stock_Type = -1 and Transaction_Type = 'MCC-MSALE'  then 1 else 0 end ) As Amount,
       sum (qty * case when (Stock_Type) = -1 and (Transaction_Type) = 'SA' THEN 1 else 0 end) as OutQty, sum(Qty * case when Stock_Type = 1  then 1 else 0 end ) as RecQty , sum(Qty * Stock_Type) as ActualQty  from ( " & Environment.NewLine & "" & BaseQry & ""

        BaseQry += "select Transaction_Type  ,Stock_Type, VLC_Code , VLC_Code_VLC_Uploader , VLC_Name ,Doc_Date ,xxx.Item_Code, [Short Description] , TabDiv.UOM_Code
         , ((Qty * TabMul.Conversion_Factor)/TabDiv.Conversion_Factor) as Qty , Rate , Amount   from ( " & Environment.NewLine & ""

        BaseQry += "select 'MCC-MSALE' as Transaction_Type, (VLC_Code_VLC_Uploader)  , (VLC_Name) ,(VLC_Code) ,convert(Date ,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Doc_Date ,
        (TSPL_ITEM_MASTER.Short_Description) as [Short Description] , TSPL_SD_SHIPMENT_DETAIL. OrgUnit_code AS UOM ,TSPL_SD_SHIPMENT_DETAIL.Qty, (TSPL_SD_SHIPMENT_DETAIL.Item_Cost) AS Rate , (TSPL_SD_SHIPMENT_DETAIL.Amount) as Amount, '-1' as Stock_Type, TSPL_ITEM_MASTER.Item_Code   
         from TSPL_SD_SHIPMENT_DETAIL
         left outer join TSPL_SD_SHIPMENT_HEAD on  TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE 
         left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code
         left outer join tspl_customer_master on tspl_customer_master.Cust_Code = TSPL_SD_SHIPMENT_HEAD.customer_code
         left join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code 
         left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code 
         left join TSPL_VLC_MASTER_HEAD on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code where 2 = 2  AND TSPL_SD_SHIPMENT_HEAD.Status = 1"
        If clsCommon.myLen(txtLocation.Value) > 0 Then
            BaseQry += "And TSPL_SD_SHIPMENT_HEAD.Bill_To_Location = '" & txtLocation.Value & "'"
        End If
        If clsCommon.myLen(txtLocation.Value) > 0 Then
            BaseQry += " and TSPL_SD_SHIPMENT_HEAD.Sub_Location_code = '" & txtSubLocation.Value & "'"
        End If
        If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
            BaseQry += " AND tspl_item_master.Item_Code in (" & clsCommon.GetMulcallString(txtItemCode.arrValueMember) & ")"
        End If

        BaseQry += "" & Environment.NewLine & " union all " & Environment.NewLine & ""

        BaseQry += "select 'SRN' as Transaction_Type , '' as VLC_Code_VLC_Uploader , '' as VLC_Name , '' as VLC_Code,convert(date ,TSPL_SRN_HEAD.SRN_Date,103) as Doc_Date , (tspl_item_master.Short_Description) as [Short Description] ,TSPL_SRN_DETAIL.Unit_code as UOM ,TSPL_SRN_DETAIL.SRN_Qty as Qty,0 AS Rate , 0  as Amount, '1' as Stock_Type , TSPL_SRN_DETAIL.Item_Code  
             from TSPL_SRN_DETAIL 
			  			left outer join TSPL_SRN_HEAD on TSPL_SRN_DETAIL.SRN_No = TSPL_SRN_HEAD.SRN_No
						left outer join tspl_item_master on tspl_item_master.Item_Code = TSPL_SRN_DETAIL.Item_Code
			  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SRN_HEAD.Bill_To_Location left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No = TSPL_SRN_HEAD.Against_PO  left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_SubLocation  on TSPL_LOCATION_MASTER_SubLocation.Location_Code=TSPL_SRN_HEAD.Sublocation_Code
			where 2 = 2 AND TSPL_SRN_HEAD.Status = 1 "

        If clsCommon.myLen(txtLocation.Value) > 0 Then
            BaseQry += " And TSPL_SRN_HEAD.Bill_To_Location = '" & txtLocation.Value & "'"
        End If
        If clsCommon.myLen(txtLocation.Value) > 0 Then
            BaseQry += " and TSPL_SRN_HEAD.Sublocation_Code = '" & txtSubLocation.Value & "'"
        End If
        If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
            BaseQry += " AND tspl_item_master.Item_Code in (" & clsCommon.GetMulcallString(txtItemCode.arrValueMember) & ")"
        End If

        BaseQry += "" & Environment.NewLine & " union all " & Environment.NewLine & ""
        BaseQry += " 
			   select 'SA' as Transaction_Type ,  '' as VLC_Code_VLC_Uploader , '' as VLC_Name , '' as VLC_Code , convert(date ,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as  Doc_Date, tspl_item_master.Short_Description as [Short Description] ,TSPL_ADJUSTMENT_DETAIL.Unit_code as UOM ,TSPL_ADJUSTMENT_DETAIL.Item_Quantity as Qty,0 AS Rate , 0  as Amount, (case when TSPL_ADJUSTMENT_HEADER.Trans_Type = 'In' THEN '1' ELSE '-1' END ) as Stock_Type ,TSPL_ADJUSTMENT_DETAIL.Item_Code  			   
			   from TSPL_ADJUSTMENT_DETAIL
			   left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_DETAIL.Adjustment_No = TSPL_ADJUSTMENT_HEADER.Adjustment_No
			   left outer join tspl_item_master on tspl_item_master.Item_Code = TSPL_ADJUSTMENT_DETAIL.Item_Code
               where 2 = 2  AND TSPL_ADJUSTMENT_HEADER.Posted = 'Y'"

        If clsCommon.myLen(txtLocation.Value) > 0 Then
            BaseQry += "And TSPL_ADJUSTMENT_HEADER.MainLocationCode = '" & txtLocation.Value & "'"
        End If
        If clsCommon.myLen(txtLocation.Value) > 0 Then
            BaseQry += "  and TSPL_ADJUSTMENT_HEADER.Loc_Code = '" & txtSubLocation.Value & "'"
        End If
        If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
            BaseQry += " AND tspl_item_master.Item_Code in (" & clsCommon.GetMulcallString(txtItemCode.arrValueMember) & ")"
        End If

        BaseQry += "" & Environment.NewLine & " union all  " & Environment.NewLine & ""

        BaseQry += "select 'MCC-MSALERETURN' as Transaction_Type , (VLC_Code_VLC_Uploader), (VLC_Name), VLC_Code , convert(date ,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)  as Doc_Date ,tspl_item_master.Short_Description as [Short Description] ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code UOM ,TSPL_SD_SALE_RETURN_DETAIL.Qty as Qty, 0 AS Rate , 0  as Amount, '1' as Stock_Type,TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
           from TSPL_SD_SALE_RETURN_DETAIL
left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE = TSPL_SD_SALE_RETURN_HEAD.Document_Code
			   left outer join tspl_item_master on tspl_item_master.Item_Code = TSPL_SD_SALE_RETURN_DETAIL.Item_Code
			   left outer join tspl_customer_master on tspl_customer_master.Cust_Code = TSPL_SD_SALE_RETURN_HEAD.customer_code
    left join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code 
            left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code 
            left join TSPL_VLC_MASTER_HEAD on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code 
			 where 2 = 2  AND TSPL_SD_SALE_RETURN_HEAD.Status = 1 "
        If clsCommon.myLen(txtLocation.Value) > 0 Then
            BaseQry += "And TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location = '" & txtLocation.Value & "'"
        End If
        If clsCommon.myLen(txtLocation.Value) > 0 Then
            BaseQry += "  and TSPL_SD_SALE_RETURN_HEAD.Sub_Location_code = '" & txtSubLocation.Value & "'"
        End If
        If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
            BaseQry += " AND tspl_item_master.Item_Code in (" & clsCommon.GetMulcallString(txtItemCode.arrValueMember) & ")"
        End If

        BaseQry += ") xxx " & Environment.NewLine & "
left join  TSPL_ITEM_UOM_DETAIL  as TabMul on TabMul.Item_Code=xxx.Item_Code and TabMul.UOM_Code= xxx.UOM
left join  TSPL_ITEM_UOM_DETAIL  as TabDiv on TabDiv.Item_Code=xxx.Item_Code and TabDiv.Default_UOM=1
 )xxxfinal " & Environment.NewLine & ""

        BaseQry += "group by Doc_Date  , VLC_Code ) xxxx )" & Environment.NewLine & "
        Select ROW_NUMBER() Over (Order By Doc_Date) AS [SNo.], xxxxx.VLC_Name,VLC_Code_VLC_Uploader,Date,[Short Description],UOM,Qty,Rate,Amount,OP as OPQty,OutQty,RecQty ,(xxxxx.OP + xxxxx.RecQty - xxxxx.OutQty) as Total_Qty ,  (xxxxx.OP + xxxxx.RecQty - xxxxx.OutQty - xxxxx.Qty) as Closing_Qty  from (
	   select CTE.* ,isnull((select sum(InnerCTE.ActualQty) from CTE as InnerCTE where 2= (case when InnerCTE.Doc_Date <  CTE.Doc_Date then 2 else 3 end )
          ),0) as OP  from CTE ) xxxxx " & Environment.NewLine & " where convert( date ,xxxxx.Doc_Date , 103) >= CONVERT(date, '" & txtFromDate.Value & "', 103)
and convert( date ,xxxxx.Doc_Date , 103) <= CONVERT(date, '" & txtToDate.Value & "', 103) "


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(BaseQry)

        Dim OPQty As Double = 0
        Dim dtMainTable As DataTable = New DataTable()
        dtMainTable.Columns.Add("SNo.", GetType(Integer))
        dtMainTable.Columns.Add("VLC_Name", GetType(String))
        dtMainTable.Columns.Add("VLC_Code_VLC_Uploader", GetType(String))
        dtMainTable.Columns.Add("Date", GetType(String))
        dtMainTable.Columns.Add("Short Description", GetType(String))
        dtMainTable.Columns.Add("UOM", GetType(String))
        dtMainTable.Columns.Add("Qty", GetType(Double))
        dtMainTable.Columns.Add("Rate", GetType(Decimal))
        dtMainTable.Columns.Add("Amount", GetType(Decimal))
        dtMainTable.Columns.Add("OPQty", GetType(Double))
        dtMainTable.Columns.Add("OutQty", GetType(Double))
        dtMainTable.Columns.Add("RecQty", GetType(Double))
        dtMainTable.Columns.Add("Total_Qty", GetType(Double))
        dtMainTable.Columns.Add("Closing_Qty", GetType(Double))

        For ii As Integer = 0 To dt.Rows.Count - 1
            Dim CattleDate As Date
            Dim OutQty As Double = 0
            Dim ReceivedQty As Double = 0
            Dim TotalQty As Double = 0
            Dim ClosingQty As Double = 0
            Dim SNO As Integer = 0

            If ii > 0 Then
                If dtMainTable.Rows.Count > 0 Then
                    SNO = dtMainTable.Rows.Count + 1
                End If
                CattleDate = dt.Rows(ii - 1)("Date")
                If clsCommon.CompairString(dt.Rows(ii)("Date"), CattleDate) = CompairStringResult.Equal Then
                    If dt.Rows(ii)("RecQty") > 0 AndAlso clsCommon.CompairString(dt.Rows(ii)("VLC_Code_VLC_Uploader"), "") = CompairStringResult.Equal AndAlso dt.Rows(ii)("Qty") = 0 Then

                        ReceivedQty = clsCommon.myCdbl(dt.Rows(ii)("RecQty")) + clsCommon.myCdbl(dt.Rows(ii - 1)("RecQty"))
                        OutQty = clsCommon.myCdbl(dt.Rows(ii)("OutQty")) + clsCommon.myCdbl(dt.Rows(ii - 1)("OutQty"))
                        TotalQty = clsCommon.myCdbl(dt.Rows(ii)("OPQty")) + ReceivedQty - OutQty
                        ClosingQty = TotalQty - clsCommon.myCdbl(dt.Rows(ii)("Qty"))
                        dtMainTable.Rows(ii - 1)("VLC_Name") = DBNull.Value
                        dtMainTable.Rows(ii - 1)("VLC_Code_VLC_Uploader") = DBNull.Value
                        dtMainTable.Rows(ii - 1)("SNo.") = dtMainTable.Rows.Count
                        dtMainTable.Rows(ii - 1)("OPQty") = dt.Rows(ii)("OPQty")
                        dtMainTable.Rows(ii - 1)("OutQty") = OutQty
                        dtMainTable.Rows(ii - 1)("RecQty") = ReceivedQty
                        dtMainTable.Rows(ii - 1)("Total_Qty") = TotalQty
                        dtMainTable.Rows(ii - 1)("Closing_Qty") = ClosingQty

                    ElseIf dt.Rows(ii)("RecQty") = 0 AndAlso clsCommon.CompairString(dt.Rows(ii)("VLC_Code_VLC_Uploader"), "") = CompairStringResult.Equal AndAlso dt.Rows(ii)("OutQty") > 0 Then
                        ReceivedQty = clsCommon.myCdbl(dt.Rows(ii)("RecQty")) + clsCommon.myCdbl(dt.Rows(ii - 1)("RecQty"))
                        OutQty = clsCommon.myCdbl(dt.Rows(ii)("OutQty")) + clsCommon.myCdbl(dt.Rows(ii - 1)("OutQty"))
                        TotalQty = clsCommon.myCdbl(dt.Rows(ii)("OPQty")) + ReceivedQty - OutQty
                        ClosingQty = TotalQty - clsCommon.myCdbl(dt.Rows(ii)("Qty"))
                        dtMainTable.Rows(ii - 1)("SNo.") = dtMainTable.Rows.Count
                        dtMainTable.Rows(ii - 1)("OPQty") = dt.Rows(ii)("OPQty")
                        dtMainTable.Rows(ii - 1)("OutQty") = OutQty
                        dtMainTable.Rows(ii - 1)("RecQty") = ReceivedQty
                        dtMainTable.Rows(ii - 1)("Total_Qty") = TotalQty
                        dtMainTable.Rows(ii - 1)("Closing_Qty") = ClosingQty

                    ElseIf dt.Rows(ii)("RecQty") > 0 AndAlso clsCommon.myCstr(dt.Rows(ii)("VLC_Code_VLC_Uploader")) <> "" AndAlso dt.Rows(ii)("Qty") = 0 Then

                        ReceivedQty = clsCommon.myCdbl(dt.Rows(ii)("RecQty")) + clsCommon.myCdbl(dt.Rows(ii - 1)("RecQty"))
                        OutQty = clsCommon.myCdbl(dt.Rows(ii)("OutQty")) + clsCommon.myCdbl(dt.Rows(ii - 1)("OutQty"))
                        TotalQty = clsCommon.myCdbl(dt.Rows(ii)("OPQty")) + ReceivedQty - OutQty
                        ClosingQty = TotalQty - clsCommon.myCdbl(dt.Rows(ii)("Qty"))
                        dtMainTable.Rows(ii - 1)("VLC_Name") = DBNull.Value
                        dtMainTable.Rows(ii - 1)("VLC_Code_VLC_Uploader") = DBNull.Value
                        dtMainTable.Rows(ii - 1)("SNo.") = dtMainTable.Rows.Count
                        dtMainTable.Rows(ii - 1)("OPQty") = dt.Rows(ii)("OPQty")
                        dtMainTable.Rows(ii - 1)("OutQty") = OutQty
                        dtMainTable.Rows(ii - 1)("RecQty") = ReceivedQty
                        dtMainTable.Rows(ii - 1)("Total_Qty") = TotalQty
                        dtMainTable.Rows(ii - 1)("Closing_Qty") = ClosingQty

                    ElseIf clsCommon.myCstr(dt.Rows(ii)("VLC_Code_VLC_Uploader")) <> "" Then
                        dtMainTable.Rows.Add(SNO, clsCommon.myCstr(dt.Rows(ii).Item("VLC_Name")), clsCommon.myCstr(dt.Rows(ii).Item("VLC_Code_VLC_Uploader")), clsCommon.myCstr(dt.Rows(ii).Item("Date")), clsCommon.myCstr(dt.Rows(ii).Item("Short Description")), clsCommon.myCstr(dt.Rows(ii).Item("UOM")), clsCommon.myCdbl(dt.Rows(ii).Item("Qty")), clsCommon.myCDecimal(dt.Rows(ii).Item("Rate")), clsCommon.myCDecimal(dt.Rows(ii).Item("Amount")), DBNull.Value, clsCommon.myCdbl(dt.Rows(ii).Item("OutQty")), clsCommon.myCdbl(dt.Rows(ii).Item("RecQty")), DBNull.Value, (clsCommon.myCdbl(dtMainTable.Rows(ii - 1).Item("Closing_Qty"))) - clsCommon.myCdbl(dt.Rows(ii).Item("Qty")))

                    End If
                Else
                    dtMainTable.Rows.Add(SNO, clsCommon.myCstr(dt.Rows(ii).Item("VLC_Name")), clsCommon.myCstr(dt.Rows(ii).Item("VLC_Code_VLC_Uploader")), clsCommon.myCstr(dt.Rows(ii).Item("Date")), clsCommon.myCstr(dt.Rows(ii).Item("Short Description")), clsCommon.myCstr(dt.Rows(ii).Item("UOM")), clsCommon.myCdbl(dt.Rows(ii).Item("Qty")), clsCommon.myCDecimal(dt.Rows(ii).Item("Rate")), clsCommon.myCDecimal(dt.Rows(ii).Item("Amount")), clsCommon.myCdbl(dt.Rows(ii).Item("OPQty")), clsCommon.myCdbl(dt.Rows(ii).Item("OutQty")), clsCommon.myCdbl(dt.Rows(ii).Item("RecQty")), clsCommon.myCdbl(dt.Rows(ii).Item("Total_Qty")), clsCommon.myCdbl(dt.Rows(ii).Item("Closing_Qty")))
                End If
            Else
                dtMainTable.Rows.Add(dt.Rows(ii).Item("SNo."), clsCommon.myCstr(dt.Rows(ii).Item("VLC_Name")), clsCommon.myCstr(dt.Rows(ii).Item("VLC_Code_VLC_Uploader")), clsCommon.myCstr(dt.Rows(ii).Item("Date")), clsCommon.myCstr(dt.Rows(ii).Item("Short Description")), clsCommon.myCstr(dt.Rows(ii).Item("UOM")), clsCommon.myCdbl(dt.Rows(ii).Item("Qty")), clsCommon.myCDecimal(dt.Rows(ii).Item("Rate")), clsCommon.myCDecimal(dt.Rows(ii).Item("Amount")), clsCommon.myCdbl(dt.Rows(ii).Item("OPQty")), clsCommon.myCdbl(dt.Rows(ii).Item("OutQty")), clsCommon.myCdbl(dt.Rows(ii).Item("RecQty")), clsCommon.myCdbl(dt.Rows(ii).Item("Total_Qty")), clsCommon.myCdbl(dt.Rows(ii).Item("Closing_Qty")))

            End If
        Next
        Return dtMainTable
    End Function
    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        gv1.EnableFiltering = True
        gv1.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next
        If chkBalanceWise.Checked Then
            gv1.Columns("VLC_Name").HeaderText = "DCS Name"
            gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS Code"
            gv1.Columns("OPQty").HeaderText = "Opening Qty"
            gv1.Columns("OutQty").HeaderText = "Out Qty"
            gv1.Columns("RecQty").HeaderText = "Receiving Qty"
            gv1.Columns("Total_Qty").HeaderText = "Total Qty"
            gv1.Columns("Closing_Qty").HeaderText = "Closing Qty"


        Else
            gv1.Columns("Doc_Date").IsVisible = False

        End If
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim Qty As New GridViewSummaryItem("Qty", "", GridAggregateFunction.Sum)
        summaryRowItem.Add(Qty)
        Dim Amount As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Amount)

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub txtItemCode__My_Click(sender As Object, e As EventArgs) Handles txtItemCode._My_Click
        Dim qry As String = ""
        qry = "select Item_Code as Code , Item_Desc  as [Item Description] , Short_Description AS [Short Description] from TSPL_ITEM_MASTER where Item_Used_as ='S'"

        txtItemCode.arrValueMember = clsCommon.ShowMultipleSelectForm("CattleFeedSale", qry, "Code", "Item Description", txtItemCode.arrValueMember, txtItemCode.arrDispalyMember)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try

            If gv1.Rows.Count > 0 Then
                Dim style As New GridPrintStyle()
                style.PrintGrouping = True
                style.HeaderCellBackColor = Color.White
                style.GroupRowBackColor = Color.White
                style.SummaryCellBackColor = Color.White
                style.PrintSummaries = True
                gv1.PrintStyle = style

                Dim doc As New clsMyPrintDocument()

                doc.Margins.Top = 50
                doc.Margins.Bottom = 50
                doc.Margins.Left = 50
                doc.Margins.Right = 50
                doc.HeaderHeight = 90
                doc.Landscape = True
                doc.AssociatedObject = gv1

                doc.DocumentName = objCommonVar.CurrentCompanyName
                doc.MiddleHeader = clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptCattleFeedSaleReport & "'")
                doc.LeftHeader = "Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + Environment.NewLine & "Company : " & objCommonVar.CurrentCompanyName + Environment.NewLine + "Location: " + txtLocation.Value + Environment.NewLine + "Sub Location: " + txtSubLocation.Value + Environment.NewLine + "Item: " + txtItemCode.arrDispalyMember(0)
                doc.RightHeader = "Print Date(" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm : ss tt") + ")"
                doc.HeaderFont = New Font("Segoe UI", 10, FontStyle.Bold)

                doc.AssociatedObject = gv1

                doc.RightFooter = "Page [Page #] Of [Total Pages]"

                Dim dialog As New RadPrintPreviewDialog
                dialog.Document = doc
                dialog.ToolMenu.Visible = True
                dialog.Show()

                doc.Print()
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found To export", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Print Date (" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm:ss tt") + ")")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptCattleFeedSaleReport & "'"))
                arrHeader.Add("Date Range : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    arrHeader.Add("Location : " & txtLocation.Value)
                End If
                If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                    arrHeader.Add("Sub Location : " & txtSubLocation.Value)
                End If
                If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
                    arrHeader.Add("Item : " & txtItemCode.arrDispalyMember(0))

                End If


                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                Throw New Exception("No data found to export.")

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkBalanceWise_CheckStateChanged(sender As Object, e As EventArgs) Handles chkBalanceWise.CheckStateChanged
        If chkBalanceWise.Checked Then
            txtCustomer.Enabled = False
            ddCreditCash.SelectedIndex = 2
            ddCreditCash.Enabled = False
        Else
            txtCustomer.Enabled = True
            ddCreditCash.Enabled = True
        End If
    End Sub
End Class