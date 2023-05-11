''Created By Monika 24/03/2015
Imports common
Imports System.Data.SqlClient

Public Class clsCSATransferReturnHead
#Region "Variables"
    Public Gate_ReturnNo As String = Nothing
    Public Document_Code As String = Nothing
    Public Document_Date As Date? = Nothing
    Public Description As String = Nothing
    Public Customer_Code As String = Nothing
    Public Customer_Name As String = Nothing
    Public CSA_Loc_Code As String = Nothing
    Public Status As Integer = Nothing
    Public Bill_To_Location As String = Nothing
    Public Location_Desc As String = Nothing
    Public Total_Amt As Decimal = Nothing
    Public Tax_Group As String = Nothing
    Public Posting_Date As DateTime = Nothing
    Public Return_Type As String = Nothing
    Public Gate_Entry_No As String = Nothing

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

    Public Amount_Less_Discount As Decimal = 0
    Public Total_Tax_Amt As Decimal = 0

    Public Arr As List(Of clsCSATransferReturnDetail) = Nothing
#End Region
    Public Shared Function GetFinder(ByVal whrCls As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = Nothing
        Dim qry As String = "select document_code as Code,document_Date as [Doc Date],[Description],TSPL_SD_SALE_RETURN_HEAD.customer_code as [CSA Code],tspl_customer_master.customer_name as [CSA Name],tspl_location_master.location_desc as [Location],(case when TSPL_SD_SALE_RETURN_HEAD.status=1 then 'Posted' else 'Unposted' end) as Status,(case when TSPL_SD_SALE_RETURN_HEAD.Return_Type='D' then 'Damage Goods' when TSPL_SD_SALE_RETURN_HEAD.Return_Type='S' then 'Shortage Goods' else 'Return Goods' end) as [Type] "
        qry += " from TSPL_SD_SALE_RETURN_HEAD left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SD_SALE_RETURN_HEAD.customer_code left outer join tspl_location_master on tspl_location_master.location_code=TSPL_SD_SALE_RETURN_HEAD.bill_to_location "

        If clsCommon.myLen(whrCls) > 0 Then
            whrCls += " and TSPL_SD_SALE_RETURN_HEAD.trans_type='CSA' "
        Else
            whrCls = " TSPL_SD_SALE_RETURN_HEAD.trans_type='CSA' "
        End If

        str = clsCommon.myCstr(clsCommon.ShowSelectForm("CSATRNRETUN", qry, "Code", whrCls, strCurrCode, "Code", isButtonClicked))

        Return str
    End Function

    Public Shared Function GetDescription(Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim str As String = ""
        Dim qry As String = "select top 1 description from tspl_sd_sale_return_head where trans_type='CSA' and isnull(description,'')<>'' order by document_date desc"
        str = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As clsCSATransferReturnHead, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso SaveData(obj, isNewEntry, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsCSATransferReturnHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleCSASale, clsUserMgtCode.frmCSATransferReturn, obj.Bill_To_Location, obj.Document_Date, trans)
            clsBatchInventory.DeleteData("SD-CSATRANS-RETURN", obj.Document_Code, trans)

            Dim isSaved As Boolean = True

            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSaleReturn, Nothing, obj.Bill_To_Location)
            End If

            coll = New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Trans_Type", "CSA")
            clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "CSA_Loc_Code", obj.CSA_Loc_Code)
            clsCommon.AddColumnsForChange(coll, "Bill_To_Location", obj.Bill_To_Location)
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Amount_Less_Discount", obj.Amount_Less_Discount)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Comments", "Csa Transfer Return")
            clsCommon.AddColumnsForChange(coll, "tax_group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Return_Type", obj.Return_Type)
            clsCommon.AddColumnsForChange(coll, "Gate_ReturnNo", obj.Gate_ReturnNo)
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", obj.Gate_Entry_No, True)

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

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALE_RETURN_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALE_RETURN_HEAD", OMInsertOrUpdate.Update, " document_code='" + obj.Document_Code + "' and trans_type='CSA' ", trans)
            End If


            isSaved = isSaved AndAlso clsCSATransferReturnDetail.SaveData(obj.Document_Code, obj.Document_Date, obj.Bill_To_Location, obj.CSA_Loc_Code, obj.Arr, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            coll = Nothing
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso DeleteData(strCode, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Try
            Dim isSaved As Boolean = True

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Document_Date,Bill_To_Location from TSPL_SD_SALE_RETURN_HEAD where document_code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleCSASale, clsUserMgtCode.frmCSATransferReturn, clsCommon.myCstr(dt.Rows(0)("Bill_To_Location")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)


            End If
            ''delete from inventory
            qry = "delete from tspl_inventory_movement where trans_type='SD-CSATRANS-RETURN' and Source_Doc_No='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''-------end here====

            clsBatchInventory.DeleteData("SD-CSATRANS-RETURN", strCode, trans)

            ''-----------unpost GL--------------
            qry = "delete from TSPL_JOURNAL_MASTER where source_code='CS-RC' and source_doc_no ='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''-------------------------------------------------

            qry = "delete from TSPL_SD_SALE_RETURN_detail where document_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SD_SALE_RETURN_HEAD where document_code='" + strCode + "' and trans_Type='CSA'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal FormID As String, ByVal strCode As String, ByVal arrLoc As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso PostData(FormID, strCode, arrLoc, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal FormID As String, ByVal strCode As String, ByVal arrLoc As String, ByVal trans As SqlTransaction) As Boolean
        Dim obj As New clsCSATransferReturnHead()
        Try
            Dim isSaved As Boolean = True

            obj = clsCSATransferReturnHead.GetData(strCode, Nothing, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleCSASale, clsUserMgtCode.frmCSATransferReturn, obj.Bill_To_Location, obj.Document_Date, trans)


            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Document already post on :" + obj.Posting_Date)
            End If


            isSaved = isSaved AndAlso SendToInventoryMovement(obj, trans)
            'comment by Balwinder cogs is handled in JE funcion on 02/02/2016
            'If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = True Then
            isSaved = isSaved AndAlso CreateJournalEntry(obj.Document_Code, obj, trans)
            'End If



            Dim qry As String = "Update TSPL_SD_SALE_RETURN_HEAD set Status=1, Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "',Modify_By='" + objCommonVar.CurrentUserCode + "', Modify_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "' where DOCument_CODE='" + strCode + "' and trans_type='CSA'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Function

    Public Shared Function SendToInventoryMovement(ByVal obj As clsCSATransferReturnHead, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim ArrInventoryMovementOut As New List(Of clsInventoryMovement)
        Dim ArrInventoryMovementIn As New List(Of clsInventoryMovement)
        Try

            Dim isSaved As Boolean = True

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0 Then
                ArrInventoryMovementOut = New List(Of clsInventoryMovement)
                ArrInventoryMovementIn = New List(Of clsInventoryMovement)

                Dim strRgpNo As String = Nothing
                Dim intCounter As Integer = 0

                For Each objTr As clsCSATransferReturnDetail In obj.Arr
                    intCounter = intCounter + 1

                    '' In from from location
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

                    Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Unit_code, trans)
                    If ConvFac = 0 Then
                        Throw New Exception("Conversion Factor found zero for item :" + objTr.Item_Code + " and Uom:'" + objTr.Unit_code)
                    End If

                    Dim objInventoryMovemnt As New clsInventoryMovement()

                    objInventoryMovemnt.Ref_Line_No = objTr.Line_No

                    objInventoryMovemnt.InOut = "I"
                    objInventoryMovemnt.Location_Code = obj.Bill_To_Location
                    objInventoryMovemnt.Item_Code = objTr.Item_Code
                    objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                    objInventoryMovemnt.Qty = objTr.Qty '+ objTr.Free_Qty
                    objInventoryMovemnt.UOM = objTr.Unit_code
                    objInventoryMovemnt.Basic_Cost = objTr.Item_Cost
                    objInventoryMovemnt.MRP = 0
                    objInventoryMovemnt.Add_Cost = 0
                    objInventoryMovemnt.Net_Cost = objTr.Item_Cost
                    objInventoryMovemnt.Other_Location_Code = obj.CSA_Loc_Code
                    objInventoryMovemnt.Other_Location_Desc = clsLocation.GetName(obj.CSA_Loc_Code, trans)
                    objInventoryMovemnt.Cust_Code = obj.Customer_Code
                    objInventoryMovemnt.Cust_Name = obj.Customer_Name
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "FT"
                    End If

                    Dim cost As Decimal = 0

                    cost = clsInventoryMovementNew.GetCost(EnumCostingMethod.Averege, objTr.Item_Code, obj.Bill_To_Location, objTr.Qty, obj.Document_Date, clsCommon.GETSERVERDATE(trans), True, trans)
                    objInventoryMovemnt.FIFO_Cost = cost

                    cost = clsInventoryMovementNew.GetCost(EnumCostingMethod.Averege, objTr.Item_Code, obj.Bill_To_Location, objTr.Qty, obj.Document_Date, clsCommon.GETSERVERDATE(trans), True, trans)
                    objInventoryMovemnt.Avg_Cost = cost

                    cost = clsInventoryMovementNew.GetCost(EnumCostingMethod.Averege, objTr.Item_Code, obj.Bill_To_Location, objTr.Qty, obj.Document_Date, clsCommon.GETSERVERDATE(trans), True, trans)
                    objInventoryMovemnt.LIFO_Cost = cost

                    objInventoryMovemnt.ItemType = strItemTypeToSave
                    ArrInventoryMovementOut.Add(objInventoryMovemnt)
                Next

                ''when shortage or damaged then no IN inventory hit
                If clsCommon.CompairString(obj.Return_Type, "D") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Return_Type, "S") <> CompairStringResult.Equal Then
                    isSaved = isSaved AndAlso clsInventoryMovement.SaveData("SD-CSATRANS-RETURN", obj.Document_Code, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementOut, trans)
                End If

                For Each objInventoryMovemntIn As clsInventoryMovement In ArrInventoryMovementOut
                    Dim objToInsert As clsInventoryMovement = clsInventoryMovement.DeepCopyObject(objInventoryMovemntIn)
                    objToInsert.InOut = "O"
                    objToInsert.Location_Code = obj.CSA_Loc_Code

                    objToInsert.Other_Location_Code = obj.Bill_To_Location
                    objToInsert.Other_Location_Desc = clsLocation.GetName(obj.Bill_To_Location, trans)

                    ArrInventoryMovementIn.Add(objToInsert)
                Next
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData("SD-CSATRANS-RETURN", obj.Document_Code, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementIn, trans)


            End If ''obj cond.

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            ArrInventoryMovementOut = Nothing
            ArrInventoryMovementIn = Nothing
        End Try
    End Function

    Public Shared Function CreateJournalEntry(ByVal strCode As String, ByVal obj As clsCSATransferReturnHead, ByVal trans As SqlTransaction)
        Dim dt As New DataTable()
        Try
            Dim ItemWiseCSAAccount As Boolean = False
            ItemWiseCSAAccount = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowItemWiseCSAAccountingON_CSASale, clsFixedParameterCode.AllowItemWiseCSAAccountingON_CSASale, trans)) = "1", True, False))
            Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)

            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim strCostOfGoodSold_Acc As String = ""
            Dim strConsignment_Acc As String = ""
            Dim strInventoryControl_Acc As String = ""
            Dim strReceivableControlAcc As String = ""
            Dim strGSOL_Acc As String = ""
            Dim isSaved As Boolean = True
            Dim qry As String
            Dim InnerQry As String


            InnerQry = " select   TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct,TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt, TSPL_SD_SALE_RETURN_DETAIL.DOCument_CODE,TSPL_SD_SALE_RETURN_HEAD.Bill_TO_location,TSPL_SD_SALE_RETURN_HEAD.CSA_LOC_COde, " & _
              " TSPL_SD_SALE_RETURN_HEAD.Customer_Code,TSPL_SD_SALE_RETURN_DETAIL.Line_No,TSPL_SD_SALE_RETURN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc, " & _
              " TSPL_SD_SALE_RETURN_DETAIL.Qty,TSPL_SD_SALE_RETURN_DETAIL.item_cost *  TSPL_SD_SALE_RETURN_DETAIL.Qty as Transfer_Amount, " & _
              " (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=0 then Inv_Movement.Avg_Cost when TSPL_PURCHASE_ACCOUNTS.Costing_Method=1 then FIFO_Cost" & _
              " when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then FIFO_Cost end) as Item_Cost,TSPL_SALES_ACCOUNTS.Cost_Of_Goods_Sold, " & _
              " Cost_Good_GL.Description as Cost_Of_Goods_Sold_Desc, " & _
            " TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,Inv_Conrol_GL.Description as Inv_Control_Account_Desc, "
            If Not ItemWiseCSAAccount Then
                InnerQry += " TSPL_CUSTOMER_ACCOUNT_SET.GSOC_Acct," & _
                    " TSPL_CUSTOMER_ACCOUNT_SET.Consignment_Acct,Consignment_Gl.Description as Consignment_Acct_Desc, "
            Else
                InnerQry += " ItemCustAcc.GSOC_Acct," & _
                    " ItemCustAcc.Consignment_Acct,Consignment_Gl.Description as Consignment_Acct_Desc, "
            End If

            InnerQry += " GSOC_GL.Description as GSOC_Acct_Desc from TSPL_SD_SALE_RETURN_DETAIL " & _
              " inner join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_DETAIL.document_code=TSPL_SD_SALE_RETURN_HEAD.DOCument_CODE " & _
              " left join TSPL_ITEM_MASTER on TSPL_SD_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
              " left join TSPL_SALES_ACCOUNTS on TSPL_ITEM_MASTER.Sale_Class_Code=TSPL_SALES_ACCOUNTS.Sales_Class_Code " & _
              " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " & _
              " left join TSPL_GL_ACCOUNTS as Cost_Good_GL on TSPL_SALES_ACCOUNTS.Cost_Of_Goods_Sold=Cost_Good_GL.Account_Code " & _
              " left join TSPL_GL_ACCOUNTS as Inv_Conrol_GL on TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account=Inv_Conrol_GL.Account_Code " & _
              " left join TSPL_CUSTOMER_MASTER on TSPL_SD_SALE_RETURN_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & _
             "  left join TSPL_CUSTOMER_ACCOUNT_SET on TSPL_CUSTOMER_MASTER.Cust_Account=TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account "
            If Not ItemWiseCSAAccount Then
                InnerQry += " left join TSPL_GL_ACCOUNTS as GSOC_GL on TSPL_CUSTOMER_ACCOUNT_SET.GSOC_Acct=GSOC_GL.Account_Code " & _
                    " left join TSPL_GL_ACCOUNTS as Consignment_Gl on TSPL_CUSTOMER_ACCOUNT_SET.Consignment_Acct=Consignment_Gl.Account_Code "
            Else
                InnerQry += " left join TSPL_CUSTOMER_ACCOUNT_SET as ItemCustAcc on tspl_item_master.Cust_Account=ItemCustAcc.Cust_Account left join TSPL_GL_ACCOUNTS as GSOC_GL on ItemCustAcc.GSOC_Acct=GSOC_GL.Account_Code " & _
                    " left join TSPL_GL_ACCOUNTS as Consignment_Gl on ItemCustAcc.Consignment_Acct=Consignment_Gl.Account_Code "
            End If

            InnerQry += " left join (select item_code,sum(FIFO_Cost) as FIFO_Cost,sum(Avg_Cost) as Avg_Cost ,sum(LIFO_Cost) as LIFO_Cost " & _
              " from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & strCode & "' and InOut='I' group by item_code) as Inv_Movement " & _
              " on TSPL_SD_SALE_RETURN_DETAIL.Item_Code=Inv_Movement.Item_Code " & _
              " where TSPL_SD_SALE_RETURN_HEAD.DOCument_CODE='" & strCode & "' and TSPL_SD_SALE_RETURN_HEAD.Trans_type='CSA' "

            qry = " select Receivable_Control_acct,Item_Code,Item_Desc,Cost_Of_Goods_Sold,Cost_Of_Goods_Sold_Desc,Consignment_Acct,Consignment_Acct_Desc," & _
                  " Inv_Control_Account,Inv_Control_Account_Desc,GSOC_Acct,GSOC_Acct_Desc,SUM(qty) as qty, " & _
                  " SUM(Transfer_Amount) as Transfer_Amount,SUM(coalesce(Item_Cost,0)) as Item_Cost from ( " & InnerQry & "" & _
                  " ) as Final group by Item_Code,Item_Desc,Cost_Of_Goods_Sold,Cost_Of_Goods_Sold_Desc, " & _
                  " Consignment_Acct, Consignment_Acct_Desc, Inv_Control_Account, Inv_Control_Account_Desc,GSOC_Acct,GSOC_Acct_Desc,Receivable_Control_acct"

            '' Validation of GL Itemwise
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    strCostOfGoodSold_Acc = clsCommon.myCstr(dr.Item("Cost_Of_Goods_Sold"))
                    strConsignment_Acc = clsCommon.myCstr(dr.Item("Consignment_Acct"))

                    strInventoryControl_Acc = clsCommon.myCstr(dr.Item("Inv_Control_Account"))
                    strGSOL_Acc = clsCommon.myCstr(dr.Item("GSOC_Acct"))

                    strReceivableControlAcc = clsCommon.myCstr(dr.Item("Receivable_Control_acct"))

                    If isSkipCogsGL Then
                        strCostOfGoodSold_Acc = ""
                        strInventoryControl_Acc = ""
                    End If

                    '' dr cost of goods sold
                    strCostOfGoodSold_Acc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strCostOfGoodSold_Acc, obj.Bill_To_Location, trans)) 'obj.from_location_code 'plant
                    If clsCommon.myLen(strCostOfGoodSold_Acc) = 0 AndAlso Not isSkipCogsGL Then
                        Throw New Exception("Please set Cost of Goods Sold Account for item " & dr.Item("Item_Code") & "")
                    End If

                    '' dr consignment acc
                    strConsignment_Acc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strConsignment_Acc, obj.Bill_To_Location, trans))
                    If clsCommon.myLen(strConsignment_Acc) = 0 Then
                        Throw New Exception("Please set Consignment Account for item " & dr.Item("Item_Code") & "")
                    End If

                    '' cr Inv_Control_Account
                    strInventoryControl_Acc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strInventoryControl_Acc, obj.Bill_To_Location, trans))
                    If clsCommon.myLen(strInventoryControl_Acc) = 0 AndAlso Not isSkipCogsGL Then
                        Throw New Exception("Please set Inventory Control Account for item " & dr.Item("Item_Code") & "")
                    End If

                    '' cr GSOC_Acct
                    strGSOL_Acc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strGSOL_Acc, obj.Bill_To_Location, trans))
                    If clsCommon.myLen(strGSOL_Acc) = 0 Then
                        Throw New Exception("Please set GSOC Account for item " & dr.Item("Item_Code") & "")
                    End If

                    '' dr Receivable_Acct
                    strReceivableControlAcc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strReceivableControlAcc, obj.Bill_To_Location, trans))
                    If clsCommon.myLen(strReceivableControlAcc) = 0 Then
                        Throw New Exception("Please set Receivable Account for item " & dr.Item("Item_Code") & "")
                    End If
                Next
            End If
            Dim GSTStatus As Boolean = clsERPFuncationality.GetGSTStatus(clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            '' Create Financial Entry
            qry = " select Receivable_Control_acct,Cost_Of_Goods_Sold,Cost_Of_Goods_Sold_Desc,Consignment_Acct,Consignment_Acct_Desc," & _
                  " Inv_Control_Account,Inv_Control_Account_Desc,GSOC_Acct,GSOC_Acct_Desc,SUM(qty) as qty, " & _
                  " SUM(Transfer_Amount) as Transfer_Amount,SUM(coalesce(Item_Cost,0)) as Item_Cost,sum(Total_Tax_Amt) as Total_Tax_Amt from ( " & InnerQry & "" & _
                  " ) as Final group by Cost_Of_Goods_Sold,Cost_Of_Goods_Sold_Desc, " & _
                  " Consignment_Acct, Consignment_Acct_Desc, Inv_Control_Account, Inv_Control_Account_Desc,GSOC_Acct,GSOC_Acct_Desc,Receivable_Control_acct"

            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    strCostOfGoodSold_Acc = clsCommon.myCstr(dr.Item("Cost_Of_Goods_Sold"))
                    strConsignment_Acc = clsCommon.myCstr(dr.Item("Consignment_Acct"))

                    strInventoryControl_Acc = clsCommon.myCstr(dr.Item("Inv_Control_Account"))
                    strGSOL_Acc = clsCommon.myCstr(dr.Item("GSOC_Acct"))

                    strReceivableControlAcc = clsCommon.myCstr(dr.Item("Receivable_Control_acct"))
                    If isSkipCogsGL Then
                        strCostOfGoodSold_Acc = ""
                        strInventoryControl_Acc = ""
                    End If

                    '' dr cost of goods sold
                    strCostOfGoodSold_Acc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strCostOfGoodSold_Acc, obj.Bill_To_Location, trans))
                    If clsCommon.myLen(strCostOfGoodSold_Acc) = 0 AndAlso Not isSkipCogsGL Then
                        Throw New Exception("Invalid Cost of Goods Sold Account " & strCostOfGoodSold_Acc & "")
                    End If

                    

                    Dim Acc1() As String = {strCostOfGoodSold_Acc, -1 * clsCommon.myCdbl(dr("Item_Cost"))}

                    If Not isSkipCogsGL Then
                        ArryLstGLAC.Add(Acc1)
                    End If

                    '' dr consignment acc
                    strConsignment_Acc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strConsignment_Acc, obj.Bill_To_Location, trans))
                    If clsCommon.myLen(strConsignment_Acc) = 0 Then
                        Throw New Exception("Invalid Consignment Account " & strConsignment_Acc & "")
                    End If

                    '' dr Receivable_Acct
                    strReceivableControlAcc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strReceivableControlAcc, obj.Bill_To_Location, trans))
                    If clsCommon.myLen(strReceivableControlAcc) = 0 Then
                        Throw New Exception("Invalid Receivable Account " & strReceivableControlAcc & "")
                    End If

                    Dim Acc2() As String
                    If GSTStatus Then
                        'Acc2 = {strConsignment_Acc, -1 * (clsCommon.myCdbl(dr("Transfer_Amount")) + clsCommon.myCdbl(dr("Total_Tax_Amt")))}
                        Acc2 = {strConsignment_Acc, -1 * clsCommon.myCdbl(dr("Transfer_Amount"))}
                    Else
                        Acc2 = {strConsignment_Acc, -1 * clsCommon.myCdbl(dr("Transfer_Amount"))}
                    End If
                    ArryLstGLAC.Add(Acc2)


                    ''richa 26/10/2017
                    If GSTStatus Then
                        Acc2 = {strReceivableControlAcc, -1 * clsCommon.myCdbl(dr("Total_Tax_Amt"))}
                        If clsCommon.myLen(strReceivableControlAcc) > 0 Then
                            ArryLstGLAC.Add(Acc2)
                        End If
                    End If
                    ''---------------

                    '' cr Inv_Control_Account
                    strInventoryControl_Acc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strInventoryControl_Acc, obj.Bill_To_Location, trans))
                    If clsCommon.myLen(strInventoryControl_Acc) = 0 AndAlso Not isSkipCogsGL Then
                        Throw New Exception("Invalid Inventory Control Account " & strInventoryControl_Acc & "")
                    End If
                    Dim Acc3() As String = {strInventoryControl_Acc, 1 * clsCommon.myCdbl(dr("Item_Cost"))}

                    If Not isSkipCogsGL Then
                        ArryLstGLAC.Add(Acc3)
                    End If

                    '' cr GSOC_Acct
                    strGSOL_Acc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strGSOL_Acc, obj.Bill_To_Location, trans))
                    If clsCommon.myLen(strGSOL_Acc) = 0 Then
                        Throw New Exception("Invalid GSOC Account  " & strGSOL_Acc & "")
                    End If
                    Dim Acc4() As String = {strGSOL_Acc, 1 * clsCommon.myCdbl(dr("Transfer_Amount"))}
                    ArryLstGLAC.Add(Acc4)
                Next
            End If

            ''-----------tax included in case of gst
            If GSTStatus Then
                JournalEntryForGST_Common(obj, ArryLstGLAC, obj.Bill_To_Location, trans)
            End If
            ''-----------------
            '===============gl entry for excisable tax======================================
            'If clsCommon.CompairString(obj.Excisable, "1") = CompairStringResult.Equal Then
            '    JE_Excisable_Common(obj, ArryLstGLAC, obj.From_Location_Code, trans)
            'End If
            '=============================================================================

            Dim GLDesc As String = "Journal Entry Against CSA Transfer Return- Doc No." & strCode & " "
            Dim Remarks As String = "Journal Entry against CSA Transfer Return for customer: CAE - " & obj.Customer_Name & "  For Doc No. " & strCode & ""



            ''================check any GL exist or not,in case of reverse document=======
            qry = "select voucher_no from TSPL_JOURNAL_MASTER where source_code='cs-rc' and source_doc_no ='" + obj.Document_Code + "'"
            Dim Old_Voucher_No As String = ""
            Old_Voucher_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(Old_Voucher_No) > 0 Then
                isSaved = isSaved AndAlso transportSql.FunGrnlEntryWithTrans(obj.Bill_To_Location, False, Old_Voucher_No, trans, obj.Document_Date, GLDesc, "CS-RC", "CSA Transfer Return", obj.Document_Code, "", "C", obj.Customer_Code, obj.Customer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , Remarks, "")
            Else
                isSaved = isSaved AndAlso transportSql.FunGrnlEntryWithTrans(obj.Bill_To_Location, False, trans, obj.Document_Date, GLDesc, "CS-RC", "CSA Transfer Return", obj.Document_Code, "", "C", obj.Customer_Code, obj.Customer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , Remarks, "")
            End If


            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dt = Nothing
        End Try
    End Function

    Public Shared Function JournalEntryForGST_Common(ByVal obj As clsCSATransferReturnHead, ByVal ArryLstGLAC As ArrayList, ByVal strLocationSegment As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            qry = " select Doc.Document_Code as Document_No,TaxM1.Tax_Liability_Account as Tax1_GLAC,TaxM2.Tax_Liability_Account as Tax2_GLAC," & _
                     " TaxM3.Tax_Liability_Account as Tax3_GLAC,TaxM4.Tax_Liability_Account as Tax4_GLAC," & _
                     " TaxM5.Tax_Liability_Account as Tax5_GLAC,TaxM6.Tax_Liability_Account as Tax6_GLAC, " & _
                     " TaxM7.Tax_Liability_Account as Tax7_GLAC,TaxM8.Tax_Liability_Account as Tax8_GLAC, " & _
                     " TaxM9.Tax_Liability_Account as Tax9_GLAC,TaxM10.Tax_Liability_Account as Tax10_GLAC, " & _
                     " TaxM1.Tax_Net_Payable as Tax1_GLAC_Payable,TaxM2.Tax_Net_Payable as Tax2_GLAC_Payable, " & _
                     " TaxM3.Tax_Net_Payable as Tax3_GLAC_Payable,TaxM4.Tax_Net_Payable as Tax4_GLAC_Payable, " & _
                     " TaxM5.Tax_Net_Payable as Tax5_GLAC_Payable,TaxM6.Tax_Net_Payable as Tax6_GLAC_Payable, " & _
                     " TaxM7.Tax_Net_Payable as Tax7_GLAC_Payable,TaxM8.Tax_Net_Payable as Tax8_GLAC_Payable, " & _
                     " TaxM9.Tax_Net_Payable as Tax9_GLAC_Payable,TaxM10.Tax_Net_Payable as Tax10_GLAC_Payable from TSPL_SD_SALE_RETURN_head doc " & _
                     " left join TSPL_TAX_MASTER TaxM1 on Doc.TAX1=TaxM1.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM2 on Doc.TAX2=TaxM2.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM3 on Doc.TAX3=TaxM3.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM4 on Doc.TAX4=TaxM4.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM5 on Doc.TAX5=TaxM5.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM6 on Doc.TAX6=TaxM6.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM7 on Doc.TAX7=TaxM7.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM8 on Doc.TAX8=TaxM8.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM9 on Doc.TAX9=TaxM9.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM10 on Doc.TAX10=TaxM10.Tax_Code where Doc.Document_Code='" & obj.Document_Code & "'"
            Dim dtTax As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtTax.Rows.Count = 0 Then
                Throw New Exception("Tax details of transfer document not found.")
            End If
            Dim TAX1_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax1_GLAC"))
            Dim TAX2_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax2_GLAC"))
            Dim TAX3_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax3_GLAC"))
            Dim TAX4_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax4_GLAC"))
            Dim TAX5_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax5_GLAC"))
            Dim TAX6_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax6_GLAC"))
            Dim TAX7_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax7_GLAC"))
            Dim TAX8_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax8_GLAC"))
            Dim TAX9_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax9_GLAC"))
            Dim TAX10_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax10_GLAC"))

           

            '' validation for gl
            If obj.TAX1_Amt > 0 Then
                If clsCommon.myLen(TAX1_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX1)
                End If
            End If
            If obj.TAX2_Amt > 0 Then
                If clsCommon.myLen(TAX2_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX2)
                End If
            End If
            If obj.TAX3_Amt > 0 Then
                If clsCommon.myLen(TAX3_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX3)
                End If
            End If
            If obj.TAX4_Amt > 0 Then
                If clsCommon.myLen(TAX4_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX4)
                End If
            End If
            If obj.TAX5_Amt > 0 Then
                If clsCommon.myLen(TAX5_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX5)
                End If
            End If
            If obj.TAX6_Amt > 0 Then
                If clsCommon.myLen(TAX6_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX6)
                End If
            End If

            If obj.TAX7_Amt > 0 Then
                If clsCommon.myLen(TAX7_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX7)
                End If
            End If

            If obj.TAX8_Amt > 0 Then
                If clsCommon.myLen(TAX8_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX8)
                End If
            End If

            If obj.TAX9_Amt > 0 Then
                If clsCommon.myLen(TAX9_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX9)
                End If
            End If


            If obj.TAX10_Amt > 0 Then
                If clsCommon.myLen(TAX10_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX10)
                End If
            End If

            If obj.TAX1_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX1_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, obj.TAX1_Amt}
                ArryLstGLAC.Add(accDR)
            End If
            If obj.TAX2_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX2_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, obj.TAX2_Amt}
                ArryLstGLAC.Add(accDR)
            End If
            If obj.TAX3_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX3_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, obj.TAX3_Amt}
                ArryLstGLAC.Add(accDR)
            End If
            If obj.TAX4_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX4_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, obj.TAX4_Amt}
                ArryLstGLAC.Add(accDR)
            End If
            If obj.TAX5_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX5_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, obj.TAX5_Amt}
                ArryLstGLAC.Add(accDR)

            End If
            If obj.TAX6_Amt > 0 Then
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX6_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, obj.TAX6_Amt}
                ArryLstGLAC.Add(accDR)
            End If

            If obj.TAX7_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX7_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, obj.TAX7_Amt}
                ArryLstGLAC.Add(accDR)
            End If

            If obj.TAX8_Amt > 0 Then
                '' credi
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX8_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, obj.TAX8_Amt}
                ArryLstGLAC.Add(accDR)
            End If

            If obj.TAX9_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX9_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, obj.TAX9_Amt}
                ArryLstGLAC.Add(accDR)
            End If

            If obj.TAX10_Amt > 0 Then
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX10_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, obj.TAX10_Amt}
                ArryLstGLAC.Add(accDR)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal Navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsCSATransferReturnHead
        Dim dt As New DataTable()
        Dim dt1 As New DataTable()
        Dim objtr As New clsCSATransferReturnDetail()

        Try
            Dim loc_code As String = ""
            If arrLoc IsNot Nothing AndAlso clsCommon.myLen(arrLoc) > 0 Then
                loc_code = " and tspl_sd_sale_return_head.bill_to_location in (" + arrLoc + ") "
            End If
            Dim obj As New clsCSATransferReturnHead()
            obj.Arr = New List(Of clsCSATransferReturnDetail)
            Dim qry As String = "select tspl_sd_sale_return_head.*,tspl_location_master.location_desc,tspl_customer_master.customer_name from tspl_sd_sale_return_head left outer join tspl_location_master on tspl_location_master.location_code=tspl_sd_sale_return_head.bill_to_location left outer join tspl_customer_master on tspl_customer_master.cust_code=tspl_sd_sale_return_head.customer_code " & _
            " where tspl_sd_sale_return_head.trans_type='CSA' " + loc_code + " "


            Select Case Navtype
                Case NavigatorType.Current
                    qry += " and tspl_sd_sale_return_head.document_code='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and tspl_sd_sale_return_head.document_code in (Select min(document_code) from tspl_sd_sale_return_head where tspl_sd_sale_return_head.trans_type='CSA' " + loc_code + ")"
                Case NavigatorType.Last
                    qry += " and tspl_sd_sale_return_head.document_code in (Select max(document_code) from tspl_sd_sale_return_head where tspl_sd_sale_return_head.trans_type='CSA' " + loc_code + ")"
                Case NavigatorType.Next
                    qry += " and tspl_sd_sale_return_head.document_code in (Select min(document_code) from tspl_sd_sale_return_head where tspl_sd_sale_return_head.trans_type='CSA' " + loc_code + " and document_code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and tspl_sd_sale_return_head.document_code in (Select max(document_code) from tspl_sd_sale_return_head where tspl_sd_sale_return_head.trans_type='CSA' " + loc_code + " and document_code<'" + strCode + "')"
            End Select
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
                obj.Gate_ReturnNo = clsCommon.myCstr(dt.Rows(0)("Gate_ReturnNo"))
                obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("document_code"))
                obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("document_date"))
                obj.Description = clsCommon.myCstr(dt.Rows(0)("description"))
                obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("customer_code"))
                obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("customer_name"))
                obj.Bill_To_Location = clsCommon.myCstr(dt.Rows(0)("bill_to_location"))
                obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("location_desc"))
                obj.CSA_Loc_Code = clsCommon.myCstr(dt.Rows(0)("csa_loc_code"))
                obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
                obj.Status = CInt(clsCommon.myCdbl(dt.Rows(0)("Status")))
                obj.Return_Type = clsCommon.myCstr(dt.Rows(0)("Return_Type"))

                If (dt.Rows(0)("Posting_Date")) Is DBNull.Value Then
                    obj.Posting_Date = Nothing
                Else
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If
                obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
                obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
                obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
                obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Base_Amt"))
                obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
                obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
                obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
                obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Base_Amt"))
                obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
                obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
                obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Base_Amt"))
                obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
                obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
                obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
                obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
                obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Base_Amt"))
                obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
                obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
                obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
                obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Base_Amt"))
                obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
                obj.TAX6 = clsCommon.myCstr(dt.Rows(0)("TAX6"))
                obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX6_Rate"))
                obj.TAX6_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Base_Amt"))
                obj.TAX6_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Amt"))
                obj.TAX7 = clsCommon.myCstr(dt.Rows(0)("TAX7"))
                obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX7_Rate"))
                obj.TAX7_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Base_Amt"))
                obj.TAX7_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Amt"))
                obj.TAX8 = clsCommon.myCstr(dt.Rows(0)("TAX8"))
                obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX8_Rate"))
                obj.TAX8_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Base_Amt"))
                obj.TAX8_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Amt"))
                obj.TAX9 = clsCommon.myCstr(dt.Rows(0)("TAX9"))
                obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX9_Rate"))
                obj.TAX9_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Base_Amt"))
                obj.TAX9_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Amt"))
                obj.TAX10 = clsCommon.myCstr(dt.Rows(0)("TAX10"))
                obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX10_Rate"))
                obj.TAX10_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Base_Amt"))
                obj.TAX10_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Amt"))

                obj.Amount_Less_Discount = clsCommon.myCdbl(dt.Rows(0)("Amount_Less_Discount"))
                obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))

                qry = "select tspl_sd_sale_return_detail.*,tspl_item_master.item_desc,tspl_item_master.csa_type,tspl_csa_transfer_head.description as transfer_desc from tspl_sd_sale_return_detail left outer join tspl_item_master on tspl_item_master.item_code=tspl_sd_sale_return_detail.item_code left outer join tspl_csa_transfer_head on tspl_csa_transfer_head.doc_code=tspl_sd_sale_return_detail.transfer_no where tspl_sd_sale_return_detail.document_code='" + obj.Document_Code + "'"
                dt1 = New DataTable()
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        objtr = New clsCSATransferReturnDetail()
                        objtr.arrBatchItem = New List(Of clsBatchInventory)

                        objtr.Document_Code = clsCommon.myCstr(dr("document_code"))
                        objtr.FOC_Item = CInt(clsCommon.myCdbl(dr("foc_item")))
                        objtr.Line_No = CInt(clsCommon.myCdbl(dr("line_no")))
                        objtr.Item_Code = clsCommon.myCstr(dr("item_code"))
                        objtr.Item_Desc = clsCommon.myCstr(dr("item_desc"))
                        objtr.Item_Cost = clsCommon.myCdbl(dr("item_cost"))
                        objtr.Unit_code = clsCommon.myCstr(dr("unit_code"))
                        objtr.CSA_Type = clsCommon.myCstr(dr("csa_type"))
                        objtr.Qty = clsCommon.myCdbl(dr("qty"))
                        objtr.Remarks = clsCommon.myCstr(dr("remarks"))
                        objtr.Row_Type = clsCommon.myCstr(dr("row_type"))
                        objtr.Transfer_Desc = clsCommon.myCstr(dr("transfer_desc"))
                        objtr.Transfer_No = clsCommon.myCstr(dr("transfer_no"))
                        objtr.Org_Transfer_UOM = clsCommon.myCstr(dr("Org_Transfer_UOM"))
                        objtr.Org_Transfer_Qty = clsCommon.myCdbl(dr("Org_Transfer_Qty"))
                        objtr.Amount = clsCommon.myCdbl(dr("amount"))
                        objtr.Alt_Transfer_Qty = clsCommon.myCdbl(dr("Alt_Transfer_Qty"))
                        objtr.Adjustment_No = clsCommon.myCstr(dr("adjustment_no"))
                        objtr.CSA_SalePatti_Return_No = clsCommon.myCstr(dr("CSA_SalePatti_Return_No"))


                        objtr.TAX1 = clsCommon.myCstr(dr("TAX1"))
                        objtr.TAX1_Base_Amt = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                        objtr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                        objtr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                        objtr.TAX2 = clsCommon.myCstr(dr("TAX2"))
                        objtr.TAX2_Base_Amt = clsCommon.myCdbl(dr("TAX2_Base_Amt"))
                        objtr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                        objtr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
                        objtr.TAX3 = clsCommon.myCstr(dr("TAX3"))
                        objtr.TAX3_Base_Amt = clsCommon.myCdbl(dr("TAX3_Base_Amt"))
                        objtr.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                        objtr.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
                        objtr.TAX4 = clsCommon.myCstr(dr("TAX4"))
                        objtr.TAX4_Base_Amt = clsCommon.myCdbl(dr("TAX4_Base_Amt"))
                        objtr.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                        objtr.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
                        objtr.TAX5 = clsCommon.myCstr(dr("TAX5"))
                        objtr.TAX5_Base_Amt = clsCommon.myCdbl(dr("TAX5_Base_Amt"))
                        objtr.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                        objtr.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
                        objtr.TAX6 = clsCommon.myCstr(dr("TAX6"))
                        objtr.TAX6_Base_Amt = clsCommon.myCdbl(dr("TAX6_Base_Amt"))
                        objtr.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                        objtr.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
                        objtr.TAX7 = clsCommon.myCstr(dr("TAX7"))
                        objtr.TAX7_Base_Amt = clsCommon.myCdbl(dr("TAX7_Base_Amt"))
                        objtr.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                        objtr.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
                        objtr.TAX8 = clsCommon.myCstr(dr("TAX8"))
                        objtr.TAX8_Base_Amt = clsCommon.myCdbl(dr("TAX8_Base_Amt"))
                        objtr.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                        objtr.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
                        objtr.TAX9 = clsCommon.myCstr(dr("TAX9"))
                        objtr.TAX9_Base_Amt = clsCommon.myCdbl(dr("TAX9_Base_Amt"))
                        objtr.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
                        objtr.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
                        objtr.TAX10 = clsCommon.myCstr(dr("TAX10"))
                        objtr.TAX10_Base_Amt = clsCommon.myCdbl(dr("TAX10_Base_Amt"))
                        objtr.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
                        objtr.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))
                        objtr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                        objtr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))

                        objtr.arrBatchItem = clsBatchInventory.GetData("SD-CSATRANS-RETURN", obj.Document_Code, objtr.Item_Code, objtr.Line_No, trans)

                        obj.Arr.Add(objtr)
                    Next
                End If ''dt1 cond.

            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dt = Nothing
            dt1 = Nothing
            objtr = Nothing
        End Try
    End Function

#Region "Reverse and Unpost" ''BM00000009170
    Public Shared Function UnPostData(ByVal FormID As String, ByVal strCode As String, ByVal arrLoc As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso UnPostData(FormID, strCode, arrLoc, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function UnPostData(ByVal FormID As String, ByVal strCode As String, ByVal arrLoc As String, ByVal trans As SqlTransaction) As Boolean
        Dim obj As New clsCSATransferReturnHead()
        Dim qry As String = ""
        Try
            Dim isSaved As Boolean = True

            obj = clsCSATransferReturnHead.GetData(strCode, Nothing, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleCSASale, clsUserMgtCode.frmCSATransferReturn, obj.Bill_To_Location, obj.Document_Date, trans)


            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Unpost")
            End If
            If (obj.Status <> 1) Then
                Throw New Exception("Document already unposted")
            End If

            ''delete from inventory
            qry = "update tspl_batch_item set against_inv_movement_trans_id=NULL where document_type='SD-CSATRANS-RETURN' and document_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from tspl_inventory_movement where trans_type='SD-CSATRANS-RETURN' and Source_Doc_No='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''-------end here====

            ''-----------unpost GL--------------
            qry = "update TSPL_JOURNAL_MASTER set Authorized='N',Modify_By='" + objCommonVar.CurrentUserCode + "' ,Modify_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "' " & _
                " where source_code='CS-RC' and source_doc_no ='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''-------------------------------------------------

            qry = "Update TSPL_SD_SALE_RETURN_HEAD set Status=0,Modify_By='" + objCommonVar.CurrentUserCode + "', Modify_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "' where DOCument_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Function
#End Region
End Class

Public Class clsCSATransferReturnDetail
#Region "variables"
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
    Public Document_Code As String = Nothing
    Public Line_No As Integer = Nothing
    Public Row_Type As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public CSA_Type As String = Nothing
    Public Qty As Decimal = Nothing
    Public Unit_code As String = Nothing
    Public Item_Cost As Decimal = Nothing
    Public Remarks As String = Nothing
    Public Total_Basic_Amt As Decimal = Nothing
    Public Amount As Decimal = Nothing
    Public Transfer_No As String = Nothing
    Public Transfer_Desc As String = Nothing
    Public Org_Transfer_Qty As Decimal = Nothing
    Public Org_Transfer_UOM As String = Nothing
    Public Alt_Transfer_Qty As Decimal = Nothing
    Public FOC_Item As Integer = Nothing
    Public Adjustment_No As String = Nothing
    Public CSA_SalePatti_Return_No As String = Nothing


    Public TAX1 As String = Nothing
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Rate As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Rate As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Rate As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Rate As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Rate As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Base_Amt As Double = 0
    Public TAX6_Rate As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Base_Amt As Double = 0
    Public TAX7_Rate As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Base_Amt As Double = 0
    Public TAX8_Rate As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Base_Amt As Double = 0
    Public TAX9_Rate As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Base_Amt As Double = 0
    Public TAX10_Rate As Double = 0
    Public TAX10_Amt As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal strDate As DateTime, ByVal bill_to_location As String, ByVal strCSALocation As String, ByVal Arr As List(Of clsCSATransferReturnDetail), ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = "delete from TSPL_SD_SALE_RETURN_detail where document_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each objtr As clsCSATransferReturnDetail In Arr
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Line_No", objtr.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Location", bill_to_location)
                    clsCommon.AddColumnsForChange(coll, "Invoice_Code", "", True)
                    clsCommon.AddColumnsForChange(coll, "FOC_Item", objtr.FOC_Item)
                    clsCommon.AddColumnsForChange(coll, "Row_Type", "Item")
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Qty", objtr.Qty)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", objtr.Unit_code)
                    clsCommon.AddColumnsForChange(coll, "Item_Cost", objtr.Item_Cost)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                    clsCommon.AddColumnsForChange(coll, "Total_Basic_Amt", objtr.Total_Basic_Amt)
                    clsCommon.AddColumnsForChange(coll, "Amount", objtr.Amount)
                    clsCommon.AddColumnsForChange(coll, "Transfer_No", objtr.Transfer_No)
                    clsCommon.AddColumnsForChange(coll, "Org_Transfer_Qty", objtr.Org_Transfer_Qty)
                    clsCommon.AddColumnsForChange(coll, "Org_Transfer_UOM", objtr.Org_Transfer_UOM)
                    clsCommon.AddColumnsForChange(coll, "Alt_Transfer_Qty", objtr.Alt_Transfer_Qty)
                    clsCommon.AddColumnsForChange(coll, "Adjustment_No", objtr.Adjustment_No)
                    clsCommon.AddColumnsForChange(coll, "CSA_SalePatti_Return_No", objtr.CSA_SalePatti_Return_No)

                    clsCommon.AddColumnsForChange(coll, "TAX1", objtr.TAX1)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", objtr.TAX1_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Rate", objtr.TAX1_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Amt", objtr.TAX1_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX2", objtr.TAX2)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", objtr.TAX2_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Rate", objtr.TAX2_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Amt", objtr.TAX2_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3", objtr.TAX3)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", objtr.TAX3_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Rate", objtr.TAX3_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Amt", objtr.TAX3_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4", objtr.TAX4)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", objtr.TAX4_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Rate", objtr.TAX4_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Amt", objtr.TAX4_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX5", objtr.TAX5)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", objtr.TAX5_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Rate", objtr.TAX5_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Amt", objtr.TAX5_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX6", objtr.TAX6)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", objtr.TAX6_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Rate", objtr.TAX6_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Amt", objtr.TAX6_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX7", objtr.TAX7)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", objtr.TAX7_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Rate", objtr.TAX7_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Amt", objtr.TAX7_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX8", objtr.TAX8)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", objtr.TAX8_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Rate", objtr.TAX8_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Amt", objtr.TAX8_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX9", objtr.TAX9)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", objtr.TAX9_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Rate", objtr.TAX9_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Amt", objtr.TAX9_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX10", objtr.TAX10)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", objtr.TAX10_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Rate", objtr.TAX10_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Amt", objtr.TAX10_Amt)
                    clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", objtr.Total_Tax_Amt)
                    clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", objtr.Item_Net_Amt)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALE_RETURN_detail", OMInsertOrUpdate.Insert, "", trans)

                    If objtr.arrBatchItem IsNot Nothing AndAlso objtr.arrBatchItem.Count > 0 Then
                        clsBatchInventory.SaveData("SD-CSATRANS-RETURN", strCode, strDate, "I", objtr.Item_Code, bill_to_location, objtr.Line_No, 0, objtr.Unit_code, objtr.arrBatchItem, trans)
                        clsBatchInventory.SaveData("SD-CSATRANS-RETURN", strCode, strDate, "O", objtr.Item_Code, strCSALocation, objtr.Line_No, 0, objtr.Unit_code, objtr.arrBatchItem, trans)
                    End If
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            coll = Nothing
        End Try
    End Function
End Class