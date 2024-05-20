Imports common
Imports System.Data.SqlClient

Public Class clsUnloading
#Region "Varibales"
    Public IsAgainstJobWork As Integer = 0
    Public Joblocation_Code As String = Nothing
    Public Arr As List(Of clsUnloadingChemberNoDetails) = Nothing
    Public AcknowEntryDocument_No As String = String.Empty
    Public Unloading_No As String = String.Empty
    Public Unloading_Date_Time As Date = Nothing
    Public Gate_Entry_No As String = String.Empty
    Public Weighment_No As String = String.Empty
    Public QC_No As String = String.Empty
    Public Tanker_No As String = String.Empty
    Public isPosted As Integer = 0
    Public Posting_Date As Date = Nothing
    Public location_Code As String = String.Empty
    Public Sub_location_Code As String = String.Empty
    Public Item_Code As String = String.Empty
    Public Item_Desc As String = String.Empty
    Public UOM As String = String.Empty
    Public Qty As Double = 0
    Public snf_Per As Double = 0
    Public fat_per As Double = 0
    Public fat_KG As Double = 0
    Public SNF_KG As Double = 0
    Public Created_By As String = String.Empty
    Public Created_Date As String = String.Empty
    Public Modify_By As String = String.Empty
    Public Modify_Date As String = String.Empty
    Public comp_code As String = String.Empty
    Public isNewEntry As Boolean = False
#End Region
    
  

    Public Shared Function deleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Return deleteData(True, strDocNo, trans)
    End Function

    Public Shared Function deleteData(ByVal isCheckForPost As Boolean, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            If isCheckForPost Then
                Dim Isposted As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isposted from TSPL_MILK_UNLOADING where Unloading_No='" & strDocNo & "'", trans))
                If Isposted = 1 Then
                    Throw New Exception("Document is Posted so you can not delete")
                End If
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_MILK_UNLOADING.location_Code,TSPL_MILK_UNLOADING.Unloading_Date_Time from TSPL_MILK_UNLOADING where Unloading_No='" + strDocNo + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmUnloading, clsCommon.myCstr(dt.Rows(0)("location_Code")), clsCommon.myCDate(dt.Rows(0)("Unloading_Date_Time")), trans)

            End If

            Dim qry As String = ""
            qry = "delete from TSPL_Milk_unloading_Chember_Details where Unloading_No='" & strDocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MILK_UNLOADING where Unloading_No='" & strDocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select TSPL_MILK_UNLOADING.Unloading_No as [UnloadingNo] ,TSPL_MILK_UNLOADING.Unloading_Date_Time as [Unloading Date Time] ,TSPL_MILK_UNLOADING.Gate_Entry_No as [Gate Entry No] ,TSPL_MILK_UNLOADING.Weighment_No as [Weighment No] ,TSPL_MILK_UNLOADING.QC_No as [Qc No] ,TSPL_MILK_UNLOADING.Tanker_No as [Tanker No] ,case when isnull (TSPL_MILK_UNLOADING.isPosted,0)=1 then 'Yes' else 'No' end as [Is Posted] ,TSPL_MILK_UNLOADING.Posting_Date as [Posting Date] ,TSPL_MILK_UNLOADING.location_Code as [Location Code] ,TSPL_MILK_UNLOADING.Sub_location_Code as [Sub Location Code] ,case when tspl_gate_entry_details.In_Return=1 then 'Yes' else 'No' end as [Milk In Return] ,TSPL_MILK_UNLOADING.Item_Code as [Item Code] ,TSPL_MILK_UNLOADING.Item_Desc as [Item Desc] ,TSPL_MILK_UNLOADING.UOM as [Uom] ,TSPL_MILK_UNLOADING.Qty as [Qty] ,TSPL_MILK_UNLOADING.snf_Per as [SNF Per] ,TSPL_MILK_UNLOADING.fat_per as [FAT Per] ,TSPL_MILK_UNLOADING.fat_KG as [FAT Kg] ,TSPL_MILK_UNLOADING.SNF_KG as [SNF Kg] ,TSPL_MILK_UNLOADING.Created_By as [Created By] ,TSPL_MILK_UNLOADING.Created_Date as [Created Date] ,TSPL_MILK_UNLOADING.Modify_By as [Modify By] ,TSPL_MILK_UNLOADING.Modify_Date as [Modify Date] ,TSPL_MILK_UNLOADING.comp_code as [Company Code]  From TSPL_MILK_UNLOADING  left outer join Tspl_Gate_Entry_Details on TSPL_MILK_UNLOADING.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No"
            str = clsCommon.ShowSelectForm("UNLDFND", qry, "UnloadingNo", whrcls, curcode, "TSPL_MILK_UNLOADING.Unloading_Date_Time desc", isButtonClicked, "TSPL_MILK_UNLOADING.Unloading_Date_Time")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function getGateEntryFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim strwhrcls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strwhrcls = " where 1=1 and Tspl_Gate_Entry_Details.location_Code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        Try
            Dim RejectiononQCforSeparationofBulkProcurementMCC As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RejectiononQCforSeparationofBulkProcurementMCC, clsFixedParameterCode.RejectiononQCforSeparationofBulkProcurementMCC, Nothing)) = 1, True, False)
            Dim qry As String = "select * from (select TSPL_gate_entry_details.Gate_Entry_No as [GateEntryNo] ,TSPL_gate_entry_details.Doc_Type as [Doc Type] ,TSPL_gate_entry_details.Date_And_Time as [ Gate Entry Date And Time],  TSPL_Weighment_Detail.Weighment_No as [Weighment No],TSPL_Weighment_Detail.Weighment_Date as [Weighment Date],tspl_quality_check.QC_No as [QC No],tspl_quality_check.Qc_In_Date_Time as [QC Date Time]  ,TSPL_gate_entry_details.Challan_No as [Challan No] ,TSPL_gate_entry_details.Challan_Date as [Challan Date] ,TSPL_gate_entry_details.Tanker_No as [Tanker No] , case when isnull (TSPL_gate_entry_details.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,TSPL_gate_entry_details.Posting_Date as [Posting Date] ,TSPL_gate_entry_details.Dispatched_From_Mcc as [Dispatched From Mcc] ,TSPL_gate_entry_details.location_Code as [Location Code] ,TSPL_gate_entry_details.Location_Desc as [Location Desc] ,TSPL_gate_entry_details.Vendor_Code as [Vendor Code] ,TSPL_gate_entry_details.Vendor_Desc as [Vendor Desc] ,TSPL_gate_entry_details.Item_Code as [Item Code] ,TSPL_gate_entry_details.Item_Desc as [Item Desc] ,TSPL_gate_entry_details.Qty_In_Kg as [Qty In Kg] ,TSPL_gate_entry_details.snf_Per as [SNF %] ,TSPL_gate_entry_details.fat_per as [FAT %] ,TSPL_gate_entry_details.Created_By as [Created By] ,TSPL_gate_entry_details.Created_Date as [Created Date] ,TSPL_gate_entry_details.Modify_By as [Modify By] ,TSPL_gate_entry_details.Modify_Date as [Modify Date] ,TSPL_gate_entry_details.comp_code as [Company Code] , " & IIf(RejectiononQCforSeparationofBulkProcurementMCC = True, "case when ISNULL(tspl_quality_check.is_param_accepted,0)>0 then 1 else 0 end", " case when ISNULL(TSPL_gate_entry_details.Doc_Type,'')='MccProc' then 1 when ISNULL(tspl_quality_check.is_param_accepted,0)>0 then 1 else 0 end ") & "  as Accepted,tspl_quality_check.isPosted From TSPL_gate_entry_details left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join tspl_quality_check on tspl_quality_check.gate_entry_no=TSPL_gate_entry_details.Gate_Entry_No " & strwhrcls & ") xx"
            str = clsCommon.ShowSelectForm("GTENUN", qry, "GateEntryNo", whrcls, curcode, "GateEntryNo", isButtonClicked)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function getGateTankerFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try

            Dim qry As String = "select * from (select TSPL_gate_entry_details.Tanker_No as [TankerNo] ,TSPL_gate_entry_details.Gate_Entry_No as [GateEntryNo] ,TSPL_gate_entry_details.Doc_Type as [Doc Type] ,TSPL_gate_entry_details.Date_And_Time as [ Gate Entry Date And Time],  TSPL_Weighment_Detail.Weighment_No as [Weighment No],tspl_quality_check.QC_No as [QC No]  ,TSPL_gate_entry_details.Challan_No as [Challan No] ,TSPL_gate_entry_details.Challan_Date as [Challan Date] , case when isnull (TSPL_gate_entry_details.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,TSPL_gate_entry_details.Posting_Date as [Posting Date] ,TSPL_gate_entry_details.Dispatched_From_Mcc as [Dispatched From Mcc] ,TSPL_gate_entry_details.location_Code as [Location Code] ,TSPL_gate_entry_details.Location_Desc as [Location Desc] ,TSPL_gate_entry_details.Vendor_Code as [Vendor Code] ,TSPL_gate_entry_details.Vendor_Desc as [Vendor Desc] ,TSPL_gate_entry_details.Item_Code as [Item Code] ,TSPL_gate_entry_details.Item_Desc as [Item Desc] ,TSPL_gate_entry_details.Qty_In_Kg as [Qty In Kg] ,TSPL_gate_entry_details.snf_Per as [SNF %] ,TSPL_gate_entry_details.fat_per as [FAT %] ,TSPL_gate_entry_details.Created_By as [Created By] ,TSPL_gate_entry_details.Created_Date as [Created Date] ,TSPL_gate_entry_details.Modify_By as [Modify By] ,TSPL_gate_entry_details.Modify_Date as [Modify Date] ,TSPL_gate_entry_details.comp_code as [Company Code] , case when ISNULL(TSPL_gate_entry_details.Doc_Type,'')='MccProc' then 1 when ISNULL(tspl_quality_check.is_param_accepted,0)>0 then 1 else 0 end as Accepted From TSPL_gate_entry_details left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join tspl_quality_check on tspl_quality_check.gate_entry_no=TSPL_gate_entry_details.Gate_Entry_No) xx "
            str = customFinder.getFinder("TNKRFNDUN", qry, whrcls, "TankerNo", curcode, "GateEntryNo")

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function saveData(ByVal obj As clsUnloading, ByVal trans As SqlTransaction, Optional ByVal isHistory As Boolean = False) As Boolean
        Dim issaved As Boolean = True
        Dim Isposted As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isposted from TSPL_MILK_UNLOADING where Unloading_No='" & obj.Unloading_No & "'", trans))
        If Isposted = 1 And isHistory = False Then
            Throw New Exception("Document is already Posted")
        End If
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmUnloading, obj.location_Code, obj.Unloading_Date_Time, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Unloading_No", clsCommon.myCstr(obj.Unloading_No))

            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Unloading_Date_Time", clsCommon.GetPrintDate(obj.Unloading_Date_Time, "dd/MMM/yyyy hh:mm:ss tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Unloading_Date_Time", clsCommon.GetPrintDate(obj.Unloading_Date_Time, "dd/MMM/yyyy"))

            End If

            clsCommon.AddColumnsForChange(coll, "IsAgainstJobWork", obj.IsAgainstJobWork)
            clsCommon.AddColumnsForChange(coll, "Joblocation_Code", obj.Joblocation_Code)
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", clsCommon.myCstr(obj.Gate_Entry_No))
            clsCommon.AddColumnsForChange(coll, "Weighment_No", clsCommon.myCstr(obj.Weighment_No))
            clsCommon.AddColumnsForChange(coll, "QC_No", clsCommon.myCstr(obj.QC_No))
            clsCommon.AddColumnsForChange(coll, "location_Code", clsCommon.myCstr(obj.location_Code))
            clsCommon.AddColumnsForChange(coll, "Sub_location_Code", clsCommon.myCstr(obj.Sub_location_Code), True)
            clsCommon.AddColumnsForChange(coll, "AcknowEntryDocument_No", clsCommon.myCstr(obj.AcknowEntryDocument_No), True)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", clsCommon.myCstr(obj.Tanker_No))
            clsCommon.AddColumnsForChange(coll, "isPosted", obj.isPosted)
            If obj.isPosted = 1 Then
                clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Posting_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "Item_Code", clsCommon.myCstr(obj.Item_Code))
            clsCommon.AddColumnsForChange(coll, "Item_Desc", clsCommon.myCstr(obj.Item_Desc))
            clsCommon.AddColumnsForChange(coll, "UOM", clsCommon.myCstr(obj.UOM))
            clsCommon.AddColumnsForChange(coll, "Qty", clsCommon.myCdbl(obj.Qty))
            clsCommon.AddColumnsForChange(coll, "snf_Per", clsCommon.myCdbl(obj.snf_Per))
            clsCommon.AddColumnsForChange(coll, "fat_per", clsCommon.myCdbl(obj.fat_per))
            clsCommon.AddColumnsForChange(coll, "snf_kg", clsCommon.myCdbl(obj.SNF_KG))
            clsCommon.AddColumnsForChange(coll, "fat_kg", clsCommon.myCdbl(obj.fat_KG))
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", clsCommon.myCstr(obj.comp_code))
            If obj.isNewEntry Or isHistory Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isHistory, "TSPL_MILK_UNLOADING_History", "TSPL_MILK_UNLOADING"), OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_UNLOADING", OMInsertOrUpdate.Update, "TSPL_MILK_UNLOADING.Unloading_no='" + obj.Unloading_No + "'", trans)
            End If
            issaved = issaved AndAlso clsUnloadingChemberNoDetails.SaveData(obj.Unloading_No, obj.Arr, trans, obj.Weighment_No)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function

    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsUnloading
        Dim obj As New clsUnloading
        Try

            Dim qst As String = " select *   From TSPL_MILK_UNLOADING   where 1=1  "
            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = " and location_code in (" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_MILK_UNLOADING.Unloading_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_MILK_UNLOADING.Unloading_No in (select min(Unloading_No ) from TSPL_MILK_UNLOADING where 1=1 and Unloading_No  >'" + strCode + "' " & whrCls & " )"
                Case NavigatorType.First
                    qst += " and TSPL_MILK_UNLOADING.Unloading_No in (select MIN(Unloading_No ) from TSPL_MILK_UNLOADING where 1=1 " & whrCls & " )"
                Case NavigatorType.Last
                    qst += " and TSPL_MILK_UNLOADING.Unloading_No in (select Max(Unloading_No ) from TSPL_MILK_UNLOADING where 1=1 " & whrCls & " )"
                Case NavigatorType.Previous
                    qst += " and TSPL_MILK_UNLOADING.Unloading_No in (select Max(Unloading_No ) from TSPL_MILK_UNLOADING where 1=1 and Unloading_No  <'" + strCode + "' " & whrCls & " )"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.IsAgainstJobWork = clsCommon.myCdbl(dt.Rows(0)("IsAgainstJobWork"))
                obj.Joblocation_Code = clsCommon.myCstr(dt.Rows(0)("Joblocation_Code"))
                obj.Unloading_No = clsCommon.myCstr(dt.Rows(0)("Unloading_No"))
                obj.AcknowEntryDocument_No = clsCommon.myCstr(dt.Rows(0)("AcknowEntryDocument_No"))
                obj.Unloading_Date_Time = clsCommon.myCDate(dt.Rows(0)("Unloading_Date_Time"), "dd/MMM/yyyy hh:mm:ss tt")
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
                obj.Weighment_No = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
                obj.QC_No = clsCommon.myCstr(dt.Rows(0)("QC_No"))
                obj.location_Code = clsCommon.myCstr(dt.Rows(0)("location_Code"))
                obj.Sub_location_Code = clsCommon.myCstr(dt.Rows(0)("Sub_location_Code"))
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
                obj.Qty = clsCommon.myCdbl(dt.Rows(0)("Qty"))
                obj.snf_Per = clsCommon.myCdbl(dt.Rows(0)("snf_Per"))
                obj.fat_per = clsCommon.myCdbl(dt.Rows(0)("fat_per"))
                obj.SNF_KG = clsCommon.myCdbl(dt.Rows(0)("snf_kg"))
                obj.fat_KG = clsCommon.myCdbl(dt.Rows(0)("fat_kg"))
                obj.Arr = clsUnloadingChemberNoDetails.GetData(obj.Unloading_No, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function isUnloadingDone(ByVal strGateEntryNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        qry = "select count(*) from tspl_milk_unloading where gate_entry_no='" & strGateEntryNo & "' and isposted=1"
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) <= 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Function isWeigmentDoneChamberwise(ByVal strWeighmentNo As String, ByVal trans As SqlTransaction) As Double
        Dim intChamberNo As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select top 1 Line_No from TSPL_Weighment_Detail left outer join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_Weighment_Detail.Weighment_No=TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No where TSPL_Weighment_Detail.Weighment_No='" & strWeighmentNo & "' and TSPL_WEIGHMENT_CHEMBER_DETAILS.Gross_Weight > 0 order by Line_No desc"))
        Return intChamberNo
    End Function

    Public Shared Function GetUnloadingDocNoFromGateEntry(ByVal strGateEntryNo As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = String.Empty
        Dim Doc_No As String = ""
        qry = "select Unloading_No from tspl_milk_unloading where gate_entry_no='" & strGateEntryNo & "' "
        Doc_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Return Doc_No
    End Function
    Public Shared Function postData(ByVal StrDocNo As String, ByVal formId As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            postData(StrDocNo, formId, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function postData(ByVal StrDocNo As String, ByVal formId As String, ByVal trans As SqlTransaction) As Boolean
        Dim isPosted As Boolean = True
        Try
            If (clsCommon.myLen(StrDocNo) <= 0) Then
                Throw New Exception("Unloading No not found to Post")
            End If
            Dim obj As clsUnloading = clsUnloading.getData(StrDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Unloading_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmUnloading, obj.location_Code, obj.Unloading_Date_Time, trans)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim strQry As String = " update TSPL_MILK_UNLOADING set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where Unloading_no='" & StrDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(strQry, trans)
          
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean ''ERO/01/02/19-000483 by balwinder on 01/02/2019
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Unloading No not found to Post")
            End If
            Dim obj As clsUnloading = clsUnloading.getData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Unloading_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmUnloading, obj.location_Code, obj.Unloading_Date_Time, trans)
            If (obj.isPosted = 0) Then
                Throw New Exception("Transaction should be Posted ")
            End If
            Dim strQry As String = "select Doc_No from TSPL_cleaning where Gate_Entry_No='" + obj.Gate_Entry_No + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Can not reverse the document because cleaning [" + clsCommon.myCstr(dt.Rows(0)("Doc_No")) + "] is created")
            End If


            strQry = " update TSPL_MILK_UNLOADING set isPosted='0', Posting_Date=null where Unloading_no='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsUnloadingChemberNoDetails
#Region "Variables"
    Public Unloading_No As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public UOM As String = Nothing
    Public Chamber_Desc As String = Nothing
    Public Chamber_Qty As Double = 0
    Public snf_Per As Double = 0
    Public fat_per As Double = 0
    Public UnLoading_Status As Integer = 0
    Public StartTime As DateTime?
    Public EndTime As DateTime?
    Public Unloading_Sequence As Integer = 0
    Public Sublocation_Code As String = Nothing
    Public IsBatchWise As String = "N"
    Public Batch_No As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsUnloadingChemberNoDetails), ByVal trans As SqlTransaction, ByVal strWeighmentNo As String) As Boolean
        Dim intCount As Integer = 0
        Dim intLineNo As Integer = 0
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim sQuery As String = "Delete from TSPL_Milk_unloading_Chember_Details where Unloading_No='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            For Each obj As clsUnloadingChemberNoDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Unloading_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "Chamber_Desc", obj.Chamber_Desc)
                clsCommon.AddColumnsForChange(coll, "Chamber_Qty", obj.Chamber_Qty)
                clsCommon.AddColumnsForChange(coll, "snf_Per", obj.snf_Per)
                clsCommon.AddColumnsForChange(coll, "fat_per", obj.fat_per)
                clsCommon.AddColumnsForChange(coll, "UnLoading_Status", obj.UnLoading_Status)
                clsCommon.AddColumnsForChange(coll, "Unloading_Sequence", obj.Unloading_Sequence)
                clsCommon.AddColumnsForChange(coll, "Sublocation_Code", obj.Sublocation_Code, True)
                clsCommon.AddColumnsForChange(coll, "IsBatchWise", obj.IsBatchWise)
                clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)
                If obj.StartTime IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "StartTime", clsCommon.GetPrintDate(obj.StartTime, "dd/MMM/yyyy hh:mm tt"))
                End If
                If obj.EndTime IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "EndTime", clsCommon.GetPrintDate(obj.EndTime, "dd/MMM/yyyy hh:mm tt"))
                End If
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Milk_unloading_Chember_Details", OMInsertOrUpdate.Insert, "", trans)
                Dim dblGrossWt As Double = 0
                Dim WeighmentSeq As Double = 0
                If obj.UnLoading_Status = 1 Then
                    If obj.Unloading_Sequence = 1 Then
                        dblGrossWt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Gross_Weight_Header from tspl_weighment_detail where Weighment_No='" & strWeighmentNo & "'", trans))
                        sQuery = "Update TSPL_Weighment_Chember_Details  set Gross_Weight=" & dblGrossWt & " ,Weighment_Sequence=" & obj.Unloading_Sequence & " where Weighment_No='" & strWeighmentNo & "' and Line_No=" & obj.Line_No & " and isCanType=0 "
                        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                    Else
                        If obj.Unloading_Sequence > 0 Then
                            dblGrossWt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select isnull(Tare_Weight,0) from TSPL_Weighment_Chember_Details where Weighment_No='" & strWeighmentNo & "' and Weighment_Sequence=" & obj.Unloading_Sequence - 1 & " ", trans))
                            sQuery = "Update TSPL_Weighment_Chember_Details set Gross_Weight=" & dblGrossWt & ",Weighment_Sequence=" & obj.Unloading_Sequence & " where Weighment_No='" & strWeighmentNo & "' and Line_No=" & obj.Line_No & " and isCanType=0 "
                            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                        End If                      
                    End If
                End If
               
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsUnloadingChemberNoDetails)
        Dim arr As List(Of clsUnloadingChemberNoDetails) = Nothing
        Dim qry As String
        qry = "select * from " & _
            "TSPL_Milk_unloading_Chember_Details where TSPL_Milk_unloading_Chember_Details.Unloading_No='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsUnloadingChemberNoDetails)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsUnloadingChemberNoDetails = New clsUnloadingChemberNoDetails()
                obj.Unloading_No = clsCommon.myCstr(dr("Unloading_No"))
                obj.Line_No = clsCommon.myCstr(dr("Line_No"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.UOM = clsCommon.myCstr(dr("UOM"))
                obj.snf_Per = clsCommon.myCdbl(dr("snf_Per"))
                obj.fat_per = clsCommon.myCdbl(dr("fat_per"))
                obj.Chamber_Qty = clsCommon.myCdbl(dr("Chamber_Qty"))
                obj.Chamber_Desc = clsCommon.myCstr(dr("Chamber_Desc"))
                obj.UnLoading_Status = clsCommon.myCstr(dr("UnLoading_Status"))
                If dr("StartTime") IsNot DBNull.Value Then
                    obj.StartTime = clsCommon.myCDate(dr("StartTime"))
                End If
                If dr("EndTime") IsNot DBNull.Value Then
                    obj.EndTime = clsCommon.myCDate(dr("EndTime"))
                End If
                obj.Unloading_Sequence = clsCommon.myCdbl(dr("Unloading_Sequence"))
                obj.Sublocation_Code = clsCommon.myCstr(dr("Sublocation_Code"))
                obj.IsBatchWise = clsCommon.myCstr(dr("IsBatchWise"))
                obj.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class
