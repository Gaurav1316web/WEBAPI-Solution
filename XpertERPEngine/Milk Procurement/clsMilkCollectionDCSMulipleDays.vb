Imports common
Imports System.Data.SqlClient

Public Class clsMilkCollectionDCSMulipleDays
#Region "Variables"
    Public Document_No As String
    Public Document_Date As DateTime
    Public Route_Code As String
    Public Route_Name As String ''Not a table Column
    Public Tanker_No As String
    Public Vehicle_No As String
    Public MCC_Code As String
    Public MCC_Name As String ''Not a table Column
    Public MCC_Uploader_No As String ''Not a table Column
    Public Entered_Qty As Decimal
    Public Entered_FATKg As Decimal
    Public Entered_SNFKg As Decimal
    Public CLR As Decimal
    Public FAT As Decimal
    Public Description As String
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posting_Date As DateTime? = Nothing
    Public FAT_SNF_Type As Integer
    Public Trip_No As Integer
    Public Arr As List(Of clsMilkCollectionDCSMulipleDaysDetail) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsMilkCollectionDCSMulipleDays, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsMilkCollectionDCSMulipleDays, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            If isNewEntry = False Then
                If clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select isnull(Status,0) as Status from  TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS where Document_No ='" + obj.Document_No + "' ", trans)) = 1 Then
                    Throw New Exception("Posted Document [" + obj.Document_No + "]")
                End If

                HistoryUpdate(obj.Document_No, trans)
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkCollectionDCSMultipleDays, obj.MCC_Code, obj.Document_Date, trans)
            Dim qry As String = "delete from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCC_Code)
            clsCommon.AddColumnsForChange(coll, "Route_Code", obj.Route_Code, True)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No)
            clsCommon.AddColumnsForChange(coll, "Trip_No", obj.Trip_No)
            clsCommon.AddColumnsForChange(coll, "Vehicle_No", obj.Vehicle_No)
            clsCommon.AddColumnsForChange(coll, "Entered_Qty", obj.Entered_Qty)
            clsCommon.AddColumnsForChange(coll, "Entered_FATKg", obj.Entered_FATKg)
            clsCommon.AddColumnsForChange(coll, "Entered_SNFKg", obj.Entered_SNFKg)
            clsCommon.AddColumnsForChange(coll, "CLR", obj.CLR)
            clsCommon.AddColumnsForChange(coll, "FAT", obj.FAT)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "FAT_SNF_Type", obj.FAT_SNF_Type)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.MilkCollectionDCSMuliple, "", "")
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS", OMInsertOrUpdate.Update, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No='" + obj.Document_No + "'", trans)
            End If
            clsMilkCollectionDCSMulipleDaysDetail.SaveData(obj.Document_No, obj.Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS", "Document_No", "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL", "Document_No", trans)
        Return True
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkCollectionDCSMulipleDays
        Return GetData(strPONo, NavType, trans, "", "")
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal strDetailWhrlCls As String, ByVal strDetailOrderBy As String) As clsMilkCollectionDCSMulipleDays
        Dim obj As clsMilkCollectionDCSMulipleDays = Nothing
        Dim qry As String = "SELECT TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.*,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME as Route_Name , TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader
FROM TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS 
left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Route_Code 
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.MCC_Code 
where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No = (select MIN(Document_No) from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS where 1=1  )"
            Case NavigatorType.Last
                qry += " and TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No = (select Max(Document_No) from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS where 1=1  )"
            Case NavigatorType.Next
                qry += " and TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No = (select Min(Document_No) from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS where Document_No>'" + strPONo + "'  )"
            Case NavigatorType.Previous
                qry += " and TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No = (select Max(Document_No) from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS where Document_No<'" + strPONo + "'  )"
            Case NavigatorType.Current
                qry += " and TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMilkCollectionDCSMulipleDays()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
            obj.MCC_Name = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
            obj.MCC_Uploader_No = clsCommon.myCstr(dt.Rows(0)("Mcc_Code_VLC_Uploader"))
            obj.Route_Code = clsCommon.myCstr(dt.Rows(0)("Route_Code"))
            obj.Route_Name = clsCommon.myCstr(dt.Rows(0)("Route_Name"))
            obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            obj.Trip_No = clsCommon.myCstr(dt.Rows(0)("Trip_No"))
            obj.Vehicle_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_No"))
            obj.Entered_Qty = clsCommon.myCDecimal(dt.Rows(0)("Entered_Qty"))
            obj.Entered_FATKg = clsCommon.myCDecimal(dt.Rows(0)("Entered_FATKg"))
            obj.Entered_SNFKg = clsCommon.myCDecimal(dt.Rows(0)("Entered_SNFKg"))
            obj.CLR = clsCommon.myCDecimal(dt.Rows(0)("CLR"))
            obj.FAT = clsCommon.myCDecimal(dt.Rows(0)("FAT"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Status = IIf(clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.FAT_SNF_Type = clsCommon.myCDecimal(dt.Rows(0)("FAT_SNF_Type"))
            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If
            obj.Arr = clsMilkCollectionDCSMulipleDaysDetail.GetData(obj.Document_No, strDetailWhrlCls, strDetailOrderBy, trans)
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsMilkCollectionDCSMulipleDays = clsMilkCollectionDCSMulipleDays.GetData(strCode, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkCollectionDCSMultipleDays, obj.MCC_Code, obj.Document_Date, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Delete")
            End If

            If (obj.Status = ERPTransactionStatus.Approved OrElse obj.Status = ERPTransactionStatus.Posted) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If
            HistoryUpdate(strCode, trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL where Document_No='" + strCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS where Document_No='" + strCode + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    'Public Shared Function PostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
    '    Try
    '        'Throw New Exception("Auto post by mupliple days merge")
    '        If (clsCommon.myLen(strCode) <= 0) Then
    '            Throw New Exception("Document No not found to Post")
    '        End If
    '        Dim obj As clsMilkCollectionDCSMulipleDays = clsMilkCollectionDCSMulipleDays.GetData(strCode, NavigatorType.Current, trans, "", "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Collection_Date")
    '        If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
    '            Throw New Exception("Document No: " + strCode + " not found to Post")
    '        End If
    '        If (obj.Status = ERPTransactionStatus.Approved) Then
    '            Throw New Exception("Already Posted on :" + obj.Posting_Date)
    '        End If

    '        Dim coll As New Hashtable()
    '        clsCommon.AddColumnsForChange(coll, "Status", 1)
    '        clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
    '        clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
    '        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
    '        'Throw New Exception("Balwinder Singh Premi")
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function
    Public Shared Function PostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As clsMilkCollectionDCSMulipleDays = clsMilkCollectionDCSMulipleDays.GetData(strCode, NavigatorType.Current, trans, "", "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Collection_Date")
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkCollectionDCSMultipleDays, obj.MCC_Code, obj.Document_Date, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If
            Dim SettApplyMergeForDCSMultipleDays As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyMergeForDCSMultipleDays, clsFixedParameterCode.ApplyMergeForDCSMultipleDays, trans)) > 0)
            If Not SettApplyMergeForDCSMultipleDays Then
                Dim isPickCLRInsteadOfSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, trans)) > 0)
                Dim corrFactor As Decimal = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, trans))
                Dim Arr As New Dictionary(Of Date, List(Of clsMilkCollectionDCSDetail))
                Dim MaxDate As Date = obj.Arr(0).Collection_Date
                Dim TotQty As Decimal = 0
                Dim TotFATKG As Decimal = 0
                Dim TotSNFKG As Decimal = 0
                For Each objtr As clsMilkCollectionDCSMulipleDaysDetail In obj.Arr
                    If objtr.Qty <= 0 Then
                        Throw New Exception("Qty is Zero at Sample No" + clsCommon.myCstr(objtr.SNo))
                    End If
                    If objtr.FAT <= 0 Then
                        Throw New Exception("FAT is Zero at Sample No" + clsCommon.myCstr(objtr.SNo))
                    End If
                    If objtr.SNF <= 0 Then
                        Throw New Exception("SNF/CLR is Zero at Sample No" + clsCommon.myCstr(objtr.SNo))
                    End If
                    If objtr.Collection_Date > MaxDate Then
                        MaxDate = objtr.Collection_Date
                    End If
                    TotQty += objtr.Qty
                    TotFATKG += objtr.FATKG
                    Dim dclCurrSNFKG As Decimal = objtr.SNFKG
                    If isPickCLRInsteadOfSNF Then
                        Dim snfPer As Decimal = clsEkoPro.getSnfOnCalculation(objtr.FAT, objtr.SNF, corrFactor)
                        dclCurrSNFKG = objtr.Qty * snfPer / 100
                    End If
                    TotSNFKG += dclCurrSNFKG
                    Dim objDCSTr As New clsMilkCollectionDCSDetail()

                    objDCSTr.VLC_Code = objtr.VLC_Code
                    objDCSTr.Shift = objtr.Shift
                    objDCSTr.Milk_Type = objtr.Milk_Type
                    objDCSTr.Dock_Collection_Milk_Type = objtr.Dock_Collection_Milk_Type
                    objDCSTr.Qty = objtr.Qty
                    objDCSTr.FAT = objtr.FAT
                    objDCSTr.SNF = objtr.SNF
                    objDCSTr.FATKG = objtr.FATKG
                    objDCSTr.SNFKG = objtr.SNFKG
                    If Not Arr.ContainsKey(objtr.Collection_Date) Then
                        Dim ArrDCSTr As New List(Of clsMilkCollectionDCSDetail)
                        Arr.Add(objtr.Collection_Date, ArrDCSTr)
                    End If
                    objDCSTr.SNo = Arr(objtr.Collection_Date).Count + 1
                    Arr(objtr.Collection_Date).Add(objDCSTr)
                Next

                For ii As Integer = 0 To Arr.Keys.Count - 1
                    Dim ArrDCS As List(Of clsMilkCollectionDCSDetail) = Arr.Item(Arr.Keys(ii))
                    Dim objMCC As New clsMilkCollectionMCC
                    objMCC.Document_Date = Arr.Keys(ii)
                    objMCC.Late = 0
                    objMCC.Route_Code = obj.Route_Code
                    objMCC.Tanker_No = obj.Tanker_No
                    objMCC.Vehicle_No = obj.Vehicle_No
                    objMCC.Trip_No = 1
                    objMCC.Entered_Qty = 0
                    objMCC.Entered_FATKg = 0
                    objMCC.Entered_SNFKg = 0
                    objMCC.Description = "DCS Multiple Days [" + obj.Document_No + "]"
                    objMCC.Slip_No = "1"
                    objMCC.FAT_SNF_Type = obj.FAT_SNF_Type
                    objMCC.Against_DCS_Multiple_Days = obj.Document_No

                    Dim objMCCTr As New clsMilkCollectionMCCDetail
                    objMCCTr.SNo = 1
                    objMCCTr.MCC_Code = obj.MCC_Code
                    objMCCTr.Milk_Type = "Good"
                    For Each objDCSTR As clsMilkCollectionDCSDetail In ArrDCS
                        objMCC.Entered_Qty += objDCSTR.Qty
                        objMCC.Entered_FATKg += objDCSTR.FATKG
                        Dim dclCurrSNFKG As Decimal = objDCSTR.SNFKG
                        If isPickCLRInsteadOfSNF Then
                            Dim snfPer As Decimal = clsEkoPro.getSnfOnCalculation(objDCSTR.FAT, objDCSTR.SNF, corrFactor)
                            dclCurrSNFKG = objDCSTR.Qty * snfPer / 100
                        End If
                        objMCC.Entered_SNFKg += dclCurrSNFKG
                    Next
                    'Not to adjust becuase it come -ve qty,FAT,SNF MDM./2425/002845 /JAL/Kislay  
                    'If clsCommon.GetDateWithStartTime(Arr.Keys(ii)) = clsCommon.GetDateWithStartTime(MaxDate) Then
                    '    objMCC.Entered_Qty = objMCC.Entered_Qty + (obj.Entered_Qty - TotQty)
                    '    objMCC.Entered_FATKg = objMCC.Entered_FATKg + (obj.Entered_FATKg - TotFATKG)
                    '    objMCC.Entered_SNFKg = objMCC.Entered_SNFKg + (obj.Entered_SNFKg - TotSNFKG)
                    'End If
                    objMCCTr.Qty = objMCC.Entered_Qty
                    objMCCTr.FATKG = objMCC.Entered_FATKg
                    objMCCTr.SNFKG = objMCC.Entered_SNFKg

                    objMCCTr.FAT = Math.Round((clsCommon.myCDivide((objMCCTr.FATKG * 100), objMCCTr.Qty)), 2, MidpointRounding.ToEven)
                    objMCCTr.SNF = Math.Round((clsCommon.myCDivide((objMCCTr.SNFKG * 100), objMCCTr.Qty)), 2, MidpointRounding.ToEven)
                    objMCC.Arr = New List(Of clsMilkCollectionMCCDetail)
                    objMCC.Arr.Add(objMCCTr)

                    objMCC.SaveData(objMCC, True, trans)
                    clsMilkCollectionMCC.PostData(objMCC.Document_No, trans)
                    objMCC = clsMilkCollectionMCC.GetData(objMCC.Document_No, NavigatorType.Current, trans)

                    Dim objDCS As New clsMilkCollectionDCS
                    objDCS.Document_Date = Arr.Keys(ii)
                    objDCS.Description = "DCS Multiple Days [" + obj.Document_No + "]"
                    objDCS.Slip_No = "1"
                    objDCS.Arr = ArrDCS
                    objDCS.ArrMCC = New List(Of clsMilkCollectionDCSMCCDetail)
                    Dim objDCSMCC As New clsMilkCollectionDCSMCCDetail
                    objDCSMCC.Against_Milk_Collection_MCC_Detail = objMCC.Arr(0).PK_Id
                    objDCS.ArrMCC.Add(objDCSMCC)

                    objDCS.SaveData(objDCS, True, trans)
                    clsMilkCollectionDCS.PostData(objDCS.Document_No, trans)
                Next
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans, True)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String, ByVal trans As SqlTransaction, ByVal changeStatus As Boolean) As Boolean
        Try
            Dim arrAllDoc As New List(Of String)
            Dim arrShiftDetail As New List(Of clsTemp)
            GetRecursiveDoc(strDocNo, arrAllDoc, arrShiftDetail, trans)
            If arrAllDoc IsNot Nothing AndAlso arrAllDoc.Count > 0 Then
                For Each str As String In arrAllDoc
                    Dim obj As clsMilkCollectionDCSMulipleDays = clsMilkCollectionDCSMulipleDays.GetData(strDocNo, NavigatorType.Current, trans)
                    If (obj Is Nothing OrElse clsCommon.myLen(obj.Status) <= 0) Then
                        Throw New Exception("No Data found to reverse And unpost")
                    End If

                    If Not obj.Status = ERPTransactionStatus.Approved Then
                        Throw New Exception("Transaction status should be posted for reverse and unpost")
                    End If
                    If changeStatus Then
                        Dim qry As String = "select Document_No from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DOCS where Against_DCS_Multiple_Days='" + obj.Document_No + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Throw New Exception("DCS Multiple Days Document No [" + strDocNo + "] is used in DCS Multiple Days Merge Document No [" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "]")
                        End If
                    End If
                Next
            End If
            If arrShiftDetail IsNot Nothing AndAlso arrShiftDetail.Count > 0 Then
                For Each objSD As clsTemp In arrShiftDetail
                    Dim arrMCC As New ArrayList
                    arrMCC.Add(objSD.MCC_Code)
                    clsMilkShiftUploaderHead.DeleteCollectionBulk(objSD.C_Date, objSD.C_Date, " and SHIFT='" + objSD.c_Shift + "'", arrMCC, True, trans)
                Next
            End If
            If changeStatus Then
                If arrAllDoc IsNot Nothing AndAlso arrAllDoc.Count > 0 Then
                    For Each str As String In arrAllDoc
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Status", 0)
                        clsCommon.AddColumnsForChange(coll, "Posted_By", Nothing, True)
                        clsCommon.AddColumnsForChange(coll, "Posted_Date", Nothing, True)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS", OMInsertOrUpdate.Update, "Document_No='" + str + "'", trans)
                    Next
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Sub GetRecursiveDoc(strDocNo As String, ByRef arrAllDoc As List(Of String), ByRef arrShiftDetail As List(Of clsTemp), trans As SqlTransaction)
        If Not arrAllDoc.Contains(strDocNo) Then
            arrAllDoc.Add(strDocNo)
        End If

        Dim qry As String = "select Document_No,MCC_Code,Collection_Date,Shift from (
select TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Document_No,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.MCC_Code, TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Collection_Date,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Shift 
from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL  
left outer join TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS on TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Document_No
where TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Document_No='" + strDocNo + "'
)xx group by Document_No,MCC_Code,Collection_Date,Shift"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsTemp
                obj.MCC_Code = clsCommon.myCstr(dr("MCC_Code"))
                obj.C_Date = clsCommon.myCDate(dr("Collection_Date"))
                obj.c_Shift = clsCommon.myCstr(dr("Shift"))
                arrShiftDetail.Add(obj)

                qry = "select Document_No,MCC_Code,Collection_Date,Shift from ( 
select TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Document_No,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.MCC_Code, TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Collection_Date,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Shift 
from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL  
left outer join TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS on TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Document_No
where TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Document_No<>'" + clsCommon.myCstr(dr("Document_No")) + "' and TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.MCC_Code='" + clsCommon.myCstr(dr("MCC_Code")) + "' and  TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Collection_Date='" + clsCommon.GetPrintDate(clsCommon.myCDate(dr("Collection_Date")), "dd/MMM/yyyy") + "' and TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Shift='" + clsCommon.myCstr(dr("Shift")) + "'
)xx group by Document_No,MCC_Code,Collection_Date,Shift"
                Dim dtInner As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtInner IsNot Nothing AndAlso dtInner.Rows.Count > 0 Then
                    Throw New Exception("Repeated MCC Milk collection MCC [" + clsCommon.myCstr(dr("MCC_Code")) + "],Date [" + clsCommon.GetPrintDate(clsCommon.myCDate(dr("Collection_Date")), "dd/MM/yyyy") + "], Shift='" + clsCommon.myCstr(dr("Shift")) + "")
                    'For Each drInner As DataRow In dtInner.Rows
                    '    If Not arrAllDoc.Contains(clsCommon.myCstr(drInner("Document_No"))) Then
                    '        GetRecursiveDoc(clsCommon.myCstr(drInner("Document_No")), arrAllDoc, arrShiftDetail, trans)
                    '    End If
                    'Next
                End If
            Next
        End If
    End Sub

    Class clsTemp
        Public MCC_Code As String
        Public C_Date As Date
        Public c_Shift As String
    End Class
End Class

Public Class clsMilkCollectionDCSMulipleDaysDetail
#Region "Variables"
    Public PK_Id As Integer = 0
    Public Document_No As String
    Public SNo As Integer
    Public Collection_Date As Date
    Public VLC_Uploader_Code As String ''Not a Table Column
    Public VLC_Code As String
    Public VLC_Name As String ''Not a Table Column
    Public Shift As String
    Public Milk_Type As String
    Public Dock_Collection_Milk_Type As String
    Public Qty As Decimal
    Public FAT As Decimal
    Public SNF As Decimal
    Public FATKG As Decimal
    Public SNFKG As Decimal
#End Region

    'Public Shared Function AddMissing(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkCollectionDCSMulipleDaysDetail)) As Boolean
    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try
    '        Dim dtDocDate As DateTime = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Document_Date from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS where Document_No='" + strDocNo + "'", trans))
    '        SaveData(strDocNo, dtDocDate, Arr, False, trans)
    '        trans.Commit()
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkCollectionDCSMulipleDaysDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMilkCollectionDCSMulipleDaysDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Collection_Date", clsCommon.GetPrintDate(obj.Collection_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "VLC_Code", obj.VLC_Code)
                clsCommon.AddColumnsForChange(coll, "Shift", obj.Shift)
                If clsCommon.CompairString(obj.Milk_Type, "Good") = CompairStringResult.Equal OrElse clsCommon.myLen(obj.Milk_Type) <= 0 Then
                    obj.Milk_Type = "Good" ''To Handle Case sensitivity
                End If

                clsCommon.AddColumnsForChange(coll, "Milk_Type", obj.Milk_Type)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "FAT", obj.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
                clsCommon.AddColumnsForChange(coll, "FATKG", obj.FATKG)
                clsCommon.AddColumnsForChange(coll, "SNFKG", obj.SNFKG)
                clsCommon.AddColumnsForChange(coll, "Dock_Collection_Milk_Type", obj.Dock_Collection_Milk_Type)
                If obj.PK_Id > 0 Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL", OMInsertOrUpdate.Update, "PK_Id='" + clsCommon.myCstr(obj.PK_Id) + "' ", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                End If
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal strExtraWhrclas As String, ByVal strOrderByColumns As String, ByVal trans As SqlTransaction) As List(Of clsMilkCollectionDCSMulipleDaysDetail)
        Dim arr As List(Of clsMilkCollectionDCSMulipleDaysDetail) = Nothing
        Dim qry As String = "SELECT TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.*,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader  
FROM TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.VLC_Code  
where  TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Document_No='" + strPONo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        If clsCommon.myLen(strOrderByColumns) > 0 Then
            qry += " order by  " + strOrderByColumns
        Else
            qry += " ORDER BY TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.SNO"
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsMilkCollectionDCSMulipleDaysDetail)
            Dim objTr As clsMilkCollectionDCSMulipleDaysDetail
            For Each dr As DataRow In dt.Rows
                objTr = New clsMilkCollectionDCSMulipleDaysDetail
                objTr.PK_Id = clsCommon.myCDecimal(dr("PK_Id"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Collection_Date = clsCommon.myCDate(dr("Collection_Date"))
                objTr.SNo = clsCommon.myCDecimal(dr("SNo"))
                objTr.VLC_Uploader_Code = clsCommon.myCstr(dr("VLC_Code_VLC_Uploader"))
                objTr.VLC_Code = clsCommon.myCstr(dr("VLC_Code"))
                objTr.VLC_Name = clsCommon.myCstr(dr("VLC_Name"))
                objTr.Shift = clsCommon.myCstr(dr("Shift"))
                objTr.Qty = clsCommon.myCDecimal(dr("Qty"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FATKG = clsCommon.myCDecimal(dr("FATKG"))
                objTr.SNFKG = clsCommon.myCDecimal(dr("SNFKG"))
                objTr.Milk_Type = clsCommon.myCstr(dr("Milk_Type"))
                objTr.Dock_Collection_Milk_Type = clsCommon.myCstr(dr("Dock_Collection_Milk_Type"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function

    Public Shared Function DeleteData(ByVal PKID As Integer) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "select Document_No from  TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL where Document_No in ( select Document_No from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL where PK_Id = " + clsCommon.myCstr(PKID) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Invalid PK ID")
            End If
            qry = "Delete from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL where PK_Id=" + clsCommon.myCstr(PKID) + ""
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If dt.Rows.Count = 1 Then
                qry = "Delete from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS where Document_No='" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class



