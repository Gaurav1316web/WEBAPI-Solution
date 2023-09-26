Imports System.Data.SqlClient

Public Class clsTenderHead
#Region "Variables"

    Public close_yn As String
    Public close_remarks As String

    Public DocumentCode As String = Nothing
    Public DocumentDate As DateTime = Nothing
    Public TendorSeqNo As Integer = 0
    Public DocumentAmount As Double = 0
    Public FieldValue1 As String = Nothing
    Public FieldValue2 As String = Nothing
    Public FieldValue3 As String = Nothing
    Public FieldValue4 As String = Nothing
    Public FieldValue5 As String = Nothing
    Public FieldValue6 As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As DateTime = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As DateTime = Nothing
    Public Posted As Integer = 0
    Public Posting_Date As DateTime?
    Public Posted_By As String = Nothing
    Public OtherInfo1 As String = Nothing
    Public OtherInfo2 As String = Nothing
    Public OtherInfo3 As String = Nothing
    Public OtherInfo4 As String = Nothing
    Public OtherInfo5 As String = Nothing
    Public OtherInfo6 As String = Nothing
    Public OtherInfo7 As String = Nothing
    Public OtherInfo8 As String = Nothing
    Public OtherInfo9 As String = Nothing
    Public OtherInfo10 As String = Nothing

    Public Tender_Type As Integer = 0 ''0-RM Tender;1-Risk Purchase;2-Techical Spare Part;3-Local Purchase
    Public Mode As Integer = 0 ''0-Online;1-Offline
    Public Arr As List(Of clsTenderDetail) = Nothing
    Public ArrSchedule As List(Of clsTenderSchedule) = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending



#End Region
    Public Function SaveData(ByVal obj As clsTenderHead, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsTenderHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim qry As String = "delete from tspl_tender_detail where DocumentCode='" + obj.DocumentCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_TENDER_SCHEDULE_PENALTY where DocumentCode='" + obj.DocumentCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_TENDER_SCHEDULE where DocumentCode='" + obj.DocumentCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


            If (clsCommon.myLen(obj.DocumentCode) <= 0) Then
                Throw New Exception("Error in Document Code Not Found")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DocumentCode", obj.DocumentCode)
            clsCommon.AddColumnsForChange(coll, "DocumentDate", clsCommon.GetPrintDate(obj.DocumentDate, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "DocumentAmount", obj.DocumentAmount)
            clsCommon.AddColumnsForChange(coll, "FieldValue1", obj.FieldValue1, True)
            clsCommon.AddColumnsForChange(coll, "FieldValue2", obj.FieldValue2, True)
            clsCommon.AddColumnsForChange(coll, "FieldValue3", obj.FieldValue3, True)
            clsCommon.AddColumnsForChange(coll, "FieldValue4", obj.FieldValue4, True)
            clsCommon.AddColumnsForChange(coll, "FieldValue5", obj.FieldValue5, True)
            clsCommon.AddColumnsForChange(coll, "FieldValue6", obj.FieldValue6, True)
            clsCommon.AddColumnsForChange(coll, "OtherInfo1", obj.OtherInfo1, True)
            clsCommon.AddColumnsForChange(coll, "OtherInfo2", obj.OtherInfo2, True)
            clsCommon.AddColumnsForChange(coll, "OtherInfo3", obj.OtherInfo3, True)
            clsCommon.AddColumnsForChange(coll, "OtherInfo4", obj.OtherInfo4, True)
            clsCommon.AddColumnsForChange(coll, "OtherInfo5", obj.OtherInfo5, True)
            clsCommon.AddColumnsForChange(coll, "OtherInfo6", obj.OtherInfo6, True)
            clsCommon.AddColumnsForChange(coll, "OtherInfo7", obj.OtherInfo7, True)
            clsCommon.AddColumnsForChange(coll, "OtherInfo8", obj.OtherInfo8, True)
            clsCommon.AddColumnsForChange(coll, "OtherInfo9", obj.OtherInfo9, True)
            clsCommon.AddColumnsForChange(coll, "OtherInfo10", obj.OtherInfo10, True)

            clsCommon.AddColumnsForChange(coll, "Tender_Type", obj.Tender_Type, True)
            clsCommon.AddColumnsForChange(coll, "Mode", obj.Mode, True)

            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TENDER_HEADER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TENDER_HEADER", OMInsertOrUpdate.Update, "tspl_tender_header.DocumentCode='" + obj.DocumentCode + "'", trans)
            End If
            clsTenderDetail.SaveData(obj.DocumentCode, obj.Arr, trans)
            clsTenderSchedule.SaveData(obj.DocumentCode, obj.ArrSchedule, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function closeRaldata(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal cls As String, ByVal strRemarks As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            closeRaldata(trans, strDocNo, isCheckForPosted, cls, strRemarks)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function closeRaldata(ByVal trans As SqlTransaction, ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal cls As String, ByVal strRemarks As String) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("RAL not found to Close")
            End If
            Dim strClosedDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsTenderHead = clsTenderHead.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.DocumentCode) <= 0) Then
                Throw New Exception("No Data found to Close")
            End If
            obj.close_yn = cls
            obj.close_remarks = strRemarks
            Dim qry As String = "Update TSPL_TENDER_HEADER set close_yn='" + obj.close_yn + "',close_remarks='" + obj.close_remarks + "',Closed_By='" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "',Closed_Date='" + strClosedDate + "' where DocumentCode='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function GetData(ByVal strDocumentCode As String, ByVal NavType As NavigatorType) As clsTenderHead
        Try
            Return GetData(strDocumentCode, NavType, Nothing)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strDocumentCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsTenderHead
        Try
            Dim obj As clsTenderHead = Nothing
            Dim qry As String = "SELECT tspl_tender_header.* from tspl_tender_header where 2=2"
            Dim WhrCls As String = ""

            Select Case NavType
                Case NavigatorType.First
                    qry += " and tspl_tender_header.DocumentCode = (select MIN(tspl_tender_header.DocumentCode) from tspl_tender_header Where 1=1 " + WhrCls + ")"
                Case NavigatorType.Last
                    qry += " and tspl_tender_header.DocumentCode = (select Max(tspl_tender_header.DocumentCode) from tspl_tender_header Where 1=1 " + WhrCls + ")"
                Case NavigatorType.Next
                    qry += " and tspl_tender_header.DocumentCode = (select Min(tspl_tender_header.DocumentCode) from tspl_tender_header where DocumentCode>'" + strDocumentCode + "' " + WhrCls + ")"
                Case NavigatorType.Previous
                    qry += " and tspl_tender_header.DocumentCode = (select Max(tspl_tender_header.DocumentCode) from tspl_tender_header where DocumentCode<'" + strDocumentCode + "' " + WhrCls + ")"
                Case NavigatorType.Current
                    qry += " and tspl_tender_header.DocumentCode = '" + strDocumentCode + "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsTenderHead
                obj.DocumentCode = clsCommon.myCstr(dt.Rows(0)("DocumentCode"))
                obj.DocumentDate = clsCommon.myCstr(dt.Rows(0)("DocumentDate"))
                obj.TendorSeqNo = Convert.ToInt32(clsCommon.myCdbl(dt.Rows(0)("TendorSeqNo")))
                obj.DocumentAmount = clsCommon.myCdbl(dt.Rows(0)("DocumentAmount"))
                obj.FieldValue1 = clsCommon.myCstr(dt.Rows(0)("FieldValue1"))
                obj.FieldValue2 = clsCommon.myCstr(dt.Rows(0)("FieldValue2"))
                obj.FieldValue3 = clsCommon.myCstr(dt.Rows(0)("FieldValue3"))
                obj.FieldValue4 = clsCommon.myCstr(dt.Rows(0)("FieldValue4"))
                obj.FieldValue5 = clsCommon.myCstr(dt.Rows(0)("FieldValue5"))
                obj.FieldValue6 = clsCommon.myCstr(dt.Rows(0)("FieldValue6"))
                obj.Posted = Convert.ToInt32(clsCommon.myCdbl(dt.Rows(0)("Posted")))
                obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)

                obj.OtherInfo1 = clsCommon.myCstr(dt.Rows(0)("OtherInfo1"))
                obj.OtherInfo2 = clsCommon.myCstr(dt.Rows(0)("OtherInfo2"))
                obj.OtherInfo3 = clsCommon.myCstr(dt.Rows(0)("OtherInfo3"))
                obj.OtherInfo4 = clsCommon.myCstr(dt.Rows(0)("OtherInfo4"))
                obj.OtherInfo5 = clsCommon.myCstr(dt.Rows(0)("OtherInfo5"))
                obj.OtherInfo6 = clsCommon.myCstr(dt.Rows(0)("OtherInfo6"))
                obj.OtherInfo7 = clsCommon.myCstr(dt.Rows(0)("OtherInfo7"))
                obj.OtherInfo8 = clsCommon.myCstr(dt.Rows(0)("OtherInfo8"))
                obj.OtherInfo9 = clsCommon.myCstr(dt.Rows(0)("OtherInfo9"))
                obj.OtherInfo10 = clsCommon.myCstr(dt.Rows(0)("OtherInfo10"))

                obj.Tender_Type = clsCommon.myCDecimal(dt.Rows(0)("Tender_Type"))
                obj.Mode = clsCommon.myCDecimal(dt.Rows(0)("Mode"))
                obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
                obj.Created_Date = clsCommon.myCstr(dt.Rows(0)("Created_Date"))
                obj.Modified_By = clsCommon.myCstr(dt.Rows(0)("Modified_By"))
                obj.Modified_Date = clsCommon.myCstr(dt.Rows(0)("Modified_Date"))
                obj.close_yn = clsCommon.myCstr(dt.Rows(0)("close_yn"))
                If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                    obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
                    obj.Posted_By = clsCommon.myCstr(dt.Rows(0)("Posted_By"))
                End If

                qry = "SELECT TSPL_TENDER_DETAIL.*,TSPL_LOCATION_MASTER.Location_Desc
                    , TSPL_VENDOR_MASTER.Vendor_Name,TSPL_ITEM_MASTER.Item_Desc
                    FROM TSPL_TENDER_DETAIL left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TENDER_DETAIL.Location 
                    left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.VENDOR_Code=TSPL_TENDER_DETAIL.Vendor_Code
                    left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_TENDER_DETAIL.Item_Code
                    where tspl_tender_detail.DocumentCode='" + obj.DocumentCode + "'ORDER BY tspl_tender_detail.Line_No"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    obj.Arr = New List(Of clsTenderDetail)
                    Dim objTr As clsTenderDetail
                    For Each dr As DataRow In dt.Rows
                        objTr = New clsTenderDetail
                        objTr.DocumentCode = clsCommon.myCstr(dr("DocumentCode"))
                        objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                        objTr.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                        objTr.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))
                        objTr.Location = clsCommon.myCstr(dr("Location"))
                        objTr.Location_Name = clsCommon.myCstr(dr("Location_Desc"))
                        objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                        objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                        objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                        objTr.Discount = clsCommon.myCdbl(dr("Discount"))

                        objTr.Rate = clsCommon.myCdbl(dr("Rate"))
                        objTr.Item_Cost = clsCommon.myCdbl(dr("Item_Cost"))
                        objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                        objTr.Comments = clsCommon.myCstr(dr("Comments"))
                        obj.Arr.Add(objTr)
                    Next
                End If
                obj.ArrSchedule = clsTenderSchedule.GetData(obj.DocumentCode, trans)
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

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
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Tender No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy  hh:mm tt")

            Dim obj As clsTenderHead = clsTenderHead.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.DocumentCode) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Posted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            GeneratePO(obj, trans)

            Dim qry As String = "Update tspl_tender_header set Posted=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where DocumentCode='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Function GeneratePO(objTender As clsTenderHead, trans As SqlTransaction) As Boolean
        If objTender.Tender_Type <> 2 Then
            'If (Qty.Rows.Count > 0) Then
            'If clsCommon.myCdbl(grow.Cells(colQty).Value) > 0 Then
            Dim qry As String = ""
            '        "select TSPL_TAX_GROUP_MASTER.Tax_Group_Code,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,TSPL_TAX_GROUP_DETAILS.Tax_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code_Desc 
            'from TSPL_TAX_GROUP_DETAILS
            'left outer join tspl_tax_Group_master on tspl_tax_Group_master.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type=tspl_tax_Group_master.Tax_Group_Type
            'where tspl_tax_Group_master.Is_Tax_Exempted=1 and tspl_tax_Group_master.Tax_Group_Type='P'"
            '        Dim dtTax As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            '        If dtTax Is Nothing OrElse dtTax.Rows.Count <= 0 Then
            '            Throw New Exception("Please Mark Exempted Tax Group")
            '        End If

            Dim arr As New Dictionary(Of String, clsPurchaseOrderHead)
            For ii As Integer = 0 To objTender.Arr.Count - 1
                qry = "select IsTaxable from TSPL_ITEM_MASTER where Item_Code='" + objTender.Arr(ii).Item_Code + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                Dim isTaxable As Integer = clsCommon.myCDecimal(dt.Rows(0)("IsTaxable"))

                qry = "select State_Code from (
select State as State_Code from TSPL_LOCATION_MASTER where Location_Code='" + objTender.Arr(ii).Location + "'
union all
select State_Code from TSPL_VENDOR_MASTER where Vendor_Code='" + objTender.Arr(ii).Vendor_Code + "'
)x group by State_Code"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                Dim taxGrpType As String = ""
                qry = "select Tax_Group_Code,Tax_Code from TSPL_LOCATION_WISE_TAX_MASTER where Location_Code='" + objTender.Arr(ii).Location + "' and Tax_Type='P' and Is_Default_Tax=1 "
                If dt.Rows.Count > 1 Then
                    taxGrpType += " Category=[Interstate]"
                    qry += " and Tax_Category='I' "
                Else
                    taxGrpType += " Category=[Local]"
                    qry += " and Tax_Category='L' "
                End If
                If isTaxable Then
                    taxGrpType += " [Taxable]"
                    qry += " and Is_Default_Tax_Group_GST=1 "
                Else
                    taxGrpType += " [Non Taxable]"
                    qry += " and Is_Default_Tax_Group=1 "
                End If
                Dim dtTax As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtTax Is Nothing OrElse dtTax.Rows.Count <= 0 Then
                    Throw New Exception("Tax Group Not found " + Environment.NewLine + "Location [" + objTender.Arr(ii).Location + "] " + taxGrpType)
                End If


                Dim strKey As String = objTender.Arr(ii).Vendor_Code + objTender.Arr(ii).Location + clsCommon.myCstr(dtTax.Rows(0)("Tax_Group_Code"))

                Dim obj As clsPurchaseOrderHead = Nothing
                If arr.ContainsKey(strKey) Then
                    obj = arr(strKey)
                    arr.Remove(strKey)
                Else
                    obj = New clsPurchaseOrderHead

                    obj.roadpermit = "0"
                    obj.Cform = "0"
                    obj.IsCancel = 0
                    obj.Is_Open_PO = 0
                    'obj.Against_PO = clsCommon.myCstr(txtAgainstPO_No.Text)
                    'If dtpRenewal.Checked Then
                    '    obj.Renewal_Date = clsCommon.myCstr(dtpRenewal.Text)
                    'Else
                    '    obj.Renewal_Date = ""
                    'End If

                    'obj.ReferencePO = clsCommon.myCstr(txtReferencePO.Text)
                    'obj.Against_Vendor_Quotation = lblVendorQuotationNo.Text
                    'obj.Delivery_Terms_Code = clsCommon.myCstr(txtDelivery_Code.Value)
                    'obj.Payment_Terms = clsCommon.myCstr(txtPaymentTerm.Text)
                    'obj.Insurance_Terms = clsCommon.myCstr(txtInsuranceTerms.Text)
                    'obj.PurchaseOrder_No = txtDocNo.Value
                    obj.PurchaseOrder_Date = objTender.DocumentDate
                    obj.Delivery_date = objTender.DocumentDate
                    qry = "select MIN(From_Date) as From_Date from TSPL_TENDER_SCHEDULE where DocumentCode='" + objTender.DocumentCode + "' and PSNo='" + clsCommon.myCstr(objTender.Arr(ii).Line_No) + "'"
                    Dim dtTem As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtTem.Rows(0)("From_Date") IsNot DBNull.Value Then
                        obj.Delivery_date = clsCommon.myCDate(dtTem.Rows(0)("From_Date"))
                    End If
                    'obj.Delivery_Duration = clsCommon.myCstr(txtDeliveryDuration.Text)
                    obj.Vendor_Code = objTender.Arr(ii).Vendor_Code
                    obj.Vendor_Name = objTender.Arr(ii).Vendor_Name
                    obj.Ref_No = objTender.DocumentCode




                    obj.Remarks = objTender.Arr(ii).Remarks
                    obj.Bill_To_Location = objTender.Arr(ii).Location
                    obj.Ship_To_Location = objTender.Arr(ii).Location
                    'obj.Sublocation_Code = txtSubLocation.Value
                    obj.Comments = objTender.Arr(ii).Comments
                    obj.On_Hold = False
                    obj.Mode_Of_Transport = "By Road"
                    obj.Description = objTender.Arr(ii).Remarks


                    obj.GSTRegistered = False
                    obj.PurchaseOrder_Type = "L"
                    'obj.PROJECT_ID = fndProject.Value
                    obj.MCC_Purchase = 0
                    'obj.State_Code = fndState.Value

                    obj.isBlanket = 0
                    obj.IsPO = 0
                    obj.IsContent = 0
                    obj.isJobWorkOutward = 0
                    obj.Auto_Calculate = 0
                    obj.Subject = ""
                    obj.Content_Subject = ""
                    obj.Kind_Attentation = ""
                    obj.Against_Tender = "Y"
                    obj.ServiceBill_Date = ""
                    obj.ServiceBill_No = ""
                    obj.RefTendorNo = objTender.DocumentCode
                    obj.Form_ID = clsUserMgtCode.mbtnPurchaseOrder


                    obj.Item_Type = clsItemMaster.GetItemTypeFromMaster(objTender.Arr(ii).Item_Code, trans)
                    'obj.Dept = txtDept.Value
                    'obj.Dept_Desc = lblDept.Text

                    obj.Tax_Group = clsCommon.myCstr(dtTax.Rows(0)("Tax_Group_Code"))
                    If (dtTax.Rows.Count > 0) Then
                        obj.TAX1 = clsCommon.myCstr(dtTax.Rows(0)("Tax_Code"))

                        obj.AssessableAmt = 0
                    End If



                    If (dtTax.Rows.Count > 1) Then
                        obj.TAX2 = clsCommon.myCstr(dtTax.Rows(1)("Tax_Code"))
                        'obj.TAX2_Rate = clsCommon.myCdbl(dtTax.Rows(1).Cells(colTTaxRate).Value)
                        'obj.TAX2_Base_Amt = clsCommon.myCdbl(dtTax.Rows(1).Cells(colTBaseAmt).Value)
                        'obj.TAX2_Amt = clsCommon.myCdbl(dtTax.Rows(1).Cells(colTTaxAmt).Value)
                    End If
                    If (dtTax.Rows.Count > 2) Then
                        obj.TAX3 = clsCommon.myCstr(dtTax.Rows(2)("Tax_Code"))
                        'obj.TAX3_Rate = clsCommon.myCdbl(dtTax.Rows(2).Cells(colTTaxRate).Value)
                        'obj.TAX3_Base_Amt = clsCommon.myCdbl(dtTax.Rows(2).Cells(colTBaseAmt).Value)
                        'obj.TAX3_Amt = clsCommon.myCdbl(dtTax.Rows(2).Cells(colTTaxAmt).Value)
                    End If
                    If (dtTax.Rows.Count > 3) Then
                        obj.TAX4 = clsCommon.myCstr(dtTax.Rows(3)("Tax_Code"))
                        'obj.TAX4_Rate = clsCommon.myCdbl(dtTax.Rows(3).Cells(colTTaxRate).Value)
                        'obj.TAX4_Base_Amt = clsCommon.myCdbl(dtTax.Rows(3).Cells(colTBaseAmt).Value)
                        'obj.TAX4_Amt = clsCommon.myCdbl(dtTax.Rows(3).Cells(colTTaxAmt).Value)
                    End If
                    If (dtTax.Rows.Count > 4) Then
                        obj.TAX5 = clsCommon.myCstr(dtTax.Rows(4)("Tax_Code"))
                        'obj.TAX5_Rate = clsCommon.myCdbl(dtTax.Rows(4).Cells(colTTaxRate).Value)
                        'obj.TAX5_Base_Amt = clsCommon.myCdbl(dtTax.Rows(4).Cells(colTBaseAmt).Value)
                        'obj.TAX5_Amt = clsCommon.myCdbl(dtTax.Rows(4).Cells(colTTaxAmt).Value)
                    End If
                    If (dtTax.Rows.Count > 5) Then
                        obj.TAX6 = clsCommon.myCstr(dtTax.Rows(5)("Tax_Code"))
                        'obj.TAX6_Rate = clsCommon.myCdbl(dtTax.Rows(5).Cells(colTTaxRate).Value)
                        'obj.TAX6_Base_Amt = clsCommon.myCdbl(dtTax.Rows(5).Cells(colTBaseAmt).Value)
                        'obj.TAX6_Amt = clsCommon.myCdbl(dtTax.Rows(5).Cells(colTTaxAmt).Value)
                    End If
                    If (dtTax.Rows.Count > 6) Then
                        obj.TAX7 = clsCommon.myCstr(dtTax.Rows(6)("Tax_Code"))
                        'obj.TAX7_Rate = clsCommon.myCdbl(dtTax.Rows(6).Cells(colTTaxRate).Value)
                        'obj.TAX7_Base_Amt = clsCommon.myCdbl(dtTax.Rows(6).Cells(colTBaseAmt).Value)
                        'obj.TAX7_Amt = clsCommon.myCdbl(dtTax.Rows(6).Cells(colTTaxAmt).Value)
                    End If
                    If (dtTax.Rows.Count > 7) Then
                        obj.TAX8 = clsCommon.myCstr(dtTax.Rows(7)("Tax_Code"))
                        'obj.TAX8_Rate = clsCommon.myCdbl(dtTax.Rows(7).Cells(colTTaxRate).Value)
                        'obj.TAX8_Base_Amt = clsCommon.myCdbl(dtTax.Rows(7).Cells(colTBaseAmt).Value)
                        'obj.TAX8_Amt = clsCommon.myCdbl(dtTax.Rows(7).Cells(colTTaxAmt).Value)
                    End If
                    If (dtTax.Rows.Count > 8) Then
                        obj.TAX9 = clsCommon.myCstr(dtTax.Rows(8)("Tax_Code"))
                        'obj.TAX9_Rate = clsCommon.myCdbl(dtTax.Rows(8).Cells(colTTaxRate).Value)
                        'obj.TAX9_Base_Amt = clsCommon.myCdbl(dtTax.Rows(8).Cells(colTBaseAmt).Value)
                        'obj.TAX9_Amt = clsCommon.myCdbl(dtTax.Rows(8).Cells(colTTaxAmt).Value)
                    End If
                    If (dtTax.Rows.Count > 9) Then
                        obj.TAX10 = clsCommon.myCstr(dtTax.Rows(9)("Tax_Code"))
                        'obj.TAX10_Rate = clsCommon.myCdbl(dtTax.Rows(9).Cells(colTTaxRate).Value)
                        'obj.TAX10_Base_Amt = clsCommon.myCdbl(dtTax.Rows(9).Cells(colTBaseAmt).Value)
                        'obj.TAX10_Amt = clsCommon.myCdbl(dtTax.Rows(9).Cells(colTTaxAmt).Value)
                    End If

                    'obj.Terms_Code = txtTermCode.Value
                    'obj.Terms_Remark = txtTermRemark.Text
                    'obj.Due_Date = ""


                    obj.Total_Tax_Amt = 0
                    obj.Amt_After_Tax = 0
                    obj.Discount_Base = 0
                    obj.Header_Discount_Amount = 0
                    obj.Discount_Amt = 0


                    obj.Total_Taxable_Amount = 0
                    obj.PO_Total_Amt = 0
                    obj.PO_Amount = 0

                    'obj.Abandonment_No = clsCommon.myCdbl(lblAbandonmentNo.Text)
                    'If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.WorkOrderEng) = CompairStringResult.Equal Then
                    '    obj.Against_WorkEstimation_Id = txtReqNo.Value
                    'Else
                    '    obj.Against_Requisition = txtReqNo.Value
                    'End If
                    'obj.Against_RGP_NO = txtRGPNo.Value
                    'obj.Capex_Code = fndcapexcode.Value
                    'obj.Capex_SubCode = fndcapexsubcode.Value
                    'obj.Category = clsCommon.myCstr(ddl_category.SelectedValue.ToString())
                    'obj.Emergency = IIf(chk_emergency.Checked, 1, 0)
                    'obj.Deliverydays = CInt(txt_deliverydays.Text)
                    obj.close_yn = "N"


                    obj.Total_Add_Charge = 0
                    obj.Total_Add_Charge_Insurance = 0
                    obj.Total_Item_Insurance_Amt = 0

                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic

                    'obj.Quotation_No = txtQuotationNo.Text
                    'If clsCommon.myLen(txtQuotationNo.Text) > 0 Then
                    '    obj.Quotation_Date = txtQuotationDate.Value
                    'End If
                    obj.is_Excise_On_Qty = False

                    'obj.IsAbatementPO = False

                    obj.Apply_Receive_Control = False
                    'obj.Bank_Code = txtBankCode.Value
                    'obj.Payment_Code = txtPaymentMode.Value

                    '' Work done agaist ticket no.BHA/13/08/18-000419
                    'obj.Insurance = clsCommon.myCstr(txtInsurance.Text)
                    'obj.Packing_Forward = clsCommon.myCstr(txtPackingForward.Text)
                    'obj.Freight = clsCommon.myCstr(txtFreight.Text)


                    'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                    '    If clsCommon.CompairString(cboItemType.SelectedValue, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cboPOType.SelectedValue, "J") = CompairStringResult.Equal Then
                    '        obj.WorkOrder_Vendor = lblWVendorName.Text
                    '        obj.WorkOrder_Vendor_Add = lblWAddress.Text
                    '        obj.WorkOrder_Vendor_Phn = lblWPhone.Text
                    '    End If
                    'End If

                    'obj.objPIRemittance = clsPIRemittance.Convert(objRemittance, dblPreviousTDSAmt)
                    obj.Form_ID = clsUserMgtCode.mbtnPurchaseOrder
                    obj.CURRENCY_CODE = Nothing
                    obj.ConvRate = 1
                    obj.ApplicableFrom = Nothing
                    obj.Auto_PO = "1"
                    obj.Arr = New List(Of clsPurchaseOrderDetail)
                End If
#Region "Set PO Detail"
                Dim objTr As New clsPurchaseOrderDetail()
                objTr.Line_No = obj.Arr.Count + 1
                objTr.Row_Type = clsItemRowType.RowTypeItem

                ''---------------------
                objTr.Item_Code = objTender.Arr(ii).Item_Code
                objTr.Item_Desc = objTender.Arr(ii).Item_Name
                objTr.PurchaseOrder_Qty = objTender.Arr(ii).Qty


                objTr.Balance_Qty = objTender.Arr(ii).Qty
                objTr.Unit_code = objTender.Arr(ii).Unit_code
                'objTr.Requisition_Id = clsCommon.myCstr(grow.Cells(colReqistionNo).Value)
                'objTr.Location = clsCommon.myCstr(grow.Cells(colloc).Value)

                'objTr.Last_Other_Vendor_Rate = clsCommon.myCdbl(grow.Cells(colLastRateOtherVendor).Value)
                'objTr.Last_Same_Vendor_Rate = clsCommon.myCdbl(grow.Cells(colLastRateSameVendor).Value)
                objTr.Amount = objTender.Arr(ii).Item_Cost

                objTr.Header_Discount_Per = 0
                objTr.Header_Discount_Amount = 0
                objTr.Disc_Per = 0
                objTr.Detail_Discount_Amount = 0

                objTr.Disc_Amt = 0


                Dim TotalTaxRate As Decimal = 0
                If (dtTax.Rows.Count > 0) Then
                    Dim objTAXRate As clsItemWiseTaxAuthority = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(objTender.Arr(ii).Item_Code, clsCommon.myCstr(dtTax.Rows(0)("Tax_Group_Code")), clsCommon.myCstr(dtTax.Rows(0)("Tax_Code")), objTender.DocumentDate, "P", trans)
                    If objTAXRate IsNot Nothing Then
                        objTr.Against_Item_Wise_Tax_Rate = objTAXRate.HCODE
                        objTr.TAX1_Rate = objTAXRate.TAX_Rate
                        TotalTaxRate += objTAXRate.TAX_Rate
                    End If
                    objTr.TAX1 = clsCommon.myCstr(dtTax.Rows(0)("Tax_Code"))
                    objTr.TAX1_Base_Amt = objTender.Arr(ii).Item_Cost
                    objTr.TAX1_Amt = 0
                End If

                If (dtTax.Rows.Count > 1) Then
                    Dim objTAXRate As clsItemWiseTaxAuthority = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(objTender.Arr(ii).Item_Code, clsCommon.myCstr(dtTax.Rows(1)("Tax_Group_Code")), clsCommon.myCstr(dtTax.Rows(1)("Tax_Code")), objTender.DocumentDate, "P", trans)
                    If objTAXRate IsNot Nothing Then
                        objTr.TAX2_Rate = objTAXRate.TAX_Rate
                        TotalTaxRate += objTAXRate.TAX_Rate
                    End If
                    objTr.TAX2 = clsCommon.myCstr(dtTax.Rows(1)("Tax_Code"))
                    objTr.TAX2_Base_Amt = objTender.Arr(ii).Item_Cost
                    objTr.TAX2_Amt = 0
                End If


                If (dtTax.Rows.Count > 2) Then
                    Dim objTAXRate As clsItemWiseTaxAuthority = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(objTender.Arr(ii).Item_Code, clsCommon.myCstr(dtTax.Rows(2)("Tax_Group_Code")), clsCommon.myCstr(dtTax.Rows(2)("Tax_Code")), objTender.DocumentDate, "P", trans)
                    If objTAXRate IsNot Nothing Then
                        objTr.TAX3_Rate = objTAXRate.TAX_Rate
                        TotalTaxRate += objTAXRate.TAX_Rate
                    End If
                    objTr.TAX3 = clsCommon.myCstr(dtTax.Rows(2)("Tax_Code"))
                    objTr.TAX3_Base_Amt = objTender.Arr(ii).Item_Cost
                    objTr.TAX3_Amt = 0
                End If


                If (dtTax.Rows.Count > 3) Then
                    Dim objTAXRate As clsItemWiseTaxAuthority = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(objTender.Arr(ii).Item_Code, clsCommon.myCstr(dtTax.Rows(3)("Tax_Group_Code")), clsCommon.myCstr(dtTax.Rows(3)("Tax_Code")), objTender.DocumentDate, "P", trans)
                    objTr.TAX4 = clsCommon.myCstr(dtTax.Rows(3)("Tax_Code"))
                    If objTAXRate IsNot Nothing Then

                    End If
                    objTr.TAX4_Base_Amt = objTender.Arr(ii).Item_Cost
                    objTr.TAX4_Rate = objTAXRate.TAX_Rate
                    objTr.TAX4_Amt = 0
                    TotalTaxRate += objTAXRate.TAX_Rate
                End If


                If (dtTax.Rows.Count > 4) Then
                    Dim objTAXRate As clsItemWiseTaxAuthority = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(objTender.Arr(ii).Item_Code, clsCommon.myCstr(dtTax.Rows(4)("Tax_Group_Code")), clsCommon.myCstr(dtTax.Rows(4)("Tax_Code")), objTender.DocumentDate, "P", trans)
                    objTr.TAX5 = clsCommon.myCstr(dtTax.Rows(4)("Tax_Code"))
                    If objTAXRate IsNot Nothing Then
                        objTr.TAX5_Rate = objTAXRate.TAX_Rate
                        TotalTaxRate += objTAXRate.TAX_Rate
                    End If
                    objTr.TAX5_Base_Amt = objTender.Arr(ii).Item_Cost
                    objTr.TAX5_Amt = 0
                End If


                If (dtTax.Rows.Count > 5) Then
                    Dim objTAXRate As clsItemWiseTaxAuthority = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(objTender.Arr(ii).Item_Code, clsCommon.myCstr(dtTax.Rows(5)("Tax_Group_Code")), clsCommon.myCstr(dtTax.Rows(5)("Tax_Code")), objTender.DocumentDate, "P", trans)
                    If objTAXRate IsNot Nothing Then
                        objTr.TAX6_Rate = objTAXRate.TAX_Rate
                        TotalTaxRate += objTAXRate.TAX_Rate
                    End If
                    objTr.TAX6 = clsCommon.myCstr(dtTax.Rows(5)("Tax_Code"))
                    objTr.TAX6_Base_Amt = objTender.Arr(ii).Item_Cost
                    objTr.TAX6_Amt = 0
                End If


                If (dtTax.Rows.Count > 6) Then
                    Dim objTAXRate As clsItemWiseTaxAuthority = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(objTender.Arr(ii).Item_Code, clsCommon.myCstr(dtTax.Rows(6)("Tax_Group_Code")), clsCommon.myCstr(dtTax.Rows(6)("Tax_Code")), objTender.DocumentDate, "P", trans)
                    If objTAXRate IsNot Nothing Then
                        objTr.TAX7_Rate = objTAXRate.TAX_Rate
                        TotalTaxRate += objTAXRate.TAX_Rate
                    End If
                    objTr.TAX7 = clsCommon.myCstr(dtTax.Rows(6)("Tax_Code"))
                    objTr.TAX7_Base_Amt = objTender.Arr(ii).Item_Cost
                    objTr.TAX7_Amt = 0
                End If


                If (dtTax.Rows.Count > 7) Then
                    Dim objTAXRate As clsItemWiseTaxAuthority = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(objTender.Arr(ii).Item_Code, clsCommon.myCstr(dtTax.Rows(7)("Tax_Group_Code")), clsCommon.myCstr(dtTax.Rows(7)("Tax_Code")), objTender.DocumentDate, "P", trans)
                    If objTAXRate IsNot Nothing Then
                        objTr.TAX8_Rate = objTAXRate.TAX_Rate
                        TotalTaxRate += objTAXRate.TAX_Rate
                    End If
                    objTr.TAX8 = clsCommon.myCstr(dtTax.Rows(7)("Tax_Code"))
                    objTr.TAX8_Base_Amt = objTender.Arr(ii).Item_Cost
                    objTr.TAX8_Amt = 0
                End If


                If (dtTax.Rows.Count > 8) Then
                    Dim objTAXRate As clsItemWiseTaxAuthority = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(objTender.Arr(ii).Item_Code, clsCommon.myCstr(dtTax.Rows(8)("Tax_Group_Code")), clsCommon.myCstr(dtTax.Rows(8)("Tax_Code")), objTender.DocumentDate, "P", trans)
                    If objTAXRate IsNot Nothing Then
                        objTr.TAX9_Rate = objTAXRate.TAX_Rate
                        TotalTaxRate += objTAXRate.TAX_Rate
                    End If
                    objTr.TAX9 = clsCommon.myCstr(dtTax.Rows(8)("Tax_Code"))
                    objTr.TAX9_Base_Amt = objTender.Arr(ii).Item_Cost
                    objTr.TAX9_Amt = 0
                End If

                If (dtTax.Rows.Count > 9) Then
                    Dim objTAXRate As clsItemWiseTaxAuthority = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(objTender.Arr(ii).Item_Code, clsCommon.myCstr(dtTax.Rows(9)("Tax_Group_Code")), clsCommon.myCstr(dtTax.Rows(9)("Tax_Code")), objTender.DocumentDate, "P", trans)
                    If objTAXRate IsNot Nothing Then
                        objTr.TAX10_Rate = objTAXRate.TAX_Rate
                        TotalTaxRate += objTAXRate.TAX_Rate
                    End If
                    objTr.TAX10 = clsCommon.myCstr(dtTax.Rows(9)("Tax_Code"))
                    objTr.TAX10_Base_Amt = objTender.Arr(ii).Item_Cost
                    objTr.TAX10_Amt = 0
                End If















                Dim BaseAmt As Decimal = ((objTender.Arr(ii).Item_Cost * 100) / (100 + TotalTaxRate))
                objTr.Item_Cost = clsCommon.myCDivide(BaseAmt, objTender.Arr(ii).Qty)
                objTr.Amt_Less_Discount = BaseAmt

                objTr.Item_Insurance_Base_Amt = BaseAmt
                objTr.Item_Insurance_Apply_On = clsCalculationlApplyON.RowTypeApplyOnPercent
                objTr.Item_Insurance_Rate = 0
                objTr.Item_Insurance_Amt = 0
                objTr.Item_Amt_After_Insurance = BaseAmt

                objTr.Taxable_Amount = BaseAmt
                objTr.Taxable_Amount_Per = 100

                If (dtTax.Rows.Count > 0) Then
                    objTr.TAX1_Base_Amt = BaseAmt
                    objTr.TAX1_Amt = objTr.TAX1_Base_Amt * objTr.TAX1_Rate / 100


                    obj.TAX1_Base_Amt += BaseAmt
                    obj.TAX1_Amt += objTr.TAX1_Amt
                    obj.TAX1_Rate = clsCommon.myCDivide(obj.TAX1_Amt * 100, obj.TAX1_Base_Amt)
                End If

                If (dtTax.Rows.Count > 1) Then
                    objTr.TAX2_Base_Amt = BaseAmt
                    objTr.TAX2_Amt = objTr.TAX2_Base_Amt * objTr.TAX2_Rate / 100

                    obj.TAX2_Base_Amt += BaseAmt
                    obj.TAX2_Amt += objTr.TAX2_Amt
                    obj.TAX2_Rate = clsCommon.myCDivide(obj.TAX2_Amt * 100, obj.TAX2_Base_Amt)
                End If


                If (dtTax.Rows.Count > 2) Then
                    objTr.TAX3_Base_Amt = BaseAmt
                    objTr.TAX3_Amt = objTr.TAX3_Base_Amt * objTr.TAX3_Rate / 100

                    obj.TAX3_Base_Amt += BaseAmt
                    obj.TAX3_Amt += objTr.TAX3_Amt
                    obj.TAX3_Rate = clsCommon.myCDivide(obj.TAX3_Amt * 100, obj.TAX3_Base_Amt)
                End If


                If (dtTax.Rows.Count > 3) Then
                    objTr.TAX4_Base_Amt = BaseAmt
                    objTr.TAX4_Amt = objTr.TAX4_Base_Amt * objTr.TAX4_Rate / 100

                    obj.TAX4_Base_Amt += BaseAmt
                    obj.TAX4_Amt += objTr.TAX4_Amt
                    obj.TAX4_Rate = clsCommon.myCDivide(obj.TAX4_Amt * 100, obj.TAX4_Base_Amt)
                End If


                If (dtTax.Rows.Count > 4) Then
                    objTr.TAX5_Base_Amt = BaseAmt
                    objTr.TAX5_Amt = objTr.TAX5_Base_Amt * objTr.TAX5_Rate / 100

                    obj.TAX5_Base_Amt += BaseAmt
                    obj.TAX5_Amt += objTr.TAX5_Amt
                    obj.TAX5_Rate = clsCommon.myCDivide(obj.TAX5_Amt * 100, obj.TAX5_Base_Amt)
                End If


                If (dtTax.Rows.Count > 5) Then
                    objTr.TAX6_Base_Amt = BaseAmt
                    objTr.TAX6_Amt = objTr.TAX6_Base_Amt * objTr.TAX6_Rate / 100

                    obj.TAX6_Base_Amt += BaseAmt
                    obj.TAX6_Amt += objTr.TAX6_Amt
                    obj.TAX6_Rate = clsCommon.myCDivide(obj.TAX6_Amt * 100, obj.TAX6_Base_Amt)
                End If


                If (dtTax.Rows.Count > 6) Then
                    objTr.TAX7_Base_Amt = BaseAmt
                    objTr.TAX7_Amt = objTr.TAX7_Base_Amt * objTr.TAX7_Rate / 100

                    obj.TAX7_Base_Amt += BaseAmt
                    obj.TAX7_Amt += objTr.TAX7_Amt
                    obj.TAX7_Rate = clsCommon.myCDivide(obj.TAX7_Amt * 100, obj.TAX7_Base_Amt)
                End If


                If (dtTax.Rows.Count > 7) Then
                    objTr.TAX8_Base_Amt = BaseAmt
                    objTr.TAX8_Amt = objTr.TAX8_Base_Amt * objTr.TAX8_Rate / 100

                    obj.TAX8_Base_Amt += BaseAmt
                    obj.TAX8_Amt += objTr.TAX8_Amt
                    obj.TAX8_Rate = clsCommon.myCDivide(obj.TAX8_Amt * 100, obj.TAX8_Base_Amt)
                End If


                If (dtTax.Rows.Count > 8) Then
                    objTr.TAX9_Base_Amt = BaseAmt
                    objTr.TAX9_Amt = objTr.TAX9_Base_Amt * objTr.TAX9_Rate / 100

                    obj.TAX9_Base_Amt += BaseAmt
                    obj.TAX9_Amt += objTr.TAX9_Amt
                    obj.TAX9_Rate = clsCommon.myCDivide(obj.TAX9_Amt * 100, obj.TAX9_Base_Amt)
                End If

                If (dtTax.Rows.Count > 9) Then
                    objTr.TAX10_Base_Amt = BaseAmt
                    objTr.TAX10_Amt = objTr.TAX10_Base_Amt * objTr.TAX10_Rate / 100

                    obj.TAX10_Base_Amt += BaseAmt
                    obj.TAX10_Amt += objTr.TAX10_Amt
                    obj.TAX10_Rate = clsCommon.myCDivide(obj.TAX10_Amt * 100, obj.TAX10_Base_Amt)
                End If

                objTr.Total_Tax_Amt = objTr.TAX1_Amt + objTr.TAX2_Amt + objTr.TAX3_Amt + objTr.TAX4_Amt + objTr.TAX5_Amt + objTr.TAX6_Amt + objTr.TAX7_Amt + objTr.TAX8_Amt + objTr.TAX9_Amt + objTr.TAX10_Amt
                objTr.Item_Net_Amt = BaseAmt + objTr.Total_Tax_Amt

                objTr.Specification = ""
                objTr.Remarks = ""
                objTr.Location = objTender.Arr(ii).Location




                obj.Discount_Base = BaseAmt
                obj.Header_Discount_Amount = 0
                obj.Discount_Amt = 0
                obj.Amount_Less_Discount += BaseAmt
                obj.Total_Tax_Amt += objTr.Total_Tax_Amt
                obj.Amt_After_Tax += (objTr.Taxable_Amount + objTr.Total_Tax_Amt)



                obj.Total_Taxable_Amount = 0
                obj.PO_Total_Amt += objTr.Item_Net_Amt
                obj.PO_Amount += objTr.Item_Net_Amt
                If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                    obj.Arr.Add(objTr)
                End If
#End Region

                arr.Add(strKey, obj)
            Next


            For Each key As String In arr.Keys
                Dim obj As clsPurchaseOrderHead = arr.Item(key)
                obj.SaveData(obj, True, False, trans)
                obj.PostData(clsUserMgtCode.mbtnPurchaseOrder, obj.PurchaseOrder_No, "", False, False, trans, "", "")
            Next
        End If
        'End If
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
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
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Tender not found to Delete")
        End If
        Dim obj As clsTenderHead = clsTenderHead.GetData(strCode, NavigatorType.Current, trans)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.DocumentCode) > 0) Then
            Try
                If (obj.Posted = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If

                Dim qry As String = "delete from tspl_tender_detail where DocumentCode='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_TENDER_SCHEDULE_PENALTY where DocumentCode='" + obj.DocumentCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_TENDER_SCHEDULE where DocumentCode='" + obj.DocumentCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)


                qry = "delete from tspl_tender_header where DocumentCode='" + strCode + "'"
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
            Dim qry As String = "select 1 from tspl_tender_header where DocumentCode='" + strCode + "' and Posted=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Transaction status should be posted.")
            End If

            qry = "select PurchaseOrder_No from TSPL_PURCHASE_ORDER_HEad where Against_Tender ='Y' and RefTendorNo='" + strCode + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    qry = clsCommon.myCstr(dr("PurchaseOrder_No"))
                    clsPurchaseOrderHead.ReverseAndUnpost(qry, clsUserMgtCode.mbtnPurchaseOrder, trans)
                    clsPurchaseOrderHead.DeleteData(qry, False, trans)
                Next
            End If

            qry = "update tspl_tender_header set POSTED=0,Posting_Date=null where DocumentCode='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select DocumentCode as TenderNo,DocumentDate as TenderDate from TSPL_TENDER_HEADER"
        str = clsCommon.ShowSelectForm("TenderFin", qry, "TenderNo", whrcls, curcode, "TenderNo", isButtonClicked)
        Return str
    End Function

    Public Shared Function GetTenderType(ByVal strcode As String, ByVal trans As SqlTransaction) As Integer
        Try
            Dim qry As String = "select Tender_Type from TSPL_TENDER_HEADER where DocumentCode ='" + strcode + "'"
            Return clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsTenderDetail
#Region "Variables"
    Public DocumentCode As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Unit_code As String = Nothing
    Public Location As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Location_Name As String = Nothing
    Public Qty As Double = 0
    Public Discount As Double = 0

    Public Rate As Double = 0
    Public Item_Cost As Double = 0
    Public Remarks As String = Nothing
    Public Comments As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsTenderDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsTenderDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DocumentCode", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)

                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommon.AddColumnsForChange(coll, "Discount", obj.Discount)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)


                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks, True)
                clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TENDER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetFinder(ByVal strTenderNo As String, ByVal strVendorCode As String) As clsTenderDetail
        Dim obj As clsTenderDetail = Nothing
        Dim qry As String = " select TSPL_TENDER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_TENDER_DETAIL.Unit_code,TSPL_TENDER_DETAIL.Rate from TSPL_TENDER_DETAIL
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.ITEM_CODE=TSPL_TENDER_DETAIL.Item_Code
 where DocumentCode='" + strTenderNo + "' and Vendor_Code='" + strVendorCode + "' "
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("TenVedItm", qry)
        If dr IsNot Nothing Then
            obj = New clsTenderDetail()
            obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
            obj.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
            obj.Unit_code = clsCommon.myCstr(dr("Unit_code"))
            obj.Rate = clsCommon.myCdbl(dr("Rate"))
        End If
        Return obj
    End Function
End Class

Public Class clsTenderSchedule
#Region "Variables"
    Public DocumentCode As String
    Public SNo As Integer
    Public PSNo As Integer
    Public Schedule_No As Integer
    Public From_Date As Date
    Public To_Date As Date
    Public Vendor_Code As String
    Public Location_Code As String
    Public Item_Code As String
    Public Schedule_Qty_Per As Decimal
    Public Schedule_Qty As Decimal
    Public Schedule_Short_Per As Decimal
    Public Schedule_Short As Decimal
    Public Late_Days As Integer
    Public Extension_Days As Integer
    Public Item_Name As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Location_Name As String = Nothing
    Public Arr As List(Of clsTenderSchedulePenelty) = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsTenderSchedule), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsTenderSchedule In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DocumentCode", strDocNo)
                clsCommon.AddColumnsForChange(coll, "PSNo", obj.PSNo)
                clsCommon.AddColumnsForChange(coll, "Schedule_No", obj.Schedule_No)
                clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Schedule_Qty_Per", obj.Schedule_Qty_Per)
                clsCommon.AddColumnsForChange(coll, "Schedule_Qty", obj.Schedule_Qty)
                clsCommon.AddColumnsForChange(coll, "Schedule_Short_Per", obj.Schedule_Short_Per)
                clsCommon.AddColumnsForChange(coll, "Schedule_Short", obj.Schedule_Short)
                clsCommon.AddColumnsForChange(coll, "Late_Days", obj.Late_Days)
                clsCommon.AddColumnsForChange(coll, "Extension_Days", obj.Extension_Days)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TENDER_SCHEDULE", OMInsertOrUpdate.Insert, "", trans)

                Dim PK As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select MAX(PK_ID) from TSPL_TENDER_SCHEDULE where DocumentCode='" + strDocNo + "'", trans))
                clsTenderSchedulePenelty.SaveData(strDocNo, PK, obj.Arr, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsTenderSchedule)
        Dim arr As List(Of clsTenderSchedule) = Nothing
        Dim qry As String = "select TSPL_TENDER_SCHEDULE.* from TSPL_TENDER_SCHEDULE  where TSPL_TENDER_SCHEDULE.DocumentCode='" + clsCommon.myCstr(strDocNo) + "' order by TSPL_TENDER_SCHEDULE.PK_Id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsTenderSchedule)()
            For ii As Integer = 0 To dt.Rows.Count - 1
                Dim obj As New clsTenderSchedule
                obj.SNo = ii + 1
                obj.DocumentCode = clsCommon.myCstr(dt.Rows(ii)("DocumentCode"))
                obj.PSNo = clsCommon.myCDecimal(dt.Rows(ii)("PSNo"))
                obj.Schedule_No = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_No"))
                obj.From_Date = clsCommon.myCDate(dt.Rows(ii)("From_Date"))
                obj.To_Date = clsCommon.myCDate(dt.Rows(ii)("To_Date"))

                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(ii)("Vendor_Code"))
                obj.Location_Code = clsCommon.myCstr(dt.Rows(ii)("Location_Code"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(ii)("Item_Code"))

                obj.Schedule_Qty_Per = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Qty_Per"))
                obj.Schedule_Qty = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Qty"))
                obj.Schedule_Short_Per = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Short_Per"))
                obj.Schedule_Short = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Short"))
                obj.Late_Days = clsCommon.myCDecimal(dt.Rows(ii)("Late_Days"))
                obj.Extension_Days = clsCommon.myCDecimal(dt.Rows(ii)("Extension_Days"))
                obj.Arr = clsTenderSchedulePenelty.GetData(clsCommon.myCDecimal(dt.Rows(ii)("PK_Id")), False, trans)
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsTenderSchedulePenelty
#Region "Variables"
    Public PK_Id As Integer
    Public DocumentCode As String
    Public Against_Tender_Schedule_PK_Id As Integer
    Public Penalty_Date As Date
    Public Penalty As Decimal
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal AgainstSchedulePKId As Integer, ByVal Arr As List(Of clsTenderSchedulePenelty), ByVal trans As SqlTransaction) As Boolean
        For Each obj As clsTenderSchedulePenelty In Arr
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DocumentCode", strDocNo)
            clsCommon.AddColumnsForChange(coll, "Against_Tender_Schedule_PK_Id", AgainstSchedulePKId)
            clsCommon.AddColumnsForChange(coll, "Penalty_Date", clsCommon.GetPrintDate(obj.Penalty_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Penalty", obj.Penalty)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TENDER_SCHEDULE_PENALTY", OMInsertOrUpdate.Insert, "", trans)
        Next
        Return True
    End Function

    Public Shared Function GetData(ByVal AgainstSchedulePKId As Integer, ByVal AddExtensionDays As Boolean, ByVal trans As SqlTransaction) As List(Of clsTenderSchedulePenelty)
        Dim arr As List(Of clsTenderSchedulePenelty) = Nothing
        Dim qry As String = "select TSPL_TENDER_SCHEDULE_PENALTY.DocumentCode,TSPL_TENDER_SCHEDULE_PENALTY.PK_Id,TSPL_TENDER_SCHEDULE_PENALTY.Against_Tender_Schedule_PK_Id "
        If AddExtensionDays = True Then
            qry += " ,DATEADD(day,isnull(TSPL_TENDER_SCHEDULE.Extension_Days,0),TSPL_TENDER_SCHEDULE_PENALTY.Penalty_Date) "
        Else
            qry += " ,TSPL_TENDER_SCHEDULE_PENALTY.Penalty_Date "
        End If
        qry += " AS Penalty_Date ,TSPL_TENDER_SCHEDULE_PENALTY.Penalty
         from TSPL_TENDER_SCHEDULE_PENALTY
         left outer join TSPL_TENDER_SCHEDULE on TSPL_TENDER_SCHEDULE.DocumentCode=TSPL_TENDER_SCHEDULE_PENALTY.DocumentCode
         and TSPL_TENDER_SCHEDULE.PK_ID=TSPL_TENDER_SCHEDULE_PENALTY.Against_Tender_Schedule_PK_Id
         where TSPL_TENDER_SCHEDULE_PENALTY.Against_Tender_Schedule_PK_Id='" + clsCommon.myCstr(AgainstSchedulePKId) + "' order by TSPL_TENDER_SCHEDULE_PENALTY.PK_Id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsTenderSchedulePenelty)()
            For ii As Integer = 0 To dt.Rows.Count - 1
                Dim obj As New clsTenderSchedulePenelty
                obj.PK_Id = clsCommon.myCDecimal(dt.Rows(ii)("PK_Id"))
                obj.DocumentCode = clsCommon.myCstr(dt.Rows(ii)("DocumentCode"))
                obj.Against_Tender_Schedule_PK_Id = clsCommon.myCDecimal(dt.Rows(ii)("Against_Tender_Schedule_PK_Id"))
                obj.Penalty_Date = clsCommon.myCDate(dt.Rows(ii)("Penalty_Date"))
                obj.Penalty = clsCommon.myCDecimal(dt.Rows(ii)("Penalty"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class



