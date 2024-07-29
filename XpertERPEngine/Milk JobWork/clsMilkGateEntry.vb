
Imports System.Data.SqlClient
Imports common
Public Class clsMilkGateEntry

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
    Public Shared Function saveData(ByVal obj As clsMilkGateEntry, ByVal trans As SqlTransaction, Optional ByVal isHistory As Boolean = False) As Boolean
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.MilkJobWork, clsUserMgtCode.FrmMilkGateEntry, obj.location_Code, obj.Date_And_Time, trans)

            Dim issaved As Boolean = True
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", obj.Gate_Entry_No)
            clsCommon.AddColumnsForChange(coll, "Doc_Type", obj.Doc_Type)
            clsCommon.AddColumnsForChange(coll, "Date_And_Time", clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Challan_No", obj.Challan_No)
            clsCommon.AddColumnsForChange(coll, "Challan_Date", clsCommon.GetPrintDate(obj.Challan_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No)
            clsCommon.AddColumnsForChange(coll, "isPosted", obj.isPosted)
            If obj.isPosted = 1 Then
                clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Posting_Date, "dd/MMM/yyyy"))
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
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", clsCommon.myCstr(obj.comp_code))
            If obj.isNewEntry Or isHistory Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isHistory, "Tspl_Milk_Gate_Entry_Details_History", "TSPL_MILK_GATE_ENTRY_DETAILS"), OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_GATE_ENTRY_DETAILS", OMInsertOrUpdate.Update, "TSPL_MILK_GATE_ENTRY_DETAILS.gate_entry_no='" + obj.Gate_Entry_No + "'", trans)
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try

            Dim qry As String = " select TSPL_MILK_GATE_ENTRY_DETAILS.Gate_Entry_No as [GateEntryNo] ,TSPL_MILK_GATE_ENTRY_DETAILS.Doc_Type as [Doc Type] ,TSPL_MILK_GATE_ENTRY_DETAILS.Date_And_Time as [Date And Time] ,TSPL_MILK_GATE_ENTRY_DETAILS.Challan_No as [Challan No] ,TSPL_MILK_GATE_ENTRY_DETAILS.Challan_Date as [Challan Date] ,TSPL_MILK_GATE_ENTRY_DETAILS.Tanker_No as [Tanker No] ,TSPL_MILK_GATE_ENTRY_DETAILS.Dispatched_From_Mcc as [Dispatched From Mcc] ,case when TSPL_MILK_GATE_ENTRY_DETAILS.isPosted=0 then 'NO' else 'YES' end as [Posting Status],TSPL_MILK_GATE_ENTRY_DETAILS.location_Code as [Location Code] ,TSPL_MILK_GATE_ENTRY_DETAILS.Location_Desc as [Location Desc] ,TSPL_MILK_GATE_ENTRY_DETAILS.Vendor_Code as [Vendor Code] ,TSPL_MILK_GATE_ENTRY_DETAILS.Vendor_Desc as [Vendor Desc] ,TSPL_MILK_GATE_ENTRY_DETAILS.Item_Code as [Item Code] ,TSPL_MILK_GATE_ENTRY_DETAILS.Item_Desc as [Item Desc] ,TSPL_MILK_GATE_ENTRY_DETAILS.Qty_In_Kg as [Qty In Kg] ,TSPL_MILK_GATE_ENTRY_DETAILS.snf_Per as [SNF Per] ,TSPL_MILK_GATE_ENTRY_DETAILS.fat_per as [FAT Per] ,TSPL_MILK_GATE_ENTRY_DETAILS.Created_By as [Created By] ,TSPL_MILK_GATE_ENTRY_DETAILS.Created_Date as [Created Date] ,TSPL_MILK_GATE_ENTRY_DETAILS.Modify_By as [Modify By] ,TSPL_MILK_GATE_ENTRY_DETAILS.Modify_Date as [Modify Date] ,TSPL_MILK_GATE_ENTRY_DETAILS.comp_code as [Company Code], case when  ISNULL(tspl_gate_out.Doc_No ,'')='' then 'Pending For Gate Out' else ISNULL(tspl_gate_out.Doc_No ,'') end as [Gate Out No]  From TSPL_MILK_GATE_ENTRY_DETAILS left outer join TSPL_Gate_Out on TSPL_Gate_Out.Gate_Entry_No=TSPL_MILK_GATE_ENTRY_DETAILS.Gate_Entry_No "
            str = clsCommon.ShowSelectForm("GATEENTRY", qry, "GateEntryNo", whrcls, curcode, "GateEntryNo", isButtonClicked)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function getTankerFinder(ByVal whrcls As String, ByVal curcode As String) As String
        Dim str As String = ""
        Try
            'Dim dt As DataRow

            Dim qry As String = " select TSPL_MILK_GATE_ENTRY_DETAILS.Tanker_No as [TankerNo] , TSPL_MILK_GATE_ENTRY_DETAILS.Gate_Entry_No as [GateEntryNo] ,TSPL_MILK_GATE_ENTRY_DETAILS.Doc_Type as [Doc Type] ,TSPL_MILK_GATE_ENTRY_DETAILS.Date_And_Time as [Date And Time] ,TSPL_MILK_GATE_ENTRY_DETAILS.Challan_No as [Challan No] ,TSPL_MILK_GATE_ENTRY_DETAILS.Challan_Date as [Challan Date] ,TSPL_MILK_GATE_ENTRY_DETAILS.Dispatched_From_Mcc as [Dispatched From Mcc] ,TSPL_MILK_GATE_ENTRY_DETAILS.location_Code as [Location Code] ,TSPL_MILK_GATE_ENTRY_DETAILS.Location_Desc as [Location Desc] ,TSPL_MILK_GATE_ENTRY_DETAILS.Vendor_Code as [Vendor Code] ,TSPL_MILK_GATE_ENTRY_DETAILS.Vendor_Desc as [Vendor Desc] ,TSPL_MILK_GATE_ENTRY_DETAILS.Item_Code as [Item Code] ,TSPL_MILK_GATE_ENTRY_DETAILS.Item_Desc as [Item Desc] ,TSPL_MILK_GATE_ENTRY_DETAILS.Qty_In_Kg as [Qty In Kg] ,TSPL_MILK_GATE_ENTRY_DETAILS.snf_Per as [SNF Per] ,TSPL_MILK_GATE_ENTRY_DETAILS.fat_per as [FAT Per] ,TSPL_MILK_GATE_ENTRY_DETAILS.Created_By as [Created By] ,TSPL_MILK_GATE_ENTRY_DETAILS.Created_Date as [Created Date] ,TSPL_MILK_GATE_ENTRY_DETAILS.Modify_By as [Modify By] ,TSPL_MILK_GATE_ENTRY_DETAILS.Modify_Date as [Modify Date] ,TSPL_MILK_GATE_ENTRY_DETAILS.comp_code as [Company Code]  From TSPL_MILK_GATE_ENTRY_DETAILS  "
            'If clsCommon.myLen(whrcls) > 0 Then
            '    qry = qry & "  and " & whrcls
            'End If
            'dt = clsCommon.ShowSelectFormForRow("TNKGTFND", qry, "TankerNo", curcode)
            'If dt IsNot Nothing Then
            ' str = dt("GateEntryNo").ToString
            'Else
            'str = ""
            'End If
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
        Dim trans As SqlTransaction = Nothing
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(strGateEntryNo) <= 0) Then
                Throw New Exception("Gate Entry No not found to Post")
            End If

            Dim obj As clsMilkGateEntry = clsMilkGateEntry.getData(strGateEntryNo, docType, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Gate_Entry_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            trans = clsDBFuncationality.GetTransactin()
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.MilkJobWork, clsUserMgtCode.FrmMilkGateEntry, obj.location_Code, obj.Date_And_Time, trans)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            '--------------------
            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(formId, "TSPL_MILK_GATE_ENTRY_DETAILS", "gate_entry_no", obj.Gate_Entry_No, trans)
            If isResult = False Then
                trans.Commit()
                Return False
            End If
            Dim strQry As String = " update TSPL_MILK_GATE_ENTRY_DETAILS set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where gate_entry_no='" & strGateEntryNo & "'"
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
    End Function

    Public Shared Function deleteData(ByVal strGateEntryNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_MILK_GATE_ENTRY_DETAILS where  gate_entry_no='" & strGateEntryNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsMilkGateEntry
        Dim obj As New clsMilkGateEntry
        Try
            Dim qst As String = " select *   From TSPL_MILK_GATE_ENTRY_DETAILS   where 1=1  "
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    qst = qst & " and location_code in (" & objCommonVar.strCurrUserLocations & ") "
                End If
            End If
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_MILK_GATE_ENTRY_DETAILS.gate_entry_no in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_MILK_GATE_ENTRY_DETAILS.gate_entry_no in (select min(gate_entry_no ) from TSPL_MILK_GATE_ENTRY_DETAILS where gate_entry_no  >'" + strCode + "' and  location_code in (" & objCommonVar.strCurrUserLocations & ") )"
                Case NavigatorType.First
                    qst += " and TSPL_MILK_GATE_ENTRY_DETAILS.gate_entry_no in (select MIN(gate_entry_no ) from TSPL_MILK_GATE_ENTRY_DETAILS  where  location_code in (" & objCommonVar.strCurrUserLocations & "))"
                Case NavigatorType.Last
                    qst += " and TSPL_MILK_GATE_ENTRY_DETAILS.gate_entry_no in (select Max(gate_entry_no ) from TSPL_MILK_GATE_ENTRY_DETAILS where  location_code in (" & objCommonVar.strCurrUserLocations & "))"
                Case NavigatorType.Previous
                    qst += " and TSPL_MILK_GATE_ENTRY_DETAILS.gate_entry_no in (select Max(gate_entry_no ) from TSPL_MILK_GATE_ENTRY_DETAILS where gate_entry_no  <'" + strCode + "' ) and location_code in (" & objCommonVar.strCurrUserLocations & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
                obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
                obj.Date_And_Time = clsCommon.myCDate(dt.Rows(0)("Date_And_Time"))
                obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
                obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"))
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
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
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal docType As String, ByVal navtype As NavigatorType) As clsMilkGateEntry
        Dim obj As New clsMilkGateEntry
        Try
            Dim qst As String = " select *   From TSPL_MILK_GATE_ENTRY_DETAILS   where 1=1 and doc_type='" & docType & "'"
            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO() AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " and location_code in (" & objCommonVar.strCurrUserLocations & ") "
            End If
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_MILK_GATE_ENTRY_DETAILS.gate_entry_no in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_MILK_GATE_ENTRY_DETAILS.gate_entry_no in (select min(gate_entry_no ) from TSPL_MILK_GATE_ENTRY_DETAILS where gate_entry_no  >'" + strCode + "' and doc_type='" & docType & "' " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_MILK_GATE_ENTRY_DETAILS.gate_entry_no in (select MIN(gate_entry_no ) from TSPL_MILK_GATE_ENTRY_DETAILS where doc_type='" & docType & "' " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_MILK_GATE_ENTRY_DETAILS.gate_entry_no in (select Max(gate_entry_no ) from TSPL_MILK_GATE_ENTRY_DETAILS where doc_type='" & docType & "' " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_MILK_GATE_ENTRY_DETAILS.gate_entry_no in (select Max(gate_entry_no ) from TSPL_MILK_GATE_ENTRY_DETAILS where gate_entry_no  <'" + strCode + "' and doc_type='" & docType & "' " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
                obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
                obj.Date_And_Time = clsCommon.myCDate(dt.Rows(0)("Date_And_Time"))
                obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
                obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"))
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
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

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal docType As String, ByVal navtype As NavigatorType, ByVal isUserCheck As Boolean) As clsMilkGateEntry
        Dim obj As clsMilkGateEntry = New clsMilkGateEntry()
        If isUserCheck Then
            Return getData(strCode, docType, navtype)
        Else
            Try
                Dim qst As String = " select *   From TSPL_MILK_GATE_ENTRY_DETAILS   where 1=1 and doc_type='" & docType & "' "
                Select Case navtype
                    Case NavigatorType.Current
                        qst += " and TSPL_MILK_GATE_ENTRY_DETAILS.gate_entry_no in ('" + strCode + "') "
                    Case NavigatorType.Next
                        qst += " and TSPL_MILK_GATE_ENTRY_DETAILS.gate_entry_no in (select min(gate_entry_no ) from TSPL_MILK_GATE_ENTRY_DETAILS where gate_entry_no  >'" + strCode + "' and doc_type='" & docType & "' )"
                    Case NavigatorType.First
                        qst += " and TSPL_MILK_GATE_ENTRY_DETAILS.gate_entry_no in (select MIN(gate_entry_no ) from TSPL_MILK_GATE_ENTRY_DETAILS where doc_type='" & docType & "' )"
                    Case NavigatorType.Last
                        qst += " and TSPL_MILK_GATE_ENTRY_DETAILS.gate_entry_no in (select Max(gate_entry_no ) from TSPL_MILK_GATE_ENTRY_DETAILS where doc_type='" & docType & "' )"
                    Case NavigatorType.Previous
                        qst += " and TSPL_MILK_GATE_ENTRY_DETAILS.gate_entry_no in (select Max(gate_entry_no ) from TSPL_MILK_GATE_ENTRY_DETAILS where gate_entry_no  <'" + strCode + "' and doc_type='" & docType & "' )"
                End Select
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
                    obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
                    obj.Date_And_Time = clsCommon.myCDate(dt.Rows(0)("Date_And_Time"))
                    obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
                    obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"))
                    obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                    obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
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

                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End If
        Return obj
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strDocNo) <= 0 Then
                Throw New Exception("Please select a Gate Entry No")
            End If
            Dim Qry As String = "select isPosted from TSPL_MILK_GATE_ENTRY_DETAILS where Gate_Entry_No='" + strDocNo + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If
            Dim isUsed As Integer = clsDBFuncationality.getSingleValue("select SUM(row_Count ) from (select COUNT(*) as row_Count from  TSPL_Milk_Weighment_Detail where gate_entry_no='" & strDocNo & "' union all select COUNT(*) as row_Count from tspl_Milk_quality_check where gate_entry_no='" & strDocNo & "') xx ", trans)
            If isUsed > 0 Then
                Throw New Exception("Gate Entry No is in use")
            End If
            Qry = "Update TSPL_MILK_GATE_ENTRY_DETAILS set isPosted = 0,Posting_Date=null where gate_entry_no='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
'Public Class customFinder
'    Public Shared Function getFinder(ByVal reportId As String, ByVal qry As String, ByVal whrCls As String, ByVal currColCode As String, ByVal currColValue As String, ByVal ReqColCode As String) As String
'        Dim str As String = ""
'        Try
'            Dim dt As DataRow
'            qry = qry & " where 1=1 "
'            If clsCommon.myLen(whrCls) > 0 Then

'                qry = qry & " and  " & whrCls
'            End If
'            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from ( " & qry & ") xx ")) > 0 Then
'                dt = clsCommon.ShowSelectFormForRow(reportId, qry, currColCode, currColValue)
'                If dt IsNot Nothing Then
'                    str = dt(ReqColCode).ToString
'                Else
'                    str = ""
'                End If
'            Else
'                Throw New Exception("No Data Found.")
'            End If
'        Catch ex As Exception
'            Throw New Exception(ex.Message)
'        End Try
'        Return str
'    End Function
'End Class