Imports common
Imports System.Data.SqlClient
Public Class clsMilkCleaning
    Public Doc_No As String = String.Empty
    Public Start_Date_Time As Date = Nothing
    Public End_Date_Time As Date = Nothing
    Public Gate_Entry_No As String = String.Empty
    Public Weighment_No As String = String.Empty
    Public QC_No As String = String.Empty
    Public Tanker_No As String = String.Empty
    Public isPosted As Integer = 0
    Public Posting_Date As Date = Nothing
    Public Done_by As String = String.Empty
    Public Checked_by As String = String.Empty
    Public Status As String = String.Empty
    Public Created_By As String = String.Empty
    Public Created_Date As String = String.Empty
    Public Modify_By As String = String.Empty
    Public Modify_Date As String = String.Empty
    Public comp_code As String = String.Empty
    Public isNewEntry As Boolean = False
    Public Remarks As String = String.Empty
    Public Shared Function postData(ByVal StrDocNo As String, ByVal formId As String) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(StrDocNo) <= 0) Then
                Throw New Exception("Cleaning Doc No not found to Post")
            End If

            Dim obj As clsMilkCleaning = clsMilkCleaning.getData(StrDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            trans = clsDBFuncationality.GetTransactin()
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Cleaning", "Cleaning", "", obj.Start_Date_Time, trans)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            '--------------------
            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(formId, "TSPL_JOB_MILK_Cleaning", "Doc_no", obj.Doc_No, trans)
            If isResult = False Then
                trans.Commit()
                Return False
            End If
            Dim strQry As String = " update TSPL_JOB_MILK_Cleaning set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where doc_no='" & StrDocNo & "'"
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)

            If isPosted Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
            Return isPosted
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function deleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_JOB_MILK_Cleaning where doc_No='" & strDocNo & "'"
            Dim isDeleted As Boolean = True
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select TSPL_JOB_MILK_Cleaning.Doc_No as [DocumentNo] ,TSPL_JOB_MILK_Cleaning.Start_Date_Time as [Start Date Time] ,TSPL_JOB_MILK_Cleaning.End_Date_Time as [End Date Time] ,TSPL_JOB_MILK_Cleaning.Gate_Entry_No as [Gate Entry No] ,TSPL_JOB_MILK_Cleaning.Weighment_No as [Weighment No] ,TSPL_JOB_MILK_Cleaning.QC_No as [Qc No] ,TSPL_JOB_MILK_Cleaning.Tanker_No as [Tanker No] , case when isnull (TSPL_JOB_MILK_Cleaning.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,TSPL_JOB_MILK_Cleaning.Posting_Date as [Posting Date] ,TSPL_JOB_MILK_Cleaning.Done_by as [Cleaning Done By] ,TSPL_JOB_MILK_Cleaning.Checked_by as [Checked By] ,TSPL_JOB_MILK_Cleaning.Status as [Status] ,TSPL_JOB_MILK_Cleaning.Created_By as [Created By] ,TSPL_JOB_MILK_Cleaning.Created_Date as [Created Date] ,TSPL_JOB_MILK_Cleaning.Modify_By as [Modify By] ,TSPL_JOB_MILK_Cleaning.Modify_Date as [Modify Date] ,TSPL_JOB_MILK_Cleaning.comp_code as [Company Code] From TSPL_JOB_MILK_Cleaning left outer join Tspl_Milk_Gate_Entry_Details on Tspl_Milk_Gate_Entry_Details.Gate_Entry_No=TSPL_JOB_MILK_Cleaning.Gate_Entry_No  "
            str = clsCommon.ShowSelectForm("CLNFND", qry, "DocumentNo", whrcls, curcode, "DocumentNo", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function getGateEntryFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            'Dim qry As String = " select * from (select Tspl_Milk_Gate_Entry_Details.Gate_Entry_No as [GateEntryNo] ,Tspl_Milk_Gate_Entry_Details.Doc_Type as [Doc Type] ,Tspl_Milk_Gate_Entry_Details.Date_And_Time as [ Gate Entry Date And Time],  TSPL_MILK_Weighment_Detail.Weighment_No as [Weighment No],TSPL_MILK_QUALITY_CHECK.QC_No as [QC No]  ,Tspl_Milk_Gate_Entry_Details.Challan_No as [Challan No] ,Tspl_Milk_Gate_Entry_Details.Challan_Date as [Challan Date] ,Tspl_Milk_Gate_Entry_Details.Tanker_No as [Tanker No] , case when isnull (Tspl_Milk_Gate_Entry_Details.isPosted,0)=0 then 'NO' else 'Yes' end as [Is Posted] ,Tspl_Milk_Gate_Entry_Details.Posting_Date as [Posting Date] ,Tspl_Milk_Gate_Entry_Details.Dispatched_From_Mcc as [Dispatched From Mcc] ,Tspl_Milk_Gate_Entry_Details.location_Code as [Location Code] ,Tspl_Milk_Gate_Entry_Details.Location_Desc as [Location Desc] ,Tspl_Milk_Gate_Entry_Details.Vendor_Code as [Vendor Code] ,Tspl_Milk_Gate_Entry_Details.Vendor_Desc as [Vendor Desc] ,Tspl_Milk_Gate_Entry_Details.Item_Code as [Item Code] ,Tspl_Milk_Gate_Entry_Details.Item_Desc as [Item Desc] ,Tspl_Milk_Gate_Entry_Details.Qty_In_Kg as [Qty] ,Tspl_Milk_Gate_Entry_Details.snf_Per as [SNF %] ,Tspl_Milk_Gate_Entry_Details.fat_per as [FAT %] ,Tspl_Milk_Gate_Entry_Details.Created_By as [Created By] ,Tspl_Milk_Gate_Entry_Details.Created_Date as [Created Date] ,Tspl_Milk_Gate_Entry_Details.Modify_By as [Modify By] ,Tspl_Milk_Gate_Entry_Details.Modify_Date as [Modify Date] ,Tspl_Milk_Gate_Entry_Details.comp_code as [Company Code]  From Tspl_Milk_Gate_Entry_Details left outer join TSPL_MILK_Weighment_Detail on TSPL_MILK_Weighment_Detail.Gate_Entry_No=Tspl_Milk_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_MILK_QUALITY_CHECK on TSPL_MILK_QUALITY_CHECK.gate_entry_no=Tspl_Milk_Gate_Entry_Details.Gate_Entry_No left outer join TSPL_JOB_MILK_UNLOADING on TSPL_JOB_MILK_UNLOADING.Gate_Entry_No=Tspl_Milk_Gate_Entry_Details.Gate_Entry_No left outer join TSPL_JOB_MILK_Cleaning on TSPL_JOB_MILK_Cleaning.Gate_Entry_No=Tspl_Milk_Gate_Entry_Details.Gate_Entry_No  where TSPL_JOB_MILK_UNLOADING.isPosted='1' and Tspl_Milk_Gate_Entry_Details.Doc_Type='BulkProc' and TSPL_MILK_QUALITY_CHECK.is_Param_Accepted<>0  union all select Tspl_Milk_Gate_Entry_Details.Gate_Entry_No as [GateEntryNo] ,Tspl_Milk_Gate_Entry_Details.Doc_Type as [Doc Type] ,Tspl_Milk_Gate_Entry_Details.Date_And_Time as [ Gate Entry Date And Time],  TSPL_MILK_Weighment_Detail.Weighment_No as [Weighment No],TSPL_MILK_QUALITY_CHECK.QC_No as [QC No]  ,Tspl_Milk_Gate_Entry_Details.Challan_No as [Challan No] ,Tspl_Milk_Gate_Entry_Details.Challan_Date as [Challan Date] ,Tspl_Milk_Gate_Entry_Details.Tanker_No as [Tanker No] , case when isnull (Tspl_Milk_Gate_Entry_Details.isPosted,0)=0 then 'NO' else 'Yes' end as [Is Posted] ,Tspl_Milk_Gate_Entry_Details.Posting_Date as [Posting Date] ,Tspl_Milk_Gate_Entry_Details.Dispatched_From_Mcc as [Dispatched From Mcc] ,Tspl_Milk_Gate_Entry_Details.location_Code as [Location Code] ,Tspl_Milk_Gate_Entry_Details.Location_Desc as [Location Desc] ,Tspl_Milk_Gate_Entry_Details.Vendor_Code as [Vendor Code] ,Tspl_Milk_Gate_Entry_Details.Vendor_Desc as [Vendor Desc] ,Tspl_Milk_Gate_Entry_Details.Item_Code as [Item Code] ,Tspl_Milk_Gate_Entry_Details.Item_Desc as [Item Desc] ,Tspl_Milk_Gate_Entry_Details.Qty_In_Kg as [Qty] ,Tspl_Milk_Gate_Entry_Details.snf_Per as [SNF %] ,Tspl_Milk_Gate_Entry_Details.fat_per as [FAT %] ,Tspl_Milk_Gate_Entry_Details.Created_By as [Created By] ,Tspl_Milk_Gate_Entry_Details.Created_Date as [Created Date] ,Tspl_Milk_Gate_Entry_Details.Modify_By as [Modify By] ,Tspl_Milk_Gate_Entry_Details.Modify_Date as [Modify Date] ,Tspl_Milk_Gate_Entry_Details.comp_code as [Company Code]  From Tspl_Milk_Gate_Entry_Details left outer join TSPL_MILK_Weighment_Detail on TSPL_MILK_Weighment_Detail.Gate_Entry_No=Tspl_Milk_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_MILK_QUALITY_CHECK on TSPL_MILK_QUALITY_CHECK.gate_entry_no=Tspl_Milk_Gate_Entry_Details.Gate_Entry_No left outer join TSPL_JOB_MILK_UNLOADING on TSPL_JOB_MILK_UNLOADING.Gate_Entry_No=Tspl_Milk_Gate_Entry_Details.Gate_Entry_No left outer join TSPL_JOB_MILK_Cleaning on TSPL_JOB_MILK_Cleaning.Gate_Entry_No=Tspl_Milk_Gate_Entry_Details.Gate_Entry_No  where TSPL_JOB_MILK_Cleaning .isPosted='1' and Tspl_Milk_Gate_Entry_Details.Doc_Type='MccProc') xxx"
            Dim qry As String = " select Gate_Entry_No as [GateEntryNo],Weighment_No as [Weighment No],QC_No as [Qc no],Unloading_No as [Unloading No] from(  select * from TSPL_JOB_MILK_UNLOADING where isPosted='1' and Gate_Entry_No not in (select Gate_Entry_No  from TSPL_JOB_MILK_Cleaning )) xxx "
            str = clsCommon.ShowSelectForm("GTENUN", qry, "GateEntryNo", whrcls, curcode, "GateEntryNo", isButtonClicked)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function getTankerFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrcls = " and Tspl_Milk_Gate_Entry_Details.location_Code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If

        Try
            Dim qry As String = " select xxx.Tanker_No as [TankerNo], xxx.Gate_Entry_No as [GateEntryNo],xxx.Date_And_Time as [Gate Entry Date]  ,xxx.Weighment_No as [Weighment No],xxx.Weighment_date  as [Weighment Date], isnull(convert(varchar,xxx.Tare_Weight_date,103),'') as  [Tare Weighment Date],xxx.QC_No as [Qc no],xxx.QC_In_Date_Time as [QC In Date],xxx.QC_Out_Date_Time as [QC Out Date], xxx.Unloading_No as [Unloading No],xxx.Unloading_Date_Time as [Unloading Date] from(  select TSPL_JOB_MILK_UNLOADING.*,TSPL_MILK_Weighment_Detail.Weighment_date ,TSPL_MILK_Weighment_Detail.Tare_Weight_date ,Tspl_Milk_Gate_Entry_Details.Date_And_Time ,TSPL_MILK_QUALITY_CHECK.QC_In_Date_Time ,TSPL_MILK_QUALITY_CHECK.QC_Out_Date_Time  from TSPL_JOB_MILK_UNLOADING left outer join TSPL_MILK_Weighment_Detail on TSPL_MILK_Weighment_Detail.Weighment_No=TSPL_JOB_MILK_UNLOADING.Weighment_No left outer join Tspl_Milk_Gate_Entry_Details  on Tspl_Milk_Gate_Entry_Details .Gate_Entry_No =TSPL_JOB_MILK_UNLOADING.Gate_Entry_No left outer join TSPL_MILK_QUALITY_CHECK  on TSPL_MILK_QUALITY_CHECK .QC_No =TSPL_JOB_MILK_UNLOADING.QC_No    where TSPL_JOB_MILK_UNLOADING.isPosted='1' and TSPL_JOB_MILK_UNLOADING.Gate_Entry_No not in (select Gate_Entry_No  from TSPL_JOB_MILK_Cleaning )  " & whrcls & ") xxx "
            'str = clsCommon.ShowSelectForm("GTENUN", qry, "GateEntryNo", whrcls, curcode, "GateEntryNo", isButtonClicked)
            str = customFinder.getFinder("TNKRFNDCLN", qry, "", "TankerNo", curcode, "GateEntryNo")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function saveData(ByVal obj As clsMilkCleaning, ByVal trans As SqlTransaction, Optional ByVal isHistory As Boolean = False) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_No", clsCommon.myCstr(obj.Doc_No))
            clsCommon.AddColumnsForChange(coll, "Start_Date_Time", clsCommon.GetPrintDate(obj.Start_Date_Time, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "End_Date_Time", clsCommon.GetPrintDate(obj.End_Date_Time, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", clsCommon.myCstr(obj.Gate_Entry_No))
            clsCommon.AddColumnsForChange(coll, "Weighment_No", clsCommon.myCstr(obj.Weighment_No))
            clsCommon.AddColumnsForChange(coll, "QC_No", clsCommon.myCstr(obj.QC_No))
            clsCommon.AddColumnsForChange(coll, "Tanker_No", clsCommon.myCstr(obj.Tanker_No))
            clsCommon.AddColumnsForChange(coll, "Done_by", clsCommon.myCstr(obj.Done_by), True)
            clsCommon.AddColumnsForChange(coll, "Checked_by", clsCommon.myCstr(obj.Checked_by), True)
            clsCommon.AddColumnsForChange(coll, "Status", clsCommon.myCstr(obj.Status))
            clsCommon.AddColumnsForChange(coll, "Remarks", clsCommon.myCstr(obj.Remarks))
            clsCommon.AddColumnsForChange(coll, "isPosted", obj.isPosted)
            If obj.isPosted = 1 Then
                clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Posting_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", clsCommon.myCstr(obj.comp_code))
            If obj.isNewEntry Or isHistory Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isHistory, "TSPL_JOB_MILK_Cleaning_History", "TSPL_JOB_MILK_Cleaning"), OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JOB_MILK_Cleaning", OMInsertOrUpdate.Update, "TSPL_JOB_MILK_Cleaning.Doc_no='" + obj.Doc_No + "'", trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsMilkCleaning
        Dim obj As New clsMilkCleaning
        Try
            Dim qst As String = " select TSPL_JOB_MILK_Cleaning.*   From TSPL_JOB_MILK_Cleaning left outer join Tspl_Milk_Gate_Entry_Details on Tspl_Milk_Gate_Entry_Details.Gate_Entry_No   =TSPL_JOB_MILK_Cleaning.Gate_Entry_No where 1=1  "
            Dim strwhrcls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    strwhrcls = " and Tspl_Milk_Gate_Entry_Details.location_Code  in (" & objCommonVar.strCurrUserLocations & ") "
                End If
            End If
            qst += strwhrcls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_JOB_MILK_Cleaning.Doc_no in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_JOB_MILK_Cleaning.Doc_no in (select min(TSPL_JOB_MILK_Cleaning.Doc_no ) from TSPL_JOB_MILK_Cleaning left outer join Tspl_Milk_Gate_Entry_Details on Tspl_Milk_Gate_Entry_Details.Gate_Entry_No   =TSPL_JOB_MILK_Cleaning.Gate_Entry_No  where TSPL_JOB_MILK_Cleaning.Doc_No  >'" + strCode + "' " & strwhrcls & "   )"
                Case NavigatorType.First
                    qst += " and TSPL_JOB_MILK_Cleaning.Doc_no in (select MIN(TSPL_JOB_MILK_Cleaning.Doc_no ) from TSPL_JOB_MILK_Cleaning left outer join Tspl_Milk_Gate_Entry_Details on Tspl_Milk_Gate_Entry_Details.Gate_Entry_No   =TSPL_JOB_MILK_Cleaning.Gate_Entry_No where 1=1 " & strwhrcls & "  )"
                Case NavigatorType.Last
                    qst += " and TSPL_JOB_MILK_Cleaning.Doc_no in (select Max(TSPL_JOB_MILK_Cleaning.Doc_no ) from TSPL_JOB_MILK_Cleaning left outer join Tspl_Milk_Gate_Entry_Details on Tspl_Milk_Gate_Entry_Details.Gate_Entry_No   =TSPL_JOB_MILK_Cleaning.Gate_Entry_No where 1=1 " & strwhrcls & "   )"
                Case NavigatorType.Previous
                    qst += " and TSPL_JOB_MILK_Cleaning.Doc_no in (select Max(TSPL_JOB_MILK_Cleaning.Doc_no ) from TSPL_JOB_MILK_Cleaning left outer join Tspl_Milk_Gate_Entry_Details on Tspl_Milk_Gate_Entry_Details.Gate_Entry_No   =TSPL_JOB_MILK_Cleaning.Gate_Entry_No  where TSPL_JOB_MILK_Cleaning.Doc_No  <'" + strCode + "' " & strwhrcls & "   )"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Doc_No = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
                obj.Start_Date_Time = clsCommon.myCDate(dt.Rows(0)("Start_Date_Time"), "dd/MMM/yyyy hh:mm:ss tt")
                obj.End_Date_Time = clsCommon.myCDate(dt.Rows(0)("End_Date_Time"), "dd/MMM/yyyy hh:mm:ss tt")
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
                obj.Weighment_No = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
                obj.QC_No = clsCommon.myCstr(dt.Rows(0)("QC_No"))
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                obj.Done_by = clsCommon.myCstr(dt.Rows(0)("Done_by"))
                obj.Checked_by = clsCommon.myCstr(dt.Rows(0)("Checked_by"))
                obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function isCleaningDone(ByVal strGateEntryNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        qry = "select count(*) from TSPL_JOB_MILK_Cleaning where gate_entry_no='" & strGateEntryNo & "' and isposted=1"
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) <= 0 Then
            Return False
        Else
            Return True
        End If
    End Function
End Class
