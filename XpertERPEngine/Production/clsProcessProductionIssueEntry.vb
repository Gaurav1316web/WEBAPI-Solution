'----------------------created by Monika 08/08/2014--BM00000003198--------------
Imports common
Imports System.Data.SqlClient

Public Class clsProcessProductionIssueEntry
#Region "Variables"
    Public issuecode As String = Nothing
    Public issue_date As Date = Nothing
    Public issuedesc As String = Nothing
    Public batch_code As String = Nothing
    Public loc_code As String = Nothing
    Public loc_name As String = Nothing
    Public frm_loc_code As String = Nothing
    Public frm_loc_desc As String = Nothing
    Public to_loc_code As String = Nothing
    Public to_loc_name As String = Nothing
    Public status As String = Nothing
    Public is_post As String = Nothing
    Public Main_Loc_Code As String = Nothing
    Public Main_Loc_Name As String = Nothing
    Public Rbtn_Frm_Sub As Integer = Nothing
    Public Rbtn_To_Sub As Integer = Nothing
    Public Against_BO As Integer = Nothing
    Public Section_Code As String = Nothing
    Public Section_Name As String = Nothing
    Public ManualBatchNo As String = Nothing
    Public LINE_NO As String = String.Empty
    Public CostCenterCode As String = String.Empty
    Public ProfitCenterCode As String = String.Empty
    Public CostCenterName As String = String.Empty
    Public ProfitCenterName As String = String.Empty
    Public Is_Job_Work_Inward As Boolean = False
    Public ArrItem As List(Of clsProcessProductionIssueItemDetail) = Nothing
    Public ArrQC As List(Of clsProcessProductionIssueQCDetail) = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "select TSPL_PP_ISSUE_HEAD.Issue_Code as Code,TSPL_PP_ISSUE_HEAD.Issue_Date as [Issue Date],TSPL_PP_ISSUE_HEAD.Description,(case when TSPL_PP_ISSUE_HEAD.against_bo=1 then 'With BO' else 'Without BO' end) as Type,TSPL_PP_ISSUE_HEAD.Status,(case when TSPL_PP_ISSUE_HEAD.is_post='1' then 'Posted' else 'UnPosted' end) as [Post Status],TSPL_PP_ISSUE_HEAD.Batch_Code as [Batch Code],TSPL_PP_BATCH_ORDER_HEAD.Location_Code as [Batch Location] from TSPL_PP_ISSUE_HEAD left outer join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code=TSPL_PP_ISSUE_HEAD.Batch_Code  "
        Dim str As String = ""
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls = whrCls
        Else
            whrCls = " " 'tspl_pp_issue_head.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        End If
        str = clsCommon.ShowSelectForm("ISSFND", qry, "Code", whrCls, currCode, "Code", isButtonClicked, "TSPL_PP_ISSUE_HEAD.Issue_Date")

        Return str
    End Function

    Public Shared Function GetBOSectionLocationCode(ByVal BO_Code As String, ByVal Main_Location_code As String, ByVal Item_Code As String) As String
        Return GetBOSectionLocationCode(BO_Code, Main_Location_code, Item_Code, Nothing)
    End Function
    Public Shared Function GetBOSectionLocationCode(ByVal BO_Code As String, ByVal Main_Location_code As String, ByVal Item_Code As String, ByVal tran As SqlTransaction) As String
        Dim str As String = "select distinct (select ','''+location_code+'''' from tspl_location_master where isnull(Is_Consumption_Location,0)=1 and isnull(Is_Section,'N')='Y' and main_location_code='" + Main_Location_code + "' and section_code in ( "
        str += "select TSPL_PP_BATCH_ORDER_BOM_DETAIL.section_code from TSPL_PP_BATCH_ORDER_BOM_DETAIL where TSPL_PP_BATCH_ORDER_BOM_DETAIL.batch_code='" + BO_Code + "' "
        str += " and TSPL_PP_BATCH_ORDER_BOM_DETAIL.item_code in (select TSPL_PP_BOM_HEAD.prod_item_code from TSPL_PP_BOM_HEAD left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.bom_code=tspl_pp_bom_head.bom_code where TSPL_PP_BOM_ITEM_DETAIL.item_code='" + Item_Code + "' and TSPL_PP_BOM_HEAD.bom_code in (select bom_code from TSPL_PP_BATCH_ORDER_BOM_DETAIL where batch_code='" + BO_Code + "'))"
        str += ") for xml path('') ) "

        If clsCommon.myLen(BO_Code) <= 0 Then
            str = "select distinct (select ','''+location_code+'''' from tspl_location_master where isnull(Is_Consumption_Location,0)=1 and isnull(Is_Section,'N')='Y' and main_location_code='" + Main_Location_code + "' "
            str += " for xml path('') ) "
        End If

        Dim location As String = ""
        location = clsCommon.myCstr(clsDBFuncationality.getSingleValue(str, tran))

        If clsCommon.myLen(location) > 0 AndAlso location.Substring(0, 1) = "," Then
            location = location.Substring(1, location.Length - 1)
        End If

 

        Return location
    End Function

    Public Shared Function GetLocationType() As DataTable
        Return GetLocationType(Nothing)
    End Function
    Public Shared Function GetLocationType(ByVal tran As SqlTransaction) As DataTable
        Return GetLocationType(tran, False)
    End Function
    Public Shared Function GetLocationType(ByVal tran As SqlTransaction, ByVal FirstSubLocationinOrder As Boolean) As DataTable
        Dim qry As String = "select 'MAIN' as Code,'Main Location' as Name union all select 'SUB' as Code,'Sub-Location' as Name union all select 'SEC' as Code,'Section-wise Tanker' as Name"
        If FirstSubLocationinOrder Then
            qry = "select  'SUB' as Code,'Sub-Location' as Name union all select 'MAIN' as Code,'Main Location' as Name union all select 'SEC' as Code,'Section-wise Tanker' as Name"
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        Return dt
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


            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Issue_Date,Main_Location_Code from TSPL_PP_ISSUE_HEAD where issue_code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProcessProductionIssueEntry, clsCommon.myCstr(dt.Rows(0)("Main_Location_Code")), clsCommon.myCDate(dt.Rows(0)("Issue_Date")), trans)
            End If
            Dim qry As String = "select count(*) from TSPL_PP_ISSUE_HEAD where Is_Post='0' and issue_code='" + strCode + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

            If check > 0 Then
                Throw New Exception("Current document is not posted.")
            End If


            qry = "select count(*) from TSPL_PP_STAGE_PROCESS_HEAD where issue_code='" + strCode + "'"
            check = clsDBFuncationality.getSingleValue(qry, trans)
            If check > 0 Then
                Throw New Exception("Cannot unpost document,is used in Stage Process.")
            End If

            qry = "select count(*) from TSPL_PP_SP_ISSUE_ITEM_DETAIL where issue_code='" + strCode + "'"
            check = clsDBFuncationality.getSingleValue(qry, trans)
            If check > 0 Then
                Throw New Exception("Cannot unpost document,is used in Standardization.")
            End If


            clsBatchInventory.ReverseAndUnpost(clsUserMgtCode.frmProcessProductionIssueEntry, strCode, trans)
            clsBatchInventoryNew.ReverseAndUnpost(clsUserMgtCode.frmProcessProductionIssueEntry, strCode, trans)



            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" + strCode + "' and Source_Code='PR-IS' ", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
            End If
            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" + strCode + "' and Source_Code='PR-IS')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No ='" + strCode + "' and Source_Code='PR-IS'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_INVENTORY_MOVEMENT", "Source_Doc_No", trans)
            qry = "delete from tspl_inventory_movement where trans_type='" + FormId + "' and source_doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "tspl_inventory_movement_new", "Source_Doc_No", trans)
            qry = "delete from tspl_inventory_movement_new where trans_type='" + FormId + "' and source_doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_PP_ISSUE_HEAD set Is_post='0',Modified_By='" + objCommonVar.CurrentUserCode + "',Modified_Date='" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")) + "' where issue_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PP_ISSUE_HEAD", "issue_code", trans)

            Dim BOCOde As String = ""
            BOCOde = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select batch_code from TSPL_PP_ISSUE_HEAD where issue_code='" + strCode + "'", trans))

            While (clsCommon.myLen(BOCOde) > 0)
                qry = "update TSPL_PP_BATCH_ORDER_HEAD set Closed_YN='0' where batch_code in ('" + BOCOde + "')"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                BOCOde = clsProcessBatchOrder.GetMainBO(BOCOde, trans)
            End While



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal isNewEntry As Boolean, ByVal obj As clsProcessProductionIssueEntry) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso SaveData(isNewEntry, obj, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal isNewEntry As Boolean, ByVal obj As clsProcessProductionIssueEntry, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()

            If isNewEntry Then
                obj.issuecode = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.issue_date, "dd/MM/yyyy"), clsDocType.PPISSUEENTRY, "", obj.Main_Loc_Code)
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProcessProductionIssueEntry, obj.Main_Loc_Code, obj.issue_date, trans)
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Issue_Code", obj.issuecode)
            clsCommon.AddColumnsForChange(coll, "Issue_Date", clsCommon.GetPrintDate(obj.issue_date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.issuedesc)
            clsCommon.AddColumnsForChange(coll, "Status", obj.status)
            clsCommon.AddColumnsForChange(coll, "Batch_Code", obj.batch_code, True)
            clsCommon.AddColumnsForChange(coll, "Main_Location_Code", obj.Main_Loc_Code, True)
            clsCommon.AddColumnsForChange(coll, "From_SubLocation_YN", obj.Rbtn_Frm_Sub)
            clsCommon.AddColumnsForChange(coll, "To_SubLocation_YN", obj.Rbtn_To_Sub)
            clsCommon.AddColumnsForChange(coll, "From_Loaction_Code", obj.frm_loc_code)
            clsCommon.AddColumnsForChange(coll, "To_Location_Code", obj.to_loc_code)
            clsCommon.AddColumnsForChange(coll, "Is_post", obj.is_post)
            clsCommon.AddColumnsForChange(coll, "Section_Code", obj.Section_Code, True)
            ''richa agarwal BHA/02/07/18-000121 7 july,2018 
            clsCommon.AddColumnsForChange(coll, "ManualBatchNo", obj.ManualBatchNo)
            ''richa agarwal againt ticket no BHA/02/07/18-000120
            clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO, True)
            clsCommon.AddColumnsForChange(coll, "CostCenterCode", obj.CostCenterCode, True)
            clsCommon.AddColumnsForChange(coll, "ProfitCenterCode", obj.ProfitCenterCode, True)
            ''------------------
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")))

            clsCommon.AddColumnsForChange(coll, "Against_BO", obj.Against_BO)
            clsCommon.AddColumnsForChange(coll, "Is_Job_Work_Inward", IIf(obj.Is_Job_Work_Inward, 1, 0))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_ISSUE_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                HistoryUpdate(obj.issuecode, trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_ISSUE_HEAD", OMInsertOrUpdate.Update, " TSPL_PP_ISSUE_HEAD.issue_code='" + obj.issuecode + "'", trans)
            End If

            isSaved = isSaved AndAlso clsProcessProductionIssueItemDetail.SaveData(obj, trans)
            isSaved = isSaved AndAlso clsProcessProductionIssueQCDetail.SaveData(obj.issuecode, obj.ArrQC, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PP_ISSUE_HEAD", "issue_code", "TSPL_PP_ISSUE_ITEM_DETAIL", "issue_code", "TSPL_PP_ISSUE_QC_DETAIL", "issue_code", trans)
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso DeleteData(strCode, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Issue_Date,Main_Location_Code from TSPL_PP_ISSUE_HEAD where issue_code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProcessProductionIssueEntry, clsCommon.myCstr(dt.Rows(0)("Main_Location_Code")), clsCommon.myCDate(dt.Rows(0)("Issue_Date")), trans)
            End If

            Dim qry = "select * from (" + Environment.NewLine +
            "select Standardization_Code as Code,'Production Std.' as Type from TSPL_PP_STANDARDIZATION_HEAD where Main_Batch_Code in (select Batch_Code from TSPL_PP_ISSUE_HEAD where Issue_Code='" + strCode + "')" + Environment.NewLine +
            "union all" + Environment.NewLine +
            "select STAGE_PROCESS_CODE as Code,'Stage Process' as Type from TSPL_PP_STAGE_PROCESS_HEAD where Main_Batch_Code in (select Batch_Code from TSPL_PP_ISSUE_HEAD where Issue_Code='" + strCode + "')" + Environment.NewLine +
            ")x"
            Dim dtFutureDoc As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtFutureDoc IsNot Nothing AndAlso dtFutureDoc.Rows.Count > 0 Then
                Throw New Exception("" + clsCommon.myCstr(dtFutureDoc.Rows(0)("Type")) + " No [" + clsCommon.myCstr(dtFutureDoc.Rows(0)("Code")) + "] is created.Can't Delete It")
            End If


            HistoryUpdate(strCode, trans)
            clsBatchInventory.DeleteData(clsUserMgtCode.frmProcessProductionIssueEntry, strCode, trans)
            clsBatchInventoryNew.DeleteData(clsUserMgtCode.frmProcessProductionIssueEntry, strCode, trans)

            qry = "delete from TSPL_PP_ISSUE_QC_DETAIL where issue_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_ISSUE_ITEM_DETAIL where issue_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim VoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PR-IS' and Source_Doc_No='" & strCode & "'", trans))
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            qry = "delete from TSPL_PP_ISSUE_HEAD where issue_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_PP_ISSUE_HEAD_Delete_Data set Delete_By = '" + objCommonVar.CurrentUserCode + "' where issue_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function CancelData(ByVal Form_Id As String, ByVal Doc_No As String) As Boolean
        Dim qry As String = ""
        Dim check As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            Dim obj As clsProcessProductionIssueEntry = clsProcessProductionIssueEntry.GetData(Doc_No, "", NavigatorType.Current, trans)
            If obj Is Nothing OrElse clsCommon.myLen(obj.issuecode) <= 0 Then
                Throw New Exception("Document- " & Doc_No & " not found")
            End If

            'If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProdcutionDoNotCheckForwardDocuments, clsFixedParameterCode.ProdcutionDoNotCheckForwardDocuments, trans)) <= 0 Then
            qry = "select STAGE_PROCESS_CODE from TSPL_PP_STAGE_PROCESS_HEAD where main_batch_code='" + obj.batch_code + "'"
            check = clsDBFuncationality.getSingleValue(qry, trans)
            If clsCommon.myLen(check) > 0 Then
                Throw New Exception("Cannot cancel document [" + check + "].It is used in Stage Process.")
            End If

            qry = "select Standardization_Code from TSPL_PP_STANDARDIZATION_HEAD where child_Batch_code='" + obj.batch_code + "' or Main_Batch_Code='" + obj.batch_code + "'"
            check = clsDBFuncationality.getSingleValue(qry, trans)
            If clsCommon.myLen(check) > 0 Then
                Throw New Exception("Cannot cancel document [" + check + "].It is used in Standardization.")
            End If
            'End If


            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PP_ISSUE_QC_DETAIL", "issue_code", trans)
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PP_ISSUE_ITEM_DETAIL", "issue_code", trans)
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PP_ISSUE_HEAD", "issue_code", trans)


            '' cancel custom field data
            clsCommonFunctionality.SaveCancelDataMultKey(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CUSTOM_FIELD_VALUES", "Transaction_Code", "Program_Code", Form_Id, trans)

            clsBatchInventory.DeleteData(clsUserMgtCode.frmProcessProductionIssueEntry, Doc_No, trans)
            clsBatchInventoryNew.DeleteData(clsUserMgtCode.frmProcessProductionIssueEntry, Doc_No, trans)

            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" + Doc_No + "' and Source_Code='PR-IS')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No ='" + Doc_No + "' and Source_Code='PR-IS'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from tspl_inventory_movement where trans_type='" + Form_Id + "' and source_doc_no='" + Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from tspl_inventory_movement_new where trans_type='" + Form_Id + "' and source_doc_no='" + Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_ISSUE_QC_DETAIL where issue_code='" + Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_ISSUE_ITEM_DETAIL where issue_code='" + Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_ISSUE_HEAD where issue_code='" + Doc_No + "'"
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

    Public Shared Function GetData(ByVal strCode As String, ByVal arrloc As String, ByVal NavType As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsProcessProductionIssueEntry
        Try
            Dim obj As New clsProcessProductionIssueEntry()
            Dim qry As String = "select TSPL_PP_ISSUE_HEAD.*, TSPL_PP_ISSUE_HEAD.LINE_NO,TSPL_PP_ISSUE_HEAD.CostCenterCode , TSPL_CostCenter_MASTER.Cost_name as [Cost_Center_Name], TSPL_PP_ISSUE_HEAD.ProfitCenterCode  ,TSPL_PROFIT_CENTER_MASTER.Name as ProfitCenterName from TSPL_PP_ISSUE_HEAD" & _
                  " left outer join TSPL_PROFIT_CENTER_MASTER on TSPL_PROFIT_CENTER_MASTER.Code =TSPL_PP_ISSUE_HEAD.ProfitCenterCode " & _
            " left outer join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.Cost_Code =TSPL_PP_ISSUE_HEAD.CostCenterCode "
            Dim LocCond As String = "where 1=1 "
            If clsCommon.myLen(arrloc) > 0 Then
                LocCond = LocCond & " and TSPL_PP_ISSUE_HEAD.main_location_code in (" & arrloc & ")"
            End If
            Select Case NavType
                Case NavigatorType.Current
                    qry += " " & LocCond & " and TSPL_PP_ISSUE_HEAD.issue_code='" & strCode & "'"
                Case NavigatorType.First
                    qry += " " & LocCond & " and TSPL_PP_ISSUE_HEAD.issue_code in (select min(TSPL_PP_ISSUE_HEAD.issue_code) from TSPL_PP_ISSUE_HEAD " & LocCond & ")"
                Case NavigatorType.Last
                    qry += " " & LocCond & " and TSPL_PP_ISSUE_HEAD.issue_code in (select max(TSPL_PP_ISSUE_HEAD.issue_code) from TSPL_PP_ISSUE_HEAD " & LocCond & ")"
                Case NavigatorType.Next
                    qry += " " & LocCond & " and TSPL_PP_ISSUE_HEAD.issue_code in (select min(TSPL_PP_ISSUE_HEAD.issue_code) from TSPL_PP_ISSUE_HEAD " & LocCond & " and TSPL_PP_ISSUE_HEAD.issue_code>'" & strCode & "')"
                Case NavigatorType.Previous
                    qry += " " & LocCond & " and TSPL_PP_ISSUE_HEAD.issue_code in (select max(TSPL_PP_ISSUE_HEAD.issue_code) from TSPL_PP_ISSUE_HEAD " & LocCond & " and TSPL_PP_ISSUE_HEAD.issue_code<'" & strCode & "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.issuecode = clsCommon.myCstr(dt.Rows(0)("Issue_Code"))
                obj.issue_date = clsCommon.myCDate(dt.Rows(0)("Issue_Date"))
                obj.issuedesc = clsCommon.myCstr(dt.Rows(0)("Description"))
                obj.status = clsCommon.myCstr(dt.Rows(0)("Status"))
                obj.is_post = clsCommon.myCstr(dt.Rows(0)("Is_post"))
                obj.batch_code = clsCommon.myCstr(dt.Rows(0)("Batch_Code"))
                obj.Main_Loc_Code = clsCommon.myCstr(dt.Rows(0)("Main_Location_Code"))
                obj.Section_Code = clsCommon.myCstr(dt.Rows(0)("Section_Code"))
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
                obj.Section_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_section_master where section_code='" + obj.Section_Code + "'", trans))
                obj.Rbtn_Frm_Sub = CInt(dt.Rows(0)("From_SubLocation_YN"))
                obj.Rbtn_To_Sub = CInt(dt.Rows(0)("To_SubLocation_YN"))
                obj.loc_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + obj.batch_code + "'", trans))
                obj.loc_name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_desc from TSPL_LOCATION_MASTER where location_code='" + obj.loc_code + "'", trans))
                obj.frm_loc_code = clsCommon.myCstr(dt.Rows(0)("From_Loaction_Code"))
                obj.frm_loc_desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_desc from TSPL_LOCATION_MASTER where location_code='" + obj.frm_loc_code + "'", trans))
                obj.to_loc_code = clsCommon.myCstr(dt.Rows(0)("To_Location_Code"))
                obj.to_loc_name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_desc from TSPL_LOCATION_MASTER where location_code='" + obj.to_loc_code + "'", trans))

                obj.Against_BO = CInt(clsCommon.myCdbl(dt.Rows(0)("Against_BO")))

                obj.ArrItem = New List(Of clsProcessProductionIssueItemDetail)
                obj.ArrQC = New List(Of clsProcessProductionIssueQCDetail)

                qry = "select * from TSPL_PP_ISSUE_ITEM_DETAIL where issue_code='" + obj.issuecode + "' order by sno"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsProcessProductionIssueItemDetail()

                        objtr.sno = CInt(dr("SNO"))
                        objtr.From_SubLocation_YN = clsCommon.myCstr(dr("From_SubLocation_YN"))
                        objtr.To_SubLocation_YN = clsCommon.myCstr(dr("To_SubLocation_YN"))
                        objtr.frm_loc_code = clsCommon.myCstr(dr("From_Loaction_Code"))
                        objtr.frm_loc_desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + objtr.frm_loc_code + "'", trans))
                        objtr.to_loc_code = clsCommon.myCstr(dr("To_Location_Code"))
                        objtr.to_loc_desc = clsLocation.GetName(objtr.to_loc_code, trans)
                        objtr.itemcode = clsCommon.myCstr(dr("Item_Code"))
                        objtr.itemname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + objtr.itemcode + "'", trans))
                        objtr.item_type = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + objtr.itemcode + "'", trans))
                        objtr.product_type = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select product_type from tspl_item_master where item_code='" + objtr.itemcode + "'", trans))
                        objtr.uom_code = clsCommon.myCstr(dr("Unit_Code"))
                        objtr.uom_desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_desc from tspl_unit_master where unit_code='" + objtr.uom_code + "'", trans))
                        objtr.avail_qty = clsCommon.myCdbl(dr("Avail_Qty"))
                        objtr.avail_fat_kg = clsCommon.myCdbl(dr("Avail_FAT_KG"))
                        objtr.avail_fat_pers = clsCommon.myCdbl(dr("Avail_FAT_Pers"))
                        objtr.avail_snf_kg = clsCommon.myCdbl(dr("Avail_SNF_KG"))
                        objtr.avail_snf_pers = clsCommon.myCdbl(dr("Avail_SNF_Pers"))
                        objtr.req_qty = clsCommon.myCdbl(dr("Required_Qty"))
                        objtr.issue_qty = clsCommon.myCdbl(dr("Qty"))
                        objtr.fat_kg = clsCommon.myCdbl(dr("FAT_KG"))
                        objtr.fat_pers = clsCommon.myCdbl(dr("FAT_Pers"))
                        objtr.snf_kg = clsCommon.myCdbl(dr("SNF_KG"))
                        objtr.snf_pers = clsCommon.myCdbl(dr("SNF_Pers"))
                        objtr.remarks = clsCommon.myCstr(dr("Remarks"))
                        objtr.Fat_Rate = clsCommon.myCdbl(dr("Fat_Rate"))
                        objtr.SNF_Rate = clsCommon.myCdbl(dr("SNF_Rate"))

                        objtr.Fat_Amt = clsCommon.myCdbl(dr("Fat_Amt"))
                        objtr.SNF_Amt = clsCommon.myCdbl(dr("SNF_Amt"))

                        objtr.arrBatchItem = clsBatchInventory.GetData(clsUserMgtCode.frmProcessProductionIssueEntry, obj.issuecode, objtr.itemcode, objtr.sno, trans, "OnlyOutType")
                        objtr.arrBatchItemNew = clsBatchInventoryNew.GetData(clsUserMgtCode.frmProcessProductionIssueEntry, obj.issuecode, objtr.itemcode, objtr.sno, trans, "OnlyOutType")
                        obj.ArrItem.Add(objtr)
                    Next
                End If

                qry = "select * from TSPL_PP_ISSUE_QC_DETAIL where issue_code='" + obj.issuecode + "' order by sno"
                dt1 = New DataTable()
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsProcessProductionIssueQCDetail()

                        objtr.sno = CInt(dr("sno"))
                        objtr.frm_loc_code = clsCommon.myCstr(dr("From_Location_Code"))
                        objtr.frm_loc_desc = clsLocation.GetName(objtr.frm_loc_code, trans)
                        objtr.to_loc_code = clsCommon.myCstr(dr("To_Location_Code"))
                        objtr.to_loc_desc = clsLocation.GetName(objtr.to_loc_code, trans)
                        objtr.itemcode = clsCommon.myCstr(dr("Item_Code"))
                        objtr.itemname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + objtr.itemcode + "'", trans))
                        objtr.param_code = clsCommon.myCstr(dr("Parameter_Code"))
                        objtr.param_desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_PARAMETER_MASTER where code='" + objtr.param_code + "'", trans))
                        objtr.param_type = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select type from TSPL_PARAMETER_MASTER where code='" + objtr.param_code + "'", trans))
                        objtr.param_nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (Case when Nature='A' then 'Alphanumeric' else case when Nature='B' then 'Boolean' else case when Nature='R' then 'Range' end end end) as Nature from TSPL_PARAMETER_MASTER where code='" + objtr.param_code + "'", trans))
                        objtr.lrange = clsCommon.myCdbl(dr("Lower_range"))
                        objtr.urange = clsCommon.myCdbl(dr("Upper_range"))
                        objtr.status_grid = clsCommon.myCstr(dr("Status"))
                        objtr.value1 = clsCommon.myCstr(dr("Value1"))
                        objtr.value2 = clsCommon.myCstr(dr("Value2"))
                        objtr.remarks = clsCommon.myCstr(dr("Remarks"))
                        objtr.QCRange = clsCommon.myCdbl(dr("QC_Range"))
                        objtr.QCStatus = clsCommon.myCstr(dr("QC_Status"))
                        objtr.QCValue = clsCommon.myCstr(dr("QC_Value"))

                        obj.ArrQC.Add(objtr)
                    Next
                End If

            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal Form_Id As String, ByVal IsCloseBO As Boolean, ByVal arrloc As String, ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso PostData(Form_Id, IsCloseBO, arrloc, strCode, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal Form_Id As String, ByVal IsCloseBO As Boolean, ByVal arrloc As String, ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Issue_Date,Main_Location_Code from TSPL_PP_ISSUE_HEAD where issue_code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProcessProductionIssueEntry, clsCommon.myCstr(dt.Rows(0)("Main_Location_Code")), clsCommon.myCDate(dt.Rows(0)("Issue_Date")), trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return PostData(Form_Id, IsCloseBO, arrloc, strCode, trans, "")
    End Function

    Public Shared Function PostData(ByVal Form_Id As String, ByVal IsCloseBO As Boolean, ByVal arrloc As String, ByVal strCode As String, ByVal trans As SqlTransaction, ByVal VoucherNo As String) As Boolean
        Return PostData(Form_Id, IsCloseBO, arrloc, strCode, trans, VoucherNo, True)
    End Function
    Public Shared Function PostData(ByVal Form_Id As String, ByVal IsCloseBO As Boolean, ByVal arrloc As String, ByVal strCode As String, ByVal trans As SqlTransaction, ByVal VoucherNo As String, ByVal isCheckFutureBalance As Boolean) As Boolean
        Try

            Dim qry As String = "update TSPL_PP_ISSUE_HEAD set Status='Approved',Is_post='1',Modified_By='" + objCommonVar.CurrentUserCode + "',Modified_Date='" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")) + "' where issue_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PP_ISSUE_HEAD", "issue_code", trans)
            '-------------batch order should be closed against this issue entry----------------
            If IsCloseBO Then
                Dim BOCOde As String = ""
                BOCOde = clsDBFuncationality.getSingleValue("select batch_code from TSPL_PP_ISSUE_HEAD where issue_code='" + strCode + "'", trans)
                While (clsCommon.myLen(BOCOde) > 0)
                    qry = "update TSPL_PP_BATCH_ORDER_HEAD set Closed_YN='1' where batch_code in ('" + BOCOde + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    BOCOde = clsProcessBatchOrder.GetMainBO(BOCOde, trans)
                End While
            End If

            '----------inventory movement entry--------------------------------------------------
            Dim isSaved As Boolean = True
            Dim obj As clsProcessProductionIssueEntry = clsProcessProductionIssueEntry.GetData(strCode, arrloc, NavigatorType.Current, trans)
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim ArrInvetoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            Dim settAllowNegativeStockInDairyProduction As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStockInDairyProduction, clsFixedParameterCode.AllowNegativeStockInDairyProduction, trans)) > 0)
            If obj.ArrItem IsNot Nothing AndAlso obj.ArrItem.Count > 0 Then
                For Each objtr As clsProcessProductionIssueItemDetail In obj.ArrItem
                    If Not settAllowNegativeStockInDairyProduction Then
                        Dim CheckStockServerDate As Boolean
                        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CheckLiveStockInProductionDuringTrans, clsFixedParameterCode.CheckLiveStockInProductionDuringTrans, trans)), "1") = CompairStringResult.Equal Then
                            CheckStockServerDate = True
                        Else
                            CheckStockServerDate = False
                        End If
                        Dim dt As DataTable = clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(objtr.itemcode, obj.Main_Loc_Code, objtr.frm_loc_code, IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(trans), obj.issue_date), trans, objtr.uom_code, objtr.From_SubLocation_YN)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            If objtr.issue_qty > clsCommon.myCdbl(dt.Rows(0)("qty")) Then
                                If Math.Abs(objtr.issue_qty - clsCommon.myCdbl(dt.Rows(0)("qty"))) > 0.01 Then
                                    Throw New Exception("Item [" + objtr.itemcode + "] Location [" + objtr.frm_loc_code + "] Issue Qty [" + clsCommon.myCstr(objtr.issue_qty) + "] is more than Balance Qty [" + clsCommon.myCstr(clsCommon.myCdbl(dt.Rows(0)("qty"))) + "]")
                                End If
                            End If
                        End If
                        If isCheckFutureBalance Then
                            Dim Product_Type As String = clsItemMaster.GetItemProductType(objtr.itemcode, trans)
                            Dim FutureBalanceQty As Decimal = 0
                            If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                                FutureBalanceQty = clsInventoryMovementNew.getBalance(objtr.itemcode, clsLocation.GetMainLocationMilk(objtr.frm_loc_code, trans), objtr.frm_loc_code, "", obj.issue_date, trans, objtr.uom_code)
                            Else
                                FutureBalanceQty = clsItemLocationDetails.getBalance(objtr.itemcode, objtr.frm_loc_code, "", obj.issue_date, trans, objtr.uom_code, 0)
                            End If
                            FutureBalanceQty = Math.Round(Math.Round(FutureBalanceQty, 3, MidpointRounding.AwayFromZero), 2, MidpointRounding.AwayFromZero)
                            If objtr.issue_qty > FutureBalanceQty Then
                                If Math.Abs(objtr.issue_qty - FutureBalanceQty) > 0.01 Then
                                    Throw New Exception("Item [" + objtr.itemcode + "] Location [" + objtr.frm_loc_code + "] Issue Qty [" + clsCommon.myCstr(objtr.issue_qty) + "] is more than Future Mininium Balance Qty [" + clsCommon.myCstr(FutureBalanceQty) + "]")
                                End If
                            End If
                        End If
                    End If

                    If clsCommon.CompairString(objtr.product_type, "MI") = CompairStringResult.Equal Then
                        '====================Stock Out Milk Item===============================
                        Dim objInventoryMovemnt As New clsInventoryMovementNew()
                        objInventoryMovemnt.InOut = "O"
                        objInventoryMovemnt.main_location = obj.Main_Loc_Code
                        objInventoryMovemnt.Location_Code = objtr.frm_loc_code
                        objInventoryMovemnt.Other_Location_Code = objtr.to_loc_code
                        objInventoryMovemnt.Other_Location_Desc = objtr.to_loc_desc
                        objInventoryMovemnt.Item_Code = objtr.itemcode
                        objInventoryMovemnt.Item_Desc = objtr.itemname
                        objInventoryMovemnt.Qty = objtr.issue_qty
                        objInventoryMovemnt.UOM = objtr.uom_code
                        objInventoryMovemnt.MRP = Nothing
                        objInventoryMovemnt.Add_Cost = Nothing
                        objInventoryMovemnt.Net_Cost = Nothing
                        If clsCommon.CompairString(objtr.item_type, "R") = CompairStringResult.Equal Then
                            objInventoryMovemnt.ItemType = "RM"
                        ElseIf clsCommon.CompairString(objtr.item_type, "F") = CompairStringResult.Equal Then
                            objInventoryMovemnt.ItemType = "FT"
                        Else
                            objInventoryMovemnt.ItemType = objtr.item_type
                        End If
                        objInventoryMovemnt.Basic_Cost = Nothing
                        objInventoryMovemnt.Batch_No = obj.batch_code
                        objInventoryMovemnt.MFG_Date = Nothing
                        objInventoryMovemnt.Expiry_Date = Nothing
                        objInventoryMovemnt.FAT_KG = objtr.fat_kg
                        objInventoryMovemnt.FAT_Per = objtr.fat_pers
                        objInventoryMovemnt.SNF_KG = objtr.snf_kg
                        objInventoryMovemnt.SNF_Per = objtr.snf_pers
                        objInventoryMovemnt.Fat_Rate = objtr.Fat_Rate
                        objInventoryMovemnt.Fat_Amt = objtr.Fat_Amt
                        objInventoryMovemnt.SNF_Rate = objtr.SNF_Rate
                        objInventoryMovemnt.SNF_Amt = objtr.SNF_Amt
                        Dim cost As Decimal = objtr.Fat_Amt + objtr.SNF_Amt
                        objInventoryMovemnt.FIFO_Cost = cost
                        objInventoryMovemnt.Avg_Cost = cost
                        objInventoryMovemnt.LIFO_Cost = cost
                        objInventoryMovemnt.CalculateAvgCost = False
                        objInventoryMovemnt.Ref_Line_No = objtr.sno
                        ArrInvetoryMovementNew.Add(objInventoryMovemnt)

                        '====================Stock In Milk Item===============================
                        objInventoryMovemnt = New clsInventoryMovementNew()
                        objInventoryMovemnt.InOut = "I"
                        objInventoryMovemnt.main_location = obj.Main_Loc_Code
                        objInventoryMovemnt.Location_Code = objtr.to_loc_code

                        objInventoryMovemnt.Other_Location_Code = objtr.frm_loc_code
                        objInventoryMovemnt.Other_Location_Desc = objtr.frm_loc_desc

                        objInventoryMovemnt.Item_Code = objtr.itemcode
                        objInventoryMovemnt.Item_Desc = objtr.itemname
                        objInventoryMovemnt.Qty = objtr.issue_qty
                        objInventoryMovemnt.UOM = objtr.uom_code
                        objInventoryMovemnt.MRP = Nothing
                        objInventoryMovemnt.Add_Cost = Nothing
                        objInventoryMovemnt.Net_Cost = Nothing
                        If clsCommon.CompairString(objtr.item_type, "R") = CompairStringResult.Equal Then
                            objInventoryMovemnt.ItemType = "RM"
                        ElseIf clsCommon.CompairString(objtr.item_type, "F") = CompairStringResult.Equal Then
                            objInventoryMovemnt.ItemType = "FT"
                        Else
                            objInventoryMovemnt.ItemType = objtr.item_type
                        End If
                        objInventoryMovemnt.Basic_Cost = Nothing
                        objInventoryMovemnt.Batch_No = obj.batch_code
                        objInventoryMovemnt.MFG_Date = Nothing
                        objInventoryMovemnt.Expiry_Date = Nothing
                        objInventoryMovemnt.FAT_KG = objtr.fat_kg
                        objInventoryMovemnt.FAT_Per = objtr.fat_pers
                        objInventoryMovemnt.SNF_KG = objtr.snf_kg
                        objInventoryMovemnt.SNF_Per = objtr.snf_pers
                        objInventoryMovemnt.Fat_Rate = objtr.Fat_Rate
                        objInventoryMovemnt.Fat_Amt = objtr.Fat_Amt
                        objInventoryMovemnt.SNF_Rate = objtr.SNF_Rate
                        objInventoryMovemnt.SNF_Amt = objtr.SNF_Amt
                        objInventoryMovemnt.FIFO_Cost = cost
                        objInventoryMovemnt.LIFO_Cost = cost
                        objInventoryMovemnt.Avg_Cost = cost
                        objInventoryMovemnt.Basic_Cost = If(objtr.issue_qty <= 0, 0, cost / objtr.issue_qty)
                        objInventoryMovemnt.Net_Cost = cost
                        objInventoryMovemnt.CalculateAvgCost = False
                        objInventoryMovemnt.Ref_Line_No = objtr.sno
                        ArrInvetoryMovementNew.Add(objInventoryMovemnt)
                    Else
                        '====================Stock Out Non Milk Item===============================
                        Dim objInventoryMovemnt As New clsInventoryMovement()
                        objInventoryMovemnt.InOut = "O"
                        objInventoryMovemnt.Location_Code = objtr.frm_loc_code
                        objInventoryMovemnt.Other_Location_Code = objtr.to_loc_code
                        objInventoryMovemnt.Other_Location_Desc = objtr.to_loc_desc
                        objInventoryMovemnt.Item_Code = objtr.itemcode
                        objInventoryMovemnt.Item_Desc = objtr.itemname
                        objInventoryMovemnt.Qty = objtr.issue_qty
                        objInventoryMovemnt.UOM = objtr.uom_code
                        objInventoryMovemnt.MRP = Nothing
                        objInventoryMovemnt.Add_Cost = Nothing
                        objInventoryMovemnt.Net_Cost = Nothing
                        If clsCommon.CompairString(objtr.item_type, "R") = CompairStringResult.Equal Then
                            objInventoryMovemnt.ItemType = "RM"
                        ElseIf clsCommon.CompairString(objtr.item_type, "F") = CompairStringResult.Equal Then
                            objInventoryMovemnt.ItemType = "FT"
                        Else
                            objInventoryMovemnt.ItemType = objtr.item_type
                        End If
                        objInventoryMovemnt.Batch_No = obj.batch_code
                        objInventoryMovemnt.MFG_Date = Nothing
                        objInventoryMovemnt.Expiry_Date = Nothing
                        objInventoryMovemnt.FAT_KG = objtr.fat_kg
                        objInventoryMovemnt.FAT_Per = objtr.fat_pers
                        objInventoryMovemnt.SNF_KG = objtr.snf_kg
                        objInventoryMovemnt.SNF_Per = objtr.snf_pers
                        objInventoryMovemnt.Fat_Rate = objtr.Fat_Rate
                        objInventoryMovemnt.Fat_Amt = objtr.Fat_Amt
                        objInventoryMovemnt.SNF_Rate = objtr.SNF_Rate
                        objInventoryMovemnt.SNF_Amt = objtr.SNF_Amt
                        Dim cost As Decimal = objtr.Fat_Amt + objtr.SNF_Amt
                        objInventoryMovemnt.FIFO_Cost = cost
                        objInventoryMovemnt.Avg_Cost = cost
                        objInventoryMovemnt.LIFO_Cost = cost
                        objInventoryMovemnt.Basic_Cost = If(objtr.issue_qty <= 0, 0, cost / objtr.issue_qty)
                        objInventoryMovemnt.CalculateAvgCost = False
                        objInventoryMovemnt.Ref_Line_No = objtr.sno
                        ArrInventoryMovement.Add(objInventoryMovemnt)

                        '====================Stock In Non Milk Item===============================
                        objInventoryMovemnt = New clsInventoryMovement()
                        objInventoryMovemnt.InOut = "I"
                        objInventoryMovemnt.Location_Code = objtr.to_loc_code
                        objInventoryMovemnt.Other_Location_Code = objtr.frm_loc_code
                        objInventoryMovemnt.Other_Location_Desc = objtr.frm_loc_desc
                        objInventoryMovemnt.Item_Code = objtr.itemcode
                        objInventoryMovemnt.Item_Desc = objtr.itemname
                        objInventoryMovemnt.Qty = objtr.issue_qty
                        objInventoryMovemnt.UOM = objtr.uom_code
                        objInventoryMovemnt.MRP = Nothing
                        objInventoryMovemnt.Add_Cost = Nothing
                        objInventoryMovemnt.Net_Cost = Nothing
                        If clsCommon.CompairString(objtr.item_type, "R") = CompairStringResult.Equal Then
                            objInventoryMovemnt.ItemType = "RM"
                        ElseIf clsCommon.CompairString(objtr.item_type, "F") = CompairStringResult.Equal Then
                            objInventoryMovemnt.ItemType = "FT"
                        Else
                            objInventoryMovemnt.ItemType = objtr.item_type
                        End If
                        objInventoryMovemnt.Basic_Cost = Nothing
                        objInventoryMovemnt.Batch_No = obj.batch_code
                        objInventoryMovemnt.MFG_Date = Nothing
                        objInventoryMovemnt.Expiry_Date = Nothing
                        objInventoryMovemnt.FAT_KG = objtr.fat_kg
                        objInventoryMovemnt.FAT_Per = objtr.fat_pers
                        objInventoryMovemnt.SNF_KG = objtr.snf_kg
                        objInventoryMovemnt.SNF_Per = objtr.snf_pers
                        objInventoryMovemnt.Fat_Rate = objtr.Fat_Rate
                        objInventoryMovemnt.Fat_Amt = objtr.Fat_Amt
                        objInventoryMovemnt.SNF_Rate = objtr.SNF_Rate
                        objInventoryMovemnt.SNF_Amt = objtr.SNF_Amt
                        objInventoryMovemnt.FIFO_Cost = cost
                        objInventoryMovemnt.LIFO_Cost = cost
                        objInventoryMovemnt.Avg_Cost = cost
                        objInventoryMovemnt.Basic_Cost = If(objtr.issue_qty <= 0, 0, cost / objtr.issue_qty)
                        objInventoryMovemnt.Net_Cost = cost
                        objInventoryMovemnt.CalculateAvgCost = False
                        objInventoryMovemnt.Ref_Line_No = objtr.sno
                        ArrInventoryMovement.Add(objInventoryMovemnt)
                    End If
                Next
                If ArrInventoryMovement.Count > 0 Then
                    isSaved = isSaved AndAlso clsInventoryMovement.SaveData(Form_Id, obj.issuecode, obj.issue_date, clsCommon.GetPrintDate(obj.issue_date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                End If
                If ArrInvetoryMovementNew.Count > 0 Then
                    isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData(Form_Id, obj.issuecode, obj.issue_date, clsCommon.GetPrintDate(obj.issue_date, "dd/MM/yyyy"), ArrInvetoryMovementNew, trans)
                End If
            End If
            If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CreateJEOnProduction, clsFixedParameterCode.CreateJEOnProduction, trans)) = "1" Then
                isSaved = isSaved And JournalEntry(trans, obj, VoucherNo)
            End If
            If clsCommon.myLen(VoucherNo) <= 0 Then
                '== Notification regarding work agaist ticket no. BHA/14/08/18-000426 for all Production Screen
                Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProcessProductionIssueEntry + "'", trans))
                If clsCommon.CompairString(strNotificationOn, "P") = CompairStringResult.Equal Then
                    CreateNotificationContentEMP(strCode, trans)
                End If
            End If
        Catch ex As Exception
            Throw New Exception("Production Issue Entry [" + strCode + "]" + Environment.NewLine + ex.Message)
        End Try
        Return True
    End Function

    Private Shared Function CreateNotificationContentEMP(ByVal StrDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim strNotifiContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProcessProductionIssueEntry + "'", trans))
        Dim strNotifi_DetalContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Detail_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProcessProductionIssueEntry + "'", trans))
        Dim strNotifiCaption As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Caption from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProcessProductionIssueEntry + "'", trans))
        Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProcessProductionIssueEntry + "'", trans))
        Dim strDocumentDate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Plan_Date from TSPL_PP_PRODUCTION_PLAN_HEAD where plan_code='" + StrDocNo + "'", trans))

        If clsCommon.myLen(strNotifiContent) > 0 Then
            Dim objNotification As New clsNotificationHead()
            objNotification.Notification_Text = strNotifiContent
            objNotification.Notification_Caption = strNotifiCaption
            objNotification.Notification_On = strNotificationOn
            objNotification.Notification_Detail_Text = strNotifi_DetalContent
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(StrDocNo))
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, (clsCommon.myCDate(strDocumentDate)))
            objNotification.SaveData(clsUserMgtCode.frmProcessProductionIssueEntry, objNotification, trans)
            objNotification = Nothing
            Return True
        End If
        Return False
    End Function

    Public Shared Function JournalEntry(ByVal trans As SqlTransaction, ByVal obj1 As clsProcessProductionIssueEntry, Optional ByVal strVourcherNoForRecreateOnly As String = "") As Boolean
        Try
            Dim VoucherNo As String = ""
            If clsCommon.myLen(strVourcherNoForRecreateOnly) > 0 Then
                VoucherNo = strVourcherNoForRecreateOnly
            Else
                VoucherNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PR-IS' and Source_Doc_No='" & obj1.issuecode & "'", trans))
            End If

            If obj1.Is_Job_Work_Inward Then
                If clsCommon.myLen(VoucherNo) > 0 Then
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_JOURNAL_DETAILS where Voucher_No='" + VoucherNo + "' ", trans)
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_JOURNAL_MASTER where Voucher_No='" + VoucherNo + "' ", trans)
                End If
                Return True  ''Journal Entry will not create is job work type.
            End If

            Dim JRNL_DATE As Date = obj1.issue_date
            Dim Count As Integer = 0
            Dim qry As String
            Dim dtGL As DataTable
            Dim TotalDebitAmt As Decimal = 0
            Dim TotalCreditAmt As Decimal = 0
            Dim isSaved As Boolean = True
            Dim VoucherDesc As String = "Financial Entry for Issue- " & obj1.issuecode & " "
            Dim SourceDocDesc As String = obj1.issuedesc
            Dim SourceDocNo As String = obj1.issuecode
            Dim Comments As String = obj1.issuedesc
            Dim Remarks As String = obj1.issuedesc

            Dim i As Integer = 0
            Dim settActivateSFGProduction As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ActivateSFGProduction, clsFixedParameterCode.ActivateSFGProduction, trans)) = 1)
            qry = " select Issue.*,CrGL.Description as CreditAccountDesc,DrGL.Description as DebitAccountDesc  from ( " & _
                  " select TSPL_PP_ISSUE_HEAD.ISSUE_CODE,MAX(TSPL_PP_ISSUE_HEAD.Batch_Code) as BO_CODE,TSPL_PP_ISSUE_ITEM_DETAIL.ITEM_CODE AS PROD_ITEM_CODE," & _
                  " (CASE WHEN SUBSTRING(RIGHT(TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,4),1,1)='-' THEN " & _
                  " REPLACE(TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,RIGHT(TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,4),'-' " & _
                  " +LEFT(COALESCE(TSPL_LOCATION_MASTER.Loc_Segment_Code,''),3)) " & _
                  " ELSE TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account+ '-' + LEFT(COALESCE(TSPL_LOCATION_MASTER.Loc_Segment_Code,''),3) END) as CreditAccount, " & _
                  " (CASE WHEN SUBSTRING(RIGHT(TSPL_PURCHASE_ACCOUNTS.WIP_Account,4),1,1)='-' THEN REPLACE(TSPL_PURCHASE_ACCOUNTS.WIP_Account, " & _
                  " RIGHT(TSPL_PURCHASE_ACCOUNTS.WIP_Account,4),'-' +LEFT(COALESCE(TSPL_LOCATION_MASTER.Loc_Segment_Code,''),3)) " & _
                  " ELSE TSPL_PURCHASE_ACCOUNTS.WIP_Account+ '-'+LEFT(COALESCE(TSPL_LOCATION_MASTER.Loc_Segment_Code,''),3) END) as DebitAccount, " & _
                  " sum(TSPL_PP_ISSUE_ITEM_DETAIL.Fat_Amt+TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Amt) as ItemCost from TSPL_PP_ISSUE_ITEM_DETAIL " & _
                  " inner join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_ITEM_DETAIL.ISSUE_CODE=TSPL_PP_ISSUE_HEAD.ISSUE_CODE " & _
                  " inner join TSPL_ITEM_MASTER on TSPL_PP_ISSUE_ITEM_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " & _
                  " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " & _
                  " left join TSPL_LOCATION_MASTER on TSPL_PP_ISSUE_HEAD.From_Loaction_Code=TSPL_LOCATION_MASTER.LOCATION_CODE " & _
                  " WHERE TSPL_PP_ISSUE_ITEM_DETAIL.ISSUE_CODE='" & obj1.issuecode & "'"
            If Not settActivateSFGProduction Then ''By balwinder on 06/08/2018 with ranjana mam.
                qry += " and TSPL_ITEM_MASTER.Item_Type not in ('S') "
            End If
            qry += "  group by TSPL_PP_ISSUE_HEAD.ISSUE_CODE, " & _
                  " TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.WIP_Account,TSPL_PP_ISSUE_HEAD.From_Loaction_Code, " & _
                  " COALESCE(TSPL_LOCATION_MASTER.Loc_Segment_Code,''), " & _
                  " TSPL_PP_ISSUE_ITEM_DETAIL.ITEM_CODE) as Issue left join TSPL_GL_ACCOUNTS as CrGL on Issue.CreditAccount=CrGL.Account_Code " & _
                  " left join TSPL_GL_ACCOUNTS as DrGL on Issue.DebitAccount=DrGL.Account_Code"
            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            For Each grow As DataRow In dtGL.Rows
                '' check for account setting  exist or not
                If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
                    Throw New Exception("Inventory Control Account not found for Raw Materials of Item " & grow.Item("PROD_ITEM_CODE") & "")
                    Return False
                End If
                If clsCommon.myLen(grow.Item("DebitAccount")) <= 0 Then
                    Throw New Exception("WIP Account not found for Item " & grow.Item("PROD_ITEM_CODE") & "")
                    Return False
                End If
                If clsCommon.myLen(grow.Item("CreditAccountDesc")) <= 0 Then
                    Throw New Exception("Inventory Control Account " & grow.Item("CreditAccount") & " does not exist for Raw Materials of Item " & grow.Item("PROD_ITEM_CODE") & "")
                    Return False
                End If
                If clsCommon.myLen(grow.Item("DebitAccountDesc")) <= 0 Then
                    Throw New Exception("WIP Account " & grow.Item("DebitAccount") & " does not exist for Item " & grow.Item("PROD_ITEM_CODE") & "")
                    Return False
                End If
                Count += 1
                TotalCreditAmt = TotalCreditAmt + clsCommon.myCdbl(grow.Item("ItemCost"))
                TotalDebitAmt = TotalDebitAmt + clsCommon.myCdbl(grow.Item("ItemCost"))
            Next
            If Count <= 0 Then
                Throw New Exception("No Data found")
                Return False
            End If
            Dim ArryLstGLAC As ArrayList = New ArrayList()

            qry = "SELECT ISSUE_CODE,PROD_ITEM_CODE,BO_CODE,DebitAccount,'' AS CreditAccount,DebitAccountDesc,'' AS CreditAccountDesc,SUM(ITEMCOST) AS ITEMCOST FROM (" & _
            " " & qry & " ) AS FINAL GROUP BY ISSUE_CODE,PROD_ITEM_CODE,BO_CODE,DebitAccount,DebitAccountDesc" & _
            " Union all" & _
            " SELECT ISSUE_CODE,PROD_ITEM_CODE,BO_CODE,'' AS DebitAccount,CreditAccount,'' AS DebitAccountDesc,CreditAccountDesc,SUM(ITEMCOST) AS ITEMCOST FROM (" & _
            " " & qry & " ) AS FINAL GROUP BY ISSUE_CODE,PROD_ITEM_CODE,BO_CODE,CreditAccount,CreditAccountDesc"

            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            If (dtGL IsNot Nothing AndAlso dtGL.Rows.Count > 0) Then
                For Each dr As DataRow In dtGL.Rows
                    '' debit
                    Dim DebitAcc As String = clsCommon.myCstr(dr.Item("DebitAccount"))
                    If clsCommon.myLen(DebitAcc) > 0 Then
                        Dim Acc1() As String = {DebitAcc, 1 * clsCommon.myCdbl(dr("ITEMCOST"))}
                        ArryLstGLAC.Add(Acc1)
                        ''''''''
                        'clsInventoryMovement.UpdateInvControlAccount(clsCommon.myCstr(dr("ISSUE_CODE")), clsUserMgtCode.frmProcessProductionIssueEntry, clsCommon.myCstr(dr("PROD_ITEM_CODE")), DebitAcc, "", "I", trans)
                        ''------------------
                    End If
                    '' credit
                    Dim CreditAcc As String = clsCommon.myCstr(dr.Item("CreditAccount"))
                    If clsCommon.myLen(CreditAcc) > 0 Then
                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                            Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(dr("ITEMCOST"))}
                            ArryLstGLAC.Add(Acc2)
                        Else
                            Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(dr("ITEMCOST")), "", "", "", "", "", "", "I"}
                            ArryLstGLAC.Add(Acc2)
                            ''BHA/27/11/18-000726 by Balwinder on 18/01/2019
                            clsInventoryMovement.UpdateInvControlAccount(clsCommon.myCstr(dr("ISSUE_CODE")), clsUserMgtCode.frmProcessProductionIssueEntry, clsCommon.myCstr(dr("PROD_ITEM_CODE")), "", CreditAcc, "O", trans)
                            ''------------------
                        End If
                    End If
                Next
                Dim GLDesc As String = "Journal Entry Against Production Issue- Doc No." & obj1.issuecode & " "
                If clsCommon.myLen(VoucherNo) > 0 Then
                    isSaved = isSaved AndAlso clsJournalMaster.FunGrnlEntryWithTrans(obj1.frm_loc_code, False, VoucherNo, trans, obj1.issue_date, GLDesc, "PR-IS", "Production Issue", obj1.issuecode, obj1.issuedesc, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, GLDesc, "")
                Else
                    isSaved = isSaved AndAlso clsJournalMaster.FunGrnlEntryWithTrans(obj1.frm_loc_code, False, trans, obj1.issue_date, GLDesc, "PR-IS", "Production Issue", obj1.issuecode, obj1.issuedesc, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , GLDesc, "")
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsProcessProductionIssueItemDetail
#Region "Variables"
    Public sno As Integer = Nothing
    Public frm_loc_code As String = Nothing
    Public frm_loc_desc As String = Nothing
    Public to_loc_code As String = Nothing
    Public to_loc_desc As String = Nothing
    Public itemcode As String = Nothing
    Public itemname As String = Nothing
    Public item_type As String = Nothing
    Public product_type As String = Nothing
    Public uom_code As String = Nothing
    Public uom_desc As String = Nothing
    Public avail_qty As Decimal = Nothing
    Public avail_fat_pers As Decimal = Nothing
    Public avail_fat_kg As Decimal = Nothing
    Public avail_snf_pers As Decimal = Nothing
    Public avail_snf_kg As Decimal = Nothing
    Public req_qty As Decimal = Nothing
    Public issue_qty As Decimal = Nothing
    Public fat_pers As Decimal = Nothing
    Public fat_kg As Decimal = Nothing
    Public snf_pers As Decimal = Nothing
    Public snf_kg As Decimal = Nothing
    Public remarks As String = Nothing
    Public From_SubLocation_YN As Integer = Nothing
    Public To_SubLocation_YN As Integer = Nothing
    '' production costing columns
    Public Fat_Rate As Decimal = 0
    Public SNF_Rate As Decimal = 0
    Public Fat_Amt As Decimal = 0
    Public SNF_Amt As Decimal = 0

    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
    Public arrBatchItemNew As List(Of clsBatchInventoryNew) = Nothing
#End Region

    Public Shared Function SaveData(ByVal objIssue As clsProcessProductionIssueEntry, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = "delete from TSPL_PP_ISSUE_ITEM_DETAIL where issue_code='" + objIssue.issuecode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsBatchInventory.DeleteData(clsUserMgtCode.frmProcessProductionIssueEntry, objIssue.issuecode, trans)
            clsBatchInventoryNew.DeleteData(clsUserMgtCode.frmProcessProductionIssueEntry, objIssue.issuecode, trans)

            Dim coll As New Hashtable()

            If objIssue.ArrItem IsNot Nothing AndAlso objIssue.ArrItem.Count > 0 Then
                For Each objtr As clsProcessProductionIssueItemDetail In objIssue.ArrItem
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Issue_Code", objIssue.issuecode)
                    clsCommon.AddColumnsForChange(coll, "From_SubLocation_YN", objtr.From_SubLocation_YN)
                    clsCommon.AddColumnsForChange(coll, "To_SubLocation_YN", objtr.To_SubLocation_YN)
                    clsCommon.AddColumnsForChange(coll, "From_Loaction_Code", objtr.frm_loc_code, True)
                    clsCommon.AddColumnsForChange(coll, "To_Location_Code", objtr.to_loc_code, True)
                    clsCommon.AddColumnsForChange(coll, "SNO", objtr.sno)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.itemcode)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.uom_code)
                    clsCommon.AddColumnsForChange(coll, "Avail_Qty", objtr.avail_qty)
                    clsCommon.AddColumnsForChange(coll, "Avail_FAT_Pers", objtr.avail_fat_pers)
                    clsCommon.AddColumnsForChange(coll, "Avail_FAT_KG", objtr.avail_fat_kg)
                    clsCommon.AddColumnsForChange(coll, "Avail_SNF_Pers", objtr.avail_snf_pers)
                    clsCommon.AddColumnsForChange(coll, "Avail_SNF_KG", objtr.avail_snf_kg)
                    clsCommon.AddColumnsForChange(coll, "Required_Qty", objtr.req_qty)
                    clsCommon.AddColumnsForChange(coll, "Qty", objtr.issue_qty)
                    clsCommon.AddColumnsForChange(coll, "FAT_Pers", objtr.fat_pers)
                    clsCommon.AddColumnsForChange(coll, "SNF_Pers", objtr.snf_pers)
                    clsCommon.AddColumnsForChange(coll, "FAT_KG", objtr.fat_kg)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", objtr.snf_kg)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.remarks)


                    '' production costing columns
                    If objIssue.Is_Job_Work_Inward Then
                        objtr.Fat_Rate = 0
                        objtr.SNF_Rate = 0
                        objtr.Fat_Amt = 0
                        objtr.SNF_Amt = 0
                    Else
                        Dim objCost As New MIlkComponentType
                        objCost = clsInventoryMovementNew.GetAvgCost(True, False, False, False, "", objtr.product_type, objtr.itemcode, If(clsCommon.myLen(objtr.frm_loc_code) <= 0, objtr.to_loc_code, objtr.frm_loc_code), objtr.issue_qty, objtr.uom_code, objtr.fat_kg, objtr.snf_kg, objIssue.issue_date, objIssue.issue_date, False, trans)
                        objtr.Fat_Rate = If(objtr.fat_kg <= 0, 0, objCost.FAT_Cost / objtr.fat_kg)
                        objtr.SNF_Rate = If(objtr.snf_kg <= 0, 0, objCost.SNF_Cost / objtr.snf_kg)
                        objtr.Fat_Amt = objCost.FAT_Cost
                        objtr.SNF_Amt = objCost.SNF_Cost
                    End If

                    clsCommon.AddColumnsForChange(coll, "Fat_Rate", objtr.Fat_Rate)
                    clsCommon.AddColumnsForChange(coll, "SNF_Rate", objtr.SNF_Rate)
                    clsCommon.AddColumnsForChange(coll, "Fat_Amt", objtr.Fat_Amt)
                    clsCommon.AddColumnsForChange(coll, "SNF_Amt", objtr.SNF_Amt)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_ISSUE_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                    clsBatchInventory.SaveData(clsUserMgtCode.frmProcessProductionIssueEntry, objIssue.issuecode, objIssue.issue_date, "O", objtr.itemcode, objtr.frm_loc_code, objtr.sno, 0, objtr.uom_code, objtr.arrBatchItem, trans)
                    clsBatchInventory.SaveData(clsUserMgtCode.frmProcessProductionIssueEntry, objIssue.issuecode, objIssue.issue_date, "I", objtr.itemcode, objtr.to_loc_code, objtr.sno, 0, objtr.uom_code, objtr.arrBatchItem, trans)

                    clsBatchInventoryNew.SaveData(clsUserMgtCode.frmProcessProductionIssueEntry, objIssue.issuecode, objIssue.issue_date, "O", objtr.itemcode, objtr.frm_loc_code, objtr.sno, 0, objtr.uom_code, objtr.arrBatchItemNew, trans)
                    clsBatchInventoryNew.SaveData(clsUserMgtCode.frmProcessProductionIssueEntry, objIssue.issuecode, objIssue.issue_date, "I", objtr.itemcode, objtr.to_loc_code, objtr.sno, 0, objtr.uom_code, objtr.arrBatchItemNew, trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
Public Class clsProcessProductionIssueQCDetail
#Region "Variables"
    Public sno As Integer = Nothing
    Public frm_loc_code As String = Nothing
    Public frm_loc_desc As String = Nothing
    Public to_loc_code As String = Nothing
    Public to_loc_desc As String = Nothing
    Public itemcode As String = Nothing
    Public itemname As String = Nothing
    Public param_code As String = Nothing
    Public param_desc As String = Nothing
    Public param_type As String = Nothing
    Public param_nature As String = Nothing
    Public lrange As Decimal = Nothing
    Public urange As Decimal = Nothing
    Public status_grid As String = Nothing
    Public value1 As String = Nothing
    Public value2 As String = Nothing
    Public remarks As String = Nothing
    Public QCRange As Decimal = Nothing
    Public QCValue As String = Nothing
    Public QCStatus As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal issue_code As String, ByVal arr As List(Of clsProcessProductionIssueQCDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = "delete from TSPL_PP_ISSUE_QC_DETAIL where issue_code='" + issue_code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsProcessProductionIssueQCDetail In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Issue_Code", issue_code)
                    clsCommon.AddColumnsForChange(coll, "SNO", objtr.sno)
                    clsCommon.AddColumnsForChange(coll, "From_Location_Code", objtr.frm_loc_code, True)
                    clsCommon.AddColumnsForChange(coll, "To_Location_Code", objtr.to_loc_code, True)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.itemcode)
                    clsCommon.AddColumnsForChange(coll, "Parameter_Code", objtr.param_code)
                    clsCommon.AddColumnsForChange(coll, "Lower_range", objtr.lrange)
                    clsCommon.AddColumnsForChange(coll, "Upper_range", objtr.urange)
                    clsCommon.AddColumnsForChange(coll, "Status", objtr.status_grid)
                    clsCommon.AddColumnsForChange(coll, "Value1", objtr.value1)
                    clsCommon.AddColumnsForChange(coll, "Value2", objtr.value2)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.remarks)
                    clsCommon.AddColumnsForChange(coll, "QC_Range", objtr.QCRange)
                    clsCommon.AddColumnsForChange(coll, "QC_Value", objtr.QCValue)
                    clsCommon.AddColumnsForChange(coll, "QC_Status", objtr.QCStatus)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_ISSUE_QC_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
