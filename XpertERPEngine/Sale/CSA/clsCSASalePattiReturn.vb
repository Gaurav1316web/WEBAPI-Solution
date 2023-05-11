''created by Monika 23/06/2016
Imports common
Imports Telerik
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class clsCSASalePattiReturnHead
#Region "variables"
    Public Document_Code As String = Nothing
    Public Documnet_Date As DateTime? = Nothing
    Public Description As String = Nothing
    Public Cust_Code As String = Nothing
    Public Cust_Name As String = Nothing
    Public CSA_Location_Code As String = Nothing
    Public CSA_location_desc As String = Nothing
    Public Location_Code As String = Nothing
    Public location_desc As String = Nothing
    Public Document_Amount As Decimal = Nothing
    Public Status As Integer = 0
    Public Tax_Group As String = Nothing
    Public isNewEntry As Boolean = True
    Public Return_Type As String = ""
    Public Is_Cancelled As Integer = 0

    Public Arr As New List(Of clsCSASalePattiReturnDetail)
#End Region

    Public Shared Function GetLatestDescription(ByVal trans As SqlTransaction) As String
        Dim str As String = ""
        Dim qry As String = "select top 1 description from tspl_sd_sale_return_head where comp_code='" + objCommonVar.CurrentCompanyCode + "' and trans_type='CPR' and isnull(description,'')<>'' order by document_date desc"
        str = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

        Return str
    End Function

    Public Shared Function GetFinder(ByVal strCurrCode As String, ByVal whrCls As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select tspl_sd_sale_return_head.document_code as Code,convert(varchar,tspl_sd_sale_return_head.document_date,103) as [Date],tspl_sd_sale_return_head.[Description],tspl_sd_sale_return_head.customer_code as [Customer Code],tspl_customer_master.customer_name as [Customer],tspl_sd_sale_return_head.bill_to_location as [Location Code],tspl_location_master.location_desc as [Location Name]" & _
            ",case when tspl_sd_sale_return_head.return_type='I' then 'Return Goods' when tspl_sd_sale_return_head.return_type='D' then 'Damage Goods' when tspl_sd_sale_return_head.return_type='S' then 'Shortage Goods' else 'Return Goods' end as [Type],tspl_sd_sale_return_head.total_amt as [Total Amount],case when isnull(tspl_sd_sale_return_head.status,0)=1 then 'Approved' else 'Pending' end as [Status] from tspl_sd_sale_return_head left outer join tspl_location_master on tspl_location_master.location_code=tspl_sd_sale_return_head.bill_to_location " & _
            " left outer join tspl_customer_master on tspl_customer_master.cust_code=tspl_sd_sale_return_head.customer_code "

        If clsCommon.myLen(whrCls) <= 0 Then
            whrCls = " tspl_sd_sale_return_head.Trans_Type='CPR' "
        End If
        str = clsCommon.myCstr(clsCommon.ShowSelectForm("CSASALERET", qry, "Code", whrCls, strCurrCode, "Code", isButtonClicked))

        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As clsCSASalePattiReturnHead) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsCSASalePattiReturnHead, ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "CSA Sale", "CSA Sale Patti Return", obj.Location_Code, obj.Documnet_Date, trans)

            clsBatchInventory.DeleteData("CSA-SALEPATTI-RETURN", obj.Document_Code, trans)

            If obj.isNewEntry Then
                If obj.Is_Cancelled = 1 Then
                    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Documnet_Date, clsDocType.CSASALERETURN, clsDocTransactionType.SaleReturnCancel, obj.Location_Code)
                Else
                    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Documnet_Date, clsDocType.CSASALERETURN, clsDocTransactionType.NA, obj.Location_Code)
                End If


            End If

            coll = New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Trans_Type", "CPR")
            clsCommon.AddColumnsForChange(coll, "Return_Type", obj.Return_Type)
            clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Documnet_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Cust_Code, True)
            clsCommon.AddColumnsForChange(coll, "CSA_Loc_Code", obj.CSA_Location_Code, True)
            clsCommon.AddColumnsForChange(coll, "bill_to_location", obj.Location_Code, True)
            clsCommon.AddColumnsForChange(coll, "total_amt", obj.Document_Amount)
            clsCommon.AddColumnsForChange(coll, "Tax_group", obj.Tax_Group, True)
            clsCommon.AddColumnsForChange(coll, "Comments", "CSA Sale Patti Return")
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Is_Cancelled", obj.Is_Cancelled)

            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALE_RETURN_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALE_RETURN_HEAD", OMInsertOrUpdate.Update, " trans_type='CPR' and document_code='" + obj.Document_Code + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' ", trans)
            End If

            clsCSASalePattiReturnDetail.SaveData(obj.Document_Code, obj.Location_Code, obj.Documnet_Date, obj.CSA_Location_Code, obj.Arr, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            coll = Nothing
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCSASalePattiReturnHead
        Dim obj As New clsCSASalePattiReturnHead()
        Dim dt As New DataTable()
        Dim loc_code As String = ""
        Try
            obj.Arr = New List(Of clsCSASalePattiReturnDetail)

            Dim qry As String = "select tspl_sd_sale_return_head.*,tspl_customer_master.customer_name,tspl_location_master.location_desc,LocMaster.location_desc as CSALOc, tspl_sd_sale_return_head.Is_Cancelled from tspl_sd_sale_return_head left outer join tspl_location_master on tspl_location_master.location_code=tspl_sd_sale_return_head.bill_to_location left outer join tspl_customer_master on tspl_customer_master.cust_code=tspl_sd_sale_return_head.customer_code " & _
                " left outer join tspl_location_master as LocMaster on LocMaster.location_code=tspl_sd_sale_return_head.CSA_Loc_Code where tspl_sd_sale_return_head.comp_code='" + objCommonVar.CurrentCompanyCode + "' and tspl_sd_sale_return_head.trans_type='CPR' "

            If arrLoc IsNot Nothing AndAlso clsCommon.myLen(arrLoc) > 0 Then
                qry += " and tspl_sd_sale_return_head.bill_to_location in (" + arrLoc + ") "
                loc_code = " and tspl_sd_sale_return_head.comp_code='" + objCommonVar.CurrentCompanyCode + "' and tspl_sd_sale_return_head.bill_to_location in (" + arrLoc + ") "
            End If

            Select Case NavType
                Case NavigatorType.Current
                    qry += " and tspl_sd_sale_return_head.document_code='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and tspl_sd_sale_return_head.document_code in (Select min(document_code) from tspl_sd_sale_return_head where trans_type='CPR' " + loc_code + ")"
                Case NavigatorType.Last
                    qry += " and tspl_sd_sale_return_head.document_code in (Select max(document_code) from tspl_sd_sale_return_head where trans_type='CPR' " + loc_code + ")"
                Case NavigatorType.Next
                    qry += " and tspl_sd_sale_return_head.document_code in (Select min(document_code) from tspl_sd_sale_return_head where trans_type='CPR' " + loc_code + " and document_code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and tspl_sd_sale_return_head.document_code in (Select max(document_code) from tspl_sd_sale_return_head where trans_type='CPR' " + loc_code + " and document_code<'" + strCode + "')"
            End Select
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("customer_code"))
                obj.Cust_Name = clsCommon.myCstr(dt.Rows(0)("customer_name"))
                obj.Description = clsCommon.myCstr(dt.Rows(0)("description"))
                obj.Return_Type = clsCommon.myCstr(dt.Rows(0)("Return_Type"))
                obj.Document_Amount = clsCommon.myCdbl(dt.Rows(0)("total_amt"))
                obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("document_code"))
                obj.Documnet_Date = clsCommon.myCDate(dt.Rows(0)("document_date"))
                obj.CSA_Location_Code = clsCommon.myCstr(dt.Rows(0)("CSA_Loc_Code"))
                obj.CSA_location_desc = clsCommon.myCstr(dt.Rows(0)("csaloc"))
                obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("bill_to_location"))
                obj.location_desc = clsCommon.myCstr(dt.Rows(0)("location_desc"))
                obj.Status = clsCommon.myCdbl(dt.Rows(0)("status"))
                obj.Is_Cancelled = clsCommon.myCdbl(dt.Rows(0)("Is_Cancelled"))

                obj.Arr = clsCSASalePattiReturnDetail.GetData(obj.Document_Code, trans)
            End If ''end dt

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dt = Nothing
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_SD_SALE_RETURN_Detail where document_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsBatchInventory.DeleteData("CSA-SALEPATTI-RETURN", strCode, trans)

            qry = "delete from TSPL_SD_SALE_RETURN_HEAD where document_code='" + strCode + "' and trans_type='CPR'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strCode, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim obj As New clsCSASalePattiReturnHead()
        Try
            Dim isSaved As Boolean = True

            obj = clsCSASalePattiReturnHead.GetData(strCode, Nothing, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Document already posted")
            End If

            isSaved = isSaved AndAlso SendToInventoryMovement(obj, trans)
            'If (clsCommon.CompairString(obj.Return_Type, "D") = CompairStringResult.Equal) Then 'OrElse clsCommon.CompairString(obj.Return_Type, "S") = CompairStringResult.Equal
            '    ''if return type is damage then no inventory hit and no debit note
            'Else
            isSaved = isSaved AndAlso createARInvoice(obj, trans)
            'End If

            Dim qry As String = "Update TSPL_SD_SALE_RETURN_HEAD set Status=1,Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "',Modify_By='" + objCommonVar.CurrentUserCode + "', Modify_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "' where DOCument_CODE='" + strCode + "' and trans_type='CPR'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Function

    Public Shared Function SendToInventoryMovement(ByVal obj As clsCSASalePattiReturnHead, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim ArrInventoryMovementOut As New List(Of clsInventoryMovement)
        Dim ArrInventoryMovementIn As New List(Of clsInventoryMovement)
        Try

            Dim isSaved As Boolean = True

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0 Then
                ArrInventoryMovementOut = New List(Of clsInventoryMovement)
                ArrInventoryMovementIn = New List(Of clsInventoryMovement)

                Dim intCounter As Integer = 0

                For Each objTr As clsCSASalePattiReturnDetail In obj.Arr
                    intCounter = intCounter + 1

                    '' In at csa location
                    Dim strItemType As String = clsItemMaster.GetItemType(objTr.Item_Code, trans)
                    Dim strItemTypeToSave As String = ""
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        strItemTypeToSave = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        strItemTypeToSave = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        strItemTypeToSave = "FT"
                    Else
                        strItemTypeToSave = strItemType
                    End If

                    Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.UOM, trans)
                    If ConvFac = 0 Then
                        Throw New Exception("Conversion Factor found zero for item :" + objTr.Item_Code + " and Uom:'" + objTr.UOM)
                    End If

                    Dim objInventoryMovemnt As New clsInventoryMovement()

                    objInventoryMovemnt.Ref_Line_No = objTr.Line_No

                    objInventoryMovemnt.InOut = "I"
                    objInventoryMovemnt.Location_Code = obj.CSA_Location_Code
                    objInventoryMovemnt.Item_Code = objTr.Item_Code
                    objInventoryMovemnt.Item_Desc = objTr.Item_Name
                    objInventoryMovemnt.Qty = objTr.Qty
                    objInventoryMovemnt.UOM = objTr.UOM
                    objInventoryMovemnt.Basic_Cost = objTr.Unit_Rate
                    objInventoryMovemnt.MRP = 0
                    objInventoryMovemnt.Add_Cost = 0
                    objInventoryMovemnt.Net_Cost = objTr.Unit_Rate
                    objInventoryMovemnt.Cust_Code = obj.Cust_Code
                    objInventoryMovemnt.Cust_Name = obj.Cust_Name
                    objInventoryMovemnt.BatchSkipOnSetting = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.BatchSkipCSAReturn, clsFixedParameterCode.BatchSkipCSAReturn, trans)) = "1", True, False))

                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "FT"
                    End If

                    Dim cost As Decimal = 0

                    cost = clsInventoryMovementNew.GetCost(EnumCostingMethod.Averege, objTr.Item_Code, obj.CSA_Location_Code, objTr.Qty, obj.Documnet_Date, clsCommon.GETSERVERDATE(trans), True, trans)
                    objInventoryMovemnt.FIFO_Cost = cost

                    cost = clsInventoryMovementNew.GetCost(EnumCostingMethod.Averege, objTr.Item_Code, obj.CSA_Location_Code, objTr.Qty, obj.Documnet_Date, clsCommon.GETSERVERDATE(trans), True, trans)
                    objInventoryMovemnt.Avg_Cost = cost

                    cost = clsInventoryMovementNew.GetCost(EnumCostingMethod.Averege, objTr.Item_Code, obj.CSA_Location_Code, objTr.Qty, obj.Documnet_Date, clsCommon.GETSERVERDATE(trans), True, trans)
                    objInventoryMovemnt.LIFO_Cost = cost

                    objInventoryMovemnt.ItemType = strItemTypeToSave
                    ArrInventoryMovementOut.Add(objInventoryMovemnt)
                Next
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData("CSA-SALEPATTI-RETURN", obj.Document_Code, obj.Documnet_Date, clsCommon.GetPrintDate(obj.Documnet_Date, "dd/MM/yyyy"), ArrInventoryMovementOut, trans)
            End If ''obj cond.

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            ArrInventoryMovementOut = Nothing
            ArrInventoryMovementIn = Nothing
        End Try
    End Function

    Private Shared Function createARInvoice(ByVal obj As clsCSASalePattiReturnHead, ByVal trans As SqlTransaction) As Boolean
        Try
            '=================Added by preeti Gupta==========================
            Dim ItemWiseCSAAccount As Boolean = False
            Dim AlwSaleMfgAcctng As Boolean = False

            ItemWiseCSAAccount = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowItemWiseCSAAccountingON_CSASale, clsFixedParameterCode.AllowItemWiseCSAAccountingON_CSASale, trans)) = "1", True, False))
            AlwSaleMfgAcctng = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.Allow_SaleMfgACONCSAPatti, clsFixedParameterCode.Allow_SaleMfgACONCSAPatti, trans)) = "1", True, False))

            '===============================================================
            ''''''''''''''''''''''''''''''''''For Making AR Invoice
            Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)

            Dim StopGLForConsignment As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.StopGLEntryForConsignmentAtCSATransfer, clsFixedParameterCode.StopGLEntryForConsignmentAtCSATransfer, trans))
            If StopGLForConsignment = "1" Then
                isSkipCogsGL = False
            End If

            Dim objCustInv As New clsCustomerInvoiceHead()
            ''objCustInv.Document_No ''Will be Generateed
            objCustInv.Document_Date = obj.Documnet_Date
            objCustInv.Document_Type = "C"
            objCustInv.Document_Total = obj.Document_Amount
            objCustInv.Customer_Code = obj.Cust_Code
            objCustInv.Customer_Name = obj.Cust_Name
            objCustInv.Posting_Date = clsCommon.myCDate(clsCommon.GETSERVERDATE(trans))
            Dim qry As String = " select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.Cust_Code + "'"
            objCustInv.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            ''objCustInv.Order_No
            objCustInv.loc_code = clsCommon.myCstr(clsLocation.GetSegmentCode(obj.Location_Code, trans))
            objCustInv.On_Hold = 0
            'objCustInv.Remarks = obj.Remarks
            objCustInv.Description = "AR Credit note against CSA Sale Patti Return no. " + obj.Document_Code + ",dated: " + obj.Documnet_Date + ""
            objCustInv.Tax_Group = "" ' obj.tax_group_code
            objCustInv.TAX1 = "" 'obj.TAX1
            objCustInv.TAX1_Rate = 0 ' obj.TAX1_Rate
            objCustInv.TAX1_Amt = 0 ' obj.TAX1_Amt
            objCustInv.TAX2 = "" ' obj.TAX2_Amt
            objCustInv.TAX2_Rate = 0 'obj.TAX2_Rate
            objCustInv.TAX2_Amt = 0 'obj.TAX2_Amt
            objCustInv.TAX3 = "" ' obj.TAX3
            objCustInv.TAX3_Rate = 0 ' obj.TAX3_Rate
            objCustInv.TAX3_Amt = 0 ' obj.TAX3_Amt
            objCustInv.TAX4 = "" ' obj.TAX4
            objCustInv.TAX4_Rate = 0 'obj.TAX4_Rate
            objCustInv.TAX4_Amt = 0 ' obj.TAX4_Amt
            objCustInv.TAX5 = "" ' obj.TAX5
            objCustInv.TAX5_Rate = 0 'obj.TAX5_Rate
            objCustInv.TAX5_Amt = 0 'obj.TAX5_Amt
            objCustInv.TAX6 = "" ' obj.TAX6
            objCustInv.TAX6_Rate = 0 ' obj.TAX6_Rate
            objCustInv.TAX6_Amt = 0 'obj.TAX6_Amt
            objCustInv.TAX7 = "" ' obj.TAX7
            objCustInv.TAX7_Rate = 0 ' obj.TAX7_Rate
            objCustInv.TAX7_Amt = 0 ' obj.TAX7_Amt
            objCustInv.TAX8 = "" 'obj.TAX8
            objCustInv.TAX8_Rate = 0 ' obj.TAX8_Rate
            objCustInv.TAX8_Amt = 0 ' obj.TAX8_Amt
            objCustInv.TAX9 = "" ' obj.TAX9
            objCustInv.TAX9_Rate = 0 ' obj.TAX9_Rate
            objCustInv.TAX9_Amt = 0 ' obj.TAX9_Amt
            objCustInv.TAX10 = "" 'obj.TAX10
            objCustInv.TAX10_Rate = 0 ' obj.TAX10_Rate
            objCustInv.TAX10_Amt = 0 ' obj.TAX10_Amt
            objCustInv.Total_Tax = 0 ' obj.lbltaxamt
            objCustInv.Tax1_BAmount = 0 ' obj.TAX1_Base_Amt
            objCustInv.Tax2_BAmount = 0 ' obj.TAX2_Base_Amt
            objCustInv.Tax3_BAmount = 0 ' obj.TAX3_Base_Amt
            objCustInv.Tax4_BAmount = 0 ' obj.TAX4_Base_Amt
            objCustInv.Tax5_BAmount = 0 ' obj.TAX5_Base_Amt
            objCustInv.Tax6_BAmount = 0 'obj.TAX6_Base_Amt
            objCustInv.Tax7_BAmount = 0 ' obj.TAX7_Base_Amt
            objCustInv.Tax8_BAmount = 0 ' obj.TAX8_Base_Amt
            objCustInv.Tax9_BAmount = 0 ' obj.TAX9_Base_Amt
            objCustInv.Tax10_BAmount = 0 ' obj.TAX10_Base_Amt
            objCustInv.Balance_Amt = obj.Document_Amount
            objCustInv.Terms_Code = Nothing

            '' currency details
            objCustInv.CURRENCY_CODE = Nothing
            objCustInv.ConvRate = Nothing
            objCustInv.ApplicableFrom = Nothing
            objCustInv.Trans_Type = "CSA"

            objCustInv.Terms_Description = Nothing
            objCustInv.Due_Date = Nothing
            objCustInv.Discount_Percentage = 0
            objCustInv.Discount_Base = 0
            objCustInv.Discount_Amount = 0
            objCustInv.Amount_Less_Discount = obj.Document_Amount

            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable("select Receivable_Control_acct,Receipts_Discount_acct from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" + objCustInv.Account_Set + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objCustInv.Customer_Control_AC = clsCommon.myCstr(dt.Rows(0)("Receivable_Control_acct"))
                objCustInv.Discount_GL_AC = Nothing
            End If

            objCustInv.RefDocType = "CSA Sale Patti Return"
            objCustInv.RefDocNo = obj.Document_Code
            objCustInv.Tax_Calculation_Type = Nothing
            objCustInv.Against_Sale_Return_No = obj.Document_Code
            Dim counter As Integer = 1
            Dim counter_trans As Integer = 1
            Dim counter_GL As Integer = 1

            objCustInv.Arr = New List(Of clsCustomerInvoiceDetail)

            ''item loop 

            Dim Item_Cost As Double = Nothing


            Dim objCustInvTR As clsCustomerInvoiceDetail = New clsCustomerInvoiceDetail()
            qry = "select tspl_sd_sale_return_detail.FOC_Item,sum(isnull(tspl_sd_sale_return_detail.Amount,0)) as amount,max(tspl_sd_sale_return_detail.Remarks) as remarks,tspl_customer_account_set.Consignment_Acct from tspl_sd_sale_return_detail left outer join tspl_sd_sale_return_head on tspl_sd_sale_return_detail.document_code=tspl_sd_sale_return_head.document_code " & _
                    " left outer join tspl_item_master on tspl_item_master.item_code=tspl_sd_sale_return_detail.item_code "
            If ItemWiseCSAAccount Then
                qry += " left outer join tspl_customer_account_set on tspl_customer_account_set.cust_account=tspl_item_master.cust_account "
            ElseIf Not ItemWiseCSAAccount Then
                qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code left outer join tspl_customer_account_set on tspl_customer_account_set.cust_account=TSPL_CUSTOMER_MASTER.Cust_Account "
            End If

            qry += " where tspl_sd_sale_return_detail.document_code='" + obj.Document_Code + "' and tspl_sd_sale_return_Head.trans_type='CPR' group by tspl_sd_sale_return_detail.FOC_Item,tspl_customer_account_set.Consignment_Acct "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    '====================Item gl entry===============================
                    If clsCommon.myCdbl(dr("foc_item")) > 0 Then
                        Continue For
                    End If
                    If clsCommon.myCdbl(dr("amount")) <= 0 Then
                        Continue For
                    End If

                    objCustInvTR = New clsCustomerInvoiceDetail()
                    objCustInvTR.SNo = counter
                    objCustInvTR.GL_Account_Code = clsCommon.myCstr(dr("Consignment_Acct"))
                    objCustInvTR.GL_Account_Code = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Location_Code, trans))
                    objCustInvTR.GL_Account_Desc = clsCommon.myCstr(clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans))

                    objCustInvTR.Amount = clsCommon.myCdbl(dr("amount"))
                    objCustInvTR.Discount_Per = 0 ' objTr.Disc_Per
                    objCustInvTR.Discount = 0 ' objTr.Disc_Amt
                    objCustInvTR.Amount_less_Discount = clsCommon.myCdbl(dr("amount"))
                    objCustInvTR.TAX1 = "" ' objTr.TAX1
                    objCustInvTR.TAX1_Rate = 0 ' objTr.TAX1_Rate
                    objCustInvTR.TAX1_Amt = 0 ' objTr.TAX1_Amt
                    objCustInvTR.TAX1_Base_Amt = 0 ' objTr.TAX1_Base_Amt
                    objCustInvTR.TAX2 = "" ' objTr.TAX2
                    objCustInvTR.TAX2_Rate = 0 'objTr.TAX2_Rate
                    objCustInvTR.TAX2_Amt = 0 ' objTr.TAX2_Amt
                    objCustInvTR.TAX2_Base_Amt = 0 ' objTr.TAX2_Base_Amt
                    objCustInvTR.TAX3 = "" ' objTr.TAX3
                    objCustInvTR.TAX3_Rate = 0 ' objTr.TAX3_Rate
                    objCustInvTR.TAX3_Amt = 0 ' objTr.TAX3_Amt
                    objCustInvTR.TAX3_Base_Amt = 0 ' objTr.TAX3_Base_Amt
                    objCustInvTR.TAX4 = "" ' objTr.TAX4
                    objCustInvTR.TAX4_Rate = 0 ' objTr.TAX4_Rate
                    objCustInvTR.TAX4_Amt = 0 'objTr.TAX4_Amt
                    objCustInvTR.TAX4_Base_Amt = 0 ' objTr.TAX4_Base_Amt
                    objCustInvTR.TAX5 = "" 'objTr.TAX5
                    objCustInvTR.TAX5_Rate = 0 'objTr.TAX5_Rate
                    objCustInvTR.TAX5_Amt = 0 ' objTr.TAX5_Amt
                    objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                    objCustInvTR.TAX6 = "" ' objTr.TAX6
                    objCustInvTR.TAX6_Rate = 0 ' objTr.TAX6_Rate
                    objCustInvTR.TAX6_Amt = 0 'objTr.TAX6_Amt
                    objCustInvTR.TAX6_Base_Amt = 0 'objTr.TAX6_Base_Amt
                    objCustInvTR.TAX7 = "" 'objTr.TAX7
                    objCustInvTR.TAX7_Rate = 0 'objTr.TAX7_Rate
                    objCustInvTR.TAX7_Amt = 0 'objTr.TAX7_Amt
                    objCustInvTR.TAX7_Base_Amt = 0 ' objTr.TAX7_Base_Amt
                    objCustInvTR.TAX8 = "" ' objTr.TAX8
                    objCustInvTR.TAX8_Rate = 0 ' objTr.TAX8_Rate
                    objCustInvTR.TAX8_Amt = 0 'objTr.TAX8_Amt
                    objCustInvTR.TAX8_Base_Amt = 0 'objTr.TAX8_Base_Amt
                    objCustInvTR.TAX9 = "" ' objTr.TAX9
                    objCustInvTR.TAX9_Rate = 0 ' objTr.TAX9_Rate
                    objCustInvTR.TAX9_Amt = 0 'objTr.TAX9_Amt
                    objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                    objCustInvTR.TAX10 = "" ' objTr.TAX10
                    objCustInvTR.TAX10_Rate = 0 ' objTr.TAX10_Rate
                    objCustInvTR.TAX10_Amt = 0 ' objTr.TAX10_Amt
                    objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                    objCustInvTR.Total_Tax = 0 ' objTr.Item_Tax
                    objCustInvTR.Total_Amount = clsCommon.myCdbl(dr("amount"))
                    objCustInvTR.Remarks = clsCommon.myCstr(dr("remarks"))
                    objCustInvTR.TAX1_Base_Amt = 0 ' objTr.TAX1_Base_Amt
                    objCustInvTR.TAX2_Base_Amt = 0 'objTr.TAX2_Base_Amt
                    objCustInvTR.TAX3_Base_Amt = 0 ' objTr.TAX3_Base_Amt
                    objCustInvTR.TAX4_Base_Amt = 0 ' objTr.TAX4_Base_Amt
                    objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                    objCustInvTR.TAX6_Base_Amt = 0 ' objTr.TAX6_Base_Amt
                    objCustInvTR.TAX7_Base_Amt = 0 ' objTr.TAX7_Base_Amt
                    objCustInvTR.TAX8_Base_Amt = 0 ' objTr.TAX8_Base_Amt
                    objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                    objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                    objCustInvTR.Comments = "CSA SALE Patti Return"
                    objCustInv.Arr.Add(objCustInvTR)
                    counter += 1
                Next
            Else
                If ItemWiseCSAAccount Then
                    Throw New Exception("Please set consignment account for itemwise.")
                Else
                    Throw New Exception("Please set consignment account for customer.")
                End If
            End If

            ''=======sale return account in gl======================
            If AlwSaleMfgAcctng Then
                objCustInvTR = New clsCustomerInvoiceDetail()
                qry = "select tspl_sd_sale_return_detail.FOC_Item,sum(isnull(tspl_sd_sale_return_detail.Amount,0)) as amount,max(tspl_sd_sale_return_detail.Remarks) as remarks,tspl_customer_account_set.gsoc_acct as Consignment_Acct from tspl_sd_sale_return_detail left outer join tspl_sd_sale_return_head on tspl_sd_sale_return_detail.document_code=tspl_sd_sale_return_head.document_code " & _
                        " left outer join tspl_item_master on tspl_item_master.item_code=tspl_sd_sale_return_detail.item_code "
                If ItemWiseCSAAccount Then
                    qry += " left outer join tspl_customer_account_set on tspl_customer_account_set.cust_account=tspl_item_master.cust_account "
                ElseIf Not ItemWiseCSAAccount Then
                    qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code left outer join tspl_customer_account_set on tspl_customer_account_set.cust_account=TSPL_CUSTOMER_MASTER.Cust_Account "
                End If

                qry += " where tspl_sd_sale_return_detail.document_code='" + obj.Document_Code + "' and tspl_sd_sale_return_Head.trans_type='CPR' group by tspl_sd_sale_return_detail.FOC_Item,tspl_customer_account_set.gsoc_acct "
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        '====================Item gl entry===============================
                        If clsCommon.myCdbl(dr("foc_item")) > 0 Then
                            Continue For
                        End If
                        If clsCommon.myCdbl(dr("amount")) <= 0 Then
                            Continue For
                        End If

                        objCustInvTR = New clsCustomerInvoiceDetail()
                        objCustInvTR.SNo = counter
                        objCustInvTR.GL_Account_Code = clsCommon.myCstr(dr("Consignment_Acct"))
                        objCustInvTR.GL_Account_Code = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Location_Code, trans))
                        objCustInvTR.GL_Account_Desc = clsCommon.myCstr(clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans))

                        objCustInvTR.Amount = 0 - clsCommon.myCdbl(dr("amount"))
                        objCustInvTR.Discount_Per = 0 ' objTr.Disc_Per
                        objCustInvTR.Discount = 0 ' objTr.Disc_Amt
                        objCustInvTR.Amount_less_Discount = 0 - clsCommon.myCdbl(dr("amount"))
                        objCustInvTR.TAX1 = "" ' objTr.TAX1
                        objCustInvTR.TAX1_Rate = 0 ' objTr.TAX1_Rate
                        objCustInvTR.TAX1_Amt = 0 ' objTr.TAX1_Amt
                        objCustInvTR.TAX1_Base_Amt = 0 ' objTr.TAX1_Base_Amt
                        objCustInvTR.TAX2 = "" ' objTr.TAX2
                        objCustInvTR.TAX2_Rate = 0 'objTr.TAX2_Rate
                        objCustInvTR.TAX2_Amt = 0 ' objTr.TAX2_Amt
                        objCustInvTR.TAX2_Base_Amt = 0 ' objTr.TAX2_Base_Amt
                        objCustInvTR.TAX3 = "" ' objTr.TAX3
                        objCustInvTR.TAX3_Rate = 0 ' objTr.TAX3_Rate
                        objCustInvTR.TAX3_Amt = 0 ' objTr.TAX3_Amt
                        objCustInvTR.TAX3_Base_Amt = 0 ' objTr.TAX3_Base_Amt
                        objCustInvTR.TAX4 = "" ' objTr.TAX4
                        objCustInvTR.TAX4_Rate = 0 ' objTr.TAX4_Rate
                        objCustInvTR.TAX4_Amt = 0 'objTr.TAX4_Amt
                        objCustInvTR.TAX4_Base_Amt = 0 ' objTr.TAX4_Base_Amt
                        objCustInvTR.TAX5 = "" 'objTr.TAX5
                        objCustInvTR.TAX5_Rate = 0 'objTr.TAX5_Rate
                        objCustInvTR.TAX5_Amt = 0 ' objTr.TAX5_Amt
                        objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                        objCustInvTR.TAX6 = "" ' objTr.TAX6
                        objCustInvTR.TAX6_Rate = 0 ' objTr.TAX6_Rate
                        objCustInvTR.TAX6_Amt = 0 'objTr.TAX6_Amt
                        objCustInvTR.TAX6_Base_Amt = 0 'objTr.TAX6_Base_Amt
                        objCustInvTR.TAX7 = "" 'objTr.TAX7
                        objCustInvTR.TAX7_Rate = 0 'objTr.TAX7_Rate
                        objCustInvTR.TAX7_Amt = 0 'objTr.TAX7_Amt
                        objCustInvTR.TAX7_Base_Amt = 0 ' objTr.TAX7_Base_Amt
                        objCustInvTR.TAX8 = "" ' objTr.TAX8
                        objCustInvTR.TAX8_Rate = 0 ' objTr.TAX8_Rate
                        objCustInvTR.TAX8_Amt = 0 'objTr.TAX8_Amt
                        objCustInvTR.TAX8_Base_Amt = 0 'objTr.TAX8_Base_Amt
                        objCustInvTR.TAX9 = "" ' objTr.TAX9
                        objCustInvTR.TAX9_Rate = 0 ' objTr.TAX9_Rate
                        objCustInvTR.TAX9_Amt = 0 'objTr.TAX9_Amt
                        objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                        objCustInvTR.TAX10 = "" ' objTr.TAX10
                        objCustInvTR.TAX10_Rate = 0 ' objTr.TAX10_Rate
                        objCustInvTR.TAX10_Amt = 0 ' objTr.TAX10_Amt
                        objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                        objCustInvTR.Total_Tax = 0 ' objTr.Item_Tax
                        objCustInvTR.Total_Amount = 0 - clsCommon.myCdbl(dr("amount"))
                        objCustInvTR.Remarks = clsCommon.myCstr(dr("remarks"))
                        objCustInvTR.TAX1_Base_Amt = 0 ' objTr.TAX1_Base_Amt
                        objCustInvTR.TAX2_Base_Amt = 0 'objTr.TAX2_Base_Amt
                        objCustInvTR.TAX3_Base_Amt = 0 ' objTr.TAX3_Base_Amt
                        objCustInvTR.TAX4_Base_Amt = 0 ' objTr.TAX4_Base_Amt
                        objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                        objCustInvTR.TAX6_Base_Amt = 0 ' objTr.TAX6_Base_Amt
                        objCustInvTR.TAX7_Base_Amt = 0 ' objTr.TAX7_Base_Amt
                        objCustInvTR.TAX8_Base_Amt = 0 ' objTr.TAX8_Base_Amt
                        objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                        objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                        objCustInvTR.Comments = "CSA SALE Patti Return"
                        objCustInv.Arr.Add(objCustInvTR)
                        counter += 1
                    Next
                Else
                    If ItemWiseCSAAccount Then
                        Throw New Exception("Please set consignment account for itemwise.")
                    Else
                        Throw New Exception("Please set consignment account for customer.")
                    End If
                End If

                ''===================================
                counter += 1
                objCustInvTR = New clsCustomerInvoiceDetail()
                qry = "select tspl_sd_sale_return_detail.FOC_Item,sum(isnull(tspl_sd_sale_return_detail.Amount,0)) as amount,max(tspl_sd_sale_return_detail.Remarks) as remarks,TSPL_SALES_ACCOUNTS.Sales_Return_Account as Consignment_Acct from tspl_sd_sale_return_detail left outer join tspl_sd_sale_return_head on tspl_sd_sale_return_detail.document_code=tspl_sd_sale_return_head.document_code " & _
                        " left outer join tspl_item_master on tspl_item_master.item_code=tspl_sd_sale_return_detail.item_code left outer join TSPL_SALES_ACCOUNTS on TSPL_ITEM_MASTER.Sale_Class_Code=TSPL_SALES_ACCOUNTS.Sales_Class_Code "
                qry += " where tspl_sd_sale_return_detail.document_code='" + obj.Document_Code + "' and tspl_sd_sale_return_Head.trans_type='CPR' group by tspl_sd_sale_return_detail.FOC_Item,TSPL_SALES_ACCOUNTS.Sales_Return_Account "
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        '====================Item gl entry===============================
                        If clsCommon.myCdbl(dr("foc_item")) > 0 Then
                            Continue For
                        End If
                        If clsCommon.myCdbl(dr("amount")) <= 0 Then
                            Continue For
                        End If

                        objCustInvTR = New clsCustomerInvoiceDetail()
                        objCustInvTR.SNo = counter
                        objCustInvTR.GL_Account_Code = clsCommon.myCstr(dr("Consignment_Acct"))
                        objCustInvTR.GL_Account_Code = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Location_Code, trans))
                        objCustInvTR.GL_Account_Desc = clsCommon.myCstr(clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans))

                        objCustInvTR.Amount = clsCommon.myCdbl(dr("amount"))
                        objCustInvTR.Discount_Per = 0 ' objTr.Disc_Per
                        objCustInvTR.Discount = 0 ' objTr.Disc_Amt
                        objCustInvTR.Amount_less_Discount = clsCommon.myCdbl(dr("amount"))
                        objCustInvTR.TAX1 = "" ' objTr.TAX1
                        objCustInvTR.TAX1_Rate = 0 ' objTr.TAX1_Rate
                        objCustInvTR.TAX1_Amt = 0 ' objTr.TAX1_Amt
                        objCustInvTR.TAX1_Base_Amt = 0 ' objTr.TAX1_Base_Amt
                        objCustInvTR.TAX2 = "" ' objTr.TAX2
                        objCustInvTR.TAX2_Rate = 0 'objTr.TAX2_Rate
                        objCustInvTR.TAX2_Amt = 0 ' objTr.TAX2_Amt
                        objCustInvTR.TAX2_Base_Amt = 0 ' objTr.TAX2_Base_Amt
                        objCustInvTR.TAX3 = "" ' objTr.TAX3
                        objCustInvTR.TAX3_Rate = 0 ' objTr.TAX3_Rate
                        objCustInvTR.TAX3_Amt = 0 ' objTr.TAX3_Amt
                        objCustInvTR.TAX3_Base_Amt = 0 ' objTr.TAX3_Base_Amt
                        objCustInvTR.TAX4 = "" ' objTr.TAX4
                        objCustInvTR.TAX4_Rate = 0 ' objTr.TAX4_Rate
                        objCustInvTR.TAX4_Amt = 0 'objTr.TAX4_Amt
                        objCustInvTR.TAX4_Base_Amt = 0 ' objTr.TAX4_Base_Amt
                        objCustInvTR.TAX5 = "" 'objTr.TAX5
                        objCustInvTR.TAX5_Rate = 0 'objTr.TAX5_Rate
                        objCustInvTR.TAX5_Amt = 0 ' objTr.TAX5_Amt
                        objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                        objCustInvTR.TAX6 = "" ' objTr.TAX6
                        objCustInvTR.TAX6_Rate = 0 ' objTr.TAX6_Rate
                        objCustInvTR.TAX6_Amt = 0 'objTr.TAX6_Amt
                        objCustInvTR.TAX6_Base_Amt = 0 'objTr.TAX6_Base_Amt
                        objCustInvTR.TAX7 = "" 'objTr.TAX7
                        objCustInvTR.TAX7_Rate = 0 'objTr.TAX7_Rate
                        objCustInvTR.TAX7_Amt = 0 'objTr.TAX7_Amt
                        objCustInvTR.TAX7_Base_Amt = 0 ' objTr.TAX7_Base_Amt
                        objCustInvTR.TAX8 = "" ' objTr.TAX8
                        objCustInvTR.TAX8_Rate = 0 ' objTr.TAX8_Rate
                        objCustInvTR.TAX8_Amt = 0 'objTr.TAX8_Amt
                        objCustInvTR.TAX8_Base_Amt = 0 'objTr.TAX8_Base_Amt
                        objCustInvTR.TAX9 = "" ' objTr.TAX9
                        objCustInvTR.TAX9_Rate = 0 ' objTr.TAX9_Rate
                        objCustInvTR.TAX9_Amt = 0 'objTr.TAX9_Amt
                        objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                        objCustInvTR.TAX10 = "" ' objTr.TAX10
                        objCustInvTR.TAX10_Rate = 0 ' objTr.TAX10_Rate
                        objCustInvTR.TAX10_Amt = 0 ' objTr.TAX10_Amt
                        objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                        objCustInvTR.Total_Tax = 0 ' objTr.Item_Tax
                        objCustInvTR.Total_Amount = clsCommon.myCdbl(dr("amount"))
                        objCustInvTR.Remarks = clsCommon.myCstr(dr("remarks"))
                        objCustInvTR.TAX1_Base_Amt = 0 ' objTr.TAX1_Base_Amt
                        objCustInvTR.TAX2_Base_Amt = 0 'objTr.TAX2_Base_Amt
                        objCustInvTR.TAX3_Base_Amt = 0 ' objTr.TAX3_Base_Amt
                        objCustInvTR.TAX4_Base_Amt = 0 ' objTr.TAX4_Base_Amt
                        objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                        objCustInvTR.TAX6_Base_Amt = 0 ' objTr.TAX6_Base_Amt
                        objCustInvTR.TAX7_Base_Amt = 0 ' objTr.TAX7_Base_Amt
                        objCustInvTR.TAX8_Base_Amt = 0 ' objTr.TAX8_Base_Amt
                        objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                        objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                        objCustInvTR.Comments = "CSA SALE Patti Return"
                        objCustInv.Arr.Add(objCustInvTR)
                        counter += 1
                    Next
                Else
                    Throw New Exception("Please set sale return account for item.")
                End If
            End If
            ''======================end here======================================


            ''---------------check if there is already any AR is made,means entry is reposted after unpost
            qry = "select document_no from TSPL_Customer_Invoice_Head where trans_type='CSA' and RefDocType='CSA Sale Patti Return' and RefDocNo='" + obj.Document_Code + "' and Against_Sale_Return_No='" + obj.Document_Code + "'"
            Dim OLDARNO As String = ""
            OLDARNO = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

            qry = "select voucher_no from TSPL_JOURNAL_MASTER where source_code='AR-CR' and source_doc_no='" + OLDARNO + "' " ' and Against_Sale_No='" + obj.docno + "'"
            Dim OLD_VoucherNO As String = ""
            OLD_VoucherNO = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

            If clsCommon.myLen(OLDARNO) > 0 Then
                objCustInv.Document_No = OLDARNO
                objCustInv.SaveData(objCustInv, False, trans, clsUserMgtCode.frmCSASalePattiReturn, Nothing, OLDARNO) '"CSA-SALE"
            Else
                objCustInv.SaveData(objCustInv, True, trans, clsUserMgtCode.frmCSASalePattiReturn) '"CSA-SALE"
            End If

            If clsCommon.myLen(OLD_VoucherNO) > 0 Then
                clsCustomerInvoiceHead.PostData(clsUserMgtCode.frmCSASalePattiReturn, objCustInv.Document_No, "", trans, OLD_VoucherNO)
            Else
                clsCustomerInvoiceHead.PostData(clsUserMgtCode.frmCSASalePattiReturn, objCustInv.Document_No, "", trans)
            End If
            ''---------------------------------------------------------------------------------------------


            objCustInvTR = Nothing
            objCustInv = Nothing
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
End Class

Public Class clsCSASalePattiReturnDetail
#Region "variables"
    Public Document_Code As String = Nothing
    Public Line_No As Integer = Nothing
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public Is_FOC As Boolean = False
    Public CSA_Type As String = Nothing
    Public UOM As String = Nothing
    Public Unit_Rate As Decimal = Nothing
    Public Qty As Decimal = Nothing
    Public Amount As Decimal = Nothing
    Public Remarks As String = Nothing

    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal bill_to_location As String, ByVal strDocDate As DateTime, ByVal CSA_Loc_COde As String, ByVal Arr As List(Of clsCSASalePattiReturnDetail), ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            Dim qry As String = "delete from tspl_sd_sale_return_detail where document_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsCSASalePattiReturnDetail In Arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Location", bill_to_location)
                    clsCommon.AddColumnsForChange(coll, "Invoice_Code", "", True)
                    clsCommon.AddColumnsForChange(coll, "FOC_Item", IIf(obj.Is_FOC = True, 1, 0))
                    clsCommon.AddColumnsForChange(coll, "Row_Type", "Item")
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", obj.UOM, True)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Unit_Rate)
                    clsCommon.AddColumnsForChange(coll, "Total_Basic_Amt", obj.Amount)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALE_RETURN_detail", OMInsertOrUpdate.Insert, "", trans)

                    If obj.arrBatchItem IsNot Nothing AndAlso obj.arrBatchItem.Count > 0 Then
                        clsBatchInventory.SaveData("CSA-SALEPATTI-RETURN", strCode, strDocDate, "I", obj.Item_Code, CSA_Loc_COde, obj.Line_No, 0, obj.UOM, obj.arrBatchItem, trans)
                    End If
                Next
            End If ''end arr cond.

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsCSASalePattiReturnDetail)
        Dim Arr As New List(Of clsCSASalePattiReturnDetail)
        Dim dt As New DataTable()
        Dim obj As New clsCSASalePattiReturnDetail()
        Try
            Dim qry As String = "select TSPL_SD_SALE_RETURN_detail.*,tspl_item_master.item_desc,tspl_item_master.csa_type from TSPL_SD_SALE_RETURN_detail left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_RETURN_detail.item_code " & _
                " where TSPL_SD_SALE_RETURN_detail.document_code='" + strCode + "' "
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    obj = New clsCSASalePattiReturnDetail()
                    obj.arrBatchItem = New List(Of clsBatchInventory)

                    obj.Amount = clsCommon.myCdbl(dr("amount"))
                    obj.CSA_Type = clsCommon.myCstr(dr("csa_type"))
                    obj.Document_Code = clsCommon.myCstr(dr("document_code"))
                    obj.Is_FOC = IIf(clsCommon.myCdbl(dr("foc_item")) = 1, True, False)
                    obj.Item_Code = clsCommon.myCstr(dr("item_code"))
                    obj.Item_Name = clsCommon.myCstr(dr("item_desc"))
                    obj.Line_No = clsCommon.myCdbl(dr("line_no"))
                    obj.Qty = clsCommon.myCdbl(dr("qty"))
                    obj.Remarks = clsCommon.myCstr(dr("remarks"))
                    obj.Unit_Rate = clsCommon.myCdbl(dr("item_cost"))
                    obj.UOM = clsCommon.myCstr(dr("unit_code"))

                    obj.arrBatchItem = clsBatchInventory.GetData("CSA-SALEPATTI-RETURN", obj.Document_Code, obj.Item_Code, obj.Line_No, trans)

                    Arr.Add(obj)
                Next
            End If ''end dt

            Return Arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dt = Nothing
            obj = Nothing
        End Try
    End Function
End Class
