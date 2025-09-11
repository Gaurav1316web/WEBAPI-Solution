Imports System.Data.SqlClient

Public Class clsCustomerTenderOrder
#Region "Variables"
    Public Document_Code As String = ""
    Public Document_Date As DateTime = Nothing
    Public RAL_No As String = Nothing
    Public Cust_Code As String = Nothing
    Public Location_Code As String = Nothing
    Public Sub_Location As String = Nothing
    Public Ref_No As String = Nothing
    Public Ref_Date As DateTime = Nothing
    Public Tax_Group As String = Nothing
    Public TaxGroupName As String = Nothing
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Double = 0
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Double = 0
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Double = 0
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Double = 0
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Double = 0
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Double = 0
    Public TAX6_Base_Amt As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Double = 0
    Public TAX7_Base_Amt As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Double = 0
    Public TAX8_Base_Amt As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Double = 0
    Public TAX9_Base_Amt As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Double = 0
    Public TAX10_Base_Amt As Double = 0
    Public TAX10_Amt As Double = 0
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public Doc_Amt_Without_Tax As Double = 0
    Public Tax_Amt As Double = 0
    Public Document_Amt As Double = 0
    Public Remarks As String = ""
    Public Status As Integer = 0
    Public Posted_Date As DateTime = Nothing
    Public Arr As List(Of clsCustomerTenderOrderDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsCustomerTenderOrder, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As clsCustomerTenderOrder, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_CUSTOMER_TENDER_ORDER_DETAIL where Document_Code='" & obj.Document_Code & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "RAL_No", obj.RAL_No)
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Sub_Location", obj.Sub_Location, True)
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Ref_Date", clsCommon.GetPrintDate(obj.Ref_Date, "dd/MMM/yyyy"), True)
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
            clsCommon.AddColumnsForChange(coll, "Doc_Amt_Without_Tax", obj.Doc_Amt_Without_Tax)
            clsCommon.AddColumnsForChange(coll, "Tax_Amt", obj.Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Document_Amt", obj.Document_Amt)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmCustomerTenderOrder, "", obj.Location_Code)
                If clsCommon.myLen(obj.Document_Code) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_TENDER_ORDER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_TENDER_ORDER", OMInsertOrUpdate.Update, "Document_Code='" & clsCommon.myCstr(obj.Document_Code) & "'", trans)
            End If
            clsCustomerTenderOrderDetail.SaveData(obj.Document_Code, obj.Arr, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_CUSTOMER_TENDER_ORDER", "Document_Code", "TSPL_CUSTOMER_TENDER_ORDER_DETAIL", "Document_Code", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCustomerTenderOrder
        Dim obj As clsCustomerTenderOrder = Nothing
        Try
            Dim Whrcls As String = ""
            Dim strQry As String = "select Document_Code,Document_Date,RAL_No,Cust_Code,Location_Code,Sub_Location,Ref_No,Ref_Date,Document_Amt,Remarks,Status,Posted_Date " &
                ",Tax_Group,TaxGroupName,Tax1,Tax1_Rate,Tax1_Base_Amt,TAX1_Amt,Tax2,Tax2_Rate,Tax2_Base_Amt,TAX2_Amt,Tax3,Tax3_Rate,Tax3_Base_Amt,TAX3_Amt,Tax4,Tax4_Rate,Tax4_Base_Amt,TAX4_Amt,Tax5,Tax5_Rate,Tax5_Base_Amt,TAX5_Amt,Tax6,Tax6_Rate,Tax6_Base_Amt,Tax6_Amt,Tax7,Tax7_Rate,Tax7_Base_Amt,Tax7_Amt,Tax8,Tax8_Rate,Tax8_Base_Amt,Tax8_Amt,Tax9,Tax9_Rate,Tax9_Base_Amt,Tax9_Amt,Tax10,Tax10_Rate,Tax10_Base_Amt,Tax10_Amt,Doc_Amt_Without_Tax,Tax_Amt  " &
                " From TSPL_CUSTOMER_TENDER_ORDER  where 2=2"
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and TSPL_CUSTOMER_TENDER_ORDER.Document_Code = (select MIN(Document_Code) from TSPL_CUSTOMER_TENDER_ORDER where 1=1 " & Whrcls & "  )"
                Case NavigatorType.Last
                    strQry += " and TSPL_CUSTOMER_TENDER_ORDER.Document_Code = (select Max(Document_Code) from TSPL_CUSTOMER_TENDER_ORDER where 1=1 " & Whrcls & "  )"
                Case NavigatorType.Next
                    strQry += " and TSPL_CUSTOMER_TENDER_ORDER.Document_Code = (select Min(Document_Code) from TSPL_CUSTOMER_TENDER_ORDER where Document_Code>'" & clsCommon.myCstr(strCode) & "' " & Whrcls & "   )"
                Case NavigatorType.Previous
                    strQry += " and TSPL_CUSTOMER_TENDER_ORDER.Document_Code = (select Max(Document_Code) from TSPL_CUSTOMER_TENDER_ORDER where Document_Code<'" & clsCommon.myCstr(strCode) & "' " & Whrcls & "  )"
                Case NavigatorType.Current
                    strQry += " and TSPL_CUSTOMER_TENDER_ORDER.Document_Code = '" & clsCommon.myCstr(strCode) & "'  " & Whrcls & " "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsCustomerTenderOrder()
                obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.Document_Date = clsCommon.GetPrintDate(dt.Rows(0)("Document_Date"), "dd/MMM/yyyy")
                obj.RAL_No = clsCommon.myCstr(dt.Rows(0)("RAL_No"))
                obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
                obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                obj.Sub_Location = clsCommon.myCstr(dt.Rows(0)("Sub_Location"))
                obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
                If dt.Rows(0)("Ref_Date") IsNot DBNull.Value Then
                    obj.Ref_Date = clsCommon.GetPrintDate(dt.Rows(0)("Ref_Date"), "dd/MMM/yyyy")
                End If
                obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
                obj.TaxGroupName = clsCommon.myCstr(dt.Rows(0)("TaxGroupName"))
                obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
                obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
                obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Base_Amt"))
                obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
                obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
                obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
                obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Base_Amt"))
                obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
                obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
                obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
                obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Base_Amt"))
                obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
                obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
                obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
                obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Base_Amt"))
                obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
                obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
                obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
                obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Base_Amt"))
                obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
                obj.TAX6 = clsCommon.myCstr(dt.Rows(0)("Tax6"))
                obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0)("Tax6_Rate"))
                obj.TAX6_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax6_Base_Amt"))
                obj.TAX6_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax6_Amt"))
                obj.TAX7 = clsCommon.myCstr(dt.Rows(0)("Tax7"))
                obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0)("Tax7_Rate"))
                obj.TAX7_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax7_Base_Amt"))
                obj.TAX7_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax7_Amt"))
                obj.TAX8 = clsCommon.myCstr(dt.Rows(0)("Tax8"))
                obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0)("Tax8_Rate"))
                obj.TAX8_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax8_Base_Amt"))
                obj.TAX8_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax8_Amt"))
                obj.TAX9 = clsCommon.myCstr(dt.Rows(0)("Tax9"))
                obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0)("Tax9_Rate"))
                obj.TAX9_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax9_Base_Amt"))
                obj.TAX9_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax9_Amt"))
                obj.TAX10 = clsCommon.myCstr(dt.Rows(0)("Tax10"))
                obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0)("Tax10_Rate"))
                obj.TAX10_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax10_Base_Amt"))
                obj.TAX10_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax10_Amt"))
                obj.Doc_Amt_Without_Tax = clsCommon.myCdbl(dt.Rows(0)("Doc_Amt_Without_Tax"))
                obj.Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax_Amt"))
                obj.Document_Amt = clsCommon.myCdbl(dt.Rows(0)("Document_Amt"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Status = IIf(clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                    obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
                End If
                obj.Arr = clsCustomerTenderOrderDetail.GetData(obj.Document_Code, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
    Public Shared Function PostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim obj As clsCustomerTenderOrder = clsCustomerTenderOrder.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Code : " & strCode & " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" & obj.Posted_Date)
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_TENDER_ORDER", OMInsertOrUpdate.Update, "Document_Code='" & clsCommon.myCstr(obj.Document_Code) & "'", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_CUSTOMER_TENDER_ORDER", "Document_Code", "TSPL_CUSTOMER_TENDER_ORDER_DETAIL", "Document_Code", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean
        Dim obj As New clsCustomerTender()
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strCode, "TSPL_CUSTOMER_TENDER_ORDER", "Document_Code", "TSPL_CUSTOMER_TENDER_ORDER_DETAIL", "Document_Code", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_CUSTOMER_TENDER_ORDER", "Document_Code", "TSPL_CUSTOMER_TENDER_ORDER_DETAIL", "Document_Code", trans)
            Dim isPosted As Integer = 0
            isPosted = clsDBFuncationality.getSingleValue("SELECT Count(*) FROM TSPL_CUSTOMER_TENDER_ORDER where Document_Code = '" & strCode & "' and Status=1", trans)
            If (isPosted = 1) Then
                Throw New Exception("Already Posted on :" & obj.Posted_Date)
            End If
            Dim qry As String
            qry = "delete from TSPL_CUSTOMER_TENDER_ORDER_DETAIL where Document_Code ='" & strCode & "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_CUSTOMER_TENDER_ORDER where Document_Code ='" & strCode & "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
End Class
Public Class clsCustomerTenderOrderDetail
#Region "Variables"
    Public Document_Code As String = ""
    Public RowType As String = ""
    Public Item_Code As String = ""
    Public Unit_Code As String = ""
    Public Tender_Rate As Double = 0
    Public Item_Rate As Double = 0
    Public Item_Amt As Double = 0
    Public Qty As Double = 0
    Public Item_Type As String = ""
    Public TAX1 As String = ""
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Rate As Double = 0
    Public TAX1_Amt As Double = 0
    Public Tax2 As String = ""
    Public Tax2_Base_Amt As Double = 0
    Public Tax2_Rate As Double = 0
    Public Tax2_Amt As Double = 0
    Public Tax3 As String = ""
    Public Tax3_Base_Amt As Double = 0
    Public Tax3_Rate As Double = 0
    Public Tax3_Amt As Double = 0
    Public Tax4 As String = ""
    Public Tax4_Base_Amt As Double = 0
    Public Tax4_Rate As Double = 0
    Public Tax4_Amt As Double = 0
    Public Tax5 As String = ""
    Public Tax5_Base_Amt As Double = 0
    Public Tax5_Rate As Double = 0
    Public Tax5_Amt As Double = 0
    Public Tax6 As String = ""
    Public Tax6_Base_Amt As Double = 0
    Public Tax6_Rate As Double = 0
    Public Tax6_Amt As Double = 0
    Public Tax7 As String = ""
    Public Tax7_Base_Amt As Double = 0
    Public Tax7_Rate As Double = 0
    Public Tax7_Amt As Double = 0
    Public Tax8 As String = ""
    Public Tax8_Base_Amt As Double = 0
    Public Tax8_Rate As Double = 0
    Public Tax8_Amt As Double = 0
    Public Tax9 As String = ""
    Public Tax9_Base_Amt As Double = 0
    Public Tax9_Rate As Double = 0
    Public Tax9_Amt As Double = 0
    Public Tax10 As String = ""
    Public Tax10_Base_Amt As Double = 0
    Public Tax10_Rate As Double = 0
    Public Tax10_Amt As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Total_Amt As Double = 0
    Public Inclusive_Tax As Double = 0
    Public Inclusive_TPT As Double = 0
#End Region
    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsCustomerTenderOrderDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsCustomerTenderOrderDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "RowType", obj.RowType)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "Tender_Rate", obj.Tender_Rate)
                    clsCommon.AddColumnsForChange(coll, "Item_Rate", obj.Item_Rate)
                    clsCommon.AddColumnsForChange(coll, "Item_Amt", obj.Item_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1, True)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate, True)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Tax2", obj.Tax2, True)
                    clsCommon.AddColumnsForChange(coll, "Tax2_Base_Amt", obj.Tax2_Base_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Tax2_Rate", obj.Tax2_Rate, True)
                    clsCommon.AddColumnsForChange(coll, "Tax2_Amt", obj.Tax2_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Tax3", obj.Tax3, True)
                    clsCommon.AddColumnsForChange(coll, "Tax3_Base_Amt", obj.Tax3_Base_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Tax3_Rate", obj.Tax3_Rate, True)
                    clsCommon.AddColumnsForChange(coll, "Tax3_Amt", obj.Tax3_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Tax4", obj.Tax4, True)
                    clsCommon.AddColumnsForChange(coll, "Tax4_Base_Amt", obj.Tax4_Base_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Tax4_Rate", obj.Tax4_Rate, True)
                    clsCommon.AddColumnsForChange(coll, "Tax4_Amt", obj.Tax4_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Tax5", obj.Tax5, True)
                    clsCommon.AddColumnsForChange(coll, "Tax5_Base_Amt", obj.Tax5_Base_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Tax5_Rate", obj.Tax5_Rate, True)
                    clsCommon.AddColumnsForChange(coll, "Tax5_Amt", obj.Tax5_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Tax6", obj.Tax6, True)
                    clsCommon.AddColumnsForChange(coll, "Tax6_Base_Amt", obj.Tax6_Base_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Tax6_Rate", obj.Tax6_Rate, True)
                    clsCommon.AddColumnsForChange(coll, "Tax6_Amt", obj.Tax6_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Tax7", obj.Tax7, True)
                    clsCommon.AddColumnsForChange(coll, "Tax7_Base_Amt", obj.Tax7_Base_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Tax7_Rate", obj.Tax7_Rate, True)
                    clsCommon.AddColumnsForChange(coll, "Tax7_Amt", obj.Tax7_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Tax8", obj.Tax8, True)
                    clsCommon.AddColumnsForChange(coll, "Tax8_Base_Amt", obj.Tax8_Base_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Tax8_Rate", obj.Tax8_Rate, True)
                    clsCommon.AddColumnsForChange(coll, "Tax8_Amt", obj.Tax8_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Tax9", obj.Tax9, True)
                    clsCommon.AddColumnsForChange(coll, "Tax9_Base_Amt", obj.Tax9_Base_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Tax9_Rate", obj.Tax9_Rate, True)
                    clsCommon.AddColumnsForChange(coll, "Tax9_Amt", obj.Tax9_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Tax10", obj.Tax10, True)
                    clsCommon.AddColumnsForChange(coll, "Tax10_Base_Amt", obj.Tax10_Base_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Tax10_Rate", obj.Tax10_Rate, True)
                    clsCommon.AddColumnsForChange(coll, "Tax10_Amt", obj.Tax10_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt, True)
                    clsCommon.AddColumnsForChange(coll, "Inclusive_Tax", obj.Inclusive_Tax, True)
                    clsCommon.AddColumnsForChange(coll, "Inclusive_TPT", obj.Inclusive_TPT, True)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_TENDER_ORDER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsCustomerTenderOrderDetail)
        Dim arr As List(Of clsCustomerTenderOrderDetail) = Nothing
        Try
            Dim dt As DataTable
            Dim strQry As String = "select * from TSPL_CUSTOMER_TENDER_ORDER_DETAIL where Document_Code='" & strCode & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                arr = New List(Of clsCustomerTenderOrderDetail)
                Dim objTr As clsCustomerTenderOrderDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsCustomerTenderOrderDetail
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.RowType = clsCommon.myCstr(dr("RowType"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.Tender_Rate = clsCommon.myCdbl(dr("Tender_Rate"))
                    objTr.Item_Rate = clsCommon.myCdbl(dr("Item_Rate"))
                    objTr.Item_Amt = clsCommon.myCdbl(dr("Item_Amt"))
                    objTr.TAX1 = clsCommon.myCdbl(dr("TAX1"))
                    objTr.TAX1_Base_Amt = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                    objTr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                    objTr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                    objTr.Tax2 = clsCommon.myCdbl(dr("Tax2"))
                    objTr.Tax2_Base_Amt = clsCommon.myCdbl(dr("Tax2_Base_Amt"))
                    objTr.Tax2_Rate = clsCommon.myCdbl(dr("Tax2_Rate"))
                    objTr.Tax2_Amt = clsCommon.myCdbl(dr("Tax2_Amt"))
                    objTr.Tax3 = clsCommon.myCdbl(dr("Tax3"))
                    objTr.Tax3_Base_Amt = clsCommon.myCdbl(dr("Tax3_Base_Amt"))
                    objTr.Tax3_Rate = clsCommon.myCdbl(dr("Tax3_Rate"))
                    objTr.Tax3_Amt = clsCommon.myCdbl(dr("Tax3_Amt"))
                    objTr.Tax4 = clsCommon.myCdbl(dr("Tax4"))
                    objTr.Tax4_Base_Amt = clsCommon.myCdbl(dr("Tax4_Base_Amt"))
                    objTr.Tax4_Rate = clsCommon.myCdbl(dr("Tax4_Rate"))
                    objTr.Tax4_Amt = clsCommon.myCdbl(dr("Tax4_Amt"))
                    objTr.Tax5 = clsCommon.myCdbl(dr("Tax5"))
                    objTr.Tax5_Base_Amt = clsCommon.myCdbl(dr("Tax5_Base_Amt"))
                    objTr.Tax5_Rate = clsCommon.myCdbl(dr("Tax5_Rate"))
                    objTr.Tax5_Amt = clsCommon.myCdbl(dr("Tax5_Amt"))
                    objTr.Tax6 = clsCommon.myCdbl(dr("Tax6"))
                    objTr.Tax6_Base_Amt = clsCommon.myCdbl(dr("Tax6_Base_Amt"))
                    objTr.Tax6_Rate = clsCommon.myCdbl(dr("Tax6_Rate"))
                    objTr.Tax6_Amt = clsCommon.myCdbl(dr("Tax6_Amt"))
                    objTr.Tax7 = clsCommon.myCdbl(dr("Tax7"))
                    objTr.Tax7_Base_Amt = clsCommon.myCdbl(dr("Tax7_Base_Amt"))
                    objTr.Tax7_Rate = clsCommon.myCdbl(dr("Tax7_Rate"))
                    objTr.Tax7_Amt = clsCommon.myCdbl(dr("Tax7_Amt"))
                    objTr.Tax8 = clsCommon.myCdbl(dr("Tax8"))
                    objTr.Tax8_Base_Amt = clsCommon.myCdbl(dr("Tax8_Base_Amt"))
                    objTr.Tax8_Rate = clsCommon.myCdbl(dr("Tax8_Rate"))
                    objTr.Tax8_Amt = clsCommon.myCdbl(dr("Tax8_Amt"))
                    objTr.Tax9 = clsCommon.myCdbl(dr("Tax9"))
                    objTr.Tax9_Base_Amt = clsCommon.myCdbl(dr("Tax9_Base_Amt"))
                    objTr.Tax9_Rate = clsCommon.myCdbl(dr("Tax9_Rate"))
                    objTr.Tax9_Amt = clsCommon.myCdbl(dr("Tax9_Amt"))
                    objTr.Tax10 = clsCommon.myCdbl(dr("Tax10"))
                    objTr.Tax10_Base_Amt = clsCommon.myCdbl(dr("Tax10_Base_Amt"))
                    objTr.Tax10_Rate = clsCommon.myCdbl(dr("Tax10_Rate"))
                    objTr.Tax10_Amt = clsCommon.myCdbl(dr("Tax10_Amt"))
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                    objTr.Total_Amt = clsCommon.myCdbl(dr("Total_Amt"))
                    objTr.Inclusive_Tax = clsCommon.myCdbl(dr("Inclusive_Tax"))
                    objTr.Inclusive_TPT = clsCommon.myCdbl(dr("Inclusive_TPT"))
                    arr.Add(objTr)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arr
    End Function
End Class
