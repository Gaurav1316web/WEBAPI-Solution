Imports System.Data.SqlClient
Imports common
Imports Telerik.WinControls.UI

Public Class clsMilkQualityCheck
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
    Public Created_By As String = String.Empty
    Public Created_Date As String = String.Empty
    Public Modify_By As String = String.Empty
    Public Modify_Date As String = String.Empty
    Public comp_code As String = String.Empty
    Public Weighment_No As String = String.Empty
    Public Weighment_Date As Date = Nothing
    Public isNewEntry As Boolean = False
    Public DeductionAmount As Double = 0
    Public arrQcParam As List(Of clsMilkQcParam) = Nothing
    Public Receipt_Control_FAT As Double = 0
    Public Receipt_Control_SNF As Double = 0
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
            Dim Qry As String = "select isPosted from tspl_Milk_quality_check where Qc_no='" + strDocNo + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If
            Dim isUsed As Integer = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_JOB_MILK_UNLOADING where Qc_no='" & strDocNo & "'", trans)
            If isUsed > 0 Then
                Throw New Exception("QC No is in use")
            End If
            Qry = "Update tspl_Milk_quality_check set isPosted = 0,Posting_Date=null where QC_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "tspl_Milk_quality_check", "qc_no", trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function postData(ByVal strQCNo As String, ByVal docType As String, ByVal formId As String, ByVal trans As SqlTransaction) As Boolean
        Dim isTrnasInitPostData As Boolean = False
        If trans Is Nothing Then
            trans = clsDBFuncationality.GetTransactin()
            isTrnasInitPostData = True
        Else
            isTrnasInitPostData = False
        End If
        Dim isPosted As Boolean = True
        Try

            If (clsCommon.myLen(strQCNo) <= 0) Then
                Throw New Exception("QC No not found to Post")
            End If

            Dim obj As clsMilkQualityCheck = clsMilkQualityCheck.getData(strQCNo, docType, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.QC_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            ' trans = clsDBFuncationality.GetTransactin()
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.MilkJobWork, clsUserMgtCode.FrmJobMilkQualityCheck, obj.location_Code, obj.QC_In_Date_Time, trans)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            '--------------------
            'Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(formId, "tspl_Milk_quality_check", "QC_no", obj.QC_No, trans)
            'If isResult = False Then
            '    trans.Commit()
            '    Return False
            'End If


            Dim strQry As String = " update tspl_Milk_quality_check set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where qc_no='" & strQCNo & "'"
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            isPosted = clsMilkQualityCheck.SaveAndPostUnloadingGateOutMilkTransferIn(obj.Gate_Entry_No, trans)
            'strQry = " update TSPL_Milk_Weighment_Detail set status=2 where gate_entry_no='" & obj.Gate_Entry_No & "'"
            'isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            ' If isTrnasInitPostData Then

            ' End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strQCNo), "tspl_Milk_quality_check", "qc_no", trans)
            If isPosted Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            Try
                trans.Rollback()
            Catch ex1 As Exception

            End Try
            Throw New Exception(ex.Message)
        End Try
        Return isPosted
    End Function


    Public Shared Function deleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strDocNo), "tspl_Milk_quality_check", "qc_no", "TSPL_Milk_QC_Parameter_Detail", "qc_no", trans)
            Dim qry2 As String = "delete from tspl_Milk_quality_check where qc_no='" & strDocNo & "'"
            Dim qry1 As String = "delete from TSPL_Milk_QC_Parameter_Detail where qc_no='" & strDocNo & "'"
            Dim isDeleted As Boolean = True
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry1, trans)
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry2, trans)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select tspl_Milk_quality_check.QC_No as Code ,tspl_Milk_quality_check.QC_In_Date_Time as [Qc In Date Time] ,tspl_Milk_quality_check.QC_Out_Date_Time as [Qc Out Date Time] ,tspl_Milk_quality_check.Gate_Entry_No as [Gate Entry No] ,tspl_Milk_quality_check.Doc_Type as [Doc Type] ,tspl_Milk_quality_check.Gate_Entry_Date_And_Time as [Gate Entry Date And Time] ,tspl_Milk_quality_check.Challan_No as [Challan No] ,tspl_Milk_quality_check.Challan_Date as [Challan Date] ,tspl_Milk_quality_check.Tanker_No as [Tanker No] ,case when isnull( tspl_Milk_quality_check.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,tspl_Milk_quality_check.Posting_Date as [Posting Date] ,tspl_Milk_quality_check.Dispatched_From_Mcc_Code as [Dispatched From Mcc Code] ,tspl_Milk_quality_check.Dispatched_From_Mcc_Desc as [Dispatched From Mcc Desc] ,tspl_Milk_quality_check.location_Code as [Location Code] ,tspl_Milk_quality_check.Location_Desc as [Location Desc] ,tspl_Milk_quality_check.Vendor_Code as [Vendor Code] ,tspl_Milk_quality_check.Vendor_Desc as [Vendor Desc] ,tspl_Milk_quality_check.Item_Code as [Item Code] ,tspl_Milk_quality_check.Item_Desc as [Item Desc] ,tspl_Milk_quality_check.Qty_In_Kg as [Qty In Kg] ,tspl_Milk_quality_check.snf_Per as [SNF %] ,tspl_Milk_quality_check.fat_per as [FAT %] ,tspl_Milk_quality_check.Dip_Value as [Dip Value] ,tspl_Milk_quality_check.Created_By as [Created By] ,tspl_Milk_quality_check.Created_Date as [Created Date] ,tspl_Milk_quality_check.Modify_By as [Modify By] ,tspl_Milk_quality_check.Modify_Date as [Modify Date] ,tspl_Milk_quality_check.comp_code as [Company Code] ,tspl_Milk_quality_check.Weighment_No as [Weighment No] ,tspl_Milk_quality_check.Weighment_Date as [Weighment Date],ISNULL(TSPL_JOB_MILK_UNLOADING.unloading_no,'') as [Unloading No] , case when ISNULL(TSPL_JOB_MILK_UNLOADING.isPosted ,0)=0 then 'Not Done' else 'Done' end as [Unloading Posting Status],case when  ISNULL(tspl_Milk_quality_check.is_Param_Accepted ,0)=0 and  ISNULL(tspl_Milk_quality_check.isPosted,0)=0 and  ISNULL(tspl_Milk_quality_check.QC_No ,'')='' then 'QC Status Not Found' when  ISNULL(tspl_Milk_quality_check.is_Param_Accepted ,0)=1 and  ISNULL(tspl_Milk_quality_check.isPosted,0)=1 then 'QC Accepted' when  ISNULL(tspl_Milk_quality_check.is_Param_Accepted ,0)=0 and  ISNULL(tspl_Milk_quality_check.isPosted,0)=1 then 'QC Rejected' when  ISNULL(tspl_Milk_quality_check.is_Param_Accepted ,0)=2 and  ISNULL(tspl_Milk_quality_check.isPosted,0)=1 then 'Accepted With Special Approval' else 'QC Pending' end as [QC Status]  From tspl_Milk_quality_check left outer join TSPL_JOB_MILK_UNLOADING on TSPL_JOB_MILK_UNLOADING.Gate_Entry_No=tspl_Milk_quality_check.Gate_Entry_No "
            str = clsCommon.ShowSelectForm("QCFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function getGateEntryFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "select Tspl_Milk_Gate_Entry_Details.Tanker_No as [TankerNo],Tspl_Milk_Gate_Entry_Details.Gate_Entry_No as [Gate Entry No] ,Tspl_Milk_Gate_Entry_Details.Doc_Type as [Doc Type] ,Tspl_Milk_Gate_Entry_Details.Date_And_Time as [ Gate Entry Date And Time],  TSPL_Milk_Weighment_Detail.Weighment_No as [Weighment No],TSPL_Milk_Weighment_Detail.Weighment_date  as [Weighment Date]  ,Tspl_Milk_Gate_Entry_Details.Challan_No as [Challan No] ,Tspl_Milk_Gate_Entry_Details.Challan_Date as [Challan Date]  ,Tspl_Milk_Gate_Entry_Details.isPosted as [Is Posted] ,Tspl_Milk_Gate_Entry_Details.Posting_Date as [Posting Date] ,Tspl_Milk_Gate_Entry_Details.Dispatched_From_Mcc as [Dispatched From Mcc] ,Tspl_Milk_Gate_Entry_Details.location_Code as [Location Code] ,Tspl_Milk_Gate_Entry_Details.Location_Desc as [Location Desc] ,Tspl_Milk_Gate_Entry_Details.Vendor_Code as [Vendor Code] ,Tspl_Milk_Gate_Entry_Details.Vendor_Desc as [Vendor Desc] ,Tspl_Milk_Gate_Entry_Details.Item_Code as [Item Code] ,Tspl_Milk_Gate_Entry_Details.Item_Desc as [Item Desc] ,Tspl_Milk_Gate_Entry_Details.Qty_In_Kg as [Qty In Kg] ,Tspl_Milk_Gate_Entry_Details.snf_Per as [SNF %] ,Tspl_Milk_Gate_Entry_Details.fat_per as [FAT %] ,Tspl_Milk_Gate_Entry_Details.Created_By as [Created By] ,Tspl_Milk_Gate_Entry_Details.Created_Date as [Created Date] ,Tspl_Milk_Gate_Entry_Details.Modify_By as [Modify By] ,Tspl_Milk_Gate_Entry_Details.Modify_Date as [Modify Date] ,Tspl_Milk_Gate_Entry_Details.comp_code as [Company Code]  From Tspl_Milk_Gate_Entry_Details left outer join TSPL_Milk_Weighment_Detail on TSPL_Milk_Weighment_Detail.Gate_Entry_No=Tspl_Milk_Gate_Entry_Details.Gate_Entry_No    "
            str = clsCommon.ShowSelectForm("GTENTR", qry, "TankerNo", whrcls, curcode, "TankerNo", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function getGateEntryFinder(ByVal curcode As String, ByVal whrCls As String) As DataRow
        Dim dt As DataRow = Nothing
        Try
            Dim qry As String = "select Tspl_Milk_Gate_Entry_Details.Tanker_No as [TankerNo],Tspl_Milk_Gate_Entry_Details.Gate_Entry_No as [Gate Entry No] ,Tspl_Milk_Gate_Entry_Details.Doc_Type as [Doc Type] ,Tspl_Milk_Gate_Entry_Details.Date_And_Time as [ Gate Entry Date And Time],  TSPL_Milk_Weighment_Detail.Weighment_No as [Weighment No],TSPL_Milk_Weighment_Detail.Weighment_date  as [Weighment Date]  ,Tspl_Milk_Gate_Entry_Details.Challan_No as [Challan No] ,Tspl_Milk_Gate_Entry_Details.Challan_Date as [Challan Date]  ,Tspl_Milk_Gate_Entry_Details.isPosted as [Is Posted] ,Tspl_Milk_Gate_Entry_Details.Posting_Date as [Posting Date] ,Tspl_Milk_Gate_Entry_Details.Dispatched_From_Mcc as [Dispatched From Mcc] ,Tspl_Milk_Gate_Entry_Details.location_Code as [Location Code] ,Tspl_Milk_Gate_Entry_Details.Location_Desc as [Location Desc] ,Tspl_Milk_Gate_Entry_Details.Vendor_Code as [Vendor Code] ,Tspl_Milk_Gate_Entry_Details.Vendor_Desc as [Vendor Desc] ,Tspl_Milk_Gate_Entry_Details.Item_Code as [Item Code] ,Tspl_Milk_Gate_Entry_Details.Item_Desc as [Item Desc] ,Tspl_Milk_Gate_Entry_Details.Qty_In_Kg as [Qty In Kg] ,Tspl_Milk_Gate_Entry_Details.snf_Per as [SNF %] ,Tspl_Milk_Gate_Entry_Details.fat_per as [FAT %] ,Tspl_Milk_Gate_Entry_Details.Created_By as [Created By] ,Tspl_Milk_Gate_Entry_Details.Created_Date as [Created Date] ,Tspl_Milk_Gate_Entry_Details.Modify_By as [Modify By] ,Tspl_Milk_Gate_Entry_Details.Modify_Date as [Modify Date] ,Tspl_Milk_Gate_Entry_Details.comp_code as [Company Code]  From Tspl_Milk_Gate_Entry_Details left outer join TSPL_Milk_Weighment_Detail on TSPL_Milk_Weighment_Detail.Gate_Entry_No=Tspl_Milk_Gate_Entry_Details.Gate_Entry_No  where 1=1  "
            If clsCommon.myLen(whrCls) > 0 Then
                qry = qry & " and " & whrCls
            End If
            dt = clsCommon.ShowSelectFormForRow("GTENTR", qry, "TankerNo", curcode)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return dt
    End Function
    Public Shared Function saveData(ByVal obj As clsMilkQualityCheck, ByVal trans As SqlTransaction, Optional ByVal isHistory As Boolean = False) As Boolean
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.MilkJobWork, clsUserMgtCode.FrmJobMilkQualityCheck, obj.location_Code, obj.QC_In_Date_Time, trans)
            Dim issaved As Boolean = True
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "QC_No", clsCommon.myCstr(obj.QC_No))
            clsCommon.AddColumnsForChange(coll, "Doc_Type", clsCommon.myCstr(obj.Doc_Type))
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", clsCommon.myCstr(obj.Gate_Entry_No))
            clsCommon.AddColumnsForChange(coll, "QC_In_Date_Time", clsCommon.GetPrintDate(obj.QC_In_Date_Time, "dd/MMM/yyyy hh:mm:ss tt"), True)
            clsCommon.AddColumnsForChange(coll, "QC_Out_Date_Time", clsCommon.GetPrintDate(obj.QC_Out_Date_Time, "dd/MMM/yyyy hh:mm:ss tt"), True)
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_Date_And_Time", clsCommon.GetPrintDate(obj.Gate_Entry_Date_And_Time, "dd/MMM/yyyy hh:mm:ss tt"), True)
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

            clsCommon.AddColumnsForChange(coll, "DeductionAmount", clsCommon.myCdbl(obj.DeductionAmount))
            clsCommon.AddColumnsForChange(coll, "Receipt_Control_FAT", clsCommon.myCdbl(obj.Receipt_Control_FAT))
            clsCommon.AddColumnsForChange(coll, "Receipt_Control_SNF", clsCommon.myCdbl(obj.Receipt_Control_SNF))
            ''-----------------------------
            clsCommon.AddColumnsForChange(coll, "Modify_By", clsCommon.myCstr(objCommonVar.CurrentUserCode))
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", clsCommon.myCstr(objCommonVar.CurrentCompanyCode))
            clsCommon.AddColumnsForChange(coll, "Remarks", clsCommon.myCstr(obj.Remarks))
            If Not obj.isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.QC_No), "tspl_Milk_quality_check", "qc_no", "TSPL_Milk_QC_Parameter_Detail", "qc_no", trans)
            End If
            If obj.isNewEntry OrElse isHistory Then
                clsCommon.AddColumnsForChange(coll, "Created_By", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isHistory, "tspl_Milk_quality_check_History", "tspl_Milk_quality_check"), OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_Milk_quality_check", OMInsertOrUpdate.Update, "tspl_Milk_quality_check.QC_No='" + obj.QC_No + "'", trans)
            End If
            issaved = issaved AndAlso clsMilkQcParam.saveData(obj.arrQcParam, obj.QC_No, trans, isHistory)
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    'Public Shared Function isQCDone(ByVal strGateEntryNo As String, ByVal trans As SqlTransaction) As Boolean
    '    Dim qry As String = String.Empty
    '    qry = "select count(*) from tspl_Milk_quality_check where gate_entry_no='" & strGateEntryNo & "' and isposted=1"
    '    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) <= 0 Then
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function

    Public Shared Function getData(ByVal strCode As String, ByVal docType As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsMilkQualityCheck
        Dim obj As New clsMilkQualityCheck
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = " select *   From tspl_Milk_quality_check   where 1=1 and doc_type='" & docType & "' "
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = "and tspl_Milk_quality_check.location_code in (" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and tspl_Milk_quality_check.QC_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and tspl_Milk_quality_check.QC_No in (select min(QC_No ) from tspl_Milk_quality_check where QC_No  >'" + strCode + "' and doc_type='" & docType & "'  " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and tspl_Milk_quality_check.QC_No in (select MIN(QC_No ) from tspl_Milk_quality_check where doc_type='" & docType & "'  " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and tspl_Milk_quality_check.QC_No in (select Max(QC_No ) from tspl_Milk_quality_check where doc_type='" & docType & "'  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and tspl_Milk_quality_check.QC_No in (select Max(QC_No ) from tspl_Milk_quality_check where QC_No  <'" + strCode + "' and doc_type='" & docType & "'  " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.QC_No = clsCommon.myCstr(dt.Rows(0)("QC_No"))
                obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
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
                obj.Weighment_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select weighment_no from TSPL_Milk_Weighment_Detail where gate_entry_no='" & obj.Gate_Entry_No & "'", trans))
                If clsCommon.myLen(obj.Weighment_No) > 0 Then
                    obj.Weighment_Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select weighment_date from TSPL_Milk_Weighment_Detail where gate_entry_no='" & obj.Gate_Entry_No & "'", trans))
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

                obj.DeductionAmount = clsCommon.myCdbl(dt.Rows(0)("DeductionAmount"))
                obj.Receipt_Control_FAT = clsCommon.myCdbl(dt.Rows(0)("Receipt_Control_FAT"))
                obj.Receipt_Control_SNF = clsCommon.myCdbl(dt.Rows(0)("Receipt_Control_SNF"))
                ''-----------------------------
                obj.arrQcParam = clsMilkQcParam.getData(obj.QC_No, trans)

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsMilkQualityCheck
        Dim obj As New clsMilkQualityCheck
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = " select *   From tspl_Milk_quality_check   where 1=1 "

            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = "and tspl_Milk_quality_check.location_code in (" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and tspl_Milk_quality_check.QC_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and tspl_Milk_quality_check.QC_No in (select min(QC_No ) from tspl_Milk_quality_check where QC_No  >'" + strCode & "'" & whrCls & ")"
                Case NavigatorType.First
                    qst += " and tspl_Milk_quality_check.QC_No in (select MIN(QC_No ) from tspl_Milk_quality_check where 1=1 " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and tspl_Milk_quality_check.QC_No in (select Max(QC_No ) from tspl_Milk_quality_check where 1=1  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and tspl_Milk_quality_check.QC_No in (select Max(QC_No ) from tspl_Milk_quality_check where QC_No  <'" + strCode + "'   " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.QC_No = clsCommon.myCstr(dt.Rows(0)("QC_No"))
                obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
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
                obj.Weighment_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select weighment_no from TSPL_Milk_Weighment_Detail where gate_entry_no='" & obj.Gate_Entry_No & "'", trans))
                If clsCommon.myLen(obj.Weighment_No) > 0 Then
                    obj.Weighment_Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select weighment_date from TSPL_Milk_Weighment_Detail where gate_entry_no='" & obj.Gate_Entry_No & "'", trans))
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
                obj.arrQcParam = clsMilkQcParam.getData(obj.QC_No, trans)

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function isControlSample(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim rValue As Boolean = False
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select case when isnull(Control_Sample,'')='NO' then 0 else 1 end as ControlSample  from TSPL_MCC_Dispatch_Challan left outer join Tspl_Milk_Gate_Entry_Details on Tspl_Milk_Gate_Entry_Details.Challan_No=TSPL_MCC_Dispatch_Challan.Chalan_NO where Tspl_Milk_Gate_Entry_Details.Gate_Entry_No='" & GateEntryNo & "'  ", trans)) = 1 Then
            rValue = True
        Else
            rValue = False
        End If
        Return rValue
    End Function
    Public Shared Function isMccInDoc(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim rValue As Boolean = False
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select case when ISNULL(Doc_Type,'')='Sku_Receipt' then 1 else 0 end as DocType   from Tspl_Milk_Gate_Entry_Details where Tspl_Milk_Gate_Entry_Details.Gate_Entry_No='" & GateEntryNo & "'  ", trans)) = 1 Then
            rValue = True
        Else
            rValue = False
        End If
        Return rValue
    End Function
    Public Shared Function isWeighmentDone(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim rValue As Boolean = False
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select case when isnull(Weighment_No,'')='' then 0 else 1 end as WeighmentNo   from TSPL_Milk_Weighment_Detail  where Gate_Entry_No='" & GateEntryNo & "' and isPosted=1", trans)) = 1 Then
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
    Public Shared Function getVirtualSilo(ByVal LocCode As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim rValue As String = ""
        rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select top 1 Location_Code   from TSPL_LOCATION_MASTER  where Is_Sub_Location='Y' and Main_Location_Code='" & LocCode & "' and Location_Type='Virtual'", trans))
        Return rValue
    End Function
    Public Shared Function getChallanNo(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim rValue As String = ""
        rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select isnull(Challan_No,'') as ChallanNo  from Tspl_Milk_Gate_Entry_Details where Gate_Entry_No ='" & GateEntryNo & "' ", trans))
        Return rValue
    End Function
    Public Shared Function getWeighmentNo(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim rValue As String = ""
        rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  isnull(Weighment_No,'') as WeighmentNo   from TSPL_Milk_Weighment_Detail  where Gate_Entry_No='" & GateEntryNo & "' and isPosted=1", trans))
        Return rValue
    End Function
    Public Shared Function isQCDone(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim rValue As Boolean = False
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select case when isnull(QC_No ,'')='' then 0 else 1 end as QCNO   from tspl_Milk_quality_check  where Gate_Entry_No='" & GateEntryNo & "' and isPosted=1", trans)) = 1 Then
            rValue = True
        Else
            rValue = False
        End If
        Return rValue
    End Function
    Public Shared Function getQCNo(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim rValue As String = ""
        rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  isnull(QC_No ,'') as QCNO   from tspl_Milk_quality_check where Gate_Entry_No='" & GateEntryNo & "' and isPosted=1 ", trans))
        Return rValue
    End Function
    Public Shared Function ControlSampleFAT(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Double
        Dim rValue As Double = 0
        rValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select  isnull(control_sample_fat ,0) ControlSample  from TSPL_MCC_Dispatch_Challan left outer join Tspl_Milk_Gate_Entry_Details on Tspl_Milk_Gate_Entry_Details.Challan_No=TSPL_MCC_Dispatch_Challan.Chalan_NO where Tspl_Milk_Gate_Entry_Details.Gate_Entry_No='" & GateEntryNo & "'  ", trans))
        Return rValue
    End Function
    Public Shared Function ControlSampleSNF(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Double
        Dim rValue As Double = 0
        rValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select  isnull(control_sample_snf  ,0) ControlSample  from TSPL_MCC_Dispatch_Challan left outer join Tspl_Milk_Gate_Entry_Details on Tspl_Milk_Gate_Entry_Details.Challan_No=TSPL_MCC_Dispatch_Challan.Chalan_NO where Tspl_Milk_Gate_Entry_Details.Gate_Entry_No='" & GateEntryNo & "'  ", trans))
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
            obj.isNewEntry = True
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            If obj.isNewEntry Then
                obj.Unloading_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Unloading, "", objGt.location_Code)
                If clsCommon.myLen(obj.Unloading_No) <= 0 Then
                    Throw New Exception("Error In Unloading  No Genertion")
                End If
            End If
            obj.Gate_Entry_No = clsCommon.myCstr(objGt.Gate_Entry_No)
            obj.Unloading_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Tanker_No = clsCommon.myCstr(objGt.Tanker_No)
            obj.Weighment_No = clsMilkQualityCheck.getWeighmentNo(GateEntryNo, trans)
            obj.QC_No = clsMilkQualityCheck.getQCNo(GateEntryNo, trans)
            obj.location_Code = objGt.location_Code
            obj.Sub_location_Code = clsMilkQualityCheck.getVirtualSilo(objGt.location_Code, trans)
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
            isSaved = clsUnloading.saveData(obj, trans)
            isSaved = clsUnloading.postData(obj.Unloading_No, "", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveAndPostUnloadingGateOutMilkTransferIn(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean = True
        Try
            If clsMilkQualityCheck.isMccInDoc(GateEntryNo, trans) Then
                If clsMilkQualityCheck.isIntermittentDoc(clsMilkQualityCheck.getChallanNo(GateEntryNo, trans), trans) Then
                    If clsMilkQualityCheck.isWeighmentDone(GateEntryNo, trans) And clsMilkQualityCheck.isQCDone(GateEntryNo, trans) Then
                        Dim objGt As clsGateEntry = clsGateEntry.getData(GateEntryNo, NavigatorType.Current, trans)
                        If clsMilkQualityCheck.isVirtualSiloFound(objGt.location_Code, trans) Then
                            isSaved = clsMilkQualityCheck.SaveAndPostUnloadingData(GateEntryNo, trans)
                            isSaved = clsMilkQualityCheck.SaveGateOutData(GateEntryNo, trans)
                            isSaved = clsMilkQualityCheck.SaveAndPostTransferInData(GateEntryNo, trans)
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
            obj.isNewEntry = True
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Receipt_Challan_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.MilkTransferIn, "", objGt.location_Code)
            If clsCommon.myLen(obj.Receipt_Challan_No) <= 0 Then
                Throw New Exception("Error in Receipt Challan  No genertion")
            End If
            obj.Receipt_Challan_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Dispatch_Challan_No = clsCommon.myCstr(clsMilkQualityCheck.getChallanNo(GateEntryNo, trans))
            obj.Weighment_No = clsMilkQualityCheck.getWeighmentNo(GateEntryNo, trans)
            obj.Qc_No = clsMilkQualityCheck.getQCNo(GateEntryNo, trans)
            obj.Gate_Entry_no = GateEntryNo
            obj.location_code = clsCommon.myCstr(objGt.location_Code)
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
            Dim strLoc As String = clsDBFuncationality.getSingleValue("select location_code from Tspl_Milk_Gate_Entry_Details  where gate_entry_no='" & GateEntryNo & "'", trans)
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Doc_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.GateOut, "", strLoc)
            If clsCommon.myLen(obj.Doc_No) <= 0 Then
                Throw New Exception("Error In Document  No Genertion")
            End If
            obj.Gate_Entry_No = clsCommon.myCstr(GateEntryNo)
            obj.Doc_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Tanker_No = clsCommon.myCstr(objGt.Tanker_No)
            obj.Weighment_No = clsMilkQualityCheck.getWeighmentNo(GateEntryNo, trans)
            obj.QC_No = clsMilkQualityCheck.getQCNo(GateEntryNo, trans)
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
Public Class clsMilkQcParam
    Public QC_No As String = String.Empty
    Public Param_Field_Code As String = String.Empty
    Public Param_Field_Desc As String = String.Empty
    Public Param_Field_Value As String = String.Empty
    Public Param_Type As String = String.Empty
    Public Shared Function saveData(ByVal arrObj As List(Of clsMilkQcParam), ByVal strQCNo As String, ByVal trans As SqlTransaction, Optional ByVal isHistory As Boolean = False) As Boolean
        Dim issaved As Boolean = True
        Try

            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                If Not isHistory Then
                    Dim qry As String = "delete from TSPL_Milk_QC_Parameter_Detail where QC_No='" & strQCNo & "'"
                    issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
                For Each obj As clsMilkQcParam In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "QC_No", obj.QC_No)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Code", obj.Param_Field_Code)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Desc", obj.Param_Field_Desc)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Value", obj.Param_Field_Value)
                    clsCommon.AddColumnsForChange(coll, "Param_Type", obj.Param_Type)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isHistory, "TSPL_Milk_QC_Parameter_Detail_History", "TSPL_Milk_QC_Parameter_Detail"), OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function getData(ByVal strQCNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMilkQcParam)
        Dim arrObj As List(Of clsMilkQcParam) = Nothing
        Try
            Dim obj As clsMilkQcParam = Nothing
            Dim qry As String = "select * from TSPL_Milk_QC_Parameter_Detail where QC_No='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsMilkQcParam)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsMilkQcParam()
                    obj.QC_No = clsCommon.myCstr(dt.Rows(i)("QC_No"))
                    obj.Param_Field_Code = clsCommon.myCstr(dt.Rows(i)("Param_Field_Code"))
                    obj.Param_Field_Desc = clsCommon.myCstr(dt.Rows(i)("Param_Field_Desc"))
                    obj.Param_Field_Value = clsCommon.myCstr(dt.Rows(i)("Param_Field_Value"))
                    obj.Param_Type = clsCommon.myCstr(dt.Rows(i)("Param_Type"))
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
            Dim qry As String = "delete from TSPL_Milk_QC_Parameter_Detail where QC_No='" & strQCNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return isDeleted
    End Function
End Class
Public Class clsMilkQCManualSealDetail
    Public Chalan_No As String = String.Empty
    Public Seal_No As String = String.Empty
    Public Status As String = String.Empty
    Public Shared Function SaveData(ByVal arr As List(Of clsMilkQCManualSealDetail), ByVal tran As SqlTransaction) As Boolean
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
    Public Shared Function SaveData(ByVal arr As List(Of clsMilkQCManualSealDetail), ByVal tran As SqlTransaction, ByVal isReversed As Boolean) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim i As Integer = 0
            If arr.Count > 0 Then
                If Not isReversed Then
                    Dim qry As String = "delete from TSPL_Milk_QC_Manual_Seal_Details where  chalan_No='" & arr.Item(0).Chalan_No & "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                End If
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Chalan_No", arr.Item(i).Chalan_No)
                    clsCommon.AddColumnsForChange(coll, "Seal_No", arr.Item(i).Seal_No)
                    clsCommon.AddColumnsForChange(coll, "Status", arr.Item(i).Status)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isReversed, "TSPL_Milk_QC_Manual_Seal_Details_History", "TSPL_Milk_QC_Manual_Seal_Details"), OMInsertOrUpdate.Insert, "", tran)
                Next
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function DeleteData(ByVal challan_no As String, ByVal tran As SqlTransaction) As Boolean
        Dim isdeleted As Boolean = False
        Dim qry As String = "delete from TSPL_Milk_QC_Manual_Seal_Details where  chalan_No='" & challan_no & "'"
        isdeleted = clsDBFuncationality.ExecuteNonQuery(qry, tran)
        Return isdeleted
    End Function
    Public Shared Function getData(ByVal chalanNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMilkQCManualSealDetail)
        Dim arr As New List(Of clsMilkQCManualSealDetail)
        Try
            Dim obj As New clsMilkQCManualSealDetail
            Dim q As String = "select * from TSPL_Milk_QC_Manual_Seal_Details where chalan_no='" & chalanNo & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q, trans)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsMilkQCManualSealDetail()
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

Public Class clsMilkQCPaperSealDetail
    Public Chalan_No As String = String.Empty
    Public Seal_No As String = String.Empty
    Public Status As String = String.Empty
    Public Shared Function SaveData(ByVal arr As List(Of clsMilkQCPaperSealDetail), ByVal tran As SqlTransaction) As Boolean
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
        Dim qry As String = "delete from TSPL_Milk_QC_Paper_Seal_Details where  chalan_No='" & challan_no & "'"
        isdeleted = clsDBFuncationality.ExecuteNonQuery(qry, tran)
        Return isdeleted
    End Function
    Public Shared Function SaveData(ByVal arr As List(Of clsMilkQCPaperSealDetail), ByVal tran As SqlTransaction, ByVal isReversed As Boolean) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim i As Integer = 0

            If arr.Count > 0 Then
                If Not isReversed Then
                    Dim qry As String = "delete from TSPL_Milk_QC_Paper_Seal_Details where  chalan_No='" & arr.Item(0).Chalan_No & "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                End If

                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Chalan_No", arr.Item(i).Chalan_No)
                    clsCommon.AddColumnsForChange(coll, "Seal_No", arr.Item(i).Seal_No)
                    clsCommon.AddColumnsForChange(coll, "Status", arr.Item(i).Status)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isReversed, "TSPL_Milk_QC_Paper_Seal_Details_History", "TSPL_Milk_QC_Paper_Seal_Details"), OMInsertOrUpdate.Insert, "", tran)
                Next
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return issaved
    End Function

    Public Shared Function getData(ByVal chalanNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMilkQCPaperSealDetail)
        Dim arr As New List(Of clsMilkQCPaperSealDetail)
        Try
            Dim obj As New clsMilkQCPaperSealDetail
            Dim q As String = "select * from TSPL_Milk_QC_Paper_Seal_Details where chalan_no='" & chalanNo & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q, trans)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsMilkQCPaperSealDetail()
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

