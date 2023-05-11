Imports System.Data.SqlClient
Imports common
Imports Telerik.WinControls.UI

Public Class clsQCSeparation
    Public IsAgainstJobWork As Integer = 0
    Public Joblocation_Code As String = Nothing
    Public Arr As List(Of clsQCSeparationChemberNoDetails) = Nothing
    Public Remarks As String = String.Empty
    Public Document_No As String = String.Empty
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
    Public arrQcParam As List(Of clsQcSeparationParam) = Nothing
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
            Dim Qry As String = "select isPosted from TSPL_QC_Separation where Document_No='" + strDocNo + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "Update TSPL_QC_Separation set isPosted = 0,Posting_Date=null where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
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

            Dim obj As clsQCSeparation = clsQCSeparation.getData(strQCNo, docType, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.QC_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Quality Check Separation", "Quality Check Separation", obj.location_Code, obj.QC_In_Date_Time, trans)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If


            Dim strQry As String = " update TSPL_QC_Separation set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where Document_No='" & strQCNo & "'"
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            'isPosted = clsQCSeparation.SaveAndPostUnloadingGateOutMilkTransferIn(obj.Gate_Entry_No, trans)

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
        Return deleteData(True, strDocNo, trans)
    End Function
    Public Shared Function deleteData(ByVal isCheckForPost As Boolean, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If isCheckForPost Then
                Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select isPosted from TSPL_QC_Separation Where Document_No='" + strDocNo + "'", trans))
                If Status = 1 Then
                    Throw New Exception("This document is already posted.")
                End If
            End If

            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_QC_Separation where Document_No='" & strDocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_QC_Separation_Parameter_Detail where Document_No='" & strDocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_Quality_Separation_Chember_Details where Document_No='" & strDocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select TSPL_QC_Separation.QC_No as Code ,convert(varchar,TSPL_QC_Separation.QC_In_Date_Time,103) as [Qc In Date] ,convert(varchar,TSPL_QC_Separation.QC_Out_Date_Time,103) as [Qc Out Date] ,TSPL_QC_Separation.Gate_Entry_No as [Gate Entry No] ,TSPL_QC_Separation.Doc_Type as [Doc Type] ,convert(varchar,TSPL_QC_Separation.Gate_Entry_Date_And_Time,103) as [Gate Entry Date] ,TSPL_QC_Separation.Challan_No as [Challan No] ,convert(varchar,TSPL_QC_Separation.Challan_Date,103) as [Challan Date] ,TSPL_QC_Separation.Tanker_No as [Tanker No] ,case when isnull( TSPL_QC_Separation.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,case when tspl_gate_entry_details.In_Return=1 then 'Yes' else 'No' end as [Milk In Return] ,convert(varchar,TSPL_QC_Separation.Posting_Date,103) as [Posting Date] ,TSPL_QC_Separation.Dispatched_From_Mcc_Code as [Dispatched From Mcc Code] ,TSPL_QC_Separation.Dispatched_From_Mcc_Desc as [Dispatched From Mcc Desc] ,TSPL_QC_Separation.location_Code as [Location Code] ,TSPL_QC_Separation.Location_Desc as [Location Desc] ,TSPL_QC_Separation.Vendor_Code as [Vendor Code] ,TSPL_QC_Separation.Vendor_Desc as [Vendor Desc] ,TSPL_QC_Separation.Item_Code as [Item Code] ,TSPL_QC_Separation.Item_Desc as [Item Desc] ,TSPL_QC_Separation.Qty_In_Kg as [Qty In Kg] ,TSPL_QC_Separation.snf_Per as [SNF %] ,TSPL_QC_Separation.fat_per as [FAT %] ,TSPL_QC_Separation.Dip_Value as [Dip Value] ,TSPL_QC_Separation.Created_By as [Created By] ,cast(convert(date,TSPL_QC_Separation.Created_Date,103) as varchar) as [Created Date] ,TSPL_QC_Separation.Modify_By as [Modify By] ,cast(convert(date,TSPL_QC_Separation.Modify_Date,103) as varchar) as [Modify Date] ,TSPL_QC_Separation.comp_code as [Company Code] ,TSPL_QC_Separation.Weighment_No as [Weighment No] ,convert(varchar,TSPL_QC_Separation.Weighment_Date,103) as [Weighment Date],ISNULL(tspl_milk_unloading.unloading_no,'') as [Unloading No] , case when ISNULL(tspl_milk_unloading.isPosted ,0)=0 then 'Not Done' else 'Done' end as [Unloading Posting Status],case when  ISNULL(TSPL_QC_Separation.is_Param_Accepted ,0)=0 and  ISNULL(TSPL_QC_Separation.isPosted,0)=0 and  ISNULL(TSPL_QC_Separation.QC_No ,'')='' then 'QC Status Not Found' when  ISNULL(TSPL_QC_Separation.is_Param_Accepted ,0)=1 and  ISNULL(TSPL_QC_Separation.isPosted,0)=1 then 'QC Accepted' when  ISNULL(TSPL_QC_Separation.is_Param_Accepted ,0)=0 and  ISNULL(TSPL_QC_Separation.isPosted,0)=1 then 'QC Rejected' when  ISNULL(TSPL_QC_Separation.is_Param_Accepted ,0)=2 and  ISNULL(TSPL_QC_Separation.isPosted,0)=1 then 'Accepted With Special Approval' else 'QC Pending' end as [QC Status]  From TSPL_QC_Separation left outer join TSPL_MILK_UNLOADING on TSPL_MILK_UNLOADING.Gate_Entry_No=TSPL_QC_Separation.Gate_Entry_No left outer join Tspl_Gate_Entry_Details on TSPL_QC_Separation.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No "
            str = clsCommon.ShowSelectForm("QCFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
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
            Dim qry As String = "select TSPL_QUALITY_CHECK.Tanker_No as [TankerNo],TSPL_QUALITY_CHECK.QC_No as  [QC No],TSPL_QUALITY_CHECK.Gate_Entry_No as [Gate Entry No] ,TSPL_QUALITY_CHECK.Doc_Type as [Doc Type] , " & _
            " TSPL_Weighment_Detail.Weighment_No as [Weighment No],TSPL_Weighment_Detail.Weighment_date  as [Weighment Date]  ,TSPL_QUALITY_CHECK.Challan_No as [Challan No] ,TSPL_QUALITY_CHECK.Challan_Date as [Challan Date]  ,TSPL_QUALITY_CHECK.isPosted as [Is Posted] ,TSPL_QUALITY_CHECK.Posting_Date as [Posting Date] , " & _
            " TSPL_QUALITY_CHECK.location_Code as [Location Code] ,TSPL_QUALITY_CHECK.Location_Desc as [Location Desc] ,TSPL_QUALITY_CHECK.Vendor_Code as [Vendor Code] ,TSPL_QUALITY_CHECK.Vendor_Desc as [Vendor Desc] ,TSPL_QUALITY_CHECK.Item_Code as [Item Code] ,TSPL_QUALITY_CHECK.Item_Desc as [Item Desc] ,TSPL_QUALITY_CHECK.Qty_In_Kg as [Qty In Kg] ,TSPL_QUALITY_CHECK.snf_Per as [SNF %] ,TSPL_QUALITY_CHECK.fat_per as [FAT %] ,TSPL_QUALITY_CHECK.Created_By as [Created By] ,TSPL_QUALITY_CHECK.Created_Date as [Created Date] ,TSPL_QUALITY_CHECK.Modify_By as [Modify By] ,TSPL_QUALITY_CHECK.Modify_Date as [Modify Date] ,TSPL_QUALITY_CHECK.comp_code as [Company Code]  From TSPL_QUALITY_CHECK left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Gate_Entry_No=TSPL_QUALITY_CHECK.Gate_Entry_No where 1=1  "
            If clsCommon.myLen(whrCls) > 0 Then
                qry = qry & " and " & whrCls
            End If
            dt = clsCommon.ShowSelectFormForRow("QCFndr", qry, "TankerNo", curcode)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return dt
    End Function
    Public Shared Function saveData(ByVal obj As clsQCSeparation, ByVal trans As SqlTransaction, Optional ByVal isHistory As Boolean = False) As Boolean
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Procurement Bulk", "Quality Check", obj.location_Code, obj.QC_In_Date_Time, trans)
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            '============Added by preeti Gupta==============
            Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select isPosted from TSPL_QC_Separation Where QC_No='" + obj.QC_No + "'", trans))

            If Status = 1 AndAlso Not isHistory Then
                Throw New Exception("This document is already posted.")
            End If
            '===============================================
            Dim issaved As Boolean = True
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "QC_No", clsCommon.myCstr(obj.QC_No))
            clsCommon.AddColumnsForChange(coll, "Doc_Type", clsCommon.myCstr(obj.Doc_Type))
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", clsCommon.myCstr(obj.Gate_Entry_No))
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
            ''-----------------------------
            clsCommon.AddColumnsForChange(coll, "Modify_By", clsCommon.myCstr(objCommonVar.CurrentUserCode))
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", clsCommon.myCstr(objCommonVar.CurrentCompanyCode))
            clsCommon.AddColumnsForChange(coll, "Remarks", clsCommon.myCstr(obj.Remarks))
            '========SANJEET=========================
            clsCommon.AddColumnsForChange(coll, "Adjusted_FAT", clsCommon.myCdbl(obj.Adjust_fat_per))
            clsCommon.AddColumnsForChange(coll, "Adjusted_SNF", clsCommon.myCdbl(obj.Adjust_snf_Per))
            clsCommon.AddColumnsForChange(coll, "Adjusted_CLR", clsCommon.myCdbl(obj.Adjust_clr))
            clsCommon.AddColumnsForChange(coll, "IsAgainstJobWork", obj.IsAgainstJobWork)
            clsCommon.AddColumnsForChange(coll, "Joblocation_Code", obj.Joblocation_Code)
            '=================================================
            If obj.isNewEntry OrElse isHistory Then
                clsCommon.AddColumnsForChange(coll, "Created_By", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isHistory, "tspl_quality_check_History", "TSPL_QC_Separation"), OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_Separation", OMInsertOrUpdate.Update, "TSPL_QC_Separation.QC_No='" + obj.QC_No + "'", trans)
            End If
            issaved = issaved AndAlso clsQcSeparationParam.saveData(obj.arrQcParam, obj.QC_No, trans, isHistory)
            issaved = issaved AndAlso clsQCSeparationChemberNoDetails.SaveData(obj.QC_No, obj.Arr, trans)
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal strCode As String, ByVal docType As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsQCSeparation
        Dim obj As New clsQCSeparation
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = " select *   From TSPL_QC_Separation   where 1=1 and doc_type='" & docType & "' "
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = "and TSPL_QC_Separation.location_code in (" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_QC_Separation.Document_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_QC_Separation.Document_No in (select min(Document_No ) from TSPL_QC_Separation where Document_No  >'" + strCode + "' and doc_type='" & docType & "'  " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_QC_Separation.Document_No in (select MIN(Document_No ) from TSPL_QC_Separation where doc_type='" & docType & "'  " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_QC_Separation.Document_No in (select Max(Document_No ) from TSPL_QC_Separation where doc_type='" & docType & "'  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_QC_Separation.Document_No in (select Max(Document_No ) from TSPL_QC_Separation where Document_No  <'" + strCode + "' and doc_type='" & docType & "'  " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
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
                obj.arrQcParam = clsQcSeparationParam.getData(obj.QC_No, trans)
                obj.Arr = clsQCSeparationChemberNoDetails.GetData(obj.QC_No, trans)
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
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select case when isnull(QC_No ,'')='' then 0 else 1 end as QCNO   from TSPL_QC_Separation  where Gate_Entry_No='" & GateEntryNo & "' and isPosted=1", trans)) = 1 Then
            rValue = True
        Else
            rValue = False
        End If
        Return rValue
    End Function
    Public Shared Function getQCNo(ByVal GateEntryNo As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim rValue As String = ""
        rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  isnull(QC_No ,'') as QCNO   from TSPL_QC_Separation where Gate_Entry_No='" & GateEntryNo & "' and isPosted=1 ", trans))
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
    
End Class
Public Class clsQcSeparationParam
    Public Document_No As String = String.Empty
    Public Param_Field_Code As String = String.Empty
    Public Param_Field_Desc As String = String.Empty
    Public Param_Field_Value As String = String.Empty
    Public Param_Type As String = String.Empty
    Public LINE_NO As Integer = 0
    Public Remarks As String = String.Empty
    Public Shared Function saveData(ByVal arrObj As List(Of clsQcSeparationParam), ByVal strDocument_No As String, ByVal trans As SqlTransaction, Optional ByVal isHistory As Boolean = False) As Boolean
        Dim issaved As Boolean = True
        Try

            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                If Not isHistory Then
                    Dim qry As String = "delete from TSPL_QC_Separation_Parameter_Detail where Document_No='" & strDocument_No & "'"
                    issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
                For Each obj As clsQcSeparationParam In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Code", obj.Param_Field_Code)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Desc", obj.Param_Field_Desc)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Value", obj.Param_Field_Value)
                    clsCommon.AddColumnsForChange(coll, "Param_Type", obj.Param_Type)
                    clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO)
                    clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isHistory, "TSPL_QC_Separation_Parameter_Detail_Hist_data", "TSPL_QC_Separation_Parameter_Detail"), OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function getData(ByVal strQCNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsQcSeparationParam)
        Dim arrObj As List(Of clsQcSeparationParam) = Nothing
        Try
            Dim obj As clsQcSeparationParam = Nothing
            Dim qry As String = "select * from TSPL_QC_Separation_Parameter_Detail where Document_No='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsQcSeparationParam)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsQcSeparationParam()
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.Param_Field_Code = clsCommon.myCstr(dt.Rows(i)("Param_Field_Code"))
                    obj.Param_Field_Desc = clsCommon.myCstr(dt.Rows(i)("Param_Field_Desc"))
                    obj.Param_Field_Value = clsCommon.myCstr(dt.Rows(i)("Param_Field_Value"))
                    obj.Param_Type = clsCommon.myCstr(dt.Rows(i)("Param_Type"))
                    obj.LINE_NO = clsCommon.myCdbl(dt.Rows(i)("LINE_NO"))
                    obj.Remarks = clsCommon.myCstr(dt.Rows(i)("Remarks"))
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
            Dim qry As String = "delete from TSPL_QC_Separation_Parameter_Detail where Document_No='" & strQCNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return isDeleted
    End Function
End Class
Public Class clsQCSeparationChemberNoDetails
#Region "Variables"
    Public Document_No As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public UOM As String = Nothing
    Public Qty As Integer = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsQCSeparationChemberNoDetails), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim sQuery As String = "Delete from TSPL_QC_Separation_Chember_Details where Document_No='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            For Each obj As clsQCSeparationChemberNoDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_Separation_Chember_Details", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsQCSeparationChemberNoDetails)
        Dim arr As List(Of clsQCSeparationChemberNoDetails) = Nothing
        Dim qry As String
        qry = "select * from " & _
            "TSPL_QC_Separation_Chember_Details where TSPL_QC_Separation_Chember_Details.Document_No='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsQCSeparationChemberNoDetails)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsQCSeparationChemberNoDetails = New clsQCSeparationChemberNoDetails()
                obj.Document_No = clsCommon.myCstr(dr("Document_No"))
                obj.Line_No = clsCommon.myCstr(dr("Line_No"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.UOM = clsCommon.myCstr(dr("UOM"))
                obj.Qty = clsCommon.myCdbl(dr("Qty"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class