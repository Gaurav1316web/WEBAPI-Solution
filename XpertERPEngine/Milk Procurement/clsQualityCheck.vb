Imports System.Data.SqlClient
Imports common
Imports Telerik.WinControls.UI

Public Class clsQualityCheck
    Public Shift_Code As String = String.Empty
    Public Cleaning_Tester As String = String.Empty
    Public IsAgainstJobWork As Integer = 0
    Public Joblocation_Code As String = Nothing
    Public Manual_Entry As Integer = 0
    Public Arr As List(Of clsQualityChemberNoDetails) = Nothing
    Public AcknowEntryDocument_No As String = String.Empty
    Public Remarks As String = String.Empty
    Public QC_No As String = String.Empty
    Public QC_In_Date_Time As Date = Nothing
    Public QC_Out_Date_Time As Date = Nothing
    Public Gate_Entry_No As String = String.Empty
    Public Doc_Type As String = String.Empty
    Public Gate_Entry_Date_And_Time As Date = Nothing
    Public Challan_No As String = String.Empty
    Public Challan_Date As Date = Nothing
    Public Tanker_No As String = String.Empty
    Public isPosted As Integer = 0
    Public Posting_Date As Date = Nothing
    Public Dispatched_From_Mcc_Code As String = String.Empty
    Public Dispatched_From_Mcc_Desc As String = String.Empty
    Public location_Code As String = String.Empty
    Public Location_Desc As String = String.Empty
    Public Vendor_Code As String = String.Empty
    Public Vendor_Desc As String = String.Empty
    Public Item_Code As String = String.Empty
    Public Item_Desc As String = String.Empty
    Public UOM As String = String.Empty
    Public Qty_In_Kg As Double = 0
    Public snf_Per As Double = 0
    Public fat_per As Double = 0
    Public snf_KG As Double = 0
    Public fat_KG As Double = 0
    Public Dip_Value As Double = 0
    Public is_Param_accepted As Double = 0
    Public is_QC_Separated As Integer = 0
    Public Created_By As String = String.Empty
    Public Created_Date As String = String.Empty
    Public Modify_By As String = String.Empty
    Public Modify_Date As String = String.Empty
    Public comp_code As String = String.Empty
    Public Weighment_No As String = String.Empty
    Public Weighment_Date As Date = Nothing
    Public isNewEntry As Boolean = False
    Public DeductionAmount As Double = 0
    Public arrQcParam As List(Of clsQcParam) = Nothing
    Public Receipt_Control_FAT As Double = 0
    Public Receipt_Control_SNF As Double = 0
    Public Adjust_fat_per As Double = 0
    Public Adjust_snf_Per As Double = 0
    Public Adjust_clr As Double = 0
    Public Shared Function chkIsGridColumnHasTag(ByRef gv As RadGridView) As Boolean
        Try
            Dim rValue As Boolean = True
            For i As Integer = 0 To gv.Columns.Count - 1
                If gv.Columns(i).IsVisible AndAlso clsCommon.myLen(gv.Columns(i).Tag) <= 0 Then
                    rValue = False
                    Exit For
                Else
                    rValue = True
                End If
            Next
            Return rValue
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strDocNo) <= 0 Then
                Throw New Exception("Please select a QC No")
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select location_Code, QC_In_Date_Time from tspl_quality_check where QC_No='" + strDocNo + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmQualityCheck, clsCommon.myCstr(dt.Rows(0)("location_Code")), clsCommon.myCDate(dt.Rows(0)("QC_In_Date_Time")), trans)

            End If
            Dim Qry As String = "select isPosted from tspl_quality_check where Qc_no='" + strDocNo + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If
            Dim TankerFromMaster As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))
            Dim isUsed As Integer = 0
            If TankerFromMaster = 0 Then
                isUsed = clsDBFuncationality.getSingleValue(" select count(*) from tspl_milk_unloading where Qc_no='" & strDocNo & "'", trans)
                If isUsed > 0 Then
                    Throw New Exception("QC No is in use")
                End If
            Else
                isUsed = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_SECONDARY_SETTING_QC_HEAD where Qc_no='" & strDocNo & "'", trans)
                If isUsed > 0 Then
                    Throw New Exception("QC No is in use")
                End If
            End If

            Qry = "Update tspl_quality_check set isPosted = 0,Posting_Date=null where QC_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "tspl_quality_check", "QC_No", trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function postData(ByVal strQCNo As String, ByVal docType As String, ByVal formId As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            postData(strQCNo, docType, formId, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function postData(ByVal strQCNo As String, ByVal docType As String, ByVal formId As String, ByVal trans As SqlTransaction) As Boolean
        Dim isTrnasInitPostData As Boolean = False
        'If trans Is Nothing Then
        '    trans = clsDBFuncationality.GetTransactin()
        '    isTrnasInitPostData = True
        'Else
        '    isTrnasInitPostData = False
        'End If
        Dim isPosted As Boolean = True
        Try

            If (clsCommon.myLen(strQCNo) <= 0) Then
                Throw New Exception("QC No not found to Post")
            End If

            Dim obj As clsQualityCheck = clsQualityCheck.getData(strQCNo, docType, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.QC_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            ' trans = clsDBFuncationality.GetTransactin()
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmQualityCheck, obj.location_Code, obj.QC_In_Date_Time, trans)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            '--------------------
            'Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(formId, "tspl_quality_check", "QC_no", obj.QC_No, trans)
            'If isResult = False Then
            '    trans.Commit()
            '    Return False
            'End If


            Dim strQry As String = " update tspl_quality_check set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where qc_no='" & strQCNo & "'"
            clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strQCNo, "tspl_quality_check", "QC_No", trans)
            If clsCommon.myLen(clsCommon.myCstr(obj.AcknowEntryDocument_No)) <= 0 Then
                clsQualityCheck.SaveAndPostUnloadingGateOutMilkTransferIn(obj.Gate_Entry_No, trans)
            End If

            'strQry = " update tspl_weighment_detail set status=2 where gate_entry_no='" & obj.Gate_Entry_No & "'"
            'isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            ' If isTrnasInitPostData Then

            ' End If
            'If isPosted Then
            '    trans.Commit()
            'Else
            '    trans.Rollback()
            'End If
        Catch ex As Exception
            'Try
            '    trans.Rollback()
            'Catch ex1 As Exception

            'End Try
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function deleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Return deleteData(True, strDocNo, trans)
    End Function
    Public Shared Function deleteData(ByVal isCheckForPost As Boolean, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If isCheckForPost Then
                Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select isPosted from tspl_quality_check Where QC_No='" + strDocNo + "'", trans))
                If Status = 1 Then
                    Throw New Exception("This document is already posted.")
                End If
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select location_Code, QC_In_Date_Time from tspl_quality_check where QC_No='" + strDocNo + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmQualityCheck, clsCommon.myCstr(dt.Rows(0)("location_Code")), clsCommon.myCDate(dt.Rows(0)("QC_In_Date_Time")), trans)

            End If

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "tspl_quality_check", "QC_No", "TSPL_Quality_Chember_Details", "QC_No", trans)

            Dim qry2 As String = "delete from tspl_quality_check where qc_no='" & strDocNo & "'"
            Dim qry1 As String = "delete from TSPL_QC_Parameter_Detail where qc_no='" & strDocNo & "'"
            Dim qry3 As String = "delete from TSPL_Quality_Chember_Details where qc_no='" & strDocNo & "'"

            Dim isDeleted As Boolean = True
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry1, trans)
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry3, trans)
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry2, trans)

            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "" '" select tspl_quality_check.QC_No as Code ,convert(varchar,tspl_quality_check.QC_In_Date_Time,103) as [Qc In Date] ,convert(varchar,tspl_quality_check.QC_Out_Date_Time,103) as [Qc Out Date] ,tspl_quality_check.Gate_Entry_No as [Gate Entry No] ,tspl_quality_check.Doc_Type as [Doc Type] ,convert(varchar,tspl_quality_check.Gate_Entry_Date_And_Time,103) as [Gate Entry Date] ,tspl_quality_check.Challan_No as [Challan No] ,convert(varchar,tspl_quality_check.Challan_Date,103) as [Challan Date] ,tspl_quality_check.Tanker_No as [Tanker No] ,case when isnull( tspl_quality_check.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,case when tspl_gate_entry_details.In_Return=1 then 'Yes' else 'No' end as [Milk In Return] ,convert(varchar,tspl_quality_check.Posting_Date,103) as [Posting Date] ,tspl_quality_check.Dispatched_From_Mcc_Code as [Dispatched From Mcc Code] ,tspl_quality_check.Dispatched_From_Mcc_Desc as [Dispatched From Mcc Desc] ,tspl_quality_check.location_Code as [Location Code] ,tspl_quality_check.Location_Desc as [Location Desc] ,tspl_quality_check.Vendor_Code as [Vendor Code] ,tspl_quality_check.Vendor_Desc as [Vendor Desc] ,tspl_quality_check.Item_Code as [Item Code] ,tspl_quality_check.Item_Desc as [Item Desc] ,tspl_quality_check.Qty_In_Kg as [Qty In Kg] ,tspl_quality_check.snf_Per as [SNF %] ,tspl_quality_check.fat_per as [FAT %] ,tspl_quality_check.Dip_Value as [Dip Value] ,tspl_quality_check.Created_By as [Created By] ,cast(convert(date,tspl_quality_check.Created_Date,103) as varchar) as [Created Date] ,tspl_quality_check.Modify_By as [Modify By] ,cast(convert(date,tspl_quality_check.Modify_Date,103) as varchar) as [Modify Date] ,tspl_quality_check.comp_code as [Company Code] ,tspl_quality_check.Weighment_No as [Weighment No] ,convert(varchar,tspl_quality_check.Weighment_Date,103) as [Weighment Date],ISNULL(tspl_milk_unloading.unloading_no,'') as [Unloading No] , case when ISNULL(tspl_milk_unloading.isPosted ,0)=0 then 'Not Done' else 'Done' end as [Unloading Posting Status],case when  ISNULL(tspl_quality_check.is_Param_Accepted ,0)=0 and  ISNULL(tspl_quality_check.isPosted,0)=0 and  ISNULL(TSPL_QUALITY_CHECK.QC_No ,'')='' then 'QC Status Not Found' when  ISNULL(tspl_quality_check.is_Param_Accepted ,0)=1 and  ISNULL(tspl_quality_check.isPosted,0)=1 then 'QC Accepted' when  ISNULL(tspl_quality_check.is_Param_Accepted ,0)=0 and  ISNULL(tspl_quality_check.isPosted,0)=1 then 'QC Rejected' when  ISNULL(tspl_quality_check.is_Param_Accepted ,0)=2 and  ISNULL(tspl_quality_check.isPosted,0)=1 then 'Accepted With Special Approval' else 'QC Pending' end as [QC Status]  From tspl_quality_check left outer join TSPL_MILK_UNLOADING on TSPL_MILK_UNLOADING.Gate_Entry_No=TSPL_QUALITY_CHECK.Gate_Entry_No left outer join Tspl_Gate_Entry_Details on tspl_quality_check.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No "
            Dim AllowGenerateReferenceNoForBulkGateEntry As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowGenerateReferenceNoForBulkGateEntry, clsFixedParameterCode.AllowGenerateReferenceNoForBulkGateEntry, Nothing)) = 0, False, True)
            If AllowGenerateReferenceNoForBulkGateEntry = True Then
                qry = " select tspl_quality_check.QC_No as Code ,convert(varchar,tspl_quality_check.QC_In_Date_Time,103) as [Qc In Date] ,tspl_quality_check.Doc_Type as [Doc Type],Tspl_Gate_Entry_Details.Reference_No as [Reference No],case when isnull( tspl_quality_check.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted], case when  ISNULL(tspl_quality_check.is_Param_Accepted ,0)=0 and  ISNULL(tspl_quality_check.isPosted,0)=0 and  ISNULL(TSPL_QUALITY_CHECK.QC_No ,'')='' then 'QC Status Not Found' when  ISNULL(tspl_quality_check.is_Param_Accepted ,0)=1 and  ISNULL(tspl_quality_check.isPosted,0)=1 then 'QC Accepted' when  ISNULL(tspl_quality_check.is_Param_Accepted ,0)=0 and  ISNULL(tspl_quality_check.isPosted,0)=1 then 'QC Rejected' when  ISNULL(tspl_quality_check.is_Param_Accepted ,0)=2 and  ISNULL(tspl_quality_check.isPosted,0)=1 then 'Accepted With Special Approval' else 'QC Pending' end as [QC Status]  From tspl_quality_check left outer join TSPL_MILK_UNLOADING on TSPL_MILK_UNLOADING.Gate_Entry_No=TSPL_QUALITY_CHECK.Gate_Entry_No left outer join Tspl_Gate_Entry_Details on tspl_quality_check.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No "
            Else
                qry = " select tspl_quality_check.QC_No as Code ,convert(varchar,tspl_quality_check.QC_In_Date_Time,103) as [Qc In Date] ,convert(varchar,tspl_quality_check.QC_Out_Date_Time,103) as [Qc Out Date] ,tspl_quality_check.Gate_Entry_No as [Gate Entry No] ,tspl_quality_check.Doc_Type as [Doc Type] ,convert(varchar,tspl_quality_check.Gate_Entry_Date_And_Time,103) as [Gate Entry Date] ,tspl_quality_check.Challan_No as [Challan No] ,convert(varchar,tspl_quality_check.Challan_Date,103) as [Challan Date] ,tspl_quality_check.Tanker_No as [Tanker No] ,case when isnull( tspl_quality_check.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,case when tspl_gate_entry_details.In_Return=1 then 'Yes' else 'No' end as [Milk In Return] ,convert(varchar,tspl_quality_check.Posting_Date,103) as [Posting Date] ,tspl_quality_check.Dispatched_From_Mcc_Code as [Dispatched From Mcc Code] ,tspl_quality_check.Dispatched_From_Mcc_Desc as [Dispatched From Mcc Desc] ,tspl_quality_check.location_Code as [Location Code] ,tspl_quality_check.Location_Desc as [Location Desc] ,tspl_quality_check.Vendor_Code as [Vendor Code] ,tspl_quality_check.Vendor_Desc as [Vendor Desc] ,tspl_quality_check.Item_Code as [Item Code] ,tspl_quality_check.Item_Desc as [Item Desc] ,tspl_quality_check.Qty_In_Kg as [Qty In Kg] ,tspl_quality_check.snf_Per as [SNF %] ,tspl_quality_check.fat_per as [FAT %] ,tspl_quality_check.Dip_Value as [Dip Value] ,tspl_quality_check.Created_By as [Created By] ,cast(convert(date,tspl_quality_check.Created_Date,103) as varchar) as [Created Date] ,tspl_quality_check.Modify_By as [Modify By] ,cast(convert(date,tspl_quality_check.Modify_Date,103) as varchar) as [Modify Date] ,tspl_quality_check.comp_code as [Company Code] ,tspl_quality_check.Weighment_No as [Weighment No] ,convert(varchar,tspl_quality_check.Weighment_Date,103) as [Weighment Date],ISNULL(tspl_milk_unloading.unloading_no,'') as [Unloading No] , case when ISNULL(tspl_milk_unloading.isPosted ,0)=0 then 'Not Done' else 'Done' end as [Unloading Posting Status],case when  ISNULL(tspl_quality_check.is_Param_Accepted ,0)=0 and  ISNULL(tspl_quality_check.isPosted,0)=0 and  ISNULL(TSPL_QUALITY_CHECK.QC_No ,'')='' then 'QC Status Not Found' when  ISNULL(tspl_quality_check.is_Param_Accepted ,0)=1 and  ISNULL(tspl_quality_check.isPosted,0)=1 then 'QC Accepted' when  ISNULL(tspl_quality_check.is_Param_Accepted ,0)=0 and  ISNULL(tspl_quality_check.isPosted,0)=1 then 'QC Rejected' when  ISNULL(tspl_quality_check.is_Param_Accepted ,0)=2 and  ISNULL(tspl_quality_check.isPosted,0)=1 then 'Accepted With Special Approval' else 'QC Pending' end as [QC Status]  From tspl_quality_check left outer join TSPL_MILK_UNLOADING on TSPL_MILK_UNLOADING.Gate_Entry_No=TSPL_QUALITY_CHECK.Gate_Entry_No left outer join Tspl_Gate_Entry_Details on tspl_quality_check.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No "
            End If
            str = clsCommon.ShowSelectForm("QCFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked, "tspl_quality_check.QC_In_Date_Time")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function getGateEntryFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "select TSPL_gate_entry_details.Tanker_No as [TankerNo],TSPL_gate_entry_details.Gate_Entry_No as [Gate Entry No] ,TSPL_gate_entry_details.Doc_Type as [Doc Type] ,TSPL_gate_entry_details.Date_And_Time as [ Gate Entry Date And Time],  TSPL_Weighment_Detail.Weighment_No as [Weighment No],TSPL_Weighment_Detail.Weighment_date  as [Weighment Date]  ,TSPL_gate_entry_details.Challan_No as [Challan No] ,TSPL_gate_entry_details.Challan_Date as [Challan Date]  ,TSPL_gate_entry_details.isPosted as [Is Posted] ,TSPL_gate_entry_details.Posting_Date as [Posting Date] ,TSPL_gate_entry_details.Dispatched_From_Mcc as [Dispatched From Mcc] ,TSPL_gate_entry_details.location_Code as [Location Code] ,TSPL_gate_entry_details.Location_Desc as [Location Desc] ,TSPL_gate_entry_details.Vendor_Code as [Vendor Code] ,TSPL_gate_entry_details.Vendor_Desc as [Vendor Desc] ,TSPL_gate_entry_details.Item_Code as [Item Code] ,TSPL_gate_entry_details.Item_Desc as [Item Desc] ,TSPL_gate_entry_details.Qty_In_Kg as [Qty In Kg] ,TSPL_gate_entry_details.snf_Per as [SNF %] ,TSPL_gate_entry_details.fat_per as [FAT %] ,TSPL_gate_entry_details.Created_By as [Created By] ,TSPL_gate_entry_details.Created_Date as [Created Date] ,TSPL_gate_entry_details.Modify_By as [Modify By] ,TSPL_gate_entry_details.Modify_Date as [Modify Date] ,TSPL_gate_entry_details.comp_code as [Company Code]  From TSPL_gate_entry_details left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No    "
            str = clsCommon.ShowSelectForm("GTENTR", qry, "TankerNo", whrcls, curcode, "TankerNo", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function getGateEntryFinder(ByVal curcode As String, ByVal whrCls As String) As DataRow
        Dim dt As DataRow = Nothing
        Try
            Dim qry As String = "select TSPL_gate_entry_details.Tanker_No as [TankerNo],TSPL_gate_entry_details.Gate_Entry_No as [Gate Entry No] ,TSPL_gate_entry_details.Doc_Type as [Doc Type] ,TSPL_gate_entry_details.Date_And_Time as [ Gate Entry Date And Time],  TSPL_Weighment_Detail.Weighment_No as [Weighment No],TSPL_Weighment_Detail.Weighment_date  as [Weighment Date]  ,TSPL_gate_entry_details.Challan_No as [Challan No] ,TSPL_gate_entry_details.Challan_Date as [Challan Date]  ,TSPL_gate_entry_details.isPosted as [Is Posted] ,TSPL_gate_entry_details.Posting_Date as [Posting Date] ,TSPL_gate_entry_details.Dispatched_From_Mcc as [Dispatched From Mcc] ,TSPL_gate_entry_details.location_Code as [Location Code] ,TSPL_gate_entry_details.Location_Desc as [Location Desc] ,TSPL_gate_entry_details.Vendor_Code as [Vendor Code] ,TSPL_gate_entry_details.Vendor_Desc as [Vendor Desc] ,TSPL_gate_entry_details.Item_Code as [Item Code] ,TSPL_gate_entry_details.Item_Desc as [Item Desc] ,TSPL_gate_entry_details.Qty_In_Kg as [Qty In Kg] ,TSPL_gate_entry_details.snf_Per as [SNF %] ,TSPL_gate_entry_details.fat_per as [FAT %] ,TSPL_gate_entry_details.Created_By as [Created By] ,TSPL_gate_entry_details.Created_Date as [Created Date] ,TSPL_gate_entry_details.Modify_By as [Modify By] ,TSPL_gate_entry_details.Modify_Date as [Modify Date] ,TSPL_gate_entry_details.comp_code as [Company Code]  From TSPL_gate_entry_details left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No  where 1=1  "
            If clsCommon.myLen(whrCls) > 0 Then
                qry = qry & " and " & whrCls
            End If
            dt = clsCommon.ShowSelectFormForRow("GTENTR", qry, "TankerNo", curcode)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return dt
    End Function

    Public Shared Function getRefrenceNoFinder(ByVal curcode As String, ByVal whrCls As String) As DataRow
        Dim dt As DataRow = Nothing
        Try
            Dim qry As String = "select TSPL_gate_entry_details.Reference_No as [ReferenceNo]  From TSPL_gate_entry_details left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No  where 1=1  "
            If clsCommon.myLen(whrCls) > 0 Then
                qry = qry & " and " & whrCls
            End If
            dt = clsCommon.ShowSelectFormForRow("GTENTR_REF", qry, "ReferenceNo", curcode)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return dt
    End Function

    Public Shared Function saveData(ByVal obj As clsQualityCheck, ByVal trans As SqlTransaction, Optional ByVal isHistory As Boolean = False) As Boolean
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmQualityCheck, obj.location_Code, obj.QC_In_Date_Time, trans)
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            '============Added by preeti Gupta==============
            Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select isPosted from tspl_quality_check Where QC_No='" + obj.QC_No + "'", trans))

            If Status = 1 AndAlso Not isHistory Then
                Throw New Exception("This document is already posted.")
            End If
            If Not obj.isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.QC_No, "tspl_quality_check", "QC_No", "TSPL_Quality_Chember_Details", "QC_No", trans)
            End If
            '===============================================
            Dim issaved As Boolean = True
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "QC_No", clsCommon.myCstr(obj.QC_No))
            clsCommon.AddColumnsForChange(coll, "Doc_Type", clsCommon.myCstr(obj.Doc_Type))
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", clsCommon.myCstr(obj.Gate_Entry_No))
            clsCommon.AddColumnsForChange(coll, "AcknowEntryDocument_No", obj.AcknowEntryDocument_No, True)
            clsCommon.AddColumnsForChange(coll, "Manual_Entry", obj.Manual_Entry, True)
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "QC_In_Date_Time", clsCommon.GetPrintDate(obj.QC_In_Date_Time, "dd/MMM/yyyy hh:mm:ss tt"), True)
                clsCommon.AddColumnsForChange(coll, "QC_Out_Date_Time", clsCommon.GetPrintDate(obj.QC_Out_Date_Time, "dd/MMM/yyyy hh:mm:ss tt"), True)
                clsCommon.AddColumnsForChange(coll, "Gate_Entry_Date_And_Time", clsCommon.GetPrintDate(obj.Gate_Entry_Date_And_Time, "dd/MMM/yyyy hh:mm:ss tt"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "QC_In_Date_Time", clsCommon.GetPrintDate(obj.QC_In_Date_Time, "dd/MMM/yyyy"), True)
                clsCommon.AddColumnsForChange(coll, "QC_Out_Date_Time", clsCommon.GetPrintDate(obj.QC_Out_Date_Time, "dd/MMM/yyyy"), True)
                clsCommon.AddColumnsForChange(coll, "Gate_Entry_Date_And_Time", clsCommon.GetPrintDate(obj.Gate_Entry_Date_And_Time, "dd/MMM/yyyy"), True)
            End If



            clsCommon.AddColumnsForChange(coll, "Tanker_No", clsCommon.myCstr(obj.Tanker_No))
            clsCommon.AddColumnsForChange(coll, "Challan_No", clsCommon.myCstr(obj.Challan_No))
            clsCommon.AddColumnsForChange(coll, "Challan_Date", clsCommon.GetPrintDate(obj.Challan_Date, "dd/MMM/yyyy"), True)
            clsCommon.AddColumnsForChange(coll, "Weighment_No", clsCommon.myCstr(obj.Weighment_No))
            If clsCommon.myLen(obj.Weighment_No) > 0 Then

                clsCommon.AddColumnsForChange(coll, "Weighment_Date", clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy"), True)
            End If
            clsCommon.AddColumnsForChange(coll, "isPosted", obj.isPosted)
            If obj.isPosted = 1 Then
                clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Posting_Date, "dd/MMM/yyyy"), True)
            End If
            clsCommon.AddColumnsForChange(coll, "location_Code", clsCommon.myCstr(obj.location_Code))
            clsCommon.AddColumnsForChange(coll, "Location_Desc", clsCommon.myCstr(obj.Location_Desc))
            If clsCommon.CompairString(obj.Doc_Type, "MccProc") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "Dispatched_From_Mcc_Code", clsCommon.myCstr(obj.Dispatched_From_Mcc_Code))
                clsCommon.AddColumnsForChange(coll, "Dispatched_From_Mcc_Desc", clsCommon.myCstr(obj.Dispatched_From_Mcc_Desc))
            Else
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", clsCommon.myCstr(obj.Vendor_Code))
                clsCommon.AddColumnsForChange(coll, "Vendor_Desc", clsCommon.myCstr(obj.Vendor_Desc))
            End If
            clsCommon.AddColumnsForChange(coll, "Item_Code", clsCommon.myCstr(obj.Item_Code))
            clsCommon.AddColumnsForChange(coll, "Item_Desc", clsCommon.myCstr(obj.Item_Desc))
            clsCommon.AddColumnsForChange(coll, "UOM", clsCommon.myCstr(obj.UOM))
            clsCommon.AddColumnsForChange(coll, "Qty_In_Kg", clsCommon.myCdbl(obj.Qty_In_Kg))
            clsCommon.AddColumnsForChange(coll, "snf_Per", clsCommon.myCdbl(obj.snf_Per))
            clsCommon.AddColumnsForChange(coll, "fat_per", clsCommon.myCdbl(obj.fat_per))
            clsCommon.AddColumnsForChange(coll, "snf_kg", clsCommon.myCdbl(obj.snf_KG))
            clsCommon.AddColumnsForChange(coll, "fat_kg", clsCommon.myCdbl(obj.fat_KG))
            clsCommon.AddColumnsForChange(coll, "Dip_Value", clsCommon.myCdbl(obj.Dip_Value))
            ''richa Against Ticket No.BM00000003719 on 04/09/2014
            clsCommon.AddColumnsForChange(coll, "DeductionAmount", clsCommon.myCdbl(obj.DeductionAmount))
            clsCommon.AddColumnsForChange(coll, "Receipt_Control_FAT", clsCommon.myCdbl(obj.Receipt_Control_FAT))
            clsCommon.AddColumnsForChange(coll, "Receipt_Control_SNF", clsCommon.myCdbl(obj.Receipt_Control_SNF))
            clsCommon.AddColumnsForChange(coll, "is_Param_accepted", clsCommon.myCdbl(obj.is_Param_accepted))
            clsCommon.AddColumnsForChange(coll, "is_QC_Separated", clsCommon.myCdbl(obj.is_QC_Separated))
            clsCommon.AddColumnsForChange(coll, "Manual_Entry", clsCommon.myCdbl(obj.Manual_Entry))
            ''-----------------------------
            clsCommon.AddColumnsForChange(coll, "Modify_By", clsCommon.myCstr(objCommonVar.CurrentUserCode))
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If obj.Manual_Entry = 1 Then
                clsCommon.AddColumnsForChange(coll, "Manual_By", clsCommon.myCstr(objCommonVar.CurrentUserCode), True)
                clsCommon.AddColumnsForChange(coll, "Manual_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"), True)
            End If

            clsCommon.AddColumnsForChange(coll, "Comp_Code", clsCommon.myCstr(objCommonVar.CurrentCompanyCode))
            clsCommon.AddColumnsForChange(coll, "Remarks", clsCommon.myCstr(obj.Remarks))
            '========SANJEET=========================
            clsCommon.AddColumnsForChange(coll, "Adjusted_FAT", clsCommon.myCdbl(obj.Adjust_fat_per))
            clsCommon.AddColumnsForChange(coll, "Adjusted_SNF", clsCommon.myCdbl(obj.Adjust_snf_Per))
            clsCommon.AddColumnsForChange(coll, "Adjusted_CLR", clsCommon.myCdbl(obj.Adjust_clr))
            clsCommon.AddColumnsForChange(coll, "IsAgainstJobWork", obj.IsAgainstJobWork)
            clsCommon.AddColumnsForChange(coll, "Joblocation_Code", obj.Joblocation_Code)
            clsCommon.AddColumnsForChange(coll, "Shift_Code", clsCommon.myCstr(obj.Shift_Code), True)
            clsCommon.AddColumnsForChange(coll, "Cleaning_Tester", clsCommon.myCstr(obj.Cleaning_Tester), True)
            '=================================================
            If obj.isNewEntry OrElse isHistory Then
                clsCommon.AddColumnsForChange(coll, "Created_By", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isHistory, "tspl_quality_check_History", "tspl_quality_check"), OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_quality_check", OMInsertOrUpdate.Update, "tspl_quality_check.QC_No='" + obj.QC_No + "'", trans)
            End If
            issaved = issaved AndAlso clsQcParam.saveData(obj.arrQcParam, obj.QC_No, trans, isHistory)
            issaved = issaved AndAlso clsQualityChemberNoDetails.SaveData(obj.QC_No, obj.Arr, trans)
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    'Public Shared Function isQCDone(ByVal strGateEntryNo As String, ByVal trans As SqlTransaction) As Boolean
    '    Dim qry As String = String.Empty
    '    qry = "select count(*) from tspl_Quality_check where gate_entry_no='" & strGateEntryNo & "' and isposted=1"
    '    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) <= 0 Then
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function

    Public Shared Function getData(ByVal strCode As String, ByVal docType As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsQualityCheck
        Dim obj As New clsQualityCheck
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = " select *   From tspl_quality_check   where 1=1 and doc_type='" & docType & "' "
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = "and TSPL_QUALITY_CHECK.location_code in (" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and tspl_quality_check.QC_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and tspl_quality_check.QC_No in (select min(QC_No ) from tspl_quality_check where QC_No  >'" + strCode + "' and doc_type='" & docType & "'  " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and tspl_quality_check.QC_No in (select MIN(QC_No ) from tspl_quality_check where doc_type='" & docType & "'  " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and tspl_quality_check.QC_No in (select Max(QC_No ) from tspl_quality_check where doc_type='" & docType & "'  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and tspl_quality_check.QC_No in (select Max(QC_No ) from tspl_quality_check where QC_No  <'" + strCode + "' and doc_type='" & docType & "'  " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.QC_No = clsCommon.myCstr(dt.Rows(0)("QC_No"))
                obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
                obj.AcknowEntryDocument_No = clsCommon.myCstr(dt.Rows(0)("AcknowEntryDocument_No"))
                obj.QC_In_Date_Time = clsCommon.myCstr(dt.Rows(0)("QC_In_Date_Time"))
                obj.QC_Out_Date_Time = clsCommon.myCstr(dt.Rows(0)("QC_Out_Date_Time"))
                obj.Gate_Entry_Date_And_Time = clsCommon.myCDate(dt.Rows(0)("Gate_Entry_Date_And_Time"))
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
                obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"))
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If
                obj.location_Code = clsCommon.myCstr(dt.Rows(0)("location_Code"))
                obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
                If clsCommon.CompairString(obj.Doc_Type, "MccProc") = CompairStringResult.Equal Then
                    obj.Dispatched_From_Mcc_Code = clsCommon.myCstr(dt.Rows(0)("Dispatched_From_Mcc_Code"))
                    obj.Dispatched_From_Mcc_Desc = clsCommon.myCstr(dt.Rows(0)("Dispatched_From_Mcc_Desc"))
                Else
                    obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                    obj.Vendor_Desc = clsCommon.myCstr(dt.Rows(0)("Vendor_Desc"))
                End If
                obj.is_Param_accepted = clsCommon.myCdbl(dt.Rows(0)("is_Param_accepted"))
                obj.is_QC_Separated = clsCommon.myCdbl(dt.Rows(0)("is_QC_Separated"))
                obj.Weighment_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select weighment_no from tspl_weighment_Detail where gate_entry_no='" & obj.Gate_Entry_No & "'", trans))
                If clsCommon.myLen(obj.Weighment_No) > 0 Then
                    obj.Weighment_Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select weighment_date from tspl_weighment_Detail where gate_entry_no='" & obj.Gate_Entry_No & "'", trans))
                End If
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
                obj.Qty_In_Kg = clsCommon.myCdbl(dt.Rows(0)("Qty_In_Kg"))
                obj.snf_Per = clsCommon.myCdbl(dt.Rows(0)("snf_Per"))
                obj.fat_per = clsCommon.myCdbl(dt.Rows(0)("fat_per"))
                obj.snf_KG = clsCommon.myCdbl(dt.Rows(0)("snf_kg"))
                obj.fat_KG = clsCommon.myCdbl(dt.Rows(0)("fat_kg"))
                obj.Dip_Value = clsCommon.myCdbl(dt.Rows(0)("Dip_Value"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                ''richa Against Ticket No.BM00000003719 on 04/09/2014
                obj.DeductionAmount = clsCommon.myCdbl(dt.Rows(0)("DeductionAmount"))
                obj.Receipt_Control_FAT = clsCommon.myCdbl(dt.Rows(0)("Receipt_Control_FAT"))
                obj.Receipt_Control_SNF = clsCommon.myCdbl(dt.Rows(0)("Receipt_Control_SNF"))
                ''-----------------------------
                '=============SANJEET============
                obj.Adjust_fat_per = clsCommon.myCdbl(dt.Rows(0)("Adjusted_FAT"))
                obj.Adjust_snf_Per = clsCommon.myCdbl(dt.Rows(0)("Adjusted_SNF"))
                obj.Adjust_clr = clsCommon.myCdbl(dt.Rows(0)("Adjusted_CLR"))
                '==============================
                obj.IsAgainstJobWork = clsCommon.myCdbl(dt.Rows(0)("IsAgainstJobWork"))
                obj.Joblocation_Code = clsCommon.myCstr(dt.Rows(0)("Joblocation_Code"))
                obj.Shift_Code = clsCommon.myCstr(dt.Rows(0)("Shift_Code"))
                obj.Cleaning_Tester = clsCommon.myCstr(dt.Rows(0)("Cleaning_Tester"))
                obj.arrQcParam = clsQcParam.getData(obj.QC_No, trans)
                obj.Arr = clsQualityChemberNoDetails.GetData(obj.QC_No, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsQualityCheck
        Dim obj As New clsQualityCheck
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = " select *   From tspl_quality_check   where 1=1 "

            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = "and TSPL_QUALITY_CHECK.location_code in (" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and tspl_quality_check.QC_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and tspl_quality_check.QC_No in (select min(QC_No ) from tspl_quality_check where QC_No  >'" + strCode & "'" & whrCls & ")"
                Case NavigatorType.First
                    qst += " and tspl_quality_check.QC_No in (select MIN(QC_No ) from tspl_quality_check where 1=1 " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and tspl_quality_check.QC_No in (select Max(QC_No ) from tspl_quality_check where 1=1  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and tspl_quality_check.QC_No in (select Max(QC_No ) from tspl_quality_check where QC_No  <'" + strCode + "'   " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.QC_No = clsCommon.myCstr(dt.Rows(0)("QC_No"))
                obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
                obj.QC_In_Date_Time = clsCommon.myCstr(dt.Rows(0)("QC_In_Date_Time"))
                obj.QC_Out_Date_Time = clsCommon.myCstr(dt.Rows(0)("QC_Out_Date_Time"))
                obj.AcknowEntryDocument_No = clsCommon.myCstr(dt.Rows(0)("AcknowEntryDocument_No"))
                obj.Gate_Entry_Date_And_Time = clsCommon.myCDate(dt.Rows(0)("Gate_Entry_Date_And_Time"))
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
                obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"))
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If
                obj.location_Code = clsCommon.myCstr(dt.Rows(0)("location_Code"))
                obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
                If clsCommon.CompairString(obj.Doc_Type, "MccProc") = CompairStringResult.Equal Then
                    obj.Dispatched_From_Mcc_Code = clsCommon.myCstr(dt.Rows(0)("Dispatched_From_Mcc_Code"))
                    obj.Dispatched_From_Mcc_Desc = clsCommon.myCstr(dt.Rows(0)("Dispatched_From_Mcc_Desc"))
                Else
                    obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                    obj.Vendor_Desc = clsCommon.myCstr(dt.Rows(0)("Vendor_Desc"))
                End If
                obj.is_Param_accepted = clsCommon.myCdbl(dt.Rows(0)("is_Param_accepted"))
                obj.is_QC_Separated = clsCommon.myCdbl(dt.Rows(0)("is_QC_Separated"))
                obj.Weighment_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select weighment_no from tspl_weighment_Detail where gate_entry_no='" & obj.Gate_Entry_No & "'", trans))
                If clsCommon.myLen(obj.Weighment_No) > 0 Then
                    obj.Weighment_Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select weighment_date from tspl_weighment_Detail where gate_entry_no='" & obj.Gate_Entry_No & "'", trans))
                End If
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
                obj.Qty_In_Kg = clsCommon.myCdbl(dt.Rows(0)("Qty_In_Kg"))
                obj.snf_Per = clsCommon.myCdbl(dt.Rows(0)("snf_Per"))
                obj.fat_per = clsCommon.myCdbl(dt.Rows(0)("fat_per"))
                obj.snf_KG = clsCommon.myCdbl(dt.Rows(0)("snf_kg"))
                obj.fat_KG = clsCommon.myCdbl(dt.Rows(0)("fat_kg"))
                obj.Dip_Value = clsCommon.myCdbl(dt.Rows(0)("Dip_Value"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                ''richa Against Ticket No.BM00000003719 on 04/09/2014
                obj.DeductionAmount = clsCommon.myCdbl(dt.Rows(0)("DeductionAmount"))
                obj.Receipt_Control_FAT = clsCommon.myCdbl(dt.Rows(0)("Receipt_Control_FAT"))
                obj.Receipt_Control_SNF = clsCommon.myCdbl(dt.Rows(0)("Receipt_Control_SNF"))
                ''-----------------------------

                '=============SANJEET============
                obj.Adjust_fat_per = clsCommon.myCdbl(dt.Rows(0)("Adjusted_FAT"))
                obj.Adjust_snf_Per = clsCommon.myCdbl(dt.Rows(0)("Adjusted_SNF"))
                obj.Adjust_clr = clsCommon.myCdbl(dt.Rows(0)("Adjusted_CLR"))
                '==============================
                obj.IsAgainstJobWork = clsCommon.myCdbl(dt.Rows(0)("IsAgainstJobWork"))
                obj.Joblocation_Code = clsCommon.myCstr(dt.Rows(0)("Joblocation_Code"))
                obj.Shift_Code = clsCommon.myCstr(dt.Rows(0)("Shift_Code"))
                obj.Cleaning_Tester = clsCommon.myCstr(dt.Rows(0)("Cleaning_Tester"))
                obj.arrQcParam = clsQcParam.getData(obj.QC_No, trans)
                obj.Arr = clsQualityChemberNoDetails.GetData(obj.QC_No, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function isControlSample(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim rValue As Boolean = False
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select case when isnull(Control_Sample,'')='NO' then 0 else 1 end as ControlSample  from TSPL_MCC_Dispatch_Challan left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Challan_No=TSPL_MCC_Dispatch_Challan.Chalan_NO where Tspl_Gate_Entry_Details.Gate_Entry_No='" & GateEntryNo & "'  ", trans)) = 1 Then
            rValue = True
        Else
            rValue = False
        End If
        Return rValue
    End Function
    Public Shared Function isMccInDoc(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim rValue As Boolean = False
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select case when ISNULL(Doc_Type,'')='MccProc' then 1 else 0 end as DocType   from Tspl_Gate_Entry_Details where Tspl_Gate_Entry_Details.Gate_Entry_No='" & GateEntryNo & "'  ", trans)) = 1 Then
            rValue = True
        Else
            rValue = False
        End If
        Return rValue
    End Function
    Public Shared Function isWeighmentDone(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim rValue As Boolean = False
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select case when isnull(Weighment_No,'')='' then 0 else 1 end as WeighmentNo   from TSPL_Weighment_Detail  where Gate_Entry_No='" & GateEntryNo & "' and isPosted=1", trans)) = 1 Then
            rValue = True
        Else
            rValue = False
        End If
        Return rValue
    End Function
    Public Shared Function isVirtualSiloFound(ByVal LocCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim rValue As Boolean = False
        If clsCommon.myLen(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Location_Code   from TSPL_LOCATION_MASTER  where Is_Sub_Location='Y' and Main_Location_Code='" & LocCode & "' and Location_Type='Virtual'", trans))) > 0 Then
            rValue = True
        Else
            rValue = False
        End If
        Return rValue
    End Function
    Public Shared Function getPhysicalSilo(ByVal LocCode As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim rValue As String = ""
        rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select top 1 Location_Code   from TSPL_LOCATION_MASTER  where Is_Sub_Location='Y' and Main_Location_Code='" & LocCode & "' and Location_Type='Physical'", trans))
        Return rValue
    End Function
    Public Shared Function getVirtualSilo(ByVal LocCode As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim rValue As String = ""
        rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select top 1 Location_Code   from TSPL_LOCATION_MASTER  where Is_Sub_Location='Y' and Main_Location_Code='" & LocCode & "' and Location_Type='Virtual'", trans))
        Return rValue
    End Function
    Public Shared Function getChallanNo(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim rValue As String = ""
        rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select isnull(Challan_No,'') as ChallanNo  from Tspl_Gate_Entry_Details where Gate_Entry_No ='" & GateEntryNo & "' ", trans))
        Return rValue
    End Function
    Public Shared Function getWeighmentNo(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim FirstGateOutProcessForMCCBulkProcument As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FirstGateOutProcessForMCCBulkProcument, clsFixedParameterCode.FirstGateOutProcessForMCCBulkProcument, trans))
        Dim rValue As String = ""
        Dim strWhr As String = ""
        If FirstGateOutProcessForMCCBulkProcument = 0 Then
            strWhr = " and isPosted=1 "
        End If
        rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  isnull(Weighment_No,'') as WeighmentNo   from TSPL_Weighment_Detail  where Gate_Entry_No='" & GateEntryNo & "' " & strWhr & "", trans))
        Return rValue
    End Function
    Public Shared Function getWeighmentDate(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As DateTime
        Dim FirstGateOutProcessForMCCBulkProcument As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FirstGateOutProcessForMCCBulkProcument, clsFixedParameterCode.FirstGateOutProcessForMCCBulkProcument, trans))
        Dim strWhr As String = ""
        If FirstGateOutProcessForMCCBulkProcument = 0 Then
            strWhr = " and isPosted=1 "
        End If
        Dim rValue As DateTime = clsCommon.myCDate(clsDBFuncationality.getSingleValue(" select  Weighment_date   from TSPL_Weighment_Detail  where Gate_Entry_No='" & GateEntryNo & "' " & strWhr & " ", trans))
        Return rValue
    End Function
    Public Shared Function isQCDone(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim rValue As Boolean = False
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select case when isnull(QC_No ,'')='' then 0 else 1 end as QCNO   from TSPL_QUALITY_CHECK  where Gate_Entry_No='" & GateEntryNo & "' and isPosted=1", trans)) = 1 Then
            rValue = True
        Else
            rValue = False
        End If
        Return rValue
    End Function
    Public Shared Function getQCNo(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim rValue As String = ""
        rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  isnull(QC_No ,'') as QCNO   from TSPL_QUALITY_CHECK where Gate_Entry_No='" & GateEntryNo & "' and isPosted=1 ", trans))
        Return rValue
    End Function
    Public Shared Function ControlSampleFAT(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Double
        Dim rValue As Double = 0
        rValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select  isnull(control_sample_fat ,0) ControlSample  from TSPL_MCC_Dispatch_Challan left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Challan_No=TSPL_MCC_Dispatch_Challan.Chalan_NO where Tspl_Gate_Entry_Details.Gate_Entry_No='" & GateEntryNo & "'  ", trans))
        Return rValue
    End Function
    Public Shared Function ControlSampleSNF(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Double
        Dim rValue As Double = 0
        rValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select  isnull(control_sample_snf  ,0) ControlSample  from TSPL_MCC_Dispatch_Challan left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Challan_No=TSPL_MCC_Dispatch_Challan.Chalan_NO where Tspl_Gate_Entry_Details.Gate_Entry_No='" & GateEntryNo & "'  ", trans))
        Return rValue
    End Function
    Public Shared Function isIntermittentDoc(ByVal challanNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim rValue As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("  select isintermittent from TSPL_MCC_Dispatch_Challan where Chalan_NO ='" & challanNo & "' ", trans))
        If rValue = 1 Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Shared Function SaveAndPostUnloadingData(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean = False
        Try
            Dim obj As clsUnloading = New clsUnloading()
            Dim objGt As clsGateEntry = clsGateEntry.getData(GateEntryNo, NavigatorType.Current, trans)
            Dim Unloading_Doc As String = clsUnloading.GetUnloadingDocNoFromGateEntry(GateEntryNo, trans)
            If clsCommon.myLen(Unloading_Doc) > 0 Then
                obj.isNewEntry = False
                obj = clsUnloading.getData(Unloading_Doc, NavigatorType.Current, trans)
                If obj.isPosted = 1 Then
                    Return True
                End If
            Else
                obj.isNewEntry = True
            End If

            ''  Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            Dim dt As Date = clsCommon.GetPrintDate(clsQualityCheck.getWeighmentDate(GateEntryNo, trans), "dd/MMM/yyyy hh:mm:ss tt")
            If obj.isNewEntry Then
                obj.Unloading_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Unloading, clsDocTransactionType.NA, objGt.location_Code)
                If clsCommon.myLen(obj.Unloading_No) <= 0 Then
                    Throw New Exception("Error In Unloading  No Generation")
                End If
            End If
            obj.Gate_Entry_No = clsCommon.myCstr(objGt.Gate_Entry_No)
            obj.Unloading_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Tanker_No = clsCommon.myCstr(objGt.Tanker_No)
            obj.Weighment_No = clsQualityCheck.getWeighmentNo(GateEntryNo, trans)
            obj.QC_No = clsQualityCheck.getQCNo(GateEntryNo, trans)
            obj.location_Code = objGt.location_Code
            obj.AcknowEntryDocument_No = clsCommon.myCstr(objGt.AcknowEntryDocument_No)
            If clsCommon.myLen(clsCommon.myCstr(obj.AcknowEntryDocument_No)) > 0 Then
                If clsERPFuncationality.isLocationMcc(objGt.location_Code, trans) Then
                    obj.Sub_location_Code = clsQualityCheck.getVirtualSilo(objGt.location_Code, trans)
                Else
                    obj.Sub_location_Code = clsQualityCheck.getPhysicalSilo(objGt.location_Code, trans)
                End If

            Else
                obj.Sub_location_Code = clsQualityCheck.getVirtualSilo(objGt.location_Code, trans)
            End If

            obj.Item_Code = objGt.Item_Code
            obj.Item_Desc = clsCommon.myCstr(objGt.Item_Desc)
            obj.UOM = clsCommon.myCstr(objGt.UOM)
            obj.Qty = 0
            obj.fat_per = 0
            obj.snf_Per = 0
            obj.SNF_KG = 0
            obj.fat_KG = 0
            obj.isPosted = 0
            obj.Modify_By = objCommonVar.CurrentUserCode
            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            obj.comp_code = objCommonVar.CurrentCompanyCode
            obj.Created_By = objCommonVar.CurrentUserCode
            obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            obj.Arr = New List(Of clsUnloadingChemberNoDetails)
            ''richa agarwal 31 Jan,2020
            Dim objTr As clsUnloadingChemberNoDetails = New clsUnloadingChemberNoDetails()
            If objGt.Arr IsNot Nothing AndAlso objGt.Arr.Count > 0 Then
                For Each objTrDD As clsGateEntryChemberNoDetails In objGt.Arr
                    objTr = New clsUnloadingChemberNoDetails()

                    objTr.Line_No = objTrDD.Line_No
                    objTr.Item_Code = objTrDD.Item_Code
                    objTr.UOM = objTrDD.UOM
                    objTr.Chamber_Desc = objTrDD.Chamber_Desc
                    objTr.Chamber_Qty = objTrDD.Chamber_Qty
                    objTr.snf_Per = objTrDD.snf_Per
                    objTr.fat_per = objTrDD.fat_per
                    objTr.Unloading_Sequence = objTrDD.Line_No
                    objTr.Sublocation_Code = obj.Sub_location_Code
                    objTr.UnLoading_Status = 1
                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next
            End If

            isSaved = clsUnloading.saveData(obj, trans)
            isSaved = clsUnloading.postData(obj.Unloading_No, "", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveAndPostUnloadingGateOutMilkTransferIn(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        '' change done by Panch Raj against ticket No:BM00000008730
        Dim isSaved As Boolean = True
        Try
            If clsQualityCheck.isMccInDoc(GateEntryNo, trans) Then
                If clsQualityCheck.isIntermittentDoc(clsQualityCheck.getChallanNo(GateEntryNo, trans), trans) Then
                    If clsQualityCheck.isWeighmentDone(GateEntryNo, trans) And clsQualityCheck.isQCDone(GateEntryNo, trans) And clsMccDispatch.GetReachedAtFinalLoc(GateEntryNo, trans) = 0 Then
                        Dim objGt As clsGateEntry = clsGateEntry.getData(GateEntryNo, NavigatorType.Current, trans)
                        If clsQualityCheck.isVirtualSiloFound(objGt.location_Code, trans) Then
                            isSaved = clsQualityCheck.SaveAndPostUnloadingData(GateEntryNo, trans)
                            isSaved = clsQualityCheck.SaveGateOutData(GateEntryNo, trans)
                            isSaved = clsQualityCheck.SaveAndPostTransferInData(GateEntryNo, trans)
                        Else
                            Throw New Exception(" Please Create Virtual Silo for location  " & objGt.location_Code)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveAndPostTransferInData(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean = False
        Try
            Dim obj As clsMilkTransferIn = New clsMilkTransferIn()
            Dim objGt As clsGateEntry = clsGateEntry.getData(GateEntryNo, NavigatorType.Current, trans)
            Dim MilkIn_Doc As String = clsMilkTransferIn.GetTransferInDocNoFromGateEntry(GateEntryNo, trans)
            If clsCommon.myLen(MilkIn_Doc) > 0 Then
                obj.isNewEntry = False
                obj = clsMilkTransferIn.getData(MilkIn_Doc, NavigatorType.Current, trans)
                If obj.isPosted = 1 Then
                    Return True
                End If
            Else
                obj.isNewEntry = True
            End If

            ' Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            Dim dt As Date = clsCommon.GetPrintDate(clsQualityCheck.getWeighmentDate(GateEntryNo, trans), "dd/MMM/yyyy hh:mm:ss tt")
            If obj.isNewEntry = True Then
                obj.Receipt_Challan_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.MilkTransferIn, clsDocTransactionType.NA, objGt.location_Code)
                If clsCommon.myLen(obj.Receipt_Challan_No) <= 0 Then
                    Throw New Exception("Error in Receipt Challan  No genertion")
                End If
            End If

            obj.Receipt_Challan_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Dispatch_Challan_No = clsCommon.myCstr(clsQualityCheck.getChallanNo(GateEntryNo, trans))
            obj.Weighment_No = clsQualityCheck.getWeighmentNo(GateEntryNo, trans)
            obj.Qc_No = clsQualityCheck.getQCNo(GateEntryNo, trans)
            obj.Gate_Entry_no = GateEntryNo
            obj.location_code = clsCommon.myCstr(objGt.location_Code)
            obj.AcknowEntryDocument_No = clsCommon.myCstr(objGt.AcknowEntryDocument_No)
            obj.km_reading_receipt = 0
            obj.Receipt_Control_FAT = 0
            obj.Receipt_Control_SNF = 0
            obj.Modified_By = objCommonVar.CurrentUserCode
            obj.Modified_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            obj.Comp_Code = objCommonVar.CurrentCompanyCode
            obj.Created_By = objCommonVar.CurrentUserCode
            obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            isSaved = clsMilkTransferIn.saveData(obj, trans)
            isSaved = clsMilkTransferIn.postData(obj.Receipt_Challan_No, trans)

            If clsCommon.myLen(clsCommon.myCstr(obj.AcknowEntryDocument_No)) > 0 Then
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select FAT_Rate,SNF_Rate,FAT_KG,SNF_KG,SNO,Chamber_No,Qty_KG from TSPL_ACKNOWLEDGENT_ENTRY_Detail where document_no='" & obj.AcknowEntryDocument_No & "'", trans)
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt1.Rows.Count - 1

                        Dim strQry As String = "Update TSPL_WEIGHMENT_CHEMBER_DETAILS set  " &
                     "CH_FAT_Rate='" & Math.Round(clsCommon.myCdbl(dt1.Rows(ii)("FAT_Rate")), 2) & "', " &
                     "CH_SNF_Rate='" & Math.Round(clsCommon.myCdbl(dt1.Rows(ii)("SNF_Rate")), 2) & "', " &
                     "CH_FAT_Value='" & Math.Round((clsCommon.myCdbl(dt1.Rows(ii)("FAT_KG")) * clsCommon.myCdbl(dt1.Rows(ii)("FAT_Rate"))), 2) & "', " &
                     "CH_SNF_Value='" & Math.Round((clsCommon.myCdbl(dt1.Rows(ii)("SNF_KG")) * clsCommon.myCdbl(dt1.Rows(ii)("SNF_Rate"))), 2) & "', " &
                     "CH_FAT_Kg='" & clsCommon.myCdbl(dt1.Rows(ii)("FAT_KG")) & "', " &
                     "CH_SNF_Kg='" & clsCommon.myCdbl(dt1.Rows(ii)("SNF_KG")) & "' " &
                     "where Weighment_No='" & obj.Weighment_No & "' and LINE_NO='" & clsCommon.myCdbl(dt1.Rows(ii)("SNO")) & "' and Chamber_Qty='" & clsCommon.myCdbl(dt1.Rows(ii)("Qty_KG")) & "' "

                        clsDBFuncationality.ExecuteNonQuery(strQry, trans)

                        strQry = "update TSPL_WEIGHMENT_CHEMBER_DETAILS set CH_Amount =CH_FAT_Value+CH_SNF_Value where Weighment_No='" & obj.Weighment_No & "' "
                        clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                    Next
                End If
            End If




        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function SaveGateOutData(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean = False
        Try
            Dim obj As clsGateOut = New clsGateOut()
            Dim objGt As clsGateEntry = clsGateEntry.getData(GateEntryNo, NavigatorType.Current, trans)
            obj.isNewEntry = True
            Dim strLoc As String = clsDBFuncationality.getSingleValue("select location_code from Tspl_Gate_Entry_Details  where gate_entry_no='" & GateEntryNo & "'", trans)
            '   Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            Dim dt As Date = clsCommon.GetPrintDate(clsQualityCheck.getWeighmentDate(GateEntryNo, trans), "dd/MMM/yyyy hh:mm:ss tt")
            obj.Doc_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.GateOut, clsDocTransactionType.NA, strLoc)
            If clsCommon.myLen(obj.Doc_No) <= 0 Then
                Throw New Exception("Error In Document  No Genertion")
            End If
            obj.Gate_Entry_No = clsCommon.myCstr(GateEntryNo)
            obj.AcknowEntryDocument_No = clsCommon.myCstr(objGt.AcknowEntryDocument_No)

            obj.Doc_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Tanker_No = clsCommon.myCstr(objGt.Tanker_No)
            obj.Weighment_No = clsQualityCheck.getWeighmentNo(GateEntryNo, trans)
            obj.QC_No = clsQualityCheck.getQCNo(GateEntryNo, trans)
            obj.Modify_By = objCommonVar.CurrentUserCode
            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            obj.comp_code = objCommonVar.CurrentCompanyCode
            obj.Created_By = objCommonVar.CurrentUserCode
            obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            isSaved = clsGateOut.saveData(obj, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
End Class
Public Class clsQcParam
    Public QC_No As String = String.Empty
    Public Param_Field_Code As String = String.Empty
    Public Param_Field_Desc As String = String.Empty
    Public Param_Field_Value As String = String.Empty
    Public Param_Type As String = String.Empty
    Public LINE_NO As Integer = 0
    Public Remarks As String = String.Empty
    Public BoilingDifference As Double = 0
    Public Shared Function saveData(ByVal arrObj As List(Of clsQcParam), ByVal strQCNo As String, ByVal trans As SqlTransaction, Optional ByVal isHistory As Boolean = False) As Boolean
        Dim issaved As Boolean = True
        Try

            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                If Not isHistory Then
                    Dim qry As String = "delete from TSPL_QC_Parameter_Detail where QC_No='" & strQCNo & "'"
                    issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
                For Each obj As clsQcParam In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "QC_No", obj.QC_No)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Code", obj.Param_Field_Code)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Desc", obj.Param_Field_Desc)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Value", obj.Param_Field_Value)
                    clsCommon.AddColumnsForChange(coll, "Param_Type", obj.Param_Type)
                    clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO)
                    clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                    clsCommon.AddColumnsForChange(coll, "BoilingDifference", obj.BoilingDifference)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isHistory, "TSPL_QC_Parameter_Detail_History", "TSPL_QC_Parameter_Detail"), OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function getData(ByVal strQCNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsQcParam)
        Dim arrObj As List(Of clsQcParam) = Nothing
        Try
            Dim obj As clsQcParam = Nothing
            Dim qry As String = "select * from TSPL_QC_Parameter_Detail where QC_No='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsQcParam)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsQcParam()
                    obj.QC_No = clsCommon.myCstr(dt.Rows(i)("QC_No"))
                    obj.Param_Field_Code = clsCommon.myCstr(dt.Rows(i)("Param_Field_Code"))
                    obj.Param_Field_Desc = clsCommon.myCstr(dt.Rows(i)("Param_Field_Desc"))
                    obj.Param_Field_Value = clsCommon.myCstr(dt.Rows(i)("Param_Field_Value"))
                    obj.Param_Type = clsCommon.myCstr(dt.Rows(i)("Param_Type"))
                    obj.LINE_NO = clsCommon.myCdbl(dt.Rows(i)("LINE_NO"))
                    obj.Remarks = clsCommon.myCstr(dt.Rows(i)("Remarks"))
                    obj.BoilingDifference = clsCommon.myCdbl(dt.Rows(i)("BoilingDifference"))

                    arrObj.Add(obj)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrObj
    End Function
    Public Shared Function deleteData(ByVal strQCNo As String) As Boolean
        Dim isDeleted As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_QC_Parameter_Detail where QC_No='" & strQCNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return isDeleted
    End Function
End Class
Public Class clsQCManualSealDetail
    Public Chalan_No As String = String.Empty
    Public Seal_No As String = String.Empty
    Public Status As String = String.Empty
    Public Shared Function SaveData(ByVal arr As List(Of clsQCManualSealDetail), ByVal tran As SqlTransaction) As Boolean
        'Try
        '    Dim i As Integer = 0
        '    Dim issaved As Boolean = True
        '    If arr.Count > 0 Then
        '        Dim qry As String = "delete from TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details where  chalan_No='" & arr.Item(0).Chalan_No & "'"
        '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '        For i = 0 To arr.Count - 1
        '            Dim coll As New Hashtable()
        '            clsCommon.AddColumnsForChange(coll, "Chalan_No", arr.Item(i).Chalan_No)
        '            clsCommon.AddColumnsForChange(coll, "Seal_No", arr.Item(i).Seal_No)
        '            issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details", OMInsertOrUpdate.Insert, "", tran)
        '            issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details_History", OMInsertOrUpdate.Insert, "", tran)
        '        Next
        '    End If
        '    Return issaved
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
        Return SaveData(arr, tran, False)
    End Function
    Public Shared Function SaveData(ByVal arr As List(Of clsQCManualSealDetail), ByVal tran As SqlTransaction, ByVal isReversed As Boolean) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim i As Integer = 0
            If arr.Count > 0 Then
                If Not isReversed Then
                    Dim qry As String = "delete from TSPL_QC_Manual_Seal_Details where  chalan_No='" & arr.Item(0).Chalan_No & "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                End If
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Chalan_No", arr.Item(i).Chalan_No)
                    clsCommon.AddColumnsForChange(coll, "Seal_No", arr.Item(i).Seal_No)
                    clsCommon.AddColumnsForChange(coll, "Status", arr.Item(i).Status)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isReversed, "TSPL_QC_manual_Seal_Details_History", "TSPL_QC_manual_Seal_Details"), OMInsertOrUpdate.Insert, "", tran)
                Next
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function DeleteData(ByVal challan_no As String, ByVal tran As SqlTransaction) As Boolean
        Dim isdeleted As Boolean = False
        Dim qry As String = "delete from TSPL_QC_Manual_Seal_Details where  chalan_No='" & challan_no & "'"
        isdeleted = clsDBFuncationality.ExecuteNonQuery(qry, tran)
        Return isdeleted
    End Function
    Public Shared Function getData(ByVal chalanNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsQCManualSealDetail)
        Dim arr As New List(Of clsQCManualSealDetail)
        Try
            Dim obj As New clsQCManualSealDetail
            Dim q As String = "select * from TSPL_QC_manual_Seal_Details where chalan_no='" & chalanNo & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q, trans)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsQCManualSealDetail()
                    obj.Chalan_No = clsCommon.myCstr(dtbl.Rows(i)("Chalan_No"))
                    obj.Seal_No = clsCommon.myCstr(dtbl.Rows(i)("Seal_No"))
                    obj.Status = clsCommon.myCstr(dtbl.Rows(i)("Status"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arr
    End Function

End Class

Public Class clsQCPaperSealDetail
    Public Chalan_No As String = String.Empty
    Public Seal_No As String = String.Empty
    Public Status As String = String.Empty
    Public Shared Function SaveData(ByVal arr As List(Of clsQCPaperSealDetail), ByVal tran As SqlTransaction) As Boolean
        'Try
        '    Dim i As Integer = 0
        '    Dim issaved As Boolean = True
        '    If arr.Count > 0 Then
        '        Dim qry As String = "delete from TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details where  chalan_No='" & arr.Item(0).Chalan_No & "'"
        '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '        For i = 0 To arr.Count - 1
        '            Dim coll As New Hashtable()
        '            clsCommon.AddColumnsForChange(coll, "Chalan_No", arr.Item(i).Chalan_No)
        '            clsCommon.AddColumnsForChange(coll, "Seal_No", arr.Item(i).Seal_No)
        '            issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details", OMInsertOrUpdate.Insert, "", tran)
        '            issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details_History", OMInsertOrUpdate.Insert, "", tran)
        '        Next
        '    End If
        '    Return issaved
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
        Return SaveData(arr, tran, False)
    End Function
    Public Shared Function DeleteData(ByVal challan_no As String, ByVal tran As SqlTransaction) As Boolean
        Dim isdeleted As Boolean = False
        Dim qry As String = "delete from TSPL_QC_Paper_Seal_Details where  chalan_No='" & challan_no & "'"
        isdeleted = clsDBFuncationality.ExecuteNonQuery(qry, tran)
        Return isdeleted
    End Function
    Public Shared Function SaveData(ByVal arr As List(Of clsQCPaperSealDetail), ByVal tran As SqlTransaction, ByVal isReversed As Boolean) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim i As Integer = 0

            If arr.Count > 0 Then
                If Not isReversed Then
                    Dim qry As String = "delete from TSPL_QC_Paper_Seal_Details where  chalan_No='" & arr.Item(0).Chalan_No & "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                End If

                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Chalan_No", arr.Item(i).Chalan_No)
                    clsCommon.AddColumnsForChange(coll, "Seal_No", arr.Item(i).Seal_No)
                    clsCommon.AddColumnsForChange(coll, "Status", arr.Item(i).Status)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isReversed, "TSPL_QC_Paper_Seal_Details_History", "TSPL_QC_Paper_Seal_Details"), OMInsertOrUpdate.Insert, "", tran)
                Next
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return issaved
    End Function

    Public Shared Function getData(ByVal chalanNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsQCPaperSealDetail)
        Dim arr As New List(Of clsQCPaperSealDetail)
        Try
            Dim obj As New clsQCPaperSealDetail
            Dim q As String = "select * from TSPL_QC_Paper_Seal_Details where chalan_no='" & chalanNo & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q, trans)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsQCPaperSealDetail()
                    obj.Chalan_No = clsCommon.myCstr(dtbl.Rows(i)("Chalan_No"))
                    obj.Seal_No = clsCommon.myCstr(dtbl.Rows(i)("Seal_No"))
                    obj.Status = clsCommon.myCstr(dtbl.Rows(i)("Status"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arr
    End Function

End Class

Public Class clsQualityChemberNoDetails
#Region "Variables"
    Public QC_No As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public UOM As String = Nothing
    Public Chamber_Desc As String = Nothing
    Public Chamber_Qty As Integer = 0
    Public snf_Per As Double = 0
    Public fat_per As Double = 0
    Public MIKL_TYPE_CODE As String = Nothing
    Public MILK_GRADE_CODE As String = Nothing
    Public Adjust_fat_per As Double = 0
    Public Adjust_snf_Per As Double = 0
    Public Adjust_clr As Double = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsQualityChemberNoDetails), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim sQuery As String = "Delete from TSPL_Quality_Chember_Details where QC_No='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            For Each obj As clsQualityChemberNoDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "QC_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "Chamber_Desc", obj.Chamber_Desc)
                clsCommon.AddColumnsForChange(coll, "Chamber_Qty", obj.Chamber_Qty)
                clsCommon.AddColumnsForChange(coll, "snf_Per", obj.snf_Per)
                clsCommon.AddColumnsForChange(coll, "fat_per", obj.fat_per)
                clsCommon.AddColumnsForChange(coll, "MIKL_TYPE_CODE", obj.MIKL_TYPE_CODE, True)
                clsCommon.AddColumnsForChange(coll, "MILK_GRADE_CODE", obj.MILK_GRADE_CODE, True)
                '=====================SANJEET======================
                clsCommon.AddColumnsForChange(coll, "Adjusted_FAT", clsCommon.myCdbl(obj.Adjust_fat_per))
                clsCommon.AddColumnsForChange(coll, "Adjusted_SNF", clsCommon.myCdbl(obj.Adjust_snf_Per))
                clsCommon.AddColumnsForChange(coll, "Adjusted_CLR", clsCommon.myCdbl(obj.Adjust_clr))
                '=======================================================
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Quality_Chember_Details", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsQualityChemberNoDetails)
        Dim arr As List(Of clsQualityChemberNoDetails) = Nothing
        Dim qry As String
        qry = "select * from " & _
            "TSPL_Quality_Chember_Details where TSPL_Quality_Chember_Details.QC_No='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsQualityChemberNoDetails)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsQualityChemberNoDetails = New clsQualityChemberNoDetails()
                obj.QC_No = clsCommon.myCstr(dr("QC_No"))
                obj.Line_No = clsCommon.myCstr(dr("Line_No"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.UOM = clsCommon.myCstr(dr("UOM"))
                obj.snf_Per = clsCommon.myCdbl(dr("snf_Per"))
                obj.fat_per = clsCommon.myCdbl(dr("fat_per"))
                obj.Chamber_Qty = clsCommon.myCdbl(dr("Chamber_Qty"))
                obj.Chamber_Desc = clsCommon.myCstr(dr("Chamber_Desc"))
                obj.MIKL_TYPE_CODE = clsCommon.myCstr(dr("MIKL_TYPE_CODE"))
                obj.MILK_GRADE_CODE = clsCommon.myCstr(dr("MILK_GRADE_CODE"))
                obj.Adjust_fat_per = clsCommon.myCdbl(dr("Adjusted_FAT"))
                obj.Adjust_snf_Per = clsCommon.myCdbl(dr("Adjusted_SNF"))
                obj.Adjust_clr = clsCommon.myCdbl(dr("Adjusted_CLR"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class