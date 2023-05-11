Imports common
Imports System.Data.SqlClient

Public Class clsMilkUnloading
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
    Public Shared Function postData(ByVal StrDocNo As String, ByVal formId As String, ByVal trans As SqlTransaction) As Boolean
        ' Dim trans As SqlTransaction = Nothing
        Dim isTrnasInitPostData As Boolean = False
        If trans Is Nothing Then
            trans = clsDBFuncationality.GetTransactin()
            isTrnasInitPostData = True
        Else
            isTrnasInitPostData = False
        End If
        Dim isPosted As Boolean = True
        Try

            If (clsCommon.myLen(StrDocNo) <= 0) Then
                Throw New Exception("Unloading No not found to Post")
            End If

            Dim obj As clsMilkUnloading = clsMilkUnloading.getData(StrDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Unloading_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            ' trans = clsDBFuncationality.GetTransactin()
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Jobwork", "Milk JobWork Unloading", obj.location_Code, obj.Unloading_Date_Time, trans)

            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            '--------------------
            ' Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(formId, "TSPL_JOB_MILK_UNLOADING", "Unloading_No", obj.Unloading_No, trans)
            'If isResult = False Then
            'trans.Commit()
            'Return False
            'End If
            Dim strQry As String = " update TSPL_JOB_MILK_UNLOADING set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where Unloading_no='" & StrDocNo & "'"
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            If isTrnasInitPostData Then
                If isPosted Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            End If

        Catch ex As Exception
            If isTrnasInitPostData Then
                Try
                    trans.Rollback()
                Catch ex1 As Exception

                End Try
            End If

            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return isPosted
    End Function

    Public Shared Function deleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_JOB_MILK_UNLOADING where Unloading_No='" & strDocNo & "'"
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
            Dim qry As String = " select TSPL_JOB_MILK_UNLOADING.Unloading_No as [UnloadingNo] ,TSPL_JOB_MILK_UNLOADING.Unloading_Date_Time as [Unloading Date Time] ,TSPL_JOB_MILK_UNLOADING.Gate_Entry_No as [Gate Entry No] ,TSPL_JOB_MILK_UNLOADING.Weighment_No as [Weighment No] ,TSPL_JOB_MILK_UNLOADING.QC_No as [Qc No] ,TSPL_JOB_MILK_UNLOADING.Tanker_No as [Tanker No] ,case when isnull (TSPL_JOB_MILK_UNLOADING.isPosted,0)=1 then 'Yes' else 'No' end as [Is Posted] ,TSPL_JOB_MILK_UNLOADING.Posting_Date as [Posting Date] ,TSPL_JOB_MILK_UNLOADING.location_Code as [Location Code] ,TSPL_JOB_MILK_UNLOADING.Sub_location_Code as [Sub Location Code] ,TSPL_JOB_MILK_UNLOADING.Item_Code as [Item Code] ,TSPL_JOB_MILK_UNLOADING.Item_Desc as [Item Desc] ,TSPL_JOB_MILK_UNLOADING.UOM as [Uom] ,TSPL_JOB_MILK_UNLOADING.Qty as [Qty] ,TSPL_JOB_MILK_UNLOADING.snf_Per as [SNF Per] ,TSPL_JOB_MILK_UNLOADING.fat_per as [FAT Per] ,TSPL_JOB_MILK_UNLOADING.fat_KG as [FAT Kg] ,TSPL_JOB_MILK_UNLOADING.SNF_KG as [SNF Kg] ,TSPL_JOB_MILK_UNLOADING.Created_By as [Created By] ,TSPL_JOB_MILK_UNLOADING.Created_Date as [Created Date] ,TSPL_JOB_MILK_UNLOADING.Modify_By as [Modify By] ,TSPL_JOB_MILK_UNLOADING.Modify_Date as [Modify Date] ,TSPL_JOB_MILK_UNLOADING.comp_code as [Company Code]  From TSPL_JOB_MILK_UNLOADING "
            str = clsCommon.ShowSelectForm("UNLDFND", qry, "UnloadingNo", whrcls, curcode, "UnloadingNo", isButtonClicked)
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
                strwhrcls = " where 1=1 and Tspl_Milk_Gate_Entry_Details.location_Code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        Try

            Dim qry As String = "select * from (select Tspl_Milk_Gate_Entry_Details.Gate_Entry_No as [GateEntryNo] ,Tspl_Milk_Gate_Entry_Details.Doc_Type as [Doc Type] ,Tspl_Milk_Gate_Entry_Details.Date_And_Time as [ Gate Entry Date And Time],  TSPL_Milk_Weighment_Detail.Weighment_No as [Weighment No],TSPL_Milk_Weighment_Detail.Weighment_Date as [Weighment Date],tspl_Milk_quality_check.QC_No as [QC No],tspl_Milk_quality_check.Qc_In_Date_Time as [QC Date Time]  ,Tspl_Milk_Gate_Entry_Details.Challan_No as [Challan No] ,Tspl_Milk_Gate_Entry_Details.Challan_Date as [Challan Date] ,Tspl_Milk_Gate_Entry_Details.Tanker_No as [Tanker No] , case when isnull (Tspl_Milk_Gate_Entry_Details.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,Tspl_Milk_Gate_Entry_Details.Posting_Date as [Posting Date] ,Tspl_Milk_Gate_Entry_Details.Dispatched_From_Mcc as [Dispatched From Mcc] ,Tspl_Milk_Gate_Entry_Details.location_Code as [Location Code] ,Tspl_Milk_Gate_Entry_Details.Location_Desc as [Location Desc] ,Tspl_Milk_Gate_Entry_Details.Vendor_Code as [Vendor Code] ,Tspl_Milk_Gate_Entry_Details.Vendor_Desc as [Vendor Desc] ,Tspl_Milk_Gate_Entry_Details.Item_Code as [Item Code] ,Tspl_Milk_Gate_Entry_Details.Item_Desc as [Item Desc] ,Tspl_Milk_Gate_Entry_Details.Qty_In_Kg as [Qty In Kg] ,Tspl_Milk_Gate_Entry_Details.snf_Per as [SNF %] ,Tspl_Milk_Gate_Entry_Details.fat_per as [FAT %] ,Tspl_Milk_Gate_Entry_Details.Created_By as [Created By] ,Tspl_Milk_Gate_Entry_Details.Created_Date as [Created Date] ,Tspl_Milk_Gate_Entry_Details.Modify_By as [Modify By] ,Tspl_Milk_Gate_Entry_Details.Modify_Date as [Modify Date] ,Tspl_Milk_Gate_Entry_Details.comp_code as [Company Code] , case when ISNULL(Tspl_Milk_Gate_Entry_Details.Doc_Type,'')='Sku_Receipt' then 1 when ISNULL(tspl_Milk_quality_check.is_param_accepted,0)>0 then 1 else 0 end as Accepted From Tspl_Milk_Gate_Entry_Details left outer join TSPL_Milk_Weighment_Detail on TSPL_Milk_Weighment_Detail.Gate_Entry_No=Tspl_Milk_Gate_Entry_Details.Gate_Entry_No  left outer join tspl_Milk_quality_check on tspl_Milk_quality_check.gate_entry_no=Tspl_Milk_Gate_Entry_Details.Gate_Entry_No " & strwhrcls & ") xx"
            str = clsCommon.ShowSelectForm("GTENUN", qry, "GateEntryNo", whrcls, curcode, "GateEntryNo", isButtonClicked)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function getGateTankerFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try

            Dim qry As String = "select * from (select Tspl_Milk_Gate_Entry_Details.Tanker_No as [TankerNo] ,Tspl_Milk_Gate_Entry_Details.Gate_Entry_No as [GateEntryNo] ,Tspl_Milk_Gate_Entry_Details.Doc_Type as [Doc Type] ,Tspl_Milk_Gate_Entry_Details.Date_And_Time as [ Gate Entry Date And Time],  TSPL_Milk_Weighment_Detail.Weighment_No as [Weighment No],tspl_Milk_quality_check.QC_No as [QC No]  ,Tspl_Milk_Gate_Entry_Details.Challan_No as [Challan No] ,Tspl_Milk_Gate_Entry_Details.Challan_Date as [Challan Date] , case when isnull (Tspl_Milk_Gate_Entry_Details.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,Tspl_Milk_Gate_Entry_Details.Posting_Date as [Posting Date] ,Tspl_Milk_Gate_Entry_Details.Dispatched_From_Mcc as [Dispatched From Mcc] ,Tspl_Milk_Gate_Entry_Details.location_Code as [Location Code] ,Tspl_Milk_Gate_Entry_Details.Location_Desc as [Location Desc] ,Tspl_Milk_Gate_Entry_Details.Vendor_Code as [Vendor Code] ,Tspl_Milk_Gate_Entry_Details.Vendor_Desc as [Vendor Desc] ,Tspl_Milk_Gate_Entry_Details.Item_Code as [Item Code] ,Tspl_Milk_Gate_Entry_Details.Item_Desc as [Item Desc] ,Tspl_Milk_Gate_Entry_Details.Qty_In_Kg as [Qty In Kg] ,Tspl_Milk_Gate_Entry_Details.snf_Per as [SNF %] ,Tspl_Milk_Gate_Entry_Details.fat_per as [FAT %] ,Tspl_Milk_Gate_Entry_Details.Created_By as [Created By] ,Tspl_Milk_Gate_Entry_Details.Created_Date as [Created Date] ,Tspl_Milk_Gate_Entry_Details.Modify_By as [Modify By] ,Tspl_Milk_Gate_Entry_Details.Modify_Date as [Modify Date] ,Tspl_Milk_Gate_Entry_Details.comp_code as [Company Code] , case when ISNULL(Tspl_Milk_Gate_Entry_Details.Doc_Type,'')='Sku_Receipt' then 1 when ISNULL(tspl_Milk_quality_check.is_param_accepted,0)>0 then 1 else 0 end as Accepted From Tspl_Milk_Gate_Entry_Details left outer join TSPL_Milk_Weighment_Detail on TSPL_Milk_Weighment_Detail.Gate_Entry_No=Tspl_Milk_Gate_Entry_Details.Gate_Entry_No  left outer join tspl_Milk_quality_check on tspl_Milk_quality_check.gate_entry_no=Tspl_Milk_Gate_Entry_Details.Gate_Entry_No) xx "
            str = customFinder.getFinder("TNKRFNDUN", qry, whrcls, "TankerNo", curcode, "GateEntryNo")

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function saveData(ByVal obj As clsMilkUnloading, ByVal trans As SqlTransaction, Optional ByVal isHistory As Boolean = False) As Boolean
        Dim issaved As Boolean = True
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Jobwork", "Milk JobWork Unloading", obj.location_Code, obj.Unloading_Date_Time, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Unloading_No", clsCommon.myCstr(obj.Unloading_No))
            clsCommon.AddColumnsForChange(coll, "Unloading_Date_Time", clsCommon.GetPrintDate(obj.Unloading_Date_Time, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", clsCommon.myCstr(obj.Gate_Entry_No))
            clsCommon.AddColumnsForChange(coll, "Weighment_No", clsCommon.myCstr(obj.Weighment_No))
            clsCommon.AddColumnsForChange(coll, "QC_No", clsCommon.myCstr(obj.QC_No))
            clsCommon.AddColumnsForChange(coll, "location_Code", clsCommon.myCstr(obj.location_Code))
            clsCommon.AddColumnsForChange(coll, "Sub_location_Code", clsCommon.myCstr(obj.Sub_location_Code))
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
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isHistory, "TSPL_JOB_MILK_UNLOADING_History", "TSPL_JOB_MILK_UNLOADING"), OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JOB_MILK_UNLOADING", OMInsertOrUpdate.Update, "TSPL_JOB_MILK_UNLOADING.Unloading_no='" + obj.Unloading_No + "'", trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsMilkUnloading
        Dim obj As New clsMilkUnloading
        Try

            Dim qst As String = " select *   From TSPL_JOB_MILK_UNLOADING   where 1=1  "
            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = " and location_code in (" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_JOB_MILK_UNLOADING.Unloading_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_JOB_MILK_UNLOADING.Unloading_No in (select min(Unloading_No ) from TSPL_JOB_MILK_UNLOADING where Unloading_No  >'" + strCode + "' " & whrCls & " )"
                Case NavigatorType.First
                    qst += " and TSPL_JOB_MILK_UNLOADING.Unloading_No in (select MIN(Unloading_No ) from TSPL_JOB_MILK_UNLOADING " & whrCls & " )"
                Case NavigatorType.Last
                    qst += " and TSPL_JOB_MILK_UNLOADING.Unloading_No in (select Max(Unloading_No ) from TSPL_JOB_MILK_UNLOADING " & whrCls & " )"
                Case NavigatorType.Previous
                    qst += " and TSPL_JOB_MILK_UNLOADING.Unloading_No in (select Max(Unloading_No ) from TSPL_JOB_MILK_UNLOADING where Unloading_No  <'" + strCode + "' " & whrCls & " )"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Unloading_No = clsCommon.myCstr(dt.Rows(0)("Unloading_No"))
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

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function isUnloadingDone(ByVal strGateEntryNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        qry = "select count(*) from TSPL_JOB_MILK_UNLOADING where gate_entry_no='" & strGateEntryNo & "' and isposted=1"
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) <= 0 Then
            Return False
        Else
            Return True
        End If
    End Function
End Class
