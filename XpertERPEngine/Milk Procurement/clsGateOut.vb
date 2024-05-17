Imports common
Imports System.Data.SqlClient
Public Class clsGateOut
    Public DriverName As String = Nothing
    Public IsPosted As Integer = 0
    Public AllocateToMCC As String = Nothing
    Public IsAgainstJobWork As Integer = 0
    Public Joblocation_Code As String = Nothing
    Public AcknowEntryDocument_No As String = Nothing
    Public Doc_No As String = String.Empty
    Public Doc_Date As Date = Nothing
    Public Gate_Entry_No As String = String.Empty
    Public Weighment_No As String = String.Empty
    Public QC_No As String = String.Empty
    Public Tanker_No As String = String.Empty
    Public Created_By As String = String.Empty
    Public Created_Date As String = String.Empty
    Public Modify_By As String = String.Empty
    Public Modify_Date As String = String.Empty
    Public comp_code As String = String.Empty
    Public isNewEntry As Boolean = False
    'Public Shared Function postData(ByVal StrDocNo As String, ByVal formId As String) As Boolean
    '    Dim trans As SqlTransaction = Nothing
    '    Try
    '        Dim isPosted As Boolean = True
    '        If (clsCommon.myLen(StrDocNo) <= 0) Then
    '            Throw New Exception("Gate Out Doc No not found to Post")
    '        End If

    '        Dim obj As clsGateOut = clsGateOut.getData(StrDocNo, NavigatorType.Current)
    '        If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_No) <= 0) Then
    '            Throw New Exception("No Data found to Post")
    '        End If
    '        trans = clsDBFuncationality.GetTransactin()
    '        '--------------------
    '        Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(formId, "TSPL_Gate_Out", "Doc_no", obj.Doc_No, trans)
    '        If isResult = False Then
    '            trans.Commit()
    '            Return False
    '        End If
    '        Dim strQry As String = " update TSPL_cleaning set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where doc_no='" & StrDocNo & "'"
    '        isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)

    '        If isPosted Then
    '            trans.Commit()
    '        Else
    '            trans.Rollback()
    '        End If
    '        Return isPosted
    '    Catch ex As Exception
    '        trans.Rollback()
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Function

    Public Shared Function deleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select tspl_gate_entry_details.location_Code,TSPL_Gate_Out.Doc_Date from tspl_gate_entry_details
left outer join TSPL_Gate_Out on TSPL_Gate_Out.Gate_Entry_No=tspl_gate_entry_details.Gate_Entry_No
where TSPL_Gate_Out.Doc_No='" + strDocNo + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmGateOut, clsCommon.myCstr(dt.Rows(0)("location_Code")), clsCommon.myCDate(dt.Rows(0)("Doc_Date")), trans)
            End If

            Dim qry As String = "delete from TSPL_Gate_Out where doc_No='" & strDocNo & "'"
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
            Dim qry As String = " select TSpl_Gate_Out.Doc_No as [DocumentNo] ,TSpl_Gate_Out.Doc_Date as [Doc Date] ,TSpl_Gate_Out.Gate_Entry_No as [Gate Entry No] ,TSpl_Gate_Out.Weighment_No as [Weighment No] ,TSpl_Gate_Out.QC_No as [Qc No] ,TSpl_Gate_Out.Tanker_No as [Tanker No] ,TSpl_Gate_Out.Created_By as [Created By] ,TSpl_Gate_Out.Created_Date as [Created Date] ,TSpl_Gate_Out.Modify_By as [Modify By] ,TSpl_Gate_Out.Modify_Date as [Modify Date] ,TSpl_Gate_Out.comp_code as [Company Code] From TSpl_Gate_Out  left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No =TSpl_Gate_Out.Gate_Entry_No "
            str = clsCommon.ShowSelectForm("GTOUT", qry, "DocumentNo", whrcls, curcode, "TSpl_Gate_Out.Doc_Date desc", isButtonClicked, "TSpl_Gate_Out.Doc_Date")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function getGateEntryFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "select * from (  select TSPL_gate_entry_details.Tanker_No as [Tanker No], TSPL_gate_entry_details.Gate_Entry_No as [GateEntryNo] ,TSPL_gate_entry_details.Doc_Type as [Doc Type]   ,TSPL_Weighment_Detail.Weighment_No as [Weighment No],tspl_quality_check.QC_No as [QC No]  ,TSPL_gate_entry_details.Challan_No as [Challan No] ,TSPL_gate_entry_details.Dispatched_From_Mcc as [Dispatched From Mcc] ,TSPL_gate_entry_details.location_Code as [Location Code] ,TSPL_gate_entry_details.Location_Desc as [Location Desc] ,TSPL_gate_entry_details.Vendor_Code as [Vendor Code] ,TSPL_gate_entry_details.Vendor_Desc as [Vendor Desc] ,TSPL_gate_entry_details.Item_Code as [Item Code] ,TSPL_gate_entry_details.Item_Desc as [Item Desc]   From TSPL_gate_entry_details left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join tspl_quality_check on tspl_quality_check.gate_entry_no=TSPL_gate_entry_details.Gate_Entry_No   where (TSPL_Weighment_Detail.isPosted='1' or tspl_quality_check.is_param_accepted='0') and Tspl_Gate_Entry_Details.Doc_Type='BulkProc'   union all   select TSPL_gate_entry_details.Tanker_No as [Tanker No], TSPL_gate_entry_details.Gate_Entry_No as [GateEntryNo] ,TSPL_gate_entry_details.Doc_Type as [Doc Type]   ,TSPL_Weighment_Detail.Weighment_No as [Weighment No],tspl_quality_check.QC_No as [QC No]  ,TSPL_gate_entry_details.Challan_No as [Challan No] ,TSPL_gate_entry_details.Dispatched_From_Mcc as [Dispatched From Mcc] ,TSPL_gate_entry_details.location_Code as [Location Code] ,TSPL_gate_entry_details.Location_Desc as [Location Desc] ,TSPL_gate_entry_details.Vendor_Code as [Vendor Code] ,TSPL_gate_entry_details.Vendor_Desc as [Vendor Desc] ,TSPL_gate_entry_details.Item_Code as [Item Code] ,TSPL_gate_entry_details.Item_Desc as [Item Desc]   From TSPL_gate_entry_details left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join tspl_quality_check on tspl_quality_check.gate_entry_no=TSPL_gate_entry_details.Gate_Entry_No   where TSPL_Weighment_Detail.isPosted='1'  and Tspl_Gate_Entry_Details.Doc_Type='MccProc'    ) xxx   "
            str = clsCommon.ShowSelectForm("GTENUN", qry, "GateEntryNo", whrcls, curcode, "GateEntryNo", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function getTankerFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try

            Dim strBulkCleaningMandatory As String = ""
            Dim strMCCCleaningMandatory As String = ""
            Dim isCleaningMandatoryBeforeGateout As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isCleaningMandatoryBeforeGateout, clsFixedParameterCode.isCleaningMandatoryBeforeGateout, Nothing))
            Dim ShowBothTankertypeOnCleaning As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowBothTankertypeOnCleaning, clsFixedParameterCode.ShowBothTankertypeOnCleaning, Nothing))
            If isCleaningMandatoryBeforeGateout = 1 Then
                If ShowBothTankertypeOnCleaning = 1 Then
                    strBulkCleaningMandatory = "  and  case when is_param_accepted <> 0 then  (select count(*) from TSPL_Cleaning where TSPL_Cleaning.Gate_Entry_No=TSPL_gate_entry_details.Gate_Entry_No) else '1'  end  =1  "
                End If
                strMCCCleaningMandatory = "  and  case when is_param_accepted <> 0 then  (select count(*) from TSPL_Cleaning where TSPL_Cleaning.Gate_Entry_No=TSPL_gate_entry_details.Gate_Entry_No) else '1'  end  =1  "
            End If
            ''richa VIJ/14/10/19-000006 
            Dim qry As String = "select * from (  select TSPL_gate_entry_details.Tanker_No as [TankerNo], TSPL_gate_entry_details.Gate_Entry_No as [GateEntryNo] ,TSPL_gate_entry_details.Doc_Type as [Doc Type]   ,TSPL_Weighment_Detail.Weighment_No as [Weighment No],tspl_quality_check.QC_No as [QC No]  ,TSPL_gate_entry_details.Challan_No as [Challan No] ,TSPL_gate_entry_details.Dispatched_From_Mcc as [Dispatched From Mcc] ,TSPL_gate_entry_details.location_Code as [Location Code] ,TSPL_gate_entry_details.Location_Desc as [Location Desc] ,TSPL_gate_entry_details.Vendor_Code as [Vendor Code] ,TSPL_gate_entry_details.Vendor_Desc as [Vendor Desc] ,TSPL_gate_entry_details.Item_Code as [Item Code] ,TSPL_gate_entry_details.Item_Desc as [Item Desc]   From TSPL_gate_entry_details left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join tspl_quality_check on tspl_quality_check.gate_entry_no=TSPL_gate_entry_details.Gate_Entry_No   where (TSPL_Weighment_Detail.isPosted='1' or (tspl_quality_check.is_param_accepted='0' and tspl_quality_check.is_QC_Separated =0)  or Tspl_Gate_Entry_Details.Tanker_Return=1) and Tspl_Gate_Entry_Details.Doc_Type='BulkProc' " & strBulkCleaningMandatory & " union all  " & _
                " select TSPL_gate_entry_details.Tanker_No as [TankerNo], TSPL_gate_entry_details.Gate_Entry_No as [GateEntryNo] ,TSPL_gate_entry_details.Doc_Type as [Doc Type]   ,TSPL_Weighment_Detail.Weighment_No as [Weighment No],tspl_quality_check.QC_No as [QC No]  ,TSPL_gate_entry_details.Challan_No as [Challan No] ,TSPL_gate_entry_details.Dispatched_From_Mcc as [Dispatched From Mcc] ,TSPL_gate_entry_details.location_Code as [Location Code] ,TSPL_gate_entry_details.Location_Desc as [Location Desc] ,TSPL_gate_entry_details.Vendor_Code as [Vendor Code] ,TSPL_gate_entry_details.Vendor_Desc as [Vendor Desc] ,TSPL_gate_entry_details.Item_Code as [Item Code] ,TSPL_gate_entry_details.Item_Desc as [Item Desc]   From TSPL_gate_entry_details left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join tspl_quality_check on tspl_quality_check.gate_entry_no=TSPL_gate_entry_details.Gate_Entry_No   where TSPL_Weighment_Detail.isPosted='1'  and Tspl_Gate_Entry_Details.Doc_Type='MccProc'  " & strMCCCleaningMandatory & " "
            If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.GateOutTankerNoAfterGeneralWeighment, clsFixedParameterCode.GateOutTankerNoAfterGeneralWeighment, Nothing)) = "1", True, False)) Then
                qry += " Union All " & _
             " select TSPL_gate_entry_details.Tanker_No as [TankerNo], TSPL_gate_entry_details.Gate_Entry_No as [GateEntryNo] ,TSPL_gate_entry_details.Doc_Type as [Doc Type] ,TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No as [Weighment No],'' as [QC No]  ,TSPL_gate_entry_details.Challan_No as [Challan No] ,TSPL_gate_entry_details.Dispatched_From_Mcc as [Dispatched From Mcc] ,TSPL_gate_entry_details.location_Code as [Location Code] ,TSPL_gate_entry_details.Location_Desc as [Location Desc] ,TSPL_gate_entry_details.Vendor_Code as [Vendor Code] ,TSPL_gate_entry_details.Vendor_Desc as [Vendor Desc] ,TSPL_gate_entry_details.Item_Code as [Item Code] ,TSPL_gate_entry_details.Item_Desc as [Item Desc]   From TSPL_gate_entry_details " & _
             " inner join TSPL_GENERAL_WEIGHMENT_DETAIL on TSPL_GENERAL_WEIGHMENT_DETAIL.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No  " & _
             "  where TSPL_GENERAL_WEIGHMENT_DETAIL.Gate_Entry_No is not null and TSPL_GENERAL_WEIGHMENT_DETAIL.IsJobWork = 1   and (TSPL_GENERAL_WEIGHMENT_DETAIL.Posted='1'   or Tspl_Gate_Entry_Details.Tanker_Return=1) "
            End If
            qry += " ) xxx   "
            str = customFinder.getFinder("TNKRGTENGTOUT", qry, whrcls, "TankerNo", curcode, "GateEntryNo")
            'str = clsCommon.ShowSelectForm("GTENGTOUT", qry, "TankerNo", whrcls, curcode, "GateEntryNo", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function saveData(ByVal obj As clsGateOut, ByVal trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
        Try
            Dim strLocation_Code As String = clsDBFuncationality.getSingleValue("select tspl_gate_entry_details.location_Code from tspl_gate_entry_details
left outer join TSPL_Gate_Out on TSPL_Gate_Out.Gate_Entry_No=tspl_gate_entry_details.Gate_Entry_No
where TSPL_Gate_Out.Doc_No='" + obj.Doc_No + "'", trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmGateOut, strLocation_Code, obj.Doc_Date, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_No", clsCommon.myCstr(obj.Doc_No))

            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "IsAgainstJobWork", obj.IsAgainstJobWork)
            clsCommon.AddColumnsForChange(coll, "Joblocation_Code", obj.Joblocation_Code)
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", clsCommon.myCstr(obj.Gate_Entry_No), True)
            clsCommon.AddColumnsForChange(coll, "AcknowEntryDocument_No", clsCommon.myCstr(obj.AcknowEntryDocument_No), True)
            clsCommon.AddColumnsForChange(coll, "Weighment_No", clsCommon.myCstr(obj.Weighment_No), True)
            clsCommon.AddColumnsForChange(coll, "QC_No", clsCommon.myCstr(obj.QC_No), True)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", clsCommon.myCstr(obj.Tanker_No))
            clsCommon.AddColumnsForChange(coll, "Modify_By", clsCommon.myCstr(obj.Modify_By))
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.myCstr(obj.Modify_Date))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", clsCommon.myCstr(obj.comp_code))
            clsCommon.AddColumnsForChange(coll, "AllocateToMCC", clsCommon.myCstr(obj.AllocateToMCC), True)
            clsCommon.AddColumnsForChange(coll, "DriverName", clsCommon.myCstr(obj.DriverName), True)
            'clsCommon.AddColumnsForChange(coll, "IsPosted", obj.IsPosted)
            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", clsCommon.myCstr(obj.Created_By))
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(obj.Created_Date))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Gate_Out", OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Gate_Out", OMInsertOrUpdate.Update, "TSPL_Gate_Out.Doc_no='" + obj.Doc_No + "'", trans)
            End If
            coll = New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Modified_By", clsCommon.myCstr(obj.Modify_By))
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans, "dd/MM/yyyy hh:mm:ss tt"), "dd/MM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "isGateOut", 1)
            clsCommon.AddColumnsForChange(coll, "Ref_Doc_No", "", True)
            issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_tanker_master", OMInsertOrUpdate.Update, "tspl_tanker_master.tanker_no='" + obj.Tanker_No + "'", trans)
            Dim QCStatus As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select is_Param_Accepted from tspl_quality_check where QC_No='" & obj.QC_No & "'", trans))
            If QCStatus = 0 Then
                issaved = issaved And clsDBFuncationality.ExecuteNonQuery("Update TSPL_Weighment_Detail set isPosted=1,Posting_Date='" & clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy") & "' where Weighment_No='" & obj.Weighment_No & "'", trans)

            End If
            SaveEmailText(clsCommon.myCstr(obj.Doc_No), trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType) As clsGateOut
        Dim obj As New clsGateOut
        Try
            Dim qst As String = " select TSPL_Gate_Out.*  From TSPL_Gate_Out  left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No =TSpl_Gate_Out.Gate_Entry_No   where 1=1  "
            Dim strwhrcls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    strwhrcls = " and Tspl_Gate_Entry_Details.location_Code  in (" & objCommonVar.strCurrUserLocations & ") "
                End If
            End If
            qst += strwhrcls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_Gate_Out.Doc_no in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_Gate_Out.Doc_no in (select min(TSPL_Gate_Out.Doc_no ) from TSPL_Gate_Out left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No  =TSpl_Gate_Out.Gate_Entry_No  where TSPL_Gate_Out.Doc_No  >'" + strCode + "' " & strwhrcls & "   )"
                Case NavigatorType.First
                    qst += " and TSPL_Gate_Out.Doc_no in (select MIN(TSPL_Gate_Out.Doc_no ) from TSPL_Gate_Out left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No  =TSpl_Gate_Out.Gate_Entry_No where 1=1  " & strwhrcls & "  )"
                Case NavigatorType.Last
                    qst += " and TSPL_Gate_Out.Doc_no in (select Max(TSPL_Gate_Out.Doc_no ) from TSPL_Gate_Out left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No  =TSpl_Gate_Out.Gate_Entry_No where 1=1  " & strwhrcls & "   )"
                Case NavigatorType.Previous
                    qst += " and TSPL_Gate_Out.Doc_no in (select Max(TSPL_Gate_Out.Doc_no ) from TSPL_Gate_Out left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No  =TSpl_Gate_Out.Gate_Entry_No where TSPL_Gate_Out.Doc_No  <'" + strCode + "'  " & strwhrcls & "  )"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.IsAgainstJobWork = clsCommon.myCdbl(dt.Rows(0)("IsAgainstJobWork"))
                obj.Joblocation_Code = clsCommon.myCstr(dt.Rows(0)("Joblocation_Code"))
                obj.AcknowEntryDocument_No = clsCommon.myCstr(dt.Rows(0)("AcknowEntryDocument_No"))
                obj.Doc_No = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
                obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"), "dd/MMM/yyyy hh:mm:ss tt")
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
                obj.Weighment_No = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
                obj.QC_No = clsCommon.myCstr(dt.Rows(0)("QC_No"))
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                obj.AllocateToMCC = clsCommon.myCstr(dt.Rows(0)("AllocateToMCC"))
                obj.DriverName = clsCommon.myCstr(dt.Rows(0)("DriverName"))
                obj.IsPosted = clsCommon.myCdbl(dt.Rows(0)("IsPosted"))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function isGateOutDone(ByVal strGateEntryNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        qry = "select count(*) from tspl_Gate_Out where gate_entry_no='" & strGateEntryNo & "' "
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) <= 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    ' Ticket No : MIL/12/06/19-000097 By Prabhakar
    Public Shared Function SaveEmailText(ByVal GateOutNo As String, ByVal trans As SqlTransaction) As Boolean  ' , ByVal strPath As String, ByVal arrRecepients As List(Of String)
        Dim objES As clsESContent
        objES = clsESContent.GetData(clsUserMgtCode.frmGateOut, trans)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_Gate_Out.Doc_No as GateOutNo,TSPL_Gate_Out.Doc_Date as GateOutDate, tspl_bulk_milk_srn.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,tspl_bulk_milk_srn.Net_Weight,tspl_bulk_milk_srn.NetRate, tspl_bulk_milk_srn.Actual_Amount  from TSPL_Gate_Out inner join tspl_bulk_milk_srn on tspl_bulk_milk_srn.Gate_Entry_No = TSPL_Gate_Out.Gate_Entry_No left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER.Vendor_Code = tspl_bulk_milk_srn.Vendor_Code  where  TSPL_Gate_Out.Doc_No= '" + GateOutNo + "' ", trans)

        If objES IsNot Nothing AndAlso dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim objSMSH As New clsEMailHead()
            objSMSH.Email_Subject = objES.EMail_Subject
            objSMSH.Email_Text = objES.EMail_Text
            objSMSH.IsBodyHtml = "1"
            objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.GateOutNo, clsCommon.myCstr(dt.Rows(0)("GateOutNo")))
            objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(frmEMailAndSMSSetting.GateOutNo, clsCommon.myCstr(dt.Rows(0)("GateOutNo")))

            objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.GateOutDate, clsCommon.myCstr(dt.Rows(0)("GateOutDate")))
            objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(frmEMailAndSMSSetting.GateOutNo, clsCommon.myCstr(dt.Rows(0)("GateOutDate")))

            objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, clsCommon.myCstr(dt.Rows(0)("Vendor_Name")))
            objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(frmEMailAndSMSSetting.Vendor_Name, clsCommon.myCstr(dt.Rows(0)("Vendor_Name")))

            objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.NetWeight, clsCommon.myCstr(dt.Rows(0)("Net_Weight")))
            objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(frmEMailAndSMSSetting.NetWeight, clsCommon.myCstr(dt.Rows(0)("Net_Weight")))

            objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Rate, clsCommon.myCstr(dt.Rows(0)("NetRate")))
            objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(frmEMailAndSMSSetting.Rate, clsCommon.myCstr(dt.Rows(0)("NetRate")))

            objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Amount, clsCommon.myCstr(dt.Rows(0)("Actual_Amount")))
            objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(frmEMailAndSMSSetting.Amount, clsCommon.myCstr(dt.Rows(0)("Actual_Amount")))

            Dim dtQC As DataTable = clsDBFuncationality.GetDataTable("select TSPL_QC_Parameter_Detail.Param_Field_Desc, Param_Field_Value from TSPL_QC_Parameter_Detail where Param_Field_Value is not null and Param_Field_Value <> '' and  QC_No = (select TSPL_Gate_Out.QC_No from TSPL_Gate_Out where Doc_No = '" + GateOutNo + "') ", trans)
            If dtQC IsNot Nothing AndAlso dtQC.Rows.Count > 0 Then
                Dim strQC As String = " <table border=" + "1" + " cellpadding=" + "1" + " cellspacing=" + "0" + " width = " + "450" + "><tr bgcolor=''#4da6ff''><td width=100><b>Parameters</b></td><td width=100><b> Value</b></td></tr>  "
                For Each row As DataRow In dtQC.Rows
                    strQC += " <tr><td>" + clsCommon.myCstr(row("Param_Field_Desc")) + "</td><td> " + clsCommon.myCstr(row("Param_Field_Value")) + "</td></tr> "
                Next
                strQC += " </table> "
                objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.QCParameterDetails, strQC)
                objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(frmEMailAndSMSSetting.QCParameterDetails, strQC)
            End If

            'objSMSH.Attachment_1_Path = strPath
            'objSMSH.arrEMail = New List(Of String)()

            'For Each strRece As String In arrRecepients
            '    objSMSH.arrEMail.Add(strRece)
            'Next

            objSMSH.SaveData(clsUserMgtCode.frmGateOut, objSMSH, trans)
            objSMSH = Nothing
        End If
        Return True
    End Function

End Class
