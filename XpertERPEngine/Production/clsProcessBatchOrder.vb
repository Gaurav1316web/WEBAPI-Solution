'Monika--------------------------------BM00000003196---------------29/07/2014
Imports common
Imports System.Data.SqlClient
Public Class clsProcessBatchOrder

#Region "Variables"
    Public ArrMainItem As List(Of clsProcessBatchOrderMainDetail) = Nothing
    Public ArrRawItem As List(Of clsProcessBatchOrderRawDetail) = Nothing

    Public closeyn As String = Nothing
    Public Batchcode As String = Nothing
    Public Batchdate As Date = Nothing
    Public batchdesc As String = Nothing
    Public status As String = Nothing
    Public IsPost As String = Nothing
    Public locationcode As String = Nothing
    Public locationname As String = Nothing
    Public itemcatcode As String = Nothing
    Public itemcatname As String = Nothing
    Public Plancode As String = Nothing
    Public Main_batchcode As String = Nothing
    Public Sub_batchcode As String = Nothing
    Public SubBatchAfterPost As String = Nothing
    Public Section_Code As String = Nothing
    Public Section_Name As String = Nothing
    Public ManualBatchNo As String = Nothing
    Public LINE_NO As String = String.Empty
    Public CostCenterCode As String = String.Empty
    Public ProfitCenterCode As String = String.Empty
    Public CostCenterName As String = String.Empty
    Public ProfitCenterName As String = String.Empty
    Public Is_Job_Work_Inward As Boolean = False
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "select TSPL_PP_BATCH_ORDER_HEAD.batch_code as Code,TSPL_PP_BATCH_ORDER_HEAD.batch_date as [Date],TSPL_PP_BATCH_ORDER_HEAD.Description, " _
        & " TSPL_PP_BATCH_ORDER_HEAD.Status,(case when TSPL_PP_BATCH_ORDER_HEAD.is_post='1' then 'Posted' else 'UnPosted' end) as [Post Status], " _
        & " TSPL_PP_BATCH_ORDER_HEAD.location_code as [Location Code],tspl_location_master.Location_Desc as [Location], " _
        & " TSPL_PP_BATCH_ORDER_HEAD.structure_code as [Production Structure],tspl_structure_master.structure_descq as [Structure], " _
        & " TSPL_PP_BATCH_ORDER_HEAD.plan_code as [Plan Code],(case when len(isnull(TSPL_PP_BATCH_ORDER_HEAD.main_batch_code,''))>0 then 'Child BO' else 'Main BO' end) as [BO Type],TSPL_PP_BATCH_ORDER_HEAD.main_batch_code as [Main Batch Code], " _
        & " TSPL_PP_BATCH_ORDER_HEAD.sub_batch_code as [Sub Batch Code],[Main Item Code],[Item Description],Quantity,[Product Type],TSPL_PP_BATCH_ORDER_HEAD.ManualBatchNo as [Manual Batch No],TSPL_PP_BATCH_ORDER_HEAD.Line_No as [Line No],TSPL_PP_BATCH_ORDER_HEAD.CostCenterCode as [Cost Center Code] , TSPL_CostCenter_MASTER.Cost_name as [Cost Center Name], TSPL_PP_BATCH_ORDER_HEAD.ProfitCenterCode as [Profit Center Code]  ,TSPL_PROFIT_CENTER_MASTER.Name as [Profit Center Name] from TSPL_PP_BATCH_ORDER_HEAD " _
        & " left outer join tspl_location_master on TSPL_PP_BATCH_ORDER_HEAD.location_code=tspl_location_master.location_code " _
        & " left outer join tspl_structure_master on TSPL_PP_BATCH_ORDER_HEAD.structure_code=tspl_structure_master.structure_code " _
        & " left join (select * from (  " _
        & " select ROW_NUMBER()  over(partition by Batch_Code order by Product_Type desc, TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code) as S_no ,Batch_Code as Batch_Code_Main, " _
        & " TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code as [Main Item Code],Item_Desc as [Item Description],Product_Type as [Product Type],Quantity " _
        & " from TSPL_PP_BATCH_ORDER_BOM_DETAIL left join TSPL_ITEM_MASTER on TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " _
        & " ) as M_Inner where S_no=1 ) as Main on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code=Main.Batch_Code_Main" _
        & " left outer join TSPL_PROFIT_CENTER_MASTER on TSPL_PROFIT_CENTER_MASTER.Code =TSPL_PP_BATCH_ORDER_HEAD.ProfitCenterCode " _
        & " left outer join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.Cost_Code =TSPL_PP_BATCH_ORDER_HEAD.CostCenterCode "
        Dim str As String = ""

        str = clsCommon.ShowSelectForm("BTCHFND", qry, "Code", whrCls, strCurrCode, "Code", isButtonClicked, "TSPL_PP_BATCH_ORDER_HEAD.batch_date")

        Return str
    End Function
    Public Shared Function GetFinder_PendingBatchQuantity(ByVal whrCls As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean, ByVal currentStageProcessCode As String) As String
        Dim qry As String = " select TSPL_PP_BATCH_ORDER_HEAD.batch_code as Code,TSPL_PP_BATCH_ORDER_HEAD.batch_date as [Date], " & _
            " TSPL_PP_BATCH_ORDER_HEAD.Description,  TSPL_PP_BATCH_ORDER_HEAD.Status,(case when TSPL_PP_BATCH_ORDER_HEAD.is_post='1' then 'Posted' else 'UnPosted' end) as [Post Status], " & _
            " TSPL_PP_BATCH_ORDER_HEAD.location_code as [Location Code],tspl_location_master.Location_Desc as [Location],  TSPL_PP_BATCH_ORDER_HEAD.structure_code as [Production Structure], " & _
            " tspl_structure_master.structure_descq as [Structure],  TSPL_PP_BATCH_ORDER_HEAD.plan_code as [Plan Code], " & _
            " (case when len(isnull(TSPL_PP_BATCH_ORDER_HEAD.main_batch_code,''))>0 then 'Child BO' else 'Main BO' end) as [BO Type], " & _
            " TSPL_PP_BATCH_ORDER_HEAD.main_batch_code as [Main Batch Code],  TSPL_PP_BATCH_ORDER_HEAD.sub_batch_code as [Sub Batch Code],[Main Item Code],[Item Description]," & _
            " coalesce(Prod.Unit_Code,Main.Unit_Code) as Unit_Code,coalesce(prod.Quantity,Main.Quantity) as Quantity, " & _
            " prod.Produced_Qty as [Production Quantity],[Product Type] from TSPL_PP_BATCH_ORDER_HEAD  " & _
            " left outer join tspl_location_master on TSPL_PP_BATCH_ORDER_HEAD.location_code=tspl_location_master.location_code  " & _
            " left outer join tspl_structure_master on TSPL_PP_BATCH_ORDER_HEAD.structure_code=tspl_structure_master.structure_code " & _
            " left join (select * from (   select ROW_NUMBER()  over(partition by Batch_Code order by Product_Type desc, TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code) as S_no, " & _
            " Batch_Code as Batch_Code_Main,  TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code as [Main Item Code],Item_Desc as [Item Description],Product_Type as [Product Type], " & _
            " TSPL_PP_BATCH_ORDER_BOM_DETAIL.Unit_Code, Quantity from TSPL_PP_BATCH_ORDER_BOM_DETAIL " & _
            " left join TSPL_ITEM_MASTER on TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  ) as M_Inner where S_no=1 ) as Main on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code=Main.Batch_Code_Main" & _
            " left join (select * from ( " & _
            " select Batch_Code,UNIT_CODE,Quantity,Produced_Qty from (select ROW_NUMBER()  over(partition by Child_Batch_Code order by Product_Type desc, STD_Dtl.Item_Code) as S_no,STD.Child_Batch_Code as Batch_Code, " & _
            " STD_Dtl.Item_Code,STD_Dtl.Unit_Code,sum(STD_Dtl.Quantity) as Quantity,sum(STD_Dtl.Produced_Qty) as Produced_Qty " & _
            " from TSPL_PP_STANDARDIZATION_HEAD STD left join TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL STD_Dtl on STD.Standardization_Code=STD_Dtl.Standardization_Code " & _
            " left join TSPL_ITEM_MASTER Item on STD_Dtl.Item_Code= Item.Item_Code " & _
            " group by STD.Child_Batch_Code,STD_Dtl.Item_Code,STD_Dtl.Unit_Code,Item.Product_Type " & _
            " ) as Prod_STD where S_no=1 " & _
            " union all " & _
            " select  Batch_Code,UNIT_CODE,sum(Quantity) as Quantity,sum(Produced_Qty) as Produced_Qty from ( " & _
            " select ROW_NUMBER()  over(partition by Batch_Code order by Product_Type desc, PE_Dtl.Item_Code) as S_no,PE.Batch_Code, " & _
            " PE_Dtl.Item_Code,PE_Dtl.Unit_Code,sum(PE_Dtl.BATCH_QTY) as Quantity,sum(PE_Dtl.FINAL_PRODUCTION_QTY) as Produced_Qty " & _
            " from TSPL_PP_PRODUCTION_ENTRY PE left join TSPL_PP_PRODUCTION_ENTRY_DETAIL PE_Dtl on PE.PROD_ENTRY_CODE=PE_Dtl.PROD_ENTRY_CODE " & _
            " left join TSPL_ITEM_MASTER Item on PE_Dtl.Item_Code= Item.Item_Code " & _
            " group by PE.Batch_Code,PE_Dtl.Item_Code,PE_Dtl.Unit_Code,Item.Product_Type" & _
            " ) as Prod_Main  where Batch_Code not in (select Main_Batch_Code from TSPL_PP_STAGE_PROCESS_HEAD where STAGE_PROCESS_CODE='" + currentStageProcessCode + "')  group by Batch_Code,UNIT_CODE " & _
            " ) as Final_Prod) as Prod " & _
            " on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code=Prod.Batch_Code  left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code=Main.[Main Item Code] "
        '" ) as Final_Table where Quantity>[Production Quantity]"
        Dim str As String = ""

        str = clsCommon.ShowSelectForm("BTCHFND", qry, "Code", whrCls, strCurrCode, "Code", isButtonClicked)

        Return str
    End Function

    Public Shared Function UnpostData(ByVal strcode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim issaved As Boolean = True
            issaved = issaved AndAlso UnpostData(strcode, trans)

            trans.Commit()
            Return issaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function UnpostData(ByVal strcode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If clsCommon.myLen(strcode) <= 0 Then
                Throw New Exception("Document No Not found for unpost.")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Batch_Date , Location_Code from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + strcode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmBatchOrderDairy, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Batch_Date")), trans)

            End If
            Dim qry As String = "select count(*) from TSPL_PP_BATCH_ORDER_HEAD where Is_Post='0' and batch_code='" + strcode + "' and isnull(main_batch_code,'')=''"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            If check > 0 Then
                Throw New Exception("Current document [" + strcode + "] is not posted.")
            End If
            qry = "select count(*) from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + strcode + "' and isnull(main_batch_code,'')=''"
            check = clsDBFuncationality.getSingleValue(qry, trans)
            If check <= 0 Then
                Throw New Exception("Current document [" + strcode + "] is not main batch order.")
            End If
            Dim Main_BO As String
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProdcutionDoNotCheckForwardDocuments, clsFixedParameterCode.ProdcutionDoNotCheckForwardDocuments, trans)) <= 0 Then
                qry = "select count(*) from TSPL_PP_ISSUE_HEAD where batch_code='" + strcode + "'"
                check = clsDBFuncationality.getSingleValue(qry, trans)

                Main_BO = clsProcessBatchOrder.GetMainBO(strcode, trans)
                If clsCommon.myLen(Main_BO) > 0 Then
                    While (clsCommon.myLen(Main_BO) > 0)
                        qry = "select count(*) from TSPL_PP_ISSUE_HEAD where Batch_Code in ('" + Main_BO + "')"
                        check += clsDBFuncationality.getSingleValue(qry, trans)
                        Main_BO = clsProcessBatchOrder.GetMainBO(Main_BO, trans)
                    End While
                End If

                If check > 0 Then
                    Throw New Exception("Cannot unpost document [" + strcode + "].It is used in Production Issue Entry.")
                End If
            End If


            qry = "update TSPL_PP_BATCH_ORDER_HEAD set Is_Post='0',Modified_By='" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "',Modified_Date='" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")) + "' where batch_code='" + strcode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Main_BO = GetMainBO(strcode, trans)
            While (clsCommon.myLen(Main_BO) > 0)
                qry = "update TSPL_PP_BATCH_ORDER_HEAD set Is_Post='0',Modified_By='" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "',Modified_Date='" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")) + "' where Batch_Code in ('" + Main_BO + "')"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Main_BO = GetMainBO(Main_BO, trans)
            End While

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strcode), "TSPL_PP_BATCH_ORDER_HEAD", "batch_code", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetDescription(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select Description from TSPL_PP_BATCH_ORDER_HEAD where batch_code = '" + strCode + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetDate(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Date
        Dim qry As String = "select Batch_Date from TSPL_PP_BATCH_ORDER_HEAD where batch_code = '" + strCode + "'"
        Return clsCommon.myCDate(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function SaveData(ByVal obj As clsProcessBatchOrder, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsProcessBatchOrder, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            If isNewEntry Then 'clsCommon.myLen(obj.Batchcode) <= 0
                obj.Batchcode = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.Batchdate, "dd/MMM/yyyy"), clsDocType.BATCHORDER, "", obj.locationcode)
            End If

            'Dim qry As String = "select count(*) from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + obj.Batchcode + "'"
            'Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmBatchOrderDairy, obj.locationcode, obj.Batchdate, trans)
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Batch_Code", obj.Batchcode)
            clsCommon.AddColumnsForChange(coll, "Batch_Date", clsCommon.GetPrintDate(obj.Batchdate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.batchdesc)
            clsCommon.AddColumnsForChange(coll, "Status", obj.status)
            clsCommon.AddColumnsForChange(coll, "Is_Post", obj.IsPost)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.locationcode)
            clsCommon.AddColumnsForChange(coll, "Structure_Code", obj.itemcatcode)
            clsCommon.AddColumnsForChange(coll, "Plan_Code", obj.Plancode)
            clsCommon.AddColumnsForChange(coll, "Section_Code", obj.Section_Code, True)
            clsCommon.AddColumnsForChange(coll, "Main_Batch_Code", obj.Main_batchcode)
            clsCommon.AddColumnsForChange(coll, "Sub_Batch_Code", obj.Sub_batchcode)
            clsCommon.AddColumnsForChange(coll, "Closed_YN", obj.closeyn)
            ''richa agarwal BHA/02/07/18-000121 7 july,2018 
            clsCommon.AddColumnsForChange(coll, "ManualBatchNo", obj.ManualBatchNo)
            ''richa agarwal againt ticket no BHA/02/07/18-000120
            clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO, True)
            clsCommon.AddColumnsForChange(coll, "CostCenterCode", obj.CostCenterCode, True)
            clsCommon.AddColumnsForChange(coll, "ProfitCenterCode", obj.ProfitCenterCode, True)
            ''------------------
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")))
            clsCommon.AddColumnsForChange(coll, "Is_Job_Work_Inward", IIf(obj.Is_Job_Work_Inward, 1, 0))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_BATCH_ORDER_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                HistoryUpdate(obj.Batchcode, trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_BATCH_ORDER_HEAD", OMInsertOrUpdate.Update, " TSPL_PP_BATCH_ORDER_HEAD.batch_code='" + obj.Batchcode + "'", trans)
            End If

            isSaved = isSaved AndAlso clsProcessBatchOrderMainDetail.SaveMainItem(obj.ArrMainItem, obj.Batchcode, trans)
            isSaved = isSaved AndAlso clsProcessBatchOrderRawDetail.SaveRawItem(obj.ArrRawItem, obj.Batchcode, trans)


            'Dim qry As String = "delete from TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL where Batch_Code in (select batch_code from TSPL_PP_BATCH_ORDER_HEAD where main_Batch_Code='" + obj.Batchcode + "')"
            'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "delete from TSPL_PP_BATCH_ORDER_BOM_DETAIL where Batch_Code in (select batch_code from TSPL_PP_BATCH_ORDER_HEAD where main_Batch_Code='" + obj.Batchcode + "')"
            'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "delete from TSPL_PP_BATCH_ORDER_HEAD where main_batch_code='" + obj.Batchcode + "'"
            'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            EffectOnHierarchy(obj.Batchcode, trans, False, False, True)


            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrloc As String, ByVal NavType As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsProcessBatchOrder
        Try
            Dim obj As New clsProcessBatchOrder()

            Dim qry As String = "select TSPL_PP_BATCH_ORDER_HEAD.*,TSPL_SECTION_MASTER.description as section_name, TSPL_PP_BATCH_ORDER_HEAD.LINE_NO,TSPL_PP_BATCH_ORDER_HEAD.CostCenterCode , TSPL_CostCenter_MASTER.Cost_name as [Cost_Center_Name], TSPL_PP_BATCH_ORDER_HEAD.ProfitCenterCode  ,TSPL_PROFIT_CENTER_MASTER.Name as ProfitCenterName  from TSPL_PP_BATCH_ORDER_HEAD left outer join TSPL_PP_PRODUCTION_PLAN_HEAD on TSPL_PP_PRODUCTION_PLAN_HEAD.plan_code=TSPL_PP_BATCH_ORDER_HEAD.plan_code left outer join TSPL_SECTION_MASTER on TSPL_SECTION_MASTER.section_code=TSPL_PP_BATCH_ORDER_HEAD.section_code " & _
                        " left outer join TSPL_PROFIT_CENTER_MASTER on TSPL_PROFIT_CENTER_MASTER.Code =TSPL_PP_BATCH_ORDER_HEAD.ProfitCenterCode " & _
            " left outer join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.Cost_Code =TSPL_PP_BATCH_ORDER_HEAD.CostCenterCode where  2=2 "

            Dim whr As String = ""
            If clsCommon.myLen(arrloc) > 0 Then
                whr += " and TSPL_PP_BATCH_ORDER_HEAD.location_code in (" + arrloc + ") "
            End If

            qry += whr

            Select Case NavType
                Case NavigatorType.Current
                    qry += " and TSPL_PP_BATCH_ORDER_HEAD.batch_code='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and TSPL_PP_BATCH_ORDER_HEAD.batch_code in (select min(batch_code) from TSPL_PP_BATCH_ORDER_HEAD where 2=2 " + whr + ")"
                Case NavigatorType.Last
                    qry += " and TSPL_PP_BATCH_ORDER_HEAD.batch_code in (select max(batch_code) from TSPL_PP_BATCH_ORDER_HEAD where 2=2 " + whr + ")"
                Case NavigatorType.Next
                    qry += " and TSPL_PP_BATCH_ORDER_HEAD.batch_code in (select min(batch_code) from TSPL_PP_BATCH_ORDER_HEAD where 2=2 " + whr + " and batch_code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and TSPL_PP_BATCH_ORDER_HEAD.batch_code in (select max(batch_code) from TSPL_PP_BATCH_ORDER_HEAD where 2=2 " + whr + " and batch_code<'" + strCode + "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Batchcode = clsCommon.myCstr(dt.Rows(0)("Batch_Code"))
                obj.Batchdate = clsCommon.myCDate(dt.Rows(0)("Batch_Date"))
                obj.batchdesc = clsCommon.myCstr(dt.Rows(0)("Description"))
                obj.status = clsCommon.myCstr(dt.Rows(0)("Status"))
                obj.IsPost = clsCommon.myCstr(dt.Rows(0)("Is_Post"))
                obj.locationcode = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                obj.locationname = clsLocation.GetName(obj.locationcode, trans)
                obj.itemcatcode = clsCommon.myCstr(dt.Rows(0)("Structure_Code"))
                obj.itemcatname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select structure_descq from tspl_structure_master where structure_code='" + obj.itemcatcode + "'", trans))
                obj.Plancode = clsCommon.myCstr(dt.Rows(0)("Plan_Code"))
                obj.Section_Code = clsCommon.myCstr(dt.Rows(0)("section_code"))
                obj.Section_Name = clsCommon.myCstr(dt.Rows(0)("section_name"))
                obj.Main_batchcode = clsCommon.myCstr(dt.Rows(0)("Main_Batch_Code"))
                obj.Sub_batchcode = clsCommon.myCstr(dt.Rows(0)("Sub_Batch_Code"))
                obj.closeyn = clsCommon.myCstr(dt.Rows(0)("Closed_YN"))
                ''richa agarwal BHA/02/07/18-000121 7 july,2018 
                obj.ManualBatchNo = clsCommon.myCstr(dt.Rows(0)("ManualBatchNo"))
                ''richa agarwal againt ticket no BHA/02/07/18-000120
                obj.LINE_NO = clsCommon.myCstr(dt.Rows(0)("LINE_NO"))
                obj.CostCenterCode = clsCommon.myCstr(dt.Rows(0)("CostCenterCode"))
                obj.CostCenterName = clsCommon.myCstr(dt.Rows(0)("Cost_Center_Name"))
                obj.ProfitCenterCode = clsCommon.myCstr(dt.Rows(0)("ProfitCenterCode"))
                obj.ProfitCenterName = clsCommon.myCstr(dt.Rows(0)("ProfitCenterName"))
                ''--------------------------
                obj.Is_Job_Work_Inward = (clsCommon.myCdbl(dt.Rows(0)("Is_Job_Work_Inward")) = 1)

                obj.ArrMainItem = New List(Of clsProcessBatchOrderMainDetail)
                obj.ArrRawItem = New List(Of clsProcessBatchOrderRawDetail)

                '-------------------------main item---------------------------
                qry = "select TSPL_PP_BATCH_ORDER_BOM_DETAIL.*,tspl_pp_bom_head.description as bom_desc from TSPL_PP_BATCH_ORDER_BOM_DETAIL left outer join tspl_pp_bom_head on tspl_pp_bom_head.bom_code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.bom_code where TSPL_PP_BATCH_ORDER_BOM_DETAIL.batch_code='" + obj.Batchcode + "' order by TSPL_PP_BATCH_ORDER_BOM_DETAIL.sno"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                Dim objtr As New clsProcessBatchOrderMainDetail()

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        objtr = New clsProcessBatchOrderMainDetail()

                        objtr.SNO = CInt(dr("SNO"))
                        objtr.Plan_Code = clsCommon.myCstr(dr("plan_code"))
                        objtr.bomcode = clsCommon.myCstr(dr("BOM_Code"))
                        objtr.BOM_Revision_No = clsCommon.myCstr(dr("BOM_Revision_No"))
                        objtr.BomDesc = clsCommon.myCstr(dr("bom_desc"))
                        objtr.icode = clsCommon.myCstr(dr("Item_Code"))
                        objtr.iname = clsItemMaster.GetItemName(objtr.icode, trans)
                        objtr.itype = clsItemMaster.GetItemType(objtr.icode, trans)
                        objtr.UOM = clsCommon.myCstr(dr("Unit_Code"))
                        objtr.qty = clsCommon.myCdbl(dr("Quantity"))
                        objtr.shiftcode = clsCommon.myCstr(dr("Shift_Code"))
                        objtr.shiftname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select shift_name from tspl_shift_master where shift_code='" + objtr.shiftcode + "'", trans))
                        objtr.sectioncode = clsCommon.myCstr(dr("Section_Code"))
                        objtr.sectionname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_section_master where section_code='" + objtr.sectioncode + "'", trans))
                        objtr.bom_fat_kg = clsCommon.myCdbl(dr("FAT_KG"))
                        objtr.bom_fat_pers = clsCommon.myCdbl(dr("FAT_PERS"))
                        objtr.bom_snf_kg = clsCommon.myCdbl(dr("SNF_KG"))
                        objtr.bom_snf_pers = clsCommon.myCdbl(dr("SNF_PERS"))

                        obj.ArrMainItem.Add(objtr)
                    Next
                End If

                '-------------------------raw item-------------------------------------------
                qry = "select * from TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL where batch_code='" + obj.Batchcode + "' order by sno"
                dt1 = New DataTable()
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr1 As New clsProcessBatchOrderRawDetail()

                        objtr1.Rawsno = CInt(dr("SNO"))
                        objtr1.Rawomcode = clsCommon.myCstr(dr("BOM_Code"))
                        objtr1.Proditem = clsCommon.myCstr(dr("Prod_Item_Code"))
                        objtr1.rawicode = clsCommon.myCstr(dr("Item_Code"))
                        objtr1.rawiname = clsItemMaster.GetItemName(objtr1.rawicode, trans)
                        objtr1.rawitype = clsItemMaster.GetItemType(objtr1.rawicode, trans)
                        objtr1.rawunit = clsCommon.myCstr(dr("Unit_Code"))
                        objtr1.producttype = clsItemMaster.GetItemProductType(objtr1.rawicode, trans)
                        objtr1.rawqty = clsCommon.myCdbl(dr("Quantity"))
                        objtr1.fat = clsCommon.myCdbl(dr("FAT"))
                        objtr1.snf = clsCommon.myCdbl(dr("SNF"))
                        If dr("fat_kg") Is DBNull.Value Then
                            objtr1.fat_kg = 0
                        Else
                            objtr1.fat_kg = clsCommon.myCdbl(dr("fat_kg"))
                        End If
                        If dr("snf_kg") Is DBNull.Value Then
                            objtr1.snf_kg = 0
                        Else
                            objtr1.snf_kg = clsCommon.myCdbl(dr("snf_kg"))
                        End If

                        objtr1.rawbatchcode = clsCommon.myCstr(dr("SFG_Batch_Code"))
                        objtr1.remarks = clsCommon.myCstr(dr("Remarks"))

                        obj.ArrRawItem.Add(objtr1)
                    Next
                End If

            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PP_BATCH_ORDER_HEAD", "batch_code", "TSPL_PP_BATCH_ORDER_BOM_DETAIL", "batch_code", "TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL", "batch_code", trans)
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim issaved As Boolean = True
            issaved = issaved AndAlso DeleteData(strCode, trans)

            trans.Commit()
            Return issaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Batch_Date , Location_Code from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmBatchOrderDairy, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Batch_Date")), trans)

            End If
            Dim qry As String = "select * from TSPL_PP_ISSUE_HEAD where Batch_Code='" + strCode + "'"
            Dim strFutureDoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(strFutureDoc) > 0 Then
                Throw New Exception("Production Issue No [" + strFutureDoc + "] is created.Can't Delete It")
            End If


            HistoryUpdate(strCode, trans)
            qry = "delete from TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL where batch_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_BATCH_ORDER_BOM_DETAIL where batch_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            EffectOnHierarchy(strCode, trans, False, False, True)
            clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CHILDBO_REF_NO where Main_BO_Code='" + strCode + "'", trans)

            qry = "update TSPL_PP_BATCH_ORDER_HEAD_Delete_Data set Delete_By = '" + objCommonVar.CurrentUserCode + "' where batch_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function CancelData(ByVal Form_Id As String, ByVal Doc_No As String) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            Dim obj As clsProcessBatchOrder = clsProcessBatchOrder.GetData(Doc_No, "", NavigatorType.Current, trans)
            If obj Is Nothing OrElse clsCommon.myLen(obj.Batchcode) <= 0 Then
                Throw New Exception("Document- " & Doc_No & " not found")
            End If

            qry = "select Issue_Code from TSPL_PP_ISSUE_HEAD where Batch_Code='" + Doc_No + "'"
            Dim strFutureDoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(strFutureDoc) > 0 Then
                Throw New Exception("Production Issue No [" + strFutureDoc + "] is created.Can't Cancel It")
            End If
            ''''''''''
            qry = "select count(*) from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + Doc_No + "' and isnull(main_batch_code,'')=''"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            If check <= 0 Then
                Throw New Exception("Current document [" + Doc_No + "] is not main batch order.")
            End If
            Dim Main_BO As String

            qry = "select count(*) from TSPL_PP_ISSUE_HEAD where batch_code='" + Doc_No + "'"
            check = clsDBFuncationality.getSingleValue(qry, trans)

            Main_BO = clsProcessBatchOrder.GetMainBO(Doc_No, trans)
            If clsCommon.myLen(Main_BO) > 0 Then
                While (clsCommon.myLen(Main_BO) > 0)
                    qry = "select count(*) from TSPL_PP_ISSUE_HEAD where Batch_Code in ('" + Main_BO + "')"
                    check += clsDBFuncationality.getSingleValue(qry, trans)
                    Main_BO = clsProcessBatchOrder.GetMainBO(Main_BO, trans)
                End While
            End If

            If check > 0 Then
                Throw New Exception("Cannot cancel document [" + Doc_No + "].It is used in Production Issue Entry.")
            End If


            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL", "batch_code", trans)
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PP_BATCH_ORDER_BOM_DETAIL", "batch_code", trans)
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PP_BATCH_ORDER_HEAD", "batch_code", trans)
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TEMP_CHILDBO_REF_NO", "Main_BO_Code", trans)

            '' cancel custom field data
            clsCommonFunctionality.SaveCancelDataMultKey(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CUSTOM_FIELD_VALUES", "Transaction_Code", "Program_Code", Form_Id, trans)

            qry = "delete from TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL where batch_code='" + Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_BATCH_ORDER_BOM_DETAIL where batch_code='" + Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CHILDBO_REF_NO where Main_BO_Code='" + Doc_No + "'", trans)

            trans.Commit()
            '' release objects 
            obj = Nothing
            qry = Nothing

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim issaved As Boolean = True
            issaved = issaved AndAlso PostData(strCode, trans)

            trans.Commit()
            Return issaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function PostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Batch_Date , Location_Code from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmBatchOrderDairy, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Batch_Date")), trans)

            End If
            Dim qry As String = "update TSPL_PP_BATCH_ORDER_HEAD set Status='Approved',Is_Post='1',Modified_By='" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "',Modified_Date='" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")) + "' where batch_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PP_BATCH_ORDER_HEAD", "batch_code", trans)
            EffectOnHierarchy(strCode, trans, False, True, False)
            '== Notification regarding
            Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmBatchOrderDairy + "'", trans))
            If clsCommon.CompairString(strNotificationOn, "P") = CompairStringResult.Equal Then
                CreateNotificationContentEMP(strCode, trans)
            End If
            '== Complete
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Private Shared Function CreateNotificationContentEMP(ByVal StrDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim strNotifiContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmBatchOrderDairy + "'", trans))
        Dim strNotifi_DetalContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Detail_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmBatchOrderDairy + "'", trans))
        Dim strNotifiCaption As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Caption from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmBatchOrderDairy + "'", trans))
        Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmBatchOrderDairy + "'", trans))
        Dim strDocumentDate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Plan_Date from TSPL_PP_PRODUCTION_PLAN_HEAD where plan_code='" + StrDocNo + "'", trans))

        If clsCommon.myLen(strNotifiContent) > 0 Then
            Dim objNotification As New clsNotificationHead()
            objNotification.Notification_Text = strNotifiContent
            objNotification.Notification_Caption = strNotifiCaption
            objNotification.Notification_On = strNotificationOn
            objNotification.Notification_Detail_Text = strNotifi_DetalContent
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(StrDocNo))
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, (clsCommon.myCDate(strDocumentDate)))
            objNotification.SaveData(clsUserMgtCode.frmBatchOrderDairy, objNotification, trans)
            objNotification = Nothing
            Return True
        End If
        Return False
    End Function
    Public Shared Function CloseData(ByVal strCode As String, ByVal is_Closed As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim issaved As Boolean = True
            issaved = issaved AndAlso CloseData(strCode, is_Closed, trans)

            trans.Commit()
            Return issaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function CloseData(ByVal strCode As String, ByVal is_Closed As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            If is_Closed Then
                qry = "update TSPL_PP_BATCH_ORDER_HEAD set Closed_yn='1',Modified_By='" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "',Modified_Date='" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")) + "' where batch_code='" + strCode + "'"
            Else
                qry = "update TSPL_PP_BATCH_ORDER_HEAD set Closed_yn='0',Modified_By='" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "',Modified_Date='" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")) + "' where batch_code='" + strCode + "'"
            End If
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            EffectOnHierarchy(strCode, trans, is_Closed, False, False)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetBalance(ByVal Icode As String, ByVal Producttype As String, ByVal UOM As String, ByVal LocationCode As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As Date, ByVal trans As SqlTransaction) As Decimal
        Dim qry As String = ""
        Dim qty As Decimal = clsCommon.myCdbl(clsProcessProductionPlanning.GetMilkAndALLItemStockBalance(Icode, LocationCode, LocationCode, strDocumentNo, dtDocumentDate, trans, UOM))
        'If clsCommon.CompairString(Producttype, "Milk") <> CompairStringResult.Equal Then
        '    qry = "select sum(qty) as qty from (select (qty*Conversion_Factor/finalcnvrt) as qty from (select TSPL_INVENTORY_MOVEMENT.Item_Code,Location_Code,UOM,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,finalcnvrsn.Conversion_Factor as finalcnvrt,(case when InOut='I' then Qty else case when inout='O' then (0-Qty) end end) as qty from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_INVENTORY_MOVEMENT.UOM left outer join TSPL_ITEM_UOM_DETAIL as finalcnvrsn on finalcnvrsn.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code and finalcnvrsn.UOM_Code='" + UOM + "')a where Item_Code='" + Icode + "' and Location_Code='" + LocationCode + "')axa"
        'ElseIf clsCommon.CompairString(clsCommon.myCstr(Producttype), "Milk") = CompairStringResult.Equal Then
        '    qry = "select sum(qty) as qty from (select (qty*Conversion_Factor/finalcnvrt) as qty from (select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,Location_Code,UOM,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,finalcnvrsn.Conversion_Factor as finalcnvrt,(case when InOut='I' then Qty else case when inout='O' then (0-Qty) end end) as qty from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_INVENTORY_MOVEMENT_NEW.UOM left outer join TSPL_ITEM_UOM_DETAIL as finalcnvrsn on finalcnvrsn.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code and finalcnvrsn.UOM_Code='" + UOM + "' where TSPL_INVENTORY_MOVEMENT_NEW.Item_Code='" + Icode + "' and (Location_Code='" + LocationCode + "' or Main_Location='" + LocationCode + "'))a )axa"
        'End If
        'Dim qty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

        Return qty
    End Function

    Public Shared Function GetBOAvailQty(ByVal BO_CODE As String, ByVal Icode As String, ByVal UOM As String, ByVal LocationCode As String) As Decimal
        Dim qry As String = "select sum(axa.qty) as qty from (select sum(isnull(quantity,0)) as qty from TSPL_PP_BATCH_ORDER_BOM_DETAIL where item_code='" + Icode + "' and batch_code in (select batch_code from TSPL_PP_BATCH_ORDER_HEAD where isnull(Is_Post,'0')='0' and isnull(Closed_YN,'0')='0' and location_code='" + LocationCode + "' and batch_code <> '" + BO_CODE + "' and Main_Batch_Code <> '" + BO_CODE + "') " & _
                  " Union all " & _
        " select (case when (aa.qty>0 and aa.qty>aa.issue_qty) then aa.qty-aa.issue_qty else case when (aa.qty=0 or aa.qty=aa.issue_qty or aa.qty<aa.issue_qty) then 0 end end) as qty from ( " & _
        " select a.Item_Code,sum(a.issue_qty) as issue_qty,SUM(a.qty) as qty from ( select ax.Batch_Code,ax.Item_Code,sum(ax.issue_qty) as issue_qty,SUM(ax.qty) as qty from ( " & _
        " select TSPL_PP_ISSUE_HEAD.Batch_Code,TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code,Unit_Code,((isnull(Qty,0)) * TSPL_ITEM_UOM_DETAIL. Conversion_Factor)/finalconversion.Conversion_Factor as issue_qty,0 as qty from TSPL_PP_ISSUE_ITEM_DETAIL left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Unit_Code left outer join TSPL_ITEM_UOM_DETAIL finalconversion on finalconversion.Item_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code and finalconversion.UOM_Code='" + UOM + "' left outer join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code where TSPL_PP_ISSUE_HEAD.Is_post='1' and ISNULL(TSPL_PP_ISSUE_HEAD.batch_code,'') in (select batch_code from TSPL_PP_BATCH_ORDER_HEAD where isnull(Is_Post,'0')='1' and isnull(Closed_YN,'0')='0' and location_code='" + LocationCode + "') and TSPL_PP_ISSUE_HEAD.Main_Location_Code='" + LocationCode + "' and TSPL_PP_ISSUE_ITEM_DETAIL.item_code='" + Icode + "' " & _
        " Union all " & _
        " select TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code,TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code,Unit_Code,0 as issue_qty,(isnull(Quantity,0) * TSPL_ITEM_UOM_DETAIL. Conversion_Factor)/finalconversion.Conversion_Factor as Qty from TSPL_PP_BATCH_ORDER_BOM_DETAIL left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Unit_Code left outer join TSPL_ITEM_UOM_DETAIL finalconversion on finalconversion.Item_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code and finalconversion.UOM_Code='" + UOM + "' where Batch_Code in (select batch_code from TSPL_PP_BATCH_ORDER_HEAD where isnull(Is_Post,'0')='1' and isnull(Closed_YN,'0')='0' and location_code='" + LocationCode + "') and TSPL_PP_BATCH_ORDER_BOM_DETAIL.item_code='" + Icode + "' )ax group by ax.Item_Code,ax.Batch_Code )a group by a.Item_Code )aa )axa "
        Dim qty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

        Return qty
    End Function

    Public Shared Function EffectOnHierarchy(ByVal BO_Code As String, ByVal trans As SqlTransaction, ByVal is_Closed As Boolean, ByVal is_Post As Boolean, ByVal is_Delete As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim Main_BO As String = ""
            Dim qry As String = ""
            If is_Post = True Then
                Main_BO = GetMainBO(BO_Code, trans)
                While (clsCommon.myLen(Main_BO) > 0)
                    qry = "update TSPL_PP_BATCH_ORDER_HEAD set Status='Approved',Is_Post='1',Modified_By='" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "',Modified_Date='" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")) + "' where Batch_Code in ('" + Main_BO + "')"
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    '===========check having more child===
                    Main_BO = GetMainBO(Main_BO, trans)
                End While

            ElseIf is_Delete = True Then
                Main_BO = GetMainBO(BO_Code, trans)
                While (clsCommon.myLen(Main_BO) > 0)
                    qry = "delete from TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL where batch_code in ('" + Main_BO + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_PP_BATCH_ORDER_BOM_DETAIL where batch_code in ('" + Main_BO + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_PP_BATCH_ORDER_HEAD where batch_code in ('" + Main_BO + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    '===========check having more child===
                    Main_BO = GetMainBO(Main_BO, trans)
                End While

            ElseIf Not is_Post AndAlso Not is_Delete Then
                Main_BO = GetMainBO(BO_Code, trans)
                While (clsCommon.myLen(Main_BO) > 0)
                    If is_Closed Then
                        qry = "update TSPL_PP_BATCH_ORDER_HEAD set Closed_Yn='1',Modified_By='" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "',Modified_Date='" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")) + "' where Batch_Code in ('" + Main_BO + "')"
                    Else
                        qry = "update TSPL_PP_BATCH_ORDER_HEAD set Closed_Yn='0',Modified_By='" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "',Modified_Date='" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")) + "' where Batch_Code in ('" + Main_BO + "')"
                    End If
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    '===========check having more child===
                    Main_BO = GetMainBO(Main_BO, trans)
                End While
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetMainBO(ByVal BO_Code As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = ""
            Dim isSaved As Boolean = True

            'qry = "select distinct (select ''','''+sfg_batch_code from TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL where batch_code in ('" + BO_Code + "') and isnull(sfg_batch_code,'')<>'' for xml path('')) as code"
            qry = "select distinct (select ''','''+batch_code from TSPL_PP_BATCH_ORDER_HEAD where main_batch_code in ('" + BO_Code + "') for xml path('')) as code"
            Dim strCode As String = ""
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

            If clsCommon.myLen(strCode) > 0 Then
                If strCode.Substring(0, 3) = "','" Then
                    strCode = strCode.Substring(3, strCode.Length - 3)
                End If
            End If

            Return strCode
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function IsJobWorkBatchOrder(ByVal strCode As String, ByVal tran As SqlTransaction) As Boolean
        Return (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Is_Job_Work_Inward from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + strCode + "'", tran)) = 1)
    End Function
End Class

Public Class clsProcessBatchOrderMainDetail
#Region "Variables"
    Public ArrMainItem As List(Of clsProcessBatchOrderMainDetail) = Nothing
    Public bom_fat_pers As Decimal = Nothing
    Public bom_fat_kg As Decimal = Nothing
    Public bom_snf_pers As Decimal = Nothing
    Public bom_snf_kg As Decimal = Nothing
    Public SNO As Integer = Nothing
    Public bomcode As String = Nothing
    Public BomDesc As String = Nothing
    Public icode As String = Nothing
    Public iname As String = Nothing
    Public itype As String = Nothing
    Public UOM As String = Nothing
    Public qty As Decimal = Nothing
    Public shiftcode As String = Nothing
    Public shiftname As String = Nothing
    Public sectioncode As String = Nothing
    Public sectionname As String = Nothing
    Public Plan_Code As String = Nothing
    Public BOM_Revision_No As String = Nothing
#End Region
    Public Shared Function SaveMainItem(ByVal arr As List(Of clsProcessBatchOrderMainDetail), ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_PP_BATCH_ORDER_BOM_DETAIL where batch_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsProcessBatchOrderMainDetail In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Batch_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "SNO", objtr.SNO)
                    clsCommon.AddColumnsForChange(coll, "BOM_Code", objtr.bomcode)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.icode)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.UOM)
                    clsCommon.AddColumnsForChange(coll, "Quantity", objtr.qty)
                    clsCommon.AddColumnsForChange(coll, "Shift_Code", objtr.shiftcode)
                    clsCommon.AddColumnsForChange(coll, "Section_Code", objtr.sectioncode)
                    clsCommon.AddColumnsForChange(coll, "FAT_PERS", objtr.bom_fat_pers)
                    clsCommon.AddColumnsForChange(coll, "FAT_KG", objtr.bom_fat_kg)
                    clsCommon.AddColumnsForChange(coll, "SNF_PERS", objtr.bom_snf_pers)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", objtr.bom_snf_kg)
                    clsCommon.AddColumnsForChange(coll, "Plan_Code", objtr.Plan_Code)


                    clsCommon.AddColumnsForChange(coll, "BOM_Revision_No", objtr.BOM_Revision_No)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_BATCH_ORDER_BOM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsProcessBatchOrderRawDetail
#Region "Variables"
    Public ArrRawItem As List(Of clsProcessBatchOrderRawDetail) = Nothing
    Public Rawsno As Integer = Nothing
    Public Rawomcode As String = Nothing
    Public Proditem As String = Nothing
    Public rawicode As String = Nothing
    Public rawiname As String = Nothing
    Public rawitype As String = Nothing
    Public rawunit As String = Nothing
    Public producttype As String = Nothing
    Public rawqty As Decimal = Nothing
    Public fat As Decimal = Nothing
    Public snf As Decimal = Nothing
    Public fat_kg As Decimal = Nothing
    Public snf_kg As Decimal = Nothing
    Public rawbatchcode As String = Nothing
    Public remarks As String = Nothing
#End Region

    Public Shared Function SaveRawItem(ByVal arr As List(Of clsProcessBatchOrderRawDetail), ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL where batch_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsProcessBatchOrderRawDetail In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Batch_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "SNO", objtr.Rawsno)
                    clsCommon.AddColumnsForChange(coll, "BOM_Code", objtr.Rawomcode)
                    clsCommon.AddColumnsForChange(coll, "Prod_Item_Code", objtr.Proditem)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.rawicode)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.rawunit)
                    clsCommon.AddColumnsForChange(coll, "Quantity", objtr.rawqty)
                    clsCommon.AddColumnsForChange(coll, "FAT", objtr.fat)
                    clsCommon.AddColumnsForChange(coll, "SNF", objtr.snf)
                    clsCommon.AddColumnsForChange(coll, "fat_kg", objtr.fat_kg)
                    clsCommon.AddColumnsForChange(coll, "snf_kg", objtr.snf_kg)
                    clsCommon.AddColumnsForChange(coll, "SFG_Batch_Code", objtr.rawbatchcode)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.remarks)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
'=================================================================================
Public Class clsProcessSFGBatchOrder
#Region "Variables"
    Dim Shared  AllBomCode As String = Nothing
    Public ArrMainItem As List(Of clsProcessSFGBatchOrder) = Nothing
    Public ArrRawItem As List(Of clsProcessSFGBatchOrder) = Nothing

    Public closeyn As String = Nothing
    Public Batchcode As String = Nothing
    Public Batchdate As Date = Nothing
    Public batchdesc As String = Nothing
    Public status As String = Nothing
    Public IsPost As String = Nothing
    Public locationcode As String = Nothing
    Public locationname As String = Nothing
    Public itemcatcode As String = Nothing
    Public itemcatname As String = Nothing
    Public Plancode As String = Nothing
    Public Main_batchcode As String = Nothing
    Public Sub_batchcode As String = Nothing

    Public SNO As Integer = Nothing
    Public bomcode As String = Nothing
    Public icode As String = Nothing
    Public iname As String = Nothing
    Public itype As String = Nothing
    Public UOM As String = Nothing
    Public qty As Decimal = Nothing
    Public shiftcode As String = Nothing
    Public shiftname As String = Nothing
    Public sectioncode As String = Nothing
    Public sectionname As String = Nothing

    Public Rawsno As Integer = Nothing
    Public Rawomcode As String = Nothing
    Public Proditem As String = Nothing
    Public rawicode As String = Nothing
    Public rawiname As String = Nothing
    Public rawitype As String = Nothing
    Public rawunit As String = Nothing
    Public producttype As String = Nothing
    Public rawqty As Decimal = Nothing
    Public fat As Decimal = Nothing
    Public snf As Decimal = Nothing
    Public rawbatchcode As String = Nothing
    Public remarks As String = Nothing
#End Region

    Public Shared Function GetSFGQtyAT_SAVE(ByVal SFGCode As String, ByVal SFGQty As String, ByVal CurrentIcode As String) As Decimal
        Dim qty As Decimal = 0

        If clsCommon.myLen(CurrentIcode) > 0 Then
            Dim xSplitCodes() As String
            Dim xSplitQty() As String

            xSplitCodes = SFGCode.Replace("'", "").Split(",")
            xSplitQty = SFGQty.Split(",")

            For ii As Integer = 0 To xSplitCodes.Length - 1
                If clsCommon.myLen(xSplitCodes(ii)) > 0 AndAlso clsCommon.CompairString(xSplitCodes(ii), CurrentIcode) = CompairStringResult.Equal Then
                    qty = xSplitQty(ii)
                End If
            Next
        End If

        Return qty
    End Function

    Public Shared Function GetSFGRefNoAT_SAVE(ByVal SFGCode As String, ByVal SFGRefNo As String, ByVal CurrentIcode As String) As String
        Dim qty As String = ""

        If clsCommon.myLen(CurrentIcode) > 0 Then
            Dim xSplitCodes() As String
            Dim xSplitQty() As String

            xSplitCodes = SFGCode.Replace("'", "").Split(",")
            xSplitQty = SFGRefNo.Split(",")

            For ii As Integer = 0 To xSplitCodes.Length - 1
                If clsCommon.myLen(xSplitCodes(ii)) > 0 AndAlso clsCommon.CompairString(xSplitCodes(ii), CurrentIcode) = CompairStringResult.Equal Then
                    qty = xSplitQty(ii)
                End If
            Next
        End If

        Return qty
    End Function

    Public Shared Function SaveData(ByVal obj As clsProcessSFGBatchOrder, ByVal SFGQty As Decimal, ByVal SFGCOdes As String, ByVal SFGItemUOM As String, ByVal isnewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, SFGQty, SFGCOdes, SFGItemUOM, isnewEntry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsProcessSFGBatchOrder, ByVal SFGQty As Decimal, ByVal SFGCOdes As String, ByVal SFGItemUOM As String, ByVal isnewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            If isnewEntry Then 'clsCommon.myLen(obj.Batchcode) <= 0
                'obj.Batchcode = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.BATCHORDER, "", "")
            End If

            Dim qry As String = "" ' "select count(*) from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + obj.Batchcode + "'"
            'Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Batch_Code", obj.Batchcode)
            clsCommon.AddColumnsForChange(coll, "Batch_Date", clsCommon.GetPrintDate(obj.Batchdate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.batchdesc)
            clsCommon.AddColumnsForChange(coll, "Status", obj.status)
            clsCommon.AddColumnsForChange(coll, "Is_Post", obj.IsPost)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.locationcode)
            clsCommon.AddColumnsForChange(coll, "Structure_Code", obj.itemcatcode)
            clsCommon.AddColumnsForChange(coll, "Plan_Code", "")
            clsCommon.AddColumnsForChange(coll, "Main_Batch_Code", obj.Main_batchcode)
            clsCommon.AddColumnsForChange(coll, "Sub_Batch_Code", obj.Sub_batchcode)
            clsCommon.AddColumnsForChange(coll, "Closed_YN", obj.closeyn)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")))

            If isnewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_BATCH_ORDER_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_BATCH_ORDER_HEAD", OMInsertOrUpdate.Update, " TSPL_PP_BATCH_ORDER_HEAD.batch_code='" + obj.Batchcode + "'", trans)
            End If

            isSaved = isSaved AndAlso SaveMainItem(SFGQty, SFGCOdes, SFGItemUOM, obj.Batchcode, obj.shiftcode, obj.Batchdate, obj.Main_batchcode, obj.bomcode, trans)
            isSaved = isSaved AndAlso SaveRawItem(SFGQty, obj.ArrRawItem, obj.Batchcode, trans)

            '--------------------update batch code as child batch in main batch order record-----------------
            qry = "update TSPL_PP_BATCH_ORDER_HEAD set Sub_Batch_Code=Sub_Batch_Code+','+'" + obj.Batchcode + "' where batch_code='" + obj.Main_batchcode + "'" 'Sub_Batch_Code+''''+','+'" + obj.Batchcode + "'
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL set SFG_Batch_Code='" + obj.Batchcode + "' where batch_code='" + obj.Main_batchcode + "' and item_code in ('" + SFGCOdes + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveMainItem(ByVal Qty As Decimal, ByVal sfgcode As String, ByVal sfgitemuom As String, ByVal strCode As String, ByVal SFGshiftcode As String, ByVal xdate As Date, ByVal mainbatchcode As String, ByVal BOM_Code As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_PP_BATCH_ORDER_BOM_DETAIL where batch_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '-------------------shiftcode-----------------
            If clsCommon.myLen(SFGshiftcode) <= 0 Then
                qry = "select top 1 shift_code from tspl_shift_master"
                SFGshiftcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            End If

            '===========add top 1 because now bom of same item with same unit made more than 1 times.--------------
            If clsCommon.myLen(BOM_Code) <= 0 Then
                qry = "select top 1 TSPL_PP_BOM_HEAD.BOM_CODE,ROW_NUMBER() over (order by TSPL_PP_BOM_HEAD.prod_item_code) as SNO,TSPL_PP_BOM_HEAD.prod_ITEM_CODE as item_code,TSPL_PP_BOM_HEAD.prod_quantity as QUANTITY,TSPL_PP_BOM_head.prod_item_unit_code as UNIT_CODE,TSPL_PP_BOM_HEAD.Section_Code from TSPL_PP_BOM_HEAD where isnull(TSPL_PP_BOM_HEAD.is_osp,0)<>1 and isnull(TSPL_PP_BOM_HEAD.Is_Post,'0')='1' and ('" + clsCommon.GetPrintDate(xdate, "dd/MMM/yyyy") + "' between TSPL_PP_BOM_HEAD.Valid_FROM_DATE and TSPL_PP_BOM_HEAD.Valid_UPTO_DATE) and TSPL_PP_BOM_head.prod_ITEM_CODE in ('" + sfgcode + "') and TSPL_PP_BOM_head.PROD_ITEM_UNIT_CODE in ('" + sfgitemuom + "') "
                qry += " order by TSPL_PP_BOM_HEAD.bom_date desc"
            Else
                qry = "select top 1 TSPL_PP_BOM_HEAD.BOM_CODE,ROW_NUMBER() over (order by TSPL_PP_BOM_HEAD.prod_item_code) as SNO,TSPL_PP_BOM_HEAD.prod_ITEM_CODE as item_code,TSPL_PP_BOM_HEAD.prod_quantity as QUANTITY,TSPL_PP_BOM_head.prod_item_unit_code as UNIT_CODE,TSPL_PP_BOM_HEAD.Section_Code from TSPL_PP_BOM_HEAD where isnull(TSPL_PP_BOM_HEAD.is_osp,0)<>1 and isnull(TSPL_PP_BOM_HEAD.Is_Post,'0')='1' and ('" + clsCommon.GetPrintDate(xdate, "dd/MMM/yyyy") + "' between TSPL_PP_BOM_HEAD.Valid_FROM_DATE and TSPL_PP_BOM_HEAD.Valid_UPTO_DATE) and TSPL_PP_BOM_head.prod_ITEM_CODE in ('" + sfgcode + "') and TSPL_PP_BOM_head.PROD_ITEM_UNIT_CODE in ('" + sfgitemuom + "') and TSPL_PP_BOM_HEAD.BOM_CODE='" & BOM_Code & "'"
                qry += " order by TSPL_PP_BOM_HEAD.bom_date desc"
            End If
            
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            AllBomCode = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    coll = New Hashtable()

                    AllBomCode = AllBomCode + "','" + clsCommon.myCstr(dr("BOM_CODE"))

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Batch_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "SNO", CInt(dr("SNO")))
                    clsCommon.AddColumnsForChange(coll, "BOM_Code", clsCommon.myCstr(dr("BOM_CODE")))
                    clsCommon.AddColumnsForChange(coll, "Item_Code", clsCommon.myCstr(dr("ITEM_CODE")))
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", clsCommon.myCstr(dr("UNIT_CODE")))
                    clsCommon.AddColumnsForChange(coll, "Quantity", Qty) 'clsCommon.myCdbl(dr("QUANTITY"))
                    clsCommon.AddColumnsForChange(coll, "Shift_Code", SFGshiftcode)

                    Dim fatpers As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + clsCommon.myCstr(dr("ITEM_CODE")) + "' and TSPL_PARAMETER_MASTER.Type='FAT'", trans))
                    clsCommon.AddColumnsForChange(coll, "FAT_PERS", fatpers)
                    clsCommon.AddColumnsForChange(coll, "FAT_KG", IIf(clsCommon.myCdbl(fatpers) > 0, System.Math.Round((Qty * clsCommon.myCdbl(fatpers)) / 100, 2), 0)) 'clsCommon.myCdbl(dr("QUANTITY"))

                    Dim snfpers As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + clsCommon.myCstr(dr("ITEM_CODE")) + "' and TSPL_PARAMETER_MASTER.Type='SNF'", trans))
                    clsCommon.AddColumnsForChange(coll, "SNF_PERS", snfpers)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", IIf(clsCommon.myCdbl(snfpers) > 0, System.Math.Round((Qty * clsCommon.myCdbl(snfpers)) / 100, 2), 0)) 'clsCommon.myCdbl(dr("QUANTITY"))
                    clsCommon.AddColumnsForChange(coll, "Section_Code", clsCommon.myCstr(dr("Section_Code")))

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_BATCH_ORDER_BOM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveRawItem(ByVal Main_Qty As String, ByVal arr As List(Of clsProcessSFGBatchOrder), ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL where batch_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "select row_number() over (order by TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE) as sno,TSPL_PP_BOM_HEAD.prod_quantity,TSPL_PP_BOM_HEAD.bom_code,TSPL_PP_BOM_HEAD.prod_item_code,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,TSPL_PP_BOM_ITEM_DETAIL.QUANTITY,TSPL_PP_BOM_ITEM_DETAIL.FAT,TSPL_PP_BOM_ITEM_DETAIL.SNF,TSPL_PP_BOM_ITEM_DETAIL.remarks from TSPL_PP_BOM_ITEM_DETAIL left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.bom_code=TSPL_PP_BOM_ITEM_DETAIL.bom_code where TSPL_PP_BOM_ITEM_DETAIL.bom_code in ('" + AllBomCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Batch_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "SNO", CInt(dr("sno")))
                    clsCommon.AddColumnsForChange(coll, "BOM_Code", clsCommon.myCstr(dr("bom_code")))
                    clsCommon.AddColumnsForChange(coll, "Prod_Item_Code", clsCommon.myCstr(dr("prod_item_code")))
                    clsCommon.AddColumnsForChange(coll, "Item_Code", clsCommon.myCstr(dr("ITEM_CODE")))
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", clsCommon.myCstr(dr("UNIT_CODE")))

                    Dim rawqty As Decimal = 0
                    rawqty = System.Math.Round((clsCommon.myCdbl(dr("QUANTITY")) / clsCommon.myCdbl(dr("prod_quantity"))) * Main_Qty, 2)

                    clsCommon.AddColumnsForChange(coll, "Quantity", rawqty) 'clsCommon.myCdbl(dr("QUANTITY"))
                    clsCommon.AddColumnsForChange(coll, "FAT", clsCommon.myCdbl(dr("fat")))
                    clsCommon.AddColumnsForChange(coll, "SNF", clsCommon.myCdbl(dr("snf")))
                    clsCommon.AddColumnsForChange(coll, "fat_kg", IIf(clsCommon.myCdbl(dr("fat")) > 0, System.Math.Round((rawqty * clsCommon.myCdbl(dr("fat"))) / 100, 2), 0))
                    clsCommon.AddColumnsForChange(coll, "snf_kg", IIf(clsCommon.myCdbl(dr("snf")) > 0, System.Math.Round((rawqty * clsCommon.myCdbl(dr("snf"))) / 100, 2), 0))
                    clsCommon.AddColumnsForChange(coll, "SFG_Batch_Code", "")
                    clsCommon.AddColumnsForChange(coll, "Remarks", clsCommon.myCstr(dr("remarks")))

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetManualBOM(ByVal Main_Batch_Code As String, ByVal Item_Code As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select  top 1 BOMCODE from TEMP_CHILDBO_REF_NO where Main_BO_Code='" & Main_Batch_Code & "' AND Item_Code='" & Item_Code & "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function GetBOM(ByVal Child_Batch_Code As String, ByVal Item_Code As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select top 1 BOM_Code from TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL where Batch_Code='" & Child_Batch_Code & "' and Prod_Item_Code='" & Item_Code & "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

End Class

