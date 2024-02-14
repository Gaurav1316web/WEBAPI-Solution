Imports common
Imports System.Data.SqlClient

Public Class clsMilkCollectionDCSMulipleDaysMerge
#Region "Variables"
    Public Document_No As String
    Public Document_Date As DateTime
    Public Route_Code As String
    Public Route_Name As String ''Not a table Column
    Public Tanker_No As String
    Public Entered_Qty As Decimal
    Public Entered_FATKg As Decimal
    Public Entered_SNFKg As Decimal
    Public Description As String
    Public FAT_SNF_Type As Integer
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posting_Date As DateTime? = Nothing

    Public ArrDoc As List(Of clsMilkCollectionDCSMulipleDaysMergeDocument) = Nothing
    Public Arr As List(Of clsMilkCollectionDCSMulipleDaysMergeDayDetail) = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsMilkCollectionDCSMulipleDaysMerge, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsMilkCollectionDCSMulipleDaysMerge, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            If isNewEntry = False Then
                If clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select isnull(Status,0) as Status from  TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE where Document_No ='" + obj.Document_No + "' ", trans)) = 1 Then
                    Throw New Exception("Posted Document [" + obj.Document_No + "]")
                End If
            End If

            Dim qry As String = "delete from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DOCS where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DAY_DETAIL where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Route_Code", obj.Route_Code)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No)
            clsCommon.AddColumnsForChange(coll, "Entered_Qty", obj.Entered_Qty)
            clsCommon.AddColumnsForChange(coll, "Entered_FATKg", obj.Entered_FATKg)
            clsCommon.AddColumnsForChange(coll, "Entered_SNFKg", obj.Entered_SNFKg)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "FAT_SNF_Type", obj.FAT_SNF_Type)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.MilkCollectionDCSMulipleMerge, "", "")
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE", OMInsertOrUpdate.Update, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE.Document_No='" + obj.Document_No + "'", trans)
            End If
            clsMilkCollectionDCSMulipleDaysMergeDocument.SaveData(obj.Document_No, obj.ArrDoc, trans)
            clsMilkCollectionDCSMulipleDaysMergeDayDetail.SaveData(obj.Document_No, obj.Document_Date, obj.Arr, False, trans)
            HistoryUpdate(obj.Document_No, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE", "Document_No", "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DOCS", "Document_No", "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DAY_DETAIL", "Document_No", trans)
        Return True
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkCollectionDCSMulipleDaysMerge
        Return GetData(strPONo, NavType, trans, "", "", True)
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal strDetailWhrlCls As String, ByVal strDetailOrderBy As String, ByVal AddDetails As Boolean) As clsMilkCollectionDCSMulipleDaysMerge
        Dim obj As clsMilkCollectionDCSMulipleDaysMerge = Nothing
        Dim qry As String = "SELECT TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE.*,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME as Route_Name 
FROM TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE 
left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE.Route_Code 
where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE.Document_No = (select MIN(Document_No) from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE where 1=1  )"
            Case NavigatorType.Last
                qry += " and TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE.Document_No = (select Max(Document_No) from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE where 1=1  )"
            Case NavigatorType.Next
                qry += " and TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE.Document_No = (select Min(Document_No) from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE where Document_No>'" + strPONo + "'  )"
            Case NavigatorType.Previous
                qry += " and TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE.Document_No = (select Max(Document_No) from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE where Document_No<'" + strPONo + "'  )"
            Case NavigatorType.Current
                qry += " and TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE.Document_No = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMilkCollectionDCSMulipleDaysMerge()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Route_Code = clsCommon.myCstr(dt.Rows(0)("Route_Code"))
            obj.Route_Name = clsCommon.myCstr(dt.Rows(0)("Route_Name"))
            obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            obj.Entered_Qty = clsCommon.myCDecimal(dt.Rows(0)("Entered_Qty"))
            obj.Entered_FATKg = clsCommon.myCDecimal(dt.Rows(0)("Entered_FATKg"))
            obj.Entered_SNFKg = clsCommon.myCDecimal(dt.Rows(0)("Entered_SNFKg"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Status = IIf(clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.FAT_SNF_Type = clsCommon.myCDecimal(dt.Rows(0)("FAT_SNF_Type"))
            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If
            If AddDetails Then
                obj.ArrDoc = clsMilkCollectionDCSMulipleDaysMergeDocument.GetData(obj.Document_No, strDetailWhrlCls, strDetailOrderBy, trans)
                obj.Arr = clsMilkCollectionDCSMulipleDaysMergeDayDetail.GetData(obj.Document_No, strDetailWhrlCls, strDetailOrderBy, trans)
            End If
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsMilkCollectionDCSMulipleDaysMerge = clsMilkCollectionDCSMulipleDaysMerge.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Delete")
            End If

            If (obj.Status = ERPTransactionStatus.Approved OrElse obj.Status = ERPTransactionStatus.Posted) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If
            HistoryUpdate(strCode, trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DAY_DETAIL where Document_No='" + strCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DOCS where Document_No='" + strCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE where Document_No='" + strCode + "'", trans)
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
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As clsMilkCollectionDCSMulipleDaysMerge = clsMilkCollectionDCSMulipleDaysMerge.GetData(strCode, NavigatorType.Current, trans, "", "", True)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If
            Dim isPickCLRInsteadOfSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, trans)) > 0)
            Dim corrFactor As Decimal = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, trans))

            Dim ArrDCSMD As New List(Of clsMilkCollectionDCSMulipleDays)
            For Each objtr As clsMilkCollectionDCSMulipleDaysMergeDocument In obj.ArrDoc
                ArrDCSMD.Add(clsMilkCollectionDCSMulipleDays.GetData(objtr.Against_DCS_Multiple_Days, NavigatorType.Current, trans))
                clsMilkCollectionDCSMulipleDays.PostData(objtr.Against_DCS_Multiple_Days, trans)
            Next

            For Each objtr As clsMilkCollectionDCSMulipleDaysMergeDayDetail In obj.Arr
                If objtr.Qty <= 0 Then
                    Throw New Exception("Qty is Zero on " + clsCommon.GetPrintDate(objtr.IDate, "dd/MM/yyyy"))
                End If
                If objtr.FAT <= 0 Then
                    Throw New Exception("FAT is Zero on " + clsCommon.GetPrintDate(objtr.IDate, "dd/MM/yyyy"))
                End If
                If objtr.SNF <= 0 Then
                    Throw New Exception("SNF/CLR is Zero on " + clsCommon.GetPrintDate(objtr.IDate, "dd/MM/yyyy"))
                End If

                Dim objMCC As New clsMilkCollectionMCC
                objMCC.Document_Date = objtr.IDate
                objMCC.Late = 0
                objMCC.Route_Code = obj.Route_Code
                objMCC.Tanker_No = obj.Tanker_No
                objMCC.Vehicle_No = ""
                objMCC.Trip_No = 1
                objMCC.Entered_Qty = objtr.Qty
                objMCC.Entered_FATKg = objtr.FATKG
                objMCC.Entered_SNFKg = objtr.SNFKG
                objMCC.Description = "DCS Multiple Days Merge [" + obj.Document_No + "]"
                objMCC.Slip_No = "1"
                objMCC.FAT_SNF_Type = obj.FAT_SNF_Type
                objMCC.Against_DCS_Multiple_Days_Merge = obj.Document_No

                objMCC.Arr = New List(Of clsMilkCollectionMCCDetail)

                For Each objDCSMD As clsMilkCollectionDCSMulipleDays In ArrDCSMD
                    For Each objDCSMDTr As clsMilkCollectionDCSMulipleDaysDetail In objDCSMD.Arr
                        If objtr.IDate = objDCSMDTr.Collection_Date Then
                            Dim idx As Integer = -1
                            For ll As Integer = 0 To objMCC.Arr.Count - 1
                                If clsCommon.CompairString(objDCSMD.MCC_Code, objMCC.Arr(ll).MCC_Code) = CompairStringResult.Equal Then
                                    idx = ll
                                    Exit For
                                End If
                            Next
                            If idx >= 0 Then
                                objMCC.Arr(idx).Qty += objDCSMDTr.Qty
                                objMCC.Arr(idx).FATKG += objDCSMDTr.FATKG
                                objMCC.Arr(idx).SNFKG += objDCSMDTr.SNFKG

                                objMCC.Arr(idx).FAT = clsCommon.myCDivide(objMCC.Arr(idx).FATKG * 100, objMCC.Arr(idx).Qty)
                                objMCC.Arr(idx).SNF = clsCommon.myCDivide(objMCC.Arr(idx).SNFKG * 100, objMCC.Arr(idx).Qty)
                            Else
                                Dim objMCCTr As New clsMilkCollectionMCCDetail
                                objMCCTr.SNo = objMCC.Arr.Count + 1
                                objMCCTr.MCC_Code = objDCSMD.MCC_Code
                                objMCCTr.Milk_Type = "Good"
                                objMCCTr.Qty = objDCSMDTr.Qty
                                objMCCTr.FATKG = objDCSMDTr.FATKG
                                objMCCTr.SNFKG = objDCSMDTr.SNFKG
                                objMCCTr.FAT = objDCSMDTr.FAT
                                objMCCTr.SNF = objDCSMDTr.SNF
                                objMCCTr.Against_Multiple_Days_Merge_Day_Detail = objtr.PK_Id
                                objMCC.Arr.Add(objMCCTr)
                            End If
                        End If
                    Next
                Next
                objMCC.SaveData(objMCC, True, trans)
                clsMilkCollectionMCC.PostData(objMCC.Document_No, trans)
                objMCC = clsMilkCollectionMCC.GetData(objMCC.Document_No, NavigatorType.Current, trans)

                For Each objMCCTR As clsMilkCollectionMCCDetail In objMCC.Arr
                    Dim objDCS As New clsMilkCollectionDCS
                    objDCS.Document_Date = objtr.IDate
                    objDCS.Description = "DCS Multiple Days Merge [" + obj.Document_No + "]"
                    objDCS.Slip_No = "1"

                    objDCS.ArrMCC = New List(Of clsMilkCollectionDCSMCCDetail)
                    Dim objDCSMCC As New clsMilkCollectionDCSMCCDetail
                    objDCSMCC.Against_Milk_Collection_MCC_Detail = objMCCTR.PK_Id
                    objDCS.ArrMCC.Add(objDCSMCC)

                    objDCS.Arr = New List(Of clsMilkCollectionDCSDetail)
                    For Each objDCSMD As clsMilkCollectionDCSMulipleDays In ArrDCSMD
                        If clsCommon.CompairString(objDCSMD.MCC_Code, objMCCTR.MCC_Code) = CompairStringResult.Equal Then
                            For Each objDCSMDTr As clsMilkCollectionDCSMulipleDaysDetail In objDCSMD.Arr
                                If objtr.IDate = objDCSMDTr.Collection_Date Then
                                    Dim objDCSTr As New clsMilkCollectionDCSDetail()
                                    objDCSTr.VLC_Code = objDCSMDTr.VLC_Code
                                    objDCSTr.Shift = objDCSMDTr.Shift
                                    objDCSTr.Milk_Type = objDCSMDTr.Milk_Type
                                    objDCSTr.Dock_Collection_Milk_Type = objDCSMDTr.Dock_Collection_Milk_Type
                                    objDCSTr.Qty = objDCSMDTr.Qty
                                    objDCSTr.FAT = objDCSMDTr.FAT
                                    objDCSTr.SNF = objDCSMDTr.SNF
                                    objDCSTr.FATKG = objDCSMDTr.FATKG
                                    objDCSTr.SNFKG = objDCSMDTr.SNFKG
                                    objDCSTr.SNo = objDCS.Arr.Count + 1
                                    objDCS.Arr.Add(objDCSTr)
                                End If
                            Next
                        End If
                    Next
                    objDCS.SaveData(objDCS, True, trans)
                    clsMilkCollectionDCS.PostData(objDCS.Document_No, trans)
                Next
            Next

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
            'Throw New Exception("Balwinder Singh Premi")
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim obj As clsMilkCollectionDCSMulipleDaysMerge = clsMilkCollectionDCSMulipleDaysMerge.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Status) <= 0) Then
                Throw New Exception("No Data found to Reverse And UnPost")
            End If

            If Not obj.Status = ERPTransactionStatus.Approved Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            For Each objtr As clsMilkCollectionDCSMulipleDaysMergeDocument In obj.ArrDoc
                clsMilkCollectionDCSMulipleDays.ReverseAndUnpost(objtr.Against_DCS_Multiple_Days, trans, False)
            Next

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 0)
            clsCommon.AddColumnsForChange(coll, "Posted_By", Nothing, True)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", Nothing, True)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsMilkCollectionDCSMulipleDaysMergeDocument
#Region "Variables"
    Public PK_Id As Integer = 0
    Public Document_No As String
    Public Against_DCS_Multiple_Days As String
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkCollectionDCSMulipleDaysMergeDocument), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMilkCollectionDCSMulipleDaysMergeDocument In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Against_DCS_Multiple_Days", obj.Against_DCS_Multiple_Days)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DOCS", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal strExtraWhrclas As String, ByVal strOrderByColumns As String, ByVal trans As SqlTransaction) As List(Of clsMilkCollectionDCSMulipleDaysMergeDocument)
        Dim arr As List(Of clsMilkCollectionDCSMulipleDaysMergeDocument) = Nothing
        Dim qry As String = "SELECT TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DOCS.* 
FROM TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DOCS 
where  TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DOCS.Document_No='" + strPONo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        If clsCommon.myLen(strOrderByColumns) > 0 Then
            qry += " order by  " + strOrderByColumns
        Else
            qry += " ORDER BY TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DOCS.PK_Id"
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsMilkCollectionDCSMulipleDaysMergeDocument)
            Dim objTr As clsMilkCollectionDCSMulipleDaysMergeDocument
            For Each dr As DataRow In dt.Rows
                objTr = New clsMilkCollectionDCSMulipleDaysMergeDocument
                objTr.PK_Id = clsCommon.myCDecimal(dr("PK_Id"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Against_DCS_Multiple_Days = clsCommon.myCstr(dr("Against_DCS_Multiple_Days"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function


End Class

Public Class clsMilkCollectionDCSMulipleDaysMergeDayDetail
#Region "Variables"
    Public PK_Id As Integer = 0
    Public Document_No As String
    Public IDate As Date
    Public Qty As Decimal
    Public FAT As Decimal
    Public SNF As Decimal
    Public FATKG As Decimal
    Public SNFKG As Decimal
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsMilkCollectionDCSMulipleDaysMergeDayDetail), ByVal IsUpdatedFromCorrection As Boolean, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMilkCollectionDCSMulipleDaysMergeDayDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "IDate", clsCommon.GetPrintDate(obj.IDate, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "FAT", obj.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
                clsCommon.AddColumnsForChange(coll, "FATKG", obj.FATKG)
                clsCommon.AddColumnsForChange(coll, "SNFKG", obj.SNFKG)
                If obj.PK_Id > 0 Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DAY_DETAIL", OMInsertOrUpdate.Update, "PK_Id='" + clsCommon.myCstr(obj.PK_Id) + "' ", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DAY_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                End If
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal strExtraWhrclas As String, ByVal strOrderByColumns As String, ByVal trans As SqlTransaction) As List(Of clsMilkCollectionDCSMulipleDaysMergeDayDetail)
        Dim arr As List(Of clsMilkCollectionDCSMulipleDaysMergeDayDetail) = Nothing
        Dim qry As String = "SELECT TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DAY_DETAIL.* 
FROM TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DAY_DETAIL 
where  TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DAY_DETAIL.Document_No='" + strPONo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        If clsCommon.myLen(strOrderByColumns) > 0 Then
            qry += " order by  " + strOrderByColumns
        Else
            qry += " ORDER BY TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DAY_DETAIL.PK_Id"
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsMilkCollectionDCSMulipleDaysMergeDayDetail)
            Dim objTr As clsMilkCollectionDCSMulipleDaysMergeDayDetail
            For Each dr As DataRow In dt.Rows
                objTr = New clsMilkCollectionDCSMulipleDaysMergeDayDetail
                objTr.PK_Id = clsCommon.myCDecimal(dr("PK_Id"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.IDate = clsCommon.myCDate(dr("IDate"))
                objTr.Qty = clsCommon.myCDecimal(dr("Qty"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FATKG = clsCommon.myCDecimal(dr("FATKG"))
                objTr.SNFKG = clsCommon.myCDecimal(dr("SNFKG"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class



