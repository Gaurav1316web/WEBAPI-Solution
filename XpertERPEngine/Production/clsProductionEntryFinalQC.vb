Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class clsProductionEntryFinalQCHead

#Region "Variables"
    Public QC_Code As String
    Public QC_Date As Date
    Public Against_PE_Code As String = Nothing
    Public DESCRIPTION As String
    Public Batch_Code As String
    Public BATCH_DATE As Date
    Public RECEIVED_BY As String
    Public RECEIVED_BY_NAME As String
    Public LOCATION_CODE As String
    Public LOCATION_NAME As String
    Public COMMENTS As String
    Public CREATED_BY As String
    Public APPROVED_BY As String
    Public POSTED As Boolean
    Public Posting_Date As Date
    Public Section_Stage_Map_Code As String
    Public CONSM_LOCATION_CODE As String
    Public CONSM_SECTION_CODE As String
    Public ManualBatchNo As String = String.Empty
    Public LINE_NO As String = String.Empty
    Public CostCenterCode As String = String.Empty
    Public ProfitCenterCode As String = String.Empty
    Public CostCenterName As String = String.Empty
    Public ProfitCenterName As String = String.Empty
    Public Status As String
    Public Is_Job_Work_Inward As Boolean = False
    Public ArrBatchItem As List(Of clsProductionEntryFinalQCDetail) = Nothing
    Public ArrIssueItem As List(Of clsProductionEntryFinalQCIssueItems) = Nothing
    Public ArrStage As List(Of clsProductionEntryFinalQCStageDetail) = Nothing
    Public ArrQC As List(Of clsProductionEntryFinalQCQCDetail) = Nothing
    Public ArrWF As List(Of clsProductionEntryFinalQCWreckageFlushingItem) = Nothing
    Public ArrScrap As List(Of clsProductionEntryFinalQCScrapItems) = Nothing
    Public ArrConsmCost As New List(Of clsConsumptionCostWithoutBatch)
    Public ArrDetail As List(Of clsProductionEntryFinalQCParameterDetail) = Nothing
    Public ArrParamDetail As List(Of clsProductionEntryFinalQCQCParam) = Nothing
#End Region

    Public Shared Function GetBatchFinder(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "select * from (select  TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code as [Code],max(TSPL_PP_BATCH_ORDER_HEAD.location_code) as [Location Code],max(TSPL_PP_BATCH_ORDER_HEAD.batch_date) as [Date],max(isnull(TSPL_PP_BATCH_ORDER_HEAD.ManualBatchNo,'') ) as [Manual Batch No],max(TSPL_PP_BATCH_ORDER_HEAD.Status) as Status,max(TSPL_PP_BATCH_ORDER_HEAD.structure_code) as [Production Structure],TSPL_PP_BATCH_ORDER_BOM_DETAIL.Unit_Code,sum(TSPL_PP_BATCH_ORDER_BOM_DETAIL.Quantity) as Quantity,max(ISNULL(prod.Produced_Qty,0)) as [Production Quantity],max(TSPL_PP_STAGE_PROCESS_HEAD.comp_code) as Company_Code,max(TSPL_PP_STAGE_PROCESS_HEAD.posted) as Posted,max(TSPL_PP_BATCH_ORDER_HEAD.Closed_YN) as [Manual Closed] " & _
       " from (select max(STAGE_PROCESS_CODE) as STAGE_PROCESS_CODE,MAX(posted) as posted,max(comp_code) as comp_code,Main_Batch_Code from TSPL_PP_STAGE_PROCESS_HEAD  where 2=2  group by Main_Batch_Code) as TSPL_PP_STAGE_PROCESS_HEAD inner join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code=TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code " & _
       " and tspl_pp_batch_order_head.comp_code=TSPL_PP_STAGE_PROCESS_HEAD.comp_code " & _
       " left join TSPL_PP_BATCH_ORDER_BOM_DETAIL on tspl_pp_batch_order_head.Batch_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code " & _
       " Left join ( select  Batch_Code,UNIT_CODE,sum(Quantity) as Quantity,sum(isnull(Produced_Qty,0)) as Produced_Qty from (  select ROW_NUMBER()  over(partition by PE.Batch_Code order by Item.Product_Type desc, PE_Dtl.Item_Code) as S_no, " & _
       " PE.Batch_Code,  PE_Dtl.Item_Code,PE_Dtl.Unit_Code,sum(PE_Dtl.BATCH_QTY) as Quantity,sum(PE_Dtl.FINAL_PRODUCTION_QTY) as Produced_Qty  from TSPL_PE_FINALQC_HEAD PE left join TSPL_PP_PRODUCTION_RETURN PR on PE.QC_Code=PR.QC_Code " & _
       " left join TSPL_PE_FINALQC_DETAIL PE_Dtl on PE.QC_Code=PE_Dtl.QC_Code  " & _
       " left join TSPL_ITEM_MASTER Item on PE_Dtl.Item_Code= Item.Item_Code where PR.PROD_RETURN_CODE is null  group by PE.Batch_Code,PE_Dtl.Item_Code,PE_Dtl.Unit_Code,Item.Product_Type ) as Prod_Main " & _
       " group by Batch_Code,UNIT_CODE ) as Prod " & _
       " on TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code=Prod.Batch_Code and TSPL_PP_BATCH_ORDER_BOM_DETAIL.Unit_Code=Prod.Unit_Code" & _
       " group by TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code,TSPL_PP_BATCH_ORDER_BOM_DETAIL.Unit_Code,Prod.Unit_Code) as Final "
        Dim str As String = ""
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls = whrCls + " and Company_Code='" + objCommonVar.CurrentCompanyCode + "'"
        Else
            whrCls = " Company_Code='" + objCommonVar.CurrentCompanyCode + "'"
        End If
        str = clsCommon.ShowSelectForm("STD", qry, "Code", whrCls, currCode, "Code", isButtonClicked)

        Return str
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrloc As String, ByVal NavType As NavigatorType) As clsProductionEntryFinalQCHead
        Return GetData(strCode, arrloc, NavType, Nothing)
    End Function
    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PE_FINALQC_HEAD", "QC_Code", "TSPL_PE_FINALQC_DETAIL", "QC_Code", "TSPL_PE_FINALQC_STAGE_DETAIL", "QC_Code", trans)
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PE_FINALQC_QC_DETAIL", "QC_Code", "TSPL_PE_FINALQC_WRECKAGE_FLUSHING_ITEM", "QC_Code", "TSPL_PE_FINALQC_SCRAP_ITEM", "QC_Code", trans)
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PE_FINALQC_PARAMETER_DETAIL", "QC_Code", "TSPL_PE_FINALQC_QC_PARAMETER", "QC_Code", trans)
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select QC_Date , LOCATION_CODE from TSPL_PE_FINALQC_HEAD where QC_Code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProductionEntryFinalQC, clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE")), clsCommon.myCDate(dt.Rows(0)("QC_Date")), trans)

            End If

            HistoryUpdate(strCode, trans)
            clsSerializeInvenotry.DeleteData("Production", strCode, trans)
            Dim qry As String

            qry = "delete from tspl_batch_Item where Document_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "delete from TSPL_PE_FINALQC_PARAMETER_DETAIL where QC_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PE_FINALQC_QC_PARAMETER where QC_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "delete from TSPL_PE_FINALQC_DETAIL where QC_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PE_FINALQC_CONSUMPTION where QC_Code ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PE_FINALQC_ISSUE_ITEM where QC_Code ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PE_FINALQC_STAGE_DETAIL where QC_Code ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PE_FINALQC_WRECKAGE_FLUSHING_ITEM where QC_Code ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PE_FINALQC_QC_DETAIL where QC_Code ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PE_FINALQC_SCRAP_ITEM where QC_Code ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "DELETE FROM TSPL_PP_COST_WITHOUT_BATCH WHERE PROD_ENTRY_CODE in ( select Against_PE_Code from TSPL_PE_FINALQC_HEAD where QC_Code='" & strCode & "')"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PE_FINALQC_HEAD where QC_Code ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_PE_FINALQC_HEAD_Delete_Data set Delete_By = '" + objCommonVar.CurrentUserCode + "' where QC_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrloc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsProductionEntryFinalQCHead
        Dim obj As New clsProductionEntryFinalQCHead()
        Dim objtr As New clsProductionEntryFinalQCDetail()

        Dim LocCond As String = " where 1=1 "
        If clsCommon.myLen(arrloc) > 0 Then
            LocCond = LocCond & " and TSPL_PE_FINALQC_HEAD.LOCATION_CODE in (" + arrloc + ")"
        End If

        Dim qry As String = "SELECT TSPL_PE_FINALQC_HEAD.Is_Job_Work_Inward,TSPL_PE_FINALQC_HEAD.QC_Code,TSPL_PE_FINALQC_HEAD.Against_PE_Code,TSPL_PE_FINALQC_HEAD.Status,TSPL_PE_FINALQC_HEAD.DESCRIPTION,TSPL_PE_FINALQC_HEAD.QC_Date,TSPL_PE_FINALQC_HEAD.Batch_Code, TSPL_PE_FINALQC_HEAD.BATCH_DATE,TSPL_PE_FINALQC_HEAD.RECEIVED_BY,TSPL_EMPLOYEE_MASTER.EMP_NAME,TSPL_PE_FINALQC_HEAD.LOCATION_CODE,TSPL_LOCATION_MASTER.LOCATION_DESC,TSPL_PE_FINALQC_HEAD.COMMENTS,"
        qry += " TSPL_PE_FINALQC_HEAD.MODIFIED_BY AS APPROVED_BY,TSPL_PE_FINALQC_HEAD.Created_By,TSPL_PE_FINALQC_HEAD.POSTED,TSPL_PE_FINALQC_HEAD.POSTING_DATE,TSPL_PE_FINALQC_HEAD.Section_Stage_Map_Code,TSPL_PE_FINALQC_HEAD.CONSM_LOCATION_CODE,TSPL_PE_FINALQC_HEAD.CONSM_SECTION_CODE,TSPL_PE_FINALQC_HEAD.ManualBatchNo, TSPL_PE_FINALQC_HEAD.LINE_NO,TSPL_PE_FINALQC_HEAD.CostCenterCode , TSPL_CostCenter_MASTER.Cost_name as [Cost_Center_Name],TSPL_PE_FINALQC_HEAD.ProfitCenterCode  ,TSPL_PROFIT_CENTER_MASTER.Name as ProfitCenterName  FROM   TSPL_PE_FINALQC_HEAD " & _
        " left outer join TSPL_PROFIT_CENTER_MASTER on TSPL_PROFIT_CENTER_MASTER.Code =TSPL_PE_FINALQC_HEAD.ProfitCenterCode " & _
        " left outer join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.Cost_Code =TSPL_PE_FINALQC_HEAD.CostCenterCode " & _
        " left JOIN  TSPL_EMPLOYEE_MASTER ON TSPL_PE_FINALQC_HEAD.RECEIVED_BY=TSPL_EMPLOYEE_MASTER.EMP_CODE INNER JOIN  TSPL_LOCATION_MASTER ON TSPL_PE_FINALQC_HEAD.LOCATION_CODE=TSPL_LOCATION_MASTER.LOCATION_CODE " & LocCond & " "

        Select Case NavType
            Case NavigatorType.First
                qry += " AND QC_Code = (select MIN(QC_Code) from TSPL_PE_FINALQC_HEAD where location_code in (" + arrloc + "))"
            Case NavigatorType.Last
                qry += " AND QC_Code = (select Max(QC_Code) from TSPL_PE_FINALQC_HEAD where location_code in (" + arrloc + "))"
            Case NavigatorType.Next
                qry += " AND QC_Code = (select Min(QC_Code) from TSPL_PE_FINALQC_HEAD where QC_Code>'" + strCode + "' and location_code in (" + arrloc + "))"
            Case NavigatorType.Previous
                qry += " AND QC_Code = (select Max(QC_Code) from TSPL_PE_FINALQC_HEAD where QC_Code<'" + strCode + "' and location_code in (" + arrloc + "))"
            Case NavigatorType.Current
                qry += " AND QC_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj.QC_Code = dt.Rows(0)("QC_Code")
            obj.Against_PE_Code = clsCommon.myCstr(dt.Rows(0)("Against_PE_Code"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.QC_Date = clsCommon.GetPrintDate(dt.Rows(0)("QC_Date"), "dd/MMM/yyyy")
            obj.Batch_Code = clsCommon.myCstr(dt.Rows(0)("Batch_Code"))
            obj.BATCH_DATE = clsCommon.GetPrintDate(dt.Rows(0)("BATCH_DATE"), "dd/MMM/yyyy")
            obj.RECEIVED_BY = clsCommon.myCstr(dt.Rows(0)("RECEIVED_BY"))
            obj.RECEIVED_BY_NAME = clsCommon.myCstr(dt.Rows(0)("EMP_NAME"))
            obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
            obj.LOCATION_NAME = clsCommon.myCstr(dt.Rows(0)("LOCATION_DESC"))
            obj.COMMENTS = clsCommon.myCstr(dt.Rows(0)("COMMENTS"))
            obj.Section_Stage_Map_Code = clsCommon.myCstr(dt.Rows(0)("Section_Stage_Map_Code"))
            obj.CONSM_LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("CONSM_LOCATION_CODE"))
            obj.CONSM_SECTION_CODE = clsCommon.myCstr(dt.Rows(0)("CONSM_SECTION_CODE"))
            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.APPROVED_BY = clsCommon.myCstr(dt.Rows(0)("APPROVED_BY"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            obj.ManualBatchNo = clsCommon.myCstr(dt.Rows(0)("ManualBatchNo"))
            obj.LINE_NO = clsCommon.myCstr(dt.Rows(0)("LINE_NO"))
            obj.CostCenterCode = clsCommon.myCstr(dt.Rows(0)("CostCenterCode"))
            obj.CostCenterName = clsCommon.myCstr(dt.Rows(0)("Cost_Center_Name"))
            obj.ProfitCenterCode = clsCommon.myCstr(dt.Rows(0)("ProfitCenterCode"))
            obj.ProfitCenterName = clsCommon.myCstr(dt.Rows(0)("ProfitCenterName"))
            obj.Is_Job_Work_Inward = (clsCommon.myCdbl(dt.Rows(0)("Is_Job_Work_Inward")) = 1)
            strCode = dt.Rows(0)("QC_Code")
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If

        obj.ArrBatchItem = clsProductionEntryFinalQCDetail.GetProductionEntryDetail(strCode, trans)
        obj.ArrIssueItem = clsProductionEntryFinalQCIssueItems.GetPPPEIssuedDetail(strCode, trans)
        obj.ArrStage = clsProductionEntryFinalQCStageDetail.GetPPPEStageDetail(strCode, trans)
        obj.ArrQC = clsProductionEntryFinalQCQCDetail.GetPPPEQCDetail(strCode, trans)
        obj.ArrWF = clsProductionEntryFinalQCWreckageFlushingItem.GetPPPEWFDetail(strCode, trans)
        obj.ArrScrap = clsProductionEntryFinalQCScrapItems.GetPPPEScrapDetail(strCode, trans)
        obj.ArrConsmCost = clsConsumptionCostWithoutBatch.GetConsumptionCost(obj.Against_PE_Code, trans)
        obj.ArrDetail = clsProductionEntryFinalQCParameterDetail.GetData(obj.QC_Code, trans)
        obj.ArrParamDetail = clsProductionEntryFinalQCQCParam.getData(obj.QC_Code, trans)
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsProductionEntryFinalQCHead, ByVal objList As List(Of clsProductionEntryFinalQCDetail), ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If isNewEntry Then
                If clsCommon.myLen(strCode) <= 0 Then
                    obj.QC_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.QC_Date, "dd/MMM/yyyy"), clsDocType.ProductionEntryFinalQC, "", obj.LOCATION_CODE)
                Else
                    obj.QC_Code = strCode
                End If
            End If
            If (clsCommon.myLen(obj.QC_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim qry As String = ""
            qry = "SELECT POSTED FROM TSPL_PE_FINALQC_HEAD WHERE QC_Code='" & obj.QC_Code & "'"
            obj.POSTED = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))

            If (obj.POSTED = True) Then
                Throw New Exception("Document -" & obj.QC_Code & " is already posted.")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProductionEntryFinalQC, obj.LOCATION_CODE, obj.QC_Date, trans)
            qry = "delete from tspl_batch_Item where Document_Code ='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PE_FINALQC_CONSUMPTION where QC_Code='" & obj.QC_Code & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_COST_WITHOUT_BATCH where PROD_ENTRY_CODE='" & obj.Against_PE_Code & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "QC_Code", obj.QC_Code)
            clsCommon.AddColumnsForChange(coll, "Against_PE_Code", obj.Against_PE_Code)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "QC_Date", clsCommon.GetPrintDate(obj.QC_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Batch_Code", obj.Batch_Code, True)
            clsCommon.AddColumnsForChange(coll, "BATCH_DATE", clsCommon.GetPrintDate(obj.BATCH_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "RECEIVED_BY", clsCommon.myCstr(obj.RECEIVED_BY), True)
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", clsCommon.myCstr(obj.LOCATION_CODE))
            clsCommon.AddColumnsForChange(coll, "COMMENTS", clsCommon.myCstr(obj.COMMENTS))
            clsCommon.AddColumnsForChange(coll, "Section_Stage_Map_Code", clsCommon.myCstr(obj.Section_Stage_Map_Code), True)
            clsCommon.AddColumnsForChange(coll, "CONSM_LOCATION_CODE", clsCommon.myCstr(obj.CONSM_LOCATION_CODE), True)
            clsCommon.AddColumnsForChange(coll, "CONSM_SECTION_CODE", clsCommon.myCstr(obj.CONSM_SECTION_CODE), True)
            clsCommon.AddColumnsForChange(coll, "ManualBatchNo", obj.ManualBatchNo)
            clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO, True)
            clsCommon.AddColumnsForChange(coll, "CostCenterCode", obj.CostCenterCode, True)
            clsCommon.AddColumnsForChange(coll, "ProfitCenterCode", obj.ProfitCenterCode, True)
            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Is_Job_Work_Inward", IIf(obj.Is_Job_Work_Inward, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_PE_FINALQC_HEAD where QC_Code = '" & obj.QC_Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PE_FINALQC_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.QC_Code + " Is Already Exist")
                End If
            Else
                HistoryUpdate(obj.QC_Code, trans)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PE_FINALQC_HEAD", OMInsertOrUpdate.Update, "TSPL_PE_FINALQC_HEAD.QC_Code='" + obj.QC_Code + "'", trans)
            End If

            clsProductionEntryFinalQCDetail.SaveDetailData(obj.QC_Code, obj, objList, trans)
            clsProductionEntryFinalQCIssueItems.SaveData(obj.QC_Code, obj, obj.ArrIssueItem, trans)
            clsProductionEntryFinalQCStageDetail.SaveData(obj.QC_Code, obj.ArrStage, trans)
            clsProductionEntryFinalQCQCDetail.SaveData(obj.QC_Code, obj.ArrQC, trans)
            clsProductionEntryFinalQCWreckageFlushingItem.SaveData(obj, trans)
            clsProductionEntryFinalQCScrapItems.SaveData(obj, trans)
            clsConsumptionCostWithoutBatch.SaveConsumptionCost(obj.Against_PE_Code, obj.ArrConsmCost, trans)
            clsProductionEntryFinalQCParameterDetail.SaveData(obj.QC_Code, obj.ArrDetail, trans)
            clsProductionEntryFinalQCQCParam.saveData(obj.QC_Code, obj.ArrParamDetail, trans)
            If clsCommon.CompairString(obj.Status, "R") = CompairStringResult.Equal Then
                qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code) " & _
                    "values ('" + clsUserMgtCode.frmProductionEntryFinalQC + "','" & clsUserMgtCode.frmProductionEntryFinalQC & "','" & obj.QC_Code & "','" & clsCommon.GetPrintDate(obj.QC_Date, "dd-MMM-yyyy hh:mm:ss") & "','Other',0,'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" & objCommonVar.CurrentCompanyCode & "')"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal Form_Id As String, ByVal strDocNo As String, ByVal arrloc As String, ByVal isCheckForPosted As Boolean) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin
            PostData(Form_Id, strDocNo, arrloc, isCheckForPosted, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal Form_Id As String, ByVal strDocNo As String, ByVal arrloc As String, ByVal isCheckForPosted As Boolean, ByVal trans As SqlTransaction) As Boolean
        Return PostData(Form_Id, strDocNo, arrloc, isCheckForPosted, trans, "")
    End Function

    Public Shared Function PostData(ByVal Form_Id As String, ByVal strDocNo As String, ByVal arrloc As String, ByVal isCheckForPosted As Boolean, ByVal trans As SqlTransaction, ByVal voucherNo As String) As Boolean

        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Code not found to Post")
        End If
        Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
        Dim obj As clsProductionEntryFinalQCHead = clsProductionEntryFinalQCHead.GetData(strDocNo, arrloc, NavigatorType.Current, trans)

        If (obj Is Nothing OrElse clsCommon.myLen(obj.QC_Code) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If
        If (isCheckForPosted AndAlso obj.POSTED = True) Then
            Throw New Exception("Already Post on :" + obj.Posting_Date)
        End If

        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProductionEntryFinalQC, obj.LOCATION_CODE, obj.BATCH_DATE, trans)


        If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RequiredFinalQCofProductionEntry, clsFixedParameterCode.RequiredFinalQCofProductionEntry, trans)) > 0) Then
            HistoryUpdate(strDocNo, trans)
            Dim qry As String = "Update TSPL_PE_FINALQC_HEAD set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where QC_Code ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsProductionEntryFinalQCConsumption.SaveRM(obj.QC_Code, arrloc, trans)
            clsProductionEntryFinalQCConsumption.UpdateInventoryMovement(Form_Id, obj.QC_Code, arrloc, trans)
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateJEOnProduction, clsFixedParameterCode.CreateJEOnProduction, trans)) > 0 Then
                JournalEntry(trans, obj, voucherNo)
            End If
        Else
            Throw New Exception("Final QC is not for you")
        End If
        If clsCommon.myLen(voucherNo) <= 0 Then
            Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProductionEntryFinalQC + "'", trans))
            If clsCommon.CompairString(strNotificationOn, "P") = CompairStringResult.Equal Then
                CreateNotificationContentEMP(strDocNo, trans)
            End If
        End If
        Return True
    End Function

    Private Shared Function CreateNotificationContentEMP(ByVal StrDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim strNotifiContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProductionEntryFinalQC + "'", trans))
        Dim strNotifi_DetalContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Detail_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProductionEntryFinalQC + "'", trans))
        Dim strNotifiCaption As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Caption from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProductionEntryFinalQC + "'", trans))
        Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProductionEntryFinalQC + "'", trans))
        Dim strDocumentDate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Plan_Date from TSPL_PP_PRODUCTION_PLAN_HEAD where plan_code='" + StrDocNo + "'", trans))

        If clsCommon.myLen(strNotifiContent) > 0 Then
            Dim objNotification As New clsNotificationHead()
            objNotification.Notification_Text = strNotifiContent
            objNotification.Notification_Caption = strNotifiCaption
            objNotification.Notification_On = strNotificationOn
            objNotification.Notification_Detail_Text = strNotifi_DetalContent
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(StrDocNo))
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, (clsCommon.myCDate(strDocumentDate)))
            objNotification.SaveData(clsUserMgtCode.frmProductionEntryFinalQC, objNotification, trans)
            objNotification = Nothing
            Return True
        End If
        Return False
    End Function

    Public Shared Function CheckValidCode(ByVal Doc_No As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String = "select count(*) from TSPL_PE_FINALQC_HEAD where QC_Code='" & Doc_No & "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' "
        Dim count As Integer = clsDBFuncationality.getSingleValue(qry, trans)
        If count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "SELECT TSPL_PE_FINALQC_HEAD.QC_Code AS Code,TSPL_PE_FINALQC_HEAD.Against_PE_Code as ProductionEntry,TSPL_PE_FINALQC_HEAD.DESCRIPTION,Batch_Code as [Batch Code],LOCATION_CODE as [Location Code],CONSM_LOCATION_CODE,CONSM_SECTION_CODE,Section_Stage_Map_Code,TSPL_PE_FINALQC_HEAD.QC_Date, "
        qry += " TSPL_PE_FINALQC_HEAD.MODIFIED_BY AS APPROVED_BY,TSPL_PE_FINALQC_HEAD.Created_By,TSPL_PE_FINALQC_HEAD.POSTED,TSPL_PE_FINALQC_HEAD.POSTING_DATE FROM TSPL_PE_FINALQC_HEAD "
        Dim str As String = ""
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls = whrCls + " and TSPL_PE_FINALQC_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Else
            whrCls = " TSPL_PE_FINALQC_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        End If
        str = clsCommon.ShowSelectForm("PEQCM", qry, "Code", whrCls, currCode, "Code", isButtonClicked, "TSPL_PE_FINALQC_HEAD.QC_Date")

        Return str
    End Function

    Public Shared Function GetProductionReportData(ByVal FromDate As Date, ByVal ToDate As Date) As DataTable
        Dim dt As DataTable = Nothing
        Try
            Dim qry As String = ""
            qry = " DECLARE @STRQ VARCHAR(MAX); "
            qry += " EXEC TSPL_DATEWISE_PRODUCTION '" & clsCommon.GetPrintDate(FromDate.AddDays(-1), "dd/MMM/yyyy") & "' ,'" & clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") & "',@STRQ OUTPUT; "
            qry += " SELECT @STRQ; "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dt = clsDBFuncationality.GetDataTable(clsCommon.myCstr(dt.Rows(0)(0)))
            Else
                dt = New DataTable
            End If
        Catch ex As Exception
            'Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function

    Public Shared Function checkStock(ByVal TR_Type As String, ByVal BO_MO_Code As String, ByVal ProdQty As Decimal, ByVal QC_Date As Date, ByVal Location_Code As String, ByVal QC_Code As String) As Boolean
        Dim strq As String = ""
        strq = ""
        If TR_Type = "BO" Then

            strq = " SELECT TSPL_MF_BATCH_PP_DETAIL.ITEM_CODE as PROD_ITEM_CODE,SUM(TSPL_MF_BATCH_PP_DETAIL.BATCH_QTY) AS PROD_QTY,TSPL_MF_BATCH_ORDER_DETAIL.ITEM_CODE," & _
                   " SUM(TSPL_MF_BATCH_ORDER_DETAIL.QTY) AS BOM_QTY,((SUM(TSPL_MF_BATCH_ORDER_DETAIL.QTY)/SUM(TSPL_MF_BATCH_PP_DETAIL.BATCH_QTY))* " & ProdQty & ") as REQUIR_QTY,TSPL_MF_BATCH_ORDER_DETAIL.UNIT_CODE " & _
                   " FROM TSPL_MF_BATCH_ORDER_DETAIL inner join TSPL_MF_BATCH_ORDER on TSPL_MF_BATCH_ORDER.Batch_Code=TSPL_MF_BATCH_ORDER_DETAIL.Batch_Code " & _
                   " inner join TSPL_MF_BATCH_PP_DETAIL on TSPL_MF_BATCH_ORDER.Batch_Code=TSPL_MF_BATCH_PP_DETAIL.Batch_Code " & _
                   " and TSPL_MF_BATCH_ORDER_DETAIL.Shift_Code=TSPL_MF_BATCH_PP_DETAIL.Shift_Code " & _
                   " and TSPL_MF_BATCH_ORDER_DETAIL.Section_Code=TSPL_MF_BATCH_PP_DETAIL.Section_Code " & _
                   " WHERE TSPL_MF_BATCH_ORDER.Batch_Code='" & BO_MO_Code & "' " & _
                   " GROUP BY TSPL_MF_BATCH_PP_DETAIL.ITEM_CODE,TSPL_MF_BATCH_ORDER_DETAIL.ITEM_CODE,TSPL_MF_BATCH_ORDER_DETAIL.UNIT_CODE "
        ElseIf TR_Type = "MO" Then
            strq = " select TSPL_MF_MANUFACTURING_ORDER.ITEM_CODE as PROD_ITEM_CODE,TSPL_MF_MANUFACTURING_ORDER.QTY_ORDERED_STOCK as PROD_QTY," & _
                   " TSPL_MF_MO_MATERIAL.CONSM_ITEM_CODE as ITEM_CODE,TSPL_MF_MO_MATERIAL.BOM_QUANTITY AS BOM_QTY, " & _
                   " ((TSPL_MF_MO_MATERIAL.CONSM_QUANTITY * " & ProdQty & ")/TSPL_MF_MANUFACTURING_ORDER.QTY_ORDERED_STOCK)  AS REQUIR_QTY, " & _
                   " TSPL_MF_MO_MATERIAL.CONSM_ITEM_UNIT_CODE AS UNIT_CODE from TSPL_MF_MO_MATERIAL inner join TSPL_MF_MANUFACTURING_ORDER " & _
                   " on TSPL_MF_MO_MATERIAL.MO_CODE=TSPL_MF_MANUFACTURING_ORDER.MO_CODE WHERE TSPL_MF_MANUFACTURING_ORDER.MO_CODE='" & BO_MO_Code & "'"
        End If
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq)
        For Each dr As DataRow In dt.Rows
            Dim availQty As Double = 0
            Dim reqQty As Double = 0

            availQty = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(dr.Item("ITEM_CODE"), Location_Code, QC_Code, QC_Date, Nothing, dr.Item("Unit_Code"))
            reqQty = dr.Item("REQUIR_QTY")
            If availQty < reqQty Then
                clsCommon.MyMessageBoxShow("Item Code: " & dr.Item("ITEM_CODE") & " ; Required Qty : " & reqQty & " ; Available Qty : " & availQty & "")
                Return False
            End If
        Next

        Return True
    End Function

    Public Shared Function GetCategorywiseProduction(ByVal FromBODate As Date, ByVal ToBODate As Date, ByVal figure As String) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""
            qry = "SELECT TSPL_ITEM_MASTER.PROD_ITEM_CATEGORY_CODE AS 'Category', CONVERT(Decimal(18,2), SUM(TSPL_PE_FINALQC_DETAIL.RECEIPT_QTY)/" + figure + ") AS 'Produced Qty', " & _
                 " SUM(TSPL_PE_FINALQC_DETAIL.REJ_QTY) AS 'Rejected Qty',SUM(TSPL_PE_FINALQC_DETAIL.BREAKAGE_QTY) as 'Break Qty' " & _
                 " FROM TSPL_PE_FINALQC_DETAIL INNER JOIN TSPL_PE_FINALQC_HEAD ON TSPL_PE_FINALQC_DETAIL.QC_Code=TSPL_PE_FINALQC_HEAD.QC_Code  " & _
                 " LEFT JOIN TSPL_ITEM_MASTER ON TSPL_PE_FINALQC_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " & _
                 " where TSPL_PE_FINALQC_HEAD.QC_Date between '" & clsCommon.GetPrintDate(FromBODate, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(ToBODate, "dd/MMM/yyyy") & "'  " & _
                 " GROUP BY TSPL_ITEM_MASTER.PROD_ITEM_CATEGORY_CODE "
            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function

    Public Shared Function GetCategorywiseProductionDetail(ByVal FromBODate As Date, ByVal ToBODate As Date, ByVal Category As String) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""
            qry = "SELECT TSPL_PE_FINALQC_HEAD.QC_Code AS 'Production No',TSPL_PE_FINALQC_HEAD.DESCRIPTION AS 'Description',TSPL_PE_FINALQC_HEAD.QC_Date AS 'Production Date',TSPL_ITEM_MASTER.PROD_ITEM_CATEGORY_CODE AS 'Category',(TSPL_PE_FINALQC_DETAIL.RECEIPT_QTY) AS 'Produced Qty', " & _
                 " (TSPL_PE_FINALQC_DETAIL.REJ_QTY) AS 'Rejected Qty',(TSPL_PE_FINALQC_DETAIL.BREAKAGE_QTY) as 'Break Qty' " & _
                 " FROM TSPL_PE_FINALQC_DETAIL INNER JOIN TSPL_PE_FINALQC_HEAD ON TSPL_PE_FINALQC_DETAIL.QC_Code=TSPL_PE_FINALQC_HEAD.QC_Code  " & _
                 " LEFT JOIN TSPL_ITEM_MASTER ON TSPL_PE_FINALQC_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " & _
                 " where TSPL_PE_FINALQC_HEAD.QC_Date between '" & clsCommon.GetPrintDate(FromBODate, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(ToBODate, "dd/MMM/yyyy") & "'  " & _
                 " and TSPL_ITEM_MASTER.PROD_ITEM_CATEGORY_CODE='" & Category & "'"
            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function

    Public Shared Function GetIssueAgainstBatch(ByVal Batch_Code As String, ByVal Doc_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim qry As String = " select TSPL_PP_ISSUE_HEAD.Issue_Code,TSPL_PP_SP_ISSUE_ITEM_DETAIL.From_Loaction_Code,TSPL_PP_SP_ISSUE_ITEM_DETAIL.To_Location_Code," & _
         " TSPL_PP_ISSUE_HEAD.Main_Location_Code, TSPL_PP_SP_ISSUE_ITEM_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type," & _
         " isnull(TSPL_ITEM_MASTER.Product_Type,'') as Product_Type,TSPL_UNIT_MASTER.Unit_Desc,TSPL_PP_SP_ISSUE_ITEM_DETAIL.Unit_Code," & _
         " TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_Qty as Issue_Qty, " & _
         " TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_FAT_KG as Avail_FAT_KG,TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_FAT_Per as Avail_FAT_Pers, " & _
         " TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_SNF_KG as Avail_SNF_KG,TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_SNF_Per as Avail_SNF_Pers, " & _
         " ((TSPL_PP_SP_ISSUE_ITEM_DETAIL.FAT_Amt)/(case when TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_FAT_KG=0 then 1 else TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_FAT_KG end)) as Issued_FAT_Rate,((TSPL_PP_SP_ISSUE_ITEM_DETAIL.SNF_Amt)/(case when TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_SNF_KG=0 then 1 else TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_SNF_KG end)) as Issued_SNF_Rate,(TSPL_PP_SP_ISSUE_ITEM_DETAIL.FAT_Amt) as Issued_FAT_Amt,(TSPL_PP_SP_ISSUE_ITEM_DETAIL.SNF_Amt) as Issued_SNF_Amt " & _
         " from TSPL_PP_SP_ISSUE_ITEM_DETAIL " & _
         " inner join TSPL_PP_STAGE_PROCESS_HEAD on TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE=TSPL_PP_SP_ISSUE_ITEM_DETAIL.STAGE_PROCESS_CODE " & _
         " inner join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=TSPL_PP_SP_ISSUE_ITEM_DETAIL.Issue_Code " & _
         " left join TSPL_ITEM_MASTER on TSPL_PP_SP_ISSUE_ITEM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
         " left join TSPL_UNIT_MASTER on TSPL_PP_SP_ISSUE_ITEM_DETAIL.Unit_Code=TSPL_UNIT_MASTER.Unit_Code " & _
         " where TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code='" & Batch_Code & "'  "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function

    Public Shared Function getIssueItemFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        If clsCommon.myLen(whrcls.Trim) = 0 Then
        Else
            whrcls = whrcls
        End If
        Dim qry As String = " select TSPL_PP_SP_ISSUE_ITEM_DETAIL.Item_Code as Code,TSPL_ITEM_MASTER.Item_Desc as Description, " & _
         " TSPL_ITEM_MASTER.Item_Type as [Item Type]," & _
         " isnull(TSPL_ITEM_MASTER.Product_Type,'') as [Product Type],TSPL_PP_SP_ISSUE_ITEM_DETAIL.Unit_Code as [Unit Code],TSPL_UNIT_MASTER.Unit_Desc as [Unit Description] " & _
         " from TSPL_PP_SP_ISSUE_ITEM_DETAIL " & _
         " inner join TSPL_PP_STAGE_PROCESS_HEAD on TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE=TSPL_PP_SP_ISSUE_ITEM_DETAIL.STAGE_PROCESS_CODE " & _
         " inner join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=TSPL_PP_SP_ISSUE_ITEM_DETAIL.Issue_Code " & _
         " left join TSPL_ITEM_MASTER on TSPL_PP_SP_ISSUE_ITEM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
         " left join TSPL_UNIT_MASTER on TSPL_PP_SP_ISSUE_ITEM_DETAIL.Unit_Code=TSPL_UNIT_MASTER.Unit_Code "

        str = clsCommon.ShowSelectForm("ITMIssueFind", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function getSectionStockItemFinder(ByVal whrcls As String, ByVal Loc_Code As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        If clsCommon.myLen(whrcls.Trim) = 0 Then
        Else
            whrcls = whrcls
        End If

        Dim qry As String = " select * from ( select Item_Code as Code,Item_Desc as [Item Description],sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed," & _
                           " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT  " & _
                           " where Location_Code='" & Loc_Code & "'  group by Item_Code,Item_Desc,Stock_UOM " & _
                           " union all " & _
                           " select Item_Code,Item_Desc,sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed, " & _
                           " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT_NEW where Location_Code='" & Loc_Code & "' " & _
                           " group by Item_Code,Item_Desc,Stock_UOM) as finder "

        str = clsCommon.ShowSelectForm("ITMIssueFind", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function getSectionStockItemMultipleFinder(ByVal whrcls As String, ByVal Loc_Code As String, ByVal ArrList As ArrayList, Optional ByVal TransType As String = "") As ArrayList
        Dim str As String = ""
        Dim qry As String = ""
        If clsCommon.myLen(whrcls.Trim) = 0 Then
        Else
            whrcls = whrcls
        End If
        If clsCommon.myCstr(TransType) = "Scrap" Then
            qry = " select *  from ( select Item_Code as Code,Item_Desc as [Name],sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed," & _
                         " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT  " & _
                         "  group by Item_Code,Item_Desc,Stock_UOM " & _
                         " union all " & _
                         " select Item_Code,Item_Desc,sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed, " & _
                         " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT_NEW " & _
                         " group by Item_Code,Item_Desc,Stock_UOM) as finder "
        Else
            qry = " select *  from ( select Item_Code as Code,Item_Desc as [Name],sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed," & _
                          " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT  " & _
                          " where Location_Code='" & Loc_Code & "'  group by Item_Code,Item_Desc,Stock_UOM " & _
                          " union all " & _
                          " select Item_Code,Item_Desc,sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed, " & _
                          " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT_NEW where Location_Code='" & Loc_Code & "' " & _
                          " group by Item_Code,Item_Desc,Stock_UOM) as finder "
        End If


        Dim arr1 As New ArrayList
        arr1 = clsCommon.ShowMultipleSelectForm("scjs", qry, "Code", "Name", ArrList, Nothing)

        Return arr1
    End Function

    Public Shared Function getItemFinder(ByVal whrcls As String, ByVal Loc_Code As String, ByVal ArrList As ArrayList, Optional ByVal TransType As String = "") As ArrayList
        Dim str As String = ""
        Dim qry As String = ""
        If clsCommon.myLen(whrcls.Trim) = 0 Then
        Else
            whrcls = whrcls
        End If
        qry = "Select Item_Code as Code, Item_Desc as Name from TSPL_ITEM_MASTER where tspl_item_master.Active ='1'  "
        Dim arr1 As New ArrayList
        arr1 = clsCommon.ShowMultipleSelectForm("Item_Finder", qry, "Code", "Name", ArrList, Nothing)
        Return arr1
    End Function

    Public Shared Function getLocationFinderWithBalance(ByVal whrcls As String, ByVal Curr_code As String, ByVal Item_Code As String, ByVal ShowHavingAvailBalanceOnly As Boolean, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        If clsCommon.myLen(whrcls.Trim) = 0 Then
            whrcls = " TSPL_LOCATION_MASTER.Location_Type='Physical'  "
        End If

        If ShowHavingAvailBalanceOnly Then
            whrcls = whrcls & " and len(Code)>0 and Balance>0"
        Else
        End If
        Dim qry As String = " select TSPL_LOCATION_MASTER.Location_Code as Code,TSPL_LOCATION_MASTER.Location_Desc as Name,TSPL_LOCATION_MASTER.Is_Section as [Is Section], " & _
                            " TSPL_LOCATION_MASTER.Is_Sub_Location as [Is Sub Location],TSPL_LOCATION_MASTER.Section_Code as [Section Code],TSPL_LOCATION_MASTER.Main_Location_Code as [Main Location],finder.Balance " & _
                            " from TSPL_LOCATION_MASTER left join ( select Location_Code as Code,sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed, " & _
                            " sum((case when  inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT   where Item_Code='" & Item_Code & "' " & _
                            " group by Location_Code,Stock_UOM  union all  select Location_Code,sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed, " & _
                            " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" & Item_Code & "' " & _
                            " group by Location_Code,Stock_UOM) as finder on finder.Code=TSPL_LOCATION_MASTER.Location_Code  "

        str = clsCommon.ShowSelectForm("LocationFinder", qry, "Code", whrcls, Curr_code, "", isButtonClicked)

        Return str
    End Function

    Public Shared Function GetBatchConsumptionSection(ByVal Batch_Loc As String, ByVal Batch_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Try
            Dim qry As String = "SELECT Location_Code FROM TSPL_LOCATION_MASTER WHERE Main_Location_Code='" & Batch_Loc & "' AND Is_Section='Y' AND Is_Consumption_Location=1 and Section_Code in (select distinct Section_Code from TSPL_PP_BATCH_ORDER_BOM_DETAIL where batch_code='" & Batch_Code & "')"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetSectionConsumptionSection(ByVal Location_Code As String, ByVal Section_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Try
            Dim qry As String = "SELECT Location_Code FROM TSPL_LOCATION_MASTER WHERE Main_Location_Code='" & Location_Code & "' AND Is_Section='Y' AND Is_Consumption_Location=1 and Section_Code='" & Section_Code & "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function UnpostData(ByVal strCode As String, ByVal FormId As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim issaved As Boolean = True
            issaved = issaved AndAlso UnpostData(strCode, FormId, trans)
            trans.Commit()
            Return issaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function UnpostData(ByVal strCode As String, ByVal FormId As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select QC_Date , LOCATION_CODE from TSPL_PE_FINALQC_HEAD where QC_Code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProductionEntryFinalQC, clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE")), clsCommon.myCDate(dt.Rows(0)("QC_Date")), trans)

            End If
            Dim issaved As Boolean = True
            Dim qry As String
            HistoryUpdate(strCode, trans)
            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where  Source_Doc_No='" + strCode + "' and Source_Code='PE-FQ'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            qry = "update tspl_batch_Item set Against_Inv_Movement_Trans_Id=null where Document_Code='" & strCode & "'"
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from tspl_inventory_movement where trans_type='" + FormId + "' and source_doc_no='" + strCode + "'"
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from tspl_inventory_movement_new where trans_type='" + FormId + "' and source_doc_no='" + strCode + "'"
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "DELETE FROM TSPL_PE_FINALQC_CONSUMPTION WHERE QC_Code='" & strCode & "'"
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "update TSPL_PE_FINALQC_HEAD set Posted='0',Modified_By='" + objCommonVar.CurrentUserCode + "',Modified_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' where QC_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetSectionStock(ByVal Section_Loc As String, Optional ByVal Item_Code As String = "", Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Try
            Dim Cond1 As String = "Location_Code='" & Section_Loc & "'"
            If clsCommon.myLen(Item_Code) > 0 Then
                Cond1 = Cond1 & " and TSPL_INVENTORY_MOVEMENT.Item_Code='" & Item_Code & "' "
            End If
            Dim Cond2 As String = "Location_Code='" & Section_Loc & "'"
            If clsCommon.myLen(Item_Code) > 0 Then
                Cond2 = Cond2 & " and TSPL_INVENTORY_MOVEMENT_NEW.Item_Code='" & Item_Code & "' "
            End If
            Dim qry As String = " select TSPL_INVENTORY_MOVEMENT.Item_Code as [Item Code],TSPL_ITEM_MASTER.Item_Desc as [Item Description],sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed," & _
                           " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,TSPL_INVENTORY_MOVEMENT.Stock_UOM from TSPL_INVENTORY_MOVEMENT  " & _
                           " left join TSPL_ITEM_MASTER on TSPL_INVENTORY_MOVEMENT.Item_Code=TSPL_ITEM_MASTER.Item_Code  " & _
                           " where " & Cond1 & "  group by TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_INVENTORY_MOVEMENT.Stock_UOM " & _
                           " union all " & _
                           " select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,TSPL_ITEM_MASTER.Item_Desc,sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed, " & _
                           " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM from TSPL_INVENTORY_MOVEMENT_NEW " & _
                           " left join TSPL_ITEM_MASTER on TSPL_INVENTORY_MOVEMENT_NEW.Item_Code=TSPL_ITEM_MASTER.Item_Code  " & _
                           " where " & Cond2 & " group by TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetSectionStockHistory(ByVal Section_Loc As String, ByVal Item_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Try
            Dim qry As String = "select * from ( select TSPL_INVENTORY_MOVEMENT.Item_Code as [Item Code],TSPL_ITEM_MASTER.Item_Desc as [Item Description],Trans_Type as [Transaction Type],Source_Doc_No as [Doc No],Source_Doc_Date as [Doc Date],(case when inout='I'  then Stock_Qty else 0 end) as Received,(case when inout='O'  then Stock_Qty else 0 end) as Consumed," & _
                           " ((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,TSPL_INVENTORY_MOVEMENT.Stock_UOM from TSPL_INVENTORY_MOVEMENT  " & _
                           " left join TSPL_ITEM_MASTER on TSPL_INVENTORY_MOVEMENT.Item_Code=TSPL_ITEM_MASTER.Item_Code  " & _
                           " where Location_Code='" & Section_Loc & "' and TSPL_INVENTORY_MOVEMENT.Item_Code='" & Item_Code & "' " & _
                           " union all " & _
                           " select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,TSPL_ITEM_MASTER.Item_Desc,Trans_Type as [Transaction Type],Source_Doc_No as [Doc No],Source_Doc_Date as [Doc Date],(case when inout='I'  then Stock_Qty else 0 end) as Received,(case when inout='O'  then Stock_Qty else 0 end) as Consumed, " & _
                           " ((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM from TSPL_INVENTORY_MOVEMENT_NEW " & _
                           " left join TSPL_ITEM_MASTER on TSPL_INVENTORY_MOVEMENT_NEW.Item_Code=TSPL_ITEM_MASTER.Item_Code  " & _
                           " where Location_Code='" & Section_Loc & "' and TSPL_INVENTORY_MOVEMENT_NEW.Item_Code='" & Item_Code & "' ) as SectionHist order by [Item Code],convert(date,[Doc Date],103) "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function JournalEntry(ByVal trans As SqlTransaction, ByVal obj As clsProductionEntryFinalQCHead, Optional ByVal strVourcherNoForRecreateOnly As String = "") As Boolean
        Try
            Dim VoucherNo As String
            If clsCommon.myLen(strVourcherNoForRecreateOnly) > 0 Then
                VoucherNo = strVourcherNoForRecreateOnly
            Else
                VoucherNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PE-FQ' and Source_Doc_No='" & obj.QC_Code & "'", trans))
            End If
            If obj.Is_Job_Work_Inward Then
                If clsCommon.myLen(VoucherNo) > 0 Then
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_JOURNAL_DETAILS where Voucher_No='" + VoucherNo + "' ", trans)
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_JOURNAL_MASTER where Voucher_No='" + VoucherNo + "' ", trans)
                End If
                Return True  ''Journal Entry will not create is job work type.
            End If

            Dim Count As Integer = 0
            Dim qry As String
            Dim dtGL As DataTable
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim VoucherDesc As String = "Financial Entry for Production Entry -" & obj.QC_Code & " "
            Dim SourceDocDesc As String = obj.DESCRIPTION
            Dim SourceDocNo As String = obj.QC_Code
            Dim Comments As String = obj.COMMENTS
            Dim Remarks As String = obj.DESCRIPTION
            Dim i As Integer = 0
            Dim dclPLAmt As Decimal = 0
            '' credit wip account of consumption items
            qry = " SELECT Consm.CONSM_ITEM_CODE,Consm.Avg_Cost,TSPL_ITEM_MASTER.Item_Desc,TSPL_PURCHASE_ACCOUNTS.WIP_Account AS CreditAccount " & _
                  " FROM TSPL_PE_FINALQC_CONSUMPTION  Consm " & _
                  " left join TSPL_ITEM_MASTER on Consm.CONSM_ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " & _
                  " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " & _
                  " WHERE Consm.QC_Code='" & obj.QC_Code & "'"

            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            For Each grow As DataRow In dtGL.Rows
                '' check for account setting  exist or not
                If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
                    Throw New Exception("WIP Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
                End If
                Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.LOCATION_CODE, trans)
                If clsCommon.myLen(CreditAcc) > 0 Then
                    Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
                    ArryLstGLAC.Add(Acc2)
                    dclPLAmt += -1 * clsCommon.myCdbl(grow("Avg_Cost"))
                End If
            Next
            qry = " select FE.Item_Code as CONSM_ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_PURCHASE_ACCOUNTS.WIP_Account as CreditAccount,FE.Cost as Avg_Cost from ( " & _
               " select TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code,((case when TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then  TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Fat_Amt  else -TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Fat_Amt end) " & _
              " +(case when TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then  TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.SNF_Amt  else -TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.SNF_Amt end)) as Cost from TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL " & _
              " inner join TSPL_PP_STAGE_PROCESS_HEAD on TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE=TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE " & _
              " inner join   TSPL_PE_FINALQC_HEAD on TSPL_PE_FINALQC_HEAD.Batch_Code=TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code " & _
              " where TSPL_PE_FINALQC_HEAD.QC_Code='" & obj.QC_Code & "'  and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code not in (select CONSM_ITEM_CODE from TSPL_PE_FINALQC_CONSUMPTION where QC_Code='" & obj.QC_Code & "')) as FE " & _
              " left join TSPL_ITEM_MASTER on FE.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
              " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code "
            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            For Each grow As DataRow In dtGL.Rows
                '' check for account setting  exist or not
                If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
                    Throw New Exception("WIP Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
                End If
                Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.LOCATION_CODE, trans)
                If clsCommon.myLen(CreditAcc) > 0 Then
                    Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
                    ArryLstGLAC.Add(Acc2)
                    dclPLAmt += -1 * clsCommon.myCdbl(grow("Avg_Cost"))
                End If
            Next



            qry = " select FE.Item_Code as CONSM_ITEM_CODE,TSPL_ITEM_MASTER.Product_Type,TSPL_ITEM_MASTER.Item_Desc,TSPL_PURCHASE_ACCOUNTS.WIP_Account as CreditAccount,FE.Cost as Avg_Cost,FE .transType,FE.location_code AS Location_Code,case when isnull(FE .transType,'')='Back' then  TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account when isnull(FE .transType,'')='Wreckage' then  TSPL_PURCHASE_ACCOUNTS.Wrekage_Account end as DebitAccount from ( " & _
             " select TSPL_PE_FINALQC_WRECKAGE_FLUSHING_ITEM.Item_Code,(Fat_Amt+SNF_Amt) as Cost,case when BACK_QTY>0 then 'Back' when WRECKAGE_QTY >0 then 'Wreckage' end as transType,TSPL_PE_FINALQC_WRECKAGE_FLUSHING_ITEM.location_code from TSPL_PE_FINALQC_WRECKAGE_FLUSHING_ITEM where QC_Code='" & obj.QC_Code & "' " & _
                         " ) as FE " & _
             " left join TSPL_ITEM_MASTER on FE.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
             " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code "

            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            For Each grow As DataRow In dtGL.Rows
                '' check for account setting  exist or not
                If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
                    Throw New Exception("WIP Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
                End If
                Dim AVG_COST As Double = 0
                If clsCommon.CompairString(grow.Item("transType"), "Back") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(clsCommon.myCstr(grow.Item("Product_Type")), "MI") = CompairStringResult.Equal Then
                        AVG_COST = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" sELECT Avg_Cost FROM TSPL_INVENTORY_MOVEMENT_NEW WHERE Source_Doc_No ='" & obj.QC_Code & "' AND Item_Code ='" & clsCommon.myCstr(grow.Item("CONSM_ITEM_CODE")) & "' and tspl_inventory_movement_new.Location_Code ='" & clsCommon.myCstr(grow.Item("Location_Code")) & "' ", trans))
                    Else
                        AVG_COST = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" sELECT Avg_Cost FROM TSPL_INVENTORY_MOVEMENT WHERE Source_Doc_No ='" & obj.QC_Code & "' AND Item_Code ='" & clsCommon.myCstr(grow.Item("CONSM_ITEM_CODE")) & "' and tspl_inventory_movement.Location_Code ='" & clsCommon.myCstr(grow.Item("Location_Code")) & "' ", trans))
                    End If
                Else
                    If clsCommon.CompairString(clsCommon.myCstr(grow.Item("Product_Type")), "MI") = CompairStringResult.Equal Then
                        AVG_COST = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" sELECT Avg_Cost FROM TSPL_INVENTORY_MOVEMENT_NEW WHERE Source_Doc_No ='" & obj.QC_Code & "' AND Item_Code ='" & clsCommon.myCstr(grow.Item("CONSM_ITEM_CODE")) & "' and tspl_inventory_movement_new.Location_Code ='" & clsCommon.myCstr(obj.CONSM_LOCATION_CODE) & "' ", trans))
                    Else
                        AVG_COST = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" sELECT Avg_Cost FROM TSPL_INVENTORY_MOVEMENT WHERE Source_Doc_No ='" & obj.QC_Code & "' AND Item_Code ='" & clsCommon.myCstr(grow.Item("CONSM_ITEM_CODE")) & "' and tspl_inventory_movement.Location_Code ='" & clsCommon.myCstr(obj.CONSM_LOCATION_CODE) & "' ", trans))
                    End If
                End If

                Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.LOCATION_CODE, trans)
                If clsCommon.myLen(CreditAcc) > 0 Then
                    Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(AVG_COST)}
                    ArryLstGLAC.Add(Acc2)
                    dclPLAmt += -1 * clsCommon.myCdbl(AVG_COST)
                End If
                If clsCommon.myLen(grow.Item("DebitAccount")) <= 0 Then
                    If clsCommon.CompairString(grow.Item("transType"), "Back") = CompairStringResult.Equal Then
                        Throw New Exception("Inventory Control A/c not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
                    Else
                        Throw New Exception("Wreckage Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
                    End If
                End If

                Dim DebitAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("DebitAccount")), obj.LOCATION_CODE, trans)
                If clsCommon.myLen(DebitAcc) > 0 Then
                    Dim Acc3() As String = {DebitAcc, 1 * clsCommon.myCdbl(AVG_COST)}
                    ArryLstGLAC.Add(Acc3)
                    dclPLAmt += 1 * clsCommon.myCdbl(AVG_COST)
                End If
            Next

            '' for scrap detail items

            qry = " Select  TSPL_PE_FINALQC_SCRAP_ITEM.Item_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PE_FINALQC_SCRAP_ITEM.SCRAP_QTY as ScrapQty,TSPL_ITEM_MASTER.Product_Type,TSPL_PE_FINALQC_SCRAP_ITEM.Location_Code  from TSPL_PE_FINALQC_SCRAP_ITEM " & _
                 " left outer join TSPL_ITEM_MASTER on TSPL_PE_FINALQC_SCRAP_ITEM.Item_Code =TSPL_ITEM_MASTER.Item_Code " & _
                 " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " & _
                      " where TSPL_PE_FINALQC_SCRAP_ITEM.QC_Code ='" & obj.QC_Code & "'"
            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            For Each grow As DataRow In dtGL.Rows
                If clsCommon.myCdbl(grow.Item("ScrapQty")) > 0 Then
                    If clsCommon.myLen(grow.Item("Inv_Control_Account")) <= 0 Then
                        Throw New Exception("Inventory Control Account not found for Item " & clsCommon.myCstr(grow.Item("Item_Code")) & "")
                    End If

                    Dim strMainItemWreckageAcc As String = String.Empty
                    If clsCommon.myCdbl(grow.Item("ScrapQty")) > 0 Then
                        Dim strMainItemOfScrapItem As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Scrap_Item_Code from TSPL_ITEM_MASTER where Item_Code ='" & clsCommon.myCstr(grow.Item("Item_Code")) & "' and Is_Scrap_Item ='1'", trans))
                        If clsCommon.myLen(strMainItemOfScrapItem) > 0 Then
                            strMainItemWreckageAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select TSPL_PURCHASE_ACCOUNTS.Wrekage_Account from TSPL_PURCHASE_ACCOUNTS left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code where TSPL_ITEM_MASTER.Item_Code in (Select Scrap_Item_Code from TSPL_ITEM_MASTER where Item_Code ='" & clsCommon.myCstr(grow.Item("Item_Code")) & "' and Is_Scrap_Item ='1')", trans))
                            If clsCommon.myLen(strMainItemWreckageAcc) <= 0 Then
                                Throw New Exception("Wreckage Control not found for Item " & clsCommon.myCstr(grow.Item("Item_Code")) & "")
                            End If
                        Else
                            Throw New Exception("Please Map Main item for scrap Item " & clsCommon.myCstr(grow.Item("Item_Code")) & " in Item Master")
                        End If

                    End If
                    Dim AVG_COST As Double = 0
                    If clsCommon.myCdbl(grow.Item("ScrapQty")) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Item("Product_Type")), "MI") = CompairStringResult.Equal Then
                            AVG_COST = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" sELECT Avg_Cost FROM TSPL_INVENTORY_MOVEMENT_NEW WHERE Source_Doc_No ='" & obj.QC_Code & "' AND Item_Code ='" & clsCommon.myCstr(grow.Item("Item_Code")) & "' and tspl_inventory_movement_new.Location_Code ='" & clsCommon.myCstr(grow.Item("Location_Code")) & "' ", trans))
                        Else
                            AVG_COST = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" sELECT Avg_Cost FROM TSPL_INVENTORY_MOVEMENT WHERE Source_Doc_No ='" & obj.QC_Code & "' AND Item_Code ='" & clsCommon.myCstr(grow.Item("Item_Code")) & "' and tspl_inventory_movement.Location_Code ='" & clsCommon.myCstr(grow.Item("Location_Code")) & "' ", trans))
                        End If
                    End If

                    Dim CreditAcc As String = String.Empty
                    CreditAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(strMainItemWreckageAcc), obj.LOCATION_CODE, trans)
                    If clsCommon.myLen(CreditAcc) > 0 Then
                        Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(AVG_COST)}
                        ArryLstGLAC.Add(Acc2)
                        dclPLAmt += -1 * clsCommon.myCdbl(AVG_COST)
                    End If

                    Dim DebitAcc As String = String.Empty
                    DebitAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("Inv_Control_Account")), obj.LOCATION_CODE, trans)
                    If clsCommon.myLen(DebitAcc) > 0 Then
                        Dim Acc2() As String = {DebitAcc, 1 * clsCommon.myCdbl(AVG_COST)}
                        ArryLstGLAC.Add(Acc2)
                        dclPLAmt += 1 * clsCommon.myCdbl(AVG_COST)
                    End If
                End If

            Next

            '''''''' end of richa work


            '' credit wip account of overhead cost
            qry = " select Cost.COST_CODE,Cost.OverHead_Cost as Avg_Cost,TSPL_OVERHEAD_COST.GL_Acc as CreditAccount from TSPL_PP_COST_WITHOUT_BATCH Cost " & _
                  " left join TSPL_OVERHEAD_COST on Cost.COST_CODE=TSPL_OVERHEAD_COST.COST_CODE " & _
                  " WHERE Cost.PROD_ENTRY_CODE in ( select Against_PE_Code from TSPL_PE_FINALQC_HEAD where QC_Code='" & obj.QC_Code & "')"
            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            For Each grow As DataRow In dtGL.Rows
                '' check for account setting  exist or not
                If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
                    Throw New Exception("GL Account not found for Cost Code " & grow.Item("COST_CODE") & "")
                End If
                Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.LOCATION_CODE, trans)
                If clsCommon.myLen(CreditAcc) > 0 Then
                    Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
                    ArryLstGLAC.Add(Acc2)
                    dclPLAmt += -1 * clsCommon.myCdbl(grow("Avg_Cost"))
                End If
            Next

            '' credit wip account of production items
            qry = "Select TSPL_PE_FINALQC_DETAIL.ITEM_CODE,TSPL_PE_FINALQC_DETAIL.UNIT_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_PE_FINALQC_DETAIL.Avg_Cost,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account as DebitAccount" + Environment.NewLine + _
            ",TSPL_PE_FINALQC_DETAIL.FINAL_PRODUCTION_QTY*isnull(TSPL_ITEM_UOM_DETAIL.Item_Cost,0) as ProductCost,TSPL_ITEM_UOM_DETAIL.Item_Cost,TSPL_PURCHASE_ACCOUNTS.Loss_Ac,TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code" + Environment.NewLine + _
"            from TSPL_PE_FINALQC_DETAIL" + Environment.NewLine + _
            "left join TSPL_ITEM_MASTER on TSPL_PE_FINALQC_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code  " + Environment.NewLine + _
            "left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code" + Environment.NewLine + _
            "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_PE_FINALQC_DETAIL.ITEM_CODE and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_PE_FINALQC_DETAIL.UNIT_CODE  " + Environment.NewLine + _
            "WHERE TSPL_PE_FINALQC_DETAIL.QC_Code='" & obj.QC_Code & "'"

            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            ''BHA/13/08/18-000420 by balwinder on 13/08/2018
            Dim settPickProductCostFromItemUOMDetail As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0)
            For Each grow As DataRow In dtGL.Rows
                Dim AccountCode As String = clsCommon.myCstr(grow.Item("DebitAccount"))
                If clsCommon.myLen(AccountCode) <= 0 Then
                    Throw New Exception("Inventory Control account not found purchase Account set-" + clsCommon.myCstr(grow.Item("Purchase_Class_Code")) + "  for Item Code " & grow.Item("ITEM_CODE") & "")
                End If
                AccountCode = clsERPFuncationality.ChangeGLAccountLocationSegment(AccountCode, obj.LOCATION_CODE, trans)
                If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                    Dim Acc2() As String = {AccountCode, clsCommon.myCdbl(grow("Avg_Cost"))}
                    ArryLstGLAC.Add(Acc2)
                Else
                    Dim Acc2() As String = {AccountCode, clsCommon.myCdbl(grow("Avg_Cost")), "", "", "", "", "", "", "I"}
                    ArryLstGLAC.Add(Acc2)
                    'Update inventory account
                    clsInventoryMovement.UpdateInvControlAccount(clsCommon.myCstr(obj.QC_Code), clsUserMgtCode.frmProductionEntryFinalQC, clsCommon.myCstr(grow.Item("ITEM_CODE")), AccountCode, "", "I", trans)
                End If
                dclPLAmt += clsCommon.myCdbl(grow("Avg_Cost"))

                If settPickProductCostFromItemUOMDetail Then
                    If clsCommon.myCdbl(grow("Item_Cost")) <= 0 Then
                        Throw New Exception("Please provide item cost in item master's item Uom detail for Item [" + clsCommon.myCstr(grow.Item("ITEM_CODE")) + "] and UOM[" + clsCommon.myCstr(grow.Item("UNIT_CODE")) + "].")
                    End If
                    Dim ACCCode As String = clsCommon.myCstr(grow.Item("Loss_Ac"))
                    If clsCommon.myLen(ACCCode) <= 0 Then
                        Throw New Exception("Gain/Loss account not found purchase Account set-" + clsCommon.myCstr(grow.Item("Purchase_Class_Code")) + " for Item Code " & grow.Item("ITEM_CODE") & "")
                    End If
                    ACCCode = clsERPFuncationality.ChangeGLAccountLocationSegment(ACCCode, obj.LOCATION_CODE, trans)

                    Dim Acc4() As String = {ACCCode, -1 * dclPLAmt} ''It should be last account 
                    ArryLstGLAC.Add(Acc4)
                End If

            Next

            Dim GLDesc As String = "Journal Entry Against Production Entry- Doc No." & obj.QC_Code & " "
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsJournalMaster.FunGrnlEntryWithTrans(obj.LOCATION_CODE, False, VoucherNo, trans, obj.QC_Date, GLDesc, "PE-FQ", "Production Entry Final QC", obj.QC_Code, obj.DESCRIPTION, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, GLDesc, "")
            Else
                clsJournalMaster.FunGrnlEntryWithTrans(obj.LOCATION_CODE, False, trans, obj.QC_Date, GLDesc, "PE-FQ", "Production Entry Final QC", obj.QC_Code, obj.DESCRIPTION, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , GLDesc, "")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetPendingBatchQty(ByVal batch_Code As String, ByVal Prod_Code As String, ByVal Item_Code As String, ByVal trans As SqlTransaction) As Decimal
        Dim qry As String = ""
        qry = " select sum(Quantity) as Pending_Batch_Qty from (" & _
              " select Quantity from TSPL_PP_BATCH_ORDER_BOM_DETAIL where Batch_Code='" & batch_Code & "' and Item_Code='" & Item_Code & "'" & _
              " union all " & _
              " select sum(-TSPL_PE_FINALQC_DETAIL.FINAL_PRODUCTION_QTY) as Produced_Qty from TSPL_PE_FINALQC_HEAD left join TSPL_PE_FINALQC_DETAIL " & _
              " on TSPL_PE_FINALQC_HEAD.QC_Code=TSPL_PE_FINALQC_DETAIL.QC_Code  " & _
              " where Batch_Code='" & batch_Code & "' and TSPL_PE_FINALQC_HEAD.QC_Code not in ('" & Prod_Code & "') " & _
              " and Item_Code='" & Item_Code & "') as t1"
        Dim Qty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return Qty
    End Function

    Public Shared Function GetPrevProductionQty(ByVal batch_Code As String, ByVal Prod_Code As String, ByVal Item_Code As String, ByVal trans As SqlTransaction) As Decimal
        Dim qry As String  = " select sum(TSPL_PE_FINALQC_DETAIL.FINAL_PRODUCTION_QTY) as Produced_Qty " + Environment.NewLine + _
        " from TSPL_PE_FINALQC_HEAD" + Environment.NewLine + _
        " left outer join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_PE_FINALQC_HEAD.Against_PE_Code" + Environment.NewLine + _
        " left join TSPL_PP_PRODUCTION_RETURN  on TSPL_PE_FINALQC_HEAD.QC_Code=TSPL_PP_PRODUCTION_RETURN.PROD_ENTRY_CODE " + Environment.NewLine + _
        " left join TSPL_PE_FINALQC_DETAIL  on TSPL_PE_FINALQC_HEAD.QC_Code=TSPL_PE_FINALQC_DETAIL.QC_Code" & _
        " where TSPL_PE_FINALQC_HEAD.Batch_Code='" & batch_Code & "' and TSPL_PE_FINALQC_HEAD.QC_Code not in ('" & Prod_Code & "') and  TSPL_PP_PRODUCTION_RETURN.PROD_RETURN_CODE is null  " & _
        " and TSPL_PE_FINALQC_DETAIL.Item_Code='" & Item_Code & "'"
        Dim Qty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return Qty
    End Function

    Public Shared Function UpdateConsumption(ByVal ReceiptCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            UpdateConsumption(ReceiptCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function UpdateConsumption(ByVal ReceiptCode As String, ByVal trans As SqlTransaction) As Boolean
        If clsCommon.myLen(ReceiptCode) <= 0 Then
            Throw New Exception("Document No can not be blank")
        End If
        Dim objRec As clsProductionEntryFinalQCHead
        objRec = clsProductionEntryFinalQCHead.GetData(ReceiptCode, "", NavigatorType.Current, trans)
        If objRec Is Nothing Then
            Throw New Exception("Document not found")
        End If
        If clsCommon.myLen(objRec.QC_Code) <= 0 Then
            Throw New Exception("Document not found")
        End If
        Dim isSaved As Boolean = True

        Try
            '' query for consumption on batch order bom basis
            Dim qry As String = "delete from TSPL_INVENTORY_MOVEMENT_NEW where SOURCE_DOC_NO='" & objRec.QC_Code & "' and IS_CONSUMPTION=1"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_INVENTORY_MOVEMENT where SOURCE_DOC_NO='" & objRec.QC_Code & "' and IS_CONSUMPTION=1"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PE_FINALQC_CONSUMPTION where QC_Code='" & objRec.QC_Code & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' update consumption
            isSaved = isSaved AndAlso clsProductionEntryFinalQCConsumption.SaveRM(objRec.QC_Code, Nothing, trans)
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim ArrInventoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            isSaved = isSaved AndAlso clsProductionEntryFinalQCConsumption.UpdateInventoryMovementConsumption("PROD_ENTRY", ArrInventoryMovement, ArrInventoryMovementNew, objRec, Nothing, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function

End Class

Public Class clsProductionEntryFinalQCDetail
#Region "Variables"
    Public QC_Code As String
    Public Shift_Code As String
    Public Section_Code As String
    Public BOM_CODE As String
    Public ITEM_CODE As String
    Public ITEM_DESCRIPTION As String
    Public BATCH_QTY As Decimal
    Public UNIT_CODE As String
    Public RECEIPT_QTY As Decimal
    Public REJ_HEAD As String
    Public REJ_QTY As Decimal
    Public BREAKAGE_HEAD As String
    Public BREAKAGE_QTY As Decimal
    Public LAB_TESTING As String
    Public FINAL_PRODUCTION_QTY As Decimal = 0
    Public LOCATION_CODE As String
    Public START_TIME As DateTime? = Nothing
    Public END_TIME As DateTime? = Nothing
    Public MFG_DATE As Date
    Public EXP_DATE As Date
    Public FIFO_Cost As Decimal
    Public LIFO_Cost As Decimal
    Public AVG_Cost As Decimal
    Public Costing_Method As Integer
    Public FAT_Per As Decimal
    Public SNF_Per As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
    Public Fat_Rate As Decimal = 0
    Public SNF_Rate As Decimal = 0
    Public Fat_Amt As Decimal = 0
    Public SNF_Amt As Decimal = 0
    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
#End Region

    Public Shared Function SaveDetailData(ByVal strDocNo As String, ByVal objRec As clsProductionEntryFinalQCHead, ByVal Arr As List(Of clsProductionEntryFinalQCDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "DELETE FROM TSPL_PE_FINALQC_DETAIL WHERE QC_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsProductionEntryFinalQCDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "QC_Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Shift_Code", obj.Shift_Code)
                    clsCommon.AddColumnsForChange(coll, "Section_Code", obj.Section_Code)
                    clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE)
                    clsCommon.AddColumnsForChange(coll, "ITEM_CODE", obj.ITEM_CODE)
                    clsCommon.AddColumnsForChange(coll, "ITEM_DESCRIPTION", obj.ITEM_DESCRIPTION)
                    clsCommon.AddColumnsForChange(coll, "BATCH_QTY", obj.BATCH_QTY)
                    clsCommon.AddColumnsForChange(coll, "RECEIPT_QTY", obj.FINAL_PRODUCTION_QTY)
                    clsCommon.AddColumnsForChange(coll, "REJ_HEAD", obj.REJ_HEAD)
                    clsCommon.AddColumnsForChange(coll, "REJ_QTY", obj.REJ_QTY)
                    clsCommon.AddColumnsForChange(coll, "BREAKAGE_HEAD", obj.BREAKAGE_HEAD)
                    clsCommon.AddColumnsForChange(coll, "BREAKAGE_QTY", obj.BREAKAGE_QTY)
                    clsCommon.AddColumnsForChange(coll, "LAB_TESTING", obj.LAB_TESTING)
                    clsCommon.AddColumnsForChange(coll, "FINAL_PRODUCTION_QTY", obj.FINAL_PRODUCTION_QTY)
                    clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE, True)
                    clsCommon.AddColumnsForChange(coll, "UNIT_CODE", obj.UNIT_CODE)
                    clsCommon.AddColumnsForChange(coll, "MFG_DATE", clsCommon.GetPrintDate(obj.MFG_DATE, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "EXP_DATE", clsCommon.GetPrintDate(obj.EXP_DATE, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "FAT_Per", obj.FAT_Per)
                    clsCommon.AddColumnsForChange(coll, "SNF_Per", obj.SNF_Per)
                    clsCommon.AddColumnsForChange(coll, "FAT_KG", obj.FAT_KG)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PE_FINALQC_DETAIL", OMInsertOrUpdate.Insert, "TSPL_PE_FINALQC_DETAIL.QC_Code='" + strDocNo + "' ", trans)
                    clsSerializeInvenotry.SaveData("Production", strDocNo, objRec.QC_Date, "I", obj.ITEM_CODE, objRec.LOCATION_CODE, (Arr.IndexOf(obj) + 1), obj.arrSrItem, trans)
                    If clsItemMaster.IsBatchItem(obj.ITEM_CODE, trans) = True Then
                        obj.arrBatchItem = New List(Of clsBatchInventory)
                        Dim objBatchInv As clsBatchInventory = New clsBatchInventory()
                        objBatchInv.arr = New List(Of clsBatchInventory)
                        objBatchInv.Batch_No = objRec.Batch_Code
                        objBatchInv.Manufacture_Date = obj.MFG_DATE
                        objBatchInv.Expiry_Date = obj.EXP_DATE
                        objBatchInv.Qty = obj.FINAL_PRODUCTION_QTY
                        objBatchInv.Manual_BatchNo = objRec.ManualBatchNo
                        If clsCommon.myLen(objBatchInv.Batch_No) > 0 AndAlso objBatchInv.Qty <> 0 Then
                            objBatchInv.arr.Add(objBatchInv)
                        End If
                        obj.arrBatchItem.Add(objBatchInv)
                        clsBatchInventory.SaveData(clsUserMgtCode.frmProductionEntryFinalQC, strDocNo, objRec.QC_Date, "I", obj.ITEM_CODE, objRec.LOCATION_CODE, Arr.IndexOf(obj) + 1, 0, obj.UNIT_CODE, obj.arrBatchItem, trans)
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Shared Function GetProductionEntryDetail(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsProductionEntryFinalQCDetail)
        Dim qry As String
        qry = "SELECT T1.*,coalesce(TSPL_PURCHASE_ACCOUNTS.Costing_Method,0) as Costing_Method FROM  TSPL_PE_FINALQC_DETAIL T1 " & _
        " left join TSPL_ITEM_MASTER on T1.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " & _
        " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code WHERE 2=2 " & _
        " AND T1.QC_Code = '" + strCode + "' ORDER BY T1.ITEM_CODE"

        Dim objtr As New clsProductionEntryFinalQCDetail
        Dim ObjList As New List(Of clsProductionEntryFinalQCDetail)
        Dim dt As New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsProductionEntryFinalQCDetail()
                objtr.Shift_Code = clsCommon.myCstr(dr("Shift_Code"))
                objtr.Section_Code = clsCommon.myCstr(dr("Section_Code"))
                objtr.BOM_CODE = clsCommon.myCstr(dr("BOM_CODE"))

                objtr.ITEM_CODE = clsCommon.myCstr(dr("ITEM_CODE"))
                objtr.ITEM_DESCRIPTION = clsCommon.myCstr(dr("ITEM_DESCRIPTION"))
                objtr.BATCH_QTY = clsCommon.myCstr(dr("BATCH_QTY"))
                objtr.RECEIPT_QTY = clsCommon.myCdbl(dr("RECEIPT_QTY"))
                objtr.REJ_HEAD = clsCommon.myCstr(dr("REJ_HEAD"))
                objtr.REJ_QTY = clsCommon.myCdbl(dr("REJ_QTY"))
                objtr.BREAKAGE_HEAD = clsCommon.myCstr(dr("BREAKAGE_HEAD"))
                objtr.BREAKAGE_QTY = clsCommon.myCdbl(dr("BREAKAGE_QTY"))
                objtr.LAB_TESTING = clsCommon.myCstr(dr("LAB_TESTING"))
                objtr.UNIT_CODE = clsCommon.myCstr(dr("UNIT_CODE"))
                objtr.FINAL_PRODUCTION_QTY = clsCommon.myCdbl(dr("FINAL_PRODUCTION_QTY"))
                objtr.LOCATION_CODE = clsCommon.myCstr(dr("LOCATION_CODE"))
                objtr.FIFO_Cost = clsCommon.myCdbl(dr("FIFO_Cost"))
                objtr.LIFO_Cost = clsCommon.myCdbl(dr("LIFO_Cost"))
                objtr.AVG_Cost = clsCommon.myCdbl(dr("AVG_Cost"))
                objtr.Costing_Method = clsCommon.myCdbl(dr("Costing_Method"))
                objtr.FAT_Per = clsCommon.myCdbl(dr("FAT_Per"))
                objtr.SNF_Per = clsCommon.myCdbl(dr("SNF_Per"))
                objtr.FAT_KG = clsCommon.myCdbl(dr("FAT_KG"))
                objtr.SNF_KG = clsCommon.myCdbl(dr("SNF_KG"))
                objtr.Fat_Rate = clsCommon.myCdbl(dr.Item("Fat_Rate"))
                objtr.Fat_Amt = clsCommon.myCdbl(dr.Item("Fat_Amt"))
                objtr.SNF_Rate = clsCommon.myCdbl(dr.Item("SNF_Rate"))
                objtr.SNF_Amt = clsCommon.myCdbl(dr.Item("SNF_Amt"))
                objtr.arrBatchItem = clsBatchInventory.GetData(clsUserMgtCode.frmProductionEntryFinalQC, objtr.QC_Code, objtr.ITEM_CODE, dt.Rows.IndexOf(dr) + 1, trans)
                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function
End Class

Public Class clsProductionEntryFinalQCConsumption
#Region "Variables"
    Dim QC_Code As String
    Dim CONSM_ITEM_CODE As String
    Dim CONSM_QTY As Decimal
    Dim LOCATION_CODE As String
    Dim UNIT_CODE As String
    Dim FIFO_COST As Decimal
    Dim LIFO_COST As Decimal
    Dim AVG_COST As Decimal
    Public FAT_Per As Decimal
    Public SNF_Per As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
    Public Fat_Rate As Decimal = 0
    Public SNF_Rate As Decimal = 0
    Public Fat_Amt As Decimal = 0
    Public SNF_Amt As Decimal = 0
#End Region

    Public Shared Function GetRM(ByVal ReceiptCode As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim qry As String = ""
        Dim MI_Consm_Type As String = clsFixedParameter.GetData(clsFixedParameterType.ConsumptionType, clsFixedParameterCode.ConsumptionTypeMilk, trans)
        Dim MP_Consm_Type As String = clsFixedParameter.GetData(clsFixedParameterType.ConsumptionType, clsFixedParameterCode.ConsumptionTypeMilkProduct, trans)
        Dim Othr_Consm_Type As String = clsFixedParameter.GetData(clsFixedParameterType.ConsumptionType, clsFixedParameterCode.ConsumptionTypeOther, trans)
        qry = " select Consumption.QC_Code,CONSM_LOCATION_CODE,CONSM_SECTION_CODE," & _
             " Consm_Item_Code,(case when TSPL_ITEM_MASTER.Product_Type='MI' then (case when  '" & MI_Consm_Type & "'='0' then TSPL_PE_FINALQC_ISSUE_ITEM.Unit_Code else Consumption.Consm_Unit_Code end) when TSPL_ITEM_MASTER.Product_Type='MP' then (case when '" & MP_Consm_Type & "'='0' then TSPL_PE_FINALQC_ISSUE_ITEM.Unit_Code else Consumption.Consm_Unit_Code end) else (case when '" & Othr_Consm_Type & "'='0' then TSPL_PE_FINALQC_ISSUE_ITEM.Unit_Code else Consumption.Consm_Unit_Code end) end) as Consm_Unit_Code, " & _
             " round((case when TSPL_ITEM_MASTER.Product_Type='MI' then (case when  '" & MI_Consm_Type & "'='0' then max(TSPL_PE_FINALQC_ISSUE_ITEM.Avail_Qty) else  sum(Consumption.CONSM_QTY) end) when TSPL_ITEM_MASTER.Product_Type='MP' then (case when '" & MP_Consm_Type & "'='0' then max(TSPL_PE_FINALQC_ISSUE_ITEM.Avail_Qty) else  sum(Consumption.CONSM_QTY) end) else  (case when '" & Othr_Consm_Type & "'='0' then max(TSPL_PE_FINALQC_ISSUE_ITEM.Avail_Qty) else  sum(Consumption.CONSM_QTY) end) end),2) as CONSM_QTY, " & _
             " round((case when TSPL_ITEM_MASTER.Product_Type='MI' then (case when  '" & MI_Consm_Type & "'='0' then max(TSPL_PE_FINALQC_ISSUE_ITEM.Avail_FAT_KG) " & _
             " else  sum(Consumption.Consm_Fat_Kg) end) when TSPL_ITEM_MASTER.Product_Type='MP' then (case when '" & MP_Consm_Type & "'='0' " & _
             " then max(TSPL_PE_FINALQC_ISSUE_ITEM.Avail_FAT_KG) else  sum(Consumption.Consm_Fat_Kg) end) else  0 end),3) as FAT_KG, " & _
             " round((case when TSPL_ITEM_MASTER.Product_Type='MI' then (case when  '" & MI_Consm_Type & "'='0' then max(TSPL_PE_FINALQC_ISSUE_ITEM.Avail_SNF_KG) " & _
             " else  sum(Consumption.Consm_SNF_Kg) end) when TSPL_ITEM_MASTER.Product_Type='MP' then (case when '" & MP_Consm_Type & "'='0' " & _
             " then max(TSPL_PE_FINALQC_ISSUE_ITEM.Avail_SNF_KG) else  sum(Consumption.Consm_SNF_Kg) end) else  0 end),3) as SNF_KG, " & _
             " (case when TSPL_ITEM_MASTER.Product_Type='MI' then (case when '" & MI_Consm_Type & "'='0' then (case when max(TSPL_PE_FINALQC_ISSUE_ITEM.Avail_Qty)=0 then 0 else round((max(TSPL_PE_FINALQC_ISSUE_ITEM.Avail_FAT_KG )/max(TSPL_PE_FINALQC_ISSUE_ITEM.Avail_Qty))*100,3) end) else max(Consumption.Consm_Fat_Per) end) when TSPL_ITEM_MASTER.Product_Type='MP' then (case when '" & MP_Consm_Type & "'='0' then (case when max(TSPL_PE_FINALQC_ISSUE_ITEM.Avail_Qty)=0 then 0 else round((max(TSPL_PE_FINALQC_ISSUE_ITEM.Avail_FAT_KG )/max(TSPL_PE_FINALQC_ISSUE_ITEM.Avail_Qty))*100,3) end) else max(Consumption.Consm_Fat_Per) end) else 0 end) as FAT_Pers," & _
             " (case when TSPL_ITEM_MASTER.Product_Type='MI' then (case when '" & MI_Consm_Type & "'='0' then (case when max(TSPL_PE_FINALQC_ISSUE_ITEM.Avail_Qty)=0 then 0 else round((max(TSPL_PE_FINALQC_ISSUE_ITEM.Avail_SNF_KG )/max(TSPL_PE_FINALQC_ISSUE_ITEM.Avail_Qty))*100,3) end) else max(Consumption.Consm_SNF_Per) end) when TSPL_ITEM_MASTER.Product_Type='MP' then (case when '" & MP_Consm_Type & "'='0' then (case when max(TSPL_PE_FINALQC_ISSUE_ITEM.Avail_Qty)=0 then 0 else round((max(TSPL_PE_FINALQC_ISSUE_ITEM.Avail_SNF_KG )/max(TSPL_PE_FINALQC_ISSUE_ITEM.Avail_Qty))*100,3) end) else max(Consumption.Consm_SNF_Per) end) else 0 end) as SNF_Pers, " & _
             " max(TSPL_PE_FINALQC_ISSUE_ITEM.Fat_Amt) as Fat_Amt,max(TSPL_PE_FINALQC_ISSUE_ITEM.SNF_Amt) as SNF_Amt  from ( " & _
             " select TSPL_PE_FINALQC_HEAD.QC_Code,TSPL_PE_FINALQC_DETAIL.BOM_CODE,TSPL_PE_FINALQC_DETAIL.ITEM_CODE, " & _
             " TSPL_PE_FINALQC_DETAIL.BATCH_QTY,TSPL_PE_FINALQC_DETAIL.UNIT_CODE,TSPL_PE_FINALQC_DETAIL.FINAL_PRODUCTION_QTY, " & _
             " TSPL_PE_FINALQC_DETAIL.LOCATION_CODE,CONSM_LOCATION_CODE, CONSM_SECTION_CODE,TSPL_PP_BOM_ITEM_DETAIL.Item_Code as Consm_Item_Code, " & _
             " TSPL_PP_BOM_ITEM_DETAIL.Unit_Code as Consm_Unit_Code, " & _
             " (TSPL_PP_BOM_ITEM_DETAIL.Quantity/(TSPL_PP_BOM_HEAD.PROD_QUANTITY*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code " & _
             " and UOM_Code=TSPL_PP_BOM_HEAD.Prod_Item_Unit_Code)))*TSPL_PE_FINALQC_DETAIL.FINAL_PRODUCTION_QTY*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_PE_FINALQC_DETAIL.Item_Code " & _
             " and UOM_Code=TSPL_PE_FINALQC_DETAIL.Unit_Code) as Consm_Qty, " & _
             " (TSPL_PP_BOM_ITEM_DETAIL.FAT_KG/(TSPL_PP_BOM_HEAD.PROD_QUANTITY*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code " & _
             " and UOM_Code=TSPL_PP_BOM_HEAD.Prod_Item_Unit_Code)))*TSPL_PE_FINALQC_DETAIL.FINAL_PRODUCTION_QTY*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_PE_FINALQC_DETAIL.Item_Code " & _
             " and UOM_Code=TSPL_PE_FINALQC_DETAIL.Unit_Code) as Consm_Fat_Kg," & _
             " (TSPL_PP_BOM_ITEM_DETAIL.SNF_KG/(TSPL_PP_BOM_HEAD.PROD_QUANTITY*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code " & _
             " and UOM_Code=TSPL_PP_BOM_HEAD.Prod_Item_Unit_Code)))*TSPL_PE_FINALQC_DETAIL.FINAL_PRODUCTION_QTY*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_PE_FINALQC_DETAIL.Item_Code " & _
             " and UOM_Code=TSPL_PE_FINALQC_DETAIL.Unit_Code) as Consm_SNF_Kg,TSPL_PP_BOM_ITEM_DETAIL.Fat as Consm_Fat_Per,TSPL_PP_BOM_ITEM_DETAIL.SNF as Consm_SNF_Per  " & _
             " from TSPL_PE_FINALQC_DETAIL inner join TSPL_PE_FINALQC_HEAD " & _
             " on TSPL_PE_FINALQC_DETAIL.QC_Code=TSPL_PE_FINALQC_HEAD.QC_Code " & _
             " left join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PE_FINALQC_HEAD.Batch_Code=TSPL_PP_BATCH_ORDER_HEAD.Batch_Code " & _
             " left join TSPL_PP_BATCH_ORDER_BOM_DETAIL on TSPL_PE_FINALQC_DETAIL.BOM_CODE=TSPL_PP_BATCH_ORDER_BOM_DETAIL.BOM_CODE " & _
             " and TSPL_PE_FINALQC_DETAIL.ITEM_CODE=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code " & _
             " and TSPL_PE_FINALQC_DETAIL.Unit_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Unit_Code  " & _
             " and TSPL_PE_FINALQC_HEAD.Batch_Code=TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code "
        qry += " left join (select 'xxx' as History_No,BOM_CODE, Item_Code,Unit_Code,Quantity,FAT_KG,SNF_KG,Fat,SNF from TSPL_PP_BOM_ITEM_DETAIL union all select History_No,BOM_CODE, Item_Code,Unit_Code,Quantity,FAT_KG,SNF_KG,Fat,SNF from TSPL_PP_BOM_ITEM_DETAIL_HISTORY) as TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BATCH_ORDER_BOM_DETAIL.BOM_CODE=TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE and TSPL_PE_FINALQC_DETAIL.BOM_Code =TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE      " + Environment.NewLine + _
            " inner join (select Revision_No, 'xxx' as History_No,BOM_CODE,PROD_QUANTITY,Prod_Item_Unit_Code from TSPL_PP_BOM_HEAD union all select Revision_No,History_No,BOM_CODE,PROD_QUANTITY,Prod_Item_Unit_Code from TSPL_PP_BOM_HEAD_HISTORY) as TSPL_PP_BOM_HEAD " + Environment.NewLine + _
            " on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TSPL_PP_BOM_HEAD.BOM_CODE and isnull( TSPL_PP_BOM_HEAD.Revision_no,'')=isnull( TSPL_PP_BATCH_ORDER_BOM_DETAIL.bom_revision_no,'') and TSPL_PP_BOM_HEAD.History_No=TSPL_PP_BOM_ITEM_DETAIL.History_No" + _
             " where TSPL_PE_FINALQC_HEAD.QC_Code='" & ReceiptCode & "'  " + Environment.NewLine + _
             " ) as Consumption " & _
             " left join (select QC_Code,Item_Code,Unit_Code,sum(Avail_Qty) as Avail_Qty,sum(Avail_FAT_KG) as Avail_FAT_KG,sum(Avail_SNF_KG) as Avail_SNF_KG," & _
             " (sum(Avail_FAT_KG)/(case when sum(Avail_Qty)=0 then 1 else sum(Avail_Qty) end))*100 as Avail_FAT_Per, " & _
             " (sum(Avail_SNF_KG)/(case when sum(Avail_Qty)=0 then 1 else sum(Avail_Qty) end))*100  as Avail_SNF_Per,sum(Fat_Amt) as Fat_Amt,sum(SNF_Amt) as SNF_Amt " & _
             " from ( " & _
             " select QC_Code,Item_Code,Unit_Code,Avail_Qty,Avail_FAT_Per,Avail_FAT_KG,Avail_SNF_Per,Avail_SNF_KG,Fat_Amt,SNF_Amt from TSPL_PE_FINALQC_ISSUE_ITEM " & _
             " where QC_Code='" & ReceiptCode & "' " & _
             " union all " & _
             " select QC_Code,Item_Code,Unit_Code,-BACK_QTY as BACK_QTY,Avail_FAT_Per,-Avail_FAT_KG as Avail_FAT_KG,Avail_SNF_Per,-Avail_SNF_KG as Avail_SNF_KG,-Fat_Amt,-SNF_Amt " & _
             " from TSPL_PE_FINALQC_WRECKAGE_FLUSHING_ITEM where BACK_QTY>0 and QC_Code='" & ReceiptCode & "' " & _
             " Union All " & _
             " select TSPL_PE_FINALQC_HEAD.QC_Code,TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code,TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Unit_Code," & _
             " (case when TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then  TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY else -TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY end) as AddRemove_Qty," & _
             " round(TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Fat_Per,3) as Fat_Per,(case when TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then  TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Fat_Kg " & _
             " else -TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Fat_Kg end) as Fat_KG,round(TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.SNF_Per,3) as SNF_Per,(case when TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then  TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.SNF_Kg " & _
             " else -TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.SNF_Kg end) as SNF_Kg,(case when TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then  TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Fat_Amt " & _
             " else -TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Fat_Amt end) as Fat_Amt,(case when TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then  TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.SNF_Amt " & _
             " else -TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.SNF_Amt end) as SNF_Amt from TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL " & _
             " inner join TSPL_PP_STAGE_PROCESS_HEAD on TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE=TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE " & _
             " inner join   TSPL_PE_FINALQC_HEAD on TSPL_PE_FINALQC_HEAD.Batch_Code=TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code " & _
             " where TSPL_PE_FINALQC_HEAD.QC_Code='" & ReceiptCode & "' " & _
             " ) as TSPL_PE_FINALQC_ISSUE_ITEM group by QC_Code,Item_Code,Unit_Code) TSPL_PE_FINALQC_ISSUE_ITEM on Consumption.QC_Code=TSPL_PE_FINALQC_ISSUE_ITEM.QC_Code and Consumption.Consm_Item_Code=TSPL_PE_FINALQC_ISSUE_ITEM.Item_Code " & _
             " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=Consumption.Consm_Item_Code " & _
             " group by Consumption.QC_Code," & _
             " CONSM_LOCATION_CODE, CONSM_SECTION_CODE, CONSM_ITEM_CODE, Consm_Unit_Code,TSPL_ITEM_MASTER.Product_Type,TSPL_PE_FINALQC_ISSUE_ITEM.Unit_Code "

        Return clsDBFuncationality.GetDataTable(qry, trans)
    End Function

    Public Shared Function GetAddRemoveConsumptionQry(ByVal Batch_Code As String, ByVal Batch_FatSnfKg As Decimal, ByVal Prod_FatSnfKg As Decimal) As String
        Dim qry As String = ""
        Dim Factor As Decimal = 1
        If Batch_FatSnfKg <= 0 Then
            Factor = 1
        Else
            Factor = Prod_FatSnfKg / Batch_FatSnfKg
        End If
        qry = " SELECT   (coalesce(SUM(AR_FAT_KG),0)* " & Factor & ") AS AR_FAT_KG,coalesce(SUM(AR_SNF_KG),0)* " & Factor & " AS AR_SNF_KG,coalesce(SUM(AR_Fat_Amt),0)* " & Factor & " AS AR_Fat_Amt,coalesce(SUM(AR_SNF_Amt),0)* " & Factor & " AS AR_SNF_Amt FROM (" & _
              " select STDH.Standardization_Code,STDH.Main_Batch_Code,STDD.Item_Code,STDD.Unit_Code,STDD.Loaction_Code,STDD.ADD_REMOVE_TYPE,STDD.ADD_REMOVE_QTY," & _
              " STDD.AR_FAT_Per,STDD.AR_SNF_Per,(CASE WHEN STDD.ADD_REMOVE_TYPE='Add' then -1 else 0 end)* STDD.AR_FAT_KG as AR_FAT_KG, " & _
              " (CASE WHEN STDD.ADD_REMOVE_TYPE='Add' then -1 else 0 end)*STDD.AR_SNF_KG as AR_SNF_KG, " & _
              " (CASE WHEN STDD.ADD_REMOVE_TYPE='Add' then -1 else 0 end)*STDD.Fat_Amt as AR_Fat_Amt, " & _
              " (CASE WHEN STDD.ADD_REMOVE_TYPE='Add' then -1 else 0 end)*STDD.SNF_Amt AS AR_SNF_Amt from TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL STDD " & _
              " inner join TSPL_PP_STANDARDIZATION_HEAD STDH on STDD.Standardization_Code=STDH.Standardization_Code " & _
              " WHERE STDH.Main_Batch_Code='" & Batch_Code & "' " & _
              " union all " & _
              " select SPH.STAGE_PROCESS_CODE,SPH.Main_Batch_Code,SPD.Item_Code,SPD.Unit_Code,SPD.Loaction_Code,SPD.ADD_REMOVE_TYPE,SPD.ADD_REMOVE_QTY,SPD.Fat_Per,SPD.SNF_Per," & _
              " (CASE WHEN SPD.ADD_REMOVE_TYPE='Add' then -1 else 0 end)*SPD.Fat_Kg AS AR_Fat_Kg, " & _
              " (CASE WHEN SPD.ADD_REMOVE_TYPE='Add' then -1 else 0 end)*SPD.SNF_Kg AS AR_SNF_Kg, " & _
              " (CASE WHEN SPD.ADD_REMOVE_TYPE='Add' then -1 else 0 end)*SPD.Fat_Amt AS AR_Fat_Amt, " & _
              " (CASE WHEN SPD.ADD_REMOVE_TYPE='Add' then -1 else 0 end)*SPD.SNF_Amt AS AR_SNF_Amt from TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL SPD " & _
              " INNER JOIN TSPL_PP_STAGE_PROCESS_HEAD SPH ON SPD.STAGE_PROCESS_CODE=SPH.STAGE_PROCESS_CODE " & _
              " WHERE SPH.Main_Batch_Code='" & Batch_Code & "') AS  ADD_REMOVE "
        Return qry
    End Function

    Public Shared Function SaveRM(ByVal ReceiptCode As String, ByVal arrLoc As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim objRec As clsProductionEntryFinalQCHead
        objRec = clsProductionEntryFinalQCHead.GetData(ReceiptCode, arrLoc, NavigatorType.Current, trans)
        If objRec Is Nothing Then
            Return False
        End If
        If clsCommon.myLen(objRec.QC_Code) <= 0 Or clsCommon.myLen(objRec.Batch_Code) <= 0 Then
            Return False
        End If
        Dim dtIssue As DataTable
        Dim settAllowNegativeStockInDairyProduction As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStockInDairyProduction, clsFixedParameterCode.AllowNegativeStockInDairyProduction, trans)) > 0)
        dtIssue = GetRM(ReceiptCode, trans)
        Dim totalFifoCost As Decimal = 0
        Dim totalLifoCost As Decimal = 0
        Dim totalAvgCost As Decimal = 0
        Dim totalFatKGProd As Decimal = 0
        Dim totalSNFKGProd As Decimal = 0
        Dim totalFatKGBatch As Decimal = 0
        Dim totalSNFKGBatch As Decimal = 0
        Dim totalFatSnfKgProd As Decimal = 0
        Dim totalFatSnfKgBatch As Decimal = 0
        For Each drProd As clsProductionEntryFinalQCDetail In objRec.ArrBatchItem
            totalFatKGProd = totalFatKGProd + drProd.FAT_KG
            totalSNFKGProd = totalSNFKGProd + drProd.SNF_KG
            totalFatKGBatch = totalFatKGBatch + clsBOM.GetFatSNFKG_AfterConversion(drProd.ITEM_CODE, drProd.UNIT_CODE, drProd.BATCH_QTY, drProd.FAT_Per, trans)
            totalSNFKGBatch = totalSNFKGBatch + clsBOM.GetFatSNFKG_AfterConversion(drProd.ITEM_CODE, drProd.UNIT_CODE, drProd.BATCH_QTY, drProd.SNF_Per, trans)
        Next
        totalFatSnfKgProd = totalFatKGProd + totalSNFKGProd
        totalFatSnfKgBatch = totalFatKGBatch + totalSNFKGBatch

        Dim dtADDRem As DataTable = clsDBFuncationality.GetDataTable(clsProductionEntryFinalQCConsumption.GetAddRemoveConsumptionQry(objRec.Batch_Code, totalFatSnfKgBatch, totalFatSnfKgProd), trans)
        For Each dr As DataRow In dtIssue.Rows
            Dim coll As New Hashtable()
            Dim BalanceQty As Decimal = 0
            Dim cost As Decimal = 0
            Dim Product_Type As String = clsItemMaster.GetItemProductType(dr.Item("Consm_Item_Code"), trans)
            '' check item balance 
            If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                BalanceQty = clsInventoryMovementNew.getBalance(clsCommon.myCstr(dr.Item("Consm_Item_Code")), clsLocation.GetMainLocationMilk(clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")), trans), clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")), ReceiptCode, objRec.QC_Date, trans, clsCommon.myCstr(dr.Item("Consm_Unit_Code")))
            Else
                BalanceQty = clsItemLocationDetails.getBalance(clsCommon.myCstr(dr.Item("Consm_Item_Code")), clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")), ReceiptCode, objRec.QC_Date, trans, clsCommon.myCstr(dr.Item("Consm_Unit_Code")), 0)
            End If
            If clsCommon.myCdbl(dr.Item("Consm_Qty")) > BalanceQty Then
                If Not settAllowNegativeStockInDairyProduction Then
                    Throw New Exception("Item: " & clsCommon.myCstr(dr.Item("Consm_Item_Code")) & ", Location:" & clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")) & " Available Qty: " & BalanceQty & "  Consumed Qty: " & clsCommon.myCdbl(dr.Item("Consm_Qty")) & " ")
                End If
            End If
            clsCommon.AddColumnsForChange(coll, "QC_Code", ReceiptCode)
            clsCommon.AddColumnsForChange(coll, "CONSM_ITEM_CODE", clsCommon.myCstr(dr.Item("Consm_Item_Code")))
            clsCommon.AddColumnsForChange(coll, "CONSM_QTY", clsCommon.myCdbl(dr.Item("Consm_Qty")))
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")))
            clsCommon.AddColumnsForChange(coll, "UNIT_CODE", clsCommon.myCstr(dr.Item("Consm_Unit_Code")))
            clsCommon.AddColumnsForChange(coll, "FAT_Per", clsCommon.myCdbl(dr.Item("FAT_Pers")))
            clsCommon.AddColumnsForChange(coll, "SNF_Per", clsCommon.myCdbl(dr.Item("SNF_Pers")))
            clsCommon.AddColumnsForChange(coll, "FAT_KG", clsCommon.myCdbl(dr.Item("FAT_KG")))
            clsCommon.AddColumnsForChange(coll, "SNF_KG", clsCommon.myCdbl(dr.Item("SNF_KG")))
            clsCommon.AddColumnsForChange(coll, "FAT_Amt", IIf(objRec.Is_Job_Work_Inward, 0, clsCommon.myCdbl(dr.Item("Fat_Amt"))))
            clsCommon.AddColumnsForChange(coll, "SNF_Amt", IIf(objRec.Is_Job_Work_Inward, 0, clsCommon.myCdbl(dr.Item("SNF_Amt"))))
            clsCommon.AddColumnsForChange(coll, "FAT_Rate", If(clsCommon.myCdbl(dr.Item("FAT_KG")) <= 0, 0, clsCommon.myCdbl(dr.Item("Fat_Amt")) / clsCommon.myCdbl(dr.Item("FAT_KG"))))
            clsCommon.AddColumnsForChange(coll, "SNF_Rate", If(clsCommon.myCdbl(dr.Item("SNF_KG")) <= 0, 0, clsCommon.myCdbl(dr.Item("SNF_Amt")) / clsCommon.myCdbl(dr.Item("SNF_KG"))))

            cost = IIf(objRec.Is_Job_Work_Inward, 0, clsCommon.myCdbl(dr.Item("Fat_Amt")) + clsCommon.myCdbl(dr.Item("SNF_Amt")))
            clsCommon.AddColumnsForChange(coll, "FIFO_COST", cost)
            totalFifoCost = totalFifoCost + cost

            clsCommon.AddColumnsForChange(coll, "AVG_COST", cost)
            totalAvgCost = totalAvgCost + cost

            clsCommon.AddColumnsForChange(coll, "LIFO_COST", cost)
            totalLifoCost = totalLifoCost + cost

            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PE_FINALQC_CONSUMPTION", OMInsertOrUpdate.Insert, "", trans)
        Next
        Dim totalProduced As Decimal = 0
        Dim objRate As New MIlkComponentType
        Dim ProdCost As Decimal = 0
        If Not objRec.Is_Job_Work_Inward Then
            ProdCost = GetIssueCost(ReceiptCode, trans)
        End If
        For Each objProd As clsProductionEntryFinalQCDetail In objRec.ArrBatchItem
            objRate.FAT_Kg = objRate.FAT_Kg + objProd.FAT_KG
            objRate.SNF_Kg = objRate.SNF_Kg + objProd.SNF_KG
            totalProduced = totalProduced + objProd.RECEIPT_QTY
        Next
        Dim Fatproportion As Decimal = If((objRate.FAT_Kg + objRate.SNF_Kg) <= 0, 1, Math.Round(objRate.FAT_Kg / (objRate.FAT_Kg + objRate.SNF_Kg), 2))
        Dim SNFProportion As Decimal = 1 - Fatproportion
        objRate.FAT_Cost = If(objRate.FAT_Kg <= 0, 0, (ProdCost * Fatproportion) / objRate.FAT_Kg)
        objRate.SNF_Cost = If(objRate.SNF_Kg <= 0, 0, (ProdCost * SNFProportion) / objRate.SNF_Kg)
        Dim settPickProductCostFromItemUOMDetail As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0)
        For Each objProd As clsProductionEntryFinalQCDetail In objRec.ArrBatchItem
            Dim coll As New Hashtable()
            Dim cost As Decimal = objRate.FAT_Cost * objProd.FAT_KG + objRate.SNF_Cost * objProd.SNF_KG
            If settPickProductCostFromItemUOMDetail Then
                Dim bascCost As Decimal = clsItemUOMDetails.GetItemUOMCost(objRec.QC_Date, objProd.ITEM_CODE, objProd.UNIT_CODE, trans)
                If bascCost <= 0 Then
                    Throw New Exception("Please provide Item Cost of item " + objProd.ITEM_CODE + " and unit " + objProd.UNIT_CODE)
                End If
                cost = objProd.FINAL_PRODUCTION_QTY * bascCost
            End If
            clsCommon.AddColumnsForChange(coll, "FIFO_Cost", cost)
            clsCommon.AddColumnsForChange(coll, "AVG_Cost", cost)
            clsCommon.AddColumnsForChange(coll, "LIFO_Cost", cost)

            '' update production avg cost
            clsCommon.AddColumnsForChange(coll, "Fat_Rate", objRate.FAT_Cost)
            clsCommon.AddColumnsForChange(coll, "SNF_Rate", objRate.SNF_Cost)
            clsCommon.AddColumnsForChange(coll, "Fat_Amt", objRate.FAT_Cost * objProd.FAT_KG)
            clsCommon.AddColumnsForChange(coll, "SNF_Amt", objRate.SNF_Cost * objProd.SNF_KG)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PE_FINALQC_DETAIL", OMInsertOrUpdate.Update, "TSPL_PE_FINALQC_DETAIL.QC_Code='" + objRec.QC_Code + "' and TSPL_PE_FINALQC_DETAIL.BOM_CODE='" & objProd.BOM_CODE & "' AND TSPL_PE_FINALQC_DETAIL.ITEM_CODE='" & objProd.ITEM_CODE & "'", trans)
        Next
        Return True
    End Function

    Public Shared Function UpdateRM(ByVal ReceiptCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim objRec As clsProductionEntryFinalQCHead
        objRec = clsProductionEntryFinalQCHead.GetData(ReceiptCode, "", NavigatorType.Current, trans)
        If objRec Is Nothing Then
            Return False
        End If
        If clsCommon.myLen(objRec.QC_Code) <= 0 Or clsCommon.myLen(objRec.Batch_Code) <= 0 Then
            Return False
        End If
        Dim dtIssue As DataTable
        dtIssue = GetRM(ReceiptCode, trans)
        Dim totalFifoCost As Decimal = 0
        Dim totalLifoCost As Decimal = 0
        Dim totalAvgCost As Decimal = 0

        For Each dr As DataRow In dtIssue.Rows
            Dim coll As New Hashtable()
            Dim cost As Decimal = 0
            Dim objCost As New MIlkComponentType
            Dim Product_Type As String = clsItemMaster.GetItemProductType(dr.Item("Consm_Item_Code"), trans)
            If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                objCost.FAT_Cost = clsCommon.myCdbl(dr.Item("FAT_Amt"))
                objCost.SNF_Cost = clsCommon.myCdbl(dr.Item("SNF_Amt"))
            Else
                objCost = clsInventoryMovementNew.GetAvgCost(Product_Type, dr.Item("Consm_Item_Code"), dr.Item("CONSM_LOCATION_CODE"), clsCommon.myCdbl(dr.Item("Consm_Qty")), clsCommon.myCstr(dr.Item("Consm_Unit_Code")), clsCommon.myCdbl(dr.Item("FAT_KG")), clsCommon.myCdbl(dr.Item("SNF_KG")), objRec.QC_Date, clsCommon.GETSERVERDATE(trans), False, trans)
            End If
            clsCommon.AddColumnsForChange(coll, "FAT_Amt", objCost.FAT_Cost)
            clsCommon.AddColumnsForChange(coll, "SNF_Amt", objCost.SNF_Cost)
            clsCommon.AddColumnsForChange(coll, "FAT_Rate", If(clsCommon.myCdbl(dr.Item("FAT_KG")) <= 0, 0, objCost.FAT_Cost / clsCommon.myCdbl(dr.Item("FAT_KG"))))
            clsCommon.AddColumnsForChange(coll, "SNF_Rate", If(clsCommon.myCdbl(dr.Item("SNF_KG")) <= 0, 0, objCost.SNF_Cost / clsCommon.myCdbl(dr.Item("SNF_KG"))))
            cost = objCost.FAT_Cost + objCost.SNF_Cost
            clsCommon.AddColumnsForChange(coll, "FIFO_COST", cost)
            totalFifoCost = totalFifoCost + cost
            clsCommon.AddColumnsForChange(coll, "AVG_COST", cost)
            totalAvgCost = totalAvgCost + cost
            clsCommon.AddColumnsForChange(coll, "LIFO_COST", cost)
            totalLifoCost = totalLifoCost + cost
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PE_FINALQC_CONSUMPTION", OMInsertOrUpdate.Update, "TSPL_PE_FINALQC_CONSUMPTION.QC_Code='" & ReceiptCode & "' and TSPL_PE_FINALQC_CONSUMPTION.CONSM_ITEM_CODE='" & clsCommon.myCstr(dr.Item("Consm_Item_Code")) & "'", trans)
        Next
        Dim totalProduced As Decimal = 0
        Dim objRate As New MIlkComponentType
        Dim ProdCost As Decimal = 0
        ProdCost = GetIssueCost(ReceiptCode, trans)

        For Each objProd As clsProductionEntryFinalQCDetail In objRec.ArrBatchItem
            objRate.FAT_Kg = objRate.FAT_Kg + objProd.FAT_KG
            objRate.SNF_Kg = objRate.SNF_Kg + objProd.SNF_KG
            totalProduced = totalProduced + objProd.RECEIPT_QTY
        Next
        objRate.FAT_Cost = (ProdCost * 2 / 3.0) / IIf(objRate.FAT_Kg <= 0, 1, objRate.FAT_Kg)
        objRate.SNF_Cost = (ProdCost * 1 / 3.0) / IIf(objRate.SNF_Kg <= 0, 1, objRate.SNF_Kg)
        Dim settPickProductCostFromItemUOMDetail As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0)
        For Each objProd As clsProductionEntryFinalQCDetail In objRec.ArrBatchItem
            Dim coll As New Hashtable()
            Dim cost As Decimal = objRate.FAT_Cost * objProd.FAT_KG + objRate.SNF_Cost * objProd.SNF_KG
            If settPickProductCostFromItemUOMDetail Then
                Dim bascCost As Decimal = clsItemUOMDetails.GetItemUOMCost(objRec.QC_Date, objProd.ITEM_CODE, objProd.UNIT_CODE, trans)
                If bascCost <= 0 Then
                    Throw New Exception("Please provide Item Cost of item " + objProd.ITEM_CODE + " and unit " + objProd.UNIT_CODE)
                End If
                cost = objProd.FINAL_PRODUCTION_QTY * bascCost
            End If
            clsCommon.AddColumnsForChange(coll, "FIFO_Cost", cost)
            clsCommon.AddColumnsForChange(coll, "AVG_Cost", cost)
            clsCommon.AddColumnsForChange(coll, "LIFO_Cost", cost)
            clsCommon.AddColumnsForChange(coll, "Fat_Rate", objRate.FAT_Cost)
            clsCommon.AddColumnsForChange(coll, "SNF_Rate", objRate.SNF_Cost)
            clsCommon.AddColumnsForChange(coll, "Fat_Amt", objRate.FAT_Cost * objProd.FAT_KG)
            clsCommon.AddColumnsForChange(coll, "SNF_Amt", objRate.SNF_Cost * objProd.SNF_KG)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PE_FINALQC_DETAIL", OMInsertOrUpdate.Update, "TSPL_PE_FINALQC_DETAIL.QC_Code='" + objRec.QC_Code + "' and TSPL_PE_FINALQC_DETAIL.BOM_CODE='" & objProd.BOM_CODE & "' AND TSPL_PE_FINALQC_DETAIL.ITEM_CODE='" & objProd.ITEM_CODE & "'", trans)
        Next
        Return True
    End Function

    Public Shared Function GetIssueCost(ByVal Doc_Code As String, ByVal trans As SqlTransaction) As Decimal
        Dim obj As New MIlkComponentType
        Dim qry As String = "select sum(Cost) as Cost from (select (case when (Consm.Fat_Amt+Consm.SNF_Amt)<=0 then Consm.Avg_Cost else (Consm.Fat_Amt+Consm.SNF_Amt) end) as Cost from TSPL_PE_FINALQC_CONSUMPTION Consm " & _
            " left join TSPL_ITEM_MASTER Item on Consm.CONSM_ITEM_CODE=Item.Item_Code where Consm.QC_Code='" & Doc_Code & "'" & _
            " union all select SUM(Cost.OverHead_Cost) AS OverHead_Cost from TSPL_PP_COST_WITHOUT_BATCH Cost  where PROD_ENTRY_CODE in ( select Against_PE_Code from TSPL_PE_FINALQC_HEAD where QC_Code='" & Doc_Code & "' )" & _
            " union all select -(Fat_Amt+SNF_Amt) as Cost from TSPL_PE_FINALQC_WRECKAGE_FLUSHING_ITEM where QC_Code='" & Doc_Code & "' and BACK_QTY>0 and Item_Code not in (select CONSM_ITEM_CODE from TSPL_PE_FINALQC_CONSUMPTION where QC_Code='" & Doc_Code & "')" & _
            " union all select ((case when TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then  TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Fat_Amt  else -TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Fat_Amt end) " & _
            " +(case when TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then  TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.SNF_Amt  else -TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.SNF_Amt end)) as ARCost from TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL  " & _
            " inner join TSPL_PP_STAGE_PROCESS_HEAD on TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE=TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE " & _
            " inner join   TSPL_PE_FINALQC_HEAD on TSPL_PE_FINALQC_HEAD.Batch_Code=TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code  " & _
            " where TSPL_PE_FINALQC_HEAD.QC_Code='" & Doc_Code & "'  and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code not in (select CONSM_ITEM_CODE from TSPL_PE_FINALQC_CONSUMPTION where QC_Code='" & Doc_Code & "')" & _
            " ) as Final "
        Dim cost As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return cost
    End Function

    Public Shared Function GetConsumedRM(ByVal ReceiptCode As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsProductionEntryFinalQCConsumption)
        Dim qry As String = "select QC_Code,CONSM_ITEM_CODE,CONSM_QTY,LOCATION_CODE,UNIT_CODE,FIFO_COST,LIFO_COST,AVG_COST,FAT_Per,FAT_KG,SNF_Per,SNF_KG,Fat_Rate,SNF_Rate,FAT_Amt,SNF_Amt from TSPL_PE_FINALQC_CONSUMPTION where QC_Code='" & ReceiptCode & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim obj As clsProductionEntryFinalQCConsumption
        Dim objList As New List(Of clsProductionEntryFinalQCConsumption)
        For Each dr As DataRow In dt.Rows
            obj = New clsProductionEntryFinalQCConsumption
            obj.QC_Code = dr.Item("QC_Code")
            obj.CONSM_ITEM_CODE = dr.Item("CONSM_ITEM_CODE")
            obj.CONSM_QTY = dr.Item("CONSM_QTY")
            obj.LOCATION_CODE = dr.Item("LOCATION_CODE")
            obj.UNIT_CODE = dr.Item("UNIT_CODE")
            obj.FIFO_COST = dr.Item("FIFO_COST")
            obj.LIFO_COST = dr.Item("LIFO_COST")
            obj.AVG_COST = dr.Item("AVG_COST")

            obj.FAT_Per = dr.Item("FAT_Per")
            obj.FAT_KG = dr.Item("FAT_KG")
            obj.SNF_Per = dr.Item("SNF_Per")
            obj.SNF_KG = dr.Item("SNF_KG")

            obj.Fat_Rate = clsCommon.myCdbl(dr.Item("Fat_Rate"))
            obj.Fat_Amt = clsCommon.myCdbl(dr.Item("Fat_Amt"))
            obj.SNF_Rate = clsCommon.myCdbl(dr.Item("SNF_Rate"))
            obj.SNF_Amt = clsCommon.myCdbl(dr.Item("SNF_Amt"))


            objList.Add(obj)
        Next
        Return objList
    End Function

    Public Shared Function UpdateInventoryMovement(ByVal form_id As String, ByVal ReceiptCode As String, ByVal arrloc As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim obj As clsInventoryMovement = Nothing
            Dim objNew As clsInventoryMovementNew = Nothing
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim ArrInventoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            Dim strq As String = ""
            Dim objRec As clsProductionEntryFinalQCHead = clsProductionEntryFinalQCHead.GetData(ReceiptCode, arrloc, NavigatorType.Current, trans)
            Dim objListProd As List(Of clsProductionEntryFinalQCDetail) = objRec.ArrBatchItem
            Dim objListBack As List(Of clsProductionEntryFinalQCWreckageFlushingItem) = objRec.ArrWF
            Dim objListScrap As List(Of clsProductionEntryFinalQCScrapItems) = objRec.ArrScrap
            Dim settPickProductCostFromItemUOMDetail As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0)
            If (objListProd IsNot Nothing AndAlso objListProd.Count > 0) Then
                For Each objProd As clsProductionEntryFinalQCDetail In objListProd
                    Dim strLocation As String = objProd.LOCATION_CODE
                    If clsCommon.CompairString(objRec.Status, "R") = CompairStringResult.Equal Then
                        strLocation = clsLocation.GetRejectedLocation(objProd.LOCATION_CODE, trans)
                    End If

                    Dim strItemTypeToSave As String = ""
                    Dim strItemType As String
                    Dim unitCostFromItemUOMDetail As Decimal = 0
                    Dim AvgCost As Decimal = 0
                    If Not objRec.Is_Job_Work_Inward Then
                        If settPickProductCostFromItemUOMDetail Then
                            unitCostFromItemUOMDetail = clsItemUOMDetails.GetItemUOMCost(objRec.QC_Date, objProd.ITEM_CODE, objProd.UNIT_CODE, trans)
                            If unitCostFromItemUOMDetail <= 0 Then
                                Throw New Exception("Please provide item cost in item master's item Uom detail for Item [" + objProd.ITEM_CODE + "] and UOM[" + objProd.UNIT_CODE + "].")
                            End If
                            AvgCost = objProd.FINAL_PRODUCTION_QTY * unitCostFromItemUOMDetail
                        Else
                            AvgCost = objProd.Fat_Amt + objProd.SNF_Amt
                        End If
                    End If
                    Dim strProductType As String = clsItemMaster.GetItemProductType(objProd.ITEM_CODE, trans)
                    If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                        objNew = New clsInventoryMovementNew
                        objNew.Trans_Type = "Production"
                        objNew.InOut = "I"
                        objNew.Location_Code = strLocation
                        objNew.Item_Code = objProd.ITEM_CODE
                        objNew.Item_Desc = objProd.ITEM_DESCRIPTION
                        objNew.Qty = objProd.FINAL_PRODUCTION_QTY
                        objNew.UOM = objProd.UNIT_CODE
                        objNew.Source_Doc_No = objRec.QC_Code
                        objNew.Source_Doc_Date = objRec.QC_Date

                        objNew.FAT_Per = objProd.FAT_Per
                        objNew.SNF_Per = objProd.SNF_Per
                        objNew.FAT_KG = objProd.FAT_KG
                        objNew.SNF_KG = objProd.SNF_KG
                        objNew.Batch_No = objRec.Batch_Code
                        objNew.CalculateAvgCost = False
                        '' UPDATE PRODUCTION COST
                        objNew.Fat_Rate = objProd.Fat_Rate
                        objNew.SNF_Rate = objProd.SNF_Rate
                        objNew.Fat_Amt = objProd.Fat_Amt
                        objNew.SNF_Amt = objProd.SNF_Amt

                        objNew.Avg_Cost = AvgCost
                        objNew.FIFO_Cost = AvgCost
                        objNew.LIFO_Cost = AvgCost
                        If clsCommon.CompairString(objNew.InOut, "I") = CompairStringResult.Equal Then
                            objNew.Basic_Cost = (AvgCost) / IIf(objProd.FINAL_PRODUCTION_QTY = 0, 1, objProd.FINAL_PRODUCTION_QTY)
                            objNew.Net_Cost = AvgCost
                        End If

                        strItemType = clsItemMaster.GetItemType(objProd.ITEM_CODE, trans)
                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            strItemTypeToSave = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            strItemTypeToSave = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            strItemTypeToSave = "FT"
                        Else
                            strItemTypeToSave = strItemType
                        End If
                        objNew.ItemType = strItemTypeToSave
                        objNew.MFG_Date = objRec.QC_Date
                        ArrInventoryMovementNew.Add(objNew)
                    Else
                        obj = New clsInventoryMovement
                        obj.Trans_Type = "Production"
                        obj.InOut = "I"
                        obj.Location_Code = strLocation
                        obj.Item_Code = objProd.ITEM_CODE
                        obj.Item_Desc = objProd.ITEM_DESCRIPTION
                        obj.Qty = objProd.FINAL_PRODUCTION_QTY
                        obj.UOM = objProd.UNIT_CODE
                        obj.Source_Doc_No = objRec.QC_Code
                        obj.Source_Doc_Date = objRec.QC_Date
                        obj.CalculateAvgCost = False
                        strItemType = clsItemMaster.GetItemType(objProd.ITEM_CODE, trans)
                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            strItemTypeToSave = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            strItemTypeToSave = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            strItemTypeToSave = "FT"
                        Else
                            strItemTypeToSave = strItemType
                        End If
                        obj.ItemType = strItemTypeToSave
                        obj.Batch_No = objRec.Batch_Code

                        ''===========================================================
                        obj.FAT_Per = objProd.FAT_Per
                        obj.SNF_Per = objProd.SNF_Per
                        obj.FAT_KG = objProd.FAT_KG
                        obj.SNF_KG = objProd.SNF_KG

                        obj.Fat_Rate = objProd.Fat_Rate
                        obj.SNF_Amt = objProd.SNF_Amt
                        obj.Fat_Amt = objProd.Fat_Amt
                        obj.SNF_Rate = objProd.SNF_Rate
                        ''==================================================================
                        obj.Avg_Cost = AvgCost
                        obj.Avg_Cost = AvgCost
                        obj.FIFO_Cost = AvgCost
                        obj.LIFO_Cost = AvgCost
                        If clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal Then
                            obj.Basic_Cost = (AvgCost) / IIf(objProd.RECEIPT_QTY <= 0, 1, objProd.RECEIPT_QTY)
                            obj.Net_Cost = AvgCost
                        End If
                        obj.MFG_Date = objRec.QC_Date
                        ArrInventoryMovement.Add(obj)
                    End If
                Next
            End If

            If (objListBack IsNot Nothing AndAlso objListBack.Count > 0) Then
                For Each objBack As clsProductionEntryFinalQCWreckageFlushingItem In objListBack
                    If clsCommon.myCdbl(objBack.BACK_QTY) <= 0 Then
                        Continue For
                    End If
                    Dim strItemTypeToSave As String = ""
                    Dim strItemType As String
                    Dim strProductType As String
                    strProductType = clsItemMaster.GetItemProductType(objBack.Item_Code, trans)
                    If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                        objNew = New clsInventoryMovementNew
                        objNew.Trans_Type = "Production-Return"
                        objNew.InOut = "I"
                        objNew.Location_Code = objBack.Location_Code
                        objNew.Item_Code = objBack.Item_Code
                        objNew.Item_Desc = objBack.Item_Desc
                        objNew.Qty = objBack.BACK_QTY
                        objNew.UOM = objBack.Unit_Code
                        objNew.Source_Doc_No = objRec.QC_Code
                        objNew.Source_Doc_Date = objRec.QC_Date
                        objNew.CalculateAvgCost = False
                        objNew.FAT_Per = objBack.Avail_FAT_Per
                        objNew.SNF_Per = objBack.Avail_SNF_Per
                        objNew.FAT_KG = objBack.Avail_FAT_KG
                        objNew.SNF_KG = objBack.Avail_SNF_KG
                        objNew.Batch_No = objRec.Batch_Code

                        '' UPDATE PRODUCTION COST
                        objNew.Fat_Rate = objBack.Fat_Rate
                        objNew.SNF_Rate = objBack.SNF_Rate
                        objNew.Fat_Amt = objBack.Fat_Amt
                        objNew.SNF_Amt = objBack.SNF_Amt

                        objNew.Avg_Cost = objBack.Fat_Amt + objBack.SNF_Amt
                        objNew.FIFO_Cost = objBack.Fat_Amt + objBack.SNF_Amt
                        objNew.LIFO_Cost = objBack.Fat_Amt + objBack.SNF_Amt

                        If clsCommon.CompairString(objNew.InOut, "I") = CompairStringResult.Equal Then
                            objNew.Basic_Cost = (objBack.Fat_Amt + objBack.SNF_Amt) / IIf(objBack.BACK_QTY = 0, 1, objBack.BACK_QTY)
                            objNew.Net_Cost = objBack.Fat_Amt + objBack.SNF_Amt
                        End If

                        strItemType = clsItemMaster.GetItemType(objBack.Item_Code, trans)
                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            strItemTypeToSave = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            strItemTypeToSave = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            strItemTypeToSave = "FT"
                        Else
                            strItemTypeToSave = strItemType
                        End If
                        objNew.ItemType = strItemTypeToSave

                        objNew.MRP = 0
                        objNew.Add_Cost = 0
                        objNew.MRP = 0
                        objNew.MFG_Date = objRec.QC_Date
                        objNew.IS_CONSUMPTION = 3
                        ArrInventoryMovementNew.Add(objNew)
                    Else
                        obj = New clsInventoryMovement
                        obj.Trans_Type = "Production-Return"
                        obj.InOut = "I"
                        obj.Location_Code = objBack.Location_Code
                        obj.Item_Code = objBack.Item_Code
                        obj.Item_Desc = objBack.Item_Desc
                        obj.Qty = objBack.BACK_QTY
                        obj.UOM = objBack.Unit_Code
                        obj.Source_Doc_No = objRec.QC_Code
                        obj.Source_Doc_Date = objRec.QC_Date
                        obj.CalculateAvgCost = False
                        strItemType = clsItemMaster.GetItemType(objBack.Item_Code, trans)

                        obj.Batch_No = objRec.Batch_Code
                        obj.FAT_Per = objBack.Avail_FAT_Per
                        obj.SNF_Per = objBack.Avail_SNF_Per
                        obj.FAT_KG = objBack.Avail_FAT_KG
                        obj.SNF_KG = objBack.Avail_SNF_KG
                        obj.Fat_Rate = objBack.Fat_Rate
                        obj.SNF_Rate = objBack.SNF_Rate
                        obj.Fat_Amt = objBack.Fat_Amt
                        obj.SNF_Amt = objBack.SNF_Amt
                        obj.Avg_Cost = objBack.Fat_Amt + objBack.SNF_Amt
                        obj.FIFO_Cost = objBack.Fat_Amt + objBack.SNF_Amt
                        obj.LIFO_Cost = objBack.Fat_Amt + objBack.SNF_Amt

                        If clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal Then
                            obj.Basic_Cost = (objBack.Fat_Amt + objBack.SNF_Amt) / IIf(objBack.BACK_QTY <= 0, 1, objBack.BACK_QTY)
                            obj.Net_Cost = objBack.Fat_Amt + objBack.SNF_Amt
                        End If

                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            strItemTypeToSave = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            strItemTypeToSave = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            strItemTypeToSave = "FT"
                        Else
                            strItemTypeToSave = strItemType
                        End If
                        obj.ItemType = strItemTypeToSave

                        'obj.Basic_Cost = 0
                        obj.MRP = 0
                        obj.Add_Cost = 0
                        obj.MRP = 0
                        obj.MFG_Date = objRec.QC_Date
                        obj.IS_CONSUMPTION = 3
                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0 Then
                            Dim QtyInStockUOM As Decimal = IIf(objBack.BACK_QTY > 0, objBack.BACK_QTY, objBack.WRECKAGE_QTY) * clsItemMaster.GetConvertionFactor(objBack.Item_Code, objBack.Unit_Code, trans)
                            obj.Avg_Cost = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, objBack.Item_Code, objRec.CONSM_LOCATION_CODE, QtyInStockUOM, objRec.QC_Date, clsCommon.GETSERVERDATE(trans), True, trans)
                            If objRec.Is_Job_Work_Inward Then
                                obj.Avg_Cost = 0
                            End If
                            obj.LIFO_Cost = obj.Avg_Cost
                            obj.FIFO_Cost = obj.Avg_Cost
                            obj.CalculateAvgCost = False
                        End If
                        ''------------------
                        ArrInventoryMovement.Add(obj)
                    End If
                Next
            End If

            If (objListBack IsNot Nothing AndAlso objListBack.Count > 0) Then
                For Each objBack As clsProductionEntryFinalQCWreckageFlushingItem In objListBack
                    If clsCommon.myCdbl(objBack.BACK_QTY) <= 0 And clsCommon.myCdbl(objBack.WRECKAGE_QTY) <= 0 Then
                        Continue For
                    End If
                    Dim strItemTypeToSave As String = ""
                    Dim strItemType As String
                    Dim strProductType As String
                    strProductType = clsItemMaster.GetItemProductType(objBack.Item_Code, trans)
                    If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                        objNew = New clsInventoryMovementNew
                        objNew.Trans_Type = form_id '"Production-Return"
                        objNew.InOut = "O"
                        objNew.Location_Code = objRec.CONSM_LOCATION_CODE
                        objNew.Item_Code = objBack.Item_Code
                        objNew.Item_Desc = objBack.Item_Desc
                        objNew.Qty = IIf(objBack.BACK_QTY > 0, objBack.BACK_QTY, objBack.WRECKAGE_QTY)
                        objNew.UOM = objBack.Unit_Code
                        objNew.Source_Doc_No = objRec.QC_Code
                        objNew.Source_Doc_Date = objRec.QC_Date
                        objNew.CalculateAvgCost = False
                        objNew.FAT_Per = objBack.Avail_FAT_Per
                        objNew.SNF_Per = objBack.Avail_SNF_Per
                        objNew.FAT_KG = objBack.Avail_FAT_KG
                        objNew.SNF_KG = objBack.Avail_SNF_KG
                        objNew.Batch_No = objRec.Batch_Code

                        '' UPDATE PRODUCTION COST
                        objNew.Fat_Rate = objBack.Fat_Rate
                        objNew.SNF_Rate = objBack.SNF_Rate
                        objNew.Fat_Amt = objBack.Fat_Amt
                        objNew.SNF_Amt = objBack.SNF_Amt

                        objNew.Avg_Cost = objBack.Fat_Amt + objBack.SNF_Amt
                        objNew.FIFO_Cost = objBack.Fat_Amt + objBack.SNF_Amt
                        objNew.LIFO_Cost = objBack.Fat_Amt + objBack.SNF_Amt

                        strItemType = clsItemMaster.GetItemType(objBack.Item_Code, trans)
                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            strItemTypeToSave = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            strItemTypeToSave = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            strItemTypeToSave = "FT"
                        Else
                            strItemTypeToSave = strItemType
                            'Throw New Exception("Item Type not found: " + strItemType)
                        End If
                        objNew.ItemType = strItemTypeToSave

                        'objNew.Basic_Cost = 0
                        objNew.MRP = 0
                        objNew.Add_Cost = 0
                        objNew.MRP = 0
                        objNew.MFG_Date = objRec.QC_Date
                        If objBack.BACK_QTY > 0 Then
                            objNew.IS_CONSUMPTION = 3
                        Else
                            objNew.IS_CONSUMPTION = 2
                        End If

                        ArrInventoryMovementNew.Add(objNew)
                    Else
                        obj = New clsInventoryMovement
                        obj.Trans_Type = form_id '"Production-Return"
                        obj.InOut = "O"
                        obj.Location_Code = objRec.CONSM_LOCATION_CODE
                        obj.Item_Code = objBack.Item_Code
                        obj.Item_Desc = objBack.Item_Desc
                        obj.Qty = IIf(objBack.BACK_QTY > 0, objBack.BACK_QTY, objBack.WRECKAGE_QTY)
                        obj.UOM = objBack.Unit_Code
                        obj.Source_Doc_No = objRec.QC_Code
                        obj.Source_Doc_Date = objRec.QC_Date
                        obj.CalculateAvgCost = False
                        strItemType = clsItemMaster.GetItemType(objBack.Item_Code, trans)

                        obj.Batch_No = objRec.Batch_Code

                        ''=====================================================================
                        obj.FAT_Per = objBack.Avail_FAT_Per
                        obj.SNF_Per = objBack.Avail_SNF_Per
                        obj.FAT_KG = objBack.Avail_FAT_KG
                        obj.SNF_KG = objBack.Avail_SNF_KG

                        '' UPDATE PRODUCTION COST
                        obj.Fat_Rate = objBack.Fat_Rate
                        obj.SNF_Rate = objBack.SNF_Rate
                        obj.Fat_Amt = objBack.Fat_Amt
                        obj.SNF_Amt = objBack.SNF_Amt
                        ''=====================================================================

                        obj.Avg_Cost = objBack.Fat_Amt + objBack.SNF_Amt
                        obj.FIFO_Cost = objBack.Fat_Amt + objBack.SNF_Amt
                        obj.LIFO_Cost = objBack.Fat_Amt + objBack.SNF_Amt

                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            strItemTypeToSave = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            strItemTypeToSave = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            strItemTypeToSave = "FT"
                        Else
                            strItemTypeToSave = strItemType
                            'Throw New Exception("Item Type not found: " + strItemType)
                        End If
                        obj.ItemType = strItemTypeToSave

                        'obj.Basic_Cost = 0
                        'obj.MRP = 0
                        obj.Add_Cost = 0
                        obj.MRP = 0
                        obj.MFG_Date = objRec.QC_Date
                        If objBack.BACK_QTY > 0 Then
                            obj.IS_CONSUMPTION = 3
                        Else
                            obj.IS_CONSUMPTION = 2
                        End If
                        ''richa 20 Aug,2018 BHA/20/08/18-000467
                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0 Then
                            Dim QtyInStockUOM As Decimal = IIf(objBack.BACK_QTY > 0, objBack.BACK_QTY, objBack.WRECKAGE_QTY) * clsItemMaster.GetConvertionFactor(objBack.Item_Code, objBack.Unit_Code, trans)
                            obj.Avg_Cost = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, objBack.Item_Code, objRec.CONSM_LOCATION_CODE, QtyInStockUOM, objRec.QC_Date, clsCommon.GETSERVERDATE(trans), True, trans)
                            If objRec.Is_Job_Work_Inward Then
                                obj.Avg_Cost = 0
                            End If
                            obj.LIFO_Cost = obj.Avg_Cost
                            obj.FIFO_Cost = obj.Avg_Cost
                            obj.CalculateAvgCost = False
                        End If
                        ''------------------
                        ArrInventoryMovement.Add(obj)
                    End If
                Next
            End If

            '' out consumed data

            '' in item qty on ScrapDetail to location

            If (objListScrap IsNot Nothing AndAlso objListScrap.Count > 0) Then
                For Each objBack As clsProductionEntryFinalQCScrapItems In objListScrap
                    If clsCommon.myCdbl(objBack.Scrap_QTY) <= 0 Then
                        Continue For
                    End If
                    Dim strItemTypeToSave As String = ""
                    Dim strItemType As String
                    Dim strProductType As String
                    '' in produced item into selected ScrapDetail to location
                    strProductType = clsItemMaster.GetItemProductType(objBack.Item_Code, trans)
                    If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                        objNew = New clsInventoryMovementNew
                        objNew.Trans_Type = "Production-Return"
                        objNew.InOut = "I"
                        objNew.Location_Code = objBack.Location_Code
                        objNew.Item_Code = objBack.Item_Code
                        objNew.Item_Desc = objBack.Item_Desc
                        objNew.Qty = objBack.Scrap_QTY
                        objNew.UOM = objBack.Unit_Code
                        objNew.Source_Doc_No = objRec.QC_Code
                        objNew.Source_Doc_Date = objRec.QC_Date
                        objNew.CalculateAvgCost = False
                        objNew.FAT_Per = objBack.Avail_FAT_Per
                        objNew.SNF_Per = objBack.Avail_SNF_Per
                        objNew.FAT_KG = objBack.Avail_FAT_KG
                        objNew.SNF_KG = objBack.Avail_SNF_KG
                        objNew.Batch_No = objRec.Batch_Code

                        '' UPDATE PRODUCTION COST
                        objNew.Fat_Rate = objBack.Fat_Rate
                        objNew.SNF_Rate = objBack.SNF_Rate
                        objNew.Fat_Amt = objBack.Fat_Amt
                        objNew.SNF_Amt = objBack.SNF_Amt

                        objNew.Avg_Cost = objBack.Fat_Amt + objBack.SNF_Amt
                        objNew.FIFO_Cost = objBack.Fat_Amt + objBack.SNF_Amt
                        objNew.LIFO_Cost = objBack.Fat_Amt + objBack.SNF_Amt

                        If clsCommon.CompairString(objNew.InOut, "I") = CompairStringResult.Equal Then
                            objNew.Basic_Cost = (objBack.Fat_Amt + objBack.SNF_Amt) / IIf(objBack.Scrap_QTY = 0, 1, objBack.Scrap_QTY)
                            objNew.Net_Cost = objBack.Fat_Amt + objBack.SNF_Amt
                        End If

                        strItemType = clsItemMaster.GetItemType(objBack.Item_Code, trans)
                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            strItemTypeToSave = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            strItemTypeToSave = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            strItemTypeToSave = "FT"
                        Else
                            strItemTypeToSave = strItemType
                            'Throw New Exception("Item Type not found: " + strItemType)
                        End If
                        objNew.ItemType = strItemTypeToSave

                        'objNew.Basic_Cost = 0
                        objNew.MRP = 0
                        objNew.Add_Cost = 0
                        objNew.MRP = 0
                        objNew.MFG_Date = objRec.QC_Date
                        objNew.IS_CONSUMPTION = 3
                        ArrInventoryMovementNew.Add(objNew)
                    Else
                        obj = New clsInventoryMovement
                        obj.Trans_Type = "Production-Return"
                        obj.InOut = "I"
                        obj.Location_Code = objBack.Location_Code
                        obj.Item_Code = objBack.Item_Code
                        obj.Item_Desc = objBack.Item_Desc
                        obj.Qty = objBack.Scrap_QTY
                        obj.UOM = objBack.Unit_Code
                        obj.Source_Doc_No = objRec.QC_Code
                        obj.Source_Doc_Date = objRec.QC_Date
                        obj.CalculateAvgCost = False
                        strItemType = clsItemMaster.GetItemType(objBack.Item_Code, trans)

                        obj.Batch_No = objRec.Batch_Code

                        ''==================================================================================
                        obj.FAT_Per = objBack.Avail_FAT_Per
                        obj.SNF_Per = objBack.Avail_SNF_Per
                        obj.FAT_KG = objBack.Avail_FAT_KG
                        obj.SNF_KG = objBack.Avail_SNF_KG

                        '' UPDATE PRODUCTION COST
                        obj.Fat_Rate = objBack.Fat_Rate
                        obj.SNF_Rate = objBack.SNF_Rate
                        obj.Fat_Amt = objBack.Fat_Amt
                        obj.SNF_Amt = objBack.SNF_Amt
                        ''==================================================================================

                        obj.Avg_Cost = objBack.Fat_Amt + objBack.SNF_Amt

                        If clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal Then
                            obj.CalculateAvgCost = False
                            If Not objRec.Is_Job_Work_Inward Then
                                If obj.Avg_Cost <= 0 Then
                                    Dim isApplyCostOnPostDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCostingOnPostedDate, clsFixedParameterCode.ApplyCostingOnPostedDate, trans)) = 1, True, False)
                                    obj.Stock_Qty = obj.Qty * clsItemMaster.GetConvertionFactor(obj.Item_Code, obj.UOM, trans)
                                    obj.Avg_Cost = clsInventoryMovement.GetCost(EnumCostingMethod.AveregeIn, obj.Item_Code, obj.Location_Code, obj.Stock_Qty, objRec.QC_Date, clsCommon.GETSERVERDATE(trans), isApplyCostOnPostDate, trans)
                                End If
                                If obj.Avg_Cost <= 0 Then
                                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0 Then
                                        Dim bascCost As Decimal = clsItemUOMDetails.GetItemUOMCost(objRec.QC_Date, obj.Item_Code, obj.UOM, trans)
                                        If bascCost <= 0 Then
                                            Throw New Exception("Please provide Item Cost of item " + obj.Item_Code + " and unit " + obj.UOM)
                                        End If
                                        obj.Avg_Cost = obj.Qty * bascCost
                                    End If
                                End If
                            End If
                            
                            obj.Basic_Cost = obj.Stock_Qty / IIf(objBack.Scrap_QTY <= 0, 1, objBack.Scrap_QTY)
                            obj.Net_Cost = obj.Stock_Qty
                        End If

                        ''--------------
                        obj.FIFO_Cost = obj.Avg_Cost
                        obj.LIFO_Cost = obj.Avg_Cost
                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            strItemTypeToSave = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            strItemTypeToSave = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            strItemTypeToSave = "FT"
                        Else
                            strItemTypeToSave = strItemType
                            'Throw New Exception("Item Type not found: " + strItemType)
                        End If
                        obj.ItemType = strItemTypeToSave

                        'obj.Basic_Cost = 0
                        obj.MRP = 0
                        obj.Add_Cost = 0
                        obj.MRP = 0
                        obj.MFG_Date = objRec.QC_Date
                        obj.IS_CONSUMPTION = 3
                        ArrInventoryMovement.Add(obj)
                    End If
                Next
            End If

            '' update inventory for consumption
            UpdateInventoryMovementConsumption(form_id, ArrInventoryMovement, ArrInventoryMovementNew, objRec, arrloc, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False

        End Try
        Return True
    End Function

    Public Shared Function UpdateInventoryMovementConsumption(ByVal form_id As String, ByRef ArrInventoryMovement As List(Of clsInventoryMovement), ByRef ArrInventoryMovementNew As List(Of clsInventoryMovementNew), ByVal objRec As clsProductionEntryFinalQCHead, ByVal arrloc As String, ByVal trans As SqlTransaction) As Boolean
        Dim obj As clsInventoryMovement = Nothing
        Dim objNew As clsInventoryMovementNew = Nothing

        Dim objData As List(Of clsProductionEntryFinalQCConsumption) = clsProductionEntryFinalQCConsumption.GetConsumedRM(objRec.QC_Code, trans)
        For Each dr As clsProductionEntryFinalQCConsumption In objData
            Dim strItemTypeToSave As String = ""
            Dim strItemType As String
            Dim strProductType As String
            '' out consumed item
            strProductType = clsItemMaster.GetItemProductType(dr.CONSM_ITEM_CODE, trans)
            If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                objNew = New clsInventoryMovementNew
                objNew.Trans_Type = "Consumption"
                objNew.InOut = "O"
                objNew.Location_Code = dr.LOCATION_CODE
                objNew.Item_Code = dr.CONSM_ITEM_CODE
                objNew.Item_Desc = clsItemMaster.GetItemName(dr.CONSM_ITEM_CODE, trans)
                objNew.Qty = dr.CONSM_QTY
                objNew.UOM = dr.UNIT_CODE
                objNew.Source_Doc_No = dr.QC_Code
                objNew.Source_Doc_Date = objRec.QC_Date
                objNew.CalculateAvgCost = False
                objNew.FAT_Per = dr.FAT_Per
                objNew.SNF_Per = dr.SNF_Per
                objNew.FAT_KG = dr.FAT_KG
                objNew.SNF_KG = dr.SNF_KG
                objNew.Batch_No = objRec.Batch_Code

                '' UPDATE PRODUCTION COST
                objNew.Fat_Rate = dr.Fat_Rate
                objNew.SNF_Rate = dr.SNF_Rate
                objNew.Fat_Amt = dr.Fat_Amt
                objNew.SNF_Amt = dr.SNF_Amt

                objNew.Avg_Cost = dr.Fat_Amt + dr.SNF_Amt
                objNew.FIFO_Cost = dr.Fat_Amt + dr.SNF_Amt
                objNew.LIFO_Cost = dr.Fat_Amt + dr.SNF_Amt

                strItemType = clsItemMaster.GetItemType(dr.CONSM_ITEM_CODE, trans)
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                Else
                    strItemTypeToSave = strItemType
                    'Throw New Exception("Item Type not found: " + strItemType)
                End If
                objNew.ItemType = strItemTypeToSave
                objNew.Basic_Cost = 0
                objNew.Add_Cost = 0
                objNew.MRP = 0
                objNew.DonNotCalculateAvgFATSNFCost = objRec.Is_Job_Work_Inward
                objNew.IS_CONSUMPTION = 1
                ArrInventoryMovementNew.Add(objNew)

            Else
                obj = New clsInventoryMovement
                obj.Trans_Type = "Consumption"
                obj.InOut = "O"
                obj.Location_Code = dr.LOCATION_CODE
                obj.Item_Code = dr.CONSM_ITEM_CODE
                obj.Item_Desc = clsItemMaster.GetItemName(dr.CONSM_ITEM_CODE, trans)
                obj.Qty = dr.CONSM_QTY
                obj.UOM = dr.UNIT_CODE
                obj.Source_Doc_No = dr.QC_Code
                obj.Source_Doc_Date = objRec.QC_Date
                obj.Batch_No = objRec.Batch_Code
                obj.CalculateAvgCost = False

                ''======================================================================
                obj.FAT_Per = dr.FAT_Per
                obj.SNF_Per = dr.SNF_Per
                obj.FAT_KG = dr.FAT_KG
                obj.SNF_KG = dr.SNF_KG

                '' UPDATE PRODUCTION COST
                obj.Fat_Rate = dr.Fat_Rate
                obj.SNF_Rate = dr.SNF_Rate
                obj.Fat_Amt = dr.Fat_Amt
                obj.SNF_Amt = dr.SNF_Amt
                ''===============================================================

                obj.Avg_Cost = dr.AVG_COST
                obj.FIFO_Cost = dr.AVG_COST
                obj.LIFO_Cost = dr.AVG_COST

                strItemType = clsItemMaster.GetItemType(dr.CONSM_ITEM_CODE, trans)
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                Else
                    strItemTypeToSave = strItemType
                    'Throw New Exception("Item Type not found: " + strItemType)
                End If
                obj.ItemType = strItemTypeToSave
                obj.Basic_Cost = 0
                obj.Add_Cost = 0
                obj.MRP = 0
                obj.IS_CONSUMPTION = 1
                ArrInventoryMovement.Add(obj)

            End If

        Next


        If ArrInventoryMovement.Count > 0 Then
            clsInventoryMovement.SaveData(form_id, objRec.QC_Code, clsCommon.GetPrintDate(objRec.QC_Date, "dd/MMM/yyyy"), clsCommon.GetPrintDate(objRec.QC_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
        End If
        If ArrInventoryMovementNew.Count > 0 Then
            clsInventoryMovementNew.SaveData(form_id, objRec.QC_Code, clsCommon.GetPrintDate(objRec.QC_Date, "dd/MMM/yyyy"), clsCommon.GetPrintDate(objRec.QC_Date, "dd/MM/yyyy"), ArrInventoryMovementNew, trans)
        End If
        Return True
    End Function
End Class

Public Class clsProductionEntryFinalQCIssueItems
#Region "Variables"
    Public PRODUCTION_ENTRY_CODE As String = Nothing
    Public SNO As Integer = 0
    Public Issue_Code As String = Nothing
    Public From_Loaction_Code As String = Nothing
    Public To_Location_Code As String = Nothing

    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_Code As String = Nothing
    Public Item_Type As String = Nothing
    Public Product_Type As String
    Public Unit_Desc As String = Nothing

    Public Avail_Qty As Decimal = 0
    Public Avail_FAT_Per As Decimal = 0
    Public Avail_SNF_Per As Decimal = 0
    Public Avail_FAT_KG As Decimal = 0
    Public Avail_SNF_KG As Decimal = 0

    'Public Diff_Qty As Decimal = 0
    'Public Diff_FAT_Per As Decimal = 0
    'Public Diff_SNF_Per As Decimal = 0
    'Public Diff_FAT_KG As Decimal = 0
    'Public Diff_SNF_KG As Decimal = 0

    Public Remarks As String = Nothing
    'Public Issue_Status As String = Nothing
    Public Comp_Code As String = Nothing

    '' production costing columns
    Public Fat_Rate As Decimal = 0
    Public SNF_Rate As Decimal = 0
    Public Fat_Amt As Decimal = 0
    Public SNF_Amt As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal PRODUCTION_ENTRY_CODE As String, ByVal obj As clsProductionEntryFinalQCHead, ByVal arr As List(Of clsProductionEntryFinalQCIssueItems), ByVal trans As SqlTransaction) As Boolean
        'Try
        Dim isSaved As Boolean = True
        Dim qry As String = "delete from TSPL_PE_FINALQC_ISSUE_ITEM where comp_code='" + objCommonVar.CurrentCompanyCode + "' and QC_Code='" + PRODUCTION_ENTRY_CODE + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Dim coll As New Hashtable()

        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each objtr As clsProductionEntryFinalQCIssueItems In arr
                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "QC_Code", PRODUCTION_ENTRY_CODE)
                clsCommon.AddColumnsForChange(coll, "SNO", objtr.SNO)
                clsCommon.AddColumnsForChange(coll, "Issue_Code", objtr.Issue_Code, True)
                clsCommon.AddColumnsForChange(coll, "From_Loaction_Code", objtr.From_Loaction_Code, True)
                clsCommon.AddColumnsForChange(coll, "To_Location_Code", objtr.To_Location_Code, True)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "Avail_Qty", objtr.Avail_Qty)
                clsCommon.AddColumnsForChange(coll, "Avail_FAT_Per", objtr.Avail_FAT_Per)
                clsCommon.AddColumnsForChange(coll, "Avail_FAT_KG", objtr.Avail_FAT_KG)
                clsCommon.AddColumnsForChange(coll, "Avail_SNF_Per", objtr.Avail_SNF_Per)
                clsCommon.AddColumnsForChange(coll, "Avail_SNF_KG", objtr.Avail_SNF_KG)
                clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                clsCommon.AddColumnsForChange(coll, "Fat_Rate", objtr.Fat_Rate)
                clsCommon.AddColumnsForChange(coll, "SNF_Rate", objtr.SNF_Rate)
                clsCommon.AddColumnsForChange(coll, "Fat_Amt", objtr.Fat_Amt)
                clsCommon.AddColumnsForChange(coll, "SNF_Amt", objtr.SNF_Amt)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PE_FINALQC_ISSUE_ITEM", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If

        Return isSaved
    End Function

    Public Shared Function GetPPPEIssuedDetail(ByVal PRODUCTION_ENTRY_CODE As String, ByVal trans As SqlTransaction) As List(Of clsProductionEntryFinalQCIssueItems)
        Dim objIssueList As New List(Of clsProductionEntryFinalQCIssueItems)
        Dim qry As String = "select TSPL_PE_FINALQC_ISSUE_ITEM.*,TSPL_ITEM_MASTER.ITEM_DESC,TSPL_ITEM_MASTER.ITEM_TYPE,isnull(TSPL_ITEM_MASTER.Product_Type,'') as Product_Type,TSPL_UNIT_MASTER.Unit_Desc from TSPL_PE_FINALQC_ISSUE_ITEM " & _
        " left join TSPL_ITEM_MASTER on TSPL_PE_FINALQC_ISSUE_ITEM.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE  " & _
        " left join TSPL_UNIT_MASTER on TSPL_PE_FINALQC_ISSUE_ITEM.Unit_Code=TSPL_UNIT_MASTER.Unit_Code  " & _
        " where TSPL_PE_FINALQC_ISSUE_ITEM.comp_code='" + objCommonVar.CurrentCompanyCode + "' and QC_Code='" + PRODUCTION_ENTRY_CODE + "' order by sno"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsProductionEntryFinalQCIssueItems()

                objtr.Issue_Code = clsCommon.myCstr(dr("Issue_Code"))
                objtr.From_Loaction_Code = clsCommon.myCstr(dr("From_Loaction_Code"))
                objtr.To_Location_Code = clsCommon.myCstr(dr("To_Location_Code"))
                objtr.Avail_FAT_KG = dr("Avail_FAT_KG")
                objtr.Avail_FAT_Per = dr("Avail_FAT_Per")
                objtr.Avail_Qty = dr("Avail_Qty")
                objtr.Avail_SNF_KG = dr("Avail_SNF_KG")
                objtr.Avail_SNF_Per = dr("Avail_SNF_Per")
                objtr.Comp_Code = clsCommon.myCstr(dr("Comp_Code"))
                'objtr.Diff_FAT_KG = dr("Diff_FAT_KG")
                'objtr.Diff_FAT_Per = dr("Diff_FAT_Per")
                'objtr.Diff_Qty = dr("Diff_Qty")
                'objtr.Diff_SNF_KG = dr("Diff_SNF_KG")
                'objtr.Diff_SNF_Per = dr("Diff_SNF_Per")

                objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                objtr.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                objtr.Product_Type = clsCommon.myCstr(dr("Product_Type"))

                objtr.Remarks = clsCommon.myCstr(dr("Remarks"))
                'objtr.Issue_Status = clsCommon.myCstr(dr("Issue_Status"))

                objtr.SNO = clsCommon.myCdbl(dr("SNO"))
                objtr.PRODUCTION_ENTRY_CODE = clsCommon.myCstr(dr("QC_Code"))
                objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                objtr.Unit_Desc = clsCommon.myCstr(dr("Unit_Desc"))

                '' production costing columns
                objtr.Fat_Rate = clsCommon.myCdbl(dr("Fat_Rate"))
                objtr.SNF_Rate = clsCommon.myCdbl(dr("SNF_Rate"))
                objtr.Fat_Amt = clsCommon.myCdbl(dr("Fat_Amt"))
                objtr.SNF_Amt = clsCommon.myCdbl(dr("SNF_Amt"))
                objIssueList.Add(objtr)
            Next
        End If
        Return objIssueList
    End Function
End Class

Public Class clsProductionEntryFinalQCStageDetail
#Region "Variables"
    Public PRODUCTION_ENTRY_CODE As String = Nothing
    Public SNO As Integer = 0
    Public Stage_Code As String = Nothing
    Public Stage_Desc As String = Nothing
    Public Received_Qty As String = Nothing
    Public Unit_Code As String = Nothing
    Public Unit_Desc As String = Nothing
    Public Log_Sheet_No As String = Nothing
    Public Status As String = Nothing
    Public Remarks As String
    Public Section_Code As String
    Public Structure_Code As String
    Public Comp_Code As String = Nothing
    Public Batch_Code As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal PRODUCTION_ENTRY_CODE As String, ByVal arr As List(Of clsProductionEntryFinalQCStageDetail), ByVal trans As SqlTransaction) As Boolean
        'Try
        Dim isSaved As Boolean = True
        Dim qry As String = "delete from TSPL_PE_FINALQC_STAGE_DETAIL where comp_code='" + objCommonVar.CurrentCompanyCode + "' and QC_Code='" + PRODUCTION_ENTRY_CODE + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Dim coll As New Hashtable()

        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each objtr As clsProductionEntryFinalQCStageDetail In arr
                coll = New Hashtable()

                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "QC_Code", PRODUCTION_ENTRY_CODE)
                clsCommon.AddColumnsForChange(coll, "SNO", objtr.SNO)
                clsCommon.AddColumnsForChange(coll, "Stage_Code", objtr.Stage_Code)
                clsCommon.AddColumnsForChange(coll, "Received_Qty", objtr.Received_Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "Log_Sheet_No", objtr.Log_Sheet_No, True)
                clsCommon.AddColumnsForChange(coll, "Status", objtr.Status)
                clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                clsCommon.AddColumnsForChange(coll, "Section_Code", objtr.Section_Code)
                clsCommon.AddColumnsForChange(coll, "Structure_Code", objtr.Structure_Code)
                clsCommon.AddColumnsForChange(coll, "Batch_Code", objtr.Batch_Code, True)

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PE_FINALQC_STAGE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return isSaved
    End Function

    Public Shared Function GetPPPEStageDetail(ByVal PRODUCTION_ENTRY_CODE As String, ByVal trans As SqlTransaction) As List(Of clsProductionEntryFinalQCStageDetail)
        Dim objARList As New List(Of clsProductionEntryFinalQCStageDetail)
        Dim qry As String = "select TSPL_PE_FINALQC_STAGE_DETAIL.*, " & _
        " TSPL_UNIT_MASTER.Unit_Desc,TSPL_STAGE_MASTER.Description as Stage_Desc from TSPL_PE_FINALQC_STAGE_DETAIL " & _
        " left join TSPL_UNIT_MASTER on TSPL_PE_FINALQC_STAGE_DETAIL.Unit_Code=TSPL_UNIT_MASTER.Unit_Code  " & _
        " left join TSPL_STAGE_MASTER on TSPL_PE_FINALQC_STAGE_DETAIL.Stage_Code=TSPL_STAGE_MASTER.Stage_Code  " & _
        " where TSPL_PE_FINALQC_STAGE_DETAIL.comp_code='" + objCommonVar.CurrentCompanyCode + "' and QC_Code='" + PRODUCTION_ENTRY_CODE + "' order by sno"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsProductionEntryFinalQCStageDetail()
                objtr.SNO = CInt(dr("SNO"))
                objtr.Comp_Code = clsCommon.myCstr(dr("Comp_Code"))
                objtr.Log_Sheet_No = clsCommon.myCstr(dr("Log_Sheet_No"))
                objtr.Received_Qty = clsCommon.myCdbl(dr("Received_Qty"))
                objtr.Remarks = clsCommon.myCstr(dr("Remarks"))
                objtr.Stage_Code = clsCommon.myCstr(dr("Stage_Code"))
                objtr.Stage_Desc = clsCommon.myCstr(dr("Stage_Desc"))
                objtr.PRODUCTION_ENTRY_CODE = clsCommon.myCstr(dr("QC_Code"))
                objtr.Status = clsCommon.myCstr(dr("Status"))
                objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                objtr.Section_Code = clsCommon.myCstr(dr("Section_Code"))
                objtr.Structure_Code = clsCommon.myCstr(dr("Structure_Code"))
                objtr.Batch_Code = clsCommon.myCstr(dr("Batch_Code"))
                objARList.Add(objtr)
            Next
        End If
        Return objARList
    End Function
End Class

Public Class clsProductionEntryFinalQCWreckageFlushingItem
#Region "Variables"
    Public PRODUCTION_ENTRY_CODE As String = Nothing
    Public SNO As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_Code As String = Nothing
    Public Unit_Desc As String = Nothing
    'Public Item_Type As String = Nothing
    'Public Product_Type As String
    Public BACK_QTY As Decimal = 0
    Public WRECKAGE_QTY As Decimal = 0
    Public Location_Code As String = Nothing
    Public Avail_FAT_Per As Decimal = 0
    Public Avail_SNF_Per As Decimal = 0
    Public Avail_FAT_KG As Decimal = 0
    Public Avail_SNF_KG As Decimal = 0
    Public Remarks As String = Nothing
    Public Comp_Code As String = Nothing

    '' production costing columns
    Public Fat_Rate As Decimal = 0
    Public SNF_Rate As Decimal = 0
    Public Fat_Amt As Decimal = 0
    Public SNF_Amt As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal obj As clsProductionEntryFinalQCHead, ByVal trans As SqlTransaction) As Boolean
        'Try
        Dim isSaved As Boolean = True
        Dim qry As String = "delete from TSPL_PE_FINALQC_WRECKAGE_FLUSHING_ITEM where comp_code='" + objCommonVar.CurrentCompanyCode + "' and QC_Code='" + obj.QC_Code + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Dim coll As New Hashtable()
        If obj.ArrWF IsNot Nothing AndAlso obj.ArrWF.Count > 0 Then
            For Each objtr As clsProductionEntryFinalQCWreckageFlushingItem In obj.ArrWF
                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "QC_Code", obj.QC_Code)
                clsCommon.AddColumnsForChange(coll, "SNO", objtr.SNO)

                clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)

                clsCommon.AddColumnsForChange(coll, "BACK_QTY", objtr.BACK_QTY)
                clsCommon.AddColumnsForChange(coll, "WRECKAGE_QTY", objtr.WRECKAGE_QTY)
                clsCommon.AddColumnsForChange(coll, "Location_Code", objtr.Location_Code, True)
                clsCommon.AddColumnsForChange(coll, "Avail_FAT_Per", objtr.Avail_FAT_Per)
                clsCommon.AddColumnsForChange(coll, "Avail_FAT_KG", objtr.Avail_FAT_KG)
                clsCommon.AddColumnsForChange(coll, "Avail_SNF_Per", objtr.Avail_SNF_Per)
                clsCommon.AddColumnsForChange(coll, "Avail_SNF_KG", objtr.Avail_SNF_KG)

                clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)


                If obj.Is_Job_Work_Inward Then
                    objtr.Fat_Rate = 0
                    objtr.SNF_Rate = 0
                    objtr.Fat_Amt = 0
                    objtr.SNF_Amt = 0
                Else
                    If objtr.BACK_QTY > 0 Then
                        Dim objCost As New MIlkComponentType
                        objCost = clsInventoryMovementNew.GetAvgCost("MI", objtr.Item_Code, obj.CONSM_LOCATION_CODE, objtr.BACK_QTY, objtr.Unit_Code, objtr.Avail_FAT_KG, objtr.Avail_FAT_KG, obj.QC_Date, obj.QC_Date, False, trans) 'GetBackQtyFatSNFRate(obj.QC_Code, objtr.Item_Code, trans)
                        objtr.Fat_Amt = objCost.FAT_Cost
                        objtr.SNF_Amt = objCost.SNF_Cost
                        If objtr.Avail_FAT_KG = 0 Then
                            objtr.Fat_Rate = 0
                        Else
                            objtr.Fat_Rate = Math.Round(objCost.FAT_Cost / objtr.Avail_FAT_KG, 3, MidpointRounding.AwayFromZero)
                        End If

                        If objtr.Avail_SNF_KG = 0 Then
                            objtr.SNF_Rate = 0
                        Else
                            objtr.SNF_Rate = Math.Round(objCost.SNF_Cost / objtr.Avail_SNF_KG, 3, MidpointRounding.AwayFromZero)
                        End If
                    ElseIf objtr.WRECKAGE_QTY > 0 Then
                        Dim objCost As New MIlkComponentType
                        objCost = clsInventoryMovementNew.GetAvgCost("MI", objtr.Item_Code, obj.CONSM_LOCATION_CODE, objtr.WRECKAGE_QTY, objtr.Unit_Code, objtr.Avail_FAT_KG, objtr.Avail_FAT_KG, obj.QC_Date, obj.QC_Date, False, trans)
                        objtr.Fat_Amt = objCost.FAT_Cost
                        objtr.SNF_Amt = objCost.SNF_Cost
                        If objtr.Avail_FAT_KG = 0 Then
                            objtr.Fat_Rate = 0
                        Else
                            objtr.Fat_Rate = Math.Round(objCost.FAT_Cost / objtr.Avail_FAT_KG, 3, MidpointRounding.AwayFromZero)
                        End If

                        If objtr.Avail_SNF_KG = 0 Then
                            objtr.SNF_Rate = 0
                        Else
                            objtr.SNF_Rate = Math.Round(objCost.SNF_Cost / objtr.Avail_SNF_KG, 3, MidpointRounding.AwayFromZero)
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "Fat_Rate", objtr.Fat_Rate)
                clsCommon.AddColumnsForChange(coll, "SNF_Rate", objtr.SNF_Rate)
                clsCommon.AddColumnsForChange(coll, "Fat_Amt", objtr.Fat_Amt)
                clsCommon.AddColumnsForChange(coll, "SNF_Amt", objtr.SNF_Amt)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PE_FINALQC_WRECKAGE_FLUSHING_ITEM", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return isSaved
    End Function

    Public Shared Function GetBackQtyFatSNFRate(ByVal Doc_Code As String, ByVal Item_Code As String, ByVal trans As SqlTransaction) As MIlkComponentType
        Dim objCost As New MIlkComponentType
        Dim qry As String = "select AVG(Fat_Rate) as Fat_Rate,AVG(SNF_Rate) as SNF_Rate from TSPL_PE_FINALQC_ISSUE_ITEM where QC_Code='" & Doc_Code & "' and Item_Code='" & Item_Code & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            objCost.FAT_Cost = clsCommon.myCdbl(dt.Rows(0).Item("Fat_Rate"))
            objCost.SNF_Cost = clsCommon.myCdbl(dt.Rows(0).Item("SNF_Rate"))
        End If
        Return objCost
    End Function

    Public Shared Function GetPPPEWFDetail(ByVal PRODUCTION_ENTRY_CODE As String, ByVal trans As SqlTransaction) As List(Of clsProductionEntryFinalQCWreckageFlushingItem)
        Dim objWFList As New List(Of clsProductionEntryFinalQCWreckageFlushingItem)
        Dim qry As String = "select TSPL_PE_FINALQC_WRECKAGE_FLUSHING_ITEM.*,TSPL_ITEM_MASTER.ITEM_DESC,TSPL_ITEM_MASTER.ITEM_TYPE,isnull(TSPL_ITEM_MASTER.Product_Type,'') as Product_Type,TSPL_UNIT_MASTER.Unit_Desc from TSPL_PE_FINALQC_WRECKAGE_FLUSHING_ITEM " & _
        " left join TSPL_ITEM_MASTER on TSPL_PE_FINALQC_WRECKAGE_FLUSHING_ITEM.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE  " & _
        " left join TSPL_UNIT_MASTER on TSPL_PE_FINALQC_WRECKAGE_FLUSHING_ITEM.Unit_Code=TSPL_UNIT_MASTER.Unit_Code  " & _
        " where TSPL_PE_FINALQC_WRECKAGE_FLUSHING_ITEM.comp_code='" + objCommonVar.CurrentCompanyCode + "' and QC_Code='" + PRODUCTION_ENTRY_CODE + "' order by sno"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsProductionEntryFinalQCWreckageFlushingItem()
                objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                objtr.BACK_QTY = dr("BACK_QTY")
                objtr.WRECKAGE_QTY = dr("WRECKAGE_QTY")
                objtr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objtr.Avail_SNF_KG = dr("Avail_SNF_KG")
                objtr.Avail_SNF_Per = dr("Avail_SNF_Per")
                objtr.Avail_FAT_KG = dr("Avail_FAT_KG")
                objtr.Avail_FAT_Per = dr("Avail_FAT_Per")
                objtr.Comp_Code = clsCommon.myCstr(dr("Comp_Code"))

                'objtr.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                'objtr.Product_Type = clsCommon.myCstr(dr("Product_Type"))

                objtr.Remarks = clsCommon.myCstr(dr("Remarks"))
                objtr.SNO = clsCommon.myCdbl(dr("SNO"))
                objtr.PRODUCTION_ENTRY_CODE = clsCommon.myCstr(dr("QC_Code"))
                objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                objtr.Unit_Desc = clsCommon.myCstr(dr("Unit_Desc"))

                '' production costing
                objtr.Fat_Rate = clsCommon.myCdbl(dr("Fat_Rate"))
                objtr.SNF_Rate = clsCommon.myCdbl(dr("SNF_Rate"))
                objtr.Fat_Amt = clsCommon.myCdbl(dr("Fat_Amt"))
                objtr.SNF_Amt = clsCommon.myCdbl(dr("SNF_Amt"))

                objWFList.Add(objtr)
            Next
        End If
        Return objWFList
    End Function

End Class

Public Class clsProductionEntryFinalQCQCDetail
#Region "Variables"
    Public sno As Integer = Nothing
    Public Item_Code As String = Nothing
    Public ItemDesc As String = Nothing
    Public param_code As String = Nothing
    Public param_desc As String = Nothing
    Public param_type As String = Nothing
    Public param_nature As String = Nothing
    Public param_nature_Desc As String = Nothing
    Public Standard_Range As Decimal = Nothing
    Public Standard_Status As String = Nothing
    Public Standard_Value As String = Nothing

    'Public urange As Decimal = Nothing
    Public Actual_Range As String = Nothing
    Public Actual_Value As String = Nothing
    Public Actual_Status As String = Nothing
    Public Qc_Status As String
    Public remarks As String = Nothing
    Public Comp_Code As String = Nothing
    Public QC_Type As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal PRODUCTION_ENTRY_CODE As String, ByVal arr As List(Of clsProductionEntryFinalQCQCDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = "delete from TSPL_PE_FINALQC_QC_DETAIL where comp_code='" + objCommonVar.CurrentCompanyCode + "' and QC_Code='" + PRODUCTION_ENTRY_CODE + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsProductionEntryFinalQCQCDetail In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "QC_Type", objtr.QC_Type)
                    clsCommon.AddColumnsForChange(coll, "QC_Code", PRODUCTION_ENTRY_CODE)
                    clsCommon.AddColumnsForChange(coll, "SNO", objtr.sno)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Parameter_Code", objtr.param_code)
                    clsCommon.AddColumnsForChange(coll, "Lower_range", objtr.Standard_Range)
                    clsCommon.AddColumnsForChange(coll, "Upper_range", objtr.Standard_Range)
                    'clsCommon.AddColumnsForChange(coll, "Status", objtr.Standard_Status)
                    'clsCommon.AddColumnsForChange(coll, "Value1", objtr.Standard_Value)

                    clsCommon.AddColumnsForChange(coll, "Actual_Range", objtr.Actual_Range)
                    clsCommon.AddColumnsForChange(coll, "Actual_Status", objtr.Actual_Status)
                    clsCommon.AddColumnsForChange(coll, "Actual_Value", objtr.Actual_Value)
                    clsCommon.AddColumnsForChange(coll, "Qc_Status", objtr.Qc_Status)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.remarks)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PE_FINALQC_QC_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetPPPEQCDetail(ByVal QC_Code As String, ByVal trans As SqlTransaction) As List(Of clsProductionEntryFinalQCQCDetail)
        Dim objQCList As New List(Of clsProductionEntryFinalQCQCDetail)
        Dim qry As String = "select TSPL_PE_FINALQC_QC_DETAIL.*,TSPL_ITEM_MASTER.ITEM_DESC,TSPL_ITEM_MASTER.ITEM_TYPE,TSPL_ITEM_MASTER.Product_Type, " & _
        " TSPL_PARAMETER_MASTER.Description as Paramenter_Desc,TSPL_PARAMETER_MASTER.Nature as Parameter_Nature,(Case when TSPL_PARAMETER_MASTER.Nature='A' then'Alphanumeric' else  case when TSPL_PARAMETER_MASTER.Nature='B' then'Boolean' else case when TSPL_PARAMETER_MASTER.Nature='R' then'Range' end end end) as Nature_Desc,TSPL_PARAMETER_MASTER.Type as Parameter_Type " & _
        " from TSPL_PE_FINALQC_QC_DETAIL " & _
        " left join TSPL_ITEM_MASTER on TSPL_PE_FINALQC_QC_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE  " & _
        " left join TSPL_PARAMETER_MASTER on TSPL_PE_FINALQC_QC_DETAIL.Parameter_Code=TSPL_PARAMETER_MASTER.Code " & _
        " where TSPL_PE_FINALQC_QC_DETAIL.comp_code='" + objCommonVar.CurrentCompanyCode + "' and QC_Code='" + QC_Code + "' order by sno"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsProductionEntryFinalQCQCDetail()
                objtr.sno = CInt(dr("SNO"))
                objtr.QC_Type = clsCommon.myCstr(dr("QC_Type"))
                objtr.Comp_Code = clsCommon.myCstr(dr("Comp_Code"))
                objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objtr.ItemDesc = clsCommon.myCstr(dr("Item_Desc"))
                objtr.param_code = clsCommon.myCstr(dr("Parameter_Code"))
                objtr.param_desc = clsCommon.myCstr(dr("Paramenter_Desc"))
                objtr.param_nature = clsCommon.myCstr(dr("Parameter_Nature"))
                objtr.param_nature_Desc = clsCommon.myCstr(dr("Nature_Desc"))
                objtr.param_type = clsCommon.myCstr(dr("Parameter_Type"))
                objtr.remarks = clsCommon.myCstr(dr("remarks"))
                objtr.Standard_Range = clsCommon.myCstr(dr("Lower_Range"))
                objtr.Actual_Range = clsCommon.myCstr(dr("Actual_Range"))
                objtr.Actual_Status = clsCommon.myCstr(dr("Actual_Status"))
                objtr.Actual_Value = clsCommon.myCstr(dr("Actual_Value"))
                objtr.Qc_Status = clsCommon.myCstr(dr("Qc_Status"))
                objQCList.Add(objtr)
            Next
        End If
        Return objQCList
    End Function
End Class

Public Class clsProductionEntryFinalQCScrapItems
#Region "Variables"
    Public SNO As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_Code As String = Nothing
    Public Unit_Desc As String = Nothing
    Public Location_Code As String = Nothing
    Public Scrap_QTY As Decimal = 0
    Public Avail_FAT_Per As Decimal = 0
    Public Avail_SNF_Per As Decimal = 0
    Public Avail_FAT_KG As Decimal = 0
    Public Avail_SNF_KG As Decimal = 0
    Public Remarks As String = Nothing
    Public Comp_Code As String = Nothing

    '' production costing columns
    Public Fat_Rate As Decimal = 0
    Public SNF_Rate As Decimal = 0
    Public Fat_Amt As Decimal = 0
    Public SNF_Amt As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal obj As clsProductionEntryFinalQCHead, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = "delete from TSPL_PE_FINALQC_SCRAP_ITEM where comp_code='" + objCommonVar.CurrentCompanyCode + "' and QC_Code='" + obj.QC_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            If obj.ArrScrap IsNot Nothing AndAlso obj.ArrScrap.Count > 0 Then
                For Each objtr As clsProductionEntryFinalQCScrapItems In obj.ArrScrap
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "QC_Code", obj.QC_Code)
                    clsCommon.AddColumnsForChange(coll, "SNO", objtr.SNO)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)
                    clsCommon.AddColumnsForChange(coll, "Scrap_Qty", objtr.Scrap_QTY)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", objtr.Location_Code)
                    clsCommon.AddColumnsForChange(coll, "Avail_FAT_Per", objtr.Avail_FAT_Per)
                    clsCommon.AddColumnsForChange(coll, "Avail_FAT_KG", objtr.Avail_FAT_KG)
                    clsCommon.AddColumnsForChange(coll, "Avail_SNF_Per", objtr.Avail_SNF_Per)
                    clsCommon.AddColumnsForChange(coll, "Avail_SNF_KG", objtr.Avail_SNF_KG)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                    '' production costing columns
                    If obj.Is_Job_Work_Inward Then
                        objtr.Fat_Rate = 0
                        objtr.SNF_Rate = 0
                        objtr.Fat_Amt = 0
                        objtr.SNF_Amt = 0
                    Else
                        If objtr.Scrap_QTY > 0 Then
                            Dim objCost As New MIlkComponentType
                            objCost = clsInventoryMovementNew.GetAvgCost("MI", objtr.Item_Code, obj.CONSM_LOCATION_CODE, objtr.Scrap_QTY, objtr.Unit_Code, objtr.Avail_FAT_KG, objtr.Avail_FAT_KG, obj.QC_Date, obj.QC_Date, False, trans) 'GetBackQtyFatSNFRate(obj.QC_Code, objtr.Item_Code, trans)
                            objtr.Fat_Amt = objCost.FAT_Cost
                            objtr.SNF_Amt = objCost.SNF_Cost
                            If objtr.Avail_FAT_KG = 0 Then
                                objtr.Fat_Rate = 0
                            Else
                                objtr.Fat_Rate = Math.Round(objCost.FAT_Cost / objtr.Avail_FAT_KG, 3, MidpointRounding.AwayFromZero)
                            End If

                            If objtr.Avail_SNF_KG = 0 Then
                                objtr.SNF_Rate = 0
                            Else
                                objtr.SNF_Rate = Math.Round(objCost.SNF_Cost / objtr.Avail_SNF_KG, 3, MidpointRounding.AwayFromZero)
                            End If
                        End If
                    End If
                    clsCommon.AddColumnsForChange(coll, "Fat_Rate", objtr.Fat_Rate)
                    clsCommon.AddColumnsForChange(coll, "SNF_Rate", objtr.SNF_Rate)
                    clsCommon.AddColumnsForChange(coll, "Fat_Amt", objtr.Fat_Amt)
                    clsCommon.AddColumnsForChange(coll, "SNF_Amt", objtr.SNF_Amt)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PE_FINALQC_SCRAP_ITEM", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetBackQtyFatSNFRate(ByVal Doc_Code As String, ByVal Item_Code As String, ByVal trans As SqlTransaction) As MIlkComponentType
        Dim objCost As New MIlkComponentType
        Dim qry As String = "select AVG(Fat_Rate) as Fat_Rate,AVG(SNF_Rate) as SNF_Rate from TSPL_PE_FINALQC_ISSUE_ITEM where QC_Code='" & Doc_Code & "' and Item_Code='" & Item_Code & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            objCost.FAT_Cost = clsCommon.myCdbl(dt.Rows(0).Item("Fat_Rate"))
            objCost.SNF_Cost = clsCommon.myCdbl(dt.Rows(0).Item("SNF_Rate"))
        End If
        Return objCost
    End Function

    Public Shared Function GetPPPEScrapDetail(ByVal PRODUCTION_ENTRY_CODE As String, ByVal trans As SqlTransaction) As List(Of clsProductionEntryFinalQCScrapItems)
        Dim objScrpList As New List(Of clsProductionEntryFinalQCScrapItems)
        Dim qry As String = "select TSPL_PE_FINALQC_SCRAP_ITEM.*,TSPL_ITEM_MASTER.ITEM_DESC,TSPL_ITEM_MASTER.ITEM_TYPE,isnull(TSPL_ITEM_MASTER.Product_Type,'') as Product_Type,TSPL_UNIT_MASTER.Unit_Desc from TSPL_PE_FINALQC_SCRAP_ITEM " & _
        " left join TSPL_ITEM_MASTER on TSPL_PE_FINALQC_SCRAP_ITEM.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE  " & _
        " left join TSPL_UNIT_MASTER on TSPL_PE_FINALQC_SCRAP_ITEM.Unit_Code=TSPL_UNIT_MASTER.Unit_Code  " & _
        " where TSPL_PE_FINALQC_SCRAP_ITEM.comp_code='" + objCommonVar.CurrentCompanyCode + "' and QC_Code='" + PRODUCTION_ENTRY_CODE + "' order by sno"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsProductionEntryFinalQCScrapItems()
                objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))

                objtr.Avail_SNF_KG = dr("Avail_SNF_KG")
                objtr.Avail_SNF_Per = dr("Avail_SNF_Per")
                objtr.Avail_FAT_KG = dr("Avail_FAT_KG")
                objtr.Avail_FAT_Per = dr("Avail_FAT_Per")
                objtr.Comp_Code = clsCommon.myCstr(dr("Comp_Code"))
                objtr.Location_Code = clsCommon.myCstr(dr("Location_Code"))

                objtr.Remarks = clsCommon.myCstr(dr("Remarks"))
                objtr.SNO = clsCommon.myCdbl(dr("SNO"))

                objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                objtr.Unit_Desc = clsCommon.myCstr(dr("Unit_Desc"))
                objtr.Scrap_QTY = clsCommon.myCstr(dr("Scrap_Qty"))

                '' production costing
                objtr.Fat_Rate = clsCommon.myCdbl(dr("Fat_Rate"))
                objtr.SNF_Rate = clsCommon.myCdbl(dr("SNF_Rate"))
                objtr.Fat_Amt = clsCommon.myCdbl(dr("Fat_Amt"))
                objtr.SNF_Amt = clsCommon.myCdbl(dr("SNF_Amt"))

                objScrpList.Add(objtr)
            Next
        End If
        Return objScrpList
    End Function
End Class

Public Class clsProductionEntryFinalQCParameterDetail
#Region "Variables"
    Public QC_Code As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public FAT_Per As Double = 0
    Public SNF_Per As Double = 0
    Public QC_From As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsProductionEntryFinalQCParameterDetail), ByVal trans As SqlTransaction) As Boolean
        Dim sQuery As String = "Delete from TSPL_PE_FINALQC_PARAMETER_DETAIL where QC_Code='" & strDocNo & "'"
        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsProductionEntryFinalQCParameterDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "QC_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "FAT_Per", obj.FAT_Per)
                clsCommon.AddColumnsForChange(coll, "SNF_Per", obj.SNF_Per)
                clsCommon.AddColumnsForChange(coll, "QC_From", obj.QC_From)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PE_FINALQC_PARAMETER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsProductionEntryFinalQCParameterDetail)
        Dim arr As List(Of clsProductionEntryFinalQCParameterDetail) = Nothing
        Dim qry As String
        qry = "select TSPL_PE_FINALQC_PARAMETER_DETAIL.*,TSPL_ITEM_MASTER.Item_Desc from " & _
            "TSPL_PE_FINALQC_PARAMETER_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PE_FINALQC_PARAMETER_DETAIL.Item_Code where TSPL_PE_FINALQC_PARAMETER_DETAIL.QC_Code='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsProductionEntryFinalQCParameterDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsProductionEntryFinalQCParameterDetail = New clsProductionEntryFinalQCParameterDetail()
                obj.QC_Code = clsCommon.myCstr(dr("QC_Code"))
                obj.Line_No = clsCommon.myCstr(dr("Line_No"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                obj.SNF_Per = clsCommon.myCdbl(dr("snf_Per"))
                obj.FAT_Per = clsCommon.myCdbl(dr("fat_per"))
                obj.QC_From = clsCommon.myCstr(dr("QC_From"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsProductionEntryFinalQCQCParam
#Region "Variables"
    Public QC_Code As String = String.Empty
    Public Param_Field_Code As String = String.Empty
    Public Param_Field_Desc As String = String.Empty
    Public Param_Field_Value As String = String.Empty
    Public Param_Type As String = String.Empty
    Public LINE_NO As Integer = 0
#End Region
    
    Public Shared Function saveData(ByVal strQCNo As String, ByVal arrObj As List(Of clsProductionEntryFinalQCQCParam)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            saveData(strQCNo, arrObj, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function saveData(ByVal strQCNo As String, ByVal arrObj As List(Of clsProductionEntryFinalQCQCParam), ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "delete from TSPL_PE_FINALQC_QC_PARAMETER where QC_Code='" & strQCNo & "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Try
            Dim coll As Hashtable
            If arrObj IsNot Nothing Then
                For Each obj As clsProductionEntryFinalQCQCParam In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "QC_Code", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Code", obj.Param_Field_Code)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Desc", obj.Param_Field_Desc)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Value", obj.Param_Field_Value)
                    clsCommon.AddColumnsForChange(coll, "Param_Type", obj.Param_Type)
                    clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PE_FINALQC_QC_PARAMETER", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getData(ByVal strQCNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsProductionEntryFinalQCQCParam)
        Dim arrObj As List(Of clsProductionEntryFinalQCQCParam) = Nothing
        Try
            Dim obj As clsProductionEntryFinalQCQCParam = Nothing
            Dim qry As String = "select * from TSPL_PE_FINALQC_QC_PARAMETER where QC_Code='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsProductionEntryFinalQCQCParam)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsProductionEntryFinalQCQCParam()
                    obj.QC_Code = clsCommon.myCstr(dt.Rows(i)("QC_Code"))
                    obj.Param_Field_Code = clsCommon.myCstr(dt.Rows(i)("Param_Field_Code"))
                    obj.Param_Field_Desc = clsCommon.myCstr(dt.Rows(i)("Param_Field_Desc"))
                    obj.Param_Field_Value = clsCommon.myCstr(dt.Rows(i)("Param_Field_Value"))
                    obj.Param_Type = clsCommon.myCstr(dt.Rows(i)("Param_Type"))
                    obj.LINE_NO = clsCommon.myCdbl(dt.Rows(i)("LINE_NO"))
                    arrObj.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrObj
    End Function
End Class