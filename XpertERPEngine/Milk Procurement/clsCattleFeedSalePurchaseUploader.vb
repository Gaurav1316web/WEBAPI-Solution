Imports System.Data.SqlClient
Imports common

Public Class clsCattleFeedSalePurchaseUploader
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_date As Date? = Nothing
    Public Location_Code As String = Nothing
    Public Sub_Location_Code As String = Nothing
    Public Remarks As String = Nothing
    Public Status As Integer = 0
    Public Posted_Date As DateTime? = Nothing
    Public Arr As List(Of clsCattleFeedSalePurchaseUploaderDetail) = Nothing
    Public ArrItemDetails As List(Of clsCattleFeedSalePurchaseUploaderItemDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsCattleFeedSalePurchaseUploader, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal AutoSave As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, Nothing, trans, AutoSave)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsCattleFeedSalePurchaseUploader, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal trans As SqlTransaction, ByVal AutoSave As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_ITEM_DETAIL where Document_No='" & obj.Document_No & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL where Document_No='" & obj.Document_No & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Sub_Location_Code", obj.Sub_Location_Code)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_date, clsDocType.CFSalePurchaseUploader, "", "")
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER", OMInsertOrUpdate.Update, "TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.Document_No='" & obj.Document_No & "'", trans)
            End If
            Dim objDetail As New clsCattleFeedSalePurchaseUploaderDetail()
            isSaved = isSaved AndAlso objDetail.SaveData(obj.Document_No, obj.Arr, trans)

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType) As clsCattleFeedSalePurchaseUploader
        Return GetData(strRetCode, NavType, Nothing)
    End Function

    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCattleFeedSalePurchaseUploader
        Dim obj As clsCattleFeedSalePurchaseUploader = Nothing
        Dim qry As String = "select * from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry &= " and TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.Document_No = (select MIN(Document_No) from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER)"
            Case NavigatorType.Last
                qry &= " and TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.Document_No = (select Max(Document_No) from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER)"
            Case NavigatorType.Next
                qry &= " and TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.Document_No = (select Min(Document_No) from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER where Document_No >'" & strCode & "')"
            Case NavigatorType.Previous
                qry &= " and TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.Document_No = (select Max(Document_No) from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER where Document_No <'" & strCode & "')"
            Case NavigatorType.Current
                qry &= " and TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.Document_No = '" & strCode & "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsCattleFeedSalePurchaseUploader()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_date = clsCommon.myCDate(dt.Rows(0)("Document_date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Sub_Location_Code = clsCommon.myCstr(dt.Rows(0)("Sub_Location_Code"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If
            Dim objDetail As New clsCattleFeedSalePurchaseUploaderDetail()
            obj.Arr = objDetail.GetData(obj.Document_No, trans)
            Dim objItemDetail As New clsCattleFeedSalePurchaseUploaderItemDetail()
            obj.ArrItemDetails = objItemDetail.GetData(obj.Document_No, 0, True, trans)
        End If
        Return obj
    End Function

    Public Function getFinder(ByVal strCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim sql As String = "select Document_No as DocumentNo ,convert(varchar(12),Document_date,103) as DocumentDate, Location_Code as [Location Code],Sub_Location_Code as [Sub Location Code],case when Status = 1 then 'posted' else 'Unposted' end as Posted from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER"
        str = clsCommon.ShowSelectForm("CFSPUP", sql, "DocumentNo", "", strCode, "DocumentNo", isButtonClicked)
        Return str
    End Function
    Public Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As clsCattleFeedSalePurchaseUploader = New clsCattleFeedSalePurchaseUploader()
            obj = obj.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Posted")
            End If
            Dim objSRN As clsSRNHead = New clsSRNHead()
            Dim Arr As New Dictionary(Of String, clsSRNHead)
            Dim ArrSRNDetail As New Dictionary(Of String, clsSRNDetail)
            Dim strBillNoDate As String = ""
            Dim strBillNoDateItem As String = ""
            Dim LineNo As Integer = 1
            Dim dtTax As DataTable = New DataTable
            '---- Create SRN Document of Purchase 
            For Each objDetail As clsCattleFeedSalePurchaseUploaderDetail In obj.Arr
                strBillNoDate = (objDetail.Bill_No + " " + objDetail.SRN_Dispatch_Date).ToUpper()
                For Each objItemDetail As clsCattleFeedSalePurchaseUploaderItemDetail In objDetail.ArrItemDetails
                    strBillNoDateItem = (objDetail.Bill_No + " " + objDetail.SRN_Dispatch_Date + " " + objItemDetail.Item_Code).ToUpper()
                    If Not Arr.ContainsKey(strBillNoDate) Then
                        objSRN = New clsSRNHead()
                        objSRN.PurchaseOrder_Type = "L"
                        objSRN.Document_Type = "SRN"
                        objSRN.SRN_Date = objDetail.SRN_Dispatch_Date
                        objSRN.Vendor_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select vendor_Code from TSPL_ZONE_MASTER where Zone_Code =  '" + objDetail.Zone_Code + "'", trans))
                        objSRN.Vendor_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Vendor_Name from TSPL_VENDOR_MASTER where vendor_Code =  '" + objSRN.Vendor_Code + "'", trans))
                        objSRN.Remarks = obj.Remarks
                        objSRN.Item_Type = clsItemMaster.GetItemType(objItemDetail.Item_Code, trans)
                        objSRN.Bill_To_Location = obj.Location_Code
                        objSRN.BillToLocationName = clsLocation.GetName(obj.Location_Code, trans)
                        objSRN.Sublocation_Code = obj.Sub_Location_Code
                        objSRN.SubLocationName = clsLocation.GetName(obj.Sub_Location_Code, trans)
                        objSRN.Tax_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Group_Code from TSPL_TAX_GROUP_MASTER where Is_Tax_Exempted=1 and Tax_Group_Type = 'P' ", trans))
                        dtTax = SetitemWiseTaxSetting(objSRN.Tax_Group, objSRN.Vendor_Code, objSRN.Bill_To_Location, trans)

                        If (dtTax IsNot Nothing AndAlso dtTax.Rows.Count > 0) Then
                            objSRN.TAX1 = clsCommon.myCstr(dtTax.Rows(0)("Tax_Code"))
                            objSRN.TAX1_Rate = clsCommon.myCdbl(dtTax.Rows(0)("TaxRate"))
                        End If

                        If dtTax.Rows.Count = 1 Then
                            objSRN.TAX1 = clsCommon.myCstr(dtTax.Rows(0)("Tax_Code"))
                            objSRN.TAX1_Rate = clsCommon.myCdbl(dtTax.Rows(0)("TaxRate"))
                            objSRN.TAX1_Amt = 0
                        End If
                        objSRN.TAX1_Base_Amt += 0
                        If dtTax.Rows.Count = 2 Then
                            objSRN.TAX2 = clsCommon.myCstr(dtTax.Rows(1)("Tax_Code"))
                            objSRN.TAX2_Rate = clsCommon.myCdbl(dtTax.Rows(1)("TaxRate"))
                            objSRN.TAX2_Amt = 0
                        End If
                        objSRN.TAX2_Base_Amt += 0
                        If dtTax.Rows.Count = 3 Then
                            objSRN.TAX3 = clsCommon.myCstr(dtTax.Rows(2)("Tax_Code"))
                            objSRN.TAX3_Rate = clsCommon.myCdbl(dtTax.Rows(3)("TaxRate"))
                            objSRN.TAX3_Amt = 0
                        End If
                        objSRN.TAX3_Base_Amt += 0
                        If dtTax.Rows.Count = 4 Then
                            objSRN.TAX4 = clsCommon.myCstr(dtTax.Rows(3)("Tax_Code"))
                            objSRN.TAX4_Rate = clsCommon.myCdbl(dtTax.Rows(4)("TaxRate"))
                            objSRN.TAX4_Amt = 0
                        End If
                        objSRN.TAX4_Base_Amt = 0
                        If dtTax.Rows.Count = 5 Then
                            objSRN.TAX5 = clsCommon.myCstr(dtTax.Rows(4)("Tax_Code"))
                            objSRN.TAX5_Rate = clsCommon.myCdbl(dtTax.Rows(5)("TaxRate"))
                            objSRN.TAX5_Amt = 0
                        End If
                        objSRN.TAX5_Base_Amt = 0
                        If dtTax.Rows.Count = 6 Then
                            objSRN.TAX6 = clsCommon.myCstr(dtTax.Rows(5)("Tax_Code"))
                            objSRN.TAX6_Rate = clsCommon.myCdbl(dtTax.Rows(6)("TaxRate"))
                            objSRN.TAX6_Amt = 0
                        End If
                        objSRN.TAX6_Base_Amt = 0
                        If dtTax.Rows.Count = 7 Then
                            objSRN.TAX7 = clsCommon.myCstr(dtTax.Rows(6)("Tax_Code"))
                            objSRN.TAX7_Rate = clsCommon.myCdbl(dtTax.Rows(6)("TaxRate"))
                            objSRN.TAX7_Amt = 0
                        End If
                        objSRN.TAX7_Base_Amt = 0
                        If dtTax.Rows.Count = 8 Then
                            objSRN.TAX8 = clsCommon.myCstr(dtTax.Rows(7)("Tax_Code"))
                            objSRN.TAX8_Rate = clsCommon.myCdbl(dtTax.Rows(7)("TaxRate"))
                            objSRN.TAX8_Amt = 0
                        End If
                        objSRN.TAX8_Base_Amt = 0
                        If dtTax.Rows.Count = 9 Then
                            objSRN.TAX9 = clsCommon.myCstr(dtTax.Rows(8)("Tax_Code"))
                            objSRN.TAX9_Rate = clsCommon.myCdbl(dtTax.Rows(8)("TaxRate"))
                            objSRN.TAX9_Amt = 0
                        End If
                        objSRN.TAX9_Base_Amt = 0
                        If dtTax.Rows.Count = 10 Then
                            objSRN.TAX10 = clsCommon.myCstr(dtTax.Rows(9)("Tax_Code"))
                            objSRN.TAX10_Rate = clsCommon.myCdbl(dtTax.Rows(9)("TaxRate"))
                            objSRN.TAX10_Amt = 0
                        End If
                        objSRN.TAX10_Base_Amt = 0
                        objSRN.Total_Tax_Amt = 0
                        objSRN.Discount_Base = 0
                        objSRN.Discount_Amt = 0
                        objSRN.Amount_Less_Discount = 0
                        objSRN.Total_Taxable_Amount = 0
                        objSRN.SRN_Total_Amt = 0
                        objSRN.TotalUnit_Cost_Tax = 0
                        objSRN.Comp_Code = Nothing
                        objSRN.Posting_Date = Nothing
                        objSRN.Landed_Add_Cost = 0
                        objSRN.Total_Landed_Cost = 0
                        objSRN.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
                        objSRN.Total_Accepted_Amount = 0
                        objSRN.Total_Rejected_Amount = 0
                        objSRN.Total_Shortage_Amount = 0
                        objSRN.Total_Leak_Amount = 0
                        objSRN.Total_Burst_Amount = 0
                        objSRN.Against_CF_Sale_Purchase_No = obj.Document_No
                        objSRN.Arr = New List(Of clsSRNDetail)
                        Arr.Add(strBillNoDate, objSRN)
                    End If
                    Dim objSRNDetail As New clsSRNDetail()

                    If Not ArrSRNDetail.ContainsKey(strBillNoDateItem) Then
                        objSRNDetail.Line_No += LineNo
                        objSRNDetail.Item_Code = objItemDetail.Item_Code
                        objSRNDetail.Item_Desc = clsItemMaster.GetItemName(objItemDetail.Item_Code, trans)
                        objSRNDetail.Balance_Qty = objItemDetail.Purchase_Qty
                        objSRNDetail.SRN_Qty = objItemDetail.Purchase_Qty
                        objSRNDetail.MRN_Qty = objItemDetail.Purchase_Qty
                        objSRNDetail.Amount = objItemDetail.Purchase_Amt
                        objSRNDetail.Accepted_Amount = objItemDetail.Purchase_Amt
                        objSRNDetail.Amt_Less_Discount = objItemDetail.Purchase_Amt

                        If dtTax.Rows.Count = 1 Then
                            objSRNDetail.TAX1 = clsCommon.myCstr(dtTax.Rows(0)("Tax_Code"))
                            objSRNDetail.TAX1_Rate = clsCommon.myCdbl(dtTax.Rows(0)("TaxRate"))
                        End If
                        objSRNDetail.TAX1_Base_Amt = objItemDetail.Purchase_Amt
                        If dtTax.Rows.Count = 2 Then
                            objSRNDetail.TAX2 = clsCommon.myCstr(dtTax.Rows(1)("Tax_Code"))
                            objSRNDetail.TAX2_Rate = clsCommon.myCdbl(dtTax.Rows(1)("TaxRate"))
                        End If
                        objSRNDetail.TAX2_Base_Amt = objItemDetail.Purchase_Amt
                        If dtTax.Rows.Count = 3 Then
                            objSRNDetail.TAX3 = clsCommon.myCstr(dtTax.Rows(2)("Tax_Code"))
                            objSRNDetail.TAX3_Rate = clsCommon.myCdbl(dtTax.Rows(2)("TaxRate"))
                        End If
                        objSRNDetail.TAX3_Base_Amt = objItemDetail.Purchase_Amt
                        If dtTax.Rows.Count = 4 Then
                            objSRNDetail.TAX4 = clsCommon.myCstr(dtTax.Rows(3)("Tax_Code"))
                            objSRNDetail.TAX4_Rate = clsCommon.myCdbl(dtTax.Rows(3)("TaxRate"))
                        End If
                        objSRNDetail.TAX4_Base_Amt = objItemDetail.Purchase_Amt
                        If dtTax.Rows.Count = 5 Then
                            objSRNDetail.TAX5 = clsCommon.myCstr(dtTax.Rows(4)("Tax_Code"))
                            objSRNDetail.TAX5_Rate = clsCommon.myCdbl(dtTax.Rows(4)("TaxRate"))
                        End If
                        objSRNDetail.TAX5_Base_Amt = objItemDetail.Purchase_Amt
                        If dtTax.Rows.Count = 6 Then
                            objSRNDetail.TAX6 = clsCommon.myCstr(dtTax.Rows(5)("Tax_Code"))
                            objSRNDetail.TAX6_Rate = clsCommon.myCdbl(dtTax.Rows(5)("TaxRate"))
                        End If
                        objSRNDetail.TAX6_Base_Amt = objItemDetail.Purchase_Amt
                        If dtTax.Rows.Count = 7 Then
                            objSRNDetail.TAX7 = clsCommon.myCstr(dtTax.Rows(6)("Tax_Code"))
                            objSRNDetail.TAX7_Rate = clsCommon.myCdbl(dtTax.Rows(6)("TaxRate"))
                        End If
                        objSRNDetail.TAX7_Base_Amt = objItemDetail.Purchase_Amt
                        If dtTax.Rows.Count = 8 Then
                            objSRNDetail.TAX8 = clsCommon.myCstr(dtTax.Rows(7)("Tax_Code"))
                            objSRNDetail.TAX8_Rate = clsCommon.myCdbl(dtTax.Rows(7)("TaxRate"))
                        End If
                        objSRNDetail.TAX8_Base_Amt = objItemDetail.Purchase_Amt
                        If dtTax.Rows.Count = 9 Then
                            objSRNDetail.TAX9 = clsCommon.myCstr(dtTax.Rows(8)("Tax_Code"))
                            objSRNDetail.TAX9_Rate = clsCommon.myCdbl(dtTax.Rows(8)("TaxRate"))
                        End If
                        objSRNDetail.TAX9_Base_Amt = objItemDetail.Purchase_Amt
                        If dtTax.Rows.Count = 10 Then
                            objSRNDetail.TAX10 = clsCommon.myCstr(dtTax.Rows(9)("Tax_Code"))
                            objSRNDetail.TAX10_Rate = clsCommon.myCdbl(dtTax.Rows(9)("TaxRate"))
                        End If
                        objSRNDetail.TAX10_Base_Amt = objItemDetail.Purchase_Amt
                        objSRNDetail.Item_Net_Amt = objItemDetail.Purchase_Amt
                        objSRNDetail.Row_Type = "Item"
                        Arr(strBillNoDate).Arr.Add(objSRNDetail)
                        ArrSRNDetail.Add(strBillNoDateItem, objSRNDetail)
                        LineNo += 1
                    Else
                        ArrSRNDetail(strBillNoDateItem).Balance_Qty += objItemDetail.Purchase_Qty
                        ArrSRNDetail(strBillNoDateItem).SRN_Qty += objItemDetail.Purchase_Qty
                        ArrSRNDetail(strBillNoDateItem).MRN_Qty += objItemDetail.Purchase_Qty
                        ArrSRNDetail(strBillNoDateItem).Amount += objItemDetail.Purchase_Amt
                        ArrSRNDetail(strBillNoDateItem).Accepted_Amount += objItemDetail.Purchase_Amt
                        ArrSRNDetail(strBillNoDateItem).TAX1_Base_Amt += objItemDetail.Purchase_Amt
                        ArrSRNDetail(strBillNoDateItem).TAX2_Base_Amt += objItemDetail.Purchase_Amt
                        ArrSRNDetail(strBillNoDateItem).TAX3_Base_Amt += objItemDetail.Purchase_Amt
                        ArrSRNDetail(strBillNoDateItem).TAX4_Base_Amt += objItemDetail.Purchase_Amt
                        ArrSRNDetail(strBillNoDateItem).TAX5_Base_Amt += objItemDetail.Purchase_Amt
                        ArrSRNDetail(strBillNoDateItem).TAX6_Base_Amt += objItemDetail.Purchase_Amt
                        ArrSRNDetail(strBillNoDateItem).TAX7_Base_Amt += objItemDetail.Purchase_Amt
                        ArrSRNDetail(strBillNoDateItem).TAX8_Base_Amt += objItemDetail.Purchase_Amt
                        ArrSRNDetail(strBillNoDateItem).TAX9_Base_Amt += objItemDetail.Purchase_Amt
                        ArrSRNDetail(strBillNoDateItem).TAX10_Base_Amt += objItemDetail.Purchase_Amt
                        ArrSRNDetail(strBillNoDateItem).Item_Net_Amt += objItemDetail.Purchase_Amt
                        ArrSRNDetail(strBillNoDateItem).Amt_Less_Discount += objItemDetail.Purchase_Amt
                        ArrSRNDetail(strBillNoDateItem).Item_Cost = ArrSRNDetail(strBillNoDateItem).Amount / ArrSRNDetail(strBillNoDateItem).SRN_Qty
                    End If
                    Arr(strBillNoDate).TAX1_Amt += objSRNDetail.TAX1_Amt
                    Arr(strBillNoDate).TAX1_Base_Amt += objItemDetail.Purchase_Amt
                    Arr(strBillNoDate).TAX2_Amt += objSRNDetail.TAX2_Amt
                    Arr(strBillNoDate).TAX2_Base_Amt += objItemDetail.Purchase_Amt
                    Arr(strBillNoDate).TAX3_Amt += objSRNDetail.TAX3_Amt
                    Arr(strBillNoDate).TAX3_Base_Amt += objItemDetail.Purchase_Amt
                    Arr(strBillNoDate).TAX4_Amt += objSRNDetail.TAX4_Amt
                    Arr(strBillNoDate).TAX4_Base_Amt += objItemDetail.Purchase_Amt
                    Arr(strBillNoDate).TAX5_Amt += objSRNDetail.TAX5_Amt
                    Arr(strBillNoDate).TAX5_Base_Amt += objItemDetail.Purchase_Amt
                    Arr(strBillNoDate).TAX6_Amt += objSRNDetail.TAX6_Amt
                    Arr(strBillNoDate).TAX6_Base_Amt += objItemDetail.Purchase_Amt
                    Arr(strBillNoDate).TAX7_Amt += objSRNDetail.TAX7_Amt
                    Arr(strBillNoDate).TAX7_Base_Amt += objItemDetail.Purchase_Amt
                    Arr(strBillNoDate).TAX8_Amt += objSRNDetail.TAX8_Amt
                    Arr(strBillNoDate).TAX8_Base_Amt += objItemDetail.Purchase_Amt
                    Arr(strBillNoDate).TAX9_Amt += objSRNDetail.TAX9_Amt
                    Arr(strBillNoDate).TAX9_Base_Amt += objItemDetail.Purchase_Amt
                    Arr(strBillNoDate).TAX10_Amt += objSRNDetail.TAX10_Amt
                    Arr(strBillNoDate).TAX10_Base_Amt += objItemDetail.Purchase_Amt
                    Arr(strBillNoDate).Total_Tax_Amt += objSRNDetail.Total_Tax_Amt
                    Arr(strBillNoDate).Amount_Less_Discount += objItemDetail.Purchase_Amt
                    Arr(strBillNoDate).Discount_Base += objItemDetail.Purchase_Amt
                    Arr(strBillNoDate).SRN_Total_Amt += objItemDetail.Purchase_Amt
                Next
            Next
            For ii As Integer = 0 To Arr.Keys.Count - 1
                objSRN = New clsSRNHead()
                objSRN = Arr.Item(Arr.Keys(ii))
                objSRN.SaveData(objSRN, True, trans)
                clsSRNHead.PostData(clsUserMgtCode.mbtnSRN, objSRN.SRN_No, trans, False, False, "")
            Next

            '----Ends here

            '---- Create DCS SALE Document

            Dim ArrDCSSale As New Dictionary(Of String, clsMCCMaterialSale)
            Dim ArrDCSSaleDetail As New Dictionary(Of String, clsMCCMaterialSaleDetail)
            Dim strItemDedDateDCS As String = ""
            Dim strItemDedDateDCSItemCode As String = ""
            Dim strDeduction As String = ""
            Dim objDCSSale As New clsMCCMaterialSale()
            For Each objDetail As clsCattleFeedSalePurchaseUploaderDetail In obj.Arr
                For Each objItemDetail As clsCattleFeedSalePurchaseUploaderItemDetail In objDetail.ArrItemDetails
                    strDeduction = clsItemMaster.GetItemDeductionType(objItemDetail.Item_Code, trans)
                    If clsCommon.myLen(strDeduction) > 0 Then
                        strItemDedDateDCSItemCode = strDeduction + " " + objDetail.SRN_Dispatch_Date + " " + objDetail.VLC_Code + " " + objItemDetail.Item_Code
                        strItemDedDateDCS = strDeduction + " " + objDetail.SRN_Dispatch_Date + " " + objDetail.VLC_Code
                        If Not ArrDCSSale.ContainsKey(strItemDedDateDCS) Then
                            objDCSSale = New clsMCCMaterialSale()
                            objDCSSale.Document_Date = objDetail.SRN_Dispatch_Date
                            objDCSSale.Total_Comm_Amt = 0
                            objDCSSale.RoundOffAmount = 0
                            Dim isTaxable As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select IsTaxable from tspl_item_master where item_code ='" + objItemDetail.Item_Code + "'", trans) = 1)
                            objDCSSale.Invoice_Type = IIf(isTaxable, "T", "N")
                            objDCSSale.Customer_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code FROM TSPL_VLC_MASTER_HEAD left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code left outer join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code WHERE VLC_Code='" + objDetail.VLC_Code + "' ", trans))
                            objDCSSale.Customer_Name = clsCustomerMaster.GetName(objDCSSale.Customer_Code, trans)
                            objDCSSale.Status = IIf(obj.Status = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                            objDCSSale.Remarks = obj.Remarks
                            objDCSSale.Bill_To_Location = obj.Location_Code
                            objDCSSale.Sub_Location_code = obj.Sub_Location_Code
                            objDCSSale.Is_CashSale = IIf(objDetail.Sale_Type = "Cash", "Y", "N")
                            objDCSSale.Against_CF_Sale_Purchase_No = obj.Document_No
                            objDCSSale.Tax_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Group_Code from TSPL_TAX_GROUP_MASTER where Is_Tax_Exempted=1 and Tax_Group_Type = 'S' ", trans))

                            dtTax = SetitemWiseTaxSetting(objDCSSale.Tax_Group, objDCSSale.Customer_Code, objDCSSale.Bill_To_Location, trans)

                            If dtTax.Rows.Count = 1 Then
                                objDCSSale.TAX1 = clsCommon.myCstr(dtTax.Rows(0)("Tax_Code"))
                                objDCSSale.TAX1_Rate = clsCommon.myCdbl(dtTax.Rows(0)("TaxRate"))
                                objDCSSale.TAX1_Amt = 0
                            End If
                            objDCSSale.TAX1_Base_Amt += 0
                            If dtTax.Rows.Count = 2 Then
                                objDCSSale.TAX2 = clsCommon.myCstr(dtTax.Rows(1)("Tax_Code"))
                                objDCSSale.TAX2_Rate = clsCommon.myCdbl(dtTax.Rows(1)("TaxRate"))
                                objDCSSale.TAX2_Amt = 0
                            End If
                            objDCSSale.TAX2_Base_Amt += 0
                            If dtTax.Rows.Count = 3 Then
                                objDCSSale.TAX3 = clsCommon.myCstr(dtTax.Rows(2)("Tax_Code"))
                                objDCSSale.TAX3_Rate = clsCommon.myCdbl(dtTax.Rows(3)("TaxRate"))
                                objDCSSale.TAX3_Amt = 0
                            End If
                            objDCSSale.TAX3_Base_Amt += 0
                            If dtTax.Rows.Count = 4 Then
                                objDCSSale.TAX4 = clsCommon.myCstr(dtTax.Rows(3)("Tax_Code"))
                                objDCSSale.TAX4_Rate = clsCommon.myCdbl(dtTax.Rows(4)("TaxRate"))
                                objDCSSale.TAX4_Amt = 0
                            End If
                            objDCSSale.TAX4_Base_Amt = 0
                            If dtTax.Rows.Count = 5 Then
                                objDCSSale.TAX5 = clsCommon.myCstr(dtTax.Rows(4)("Tax_Code"))
                                objDCSSale.TAX5_Rate = clsCommon.myCdbl(dtTax.Rows(5)("TaxRate"))
                                objDCSSale.TAX5_Amt = 0
                            End If
                            objDCSSale.TAX5_Base_Amt = 0
                            If dtTax.Rows.Count = 6 Then
                                objDCSSale.TAX6 = clsCommon.myCstr(dtTax.Rows(5)("Tax_Code"))
                                objDCSSale.TAX6_Rate = clsCommon.myCdbl(dtTax.Rows(6)("TaxRate"))
                                objDCSSale.TAX6_Amt = 0
                            End If
                            objDCSSale.TAX6_Base_Amt = 0
                            If dtTax.Rows.Count = 7 Then
                                objDCSSale.TAX7 = clsCommon.myCstr(dtTax.Rows(6)("Tax_Code"))
                                objDCSSale.TAX7_Rate = clsCommon.myCdbl(dtTax.Rows(6)("TaxRate"))
                                objDCSSale.TAX7_Amt = 0
                            End If
                            objDCSSale.TAX7_Base_Amt = 0
                            If dtTax.Rows.Count = 8 Then
                                objDCSSale.TAX8 = clsCommon.myCstr(dtTax.Rows(7)("Tax_Code"))
                                objDCSSale.TAX8_Rate = clsCommon.myCdbl(dtTax.Rows(7)("TaxRate"))
                                objDCSSale.TAX8_Amt = 0
                            End If
                            objDCSSale.TAX8_Base_Amt = 0
                            If dtTax.Rows.Count = 9 Then
                                objDCSSale.TAX9 = clsCommon.myCstr(dtTax.Rows(8)("Tax_Code"))
                                objDCSSale.TAX9_Rate = clsCommon.myCdbl(dtTax.Rows(8)("TaxRate"))
                                objDCSSale.TAX9_Amt = 0
                            End If
                            objDCSSale.TAX9_Base_Amt = 0
                            If dtTax.Rows.Count = 10 Then
                                objDCSSale.TAX10 = clsCommon.myCstr(dtTax.Rows(9)("Tax_Code"))
                                objDCSSale.TAX10_Rate = clsCommon.myCdbl(dtTax.Rows(9)("TaxRate"))
                                objDCSSale.TAX10_Amt = 0
                            End If
                            objDCSSale.TAX10_Base_Amt = 0

                            objDCSSale.Discount_Base = 0
                            objDCSSale.Discount_Amt = 0
                            objDCSSale.Amount_Less_Discount = 0
                            objDCSSale.Total_Amt = 0
                            objDCSSale.Total_Tax_Amt = 0
                            objDCSSale.BillToLocationName = clsLocation.GetName(objDCSSale.BillToLocationName, trans)
                            objDCSSale.ShipToLocationName = clsLocation.GetName(objDCSSale.Bill_To_Location, trans)
                            objDCSSale.Route_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_Code from TSPL_VLC_MASTER_HEAD where VLC_Code='" + objDetail.VLC_Code + "'", trans))
                            objDCSSale.Route_Desc = clsRouteMaster.GetName(objDCSSale.Route_No, trans)
                            'objDCSSale.Price_Code = obj.Price_Code
                            objDCSSale.HeadDisc_Per = 0
                            objDCSSale.HeadDisc_Amt = 0
                            objDCSSale.HeadDisc_PerAmt = 0
                            objDCSSale.RateDiff_Per = 0
                            objDCSSale.RateDiff_Amt = 0
                            objDCSSale.Gross_Amount = 0
                            objDCSSale.TotalSubsidyAmt = 0
                            objDCSSale.TotalSubsidyDisAmt = 0
                            objDCSSale.Transporter_Commission_TotalAmt = 0
                            objDCSSale.Transporter_Commission_TotalAmt = 0
                            objDCSSale.TotCashDiscAmt = 0
                            If obj.Posted_Date IsNot Nothing Then
                                objDCSSale.Posting_Date = obj.Posted_Date
                            End If
                            objDCSSale.Inv_Date = objDetail.SRN_Dispatch_Date
                            'objDCSSale.Deduction_Type = strDeduction
                            objDCSSale.Deduction = strDeduction
                            objDCSSale.Item_Type = clsItemMaster.GetItemType(objItemDetail.Item_Code, trans)
                            objDCSSale.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
                            objDCSSale.Is_Create_Auto_Invoice = True
                            objDCSSale.Is_Taxable = isTaxable
                            objDCSSale.Arr = New List(Of clsMCCMaterialSaleDetail)
                            ArrDCSSale.Add(strItemDedDateDCS, objDCSSale)
                        End If

                        Dim objDCSSaleDetail As New clsMCCMaterialSaleDetail()
                        'objDCSSaleDetail.vendor_code = objDetail.vendor_code
                        'objDCSSaleDetail.vendor_desc = objDetail.vendor_desc
                        If Not ArrDCSSaleDetail.ContainsKey(strItemDedDateDCSItemCode) Then
                            objDCSSaleDetail.Line_No = LineNo
                            objDCSSaleDetail.Row_Type = "Item"
                            objDCSSaleDetail.Status = obj.Status
                            objDCSSaleDetail.Item_Code = objItemDetail.Item_Code
                            objDCSSaleDetail.Item_Desc = clsItemMaster.GetItemName(objItemDetail.Item_Code, trans)
                            objDCSSaleDetail.Qty = objItemDetail.Purchase_Qty
                            objDCSSaleDetail.Free_Qty = objItemDetail.Purchase_Qty
                            objDCSSaleDetail.Balance_Qty = objItemDetail.Purchase_Qty
                            objDCSSaleDetail.Unit_code = objItemDetail.Unit_Code
                            objDCSSaleDetail.Location = obj.Location_Code
                            objDCSSaleDetail.LocationName = clsLocation.GetName(obj.Location_Code, trans)

                            If dtTax.Rows.Count = 1 Then
                                objDCSSaleDetail.TAX1 = clsCommon.myCstr(dtTax.Rows(0)("Tax_Code"))
                                objDCSSaleDetail.TAX1_Rate = clsCommon.myCdbl(dtTax.Rows(0)("TaxRate"))
                                objDCSSaleDetail.TAX1_Amt = 0
                            End If
                            objDCSSaleDetail.TAX1_Base_Amt = objItemDetail.Purchase_Amt
                            If dtTax.Rows.Count = 2 Then
                                objDCSSaleDetail.TAX2 = clsCommon.myCstr(dtTax.Rows(1)("Tax_Code"))
                                objDCSSaleDetail.TAX2_Rate = clsCommon.myCdbl(dtTax.Rows(1)("TaxRate"))
                                objDCSSaleDetail.TAX2_Amt = 0
                            End If
                            objDCSSaleDetail.TAX2_Base_Amt = objItemDetail.Purchase_Amt
                            If dtTax.Rows.Count = 3 Then
                                objDCSSaleDetail.TAX3 = clsCommon.myCstr(dtTax.Rows(2)("Tax_Code"))
                                objDCSSaleDetail.TAX3_Rate = clsCommon.myCdbl(dtTax.Rows(3)("TaxRate"))
                                objDCSSaleDetail.TAX3_Amt = 0
                            End If
                            objDCSSaleDetail.TAX3_Base_Amt += objItemDetail.Purchase_Amt
                            If dtTax.Rows.Count = 4 Then
                                objDCSSaleDetail.TAX4 = clsCommon.myCstr(dtTax.Rows(3)("Tax_Code"))
                                objDCSSaleDetail.TAX4_Rate = clsCommon.myCdbl(dtTax.Rows(4)("TaxRate"))
                                objDCSSaleDetail.TAX4_Amt = 0
                            End If
                            objDCSSaleDetail.TAX4_Base_Amt = objItemDetail.Purchase_Amt
                            If dtTax.Rows.Count = 5 Then
                                objDCSSaleDetail.TAX5 = clsCommon.myCstr(dtTax.Rows(4)("Tax_Code"))
                                objDCSSaleDetail.TAX5_Rate = clsCommon.myCdbl(dtTax.Rows(5)("TaxRate"))
                                objDCSSaleDetail.TAX5_Amt = 0
                            End If
                            objDCSSaleDetail.TAX5_Base_Amt = objItemDetail.Purchase_Amt
                            If dtTax.Rows.Count = 6 Then
                                objDCSSaleDetail.TAX6 = clsCommon.myCstr(dtTax.Rows(5)("Tax_Code"))
                                objDCSSaleDetail.TAX6_Rate = clsCommon.myCdbl(dtTax.Rows(6)("TaxRate"))
                                objDCSSaleDetail.TAX6_Amt = 0
                            End If
                            objDCSSaleDetail.TAX6_Base_Amt = objItemDetail.Purchase_Amt
                            If dtTax.Rows.Count = 7 Then
                                objDCSSaleDetail.TAX7 = clsCommon.myCstr(dtTax.Rows(6)("Tax_Code"))
                                objDCSSaleDetail.TAX7_Rate = clsCommon.myCdbl(dtTax.Rows(6)("TaxRate"))
                                objDCSSaleDetail.TAX7_Amt = 0
                            End If
                            objDCSSaleDetail.TAX7_Base_Amt = objItemDetail.Purchase_Amt
                            If dtTax.Rows.Count = 8 Then
                                objDCSSaleDetail.TAX8 = clsCommon.myCstr(dtTax.Rows(7)("Tax_Code"))
                                objDCSSaleDetail.TAX8_Rate = clsCommon.myCdbl(dtTax.Rows(7)("TaxRate"))
                                objDCSSaleDetail.TAX8_Amt = 0
                            End If
                            objDCSSaleDetail.TAX8_Base_Amt = objItemDetail.Purchase_Amt
                            If dtTax.Rows.Count = 9 Then
                                objDCSSaleDetail.TAX9 = clsCommon.myCstr(dtTax.Rows(8)("Tax_Code"))
                                objDCSSaleDetail.TAX9_Rate = clsCommon.myCdbl(dtTax.Rows(8)("TaxRate"))
                                objDCSSaleDetail.TAX9_Amt = 0
                            End If
                            objDCSSaleDetail.TAX9_Base_Amt = objItemDetail.Purchase_Amt
                            If dtTax.Rows.Count = 10 Then
                                objDCSSaleDetail.TAX10 = clsCommon.myCstr(dtTax.Rows(9)("Tax_Code"))
                                objDCSSaleDetail.TAX10_Rate = clsCommon.myCdbl(dtTax.Rows(9)("TaxRate"))
                                objDCSSaleDetail.TAX10_Amt = 0
                            End If
                            objDCSSaleDetail.TAX10_Base_Amt = objItemDetail.Purchase_Amt

                            objDCSSaleDetail.Amount = objItemDetail.Purchase_Amt
                            objDCSSaleDetail.Amt_Less_Discount = objItemDetail.Purchase_Amt
                            objDCSSaleDetail.Total_Tax_Amt = 0
                            objDCSSaleDetail.Item_Net_Amt = objItemDetail.Purchase_Amt
                            'objDCSSaleDetail.Batch_No = objDetail.Batch_No
                            objDCSSaleDetail.Total_Basic_Amt = objItemDetail.Purchase_Amt
                            objDCSSaleDetail.Total_Disc_Amt = 0
                            objDCSSaleDetail.Item_Cost = clsEkoPro.GetRateMccSale(objDCSSale.Bill_To_Location, objItemDetail.Item_Code, objItemDetail.Unit_Code, objDetail.SRN_Dispatch_Date, trans)

                            '    objDCSSaleDetail.Price_code = objDetail.Price_code
                            '  objDCSSaleDetail.Price_Date = objDetail.Price_Date
                            'objDCSSaleDetail.Item_Weight = objDetail.Item_Weight
                            'objDCSSaleDetail.TotalItem_Weight = objDetail.TotalItem_Weight
                            'objDCSSaleDetail.Conv_Factor = objDetail.Conv_Factor
                            ArrDCSSale(strItemDedDateDCS).Arr.Add(objDCSSaleDetail)
                            ArrDCSSaleDetail.Add(strItemDedDateDCSItemCode, objDCSSaleDetail)
                            LineNo += 1
                            ' objDCSSaleDetail.Commission_Amt = objDetail.Commission_Amt
                            'objDCSSaleDetail.Amt_Less_Commission = objDetail.Amt_Less_Commission
                            'objDCSSaleDetail.Transporter_Commission_Rate = objDetail.Transporter_Commission_Rate
                            'objDCSSaleDetail.Transporter_Commission_Amt = objDetail.Transporter_Commission_Amt
                            'objDCSSaleDetail.arrBatchItem = objDetail.arrBatchItem
                            'objDCSSaleDetail.arrSrItem = objDetail.arrSrItem
                        Else
                            ArrDCSSaleDetail(strItemDedDateDCSItemCode).Balance_Qty += objItemDetail.Purchase_Qty
                            ArrDCSSaleDetail(strItemDedDateDCSItemCode).Qty += objItemDetail.Purchase_Qty
                            ArrDCSSaleDetail(strItemDedDateDCSItemCode).Qty += objItemDetail.Purchase_Qty
                            ArrDCSSaleDetail(strItemDedDateDCSItemCode).Free_Qty += objItemDetail.Purchase_Qty
                            ArrDCSSaleDetail(strItemDedDateDCSItemCode).TAX1_Base_Amt += objItemDetail.Purchase_Amt
                            ArrDCSSaleDetail(strItemDedDateDCSItemCode).TAX2_Base_Amt += objItemDetail.Purchase_Amt
                            ArrDCSSaleDetail(strItemDedDateDCSItemCode).TAX3_Base_Amt += objItemDetail.Purchase_Amt
                            ArrDCSSaleDetail(strItemDedDateDCSItemCode).TAX4_Base_Amt += objItemDetail.Purchase_Amt
                            ArrDCSSaleDetail(strItemDedDateDCSItemCode).TAX5_Base_Amt += objItemDetail.Purchase_Amt
                            ArrDCSSaleDetail(strItemDedDateDCSItemCode).TAX6_Base_Amt += objItemDetail.Purchase_Amt
                            ArrDCSSaleDetail(strItemDedDateDCSItemCode).TAX7_Base_Amt += objItemDetail.Purchase_Amt
                            ArrDCSSaleDetail(strItemDedDateDCSItemCode).TAX8_Base_Amt += objItemDetail.Purchase_Amt
                            ArrDCSSaleDetail(strItemDedDateDCSItemCode).TAX9_Base_Amt += objItemDetail.Purchase_Amt
                            ArrDCSSaleDetail(strItemDedDateDCSItemCode).TAX10_Base_Amt += objItemDetail.Purchase_Amt
                            ArrDCSSaleDetail(strItemDedDateDCSItemCode).Item_Net_Amt += objItemDetail.Purchase_Amt
                            ArrDCSSaleDetail(strItemDedDateDCSItemCode).Amt_Less_Discount += objItemDetail.Purchase_Amt
                            ArrDCSSaleDetail(strItemDedDateDCSItemCode).Amount += objItemDetail.Purchase_Amt
                            'objDCSSaleDetail.Batch_No = objDetail.Batch_No
                            ArrDCSSaleDetail(strItemDedDateDCSItemCode).Total_Basic_Amt += objItemDetail.Purchase_Amt

                            ArrDCSSaleDetail(strItemDedDateDCS).Item_Cost = clsEkoPro.GetRateMccSale(objDCSSale.Bill_To_Location, objItemDetail.Item_Code, objItemDetail.Unit_Code, objDetail.SRN_Dispatch_Date, trans)
                        End If
                        ArrDCSSale(strItemDedDateDCS).TAX1_Amt += objDCSSaleDetail.TAX1_Amt
                        ArrDCSSale(strItemDedDateDCS).TAX1_Base_Amt += objItemDetail.Purchase_Amt
                        ArrDCSSale(strItemDedDateDCS).TAX2_Amt += objDCSSaleDetail.TAX2_Amt
                        ArrDCSSale(strItemDedDateDCS).TAX2_Base_Amt += objItemDetail.Purchase_Amt
                        ArrDCSSale(strItemDedDateDCS).TAX3_Amt += objDCSSaleDetail.TAX3_Amt
                        ArrDCSSale(strItemDedDateDCS).TAX3_Base_Amt += objItemDetail.Purchase_Amt
                        ArrDCSSale(strItemDedDateDCS).TAX4_Amt += objDCSSaleDetail.TAX4_Amt
                        ArrDCSSale(strItemDedDateDCS).TAX4_Base_Amt += objItemDetail.Purchase_Amt
                        ArrDCSSale(strItemDedDateDCS).TAX5_Amt += objDCSSaleDetail.TAX5_Amt
                        ArrDCSSale(strItemDedDateDCS).TAX5_Base_Amt += objItemDetail.Purchase_Amt
                        ArrDCSSale(strItemDedDateDCS).TAX6_Amt += objDCSSaleDetail.TAX6_Amt
                        ArrDCSSale(strItemDedDateDCS).TAX6_Base_Amt += objItemDetail.Purchase_Amt
                        ArrDCSSale(strItemDedDateDCS).TAX7_Amt += objDCSSaleDetail.TAX7_Amt
                        ArrDCSSale(strItemDedDateDCS).TAX7_Base_Amt += objItemDetail.Purchase_Amt
                        ArrDCSSale(strItemDedDateDCS).TAX8_Amt += objDCSSaleDetail.TAX8_Amt
                        ArrDCSSale(strItemDedDateDCS).TAX8_Base_Amt += objItemDetail.Purchase_Amt
                        ArrDCSSale(strItemDedDateDCS).TAX9_Amt += objDCSSaleDetail.TAX9_Amt
                        ArrDCSSale(strItemDedDateDCS).TAX9_Base_Amt += objItemDetail.Purchase_Amt
                        ArrDCSSale(strItemDedDateDCS).TAX10_Amt += objDCSSaleDetail.TAX10_Amt
                        ArrDCSSale(strItemDedDateDCS).TAX10_Base_Amt += objItemDetail.Purchase_Amt
                        ArrDCSSale(strItemDedDateDCS).Total_Amt += objItemDetail.Purchase_Amt
                        ArrDCSSale(strItemDedDateDCS).Total_Tax_Amt += objDCSSaleDetail.Total_Tax_Amt
                        ArrDCSSale(strItemDedDateDCS).Amount_Less_Discount += objItemDetail.Purchase_Amt
                        ArrDCSSale(strItemDedDateDCS).Discount_Base += objItemDetail.Purchase_Amt

                        Dim lstDecml As New List(Of Decimal)
                        lstDecml = ClsScrapSaleHead.Calculate_RoundOffAmt(clsCommon.myCdbl(objItemDetail.Purchase_Amt), Nothing)
                        If lstDecml IsNot Nothing AndAlso lstDecml.Count > 0 Then
                            ArrDCSSale(strItemDedDateDCS).RoundOffAmount += clsCommon.myCdbl(lstDecml(1))
                        End If
                        ArrDCSSale(strItemDedDateDCS).Gross_Amount += objItemDetail.Purchase_Amt
                    Else
                        Throw New Exception("Please map Deduction Type with Item [" + objItemDetail.Item_Code + "]")
                    End If
                Next
            Next

            For ii As Integer = 0 To ArrDCSSale.Keys.Count - 1
                Dim objShipment As clsMCCMaterialSale = ArrDCSSale.Item(ArrDCSSale.Keys(ii))
                objShipment.SaveData(objShipment, True, trans)
                clsMCCMaterialSale.PostData(clsUserMgtCode.frmDCSSaleEntry, objShipment.Document_Code, trans)
            Next

            '---Ends here
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER set Status= 1, Posted_By = '" & objCommonVar.CurrentUserCode & "',Posted_Date = '" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") & "'  where Document_No='" & obj.Document_No & "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Function SetitemWiseTaxSetting(ByVal TaxGroup As String, ByVal VendorNo As String, ByVal Location As String, ByVal trans As SqlTransaction) As DataTable
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(TaxGroup, "P", VendorNo, Location, trans)
        Return dt
    End Function
    Public Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim isResponse As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If ReverseAndUnpost(strCode, trans) Then
                isResponse = True
            Else
                isResponse = False
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isResponse
    End Function

    Public Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isResponse As Boolean = True
        Try

            Dim obj As clsCattleFeedSalePurchaseUploader = New clsCattleFeedSalePurchaseUploader
            obj = obj.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Status) <= 0) Then
                clsCommon.MyMessageBoxShow("No Data found to Reverse And UnPost")
                isResponse = False
            End If

            If Not obj.Status = 1 Then
                clsCommon.MyMessageBoxShow("Transaction status should be posted for reverse and unpost")
                isResponse = False
            End If

            Dim qry As String = "select distinct isnull(TSPL_SRN_HEAD.SRN_No,'') DOCUMENT_CODE from TSPL_SRN_HEAD where Against_CF_Sale_Purchase_No ='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    clsSRNHead.ReverseAndUnpost(clsCommon.myCstr(dr("DOCUMENT_CODE")), trans)
                Next
            End If

            qry = "select distinct isnull(TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE,'') DOCUMENT_CODE from TSPL_SD_SHIPMENT_HEAD where Against_CF_Sale_Purchase_No ='" + strCode + "' "
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    clsMCCMaterialSale.ReverseAndUnpost(clsCommon.myCstr(dr("DOCUMENT_CODE")), False, trans)
                Next
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 0)
            clsCommon.AddColumnsForChange(coll, "Posted_By", Nothing, True)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", Nothing, True)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER", OMInsertOrUpdate.Update, "Document_No='" & obj.Document_No & "'", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isResponse
    End Function

    Public Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsCattleFeedSalePurchaseUploader = New clsCattleFeedSalePurchaseUploader()
        obj = obj.GetData(strCode, NavigatorType.Current, trans)
        Try
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            If clsCommon.CompairString(obj.Status, "1") = CompairStringResult.Equal Then
                Throw New Exception("Already Posted on :" + obj.Posted_Date)
            End If

            Dim qry As String = "select distinct isnull(TSPL_SRN_HEAD.SRN_No,'') DOCUMENT_CODE from TSPL_SRN_HEAD where Against_CF_Sale_Purchase_No ='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    clsSRNHead.DeleteData(clsCommon.myCstr(dr("DOCUMENT_CODE")), trans)
                Next
            End If
            qry = "select distinct isnull(TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE,'') DOCUMENT_CODE from TSPL_SD_SHIPMENT_HEAD where Against_CF_Sale_Purchase_No ='" + strCode + "' "

            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    clsMCCMaterialSale.DeleteData(clsCommon.myCstr(dr("DOCUMENT_CODE")), trans)
                Next
            End If

            qry = "delete from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_ITEM_DETAIL where Document_No='" & obj.Document_No & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsCattleFeedSalePurchaseUploaderDetail

#Region "Variables"
    Public Document_No As String = Nothing
    Public SRN_Dispatch_Date As Date
    Public VLC_Code As String = Nothing
    Public VLC_Code_VLC_Uploader As String = Nothing
    Public VLC_Name As String = Nothing
    Public Zone_Code As String = Nothing
    Public GRN_No As String = Nothing
    Public Truck_No As String = Nothing
    Public Challan_No As String = Nothing
    Public Freight As Decimal
    Public Bill_No As String = Nothing
    Public Sale_Type As String = Nothing
    Public Total_Sale_Amt As Decimal
    Public ArrItemDetails As List(Of clsCattleFeedSalePurchaseUploaderItemDetail) = Nothing
#End Region
    Public Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsCattleFeedSalePurchaseUploaderDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCattleFeedSalePurchaseUploaderDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strCode)
                clsCommon.AddColumnsForChange(coll, "SRN_Dispatch_Date", clsCommon.GetPrintDate(obj.SRN_Dispatch_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "VLC_Code", obj.VLC_Code)
                clsCommon.AddColumnsForChange(coll, "Zone_Code", obj.Zone_Code)
                clsCommon.AddColumnsForChange(coll, "GRN_No", obj.GRN_No)
                clsCommon.AddColumnsForChange(coll, "Truck_No", obj.Truck_No)
                clsCommon.AddColumnsForChange(coll, "Challan_No", obj.Challan_No)
                clsCommon.AddColumnsForChange(coll, "Freight", obj.Freight)
                clsCommon.AddColumnsForChange(coll, "Bill_No", obj.Bill_No)
                clsCommon.AddColumnsForChange(coll, "Sale_Type", obj.Sale_Type)
                clsCommon.AddColumnsForChange(coll, "Total_Sale_Amt", obj.Total_Sale_Amt)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                Dim PK_ID As Integer = clsERPFuncationality.GetScopeIdentityValue(trans)
                Dim objItemDetail As New clsCattleFeedSalePurchaseUploaderItemDetail()
                objItemDetail.SaveData(strCode, PK_ID, obj.ArrItemDetails, trans)
            Next
        End If
        Return True
    End Function

    Public Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsCattleFeedSalePurchaseUploaderDetail)
        Dim arr As List(Of clsCattleFeedSalePurchaseUploaderDetail) = Nothing
        Dim qry As String = "select TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Document_No, TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.SRN_Dispatch_Date,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Zone_Code,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.GRN_No,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Truck_No,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Challan_No,
TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Freight,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Bill_No,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Sale_Type,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Total_Sale_Amt,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.pk_id
         from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL left outer join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VLC_CODE = TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.VLC_CODE where  TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Document_No = '" & strCode & "' order by Document_No,PK_ID "
        Dim PK_ID As Integer = 0
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsCattleFeedSalePurchaseUploaderDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsCattleFeedSalePurchaseUploaderDetail = New clsCattleFeedSalePurchaseUploaderDetail()
                obj.Document_No = clsCommon.myCstr(dr("Document_No"))
                obj.SRN_Dispatch_Date = clsCommon.myCDate(dr("SRN_Dispatch_Date"))
                obj.VLC_Code = clsCommon.myCstr(dr("VLC_Code"))
                obj.VLC_Code_VLC_Uploader = clsCommon.myCstr(dr("VLC_Code_VLC_Uploader"))
                obj.VLC_Name = clsCommon.myCstr(dr("VLC_Name"))
                obj.Zone_Code = clsCommon.myCstr(dr("Zone_Code"))
                obj.GRN_No = clsCommon.myCstr(dr("GRN_No"))
                obj.Truck_No = clsCommon.myCstr(dr("Truck_No"))
                obj.Challan_No = clsCommon.myCstr(dr("Challan_No"))
                obj.Freight = clsCommon.myCDecimal(dr("Freight"))
                obj.Bill_No = clsCommon.myCstr(dr("Bill_No"))
                obj.Sale_Type = clsCommon.myCstr(dr("Sale_Type"))
                obj.Total_Sale_Amt = clsCommon.myCDecimal(dr("Total_Sale_Amt"))
                PK_ID = clsCommon.myCdbl(dr("PK_ID"))
                Dim objItemDetail As New clsCattleFeedSalePurchaseUploaderItemDetail()
                obj.ArrItemDetails = objItemDetail.GetData(strCode, PK_ID, False, trans)
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function

End Class

Public Class clsCattleFeedSalePurchaseUploaderItemDetail
#Region "Variables"
    Public Document_No As String = Nothing
    Public Ref_PK_ID As Integer
    Public Item_Code As String = Nothing
    Public Unit_Code As String = Nothing
    Public Purchase_Qty As Decimal
    Public Purchase_Rate As Decimal
    Public Purchase_Amt As Decimal
    Public Sale_Rate As Decimal
    Public Sale_Amt As Decimal
#End Region
    Public Function SaveData(ByVal strCode As String, ByVal PK_ID As Integer, ByVal Arr As List(Of clsCattleFeedSalePurchaseUploaderItemDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCattleFeedSalePurchaseUploaderItemDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strCode)
                clsCommon.AddColumnsForChange(coll, "Ref_PK_ID", PK_ID)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "Purchase_Qty", obj.Purchase_Qty)
                clsCommon.AddColumnsForChange(coll, "Purchase_Rate", obj.Purchase_Rate)
                clsCommon.AddColumnsForChange(coll, "Purchase_Amt", obj.Purchase_Amt)
                clsCommon.AddColumnsForChange(coll, "Sale_Amt", obj.Sale_Amt)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Function GetData(ByVal strCode As String, ByVal Ref_PK_ID As Integer, ByVal LoadAllData As Boolean, ByVal trans As SqlTransaction) As List(Of clsCattleFeedSalePurchaseUploaderItemDetail)
        Dim arr As List(Of clsCattleFeedSalePurchaseUploaderItemDetail) = Nothing

        Dim qry As String = "select TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_ITEM_DETAIL.Ref_PK_ID,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_ITEM_DETAIL.* from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_ITEM_DETAIL  
        inner join TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL on TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.PK_Id = TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_ITEM_DETAIL.Ref_PK_ID   where  TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_ITEM_DETAIL.Document_No = '" & strCode & "' "

        If Not LoadAllData Then
            qry &= " and Ref_PK_ID = " & clsCommon.myCstr(Ref_PK_ID) & " "
        End If
        qry &= " order by TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Document_No, TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.PK_ID "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsCattleFeedSalePurchaseUploaderItemDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsCattleFeedSalePurchaseUploaderItemDetail = New clsCattleFeedSalePurchaseUploaderItemDetail()
                obj.Document_No = strCode
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                obj.Purchase_Qty = clsCommon.myCDecimal(dr("Purchase_Qty"))
                obj.Purchase_Rate = clsCommon.myCDecimal(dr("Purchase_Rate"))
                obj.Purchase_Amt = clsCommon.myCDecimal(dr("Purchase_Amt"))
                obj.Sale_Amt = clsCommon.myCDecimal(dr("Sale_Amt"))
                obj.Ref_PK_ID = clsCommon.myCdbl(dr("Ref_PK_ID"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class