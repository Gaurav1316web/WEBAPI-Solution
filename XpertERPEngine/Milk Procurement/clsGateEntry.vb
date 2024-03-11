' Created By Pankaj Jha on 03/07/204 Against Ticket No: BM00000002720
Imports System.Data.SqlClient
Imports common
Public Class clsGateEntry
    Public Weight As Double = 0
    Public No_Of_CAN As Integer = 0
    Public Transpoter_Id As String = Nothing
    Public Bulk_ROUTE_NO As String = Nothing
    Public AcknowEntryDocument_No As String = Nothing
    Public Distance As Decimal = 0
    Public Rate As Decimal = 0
    Public Amount As Decimal = 0
    Public ProvisionNo As String = Nothing
    Public NO_OF_CHAMBER As Integer = 0
    Public IsAgainstJobWork As Integer = 0
    Public Sublocation_Code As String = Nothing
    Public PO_No As String = Nothing
    Public Tanker_Return As Integer = 0
    Public SealNo_Header As String = Nothing
    Public Intimation_No As String = Nothing
    Public Supplier_Code As String = Nothing
    Public Dispatch_Centre_Code As String = Nothing
    Public MIKL_TYPE_CODE As String = Nothing
    Public Intimation_Status As String = Nothing
    Public Gate_Entry_Type As String = Nothing
    Public Seal_Status As String = Nothing
    Public TotalQty_In_Kg As Double = 0
    Public isNewEntry As Boolean = False
    Public Gate_Entry_No As String = String.Empty
    Public Doc_Type As String = String.Empty
    Public Date_And_Time As Date = Nothing
    Public Challan_No As String = String.Empty
    Public Challan_Date As Date = Nothing
    Public Tanker_No As String = String.Empty
    Public isPosted As Integer = 0
    Public Posting_Date As Date = Nothing
    Public Dispatched_From_Mcc As String = String.Empty
    Public location_Code As String = String.Empty
    Public Location_Desc As String = String.Empty
    Public Vendor_Code As String = String.Empty
    Public Vendor_Desc As String = String.Empty
    Public Item_Code As String = String.Empty
    Public Item_Desc As String = String.Empty
    Public Qty_In_Kg As Double = 0
    Public snf_Per As Double = 0
    Public fat_per As Double = 0
    Public Created_By As String = String.Empty
    Public Created_Date As String = String.Empty
    Public Modify_By As String = String.Empty
    Public Modify_Date As String = String.Empty
    Public comp_code As String = String.Empty
    Public UOM As String = String.Empty
    Public Arr As List(Of clsGateEntryChemberNoDetails) = Nothing
    Public Arr1 As List(Of clsChemberNoDetails) = Nothing
    Public ArrPrice As List(Of clsGateEntryPriceDetails) = Nothing
    Public Amendment_No As Double = 0
    Public IsNetWeight As Integer = 0
    Public Reference_No As String = String.Empty
    Public openingKM As Decimal = 0
    Public closingKM As Decimal = 0
    Public Toll_Amount As Decimal = 0
    Public Closing_Date As DateTime? = Nothing
    Public Against_Gate_Out As String = Nothing
    Public IsAgainstGateOut As Integer = 0
    Public ROUTE_NO As String = Nothing
    Public Shared Function saveData(ByVal obj As clsGateEntry, ByVal trans As SqlTransaction, Optional ByVal isHistory As Boolean = False, Optional ByVal isAmendment As Boolean = False) As Boolean
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.frmGateEntry, obj.location_Code, obj.Date_And_Time, trans)
            Dim issaved As Boolean = True
            '============Added by preeti Gupta==============
            Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select isPosted from tspl_gate_entry_details Where Gate_Entry_No='" + obj.Gate_Entry_No + "'", trans))
            If Status = 1 AndAlso Not isHistory AndAlso Not isAmendment Then
                Throw New Exception("This document is already posted.")
            End If
            '===============================================
            If Not obj.isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Gate_Entry_No), "TSPL_GATE_ENTRY_DETAILS", "Gate_Entry_No", "TSPL_Bulk_Gate_Entry_Chember_Details", "GE_Code", "TSPL_Gate_Entry_Chember_Details", "GE_Code", trans)
            End If
            If isAmendment Then
                obj.Amendment_No = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT ISNULL(Amendment_No,0) from TSPL_GATE_ENTRY_DETAILS WHERE Gate_Entry_No='" + clsCommon.myCstr(obj.Gate_Entry_No) + "'", trans))
                issaved = issaved AndAlso SaveDataForHistory(obj.Gate_Entry_No, clsCommon.myCdbl(obj.Amendment_No + 1), trans)
            End If

            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", obj.Gate_Entry_No)
            clsCommon.AddColumnsForChange(coll, "Doc_Type", obj.Doc_Type)


            '==============================Added by preeti gupta======================
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Date_And_Time", clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MMM/yyyy hh:mm:ss tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Date_And_Time", clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MMM/yyyy"))
            End If
            '===================================End=======================================
            clsCommon.AddColumnsForChange(coll, "No_Of_CAN", obj.No_Of_CAN)
            clsCommon.AddColumnsForChange(coll, "Weight", obj.Weight)
            clsCommon.AddColumnsForChange(coll, "Transpoter_Id", obj.Transpoter_Id, True)
            clsCommon.AddColumnsForChange(coll, "AcknowEntryDocument_No", obj.AcknowEntryDocument_No, True)
            clsCommon.AddColumnsForChange(coll, "Bulk_ROUTE_NO", obj.Bulk_ROUTE_NO)
            clsCommon.AddColumnsForChange(coll, "Distance", obj.Distance)
            clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
            clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
            clsCommon.AddColumnsForChange(coll, "ProvisionNo", obj.ProvisionNo)
            clsCommon.AddColumnsForChange(coll, "NO_OF_CHAMBER", obj.NO_OF_CHAMBER)
            clsCommon.AddColumnsForChange(coll, "Challan_No", obj.Challan_No)
            clsCommon.AddColumnsForChange(coll, "Challan_Date", clsCommon.GetPrintDate(obj.Challan_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No)
            clsCommon.AddColumnsForChange(coll, "isPosted", obj.isPosted)
            clsCommon.AddColumnsForChange(coll, "Against_Gate_Out", obj.Against_Gate_Out, True)
            clsCommon.AddColumnsForChange(coll, "IsAgainstGateOut", obj.IsAgainstGateOut)
            clsCommon.AddColumnsForChange(coll, "ROUTE_NO", obj.ROUTE_NO, True)
            If obj.isPosted = 1 AndAlso Not isAmendment Then
                clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Posting_Date, "dd/MMM/yyyy"))
            End If
            If isAmendment Then
                Dim AmendmentCode As String = Nothing
                AmendmentCode = obj.Gate_Entry_No + "$" + clsCommon.myCstr(obj.Amendment_No + 1)
                clsCommon.AddColumnsForChange(coll, "Amendment_No", obj.Amendment_No + 1)
                clsCommon.AddColumnsForChange(coll, "Amendment_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Amendment_Code", AmendmentCode)
                clsCommon.AddColumnsForChange(coll, "Amendment_By", objCommonVar.CurrentUserCode)
            Else
                clsCommon.AddColumnsForChange(coll, "Amendment_No", obj.Amendment_No)
            End If
            clsCommon.AddColumnsForChange(coll, "Dispatched_From_Mcc", obj.Dispatched_From_Mcc, True)
            clsCommon.AddColumnsForChange(coll, "location_Code", obj.location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Location_Desc", obj.Location_Desc, True)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code, True)
            clsCommon.AddColumnsForChange(coll, "Vendor_Desc", obj.Vendor_Desc, True)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
            clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
            clsCommon.AddColumnsForChange(coll, "Qty_In_Kg", obj.Qty_In_Kg)
            clsCommon.AddColumnsForChange(coll, "snf_Per", obj.snf_Per)
            clsCommon.AddColumnsForChange(coll, "fat_per", obj.fat_per)
            clsCommon.AddColumnsForChange(coll, "Intimation_No", obj.Intimation_No, True)
            clsCommon.AddColumnsForChange(coll, "Supplier_Code", obj.Supplier_Code, True)
            clsCommon.AddColumnsForChange(coll, "SealNo_Header", obj.SealNo_Header)
            clsCommon.AddColumnsForChange(coll, "Dispatch_Centre_Code", obj.Dispatch_Centre_Code)
            clsCommon.AddColumnsForChange(coll, "MIKL_TYPE_CODE", obj.MIKL_TYPE_CODE, True)
            clsCommon.AddColumnsForChange(coll, "Intimation_Status", obj.Intimation_Status)
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_Type", obj.Gate_Entry_Type)
            clsCommon.AddColumnsForChange(coll, "Seal_Status", obj.Seal_Status)
            clsCommon.AddColumnsForChange(coll, "TotalQty_In_Kg", obj.TotalQty_In_Kg)
            clsCommon.AddColumnsForChange(coll, "Tanker_Return", obj.Tanker_Return)
            clsCommon.AddColumnsForChange(coll, "PO_No", obj.PO_No, True)
            clsCommon.AddColumnsForChange(coll, "IsAgainstJobWork", obj.IsAgainstJobWork)
            clsCommon.AddColumnsForChange(coll, "Sublocation_Code", obj.Sublocation_Code)
            clsCommon.AddColumnsForChange(coll, "IsNetWeight", obj.IsNetWeight)
            clsCommon.AddColumnsForChange(coll, "openingKM", obj.openingKM)
            clsCommon.AddColumnsForChange(coll, "closingKM", obj.closingKM)
            clsCommon.AddColumnsForChange(coll, "Toll_Amount", obj.Toll_Amount)
            If Not isAmendment Then
                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            End If
            clsCommon.AddColumnsForChange(coll, "Comp_Code", clsCommon.myCstr(obj.comp_code))
            If obj.isNewEntry Or isHistory Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                '===================
                Dim AllowGenerateReferenceNoForBulkGateEntry As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowGenerateReferenceNoForBulkGateEntry, clsFixedParameterCode.AllowGenerateReferenceNoForBulkGateEntry, trans)) = 0, False, True)
                If AllowGenerateReferenceNoForBulkGateEntry = True AndAlso obj.isNewEntry = True Then
                    obj.Reference_No = clsERPFuncationality.GetNextCode(trans, obj.Date_And_Time, clsDocType.GateEntry, clsDocTransactionType.ReferenceNo, Nothing)
                    If clsCommon.myLen(obj.Reference_No) <= 0 Then
                        Throw New Exception("Error in Refrence No genertion")
                    End If
                    clsCommon.AddColumnsForChange(coll, "Reference_No", obj.Reference_No)
                End If
                '===================
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isHistory, "tspl_gate_entry_details_History", "tspl_gate_entry_details"), OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_gate_entry_details", OMInsertOrUpdate.Update, "tspl_gate_entry_details.gate_entry_no='" + obj.Gate_Entry_No + "'", trans)
            End If
            issaved = issaved AndAlso clsChemberNoDetails.SaveData(obj.Gate_Entry_No, obj.Arr1, trans)
            issaved = issaved AndAlso clsGateEntryChemberNoDetails.SaveData(obj.Gate_Entry_No, obj.Arr, trans)
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function UpdateAfterPosting(ByVal obj As clsGateEntry, ByVal DocNO As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If obj IsNot Nothing And clsCommon.myLen(DocNO) > 0 Then
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "openingKM", obj.openingKM)
                clsCommon.AddColumnsForChange(coll, "closingKM", obj.closingKM)
                clsCommon.AddColumnsForChange(coll, "Toll_Amount", obj.Toll_Amount)
                If obj.Closing_Date IsNot Nothing And clsCommon.myLen(obj.Closing_Date) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Closing_Date", clsCommon.GetPrintDate(obj.Closing_Date, "dd/MMM/yyyy hh:mm tt"))
                End If
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_gate_entry_details", OMInsertOrUpdate.Update, "tspl_gate_entry_details.gate_entry_no='" + DocNO + "'", trans)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function CreateProvison(ByVal strDocNo As String, ByVal FormId As String, ByVal trans As SqlTransaction) As Boolean
        Dim obj As clsGateEntry = clsGateEntry.getData(strDocNo, NavigatorType.Current, trans)

        Dim objProv As clsProvisionEntry = New clsProvisionEntry()
        objProv.isNewEntry = True
        objProv.Doc_Date = obj.Date_And_Time

        objProv.Vendor_Code = obj.Vendor_Code
        objProv.Vendor_Desc = obj.Vendor_Desc
        objProv.Vendor_Type = "Secondary Transporter"
        objProv.Prov_type = "Bulk Proc"
        objProv.Status = "No"
        objProv.Ref_Doc_No = obj.Gate_Entry_No


        Dim strRatePerKM As Double = clsDBFuncationality.getSingleValue("select isnull(Price_KM,0) as Price_KM from tspl_tanker_master where Tanker_No='" + obj.Tanker_No + "'", trans)

        If strRatePerKM <= 0 Then
            Throw New Exception("First Enter Price Per KM for Tanker No : " + obj.Tanker_No + " in Tanker Master.")
        End If

        Dim dclDistance As Decimal = clsDBFuncationality.getSingleValue("select isnull(tspl_bulk_route_master.Distance,0) from TSPL_MCC_TANKER_GATE_OUT left outer Join tspl_bulk_route_master on tspl_bulk_route_master.ROUTE_NO=TSPL_MCC_TANKER_GATE_OUT.Bulk_route_code where TSPL_MCC_TANKER_GATE_OUT.gate_out_no= '" + obj.Against_Gate_Out + "'", trans)
        If dclDistance <= 0 Then
            Throw New Exception("First Enter +ve Distance on Bulk Route master.")
        End If

        'If (clsCommon.myCdbl(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateProvisionOnOpeningAndClosingKM, clsFixedParameterCode.CreateProvisionOnOpeningAndClosingKM, trans))) = 1) Then
        If dclDistance > (obj.closingKM - obj.openingKM) Then
            dclDistance = (obj.closingKM - obj.openingKM)
        End If
        'End If
        'Dim dbltollAmt As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(Toll_Amt ,0) from  tspl_location_distance_master where From_Location_code ='" & obj.MCC_Code & "' and to_Location_Code ='" & obj.Mcc_Or_Plant_Code & "'", trans))
        'If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TollTaxMaster, clsFixedParameterCode.TollTaxMaster, trans)) = 0, False, True) = False AndAlso IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateProvisionOfTankerDispatchWithClosingKM, clsFixedParameterCode.CreateProvisionOfTankerDispatchWithClosingKM, trans)) = 0, False, True) = True Then
        '    dbltollAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(Toll_Amount ,0) from  tspl_mcc_dispatch_challan where  Chalan_NO ='" & obj.Chalan_NO & "'", trans))

        'End If

        objProv.Amount = strRatePerKM * dclDistance + obj.Toll_Amount
        objProv.Prog_Code = FormId
        objProv.Prov_Month = Month(obj.Date_And_Time)
        objProv.Prov_Year = Year(obj.Date_And_Time)
        objProv.Comp_Code = obj.Comp_Code
        objProv.Created_By = obj.Created_By
        objProv.Created_Date = obj.Created_Date
        objProv.Modified_By = obj.Modify_By
        objProv.Loc_Code = obj.location_Code
        objProv.Loc_Desc = obj.Location_Desc
        objProv.Toll_Amt = obj.Toll_Amount
        objProv.GL_Location_Seg = clsLocation.GetSegmentCode(obj.location_Code, trans)
        'objProv.
        clsProvisionEntry.SaveData(objProv, trans)
        clsProvisionEntry.PostData(objProv.Doc_No, trans, True)
        Return True
    End Function
    Public Shared Function SaveDataForHistory(ByVal strCode As String, ByVal intAmbandentNo As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim strInvColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_GATE_ENTRY_DETAILS_History", trans)
            Dim qry As String = "INSERT INTO TSPL_GATE_ENTRY_DETAILS_HISTORY (" + strInvColumns + ") SELECT " + strInvColumns.Replace("Amendment_No", "" + clsCommon.myCstr(intAmbandentNo) + "") + " FROM TSPL_GATE_ENTRY_DETAILS WHERE Gate_Entry_No='" + clsCommon.myCstr(strCode) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            'Dim qry As String = " select tspl_gate_entry_details.Gate_Entry_No as [GateEntryNo] ,tspl_gate_entry_details.Doc_Type as [Doc Type] ,convert(varchar,tspl_gate_entry_details.Date_And_Time,103) as [Date] ,tspl_gate_entry_details.Challan_No as [Challan No] ,convert(varchar,tspl_gate_entry_details.Challan_Date,103) as [Challan Date] ,tspl_gate_entry_details.Tanker_No as [Tanker No] ,tspl_gate_entry_details.Dispatched_From_Mcc as [Dispatched From Mcc] ,case when tspl_gate_entry_details.isPosted=0 then 'NO' else 'YES' end as [Posting Status],tspl_gate_entry_details.location_Code as [Location Code] ,tspl_gate_entry_details.Location_Desc as [Location Desc] ,tspl_gate_entry_details.Vendor_Code as [Vendor Code] ,tspl_gate_entry_details.Vendor_Desc as [Vendor Desc] ,tspl_gate_entry_details.Item_Code as [Item Code] ,tspl_gate_entry_details.Item_Desc as [Item Desc] ,tspl_gate_entry_details.Qty_In_Kg as [Qty In Kg] ,tspl_gate_entry_details.snf_Per as [SNF Per] ,tspl_gate_entry_details.fat_per as [FAT Per] ,tspl_gate_entry_details.Created_By as [Created By] ,cast(convert(date,tspl_gate_entry_details.Created_Date,103) as varchar) as [Created Date] ,tspl_gate_entry_details.Modify_By as [Modify By] ,cast(convert(date,tspl_gate_entry_details.Modify_Date,103)as varchar) as [Modify Date] ,tspl_gate_entry_details.comp_code as [Company Code], case when  ISNULL(tspl_gate_out.Doc_No ,'')='' then 'Pending For Gate Out' else ISNULL(tspl_gate_out.Doc_No ,'') end as [Gate Out No]  From tspl_gate_entry_details left outer join TSPL_Gate_Out on TSPL_Gate_Out.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No "
            Dim qry As String = " select tspl_gate_entry_details.Gate_Entry_No as [GateEntryNo] ,tspl_gate_entry_details.Doc_Type as [Doc Type] ,convert(varchar,tspl_gate_entry_details.Date_And_Time,103) as [Date] ,tspl_gate_entry_details.Challan_No as [Challan No] ,convert(varchar,tspl_gate_entry_details.Challan_Date,103) as [Challan Date] ,tspl_gate_entry_details.Tanker_No as [Tanker No] ,tspl_gate_entry_details.Dispatched_From_Mcc as [Dispatched From Mcc] ,case when tspl_gate_entry_details.isPosted=0 then 'NO' else 'YES' end as [Posting Status],tspl_gate_entry_details.location_Code as [Location Code] ,tspl_gate_entry_details.Location_Desc as [Location Desc] ,tspl_gate_entry_details.Vendor_Code as [Vendor Code] ,tspl_gate_entry_details.Vendor_Desc as [Vendor Desc],case when tspl_gate_entry_details.In_Return=1 then 'Yes' else 'No' end as [Milk In Return] ,tspl_gate_entry_details.Item_Code as [Item Code] ,tspl_gate_entry_details.Item_Desc as [Item Desc] ,tspl_gate_entry_details.Qty_In_Kg as [Qty In Kg] ,tspl_gate_entry_details.snf_Per as [SNF Per] ,tspl_gate_entry_details.fat_per as [FAT Per] ,tspl_gate_entry_details.Created_By as [Created By] ,cast(convert(date,tspl_gate_entry_details.Created_Date,103) as varchar) as [Created Date] ,tspl_gate_entry_details.Modify_By as [Modify By] ,cast(convert(date,tspl_gate_entry_details.Modify_Date,103)as varchar) as [Modify Date] ,tspl_gate_entry_details.comp_code as [Company Code], case when  ISNULL(tspl_gate_out.Doc_No ,'')='' then 'Pending For Gate Out' else ISNULL(tspl_gate_out.Doc_No ,'') end as [Gate Out No]  From tspl_gate_entry_details left outer join TSPL_Gate_Out on TSPL_Gate_Out.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No "
            str = clsCommon.ShowSelectForm("GATEENTRY", qry, "GateEntryNo", whrcls, curcode, "tspl_gate_entry_details.Date_And_Time desc", isButtonClicked)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function getTankerFinder(ByVal whrcls As String, ByVal curcode As String) As String

        Dim str As String = ""
        Try
            Dim qry As String = " select tspl_gate_entry_details.Tanker_No as [TankerNo] , tspl_gate_entry_details.Gate_Entry_No as [GateEntryNo] ,tspl_gate_entry_details.Doc_Type as [Doc Type] ,tspl_gate_entry_details.Date_And_Time as [Date And Time] ,tspl_gate_entry_details.Challan_No as [Challan No] ,tspl_gate_entry_details.Challan_Date as [Challan Date] ,tspl_gate_entry_details.Dispatched_From_Mcc as [Dispatched From Mcc] ,tspl_gate_entry_details.location_Code as [Location Code] ,tspl_gate_entry_details.Location_Desc as [Location Desc] ,tspl_gate_entry_details.Vendor_Code as [Vendor Code] ,tspl_gate_entry_details.Vendor_Desc as [Vendor Desc] ,tspl_gate_entry_details.Item_Code as [Item Code] ,tspl_gate_entry_details.Item_Desc as [Item Desc] ,tspl_gate_entry_details.Qty_In_Kg as [Qty In Kg] ,tspl_gate_entry_details.snf_Per as [SNF Per] ,tspl_gate_entry_details.fat_per as [FAT Per] ,tspl_gate_entry_details.Created_By as [Created By] ,tspl_gate_entry_details.Created_Date as [Created Date] ,tspl_gate_entry_details.Modify_By as [Modify By] ,tspl_gate_entry_details.Modify_Date as [Modify Date] ,tspl_gate_entry_details.comp_code as [Company Code]  From tspl_gate_entry_details  "
            str = customFinder.getFinder("TNKRFNDGT", qry, whrcls, "TankerNo", curcode, "GateEntryNo")

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
    End Function


    Public Shared Function getUsersDefaultLocation(Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim strLoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location  from TSPL_USER_MASTER   where user_code='" & objCommonVar.CurrentUserCode & "'", trans))
        Return strLoc
    End Function
    Public Shared Function postData(ByVal strGateEntryNo As String, ByVal docType As String, ByVal formId As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            postData(strGateEntryNo, docType, formId, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function postData(ByVal strGateEntryNo As String, ByVal docType As String, ByVal formId As String, ByVal trans As SqlTransaction) As Boolean
        Try
            'Dim isPosted As Boolean = True
            If (clsCommon.myLen(strGateEntryNo) <= 0) Then
                Throw New Exception("Gate Entry No not found to Post")
            End If

            ' Dim obj As clsGateEntry = clsGateEntry.getData(strGateEntryNo, docType, NavigatorType.Current)
            Dim obj As clsGateEntry = clsGateEntry.getData(strGateEntryNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Gate_Entry_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            'trans = clsDBFuncationality.GetTransactin()
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.frmGateEntry, obj.location_Code, obj.Date_And_Time, trans)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))
            Dim AllowGateEntryAgainstPO As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowGateEntryAgainstPO, clsFixedParameterCode.AllowGateEntryAgainstPO, trans))
            Dim AllowManualPriceONBulkPO As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowManualPriceONBulkPO, clsFixedParameterCode.AllowManualPriceONBulkPO, trans))
            Dim AllowPriceMappingOnBulkSRNinChamberBulkProc As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPriceMappingOnBulkSRNinChamberBulkProc, clsFixedParameterCode.AllowPriceMappingOnBulkSRNinChamberBulkProc, trans))

            '--------------------
            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(formId, "tspl_gate_entry_details", "gate_entry_no", obj.Gate_Entry_No, trans)
            If TankerFromMaster = 1 AndAlso clsCommon.CompairString(obj.Doc_Type, "BulkProc") = CompairStringResult.Equal AndAlso AllowManualPriceONBulkPO = 0 Then
                Dim ZeroRate As String = ""
                Dim BulkProcPriceChartStandardRateWithZero As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcPriceChartStandardRateWithZero, clsFixedParameterCode.BulkProcPriceChartStandardRateWithZero, trans))
                If BulkProcPriceChartStandardRateWithZero = 1 Then
                    If clsCommon.CompairString(obj.Gate_Entry_Type, "J") = CompairStringResult.Equal Then
                        ZeroRate = " and Standard_Rate = 0 "
                    End If
                End If
                Dim strPriceCode As String = ""
                If AllowPriceMappingOnBulkSRNinChamberBulkProc = 0 Then
                    If AllowGateEntryAgainstPO = 0 AndAlso clsCommon.myLen(obj.PO_No) = 0 Then
                        strPriceCode = clsDBFuncationality.getSingleValue("select top 1 TSPL_Bulk_Price_MASTER.Price_Code from  TSPL_Bulk_Price_MASTER left outer join " & _
                                           "TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code " & _
                                           "where  TSPL_Bulk_Price_MASTER.Posted=1  and effective_Date<='" & clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MMM/yyyy hh:mm tt") & "' and  expirydate >= '" & clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MMM/yyyy hh:mm tt") & "' and TSPL_Bulk_Price_MASTER.Milk_Type_Code='" & obj.MIKL_TYPE_CODE & "'   and " & _
                                           "TSPL_Bulk_Price_MASTER.Price_Code in (select Tspl_Vendor_Price_Chart_mapping.PriceCode from " & _
                                           "Tspl_Vendor_Price_Chart_mapping where TSPL_VENDOR_PRICE_CHART_MAPPING.Posted=1 and " & _
                                           "Tspl_Vendor_Price_Chart_mapping.VendorCode='" & obj.Vendor_Code & "' " & ZeroRate & "  )  order by Price_Date desc", trans)
                        If clsCommon.myLen(strPriceCode) = 0 Then
                            Throw New Exception("Please create price chart for Vendor : " + obj.Vendor_Code)
                        End If

                    Else
                        strPriceCode = clsDBFuncationality.getSingleValue("select Price_Code from TSPL_PO_BULK_MASTER where PO_No='" & obj.PO_No & "'", trans)
                    End If
                    clsGateEntryPriceDetails.SaveData(obj.Gate_Entry_No, trans, strPriceCode)
                End If
            End If

            Dim strQry As String = " update tspl_gate_entry_details set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where gate_entry_no='" & strGateEntryNo & "'"
            clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strGateEntryNo), "TSPL_GATE_ENTRY_DETAILS", "Gate_Entry_No", trans)

            If TankerFromMaster = 1 Then
                CreateSMSEmailContent(clsUserMgtCode.frmGateEntry, obj, trans)
            End If
            ' done by priti BHA/21/06/18-000074 
            If obj.Amount > 0 And clsCommon.myLen(obj.Transpoter_Id) > 0 Then
                CreateProvision(obj, trans)
            End If
          
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Shared Sub CreateSMSEmailContent(ByVal Form_ID As String, ByVal obj As clsGateEntry, ByVal trans As SqlTransaction)
        '' create sms content
        Dim dtSMSEmail As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,EMail_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmGateEntry + "'", trans)
        Dim strSMSContent As String = ""
        Dim strEMailContent As String = ""
        If dtSMSEmail.Rows.Count > 0 Then
            strSMSContent = clsCommon.myCstr(dtSMSEmail.Rows(0).Item("SMS_Text"))
            strEMailContent = clsCommon.myCstr(dtSMSEmail.Rows(0).Item("EMail_Text"))
        End If
        Dim strSupplierName = clsCommon.myCstr(clsSupplierMaster.getSupplierName(obj.Supplier_Code, trans))
        Dim strMilkTypeName = clsCommon.myCstr(clsMilkTypeMaster.getMilkTypeName(obj.MIKL_TYPE_CODE, trans))
        'SMSCode Start
        If clsCommon.myLen(strSMSContent) > 0 Then
            Dim objSMSH As New clsSMSHead()
            objSMSH.SMS_Text = strSMSContent
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MMM/yyyy"))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(obj.Gate_Entry_No))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Type, clsCommon.myCstr(obj.Doc_Type))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.TotalQty, clsCommon.myCdbl(obj.TotalQty_In_Kg))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Vendor_Code, clsCommon.myCstr(obj.Vendor_Code))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Vendor_Name, clsCommon.myCstr(obj.Vendor_Desc))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Code, clsCommon.myCstr(obj.location_Code))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Name, clsCommon.myCstr(obj.Location_Desc))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.SupplierCode, clsCommon.myCstr(obj.Supplier_Code))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.SupplierName, strSupplierName)
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.TankerNo, clsCommon.myCstr(obj.Tanker_No))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MilkTypeCode, clsCommon.myCstr(obj.MIKL_TYPE_CODE))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MilkTypeName, strMilkTypeName)
            CreateSMSContent(objSMSH.SMS_Text, trans)
            'obj1.SMS_Content = objSMSH.SMS_Text
        End If

        'email content Start
        If clsCommon.myLen(strEMailContent) > 0 Then
            Dim objEmailH As New clsEMailHead
            objEmailH.Email_Text = strEMailContent
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MMM/yyyy"))
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(obj.Gate_Entry_No))
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Type, clsCommon.myCstr(obj.Doc_Type))
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.TotalQty, clsCommon.myCdbl(obj.TotalQty_In_Kg))
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Vendor_Code, clsCommon.myCstr(obj.Vendor_Code))
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Vendor_Name, clsCommon.myCstr(obj.Vendor_Desc))
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Code, clsCommon.myCstr(obj.location_Code))
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Name, clsCommon.myCstr(obj.Location_Desc))
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.SupplierCode, clsCommon.myCstr(obj.Supplier_Code))
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.SupplierName, strSupplierName)
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.TankerNo, clsCommon.myCstr(obj.Tanker_No))
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MilkTypeCode, clsCommon.myCstr(obj.MIKL_TYPE_CODE))
            objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MilkTypeName, strMilkTypeName)
            CreateEmailContent(objEmailH.Email_Text, trans)
        End If

    End Sub
    Public Shared Sub CreateSMSContent(ByVal strSMSContent As String, ByVal trans As SqlTransaction)
        If clsCommon.myLen(strSMSContent) > 0 Then
            Dim objSMSH As New clsSMSHead()
            objSMSH.SMS_Text = strSMSContent
            objSMSH.arrMobilNo = New List(Of String)()
            objSMSH.SaveData(clsUserMgtCode.frmGateEntry, objSMSH, trans)
            objSMSH = Nothing
        End If
    End Sub
    Public Shared Sub CreateEmailContent(ByVal strEmailContent As String, ByVal trans As SqlTransaction)
        'MailCode Start
        If clsCommon.myLen(strEmailContent) > 0 Then
            Dim qry As String = "SELECT EMail_Subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmGateEntry + "'"
            Dim EmailSubject As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT EMail_Subject from TSPL_ES_Content where Form_ID='" & clsUserMgtCode.frmGateEntry & "'", trans))
            Dim objSMSH As New clsEMailHead()
            objSMSH.Email_Text = strEmailContent
            objSMSH.Email_Subject = EmailSubject
            objSMSH.arrEMail = New List(Of String)()
            objSMSH.SaveData(clsUserMgtCode.frmGateEntry, objSMSH, trans)
            objSMSH = Nothing
        End If
    End Sub
    '==================Added by preeti 
    Public Shared Function deleteData(ByVal strGateEntryNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            deleteData(strGateEntryNo, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try

    End Function

    Public Shared Function deleteData(ByVal strGateEntryNo As String, ByVal trans As SqlTransaction) As Boolean
        Return deleteData(True, strGateEntryNo, trans)
    End Function

    Public Shared Function deleteData(ByVal isCheckForPost As Boolean, ByVal strGateEntryNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If isCheckForPost Then
                Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select isPosted from tspl_gate_entry_details Where Gate_Entry_No='" + strGateEntryNo + "'", trans))
                If Status = 1 Then
                    Throw New Exception("This document is already posted.")
                End If
            End If


            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select location_Code,Date_And_Time from tspl_gate_entry_details where Gate_Entry_No='" + strGateEntryNo + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.frmGateEntry, clsCommon.myCstr(dt.Rows(0)("location_Code")), clsCommon.myCDate(dt.Rows(0)("Date_And_Time")), trans)

            End If


            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strGateEntryNo), "TSPL_GATE_ENTRY_DETAILS", "Gate_Entry_No", "TSPL_Bulk_Gate_Entry_Chember_Details", "GE_Code", "TSPL_Gate_Entry_Chember_Details", "GE_Code", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strGateEntryNo), "TSPL_Gate_Entry_Price_Chart", "GE_Code", trans)
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_Gate_Entry_Price_Chart where  GE_Code='" & strGateEntryNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_Gate_Entry_Chember_Details where  GE_Code='" & strGateEntryNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_Bulk_Gate_Entry_Chember_Details where  GE_Code='" & strGateEntryNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from tspl_gate_entry_details where  gate_entry_no='" & strGateEntryNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            Return isDeleted
        Catch ex As Exception

            Return False
        End Try
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsGateEntry
        Dim obj As New clsGateEntry
        Try
            Dim qst As String = " select *   From tspl_gate_entry_details   where 1=1  "
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    qst = qst & " and location_code in (" & objCommonVar.strCurrUserLocations & ") "
                End If
            End If
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and tspl_gate_entry_details.gate_entry_no in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and tspl_gate_entry_details.gate_entry_no in (select min(gate_entry_no ) from tspl_gate_entry_details where gate_entry_no  >'" + strCode + "' and  location_code in (" & objCommonVar.strCurrUserLocations & ") )"
                Case NavigatorType.First
                    qst += " and tspl_gate_entry_details.gate_entry_no in (select MIN(gate_entry_no ) from tspl_gate_entry_details  where  location_code in (" & objCommonVar.strCurrUserLocations & "))"
                Case NavigatorType.Last
                    qst += " and tspl_gate_entry_details.gate_entry_no in (select Max(gate_entry_no ) from tspl_gate_entry_details where  location_code in (" & objCommonVar.strCurrUserLocations & "))"
                Case NavigatorType.Previous
                    qst += " and tspl_gate_entry_details.gate_entry_no in (select Max(gate_entry_no ) from tspl_gate_entry_details where gate_entry_no  <'" + strCode + "' ) and location_code in (" & objCommonVar.strCurrUserLocations & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.No_Of_CAN = clsCommon.myCdbl(dt.Rows(0)("No_Of_CAN"))
                obj.Weight = clsCommon.myCdbl(dt.Rows(0)("Weight"))
                obj.Transpoter_Id = clsCommon.myCstr(dt.Rows(0)("Transpoter_Id"))
                obj.AcknowEntryDocument_No = clsCommon.myCstr(dt.Rows(0)("AcknowEntryDocument_No"))
                obj.Bulk_ROUTE_NO = clsCommon.myCstr(dt.Rows(0)("Bulk_ROUTE_NO"))
                obj.Distance = clsCommon.myCdbl(dt.Rows(0)("Distance"))
                obj.Rate = clsCommon.myCdbl(dt.Rows(0)("Rate"))
                obj.Amount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                obj.ProvisionNo = clsCommon.myCstr(dt.Rows(0)("ProvisionNo"))
                obj.NO_OF_CHAMBER = clsCommon.myCdbl(dt.Rows(0)("NO_OF_CHAMBER"))
                obj.IsAgainstJobWork = clsCommon.myCdbl(dt.Rows(0)("IsAgainstJobWork"))
                obj.Sublocation_Code = clsCommon.myCstr(dt.Rows(0)("Sublocation_Code"))
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
                obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
                obj.Date_And_Time = clsCommon.myCDate(dt.Rows(0)("Date_And_Time"))
                obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
                obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"))
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
                obj.Against_Gate_Out = clsCommon.myCstr(dt.Rows(0)("Against_Gate_Out"))
                obj.IsAgainstGateOut = clsCommon.myCdbl(dt.Rows(0)("IsAgainstGateOut"))
                obj.ROUTE_NO = clsCommon.myCstr(dt.Rows(0)("ROUTE_NO"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If
                obj.Dispatched_From_Mcc = clsCommon.myCstr(dt.Rows(0)("Dispatched_From_Mcc"))
                obj.location_Code = clsCommon.myCstr(dt.Rows(0)("location_Code"))
                obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
                obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                obj.Vendor_Desc = clsCommon.myCstr(dt.Rows(0)("Vendor_Desc"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.Qty_In_Kg = clsCommon.myCdbl(dt.Rows(0)("Qty_In_Kg"))
                obj.snf_Per = clsCommon.myCdbl(dt.Rows(0)("snf_Per"))
                obj.fat_per = clsCommon.myCdbl(dt.Rows(0)("fat_per"))
                obj.TotalQty_In_Kg = clsCommon.myCdbl(dt.Rows(0)("TotalQty_In_Kg"))
                obj.Intimation_No = clsCommon.myCstr(dt.Rows(0)("Intimation_No"))
                obj.SealNo_Header = clsCommon.myCstr(dt.Rows(0)("SealNo_Header"))
                obj.Supplier_Code = clsCommon.myCstr(dt.Rows(0)("Supplier_Code"))
                obj.Dispatch_Centre_Code = clsCommon.myCstr(dt.Rows(0)("Dispatch_Centre_Code"))
                obj.MIKL_TYPE_CODE = clsCommon.myCstr(dt.Rows(0)("MIKL_TYPE_CODE"))
                obj.Intimation_Status = clsCommon.myCstr(dt.Rows(0)("Intimation_Status"))
                obj.Gate_Entry_Type = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_Type"))
                obj.Seal_Status = clsCommon.myCstr(dt.Rows(0)("Seal_Status"))
                obj.Tanker_Return = clsCommon.myCdbl(dt.Rows(0)("Tanker_Return"))
                obj.PO_No = clsCommon.myCstr(dt.Rows(0)("PO_No"))
                obj.IsNetWeight = clsCommon.myCdbl(dt.Rows(0)("IsNetWeight"))
                obj.Reference_No = clsCommon.myCstr(dt.Rows(0)("Reference_No"))
                obj.openingKM = clsCommon.myCdbl(dt.Rows(0)("openingKM"))
                obj.closingKM = clsCommon.myCdbl(dt.Rows(0)("closingKM"))
                obj.Toll_Amount = clsCommon.myCdbl(dt.Rows(0)("Toll_Amount"))
                If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("Closing_Date"))) > 0 Then
                    obj.Closing_Date = clsCommon.myCDate(dt.Rows(0)("Closing_Date"))
                End If
                obj.Arr1 = clsChemberNoDetails.GetData(obj.Gate_Entry_No, trans)
                obj.Arr = clsGateEntryChemberNoDetails.GetData(obj.Gate_Entry_No, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal docType As String, ByVal navtype As NavigatorType) As clsGateEntry
        Dim obj As New clsGateEntry
        Try
            Dim qst As String = " select *   From tspl_gate_entry_details   where 1=1 and doc_type='" & docType & "'"
            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO() AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " and location_code in (" & objCommonVar.strCurrUserLocations & ") "
            End If
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and tspl_gate_entry_details.gate_entry_no in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and tspl_gate_entry_details.gate_entry_no in (select min(gate_entry_no ) from tspl_gate_entry_details where gate_entry_no  >'" + strCode + "' and doc_type='" & docType & "' " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and tspl_gate_entry_details.gate_entry_no in (select MIN(gate_entry_no ) from tspl_gate_entry_details where doc_type='" & docType & "' " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and tspl_gate_entry_details.gate_entry_no in (select Max(gate_entry_no ) from tspl_gate_entry_details where doc_type='" & docType & "' " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and tspl_gate_entry_details.gate_entry_no in (select Max(gate_entry_no ) from tspl_gate_entry_details where gate_entry_no  <'" + strCode + "' and doc_type='" & docType & "' " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.No_Of_CAN = clsCommon.myCdbl(dt.Rows(0)("No_Of_CAN"))
                obj.Weight = clsCommon.myCdbl(dt.Rows(0)("Weight"))
                obj.Transpoter_Id = clsCommon.myCstr(dt.Rows(0)("Transpoter_Id"))
                obj.Bulk_ROUTE_NO = clsCommon.myCstr(dt.Rows(0)("Bulk_ROUTE_NO"))
                obj.AcknowEntryDocument_No = clsCommon.myCstr(dt.Rows(0)("AcknowEntryDocument_No"))
                obj.Distance = clsCommon.myCdbl(dt.Rows(0)("Distance"))
                obj.Rate = clsCommon.myCdbl(dt.Rows(0)("Rate"))
                obj.Amount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                obj.ProvisionNo = clsCommon.myCstr(dt.Rows(0)("ProvisionNo"))
                obj.NO_OF_CHAMBER = clsCommon.myCdbl(dt.Rows(0)("NO_OF_CHAMBER"))
                obj.IsAgainstJobWork = clsCommon.myCdbl(dt.Rows(0)("IsAgainstJobWork"))
                obj.Sublocation_Code = clsCommon.myCstr(dt.Rows(0)("Sublocation_Code"))
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
                obj.Reference_No = clsCommon.myCstr(dt.Rows(0)("Reference_No"))
                obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
                obj.Date_And_Time = clsCommon.myCDate(dt.Rows(0)("Date_And_Time"))
                obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
                obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"))
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                obj.ROUTE_NO = clsCommon.myCstr(dt.Rows(0)("ROUTE_NO"))
                obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
                obj.Against_Gate_Out = clsCommon.myCstr(dt.Rows(0)("Against_Gate_Out"))
                obj.IsAgainstGateOut = clsCommon.myCdbl(dt.Rows(0)("IsAgainstGateOut"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If
                obj.Dispatched_From_Mcc = clsCommon.myCstr(dt.Rows(0)("Dispatched_From_Mcc"))
                obj.location_Code = clsCommon.myCstr(dt.Rows(0)("location_Code"))
                obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
                obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                obj.Vendor_Desc = clsCommon.myCstr(dt.Rows(0)("Vendor_Desc"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.Qty_In_Kg = clsCommon.myCdbl(dt.Rows(0)("Qty_In_Kg"))
                obj.snf_Per = clsCommon.myCdbl(dt.Rows(0)("snf_Per"))
                obj.fat_per = clsCommon.myCdbl(dt.Rows(0)("fat_per"))
                obj.TotalQty_In_Kg = clsCommon.myCdbl(dt.Rows(0)("TotalQty_In_Kg"))
                obj.Tanker_Return = clsCommon.myCdbl(dt.Rows(0)("Tanker_Return"))
                obj.Intimation_No = clsCommon.myCstr(dt.Rows(0)("Intimation_No"))
                obj.SealNo_Header = clsCommon.myCstr(dt.Rows(0)("SealNo_Header"))
                obj.Supplier_Code = clsCommon.myCstr(dt.Rows(0)("Supplier_Code"))
                obj.Dispatch_Centre_Code = clsCommon.myCstr(dt.Rows(0)("Dispatch_Centre_Code"))
                obj.MIKL_TYPE_CODE = clsCommon.myCstr(dt.Rows(0)("MIKL_TYPE_CODE"))
                obj.Intimation_Status = clsCommon.myCstr(dt.Rows(0)("Intimation_Status"))
                obj.Gate_Entry_Type = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_Type"))
                obj.Seal_Status = clsCommon.myCstr(dt.Rows(0)("Seal_Status"))
                obj.PO_No = clsCommon.myCstr(dt.Rows(0)("PO_No"))
                obj.IsNetWeight = clsCommon.myCdbl(dt.Rows(0)("IsNetWeight"))
                obj.openingKM = clsCommon.myCdbl(dt.Rows(0)("openingKM"))
                obj.closingKM = clsCommon.myCdbl(dt.Rows(0)("closingKM"))
                obj.Toll_Amount = clsCommon.myCdbl(dt.Rows(0)("Toll_Amount"))
                If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("Closing_Date"))) > 0 Then
                    obj.Closing_Date = clsCommon.myCDate(dt.Rows(0)("Closing_Date"))
                End If
                obj.Arr1 = clsChemberNoDetails.GetData(obj.Gate_Entry_No, Nothing)
                obj.Arr = clsGateEntryChemberNoDetails.GetData(obj.Gate_Entry_No, Nothing)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal docType As String, ByVal navtype As NavigatorType, ByVal isUserCheck As Boolean) As clsGateEntry
        Dim obj As clsGateEntry = New clsGateEntry()
        If isUserCheck Then
            Return getData(strCode, docType, navtype)
        Else
            Try
                Dim qst As String = " select *   From tspl_gate_entry_details   where 1=1 and doc_type='" & docType & "' "
                Select Case navtype
                    Case NavigatorType.Current
                        qst += " and tspl_gate_entry_details.gate_entry_no in ('" + strCode + "') "
                    Case NavigatorType.Next
                        qst += " and tspl_gate_entry_details.gate_entry_no in (select min(gate_entry_no ) from tspl_gate_entry_details where gate_entry_no  >'" + strCode + "' and doc_type='" & docType & "' )"
                    Case NavigatorType.First
                        qst += " and tspl_gate_entry_details.gate_entry_no in (select MIN(gate_entry_no ) from tspl_gate_entry_details where doc_type='" & docType & "' )"
                    Case NavigatorType.Last
                        qst += " and tspl_gate_entry_details.gate_entry_no in (select Max(gate_entry_no ) from tspl_gate_entry_details where doc_type='" & docType & "' )"
                    Case NavigatorType.Previous
                        qst += " and tspl_gate_entry_details.gate_entry_no in (select Max(gate_entry_no ) from tspl_gate_entry_details where gate_entry_no  <'" + strCode + "' and doc_type='" & docType & "' )"
                End Select
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    obj.No_Of_CAN = clsCommon.myCdbl(dt.Rows(0)("No_Of_CAN"))
                    obj.Weight = clsCommon.myCdbl(dt.Rows(0)("Weight"))
                    obj.Transpoter_Id = clsCommon.myCstr(dt.Rows(0)("Transpoter_Id"))
                    obj.Bulk_ROUTE_NO = clsCommon.myCstr(dt.Rows(0)("Bulk_ROUTE_NO"))
                    obj.AcknowEntryDocument_No = clsCommon.myCstr(dt.Rows(0)("AcknowEntryDocument_No"))
                    obj.Distance = clsCommon.myCdbl(dt.Rows(0)("Distance"))
                    obj.Rate = clsCommon.myCdbl(dt.Rows(0)("Rate"))
                    obj.Amount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                    obj.ProvisionNo = clsCommon.myCstr(dt.Rows(0)("ProvisionNo"))
                    obj.NO_OF_CHAMBER = clsCommon.myCdbl(dt.Rows(0)("NO_OF_CHAMBER"))
                    obj.IsAgainstJobWork = clsCommon.myCdbl(dt.Rows(0)("IsAgainstJobWork"))
                    obj.Sublocation_Code = clsCommon.myCstr(dt.Rows(0)("Sublocation_Code"))
                    obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
                    obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
                    obj.Date_And_Time = clsCommon.myCDate(dt.Rows(0)("Date_And_Time"))
                    obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
                    obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"))
                    obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                    obj.ROUTE_NO = clsCommon.myCstr(dt.Rows(0)("ROUTE_NO"))
                    obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
                    obj.Against_Gate_Out = clsCommon.myCstr(dt.Rows(0)("Against_Gate_Out"))
                    obj.IsAgainstGateOut = clsCommon.myCdbl(dt.Rows(0)("IsAgainstGateOut"))
                    If obj.isPosted = 1 Then
                        obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                    End If
                    'If clsCommon.CompairString(obj.Doc_Type, "MccProc") = CompairStringResult.Equal Then
                    obj.Dispatched_From_Mcc = clsCommon.myCstr(dt.Rows(0)("Dispatched_From_Mcc"))
                    'Else
                    obj.location_Code = clsCommon.myCstr(dt.Rows(0)("location_Code"))
                    obj.Location_Desc = clsCommon.myCstr(clsLocation.GetName(obj.location_Code, Nothing))
                    obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                    obj.Vendor_Desc = clsCommon.myCstr(clsVendorMaster.GetName(obj.Vendor_Code, Nothing))
                    'End If
                    obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                    obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                    obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
                    obj.Qty_In_Kg = clsCommon.myCdbl(dt.Rows(0)("Qty_In_Kg"))
                    obj.snf_Per = clsCommon.myCdbl(dt.Rows(0)("snf_Per"))
                    obj.fat_per = clsCommon.myCdbl(dt.Rows(0)("fat_per"))
                    obj.TotalQty_In_Kg = clsCommon.myCdbl(dt.Rows(0)("TotalQty_In_Kg"))
                    obj.Tanker_Return = clsCommon.myCdbl(dt.Rows(0)("Tanker_Return"))
                    obj.Intimation_No = clsCommon.myCstr(dt.Rows(0)("Intimation_No"))
                    obj.SealNo_Header = clsCommon.myCstr(dt.Rows(0)("SealNo_Header"))
                    obj.Supplier_Code = clsCommon.myCstr(dt.Rows(0)("Supplier_Code"))
                    obj.Dispatch_Centre_Code = clsCommon.myCstr(dt.Rows(0)("Dispatch_Centre_Code"))
                    obj.MIKL_TYPE_CODE = clsCommon.myCstr(dt.Rows(0)("MIKL_TYPE_CODE"))
                    obj.Intimation_Status = clsCommon.myCstr(dt.Rows(0)("Intimation_Status"))
                    obj.Gate_Entry_Type = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_Type"))
                    obj.Seal_Status = clsCommon.myCstr(dt.Rows(0)("Seal_Status"))
                    obj.PO_No = clsCommon.myCstr(dt.Rows(0)("PO_No"))
                    obj.IsNetWeight = clsCommon.myCdbl(dt.Rows(0)("IsNetWeight"))
                    obj.openingKM = clsCommon.myCdbl(dt.Rows(0)("openingKM"))
                    obj.closingKM = clsCommon.myCdbl(dt.Rows(0)("closingKM"))
                    obj.Toll_Amount = clsCommon.myCdbl(dt.Rows(0)("Toll_Amount"))
                    If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("Closing_Date"))) > 0 Then
                        obj.Closing_Date = clsCommon.myCDate(dt.Rows(0)("Closing_Date"))
                    End If
                    obj.Arr1 = clsChemberNoDetails.GetData(obj.Gate_Entry_No, Nothing)
                    obj.Arr = clsGateEntryChemberNoDetails.GetData(obj.Gate_Entry_No, Nothing)
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End If
        Return obj
    End Function
    Private Shared Function CreateProvision(ByVal objTrans As clsGateEntry, ByVal trans As SqlTransaction) As Boolean
        Dim obj As New clsProvisionEntry()
        Try
            obj = New clsProvisionEntry()
            obj.isNewEntry = True
            obj.Doc_Date = objTrans.Date_And_Time
            obj.Vendor_Code = objTrans.Transpoter_Id
            obj.Vendor_Desc = clsDBFuncationality.getSingleValue("select Transpoter_Name from TSPL_TRANSPORT__TANKER_DETAILS where Transpoter_Id='" & objTrans.Transpoter_Id & "'", trans)
            obj.Vendor_Type = "Transporter For Gate Entry"
            obj.Status = "No"
            obj.Ref_Doc_No = objTrans.Gate_Entry_No
            obj.Prov_type = "Freight"
            obj.Amount = objTrans.Amount
            obj.Prog_Code = clsUserMgtCode.frmGateEntry
            obj.Prov_Month = Month(objTrans.Date_And_Time)
            obj.Prov_Year = Year(objTrans.Date_And_Time)
            obj.Loc_Code = objTrans.location_Code
            obj.Loc_Desc = clsLocation.GetName(obj.Loc_Code, trans)
            obj.Freight_Type = ""
            obj.FixedCharge = 0
            obj.EmptyCharge = 0
            If clsProvisionEntry.SaveData(obj, trans) Then
                If clsProvisionEntry.PostData(obj.Doc_No, trans, False) Then
                    Dim qry = "Update Tspl_Gate_Entry_Details set ProvisionNo='" & obj.Doc_No & "' where Gate_Entry_No='" & objTrans.Gate_Entry_No & "' "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            obj = Nothing
        End Try

    End Function
    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strDocNo) <= 0 Then
                Throw New Exception("Please select a Gate Entry No")
            End If


            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select location_Code,Date_And_Time from tspl_gate_entry_details where Gate_Entry_No='" + strDocNo + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.frmGateEntry, clsCommon.myCstr(dt.Rows(0)("location_Code")), clsCommon.myCDate(dt.Rows(0)("Date_And_Time")), trans)

            End If
            Dim Qry As String = "select isPosted from Tspl_Gate_Entry_Details where Gate_Entry_No='" + strDocNo + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If
            Dim isUsed As Integer = clsDBFuncationality.getSingleValue("select SUM(row_Count ) from (select COUNT(*) as row_Count from  TSPL_Weighment_Detail where gate_entry_no='" & strDocNo & "' union all select COUNT(*) as row_Count from tspl_quality_check where gate_entry_no='" & strDocNo & "') xx ", trans)
            If isUsed > 0 Then
                Throw New Exception("Gate Entry No is in use")
            End If
            Qry = "Update Tspl_Gate_Entry_Details set isPosted = 0,Posting_Date=null where gate_entry_no='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strDocNo), "TSPL_GATE_ENTRY_DETAILS", "Gate_Entry_No", trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
Public Class customFinder
    Public Shared Function getFinder(ByVal reportId As String, ByVal qry As String, ByVal whrCls As String, ByVal currColCode As String, ByVal currColValue As String, ByVal ReqColCode As String) As String
        Dim str As String = ""
        Try
            Dim dt As DataRow
            qry = qry & " where 1=1 "
            If clsCommon.myLen(whrCls) > 0 Then
                qry = qry & " and  " & whrCls
            End If
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from ( " & qry & ") xx ")) > 0 Then
                dt = clsCommon.ShowSelectFormForRow(reportId, qry, currColCode, currColValue)
                If dt IsNot Nothing Then
                    str = dt(ReqColCode).ToString
                Else
                    str = ""
                End If
            Else
                Throw New Exception("No Data Found.")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
    End Function
End Class

Public Class clsChemberNoDetails
    ' done by priti BHA/16/07/18-000165 for qty round off pblm in bharat
#Region "Variables"
    Public GE_Code As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Desc As String = Nothing
    Public Chember_Code As Integer = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public UOM As String = Nothing
    Public Chamber_Desc As String = Nothing
    Public Chamber_Qty As Double = 0
    Public snf_Per As Double = 0
    Public fat_per As Double = 0
    Public DIP_Status As String = Nothing
    Public Sample_Lifted As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsChemberNoDetails), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim sQuery As String = "Delete from TSPL_Bulk_Gate_Entry_Chember_Details where GE_COde='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            For Each obj As clsChemberNoDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "GE_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommon.AddColumnsForChange(coll, "Chember_Code", obj.Chember_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bulk_Gate_Entry_Chember_Details", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsChemberNoDetails)
        Dim arr As List(Of clsChemberNoDetails) = Nothing
        Dim qry As String
        qry = "select GE_Code,TSPL_Bulk_Gate_Entry_Chember_Details.Vendor_Code,Chember_Code,Vendor_Name from " & _
            "TSPL_Bulk_Gate_Entry_Chember_Details left join tspl_vendor_master on tspl_vendor_master.vendor_COde=" _
            & " TSPL_Bulk_Gate_Entry_Chember_Details.vendor_code  where TSPL_Bulk_Gate_Entry_Chember_Details.GE_Code='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsChemberNoDetails)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsChemberNoDetails = New clsChemberNoDetails()
                obj.GE_Code = clsCommon.myCstr(dr("GE_Code"))
                obj.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                obj.Vendor_Desc = clsCommon.myCstr(dr("Vendor_Name"))
                obj.Chember_Code = clsCommon.myCdbl(dr("Chember_Code"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class
Public Class clsGateEntryChemberNoDetails
#Region "Variables"
    Public GE_Code As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public UOM As String = Nothing
    Public Chamber_Desc As String = Nothing
    Public Chamber_Qty As Double = 0
    Public snf_Per As Double = 0
    Public fat_per As Double = 0
    Public DIP_Status As String = Nothing
    Public Sample_Lifted As String = Nothing
    Public MIKL_TYPE_CODE As String = Nothing
    Public Dip_value As String = Nothing
    Public Seal_No As String = Nothing
    Public ManualRate As Double = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsGateEntryChemberNoDetails), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim sQuery As String = "Delete from TSPL_Gate_Entry_Chember_Details where GE_COde='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            For Each obj As clsGateEntryChemberNoDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "GE_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "Chamber_Desc", obj.Chamber_Desc)
                clsCommon.AddColumnsForChange(coll, "Chamber_Qty", obj.Chamber_Qty)
                clsCommon.AddColumnsForChange(coll, "snf_Per", obj.snf_Per)
                clsCommon.AddColumnsForChange(coll, "fat_per", obj.fat_per)
                clsCommon.AddColumnsForChange(coll, "DIP_Status", obj.DIP_Status)
                clsCommon.AddColumnsForChange(coll, "Sample_Lifted", obj.Sample_Lifted)
                clsCommon.AddColumnsForChange(coll, "MIKL_TYPE_CODE", obj.MIKL_TYPE_CODE, True)
                clsCommon.AddColumnsForChange(coll, "Dip_value", obj.Dip_value)
                clsCommon.AddColumnsForChange(coll, "Seal_No", obj.Seal_No)
                clsCommon.AddColumnsForChange(coll, "ManualRate", obj.ManualRate)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Gate_Entry_Chember_Details", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsGateEntryChemberNoDetails)
        Dim arr As List(Of clsGateEntryChemberNoDetails) = Nothing
        Dim qry As String
        qry = "select * from " &
            "TSPL_Gate_Entry_Chember_Details where TSPL_Gate_Entry_Chember_Details.GE_Code='" + strCode + "' and Chamber_Qty >0  order by Line_No asc "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsGateEntryChemberNoDetails)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsGateEntryChemberNoDetails = New clsGateEntryChemberNoDetails()
                obj.GE_Code = clsCommon.myCstr(dr("GE_Code"))
                obj.Line_No = clsCommon.myCstr(dr("Line_No"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.UOM = clsCommon.myCstr(dr("UOM"))
                obj.snf_Per = clsCommon.myCdbl(dr("snf_Per"))
                obj.fat_per = clsCommon.myCdbl(dr("fat_per"))
                obj.Chamber_Qty = clsCommon.myCdbl(dr("Chamber_Qty"))
                obj.Chamber_Desc = clsCommon.myCstr(dr("Chamber_Desc"))
                obj.DIP_Status = clsCommon.myCstr(dr("DIP_Status"))
                obj.Sample_Lifted = clsCommon.myCstr(dr("Sample_Lifted"))
                obj.MIKL_TYPE_CODE = clsCommon.myCstr(dr("MIKL_TYPE_CODE"))
                obj.Dip_value = clsCommon.myCstr(dr("Dip_value"))
                obj.Seal_No = clsCommon.myCstr(dr("Seal_No"))
                obj.ManualRate = clsCommon.myCdbl(dr("ManualRate"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class
Public Class clsGateEntryPriceDetails
#Region "Variables"
    Public GE_Code As String = Nothing
    Public Line_No As Integer = 0
    Public Price_Code As String = Nothing
    Public Milk_Grade_code As String = Nothing
    Public VendorCode As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal trans As SqlTransaction, ByVal strPriceCode As String) As Boolean
        Dim sQuery As String = "Delete from TSPL_Gate_Entry_Price_Chart where GE_COde='" & strDocNo & "'"
        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)

        sQuery = "select TSPL_Bulk_Price_MASTER.Price_Code,Milk_Grade_code from TSPL_Bulk_Price_MASTER left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code where TSPL_Bulk_Price_MASTER.Price_Code='" & strPriceCode & "'"
        Dim dt = clsDBFuncationality.GetDataTable(sQuery, trans)
        For Each dr As DataRow In dt.Rows
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "GE_Code", strDocNo)
            clsCommon.AddColumnsForChange(coll, "Price_Code", strPriceCode)
            clsCommon.AddColumnsForChange(coll, "Milk_Grade_code", clsCommon.myCstr(dr("Milk_Grade_code")))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Gate_Entry_Price_Chart", OMInsertOrUpdate.Insert, "", trans)
        Next

        Return True
    End Function

End Class