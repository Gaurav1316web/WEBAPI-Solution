Imports common
Imports System.Data.SqlClient

Public Class clsItemReorderLevel
#Region "Variables"
    Public Apply As Char = "N"
    Public Item_Code As String = ""
    Public Item_Description As String = ""
    Public Min_Level As Double = 0
    Public Min_Level_Tollerence As Double = 0
    Public Max_Level As Double = 0
    Public Max_Level_Tollerence As Double = 0
    Public Reorder_Level As Double = 0
    Public Reorder_Level_Tollerence As Double = 0
    Public Item_Category_Code As String = ""
    Public Unit_Code As String = ""
    Public Location_Code As String = ""
    Public Reorder_Qty As Double = 0
#End Region

    Public Shared Function SaveData(ByVal Arr As List(Of clsItemReorderLevel)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = True
        Try
            For Each obj As clsItemReorderLevel In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Apply", obj.Apply)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Min_Level", obj.Min_Level)
                clsCommon.AddColumnsForChange(coll, "Min_Level_Tollerence", obj.Min_Level_Tollerence)
                clsCommon.AddColumnsForChange(coll, "Max_Level", obj.Max_Level)
                clsCommon.AddColumnsForChange(coll, "Max_Level_Tollerence", obj.Max_Level_Tollerence)
                clsCommon.AddColumnsForChange(coll, "Reorder_Level", obj.Reorder_Level)
                clsCommon.AddColumnsForChange(coll, "Reorder_Level_Tollerence", obj.Reorder_Level_Tollerence)
                clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)
                clsCommon.AddColumnsForChange(coll, "Reorder_Qty", obj.Reorder_Qty)
                clsCommon.AddColumnsForChange(coll, "Uom_Code", obj.Unit_Code, True)
                'done by stuti on 22/10/2016
                Dim count As Integer = clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_ITEM_REORDER_LEVEL_NEW WHERE Item_Code='" + obj.Item_Code + "' and Location_Code='" + obj.Location_Code + "'", trans)
                If count <= 0 Then
                    ' Modify by : Prabhakar Ticket Ref : BM00000009269
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_REORDER_LEVEL_NEW", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_REORDER_LEVEL_NEW", OMInsertOrUpdate.Update, "Item_Code='" + obj.Item_Code + "' and Location_Code='" + obj.Location_Code + "' ", trans)

                End If
            Next
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal itemtype As String) As DataTable
        Try
            ' Modified  by : Prabhakar Ticket Ref : BM00000009269
            Dim qry As String = " select CAST(Case When TSPL_ITEM_REORDER_LEVEL_NEW.Apply='Y' Then 1 Else 0 End as Bit) as Apply, TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.CSA_TYPE as Item_Group,stuff((select  ',' + TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  from TSPL_ITEM_MASTER_CATEGORY left outer  join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values where TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_Code    for xml path('')  ),1,1,'') Category_Type ,TSPL_ITEM_MASTER.Unit_Code,TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level,TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level_Tollerence,TSPL_ITEM_REORDER_LEVEL_NEW.Max_Level,TSPL_ITEM_REORDER_LEVEL_NEW.Max_Level_Tollerence,TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Level,TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Level_Tollerence,TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code   from TSPL_ITEM_MASTER left outer join TSPL_ITEM_REORDER_LEVEL_NEW  on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_REORDER_LEVEL_NEW.item_Code "
            qry += " where 2=2 and TSPL_ITEM_MASTER.CSA_TYPE ='" + itemtype + "' "
            qry += " group by TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_REORDER_LEVEL_NEW.Apply,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Unit_Code,TSPL_ITEM_MASTER.CSA_TYPE,TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level,TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level_Tollerence,TSPL_ITEM_REORDER_LEVEL_NEW.Max_Level,TSPL_ITEM_REORDER_LEVEL_NEW.Max_Level_Tollerence,TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Level,TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Level_Tollerence,TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code "
            Return clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetDataByLocation(ByVal itemtype As String, ByVal strlocationCode As String) As DataTable
        Try
            ' Modified  by : Prabhakar Ticket Ref : BM00000009269
            Dim qry As String = "  select CAST(Case When TSPL_ITEM_REORDER_LEVEL_NEW.Apply='Y' Then 1 Else 0 End as Bit) as Apply, TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.CSA_TYPE as Item_Group,stuff((select  ',' + TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  from TSPL_ITEM_MASTER_CATEGORY left outer  join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values where TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_Code    for xml path('')  ),1,1,'') Category_Type ,TSPL_ITEM_MASTER.Unit_Code,TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level,TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level_Tollerence,TSPL_ITEM_REORDER_LEVEL_NEW.Max_Level,TSPL_ITEM_REORDER_LEVEL_NEW.Max_Level_Tollerence,TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Level,TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Level_Tollerence,TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code   from TSPL_ITEM_MASTER left outer join TSPL_ITEM_REORDER_LEVEL_NEW  on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_REORDER_LEVEL_NEW.item_Code "
            qry += " where 2=2 and TSPL_ITEM_MASTER.CSA_TYPE='" + itemtype + "' and ( (TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code  is null  or TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code ='" + strlocationCode + "' or TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code ='') ) "
            qry += " group by TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_REORDER_LEVEL_NEW.Apply,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Unit_Code,TSPL_ITEM_MASTER.CSA_TYPE,TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level,TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level_Tollerence,TSPL_ITEM_REORDER_LEVEL_NEW.Max_Level,TSPL_ITEM_REORDER_LEVEL_NEW.Max_Level_Tollerence,TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Level,TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Level_Tollerence,TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code "
            Return clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetDataAll(ByVal itemtype As String, ByVal strlocationCode As String, ByVal CategoryType As ArrayList, ByVal CategoryValues As ArrayList, Optional ByVal IsApply As Integer = 2, Optional ByVal Itemcode As String = Nothing, Optional ByVal strmultilocation As String = Nothing) As DataTable
        Try
            Dim isFirstTime As Boolean = True
            Dim qry As String = Nothing
            Dim strpivot As String = Nothing
            Dim strpivotposition As String = Nothing
            If CategoryType.Count > 0 Then
                qry = "select stuff((select ','+'[' + TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION +']' from TSPL_ITEM_CATEGORY_LEVEL WHERE  TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE <>'Item Group' and TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE IN('" + clsCommon.GetMulcallStringWithComma(CategoryType).Replace(",", "','") + "') for xml path('') ), 1, 1, '') Category_Type_Desc"
                strpivot = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                qry = "select stuff((select ','+'Max('+'[' + TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION +']) as [' +TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION+']' from TSPL_ITEM_CATEGORY_LEVEL WHERE  TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE <>'Item Group' and  TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE IN('" + clsCommon.GetMulcallStringWithComma(CategoryType).Replace(",", "','") + "') for xml path('') ), 1, 1, '') Category_Type_Desc"
                strpivotposition = "," + clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            Else
                qry = "select stuff((select ','+'[' + TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION +']' from TSPL_ITEM_CATEGORY_LEVEL WHERE  TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE <>'Item Group' for xml path('') ), 1, 1, '') Category_Type_Desc"
                strpivot = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                qry = "select stuff((select ','+'Max('+'[' + TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION +']) as [' +TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION+']' from TSPL_ITEM_CATEGORY_LEVEL WHERE  TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE <>'Item Group' for xml path('') ), 1, 1, '') Category_Type_Desc"
                strpivotposition = "," + clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            End If
            If strpivot = Nothing Then
                Throw New Exception("No data found to display")
            End If
            qry = "select CAST(MAX(Apply) AS BIT) as Apply,Max([Item Code]) as [Item Code],Max([Item Description]) as [Item Description],Max([Item Group]) as [Item Group],Max([Item Type]) as [Item Type],Max([Unit Code]) as [Unit Code]" + strpivotposition + ",max([Min Level]) as [Min Level],max([Min Level Tolerance]) as [Min Level Tolerance],max([Max Level]) as [Max Level],max([Max Level Tolerance]) as [Max Level Tolerance],max([Reorder Level]) as [Reorder Level],max([Reorder Level Tolerance]) as [Reorder Level Tolerance],max([Reorder Qty]) as [Reorder Qty],max([Location Code]) as [Location Code] " & _
                " from (select (final.Apply) as Apply,final.Item_Code as [Item Code],final.Item_Desc as [Item Description],final.Item_Group as [Item Group],final.[Item Type] as [Item Type],final.catcode as catcode,final.catvalue as catvalue,final.Unit_Code as [Unit Code],isnull(final.Min_Level, 0) as [Min Level],isnull(final.Min_Level_Tollerence, 0) as [Min Level Tolerance], " & _
                " isnull(final.Max_Level, 0) as [Max Level],isnull(final.Max_Level_Tollerence, 0) as [Max Level Tolerance],isnull(final.Reorder_Level, 0) as [Reorder Level],isnull(final.Reorder_Level_Tollerence, 0) as [Reorder Level Tolerance],isnull(final.Reorder_Qty, 0) as [Reorder Qty],final.Location_Code as [Location Code] from (select (Case When TSPL_ITEM_REORDER_LEVEL_NEW.Apply = 'Y' Then 1 Else 0 End) as Apply, " & _
                " TSPL_ITEM_MASTER.Item_Code, (TSPL_ITEM_MASTER.Item_Desc) AS Item_Desc, (TSPL_ITEM_MASTER.CSA_TYPE) as Item_Group, (TSPL_ITEM_MASTER.Item_Type) as 'Item Type',case when isnull((TSPL_ITEM_REORDER_LEVEL_NEW.UOM_Code), '') = '' then (TSPL_ITEM_UOM_DETAIL.Uom_Code) else (TSPL_ITEM_REORDER_LEVEL_NEW.UOM_Code) End AS Unit_Code, " & _
                " (TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level) AS Min_Level , (TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level_Tollerence) AS Min_Level_Tollerence , (TSPL_ITEM_REORDER_LEVEL_NEW.Max_Level) AS Max_Level, (TSPL_ITEM_REORDER_LEVEL_NEW.Max_Level_Tollerence) AS Max_Level_Tollerence, (TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Level) AS Reorder_Level," & _
                " (TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Level_Tollerence) AS Reorder_Level_Tollerence, (TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Qty) AS Reorder_Qty, (TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code) AS Location_Code,(TSPL_ITEM_CATEGORY_LEVEL.Description) as catcode,(TSPL_ITEM_CATEGORY_LEVEL_VALUES.Description) as catvalue " & _
                " from TSPL_ITEM_MASTER " & _
                " left outer join TSPL_ITEM_REORDER_LEVEL_NEW on TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code = TSPL_ITEM_MASTER.item_Code left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code left outer join tspl_item_master_category on TSPL_ITEM_MASTER.Item_Code = tspl_item_master_category.Item_Code left outer join TSPL_ITEM_CATEGORY_LEVEL on " & _
                " tspl_item_master_category.item_category_code = TSPL_ITEM_CATEGORY_LEVEL.item_category_code left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on tspl_item_master_category.item_category_code = TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code and TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  = TSPL_ITEM_CATEGORY_LEVEL_VALUES.Code where 2 = 2 and TSPL_ITEM_UOM_DETAIL.Stocking_Unit='Y' "
            If clsCommon.myLen(strlocationCode) > 0 AndAlso clsCommon.myLen(strmultilocation) <= 0 Then
                qry += " and TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code in('" + strlocationCode + "')"
            ElseIf clsCommon.myLen(strlocationCode) <= 0 AndAlso clsCommon.myLen(strmultilocation) > 0 Then
                qry += " and TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code in('" + strmultilocation + "')"
            End If
            If clsCommon.myLen(itemtype) > 0 Then
                qry += " and TSPL_ITEM_MASTER.Item_Type in ('" + itemtype + "') "
            End If
            If clsCommon.myLen(Itemcode) > 0 Then
                qry += " and TSPL_ITEM_MASTER.Item_Code in ('" + Itemcode + "') "
            End If
            If CategoryType.Count > 0 Then
                qry += " and TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE IN('" + clsCommon.GetMulcallStringWithComma(CategoryType).Replace(",", "','") + "')"
            End If
            If CategoryValues.Count > 0 Then
                qry += " and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE IN('" + clsCommon.GetMulcallStringWithComma(CategoryValues).Replace(",", "','") + "')"
            End If
            If clsCommon.myLen(strmultilocation) <= 0 Then
                qry += " union "
                qry += "select 0 as Apply, TSPL_ITEM_MASTER.Item_Code, (TSPL_ITEM_MASTER.Item_Desc) AS Item_Desc, (TSPL_ITEM_MASTER.CSA_TYPE) as Item_Group, (TSPL_ITEM_MASTER.Item_Type)as 'Item Type', case when isnull((TSPL_ITEM_REORDER_LEVEL_NEW.UOM_Code), '') = '' then (TSPL_ITEM_UOM_DETAIL.Uom_Code) else (TSPL_ITEM_REORDER_LEVEL_NEW.UOM_Code) End AS Unit_Code, 0 AS Min_Level , 0 AS Min_Level_Tollerence , 0 AS Max_Level, 0 AS Max_Level_Tollerence, 0 AS Reorder_Level, 0 AS Reorder_Level_Tollerence, 0 AS Reorder_Qty, '' AS Location_Code, (TSPL_ITEM_CATEGORY_LEVEL.Description) as catcode, (TSPL_ITEM_CATEGORY_LEVEL_VALUES.Description)as catvalue " & _
                        "from TSPL_ITEM_MASTER left outer join TSPL_ITEM_REORDER_LEVEL_NEW on TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code = TSPL_ITEM_MASTER.item_Code left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code left outer join tspl_item_master_category on TSPL_ITEM_MASTER.Item_Code = tspl_item_master_category.Item_Code left outer join TSPL_ITEM_CATEGORY_LEVEL on tspl_item_master_category.item_category_code = TSPL_ITEM_CATEGORY_LEVEL.item_category_code left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on tspl_item_master_category.item_category_code = TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code and TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values = TSPL_ITEM_CATEGORY_LEVEL_VALUES.Code " & _
                        "where 2 = 2 and TSPL_ITEM_UOM_DETAIL.Stocking_Unit='Y'"
                If clsCommon.myLen(itemtype) > 0 Then
                    qry += " and TSPL_ITEM_MASTER.Item_Type in ('" + itemtype + "') "
                End If
                If clsCommon.myLen(Itemcode) > 0 Then
                    qry += " and TSPL_ITEM_MASTER.Item_Code in ('" + Itemcode + "') "
                End If
                If CategoryType.Count > 0 Then
                    qry += " and TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE IN('" + clsCommon.GetMulcallStringWithComma(CategoryType).Replace(",", "','") + "')"
                End If
                If CategoryValues.Count > 0 Then
                    qry += " and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE IN('" + clsCommon.GetMulcallStringWithComma(CategoryValues).Replace(",", "','") + "')"
                End If
            End If

            qry += " ) as final "
            If IsApply <> 2 Then
                qry += " where final.Apply='" + clsCommon.myCstr(IsApply) + "'"
            End If
            qry += ") as fin2 pivot( max(fin2.catvalue) for fin2.catcode in (" + strpivot + ")) as an group by [Item Code] "
            If clsCommon.myLen(strmultilocation) > 0 Then
                qry += " ,[Location Code]"
            End If
            Return clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetItemsForReOrder(ByVal strTillDate As String) As DataTable
        Try
            Dim qry As String = "Select CAST(0 as bit) as [Select], ZZZ.ICode, TSPL_ITEM_MASTER.Item_Desc, ZZZ.BalanceQty, TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Level from (" & _
        " select ICode,SUM(Qty * case when TransType='' then 1 else 0 end)as BalanceQty,SUM(Qty * case when TransType='' then 0 else 1 end)as CommitQty,SUM(Qty *RI )as ActualBalanceQty from (" & _
        " select  xx.TransType,xx.TransCode,xx.DocNo, xx.ICode,xx.Location,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,( (xx.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) as Qty" & _
        " from (" & _
        " select '' as TransType,'' as TransCode,'' as DocNo, Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM  from( select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew from( select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,TSPL_INVENTORY_MOVEMENT.Qty   ,TSPL_INVENTORY_MOVEMENT.UOM as UOMNew  from TSPL_INVENTORY_MOVEMENT  where TSPL_INVENTORY_MOVEMENT.Qty<>0 and TSPL_INVENTORY_MOVEMENT.Item_Code='D0001' and 2=(case when TSPL_INVENTORY_MOVEMENT.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(strTillDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end))xxx )xxxx group by Item_Code,Location_Code,UOMNew" & _
        " union all" & _
        " select 'Purchase Return' as TransType,'PurchaseReturn' as TransCode,TSPL_PR_HEAD.PR_No as DocNo, TSPL_PR_DETAIL.Item_Code as ICode,TSPL_PR_DETAIL.Location as Locaion,TSPL_PR_DETAIL.PR_Qty as Qty,-1 as RI,TSPL_PR_DETAIL.Unit_code AS Uom  from TSPL_PR_DETAIL  left outer join TSPL_PR_HEAD on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No where TSPL_PR_HEAD.Status=0 and TSPL_PR_DETAIL.PR_Qty<>0" & _
        " union all" & _
        " select 'IC-AD' as TransType,'ICAdj' as TransCode,TSPL_ADJUSTMENT_HEADER.Adjustment_No as DocNo, TSPL_ADJUSTMENT_DETAIL.Item_Code as ICode,TSPL_ADJUSTMENT_HEADER.Loc_Code as Locaion,TSPL_ADJUSTMENT_DETAIL.Item_Quantity as Qty,-1 as RI,TSPL_ADJUSTMENT_DETAIL.Unit_Code AS Uom  from TSPL_ADJUSTMENT_DETAIL  left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No where TSPL_ADJUSTMENT_HEADER.Posted='N' and TSPL_ADJUSTMENT_DETAIL.Item_Quantity<>0  and TSPL_ADJUSTMENT_DETAIL.Adjustment_Type in ('BD','QD')" & _
        " union all" & _
        " select 'RGP' as TransType,'RGP' as TransCode,TSPL_RGP_HEAD.RGP_No as DocNo, TSPL_RGP_DETAIL.Item_Code as ICode,TSPL_RGP_HEAD.Location as Locaion,TSPL_RGP_DETAIL.RGP_Qty as Qty,-1 as RI,TSPL_RGP_DETAIL.Unit_code AS Uom  from TSPL_RGP_DETAIL  left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No where TSPL_RGP_HEAD.Status=0 and TSPL_RGP_DETAIL.RGP_Qty<>0" & _
        " union all" & _
        " select 'Scrap' as TransType,'ScrapShipment' as TransCode,TSPL_SCRAPSALE_HEAD.shipment_No as DocNo, TSPL_SCRAPSALE_DETAIL.Item_Code as ICode,TSPL_SCRAPSALE_HEAD.Loc_Code as Locaion,TSPL_SCRAPSALE_DETAIL.shipped_Qty as Qty,-1 as RI,TSPL_SCRAPSALE_DETAIL.Unit_code AS Uom  from TSPL_SCRAPSALE_DETAIL  left outer join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No where TSPL_SCRAPSALE_HEAD.IsPost=0 and TSPL_SCRAPSALE_DETAIL.shipped_Qty<>0" & _
        " union all" & _
        " select 'Issue/Return/Transfer' as TransType,'IssueReturnTransfer' as TransCode,TSPL_IssueReturn_HEAD.Doc_No as DocNo, TSPL_IssueReturn_DETAIL.Item_Code as ICode,TSPL_IssueReturn_HEAD.From_Location as Locaion,TSPL_IssueReturn_DETAIL.Issued_Qty as Qty,-1 as RI,TSPL_IssueReturn_DETAIL.Unit_code AS Uom  from TSPL_IssueReturn_DETAIL  left outer join TSPL_IssueReturn_HEAD on TSPL_IssueReturn_HEAD.Doc_No=TSPL_IssueReturn_DETAIL.Doc_No where TSPL_IssueReturn_HEAD.Status=0 and TSPL_IssueReturn_DETAIL.Issued_Qty<>0" & _
        " union all" & _
        " select  'Shipment' as TransType,'SDShipment' as TransCode,TSPL_SD_SHIPMENT_HEAD.Document_Code as DocNo, TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location as Locaion,TSPL_SD_SHIPMENT_DETAIL.Qty as Qty,-1 as RI,TSPL_SD_SHIPMENT_DETAIL.Unit_code AS Uom   from TSPL_SD_SHIPMENT_DETAIL  left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_HEAD.Status=0 and TSPL_SD_SHIPMENT_DETAIL.Qty<>0" & _
        " union all" & _
        " select 'Assemblies' as TransType,'Assemblies' as TransCode,TSPL_PJC_ASSEMBLIES.CODE as DocNo, Main_Item_Code as ICode,LOCATION_CODE as Location,QUANTITY,(case when TRANSACTION_TYPE='Assembly' then 1  else -1 end) as RI, BUILD_ITEM_UNIT_CODE as UnitCode from TSPL_PJC_ASSEMBLIES where TSPL_PJC_ASSEMBLIES.POSTED=0 and  TSPL_PJC_ASSEMBLIES.Main_Item_Code='D0001'  and TSPL_PJC_ASSEMBLIES.CODE  not in ('') union all   select  'Assemblies' as TransType,'Assemblies' as TransCode,TSPL_PJC_ASSEMBLIES.CODE as DocNo, TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE AS ICode,TSPL_PJC_ASSEMBLIES.LOCATION_CODE as Location, TSPL_MF_BOM_DETAIL.CONSM_QUANTITY*(TSPL_PJC_ASSEMBLIES.QUANTITY/TSPL_PJC_ASSEMBLIES.BUILD_QUANTITY) as Qty, (case when TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly' then  -1 else  1 end) AS RI, TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE as UnitCode from TSPL_PJC_ASSEMBLIES  inner join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_PJC_ASSEMBLIES.BOM_CODE inner JOIN TSPL_MF_BOM_DETAIL ON TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_MF_BOM_DETAIL.BOM_CODE  where TSPL_PJC_ASSEMBLIES.POSTED=0" & _
        " )xx left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM" & _
        " left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode WHERE TSPL_ITEM_UOM_DETAIL.Stocking_Unit='Y'" & _
        " )FinalQry group by ICode" & _
        " ) ZZZ LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=ZZZ.ICode Left Outer Join TSPL_ITEM_REORDER_LEVEL_NEW ON TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code=ZZZ.ICode WHERE TSPL_ITEM_REORDER_LEVEL_NEW.Apply='Y' AND ZZZ.BalanceQty<=TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Level"
            Return clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetItemReOrderQty(ByVal strItemCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Integer
        Try
            Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Reorder_Level-(Reorder_Level*Reorder_Level_Tollerence/100) from TSPL_ITEM_REORDER_LEVEL_NEW WHERE Item_Code='" + strItemCode + "' AND Apply='Y'", trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'KUNAL > UDL > ADDED - NEW FUNCTION SO THAT PREVIOUS WILL NOT AFFECTED
    Public Shared Function GetDataAll(ByVal companyCode As String, ByVal itemtype As String, ByVal strlocationCode As String, ByVal CategoryType As ArrayList, ByVal CategoryValues As ArrayList, Optional ByVal IsApply As Integer = 2, Optional ByVal Itemcode As String = Nothing, Optional ByVal strmultilocation As String = Nothing) As DataTable
        Try
            Dim isFirstTime As Boolean = True
            Dim qry As String = Nothing
            Dim strpivot As String = Nothing
            Dim strpivotposition As String = Nothing
            If CategoryType.Count > 0 Then
                qry = "select stuff((select ','+'[' + TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION +']' from TSPL_ITEM_CATEGORY_LEVEL WHERE TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE IN('" + clsCommon.GetMulcallStringWithComma(CategoryType).Replace(",", "','") + "') for xml path('') ), 1, 1, '') Category_Type_Desc"
                strpivot = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                qry = "select stuff((select ','+'Max('+'[' + TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION +']) as [' +TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION+']' from TSPL_ITEM_CATEGORY_LEVEL WHERE TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE IN('" + clsCommon.GetMulcallStringWithComma(CategoryType).Replace(",", "','") + "') for xml path('') ), 1, 1, '') Category_Type_Desc"
                strpivotposition = "," + clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            Else
                qry = "select stuff((select ','+'[' + TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION +']' from TSPL_ITEM_CATEGORY_LEVEL for xml path('') ), 1, 1, '') Category_Type_Desc"
                strpivot = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                qry = "select stuff((select ','+'Max('+'[' + TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION +']) as [' +TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION+']' from TSPL_ITEM_CATEGORY_LEVEL for xml path('') ), 1, 1, '') Category_Type_Desc"
                strpivotposition = "," + clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            End If
            qry = " SELECT " & _
                  " fnlResult.[Category Code] AS [Code]  ," & _
                  " fnlResult.[Category Value] AS [Category Desc] ," & _
                  " fnlResult.[Item Code] AS [Item Code] ," & _
                  " fnlresult.[Item Description] ," & _
                  " fnlResult.[Unit Code] AS [U/M] ," & _
                  " fnlResult.[Min Level] AS [Min Qty] ," & _
                  " fnlResult.[Max Level] AS [Max Qty] ," & _
                  " fnlResult.[Reorder Level] , " & _
                  " fnlResult.[Reorder Qty] , " & _
                  " ISNULL([Balance_Qty], 0) AS [Closing Qty] ," & _
                  " '' AS [Leading Time] , " & _
                  " '' AS [Remark] , " & _
                  " fnlResult.[Loc Desc] , " & _
                  " fnlResult.[Full Add] , " & _
                  " fnlResult.[Comp Name] " & _
                " FROM (" & _
                " select CAST(MAX(Apply) AS BIT) as Apply,Max([Item Code]) as [Item Code],Max([Item Description]) as [Item Description],Max([Item Group]) as [Item Group],Max([Item Type]) as [Item Type],Max([Unit Code]) as [Unit Code]" + strpivotposition + ",max([Min Level]) as [Min Level],max([Min Level Tolerance]) as [Min Level Tolerance],max([Max Level]) as [Max Level],max([Max Level Tolerance]) as [Max Level Tolerance],max([Reorder Level]) as [Reorder Level],max([Reorder Level Tolerance]) as [Reorder Level Tolerance],max([Reorder Qty]) as [Reorder Qty],max([Location Code]) as [Location Code] , " & _
                " ISNULL([Category Code], '') AS [Category Code]," & _
                  " MAX(ISNULL([Category Value], '')) AS [Category Value], " & _
                  " max([Loc Desc]) as [Loc Desc], max([Loc Full Address]) as [Full Add], " & _
                  " SUM([Stock_Qty]) [Stock_Qty], MAX([Stock_UOM]) [Stock_UOM], " & _
                  " MAX(PUNCHING_DAte) PUNCHING_DAte,  " & _
                  " MAX(InOut) InOut, " & _
                  " SUM(STOCK_QTY * (CASE " & _
                  "  WHEN PUNCHING_DAte <= GETDATE() THEN 1 " & _
                  "  ELSE 0 " & _
                  " END) * (CASE " & _
                  "  WHEN InOut = 'I' THEN 1 " & _
                  "  ELSE -1 " & _
                  " END)) AS [Balance_Qty] ," & _
                  " max([Comp Name]) as  [Comp Name] " & _
                " from (select (final.Apply) as Apply,final.Item_Code as [Item Code],final.Item_Desc as [Item Description],final.Item_Group as [Item Group],final.[Item Type] as [Item Type],final.catcode as catcode,final.catvalue as catvalue,final.Unit_Code as [Unit Code],isnull(final.Min_Level, 0) as [Min Level],isnull(final.Min_Level_Tollerence, 0) as [Min Level Tolerance], " & _
                " isnull(final.Max_Level, 0) as [Max Level],isnull(final.Max_Level_Tollerence, 0) as [Max Level Tolerance],isnull(final.Reorder_Level, 0) as [Reorder Level],isnull(final.Reorder_Level_Tollerence, 0) as [Reorder Level Tolerance],isnull(final.Reorder_Qty, 0) as [Reorder Qty],final.Location_Code as [Location Code] ,  final.catcode AS [Category Code],  final.catvalue AS [Category Value],  final.[Loc Desc], final.[Loc Full Address],  [Comp Name], subQryClosingQty.* " & _
                " from (select (Case When TSPL_ITEM_REORDER_LEVEL_NEW.Apply = 'Y' Then 1 Else 0 End) as Apply, " & _
                " TSPL_ITEM_MASTER.Item_Code, (TSPL_ITEM_MASTER.Item_Desc) AS Item_Desc, (TSPL_ITEM_MASTER.CSA_TYPE) as Item_Group, (TSPL_ITEM_MASTER.Item_Type) as 'Item Type',case when isnull((TSPL_ITEM_REORDER_LEVEL_NEW.UOM_Code), '') = '' then (TSPL_ITEM_UOM_DETAIL.Uom_Code) else (TSPL_ITEM_REORDER_LEVEL_NEW.UOM_Code) End AS Unit_Code, " & _
                " (TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level) AS Min_Level , (TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level_Tollerence) AS Min_Level_Tollerence , (TSPL_ITEM_REORDER_LEVEL_NEW.Max_Level) AS Max_Level, (TSPL_ITEM_REORDER_LEVEL_NEW.Max_Level_Tollerence) AS Max_Level_Tollerence, (TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Level) AS Reorder_Level," & _
                " (TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Level_Tollerence) AS Reorder_Level_Tollerence, (TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Qty) AS Reorder_Qty, (TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code) AS Location_Code,(TSPL_ITEM_CATEGORY_LEVEL.Description) as catcode,(TSPL_ITEM_CATEGORY_LEVEL_VALUES.Description) as catvalue," & _
                "  TSPL_LOCATION_MASTER.Location_Desc as [Loc Desc], concat(coalesce(TSPL_LOCATION_MASTER.Add1,''),',',coalesce(TSPL_LOCATION_MASTER.add2,''),',',coalesce(TSPL_LOCATION_MASTER.Add3,''),',',coalesce(TSPL_LOCATION_MASTER.city_code,''),',',coalesce(TSPL_LOCATION_MASTER.State,'')) as [Loc Full Address] ,   TSPL_COMPANY_MASTER.Comp_Name AS [Comp Name] " & _
                " from TSPL_ITEM_REORDER_LEVEL_NEW" & _
                " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_REORDER_LEVEL_NEW.item_Code left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code left outer join tspl_item_master_category on TSPL_ITEM_MASTER.Item_Code = tspl_item_master_category.Item_Code left outer join TSPL_ITEM_CATEGORY_LEVEL on " & _
                " tspl_item_master_category.item_category_code = TSPL_ITEM_CATEGORY_LEVEL.item_category_code left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on tspl_item_master_category.item_category_code = TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code and TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  = TSPL_ITEM_CATEGORY_LEVEL_VALUES.Code " & _
                " LEFT JOIN TSPL_LOCATION_MASTER  ON TSPL_LOCATION_MASTER.Location_Code = TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code " & _
                 " LEFT JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code = TSPL_ITEM_REORDER_LEVEL_NEW.comp_code " & _
                " where 2 = 2 and TSPL_ITEM_UOM_DETAIL.Stocking_Unit='Y' "

            If clsCommon.myLen(strlocationCode) > 0 AndAlso clsCommon.myLen(strmultilocation) <= 0 Then
                qry += " and TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code in('" + strlocationCode + "')"
            ElseIf clsCommon.myLen(strlocationCode) <= 0 AndAlso clsCommon.myLen(strmultilocation) > 0 Then
                qry += " and TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code in('" + strmultilocation + "')"
            End If
            If clsCommon.myLen(itemtype) > 0 Then
                qry += " and TSPL_ITEM_MASTER.Item_Type in ('" + itemtype + "') "
            End If
            If clsCommon.myLen(Itemcode) > 0 Then
                qry += " and TSPL_ITEM_MASTER.Item_Code in ('" + Itemcode + "') "
            End If
            If CategoryType.Count > 0 Then
                qry += " and TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE IN ('" + clsCommon.GetMulcallStringWithComma(CategoryType).Replace(",", "','") + "')"
            End If
            If CategoryValues.Count > 0 Then
                qry += " and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE IN ('" + clsCommon.GetMulcallStringWithComma(CategoryValues).Replace(",", "','") + "')"
            End If
            If clsCommon.myLen(strmultilocation) <= 0 Then
                qry += " union "
                qry += " select 0 as Apply, TSPL_ITEM_MASTER.Item_Code, (TSPL_ITEM_MASTER.Item_Desc) AS Item_Desc, (TSPL_ITEM_MASTER.CSA_TYPE) as Item_Group, (TSPL_ITEM_MASTER.Item_Type)as 'Item Type', case when isnull((TSPL_ITEM_REORDER_LEVEL_NEW.UOM_Code), '') = '' then (TSPL_ITEM_UOM_DETAIL.Uom_Code) else (TSPL_ITEM_REORDER_LEVEL_NEW.UOM_Code) End AS Unit_Code, 0 AS Min_Level , 0 AS Min_Level_Tollerence , 0 AS Max_Level, 0 AS Max_Level_Tollerence, 0 AS Reorder_Level, 0 AS Reorder_Level_Tollerence, 0 AS Reorder_Qty, '' AS Location_Code, (TSPL_ITEM_CATEGORY_LEVEL.Description) as catcode, (TSPL_ITEM_CATEGORY_LEVEL_VALUES.Description)as catvalue , " & _
                        " TSPL_LOCATION_MASTER.Location_Desc as [Loc Desc],  concat(coalesce(TSPL_LOCATION_MASTER.Add1,''),',',coalesce(TSPL_LOCATION_MASTER.add2,''),',',coalesce(TSPL_LOCATION_MASTER.Add3,''),',',coalesce(TSPL_LOCATION_MASTER.city_code,''),',',coalesce(TSPL_LOCATION_MASTER.State,'')) as [Loc Full Address], " & _
                        " TSPL_COMPANY_MASTER.Comp_Name AS [Comp Name] " & _
                        " from TSPL_ITEM_REORDER_LEVEL_NEW left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_REORDER_LEVEL_NEW.item_Code left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code left outer join tspl_item_master_category on TSPL_ITEM_MASTER.Item_Code = tspl_item_master_category.Item_Code left outer join TSPL_ITEM_CATEGORY_LEVEL on tspl_item_master_category.item_category_code = TSPL_ITEM_CATEGORY_LEVEL.item_category_code left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on tspl_item_master_category.item_category_code = TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code and TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values = TSPL_ITEM_CATEGORY_LEVEL_VALUES.Code " & _
                        " LEFT JOIN TSPL_LOCATION_MASTER  ON TSPL_LOCATION_MASTER.Location_Code = TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code " & _
                         "  LEFT JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code = TSPL_ITEM_REORDER_LEVEL_NEW.comp_code  " & _
                         " where 2 = 2 and TSPL_ITEM_UOM_DETAIL.Stocking_Unit='Y'"
                If clsCommon.myLen(itemtype) > 0 Then
                    qry += " and TSPL_ITEM_MASTER.Item_Type in ('" + itemtype + "') "
                End If
                If clsCommon.myLen(Itemcode) > 0 Then
                    qry += " and TSPL_ITEM_MASTER.Item_Code in ('" + Itemcode + "') "
                End If
                If CategoryType.Count > 0 Then
                    qry += " and TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE IN('" + clsCommon.GetMulcallStringWithComma(CategoryType).Replace(",", "','") + "')"
                End If
                If CategoryValues.Count > 0 Then
                    qry += " and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE IN('" + clsCommon.GetMulcallStringWithComma(CategoryValues).Replace(",", "','") + "')"
                End If
            End If

            qry += " ) as final " & _
        "   LEFT JOIN (SELECT " & _
          "  Punching_Date," & _
          "  InOut," & _
          "   Location_Code," & _
          "  IMV.Item_Code," & _
          "  IM.item_type," & _
          "   UOM," & _
          "  MRP," & _
          " ISNULL(Stock_UOM, '') Stock_UOM," & _
          " ISNULL(Stock_Qty, 0) Stock_Qty" & _
          "  FROM TSPL_INVENTORY_MOVEMENT IMV" & _
          "  LEFT JOIN TSPL_ITEM_MASTER IM" & _
          "   ON IMV.Item_Code = IM.Item_Code" & _
          "  UNION ALL" & _
          "  SELECT" & _
          "  Punching_Date," & _
          "   InOut," & _
          "  Location_Code," & _
          "  IMVN.Item_Code," & _
          "   IM.item_type," & _
          "   UOM," & _
          "   MRP," & _
          "  ISNULL(Stock_UOM, '') Stock_UOM," & _
          " ISNULL(Stock_Qty, 0) Stock_Qty" & _
          " FROM TSPL_INVENTORY_MOVEMENT_NEW IMVN" & _
          " LEFT JOIN TSPL_ITEM_MASTER IM" & _
          " ON IMVN.Item_Code = IM.Item_Code" & _
          " WHERE 1=1 "
            If clsCommon.myLen(strlocationCode) > 0 AndAlso clsCommon.myLen(strmultilocation) <= 0 Then
                qry += "  AND Location_Code IN ('" + strlocationCode + "') "
            End If

            If clsCommon.myLen(itemtype) > 0 Then
                qry += "  AND Item_Type IN ('" + itemtype + "')"
            End If

            qry += " ) AS subQryClosingQty ON subQryClosingQty.Item_Code = final.Item_Code" & _
          "     AND subQryClosingQty.Item_Type = final.[Item Type]" & _
          "     AND subQryClosingQty.UOM = final.Unit_Code" & _
          "     AND subQryClosingQty.location_Code = final.location_Code"
            If IsApply <> 2 Then
                qry += " where final.Apply='" + clsCommon.myCstr(IsApply) + "'"
            End If
            qry += ") as fin2 pivot( max(fin2.catvalue) for fin2.catcode in (" + strpivot + ")) as an group by [Item Code] ,  [Category Code]  "
            If clsCommon.myLen(strmultilocation) > 0 Then
                qry += " ,[Location Code]"
            End If

            qry += " ) AS fnlResult "
            Return clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class


