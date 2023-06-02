Imports System.Data.SqlClient
Imports common
Imports System.IO
Public Class RptUnpostingTransItemQty

    Inherits FrmMainTranScreen

#Region "Varables"
    Public qry As String = ""
    'Public ReportID As String = "COmmitedQty"
    'Public strUOM As String = ""
    'Public IsMRPMandatory As Boolean = False
    'Public _METEXT As String = "Item Commited Quantity Report"
#End Region

    'Private Sub txtLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
    '    txtLocationCode.Value = clsCommon.ShowSelectForm("LOCATION@CODE", qry, "Code", "", txtLocationCode.Value, "Code", isButtonClicked)
    '    txtLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocationCode.Value + "'"))
    'End Sub

    'Private Sub txtItemCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    Dim qry As String = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER  "
    '    txtItemCode.Value = clsCommon.ShowSelectForm("Item@CODE", qry, "Code", "", txtItemCode.Value, "Code", isButtonClicked)
    '    lbltemName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where item_Code='" + txtItemCode.Value + "'"))
    'End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        'If clsCommon.myLen(txtItemCode.Value) <= 0 Then
        '    clsCommon.MyMessageBoxShow("First Seclet Item.", Me.Text)
        '    Return
        'End If
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        ' Me.Text = _METEXT
        qry = " select TransType,TransCode,DocNo,DocDate,ICode,Locaion, tspl_Location_master.Location_Desc,Qty  as Qty ,UOM,coalesce(TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader,FinalQry.VSP) as [VLC Uploader Code],FinalQry.Invoice_No as [Invoice No]  from (" + getBaseQryForOther() + ")FinalQry left outer join tspl_Location_master on tspl_Location_master.location_Code =FinalQry.Locaion left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=FinalQry.VSP  where TransType<> ''"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv1.DataSource = Nothing
            gv1.DataSource = dt
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = True
                gv1.Columns(ii).Width = 100
            Next
            'lblUOM.Text = strUOM

            gv1.Columns("TransType").Width = 150
            gv1.Columns("TransType").IsVisible = False
            gv1.Columns("TransType").HeaderText = "Transaction Type"

            gv1.Columns("ICode").IsVisible = True
            gv1.Columns("ICode").Width = 100
            gv1.Columns("ICode").HeaderText = "Item"

            gv1.Columns("Locaion").IsVisible = True
            gv1.Columns("Locaion").Width = 150
            gv1.Columns("Locaion").HeaderText = "Location"

            gv1.Columns("Location_Desc").IsVisible = True
            gv1.Columns("Location_Desc").Width = 150
            gv1.Columns("Location_Desc").HeaderText = "Location Desc"

            gv1.Columns("DocNo").IsVisible = True
            gv1.Columns("DocNo").Width = 150
            gv1.Columns("DocNo").HeaderText = "Document No"

            gv1.Columns("DocDate").IsVisible = True
            gv1.Columns("DocDate").Width = 150
            gv1.Columns("DocDate").HeaderText = "Document Date"


            gv1.Columns("Qty").IsVisible = True
            gv1.Columns("Qty").Width = 150
            gv1.Columns("Qty").HeaderText = "Quantity"
            gv1.Columns("Qty").FormatString = "{0:n2}"

            gv1.Columns("VLC Uploader Code").IsVisible = True
            gv1.Columns("VLC Uploader Code").Width = 150
            gv1.Columns("VLC Uploader Code").HeaderText = "VLC Uploader Code"

            gv1.Columns("Invoice No").IsVisible = True
            gv1.Columns("Invoice No").Width = 150

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            gv1.ShowGroupPanel = False
            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = False
            gv1.AllowRowReorder = False
            gv1.EnableSorting = False
            gv1.ShowFilteringRow = True
            gv1.EnableFiltering = True
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.MasterTemplate.ShowRowHeaderColumn = False

            ReStoreGridLayout()
            'txtItemCode.Enabled = False
            'txtLocationCode.Enabled = False
            fndItemCode.Enabled = False
            fndLocation.Enabled = False
            fromDate.Enabled = False
            ToDate.Enabled = False
            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data found to dispaly", Me.Text)
            ' Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub ReStoreGridLayout()

        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btnclose1_Click(sender As Object, e As EventArgs) Handles btnclose1.Click
        Me.Close()
        
    End Sub

    'Public Function getBaseQryForSales() As String
    '    Dim qry As String = Nothing
    '    If clsCommon.myLen(txtItemCode.Value) > 0 Then


    '        qry = "select 'Purchase Order' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.mbtnPurchaseOrder) + "' as TransCode,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No  as DocNo, TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_PURCHASE_ORDER_DETAIL.Location,((PurchaseOrder_Qty* case when TSPL_ITEM_UOM_DETAIL.Conversion_Factor=0 then 1 else TSPL_ITEM_UOM_DETAIL.Conversion_Factor end)/(case when FinalConvert.Conversion_Factor=0 then 1 else FinalConvert.Conversion_Factor end) ) as Qty from TSPL_PURCHASE_ORDER_DETAIL "
    '        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_PURCHASE_ORDER_DETAIL.Unit_code"
    '        qry += " left outer join TSPL_ITEM_UOM_DETAIL as FinalConvert on FinalConvert.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code  and  FinalConvert.UOM_Code =TSPL_PURCHASE_ORDER_DETAIL.Unit_code "
    '        qry += " where TSPL_PURCHASE_ORDER_DETAIL.Item_Code='" + txtItemCode.Value + "'"
    '        If clsCommon.myLen(txtLocationCode.Value) > 0 Then
    '            qry += " and TSPL_PURCHASE_ORDER_DETAIL.Location='" + txtLocationCode.Value + "' "
    '        End If
    '        qry += " and not exists (select 1 from TSPL_SRN_DETAIL where TSPL_SRN_DETAIL.PO_ID=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No and TSPL_SRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code)"

    '        qry += " Union All "
    '        qry += "select 'Sale Order' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmSNSalesOrder) + "' as TransCode,TSPL_SD_SALES_ORDER_DETAIL.Document_Code as DocNo, TSPL_SD_SALES_ORDER_DETAIL.Item_Code as ICode,TSPL_SD_SALES_ORDER_DETAIL.Location,(  (Qty*case when TSPL_ITEM_UOM_DETAIL.Conversion_Factor=0 then 1 else TSPL_ITEM_UOM_DETAIL.Conversion_Factor end)/(case when FinalConvert.Conversion_Factor=0 then 1 else FinalConvert.Conversion_Factor end)  ) as Qty from TSPL_SD_SALES_ORDER_DETAIL "
    '        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALES_ORDER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_SALES_ORDER_DETAIL.Unit_code"
    '        qry += " left outer join TSPL_ITEM_UOM_DETAIL as FinalConvert on FinalConvert.Item_Code=TSPL_SD_SALES_ORDER_DETAIL.Item_Code  and  FinalConvert.UOM_Code =TSPL_SD_SALES_ORDER_DETAIL.Unit_code  "
    '        qry += " where TSPL_SD_SALES_ORDER_DETAIL.Item_Code='" + txtItemCode.Value + "'"
    '        If clsCommon.myLen(txtLocationCode.Value) > 0 Then
    '            qry += " and TSPL_SD_SALES_ORDER_DETAIL.Location='" + txtLocationCode.Value + "'"
    '        End If
    '        qry += " and not exists (select 1 from TSPL_SD_SHIPMENT_DETAIL where TSPL_SD_SHIPMENT_DETAIL.Order_Code=TSPL_SD_SALES_ORDER_DETAIL.Document_Code and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_SD_SALES_ORDER_DETAIL.Item_Code)"

    '        qry += " Union All "
    '        qry += "select 'CSA' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmCSASaleInvoice) + "' as TransCode,TSPL_CSA_DO_DETAIL.Doc_No  as DocNo, TSPL_CSA_DO_DETAIL.Item_Code as ICode,TSPL_CSA_DO_HEAD.from_Location_code as Location,(  (Qty*case when TSPL_ITEM_UOM_DETAIL.Conversion_Factor=0 then 1 else TSPL_ITEM_UOM_DETAIL.Conversion_Factor end)/(case when FinalConvert.Conversion_Factor=0 then 1 else FinalConvert.Conversion_Factor end)  ) as Qty from TSPL_CSA_DO_DETAIL "
    '        qry += " left join TSPL_CSA_DO_HEAD on TSPL_CSA_DO_HEAD.Doc_No = TSPL_CSA_DO_DETAIL.Doc_No "
    '        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_CSA_DO_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_CSA_DO_DETAIL.UOM"
    '        qry += " left outer join TSPL_ITEM_UOM_DETAIL as FinalConvert on FinalConvert.Item_Code=TSPL_CSA_DO_DETAIL.Item_Code  and  FinalConvert.UOM_Code =TSPL_CSA_DO_DETAIL.UOM "
    '        qry += " where TSPL_CSA_DO_DETAIL.Item_Code='" + _txtItemCode.Value + "'"
    '        If clsCommon.myLen(_txtLocationCode.Value) > 0 Then
    '            qry += " and TSPL_CSA_DO_DETAIL.doc_no in (select doc_no from TSPL_CSA_DO_HEAD where from_Location_code='" + txtLocationCode.Value + "' and doc_no<>'')"
    '        End If
    '    End If

    '    Return qry
    'End Function

    Public Function getBaseQryForOther() As String
        ' Dim qry As String = clsItemLocationDetails.getBaseQryForItemBalanceDuringTransaction(txtItemCode.Value, "", txtLocationCode.Value, Nothing, Nothing, False, False, Nothing)
        ' Dim qry As String = getBaseQryForItemBalanceDuringTransaction(txtItemCode.Value, "", txtLocationCode.Value, Nothing, Nothing, False, False, Nothing)
        Dim qry As String = getBaseQryForItemBalanceDuringTransaction("", "", "", Nothing, Nothing, False, False, Nothing)
        Return qry
    End Function
    ' Ticket No : TEC/07/02/19-000418 by prabhakar for Reset
    Private Sub btnreset1_Click(sender As Object, e As EventArgs) Handles btnreset1.Click
        Reset()
    End Sub
    Sub Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        'txtItemCode.Enabled = True
        'txtLocationCode.Enabled = True
        fndItemCode.Enabled = True
        fndLocation.Enabled = True
        fromDate.Enabled = True
        ToDate.Enabled = True
        gv1.DataSource = Nothing
        fromDate.Checked = False
        ToDate.Checked = False
        fndItemCode.arrValueMember = Nothing
        fndLocation.arrValueMember = Nothing
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Public Function getBaseQryForItemBalanceDuringTransaction(ByVal _ICode As String, ByVal _UOM As String, ByVal _LCode As String, ByVal _TransDate As Date, ByVal _TransNo As String, ByVal _IsMRPMandatory As Boolean, ByVal _MRP As Decimal, ByVal trans As SqlTransaction) As String
        Dim IsItemWithDifferntUnitConsiderAsOtherItem As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsItemWithDifferntUnitConsiderAsOtherItem, clsFixedParameterCode.IsItemWithDifferntUnitConsiderAsOtherItem, trans)) > 0, True, False)
        Dim qry As String = "select  xx.TransType,xx.TransCode,xx.DocNo,xx.DocDate ,xx.ICode, xx.Locaion ,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,xx.Qty as Qty  ,xx.UOM ,xx.VSP,xx.Invoice_No" + Environment.NewLine  ' ( (xx.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) as Qty
        qry += " from (" + Environment.NewLine
        '' ''qry += " select '' as TransType,'' as TransCode,'' as DocNo, Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM  from("
        '' ''qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew from("
        '' ''qry += " select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,TSPL_INVENTORY_MOVEMENT.Stock_Qty as Qty   ,TSPL_INVENTORY_MOVEMENT.Stock_UOM as UOMNew "
        '' ''qry += " from TSPL_INVENTORY_MOVEMENT "
        '' ''qry += " where TSPL_INVENTORY_MOVEMENT.Qty<>0 and TSPL_INVENTORY_MOVEMENT.Item_Code='" + _ICode + "'"
        '' ''If clsCommon.myLen(_LCode) > 0 Then
        '' ''    qry += "  and Location_Code='" + _LCode + "'"
        '' ''End If
        'If _IsMRPMandatory AndAlso _MRP > 0 Then
        '    qry += " and TSPL_INVENTORY_MOVEMENT.MRP='" + clsCommon.myCstr(_MRP) + "' "
        'End If

        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_INVENTORY_MOVEMENT.UOM='" + _UOM + "' "
        'End If

        'Dim intSettingType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, trans))

        'If intSettingType = 1 Then
        '    qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
        'ElseIf intSettingType = 0 Then
        '    qry += " and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "'"
        'End If
        '' ''qry += " )xxx  "
        '' ''qry += " )xxxx group by Item_Code,Location_Code,UOMNew "

        '' ''qry += " union all " + Environment.NewLine
        '' ''qry += " select '' as TransType,'' as TransCode,'' as DocNo, Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM  from("
        '' ''qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew from("
        '' ''qry += " select TSPL_inventory_Movement_New.Trans_Id, TSPL_inventory_Movement_New.Item_Code ,TSPL_inventory_Movement_New.Location_Code , TSPL_inventory_Movement_New.InOut,TSPL_inventory_Movement_New.Stock_Qty as Qty   ,TSPL_inventory_Movement_New.Stock_UOM as UOMNew "
        '' ''qry += " from TSPL_inventory_Movement_New "
        '' ''qry += " where TSPL_inventory_Movement_New.Qty<>0 and TSPL_inventory_Movement_New.Item_Code='" + _ICode + "'"
        '' ''If clsCommon.myLen(_LCode) > 0 Then
        '' ''    qry += "  and Location_Code='" + _LCode + "'"
        '' ''End If
        'If _IsMRPMandatory AndAlso _MRP > 0 Then
        '    qry += " and TSPL_inventory_Movement_New.MRP='" + clsCommon.myCstr(_MRP) + "' "
        'End If

        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_inventory_Movement_New.UOM='" + _UOM + "' "
        'End If

        'If intSettingType = 1 Then
        '    qry += " and 2=(case when TSPL_inventory_Movement_New.InOut='O' then 2 else case when TSPL_inventory_Movement_New.InOut='I' and TSPL_inventory_Movement_New.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
        'ElseIf intSettingType = 0 Then
        '    qry += " and TSPL_inventory_Movement_New.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "'"
        'End If
        '' ''qry += " )xxx  "
        '' ''qry += " )xxxx group by Item_Code,Location_Code,UOMNew "

        '' ''qry += " union all " + Environment.NewLine

        qry += " select 'Purchase Return' as TransType,'Purchase Return' as TransCode,TSPL_PR_HEAD.PR_No as DocNo,Convert (varchar,TSPL_PR_HEAD.PR_Date,103) as DocDate ,TSPL_PR_DETAIL.Item_Code as ICode,case when TSPL_PR_HEAD.is_Reject_Item=1 then TSPL_LOCATION_MASTER.Rejected_Location else  TSPL_PR_DETAIL.Location end as Locaion,TSPL_PR_DETAIL.PR_Qty as Qty,-1 as RI,TSPL_PR_DETAIL.Unit_code AS Uom "
        qry += " ,TSPL_PR_HEAD.Vendor_Code as VSP,'' as Invoice_No from TSPL_PR_DETAIL "
        qry += " left outer join TSPL_PR_HEAD on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No"
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PR_HEAD.Bill_To_Location "
        qry += " where TSPL_PR_HEAD.Status=0  "

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_PR_HEAD.PR_Date ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_PR_HEAD.PR_Date ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If
        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_PR_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and (case when TSPL_PR_HEAD.is_Reject_Item=1 then TSPL_LOCATION_MASTER.Rejected_Location else  TSPL_PR_DETAIL.Location end) in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ")"
        End If
        'If _IsMRPMandatory AndAlso _MRP > 0 Then
        '    qry += " and TSPL_PR_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
        'End If
        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_PR_DETAIL.Unit_code='" + _UOM + "' "
        'End If
        qry += " and TSPL_PR_DETAIL.PR_Qty<>0  "
        'qry += " and TSPL_PR_DETAIL.PR_No not in ('" + _TransNo + "')"

        qry += " union all " + Environment.NewLine

        qry += " select 'Store_Adjustment' as TransType,'Store_Adjustment' as TransCode,TSPL_ADJUSTMENT_HEADER.Adjustment_No as DocNo,convert( varchar,TSPL_ADJUSTMENT_HEADER. ADJUSTMENT_DATE,103) as DocDate  ,TSPL_ADJUSTMENT_DETAIL.Item_Code as ICode,TSPL_ADJUSTMENT_HEADER.Loc_Code as Locaion,TSPL_ADJUSTMENT_DETAIL.Item_Quantity as Qty,-1 as RI,TSPL_ADJUSTMENT_DETAIL.Unit_Code AS Uom "
        qry += " ,'' as VSP,'' as Invoice_No from TSPL_ADJUSTMENT_DETAIL "
        qry += " left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No"
        qry += " where TSPL_ADJUSTMENT_HEADER.Posted='N' and Trans_Type = 'Out'    "
        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_ADJUSTMENT_HEADER. ADJUSTMENT_DATE ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_ADJUSTMENT_HEADER. ADJUSTMENT_DATE ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If
        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_ADJUSTMENT_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_ADJUSTMENT_HEADER.Loc_Code in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If
        'If _IsMRPMandatory AndAlso _MRP > 0 Then
        '    qry += " and TSPL_ADJUSTMENT_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
        'End If
        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_ADJUSTMENT_DETAIL.Unit_Code='" + _UOM + "' "
        'End If
        qry += " and TSPL_ADJUSTMENT_DETAIL.Item_Quantity<>0  and TSPL_ADJUSTMENT_DETAIL.Adjustment_Type in ('BD','QD') " 'and TSPL_ADJUSTMENT_HEADER.Adjustment_No not in ('" + _TransNo + "')"

        qry += " union all " + Environment.NewLine

        qry += " select 'RGP' as TransType,'RGP' as TransCode,TSPL_RGP_HEAD.RGP_No as DocNo,convert( varchar, TSPL_RGP_HEAD.RGP_Date,103) as DocDate , TSPL_RGP_DETAIL.Item_Code as ICode,TSPL_RGP_HEAD.Location as Locaion,TSPL_RGP_DETAIL.RGP_Qty as Qty,-1 as RI,TSPL_RGP_DETAIL.Unit_code AS Uom "
        qry += " ,TSPL_RGP_HEAD.Vendor_Code as VSP,'' as Invoice_No from TSPL_RGP_DETAIL "
        qry += " left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No"
        qry += " where TSPL_RGP_HEAD.Status=0 "

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_RGP_HEAD.RGP_Date ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_RGP_HEAD.RGP_Date ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_RGP_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and  TSPL_RGP_HEAD.Location in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If

        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_RGP_DETAIL.Unit_code='" + _UOM + "' "
        'End If
        qry += " and TSPL_RGP_DETAIL.RGP_Qty<>0  "
        'qry += " and TSPL_RGP_DETAIL.RGP_No not in ('" + _TransNo + "')"

        qry += " union all " + Environment.NewLine

        qry += " select 'Scrap Sale' as TransType,'Scrap Sale' as TransCode,TSPL_SCRAPSALE_HEAD.shipment_No as DocNo, convert( varchar,TSPL_SCRAPSALE_HEAD.shipment_Date,103) as DocDate,TSPL_SCRAPSALE_DETAIL.Item_Code as ICode,TSPL_SCRAPSALE_HEAD.Loc_Code as Locaion,TSPL_SCRAPSALE_DETAIL.shipped_Qty as Qty,-1 as RI,TSPL_SCRAPSALE_DETAIL.Unit_code AS Uom "
        qry += " ,TSPL_SCRAPSALE_HEAD.cust_Code as VSP,'' as Invoice_No from TSPL_SCRAPSALE_DETAIL "
        qry += " left outer join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No"
        qry += " where TSPL_SCRAPSALE_HEAD.IsPost=0 and  TSPL_SCRAPSALE_HEAD.Doc_Type = 'S' "

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If
        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_SCRAPSALE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_SCRAPSALE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If

        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_SCRAPSALE_DETAIL.Unit_code='" + _UOM + "' "
        'End If
        qry += " and TSPL_SCRAPSALE_DETAIL.shipped_Qty<>0 " ' and TSPL_SCRAPSALE_DETAIL.shipment_No not in ('" + _TransNo + "')"

        qry += "  union all " + Environment.NewLine

        qry += " select 'Issue/Return/Transfer' as TransType,'Issue/Return/Transfer' as TransCode,TSPL_IssueReturn_HEAD.Doc_No as DocNo,convert( varchar,TSPL_IssueReturn_HEAD.DOC_DATE,103) as DocDate, TSPL_IssueReturn_DETAIL.Item_Code as ICode,TSPL_IssueReturn_HEAD.From_Location as Locaion,TSPL_IssueReturn_DETAIL.Issued_Qty as Qty,-1 as RI,TSPL_IssueReturn_DETAIL.Unit_code AS Uom "
        qry += " ,'' as VSP,'' as Invoice_No from TSPL_IssueReturn_DETAIL "
        qry += " left outer join TSPL_IssueReturn_HEAD on TSPL_IssueReturn_HEAD.Doc_No=TSPL_IssueReturn_DETAIL.Doc_No"
        qry += " where TSPL_IssueReturn_HEAD.Status=0 and TSPL_IssueReturn_HEAD.Doc_Type = 'Issue'  "

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_IssueReturn_HEAD.DOC_DATE ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_IssueReturn_HEAD.DOC_DATE ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If
        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_IssueReturn_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_IssueReturn_HEAD.From_Location in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If

        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_IssueReturn_DETAIL.Unit_code='" + _UOM + "' "
        'End If
        qry += " and TSPL_IssueReturn_DETAIL.Issued_Qty<>0 " 'and TSPL_IssueReturn_DETAIL.Doc_No not in ('" + _TransNo + "') " + Environment.NewLine

        qry += "  union all " + Environment.NewLine
        qry += "  select  'SaleOrder' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmSNSalesOrder) + "' as TransCode,TSPL_SD_SALES_ORDER_HEAD.Document_Code as DocNo,convert( varchar,TSPL_SD_SALES_ORDER_HEAD.Document_Date,103) as DocDate, TSPL_SD_SALES_ORDER_DETAIL.Item_Code as ICode,TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location as Locaion,(TSPL_SD_SALES_ORDER_DETAIL.CommitedQty)-isnull(TSPL_SD_SHIPMENT_DETAIL.qty,0)  as Qty,-1 as RI,TSPL_SD_SALES_ORDER_DETAIL.Unit_code AS Uom  "
        qry += " ,TSPL_SD_SALES_ORDER_HEAD.customer_code as VSP,'' as Invoice_No from TSPL_SD_SALES_ORDER_DETAIL"
        qry += " left outer join TSPL_SD_SALES_ORDER_HEAD on TSPL_SD_SALES_ORDER_HEAD.Document_Code=TSPL_SD_SALES_ORDER_DETAIL.DOCUMENT_CODE "
        qry += " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order  =TSPL_SD_SALES_ORDER_HEAD.DOCUMENT_CODE "
        qry += " left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE =TSPL_SD_SHIPMENT_HEAD.Document_Code"
        qry += " where TSPL_SD_SHIPMENT_HEAD.Status=0  "

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_SD_SALES_ORDER_HEAD.Document_Date ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_SD_SALES_ORDER_HEAD.Document_Date ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_SD_SALES_ORDER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If

        'If _IsMRPMandatory AndAlso _MRP > 0 Then
        '    qry += " and TSPL_SD_SALES_ORDER_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
        'End If
        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_SD_SALES_ORDER_DETAIL.Unit_code='" + _UOM + "' "
        'End If
        qry += " and TSPL_SD_SALES_ORDER_DETAIL.CommitedQty>0 " 'and TSPL_SD_SALES_ORDER_DETAIL.DOCUMENT_CODE not  in('" + _TransNo + "') "
        qry += "  union all " + Environment.NewLine
        qry += " select  'Shipment' as TransType,'Shipment' as TransCode,TSPL_SD_SHIPMENT_HEAD.Document_Code as DocNo,convert( varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as DocDate ,TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location as Locaion,TSPL_SD_SHIPMENT_DETAIL.Qty as Qty,-1 as RI,TSPL_SD_SHIPMENT_DETAIL.Unit_code AS Uom  "
        qry += " ,TSPL_SD_SHIPMENT_HEAD.Customer_code as VSP,TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No as Invoice_No from TSPL_SD_SHIPMENT_DETAIL "
        qry += " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE"
        qry += " where TSPL_SD_SHIPMENT_HEAD.Status=0 "

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_SD_SHIPMENT_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If

        'If _IsMRPMandatory AndAlso _MRP > 0 Then
        '    qry += " and TSPL_SD_SHIPMENT_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
        'End If
        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_SD_SHIPMENT_DETAIL.Unit_code='" + _UOM + "' "
        'End If
        qry += " and TSPL_SD_SHIPMENT_DETAIL.Qty<>0 " ' and TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE not in ('" + _TransNo + "')"
        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowStockCheckatDOLevel, clsFixedParameterCode.AllowStockCheckatDOLevel, trans)), "1") = CompairStringResult.Equal Then
            qry += " and TSPL_SD_SHIPMENT_HEAD.Trans_Type not in ( 'PS') " + Environment.NewLine +
            " union all " + Environment.NewLine +
            " select * from (" + Environment.NewLine +
            " select 'DeliveryOrderPS' as TransType,'DeliveryOrderPS' as TransCode,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code as DocNo,convert( varchar,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Date,103) as DocDate ,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code as ICode,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Bill_To_Location as Locaion " + Environment.NewLine +
            ",case when isnull(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close,'N')='N' then TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Qty -isnull((select sum( TSPL_SD_SHIPMENT_DETAIL.qty) from TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code= TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_HEAD.Status=1 and TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS =TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.DOCUMENT_CODE and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code and TSPL_SD_SHIPMENT_DETAIL.Unit_code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Unit_code),0)" + Environment.NewLine +
            " else isnull((select sum( TSPL_SD_SHIPMENT_DETAIL.qty) from TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code= TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_HEAD.Status=0 and TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS =TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.DOCUMENT_CODE and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code and TSPL_SD_SHIPMENT_DETAIL.Unit_code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Unit_code),0)  end as Qty ,-1 as RI,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Unit_code AS Uom  " + Environment.NewLine +
            " ,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.CUSTOMER_CODE as VSP,'' as Invoice_No from TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE " + Environment.NewLine +
            " left outer join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.DOCUMENT_CODE " + Environment.NewLine +
            " where 2=2 " + Environment.NewLine

            If fromDate.Checked = True Then
                qry += " and convert(date,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Date ,103) >= convert(date,'" + fromDate.Value + "',103) "
            End If
            If ToDate.Checked = True Then
                qry += " and convert(date,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Date ,103) <= convert(date,'" + ToDate.Value + "',103) "
            End If

            If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
                qry += " and TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
            End If
            If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Bill_To_Location in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
            End If


            'If _IsMRPMandatory AndAlso _MRP > 0 Then
            '    qry += " and TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.MRP='" + clsCommon.myCstr(_MRP) + "' "
            'End If
            'If IsItemWithDifferntUnitConsiderAsOtherItem Then
            '    qry += " and TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Unit_code='" + _UOM + "' "
            'End If
            ' qry += " and (TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.DOCUMENT_CODE not in ('" + _TransNo + "'))" + Environment.NewLine + _
            qry += " ) x where Qty>0 "
        End If



        '' query for assemblies and disassemblies
        qry += " union all " + Environment.NewLine
        qry += " select 'Assemblies' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmAssemblies) + "' as TransCode,TSPL_PJC_ASSEMBLIES.CODE as DocNo, convert( varchar,TSPL_PJC_ASSEMBLIES.ASSEMBLY_DATE,103) as DocDate,Main_Item_Code as ICode,LOCATION_CODE as Location,QUANTITY,(case when TRANSACTION_TYPE='Assembly' then 1  else -1 end) as RI,"
        qry += " BUILD_ITEM_UNIT_CODE as UnitCode,'' as VSP,'' as Invoice_No from TSPL_PJC_ASSEMBLIES where TSPL_PJC_ASSEMBLIES.POSTED=0  " ' and TSPL_PJC_ASSEMBLIES.CODE  not in ('" + _TransNo + "')"


        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_PJC_ASSEMBLIES.ASSEMBLY_DATE ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_PJC_ASSEMBLIES.ASSEMBLY_DATE ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_PJC_ASSEMBLIES.Main_Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_PJC_ASSEMBLIES.LOCATION_CODE in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If
        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and BUILD_ITEM_UNIT_CODE='" + _UOM + "' "
        'End If

        qry += " union all  "

        qry += " select  'Assemblies' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmAssemblies) + "' as TransCode,TSPL_PJC_ASSEMBLIES.CODE as DocNo,convert( varchar,TSPL_PJC_ASSEMBLIES.ASSEMBLY_DATE,103) as DocDate, TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE AS ICode,TSPL_PJC_ASSEMBLIES.LOCATION_CODE as Location,"
        qry += " TSPL_MF_BOM_DETAIL.CONSM_QUANTITY*(TSPL_PJC_ASSEMBLIES.QUANTITY/TSPL_PJC_ASSEMBLIES.BUILD_QUANTITY) as Qty,"
        qry += " (case when TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly' then  -1 else  1 end) AS RI,"
        qry += " TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE as UnitCode,'' as VSP,'' as Invoice_No from TSPL_PJC_ASSEMBLIES "
        qry += " inner join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_PJC_ASSEMBLIES.BOM_CODE"
        qry += " inner JOIN TSPL_MF_BOM_DETAIL ON TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_MF_BOM_DETAIL.BOM_CODE "
        qry += " where TSPL_PJC_ASSEMBLIES.POSTED=0  " ' and TSPL_PJC_ASSEMBLIES.CODE  not in ('" + _TransNo + "')"

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_PJC_ASSEMBLIES.ASSEMBLY_DATE ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_PJC_ASSEMBLIES.ASSEMBLY_DATE ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_PJC_ASSEMBLIES.LOCATION_CODE in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If

        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE='" + _UOM + "' "
        'End If

        qry += " union all " + Environment.NewLine
        qry += " select 'Dis-Assemblies' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmAssemblies) + "' as TransCode,TSPL_PROD_ASSEMBLIES.CODE as DocNo,convert( varchar,TSPL_PROD_ASSEMBLIES.ASSEMBLY_DATE,103) as DocDate , Main_Item_Code as ICode,LOCATION_CODE as Location,QUANTITY,(case when TRANSACTION_TYPE='Assembly' then 1  else -1 end) as RI,"
        qry += " BUILD_ITEM_UNIT_CODE as UnitCode,'' as VSP,'' as Invoice_No from TSPL_PROD_ASSEMBLIES where TSPL_PROD_ASSEMBLIES.POSTED=0  " ' and TSPL_PROD_ASSEMBLIES.CODE  not in ('" + _TransNo + "')"

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_PROD_ASSEMBLIES.ASSEMBLY_DATE ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_PROD_ASSEMBLIES.ASSEMBLY_DATE ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_PROD_ASSEMBLIES.Main_Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_PROD_ASSEMBLIES.LOCATION_CODE in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If

        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and BUILD_ITEM_UNIT_CODE='" + _UOM + "' "
        'End If

        qry += " union all  "

        qry += " select  'Assemblies' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmAssemblies) + "' as TransCode,TSPL_PJC_ASSEMBLIES.CODE as DocNo,convert( varchar,TSPL_PJC_ASSEMBLIES.ASSEMBLY_DATE,103) as DocDate ,TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE AS ICode,TSPL_PJC_ASSEMBLIES.LOCATION_CODE as Location,"
        qry += " TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_QUANTITY*(TSPL_PJC_ASSEMBLIES.QUANTITY/TSPL_PJC_ASSEMBLIES.BUILD_QUANTITY) as Qty,"
        qry += " (case when TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly' then  -1 else  1 end) AS RI,"
        qry += " TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_UNIT_CODE as UnitCode,'' as VSP,'' as Invoice_No from TSPL_PJC_ASSEMBLIES "
        qry += " inner JOIN TSPL_PROD_ASSEMBLIES_ITEM_DETAIL ON TSPL_PJC_ASSEMBLIES.CODE=TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.ASSEMBLY_CODE "
        qry += " where TSPL_PJC_ASSEMBLIES.POSTED=0   " 'and TSPL_PJC_ASSEMBLIES.CODE  not in ('" + _TransNo + "')"
        

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_PJC_ASSEMBLIES.ASSEMBLY_DATE ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_PJC_ASSEMBLIES.ASSEMBLY_DATE ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_PJC_ASSEMBLIES.LOCATION_CODE in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If
        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_UNIT_CODE='" + _UOM + "' "
        'End If


        qry += " union all  "

        qry += " select  'Wreckage' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmAssemblies) + "' as TransCode,TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE as DocNo,convert( varchar,TSPL_WRECKAGE_ENTRY.PROD_DATE,103) as DocDate, TSPL_WRECKAGE_BOOKING.Item_Code AS ICode,TSPL_WRECKAGE_ENTRY.LOCATION_CODE as Location,"
        qry += " TSPL_WRECKAGE_BOOKING.WRECKAGE_QTY as Qty, -1 AS RI,"
        qry += " TSPL_WRECKAGE_BOOKING.Unit_Code as UnitCode,'' as VSP,'' as Invoice_No from TSPL_WRECKAGE_ENTRY "
        qry += " inner JOIN TSPL_WRECKAGE_BOOKING ON TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE=TSPL_WRECKAGE_BOOKING.WRECKAGE_CODE "
        qry += " where TSPL_WRECKAGE_ENTRY.POSTED=0  " ' and TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE  not in ('" + _TransNo + "')"

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_WRECKAGE_ENTRY.PROD_DATE ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_WRECKAGE_ENTRY.PROD_DATE ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_WRECKAGE_BOOKING.ITEM_CODE in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_WRECKAGE_ENTRY.LOCATION_CODE in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If

        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_WRECKAGE_BOOKING.Unit_Code='" + _UOM + "' "
        'End If

        qry += " union all " + Environment.NewLine
        qry += " select 'Production Issue' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmProcessProductionIssueEntry) + "' as TransCode,TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code as DocNo,convert( varchar,TSPL_PP_ISSUE_HEAD.Issue_Date,103) as DocDate, TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code as ICode,TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code as Location,TSPL_PP_ISSUE_ITEM_DETAIL.Qty as QUANTITY,-1 as RI,"
        qry += " TSPL_PP_ISSUE_ITEM_DETAIL.Unit_Code as UnitCode,'' as VSP,'' as Invoice_No from TSPL_PP_ISSUE_ITEM_DETAIL left outer join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code where TSPL_PP_ISSUE_HEAD.Is_post=0  " ' and TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code  not in ('" + _TransNo + "')"

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_PP_ISSUE_HEAD.Issue_Date ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_PP_ISSUE_HEAD.Issue_Date ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If
        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_PP_ISSUE_ITEM_DETAIL.Unit_Code='" + _UOM + "' "
        'End If
        ' Transfer =====================================================================
        qry += " union all " + Environment.NewLine
        qry += " select 'TRANSFER_OUT' as TransType,'TRANSFER_OUT' as TransCode,TSPL_TRANSFER_ORDER_HEAD.Document_NO as DocNo,Convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_DAte,103) as DocDate ,TSPL_TRANSFER_ORDER_DETAIL.Item_Code as ICode,TSPL_TRANSFER_ORDER_HEAD.From_Location as Locaion,TSPL_TRANSFER_ORDER_DETAIL.Out_Qty as Qty,-1 as RI,TSPL_TRANSFER_ORDER_DETAIL.Unit_code AS Uom,'' as VSP,'' as Invoice_No from TSPL_TRANSFER_ORDER_DETAIL "
        qry += " left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_TRANSFER_ORDER_DETAIL.Document_No " & _
               " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TRANSFER_ORDER_HEAD.From_Location " & _
               " where TSPL_TRANSFER_ORDER_HEAD.Status=0  " & _
               " and TSPL_TRANSFER_ORDER_DETAIL.Out_Qty<>0 and TSPL_TRANSFER_ORDER_HEAD.Transfer_Type  = 'O' "
        
        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_DAte ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_DAte ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_TRANSFER_ORDER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_TRANSFER_ORDER_HEAD.From_Location in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If


        ' PHYSICAL STOCK

        qry += " union all " + Environment.NewLine
        qry += "   select 'PHYSICAL_STOCK' as TransType,'PHYSICAL_STOCK' as TransCode,tspl_physical_stock.Physical_No as DocNo,Convert(varchar,tspl_physical_stock.STOCK_DATE,103) as DocDate ,tspl_physical_stock.Item_Code as ICode,tspl_physical_stock.Location as Locaion,tspl_physical_stock.Physical_Qty as Qty,-1 as RI,tspl_physical_stock.Stock_Unit AS Uom,'' as VSP,'' as Invoice_No from tspl_physical_stock " &
               "   left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=tspl_physical_stock.Location " &
               "   where tspl_physical_stock.Is_Posted=0  " &
               "   and tspl_physical_stock.Physical_Qty<>0 "

        If fromDate.Checked = True Then
            qry += " and convert(date,tspl_physical_stock.STOCK_DATE ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,tspl_physical_stock.STOCK_DATE ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and tspl_physical_stock.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and tspl_physical_stock.Location in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If


        ' Export Sale Invoice 
        qry += " union all " + Environment.NewLine
        qry += " select 'Export_Sales_Invoice' as TransType,'Export_Sales_Invoice' as TransCode,TSPL_SD_SALE_INVOICE_HEAD.Document_Code as DocNo,Convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_DAte,103) as DocDate ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as ICode,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as Locaion,TSPL_SD_SALE_INVOICE_DETAIL.Qty as Qty,-1 as RI,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code AS Uom,TSPL_SD_SALE_INVOICE_HEAD.customer_code as VSP,'' as Invoice_No from TSPL_SD_SALE_INVOICE_DETAIL " &
               " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.Document_Code " &
               " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location " &
               " where TSPL_SD_SALE_INVOICE_HEAD.Status=0 " &
               " and TSPL_SD_SALE_INVOICE_DETAIL.Qty<>0  and TSPL_SD_SALE_INVOICE_HEAD.trans_type='EXP'  "


        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_DAte ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_DAte ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_SD_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If



        qry += " union all " + Environment.NewLine

        qry += " select 'Production_Jobwork_Dispatch' as TransType,'Production_Jobwork_Dispatch' as TransCode,TSPL_SCRAPSALE_HEAD.shipment_No as DocNo, convert( varchar,TSPL_SCRAPSALE_HEAD.shipment_Date,103) as DocDate,TSPL_SCRAPSALE_DETAIL.Item_Code as ICode,TSPL_SCRAPSALE_HEAD.Loc_Code as Locaion,TSPL_SCRAPSALE_DETAIL.shipped_Qty as Qty,-1 as RI,TSPL_SCRAPSALE_DETAIL.Unit_code AS Uom "
        qry += " ,TSPL_SCRAPSALE_HEAD.cust_code as VSP,'' as Invoice_No from TSPL_SCRAPSALE_DETAIL "
        qry += " left outer join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No"
        qry += " where TSPL_SCRAPSALE_HEAD.IsPost=0 and  TSPL_SCRAPSALE_HEAD.Doc_Type = 'J' "
        'If clsCommon.myLen(_LCode) > 0 Then
        '    qry += "  and TSPL_SCRAPSALE_HEAD.Loc_Code='" + _LCode + "' "
        'End If

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_SCRAPSALE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_SCRAPSALE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If


        qry += " union all " + Environment.NewLine
        qry += "  select 'VSP_Item_Issue' as TransType,'VSP_Item_Issue' as TransCode,TSPL_VSPItem_HEAD.Doc_No as DocNo,Convert(varchar,TSPL_VSPItem_HEAD.Doc_DAte,103) as DocDate ,TSPL_VSPItem_DETAIL.Item_Code as ICode,TSPL_VSPItem_HEAD.From_Location as Locaion,TSPL_VSPItem_DETAIL.Issued_Qty as Qty,-1 as RI,TSPL_VSPItem_DETAIL.Unit_code AS Uom,'' as VSP,'' as Invoice_No from TSPL_VSPItem_DETAIL " &
               "  left outer join TSPL_VSPItem_HEAD on TSPL_VSPItem_HEAD.Doc_No=TSPL_VSPItem_DETAIL.Doc_No " &
               "  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VSPItem_HEAD.From_Location " &
               "  where TSPL_VSPItem_HEAD.Status=0   " &
               "  and TSPL_VSPItem_DETAIL.Issued_Qty<>0  and TSPL_VSPItem_HEAD.Doc_Type = 'Issue'  "

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_VSPItem_HEAD.Doc_DAte ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_VSPItem_HEAD.Doc_DAte ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_VSPItem_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_VSPItem_HEAD.From_Location in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If




        qry += " union all " + Environment.NewLine
        qry += " select 'VSP_Asset_Issue' as TransType,'VSP_Asset_Issue' as TransCode,TSPL_VSPAsset_HEAD.Doc_No as DocNo,Convert(varchar,TSPL_VSPAsset_HEAD.Doc_Date,103) as DocDate ,TSPL_VSPAsset_DETAIL.Item_Code as ICode,TSPL_VSPAsset_HEAD.From_Location as Locaion,TSPL_VSPAsset_DETAIL.Issued_Qty as Qty,-1 as RI,TSPL_VSPAsset_DETAIL.Unit_code AS Uom,'' as VSP,'' as Invoice_No from TSPL_VSPAsset_DETAIL " &
               " left outer join TSPL_VSPAsset_HEAD on TSPL_VSPAsset_HEAD.Doc_No=TSPL_VSPAsset_DETAIL.Doc_No " &
               " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VSPAsset_HEAD.From_Location " &
               " where TSPL_VSPAsset_HEAD.Status=0  " &
               " and TSPL_VSPAsset_DETAIL.Issued_Qty<>0   and TSPL_VSPAsset_HEAD.Doc_Type = 'Issue' "

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_VSPAsset_HEAD.Doc_Date ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_VSPAsset_HEAD.Doc_Date ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_VSPAsset_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_VSPAsset_HEAD.From_Location in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If


        qry += " union all " + Environment.NewLine
        qry += " select 'MCC Material Sale Farmer' as TransType,'MCC Material Sale Farmer' as TransCode,TSPL_MCC_Sale_Farmer_Head.Document_Code as DocNo,Convert(varchar,TSPL_MCC_Sale_Farmer_Head.Document_Date,103) as DocDate ,TSPL_MCC_SALE_FARMER_DETAIL.Item_Code as ICode,TSPL_MCC_Sale_Farmer_Head.Bill_To_Location as Locaion,TSPL_MCC_SALE_FARMER_DETAIL.Qty as Qty,-1 as RI,TSPL_MCC_SALE_FARMER_DETAIL.Unit_code AS Uom,TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader as VSP,'' as Invoice_No from TSPL_MCC_SALE_FARMER_DETAIL " &
               " left outer join TSPL_MCC_Sale_Farmer_Head on TSPL_MCC_Sale_Farmer_Head.Document_Code=TSPL_MCC_SALE_FARMER_DETAIL.Document_Code " &
               " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_Sale_Farmer_Head.Bill_To_Location " &
               "  left outer join  TSPL_mp_master on TSPL_mp_master.mp_code=TSPL_MCC_Sale_Farmer_HEAD.farmer_code left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_mp_master.vlc_Code " &
               " where TSPL_MCC_Sale_Farmer_Head.Status=0   " &
               " and TSPL_MCC_SALE_FARMER_DETAIL.Qty<>0    "

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_MCC_Sale_Farmer_Head.Document_Date ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_MCC_Sale_Farmer_Head.Document_Date ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_MCC_SALE_FARMER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_MCC_Sale_Farmer_Head.Bill_To_Location in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If

        qry += " union all " + Environment.NewLine
        qry += " select 'Bulk_Dispatch' as TransType,'Bulk_Dispatch' as TransCode,TSPL_Dispatch_BulkSale.Document_No as DocNo,Convert(varchar,TSPL_Dispatch_BulkSale.Document_DAte,103) as DocDate ,TSPL_Dispatch_Detail_BulkSale.Item_Code as ICode,TSPL_Dispatch_BulkSale.Location_Code as Locaion,TSPL_Dispatch_Detail_BulkSale.Qty as Qty,-1 as RI,TSPL_Dispatch_Detail_BulkSale.Unit_code AS Uom,'' as VSP,'' as Invoice_No from TSPL_Dispatch_Detail_BulkSale " &
               " left outer join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_Dispatch_Detail_BulkSale.Document_No " &
               " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_Dispatch_BulkSale.Location_Code " &
               " where TSPL_Dispatch_BulkSale.Posted=0  " &
               " and TSPL_Dispatch_Detail_BulkSale.Qty<>0    "

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_Dispatch_BulkSale.Document_DAte ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_Dispatch_BulkSale.Document_DAte ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_Dispatch_Detail_BulkSale.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_Dispatch_BulkSale.Location_Code in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If


        qry += " union all " + Environment.NewLine
        qry += "  select 'Bulk_Milk_Purchase_Return' as TransType,'Bulk_Milk_Purchase_Return' as TransCode,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No as DocNo,Convert(varchar,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103) as DocDate ,TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Item_Code as ICode,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Loc_Code as Locaion,TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Invoice_Qty as Qty,-1 as RI,TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.UOM AS Uom,'' as VSP,'' as Invoice_No from TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL " &
               "  left outer join TSPL_BULK_MILK_PURCHASE_RETURN_HEAD on TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No=TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Pur_Return_No " &
               "  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Loc_Code " &
               "  where TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.isPosted=0   " &
               "  and TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Invoice_Qty<>0  "

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If

        qry += " union all " + Environment.NewLine
        qry += " select 'Milk Transfer In Return' as TransType,'Milk Transfer In Return' as TransCode,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No as DocNo,Convert(varchar,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,103) as DocDate ,tspl_weighment_detail.Item_Code as ICode,TSPL_MILK_TRANSFER_IN_RETURN.location_code as Locaion,tspl_weighment_detail.Qty_In_Kg as Qty,-1 as RI,tspl_weighment_detail.UOM AS Uom,'' as VSP,'' as Invoice_No from tspl_weighment_detail " &
               " left outer join TSPL_MILK_TRANSFER_IN_RETURN on TSPL_MILK_TRANSFER_IN_RETURN.Weighment_No=tspl_weighment_detail.Weighment_No " &
               " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MILK_TRANSFER_IN_RETURN.location_code " &
               " where TSPL_MILK_TRANSFER_IN_RETURN.isPosted=0   " &
               " and tspl_weighment_detail.Qty_In_Kg<>0 "

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and tspl_weighment_detail.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_MILK_TRANSFER_IN_RETURN.location_code in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If

        '' SRN
        'qry += " union all " + Environment.NewLine
        'qry += " select 'Store Received Note' as TransType,'Store Received Note' as TransCode,TSPL_SRN_HEAD.SRN_No as DocNo,Convert(varchar,TSPL_SRN_HEAD.SRN_Date,103) as DocDate ,TSPL_SRN_DETAIL.Item_Code as ICode,TSPL_SRN_HEAD.Bill_To_Location as Locaion,TSPL_SRN_DETAIL.SRN_Qty as Qty,-1 as RI,TSPL_SRN_DETAIL.Unit_code AS Uom from TSPL_SRN_DETAIL " & _
        '       " left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No " & _
        '       " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SRN_HEAD.Bill_To_Location " & _
        '       " where (TSPL_SRN_HEAD.Status=0 and TSPL_SRN_DETAIL.Item_Code= '" & _ICode & "') " & _
        '       " and TSPL_SRN_DETAIL.SRN_Qty<>0 "
        'If clsCommon.myLen(_LCode) > 0 Then
        '    qry += " and TSPL_SRN_HEAD.Bill_To_Location ='" & _LCode & "'"
        'End If

        '===============================================================================
        '' query for add/remove items durng Process production Standardization

        qry += " union all " + Environment.NewLine
        qry += " select 'Production Standardization' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmProcessProductionStandardization) + "' as TransCode,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code as Doc_No,convert( varchar,TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date,103) as DocDate,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Item_Code as ICode,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Loaction_Code as Location,"
        qry += " TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY,"
        qry += " (case when TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then 1 else  -1  end)as RI,"
        qry += " TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE,'' as VSP,'' as Invoice_No from TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL "
        qry += " inner join TSPL_PP_STANDARDIZATION_HEAD on TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code = TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code "
        qry += " where TSPL_PP_STANDARDIZATION_HEAD.Posted=0  "
        'qry += " and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code not in ('" + _TransNo + "')"
        qry += " and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Remove' "

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Loaction_Code in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If

        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE='" + _UOM + "' "
        'End If
        '' query for add/remove items durng Process production STAGE PROCESS  ' clsCommon.myCstr(clsUserMgtCode.frmProcessProductionStageProcess) replace  PP_SP below line
        qry += " union all " + Environment.NewLine
        qry += " select 'Production Stage Process' as TransType, 'PP_SP' as TransCode,TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE as Doc_No, convert( varchar,TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_DATE,103) as DocDate ,TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code as ICode,TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Loaction_Code as Location,"
        qry += " TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY,"
        qry += " (case when TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then 1 else  -1  end)as RI,"
        qry += " TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE,'' as VSP,'' as Invoice_No from TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL "
        qry += " inner join TSPL_PP_STAGE_PROCESS_HEAD on TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE = TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE "
        qry += " where TSPL_PP_STAGE_PROCESS_HEAD.Posted=0  "
        'qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE not in ('" + _TransNo + "')"
        qry += "  and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Remove'"

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_DATE ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_DATE ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Loaction_Code in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If

        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE='" + _UOM + "' "
        'End If
        ''For CSA Transfer
        qry += " union all " + Environment.NewLine +
        " select 'CSA Transfer' as TransType,'SD-CSATRANS' as TransCode,TSPL_CSA_TRANSFER_HEAD.DOC_CODE as Doc_No,convert( varchar,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103) as DocDate, TSPL_CSA_TRANSFER_DETAIL.Item_Code as ICode,TSPL_CSA_TRANSFER_HEAD.From_Location_Code as Location, TSPL_CSA_TRANSFER_DETAIL.Qty, -1 as RI, TSPL_CSA_TRANSFER_DETAIL.Unit_code as Uom " + Environment.NewLine +
        " ,TSPL_CSA_TRANSFER_HEAD.cust_code as VSP,'' as Invoice_No from TSPL_CSA_TRANSFER_DETAIL " + Environment.NewLine +
        " left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DOC_CODE=TSPL_CSA_TRANSFER_DETAIL.DOC_CODE " + Environment.NewLine +
        " where TSPL_CSA_TRANSFER_HEAD.Status=0  " ' and TSPL_CSA_TRANSFER_HEAD.DOC_CODE not in ('" + _TransNo + "') "

        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_CSA_TRANSFER_HEAD.Transfer_Date ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_CSA_TRANSFER_HEAD.Transfer_Date ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_CSA_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_CSA_TRANSFER_HEAD.From_Location_Code in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If

        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_CSA_TRANSFER_DETAIL.Unit_code ='" + _UOM + "'  "
        'End If
        'If _IsMRPMandatory AndAlso _MRP > 0 Then
        '    qry += " and TSPL_CSA_TRANSFER_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "'"
        'End If
        qry += " union all " + Environment.NewLine
        qry += " select 'Milk Jobwork' as Trans_Type,'" + clsCommon.myCstr(clsUserMgtCode.frmMilkJobWorkTransfer) + "' as Trans_Code,JWD.Document_Code as Doc_No, convert( varchar,JW.Document_Date,103) as DocDate ,JWD.Item_Code as ICode,JW.Loc_Code,JWD.Net_Weight,-1 AS RI," &
               " JWD.UOM,JW.vendor_code as VSP,'' as Invoice_No  from TSPL_MILK_JOBWORK_TRANSFER_DETAILS JWD inner join TSPL_MILK_JOBWORK_TRANSFER_HEAD JW on JWD.Document_Code=JW.Document_Code " &
               " where  JW.isPosted=0  " 'AND JWD.Document_Code NOT IN ('" + _TransNo + "') "

        If fromDate.Checked = True Then
            qry += " and convert(date,JW.Document_Date ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,JW.Document_Date ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and JWD.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and JW.Loc_Code in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If


        qry += " union all " + Environment.NewLine
        qry += " select 'Milk Jobwork Other' as Trans_Type,'" + clsCommon.myCstr(clsUserMgtCode.frmMilkJobWorkTransferOther) + "' as Trans_Code,TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TRANSFER_NO as Doc_No,convert( varchar,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_Date,103) as DocDate,TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Item_Code as ICode,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.From_Locaction,TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Qty,-1 AS RI," &
               " TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.UOM,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.vendor_code as VSP,'' as Invoice_No  from TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS inner join TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD on TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_NO=TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TRANSFER_NO " &
               " where  TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Status=0  " 'AND TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TRANSFER_NO NOT IN ('" + _TransNo + "') "


        If fromDate.Checked = True Then
            qry += " and convert(date,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_Date ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_Date ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.From_Locaction in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If

        ' For Tanker Dispatch 
        'If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsChamberWiseTanker, clsFixedParameterCode.IsChamberWiseTanker, Nothing)) = 1 Then
        '    qry += " union all " + Environment.NewLine
        '    qry += " select 'Tanker Dispatch' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmMCCDispatch) + "' as TransCode,TSPL_MCC_Dispatch_Challan_Stock_Detail.Chalan_NO as DocNo, Convert(varchar,TSPL_MCC_Dispatch_Challan_Stock_Detail.Chalan_Date,103) as DocDate,TSPL_MCC_Dispatch_Challan_Stock_Detail.Item_Code,TSPL_MCC_Dispatch_Challan_Stock_Detail.Location_Code as From_Locaction,TSPL_MCC_Dispatch_Challan_Stock_Detail.Qty as Qty,,-1 as RI,TSPL_MCC_Dispatch_Challan_Stock_Detail.UOM AS Uom from TSPL_MCC_Dispatch_Challan_Stock_Detail where " & _
        '           " TSPL_MCC_Dispatch_Challan_Stock_Detail.isPosted = 0 and TSPL_MCC_Dispatch_Challan_Stock_Detail.Item_Code = '" + _ICode + "' "
        '    If clsCommon.myLen(_LCode) > 0 Then
        '        qry += " and TSPL_MCC_Dispatch_Challan_Stock_Detail.Location_Code ='" + _LCode + "'  "
        '    End If
        'Else
        ' Ticket No : BHA/06/02/19-000809 by prabhakar for Tanker Disptach unposted Qty
        qry += " union all " + Environment.NewLine
        qry += " select 'Tanker Dispatch' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmMCCDispatch) + "' as Trans_Code,tspl_mcc_dispatch_challan.Chalan_NO as DocNo, Convert(varchar,tspl_mcc_dispatch_challan.Dispatch_Date,103) as DocDate,tspl_mcc_dispatch_challan.Item_Code,tspl_mcc_dispatch_challan.MCC_Code as From_Locaction ,tspl_mcc_dispatch_challan.Net_Qty as Qty,-1 as RI,tspl_mcc_dispatch_challan.UOM_Code AS Uom ,'' as VSP,'' as Invoice_No from tspl_mcc_dispatch_challan where " &
               " tspl_mcc_dispatch_challan.isPosted = 0  "

        If fromDate.Checked = True Then
            qry += " and convert(date,tspl_mcc_dispatch_challan.Dispatch_Date ,103) >= convert(date,'" + fromDate.Value + "',103) "
        End If
        If ToDate.Checked = True Then
            qry += " and convert(date,tspl_mcc_dispatch_challan.Dispatch_Date ,103) <= convert(date,'" + ToDate.Value + "',103) "
        End If

        If fndItemCode.arrValueMember IsNot Nothing AndAlso fndItemCode.arrValueMember.Count > 0 Then
            qry += " and tspl_mcc_dispatch_challan.Item_Code in (" + clsCommon.GetMulcallString(fndItemCode.arrValueMember) + ") "
        End If
        If fndLocation.arrValueMember IsNot Nothing AndAlso fndLocation.arrValueMember.Count > 0 Then
            qry += " and tspl_mcc_dispatch_challan.MCC_Code in (" + clsCommon.GetMulcallString(fndLocation.arrValueMember) + ") "
        End If
        'End If


        qry += " )xx" + Environment.NewLine
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM"
        qry += "  left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode   and FinalUOM.UOM_Code = xx.UOM "  'and FinalUOM.UOM_Code='" + _UOM + "'"
        Return qry
    End Function


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptUnpostingTransItemQty & "'"))
                'arrHeader.Add("Item Code : " + txtItemCode.Value)
                'If clsCommon.myLen(txtLocationCode.Value) > 0 Then
                '    arrHeader.Add("Location : " + txtLocationCode.Value)
                'End If

                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToExcelGrid("Item Commited Quantity Report", gv1, arrHeader, Me.Text)
                Else
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Item Commited Quantity Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                clsCommon.MyMessageBoxShow("No Data Found for Print", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            Dim strTransType As String = clsCommon.myCstr(gv1.CurrentRow.Cells("TransType").Value)
            Dim strDocNo As String = clsCommon.myCstr(gv1.CurrentRow.Cells("DocNo").Value)
            If clsCommon.CompairString(strTransType, "Export_Sales_Invoice") = CompairStringResult.Equal Then
                If clsCommon.myLen(strDocNo) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesInvoiceMT, strDocNo)
                End If
            ElseIf clsCommon.CompairString(strTransType, "Production Issue") = CompairStringResult.Equal Then
                If clsCommon.myLen(strDocNo) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionIssueEntry, strDocNo)
                End If
            ElseIf clsCommon.CompairString(strTransType, "Production Standardization") = CompairStringResult.Equal Then
                If clsCommon.myLen(strDocNo) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStandardization, strDocNo)
                End If
            ElseIf clsCommon.CompairString(strTransType, "Milk Jobwork Other") = CompairStringResult.Equal Then
                If clsCommon.myLen(strDocNo) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransferOther, strDocNo)
                End If
                ' VSPTRAN
                'ElseIf clsCommon.CompairString(strTransType, "VSP_Item_Issue") = CompairStringResult.Equal Then
                '    If clsCommon.myLen(strDocNo) > 0 Then
                '        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPItemIssue, strDocNo)
                '    End If
                'VSPASSETISSUE
            ElseIf clsCommon.CompairString(strTransType, "VSP_Asset_Issue") = CompairStringResult.Equal Then
                If clsCommon.myLen(strDocNo) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPAssetIssue, strDocNo)
                End If
                'ICAdj
            ElseIf clsCommon.CompairString(strTransType, "Store_Adjustment") = CompairStringResult.Equal Then
                If clsCommon.myLen(strDocNo) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, strDocNo)
                End If
                ' DispatchBS
            ElseIf clsCommon.CompairString(strTransType, "Bulk_Dispatch") = CompairStringResult.Equal Then
                If clsCommon.myLen(strDocNo) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSale, strDocNo)
                End If
                ' Bulk_Purchase_Return

            ElseIf clsCommon.CompairString(strTransType, "Bulk_Milk_Purchase_Return") = CompairStringResult.Equal Then
                If clsCommon.myLen(strDocNo) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkPurchaseReturn, strDocNo)
                End If

                'MilkTransferInReturn

            ElseIf clsCommon.CompairString(strTransType, "Milk Transfer In Return") = CompairStringResult.Equal Then
                If clsCommon.myLen(strDocNo) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferInReturn, strDocNo)
                End If
                ' PurchaseReturn
            ElseIf clsCommon.CompairString(strTransType, "Purchase Return") = CompairStringResult.Equal Then
                If clsCommon.myLen(strDocNo) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, strDocNo)
                End If
                'RGP
            ElseIf clsCommon.CompairString(strTransType, "RGP") = CompairStringResult.Equal Then
                If clsCommon.myLen(strDocNo) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strDocNo)
                End If
                'ScrapShipment
            ElseIf clsCommon.CompairString(strTransType, "Scrap Sale") = CompairStringResult.Equal Then
                If clsCommon.myLen(strDocNo) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSale, strDocNo)
                End If
                'IssueReturnTransfer
            ElseIf clsCommon.CompairString(strTransType, "Issue/Return/Transfer") = CompairStringResult.Equal Then
                If clsCommon.myLen(strDocNo) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnIssueReturn, strDocNo)
                End If
                'clsUserMgtCode.Transfer
            ElseIf clsCommon.CompairString(strTransType, "TRANSFER_OUT") = CompairStringResult.Equal Then
                If clsCommon.myLen(strDocNo) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strDocNo)
                End If
                ' Tanker Dispatch
            ElseIf clsCommon.CompairString(strTransType, "Tanker Dispatch") = CompairStringResult.Equal Then
                If clsCommon.myLen(strDocNo) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, strDocNo)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    ' Ticket No : TEC/25/02/19-000430 By Prabhakar For MultiSelection 
    Private Sub fndItemCode__My_Click(sender As Object, e As EventArgs) Handles fndItemCode._My_Click
        Dim qry As String = " select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER "
        fndItemCode.arrValueMember = clsCommon.ShowMultipleSelectForm("UnpostingItemCode@MulitSection", qry, "Code", "Name", fndItemCode.arrValueMember, fndItemCode.arrDispalyMember)
    End Sub

    Private Sub fndLocation__My_Click(sender As Object, e As EventArgs) Handles fndLocation._My_Click
        Dim qry As String = " select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        fndLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("UnpostingLocationCode@MulitSection", qry, "Code", "Name", fndLocation.arrValueMember, fndLocation.arrDispalyMember)
    End Sub

    Private Sub RptUnpostingTransItemQty_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        Reset()
    End Sub


End Class
