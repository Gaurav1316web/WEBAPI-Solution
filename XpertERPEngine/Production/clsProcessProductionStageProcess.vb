'----------------------created by Panch Raj 01/09/2014--BM00000003484--------------
Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Telerik.WinControls

Public Class clsProcessProductionStageProcess
#Region "Variables"
    Public STAGE_PROCESS_CODE As String = Nothing
    Public STAGE_PROCESS_DATE As Date = Nothing
    Public Main_Batch_Code As String = Nothing
    Public Main_Batch_Desc As String = Nothing
    Public Loaction_Code As String = Nothing   '' only for displaying data
    Public Loaction_Desc As String = Nothing   '' only for displaying data
    Public Issue_Code As String = Nothing
    Public ManualBatchNo As String = String.Empty
    Public LINE_NO As String = String.Empty
    Public CostCenterCode As String = String.Empty
    Public ProfitCenterCode As String = String.Empty
    Public CostCenterName As String = String.Empty
    Public ProfitCenterName As String = String.Empty
    Public Issue_From_Loaction_Code As String = Nothing '' only for displaying data
    Public Issue_To_Loaction_Code As String = Nothing   '' only for displaying data
    Public Posted As Integer
    Public Section_Stage_Map_Code As String = Nothing
    Public Is_Job_Work_Inward As Boolean = False
    Public ArrBatchItem As List(Of clsProcessProductionSPBatchItemDetail) = Nothing
    Public ArrIssueItem As List(Of clsProcessProductionSPIssueItemDetail) = Nothing
    Public ArrStage As List(Of clsProcessProductionSPDetail) = Nothing
    Public ArrARItem As List(Of clsProcessProductionSPARDetail) = Nothing
    'Public ArrQC As List(Of clsProcessProductionStageProcessQCDetail) = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        'BHA/20/08/18-000465 by balwinder on 20/08/2018
        Dim qry As String = "select TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE as Code,TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_DATE as [Date], " & _
        " TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code as [Main Batch Code],TSPL_PP_BATCH_ORDER_HEAD.Location_Code as [Batch Location], " & _
        " TSPL_PP_STAGE_PROCESS_HEAD.Created_By as [Created By],TSPL_PP_STAGE_PROCESS_HEAD.Created_Date as [Created Date]," & _
        " TSPL_PP_STAGE_PROCESS_HEAD.Modified_By as [Modified By],TSPL_PP_STAGE_PROCESS_HEAD.Modified_Date as [Modified Date], " & _
        " TSPL_PP_STAGE_PROCESS_HEAD.Posted as [Is Posted] " & _
        " from TSPL_PP_STAGE_PROCESS_HEAD left outer join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code=TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code " & _
        " and tspl_pp_batch_order_head.comp_code=TSPL_PP_STAGE_PROCESS_HEAD.comp_code "
        Dim str As String = ""
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls = whrCls + " and TSPL_PP_STAGE_PROCESS_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Else
            whrCls = " TSPL_PP_STAGE_PROCESS_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        End If
        str = clsCommon.ShowSelectForm("STD", qry, "Code", whrCls, currCode, "Code", isButtonClicked, "TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_DATE")

        Return str
    End Function

    Public Shared Function SaveData(ByVal isNewEntry As Boolean, ByVal obj As clsProcessProductionStageProcess) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If SaveData(isNewEntry, obj, trans) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal isNewEntry As Boolean, ByVal obj As clsProcessProductionStageProcess, ByVal trans As SqlTransaction) As Boolean
        Dim loc As String = obj.Loaction_Code
        Dim isSaved As Boolean = True
        Dim coll As New Hashtable()

        If isNewEntry Then
            obj.STAGE_PROCESS_CODE = clsERPFuncationality.GetNextCode(trans, obj.STAGE_PROCESS_DATE, clsDocType.PPStageProcess, "", obj.Loaction_Code)
        End If
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProcessProductionStageProcess, obj.Loaction_Code, obj.STAGE_PROCESS_DATE, trans)
        clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
        clsCommon.AddColumnsForChange(coll, "STAGE_PROCESS_CODE", obj.STAGE_PROCESS_CODE)
        clsCommon.AddColumnsForChange(coll, "STAGE_PROCESS_DATE", clsCommon.GetPrintDate(obj.STAGE_PROCESS_DATE, "dd/MMM/yyyy"))

        clsCommon.AddColumnsForChange(coll, "Main_Batch_Code", obj.Main_Batch_Code)
        clsCommon.AddColumnsForChange(coll, "Issue_Code", obj.Issue_Code, True)

        clsCommon.AddColumnsForChange(coll, "Section_Stage_Map_Code", obj.Section_Stage_Map_Code, True)
        'clsCommon.AddColumnsForChange(coll, "Loaction_Code", obj.Loaction_Code)
        ''richa agarwal BHA/02/07/18-000121 7 july,2018 
        clsCommon.AddColumnsForChange(coll, "ManualBatchNo", obj.ManualBatchNo)
        ''richa agarwal againt ticket no BHA/02/07/18-000120
        clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO, True)
        clsCommon.AddColumnsForChange(coll, "CostCenterCode", obj.CostCenterCode, True)
        clsCommon.AddColumnsForChange(coll, "ProfitCenterCode", obj.ProfitCenterCode, True)
        clsCommon.AddColumnsForChange(coll, "Posted", obj.Posted)
        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
        clsCommon.AddColumnsForChange(coll, "Is_Job_Work_Inward", IIf(obj.Is_Job_Work_Inward, 1, 0))
        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_STAGE_PROCESS_HEAD", OMInsertOrUpdate.Insert, "", trans)
        Else
            HistoryUpdate(obj.STAGE_PROCESS_CODE, trans)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_STAGE_PROCESS_HEAD", OMInsertOrUpdate.Update, " TSPL_PP_STAGE_PROCESS_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "' and TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE='" + obj.STAGE_PROCESS_CODE + "'", trans)
        End If

        isSaved = isSaved AndAlso clsProcessProductionSPBatchItemDetail.SaveData(obj.STAGE_PROCESS_CODE, obj.ArrBatchItem, trans)
        isSaved = isSaved AndAlso clsProcessProductionSPIssueItemDetail.SaveData(obj.STAGE_PROCESS_CODE, obj, obj.ArrIssueItem, trans)
        isSaved = isSaved AndAlso clsProcessProductionSPDetail.SaveData(obj.STAGE_PROCESS_CODE, obj.ArrStage, trans)
        isSaved = isSaved AndAlso clsProcessProductionSPARDetail.SaveData(obj, trans, obj.Is_Job_Work_Inward)
        Return isSaved
    End Function
    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PP_STAGE_PROCESS_HEAD", "STAGE_PROCESS_CODE", "TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL", "STAGE_PROCESS_CODE", "TSPL_PP_SP_ISSUE_ITEM_DETAIL", "STAGE_PROCESS_CODE", trans)
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PP_STAGE_PROCESS_DETAIL", "STAGE_PROCESS_CODE", "TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL", "STAGE_PROCESS_CODE", trans)
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_DATE,TSPL_PP_BATCH_ORDER_HEAD.Location_Code from TSPL_PP_STAGE_PROCESS_HEAD	
            left outer join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code=TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code
            where TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE='" + strCode + "'", trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProcessProductionStageProcess, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("STAGE_PROCESS_DATE")), trans)

            End If

            Dim qry As String = "select PROD_ENTRY_CODE  from TSPL_PP_PRODUCTION_ENTRY where Batch_Code in (select Main_Batch_Code from TSPL_PP_STAGE_PROCESS_HEAD where STAGE_PROCESS_CODE='" + strCode + "')"
            Dim strFutureDoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(strFutureDoc) > 0 Then
                Throw New Exception("Production Entry No [" + strFutureDoc + "] is created.Can't Delete It")
            End If

            HistoryUpdate(strCode, trans)
            clsBatchInventory.DeleteData(clsUserMgtCode.frmProcessProductionStageProcess, strCode, trans)
            clsBatchInventoryNew.DeleteData(clsUserMgtCode.frmProcessProductionStageProcess, strCode, trans)

            qry = "delete from TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL where comp_code='" + objCommonVar.CurrentCompanyCode + "' and STAGE_PROCESS_CODE='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_SP_ISSUE_ITEM_DETAIL where comp_code='" + objCommonVar.CurrentCompanyCode + "' and STAGE_PROCESS_CODE='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_STAGE_PROCESS_DETAIL where comp_code='" + objCommonVar.CurrentCompanyCode + "' and STAGE_PROCESS_CODE='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET where comp_code='" + objCommonVar.CurrentCompanyCode + "' and STAGE_PROCESS_CODE='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL where comp_code='" + objCommonVar.CurrentCompanyCode + "' and STAGE_PROCESS_CODE='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_PP_ISSUE_HEAD set STAGE_PROCESS_CODE=null where STAGE_PROCESS_CODE='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_STAGE_PROCESS_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and STAGE_PROCESS_CODE='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "update TSPL_PP_STAGE_PROCESS_HEAD_Delete_Data set Delete_By = '" + objCommonVar.CurrentUserCode + "' where STAGE_PROCESS_CODE='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal arrloc As String, ByVal trans As SqlTransaction) As clsProcessProductionStageProcess
        Try
            Dim obj As New clsProcessProductionStageProcess()
            Dim qry As String = "select TSPL_PP_STAGE_PROCESS_HEAD.*, TSPL_PP_STAGE_PROCESS_HEAD.LINE_NO,TSPL_PP_STAGE_PROCESS_HEAD.CostCenterCode , TSPL_CostCenter_MASTER.Cost_name as [Cost_Center_Name], TSPL_PP_STAGE_PROCESS_HEAD.ProfitCenterCode  ,TSPL_PROFIT_CENTER_MASTER.Name as ProfitCenterName from TSPL_PP_STAGE_PROCESS_HEAD " & _
            " left outer join TSPL_PROFIT_CENTER_MASTER on TSPL_PROFIT_CENTER_MASTER.Code =TSPL_PP_STAGE_PROCESS_HEAD.ProfitCenterCode " & _
            " left outer join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.Cost_Code =TSPL_PP_STAGE_PROCESS_HEAD.CostCenterCode " & _
            " where Main_Batch_Code in (select batch_code from tspl_pp_batch_order_head where 2=2 "
            If clsCommon.myLen(arrloc) > 0 Then
                qry = qry & " and location_code in (" + arrloc + ")"
            End If
            qry = qry & ")"
            '"& If(arrloc  &" location_code in (" + arrloc + "))
            Select Case NavType
                Case NavigatorType.Current
                    qry += " and TSPL_PP_STAGE_PROCESS_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "' and TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and TSPL_PP_STAGE_PROCESS_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "' and TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE in (select min(STAGE_PROCESS_CODE) from TSPL_PP_STAGE_PROCESS_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and Main_Batch_Code in (select batch_code from tspl_pp_batch_order_head where location_code in (" + arrloc + ")))"
                Case NavigatorType.Last
                    qry += " and TSPL_PP_STAGE_PROCESS_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "' and TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE in (select max(STAGE_PROCESS_CODE) from TSPL_PP_STAGE_PROCESS_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and Main_Batch_Code in (select batch_code from tspl_pp_batch_order_head where location_code in (" + arrloc + ")))"
                Case NavigatorType.Next
                    qry += " and TSPL_PP_STAGE_PROCESS_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "' and TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE in (select min(STAGE_PROCESS_CODE) from TSPL_PP_STAGE_PROCESS_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and STAGE_PROCESS_CODE>'" + strCode + "' and Main_Batch_Code in (select batch_code from tspl_pp_batch_order_head where location_code in (" + arrloc + ")))"
                Case NavigatorType.Previous
                    qry += " and TSPL_PP_STAGE_PROCESS_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "' and TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE in (select max(STAGE_PROCESS_CODE) from TSPL_PP_STAGE_PROCESS_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and STAGE_PROCESS_CODE<'" + strCode + "' and Main_Batch_Code in (select batch_code from tspl_pp_batch_order_head where location_code in (" + arrloc + ")))"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.STAGE_PROCESS_CODE = clsCommon.myCstr(dt.Rows(0)("STAGE_PROCESS_CODE"))
                obj.STAGE_PROCESS_DATE = clsCommon.myCDate(dt.Rows(0)("STAGE_PROCESS_DATE"))
                obj.Posted = clsCommon.myCstr(dt.Rows(0)("Posted"))
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
                obj.Main_Batch_Code = clsCommon.myCstr(dt.Rows(0)("Main_Batch_Code"))
                obj.Loaction_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code from TSPL_PP_BATCH_ORDER_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and Batch_Code='" + obj.Main_Batch_Code + "'", trans))
                obj.Loaction_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_desc from TSPL_LOCATION_MASTER where location_code='" + obj.Loaction_Code + "'", trans))
                obj.Main_Batch_Code = clsCommon.myCstr(dt.Rows(0)("Main_Batch_Code"))
                obj.Issue_Code = clsCommon.myCstr(dt.Rows(0)("Issue_Code"))
                obj.Issue_From_Loaction_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select From_Loaction_Code from TSPL_PP_ISSUE_HEAD where  Issue_Code='" + obj.Issue_Code + "'", trans))
                obj.Issue_To_Loaction_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select To_Location_Code from TSPL_PP_ISSUE_HEAD where Issue_Code='" + obj.Issue_Code + "'", trans))

                obj.Section_Stage_Map_Code = clsCommon.myCstr(dt.Rows(0)("Section_Stage_Map_Code"))

                obj.ArrBatchItem = clsProcessProductionSPBatchItemDetail.GetPPSPBatchDetail(obj.STAGE_PROCESS_CODE, trans)
                obj.ArrIssueItem = clsProcessProductionSPIssueItemDetail.GetPPSPIssuedDetail(obj.STAGE_PROCESS_CODE, trans)
                obj.ArrStage = clsProcessProductionSPDetail.GetPPSPDetail(obj.STAGE_PROCESS_CODE, trans)
                obj.ArrARItem = clsProcessProductionSPARDetail.GetPPSPARDetail(obj.STAGE_PROCESS_CODE, trans)

            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function PostData(ByVal Form_Id As String, ByVal strCode As String, ByVal arrLoc As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = PostData(Form_Id, strCode, arrLoc, trans)
            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal Form_Id As String, ByVal strCode As String, ByVal arrLoc As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_DATE,TSPL_PP_BATCH_ORDER_HEAD.Location_Code from TSPL_PP_STAGE_PROCESS_HEAD	
            left outer join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code=TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code
            where TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE='" + strCode + "'", trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProcessProductionStageProcess, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("STAGE_PROCESS_DATE")), trans)

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return PostData(Form_Id, strCode, arrLoc, trans, "")
    End Function
    Public Shared Function PostData(ByVal Form_Id As String, ByVal strCode As String, ByVal arrLoc As String, ByVal trans As SqlTransaction, ByVal VoucherNo As String) As Boolean
        Dim obj As clsProcessProductionStageProcess = clsProcessProductionStageProcess.GetData(strCode, NavigatorType.Current, "", trans)
        SendInventoryMovementAddedRemoved(Form_Id, strCode, arrLoc, trans)

        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateJEOnProduction, clsFixedParameterCode.CreateJEOnProduction, trans)) > 0 Then
            CreateJournalEntry(trans, strCode, VoucherNo)
        End If
        HistoryUpdate(strCode, trans)
        Dim qry As String = "update TSPL_PP_STAGE_PROCESS_HEAD set Posted='1',Modified_By='" + objCommonVar.CurrentUserCode + "',Modified_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' where comp_code='" + objCommonVar.CurrentCompanyCode + "' and STAGE_PROCESS_CODE='" + strCode + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        If clsCommon.myLen(VoucherNo) <= 0 Then
            Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProcessProductionStageProcess + "'", trans))
            If clsCommon.CompairString(strNotificationOn, "P") = CompairStringResult.Equal Then
                CreateNotificationContentEMP(strCode, trans)
            End If
        End If
        Return True
    End Function

    Private Shared Function CreateNotificationContentEMP(ByVal StrDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim strNotifiContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProcessProductionStageProcess + "'", trans))
        Dim strNotifi_DetalContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Detail_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProcessProductionStageProcess + "'", trans))
        Dim strNotifiCaption As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Caption from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProcessProductionStageProcess + "'", trans))
        Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProcessProductionStageProcess + "'", trans))
        Dim strDocumentDate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Plan_Date from TSPL_PP_PRODUCTION_PLAN_HEAD where plan_code='" + StrDocNo + "'", trans))

        If clsCommon.myLen(strNotifiContent) > 0 Then
            Dim objNotification As New clsNotificationHead()
            objNotification.Notification_Text = strNotifiContent
            objNotification.Notification_Caption = strNotifiCaption
            objNotification.Notification_On = strNotificationOn
            objNotification.Notification_Detail_Text = strNotifi_DetalContent
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(StrDocNo))
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, (clsCommon.myCDate(strDocumentDate)))
            objNotification.SaveData(clsUserMgtCode.frmProcessProductionStageProcess, objNotification, trans)
            objNotification = Nothing
            Return True
        End If
        Return False
    End Function

    Public Shared Function CreateJournalEntry(ByVal trans As SqlTransaction, ByVal Doc_Code As String, Optional ByVal strVourcherNoForRecreateOnly As String = "") As Boolean
        Try
            Dim obj As clsProcessProductionStageProcess = clsProcessProductionStageProcess.GetData(Doc_Code, NavigatorType.Current, "", trans)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.STAGE_PROCESS_CODE) > 0 Then
                Dim VoucherNo As String
                If clsCommon.myLen(strVourcherNoForRecreateOnly) > 0 Then
                    VoucherNo = strVourcherNoForRecreateOnly
                Else
                    VoucherNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PR-SP' and Source_Doc_No='" & obj.STAGE_PROCESS_CODE & "'", trans))
                End If
                If obj.Is_Job_Work_Inward Then
                    If clsCommon.myLen(VoucherNo) > 0 Then
                        clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_JOURNAL_DETAILS where Voucher_No='" + VoucherNo + "' ", trans)
                        clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_JOURNAL_MASTER where Voucher_No='" + VoucherNo + "' ", trans)
                    End If
                    Return True  ''Journal Entry will not create is job work type.
                End If

                Dim ArryLstGLAC As ArrayList = New ArrayList()
                Dim qry As String = Nothing
                For Each objARTR As clsProcessProductionSPARDetail In obj.ArrARItem
                    qry = "select TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.WIP_Account " + Environment.NewLine + _
                    " from TSPL_ITEM_MASTER" + Environment.NewLine + _
                    " left join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Item_Code='" + objARTR.Item_Code + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Please set purchase account set for item code " + objARTR.Item_Code)
                    End If
                    Dim strInv_Control_Account As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
                    If clsCommon.myLen(strInv_Control_Account) <= 0 Then
                        Throw New Exception("Please set Inv Control Account of Purchase account set " + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " For item " + objARTR.Item_Code)
                    End If
                    Dim strWIP_Account As String = clsCommon.myCstr(dt.Rows(0)("WIP_Account"))
                    If clsCommon.myLen(strWIP_Account) <= 0 Then
                        Throw New Exception("Please set WIP Account of Purchase account set " + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " For item " + objARTR.Item_Code)
                    End If
                    Dim dblAmt As Decimal = objARTR.Fat_Amt + objARTR.SNF_Amt
                    Dim RI As Integer = 1
                    If Not clsCommon.CompairString(objARTR.ADD_REMOVE_TYPE, "Add") = CompairStringResult.Equal Then
                        RI = -1
                    End If
                    strWIP_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(strWIP_Account, obj.Loaction_Code, trans)
                    Dim Acc1() As String = {strWIP_Account, RI * dblAmt}
                    ArryLstGLAC.Add(Acc1)

                    Dim amt As Decimal = -1 * RI * dblAmt
                    strInv_Control_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(strInv_Control_Account, obj.Loaction_Code, trans)
                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                        Dim Acc2() As String = {strInv_Control_Account, amt}
                        ArryLstGLAC.Add(Acc2)
                    Else
                        Dim Acc2() As String = {strInv_Control_Account, amt, "", "", "", "", "", "", "I"}
                        ArryLstGLAC.Add(Acc2)

                        ' ''BHA/27/11/18-000730 by Balwinder on 18/01/2019 
                        clsInventoryMovement.UpdateInvControlAccount(obj.STAGE_PROCESS_CODE, clsUserMgtCode.frmProcessProductionStageProcess, objARTR.Item_Code, IIf(amt > 0, strInv_Control_Account, ""), IIf(amt > 0, "", strInv_Control_Account), "", trans)
                    End If
                Next

                Dim GLDesc As String = "Journal Entry Against Production Stage - Doc No." & obj.STAGE_PROCESS_CODE & " "
                If clsCommon.myLen(VoucherNo) > 0 Then
                    clsJournalMaster.FunGrnlEntryWithTrans(obj.Loaction_Code, False, VoucherNo, trans, obj.STAGE_PROCESS_DATE, GLDesc, "PR-SP", "Production Standardization", obj.STAGE_PROCESS_CODE, "Production Stage Process", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, GLDesc, "")
                Else
                    clsJournalMaster.FunGrnlEntryWithTrans(obj.Loaction_Code, False, trans, obj.STAGE_PROCESS_DATE, GLDesc, "PR-SP", "Production Standardization", obj.STAGE_PROCESS_CODE, "Production Stage Process", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , GLDesc, "")
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SendInventoryMovementAddedRemoved(ByVal Form_Id As String, ByVal strCode As String, ByVal arrLoc As String, ByVal trans As SqlTransaction) As Boolean
        '----------inventory movement entry for added removed items from milk--------------------------------------------------
        Dim isSaved As Boolean = True
        Dim obj As clsProcessProductionStageProcess = clsProcessProductionStageProcess.GetData(strCode, NavigatorType.Current, arrLoc, trans)
        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        Dim ArrInventoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
        Dim settAllowNegativeStockInDairyProduction As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStockInDairyProduction, clsFixedParameterCode.AllowNegativeStockInDairyProduction, trans)) > 0)
        If obj.ArrARItem IsNot Nothing AndAlso obj.ArrARItem.Count > 0 Then
            For Each objtr As clsProcessProductionSPARDetail In obj.ArrARItem
                Dim objInventoryMovemnt As New clsInventoryMovement()
                Dim objInventoryMovemntNew As New clsInventoryMovementNew
                Dim strProductType As String
                strProductType = clsItemMaster.GetItemProductType(objtr.Item_Code, trans)
                If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(objtr.ADD_REMOVE_TYPE, "Add") = CompairStringResult.Equal Then
                        objInventoryMovemntNew.InOut = "O"
                    Else
                        objInventoryMovemntNew.InOut = "I"
                        objInventoryMovemntNew.CalculateAvgCost = False
                    End If
                    If clsCommon.CompairString(objInventoryMovemntNew.InOut, "O") = CompairStringResult.Equal AndAlso objtr.ADD_REMOVE_QTY > 0 Then
                        Dim BalanceQty As Decimal = 0
                        Dim Stock_Qty As Decimal = 0
                        Stock_Qty = objtr.ADD_REMOVE_QTY * clsItemMaster.GetConvertionFactor(objtr.Item_Code, objtr.Unit_Code, trans)
                        '' check item balance 
                        BalanceQty = clsInventoryMovementNew.getBalance(objtr.Item_Code, clsLocation.GetMainLocationMilk(objtr.Loaction_Code, trans), objtr.Loaction_Code, objtr.STAGE_PROCESS_CODE, obj.STAGE_PROCESS_DATE, trans, objtr.Unit_Code)
                        If objtr.ADD_REMOVE_QTY > BalanceQty Then
                            If Not settAllowNegativeStockInDairyProduction Then
                                Throw New Exception("Item: " & objtr.Item_Code & ", Location:" & objtr.Loaction_Code & " Available Qty: " & BalanceQty & "  Consumed Qty: " & objtr.ADD_REMOVE_QTY & " ")
                            End If
                        End If
                    End If
                    objInventoryMovemntNew.Location_Code = objtr.Loaction_Code
                    objInventoryMovemntNew.Item_Code = objtr.Item_Code
                    objInventoryMovemntNew.Item_Desc = objtr.Item_Desc
                    objInventoryMovemntNew.Qty = objtr.ADD_REMOVE_QTY
                    objInventoryMovemntNew.UOM = objtr.Unit_Code
                    objInventoryMovemntNew.MRP = Nothing
                    objInventoryMovemntNew.Add_Cost = Nothing
                    objInventoryMovemntNew.Net_Cost = Nothing
                    If clsCommon.CompairString(objtr.Item_Type, "R") = CompairStringResult.Equal Then
                        objInventoryMovemntNew.ItemType = "RM"
                    ElseIf clsCommon.CompairString(objtr.Item_Type, "F") = CompairStringResult.Equal Then
                        objInventoryMovemntNew.ItemType = "FT"
                    Else
                        objInventoryMovemntNew.ItemType = objtr.Item_Type
                    End If
                    objInventoryMovemntNew.Basic_Cost = Nothing
                    objInventoryMovemntNew.Batch_No = obj.Main_Batch_Code
                    objInventoryMovemntNew.MFG_Date = Nothing
                    objInventoryMovemntNew.Expiry_Date = Nothing
                    '' update cost columns
                    objInventoryMovemntNew.Fat_Rate = objtr.Fat_Rate
                    objInventoryMovemntNew.Fat_Amt = objtr.Fat_Amt
                    objInventoryMovemntNew.SNF_Rate = objtr.SNF_Rate
                    objInventoryMovemntNew.SNF_Amt = objtr.SNF_Amt
                    ''===================================fat value to inventory movement
                    objInventoryMovemntNew.FAT_Per = objtr.Fat_Per
                    objInventoryMovemntNew.SNF_Per = objtr.SNF_Per
                    objInventoryMovemntNew.FAT_KG = objtr.Fat_Kg
                    objInventoryMovemntNew.SNF_KG = objtr.SNF_Kg

                    Dim cost As Decimal = objtr.Fat_Amt + objtr.SNF_Amt
                    objInventoryMovemntNew.FIFO_Cost = cost
                    objInventoryMovemntNew.Avg_Cost = cost
                    objInventoryMovemntNew.LIFO_Cost = cost
                    ''=========================================================================
                    objInventoryMovemntNew.Ref_Line_No = objtr.SNO
                    ArrInventoryMovementNew.Add(objInventoryMovemntNew)
                Else
                    If clsCommon.CompairString(objtr.ADD_REMOVE_TYPE, "Add") = CompairStringResult.Equal Then
                        objInventoryMovemnt.InOut = "O"
                    Else
                        objInventoryMovemnt.InOut = "I"
                        objInventoryMovemnt.CalculateAvgCost = False
                    End If
                    If clsCommon.CompairString(objInventoryMovemnt.InOut, "O") = CompairStringResult.Equal AndAlso objtr.ADD_REMOVE_QTY > 0 Then
                        Dim BalanceQty As Decimal = 0
                        Dim Stock_Qty As Decimal = 0
                        Stock_Qty = objtr.ADD_REMOVE_QTY * clsItemMaster.GetConvertionFactor(objtr.Item_Code, objtr.Unit_Code, trans)

                        '' check item balance 
                        BalanceQty = clsItemLocationDetails.getBalance(objtr.Item_Code, objtr.Loaction_Code, objtr.STAGE_PROCESS_CODE, obj.STAGE_PROCESS_DATE, trans, objtr.Unit_Code, 0)
                        If objtr.ADD_REMOVE_QTY > BalanceQty Then
                            If Not settAllowNegativeStockInDairyProduction Then
                                Throw New Exception("Item: " & objtr.Item_Code & ", Location:" & objtr.Loaction_Code & " Available Qty: " & BalanceQty & "  Consumed Qty: " & objtr.ADD_REMOVE_QTY & " ")
                            End If
                        End If
                    End If
                    objInventoryMovemnt.Location_Code = objtr.Loaction_Code
                    objInventoryMovemnt.Item_Code = objtr.Item_Code
                    objInventoryMovemnt.Item_Desc = objtr.Item_Desc
                    objInventoryMovemnt.Qty = objtr.ADD_REMOVE_QTY
                    objInventoryMovemnt.UOM = objtr.Unit_Code
                    objInventoryMovemnt.MRP = Nothing
                    objInventoryMovemnt.Add_Cost = Nothing
                    objInventoryMovemnt.Net_Cost = Nothing
                    If clsCommon.CompairString(objtr.Item_Type, "R") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "RM"
                    ElseIf clsCommon.CompairString(objtr.Item_Type, "F") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "FT"
                    Else
                        objInventoryMovemnt.ItemType = objtr.Item_Type
                    End If
                    objInventoryMovemnt.Basic_Cost = Nothing
                    objInventoryMovemnt.Batch_No = obj.Main_Batch_Code
                    objInventoryMovemnt.MFG_Date = Nothing
                    objInventoryMovemnt.Expiry_Date = Nothing
                    '' update cost columns
                    objInventoryMovemnt.Fat_Rate = objtr.Fat_Rate
                    objInventoryMovemnt.Fat_Amt = objtr.Fat_Amt
                    objInventoryMovemnt.SNF_Rate = objtr.SNF_Rate
                    objInventoryMovemnt.SNF_Amt = objtr.SNF_Amt
                    ''===================================fat value to inventory movement
                    objInventoryMovemnt.FAT_Per = objtr.Fat_Per
                    objInventoryMovemnt.SNF_Per = objtr.SNF_Per
                    objInventoryMovemnt.FAT_KG = objtr.Fat_Kg
                    objInventoryMovemnt.SNF_KG = objtr.SNF_Kg
                    Dim cost As Decimal = objtr.Fat_Amt + objtr.SNF_Amt
                    objInventoryMovemnt.FIFO_Cost = cost
                    objInventoryMovemnt.Avg_Cost = cost
                    objInventoryMovemnt.LIFO_Cost = cost
                    ''=========================================================================
                    objInventoryMovemnt.Ref_Line_No = objtr.SNO
                    ArrInventoryMovement.Add(objInventoryMovemnt)
                End If
            Next
            '-----------other than milk product in inventory table
            If ArrInventoryMovement.Count > 0 Then
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData(Form_Id, obj.STAGE_PROCESS_CODE, obj.STAGE_PROCESS_DATE, clsCommon.GetPrintDate(obj.STAGE_PROCESS_DATE, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            End If
            If ArrInventoryMovementNew.Count > 0 Then
                isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData(Form_Id, obj.STAGE_PROCESS_CODE, obj.STAGE_PROCESS_DATE, clsCommon.GetPrintDate(obj.STAGE_PROCESS_DATE, "dd/MM/yyyy"), ArrInventoryMovementNew, trans)
            End If

        End If
        Return isSaved
        '----------------------------------------------------------------------------------------

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
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Select document for unpost.")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_DATE,TSPL_PP_BATCH_ORDER_HEAD.Location_Code from TSPL_PP_STAGE_PROCESS_HEAD	
            left outer join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code=TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code
            where TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE='" + strCode + "'", trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProcessProductionStageProcess, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("STAGE_PROCESS_DATE")), trans)

            End If
            Dim qry As String = "select Posted,Main_Batch_Code from TSPL_PP_STAGE_PROCESS_HEAD where STAGE_PROCESS_CODE='" + strCode + "'"
            Dim Dtcheck As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If Dtcheck Is Nothing OrElse Dtcheck.Rows.Count <= 0 Then
                Throw New Exception("Wrong Docuemnt No :" + strCode)
            End If

            If clsCommon.myCdbl(Dtcheck.Rows(0)("Posted")) <= 0 Then
                Throw New Exception("Document " + strCode + " Should be posted.")
            End If
            HistoryUpdate(strCode, trans)
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProdcutionDoNotCheckForwardDocuments, clsFixedParameterCode.ProdcutionDoNotCheckForwardDocuments, trans)) <= 0 Then
                qry = "select PROD_ENTRY_CODE from TSPL_PP_PRODUCTION_ENTRY where BATCH_CODE='" + clsCommon.myCstr(Dtcheck.Rows(0)("Main_Batch_Code")) + "'"
                Dtcheck = clsDBFuncationality.getSingleValue(qry, trans)
                If Dtcheck IsNot Nothing AndAlso Dtcheck.Rows.Count > 0 Then
                    Throw New Exception("Cannot unpost document,batch is used in Production Entry No " + clsCommon.myCstr(Dtcheck.Rows(0)("PROD_ENTRY_CODE")) + ".")
                End If
            End If
            clsBatchInventory.ReverseAndUnpost(clsUserMgtCode.frmProcessProductionStageProcess, strCode, trans)
            clsBatchInventoryNew.ReverseAndUnpost(clsUserMgtCode.frmProcessProductionStageProcess, strCode, trans)

            qry = "delete from tspl_inventory_movement where trans_type='" + FormId + "' and source_doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from tspl_inventory_movement_new where trans_type='" + FormId + "' and source_doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" + strCode + "' and source_code='PR-SP')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No='" + strCode + "' and source_code='PR-SP'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_PP_STAGE_PROCESS_HEAD set Posted='0',Modified_By='" + objCommonVar.CurrentUserCode + "',Modified_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' where STAGE_PROCESS_CODE='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetItemQCParameter(ByVal Item_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim qry As String = "select TSPL_ITEM_QC_PARAMETER_MASTER.code,tspl_parameter_master.Type,TSPL_ITEM_QC_PARAMETER_MASTER.lower_range, " & _
        " TSPL_ITEM_QC_PARAMETER_MASTER.upper_range,tspl_parameter_master.description,TSPL_ITEM_QC_PARAMETER_MASTER.status, " & _
        " TSPL_ITEM_QC_PARAMETER_MASTER.value1,TSPL_ITEM_QC_PARAMETER_MASTER.value2 from TSPL_ITEM_QC_PARAMETER_MASTER " & _
        " left outer join tspl_parameter_master on TSPL_ITEM_QC_PARAMETER_MASTER.code=tspl_parameter_master.code " & _
        " where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" & Item_Code & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function
    Public Shared Function GetIssueAgainstBatch(ByVal Batch_Code As String, ByVal Doc_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim qry As String = "select Issue.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type,isnull(TSPL_ITEM_MASTER.Product_Type,'') as Product_Type,TSPL_UNIT_MASTER.Unit_Desc" & _
        " from (select TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code,TSPL_PP_ISSUE_ITEM_DETAIL.Unit_Code,sum(TSPL_PP_ISSUE_ITEM_DETAIL.Qty) as Issue_Qty, " & _
        " sum(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG) as Avail_FAT_KG,AVG(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_Pers) as Avail_FAT_Pers, " & _
        " sum(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG) as Avail_SNF_KG,AVG(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Pers) as Avail_SNF_Pers " & _
        " from TSPL_PP_ISSUE_ITEM_DETAIL " & _
        " inner join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code " & _
        " where TSPL_PP_ISSUE_HEAD.Batch_Code='" & Batch_Code & "' and TSPL_PP_ISSUE_HEAD.Is_post=1 and (TSPL_PP_ISSUE_HEAD.STAGE_PROCESS_CODE is null or TSPL_PP_ISSUE_HEAD.STAGE_PROCESS_CODE='" & Doc_Code & "') group by TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code,TSPL_PP_ISSUE_ITEM_DETAIL.Unit_Code) as Issue " & _
        " left join TSPL_ITEM_MASTER on Issue.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
        " left join TSPL_UNIT_MASTER on Issue.Unit_Code=TSPL_UNIT_MASTER.Unit_Code "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function
    Public Shared Function GetQCParameters(ByVal Batch_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim qry As String = "select ROW_NUMBER() over(order by TSPL_ITEM_QC_PARAMETER_MASTER.Code) as Sno,TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code, " & _
        " TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_QC_PARAMETER_MASTER.Code,TSPL_PARAMETER_MASTER.Description as parameterdesc," & _
        " TSPL_PARAMETER_MASTER.Type,(Case when TSPL_PARAMETER_MASTER.Nature='A' then'Alphanumeric' else " & _
        " case when TSPL_PARAMETER_MASTER.Nature='B' then'Boolean' else case when TSPL_PARAMETER_MASTER.Nature='R' then'Range' end end end) as Nature," & _
        " TSPL_ITEM_QC_PARAMETER_MASTER.Lower_range,TSPL_ITEM_QC_PARAMETER_MASTER.Upper_range,TSPL_ITEM_QC_PARAMETER_MASTER.Value1," & _
        " TSPL_ITEM_QC_PARAMETER_MASTER.Value2,TSPL_ITEM_QC_PARAMETER_MASTER.Status from TSPL_ITEM_QC_PARAMETER_MASTER " & _
        " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code " & _
        " and tspl_item_master.comp_code=tspl_item_qc_parameter_master.comp_code " & _
        " left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code " & _
        " and tspl_parameter_master.comp_code=tspl_item_qc_parameter_master.comp_code " & _
        " where tspl_item_qc_parameter_master.comp_code='" + objCommonVar.CurrentCompanyCode + "' " & _
        " and TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code in (select distinct Item_Code from TSPL_PP_ISSUE_ITEM_DETAIL " & _
        " inner join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code=TSPL_PP_ISSUE_HEAD.Issue_Code " & _
        " where TSPL_PP_ISSUE_HEAD.Batch_Code='" & Batch_Code & "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function
    Public Shared Function CheckValidCode(ByVal Doc_No As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String = "select count(*) from TSPL_PP_STAGE_PROCESS_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and STAGE_PROCESS_CODE='" + Doc_No + "'"
        Dim count As Integer = clsDBFuncationality.getSingleValue(qry, trans)
        If count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function FillStageDetail(ByVal Batch_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As ClsSectionStageMapping
        Dim qry As String = ""
        Dim obj As ClsSectionStageMapping = Nothing
        qry = " select distinct top 1 TSPL_SECTION_STAGE_MAPPING.Doc_Code  from TSPL_SECTION_STAGE_MAPPING inner join TSPL_SECTION_STAGE_MAPPING_HEAD on " & _
              " TSPL_SECTION_STAGE_MAPPING_HEAD.Doc_Code = TSPL_SECTION_STAGE_MAPPING.Doc_Code " & _
              " left join TSPL_STAGE_MASTER on TSPL_SECTION_STAGE_MAPPING.Stage_Code=TSPL_STAGE_MASTER.Stage_Code " & _
              " where TSPL_SECTION_STAGE_MAPPING_HEAD.Structure_Code in " & _
              " (select Structure_Code from TSPL_PP_BATCH_ORDER_HEAD where Batch_Code='" & Batch_Code & "') " & _
              " and TSPL_SECTION_STAGE_MAPPING_HEAD.Section_Code in (select Section_Code from TSPL_PP_BATCH_ORDER_BOM_DETAIL " & _
              " where Batch_Code= '" & Batch_Code & "') "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            If clsCommon.myLen(dt.Rows(0).Item("Doc_Code")) > 0 Then
                obj = ClsSectionStageMapping.GetData(dt.Rows(0).Item("Doc_Code"), NavigatorType.Current, trans)
            Else
                Throw New Exception("Please check section of batch and BOM with Section Stage Mapping for batch " & Batch_Code & "")
            End If
        Else
            Throw New Exception("Please check section of batch and BOM with Section Stage Mapping for batch " & Batch_Code & "")
        End If
        Return obj
    End Function
    Public Shared Function CancelData(ByVal Form_Id As String, ByVal Doc_No As String) As Boolean
        '' created by Panch Raj against ticket No- KDI/21/05/18-000322 on date 01-06-2018
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            '' table list 
            ''1. TSPL_PP_STAGE_PROCESS_HEAD
            ''2. TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL
            ''3. TSPL_PP_SP_ISSUE_ITEM_DETAIL
            ''4. TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL
            ''5. TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET
            ''6. TSPL_PP_STAGE_PROCESS_DETAIL
            ''7. TSPL_CUSTOM_FIELD_VALUES
            ''8. TSPL_INVENTORY_MOVEMENT_NEW ( no need of history)
            ''9. TSPL_INVENTORY_MOVEMENT     ( no need of history)
            ''10. TSPL_JOURNAL_DETAILS
            ''11. TSPL_JOURNAL_MASTER
            '' steps for checking the items stock and batch wise stock
            Dim obj As clsProcessProductionStageProcess = clsProcessProductionStageProcess.GetData(Doc_No, NavigatorType.Current, "", trans)
            If obj Is Nothing OrElse clsCommon.myLen(obj.STAGE_PROCESS_CODE) <= 0 Then
                Throw New Exception("Document- " & Doc_No & " not found")
            End If

            '' check batch is used in production
            qry = "select PROD_ENTRY_CODE from TSPL_PP_PRODUCTION_ENTRY where Batch_Code='" & obj.Main_Batch_Code & "'"
            Dim strProd_doc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(strProd_doc) > 0 Then
                Throw New Exception("Production Process(" & strProd_doc & ") is done against Main Batch-" & obj.Main_Batch_Code & " of Stage Process(" & Doc_No & ").")
            End If
            clsItemLocationDetails.CheckCancelInventoryBalance(Form_Id, Doc_No, trans)
            '' transfer data into cancel table

            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PP_STAGE_PROCESS_HEAD", "STAGE_PROCESS_CODE", "TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL", "STAGE_PROCESS_CODE", trans)
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PP_SP_ISSUE_ITEM_DETAIL", "STAGE_PROCESS_CODE", "TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL", "STAGE_PROCESS_CODE", trans)
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PP_STAGE_PROCESS_DETAIL", "STAGE_PROCESS_CODE", "TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET", "STAGE_PROCESS_CODE", trans)

            qry = "select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Doc_No='" & Doc_No & "'"
            Dim Voucher_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(Voucher_No) > 0 Then
                clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Voucher_No, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
            End If


            '' cancel custom field data
            clsCommonFunctionality.SaveCancelDataMultKey(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CUSTOM_FIELD_VALUES", "Transaction_Code", "Program_Code", Form_Id, trans)
            '' release issue involved in stage process
            qry = "update TSPL_PP_ISSUE_HEAD set STAGE_PROCESS_CODE=null where STAGE_PROCESS_CODE='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" & Doc_No & "' and Trans_Type='" & Form_Id & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & Doc_No & "' and Trans_Type='" & Form_Id & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" & Doc_No & "'", trans))
            If clsCommon.myLen(strVoucherNo) > 0 Then
                qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in ('" + strVoucherNo + "')"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No='" & strVoucherNo & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            strVoucherNo = Nothing
           

            qry = "delete from TSPL_CUSTOM_FIELD_VALUES where Transaction_Code='" & Doc_No & "' and Program_Code='" & Form_Id & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL where STAGE_PROCESS_CODE='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_SP_ISSUE_ITEM_DETAIL where STAGE_PROCESS_CODE='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL where STAGE_PROCESS_CODE='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET where STAGE_PROCESS_CODE='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_STAGE_PROCESS_DETAIL where STAGE_PROCESS_CODE='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_STAGE_PROCESS_HEAD where STAGE_PROCESS_CODE='" & Doc_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
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
End Class

Public Class clsProcessProductionSPBatchItemDetail
#Region "Variables"
    Public STAGE_PROCESS_CODE As String = Nothing
    Public SNO As String = Nothing
    Public BOM_Code As String = Nothing
    Public BOM_Desc As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_Code As String = Nothing

    Public Item_Type As String = Nothing
    Public Product_Type As String
    Public Unit_Desc As String = Nothing

    Public Quantity As String = Nothing
    Public Shift_Code As String = Nothing
    Public Shift_Desc As String = Nothing
    Public Section_Code As String = Nothing
    Public Section_Desc As String = Nothing

    Public Requir_FAT_per As Decimal = 0
    Public Requir_SNF_Per As Decimal = 0
    Public Requir_FAT_KG As Decimal = 0
    Public Requir_SNF_KG As Decimal = 0

    Public Produced_Qty As Decimal = 0
    Public Produced_FAT_KG As Decimal = 0
    Public Produced_SNF_KG As Decimal = 0

    Public NO_SAMPLE_QC As Decimal = 0
    Public DAMAGE_Qty As Decimal = 0
    Public FINAL_PROD_Qty As Decimal = 0
    Public SP_Loaction_Code As String = Nothing
    Public SP_Loaction_Desc As String = Nothing
    Public Comp_Code As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal STAGE_PROCESS_CODE As String, ByVal arr As List(Of clsProcessProductionSPBatchItemDetail), ByVal trans As SqlTransaction) As Boolean
        'Try
        Dim isSaved As Boolean = True
        Dim qry As String = "delete from TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL where comp_code='" + objCommonVar.CurrentCompanyCode + "' and STAGE_PROCESS_CODE='" + STAGE_PROCESS_CODE + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Dim coll As New Hashtable()

        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each objtr As clsProcessProductionSPBatchItemDetail In arr
                coll = New Hashtable()

                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "STAGE_PROCESS_CODE", STAGE_PROCESS_CODE)
                clsCommon.AddColumnsForChange(coll, "SNO", objtr.SNO)
                clsCommon.AddColumnsForChange(coll, "BOM_Code", objtr.BOM_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "Quantity", objtr.Quantity)
                clsCommon.AddColumnsForChange(coll, "Shift_Code", objtr.Shift_Code)
                clsCommon.AddColumnsForChange(coll, "Section_Code", objtr.Section_Code)

                clsCommon.AddColumnsForChange(coll, "Requir_FAT_per", objtr.Requir_FAT_per)
                clsCommon.AddColumnsForChange(coll, "Requir_FAT_KG", objtr.Requir_FAT_KG)
                clsCommon.AddColumnsForChange(coll, "Requir_SNF_Per", objtr.Requir_SNF_Per)
                clsCommon.AddColumnsForChange(coll, "Requir_SNF_KG", objtr.Requir_SNF_KG)

                clsCommon.AddColumnsForChange(coll, "Produced_Qty", objtr.Produced_Qty)
                clsCommon.AddColumnsForChange(coll, "Produced_FAT_KG", objtr.Produced_FAT_KG)
                clsCommon.AddColumnsForChange(coll, "Produced_SNF_KG", objtr.Produced_SNF_KG)

                '' NEW COLUMNS
                clsCommon.AddColumnsForChange(coll, "NO_SAMPLE_QC", objtr.NO_SAMPLE_QC)
                clsCommon.AddColumnsForChange(coll, "DAMAGE_Qty", objtr.DAMAGE_Qty)
                clsCommon.AddColumnsForChange(coll, "FINAL_PROD_Qty", objtr.FINAL_PROD_Qty)
                clsCommon.AddColumnsForChange(coll, "SP_Loaction_Code", objtr.SP_Loaction_Code, True)
                ''END NEW COLUMNS
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If

        Return isSaved
        'Catch ex As Exception
        '    Throw New Exception(ex.Message)
        'End Try
    End Function

    Public Shared Function GetPPSPBatchDetail(ByVal STAGE_PROCESS_CODE As String, ByVal trans As SqlTransaction) As List(Of clsProcessProductionSPBatchItemDetail)
        Dim objIssueList As New List(Of clsProcessProductionSPBatchItemDetail)
        Dim qry As String = "select TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.*,TSPL_ITEM_MASTER.ITEM_DESC,TSPL_ITEM_MASTER.ITEM_TYPE, " & _
        " isnull(TSPL_ITEM_MASTER.Product_Type,'') as Product_Type,TSPL_UNIT_MASTER.Unit_Desc,TSPL_SECTION_MASTER.Description as Section_Desc, " & _
        " TSPL_SHIFT_MASTER.SHIFT_NAME as Shift_Desc,tspl_pp_bom_head.Description as Bom_Desc,TSPL_LOCATION_MASTER.LOCATION_DESC AS SP_LOCATION_DESC " & _
        " from TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL " & _
        " left join TSPL_ITEM_MASTER on TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE  " & _
        " left join TSPL_UNIT_MASTER on TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.Unit_Code=TSPL_UNIT_MASTER.Unit_Code  " & _
        " left join TSPL_SECTION_MASTER on TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.Section_Code=TSPL_SECTION_MASTER.Section_Code  " & _
        " left join TSPL_SHIFT_MASTER on TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.Shift_Code=TSPL_SHIFT_MASTER.SHIFT_CODE  " & _
        " left join tspl_pp_bom_head on TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.BOM_Code=tspl_pp_bom_head.BOM_CODE " & _
        " left join TSPL_LOCATION_MASTER on TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.SP_LOACTION_CODE=TSPL_LOCATION_MASTER.LOCATION_CODE " & _
        " where TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL.comp_code='" + objCommonVar.CurrentCompanyCode + "' and STAGE_PROCESS_CODE='" + STAGE_PROCESS_CODE + "' order by sno"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsProcessProductionSPBatchItemDetail()

                objtr.SNO = CInt(dr("SNO"))
                objtr.BOM_Code = clsCommon.myCstr(dr("BOM_Code"))
                objtr.BOM_Desc = clsCommon.myCstr(dr("BOM_Desc"))
                objtr.Comp_Code = clsCommon.myCstr(dr("Comp_Code"))
                objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))

                objtr.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                objtr.Produced_FAT_KG = dr("Produced_FAT_KG")
                objtr.Produced_Qty = dr("Produced_Qty")
                objtr.Produced_SNF_KG = dr("Produced_SNF_KG")
                objtr.Product_Type = clsCommon.myCstr(dr("Product_Type"))
                objtr.Quantity = dr("Quantity")
                objtr.Requir_FAT_KG = dr("Requir_FAT_KG")
                objtr.Requir_FAT_per = dr("Requir_FAT_per")
                objtr.Requir_SNF_KG = dr("Requir_SNF_KG")
                objtr.Requir_SNF_Per = dr("Requir_SNF_Per")

                objtr.Section_Code = clsCommon.myCstr(dr("Section_Code"))
                objtr.Section_Desc = clsCommon.myCstr(dr("Section_Desc"))

                objtr.Shift_Code = clsCommon.myCstr(dr("Shift_Code"))
                objtr.Shift_Desc = clsCommon.myCstr(dr("Shift_Desc"))

                objtr.STAGE_PROCESS_CODE = clsCommon.myCstr(dr("STAGE_PROCESS_CODE"))
                objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                objtr.Unit_Desc = clsCommon.myCstr(dr("Unit_Desc"))

                '' NEW COLUMNS
                objtr.NO_SAMPLE_QC = clsCommon.myCdbl(dr("NO_SAMPLE_QC"))
                objtr.DAMAGE_Qty = clsCommon.myCdbl(dr("DAMAGE_Qty"))
                objtr.FINAL_PROD_Qty = clsCommon.myCdbl(dr("FINAL_PROD_Qty"))
                objtr.SP_Loaction_Code = clsCommon.myCstr(dr("SP_Loaction_Code"))
                objtr.SP_Loaction_Desc = clsCommon.myCstr(dr("SP_LOCATION_DESC"))
                '' END NEW COLUMNS

                objIssueList.Add(objtr)
            Next
        End If
        Return objIssueList
    End Function

End Class
Public Class clsProcessProductionSPIssueItemDetail
#Region "Variables"
    Public STAGE_PROCESS_CODE As String = Nothing
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

    Public Diff_Qty As Decimal = 0
    Public Diff_FAT_Per As Decimal = 0
    Public Diff_SNF_Per As Decimal = 0
    Public Diff_FAT_KG As Decimal = 0
    Public Diff_SNF_KG As Decimal = 0

    Public Remarks As String = Nothing
    Public Issue_Status As String = Nothing
    Public Comp_Code As String = Nothing

    '' production costing columns
    Public Fat_Rate As Decimal = 0
    Public SNF_Rate As Decimal = 0
    Public Fat_Amt As Decimal = 0
    Public SNF_Amt As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal STAGE_PROCESS_CODE As String, ByVal obj As clsProcessProductionStageProcess, ByVal arr As List(Of clsProcessProductionSPIssueItemDetail), ByVal trans As SqlTransaction) As Boolean
        'Try
        Dim isSaved As Boolean = True
        Dim qry As String = "delete from TSPL_PP_SP_ISSUE_ITEM_DETAIL where comp_code='" + objCommonVar.CurrentCompanyCode + "' and STAGE_PROCESS_CODE='" + STAGE_PROCESS_CODE + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Dim coll As New Hashtable()

        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each objtr As clsProcessProductionSPIssueItemDetail In arr
                coll = New Hashtable()

                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "STAGE_PROCESS_CODE", STAGE_PROCESS_CODE)
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

                clsCommon.AddColumnsForChange(coll, "Diff_Qty", objtr.Diff_Qty)
                clsCommon.AddColumnsForChange(coll, "Diff_FAT_Per", objtr.Diff_FAT_Per)
                clsCommon.AddColumnsForChange(coll, "Diff_FAT_KG", objtr.Diff_FAT_KG)
                clsCommon.AddColumnsForChange(coll, "Diff_SNF_Per", objtr.Diff_SNF_Per)
                clsCommon.AddColumnsForChange(coll, "Diff_SNF_KG", objtr.Diff_SNF_KG)



                clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                clsCommon.AddColumnsForChange(coll, "Issue_Status", objtr.Issue_Status)

                '' production costing columns
                clsCommon.AddColumnsForChange(coll, "Fat_Rate", objtr.Fat_Rate)
                clsCommon.AddColumnsForChange(coll, "SNF_Rate", objtr.SNF_Rate)
                clsCommon.AddColumnsForChange(coll, "Fat_Amt", objtr.Fat_Amt)
                clsCommon.AddColumnsForChange(coll, "SNF_Amt", objtr.SNF_Amt)

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_SP_ISSUE_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
            qry = "update  TSPL_PP_ISSUE_HEAD set STAGE_PROCESS_CODE='" & STAGE_PROCESS_CODE & "' where (STAGE_PROCESS_CODE is null or STAGE_PROCESS_CODE='" & STAGE_PROCESS_CODE & "') and Batch_Code='" & obj.Main_Batch_Code & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        End If

        Return isSaved
        'Catch ex As Exception
        '    Throw New Exception(ex.Message)
        'End Try
    End Function
    Public Shared Function GetPPSPIssuedDetail(ByVal STAGE_PROCESS_CODE As String, ByVal trans As SqlTransaction) As List(Of clsProcessProductionSPIssueItemDetail)
        Dim objIssueList As New List(Of clsProcessProductionSPIssueItemDetail)
        Dim qry As String = "select TSPL_PP_SP_ISSUE_ITEM_DETAIL.*,TSPL_ITEM_MASTER.ITEM_DESC,TSPL_ITEM_MASTER.ITEM_TYPE,isnull(TSPL_ITEM_MASTER.Product_Type,'') as Product_Type,TSPL_UNIT_MASTER.Unit_Desc from TSPL_PP_SP_ISSUE_ITEM_DETAIL " & _
        " left join TSPL_ITEM_MASTER on TSPL_PP_SP_ISSUE_ITEM_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE  " & _
        " left join TSPL_UNIT_MASTER on TSPL_PP_SP_ISSUE_ITEM_DETAIL.Unit_Code=TSPL_UNIT_MASTER.Unit_Code  " & _
        " where TSPL_PP_SP_ISSUE_ITEM_DETAIL.comp_code='" + objCommonVar.CurrentCompanyCode + "' and STAGE_PROCESS_CODE='" + STAGE_PROCESS_CODE + "' order by sno"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsProcessProductionSPIssueItemDetail()

                objtr.Issue_Code = clsCommon.myCstr(dr("Issue_Code"))
                objtr.From_Loaction_Code = clsCommon.myCstr(dr("From_Loaction_Code"))
                objtr.To_Location_Code = clsCommon.myCstr(dr("To_Location_Code"))
                objtr.Avail_FAT_KG = dr("Avail_FAT_KG")
                objtr.Avail_FAT_Per = dr("Avail_FAT_Per")
                objtr.Avail_Qty = dr("Avail_Qty")
                objtr.Avail_SNF_KG = dr("Avail_SNF_KG")
                objtr.Avail_SNF_Per = dr("Avail_SNF_Per")
                objtr.Comp_Code = clsCommon.myCstr(dr("Comp_Code"))
                objtr.Diff_FAT_KG = dr("Diff_FAT_KG")
                objtr.Diff_FAT_Per = dr("Diff_FAT_Per")
                objtr.Diff_Qty = dr("Diff_Qty")
                objtr.Diff_SNF_KG = dr("Diff_SNF_KG")
                objtr.Diff_SNF_Per = dr("Diff_SNF_Per")

                objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                objtr.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                objtr.Product_Type = clsCommon.myCstr(dr("Product_Type"))

                objtr.Remarks = clsCommon.myCstr(dr("Remarks"))
                objtr.Issue_Status = clsCommon.myCstr(dr("Issue_Status"))

                objtr.SNO = clsCommon.myCdbl(dr("SNO"))
                objtr.STAGE_PROCESS_CODE = clsCommon.myCstr(dr("STAGE_PROCESS_CODE"))
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
Public Class clsProcessProductionSPDetail
#Region "Variables"
    Public STAGE_PROCESS_CODE As String = Nothing
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
    Public SPQCList As New List(Of clsPPStageProcessLogSheetDetail)
    Public arrXtime As New List(Of String)
#End Region

    Public Shared Function SaveData(ByVal STAGE_PROCESS_CODE As String, ByVal arr As List(Of clsProcessProductionSPDetail), ByVal trans As SqlTransaction) As Boolean
        'Try
        Dim isSaved As Boolean = True
        Dim qry As String = "delete from TSPL_PP_STAGE_PROCESS_DETAIL where comp_code='" + objCommonVar.CurrentCompanyCode + "' and STAGE_PROCESS_CODE='" + STAGE_PROCESS_CODE + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Dim coll As New Hashtable()

        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each objtr As clsProcessProductionSPDetail In arr
                coll = New Hashtable()

                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "STAGE_PROCESS_CODE", STAGE_PROCESS_CODE)
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

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_STAGE_PROCESS_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                isSaved = isSaved AndAlso clsPPStageProcessLogSheetDetail.SaveData(STAGE_PROCESS_CODE, objtr.Stage_Code, objtr.SPQCList, trans)
            Next
        End If

        Return isSaved
        'Catch ex As Exception
        '    Throw New Exception(ex.Message)
        'End Try
    End Function

    Public Shared Function GetPPSPDetail(ByVal STAGE_PROCESS_CODE As String, ByVal trans As SqlTransaction) As List(Of clsProcessProductionSPDetail)
        Dim objARList As New List(Of clsProcessProductionSPDetail)
        Dim qry As String = "select TSPL_PP_STAGE_PROCESS_DETAIL.*, " & _
        " TSPL_UNIT_MASTER.Unit_Desc,TSPL_STAGE_MASTER.Description as Stage_Desc from TSPL_PP_STAGE_PROCESS_DETAIL " & _
        " left join TSPL_UNIT_MASTER on TSPL_PP_STAGE_PROCESS_DETAIL.Unit_Code=TSPL_UNIT_MASTER.Unit_Code  " & _
        " left join TSPL_STAGE_MASTER on TSPL_PP_STAGE_PROCESS_DETAIL.Stage_Code=TSPL_STAGE_MASTER.Stage_Code  " & _
        " where TSPL_PP_STAGE_PROCESS_DETAIL.comp_code='" + objCommonVar.CurrentCompanyCode + "' and STAGE_PROCESS_CODE='" + STAGE_PROCESS_CODE + "' order by sno"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsProcessProductionSPDetail()
                objtr.SNO = CInt(dr("SNO"))
                objtr.Comp_Code = clsCommon.myCstr(dr("Comp_Code"))
                objtr.Log_Sheet_No = clsCommon.myCstr(dr("Log_Sheet_No"))
                objtr.Received_Qty = clsCommon.myCdbl(dr("Received_Qty"))
                objtr.Remarks = clsCommon.myCstr(dr("Remarks"))
                objtr.Stage_Code = clsCommon.myCstr(dr("Stage_Code"))
                objtr.Stage_Desc = clsCommon.myCstr(dr("Stage_Desc"))
                objtr.STAGE_PROCESS_CODE = clsCommon.myCstr(dr("STAGE_PROCESS_CODE"))
                objtr.Status = clsCommon.myCstr(dr("Status"))
                objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                objtr.Section_Code = clsCommon.myCstr(dr("Section_Code"))
                objtr.Structure_Code = clsCommon.myCstr(dr("Structure_Code"))
                objtr.Batch_Code = clsCommon.myCstr(dr("Batch_Code"))
                objtr.SPQCList = clsPPStageProcessLogSheetDetail.GetPPStageProcessQCLogSheetDetail(STAGE_PROCESS_CODE, objtr.Stage_Code, objtr.Log_Sheet_No, trans)
                objtr.arrXtime = clsPPStageProcessLogSheetDetail.GetPPSPXTimeDetail(STAGE_PROCESS_CODE, objtr.Stage_Code, objtr.Log_Sheet_No, trans)
                objARList.Add(objtr)
            Next
        End If
        Return objARList
    End Function
End Class
Public Class clsPPStageProcessLogSheetDetail
#Region "Variables"
    Public STAGE_PROCESS_CODE As String = Nothing
    Public Log_Sheet_No As String = Nothing
    Public Sno As Integer = Nothing
    Public Stage_Code As String = Nothing
    Public xtime As String = Nothing
    Public param_code As String = Nothing
    Public Parameter_STD_Value As String = Nothing
    Public Parameter_ACT_Value As String = Nothing
    Public Parameter_Desc As String = Nothing
    Public Comp_Code As String = Nothing
    Public Fill_Date As Date? = Nothing
    Public QCLM_CODE As String = Nothing
    Public Batch_Code As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal StageCode As String, ByVal arr As List(Of clsPPStageProcessLogSheetDetail), ByVal trans As SqlTransaction) As Boolean
        'Try
        Dim isSaved As Boolean = True
        Dim coll As New Hashtable()

        Dim qry As String = ""
        qry = "delete from TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET where STAGE_PROCESS_CODE='" + strCode + "' " & _
                " and Stage_Code='" & StageCode & "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each objtr As clsPPStageProcessLogSheetDetail In arr
                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "STAGE_PROCESS_CODE", strCode)
                clsCommon.AddColumnsForChange(coll, "Log_Sheet_No", objtr.Log_Sheet_No)
                clsCommon.AddColumnsForChange(coll, "Stage_Code", objtr.Stage_Code)
                clsCommon.AddColumnsForChange(coll, "SNO", objtr.Sno)
                clsCommon.AddColumnsForChange(coll, "Time_Value", objtr.xtime)
                clsCommon.AddColumnsForChange(coll, "Parameter_Code", objtr.param_code, True)
                clsCommon.AddColumnsForChange(coll, "Parameter_STD_Value", objtr.Parameter_STD_Value)
                clsCommon.AddColumnsForChange(coll, "Parameter_ACT_Value", objtr.Parameter_ACT_Value)
                clsCommon.AddColumnsForChange(coll, "Fill_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Batch_Code", objtr.Batch_Code, True)
                clsCommon.AddColumnsForChange(coll, "QCLM_CODE", objtr.QCLM_CODE, True)

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If

        Return isSaved
        'Catch ex As Exception
        '    Throw New Exception(ex.Message)
        'End Try
    End Function
    Public Shared Function GetPPStageProcessQCLogSheetDetail(ByVal STAGE_PROCESS_CODE As String, ByVal Stage_Code As String, ByVal Log_Sheet_No As String, ByVal trans As SqlTransaction) As List(Of clsPPStageProcessLogSheetDetail)
        Dim objQCList As New List(Of clsPPStageProcessLogSheetDetail)
        Dim qry As String = "select TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET.Log_Sheet_No as Log_Sheet_No,TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET.Stage_Code,TSPL_PP_LOG_SHEET_DETAIL.SNO, " & _
        " TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET.Time_Value,TSPL_PARAMETER_MASTER.Description as Parameter_Desc, Parameter_Value as Parameter_STD_Value, " & _
        " TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET.Parameter_ACT_Value ,TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET.STAGE_PROCESS_CODE,TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET.Batch_Code,TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET.QCLM_CODE  " & _
        " from  (select * from TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET where STAGE_PROCESS_CODE='" + STAGE_PROCESS_CODE + "') as TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET " & _
        " left join TSPL_PP_LOG_SHEET_DETAIL on TSPL_PP_LOG_SHEET_DETAIL.Doc_No=TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET.Log_Sheet_No  " & _
        " AND TSPL_PP_LOG_SHEET_DETAIL.Parameter_Code=TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET.QCLM_CODE    " & _
        " AND TSPL_PP_LOG_SHEET_DETAIL.Time_Value=TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET.Time_Value " & _
        " left join TSPL_PARAMETER_MASTER on TSPL_PP_LOG_SHEET_DETAIL.Parameter_Code=TSPL_PARAMETER_MASTER.Code " & _
        " left join  TSPL_PP_LOG_SHEET_HEAD on TSPL_PP_LOG_SHEET_HEAD.Doc_No=TSPL_PP_LOG_SHEET_DETAIL.Doc_No " & _
        " where TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET.stage_code='" + Stage_Code + "' and TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET.Log_Sheet_No='" & Log_Sheet_No & "'  order by sno"

        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsPPStageProcessLogSheetDetail()
                objtr.Sno = clsCommon.myCdbl(dr("SNO"))
                objtr.Comp_Code = objCommonVar.CurrentCompanyCode
                objtr.Log_Sheet_No = clsCommon.myCstr(dr("Log_Sheet_No"))
                objtr.param_code = "" 'clsCommon.myCstr(dr("Parameter_Code"))

                objtr.Parameter_ACT_Value = clsCommon.myCstr(dr("Parameter_ACT_Value"))
                objtr.Parameter_STD_Value = clsCommon.myCstr(dr("Parameter_STD_Value"))
                objtr.Parameter_Desc = clsCommon.myCstr(dr("Parameter_Desc")) 'clsCommon.myCstr(dr("Parameter_Desc"))
                objtr.Parameter_STD_Value = clsCommon.myCstr(dr("Parameter_STD_Value"))
                objtr.Stage_Code = clsCommon.myCstr(dr("Stage_Code"))
                objtr.STAGE_PROCESS_CODE = clsCommon.myCstr(dr("STAGE_PROCESS_CODE"))

                objtr.xtime = clsCommon.myCstr(dr("Time_Value"))
                objtr.Batch_Code = clsCommon.myCstr(dr("Batch_Code"))
                objtr.QCLM_CODE = clsCommon.myCstr(dr("QCLM_CODE"))

                objQCList.Add(objtr)
            Next
        End If
        Return objQCList
    End Function
    Public Shared Function GetPPSPXTimeDetail(ByVal STAGE_PROCESS_CODE As String, ByVal Stage_Code As String, ByVal Log_Sheet_No As String, ByVal trans As SqlTransaction) As List(Of String)
        Dim objXTime As New List(Of String)
        Dim qry As String = "select distinct SNO,Time_Value from TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET " & _
        " where stage_code='" + Stage_Code + "' and Log_Sheet_No='" & Log_Sheet_No & "' and STAGE_PROCESS_CODE='" & STAGE_PROCESS_CODE & "'  order by sno"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                objXTime.Add(clsCommon.myCstr(dr.Item("Time_Value")))
            Next
        End If
        Return objXTime
    End Function
    'Public Shared Function GetQCLogSheetSTDDt(ByVal Log_Sheet_No As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
    '    Dim qry As String = "select Doc_No as [Log Sheet No],SNO as [Serial No],Time_Value as [Time],Parameter_Code as [Parameter Code]," & _
    '                        " Parameter_Value as [Standard Value],'' as [Actual Value]  from TSPL_PP_LOG_SHEET_DETAIL where Doc_No='" & Log_Sheet_No & "' and Comp_Code='" & objCommonVar.CurrentCompanyCode & "'"
    '    Return clsDBFuncationality.GetDataTable(qry, trans)
    'End Function
End Class
Public Class clsProcessProductionSPARDetail
#Region "Variables"
    Public STAGE_PROCESS_CODE As String = Nothing
    Public SNO As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_Code As String = Nothing

    Public Item_Type As String = Nothing
    Public Product_Type As String
    Public Unit_Desc As String = Nothing

    Public ADD_REMOVE_QTY As String = Nothing
    Public ADD_REMOVE_TYPE As String = Nothing
    Public Loaction_Code As String = Nothing
    Public Location_Desc As String = Nothing

    Public Remarks As String = Nothing
    Public Comp_Code As String = Nothing

    '' production costing columns
    Public Fat_Rate As Decimal = 0
    Public SNF_Rate As Decimal = 0
    Public Fat_Amt As Decimal = 0
    Public SNF_Amt As Decimal = 0

    Public Fat_Per As Decimal = 0
    Public SNF_Per As Decimal = 0
    Public Fat_Kg As Decimal = 0
    Public SNF_Kg As Decimal = 0
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
    Public arrBatchItemNew As List(Of clsBatchInventoryNew) = Nothing
#End Region

    Public Shared Function SaveData(ByVal objSP As clsProcessProductionStageProcess, ByVal trans As SqlTransaction, ByVal isJobWorkInward As Boolean) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = "delete from TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL where comp_code='" & objCommonVar.CurrentCompanyCode & "' and STAGE_PROCESS_CODE='" & objSP.STAGE_PROCESS_CODE & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsBatchInventory.DeleteData(clsUserMgtCode.frmProcessProductionStageProcess, objSP.STAGE_PROCESS_CODE, trans)
            clsBatchInventoryNew.DeleteData(clsUserMgtCode.frmProcessProductionStageProcess, objSP.STAGE_PROCESS_CODE, trans)
            Dim coll As New Hashtable()
            ''Calculate Avg Issue FAT/SNF Rate
            Dim issueFATKG As Decimal = 0
            Dim issueFATAmt As Decimal = 0
            Dim issueSNFKG As Decimal = 0
            Dim issueSNFAmt As Decimal = 0
            For Each objtr As clsProcessProductionSPIssueItemDetail In objSP.ArrIssueItem
                issueFATKG += objtr.Avail_FAT_KG
                issueSNFKG += objtr.Avail_SNF_KG
                issueFATAmt += objtr.Fat_Amt
                issueSNFAmt += objtr.SNF_Amt
            Next
            Dim issueAvgFATRate As Decimal = 0
            Dim issueAvgSNFRate As Decimal = 0
            If issueFATAmt > 0 And issueFATKG > 0 Then
                issueAvgFATRate = Math.Round(issueFATAmt / issueFATKG, 2, MidpointRounding.ToEven)
            End If
            If issueSNFAmt > 0 And issueSNFKG > 0 Then
                issueAvgSNFRate = Math.Round(issueSNFAmt / issueSNFKG, 2, MidpointRounding.ToEven)
            End If
            ''Enf of Calculate Avg Issue FAT/SNF Rate
            If objSP.ArrARItem IsNot Nothing AndAlso objSP.ArrARItem.Count > 0 Then
                For Each objtr As clsProcessProductionSPARDetail In objSP.ArrARItem
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "STAGE_PROCESS_CODE", objSP.STAGE_PROCESS_CODE)
                    clsCommon.AddColumnsForChange(coll, "SNO", objtr.SNO)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)
                    clsCommon.AddColumnsForChange(coll, "ADD_REMOVE_QTY", objtr.ADD_REMOVE_QTY)
                    clsCommon.AddColumnsForChange(coll, "ADD_REMOVE_TYPE", objtr.ADD_REMOVE_TYPE)
                    clsCommon.AddColumnsForChange(coll, "Loaction_Code", objtr.Loaction_Code)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                    If isJobWorkInward Then
                        objtr.Fat_Rate = 0
                        objtr.SNF_Rate = 0
                        objtr.Fat_Amt = 0
                        objtr.SNF_Amt = 0
                    Else
                        Dim objCost As New MIlkComponentType
                        If clsCommon.CompairString(objtr.ADD_REMOVE_TYPE, "Remove") = CompairStringResult.Equal Then
                            objtr.Fat_Rate = issueAvgFATRate
                            objtr.SNF_Rate = issueAvgSNFRate
                            objtr.Fat_Amt = Math.Round(issueAvgFATRate * objtr.Fat_Kg, 2, MidpointRounding.ToEven)
                            objtr.SNF_Amt = Math.Round(issueAvgSNFRate * objtr.SNF_Kg, 2, MidpointRounding.ToEven)
                        Else
                            objCost = clsInventoryMovementNew.GetAvgCost(objtr.Product_Type, objtr.Item_Code, objtr.Loaction_Code, objtr.ADD_REMOVE_QTY, objtr.Unit_Code, objtr.Fat_Kg, objtr.SNF_Kg, objSP.STAGE_PROCESS_DATE, objSP.STAGE_PROCESS_DATE, False, trans)
                            objtr.Fat_Rate = If(objtr.Fat_Kg <= 0, 0, objCost.FAT_Cost / objtr.Fat_Kg)
                            objtr.SNF_Rate = If(objtr.SNF_Kg <= 0, 0, objCost.SNF_Cost / objtr.SNF_Kg)
                            objtr.Fat_Amt = objCost.FAT_Cost
                            objtr.SNF_Amt = objCost.SNF_Cost
                        End If
                    End If
                    clsCommon.AddColumnsForChange(coll, "Fat_Rate", objtr.Fat_Rate)
                    clsCommon.AddColumnsForChange(coll, "SNF_Rate", objtr.SNF_Rate)
                    clsCommon.AddColumnsForChange(coll, "Fat_Amt", objtr.Fat_Amt)
                    clsCommon.AddColumnsForChange(coll, "SNF_Amt", objtr.SNF_Amt)
                    clsCommon.AddColumnsForChange(coll, "Fat_Per", objtr.Fat_Per)
                    clsCommon.AddColumnsForChange(coll, "SNF_Per", objtr.SNF_Per)
                    clsCommon.AddColumnsForChange(coll, "Fat_Kg", objtr.Fat_Kg)
                    clsCommon.AddColumnsForChange(coll, "SNF_Kg", objtr.SNF_Kg)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                    If clsCommon.CompairString(clsItemMaster.GetItemProductType(objtr.Item_Code, trans), "MI") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(objtr.ADD_REMOVE_TYPE, "Remove") = CompairStringResult.Equal Then
                            clsBatchInventoryNew.SaveData(clsUserMgtCode.frmProcessProductionStageProcess, objSP.STAGE_PROCESS_CODE, objSP.STAGE_PROCESS_DATE, "I", objtr.Item_Code, objtr.Loaction_Code, objtr.SNO, 0, objtr.Unit_Code, objtr.arrBatchItemNew, trans)
                        Else
                            clsBatchInventoryNew.SaveData(clsUserMgtCode.frmProcessProductionStageProcess, objSP.STAGE_PROCESS_CODE, objSP.STAGE_PROCESS_DATE, "O", objtr.Item_Code, objtr.Loaction_Code, objtr.SNO, 0, objtr.Unit_Code, objtr.arrBatchItemNew, trans)
                        End If
                    Else
                        If clsCommon.CompairString(objtr.ADD_REMOVE_TYPE, "Remove") = CompairStringResult.Equal Then
                            clsBatchInventory.SaveData(clsUserMgtCode.frmProcessProductionStageProcess, objSP.STAGE_PROCESS_CODE, objSP.STAGE_PROCESS_DATE, "I", objtr.Item_Code, objtr.Loaction_Code, objtr.SNO, 0, objtr.Unit_Code, objtr.arrBatchItem, trans)
                        Else
                            clsBatchInventory.SaveData(clsUserMgtCode.frmProcessProductionStageProcess, objSP.STAGE_PROCESS_CODE, objSP.STAGE_PROCESS_DATE, "O", objtr.Item_Code, objtr.Loaction_Code, objtr.SNO, 0, objtr.Unit_Code, objtr.arrBatchItem, trans)
                        End If
                    End If
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetPPSPARDetail(ByVal STAGE_PROCESS_CODE As String, ByVal trans As SqlTransaction) As List(Of clsProcessProductionSPARDetail)
        Dim objARList As New List(Of clsProcessProductionSPARDetail)
        Dim qry As String = "select TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.*,TSPL_ITEM_MASTER.ITEM_DESC,TSPL_ITEM_MASTER.ITEM_TYPE,TSPL_ITEM_MASTER.Product_Type,TSPL_UNIT_MASTER.Unit_Desc,TSPL_LOCATION_MASTER.Location_Desc from TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL " & _
        " left join TSPL_ITEM_MASTER on TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE  " & _
        " left join TSPL_UNIT_MASTER on TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Unit_Code=TSPL_UNIT_MASTER.Unit_Code  " & _
        " left join TSPL_LOCATION_MASTER on TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Loaction_Code=TSPL_LOCATION_MASTER.Location_Code  " & _
        " where TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.comp_code='" + objCommonVar.CurrentCompanyCode + "' and STAGE_PROCESS_CODE='" + STAGE_PROCESS_CODE + "' order by sno"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsProcessProductionSPARDetail()
                objtr.SNO = CInt(dr("SNO"))
                objtr.Comp_Code = clsCommon.myCstr(dr("Comp_Code"))
                objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                objtr.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                objtr.ADD_REMOVE_QTY = dr("ADD_REMOVE_QTY")
                objtr.ADD_REMOVE_TYPE = clsCommon.myCstr(dr("ADD_REMOVE_TYPE"))
                objtr.Loaction_Code = clsCommon.myCstr(dr("Loaction_Code"))
                objtr.Location_Desc = clsCommon.myCstr(dr("Location_Desc"))
                objtr.Product_Type = clsCommon.myCstr(dr("Product_Type"))
                objtr.Remarks = clsCommon.myCstr(dr("Remarks"))

                objtr.STAGE_PROCESS_CODE = clsCommon.myCstr(dr("STAGE_PROCESS_CODE"))
                'objtr.Standardization_Code = clsCommon.myCstr(dr("Standardization_Code"))
                objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                objtr.Unit_Desc = clsCommon.myCstr(dr("Unit_Desc"))

                objtr.Fat_Rate = clsCommon.myCdbl(dr("Fat_Rate"))
                objtr.SNF_Rate = clsCommon.myCdbl(dr("SNF_Rate"))
                objtr.Fat_Amt = clsCommon.myCdbl(dr("Fat_Amt"))
                objtr.SNF_Amt = clsCommon.myCdbl(dr("SNF_Amt"))

                objtr.Fat_Per = clsCommon.myCdbl(dr("Fat_Per"))
                objtr.SNF_Per = clsCommon.myCdbl(dr("SNF_Per"))
                objtr.Fat_Kg = clsCommon.myCdbl(dr("Fat_Kg"))
                objtr.SNF_Kg = clsCommon.myCdbl(dr("SNF_Kg"))
                objtr.arrBatchItem = clsBatchInventory.GetData(clsUserMgtCode.frmProcessProductionStageProcess, objtr.STAGE_PROCESS_CODE, objtr.Item_Code, objtr.SNO, trans)
                objtr.arrBatchItemNew = clsBatchInventoryNew.GetData(clsUserMgtCode.frmProcessProductionStageProcess, objtr.STAGE_PROCESS_CODE, objtr.Item_Code, objtr.SNO, trans)
                objARList.Add(objtr)
            Next
        End If
        Return objARList
    End Function
End Class