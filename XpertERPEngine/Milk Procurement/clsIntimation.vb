' Created By Pankaj Jha on 03/07/204 Against Ticket No: BM00000002720
Imports System.Data.SqlClient
Imports common
Public Class clsIntimation
    Public CONTRACTOR_CODE As String = Nothing
    Public Supplier_Code As String = Nothing
    Public Dispatch_Centre_Code As String = Nothing
    Public MIKL_TYPE_CODE As String = Nothing
    Public Status As String = Nothing
    Public Arr As List(Of clsIntimationChamberDetails) = Nothing
    Public isNewEntry As Boolean = False
    Public Intimation_No As String = String.Empty
    Public Date_And_Time As Date = Nothing
    Public Challan_No As String = String.Empty
    Public Challan_Date As Date = Nothing
    Public Tanker_No As String = String.Empty
    Public isPosted As Integer = 0
    Public Posting_Date As Date = Nothing
    Public location_Code As String = String.Empty
    Public Vendor_Code As String = String.Empty
    Public Item_Code As String = String.Empty
    Public TotalQty_In_Kg As Double = 0
    Public UOM As String = String.Empty



    Public Shared Function saveData(ByVal obj As clsIntimation, ByVal trans As SqlTransaction, Optional ByVal isHistory As Boolean = False) As Boolean
        Try
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Procurement Bulk", "Intimation No", obj.location_Code, obj.Date_And_Time, trans)
            Dim issaved As Boolean = True
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Intimation_No", obj.Intimation_No)

            '==============================Added by preeti gupta======================
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Date_And_Time", clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Date_And_Time", clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MMM/yyyy"))
            End If
            '===================================End=======================================

            clsCommon.AddColumnsForChange(coll, "Challan_No", obj.Challan_No)
            clsCommon.AddColumnsForChange(coll, "Challan_Date", clsCommon.GetPrintDate(obj.Challan_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No, True)
            clsCommon.AddColumnsForChange(coll, "isPosted", obj.isPosted)
            If obj.isPosted = 1 Then
                clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Posting_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "location_Code", obj.location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code, True)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
            clsCommon.AddColumnsForChange(coll, "TotalQty_In_Kg", obj.TotalQty_In_Kg)
            clsCommon.AddColumnsForChange(coll, "Supplier_Code", obj.Supplier_Code, True)
            clsCommon.AddColumnsForChange(coll, "CONTRACTOR_CODE", obj.CONTRACTOR_CODE, True)
            clsCommon.AddColumnsForChange(coll, "Dispatch_Centre_Code", obj.Dispatch_Centre_Code)
            clsCommon.AddColumnsForChange(coll, "MIKL_TYPE_CODE", obj.MIKL_TYPE_CODE, True)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Intimation_Master", OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Intimation_Master", OMInsertOrUpdate.Update, "TSPL_Intimation_Master.Intimation_No='" + obj.Intimation_No + "'", trans)
            End If
            issaved = issaved AndAlso clsIntimationChamberDetails.SaveData(obj.Intimation_No, obj.Arr, trans)
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try

            Dim qry As String = " select TSPL_Intimation_Master.Intimation_No as [Code] ,convert(varchar,TSPL_Intimation_Master.Date_And_Time,103) as [Date],TSPL_Intimation_Master.Status ,TSPL_Intimation_Master.Challan_No as [Challan No] ,convert(varchar,TSPL_Intimation_Master.Challan_Date,103) as [Challan Date] ,TSPL_Intimation_Master.Tanker_No as [Tanker No] ,TSPL_Intimation_Master.Supplier_Code as [Supplier Code] ,case when TSPL_Intimation_Master.isPosted=0 then 'NO' else 'YES' end as [Posting Status],TSPL_Intimation_Master.location_Code as [Location Code] ,TSPL_Intimation_Master.Vendor_Code as [Vendor Code] ,TSPL_Intimation_Master.Item_Code as [Item Code] ,TSPL_Intimation_Master.TotalQty_In_Kg as [Qty In Kg] ,TSPL_Intimation_Master.Created_By as [Created By] ,cast(convert(date,TSPL_Intimation_Master.Created_Date,103) as varchar) as [Created Date] ,TSPL_Intimation_Master.Modify_By as [Modify By] ,cast(convert(date,TSPL_Intimation_Master.Modify_Date,103)as varchar) as [Modify Date] ,TSPL_Intimation_Master.comp_code as [Company Code]From TSPL_Intimation_Master  "
            str = clsCommon.ShowSelectForm("IntimationENTRY", qry, "Code", whrcls, curcode, "Code", isButtonClicked)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function getTankerFinder(ByVal whrcls As String, ByVal curcode As String) As String
        Dim str As String = ""
        Try
            'Dim dt As DataRow

            Dim qry As String = " select TSPL_Intimation_Master.Tanker_No as [TankerNo] , TSPL_Intimation_Master.Intimation_No as [GateEntryNo] ,TSPL_Intimation_Master.Doc_Type as [Doc Type] ,TSPL_Intimation_Master.Date_And_Time as [Date And Time] ,TSPL_Intimation_Master.Challan_No as [Challan No] ,TSPL_Intimation_Master.Challan_Date as [Challan Date] ,TSPL_Intimation_Master.Dispatched_From_Mcc as [Dispatched From Mcc] ,TSPL_Intimation_Master.location_Code as [Location Code] ,TSPL_Intimation_Master.Location_Desc as [Location Desc] ,TSPL_Intimation_Master.Vendor_Code as [Vendor Code] ,TSPL_Intimation_Master.Vendor_Desc as [Vendor Desc] ,TSPL_Intimation_Master.Item_Code as [Item Code] ,TSPL_Intimation_Master.Item_Desc as [Item Desc] ,TSPL_Intimation_Master.Qty_In_Kg as [Qty In Kg] ,TSPL_Intimation_Master.snf_Per as [SNF Per] ,TSPL_Intimation_Master.fat_per as [FAT Per] ,TSPL_Intimation_Master.Created_By as [Created By] ,TSPL_Intimation_Master.Created_Date as [Created Date] ,TSPL_Intimation_Master.Modify_By as [Modify By] ,TSPL_Intimation_Master.Modify_Date as [Modify Date] ,TSPL_Intimation_Master.comp_code as [Company Code]  From TSPL_Intimation_Master  "
            str = customFinder.getFinder("TNKRFNDGT", qry, whrcls, "TankerNo", curcode, "GateEntryNo")

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
    End Function


    Public Shared Function getItemName(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim strDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_ITEM_MASTER.Item_Desc from TSPL_ITEM_MASTER where item_code='" & strCode & "'", trans))
        Return strDesc
    End Function


    Public Shared Function getUsersDefaultLocation(Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim strLoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location  from TSPL_USER_MASTER   where user_code='" & objCommonVar.CurrentUserCode & "'", trans))
        Return strLoc
    End Function

    Public Shared Function postData(ByVal strIntimationNo As String, ByVal docType As String, ByVal formId As String) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(strIntimationNo) <= 0) Then
                Throw New Exception("Intimation No No not found to Post")
            End If

            Dim obj As clsIntimation = clsIntimation.getData(strIntimationNo, docType, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Intimation_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            trans = clsDBFuncationality.GetTransactin()
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Intimation No", "Intimation No", obj.location_Code, obj.Date_And_Time, trans)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            '--------------------
            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(formId, "TSPL_Intimation_Master", "Intimation_No", obj.Intimation_No, trans)
            If isResult = False Then
                trans.Commit()
                Return False
            End If
            Dim strQry As String = " update TSPL_Intimation_Master set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where Intimation_No='" & strIntimationNo & "'"
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            If clsCommon.CompairString(obj.Status, "A") = CompairStringResult.Equal Then
                isPosted = ConvertIntimationToGateEntry(obj, trans)
            End If

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

    Public Shared Function deleteData(ByVal strIntimationNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_Intimation_Master where  Intimation_No='" & strIntimationNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function getData(ByVal strCode As String, ByVal docType As String, ByVal navtype As NavigatorType) As clsIntimation
        Dim obj As New clsIntimation
        Try
            Dim qst As String = " select *   From TSPL_Intimation_Master   where 1=1 "
            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO() AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " and location_code in (" & objCommonVar.strCurrUserLocations & ") "
            End If
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_Intimation_Master.Intimation_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_Intimation_Master.Intimation_No in (select min(Intimation_No ) from TSPL_Intimation_Master where Intimation_No  >'" + strCode + "' " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_Intimation_Master.Intimation_No in (select MIN(Intimation_No ) from TSPL_Intimation_Master where " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_Intimation_Master.Intimation_No in (select Max(Intimation_No ) from TSPL_Intimation_Master where  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_Intimation_Master.Intimation_No in (select Max(Intimation_No ) from TSPL_Intimation_Master where Intimation_No  <'" + strCode + "' " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Intimation_No = clsCommon.myCstr(dt.Rows(0)("Intimation_No"))
                obj.Date_And_Time = clsCommon.myCDate(dt.Rows(0)("Date_And_Time"))
                obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
                obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"))
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If
                obj.location_Code = clsCommon.myCstr(dt.Rows(0)("location_Code"))
                obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.TotalQty_In_Kg = clsCommon.myCdbl(dt.Rows(0)("TotalQty_In_Kg"))
                obj.Supplier_Code = clsCommon.myCstr(dt.Rows(0)("Supplier_Code"))
                obj.CONTRACTOR_CODE = clsCommon.myCstr(dt.Rows(0)("CONTRACTOR_CODE"))
                obj.Dispatch_Centre_Code = clsCommon.myCstr(dt.Rows(0)("Dispatch_Centre_Code"))
                obj.MIKL_TYPE_CODE = clsCommon.myCstr(dt.Rows(0)("MIKL_TYPE_CODE"))
                obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
                obj.Arr = clsIntimationChamberDetails.GetData(obj.Intimation_No, Nothing)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Private Shared Function ConvertIntimationToGateEntry(ByVal objIntimation As clsIntimation, ByVal trans As SqlTransaction) As Boolean
        Dim obj As New clsGateEntry()
        obj = New clsGateEntry()
        obj.Gate_Entry_No = clsERPFuncationality.GetNextCode(trans, objIntimation.Date_And_Time, clsDocType.GateEntry, clsDocTransactionType.BulkProc, objIntimation.location_Code)
        obj.Doc_Type = "BulkProc"
        obj.Date_And_Time = objIntimation.Date_And_Time
        obj.Challan_No = objIntimation.Challan_No
        obj.Challan_Date = objIntimation.Challan_Date
        obj.Tanker_No = objIntimation.Tanker_No
        obj.isPosted = 0
        If obj.isPosted = 1 Then
            obj.Posting_Date = objIntimation.Posting_Date
        End If
        obj.Dispatched_From_Mcc = ""
        obj.location_Code = objIntimation.location_Code
        obj.UOM = objIntimation.UOM
        obj.Location_Desc = clsLocation.GetName(objIntimation.location_Code, trans)
        obj.Vendor_Code = objIntimation.Vendor_Code
        obj.Vendor_Desc = clsVendorMaster.GetName(objIntimation.Vendor_Code, trans)
        obj.Item_Code = objIntimation.Item_Code
        obj.Item_Desc = clsIntimation.getItemName(objIntimation.Item_Code, trans)
        obj.Qty_In_Kg = 0
        obj.snf_Per = 0
        obj.fat_per = 0
        obj.TotalQty_In_Kg = objIntimation.TotalQty_In_Kg
        obj.Intimation_No = objIntimation.Intimation_No
        obj.Supplier_Code = objIntimation.Supplier_Code
        obj.Dispatch_Centre_Code = objIntimation.Dispatch_Centre_Code
        obj.MIKL_TYPE_CODE = objIntimation.MIKL_TYPE_CODE
        obj.Intimation_Status = ""
        obj.Gate_Entry_Type = "P"
        obj.Seal_Status = ""
        obj.isNewEntry = True
        If (objIntimation.Arr IsNot Nothing AndAlso objIntimation.Arr.Count > 0) Then
            obj.Arr = New List(Of clsGateEntryChemberNoDetails)
            Dim objTr As clsGateEntryChemberNoDetails
            For Each objIntimationDetail As clsIntimationChamberDetails In objIntimation.Arr
                objTr = New clsGateEntryChemberNoDetails
                objTr.Line_No = objIntimationDetail.Line_No
                objTr.Item_Code = objIntimationDetail.Item_Code
                objTr.DIP_Status = ""
                objTr.Sample_Lifted = ""
                objTr.UOM = objIntimationDetail.UOM
                objTr.Chamber_Qty = objIntimationDetail.Chamber_Qty
                objTr.Chamber_Desc = objIntimationDetail.Chamber_Desc
                objTr.fat_per = objIntimationDetail.fat_per
                objTr.snf_Per = objIntimationDetail.snf_Per
                objTr.MIKL_TYPE_CODE = objIntimation.MIKL_TYPE_CODE
                obj.Arr.Add(objTr)
            Next
        End If
        clsGateEntry.saveData(obj, trans)
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strDocNo) <= 0 Then
                Throw New Exception("Please select a Intimation No No")
            End If
            Dim Qry As String = "select isPosted from TSPL_Intimation_Master where Intimation_No='" + strDocNo + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If
            Dim isUsed As Integer = clsDBFuncationality.getSingleValue("select SUM(row_Count ) from (select COUNT(*) as row_Count from  TSPL_Weighment_Detail where Intimation_No='" & strDocNo & "' union all select COUNT(*) as row_Count from tspl_quality_check where Intimation_No='" & strDocNo & "') xx ", trans)
            If isUsed > 0 Then
                Throw New Exception("Intimation No No is in use")
            End If
            Qry = "Update TSPL_Intimation_Master set isPosted = 0,Posting_Date=null where Intimation_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class


Public Class clsIntimationChamberDetails
#Region "Variables"
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public UOM As String = Nothing
    Public Chamber_Desc As String = Nothing
    Public Chamber_Qty As Integer = 0
    Public snf_Per As Double = 0
    Public fat_per As Double = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsIntimationChamberDetails), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim sQuery As String = "Delete from TSPL_Intimation_Chamber_Detail where Intimation_No='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            For Each obj As clsIntimationChamberDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Intimation_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "Chamber_Desc", obj.Chamber_Desc)
                clsCommon.AddColumnsForChange(coll, "Chamber_Qty", obj.Chamber_Qty)
                clsCommon.AddColumnsForChange(coll, "snf_Per", obj.snf_Per)
                clsCommon.AddColumnsForChange(coll, "fat_per", obj.fat_per)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Intimation_Chamber_Detail", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsIntimationChamberDetails)
        Dim arr As List(Of clsIntimationChamberDetails) = Nothing
        Dim qry As String
        qry = "select TSPL_Intimation_Chamber_Detail.Line_No,TSPL_Intimation_Chamber_Detail.UOM,TSPL_Intimation_Chamber_Detail.Item_Code,TSPL_Intimation_Chamber_Detail.snf_Per,TSPL_Intimation_Chamber_Detail.fat_per,TSPL_Intimation_Chamber_Detail.Chamber_Desc,TSPL_Intimation_Chamber_Detail.Chamber_Qty from  TSPL_Intimation_Chamber_Detail where TSPL_Intimation_Chamber_Detail.Intimation_No='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsIntimationChamberDetails)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsIntimationChamberDetails = New clsIntimationChamberDetails()
                obj.Line_No = clsCommon.myCstr(dr("Line_No"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.UOM = clsCommon.myCstr(dr("UOM"))
                obj.snf_Per = clsCommon.myCdbl(dr("snf_Per"))
                obj.fat_per = clsCommon.myCdbl(dr("fat_per"))
                obj.Chamber_Qty = clsCommon.myCdbl(dr("Chamber_Qty"))
                obj.Chamber_Desc = clsCommon.myCstr(dr("Chamber_Desc"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class
