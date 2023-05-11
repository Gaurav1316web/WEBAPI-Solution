
Imports common
Imports System.Data.SqlClient

Public Class clsMilkWeighment_JOW
    Public isNewEntry As Boolean = True
    Public Weighment_No As String = String.Empty
    Public Weighment_Date As Date = Nothing
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
    Public JobWorkLocation As String = String.Empty
    Public Qty_In_Kg As Double = 0
    Public snf_Per As Double = 0
    Public fat_per As Double = 0
    Public Created_By As String = String.Empty
    Public UOM As String = String.Empty
    Public Created_Date As String = String.Empty
    Public Modify_By As String = String.Empty
    Public Modify_Date As String = String.Empty
    Public comp_code As String = String.Empty
    Public Gross_Weight As Double = 0
    Public Dip_Value As Double = 0
    Public Tare_Weight As Double = 0
    Public Net_Weight As Double = 0
    Public status As Integer = 0
    Public Sent_to_QC_By As String = String.Empty
    Public Sent_To_QC_Date As Date = Nothing
    Public QC_Done_By As String = String.Empty
    Public QC_Done_Date As Date = Nothing
    Public Weighment_Slip_No As String = String.Empty
    Public Tare_Weight_date As DateTime? = Nothing
    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strDocNo) <= 0 Then
                Throw New Exception("Please select a Weighment No")
            End If
            Dim Qry As String = "select isPosted from TSPL_JWO_Weighment where Weighment_no='" + strDocNo + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If
            'Dim isUsed As Integer = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_Bulk_MILK_SRN where Weighment_no='" & strDocNo & "'", trans)
            'If isUsed > 0 Then
            '    Throw New Exception("Weighment No is in use")
            'End If
            Qry = "Update TSPL_JWO_Weighment set isPosted = 0,Posting_Date=null where weighment_no='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_JWO_Weighment", "Weighment_No", trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function SaveData(ByVal obj As clsMilkWeighment_JOW, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function saveData(ByVal obj As clsMilkWeighment_JOW, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "JobWork Outward", "JobWork Milk Weighment", obj.location_Code, obj.Weighment_Date, trans)
            If isNewEntry Then
                obj.Weighment_No = clsERPFuncationality.GetNextCode(trans, obj.Weighment_Date, clsDocType.WeighmentJWO, "", obj.location_Code)
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Weighment_No", obj.Weighment_No)
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Weighment_Date", clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Weighment_Date", clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", obj.Gate_Entry_No)
            clsCommon.AddColumnsForChange(coll, "Doc_Type", obj.Doc_Type)
            clsCommon.AddColumnsForChange(coll, "Date_And_Time", clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Challan_No", obj.Challan_No)
            clsCommon.AddColumnsForChange(coll, "Challan_Date", clsCommon.GetPrintDate(obj.Challan_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No)
            clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
            clsCommon.AddColumnsForChange(coll, "isPosted", obj.isPosted)

            If obj.isPosted = 1 Then
                clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Posting_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Dispatched_From_Mcc", obj.Dispatched_From_Mcc, True)

            clsCommon.AddColumnsForChange(coll, "location_Code", obj.location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Weighment_Slip_No", obj.Weighment_Slip_No, True)
            clsCommon.AddColumnsForChange(coll, "Location_Desc", obj.Location_Desc, True)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code, True)
            clsCommon.AddColumnsForChange(coll, "Vendor_Desc", obj.Vendor_Desc, True)
            clsCommon.AddColumnsForChange(coll, "JobWorkLocation", obj.JobWorkLocation, True)

            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
            clsCommon.AddColumnsForChange(coll, "Qty_In_Kg", obj.Qty_In_Kg)
            clsCommon.AddColumnsForChange(coll, "snf_Per", obj.snf_Per)
            clsCommon.AddColumnsForChange(coll, "fat_per", obj.fat_per)
            If obj.status <> 0 Then
                clsCommon.AddColumnsForChange(coll, "status", obj.status)
            End If
            clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)
            clsCommon.AddColumnsForChange(coll, "Dip_Value", obj.Dip_Value)
            clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
            clsCommon.AddColumnsForChange(coll, "Net_Weight", obj.Net_Weight)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", obj.comp_code)
            If obj.Tare_Weight_date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Tare_Weight_date", clsCommon.GetPrintDate(obj.Tare_Weight_date, "dd/MMM/yyyy hh:mm tt"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "Tare_Weight_date", Nothing, True)
            End If
            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_Weighment", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Weighment_No, "TSPL_JWO_Weighment", "Weighment_No", trans)
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_Weighment", OMInsertOrUpdate.Update, "TSPL_JWO_Weighment.Weighment_No='" + obj.Weighment_No + "'", trans)
            End If

            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_JWO_QUALITY_CHECK where isposted=1 and gate_entry_no='" & obj.Gate_Entry_No & "'  and weighment_no=''", trans)) > 0 Then
                issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery("update TSPL_JWO_QUALITY_CHECK set weighment_no='" & obj.Weighment_No & "',weighment_date='" & clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy") & "' where gate_entry_no='" & obj.Gate_Entry_No & "'", trans)
            End If

            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal docType As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsMilkWeighment_JOW
        Return getData(strCode, docType, navtype, False, trans)
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal docType As String, ByVal navtype As NavigatorType, ByVal isPendingOnly As Boolean, Optional ByVal trans As SqlTransaction = Nothing) As clsMilkWeighment_JOW
        Try
            Dim obj As New clsMilkWeighment_JOW
            Dim whrCls As String = String.Empty
            Dim whrcls2 As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrcls2 = " and location_code in (" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                whrCls = IIf(clsCommon.myLen(docType) > 0, " and doc_type='" & docType & "'  ", "  ") & whrcls2
            Else
                whrCls = IIf(clsCommon.myLen(docType) > 0, " and doc_type='" & docType & "'  ", "  ")
            End If

            If isPendingOnly Then
                whrCls = whrCls & "    and isPosted=0 "

            End If
            Dim qst As String = " select * From TSPL_JWO_Weighment where 1=1 "
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_JWO_Weighment.Weighment_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_JWO_Weighment.Weighment_No in (select min(Weighment_No ) from TSPL_JWO_Weighment where Weighment_No  >'" + strCode + "'" & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_JWO_Weighment.Weighment_No in (select MIN(Weighment_No ) from TSPL_JWO_Weighment where 1=1 " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_JWO_Weighment.Weighment_No in (select Max(Weighment_No ) from TSPL_JWO_Weighment where 1=1" & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_JWO_Weighment.Weighment_No in (select Max(Weighment_No ) from TSPL_JWO_Weighment where Weighment_No  <'" + strCode + "'" & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Weighment_No = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
                obj.Weighment_Date = clsCommon.GetPrintDate(dt.Rows(0)("Weighment_Date"), "dd/MMM/yyyy hh:mm:ss tt")
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
                obj.Date_And_Time = clsCommon.myCDate(dt.Rows(0)("Date_And_Time"), "dd/MMM/yyyy hh:mm:ss tt")
                obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
                obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"), "dd/MMM/yyyy")
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                '                If clsCommon.CompairString(docType, "MccProc") = CompairStringResult.Equal Then
                obj.Dispatched_From_Mcc = clsCommon.myCstr(dt.Rows(0)("Dispatched_From_Mcc"))
                'ElseIf clsCommon.CompairString(docType, "BulkProc") = CompairStringResult.Equal Then
                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                obj.Vendor_Desc = clsCommon.myCstr(dt.Rows(0)("Vendor_Desc"))
                obj.location_Code = clsCommon.myCstr(dt.Rows(0)("location_Code"))
                obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
                obj.JobWorkLocation = clsCommon.myCstr(dt.Rows(0)("JobWorkLocation"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Weighment_Slip_No = clsCommon.myCstr(dt.Rows(0)("Weighment_Slip_No"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
                obj.Qty_In_Kg = clsCommon.myCdbl(dt.Rows(0)("Qty_In_Kg"))
                obj.fat_per = clsCommon.myCdbl(dt.Rows(0)("fat_per"))
                obj.snf_Per = clsCommon.myCdbl(dt.Rows(0)("snf_Per"))
                obj.Dip_Value = clsCommon.myCdbl(dt.Rows(0)("Dip_Value"))
                obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight"))
                obj.Tare_Weight = clsCommon.myCdbl(dt.Rows(0)("Tare_Weight"))
                obj.Net_Weight = clsCommon.myCdbl(dt.Rows(0)("Net_Weight"))
                If clsDBFuncationality.getSingleValue("select count(*) from TSPL_JWO_QUALITY_CHECK where isPosted=1 and gate_entry_no='" & obj.Gate_Entry_No & "'", trans) = 0 Then
                    obj.status = 0
                Else
                    obj.status = 1
                End If
                ''obj.status = clsCommon.myCdbl(dt.Rows(0)("status"))
                obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
                obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If

                If dt.Rows(0)("Tare_Weight_date") IsNot DBNull.Value Then
                    obj.Tare_Weight_date = clsCommon.myCDate(dt.Rows(0)("Tare_Weight_date"))
                End If
            Else
                obj = Nothing
            End If

            Return obj

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, ByVal isPendingOnly As Boolean, Optional ByVal trans As SqlTransaction = Nothing) As clsMilkWeighment_JOW
        Try
            Dim obj As New clsMilkWeighment_JOW
            Dim whrCls As String = String.Empty
            Dim whrcls2 As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = " and location_code in (" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            'If Not clsMccMaster.isCurrentUserHO Then
            '    whrCls = IIf(clsCommon.myLen(docType) > 0, " and doc_type='" & docType & "'  ", "  ") & whrcls2
            'Else
            '    whrCls = IIf(clsCommon.myLen(docType) > 0, " and doc_type='" & docType & "'  ", "  ")
            'End If

            If isPendingOnly Then
                whrCls = whrCls & "    and isPosted=0 "

            End If
            Dim qst As String = " select *   From TSPL_JWO_Weighment   where 1=1 "
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_JWO_Weighment.Weighment_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_JWO_Weighment.Weighment_No in (select min(Weighment_No ) from TSPL_JWO_Weighment where Weighment_No  >'" + strCode + "'" & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_JWO_Weighment.Weighment_No in (select MIN(Weighment_No ) from TSPL_JWO_Weighment where 1=1 " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_JWO_Weighment.Weighment_No in (select Max(Weighment_No ) from TSPL_JWO_Weighment where 1=1" & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_JWO_Weighment.Weighment_No in (select Max(Weighment_No ) from TSPL_JWO_Weighment where Weighment_No  <'" + strCode + "'" & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Weighment_No = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
                obj.Weighment_Date = clsCommon.GetPrintDate(dt.Rows(0)("Weighment_Date"), "dd/MMM/yyyy hh:mm:ss tt")
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
                obj.Date_And_Time = clsCommon.myCDate(dt.Rows(0)("Date_And_Time"), "dd/MMM/yyyy hh:mm:ss tt")
                obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
                obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"), "dd/MMM/yyyy")
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                '                If clsCommon.CompairString(docType, "MccProc") = CompairStringResult.Equal Then
                obj.Dispatched_From_Mcc = clsCommon.myCstr(dt.Rows(0)("Dispatched_From_Mcc"))
                'ElseIf clsCommon.CompairString(docType, "BulkProc") = CompairStringResult.Equal Then
                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                obj.Vendor_Desc = clsCommon.myCstr(dt.Rows(0)("Vendor_Desc"))
                obj.location_Code = clsCommon.myCstr(dt.Rows(0)("location_Code"))
                obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
                obj.JobWorkLocation = clsCommon.myCstr(dt.Rows(0)("JobWorkLocation"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Weighment_Slip_No = clsCommon.myCstr(dt.Rows(0)("Weighment_Slip_No"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
                obj.Qty_In_Kg = clsCommon.myCdbl(dt.Rows(0)("Qty_In_Kg"))
                obj.fat_per = clsCommon.myCdbl(dt.Rows(0)("fat_per"))
                obj.snf_Per = clsCommon.myCdbl(dt.Rows(0)("snf_Per"))
                obj.Dip_Value = clsCommon.myCdbl(dt.Rows(0)("Dip_Value"))
                obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight"))
                obj.Tare_Weight = clsCommon.myCdbl(dt.Rows(0)("Tare_Weight"))
                obj.Net_Weight = clsCommon.myCdbl(dt.Rows(0)("Net_Weight"))
                If clsDBFuncationality.getSingleValue("select count(*) from TSPL_JWO_QUALITY_CHECK where isPosted=1 and gate_entry_no='" & obj.Gate_Entry_No & "'", trans) = 0 Then
                    obj.status = 0
                Else
                    obj.status = 1
                End If
                ''obj.status = clsCommon.myCdbl(dt.Rows(0)("status"))
                obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
                obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If
                If dt.Rows(0)("Tare_Weight_date") IsNot DBNull.Value Then
                    obj.Tare_Weight_date = clsCommon.myCDate(dt.Rows(0)("Tare_Weight_date"))
                End If
            Else
                obj = Nothing
            End If

            Return obj

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function isWeighmentDone(ByVal strGateEntryNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        qry = "select count(*) from TSPL_JWO_Weighment where gate_entry_no='" & strGateEntryNo & "' and isposted=1"
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) <= 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Shared Function deleteData(ByVal strWeighmentNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            Dim isUsed As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_JWO_UNLOADING where weighment_no='" & strWeighmentNo & "'", trans)
            If isUsed > 0 Then
                Throw New Exception("Record is in use.")
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strWeighmentNo, "TSPL_JWO_Weighment", "Weighment_No", trans)
            Dim strQry As String = "delete from TSPL_JWO_Weighment where Weighment_No='" & strWeighmentNo & "'"
            Dim isDeleted As Boolean = clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select TSPL_JWO_Weighment.Weighment_No as [Code],TSPL_JWO_Weighment.Weighment_Slip_no as [Weighment Slip No] ,TSPL_JWO_Weighment.Weighment_date as [Weighment Date], TSPL_JWO_Weighment.Gate_Entry_No as [Gate Entry No] ,TSPL_JWO_Weighment.Doc_Type as [Doc Type] ,TSPL_JWO_Weighment.Date_And_Time as [Gate Entry Date And Time] ,TSPL_JWO_Weighment.Challan_No as [Challan No] ,TSPL_JWO_Weighment.Challan_Date as [Challan Date] ,TSPL_JWO_Weighment.Tanker_No as [Tanker No] ,case when isnull(TSPL_JWO_Weighment.isPosted,0)=0 then 'No' else 'Yes' end  as [Is Posted] ,TSPL_JWO_Weighment.Posting_Date as [Posting Date] ,TSPL_JWO_Weighment.Dispatched_From_Mcc as [Dispatched From Mcc] ,TSPL_JWO_Weighment.location_Code as [Location Code] ,TSPL_JWO_Weighment.Location_Desc as [Location Desc] ,TSPL_JWO_Weighment.Vendor_Code as [Vendor Code] ,TSPL_JWO_Weighment.Vendor_Desc as [Vendor Desc] ,TSPL_JWO_Weighment.Item_Code as [Item Code] ,TSPL_JWO_Weighment.Item_Desc as [Item Desc] ,TSPL_JWO_Weighment.Qty_In_Kg as [Qty In Kg] ,TSPL_JWO_Weighment.snf_Per as [SNF Per] ,TSPL_JWO_Weighment.fat_per as [FAT Per] ,TSPL_JWO_Weighment.Created_By as [Created By] ,TSPL_JWO_Weighment.Created_Date as [Created Date] ,TSPL_JWO_Weighment.Modify_By as [Modify By] ,TSPL_JWO_Weighment.Modify_Date as [Modify Date] ,TSPL_JWO_Weighment.comp_code as [Company Code] ,TSPL_JWO_Weighment.Gross_Weight as [Gross Weight] ,TSPL_JWO_Weighment.Dip_Value as [Dip Value] ,TSPL_JWO_Weighment.Tare_Weight as [Tare Weight] ,TSPL_JWO_Weighment.Net_Weight as [Net Weight] ,case when isnull (TSPL_JWO_Weighment.status,0)=0 then 'Not Sent For QC' when TSPL_JWO_Weighment.status=1 then 'Sent For QC' else 'QC Done' end as [Status] ,TSPL_JWO_Weighment.Sent_to_QC_By as [Sent To Qc By] ,TSPL_JWO_Weighment.Sent_To_QC_Date as [Sent To Qc Date] ,TSPL_JWO_Weighment.QC_Done_By as [Qc Done By] ,TSPL_JWO_Weighment.QC_Done_Date as [Qc Done Date]   From TSPL_JWO_Weighment"
            str = clsCommon.ShowSelectForm("WGHMNT", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function postData(ByVal strWeighmentNo As String, ByVal docType As String, ByVal formId As String, ByVal trans As SqlTransaction) As Boolean
        Dim isTrnasInitPostData As Boolean = False
        If trans Is Nothing Then
            'trans = clsDBFuncationality.GetTransactin()
            isTrnasInitPostData = True
        Else
            isTrnasInitPostData = False
        End If
        Dim isPosted As Boolean = True
        Try

            If (clsCommon.myLen(strWeighmentNo) <= 0) Then
                Throw New Exception("Weighment No not found to Post")
            End If

            Dim obj As clsMilkWeighment_JOW = clsMilkWeighment_JOW.getData(strWeighmentNo, docType, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Weighment_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            'trans = clsDBFuncationality.GetTransactin()
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "JobWork Outward", "JobWork Milk Weighment", obj.location_Code, obj.Weighment_Date, trans)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            '--------------------
            'Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(formId, "TSPL_JWO_Weighment", "Weighment_No", obj.Weighment_No, trans)
            'If isResult = False Then
            '    trans.Commit()
            '    Return False
            'End If
            Dim strQry As String = " update TSPL_JWO_Weighment set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where Weighment_No='" & strWeighmentNo & "'"
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strWeighmentNo, "TSPL_JWO_Weighment", "Weighment_No", trans)
            'isPosted = clsQualityCheck.SaveAndPostUnloadingGateOutMilkTransferIn(obj.Gate_Entry_No, trans)
            'If isPosted Then
            '    trans.Commit()
            'Else
            '    trans.Rollback()
            'End If

        Catch ex As Exception

            Try
                'trans.Rollback()
            Catch ex1 As Exception

            End Try

            Throw New Exception(ex.Message)
        End Try
        Return isPosted
    End Function

End Class
