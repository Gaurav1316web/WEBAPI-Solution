Imports common
Imports System.Data.SqlClient
Public Class clsTenderShortPenalty

#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime
    Public Location_Code As String = Nothing
    Public Location_Name As String = Nothing
    Public Tender_No As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public Short_Qty As Decimal = 0
    Public Short_Tolerance As Decimal = 0
    Public Penalty_Per As Decimal = 0
    Public Tender_Qty As Decimal = 0
    Public Tender_Rate As Decimal = 0
    Public Received_Qty As Decimal = 0
    Public Difference_Qty As Decimal = 0
    Public Remarks As String = Nothing
    Public Amount As Decimal = 0
    Public Tax_Group As String = Nothing
    Public TaxGroupName As String = Nothing 'Not a table field
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Decimal = 0
    Public TAX1_Base_Amt As Decimal = 0
    Public TAX1_Amt As Decimal = 0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Decimal = 0
    Public TAX2_Base_Amt As Decimal = 0
    Public TAX2_Amt As Decimal = 0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Decimal = 0
    Public TAX3_Base_Amt As Decimal = 0
    Public TAX3_Amt As Decimal = 0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Decimal = 0
    Public TAX4_Base_Amt As Decimal = 0
    Public TAX4_Amt As Decimal = 0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Decimal = 0
    Public TAX5_Base_Amt As Decimal = 0
    Public TAX5_Amt As Decimal = 0
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Decimal = 0
    Public TAX6_Base_Amt As Decimal = 0
    Public TAX6_Amt As Decimal = 0
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Decimal = 0
    Public TAX7_Base_Amt As Decimal = 0
    Public TAX7_Amt As Decimal = 0
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Decimal = 0
    Public TAX8_Base_Amt As Decimal = 0
    Public TAX8_Amt As Decimal = 0
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Decimal = 0
    Public TAX9_Base_Amt As Decimal = 0
    Public TAX9_Amt As Decimal = 0
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Decimal = 0
    Public TAX10_Base_Amt As Decimal = 0
    Public TAX10_Amt As Decimal = 0
    Public Total_Tax_Amt As Decimal = 0
    Public Total_Amt As Decimal = 0
    Public Posting_Date As DateTime? = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public CURRENCY_CODE As String = ""
    Public ConvRate As Decimal
    Public ApplicableFrom As Date? = Nothing
    Public Arr As List(Of clsTenderShortPenaltyDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsTenderShortPenalty, ByVal isNewEntry As Boolean, Optional ByVal isamendment As Boolean = False) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans, isamendment)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsTenderShortPenalty, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, Optional ByVal isamendment As Boolean = False) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_TENDER_SHORT_PENALTY_DETAIL where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.TenderShortPenalty, "", obj.Location_Code)
            End If
            If (clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Tender_No", obj.Tender_No)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Short_Qty", obj.Short_Qty)
            clsCommon.AddColumnsForChange(coll, "Short_Tolerance", obj.Short_Tolerance)
            clsCommon.AddColumnsForChange(coll, "Penalty_Per", obj.Penalty_Per)
            clsCommon.AddColumnsForChange(coll, "Tender_Qty", obj.Tender_Qty)
            clsCommon.AddColumnsForChange(coll, "Tender_Rate", obj.Tender_Rate)
            clsCommon.AddColumnsForChange(coll, "Received_Qty", obj.Received_Qty)
            clsCommon.AddColumnsForChange(coll, "Difference_Qty", obj.Difference_Qty)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
            clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
            clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
            clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
            clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
            clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
            clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
            clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
            clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
            clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
            clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ConvRate", obj.ConvRate)
            If clsCommon.myLen(obj.ApplicableFrom) > 0 Then
                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", clsCommon.GetPrintDate(obj.ApplicableFrom, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TENDER_SHORT_PENALTY", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TENDER_SHORT_PENALTY", OMInsertOrUpdate.Update, "TSPL_TENDER_SHORT_PENALTY.Document_No='" + obj.Document_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsTenderShortPenaltyDetail.SaveData(obj.Document_No, Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsTenderShortPenalty
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsTenderShortPenalty
        Dim obj As clsTenderShortPenalty = Nothing
        Dim qry As String = "SELECT TSPL_TENDER_SHORT_PENALTY.* ,TSPL_LOCATION_MASTER.Location_Desc as Location_Name,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_ITEM_MASTER.Item_Desc,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc, 
         FROM TSPL_TENDER_SHORT_PENALTY 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TENDER_SHORT_PENALTY.Location_Code 
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_TENDER_SHORT_PENALTY.Vendor_Code 
left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_TENDER_SHORT_PENALTY.Tax_Group 
left outer join TSPL_ITEM_MASTER  on TSPL_ITEM_MASTER.Item_Code=TSPL_TENDER_SHORT_PENALTY.Item_Code where 2=2"
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " And Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " And TSPL_TENDER_SHORT_PENALTY.Document_No = (select MIN(Document_No) from TSPL_TENDER_SHORT_PENALTY WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " And TSPL_TENDER_SHORT_PENALTY.Document_No = (select Max(Document_No) from TSPL_TENDER_SHORT_PENALTY WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Next
                qry += " And TSPL_TENDER_SHORT_PENALTY.Document_No = (select Min(Document_No) from TSPL_TENDER_SHORT_PENALTY where Document_No>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_TENDER_SHORT_PENALTY.Document_No = (select Max(Document_No) from TSPL_TENDER_SHORT_PENALTY where Document_No<'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_TENDER_SHORT_PENALTY.Document_No = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsTenderShortPenalty()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Name"))
            obj.Tender_No = clsCommon.myCstr(dt.Rows(0)("Tender_No"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            obj.Item_Name = clsCommon.myCstr(dt.Rows(0)("Item_Name"))
            obj.Short_Qty = clsCommon.myCDecimal(dt.Rows(0)("Short_Qty"))
            obj.Short_Tolerance = clsCommon.myCDecimal(dt.Rows(0)("Short_Tolerance"))
            obj.Penalty_Per = clsCommon.myCDecimal(dt.Rows(0)("Penalty_Per"))
            obj.Tender_Qty = clsCommon.myCDecimal(dt.Rows(0)("Tender_Qty"))
            obj.Tender_Rate = clsCommon.myCDecimal(dt.Rows(0)("Tender_Rate"))
            obj.Received_Qty = clsCommon.myCDecimal(dt.Rows(0)("Received_Qty"))
            obj.Difference_Qty = clsCommon.myCDecimal(dt.Rows(0)("Difference_Qty"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Amount = clsCommon.myCDecimal(dt.Rows(0)("Amount"))
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
            obj.TAX1_Rate = clsCommon.myCDecimal(dt.Rows(0)("TAX1_Rate"))
            obj.TAX1_Base_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX1_Base_Amt"))
            obj.TAX1_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX1_Amt"))
            obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
            obj.TAX2_Rate = clsCommon.myCDecimal(dt.Rows(0)("TAX2_Rate"))
            obj.TAX2_Base_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX2_Base_Amt"))
            obj.TAX2_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX2_Amt"))
            obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
            obj.TAX3_Base_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX3_Base_Amt"))
            obj.TAX3_Rate = clsCommon.myCDecimal(dt.Rows(0)("TAX3_Rate"))
            obj.TAX3_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX3_Amt"))
            obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
            obj.TAX4_Rate = clsCommon.myCDecimal(dt.Rows(0)("TAX4_Rate"))
            obj.TAX4_Base_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX4_Base_Amt"))
            obj.TAX4_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX4_Amt"))
            obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
            obj.TAX5_Rate = clsCommon.myCDecimal(dt.Rows(0)("TAX5_Rate"))
            obj.TAX5_Base_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX5_Base_Amt"))
            obj.TAX5_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX5_Amt"))
            obj.TAX6 = clsCommon.myCstr(dt.Rows(0)("TAX6"))
            obj.TAX6_Rate = clsCommon.myCDecimal(dt.Rows(0)("TAX6_Rate"))
            obj.TAX6_Base_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX6_Base_Amt"))
            obj.TAX6_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX6_Amt"))
            obj.TAX7 = clsCommon.myCstr(dt.Rows(0)("TAX7"))
            obj.TAX7_Rate = clsCommon.myCDecimal(dt.Rows(0)("TAX7_Rate"))
            obj.TAX7_Base_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX7_Base_Amt"))
            obj.TAX7_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX7_Amt"))
            obj.TAX8 = clsCommon.myCstr(dt.Rows(0)("TAX8"))
            obj.TAX8_Rate = clsCommon.myCDecimal(dt.Rows(0)("TAX8_Rate"))
            obj.TAX8_Base_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX8_Base_Amt"))
            obj.TAX8_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX8_Amt"))
            obj.TAX9 = clsCommon.myCstr(dt.Rows(0)("TAX9"))
            obj.TAX9_Rate = clsCommon.myCDecimal(dt.Rows(0)("TAX9_Rate"))
            obj.TAX9_Base_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX9_Base_Amt"))
            obj.TAX9_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX9_Amt"))
            obj.TAX10 = clsCommon.myCstr(dt.Rows(0)("TAX10"))
            obj.TAX10_Rate = clsCommon.myCDecimal(dt.Rows(0)("TAX10_Rate"))
            obj.TAX10_Base_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX10_Base_Amt"))
            obj.TAX10_Amt = clsCommon.myCDecimal(dt.Rows(0)("TAX10_Amt"))

            obj.Total_Tax_Amt = clsCommon.myCDecimal(dt.Rows(0)("Total_Tax_Amt"))
            obj.Total_Amt = clsCommon.myCDecimal(dt.Rows(0)("Total_Amt"))
            obj.Status = IIf(clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
            End If
            '' CURRENCYCONVERSION 
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.ConvRate = clsCommon.myCDecimal(dt.Rows(0)("ConvRate"))
            If IsDBNull(dt.Rows(0)("ApplicableFrom")) = True Then
                obj.ApplicableFrom = Nothing
            Else
                obj.ApplicableFrom = clsCommon.GetPrintDate(dt.Rows(0)("ApplicableFrom"), "dd/MMM/yyyy")
            End If
            '' END CURRENCYCONVERSION 

            qry = "SELECT TSPL_TENDER_SHORT_PENALTY_DETAIL.* FROM TSPL_TENDER_SHORT_PENALTY_DETAIL  where TSPL_TENDER_SHORT_PENALTY_DETAIL.Document_No='" + obj.Document_No + "' ORDER BY TSPL_TENDER_SHORT_PENALTY_DETAIL.PK_ID"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsTenderShortPenaltyDetail)
                Dim objTr As clsTenderShortPenaltyDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsTenderShortPenaltyDetail
                    objTr.SRN_ID = clsCommon.myCstr(dr("SRN_ID"))
                    objTr.Document_No = clsCommon.myCDecimal(dr("Accept_Qty"))
                    objTr.SRN_Qty = clsCommon.myCDecimal(dr("Reject_Qty"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function
    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Return PostData(strDocNo, True, trans)
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("MRN No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim obj As clsTenderShortPenalty = clsTenderShortPenalty.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim qry As String = "Update TSPL_TENDER_SHORT_PENALTY set Status=1, Post_Date='" + strPostDate + "',Post_By='" + objCommonVar.CurrentUserCode + "' where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            trans.Commit()
            DeleteData(strCode, trans)
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Purchase Order No not found to Delete")
        End If
        Dim obj As clsTenderShortPenalty = clsTenderShortPenalty.GetData(strCode, NavigatorType.Current, trans)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                clsMRNAdditionChargeInsurance.DeleteData(obj.Document_No, trans)
                Dim qry As String = "delete from TSPL_TENDER_SHORT_PENALTY_DETAIL where Document_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_TENDER_SHORT_PENALTY where Document_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
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
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "select 1 from TSPL_TENDER_SHORT_PENALTY where Document_No='" + strCode + "' and Status=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Transaction status should be posted.")
            End If
            qry = "select distinct SRN_No from TSPL_SRN_DETAIL where MRN_Id ='" + strCode + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                qry = "MRN is used in following SRN"
                For Each dr As DataRow In dt.Rows
                    qry += Environment.NewLine + clsCommon.myCstr(dr("SRN_No"))
                Next
                qry += Environment.NewLine + "Can't unpost it"
                Throw New Exception(qry)
            End If
            qry = "update TSPL_TENDER_SHORT_PENALTY set Status=0,Posting_Date=null where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsTenderShortPenaltyDetail
#Region "Variables"
    Public Document_No As String = Nothing
    Public SRN_ID As String = Nothing
    Public SRN_Qty As Decimal = 0
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsTenderShortPenaltyDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsTenderShortPenaltyDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "SRN_ID", obj.SRN_ID)
                clsCommon.AddColumnsForChange(coll, "SRN_Qty", obj.SRN_Qty)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TENDER_SHORT_PENALTY_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class
