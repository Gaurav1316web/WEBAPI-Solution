' Sanjeet(11/01/2017)
' UDL Case decimal places 3 in this report GRN,MRN,SRN,Rejection etc(Parteek) 27/10/2017
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports System.IO
Public Class RptPendingPO
    Inherits FrmMainTranScreen

    'Dim is_Load_MRN As Boolean = False
    Dim dtCategory As DataTable
    Dim dtData As New DataTable
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptPendingPO)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
    End Sub

    Private Sub frmPo_action_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            SetUserMgmtNew()
            If clsCommon.CompairString("UDL", objCommonVar.CurrentCompanyCode) = CompairStringResult.Equal Then
                chk_groupby.Visible = True
            Else
                chk_groupby.Visible = False
            End If
            ' is_Load_MRN = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowGRN, clsFixedParameterCode.ShowGRN, Nothing)) = 1 And clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowMRN, clsFixedParameterCode.ShowMRN, Nothing)) = 1, True, False)
            dtpfromdate.Value = clsCommon.GETSERVERDATE()
            dtpTodate.Value = clsCommon.GETSERVERDATE()
            LoadDocuemntNo()
            LoadVendor()
            ItemLoad()
            LoadCategoryTypes()
            LoadLocation()
            LoadCategory()
            chkdocAll.IsChecked = True
            rbtnCategoryAll.IsChecked = True
            chkVendor_all.IsChecked = True
            chkItemAll.IsChecked = True
            chkLocationAll.IsChecked = True

            RadPageView1.SelectedPage = RadPageViewPage1
            gv1.EnableGrouping = True
            'KUNAL > KDIL > FILTERS WAS NOT ENABLED ON USER WISE
            Me.gv.MasterTemplate.EnableFiltering = True
        Catch ex As Exception

        End Try
    End Sub

    Sub LoadCategoryTypes()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Both")
        dt.Rows.Add("Regular")
        dt.Rows.Add("Capex")

        ddlCategory.DataSource = dt
        ddlCategory.ValueMember = "Code"
        ddlCategory.DisplayMember = "Code"
        ddlCategory.SelectedIndex = 0
    End Sub

    Public Sub LoadCategory()

        dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")

        gvCategory.DataSource = Nothing
        Dim qry As String = "select cast( 0 as bit) as SEL,ITEM_CATEGORY_CODE AS Code,DESCRIPTION as NAME from TSPL_ITEM_CATEGORY_LEVEL ORDER BY CATEGORY_LEVEL"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        gvCategory.DataSource = dt
        gvCategory.Columns("SEL").ReadOnly = False
        gvCategory.Columns("SEL").Width = 30
        gvCategory.Columns("SEL").HeaderText = " "

        gvCategory.Columns("CODE").ReadOnly = True
        gvCategory.Columns("CODE").Width = 100
        gvCategory.Columns("CODE").HeaderText = "Code"

        gvCategory.Columns("NAME").ReadOnly = True
        gvCategory.Columns("NAME").Width = 200
        gvCategory.Columns("NAME").HeaderText = "Name"

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

    Private Sub btnclose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        PrintData1()
    End Sub
    Sub PrintData1()

        If chkDoc_select.IsChecked AndAlso cbgDocument.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Documnet Number", Me.Text)
            Return

        End If
        If chkVendor_select.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor", Me.Text)
            Return
        End If
        If chkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please selete one Item", Me.Text)
            Return
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select one location ", Me.Text)
            Return

        End If
        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy")
        Dim todate As String = clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")

        PrintData(fromdate, todate, chkDoc_select.IsChecked, cbgDocument.CheckedValue, chkVendor_select.IsChecked, cbgVendor.CheckedValue, chkItemSelect.IsChecked, cbgItem.CheckedValue, chkLocationSelect.IsChecked, cbgLocation.CheckedValue)
    End Sub
    Public Sub PrintData(ByVal FromDate As String, ByVal ToDate As String, ByVal isDocSelect As Boolean, ByVal ArrDoc As ArrayList, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal isitemSelect As Boolean, ByVal arrItem As ArrayList, ByVal isLocation As Boolean, ByVal arrLocation As ArrayList)
        '        Dim Address As String = ""
        '        Dim Item As String = ""
        '        Dim location As String = ""
        '        Dim DocNo As String = ""
        '        Dim Vendor As String = ""
        '        Dim StrItem As String = ""
        '        Dim Strlocation As String = ""
        '        Dim StrDocNo As String = ""
        '        Dim StrVendor As String = ""

        '        If cbgVendor.CheckedValue.Count > 0 Then
        '            Vendor = ("'" + clsCommon.GetMulcallString(ArrVendor) + "'")
        '            StrVendor = Vendor.Replace("'", "")
        '        End If
        '        If cbgDocument.CheckedValue.Count > 0 Then
        '            DocNo = "'" + clsCommon.GetMulcallString(ArrDoc) + "'"
        '            StrDocNo = DocNo.Replace("'", "")
        '        End If
        '        If cbgLocation.CheckedValue.Count > 0 Then
        '            location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
        '            Strlocation = location.Replace("'", "")
        '        End If
        '        If cbgItem.CheckedValue.Count > 0 Then
        '            Item = "'" + clsCommon.GetMulcallString(arrItem) + "'"
        '            StrItem = Item.Replace("'", "")
        '        End If
        '        Dim status As String = "ALL"
        '        If Not rdobtnall.IsChecked Then
        '            If rdoPonever.IsChecked = True Then
        '                status = "Never"
        '            End If
        '            If rdoPOPartial.IsChecked = True Then
        '                status = "Partial "
        '            End If
        '            If rdoCompleted.IsChecked = True Then
        '                status = "Completed"
        '            End If
        '        End If
        '        'Dim Balance As String = ""
        '        'If is_Load_MRN = True Then
        '        '    Balance = "po_qty-grnqty"
        '        'Else
        '        '    Balance = "po_qty-srn_qty"
        '        'End If
        '        'If clsCommon.myLen(Balance) <= 0 Then
        '        '    Balance = "0"
        '        'End If
        '        If isLocation AndAlso arrLocation.Count = 1 Then
        '            Address = "(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(TSPL_TDS_STATE_MASTER .State_Name)> 0 then ',' else '' end  +(TSPL_TDS_STATE_MASTER .State_Name )) from tspl_location_master LEFT OUTER JOIN TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER .State =TSPL_TDS_STATE_MASTER .State_Code  where Location_Code =TSPL_PURCHASE_ORDER_HEAD .Bill_To_Location )"
        '        Else
        '            Address = "(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) "
        '        End If

        '        ' Dim strQuery As String = "select TSPL_PURCHASE_ORDER_HEAD.comp_code,'" + FromDate + "' as  FromDate,'" + ToDate + "' as ToDate,'" + StrDocNo + "' as StrDocNo ,'" + StrVendor + "' as StrVendor,'" + Strlocation + "' as Strlocation,'" + StrItem + "' as StrItem,'" + status + "' as status,TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_No as purchase_no , convert (date, TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_Date,103) as po_date,convert (varchar, TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_Date,103) as po_date_var ,TSPL_PURCHASE_ORDER_HEAD .Vendor_Code  as vendor_no ,TSPL_PURCHASE_ORDER_HEAD .Vendor_Name  as Vendor_name , TSPL_COMPANY_MASTER.Comp_Name as compname," + Address + " as address1,TSPL_PURCHASE_ORDER_HEAD .delivery_date as Delivaery_date,TSPL_PURCHASE_ORDER_DETAIL .Item_Cost as po_rate ,TSPL_PURCHASE_ORDER_DETAIL.item_code as item_code,TSPL_PURCHASE_ORDER_DETAIL.item_desc as itemdesc,TSPL_PURCHASE_ORDER_DETAIL.purchaseorder_qty as po_qty,TSPL_PURCHASE_ORDER_DETAIL.unit_code as uom ,tspl_purchase_order_head.Bill_To_Location as Location,(select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code =TSPL_PURCHASE_ORDER_HEAD .Bill_To_Location )as Location_Desc, stuff((select ',' + isnull(GRN_NO,'') from TSPL_GRN_DETAIL where TSPL_GRN_DETAIL.PO_Id=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No and  TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code for xml path ('')),1,1,'' )as GRN_NO ,(select isnull(SUM(isnull(GRN_Qty,0)),0.0) from TSPL_GRN_DETAIL where TSPL_GRN_DETAIL.PO_Id=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No and  TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code) as GRNQty,(select isnull(SUM(isnull(Tolerence_Qty,0)),0.0) from TSPL_GRN_DETAIL where TSPL_GRN_DETAIL.PO_Id=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No and  TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code) as Tolerence_Qty,( select isnull(SUM(ISNULL(TSPL_MRN_DETAIL.MRN_Qty,0)),0.0) from TSPL_MRN_DETAIL where TSPL_MRN_DETAIL .Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code and  TSPL_MRN_DETAIL .GRN_Id in (select GRN_No from TSPL_GRN_DETAIL where TSPL_GRN_DETAIL.PO_Id=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No and  TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code) ) as MRNQty,(select isnull(SUM(ISNULL(TSPL_SRN_DETAIL.SRN_Qty,0)),0.0) from TSPL_SRN_DETAIL where TSPL_SRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL .Item_Code and TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=TSPL_SRN_DETAIL.PO_ID  )as SRN_Qty,(select isnull(SUM(ISNULL(TSPL_SRN_DETAIL.Item_Cost ,0)),0.0) from TSPL_SRN_DETAIL where TSPL_SRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL .Item_Code  and MRN_Id in(select TSPL_MRN_DETAIL.MRN_No from TSPL_MRN_DETAIL where TSPL_MRN_DETAIL .Item_Code=TSPL_PURCHASE_ORDER_DETAIL .Item_Code  and  TSPL_MRN_DETAIL .GRN_Id in (select GRN_No from TSPL_GRN_DETAIL where TSPL_GRN_DETAIL.PO_Id=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No  and  TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL .Item_Code )))as SRN_Cost,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code as [MainGroupCode],  TSPL_ITEM_CATEGORY_LEVEL.description as [MainGroup],TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values as [GroupCode], TSPL_ITEM_CATEGORY_LEVEL_VALUES.description as [GroupName] from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD  on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No  left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_PURCHASE_ORDER_HEAD.comp_code left outer join TSPL_ITEM_MASTER on TSPL_PURCHASE_ORDER_DETAIL.item_code=tspl_item_master.item_code LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.item_code=  TSPL_ITEM_MASTER.item_code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL ON TSPL_ITEM_CATEGORY_LEVEL.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.code= TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values  where 2=2 "
        '        Dim strQuery As String = "select MAX(TSPL_PURCHASE_ORDER_HEAD.comp_code) AS comp_code,'" + FromDate + "' as  FromDate,'" + ToDate + "' as ToDate,'' as StrDocNo , " & _
        '      "'' as StrVendor, '' as Strlocation,'' as StrItem,'" + status + "' as status,MAX(TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_No) as purchase_no,convert (varchar, max(TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_Date),103) as po_date_var ," & _
        '" MAX(TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_Date) as po_date , CONVERT(VARCHAR , MAX(TSPL_SRN_HEAD.SRN_Date) , 103)  AS SRN_Date," & _
        '  " MAX(TSPL_PURCHASE_ORDER_HEAD .Vendor_Code)  as vendor_no ,MAX(TSPL_PURCHASE_ORDER_HEAD .Vendor_Name)  as Vendor_name , MAX(TSPL_COMPANY_MASTER.Comp_Name) as compname, " & _
        '      "  (MAX(TSPL_COMPANY_MASTER.Add1) + case When MAX(TSPL_COMPANY_MASTER.Add2)='' Then '' else ', '+ Convert(Varchar,MAX(TSPL_COMPANY_MASTER.Add2), 103) End +  Case When MAX(TSPL_COMPANY_MASTER.Add3)='' Then '' Else ', '+ COnvert( Varchar,MAX(TSPL_COMPANY_MASTER.Add3),103) end +  case When MAX(TSPL_COMPANY_MASTER.City_Code) ='' then '' else ', '+ Convert(Varchar,MAX(TSPL_COMPANY_MASTER.City_Code), 103) end+  Case When " & _
        '" MAX(TSPL_COMPANY_MASTER.State)='' Then '' else ', '+Convert(Varchar, MAX(TSPL_COMPANY_MASTER.State)) end +   Case When MAX(TSPL_COMPANY_MASTER.Pincode)='' Then '' Else ', '+ Convert(Varchar,MAX(TSPL_COMPANY_MASTER.Pincode), 103)  end)  as address1," & _
        '      "  MAX(TSPL_PURCHASE_ORDER_HEAD .delivery_date) as Delivaery_date,MAX(TSPL_PURCHASE_ORDER_DETAIL .Item_Cost) as po_rate , " & _
        '" MAX(TSPL_PURCHASE_ORDER_DETAIL.item_code) as item_code,MAX(TSPL_PURCHASE_ORDER_DETAIL.item_desc) as itemdesc,max(TSPL_ITEM_MASTER.AllowSRNWithoutShortReject) as Allow_SRN," & _
        '" SUM(TSPL_PURCHASE_ORDER_DETAIL.purchaseorder_qty) as po_qty, MAX(TSPL_PURCHASE_ORDER_DETAIL.unit_code) as uom ," & _
        '" MAX(tspl_purchase_order_head.Bill_To_Location) as Location, (select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  =MAX(TSPL_PURCHASE_ORDER_HEAD .Bill_To_Location) ) as Location_Desc," & _
        '"isnull(MAX(TSPL_GRN_DETAIL.GRN_NO),'') as GRN_NO ,CONVERT(VARCHAR , MAX(TSPL_GRN_HEAD.GRN_Date) , 103)   AS  GRN_Date ,isnull(MAX(TSPL_GRN_DETAIL.GRN_Qty),0) as GRNQty,isnull(MAX(TSPL_GRN_DETAIL.Tolerence_Qty),0) AS Tolerence_Qty,ISNULL(MAX(TSPL_MRN_DETAIL.MRN_Qty),0)as  MRNQty, ISNULL(MAX(TSPL_SRN_DETAIL.SRN_Qty),0)as SRN_Qty,ISNULL(SUM(TSPL_SRN_DETAIL.Short_Qty),0)as Short_Qty,ISNULL(SUM(TSPL_SRN_DETAIL.Leak_Qty),0)as Leak_Qty,ISNULL(SUM(TSPL_SRN_DETAIL.Burst_Qty),0)as Burst_Qty,ISNULL(SUM(TSPL_SRN_DETAIL.Rejected_Qty),0)as Rejected_Qty,ISNULL(SUM(TSPL_SRN_DETAIL.Item_Cost) ,0)as SRN_Cost,MAX(TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code) as [MainGroupCode],   MAX(TSPL_ITEM_CATEGORY_LEVEL.description) as [Main Group],MAX(TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values) as [GroupCode], MAX(TSPL_ITEM_CATEGORY_LEVEL_VALUES.description) as [Group Name]   ,case when coalesce(( isnull(ISNULL(SUM(TSPL_SRN_DETAIL.SRN_Qty),0),0.0)),0)<=0 then  coalesce(( select (ISNULL(SUM(TSPL_MRN_DETAIL.MRN_Qty),0))  ) , ( isnull((isnull(SUM(TSPL_GRN_DETAIL.GRN_Qty),0)),0.0) )) end as QCQty,max(TSPL_PURCHASE_ORDER_HEAD.Category) as Category,case when max(isnull(TSPL_PURCHASE_ORDER_HEAD.Emergency,0))=0 then 'No' else 'Yes' end as Emergency,MAX(isnull(TSPL_PURCHASE_ORDER_HEAD.Capex_Code,'')) as [Capex Code],MAX(isnull(TSPL_CAPEX_MASTER.Description,'')) as [Capex Description],MAX(isnull(TSPL_PURCHASE_ORDER_HEAD.Capex_SubCode,'')) as [Capex Sub Code],MAX(isnull(TSPL_CAPEX_BUDGET_MASTER.Description,'')) as [Capex Sub Description]  " & _
        '" from (SELECT sum(TSPL_PURCHASE_ORDER_DETAIL.purchaseorder_qty) as purchaseorder_qty,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,TSPL_PURCHASE_ORDER_DETAIL.Item_Code,TSPL_PURCHASE_ORDER_DETAIL.Unit_code,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost,TSPL_PURCHASE_ORDER_DETAIL.Item_Desc from TSPL_PURCHASE_ORDER_DETAIL GROUP BY TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,TSPL_PURCHASE_ORDER_DETAIL.Item_Code,TSPL_PURCHASE_ORDER_DETAIL.Unit_code,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost,TSPL_PURCHASE_ORDER_DETAIL.Item_Desc ) as tspl_purchase_order_detail " & _
        '" left outer join TSPL_PURCHASE_ORDER_HEAD  on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No " & _
        '" left join TSPL_GRN_DETAIL on isnull(TSPL_GRN_DETAIL.PO_Id,'') =isnull(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,'') and TSPL_GRN_DETAIL.Item_Code " & _
        '" =tspl_purchase_order_detail.Item_Code  " & _
        '" left join TSPL_GRN_HEAD on isnull(TSPL_GRN_HEAD.grn_no,'')=isnull(TSPL_GRN_DETAIL.grn_no,'') " & _
        '" left join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL .Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code " & _
        '" and  isnull(TSPL_MRN_DETAIL .GRN_Id,'') =isnull(TSPL_GRN_DETAIL.grn_no,'') and isnull(TSPL_MRN_DETAIL.PO_Id,'')=isnull(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,'') and  " & _
        '" TSPL_MRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code " & _
        '" left join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL .Item_Code and isnull(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,'')= " & _
        '" isnull(TSPL_SRN_DETAIL.PO_ID ,'') " & _
        '" and isnull(TSPL_MRN_DETAIL.mrn_no,'')=isnull(TSPL_SRN_DETAIL.mrn_id,'') and isnull(TSPL_GRN_DETAIL.grn_no,'')=isnull(TSPL_MRN_DETAIL.grn_id,'') " & _
        '" left join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_no " & _
        '" left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_PURCHASE_ORDER_HEAD.comp_code " & _
        '" left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =tspl_purchase_order_detail.Item_Code " & _
        '"  LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.item_code=  TSPL_ITEM_MASTER.item_code " & _
        '"  LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL ON TSPL_ITEM_CATEGORY_LEVEL.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " & _
        '" LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and " & _
        '" TSPL_ITEM_CATEGORY_LEVEL_VALUES.code= TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values " & _
        '" left outer join TSPL_REQUISITION_HEAD on isnull(TSPL_PURCHASE_ORDER_HEAD.Against_Requisition,'')=isnull(TSPL_REQUISITION_HEAD.Requisition_Id,'') " & _
        '" LEFT OUTER JOIN TSPL_CAPEX_MASTER ON TSPL_CAPEX_MASTER.Code =TSPL_PURCHASE_ORDER_HEAD.Capex_Code" & _
        '" LEFT OUTER JOIN TSPL_CAPEX_BUDGET_MASTER ON TSPL_CAPEX_BUDGET_MASTER.Code =TSPL_PURCHASE_ORDER_HEAD.Capex_SubCode" & _
        '" GROUP BY TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,TSPL_PURCHASE_ORDER_DETAIL.Item_Code,TSPL_PURCHASE_ORDER_DETAIL.Unit_code HAVING 2=2 "

        '        If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
        '            strQuery += " and max(TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_No) in (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
        '        End If
        '        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
        '            strQuery += " and max(tspl_purchase_order_head.Bill_To_Location) in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        '        End If
        '        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
        '            strQuery += "  and max(TSPL_PURCHASE_ORDER_HEAD .Vendor_Code) in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
        '        End If
        '        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
        '            strQuery += "  and  max(TSPL_PURCHASE_ORDER_DETAIL.item_code) in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
        '        End If

        '        Dim strWhrCatg As String = ""
        '        strWhrCatg = ""
        '        Dim isFirstTime As Boolean = True

        '        If rbtnCategorySelect.IsChecked Then
        '            Dim IsApplicable As Boolean = False
        '            strQuery += " and max(TSPL_PURCHASE_ORDER_DETAIL.item_code) in  (select Item_code  from TSPL_ITEM_MASTER_CATEGORY where Item_code in (select distinct item_code from TSPL_PURCHASE_ORDER_DETAIL) and ( " + Environment.NewLine

        '            For ii As Integer = 0 To gvCategory.RowCount - 1
        '                If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then

        '                    If Not isFirstTime Then
        '                        strQuery += " or "
        '                    End If

        '                    strQuery += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("Code").Value) + "' " + Environment.NewLine
        '                    Dim arr As Dictionary(Of String, Object) = gvCategory.Rows(ii).Tag
        '                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
        '                        For Each strInn As String In arr.Keys
        '                            strQuery += " and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(strInn) + "' "
        '                        Next
        '                    End If ''tag arr cond.
        '                    strQuery += " ) "
        '                    isFirstTime = False
        '                End If
        '            Next
        '            strQuery += " ))"

        '        End If
        '        '---------------------------'
        '        strQuery += " and convert(date,max(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date),103)>= convert(date,'" + FromDate + "',103) and convert(date,max(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date),103)<= convert(date,'" + ToDate + "',103) and max(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Type) <>  'J' "

        '        Dim qry12 As String = strQuery
        '        strQuery = "select distinct comp_code,Fromdate,Todate,strdocno,strvendor,strlocation,stritem,purchase_no,po_date,po_date_var,vendor_no as vendor_no,vendor_name,compname,address1,delivaery_date,po_rate,item_code,itemdesc,po_qty,((po_qty+Tolerence_Qty)-GRNQty +Short_Qty+Leak_Qty+Burst_Qty+Rejected_Qty) as [Balance Qty],uom,location,location_desc,grnqty,Tolerence_Qty,mrnqty,srn_qty,srn_cost ,GRN_NO,status,Category,Emergency,[Capex Code],[Capex Description],[Capex Sub Code],[Capex Sub Description] from (" + qry12 + ")ass1"

        '        qry12 = strQuery

        '        strQuery = "select TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,* from (" + qry12 + ")ax left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.comp_code=ax.comp_code where ax.comp_code='" + objCommonVar.CurrentCompanyCode + "' "
        '        If Not rdobtnall.IsChecked Then
        '            If rdoPonever.IsChecked = True Then
        '                strQuery += " and len(isnull(GRN_NO,''))<=0 "
        '            End If
        '            If rdoPOPartial.IsChecked = True Then

        '                strQuery += " and [Balance Qty]<>0 and len(isnull(GRN_NO,''))>0 "
        '            End If
        '            If rdoCompleted.IsChecked = True Then
        '                strQuery += " and [Balance Qty] =0 "
        '            End If
        '        End If
        '        Dim strOrederCls As String = " order by  Po_Date " ' purchase_no replace by Po_Date 
        '        Dim qry As String = strQuery + strOrederCls

        '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dtData Is Nothing OrElse dtData.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        Else
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dtData, "RptPendingPo", "Purchase Order Pending Report")
            frmCRV = Nothing
        End If
    End Sub
    Private Sub chkDocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkdocAll.ToggleStateChanged, chkDoc_select.ToggleStateChanged

        cbgDocument.Enabled = Not chkdocAll.IsChecked
    End Sub
    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendor_all.ToggleStateChanged, chkVendor_select.ToggleStateChanged

        cbgVendor.Enabled = Not chkVendor_all.IsChecked
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        LoadCategoryTypes()
        RadGroupBox7.Enabled = True
        gv.DataSource = Nothing
        dtData.Rows.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub frmPo_action_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Alt And e.KeyCode = Keys.P Then
            PrintData1()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If

    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged, rbtnCategorySelect.ToggleStateChanged

        gvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub
    Public Sub ReferehData(ByVal FromDate As String, ByVal ToDate As String, ByVal isDocSelect As Boolean, ByVal ArrDoc As ArrayList, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal isitemSelect As Boolean, ByVal arrItem As ArrayList, ByVal isLocation As Boolean, ByVal arrLocation As ArrayList)
        'Ticket No- UDL/16/10/18-000232 ,One PO item used in multiple GRN, sum(ISNULL(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty,0)) -> max(ISNULL(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty,0))
        Dim Address As String = ""
        Dim Item As String = ""
        Dim location As String = ""
        Dim DocNo As String = ""
        Dim Vendor As String = ""
        Dim StrItem As String = ""
        Dim Strlocation As String = ""
        Dim StrDocNo As String = ""
        Dim StrVendor As String = ""
        Dim status As String = "ALL"
        Dim postatus As String = Nothing
        postatus = " (SELECT case when isnull(max(PO.close_yn),'N')='Y' then 'Close' WHEN ((MAX(ISNULL(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty,0))+sum(ISNULL(TSPL_GRN_DETAIL.Tolerence_Qty,0)))-ISNULL(SUM(CASE WHEN TSPL_GRN_HEAD.IsCancel=0 THEN TSPL_GRN_DETAIL.GRN_Qty ELSE 0 END),0) +sum(ISNULL(TSPL_SRN_DETAIL.Short_Qty,0))+sum(ISNULL(TSPL_SRN_DETAIL.Leak_Qty,0))+sum(ISNULL(TSPL_SRN_DETAIL.Burst_Qty,0))+sum(ISNULL(TSPL_SRN_DETAIL.Rejected_Qty,coalesce(TSPL_QC_CHECK_SRN_DETAIL.Reject_Qty,0))))>0 THEN 'Open' else 'Complete' end as status FROM TSPL_PURCHASE_ORDER_HEAD PO" & _
                    " LEFT OUTER JOIN (select PurchaseOrder_No,sum(PurchaseOrder_Qty) as PurchaseOrder_Qty,Item_Code  from TSPL_PURCHASE_ORDER_DETAIL group by PurchaseOrder_No,Item_Code)TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=PO.PurchaseOrder_No" & _
                    " LEFT OUTER JOIN TSPL_GRN_DETAIL ON TSPL_GRN_DETAIL.PO_Id=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No and TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code" & _
                    " LEFT OUTER JOIN TSPL_GRN_HEAD ON TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No" & _
                    " left join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL.GRN_Id=TSPL_GRN_DETAIL.GRN_No  and  TSPL_MRN_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code  and  TSPL_MRN_DETAIL.Unit_code=TSPL_GRN_DETAIL.Unit_Code" & _
                    " left join TSPL_QC_CHECK_SRN_DETAIL on TSPL_MRN_DETAIL.MRN_No=TSPL_QC_CHECK_SRN_DETAIL.MRN_No  and  TSPL_MRN_DETAIL.Item_Code=TSPL_QC_CHECK_SRN_DETAIL.Item_Code and  TSPL_MRN_DETAIL.Unit_code=TSPL_QC_CHECK_SRN_DETAIL.Unit_Code " & _
                    " LEFT OUTER JOIN TSPL_SRN_DETAIL ON TSPL_SRN_DETAIL.PO_Id=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No and TSPL_SRN_DETAIL.GRN_ID=TSPL_GRN_DETAIL.GRN_No and TSPL_SRN_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code and TSPL_SRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code" & _
                    " where PO.PurchaseOrder_No=max(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No) GROUP BY PO.PurchaseOrder_No) as postatus"
        If cbgVendor.CheckedValue.Count > 0 Then
            Vendor = ("'" + clsCommon.GetMulcallString(ArrVendor) + "'")
            StrVendor = Vendor.Replace("'", "")
        End If
        If cbgDocument.CheckedValue.Count > 0 Then
            DocNo = "'" + clsCommon.GetMulcallString(ArrDoc) + "'"
            StrDocNo = DocNo.Replace("'", "")
        End If
        If cbgLocation.CheckedValue.Count > 0 Then
            location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
            Strlocation = location.Replace("'", "")
        End If
        If cbgItem.CheckedValue.Count > 0 Then
            Item = "'" + clsCommon.GetMulcallString(arrItem) + "'"
            StrItem = Item.Replace("'", "")
        End If

        If Not rdobtnall.IsChecked Then
            If rdoPonever.IsChecked = True Then
                status = "Never"
            End If
            If rdoPOPartial.IsChecked = True Then
                status = "Partial "
            End If
            If rdoCompleted.IsChecked = True Then
                status = "Completed"
            End If
        End If
        'Dim Balance As String = ""
        'If is_Load_MRN = True Then
        '    Balance = "po_qty-grnqty"
        'Else
        '    Balance = "po_qty-srn_qty"
        'End If
        'If clsCommon.myLen(Balance) <= 0 Then
        '    Balance = "0"
        'End If
        If isLocation AndAlso arrLocation.Count = 1 Then
            Address = "(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(TSPL_TDS_STATE_MASTER .State_Name)> 0 then ',' else '' end  +(TSPL_TDS_STATE_MASTER .State_Name )) from tspl_location_master LEFT OUTER JOIN TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER .State =TSPL_TDS_STATE_MASTER .State_Code  where Location_Code =TSPL_PURCHASE_ORDER_HEAD .Bill_To_Location )"
        Else
            Address = "(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) "
        End If

        Dim strQuery As String = "select MAX(TSPL_PURCHASE_ORDER_HEAD.comp_code) AS comp_code,'" + FromDate + "' as  FromDate,'" + ToDate + "' as ToDate,'' as StrDocNo , " & _
        "'' as StrVendor, '' as Strlocation,'' as StrItem,'" + status + "' as status,MAX(TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_No) as purchase_no," & _
  " MAX(TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_Date) as po_date," + postatus + " , CONVERT(VARCHAR , MAX(TSPL_SRN_HEAD.SRN_Date) , 103)  AS SRN_Date," & _
    " MAX(TSPL_PURCHASE_ORDER_HEAD .Vendor_Code)  as vendor_no ,MAX(TSPL_PURCHASE_ORDER_HEAD .Vendor_Name)  as Vendor_name , MAX(TSPL_COMPANY_MASTER.Comp_Name) as compname, " & _
        "  (MAX(TSPL_COMPANY_MASTER.Add1) + case When MAX(TSPL_COMPANY_MASTER.Add2)='' Then '' else ', '+ Convert(Varchar,MAX(TSPL_COMPANY_MASTER.Add2), 103) End +  Case When MAX(TSPL_COMPANY_MASTER.Add3)='' Then '' Else ', '+ COnvert( Varchar,MAX(TSPL_COMPANY_MASTER.Add3),103) end +  case When MAX(TSPL_COMPANY_MASTER.City_Code) ='' then '' else ', '+ Convert(Varchar,MAX(TSPL_COMPANY_MASTER.City_Code), 103) end+  Case When " & _
  " MAX(TSPL_COMPANY_MASTER.State)='' Then '' else ', '+Convert(Varchar, MAX(TSPL_COMPANY_MASTER.State)) end +   Case When MAX(TSPL_COMPANY_MASTER.Pincode)='' Then '' Else ', '+ Convert(Varchar,MAX(TSPL_COMPANY_MASTER.Pincode), 103)  end)  as address1," & _
        "  MAX(TSPL_PURCHASE_ORDER_HEAD .delivery_date) as Delivaery_date,MAX(TSPL_PURCHASE_ORDER_DETAIL .Item_Cost) as po_rate , " & _
  " MAX(TSPL_PURCHASE_ORDER_DETAIL.item_code) as item_code,MAX(TSPL_PURCHASE_ORDER_DETAIL.item_desc) as itemdesc,max(TSPL_ITEM_MASTER.AllowSRNWithoutShortReject) as Allow_SRN," & _
  " MAX(TSPL_PURCHASE_ORDER_DETAIL.purchaseorder_qty) as po_qty, MAX(TSPL_PURCHASE_ORDER_DETAIL.unit_code) as uom ," & _
  " MAX(tspl_purchase_order_head.Bill_To_Location) as Location, (select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  =MAX(TSPL_PURCHASE_ORDER_HEAD .Bill_To_Location) ) as Location_Desc," & _
  "isnull(MAX(TSPL_GRN_DETAIL.GRN_NO),'') as GRN_NO ,CONVERT(VARCHAR , MAX(TSPL_GRN_HEAD.GRN_Date) , 103)   AS  GRN_Date , isnull(SUM(CASE WHEN TSPL_GRN_HEAD.IsCancel=0 THEN TSPL_GRN_DETAIL.GRN_Qty ELSE 0 END),0) " & _
  " as GRNQty,isnull(SUM(TSPL_GRN_DETAIL.Tolerence_Qty),0) AS Tolerence_Qty,ISNULL(SUM(TSPL_MRN_DETAIL.MRN_Qty),0)as  MRNQty, ISNULL(SUM(TSPL_SRN_DETAIL.SRN_Qty),0)as SRN_Qty,ISNULL(SUM(TSPL_SRN_DETAIL.Short_Qty),0)as Short_Qty,ISNULL(SUM(TSPL_SRN_DETAIL.Leak_Qty),0)as Leak_Qty,ISNULL(SUM(TSPL_SRN_DETAIL.Burst_Qty),0)as Burst_Qty,ISNULL(SUM(TSPL_SRN_DETAIL.Rejected_Qty),sum(coalesce(TSPL_QC_CHECK_SRN_DETAIL.Reject_Qty,0)))as Rejected_Qty,ISNULL(MAX(TSPL_SRN_DETAIL.Item_Cost) ,0)as SRN_Cost,MAX(TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code) as [MainGroupCode],   MAX(TSPL_ITEM_CATEGORY_LEVEL.description) as [Main Group],MAX(TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values) as [GroupCode], MAX(TSPL_ITEM_CATEGORY_LEVEL_VALUES.description) as [Group Name]   ,case when coalesce(( isnull(ISNULL(SUM(TSPL_SRN_DETAIL.SRN_Qty),0),0.0)),0)<=0 then  coalesce(( select (ISNULL(SUM(TSPL_MRN_DETAIL.MRN_Qty),0))  ) , ( isnull((isnull(SUM(TSPL_GRN_DETAIL.GRN_Qty),0)),0.0) )) end as QCQty,max(TSPL_PURCHASE_ORDER_HEAD.Category) as Category,case when max(isnull(TSPL_PURCHASE_ORDER_HEAD.Emergency,0))=0 then 'No' else 'Yes' end as Emergency,MAX(isnull(TSPL_PURCHASE_ORDER_HEAD.Capex_Code,'')) as [Capex Code],MAX(isnull(TSPL_CAPEX_MASTER.Description,'')) as [Capex Description],MAX(isnull(TSPL_PURCHASE_ORDER_HEAD.Capex_SubCode,'')) as [Capex Sub Code],MAX(isnull(TSPL_CAPEX_BUDGET_MASTER.Description,'')) as [Capex Sub Description] ,MAX(TSPL_PURCHASE_ORDER_DETAIL.purchaseorder_qty)-ISNULL(SUM(TSPL_SRN_DETAIL.SRN_Qty),0) as Shortage  " & _
" from (SELECT sum(TSPL_PURCHASE_ORDER_DETAIL.purchaseorder_qty) as purchaseorder_qty,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,TSPL_PURCHASE_ORDER_DETAIL.Item_Code,TSPL_PURCHASE_ORDER_DETAIL.Unit_code,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost,TSPL_PURCHASE_ORDER_DETAIL.Item_Desc from TSPL_PURCHASE_ORDER_DETAIL GROUP BY TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,TSPL_PURCHASE_ORDER_DETAIL.Item_Code,TSPL_PURCHASE_ORDER_DETAIL.Unit_code,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost,TSPL_PURCHASE_ORDER_DETAIL.Item_Desc ) as tspl_purchase_order_detail " & _
" left outer join TSPL_PURCHASE_ORDER_HEAD  on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No " & _
" left join TSPL_GRN_DETAIL on isnull(TSPL_GRN_DETAIL.PO_Id,'') =isnull(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,'') and TSPL_GRN_DETAIL.Item_Code " & _
" =tspl_purchase_order_detail.Item_Code  " & _
" left join TSPL_GRN_HEAD on isnull(TSPL_GRN_HEAD.grn_no,'')=isnull(TSPL_GRN_DETAIL.grn_no,'') " & _
" left join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL .Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code " & _
" and  isnull(TSPL_MRN_DETAIL .GRN_Id,'') =isnull(TSPL_GRN_DETAIL.grn_no,'') and isnull(TSPL_MRN_DETAIL.PO_Id,'')=isnull(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,'') and  " & _
" TSPL_MRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code " & _
" left join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL .Item_Code and isnull(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,'')= " & _
" isnull(TSPL_SRN_DETAIL.PO_ID ,'') " & _
" and isnull(TSPL_MRN_DETAIL.mrn_no,'')=isnull(TSPL_SRN_DETAIL.mrn_id,'') and isnull(TSPL_GRN_DETAIL.grn_no,'')=isnull(TSPL_MRN_DETAIL.grn_id,'') " & _
" left join TSPL_QC_CHECK_SRN_DETAIL on TSPL_MRN_DETAIL.MRN_No=TSPL_QC_CHECK_SRN_DETAIL.MRN_No  and  TSPL_MRN_DETAIL.Line_No=TSPL_QC_CHECK_SRN_DETAIL.Line_No   " & _
" left join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_no " & _
" left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_PURCHASE_ORDER_HEAD.comp_code " & _
" left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =tspl_purchase_order_detail.Item_Code " & _
"  LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.item_code=  TSPL_ITEM_MASTER.item_code " & _
"  LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL ON TSPL_ITEM_CATEGORY_LEVEL.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " & _
 " LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and " & _
 " TSPL_ITEM_CATEGORY_LEVEL_VALUES.code= TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values " & _
 " left outer join TSPL_REQUISITION_HEAD on isnull(TSPL_PURCHASE_ORDER_HEAD.Against_Requisition,'')=isnull(TSPL_REQUISITION_HEAD.Requisition_Id,'') " & _
 " LEFT OUTER JOIN TSPL_CAPEX_MASTER ON TSPL_CAPEX_MASTER.Code =TSPL_PURCHASE_ORDER_HEAD.Capex_Code" & _
 " LEFT OUTER JOIN TSPL_CAPEX_BUDGET_MASTER ON TSPL_CAPEX_BUDGET_MASTER.Code =TSPL_PURCHASE_ORDER_HEAD.Capex_SubCode"


        strQuery += " GROUP BY TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,TSPL_PURCHASE_ORDER_DETAIL.Item_Code,TSPL_PURCHASE_ORDER_DETAIL.Unit_code"
        If chk_groupby.Checked Then
            strQuery += " ,TSPL_GRN_HEAD.GRN_No"
        End If
        strQuery += " HAVING 2=2 "

        '====added by shivani
        If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
            strQuery += " and MAX(TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_No) in (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            strQuery += "  and MAX(tspl_purchase_order_head.Bill_To_Location) in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            strQuery += "  and MAX(TSPL_PURCHASE_ORDER_HEAD .Vendor_Code) in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            strQuery += "  and  MAX(TSPL_PURCHASE_ORDER_DETAIL.item_code) in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
        End If
        '==============
        '=====sanjeet=====================
        If clsCommon.CompairString(ddlCategory.SelectedValue, "Both") <> CompairStringResult.Equal Then
            strQuery += " and MAX(TSPL_PURCHASE_ORDER_HEAD.Category)='" & ddlCategory.SelectedValue & "'" + Environment.NewLine
        End If

        '=====================
        '------Ravi--------------
        Dim strWhrCatg As String = ""
        strWhrCatg = ""
        Dim isFirstTime As Boolean = True

        If rbtnCategorySelect.IsChecked Then
            Dim IsApplicable As Boolean = False
            strQuery += " and MAX(TSPL_PURCHASE_ORDER_DETAIL.item_code) in  (select Item_code  from TSPL_ITEM_MASTER_CATEGORY where Item_code in (select distinct item_code from TSPL_PURCHASE_ORDER_DETAIL) and ( " + Environment.NewLine

            For ii As Integer = 0 To gvCategory.RowCount - 1
                If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then

                    If Not isFirstTime Then
                        strQuery += " or "
                    End If

                    strQuery += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("Code").Value) + "' " + Environment.NewLine
                    Dim arr As Dictionary(Of String, Object) = gvCategory.Rows(ii).Tag
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        For Each strInn As String In arr.Keys
                            strQuery += " and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(strInn) + "' "
                        Next
                    End If ''tag arr cond.
                    strQuery += " ) "
                    isFirstTime = False
                End If
            Next
            strQuery += " ))"

        End If


        strQuery += " and convert(date,MAX(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date),103)>= convert(date,'" + FromDate + "',103) and convert(date,MAX(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date),103)<= convert(date,'" + ToDate + "',103) and MAX(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Type) <>  'J' "

        Dim qry12 As String = strQuery
        'Change by Prabhakar :
        strQuery = "select distinct  comp_code,Fromdate,Todate,strdocno,strvendor,strlocation,stritem,purchase_no,FORMAT(po_date,'dd/MM/yyyy') as po_date,postatus,GRN_NO, GRN_Date, vendor_no as vendor_no,vendor_name,compname,address1,delivaery_date,po_rate,item_code,itemdesc,convert(decimal(18,3),po_qty) as po_qty,((po_qty+Tolerence_Qty)-GRNQty +Short_Qty+Leak_Qty+Burst_Qty+Rejected_Qty) as [Balance Qty],uom,location,location_desc,convert(decimal(18,3),grnqty) as grnqty,convert(decimal(18,3),Tolerence_Qty) as Tolerence_Qty,convert(decimal(18,3),mrnqty) as mrnqty,convert(decimal(18,3),srn_qty) as srn_qty,convert(decimal(18,3),QCQty) as  QCQty,convert(decimal(18,3),Short_Qty) as Short_Qty,convert(decimal(18,3),Leak_Qty) as Leak_Qty,convert(decimal(18,3),Burst_Qty) as Burst_Qty,convert(decimal(18,3),Rejected_Qty) as Rejected_Qty,srn_cost ,status,Category,Emergency,[Capex Code],[Capex Description],[Capex Sub Code],[Capex Sub Description] ,convert(decimal(18,3),Shortage) as Shortage from (" + qry12 + ")ass1"

        qry12 = strQuery

        strQuery = "select TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,* from (" + qry12 + ")ax left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.comp_code=ax.comp_code where ax.comp_code='" + objCommonVar.CurrentCompanyCode + "' "

        If Not rdobtnall.IsChecked Then
            If rdoPonever.IsChecked = True Then
                'strQuery += " and len(isnull(GRN_NO,''))<=0 "
                strQuery += " and isnull(grnqty,0)=0 "
            End If
            If rdoPOPartial.IsChecked = True Then

                strQuery += " and [Balance Qty]<>0 and len(isnull(GRN_NO,''))>0 "
            End If
            If rdoCompleted.IsChecked = True Then
                strQuery += " and [Balance Qty] =0 "
            End If
        End If

        Dim strOrederCls As String = " order by Po_Date,purchase_no"
        Dim qry As String = strQuery + strOrederCls

        dtData = clsDBFuncationality.GetDataTable(qry)
        gv.DataSource = Nothing
        gv.Columns.Clear()
        gv.Rows.Clear()
        gv.GroupDescriptors.Clear()
        gv.MasterTemplate.SummaryRowsBottom.Clear()

        If dtData Is Nothing OrElse dtData.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        Else
            gv.DataSource = dtData
            SetGridFormatOFGV()
            ReStoreGridLayout()
            RadPageView1.SelectedPage = RadPageViewPage2
            RadGroupBox7.Enabled = False
        End If
    End Sub

    
    Sub refereshdata1()

        If chkDoc_select.IsChecked AndAlso cbgDocument.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Documnet Number", Me.Text)
            Return

        End If
        If chkVendor_select.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Vendor", Me.Text)
            Return
        End If
        If chkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please selete one Item", Me.Text)
            Return
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select one location ", Me.Text)
            Return

        End If
        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy")
        Dim todate As String = clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")

        ReferehData(fromdate, todate, chkDoc_select.IsChecked, cbgDocument.CheckedValue, chkVendor_select.IsChecked, cbgVendor.CheckedValue, chkItemSelect.IsChecked, cbgItem.CheckedValue, chkLocationSelect.IsChecked, cbgLocation.CheckedValue)
    End Sub
    Sub SetGridFormatOFGV()
        'Dim strItemCode, head2 As String

        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next
        gv.Columns("item_code").IsVisible = True
        gv.Columns("item_code").Width = 120
        gv.Columns("item_code").HeaderText = "Item"

        gv.Columns("itemdesc").IsVisible = True
        gv.Columns("itemdesc").Width = 120
        gv.Columns("itemdesc").HeaderText = "Item Description"

        gv.Columns("delivaery_date").IsVisible = True
        gv.Columns("delivaery_date").Width = 120
        gv.Columns("delivaery_date").HeaderText = "Delivery Date"

        gv.Columns("uom").IsVisible = True
        gv.Columns("uom").Width = 120
        gv.Columns("uom").HeaderText = "Unit"


        gv.Columns("po_rate").IsVisible = True
        gv.Columns("po_rate").Width = 120
        gv.Columns("po_rate").HeaderText = "Rate"

        gv.Columns("po_qty").IsVisible = True
        gv.Columns("po_qty").Width = 120
        gv.Columns("po_qty").HeaderText = "PO Qty"

        gv.Columns("srn_qty").IsVisible = True
        gv.Columns("srn_qty").Width = 120
        gv.Columns("srn_qty").HeaderText = "SRN QTY"

        gv.Columns("short_qty").IsVisible = True
        gv.Columns("short_qty").Width = 120
        gv.Columns("short_qty").HeaderText = "Short QTY"

        gv.Columns("Leak_qty").IsVisible = True
        gv.Columns("Leak_qty").Width = 120
        gv.Columns("Leak_qty").HeaderText = "Leak QTY"


        gv.Columns("Shortage").IsVisible = True
        gv.Columns("Shortage").Width = 120
        gv.Columns("Shortage").HeaderText = "Shortage"

        gv.Columns("burst_qty").IsVisible = True
        gv.Columns("burst_qty").Width = 120
        gv.Columns("burst_qty").HeaderText = "Burst QTY"

        gv.Columns("Rejected_qty").IsVisible = True
        gv.Columns("Rejected_qty").Width = 120
        gv.Columns("Rejected_qty").HeaderText = "Rejected QTY"

        gv.Columns("Tolerence_Qty").IsVisible = True
        gv.Columns("Tolerence_Qty").Width = 120
        gv.Columns("Tolerence_Qty").HeaderText = "Tolerence QTY"

        If chk_groupby.Checked Then
            gv.Columns("Balance Qty").IsVisible = False
            gv.Columns("Balance Qty").VisibleInColumnChooser = False
            gv.Columns("Balance Qty").Width = 120
            gv.Columns("Balance Qty").HeaderText = "Balance Qty"
        Else
            gv.Columns("Balance Qty").IsVisible = True
            gv.Columns("Balance Qty").Width = 120
            gv.Columns("Balance Qty").HeaderText = "Balance Qty"
        End If

        gv.Columns("status").IsVisible = True
        gv.Columns("status").Width = 120
        gv.Columns("status").HeaderText = "Status"

        gv.Columns("postatus").IsVisible = True
        gv.Columns("postatus").Width = 120
        gv.Columns("postatus").HeaderText = "PO Status"

        gv.Columns("Category").IsVisible = True
        gv.Columns("Category").Width = 120
        gv.Columns("Category").HeaderText = "Category"

        gv.Columns("Emergency").IsVisible = True
        gv.Columns("Emergency").Width = 120
        gv.Columns("Emergency").HeaderText = "Emergency"

        gv.Columns("Capex Code").IsVisible = True
        gv.Columns("Capex Code").Width = 120
        gv.Columns("Capex Code").HeaderText = "Capex Code"

        gv.Columns("Capex Description").IsVisible = True
        gv.Columns("Capex Description").Width = 120
        gv.Columns("Capex Description").HeaderText = "Capex Description"

        gv.Columns("Capex Sub Code").IsVisible = True
        gv.Columns("Capex Sub Code").Width = 120
        gv.Columns("Capex Sub Code").HeaderText = "Capex Sub Code"

        gv.Columns("Capex Sub Description").IsVisible = True
        gv.Columns("Capex Sub Description").Width = 120
        gv.Columns("Capex Sub Description").HeaderText = "Capex Sub Description"

        'gv.Columns("QCQty").IsVisible = True
        'gv.Columns("QCQty").Width = 120
        'gv.Columns("QCQty").HeaderText = "QC Qty"


        'gv.Columns("SRN_No").IsVisible = True
        'gv.Columns("SRN_No").Width = 120
        'gv.Columns("SRN_No").HeaderText = "SRN No"

        'gv.Columns("SRN_Date").IsVisible = True
        'gv.Columns("SRN_Date").Width = 120
        'gv.Columns("SRN_Date").HeaderText = "SRN_Date"



        gv.Columns("Logo_Img").IsVisible = False
        gv.Columns("Logo_Img").Width = 200
        gv.Columns("Logo_Img").HeaderText = "Logo_Img"


        gv.Columns("Logo_Img2").IsVisible = False
        gv.Columns("Logo_Img2").Width = 200
        gv.Columns("Logo_Img2").HeaderText = "Logo_Img2"

        gv.Columns("comp_code").IsVisible = False
        gv.Columns("comp_code").Width = 70
        gv.Columns("comp_code").HeaderText = "comp_code"

        gv.Columns("Fromdate").IsVisible = False
        gv.Columns("Fromdate").Width = 100
        gv.Columns("Fromdate").HeaderText = "Fromdate"

        gv.Columns("Todate").IsVisible = False
        gv.Columns("Todate").Width = 100
        gv.Columns("Todate").HeaderText = "Todate"

        gv.Columns("strdocno").IsVisible = False
        gv.Columns("strdocno").Width = 100
        gv.Columns("strdocno").HeaderText = "strdocno"

        gv.Columns("strvendor").IsVisible = False
        gv.Columns("strvendor").Width = 120
        gv.Columns("strvendor").HeaderText = "strvendor"

        gv.Columns("strlocation").IsVisible = False
        gv.Columns("strlocation").Width = 120
        gv.Columns("strlocation").HeaderText = "strlocation"

        gv.Columns("stritem").IsVisible = False
        gv.Columns("stritem").Width = 120
        gv.Columns("stritem").HeaderText = "stritem"

        gv.Columns("purchase_no").IsVisible = True
        gv.Columns("purchase_no").Width = 120
        gv.Columns("purchase_no").HeaderText = "PO No"

        gv.Columns("po_date").IsVisible = True
        gv.Columns("po_date").Width = 120
        gv.Columns("po_date").HeaderText = "PO Date"

        gv.Columns("vendor_no").IsVisible = True
        gv.Columns("vendor_no").Width = 120
        gv.Columns("vendor_no").HeaderText = "Vendor No"

        gv.Columns("vendor_name").IsVisible = True
        gv.Columns("vendor_name").Width = 120
        gv.Columns("vendor_name").HeaderText = "Vendor Name"

        gv.Columns("compname").IsVisible = False
        gv.Columns("compname").Width = 120
        gv.Columns("compname").HeaderText = "compname"

        gv.Columns("address1").IsVisible = False
        gv.Columns("address1").Width = 120
        gv.Columns("address1").HeaderText = "address1"

        gv.Columns("location").IsVisible = False
        gv.Columns("location").Width = 120
        gv.Columns("location").HeaderText = "location"

        gv.Columns("location_desc").IsVisible = False
        gv.Columns("location_desc").Width = 120
        gv.Columns("location_desc").HeaderText = "location_desc"

        gv.Columns("grnqty").IsVisible = True
        gv.Columns("grnqty").Width = 120
        gv.Columns("grnqty").HeaderText = "grnqty"

        gv.Columns("mrnqty").IsVisible = True
        gv.Columns("mrnqty").Width = 120
        gv.Columns("mrnqty").HeaderText = "mrnqty"



        gv.Columns("srn_cost").IsVisible = False
        gv.Columns("srn_cost").Width = 120
        gv.Columns("srn_cost").HeaderText = "srn_cost"

        gv.Columns("City_Code").IsVisible = False
        gv.Columns("City_Code").Width = 120
        gv.Columns("City_Code").HeaderText = "City_Code"

        gv.Columns("Pincode").IsVisible = False
        gv.Columns("Pincode").Width = 120
        gv.Columns("Pincode").HeaderText = "Pincode"

        gv.Columns("state").IsVisible = False
        gv.Columns("state").Width = 120
        gv.Columns("state").HeaderText = "state"

        gv.Columns("GRN_NO").IsVisible = True
        gv.Columns("GRN_NO").Width = 120
        gv.Columns("GRN_NO").HeaderText = "GRN_NO"

        gv.Columns("GRN_Date").IsVisible = True
        gv.Columns("GRN_Date").Width = 120
        gv.Columns("GRN_Date").HeaderText = "GRN_Date"

        'gv.Columns("Against_Requisition").IsVisible = True
        'gv.Columns("Against_Requisition").Width = 120
        'gv.Columns("Against_Requisition").HeaderText = "Requisition_No"

        'gv.Columns("Requisition_Date").IsVisible = True
        'gv.Columns("Requisition_Date").Width = 120
        'gv.Columns("Requisition_Date").HeaderText = "Requisition_Date"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        If Not chk_groupby.Checked Then
            Dim item1 As New GridViewSummaryItem("po_qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
        End If
        
        Dim item2 As New GridViewSummaryItem("srn_qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Balance Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("mrnqty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("grnqty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("Tolerence_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        If chk_groupby.Checked Then
            gv.GroupDescriptors.Add(New GridGroupByExpression("purchase_no as Item format ""{0}: {1}"" Group By purchase_no"))
        End If

        gv.ShowGroupPanel = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


    End Sub
    Private Sub btnReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferesh.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        refereshdata1()
    End Sub

    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        Try
            If e.RowIndex >= 0 Then

                If e.Column.Name = "purchase_no" Then
                    If clsCommon.myLen(gv.CurrentRow.Cells("purchase_no").Value) >= 0 Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseOrder, gv.CurrentRow.Cells("purchase_no").Value)
                    End If
                End If

                If e.Column.Name = "SRN_NO" Then
                    If clsCommon.myLen(gv.CurrentRow.Cells("SRN_No").Value) >= 0 Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, gv.CurrentRow.Cells("SRN_No").Value)
                    End If
                End If

                If e.Column.Name = "GRN_NO" Then
                    If clsCommon.myLen(gv.CurrentRow.Cells("GRN_NO").Value) >= 0 Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGRN, gv.CurrentRow.Cells("GRN_NO").Value)
                    End If
                End If
                ' Against_Requisition

                If e.Column.Name = "Against_Requisition" Then
                    If clsCommon.myLen(gv.CurrentRow.Cells("Against_Requisition").Value) >= 0 Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseRequistion, gv.CurrentRow.Cells("Against_Requisition").Value)
                    End If
                End If

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(e, ex.Message, Me.Text)
        End Try
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptPendingPO & "'"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")) + " ")

            If txtDocNo.arrDispalyMember IsNot Nothing AndAlso txtDocNo.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Document No : " + clsCommon.GetMulcallStringWithComma(txtDocNo.arrDispalyMember))
            End If

            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtVendor.arrDispalyMember IsNot Nothing AndAlso txtVendor.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If
            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
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
                clsCommon.MyExportToPDF("Purchase Order Pending Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Sub LoadDocuemntNo()
        Dim qry As String = "select purchaseorder_no as Code,description as [Description] from TSPL_PURCHASE_ORDER_HEAD"
        cbgDocument.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDocument.ValueMember = "Code"
        cbgDocument.DisplayMember = "Invoice_Entry_Date"


    End Sub
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER WHERE Status='N' order by Vendor_Code"
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Vendor_Code"
        cbgVendor.DisplayMember = "Vendor_Name"
    End Sub
    Public Sub ItemLoad()
        Dim qry As String = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER  "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Code"
    End Sub
    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
    End Sub

    Private Sub txtDocNo__My_Click(sender As Object, e As EventArgs) Handles txtDocNo._My_Click
        Dim qry As String = "select purchaseorder_no as Code,description as Name from TSPL_PURCHASE_ORDER_HEAD where   convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)>= convert(date,'" + dtpfromdate.Value + "',103) and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)<= convert(date,'" + dtpTodate.Value + "',103) and PurchaseOrder_Type <>  'J' "
        txtDocNo.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtDocNo.arrValueMember, txtDocNo.arrDispalyMember)
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER Where Status='N' order by Vendor_Code"
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnQuickExport.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptPendingPO & "'"))


            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If Not IsNothing(txtItem.arrValueMember) Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
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

    Private Sub MyRadioButton2_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        gvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
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

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
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
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
End Class
