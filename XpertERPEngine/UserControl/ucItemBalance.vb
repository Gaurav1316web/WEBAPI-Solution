'-------------------BM00000003620--------------------
Imports System.Data.SqlClient
Imports common
Public Class ucItemBalance
    Private _ICode As String = ""
    Private _IName As String = ""
    Private _LCode As String = ""
    Private _LName As String = ""
    Private _MRP As Double = 0
    Private _UOM As String = ""
    Private _ShowPOQty As Boolean = False
    Private _ShowSOQty As Boolean = False
    Private _ShowCSADOQty As Boolean = False
    Private _TransDate As DateTime
    Private _TransNo As String = ""
    Private _Visible As Boolean = False
    Private _LblVisible As Boolean = False

    Private _IsMRPMandatory As Boolean = False

    Public Property TransNo() As String
        Get
            Return _TransNo
        End Get
        Set(ByVal value As String)
            _TransNo = value
        End Set
    End Property

    Public Property TransDate() As DateTime
        Get
            Return _TransDate
        End Get
        Set(ByVal value As DateTime)
            _TransDate = value
        End Set
    End Property

    Public Property ShowSOQty() As Boolean
        Get
            Return _ShowSOQty
        End Get
        Set(ByVal value As Boolean)
            _ShowSOQty = value
        End Set
    End Property

    Public Property ShowCSADOQty() As Boolean
        Get
            Return _ShowCSADOQty
        End Get
        Set(ByVal value As Boolean)
            _ShowCSADOQty = value
        End Set
    End Property

    Public Property ShowPOQty() As Boolean
        Get
            Return _ShowPOQty
        End Get
        Set(ByVal value As Boolean)
            _ShowPOQty = value
        End Set
    End Property

    Public Property ItemCode() As String
        Get
            Return _ICode
        End Get
        Set(ByVal value As String)
            _ICode = value
        End Set
    End Property
    Public Property ItemName() As String
        Get
            Return _IName
        End Get
        Set(ByVal value As String)
            _IName = value
        End Set
    End Property
    Public Property LocationCode() As String
        Get
            Return _LCode
        End Get
        Set(ByVal value As String)
            _LCode = value
        End Set
    End Property
    Public Property LocationName() As String
        Get
            Return _LName
        End Get
        Set(ByVal value As String)
            _LName = value
        End Set
    End Property
    Public Property UOM() As String
        Get
            Return _UOM
        End Get
        Set(ByVal value As String)
            _UOM = value
        End Set
    End Property
    Public Property ItemMRP() As Double
        Get
            Return _MRP
        End Get
        Set(ByVal value As Double)
            _MRP = value
        End Set
    End Property
    Public Property CommitedQty() As Boolean
        Get
            Return _Visible
        End Get
        Set(ByVal value1 As Boolean)
            _Visible = value1
            btnDrillDown.Visible = _Visible
        End Set
    End Property
    Public Property CommitedQtyLbl() As Boolean
        Get
            Return _LblVisible
        End Get
        Set(ByVal value2 As Boolean)
            _LblVisible = value2
            Label4.Visible = _LblVisible
        End Set
    End Property
    Public Sub RefreshData(Optional ByVal trans As SqlTransaction = Nothing)
        RadGroupBox1.Text = ""
        lblLocationName.Text = _LName
        lblUOM.Text = _UOM
        lblMrp.Text = "0"
        lblBalance.Text = "0"
        Label4.Text = "0"
        Label7.Text = "0"
        _IsMRPMandatory = False
        OrderHeader.Visible = False
        lblOrderQty.Visible = False
        btnOrder_Qty.Visible = False
        HeaderMRP.Visible = False
        lblMrp.Visible = False

        If clsCommon.myCstr(_ICode).Contains("'") Then
            _ICode = clsCommon.myCstr(_ICode.Replace("'", ""))
        End If
        If clsCommon.myLen(_ICode) > 0 Then
            RadGroupBox1.Text = _ICode + " ( " + _IName + " )"
            Dim qry As String = "select Is_MRP  from TSPL_ITEM_MASTER where Item_Code='" + _ICode + "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) = 1 Then
                Dim IsMRPWiseBalance As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsMRPWiseBalance, clsFixedParameterCode.IsMRPWiseBalance, trans)) > 0, True, False)
                If IsMRPWiseBalance Then
                    _IsMRPMandatory = True
                End If
            End If
            qry = getBaseQryForOther()
            If _MRP > 0 AndAlso _IsMRPMandatory Then
                HeaderMRP.Visible = True
                lblMrp.Visible = True
                lblMrp.Text = clsCommon.myFormat(_MRP)
            End If
            qry = "select ICode,SUM(Qty * case when TransType='' then 1 else 0 end)as BalanceQty,SUM(Qty * case when TransType='' then 0 else 1 end)as CommitQty,SUM(Qty *RI )as ActualBalanceQty from (" + qry + ")FinalQry group by ICode"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                lblBalance.Text = clsCommon.myFormat(dt.Rows(0)("BalanceQty"))
                Label4.Text = clsCommon.myFormat(dt.Rows(0)("CommitQty"))
                Label7.Text = clsCommon.myFormat(dt.Rows(0)("ActualBalanceQty"))
            End If

            If _ShowPOQty OrElse _ShowSOQty OrElse _ShowCSADOQty Then
                OrderHeader.Visible = True
                lblOrderQty.Visible = True
                btnOrder_Qty.Visible = True
                If _ShowPOQty Then
                    qry = "select Sum(Qty) as Qty from  (" + getBaseQryForSales() + ") as final"
                ElseIf _ShowSOQty Then
                    qry = "select Sum(Qty) as Qty from(" + getBaseQryForSales() + ") as final"
                ElseIf _ShowCSADOQty Then
                    qry = "select Sum(Qty) as Qty from(" + getBaseQryForSales() + ") as final "
                End If
                lblOrderQty.Text = clsCommon.myFormat(clsDBFuncationality.getSingleValue(qry, trans))
            End If
        End If
    End Sub

    Private Sub btnDrillDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrillDown.Click

        'objCommonVar.ScreenToOpen = clsUserMgtCode.frmBalanceSheetPerforma
        'objCommonVar.ScreenToOpenUOM = _UOM
        'objCommonVar.ScreenToOpenIsMRPMandatory = _IsMRPMandatory
        'objCommonVar.ScreenToOpen_Text = "Commited Quantity"
        'objCommonVar.ScreenToOpenQry = "select TransType,TransCode,DocNo,ICode,Location,Qty  as Qty ,0 as MRP from (" + getBaseQryForOther() + ")FinalQry  where TransType<> ''"

        Dim frm As New frmBalanceQty()
        frm.strUOM = _UOM
        frm.IsMRPMandatory = _IsMRPMandatory
        frm._METEXT = "Commited Quantity"
        'If _IsMRPMandatory Then
        frm.qry = "select TransType,TransCode,DocNo,ICode,Location,Qty  as Qty ,0 as MRP from (" + getBaseQryForOther() + ")FinalQry  where TransType<> ''"
        'End If
        frm.Show()
    End Sub

    Public Function getBaseQryForOther() As String

        Dim qry As String = clsItemLocationDetails.getBaseQryForItemBalanceDuringTransaction(_ICode, _UOM, _LCode, _TransDate, _TransNo, _IsMRPMandatory, _MRP, Nothing)
        'qry=clsInventoryMovement.
        'Dim IsItemWithDifferntUnitConsiderAsOtherItem As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsItemWithDifferntUnitConsiderAsOtherItem, clsFixedParameterCode.IsItemWithDifferntUnitConsiderAsOtherItem, Nothing)) > 0, True, False)
        'Dim qry As String = "select  xx.TransType,xx.TransCode,xx.DocNo, xx.ICode,xx.Location,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,( (xx.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) as Qty" + Environment.NewLine
        'qry += " from (" + Environment.NewLine
        'qry += " select '' as TransType,'' as TransCode,'' as DocNo, Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM  from("
        'qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew from("
        'qry += " select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,TSPL_INVENTORY_MOVEMENT.Stock_Qty as Qty   ,TSPL_INVENTORY_MOVEMENT.Stock_UOM as UOMNew "
        'qry += " from TSPL_INVENTORY_MOVEMENT "
        'qry += " where TSPL_INVENTORY_MOVEMENT.Qty<>0 and TSPL_INVENTORY_MOVEMENT.Item_Code='" + _ICode + "'"
        'If clsCommon.myLen(_LCode) > 0 Then
        '    qry += "  and Location_Code='" + _LCode + "'"
        'End If
        'If _IsMRPMandatory AndAlso _MRP > 0 Then
        '    qry += " and TSPL_INVENTORY_MOVEMENT.MRP='" + clsCommon.myCstr(_MRP) + "' "
        'End If

        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_INVENTORY_MOVEMENT.UOM='" + _UOM + "' "
        'End If

        'Dim intSettingType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, Nothing))

        'If intSettingType = 1 Then
        '    qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
        'ElseIf intSettingType = 0 Then
        '    qry += " and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "'"
        'End If
        'qry += " )xxx  "
        'qry += " )xxxx group by Item_Code,Location_Code,UOMNew "

        ''If Not intSettingType = 2 Then
        'qry += " union all " + Environment.NewLine
        'qry += " select '' as TransType,'' as TransCode,'' as DocNo, Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM  from("
        'qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew from("
        'qry += " select TSPL_inventory_Movement_New.Trans_Id, TSPL_inventory_Movement_New.Item_Code ,TSPL_inventory_Movement_New.Location_Code , TSPL_inventory_Movement_New.InOut,TSPL_inventory_Movement_New.Stock_Qty as Qty   ,TSPL_inventory_Movement_New.Stock_UOM as UOMNew "
        'qry += " from TSPL_inventory_Movement_New "
        'qry += " where TSPL_inventory_Movement_New.Qty<>0 and TSPL_inventory_Movement_New.Item_Code='" + _ICode + "'"
        'If clsCommon.myLen(_LCode) > 0 Then
        '    qry += "  and Location_Code='" + _LCode + "'"
        'End If
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
        'qry += " )xxx  "
        'qry += " )xxxx group by Item_Code,Location_Code,UOMNew "

        ''If Not intSettingType = 2 Then
        'qry += " union all " + Environment.NewLine

        'qry += " select 'Purchase Return' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.mbtnPurchaseReturn) + "' as TransCode,TSPL_PR_HEAD.PR_No as DocNo, TSPL_PR_DETAIL.Item_Code as ICode,case when TSPL_PR_HEAD.is_Reject_Item=1 then TSPL_LOCATION_MASTER.Rejected_Location else  TSPL_PR_DETAIL.Location end as Locaion,TSPL_PR_DETAIL.PR_Qty as Qty,-1 as RI,TSPL_PR_DETAIL.Unit_code AS Uom "
        'qry += " from TSPL_PR_DETAIL "
        'qry += " left outer join TSPL_PR_HEAD on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No"
        'qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PR_HEAD.Bill_To_Location "
        'qry += " where TSPL_PR_HEAD.Status=0 and TSPL_PR_DETAIL.Item_Code='" + _ICode + "' "
        'If clsCommon.myLen(_LCode) > 0 Then
        '    qry += " and (case when TSPL_PR_HEAD.is_Reject_Item=1 then TSPL_LOCATION_MASTER.Rejected_Location else  TSPL_PR_DETAIL.Location end)='" + _LCode + "'"
        'End If
        'If _IsMRPMandatory AndAlso _MRP > 0 Then
        '    qry += " and TSPL_PR_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
        'End If
        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_PR_DETAIL.Unit_code='" + _UOM + "' "
        'End If
        'qry += " and TSPL_PR_DETAIL.PR_Qty<>0  "
        'qry += " and TSPL_PR_DETAIL.PR_No not in ('" + _TransNo + "')"

        'qry += " union all " + Environment.NewLine

        'qry += " select 'IC-AD' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.mbtnStoreAdjustment) + "' as TransCode,TSPL_ADJUSTMENT_HEADER.Adjustment_No as DocNo, TSPL_ADJUSTMENT_DETAIL.Item_Code as ICode,TSPL_ADJUSTMENT_HEADER.Loc_Code as Locaion,TSPL_ADJUSTMENT_DETAIL.Item_Quantity as Qty,-1 as RI,TSPL_ADJUSTMENT_DETAIL.Unit_Code AS Uom "
        'qry += " from TSPL_ADJUSTMENT_DETAIL "
        'qry += " left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No"
        'qry += " where TSPL_ADJUSTMENT_HEADER.Posted='N' and TSPL_ADJUSTMENT_DETAIL.Item_Code='" + _ICode + "'"
        'If clsCommon.myLen(_LCode) > 0 Then
        '    qry += " and TSPL_ADJUSTMENT_HEADER.Loc_Code='" + _LCode + "' "
        'End If
        'If _IsMRPMandatory AndAlso _MRP > 0 Then
        '    qry += " and TSPL_ADJUSTMENT_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
        'End If
        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_ADJUSTMENT_DETAIL.Unit_Code='" + _UOM + "' "
        'End If
        'qry += " and TSPL_ADJUSTMENT_DETAIL.Item_Quantity<>0  and TSPL_ADJUSTMENT_DETAIL.Adjustment_Type in ('BD','QD') and TSPL_ADJUSTMENT_HEADER.Adjustment_No not in ('" + _TransNo + "')"

        'qry += " union all " + Environment.NewLine

        'qry += " select 'RGP' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.mbtnGatePass) + "' as TransCode,TSPL_RGP_HEAD.RGP_No as DocNo, TSPL_RGP_DETAIL.Item_Code as ICode,TSPL_RGP_HEAD.Location as Locaion,TSPL_RGP_DETAIL.RGP_Qty as Qty,-1 as RI,TSPL_RGP_DETAIL.Unit_code AS Uom "
        'qry += " from TSPL_RGP_DETAIL "
        'qry += " left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No"
        'qry += " where TSPL_RGP_HEAD.Status=0 and TSPL_RGP_DETAIL.Item_Code='" + _ICode + "'"
        'If clsCommon.myLen(_LCode) > 0 Then
        '    qry += "  and TSPL_RGP_HEAD.Location='" + _LCode + "'"
        'End If
        ''If _IsMRPMandatory AndAlso _MRP > 0 Then
        ''    qry += " and TSPL_ADJUSTMENT_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
        ''End If
        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_RGP_DETAIL.Unit_code='" + _UOM + "' "
        'End If
        'qry += " and TSPL_RGP_DETAIL.RGP_Qty<>0  "
        'qry += " and TSPL_RGP_DETAIL.RGP_No not in ('" + _TransNo + "')"

        'qry += " union all " + Environment.NewLine

        'qry += " select 'Scrap' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.ScrapSale) + "' as TransCode,TSPL_SCRAPSALE_HEAD.shipment_No as DocNo, TSPL_SCRAPSALE_DETAIL.Item_Code as ICode,TSPL_SCRAPSALE_HEAD.Loc_Code as Locaion,TSPL_SCRAPSALE_DETAIL.shipped_Qty as Qty,-1 as RI,TSPL_SCRAPSALE_DETAIL.Unit_code AS Uom "
        'qry += " from TSPL_SCRAPSALE_DETAIL "
        'qry += " left outer join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No"
        'qry += " where TSPL_SCRAPSALE_HEAD.IsPost=0 and TSPL_SCRAPSALE_DETAIL.Item_Code='" + _ICode + "'"
        'If clsCommon.myLen(_LCode) > 0 Then
        '    qry += "  and TSPL_SCRAPSALE_HEAD.Loc_Code='" + _LCode + "' "
        'End If
        ''If _IsMRPMandatory AndAlso _MRP > 0 Then
        ''    qry += " and TSPL_ADJUSTMENT_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
        ''End If
        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_SCRAPSALE_DETAIL.Unit_code='" + _UOM + "' "
        'End If
        'qry += " and TSPL_SCRAPSALE_DETAIL.shipped_Qty<>0 and TSPL_SCRAPSALE_DETAIL.shipment_No not in ('" + _TransNo + "')"

        'qry += "  union all " + Environment.NewLine

        'qry += " select 'Issue/Return/Transfer' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.mbtnIssueReturn) + "' as TransCode,TSPL_IssueReturn_HEAD.Doc_No as DocNo, TSPL_IssueReturn_DETAIL.Item_Code as ICode,TSPL_IssueReturn_HEAD.From_Location as Locaion,TSPL_IssueReturn_DETAIL.Issued_Qty as Qty,-1 as RI,TSPL_IssueReturn_DETAIL.Unit_code AS Uom "
        'qry += " from TSPL_IssueReturn_DETAIL "
        'qry += " left outer join TSPL_IssueReturn_HEAD on TSPL_IssueReturn_HEAD.Doc_No=TSPL_IssueReturn_DETAIL.Doc_No"
        'qry += " where TSPL_IssueReturn_HEAD.Status=0 and TSPL_IssueReturn_DETAIL.Item_Code='" + _ICode + "' "
        'If clsCommon.myLen(_LCode) > 0 Then
        '    qry += " and TSPL_IssueReturn_HEAD.From_Location='" + _LCode + "' "
        'End If
        ''If _IsMRPMandatory AndAlso _MRP > 0 Then
        ''    qry += " and TSPL_ADJUSTMENT_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
        ''End If
        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_IssueReturn_DETAIL.Unit_code='" + _UOM + "' "
        'End If
        'qry += " and TSPL_IssueReturn_DETAIL.Issued_Qty<>0 and TSPL_IssueReturn_DETAIL.Doc_No not in ('" + _TransNo + "') " + Environment.NewLine

        'qry += "  union all " + Environment.NewLine
        'qry += "  select  'SaleOrder' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmSNSalesOrder) + "' as TransCode,TSPL_SD_SALES_ORDER_HEAD.Document_Code as DocNo, TSPL_SD_SALES_ORDER_DETAIL.Item_Code as ICode,TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location as Locaion,(TSPL_SD_SALES_ORDER_DETAIL.CommitedQty)-isnull(TSPL_SD_SHIPMENT_DETAIL.qty,0)  as Qty,-1 as RI,TSPL_SD_SALES_ORDER_DETAIL.Unit_code AS Uom  "
        'qry += " from TSPL_SD_SALES_ORDER_DETAIL"
        'qry += " left outer join TSPL_SD_SALES_ORDER_HEAD on TSPL_SD_SALES_ORDER_HEAD.Document_Code=TSPL_SD_SALES_ORDER_DETAIL.DOCUMENT_CODE "
        'qry += " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order  =TSPL_SD_SALES_ORDER_HEAD.DOCUMENT_CODE "
        'qry += " left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE =TSPL_SD_SHIPMENT_HEAD.Document_Code"
        'qry += " where TSPL_SD_SALES_ORDER_DETAIL.Item_Code='" + _ICode + "'  "


        'If clsCommon.myLen(_LCode) > 0 Then
        '    qry += " and TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location='" + _LCode + "'  "
        'End If
        'If _IsMRPMandatory AndAlso _MRP > 0 Then
        '    qry += " and TSPL_SD_SALES_ORDER_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
        'End If
        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_SD_SALES_ORDER_DETAIL.Unit_code='" + _UOM + "' "
        'End If

        'qry += " and TSPL_SD_SALES_ORDER_DETAIL.CommitedQty>0 and TSPL_SD_SALES_ORDER_DETAIL.DOCUMENT_CODE not  in('" + _TransNo + "') "


        'qry += "  union all " + Environment.NewLine
        'qry += " select  'Shipment' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmShipmentProductSale) + "' as TransCode,TSPL_SD_SHIPMENT_HEAD.Document_Code as DocNo, TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location as Locaion,TSPL_SD_SHIPMENT_DETAIL.Qty as Qty,-1 as RI,TSPL_SD_SHIPMENT_DETAIL.Unit_code AS Uom  "
        'qry += " from TSPL_SD_SHIPMENT_DETAIL "
        'qry += " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE"
        'qry += " where TSPL_SD_SHIPMENT_HEAD.Status=0 and TSPL_SD_SHIPMENT_DETAIL.Item_Code='" + _ICode + "'"
        'If clsCommon.myLen(_LCode) > 0 Then
        '    qry += " and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" + _LCode + "'  "
        'End If
        'If _IsMRPMandatory AndAlso _MRP > 0 Then
        '    qry += " and TSPL_SD_SHIPMENT_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
        'End If
        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_SD_SHIPMENT_DETAIL.Unit_code='" + _UOM + "' "
        'End If
        'qry += " and TSPL_SD_SHIPMENT_DETAIL.Qty<>0 and TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE not in ('" + _TransNo + "')"

        ' '' query for assemblies and disassemblies
        'qry += " union all " + Environment.NewLine
        'qry += " select 'Assemblies' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmAssemblies) + "' as TransCode,TSPL_PJC_ASSEMBLIES.CODE as DocNo, Main_Item_Code as ICode,LOCATION_CODE as Location,QUANTITY,(case when TRANSACTION_TYPE='Assembly' then 1  else -1 end) as RI,"
        'qry += " BUILD_ITEM_UNIT_CODE as UnitCode from TSPL_PJC_ASSEMBLIES where TSPL_PJC_ASSEMBLIES.POSTED=0 and  TSPL_PJC_ASSEMBLIES.Main_Item_Code='" + _ICode + "'  and TSPL_PJC_ASSEMBLIES.CODE  not in ('" + _TransNo + "')"
        'If clsCommon.myLen(_LCode) > 0 Then
        '    qry += " and TSPL_PJC_ASSEMBLIES.LOCATION_CODE='" & _LCode & "'"
        'End If
        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and BUILD_ITEM_UNIT_CODE='" + _UOM + "' "
        'End If
        ''If _IsMRPMandatory AndAlso _MRP > 0 Then
        ''    qry += " and TSPL_SD_SHIPMENT_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
        ''End If
        'qry += " union all  "

        'qry += " select  'Assemblies' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmAssemblies) + "' as TransCode,TSPL_PJC_ASSEMBLIES.CODE as DocNo, TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE AS ICode,TSPL_PJC_ASSEMBLIES.LOCATION_CODE as Location,"
        'qry += " TSPL_MF_BOM_DETAIL.CONSM_QUANTITY*(TSPL_PJC_ASSEMBLIES.QUANTITY/TSPL_PJC_ASSEMBLIES.BUILD_QUANTITY) as Qty,"
        'qry += " (case when TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly' then  -1 else  1 end) AS RI,"
        'qry += " TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE as UnitCode from TSPL_PJC_ASSEMBLIES "
        'qry += " inner join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_PJC_ASSEMBLIES.BOM_CODE"
        'qry += " inner JOIN TSPL_MF_BOM_DETAIL ON TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_MF_BOM_DETAIL.BOM_CODE "
        'qry += " where TSPL_PJC_ASSEMBLIES.POSTED=0 and TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE='" + _ICode + "'  and TSPL_PJC_ASSEMBLIES.CODE  not in ('" + _TransNo + "')"
        'If clsCommon.myLen(_LCode) > 0 Then
        '    qry += " and TSPL_PJC_ASSEMBLIES.LOCATION_CODE='" & _LCode & "'"
        'End If
        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE='" + _UOM + "' "
        'End If
        ''If _IsMRPMandatory AndAlso _MRP > 0 Then
        ''    qry += " and TSPL_SD_SHIPMENT_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
        ''End If

        ''End If

        ''=============CSA DO==================== will not refelect the balance of stock  balwinder on 16/03/2015 
        ''qry += " Union all " + Environment.NewLine
        ''qry += "select  'CSA DO' as TransType,'" + clsCommon.myCstr(EnumTransType.SDCSADO) + "' as TransCode,TSPL_CSA_DO_DETAIL.Doc_No as DocNo, TSPL_CSA_DO_DETAIL.Item_Code as ICode,TSPL_CSA_DO_HEAD.From_Location_Code as Locaion,TSPL_CSA_DO_DETAIL.Qty as Qty,-1 as RI,TSPL_CSA_DO_DETAIL.UOM AS Uom   from TSPL_CSA_DO_DETAIL  left outer join TSPL_CSA_DO_HEAD on TSPL_CSA_DO_HEAD.Doc_No=TSPL_CSA_DO_DETAIL.Doc_No "
        ''qry += " where TSPL_CSA_DO_HEAD.Is_Post='1' and TSPL_CSA_DO_DETAIL.Item_Code='" + _ICode + "' and TSPL_CSA_DO_DETAIL.Qty<>0 and TSPL_CSA_DO_DETAIL.Doc_No not in ('" + _TransNo + "')"
        ''If clsCommon.myLen(_LCode) > 0 Then
        ''    qry += " and TSPL_CSA_DO_HEAD.From_Location_Code='" + _LCode + "'"
        ''End If
        ''If IsItemWithDifferntUnitConsiderAsOtherItem Then
        ''    qry += " and TSPL_CSA_DO_DETAIL.UOM='" + _UOM + "' "
        ''End If
        ''=======================================
        'qry += " union all " + Environment.NewLine
        'qry += " select 'Production Issue' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmProcessProductionIssueEntry) + "' as TransCode,TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code as DocNo, TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code as ICode,TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code as Location,TSPL_PP_ISSUE_ITEM_DETAIL.Qty as QUANTITY,-1 as RI,"
        'qry += " TSPL_PP_ISSUE_ITEM_DETAIL.Unit_Code as UnitCode from TSPL_PP_ISSUE_ITEM_DETAIL left outer join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code where TSPL_PP_ISSUE_HEAD.Is_post=0 and  TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code='" + _ICode + "'  and TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code  not in ('" + _TransNo + "')"
        'If clsCommon.myLen(_LCode) > 0 Then
        '    qry += " and TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code='" & _LCode & "'"
        'End If
        'If IsItemWithDifferntUnitConsiderAsOtherItem Then
        '    qry += " and TSPL_PP_ISSUE_ITEM_DETAIL.Unit_Code='" + _UOM + "' "
        'End If

        'qry += " )xx" + Environment.NewLine
        'qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM"
        'qry += "  left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode and FinalUOM.UOM_Code='" + _UOM + "'"
        Return qry
    End Function

    Private Sub btnOrder_Qty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrder_Qty.Click
        'objCommonVar.ScreenToOpen = clsUserMgtCode.frmBalanceSheetPerforma
        'objCommonVar.ScreenToOpenUOM = _UOM
        'objCommonVar.ScreenToOpenIsMRPMandatory = _IsMRPMandatory
        'objCommonVar.ScreenToOpen_Text = "Order Quantity"
        'objCommonVar.ScreenToOpenQry = "select TransType,TransCode,DocNo,ICode,Location,Qty  as Qty ,0 as MRP from (" + getBaseQryForOther() + ")FinalQry where TransType<> ''"

        Dim frm As New frmBalanceQty()
        frm.strUOM = _UOM
        frm.IsMRPMandatory = _IsMRPMandatory
        frm._METEXT = "Order Quantity"
        'If _IsMRPMandatory Then
        frm.qry = "select TransType,TransCode,DocNo,ICode,Location,Qty  as Qty ,0 as MRP from (" + getBaseQryForSales() + ")FinalQry where TransType<> ''"
        'End If
        frm.Show()
    End Sub
    Public Function getBaseQryForSales() As String
        Dim qry As String = Nothing
        If clsCommon.myLen(_ICode) > 0 Then
            RadGroupBox1.Text = _ICode + " ( " + _IName + " )"
            qry = "select Is_MRP  from TSPL_ITEM_MASTER where Item_Code='" + _ICode + "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 1 Then
                Dim IsMRPWiseBalance As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsMRPWiseBalance, clsFixedParameterCode.IsMRPWiseBalance, Nothing)) > 0, True, False)
                If IsMRPWiseBalance Then
                    _IsMRPMandatory = True
                End If
            End If
            qry = getBaseQryForOther()
            If _MRP > 0 AndAlso _IsMRPMandatory Then
                HeaderMRP.Visible = True
                lblMrp.Visible = True
                lblMrp.Text = clsCommon.myFormat(_MRP)
            End If
            qry = "select ICode,SUM(Qty * case when TransType='' then 1 else 0 end)as BalanceQty,SUM(Qty * case when TransType='' then 0 else 1 end)as CommitQty,SUM(Qty *RI )as ActualBalanceQty from (" + qry + ")FinalQry group by ICode"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                lblBalance.Text = clsCommon.myFormat(dt.Rows(0)("BalanceQty"))
                Label4.Text = clsCommon.myFormat(dt.Rows(0)("CommitQty"))
                Label7.Text = clsCommon.myFormat(dt.Rows(0)("ActualBalanceQty"))
            End If

            If _ShowPOQty OrElse _ShowSOQty OrElse _ShowCSADOQty Then
                OrderHeader.Visible = True
                lblOrderQty.Visible = True
                btnOrder_Qty.Visible = True
                If _ShowPOQty Then
                    qry = "select 'Purchase Order' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.mbtnPurchaseOrder) + "' as TransCode,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No  as DocNo, TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_PURCHASE_ORDER_DETAIL.Location,((PurchaseOrder_Qty* case when TSPL_ITEM_UOM_DETAIL.Conversion_Factor=0 then 1 else TSPL_ITEM_UOM_DETAIL.Conversion_Factor end)/(case when FinalConvert.Conversion_Factor=0 then 1 else FinalConvert.Conversion_Factor end) ) as Qty from TSPL_PURCHASE_ORDER_DETAIL "
                    qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_PURCHASE_ORDER_DETAIL.Unit_code"
                    qry += " left outer join TSPL_ITEM_UOM_DETAIL as FinalConvert on FinalConvert.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code and FinalConvert.UOM_Code='" + _UOM + "' "
                    qry += " where TSPL_PURCHASE_ORDER_DETAIL.Item_Code='" + _ICode + "'"
                    If clsCommon.myLen(_LCode) > 0 Then
                        qry += " and TSPL_PURCHASE_ORDER_DETAIL.Location='" + _LCode + "' "
                    End If
                    qry += " and not exists (select 1 from TSPL_SRN_DETAIL where TSPL_SRN_DETAIL.PO_ID=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No and TSPL_SRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code)"
                    If _IsMRPMandatory AndAlso _MRP > 0 Then
                        qry += " and TSPL_PURCHASE_ORDER_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "'"
                    End If
                ElseIf _ShowSOQty Then
                    qry = "select 'Sale Order' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmSNSalesOrder) + "' as TransCode,TSPL_SD_SALES_ORDER_DETAIL.Document_Code as DocNo, TSPL_SD_SALES_ORDER_DETAIL.Item_Code as ICode,TSPL_SD_SALES_ORDER_DETAIL.Location,(  (Qty*case when TSPL_ITEM_UOM_DETAIL.Conversion_Factor=0 then 1 else TSPL_ITEM_UOM_DETAIL.Conversion_Factor end)/(case when FinalConvert.Conversion_Factor=0 then 1 else FinalConvert.Conversion_Factor end)  ) as Qty from TSPL_SD_SALES_ORDER_DETAIL "
                    qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALES_ORDER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_SALES_ORDER_DETAIL.Unit_code"
                    qry += " left outer join TSPL_ITEM_UOM_DETAIL as FinalConvert on FinalConvert.Item_Code=TSPL_SD_SALES_ORDER_DETAIL.Item_Code and FinalConvert.UOM_Code='" + _UOM + "' "
                    qry += " where TSPL_SD_SALES_ORDER_DETAIL.Item_Code='" + _ICode + "'"
                    If clsCommon.myLen(_LCode) > 0 Then
                        qry += " and TSPL_SD_SALES_ORDER_DETAIL.Location='" + _LCode + "'"
                    End If
                    qry += " and not exists (select 1 from TSPL_SD_SHIPMENT_DETAIL where TSPL_SD_SHIPMENT_DETAIL.Order_Code=TSPL_SD_SALES_ORDER_DETAIL.Document_Code and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_SD_SALES_ORDER_DETAIL.Item_Code)"
                    If _IsMRPMandatory AndAlso _MRP > 0 Then
                        qry += " and TSPL_SD_SALES_ORDER_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "'"
                    End If
                ElseIf _ShowCSADOQty Then
                    qry = "select 'CSA' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmCSASaleInvoice) + "' as TransCode,TSPL_CSA_DO_DETAIL.Doc_No  as DocNo, TSPL_CSA_DO_DETAIL.Item_Code as ICode,TSPL_CSA_DO_HEAD.from_Location_code as Location,(  (Qty*case when TSPL_ITEM_UOM_DETAIL.Conversion_Factor=0 then 1 else TSPL_ITEM_UOM_DETAIL.Conversion_Factor end)/(case when FinalConvert.Conversion_Factor=0 then 1 else FinalConvert.Conversion_Factor end)  ) as Qty from TSPL_CSA_DO_DETAIL "
                    qry += " left join TSPL_CSA_DO_HEAD on TSPL_CSA_DO_HEAD.Doc_No = TSPL_CSA_DO_DETAIL.Doc_No "
                    qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_CSA_DO_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_CSA_DO_DETAIL.UOM"
                    qry += " left outer join TSPL_ITEM_UOM_DETAIL as FinalConvert on FinalConvert.Item_Code=TSPL_CSA_DO_DETAIL.Item_Code and FinalConvert.UOM_Code='" + _UOM + "' "
                    qry += " where TSPL_CSA_DO_DETAIL.Item_Code='" + _ICode + "'"
                    If clsCommon.myLen(_LCode) > 0 Then
                        qry += " and TSPL_CSA_DO_DETAIL.doc_no in (select doc_no from TSPL_CSA_DO_HEAD where from_Location_code='" + _LCode + "' and doc_no<>'" + _TransNo + "')"
                    End If
                End If

            End If
        End If
        Return qry
    End Function
End Class
