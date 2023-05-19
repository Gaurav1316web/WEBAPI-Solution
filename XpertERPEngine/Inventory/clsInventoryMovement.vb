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
        qry += " where TSPL_INVENTORY_MOVEMENT.Qty<>0 and TSPL_INVENTORY_MOVEMENT.Item_Code='" + _ICode + "'"
        If clsCommon.myLen(_LCode) > 0 Then
            qry += "  and Location_Code='" + _LCode + "'"
        End If
        If _IsMRPMandatory AndAlso _MRP > 0 Then
            qry += " and TSPL_INVENTORY_MOVEMENT.MRP='" + clsCommon.myCstr(_MRP) + "' "
        End If

        If IsItemWithDifferntUnitConsiderAsOtherItem Then
            qry += " and TSPL_INVENTORY_MOVEMENT.UOM='" + _UOM + "' "
        End If

        Dim intSettingType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, trans))
        Dim qryMinBal As String = "select null as Item_Code,null as Location_Code,null as Minimum_Balance"
        If intSettingType = 1 Then
            'qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
            qryMinBal = " select Item_Code,Location_Code,min(Closing_Balance) as Minimum_Balance from (" & _
                        " select Item_Code,Location_Code,cast(Punching_Date as date) as Punching_Date,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*Stock_Qty)) over(order by cast(Punching_Date as date)) as Closing_Balance " & _
                        " from TSPL_INVENTORY_MOVEMENT where Item_Code='" & _ICode & "' AND Location_Code='" & _LCode & "' " & _
                        " group by cast(Punching_Date as date),Item_Code,Location_Code " & _
                        " union all " & _
                        " select Item_Code,Location_Code,cast(Punching_Date as date) as Punching_Date,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*Stock_Qty)) over(order by cast(Punching_Date as date)) as Closing_Balance " & _
                        " from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" & _ICode & "' AND Location_Code='" & _LCode & "' " & _
                        " group by cast(Punching_Date as date),Item_Code,Location_Code) as MinimumQry where Punching_Date>'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(_TransDate), "dd/MMM/yyyy hh:mm tt") + "' " & _
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
                qry += " and TSPL_SD_SHIPMENT_HEAD.Trans_Type not in ( 'PS') " + Environment.NewLine + _
                " union all " + Environment.NewLine + _
                " select * from (" + Environment.NewLine + _
                " select 'DeliveryOrderPS' as TransType,'DeliveryOrderPS' as TransCode,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code as DocNo, TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code as ICode,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Bill_To_Location as Locaion " + Environment.NewLine + _
                ",case when isnull(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close,'N')='N' then TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Qty -isnull((select sum( TSPL_SD_SHIPMENT_DETAIL.qty) from TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code= TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_HEAD.Status=1 and TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS =TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.DOCUMENT_CODE and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code and TSPL_SD_SHIPMENT_DETAIL.Unit_code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Unit_code),0)" + Environment.NewLine + _
                " else isnull((select sum( TSPL_SD_SHIPMENT_DETAIL.qty) from TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code= TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_HEAD.Status=0 and TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS =TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.DOCUMENT_CODE and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code and TSPL_SD_SHIPMENT_DETAIL.Unit_code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Unit_code),0)  end as Qty ,-1 as RI,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Unit_code AS Uom  " + Environment.NewLine + _
                " from TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE " + Environment.NewLine + _
                " left outer join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.DOCUMENT_CODE " + Environment.NewLine + _
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
                qry += " and (TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.DOCUMENT_CODE not in ('" + _TransNo + "'))" + Environment.NewLine + _
                " ) x where Qty>0 "
            End If


            ''can sale
            qry += " union all  " + Environment.NewLine

            qry += " select 'CanSale' as TransType,'" + clsCommon.myCstr(clsUserMgtCode.FrmCanSale) + "' as TransCode,TSPL_CAN_SALE_HEAD.Document_No as DocNo,TSPL_CAN_SALE_HEAD.CanItemCode  as ICode," & _
                " TSPL_CAN_SALE_HEAD.Location_Code as Locaion,TSPL_CAN_SALE_HEAD.TotalNoofCans ,-1 as RI,TSPL_CAN_SALE_HEAD.CanItemUOM AS Uom  from TSPL_CAN_SALE_DETAIL " & _
                " left outer join TSPL_CAN_SALE_HEAD on TSPL_CAN_SALE_HEAD.Document_No=TSPL_CAN_SALE_DETAIL.Document_No " & _
                " where TSPL_CAN_SALE_HEAD.Posted=0 and TSPL_CAN_SALE_HEAD.CanItemCode='" + _ICode + "' and TSPL_CAN_SALE_HEAD.Document_No not in ('" + _TransNo + "') and TSPL_CAN_SALE_HEAD.TotalNoofCans <>0 "
            If clsCommon.myLen(_LCode) > 0 Then
                qry += "  and TSPL_CAN_SALE_HEAD.Location_Code='" & _LCode & "'"
            End If

            ''SILO MILK TRANSFER BHA/03/08/18-000389 richa
            qry += " union all " + Environment.NewLine
            qry += " select 'Silo_MTR' as TransType,'Silo_MTR' as TransCode,TSPL_SILO_MILK_TRANSFER_HEAD.Document_Code as DocNo,TSPL_SILO_MILK_TRANSFER_DETAIL.Item_Code  as ICode,TSPL_SILO_MILK_TRANSFER_DETAIL.Silo_Code as Locaion,TSPL_SILO_MILK_TRANSFER_DETAIL.Qty,-1 as RI,TSPL_SILO_MILK_TRANSFER_DETAIL.UOM AS Uom " & _
            " from TSPL_SILO_MILK_TRANSFER_DETAIL " & _
            " left outer join TSPL_SILO_MILK_TRANSFER_HEAD on TSPL_SILO_MILK_TRANSFER_HEAD.Document_Code =TSPL_SILO_MILK_TRANSFER_DETAIL.Document_Code " & _
            " where TSPL_SILO_MILK_TRANSFER_HEAD.Posted=0 and TSPL_SILO_MILK_TRANSFER_DETAIL.Item_Code='" + _ICode + "' and TSPL_SILO_MILK_TRANSFER_DETAIL.Qty<>0  " & _
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
            qry += " union all " + Environment.NewLine + _
            " select 'CSA Transfer' as TransType,'SD-CSATRANS' as TransCode,TSPL_CSA_TRANSFER_HEAD.DOC_CODE as Doc_No, TSPL_CSA_TRANSFER_DETAIL.Item_Code as ICode,TSPL_CSA_TRANSFER_HEAD.From_Location_Code as Location, TSPL_CSA_TRANSFER_DETAIL.Qty, -1 as RI, TSPL_CSA_TRANSFER_DETAIL.Unit_code as Uom " + Environment.NewLine + _
            " from TSPL_CSA_TRANSFER_DETAIL " + Environment.NewLine + _
            " left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DOC_CODE=TSPL_CSA_TRANSFER_DETAIL.DOC_CODE " + Environment.NewLine + _
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
            qry += " select 'Milk Jobwork' as Trans_Type,'" + clsCommon.myCstr(clsUserMgtCode.frmMilkJobWorkTransfer) + "' as Trans_Code,JWD.Document_Code as Doc_No,JWD.Item_Code as ICode,JW.Loc_Code,JWD.Net_Weight,-1 AS RI," & _
                   " JWD.UOM  from TSPL_MILK_JOBWORK_TRANSFER_DETAILS JWD inner join TSPL_MILK_JOBWORK_TRANSFER_HEAD JW on JWD.Document_Code=JW.Document_Code " & _
                   " where  JW.isPosted=0 AND JWD.Item_Code='" + _ICode + "' and JW.Loc_Code='" & _LCode & "' AND JWD.Document_Code NOT IN ('" + _TransNo + "') "
            qry += " union all " + Environment.NewLine
            qry += " select 'Milk Jobwork Other' as Trans_Type,'" + clsCommon.myCstr(clsUserMgtCode.frmMilkJobWorkTransferOther) + "' as Trans_Code,TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TRANSFER_NO as Doc_No,TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Item_Code as ICode,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.From_Locaction,TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Qty,-1 AS RI," & _
                   " TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.UOM  from TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS inner join TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD on TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_NO=TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TRANSFER_NO " & _
                   " where  TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Status=0 AND TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Item_Code='" + _ICode + "' and TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.From_Locaction='" & _LCode & "'  AND TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TRANSFER_NO NOT IN ('" + _TransNo + "') "
            qry += " union all " + Environment.NewLine
            qry += " select 'Production Return' as Trans_Type,'" & clsCommon.myCstr(clsUserMgtCode.frmProcessProdReturn) & "' as Trans_Code,PER.PROD_RETURN_CODE as Doc_No,PED.ITEM_CODE as ICode,PEH.LOCATION_CODE," & _
                   " PED.FINAL_PRODUCTION_QTY,-1 as RI,PED.UNIT_CODE from TSPL_PP_PRODUCTION_ENTRY_DETAIL PED " & _
                   " inner join TSPL_PP_PRODUCTION_ENTRY PEH on PED.PROD_ENTRY_CODE=PEH.PROD_ENTRY_CODE " & _
                   " inner join TSPL_PP_PRODUCTION_RETURN PER ON PEH.PROD_ENTRY_CODE=PER.PROD_ENTRY_CODE " & _
                   " WHERE PER.POSTED=0 AND PED.ITEM_CODE='" + _ICode + "' and PEH.LOCATION_CODE='" & _LCode & "' AND PER.PROD_RETURN_CODE NOT IN ('" + _TransNo + "') "
        End If
        qry += " )xx" + Environment.NewLine & _
               " left join (" & qryMinBal & ") as Minimum_Bal on xx.ICode=Minimum_Bal.Item_Code and xx.Location=Minimum_Bal.Location_Code "
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM "
        qry += " left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode and FinalUOM.UOM_Code='" + _UOM + "'"
        Return qry
    End Function
    Public Shared Function CheckCancelInventoryBalance(ByVal Trans_Type As String, ByVal Doc_Code As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            Dim dt As DataTable

            qry = " select Trans_Id,Trans_Type,InOut,Location_Code,Item_Code,Item_Desc,Qty,UOM,Source_Doc_No,cast(Punching_Date as date) as Punching_Date,Stock_Qty,Stock_Uom,MRP " & _
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
            qry = " select Trans_Type,InOut,Main_Location,Location_Code,Item_Code,Item_Desc,Qty,UOM,Source_Doc_No,cast(Punching_Date as date) as Punching_Date,Stock_Qty,Stock_Uom " & _
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

Public Class clsInventoryMovement
    Public itemtypeinventry As String = Nothing
    Public itemstatus As String = Nothing
    Public Trans_Type As String = Nothing
    Public InOut As String = Nothing
    Public Location_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Qty As Double = 0
    Public UOM As String = Nothing
    Public Source_Doc_No As String = Nothing
    Public Source_Doc_Date As String = Nothing
    Public Entry_Date As String = Nothing
    Public Basic_Cost As Double = 0
    Public Rec_Cost As Double = 0
    Public Add_Cost As Double = 0
    Public Net_Cost As Double = 0
    Public MRP As Double = 0
    Public ItemType As String = Nothing
    Public Punching_Date As String
    Public Batch_No As String = Nothing
    Public FIFO_Cost As Double = 0
    Public LIFO_Cost As Double = 0
    Public Avg_Cost As Double = 0
    Public Posting_Date As DateTime? = Nothing
    Public MFG_Date As DateTime? = Nothing
    Public Expiry_Date As DateTime? = Nothing
    Public Stock_Qty As Double = 0
    Public Stock_UOM As String = Nothing
    Public IS_CONSUMPTION As Integer = 0

    Public Cust_Code As String = Nothing
    Public Cust_Name As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Other_Location_Code As String = Nothing
    Public Other_Location_Desc As String = Nothing
    Public BatchSkipOnSetting As Boolean = False

    Public FAT_KG As Decimal = Nothing
    Public FAT_Per As Decimal = Nothing
    Public SNF_KG As Decimal = Nothing
    Public SNF_Per As Decimal = Nothing
    Public Fat_Rate As Decimal = Nothing
    Public Fat_Amt As Decimal = Nothing
    Public SNF_Rate As Decimal = Nothing
    Public SNF_Amt As Decimal = Nothing
    Public Is_Scheme_Item As String = String.Empty
    Public Inventory_DrAcc As String = String.Empty
    Public Inventory_CrAcc As String = String.Empty
    ''============for ref line no for Batchitem======
    Public Ref_Line_No As Integer = Nothing
    Public CalculateAvgCost As Boolean = True

    Public PI_Cost As Decimal
    Public Item_Status As String
    Public Assmbly_Status As String

    Public Shared Function DeepCopyObject(ByVal obj As clsInventoryMovement) As clsInventoryMovement
        Dim objNew As clsInventoryMovement = New clsInventoryMovement()
        objNew.Trans_Type = obj.Trans_Type
        objNew.InOut = obj.InOut
        objNew.Location_Code = obj.Location_Code
        objNew.Item_Code = obj.Item_Code
        objNew.Item_Desc = obj.Item_Desc
        objNew.Qty = obj.Qty
        objNew.UOM = obj.UOM
        objNew.Source_Doc_No = obj.Source_Doc_No
        objNew.Source_Doc_Date = obj.Source_Doc_Date
        objNew.Entry_Date = obj.Entry_Date
        objNew.Basic_Cost = obj.Basic_Cost
        objNew.Rec_Cost = obj.Rec_Cost
        objNew.Add_Cost = obj.Add_Cost
        objNew.Net_Cost = obj.Net_Cost
        objNew.ItemType = obj.ItemType
        objNew.Punching_Date = obj.Punching_Date
        objNew.MRP = obj.MRP
        objNew.Batch_No = obj.Batch_No
        objNew.FIFO_Cost = obj.FIFO_Cost
        objNew.LIFO_Cost = obj.LIFO_Cost
        objNew.Avg_Cost = obj.Avg_Cost
        objNew.Posting_Date = obj.Posting_Date
        objNew.PI_Cost = obj.PI_Cost
        objNew.Stock_UOM = obj.Stock_UOM
        objNew.Stock_Qty = obj.Stock_Qty
        objNew.MFG_Date = obj.MFG_Date
        objNew.Expiry_Date = obj.Expiry_Date
        objNew.Item_Status = obj.Item_Status
        objNew.Assmbly_Status = obj.Assmbly_Status
        objNew.IS_CONSUMPTION = obj.IS_CONSUMPTION
        objNew.Cust_Code = obj.Cust_Code
        objNew.Cust_Name = obj.Cust_Name
        objNew.Vendor_Code = obj.Vendor_Code
        objNew.Vendor_Name = obj.Vendor_Name
        objNew.Other_Location_Code = obj.Other_Location_Code
        objNew.Other_Location_Desc = obj.Other_Location_Desc
        objNew.FAT_Per = obj.FAT_Per
        objNew.SNF_Per = obj.SNF_Per
        objNew.FAT_KG = obj.FAT_KG
        objNew.SNF_KG = obj.SNF_KG
        objNew.Fat_Rate = obj.Fat_Rate
        objNew.SNF_Rate = obj.SNF_Rate
        objNew.Fat_Amt = obj.Fat_Amt
        objNew.SNF_Amt = obj.SNF_Amt
        objNew.Inventory_DrAcc = obj.Inventory_DrAcc
        objNew.Inventory_CrAcc = obj.Inventory_CrAcc
        objNew.Is_Scheme_Item = obj.Is_Scheme_Item
        objNew.CalculateAvgCost = obj.CalculateAvgCost
        Return objNew
    End Function

    Private Shared Function GetDataWithBatch(ByVal TransType As String, ByVal DocNo As String, ByVal DocDate As DateTime, ByVal arr As List(Of clsInventoryMovement), ByVal trans As SqlTransaction)
        Dim arrReturn As New List(Of clsInventoryMovement)
        Try


            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isBatchApplyOnInventoryMovement, clsFixedParameterCode.isBatchApplyOnInventoryMovement, trans)) = 0 OrElse clsCommon.CompairString(TransType, "IC-AD") = CompairStringResult.Equal OrElse clsCommon.CompairString(TransType, "SRN") = CompairStringResult.Equal Then
                Return arr
            End If

            Dim arrItemDone As New List(Of String)
            For Each obj As clsInventoryMovement In arr
                If obj.Qty = 0 Then
                    Continue For
                End If

                If clsCommon.CompairString(obj.ItemType, "FM") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.ItemType, "FT") = CompairStringResult.Equal Then
                    Dim strRI As String = "1*"
                    If clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal Then
                        strRI = "-1*"
                    End If
                    Dim strLOType As String = ""
                    If clsCommon.CompairString(TransType, "Transfer") = CompairStringResult.Equal Then
                        strLOType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transfer_Type from TSPL_TRANSFER_HEAD where Transfer_No='" + DocNo + "'", trans))
                        If clsCommon.CompairString(strLOType, "LI") = CompairStringResult.Equal Then
                            strRI = "-1*"
                        End If
                    End If


                    Dim convFact As Double = clsItemMaster.GetConvertionFactor(obj.Item_Code, obj.UOM, trans)
                    Dim dblMRP As Double = obj.MRP * convFact
                    Dim qry As String = "select Stock_Qty*Conversion_Factor as Stock_Qty,Batch_No,MFG_Date,Expiry_Date from("
                    qry += " select sum(" + strRI + "Stock_Qty * case when inout='I' then 1 else case when inout='O' then -1 else 0 end end) as Stock_Qty,Batch_No,MAX(MFG_Date) as MFG_Date,MAX(Expiry_Date) as Expiry_Date  from("
                    qry += " select  Stock_Qty,inout,Batch_No,MFG_Date,Expiry_Date  "
                    qry += " from TSPL_INVENTORY_MOVEMENT "
                    qry += " where TSPL_INVENTORY_MOVEMENT.Item_Code='" + obj.Item_Code + "' and (Qty*MRP/Stock_Qty)=" + clsCommon.myCstr(dblMRP) + " and Stock_Qty<>0 "
                    If clsCommon.CompairString(TransType, "Transfer") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strLOType, "LI") = CompairStringResult.Equal Then
                            qry += " and TSPL_INVENTORY_MOVEMENT.InOut='O' and Source_Doc_No in (select top 1 Load_Out_No from TSPL_TRANSFER_HEAD where Transfer_No='" + DocNo + "')"
                        ElseIf clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal Then
                            qry += " and Source_Doc_No='" + DocNo + "'"
                        Else
                            qry += " and TSPL_INVENTORY_MOVEMENT.Location_Code='" + obj.Location_Code + "' "
                        End If
                    ElseIf clsCommon.CompairString(TransType, "Sale Return") = CompairStringResult.Equal Then
                        Dim strShipmentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Shipment_No from TSPL_SHIPMENT_MASTER where Invoice_No in ( select Invoice_No from TSPL_SALE_RETURN_HEAD where Sale_Return_No='" + DocNo + "')", trans))
                        If clsCommon.myLen(strShipmentNo) > 0 Then
                            qry += " and Source_Doc_No in ('" + strShipmentNo + "') "
                        Else
                            ''Case for sale return inter company
                            'Pick any batch that exist in out system that why we are not checking location.'
                            ''qry += " and TSPL_INVENTORY_MOVEMENT.Location_Code='" + obj.Location_Code + "' "
                            qry += " and Expiry_Date >='" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "'"
                        End If
                    ElseIf clsCommon.CompairString(TransType, "Purchase Return") = CompairStringResult.Equal Then
                        Dim strSRNNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Against_SRN  from TSPL_PR_HEAD where PR_No='" + DocNo + "'", trans))
                        If clsCommon.myLen(strSRNNo) > 0 Then
                            qry += " and Source_Doc_No in ('" + strSRNNo + "') "
                        End If
                    Else ''For WareHouse and rest all
                        qry += " and TSPL_INVENTORY_MOVEMENT.Location_Code='" + obj.Location_Code + "' "
                    End If

                    If arrItemDone.Contains(obj.Item_Code.Trim()) Then
                        For Each objForBatch As clsInventoryMovement In arrReturn
                            If clsCommon.CompairString(objForBatch.Item_Code, obj.Item_Code) = CompairStringResult.Equal Then
                                Dim innerconvFact As Double = clsItemMaster.GetConvertionFactor(objForBatch.Item_Code, objForBatch.UOM, trans)
                                Dim dblinnerMRP As Double = objForBatch.MRP * convFact
                                If dblinnerMRP = dblMRP Then
                                    qry += " union all "
                                    qry += " select " + clsCommon.myCstr(objForBatch.Qty / innerconvFact) + " as Stock_Qty,'O' as inout,'" + objForBatch.Batch_No + "' as Batch_No,null as MFG_Date,null as Expiry_Date "
                                End If
                            End If
                        Next
                    Else
                        arrItemDone.Add(obj.Item_Code.Trim())
                    End If
                    qry += " )xxx group by Batch_No having sum(" + strRI + " Stock_Qty * case when inout='I' then 1 else case when inout='O' then -1 else 0 end end)<>0"
                    qry += " )xxxx"
                    qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code='" + obj.Item_Code + "' and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + obj.UOM + "'"
                    qry += " order by Expiry_Date "

                    Dim qtyToApply As Double = obj.Qty
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            Dim objToInsert As clsInventoryMovement = DeepCopyObject(obj)
                            objToInsert.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                            objToInsert.MFG_Date = clsCommon.myCDate(dr("MFG_Date"))
                            objToInsert.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
                            If clsCommon.myCdbl(dr("Stock_Qty")) > qtyToApply Then
                                objToInsert.Qty = qtyToApply
                                qtyToApply -= qtyToApply
                            Else
                                objToInsert.Qty = clsCommon.myCdbl(dr("Stock_Qty"))
                                qtyToApply -= clsCommon.myCdbl(dr("Stock_Qty"))
                            End If
                            arrReturn.Add(objToInsert)
                            If qtyToApply = 0 Then
                                Exit For
                            End If
                        Next
                    End If
                    If qtyToApply > 0 Then
                        Throw New Exception("Item Qty not available for item" + obj.Item_Code)
                    End If
                Else
                    arrReturn.Add(obj)
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrReturn
    End Function

    Public Shared Function SaveData(ByVal TransType As String, ByVal DocNo As String, ByVal DocDate As DateTime, ByVal EntryDate As String, ByVal ArrInvMov As List(Of clsInventoryMovement), ByVal trans As SqlTransaction, Optional ByVal FromDateForAvg As Date? = Nothing, Optional ByVal ExtraWhrForAvg As String = Nothing) As Boolean
        Try
            If objCommonVar.StopInventory Then
                Return True
            End If
            Dim LineNo As Integer = 1
        Dim arr As List(Of clsInventoryMovement) = GetDataWithBatch(TransType, DocNo, DocDate, ArrInvMov, trans)
        If clsInventorySourceCode.CheckNewEntry(TransType, trans) Then
            Throw New Exception("Please make Inventory Source code '" + TransType + "'")
        End If

            If (arr IsNot Nothing AndAlso arr.Count > 0) Then
                For Each obj As clsInventoryMovement In arr
                    If clsCommon.myLen(obj.Item_Code) <= 0 Then
                        Continue For
                    End If
                    Dim coll As New Hashtable()
                    Dim dtpostingDate As DateTime = clsCommon.GETSERVERDATE(trans)
                    clsCommon.AddColumnsForChange(coll, "Trans_Type", TransType)
                    clsCommon.AddColumnsForChange(coll, "InOut", obj.InOut)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                    clsCommon.AddColumnsForChange(coll, "Source_Doc_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "Source_Doc_Date", clsCommon.GetPrintDate(DocDate, "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Entry_Date", EntryDate)
                    clsCommon.AddColumnsForChange(coll, "Basic_Cost", obj.Basic_Cost)
                    clsCommon.AddColumnsForChange(coll, "Rec_Cost", obj.Rec_Cost)
                    clsCommon.AddColumnsForChange(coll, "Add_Cost", obj.Add_Cost)
                    clsCommon.AddColumnsForChange(coll, "Net_Cost", obj.Net_Cost)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "ItemType", obj.ItemType)
                    clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                    clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)
                    clsCommon.AddColumnsForChange(coll, "Punching_Date", clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy hh:mm tt"))

                    If obj.MFG_Date IsNot Nothing AndAlso obj.MFG_Date.HasValue Then
                        clsCommon.AddColumnsForChange(coll, "MFG_Date", clsCommon.GetPrintDate(obj.MFG_Date, "dd/MMM/yyyy"))
                    End If

                    If obj.Expiry_Date IsNot Nothing AndAlso obj.MFG_Date.HasValue Then
                        clsCommon.AddColumnsForChange(coll, "Expiry_Date", clsCommon.GetPrintDate(obj.Expiry_Date, "dd/MMM/yyyy"))
                    End If

                    obj.Stock_UOM = clsItemMaster.GetStockUnit(obj.Item_Code, trans)
                    obj.Stock_Qty = Math.Round(obj.Qty * clsItemMaster.GetConvertionFactor(obj.Item_Code, obj.UOM, trans), 2, MidpointRounding.AwayFromZero)
                    clsCommon.AddColumnsForChange(coll, "Stock_Qty", obj.Stock_Qty)
                    clsCommon.AddColumnsForChange(coll, "Stock_UOM", obj.Stock_UOM)
                    clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(dtpostingDate, "dd/MMM/yyyy hh:mm tt"))

                    clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Cust_Name", obj.Cust_Name)
                    clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
                    clsCommon.AddColumnsForChange(coll, "Other_Location_Code", obj.Other_Location_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Other_Location_Desc", obj.Other_Location_Desc)
                    Dim strTransferType As String = ""
                    Dim strTransferNo As String = ""
                    If clsCommon.CompairString(TransType, "Transfer") = CompairStringResult.Equal Then
                        strTransferType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transfer_Type  from TSPL_TRANSFER_ORDER_HEAD where Document_No='" & DocNo & "'", trans))
                    End If
                    If Not obj.CalculateAvgCost Then
                    ElseIf clsCommon.CompairString(TransType, "SRN-RET") = CompairStringResult.Equal Then
                        ''Costing is filled same.
                    ElseIf clsCommon.CompairString(TransType, "PP-PR") = CompairStringResult.Equal Then
                        ''Costing is filled same.
                    ElseIf clsCommon.CompairString(TransType, "IC-AD") = CompairStringResult.Equal Then
                        obj.FIFO_Cost = obj.Basic_Cost * IIf(obj.Qty = 0, 1, obj.Qty)
                        obj.LIFO_Cost = obj.FIFO_Cost
                        obj.Avg_Cost = obj.FIFO_Cost
                    ElseIf clsCommon.CompairString(TransType, "TRN-RET") = CompairStringResult.Equal Then
                        strTransferNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transfer_No from TSPL_TRANSFER_RETURN where Document_No='" & DocNo & "'", trans))
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select FIFO_Cost,LIFO_Cost,Avg_Cost from TSPL_INVENTORY_MOVEMENT where Item_Code='" + obj.Item_Code + "' and Source_Doc_No='" + strTransferNo + "' and Stock_Qty='" + clsCommon.myCstr(obj.Stock_Qty) + "' and Trans_Type='Transfer' and InOut='O'", trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            obj.FIFO_Cost = clsCommon.myCdbl(dt.Rows(0)("FIFO_Cost"))
                            obj.LIFO_Cost = clsCommon.myCdbl(dt.Rows(0)("LIFO_Cost"))
                            obj.Avg_Cost = clsCommon.myCdbl(dt.Rows(0)("Avg_Cost"))
                        End If
                    ElseIf clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal AndAlso clsCommon.CompairString(TransType, "Transfer") = CompairStringResult.Equal Then
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select FIFO_Cost,LIFO_Cost,Avg_Cost from TSPL_INVENTORY_MOVEMENT where Item_Code='" + obj.Item_Code + "' and Source_Doc_No='" + DocNo + "' and Stock_Qty='" + clsCommon.myCstr(obj.Stock_Qty) + "' and Trans_Type='" + TransType + "' and InOut='O'", trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            obj.FIFO_Cost = clsCommon.myCdbl(dt.Rows(0)("FIFO_Cost"))
                            obj.LIFO_Cost = clsCommon.myCdbl(dt.Rows(0)("LIFO_Cost"))
                            obj.Avg_Cost = clsCommon.myCdbl(dt.Rows(0)("Avg_Cost"))
                        End If
                    ElseIf clsCommon.CompairString(strTransferType, "I") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.InOut, "O") = CompairStringResult.Equal Then
                        Dim TransferOutNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TransferOutNo  from TSPL_TRANSFER_ORDER_HEAD where Document_No='" & DocNo & "'", trans))
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select FIFO_Cost,LIFO_Cost,Avg_Cost from TSPL_INVENTORY_MOVEMENT where Item_Code='" + obj.Item_Code + "' and Source_Doc_No='" + TransferOutNo + "' and Stock_Qty='" + clsCommon.myCstr(obj.Stock_Qty) + "' and Trans_Type='" + TransType + "' and InOut='O'", trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            obj.FIFO_Cost = clsCommon.myCdbl(dt.Rows(0)("FIFO_Cost"))
                            obj.LIFO_Cost = clsCommon.myCdbl(dt.Rows(0)("LIFO_Cost"))
                            obj.Avg_Cost = clsCommon.myCdbl(dt.Rows(0)("Avg_Cost"))
                        End If

                    ElseIf Not (clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal And clsCommon.CompairString(TransType, "SD-CSATRANS") = CompairStringResult.Equal) Then
                        Dim strRefDoc As String = Nothing
                        If clsCommon.CompairString(TransType, "Sale Return") = CompairStringResult.Equal Then
                            strRefDoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Invoice_Code from TSPL_SD_SALE_RETURN_DETAIL where DOCUMENT_CODE='" & DocNo & "'", trans))
                        End If
                        If clsCommon.CompairString(TransType, "Sale Return") = CompairStringResult.Equal AndAlso clsCommon.myLen(strRefDoc) <= 0 Then
                            Dim isApplyCostOnPostDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCostingOnPostedDate, clsFixedParameterCode.ApplyCostingOnPostedDate, trans)) = 1, True, False)
                            obj.FIFO_Cost = GetCost(EnumCostingMethod.AveregeIn, obj.Item_Code, obj.Location_Code, obj.Stock_Qty, DocDate, dtpostingDate, isApplyCostOnPostDate, trans)
                            obj.LIFO_Cost = obj.FIFO_Cost
                            obj.Avg_Cost = obj.FIFO_Cost
                        ElseIf clsCommon.CompairString(TransType, "PP_ISSUE") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "Prod-Issue") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "Production") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "PP_STDN") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "PRD_STG_PROC") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "PROD_ENTRY") = CompairStringResult.Equal Then
                            obj.FIFO_Cost = obj.FIFO_Cost
                            obj.LIFO_Cost = obj.LIFO_Cost
                            obj.Avg_Cost = obj.Avg_Cost
                        ElseIf clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal Then
                            obj.FIFO_Cost = obj.Basic_Cost * obj.Qty
                            obj.LIFO_Cost = obj.Basic_Cost * obj.Qty
                            obj.Avg_Cost = obj.Basic_Cost * obj.Qty
                        Else
                            Dim isApplyCostOnPostDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCostingOnPostedDate, clsFixedParameterCode.ApplyCostingOnPostedDate, trans)) = 1, True, False)
                            obj.FIFO_Cost = GetCost(EnumCostingMethod.FIFO, obj.Item_Code, obj.Location_Code, obj.Stock_Qty, DocDate, dtpostingDate, isApplyCostOnPostDate, trans)
                            obj.LIFO_Cost = GetCost(EnumCostingMethod.LIFO, obj.Item_Code, obj.Location_Code, obj.Stock_Qty, DocDate, dtpostingDate, isApplyCostOnPostDate, trans)
                            If FromDateForAvg IsNot Nothing Then
                                obj.Avg_Cost = GetCost(EnumCostingMethod.Averege, obj.Item_Code, obj.Location_Code, obj.Stock_Qty, FromDateForAvg, dtpostingDate, isApplyCostOnPostDate, trans, "TSPL_INVENTORY_MOVEMENT", "", ExtraWhrForAvg, FromDateForAvg)
                            Else
                                obj.Avg_Cost = GetCost(EnumCostingMethod.Averege, obj.Item_Code, obj.Location_Code, obj.Stock_Qty, DocDate, dtpostingDate, isApplyCostOnPostDate, trans, "TSPL_INVENTORY_MOVEMENT", "", ExtraWhrForAvg)
                            End If

                        End If
                    End If

                    If clsCommon.CompairString(TransType, "StockConversion") = CompairStringResult.Equal Then
                        obj.FIFO_Cost = 0
                        obj.LIFO_Cost = 0
                        obj.Avg_Cost = 0
                    End If

                    clsCommon.AddColumnsForChange(coll, "FIFO_Cost", obj.FIFO_Cost)
                    clsCommon.AddColumnsForChange(coll, "LIFO_Cost", obj.LIFO_Cost)
                    clsCommon.AddColumnsForChange(coll, "Avg_Cost", obj.Avg_Cost)

                    clsCommon.AddColumnsForChange(coll, "item_status", obj.itemstatus)
                    clsCommon.AddColumnsForChange(coll, "Assmbly_Status", obj.itemtypeinventry)
                    clsCommon.AddColumnsForChange(coll, "IS_CONSUMPTION", obj.IS_CONSUMPTION)

                    ''=====================FAT/SNF values===================================================
                    clsCommon.AddColumnsForChange(coll, "FAT_Per", obj.FAT_Per)
                    clsCommon.AddColumnsForChange(coll, "FAT_KG", obj.FAT_KG)
                    clsCommon.AddColumnsForChange(coll, "Fat_Rate", obj.Fat_Rate)
                    clsCommon.AddColumnsForChange(coll, "Fat_Amt", obj.Fat_Amt)
                    clsCommon.AddColumnsForChange(coll, "SNF_Per", obj.SNF_Per)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG)
                    clsCommon.AddColumnsForChange(coll, "SNF_Rate", obj.SNF_Rate)
                    clsCommon.AddColumnsForChange(coll, "SNF_Amt", obj.SNF_Amt)
                    clsCommon.AddColumnsForChange(coll, "Is_Scheme_Item", obj.Is_Scheme_Item, True)
                    clsCommon.AddColumnsForChange(coll, "Inventory_CrAcc", obj.Inventory_CrAcc, True)
                    clsCommon.AddColumnsForChange(coll, "Inventory_DrAcc", obj.Inventory_DrAcc, True)
                    ''======================================================================================

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVENTORY_MOVEMENT", OMInsertOrUpdate.Insert, "", trans)

                    If obj.Ref_Line_No > 0 Then ''for special condition,where on screen line no is not refreshed ,so pass line no in obj,otherwise incremented default line no applied.
                        LineNo = obj.Ref_Line_No
                    End If
                    clsSerializeInvenotry.PostData(TransType, DocNo, obj.Item_Code, obj.InOut, LineNo, trans)

                    If obj.Qty <> 0 Then ''if qty=0 from store adjustment and item is of batch type ,then also no need of below method,as it sends error message.
                        If clsERPFuncationality.GetBatchWiseApplicableStatus(DocDate, trans) = True Then
                            clsBatchInventory.PostData(TransType, DocNo, obj.Item_Code, obj.InOut, LineNo, trans, obj.BatchSkipOnSetting, (obj.IS_CONSUMPTION = 1))
                        End If

                    End If

                    LineNo += 1
                Next
                'UpdateInvSummaryDataWIN("", trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function GetCost(ByVal CostMethod As EnumCostingMethod, ByVal strICode As String, ByVal strLocation As String, ByVal dblqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction) As Decimal
        Return GetCost(CostMethod, strICode, strLocation, dblqty, dtDocumentDate, dtPostingDate, isApplyCostOnPostDate, trans, "TSPL_INVENTORY_MOVEMENT")
    End Function

    Public Shared Function GetCost(ByVal CostMethod As EnumCostingMethod, ByVal strICode As String, ByVal strLocation As String, ByVal dblqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction, ByVal tableName As String) As Decimal
        Return GetCost(CostMethod, strICode, strLocation, dblqty, dtDocumentDate, dtPostingDate, isApplyCostOnPostDate, trans, tableName, "")
    End Function
    Public Shared Function GetCost(ByVal CostMethod As EnumCostingMethod, ByVal strICode As String, ByVal strLocation As String, ByVal dblqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction, ByVal tableName As String, ByVal strUOMCode As String) As Decimal
        Return GetCost(CostMethod, strICode, strLocation, dblqty, dtDocumentDate, dtPostingDate, isApplyCostOnPostDate, trans, tableName, strUOMCode, "")
    End Function
    Public Shared Function GetCost(ByVal CostMethod As EnumCostingMethod, ByVal strICode As String, ByVal strLocation As String, ByVal dblqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction, ByVal tableName As String, ByVal strUOMCode As String, ByVal ExtrWhrl As String) As Decimal
        Return GetCost(CostMethod, strICode, strLocation, dblqty, dtDocumentDate, dtPostingDate, isApplyCostOnPostDate, trans, tableName, strUOMCode, ExtrWhrl, Nothing)
    End Function

    Public Shared Function GetCost(ByVal CostMethod As EnumCostingMethod, ByVal strICode As String, ByVal strLocation As String, ByVal dblqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction, ByVal tableName As String, ByVal strUOMCode As String, ByVal ExtrWhrl As String, ByVal FromDateForAvg As Date?) As Decimal
        Dim settPickProductCostFromItemUOMDetail As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0)
        Dim flag As Boolean = True 'Added by preeti gupta Against ticket no[ERO08/08/04/19-000550]
        Dim dblRetCost As Decimal = 0
        If Not CostMethod = EnumCostingMethod.NA AndAlso dblqty > 0 Then
            Dim strSymbolCost As String = " >= "
            Dim strSymbolTrans As String = " > "
            If CostMethod = EnumCostingMethod.LIFO Then
                strSymbolCost = " <= "
                'strSymbolTrans = " < "
            End If

            Dim strDateColumn As String = " Punching_Date "
            Dim strDateForCheck As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt")
            If isApplyCostOnPostDate Then
                strDateColumn = " Posting_Date "
                strDateForCheck = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtPostingDate), "dd/MMM/yyyy hh:mm tt")
            End If

            Dim qry As String
            If CostMethod = EnumCostingMethod.AveregeIn Then
                qry = "select case when Qty<0 then 0 else abs(Amt/Qty)*" + clsCommon.myCstr(dblqty) + "  end as AvgCost from( select  sum(Amt * RI) as Amt,sum(Qty * RI) as Qty from(" + Environment.NewLine
                qry += " select Stock_Qty as Qty,( Avg_Cost) as Amt,case when InOut='O' then -1 else 1 end as RI from " & tableName & " where Item_Code='" + strICode + "' and Location_Code='" + strLocation + "' and  " + strDateColumn + " <= '" + strDateForCheck + "' " + Environment.NewLine
                qry += " and InOut='I'"
                If clsCommon.myLen(ExtrWhrl) > 0 Then
                    qry += "  " + ExtrWhrl
                End If
                qry += " )xxx )xxxx" + Environment.NewLine
                dblRetCost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

            ElseIf CostMethod = EnumCostingMethod.Averege Then
                qry = "select case when (Qty<0 or Amt<0)  then (AvgPostitiveRate*" + clsCommon.myCstr(dblqty) + ") else case when (Qty>0 and Qty>=" + clsCommon.myCstr(dblqty) + ")  then (Amt/Qty)*" + clsCommon.myCstr(dblqty) + " else case when Qty>0 and Qty<" + clsCommon.myCstr(dblqty) + " then (Amt+((" + clsCommon.myCstr(dblqty) + "-Qty)*AvgPostitiveRate)) else 0 end end end as AvgCost from( "
                qry += " select  sum(Amt * RI) as Amt,sum(Qty * RI) as Qty,"
                If settPickProductCostFromItemUOMDetail Then
                    qry += "(select top 1 Item_Cost from TSPL_ITEM_UOM_DETAIL where item_code='" + strICode + "' "
                    If clsCommon.myLen(strUOMCode) > 0 Then
                        qry += " and UOM_Code ='" + strUOMCode + "'"
                    Else
                        qry += " and Stocking_Unit='Y' "
                    End If
                    qry += ") as AvgPostitiveRate "
                Else
                    qry += "(select top 1 Avg_Cost/(case when Stock_Qty=0 then 1 else Stock_Qty end)  from " & tableName & " where Item_Code='" + strICode + "' and Location_Code='" + strLocation + "' and  " + strDateColumn + " <= '" + strDateForCheck + "' and InOut='I' and Avg_Cost>0 and Trans_Type<>'IC-AD' ) as AvgPostitiveRate "
                End If
                qry += " from(" + Environment.NewLine
                qry += " select Stock_Qty as Qty,( Avg_Cost) as Amt,case when InOut='O' then -1 else 1 end as RI  from " & tableName & " where Item_Code='" + strICode + "' and Location_Code='" + strLocation + "' and  " + strDateColumn + " <= '" + strDateForCheck + "' " + Environment.NewLine
                If FromDateForAvg IsNot Nothing Then
                    qry += " and  " + strDateColumn + " >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDateForAvg), "dd/MMM/yyyy hh:mm tt") + "'"
                End If
                If clsCommon.myLen(ExtrWhrl) > 0 Then
                    qry += " " + ExtrWhrl
                End If
                qry += " )xxx )xxxx" + Environment.NewLine
                dblRetCost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            ElseIf objCommonVar.CalculateFIFOAndLIFOCosting Then

                UpdateSeconds(strDateColumn, tableName, strICode, strLocation, strDateForCheck, trans)

                qry = ";WITH cteStockSum AS ( " + Environment.NewLine
                qry += " SELECT   Item_Code ,SUM(Stock_Qty * CASE WHEN  InOut = 'O' THEN -1 ELSE 1 END) AS TotalStock FROM  " & tableName & " where Item_Code='" + strICode + "' and Location_Code='" + strLocation + "' and " + strDateColumn + " <= '" + strDateForCheck + "'  GROUP BY Item_Code)," + Environment.NewLine

                qry += " cteReverseInSum AS (" + Environment.NewLine
                qry += " SELECT  s.Trans_Id, s.Item_Code ,s." + strDateColumn + " as TranDate ,(SELECT SUM(i.Stock_Qty) FROM " & tableName & " AS i  WHERE i.Item_Code = s.Item_Code AND i.InOut IN ( 'I' ) and i." + strDateColumn + " <= '" + strDateForCheck + "' and i.Location_Code='" + strLocation + "' AND i." + strDateColumn + " " + strSymbolCost + " s." + strDateColumn + " --for FIFO  >= " + Environment.NewLine
                qry += " and 2=(case when i." + strDateColumn + "=s." + strDateColumn + " and i.Trans_Id<s.Trans_Id then 3 else 2 end) ) AS RollingStock ,s.Stock_Qty AS ThisStock FROM " & tableName & " AS s WHERE  s.Item_Code='" + strICode + "' and s.Location_Code='" + strLocation + "' and s." + strDateColumn + " <= '" + strDateForCheck + "'  and s.InOut IN ( 'I' ) and s.Stock_Qty<>0 )," + Environment.NewLine

                qry += " cteWithLastTranDate  AS ( " + Environment.NewLine
                qry += " SELECT  LastPartialStock.Trans_Id, w.Item_Code ,w.TotalStock ,LastPartialStock. TranDate ,LastPartialStock.StockToUse ,LastPartialStock.RunningTotal ,w.TotalStock -LastPartialStock.RunningTotal+ LastPartialStock.StockToUse AS UseThisStock FROM cteStockSum AS w" + Environment.NewLine
                qry += " CROSS APPLY ( SELECT TOP 1  z.Trans_Id, z.TranDate ,z.ThisStock AS StockToUse ,z.RollingStock AS RunningTotal FROM  cteReverseInSum AS z WHERE z.Item_Code = w.Item_Code AND z.RollingStock >= w.TotalStock ORDER BY  z.TranDate " + IIf(CostMethod = EnumCostingMethod.FIFO, "DESC", "") + " --for FIFO DESC" + Environment.NewLine + ""
                qry += " ,z.RollingStock) AS LastPartialStock" + Environment.NewLine
                qry += " )" + Environment.NewLine

                qry += " select *  from (" + Environment.NewLine
                qry += " SELECT  y.Item_Code ,y.TotalStock AS CurrentItems ,e.Basic_Cost,e." + strDateColumn + " as TranDate,(CASE WHEN e." + strDateColumn + " = y.TranDate and 2=(case when e." + strDateColumn + " = y.TranDate and e.Trans_Id " + strSymbolTrans + " y.Trans_Id then 3 else 2 end) THEN y.UseThisStock" + Environment.NewLine
                qry += " ELSE e.Stock_Qty END * Price.Basic_Cost) AS CurrentValue,(CASE WHEN e. " + strDateColumn + "  = y.TranDate and 2=(case when e." + strDateColumn + " = y.TranDate and e.Trans_Id " + strSymbolTrans + " y.Trans_Id then 3 else 2 end) THEN y.UseThisStock ELSE e.Stock_Qty END  ) as BalanceQty FROM cteWithLastTranDate AS y INNER JOIN " & tableName & " AS e ON e.Item_Code = y.Item_Code and e." + strDateColumn + " <= '" + strDateForCheck + "' AND e." + strDateColumn + " " + strSymbolCost + " y.TranDate -- for Fifo >=" + Environment.NewLine
                qry += " AND e.InOut IN ('I') and e.Location_Code='" + strLocation + "' " + Environment.NewLine
                qry += " CROSS APPLY ( SELECT TOP ( 1 ) case when Stock_Qty =0 then 0 else  (p.Basic_Cost*p.Qty)/p.Stock_Qty end as Basic_Cost FROM " & tableName & " AS p  WHERE p.Item_Code = e.Item_Code " + Environment.NewLine
                qry += " AND p." + strDateColumn + " <= e." + strDateColumn + "  " + Environment.NewLine
                qry += " AND p.InOut = 'I' and p.Location_Code='" + strLocation + "'  ORDER BY p." + strDateColumn + " DESC ) AS Price" + Environment.NewLine
                qry += ")xxx   " + IIf(CostMethod = EnumCostingMethod.FIFO, " order by TranDate ", IIf(CostMethod = EnumCostingMethod.LIFO, "order by TranDate DESC", "")) + " --For Fifo not Desc order" + Environment.NewLine

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim dblbalanceQty As Double = dblqty
                    For Each dr As DataRow In dt.Rows
                        Dim dblCurrQty As Double = clsCommon.myCdbl(dr("BalanceQty"))
                        If dblbalanceQty >= dblCurrQty Then
                            dblRetCost += clsCommon.myCdbl(dr("CurrentValue"))
                        Else
                            dblRetCost += (clsCommon.myCdbl(dr("CurrentValue")) * dblbalanceQty) / dblCurrQty
                        End If
                        dblbalanceQty -= dblCurrQty
                        If dblbalanceQty <= 0 Then
                            dblRetCost = dblRetCost
                            Exit For
                        End If
                    Next
                    If Math.Round(dblbalanceQty, 2, MidpointRounding.ToEven) > 0 Then
                        'Throw New Exception("Quantity Not available for " + strICode)
                    End If
                End If
            ElseIf CostMethod = EnumCostingMethod.FIFO OrElse CostMethod = EnumCostingMethod.LIFO Then
                flag = False
            End If

            If dblRetCost <= 0 AndAlso flag Then
                If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0 Then
                    Dim strStockUOM As String = clsItemMaster.GetStockUnit(strICode, trans)
                    dblRetCost = clsItemUOMDetails.GetItemUOMCost(dtDocumentDate, strICode, strStockUOM, trans) * dblqty
                    If dblRetCost <= 0 AndAlso dblqty <> 0 Then
                        Throw New Exception("Please provide Item Cost of item: " + strICode + " and unit: " + strStockUOM)
                    End If
                End If
            End If
            If clsCommon.myLen(strUOMCode) > 0 AndAlso dblRetCost > 0 Then
                If clsCommon.myLen(strUOMCode) > 0 Then
                    Dim dblOrgConvF As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strICode & "' and UOM_Code='" & strUOMCode & "'", trans))
                    dblRetCost = Math.Round((dblRetCost * dblOrgConvF), 2)
                End If
            End If
        End If
        Return dblRetCost
    End Function

    Public Shared Sub UpdateSeconds(ByVal strDateColumn As String, ByVal tableName As String, ByVal strICode As String, ByVal strLocation As String, ByVal strDateForCheck As String, ByVal trans As SqlTransaction)
        ''BM00000008350 GKD/14/09/18-000160 richa 
        Dim qry As String = "SELECT  " + strDateColumn + ",sum(1) as Rep,DATEPART(SS," + strDateColumn + ") as SEC FROM  " & tableName & " where Item_Code='" + strICode + "' and Location_Code='" + strLocation + "' "
        If clsCommon.myLen(strDateForCheck) > 0 Then
            qry += " and " + strDateColumn + " <= '" + strDateForCheck + "' "
        End If
        qry += " group by " + strDateColumn + " having sum(1)>1"

        Dim dtSecond As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dtSecond IsNot Nothing AndAlso dtSecond.Rows.Count > 0 Then
            '' update seconds in single query
            'Dim qrySec As String = " update " & tableName & " set " & strDateColumn & "= t1.PunchDateAct from (select   Trans_Id,Source_Doc_No,Item_Code,Location_Code," & strDateColumn & ",DATEADD(second,ROW_NUMBER() over(partition by Item_Code,Location_Code,cast(" & strDateColumn & " as date) order by Item_Code,Location_Code)," & strDateColumn & ") as PunchDateAct,ROW_NUMBER() over(partition by Item_Code,Location_Code,cast(" & strDateColumn & " as date) order by Item_Code,Location_Code) as row_Num " & _
            '                    " from " & tableName & " where cast(" & strDateColumn & " as date)>='" & clsCommon.GetPrintDate(strDateForCheck, "dd-MMM-yyyy") & "' and Item_Code='" + strICode + "' and Location_Code='" & strLocation & "' " & _
            '                    " ) t1 where " & tableName & ".Trans_Id=t1.Trans_Id and " & tableName & ".Source_Doc_No=t1.Source_Doc_No " & _
            '                    " and " & tableName & ".Item_Code=t1.Item_Code and " & tableName & ".Location_Code=t1.Location_Code " & _
            '                    " and " & tableName & ".Item_Code='" + strICode + "' and " & tableName & ".Location_Code='" & strLocation & "' and cast(" & tableName & "." & strDateColumn & " as date)>='" & clsCommon.GetPrintDate(strDateForCheck, "dd-MMM-yyyy") & "' "
            Dim qrySec As String = " update " & tableName & " set " & strDateColumn & "= t1.PunchDateAct from (select   Trans_Id,Source_Doc_No,Item_Code,Location_Code," & strDateColumn & ",DATEADD(second,ROW_NUMBER() over(partition by Item_Code,Location_Code,cast(" & strDateColumn & " as date) order by Item_Code,Location_Code)," & strDateColumn & ") as PunchDateAct,ROW_NUMBER() over(partition by Item_Code,Location_Code,cast(" & strDateColumn & " as date) order by Item_Code,Location_Code) as row_Num " & _
                             " from " & tableName & " where cast(" & strDateColumn & " as date)>='" & clsCommon.GetPrintDate(strDateForCheck, "dd-MMM-yyyy") & "' and Item_Code='" + strICode + "' and Location_Code='" & strLocation & "' " & _
                             " ) t1  left outer join " & tableName & " on " & tableName & ".Source_Doc_No=t1.Source_Doc_No and " & tableName & ".Trans_Id=t1.Trans_Id   " & _
                             " and " & tableName & ".Item_Code=t1.Item_Code and " & tableName & ".Location_Code=t1.Location_Code " & _
                             " where " & tableName & ".Item_Code='" + strICode + "' and " & tableName & ".Location_Code='" & strLocation & "' and cast(" & tableName & "." & strDateColumn & " as date)>='" & clsCommon.GetPrintDate(strDateForCheck, "dd-MMM-yyyy") & "' "


            clsDBFuncationality.ExecuteNonQuery(qrySec, trans)

        End If
    End Sub


    Public Shared Function GetFatSNFStockDT(ByVal arrLoc As ArrayList, ByVal From_Date As Date, ByVal To_Date As Date, ByVal IncludeSubLocation As Boolean) As DataTable
        Dim qry As String = GetFatSNFStockQry(arrLoc, From_Date, To_Date, IncludeSubLocation)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
    Public Shared Function GetFatSNFStockQry(ByVal arrLoc As ArrayList, ByVal From_Date As Date, ByVal To_Date As Date, ByVal IncludeSubLocation As Boolean) As String
        Dim Qry As String = ""
        Dim LocTransQry As String = ""
        Dim StockUnionQry As String = ""
        LocTransQry = " select Loc.Location_Code,Loc.Location_Desc,(case when Seq_No=0 then '" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' else AllDate.thedate end) as Trans_date,Seq_No,Trans_Type from (" & _
                      " select 0 as Seq_No,'Opening' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 1 as Seq_No,'Milk Purchase' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 2 as Seq_No,'MCC Purchase' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 3 as Seq_No,'Other Purchase' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 4 as Seq_No,'Fresh Product Milk Sale' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 5 as Seq_No,'Fresh Product Chaach Sale' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 6 as Seq_No,'Fresh Product Curd Sale' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 7 as Seq_No,'Milk Sale' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 8 as Seq_No,'Curd Sale' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 9 as Seq_No,'Skimmed Milk Sale' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 10 as Seq_No,'Product Sale' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 11 as Seq_No,'Others' as Trans_Type " & Environment.NewLine &
                      " ) as TransType,TSPL_LOCATION_MASTER as Loc,dbo.ExplodeDates('" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "','" & clsCommon.GetPrintDate(To_Date, "dd-MMM-yyyy") & "') as AllDate " & Environment.NewLine &
                      " where ((Loc.Location_Type IN ('Physical','Logical','Virtual') ) or (Loc.CSA_Type='Y'))"
        '((Loc.Location_Type IN ('Physical','Logical','Virtual') ) or (Loc.CSA_Type='Y'))
        ''Loc.Location_Type='Physical' and Loc.Is_Sub_Location='N' and Loc.Is_Section='N'
        StockUnionQry = GetFatSNFBaseQry(arrLoc, From_Date, To_Date, True, IncludeSubLocation) & Environment.NewLine &
                        " Union All " & GetFatSNFBaseQry(arrLoc, From_Date, To_Date, False, IncludeSubLocation)

        Qry = " select Location_Code as [Location Code],Location_Desc as [Location Name],convert(varchar,Trans_date,103) as [Trans Date],round((coalesce([Opening FAT],RunningBalanceFAT-BalanceFat)),3) as [Opening FAT]," & Environment.NewLine &
              " round((coalesce([Opening SNF],RunningBalanceSNF-BalanceSNF)),3) as [Opening SNF],round([Milk Purchase FAT],3) as [Milk Purchase FAT]," & Environment.NewLine &
              " round([Milk Purchase SNF],3) as [Milk Purchase SNF],round([MCC Purchase FAT],3) as [MCC Purchase FAT],round([MCC Purchase SNF],3) as [MCC Purchase SNF],round([Other Purchase FAT],3) as [Other Purchase FAT],round([Other Purchase SNF],3) as [Other Purchase SNF],round([Fresh Product Milk Sale FAT],3) as [Fresh Product Milk Sale FAT], " & Environment.NewLine &
              " round([Fresh Product Milk Sale SNF],3) as [Fresh Product Milk Sale SNF],round([Fresh Product Chaach Sale FAT],3) as [Fresh Product Chaach Sale FAT],round([Fresh Product Chaach Sale SNF],3) as [Fresh Product Chaach Sale SNF],round([Fresh Product Curd Sale FAT],3) as [Fresh Product Curd Sale FAT],round([Fresh Product Curd Sale SNF],3) as [Fresh Product Curd Sale SNF], " & Environment.NewLine &
              " round([Milk Sale FAT],3) as [Milk Sale FAT],round([Milk Sale SNF],3) as [Milk Sale SNF],round([Curd Sale FAT],3) as [Curd Sale FAT],round([Curd Sale SNF],3) as [Curd Sale SNF],round([Skimmed Milk Sale FAT],3) as [Skimmed Milk Sale FAT],round([Skimmed Milk Sale SNF],3) as [Skimmed Milk Sale SNF],round([Product Sale FAT],3) as[Product Sale FAT], " & Environment.NewLine &
              " round([Product Sale SNF],3) as [Product Sale SNF],round([Others FAT],3) as [Others FAT],round([Others SNF],3) as [Others SNF],round(RunningBalanceFat,3) as [Closing FAT],round(RunningBalanceSNF,3) as [Closing SNF] from ( " & Environment.NewLine &
              " SELECT *,(coalesce([Opening FAT],0)+coalesce([Milk Purchase FAT],0)+coalesce([MCC Purchase FAT],0)+coalesce([Other Purchase FAT],0)+coalesce([Fresh Product Milk Sale FAT],0)+coalesce([Fresh Product Chaach Sale FAT],0)+coalesce([Fresh Product Curd Sale FAT],0)+coalesce([Milk Sale FAT],0)+coalesce([Curd Sale FAT],0)+coalesce([Skimmed Milk Sale FAT],0)+coalesce([Product Sale FAT],0)+coalesce([Others FAT],0)) as BalanceFat," & Environment.NewLine &
              " (coalesce([Opening SNF],0)+coalesce([Milk Purchase SNF],0)+coalesce([MCC Purchase SNF],0)+coalesce([Other Purchase SNF],0)+coalesce([Fresh Product Milk Sale SNF],0)+coalesce([Fresh Product Chaach Sale SNF],0)+coalesce([Fresh Product Curd Sale SNF],0)+coalesce([Milk Sale SNF],0)+coalesce([Curd Sale SNF],0)+coalesce([Skimmed Milk Sale SNF],0)+coalesce([Product Sale SNF],0)+coalesce([Others SNF],0)) as BalanceSNF," & Environment.NewLine &
              " sum(coalesce([Opening FAT],0)+coalesce([Milk Purchase FAT],0)+coalesce([MCC Purchase FAT],0)+coalesce([Other Purchase FAT],0)+coalesce([Fresh Product Milk Sale FAT],0)+coalesce([Fresh Product Chaach Sale FAT],0)+coalesce([Fresh Product Curd Sale FAT],0)+coalesce([Milk Sale FAT],0)+coalesce([Curd Sale FAT],0)+coalesce([Skimmed Milk Sale FAT],0)+coalesce([Product Sale FAT],0)+coalesce([Others FAT],0)) over (Partition by Location_Code order by Location_Code,Tr_Id) as RunningBalanceFat," & Environment.NewLine &
              " sum(coalesce([Opening SNF],0)+coalesce([Milk Purchase SNF],0)+coalesce([MCC Purchase SNF],0)+coalesce([Other Purchase SNF],0)+coalesce([Fresh Product Milk Sale SNF],0)+coalesce([Fresh Product Chaach Sale SNF],0)+coalesce([Fresh Product Curd Sale SNF],0)+coalesce([Milk Sale SNF],0)+coalesce([Curd Sale SNF],0)+coalesce([Skimmed Milk Sale SNF],0)+coalesce([Product Sale SNF],0)+coalesce([Others SNF],0)) over (Partition by Location_Code order by Location_Code,Tr_Id) as RunningBalanceSNF " & Environment.NewLine &
              " FROM ( " & Environment.NewLine &
              " select Location_Code,Location_Desc,Trans_date,MAX([Opening FAT]) AS [Opening FAT], " & Environment.NewLine &
              " MAX([Opening SNF]) AS [Opening SNF], " & Environment.NewLine &
              " MAX([Milk Purchase FAT]) AS [Milk Purchase FAT], " & Environment.NewLine &
              " MAX([Milk Purchase SNF]) AS [Milk Purchase SNF], " & Environment.NewLine &
              " MAX([MCC Purchase FAT]) AS [MCC Purchase FAT], " & Environment.NewLine &
              " MAX([MCC Purchase SNF]) AS [MCC Purchase SNF], " & Environment.NewLine &
              " MAX([Other Purchase FAT]) AS [Other Purchase FAT], " & Environment.NewLine &
              " MAX([Other Purchase SNF]) AS [Other Purchase SNF], " & Environment.NewLine &
              " MAX([Fresh Product Milk Sale FAT]) AS [Fresh Product Milk Sale FAT], " & Environment.NewLine &
              " MAX([Fresh Product Milk Sale SNF]) AS [Fresh Product Milk Sale SNF], " & Environment.NewLine &
              " MAX([Fresh Product Chaach Sale FAT]) AS [Fresh Product Chaach Sale FAT], " & Environment.NewLine &
              " MAX([Fresh Product Chaach Sale SNF]) AS [Fresh Product Chaach Sale SNF], " & Environment.NewLine &
              " MAX([Fresh Product Curd Sale FAT]) AS [Fresh Product Curd Sale FAT], " & Environment.NewLine &
              " MAX([Fresh Product Curd Sale SNF]) AS [Fresh Product Curd Sale SNF], " & Environment.NewLine &
              " MAX([Milk Sale FAT]) AS [Milk Sale FAT], " & Environment.NewLine &
              " MAX([Milk Sale SNF]) AS [Milk Sale SNF]," & Environment.NewLine &
              " MAX([Curd Sale FAT]) AS [Curd Sale FAT], " & Environment.NewLine &
              " MAX([Curd Sale SNF]) AS [Curd Sale SNF], " & Environment.NewLine &
              " MAX([Skimmed Milk Sale FAT]) AS [Skimmed Milk Sale FAT], " & Environment.NewLine &
              " MAX([Skimmed Milk Sale SNF]) AS [Skimmed Milk Sale SNF], " & Environment.NewLine &
              " MAX([Product Sale FAT]) AS [Product Sale FAT], " & Environment.NewLine &
              " MAX([Product Sale SNF]) AS [Product Sale SNF]," & Environment.NewLine &
              " MAX([Others FAT]) AS [Others FAT], " & Environment.NewLine &
              " MAX([Others SNF]) AS [Others SNF]," & Environment.NewLine &
              " row_number() over (partition by Location_Code order by Location_Code,Trans_date) as Tr_id from ( " & Environment.NewLine &
              " select Loc_Trans.Location_Code,Loc_Trans.Location_Desc,Loc_Trans.Trans_date,Loc_Trans.Trans_Type+' FAT' as Trans_Type_Fat, " & Environment.NewLine &
              " Loc_Trans.Trans_Type+' SNF' as Trans_Type_SNF, " & Environment.NewLine &
              " max(FatStockFinal.FAT_KG) as FAT_KG,max(FatStockFinal.SNF_KG) as SNF_KG from (" & Environment.NewLine &
              " " & LocTransQry & " ) as Loc_Trans " & Environment.NewLine &
              " inner join ( " & Environment.NewLine &
              " " & StockUnionQry & " ) as FatStockFinal on Loc_Trans.Location_Code=FatStockFinal.Location_Code and Loc_Trans.Trans_Type=FatStockFinal.Report_Type and Loc_Trans.Trans_date=FatStockFinal.Punching_Date " & Environment.NewLine &
              " group by Loc_Trans.Location_Code,Loc_Trans.Location_Desc,Loc_Trans.Trans_date,Loc_Trans.Trans_Type,Loc_Trans.Seq_No " & Environment.NewLine &
              " ) AS FatStockOuter " & Environment.NewLine &
              " PIVOT " & Environment.NewLine &
              " (  " & Environment.NewLine &
              " max(FAT_KG) " & Environment.NewLine &
              " FOR FatStockOuter.Trans_Type_Fat " & Environment.NewLine &
              " IN ([Opening FAT], " & Environment.NewLine &
              " [Milk Purchase FAT], " & Environment.NewLine &
              " [MCC Purchase FAT], " & Environment.NewLine &
              " [Other Purchase FAT], " & Environment.NewLine &
              " [Fresh Product Milk Sale FAT], " & Environment.NewLine &
              " [Fresh Product Chaach Sale FAT], " & Environment.NewLine &
              " [Fresh Product Curd Sale FAT], " & Environment.NewLine &
              " [Milk Sale FAT], " & Environment.NewLine &
              " [Curd Sale FAT], " & Environment.NewLine &
              " [Skimmed Milk Sale FAT], " & Environment.NewLine &
              " [Product Sale FAT],[Others FAT]) " & Environment.NewLine &
              " ) AS PIVF " & Environment.NewLine &
              " PIVOT " & Environment.NewLine &
              " (  " & Environment.NewLine &
              " max(SNF_KG) " & Environment.NewLine &
              " FOR PIVF.Trans_Type_SNF " & Environment.NewLine &
              " IN ([Opening SNF], " & Environment.NewLine &
              " [Milk Purchase SNF], " & Environment.NewLine &
              " [MCC Purchase SNF]," & Environment.NewLine &
              " [Other Purchase SNF]," & Environment.NewLine &
              " [Fresh Product Milk Sale SNF]," & Environment.NewLine &
              " [Fresh Product Chaach Sale SNF], " & Environment.NewLine &
              " [Fresh Product Curd Sale SNF], " & Environment.NewLine &
              " [Milk Sale SNF], " & Environment.NewLine &
              " [Curd Sale SNF], " & Environment.NewLine &
              " [Skimmed Milk Sale SNF], " & Environment.NewLine &
              " [Product Sale SNF],[Others SNF]) " & Environment.NewLine &
              " ) AS PIVS " & Environment.NewLine &
              " GROUP BY Location_Code,Location_Desc,Trans_date " & Environment.NewLine &
              " )AS FATSNFtockFinal) as Outermost "

        Return Qry
    End Function
    Public Shared Function GetFatSNFBaseQry(ByVal arrLoc As ArrayList, ByVal From_Date As Date, ByVal To_Date As Date, ByVal is_Opening As Boolean, ByVal IncludeSubLocation As Boolean) As String
        Dim Qry As String = ""
        Dim QryCond As String = " where 2=2 "
        Dim QryCondMilk As String = " where 2=2 "
        Dim ReportTypeCol As String = ""
        Dim Punching_DateCol As String = ""
        If is_Opening Then
            If IncludeSubLocation Then
                QryCond = QryCond & "and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' and (TSPL_INVENTORY_MOVEMENT.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") or TSPL_INVENTORY_MOVEMENT.Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where Main_Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")))"
                'QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' and (Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") or Main_Location in (" & clsCommon.GetMulcallString(arrLoc) & ") )"
                QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' and (TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") or TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where Main_Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")))"
            Else
                QryCond = QryCond & "and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' and  TSPL_INVENTORY_MOVEMENT.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")"
                'QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' and Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")"
                QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' and  TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")"
            End If

            ReportTypeCol = "'Opening'"
            Punching_DateCol = "cast('" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' as date)"
        Else
            If IncludeSubLocation Then
                QryCond = QryCond & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(To_Date, "dd-MMM-yyyy") & "' and (TSPL_INVENTORY_MOVEMENT.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") or TSPL_INVENTORY_MOVEMENT.Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where Main_Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")))"
                'QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(To_Date, "dd-MMM-yyyy") & "' and (Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") or Main_Location in (" & clsCommon.GetMulcallString(arrLoc) & "))"
                QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(To_Date, "dd-MMM-yyyy") & "' and (TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") or TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where Main_Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")))"
            Else
                QryCond = QryCond & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(To_Date, "dd-MMM-yyyy") & "' and TSPL_INVENTORY_MOVEMENT.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") "
                'QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(To_Date, "dd-MMM-yyyy") & "' and Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")"
                QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(To_Date, "dd-MMM-yyyy") & "' and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") "
            End If


            ReportTypeCol = "(Case when Final.Trans_Type in ('DispChallan','DispChallanRet','MilkTransferIn') " & Environment.NewLine &
              " then (case when Final.Trans_Type='DispChallan' then 'Milk Sale' else 'MCC Purchase' end) " & Environment.NewLine &
              " when  Final.Trans_Type in ('MCC-MSR','MCC-MSRN') then 'MCC Purchase' when Final.Trans_Type in ('BulkSRN','BulkSRNRet','BulkSRNTrade') then 'Milk Purchase' when Final.Trans_Type in ('IC-AD','MJ-SR', " & Environment.NewLine &
              " 'M-PURRETURN','NRGP','Purchase Return','RGP','SRN') then 'Other Purchase' " & Environment.NewLine &
              " when Final.Trans_Type in ('CSA-SALE','PS-SH','PS-SR','Sale Return','SD-CSATRANS','SD-CSATRANS-RETURN','Transfer','TRN-RET') " & Environment.NewLine &
              " then (case when Final.Trans_Type in ('Sale Return','SD-CSATRANS-RETURN','TRN-RET') then 'Other Purchase' " & Environment.NewLine &
              " when Final.Trans_Type in ('Transfer') then (case when Final.InOut='I' then 'Other Purchase' else 'Product Sale' end) else 'Product Sale' end) " & Environment.NewLine &
              " when Final.Trans_Type in ('DispatchBS','DispatchBSTrade','EX_SALE_IN','MT_SALE_IN','SaleReturnBS') " & Environment.NewLine &
              " then (case when Final.Trans_Type in ('SaleReturnBS') then 'Other Purchase' " & Environment.NewLine &
              " when Item.Item_Desc like '%Curd%' or Item.Item_Desc like '%Dahi%' then 'Curd Sale' " & Environment.NewLine &
              " when Item.Item_Desc like '%Milk%' then 'Milk Sale' " & Environment.NewLine &
              " when Item.Item_Desc like '%Skimmed%' then 'Skimmed Milk Sale' else 'Others' end ) " & Environment.NewLine &
              " when Final.Trans_Type in ('FS-SH','FS-SR')  " & Environment.NewLine &
              " then (case when Item.Item_Desc like '%Milk%' then 'Fresh Product Milk Sale' " & Environment.NewLine &
              " when Item.Item_Desc like '%Chaach%' then 'Fresh Product Chaach Sale' " & Environment.NewLine &
              " when Item.Item_Desc like '%Curd%' or Item.Item_Desc like '%Dahi%' then 'Fresh Product Curd Sale' else 'Others' end) " & Environment.NewLine &
              " else 'Others' end)"
            Punching_DateCol = "Punching_Date "
        End If
        QryCond = QryCond & " and len(coalesce(TSPL_INVENTORY_MOVEMENT.Location_Code,''))>0 "
        QryCondMilk = QryCondMilk & "and len(coalesce(TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,''))>0 "

        Qry = " select Location_Code,Report_Type," & Punching_DateCol & " as  Punching_Date,sum(case when InOut='I' then coalesce(FAT_Kg,0) else -coalesce(FAT_Kg,0) end) as FAT_KG," & Environment.NewLine &
              " sum(case when InOut='I' then coalesce(SNF_Kg,0) else -coalesce(SNF_Kg,0) end) as SNF_KG " & Environment.NewLine &
              " from ( " & Environment.NewLine &
              " select Final.Product_Type,Final.Trans_Type," & ReportTypeCol & " as Report_Type, " & Environment.NewLine &
              " Final.InOut,Final.Location_Code, " & Environment.NewLine &
              " Final.Stock_Qty,Final.Stock_UOM,Final.Net_Cost,Final.Avg_Cost, " & Environment.NewLine &
              " (case when Final.Product_Type='MI' then Final.Fat_Per else  Item_Fat.Fat_Per end) as Fat_Per, " & Environment.NewLine &
              " (case when Final.Product_Type='MI' then Final.SNF_Per else  Item_SNF.SNF_Per end) as SNF_Per, " & Environment.NewLine &
              " (case when Final.Product_Type='MI' then Final.FAT_Kg else  (case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Item_Fat.Fat_Per*Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)*100) as Float) end) end) as FAT_Kg," & Environment.NewLine &
              " (case when Final.Product_Type='MI' then Final.SNF_Kg else  (case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Item_SNF.SNF_Per*Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)*100) as Float) end) end) as SNF_Kg,Punching_Date " & Environment.NewLine &
              " from ( " & Environment.NewLine &
              " select 'MP' as Product_Type,Trans_Type,InOut,(case when len(coalesce(loc.Main_Location_Code,''))>0 then (case when " & IIf(IncludeSubLocation = True, 1, 0) & "=1 then  loc.Main_Location_Code else TSPL_INVENTORY_MOVEMENT.Location_Code end) else TSPL_INVENTORY_MOVEMENT.Location_Code end) as Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,Net_Cost,Avg_Cost, " & Environment.NewLine &
              " 0 as Fat_Per,0 as SNF_Per,0 as FAT_Kg ,0 as SNF_Kg,cast(Punching_Date as date) as Punching_Date " & Environment.NewLine &
              " from TSPL_INVENTORY_MOVEMENT left join tspl_location_master loc on TSPL_INVENTORY_MOVEMENT.Location_Code=loc.Location_Code " & QryCond & " " & Environment.NewLine &
              " union all " & Environment.NewLine &
              " select  'MI' as Product_Type,Trans_Type,InOut,(case when len(coalesce(loc.Main_Location_Code,''))>0 then (case when " & IIf(IncludeSubLocation = True, 1, 0) & "=1 then  loc.Main_Location_Code else TSPL_INVENTORY_MOVEMENT_NEW.Location_Code end) else TSPL_INVENTORY_MOVEMENT_NEW.Location_Code end) as Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,Net_Cost,Avg_Cost, " & Environment.NewLine &
              " Fat_Per,SNF_Per,FAT_Kg,SNF_Kg,cast(Punching_Date as date) as Punching_Date " & Environment.NewLine &
              " from TSPL_INVENTORY_MOVEMENT_NEW left join tspl_location_master loc on TSPL_INVENTORY_MOVEMENT_NEW.Location_Code=loc.Location_Code " & QryCondMilk & " " & Environment.NewLine &
              " ) as Final " & Environment.NewLine &
              " left join TSPL_ITEM_MASTER Item on Final.Item_Code=Item.Item_Code " & Environment.NewLine &
              " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL) as Stock_SU on Final.Item_Code=Stock_SU.Item_Code and Final.Stock_UOM=Stock_SU.UOM_Code " & Environment.NewLine &
              " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG') as StockKG on Final.Item_Code=StockKG.Item_Code  " & Environment.NewLine &
              " left join (select Item_QC.Item_Code,max(Item_QC.Actual_Range) as Fat_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QC " & Environment.NewLine &
              " left outer join TSPL_PARAMETER_MASTER Params on Params.Code=Item_QC.Code where Params.Type='FAT' " & Environment.NewLine &
              " group by Item_QC.Item_Code) as Item_Fat on Final.Item_Code=Item_Fat.Item_Code " & Environment.NewLine &
              " left join (select  Item_QC.Item_Code,max(Item_QC.Actual_Range) as SNF_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QC " & Environment.NewLine &
              " left outer join TSPL_PARAMETER_MASTER Params on Params.Code=Item_QC.Code where Params.Type='SNF' " & Environment.NewLine &
              " group by Item_QC.Item_Code) Item_SNF on Final.Item_Code=Item_SNF.Item_Code " & Environment.NewLine &
              " left join TSPL_LOCATION_MASTER Loc on Final.Location_Code=Loc.Location_Code where 2=2 " & Environment.NewLine &
              " AND ( COALESCE(Item_Fat.Fat_Per,0)<>0 OR COALESCE(Item_SNF.SNF_Per,0) <>0) " & Environment.NewLine &
              " /*and Trans_Type not in ('PP_ISSUE','PP_STDN','PRD_STG_PROC','PROD_ENTRY','PROD_WR','Prod-Scrap') */" & Environment.NewLine &
              " ) as FatSNFStock group by Report_Type,Location_Code" & IIf(is_Opening = True, "", ",Punching_Date") & ""
        Return Qry
    End Function
    Public Shared Function GetDetailQry(ByVal arrLoc As ArrayList, ByVal From_Date As Date, ByVal To_Date As Date, ByVal Report_Type As String, ByVal IncludeSubLocation As Boolean)
        Dim Qry As String = ""
        Dim QryCond As String = " where 2=2 "
        Dim QryCondMilk As String = " where 2=2 "
        Dim ReportTypeCol As String = ""
        Dim Punching_DateCol As String = ""
        If IncludeSubLocation Then
            QryCond = QryCond & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(To_Date, "dd-MMM-yyyy") & "' and ( Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") or Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where Main_Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")))"
            QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(To_Date, "dd-MMM-yyyy") & "' and (Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") or Main_Location in (" & clsCommon.GetMulcallString(arrLoc) & "))"

        Else
            QryCond = QryCond & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(To_Date, "dd-MMM-yyyy") & "' and Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")"
            QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(To_Date, "dd-MMM-yyyy") & "' and Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")"

        End If
        QryCond = QryCond & " and len(coalesce(TSPL_INVENTORY_MOVEMENT.Location_Code,''))>0 "
        QryCondMilk = QryCondMilk & "and len(coalesce(TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,''))>0 "

        ReportTypeCol = "(Case when Final.Trans_Type in ('DispChallan','DispChallanRet','MilkTransferIn') " & Environment.NewLine &
          " then (case when Final.Trans_Type='DispChallan' then 'Milk Sale' else 'Milk Purchase' end) " & Environment.NewLine &
          " when  Final.Trans_Type in ('MCC-MSR','MCC-MSRN') then 'MCC Purchase' when Final.Trans_Type in ('BulkSRN','BulkSRNRet','BulkSRNTrade','IC-AD','MJ-SR', " & Environment.NewLine &
          " 'M-PURRETURN','NRGP','Purchase Return','RGP','SRN') then 'Other Purchase' " & Environment.NewLine &
          " when Final.Trans_Type in ('CSA-SALE','PS-SH','PS-SR','Sale Return','SD-CSATRANS','SD-CSATRANS-RETURN','Transfer','TRN-RET') " & Environment.NewLine &
          " then (case when Final.Trans_Type in ('Sale Return','SD-CSATRANS-RETURN','TRN-RET') then 'Other Purchase' " & Environment.NewLine &
          " when Final.Trans_Type in ('Transfer') then (case when Final.InOut='I' then 'Other Purchase' else 'Product Sale' end) else 'Product Sale' end) " & Environment.NewLine &
          " when Final.Trans_Type in ('DispatchBS','DispatchBSTrade','EX_SALE_IN','MT_SALE_IN','SaleReturnBS') " & Environment.NewLine &
          " then (case when Final.Trans_Type in ('SaleReturnBS') then 'Other Purchase' " & Environment.NewLine &
          " when Item.Item_Desc like '%Curd%' or Item.Item_Desc like '%Dahi%' then 'Curd Sale' " & Environment.NewLine &
          " when Item.Item_Desc like '%Milk%' then 'Milk Sale' " & Environment.NewLine &
          " when Item.Item_Desc like '%Skimmed%' then 'Skimmed Milk Sale' else 'Others' end ) " & Environment.NewLine &
          " when Final.Trans_Type in ('FS-SH','FS-SR')  " & Environment.NewLine &
          " then (case when Item.Item_Desc like '%Milk%' then 'Fresh Product Milk Sale' " & Environment.NewLine &
          " when Item.Item_Desc like '%Chaach%' then 'Fresh Product Chaach Sale' " & Environment.NewLine &
          " when Item.Item_Desc like '%Curd%' or Item.Item_Desc like '%Dahi%' then 'Fresh Product Curd Sale' else 'Others' end) " & Environment.NewLine &
          " else 'Others' end)"
        Punching_DateCol = "Punching_Date "

        ''(case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast(([Quantity]*[Fat Per]*Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)*100) as numeric(18,3)) end) as [FAT KG],(case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast(([Quantity]*[SNF Per]*Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)*100) as Numeric(18,3)) end) as [SNF KG]
        ''ROUND(dbo.GetConversion(Final.Item_Code,Final.Stock_UOM)*Item.Weight_Value*Final.Stock_Qty*Item_Fat.Fat_Per/100,3) end)
        ''ROUND(dbo.GetConversion(Final.Item_Code,Final.Stock_UOM)*Item.Weight_Value*Final.Stock_Qty*Item_SNF.SNF_Per/100,3) end)

        Qry = " select Final.Product_Type,Final.Trans_Type," & ReportTypeCol & " as Report_Type, " & Environment.NewLine &
              " Final.InOut,Final.Location_Code,Final.Source_Doc_No,Final.Item_Code,Item.Item_Desc, " & Environment.NewLine &
              " Final.Stock_Qty,Final.Stock_UOM,Final.Net_Cost,Final.Avg_Cost, " & Environment.NewLine &
              " (case when Final.Product_Type='MI' then Final.Fat_Per else  Item_Fat.Fat_Per end) as Fat_Per, " & Environment.NewLine &
              " (case when Final.Product_Type='MI' then Final.SNF_Per else  Item_SNF.SNF_Per end) as SNF_Per, " & Environment.NewLine &
              " (case when Final.Product_Type='MI' then Final.FAT_Kg else  (case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Item_Fat.Fat_Per*Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)*100) as Float) end) end)*(case when Final.InOut='I' then 1 else -1 end) as FAT_Kg," & Environment.NewLine &
              " (case when Final.Product_Type='MI' then Final.SNF_Kg else  (case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Item_SNF.SNF_Per*Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)*100) as Float) end) end)*(case when Final.InOut='I' then 1 else -1 end) as SNF_Kg,Punching_Date " & Environment.NewLine &
              " from ( " & Environment.NewLine &
              " select 'MP' as Product_Type,Trans_Type,InOut,Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,Net_Cost,Avg_Cost, " & Environment.NewLine &
              " 0 as Fat_Per,0 as SNF_Per,0 as FAT_Kg ,0 as SNF_Kg,cast(Punching_Date as date) as Punching_Date " & Environment.NewLine &
              " from TSPL_INVENTORY_MOVEMENT " & QryCond & " " & Environment.NewLine &
              " union all " & Environment.NewLine &
              " select  'MI' as Product_Type,Trans_Type,InOut,Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,Net_Cost,Avg_Cost, " & Environment.NewLine &
              " Fat_Per,SNF_Per,FAT_Kg,SNF_Kg,cast(Punching_Date as date) as Punching_Date " & Environment.NewLine &
              " from TSPL_INVENTORY_MOVEMENT_NEW " & QryCondMilk & " " & Environment.NewLine &
              " ) as Final " & Environment.NewLine &
              " left join TSPL_ITEM_MASTER Item on Final.Item_Code=Item.Item_Code " & Environment.NewLine &
              " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL) as Stock_SU on Final.Item_Code=Stock_SU.Item_Code and Final.Stock_UOM=Stock_SU.UOM_Code " & Environment.NewLine &
              " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG') as StockKG on Final.Item_Code=StockKG.Item_Code " & Environment.NewLine &
              " left join (select Item_QC.Item_Code,max(Item_QC.Actual_Range) as Fat_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QC " & Environment.NewLine &
              " left outer join TSPL_PARAMETER_MASTER Params on Params.Code=Item_QC.Code where Params.Type='FAT' " & Environment.NewLine &
              " group by Item_QC.Item_Code) as Item_Fat on Final.Item_Code=Item_Fat.Item_Code " & Environment.NewLine &
              " left join (select  Item_QC.Item_Code,max(Item_QC.Actual_Range) as SNF_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QC " & Environment.NewLine &
              " left outer join TSPL_PARAMETER_MASTER Params on Params.Code=Item_QC.Code where Params.Type='SNF' " & Environment.NewLine &
              " group by Item_QC.Item_Code) Item_SNF on Final.Item_Code=Item_SNF.Item_Code " & Environment.NewLine &
              " left join TSPL_LOCATION_MASTER Loc on Final.Location_Code=Loc.Location_Code where 2=2 " & Environment.NewLine &
              " AND ( COALESCE(Item_Fat.Fat_Per,0)<>0 OR COALESCE(Item_SNF.SNF_Per,0) <>0) " & Environment.NewLine &
              " /*and Trans_Type not in ('PP_ISSUE','PP_STDN','PRD_STG_PROC','PROD_ENTRY','PROD_WR','Prod-Scrap')*/ "
        Qry = "select * from (" & Qry & ") as Outermost where Report_Type='" & Report_Type & "'"
        Return Qry
    End Function
    Public Shared Function GetBaseQuery(ByVal QryCond As String, Optional ByVal WinServicePurpose As Boolean = False)
        Dim Inv As String = "TSPL_INVENTORY_MOVEMENT"
        Dim InvMilk As String = "TSPL_INVENTORY_MOVEMENT_NEW"
        Dim ExtraCol As String = "'' as OP_Type "
        If WinServicePurpose Then
            Inv = "TSPL_INVENTORY_MOVEMENT_WIN"
            InvMilk = "TSPL_INVENTORY_MOVEMENT_NEW_WIN"
            ExtraCol = " OP_TYPE "
        End If
        If clsCommon.myLen(QryCond) > 0 Then
            QryCond = " And " & QryCond
        End If
        Dim Qry As String = " select Final.IsFromMilk,Final.Product_Type,Final.Trans_Type, " & Environment.NewLine &
                   " Final.InOut,Final.Location_Code,Final.Source_Doc_No,Final.Item_Code,Item.Item_Desc, " & Environment.NewLine &
                   " Final.Stock_Qty,(CASE WHEN INOUT='I' THEN Final.Stock_Qty ELSE 0 END) AS IN_QTY,(CASE WHEN INOUT='O' THEN Final.Stock_Qty ELSE 0 END) AS OUT_QTY,Final.Stock_UOM,Final.Net_Cost ,Final.Avg_Cost,Final.FIFO_COST,Final.LIFO_COST,Final.Basic_Cost,Final.Location_Code+'Qty' as LocQty ,Final.Location_Code+'Cost' as LocCost, " & Environment.NewLine &
                   " (case when Final.Product_Type='MI' then Final.Fat_Per else  Item_Fat.Fat_Per end) as Fat_Per, " & Environment.NewLine &
                   " (case when Final.Product_Type='MI' then Final.SNF_Per else  Item_SNF.SNF_Per end) as SNF_Per, " & Environment.NewLine &
                   " (case when Final.Product_Type='MI' then Final.FAT_Kg else  (case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast((isnull(Final.Stock_Qty,0) * isnull(Item_Fat.Fat_Per,0) * isnull(Stock_SU.Conversion_Factor,0))/(coalesce(StockKG.Conversion_Factor,1)*100) as float) end) end) as FAT_Kg," & Environment.NewLine &
                   " (case when Final.Product_Type='MI' then Final.SNF_Kg else  (case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast((isnull(Final.Stock_Qty,0) * isnull(Item_SNF.SNF_Per,0) * isnull(Stock_SU.Conversion_Factor,0))/(coalesce(StockKG.Conversion_Factor,1)*100) as float) end) end) as SNF_Kg " & Environment.NewLine

        ''=======================================Monika 27/03/2017================================
        Qry += " ,Final.ACT_FAT_PER,final.ACT_SNF_PER,final.ACT_FAT_KG,final.ACT_SNF_KG " & Environment.NewLine & _
               " ,Item_Fat.fat_per as STD_FAT_PER,Item_SNF.snf_per as STD_SNF_PER " & Environment.NewLine & _
               " ,(case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast((isnull(Final.Stock_Qty,0) * isnull(Item_Fat.Fat_Per,0) * isnull(Stock_SU.Conversion_Factor,0))/(coalesce(StockKG.Conversion_Factor,1)*100) as float) end) as STD_FAT_Kg," & Environment.NewLine &
               "  (case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast((isnull(Final.Stock_Qty,0) * isnull(Item_SNF.SNF_Per,0) * isnull(Stock_SU.Conversion_Factor,0))/(coalesce(StockKG.Conversion_Factor,1)*100) as float) end) as STD_SNF_Kg " & Environment.NewLine
        ''==================================================================================

        Qry += " ,Punching_Date," & ExtraCol & " from ( " & Environment.NewLine &
                   " select 'MP' as Product_Type,Trans_Type,InOut,Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,Net_Cost,Avg_Cost,FIFO_COST,LIFO_COST,Basic_Cost, " & Environment.NewLine &
                   " 0 as Fat_Per,0 as SNF_Per,0 as FAT_Kg ,0 as SNF_Kg,cast(Punching_Date as date) as Punching_Date "
        ''=======================================Monika 27/03/2017================================
        Qry += ",Fat_Per as ACT_FAT_PER,SNF_Per as ACT_SNF_PER,FAT_Kg as ACT_FAT_KG,SNF_Kg as ACT_SNF_KG, "
        ''==================================================================================

        Qry += "" & ExtraCol & ",0 AS IsFromMilk " & Environment.NewLine &
                   " from " & Inv & " where 2=2  " & QryCond & " " & Environment.NewLine &
                   " union all " & Environment.NewLine &
                   " select  'MI' as Product_Type,Trans_Type,InOut,Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,Net_Cost,Avg_Cost,FIFO_COST,LIFO_COST,Basic_Cost, " & Environment.NewLine &
                   " Fat_Per,SNF_Per,FAT_Kg,SNF_Kg,cast(Punching_Date as date) as Punching_Date "
        ''=======================================Monika 27/03/2017================================
        Qry += ",Fat_Per as ACT_FAT_PER,SNF_Per as ACT_SNF_PER,FAT_Kg as ACT_FAT_KG,SNF_Kg as ACT_SNF_KG, "
        ''==================================================================================

        Qry += "" & ExtraCol & ",1 AS IsFromMilk " & Environment.NewLine &
                   " from " & InvMilk & "  where 2=2  " & QryCond & " " & Environment.NewLine &
                   " ) as Final " & Environment.NewLine &
                   " left join TSPL_ITEM_MASTER Item on Final.Item_Code=Item.Item_Code " & Environment.NewLine &
                   " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL) as Stock_SU on Final.Item_Code=Stock_SU.Item_Code and Final.Stock_UOM=Stock_SU.UOM_Code " & Environment.NewLine &
                   " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG') as StockKG on Final.Item_Code=StockKG.Item_Code " & Environment.NewLine &
                   " left join (select Item_QC.Item_Code,max(Item_QC.Actual_Range) as Fat_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QC " & Environment.NewLine &
                   " left outer join TSPL_PARAMETER_MASTER Params on Params.Code=Item_QC.Code where Params.Type='FAT' " & Environment.NewLine &
                   " group by Item_QC.Item_Code) as Item_Fat on Final.Item_Code=Item_Fat.Item_Code " & Environment.NewLine &
                   " left join (select  Item_QC.Item_Code,max(Item_QC.Actual_Range) as SNF_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QC " & Environment.NewLine &
                   " left outer join TSPL_PARAMETER_MASTER Params on Params.Code=Item_QC.Code where Params.Type='SNF' " & Environment.NewLine &
                   " group by Item_QC.Item_Code) Item_SNF on Final.Item_Code=Item_SNF.Item_Code " & Environment.NewLine &
                   " left join TSPL_LOCATION_MASTER Loc on Final.Location_Code=Loc.Location_Code "
        Return Qry
    End Function
    Public Shared Function GetBaseQueryTransWIN(ByVal QryCond As String) As String
        'Dim qry As String = GetBaseQuery(QryCond, True)
        Dim qry As String = "select * from View_STOCK_DATA_GIT where 2=2 AND LEN(COALESCE(LOCATION_CODE,''))>0 AND LEN(COALESCE(ITEM_CODE,''))>0"
        If clsCommon.myLen(QryCond) > 0 Then
            qry = qry & " And " & QryCond
        End If
        Dim qryWin As String = "select Item_Code,Item_Desc,Location_Code,cast(Punching_Date as date) as Punching_Date,Trans_Type,max(cast(IsFromMilk as integer)) as IsFromMilk1," & _
            " Coalesce(sum(Stock_Qty*(case when OP_Type='I' then (case when InOut='I' then 1 else 0 end) else (case when InOut='I' then -1 else 0 end) end)),0) as IN_Qty," & _
            " Coalesce(sum(Stock_Qty*(case when OP_Type='I' then (case when InOut='I' then 0 else -1 end) else (case when InOut='I' then 0 else 1 end) end)),0) as Out_Qty," & _
            " Coalesce(sum(Stock_Qty*(case when OP_Type='I' then (case when InOut='I' then 1 else -1 end) else (case when InOut='I' then -1 else 1 end) end)),0) as Stock_Qty," & _
            " Coalesce(sum(avg_Cost*(case when OP_Type='I' then (case when InOut='I' then 1 else -1 end) else (case when InOut='I' then -1 else 1 end) end)),0) as avg_Cost," & _
            " Coalesce(sum(avg_Cost*(case when OP_Type='I' then (case when InOut='I' then 1 else 0 end) else (case when InOut='I' then -1 else 0 end) end)),0) as In_Avg_Cost," & _
            " Coalesce(sum(avg_Cost*(case when OP_Type='I' then (case when InOut='I' then 0 else -1 end) else (case when InOut='I' then 0 else 1 end) end)),0) as Out_Avg_Cost," & _
            " Coalesce(sum(avg_Cost*(case when OP_Type='I' then (case when InOut='I' then 1 else -1 end) else (case when InOut='I' then -1 else 1 end) end)),0) as FIFO_Cost," & _
            " Coalesce(sum(avg_Cost*(case when OP_Type='I' then (case when InOut='I' then 1 else -1 end) else (case when InOut='I' then -1 else 1 end) end)),0) as LIFO_Cost," & _
            " Coalesce(sum(Fat_KG*(case when OP_Type='I' then (case when InOut='I' then 1 else -1 end) else (case when InOut='I' then -1 else 1 end) end)),0) as Fat_KG," & _
            " Coalesce(sum(Fat_KG*(case when OP_Type='I' then (case when InOut='I' then 1 else 0 end) else (case when InOut='I' then -1 else 0 end) end)),0) as In_Fat_KG," & _
            " Coalesce(sum(Fat_KG*(case when OP_Type='I' then (case when InOut='I' then 0 else -1 end) else (case when InOut='I' then 0 else 1 end) end)),0) as Out_Fat_KG," & _
            " Coalesce(sum(SNF_KG*(case when OP_Type='I' then (case when InOut='I' then 1 else 0 end) else (case when InOut='I' then -1 else 0 end) end)),0) as In_SNF_KG," & _
            " Coalesce(sum(SNF_KG*(case when OP_Type='I' then (case when InOut='I' then 0 else -1 end) else (case when InOut='I' then 0 else 1 end) end)),0) as Out_SNF_KG," & _
            " Coalesce(sum(SNF_KG*(case when OP_Type='I' then (case when InOut='I' then 1 else -1 end) else (case when InOut='I' then -1 else 1 end) end)),0) as SNF_KG, " & _
            " Coalesce(sum(ACT_Fat_KG*(case when OP_Type='I' then (case when InOut='I' then 1 else -1 end) else (case when InOut='I' then -1 else 1 end) end)),0) as ACTFat_KG," & _
            " Coalesce(sum(ACT_Fat_KG*(case when OP_Type='I' then (case when InOut='I' then 1 else 0 end) else (case when InOut='I' then -1 else 0 end) end)),0) as ACTIn_Fat_KG," & _
            " Coalesce(sum(ACT_Fat_KG*(case when OP_Type='I' then (case when InOut='I' then 0 else -1 end) else (case when InOut='I' then 0 else 1 end) end)),0) as ACTOut_Fat_KG," & _
            " Coalesce(sum(ACT_SNF_KG*(case when OP_Type='I' then (case when InOut='I' then 1 else 0 end) else (case when InOut='I' then -1 else 0 end) end)),0) as ACTIn_SNF_KG," & _
            " Coalesce(sum(ACT_SNF_KG*(case when OP_Type='I' then (case when InOut='I' then 0 else -1 end) else (case when InOut='I' then 0 else 1 end) end)),0) as ACTOut_SNF_KG," & _
            " Coalesce(sum(ACT_SNF_KG*(case when OP_Type='I' then (case when InOut='I' then 1 else -1 end) else (case when InOut='I' then -1 else 1 end) end)),0) as ACTSNF_KG " & _
            " from (" & qry & ") as BaseTable group by Item_Code,Item_Desc,Location_Code,cast(Punching_Date as date),Trans_Type"
        qryWin = "SELECT BaseFinal.*,Coalesce(SUOM.Stock_UOM,'') as Stock_UOM,(CASE WHEN PRODUCT_TYPE='MI' THEN 1 ELSE 0 END) AS IsFromMilk  FROM (" & qryWin & ") BaseFinal " & _
            " left join TSPL_ITEM_MASTER ITEM ON  BaseFinal.ITEM_CODE=ITEM.ITEM_CODE " & _
            " left join (select Item_Code,UOM_Code as Stock_UOM from TSPL_ITEM_UOM_DETAIL where Stocking_Unit='Y') SUOM on BaseFinal.Item_Code=SUOM.Item_Code"
        qryWin = " MERGE INTO TSPL_INV_MOVE_TRANS_DL  A" & _
                 " USING(" & qryWin & ") TA " & _
                 " ON (A.ITEM_CODE=TA.ITEM_CODE AND A.LOCATION_CODE=TA.LOCATION_CODE AND A.TRANS_DATE=TA.PUNCHING_DATE and A.TRANS_TYPE=TA.TRANS_TYPE) " & _
                 " WHEN MATCHED THEN " & _
                 " update " & _
                 " SET A.TRANS_QTY=A.TRANS_QTY+TA.STOCK_QTY,A.IN_QTY=A.IN_QTY+TA.IN_QTY,A.OUT_QTY=A.OUT_QTY+TA.OUT_QTY, " & _
                 " A.In_Fat_KG=A.In_Fat_KG+TA.In_Fat_KG,A.Out_Fat_KG=A.Out_Fat_KG+TA.Out_Fat_KG,A.Fat_KG=A.Fat_KG+TA.Fat_KG,A.In_SNF_KG=A.In_SNF_KG+TA.In_SNF_KG,A.Out_SNF_KG=A.Out_SNF_KG+TA.Out_SNF_KG,A.SNF_KG=A.SNF_KG+TA.SNF_KG,A.FIFO_Cost=A.FIFO_Cost+TA.FIFO_Cost,A.LIFO_Cost=A.LIFO_Cost+TA.LIFO_Cost,A.In_AVG_COST=A.In_AVG_COST+TA.In_AVG_COST,A.AVG_COST=A.AVG_COST+TA.AVG_COST, " & _
                 " A.CL_QTY=A.CL_QTY+TA.STOCK_QTY,A.CL_Fat_KG=A.CL_Fat_KG+TA.Fat_KG,A.CL_SNF_KG=A.CL_SNF_KG+TA.SNF_KG,A.CL_FIFO_Cost=A.CL_FIFO_Cost+TA.FIFO_Cost,A.CL_LIFO_Cost=A.CL_LIFO_Cost+TA.LIFO_Cost,A.CL_AVG_COST=A.CL_AVG_COST+TA.AVG_COST " & _
                 " ,A.QC_IN_FAT_KG=A.QC_IN_FAT_KG + TA.ACTIn_Fat_KG, A.QC_OUT_FAT_KG=a.QC_OUT_FAT_KG + TA.ACTOut_Fat_KG, A.QC_FAT_KG=A.QC_FAT_KG + TA.ACTFat_KG, A.QC_IN_SNF_KG=A.QC_IN_SNF_KG + TA.ACTIn_SNF_KG, A.QC_OUT_SNF_KG=a.QC_OUT_SNF_KG + TA.ACTOut_SNF_KG, A.QC_SNF_KG=A.QC_SNF_KG+TA.ACTSNF_KG, A.CL_QC_FAT_KG=A.CL_QC_FAT_KG+TA.ACTFAT_KG, A.CL_QC_SNF_KG=A.CL_QC_SNF_KG+TA.ACTSNF_KG,A.IsFromMilk=TA.IsFromMilk " & _
                 " WHEN NOT MATCHED THEN  " & _
                 " insert " & _
                 " (ITEM_CODE,ITEM_DESC,LOCATION_CODE,TRANS_DATE,Stock_UOM,TRANS_TYPE,IsFromMilk,IN_QTY,Out_QTY,TRANS_QTY,In_Fat_Kg,Out_Fat_Kg,Fat_KG,In_SNF_Kg,Out_SNF_Kg,SNF_KG,In_AVG_COST,Out_AVG_COST,Avg_Cost,FIFO_Cost,LIFO_Cost,CL_QTY,CL_FAT_KG,CL_SNF_KG,CL_FIFO_Cost,CL_LIFO_Cost,CL_Avg_Cost,AGEING_Flag,AGEING_QTY,QC_IN_FAT_KG,QC_OUT_FAT_KG,QC_FAT_KG,QC_IN_SNF_KG,QC_OUT_SNF_KG,QC_SNF_KG,CL_QC_FAT_KG,CL_QC_SNF_KG) " & _
                 " VALUES " & _
                 " (TA.ITEM_CODE,TA.ITEM_DESC,TA.LOCATION_CODE,TA.Punching_Date,TA.STOCK_UOM,TA.TRANS_TYPE,TA.IsFromMilk,TA.IN_QTY,TA.OUT_QTY,TA.STOCK_QTY,TA.In_Fat_Kg,TA.Out_Fat_Kg,TA.FAT_KG,TA.In_SNF_KG,TA.Out_SNF_KG,TA.SNF_KG,TA.In_AVG_COST,TA.Out_AVG_COST,TA.AVG_COST,TA.FIFO_Cost,TA.LIFO_Cost,TA.STOCK_QTY,TA.FAT_KG,TA.SNF_KG,TA.FIFO_Cost,TA.LIFO_Cost,TA.AVG_COST,0,0,TA.ACTIN_FAT_KG,TA.ACTOUT_FAT_KG,TA.ACTFAT_KG,TA.ACTIN_SNF_KG,TA.ACTOUT_SNF_KG,TA.ACTSNF_KG,TA.ACTFAT_KG,TA.ACTSNF_KG);"
        Return qryWin
    End Function
    Public Shared Function GetBaseQueryWIN(ByVal QryCond As String) As String

        Dim qry As String = "select * from View_STOCK_DATA_GIT where 2=2 AND LEN(COALESCE(LOCATION_CODE,''))>0 AND LEN(COALESCE(ITEM_CODE,''))>0 "
        If clsCommon.myLen(QryCond) > 0 Then
            qry = qry & " And " & QryCond
        End If
        Dim qryWin As String = "select Item_Code,Item_Desc,Location_Code,cast(Punching_Date as date) as Punching_Date,max(cast(IsFromMilk as integer)) as IsFromMilk1," & _
            " Coalesce(sum(Stock_Qty*(case when OP_Type='I' then (case when InOut='I' then 1 else 0 end) else (case when InOut='I' then -1 else 0 end) end)),0) as IN_Qty," & _
            " Coalesce(sum(Stock_Qty*(case when OP_Type='I' then (case when InOut='I' then 0 else -1 end) else (case when InOut='I' then 0 else 1 end) end)),0) as Out_Qty," & _
            " Coalesce(sum(Stock_Qty*(case when OP_Type='I' then (case when InOut='I' then 1 else -1 end) else (case when InOut='I' then -1 else 1 end) end)),0) as Stock_Qty," & _
            " Coalesce(sum(avg_Cost*(case when OP_Type='I' then (case when InOut='I' then 1 else -1 end) else (case when InOut='I' then -1 else 1 end) end)),0) as avg_Cost," & _
            " Coalesce(sum(avg_Cost*(case when OP_Type='I' then (case when InOut='I' then 1 else 0 end) else (case when InOut='I' then -1 else 0 end) end)),0) as In_Avg_Cost," & _
            " Coalesce(sum(avg_Cost*(case when OP_Type='I' then (case when InOut='I' then 0 else -1 end) else (case when InOut='I' then 0 else 1 end) end)),0) as Out_Avg_Cost," & _
            " Coalesce(sum(avg_Cost*(case when OP_Type='I' then (case when InOut='I' then 1 else -1 end) else (case when InOut='I' then -1 else 1 end) end)),0) as FIFO_Cost," & _
            " Coalesce(sum(avg_Cost*(case when OP_Type='I' then (case when InOut='I' then 1 else -1 end) else (case when InOut='I' then -1 else 1 end) end)),0) as LIFO_Cost," & _
            " Coalesce(sum(Fat_KG*(case when OP_Type='I' then (case when InOut='I' then 1 else -1 end) else (case when InOut='I' then -1 else 1 end) end)),0) as Fat_KG," & _
            " Coalesce(sum(Fat_KG*(case when OP_Type='I' then (case when InOut='I' then 1 else 0 end) else (case when InOut='I' then -1 else 0 end) end)),0) as In_Fat_KG," & _
            " Coalesce(sum(Fat_KG*(case when OP_Type='I' then (case when InOut='I' then 0 else -1 end) else (case when InOut='I' then 0 else 1 end) end)),0) as Out_Fat_KG," & _
            " Coalesce(sum(SNF_KG*(case when OP_Type='I' then (case when InOut='I' then 1 else 0 end) else (case when InOut='I' then -1 else 0 end) end)),0) as In_SNF_KG," & _
            " Coalesce(sum(SNF_KG*(case when OP_Type='I' then (case when InOut='I' then 0 else -1 end) else (case when InOut='I' then 0 else 1 end) end)),0) as Out_SNF_KG," & _
            " Coalesce(sum(SNF_KG*(case when OP_Type='I' then (case when InOut='I' then 1 else -1 end) else (case when InOut='I' then -1 else 1 end) end)),0) as SNF_KG, " & _
            " Coalesce(sum(ACT_Fat_KG*(case when OP_Type='I' then (case when InOut='I' then 1 else -1 end) else (case when InOut='I' then -1 else 1 end) end)),0) as ACTFat_KG," & _
            " Coalesce(sum(ACT_Fat_KG*(case when OP_Type='I' then (case when InOut='I' then 1 else 0 end) else (case when InOut='I' then -1 else 0 end) end)),0) as ACTIn_Fat_KG," & _
            " Coalesce(sum(ACT_Fat_KG*(case when OP_Type='I' then (case when InOut='I' then 0 else -1 end) else (case when InOut='I' then 0 else 1 end) end)),0) as ACTOut_Fat_KG," & _
            " Coalesce(sum(ACT_SNF_KG*(case when OP_Type='I' then (case when InOut='I' then 1 else 0 end) else (case when InOut='I' then -1 else 0 end) end)),0) as ACTIn_SNF_KG," & _
            " Coalesce(sum(ACT_SNF_KG*(case when OP_Type='I' then (case when InOut='I' then 0 else -1 end) else (case when InOut='I' then 0 else 1 end) end)),0) as ACTOut_SNF_KG," & _
            " Coalesce(sum(ACT_SNF_KG*(case when OP_Type='I' then (case when InOut='I' then 1 else -1 end) else (case when InOut='I' then -1 else 1 end) end)),0) as ACTSNF_KG " & _
            " from (" & qry & ") as BaseTable group by Item_Code,Item_Desc,Location_Code,cast(Punching_Date as date)"
        qryWin = "SELECT BaseFinal.*,Coalesce(SUOM.Stock_UOM,'') as Stock_UOM,(CASE WHEN PRODUCT_TYPE='MI' THEN 1 ELSE 0 END) AS IsFromMilk  FROM (" & qryWin & ") BaseFinal " & _
            " left join TSPL_ITEM_MASTER ITEM ON  BaseFinal.ITEM_CODE=ITEM.ITEM_CODE " & _
            " left join (select Item_Code,UOM_Code as Stock_UOM from TSPL_ITEM_UOM_DETAIL where Stocking_Unit='Y') SUOM on BaseFinal.Item_Code=SUOM.Item_Code"
        qryWin = " MERGE INTO TSPL_INV_MOVE_DL  A" & _
                 " USING(" & qryWin & ") TA " & _
                 " ON (A.ITEM_CODE=TA.ITEM_CODE AND A.LOCATION_CODE=TA.LOCATION_CODE AND A.TRANS_DATE=TA.PUNCHING_DATE) " & _
                 " WHEN MATCHED THEN " & _
                 " update " & _
                 " SET A.TRANS_QTY=A.TRANS_QTY+TA.STOCK_QTY,A.IN_QTY=A.IN_QTY+TA.IN_QTY,A.OUT_QTY=A.OUT_QTY+TA.OUT_QTY, " & _
                 " A.In_Fat_KG=A.In_Fat_KG+TA.In_Fat_KG,A.Out_Fat_KG=A.Out_Fat_KG+TA.Out_Fat_KG,A.Fat_KG=A.Fat_KG+TA.Fat_KG,A.In_SNF_KG=A.In_SNF_KG+TA.In_SNF_KG,A.Out_SNF_KG=A.Out_SNF_KG+TA.Out_SNF_KG,A.SNF_KG=A.SNF_KG+TA.SNF_KG,A.FIFO_Cost=A.FIFO_Cost+TA.FIFO_Cost,A.LIFO_Cost=A.LIFO_Cost+TA.LIFO_Cost,A.In_AVG_COST=A.In_AVG_COST+TA.In_AVG_COST,A.AVG_COST=A.AVG_COST+TA.AVG_COST, " & _
                 " A.CL_QTY=A.CL_QTY+TA.STOCK_QTY,A.CL_Fat_KG=A.CL_Fat_KG+TA.Fat_KG,A.CL_SNF_KG=A.CL_SNF_KG+TA.SNF_KG,A.CL_FIFO_Cost=A.CL_FIFO_Cost+TA.FIFO_Cost,A.CL_LIFO_Cost=A.CL_LIFO_Cost+TA.LIFO_Cost,A.CL_AVG_COST=A.CL_AVG_COST+TA.AVG_COST " & _
                 " ,A.QC_IN_FAT_KG=A.QC_IN_FAT_KG + TA.ACTIn_Fat_KG, A.QC_OUT_FAT_KG=a.QC_OUT_FAT_KG + TA.ACTOut_Fat_KG, A.QC_FAT_KG=A.QC_FAT_KG + TA.ACTFat_KG, A.QC_IN_SNF_KG=A.QC_IN_SNF_KG + TA.ACTIn_SNF_KG, A.QC_OUT_SNF_KG=a.QC_OUT_SNF_KG + TA.ACTOut_SNF_KG, A.QC_SNF_KG=A.QC_SNF_KG+TA.ACTSNF_KG, A.CL_QC_FAT_KG=A.CL_QC_FAT_KG+TA.ACTFAT_KG, A.CL_QC_SNF_KG=A.CL_QC_SNF_KG+TA.ACTSNF_KG,A.IsFromMilk=TA.IsFromMilk " & _
                 " WHEN NOT MATCHED THEN  " & _
                 " insert " & _
                 " (ITEM_CODE,ITEM_DESC,LOCATION_CODE,TRANS_DATE,Stock_UOM,IsFromMilk,IN_QTY,Out_QTY,TRANS_QTY,In_Fat_Kg,Out_Fat_Kg,Fat_KG,In_SNF_Kg,Out_SNF_Kg,SNF_KG,In_AVG_COST,Out_AVG_COST,Avg_Cost,FIFO_Cost,LIFO_Cost,CL_QTY,CL_FAT_KG,CL_SNF_KG,CL_FIFO_Cost,CL_LIFO_Cost,CL_Avg_Cost,AGEING_Flag,AGEING_QTY,QC_IN_FAT_KG,QC_OUT_FAT_KG,QC_FAT_KG,QC_IN_SNF_KG,QC_OUT_SNF_KG,QC_SNF_KG,CL_QC_FAT_KG,CL_QC_SNF_KG) " & _
                 " VALUES " & _
                 " (TA.ITEM_CODE,TA.ITEM_DESC,TA.LOCATION_CODE,TA.Punching_Date,TA.STOCK_UOM,TA.IsFromMilk,TA.IN_QTY,TA.OUT_QTY,TA.STOCK_QTY,TA.In_Fat_Kg,TA.Out_Fat_Kg,TA.FAT_KG,TA.In_SNF_KG,TA.Out_SNF_KG,TA.SNF_KG,TA.In_AVG_COST,TA.Out_AVG_COST,TA.AVG_COST,TA.FIFO_Cost,TA.LIFO_Cost,TA.STOCK_QTY,TA.FAT_KG,TA.SNF_KG,TA.FIFO_Cost,TA.LIFO_Cost,TA.AVG_COST,0,0,TA.ACTIN_FAT_KG,TA.ACTOUT_FAT_KG,TA.ACTFAT_KG,TA.ACTIN_SNF_KG,TA.ACTOUT_SNF_KG,TA.ACTSNF_KG,TA.ACTFAT_KG,TA.ACTSNF_KG);"
        Return qryWin
    End Function
    Public Shared Function ReturnClosingUpdateTransQry(ByVal QryCond As String, Optional ByVal Temp_Filter As Boolean = False) As String
        ''''AND CL.TRANS_DATE=TEMP_DATA.TRANS_DATE
        Dim strTempFilter As String = ""
        Dim strTempFilterMilk As String = ""
        If Temp_Filter Then
            strTempFilter = "inner join Inv_TEMP on TSPL_INVENTORY_MOVEMENT_WIN.Trans_id=Inv_Temp.Trans_Id and TSPL_INVENTORY_MOVEMENT_WIN.Source_Doc_No=Inv_Temp.Source_Doc_No"
            strTempFilterMilk = "inner join Inv_Milk_TEMP on TSPL_INVENTORY_MOVEMENT_NEW_WIN.Trans_id=Inv_Milk_TEMP.Trans_Id and TSPL_INVENTORY_MOVEMENT_NEW_WIN.Source_Doc_No=Inv_Milk_TEMP.Source_Doc_No"
        End If
        Dim qry As String = " MERGE INTO TSPL_INV_MOVE_TRANS_DL  A" & _
                            " USING(SELECT CL.ITEM_CODE,CL.LOCATION_CODE,CL.TRANS_DATE,CL.STOCK_UOM,CL.TRANS_TYPE,CL.TRANS_QTY,CL.Fat_KG,CL.SNF_KG,CL.Avg_Cost,CL.FIFO_COST,CL.LIFO_COST," & _
                            " CL.CL_TRANS_QTY,CL.CL_Fat_KG,CL.CL_SNF_KG,CL.CL_Avg_Cost,CL.CL_FIFO_COST,CL.CL_LIFO_COST,CL.QC_FAT_KG,CL.QC_SNF_KG,CL.CL_QC_FAT_KG,CL.CL_QC_SNF_KG FROM ( " & _
                            " select TSPL_INV_MOVE_TRANS_DL.Item_Code,Location_Code,TRANS_DATE,Stock_UOM,TRANS_TYPE,TRANS_QTY,Fat_KG,SNF_KG,QC_FAT_KG,QC_SNF_KG,Avg_Cost,FIFO_COST,LIFO_COST, " & _
                            " sum(TRANS_QTY) over (partition by Item_Code,Location_Code order by Item_Code,Location_Code,TRANS_TYPE,TRANS_DATE) as CL_TRANS_QTY, " & _
                            " sum(Fat_KG) over (partition by Item_Code,Location_Code order by Item_Code,Location_Code,TRANS_TYPE,TRANS_DATE) as CL_Fat_KG, " & _
                            " sum(SNF_KG) over (partition by Item_Code,Location_Code order by Item_Code,Location_Code,TRANS_TYPE,TRANS_DATE) as CL_SNF_KG, " & _
                            " sum(QC_Fat_KG) over (partition by Item_Code,Location_Code order by Item_Code,Location_Code,TRANS_TYPE,TRANS_DATE) as CL_QC_Fat_KG, " & _
                            " sum(QC_SNF_KG) over (partition by Item_Code,Location_Code order by Item_Code,Location_Code,TRANS_TYPE,TRANS_DATE) as CL_QC_SNF_KG, " & _
                            " sum(Avg_Cost) over (partition by Item_Code,Location_Code order by Item_Code,Location_Code,TRANS_TYPE,TRANS_DATE) as CL_Avg_Cost, " & _
                            " sum(FIFO_COST) over (partition by Item_Code,Location_Code order by Item_Code,Location_Code,TRANS_TYPE,TRANS_DATE) as CL_FIFO_COST, " & _
                            " sum(LIFO_COST) over (partition by Item_Code,Location_Code order by Item_Code,Location_Code,TRANS_TYPE,TRANS_DATE) as CL_LIFO_COST " & _
                            " from TSPL_INV_MOVE_TRANS_DL) CL " & _
                            " INNER JOIN (SELECT DISTINCT Item_Code,Location_Code FROM " & _
                            " (SELECT Item_Code,Location_Code,TRANS_TYPE,cast(Punching_Date as date) AS TRANS_DATE FROM TSPL_INVENTORY_MOVEMENT_WIN " & strTempFilter & " " & _
                            " UNION ALL " & _
                            " SELECT Item_Code,Location_Code,TRANS_TYPE,cast(Punching_Date as date) AS TRANS_DATE FROM TSPL_INVENTORY_MOVEMENT_NEW_WIN " & strTempFilterMilk & ") TEMP  WHERE 2=2 " & QryCond & " ) TEMP_DATA ON CL.Item_Code=TEMP_DATA.ITEM_CODE " & _
                            " AND CL.LOCATION_CODE=TEMP_DATA.LOCATION_CODE  " & _
                            " ) TA  " & _
                            " ON (A.ITEM_CODE=TA.ITEM_CODE AND A.LOCATION_CODE=TA.LOCATION_CODE AND A.TRANS_DATE=TA.TRANS_DATE AND A.STOCK_UOM=TA.STOCK_UOM AND A.TRANS_TYPE=TA.TRANS_TYPE) " & _
                            " WHEN MATCHED THEN  " & _
                            " update " & _
                            " SET A.CL_QTY=TA.CL_TRANS_QTY,A.CL_Fat_KG=TA.CL_Fat_KG,A.CL_SNF_KG=TA.CL_SNF_KG,A.CL_AVG_COST=TA.CL_AVG_COST,A.CL_FIFO_COST=TA.CL_FIFO_COST,A.CL_LIFO_COST=TA.CL_LIFO_COST,A.CL_QC_Fat_KG=TA.CL_QC_Fat_KG,A.CL_QC_SNF_KG=TA.CL_QC_SNF_KG;"

        Return qry
    End Function
    Public Shared Function ReturnClosingUpdateQry(ByVal QryCond As String, Optional ByVal Temp_Filter As Boolean = False) As String
        ''''AND CL.TRANS_DATE=TEMP_DATA.TRANS_DATE
        Dim strTempFilter As String = ""
        Dim strTempFilterMilk As String = ""
        If Temp_Filter Then
            strTempFilter = "inner join Inv_TEMP on TSPL_INVENTORY_MOVEMENT_WIN.Trans_id=Inv_Temp.Trans_Id and TSPL_INVENTORY_MOVEMENT_WIN.Source_Doc_No=Inv_Temp.Source_Doc_No"
            strTempFilterMilk = "inner join Inv_Milk_TEMP on TSPL_INVENTORY_MOVEMENT_NEW_WIN.Trans_id=Inv_Milk_TEMP.Trans_Id and TSPL_INVENTORY_MOVEMENT_NEW_WIN.Source_Doc_No=Inv_Milk_TEMP.Source_Doc_No"
        End If
        Dim qry As String = " MERGE INTO TSPL_INV_MOVE_DL  A" & _
                            " USING(SELECT CL.ITEM_CODE,CL.LOCATION_CODE,CL.TRANS_DATE,CL.STOCK_UOM,CL.TRANS_QTY,CL.Fat_KG,CL.SNF_KG,CL.Avg_Cost,CL.FIFO_COST,CL.LIFO_COST," & _
                            " CL.CL_TRANS_QTY,CL.CL_Fat_KG,CL.CL_SNF_KG,CL.CL_Avg_Cost,CL.CL_FIFO_COST,CL.CL_LIFO_COST,CL.QC_FAT_KG,CL.QC_SNF_KG,CL.CL_QC_FAT_KG,CL.CL_QC_SNF_KG FROM ( " & _
                            " select TSPL_INV_MOVE_DL.Item_Code,Location_Code,TRANS_DATE,Stock_UOM,TRANS_QTY,Fat_KG,SNF_KG,QC_FAT_KG,QC_SNF_KG,Avg_Cost,FIFO_COST,LIFO_COST, " & _
                            " sum(TRANS_QTY) over (partition by Item_Code,Location_Code order by Item_Code,Location_Code,TRANS_DATE) as CL_TRANS_QTY, " & _
                            " sum(Fat_KG) over (partition by Item_Code,Location_Code order by Item_Code,Location_Code,TRANS_DATE) as CL_Fat_KG, " & _
                            " sum(SNF_KG) over (partition by Item_Code,Location_Code order by Item_Code,Location_Code,TRANS_DATE) as CL_SNF_KG, " & _
                            " sum(QC_Fat_KG) over (partition by Item_Code,Location_Code order by Item_Code,Location_Code,TRANS_DATE) as CL_QC_Fat_KG, " & _
                            " sum(QC_SNF_KG) over (partition by Item_Code,Location_Code order by Item_Code,Location_Code,TRANS_DATE) as CL_QC_SNF_KG, " & _
                            " sum(Avg_Cost) over (partition by Item_Code,Location_Code order by Item_Code,Location_Code,TRANS_DATE) as CL_Avg_Cost, " & _
                            " sum(FIFO_COST) over (partition by Item_Code,Location_Code order by Item_Code,Location_Code,TRANS_DATE) as CL_FIFO_COST, " & _
                            " sum(LIFO_COST) over (partition by Item_Code,Location_Code order by Item_Code,Location_Code,TRANS_DATE) as CL_LIFO_COST " & _
                            " from TSPL_INV_MOVE_DL) CL " & _
                            " INNER JOIN (SELECT DISTINCT Item_Code,Location_Code FROM " & _
                            " (SELECT Item_Code,Location_Code,TRANS_TYPE,cast(Punching_Date as date) AS TRANS_DATE FROM TSPL_INVENTORY_MOVEMENT_WIN " & strTempFilter & " " & _
                            " UNION ALL " & _
                            " SELECT Item_Code,Location_Code,TRANS_TYPE,cast(Punching_Date as date) AS TRANS_DATE FROM TSPL_INVENTORY_MOVEMENT_NEW_WIN " & strTempFilterMilk & ") TEMP  WHERE 2=2 " & QryCond & " ) TEMP_DATA ON CL.Item_Code=TEMP_DATA.ITEM_CODE " & _
                            " AND CL.LOCATION_CODE=TEMP_DATA.LOCATION_CODE  " & _
                            " ) TA  " & _
                            " ON (A.ITEM_CODE=TA.ITEM_CODE AND A.LOCATION_CODE=TA.LOCATION_CODE AND A.TRANS_DATE=TA.TRANS_DATE AND A.STOCK_UOM=TA.STOCK_UOM) " & _
                            " WHEN MATCHED THEN  " & _
                            " update " & _
                            " SET A.CL_QTY=TA.CL_TRANS_QTY,A.CL_Fat_KG=TA.CL_Fat_KG,A.CL_SNF_KG=TA.CL_SNF_KG,A.CL_AVG_COST=TA.CL_AVG_COST,A.CL_FIFO_COST=TA.CL_FIFO_COST,A.CL_LIFO_COST=TA.CL_LIFO_COST,A.CL_QC_Fat_KG=TA.CL_QC_Fat_KG,A.CL_QC_SNF_KG=TA.CL_QC_SNF_KG;"

        Return qry
    End Function
    'Public Shared Function GetUpdateInventorySummaryQry(ByVal QryCond As String) As String
    '    Dim qryWin As String = GetBaseQueryWIN(QryCond)
    '    Dim qrySummaryUpdate As String = "update TSPL_INV_MOVE_DL set TRANS_QTY=coalesce(TempData.Stock_Qty,0),IN_QTY=coalesce(TempData.IN_QTY,0),OUT_QTY=coalesce(TempData.OUT_QTY,0),Avg_Cost=coalesce(TempData.Avg_Cost,0),Fat_KG=coalesce(TempData.Fat_KG,0),SNF_KG=coalesce(TempData.SNF_KG,0) from (" & qryWin & ") as TempData where TSPL_INV_MOVE_DL.Item_Code=TempData.Item_Code and TSPL_INV_MOVE_DL.Location_Code=TempData.Location_Code and TSPL_INV_MOVE_DL.TRANS_DATE=TempData.Punching_Date"
    '    Return qrySummaryUpdate
    'End Function
    ''richa agarwal 5 Dec,2018

    ''richa agarwal 5 Dec,2018
    Public Shared Function UpdateInvControlAccount(ByVal strSourceDocNo As String, ByVal strSourceDocType As String, ByVal strItemCode As String, ByVal strInvControlAccDr As String, ByVal strInvControlAccCr As String, ByVal strInOutType As String, ByVal trans As SqlTransaction) As Boolean
        Return UpdateInvControlAccount(strSourceDocNo, strSourceDocType, strItemCode, strInvControlAccDr, strInvControlAccCr, strInOutType, "", trans)
    End Function
    Public Shared Function UpdateInvControlAccount(ByVal strSourceDocNo As String, ByVal strSourceDocType As String, ByVal strItemCode As String, ByVal strInvControlAccDr As String, ByVal strInvControlAccCr As String, ByVal strInOutType As String, ByVal ExtraWherClaus As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = String.Empty
            qry = " update "
            If clsCommon.CompairString(clsCommon.myCstr(clsItemMaster.GetItemProductType(strItemCode, trans)), "MI") = CompairStringResult.Equal Then
                qry += " TSPL_INVENTORY_MOVEMENT_New "
            Else
                qry += " TSPL_INVENTORY_MOVEMENT "
            End If
            qry += " SET Inventory_DrAcc='" & strInvControlAccDr & "',Inventory_CrAcc='" & strInvControlAccCr & "' WHERE Source_Doc_No='" & strSourceDocNo & "' AND Trans_Type ='" & strSourceDocType & "' AND Item_Code ='" & strItemCode & "'"
            If clsCommon.myLen(strInOutType) > 0 Then
                qry += " AND InOut='" & strInOutType & "' "
            End If
            If clsCommon.myLen(ExtraWherClaus) > 0 Then
                qry += " and " + ExtraWherClaus
            End If
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function UpdateInvSummaryData(ByVal QryCond As String, ByVal trans As SqlTransaction) As Boolean
        If clsCommon.myLen(QryCond) > 0 Then
            QryCond = " and " & QryCond
        End If
        '' lock GIT Tables 
        Dim qry As String = "select top 1 * from TSPL_INVENTORY_MOVEMENT_NEW_WIN with (TABLOCKX);"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        qry = "select top 1 * from TSPL_INVENTORY_MOVEMENT_WIN  with (TABLOCKX);"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        '' update summary transaction data
        qry = GetBaseQueryTransWIN(QryCond)
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        '' UPDATE CLOSING DATA
        qry = ReturnClosingUpdateTransQry(QryCond)
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        '' update summary transaction data
        qry = GetBaseQueryWIN(QryCond)
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        '' UPDATE CLOSING DATA
        qry = ReturnClosingUpdateQry(QryCond)
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        ''update Ageing Flag
        'qry = GetAgeingFlagUpdateQuery()
        'clsDBFuncationality.ExecuteNonQuery(qry, trans)

        ' ''update Ageing Qty
        'qry = GetAgeingQtyUpdateQuery()
        'clsDBFuncationality.ExecuteNonQuery(qry, trans)

        ' '' update AGAIN AGEING FLAG
        'qry = GetAgeingQtyUpdateAfterAgeingQuery()
        'clsDBFuncationality.ExecuteNonQuery(qry, trans)

        qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW_WIN where 2=2  " & QryCond & ""
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        qry = "delete from TSPL_INVENTORY_MOVEMENT_WIN where 2=2  " & QryCond & ""
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Return True
    End Function
    Public Shared Function GetAgeingFlagUpdateQuery() As String
        Dim qry As String = GetAgeingBaseQuery()
        'qry = " update TSPL_INV_MOVE_DL set AGEING_Flag=T1.AGEING_Flag FROM (" & qry & ") T1 " & _
        '      " WHERE TSPL_INV_MOVE_DL.Item_Code=T1.Item_Code AND TSPL_INV_MOVE_DL.Location_Code=T1.Location_Code AND TSPL_INV_MOVE_DL.TRANS_DATE=T1.TRANS_DATE AND TSPL_INV_MOVE_DL.STOCK_UOM=T1.STOCK_UOM"
        qry = " sp_updateageingqty_job "
        Return qry
    End Function
    Public Shared Function GetAgeingQtyUpdateQuery() As String
        Dim qry As String = GetAgeingBaseQuery()
        qry = " update TSPL_INV_MOVE_DL set AGEING_QTY=T1.AGEING_QTY FROM (" & qry & ") T1 " & _
              " WHERE TSPL_INV_MOVE_DL.Item_Code=T1.Item_Code AND TSPL_INV_MOVE_DL.Location_Code=T1.Location_Code AND TSPL_INV_MOVE_DL.TRANS_DATE=T1.TRANS_DATE AND TSPL_INV_MOVE_DL.STOCK_UOM=T1.STOCK_UOM"
        Return qry
    End Function
    Public Shared Function GetAgeingQtyUpdateAfterAgeingQuery() As String
        Dim qry As String = GetAgeingBaseQuery()
        qry = " UPDATE TSPL_INV_MOVE_DL SET AGEING_Flag=T2.AGEING_Flag FROM ( select  TSPL_INV_MOVE_DL.Item_Code,TSPL_INV_MOVE_DL.Location_Code,TSPL_INV_MOVE_DL.STOCK_UOM, max(T1.AGEING_Flag) AS AGEING_Flag,max(T1.TRANS_DATE) AS TRANS_DATE FROM TSPL_INV_MOVE_DL " & _
              " INNER JOIN (" & qry & ") T1 " & _
              " ON TSPL_INV_MOVE_DL.Item_Code=T1.Item_Code AND TSPL_INV_MOVE_DL.Location_Code=T1.Location_Code and TSPL_INV_MOVE_DL.STOCK_UOM=T1.STOCK_UOM AND TSPL_INV_MOVE_DL.TRANS_DATE>T1.TRANS_DATE group by TSPL_INV_MOVE_DL.Item_Code,TSPL_INV_MOVE_DL.Location_Code,TSPL_INV_MOVE_DL.STOCK_UOM) AS T2 " & _
              " WHERE TSPL_INV_MOVE_DL.Item_Code=T2.Item_Code AND TSPL_INV_MOVE_DL.Location_Code=T2.Location_Code AND TSPL_INV_MOVE_DL.TRANS_DATE=T2.TRANS_DATE AND TSPL_INV_MOVE_DL.STOCK_UOM=T2.STOCK_UOM"
        Return qry
    End Function
    Public Shared Function GetAgeingBaseQuery()
        Dim qry As String = " select TSPL_INV_MOVE_DL.Item_Code,TSPL_INV_MOVE_DL.Location_Code,TSPL_INV_MOVE_DL.TRANS_DATE,TSPL_INV_MOVE_DL.STOCK_UOM,max(TSPL_INV_MOVE_DL.IN_QTY)  as Trans_In," & _
              " sum(Inv_Temp.IN_QTY) as IN_QTY,sum(Inv_Temp.OUT_QTY) as OUT_QTY, " & _
              " sum(Inv_Temp.Trans_Qty) as Trans_Qty,(case when max(TSPL_INV_MOVE_DL.IN_QTY)<=0 then 0 else (sum(Inv_Temp.IN_QTY)+max(TSPL_INV_MOVE_DL.IN_QTY)) end) as AGEING_QTY," & _
              " (case when max(TSPL_INV_MOVE_DL.IN_QTY)<=0 then 0 else 1 end) as AGEING_Flag from TSPL_INV_MOVE_DL " & _
              " inner join (select TRANS_DATE,Item_Code,Location_Code,sum(IN_QTY) as IN_QTY,sum(OUT_QTY) as OUT_QTY,sum(Trans_Qty) as Trans_Qty from TSPL_INV_MOVE_DL" & _
              " group by TRANS_DATE,Item_Code,Location_Code) as Inv_Temp " & _
              " on TSPL_INV_MOVE_DL.Item_Code=Inv_Temp.Item_Code and TSPL_INV_MOVE_DL.Location_Code=Inv_Temp.Location_Code and Inv_Temp.TRANS_DATE<=TSPL_INV_MOVE_DL.TRANS_DATE and TSPL_INV_MOVE_DL.AGEING_Flag=0 " & _
              " where TSPL_INV_MOVE_DL.Item_Code in(select distinct Item_Code from TSPL_INVENTORY_MOVEMENT_WIN union all select Item_Code from TSPL_INVENTORY_MOVEMENT_NEW_WIN) " & _
              " and TSPL_INV_MOVE_DL.Location_Code in(select distinct Location_Code from TSPL_INVENTORY_MOVEMENT_WIN union all select Location_Code from TSPL_INVENTORY_MOVEMENT_NEW_WIN) " & _
              " group by TSPL_INV_MOVE_DL.Item_Code,TSPL_INV_MOVE_DL.Location_Code,TSPL_INV_MOVE_DL.TRANS_DATE,TSPL_INV_MOVE_DL.STOCK_UOM "
        Return qry
    End Function

    Public Shared Function GetBaseQueryWithOpening(ByVal QryCond As String, ByVal QryCond1 As String, ByVal Type As String)
        Dim Qry As String = ""
        Qry += " select Final.Product_Type,Final.Trans_Type, "
        Qry += " Final.InOut,Final.Location_Code,loc.Location_Desc,Final.Source_Doc_No,Final.Item_Code,Item.Item_Desc, "
        Qry += " Final.Stock_Qty,Final.Stock_UOM,Final.Net_Cost,Final.Avg_Cost, "
        Qry += " (case when Final.Product_Type='MI' then Final.Fat_Per else  Item_Fat.Fat_Per end) as Fat_Per, "
        Qry += " (case when Final.Product_Type='MI' then Final.SNF_Per else  Item_SNF.SNF_Per end) as SNF_Per, "
        Qry += " (case when Final.Product_Type='MI' then Final.FAT_Kg else  (case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Item_Fat.Fat_Per*Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)*100) as numeric(18,3)) end) end)*(case when Final.InOut='I' then 1 else -1 end) as FAT_Kg," & Environment.NewLine & ""
        Qry += " (case when Final.Product_Type='MI' then Final.SNF_Kg else  (case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Item_SNF.SNF_Per*Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)*100) as Numeric(18,3)) end) end)*(case when Final.InOut='I' then 1 else -1 end) as SNF_Kg,Punching_Date " & Environment.NewLine & ""
        Qry += " from ( "
        'If clsCommon.CompairString(Type, "Milk") = CompairStringResult.Equal Then
        Qry += " select 'MP' as Product_Type,Trans_Type,InOut,Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,Net_Cost,Avg_Cost, "
        Qry += " 0 as Fat_Per,0 as SNF_Per,0 as FAT_Kg ,0 as SNF_Kg,cast(Punching_Date as date) as Punching_Date "
        Qry += " from TSPL_INVENTORY_MOVEMENT " & QryCond & " "
        'Else
        Qry += " union all "
        Qry += " select  'MI' as Product_Type,Trans_Type,InOut,Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,Net_Cost,Avg_Cost, "
        Qry += " Fat_Per,SNF_Per,FAT_Kg,SNF_Kg,cast(Punching_Date as date) as Punching_Date "
        Qry += " from TSPL_INVENTORY_MOVEMENT_NEW " & QryCond & " "
        'End If
        Qry += " union all "
        'If clsCommon.CompairString(Type, "Milk") = CompairStringResult.Equal Then
        Qry += " select 'MP' as Product_Type,Trans_Type,'I' as InOut,Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,Net_Cost,Avg_Cost,  "
        Qry += " 0 as Fat_Per,0 as SNF_Per,0 as FAT_Kg ,0 as SNF_Kg, Punching_Date  "
        Qry += " from TSPL_INVENTORY_MOVEMENT  " & QryCond1 & " "
        'Else
        Qry += " union all  "
        Qry += " select  'MI' as Product_Type,Trans_Type,'I' as InOut,Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,Net_Cost,Avg_Cost,  "
        Qry += " Fat_Per,SNF_Per,FAT_Kg,SNF_Kg,Punching_Date  "
        Qry += " from TSPL_INVENTORY_MOVEMENT_NEW " & QryCond1 & " "
        'End If
        Qry += " ) as Final "
        Qry += " left join TSPL_ITEM_MASTER Item on Final.Item_Code=Item.Item_Code "
        Qry += " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL) as Stock_SU on Final.Item_Code=Stock_SU.Item_Code and Final.Stock_UOM=Stock_SU.UOM_Code "
        Qry += " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG') as StockKG on Final.Item_Code=StockKG.Item_Code "
        Qry += " left join (select Item_QC.Item_Code,max(Item_QC.Actual_Range) as Fat_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QC "
        Qry += " left outer join TSPL_PARAMETER_MASTER Params on Params.Code=Item_QC.Code where Params.Type='FAT' "
        Qry += " group by Item_QC.Item_Code) as Item_Fat on Final.Item_Code=Item_Fat.Item_Code "
        Qry += " left join (select  Item_QC.Item_Code,max(Item_QC.Actual_Range) as SNF_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QC "
        Qry += " left outer join TSPL_PARAMETER_MASTER Params on Params.Code=Item_QC.Code where Params.Type='SNF' "
        Qry += " group by Item_QC.Item_Code) Item_SNF on Final.Item_Code=Item_SNF.Item_Code "
        Qry += " left join TSPL_LOCATION_MASTER Loc on Final.Location_Code=Loc.Location_Code "
        Return Qry
    End Function
    Public Shared Function GetBaseQueryForItemLocationBalance(ByVal objFilter As clsStockRecoFilters, ByVal isForWinService As Boolean) As String
        Dim condBaseQry As String = " 2=2 "
        Dim condOpening As String = " 2=2 "
        Dim Table_Name As String = "TSPL_INV_MOVE_DL"

        'If Not objFilter.arrLoc Is Nothing AndAlso objFilter.arrLoc.Count > 0 Then
        '    condOpening = condOpening & " and Location_Code in (" & clsCommon.GetMulcallString(objFilter.arrLoc) & ")"
        '    condBaseQry = condBaseQry & " and  Location_Code in (" & clsCommon.GetMulcallString(objFilter.arrLoc) & ")"
        'End If
        If Not objFilter.arrItem Is Nothing AndAlso objFilter.arrItem.Count > 0 Then
            condOpening = condOpening & " and Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
            condBaseQry = condBaseQry & " and  Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
        End If
        If Not objFilter.arrTransaction Is Nothing AndAlso objFilter.arrTransaction.Count > 0 Then
            Table_Name = "TSPL_INV_MOVE_TRANS_DL"
            condOpening = condOpening & " and Trans_Type in (" & clsCommon.GetMulcallString(objFilter.arrTransaction) & ")"
            condBaseQry = condBaseQry & " and  Trans_Type in (" & clsCommon.GetMulcallString(objFilter.arrTransaction) & ")"
        End If
        If objFilter.From_Date <> Nothing Then
            condOpening = condOpening & " and Trans_Date < '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' "
            condBaseQry = condBaseQry & " and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' "
        Else
            Throw New Exception("Date is Mandatory")
        End If
        Dim QryBalBaseGIT As String = GetBaseQuery(condBaseQry, True)

        Dim QryBalGIT As String = "select TRANS_DATE,Location_Code,Item_Code,Stock_UOM,max(IsFromMilk) as IsFromMilk,sum(FIFO_Cost) as FIFO_Cost,sum(LIFO_Cost) as LIFO_Cost, " & _
            " sum(Avg_Cost) as Avg_Cost,sum(IN_QTY) as IN_QTY,sum(Out_QTY) as Out_QTY,sum(TRANS_QTY) as TRANS_QTY,sum(Fat_KG) as Fat_KG,sum(SNF_KG) as SNF_KG," & _
            " sum(CL_QTY) as CL_QTY,sum(CL_FAT_KG) as CL_FAT_KG,sum(CL_SNF_KG) as CL_SNF_KG, sum(CL_FIFO_Cost) as CL_FIFO_Cost,sum(CL_LIFO_Cost) as CL_LIFO_Cost," & _
            " sum(CL_Avg_Cost) as CL_Avg_Cost,max(AGEING_Flag) as AGEING_Flag,sum(In_Avg_Cost) as In_Avg_Cost,sum(Out_Avg_Cost) as Out_Avg_Cost, " & _
            " sum(In_Fat_KG) as In_Fat_KG,sum(Out_Fat_KG) as Out_Fat_KG,sum(In_SNF_KG) as In_SNF_KG,sum(Out_SNF_KG) as Out_SNF_KG "
        ''=================Monika 27/03/2017============================================================
        QryBalGIT += " ,sum(qc_in_fat_kg) as qc_in_fat_kg,sum(qc_out_fat_kg) as qc_out_fat_kg,sum(qc_fat_kg) as qc_fat_kg,sum(qc_in_snf_kg) as qc_in_snf_kg,sum(qc_out_snf_kg) as qc_out_snf_kg,sum(qc_snf_kg) as qc_snf_kg,sum(cl_qc_fat_kg) as cl_qc_fat_kg,sum(cl_qc_snf_kg) as cl_qc_snf_kg "

        QryBalGIT += " ,sum(STD_in_fat_kg) as STD_in_fat_kg,sum(STD_out_fat_kg) as STD_out_fat_kg,sum(STD_fat_kg) as STD_fat_kg,sum(STD_in_snf_kg) as STD_in_snf_kg,sum(STD_out_snf_kg) as STD_out_snf_kg,sum(STD_snf_kg) as STD_snf_kg,sum(cl_STD_fat_kg) as cl_STD_fat_kg,sum(cl_STD_snf_kg) as cl_STD_snf_kg "
        ''================================================================================================

        QryBalGIT += " from (select TRANS_DATE," & Table_Name & ".Location_Code," & Table_Name & ".Item_Code,Item_Desc,Stock_UOM,IsFromMilk,FIFO_Cost,LIFO_Cost,Avg_Cost,IN_QTY,Out_QTY,TRANS_QTY,Fat_KG,SNF_KG,CL_QTY,CL_FAT_KG,CL_SNF_KG," & _
        " CL_FIFO_Cost,CL_LIFO_Cost,CL_Avg_Cost,AGEING_Flag,In_Avg_Cost,Out_Avg_Cost,In_Fat_KG,Out_Fat_KG,In_SNF_KG,Out_SNF_KG "
        ''=================Monika 27/03/2017============================================================
        QryBalGIT += " ,qc_in_fat_kg,qc_out_fat_kg,qc_fat_kg,qc_in_snf_kg,qc_out_snf_kg,qc_snf_kg,cl_qc_fat_kg,cl_qc_snf_kg "
        QryBalGIT += " ,cast(((case when isnull(UOMDET.conversion_factor,0)>0 then cast(in_qty/UOMDET.conversion_factor as decimal(18,3)) else 0 end) * isnull(ITEMQCPARAM_FAT.fat_per,0)) / 100 as decimal(18,3)) as STD_in_fat_kg," & _
                     "  cast(((case when isnull(UOMDET.conversion_factor,0)>0 then cast(out_qty/UOMDET.conversion_factor as decimal(18,3)) else 0 end) * isnull(ITEMQCPARAM_FAT.fat_per,0)) / 100 as decimal(18,3)) as STD_out_fat_kg," & _
                     "  cast(((case when isnull(UOMDET.conversion_factor,0)>0 then cast(trans_qty/UOMDET.conversion_factor as decimal(18,3)) else 0 end) * isnull(ITEMQCPARAM_FAT.fat_per,0)) / 100 as decimal(18,3)) as STD_fat_kg," & _
                     "  cast(((case when isnull(UOMDET.conversion_factor,0)>0 then cast(in_qty/UOMDET.conversion_factor as decimal(18,3)) else 0 end) * isnull(ITEMQCPARAM_SNF.SNF_per,0)) / 100 as decimal(18,3)) as STD_in_snf_kg," & _
                     "  cast(((case when isnull(UOMDET.conversion_factor,0)>0 then cast(out_qty/UOMDET.conversion_factor as decimal(18,3)) else 0 end) * isnull(ITEMQCPARAM_SNF.SNF_per,0)) / 100 as decimal(18,3)) as STD_out_snf_kg," & _
                     "  cast(((case when isnull(UOMDET.conversion_factor,0)>0 then cast(trans_qty/UOMDET.conversion_factor as decimal(18,3)) else 0 end) * isnull(ITEMQCPARAM_SNF.SNF_per,0)) / 100 as decimal(18,3)) as STD_snf_kg," & _
                     "  cast(((case when isnull(UOMDET.conversion_factor,0)>0 then cast(cl_qty/UOMDET.conversion_factor as decimal(18,3)) else 0 end) * isnull(ITEMQCPARAM_FAT.fat_per,0)) / 100 as decimal(18,3)) as cl_STD_fat_kg," & _
                     "  cast(((case when isnull(UOMDET.conversion_factor,0)>0 then cast(cl_qty/UOMDET.conversion_factor as decimal(18,3)) else 0 end) * isnull(ITEMQCPARAM_SNF.SNF_per,0)) / 100 as decimal(18,3)) as cl_STD_snf_kg "
        ''================================================================================================

        QryBalGIT += " from " & Table_Name & " " & _
                   " left join (select item_code as PICODE,Actual_Range as Fat_Per,params.[type] from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER Params on Params.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where Params.Type='FAT' ) as ITEMQCPARAM_FAT on " & Table_Name & ".Item_Code=ITEMQCPARAM_FAT.PICODE and ITEMQCPARAM_FAT.[type]='FAT' " & Environment.NewLine &
                   " left join (select item_code as PICODE,Actual_Range as SNF_Per,params.[type] from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER Params on Params.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where Params.Type='SNF' ) as ITEMQCPARAM_SNF on " & Table_Name & ".Item_Code=ITEMQCPARAM_SNF.PICODE and ITEMQCPARAM_SNF.[type]='SNF' " & Environment.NewLine &
                   " left outer join (select item_code as UICODE,conversion_factor,uom_code from tspl_item_uom_detail)UOMDET on UOMDET.UICODE=" & Table_Name & ".item_code and UOMDET.uom_code='KG' " & _
            " where " & condOpening & " "

        QryBalGIT = QryBalGIT & " union all  " & _
           " select Punching_Date as TRANS_DATE,Location_Code,Item_Code,Item_Desc,Stock_UOM,IsFromMilk,FIFO_COST*(case when OP_TYPE='I' then 1 else -1 end),LIFO_COST*(case when OP_TYPE='I' then 1 else -1 end),Avg_Cost*(case when OP_TYPE='I' then 1 else -1 end),IN_QTY*(case when OP_TYPE='I' then 1 else -1 end),Out_QTY*(case when OP_TYPE='I' then 1 else -1 end),(IN_QTY-Out_QTY)*(case when OP_TYPE='I' then 1 else -1 end) as Trans_Qty," & _
           " Fat_KG*(case when OP_TYPE='I' then 1 else -1 end),SNF_KG*(case when OP_TYPE='I' then 1 else -1 end),(IN_QTY-Out_QTY)*(case when OP_TYPE='I' then 1 else -1 end) as CL_QTY,Fat_KG*(case when OP_TYPE='I' then 1 else -1 end) as CL_FAT_KG,SNF_KG*(case when OP_TYPE='I' then 1 else -1 end) as CL_SNF_KG,FIFO_COST*(case when OP_TYPE='I' then 1 else -1 end) as CL_FIFO_Cost,LIFO_COST*(case when OP_TYPE='I' then 1 else -1 end) as CL_LIFO_Cost,Avg_Cost*(case when OP_TYPE='I' then 1 else -1 end) as CL_Avg_Cost,0 as AGEING_Flag," & _
           " (case when INOUT='I' then Avg_Cost else 0 end)*(case when OP_TYPE='I' then 1 else -1 end) In_Avg_Cost,(case when INOUT='O' then Avg_Cost else 0 end)*(case when OP_TYPE='I' then 1 else -1 end) as Out_Avg_Cost, " & _
           " (case when INOUT='I' then Fat_KG else 0 end)*(case when OP_TYPE='I' then 1 else -1 end) as  In_Fat_KG,(case when INOUT='O' then Fat_KG else 0 end)*(case when OP_TYPE='I' then 1 else -1 end) as Out_Fat_KG, " & _
           " (case when INOUT='I' then SNF_KG else 0 end)*(case when OP_TYPE='I' then 1 else -1 end) as In_SNF_KG, (case when INOUT='O' then SNF_KG else 0 end)*(case when OP_TYPE='I' then 1 else -1 end) as Out_SNF_KG "
        ''=================Monika 27/03/2017============================================================
        QryBalGIT += " ,(case when INOUT='I' then ACT_FAT_KG else 0 end)*(case when OP_TYPE='I' then 1 else -1 end) as qc_in_fat_kg,(case when INOUT='O' then ACT_Fat_KG else 0 end)*(case when OP_TYPE='I' then 1 else -1 end) as qc_out_fat_kg,ACT_fat_kg * (case when OP_TYPE='I' then 1 else -1 end) as qc_fat_kg" & _
            " ,(case when INOUT='I' then ACT_SNF_KG else 0 end)*(case when OP_TYPE='I' then 1 else -1 end) as qc_in_snf_kg,(case when INOUT='O' then ACT_SNF_KG else 0 end)*(case when OP_TYPE='I' then 1 else -1 end) as qc_out_snf_kg,ACT_snf_kg *(case when OP_TYPE='I' then 1 else -1 end) as qc_snf_kg," & _
            " act_fat_kg *(case when OP_TYPE='I' then 1 else -1 end) as cl_qc_fat_kg,act_SNF_KG*(case when OP_TYPE='I' then 1 else -1 end) as cl_qc_snf_kg "

        QryBalGIT += " ,(case when INOUT='I' then STD_FAT_KG else 0 end)*(case when OP_TYPE='I' then 1 else -1 end) as STD_in_fat_kg,(case when INOUT='O' then STD_Fat_KG else 0 end)*(case when OP_TYPE='I' then 1 else -1 end) as STD_out_fat_kg,STD_fat_kg * (case when OP_TYPE='I' then 1 else -1 end) as STD_fat_kg" & _
            " ,(case when INOUT='I' then STD_SNF_KG else 0 end)*(case when OP_TYPE='I' then 1 else -1 end) as STD_in_snf_kg,(case when INOUT='O' then STD_SNF_KG else 0 end)*(case when OP_TYPE='I' then 1 else -1 end) as STD_out_snf_kg,STD_snf_kg *(case when OP_TYPE='I' then 1 else -1 end) as STD_snf_kg," & _
            " STD_fat_kg *(case when OP_TYPE='I' then 1 else -1 end) as cl_STD_fat_kg,STD_SNF_KG*(case when OP_TYPE='I' then 1 else -1 end) as cl_STD_snf_kg "
        ''================================================================================================

        QryBalGIT += " from (" & QryBalBaseGIT & " ) GIT ) as  Opening " & _
            " group by TRANS_DATE,Location_Code,Item_Code,Stock_UOM"
        Return QryBalGIT
    End Function
    Public Shared Function GetBaseQryStockReco(ByVal objFilter As clsStockRecoFilters) As String
        If objFilter Is Nothing Then
            Return ""
        End If
        If objFilter.From_Date = Nothing Or objFilter.To_Date = Nothing Then
            Return ""
        End If
        If (objFilter.From_Date) > (objFilter.To_Date) Then
            Throw New Exception("To Date cant be less than from date")
        End If

        Dim strCodeColumn As String = ""
        Dim strCodeColumnMax As String = ""
        Dim strCodeDescColumn As String = ""
        Dim strCodeDescColumnMax As String = ""

        Dim strCodeColumnSelect As String = ""
        Dim strCodeDescColumnSelect As String = ""

        Dim strCodeColumnNull As String = ""
        Dim strCodeDescColumnNull As String = ""

        Dim dtCategory As DataTable = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+'DESC' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
        Dim strCategoryTable As String = ""
        If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
            For ii As Integer = 0 To dtCategory.Rows.Count - 1
                If ii <> 0 Then
                    strCodeColumn += ","
                    strCodeColumnMax += ","
                    strCodeDescColumn += ","
                    strCodeDescColumnMax += ","

                    strCodeColumnSelect += ","
                    strCodeDescColumnSelect += ","

                    strCodeColumnNull += ","
                    strCodeDescColumnNull += ","
                End If
                strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeDescColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                strCodeDescColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]"

                strCodeColumnSelect += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeDescColumnSelect += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"

                strCodeColumnNull += "null as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeDescColumnNull += "null as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
            Next
            strCategoryTable = "select Item_Code," + strCodeColumnMax + "," + strCodeDescColumnMax + "  from (" + Environment.NewLine & _
            " select * from ( " + Environment.NewLine & _
            " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " + Environment.NewLine & _
            " ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc " + Environment.NewLine & _
            " ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " + Environment.NewLine & _
            " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc " + Environment.NewLine & _
            " from  TSPL_ITEM_MASTER  " + Environment.NewLine & _
            " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " + Environment.NewLine & _
            " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values" + Environment.NewLine & _
            " where 2=2 " + Environment.NewLine & _
            " )xx" + Environment.NewLine & _
            " Pivot " + Environment.NewLine & _
            " ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + strCodeColumn + ")" + Environment.NewLine & _
            " ) Pivt" + Environment.NewLine & _
            " Pivot " + Environment.NewLine & _
            " (" + Environment.NewLine & _
            " max(Category_Value_Desc) for Item_Category_CodeDesc in (" + strCodeDescColumn + ")" + Environment.NewLine & _
            " ) Pivt1 " + Environment.NewLine & _
            " ) xxx group by Item_Code "
            ''End of Category Table start now.
        End If
        ''Virtual Category Table start now.
        '' query for structure and item group custom field
        Dim MIS_Item_Group As String
        Dim qry As String = ""

        qry = " select MAP.Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING MAP " & _
            " left join TSPL_CUSTOM_FIELD_HEAD CF on MAP.Custom_Field_Code=CF.Code " & _
            " where CF.Name='MIS Item Group' and MAP.PROGRAM_CODE='" & clsUserMgtCode.itemStructure & "'"
        MIS_Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        Dim strItemGroup As String = ""
        strItemGroup = " select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join (" & _
                       " select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='" & clsUserMgtCode.itemStructure & "'  " & _
                       " and Custom_Field_Code='" & MIS_Item_Group & "') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code" & _
                       " left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "') as StructDtl on Struct_Val.Value=StructDtl.Value "


        ''Base Query start Now
        qry = "select * from ( select InventroyMovement.Trans_Id,InventroyMovement.Trans_Type,TSPL_INVENTORY_SOURCE_CODE.Name as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView',"
        qry += " case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,"
        qry += " IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull(ITEMQCParam_FAT.fat_per,0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull(ITEMQCParam_SNF.snf_per,0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location,"

        ''=================Monika 27/03/2017==============================================
        qry += " ACT_FAT_PER,ACT_SNF_PER,ACT_FAT_KG,ACT_SNF_KG," & _
            " STD_FAT_PER,cast((isnull(STD_FAT_PER,0) * (isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)),0))) / 100 as decimal(18,3)) as STD_FAT_KG," & _
            " STD_SNF_PER,cast((isnull(STD_SNF_PER,0) * (isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)),0))) / 100 as decimal(18,3)) as STD_SNF_KG, "
        ''===============================================================

        qry += " isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG,"
        If clsCommon.myLen(objFilter.UOM_Code) > 0 Then
            qry += " '" + clsCommon.myCstr(objFilter.UOM_Code) + "' as Stock_UOM,(InventroyMovement.Stock_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Stock_Qty,"
            qry += " isnull((isnull(ITEMQCParam_FAT.fat_per,0)/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end) * (TSPL_ITEM_UOM_DETAIL.Conversion_Factor),0) as FatPer,"
            'qry += " isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end) * (TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer,"
            qry += " isnull((isnull(ITEMQCParam_SNF.snf_per,0)/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end) * (TSPL_ITEM_UOM_DETAIL.Conversion_Factor),0) as SNFPer,"
        Else
            qry += " InventroyMovement.Stock_UOM,InventroyMovement.Stock_Qty,"
            'qry += " isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer,"
            'qry += " isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer,"

            qry += " isnull((isnull(ITEMQCParam_FAT.fat_per,0)/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end),0) as FatPer,"
            qry += " isnull((isnull(ITEMQCParam_SNF.snf_per,0)/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end),0) as SNFPer,"
        End If
        qry += " (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code " + Environment.NewLine

        If clsCommon.myLen(strCategoryTable) > 0 Then
            qry += "," + strCodeColumnSelect + "," + strCodeDescColumnSelect
        End If

        qry += " ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation "

        If clsCommon.CompairString(clsCommon.myCstr(objFilter.ReportType), "Transaction Wise") = CompairStringResult.Equal Then
            ''======================27/03/2017==================================
            qry += " ,MilkFATKG  as Prod_Fat_KG,MilkSNFKG as Prod_SNF_KG,"
            qry += " (case when IsFromMilk=1 then MilkFatPer else isnull(ITEMQCParam_FAT.fat_per,0) end) as Prod_FAT_Per,"
            qry += " (case when IsFromMilk=1 then MilkSNFPer else isnull(ITEMQCParam_snf.snf_per,0) end) as Prod_SNF_Per "
            ''====================================================================
        End If


        'Dim condInv As String = ""
        Dim LocationFirstTime As Integer = 0
        Dim LocationAddress As String = String.Empty
        Dim strWhrCatg As String = ""
        If objFilter.SelectLocation = True Then
            Dim IsApplicable As Boolean = False
            For ii As Integer = 0 To objFilter.arrLocation.Count - 1
                If clsCommon.myCBool(objFilter.arrLocation(ii).Sel) Then
                    LocationFirstTime += 1
                    If LocationFirstTime = 1 Then
                        LocationAddress = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress] from TSPL_LOCATION_MASTER where Location_Code= '" & clsCommon.myCstr(objFilter.arrLocation(ii).Code) & "'")
                    End If
                    If IsApplicable Then
                        strWhrCatg += " Or "
                    End If
                    'If clsCommon.CompairString(objFilter.FORMTYPE, clsUserMgtCode.stockRecoNewJR) = CompairStringResult.Equal Then
                    '    strWhrCatg += " (Location_Code = '" + clsCommon.myCstr(objFilter.arrLocation(ii).Code) + "') "
                    'Else
                    '    strWhrCatg += " ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(objFilter.arrLocation(ii).Code) + "') "
                    'End If
                    strWhrCatg += " ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(objFilter.arrLocation(ii).Code) + "') "
                    IsApplicable = True
                    Dim arr As Dictionary(Of String, Object) = objFilter.arrLocation(ii).arrOut
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        strWhrCatg += " and Location_Code in ("
                        Dim isFirstTime As Boolean = True
                        For Each strInn As String In arr.Keys
                            If Not isFirstTime Then
                                strWhrCatg += ","
                            End If
                            strWhrCatg += "'" + strInn + "'"
                            isFirstTime = False
                        Next
                        strWhrCatg += ")"
                    End If
                End If
            Next
            If Not IsApplicable Then
                Throw New Exception("Please select at least one location")
            End If

        Else
            If clsCommon.CompairString(objFilter.FORMTYPE, clsUserMgtCode.stockRecoNewJR) = CompairStringResult.Equal Then
                strWhrCatg += "  (Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where len(coalesce(Jobwork_Vendor,''))>0 and Is_Sub_Location='Y'))"
            End If
        End If
        If Not objFilter.arrItem Is Nothing AndAlso objFilter.arrItem.Count > 0 Then
            If clsCommon.myLen(strWhrCatg) > 0 Then
                strWhrCatg = strWhrCatg & " and " & " Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
            Else
                strWhrCatg = " Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
            End If
        End If
        If clsCommon.myLen(strWhrCatg) <= 0 Then
            strWhrCatg = "2=2 "
        End If
        Dim strCondOpening As String = strWhrCatg
        If objFilter.arrTransaction IsNot Nothing AndAlso objFilter.arrTransaction.Count > 0 Then
            strCondOpening = strCondOpening & " and Trans_Type in (" + clsCommon.GetMulcallString(objFilter.arrTransaction) + ") " + Environment.NewLine
        ElseIf objFilter.IsProduction_WIP Then ''when no transaction is selected and report open as WIP, then only production transactions open Monika (27/03/2017)
            strCondOpening = strCondOpening & " and Trans_Type in ('" + clsUserMgtCode.frmProcessProductionIssueEntry + "','" + clsUserMgtCode.frmProcessProductionStandardization + "','" + clsUserMgtCode.frmProcessProductionStageProcess + "','" + clsUserMgtCode.frmProductionEntry + "','" + clsUserMgtCode.frmWreckageBooking + "','Prod-Scrap','PP-PR') " + Environment.NewLine
        End If
        strCondOpening = strCondOpening & " and " & "cast(Punching_Date as Date)<'" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' "
        Dim QryOpeningGIT As String = GetBaseQueryForItemLocationBalance(objFilter, True)

        qry += " from ( "
        qry += " select Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,TSPL_INVENTORY_MOVEMENT.Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,isnull(ITEMQCParam_FAT.fat_per,0) as MilkFatPer,isnull(ITEMQCParam_SNF.snf_per,0) as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType"
        ''====================Monika 27/03/2017================================================
        qry += " ,TSPL_INVENTORY_MOVEMENT.fat_per as ACT_FAT_Per,TSPL_INVENTORY_MOVEMENT.SNF_per as ACT_SNF_PER,TSPL_INVENTORY_MOVEMENT.fat_kg as ACT_FAT_KG,TSPL_INVENTORY_MOVEMENT.snf_kg as ACT_SNF_KG " & _
               ",isnull(ITEMQCParam_FAT.fat_per,0) as STD_FAT_PER,0 as STD_FAT_KG,isnull(ITEMQCParam_SNF.SNF_PER,0) as STD_SNF_PER,0 as STD_SNF_KG "
        ''=====================================================================================

        qry += " from TSPL_INVENTORY_MOVEMENT LEFT JOIN (select Location_Code as Loc_Code,Main_Location_Code,Is_Section,Is_Sub_Location from TSPL_LOCATION_MASTER ) Loc on TSPL_INVENTORY_MOVEMENT.Location_Code=Loc.Loc_Code  " & _
            " left outer join (select TSPL_ITEM_QC_PARAMETER_MASTER.item_code as PICODE,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as Fat_Per,TSPL_PARAMETER_MASTER.[type] from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_PARAMETER_MASTER.[type]='FAT')ITEMQCParam_FAT on ITEMQCParam_FAT.PICODE=tspl_inventory_movement.item_code and ITEMQCParam_FAT.[type]='FAT' " & _
            " left outer join (select TSPL_ITEM_QC_PARAMETER_MASTER.item_code as PICODE,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as SNF_Per,TSPL_PARAMETER_MASTER.[type] from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_PARAMETER_MASTER.[type]='SNF')ITEMQCParam_SNF on ITEMQCParam_SNF.PICODE=tspl_inventory_movement.item_code and ITEMQCParam_SNF.[type]='SNF' " & _
            " where cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "' and (" + strWhrCatg + ")  " + Environment.NewLine

        qry += " union all " + Environment.NewLine
        qry += " select Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per as MilkFatPer ,TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per as MilkSNFPer,TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG as MilkFATKG,TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType "
        ''====================Monika 27/03/2017================================================
        qry += " ,TSPL_INVENTORY_MOVEMENT_NEW.fat_per as ACT_FAT_Per,TSPL_INVENTORY_MOVEMENT_NEW.SNF_per as ACT_SNF_PER,TSPL_INVENTORY_MOVEMENT_NEW.fat_kg as ACT_FAT_KG,TSPL_INVENTORY_MOVEMENT_NEW.snf_kg as ACT_SNF_KG " & _
            ",isnull(ITEMQCParam_FAT.fat_per,0) as STD_FAT_PER,0 as STD_FAT_KG,isnull(ITEMQCParam_SNF.SNF_PER,0) as STD_SNF_PER,0 as STD_SNF_KG "
        ''=====================================================================================
        ''Item.Product_Type='MI'
        qry += " from TSPL_INVENTORY_MOVEMENT_NEW LEFT JOIN (select Location_Code as Loc_Code,Main_Location_Code,Is_Section,Is_Sub_Location from TSPL_LOCATION_MASTER ) Loc on TSPL_INVENTORY_MOVEMENT_NEW.Location_Code=Loc.Loc_Code " & _
            " left outer join (select TSPL_ITEM_QC_PARAMETER_MASTER.item_code as PICODE,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as Fat_Per,TSPL_PARAMETER_MASTER.[type] from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_PARAMETER_MASTER.[type]='FAT')ITEMQCParam_FAT on ITEMQCParam_FAT.PICODE=TSPL_INVENTORY_MOVEMENT_NEW.item_code and ITEMQCParam_FAT.[type]='FAT' " & _
            " left outer join (select TSPL_ITEM_QC_PARAMETER_MASTER.item_code as PICODE,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as SNF_Per,TSPL_PARAMETER_MASTER.[type] from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_PARAMETER_MASTER.[type]='SNF')ITEMQCParam_SNF on ITEMQCParam_SNF.PICODE=TSPL_INVENTORY_MOVEMENT_NEW.item_code and ITEMQCParam_SNF.[type]='SNF' " & _
            " where cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "' and (" + strWhrCatg + ")" + Environment.NewLine
        qry += " Union All "
        qry += " select 0 as Trans_Id,'Opening' as Trans_Type,'Opening Balance' as Source_Doc_No,'" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' as Punching_Date, (CASE WHEN TSPL_INV_MOVE_DL.CL_QTY<0 THEN 'O' ELSE 'I' END) as InOut,TSPL_INV_MOVE_DL.Location_Code as Location_Code," & _
               " TSPL_INV_MOVE_DL.Item_Code,'' as UOM,0 as MRP,TSPL_INV_MOVE_DL.Stock_UOM,(TSPL_INV_MOVE_DL.CL_QTY) as CL_QTY,(TSPL_INV_MOVE_DL.CL_FIFO_Cost) as CL_FIFO_Cost, " & _
               " (TSPL_INV_MOVE_DL.CL_LIFO_Cost) as CL_LIFO_Cost,(TSPL_INV_MOVE_DL.CL_Avg_Cost) as CL_Avg_Cost,IsFromMilk," & _
               "(case when TSPL_INV_MOVE_DL.IsFromMilk=1 then (case when convert(decimal(18,6),TSPL_INV_MOVE_DL.CL_FAT_KG)=0 or coalesce(UOM_KG.Conversion_Factor,0)=0 or TSPL_INV_MOVE_DL.CL_QTY=0 then 0 else convert(decimal(18,6),(TSPL_INV_MOVE_DL.CL_FAT_KG*100/(TSPL_INV_MOVE_DL.CL_QTY/UOM_KG.Conversion_Factor))) end) else isnull(ITEMQCParam_FAT.fat_per,0) end) as Fat_Per,(case when TSPL_INV_MOVE_DL.IsFromMilk=1 THEN  (case when convert(decimal(18,6),TSPL_INV_MOVE_DL.CL_SNF_KG)=0 or coalesce(UOM_KG.Conversion_Factor,0)=0 or TSPL_INV_MOVE_DL.CL_QTY=0 then 0 else convert(decimal(18,6),(TSPL_INV_MOVE_DL.CL_SNF_KG*100/(TSPL_INV_MOVE_DL.CL_QTY/UOM_KG.Conversion_Factor))) end) ELSE isnull(ITEMQCParam_SNF.SNF_Per,0) END) as SNF_Per, " & _
               " (TSPL_INV_MOVE_DL.CL_FAT_KG) as CL_FAT_KG,(TSPL_INV_MOVE_DL.CL_SNF_KG) as CL_SNF_KG,'' as SourceCode,'' as SourceName,'' as SourceType "
        ''====================Monika 27/03/2017================================================
        qry += ",(case when convert(decimal(18,6),TSPL_INV_MOVE_DL.CL_QC_FAT_KG)=0 or coalesce(UOM_KG.Conversion_Factor,0)=0 or TSPL_INV_MOVE_DL.CL_QTY=0 then 0 else convert(decimal(18,6),(TSPL_INV_MOVE_DL.CL_QC_FAT_KG*100/(TSPL_INV_MOVE_DL.CL_QTY/UOM_KG.Conversion_Factor))) end) as ACT_FAT_Per,(case when convert(decimal(18,6),TSPL_INV_MOVE_DL.CL_QC_SNF_KG)=0 or coalesce(UOM_KG.Conversion_Factor,0)=0 or TSPL_INV_MOVE_DL.CL_QTY=0 then 0 else convert(decimal(18,6),(TSPL_INV_MOVE_DL.CL_QC_SNF_KG*100/(TSPL_INV_MOVE_DL.CL_QTY/UOM_KG.Conversion_Factor))) end) as ACT_SNF_PER, " & _
               " (TSPL_INV_MOVE_DL.CL_QC_FAT_KG) as ACT_FAT_KG,(TSPL_INV_MOVE_DL.CL_QC_SNF_KG) as ACT_SNF_KG,isnull(ITEMQCParam_FAT.fat_per,0) as STD_FAT_PER,0 as STD_FAT_KG,isnull(ITEMQCParam_SNF.SNF_per,0) as STD_SNF_PER,0 as STD_SNF_KG "
        ''=====================================================================================

        qry += " from  (" & QryOpeningGIT & ") as TSPL_INV_MOVE_DL inner join ( " & _
               " select max(TRANS_DATE) as TRANS_DATE,Location_Code as L_Code,TSPL_INV_MOVE_DL.Item_Code as ICode,Stock_UOM from TSPL_INV_MOVE_DL where TRANS_DATE<'" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' " & _
               " group by Location_Code,Item_Code,Stock_UOM) as opening on TSPL_INV_MOVE_DL.Item_Code=opening.ICode and TSPL_INV_MOVE_DL.Location_Code=opening.L_Code " & _
               " and TSPL_INV_MOVE_DL.TRANS_DATE=opening.TRANS_DATE and TSPL_INV_MOVE_DL.Stock_UOM=opening.Stock_UOM " & _
               " LEFT JOIN (select Location_Code as Loc_Code,Main_Location_Code,Is_Section,Is_Sub_Location from TSPL_LOCATION_MASTER ) Loc on TSPL_INV_MOVE_DL.Location_Code=Loc.Loc_Code " & _
               " left join (select distinct Item_Code as I_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG') as UOM_KG on TSPL_INV_MOVE_DL.Item_Code=UOM_KG.I_Code " & _
               " left outer join (SELECT ITEM_CODE as ICode,PRODUCT_TYPE FROM TSPL_ITEM_MASTER) Item on Item.ICode=TSPL_INV_MOVE_DL.Item_Code " & _
               " left outer join (select TSPL_ITEM_QC_PARAMETER_MASTER.item_code as PICODE,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as Fat_Per,TSPL_PARAMETER_MASTER.[type] from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_PARAMETER_MASTER.[type]='FAT')ITEMQCParam_FAT on ITEMQCParam_FAT.PICODE=tspl_inv_move_dl.item_code and ITEMQCParam_FAT.[type]='FAT' " & _
               " left outer join (select TSPL_ITEM_QC_PARAMETER_MASTER.item_code as PICODE,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as SNF_Per,TSPL_PARAMETER_MASTER.[type] from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_PARAMETER_MASTER.[type]='SNF')ITEMQCParam_SNF on ITEMQCParam_SNF.PICODE=tspl_inv_move_dl.item_code and ITEMQCParam_SNF.[type]='SNF' " & _
               " WHERE 2=2 and (" + strWhrCatg + ")"
        qry += ") InventroyMovement " + Environment.NewLine
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code" + Environment.NewLine
        qry += " left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code" + Environment.NewLine
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code " + Environment.NewLine
        ''==PArteek Added
        If clsCommon.CompairString(clsCommon.myCstr(objFilter.ReportType), "Transaction Wise") = CompairStringResult.Equal Then
            'qry += " left outer join TSPL_PP_PRODUCTION_ENTRY_DETAIL on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=InventroyMovement.Source_Doc_No AND InventroyMovement.Item_Code=TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE AND InventroyMovement.UOM=TSPL_PP_PRODUCTION_ENTRY_DETAIL.UNIT_CODE" + Environment.NewLine
        End If

        ''==End

        qry += " left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end)"
        qry += " left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG'"
        qry += " left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type"
        If clsCommon.myLen(objFilter.UOM_Code) > 0 Then
            qry += " inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=InventroyMovement.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + clsCommon.myCstr(objFilter.UOM_Code) + "'"
        End If
        If clsCommon.myLen(strCategoryTable) > 0 Then
            qry += " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code"
        End If
        qry += " left outer join (" & strItemGroup & ") as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code "
        qry += " left outer join ( SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER  ) as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type " & _
               " left outer join (select TSPL_ITEM_QC_PARAMETER_MASTER.item_code as PICODE,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as fat_per,TSPL_PARAMETER_MASTER.[type] from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_PARAMETER_MASTER.[type]='FAT')ITEMQCParam_FAT on ITEMQCParam_FAT.PICODE=InventroyMovement.item_code and ITEMQCParam_FAT.[type]='FAT' " & _
               " left outer join (select TSPL_ITEM_QC_PARAMETER_MASTER.item_code as PICODE,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as snf_per,TSPL_PARAMETER_MASTER.[type] from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_PARAMETER_MASTER.[type]='SNF')ITEMQCParam_SNF on ITEMQCParam_SNF.PICODE=InventroyMovement.item_code and ITEMQCParam_SNF.[type]='SNF' "
        qry += " Where 2=2 "
        If Not objFilter.IncludeGIT Then
            qry += " and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'"
        End If
        ''richa 22 Jan 2021
        If objFilter.ExcludeConsumptionLoc = True Then
            qry += " and TSPL_LOCATION_MASTER.Is_Consumption_Location =0 and MainLocationTable.Is_Consumption_Location =0 "
        End If
        If objFilter.arrItemType IsNot Nothing AndAlso objFilter.arrItemType.Count > 0 Then
            qry += " and TSPL_ITEM_MASTER.Item_Type in (" + clsCommon.GetMulcallString(objFilter.arrItemType) + ") " + Environment.NewLine
        End If
        qry += "  ) xxxxx "

        qry += " where 2=2 "

        If objFilter.FatSNF Then
            qry += " and (MilkFatPer<>0 or FatPer<>0  or  MilkSNFPer<>0 or SNFPer<>0 or IsFromMilk=1) "
        Else
            qry += " and (MilkFatPer=0 and FatPer=0  and  MilkSNFPer=0 and SNFPer=0) "
        End If

        If objFilter.arrItem IsNot Nothing AndAlso objFilter.arrItem.Count > 0 Then
            qry += " and Item_Code in (" + clsCommon.GetMulcallString(objFilter.arrItem) + ") " + Environment.NewLine
        End If
        If objFilter.arrTransaction IsNot Nothing AndAlso objFilter.arrTransaction.Count > 0 Then
            qry += " and (Trans_Type in (" + clsCommon.GetMulcallString(objFilter.arrTransaction) + ") or Trans_Type='Opening' )" + Environment.NewLine
        ElseIf objFilter.IsProduction_WIP Then ''when no transaction is selected and report open as WIP, then only production transactions open Monika (27/03/2017)
            qry += " and (Trans_Type in ('" + clsUserMgtCode.frmProcessProductionIssueEntry + "','" + clsUserMgtCode.frmProcessProductionStandardization + "','" + clsUserMgtCode.frmProcessProductionStageProcess + "','" + clsUserMgtCode.frmProductionEntry + "','" + clsUserMgtCode.frmWreckageBooking + "','Prod-Scrap','PP-PR') or Trans_Type='Opening')" + Environment.NewLine
        End If


        If objFilter.arrItemGroup IsNot Nothing AndAlso objFilter.arrItemGroup.Count > 0 Then
            qry += " and coalesce(xxxxx.Item_Group,'') in (" + clsCommon.GetMulcallString(objFilter.arrItemGroup) + ") "
        End If
        If clsCommon.CompairString(clsCommon.myCstr(objFilter.InOut), "In") = CompairStringResult.Equal Then
            qry += " and xxxxx.InOut='I'"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(objFilter.InOut), "Out") = CompairStringResult.Equal Then
            qry += " and xxxxx.InOut='O'"
        End If



        If objFilter.SelectLocation Then
            qry += " and (" + strWhrCatg + ")"
        End If

        strWhrCatg = ""
        If objFilter.SelectCategory Then
            Dim IsApplicable As Boolean = False
            For ii As Integer = 0 To objFilter.arrCategory.Count - 1
                If clsCommon.myCBool(objFilter.arrCategory(ii).Sel) Then
                    If IsApplicable Then
                        strWhrCatg += " and "
                    End If
                    IsApplicable = True
                    strWhrCatg += "("
                    Dim arr As Dictionary(Of String, Object) = objFilter.arrCategory(ii).arrOut
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        strWhrCatg += " [" + clsCommon.myCstr(objFilter.arrCategory(ii).Code) + "] in ("
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
            qry += " and (" + strWhrCatg + ")"
        End If
        Return qry
    End Function
    Public Shared Function GetStockRecoOpeningOuterPart(ByVal objFilter As clsStockRecoFilters) As String
        Dim OuterOpClo As String = " [OPBal],(case when OPBal=0 then 0 else OPBalCost/OPBal end) as OPBalrate "
        If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
            OuterOpClo += ",case when convert(decimal(18,3),[OPQTYKG])=0 then 0 else convert(decimal(18,3),([OPQCFAT]*100/[OPQTYKG])) end as [OPFATPER],convert(decimal(18,3),[OPQCFAT]) as [OPFAT],case when convert(decimal(18,3),[OPQTYKG])=0 then 0 else convert(decimal(18,3),([OPQCSNF]*100/[OPQTYKG])) end as [OPSNFPER],convert(decimal(18,3), [OPQCSNF]) as [OPSNF],"
        Else
            OuterOpClo += ",case when convert(decimal(18,3),[OPQTYKG])=0 then 0 else convert(decimal(18,3),([OPFAT]*100/[OPQTYKG])) end as [OPFATPER],convert(decimal(18,3),[OPFAT]) as [OPFAT],case when convert(decimal(18,3),[OPQTYKG])=0 then 0 else convert(decimal(18,3),([OPSNF]*100/[OPQTYKG])) end as [OPSNFPER],convert(decimal(18,3), [OPSNF]) as [OPSNF],"
        End If
        ''============================Monika 27/03/2017=================================================================
        If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then ''if fat/snf type is Q or A,then QC fat/snf seen on report
            OuterOpClo += "case when convert(decimal(18,3),[OPQTYKG])=0 then 0 else convert(decimal(18,3),([OPQCFAT]*100/[OPQTYKG])) end as [OPQCFATPER],convert(decimal(18,3),[OPQCFAT]) as [OPQCFAT],case when convert(decimal(18,3),[OPQTYKG])=0 then 0 else convert(decimal(18,3),([OPQCSNF]*100/[OPQTYKG])) end as [OPQCSNFPER],convert(decimal(18,3), [OPQCSNF]) as [OPQCSNF]," & _
                "case when convert(decimal(18,3),[OPQTYKG])=0 then 0 else convert(decimal(18,3),( ([OPFAT]*100/[OPQTYKG]) - ([OPQCFAT]*100/[OPQTYKG])) ) end as [Op Diff. Fat%]," & _
                "convert(decimal(18,3),([OPFAT] - [OPQCFAT])) as [Op Diff. FAT KG],case when convert(decimal(18,3),[OPQTYKG])=0 then 0 else convert(decimal(18,3),( ([OPSNF]*100/[OPQTYKG]) - ([OPQCSNF]*100/[OPQTYKG]) )) end as [Op Diff. SNF%],convert(decimal(18,3), ([OPSNF] - [OPQCSNF])) as [Op Diff. SNF KG],"
        End If
        If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then ''if fat/snf type is S or A,then Standard fat/snf seen on report
            OuterOpClo += "case when convert(decimal(18,3),[OPQTYKG])=0 then 0 else convert(decimal(18,3),([OPSTDFAT]*100/[OPQTYKG])) end as [OPSTDFATPER],convert(decimal(18,3),[OPSTDFAT]) as [OPSTDFAT],case when convert(decimal(18,3),[OPQTYKG])=0 then 0 else convert(decimal(18,3),([OPSTDSNF]*100/[OPQTYKG])) end as [OPSTDSNFPER],convert(decimal(18,3), [OPSTDSNF]) as [OPSTDSNF]," & _
                "case when convert(decimal(18,3),[OPQTYKG])=0 then 0 else convert(decimal(18,3),( ([OPFAT]*100/[OPQTYKG]) - ([OPSTDFAT]*100/[OPQTYKG])) ) end as [Op Diff. STD Fat%]," & _
                "convert(decimal(18,3),([OPFAT] - [OPSTDFAT])) as [Op Diff. STD FAT KG],case when convert(decimal(18,3),[OPQTYKG])=0 then 0 else convert(decimal(18,3),( ([OPSNF]*100/[OPQTYKG]) - ([OPSTDSNF]*100/[OPQTYKG]) )) end as [Op Diff. STD SNF%],convert(decimal(18,3), ([OPSNF] - [OPSTDSNF])) as [Op Diff. STD SNF KG],"
        End If
        ''=================================================================================================================

        OuterOpClo += "OPBalCost,Received_Qty,(case when Received_Qty=0 then 0 else RecdCost/Received_Qty end) as RecdRate, RecdCost "
        If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
            OuterOpClo += ",case when convert(decimal(18,3),Received_QtyKG)=0 then 0 else convert(decimal(18,3),(ACT_Received_FAT*100/Received_QtyKG)) end as Received_FATPER,convert(decimal(18,3), ACT_Received_FAT) as Received_FAT,case when convert(decimal(18,3),Received_QTYKG)=0 then 0 else convert(decimal(18,3),(ACT_Received_SNF*100/Received_QTYKG)) end as Received_SNFPER,convert(decimal(18,3), ACT_Received_SNF) as Received_SNF,"
        Else
            OuterOpClo += ",case when convert(decimal(18,3),Received_QtyKG)=0 then 0 else convert(decimal(18,3),(Received_FAT*100/Received_QtyKG)) end as Received_FATPER,convert(decimal(18,3), Received_FAT) as Received_FAT,case when convert(decimal(18,3),Received_QTYKG)=0 then 0 else convert(decimal(18,3),(Received_SNF*100/Received_QTYKG)) end as Received_SNFPER,convert(decimal(18,3), Received_SNF) as Received_SNF,"
        End If
        ''============================Monika 27/03/2017=================================================================
        If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then ''if fat/snf type is Both,then QC fat/snf seen on report
            OuterOpClo += "case when convert(decimal(18,3),Received_QtyKG)=0 then 0 else convert(decimal(18,3),(ACT_Received_FAT*100/Received_QtyKG)) end as ACT_Received_FATPER,convert(decimal(18,3), ACT_Received_FAT) as ACT_Received_FAT,case when convert(decimal(18,3),Received_QTYKG)=0 then 0 else convert(decimal(18,3),(ACT_Received_SNF*100/Received_QTYKG)) end as ACT_Received_SNFPER,convert(decimal(18,3), ACT_Received_SNF) as ACT_Received_SNF," & _
                "case when convert(decimal(18,3),Received_QtyKG)=0 then 0 else (convert(decimal(18,3),(Received_FAT*100/Received_QtyKG))) - (convert(decimal(18,3),(ACT_Received_FAT*100/Received_QtyKG))) end as [Diff. Rec. FAT%],convert(decimal(18,3), (Received_FAT - ACT_Received_FAT)) as [Diff. Rec. FAT KG],case when convert(decimal(18,3),Received_QTYKG)=0 then 0 else (convert(decimal(18,3),(Received_SNF*100/Received_QTYKG))) - (convert(decimal(18,3),(ACT_Received_SNF*100/Received_QTYKG))) end as [Diff. Rec. SNF%],convert(decimal(18,3), (Received_SNF - ACT_Received_SNF)) as [Diff. Rec. SNF KG],"
        End If
        If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then ''if fat/snf type is Both,then QC fat/snf seen on report
            OuterOpClo += "case when convert(decimal(18,3),Received_QtyKG)=0 then 0 else convert(decimal(18,3),(STD_Received_FAT*100/Received_QtyKG)) end as STD_Received_FATPER,convert(decimal(18,3), STD_Received_FAT) as STD_Received_FAT,case when convert(decimal(18,3),Received_QTYKG)=0 then 0 else convert(decimal(18,3),(STD_Received_SNF*100/Received_QTYKG)) end as STD_Received_SNFPER,convert(decimal(18,3), STD_Received_SNF) as STD_Received_SNF," & _
                "case when convert(decimal(18,3),Received_QtyKG)=0 then 0 else (convert(decimal(18,3),(Received_FAT*100/Received_QtyKG))) - (convert(decimal(18,3),(STD_Received_FAT*100/Received_QtyKG))) end as [Diff. STD Rec. FAT%],convert(decimal(18,3), (Received_FAT - STD_Received_FAT)) as [Diff. STD Rec. FAT KG],case when convert(decimal(18,3),Received_QTYKG)=0 then 0 else (convert(decimal(18,3),(Received_SNF*100/Received_QTYKG))) - (convert(decimal(18,3),(STD_Received_SNF*100/Received_QTYKG))) end as [Diff. STD Rec. SNF%],convert(decimal(18,3), (Received_SNF - STD_Received_SNF)) as [Diff. STD Rec. SNF KG],"
        End If
        ''=================================================================================================================

        OuterOpClo += "convert(decimal(18,2),Issued_Qty) as Issued_Qty, (case when Issued_Qty=0 then 0 else IssueCost/Issued_Qty end) as IssueRate,IssueCost "

        If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
            OuterOpClo += ",case when convert(decimal(18,3),Issued_QTYKG)=0 then 0 else convert(decimal(18,3),(ACT_Issued_FAT*100/Issued_QTYKG)) end as Issued_FATPER,convert(decimal(18,3), ACT_Issued_FAT) as Issued_FAT,case when convert(decimal(18,3),Issued_QTYKG)=0 then 0 else convert(decimal(18,3),(ACT_Issued_SNF*100/Issued_QTYKG)) end as Issued_SNFPER,convert(decimal(18,3) ,ACT_Issued_SNF) as Issued_SNF,"
        Else
            OuterOpClo += ",case when convert(decimal(18,3),Issued_QTYKG)=0 then 0 else convert(decimal(18,3),(Issued_FAT*100/Issued_QTYKG)) end as Issued_FATPER,convert(decimal(18,3), Issued_FAT) as Issued_FAT,case when convert(decimal(18,3),Issued_QTYKG)=0 then 0 else convert(decimal(18,3),(Issued_SNF*100/Issued_QTYKG)) end as Issued_SNFPER,convert(decimal(18,3) ,Issued_SNF) as Issued_SNF,"
        End If
        ''============================Monika 27/03/2017=================================================================
        If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then ''if fat/snf type is Both,then QC fat/snf seen on report
            OuterOpClo += "case when convert(decimal(18,3),Issued_QTYKG)=0 then 0 else convert(decimal(18,3),(ACT_Issued_FAT*100/Issued_QTYKG)) end as ACT_Issued_FATPER,convert(decimal(18,3), ACT_Issued_FAT) as ACT_Issued_FAT,case when convert(decimal(18,3),Issued_QTYKG)=0 then 0 else convert(decimal(18,3),(ACT_Issued_SNF*100/Issued_QTYKG)) end as ACT_Issued_SNFPER,convert(decimal(18,3) ,ACT_Issued_SNF) as ACT_Issued_SNF  ," & _
                "case when convert(decimal(18,3),Issued_QTYKG)=0 then 0 else (convert(decimal(18,3),(Issued_FAT*100/Issued_QTYKG))) - (convert(decimal(18,3),(ACT_Issued_FAT*100/Issued_QTYKG))) end as [Diff. Iss. FAT%],convert(decimal(18,3), (Issued_FAT - ACT_Issued_FAT)) as [Diff. Iss. FAT KG],case when convert(decimal(18,3),Issued_QTYKG)=0 then 0 else (convert(decimal(18,3),(Issued_SNF*100/Issued_QTYKG))) - (convert(decimal(18,3),(ACT_Issued_SNF*100/Issued_QTYKG))) end as [Diff. Iss. SNF%],convert(decimal(18,3) ,(Issued_SNF - ACT_Issued_SNF)) as [Diff. Iss. SNF KG],"
        End If
        If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then ''if fat/snf type is Both,then QC fat/snf seen on report
            OuterOpClo += "case when convert(decimal(18,3),Issued_QTYKG)=0 then 0 else convert(decimal(18,3),(STD_Issued_FAT*100/Issued_QTYKG)) end as STD_Issued_FATPER,convert(decimal(18,3), std_Issued_FAT) as STD_Issued_FAT,case when convert(decimal(18,3),Issued_QTYKG)=0 then 0 else convert(decimal(18,3),(STD_Issued_SNF*100/Issued_QTYKG)) end as STD_Issued_SNFPER,convert(decimal(18,3) ,STD_Issued_SNF) as STD_Issued_SNF  ," & _
                "case when convert(decimal(18,3),Issued_QTYKG)=0 then 0 else (convert(decimal(18,3),(Issued_FAT*100/Issued_QTYKG))) - (convert(decimal(18,3),(STD_Issued_FAT*100/Issued_QTYKG))) end as [Diff. STD Iss. FAT%],convert(decimal(18,3), (Issued_FAT - STD_Issued_FAT)) as [Diff. STD Iss. FAT KG],case when convert(decimal(18,3),Issued_QTYKG)=0 then 0 else (convert(decimal(18,3),(Issued_SNF*100/Issued_QTYKG))) - (convert(decimal(18,3),(STD_Issued_SNF*100/Issued_QTYKG))) end as [Diff. STD Iss. SNF%],convert(decimal(18,3) ,(Issued_SNF - STD_Issued_SNF)) as [Diff. STD Iss. SNF KG],"
        End If
        ''=================================================================================================================

        OuterOpClo += "[Balance_Qty],convert(decimal(18,3), case when Balance_Qty=0 then 0 else Cost/Balance_Qty end) as Rate,Cost "

        If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
            OuterOpClo += ",case when convert(decimal(18,3),[Balance_QTYKG])=0 then 0 else convert(decimal(18,3),([ACT_Balance_FAT]*100/[Balance_QTYKG])) end as [Balance_FATPER],convert(decimal(18,3), [ACT_Balance_FAT]) as  [Balance_FAT],case when convert(decimal(18,3),[Balance_QTYKG])=0 then 0 else convert(decimal(18,3),([ACT_Balance_SNF]*100/[Balance_QTYKG])) end as [Balance_SNFPER],convert(decimal(18,3), [ACT_Balance_SNF]) as [Balance_SNF] "
        Else
            OuterOpClo += ",case when convert(decimal(18,3),[Balance_QTYKG])=0 then 0 else convert(decimal(18,3),([Balance_FAT]*100/[Balance_QTYKG])) end as [Balance_FATPER],convert(decimal(18,3), [Balance_FAT]) as  [Balance_FAT],case when convert(decimal(18,3),[Balance_QTYKG])=0 then 0 else convert(decimal(18,3),([Balance_SNF]*100/[Balance_QTYKG])) end as [Balance_SNFPER],convert(decimal(18,3), [Balance_SNF]) as [Balance_SNF] "
        End If
        ''============================Monika 27/03/2017=================================================================
        If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then ''if fat/snf type is Both,then QC fat/snf seen on report
            OuterOpClo += ",case when convert(decimal(18,3),[Balance_QTYKG])=0 then 0 else convert(decimal(18,3),([ACT_Balance_FAT]*100/[Balance_QTYKG])) end as [ACT_Balance_FATPER],convert(decimal(18,3), [ACT_Balance_FAT]) as  [ACT_Balance_FAT],case when convert(decimal(18,3),[Balance_QTYKG])=0 then 0 else convert(decimal(18,3),([ACT_Balance_SNF]*100/[Balance_QTYKG])) end as [ACT_Balance_SNFPER],convert(decimal(18,3), [ACT_Balance_SNF]) as [ACT_Balance_SNF] " & _
                ",case when convert(decimal(18,3),[Balance_QTYKG])=0 then 0 else (convert(decimal(18,3),([Balance_FAT]*100/[Balance_QTYKG]))) - (convert(decimal(18,3),([ACT_Balance_FAT]*100/[Balance_QTYKG]))) end as [Diff. Bal. FAT%],convert(decimal(18,3), (Balance_FAT - ACT_Balance_FAT)) as  [Diff. Bal. FAT KG],case when convert(decimal(18,3),[Balance_QTYKG])=0 then 0 else (convert(decimal(18,3),([Balance_SNF]*100/[Balance_QTYKG]))) - (convert(decimal(18,3),([ACT_Balance_SNF]*100/[Balance_QTYKG]))) end as [Diff. Bal. SNF%],convert(decimal(18,3), (Balance_SNF - ACT_Balance_SNF)) as [Diff. Bal. SNF KG]"
        End If
        If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then ''if fat/snf type is Both,then QC fat/snf seen on report
            OuterOpClo += ",case when convert(decimal(18,3),[Balance_QTYKG])=0 then 0 else convert(decimal(18,3),([STD_Balance_FAT]*100/[Balance_QTYKG])) end as [STD_Balance_FATPER],convert(decimal(18,3), [STD_Balance_FAT]) as  [STD_Balance_FAT],case when convert(decimal(18,3),[Balance_QTYKG])=0 then 0 else convert(decimal(18,3),([STD_Balance_SNF]*100/[Balance_QTYKG])) end as [STD_Balance_SNFPER],convert(decimal(18,3), [STD_Balance_SNF]) as [STD_Balance_SNF] " & _
                ",case when convert(decimal(18,3),[Balance_QTYKG])=0 then 0 else (convert(decimal(18,3),([Balance_FAT]*100/[Balance_QTYKG]))) - (convert(decimal(18,3),([STD_Balance_FAT]*100/[Balance_QTYKG]))) end as [Diff. STD Bal. FAT%],convert(decimal(18,3), (Balance_FAT - STD_Balance_FAT)) as  [Diff. STD Bal. FAT KG],case when convert(decimal(18,3),[Balance_QTYKG])=0 then 0 else (convert(decimal(18,3),([Balance_SNF]*100/[Balance_QTYKG]))) - (convert(decimal(18,3),([STD_Balance_SNF]*100/[Balance_QTYKG]))) end as [Diff. STD Bal. SNF%],convert(decimal(18,3), (Balance_SNF - STD_Balance_SNF)) as [Diff. STD Bal. SNF KG]"
        End If
        ''=================================================================================================================


        Return OuterOpClo
    End Function
    Public Shared Function GetStockRecoOpeningInnerPart(ByVal objFilter As clsStockRecoFilters) As String
        Dim InnerOpClo As String = "  SUM(STOCK_QTY * (CASE WHEN Trans_Type='Opening' THEN 1 ELSE 0 end))  AS [OPBal]  ," & _
                        " SUM(Cost  * (CASE WHEN Trans_Type='Opening' THEN 1 ELSE 0 end))  AS [OPBalCost]  , " & _
                        " SUM(QtyKG * (CASE WHEN Trans_Type='Opening' THEN 1 ELSE 0 end) )  AS OPQTYKG ," & _
                        " SUM( (CASE WHEN Trans_Type='Opening' THEN MilkFATKG ELSE 0 end))  AS [OPFAT], " & _
                        " SUM((CASE WHEN Trans_Type='Opening' THEN MilkSNFKG ELSE 0 end) )  AS [OPSNF], "
        ''============================Monika 27/03/2017=================================================================
        InnerOpClo += " SUM( (CASE WHEN Trans_Type='Opening' THEN ACT_FAT_KG ELSE 0 end))  AS [OPQCFAT], " & _
                        " SUM((CASE WHEN Trans_Type='Opening' THEN ACT_SNF_KG ELSE 0 end) )  AS [OPQCSNF], "

        InnerOpClo += " SUM( (CASE WHEN Trans_Type='Opening' THEN STD_FAT_KG ELSE 0 end))  AS [OPSTDFAT], " & _
                        " SUM((CASE WHEN Trans_Type='Opening' THEN STD_SNF_KG ELSE 0 end) )  AS [OPSTDSNF], "
        ''==============================================================================================================

        ''============add stocking_qty in below rec.qty ,that pervious not added,due to that qty always calc. wrong. By Monika(23/04/2017)
        ''------------and add condition and Trans_Type<>'Opening' to every case,because opening row also has first date of from and to date filter. By Monika(23/04/2017)
        InnerOpClo += " SUM(STOCK_QTY * (CASE WHEN (PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening') THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else 0 end))  AS Received_Qty , " & _
                        " SUM(Cost * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else 0 end))  AS RecdCost , " & _
                        " SUM(QtyKG * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else 0 end))  AS Received_QtyKG , " & _
                        " SUM((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else 0 end))  AS Received_FAT , " & _
                        " SUM((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else 0 end))  AS Received_SNF , "
        ''============================Monika 27/03/2017=================================================================
        InnerOpClo += " SUM(ACT_FAT_KG * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else 0 end))  AS ACT_Received_FAT , " & _
                      " SUM(ACT_SNF_KG * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else 0 end))  AS ACT_Received_SNF , "

        InnerOpClo += " SUM(STD_FAT_KG * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else 0 end))  AS STD_Received_FAT , " & _
                      " SUM(STD_SNF_KG * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else 0 end))  AS STD_Received_SNF , "
        ''==============================================================================================================

        InnerOpClo += " SUM(STOCK_QTY * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 0 else 1 end))  AS Issued_Qty," & _
                        " SUM(Cost * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 0 else 1 end))  AS IssueCost," & _
                        " SUM(QtyKG * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 0 else 1 end))  AS Issued_QtyKG," & _
                        " SUM((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 0 else 1 end))  AS Issued_FAT," & _
                        " SUM((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end ) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 0 else 1 end))  AS Issued_SNF,"
        ''============================Monika 27/03/2017=================================================================
        InnerOpClo += " SUM(ACT_FAT_KG * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 0 else 1 end))  AS ACT_Issued_FAT," & _
                        " SUM(ACT_SNF_KG * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 0 else 1 end))  AS ACT_Issued_SNF,"

        InnerOpClo += " SUM(STD_FAT_KG * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 0 else 1 end))  AS STD_Issued_FAT," & _
                      " SUM(STD_SNF_KG * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 0 else 1 end))  AS STD_Issued_SNF,"
        ''==============================================================================================================

        ''==================change the punching date check condition, previousliy it pick the data greater than to date and now it calc. on from date.
        InnerOpClo += " SUM((CASE WHEN Trans_Type='Opening' THEN STOCK_QTY ELSE 0 end)+ (STOCK_QTY * (CASE WHEN PUNCHING_DAte>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end)))  AS [Balance_Qty]," & _
                        " SUM((CASE WHEN Trans_Type='Opening' THEN Cost ELSE 0 end)+ (Cost * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening'  THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end)))  AS [Cost], " & _
                        " SUM((CASE WHEN Trans_Type='Opening' THEN QtyKG ELSE 0 end)+ (QtyKG * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end)))  AS Balance_QtyKG," & _
                        " SUM((CASE WHEN Trans_Type='Opening' THEN MilkFATKG ELSE 0 end)+ ((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end ) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end)))  AS [Balance_FAT]," & _
                        " SUM((CASE WHEN Trans_Type='Opening' THEN MilkSNFKG ELSE 0 end)+ ((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end ) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end)))  AS [Balance_SNF],"
        ''============================Monika 27/03/2017=================================================================
        InnerOpClo += " SUM((CASE WHEN Trans_Type='Opening' THEN ACT_FAT_KG ELSE 0 end)+ (ACT_FAT_KG * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end)))  AS [ACT_Balance_FAT]," & _
                        " SUM((CASE WHEN Trans_Type='Opening' THEN ACT_SNF_KG ELSE 0 end)+ (ACT_SNF_KG * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end)))  AS [ACT_Balance_SNF],"

        InnerOpClo += " SUM((CASE WHEN Trans_Type='Opening' THEN STD_FAT_KG ELSE 0 end)+ (STD_FAT_KG * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end)))  AS [STD_Balance_FAT]," & _
                      " SUM((CASE WHEN Trans_Type='Opening' THEN STD_SNF_KG ELSE 0 end)+ (STD_SNF_KG * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end)))  AS [STD_Balance_SNF]"
        ''==============================================================================================================

        Return InnerOpClo
    End Function

#Region "Load Million Data in Packets" ''Done by Monika 27/04/2017
    Public Shared Function GetDataSet_ForPartiallyLoad(ByVal rowOffset As Long, ByVal objFilter As clsStockRecoFilters) As DataSet
        Dim ds As New DataSet()
        Dim qry As String = ""
        Dim BufferCount As Integer = 50
        Dim cn As SqlConnection = clsDBFuncationality.GetConnnection()
        Try
            Dim strFinalQry As String = GetStockRecoQry(objFilter)

            cn.Close()
            cn.Open()
            qry = "select 0 as _lookup_row_number,Part_Data.* from ( " + Environment.NewLine + strFinalQry + Environment.NewLine + " )Part_Data where Part_Data.SNO>=@rowOffset and Part_Data.SNO<=@rowOffset + @bufferedRowCount "
            Dim da As SqlDataAdapter = New SqlDataAdapter(qry, cn)
            da.SelectCommand.CommandTimeout = 10000
            da.SelectCommand.Parameters.Add("@rowOffset", SqlDbType.Int)
            da.SelectCommand.Parameters("@rowOffset").Value = rowOffset

            da.SelectCommand.Parameters.Add("@bufferedRowCount", SqlDbType.Int)
            da.SelectCommand.Parameters("@bufferedRowCount").Value = BufferCount

            ''first dataset
            da.Fill(ds, "_lookup_result")

            'for (var x = 0; x < dss.Tables["_lookup_result"].Rows.Count; ++x)
            'dss.Tables["_lookup_result"].Rows[x]["_lookup_row_number"] = rowOffset + x;

            For x As Long = 0 To ds.Tables("_lookup_result").Rows.Count - 1
                ds.Tables("_lookup_result").Rows(x)("_lookup_row_number") = rowOffset + x
            Next

            ''second dataset
            qry = "select count(Part_Data.trans_id) as _count, " + clsCommon.myCstr(BufferCount) + " as _cache_count from ( " + Environment.NewLine + strFinalQry + Environment.NewLine + " )Part_Data "
            da = New SqlDataAdapter(qry, cn)
            da.SelectCommand.CommandTimeout = 10000

            da.Fill(ds, "_lookup_count")
            cn.Close()

            Return ds
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region


    Public Shared Function GetStockRecoQry(ByVal objFilter As clsStockRecoFilters) As String
        Try
            Dim Qry As String = GetBaseQryStockReco(objFilter)
            Dim strCodeColumn As String = ""
            Dim strCodeColumnMax As String = ""
            Dim strCodeDescColumn As String = ""
            Dim strCodeDescColumnMax As String = ""

            Dim strCodeColumnSelect As String = ""
            Dim strCodeDescColumnSelect As String = ""

            Dim strCodeColumnNull As String = ""
            Dim strCodeDescColumnNull As String = ""
            Dim strWhrCatg As String = ""
            Dim LocationFirstTime As Integer = 0
            Dim LocationAddress As String = String.Empty
            Dim dtCategory As DataTable = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+'DESC' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
            Dim strCategoryTable As String = ""
            If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
                For ii As Integer = 0 To dtCategory.Rows.Count - 1
                    If ii <> 0 Then
                        strCodeColumn += ","
                        strCodeColumnMax += ","
                        strCodeDescColumn += ","
                        strCodeDescColumnMax += ","

                        strCodeColumnSelect += ","
                        strCodeDescColumnSelect += ","

                        strCodeColumnNull += ","
                        strCodeDescColumnNull += ","
                    End If
                    strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeDescColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                    strCodeDescColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]"

                    strCodeColumnSelect += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeDescColumnSelect += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"

                    strCodeColumnNull += "null as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeDescColumnNull += "null as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                Next
                strCategoryTable = "select Item_Code," + strCodeColumnMax + "," + strCodeDescColumnMax + "  from (" + Environment.NewLine & _
                " select * from ( " + Environment.NewLine & _
                " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " + Environment.NewLine & _
                " ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc " + Environment.NewLine & _
                " ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " + Environment.NewLine & _
                " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc " + Environment.NewLine & _
                " from  TSPL_ITEM_MASTER  " + Environment.NewLine & _
                " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " + Environment.NewLine & _
                " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values" + Environment.NewLine & _
                " where 2=2 " + Environment.NewLine & _
                " )xx" + Environment.NewLine & _
                " Pivot " + Environment.NewLine & _
                " ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + strCodeColumn + ")" + Environment.NewLine & _
                " ) Pivt" + Environment.NewLine & _
                " Pivot " + Environment.NewLine & _
                " (" + Environment.NewLine & _
                " max(Category_Value_Desc) for Item_Category_CodeDesc in (" + strCodeDescColumn + ")" + Environment.NewLine & _
                " ) Pivt1 " + Environment.NewLine & _
                " ) xxx group by Item_Code "
                ''End of Category Table start now.
            End If
            Dim OuterOpClo As String = String.Empty
            Dim InnerOpClo As String = String.Empty
            OuterOpClo = GetStockRecoOpeningOuterPart(objFilter)
            InnerOpClo = GetStockRecoOpeningInnerPart(objFilter)

            'Dim LocationFirstTime As Integer = 0
            'Dim LocationAddress As String = String.Empty
            Dim strFinalQry As String = ""
            If clsCommon.CompairString(clsCommon.myCstr(objFilter.ReportType), "Item Type Wise Summary") = CompairStringResult.Equal Then
                strFinalQry = "select  Item_Type,Item_Type_Name, "
                strFinalQry += OuterOpClo
                strFinalQry += " from (" + Environment.NewLine
                strFinalQry += " select  Item_Type,max(Item_Type_Name) as Item_Type_Name,"
                strFinalQry += InnerOpClo
                strFinalQry += "  from (" + Qry + ")xxx Group by Item_Type )xxxx" + Environment.NewLine
            ElseIf clsCommon.CompairString(clsCommon.myCstr(objFilter.ReportType), "Item Group Wise Summary") = CompairStringResult.Equal Then
                strFinalQry = "select  Item_Type,Item_Type_Name,Item_Group,Group_Description, "
                strFinalQry += OuterOpClo
                strFinalQry += " from (" + Environment.NewLine
                strFinalQry += " select Item_Type,max(Item_Type_Name) as Item_Type_Name, Item_Group,max(Group_Description) as Group_Description,"
                strFinalQry += InnerOpClo
                strFinalQry += "  from (" + Qry + ")xxx Group by Item_Type,Item_Group )xxxx" + Environment.NewLine
            ElseIf clsCommon.CompairString(clsCommon.myCstr(objFilter.ReportType), "Category Wise Summary") = CompairStringResult.Equal Then
                strFinalQry = "select  Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Category_Struct_Code,Stock_Qty,Stock_UOM, "
                strFinalQry += OuterOpClo
                strFinalQry += " from (" + Environment.NewLine
                strFinalQry += " select  Item_Type,max(Item_Type_Name) as Item_Type_Name,Item_Group,max(Group_Description) as Group_Description, " + strCodeColumn + "," + strCodeDescColumnMax + ",Item_Category_Struct_Code,SUM(Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,MAX(Stock_UOM) Stock_UOM,"
                strFinalQry += InnerOpClo
                strFinalQry += "  from (" + Qry + ")xxx Group by Item_Type,Item_Group, Item_Category_Struct_Code," + strCodeColumn + " )xxxx" + Environment.NewLine
            ElseIf clsCommon.CompairString(clsCommon.myCstr(objFilter.ReportType), "Item Wise Summary") = CompairStringResult.Equal Then
                strFinalQry = "select Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ", Item_Category_Struct_Code  as Item_Category_Struct_Code,Item_Code,Item_Desc,itf_code,Stock_Qty,Stock_UOM,"
                strFinalQry += OuterOpClo
                If objFilter.MRPWise = True Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from (" + Environment.NewLine
                strFinalQry += " select  Item_Type,max(Item_Type_Name) as Item_Type_Name,Item_Group,max(Group_Description) as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",max(Item_Category_Struct_Code) as Item_Category_Struct_Code,Item_Code,MAX(Item_Desc) as Item_Desc,max(itf_code)as itf_code, SUM(Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,MAX(Stock_UOM) Stock_UOM,"
                strFinalQry += InnerOpClo
                If objFilter.MRPWise = True Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from (" + Qry + ")xxx Group by Item_Type,Item_Group,Item_Code"
                If objFilter.MRPWise Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " )xxxx" + Environment.NewLine
            ElseIf clsCommon.CompairString(clsCommon.myCstr(objFilter.ReportType), "Item And Location Wise Summary") = CompairStringResult.Equal Then
                strFinalQry = "select Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ", Item_Category_Struct_Code  as Item_Category_Struct_Code,Main_Location_Code as Main_Location_Code,MainLocationDesc as MainLocationDesc,Location_Code,[Loc Desp],Item_Code,Item_Desc,itf_code,Stock_Qty,Stock_UOM,"
                strFinalQry += OuterOpClo
                If objFilter.MRPWise = True Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += "  from (" + Environment.NewLine
                strFinalQry += " select  Item_Type,max(Item_Type_Name) as Item_Type_Name,Item_Group,max(Group_Description) as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",max(Item_Category_Struct_Code) as Item_Category_Struct_Code,Main_Location_Code,max(MainLocationDesc) as MainLocationDesc, Location_Code,max([Loc Desp]) as [Loc Desp],Item_Code,MAX(Item_Desc) as Item_Desc,max(itf_code)as itf_code, SUM(Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,MAX(Stock_UOM) Stock_UOM,"
                strFinalQry += InnerOpClo
                If objFilter.MRPWise = True Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from (" + Qry + ")xxx Group by Item_Type,Item_Group,Item_Code,Main_Location_Code,Location_Code"
                If objFilter.MRPWise Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " )xxxx" + Environment.NewLine

            ElseIf clsCommon.CompairString(clsCommon.myCstr(objFilter.ReportType), "Document Wise Detail") = CompairStringResult.Equal Then
                Dim FromMonthEndDate As Date = New DateTime(objFilter.From_Date.Year, objFilter.From_Date.Month, 1).AddMonths(1).AddDays(-1)
                Dim ToMonthStartDate As Date = New DateTime(objFilter.To_Date.Year, objFilter.To_Date.Month, 1)
                Dim ToMonthEndDate As Date = New DateTime(objFilter.To_Date.Year, objFilter.To_Date.Month, 1).AddMonths(1).AddDays(-1)

                If clsCommon.CompairString(objFilter.DisplayMethod, "AD") = CompairStringResult.Equal Then '' All Documents
                    strFinalQry = "select  case when '" & LocationFirstTime & "'='1' then '" & LocationAddress & "' else '" + objCommonVar.CurrentCompanyName + " ' end as CompName," & _
                        " '" + clsCommon.myCDate(objFilter.From_Date, "dd/MMM/yyyy") + "' as FromDate,'" + clsCommon.myCDate(objFilter.To_Date, "dd/MMM/yyyy") + "' as ToDate , " & _
                        " Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,convert(varchar,Punching_Date,103) as Punching_Date,InOutView, InOut,Location_Code, " & _
                        " [Loc Desp],PrimaryLocation,MainLocationDesc as MainLocationDesc,SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description, " & _
                        " " + strCodeColumn + "," + strCodeDescColumn + ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,Stock_Qty,Rate,Cost "
                    If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
                        strFinalQry += " ,isnull((CASE when Balance_QTYKG=0 then 0 else (ACTBalance_FAT*100/Balance_QTYKG) end),0) as Balance_FATPER ,ACTBalance_FAT as Balance_FAT, " & _
                      " isnull((CASE when Balance_QTYKG=0 then 0 else ([ACTBalance_SNF]*100/[Balance_QTYKG]) end),0) as [Balance_SNFPER] , ACTBalance_SNF as Balance_SNF "
                    Else
                        strFinalQry += " ,isnull((CASE when Balance_QTYKG=0 then 0 else (Balance_FAT*100/Balance_QTYKG) end),0) as Balance_FATPER ,Balance_FAT, " & _
                        " isnull((CASE when Balance_QTYKG=0 then 0 else ([Balance_SNF]*100/[Balance_QTYKG]) end),0) as [Balance_SNFPER] , Balance_SNF "
                    End If
                    ''===================Monika 03/04/2017=========================================
                    If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                        strFinalQry += " ,isnull((CASE when Balance_QTYKG=0 then 0 else (ACTBalance_FAT*100/Balance_QTYKG) end),0) as ACTBalance_FATPER ,ACTBalance_FAT, " & _
                        " isnull((CASE when Balance_QTYKG=0 then 0 else ([ACTBalance_SNF]*100/[Balance_QTYKG]) end),0) as [ACTBalance_SNFPER] , ACTBalance_SNF,(Balance_FAT-ACTBalance_FAT) as [Diff FAT],(Balance_SNF-ACTBalance_SNF) as [Diff SNF] "
                    End If
                    If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                        strFinalQry += " ,isnull((CASE when Balance_QTYKG=0 then 0 else (STDBalance_FAT*100/Balance_QTYKG) end),0) as STDBalance_FATPER ,STDBalance_FAT, " & _
                        " isnull((CASE when Balance_QTYKG=0 then 0 else ([STDBalance_SNF]*100/[Balance_QTYKG]) end),0) as [STDBalance_SNFPER] , STDBalance_SNF "
                    End If
                    ''==============================================================================

                    If objFilter.MRPWise Then
                        strFinalQry += ",MRP "
                    End If
                    strFinalQry += " from ("
                    strFinalQry += " select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,InOutView, InOut,Main_Location_Code,MainLocationDesc,Location_Code,[Loc Desp], [LocAddress],PrimaryLocation,SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,(Stock_Qty * case when InOut='I' or Trans_Type='Opening' then 1 else -1 end) as Stock_Qty,(QtyKG * case when InOut='I' or Trans_Type='Opening' then 1 else -1 end) as Balance_QTYKG,convert(decimal(18,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' or Trans_Type='Opening' then 1 else -1 end) as Cost,( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' or Trans_Type='Opening' then 1 else -1 end) as Balance_FAT, ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' or Trans_Type='Opening' then 1 else -1 end) as Balance_SNF  "

                    ''===========================Monika 03/04/2017==========================================
                    strFinalQry += ",( ACT_FAT_KG * case when InOut='I' or Trans_Type='Opening' then 1 else -1 end) as ACTBalance_FAT, (ACT_SNF_KG * case when InOut='I' or Trans_Type='Opening' then 1 else -1 end) as ACTBalance_SNF "

                    strFinalQry += ",( STD_FAT_KG * case when InOut='I' or Trans_Type='Opening' then 1 else -1 end) as STDBalance_FAT, (STD_SNF_KG * case when InOut='I' or Trans_Type='Opening' then 1 else -1 end) as STDBalance_SNF "
                    ''========================================================================================

                    If objFilter.MRPWise Then
                        strFinalQry += ",MRP "
                    End If
                    strFinalQry += " from (" + Qry + ") xxx " + Environment.NewLine
                    strFinalQry += " where Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' " + Environment.NewLine
                    strFinalQry += ")xxxxxx "
                    strFinalQry = strFinalQry
                    If objFilter.isPrintCrystal = 0 Then
                        strFinalQry += " Order by convert(date,Punching_Date,103),Trans_Id"
                    End If
                ElseIf clsCommon.CompairString(objFilter.DisplayMethod, "DL") = CompairStringResult.Equal Then
                    Dim PeriodCol As String = "convert(varchar,Punching_Date,103) as DisplayPeriod,(case when Trans_Type='Opening' then 1 else 2 end) as DisplayId "
                    strFinalQry = GetQuery(strFinalQry, Qry, objFilter, LocationFirstTime, LocationAddress, strCodeColumn, strCodeDescColumn, PeriodCol)

                ElseIf clsCommon.CompairString(objFilter.DisplayMethod, "MT") = CompairStringResult.Equal Then
                    Dim PeriodCol As String = "(cast(DATENAME(month,Punching_Date) as varchar(3)) + '-' + DATENAME(YEAR,Punching_Date)) as DisplayPeriod,(case when Trans_Type='Opening' then 1 else 3 end) as DisplayId "
                    strFinalQry = GetQuery(strFinalQry, Qry, objFilter, LocationFirstTime, LocationAddress, strCodeColumn, strCodeDescColumn, PeriodCol)

                ElseIf clsCommon.CompairString(objFilter.DisplayMethod, "QTR") = CompairStringResult.Equal Then
                    Dim PeriodCol As String = "(DATENAME(QUARTER,Punching_Date) + '-' + DATENAME(YEAR,Punching_Date)) as DisplayPeriod,(case when Trans_Type='Opening' then 1 else 3 end) as DisplayId "
                    strFinalQry = GetQuery(strFinalQry, Qry, objFilter, LocationFirstTime, LocationAddress, strCodeColumn, strCodeDescColumn, PeriodCol)

                ElseIf clsCommon.CompairString(objFilter.DisplayMethod, "YRL") = CompairStringResult.Equal Then
                    Dim PeriodCol As String = "(DATENAME(YEAR,Punching_Date)) as DisplayPeriod,(case when Trans_Type='Opening' then 1 else 3 end) as DisplayId "
                    strFinalQry = GetQuery(strFinalQry, Qry, objFilter, LocationFirstTime, LocationAddress, strCodeColumn, strCodeDescColumn, PeriodCol)

                ElseIf clsCommon.CompairString(objFilter.DisplayMethod, "MM") = CompairStringResult.Equal Then
                    Dim PeriodCol As String = "(case when Trans_Type='Opening' then convert(varchar,Punching_Date,103) when cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(FromMonthEndDate, "dd-MMM-yyyy") & "' then convert(varchar,Punching_Date,103) when cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(ToMonthStartDate, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "' then convert(varchar,Punching_Date,103)  else (cast(DATENAME(month,Punching_Date) as varchar(3)) + '-' + DATENAME(YEAR,Punching_Date)) end) as DisplayPeriod,(case when Trans_Type='Opening' then 1 when cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(FromMonthEndDate, "dd-MMM-yyyy") & "' then 2 when cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(ToMonthStartDate, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "' then 4 else 3 end) as DisplayId "
                    strFinalQry = GetQuery(strFinalQry, Qry, objFilter, LocationFirstTime, LocationAddress, strCodeColumn, strCodeDescColumn, PeriodCol)

                End If

            ElseIf clsCommon.CompairString(clsCommon.myCstr(objFilter.ReportType), "Date, Item And Document Wise Detail") = CompairStringResult.Equal Then
                '' change by Panch Raj against Ticket No: GKD/09/05/18-000128
                strFinalQry = "select row_number() over (order by Trans_ID,Location_Code,Punching_Date) as SNO,CompName,FromDate,ToDate,Trans_Id,Main_Location_Code as Main_Location_Code,MainLocationDesc as MainLocationDesc,Location_Code,[Loc Desp],SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Category_Struct_Code,itf_code ,Stock_Qty,Rate,Cost,SourceCode,SourceName,Source_Doc_No,Punching_Date,Trans_Type,Trans_Type_Name,InOut,InOutView,Item_Code ,Item_Desc,Stock_UOM, (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)) as OPQty, case when (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))=0 then 0 else  ((isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0))/((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)))) end as OPRate "
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
                    strFinalQry += ",(isnull(ACTCLBalance_FAT,0)-isnull(ACTRecFAT,0)+isnull(ACTIssFAT,0)) as OPFAT, isnull((CASE when (CLBalance_QTYKG-Balance_QTYKG) =0 then 0 else ((ACTCLBalance_FAT-ACTBalance_FAT)*100/(CLBalance_QTYKG-Balance_QTYKG)) end),0) as OPFATPER, (isnull(ACTCLBalance_SNF,0)-isnull(ACTRecSNF,0)+isnull(ACTIssSNF,0)) as OPSNF, isnull((CASE when (CLBalance_QTYKG-Balance_QTYKG) =0 then 0 else ((ACTCLBalance_SNF-ACTBalance_SNF)*100/(CLBalance_QTYKG-Balance_QTYKG)) end),0) as OPSNFPER, "
                Else
                    strFinalQry += ",(isnull(CLBalance_FAT,0)-isnull(RecFAT,0)+isnull(IssFAT,0)) as OPFAT, isnull((CASE when (CLBalance_QTYKG-Balance_QTYKG) =0 then 0 else ((CLBalance_FAT-Balance_FAT)*100/(CLBalance_QTYKG-Balance_QTYKG)) end),0) as OPFATPER, (isnull(CLBalance_SNF,0)-isnull(RecSNF,0)+isnull(IssSNF,0)) as OPSNF, isnull((CASE when (CLBalance_QTYKG-Balance_QTYKG) =0 then 0 else ((CLBalance_SNF-Balance_SNF)*100/(CLBalance_QTYKG-Balance_QTYKG)) end),0) as OPSNFPER, "
                End If
                ''===========================Monika 28/03/2017============================
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " (isnull(ACTCLBalance_FAT,0)-isnull(ACTRecFAT,0)+isnull(ACTIssFAT,0)) as ACTOPFAT, isnull((CASE when (CLBalance_QTYKG-Balance_QTYKG) =0 then 0 else ((ACTCLBalance_FAT-ACTBalance_FAT)*100/(CLBalance_QTYKG-Balance_QTYKG)) end),0) as ACTOPFATPER, (isnull(ACTCLBalance_SNF,0)-isnull(ACTRecSNF,0)+isnull(ACTIssSNF,0)) as ACTOPSNF, isnull((CASE when (CLBalance_QTYKG-Balance_QTYKG) =0 then 0 else ((ACTCLBalance_SNF-ACTBalance_SNF)*100/(CLBalance_QTYKG-Balance_QTYKG)) end),0) as ACTOPSNFPER, "
                End If

                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " (isnull(STDCLBalance_FAT,0)-isnull(STDRecFAT,0)+isnull(STDIssFAT,0)) as STDOPFAT, isnull((CASE when (CLBalance_QTYKG-Balance_QTYKG) =0 then 0 else ((STDCLBalance_FAT-STDBalance_FAT)*100/(CLBalance_QTYKG-Balance_QTYKG)) end),0) as STDOPFATPER, (isnull(STDCLBalance_SNF,0)-isnull(STDRecSNF,0)+isnull(STDIssSNF,0)) as STDOPSNF, isnull((CASE when (CLBalance_QTYKG-Balance_QTYKG) =0 then 0 else ((STDCLBalance_SNF-STDBalance_SNF)*100/(CLBalance_QTYKG-Balance_QTYKG)) end),0) as STDOPSNFPER, "
                End If
                ''========================================================================

                strFinalQry += "(isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0)) as OPCost, RecQty,RecRate "
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
                    strFinalQry += ",ACTRecFAT as RecFAT,ACTRecFATPER as RecFATPER,ACTRecSNF as RecSNF,ACTRecSNFPER as RecSNFPER, "
                Else
                    strFinalQry += ",RecFAT,RecFATPER,RecSNF,RecSNFPER, "
                End If
                ''===========================Monika 28/03/2017============================
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += "ACTRecFAT,ACTRecFATPER,ACTRecSNF,ACTRecSNFPER,"
                End If
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += "STDRecFAT,STDRecFATPER,STDRecSNF,STDRecSNFPER,"
                End If
                ''========================================================================

                strFinalQry += "RecCost ,IssQty,IssRate "
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
                    strFinalQry += ",ACTIssFAT as IssFAT,ACTIssFATPER as IssFATPER,ACTIssSNF as IssSNF,ACTIssSNFPER as IssSNFPER, "
                Else
                    strFinalQry += ",IssFAT,IssFATPER,IssSNF,IssSNFPER,"
                End If
                ''===========================Monika 28/03/2017============================
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " ACTIssFAT,ACTIssFATPER,ACTIssSNF,ACTIssSNFPER, "
                End If
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " STDIssFAT,STDIssFATPER,STDIssSNF,STDIssSNFPER, "
                End If
                ''========================================================================

                strFinalQry += "IssCost ,CLQty ,case when isnull(CLQty,0)=0 then 0 else CLCost/CLQty end as CLRate"
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
                    strFinalQry += ", ACTCLBalance_FAT as CLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else (ACTCLBalance_FAT*100/CLBalance_QTYKG) end),0) as CLFATPER, ACTCLBalance_SNF as CLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else (ACTCLBalance_SNF*100/CLBalance_QTYKG) end),0) as CLSNFPER, "
                Else
                    strFinalQry += ", CLBalance_FAT as CLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_FAT*100/CLBalance_QTYKG) end),0) as CLFATPER, CLBalance_SNF as CLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_SNF*100/CLBalance_QTYKG) end),0) as CLSNFPER, "
                End If
                ''===========================Monika 28/03/2017============================
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " ACTCLBalance_FAT as ACTCLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else (ACTCLBalance_FAT*100/CLBalance_QTYKG) end),0) as ACTCLFATPER, ACTCLBalance_SNF as ACTCLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else (ACTCLBalance_SNF*100/CLBalance_QTYKG) end),0) as ACTCLSNFPER,round((CLBalance_FAT-ACTCLBalance_FAT),3) as [Diff Fat KG],round((CLBalance_SNF-ACTCLBalance_SNF),3) as [Diff SNF KG],"
                End If
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " STDCLBalance_FAT as STDCLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else (STDCLBalance_FAT*100/CLBalance_QTYKG) end),0) as STDCLFATPER, STDCLBalance_SNF as STDCLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else (STDCLBalance_SNF*100/CLBalance_QTYKG) end),0) as STDCLSNFPER,"
                End If
                ''========================================================================

                strFinalQry += " CLCost from ( "

                ''===========================Monika 28/03/2017============================
                ''add Production QC FAT/SNF at beolw table============================
                strFinalQry += "select  case when '" & LocationFirstTime & "'='1' then '" & LocationAddress & "' else '" + objCommonVar.CurrentCompanyName + " ' end as CompName,'" + clsCommon.myCDate(objFilter.From_Date, "dd/MMM/yyyy") + "' as FromDate,'" + clsCommon.myCDate(objFilter.To_Date, "dd/MMM/yyyy") + "' as ToDate ,  Trans_Id,Main_Location_Code,MainLocationDesc,Location_Code,[Loc Desp],SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description ," + strCodeColumn + "," + strCodeDescColumn + ", Item_Category_Struct_Code,itf_code ,Stock_Qty,Rate,Cost,SourceCode,SourceName,Source_Doc_No,Punching_Date as Punching_Date,Trans_Type,Trans_Type_Name,InOut,InOutView, Item_Code ,Item_Desc,Stock_UOM,Balance_FAT,Balance_SNF" & _
                    ",ACTBalance_Fat,ActBalance_SNF,STDBalance_Fat,STDBalance_SNF,isnull(Balance_QTYKG,0) as Balance_QTYKG, (case when InOut='I' then Stock_Qty else 0 end) as RecQty,  (case when InOut='I' then Rate else 0 end) as RecRate, (case when InOut='I' then Balance_FAT else 0 end) as RecFAT, (case when InOut='I' then isnull((CASE when Balance_QTYKG=0 then 0 else (Balance_FAT*100/Balance_QTYKG) end),0) else 0 end ) as RecFATPER, (case when InOut='I' then Balance_SNF else 0 end) as RecSNF, (case when InOut='I' then isnull((CASE when Balance_QTYKG=0 then 0 else ([Balance_SNF]*100/[Balance_QTYKG]) end),0) else 0 end) as RecSNFPER," & _
                    " (case when InOut='I' then ACTBalance_FAT else 0 end) as ACTRecFAT, (case when InOut='I' then isnull((CASE when Balance_QTYKG=0 then 0 else (ACTBalance_FAT*100/Balance_QTYKG) end),0) else 0 end ) as ACTRecFATPER, (case when InOut='I' then ACTBalance_SNF else 0 end) as ACTRecSNF, (case when InOut='I' then isnull((CASE when Balance_QTYKG=0 then 0 else ([ACTBalance_SNF]*100/[Balance_QTYKG]) end),0) else 0 end) as ACTRecSNFPER, " & _
                    " (case when InOut='I' then STDBalance_FAT else 0 end) as STDRecFAT, (case when InOut='I' then isnull((CASE when Balance_QTYKG=0 then 0 else (STDBalance_FAT*100/Balance_QTYKG) end),0) else 0 end ) as STDRecFATPER, (case when InOut='I' then STDBalance_SNF else 0 end) as STDRecSNF, (case when InOut='I' then isnull((CASE when Balance_QTYKG=0 then 0 else ([STDBalance_SNF]*100/[Balance_QTYKG]) end),0) else 0 end) as STDRecSNFPER, " & _
                    " (case when InOut='I' then Cost else 0 end) as RecCost, (case when InOut='O' then -1*Stock_Qty else 0 end) as IssQty, (case when InOut='O' then Rate else 0 end) as IssRate, (case when InOut='O' then -1*Balance_FAT else 0 end) as IssFAT,(case when InOut='O' then isnull((CASE when Balance_QTYKG=0 then 0 else (Balance_FAT*100/Balance_QTYKG) end),0) else 0 end ) as IssFATPER, (case when InOut='O' then -1*Balance_SNF else 0 end) as IssSNF, (case when InOut='O' then isnull((CASE when Balance_QTYKG=0 then 0 else ([Balance_SNF]*100/[Balance_QTYKG]) end),0) else 0 end) as IssSNFPER," & _
                    " (case when InOut='O' then -1* ACTBalance_FAT else 0 end) as ACTIssFAT,(case when InOut='O' then isnull((CASE when Balance_QTYKG=0 then 0 else (ACTBalance_FAT*100/Balance_QTYKG) end),0) else 0 end ) as ACTIssFATPER, (case when InOut='O' then -1* ACTBalance_SNF else 0 end) as ACTIssSNF, (case when InOut='O' then isnull((CASE when Balance_QTYKG=0 then 0 else ([ACTBalance_SNF]*100/[Balance_QTYKG]) end),0) else 0 end) as ACTIssSNFPER, " & _
                    " (case when InOut='O' then -1* STDBalance_FAT else 0 end) as STDIssFAT,(case when InOut='O' then isnull((CASE when Balance_QTYKG=0 then 0 else (STDBalance_FAT*100/Balance_QTYKG) end),0) else 0 end ) as STDIssFATPER, (case when InOut='O' then -1* STDBalance_SNF else 0 end) as STDIssSNF, (case when InOut='O' then isnull((CASE when Balance_QTYKG=0 then 0 else ([STDBalance_SNF]*100/[Balance_QTYKG]) end),0) else 0 end) as STDIssSNFPER, " & _
                    " (case when InOut='O' then -1*Cost else 0 end) as IssCost, SUM(Stock_Qty) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLQty  ,SUM(Cost) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLCost,SUM(isnull(Balance_QTYKG,0)) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLBalance_QTYKG ,SUM(Balance_FAT) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLBalance_FAT,SUM(Balance_SNF) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLBalance_SNF, " & _
                    " SUM(ACTBalance_FAT) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as ACTCLBalance_FAT,SUM(ACTBalance_SNF) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as ACTCLBalance_SNF, " & _
                    " SUM(STDBalance_FAT) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as STDCLBalance_FAT,SUM(STDBalance_SNF) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as STDCLBalance_SNF "
                If objFilter.MRPWise Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from ("
                strFinalQry += "select 0 as Trans_Id,'' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,null as Punching_Date,'' as InOutView, '' as InOut,'' as Main_Location_Code,'' as MainLocationDesc,'' as Location_Code,'' as [Loc Desp],'' as [LocAddress],'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",xxx.Item_Code as Item_Code ,max(xxx.Item_Desc) as Item_Desc,'' as Item_Category_Struct_Code,max(xxx.Stock_UOM) as Stock_UOM,'' as itf_code ,sum( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG, " & _
                    " case when sum(convert(decimal(18,3),Stock_Qty* case when InOut='I' then 1 else -1 end))=0 then 0 else sum(convert(decimal(18,3),Cost* case when InOut='I' then 1 else -1 end))/sum(convert(decimal(18,3),Stock_Qty* case when InOut='I' then 1 else -1 end)) end as Rate,sum(Cost * case when InOut='I' then 1 else -1 end) as Cost," & _
                    " sum( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end)) as Balance_SNF "
                If objFilter.MRPWise Then
                    strFinalQry += ",Max(MRP) as MRP "
                End If
                ''===========================Monika 28/03/2017============================
                strFinalQry += " ,sum( ACT_FAT_KG * case when InOut='I' then 1 else -1 end) as ACTBalance_FAT,sum(ACT_SNF_KG * (case when InOut='I' then 1 else -1 end)) as ACTBalance_SNF "

                strFinalQry += " ,sum( STD_FAT_KG * case when InOut='I' then 1 else -1 end) as STDBalance_FAT,sum(STD_SNF_KG * (case when InOut='I' then 1 else -1 end)) as STDBalance_SNF "
                ''=========================================================================================

                strFinalQry += " from (" + Qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' group by xxx.Item_Code " + Environment.NewLine
                strFinalQry += " union all "
                strFinalQry += " select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,InOutView, InOut,Main_Location_Code,MainLocationDesc,Location_Code,[Loc Desp], [LocAddress],SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG,convert(decimal(18,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1 else -1 end) as Cost,( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT, ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end) as Balance_SNF  "
                If objFilter.MRPWise Then
                    strFinalQry += ",MRP "
                End If

                ''===========================Monika 28/03/2017============================
                strFinalQry += ",( ACT_FAT_KG * case when InOut='I' then 1 else -1 end) as ACTBalance_FAT, ( ACT_SNF_KG * case when InOut='I' then 1 else -1 end) as ACTBalance_SNF "

                strFinalQry += ",( STD_FAT_KG * case when InOut='I' then 1 else -1 end) as STDBalance_FAT, ( STD_SNF_KG * case when InOut='I' then 1 else -1 end) as STDBalance_SNF "
                ''=========================================================================================

                strFinalQry += " from (" + Qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' " + Environment.NewLine

                strFinalQry += ")xxxxxx  )xxxxxxx where Trans_Id<>0  "
                If Not objFilter.ChkPartialyLoadData AndAlso objFilter.isPrintCrystal = 0 Then
                    strFinalQry += " Order by  Punching_Date,Trans_Id "
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(objFilter.ReportType), "Date and Item Wise Stock") = CompairStringResult.Equal Then

                strFinalQry = "select Location_Code,[Loc Desp],Main_Location_Code as Main_Location_Code,MainLocationDesc as MainLocationDesc,convert(varchar, Punching_Date,103) as Punching_Date  ,Item_Code ,Item_Desc,Stock_UOM, Convert(decimal(18,2),(ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))) as OPQty, case when (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))=0 then 0 else  Convert(decimal(18,2),((isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0))/((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))))) end as OPRate "
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
                    strFinalQry += ",Convert(decimal(18,3),(isnull(ACTCLBalance_FAT,0)-isnull(ACTRecFAT,0)+isnull(ACTIssFAT,0))) as OPFAT , Convert(decimal(18,3),isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((ACTCLBalance_FAT+ACTBalance_FAT)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0)) as OPFATPER, Convert(decimal(18,3),(isnull(ACTCLBalance_SNF,0)-isnull(ACTRecSNF,0)+isnull(ACTIssSNF,0))) as OPSNF , Convert(decimal(18,3),isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((ACTCLBalance_SNF+ACTBalance_SNF)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0)) as OPSNFPER,"
                Else
                    strFinalQry += ",Convert(decimal(18,3),(isnull(CLBalance_FAT,0)-isnull(RecFAT,0)+isnull(IssFAT,0))) as OPFAT , Convert(decimal(18,3),isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_FAT+Balance_FAT)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0)) as OPFATPER, Convert(decimal(18,3),(isnull(CLBalance_SNF,0)-isnull(RecSNF,0)+isnull(IssSNF,0))) as OPSNF , Convert(decimal(18,3),isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_SNF+Balance_SNF)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0)) as OPSNFPER,"
                End If
                ''===========================Monika 28/03/2017============================
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " Convert(decimal(18,3),(isnull(ACTCLBalance_FAT,0)-isnull(ACTRecFAT,0)+isnull(ACTIssFAT,0))) as ACTOPFAT , Convert(decimal(18,3),isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((ACTCLBalance_FAT+ACTBalance_FAT)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0)) as ACTOPFATPER, Convert(decimal(18,3),(isnull(ACTCLBalance_SNF,0)-isnull(ACTRecSNF,0)+isnull(ACTIssSNF,0))) as ACTOPSNF , Convert(decimal(18,3),isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((ACTCLBalance_SNF+ACTBalance_SNF)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0)) as ACTOPSNFPER, "
                End If
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " Convert(decimal(18,3),(isnull(STDCLBalance_FAT,0)-isnull(STDRecFAT,0)+isnull(STDIssFAT,0))) as STDOPFAT , Convert(decimal(18,3),isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((STDCLBalance_FAT+STDBalance_FAT)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0)) as STDOPFATPER, Convert(decimal(18,3),(isnull(STDCLBalance_SNF,0)-isnull(STDRecSNF,0)+isnull(STDIssSNF,0))) as STDOPSNF , Convert(decimal(18,3),isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((STDCLBalance_SNF+STDBalance_SNF)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0)) as STDOPSNFPER, "
                End If
                ''=========================================================================================

                strFinalQry += " Convert(decimal(18,2),(isnull(CLCost,0)-isnull(RecCost,0)+ isnull(IssCost,0))) as OPCost, RecPurQty,RecPurRate "
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
                    strFinalQry += ",ACTRecPurFAT as RecPurFAT,ACTRecPurFATPER as RecPurFATPER,ACTRecPurSNF as RecPurSNF,ACTRecPurSNFPER as RecPurSNFPER,"
                Else
                    strFinalQry += ",RecPurFAT,RecPurFATPER,RecPurSNF,RecPurSNFPER,"
                End If
                ''===========================Monika 28/03/2017============================
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " ACTRecPurFAT,ACTRecPurFATPER,ACTRecPurSNF,ACTRecPurSNFPER,"
                End If
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " STDRecPurFAT,STDRecPurFATPER,STDRecPurSNF,STDRecPurSNFPER,"
                End If
                ''=========================================================================================

                strFinalQry += " RecPurCost ,RecAdjProQty,RecAdjProRate  "
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
                    strFinalQry += ",ACTRecAdjProFAT as RecAdjProFAT ,ACTRecAdjProFATPER as RecAdjProFATPER,ACTRecAdjProSNF as RecAdjProSNF ,ACTRecAdjProSNFPER as RecAdjProSNFPER "
                Else
                    strFinalQry += ",RecAdjProFAT ,RecAdjProFATPER,RecAdjProSNF ,RecAdjProSNFPER "
                End If
                ''===========================Monika 28/03/2017============================
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " ,ACTRecAdjProFAT ,ACTRecAdjProFATPER,ACTRecAdjProSNF ,ACTRecAdjProSNFPER "
                End If
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " ,STDRecAdjProFAT ,STDRecAdjProFATPER,STDRecAdjProSNF ,STDRecAdjProSNFPER "
                End If
                ''=========================================================================================

                strFinalQry += ",RecAdjProCost ,RecOthQty,RecOthRate  "
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
                    strFinalQry += ",ACTRecOthFAT as RecOthFAT ,ACTRecOthFATPER as RecOthFATPER ,ACTRecOthSNF as RecOthSNF,ACTRecOthSNFPER as RecOthSNFPER "
                Else
                    strFinalQry += ",RecOthFAT ,RecOthFATPER ,RecOthSNF,RecOthSNFPER "
                End If
                ''===========================Monika 28/03/2017============================
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " ,ACTRecOthFAT ,ACTRecOthFATPER ,ACTRecOthSNF,ACTRecOthSNFPER "
                End If
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " ,STDRecOthFAT ,STDRecOthFATPER ,STDRecOthSNF,STDRecOthSNFPER "
                End If
                ''=========================================================================================

                strFinalQry += " ,RecOthCost,RecQty,RecRate "
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
                    strFinalQry += ",ACTRecFAT as RecFAT,ACTRecFATPER as RecFATPER,ACTRecSNF as RecSNF,ACTRecSNFPER as RecSNFPER "
                Else
                    strFinalQry += ",RecFAT,RecFATPER,RecSNF,RecSNFPER "
                End If
                ''===========================Monika 28/03/2017============================
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += ",ACTRecFAT,ACTRecFATPER,ACTRecSNF,ACTRecSNFPER "
                End If
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += ",STDRecFAT,STDRecFATPER,STDRecSNF,STDRecSNFPER "
                End If
                ''=========================================================================================

                strFinalQry += " ,RecCost  ,IssSaleQty ,IssSaleRate  "
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
                    strFinalQry += ",ACTIssSaleFAT as IssSaleFAT ,ACTIssSaleFATPER as IssSaleFATPER,ACTIssSaleSNF as IssSaleSNF,ACTIssSaleSNFPER as IssSaleSNFPER "
                Else
                    strFinalQry += ",IssSaleFAT ,IssSaleFATPER,IssSaleSNF,IssSaleSNFPER "
                End If
                ''===========================Monika 28/03/2017============================
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " ,ACTIssSaleFAT ,ACTIssSaleFATPER,ACTIssSaleSNF,ACTIssSaleSNFPER "
                End If
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " ,STDIssSaleFAT ,STDIssSaleFATPER,STDIssSaleSNF,STDIssSaleSNFPER "
                End If
                ''=========================================================================================

                strFinalQry += " ,IssSaleCost , IssIssAdjQty , IssIssAdjRate  "
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
                    strFinalQry += ", ACTIssIssAdjFAT as IssIssAdjFAT , ACTIssIssAdjFATPER as IssIssAdjFATPER ,ACTIssIssAdjSNF as IssIssAdjSNF , ACTIssIssAdjSNFPER as IssIssAdjSNFPER "
                Else
                    strFinalQry += ",IssIssAdjFAT , IssIssAdjFATPER ,IssIssAdjSNF , IssIssAdjSNFPER"
                End If
                ''===========================Monika 28/03/2017============================
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " , ACTIssIssAdjFAT , ACTIssIssAdjFATPER ,ACTIssIssAdjSNF , ACTIssIssAdjSNFPER "
                End If
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " , STDIssIssAdjFAT , STDIssIssAdjFATPER ,STDIssIssAdjSNF , STDIssIssAdjSNFPER "
                End If
                ''=========================================================================================

                strFinalQry += ",IssIssAdjCost , IssOthQty , IssOthRate  "
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
                    strFinalQry += ", ACTIssOthFAT as IssOthFAT,ACTIssOthFATPER as IssOthFATPER , ACTIssOthSNF as IssOthSNF ,ACTIssOthSNFPER as IssOthSNFPER"
                Else
                    strFinalQry += ", IssOthFAT ,IssOthFATPER , IssOthSNF ,IssOthSNFPER"
                End If
                ''===========================Monika 28/03/2017============================
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " , ACTIssOthFAT ,ACTIssOthFATPER , ACTIssOthSNF ,ACTIssOthSNFPER "
                End If
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " , STDIssOthFAT ,STDIssOthFATPER , STDIssOthSNF ,STDIssOthSNFPER "
                End If
                ''=========================================================================================

                strFinalQry += ",IssOthCost ,IssQty,IssRate "
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
                    strFinalQry += ",ACTIssFAT as IssFAT,ACTIssFATPER as IssFATPER,ACTIssSNF as IssSNF,ACTIssSNFPER as IssSNFPER "
                Else
                    strFinalQry += ",IssFAT,IssFATPER,IssSNF,IssSNFPER"
                End If
                ''===========================Monika 28/03/2017============================
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " ,ACTIssFAT,ACTIssFATPER,ACTIssSNF,ACTIssSNFPER "
                End If
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " ,STDIssFAT,STDIssFATPER,STDIssSNF,STDIssSNFPER "
                End If
                ''=========================================================================================

                strFinalQry += " ,IssCost ,CLQty ,case when isnull(CLQty,0)=0 then 0 else CLCost/CLQty end as CLRate "
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
                    strFinalQry += ", ACTCLBalance_FAT as CLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else (ACTCLBalance_FAT*100/CLBalance_QTYKG) end),0) as CLFATPER, ACTCLBalance_SNF as CLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else (ACTCLBalance_SNF*100/CLBalance_QTYKG) end),0) as CLSNFPER "
                Else
                    strFinalQry += ", CLBalance_FAT as CLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_FAT*100/CLBalance_QTYKG) end),0) as CLFATPER, CLBalance_SNF as CLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_SNF*100/CLBalance_QTYKG) end),0) as CLSNFPER "
                End If
                ''===========================Monika 28/03/2017============================
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " , ACTCLBalance_FAT as ACTCLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else (ACTCLBalance_FAT*100/CLBalance_QTYKG) end),0) as ACTCLFATPER, ACTCLBalance_SNF as ACTCLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else (ACTCLBalance_SNF*100/CLBalance_QTYKG) end),0) as ACTCLSNFPER,round((CLBalance_FAT-ACTCLBalance_FAT),3) as [Diff FAT KG],round((CLBalance_SNF-ACTCLBalance_SNF),3) as [Diff SNF KG] "
                End If
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += " , STDCLBalance_FAT as STDCLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else (STDCLBalance_FAT*100/CLBalance_QTYKG) end),0) as STDCLFATPER, STDCLBalance_SNF as STDCLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else (STDCLBalance_SNF*100/CLBalance_QTYKG) end),0) as STDCLSNFPER "
                End If
                ''=========================================================================================

                strFinalQry += " , CLCost from ( "
                strFinalQry += "select  Main_Location_Code,max(MainLocationDesc) as MainLocationDesc,Location_Code,max([Loc Desp]) as [Loc Desp],Punching_Date as Punching_Date, Item_Code ,max(Item_Desc) as Item_Desc, max(Stock_UOM) as Stock_UOM, sum(Balance_FAT) as Balance_FAT,sum(Balance_SNF) as Balance_SNF, " & _
                    " sum(ACTBalance_FAT) as ACTBalance_FAT,sum(ACTBalance_SNF) as ACTBalance_SNF,sum(STDBalance_FAT) as STDBalance_FAT,sum(STDBalance_SNF) as STDBalance_SNF  ,sum(isnull(Balance_QTYKG,0)) as Balance_QTYKG ,sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end) as RecPurQty ,sum(case when InOut='I' and In_Category in ('PU') then Rate else 0 end) as RecPurRate  ,sum(case when InOut='I' and In_Category in ('PU') then Balance_FAT else 0 end) as RecPurFAT  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as RecPurFATPER  ,sum(case when InOut='I' and In_Category in ('PU') then Balance_SNF else 0 end) as RecPurSNF  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as RecPurSNFPER  ," & _
                    " sum(case when InOut='I' and In_Category in ('PU') then ACTBalance_FAT else 0 end) as ACTRecPurFAT  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  ACTBalance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as ACTRecPurFATPER  ,sum(case when InOut='I' and In_Category in ('PU') then ACTBalance_SNF else 0 end) as ACTRecPurSNF  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  ACTBalance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as ACTRecPurSNFPER, " & _
                    " sum(case when InOut='I' and In_Category in ('PU') then STDBalance_FAT else 0 end) as STDRecPurFAT  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  STDBalance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as STDRecPurFATPER  ,sum(case when InOut='I' and In_Category in ('PU') then STDBalance_SNF else 0 end) as STDRecPurSNF  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  STDBalance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as STDRecPurSNFPER, " & _
                    "sum(case when InOut='I' and In_Category in ('PU') then Cost else 0 end) as RecPurCost  ,sum(case when InOut='I' and In_Category in ('AD') then Stock_Qty else 0 end) as RecAdjProQty  ,sum(case when InOut='I' and In_Category in ('AD') then Rate else 0 end) as RecAdjProRate  ,sum(case when InOut='I' and In_Category in ('AD') then Balance_FAT else 0 end) as RecAdjProFAT  ,(case when sum(case when InOut='I' and In_Category in ('AD') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('AD') then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category in ('AD') then  Balance_QTYKG else 0 end) end)  as RecAdjProFATPER  ,sum(case when InOut='I' and In_Category in ('AD') then Balance_SNF else 0 end) as RecAdjProSNF  ,(case when sum(case when InOut='I' and In_Category in ('AD') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('AD') then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category in ('AD') then  Balance_QTYKG else 0 end) end)  as RecAdjProSNFPER " & _
                    ",sum(case when InOut='I' and In_Category in ('AD') then ACTBalance_FAT else 0 end) as ACTRecAdjProFAT  ,(case when sum(case when InOut='I' and In_Category in ('AD') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('AD') then ACTBalance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category in ('AD') then  Balance_QTYKG else 0 end) end)  as ACTRecAdjProFATPER  ,sum(case when InOut='I' and In_Category in ('AD') then ACTBalance_SNF else 0 end) as ACTRecAdjProSNF  ,(case when sum(case when InOut='I' and In_Category in ('AD') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('AD') then  ACTBalance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category in ('AD') then  Balance_QTYKG else 0 end) end)  as ACTRecAdjProSNFPER " & _
                    ",sum(case when InOut='I' and In_Category in ('AD') then STDBalance_FAT else 0 end) as STDRecAdjProFAT  ,(case when sum(case when InOut='I' and In_Category in ('AD') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('AD') then STDBalance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category in ('AD') then  Balance_QTYKG else 0 end) end)  as STDRecAdjProFATPER  ,sum(case when InOut='I' and In_Category in ('AD') then STDBalance_SNF else 0 end) as STDRecAdjProSNF  ,(case when sum(case when InOut='I' and In_Category in ('AD') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('AD') then  STDBalance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category in ('AD') then  Balance_QTYKG else 0 end) end)  as STDRecAdjProSNFPER " & _
                    " ,sum(case when InOut='I' and In_Category in ('AD') then Cost else 0 end) as RecAdjProCost ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end) as RecOthQty  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Rate else 0 end) as RecOthRate  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Balance_FAT else 0 end) as RecOthFAT  ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as RecOthFATPER  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Balance_SNF else 0 end) as RecOthSNF ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as RecOthSNFPER " & _
                    ",sum(case when InOut='I' and In_Category not in ('AD','PU') then ACTBalance_FAT else 0 end) as ACTRecOthFAT  ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  ACTBalance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as ACTRecOthFATPER  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then ACTBalance_SNF else 0 end) as ACTRecOthSNF ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  ACTBalance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as ACTRecOthSNFPER " & _
                    ",sum(case when InOut='I' and In_Category not in ('AD','PU') then STDBalance_FAT else 0 end) as STDRecOthFAT  ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  STDBalance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as STDRecOthFATPER  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then STDBalance_SNF else 0 end) as STDRecOthSNF ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  STDBalance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as STDRecOthSNFPER " & _
                    " ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Cost else 0 end) as RecOthCost ,sum(case when InOut='I' then Stock_Qty else 0 end) as RecQty  ,sum(case when InOut='I' then Rate else 0 end) as RecRate  ,sum(case when InOut='I' then Balance_FAT else 0 end) as RecFAT,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as RecFATPER  ,sum(case when InOut='I' then Balance_SNF else 0 end) as RecSNF  ,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as RecSNFPER " & _
                    ",sum(case when InOut='I' then ACTBalance_FAT else 0 end) as ACTRecFAT,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  ACTBalance_FAT else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as ACTRecFATPER  ,sum(case when InOut='I' then ACTBalance_SNF else 0 end) as ACTRecSNF  ,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  ACTBalance_SNF else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as ACTRecSNFPER " & _
                    ",sum(case when InOut='I' then STDBalance_FAT else 0 end) as STDRecFAT,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  STDBalance_FAT else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as STDRecFATPER  ,sum(case when InOut='I' then STDBalance_SNF else 0 end) as STDRecSNF  ,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  STDBalance_SNF else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as STDRecSNFPER " & _
                    " ,sum(case when InOut='I' then Cost else 0 end) as RecCost,sum(case when InOut='O' and Out_Category in ('SA') then -1*Stock_Qty else 0 end) as IssSaleQty  ,sum(case when InOut='O' and Out_Category in ('SA') then Rate else 0 end) as IssSaleRate  ,sum(case when InOut='O' and Out_Category in ('SA') then -1*Balance_FAT else 0 end) as IssSaleFAT  ,(case when sum(case when InOut='O' and Out_Category in ('SA') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('SA') then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category in ('SA') then  Balance_QTYKG else 0 end) end)  as IssSaleFATPER  ,sum(case when InOut='O' and Out_Category in ('SA') then -1*Balance_SNF else 0 end) as IssSaleSNF  ,(case when sum(case when InOut='O' and Out_Category in ('SA') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('SA') then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category in ('SA') then  Balance_QTYKG else 0 end) end)  as IssSaleSNFPER " & _
                    ",sum(case when InOut='O' and Out_Category in ('SA') then -1 * ACTBalance_FAT else 0 end) as ACTIssSaleFAT  ,(case when sum(case when InOut='O' and Out_Category in ('SA') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('SA') then  ACTBalance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category in ('SA') then  Balance_QTYKG else 0 end) end)  as ACTIssSaleFATPER  ,sum(case when InOut='O' and Out_Category in ('SA') then -1 * ACTBalance_SNF else 0 end) as ACTIssSaleSNF  ,(case when sum(case when InOut='O' and Out_Category in ('SA') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('SA') then  ACTBalance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category in ('SA') then  Balance_QTYKG else 0 end) end)  as ACTIssSaleSNFPER " & _
                    ",sum(case when InOut='O' and Out_Category in ('SA') then -1 * STDBalance_FAT else 0 end) as STDIssSaleFAT  ,(case when sum(case when InOut='O' and Out_Category in ('SA') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('SA') then  STDBalance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category in ('SA') then  Balance_QTYKG else 0 end) end)  as STDIssSaleFATPER  ,sum(case when InOut='O' and Out_Category in ('SA') then -1 * STDBalance_SNF else 0 end) as STDIssSaleSNF  ,(case when sum(case when InOut='O' and Out_Category in ('SA') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('SA') then  STDBalance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category in ('SA') then  Balance_QTYKG else 0 end) end)  as STDIssSaleSNFPER " & _
                    " ,sum(case when InOut='O' and Out_Category in ('SA') then -1*Cost else 0 end) as IssSaleCost  ,sum(case when InOut='O' and Out_Category in ('IS') then -1*Stock_Qty else 0 end) as IssIssAdjQty  ,sum(case when InOut='O' and Out_Category in ('IS') then Rate else 0 end) as IssIssAdjRate  ,sum(case when InOut='O' and Out_Category in ('IS') then -1*Balance_FAT else 0 end) as IssIssAdjFAT  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as IssIssAdjFATPER  ,sum(case when InOut='O' and Out_Category in ('IS') then -1*Balance_SNF else 0 end) as IssIssAdjSNF  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as IssIssAdjSNFPER " & _
                    ",sum(case when InOut='O' and Out_Category in ('IS') then -1* ACTBalance_FAT else 0 end) as ACTIssIssAdjFAT  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  ACTBalance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as ACTIssIssAdjFATPER  ,sum(case when InOut='O' and Out_Category in ('IS') then -1* ACTBalance_SNF else 0 end) as ACTIssIssAdjSNF  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  ACTBalance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as ACTIssIssAdjSNFPER " & _
                    ",sum(case when InOut='O' and Out_Category in ('IS') then -1* STDBalance_FAT else 0 end) as STDIssIssAdjFAT  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  STDBalance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as STDIssIssAdjFATPER  ,sum(case when InOut='O' and Out_Category in ('IS') then -1* STDBalance_SNF else 0 end) as STDIssIssAdjSNF  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  STDBalance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as STDIssIssAdjSNFPER " & _
                    " ,sum(case when InOut='O' and Out_Category in ('IS') then -1*Cost else 0 end) as IssIssAdjCost ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1*Stock_Qty else 0 end) as IssOthQty  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then Rate else 0 end) as IssOthRate  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1*Balance_FAT else 0 end) as IssOthFAT  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as IssOthFATPER  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1*Balance_SNF else 0 end) as IssOthSNF  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as IssOthSNFPER " & _
                    ",sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1* ACTBalance_FAT else 0 end) as ACTIssOthFAT  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then ACTBalance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as ACTIssOthFATPER  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1* ACTBalance_SNF else 0 end) as ACTIssOthSNF  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then  ACTBalance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as ACTIssOthSNFPER " & _
                    ",sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1* STDBalance_FAT else 0 end) as STDIssOthFAT  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then STDBalance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as STDIssOthFATPER  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1* STDBalance_SNF else 0 end) as STDIssOthSNF  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then  STDBalance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as STDIssOthSNFPER " & _
                    " ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1*Cost else 0 end) as IssOthCost ,sum(case when InOut='O' then -1*Stock_Qty else 0 end) as IssQty  ,sum(case when InOut='O' then Rate else 0 end) as IssRate  ,sum(case when InOut='O' then -1*Balance_FAT else 0 end) as IssFAT  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as IssFATPER ,sum(case when InOut='O' then -1*Balance_SNF else 0 end) as IssSNF  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as IssSNFPER " & _
                    ",sum(case when InOut='O' then -1* ACTBalance_FAT else 0 end) as ACTIssFAT  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  ACTBalance_FAT else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as ACTIssFATPER ,sum(case when InOut='O' then -1* ACTBalance_SNF else 0 end) as ACTIssSNF  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  ACTBalance_SNF else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as ACTIssSNFPER " & _
                    ",sum(case when InOut='O' then -1* STDBalance_FAT else 0 end) as STDIssFAT  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  STDBalance_FAT else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as STDIssFATPER ,sum(case when InOut='O' then -1* STDBalance_SNF else 0 end) as STDIssSNF  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  STDBalance_SNF else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as STDIssSNFPER " & _
                    " ,sum(case when InOut='O' then -1*Cost else 0 end) as IssCost ,SUM(sum(Stock_Qty)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code,Punching_Date) as CLQty   ,SUM(sum(Cost)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLCost ,SUM(sum(isnull(Balance_QTYKG,0))) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Punching_Date) as CLBalance_QTYKG  ,SUM(sum(Balance_FAT)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Punching_Date) as CLBalance_FAT ,SUM(sum(Balance_SNF)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code,Punching_Date) as CLBalance_SNF " & _
                    ",SUM(sum(ACTBalance_FAT)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Punching_Date) as ACTCLBalance_FAT ,SUM(sum(ACTBalance_SNF)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code,Punching_Date) as ACTCLBalance_SNF " & _
                    ",SUM(sum(STDBalance_FAT)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Punching_Date) as STDCLBalance_FAT ,SUM(sum(STDBalance_SNF)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code,Punching_Date) as STDCLBalance_SNF " & _
                    "  from ("
                strFinalQry += "select 0 as Trans_Id,'Opening' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,null as Punching_Date,'' as InOutView, '' as InOut,xxx.Main_Location_Code,xxx.MainLocationDesc,xxx.Location_Code ,'' as [Loc Desp],'' as [LocAddress],'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",xxx.Item_Code as Item_Code ,max(xxx.Item_Desc) as Item_Desc,'' as Item_Category_Struct_Code,max(xxx.Stock_UOM) as Stock_UOM,'' as itf_code ,sum( Stock_Qty) as Stock_Qty,sum( QtyKG) as Balance_QTYKG, case when sum(convert(decimal(18,3),Stock_Qty))=0 then 0 else sum(convert(decimal(18,3),Cost))/sum(convert(decimal(18,3),Stock_Qty)) end as Rate,sum(Cost) as Cost,sum( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end)) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ))) as Balance_SNF"

                ''===========================Monika 28/03/2017============================
                strFinalQry += ",sum(ACT_FAT_KG) as ACTBalance_FAT,sum(ACT_SNF_KG) as ACTBalance_SNF "
                strFinalQry += ",sum(STD_FAT_KG) as STDBalance_FAT,sum(STD_SNF_KG) as STDBalance_SNF "
                ''=========================================================================================

                strFinalQry += ",'' as In_Category,'' as Out_Category"
                If objFilter.MRPWise Then
                    strFinalQry += ",Max(MRP) as MRP "
                End If
                strFinalQry += " from (" + Qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type='Opening' group by xxx.Item_Code,xxx.Location_Code,xxx.main_location_code,xxx.mainlocationdesc " + Environment.NewLine
                strFinalQry += " union all "
                strFinalQry += " select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,cast(Punching_Date as date) as Punching_Date,InOutView, InOut,Main_Location_Code,MainLocationDesc,Location_Code,[Loc Desp], [LocAddress],SourceCode,SourceName,SourceType ,Item_Type," & _
                               " Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Code ,Item_Desc,Item_Category_Struct_Code," & _
                               " Stock_UOM,itf_code ,( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG," & _
                               " convert(decimal(18,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1 else -1 end) as Cost," & _
                               " ( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT, " & _
                               " ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end) as Balance_SNF "
                ''===========================Monika 28/03/2017============================
                strFinalQry += ",( ACT_FAT_KG * (case when InOut='I' then 1 else -1 end)) as ACTBalance_FAT, (ACT_SNF_KG * (case when InOut='I' then 1 else -1 end)) as ACTBalance_SNF "
                strFinalQry += ",( STD_FAT_KG * (case when InOut='I' then 1 else -1 end)) as STDBalance_FAT, (STD_SNF_KG * (case when InOut='I' then 1 else -1 end)) as STDBalance_SNF "
                ''=========================================================================================

                strFinalQry += ",In_Category,Out_Category  "
                If objFilter.MRPWise Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from (" + Qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Trans_Type<>'Opening' " + Environment.NewLine

                strFinalQry += " union  all "
                strFinalQry += "select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,InOutView,InOut,Main_Location_Code,MainLocationDesc,Location_Code,[Loc Desp],[LocAddress],SourceCode,SourceName,SourceType ,Item_Type," & _
               " Item_Type_Name,Item_Group,Group_Description," + strCodeColumnNull + "," + strCodeDescColumnNull + ",Items.Item_Code,Item_Desc, Item_Category_Struct_Code, " & _
               " Items.Stock_UOM ,itf_code ,Stock_Qty,Balance_QTYKG,Rate,Cost,Balance_FAT, Balance_SNF,ACTBalance_FAT,ACTBalance_SNF,STDBalance_FAT,STDBalance_SNF ,In_Category,Out_Category "
                If objFilter.MRPWise Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from ( SELECT 0 as Trans_Id,null as Trans_Type,null as Trans_Type_Name, null as Source_Doc_No, thedate as Punching_Date,'In' as InOutView,'I' as InOut,case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as main_location_code,case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Desc else (select max(MainLoc1.location_desc) from tspl_location_master as MainLoc1 where MainLoc1.location_code=TSPL_LOCATION_MASTER.Main_Location_Code) end as MainLocationDesc,TSPL_LOCATION_MASTER.Location_Code as Location_Code,TSPL_LOCATION_MASTER.Location_Desc as [Loc Desp],null as [LocAddress],null as SourceCode,null as SourceName,null as SourceType ,TSPL_ITEM_MASTER.Item_Type,null as Item_Type_Name,null as Item_Group,null as Group_Description," + strCodeColumnNull + "," + strCodeDescColumnNull + ",TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,null as Item_Category_Struct_Code,TSPL_ITEM_UOM_DETAIL.UOM_Code as Stock_UOM ,null as  itf_code ,0 as Stock_Qty,0 as Balance_QTYKG,0 as Rate,0 as Cost,0 as Balance_FAT, 0 as Balance_SNF,0 as ACTBalance_FAT, 0 as ACTBalance_SNF,0 as STDBalance_FAT, 0 as STDBalance_SNF ,null as In_Category,null as Out_Category,TSPL_ITEM_MASTER.Product_Type  "
                If objFilter.MRPWise Then
                    strFinalQry += ",0 as MRP "
                End If
                strFinalQry += " FROM ExplodeDates('" + clsCommon.GetPrintDate(objFilter.From_Date, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(objFilter.To_Date, "dd/MMM/yyyy") + "') as d, TSPL_ITEM_MASTER,TSPL_LOCATION_MASTER,TSPL_ITEM_UOM_DETAIL  where 2=2 "
                If objFilter.arrItem IsNot Nothing AndAlso objFilter.arrItem.Count > 0 Then
                    strFinalQry += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(objFilter.arrItem) + ") "
                End If

                If objFilter.SelectLocation Then
                    Dim IsApplicable As Boolean = False
                    For ii As Integer = 0 To objFilter.arrLocation.Count - 1
                        If clsCommon.myCBool(objFilter.arrLocation(ii).Sel) Then
                            LocationFirstTime += 1
                            If LocationFirstTime = 1 Then
                                LocationAddress = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress] from TSPL_LOCATION_MASTER where Location_Code= '" & clsCommon.myCstr(objFilter.arrLocation(ii).Code) & "'")
                            End If
                            If IsApplicable Then
                                strWhrCatg += " Or "
                            End If
                            strWhrCatg += " ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(objFilter.arrLocation(ii).Code) + "') "
                            IsApplicable = True
                            Dim arr As Dictionary(Of String, Object) = objFilter.arrLocation(ii).arrOut
                            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                                strWhrCatg += " and Location_Code in ("
                                Dim isFirstTime As Boolean = True
                                For Each strInn As String In arr.Keys
                                    If Not isFirstTime Then
                                        strWhrCatg += ","
                                    End If
                                    strWhrCatg += "'" + strInn + "'"
                                    isFirstTime = False
                                Next
                                strWhrCatg += ")"
                            End If
                        End If
                    Next
                    If Not IsApplicable Then
                        Throw New Exception("Please select at least one location")
                    End If
                    strFinalQry += " and (" + strWhrCatg + ")"
                End If

                strFinalQry += "  and TSPL_ITEM_UOM_DETAIL.Stocking_Unit='Y' and TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code) Items" & _
                " left join (select TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as Fat_Per  from TSPL_ITEM_QC_PARAMETER_MASTER " & _
                " left join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where TSPL_PARAMETER_MASTER.Type='FAT') Fat on Items.Item_Code=Fat.Item_Code " & _
                " left join  (select TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as SNF_Per  from TSPL_ITEM_QC_PARAMETER_MASTER " & _
                " left join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where TSPL_PARAMETER_MASTER.Type='SNF') as SNF on Items.Item_Code=SNF.Item_Code where 2=2 "
                'If isDataLoad AndAlso SkipCheckFatAndSNF Then
                '    ''never want to check fat% and snf% cond. when open from double click in production(26/05/2014)
                'Else

                If objFilter.FatSNF Then
                    strFinalQry += " and (coalesce(Fat.Fat_Per,0)<>0  or  coalesce(SNF.SNF_Per,0)<>0 or Items.Product_Type='MI') "
                Else
                    strFinalQry += " and (coalesce(Fat.Fat_Per,0)=0  and  coalesce(SNF.SNF_Per,0)=0 and Items.Product_Type<>'MI') "
                End If
                'End If
                strFinalQry += " )xxxxxx Group by  Item_Code,Location_Code,main_location_code,Punching_Date )xxxxxxx where Punching_Date is not null  "



                If objFilter.isPrintCrystal = 0 Then
                    strFinalQry += " Order by  Punching_Date,Location_Code "
                End If

            ElseIf clsCommon.CompairString(clsCommon.myCstr(objFilter.ReportType), "Transaction Wise") = CompairStringResult.Equal Then
                Dim strTransCatg As String = ""
                Dim strTransName As String = ""
                'WHERE Code='PROD_ENTRY'
                Dim TransQry As String = "select code,Name,case when inouttype='In' then 'I' else 'O' end as InOutType,In_Category,Out_Category,isnull(Sequence,500) as Sequence,Type as Typed from TSPL_INVENTORY_SOURCE_CODE  order by Sequence asc,inouttype "
                Dim TransResult As DataTable = clsDBFuncationality.GetDataTable(TransQry)
                If TransResult IsNot Nothing AndAlso TransResult.Rows.Count > 0 Then
                    For ii As Integer = 0 To TransResult.Rows.Count - 1
                        If ii <> 0 Then
                            strTransCatg += ","
                            strTransName += ","
                        End If
                        strTransCatg += " sum(case when InOut='" + TransResult.Rows(ii)("InOutType") + "' and code in ('" + TransResult.Rows(ii)("Code") + "') then Stock_Qty else 0 end) as '" + TransResult.Rows(ii)("Name") + "'"
                        strTransCatg += " ,MAX(isnull(convert(decimal(18,2),(CASE when Balance_QTYKG=0 then 0 else ([Balance_FAT]*100/[Balance_QTYKG]) end)),0)) as '" + TransResult.Rows(ii)("Name") + " Fat_%', sum(case when Trans_Type<>'Opening' then convert(decimal(18,2),Balance_FAT) else 0 end) as [" + TransResult.Rows(ii)("Name") + "_Fat_KG]"
                        strTransCatg += " ,max(isnull(convert(decimal(18,2),(CASE when Balance_QTYKG=0 then 0 else ([Balance_SNF]*100/[Balance_QTYKG]) end)),0)) as '" + TransResult.Rows(ii)("Name") + " SNF_%', sum(case when Trans_Type<>'Opening' then convert(decimal(18,2),Balance_SNF) else 0 end) as [" + TransResult.Rows(ii)("Name") + "_SNF_KG]"
                        ''============================Monika 29/03/2017====================================================================
                        strTransCatg += " ,MAX(isnull(convert(decimal(18,2),(CASE when Balance_QTYKG=0 then 0 else ([ACTBalance_FAT]*100/[Balance_QTYKG]) end)),0)) as 'ACT" + TransResult.Rows(ii)("Name") + " Fat_%', sum(case when Trans_Type<>'Opening' then convert(decimal(18,2),ACTBalance_FAT) else 0 end)  as [ACT" + TransResult.Rows(ii)("Name") + "_Fat_KG] " & _
                                            " ,max(isnull(convert(decimal(18,2),(CASE when Balance_QTYKG=0 then 0 else ([ACTBalance_SNF]*100/[Balance_QTYKG]) end)),0)) as 'ACT" + TransResult.Rows(ii)("Name") + " SNF_%', sum(case when Trans_Type<>'Opening' then convert(decimal(18,2),ACTBalance_SNF) else 0 end) as [ACT" + TransResult.Rows(ii)("Name") + "_SNF_KG]"

                        strTransCatg += " ,MAX(isnull(convert(decimal(18,2),(CASE when Balance_QTYKG=0 then 0 else ([STDBalance_FAT]*100/[Balance_QTYKG]) end)),0)) as 'STD" + TransResult.Rows(ii)("Name") + " Fat_%', sum(case when Trans_Type<>'Opening' then convert(decimal(18,2),STDBalance_FAT) else 0 end)  as [STD" + TransResult.Rows(ii)("Name") + "_Fat_KG] " & _
                                        " ,max(isnull(convert(decimal(18,2),(CASE when Balance_QTYKG=0 then 0 else ([STDBalance_SNF]*100/[Balance_QTYKG]) end)),0)) as 'STD" + TransResult.Rows(ii)("Name") + " SNF_%', sum(case when Trans_Type<>'Opening' then convert(decimal(18,2),STDBalance_SNF) else 0 end) as  [STD" + TransResult.Rows(ii)("Name") + "_SNF_KG]"
                        ''=================================================================================================================

                        strTransName += "[" + TransResult.Rows(ii)("Name") + "]"
                        If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
                            strTransName += " ,[ACT" + TransResult.Rows(ii)("Name") + " Fat_%] as [" + TransResult.Rows(ii)("Name") + " Fat_%], [ACT" + TransResult.Rows(ii)("Name") + "_Fat_KG] as [" + TransResult.Rows(ii)("Name") + "_Fat_KG] " & _
                                            " ,[ACT" + TransResult.Rows(ii)("Name") + " SNF_%] as [" + TransResult.Rows(ii)("Name") + " SNF_%], [ACT" + TransResult.Rows(ii)("Name") + "_SNF_KG] as [" + TransResult.Rows(ii)("Name") + "_SNF_KG]"
                        Else
                            strTransName += " ,[" + TransResult.Rows(ii)("Name") + " Fat_%], [" + TransResult.Rows(ii)("Name") + "_Fat_KG]"
                            strTransName += " ,[" + TransResult.Rows(ii)("Name") + " SNF_%], [" + TransResult.Rows(ii)("Name") + "_SNF_KG]"
                        End If

                        ''============================Monika 29/03/2017====================================================================
                        If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                            strTransName += " ,[ACT" + TransResult.Rows(ii)("Name") + " Fat_%] as [QC " + TransResult.Rows(ii)("Name") + " Fat_%], [ACT" + TransResult.Rows(ii)("Name") + "_Fat_KG] as [QC " + TransResult.Rows(ii)("Name") + "_Fat_KG] " & _
                                            " ,[ACT" + TransResult.Rows(ii)("Name") + " SNF_%] as [QC " + TransResult.Rows(ii)("Name") + " SNF_%], [ACT" + TransResult.Rows(ii)("Name") + "_SNF_KG] as [QC " + TransResult.Rows(ii)("Name") + "_SNF_KG]"
                        End If

                        If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                            strTransName += " ,[STD" + TransResult.Rows(ii)("Name") + " Fat_%] as [STD " + TransResult.Rows(ii)("Name") + " Fat_%], [STD" + TransResult.Rows(ii)("Name") + "_Fat_KG] as [STD " + TransResult.Rows(ii)("Name") + "_Fat_KG] " & _
                                            " ,[STD" + TransResult.Rows(ii)("Name") + " SNF_%] as [STD " + TransResult.Rows(ii)("Name") + " SNF_%], [STD" + TransResult.Rows(ii)("Name") + "_SNF_KG] as [STD " + TransResult.Rows(ii)("Name") + "_SNF_KG]"
                        End If
                        ''=================================================================================================================
                    Next
                End If
                strWhrCatg = ""
                Dim IsApplicable As Boolean = False
                For ii As Integer = 0 To objFilter.arrLocation.Count - 1
                    If clsCommon.myCBool(objFilter.arrLocation(ii).Sel) Then
                        If IsApplicable Then
                            strWhrCatg += " , "
                        End If
                        strWhrCatg += "'" + clsCommon.myCstr(objFilter.arrLocation(ii).Code) + "'"
                    End If
                Next
                strFinalQry = "select row_number() over (order by Item_Code) as SNO,xxxxxxx.Location_Code as [Location Code],Loc.Location_Desc as [Location Desc],Main_Location_Code as [Main Location]  ,Item_Code as [Item Code] ,Item_Desc as [Item Description],Stock_UOM as UOM, " + strCodeColumn + "," + strCodeDescColumn + ", " & _
                    " (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)) as OPQty, case when (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))=0 then 0 else  ((isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0))/((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)))) end as OPRate "
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
                    strFinalQry += ",convert(numeric(18,3),(isnull(ACTCLBalance_FAT,0)-isnull(ACTRecFAT,0)+isnull(ACTIssFAT,0))) as OPFAT,isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((ACTCLBalance_FAT + ACTBalance_FAT)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as OPFATPER,convert(numeric(18,3),(isnull(ACTCLBalance_SNF,0)-isnull(ACTRecSNF,0)+isnull(ACTIssSNF,0))) as OPSNF,isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((ACTCLBalance_SNF+ACTBalance_SNF)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as OPSNFPER"
                Else
                    strFinalQry += ",convert(numeric(18,3), (isnull(CLBalance_FAT,0)-isnull(RecFAT,0)+isnull(IssFAT,0))) as OPFAT,isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_FAT+Balance_FAT)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as OPFATPER,convert(numeric(18,3),(isnull(CLBalance_SNF,0)-isnull(RecSNF,0)+isnull(IssSNF,0))) as OPSNF,isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_SNF+Balance_SNF)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as OPSNFPER "
                End If
                ''============================Monika 29/03/2017====================================================================
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += ",convert(numeric(18,3),(isnull(ACTCLBalance_FAT,0)-isnull(ACTRecFAT,0)+isnull(ACTIssFAT,0))) as ACTOPFAT,isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((ACTCLBalance_FAT + ACTBalance_FAT)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as ACTOPFATPER,convert(numeric(18,3),(isnull(ACTCLBalance_SNF,0)-isnull(ACTRecSNF,0)+isnull(ACTIssSNF,0))) as ACTOPSNF,isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((ACTCLBalance_SNF+ACTBalance_SNF)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as ACTOPSNFPER "
                End If
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += ",convert(numeric(18,3),(isnull(STDCLBalance_FAT,0)-isnull(STDRecFAT,0)+isnull(STDIssFAT,0))) as STDOPFAT,isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((STDCLBalance_FAT + STDBalance_FAT)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as STDOPFATPER,convert(numeric(18,3),(isnull(STDCLBalance_SNF,0)-isnull(STDRecSNF,0)+isnull(STDIssSNF,0))) as STDOPSNF,isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((STDCLBalance_SNF+STDBalance_SNF)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as STDOPSNFPER "
                End If
                ''=================================================================================================================

                strFinalQry += ",convert(numeric(18,2),(isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0))) as OPCost," + strTransName + " ,CLQty ,case when isnull(CLQty,0)=0 then 0 else CLCost/CLQty end as CLRate "
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
                    strFinalQry += ", convert(numeric(18,3),ACTCLBalance_FAT) as CLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else (ACTCLBalance_FAT*100/CLBalance_QTYKG) end),0) as CLFATPER, convert(numeric(18,3),ACTCLBalance_SNF) as CLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else (ACTCLBalance_SNF*100/CLBalance_QTYKG) end),0) as CLSNFPER "
                Else
                    strFinalQry += ", convert(numeric(18,3),CLBalance_FAT) as CLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_FAT*100/CLBalance_QTYKG) end),0) as CLFATPER, convert(numeric(18,3),CLBalance_SNF) as CLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_SNF*100/CLBalance_QTYKG) end),0) as CLSNFPER"
                End If
                ''============================Monika 29/03/2017====================================================================
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += ", convert(numeric(18,3),ACTCLBalance_FAT) as ACTCLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else (ACTCLBalance_FAT*100/CLBalance_QTYKG) end),0) as ACTCLFATPER, convert(numeric(18,3),ACTCLBalance_SNF) as ACTCLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else (ACTCLBalance_SNF*100/CLBalance_QTYKG) end),0) as ACTCLSNFPER,(convert(numeric(18,3),CLBalance_FAT)-convert(numeric(18,3),ACTCLBalance_FAT)) as [Diff Fat KG],(convert(numeric(18,3),CLBalance_SNF)-convert(numeric(18,3),ACTCLBalance_SNF)) as [Diff SNF KG] "
                End If
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += ", convert(numeric(18,3),STDCLBalance_FAT) as STDCLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else (STDCLBalance_FAT*100/CLBalance_QTYKG) end),0) as STDCLFATPER, convert(numeric(18,3),STDCLBalance_SNF) as STDCLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else (STDCLBalance_SNF*100/CLBalance_QTYKG) end),0) as STDCLSNFPER "
                End If
                ''=================================================================================================================

                strFinalQry += ", CLCost from ( "
                strFinalQry += "select  Location_Code, Item_Code," + strCodeColumnMax + "," + strCodeDescColumnMax + " ,max(Item_Desc) as Item_Desc, max(Stock_UOM) as Stock_UOM, sum(Balance_FAT) as Balance_FAT,sum(Balance_SNF) as Balance_SNF, sum(ACTBalance_FAT) as ACTBalance_FAT,sum(ACTBalance_SNF) as ACTBalance_SNF," & _
                    " sum(STDBalance_FAT) as STDBalance_FAT,sum(STDBalance_SNF) as STDBalance_SNF ,sum(case when InOut='I' and Trans_Type<>'Opening' then Stock_Qty else 0 end) as RecQty ," & _
                    " SUM(sum(Stock_Qty)) OVER (Partition BY Location_Code,Item_Code ORDER BY Location_Code,Item_Code) as CLQty,sum(case when InOut='O' and Trans_Type<>'Opening' then -1*Stock_Qty else 0 end) as IssQty," & _
                    " SUM(sum(Cost)) OVER (Partition BY Location_Code,Item_Code ORDER BY Location_Code, Item_Code) as CLCost,(case when sum(case when InOut='I' and Trans_Type<>'Opening' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I'and Trans_Type<>'Opening' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and Trans_Type<>'Opening' then  Balance_QTYKG else 0 end) end)  as RecSNFPER " & _
                    ",(case when sum(case when InOut='I' and Trans_Type<>'Opening' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type<>'Opening' then  ACTBalance_SNF else 0 end)*100/sum(case when InOut='I' and Trans_Type<>'Opening' then  Balance_QTYKG else 0 end) end)  as ACTRecSNFPER " & _
                    ",(case when sum(case when InOut='I' and Trans_Type<>'Opening' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type<>'Opening' then  STDBalance_SNF else 0 end)*100/sum(case when InOut='I' and Trans_Type<>'Opening' then  Balance_QTYKG else 0 end) end)  as STDRecSNFPER " & _
                    " ,sum(case when InOut='I' and Trans_Type<>'Opening' then Cost else 0 end) as RecCost,sum(case when InOut='O' and Trans_Type<>'Opening' then -1*Cost else 0 end) as IssCost,SUM(sum(Balance_FAT)) OVER (Partition BY Location_Code,Item_Code ORDER BY Location_Code,Item_Code) as CLBalance_FAT ,SUM(sum(Balance_SNF)) OVER (Partition BY Location_Code,Item_Code ORDER BY Location_Code,Item_Code) as CLBalance_SNF ,sum(case when InOut='O' and Trans_Type<>'Opening' then -1*Balance_FAT else 0 end) as IssFAT  ,(case when sum(case when InOut='O' and Trans_Type<>'Opening' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type<>'Opening' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Trans_Type<>'Opening' then  Balance_QTYKG else 0 end) end)  as IssFATPER ,sum(case when InOut='O' and Trans_Type<>'Opening' then -1*Balance_SNF else 0 end) as IssSNF  ,(case when sum(case when InOut='O' and Trans_Type<>'Opening' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type<>'Opening' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Trans_Type<>'Opening' then  Balance_QTYKG else 0 end) end)  as IssSNFPER ,sum(case when InOut='I' and Trans_Type<>'Opening' then Balance_FAT else 0 end) as RecFAT ,sum(case when InOut='I' and Trans_Type<>'Opening' then Balance_SNF else 0 end) as RecSNF " & _
                    ",SUM(sum(ACTBalance_FAT)) OVER (Partition BY Location_Code,Item_Code ORDER BY Location_Code,Item_Code) as ACTCLBalance_FAT ,SUM(sum(ACTBalance_SNF)) OVER (Partition BY Location_Code,Item_Code ORDER BY Location_Code,Item_Code) as ACTCLBalance_SNF ,sum(case when InOut='O' and Trans_Type<>'Opening' then -1* ACTBalance_FAT else 0 end) as ACTIssFAT  ,(case when sum(case when InOut='O' and Trans_Type<>'Opening' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type<>'Opening' then ACTBalance_FAT else 0 end)*100/sum(case when InOut='O' and Trans_Type<>'Opening' then  Balance_QTYKG else 0 end) end)  as ACTIssFATPER ,sum(case when InOut='O' and Trans_Type<>'Opening' then -1* ACTBalance_SNF else 0 end) as ACTIssSNF  ,(case when sum(case when InOut='O' and Trans_Type<>'Opening' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type<>'Opening' then  ACTBalance_SNF else 0 end)*100/sum(case when InOut='O' and Trans_Type<>'Opening' then  Balance_QTYKG else 0 end) end)  as ACTIssSNFPER ,sum(case when InOut='I' and Trans_Type<>'Opening' then ACTBalance_FAT else 0 end) as ACTRecFAT ,sum(case when InOut='I' and Trans_Type<>'Opening' then ACTBalance_SNF else 0 end) as ACTRecSNF " & _
                    ",SUM(sum(STDBalance_FAT)) OVER (Partition BY Location_Code,Item_Code ORDER BY Location_Code,Item_Code) as STDCLBalance_FAT ,SUM(sum(STDBalance_SNF)) OVER (Partition BY Location_Code,Item_Code ORDER BY Location_Code,Item_Code) as STDCLBalance_SNF ,sum(case when InOut='O' and Trans_Type<>'Opening' then -1* STDBalance_FAT else 0 end) as STDIssFAT  ,(case when sum(case when InOut='O' and Trans_Type<>'Opening' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type<>'Opening' then STDBalance_FAT else 0 end)*100/sum(case when InOut='O' and Trans_Type<>'Opening' then  Balance_QTYKG else 0 end) end)  as STDIssFATPER ,sum(case when InOut='O' and Trans_Type<>'Opening' then -1* STDBalance_SNF else 0 end) as STDIssSNF  ,(case when sum(case when InOut='O' and Trans_Type<>'Opening' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type<>'Opening' then  STDBalance_SNF else 0 end)*100/sum(case when InOut='O' and Trans_Type<>'Opening' then  Balance_QTYKG else 0 end) end)  as STDIssSNFPER ,sum(case when InOut='I' and Trans_Type<>'Opening' then STDBalance_FAT else 0 end) as STDRecFAT ,sum(case when InOut='I' and Trans_Type<>'Opening' then STDBalance_SNF else 0 end) as STDRecSNF " & _
                    ",SUM(sum(isnull(Balance_QTYKG,0))) OVER (Partition BY Location_Code,Item_Code ORDER BY Location_Code,Item_Code) as CLBalance_QTYKG , " + strTransCatg + "  ,sum(isnull(Balance_QTYKG,0)) as Balance_QTYKG  from ("

                strFinalQry += "select '' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,null as Location_Code,'' as InOutView, '' as InOut,'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",xxx.Item_Code as Item_Code ,max(xxx.Item_Desc) as Item_Desc,'' as Item_Category_Struct_Code,max(xxx.Stock_UOM) as Stock_UOM,'' as itf_code ,sum( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG, case when sum(convert(decimal(18,3),Stock_Qty* case when InOut='I' then 1 else -1 end))=0 then 0 else sum(convert(decimal(18,3),Cost* case when InOut='I' then 1 else -1 end))/sum(convert(decimal(18,3),Stock_Qty* case when InOut='I' then 1 else -1 end)) end as Rate,sum(Cost * case when InOut='I' then 1 else -1 end) as Cost,sum( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end)) as Balance_SNF " & _
                    ",sum( ACT_FAT_KG * case when InOut='I' then 1 else -1 end) as ACTBalance_FAT,sum(( ACT_SNF_KG * case when InOut='I' then 1 else -1 end)) as ACTBalance_SNF " & _
                    ",sum( STD_FAT_KG * case when InOut='I' then 1 else -1 end) as STDBalance_FAT,sum(( STD_SNF_KG * case when InOut='I' then 1 else -1 end)) as STDBalance_SNF " & _
                    ",'' as In_Category,'' as Out_Category,'' as Code "
                If objFilter.MRPWise Then
                    strFinalQry += ",Max(MRP) as MRP "
                End If
                strFinalQry += " from (" + Qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' group by xxx.Item_Code " + Environment.NewLine
                strFinalQry += " union all "
                strFinalQry += " select Trans_Type,Trans_Type_Name,Source_Doc_No,Location_Code,InOutView, InOut,SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG,convert(decimal(18,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1 else -1 end) as Cost,( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT, ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end) as Balance_SNF " & _
                    ",( ACT_FAT_KG * case when InOut='I' then 1 else -1 end) as ACTBalance_FAT, ( ACT_SNF_KG * case when InOut='I' then 1 else -1 end) as ACTBalance_SNF " & _
                    ",( STD_FAT_KG * case when InOut='I' then 1 else -1 end) as STDBalance_FAT, ( STD_SNF_KG * case when InOut='I' then 1 else -1 end) as STDBalance_SNF " & _
                    " ,In_Category,Out_Category,code  "
                If objFilter.MRPWise Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from (" + Qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' " + Environment.NewLine

                'strFinalQry += " union  all "
                'strFinalQry += "SELECT 0 as Trans_Id,null as Trans_Type,null as Trans_Type_Name, null as Source_Doc_No, thedate as Punching_Date,'In' as InOutView,'I' as InOut,null as SourceCode,null as SourceName,null as SourceType ,TSPL_ITEM_MASTER.Item_Type,null as Item_Type_Name,null as Item_Group,null as Group_Description," + strCodeColumnNull + "," + strCodeDescColumnNull + ",TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,null as Item_Category_Struct_Code,TSPL_ITEM_UOM_DETAIL.UOM_Code as Stock_UOM ,null as  itf_code ,0 as Stock_Qty,0 as Balance_QTYKG,0 as Rate,0 as Cost,0 as Balance_FAT, 0 as Balance_SNF,0 as ACTBalance_FAT, 0 as ACTBalance_SNF,0 as STDBalance_FAT, 0 as STDBalance_SNF ,null as In_Category,null as Out_Category,null as Code  "
                'If objFilter.MRPWise Then
                '    strFinalQry += ",0 as MRP "
                'End If
                'strFinalQry += " FROM ExplodeDates('" + clsCommon.GetPrintDate(objFilter.From_Date, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(objFilter.To_Date, "dd/MMM/yyyy") + "') as d,TSPL_ITEM_MASTER,TSPL_LOCATION_MASTER,TSPL_ITEM_UOM_DETAIL  where 2=2 "
                'If objFilter.arrItem IsNot Nothing AndAlso objFilter.arrItem.Count > 0 Then
                '    strFinalQry += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(objFilter.arrItem) + ") "
                'End If

                'If clsCommon.myLen(strWhrCatg) > 0 Then
                '    strFinalQry += "  and TSPL_LOCATION_MASTER.Location_Code in (" + strWhrCatg + ") "
                'End If



                'strFinalQry += " and ((TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' and (TSPL_LOCATION_MASTER.Location_Type='Physical' or TSPL_LOCATION_MASTER.Location_Type='Logical') ) or (TSPL_LOCATION_MASTER.CSA_Type='Y') )  and TSPL_ITEM_UOM_DETAIL.Stocking_Unit='Y' and TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code"
                strFinalQry += " )xxxxxx Group by  Item_Code,Location_Code )xxxxxxx " & _
                    " Left join TSPL_LOCATION_MASTER Loc on xxxxxxx.Location_Code=Loc.Location_Code  where xxxxxxx.Location_Code is not null  "

                If Not objFilter.ChkPartialyLoadData AndAlso objFilter.isPrintCrystal = 0 Then
                    strFinalQry += " Order by item_code "
                End If

                ''==========Location Wise Item in transaction

            ElseIf clsCommon.CompairString(clsCommon.myCstr(objFilter.ReportType), "Document Wise Detail Ledger") = CompairStringResult.Equal Then
                strFinalQry = "select  case when '" & LocationFirstTime & "'='1' then '" & LocationAddress & "' else '" + objCommonVar.CurrentCompanyName + " ' end as CompName,'" + clsCommon.myCDate(objFilter.From_Date, "dd/MMM/yyyy") + "' as FromDate,'" + clsCommon.myCDate(objFilter.To_Date, "dd/MMM/yyyy") + "' as ToDate ,  Trans_Id,Main_Location_Code as Main_Location_Code,MainLocationDesc as MainLocationDesc,Location_Code,[Loc Desp],SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description ," + strCodeColumn + "," + strCodeDescColumn + ", Item_Category_Struct_Code,itf_code ,Stock_Qty,Rate,Cost "
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "M") = CompairStringResult.Equal Then
                    strFinalQry += ",isnull((CASE when Balance_QTYKG=0 then 0 else (ACTBalance_FAT*100/Balance_QTYKG) end),0) as Balance_FATPER ,ACTBalance_FAT as Balance_FAT,isnull((CASE when Balance_QTYKG=0 then 0 else ([ACTBalance_SNF]*100/[Balance_QTYKG]) end),0) as [Balance_SNFPER],ACTBalance_SNF as Balance_SNF "
                Else
                    strFinalQry += ",isnull((CASE when Balance_QTYKG=0 then 0 else (Balance_FAT*100/Balance_QTYKG) end),0) as Balance_FATPER ,Balance_FAT,isnull((CASE when Balance_QTYKG=0 then 0 else ([Balance_SNF]*100/[Balance_QTYKG]) end),0) as [Balance_SNFPER] , Balance_SNF "
                End If
                ''============================Monika 29/03/2017====================================================================
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += ",isnull((CASE when Balance_QTYKG=0 then 0 else (ACTBalance_FAT*100/Balance_QTYKG) end),0) as ACTBalance_FATPER ,ACTBalance_FAT,isnull((CASE when Balance_QTYKG=0 then 0 else ([ACTBalance_SNF]*100/[Balance_QTYKG]) end),0) as [ACTBalance_SNFPER] , ACTBalance_SNF "
                End If
                If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
                    strFinalQry += ",isnull((CASE when Balance_QTYKG=0 then 0 else (STDBalance_FAT*100/Balance_QTYKG) end),0) as STDBalance_FATPER ,STDBalance_FAT,isnull((CASE when Balance_QTYKG=0 then 0 else ([STDBalance_SNF]*100/[Balance_QTYKG]) end),0) as [STDBalance_SNFPER] , STDBalance_SNF "
                End If
                ''==================================================================================================================

                strFinalQry += ",SourceCode,SourceName,Source_Doc_No,convert(varchar,Punching_Date,103) as Punching_Date,Trans_Type,Trans_Type_Name,InOut,InOutView, Item_Code ,Item_Desc,Stock_UOM, (case when InOut='I' then Stock_Qty else 0 end) as RecQty, (case when InOut='I' then Rate else 0 end) as RecRate, (case when InOut='I' then Cost else 0 end) as RecCost, (case when InOut='O' then -1*Stock_Qty else 0 end) as IssQty, (case when InOut='O' then Rate else 0 end) as IssRate, (case when InOut='O' then -1*Cost else 0 end) as IssCost, SUM(Stock_Qty) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date) as CLQty ,( case when SUM(Stock_Qty) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date)=0 then 0  else SUM(Cost) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date)/SUM(Stock_Qty) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date) end) as CLRate ,SUM(Cost) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date) as CLCost "
                If objFilter.MRPWise Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from ("
                strFinalQry += "select 0 as Trans_Id,'' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,null as Punching_Date,'' as InOutView, '' as InOut,'' as Main_Location_Code,'' as MainLocationDesc,'' as Location_Code,'' as [Loc Desp],'' as [LocAddress],'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",xxx.Item_Code as Item_Code ,max(xxx.Item_Desc) as Item_Desc,'' as Item_Category_Struct_Code,max(xxx.Stock_UOM) as Stock_UOM,'' as itf_code ,sum( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG, case when sum(convert(decimal(18,3),Stock_Qty* case when InOut='I' then 1 else -1 end))=0 then 0 else sum(convert(decimal(18,3),Cost* case when InOut='I' then 1 else -1 end))/sum(convert(decimal(18,3),Stock_Qty* case when InOut='I' then 1 else -1 end)) end as Rate,sum(Cost * case when InOut='I' then 1 else -1 end) as Cost,sum( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end)) as Balance_SNF " & _
                    ",sum( ACT_FAT_KG * (case when InOut='I' then 1 else -1 end)) as ACTBalance_FAT,sum(ACT_SNF_KG * (case when InOut='I' then 1 else -1 end)) as ACTBalance_SNF "
                If objFilter.MRPWise Then
                    strFinalQry += ",Max(MRP) as MRP "
                End If
                strFinalQry += " from (" + Qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' group by xxx.Item_Code " + Environment.NewLine
                strFinalQry += " union all "
                strFinalQry += " select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,InOutView, InOut,Main_Location_Code,MainLocationDesc,Location_Code,[Loc Desp], [LocAddress],SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG,convert(decimal(18,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1 else -1 end) as Cost,( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT, ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end) as Balance_SNF  " & _
                    ",( ACT_FAT_KG * (case when InOut='I' then 1 else -1 end)) as ACTBalance_FAT, ( ACT_SNF_KG * case when InOut='I' then 1 else -1 end) as ACTBalance_SNF "
                If objFilter.MRPWise Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from (" + Qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' and Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") + "' " + Environment.NewLine

                strFinalQry += ")xxxxxx "
                If objFilter.isPrintCrystal = 0 Then
                    strFinalQry += " Order by Item_Code,Punching_Date "
                End If
            End If

            Return strFinalQry

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try
    End Function
    Public Shared Function GetQuery(ByRef strFinalQry As String, ByRef Qry As String, ByRef objFilter As clsStockRecoFilters, ByRef LocationFirstTime As String, ByRef LocationAddress As String, ByRef strCodeColumn As String, ByRef strCodeDescColumn As String, ByVal PeriodCol As String) As String
        strFinalQry = "select  case when '" & LocationFirstTime & "'='1' then '" & LocationAddress & "' else '" & objCommonVar.CurrentCompanyName & " ' end as CompName, " & _
                        " '" & clsCommon.myCDate(objFilter.From_Date, "dd/MMM/yyyy") & "' as FromDate,'" & clsCommon.myCDate(objFilter.To_Date, "dd/MMM/yyyy") & "' as ToDate , " & _
                        " DisplayId,(case when DisplayId=4 then Trans_Id else 0 end) as Trans_Id,(case when DisplayId=4 then Trans_Type else '' end) as Trans_Type ,(case when DisplayId=4 then Trans_Type_Name else '' end) as Trans_Type_Name,(case when DisplayId in (1,4) then Source_Doc_No else '' end) as Source_Doc_No, (case when DisplayId in (1,2,4) then convert(varchar,Punching_Date,103) else DisplayPeriod end) as Punching_Date,[Year],[Month],[Day],DisplayPeriod,(case when DisplayId=4 then InOutView else '' end) as InOutView,(case when DisplayId=4 then InOut else '' end) as  InOut,Location_Code, " & _
                        " [Loc Desp],case when coalesce(PrimaryLocation,'')='' then Location_Code else PrimaryLocation end as PrimaryLocation,case when coalesce(PrimaryLocation,'')='' then [Loc Desp] else MainLocationDesc end as MainLocationDesc,(case when DisplayId=4 then SourceCode else '' end) as SourceCode,(case when DisplayId=4 then SourceName else '' end) as SourceName,(case when DisplayId=4 then SourceType else '' end) as SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description, " & _
                        " " & strCodeColumn & "," & strCodeDescColumn & ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,(case when DisplayId=4 then itf_code else '' end) as itf_code,Stock_Qty,[Balance_QTYKG],(case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,Cost," & _
                        " isnull((CASE when Balance_QTYKG=0 then 0 else (Balance_FAT*100/Balance_QTYKG) end),0) as Balance_FATPER ,Balance_FAT as Balance_FAT, " & _
                        " isnull((CASE when Balance_QTYKG=0 then 0 else ([Balance_SNF]*100/[Balance_QTYKG]) end),0) as [Balance_SNFPER] , Balance_SNF "
        ''=======================Monika 27/03/2017=====================================================
        strFinalQry += " ,isnull((CASE when Balance_QTYKG=0 then 0 else (ACTBalance_FAT*100/Balance_QTYKG) end),0) as ACTBalance_FATPER ,ACTBalance_FAT as ACTBalance_FAT, " & _
                        " isnull((CASE when Balance_QTYKG=0 then 0 else ([ACTBalance_SNF]*100/[Balance_QTYKG]) end),0) as [ACTBalance_SNFPER] ,ACTBalance_SNF as ACTBalance_SNF "

        strFinalQry += " ,isnull((CASE when Balance_QTYKG=0 then 0 else (STDBalance_FAT*100/Balance_QTYKG) end),0) as STDBalance_FATPER ,STDBalance_FAT as STDBalance_FAT, " & _
                        " isnull((CASE when Balance_QTYKG=0 then 0 else ([STDBalance_SNF]*100/[Balance_QTYKG]) end),0) as [STDBalance_SNFPER] ,STDBalance_SNF as STDBalance_SNF "
        ''=============================================================================================

        strFinalQry += " from ("
        strFinalQry += " select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,YEAR(Punching_Date) AS [Year],MONTH(Punching_Date) as [Month],DAY(Punching_Date) as [Day]," & PeriodCol & ",InOutView, InOut,Main_Location_Code,MainLocationDesc,Location_Code,[Loc Desp], [LocAddress],PrimaryLocation,SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,( Stock_Qty * case when InOut='I' or Trans_Type='Opening' then 1 else -1 end) as Stock_Qty,(QtyKG * case when InOut='I' or Trans_Type='Opening' then 1 else -1 end) as Balance_QTYKG,convert(decimal(18,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' or Trans_Type='Opening' then 1 else -1 end) as Cost," & _
            "( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' or Trans_Type='Opening' then 1 else -1 end) as Balance_FAT, ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' or Trans_Type='Opening' then 1 else -1 end) as Balance_SNF  "

        If objFilter.MRPWise Then
            strFinalQry += ",MRP "
        End If

        ''=======================Monika 27/03/2017=====================================================
        strFinalQry += ",( ACT_FAT_KG * case when InOut='I' or Trans_Type='Opening' then 1 else -1 end) as ACTBalance_FAT, ( ACT_SNF_KG * case when InOut='I' or Trans_Type='Opening' then 1 else -1 end) as ACTBalance_SNF  "

        strFinalQry += ",( STD_FAT_KG * case when InOut='I' or Trans_Type='Opening' then 1 else -1 end) as STDBalance_FAT, ( STD_SNF_KG * case when InOut='I' or Trans_Type='Opening' then 1 else -1 end) as STDBalance_SNF  "
        ''=============================================================================================

        strFinalQry += " from (" & Qry & ") xxx " & Environment.NewLine
        strFinalQry += " where Punching_Date>='" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objFilter.From_Date), "dd/MMM/yyyy hh:mm:ss tt") & "' and Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objFilter.To_Date), "dd/MMM/yyyy hh:mm:ss tt") & "' " & Environment.NewLine
        strFinalQry += ")xxxxxx  "

        ''=======================Monika 27/03/2017=====================================================
        Dim QCFATSNFVAR As String = ""
        If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "Q") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
            QCFATSNFVAR = " ,isnull((CASE when sum(Balance_QTYKG)=0 then 0 else (sum(ACTBalance_FAT)*100/sum(Balance_QTYKG)) end),0) as ACTBalance_FATPER ,sum(ACTBalance_FAT) as ACTBalance_FAT,isnull((CASE when sum(Balance_QTYKG)=0 then 0 else (sum([ACTBalance_SNF])*100/sum([Balance_QTYKG])) end),0) as [ACTBalance_SNFPER] , sum(ACTBalance_SNF) as ACTBalance_SNF " & _
                " ,isnull((CASE when sum(Balance_QTYKG)=0 then 0 else (sum(Balance_FAT)*100/sum(Balance_QTYKG)) - (sum(ACTBalance_FAT)*100/sum(Balance_QTYKG)) end),0) as [Diff. Bal FAT%] ,sum(Balance_FAT) - sum(ACTBalance_FAT) as [Diff. Bal. FAT KG],isnull((CASE when sum(Balance_QTYKG)=0 then 0 else (sum([Balance_SNF])*100/sum([Balance_QTYKG])) - (sum([ACTBalance_SNF])*100/sum([Balance_QTYKG])) end),0) as [Diff. Bal. SNF%] , sum(Balance_SNF) - sum(ACTBalance_SNF) as [Diff. Bal. SNF KG] "
        End If

        If clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.FAT_SNF_TYPE, "A") = CompairStringResult.Equal Then
            QCFATSNFVAR += " ,isnull((CASE when sum(Balance_QTYKG)=0 then 0 else (sum(STDBalance_FAT)*100/sum(Balance_QTYKG)) end),0) as STDBalance_FATPER ,sum(STDBalance_FAT) as STDBalance_FAT,isnull((CASE when sum(Balance_QTYKG)=0 then 0 else (sum([STDBalance_SNF])*100/sum([Balance_QTYKG])) end),0) as [STDBalance_SNFPER] , sum(STDBalance_SNF) as STDBalance_SNF " & _
                " ,isnull((CASE when sum(Balance_QTYKG)=0 then 0 else (sum(Balance_FAT)*100/sum(Balance_QTYKG)) - (sum(STDBalance_FAT)*100/sum(Balance_QTYKG)) end),0) as [Diff. STD Bal FAT%] ,sum(Balance_FAT) - sum(STDBalance_FAT) as [Diff. STD Bal. FAT KG],isnull((CASE when sum(Balance_QTYKG)=0 then 0 else (sum([Balance_SNF])*100/sum([Balance_QTYKG])) - (sum([STDBalance_SNF])*100/sum([Balance_QTYKG])) end),0) as [Diff. STD Bal. SNF%] , sum(Balance_SNF) - sum(STDBalance_SNF) as [Diff. STD Bal. SNF KG] "
        End If
        ''=============================================================================================

        ''outer select 
        strFinalQry = "select CompName,FromDate,ToDate,Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,max([Day]) as [Day],max([Year]) as [Year],max([Month]) as [Month],DisplayPeriod,InOutView, InOut,Location_Code," & _
            " [Loc Desp],PrimaryLocation,max(MainLocationDesc) as MainLocationDesc,SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description, " & _
            " " & strCodeColumn & "," & strCodeDescColumn & ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code , " & _
            " sum(Stock_Qty) as Stock_Qty,(case when sum(Stock_Qty)=0 then 0 else sum(Cost)/sum(Stock_Qty) end) as Rate,sum(Cost) as Cost," & _
            " isnull((CASE when sum(Balance_QTYKG)=0 then 0 else (sum(Balance_FAT)*100/sum(Balance_QTYKG)) end),0) as Balance_FATPER ,sum(Balance_FAT) as Balance_FAT, " & _
            " isnull((CASE when sum(Balance_QTYKG)=0 then 0 else (sum([Balance_SNF])*100/sum([Balance_QTYKG])) end),0) as [Balance_SNFPER] , sum(Balance_SNF) as Balance_SNF " & _
            " " + QCFATSNFVAR + " from (" & strFinalQry & ") Final " & _
            " group by CompName,FromDate,ToDate,Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,DisplayPeriod,InOutView, InOut,Location_Code," & _
            " [Loc Desp],PrimaryLocation,SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description, " & _
            " " & strCodeColumn & "," & strCodeDescColumn & ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code,DisplayId "

        strFinalQry = strFinalQry

        If objFilter.isPrintCrystal = 0 Then
            strFinalQry += " Order by [Year],[Month],[Day],DisplayId"
        End If

        Return strFinalQry
    End Function
    Public Shared Function GetStockDetailQry(ByVal arrLoc As ArrayList, ByVal From_Date As Date, ByVal To_Date As Date, ByVal Item_Code As String)
        Dim Qry As String = ""
        Dim QryCond As String = " where 2=2 and Item_Code='" & Item_Code & "'"
        Dim QryCondMilk As String = " where 2=2 and Item_Code='" & Item_Code & "'"

        Dim Punching_DateCol As String = ""
        QryCond = QryCond & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(To_Date, "dd-MMM-yyyy") & "' and Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")"
        QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(To_Date, "dd-MMM-yyyy") & "' and Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")"

        QryCond = QryCond & " and len(coalesce(TSPL_INVENTORY_MOVEMENT.Location_Code,''))>0 "
        QryCondMilk = QryCondMilk & "and len(coalesce(TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,''))>0 "

        Qry = " select convert(varchar,Final.Punching_Date,103) as [Document date],Final.Product_Type,Final.Trans_Type," & Environment.NewLine &
              " Final.InOut,Final.Location_Code as [Location Code],Final.Source_Doc_No as [Source Doc No],Final.Item_Code as [Item Code],Item.Item_Desc as [Item Desc], " & Environment.NewLine &
              " Final.Stock_Qty,Final.Stock_UOM,Final.Net_Cost,Final.Avg_Cost, " & Environment.NewLine &
              " (case when Final.Product_Type='MI' then Final.Fat_Per else  Item_Fat.Fat_Per end) as Fat_Per, " & Environment.NewLine &
              " (case when Final.Product_Type='MI' then Final.SNF_Per else  Item_SNF.SNF_Per end) as SNF_Per, " & Environment.NewLine &
              " (case when Final.Product_Type='MI' then Final.FAT_Kg else  (case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Item_Fat.Fat_Per*Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)*100) as Float) end) end)*(case when Final.InOut='I' then 1 else -1 end) as FAT_Kg," & Environment.NewLine &
              " (case when Final.Product_Type='MI' then Final.SNF_Kg else  (case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Item_SNF.SNF_Per*Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)*100) as Float) end) end)*(case when Final.InOut='I' then 1 else -1 end) as SNF_Kg " & Environment.NewLine &
              " from ( " & Environment.NewLine &
              " select 'MP' as Product_Type,Trans_Type,InOut,Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,Net_Cost,Avg_Cost, " & Environment.NewLine &
              " 0 as Fat_Per,0 as SNF_Per,0 as FAT_Kg ,0 as SNF_Kg,cast(Punching_Date as date) as Punching_Date " & Environment.NewLine &
              " from TSPL_INVENTORY_MOVEMENT " & QryCond & " " & Environment.NewLine &
              " union all " & Environment.NewLine &
              " select  'MI' as Product_Type,Trans_Type,InOut,Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,Net_Cost,Avg_Cost, " & Environment.NewLine &
              " Fat_Per,SNF_Per,FAT_Kg,SNF_Kg,cast(Punching_Date as date) as Punching_Date " & Environment.NewLine &
              " from TSPL_INVENTORY_MOVEMENT_NEW " & QryCondMilk & " " & Environment.NewLine &
              " ) as Final " & Environment.NewLine &
              " left join TSPL_ITEM_MASTER Item on Final.Item_Code=Item.Item_Code " & Environment.NewLine &
              " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL) as Stock_SU on Final.Item_Code=Stock_SU.Item_Code and Final.Stock_UOM=Stock_SU.UOM_Code " & Environment.NewLine &
              " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG') as StockKG on Final.Item_Code=StockKG.Item_Code " & Environment.NewLine &
              " left join (select Item_QC.Item_Code,max(Item_QC.Actual_Range) as Fat_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QC " & Environment.NewLine &
              " left outer join TSPL_PARAMETER_MASTER Params on Params.Code=Item_QC.Code where Params.Type='FAT' " & Environment.NewLine &
              " group by Item_QC.Item_Code) as Item_Fat on Final.Item_Code=Item_Fat.Item_Code " & Environment.NewLine &
              " left join (select  Item_QC.Item_Code,max(Item_QC.Actual_Range) as SNF_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QC " & Environment.NewLine &
              " left outer join TSPL_PARAMETER_MASTER Params on Params.Code=Item_QC.Code where Params.Type='SNF' " & Environment.NewLine &
              " group by Item_QC.Item_Code) Item_SNF on Final.Item_Code=Item_SNF.Item_Code " & Environment.NewLine &
              " left join TSPL_LOCATION_MASTER Loc on Final.Location_Code=Loc.Location_Code where 2=2 "
        'Qry = "select * from (" & Qry & ") as Outermost "
        Return Qry
    End Function
    Public Shared Function GetBaseQryOpeningGIT() As String
        Dim qry As String = " select TSPL_INV_MOVE_DL.Item_Code,TSPL_INV_MOVE_DL.Location_Code,TSPL_INV_MOVE_DL.Stock_UOM,TSPL_INV_MOVE_DL.TRANS_DATE,TSPL_INV_MOVE_DL.CL_QTY," & _
                            " TSPL_INV_MOVE_DL.CL_Avg_Cost,TSPL_INV_MOVE_DL.CL_FAT_KG,TSPL_INV_MOVE_DL.CL_SNF_KG,TSPL_INV_MOVE_DL.CL_FIFO_Cost,TSPL_INV_MOVE_DL.CL_LIFO_Cost," & _
                            " TSPL_INV_MOVE_DL.CL_QC_FAT_KG,TSPL_INV_MOVE_DL.CL_QC_SNF_KG from  ( " & _
                            " select TSPL_INV_MOVE_DL.Item_Code,TSPL_INV_MOVE_DL.Location_Code,TSPL_INV_MOVE_DL.Stock_UOM,max(TSPL_INV_MOVE_DL.TRANS_DATE) as TRANS_DATE from TSPL_INV_MOVE_DL  " & _
                            " inner join View_STOCK_DATA_GIT on TSPL_INV_MOVE_DL.Item_Code=View_STOCK_DATA_GIT.Item_Code " & _
                            " and TSPL_INV_MOVE_DL.Location_Code=View_STOCK_DATA_GIT.Location_Code " & _
                            " and TSPL_INV_MOVE_DL.Stock_UOM=View_STOCK_DATA_GIT.Stock_UOM and TSPL_INV_MOVE_DL.TRANS_DATE<View_STOCK_DATA_GIT.Punching_Date " & _
                            " group by TSPL_INV_MOVE_DL.Item_Code,TSPL_INV_MOVE_DL.Location_Code,TSPL_INV_MOVE_DL.Stock_UOM) Opening " & _
                            " left join TSPL_INV_MOVE_DL  on TSPL_INV_MOVE_DL.Item_Code=Opening.Item_Code " & _
                            " and TSPL_INV_MOVE_DL.Location_Code=Opening.Location_Code and TSPL_INV_MOVE_DL.Stock_UOM=Opening.Stock_UOM and TSPL_INV_MOVE_DL.TRANS_DATE=Opening.TRANS_DATE"
        Return qry
    End Function
    Public Shared Function ReturnClosingUpdateQryFromOpening(ByVal QryCond As String) As String
        Dim OpeningQry As String = GetBaseQryOpeningGIT()
        Dim qry As String = " MERGE INTO TSPL_INV_MOVE_DL  A" & _
                            " USING(" & OpeningQry & ") TA  " & _
                            " ON (A.ITEM_CODE=TA.ITEM_CODE AND A.LOCATION_CODE=TA.LOCATION_CODE AND A.TRANS_DATE=TA.TRANS_DATE AND A.STOCK_UOM=TA.STOCK_UOM) " & _
                            " WHEN MATCHED THEN  " & _
                            " update " & _
                            " SET A.CL_QTY=TA.CL_QTY+A.Trans_Qty,A.CL_Fat_KG=TA.CL_Fat_KG+A.Fat_KG,A.CL_SNF_KG=TA.CL_SNF_KG+A.SNF_KG,A.CL_AVG_COST=TA.CL_AVG_COST+A.AVG_COST, " & _
                            " A.CL_FIFO_COST=TA.CL_FIFO_COST+A.FIFO_COST,A.CL_LIFO_COST=TA.CL_LIFO_COST+A.LIFO_COST,A.CL_QC_FAT_KG=TA.CL_QC_FAT_KG+A.QC_FAT_KG,A.CL_QC_SNF_KG=TA.CL_QC_SNF_KG+A.QC_SNF_KG;"
        '" WHEN NOT MATCHED THEN  " & _
        '" update " & _
        '" SET A.CL_QTY=A.Trans_Qty,A.CL_Fat_KG=A.Fat_KG,A.CL_SNF_KG=A.SNF_KG,A.CL_AVG_COST=A.AVG_COST, " & _
        '" A.CL_FIFO_COST=A.FIFO_COST,A.CL_LIFO_COST=A.LIFO_COST,A.CL_QC_FAT_KG=A.QC_FAT_KG,A.CL_QC_SNF_KG=A.QC_SNF_KG;"
        Return qry
    End Function
    Public Shared Function InsertIntoGITTable(ByVal UpdateAllItem As Boolean, ByVal arrIem As ArrayList, ByVal ArrLoc As ArrayList, ByVal trans As SqlTransaction) As Boolean
        If arrIem IsNot Nothing AndAlso arrIem.Count > 0 Or ArrLoc IsNot Nothing AndAlso ArrLoc.Count > 0 Then
            UpdateAllItem = False
        End If
        Dim qry As String = ""
        Dim FilterItem As String = "select Item_Code from TSPL_ITEM_MASTER where Item_Code not in (select item_Code from TSPL_INV_MOVE_DL)"
        Dim FilterLoc As String = ""
        If UpdateAllItem = False Then
            FilterItem = "select Item_Code from TSPL_ITEM_MASTER where 2=2 "
            If arrIem IsNot Nothing AndAlso arrIem.Count > 0 Then
                FilterItem = FilterItem & " and Item_Code in (" & clsCommon.GetMulcallString(arrIem) & ")"
            End If
            If ArrLoc IsNot Nothing AndAlso ArrLoc.Count > 0 Then
                FilterLoc = " And Location_Code in (" & clsCommon.GetMulcallString(ArrLoc) & ")"
            End If
        End If
        Try
            clsDBFuncationality.ExecuteNonQuery("delete from TEMP_Delete_Inv_Sumry", trans)
            qry = "insert into TEMP_Delete_Inv_Sumry(Item_Code,Item_Desc) "
            qry += " select Item_Code,Item_Desc " + Environment.NewLine + _
            " from TSPL_ITEM_MASTER " + Environment.NewLine + _
            " where Item_Code in (" + FilterItem + ")"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '' update item in temp table for job
            qry = " INSERT into TSPL_INVENTORY_MOVEMENT_WIN(Trans_Id,Trans_Type,InOut,Location_Code,Item_Code,Item_Desc,Qty,UOM,Source_Doc_No,Source_Doc_Date,  " & _
                  " Entry_Date,Basic_Cost,Rec_Cost,Add_Cost,Net_Cost,Created_By,Comp_Code,ItemType,Punching_Date,MRP,Batch_No,FIFO_Cost,LIFO_Cost,Avg_Cost,Posting_Date, " & _
                  " PI_Cost,Stock_UOM,Stock_Qty,MFG_Date,Expiry_Date,Item_Status,Assmbly_Status,IS_CONSUMPTION,Cust_Code,Cust_Name,Vendor_Code,Vendor_Name, " & _
                  " Other_Location_Code,  Other_Location_Desc,OP_TYPE,Fat_Per,SNF_Per,Fat_KG,SNF_KG,Fat_Rate,SNF_Rate,Fat_Amt,SNF_Amt)  select Trans_Id,Trans_Type,InOut,Location_Code,Item_Code,Item_Desc,Qty,UOM,Source_Doc_No, " & _
                  " Source_Doc_Date,Entry_Date,Basic_Cost,Rec_Cost,Add_Cost, Net_Cost,Created_By,Comp_Code,ItemType,Punching_Date,MRP,Batch_No,FIFO_Cost,LIFO_Cost," & _
                  " Avg_Cost,Posting_Date,PI_Cost,Stock_UOM,Stock_Qty,MFG_Date,  Expiry_Date,Item_Status,Assmbly_Status,IS_CONSUMPTION,Cust_Code,Cust_Name," & _
                  " Vendor_Code,Vendor_Name,Other_Location_Code,Other_Location_Desc,'I',Fat_Per,SNF_Per,Fat_KG,SNF_KG,Fat_Rate,SNF_Rate,Fat_Amt,SNF_Amt from TSPL_INVENTORY_MOVEMENT where Item_Code in (select Item_Code from TEMP_Delete_Inv_Sumry) " & FilterLoc & ""
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' update milk table
            qry = " INSERT into TSPL_INVENTORY_MOVEMENT_NEW_WIN(Trans_Id,Trans_Type,InOut,Location_Code,Item_Code,Item_Desc,Qty,UOM,Source_Doc_No,Source_Doc_Date, " & _
                  " Entry_Date,Basic_Cost,Rec_Cost,Add_Cost,Net_Cost,Created_By,Comp_Code,ItemType,Punching_Date,MRP,Batch_No,MFG_Date,Expiry_Date,FIFO_Cost,LIFO_Cost, " & _
                  " Avg_Cost,Posting_Date,PI_Cost,Stock_UOM,Stock_Qty,Item_Status,Assmbly_Status,Fat_Per,SNF_Per,Fat_KG,SNF_KG,main_location,IS_CONSUMPTION,Cust_Code, " & _
                  " Cust_Name,Vendor_Code,Vendor_Name,Other_Location_Code,Other_Location_Desc,Fat_Rate,SNF_Rate,Fat_Amt,SNF_Amt,Std_Qty,OP_TYPE)  select Trans_Id, " & _
                  " Trans_Type,InOut,Location_Code,Item_Code,Item_Desc,Qty,UOM,Source_Doc_No,Source_Doc_Date,Entry_Date,Basic_Cost,Rec_Cost,  Add_Cost,Net_Cost," & _
                  " Created_By,Comp_Code,ItemType,Punching_Date,MRP,Batch_No,MFG_Date,Expiry_Date,FIFO_Cost,LIFO_Cost,Avg_Cost,Posting_Date,  PI_Cost,Stock_UOM, " & _
                  " Stock_Qty,Item_Status,Assmbly_Status,Fat_Per,SNF_Per,Fat_KG,SNF_KG,main_location,IS_CONSUMPTION,Cust_Code,Cust_Name,  Vendor_Code,Vendor_Name," & _
                  " Other_Location_Code,Other_Location_Desc,Fat_Rate,SNF_Rate,Fat_Amt,SNF_Amt,Std_Qty,'I' from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code in (select Item_Code from TEMP_Delete_Inv_Sumry) " & FilterLoc & ""
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TEMP_Delete_Inv_Sumry", trans)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function GetQtyFatSNFBaseQryGK(ByVal objFilter As clsStockRecoFilters, ByVal is_Opening As Boolean, Optional ByVal ShowTankerNo As Boolean = False) As String
        Dim Qry As String = ""
        Dim QryCond As String = " where 2=2 "
        Dim QryCondMilk As String = " where 2=2 "
        Dim ReportTypeCol As String = ""
        Dim Punching_DateCol As String = ""

        Dim LocationFirstTime As Integer = 0
        Dim LocationAddress As String = String.Empty
        Dim strWhrCatg As String = ""
        Dim strWhrCatgOldInv As String = ""
        Dim strCondDisp As String = ""
        Dim strCondPS As String = ""
        If objFilter.SelectLocation = True Then
            Dim IsApplicable As Boolean = False
            For ii As Integer = 0 To objFilter.arrLocation.Count - 1
                If clsCommon.myCBool(objFilter.arrLocation(ii).Sel) Then
                    LocationFirstTime += 1
                    If LocationFirstTime = 1 Then
                        LocationAddress = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress] from TSPL_LOCATION_MASTER where Location_Code= '" & clsCommon.myCstr(objFilter.arrLocation(ii).Code) & "'")
                    End If
                    If IsApplicable Then
                        strWhrCatg += " Or "
                        strWhrCatgOldInv += " Or "
                        strCondDisp += " Or "
                        strCondPS += " Or "
                    End If

                    strWhrCatg += " ((case when Is_Section='N' and Is_Sub_Location='N' then TSPL_INVENTORY_MOVEMENT_NEW.Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(objFilter.arrLocation(ii).Code) + "') "
                    strWhrCatgOldInv += " ((case when Is_Section='N' and Is_Sub_Location='N' then TSPL_INVENTORY_MOVEMENT.Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(objFilter.arrLocation(ii).Code) + "') "
                    strCondDisp += " ((case when Is_Section='N' and Is_Sub_Location='N' then TSPL_MCC_Dispatch_Challan.MCC_CODE else Main_Location_Code end) = '" + clsCommon.myCstr(objFilter.arrLocation(ii).Code) + "') "
                    strCondPS += " ((case when Is_Section='N' and Is_Sub_Location='N' then coalesce(TSPL_PHYSICAL_STOCK.Silo_Location,TSPL_PHYSICAL_STOCK.Location) else Main_Location_Code end) = '" + clsCommon.myCstr(objFilter.arrLocation(ii).Code) + "') "
                    IsApplicable = True
                    Dim arr As Dictionary(Of String, Object) = objFilter.arrLocation(ii).arrOut
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        strWhrCatg += " and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in ("
                        strWhrCatgOldInv += " and TSPL_INVENTORY_MOVEMENT.Location_Code in ("
                        strCondDisp += " and TSPL_MCC_Dispatch_Challan.MCC_CODE in ("
                        strCondPS += " and (case when len(coalesce(TSPL_PHYSICAL_STOCK.Silo_Location,''))>0 then TSPL_LOCATION_MASTER.Main_Location_Code else TSPL_PHYSICAL_STOCK.Location end) in ("
                        Dim isFirstTime As Boolean = True
                        For Each strInn As String In arr.Keys
                            If Not isFirstTime Then
                                strWhrCatg += ","
                                strWhrCatgOldInv += ","
                                strCondDisp += ","
                                strCondPS += ","
                            End If
                            strWhrCatg += "'" + strInn + "'"
                            strWhrCatgOldInv += "'" + strInn + "'"
                            strCondDisp += "'" + strInn + "'"
                            strCondPS += "'" + strInn + "'"
                            isFirstTime = False
                        Next
                        strWhrCatg += ")"
                        strWhrCatgOldInv += ")"
                        strCondDisp += ")"
                        strCondPS += ")"
                    End If
                End If
            Next
            If Not IsApplicable Then
                Throw New Exception("Please select at least one location")
            End If

            'Else
            '    If clsCommon.CompairString(objFilter.FORMTYPE, clsUserMgtCode.stockRecoNewJR) = CompairStringResult.Equal Then
            '        strWhrCatg += "  (Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where len(coalesce(Jobwork_Vendor,''))>0 and Is_Sub_Location='Y'))"
            '    End If
        End If
        If Not objFilter.arrItem Is Nothing AndAlso objFilter.arrItem.Count > 0 Then
            If clsCommon.myLen(strWhrCatg) > 0 Then
                strWhrCatg = strWhrCatg & " and " & " Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
                strWhrCatgOldInv = strWhrCatgOldInv & " and " & " Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
                strCondDisp = strCondDisp & " and " & " Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
                strCondPS = strCondPS & " and " & " Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
            Else
                strWhrCatg = " Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
                strWhrCatgOldInv = " strWhrCatgOldInv in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
                strCondDisp = " Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
                strCondPS = " Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
            End If
        End If
        If clsCommon.myLen(strWhrCatg) <= 0 Then
            strWhrCatg = "2=2 "
            strWhrCatgOldInv = "2=2 "
            strCondDisp = "2=2 "
            strCondPS = "2=2 "
        End If

        If is_Opening Then
            'If IncludeSubLocation Then
            '    QryCond = QryCond & "and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and TSPL_INVENTORY_MOVEMENT.Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ") and (TSPL_INVENTORY_MOVEMENT.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") or TSPL_INVENTORY_MOVEMENT.Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where Main_Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")))"               
            '    QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and TSPL_INVENTORY_MOVEMENT_New.Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ") and (TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") or TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where Main_Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")))"
            'Else
            '    QryCond = QryCond & "and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and TSPL_INVENTORY_MOVEMENT.Item_Code in (" & clsCommon.GetMulcallString(arrItemCode) & ") and  TSPL_INVENTORY_MOVEMENT.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")"                
            '    QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and  TSPL_INVENTORY_MOVEMENT_New.Item_Code in (" & clsCommon.GetMulcallString(arrItemCode) & ") and  TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")"
            'End If
            QryCond = QryCond & "and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and " & strWhrCatgOldInv & ""
            QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and " & strWhrCatg & ""

            ReportTypeCol = "'Opening'"
            Punching_DateCol = "cast('" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' as date)"
        Else
            'If IncludeSubLocation Then
            '    QryCond = QryCond & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "'  and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "' and TSPL_INVENTORY_MOVEMENT.Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ") and (TSPL_INVENTORY_MOVEMENT.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") or TSPL_INVENTORY_MOVEMENT.Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where Main_Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")))"                
            '    QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "'  and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "' and TSPL_INVENTORY_MOVEMENT_New.Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ") and (TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") or TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where Main_Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ")))"
            'Else
            '    QryCond = QryCond & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "'  and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "' and TSPL_INVENTORY_MOVEMENT.Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ")  and TSPL_INVENTORY_MOVEMENT.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") "
            '    QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "' and TSPL_INVENTORY_MOVEMENT_New.Item_Code in (" & clsCommon.GetMulcallString(objFilter.arrItem) & ") and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (" & clsCommon.GetMulcallString(arrLoc) & ") "
            'End If
            QryCond = QryCond & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "'  and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "' and  " & strWhrCatgOldInv & ""
            QryCondMilk = QryCondMilk & "and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "' and " & strWhrCatg & " "
            '' other in : ('IC-AD','MJ-SR', 'M-PURRETURN','NRGP','Purchase Return','RGP','SRN')
            ReportTypeCol = "(Case when Final.Trans_Type in ('BulkSRN','BulkSRNTrade','MCC-MSRN','BulkSRNRet','M-PURRETURN') " & Environment.NewLine &
              " then 'Purchase' " & Environment.NewLine &
              " when Final.Trans_Type in ('DispChallan-RET','MilkTransferIn')  then 'MCC Transfer Received' " & Environment.NewLine &
              " when Final.Trans_Type in ('Transfer','TRN-RET') and Final.Inout='I' then 'Other In' " & Environment.NewLine &
              " when Final.Trans_Type in ('CSA-SALE','PS-SH','PS-SR','Sale Return','SD-CSATRANS','SD-CSATRANS-RETURN','DispatchBS','DispatchBSTrade','SaleReturnBS') then 'Sale' " & Environment.NewLine &
              " when Final.Trans_Type in ('DispChallan')  then (case when LocO.Location_Category='MCC' then 'MCC Transfer Out' else 'Plant Transfer Out' end)  " & Environment.NewLine &
              " when Final.Trans_Type in ('PP_ISSUE','PP_STDN','PRD_STG_PROC','PROD_ENTR_WB','PROD_ENTRY') and Final.Inout='O' then 'Inhouse Consumption' " & _
              " when Final.Trans_Type in ('In Transit')  then 'In Transit' " & _
              " when Final.Trans_Type in ('Physical Stock')  then 'Physical Stock' " & _
              " when Final.Inout='I' then 'Other In' when Final.Inout='O' then 'Other Out' end)"
            Punching_DateCol = "Punching_Date "
        End If
        QryCond = QryCond & " and len(coalesce(TSPL_INVENTORY_MOVEMENT.Location_Code,''))>0 "
        QryCondMilk = QryCondMilk & "and len(coalesce(TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,''))>0 "

        '" select 1 as Inv_Type,'MP' as Product_Type,Trans_Type,InOut,TSPL_INVENTORY_MOVEMENT.Location_Code as Location_Code,Source_Doc_No,TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_INVENTORY_MOVEMENT.Stock_Qty,TSPL_INVENTORY_MOVEMENT.Stock_UOM,TSPL_INVENTORY_MOVEMENT.Net_Cost,Avg_Cost, " & Environment.NewLine &
        '      " 0 as Fat_Per,0 as SNF_Per,0 as FAT_Kg ,0 as SNF_Kg,cast(Punching_Date as date) as Punching_Date,Other_Location_Code " & If(ShowTankerNo = True, ",null as Tanker_No", "") & "  " & Environment.NewLine &
        '      " from TSPL_INVENTORY_MOVEMENT left join tspl_location_master  on TSPL_INVENTORY_MOVEMENT.Location_Code=tspl_location_master.Location_Code   " & QryCond & " " & Environment.NewLine &
        '      " union all " & Environment.NewLine &
        Qry = " select Item_Code,Stock_UOM,Location_Code,Report_Type," & Punching_DateCol & " as  Punching_Date,sum(case when InOut='I' then Stock_Qty*Inv_Type else -Stock_Qty*Inv_Type end) as Qty,sum(case when InOut='I' then Stock_Qty_Ltr*Inv_Type else -Stock_Qty_Ltr*Inv_Type end) as QtyLtr,sum(case when InOut='I' then coalesce(FAT_Kg,0)*Inv_Type else -coalesce(FAT_Kg,0)*Inv_Type end) as FAT_KG,sum(case when InOut='I' then coalesce(FAT_Ltr,0)*Inv_Type else -coalesce(FAT_Ltr,0)*Inv_Type end) as FAT_Ltr," & Environment.NewLine &
              " sum(case when InOut='I' then coalesce(SNF_Kg,0)*Inv_Type else -coalesce(SNF_Kg,0)*Inv_Type end) as SNF_KG,sum(case when InOut='I' then coalesce(SNF_Ltr,0)*Inv_Type else -coalesce(SNF_Ltr,0)*Inv_Type end) as SNF_Ltr,sum(case when InOut='I' then (case when Inv_Type=0 then Stock_Qty else 0 end) else -(case when Inv_Type=0 then Stock_Qty else 0 end) end) as NotInCalcQty,sum(case when InOut='I' then (case when Inv_Type=0 then coalesce(FAT_Kg,0) else 0 end) else -(case when Inv_Type=0 then coalesce(FAT_Kg,0) else 0 end) end) as NotInCalcFAT_KG," & Environment.NewLine &
              " sum(case when InOut='I' then (case when Inv_Type=0 then coalesce(SNF_KG,0) else 0 end) else -(case when Inv_Type=0 then coalesce(SNF_KG,0) else 0 end) end) as NotInCalcSNF_KG,sum(case when InOut='I' then (case when Inv_Type=0 then coalesce(Net_Cost,0) else 0 end) else -(case when Inv_Type=0 then coalesce(Net_Cost,0) else 0 end) end) as NotInCalcNetCost " & If(ShowTankerNo = True, ",sum(case when InOut='I' then Net_Cost*Inv_Type else -Net_Cost*Inv_Type end) as Net_Cost," & If(is_Opening = True, "null as Tanker_No", "Tanker_No"), "") & " " & Environment.NewLine &
              " from ( " & Environment.NewLine &
              " select Final.Inv_Type,Final.Product_Type,Final.Trans_Type," & ReportTypeCol & " as Report_Type, " & Environment.NewLine &
              " Final.InOut,Final.Location_Code,Loc.Location_Category,Final.Item_Code, " & Environment.NewLine &
              " Final.Stock_Qty,(case when coalesce(StockLtr.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Stock_SU.Conversion_Factor)/(coalesce(StockLtr.Conversion_Factor,1)) as Float) end) as Stock_Qty_Ltr,Final.Stock_UOM,Final.Net_Cost,Final.Avg_Cost, " & Environment.NewLine &
              " (case when Final.Product_Type='MI' then Final.Fat_Per else  Item_Fat.Fat_Per end) as Fat_Per, " & Environment.NewLine &
              " (case when Final.Product_Type='MI' then Final.SNF_Per else  Item_SNF.SNF_Per end) as SNF_Per, " & Environment.NewLine &
              " (case when Final.Product_Type='MI' then Final.FAT_Kg else  (case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Item_Fat.Fat_Per*Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)*100) as Float) end) end) as FAT_Kg," & Environment.NewLine &
              " (case when Final.Product_Type='MI' then Final.SNF_Kg else  (case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Item_SNF.SNF_Per*Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)*100) as Float) end) end) as SNF_Kg," & Environment.NewLine &
              " (case when coalesce(StockLtr.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Final.Fat_Per*Stock_SU.Conversion_Factor)/(coalesce(StockLtr.Conversion_Factor,1)*100) as Float) end) as FAT_Ltr," & Environment.NewLine &
              " (case when coalesce(StockLtr.Conversion_Factor,0)=0 then 0 else cast((Final.Stock_Qty*Final.SNF_Per*Stock_SU.Conversion_Factor)/(coalesce(StockLtr.Conversion_Factor,1)*100) as Float) end) as SNF_Ltr," & Environment.NewLine &
              " Punching_Date " & If(ShowTankerNo = True, ",Tanker_No", "") & " " & Environment.NewLine &
              " from ( " & Environment.NewLine &
              " select  1 as Inv_Type,'MI' as Product_Type,TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type,InOut,(case when len(tspl_location_master.Main_Location_Code)>0 then tspl_location_master.Main_Location_Code else TSPL_INVENTORY_MOVEMENT_NEW.Location_Code end) as Location_Code,Source_Doc_No,Item_Code,Stock_Qty,Stock_UOM,Net_Cost,Avg_Cost, " & Environment.NewLine &
              " Fat_Per,SNF_Per,FAT_Kg,SNF_Kg,cast(Punching_Date as date) as Punching_Date,Other_Location_Code " & If(ShowTankerNo = True, ",Doc.Tanker_No", "") & " " & Environment.NewLine &
              " from TSPL_INVENTORY_MOVEMENT_NEW left join tspl_location_master  on TSPL_INVENTORY_MOVEMENT_NEW.Location_Code=tspl_location_master.Location_Code "
        If ShowTankerNo Then
            Qry = Qry & " left join (select Chalan_NO as Document_No,Tanker_No,'DispChallan' as Trans_Type from TSPL_MCC_Dispatch_Challan " & _
                        " union all " & _
                        " select DocR.Document_No,doc.Tanker_No,'DispChallanRet' as Trans_Type " & _
                        " from TSPL_MCC_DISPATCH_CHALLAN_RETURN DocR " & _
                        " inner join TSPL_MCC_Dispatch_Challan Doc on DocR.Challan_No=Doc.Chalan_NO " & _
                        " union all " + _
                        " select SRN_NO as Document_No,Tanker_No,'BulkSRN' as Trans_Type from TSPL_Bulk_MILK_SRN" + _
                        " union all " + _
                        " select DocR.SRN_Return_NO as Document_No,doc.Tanker_No,'BulkSRNRet' as Trans_Type from TSPL_Bulk_Milk_SRN_Return DocR " + _
                        " inner join TSPL_Bulk_MILK_SRN Doc on DocR.SRN_NO=doc.SRN_NO " + _
                        " union all" + _
                        " select Doc.Receipt_Challan_No as Document_No,Docc.Tanker_No,'MilkTransferIn' as Trans_Type from TSPL_MILK_TRANSFER_IN Doc " + _
                        " inner join TSPL_MCC_Dispatch_Challan DocC on Doc.Dispatch_Challan_No=DocC.Chalan_NO" + _
                        " union all " + _
                        " select doc.Receipt_Challan_Return_No as Document_No,DocC.Tanker_No,'MilkTransferInReturn' as Trans_Type from TSPL_MILK_TRANSFER_IN_RETURN Doc " + _
                        " inner join TSPL_MCC_Dispatch_Challan DocC on Doc.Dispatch_Challan_No=DocC.Chalan_NO" + _
                        " union all " + _
                        " select TSPL_CAN_SALE_HEAD.Document_No as Document_No,'' as Tanker_No,'DisCanSale' as Trans_Type from TSPL_CAN_SALE_DETAIL " + _
                        " inner join TSPL_CAN_SALE_HEAD on TSPL_CAN_SALE_DETAIL.Document_No=TSPL_CAN_SALE_HEAD.Document_No " + _
                        " group by TSPL_CAN_SALE_HEAD.Document_No " + _
                        " union all " + _
                        " Select Document_No as Document_No,Tanker_Code,'DispatchBS' as Trans_Type from TSPL_Dispatch_BulkSale " + _
                        " union all " + _
                        " Select Document_No as Document_No,Tanker_No,'SALERETURNBS' as Trans_Type from TSPL_SALE_RETURN_MASTER_BULKSALE " + _
                        " ) as Doc on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No=Doc.Document_No and TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type=Doc.Trans_Type "
        End If
        Qry = Qry & " " & QryCondMilk & " " & Environment.NewLine
        Qry = Qry & " Union All " & Environment.NewLine &
              " select 0 as Inv_Type,'MI' as Product_Type,'In Transit' as Trans_Type,'O' as Inout, (case when len(tspl_location_master.Main_Location_Code)>0 then tspl_location_master.Main_Location_Code else TSPL_MCC_Dispatch_Challan.MCC_CODE end)  as Location_Code,Chalan_NO as Source_Doc_No,Item_Code," & _
              " Net_Qty as Stock_Qty,UOM_Code as Stock_Uom,Amount as Net_Cost,Avg_Amount as Avg_Cost,FAT_R as Fat_Per,SNF_R as SNF_Per,FAT_KG,SNF_KG ,cast(Dispatch_Date as Date) as Punching_Date,TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code as Other_Location_Code" & If(ShowTankerNo = True, ",null as Tanker_No", "") & " " & _
              " from TSPL_MCC_Dispatch_Challan left join TSPL_LOCATION_MASTER  on TSPL_MCC_Dispatch_Challan.MCC_Code=TSPL_LOCATION_MASTER.Location_Code  where 2=2 and cast(Dispatch_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "'  and " & strCondDisp & " " & Environment.NewLine &
              " union all " & Environment.NewLine &
              " select 0 as Inv_Type,'MI' as Product_Type,'Physical Stock' as Trans_Type,'I' as Inout, " & _
              " (case when len(coalesce(Silo_Location,''))>0 then TSPL_LOCATION_MASTER.Main_Location_Code else Location end) as Location_Code,Physical_No as Source_Doc_No,Item_Code," & _
              " Physical_Qty as Stock_Qty,Stock_Unit as Stock_Uom,0 as Net_Cost,0 as Avg_Cost,FAT_Pers as Fat_Per,SNF_Pers as SNF_Per,FAT_Kg,SNF_Kg ,cast(Stock_Date as Date) as Punching_Date,null as Other_Location_Code" & If(ShowTankerNo = True, ",null as Tanker_No", "") & " " & _
              " from TSPL_PHYSICAL_STOCK  left join TSPL_LOCATION_MASTER  on coalesce(TSPL_PHYSICAL_STOCK.Silo_Location,TSPL_PHYSICAL_STOCK.Location)=TSPL_LOCATION_MASTER.Location_Code where TSPL_PHYSICAL_STOCK.Is_Milk=1 and cast(TSPL_PHYSICAL_STOCK.Stock_Date as date) between '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "' and " & strCondPS & " " & Environment.NewLine &
              " ) as Final " & Environment.NewLine &
              " left join TSPL_ITEM_MASTER Item on Final.Item_Code=Item.Item_Code " & Environment.NewLine &
              " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL) as Stock_SU on Final.Item_Code=Stock_SU.Item_Code and Final.Stock_UOM=Stock_SU.UOM_Code " & Environment.NewLine &
              " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG') as StockKG on Final.Item_Code=StockKG.Item_Code  " & Environment.NewLine &
              " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='Ltr') as StockLtr on Final.Item_Code=StockLtr.Item_Code  " & Environment.NewLine &
              " left join (select Item_QC.Item_Code,max(Item_QC.Actual_Range) as Fat_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QC " & Environment.NewLine &
              " left outer join TSPL_PARAMETER_MASTER Params on Params.Code=Item_QC.Code where Params.Type='FAT' " & Environment.NewLine &
              " group by Item_QC.Item_Code) as Item_Fat on Final.Item_Code=Item_Fat.Item_Code " & Environment.NewLine &
              " left join (select  Item_QC.Item_Code,max(Item_QC.Actual_Range) as SNF_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QC " & Environment.NewLine &
              " left outer join TSPL_PARAMETER_MASTER Params on Params.Code=Item_QC.Code where Params.Type='SNF' " & Environment.NewLine &
              " group by Item_QC.Item_Code) Item_SNF on Final.Item_Code=Item_SNF.Item_Code " & Environment.NewLine &
              " left join TSPL_LOCATION_MASTER Loc on Final.Location_Code=Loc.Location_Code " & Environment.NewLine &
              " left join TSPL_LOCATION_MASTER LocO on Final.Other_Location_Code=LocO.Location_Code where 2=2 " & Environment.NewLine &
              " AND ( COALESCE(Item_Fat.Fat_Per,0)<>0 OR COALESCE(Item_SNF.SNF_Per,0) <>0) " & Environment.NewLine &
              " /*and Trans_Type not in ('PP_ISSUE','PP_STDN','PRD_STG_PROC','PROD_ENTRY','PROD_WR','Prod-Scrap') */" & Environment.NewLine &
              " ) as FatSNFStock group by Report_Type,Item_Code,Stock_UOM,Location_Code" & IIf(is_Opening = True, "", ",Punching_Date") & " " & If(ShowTankerNo = True And is_Opening = False, ",Tanker_No", "") & ""
        Return Qry
    End Function
    Public Shared Function GetQtyFatSNFStockQryGKCrystel(ByVal objFilter As clsStockRecoFilters, Optional ByVal ShowTankerNo As Boolean = False) As String
        Dim Qry As String = ""
        Dim LocTransQry As String = ""
        Dim StockUnionQry As String = ""
        If clsCommon.CompairString(objFilter.ReportType, "") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.ReportType, "None") = CompairStringResult.Equal Then
            objFilter.ReportType = "Daily"
        End If
        LocTransQry = " select 1 as Seq_No,'A' as Section,'Opening' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 2 as Seq_No,'A' as Section,'Purchase' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 3 as Seq_No,'A' as Section,'MCC Transfer Received' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 4 as Seq_No,'A' as Section,'Other In' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 5 as Seq_No,'B' as Section,'Sale' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 6 as Seq_No,'B' as Section,'MCC Transfer Out' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 7 as Seq_No,'B' as Section,'Plant Transfer Out' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 8 as Seq_No,'B' as Section,'Other Out' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 9 as Seq_No,'C' as Section,'Physical Stock' as Trans_Type " & Environment.NewLine

        '" select 9 as Seq_No,'B' as Section,'In Transit' as Trans_Type " & Environment.NewLine &
        '              " union all " & Environment.NewLine &
        '" union all " & Environment.NewLine &
        '              " select 8 as Seq_No,'B' as Section,'Inhouse Consumption' as Trans_Type " & Environment.NewLine &
        StockUnionQry = GetQtyFatSNFBaseQryGK(objFilter, True, ShowTankerNo) & Environment.NewLine &
                        " Union All " & GetQtyFatSNFBaseQryGK(objFilter, False, ShowTankerNo)
        Dim LocName As String = ""
        For Each code As clsCode In objFilter.arrLocation
            If objFilter.arrLocation.IndexOf(code) = 0 Then
                LocName = code.Desc
            Else
                LocName = LocName & "," & code.Desc
            End If
        Next
        Qry = "select ('" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MM-yyyy") & "'+ ' To ' + '" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MM-yyyy") & "') as Date_Range,'" & LocName & "' as Location,TransType.Seq_No,TransType.Section,TransType.Trans_Type,convert(Varchar,LossGain.Punching_Date,103) as Punching_Date,ROUND(sum(case when TransType.Trans_Type in ('In Transit','Physical Stock') then LossGain.NotInCalcQty  else LossGain.Qty end),2) as Qty,round(sum(case when TransType.Trans_Type in ('In Transit','Physical Stock') then LossGain.NotInCalcFAT_KG  else LossGain.FAT_KG end),3) as FAT_KG,round(sum(case when TransType.Trans_Type in ('In Transit','Physical Stock') then LossGain.NotInCalcSNF_KG  else LossGain.SNF_KG end),3) as SNF_KG,sum(case when TransType.Trans_Type in ('In Transit','Physical Stock') then LossGain.NotInCalcNetCost  else LossGain.Net_Cost end) as Net_Cost,sum(case when LossGain.Report_Type='Physical Stock' then LossGain.NotInCalcQty else 0 end ) as Physical_Stock_Qty,sum(case when LossGain.Report_Type='Physical Stock' then LossGain.NotInCalcFAT_KG else 0 end ) as Physical_Stock_FAT,sum(case when LossGain.Report_Type='Physical Stock' then LossGain.NotInCalcSNF_KG else 0 end ) as Physical_Stock_SNF,sum(case when LossGain.Report_Type='Physical Stock' then LossGain.NotInCalcNetCost else 0 end ) as Physical_Stock,LossGain.Tanker_No from (" & LocTransQry & ") as TransType left join (" & StockUnionQry & ") as LossGain on TransType.Trans_Type=LossGain.Report_Type group by TransType.Seq_No,TransType.Section,TransType.Trans_Type,LossGain.Tanker_No,LossGain.Punching_Date ORDER BY TransType.Seq_No,LossGain.Punching_Date "
        Return Qry
    End Function
    Public Shared Function GetQtyFatSNFStockQryGK(ByVal objFilter As clsStockRecoFilters, Optional ByVal ShowTankerNo As Boolean = False) As String
        Dim Qry As String = ""
        Dim LocTransQry As String = ""
        Dim StockUnionQry As String = ""
        If clsCommon.CompairString(objFilter.ReportType, "") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.ReportType, "None") = CompairStringResult.Equal Then
            objFilter.ReportType = "Daily"
        End If

        LocTransQry = " select Loc.Location_Code,Loc.Location_Desc,(case when Seq_No=0 then '" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "' end) as Trans_date,Seq_No,Trans_Type from (" & _
                      " select 0 as Seq_No,'Opening' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 1 as Seq_No,'Purchase' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 2 as Seq_No,'MCC Transfer Received' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 3 as Seq_No,'Other In' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 4 as Seq_No,'Sale' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 5 as Seq_No,'MCC Transfer Out' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 6 as Seq_No,'Plant Transfer Out' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 7 as Seq_No,'Inhouse Consumption' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 8 as Seq_No,'Other Out' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 9 as Seq_No,'In Transit' as Trans_Type " & Environment.NewLine &
                      " union all " & Environment.NewLine &
                      " select 10 as Seq_No,'Physical Stock' as Trans_Type " & Environment.NewLine &
                      " ) as TransType,TSPL_LOCATION_MASTER as Loc "

        ',dbo.ExplodeDates('" & clsCommon.GetPrintDate(objFilter.From_Date, "dd-MMM-yyyy") & "','" & clsCommon.GetPrintDate(objFilter.To_Date, "dd-MMM-yyyy") & "') as AllDate " & Environment.NewLine &

        LocTransQry += " where ((Loc.Location_Type IN ('Physical','Logical','Virtual') ) or (Loc.CSA_Type='Y'))"

        StockUnionQry = GetQtyFatSNFBaseQryGK(objFilter, True, ShowTankerNo) & Environment.NewLine &
                        " Union All " & GetQtyFatSNFBaseQryGK(objFilter, False, ShowTankerNo)
        Dim transDateSel As String = "convert(varchar,Trans_date,103) as Trans_Date"
        Dim transDateGroup As String = "Trans_date"
        Dim transDateOrder As String = "Trans_date"
        If clsCommon.CompairString(objFilter.ReportType, "Daily") = CompairStringResult.Equal Then
            transDateSel = "convert(varchar,Trans_date,103) as Trans_Date"
            transDateGroup = "Trans_date"
            transDateOrder = "Trans_date"
        ElseIf clsCommon.CompairString(objFilter.ReportType, "Monthly") = CompairStringResult.Equal Then
            transDateSel = " dateName(month,Trans_date)+ '-' + dateName(year,Trans_date) as Trans_Date"
            transDateGroup = "dateName(month,Trans_date),dateName(year,Trans_date)"
            transDateOrder = "(dateName(month,Trans_date) + '-' + dateName(year,Trans_date))"
        End If
        Qry = " select trans_Date as [Trans Date] ,Location_Code as [Location Code],Location_Desc as [Location Name],Item_Code as [Item Code]," & Environment.NewLine &
              " round((coalesce([Opening Qty],RunningBalanceQty-BalanceQty)),2) as [Opening Qty]," & Environment.NewLine &
              " round((coalesce([Opening FAT],RunningBalanceFAT-BalanceFat)),3) as [Opening FAT]," & Environment.NewLine &
              " round((coalesce([Opening SNF],RunningBalanceSNF-BalanceSNF)),3) as [Opening SNF]," & Environment.NewLine &
              " round([Purchase Qty Ltr],2) as [Purchase Qty Ltr]," & Environment.NewLine &
              " round([Purchase FAT Ltr],3) as [Purchase FAT Ltr]," & Environment.NewLine &
              " round([Purchase SNF Ltr],3) as [Purchase SNF Ltr]," & Environment.NewLine &
              " round([Purchase Qty],2) as [Purchase Qty]," & Environment.NewLine &
              " round([Purchase FAT],3) as [Purchase FAT]," & Environment.NewLine &
              " round([Purchase SNF],3) as [Purchase SNF]," & Environment.NewLine &
              " round([MCC Transfer Received Qty],2) as [MCC Transfer Received Qty]," & Environment.NewLine &
              " round([MCC Transfer Received FAT],3) as [MCC Transfer Received FAT]," & Environment.NewLine &
              " round([MCC Transfer Received SNF],3) as [MCC Transfer Received SNF]," & Environment.NewLine &
              " round([Other In Qty],2) as [Other In Qty]," & Environment.NewLine &
              " round([Other In FAT],3) as [Other In FAT]," & Environment.NewLine &
              " round([Other In SNF],3) as [Other In SNF]," & Environment.NewLine &
              " (round(coalesce([Purchase Qty],0),2)+round(coalesce([MCC Transfer Received Qty],0),2)+round(coalesce([Other In Qty],0),2)) as [Total In Qty]," & Environment.NewLine &
              " (round(coalesce([Purchase FAT],0),3)+round(coalesce([MCC Transfer Received FAT],0),3)+round(coalesce([Other In FAT],0),3)) as [Total In FAT]," & Environment.NewLine &
              " (round(coalesce([Purchase SNF],0),3)+round(coalesce([MCC Transfer Received SNF],0),3)+round(coalesce([Other In SNF],0),3)) as [Total In SNF]," & Environment.NewLine &
              " round([Sale Qty],2) as [Sale Qty]," & Environment.NewLine &
              " round([Sale FAT],3) as [Sale FAT]," & Environment.NewLine &
              " round([Sale SNF],3) as [Sale SNF]," & Environment.NewLine &
              " round([MCC Transfer Out Qty],2) as [MCC Transfer Out Qty]," & Environment.NewLine &
              " round([MCC Transfer Out FAT],3) as [MCC Transfer Out FAT]," & Environment.NewLine &
              " round([MCC Transfer Out SNF],3) as [MCC Transfer Out SNF]," & Environment.NewLine &
              " round([Plant Transfer Out Qty],2) as [Plant Transfer Out Qty]," & Environment.NewLine &
              " round([Plant Transfer Out FAT],3) as [Plant Transfer Out FAT]," & Environment.NewLine &
              " round([Plant Transfer Out SNF],3) as [Plant Transfer Out SNF]," & Environment.NewLine &
              " round([Inhouse Consumption Qty],2) as [Inhouse Consumption Qty]," & Environment.NewLine &
              " round([Inhouse Consumption FAT],3) as [Inhouse Consumption FAT]," & Environment.NewLine &
              " round([Inhouse Consumption SNF],3) as [Inhouse Consumption SNF]," & Environment.NewLine &
              " round([Other Out Qty],2) as [Other Out Qty]," & Environment.NewLine &
              " round([Other Out FAT],3) as [Other Out FAT]," & Environment.NewLine &
              " round([Other Out SNF],3) as [Other Out SNF]," & Environment.NewLine &
              " (round(coalesce([Sale Qty],0),2)+round(coalesce([MCC Transfer Out Qty],0),2)+round(coalesce([Plant Transfer Out Qty],0),2)+round(coalesce([Inhouse Consumption Qty],0),2)+round(coalesce([Other Out Qty],0),2)) as [Total Out Qty]," & Environment.NewLine &
              " (round(coalesce([Sale FAT],0),3)+round(coalesce([MCC Transfer Out FAT],0),3)+round(coalesce([Plant Transfer Out FAT],0),3)+round(coalesce([Inhouse Consumption FAT],0),3)+round(coalesce([Other Out FAT],0),3)) as [Total Out FAT]," & Environment.NewLine &
              " (round(coalesce([Sale SNF],0),3)+round(coalesce([MCC Transfer Out SNF],0),3)+round(coalesce([Plant Transfer Out SNF],0),3)+round(coalesce([Inhouse Consumption SNF],0),3)+round(coalesce([Other Out SNF],0),3)) as [Total Out SNF]," & Environment.NewLine &
              " round([In Transit Qty],2) as [In Transit Qty]," & Environment.NewLine &
              " round([In Transit FAT],3) as [In Transit FAT]," & Environment.NewLine &
              " round([In Transit SNF],3) as [In Transit SNF]," & Environment.NewLine &
              " round(RunningBalanceQty,2) as [Closing Qty],round(RunningBalanceFat,3) as [Closing FAT],round(RunningBalanceSNF,3) as [Closing SNF], " & _
              " round([Physical Stock Qty],2) as [Physical Stock Qty]," & Environment.NewLine &
              " round([Physical Stock FAT],3) as [Physical Stock FAT]," & Environment.NewLine &
              " round([Physical Stock SNF],3) as [Physical Stock SNF]," & Environment.NewLine &
              " (round(RunningBalanceQty,2)-round([Physical Stock Qty],2)) as [Loss/Gain Qty]," & Environment.NewLine &
              " (round(RunningBalanceFat,3)-round([Physical Stock FAT],3)) as [Loss/Gain FAT]," & Environment.NewLine &
              " (round(RunningBalanceSNF,3)-round([Physical Stock SNF],3)) as [Loss/Gain SNF] " & Environment.NewLine &
              " from ( " & Environment.NewLine &
              " SELECT *,(coalesce([Opening Qty],0)+coalesce([Purchase Qty],0)+coalesce([MCC Transfer Received Qty],0)+coalesce([Other In Qty],0)+coalesce([Sale Qty],0)+coalesce([Plant Transfer Out Qty],0)+coalesce([MCC Transfer Out Qty],0)+coalesce([Inhouse Consumption Qty],0)+coalesce([Other Out Qty],0)) as BalanceQty," & Environment.NewLine &
              " (coalesce([Opening FAT],0)+coalesce([Purchase FAT],0)+coalesce([MCC Transfer Received FAT],0)+coalesce([Other In FAT],0)+coalesce([Sale FAT],0)+coalesce([Plant Transfer Out FAT],0)+coalesce([MCC Transfer Out FAT],0)+coalesce([Inhouse Consumption FAT],0)+coalesce([Other Out FAT],0)) as BalanceFat," & Environment.NewLine &
              " (coalesce([Opening SNF],0)+coalesce([Purchase SNF],0)+coalesce([MCC Transfer Received SNF],0)+coalesce([Other In SNF],0)+coalesce([Sale SNF],0)+coalesce([Plant Transfer Out SNF],0)+coalesce([MCC Transfer Out SNF],0)+coalesce([Inhouse Consumption SNF],0)+coalesce([Other Out SNF],0)) as BalanceSNF," & Environment.NewLine &
              " sum(coalesce([Opening Qty],0)+coalesce([Purchase Qty],0)+coalesce([MCC Transfer Received Qty],0)+coalesce([Other In Qty],0)+coalesce([Sale Qty],0)+coalesce([MCC Transfer Out Qty],0)+coalesce([Plant Transfer Out Qty],0)+coalesce([Inhouse Consumption Qty],0)+coalesce([Other Out Qty],0)) over (Partition by Location_Code,Item_Code order by Location_Code,Tr_Id) as RunningBalanceQty," & Environment.NewLine &
              " sum(coalesce([Opening FAT],0)+coalesce([Purchase FAT],0)+coalesce([MCC Transfer Received FAT],0)+coalesce([Other In FAT],0)+coalesce([Sale FAT],0)+coalesce([MCC Transfer Out FAT],0)+coalesce([Plant Transfer Out FAT],0)+coalesce([Inhouse Consumption FAT],0)+coalesce([Other Out FAT],0)) over (Partition by Location_Code,Item_Code order by Location_Code,Tr_Id) as RunningBalanceFat," & Environment.NewLine &
              " sum(coalesce([Opening SNF],0)+coalesce([Purchase SNF],0)+coalesce([MCC Transfer Received SNF],0)+coalesce([Other In SNF],0)+coalesce([Sale SNF],0)+coalesce([MCC Transfer Out SNF],0)+coalesce([Plant Transfer Out SNF],0)+coalesce([Inhouse Consumption SNF],0)+coalesce([Other Out SNF],0)) over (Partition by Location_Code,Item_Code order by Location_Code,Tr_Id) as RunningBalanceSNF " & Environment.NewLine &
              " FROM ( " & Environment.NewLine &
              " select Item_Code,Location_Code,Location_Desc," & transDateSel & ", " & Environment.NewLine &
              " SUM([Opening Qty]) as [Opening Qty]," & Environment.NewLine &
              " SUM([Opening FAT]) AS [Opening FAT]," & Environment.NewLine &
              " SUM([Opening SNF]) AS [Opening SNF], " & Environment.NewLine &
              " SUM([Purchase Qty Ltr]) as [Purchase Qty Ltr]," & Environment.NewLine &
              " SUM([Purchase FAT Ltr]) AS [Purchase FAT Ltr], " & Environment.NewLine &
              " SUM([Purchase SNF Ltr]) AS [Purchase SNF Ltr], " & Environment.NewLine &
              " SUM([Purchase Qty]) as [Purchase Qty]," & Environment.NewLine &
              " SUM([Purchase FAT]) AS [Purchase FAT], " & Environment.NewLine &
              " SUM([Purchase SNF]) AS [Purchase SNF], " & Environment.NewLine &
              " SUM([MCC Transfer Received Qty]) AS [MCC Transfer Received Qty], " & Environment.NewLine &
              " SUM([MCC Transfer Received FAT]) AS [MCC Transfer Received FAT], " & Environment.NewLine &
              " SUM([MCC Transfer Received SNF]) AS [MCC Transfer Received SNF], " & Environment.NewLine &
              " SUM([Other In Qty]) AS [Other In Qty], " & Environment.NewLine &
              " SUM([Other In FAT]) AS [Other In FAT], " & Environment.NewLine &
              " SUM([Other In SNF]) AS [Other In SNF], " & Environment.NewLine &
              " SUM([Sale Qty]) AS [Sale Qty], " & Environment.NewLine &
              " SUM([Sale FAT]) AS [Sale FAT], " & Environment.NewLine &
              " SUM([Sale SNF]) AS [Sale SNF], " & Environment.NewLine &
              " SUM([MCC Transfer Out Qty]) AS [MCC Transfer Out Qty], " & Environment.NewLine &
              " SUM([MCC Transfer Out FAT]) AS [MCC Transfer Out FAT], " & Environment.NewLine &
              " SUM([MCC Transfer Out SNF]) AS [MCC Transfer Out SNF], " & Environment.NewLine &
              " SUM([Plant Transfer Out Qty]) AS [Plant Transfer Out Qty], " & Environment.NewLine &
              " SUM([Plant Transfer Out FAT]) AS [Plant Transfer Out FAT], " & Environment.NewLine &
              " SUM([Plant Transfer Out SNF]) AS [Plant Transfer Out SNF], " & Environment.NewLine &
              " SUM([Inhouse Consumption Qty]) AS [Inhouse Consumption Qty], " & Environment.NewLine &
              " SUM([Inhouse Consumption FAT]) AS [Inhouse Consumption FAT], " & Environment.NewLine &
              " SUM([Inhouse Consumption SNF]) AS [Inhouse Consumption SNF]," & Environment.NewLine &
              " SUM([Other Out Qty]) AS [Other Out Qty], " & Environment.NewLine &
              " SUM([Other Out FAT]) AS [Other Out FAT], " & Environment.NewLine &
              " SUM([Other Out SNF]) AS [Other Out SNF], " & Environment.NewLine &
              " SUM([In Transit Qty]) AS [In Transit Qty], " & Environment.NewLine &
              " SUM([In Transit FAT]) AS [In Transit FAT], " & Environment.NewLine &
              " SUM([In Transit SNF]) AS [In Transit SNF], " & Environment.NewLine &
              " SUM([Physical Stock Qty]) AS [Physical Stock Qty], " & Environment.NewLine &
              " SUM([Physical Stock FAT]) AS [Physical Stock FAT], " & Environment.NewLine &
              " SUM([Physical Stock SNF]) AS [Physical Stock SNF], " & Environment.NewLine &
              " row_number() over (partition by Item_Code,Location_Code order by Item_Code,Location_Code," & transDateOrder & ") as Tr_id from ( " & Environment.NewLine &
              " select Loc_Trans.Location_Code,FatStockFinal.Item_Code,Loc_Trans.Location_Desc,Loc_Trans.Trans_date,Loc_Trans.Trans_Type+' FAT' as Trans_Type_Fat,Loc_Trans.Trans_Type+' FAT Ltr' as Trans_Type_Fat_Ltr,Loc_Trans.Trans_Type+' Qty' as Trans_Type_Qty,Loc_Trans.Trans_Type+' Qty Ltr' as Trans_Type_Qty_Ltr, " & Environment.NewLine &
              " Loc_Trans.Trans_Type+' SNF' as Trans_Type_SNF,Loc_Trans.Trans_Type+' SNF Ltr' as Trans_Type_SNF_Ltr,max(case when Loc_Trans.Trans_Type in ('In Transit','Physical Stock') then FatStockFinal.NotInCalcQty  else FatStockFinal.Qty end) as Qty,max(FatStockFinal.QtyLtr) as QtyLtr, " & Environment.NewLine &
              " max(case when Loc_Trans.Trans_Type in ('In Transit','Physical Stock') then FatStockFinal.NotInCalcFAT_KG else FatStockFinal.FAT_KG end) as FAT_KG,max(FatStockFinal.FAT_Ltr) as FAT_Ltr,max(case when Loc_Trans.Trans_Type in ('In Transit','Physical Stock') then FatStockFinal.NotInCalcSNF_KG else FatStockFinal.SNF_KG end) as SNF_KG,max(FatStockFinal.SNF_Ltr) as SNF_Ltr from (" & Environment.NewLine &
              " " & LocTransQry & " ) as Loc_Trans " & Environment.NewLine &
              " inner join ( " & Environment.NewLine &
              " " & StockUnionQry & " ) as FatStockFinal on Loc_Trans.Location_Code=FatStockFinal.Location_Code and Loc_Trans.Trans_Type=FatStockFinal.Report_Type and Loc_Trans.Trans_date=FatStockFinal.Punching_Date " & Environment.NewLine &
              " group by Loc_Trans.Location_Code,FatStockFinal.Item_Code,Loc_Trans.Location_Desc,Loc_Trans.Trans_date,Loc_Trans.Trans_Type,Loc_Trans.Seq_No " & Environment.NewLine &
              " ) AS FatStockOuter " & Environment.NewLine &
              " PIVOT " & Environment.NewLine &
              " (  " & Environment.NewLine &
              " max(Qty) " & Environment.NewLine &
              " FOR Trans_Type_Qty " & Environment.NewLine &
              " IN ([Opening Qty], " & Environment.NewLine &
              " [Purchase Qty], " & Environment.NewLine &
              " [MCC Transfer Received Qty], " & Environment.NewLine &
              " [Other In Qty], " & Environment.NewLine &
              " [Sale Qty], " & Environment.NewLine &
              " [MCC Transfer Out Qty], " & Environment.NewLine &
              " [Plant Transfer Out Qty], " & Environment.NewLine &
              " [Inhouse Consumption Qty], " & Environment.NewLine &
              " [Other Out Qty], " & Environment.NewLine &
              " [In Transit Qty], " & Environment.NewLine &
              " [Physical Stock Qty]) " & Environment.NewLine &
              " ) AS PIVQty " & Environment.NewLine &
              " PIVOT " & Environment.NewLine &
              " (  " & Environment.NewLine &
              " max(QtyLtr) " & Environment.NewLine &
              " FOR Trans_Type_Qty_Ltr " & Environment.NewLine &
              " IN ([Purchase Qty Ltr]) " & Environment.NewLine &
              " ) AS PIVQtyLtr " & Environment.NewLine &
              " PIVOT " & Environment.NewLine &
              " (  " & Environment.NewLine &
              " max(Fat_Ltr) " & Environment.NewLine &
              " FOR Trans_Type_Fat_Ltr " & Environment.NewLine &
              " IN ( [Purchase Fat Ltr] ) " & Environment.NewLine &
              " ) AS PIVFatLtr " & Environment.NewLine &
              " PIVOT " & Environment.NewLine &
              " (  " & Environment.NewLine &
              " max(SNF_Ltr) " & Environment.NewLine &
              " FOR Trans_Type_SNF_Ltr " & Environment.NewLine &
              " IN ( [Purchase SNF Ltr] ) " & Environment.NewLine &
              " ) AS PIVSNFLtr " & Environment.NewLine &
              " PIVOT " & Environment.NewLine &
              " (  " & Environment.NewLine &
              " max(FAT_KG) " & Environment.NewLine &
              " FOR Trans_Type_Fat " & Environment.NewLine &
              " IN ([Opening FAT], " & Environment.NewLine &
              " [Purchase FAT], " & Environment.NewLine &
              " [MCC Transfer Received FAT], " & Environment.NewLine &
              " [Other In FAT], " & Environment.NewLine &
              " [Sale FAT], " & Environment.NewLine &
              " [MCC Transfer Out FAT], " & Environment.NewLine &
              " [Plant Transfer Out FAT], " & Environment.NewLine &
              " [Inhouse Consumption FAT], " & Environment.NewLine &
              " [Other Out FAT], " & Environment.NewLine &
              " [In Transit FAT], " & Environment.NewLine &
              " [Physical Stock FAT]) " & Environment.NewLine &
              " ) AS PIVF " & Environment.NewLine &
              " PIVOT " & Environment.NewLine &
              " (  " & Environment.NewLine &
              " max(SNF_KG) " & Environment.NewLine &
              " FOR Trans_Type_SNF " & Environment.NewLine &
              " IN ([Opening SNF], " & Environment.NewLine &
              " [Purchase SNF], " & Environment.NewLine &
              " [MCC Transfer Received SNF], " & Environment.NewLine &
              " [Other In SNF], " & Environment.NewLine &
              " [Sale SNF], " & Environment.NewLine &
              " [MCC Transfer Out SNF], " & Environment.NewLine &
              " [Plant Transfer Out SNF], " & Environment.NewLine &
              " [Inhouse Consumption SNF], " & Environment.NewLine &
              " [Other Out SNF], " & Environment.NewLine &
              " [In Transit SNF], " & Environment.NewLine &
              " [Physical Stock SNF]) " & Environment.NewLine &
              " ) AS PIVS " & Environment.NewLine &
              " GROUP BY Item_Code,Location_Code,Location_Desc," & transDateGroup & " " & Environment.NewLine &
              " )AS FATSNFtockFinal) as Outermost "
        If clsCommon.CompairString(objFilter.ReportType, "Summary") = CompairStringResult.Equal Then
            Qry = "select Seq_No as [Seq No],Report_Type as [Report Type],round(sum(Qty),2) as [Quantity],round(sum(Fat_KG),3) as [FAT Kg],round(sum(SNF_KG),3) as [SNF KG] from (" & Environment.NewLine &
              " select Loc_Trans.Location_Code,Loc_Trans.Location_Desc,Loc_Trans.Trans_date,Loc_Trans.Seq_No,Loc_Trans.Trans_Type as Report_Type,FatStockFinal.Item_Code,FatStockFinal.Stock_UOM,FatStockFinal.Qty,FatStockFinal.FAT_KG,FatStockFinal.SNF_KG from (" & LocTransQry & " ) as Loc_Trans " & Environment.NewLine &
              " left join ( " & Environment.NewLine &
              " " & StockUnionQry & " ) as FatStockFinal on Loc_Trans.Location_Code=FatStockFinal.Location_Code and Loc_Trans.Trans_Type=FatStockFinal.Report_Type "

            ' and Loc_Trans.Trans_date=FatStockFinal.Punching_Date " & Environment.NewLine &
            Qry += " ) AS FatStockOuter where Report_Type not in ('In Transit','Physical Stock') group by Seq_No,Report_Type order by Seq_No  "
        End If
        '' " group by Loc_Trans.Location_Code,FatStockFinal.Item_Code,Loc_Trans.Location_Desc,Loc_Trans.Trans_date,Loc_Trans.Trans_Type,Loc_Trans.Seq_No " & Environment.NewLine &

        Return Qry
    End Function
    Public Shared Function GetQtyFatSNFStockGKDT(ByVal objFilter As clsStockRecoFilters, Optional ByVal ShowTankerNo As Boolean = False) As DataTable
        Dim qry As String = GetQtyFatSNFStockQryGK(objFilter, ShowTankerNo)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
    Public Shared Function GetQtyFatSNFStockGKDTCrystel(ByVal objFilter As clsStockRecoFilters, Optional ByVal ShowTankerNo As Boolean = False) As DataTable
        Dim qry As String = GetQtyFatSNFStockQryGKCrystel(objFilter, ShowTankerNo)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
    Public Shared Function GetQryStockAgeing(ByVal objFilter As clsStockAgeingFilters, ByVal isGITLocation As Boolean, ByVal isShelfLifeWise As Boolean) As String
        Dim qry As String = ""
        Dim strCategoryTable As String = ""
        Dim strCodeColumn As String = ""
        Dim strCodeDescColumn As String = ""
        Try
            Dim arrOutput As New ArrayList
            arrOutput = GetBaseQryStockAgeing(objFilter, isGITLocation, isShelfLifeWise)
            If arrOutput Is Nothing OrElse arrOutput.Count < 4 Then
                Throw New Exception("error in base query generation!")
            End If
            qry = arrOutput(0)
            strCategoryTable = arrOutput(1)
            strCodeColumn = arrOutput(2)
            strCodeDescColumn = arrOutput(3)


            Dim bucket1 As Decimal = 0
            Dim bucket2 As Decimal = 0
            Dim bucket3 As Decimal = 0
            Dim bucket4 As Decimal = 0
            bucket1 = clsCommon.myCdbl(objFilter.arrAgeingBucket(0))
            bucket2 = clsCommon.myCdbl(objFilter.arrAgeingBucket(1))
            bucket3 = clsCommon.myCdbl(objFilter.arrAgeingBucket(2))
            bucket4 = clsCommon.myCdbl(objFilter.arrAgeingBucket(3))
            If bucket1 > bucket2 Then
                Throw New Exception("Ageing Bucket 1 Value must be less than Bucket 2 value")
            End If
            If bucket2 > bucket3 Then
                Throw New Exception("Ageing Bucket 2 Value must be less than Bucket 3 value")
            End If
            If bucket3 > bucket4 Then
                Throw New Exception("Ageing Bucket 3 Value must be less than Bucket 4 value")
            End If

            Dim bucket11 As Decimal = bucket1 + 1
            Dim bucket21 As Decimal = bucket2 + 1
            Dim bucket31 As Decimal = bucket3 + 1
            Dim StockCols As String = ""

            StockCols = "" & If(clsCommon.myLen(strCategoryTable) > 0, strCodeColumn & "," & strCodeDescColumn & ",", "") & " shelf_life ,Item_Code as [Item Code],Item_Desc as [Item Name],Location_Code,summary.[Location Desc] "
            If isShelfLifeWise = True AndAlso clsCommon.CompairString(objFilter.ReportType, "Detail") = CompairStringResult.Equal Then
                StockCols += " ,Trans_Type,Trans_Name,Document_No,Punching_date,Document_Date"
            End If

            StockCols += " ,Stock_Uom, sum(Current_Qty) as Current_Qty,sum(Current_Value) as Current_Value," & _
                        " sum([0-" & bucket1 & " Qty]) as [0-" & bucket1 & " Qty], " & _
                        " sum([0-" & bucket1 & " Value]) as [0-" & bucket1 & " Value]," & _
                        " sum([" & bucket11 & "-" & bucket2 & " Qty]) as [" & bucket11 & "-" & bucket2 & " Qty], " & _
                        " sum([" & bucket11 & "-" & bucket2 & " Value]) as [" & bucket11 & "-" & bucket2 & " Value], " & _
                        " sum([" & bucket21 & "-" & bucket3 & " Qty]) as [" & bucket21 & "-" & bucket3 & " Qty], " & _
                        " sum([" & bucket21 & "-" & bucket3 & " Value]) as [" & bucket21 & "-" & bucket3 & " Value], " & _
                        " sum([" & bucket31 & "-" & bucket4 & " Qty]) as [" & bucket31 & "-" & bucket4 & " Qty], " & _
                        " sum([" & bucket31 & "-" & bucket4 & " Value]) as [" & bucket31 & "-" & bucket4 & " Value], " & _
                        " sum([Over " & bucket4 & " Qty]) as [Over " & bucket4 & " Qty], " & _
                        " sum([Over " & bucket4 & " Value]) as [Over " & bucket4 & " Value] "
            If clsCommon.CompairString(objFilter.ReportType, "Summary") = CompairStringResult.Equal Then
                If clsCommon.CompairString(objFilter.AgeingColumns, "FAT-SNF") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.AgeingColumns, "All") = CompairStringResult.Equal Then
                    StockCols = StockCols & " ,sum(Current_Fat_KG) as Current_Fat_KG,sum(Current_Fat_Value) as Current_Fat_Value, " & _
                                                " sum(Current_SNF_KG) as Current_SNF_KG,sum(Current_SNF_Value) as Current_SNF_Value, " & _
                                                " sum([0-" & bucket1 & " Fat KG]) as [0-" & bucket1 & " Fat KG], " & _
                                                " sum([0-" & bucket1 & " Fat Value]) as [0-" & bucket1 & " Fat Value], " & _
                                                " sum([0-" & bucket1 & " SNF KG]) as [0-" & bucket1 & " SNF KG], " & _
                                                " sum([0-" & bucket1 & " SNF Value]) as [0-" & bucket1 & " SNF Value], " & _
                                                " sum([" & bucket11 & "-" & bucket2 & " Fat KG]) as [" & bucket11 & "-" & bucket2 & " Fat KG], " & _
                                                " sum([" & bucket11 & "-" & bucket2 & " Fat Value]) as [" & bucket11 & "-" & bucket2 & " Fat Value], " & _
                                                " sum([" & bucket11 & "-" & bucket2 & " SNF KG]) as [" & bucket11 & "-" & bucket2 & " SNF KG], " & _
                                                " sum([" & bucket11 & "-" & bucket2 & " SNF Value]) as [" & bucket11 & "-" & bucket2 & " SNF Value], " & _
                                                " sum([" & bucket21 & "-" & bucket3 & " Fat KG]) as [" & bucket21 & "-" & bucket3 & " Fat KG], " & _
                                                " sum([" & bucket21 & "-" & bucket3 & " Fat Value]) as [" & bucket21 & "-" & bucket3 & " Fat Value], " & _
                                                " sum([" & bucket21 & "-" & bucket3 & " SNF KG]) as [" & bucket21 & "-" & bucket3 & " SNF KG], " & _
                                                " sum([" & bucket21 & "-" & bucket3 & " SNF Value]) as [" & bucket21 & "-" & bucket3 & " SNF Value], " & _
                                                " sum([" & bucket31 & "-" & bucket4 & " Fat KG]) as [" & bucket31 & "-" & bucket4 & " Fat KG], " & _
                                                " sum([" & bucket31 & "-" & bucket4 & " Fat Value]) as [" & bucket31 & "-" & bucket4 & " Fat Value], " & _
                                                " sum([" & bucket31 & "-" & bucket4 & " SNF KG]) as [" & bucket31 & "-" & bucket4 & " SNF KG], " & _
                                                " sum([" & bucket31 & "-" & bucket4 & " SNF Value]) as [" & bucket31 & "-" & bucket4 & " SNF Value], " & _
                                                " sum([Over " & bucket4 & " Fat KG]) as [Over " & bucket4 & " Fat KG]," & _
                                                " sum([Over " & bucket4 & " Fat Value]) as [Over " & bucket4 & " Fat Value], " & _
                                                " sum([Over " & bucket4 & " SNF KG]) as [Over " & bucket4 & " SNF KG], " & _
                                                " sum([Over " & bucket4 & " SNF Value]) as [Over " & bucket4 & " SNF Value]"
                End If
                qry = " select " & If(clsCommon.myLen(strCategoryTable) > 0, strCodeColumn & "," & strCodeDescColumn & ",", "") & " Item_Type," & StockCols & " from (" & qry & ") Summary group by " & If(clsCommon.myLen(strCategoryTable) > 0, strCodeColumn & "," & strCodeDescColumn & ",", "") & " Item_Type,Item_Code,Item_Desc, Stock_Uom,Location_Code,summary.[Location Desc],shelf_life"
            ElseIf clsCommon.CompairString(objFilter.ReportType, "Detail") = CompairStringResult.Equal Then
                If isShelfLifeWise = True Then
                    qry = " select " & If(clsCommon.myLen(strCategoryTable) > 0, strCodeColumn & "," & strCodeDescColumn & ",", "") & " Item_Type," & StockCols & " from (" & qry & ") Summary group by " & If(clsCommon.myLen(strCategoryTable) > 0, strCodeColumn & "," & strCodeDescColumn & ",", "") & " Item_Type,Item_Code,Item_Desc, Stock_Uom,Location_Code,summary.[Location Desc],shelf_life,Trans_Type,Trans_Name,Document_No,Punching_date,Document_Date"
                End If
                qry = qry & " order by Location_Code,Item_Code,Punching_Date,Document_No "
            Else
                qry = qry & " order by Location_Code,Item_Code,Punching_Date "
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return qry
    End Function
    Public Shared Function GetBaseQryStockAgeing(ByVal objFilter As clsStockAgeingFilters, ByVal isGITLocation As Boolean, ByVal isShelfLifeWise As Boolean) As ArrayList
        'Ticket No-TEC/18/06/19-000546, add Variable-strCodeColumnMaxWithTable,Item Type Filter
        'Ticket No-BHA/03/07/19-000922, Add shelf life,remove Category,add Loaction in summary report
        Dim arrOutput As New ArrayList
        Dim qry As String = ""
        Try
            Dim ItemFilter As String = ""
            Dim locFilter As String = ""
            If Not objFilter.arrItem Is Nothing AndAlso objFilter.arrItem.Count > 0 Then
                ItemFilter = "(" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
            End If
            'If Not objFilter.arrLoc Is Nothing AndAlso objFilter.arrLoc.Count > 0 Then
            '    locFilter = "(" & clsCommon.GetMulcallString(objFilter.arrLoc) & ")"
            'End If
            If objFilter.arrAgeingBucket Is Nothing OrElse objFilter.arrAgeingBucket.Count <= 0 Then
                Throw New Exception("Ageing Bucket can not be blank")
            ElseIf objFilter.arrAgeingBucket.Count < 4 Then
                Throw New Exception("Ageing Bucket size must not be less than 4")
            ElseIf objFilter.arrAgeingBucket.Count > 4 Then
                Throw New Exception("Ageing Bucket size must not be greater than 4")
            End If
            Dim bucket1 As Decimal = 0
            Dim bucket2 As Decimal = 0
            Dim bucket3 As Decimal = 0
            Dim bucket4 As Decimal = 0
            bucket1 = clsCommon.myCdbl(objFilter.arrAgeingBucket(0))
            bucket2 = clsCommon.myCdbl(objFilter.arrAgeingBucket(1))
            bucket3 = clsCommon.myCdbl(objFilter.arrAgeingBucket(2))
            bucket4 = clsCommon.myCdbl(objFilter.arrAgeingBucket(3))
            If bucket1 > bucket2 Then
                Throw New Exception("Ageing Bucket 1 Value must be less than Bucket 2 value")
            End If
            If bucket2 > bucket3 Then
                Throw New Exception("Ageing Bucket 2 Value must be less than Bucket 3 value")
            End If
            If bucket3 > bucket4 Then
                Throw New Exception("Ageing Bucket 3 Value must be less than Bucket 4 value")
            End If

            Dim bucket11 As Decimal = bucket1 + 1
            Dim bucket21 As Decimal = bucket2 + 1
            Dim bucket31 As Decimal = bucket3 + 1
            'Dim LocationFirstTime As Integer = 0
            'Dim LocationAddress As String = String.Empty
            'Dim strWhrCatg As String = ""
            Dim strCodeColumn As String = ""
            Dim strCodeColumnMax As String = ""
            Dim strCodeColumnMaxWithTable As String = ""
            Dim strCodeDescColumn As String = ""
            Dim strCodeDescColumnMax As String = ""

            Dim strCodeColumnSelect As String = ""
            Dim strCodeDescColumnSelect As String = ""

            Dim strCodeColumnNull As String = ""
            Dim strCodeDescColumnNull As String = ""

            Dim dtCategory As DataTable = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+'DESC' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
            Dim strCategoryTable As String = ""
            'If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
            '    For ii As Integer = 0 To dtCategory.Rows.Count - 1
            '        If ii <> 0 Then
            '            strCodeColumn += ","
            '            strCodeColumnMax += ","
            '            strCodeColumnMaxWithTable += ","
            '            strCodeDescColumn += ","
            '            strCodeDescColumnMax += ","

            '            strCodeColumnSelect += ","
            '            strCodeDescColumnSelect += ","

            '            strCodeColumnNull += ","
            '            strCodeDescColumnNull += ","
            '        End If
            '        strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
            '        strCodeColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
            '        strCodeColumnMaxWithTable += "max(VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
            '        strCodeDescColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
            '        strCodeDescColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]"

            '        strCodeColumnSelect += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
            '        strCodeDescColumnSelect += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"

            '        strCodeColumnNull += "null as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
            '        strCodeDescColumnNull += "null as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
            '    Next
            '    strCategoryTable = "select Item_Code," + strCodeColumnMax + "," + strCodeDescColumnMax + "  from (" + Environment.NewLine & _
            '    " select * from ( " + Environment.NewLine & _
            '    " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " + Environment.NewLine & _
            '    " ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc " + Environment.NewLine & _
            '    " ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " + Environment.NewLine & _
            '    " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc " + Environment.NewLine & _
            '    " from  TSPL_ITEM_MASTER  " + Environment.NewLine & _
            '    " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " + Environment.NewLine & _
            '    " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values" + Environment.NewLine & _
            '    " where 2=2 " + Environment.NewLine & _
            '    " )xx" + Environment.NewLine & _
            '    " Pivot " + Environment.NewLine & _
            '    " ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + strCodeColumn + ")" + Environment.NewLine & _
            '    " ) Pivt" + Environment.NewLine & _
            '    " Pivot " + Environment.NewLine & _
            '    " (" + Environment.NewLine & _
            '    " max(Category_Value_Desc) for Item_Category_CodeDesc in (" + strCodeDescColumn + ")" + Environment.NewLine & _
            '    " ) Pivt1 " + Environment.NewLine & _
            '    " ) xxx group by Item_Code "
            '    ''End of Category Table start now.
            'End If

            If objFilter.SelectLocation = True Then
                Dim IsApplicable As Boolean = False
                For ii As Integer = 0 To objFilter.arrLocation.Count - 1
                    If clsCommon.myCBool(objFilter.arrLocation(ii).Sel) Then
                        'LocationFirstTime += 1
                        'If LocationFirstTime = 1 Then
                        '    LocationAddress = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress] from TSPL_LOCATION_MASTER where Location_Code= '" & clsCommon.myCstr(objFilter.arrLocation(ii).Code) & "'")
                        'End If
                        If IsApplicable Then
                            locFilter += " Or "
                        End If
                        locFilter += " ((case when Loc.Is_Section='N' and Loc.Is_Sub_Location='N' then Inv.Location_Code else Loc.Main_Location_Code end) = '" + clsCommon.myCstr(objFilter.arrLocation(ii).Code) + "') "
                        IsApplicable = True
                        Dim arr As Dictionary(Of String, Object) = objFilter.arrLocation(ii).arrOut
                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            locFilter += " and Inv.Location_Code in ("
                            Dim isFirstTime As Boolean = True
                            For Each strInn As String In arr.Keys
                                If Not isFirstTime Then
                                    locFilter += ","
                                End If
                                locFilter += "'" + strInn + "'"
                                isFirstTime = False
                            Next
                            locFilter += ")"
                        End If
                    End If
                Next
                If Not IsApplicable Then
                    Throw New Exception("Please select at least one location")
                End If
            Else
                'If clsCommon.CompairString(objFilter.FORMTYPE, clsUserMgtCode.stockRecoNewJR) = CompairStringResult.Equal Then
                '    strWhrCatg += "  (Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where len(coalesce(Jobwork_Vendor,''))>0 and Is_Sub_Location='Y'))"
                'End If
            End If
            'Dim bucket11 As Decimal = bucket1 + 1
            'Dim bucket21 As Decimal = bucket2 + 1
            'Dim bucket31 As Decimal = bucket3 + 1
            '' set other filter
            Dim otherFilter As String = ""
            If clsCommon.myLen(objFilter.arrItemType) > 0 Then
                otherFilter = otherFilter & " and Item.Item_Type in (" & clsCommon.GetMulcallString(objFilter.arrItemType) & ")"
            End If
            If clsCommon.myLen(objFilter.InventoryType) > 0 Then
                If clsCommon.CompairString(objFilter.InventoryType, "MI") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.InventoryType, "MP") = CompairStringResult.Equal Then
                    otherFilter = otherFilter & " and Item.Product_Type ='" & objFilter.InventoryType & "'"
                ElseIf objFilter.InventoryType = "Other" Then
                    otherFilter = otherFilter & " and Item.Product_Type not in ('MI','MP')"
                ElseIf objFilter.InventoryType = "All" Then
                    '' nothing
                End If
            End If
            If clsCommon.myLen(objFilter.Item_Status) > 0 Then
                If clsCommon.CompairString(objFilter.Item_Status, "Active") = CompairStringResult.Equal Then
                    otherFilter = otherFilter & " and Item.Active =1 "
                ElseIf clsCommon.CompairString(objFilter.Item_Status, "Inactive") = CompairStringResult.Equal Then
                    otherFilter = otherFilter & " and Item.Active =0 "
                ElseIf objFilter.InventoryType = "All" Then
                    '' nothing
                End If
            End If
            Dim CutOffDate As String = clsCommon.GetPrintDate(objFilter.CutOffDate, "dd-MMM-yyyy")
            Dim AsOnDate As String = clsCommon.GetPrintDate(objFilter.AsOnDate, "dd-MMM-yyyy")
            Dim slab1_Date As String = clsCommon.GetPrintDate(objFilter.AsOnDate.AddDays(-bucket1 + 1), "dd-MMM-yyyy")
            Dim slab2_Date As String = clsCommon.GetPrintDate(objFilter.AsOnDate.AddDays(-bucket2 + 1), "dd-MMM-yyyy")
            Dim slab3_Date As String = clsCommon.GetPrintDate(objFilter.AsOnDate.AddDays(-bucket3 + 1), "dd-MMM-yyyy")
            Dim slab4_Date As String = clsCommon.GetPrintDate(objFilter.AsOnDate.AddDays(-bucket4 + 1), "dd-MMM-yyyy")
            Dim QryOPFatSNF As String = ""
            '' change in query by Panch Raj against ticket no-UDL/21/05/18-000168
            If clsCommon.CompairString(objFilter.AgeingColumns, "FAT-SNF") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.AgeingColumns, "All") = CompairStringResult.Equal Then
                QryOPFatSNF = " ,sum((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*Inv.Fat_KG) AS Current_Fat_KG,sum((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*Inv.Fat_Amt) AS Current_Fat_Value, " & Environment.NewLine & _
                  " sum((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*Inv.SNF_KG) AS Current_SNF_KG,sum((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*Inv.SNF_Amt) AS Current_SNF_Value, " & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab1_Date & "' and CAST(Inv.Punching_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_KG) as [0-" & bucket1 & " Fat KG], " & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab1_Date & "' and CAST(Inv.Punching_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_Amt) as [0-" & bucket1 & " Fat Value], " & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab1_Date & "' and CAST(Inv.Punching_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_KG) as [0-" & bucket1 & " SNF KG], " & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab1_Date & "' and CAST(Inv.Punching_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_Amt) as [0-" & bucket1 & " SNF Value], " & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab2_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_KG) as [" & bucket11 & "-" & bucket2 & " Fat KG], " & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab2_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_Amt) as [" & bucket11 & "-" & bucket2 & " Fat Value], " & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab2_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_KG) as [" & bucket11 & "-" & bucket2 & " SNF KG], " & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab2_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_Amt) as [" & bucket11 & "-" & bucket2 & " SNF Value], " & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab3_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_KG) as [" & bucket21 & "-" & bucket3 & " Fat KG]," & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab3_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_Amt) as [" & bucket21 & "-" & bucket3 & " Fat Value], " & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab3_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_KG) as [" & bucket21 & "-" & bucket3 & " SNF KG]," & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab3_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_Amt) as [" & bucket21 & "-" & bucket3 & " SNF Value], " & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab4_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_KG) as [" & bucket31 & "-" & bucket4 & " Fat KG]," & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab4_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_Amt) as [" & bucket31 & "-" & bucket4 & " Fat Value], " & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab4_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_KG) as [" & bucket31 & "-" & bucket4 & " SNF KG]," & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab4_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_Amt) as [" & bucket31 & "-" & bucket4 & " SNF Value], " & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_KG) as [Over " & bucket4 & " Fat KG], " & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_Amt) as [Over " & bucket4 & " Fat Value], " & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_KG) as [Over " & bucket4 & " SNF KG], " & Environment.NewLine & _
                  " sum((CASE when CAST(Inv.Punching_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_Amt) as [Over " & bucket4 & " SNF Value] "

            End If

            Dim qryFatSNFMilk As String = ""
            If clsCommon.CompairString(objFilter.AgeingColumns, "FAT-SNF") = CompairStringResult.Equal OrElse clsCommon.CompairString(objFilter.AgeingColumns, "All") = CompairStringResult.Equal Then
                qryFatSNFMilk = " ,((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*Inv.Fat_KG) AS Current_Fat_KG,((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*Inv.Fat_Amt) AS Current_Fat_Value," & Environment.NewLine & _
                  " ((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*Inv.SNF_KG) AS Current_SNF_KG,((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*Inv.SNF_Amt) AS Current_SNF_Value," & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab1_Date & "' and CAST(Inv.Punching_Date AS DATE)<='" & AsOnDate & "'  " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_KG) as [0-" & bucket1 & " Fat KG]," & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab1_Date & "' and CAST(Inv.Punching_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_Amt) as [0-" & bucket1 & " Fat Value]," & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab1_Date & "' and CAST(Inv.Punching_Date AS DATE)<='" & AsOnDate & "'  " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_KG) as [0-" & bucket1 & " SNF KG]," & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab1_Date & "' and CAST(Inv.Punching_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_Amt) as [0-" & bucket1 & " SNF Value]," & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab2_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_KG) as [" & bucket11 & "-" & bucket2 & " Fat KG], " & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab2_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_Amt) as [" & bucket11 & "-" & bucket2 & " Fat Value], " & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab2_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_KG) as [" & bucket11 & "-" & bucket2 & " SNF KG], " & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab2_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_Amt) as [" & bucket11 & "-" & bucket2 & " SNF Value], " & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab3_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_KG) as [" & bucket21 & "-" & bucket3 & " Fat KG]," & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab3_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_Amt) as [" & bucket21 & "-" & bucket3 & " Fat Value]," & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab3_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_KG) as [" & bucket21 & "-" & bucket3 & " SNF KG]," & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab3_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_Amt) as [" & bucket21 & "-" & bucket3 & " SNF Value]," & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab4_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_KG) as [" & bucket31 & "-" & bucket4 & " Fat KG], " & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab4_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab3_Date & "'" & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_Amt) as [" & bucket31 & "-" & bucket4 & " Fat Value]," & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab4_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_KG) as [" & bucket31 & "-" & bucket4 & " SNF KG], " & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab4_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab3_Date & "'" & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_Amt) as [" & bucket31 & "-" & bucket4 & " SNF Value]," & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_KG) as [Over " & bucket4 & " Fat KG], " & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Fat_Amt) as [Over " & bucket4 & " Fat Value], " & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_KG) as [Over " & bucket4 & " SNF KG], " & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.SNF_Amt) as [Over " & bucket4 & " SNF Value] "

            End If

            If clsCommon.CompairString(objFilter.ReportType, "Actual Ageing") = CompairStringResult.Equal Then
                qry = " with cte0 as " & Environment.NewLine & _
                      " ( " & Environment.NewLine & _
                      " select Inv.Item_Code, Inv.location_Code, cast(Inv.Punching_date as date) as Punching_date,Inv.Stock_UOM, SUM(Inv.stock_Qty) AS qty,sum(Inv.avg_Cost) as avg_Cost " & Environment.NewLine & _
                      " from tspl_Inventory_Movement_New Inv " & Environment.NewLine & _
                      " left join TSPL_ITEM_MASTER Item on Inv.Item_Code=Item.Item_Code " & Environment.NewLine & _
                      " left join TSPL_ITEM_TYPE_MASTER ItemType on Item.Item_Type=ItemType.Item_Type_Code " & Environment.NewLine & _
                      " left join TSPL_LOCATION_MASTER Loc on Inv.Location_Code=Loc.Location_Code " & Environment.NewLine & _
                      " where Inv.InOut='I' " & If(clsCommon.myLen(ItemFilter) > 0, " and Inv.Item_Code in " & ItemFilter & "", "") & " " & If(clsCommon.myLen(locFilter) > 0, " and " & locFilter & "", "") & " " & If(clsCommon.myLen(otherFilter) > 0, "  " & otherFilter & "", "") & " " & Environment.NewLine & _
                      " AND CAST(Inv.PUNCHING_DATE AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                      " group by Inv.Item_Code, Inv.Location_Code,Inv.Stock_UOM, cast(Inv.Punching_date as date) " & Environment.NewLine & _
                      " union all " & Environment.NewLine & _
                      " select Inv.Item_Code, Inv.location_Code, cast(Inv.Punching_date as date) as Punching_date,Inv.Stock_UOM, SUM(Inv.stock_Qty) AS qty,sum(Inv.avg_Cost) as avg_Cost " & Environment.NewLine & _
                      " from tspl_Inventory_Movement Inv " & Environment.NewLine & _
                      " left join TSPL_ITEM_MASTER Item on Inv.Item_Code=Item.Item_Code " & Environment.NewLine & _
                      " left join TSPL_ITEM_TYPE_MASTER ItemType on Item.Item_Type=ItemType.Item_Type_Code " & Environment.NewLine & _
                      " left join TSPL_LOCATION_MASTER Loc on Inv.Location_Code=Loc.Location_Code " & Environment.NewLine & _
                      " where Inv.InOut='I' " & If(clsCommon.myLen(ItemFilter) > 0, " and Inv.Item_Code in " & ItemFilter & "", "") & " " & If(clsCommon.myLen(locFilter) > 0, " and " & locFilter & "", "") & " " & If(clsCommon.myLen(otherFilter) > 0, "  " & otherFilter & "", "") & " " & Environment.NewLine & _
                      " AND CAST(Inv.PUNCHING_DATE AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                      " group by Inv.Item_Code, Inv.Location_Code,Inv.Stock_UOM, cast(Inv.Punching_date as date) " & Environment.NewLine & _
                      " ) " & Environment.NewLine & _
                      " ,cte as " & Environment.NewLine & _
                      " ( " & Environment.NewLine & _
                      " select Item_Code, location_Code, Punching_date,Stock_UOM, qty,avg_Cost, " & Environment.NewLine & _
                      " SUM(qty) OVER (PARTITION BY Item_Code, Location_Code ORDER BY Punching_date) AS sum_qty, " & Environment.NewLine & _
                      " SUM(avg_Cost) OVER (PARTITION BY Item_Code, Location_Code ORDER BY Punching_date) AS sum_avg_Cost " & Environment.NewLine & _
                      " from cte0 " & Environment.NewLine & _
                      " ) " & Environment.NewLine & _
                      " ,cte2 as " & Environment.NewLine & _
                      " ( " & Environment.NewLine & _
                      " select Inv.Item_Code, Inv.Location_Code,Inv.Stock_UOM,  SUM(Inv.Stock_Qty) AS sum_qty,SUM(Inv.Avg_Cost) AS sum_Avg_Cost " & Environment.NewLine & _
                      " from tspl_Inventory_Movement_New Inv " & Environment.NewLine & _
                      " left join TSPL_ITEM_MASTER Item on Inv.Item_Code=Item.Item_Code " & Environment.NewLine & _
                      " left join TSPL_ITEM_TYPE_MASTER ItemType on Item.Item_Type=ItemType.Item_Type_Code " & Environment.NewLine & _
                      " left join TSPL_LOCATION_MASTER Loc on Inv.Location_Code=Loc.Location_Code " & Environment.NewLine & _
                      " where Inv.InOut='O' " & If(clsCommon.myLen(ItemFilter) > 0, " and Inv.Item_Code in " & ItemFilter & "", "") & " " & If(clsCommon.myLen(locFilter) > 0, " and " & locFilter & "", "") & " " & If(clsCommon.myLen(otherFilter) > 0, "  " & otherFilter & "", "") & " " & Environment.NewLine & _
                      " AND CAST(Inv.PUNCHING_DATE AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                      " group by Inv.Item_Code, Inv.Location_Code,Inv.Stock_UOM " & Environment.NewLine & _
                      " union all " & Environment.NewLine & _
                      " select Inv.Item_Code, Inv.Location_Code,Inv.Stock_UOM,SUM(Inv.Stock_Qty) AS sum_qty,SUM(Inv.Avg_Cost) AS sum_Avg_Cost " & Environment.NewLine & _
                      " from tspl_Inventory_Movement Inv " & Environment.NewLine & _
                      " left join TSPL_ITEM_MASTER Item on Inv.Item_Code=Item.Item_Code " & Environment.NewLine & _
                      " left join TSPL_ITEM_TYPE_MASTER ItemType on Item.Item_Type=ItemType.Item_Type_Code " & Environment.NewLine & _
                      " left join TSPL_LOCATION_MASTER Loc on Inv.Location_Code=Loc.Location_Code " & Environment.NewLine & _
                      " where Inv.InOut='O' " & If(clsCommon.myLen(ItemFilter) > 0, " and Inv.Item_Code in " & ItemFilter & "", "") & " " & If(clsCommon.myLen(locFilter) > 0, " and " & locFilter & "", "") & " " & If(clsCommon.myLen(otherFilter) > 0, "  " & otherFilter & "", "") & "  " & Environment.NewLine & _
                      " AND CAST(Inv.PUNCHING_DATE AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                      " group by Inv.Item_Code, Inv.Location_Code,Inv.Stock_UOM " & Environment.NewLine & _
                      " ) " & Environment.NewLine & _
                      " ,cte3 as " & Environment.NewLine & _
                      " ( " & Environment.NewLine & _
                      " select Inv1.Item_Code, Inv1.location_Code, Inv1.Punching_date,Inv1.Stock_UOM, Inv1.qty ,Inv1.avg_Cost,Inv1.sum_qty,Inv1.sum_Avg_Cost, " & Environment.NewLine & _
                      " isnull(Inv2.sum_qty, 0) AS sum_qty2,isnull(Inv2.sum_Avg_Cost, 0) AS sum_Avg_Cost2, " & Environment.NewLine & _
                      " ROW_NUMBER() OVER(PARTITION BY Inv1.Item_Code, Inv1.location_Code ORDER BY Inv1.Punching_date) AS row_num " & Environment.NewLine & _
                      " from cte Inv1 " & Environment.NewLine & _
                      " left join cte2 Inv2 on Inv1.Item_Code = Inv2.Item_Code and Inv1.location_Code = Inv2.location_Code " & Environment.NewLine & _
                      " where Inv1.sum_qty > isnull(Inv2.sum_qty, 0) " & Environment.NewLine & _
                      " ) " & Environment.NewLine & _
                      " , cte4 as " & Environment.NewLine & _
                      " ( " & Environment.NewLine & _
                      " select Item_Code, location_Code, Punching_date,Stock_UOM, 'I' as InOut, (CASE WHEN row_num = 1 THEN sum_qty - sum_qty2 ELSE qty END) AS Stock_Qty, " & Environment.NewLine & _
                      " (CASE WHEN row_num = 1 THEN sum_Avg_Cost - sum_Avg_Cost2 ELSE avg_Cost END) as Avg_Cost from cte3 " & Environment.NewLine & _
                      " ) " & Environment.NewLine & _
                  " select ItemType.Item_Type_Name as Item_Type,Inv.Item_Code,Item.Item_Desc,Inv.Location_Code,Loc.Location_Desc as [Location Desc], " & If(clsCommon.myLen(strCategoryTable) > 0, strCodeColumnSelect + "," + strCodeDescColumnSelect + ",", "") & " Inv.Punching_Date,convert(varchar,Inv.Punching_Date,103) as Document_Date," & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "'" & clsCommon.myCstr(objFilter.UOM_Code) & "' as Stock_Uom", "Inv.Stock_Uom") & " , " & Environment.NewLine & _
                  " ((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") AS Current_Qty,((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*Inv.Avg_Cost) AS Current_Value," & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab1_Date & "' and CAST(Inv.Punching_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [0-" & bucket1 & " Qty], " & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab1_Date & "' and CAST(Inv.Punching_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [0-" & bucket1 & " Value]," & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab2_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [" & bucket11 & "-" & bucket2 & " Qty]," & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab2_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [" & bucket11 & "-" & bucket2 & " Value], " & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab3_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [" & bucket21 & "-" & bucket3 & " Qty], " & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab3_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [" & bucket21 & "-" & bucket3 & " Value]," & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab4_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [" & bucket31 & "-" & bucket4 & " Qty], " & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab4_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                  " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [" & bucket31 & "-" & bucket4 & " Value], " & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [Over " & bucket4 & " Qty]," & Environment.NewLine & _
                  " ((CASE when CAST(Inv.Punching_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [Over " & bucket4 & " Value]  " & Environment.NewLine & _
                  " from cte4 Inv " & Environment.NewLine & _
                  " left join TSPL_ITEM_MASTER Item on Inv.Item_Code=Item.Item_Code " & Environment.NewLine & _
                  " left join TSPL_ITEM_TYPE_MASTER ItemType on Item.Item_Type=ItemType.Item_Type_Code " & Environment.NewLine & _
                  " left join TSPL_LOCATION_MASTER Loc on Inv.Location_Code=Loc.Location_Code " & Environment.NewLine & _
                  " " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=Inv.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + clsCommon.myCstr(objFilter.UOM_Code) + "'", "") & " " & Environment.NewLine & _
                  " " & If(clsCommon.myLen(strCategoryTable) > 0, " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=Inv.Item_Code", "") & " " & Environment.NewLine & _
                  " where 2=2 and (Inv.Stock_Qty<>0 or Inv.Avg_Cost<>0) " & If(clsCommon.myLen(ItemFilter) > 0, " and Inv.Item_Code in " & ItemFilter & "", "") & " " & If(clsCommon.myLen(locFilter) > 0, " and " & locFilter & "", "") & " " & If(clsCommon.myLen(otherFilter) > 0, "  " & otherFilter & "", "") & " "

            Else
                If isShelfLifeWise = False Then
                    ''richa agarwal 14 Nov,2018 add gitlocation filter in all unions  " & If(isGITLocation = False, " and isnull(Loc.GIT_Type,'') <>'Y' ", "") & " UDL/02/11/18-000238
                    qry = " -- opening of milk table " & Environment.NewLine & _
                     " select max(ItemType.Item_Type_Name) as Item_Type,max(Item.tech_shelf_life) as shelf_life,Inv.Item_Code,max(Item.Item_Desc) as Item_Desc,Inv.Location_Code,max(Loc.Location_Desc) as [Location Desc], " & If(clsCommon.myLen(strCategoryTable) > 0, strCodeColumnMaxWithTable + "," + strCodeDescColumnMax + ",", "") & " 'Opening' as Trans_Type,'Opening' as Trans_Name,'' as Document_No,cast('" & CutOffDate & "' as date) as Punching_date,'" & CutOffDate & "' as Document_Date," & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "'" & clsCommon.myCstr(objFilter.UOM_Code) & "' as Stock_Uom", "max(Inv.Stock_Uom) as Stock_Uom") & ", " & Environment.NewLine & _
                     " sum((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") AS Current_Qty,sum((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*Inv.Avg_Cost) AS Current_Value, " & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab1_Date & "' and CAST(Inv.Punching_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [0-" & bucket1 & " Qty], " & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab1_Date & "' and CAST(Inv.Punching_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [0-" & bucket1 & " Value], " & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab2_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [" & bucket11 & "-" & bucket2 & " Qty], " & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab2_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [" & bucket11 & "-" & bucket2 & " Value], " & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab3_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [" & bucket21 & "-" & bucket3 & " Qty]," & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab3_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [" & bucket21 & "-" & bucket3 & " Value], " & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) > ='" & slab4_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [" & bucket31 & "-" & bucket4 & " Qty]," & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) > ='" & slab4_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [" & bucket31 & "-" & bucket4 & " Value], " & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [Over " & bucket4 & " Qty], " & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [Over " & bucket4 & " Value] " & If(clsCommon.myLen(QryOPFatSNF) > 0, QryOPFatSNF, "") & " " & Environment.NewLine & _
                     " from TSPL_INVENTORY_MOVEMENT_NEW Inv " & Environment.NewLine & _
                     " left join TSPL_ITEM_MASTER Item on Inv.Item_Code=Item.Item_Code " & Environment.NewLine & _
                     " left join TSPL_ITEM_TYPE_MASTER ItemType on Item.Item_Type=ItemType.Item_Type_Code " & Environment.NewLine & _
                     " left join TSPL_LOCATION_MASTER Loc on Inv.Location_Code=Loc.Location_Code " & Environment.NewLine & _
                     " " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=Inv.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + clsCommon.myCstr(objFilter.UOM_Code) + "'", "") & " " & Environment.NewLine & _
                     " " & If(clsCommon.myLen(strCategoryTable) > 0, " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=Inv.Item_Code", "") & " " & Environment.NewLine & _
                     " where 2=2 " & If(clsCommon.myLen(ItemFilter) > 0, " and Inv.Item_Code in " & ItemFilter & "", "") & " " & If(clsCommon.myLen(locFilter) > 0, " and " & locFilter & "", "") & " " & If(clsCommon.myLen(otherFilter) > 0, " " & otherFilter & "", "") & "  " & If(isGITLocation = False, " and isnull(Loc.GIT_Type,'') <>'Y' ", "") & "  and  cast(Punching_Date as date)<'" & CutOffDate & "' group by Inv.Item_Code,Inv.Location_Code " & Environment.NewLine & _
                     " -- opening of non milk table " & Environment.NewLine & _
                     " union all " & Environment.NewLine & _
                     " select max(ItemType.Item_Type_Name) as Item_Type,max(Item.tech_shelf_life) as shelf_life,Inv.Item_Code,max(Item.Item_Desc) as Item_Desc,Inv.Location_Code,max(Loc.Location_Desc) as [Location Desc], " & If(clsCommon.myLen(strCategoryTable) > 0, strCodeColumnMaxWithTable + "," + strCodeDescColumnMax + ",", "") & " 'Opening' as Trans_Type,'Opening' as Trans_Name,'' as Document_No,cast('" & CutOffDate & "' as date) as Punching_date,'" & CutOffDate & "' as Document_Date," & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "'" & clsCommon.myCstr(objFilter.UOM_Code) & "' as Stock_Uom", "max(Inv.Stock_Uom) as Stock_Uom") & " , " & Environment.NewLine & _
                     " sum((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") AS Current_Qty,sum((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*Inv.Avg_Cost) AS Current_Value, " & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab1_Date & "' and CAST(Inv.Punching_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [0-" & bucket1 & " Qty], " & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab1_Date & "' and CAST(Inv.Punching_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [0-" & bucket1 & " Value], " & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab2_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [" & bucket11 & "-" & bucket2 & " Qty], " & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab2_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [" & bucket11 & "-" & bucket2 & " Value], " & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab3_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [" & bucket21 & "-" & bucket3 & " Qty]," & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab3_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [" & bucket21 & "-" & bucket3 & " Value], " & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab4_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [" & bucket31 & "-" & bucket4 & " Qty], " & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab4_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [" & bucket31 & "-" & bucket4 & " Value], " & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [Over " & bucket4 & " Qty], " & Environment.NewLine & _
                     " sum((CASE when CAST(Inv.Punching_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [Over " & bucket4 & " Value] " & If(clsCommon.myLen(QryOPFatSNF) > 0, QryOPFatSNF, "") & " " & Environment.NewLine & _
                     " from TSPL_INVENTORY_MOVEMENT Inv " & Environment.NewLine & _
                     " left join TSPL_ITEM_MASTER Item on Inv.Item_Code=Item.Item_Code " & Environment.NewLine & _
                     " left join TSPL_ITEM_TYPE_MASTER ItemType on Item.Item_Type=ItemType.Item_Type_Code " & Environment.NewLine & _
                     " left join TSPL_LOCATION_MASTER Loc on Inv.Location_Code=Loc.Location_Code " & Environment.NewLine & _
                     " " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=Inv.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + clsCommon.myCstr(objFilter.UOM_Code) + "'", "") & " " & Environment.NewLine & _
                     " " & If(clsCommon.myLen(strCategoryTable) > 0, " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=Inv.Item_Code", "") & " " & Environment.NewLine & _
                     " where 2=2 " & If(clsCommon.myLen(ItemFilter) > 0, " and Inv.Item_Code in " & ItemFilter & "", "") & " " & If(clsCommon.myLen(locFilter) > 0, " and " & locFilter & "", "") & " " & If(clsCommon.myLen(otherFilter) > 0, " " & otherFilter & "", "") & "  " & If(isGITLocation = False, " and isnull(Loc.GIT_Type,'') <>'Y' ", "") & " and  cast(Punching_Date as date)<'" & CutOffDate & "' group by Inv.Item_Code,Inv.Location_Code " & Environment.NewLine & _
                     " -- docs of milk table " & Environment.NewLine & _
                     " union all " & Environment.NewLine & _
                     " select ItemType.Item_Type_Name as Item_Type,Item.tech_shelf_life as shelf_life,Inv.Item_Code,Item.Item_Desc,Inv.Location_Code,Loc.Location_Desc as [Location Desc], " & If(clsCommon.myLen(strCategoryTable) > 0, strCodeColumnSelect + "," + strCodeDescColumnSelect + ",", "") & " Inv.Trans_Type,InvSource.Name as Trans_Name,Inv.Source_Doc_No as Document_No,Inv.Punching_Date,convert(varchar,Inv.Punching_Date,103) as Document_Date," & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "'" & clsCommon.myCstr(objFilter.UOM_Code) & "' as Stock_Uom", "Inv.Stock_Uom") & " , " & Environment.NewLine & _
                     " ((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") AS Current_Qty,((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*Inv.Avg_Cost) AS Current_Value," & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab1_Date & "' and CAST(Inv.Punching_Date AS DATE)<='" & AsOnDate & "'  " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [0-" & bucket1 & " Qty]," & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab1_Date & "' and CAST(Inv.Punching_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [0-" & bucket1 & " Value]," & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab2_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [" & bucket11 & "-" & bucket2 & " Qty], " & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab2_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [" & bucket11 & "-" & bucket2 & " Value], " & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab3_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [" & bucket21 & "-" & bucket3 & " Qty]," & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab3_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [" & bucket21 & "-" & bucket3 & " Value]," & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab4_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [" & bucket31 & "-" & bucket4 & " Qty], " & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab4_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab3_Date & "'" & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [" & bucket31 & "-" & bucket4 & " Value]," & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [Over " & bucket4 & " Qty], " & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [Over " & bucket4 & " Value] " & If(clsCommon.myLen(qryFatSNFMilk) > 0, qryFatSNFMilk, "") & " " & Environment.NewLine & _
                     " from TSPL_INVENTORY_MOVEMENT_NEW Inv " & Environment.NewLine & _
                     " left join TSPL_ITEM_MASTER Item on Inv.Item_Code=Item.Item_Code " & Environment.NewLine & _
                     " left join TSPL_ITEM_TYPE_MASTER ItemType on Item.Item_Type=ItemType.Item_Type_Code " & Environment.NewLine & _
                     " left join TSPL_LOCATION_MASTER Loc on Inv.Location_Code=Loc.Location_Code " & Environment.NewLine & _
                     " left join TSPL_INVENTORY_SOURCE_CODE InvSource on Inv.Trans_Type=InvSource.Code " & Environment.NewLine & _
                     " " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=Inv.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + clsCommon.myCstr(objFilter.UOM_Code) + "'", "") & " " & Environment.NewLine & _
                     " " & If(clsCommon.myLen(strCategoryTable) > 0, " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=Inv.Item_Code", "") & " " & Environment.NewLine & _
                     " where 2=2 " & If(clsCommon.myLen(ItemFilter) > 0, " and Inv.Item_Code in " & ItemFilter & "", "") & " " & If(clsCommon.myLen(locFilter) > 0, " and " & locFilter & "", "") & " " & If(clsCommon.myLen(otherFilter) > 0, " " & otherFilter & "", "") & "  " & If(isGITLocation = False, " and isnull(Loc.GIT_Type,'') <>'Y' ", "") & "  and cast(Punching_Date as date)>='" & CutOffDate & "' and cast(Punching_Date as date)<='" & AsOnDate & "' " & Environment.NewLine & _
                     " union all " & Environment.NewLine & _
                     " -- docs of non milk table " & Environment.NewLine & _
                     " select ItemType.Item_Type_Name as Item_Type,Item.tech_shelf_life as shelf_life,Inv.Item_Code,Item.Item_Desc,Inv.Location_Code,Loc.Location_Desc as [Location Desc], " & If(clsCommon.myLen(strCategoryTable) > 0, strCodeColumnSelect + "," + strCodeDescColumnSelect + ",", "") & " Inv.Trans_Type,InvSource.Name as Trans_Name,Inv.Source_Doc_No as Document_No,Inv.Punching_Date,convert(varchar,Inv.Punching_Date,103) as Document_Date," & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "'" & clsCommon.myCstr(objFilter.UOM_Code) & "' as Stock_Uom", "Inv.Stock_Uom") & " , " & Environment.NewLine & _
                     " ((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") AS Current_Qty,((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*Inv.Avg_Cost) AS Current_Value," & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab1_Date & "' and CAST(Inv.Punching_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [0-" & bucket1 & " Qty], " & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab1_Date & "' and CAST(Inv.Punching_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [0-" & bucket1 & " Value]," & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab2_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [" & bucket11 & "-" & bucket2 & " Qty]," & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab2_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [" & bucket11 & "-" & bucket2 & " Value], " & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab3_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [" & bucket21 & "-" & bucket3 & " Qty], " & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab3_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [" & bucket21 & "-" & bucket3 & " Value]," & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab4_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [" & bucket31 & "-" & bucket4 & " Qty], " & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) >= '" & slab4_Date & "' and CAST(Inv.Punching_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [" & bucket31 & "-" & bucket4 & " Value], " & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*" & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "Inv.Stock_Qty") & ") as [Over " & bucket4 & " Qty]," & Environment.NewLine & _
                     " ((CASE when CAST(Inv.Punching_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*Inv.Avg_Cost) as [Over " & bucket4 & " Value] " & If(clsCommon.myLen(qryFatSNFMilk) > 0, qryFatSNFMilk, "") & " " & Environment.NewLine & _
                     " from TSPL_INVENTORY_MOVEMENT Inv " & Environment.NewLine & _
                     " left join TSPL_ITEM_MASTER Item on Inv.Item_Code=Item.Item_Code " & Environment.NewLine & _
                     " left join TSPL_ITEM_TYPE_MASTER ItemType on Item.Item_Type=ItemType.Item_Type_Code " & Environment.NewLine & _
                     " left join TSPL_LOCATION_MASTER Loc on Inv.Location_Code=Loc.Location_Code " & Environment.NewLine & _
                     " left join TSPL_INVENTORY_SOURCE_CODE InvSource on Inv.Trans_Type=InvSource.Code " & Environment.NewLine & _
                     " " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=Inv.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + clsCommon.myCstr(objFilter.UOM_Code) + "'", "") & " " & Environment.NewLine & _
                     " " & If(clsCommon.myLen(strCategoryTable) > 0, " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=Inv.Item_Code", "") & " " & Environment.NewLine & _
                     " where 2=2  " & If(clsCommon.myLen(ItemFilter) > 0, " and Inv.Item_Code in " & ItemFilter & "", "") & " " & If(clsCommon.myLen(locFilter) > 0, " and " & locFilter & "", "") & " " & If(clsCommon.myLen(otherFilter) > 0, " " & otherFilter & "", "") & "  " & If(isGITLocation = False, " and isnull(Loc.GIT_Type,'') <>'Y' ", "") & " and cast(Punching_Date as date)>='" & CutOffDate & "' and cast(Punching_Date as date)<='" & AsOnDate & "' "

                ElseIf isShelfLifeWise = True Then
                    'Sanjay Ticket No  TEC/18/06/19-000547
                    qry = " -- opening of milk table " & Environment.NewLine & _
                     " select max(ItemType.Item_Type_Name) as Item_Type,max(Item.tech_shelf_life) as shelf_life,Inv.Item_Code,max(Item.Item_Desc) as Item_Desc,Inv.Location_Code,max(Loc.Location_Desc) as [Location Desc], " & If(clsCommon.myLen(strCategoryTable) > 0, strCodeColumnMaxWithTable + "," + strCodeDescColumnMax + ",", "") & " 'Opening' as Trans_Type,'Opening' as Trans_Name,'' as Document_No,cast('" & CutOffDate & "' as date) as Punching_date,'" & CutOffDate & "' as Document_Date," & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "'" & clsCommon.myCstr(objFilter.UOM_Code) & "' as Stock_Uom", "max(Inv.Stock_Uom) as Stock_Uom") & ", " & Environment.NewLine & _
                     " sum((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end )*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) AS Current_Qty,sum((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) AS Current_Value, " & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab1_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [0-" & bucket1 & " Qty], " & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab1_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [0-" & bucket1 & " Value], " & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab2_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [" & bucket11 & "-" & bucket2 & " Qty], " & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab2_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [" & bucket11 & "-" & bucket2 & " Value], " & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab3_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [" & bucket21 & "-" & bucket3 & " Qty]," & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab3_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [" & bucket21 & "-" & bucket3 & " Value], " & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) > ='" & slab4_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [" & bucket31 & "-" & bucket4 & " Qty]," & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) > ='" & slab4_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [" & bucket31 & "-" & bucket4 & " Value], " & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [Over " & bucket4 & " Qty], " & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [Over " & bucket4 & " Value] " & If(clsCommon.myLen(QryOPFatSNF) > 0, QryOPFatSNF, "") & " " & Environment.NewLine & _
                     " from TSPL_INVENTORY_MOVEMENT_NEW Inv " & Environment.NewLine & _
                     " inner join TSPL_BATCH_ITEM BTItem on BTItem.Document_Code=Inv.Source_Doc_No and BTItem.Item_Code=Inv.Item_Code " & Environment.NewLine & _
                     " left join TSPL_ITEM_MASTER Item on Inv.Item_Code=Item.Item_Code " & Environment.NewLine & _
                     " left join TSPL_ITEM_TYPE_MASTER ItemType on Item.Item_Type=ItemType.Item_Type_Code " & Environment.NewLine & _
                     " left join TSPL_LOCATION_MASTER Loc on Inv.Location_Code=Loc.Location_Code "
                    If (clsCommon.myLen(objFilter.UOM_Code) > 0) Then
                        qry += " left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=Inv.Item_Code"
                        qry += " and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + objFilter.UOM_Code + "'"
                    End If
                    qry += " where 2=2 " & If(clsCommon.myLen(ItemFilter) > 0, " and Inv.Item_Code in " & ItemFilter & "", "") & " " & If(clsCommon.myLen(locFilter) > 0, " and " & locFilter & "", "") & " " & If(clsCommon.myLen(otherFilter) > 0, " " & otherFilter & "", "") & "  " & If(isGITLocation = False, " and isnull(Loc.GIT_Type,'') <>'Y' ", "") & "  and  cast(Punching_Date as date)<'" & CutOffDate & "' group by Inv.Item_Code,Inv.Location_Code " & Environment.NewLine & _
                     " -- opening of non milk table " & Environment.NewLine & _
                     " union all " & Environment.NewLine & _
                     " select max(ItemType.Item_Type_Name) as Item_Type,max(Item.tech_shelf_life) as shelf_life,Inv.Item_Code,max(Item.Item_Desc) as Item_Desc,Inv.Location_Code,max(Loc.Location_Desc) as [Location Desc], " & If(clsCommon.myLen(strCategoryTable) > 0, strCodeColumnMaxWithTable + "," + strCodeDescColumnMax + ",", "") & " 'Opening' as Trans_Type,'Opening' as Trans_Name,'' as Document_No,cast('" & CutOffDate & "' as date) as Punching_date,'" & CutOffDate & "' as Document_Date," & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "'" & clsCommon.myCstr(objFilter.UOM_Code) & "' as Stock_Uom", "max(Inv.Stock_Uom) as Stock_Uom") & " , " & Environment.NewLine & _
                     " sum((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) AS Current_Qty,sum((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) AS Current_Value, " & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab1_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [0-" & bucket1 & " Qty], " & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab1_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [0-" & bucket1 & " Value], " & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab2_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [" & bucket11 & "-" & bucket2 & " Qty], " & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab2_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [" & bucket11 & "-" & bucket2 & " Value], " & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab3_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [" & bucket21 & "-" & bucket3 & " Qty]," & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab3_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [" & bucket21 & "-" & bucket3 & " Value], " & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab4_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [" & bucket31 & "-" & bucket4 & " Qty], " & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab4_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [" & bucket31 & "-" & bucket4 & " Value], " & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [Over " & bucket4 & " Qty], " & Environment.NewLine & _
                     " sum((CASE when CAST(BTItem.Expiry_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [Over " & bucket4 & " Value] " & If(clsCommon.myLen(QryOPFatSNF) > 0, QryOPFatSNF, "") & " " & Environment.NewLine & _
                     " from TSPL_INVENTORY_MOVEMENT Inv " & Environment.NewLine & _
                     " inner join TSPL_BATCH_ITEM BTItem on BTItem.Document_Code=Inv.Source_Doc_No and BTItem.Item_Code=Inv.Item_Code " & Environment.NewLine & _
                     " left join TSPL_ITEM_MASTER Item on Inv.Item_Code=Item.Item_Code " & Environment.NewLine & _
                     " left join TSPL_ITEM_TYPE_MASTER ItemType on Item.Item_Type=ItemType.Item_Type_Code " & Environment.NewLine & _
                     " left join TSPL_LOCATION_MASTER Loc on Inv.Location_Code=Loc.Location_Code "
                    If (clsCommon.myLen(objFilter.UOM_Code) > 0) Then
                        qry += " left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=Inv.Item_Code"
                        qry += " and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + objFilter.UOM_Code + "'"
                    End If
                    qry += " where 2=2 " & If(clsCommon.myLen(ItemFilter) > 0, " and Inv.Item_Code in " & ItemFilter & "", "") & " " & If(clsCommon.myLen(locFilter) > 0, " and " & locFilter & "", "") & " " & If(clsCommon.myLen(otherFilter) > 0, " " & otherFilter & "", "") & "  " & If(isGITLocation = False, " and isnull(Loc.GIT_Type,'') <>'Y' ", "") & " and  cast(Punching_Date as date)<'" & CutOffDate & "' group by Inv.Item_Code,Inv.Location_Code " & Environment.NewLine & _
                     " -- docs of milk table " & Environment.NewLine & _
                     " union all " & Environment.NewLine & _
                     " select ItemType.Item_Type_Name as Item_Type,Item.tech_shelf_life as shelf_life,Inv.Item_Code,Item.Item_Desc,Inv.Location_Code,Loc.Location_Desc as [Location Desc], " & If(clsCommon.myLen(strCategoryTable) > 0, strCodeColumnSelect + "," + strCodeDescColumnSelect + ",", "") & " Inv.Trans_Type,InvSource.Name as Trans_Name,Inv.Source_Doc_No as Document_No,Inv.Punching_Date,convert(varchar,Inv.Punching_Date,103) as Document_Date," & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "'" & clsCommon.myCstr(objFilter.UOM_Code) & "' as Stock_Uom", "Inv.Stock_Uom") & " , " & Environment.NewLine & _
                     " ((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) AS Current_Qty,((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) AS Current_Value," & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab1_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<='" & AsOnDate & "'  " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [0-" & bucket1 & " Qty]," & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab1_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [0-" & bucket1 & " Value]," & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab2_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [" & bucket11 & "-" & bucket2 & " Qty], " & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab2_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [" & bucket11 & "-" & bucket2 & " Value], " & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab3_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [" & bucket21 & "-" & bucket3 & " Qty]," & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab3_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [" & bucket21 & "-" & bucket3 & " Value]," & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab4_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [" & bucket31 & "-" & bucket4 & " Qty], " & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab4_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab3_Date & "'" & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [" & bucket31 & "-" & bucket4 & " Value]," & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [Over " & bucket4 & " Qty], " & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [Over " & bucket4 & " Value] " & If(clsCommon.myLen(qryFatSNFMilk) > 0, qryFatSNFMilk, "") & " " & Environment.NewLine & _
                     " from TSPL_INVENTORY_MOVEMENT_NEW Inv " & Environment.NewLine & _
                     " inner join TSPL_BATCH_ITEM BTItem on BTItem.Document_Code=Inv.Source_Doc_No and BTItem.Item_Code=Inv.Item_Code " & Environment.NewLine & _
                     " left join TSPL_ITEM_MASTER Item on Inv.Item_Code=Item.Item_Code " & Environment.NewLine & _
                     " left join TSPL_ITEM_TYPE_MASTER ItemType on Item.Item_Type=ItemType.Item_Type_Code " & Environment.NewLine & _
                     " left join TSPL_LOCATION_MASTER Loc on Inv.Location_Code=Loc.Location_Code " & Environment.NewLine & _
                     " left join TSPL_INVENTORY_SOURCE_CODE InvSource on Inv.Trans_Type=InvSource.Code "
                    If (clsCommon.myLen(objFilter.UOM_Code) > 0) Then
                        qry += " left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=Inv.Item_Code"
                        qry += " and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + objFilter.UOM_Code + "'"
                    End If
                    qry += " where 2=2 " & If(clsCommon.myLen(ItemFilter) > 0, " and Inv.Item_Code in " & ItemFilter & "", "") & " " & If(clsCommon.myLen(locFilter) > 0, " and " & locFilter & "", "") & " " & If(clsCommon.myLen(otherFilter) > 0, " " & otherFilter & "", "") & "  " & If(isGITLocation = False, " and isnull(Loc.GIT_Type,'') <>'Y' ", "") & "  and cast(Punching_Date as date)>='" & CutOffDate & "' and cast(Punching_Date as date)<='" & AsOnDate & "' " & Environment.NewLine & _
                     " union all " & Environment.NewLine & _
                     " -- docs of non milk table " & Environment.NewLine & _
                     " select ItemType.Item_Type_Name as Item_Type,Item.tech_shelf_life as shelf_life,Inv.Item_Code,Item.Item_Desc,Inv.Location_Code,Loc.Location_Desc as [Location Desc], " & If(clsCommon.myLen(strCategoryTable) > 0, strCodeColumnSelect + "," + strCodeDescColumnSelect + ",", "") & " Inv.Trans_Type,InvSource.Name as Trans_Name,Inv.Source_Doc_No as Document_No,Inv.Punching_Date,convert(varchar,Inv.Punching_Date,103) as Document_Date," & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "'" & clsCommon.myCstr(objFilter.UOM_Code) & "' as Stock_Uom", "Inv.Stock_Uom") & " , " & Environment.NewLine & _
                     " ((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) AS Current_Qty,((CASE WHEN Inv.InOut='I' THEN 1 ELSE -1 END)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) AS Current_Value," & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab1_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [0-" & bucket1 & " Qty], " & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab1_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<='" & AsOnDate & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [0-" & bucket1 & " Value]," & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab2_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [" & bucket11 & "-" & bucket2 & " Qty]," & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab2_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab1_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [" & bucket11 & "-" & bucket2 & " Value], " & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab3_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [" & bucket21 & "-" & bucket3 & " Qty], " & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab3_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab2_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [" & bucket21 & "-" & bucket3 & " Value]," & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab4_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [" & bucket31 & "-" & bucket4 & " Qty], " & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) >= '" & slab4_Date & "' and CAST(BTItem.Expiry_Date AS DATE)<'" & slab3_Date & "' " & Environment.NewLine & _
                     " then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [" & bucket31 & "-" & bucket4 & " Value], " & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Stock_Qty/Inv.Qty end)*BTItem.Qty " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, "/TSPL_ITEM_UOM_DETAIL.Conversion_Factor", "") & " ) as [Over " & bucket4 & " Qty]," & Environment.NewLine & _
                     " ((CASE when CAST(BTItem.Expiry_Date AS DATE) < '" & slab4_Date & "' then (case WHEN Inv.InOut='I' THEN 1 ELSE -1 END) else 0 end)*(case when Inv.Qty=0 then 0 else Inv.Avg_Cost/Inv.Qty end)*BTItem.Qty) as [Over " & bucket4 & " Value] " & If(clsCommon.myLen(qryFatSNFMilk) > 0, qryFatSNFMilk, "") & " " & Environment.NewLine & _
                     " from TSPL_INVENTORY_MOVEMENT Inv " & Environment.NewLine & _
                     " inner join TSPL_BATCH_ITEM BTItem on BTItem.Document_Code=Inv.Source_Doc_No and BTItem.Item_Code=Inv.Item_Code " & Environment.NewLine & _
                     " left join TSPL_ITEM_MASTER Item on Inv.Item_Code=Item.Item_Code " & Environment.NewLine & _
                     " left join TSPL_ITEM_TYPE_MASTER ItemType on Item.Item_Type=ItemType.Item_Type_Code " & Environment.NewLine & _
                     " left join TSPL_LOCATION_MASTER Loc on Inv.Location_Code=Loc.Location_Code " & Environment.NewLine & _
                     " left join TSPL_INVENTORY_SOURCE_CODE InvSource on Inv.Trans_Type=InvSource.Code "
                    If (clsCommon.myLen(objFilter.UOM_Code) > 0) Then
                        qry += " left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=Inv.Item_Code"
                        qry += " and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + objFilter.UOM_Code + "'"
                    End If
                    qry += " where 2=2  " & If(clsCommon.myLen(ItemFilter) > 0, " and Inv.Item_Code in " & ItemFilter & "", "") & " " & If(clsCommon.myLen(locFilter) > 0, " and " & locFilter & "", "") & " " & If(clsCommon.myLen(otherFilter) > 0, " " & otherFilter & "", "") & "  " & If(isGITLocation = False, " and isnull(Loc.GIT_Type,'') <>'Y' ", "") & " and cast(Punching_Date as date)>='" & CutOffDate & "' and cast(Punching_Date as date)<='" & AsOnDate & "' "
                    'Sanjay Ticket No  TEC/18/06/19-000547
                End If
            End If

            arrOutput.Add(qry)
            arrOutput.Add(strCategoryTable)
            arrOutput.Add(strCodeColumn)
            arrOutput.Add(strCodeDescColumn)

            Return arrOutput
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
End Class
Public Class clsStockRecoFilters
#Region "Variables"
    Public From_Date As Date = Nothing
    Public To_Date As Date = Nothing
    Public InOut As String = Nothing
    Public arrTransaction As ArrayList = Nothing
    Public arrItemGroup As ArrayList = Nothing
    Public arrItem As ArrayList = Nothing
    Public ReportType As String = Nothing
    Public arrItemType As ArrayList = Nothing
    Public UOM_Code As String = Nothing
    Public MRPWise As Boolean = False
    Public FatSNF As Boolean = True
    Public IncludeGIT As Boolean = False
    Public ExcludeConsumptionLoc As Boolean = False
    Public DisplayMethod As String = "None"
    '====================add by Monika26/03/2017==================
    Public IsProduction_WIP As Boolean = False
    Public FAT_SNF_TYPE As String = "M" ''Manual
    Public ChkPartialyLoadData As Boolean = False
    Public isPrintCrystal As Integer = 0
    ''===================================================================

    Public SelectLocation As Boolean = False
    Public arrLocation As List(Of clsCode) = Nothing
    Public SelectCategory As Boolean = False
    Public arrCategory As List(Of clsCode) = Nothing
    Public FORMTYPE As String
    Public arrLoc As ArrayList

#End Region
End Class
Public Class clsStockAgeingFilters
#Region "Variables"
    Public CutOffDate As Date = Nothing
    Public AsOnDate As Date = Nothing
    Public ReportType As String = Nothing
    Public InventoryType As String = Nothing '' Milk, Non Milk,All
    Public AgeingColumns As String = Nothing '' Qty,Value,Qty+Value,Fat-SNF,All
    Public Item_Status As String = Nothing
    Public arrItemType As ArrayList = Nothing
    Public arrTransaction As ArrayList = Nothing
    Public arrItemGroup As ArrayList = Nothing
    Public arrItem As ArrayList = Nothing
    Public UOM_Code As String = Nothing
    Public SelectLocation As Boolean = False
    Public arrLocation As List(Of clsCode) = Nothing
    Public arrLoc As ArrayList
    Public arrAgeingBucket As ArrayList
    Public SelectCategory As Boolean = False
    Public arrCategory As List(Of clsCode) = Nothing
#End Region
End Class
Public Class clsCode
    Public Sel As Boolean = False
    Public Code As String
    Public Desc As String
    Public arrOut As Dictionary(Of String, Object) = Nothing
End Class





Public Class clsPhysicalstock

#Region "Variables"
    Public Physical_No As String = Nothing
    Public Line_No As String = Nothing
    Public Description As String = Nothing
    Public Stock_Date As DateTime = Nothing
    Public Location_Code As String = Nothing
    Public Main_Location As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing

    Public GL_Account_Inventroy_Ctrl As String = Nothing
    Public GL_Account_Inventroy_CtrlName As String = Nothing
    Public GL_Account As String = Nothing
    Public GL_AccountName As String = Nothing

    Public MRP As Double = 0
    Public Stock_Unit As String = Nothing
    Public Batch_No As String = Nothing
    Public Existing_Qty As Decimal = 0
    Public Existing_FAT_Pers As Decimal = Nothing
    Public Existing_FAT_Kg As Decimal = Nothing
    Public Existing_FAT_Amt As Decimal
    Public Existing_SNF_Pers As Decimal = Nothing
    Public Existing_SNF_Kg As Decimal = Nothing
    Public Existing_SNF_Amt As Decimal
    Public Existing_Amount As Decimal

    Public Physical_Qty As Decimal = 0
    Public FAT_Pers As Decimal = Nothing
    Public FAT_Kg As Decimal = Nothing
    Public FAT_Amt As Decimal
    Public SNF_Pers As Decimal = Nothing
    Public SNF_Kg As Decimal = Nothing
    Public SNF_Amt As Decimal
    Public Amt As Decimal

    Public Difference As Decimal = 0
    Public FatPerDifference As Decimal = 0
    Public FatKgDifference As Decimal = 0
    Public FatAmtDifference As Decimal = 0
    Public SNFPerDifference As Decimal = 0
    Public SNFKgDifference As Decimal = 0
    Public SNFAmtDifference As Decimal
    Public DifferenceAmt As Decimal

    Public Is_Milk As Integer = Nothing
    Public Is_Posted As Integer = Nothing

    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
    Public Nill_Balance As Boolean = False
    Public Multiple_Location As Boolean = False
    Public Arr As List(Of clsPhysicalstock) = Nothing
#End Region

    Private Property is_MRPWise As Integer

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = " select *  from  (select TSPL_PHYSICAL_STOCK.physical_no as Code,max(TSPL_PHYSICAL_STOCK.Description) as Description,max(TSPL_PHYSICAL_STOCK.stock_Date) as [Date],max(TSPL_PHYSICAL_STOCK.location) as [Location Code],max(tspl_location_master.location_desc) as [Location],max(TSPL_PHYSICAL_STOCK.silo_location) as [Sub Location],(case when max( TSPL_PHYSICAL_STOCK.is_milk)=0 then '' else 'Milk Type' end) as [Type],case when max(TSPL_PHYSICAL_STOCK.Is_Posted)=1 then 'Posted' else 'Pending' end as Posted,max(TSPL_PHYSICAL_STOCK.Multiple_Location) as Multiple_Location from TSPL_PHYSICAL_STOCK "
        qry += "left outer join tspl_location_master on tspl_location_master.location_code=TSPL_PHYSICAL_STOCK.location group by TSPL_PHYSICAL_STOCK.physical_no ) xx "

        Dim str As String = ""
        str = clsCommon.ShowSelectForm("PHYDOCFND", qry, "Code", whrCls, strCurrCode, "Code", isButtonClicked)

        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As clsPhysicalstock, ByVal physicalNo As String, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, physicalNo, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj1 As clsPhysicalstock, ByVal physicalNo As String, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Dim whrCls As String = ""

        Try
            If (obj1.Arr IsNot Nothing AndAlso obj1.Arr.Count > 0) Then
                If isNewEntry Then
                    qry = "select max(Physical_No) from tspl_physical_stock"
                    physicalNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                    If clsCommon.myLen(physicalNo) > 0 Then
                        physicalNo = clsCommon.incval(physicalNo)
                    Else
                        physicalNo = "PHYSTK0000000000000000001"
                    End If
                End If
                If Not isNewEntry Then
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, physicalNo, "tspl_physical_stock", "Physical_No", "TSPL_ADJUSTMENT_HEADER", "against_physical_stock_no", trans)
                End If
                obj1.Physical_No = physicalNo
                clsDBFuncationality.ExecuteNonQuery("delete from tspl_physical_stock where physical_no='" + physicalNo + "'", trans)
                clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_SERIAL_ITEM where Document_Type='IC-AD' and Document_Code in (select adjustment_no from TSPL_ADJUSTMENT_HEADER where against_physical_stock_no='" + physicalNo + "')", trans)
                clsDBFuncationality.ExecuteNonQuery("delete from TSPL_INVENTORY_MOVEMENT where trans_type='IC-AD' and source_doc_no in (select adjustment_no from TSPL_ADJUSTMENT_HEADER where against_physical_stock_no='" + physicalNo + "')", trans)
                clsDBFuncationality.ExecuteNonQuery("delete from TSPL_INVENTORY_MOVEMENT_NEW where trans_type='IC-AD' and source_doc_no in (select adjustment_no from TSPL_ADJUSTMENT_HEADER where against_physical_stock_no='" + physicalNo + "')", trans)
                clsDBFuncationality.ExecuteNonQuery("delete from TSPL_ADJUSTMENT_DETAIL where adjustment_no in (select adjustment_no from TSPL_ADJUSTMENT_HEADER where against_physical_stock_no='" + physicalNo + "')", trans)
                clsDBFuncationality.ExecuteNonQuery("delete from TSPL_ADJUSTMENT_HEADER where against_physical_stock_no='" + physicalNo + "'", trans)
                clsBatchInventory.DeleteData("PH-ST", physicalNo, trans)
                Dim ii As Integer = 1
                For Each obj As clsPhysicalstock In obj1.Arr
                    Dim coll As New Hashtable()
                    Dim Entrydate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

                    If isNewEntry Then
                        obj.Physical_No = physicalNo
                    End If
                    clsCommon.AddColumnsForChange(coll, "GL_Account_Inventroy_Ctrl", obj.GL_Account_Inventroy_Ctrl, True)
                    clsCommon.AddColumnsForChange(coll, "GL_Account", obj.GL_Account, True)
                    clsCommon.AddColumnsForChange(coll, "Physical_No", obj.Physical_No)
                    clsCommon.AddColumnsForChange(coll, "Is_Milk", obj.Is_Milk)
                    clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                    clsCommon.AddColumnsForChange(coll, "Stock_Date", clsCommon.GetPrintDate(obj.Stock_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Silo_Location", obj.Location_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Location", obj.Main_Location, True)
                    clsCommon.AddColumnsForChange(coll, "Line_No", ii)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                    clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                    clsCommon.AddColumnsForChange(coll, "Stock_Unit", obj.Stock_Unit)
                    clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)
                    clsCommon.AddColumnsForChange(coll, "Nill_Balance", IIf(obj.Nill_Balance, 1, 0))
                    clsCommon.AddColumnsForChange(coll, "Existing_Qty", obj.Existing_Qty)
                    clsCommon.AddColumnsForChange(coll, "Existing_SNF_Pers", obj.Existing_SNF_Pers)
                    clsCommon.AddColumnsForChange(coll, "Existing_SNF_Kg", obj.Existing_SNF_Kg)
                    clsCommon.AddColumnsForChange(coll, "Existing_SNF_Amt", obj.Existing_SNF_Amt)
                    clsCommon.AddColumnsForChange(coll, "Existing_FAT_Pers", obj.Existing_FAT_Pers)
                    clsCommon.AddColumnsForChange(coll, "Existing_FAT_Kg", obj.Existing_FAT_Kg)
                    clsCommon.AddColumnsForChange(coll, "Existing_FAT_Amt", obj.Existing_FAT_Amt)
                    clsCommon.AddColumnsForChange(coll, "Existing_Amount", obj.Existing_Amount)

                    clsCommon.AddColumnsForChange(coll, "Physical_Qty", obj.Physical_Qty)
                    clsCommon.AddColumnsForChange(coll, "FAT_Kg", obj.FAT_Kg)
                    clsCommon.AddColumnsForChange(coll, "FAT_Pers", obj.FAT_Pers)
                    clsCommon.AddColumnsForChange(coll, "FAT_Amt", obj.FAT_Amt)
                    clsCommon.AddColumnsForChange(coll, "SNF_Kg", obj.SNF_Kg)
                    clsCommon.AddColumnsForChange(coll, "SNF_Pers", obj.SNF_Pers)
                    clsCommon.AddColumnsForChange(coll, "SNF_Amt", obj.SNF_Amt)
                    clsCommon.AddColumnsForChange(coll, "Amt", obj.Amt)
                    clsCommon.AddColumnsForChange(coll, "Multiple_Location", IIf(obj.Multiple_Location, 1, 0))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", Entrydate)
                    clsCommon.AddColumnsForChange(coll, "Comp_code", objCommonVar.CurrentCompanyCode)

                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", Entrydate)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PHYSICAL_STOCK", OMInsertOrUpdate.Insert, "", trans)

                    clsBatchInventory.SaveData("PH-ST", obj.Physical_No, obj.Stock_Date, IIf((obj.Existing_Qty - obj.Physical_Qty) < 0, "I", "O"), obj.Item_Code, obj.Main_Location, ii, obj.MRP, obj.Stock_Unit, obj.arrBatchItem, trans)
                    ii += 1
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String)
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "select adjustment_no from TSPL_ADJUSTMENT_HEADER where against_physical_stock_no='" + strDocNo + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code in('IC-AD', 'GL-JE') and Source_Doc_No='" + clsCommon.myCstr(dr("adjustment_no")) + "'", tran)
                    If clsCommon.myLen(VoucherNo) > 0 Then
                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", tran)
                        qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, tran)
                        qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    End If

                    qry = "update TSPL_BATCH_ITEM set Against_Inv_Movement_Trans_Id=null where Against_Inv_Movement_Trans_Id in (select Trans_Id from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + clsCommon.myCstr(dr("adjustment_no")) + "' and Trans_Type='IC-AD')"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = " update TSPL_BATCH_ITEM_New set Against_Inv_Movement_New_Trans_Id=null where Against_Inv_Movement_New_Trans_Id in (select Trans_Id from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + clsCommon.myCstr(dr("adjustment_no")) + "' and Trans_Type='IC-AD') "
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)

                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_INVENTORY_MOVEMENT", "Source_Doc_No", tran)
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_INVENTORY_MOVEMENT_New", "Source_Doc_No", tran)

                    qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + clsCommon.myCstr(dr("adjustment_no")) + "' and Trans_Type='IC-AD'"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_INVENTORY_MOVEMENT_New where Source_Doc_No='" + clsCommon.myCstr(dr("adjustment_no")) + "' and Trans_Type='IC-AD'"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)

                    qry = "Update TSPL_ADJUSTMENT_HEADER set Posted = 'N' where adjustment_no='" + clsCommon.myCstr(dr("adjustment_no")) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)

                    ClsAdjustments.DeleteData(clsCommon.myCstr(dr("adjustment_no")), "", tran)
                Next
            End If

            clsDBFuncationality.ExecuteNonQuery("Update TSPL_PHYSICAL_STOCK set Is_Posted=0 where Physical_No ='" + strDocNo + "'", tran)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "tspl_physical_stock", "Physical_No", tran)
            'Batch Inventory
            qry = "SELECT code FROM TSPL_BATCH_ITEM_INDIRECT WHERE Document_Type='PH-ST' and Document_Code='" + strDocNo + "' order by code"
            dt = clsDBFuncationality.GetDataTable(qry, tran)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT max(code) FROM TSPL_BATCH_ITEM", tran))
                For Each dr As DataRow In dt.Rows
                    qry = clsCommon.incval(qry)
                    clsDBFuncationality.ExecuteNonQuery("Update TSPL_BATCH_ITEM_INDIRECT set code='" + qry + "' where code='" + clsCommon.myCstr(dr("code")) + "' ", tran)
                Next
            End If
            Dim strInvColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_BATCH_ITEM", tran)
            strInvColumns = "[" + strInvColumns.Replace(",", "],[") + "]"
            qry = "INSERT INTO TSPL_BATCH_ITEM (" + strInvColumns + ") SELECT " + strInvColumns + " FROM TSPL_BATCH_ITEM_INDIRECT WHERE Document_Type='PH-ST' and Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "Delete from TSPL_BATCH_ITEM_INDIRECT where Document_Type='PH-ST' and Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
            'End of Batch Inventory

            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True

    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal is_Milk As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim isCheckForPosted As Boolean = True
            Dim qry As String = "select Location,Silo_Location from TSPL_PHYSICAL_STOCK where Physical_No='" + strDocNo + "' group by Location,Silo_Location"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    PostData(strDocNo, isCheckForPosted, trans, clsCommon.myCstr(dr("Location")), clsCommon.myCstr(dr("Silo_Location")), is_Milk)
                    isCheckForPosted = False
                Next
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal location As String, ByVal sublocation As String, ByVal is_Milk As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            PostData(strDocNo, isCheckForPosted, trans, location, sublocation, is_Milk)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal trans As SqlTransaction, ByVal location As String, ByVal sublocation As String, ByVal is_Milk As Boolean) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim qry As String
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim arr As New List(Of clsPhysicalstock)
            arr = clsPhysicalstock.GetData(strDocNo, location, sublocation, is_Milk, NavigatorType.Current, trans, "")
            For Each obj As clsPhysicalstock In arr
                If (arr Is Nothing OrElse clsCommon.myLen(obj.Physical_No) <= 0) Then
                    Throw New Exception("No Data found to Post")
                End If
                If (isCheckForPosted AndAlso obj.Is_Posted = 1) Then
                    Throw New Exception("Already Posted ")
                End If

                qry = "Update TSPL_PHYSICAL_STOCK set Is_Posted=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Physical_No ='" + strDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "tspl_physical_stock", "Physical_No", trans)
                Exit For
            Next
            'Batch Inventory
            Dim strInvColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_BATCH_ITEM", trans)
            strInvColumns = "[" + strInvColumns.Replace(",", "],[") + "]"
            qry = "INSERT INTO TSPL_BATCH_ITEM_INDIRECT (" + strInvColumns + ") SELECT " + strInvColumns + " FROM TSPL_BATCH_ITEM WHERE Document_Type='PH-ST' and Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsBatchInventory.DeleteData("PH-ST", strDocNo, trans)
            'End of Batch Inventory
            SaveAdjustmentData(arr, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Function SaveAdjustmentData(ByVal arr As List(Of clsPhysicalstock), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim obj As New ClsAdjustments()
            obj.Arr = New List(Of ClsAdjustmentsDetails)
            'Dim physicalqty As Decimal = 0
            obj.Adjustment_Type = "ADJ"
            Dim lineNo As Integer = 0
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsPhysicalstock In arr
                    obj.Trans_Type = "In"
                    If objtr.Difference < 0 OrElse objtr.DifferenceAmt < 0 Then
                        obj.Adjustment_Date = objtr.Stock_Date
                        obj.Reference = ""
                        obj.Description = objtr.Description
                        If clsCommon.myLen(obj.Description) > 100 Then
                            obj.Description = obj.Description.Substring(0, 100)
                        End If
                        obj.Reference_Document = ""
                        obj.Document_No = ""
                        obj.Unit_Code = "ALL"
                        obj.ItemType = ""
                        obj.EMP_CODE = ""
                        obj.EMP_NAME = ""
                        obj.Customer_CODE = ""
                        obj.Customer_NAME = ""
                        obj.Against_Physical_Stock_No = objtr.Physical_No
                        obj.Vehicle_Code = ""
                        obj.Vehicle_No = ""
                        obj.Challan_No = ""
                        obj.GateEntry_No = ""
                        obj.IsMilkType = objtr.Is_Milk
                        If objtr.Is_Milk > 0 Then
                            obj.MainLocationCode = objtr.Main_Location
                            obj.MainLocationDesc = clsLocation.GetName(objtr.Main_Location, trans)
                            obj.Loc_Code = objtr.Location_Code
                            obj.Loc_Desc = clsLocation.GetName(objtr.Location_Code, trans)
                        Else
                            obj.Loc_Code = objtr.Location_Code
                            obj.Loc_Desc = clsLocation.GetName(objtr.Location_Code, trans)
                        End If
                        obj.chklocation = "N"
                        obj.Against_Item_Stock_Conversion = ""
                        obj.Against_Item_Stock_Conv_Doc = ""
                        obj.Against_Bulk_Srn_PI_adjustment = ""

                        Dim objArr As New ClsAdjustmentsDetails()
                        objArr.Adjustment_Line_No = lineNo
                        lineNo += 1
                        objArr.Item_Code = objtr.Item_Code
                        objArr.Item_Description = objtr.Item_Desc
                        objArr.Bar_Code = ""
                        objArr.Adjustment_Type = "BI"
                        If objtr.Difference < 0 Then
                            objArr.Item_Quantity = Math.Abs(objtr.Difference)
                        End If
                        If objtr.DifferenceAmt < 0 Then
                            objArr.Item_Cost = Math.Abs(objtr.DifferenceAmt)
                        End If
                        'sanjay Ticket No- TEC/12/03/19-000442
                        If objArr.Item_Quantity > 0 AndAlso objArr.Item_Cost > 0 Then
                            objArr.Unit_Cost = (objArr.Item_Cost / objArr.Item_Quantity)
                        End If
                        'sanjay Ticket No- TEC/12/03/19-000442
                        objArr.Unit_Code = objtr.Stock_Unit
                        objArr.mrp = objtr.MRP
                        objArr.Batch_No = objtr.Batch_No
                        objArr.ItemType = clsItemMaster.GetStoreAdjustmentItemTypeWithTrans(objtr.Item_Code, trans)
                        objArr.Itemstatus = "NEW"
                        objArr.fat_kg = 0
                        objArr.fat_pers = 0
                        objArr.snf_kg = 0
                        objArr.snf_pers = 0
                        objArr.arrSrItem = objtr.arrSrItem

                        If objtr.arrBatchItem IsNot Nothing AndAlso objtr.arrBatchItem.Count > 0 Then
                            For jj As Integer = 0 To objtr.arrBatchItem.Count - 1
                                objtr.arrBatchItem(jj).Parent_Line_No = objArr.Adjustment_Line_No
                            Next
                        End If
                        objArr.arrBatchItem = objtr.arrBatchItem
                        'objArr.PS_GL_Account = objtr.GL_Account
                        'objArr.PS_GL_Account_Inventroy_Ctrl = objtr.GL_Account_Inventroy_Ctrl
                        obj.Arr.Add(objArr)
                    End If

                    '' add fat/snf or both
                    If objtr.FatKgDifference < 0 OrElse objtr.SNFKgDifference < 0 OrElse objtr.FatAmtDifference < 0 OrElse objtr.SNFAmtDifference < 0 Then 'remove -ve sign
                        obj.Adjustment_Date = objtr.Stock_Date
                        obj.Reference = ""
                        obj.Description = objtr.Description
                        If clsCommon.myLen(obj.Description) > 100 Then
                            obj.Description = obj.Description.Substring(0, 100)
                        End If
                        obj.Reference_Document = ""
                        obj.Document_No = ""
                        obj.Unit_Code = "ALL"
                        obj.ItemType = ""
                        obj.EMP_CODE = ""
                        obj.EMP_NAME = ""
                        obj.Customer_CODE = ""
                        obj.Customer_NAME = ""
                        obj.Against_Physical_Stock_No = objtr.Physical_No
                        obj.Vehicle_Code = ""
                        obj.Vehicle_No = ""
                        obj.Challan_No = ""
                        obj.GateEntry_No = ""
                        obj.IsMilkType = objtr.Is_Milk
                        If objtr.Is_Milk > 0 Then
                            obj.MainLocationCode = objtr.Main_Location
                            obj.MainLocationDesc = clsLocation.GetName(objtr.Main_Location, trans)
                            obj.Loc_Code = objtr.Location_Code
                            obj.Loc_Desc = clsLocation.GetName(objtr.Location_Code, trans)
                        Else
                            obj.Loc_Code = objtr.Location_Code
                            obj.Loc_Desc = clsLocation.GetName(objtr.Location_Code, trans)
                        End If

                        obj.chklocation = "N"
                        obj.Against_Item_Stock_Conversion = ""
                        obj.Against_Item_Stock_Conv_Doc = ""
                        obj.Against_Bulk_Srn_PI_adjustment = ""

                        Dim objArr As New ClsAdjustmentsDetails()
                        objArr.Adjustment_Line_No = lineNo
                        lineNo += 1
                        objArr.Item_Code = objtr.Item_Code
                        objArr.Item_Description = objtr.Item_Desc
                        objArr.Bar_Code = ""
                        objArr.Adjustment_Type = "FI"
                        objArr.Item_Quantity = 0
                        objArr.Item_Cost = 0
                        objArr.Unit_Code = objtr.Stock_Unit
                        objArr.mrp = objtr.MRP
                        objArr.Batch_No = objtr.Batch_No
                        objArr.ItemType = clsItemMaster.GetStoreAdjustmentItemTypeWithTrans(objtr.Item_Code, trans)
                        objArr.Itemstatus = "NEW"
                        If objtr.FatKgDifference < 0 Then
                            objArr.fat_kg = Math.Abs(objtr.FatKgDifference)
                            objArr.fat_pers = Math.Abs(objtr.FatPerDifference)
                        End If
                        If objtr.FatAmtDifference < 0 Then
                            objArr.fat_Amt = Math.Abs(objtr.FatAmtDifference)
                        End If

                        If objtr.SNFKgDifference < 0 Then
                            objArr.snf_kg = Math.Abs(objtr.SNFKgDifference)
                            objArr.snf_pers = Math.Abs(objtr.SNFPerDifference)
                        End If
                        If objtr.SNFAmtDifference < 0 Then
                            objArr.snf_Amt = Math.Abs(objtr.SNFAmtDifference)
                        End If
                        objArr.arrSrItem = objtr.arrSrItem
                        obj.Arr.Add(objArr)
                    End If
                Next
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    obj.SaveData(obj, True, "", trans)
                    ClsAdjustments.PostData(obj.Adjustment_No, "Store Adjustment", trans)
                End If

                '====out===
                obj = New ClsAdjustments()
                obj.Arr = New List(Of ClsAdjustmentsDetails)
                lineNo = 1
                For Each objtr As clsPhysicalstock In arr
                    obj.Trans_Type = "Out"
                    If objtr.Difference > 0 OrElse objtr.DifferenceAmt > 0 Then
                        obj.Adjustment_Date = objtr.Stock_Date
                        obj.Reference = ""
                        obj.Description = objtr.Description
                        If clsCommon.myLen(obj.Description) > 100 Then
                            obj.Description = obj.Description.Substring(0, 100)
                        End If
                        obj.Reference_Document = ""
                        obj.Document_No = ""
                        obj.Unit_Code = "ALL"
                        obj.ItemType = ""
                        obj.EMP_CODE = ""
                        obj.EMP_NAME = ""
                        obj.Customer_CODE = ""
                        obj.Customer_NAME = ""
                        obj.Against_Physical_Stock_No = objtr.Physical_No
                        obj.Vehicle_Code = ""
                        obj.Vehicle_No = ""
                        obj.Challan_No = ""
                        obj.GateEntry_No = ""
                        obj.IsMilkType = objtr.Is_Milk
                        If objtr.Is_Milk > 0 Then
                            obj.MainLocationCode = objtr.Main_Location
                            obj.MainLocationDesc = clsLocation.GetName(objtr.Main_Location, trans)
                            obj.Loc_Code = objtr.Location_Code
                            obj.Loc_Desc = clsLocation.GetName(objtr.Location_Code, trans)
                        Else
                            obj.Loc_Code = objtr.Location_Code
                            obj.Loc_Desc = clsLocation.GetName(objtr.Location_Code, trans)
                        End If

                        obj.chklocation = "N"
                        obj.Against_Item_Stock_Conversion = ""
                        obj.Against_Item_Stock_Conv_Doc = ""
                        obj.Against_Bulk_Srn_PI_adjustment = ""

                        Dim objArrOut As New ClsAdjustmentsDetails()
                        objArrOut.Adjustment_Line_No = lineNo
                        lineNo += 1
                        objArrOut.Item_Code = objtr.Item_Code
                        objArrOut.Item_Description = objtr.Item_Desc
                        objArrOut.Bar_Code = ""
                        objArrOut.Adjustment_Type = "BD"
                        If objtr.Difference > 0 Then
                            objArrOut.Item_Quantity = objtr.Difference
                        End If
                        If objtr.DifferenceAmt > 0 Then
                            objArrOut.Item_Cost = objtr.DifferenceAmt
                        End If
                        'sanjay Ticket No- TEC/12/03/19-000442
                        If objArrOut.Item_Quantity > 0 AndAlso objArrOut.Item_Cost > 0 Then
                            objArrOut.Unit_Cost = (objArrOut.Item_Cost / objArrOut.Item_Quantity)
                        End If
                        'sanjay Ticket No- TEC/12/03/19-000442
                        objArrOut.Unit_Code = objtr.Stock_Unit
                        objArrOut.mrp = objtr.MRP
                        objArrOut.Batch_No = objtr.Batch_No
                        objArrOut.ItemType = clsItemMaster.GetStoreAdjustmentItemTypeWithTrans(objtr.Item_Code, trans)
                        objArrOut.Itemstatus = "NEW"
                        objArrOut.fat_kg = 0
                        objArrOut.fat_pers = 0
                        objArrOut.snf_kg = 0
                        objArrOut.snf_pers = 0
                        objArrOut.arrSrItem = objtr.arrSrItem
                        If objtr.arrBatchItem IsNot Nothing AndAlso objtr.arrBatchItem.Count > 0 Then
                            For jj As Integer = 0 To objtr.arrBatchItem.Count - 1
                                objtr.arrBatchItem(jj).Parent_Line_No = objArrOut.Adjustment_Line_No
                            Next
                        End If
                        objArrOut.arrBatchItem = objtr.arrBatchItem
                        obj.Arr.Add(objArrOut)
                    End If
                    '' add fat/snf or both
                    If objtr.FatKgDifference > 0 OrElse objtr.SNFKgDifference > 0 OrElse objtr.FatAmtDifference > 0 Or objtr.SNFAmtDifference > 0 Then
                        obj.Adjustment_Date = objtr.Stock_Date
                        obj.Reference = ""
                        obj.Description = objtr.Description
                        If clsCommon.myLen(obj.Description) > 100 Then
                            obj.Description = obj.Description.Substring(0, 100)
                        End If
                        obj.Reference_Document = ""
                        obj.Document_No = ""
                        obj.Unit_Code = "ALL"
                        obj.ItemType = ""
                        obj.EMP_CODE = ""
                        obj.EMP_NAME = ""
                        obj.Customer_CODE = ""
                        obj.Customer_NAME = ""
                        obj.Against_Physical_Stock_No = objtr.Physical_No
                        obj.Vehicle_Code = ""
                        obj.Vehicle_No = ""
                        obj.Challan_No = ""
                        obj.GateEntry_No = ""
                        obj.IsMilkType = objtr.Is_Milk
                        If objtr.Is_Milk > 0 Then
                            obj.MainLocationCode = objtr.Main_Location
                            obj.MainLocationDesc = clsLocation.GetName(objtr.Main_Location, trans)
                            obj.Loc_Code = objtr.Location_Code
                            obj.Loc_Desc = clsLocation.GetName(objtr.Location_Code, trans)
                        Else
                            obj.Loc_Code = objtr.Location_Code
                            obj.Loc_Desc = clsLocation.GetName(objtr.Location_Code, trans)
                        End If

                        obj.chklocation = "N"
                        obj.Against_Item_Stock_Conversion = ""
                        obj.Against_Item_Stock_Conv_Doc = ""
                        obj.Against_Bulk_Srn_PI_adjustment = ""

                        Dim objArr As New ClsAdjustmentsDetails()
                        objArr.Adjustment_Line_No = lineNo
                        lineNo += 1
                        objArr.Item_Code = objtr.Item_Code
                        objArr.Item_Description = objtr.Item_Desc
                        objArr.Bar_Code = ""
                        objArr.Adjustment_Type = "FI"
                        objArr.Item_Quantity = 0
                        objArr.Item_Cost = 0
                        objArr.Unit_Code = objtr.Stock_Unit
                        objArr.mrp = objtr.MRP
                        objArr.Batch_No = objtr.Batch_No
                        objArr.ItemType = clsItemMaster.GetStoreAdjustmentItemTypeWithTrans(objtr.Item_Code, trans)
                        objArr.Itemstatus = "NEW"
                        If objtr.FatKgDifference > 0 Then
                            objArr.fat_kg = Math.Abs(objtr.FatKgDifference)
                            objArr.fat_pers = Math.Abs(objtr.FatPerDifference)
                        End If
                        If objtr.FatAmtDifference > 0 Then
                            objArr.fat_Amt = Math.Abs(objtr.FatAmtDifference)
                        End If

                        If objtr.SNFKgDifference > 0 Then
                            objArr.snf_kg = Math.Abs(objtr.SNFKgDifference)
                            objArr.snf_pers = Math.Abs(objtr.SNFPerDifference)
                        End If
                        If objtr.SNFAmtDifference > 0 Then
                            objArr.snf_Amt = Math.Abs(objtr.SNFAmtDifference)
                        End If

                        objArr.arrSrItem = objtr.arrSrItem
                        obj.Arr.Add(objArr)
                    End If
                Next
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    obj.SaveData(obj, True, "", trans)
                    ClsAdjustments.PostData(obj.Adjustment_No, "Store Adjustment", trans)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal Physical_No As String, ByVal strLocaion As String, ByVal Sub_Loc_Code As String, ByVal is_Milk As Boolean, ByVal NavType As NavigatorType, ByVal strStockDate As String) As List(Of clsPhysicalstock)
        Try
            Return GetData(Physical_No, strLocaion, Sub_Loc_Code, is_Milk, NavType, Nothing, strStockDate)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal strLocaion As String, ByVal Sub_Loc_Code As String, ByVal Is_Milk As Boolean, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal strStockDate As String) As List(Of clsPhysicalstock)
        Dim arr As New List(Of clsPhysicalstock)
        Try
            Dim qry As String = ""
            Dim isInvRead As Boolean = True
            Dim strStockByDate As String = ""
            If clsCommon.myLen(strStockDate) > 0 Then
                If Is_Milk = False Then
                    strStockByDate = "  where convert (date,TSPL_INVENTORY_MOVEMENT.Punching_Date,103) <= convert(date, '" + clsCommon.GetPrintDate(strStockDate, "dd/MMM/yyyy") + "',103) "
                Else
                    strStockByDate = "  where convert (date,TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date,103) <= convert(date, '" + clsCommon.GetPrintDate(strStockDate, "dd/MMM/yyyy") + "',103) "
                End If
            End If
            If clsCommon.myLen(strLocaion) <= 0 OrElse clsCommon.myLen(strDocNo) > 0 Then
                qry = "select TSPL_PHYSICAL_STOCK.*,TabInvCtrl.Description as GL_Account_Inventroy_CtrlName,TabGL.Description as GL_AccountName from TSPL_PHYSICAL_STOCK " + Environment.NewLine +
                    "left outer join TSPL_GL_ACCOUNTS as TabInvCtrl  on TabInvCtrl .Account_Code=TSPL_PHYSICAL_STOCK.GL_Account_Inventroy_Ctrl" + Environment.NewLine +
                    "left outer join TSPL_GL_ACCOUNTS as TabGL on TabGL.Account_Code=TSPL_PHYSICAL_STOCK.GL_Account" + Environment.NewLine +
                    "where 2=2 "
                If clsCommon.myLen(strLocaion) > 0 OrElse clsCommon.myLen(Sub_Loc_Code) > 0 Then
                    qry += " and 2 = case when isnull(TSPL_PHYSICAL_STOCK.Multiple_Location,0)=0 then 2 else case when isnull(Location,'')='" + strLocaion + "' and isnull(Silo_Location,'')='" + Sub_Loc_Code + "' then 2 else 3 end end"
                End If
                Select Case NavType
                    Case NavigatorType.Current
                        qry += " and physical_no='" + strDocNo + "'"
                    Case NavigatorType.First
                        qry += " and physical_no in (select min(physical_no) from TSPL_PHYSICAL_STOCK)"
                    Case NavigatorType.Last
                        qry += " and physical_no in (select max(physical_no) from TSPL_PHYSICAL_STOCK)"
                    Case NavigatorType.Next
                        qry += " and physical_no in (select min(physical_no) from TSPL_PHYSICAL_STOCK where physical_no>'" + strDocNo + "')"
                    Case NavigatorType.Previous
                        qry += " and physical_no in (select max(physical_no) from TSPL_PHYSICAL_STOCK where physical_no<'" + strDocNo + "')"
                End Select
                isInvRead = False
            Else
                Dim whrcls As String = ""
                If clsCommon.myLen(strLocaion) > 0 Then
                    whrcls = " and TSPL_PHYSICAL_STOCK.location='" + strLocaion + "'"
                End If
                If clsCommon.myLen(Sub_Loc_Code) > 0 Then
                    whrcls += " and TSPL_PHYSICAL_STOCK.silo_location='" + Sub_Loc_Code + "'"
                End If

                qry = "select   TSPL_PHYSICAL_STOCK.physical_no,TSPL_PHYSICAL_STOCK.Description,TSPL_PHYSICAL_STOCK.Stock_Date,TSPL_PHYSICAL_STOCK.Silo_Location as location_code,TSPL_PHYSICAL_STOCK.Location as main_location,TSPL_PHYSICAL_STOCK.Item_Code"
                qry += ",TSPL_PHYSICAL_STOCK.Existing_Qty as qty,TSPL_PHYSICAL_STOCK.existing_fat_kg as fat_kg,TSPL_PHYSICAL_STOCK.existing_snf_kg as snf_kg,TSPL_PHYSICAL_STOCK.Stock_Unit as uom,TSPL_PHYSICAL_STOCK.MRP,TSPL_PHYSICAL_STOCK.Batch_No,TSPL_PHYSICAL_STOCK.Physical_Qty,TSPL_PHYSICAL_STOCK.FAT_Kg as Phy_Fat_Kg,TSPL_PHYSICAL_STOCK.SNF_Kg as Phy_Snf_Kg,TSPL_PHYSICAL_STOCK.Is_Milk,TSPL_PHYSICAL_STOCK.Existing_Amount,TSPL_PHYSICAL_STOCK.Existing_FAT_Amt,TSPL_PHYSICAL_STOCK.Existing_SNF_Amt    from TSPL_PHYSICAL_STOCK where 2=2 "
                Select Case NavType
                    Case NavigatorType.Current
                        qry += " and physical_no='" + strDocNo + "' " + whrcls + ""
                    Case NavigatorType.First
                        qry += " and physical_no in (select min(physical_no) from TSPL_PHYSICAL_STOCK where 1=1 " + whrcls + ")"
                    Case NavigatorType.Last
                        qry += " and physical_no in (select max(physical_no) from TSPL_PHYSICAL_STOCK where 1=1 " + whrcls + ")"
                    Case NavigatorType.Next
                        qry += " and physical_no in (select min(physical_no) from TSPL_PHYSICAL_STOCK where physical_no>'" + strDocNo + "' " + whrcls + ")"
                    Case NavigatorType.Previous
                        qry += " and physical_no in (select max(physical_no) from TSPL_PHYSICAL_STOCK where physical_no<'" + strDocNo + "' " + whrcls + ")"
                End Select
                If Is_Milk Then
                    qry += " union all "
                    qry += " select  axa.physical_no,axa.Description,axa.Stock_Date,axa.location_code,axa.main_location,axa.Item_Code,axa.qty ,axa.fat_kg ,axa.snf_kg as snf_kg,axa.UOM as UOM_Code,axa.MRP,axa.Batch_No,axa.Physical_Qty,axa.Phy_Fat_Kg,axa.Phy_Snf_Kg,axa.is_milk,axa.Avg_Cost as Existing_Amount,axa.Fat_Amt,axa.SNF_Amt from (" + Environment.NewLine +
                    "select '' as physical_no,'' as Description,cast('01/01/1900' as DATEtime) as Stock_Date,aa.Location_Code,aa.main_location,aa.Item_Code,sum(aa.qty) as qty,0 as fat_pers,sum(aa.Fat_KG) as Fat_KG,0 as snf_pers,sum(aa.SNF_KG) as SNF_KG,aa.UOM,aa.MRP,aa.Batch_No,0 as Physical_Qty,0 as Phy_Fat_Pers,0 as Phy_Fat_Kg,0 as Phy_Snf_Pers,0 as Phy_Snf_Kg,1 as Is_Milk,sum(Avg_Cost) as Avg_Cost,sum(Fat_Amt) as Fat_Amt,sum(SNF_Amt) SNF_Amt  from ( " + Environment.NewLine +
                    "select TSPL_INVENTORY_MOVEMENT_NEW.Trans_Id,TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,TSPL_INVENTORY_MOVEMENT_NEW.main_location,TSPL_INVENTORY_MOVEMENT_NEW.Item_Code," + Environment.NewLine +
                    "isnull( TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty,0) *case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as qty," + Environment.NewLine +
                    "isnull( TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG,0) *case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as Fat_KG," + Environment.NewLine +
                    "isnull( TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG,0) *case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as SNF_KG," + Environment.NewLine +
                    "TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM as UOM,TSPL_INVENTORY_MOVEMENT_NEW.MRP,TSPL_INVENTORY_MOVEMENT_NEW.Batch_No " + Environment.NewLine +
                    ",isnull( TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost,0) *case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as Avg_Cost" + Environment.NewLine +
                    ",isnull( TSPL_INVENTORY_MOVEMENT_NEW.Fat_Amt,0) * case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as Fat_Amt " + Environment.NewLine +
                    ",isnull( TSPL_INVENTORY_MOVEMENT_NEW.SNF_Amt,0) * case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as SNF_Amt" + Environment.NewLine +
                    "from TSPL_INVENTORY_MOVEMENT_NEW " + Environment.NewLine + strStockByDate + Environment.NewLine +
                    ")aa group by aa.Location_Code,aa.Item_Code,aa.UOM,aa.MRP,aa.Batch_No,aa.main_location" + Environment.NewLine +
                    ")axa  where 1=1  "
                    If clsCommon.myLen(strLocaion) > 0 AndAlso clsCommon.myLen(Sub_Loc_Code) <= 0 Then
                        qry += " and axa.Location_Code='" + strLocaion + "'"
                    End If
                    If clsCommon.myLen(Sub_Loc_Code) > 0 Then
                        qry += " and axa.location_code='" + Sub_Loc_Code + "'"
                    End If
                Else
                    qry += " union all "
                    qry += "select  axa.physical_no,axa.Description,axa.Stock_Date,axa.location_code,axa.main_location,axa.Item_Code,(axa.qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/finalcnvrsn.Conversion_Factor as qty,(axa.fat_kg*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/finalcnvrsn.Conversion_Factor as fat_kg,(axa.snf_kg*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/finalcnvrsn.Conversion_Factor as snf_kg,finalcnvrsn.UOM_Code,axa.MRP,axa.Batch_No,axa.Physical_Qty,axa.Phy_Fat_Kg,axa.Phy_Snf_Kg,axa.is_milk,axa.Existing_Amount,0 as Existing_FAT_Amt,0 as Existing_SNF_Amt from ( "
                    qry += "select '' as physical_no,'' as Description,cast('01/01/1900' as DATEtime) as Stock_Date,'' as location_code,aa.Location_Code as main_location,aa.Item_Code,sum(aa.qty) as qty,0 as fat_pers,0 as fat_kg,0 as snf_pers,0 as snf_kg,aa.UOM,aa.MRP,aa.Batch_No,0 as Physical_Qty,0 as Phy_Fat_Pers,0 as Phy_Fat_Kg,0 as Phy_Snf_Pers,0 as Phy_Snf_Kg,0 as Is_Milk,sum(Existing_Amount) as Existing_Amount from ( "
                    qry += "select TSPL_INVENTORY_MOVEMENT.Trans_Id,TSPL_INVENTORY_MOVEMENT.Location_Code,TSPL_INVENTORY_MOVEMENT.Item_Code,isnull(TSPL_INVENTORY_MOVEMENT.Stock_Qty,0)*case when TSPL_INVENTORY_MOVEMENT.inout='i' then 1 else -1 end as qty ,TSPL_INVENTORY_MOVEMENT.Stock_UOM  as UOM,TSPL_INVENTORY_MOVEMENT.MRP,TSPL_INVENTORY_MOVEMENT.Batch_No,isnull( TSPL_INVENTORY_MOVEMENT.Avg_Cost,0) *case when TSPL_INVENTORY_MOVEMENT.inout='i' then 1 else -1 end as Existing_Amount from TSPL_INVENTORY_MOVEMENT  " + strStockByDate + " "
                    qry += ")aa group by aa.Location_Code,aa.Item_Code,aa.UOM,aa.MRP,aa.Batch_No)axa "
                    qry += "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=axa.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=axa.UOM "
                    qry += "left outer join TSPL_ITEM_UOM_DETAIL finalcnvrsn on finalcnvrsn.Item_Code=axa.Item_Code and finalcnvrsn.Stocking_Unit='Y' where 1=1 "
                    If clsCommon.myLen(strLocaion) > 0 Then
                        qry += " and axa.main_location='" + strLocaion + "'"
                    End If
                End If

                Dim colExtraGroup As String = ""
                Dim colExtraSel As String = ""

                colExtraGroup = ""
                colExtraSel = ",0 as MRP"

                colExtraGroup = colExtraGroup
                colExtraSel = colExtraSel & ",'' as Batch_No"

                Dim str As String = qry
                qry = "select row_number() over (order by final.item_code) as line_no,final.physical_no,final.Description,convert(date,SYSDATETIME(),103) as Stock_Date,final.location_code as Silo_Location,final.main_location  as Location,final.Item_Code" & colExtraSel & ",sum(final.qty) as Existing_Qty,sum(final.fat_kg) as existing_fat_kg "
                qry += ",sum(final.snf_kg) as existing_snf_kg,final.uom as Stock_Unit,sum(final.Physical_Qty) as Physical_Qty,sum(final.Phy_Fat_Kg) as fat_kg,sum(final.Phy_Snf_Kg) as snf_kg,final.Is_Milk,sum(Existing_Amount) as Existing_Amount,0 as Amt,sum(Existing_FAT_Amt) as Existing_FAT_Amt,sum(Existing_SNF_Amt) as Existing_SNF_Amt  from (" + str + ") final"
                qry += " group by final.physical_no,final.Description,final.Stock_Date,final.location_code ,final.main_location,final.Item_Code,final.uom,final.Is_Milk " & colExtraGroup & ""

                qry = "select xx.*,0 as Nill_Balance,TabGLOverride.Account_Code as GL_Account_Inventroy_Ctrl,TabGLOverride.Description as GL_Account_Inventroy_CtrlName,'' as GL_Account,'' as GL_AccountName,0 as SNF_Amt,0 as FAT_Amt from (" + qry + ")xx left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=xx.Item_Code" + Environment.NewLine +
                    "left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code " + Environment.NewLine +
                    "left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account " + Environment.NewLine +
                    "left outer join TSPL_GL_ACCOUNTS as TabGLOverride on TabGLOverride.Account_Seg_Code1=TSPL_GL_ACCOUNTS.Account_Seg_Code1 and  TabGLOverride.Account_Seg_Code7='" + clsLocation.GetSegmentCode(strLocaion, trans) + "'"
                isInvRead = True
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim obj As New clsPhysicalstock()
                    obj.Physical_No = clsCommon.myCstr(dr("physical_no"))
                    obj.Description = clsCommon.myCstr(dr("description"))
                    obj.Is_Milk = CInt(clsCommon.myCdbl(dr("is_milk")))
                    obj.Stock_Date = clsCommon.myCDate(dr("STOCK_DATE"))
                    If clsCommon.myLen(dr.Item("Silo_Location")) > 0 Then
                        obj.Main_Location = clsCommon.myCstr(dr("Location"))
                        obj.Location_Code = clsCommon.myCstr(dr("Silo_Location"))
                    Else
                        obj.Main_Location = ""
                        obj.Location_Code = clsCommon.myCstr(dr("Location"))
                    End If
                    obj.Nill_Balance = (clsCommon.myCdbl(dr("Nill_Balance")) = 1)
                    obj.Line_No = clsCommon.myCstr(dr("line_no"))
                    obj.Item_Code = clsCommon.myCstr(dr("item_code"))
                    obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                    obj.Stock_Unit = clsCommon.myCstr(dr("stock_unit"))
                    obj.Batch_No = clsCommon.myCstr(dr("batch_no"))
                    obj.MRP = clsCommon.myCstr(dr("mrp"))
                    obj.Existing_Qty = clsCommon.myCdbl(dr("Existing_Qty"))
                    obj.Existing_FAT_Kg = clsCommon.myCdbl(dr("Existing_FAT_Kg"))
                    obj.Existing_SNF_Kg = clsCommon.myCdbl(dr("Existing_SNF_Kg"))

                    obj.Existing_FAT_Amt = clsCommon.myCdbl(dr("Existing_FAT_Amt"))
                    obj.Existing_SNF_Amt = clsCommon.myCdbl(dr("Existing_SNF_Amt"))
                    obj.Existing_Amount = clsCommon.myCdbl(dr("Existing_Amount"))
                    obj.FAT_Amt = clsCommon.myCdbl(dr("FAT_Amt"))
                    obj.SNF_Amt = clsCommon.myCdbl(dr("SNF_Amt"))
                    obj.Amt = clsCommon.myCdbl(dr("Amt"))

                    obj.GL_Account_Inventroy_Ctrl = clsCommon.myCstr(dr("GL_Account_Inventroy_Ctrl"))
                    obj.GL_Account_Inventroy_CtrlName = clsCommon.myCstr(dr("GL_Account_Inventroy_CtrlName"))
                    obj.GL_Account = clsCommon.myCstr(dr("GL_Account"))
                    obj.GL_AccountName = clsCommon.myCstr(dr("GL_AccountName"))

                    If clsCommon.myCdbl(dr("Existing_Qty")) <> 0 Then
                        obj.Existing_FAT_Pers = System.Math.Round((clsCommon.myCdbl(dr("Existing_FAT_Kg")) * 100) / clsCommon.myCdbl(dr("Existing_Qty")), 2)
                        obj.Existing_SNF_Pers = System.Math.Round((clsCommon.myCdbl(dr("Existing_SNF_Kg")) * 100) / clsCommon.myCdbl(dr("Existing_Qty")), 2)
                    Else
                        obj.Existing_FAT_Pers = 0
                        obj.Existing_SNF_Pers = 0
                    End If


                    obj.Physical_Qty = clsCommon.myCdbl(dr("Physical_Qty"))
                    obj.FAT_Kg = clsCommon.myCdbl(dr("FAT_Kg"))
                    obj.SNF_Kg = clsCommon.myCdbl(dr("SNF_Kg"))
                    If clsCommon.myCdbl(dr("Physical_Qty")) <> 0 Then
                        obj.FAT_Pers = System.Math.Round((clsCommon.myCdbl(dr("FAT_Kg")) * 100) / clsCommon.myCdbl(dr("Physical_Qty")), 2)
                        obj.SNF_Pers = System.Math.Round((clsCommon.myCdbl(dr("SNF_Kg")) * 100) / clsCommon.myCdbl(dr("Physical_Qty")), 2)
                    Else
                        obj.FAT_Pers = 0
                        obj.SNF_Pers = 0
                    End If

                    obj.Difference = clsCommon.myCdbl(obj.Existing_Qty - obj.Physical_Qty)
                    obj.SNFKgDifference = clsCommon.myCdbl(obj.Existing_SNF_Kg - obj.SNF_Kg)
                    obj.FatKgDifference = clsCommon.myCdbl(obj.Existing_FAT_Kg - obj.FAT_Kg)

                    obj.FatAmtDifference = clsCommon.myCdbl(obj.Existing_FAT_Amt - obj.FAT_Amt)
                    obj.SNFAmtDifference = clsCommon.myCdbl(obj.Existing_SNF_Amt - obj.SNF_Amt)
                    obj.DifferenceAmt = clsCommon.myCdbl(obj.Existing_Amount - obj.Amt)

                    If clsCommon.myCdbl(obj.Difference) <> 0 Then
                        obj.FatPerDifference = System.Math.Round((clsCommon.myCdbl(obj.FatKgDifference) * 100) / clsCommon.myCdbl(obj.Difference), 2)
                        obj.SNFPerDifference = System.Math.Round((clsCommon.myCdbl(obj.SNFKgDifference) * 100) / clsCommon.myCdbl(obj.Difference), 2)
                    Else
                        obj.FatPerDifference = 0
                        obj.SNFPerDifference = 0
                    End If
                    obj.arrBatchItem = clsBatchInventory.GetData("PH-ST", obj.Physical_No, obj.Item_Code, obj.Line_No, trans)
                    If Not isInvRead Then
                        Dim doc_no As String = ""
                        Dim line_No As Integer = 0
                        doc_no = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select adjustment_no from TSPL_ADJUSTMENT_DETAIL where item_code='" + obj.Item_Code + "' and mrp='" + clsCommon.myCstr(obj.MRP) + "' and batch_no='" + obj.Batch_No + "' and unit_code='" + obj.Stock_Unit + "' and adjustment_no in (select adjustment_no from TSPL_ADJUSTMENT_HEADER where against_physical_stock_no='" + obj.Physical_No + "')", trans))
                        line_No = CInt(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select adjustment_line_no from TSPL_ADJUSTMENT_DETAIL where item_code='" + obj.Item_Code + "' and mrp='" + clsCommon.myCstr(obj.MRP) + "' and batch_no='" + obj.Batch_No + "' and unit_code='" + obj.Stock_Unit + "' and adjustment_no in (select adjustment_no from TSPL_ADJUSTMENT_HEADER where against_physical_stock_no='" + obj.Physical_No + "')", trans)))
                        obj.Is_Posted = CInt(clsCommon.myCdbl(dr("Is_Posted")))
                        obj.arrSrItem = clsSerializeInvenotry.GetData("IC-AD", doc_no, obj.Item_Code, line_No, trans)
                    End If
                    arr.Add(obj)
                Next
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Shared Function GetDataForFirstTime(ByVal strDocNo As String, ByVal strLocaion As String, ByVal Sub_Loc_Code As String, ByVal Is_Milk As Boolean, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal strStockDate As String, Optional ByVal strItemType As String = "", Optional ByVal strInventoryAccount As String = "") As DataTable
        Return GetDataForFirstTime(Nothing, strDocNo, strLocaion, Sub_Loc_Code, Is_Milk, NavType, trans, strStockDate, strItemType, strInventoryAccount)
    End Function

    Public Shared Function GetDataForFirstTime(ByVal ArrItem As ArrayList, ByVal strDocNo As String, ByVal strLocaion As String, ByVal Sub_Loc_Code As String, ByVal Is_Milk As Boolean, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal strStockDate As String, Optional ByVal strItemType As String = "", Optional ByVal strInventoryAccount As String = "") As DataTable
        Dim dt As DataTable = Nothing
        Try
            Dim qry As String = ""
            Dim isInvRead As Boolean = True
            Dim strStockByDate As String = ""
            If clsCommon.myLen(strStockDate) > 0 Then
                If Is_Milk = False Then
                    strStockByDate = "  where convert (date,TSPL_INVENTORY_MOVEMENT.Punching_Date,103) <= convert(date, '" + clsCommon.GetPrintDate(strStockDate, "dd/MMM/yyyy") + "',103) "
                Else
                    strStockByDate = "  where convert (date,TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date,103) <= convert(date, '" + clsCommon.GetPrintDate(strStockDate, "dd/MMM/yyyy") + "',103) "
                End If
            End If
            If clsCommon.myLen(strLocaion) <= 0 OrElse clsCommon.myLen(strDocNo) > 0 Then
                qry = "select TSPL_PHYSICAL_STOCK.*,TabInvCtrl.Description as GL_Account_Inventroy_CtrlName,TabGL.Description as GL_AccountName from TSPL_PHYSICAL_STOCK " + Environment.NewLine +
                    "left outer join TSPL_GL_ACCOUNTS as TabInvCtrl  on TabInvCtrl .Account_Code=TSPL_PHYSICAL_STOCK.GL_Account_Inventroy_Ctrl" + Environment.NewLine +
                    "left outer join TSPL_GL_ACCOUNTS as TabGL on TabGL.Account_Code=TSPL_PHYSICAL_STOCK.GL_Account" + Environment.NewLine +
                    "where 2=2 and physical_no='' "
                isInvRead = False
            Else
                Dim whrcls As String = ""
                If clsCommon.myLen(strLocaion) > 0 Then
                    whrcls = " and TSPL_PHYSICAL_STOCK.location='" + strLocaion + "'"
                End If
                If clsCommon.myLen(Sub_Loc_Code) > 0 Then
                    whrcls += " and TSPL_PHYSICAL_STOCK.silo_location='" + Sub_Loc_Code + "'"
                End If

                qry = "select   TSPL_PHYSICAL_STOCK.physical_no,TSPL_PHYSICAL_STOCK.Description,TSPL_PHYSICAL_STOCK.Stock_Date,TSPL_PHYSICAL_STOCK.Silo_Location as location_code,TSPL_PHYSICAL_STOCK.Location as main_location,TSPL_PHYSICAL_STOCK.Item_Code"
                qry += ",TSPL_PHYSICAL_STOCK.Existing_Qty as qty,TSPL_PHYSICAL_STOCK.existing_fat_kg as fat_kg,TSPL_PHYSICAL_STOCK.existing_snf_kg as snf_kg,TSPL_PHYSICAL_STOCK.Stock_Unit as uom,TSPL_PHYSICAL_STOCK.MRP,TSPL_PHYSICAL_STOCK.Batch_No,TSPL_PHYSICAL_STOCK.Physical_Qty,TSPL_PHYSICAL_STOCK.FAT_Kg as Phy_Fat_Kg,TSPL_PHYSICAL_STOCK.SNF_Kg as Phy_Snf_Kg,TSPL_PHYSICAL_STOCK.Is_Milk,TSPL_PHYSICAL_STOCK.Existing_Amount,TSPL_PHYSICAL_STOCK.Existing_FAT_Amt,TSPL_PHYSICAL_STOCK.Existing_SNF_Amt    from TSPL_PHYSICAL_STOCK where 2=2 and physical_no='' "
                If Is_Milk Then
                    qry += " union all " & Environment.NewLine &
                     " select  axa.physical_no,axa.Description,axa.Stock_Date,axa.location_code,axa.main_location,axa.Item_Code,axa.qty ,axa.fat_kg ,axa.snf_kg as snf_kg,axa.UOM as UOM_Code,axa.MRP,axa.Batch_No,axa.Physical_Qty,axa.Phy_Fat_Kg,axa.Phy_Snf_Kg,axa.is_milk,axa.Avg_Cost as Existing_Amount,axa.Fat_Amt,axa.SNF_Amt from (" + Environment.NewLine +
                    "select '' as physical_no,'' as Description,cast('01/01/1900' as DATEtime) as Stock_Date,aa.Location_Code,aa.main_location,aa.Item_Code,sum(aa.qty) as qty,0 as fat_pers,sum(aa.Fat_KG) as Fat_KG,0 as snf_pers,sum(aa.SNF_KG) as SNF_KG,aa.UOM,aa.MRP,aa.Batch_No,0 as Physical_Qty,0 as Phy_Fat_Pers,0 as Phy_Fat_Kg,0 as Phy_Snf_Pers,0 as Phy_Snf_Kg,1 as Is_Milk,sum(Avg_Cost) as Avg_Cost,sum(Fat_Amt) as Fat_Amt,sum(SNF_Amt) SNF_Amt  from ( " + Environment.NewLine +
                    "select TSPL_INVENTORY_MOVEMENT_NEW.Trans_Id,TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,TSPL_INVENTORY_MOVEMENT_NEW.main_location,TSPL_INVENTORY_MOVEMENT_NEW.Item_Code," + Environment.NewLine +
                    "isnull( TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty,0) *case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as qty," + Environment.NewLine +
                    "isnull( TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG,0) *case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as Fat_KG," + Environment.NewLine +
                    "isnull( TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG,0) *case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as SNF_KG," + Environment.NewLine +
                    "TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM as UOM,TSPL_INVENTORY_MOVEMENT_NEW.MRP,TSPL_INVENTORY_MOVEMENT_NEW.Batch_No " + Environment.NewLine +
                    ",isnull( TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost,0) *case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as Avg_Cost" + Environment.NewLine +
                    ",isnull( TSPL_INVENTORY_MOVEMENT_NEW.Fat_Amt,0) * case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as Fat_Amt " + Environment.NewLine +
                    ",isnull( TSPL_INVENTORY_MOVEMENT_NEW.SNF_Amt,0) * case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as SNF_Amt" + Environment.NewLine +
                    "from TSPL_INVENTORY_MOVEMENT_NEW " + Environment.NewLine + strStockByDate + Environment.NewLine +
                    ")aa group by aa.Location_Code,aa.Item_Code,aa.UOM,aa.MRP,aa.Batch_No,aa.main_location" + Environment.NewLine +
                    ")axa  where 1=1  "
                    If clsCommon.myLen(strLocaion) > 0 AndAlso clsCommon.myLen(Sub_Loc_Code) <= 0 Then
                        qry += " and axa.Location_Code='" + strLocaion + "'"
                    End If
                    If clsCommon.myLen(Sub_Loc_Code) > 0 Then
                        qry += " and axa.location_code='" + Sub_Loc_Code + "'"
                    End If
                Else
                    qry += " union all " & Environment.NewLine &
                    "select  axa.physical_no,axa.Description,axa.Stock_Date,axa.location_code,axa.main_location,axa.Item_Code,(axa.qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/finalcnvrsn.Conversion_Factor as qty,(axa.fat_kg*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/finalcnvrsn.Conversion_Factor as fat_kg,(axa.snf_kg*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/finalcnvrsn.Conversion_Factor as snf_kg,finalcnvrsn.UOM_Code,axa.MRP,axa.Batch_No,axa.Physical_Qty,axa.Phy_Fat_Kg,axa.Phy_Snf_Kg,axa.is_milk,axa.Existing_Amount,0 as Existing_FAT_Amt,0 as Existing_SNF_Amt from ( " & Environment.NewLine &
                    "select '' as physical_no,'' as Description,cast('01/01/1900' as DATEtime) as Stock_Date,'' as location_code,aa.Location_Code as main_location,aa.Item_Code,sum(aa.qty) as qty,0 as fat_pers,0 as fat_kg,0 as snf_pers,0 as snf_kg,aa.UOM,aa.MRP,aa.Batch_No,0 as Physical_Qty,0 as Phy_Fat_Pers,0 as Phy_Fat_Kg,0 as Phy_Snf_Pers,0 as Phy_Snf_Kg,0 as Is_Milk,sum(Existing_Amount) as Existing_Amount from ( " & Environment.NewLine &
                    "select TSPL_INVENTORY_MOVEMENT.Trans_Id,TSPL_INVENTORY_MOVEMENT.Location_Code,TSPL_INVENTORY_MOVEMENT.Item_Code,isnull(TSPL_INVENTORY_MOVEMENT.Stock_Qty,0)*case when TSPL_INVENTORY_MOVEMENT.inout='i' then 1 else -1 end as qty ,TSPL_INVENTORY_MOVEMENT.Stock_UOM  as UOM,TSPL_INVENTORY_MOVEMENT.MRP,TSPL_INVENTORY_MOVEMENT.Batch_No,isnull( TSPL_INVENTORY_MOVEMENT.Avg_Cost,0) *case when TSPL_INVENTORY_MOVEMENT.inout='i' then 1 else -1 end as Existing_Amount from TSPL_INVENTORY_MOVEMENT  " + strStockByDate + " " & Environment.NewLine &
                    ")aa group by aa.Location_Code,aa.Item_Code,aa.UOM,aa.MRP,aa.Batch_No)axa " & Environment.NewLine &
                    "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=axa.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=axa.UOM " & Environment.NewLine &
                    "left outer join TSPL_ITEM_UOM_DETAIL finalcnvrsn on finalcnvrsn.Item_Code=axa.Item_Code and finalcnvrsn.Stocking_Unit='Y' where 1=1 " & Environment.NewLine
                    If clsCommon.myLen(strLocaion) > 0 Then
                        qry += " and axa.main_location='" + strLocaion + "'"
                    End If
                End If

                Dim colExtraGroup As String = ""
                Dim colExtraSel As String = ""

                colExtraGroup = ""
                colExtraSel = ",0 as MRP"

                colExtraGroup = colExtraGroup
                colExtraSel = colExtraSel & ",'' as Batch_No"

                Dim str As String = qry
                qry = "select row_number() over (order by final.item_code) as line_no,final.physical_no,final.Description,convert(date,SYSDATETIME(),103) as Stock_Date,final.location_code as Silo_Location,final.main_location  as Location,final.Item_Code" & colExtraSel & ",sum(final.qty) as Existing_Qty,sum(final.fat_kg) as existing_fat_kg " & Environment.NewLine &
                ",sum(final.snf_kg) as existing_snf_kg,final.uom as Stock_Unit,sum(final.Physical_Qty) as Physical_Qty,sum(final.Phy_Fat_Kg) as fat_kg,sum(final.Phy_Snf_Kg) as snf_kg,final.Is_Milk,sum(Existing_Amount) as Existing_Amount,0 as Amt,sum(Existing_FAT_Amt) as Existing_FAT_Amt,sum(Existing_SNF_Amt) as Existing_SNF_Amt  from (" + str + ") final " & Environment.NewLine &
                " group by final.physical_no,final.Description,final.Stock_Date,final.location_code ,final.main_location,final.Item_Code,final.uom,final.Is_Milk " & colExtraGroup & "" & Environment.NewLine

                qry = "select xx.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Is_Batch_Item,TSPL_ITEM_MASTER.Is_Serial_Item,TSPL_ITEM_MASTER.Is_Pick_Auto_SrNo, TSPL_ITEM_MASTER.Is_Serial_Item,0 as Nill_Balance,TabGLOverride.Account_Code as GL_Account_Inventroy_Ctrl,TabGLOverride.Description as GL_Account_Inventroy_CtrlName,'' as GL_Account,'' as GL_AccountName,0 as SNF_Amt,0 as FAT_Amt from (" + qry + ")xx left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=xx.Item_Code" + Environment.NewLine +
                "left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code " + Environment.NewLine +
                "left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account " + Environment.NewLine +
                "left outer join TSPL_GL_ACCOUNTS as TabGLOverride on TabGLOverride.Account_Seg_Code1=TSPL_GL_ACCOUNTS.Account_Seg_Code1 and  TabGLOverride.Account_Seg_Code7='" + clsLocation.GetSegmentCode(strLocaion, trans) + "'  where 2=2 "
                If clsCommon.myLen(strItemType) > 0 Then
                    qry += " and TSPL_ITEM_MASTER.Item_Type in (" + strItemType + ") "
                End If
                If ArrItem IsNot Nothing AndAlso ArrItem.Count > 0 Then
                    qry += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(ArrItem) + ") "
                End If
                If clsCommon.myLen(strInventoryAccount) > 0 Then
                    qry += " and TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account in (" + strInventoryAccount + ")  "
                End If
                isInvRead = True
            End If

            dt = clsDBFuncationality.GetDataTable(qry, trans)

            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "tspl_physical_stock", "Physical_No", tran)
            clsDBFuncationality.ExecuteNonQuery("delete from tspl_physical_stock where physical_no='" + strDocNo + "'", tran)
            clsBatchInventory.DeleteData("PH-ST", strDocNo, tran)
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

'-------------- This Class is Specially Made  to Maintain  table TSPL_Inventory_Movement_New for milk type of data
Public Class clsInventoryMovementNew
#Region "Variables"
    Public itemtypeinventry As String = Nothing
    Public itemstatus As String = Nothing
    Public main_location As String = Nothing
    Public Trans_Type As String = Nothing
    Public InOut As String = Nothing
    Public Location_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Qty As Double = 0
    Public UOM As String = Nothing
    Public Source_Doc_No As String = Nothing
    Public Source_Doc_Date As String = Nothing
    Public Entry_Date As String = Nothing
    Public Basic_Cost As Double = 0
    Public Rec_Cost As Double = 0
    Public Add_Cost As Double = 0
    Public Net_Cost As Double = 0
    Public MRP As Double = 0
    Public ItemType As String = Nothing
    Public Punching_Date As String
    Public Batch_No As String = Nothing
    Public FIFO_Cost As Double = 0
    Public LIFO_Cost As Double = 0
    Public Avg_Cost As Double = 0
    Public Posting_Date As DateTime? = Nothing
    Public MFG_Date As DateTime? = Nothing
    Public Expiry_Date As DateTime? = Nothing
    Public Stock_Qty As Double = 0
    Public Stock_UOM As String = Nothing
    Public FAT_Per As Double = 0
    Public SNF_Per As Double = 0
    Public FAT_KG As Double = 0
    Public SNF_KG As Double = 0
    Public IS_CONSUMPTION As Integer = 0
    ''RICHA AGARWAL 06/07/2015
    Public Fat_Rate As Double = 0
    Public SNF_Rate As Double = 0
    Public Fat_Amt As Double = 0
    Public SNF_Amt As Double = 0
    Public Std_Qty As Double = 0
    Public Is_Scheme_Item As String = String.Empty
    Public Inventory_DrAcc As String = String.Empty
    Public Inventory_CrAcc As String = String.Empty
    ''--------------
    Public Ref_Line_No As Integer
    Public Cust_Code As String = Nothing
    Public Cust_Name As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Other_Location_Code As String = Nothing
    Public Other_Location_Desc As String = Nothing
    Public CalculateAvgCost As Boolean = True
    Public DonNotCalculateAvgFATSNFCost As Boolean = False
    Public CustomCoversionCLR As Decimal ''Not a Table Column
    Public Custom_UOM As String = Nothing
    Public Custom_Coversion_Factor As Decimal = 0

    Public PI_Cost As Decimal
    Public Item_Status As String
    Public Assmbly_Status As String
#End Region

    Public Shared Function DeepCopyObject(ByVal obj As clsInventoryMovementNew) As clsInventoryMovementNew
        Dim objNew As clsInventoryMovementNew = New clsInventoryMovementNew()
        objNew.Trans_Type = obj.Trans_Type
        objNew.InOut = obj.InOut
        objNew.Location_Code = obj.Location_Code
        objNew.Item_Code = obj.Item_Code
        objNew.Item_Desc = obj.Item_Desc
        objNew.Qty = obj.Qty
        objNew.UOM = obj.UOM
        objNew.Source_Doc_No = obj.Source_Doc_No
        objNew.Source_Doc_Date = obj.Source_Doc_Date
        objNew.Entry_Date = obj.Entry_Date
        objNew.Basic_Cost = obj.Basic_Cost
        objNew.Rec_Cost = obj.Rec_Cost
        objNew.Add_Cost = obj.Add_Cost
        objNew.Net_Cost = obj.Net_Cost
        objNew.ItemType = obj.ItemType
        objNew.Punching_Date = obj.Punching_Date
        objNew.MRP = obj.MRP
        objNew.Batch_No = obj.Batch_No
        objNew.MFG_Date = obj.MFG_Date
        objNew.Expiry_Date = obj.Expiry_Date
        objNew.FIFO_Cost = obj.FIFO_Cost
        objNew.LIFO_Cost = obj.LIFO_Cost
        objNew.Avg_Cost = obj.Avg_Cost
        objNew.Posting_Date = obj.Posting_Date
        objNew.PI_Cost = obj.PI_Cost
        objNew.Stock_UOM = obj.Stock_UOM
        objNew.Stock_Qty = obj.Stock_Qty
        objNew.Item_Status = obj.Item_Status
        objNew.Assmbly_Status = obj.Assmbly_Status
        objNew.FAT_Per = obj.FAT_Per
        objNew.SNF_Per = obj.SNF_Per
        objNew.FAT_KG = obj.FAT_KG
        objNew.SNF_KG = obj.SNF_KG
        objNew.main_location = obj.main_location
        objNew.IS_CONSUMPTION = obj.IS_CONSUMPTION
        objNew.Cust_Code = obj.Cust_Code
        objNew.Cust_Name = obj.Cust_Name
        objNew.Vendor_Code = obj.Vendor_Code
        objNew.Vendor_Name = obj.Vendor_Name
        objNew.Other_Location_Code = obj.Other_Location_Code
        objNew.Other_Location_Desc = obj.Other_Location_Desc
        objNew.Fat_Rate = obj.Fat_Rate
        objNew.SNF_Rate = obj.SNF_Rate
        objNew.Fat_Amt = obj.Fat_Amt
        objNew.SNF_Amt = obj.SNF_Amt
        objNew.Std_Qty = obj.Std_Qty
        objNew.Inventory_DrAcc = obj.Inventory_DrAcc
        objNew.Inventory_CrAcc = obj.Inventory_CrAcc
        objNew.Is_Scheme_Item = obj.Is_Scheme_Item
        objNew.Custom_UOM = obj.Custom_UOM
        objNew.Custom_Coversion_Factor = obj.Custom_Coversion_Factor
        objNew.DonNotCalculateAvgFATSNFCost = obj.DonNotCalculateAvgFATSNFCost
        Return objNew
    End Function

    Private Shared Function GetDataWithBatch(ByVal TransType As String, ByVal DocNo As String, ByVal DocDate As DateTime, ByVal arr As List(Of clsInventoryMovementNew), ByVal trans As SqlTransaction)
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isBatchApplyOnInventoryMovement, clsFixedParameterCode.isBatchApplyOnInventoryMovement, trans)) = 0 OrElse clsCommon.CompairString(TransType, "IC-AD") = CompairStringResult.Equal OrElse clsCommon.CompairString(TransType, "SRN") = CompairStringResult.Equal Then
            Return arr
        End If
        Dim arrReturn As New List(Of clsInventoryMovementNew)
        Dim arrItemDone As New List(Of String)
        For Each obj As clsInventoryMovementNew In arr
            If obj.Qty = 0 Then
                Continue For
            End If

            If clsCommon.CompairString(obj.ItemType, "FM") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.ItemType, "FT") = CompairStringResult.Equal Then
                Dim strRI As String = "1*"
                If clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal Then
                    strRI = "-1*"
                End If
                Dim strLOType As String = ""
                If clsCommon.CompairString(TransType, "Transfer") = CompairStringResult.Equal Then
                    strLOType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transfer_Type from TSPL_TRANSFER_HEAD where Transfer_No='" + DocNo + "'", trans))
                    If clsCommon.CompairString(strLOType, "LI") = CompairStringResult.Equal Then
                        strRI = "-1*"
                    End If
                End If


                Dim convFact As Double = clsItemMaster.GetConvertionFactor(obj.Item_Code, obj.UOM, trans)
                Dim dblMRP As Double = obj.MRP * convFact
                Dim qry As String = "select Stock_Qty*Conversion_Factor as Stock_Qty,Batch_No,MFG_Date,Expiry_Date from("
                qry += " select sum(" + strRI + "Stock_Qty * case when inout='I' then 1 else case when inout='O' then -1 else 0 end end) as Stock_Qty,Batch_No,MAX(MFG_Date) as MFG_Date,MAX(Expiry_Date) as Expiry_Date  from("
                qry += " select  Stock_Qty,inout,Batch_No,MFG_Date,Expiry_Date  "
                qry += " from TSPL_INVENTORY_MOVEMENT_new "
                qry += " where TSPL_INVENTORY_MOVEMENT_new.Item_Code='" + obj.Item_Code + "' and (Qty*MRP/Stock_Qty)=" + clsCommon.myCstr(dblMRP) + " and Stock_Qty<>0 "
                If clsCommon.CompairString(TransType, "Transfer") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(strLOType, "LI") = CompairStringResult.Equal Then
                        qry += " and TSPL_INVENTORY_MOVEMENT_new.InOut='O' and Source_Doc_No in (select top 1 Load_Out_No from TSPL_TRANSFER_HEAD where Transfer_No='" + DocNo + "')"
                    ElseIf clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal Then
                        qry += " and Source_Doc_No='" + DocNo + "'"
                    Else
                        qry += " and TSPL_INVENTORY_MOVEMENT_new.Location_Code='" + obj.Location_Code + "' "
                    End If
                ElseIf clsCommon.CompairString(TransType, "Sale Return") = CompairStringResult.Equal Then
                    Dim strShipmentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Shipment_No from TSPL_SHIPMENT_MASTER where Invoice_No in ( select Invoice_No from TSPL_SALE_RETURN_HEAD where Sale_Return_No='" + DocNo + "')", trans))
                    If clsCommon.myLen(strShipmentNo) > 0 Then
                        qry += " and Source_Doc_No in ('" + strShipmentNo + "') "
                    Else
                        qry += " and Expiry_Date >='" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "'"
                    End If
                ElseIf clsCommon.CompairString(TransType, "Purchase Return") = CompairStringResult.Equal Then
                    Dim strSRNNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Against_SRN  from TSPL_PR_HEAD where PR_No='" + DocNo + "'", trans))
                    If clsCommon.myLen(strSRNNo) > 0 Then
                        qry += " and Source_Doc_No in ('" + strSRNNo + "') "
                    End If
                Else ''For WareHouse and rest all
                    qry += " and TSPL_INVENTORY_MOVEMENT_new.Location_Code='" + obj.Location_Code + "' "
                End If

                If arrItemDone.Contains(obj.Item_Code.Trim()) Then
                    For Each objForBatch As clsInventoryMovementNew In arrReturn
                        If clsCommon.CompairString(objForBatch.Item_Code, obj.Item_Code) = CompairStringResult.Equal Then
                            Dim innerconvFact As Double = clsItemMaster.GetConvertionFactor(objForBatch.Item_Code, objForBatch.UOM, trans)
                            Dim dblinnerMRP As Double = objForBatch.MRP * convFact
                            If dblinnerMRP = dblMRP Then
                                qry += " union all "
                                qry += " select " + clsCommon.myCstr(objForBatch.Qty / innerconvFact) + " as Stock_Qty,'O' as inout,'" + objForBatch.Batch_No + "' as Batch_No,null as MFG_Date,null as Expiry_Date "
                            End If
                        End If
                    Next
                Else
                    arrItemDone.Add(obj.Item_Code.Trim())
                End If
                qry += " )xxx group by Batch_No having sum(" + strRI + " Stock_Qty * case when inout='I' then 1 else case when inout='O' then -1 else 0 end end)<>0"
                qry += " )xxxx"
                qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code='" + obj.Item_Code + "' and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + obj.UOM + "'"
                qry += " order by Expiry_Date "

                Dim qtyToApply As Double = obj.Qty
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        Dim objToInsert As clsInventoryMovementNew = DeepCopyObject(obj)
                        objToInsert.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                        objToInsert.MFG_Date = clsCommon.myCDate(dr("MFG_Date"))
                        objToInsert.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
                        If clsCommon.myCdbl(dr("Stock_Qty")) > qtyToApply Then
                            objToInsert.Qty = qtyToApply
                            qtyToApply -= qtyToApply
                        Else
                            objToInsert.Qty = clsCommon.myCdbl(dr("Stock_Qty"))
                            qtyToApply -= clsCommon.myCdbl(dr("Stock_Qty"))
                        End If
                        arrReturn.Add(objToInsert)
                        If qtyToApply = 0 Then
                            Exit For
                        End If
                    Next
                End If
                If qtyToApply > 0 Then
                    Throw New Exception("Item Qty not available for item" + obj.Item_Code)
                End If
            Else
                arrReturn.Add(obj)
            End If
        Next
        Return arrReturn
    End Function

    Public Shared Function SaveData(ByVal TransType As String, ByVal DocNo As String, ByVal DocDate As DateTime, ByVal EntryDate As String, ByVal ArrInvMov As List(Of clsInventoryMovementNew), ByVal trans As SqlTransaction) As Boolean
        If objCommonVar.StopInventoryNew Then
            Return True
        End If

        Dim LineNo As Integer = 1
        Dim arr As List(Of clsInventoryMovementNew) = GetDataWithBatch(TransType, DocNo, DocDate, ArrInvMov, trans)
        If (arr IsNot Nothing AndAlso arr.Count > 0) Then
            If clsInventorySourceCode.CheckNewEntry(TransType, trans) Then
                If Not clsCommon.CompairString(TransType, "TempDispChallan") = CompairStringResult.Equal Then
                    Throw New Exception("Please make Inventory Source code " + TransType)
                End If
            End If
            For Each obj As clsInventoryMovementNew In arr
                If clsCommon.myLen(obj.Item_Code) <= 0 Then
                    Continue For
                End If
                Dim coll As New Hashtable()
                Dim dtpostingDate As DateTime = clsCommon.GETSERVERDATE(trans)

                clsCommon.AddColumnsForChange(coll, "Trans_Type", TransType)
                clsCommon.AddColumnsForChange(coll, "InOut", obj.InOut)
                clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                obj.Qty = Math.Round(obj.Qty, 3, MidpointRounding.AwayFromZero)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "Source_Doc_No", DocNo)
                obj.Source_Doc_Date = clsCommon.GetPrintDate(DocDate, "dd/MM/yyyy")
                obj.Posting_Date = clsCommon.GetPrintDate(DocDate, "dd/MM/yyyy")
                clsCommon.AddColumnsForChange(coll, "Source_Doc_Date", clsCommon.GetPrintDate(DocDate, "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Entry_Date", EntryDate)

                clsCommon.AddColumnsForChange(coll, "Rec_Cost", obj.Rec_Cost)
                clsCommon.AddColumnsForChange(coll, "Add_Cost", obj.Add_Cost)
                clsCommon.AddColumnsForChange(coll, "Net_Cost", obj.Net_Cost)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "ItemType", obj.ItemType)
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)
                If clsCommon.CompairString(TransType, "OUT-PUT") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "Punching_Date", clsCommon.GetPrintDate(obj.Punching_Date, "dd/MMM/yyyy hh:mm tt"))
                Else
                    clsCommon.AddColumnsForChange(coll, "Punching_Date", clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy hh:mm tt"))
                End If

                clsCommon.AddColumnsForChange(coll, "fat_per", obj.FAT_Per)
                clsCommon.AddColumnsForChange(coll, "snf_per", obj.SNF_Per)
                clsCommon.AddColumnsForChange(coll, "fat_kg", obj.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "snf_kg", obj.SNF_KG)
                clsCommon.AddColumnsForChange(coll, "Std_Qty", clsInventoryMovementNew.GetStdQty(trans, obj.FAT_KG, obj.SNF_KG, DocDate))
                If obj.MFG_Date IsNot Nothing AndAlso obj.MFG_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "MFG_Date", clsCommon.GetPrintDate(obj.MFG_Date, "dd/MMM/yyyy"))
                End If

                If obj.Expiry_Date IsNot Nothing AndAlso obj.MFG_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "Expiry_Date", clsCommon.GetPrintDate(obj.Expiry_Date, "dd/MMM/yyyy"))
                End If
                obj.Stock_UOM = clsItemMaster.GetStockUnit(obj.Item_Code, trans)

                If obj.CustomCoversionCLR > 0 Then
                    obj.Custom_UOM = clsItemMaster.GetCustomConversionUOM(obj.Item_Code, trans)
                    If clsCommon.myLen(obj.Custom_UOM) > 0 Then
                        obj.Custom_Coversion_Factor = 1 + (obj.CustomCoversionCLR / 1000)
                        If clsCommon.CompairString(obj.Stock_UOM, obj.UOM) = CompairStringResult.Equal Then
                            obj.Stock_Qty = Math.Round(obj.Qty, 3, MidpointRounding.AwayFromZero)
                        Else
                            obj.Stock_Qty = Math.Round(obj.Qty * obj.Custom_Coversion_Factor, 3, MidpointRounding.AwayFromZero)
                        End If
                        clsCommon.AddColumnsForChange(coll, "Custom_UOM", obj.Custom_UOM)
                        clsCommon.AddColumnsForChange(coll, "Custom_Coversion_Factor", obj.Custom_Coversion_Factor)
                    Else
                        obj.Stock_Qty = Math.Round(obj.Qty * clsItemMaster.GetConvertionFactor(obj.Item_Code, obj.UOM, trans), 3, MidpointRounding.AwayFromZero)
                    End If
                Else
                    obj.Stock_Qty = Math.Round(obj.Qty * clsItemMaster.GetConvertionFactor(obj.Item_Code, obj.UOM, trans), 3, MidpointRounding.AwayFromZero)
                End If

                '' changes  done by richa against job work outward case because in this case main location and sub location will be same
                Dim strCheckJobWorkLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select location_code from TSPL_LOCATION_MASTER where location_code='" & obj.Location_Code & "' and is_Jobwork=1 and isnull(is_sub_location,'')='Y' and isnull(Location_Type,'')='Physical' ", trans))
                Dim Main_Location As String = clsLocation.GetMainLocationMilk(obj.Location_Code, trans)
                If clsCommon.myLen(strCheckJobWorkLocation) > 0 Then
                    Main_Location = obj.main_location
                End If
                ''-------------------
                clsCommon.AddColumnsForChange(coll, "Stock_Qty", obj.Stock_Qty)
                clsCommon.AddColumnsForChange(coll, "Stock_UOM", obj.Stock_UOM)
                clsCommon.AddColumnsForChange(coll, "main_location", Main_Location)
                clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(dtpostingDate, "dd/MMM/yyyy hh:mm tt"))

                Dim strRefDoc As String = Nothing
                If clsCommon.CompairString(TransType, "Sale Return") = CompairStringResult.Equal Then
                    strRefDoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Invoice_Code from TSPL_SD_SALE_RETURN_DETAIL where DOCUMENT_CODE='" & DocNo & "'", trans))
                End If
                If Not obj.CalculateAvgCost Then
                    clsCommon.AddColumnsForChange(coll, "FIFO_Cost", obj.FIFO_Cost)
                    clsCommon.AddColumnsForChange(coll, "LIFO_Cost", obj.LIFO_Cost)
                    clsCommon.AddColumnsForChange(coll, "Avg_Cost", obj.Avg_Cost)
                ElseIf clsCommon.CompairString(TransType, "IC-AD") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "FIFO_Cost", 0)
                    clsCommon.AddColumnsForChange(coll, "LIFO_Cost", 0)
                    clsCommon.AddColumnsForChange(coll, "Avg_Cost", obj.Basic_Cost * IIf(obj.Qty = 0, 1, obj.Qty))
                ElseIf clsCommon.CompairString(TransType, "Sale Return") = CompairStringResult.Equal AndAlso clsCommon.myLen(strRefDoc) <= 0 Then
                    Dim isApplyCostOnPostDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCostingOnPostedDate, clsFixedParameterCode.ApplyCostingOnPostedDate, trans)) = 1, True, False)
                    clsCommon.AddColumnsForChange(coll, "FIFO_Cost", 0)
                    clsCommon.AddColumnsForChange(coll, "LIFO_Cost", 0)
                    clsCommon.AddColumnsForChange(coll, "Avg_Cost", clsInventoryMovement.GetCost(EnumCostingMethod.Averege, obj.Item_Code, obj.Location_Code, obj.Stock_Qty, DocDate, dtpostingDate, isApplyCostOnPostDate, trans, "TSPL_INVENTORY_MOVEMENT_NEW"))
                ElseIf clsCommon.CompairString(TransType, "DispChallan-RET") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "FIFO_Cost", obj.FIFO_Cost)
                    clsCommon.AddColumnsForChange(coll, "LIFO_Cost", obj.LIFO_Cost)
                    clsCommon.AddColumnsForChange(coll, "Avg_Cost", obj.Avg_Cost)
                ElseIf clsCommon.CompairString(TransType, "DisCanSale") = CompairStringResult.Equal Then
                    Dim isApplyCostOnPostDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCostingOnPostedDate, clsFixedParameterCode.ApplyCostingOnPostedDate, trans)) = 1, True, False)
                    clsCommon.AddColumnsForChange(coll, "FIFO_Cost", 0)
                    clsCommon.AddColumnsForChange(coll, "LIFO_Cost", 0)
                ElseIf clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal AndAlso clsCommon.CompairString(TransType, "SI-MT") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "FIFO_Cost", 0)
                    clsCommon.AddColumnsForChange(coll, "LIFO_Cost", 0)
                    clsCommon.AddColumnsForChange(coll, "Avg_Cost", obj.Avg_Cost)
                ElseIf clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "FIFO_Cost", obj.Basic_Cost * obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "LIFO_Cost", obj.Basic_Cost * obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "Avg_Cost", obj.Basic_Cost * obj.Qty)
                Else
                    Dim isApplyCostOnPostDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCostingOnPostedDate, clsFixedParameterCode.ApplyCostingOnPostedDate, trans)) = 1, True, False)
                    clsCommon.AddColumnsForChange(coll, "FIFO_Cost", 0)
                    clsCommon.AddColumnsForChange(coll, "LIFO_Cost", 0)
                    If clsCommon.CompairString(TransType, "DispChallan") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.PostTankerDispatchWithZeroAvgCost, trans)) = 1 Then
                        clsCommon.AddColumnsForChange(coll, "Avg_Cost", 0)
                    Else
                        If clsCommon.CompairString(TransType, "DispatchBS") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(TransType, "SI-MT") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(TransType, "OUT-PUT") <> CompairStringResult.Equal Then
                            If (obj.Fat_Amt + obj.SNF_Amt) > 0 Then
                                clsCommon.AddColumnsForChange(coll, "Avg_Cost", (obj.Fat_Amt + obj.SNF_Amt))
                            Else
                                clsCommon.AddColumnsForChange(coll, "Avg_Cost", clsInventoryMovement.GetCost(EnumCostingMethod.Averege, obj.Item_Code, obj.Location_Code, obj.Stock_Qty, DocDate, dtpostingDate, isApplyCostOnPostDate, trans, "TSPL_INVENTORY_MOVEMENT_NEW"))
                            End If
                        End If
                    End If
                End If
                '' update fat,snf cost colums ''RICHA CHANGES DONE BY ME TO CALCUALTE COST ON AVG_BASES FOR OVERALL TRANSACTION INTO SYSTEM
                If Not (clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal Or obj.DonNotCalculateAvgFATSNFCost Or clsCommon.CompairString(TransType, "IC-AD") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "DispChallan") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "DispatchBSTrade") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "PP_ISSUE") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "PP_STDN") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "PRD_STG_PROC") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "PROD_ENTRY") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "BulkSRN") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "BulkSRNRet") = CompairStringResult.Equal) Then
                    'Sanjay, Bulk Milk Purchase Return Fat Amt and SNF Amt calculation
                    If clsCommon.CompairString(TransType, "M-PURRETURN") = CompairStringResult.Equal Then
                        obj.Fat_Amt = Math.Round(obj.FAT_KG * obj.Fat_Rate, 2)
                        obj.SNF_Amt = Math.Round(obj.SNF_KG * obj.SNF_Rate, 2)
                    Else
                        Dim isApplyCostOnPostDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCostingOnPostedDate, clsFixedParameterCode.ApplyCostingOnPostedDate, trans)) = 1, True, False)
                        Dim objCost As New MIlkComponentType
                        objCost = clsInventoryMovementNew.GetAvgCost("MI", obj.Item_Code, obj.Location_Code, obj.Qty, obj.UOM, obj.FAT_KG, obj.SNF_KG, obj.Source_Doc_Date, obj.Source_Doc_Date, isApplyCostOnPostDate, trans)
                        obj.Fat_Rate = objCost.FAT_Cost / IIf(obj.FAT_KG <= 0, 1, obj.FAT_KG)
                        obj.SNF_Rate = objCost.SNF_Cost / IIf(obj.SNF_KG <= 0, 1, obj.SNF_KG)
                        obj.Fat_Amt = objCost.FAT_Cost
                        obj.SNF_Amt = objCost.SNF_Cost
                    End If
                End If
                ''richa 26 Sep,2018

                If obj.CalculateAvgCost AndAlso (clsCommon.CompairString(TransType, "DisCanSale") = CompairStringResult.Equal OrElse clsCommon.CompairString(TransType, "DispatchBS") = CompairStringResult.Equal OrElse clsCommon.CompairString(TransType, "OUT-PUT") = CompairStringResult.Equal) Then
                    obj.Avg_Cost = obj.Fat_Amt + obj.SNF_Amt
                    clsCommon.AddColumnsForChange(coll, "Avg_Cost", obj.Avg_Cost)
                    clsCommon.AddColumnsForChange(coll, "Basic_Cost", obj.Avg_Cost / obj.Qty)
                Else
                    clsCommon.AddColumnsForChange(coll, "Basic_Cost", obj.Basic_Cost)
                End If

                ''------------
                If clsCommon.CompairString(TransType, "SI-MT") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.InOut, "O") = CompairStringResult.Equal Then
                    obj.Avg_Cost = obj.Fat_Amt + obj.SNF_Amt
                    clsCommon.AddColumnsForChange(coll, "Avg_Cost", obj.Avg_Cost)
                End If

                clsCommon.AddColumnsForChange(coll, "Fat_Rate", obj.Fat_Rate)
                clsCommon.AddColumnsForChange(coll, "SNF_Rate", obj.SNF_Rate)
                clsCommon.AddColumnsForChange(coll, "Fat_Amt", obj.Fat_Amt)
                clsCommon.AddColumnsForChange(coll, "SNF_Amt", obj.SNF_Amt)

                clsCommon.AddColumnsForChange(coll, "item_status", obj.itemstatus)
                clsCommon.AddColumnsForChange(coll, "Assmbly_Status", obj.itemtypeinventry)
                clsCommon.AddColumnsForChange(coll, "IS_CONSUMPTION", obj.IS_CONSUMPTION)

                clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code, True)
                clsCommon.AddColumnsForChange(coll, "Cust_Name", obj.Cust_Name)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code, True)
                clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
                clsCommon.AddColumnsForChange(coll, "Other_Location_Code", obj.Other_Location_Code, True)
                clsCommon.AddColumnsForChange(coll, "Other_Location_Desc", obj.Other_Location_Desc)
                '' update Sync Satatus
                clsCommon.AddColumnsForChange(coll, "SYNC_STATUS", 0)
                clsCommon.AddColumnsForChange(coll, "Is_Scheme_Item", obj.Is_Scheme_Item, True)
                clsCommon.AddColumnsForChange(coll, "Inventory_CrAcc", obj.Inventory_CrAcc, True)
                clsCommon.AddColumnsForChange(coll, "Inventory_DrAcc", obj.Inventory_DrAcc, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVENTORY_MOVEMENT_new", OMInsertOrUpdate.Insert, "", trans)
                If obj.Ref_Line_No > 0 Then ''for special condition,where on screen line no is not refreshed ,so pass line no in obj,otherwise incremented default line no applied.
                    LineNo = obj.Ref_Line_No
                End If
                ''richa agarwal 25 June,2019 batch item work for milk type item of batch wise
                If obj.Qty <> 0 Then ''if qty=0 from store adjustment and item is of batch type ,then also no need of below method,as it sends error message.
                    clsBatchInventoryNew.PostData(TransType, DocNo, obj.Item_Code, obj.InOut, LineNo, trans, False, (obj.IS_CONSUMPTION = 1))
                End If
                LineNo += 1
            Next
        End If
        Return True
    End Function

    ''richa 7 march,2019 GKD/28/02/19-000177 function to get fat and snf kg with committed and back date entry data 
    Public Shared Function GetbalanceQuery_FatAndSnfKG(ByVal strICode As String, ByVal strLocation As String, ByVal strSubLocation As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As DateTime, ByVal trans As SqlTransaction, ByVal strUOM As String)
        Dim strCondition As String = ""
        Dim strCondition1 As String = ""
        Dim strCondition2 As String = ""
        Dim strCondition3 As String = String.Empty
        Dim strCondition4 As String = String.Empty
        If clsCommon.myLen(strSubLocation) > 0 Then
            strCondition1 = "  and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code='" + strLocation + "' and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No='" + strSubLocation + "'"
        Else
            strCondition1 = " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code='" + strLocation + "' "
        End If

        ''richa agarwal changes done on  29 march 2016
        If clsCommon.myLen(strSubLocation) > 0 Then
            strCondition2 = "  and TSPL_ADJUSTMENT_HEADER.MainLocationCode='" + strLocation + "' and TSPL_ADJUSTMENT_HEADER.Loc_Code='" + strSubLocation + "'"
        Else
            strCondition2 = " and TSPL_ADJUSTMENT_HEADER.MainLocationCode='" + strLocation + "' "
        End If
        ''---------

        ''richa agarwal changes done on 17 Apr,2018 for TSPL_MCC_Dispatch_Challan_Stock_Detail
        If clsCommon.myLen(strSubLocation) > 0 AndAlso clsCommon.myLen(strLocation) > 0 Then
            strCondition3 = " and TSPL_MCC_Dispatch_Challan_Stock_Detail.Main_Location='" + strLocation + "' and TSPL_MCC_Dispatch_Challan_Stock_Detail.Location_Code='" + strSubLocation + "'"
        ElseIf clsCommon.myLen(strSubLocation) > 0 Then
            strCondition3 = " and (TSPL_MCC_Dispatch_Challan_Stock_Detail.Location_Code='" + strSubLocation + "' or TSPL_MCC_Dispatch_Challan_Stock_Detail.Main_Location='" + strSubLocation + "') "
        ElseIf clsCommon.myLen(strLocation) > 0 Then
            strCondition3 = " and (TSPL_MCC_Dispatch_Challan_Stock_Detail.Location_Code='" + strLocation + "' or (TSPL_MCC_Dispatch_Challan_Stock_Detail.Main_Location='" + strLocation + "' and TSPL_MCC_Dispatch_Challan_Stock_Detail.Location_Code ='')) "
        End If

        ''richa agarwal changes done on 17 Apr,2018 for TSPL_Dispatch_BulkSale
        If clsCommon.myLen(strSubLocation) > 0 Then
            strCondition4 = "  and TSPL_Dispatch_BulkSale.Location_Code='" + strLocation + "' and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No='" + strSubLocation + "'"
        Else
            strCondition4 = " and TSPL_Dispatch_BulkSale.Location_Code='" + strLocation + "' "
        End If

        ''---------

        If clsCommon.myLen(strSubLocation) > 0 AndAlso clsCommon.myLen(strLocation) > 0 Then
            strCondition = " and TSPL_INVENTORY_MOVEMENT_NEW.Main_Location='" + strLocation + "' and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strSubLocation + "'"
        ElseIf clsCommon.myLen(strSubLocation) > 0 Then
            strCondition = " and (TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strSubLocation + "' or TSPL_INVENTORY_MOVEMENT_NEW.Main_Location='" + strSubLocation + "') "
        ElseIf clsCommon.myLen(strLocation) > 0 Then
            strCondition = " and (TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strLocation + "' or (TSPL_INVENTORY_MOVEMENT_NEW.Main_Location='" + strLocation + "' and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code ='')) "
        End If

        Dim qry As String = "select (((case when Minimum_Bal.Minimum_Balance is null then  xx.Qty else (case when Minimum_Bal.Minimum_Balance>xx.Qty then xx.Qty else Minimum_Bal.Minimum_Balance end)  end))/FinalUOM.Conversion_Factor) as Qty," + Environment.NewLine &
" (((case when Minimum_Bal.Minimum_Balance_FATkg  is null then  xx.fat_kg  else (case when Minimum_Bal.Minimum_Balance_FATkg >xx.fat_kg  then xx.fat_kg else Minimum_Bal.Minimum_Balance_FATkg  end)  end))/FinalUOM.Conversion_Factor) as FAT_KG," + Environment.NewLine &
" (((case when Minimum_Bal.Minimum_Balance_SNFKG is null then  xx.Snf_kg  else (case when Minimum_Bal.Minimum_Balance_SNFKG>xx.Snf_kg then xx.Snf_kg else Minimum_Bal.Minimum_Balance_SNFKG end)  end))/FinalUOM.Conversion_Factor) as SNF_KG from (" + Environment.NewLine
        qry += " select xxx.ICode,xxx.Location,SUM(qty * TSPL_ITEM_UOM_DETAIL.Conversion_Factor*RI) as Qty,SUM(fat_kg * TSPL_ITEM_UOM_DETAIL.Conversion_Factor*RI) as fat_kg,SUM(Snf_kg * TSPL_ITEM_UOM_DETAIL.Conversion_Factor*RI) as Snf_kg from( " + Environment.NewLine
        qry += " select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM ,sum(fat_kg*RI) as fat_kg,sum(Snf_kg*RI) as Snf_kg from("
        qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew,fat_kg,snf_kg from("
        qry += " select TSPL_INVENTORY_MOVEMENT_NEW.Trans_Id, TSPL_INVENTORY_MOVEMENT_NEW.Item_Code ,TSPL_INVENTORY_MOVEMENT_NEW.Location_Code AS Location_Code, "
        qry += " TSPL_INVENTORY_MOVEMENT_NEW.InOut,TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty as Qty   ,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM as UOMNew ,isnull(TSPL_INVENTORY_MOVEMENT_NEW.fat_kg,0) as fat_kg,isnull(TSPL_INVENTORY_MOVEMENT_NEW.Snf_kg,0) as Snf_kg "
        qry += " from TSPL_INVENTORY_MOVEMENT_NEW "
        qry += " where  TSPL_INVENTORY_MOVEMENT_NEW.Item_Code='" + strICode + "' " + strCondition + " "
        Dim qryMinBal As String = "select null as Item_Code,null as Location_Code,null as Minimum_Balance"
        Dim intSettingType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, trans))
        If intSettingType = 1 Then
            qryMinBal = " select Item_Code,Location_Code,min(Closing_Balance) as Minimum_Balance,min(Closing_Balance_FATkg) as Minimum_Balance_FATkg,min(Closing_Balance_SNFKG ) as Minimum_Balance_SNFKG from (" &
                        " select Item_Code,Location_Code,cast(Punching_Date as date) as Punching_Date,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*Stock_Qty)) over(order by cast(Punching_Date as date)) as Closing_Balance, " &
                        " sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*Fat_KG )) over(order by cast(Punching_Date as date)) as Closing_Balance_FATkg,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*SNF_KG )) over(order by cast(Punching_Date as date)) as Closing_Balance_SNFKG " &
                        " from TSPL_INVENTORY_MOVEMENT where Item_Code='" & strICode & "' AND Location_Code='" & strLocation & "' " &
                        " group by cast(Punching_Date as date),Item_Code,Location_Code " &
                        " union all " &
                        " select Item_Code,Location_Code,cast(Punching_Date as date) as Punching_Date,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*Stock_Qty)) over(order by cast(Punching_Date as date)) as Closing_Balance " &
                        " ,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*Fat_KG )) over(order by cast(Punching_Date as date)) as Closing_Balance_FATkg,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*SNF_KG )) over(order by cast(Punching_Date as date)) as Closing_Balance_SNFKG " &
                        " from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" & strICode & "' AND Location_Code='" & strLocation & "' " &
                        " group by cast(Punching_Date as date),Item_Code,Location_Code) as MinimumQry where Punching_Date>'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' " &
                        " group by Item_Code,Location_Code "
            qry += " and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "'"
        ElseIf intSettingType = 0 Then
            qry += " and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "'"
        End If

        qry += " )xxx  "
        qry += " )xxxx group by Item_Code,Location_Code,UOMNew "

        qry += " union all " + Environment.NewLine

        qry += " select TSPL_Dispatch_Detail_BulkSale.Item_Code as ICode,case when ISNULL(TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No,'')<>'' then TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No else TSPL_Dispatch_BulkSale.Location_Code  end  as Locaion,TSPL_Dispatch_Detail_BulkSale.Qty,-1 as RI,TSPL_Dispatch_Detail_BulkSale.Unit_code AS Uom " &
            " ,isnull(TSPL_Dispatch_Detail_BulkSale.fat_kg,0) as fat_kg,isnull(TSPL_Dispatch_Detail_BulkSale.SNF_KG,0) as SNF_KG  " &
        " from TSPL_Dispatch_Detail_BulkSale " &
        " left outer join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_Dispatch_Detail_BulkSale.Document_No" &
        " LEFT OUTER JOIN TSPL_Quality_Check_BulkSale ON TSPL_Dispatch_BulkSale.QC_Code=TSPL_Quality_Check_BulkSale.QC_No" &
        " LEFT OUTER JOIN TSPL_LOADING_TANKER_DETAIL_BULKSALE ON TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No=TSPL_Quality_Check_BulkSale.LoadingTanker_No" &
        " where TSPL_Dispatch_BulkSale.Posted=0 and TSPL_Dispatch_Detail_BulkSale.Item_Code='" + strICode + "'  " + strCondition4 + " and TSPL_Dispatch_Detail_BulkSale.Qty<>0 " &
        " and TSPL_Dispatch_Detail_BulkSale.Document_No not in ('" + strDocumentNo + "')"

        ''can sale
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_CANSALE_DISPATCH_DETAIL.ItemCode as ICode,TSPL_CANSALE_DISPATCH_HEAD.Location_Code as Locaion,TSPL_CANSALE_DISPATCH_DETAIL.Qty,-1 as RI,TSPL_CANSALE_DISPATCH_DETAIL.UOM AS Uom " &
            " ,isnull(TSPL_CANSALE_DISPATCH_DETAIL.fat_kg,0) as fat_kg,isnull(TSPL_CANSALE_DISPATCH_DETAIL.SNF_KG,0) as SNF_KG " &
        " from TSPL_CANSALE_DISPATCH_DETAIL " &
        " left outer join TSPL_CANSALE_DISPATCH_HEAD on TSPL_CANSALE_DISPATCH_HEAD.Document_No=TSPL_CANSALE_DISPATCH_DETAIL.Document_No" &
        " where TSPL_CANSALE_DISPATCH_HEAD.Posted=0 and TSPL_CANSALE_DISPATCH_DETAIL.ItemCode='" + strICode + "' and TSPL_CANSALE_DISPATCH_HEAD.Location_Code='" + strLocation + "' and TSPL_CANSALE_DISPATCH_DETAIL.Qty<>0  " &
        " and TSPL_CANSALE_DISPATCH_DETAIL.Document_No not in ('" + strDocumentNo + "')"

        ''SILO MILK TRANSFER
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_SILO_MILK_TRANSFER_DETAIL.Item_Code  as ICode,TSPL_SILO_MILK_TRANSFER_DETAIL.Silo_Code as Locaion,TSPL_SILO_MILK_TRANSFER_DETAIL.Qty,-1 as RI,TSPL_SILO_MILK_TRANSFER_DETAIL.UOM AS Uom " &
            " ,isnull(TSPL_SILO_MILK_TRANSFER_DETAIL.fat_kg,0) as fat_kg,isnull(TSPL_SILO_MILK_TRANSFER_DETAIL.SNF_KG,0) as SNF_KG  " &
        " from TSPL_SILO_MILK_TRANSFER_DETAIL " &
        " left outer join TSPL_SILO_MILK_TRANSFER_HEAD on TSPL_SILO_MILK_TRANSFER_HEAD.Document_Code =TSPL_SILO_MILK_TRANSFER_DETAIL.Document_Code " &
        " where TSPL_SILO_MILK_TRANSFER_HEAD.Posted=0 and TSPL_SILO_MILK_TRANSFER_DETAIL.Item_Code='" + strICode + "' and TSPL_SILO_MILK_TRANSFER_DETAIL.Qty<>0  " &
        " and TSPL_SILO_MILK_TRANSFER_DETAIL.Document_Code not in ('" + strDocumentNo + "')"

        If clsCommon.myLen(strSubLocation) > 0 Then
            strCondition4 = "  and TSPL_SILO_MILK_TRANSFER_HEAD.MainLocation_Code='" + strLocation + "' and TSPL_SILO_MILK_TRANSFER_DETAIL.Silo_Code='" + strSubLocation + "'"
        Else
            strCondition4 = " and TSPL_SILO_MILK_TRANSFER_DETAIL.Silo_Code='" + strLocation + "' "
        End If

        qry += " union all " + Environment.NewLine &
            " select TSPL_Dispatch_Detail_BulkSale_Trade.Item_Code as ICode,TSPL_Dispatch_BulkSale_Trade.Location_Code as Locaion,  " + Environment.NewLine &
            " TSPL_Dispatch_Detail_BulkSale_Trade.Qty,-1 as RI,TSPL_Dispatch_Detail_BulkSale_Trade.Unit_code AS Uom,isnull(TSPL_Dispatch_Detail_BulkSale_Trade.fat_kg,0) as fat_kg,isnull(TSPL_Dispatch_Detail_BulkSale_Trade.SNF_KG,0) as SNF_KG   from TSPL_Dispatch_Detail_BulkSale_Trade  " + Environment.NewLine &
            " left outer join TSPL_Dispatch_BulkSale_Trade  on TSPL_Dispatch_BulkSale_Trade.Document_No=TSPL_Dispatch_Detail_BulkSale_Trade.Document_No  " + Environment.NewLine &
            " where TSPL_Dispatch_BulkSale_Trade.Posted=0 and TSPL_Dispatch_Detail_BulkSale_Trade.Item_Code='" + strICode + "' and TSPL_Dispatch_BulkSale_Trade.Location_Code='" + strLocation + "'" + Environment.NewLine &
            " and TSPL_Dispatch_Detail_BulkSale_Trade.Qty<>0   and TSPL_Dispatch_Detail_BulkSale_Trade.Document_No not in ('" + strDocumentNo + "')"

        qry += " union all " + Environment.NewLine

        qry += " select TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code as ICode,TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code as Locaion,TSPL_PP_ISSUE_ITEM_DETAIL.Qty,-1 as RI,TSPL_PP_ISSUE_ITEM_DETAIL.Unit_code AS Uom " &
        " ,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.fat_kg,0) as fat_kg,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG,0) as SNF_KG " + Environment.NewLine &
        " from TSPL_PP_ISSUE_ITEM_DETAIL " + Environment.NewLine &
         " left outer join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code" + Environment.NewLine &
         " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code " + Environment.NewLine &
        " where TSPL_PP_ISSUE_HEAD.Is_post=0 and TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code='" + strICode + "' and TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code='" + strLocation + "' and TSPL_PP_ISSUE_ITEM_DETAIL.Qty<>0  " + Environment.NewLine &
         " and TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code not in ('" + strDocumentNo + "')"

        qry += " union all " + Environment.NewLine
        qry += " select Main_Item_Code as ICode,LOCATION_CODE as Location,QUANTITY,(case when TRANSACTION_TYPE='Assembly' then 1  else -1 end) as RI," + Environment.NewLine &
        " BUILD_ITEM_UNIT_CODE as UnitCode ,isnull(TSPL_PROD_ASSEMBLIES.fat_kg,0) as fat_kg,isnull(TSPL_PROD_ASSEMBLIES.SNF_KG,0) as SNF_KG  from TSPL_PROD_ASSEMBLIES where TSPL_PROD_ASSEMBLIES.TRANSACTION_TYPE='Disassembly' and TSPL_PROD_ASSEMBLIES.POSTED=0 and  TSPL_PROD_ASSEMBLIES.Main_Item_Code='" + strICode + "'  and TSPL_PROD_ASSEMBLIES.CODE  not in ('" + strDocumentNo + "')"
        If clsCommon.myLen(strSubLocation) > 0 Then
            qry += " and TSPL_PROD_ASSEMBLIES.LOCATION_CODE='" & strSubLocation & "'"
        Else
            qry += " and TSPL_PROD_ASSEMBLIES.LOCATION_CODE='" & strLocation & "'"
        End If

        qry += " union all  "

        qry += " select  TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE AS ICode,TSPL_PJC_ASSEMBLIES.LOCATION_CODE as Location," + Environment.NewLine &
         " TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_QUANTITY as Qty," + Environment.NewLine &
         " (case when TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly' then  -1 else  1 end) AS RI," + Environment.NewLine &
        " TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_UNIT_CODE as UnitCode,isnull(TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.fat_kg,0) as fat_kg,isnull(TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.SNF_KG,0) as SNF_KG  from TSPL_PJC_ASSEMBLIES " + Environment.NewLine &
         " inner JOIN TSPL_PROD_ASSEMBLIES_ITEM_DETAIL ON TSPL_PJC_ASSEMBLIES.CODE=TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.ASSEMBLY_CODE " + Environment.NewLine &
         " where TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly'  and  TSPL_PJC_ASSEMBLIES.POSTED=0 and TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE='" + strICode + "'  and TSPL_PJC_ASSEMBLIES.CODE  not in ('" + strDocumentNo + "')"
        If clsCommon.myLen(strSubLocation) > 0 Then
            qry += " and TSPL_PJC_ASSEMBLIES.LOCATION_CODE='" & strSubLocation & "'"
        Else
            qry += " and TSPL_PJC_ASSEMBLIES.LOCATION_CODE='" & strLocation & "'"
        End If

        qry += " union all  "

        qry += " select  TSPL_WRECKAGE_BOOKING.Item_Code AS ICode,TSPL_WRECKAGE_ENTRY.LOCATION_CODE as Location,"
        qry += " TSPL_WRECKAGE_BOOKING.WRECKAGE_QTY as Qty, -1 AS RI,"
        qry += " TSPL_WRECKAGE_BOOKING.Unit_Code as UnitCode,isnull(TSPL_WRECKAGE_BOOKING.Avail_FAT_KG ,0) as fat_kg,isnull(TSPL_WRECKAGE_BOOKING.Avail_SNF_KG ,0) as SNF_KG  from TSPL_WRECKAGE_ENTRY "
        qry += " inner JOIN TSPL_WRECKAGE_BOOKING ON TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE=TSPL_WRECKAGE_BOOKING.WRECKAGE_CODE "
        qry += " where TSPL_WRECKAGE_ENTRY.POSTED=0 and TSPL_WRECKAGE_BOOKING.ITEM_CODE='" + strICode + "'  and TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE  not in ('" + strDocumentNo + "')"
        If clsCommon.myLen(strSubLocation) > 0 Then
            qry += " and TSPL_WRECKAGE_ENTRY.LOCATION_CODE='" & strSubLocation & "'"
        Else
            qry += " and TSPL_WRECKAGE_ENTRY.LOCATION_CODE='" & strLocation & "'"
        End If


        '' added by richa agarwal to include transactions of store adjustment whose trans type is Out and milk type is 1 
        qry += "union all " + Environment.NewLine &
            "  select TSPL_ADJUSTMENT_DETAIL.Item_Code as ICode,"
        If clsCommon.myLen(strSubLocation) > 0 Then
            qry += "  TSPL_ADJUSTMENT_HEADER.Loc_Code as Locaion, "
        Else
            qry += " TSPL_ADJUSTMENT_HEADER.MainLocationCode as Locaion, "
        End If
        qry += " TSPL_ADJUSTMENT_DETAIL.Item_Quantity ,-1 as RI,TSPL_ADJUSTMENT_DETAIL.Unit_code AS Uom ,isnull(TSPL_ADJUSTMENT_DETAIL.FAT_KG ,0) as fat_kg,isnull(TSPL_ADJUSTMENT_DETAIL.SNF_KG ,0) as SNF_KG  " &
            " from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No =TSPL_ADJUSTMENT_DETAIL.Adjustment_No  " &
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code and TSPL_ITEM_MASTER.Product_Type='MI' " &
            " where TSPL_ADJUSTMENT_HEADER.Posted ='N' and TSPL_ADJUSTMENT_DETAIL.Item_Code='" + strICode + "' " + strCondition2 + " and TSPL_ADJUSTMENT_DETAIL.Item_Quantity <>0 " &
            " and TSPL_ADJUSTMENT_HEADER.Trans_Type  ='Out' and TSPL_ADJUSTMENT_HEADER .IsMilkType =1 and TSPL_ADJUSTMENT_DETAIL.Adjustment_No  not in ('" + strDocumentNo + "') " + Environment.NewLine


        qry += " union all " + Environment.NewLine


        '' save data into detail table to check qty with main and silo location
        qry += "  select  TSPL_MCC_Dispatch_Challan_Stock_Detail.Item_Code ,  TSPL_MCC_Dispatch_Challan_Stock_Detail.Location_Code AS Location_Code , TSPL_MCC_Dispatch_Challan_Stock_Detail.Qty  as Qty,  -1 as RI, TSPL_MCC_Dispatch_Challan_Stock_Detail.UOM  as UOMNew ,isnull(TSPL_MCC_Dispatch_Challan_Stock_Detail.FAT_KG ,0) as fat_kg,isnull(TSPL_MCC_Dispatch_Challan_Stock_Detail.SNF_KG ,0) as SNF_KG  from TSPL_MCC_Dispatch_Challan_Stock_Detail  where TSPL_MCC_Dispatch_Challan_Stock_Detail.IsPosted=0 and TSPL_MCC_Dispatch_Challan_Stock_Detail.Qty<>0 " &
            " and TSPL_MCC_Dispatch_Challan_Stock_Detail.Item_Code='" + strICode + "'  " + strCondition3 + " and TSPL_MCC_Dispatch_Challan_Stock_Detail.Chalan_No not in ('" + strDocumentNo + "')"

        '' query for add/remove items durng Process production Standardization
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Item_Code,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Loaction_Code,"
        qry += " TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY,"
        qry += " (case when TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then 1 else  -1  end)as RI,"
        qry += " TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE,isnull(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_FAT_KG ,0) as fat_kg,isnull(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_SNF_KG ,0) as SNF_KG from TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL "
        qry += " inner join TSPL_PP_STANDARDIZATION_HEAD on TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code = TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code "
        qry += " where TSPL_PP_STANDARDIZATION_HEAD.Posted=0 and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Item_Code='" + strICode + "' "
        qry += " and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code not in ('" + strDocumentNo + "')"
        qry += " and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Loaction_Code='" & strLocation & "' "

        '' query for  Process production Standardization
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Item_Code,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.STD_Loaction_Code,"
        qry += " TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_Qty,"
        qry += " 1 as RI,"
        qry += " TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.UNIT_CODE,isnull(TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_FAT_KG ,0) as fat_kg,isnull(TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_SNF_KG ,0) as SNF_KG from TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL "
        qry += " inner join TSPL_PP_STANDARDIZATION_HEAD on TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Standardization_Code = TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code "
        qry += " where TSPL_PP_STANDARDIZATION_HEAD.Posted=0 and TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Item_Code='" + strICode + "' "
        qry += " and TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Standardization_Code not in ('" + strDocumentNo + "')"
        qry += " and TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.STD_Loaction_Code='" & strLocation & "' "

        '' PRODUCTION CONSUMPTION 
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE," &
              " TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.LOCATION_CODE,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_QTY,-1 as RI," &
              " TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.UNIT_CODE ,isnull(TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.fat_kg ,0) as fat_kg,isnull(TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_KG ,0) as SNF_KG from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL " &
              " inner join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE " &
              " where TSPL_PP_PRODUCTION_ENTRY.POSTED=0 and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE='" & strICode & "' " &
              " and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE not in ('" & strDocumentNo & "') " &
              " and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.LOCATION_CODE='" & strLocation & "'"

        '' query for add/remove items durng Process production STAGE PROCESS
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code,TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Loaction_Code,"
        qry += " TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY,"
        qry += " (case when TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then 1 else  -1  end)as RI,"
        qry += " TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE,isnull(TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.fat_kg ,0) as fat_kg,isnull(TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.SNF_KG ,0) as SNF_KG from TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL "
        qry += " inner join TSPL_PP_STAGE_PROCESS_HEAD on TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE = TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE "
        qry += " where TSPL_PP_STAGE_PROCESS_HEAD.Posted=0 and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code='" + strICode + "' "
        qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE not in ('" + strDocumentNo + "')"
        qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Loaction_Code='" & strLocation & "' "

        '' PRODUCTION ENTRY 
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE," &
               " TSPL_PP_PRODUCTION_ENTRY.LOCATION_CODE,TSPL_PP_PRODUCTION_ENTRY_DETAIL.RECEIPT_QTY,1 as RI," &
               " TSPL_PP_PRODUCTION_ENTRY_DETAIL.UNIT_CODE,isnull(TSPL_PP_PRODUCTION_ENTRY_DETAIL.fat_kg ,0) as fat_kg,isnull(TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG ,0) as SNF_KG from TSPL_PP_PRODUCTION_ENTRY_DETAIL " &
               " inner join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE " &
               " where TSPL_PP_PRODUCTION_ENTRY.POSTED=0 and TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE='" & strICode & "' " &
               " and TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE not in ('" & strDocumentNo & "')" &
               " and TSPL_PP_PRODUCTION_ENTRY.LOCATION_CODE='" & strLocation & "'"

        If clsCommon.myLen(strSubLocation) > 0 Then
            qry += " )xxx left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xxx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xxx.UOM where xxx.Location ='" & strSubLocation & "' group by ICode,Location"
        Else
            qry += " )xxx left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xxx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xxx.UOM group by ICode,Location"
        End If
        qry += " )xx" + Environment.NewLine +
        " left join (" & qryMinBal & ") as Minimum_Bal on xx.ICode=Minimum_Bal.Item_Code and xx.Location=Minimum_Bal.Location_Code " + Environment.NewLine +
        " left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode and FinalUOM.UOM_Code='" + strUOM + "'"
        Return qry
    End Function
    Public Shared Function getBalance_FatAndSnfKG(ByVal strICode As String, ByVal strLocation As String, ByVal strSubLocation As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As DateTime, ByVal trans As SqlTransaction, ByVal strUOM As String) As DataTable
        Dim qry = GetbalanceQuery_FatAndSnfKG(strICode, strLocation, strSubLocation, strDocumentNo, dtDocumentDate, trans, strUOM)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        'Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, trans)
        '' MIL/07/06/18-000025 richa 
        '' Return BalQty / IIf(dblConvFac <= 0, 1, dblConvFac)
        Return dt
    End Function

    ''------ end of fatAndSnfKG function

    Public Shared Function getCostingMethod(itemCode As String, Optional trans As SqlTransaction = Nothing) As Integer
        Try
            Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select costing_method from  tspl_purchase_accounts where purchase_class_code=(select Purchase_Class_Code  from tspl_item_master where Item_Code='" & itemCode & "') ", trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetCost(ByVal CostMethod As EnumCostingMethod, ByVal strICode As String, ByVal strLocation As String, ByVal dblqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction) As Double
        Dim dblRetCost As Double = 0
        If Not CostMethod = EnumCostingMethod.NA AndAlso dblqty > 0 Then
            Dim strSymbolCost As String = " >= "
            If CostMethod = EnumCostingMethod.LIFO Then
                strSymbolCost = " <= "
            End If

            Dim strDateColumn As String = " Punching_Date "
            Dim strDateForCheck As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt")
            If isApplyCostOnPostDate Then
                strDateColumn = " Posting_Date "
                strDateForCheck = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtPostingDate), "dd/MMM/yyyy hh:mm tt")
            End If

            Dim qry As String
            If CostMethod = EnumCostingMethod.Averege Then
                'qry = "select case when Qty=0 then 0 else abs(Amt/Qty)*" + clsCommon.myCstr(dblqty) + "  end as AvgCost from( select  sum(Amt * RI) as Amt,sum(Qty * RI) as Qty from(" + Environment.NewLine
                'qry += " select Stock_Qty as Qty,( Avg_Cost) as Amt,case when InOut='O' then -1 else 1 end as RI  from TSPL_INVENTORY_MOVEMENT_new where Item_Code='" + strICode + "' and Location_Code='" + strLocation + "' and  " + strDateColumn + " <= '" + strDateForCheck + "' " + Environment.NewLine
                'qry += " )xxx )xxxx" + Environment.NewLine
                qry = "select case when (Qty<0 or Amt<0)  then (AvgPostitiveRate*" + clsCommon.myCstr(dblqty) + ") else case when (Qty>0 and Qty>=" + clsCommon.myCstr(dblqty) + ")  then (Amt/Qty)*" + clsCommon.myCstr(dblqty) + " else case when Qty>0 and Qty<" + clsCommon.myCstr(dblqty) + " then (Amt+((" + clsCommon.myCstr(dblqty) + "-Qty)*AvgPostitiveRate)) else 0 end end end as AvgCost from( "
                qry += " select  sum(Amt * RI) as Amt,sum(Qty * RI) as Qty,(select top 1 Avg_Cost/(case when Stock_Qty=0 then 1 else Stock_Qty end)  from TSPL_INVENTORY_MOVEMENT_new where Item_Code='" + strICode + "' and Location_Code='" + strLocation + "' and  " + strDateColumn + " <= '" + strDateForCheck + "' and InOut='I' and Avg_Cost>0) as AvgPostitiveRate from(" + Environment.NewLine
                qry += " select Stock_Qty as Qty,( Avg_Cost) as Amt,case when InOut='O' then -1 else 1 end as RI  from TSPL_INVENTORY_MOVEMENT_new where Item_Code='" + strICode + "' and Location_Code='" + strLocation + "' and  " + strDateColumn + " <= '" + strDateForCheck + "' " + Environment.NewLine
                qry += " )xxx )xxxx" + Environment.NewLine
                dblRetCost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            Else
                qry = ";WITH cteStockSum AS ( " + Environment.NewLine
                qry += " SELECT   Item_Code ,SUM(Stock_Qty * CASE WHEN  InOut = 'O' THEN -1 ELSE 1 END) AS TotalStock FROM  TSPL_INVENTORY_MOVEMENT_new where Item_Code='" + strICode + "' and Location_Code='" + strLocation + "' and " + strDateColumn + " <= '" + strDateForCheck + "'  GROUP BY Item_Code)," + Environment.NewLine

                qry += " cteReverseInSum AS (" + Environment.NewLine
                qry += " SELECT  s.Item_Code ,s." + strDateColumn + " as TranDate ,(SELECT SUM(i.Stock_Qty) FROM TSPL_INVENTORY_MOVEMENT_new AS i  WHERE i.Item_Code = s.Item_Code AND i.InOut IN ( 'I' ) and i." + strDateColumn + " <= '" + strDateForCheck + "' and i.Location_Code='" + strLocation + "' AND i." + strDateColumn + " " + strSymbolCost + " s." + strDateColumn + " --for FIFO  >= " + Environment.NewLine
                qry += " ) AS RollingStock ,s.Stock_Qty AS ThisStock FROM TSPL_INVENTORY_MOVEMENT_new AS s WHERE  s.Item_Code='" + strICode + "' and s.Location_Code='" + strLocation + "' and s." + strDateColumn + " <= '" + strDateForCheck + "'  and s.InOut IN ( 'I' ))," + Environment.NewLine

                qry += " cteWithLastTranDate  AS ( " + Environment.NewLine
                qry += " SELECT   w.Item_Code ,w.TotalStock ,LastPartialStock. TranDate ,LastPartialStock.StockToUse ,LastPartialStock.RunningTotal ,w.TotalStock -LastPartialStock.RunningTotal+ LastPartialStock.StockToUse AS UseThisStock FROM cteStockSum AS w" + Environment.NewLine
                qry += " CROSS APPLY ( SELECT TOP ( 1 )z. TranDate ,z.ThisStock AS StockToUse ,z.RollingStock AS RunningTotal FROM  cteReverseInSum AS z WHERE z.Item_Code = w.Item_Code AND z.RollingStock >= w.TotalStock ORDER BY  z.TranDate " + IIf(CostMethod = EnumCostingMethod.FIFO, "DESC", "") + " --for FIFO DESC" + Environment.NewLine
                qry += " ) AS LastPartialStock" + Environment.NewLine
                qry += " )" + Environment.NewLine

                qry += " select *  from (" + Environment.NewLine
                qry += " SELECT  y.Item_Code ,y.TotalStock AS CurrentItems ,e.Basic_Cost,e." + strDateColumn + " as TranDate,(CASE WHEN e." + strDateColumn + " = y.TranDate THEN y.UseThisStock" + Environment.NewLine
                qry += " ELSE e.Stock_Qty END * Price.Basic_Cost) AS CurrentValue,(CASE WHEN e. " + strDateColumn + "  = y.TranDate THEN y.UseThisStock  ELSE e.Stock_Qty END  ) as BalanceQty FROM cteWithLastTranDate AS y INNER JOIN TSPL_INVENTORY_MOVEMENT_new AS e ON e.Item_Code = y.Item_Code and e." + strDateColumn + " <= '" + strDateForCheck + "' AND e." + strDateColumn + " " + strSymbolCost + " y.TranDate -- for Fifo >=" + Environment.NewLine
                qry += " AND e.InOut IN ('I') and e.Location_Code='" + strLocation + "' " + Environment.NewLine
                qry += " CROSS APPLY ( SELECT TOP ( 1 ) case when Stock_Qty =0 then 0 else  (p.Basic_Cost*p.Qty)/p.Stock_Qty end as Basic_Cost FROM TSPL_INVENTORY_MOVEMENT_new AS p  WHERE p.Item_Code = e.Item_Code " + Environment.NewLine
                qry += " AND p." + strDateColumn + " <= e." + strDateColumn + "  " + Environment.NewLine
                qry += " AND p.InOut = 'I' and p.Location_Code='" + strLocation + "'  ORDER BY p." + strDateColumn + " DESC ) AS Price" + Environment.NewLine
                qry += ")xxx   " + IIf(CostMethod = EnumCostingMethod.FIFO, " order by TranDate ", IIf(CostMethod = EnumCostingMethod.LIFO, "order by TranDate DESC", "")) + " --For Fifo not Desc order" + Environment.NewLine

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim dblbalanceQty As Double = dblqty
                    For Each dr As DataRow In dt.Rows
                        Dim dblCurrQty As Double = clsCommon.myCdbl(dr("BalanceQty"))
                        If dblbalanceQty >= dblCurrQty Then
                            dblRetCost += clsCommon.myCdbl(dr("CurrentValue"))
                        Else
                            dblRetCost += (clsCommon.myCdbl(dr("CurrentValue")) * dblbalanceQty) / dblCurrQty
                        End If
                        dblbalanceQty -= dblCurrQty
                        If dblbalanceQty <= 0 Then
                            dblRetCost = dblRetCost
                            Exit For
                        End If
                    Next
                    If dblbalanceQty > 0 Then
                        'Throw New Exception("Quantity Not available for " + strICode)
                    End If
                End If
            End If
        End If
        Return dblRetCost
    End Function

    Public Shared Function GetAvgCost(ByVal Product_Type As String, ByVal Item_Code As String, ByVal strLocation As String, ByVal dblQty As Decimal, ByVal Unit_Code As String, ByVal dblFATqty As Double, ByVal dblSNFqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction, Optional ByVal Current_Doc_Code As String = "", Optional ByVal Trans_Id As Integer = 0) As MIlkComponentType
        Return GetAvgCost("", Product_Type, Item_Code, strLocation, dblQty, Unit_Code, dblFATqty, dblSNFqty, dtDocumentDate, dtPostingDate, isApplyCostOnPostDate, trans, Current_Doc_Code, Trans_Id)
    End Function

    Public Shared Function GetAvgCost(ByVal ExtrWhrl As String, ByVal Product_Type As String, ByVal Item_Code As String, ByVal strLocation As String, ByVal dblQty As Decimal, ByVal Unit_Code As String, ByVal dblFATqty As Double, ByVal dblSNFqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction, Optional ByVal Current_Doc_Code As String = "", Optional ByVal Trans_Id As Integer = 0) As MIlkComponentType
        Return GetAvgCost(False, ExtrWhrl, Product_Type, Item_Code, strLocation, dblQty, Unit_Code, dblFATqty, dblSNFqty, dtDocumentDate, dtPostingDate, isApplyCostOnPostDate, trans, Current_Doc_Code, Trans_Id)
    End Function

    Public Shared Function GetAvgCost(ByVal IsDateWithTime As Boolean, ByVal ExtrWhrl As String, ByVal Product_Type As String, ByVal Item_Code As String, ByVal strLocation As String, ByVal dblQty As Decimal, ByVal Unit_Code As String, ByVal dblFATqty As Double, ByVal dblSNFqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction, Optional ByVal Current_Doc_Code As String = "", Optional ByVal Trans_Id As Integer = 0) As MIlkComponentType
        Return GetAvgCost(False, IsDateWithTime, ExtrWhrl, Product_Type, Item_Code, strLocation, dblQty, Unit_Code, dblFATqty, dblSNFqty, dtDocumentDate, dtPostingDate, isApplyCostOnPostDate, trans, Current_Doc_Code, Trans_Id)
    End Function
    Public Shared Function GetAvgCost(ByVal IsRejectOnly As Boolean, ByVal IsDateWithTime As Boolean, ByVal ExtrWhrl As String, ByVal Product_Type As String, ByVal Item_Code As String, ByVal strLocation As String, ByVal dblQty As Decimal, ByVal Unit_Code As String, ByVal dblFATqty As Double, ByVal dblSNFqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction, Optional ByVal Current_Doc_Code As String = "", Optional ByVal Trans_Id As Integer = 0) As MIlkComponentType
        Return GetAvgCost(True, IsRejectOnly, IsDateWithTime, ExtrWhrl, Product_Type, Item_Code, strLocation, dblQty, Unit_Code, dblFATqty, dblSNFqty, dtDocumentDate, dtPostingDate, isApplyCostOnPostDate, trans, Current_Doc_Code, Trans_Id)
    End Function
    Public Shared Function GetAvgCost(ByVal IsCheckInMainLocation As Boolean, ByVal IsRejectOnly As Boolean, ByVal IsDateWithTime As Boolean, ByVal ExtrWhrl As String, ByVal Product_Type As String, ByVal Item_Code As String, ByVal strLocation As String, ByVal dblQty As Decimal, ByVal Unit_Code As String, ByVal dblFATqty As Double, ByVal dblSNFqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction, Optional ByVal Current_Doc_Code As String = "", Optional ByVal Trans_Id As Integer = 0) As MIlkComponentType
        Return GetAvgCost(False, IsCheckInMainLocation, IsRejectOnly, IsDateWithTime, ExtrWhrl, Product_Type, Item_Code, strLocation, dblQty, Unit_Code, dblFATqty, dblSNFqty, dtDocumentDate, dtPostingDate, isApplyCostOnPostDate, trans, Current_Doc_Code, Trans_Id)
    End Function

    Public Shared Function GetAvgCost(ByVal For10decimalPlaces As Boolean, ByVal IsCheckInMainLocation As Boolean, ByVal IsRejectOnly As Boolean, ByVal IsDateWithTime As Boolean, ByVal ExtrWhrl As String, ByVal Product_Type As String, ByVal Item_Code As String, ByVal strLocation As String, ByVal dblQty As Decimal, ByVal Unit_Code As String, ByVal dblFATqty As Double, ByVal dblSNFqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction, Optional ByVal Current_Doc_Code As String = "", Optional ByVal Trans_Id As Integer = 0) As MIlkComponentType
        Dim obj As New MIlkComponentType
        Try
            Dim settPickProductCostFromItemUOMDetail As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0)
            Dim dblRetCost As Double = 0
            Dim strSymbolCost As String = " >= "

            Dim strDateColumn As String = " Punching_Date "
            Dim strDateForCheck As String = clsCommon.GetPrintDate(IIf(IsDateWithTime, dtDocumentDate, clsCommon.GetDateWithEndTime(dtDocumentDate)), "dd/MMM/yyyy hh:mm tt")
            If isApplyCostOnPostDate Then
                strDateColumn = " Posting_Date "
                strDateForCheck = clsCommon.GetPrintDate(IIf(IsDateWithTime, dtPostingDate, clsCommon.GetDateWithEndTime(dtPostingDate)), "dd/MMM/yyyy hh:mm tt")
            End If
            Dim qry As String = ""
            Dim cond_TransId As String = ""
            If Trans_Id > 0 Then
                cond_TransId = " and  TSPL_INVENTORY_MOVEMENT_new.Trans_id<'" & Trans_Id & "'"
            End If
            If clsCommon.myLen(Product_Type) <= 0 Then
                Product_Type = clsItemMaster.GetItemProductType(Item_Code, trans)
            End If

            If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                qry = " select (case when (Fat_Amt*SNF_Amt<0 or Fat_KG*SNF_KG<0 or Fat_Amt<=0 or Fat_KG<=0) then Last_Fat_Rate else Fat_Amt/Fat_KG end)*" + clsCommon.myCstr(dblFATqty) + " as Avg_Fat_Cost" + Environment.NewLine +
                    ",(case when (Fat_Amt*SNF_Amt<0 or Fat_KG*SNF_KG<0 or SNF_Amt<=0 or SNF_KG<=0) then Last_SNF_Rate else SNF_Amt/SNF_KG end)*" + clsCommon.myCstr(dblSNFqty) + " as Avg_SNF_Cost " +
                    ",(case when (Fat_Amt*SNF_Amt<0 or Fat_KG*SNF_KG<0 or SNF_Amt<=0 or SNF_KG<=0 or Stock_Qty<=0) then Last_FAT_Per else Fat_KG*100/Stock_Qty end) as FAT_Per " +
                    ",(case when (Fat_Amt*SNF_Amt<0 or Fat_KG*SNF_KG<0 or SNF_Amt<=0 or SNF_KG<=0 or Stock_Qty<=0) then Last_SNF_Per else SNF_KG*100/Stock_Qty end) as SNF_Per ,Stock_Qty,Stock_UOM " +
                      " from ( select  sum(Stock_Qty*RI) as Stock_Qty,max(Stock_UOM) as Stock_UOM, sum(Fat_Amt * RI) as Fat_Amt,cast(sum(Fat_KG * RI)as numeric(18,3)) as Fat_KG,sum(SNF_Amt * RI) as SNF_Amt,cast(sum(SNF_KG * RI)as numeric(18,3)) as SNF_KG," + Environment.NewLine
                If settPickProductCostFromItemUOMDetail Then
                    Dim Tqty As String = "select description,Specification	from tspl_Fixed_Parameter where Type='" + clsFixedParameterType.FATSNFRate + "' and Code='" + clsFixedParameterCode.FATSNFRate + "' and description<>'0'"
                    Dim Tdt As DataTable = clsDBFuncationality.GetDataTable(Tqty, trans)
                    If Tdt Is Nothing OrElse Tdt.Rows.Count > 0 Then
                        qry += "'" + clsCommon.myCstr(clsCommon.myCdbl(Tdt.Rows(0)("description"))) + "' as Last_Fat_Rate " + Environment.NewLine +
                            ",'" + clsCommon.myCstr(clsCommon.myCdbl(Tdt.Rows(0)("Specification"))) + "' as Last_SNF_Rate  " + Environment.NewLine
                    Else
                        qry += "(Select TSPL_ITEM_QC_PARAMETER_MASTER.StandardRate from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code='" + Item_Code + "' and TSPL_PARAMETER_MASTER.Type='FAT') as Last_Fat_Rate " + Environment.NewLine +
                    ",(Select TSPL_ITEM_QC_PARAMETER_MASTER.StandardRate from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code='" + Item_Code + "' and TSPL_PARAMETER_MASTER.Type='SNF') as Last_SNF_Rate " + Environment.NewLine
                    End If
                    qry += ",(Select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code='" + Item_Code + "' and TSPL_PARAMETER_MASTER.Type='FAT') as Last_Fat_Per " + Environment.NewLine +
                    ",(Select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code='" + Item_Code + "' and TSPL_PARAMETER_MASTER.Type='SNF') as Last_SNF_Per "
                Else
                    Dim strMainLocCheck As String = ""
                    ''Comment by balwinder on 24/03/2021 Pick rate Only for the location not from the main location
                    If IsCheckInMainLocation Then
                        strMainLocCheck = " or main_location='" + strLocation + "' "
                    End If
                    qry += "isnull((select top 1 Fat_Rate from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" + Item_Code + "' and (Location_Code='" + strLocation + "' " + strMainLocCheck + " ) and  " + strDateColumn + " < '" + strDateForCheck + "' and Source_Doc_No<>'" + Current_Doc_Code + "' and InOut='I' and Fat_Rate>0 order by trans_id desc ),0) as Last_Fat_Rate" + Environment.NewLine +
                        ",isnull((select top 1 SNF_Rate from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" + Item_Code + "' and (Location_Code='" + strLocation + "' " + strMainLocCheck + ") and  " + strDateColumn + " < '" + strDateForCheck + "' and Source_Doc_No<>'" + Current_Doc_Code + "' and InOut='I' and SNF_Rate>0 order by trans_id desc ),0) as Last_SNF_Rate " + Environment.NewLine +
                        ",isnull((select top 1 Fat_Per from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" + Item_Code + "' and (Location_Code='" + strLocation + "' " + strMainLocCheck + ") and  " + strDateColumn + " < '" + strDateForCheck + "' and Source_Doc_No<>'" + Current_Doc_Code + "' and InOut='I' and Fat_Rate>0 order by trans_id desc ),0) as Last_Fat_Per" + Environment.NewLine +
                        ",isnull((select top 1 SNF_Per from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" + Item_Code + "' and (Location_Code='" + strLocation + "' " + strMainLocCheck + ") and  " + strDateColumn + " < '" + strDateForCheck + "' and Source_Doc_No<>'" + Current_Doc_Code + "' and InOut='I' and SNF_Rate>0 order by trans_id desc ),0) as Last_SNF_Per "
                End If
                qry += " from( select " + Environment.NewLine
                ''"  cast(Fat_KG as numeric(18,2)) as Fat_KG ,cast(SNF_KG as numeric(18,2)) as SNF_KG ,"
                qry += " Fat_KG ,SNF_KG, cast(Fat_Amt as numeric(18,2)) as Fat_Amt, cast(SNF_Amt as numeric(18,2)) as SNF_Amt,case when InOut='O' then -1 else 1 end as RI,Stock_Qty ,Stock_UOM  from TSPL_INVENTORY_MOVEMENT_NEW " + Environment.NewLine +
                       " left outer join TSPL_Location_MASTER on TSPL_Location_MASTER.Location_Code=TSPL_INVENTORY_MOVEMENT_NEW.Location_Code where 2=2 "
                If IsRejectOnly Then
                    qry += " and TSPL_Location_MASTER.Rejected_Type='Y'"
                Else
                    qry += " and TSPL_Location_MASTER.Rejected_Type='N'"
                End If
                qry += "  and Item_Code='" & Item_Code & "' and (TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strLocation + "'"
                ''Comment by balwinder on 24/03/2021 Pick rate Only for the location not from the main location
                If IsCheckInMainLocation Then
                    qry += "  or TSPL_INVENTORY_MOVEMENT_NEW.main_location='" + strLocation + "'"
                End If
                qry += " ) and  " + strDateColumn + " <= '" + strDateForCheck + "' and Source_Doc_No not in ('" & Current_Doc_Code & "') " & cond_TransId & "  " + ExtrWhrl + Environment.NewLine +
             " ) xxx ) xxxx"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt.Rows.Count > 0 Then
                    obj.FAT_Cost = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Avg_Fat_Cost")), IIf(For10decimalPlaces, 10, 2))
                    obj.SNF_Cost = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Avg_SNF_Cost")), IIf(For10decimalPlaces, 10, 2))

                    obj.FAT_Per = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("FAT_Per")), IIf(For10decimalPlaces, 10, 3))
                    obj.SNF_Per = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("SNF_Per")), IIf(For10decimalPlaces, 10, 3))

                    obj.Stock_Qty = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Stock_Qty")), 2)
                    obj.Stock_UOM = clsCommon.myCstr(dt.Rows(0).Item("Stock_UOM"))
                End If
            ElseIf clsCommon.CompairString(Product_Type, "MP") = CompairStringResult.Equal Then
                Dim Stock_Qty As Decimal = 0
                Stock_Qty = dblQty * clsItemMaster.GetConvertionFactor(Item_Code, Unit_Code, trans)
                Dim AvgCost As Decimal = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, Item_Code, strLocation, Stock_Qty, dtDocumentDate, dtPostingDate, isApplyCostOnPostDate, trans, "TSPL_INVENTORY_MOVEMENT", "", ExtrWhrl)
                Dim avgRate As Decimal = If((dblFATqty + dblSNFqty) <= 0, 0, AvgCost / IIf((dblFATqty + dblSNFqty) <= 0, 1, (dblFATqty + dblSNFqty)))
                obj.FAT_Cost = avgRate * dblFATqty
                obj.SNF_Cost = avgRate * dblSNFqty
            Else
                Dim Stock_Qty As Decimal = 0
                Stock_Qty = dblQty * clsItemMaster.GetConvertionFactor(Item_Code, Unit_Code, trans)
                Dim AvgCost As Decimal = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, Item_Code, strLocation, Stock_Qty, dtDocumentDate, dtPostingDate, isApplyCostOnPostDate, trans, "TSPL_INVENTORY_MOVEMENT", "", ExtrWhrl)
                obj.FAT_Cost = AvgCost * 2 / 3
                obj.SNF_Cost = AvgCost / 3
            End If

            If obj.FAT_Cost + obj.SNF_Cost <= 0 Then
                If settPickProductCostFromItemUOMDetail Then
                    Dim Tqty As String = "select description,Specification	from tspl_Fixed_Parameter where Type='" + clsFixedParameterType.FATSNFRate + "' and Code='" + clsFixedParameterCode.FATSNFRate + "' and description<>'0'"
                    Dim Tdt As DataTable = clsDBFuncationality.GetDataTable(Tqty, trans)
                    If Tdt Is Nothing OrElse Tdt.Rows.Count > 0 Then
                        obj.FAT_Cost = clsCommon.myCdbl(Tdt.Rows(0)("description")) * dblFATqty
                        obj.SNF_Cost = clsCommon.myCdbl(Tdt.Rows(0)("Specification")) * dblSNFqty
                    Else
                        ''BHA/10/09/18-000527,BHA/07/09/18-000521,KDI/16/03/18-000138,BHA/17/08/18-000450 by balwinder on 11/09/2018
                        qry = "select TSPL_PARAMETER_MASTER.Type, TSPL_ITEM_QC_PARAMETER_MASTER.StandardRate from TSPL_ITEM_QC_PARAMETER_MASTER " + Environment.NewLine +
                        "left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code" + Environment.NewLine +
                        "where Item_Code='" + Item_Code + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        dblFATqty = Math.Abs(dblFATqty)
                        dblSNFqty = Math.Abs(dblSNFqty)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            For Each dr As DataRow In dt.Rows
                                If clsCommon.CompairString(clsCommon.myCstr(dr("Type")), "FAT") = CompairStringResult.Equal Then
                                    obj.FAT_Cost = clsCommon.myCdbl(dr("StandardRate")) * dblFATqty
                                    If clsCommon.myCdbl(obj.FAT_Cost) <= 0 And dblFATqty <> 0 Then
                                        Throw New Exception("Please Provide FAT Standard Rate of Item " + Item_Code + "")
                                    End If
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(dr("Type")), "SNF") = CompairStringResult.Equal Then
                                    obj.SNF_Cost = clsCommon.myCdbl(dr("StandardRate")) * dblSNFqty
                                    If clsCommon.myCdbl(obj.SNF_Cost) <= 0 And dblSNFqty <> 0 Then
                                        Throw New Exception("Please Provide SNF Standard Rate of Item " + Item_Code + "")
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return obj
    End Function

    ''richa Ticket No.BM00000003617 on 25/08/2014
    Public Shared Function getBalance(ByVal strICode As String, ByVal strMainLocation As String, ByVal strLocation As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As DateTime, ByVal trans As SqlTransaction, ByVal strUOM As String) As Double
        Dim qry As String = "select SUM(qty*RI) as Qty from(" + Environment.NewLine
        qry += " select xx.ICode,xx.Location, xx.Qty as OldQty,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,((case when Minimum_Bal.Minimum_Balance is null then  xx.Qty else (case when Minimum_Bal.Minimum_Balance>xx.Qty then xx.Qty else Minimum_Bal.Minimum_Balance end)  end)* TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Qty" + Environment.NewLine
        qry += " from (" + Environment.NewLine

        qry += " select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM  from("
        qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew from("
        qry += " select TSPL_INVENTORY_MOVEMENT_NEW.Trans_Id, TSPL_INVENTORY_MOVEMENT_NEW.Item_Code ,ISNULL(TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,'') as Location_Code , TSPL_INVENTORY_MOVEMENT_NEW.InOut,TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty as Qty,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM  as UOMNew "
        qry += " from TSPL_INVENTORY_MOVEMENT_NEW "
        qry += " where TSPL_INVENTORY_MOVEMENT_NEW.Qty<>0 and TSPL_INVENTORY_MOVEMENT_NEW.Item_Code='" + strICode + "' and coalesce(Main_Location,'')='" + strMainLocation + "' and  Location_Code='" + strLocation + "' "
        'If dblMRP > 0 Then
        '    qry += " and TSPL_INVENTORY_MOVEMENT_NEW.MRP='" + clsCommon.myCstr(dblMRP) + "'"
        'End If
        Dim qryMinBal As String = "select null as Item_Code,null as Location_Code,null as Minimum_Balance"
        Dim intSettingType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, trans))
        If intSettingType = 1 Then
            'qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='I' and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
            qryMinBal = " select Item_Code,Location_Code,min(Closing_Balance) as Minimum_Balance from (" &
                        " select Item_Code,Location_Code,cast(Punching_Date as date) as Punching_Date,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*Stock_Qty)) over(order by cast(Punching_Date as date)) as Closing_Balance " &
                        " from TSPL_INVENTORY_MOVEMENT where Item_Code='" & strICode & "' AND Location_Code='" & strLocation & "' " &
                        " group by cast(Punching_Date as date),Item_Code,Location_Code " &
                        " union all " &
                        " select Item_Code,Location_Code,cast(Punching_Date as date) as Punching_Date,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*Stock_Qty)) over(order by cast(Punching_Date as date)) as Closing_Balance " &
                        " from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" & strICode & "' and coalesce(Main_Location,'')='" + strMainLocation + "' AND Location_Code='" & strLocation & "' " &
                        " group by cast(Punching_Date as date),Item_Code,Location_Code) as MinimumQry where Punching_Date>'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' " &
                        " group by Item_Code,Location_Code "
        ElseIf intSettingType = 0 Then
            qry += " and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "'"

        End If
        qry += " )xxx  "
        qry += " )xxxx group by Item_Code,Location_Code,UOMNew "


        qry += " union all " + Environment.NewLine

        qry += " select TSPL_Dispatch_Detail_BulkSale.Item_Code as ICode,TSPL_Dispatch_BulkSale.Location_Code as Locaion,TSPL_Dispatch_Detail_BulkSale.Qty,-1 as RI,TSPL_Dispatch_Detail_BulkSale.Unit_code AS Uom "
        qry += " from TSPL_Dispatch_Detail_BulkSale "
        qry += " left outer join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_Dispatch_Detail_BulkSale.Document_No"
        qry += " where TSPL_Dispatch_BulkSale.Posted=0 and TSPL_Dispatch_Detail_BulkSale.Item_Code='" + strICode + "' and TSPL_Dispatch_BulkSale.Location_Code='" + strMainLocation + "' and TSPL_Dispatch_Detail_BulkSale.Qty<>0  "
        qry += " and TSPL_Dispatch_Detail_BulkSale.Document_No not in ('" + strDocumentNo + "')"
        'If dblMRP > 0 Then
        '    qry += " and TSPL_Dispatch_Detail_BulkSale.MRP='" + clsCommon.myCstr(dblMRP) + "' "
        'End If

        ''richa agawral

        qry += " union all " + Environment.NewLine &
            " select TSPL_Dispatch_Detail_BulkSale_Trade.Item_Code as ICode,TSPL_Dispatch_BulkSale_Trade.Location_Code as Locaion,  " + Environment.NewLine &
            " TSPL_Dispatch_Detail_BulkSale_Trade.Qty,-1 as RI,TSPL_Dispatch_Detail_BulkSale_Trade.Unit_code AS Uom  from TSPL_Dispatch_Detail_BulkSale_Trade  " + Environment.NewLine &
            " left outer join TSPL_Dispatch_BulkSale_Trade  on TSPL_Dispatch_BulkSale_Trade.Document_No=TSPL_Dispatch_Detail_BulkSale_Trade.Document_No  " + Environment.NewLine &
            " where TSPL_Dispatch_BulkSale_Trade.Posted=0 and TSPL_Dispatch_Detail_BulkSale_Trade.Item_Code='" + strICode + "' and TSPL_Dispatch_BulkSale_Trade.Location_Code='" + strMainLocation + "'" + Environment.NewLine &
            " and TSPL_Dispatch_Detail_BulkSale_Trade.Qty<>0   and TSPL_Dispatch_Detail_BulkSale_Trade.Document_No not in ('" + strDocumentNo + "')"

        qry += " union all " + Environment.NewLine

        qry += " select TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code as ICode,TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code as Locaion,TSPL_PP_ISSUE_ITEM_DETAIL.Qty,-1 as RI,TSPL_PP_ISSUE_ITEM_DETAIL.Unit_code AS Uom "
        qry += " from TSPL_PP_ISSUE_ITEM_DETAIL "
        qry += " left outer join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code"
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code and TSPL_ITEM_MASTER.Product_Type='MI' "
        qry += " where TSPL_PP_ISSUE_HEAD.Is_post=0 and TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code='" + strICode + "' and TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code='" + strLocation + "' and TSPL_PP_ISSUE_ITEM_DETAIL.Qty<>0  "
        qry += " and TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code not in ('" + strDocumentNo + "')"
        'If dblMRP > 0 Then
        '    qry += " and TSPL_Dispatch_Detail_BulkSale.MRP='" + clsCommon.myCstr(dblMRP) + "' "
        'End If

        qry += " union all " + Environment.NewLine
        qry += " select Main_Item_Code as ICode,LOCATION_CODE as Location,QUANTITY,(case when TRANSACTION_TYPE='Assembly' then 1  else -1 end) as RI,"
        qry += " BUILD_ITEM_UNIT_CODE as UnitCode from TSPL_PROD_ASSEMBLIES where TSPL_PROD_ASSEMBLIES.TRANSACTION_TYPE='Disassembly' and TSPL_PROD_ASSEMBLIES.POSTED=0 and  TSPL_PROD_ASSEMBLIES.Main_Item_Code='" + strICode + "'  and TSPL_PROD_ASSEMBLIES.CODE  not in ('" + strDocumentNo + "')"
        If clsCommon.myLen(strLocation) > 0 Then
            qry += " and TSPL_PROD_ASSEMBLIES.LOCATION_CODE='" & strLocation & "'"
        End If


        qry += " union all  "

        qry += " select  TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE AS ICode,TSPL_PJC_ASSEMBLIES.LOCATION_CODE as Location,"
        qry += " TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_QUANTITY as Qty,"
        qry += " (case when TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly' then  -1 else  1 end) AS RI,"
        qry += " TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_UNIT_CODE as UnitCode from TSPL_PJC_ASSEMBLIES "
        qry += " inner JOIN TSPL_PROD_ASSEMBLIES_ITEM_DETAIL ON TSPL_PJC_ASSEMBLIES.CODE=TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.ASSEMBLY_CODE "
        qry += " where TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly'  and  TSPL_PJC_ASSEMBLIES.POSTED=0 and TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE='" + strICode + "'  and TSPL_PJC_ASSEMBLIES.CODE  not in ('" + strDocumentNo + "')"
        If clsCommon.myLen(strLocation) > 0 Then
            qry += " and TSPL_PJC_ASSEMBLIES.LOCATION_CODE='" & strLocation & "'"
        End If



        qry += " union all  "

        qry += " select  TSPL_WRECKAGE_BOOKING.Item_Code AS ICode,TSPL_WRECKAGE_ENTRY.LOCATION_CODE as Location,"
        qry += " TSPL_WRECKAGE_BOOKING.WRECKAGE_QTY as Qty, -1 AS RI,"
        qry += " TSPL_WRECKAGE_BOOKING.Unit_Code as UnitCode from TSPL_WRECKAGE_ENTRY "
        qry += " inner JOIN TSPL_WRECKAGE_BOOKING ON TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE=TSPL_WRECKAGE_BOOKING.WRECKAGE_CODE "
        qry += " where TSPL_WRECKAGE_ENTRY.POSTED=0 and TSPL_WRECKAGE_BOOKING.ITEM_CODE='" + strICode + "'  and TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE  not in ('" + strDocumentNo + "')"
        If clsCommon.myLen(strLocation) > 0 Then
            qry += " and TSPL_WRECKAGE_ENTRY.LOCATION_CODE='" & strLocation & "'"
        End If


        qry += " union all " + Environment.NewLine

        qry += " select TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code as ICode,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code as Locaion,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Quantity as Qty,-1 as RI,TSPL_ITEM_MASTER.Unit_code AS Uom "
        qry += " from TSPL_LOADING_TANKER_DETAIL_BULKSALE "
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code and TSPL_ITEM_MASTER.Product_Type='MI' "
        qry += " where TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code='" + strICode + "' and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code='" + strMainLocation + "' AND TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No='" + strLocation + "' and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Quantity<>0  "
        qry += " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No not in ('" + strDocumentNo + "')"
        qry += " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No not in (select LoadingTanker_No FROM TSPL_Quality_Check_BulkSale LEFT OUTER JOIN TSPL_Dispatch_BulkSale ON TSPL_Dispatch_BulkSale.QC_Code=TSPL_Quality_Check_BulkSale.QC_No WHERE ISNULL(TSPL_Dispatch_BulkSale.QC_Code,'')<>'')"
        'qry += " and not exists (select 1 from TSPL_LOADING_TANKER_DETAIL_BULKSALE Left outer Join TSPL_Quality_Check_BulkSale on TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No=TSPL_Quality_Check_BulkSale.LoadingTanker_No)"

        '' added by richa agarwal to include transactions of store adjustment whose trans type is Out and milk type is 1
        qry += "union all " + Environment.NewLine &
            "  select TSPL_ADJUSTMENT_DETAIL.Item_Code as ICode,TSPL_ADJUSTMENT_HEADER.MainLocationCode  as Locaion,TSPL_ADJUSTMENT_DETAIL.Item_Quantity ,-1 as RI,TSPL_ADJUSTMENT_DETAIL.Unit_code AS Uom " &
            " from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No =TSPL_ADJUSTMENT_DETAIL.Adjustment_No  " &
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code and TSPL_ITEM_MASTER.Product_Type='MI' " &
            " where TSPL_ADJUSTMENT_HEADER.Posted ='N' and TSPL_ADJUSTMENT_DETAIL.Item_Code='" + strICode + "' and TSPL_ADJUSTMENT_HEADER.Loc_Code ='" + strLocation + "' and TSPL_ADJUSTMENT_HEADER.MainLocationCode ='" & strMainLocation & "' and TSPL_ADJUSTMENT_DETAIL.Item_Quantity <>0 " &
            " and TSPL_ADJUSTMENT_HEADER.Trans_Type  ='Out' and TSPL_ADJUSTMENT_HEADER .IsMilkType =1 and TSPL_ADJUSTMENT_DETAIL.Adjustment_No  not in ('" + strDocumentNo + "') " + Environment.NewLine

        qry += " )xx" + Environment.NewLine &
               " left join (" & qryMinBal & ") as Minimum_Bal on xx.ICode=Minimum_Bal.Item_Code and xx.Location=Minimum_Bal.Location_Code "
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM"
        qry += " )xxx group by ICode,Location"
        Dim BalQty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Dim dblConvFac As Decimal = clsItemMaster.GetConvertionFactor(strICode, strUOM, trans)

        Return BalQty / IIf(dblConvFac <= 0, 1, dblConvFac)
    End Function

    Public Shared Function GetStdQty(ByVal Trans As SqlTransaction, ByVal FATKG As Double, ByVal SNFKG As Double, ByVal TransDate As DateTime) As Decimal
        Dim retVal As Double = 0
        Try
            Dim qry As String = "select  top 1 (" + clsCommon.myCstr(FATKG) + "*CAST(Ratio as decimal)/FAT_Pers)+(" + clsCommon.myCstr(SNFKG) + "*SNF_Ratio/SNF_Pers) as Qty  from TSPL_MILK_PRICE_MASTER where Effective_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(TransDate), "dd/MMM/yyyy hh:mm tt") + "' order by Effective_Date desc"
            retVal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, Trans))
        Catch ex As Exception
        End Try
        Return retVal
    End Function

End Class

Public Class clsOutputEntry
#Region "Variables"
    Public Doc_Code As String = Nothing
    Public Doc_Date As DateTime = Nothing
    Public Plant_Code As String = Nothing
    Public Mcc_Code As String = Nothing
    Public FromDate As DateTime? = Nothing
    Public ToDate As DateTime? = Nothing
    Public Output_Type As String = Nothing
    Public FatPer As Decimal = 0
    Public SNFPer As Decimal = 0
    Public FatKG As Decimal = 0
    Public SNFKG As Decimal = 0
    Public QtyKG As Decimal = 0
    Public QtyLTR As Decimal = 0
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Post_Date As DateTime? = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsOutputEntry, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsOutputEntry, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Fixed Asset", "Asset Account Change", obj.Loc_Code, obj.Doc_Date, trans)
        Try
            If Not isNewEntry Then
                Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Status from TSPL_OUTPUT_ENTRY Where Doc_Code='" + obj.Doc_Code + "'", trans))
                If Status = 1 Then
                    Throw New Exception("This document is already posted.")
                End If
            End If

            Dim strDocNo As String = ""

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Plant_Code", obj.Plant_Code)
            clsCommon.AddColumnsForChange(coll, "Mcc_Code", obj.Mcc_Code)
            clsCommon.AddColumnsForChange(coll, "FromDate", clsCommon.GetPrintDate(obj.FromDate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "ToDate", clsCommon.GetPrintDate(obj.ToDate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Output_Type", obj.Output_Type)
            clsCommon.AddColumnsForChange(coll, "FatPer", obj.FatPer)
            clsCommon.AddColumnsForChange(coll, "SNFPer", obj.SNFPer)
            clsCommon.AddColumnsForChange(coll, "FatKG", obj.FatKG)
            clsCommon.AddColumnsForChange(coll, "SNFKG", obj.SNFKG)
            clsCommon.AddColumnsForChange(coll, "QtyKG", obj.QtyKG)
            clsCommon.AddColumnsForChange(coll, "QtyLTR", obj.QtyLTR)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
                obj.Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Doc_Date, clsDocType.OutputEntry, "", obj.Plant_Code)
                If (clsCommon.myLen(obj.Doc_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.Doc_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OUTPUT_ENTRY", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OUTPUT_ENTRY", OMInsertOrUpdate.Update, "TSPL_OUTPUT_ENTRY.Doc_Code='" + obj.Doc_Code + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function


    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsOutputEntry
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsOutputEntry
        Dim obj As clsOutputEntry = Nothing
        Dim qry As String = "SELECT TSPL_OUTPUT_ENTRY.*  FROM TSPL_OUTPUT_ENTRY  "
        qry += " where 2=2  "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_OUTPUT_ENTRY.Doc_Code = (select MIN(Doc_Code) from TSPL_OUTPUT_ENTRY where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_OUTPUT_ENTRY.Doc_Code = (select Max(Doc_Code) from TSPL_OUTPUT_ENTRY where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_OUTPUT_ENTRY.Doc_Code = (select Min(Doc_Code) from TSPL_OUTPUT_ENTRY where Doc_Code>'" + strPONo + "'" + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_OUTPUT_ENTRY.Doc_Code = (select Max(Doc_Code) from TSPL_OUTPUT_ENTRY where Doc_Code<'" + strPONo + "'" + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_OUTPUT_ENTRY.Doc_Code = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsOutputEntry()
            obj.Doc_Code = clsCommon.myCstr(dt.Rows(0)("Doc_Code"))
            obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
            obj.Plant_Code = clsCommon.myCstr(dt.Rows(0)("Plant_Code"))
            obj.Mcc_Code = clsCommon.myCstr(dt.Rows(0)("Mcc_Code"))
            obj.FromDate = clsCommon.myCDate(dt.Rows(0)("FromDate"))
            obj.ToDate = clsCommon.myCDate(dt.Rows(0)("ToDate"))
            obj.Output_Type = clsCommon.myCstr(dt.Rows(0)("Output_Type"))
            obj.FatPer = clsCommon.myCdbl(dt.Rows(0)("FatPer"))
            obj.SNFPer = clsCommon.myCdbl(dt.Rows(0)("SNFPer"))
            obj.FatKG = clsCommon.myCdbl(dt.Rows(0)("FatKG"))
            obj.SNFKG = clsCommon.myCdbl(dt.Rows(0)("SNFKG"))
            obj.QtyKG = clsCommon.myCdbl(dt.Rows(0)("QtyKG"))
            obj.QtyLTR = clsCommon.myCdbl(dt.Rows(0)("QtyLTR"))

            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Post_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If

        End If

        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String
        Try

            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim dtPostDate As DateTime = clsCommon.GETSERVERDATE(trans) ' clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsOutputEntry = clsOutputEntry.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.Post_Date, "dd/MM/yyyy"))
            End If
            ''richa VIJ/26/07/21-001248
            UpdateInventoryMovement(obj, trans, False)
            CreateJournalEntry(obj, trans, "")
            'Dim ArryLst As ArrayList = New ArrayList()

            'Dim strQ As String = "select * from TSPL_ASSET_ACCOUNT_CHANGE_DETAIL" &
            '         " where TSPL_ASSET_ACCOUNT_CHANGE_DETAIL.Doc_Code ='" & strDocNo & "'"
            'Dim dtData As DataTable = clsDBFuncationality.GetDataTable(strQ, trans)
            'If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
            '    For i As Integer = 0 To dtData.Rows.Count - 1
            '        Dim strAc_Control As String = clsERPFuncationality.ChangeGLAccountLocationSegment(dtData.Rows(i)("ChangedAc_Code"), obj.Loc_Code, trans)
            '        If clsCommon.myLen(strAc_Control) <= 0 Then
            '            Throw New Exception("GL Account " & dtData.Rows(i)("ChangedAc_Code") & " not Found For Location " & obj.Loc_Code & "")
            '        End If
            '        ArryLst.Add(New String() {strAc_Control, clsCommon.myCdbl(dtData.Rows(i)("Item_Net_Amt"))})

            '        Dim strWIP_AC As String = clsERPFuncationality.ChangeGLAccountLocationSegment(dtData.Rows(i)("Ac_Code"), obj.Loc_Code, trans)
            '        If clsCommon.myLen(strWIP_AC) <= 0 Then
            '            Throw New Exception("GL Account " & dtData.Rows(i)("Ac_Code") & " not Found For Location " & obj.Loc_Code & "")
            '        End If
            '        ArryLst.Add(New String() {strWIP_AC, clsCommon.myCdbl(dtData.Rows(i)("Item_Net_Amt")) * -1})
            '    Next

            '    transportSql.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Doc_Date, "Asset Account Change, Against Acquisition Code:  " & obj.Acquisition_Code, "AQ-AC", "Asset Account Change", strDocNo, "Asset Account Change", "V", strDocNo, "Asset Account Change", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
            'End If


            qry = "Update TSPL_OUTPUT_ENTRY set Status=1, Posted_Date='" + clsCommon.GetPrintDate(dtPostDate, "dd/MMM/yyyy hh:mm tt") + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Doc_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function UpdateInventoryMovement(ByVal obj As clsOutputEntry, ByVal trans As SqlTransaction, Optional ByVal UpdateInventory As Boolean = False) As Boolean
        Try

            Dim TransType_Str As String = ""
            Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            If UpdateInventory = True Then
                clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" & obj.Doc_Code & "'", trans)
            End If
            Dim strRgpNo As String = Nothing
            Dim intCounter As Integer = 0
            intCounter = intCounter + 1
            Dim strItemCode As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans))
            If clsCommon.myLen(strItemCode) > 0 Then

                Dim strItemType As String = clsItemMaster.GetItemType(strItemCode, trans)
                Dim strItemTypeToSave As String = ""
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                Else
                    strItemTypeToSave = strItemType
                End If
                Dim objInventoryMovemnt As New clsInventoryMovementNew()
                objInventoryMovemnt.InOut = "O"

                objInventoryMovemnt.Location_Code = obj.Mcc_Code
                objInventoryMovemnt.Item_Code = strItemCode
                objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(strItemCode, trans)
                objInventoryMovemnt.Qty = obj.QtyKG
                objInventoryMovemnt.UOM = "KG"
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "FT"
                End If
                objInventoryMovemnt.ItemType = strItemTypeToSave
                objInventoryMovemnt.FAT_KG = obj.FatKG
                objInventoryMovemnt.SNF_KG = obj.SNFKG
                objInventoryMovemnt.FAT_Per = obj.FatPer
                objInventoryMovemnt.SNF_Per = obj.SNFPer
                objInventoryMovemnt.CalculateAvgCost = True
                objInventoryMovemnt.Punching_Date = obj.ToDate
                ArrInventoryMovement.Add(objInventoryMovemnt)

                clsInventoryMovementNew.SaveData("OUT-PUT", obj.Doc_Code, obj.Doc_Date, clsCommon.GetPrintDate(obj.Doc_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            Else
                Throw New Exception("Please enter item on MCCDefaultMilkItem in Utility")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Sub CreateJournalEntry(ByVal obj As clsOutputEntry, ByVal trans As SqlTransaction, Optional ByVal strVoucherNoForRecreateOnly As String = Nothing, Optional ByVal IsDairyModule As Boolean = False)
        Try
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim strInventoryControlAc As String = ""
            Dim strStoreConsumptionAC As String = ""
            Dim dblTotalCost As Double = 0
            Dim strItemCode As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans))
            If clsCommon.myLen(strItemCode) > 0 Then

                strStoreConsumptionAC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.Store_Consumption_Acc FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
              " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
               " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + strItemCode + "'", trans))
                If clsCommon.myLen(strStoreConsumptionAC) = 0 Then
                    Throw New Exception("Please set Store Consumption Account for first item")
                End If
                strStoreConsumptionAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strStoreConsumptionAC, obj.Mcc_Code, False, trans)


                Dim dblCogsCost As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end) as COst from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0 and Source_Doc_No='" & obj.Doc_Code & "'", trans))

                Dim Acc() As String = {strStoreConsumptionAC, dblCogsCost}
                ArryLstGLAC.Add(Acc)

                Dim strSql As String = "select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end as Cost from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code  where isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0 and Source_Doc_No='" & obj.Doc_Code & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows
                        strInventoryControlAc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                        " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
                        " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'", trans))
                        If clsCommon.myLen(strInventoryControlAc) = 0 Then
                            Throw New Exception("Please set Inventory Control Account for first item")
                        End If
                        strInventoryControlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInventoryControlAc, obj.Mcc_Code, trans)


                        Dim Acc1() As String = {strInventoryControlAc, -1 * clsCommon.myCdbl(dr("Cost")), "", "", "", "", "", "", "I"}
                        ArryLstGLAC.Add(Acc1)

                        clsInventoryMovement.UpdateInvControlAccount(obj.Doc_Code, "OUT-PUT", clsCommon.myCstr(dr("Item_Code")), "", strInventoryControlAc, "", trans)
                    Next
                End If
                If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                    transportSql.FunGrnlEntryWithTrans(obj.Mcc_Code, False, strVoucherNoForRecreateOnly, trans, obj.ToDate, "Journal Entry created Against Output Entry " & obj.Doc_Code & "", "OT-PT", "Output Entry", obj.Doc_Code, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, "")
                Else
                    transportSql.FunGrnlEntryWithTrans(obj.Mcc_Code, False, trans, obj.ToDate, "Journal Entry created Against Output Entry " & obj.Doc_Code & "", "OT-PT", "Output Entry", obj.Doc_Code, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, "")
                End If
            Else
                Throw New Exception("Please enter item on MCCDefaultMilkItem in Utility")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = ""
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document not found to Delete")
        End If
        Dim obj As clsOutputEntry = clsOutputEntry.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then
            Try
                If (obj.Status = ERPTransactionStatus.Approved) Then
                    Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Post_Date, "dd/MM/yyyy"))
                End If

                qry = "delete from TSPL_OUTPUT_ENTRY where Doc_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsOutputEntry.ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        ''Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            Dim Qry As String = String.Empty
            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code in('OT-PT') and Source_Doc_No='" + strDocNo + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Qry = "delete from TSPL_INVENTORY_MOVEMENT_New where Source_Doc_No='" + strDocNo + "' and Trans_Type='OUT-PUT'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            clsDBFuncationality.ExecuteNonQuery("Update TSPL_OUTPUT_ENTRY set Status=0 where Doc_Code ='" + strDocNo + "'", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    ''---------------

    'Public Shared Function CheckCode(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
    '    Dim Qry As String = "select count(Acquisition_Code) from TSPL_ACQUISITION_HEAD where Acquisition_Code='" & Code & "'"
    '    Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans))
    '    If count > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function


End Class

