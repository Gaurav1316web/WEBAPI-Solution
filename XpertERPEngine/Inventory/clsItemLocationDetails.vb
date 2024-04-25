Imports common
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class clsItemLocationDetails
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Location_Code As String = Nothing
    Public Location_Desc As String = Nothing
    Public Item_Qty As Double = 0
    Public Amount As Double = 0
    Public MRP As Double = 0
    Public MFG_Date As Date? = Nothing
    Public Batch_No As String = Nothing
    Public Expiry_Date As Date? = Nothing
    Public ItemType As String = Nothing





    Public Shared Function SaveData(ByVal strPostDate As String, ByVal Arr As List(Of clsItemLocationDetails), ByVal trans As SqlTransaction) As Boolean
        Return True
    End Function

    Public Shared Function getBalanceWithUnapprove(ByVal strICode As String, ByVal strLocation As String, ByVal strMRP As String, ByVal strUOM As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As DateTime) As Double
        '' done by Panch Raj to call same base query for each balance function
        Dim qry As String = getBaseQryForItemBalanceDuringTransaction(strICode, strUOM, strLocation, dtDocumentDate, strDocumentNo, True, clsCommon.myCdbl(strMRP), Nothing)
        qry = "select (case when max(Minimum_Balance) is null then  ROUND(sum(qty*RI),2) else (case when ROUND(max(Minimum_Balance),2)>ROUND(sum(qty*RI),2) then ROUND(sum(qty*RI),2) else ROUND(max(Minimum_Balance),2) end)  end)  as Qty from (" & qry & ") Final"

        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function
    Public Shared Function getBalanceWithUnapproveForRMOther(ByVal strICode As String, ByVal strLocation As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As DateTime, ByVal trans As SqlTransaction, ByVal strUOM As String) As Double
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStock, clsFixedParameterType.AllowNegativeStock, trans)) > 0 Then
            Return clsCommon.myCdbl(clsFixedParameter.GetSpecification(clsFixedParameterType.AllowNegativeStock, clsFixedParameterType.AllowNegativeStock, trans))
        End If
        '' done by Panch Raj to call same base query for each balance function
        Dim qry As String = getBaseQryForItemBalanceDuringTransaction(strICode, strUOM, strLocation, dtDocumentDate, strDocumentNo, False, 0, trans)
        qry = "select (case when max(Minimum_Balance) is null then  ROUND(sum(qty*RI),2) else (case when ROUND(max(Minimum_Balance),2)>ROUND(sum(qty*RI),2) then ROUND(sum(qty*RI),2) else ROUND(max(Minimum_Balance),2) end)  end)  as Qty from (" & qry & ") Final"

        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function getBalance(ByVal strICode As String, ByVal strLocation As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As DateTime, ByVal trans As SqlTransaction, ByVal strUOM As String, ByVal dblMRP As Double) As Double
        Dim arr As List(Of String) = Nothing

        '==
        arr = New List(Of String)

        If clsCommon.myLen(strDocumentNo) > 0 Then
            arr = New List(Of String)
            arr.Add(strDocumentNo)
        End If

        Return getBalance(strICode, strLocation, arr, dtDocumentDate, trans, strUOM, dblMRP)
    End Function
    Public Shared Function getBalance(ByVal strICode As String, ByVal strLocation As String, ByVal arrExculudeDocNo As List(Of String), ByVal dtDocumentDate As DateTime, ByVal trans As SqlTransaction, ByVal strUOM As String, ByVal dblMRP As Double) As Double

        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStock, clsFixedParameterType.AllowNegativeStock, trans)) > 0 Then
            Return clsCommon.myCdbl(clsFixedParameter.GetSpecification(clsFixedParameterType.AllowNegativeStock, clsFixedParameterType.AllowNegativeStock, trans))
        End If

        Dim strDocumentNo As String = "''"
        If arrExculudeDocNo IsNot Nothing AndAlso arrExculudeDocNo.Count > 0 Then
            'strDocumentNo = clsCommon.GetMulcallStringWithComma(arrExculudeDocNo)
            strDocumentNo = clsCommon.GetMulcallString(arrExculudeDocNo)
            strDocumentNo = strDocumentNo.Remove(0, 1)
            strDocumentNo = strDocumentNo.Remove(strDocumentNo.Length - 1, 1)
        End If
        '' done by Panch Raj to call same base query for each balance function
        Dim IsMRPWiseBalance As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsMRPWiseBalance, clsFixedParameterCode.IsMRPWiseBalance, trans)) > 0, True, False)
        Dim qry As String = getBaseQryForItemBalanceDuringTransaction(strICode, strUOM, strLocation, dtDocumentDate, strDocumentNo, IsMRPWiseBalance, dblMRP, trans)
        qry = "select (case when max(Minimum_Balance) is null then  ROUND(sum(qty*RI),2) else (case when ROUND(max(Minimum_Balance),2)>ROUND(sum(qty*RI),2) then ROUND(sum(qty*RI),2) else ROUND(max(Minimum_Balance),2) end)  end)  as Qty from (" & qry & ") Final"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function getBalanceWithUnapproveEmpty(ByVal strICode As String, ByVal strLocation As String, ByVal strMRP As String, ByVal strUOM As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As DateTime) As Double
        Return getBalanceWithUnapproveEmpty(strICode, strLocation, strMRP, strUOM, strDocumentNo, dtDocumentDate, Nothing)
    End Function
    Public Shared Function getBalanceWithUnapproveEmpty(ByVal strICode As String, ByVal strLocation As String, ByVal strMRP As String, ByVal strUOM As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As DateTime, ByVal trans As SqlTransaction) As Double
        '' done by Panch Raj to call same base query for each balance function
        Dim qry As String = getBaseQryForItemBalanceDuringTransaction(strICode, strUOM, strLocation, dtDocumentDate, strDocumentNo, True, clsCommon.myCdbl(strMRP), trans)
        qry = "select (case when max(Minimum_Balance) is null then  ROUND(sum(qty*RI),2) else (case when ROUND(max(Minimum_Balance),2)>ROUND(sum(qty*RI),2) then ROUND(sum(qty*RI),2) else ROUND(max(Minimum_Balance),2) end)  end)  as Qty from (" & qry & ") Final"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

    End Function

    Public Shared Function getBalanceWithUnapproveForRMOtherforFinder(ByVal strICode As String, ByVal strLocation As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As DateTime, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select SUM(qty*RI) as Qty,MAX(ICode) as ICode from(" + Environment.NewLine
        qry += " select xx.ICode,xx.Location, xx.Qty as OldQty,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,xx.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as Qty" + Environment.NewLine
        qry += " from (" + Environment.NewLine

        'qry += " select TSPL_ITEM_LOCATION_DETAILS.Item_Code as ICode,Location_Code as Location,Item_Qty as Qty,1 as RI, TSPL_ITEM_UOM_DETAIL.UOM_Code"
        'qry += " from TSPL_ITEM_LOCATION_DETAILS "
        'qry += " inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_LOCATION_DETAILS.Item_Code and TSPL_ITEM_UOM_DETAIL.Conversion_Factor=1"
        'qry += " where TSPL_ITEM_LOCATION_DETAILS.Item_Code='" + strICode + "' and Location_Code='" + strLocation + "' and Item_Qty<>0  "

        qry += " select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,max(UOMNew) as UOM_Code from("
        qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew from("
        qry += " select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,TSPL_INVENTORY_MOVEMENT.Qty   ,TSPL_INVENTORY_MOVEMENT.UOM as UOMNew "
        qry += " from TSPL_INVENTORY_MOVEMENT "
        qry += " where TSPL_INVENTORY_MOVEMENT.Qty<>0  and Location_Code='" + strLocation + "' "
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, Nothing)) = 1 Then
            qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
        Else
            qry += " and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "'"
        End If
        qry += " )xxx  "
        qry += " )xxxx group by Item_Code,Location_Code "

        qry += " union all " + Environment.NewLine
        qry += " select TSPL_PR_DETAIL.Item_Code as ICode,TSPL_PR_DETAIL.Location as Locaion,TSPL_PR_DETAIL.PR_Qty as Qty,-1 as RI,TSPL_PR_DETAIL.Unit_code AS Uom "
        qry += " from TSPL_PR_DETAIL "
        qry += " left outer join TSPL_PR_HEAD on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No"
        qry += " where TSPL_PR_HEAD.Status=0  and TSPL_PR_DETAIL.Location='" + strLocation + "' and TSPL_PR_DETAIL.PR_Qty<>0  "
        qry += " and TSPL_PR_DETAIL.PR_No not in ('" + strDocumentNo + "')"
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_ADJUSTMENT_DETAIL.Item_Code as ICode,TSPL_ADJUSTMENT_HEADER.Loc_Code as Locaion,TSPL_ADJUSTMENT_DETAIL.Item_Quantity as Qty,-1 as RI,TSPL_ADJUSTMENT_DETAIL.Unit_Code AS Uom "
        qry += " from TSPL_ADJUSTMENT_DETAIL "
        qry += " left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No"
        qry += " where TSPL_ADJUSTMENT_HEADER.Posted='N' and TSPL_ADJUSTMENT_HEADER.Loc_Code='" + strLocation + "' and TSPL_ADJUSTMENT_DETAIL.Item_Quantity<>0  and TSPL_ADJUSTMENT_DETAIL.Adjustment_Type in ('BD','QD') and TSPL_ADJUSTMENT_HEADER.Adjustment_No not in ('" + strDocumentNo + "')"
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_RGP_DETAIL.Item_Code as ICode,TSPL_RGP_HEAD.Location as Locaion,TSPL_RGP_DETAIL.RGP_Qty as Qty,-1 as RI,TSPL_RGP_DETAIL.Unit_code AS Uom "
        qry += " from TSPL_RGP_DETAIL "
        qry += " left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No"
        qry += " where TSPL_RGP_HEAD.Status=0  and TSPL_RGP_HEAD.Location='" + strLocation + "' and TSPL_RGP_DETAIL.RGP_Qty<>0  "
        qry += " and TSPL_RGP_DETAIL.RGP_No not in ('" + strDocumentNo + "')"
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_SCRAPSALE_DETAIL.Item_Code as ICode,TSPL_SCRAPSALE_HEAD.Loc_Code as Locaion,TSPL_SCRAPSALE_DETAIL.shipped_Qty as Qty,-1 as RI,TSPL_SCRAPSALE_DETAIL.Unit_code AS Uom "
        qry += " from TSPL_SCRAPSALE_DETAIL "
        qry += " left outer join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No"
        qry += " where TSPL_SCRAPSALE_HEAD.IsPost=0  and TSPL_SCRAPSALE_HEAD.Loc_Code='" + strLocation + "' "
        qry += " and TSPL_SCRAPSALE_DETAIL.shipped_Qty<>0 and TSPL_SCRAPSALE_DETAIL.shipment_No not in ('" + strDocumentNo + "')"
        qry += "  union all " + Environment.NewLine
        qry += " select TSPL_IssueReturn_DETAIL.Item_Code as ICode,TSPL_IssueReturn_HEAD.From_Location as Locaion,TSPL_IssueReturn_DETAIL.Issued_Qty as Qty,-1 as RI,TSPL_IssueReturn_DETAIL.Unit_code AS Uom "
        qry += " from TSPL_IssueReturn_DETAIL "
        qry += " left outer join TSPL_IssueReturn_HEAD on TSPL_IssueReturn_HEAD.Doc_No=TSPL_IssueReturn_DETAIL.Doc_No"
        qry += " where TSPL_IssueReturn_HEAD.Status=0  and TSPL_IssueReturn_HEAD.From_Location='" + strLocation + "' "
        qry += " and TSPL_IssueReturn_DETAIL.Issued_Qty<>0 and TSPL_IssueReturn_DETAIL.Doc_No not in ('" + strDocumentNo + "') " + Environment.NewLine
        If objCommonVar.IsDemoERP Then
            qry += "  union all " + Environment.NewLine
            qry += " select TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location as Locaion,TSPL_SD_SHIPMENT_DETAIL.Qty as Qty,-1 as RI,TSPL_SD_SHIPMENT_DETAIL.Unit_code AS Uom  "
            qry += " from TSPL_SD_SHIPMENT_DETAIL "
            qry += " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE"
            qry += " where TSPL_SD_SHIPMENT_HEAD.Status=0  and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" + strLocation + "'  and TSPL_SD_SHIPMENT_DETAIL.Qty<>0 and TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE not in ('" + strDocumentNo + "')"
        End If
        qry += " )xx" + Environment.NewLine
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=xx.ICode where Item_Type <> 'F' "
        qry += " )xxx group by ICode,Location"
        Return qry
    End Function
    Public Shared Function getBaseQryForItemBalanceDuringTransaction(ByVal _ICode As String, ByVal _UOM As String, ByVal _LCode As String, ByVal _TransDate As Date, ByVal _TransNo As String, ByVal _IsMRPMandatory As Boolean, ByVal _MRP As Decimal, ByVal trans As SqlTransaction) As String
        Dim IsItemWithDifferntUnitConsiderAsOtherItem As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsItemWithDifferntUnitConsiderAsOtherItem, clsFixedParameterCode.IsItemWithDifferntUnitConsiderAsOtherItem, trans)) > 0, True, False)
        Dim qry As String = "select  xx.TransType,xx.TransCode,xx.DocNo, xx.ICode,xx.Location,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,((xx.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) as Qty,((Minimum_Bal.Minimum_Balance*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) as Minimum_Balance " + Environment.NewLine
        qry += " from (" + Environment.NewLine
        qry += " select '' as TransType,'' as TransCode,'' as DocNo, Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM  from("
        qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew from("
        qry += " select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,TSPL_INVENTORY_MOVEMENT.Stock_Qty as Qty   ,TSPL_INVENTORY_MOVEMENT.Stock_UOM as UOMNew "
        qry += " from TSPL_INVENTORY_MOVEMENT "
        qry += " where TSPL_INVENTORY_MOVEMENT.Qty<>0 and TSPL_INVENTORY_MOVEMENT.Item_Code='" + _ICode + "' AND PUNCHING_DAte  <= '" + clsCommon.GetPrintDate(_TransDate, "dd/MMM/yyyy hh:mm:ss tt") + "' "
        If clsCommon.myLen(_LCode) > 0 Then
            qry += "  and Location_Code='" + _LCode + "'"
        End If
        If _IsMRPMandatory AndAlso _MRP > 0 Then
            qry += " and TSPL_INVENTORY_MOVEMENT.MRP='" + clsCommon.myCstr(_MRP) + "' " 'clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy hh:mm:ss tt")
        End If

        If IsItemWithDifferntUnitConsiderAsOtherItem Then
            qry += " and TSPL_INVENTORY_MOVEMENT.UOM='" + _UOM + "' "
        End If

        Dim intSettingType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, trans))
        Dim qryMinBal As String = "select null as Item_Code,null as Location_Code,null as Minimum_Balance"
        If intSettingType = 1 Then
            'qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
            qryMinBal = " select Item_Code,Location_Code,min(Closing_Balance) as Minimum_Balance from (" &
                        " select Item_Code,Location_Code,cast(Punching_Date as date) as Punching_Date,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*Stock_Qty)) over(order by cast(Punching_Date as date)) as Closing_Balance " &
                        " from TSPL_INVENTORY_MOVEMENT where Item_Code='" & _ICode & "' AND Location_Code='" & _LCode & "' " &
                        " group by cast(Punching_Date as date),Item_Code,Location_Code " &
                        " union all " &
                        " select Item_Code,Location_Code,cast(Punching_Date as date) as Punching_Date,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*Stock_Qty)) over(order by cast(Punching_Date as date)) as Closing_Balance " &
                        " from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" & _ICode & "' AND Location_Code='" & _LCode & "' " &
                        " group by cast(Punching_Date as date),Item_Code,Location_Code) as MinimumQry where Punching_Date>'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "' " &
                        " group by Item_Code,Location_Code "
            qry += " and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "'"
        ElseIf intSettingType = 0 Then
            qry += " and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "'"
        End If
        qry += " )xxx  "
        qry += " )xxxx group by Item_Code,Location_Code,UOMNew "
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ConsiderUnpostedDocForBalance, clsFixedParameterCode.ConsiderUnpostedDocForBalance, trans)) > 0 Then
            qry += " union all " + Environment.NewLine
            qry += " select '' as TransType,'' as TransCode,'' as DocNo, Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM  from("
            qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew from("
            qry += " select TSPL_inventory_Movement_New.Trans_Id, TSPL_inventory_Movement_New.Item_Code ,TSPL_inventory_Movement_New.Location_Code , TSPL_inventory_Movement_New.InOut,case when Custom_UOM='" + _UOM + "' and Custom_Coversion_Factor>0 then cast(Stock_Qty /Custom_Coversion_Factor as decimal(18,2)) else TSPL_inventory_Movement_New.Stock_Qty end as Qty,case when Custom_UOM='" + _UOM + "' and Custom_Coversion_Factor>0 then Custom_UOM else  TSPL_inventory_Movement_New.Stock_UOM end as UOMNew"
            qry += " from TSPL_inventory_Movement_New "
            qry += " where TSPL_inventory_Movement_New.Qty<>0 and TSPL_inventory_Movement_New.Item_Code='" + _ICode + "'"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += "  and Location_Code='" + _LCode + "'"
            End If
            If _IsMRPMandatory AndAlso _MRP > 0 Then
                qry += " and TSPL_inventory_Movement_New.MRP='" + clsCommon.myCstr(_MRP) + "' "
            End If

            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_inventory_Movement_New.UOM='" + _UOM + "' "
            End If

            If intSettingType = 1 Then
                qry += " and 2=(case when TSPL_inventory_Movement_New.InOut='O' then 2 else case when TSPL_inventory_Movement_New.InOut='I' and TSPL_inventory_Movement_New.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
            ElseIf intSettingType = 0 Then
                qry += " and TSPL_inventory_Movement_New.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "'"
            End If
            qry += " )xxx  "
            qry += " )xxxx group by Item_Code,Location_Code,UOMNew "

            qry += " union all " + Environment.NewLine

            qry += " select 'Purchase Return' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.mbtnPurchaseReturn) + "' as TransCode,TSPL_PR_HEAD.PR_No as DocNo, TSPL_PR_DETAIL.Item_Code as ICode,case when TSPL_PR_HEAD.is_Reject_Item=1 then TSPL_LOCATION_MASTER.Rejected_Location else  TSPL_PR_DETAIL.Location end as Locaion,TSPL_PR_DETAIL.PR_Qty as Qty,-1 as RI,TSPL_PR_DETAIL.Unit_code AS Uom "
            qry += " from TSPL_PR_DETAIL "
            qry += " left outer join TSPL_PR_HEAD on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No"
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PR_HEAD.Bill_To_Location "
            qry += " where TSPL_PR_HEAD.Status=0 and TSPL_PR_DETAIL.Item_Code='" + _ICode + "' "
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and (case when TSPL_PR_HEAD.is_Reject_Item=1 then TSPL_LOCATION_MASTER.Rejected_Location else  TSPL_PR_DETAIL.Location end)='" + _LCode + "'"
            End If
            If _IsMRPMandatory AndAlso _MRP > 0 Then
                qry += " and TSPL_PR_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_PR_DETAIL.Unit_code='" + _UOM + "' "
            End If
            qry += " and TSPL_PR_DETAIL.PR_Qty<>0  "
            qry += " and TSPL_PR_DETAIL.PR_No not in ('" + _TransNo + "')"

            qry += " union all " + Environment.NewLine

            qry += " select 'IC-AD' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.mbtnStoreAdjustment) + "' as TransCode,TSPL_ADJUSTMENT_HEADER.Adjustment_No as DocNo, TSPL_ADJUSTMENT_DETAIL.Item_Code as ICode,TSPL_ADJUSTMENT_HEADER.Loc_Code as Locaion,TSPL_ADJUSTMENT_DETAIL.Item_Quantity as Qty,-1 as RI,TSPL_ADJUSTMENT_DETAIL.Unit_Code AS Uom "
            qry += " from TSPL_ADJUSTMENT_DETAIL "
            qry += " left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No"
            qry += " where TSPL_ADJUSTMENT_HEADER.Posted='N' and TSPL_ADJUSTMENT_DETAIL.Item_Code='" + _ICode + "'"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_ADJUSTMENT_HEADER.Loc_Code='" + _LCode + "' "
            End If
            If _IsMRPMandatory AndAlso _MRP > 0 Then
                qry += " and TSPL_ADJUSTMENT_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_ADJUSTMENT_DETAIL.Unit_Code='" + _UOM + "' "
            End If
            qry += " and TSPL_ADJUSTMENT_DETAIL.Item_Quantity<>0  and TSPL_ADJUSTMENT_DETAIL.Adjustment_Type in ('BD','QD') and TSPL_ADJUSTMENT_HEADER.Adjustment_No not in ('" + _TransNo + "')"

            qry += " union all " + Environment.NewLine

            qry += " select 'RGP' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.mbtnGatePass) + "' as TransCode,TSPL_RGP_HEAD.RGP_No as DocNo, TSPL_RGP_DETAIL.Item_Code as ICode,TSPL_RGP_HEAD.Location as Locaion,TSPL_RGP_DETAIL.RGP_Qty as Qty,-1 as RI,TSPL_RGP_DETAIL.Unit_code AS Uom "
            qry += " from TSPL_RGP_DETAIL "
            qry += " left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No"
            qry += " where TSPL_RGP_HEAD.Status=0 and TSPL_RGP_DETAIL.Item_Code='" + _ICode + "'"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += "  and TSPL_RGP_HEAD.Location='" + _LCode + "'"
            End If

            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_RGP_DETAIL.Unit_code='" + _UOM + "' "
            End If
            qry += " and TSPL_RGP_DETAIL.RGP_Qty<>0  "
            qry += " and TSPL_RGP_DETAIL.RGP_No not in ('" + _TransNo + "')"

            qry += " union all " + Environment.NewLine

            qry += " select 'Scrap' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.ScrapSale) + "' as TransCode,TSPL_SCRAPSALE_HEAD.shipment_No as DocNo, TSPL_SCRAPSALE_DETAIL.Item_Code as ICode,TSPL_SCRAPSALE_HEAD.Loc_Code as Locaion,TSPL_SCRAPSALE_DETAIL.shipped_Qty as Qty,-1 as RI,TSPL_SCRAPSALE_DETAIL.Unit_code AS Uom "
            qry += " from TSPL_SCRAPSALE_DETAIL "
            qry += " left outer join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No"
            qry += " where TSPL_SCRAPSALE_HEAD.IsPost=0 and TSPL_SCRAPSALE_DETAIL.Item_Code='" + _ICode + "'"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += "  and TSPL_SCRAPSALE_HEAD.Loc_Code='" + _LCode + "' "
            End If

            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_SCRAPSALE_DETAIL.Unit_code='" + _UOM + "' "
            End If
            qry += " and TSPL_SCRAPSALE_DETAIL.shipped_Qty<>0 and TSPL_SCRAPSALE_DETAIL.shipment_No not in ('" + _TransNo + "')"

            qry += "  union all " + Environment.NewLine

            qry += " select 'Issue/Return/Transfer' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.mbtnIssueReturn) + "' as TransCode,TSPL_IssueReturn_HEAD.Doc_No as DocNo, TSPL_IssueReturn_DETAIL.Item_Code as ICode,TSPL_IssueReturn_HEAD.From_Location as Locaion,TSPL_IssueReturn_DETAIL.Issued_Qty as Qty,-1 as RI,TSPL_IssueReturn_DETAIL.Unit_code AS Uom "
            qry += " from TSPL_IssueReturn_DETAIL "
            qry += " left outer join TSPL_IssueReturn_HEAD on TSPL_IssueReturn_HEAD.Doc_No=TSPL_IssueReturn_DETAIL.Doc_No"
            qry += " where TSPL_IssueReturn_HEAD.Status=0 and TSPL_IssueReturn_DETAIL.Item_Code='" + _ICode + "' "
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_IssueReturn_HEAD.From_Location='" + _LCode + "' "
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_IssueReturn_DETAIL.Unit_code='" + _UOM + "' "
            End If
            qry += " and TSPL_IssueReturn_DETAIL.Issued_Qty<>0 and TSPL_IssueReturn_DETAIL.Doc_No not in ('" + _TransNo + "') " + Environment.NewLine

            qry += "  union all " + Environment.NewLine
            qry += "  select  'SaleOrder' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmSNSalesOrder) + "' as TransCode,TSPL_SD_SALES_ORDER_HEAD.Document_Code as DocNo, TSPL_SD_SALES_ORDER_DETAIL.Item_Code as ICode,TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location as Locaion,(TSPL_SD_SALES_ORDER_DETAIL.CommitedQty)-isnull(TSPL_SD_SHIPMENT_DETAIL.qty,0)  as Qty,-1 as RI,TSPL_SD_SALES_ORDER_DETAIL.Unit_code AS Uom  "
            qry += " from TSPL_SD_SALES_ORDER_DETAIL"
            qry += " left outer join TSPL_SD_SALES_ORDER_HEAD on TSPL_SD_SALES_ORDER_HEAD.Document_Code=TSPL_SD_SALES_ORDER_DETAIL.DOCUMENT_CODE "
            qry += " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order  =TSPL_SD_SALES_ORDER_HEAD.DOCUMENT_CODE "
            qry += " left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE =TSPL_SD_SHIPMENT_HEAD.Document_Code"
            qry += " where TSPL_SD_SALES_ORDER_DETAIL.Item_Code='" + _ICode + "'  "


            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location='" + _LCode + "'  "
            End If
            If _IsMRPMandatory AndAlso _MRP > 0 Then
                qry += " and TSPL_SD_SALES_ORDER_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_SD_SALES_ORDER_DETAIL.Unit_code='" + _UOM + "' "
            End If
            qry += " and TSPL_SD_SALES_ORDER_DETAIL.CommitedQty>0 and TSPL_SD_SALES_ORDER_DETAIL.DOCUMENT_CODE not  in('" + _TransNo + "') "
            qry += "  union all " + Environment.NewLine
            qry += " select  'Shipment' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmShipmentProductSale) + "' as TransCode,TSPL_SD_SHIPMENT_HEAD.Document_Code as DocNo, TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location as Locaion,TSPL_SD_SHIPMENT_DETAIL.Qty as Qty,-1 as RI,TSPL_SD_SHIPMENT_DETAIL.Unit_code AS Uom  "
            qry += " from TSPL_SD_SHIPMENT_DETAIL "
            qry += " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE"
            qry += " where TSPL_SD_SHIPMENT_HEAD.Status=0 and TSPL_SD_SHIPMENT_DETAIL.Item_Code='" + _ICode + "'"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" + _LCode + "'  "
            End If
            If _IsMRPMandatory AndAlso _MRP > 0 Then
                qry += " and TSPL_SD_SHIPMENT_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_SD_SHIPMENT_DETAIL.Unit_code='" + _UOM + "' "
            End If
            qry += " and TSPL_SD_SHIPMENT_DETAIL.Qty<>0 and TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE not in ('" + _TransNo + "')"
            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowStockCheckatDOLevel, clsFixedParameterCode.AllowStockCheckatDOLevel, trans)), "1") = CompairStringResult.Equal Then
                qry += " and TSPL_SD_SHIPMENT_HEAD.Trans_Type not in ( 'PS') " + Environment.NewLine +
                " union all " + Environment.NewLine +
                " select * from (" + Environment.NewLine +
                " select 'DeliveryOrderPS' as TransType,'DeliveryOrderPS' as TransCode,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code as DocNo, TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code as ICode,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Bill_To_Location as Locaion " + Environment.NewLine +
                ",case when isnull(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close,'N')='N' then TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Qty -isnull((select sum( TSPL_SD_SHIPMENT_DETAIL.qty) from TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code= TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_HEAD.Status=1 and TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS =TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.DOCUMENT_CODE and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code and TSPL_SD_SHIPMENT_DETAIL.Unit_code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Unit_code),0)" + Environment.NewLine +
                " else isnull((select sum( TSPL_SD_SHIPMENT_DETAIL.qty) from TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code= TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_HEAD.Status=0 and TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS =TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.DOCUMENT_CODE and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code and TSPL_SD_SHIPMENT_DETAIL.Unit_code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Unit_code),0)  end as Qty ,-1 as RI,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Unit_code AS Uom  " + Environment.NewLine +
                " from TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE " + Environment.NewLine +
                " left outer join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.DOCUMENT_CODE " + Environment.NewLine +
                " where TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code='" + _ICode + "'" + Environment.NewLine

                If clsCommon.myLen(_LCode) > 0 Then
                    qry += " and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Bill_To_Location='" + _LCode + "'  "
                End If
                If _IsMRPMandatory AndAlso _MRP > 0 Then
                    qry += " and TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.MRP='" + clsCommon.myCstr(_MRP) + "' "
                End If
                If IsItemWithDifferntUnitConsiderAsOtherItem Then
                    qry += " and TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Unit_code='" + _UOM + "' "
                End If
                qry += " and (TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.DOCUMENT_CODE not in ('" + _TransNo + "'))" + Environment.NewLine +
                " ) x where Qty>0 "
            End If


            ''can sale
            qry += " union all  " + Environment.NewLine

            qry += " select 'CanSale' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.FrmCanSale) + "' as TransCode,TSPL_CAN_SALE_HEAD.Document_No as DocNo,TSPL_CAN_SALE_HEAD.CanItemCode  as ICode," &
                " TSPL_CAN_SALE_HEAD.Location_Code as Locaion,TSPL_CAN_SALE_HEAD.TotalNoofCans ,-1 as RI,TSPL_CAN_SALE_HEAD.CanItemUOM AS Uom  from TSPL_CAN_SALE_DETAIL " &
                " left outer join TSPL_CAN_SALE_HEAD on TSPL_CAN_SALE_HEAD.Document_No=TSPL_CAN_SALE_DETAIL.Document_No " &
                " where TSPL_CAN_SALE_HEAD.Posted=0 and TSPL_CAN_SALE_HEAD.CanItemCode='" + _ICode + "' and TSPL_CAN_SALE_HEAD.Document_No not in ('" + _TransNo + "') and TSPL_CAN_SALE_HEAD.TotalNoofCans <>0 "
            If clsCommon.myLen(_LCode) > 0 Then
                qry += "  and TSPL_CAN_SALE_HEAD.Location_Code='" & _LCode & "'"
            End If

            ''SILO MILK TRANSFER BHA/03/08/18-000389 richa
            qry += " union all " + Environment.NewLine
            qry += " select 'Silo_MTR' as TransType,'Silo_MTR' as TransCode,TSPL_SILO_MILK_TRANSFER_HEAD.Document_Code as DocNo,TSPL_SILO_MILK_TRANSFER_DETAIL.Item_Code  as ICode,TSPL_SILO_MILK_TRANSFER_DETAIL.Silo_Code as Locaion,TSPL_SILO_MILK_TRANSFER_DETAIL.Qty,-1 as RI,TSPL_SILO_MILK_TRANSFER_DETAIL.UOM AS Uom " &
            " from TSPL_SILO_MILK_TRANSFER_DETAIL " &
            " left outer join TSPL_SILO_MILK_TRANSFER_HEAD on TSPL_SILO_MILK_TRANSFER_HEAD.Document_Code =TSPL_SILO_MILK_TRANSFER_DETAIL.Document_Code " &
            " where TSPL_SILO_MILK_TRANSFER_HEAD.Posted=0 and TSPL_SILO_MILK_TRANSFER_DETAIL.Item_Code='" + _ICode + "' and TSPL_SILO_MILK_TRANSFER_DETAIL.Qty<>0  " &
            " and TSPL_SILO_MILK_TRANSFER_DETAIL.Document_Code not in ('" + _TransNo + "')"

            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_SILO_MILK_TRANSFER_DETAIL.Silo_Code='" + _LCode + "' "
            End If

            ''---------

            ''-----
            '' query for assemblies and disassemblies
            qry += " union all " + Environment.NewLine
            qry += " select 'Assemblies' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmAssemblies) + "' as TransCode,TSPL_PJC_ASSEMBLIES.CODE as DocNo, Main_Item_Code as ICode,LOCATION_CODE as Location,QUANTITY,(case when TRANSACTION_TYPE='Assembly' then 1  else -1 end) as RI,"
            qry += " BUILD_ITEM_UNIT_CODE as UnitCode from TSPL_PJC_ASSEMBLIES where TSPL_PJC_ASSEMBLIES.POSTED=0 and  TSPL_PJC_ASSEMBLIES.Main_Item_Code='" + _ICode + "'  and TSPL_PJC_ASSEMBLIES.CODE  not in ('" + _TransNo + "')"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_PJC_ASSEMBLIES.LOCATION_CODE='" & _LCode & "'"
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and BUILD_ITEM_UNIT_CODE='" + _UOM + "' "
            End If

            qry += " union all  "

            qry += " select  'Assemblies' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmAssemblies) + "' as TransCode,TSPL_PJC_ASSEMBLIES.CODE as DocNo, TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE AS ICode,TSPL_PJC_ASSEMBLIES.LOCATION_CODE as Location,"
            qry += " TSPL_MF_BOM_DETAIL.CONSM_QUANTITY*(TSPL_PJC_ASSEMBLIES.QUANTITY/TSPL_PJC_ASSEMBLIES.BUILD_QUANTITY) as Qty,"
            qry += " (case when TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly' then  -1 else  1 end) AS RI,"
            qry += " TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE as UnitCode from TSPL_PJC_ASSEMBLIES "
            qry += " inner join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_PJC_ASSEMBLIES.BOM_CODE"
            qry += " inner JOIN TSPL_MF_BOM_DETAIL ON TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_MF_BOM_DETAIL.BOM_CODE "
            qry += " where TSPL_PJC_ASSEMBLIES.POSTED=0 and TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE='" + _ICode + "'  and TSPL_PJC_ASSEMBLIES.CODE  not in ('" + _TransNo + "')"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_PJC_ASSEMBLIES.LOCATION_CODE='" & _LCode & "'"
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE='" + _UOM + "' "
            End If

            qry += " union all " + Environment.NewLine
            qry += " select 'Dis-Assemblies' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmAssemblies) + "' as TransCode,TSPL_PROD_ASSEMBLIES.CODE as DocNo, Main_Item_Code as ICode,LOCATION_CODE as Location,QUANTITY,(case when TRANSACTION_TYPE='Assembly' then 1  else -1 end) as RI,"
            qry += " BUILD_ITEM_UNIT_CODE as UnitCode from TSPL_PROD_ASSEMBLIES where TSPL_PROD_ASSEMBLIES.POSTED=0 and  TSPL_PROD_ASSEMBLIES.Main_Item_Code='" + _ICode + "'  and TSPL_PROD_ASSEMBLIES.CODE  not in ('" + _TransNo + "')"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_PROD_ASSEMBLIES.LOCATION_CODE='" & _LCode & "'"
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and BUILD_ITEM_UNIT_CODE='" + _UOM + "' "
            End If

            qry += " union all  "

            qry += " select  'Assemblies' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmAssemblies) + "' as TransCode,TSPL_PJC_ASSEMBLIES.CODE as DocNo, TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE AS ICode,TSPL_PJC_ASSEMBLIES.LOCATION_CODE as Location,"
            qry += " TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_QUANTITY*(TSPL_PJC_ASSEMBLIES.QUANTITY/TSPL_PJC_ASSEMBLIES.BUILD_QUANTITY) as Qty,"
            qry += " (case when TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly' then  -1 else  1 end) AS RI,"
            qry += " TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_UNIT_CODE as UnitCode from TSPL_PJC_ASSEMBLIES "
            qry += " inner JOIN TSPL_PROD_ASSEMBLIES_ITEM_DETAIL ON TSPL_PJC_ASSEMBLIES.CODE=TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.ASSEMBLY_CODE "
            qry += " where TSPL_PJC_ASSEMBLIES.POSTED=0 and TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE='" + _ICode + "'  and TSPL_PJC_ASSEMBLIES.CODE  not in ('" + _TransNo + "')"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_PJC_ASSEMBLIES.LOCATION_CODE='" & _LCode & "'"
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_UNIT_CODE='" + _UOM + "' "
            End If


            qry += " union all  "

            qry += " select  'Wreckage' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmAssemblies) + "' as TransCode,TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE as DocNo, TSPL_WRECKAGE_BOOKING.Item_Code AS ICode,TSPL_WRECKAGE_ENTRY.LOCATION_CODE as Location,"
            qry += " TSPL_WRECKAGE_BOOKING.WRECKAGE_QTY as Qty, -1 AS RI,"
            qry += " TSPL_WRECKAGE_BOOKING.Unit_Code as UnitCode from TSPL_WRECKAGE_ENTRY "
            qry += " inner JOIN TSPL_WRECKAGE_BOOKING ON TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE=TSPL_WRECKAGE_BOOKING.WRECKAGE_CODE "
            qry += " where TSPL_WRECKAGE_ENTRY.POSTED=0 and TSPL_WRECKAGE_BOOKING.ITEM_CODE='" + _ICode + "'  and TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE  not in ('" + _TransNo + "')"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_WRECKAGE_ENTRY.LOCATION_CODE='" & _LCode & "'"
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_WRECKAGE_BOOKING.Unit_Code='" + _UOM + "' "
            End If

            qry += " union all " + Environment.NewLine
            qry += " select 'Production Issue' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmProcessProductionIssueEntry) + "' as TransCode,TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code as DocNo, TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code as ICode,TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code as Location,TSPL_PP_ISSUE_ITEM_DETAIL.Qty as QUANTITY,-1 as RI,"
            qry += " TSPL_PP_ISSUE_ITEM_DETAIL.Unit_Code as UnitCode from TSPL_PP_ISSUE_ITEM_DETAIL left outer join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code where TSPL_PP_ISSUE_HEAD.Is_post=0 and  TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code='" + _ICode + "'  and TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code  not in ('" + _TransNo + "')"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code='" & _LCode & "'"
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_PP_ISSUE_ITEM_DETAIL.Unit_Code='" + _UOM + "' "
            End If
            '' query for add/remove items durng Process production Standardization
            qry += " union all " + Environment.NewLine
            qry += " select 'Production Standardization' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmProcessProductionStandardization) + "' as TransCode,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code as Doc_No,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Item_Code as ICode,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Loaction_Code as Location,"
            qry += " TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY,"
            qry += " (case when TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then 1 else  -1  end)as RI,"
            qry += " TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE from TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL "
            qry += " inner join TSPL_PP_STANDARDIZATION_HEAD on TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code = TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code "
            qry += " where TSPL_PP_STANDARDIZATION_HEAD.Posted=0 and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Item_Code='" + _ICode + "' "
            qry += " and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code not in ('" + _TransNo + "')"
            qry += " and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Loaction_Code='" & _LCode & "' and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Remove' "
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE='" + _UOM + "' "
            End If
            '' query for add/remove items durng Process production STAGE PROCESS
            qry += " union all " + Environment.NewLine
            qry += " select 'Production Stage Process' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmProcessProductionStageProcess) + "' as TransCode,TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE as Doc_No, TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code as ICode,TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Loaction_Code as Location,"
            qry += " TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY,"
            qry += " (case when TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then 1 else  -1  end)as RI,"
            qry += " TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE from TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL "
            qry += " inner join TSPL_PP_STAGE_PROCESS_HEAD on TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE = TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE "
            qry += " where TSPL_PP_STAGE_PROCESS_HEAD.Posted=0 and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code='" + _ICode + "' "
            qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE not in ('" + _TransNo + "')"
            qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Loaction_Code='" & _LCode & "' and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Remove'"
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE='" + _UOM + "' "
            End If
            ''For CSA Transfer
            qry += " union all " + Environment.NewLine +
            " select 'CSA Transfer' as TransType,'SD-CSATRANS' as TransCode,TSPL_CSA_TRANSFER_HEAD.DOC_CODE as Doc_No, TSPL_CSA_TRANSFER_DETAIL.Item_Code as ICode,TSPL_CSA_TRANSFER_HEAD.From_Location_Code as Location, TSPL_CSA_TRANSFER_DETAIL.Qty, -1 as RI, TSPL_CSA_TRANSFER_DETAIL.Unit_code as Uom " + Environment.NewLine +
            " from TSPL_CSA_TRANSFER_DETAIL " + Environment.NewLine +
            " left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DOC_CODE=TSPL_CSA_TRANSFER_DETAIL.DOC_CODE " + Environment.NewLine +
            " where TSPL_CSA_TRANSFER_HEAD.Status=0 and TSPL_CSA_TRANSFER_DETAIL.Item_Code='" + _ICode + "'  and TSPL_CSA_TRANSFER_HEAD.DOC_CODE not in ('" + _TransNo + "') "
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_CSA_TRANSFER_HEAD.From_Location_Code='" + _LCode + "'  "
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_CSA_TRANSFER_DETAIL.Unit_code ='" + _UOM + "'  "
            End If
            If _IsMRPMandatory AndAlso _MRP > 0 Then
                qry += " and TSPL_CSA_TRANSFER_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "'"
            End If

            qry += " union all " + Environment.NewLine
            qry += " select 'Milk Jobwork' as Trans_Type,'" + clsCommon.myCstr(clsUserMgtCode.frmMilkJobWorkTransfer) + "' as Trans_Code,JWD.Document_Code as Doc_No,JWD.Item_Code as ICode,JW.Loc_Code,JWD.Net_Weight,-1 AS RI," &
                   " JWD.UOM  from TSPL_MILK_JOBWORK_TRANSFER_DETAILS JWD inner join TSPL_MILK_JOBWORK_TRANSFER_HEAD JW on JWD.Document_Code=JW.Document_Code " &
                   " where  JW.isPosted=0 AND JWD.Item_Code='" + _ICode + "' and JW.Loc_Code='" & _LCode & "' AND JWD.Document_Code NOT IN ('" + _TransNo + "') "
            qry += " union all " + Environment.NewLine
            qry += " select 'Milk Jobwork Other' as Trans_Type,'" + clsCommon.myCstr(clsUserMgtCode.frmMilkJobWorkTransferOther) + "' as Trans_Code,TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TRANSFER_NO as Doc_No,TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Item_Code as ICode,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.From_Locaction,TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Qty,-1 AS RI," &
                   " TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.UOM  from TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS inner join TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD on TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_NO=TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TRANSFER_NO " &
                   " where  TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Status=0 AND TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Item_Code='" + _ICode + "' and TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.From_Locaction='" & _LCode & "'  AND TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TRANSFER_NO NOT IN ('" + _TransNo + "') "
            qry += " union all " + Environment.NewLine
            qry += " select 'Production Return' as Trans_Type,'" & clsCommon.myCstr(clsUserMgtCode.frmProcessProdReturn) & "' as Trans_Code,PER.PROD_RETURN_CODE as Doc_No,PED.ITEM_CODE as ICode,PEH.LOCATION_CODE," &
                   " PED.FINAL_PRODUCTION_QTY,-1 as RI,PED.UNIT_CODE from TSPL_PP_PRODUCTION_ENTRY_DETAIL PED " &
                   " inner join TSPL_PP_PRODUCTION_ENTRY PEH on PED.PROD_ENTRY_CODE=PEH.PROD_ENTRY_CODE " &
                   " inner join TSPL_PP_PRODUCTION_RETURN PER ON PEH.PROD_ENTRY_CODE=PER.PROD_ENTRY_CODE " &
                   " WHERE PER.POSTED=0 AND PED.ITEM_CODE='" + _ICode + "' and PEH.LOCATION_CODE='" & _LCode & "' AND PER.PROD_RETURN_CODE NOT IN ('" + _TransNo + "') "
        End If
        qry += " )xx" + Environment.NewLine &
               " left join (" & qryMinBal & ") as Minimum_Bal on xx.ICode=Minimum_Bal.Item_Code and xx.Location=Minimum_Bal.Location_Code "
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM "
        qry += " left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode and FinalUOM.UOM_Code='" + _UOM + "'"
        Return qry
    End Function
    Public Shared Function getBalance1(ByVal strICode As String, ByVal strLocation As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As DateTime, ByVal trans As SqlTransaction, ByVal strUOM As String, ByVal dblMRP As Double) As Double
        Dim arr As List(Of String) = Nothing

        '==
        arr = New List(Of String)

        If clsCommon.myLen(strDocumentNo) > 0 Then
            arr = New List(Of String)
            arr.Add(strDocumentNo)
        End If

        Return getBalance1(strICode, strLocation, arr, dtDocumentDate, trans, strUOM, dblMRP)
    End Function
    Public Shared Function getBalance1(ByVal strICode As String, ByVal strLocation As String, ByVal arrExculudeDocNo As List(Of String), ByVal dtDocumentDate As DateTime, ByVal trans As SqlTransaction, ByVal strUOM As String, ByVal dblMRP As Double) As Double

        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStock, clsFixedParameterType.AllowNegativeStock, trans)) > 0 Then
            Return clsCommon.myCdbl(clsFixedParameter.GetSpecification(clsFixedParameterType.AllowNegativeStock, clsFixedParameterType.AllowNegativeStock, trans))
        End If

        Dim strDocumentNo As String = "''"
        If arrExculudeDocNo IsNot Nothing AndAlso arrExculudeDocNo.Count > 0 Then
            'strDocumentNo = clsCommon.GetMulcallStringWithComma(arrExculudeDocNo)
            strDocumentNo = clsCommon.GetMulcallString(arrExculudeDocNo)
            strDocumentNo = strDocumentNo.Remove(0, 1)
            strDocumentNo = strDocumentNo.Remove(strDocumentNo.Length - 1, 1)
        End If
        '' done by Panch Raj to call same base query for each balance function
        Dim IsMRPWiseBalance As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsMRPWiseBalance, clsFixedParameterCode.IsMRPWiseBalance, trans)) > 0, True, False)
        Dim qry As String = getBaseQryForItemBalanceDuringTransaction1(strICode, strUOM, strLocation, dtDocumentDate, strDocumentNo, IsMRPWiseBalance, dblMRP, trans)
        qry = "select (case when max(Minimum_Balance) is null then  ROUND(sum(qty*RI),2) else (case when ROUND(max(Minimum_Balance),2)>ROUND(sum(qty*RI),2) then ROUND(sum(qty*RI),2) else ROUND(max(Minimum_Balance),2) end)  end)  as Qty from (" & qry & ") Final"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function getBaseQryForItemBalanceDuringTransaction1(ByVal _ICode As String, ByVal _UOM As String, ByVal _LCode As String, ByVal _TransDate As Date, ByVal _TransNo As String, ByVal _IsMRPMandatory As Boolean, ByVal _MRP As Decimal, ByVal trans As SqlTransaction) As String
        Dim IsItemWithDifferntUnitConsiderAsOtherItem As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsItemWithDifferntUnitConsiderAsOtherItem, clsFixedParameterCode.IsItemWithDifferntUnitConsiderAsOtherItem, trans)) > 0, True, False)
        Dim qry As String = "select  xx.TransType,xx.TransCode,xx.DocNo, xx.ICode,xx.Location,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,((xx.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) as Qty,((Minimum_Bal.Minimum_Balance*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) as Minimum_Balance " + Environment.NewLine
        qry += " from (" + Environment.NewLine
        qry += " select '' as TransType,'' as TransCode,'' as DocNo, Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM  from("
        qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew from("
        qry += " select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,TSPL_INVENTORY_MOVEMENT.Stock_Qty as Qty   ,TSPL_INVENTORY_MOVEMENT.Stock_UOM as UOMNew "
        qry += " from TSPL_INVENTORY_MOVEMENT "
        qry += " where TSPL_INVENTORY_MOVEMENT.Qty<>0 and TSPL_INVENTORY_MOVEMENT.Item_Code='" + _ICode + "' AND PUNCHING_DAte  <= '" + clsCommon.GetPrintDate(_TransDate, "dd/MMM/yyyy hh:mm:ss tt") + "' "
        If clsCommon.myLen(_LCode) > 0 Then
            qry += "  and Location_Code='" + _LCode + "'"
        End If
        If _IsMRPMandatory AndAlso _MRP > 0 Then
            qry += " and TSPL_INVENTORY_MOVEMENT.MRP='" + clsCommon.myCstr(_MRP) + "' " 'clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy hh:mm:ss tt")
        End If

        If IsItemWithDifferntUnitConsiderAsOtherItem Then
            qry += " and TSPL_INVENTORY_MOVEMENT.UOM='" + _UOM + "' "
        End If

        Dim intSettingType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, trans))
        Dim qryMinBal As String = "select null as Item_Code,null as Location_Code,null as Minimum_Balance"
        If intSettingType = 1 Then
            'qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
            qryMinBal = " select Item_Code,Location_Code,min(Closing_Balance) as Minimum_Balance from (" &
                        " select Item_Code,Location_Code,cast(Punching_Date as date) as Punching_Date,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*Stock_Qty)) over(order by cast(Punching_Date as date)) as Closing_Balance " &
                        " from TSPL_INVENTORY_MOVEMENT where Item_Code='" & _ICode & "' AND Location_Code='" & _LCode & "' " &
                        " group by cast(Punching_Date as date),Item_Code,Location_Code " &
                        " union all " &
                        " select Item_Code,Location_Code,cast(Punching_Date as date) as Punching_Date,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*Stock_Qty)) over(order by cast(Punching_Date as date)) as Closing_Balance " &
                        " from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" & _ICode & "' AND Location_Code='" & _LCode & "' " &
                        " group by cast(Punching_Date as date),Item_Code,Location_Code) as MinimumQry where Punching_Date>'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "' " &
                        " group by Item_Code,Location_Code "
            qry += " and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "'"
        ElseIf intSettingType = 0 Then
            qry += " and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "'"
        End If
        qry += " )xxx  "
        qry += " )xxxx group by Item_Code,Location_Code,UOMNew "
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ConsiderUnpostedDocForBalance, clsFixedParameterCode.ConsiderUnpostedDocForBalance, trans)) > 0 Then
            qry += " union all " + Environment.NewLine
            qry += " select '' as TransType,'' as TransCode,'' as DocNo, Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM  from("
            qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew from("
            qry += " select TSPL_inventory_Movement_New.Trans_Id, TSPL_inventory_Movement_New.Item_Code ,TSPL_inventory_Movement_New.Location_Code , TSPL_inventory_Movement_New.InOut,case when Custom_UOM='" + _UOM + "' and Custom_Coversion_Factor>0 then cast(Stock_Qty /Custom_Coversion_Factor as decimal(18,2)) else TSPL_inventory_Movement_New.Stock_Qty end as Qty,case when Custom_UOM='" + _UOM + "' and Custom_Coversion_Factor>0 then Custom_UOM else  TSPL_inventory_Movement_New.Stock_UOM end as UOMNew"
            qry += " from TSPL_inventory_Movement_New "
            qry += " where TSPL_inventory_Movement_New.Qty<>0 and TSPL_inventory_Movement_New.Item_Code='" + _ICode + "'"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += "  and Location_Code='" + _LCode + "'"
            End If
            If _IsMRPMandatory AndAlso _MRP > 0 Then
                qry += " and TSPL_inventory_Movement_New.MRP='" + clsCommon.myCstr(_MRP) + "' "
            End If

            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_inventory_Movement_New.UOM='" + _UOM + "' "
            End If

            If intSettingType = 1 Then
                qry += " and 2=(case when TSPL_inventory_Movement_New.InOut='O' then 2 else case when TSPL_inventory_Movement_New.InOut='I' and TSPL_inventory_Movement_New.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
            ElseIf intSettingType = 0 Then
                qry += " and TSPL_inventory_Movement_New.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "'"
            End If
            qry += " )xxx  "
            qry += " )xxxx group by Item_Code,Location_Code,UOMNew "

            qry += " union all " + Environment.NewLine

            qry += " select 'Purchase Return' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.mbtnPurchaseReturn) + "' as TransCode,TSPL_PR_HEAD.PR_No as DocNo, TSPL_PR_DETAIL.Item_Code as ICode,case when TSPL_PR_HEAD.is_Reject_Item=1 then TSPL_LOCATION_MASTER.Rejected_Location else  TSPL_PR_DETAIL.Location end as Locaion,TSPL_PR_DETAIL.PR_Qty as Qty,-1 as RI,TSPL_PR_DETAIL.Unit_code AS Uom "
            qry += " from TSPL_PR_DETAIL "
            qry += " left outer join TSPL_PR_HEAD on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No"
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PR_HEAD.Bill_To_Location "
            qry += " where TSPL_PR_HEAD.Status=0 and TSPL_PR_DETAIL.Item_Code='" + _ICode + "' "
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and (case when TSPL_PR_HEAD.is_Reject_Item=1 then TSPL_LOCATION_MASTER.Rejected_Location else  TSPL_PR_DETAIL.Location end)='" + _LCode + "'"
            End If
            If _IsMRPMandatory AndAlso _MRP > 0 Then
                qry += " and TSPL_PR_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_PR_DETAIL.Unit_code='" + _UOM + "' "
            End If
            qry += " and TSPL_PR_DETAIL.PR_Qty<>0  "
            qry += " and TSPL_PR_DETAIL.PR_No not in ('" + _TransNo + "')"

            qry += " union all " + Environment.NewLine

            qry += " select 'IC-AD' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.mbtnStoreAdjustment) + "' as TransCode,TSPL_ADJUSTMENT_HEADER.Adjustment_No as DocNo, TSPL_ADJUSTMENT_DETAIL.Item_Code as ICode,TSPL_ADJUSTMENT_HEADER.Loc_Code as Locaion,TSPL_ADJUSTMENT_DETAIL.Item_Quantity as Qty,-1 as RI,TSPL_ADJUSTMENT_DETAIL.Unit_Code AS Uom "
            qry += " from TSPL_ADJUSTMENT_DETAIL "
            qry += " left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No"
            qry += " where TSPL_ADJUSTMENT_HEADER.Posted='N' and TSPL_ADJUSTMENT_DETAIL.Item_Code='" + _ICode + "'"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_ADJUSTMENT_HEADER.Loc_Code='" + _LCode + "' "
            End If
            If _IsMRPMandatory AndAlso _MRP > 0 Then
                qry += " and TSPL_ADJUSTMENT_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_ADJUSTMENT_DETAIL.Unit_Code='" + _UOM + "' "
            End If
            qry += " and TSPL_ADJUSTMENT_DETAIL.Item_Quantity<>0  and TSPL_ADJUSTMENT_DETAIL.Adjustment_Type in ('BD','QD') and TSPL_ADJUSTMENT_HEADER.Adjustment_No not in ('" + _TransNo + "')"

            qry += " union all " + Environment.NewLine

            qry += " select 'RGP' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.mbtnGatePass) + "' as TransCode,TSPL_RGP_HEAD.RGP_No as DocNo, TSPL_RGP_DETAIL.Item_Code as ICode,TSPL_RGP_HEAD.Location as Locaion,TSPL_RGP_DETAIL.RGP_Qty as Qty,-1 as RI,TSPL_RGP_DETAIL.Unit_code AS Uom "
            qry += " from TSPL_RGP_DETAIL "
            qry += " left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No"
            qry += " where TSPL_RGP_HEAD.Status=0 and TSPL_RGP_DETAIL.Item_Code='" + _ICode + "'"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += "  and TSPL_RGP_HEAD.Location='" + _LCode + "'"
            End If

            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_RGP_DETAIL.Unit_code='" + _UOM + "' "
            End If
            qry += " and TSPL_RGP_DETAIL.RGP_Qty<>0  "
            qry += " and TSPL_RGP_DETAIL.RGP_No not in ('" + _TransNo + "')"

            qry += " union all " + Environment.NewLine

            qry += " select 'Scrap' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.ScrapSale) + "' as TransCode,TSPL_SCRAPSALE_HEAD.shipment_No as DocNo, TSPL_SCRAPSALE_DETAIL.Item_Code as ICode,TSPL_SCRAPSALE_HEAD.Loc_Code as Locaion,TSPL_SCRAPSALE_DETAIL.shipped_Qty as Qty,-1 as RI,TSPL_SCRAPSALE_DETAIL.Unit_code AS Uom "
            qry += " from TSPL_SCRAPSALE_DETAIL "
            qry += " left outer join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No"
            qry += " where TSPL_SCRAPSALE_HEAD.IsPost=0 and TSPL_SCRAPSALE_DETAIL.Item_Code='" + _ICode + "'"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += "  and TSPL_SCRAPSALE_HEAD.Loc_Code='" + _LCode + "' "
            End If

            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_SCRAPSALE_DETAIL.Unit_code='" + _UOM + "' "
            End If
            qry += " and TSPL_SCRAPSALE_DETAIL.shipped_Qty<>0 and TSPL_SCRAPSALE_DETAIL.shipment_No not in ('" + _TransNo + "')"

            qry += "  union all " + Environment.NewLine

            qry += " select 'Issue/Return/Transfer' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.mbtnIssueReturn) + "' as TransCode,TSPL_IssueReturn_HEAD.Doc_No as DocNo, TSPL_IssueReturn_DETAIL.Item_Code as ICode,TSPL_IssueReturn_HEAD.From_Location as Locaion,TSPL_IssueReturn_DETAIL.Issued_Qty as Qty,-1 as RI,TSPL_IssueReturn_DETAIL.Unit_code AS Uom "
            qry += " from TSPL_IssueReturn_DETAIL "
            qry += " left outer join TSPL_IssueReturn_HEAD on TSPL_IssueReturn_HEAD.Doc_No=TSPL_IssueReturn_DETAIL.Doc_No"
            qry += " where TSPL_IssueReturn_HEAD.Status=0 and TSPL_IssueReturn_DETAIL.Item_Code='" + _ICode + "' "
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_IssueReturn_HEAD.From_Location='" + _LCode + "' "
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_IssueReturn_DETAIL.Unit_code='" + _UOM + "' "
            End If
            qry += " and TSPL_IssueReturn_DETAIL.Issued_Qty<>0 and TSPL_IssueReturn_DETAIL.Doc_No not in ('" + _TransNo + "') " + Environment.NewLine

            qry += "  union all " + Environment.NewLine
            qry += "  select  'SaleOrder' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmSNSalesOrder) + "' as TransCode,TSPL_SD_SALES_ORDER_HEAD.Document_Code as DocNo, TSPL_SD_SALES_ORDER_DETAIL.Item_Code as ICode,TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location as Locaion,(TSPL_SD_SALES_ORDER_DETAIL.CommitedQty)-isnull(TSPL_SD_SHIPMENT_DETAIL.qty,0)  as Qty,-1 as RI,TSPL_SD_SALES_ORDER_DETAIL.Unit_code AS Uom  "
            qry += " from TSPL_SD_SALES_ORDER_DETAIL"
            qry += " left outer join TSPL_SD_SALES_ORDER_HEAD on TSPL_SD_SALES_ORDER_HEAD.Document_Code=TSPL_SD_SALES_ORDER_DETAIL.DOCUMENT_CODE "
            qry += " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order  =TSPL_SD_SALES_ORDER_HEAD.DOCUMENT_CODE "
            qry += " left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE =TSPL_SD_SHIPMENT_HEAD.Document_Code"
            qry += " where TSPL_SD_SALES_ORDER_DETAIL.Item_Code='" + _ICode + "'  "


            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location='" + _LCode + "'  "
            End If
            If _IsMRPMandatory AndAlso _MRP > 0 Then
                qry += " and TSPL_SD_SALES_ORDER_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "' "
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_SD_SALES_ORDER_DETAIL.Unit_code='" + _UOM + "' "
            End If
            qry += " and TSPL_SD_SALES_ORDER_DETAIL.CommitedQty>0 and TSPL_SD_SALES_ORDER_DETAIL.DOCUMENT_CODE not  in('" + _TransNo + "') "
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
            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowStockCheckatDOLevel, clsFixedParameterCode.AllowStockCheckatDOLevel, trans)), "1") = CompairStringResult.Equal Then
                qry += " and TSPL_SD_SHIPMENT_HEAD.Trans_Type not in ( 'PS') " + Environment.NewLine +
                " union all " + Environment.NewLine +
                " select * from (" + Environment.NewLine +
                " select 'DeliveryOrderPS' as TransType,'DeliveryOrderPS' as TransCode,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code as DocNo, TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code as ICode,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Bill_To_Location as Locaion " + Environment.NewLine +
                ",case when isnull(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close,'N')='N' then TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Qty -isnull((select sum( TSPL_SD_SHIPMENT_DETAIL.qty) from TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code= TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_HEAD.Status=1 and TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS =TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.DOCUMENT_CODE and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code and TSPL_SD_SHIPMENT_DETAIL.Unit_code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Unit_code),0)" + Environment.NewLine +
                " else isnull((select sum( TSPL_SD_SHIPMENT_DETAIL.qty) from TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code= TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_HEAD.Status=0 and TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS =TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.DOCUMENT_CODE and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code and TSPL_SD_SHIPMENT_DETAIL.Unit_code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Unit_code),0)  end as Qty ,-1 as RI,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Unit_code AS Uom  " + Environment.NewLine +
                " from TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE " + Environment.NewLine +
                " left outer join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.DOCUMENT_CODE " + Environment.NewLine +
                " where TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code='" + _ICode + "'" + Environment.NewLine

                If clsCommon.myLen(_LCode) > 0 Then
                    qry += " and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Bill_To_Location='" + _LCode + "'  "
                End If
                If _IsMRPMandatory AndAlso _MRP > 0 Then
                    qry += " and TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.MRP='" + clsCommon.myCstr(_MRP) + "' "
                End If
                If IsItemWithDifferntUnitConsiderAsOtherItem Then
                    qry += " and TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Unit_code='" + _UOM + "' "
                End If
                qry += " and (TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.DOCUMENT_CODE not in ('" + _TransNo + "'))" + Environment.NewLine +
                " ) x where Qty>0 "
            End If


            ''can sale
            qry += " union all  " + Environment.NewLine

            qry += " select 'CanSale' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.FrmCanSale) + "' as TransCode,TSPL_CAN_SALE_HEAD.Document_No as DocNo,TSPL_CAN_SALE_HEAD.CanItemCode  as ICode," &
                " TSPL_CAN_SALE_HEAD.Location_Code as Locaion,TSPL_CAN_SALE_HEAD.TotalNoofCans ,-1 as RI,TSPL_CAN_SALE_HEAD.CanItemUOM AS Uom  from TSPL_CAN_SALE_DETAIL " &
                " left outer join TSPL_CAN_SALE_HEAD on TSPL_CAN_SALE_HEAD.Document_No=TSPL_CAN_SALE_DETAIL.Document_No " &
                " where TSPL_CAN_SALE_HEAD.Posted=0 and TSPL_CAN_SALE_HEAD.CanItemCode='" + _ICode + "' and TSPL_CAN_SALE_HEAD.Document_No not in ('" + _TransNo + "') and TSPL_CAN_SALE_HEAD.TotalNoofCans <>0 "
            If clsCommon.myLen(_LCode) > 0 Then
                qry += "  and TSPL_CAN_SALE_HEAD.Location_Code='" & _LCode & "'"
            End If

            ''SILO MILK TRANSFER BHA/03/08/18-000389 richa
            qry += " union all " + Environment.NewLine
            qry += " select 'Silo_MTR' as TransType,'Silo_MTR' as TransCode,TSPL_SILO_MILK_TRANSFER_HEAD.Document_Code as DocNo,TSPL_SILO_MILK_TRANSFER_DETAIL.Item_Code  as ICode,TSPL_SILO_MILK_TRANSFER_DETAIL.Silo_Code as Locaion,TSPL_SILO_MILK_TRANSFER_DETAIL.Qty,-1 as RI,TSPL_SILO_MILK_TRANSFER_DETAIL.UOM AS Uom " &
            " from TSPL_SILO_MILK_TRANSFER_DETAIL " &
            " left outer join TSPL_SILO_MILK_TRANSFER_HEAD on TSPL_SILO_MILK_TRANSFER_HEAD.Document_Code =TSPL_SILO_MILK_TRANSFER_DETAIL.Document_Code " &
            " where TSPL_SILO_MILK_TRANSFER_HEAD.Posted=0 and TSPL_SILO_MILK_TRANSFER_DETAIL.Item_Code='" + _ICode + "' and TSPL_SILO_MILK_TRANSFER_DETAIL.Qty<>0  " &
            " and TSPL_SILO_MILK_TRANSFER_DETAIL.Document_Code not in ('" + _TransNo + "')"

            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_SILO_MILK_TRANSFER_DETAIL.Silo_Code='" + _LCode + "' "
            End If

            ''---------

            ''-----
            '' query for assemblies and disassemblies
            qry += " union all " + Environment.NewLine
            qry += " select 'Assemblies' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmAssemblies) + "' as TransCode,TSPL_PJC_ASSEMBLIES.CODE as DocNo, Main_Item_Code as ICode,LOCATION_CODE as Location,QUANTITY,(case when TRANSACTION_TYPE='Assembly' then 1  else -1 end) as RI,"
            qry += " BUILD_ITEM_UNIT_CODE as UnitCode from TSPL_PJC_ASSEMBLIES where TSPL_PJC_ASSEMBLIES.POSTED=0 and  TSPL_PJC_ASSEMBLIES.Main_Item_Code='" + _ICode + "'  and TSPL_PJC_ASSEMBLIES.CODE  not in ('" + _TransNo + "')"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_PJC_ASSEMBLIES.LOCATION_CODE='" & _LCode & "'"
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and BUILD_ITEM_UNIT_CODE='" + _UOM + "' "
            End If

            qry += " union all  "

            qry += " select  'Assemblies' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmAssemblies) + "' as TransCode,TSPL_PJC_ASSEMBLIES.CODE as DocNo, TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE AS ICode,TSPL_PJC_ASSEMBLIES.LOCATION_CODE as Location,"
            qry += " TSPL_MF_BOM_DETAIL.CONSM_QUANTITY*(TSPL_PJC_ASSEMBLIES.QUANTITY/TSPL_PJC_ASSEMBLIES.BUILD_QUANTITY) as Qty,"
            qry += " (case when TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly' then  -1 else  1 end) AS RI,"
            qry += " TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE as UnitCode from TSPL_PJC_ASSEMBLIES "
            qry += " inner join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_PJC_ASSEMBLIES.BOM_CODE"
            qry += " inner JOIN TSPL_MF_BOM_DETAIL ON TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_MF_BOM_DETAIL.BOM_CODE "
            qry += " where TSPL_PJC_ASSEMBLIES.POSTED=0 and TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE='" + _ICode + "'  and TSPL_PJC_ASSEMBLIES.CODE  not in ('" + _TransNo + "')"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_PJC_ASSEMBLIES.LOCATION_CODE='" & _LCode & "'"
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE='" + _UOM + "' "
            End If

            qry += " union all " + Environment.NewLine
            qry += " select 'Dis-Assemblies' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmAssemblies) + "' as TransCode,TSPL_PROD_ASSEMBLIES.CODE as DocNo, Main_Item_Code as ICode,LOCATION_CODE as Location,QUANTITY,(case when TRANSACTION_TYPE='Assembly' then 1  else -1 end) as RI,"
            qry += " BUILD_ITEM_UNIT_CODE as UnitCode from TSPL_PROD_ASSEMBLIES where TSPL_PROD_ASSEMBLIES.POSTED=0 and  TSPL_PROD_ASSEMBLIES.Main_Item_Code='" + _ICode + "'  and TSPL_PROD_ASSEMBLIES.CODE  not in ('" + _TransNo + "')"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_PROD_ASSEMBLIES.LOCATION_CODE='" & _LCode & "'"
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and BUILD_ITEM_UNIT_CODE='" + _UOM + "' "
            End If

            qry += " union all  "

            qry += " select  'Assemblies' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmAssemblies) + "' as TransCode,TSPL_PJC_ASSEMBLIES.CODE as DocNo, TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE AS ICode,TSPL_PJC_ASSEMBLIES.LOCATION_CODE as Location,"
            qry += " TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_QUANTITY*(TSPL_PJC_ASSEMBLIES.QUANTITY/TSPL_PJC_ASSEMBLIES.BUILD_QUANTITY) as Qty,"
            qry += " (case when TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly' then  -1 else  1 end) AS RI,"
            qry += " TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_UNIT_CODE as UnitCode from TSPL_PJC_ASSEMBLIES "
            qry += " inner JOIN TSPL_PROD_ASSEMBLIES_ITEM_DETAIL ON TSPL_PJC_ASSEMBLIES.CODE=TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.ASSEMBLY_CODE "
            qry += " where TSPL_PJC_ASSEMBLIES.POSTED=0 and TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE='" + _ICode + "'  and TSPL_PJC_ASSEMBLIES.CODE  not in ('" + _TransNo + "')"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_PJC_ASSEMBLIES.LOCATION_CODE='" & _LCode & "'"
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_UNIT_CODE='" + _UOM + "' "
            End If


            qry += " union all  "

            qry += " select  'Wreckage' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmAssemblies) + "' as TransCode,TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE as DocNo, TSPL_WRECKAGE_BOOKING.Item_Code AS ICode,TSPL_WRECKAGE_ENTRY.LOCATION_CODE as Location,"
            qry += " TSPL_WRECKAGE_BOOKING.WRECKAGE_QTY as Qty, -1 AS RI,"
            qry += " TSPL_WRECKAGE_BOOKING.Unit_Code as UnitCode from TSPL_WRECKAGE_ENTRY "
            qry += " inner JOIN TSPL_WRECKAGE_BOOKING ON TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE=TSPL_WRECKAGE_BOOKING.WRECKAGE_CODE "
            qry += " where TSPL_WRECKAGE_ENTRY.POSTED=0 and TSPL_WRECKAGE_BOOKING.ITEM_CODE='" + _ICode + "'  and TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE  not in ('" + _TransNo + "')"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_WRECKAGE_ENTRY.LOCATION_CODE='" & _LCode & "'"
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_WRECKAGE_BOOKING.Unit_Code='" + _UOM + "' "
            End If

            qry += " union all " + Environment.NewLine
            qry += " select 'Production Issue' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmProcessProductionIssueEntry) + "' as TransCode,TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code as DocNo, TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code as ICode,TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code as Location,TSPL_PP_ISSUE_ITEM_DETAIL.Qty as QUANTITY,-1 as RI,"
            qry += " TSPL_PP_ISSUE_ITEM_DETAIL.Unit_Code as UnitCode from TSPL_PP_ISSUE_ITEM_DETAIL left outer join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code where TSPL_PP_ISSUE_HEAD.Is_post=0 and  TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code='" + _ICode + "'  and TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code  not in ('" + _TransNo + "')"
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code='" & _LCode & "'"
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_PP_ISSUE_ITEM_DETAIL.Unit_Code='" + _UOM + "' "
            End If
            '' query for add/remove items durng Process production Standardization
            qry += " union all " + Environment.NewLine
            qry += " select 'Production Standardization' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmProcessProductionStandardization) + "' as TransCode,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code as Doc_No,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Item_Code as ICode,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Loaction_Code as Location,"
            qry += " TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY,"
            qry += " (case when TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then 1 else  -1  end)as RI,"
            qry += " TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE from TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL "
            qry += " inner join TSPL_PP_STANDARDIZATION_HEAD on TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code = TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code "
            qry += " where TSPL_PP_STANDARDIZATION_HEAD.Posted=0 and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Item_Code='" + _ICode + "' "
            qry += " and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code not in ('" + _TransNo + "')"
            qry += " and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Loaction_Code='" & _LCode & "' and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Remove' "
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE='" + _UOM + "' "
            End If
            '' query for add/remove items durng Process production STAGE PROCESS
            qry += " union all " + Environment.NewLine
            qry += " select 'Production Stage Process' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.frmProcessProductionStageProcess) + "' as TransCode,TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE as Doc_No, TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code as ICode,TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Loaction_Code as Location,"
            qry += " TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY,"
            qry += " (case when TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then 1 else  -1  end)as RI,"
            qry += " TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE from TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL "
            qry += " inner join TSPL_PP_STAGE_PROCESS_HEAD on TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE = TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE "
            qry += " where TSPL_PP_STAGE_PROCESS_HEAD.Posted=0 and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code='" + _ICode + "' "
            qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE not in ('" + _TransNo + "')"
            qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Loaction_Code='" & _LCode & "' and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Remove'"
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE='" + _UOM + "' "
            End If
            ''For CSA Transfer
            qry += " union all " + Environment.NewLine +
            " select 'CSA Transfer' as TransType,'SD-CSATRANS' as TransCode,TSPL_CSA_TRANSFER_HEAD.DOC_CODE as Doc_No, TSPL_CSA_TRANSFER_DETAIL.Item_Code as ICode,TSPL_CSA_TRANSFER_HEAD.From_Location_Code as Location, TSPL_CSA_TRANSFER_DETAIL.Qty, -1 as RI, TSPL_CSA_TRANSFER_DETAIL.Unit_code as Uom " + Environment.NewLine +
            " from TSPL_CSA_TRANSFER_DETAIL " + Environment.NewLine +
            " left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DOC_CODE=TSPL_CSA_TRANSFER_DETAIL.DOC_CODE " + Environment.NewLine +
            " where TSPL_CSA_TRANSFER_HEAD.Status=0 and TSPL_CSA_TRANSFER_DETAIL.Item_Code='" + _ICode + "'  and TSPL_CSA_TRANSFER_HEAD.DOC_CODE not in ('" + _TransNo + "') "
            If clsCommon.myLen(_LCode) > 0 Then
                qry += " and TSPL_CSA_TRANSFER_HEAD.From_Location_Code='" + _LCode + "'  "
            End If
            If IsItemWithDifferntUnitConsiderAsOtherItem Then
                qry += " and TSPL_CSA_TRANSFER_DETAIL.Unit_code ='" + _UOM + "'  "
            End If
            If _IsMRPMandatory AndAlso _MRP > 0 Then
                qry += " and TSPL_CSA_TRANSFER_DETAIL.MRP='" + clsCommon.myCstr(_MRP) + "'"
            End If

            qry += " union all " + Environment.NewLine
            qry += " select 'Milk Jobwork' as Trans_Type,'" + clsCommon.myCstr(clsUserMgtCode.frmMilkJobWorkTransfer) + "' as Trans_Code,JWD.Document_Code as Doc_No,JWD.Item_Code as ICode,JW.Loc_Code,JWD.Net_Weight,-1 AS RI," &
                   " JWD.UOM  from TSPL_MILK_JOBWORK_TRANSFER_DETAILS JWD inner join TSPL_MILK_JOBWORK_TRANSFER_HEAD JW on JWD.Document_Code=JW.Document_Code " &
                   " where  JW.isPosted=0 AND JWD.Item_Code='" + _ICode + "' and JW.Loc_Code='" & _LCode & "' AND JWD.Document_Code NOT IN ('" + _TransNo + "') "
            qry += " union all " + Environment.NewLine
            qry += " select 'Milk Jobwork Other' as Trans_Type,'" + clsCommon.myCstr(clsUserMgtCode.frmMilkJobWorkTransferOther) + "' as Trans_Code,TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TRANSFER_NO as Doc_No,TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Item_Code as ICode,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.From_Locaction,TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Qty,-1 AS RI," &
                   " TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.UOM  from TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS inner join TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD on TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_NO=TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TRANSFER_NO " &
                   " where  TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Status=0 AND TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Item_Code='" + _ICode + "' and TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.From_Locaction='" & _LCode & "'  AND TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TRANSFER_NO NOT IN ('" + _TransNo + "') "
            qry += " union all " + Environment.NewLine
            qry += " select 'Production Return' as Trans_Type,'" & clsCommon.myCstr(clsUserMgtCode.frmProcessProdReturn) & "' as Trans_Code,PER.PROD_RETURN_CODE as Doc_No,PED.ITEM_CODE as ICode,PEH.LOCATION_CODE," &
                   " PED.FINAL_PRODUCTION_QTY,-1 as RI,PED.UNIT_CODE from TSPL_PP_PRODUCTION_ENTRY_DETAIL PED " &
                   " inner join TSPL_PP_PRODUCTION_ENTRY PEH on PED.PROD_ENTRY_CODE=PEH.PROD_ENTRY_CODE " &
                   " inner join TSPL_PP_PRODUCTION_RETURN PER ON PEH.PROD_ENTRY_CODE=PER.PROD_ENTRY_CODE " &
                   " WHERE PER.POSTED=0 AND PED.ITEM_CODE='" + _ICode + "' and PEH.LOCATION_CODE='" & _LCode & "' AND PER.PROD_RETURN_CODE NOT IN ('" + _TransNo + "') "
        End If
        qry += " )xx" + Environment.NewLine &
               " left join (" & qryMinBal & ") as Minimum_Bal on xx.ICode=Minimum_Bal.Item_Code and xx.Location=Minimum_Bal.Location_Code "
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM "
        qry += " left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode and FinalUOM.UOM_Code='" + _UOM + "'"
        Return qry
    End Function
    Public Shared Function CheckCancelInventoryBalance(ByVal Trans_Type As String, ByVal Doc_Code As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            Dim dt As DataTable

            qry = " select Trans_Id,Trans_Type,InOut,Location_Code,Item_Code,Item_Desc,Qty,UOM,Source_Doc_No,cast(Punching_Date as date) as Punching_Date,Stock_Qty,Stock_Uom,MRP " &
                  " from TSPL_INVENTORY_MOVEMENT  where Trans_Type='" & Trans_Type & "' and Source_Doc_No='" & Doc_Code & "' and InOut='I'"
            '' CHECK IN INVENTORY THAT IS TO BE DELETED
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            For Each dr As DataRow In dt.Rows
                If clsCommon.CompairString(dr.Item("InOut"), "I") = CompairStringResult.Equal Then
                    '' check balance 
                    Dim bal As Decimal = clsItemLocationDetails.getBalance(dr.Item("Item_Code"), dr.Item("Location_Code"), Doc_Code, dr.Item("Punching_Date"), trans, dr.Item("Stock_UOM"), 0)
                    If bal < clsCommon.myCdbl(dr.Item("Stock_Qty")) Then
                        Throw New Exception("Insuficient Stock: Item -" & dr.Item("Item_Code") & ", Location- " & dr.Item("Location_Code") & " UOM: " & dr.Item("Stock_UOM") & " Available Qty: " & bal & " Required Qty: " & clsCommon.myCdbl(dr.Item("Stock_Qty")) & "")
                    End If

                    '' check batch negative status
                    If clsItemMaster.IsBatchItem(dr.Item("Item_Code"), trans) Then
                        qry = "select  coalesce(Batch_No,Manual_BatchNo) as Batch_No, Item_Code,Document_Date,Document_Type,In_Out_Type,UOM,Qty from tspl_batch_item where Document_Type='" & dr.Item("Trans_Type") & "' and Document_Code='" & Doc_Code & "' and Against_Inv_Movement_Trans_Id=" & dr.Item("Trans_Id") & ""
                        Dim dtBatch As DataTable
                        dtBatch = clsDBFuncationality.GetDataTable(qry, trans)
                        For Each drBatch As DataRow In dtBatch.Rows
                            Dim BatchBal As Decimal = clsBatchInventory.GetBatchBalance(clsCommon.myCstr(drBatch.Item("Item_Code")), clsCommon.myCstr(dr.Item("Location_Code")), clsCommon.myCstr(drBatch.Item("Batch_No")), clsCommon.myCdbl(dr.Item("MRP")), clsCommon.myCstr(drBatch.Item("UOM")), Doc_Code, clsCommon.myCstr(dr.Item("Trans_Type")), trans, True)
                            If clsCommon.myCdbl(drBatch.Item("Qty")) > BatchBal Then
                                Throw New Exception("Insuficient Batch Stock: Batch No-" & clsCommon.myCstr(drBatch.Item("Batch_No")) & " Item -" & dr.Item("Item_Code") & ", Location- " & dr.Item("Location_Code") & " UOM: " & drBatch.Item("UOM") & " Available Qty: " & BatchBal & " Required Qty: " & clsCommon.myCdbl(drBatch.Item("Qty")) & "")
                            End If
                        Next
                    End If
                End If

            Next
            dt = New DataTable
            '' milk inventory
            qry = " select Trans_Type,InOut,Main_Location,Location_Code,Item_Code,Item_Desc,Qty,UOM,Source_Doc_No,cast(Punching_Date as date) as Punching_Date,Stock_Qty,Stock_Uom " &
                  " from TSPL_INVENTORY_MOVEMENT_NEW  where Trans_Type='" & Trans_Type & "' and Source_Doc_No='" & Doc_Code & "' and InOut='I'"
            '' update inventory for consumption
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            For Each dr As DataRow In dt.Rows
                If clsCommon.CompairString(dr.Item("InOut"), "I") = CompairStringResult.Equal Then
                    '' check balance 
                    Dim bal As Decimal = clsInventoryMovementNew.getBalance(dr.Item("Item_Code"), clsCommon.myCstr(dr.Item("Main_Location")), dr.Item("Location_Code"), Doc_Code, dr.Item("Punching_Date"), trans, dr.Item("Stock_UOM"))
                    If bal < clsCommon.myCdbl(dr.Item("Stock_Qty")) Then
                        Throw New Exception("Insuficient Stock: Item -" & dr.Item("Item_Code") & ", Location- " & dr.Item("Location_Code") & " UOM: " & dr.Item("Stock_UOM") & " Available Qty: " & bal & " Required Qty: " & clsCommon.myCdbl(dr.Item("Stock_Qty")) & "")
                    End If

                    '' check batch negative status
                    If clsItemMaster.IsBatchItem(dr.Item("Item_Code"), trans) Then
                        qry = "select  coalesce(Batch_No,Manual_BatchNo) as Batch_No, Item_Code,Document_Date,Document_Type,In_Out_Type,UOM,Qty from tspl_batch_item where Document_Type='" & dr.Item("Trans_Type") & "' and Document_Code='" & Doc_Code & "' and Against_Inv_Movement_Trans_Id=" & dr.Item("Trans_Id") & ""
                        Dim dtBatch As DataTable
                        dtBatch = clsDBFuncationality.GetDataTable(qry, trans)
                        For Each drBatch As DataRow In dtBatch.Rows
                            Dim BatchBal As Decimal = clsBatchInventory.GetBatchBalance(clsCommon.myCstr(drBatch.Item("Item_Code")), clsCommon.myCstr(dr.Item("Location_Code")), clsCommon.myCstr(drBatch.Item("Batch_No")), clsCommon.myCdbl(dr.Item("MRP")), clsCommon.myCstr(drBatch.Item("UOM")), Doc_Code, clsCommon.myCstr(dr.Item("Trans_Type")), trans, True)
                            If clsCommon.myCdbl(drBatch.Item("Qty")) > BatchBal Then
                                Throw New Exception("Insuficient Batch Stock: Batch No-" & clsCommon.myCstr(drBatch.Item("Batch_No")) & " Item -" & dr.Item("Item_Code") & ", Location- " & dr.Item("Location_Code") & " UOM: " & drBatch.Item("UOM") & " Available Qty: " & BatchBal & " Required Qty: " & clsCommon.myCdbl(drBatch.Item("Qty")) & "")
                            End If
                        Next
                    End If
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function
End Class