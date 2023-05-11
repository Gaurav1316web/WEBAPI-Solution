
Imports System.Data.SqlClient
Imports common
Public Class clsMilkGateEntry_JOW

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
    Public JobWorkLocation As String = String.Empty
    Public UOM As String = String.Empty
    Public arrJOWGateEntryDetail As List(Of clsMilkGateEntryDetail_JOW) = Nothing
    Public ArrTransferIssueNo As ArrayList = Nothing
    Public QC_Status As Integer = 0

    Public Shared Function SaveDataQC(ByVal obj As clsMilkGateEntry_JOW) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(obj.Gate_Entry_No) <= 0 Then
                Throw New Exception("Document No found to save")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "QC_Status", 0)
            clsCommon.AddColumnsForChange(coll, "QC_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "QC_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_GATE_ENTRY", OMInsertOrUpdate.Update, "TSPL_JWO_GATE_ENTRY.gate_entry_no='" + obj.Gate_Entry_No + "'", trans)
            If obj.arrJOWGateEntryDetail IsNot Nothing Then
                For Each objTR As clsMilkGateEntryDetail_JOW In obj.arrJOWGateEntryDetail
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "QC_Status", IIf(objTR.QC_Status, 1, 0))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_GATE_ENTRY_DETAIL", OMInsertOrUpdate.Update, " Gate_Entry_No='" + obj.Gate_Entry_No + "' and Item_Code='" + objTR.Item_Code + "'  ", trans)
                Next
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostDataQC(ByVal strGateEntryNo As String, ByVal docType As String, ByVal formId As String) As Boolean
        'Dim trans As SqlTransaction = Nothing
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(strGateEntryNo) <= 0) Then
                Throw New Exception("Gate Entry No not found to Post")
            End If

            Dim obj As clsMilkGateEntry_JOW = clsMilkGateEntry_JOW.getData(strGateEntryNo, docType, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Gate_Entry_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            'trans = clsDBFuncationality.GetTransactin()
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "JobWork Outward", "JobWork Milk Gate Entry", obj.location_Code, obj.Date_And_Time, Nothing)
            If Not (obj.isPosted = 1) Then
                Throw New Exception("Should be Post Gate entry:" + obj.Posting_Date)
            End If

            If (obj.QC_Status = 1) Then
                Throw New Exception("Already Posted ")
            End If

            Dim strQry As String = " update TSPL_JWO_GATE_ENTRY set QC_Status='1',QC_Post_By='" + objCommonVar.CurrentUserCode + "' ,QC_Post_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd/MMM/yyyy") & "' where gate_entry_no='" & strGateEntryNo & "'"
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry)
            'If isPosted Then
            '    trans.Commit()
            'Else
            '    trans.Rollback()
            'End If
            Return isPosted
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsMilkGateEntry_JOW, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function saveData(ByVal obj As clsMilkGateEntry_JOW, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "JobWork Outward", "JobWork Milk Gate Entry", obj.location_Code, obj.Date_And_Time, trans)
            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Gate_Entry_No), "TSPL_JWO_GATE_ENTRY", "gate_entry_no", "TSPL_JWO_GATE_ENTRY_detail", "gate_entry_no", "TSPL_JWO_GATE_ENTRY_TRASNFER", "Gate_Entry_No", trans)
            End If
            Dim qry As String = "delete from TSPL_JWO_GATE_ENTRY_DETAIL where Gate_Entry_No='" & obj.Gate_Entry_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_JWO_GATE_ENTRY_TRASNFER where Gate_Entry_No='" & obj.Gate_Entry_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If isNewEntry Then
                obj.Gate_Entry_No = clsERPFuncationality.GetNextCode(trans, obj.Date_And_Time, clsDocType.GateEntryJWO, "", obj.location_Code)
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", obj.Gate_Entry_No)
            clsCommon.AddColumnsForChange(coll, "Doc_Type", obj.Doc_Type)
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Date_And_Time", clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Date_And_Time", clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "Challan_No", obj.Challan_No)
            clsCommon.AddColumnsForChange(coll, "Challan_Date", clsCommon.GetPrintDate(obj.Challan_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No)
            clsCommon.AddColumnsForChange(coll, "isPosted", obj.isPosted)
            If obj.isPosted = 1 Then
                clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Posting_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "Dispatched_From_Mcc", obj.Dispatched_From_Mcc, True)
            clsCommon.AddColumnsForChange(coll, "location_Code", obj.location_Code, True)
            clsCommon.AddColumnsForChange(coll, "JobWorkLocation", obj.JobWorkLocation, True)
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
            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_GATE_ENTRY", OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_GATE_ENTRY", OMInsertOrUpdate.Update, "TSPL_JWO_GATE_ENTRY.gate_entry_no='" + obj.Gate_Entry_No + "'", trans)
            End If
            clsMilkGateEntryDetail_JOW.saveData(obj.arrJOWGateEntryDetail, obj.Gate_Entry_No, trans)
            clsMilkGateEntryTransfer.SaveData(obj.Gate_Entry_No, obj.ArrTransferIssueNo, trans)
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try

            'Dim qry As String = " select TSPL_JWO_GATE_ENTRY.Gate_Entry_No as [GateEntryNo] ,TSPL_JWO_GATE_ENTRY.Doc_Type as [Doc Type] ,TSPL_JWO_GATE_ENTRY.Date_And_Time as [Date And Time] ,TSPL_JWO_GATE_ENTRY.Challan_No as [Challan No] ,TSPL_JWO_GATE_ENTRY.Challan_Date as [Challan Date] ,TSPL_JWO_GATE_ENTRY.Tanker_No as [Tanker No] ,TSPL_JWO_GATE_ENTRY.Dispatched_From_Mcc as [Dispatched From Mcc] ,case when TSPL_JWO_GATE_ENTRY.isPosted=0 then 'NO' else 'YES' end as [Posting Status],TSPL_JWO_GATE_ENTRY.location_Code as [Location Code] ,TSPL_JWO_GATE_ENTRY.Location_Desc as [Location Desc] ,TSPL_JWO_GATE_ENTRY.Vendor_Code as [Vendor Code] ,TSPL_JWO_GATE_ENTRY.Vendor_Desc as [Vendor Desc] ,TSPL_JWO_GATE_ENTRY.Item_Code as [Item Code] ,TSPL_JWO_GATE_ENTRY.Item_Desc as [Item Desc] ,TSPL_JWO_GATE_ENTRY.Qty_In_Kg as [Qty In Kg] ,TSPL_JWO_GATE_ENTRY.snf_Per as [SNF Per] ,TSPL_JWO_GATE_ENTRY.fat_per as [FAT Per] ,TSPL_JWO_GATE_ENTRY.Created_By as [Created By] ,TSPL_JWO_GATE_ENTRY.Created_Date as [Created Date] ,TSPL_JWO_GATE_ENTRY.Modify_By as [Modify By] ,TSPL_JWO_GATE_ENTRY.Modify_Date as [Modify Date] ,TSPL_JWO_GATE_ENTRY.comp_code as [Company Code], case when  ISNULL(tspl_gate_out.Doc_No ,'')='' then 'Pending For Gate Out' else ISNULL(tspl_gate_out.Doc_No ,'') end as [Gate Out No]  From TSPL_JWO_GATE_ENTRY left outer join TSPL_Gate_Out on TSPL_Gate_Out.Gate_Entry_No=TSPL_JWO_GATE_ENTRY.Gate_Entry_No "
            'str = clsCommon.ShowSelectForm("GATEENTRY", qry, "GateEntryNo", whrcls, curcode, "GateEntryNo", isButtonClicked)

            Dim qry As String = " select TSPL_JWO_GATE_ENTRY.Gate_Entry_No as [GateEntryNo] ,TSPL_JWO_GATE_ENTRY.Doc_Type as [Doc Type] ,TSPL_JWO_GATE_ENTRY.Date_And_Time as [Date And Time] ,TSPL_JWO_GATE_ENTRY.Challan_No as [Challan No] ,TSPL_JWO_GATE_ENTRY.Challan_Date as [Challan Date] ,TSPL_JWO_GATE_ENTRY.Tanker_No as [Tanker No] ,TSPL_JWO_GATE_ENTRY.Dispatched_From_Mcc as [Dispatched From Mcc] ,case when TSPL_JWO_GATE_ENTRY.isPosted=0 then 'NO' else 'YES' end as [Posting Status],TSPL_JWO_GATE_ENTRY.location_Code as [Location Code] ,TSPL_JWO_GATE_ENTRY.Location_Desc as [Location Desc] ,TSPL_JWO_GATE_ENTRY.Vendor_Code as [Vendor Code] ,TSPL_JWO_GATE_ENTRY.Vendor_Desc as [Vendor Desc] ,TSPL_JWO_GATE_ENTRY.Item_Code as [Item Code] ,TSPL_JWO_GATE_ENTRY.Item_Desc as [Item Desc] ,TSPL_JWO_GATE_ENTRY.Qty_In_Kg as [Qty In Kg] ,TSPL_JWO_GATE_ENTRY.snf_Per as [SNF Per] ,TSPL_JWO_GATE_ENTRY.fat_per as [FAT Per] ,TSPL_JWO_GATE_ENTRY.Created_By as [Created By] ,TSPL_JWO_GATE_ENTRY.Created_Date as [Created Date] ,TSPL_JWO_GATE_ENTRY.Modify_By as [Modify By] ,TSPL_JWO_GATE_ENTRY.Modify_Date as [Modify Date] ,TSPL_JWO_GATE_ENTRY.comp_code as [Company Code]  From TSPL_JWO_GATE_ENTRY  "
            str = clsCommon.ShowSelectForm("GATEENTRYJWO", qry, "GateEntryNo", whrcls, curcode, "GateEntryNo", isButtonClicked)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function getTankerFinder(ByVal whrcls As String, ByVal curcode As String) As String
        Dim str As String = ""
        Try
            'Dim dt As DataRow

            Dim qry As String = " select TSPL_JWO_GATE_ENTRY.Tanker_No as [TankerNo] , TSPL_JWO_GATE_ENTRY.Gate_Entry_No as [GateEntryNo] ,TSPL_JWO_GATE_ENTRY.Doc_Type as [Doc Type] ,TSPL_JWO_GATE_ENTRY.Date_And_Time as [Date And Time] ,TSPL_JWO_GATE_ENTRY.Challan_No as [Challan No] ,TSPL_JWO_GATE_ENTRY.Challan_Date as [Challan Date] ,TSPL_JWO_GATE_ENTRY.Dispatched_From_Mcc as [Dispatched From Mcc] ,TSPL_JWO_GATE_ENTRY.location_Code as [Location Code] ,TSPL_JWO_GATE_ENTRY.Location_Desc as [Location Desc] ,TSPL_JWO_GATE_ENTRY.Vendor_Code as [Vendor Code] ,TSPL_JWO_GATE_ENTRY.Vendor_Desc as [Vendor Desc] ,TSPL_JWO_GATE_ENTRY.Item_Code as [Item Code] ,TSPL_JWO_GATE_ENTRY.Item_Desc as [Item Desc] ,TSPL_JWO_GATE_ENTRY.Qty_In_Kg as [Qty In Kg] ,TSPL_JWO_GATE_ENTRY.snf_Per as [SNF Per] ,TSPL_JWO_GATE_ENTRY.fat_per as [FAT Per] ,TSPL_JWO_GATE_ENTRY.Created_By as [Created By] ,TSPL_JWO_GATE_ENTRY.Created_Date as [Created Date] ,TSPL_JWO_GATE_ENTRY.Modify_By as [Modify By] ,TSPL_JWO_GATE_ENTRY.Modify_Date as [Modify Date] ,TSPL_JWO_GATE_ENTRY.comp_code as [Company Code]  From TSPL_JWO_GATE_ENTRY  "
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
        'Dim trans As SqlTransaction = Nothing
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(strGateEntryNo) <= 0) Then
                Throw New Exception("Gate Entry No not found to Post")
            End If

            Dim obj As clsMilkGateEntry_JOW = clsMilkGateEntry_JOW.getData(strGateEntryNo, docType, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Gate_Entry_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            'trans = clsDBFuncationality.GetTransactin()
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "JobWork Outward", "JobWork Milk Gate Entry", obj.location_Code, obj.Date_And_Time, Nothing)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            '--------------------
            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(formId, "TSPL_JWO_GATE_ENTRY", "gate_entry_no", obj.Gate_Entry_No, Nothing)
            If isResult = False Then
                'trans.Commit()
                Return False
            End If
            Dim strQry As String = " update TSPL_JWO_GATE_ENTRY set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd/MMM/yyyy") & "' where gate_entry_no='" & strGateEntryNo & "'"
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strGateEntryNo), "TSPL_JWO_GATE_ENTRY", "gate_entry_no", Nothing)
            'If isPosted Then
            '    trans.Commit()
            'Else
            '    trans.Rollback()
            'End If
            Return isPosted
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function deleteData(ByVal strGateEntryNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strGateEntryNo), "TSPL_JWO_GATE_ENTRY", "gate_entry_no", "TSPL_JWO_GATE_ENTRY_detail", "gate_entry_no", trans)
            Dim qry As String = "delete from TSPL_JWO_GATE_ENTRY_detail where gate_entry_no='" + strGateEntryNo + "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_JWO_GATE_ENTRY where  gate_entry_no='" & strGateEntryNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsMilkGateEntry_JOW
        Dim obj As New clsMilkGateEntry_JOW
        Try
            Dim qst As String = " select TSPL_JWO_GATE_ENTRY.*   From TSPL_JWO_GATE_ENTRY   where 1=1  "
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    qst = qst & " and location_code in (" & objCommonVar.strCurrUserLocations & ") "
                End If
            End If
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_JWO_GATE_ENTRY.gate_entry_no in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_JWO_GATE_ENTRY.gate_entry_no in (select min(gate_entry_no ) from TSPL_JWO_GATE_ENTRY where gate_entry_no  >'" + strCode + "' and  location_code in (" & objCommonVar.strCurrUserLocations & ") )"
                Case NavigatorType.First
                    qst += " and TSPL_JWO_GATE_ENTRY.gate_entry_no in (select MIN(gate_entry_no ) from TSPL_JWO_GATE_ENTRY  where  location_code in (" & objCommonVar.strCurrUserLocations & "))"
                Case NavigatorType.Last
                    qst += " and TSPL_JWO_GATE_ENTRY.gate_entry_no in (select Max(gate_entry_no ) from TSPL_JWO_GATE_ENTRY where  location_code in (" & objCommonVar.strCurrUserLocations & "))"
                Case NavigatorType.Previous
                    qst += " and TSPL_JWO_GATE_ENTRY.gate_entry_no in (select Max(gate_entry_no ) from TSPL_JWO_GATE_ENTRY where gate_entry_no  <'" + strCode + "' ) and location_code in (" & objCommonVar.strCurrUserLocations & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
                obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
                obj.Date_And_Time = clsCommon.myCDate(dt.Rows(0)("Date_And_Time"))
                obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
                obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"))
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                obj.JobWorkLocation = clsCommon.myCstr(dt.Rows(0)("JobWorkLocation"))
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
                obj.QC_Status = clsCommon.myCdbl(dt.Rows(0)("QC_Status"))
                obj.arrJOWGateEntryDetail = clsMilkGateEntryDetail_JOW.getData(obj.Gate_Entry_No, trans)
                obj.ArrTransferIssueNo = clsMilkGateEntryTransfer.getData(obj.Gate_Entry_No, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal docType As String, ByVal navtype As NavigatorType) As clsMilkGateEntry_JOW
        Dim obj As New clsMilkGateEntry_JOW
        Try
            Dim qst As String = " select *   From TSPL_JWO_GATE_ENTRY   where 1=1 and doc_type='" & docType & "'"
            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO() AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " and location_code in (" & objCommonVar.strCurrUserLocations & ") "
            End If
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_JWO_GATE_ENTRY.gate_entry_no in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_JWO_GATE_ENTRY.gate_entry_no in (select min(gate_entry_no ) from TSPL_JWO_GATE_ENTRY where gate_entry_no  >'" + strCode + "' and doc_type='" & docType & "' " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_JWO_GATE_ENTRY.gate_entry_no in (select MIN(gate_entry_no ) from TSPL_JWO_GATE_ENTRY where doc_type='" & docType & "' " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_JWO_GATE_ENTRY.gate_entry_no in (select Max(gate_entry_no ) from TSPL_JWO_GATE_ENTRY where doc_type='" & docType & "' " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_JWO_GATE_ENTRY.gate_entry_no in (select Max(gate_entry_no ) from TSPL_JWO_GATE_ENTRY where gate_entry_no  <'" + strCode + "' and doc_type='" & docType & "' " & whrCls & ")"
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
                obj.JobWorkLocation = clsCommon.myCstr(dt.Rows(0)("JobWorkLocation"))
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
                obj.QC_Status = clsCommon.myCdbl(dt.Rows(0)("QC_Status"))
                obj.arrJOWGateEntryDetail = clsMilkGateEntryDetail_JOW.getData(obj.Gate_Entry_No, Nothing)
                obj.ArrTransferIssueNo = clsMilkGateEntryTransfer.getData(obj.Gate_Entry_No, Nothing)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal docType As String, ByVal navtype As NavigatorType, ByVal isUserCheck As Boolean) As clsMilkGateEntry_JOW
        Dim obj As clsMilkGateEntry_JOW = New clsMilkGateEntry_JOW()
        If isUserCheck Then
            Return getData(strCode, docType, navtype)
        Else
            Try
                Dim qst As String = " select *   From TSPL_JWO_GATE_ENTRY   where 1=1 and doc_type='" & docType & "' "
                Select Case navtype
                    Case NavigatorType.Current
                        qst += " and TSPL_JWO_GATE_ENTRY.gate_entry_no in ('" + strCode + "') "
                    Case NavigatorType.Next
                        qst += " and TSPL_JWO_GATE_ENTRY.gate_entry_no in (select min(gate_entry_no ) from TSPL_JWO_GATE_ENTRY where gate_entry_no  >'" + strCode + "' and doc_type='" & docType & "' )"
                    Case NavigatorType.First
                        qst += " and TSPL_JWO_GATE_ENTRY.gate_entry_no in (select MIN(gate_entry_no ) from TSPL_JWO_GATE_ENTRY where doc_type='" & docType & "' )"
                    Case NavigatorType.Last
                        qst += " and TSPL_JWO_GATE_ENTRY.gate_entry_no in (select Max(gate_entry_no ) from TSPL_JWO_GATE_ENTRY where doc_type='" & docType & "' )"
                    Case NavigatorType.Previous
                        qst += " and TSPL_JWO_GATE_ENTRY.gate_entry_no in (select Max(gate_entry_no ) from TSPL_JWO_GATE_ENTRY where gate_entry_no  <'" + strCode + "' and doc_type='" & docType & "' )"
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
                    obj.JobWorkLocation = clsCommon.myCstr(dt.Rows(0)("JobWorkLocation"))
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
                    obj.QC_Status = clsCommon.myCdbl(dt.Rows(0)("QC_Status"))
                    obj.arrJOWGateEntryDetail = clsMilkGateEntryDetail_JOW.getData(obj.Gate_Entry_No, Nothing)
                    obj.ArrTransferIssueNo = clsMilkGateEntryTransfer.getData(obj.Gate_Entry_No, Nothing)
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
            Dim Qry As String = "select isPosted from TSPL_JWO_GATE_ENTRY where Gate_Entry_No='" + strDocNo + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If
            Dim isUsed As Integer = clsDBFuncationality.getSingleValue("select SUM(row_Count ) from (select COUNT(*) as row_Count from  TSPL_Milk_Weighment_Detail where gate_entry_no='" & strDocNo & "' union all select COUNT(*) as row_Count from tspl_Milk_quality_check where gate_entry_no='" & strDocNo & "') xx ", trans)
            If isUsed > 0 Then
                Throw New Exception("Gate Entry No is in use")
            End If
            Qry = "Update TSPL_JWO_GATE_ENTRY set isPosted = 0,Posting_Date=null where gate_entry_no='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strDocNo), "TSPL_JWO_GATE_ENTRY", "gate_entry_no", trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
Public Class clsMilkGateEntryDetail_JOW
    Public Gate_Entry_No As String = Nothing
    Public Item_Code As String = Nothing
    Public UOM As String = Nothing
    Public Item_Desc As String = Nothing
    Public Qty_In_Kg As Double = 0
    Public snf_Per As Double = 0
    Public fat_per As Double = 0
    Public QC_Status As Boolean = False

    Public Shared Function saveData(ByVal arrObj As List(Of clsMilkGateEntryDetail_JOW), ByVal strgateentryno As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                For Each obj As clsMilkGateEntryDetail_JOW In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", strgateentryno)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                    clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                    clsCommon.AddColumnsForChange(coll, "Qty_In_Kg", obj.Qty_In_Kg)
                    clsCommon.AddColumnsForChange(coll, "snf_Per", obj.snf_Per)
                    clsCommon.AddColumnsForChange(coll, "fat_per", obj.fat_per)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_GATE_ENTRY_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            arrObj = Nothing
        End Try
    End Function

    Public Shared Function getData(ByVal strgateentryno As String, ByVal trans As SqlTransaction) As List(Of clsMilkGateEntryDetail_JOW)
        Try
            Dim arrObj As List(Of clsMilkGateEntryDetail_JOW) = Nothing
            Dim obj As clsMilkGateEntryDetail_JOW = Nothing
            Dim qry As String = "Select TSPL_JWO_GATE_ENTRY_DETAIL.* from TSPL_JWO_GATE_ENTRY_DETAIL where Gate_Entry_No='" & strgateentryno & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsMilkGateEntryDetail_JOW)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsMilkGateEntryDetail_JOW()
                    obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(i)("Gate_Entry_No"))
                    obj.Item_Code = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                    obj.Item_Desc = clsCommon.myCstr(dt.Rows(i)("Item_Desc"))
                    obj.Qty_In_Kg = clsCommon.myCdbl(dt.Rows(i)("Qty_In_Kg"))
                    obj.fat_per = clsCommon.myCdbl(dt.Rows(i)("fat_per"))
                    obj.snf_Per = clsCommon.myCdbl(dt.Rows(i)("snf_Per"))
                    obj.UOM = clsCommon.myCstr(dt.Rows(i)("UOM"))
                    obj.QC_Status = (clsCommon.myCdbl(dt.Rows(0)("QC_Status")) = 1)
                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class


Public Class clsMilkGateEntryTransfer
    Public Gate_Entry_No As String = Nothing
    Public Transfer_No As String = Nothing

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each str As String In arr
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Transfer_No", str)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_GATE_ENTRY_TRASNFER", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As ArrayList
        Try
            Dim arr As ArrayList = Nothing
            Dim obj As clsMilkGateEntryDetail_JOW = Nothing
            Dim qry As String = "Select * from TSPL_JWO_GATE_ENTRY_TRASNFER where Gate_Entry_No='" & strDocNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arr = New ArrayList
                For i As Integer = 0 To dt.Rows.Count - 1
                    arr.Add(clsCommon.myCstr(dt.Rows(i)("Transfer_No")))
                Next
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
