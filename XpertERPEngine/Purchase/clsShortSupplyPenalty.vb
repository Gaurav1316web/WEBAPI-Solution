Imports common
Imports System.Data.SqlClient

Public Class clsShortSupplyPenalty
#Region "Variables"
    Public Document_Code As String
    Public Document_Date As DateTime
    Public Location As String
    Public Location_Desc As String
    Public RAL_No As String
    Public Vendor_No As String
    Public Vendor_Name As String
    Public Item_Code As String
    Public Item_Desc As String
    Public Remarks As String
    Public PI_No As String
    Public RAL_Qty As Decimal
    Public SRN_Qty As Decimal
    Public Short_Excess_Qty As Decimal
    Public Penalty_Applicable_Per As Decimal
    Public Applicable_Short_Qty As Decimal
    Public Tolerance_Slab_Qty As Decimal
    Public Item_Rate As Decimal
    Public Penalty_Rate As Decimal
    Public Penalty_Amount As Decimal
    Public Status As Integer
    Public isPost As Boolean = False
    Public Arr As List(Of clsShortSupplyPenaltyDetail)
    Dim isSaved As Boolean = False
#End Region

    Public Function SaveData(ByVal obj As clsShortSupplyPenalty, ByVal isNewEntry As Boolean) As Boolean
        Try
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If SaveData(obj, isNewEntry, trans) Then
                isSaved = True
            End If
        Catch ex As Exception
            isSaved = False
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Function SaveData(ByVal obj As clsShortSupplyPenalty, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_SHORT_SUPPLY_PENALTY_DETAIL where Document_No='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.ShortSupplyPenalty, "", obj.Location)
            End If
            If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim ServerDate As DateTime = clsCommon.GETSERVERDATE(trans)
            Dim coll As New Hashtable()

            Dim ServerTime As DateTime = Nothing
            If isNewEntry Then
                ServerTime = clsCommon.GETSERVERDATE(trans)
                obj.Document_Date = New DateTime(obj.Document_Date.Year, obj.Document_Date.Month, obj.Document_Date.Day, ServerTime.Hour, ServerTime.Minute, ServerTime.Second)
            End If
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location)
            clsCommon.AddColumnsForChange(coll, "Tendor_No", obj.RAL_No)
            clsCommon.AddColumnsForChange(coll, "Vendor_No", obj.Vendor_No)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "PI_No", obj.PI_No)
            clsCommon.AddColumnsForChange(coll, "RAL_Qty", obj.RAL_Qty)
            clsCommon.AddColumnsForChange(coll, "SRN_Qty", obj.SRN_Qty)
            clsCommon.AddColumnsForChange(coll, "Penalty_Qty", obj.Short_Excess_Qty)
            clsCommon.AddColumnsForChange(coll, "Penalty_Applicable_Per", obj.Penalty_Applicable_Per)
            clsCommon.AddColumnsForChange(coll, "Short_Qty", obj.Applicable_Short_Qty)
            clsCommon.AddColumnsForChange(coll, "Tolerance_Slab_Qty", obj.Tolerance_Slab_Qty, True)
            clsCommon.AddColumnsForChange(coll, "Item_Rate", obj.Item_Rate)
            clsCommon.AddColumnsForChange(coll, "Penalty_Rate", obj.Penalty_Rate)
            clsCommon.AddColumnsForChange(coll, "Penalty_Amount", obj.Penalty_Amount)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(ServerDate, "dd/MMM/yyyy hh:mm:ss tt"))
            If obj.isPost Then
                clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(ServerDate, "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            End If
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(ServerDate, "dd/MMM/yyyy hh:mm:ss tt"))
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHORT_SUPPLY_PENALTY", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHORT_SUPPLY_PENALTY", OMInsertOrUpdate.Update, "TSPL_SHORT_SUPPLY_PENALTY.Document_No='" + obj.Document_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsShortSupplyPenaltyDetail.SaveData(obj.Document_Code, obj.Arr, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Document_Code), "TSPL_SHORT_SUPPLY_PENALTY", "Document_No", "TSPL_SHORT_SUPPLY_PENALTY_DETAIL", "Document_No", trans)

            If isPost Then
                qry = Nothing
                qry = "UPDATE TSPL_PURCHASE_ORDER_Detail SET TSPL_PURCHASE_ORDER_Detail.Item_Close_YN = 'Y' FROM TSPL_PURCHASE_ORDER_DETAIL  
                        LEFT OUTER JOIN TSPL_PURCHASE_ORDER_HEAD ON TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No = TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No
                        WHERE TSPL_PURCHASE_ORDER_HEAD.Vendor_Code = '" + obj.Vendor_No + "' AND TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location = '" + obj.Location + "' AND TSPL_PURCHASE_ORDER_HEAD.RefTendorNo = '" + obj.RAL_No + "' 
                        AND TSPL_PURCHASE_ORDER_DETAIL.Item_Code = '" + obj.Item_Code + "' "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                '===================Debit Note=========================
                Dim dblAmount As Decimal = obj.Penalty_Amount
                If True AndAlso dblAmount > 0 Then
                    Dim dt As DataTable = Nothing
                    Dim objVendorInvHead As clsVedorInvoiceHead
                    objVendorInvHead = New clsVedorInvoiceHead()
                    objVendorInvHead.isDeduction = 1
                    objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(ServerDate, "dd/MMM/yyyy")
                    objVendorInvHead.Vendor_Code = obj.Vendor_No
                    objVendorInvHead.Vendor_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Vendor_Name from TSPL_VENDOR_MASTER Where Vendor_Code='" + obj.Vendor_No + "'", trans))
                    objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                    objVendorInvHead.Invoice_Type = "AP"
                    objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                    objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.Location, trans)
                    objVendorInvHead.Description = "AP Debit Note Against PI"
                    objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                    If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                        Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                    End If
                    objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                    objVendorInvHead.RefDocType = "SS-PNT"
                    objVendorInvHead.RefDocNo = obj.Document_Code
                    objVendorInvHead.On_Hold = False
                    objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                    dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                        objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.Location, trans)
                        If clsCommon.myCDecimal(objVendorInvHead.Discount_Amount) > 0 Then
                            objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                            objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.Location, trans)
                        End If
                    End If
                    If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                        Throw New Exception("Please set the vendor payable Account")
                    End If
                    objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                    objVendorInvHead.Total_Landed_Amt = 0
                    objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()
                    If True Then
                        Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                        objVendorInvDetail.Detail_Line_No = 1
                        'objVendorInvDetail.DCS_Addition_Deduction = clsCommon.myCstr(drAmt("Against_DCS_ADDITION_DEDUCTION"))
                        objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Deduction_ACCOUNT"))
                        If clsCommon.myLen(objVendorInvDetail.GL_Account_Code) <= 0 Then
                            Throw New Exception("Please Set Deduction Account of Vendor Account set [" + clsCommon.myCstr(dt.Rows(0)("Acct_Set_Code")) + "] and Vendor [" + obj.Vendor_No + "]")
                        End If
                        objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, obj.Location, trans)
                        objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)
                        objVendorInvDetail.Amount = dblAmount
                        objVendorInvDetail.Discount_Per = 0
                        objVendorInvDetail.Discount = 0
                        objVendorInvDetail.Amount_less_Discount = dblAmount
                        objVendorInvDetail.Total_Tax = 0
                        objVendorInvDetail.Total_Amount = dblAmount
                        objVendorInvDetail.Landed_Amount = dblAmount
                        ''End of Set AP Invvoice Detail Table
                        If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                            objVendorInvHead.Arr.Add(objVendorInvDetail)
                        End If

                        ''Set AP Invvoice Header Table
                        objVendorInvHead.Total_Landed_Amt += dblAmount
                        objVendorInvHead.Discount_Base += dblAmount
                        objVendorInvHead.Discount_Amount += 0
                        objVendorInvHead.Amount_Less_Discount += dblAmount
                        objVendorInvHead.Document_Total += dblAmount
                        objVendorInvHead.Balance_Amt += dblAmount
                        ''End of Set AP Invvoice Header Table

                        objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                        If objVendorInvHead.Empty_Amount > 0 Then
                            If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                                Throw New Exception("Please set Inventory Control Empties")
                            End If
                            objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                        End If
                    End If
                    If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                        Throw New Exception("No GL Account Found For AP Invoice")
                    End If
                    objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                    objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                    clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
                End If

                '============================================
            End If

            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            isSaved = False
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsShortSupplyPenalty
        Dim obj As New clsShortSupplyPenalty
        Dim qry As String = "SELECT TSPL_SHORT_SUPPLY_PENALTY.*,TSPL_LOCATION_MASTER.Location_Desc,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_ITEM_MASTER.Item_Desc
FROM TSPL_SHORT_SUPPLY_PENALTY 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SHORT_SUPPLY_PENALTY.Location_Code 
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_SHORT_SUPPLY_PENALTY.Vendor_No
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHORT_SUPPLY_PENALTY.Item_Code
where 2=2"
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " And Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " And TSPL_SHORT_SUPPLY_PENALTY.Document_No = (select MIN(Document_No) from TSPL_SHORT_SUPPLY_PENALTY WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " And TSPL_SHORT_SUPPLY_PENALTY.Document_No = (select Max(Document_No) from TSPL_SHORT_SUPPLY_PENALTY WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " And TSPL_SHORT_SUPPLY_PENALTY.Document_No = '" + strDoc + "'"
            Case NavigatorType.Next
                qry += " and TSPL_SHORT_SUPPLY_PENALTY.Document_No = (select Min(Document_No) from TSPL_SHORT_SUPPLY_PENALTY where Document_No>'" + strDoc + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_SHORT_SUPPLY_PENALTY.Document_No = (select Max(Document_No) from TSPL_SHORT_SUPPLY_PENALTY where Document_No<'" + strDoc + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Location = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.RAL_No = clsCommon.myCstr(dt.Rows(0)("Tendor_No"))
            obj.Vendor_No = clsCommon.myCstr(dt.Rows(0)("Vendor_No"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.PI_No = clsCommon.myCstr(dt.Rows(0)("PI_No"))
            obj.RAL_Qty = clsCommon.myCDecimal(dt.Rows(0)("RAL_Qty"))
            obj.SRN_Qty = clsCommon.myCDecimal(dt.Rows(0)("SRN_Qty"))
            obj.Short_Excess_Qty = clsCommon.myCDecimal(dt.Rows(0)("Penalty_Qty"))
            obj.Penalty_Applicable_Per = clsCommon.myCDecimal(dt.Rows(0)("Penalty_Applicable_Per"))
            obj.Applicable_Short_Qty = clsCommon.myCDecimal(dt.Rows(0)("Short_Qty"))
            obj.Tolerance_Slab_Qty = clsCommon.myCDecimal(dt.Rows(0)("Tolerance_Slab_Qty"))
            obj.Item_Rate = clsCommon.myCDecimal(dt.Rows(0)("Item_Rate"))
            obj.Penalty_Rate = clsCommon.myCDecimal(dt.Rows(0)("Penalty_Rate"))
            obj.Penalty_Amount = clsCommon.myCDecimal(dt.Rows(0)("Penalty_Amount"))
            obj.Status = clsCommon.myCDecimal(dt.Rows(0)("Status"))

            qry = "Select TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,TSPL_SHORT_SUPPLY_PENALTY_DETAIL.*,TSPL_PI_DETAIL.PI_No,Case When Isnull(TSPL_PI_HEAD.Status,0)=1 Then 'Approved' Else 'Unapproved' End As [PI Status] from TSPL_SHORT_SUPPLY_PENALTY_DETAIL left Outer Join TSPL_PI_DETAIL On TSPL_PI_DETAIL.SRN_Id=TSPL_SHORT_SUPPLY_PENALTY_DETAIL.SRN_No Left Outer Join TSPL_PI_HEAD On TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No Left Outer Join TSPL_PURCHASE_ORDER_HEAD On PurchaseOrder_No=TSPL_PI_HEAD.Against_PO Where TSPL_SHORT_SUPPLY_PENALTY_DETAIL.Document_No='" + obj.Document_Code + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Arr = New List(Of clsShortSupplyPenaltyDetail)
                Dim objTr As clsShortSupplyPenaltyDetail
                For Each rows In dt.Rows
                    objTr = New clsShortSupplyPenaltyDetail()
                    objTr.PurchaseOrder = clsCommon.myCstr(rows("PurchaseOrder_No"))
                    objTr.Document_No = clsCommon.myCstr(rows("GRN_No"))
                    objTr.GRN_No = clsCommon.myCstr(rows("GRN_No"))
                    objTr.GRN_Date = clsCommon.myCDate(rows("GRN_Date"))
                    objTr.Weighment_Code = clsCommon.myCstr(rows("Weighment_Code"))
                    objTr.Weighment_Date = clsCommon.myCDate(rows("Weighment_Date"))
                    objTr.Gross_Weight = clsCommon.myCDecimal(rows("Gross_Weight"))
                    objTr.Tare_Weight = clsCommon.myCDecimal(rows("Tare_Weight"))
                    objTr.Extra_Weight = clsCommon.myCDecimal(rows("Extra_Weight"))
                    objTr.Net_Weight = clsCommon.myCDecimal(rows("Net_Weight"))
                    objTr.SRN_No = clsCommon.myCstr(rows("SRN_No"))
                    objTr.SRN_Date = clsCommon.myCstr(rows("SRN_Date"))
                    objTr.SRN_Qty = clsCommon.myCDecimal(rows("SRN_Qty"))
                    objTr.UOM = clsCommon.myCstr(rows("UOM"))
                    objTr.PI_No = clsCommon.myCstr(rows("PI_No"))
                    objTr.PI_Status = clsCommon.myCstr(rows("PI Status"))
                    If clsCommon.myLen(objTr.Document_No) > 0 Then
                        obj.Arr.Add(objTr)
                    End If
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strDocCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strDocCode, "TSPL_SHORT_SUPPLY_PENALTY", "Document_No", "TSPL_SHORT_SUPPLY_PENALTY_DETAIL", "Document_No", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocCode, "TSPL_SHORT_SUPPLY_PENALTY", "Document_No", "TSPL_SHORT_SUPPLY_PENALTY_DETAIL", "Document_No", trans)

            Dim Qry As String = Nothing
            Qry = "Delete from TSPL_SHORT_SUPPLY_PENALTY_DETAIL Where Document_No='" + strDocCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Delete from TSPL_SHORT_SUPPLY_PENALTY Where Document_No='" + strDocCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strDocCode As String, ByVal RAL_No As String, ByVal Item_Code As String, ByVal Vendor_No As String, ByVal Location As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = Nothing
            'clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocCode, "TSPL_SHORT_SUPPLY_PENALTY", "Document_No", "TSPL_SHORT_SUPPLY_PENALTY_DETAIL", "Document_No", trans)

            Dim PONumber As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No FROM TSPL_PURCHASE_ORDER_DETAIL LEFT OUTER JOIN TSPL_PURCHASE_ORDER_HEAD ON TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No = TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No WHERE TSPL_PURCHASE_ORDER_HEAD.Vendor_Code = '" + Vendor_No + "' AND TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location = '" + Location + "' AND TSPL_PURCHASE_ORDER_HEAD.RefTendorNo = '" + RAL_No + "'      AND TSPL_PURCHASE_ORDER_DETAIL.Item_Code = '" + Item_Code + "' ", trans))

            Qry = "UPDATE TSPL_PURCHASE_ORDER_Detail SET TSPL_PURCHASE_ORDER_Detail.Item_Close_YN = 'N' FROM TSPL_PURCHASE_ORDER_DETAIL  
                        LEFT OUTER JOIN TSPL_PURCHASE_ORDER_HEAD ON TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No = TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No
                        WHERE TSPL_PURCHASE_ORDER_HEAD.Vendor_Code = '" + Vendor_No + "' AND TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location = '" + Location + "' AND TSPL_PURCHASE_ORDER_HEAD.RefTendorNo = '" + RAL_No + "' 
                        AND TSPL_PURCHASE_ORDER_DETAIL.Item_Code = '" + Item_Code + "' "
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No In (Select TSPL_VENDOR_INVOICE_HEAD.Document_No from TSPL_VENDOR_INVOICE_HEAD where RefDocNo='" + strDocCode + "'  )"
            Dim VoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry, trans))

            If clsCommon.myLen(VoucherNo) > 0 Then
                Qry = " Delete from TSPL_JOURNAL_DETAILS where Voucher_No='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = " Delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Qry = " Delete from TSPL_VENDOR_INVOICE_DETAIL where Document_No In (Select Document_No from TSPL_VENDOR_INVOICE_HEAD where RefDocNo='" + strDocCode + "')"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = " Delete from TSPL_VENDOR_INVOICE_HEAD where RefDocNo='" + strDocCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_SHORT_SUPPLY_PENALTY Set Status=0 Where Document_No='" + strDocCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocCode, "TSPL_SHORT_SUPPLY_PENALTY", "Document_No", trans)

            If clsCommon.myLen(PONumber) > 0 Then
                Qry = "Select SUM(Item_Code)Item_Code,SUM(ItemStatus)ItemStatus from (
                        select Count(Item_Code)Item_Code,0 As ItemStatus from TSPL_PURCHASE_ORDER_DETAIL where PurchaseOrder_No='" + PONumber + "' group by PurchaseOrder_No 
                        Union All
                        select 0 As Item_Code,COUNT(Item_Close_YN)ItemStatus from TSPL_PURCHASE_ORDER_DETAIL where PurchaseOrder_No='" + PONumber + "' And Item_Close_YN='Y' group by PurchaseOrder_No 
                        )xyz"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If clsCommon.myCDecimal(dt.Rows(0)("Item_Code")) = clsCommon.myCDecimal(dt.Rows(0)("ItemStatus")) Then
                        Qry = "Update TSPL_PURCHASE_ORDER_HEAD Set close_yn='Y' Where PurchaseOrder_No='" + PONumber + "'"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    End If
                End If
            End If

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function

End Class

Public Class clsShortSupplyPenaltyDetail
#Region "Variables"
    Public PurchaseOrder As String
    Public Document_No As String
    Public GRN_No As String
    Public GRN_Date As DateTime
    Public Vehicle_No As String
    Public Weighment_Code As String
    Public Weighment_Date As DateTime
    Public Gross_Weight As Decimal
    Public Tare_Weight As Decimal
    Public Extra_Weight As Decimal
    Public Net_Weight As Decimal
    Public SRN_No As String
    Public SRN_Date As DateTime
    Public SRN_Qty As Decimal
    Public UOM As String
    Public PI_No As String
    Public PI_Status As String
#End Region
    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsShortSupplyPenaltyDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsShortSupplyPenaltyDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", strCode)
                    clsCommon.AddColumnsForChange(coll, "GRN_No", obj.GRN_No)
                    clsCommon.AddColumnsForChange(coll, "GRN_Date", clsCommon.GetPrintDate(obj.GRN_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Weighment_Code", obj.Weighment_Code)
                    clsCommon.AddColumnsForChange(coll, "Weighment_Date", clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)
                    clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
                    clsCommon.AddColumnsForChange(coll, "Extra_Weight", obj.Extra_Weight)
                    clsCommon.AddColumnsForChange(coll, "Net_Weight", obj.Net_Weight)
                    clsCommon.AddColumnsForChange(coll, "SRN_No", obj.SRN_No)
                    clsCommon.AddColumnsForChange(coll, "SRN_Date", clsCommon.GetPrintDate(obj.SRN_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "SRN_Qty", obj.SRN_Qty)
                    clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHORT_SUPPLY_PENALTY_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function
End Class

