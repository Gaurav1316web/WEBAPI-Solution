Imports common
Imports System.Data.SqlClient

Public Class clsCleaning
#Region "Variables"
    Public IsAgainstJobWork As Integer = 0
    Public Joblocation_Code As String = Nothing
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
    Public InTime As DateTime?
    Public OutTime As DateTime?
#End Region
    
    Public Shared Function postData(ByVal StrDocNo As String, ByVal formId As String) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(StrDocNo) <= 0) Then
                Throw New Exception("Cleaning Doc No not found to Post")
            End If

            Dim obj As clsCleaning = clsCleaning.getData(StrDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            trans = clsDBFuncationality.GetTransactin()
            Dim strLocation_Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_gate_entry_details.location_Code from tspl_gate_entry_details 
left outer join TSPL_Cleaning on TSPL_Cleaning.Gate_Entry_No= tspl_gate_entry_details.Gate_Entry_No 
where TSPL_Cleaning.Doc_No ='" + StrDocNo + "'", trans))

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmCleaning, strLocation_Code, obj.Start_Date_Time, trans)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(formId, "TSPL_Cleaning", "Doc_no", obj.Doc_No, trans)
            If isResult = False Then
                trans.Commit()
                Return False
            End If

            CreateIssueEntry(obj, trans) '-----ERO/27/02/19-000502 by balwinder on 01/03/2019


            Dim MCCChamberwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsChamberWiseTanker, clsFixedParameterCode.IsChamberWiseTanker, trans))
            Dim settTankerDispatchIntermittentSingleGateIn As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchIntermittentSingleGateIn, clsFixedParameterCode.TankerDispatchIntermittentSingleGateIn, trans)) = 1)
            Dim TempDocType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Doc_Type from tspl_weighment_detail where weighment_no='" & obj.Weighment_No & "'", trans))
            Dim rValue As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isintermittent from TSPL_MCC_Dispatch_Challan  left outer join Tspl_Gate_Entry_Details  on Tspl_Gate_Entry_Details.challan_no=TSPL_MCC_Dispatch_Challan.chalan_no  left join TSPL_Cleaning on Tspl_Gate_Entry_Details.Gate_Entry_No   =TSpl_Cleaning.Gate_Entry_No  where TSPL_Cleaning.Doc_No ='" & obj.Doc_No & "' ", trans))
            If rValue = 1 AndAlso settTankerDispatchIntermittentSingleGateIn = True AndAlso MCCChamberwise = 1 AndAlso clsCommon.CompairString(TempDocType, "MccProc") = CompairStringResult.Equal Then
                isPosted = clsQualityCheck.SaveGateOutData(obj.Gate_Entry_No, trans)
            End If

            Dim strQry As String = " update TSPL_cleaning set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where doc_no='" & StrDocNo & "'"
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

    Shared Function CreateIssueEntry(ByVal objCleaning As clsCleaning, ByVal trans As SqlTransaction) As Boolean
        Dim strGELoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_Code from tspl_gate_entry_details where Gate_Entry_No='" + objCleaning.Gate_Entry_No + "'", trans))
        Dim objCleaningItem As clsTankerCleaningItemHead = clsTankerCleaningItemHead.GetLatestCleaningItem(strGELoc, objCleaning.End_Date_Time, trans)
        ''richa agarwal 13 Mar,2019 add condition objCleaningItem IsNot Nothing MIL/13/03/19-000054
        If objCleaningItem IsNot Nothing Then
            If objCleaningItem.Arr IsNot Nothing AndAlso objCleaningItem.Arr.Count > 0 Then
                Dim obj As New clsIssueReturnHead()
                obj.Doc_Date = objCleaning.End_Date_Time
                obj.Doc_Type = "Issue"
                obj.Remarks = "Auto Issue Cleaning No" + objCleaning.Doc_No
                obj.From_Location = objCleaningItem.Sub_Location
                obj.BeforeTax_Amt = 0
                obj.Total_Tax_Amt = 0
                obj.doc_Amt = 0
                obj.Againt_Cleaning_No = objCleaning.Doc_No
                obj.Tanker_Cleaning_Item_No = objCleaningItem.Arr(0).Code
                obj.Arr = New List(Of clsIssueReturnDetail)
                For Each grow As clsTankerCleaningItemDetail In objCleaningItem.Arr
                    Dim objTr As New clsIssueReturnDetail()
                    objTr.arrBatchItem = New List(Of clsBatchInventory)
                    objTr.Line_No = grow.SNo
                    objTr.Req_IssueNo = ""
                    objTr.Item_Code = grow.Item_Code
                    objTr.Item_Desc = grow.Item_Desc
                    objTr.Required_Qty = grow.Qty
                    objTr.Issued_Qty = grow.Qty
                    objTr.Unit_code = grow.Unit_Code
                    Dim dblBalQty As Decimal = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(objTr.Item_Code, obj.From_Location, "", obj.Doc_Date, trans, objTr.Unit_code)
                    If objTr.Issued_Qty > dblBalQty Then
                        Throw New Exception("Item [" + objTr.Item_Code + "] location [" + obj.From_Location + "]" + Environment.NewLine + " Issued Quantity - " + clsCommon.myCstr(objTr.Issued_Qty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                    End If

                    Dim dblCostMethod As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Costing_Method as Costing_Method from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Item_Code='" & objTr.Item_Code & "' ", trans))
                    objTr.Unit_Cost = clsInventoryMovement.GetCost(dblCostMethod, objTr.Item_Code, obj.From_Location, 1, obj.Doc_Date, obj.Doc_Date, False, trans)
                    objTr.Unit_Cost = objTr.Unit_Cost * clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Unit_code, trans)

                    objTr.Amount = objTr.Issued_Qty * objTr.Unit_Cost
                    objTr.Total_Tax_Amt = objTr.Amount
                    objTr.Item_Net_Amt = objTr.Amount
                    'objTr.arrSrItem = TryCast(grow.Tag, List(Of clsSerializeInvenotry))
                    'objTr.arrBatchItem = TryCast(grow.Cells(colICode).Tag, List(Of clsBatchInventory))
                    obj.BeforeTax_Amt += objTr.Amount
                    obj.doc_Amt += objTr.Amount
                    If clsCommon.myLen(objTr.Item_Code) > 0 Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
                End If
                obj.SaveData(obj, True, trans)
                clsIssueReturnHead.PostData(obj.Doc_No, trans)
            End If

        End If

        Return True
    End Function

    Public Shared Function deleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Return deleteData(True, strDocNo, trans)
    End Function

    Public Shared Function deleteData(ByVal isCheckForPost As Boolean, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If isCheckForPost Then
                Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select isPosted from TSPL_Cleaning Where Doc_No='" + strDocNo + "'", trans))
                If Status = 1 Then
                    Throw New Exception("This document is already posted.")
                End If
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_Cleaning.Start_Date_Time,tspl_gate_entry_details.location_Code from TSPL_Cleaning
                left outer join tspl_gate_entry_details on tspl_gate_entry_details.Gate_Entry_No=TSPL_Cleaning.Gate_Entry_No
                where TSPL_Cleaning.Doc_No='" + strDocNo + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmUnloading, clsCommon.myCstr(dt.Rows(0)("location_Code")), clsCommon.myCDate(dt.Rows(0)("Start_Date_Time")), trans)
            End If

            Dim qry As String = "delete from TSPL_cleaning where doc_No='" & strDocNo & "'"
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
            Dim qry As String = " select TSpl_Cleaning.Doc_No as [DocumentNo] ,TSpl_Cleaning.Start_Date_Time as [Start Date Time] ,TSpl_Cleaning.End_Date_Time as [End Date Time] ,TSpl_Cleaning.Gate_Entry_No as [Gate Entry No] ,TSpl_Cleaning.Weighment_No as [Weighment No] ,TSpl_Cleaning.QC_No as [Qc No] ,TSpl_Cleaning.Tanker_No as [Tanker No] , case when isnull (TSPL_Cleaning.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,TSpl_Cleaning.Posting_Date as [Posting Date] ,TSpl_Cleaning.Done_by as [Cleaning Done By] ,TSpl_Cleaning.Checked_by as [Checked By] ,TSpl_Cleaning.Status as [Status] ,TSpl_Cleaning.Created_By as [Created By] ,TSpl_Cleaning.Created_Date as [Created Date] ,TSpl_Cleaning.Modify_By as [Modify By] ,TSpl_Cleaning.Modify_Date as [Modify Date] ,TSpl_Cleaning.comp_code as [Company Code] From TSpl_Cleaning left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No=TSpl_Cleaning.Gate_Entry_No  "
            str = clsCommon.ShowSelectForm("CLNFND", qry, "DocumentNo", whrcls, curcode, "DocumentNo", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function getGateEntryFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            'Dim qry As String = " select * from (select TSPL_gate_entry_details.Gate_Entry_No as [GateEntryNo] ,TSPL_gate_entry_details.Doc_Type as [Doc Type] ,TSPL_gate_entry_details.Date_And_Time as [ Gate Entry Date And Time],  TSPL_Weighment_Detail.Weighment_No as [Weighment No],tspl_quality_check.QC_No as [QC No]  ,TSPL_gate_entry_details.Challan_No as [Challan No] ,TSPL_gate_entry_details.Challan_Date as [Challan Date] ,TSPL_gate_entry_details.Tanker_No as [Tanker No] , case when isnull (TSPL_gate_entry_details.isPosted,0)=0 then 'NO' else 'Yes' end as [Is Posted] ,TSPL_gate_entry_details.Posting_Date as [Posting Date] ,TSPL_gate_entry_details.Dispatched_From_Mcc as [Dispatched From Mcc] ,TSPL_gate_entry_details.location_Code as [Location Code] ,TSPL_gate_entry_details.Location_Desc as [Location Desc] ,TSPL_gate_entry_details.Vendor_Code as [Vendor Code] ,TSPL_gate_entry_details.Vendor_Desc as [Vendor Desc] ,TSPL_gate_entry_details.Item_Code as [Item Code] ,TSPL_gate_entry_details.Item_Desc as [Item Desc] ,TSPL_gate_entry_details.Qty_In_Kg as [Qty] ,TSPL_gate_entry_details.snf_Per as [SNF %] ,TSPL_gate_entry_details.fat_per as [FAT %] ,TSPL_gate_entry_details.Created_By as [Created By] ,TSPL_gate_entry_details.Created_Date as [Created Date] ,TSPL_gate_entry_details.Modify_By as [Modify By] ,TSPL_gate_entry_details.Modify_Date as [Modify Date] ,TSPL_gate_entry_details.comp_code as [Company Code]  From TSPL_gate_entry_details left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join tspl_quality_check on tspl_quality_check.gate_entry_no=TSPL_gate_entry_details.Gate_Entry_No left outer join TSPL_MILK_UNLOADING on TSPL_MILK_UNLOADING.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No left outer join TSPL_Cleaning on TSPL_Cleaning.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No  where TSPL_MILK_UNLOADING.isPosted='1' and Tspl_Gate_Entry_Details.Doc_Type='BulkProc' and TSPL_QUALITY_CHECK.is_Param_Accepted<>0  union all select TSPL_gate_entry_details.Gate_Entry_No as [GateEntryNo] ,TSPL_gate_entry_details.Doc_Type as [Doc Type] ,TSPL_gate_entry_details.Date_And_Time as [ Gate Entry Date And Time],  TSPL_Weighment_Detail.Weighment_No as [Weighment No],tspl_quality_check.QC_No as [QC No]  ,TSPL_gate_entry_details.Challan_No as [Challan No] ,TSPL_gate_entry_details.Challan_Date as [Challan Date] ,TSPL_gate_entry_details.Tanker_No as [Tanker No] , case when isnull (TSPL_gate_entry_details.isPosted,0)=0 then 'NO' else 'Yes' end as [Is Posted] ,TSPL_gate_entry_details.Posting_Date as [Posting Date] ,TSPL_gate_entry_details.Dispatched_From_Mcc as [Dispatched From Mcc] ,TSPL_gate_entry_details.location_Code as [Location Code] ,TSPL_gate_entry_details.Location_Desc as [Location Desc] ,TSPL_gate_entry_details.Vendor_Code as [Vendor Code] ,TSPL_gate_entry_details.Vendor_Desc as [Vendor Desc] ,TSPL_gate_entry_details.Item_Code as [Item Code] ,TSPL_gate_entry_details.Item_Desc as [Item Desc] ,TSPL_gate_entry_details.Qty_In_Kg as [Qty] ,TSPL_gate_entry_details.snf_Per as [SNF %] ,TSPL_gate_entry_details.fat_per as [FAT %] ,TSPL_gate_entry_details.Created_By as [Created By] ,TSPL_gate_entry_details.Created_Date as [Created Date] ,TSPL_gate_entry_details.Modify_By as [Modify By] ,TSPL_gate_entry_details.Modify_Date as [Modify Date] ,TSPL_gate_entry_details.comp_code as [Company Code]  From TSPL_gate_entry_details left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join tspl_quality_check on tspl_quality_check.gate_entry_no=TSPL_gate_entry_details.Gate_Entry_No left outer join TSPL_MILK_UNLOADING on TSPL_MILK_UNLOADING.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No left outer join TSPL_Cleaning on TSPL_Cleaning.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No  where TSPL_Cleaning .isPosted='1' and Tspl_Gate_Entry_Details.Doc_Type='MccProc') xxx"
            Dim qry As String = " select Gate_Entry_No as [GateEntryNo],Weighment_No as [Weighment No],QC_No as [Qc no],Unloading_No as [Unloading No] from(  select * from TSPL_MILK_UNLOADING where isPosted='1' and Gate_Entry_No not in (select Gate_Entry_No  from TSPL_Cleaning )) xxx "
            str = clsCommon.ShowSelectForm("GTENUN", qry, "GateEntryNo", whrcls, curcode, "GateEntryNo", isButtonClicked)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function getTankerFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim strTankerType As String = ""
        Dim ShowBothTankerType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowBothTankertypeOnCleaning, clsFixedParameterCode.ShowBothTankertypeOnCleaning, Nothing))

        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrcls = " and Tspl_Gate_Entry_Details.location_Code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        If ShowBothTankerType = 0 Then
            strTankerType = " and Tspl_Gate_Entry_Details.Doc_Type ='MccProc'"
        End If
        ''richa agarwal 19-jan-2016 BM00000008707 to show only those tankers whose doctype is MccProc
        Try
            Dim qry As String = " select xxx.Tanker_No as [TankerNo], xxx.Gate_Entry_No as [GateEntryNo],xxx.Date_And_Time as [Gate Entry Date]  ,xxx.Weighment_No as [Weighment No],xxx.Weighment_date  as [Weighment Date], isnull(convert(varchar,xxx.Tare_Weight_date,103),'') as  [Tare Weighment Date],xxx.QC_No as [Qc no],xxx.QC_In_Date_Time as [QC In Date],xxx.QC_Out_Date_Time as [QC Out Date], xxx.Unloading_No as [Unloading No],xxx.Unloading_Date_Time as [Unloading Date] from(  select TSPL_MILK_UNLOADING.*,TSPL_Weighment_Detail.Weighment_date ,TSPL_Weighment_Detail.Tare_Weight_date ,Tspl_Gate_Entry_Details.Date_And_Time ,TSPL_QUALITY_CHECK.QC_In_Date_Time ,TSPL_QUALITY_CHECK.QC_Out_Date_Time,Tspl_Gate_Entry_Details.Doc_Type   from TSPL_MILK_UNLOADING left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Weighment_No=TSPL_MILK_UNLOADING.Weighment_No left outer join Tspl_Gate_Entry_Details  on Tspl_Gate_Entry_Details .Gate_Entry_No =TSPL_MILK_UNLOADING.Gate_Entry_No left outer join TSPL_QUALITY_CHECK  on TSPL_QUALITY_CHECK .QC_No =TSPL_MILK_UNLOADING.QC_No    where TSPL_MILK_UNLOADING.isPosted='1' and TSPL_MILK_UNLOADING.Gate_Entry_No not in (select Gate_Entry_No  from TSPL_Cleaning ) and TSPL_MILK_UNLOADING.Gate_Entry_No not in (select Gate_Entry_No  from TSPL_Gate_out )  " & strTankerType & " " & whrcls & ") xxx "
            'str = clsCommon.ShowSelectForm("GTENUN", qry, "GateEntryNo", whrcls, curcode, "GateEntryNo", isButtonClicked)
            str = customFinder.getFinder("TNKRFNDCLN", qry, "", "TankerNo", curcode, "GateEntryNo")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function saveData(ByVal obj As clsCleaning, ByVal trans As SqlTransaction, Optional ByVal isHistory As Boolean = False) As Boolean
        Dim issaved As Boolean = True
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
        '============Added by preeti Gupta==============
        Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select isPosted from TSPL_Cleaning Where Doc_No='" + obj.Doc_No + "'", trans))
        If Status = 1 AndAlso Not isHistory Then
            Throw New Exception("This document is already posted.")
        End If
        '===============================================
        Try
            Dim strLocation_Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_gate_entry_details.location_Code from tspl_gate_entry_details 
left outer join TSPL_Cleaning on TSPL_Cleaning.Gate_Entry_No= tspl_gate_entry_details.Gate_Entry_No 
where TSPL_Cleaning.Doc_No ='" + obj.Doc_No + "'", trans))

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmCleaning, strLocation_Code, obj.Start_Date_Time, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_No", clsCommon.myCstr(obj.Doc_No))

            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Start_Date_Time", clsCommon.GetPrintDate(obj.Start_Date_Time, "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommon.AddColumnsForChange(coll, "End_Date_Time", clsCommon.GetPrintDate(obj.End_Date_Time, "dd/MMM/yyyy hh:mm:ss tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Start_Date_Time", clsCommon.GetPrintDate(obj.Start_Date_Time, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "End_Date_Time", clsCommon.GetPrintDate(obj.End_Date_Time, "dd/MMM/yyyy"))

            End If
            clsCommon.AddColumnsForChange(coll, "IsAgainstJobWork", obj.IsAgainstJobWork)
            clsCommon.AddColumnsForChange(coll, "Joblocation_Code", obj.Joblocation_Code)
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
            If obj.InTime IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "InTime", clsCommon.GetPrintDate(obj.InTime, "dd/MMM/yyyy hh:mm:ss tt"))
            End If
            If obj.OutTime IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "OutTime", clsCommon.GetPrintDate(obj.OutTime, "dd/MMM/yyyy hh:mm:ss tt"))
            End If
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", clsCommon.myCstr(obj.comp_code))
            If obj.isNewEntry Or isHistory Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isHistory, "TSPL_Cleaning_History", "TSPL_Cleaning"), OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Cleaning", OMInsertOrUpdate.Update, "TSPL_Cleaning.Doc_no='" + obj.Doc_No + "'", trans)
            End If

            ''Notification on save
            Dim strNotifiContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmCleaning + "2" + "'", trans))
            If clsCommon.myLen(strNotifiContent) > 0 Then
                Dim FATPER As String = ""
                Dim SNFPER As String = ""
                Dim qty As String = ""
                Dim ItemDetail As String = ""

                Dim strNotifiCaption As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Caption from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmCleaning + "2" + "'", trans))
                Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmCleaning + "2" + "'", trans))

                If clsCommon.myLen(strNotifiContent) > 0 Then
                    Dim objNotification As New clsNotificationHead()
                    objNotification.Notification_Text = strNotifiContent
                    objNotification.Notification_Caption = strNotifiCaption
                    objNotification.Notification_On = strNotificationOn
                    objNotification.Notification_Text = objNotification.Notification_Text.Replace(frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(obj.Doc_No))
                    objNotification.Notification_Text = objNotification.Notification_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.myCstr(obj.Start_Date_Time))
                    objNotification.Notification_Text = objNotification.Notification_Text.Replace(frmEMailAndSMSSetting.TankerNo, clsCommon.myCstr(obj.Tanker_No))
                    objNotification.Notification_Text = objNotification.Notification_Text.Replace(frmEMailAndSMSSetting.Cleaner, clsCommon.myCstr(clsEmployeeMaster.GetName(obj.Done_by, trans)))
                    objNotification.SaveData(clsUserMgtCode.frmCleaning + "2", objNotification, trans)
                    objNotification = Nothing
                End If
            End If
            ''Notification on save

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function

    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsCleaning
        Dim obj As New clsCleaning
        Try
            Dim qst As String = " select TSPL_Cleaning.*   From TSPL_Cleaning left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No   =TSpl_Cleaning.Gate_Entry_No where 1=1  "
            Dim strwhrcls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    strwhrcls = " and Tspl_Gate_Entry_Details.location_Code  in (" & objCommonVar.strCurrUserLocations & ") "
                End If
            End If
            qst += strwhrcls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_Cleaning.Doc_no in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_Cleaning.Doc_no in (select min(TSPL_Cleaning.Doc_no ) from TSPL_Cleaning left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No   =TSpl_Cleaning.Gate_Entry_No  where TSPL_Cleaning.Doc_No  >'" + strCode + "' " & strwhrcls & "   )"
                Case NavigatorType.First
                    qst += " and TSPL_Cleaning.Doc_no in (select MIN(TSPL_Cleaning.Doc_no ) from TSPL_Cleaning left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No   =TSpl_Cleaning.Gate_Entry_No where 1=1 " & strwhrcls & "  )"
                Case NavigatorType.Last
                    qst += " and TSPL_Cleaning.Doc_no in (select Max(TSPL_Cleaning.Doc_no ) from TSPL_Cleaning left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No   =TSpl_Cleaning.Gate_Entry_No where 1=1 " & strwhrcls & "   )"
                Case NavigatorType.Previous
                    qst += " and TSPL_Cleaning.Doc_no in (select Max(TSPL_Cleaning.Doc_no ) from TSPL_Cleaning left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No   =TSpl_Cleaning.Gate_Entry_No  where TSPL_Cleaning.Doc_No  <'" + strCode + "' " & strwhrcls & "   )"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.IsAgainstJobWork = clsCommon.myCdbl(dt.Rows(0)("IsAgainstJobWork"))
                obj.Joblocation_Code = clsCommon.myCstr(dt.Rows(0)("Joblocation_Code"))
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
                If dt.Rows(0)("InTime") IsNot DBNull.Value Then
                    obj.InTime = clsCommon.myCDate(dt.Rows(0)("InTime"))
                End If
                If dt.Rows(0)("OutTime") IsNot DBNull.Value Then
                    obj.OutTime = clsCommon.myCDate(dt.Rows(0)("OutTime"))
                End If

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function isCleaningDone(ByVal strGateEntryNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        qry = "select count(*) from tspl_cleaning where gate_entry_no='" & strGateEntryNo & "' and isposted=1"
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) <= 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean ''ERO/01/02/19-000484 by balwinder on 01/02/2019
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Cleaning Doc No not found to Post")
            End If

            Dim obj As clsCleaning = clsCleaning.getData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim strLocation_Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_gate_entry_details.location_Code from tspl_gate_entry_details 
left outer join TSPL_Cleaning on TSPL_Cleaning.Gate_Entry_No= tspl_gate_entry_details.Gate_Entry_No 
where TSPL_Cleaning.Doc_No ='" + strCode + "'", trans))

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmCleaning, strLocation_Code, obj.Start_Date_Time, trans)
            If Not (obj.isPosted = 1) Then
                Throw New Exception("Transaction should be posted")
            End If
            Dim strQry As String = "select Doc_No from TSPL_Gate_Out where Gate_Entry_No='" + obj.Gate_Entry_No + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Can not reverse the document because Gateout  [" + clsCommon.myCstr(dt.Rows(0)("Doc_No")) + "] is created")
            End If


            Dim strIssueReturnNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Doc_No from TSPL_IssueReturn_HEAD where Againt_Cleaning_No='" + strCode + "'", trans))
            If clsCommon.myLen(strIssueReturnNo) > 0 Then
                clsIssueReturnHead.ReverseAndUnpost(strIssueReturnNo, trans)
                clsIssueReturnHead.DeleteData(strIssueReturnNo, trans)
            End If

            strQry = " update TSPL_cleaning set isPosted='0', Posting_Date=null where doc_no='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
