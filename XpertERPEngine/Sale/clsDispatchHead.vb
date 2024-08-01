
Imports common
Imports System.Data.SqlClient
Public Class clsDispatchHead
#Region "Variables"
    Public RGP_No As String = Nothing
    Public RGP_Date As DateTime = Nothing
    Public Doc_Type As String = Nothing
    Public Against_Sale As Integer = 0
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public VehicleNo As String = Nothing
    Public GPNo As String = Nothing
    Public GPDate As Date? = Nothing
    Public Reason As String = Nothing
    Public Remarks As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public On_Hold As Boolean = Nothing
    Public Posting_Date As DateTime?
    Public Document_Amount As Double = 0
    Public Location As String = Nothing
    Public LocationName As String = Nothing
    Public Delivered_By As String = Nothing
    Public Department As String = Nothing
    Public Delivered_ByName As String = Nothing
    Public Arr As List(Of clsDispatchDetail) = Nothing
    Public TaxGrp As String = Nothing
    Public ItemType As String = Nothing
    Public Billing As String = Nothing
    Public Is_Non_Inventory As Integer = 0
    Public Mode_Of_Transport As String = Nothing
    Public Cash_Memo_Detail As String = Nothing
    Public CostCentre As String = Nothing
    Public CostCentreDesc As String = Nothing
    Public Against_Customer As Integer = 0

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsDispatchHead, ByVal isNewEntry As Boolean) As Boolean

        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            Dim obj1 As New clsDispatchHead()
            obj1 = clsDispatchHead.GetData(obj1.RGP_No)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleService, clsUserMgtCode.frmAssetDistatch, obj.Location, obj.RGP_Date, trans)

            ' clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "RGP/NRGP", obj.Location, obj.RGP_Date, trans)
            Dim qry As String = "delete from TSPL_RGP_DETAIL where RGP_No='" + obj.RGP_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                'If clsCommon.CompairString(obj.Doc_Type, "RGP") = CompairStringResult.Equal Then
                obj.RGP_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.RGP_Date), clsDocType.AssetDispatch, "", obj.Location)

                'ElseIf clsCommon.CompairString(obj.Doc_Type, "NRGP") = CompairStringResult.Equal Then
                'obj.RGP_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.RGP_Date), clsDocType.NRGP, "", obj.Location)
                'Else
                'Throw New Exception("Document Type is not correct")
                'End If
            End If
            If (clsCommon.myLen(obj.RGP_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "RGP_Date", clsCommon.GetPrintDate(obj.RGP_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Reason", obj.Reason)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "VehicleNo", obj.VehicleNo)
            clsCommon.AddColumnsForChange(coll, "GPNo", obj.GPNo)
            clsCommon.AddColumnsForChange(coll, "Doc_Type", obj.Doc_Type)
            clsCommon.AddColumnsForChange(coll, "Against_Sale", obj.Against_Sale)
            clsCommon.AddColumnsForChange(coll, "Document_Amount", obj.Document_Amount)
            clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
            clsCommon.AddColumnsForChange(coll, "Delivered_By", obj.Delivered_By)
            clsCommon.AddColumnsForChange(coll, "Department", obj.Department)
            clsCommon.AddColumnsForChange(coll, "Billing", obj.Billing)
            clsCommon.AddColumnsForChange(coll, "Is_Non_Inventory", obj.Is_Non_Inventory)
            clsCommon.AddColumnsForChange(coll, "Mode_Of_Transport", obj.Mode_Of_Transport)
            clsCommon.AddColumnsForChange(coll, "Cash_Memo_Detail", obj.Cash_Memo_Detail)
            clsCommon.AddColumnsForChange(coll, "CostCentre", obj.CostCentre)
            clsCommon.AddColumnsForChange(coll, "CostCentreDesc", obj.CostCentreDesc)
            clsCommon.AddColumnsForChange(coll, "Against_Customer", obj.Against_Customer)
            If obj.GPDate.HasValue Then
                clsCommon.AddColumnsForChange(coll, "GPDate", clsCommon.GetPrintDate(obj.GPDate, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "GPDate", Nothing, True)
            End If
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "RGP_No", obj.RGP_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RGP_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RGP_HEAD", OMInsertOrUpdate.Update, "TSPL_RGP_HEAD.RGP_No='" + obj.RGP_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsDispatchDetail.SaveData(obj.RGP_No, Arr, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.RGP_No, obj.arrCustomFields, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String) As clsDispatchHead
        Return GetData(strDocumentNo, Nothing)
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsDispatchHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDispatchHead
        Dim obj As clsDispatchHead = Nothing
        Dim qry As String = "SELECT TSPL_RGP_HEAD.RGP_No,TSPL_RGP_HEAD.Billing,TSPL_RGP_HEAD.RGP_Date,TSPL_RGP_HEAD.Vendor_Code,TSPL_RGP_HEAD.Vendor_Name,TSPL_RGP_HEAD.Status,TSPL_RGP_HEAD.On_Hold,TSPL_RGP_HEAD.Reason,TSPL_RGP_HEAD.Remarks,TSPL_RGP_HEAD.Reason,TSPL_RGP_HEAD.Posting_Date,TSPL_RGP_HEAD.VehicleNo,TSPL_RGP_HEAD.GPNo,TSPL_RGP_HEAD.GPDate,TSPL_RGP_HEAD.Doc_Type,TSPL_RGP_HEAD.Document_Amount,TSPL_RGP_HEAD.Location,TSPL_RGP_HEAD.Mode_Of_Transport,TSPL_RGP_HEAD.Cash_Memo_Detail,TSPL_LOCATION_MASTER.Location_Desc as LocationName,TSPL_RGP_HEAD.Delivered_By,TSPL_EMPLOYEE_MASTER.Emp_Name as DeliveredBYName,TSPL_RGP_HEAD.Department,TSPL_RGP_HEAD.Against_Sale, TSPL_RGP_HEAD.Is_Non_Inventory,CostCentre,CostCentreDesc,Against_Customer FROM TSPL_RGP_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_RGP_HEAD.Location left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_RGP_HEAD.Delivered_By where 2=2"
        Dim whrCls As String = " and Doc_type='Disp' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_RGP_HEAD.RGP_No = (select MIN(RGP_No) from TSPL_RGP_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_RGP_HEAD.RGP_No = (select Max(RGP_No) from TSPL_RGP_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_RGP_HEAD.RGP_No = '" + strPONo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_RGP_HEAD.RGP_No = (select Min(RGP_No) from TSPL_RGP_HEAD where RGP_No>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_RGP_HEAD.RGP_No = (select Max(RGP_No) from TSPL_RGP_HEAD where RGP_No<'" + strPONo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDispatchHead()
            obj.RGP_No = clsCommon.myCstr(dt.Rows(0)("RGP_No"))
            obj.RGP_Date = clsCommon.myCstr(dt.Rows(0)("RGP_Date"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Reason = clsCommon.myCstr(dt.Rows(0)("Reason"))
            obj.Against_Sale = clsCommon.myCdbl(dt.Rows(0)("Against_Sale"))
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
            End If
            obj.Mode_Of_Transport = clsCommon.myCstr(dt.Rows(0)("Mode_Of_Transport"))
            obj.Cash_Memo_Detail = clsCommon.myCstr(dt.Rows(0)("Cash_Memo_Detail"))
            obj.VehicleNo = clsCommon.myCstr(dt.Rows(0)("VehicleNo"))
            obj.GPNo = clsCommon.myCstr(dt.Rows(0)("GPNo"))
            obj.Location = clsCommon.myCstr(dt.Rows(0)("Location"))
            obj.LocationName = clsCommon.myCstr(dt.Rows(0)("LocationName"))
            obj.Delivered_By = clsCommon.myCstr(dt.Rows(0)("Delivered_By"))
            obj.Delivered_ByName = clsCommon.myCstr(dt.Rows(0)("DeliveredBYName"))
            If dt.Rows(0)("GPDate") IsNot DBNull.Value Then
                obj.GPDate = clsCommon.myCDate(dt.Rows(0)("GPDate"))
            End If
            obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
            obj.Billing = clsCommon.myCstr(dt.Rows(0)("Billing"))
            obj.Document_Amount = clsCommon.myCstr(dt.Rows(0)("Document_Amount"))
            obj.Department = clsCommon.myCstr(dt.Rows(0)("Department"))
            obj.Is_Non_Inventory = clsCommon.myCdbl(dt.Rows(0)("Is_Non_Inventory"))
            obj.CostCentre = clsCommon.myCstr(dt.Rows(0)("CostCentre"))
            obj.CostCentreDesc = clsCommon.myCstr(dt.Rows(0)("CostCentreDesc"))
            obj.Against_Customer = clsCommon.myCdbl(dt.Rows(0)("Against_Customer"))

            qry = "SELECT TSPL_RGP_DETAIL.RGP_No,TSPL_RGP_DETAIL.Line_No,TSPL_RGP_DETAIL.Item_Code,TSPL_RGP_DETAIL.Item_Desc,TSPL_RGP_DETAIL.RGP_Qty,TSPL_RGP_DETAIL.Unit_code,TSPL_RGP_DETAIL.Item_Cost,TSPL_RGP_DETAIL.Amount, TSPL_RGP_DETAIL.Last_RGP_No, TSPL_RGP_DETAIL.Last_RGP_Date, TSPL_RGP_DETAIL.Specification, tspl_rgp_detail.security_amount,tspl_rgp_detail.Cheque_No,tspl_rgp_detail.Cheque_Date,tspl_rgp_detail.FOC,TSPL_RGP_DETAIL.serial_no,TSPL_RGP_DETAIL.agreement_no FROM TSPL_RGP_DETAIL where TSPL_RGP_DETAIL.RGP_No='" + obj.RGP_No + "' ORDER BY TSPL_RGP_DETAIL.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsDispatchDetail)
                Dim objTr As clsDispatchDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsDispatchDetail
                    objTr.RGP_No = clsCommon.myCstr(dr("RGP_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.RGP_Qty = clsCommon.myCdbl(dr("RGP_Qty"))
                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objTr.Item_Cost = clsCommon.myCdbl(dr("Item_Cost"))
                    objTr.Last_RGP_No = clsCommon.myCstr(dr("Last_RGP_No"))
                    objTr.Last_RGP_Date = clsCommon.myCstr(dr("Last_RGP_Date"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Specification = clsCommon.myCstr(dr("Specification"))
                    objTr.security_amount = clsCommon.myCdbl(dr("security_amount"))
                    objTr.chequeNo = clsCommon.myCstr(dr("Cheque_No"))
                    objTr.chequeDate = clsCommon.myCstr(dr("Cheque_Date"))
                    objTr.SL_NO = clsCommon.myCstr(dr("serial_no"))
                    objTr.AGREEMENT_NO = clsCommon.myCstr(dr("agreement_no"))
                    objTr.FOC = clsCommon.myCstr(dr("FOC"))

                    obj.Arr.Add(objTr)
                Next
            End If
        End If

        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("SRN No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim obj As clsDispatchHead = clsDispatchHead.GetData(strDocNo, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleService, clsUserMgtCode.frmAssetDistatch, obj.Location, obj.RGP_Date, trans)


            If (obj Is Nothing OrElse clsCommon.myLen(obj.RGP_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "RGP/NRGP", obj.Location, obj.RGP_Date, trans)
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("Document No " + obj.RGP_No + " Is On Hold.Can't Post it")
            End If
            Dim qry As String = ""
            Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            For Each objTr As clsDispatchDetail In obj.Arr

                Dim strItemType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Type from TSPL_ITEM_MASTER where Item_Code='" + objTr.Item_Code + "'", trans))
                Dim strItemTypeToSave As String = ""
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                    strItemTypeToSave = "A"
                End If




                Dim objLocationDetails As New clsItemLocationDetails()
                objLocationDetails.Item_Code = objTr.Item_Code
                objLocationDetails.Item_Desc = objTr.Item_Desc
                objLocationDetails.Location_Code = obj.Location
                objLocationDetails.Location_Desc = obj.LocationName
                objLocationDetails.Item_Qty = -1 * objTr.RGP_Qty
                objLocationDetails.Amount = -1 * objTr.Amount
                objLocationDetails.MRP = 0
                ''If objTr.MFG_Date.HasValue Then
                ''    objLocationDetails.MFG_Date = objTr.MFG_Date 'Null
                ''End If
                objLocationDetails.Batch_No = "" ''Batch is Blank
                ''If objTr.Expiry_Date.HasValue Then
                ''    objLocationDetails.Expiry_Date = objTr.Expiry_Date 'Null 
                ''End If
                objLocationDetails.ItemType = strItemTypeToSave

                ArrLocationDetails.Add(objLocationDetails)


                Dim objInventoryMovemnt As New clsInventoryMovement()
                objInventoryMovemnt.InOut = "O"
                objInventoryMovemnt.Location_Code = obj.Location
                objInventoryMovemnt.Vendor_Code = obj.Vendor_Code
                objInventoryMovemnt.Vendor_Name = obj.Vendor_Name
                objInventoryMovemnt.Item_Code = objTr.Item_Code
                objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                objInventoryMovemnt.Qty = objTr.RGP_Qty
                objInventoryMovemnt.UOM = objTr.Unit_code
                objInventoryMovemnt.Basic_Cost = objTr.Item_Cost
                ''objInventoryMovemnt.Rec_Cost= objTr.MRP
                ''objInventoryMovemnt.Add_Cost = 'objTr.Total_Tax_Amt
                objInventoryMovemnt.Net_Cost = objTr.Amount
                objInventoryMovemnt.ItemType = strItemTypeToSave
                ArrInventoryMovement.Add(objInventoryMovemnt)
            Next

            If obj.Is_Non_Inventory = 0 Then
                isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(strPostDate, "dd/MM/yyyy"), ArrLocationDetails, trans)
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData(obj.Doc_Type, obj.RGP_No, obj.RGP_Date, clsCommon.GetPrintDate(strPostDate, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            End If

            qry = "Update TSPL_RGP_HEAD set Status=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where RGP_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Purchase Order No not found to Delete")
        End If
        Dim obj As clsDispatchHead = clsDispatchHead.GetData(strCode, NavigatorType.Current)
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleService, clsUserMgtCode.frmAssetDistatch, obj.Location, obj.RGP_Date, Nothing)

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.RGP_No) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "RGP/NRGP", obj.Location, obj.RGP_Date, trans)
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                Dim qry As String = "delete from TSPL_RGP_DETAIL where RGP_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_RGP_HEAD where RGP_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, trans)
                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function


    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "select Status from TSPL_RGP_HEAD where RGP_No='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "select invoice_No as DocNo,'Scrap Invoice' as DocType  from TSPL_SCRAPINVOICE_HEAD where NRG_No='" + strCode + "'"
            Qry += " Union all "
            Qry += " select distinct SRN_No as DocNo,'SRN' as DocType from TSPL_SRN_DETAIL where RGP_Id='" + strCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current RGP/NRGP is used in following Transactions -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("DocType")) + "-" + clsCommon.myCstr(dr("DocNo"))
                Next
                Throw New Exception(Qry)
            End If

            Qry = "select InOut,Trans_Type,Item_Code,Item_Desc,Location_Code,case when InOut='I' then -1 else 1 end *Qty as Qty ,UOM,MRP,ItemType,case when InOut='I' then -1 else 1 end* Basic_Cost as Basic_Cost from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strCode + "' and Trans_Type in ('NRGP','RGP')"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)
                For Each objtr As DataRow In dt.Rows
                    Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(objtr("Item_Code")), clsCommon.myCstr(objtr("UOM")), trans)
                    Dim objLocationDetails As New clsItemLocationDetails()
                    objLocationDetails.Item_Code = clsCommon.myCstr(objtr("Item_Code"))
                    objLocationDetails.Item_Desc = clsCommon.myCstr(objtr("Item_Desc"))
                    objLocationDetails.Location_Code = clsCommon.myCstr(objtr("Location_Code"))
                    objLocationDetails.Location_Desc = clsLocation.GetName(objLocationDetails.Location_Code, trans)
                    objLocationDetails.Item_Qty = clsCommon.myCdbl(objtr("Qty")) / dblConvFac
                    objLocationDetails.Amount = clsCommon.myCdbl(objtr("Basic_Cost"))
                    objLocationDetails.MRP = clsCommon.myCdbl(objtr("MRP")) * dblConvFac
                    objLocationDetails.ItemType = clsCommon.myCstr(objtr("ItemType"))
                    ArrLocationDetails.Add(objLocationDetails)
                Next
                Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                clsItemLocationDetails.SaveData(strPostDate, ArrLocationDetails, trans)
            End If

            Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strCode + "' and Trans_Type in ('NRGP','RGP')"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_RGP_HEAD set Status = 0,Posting_Date=null where RGP_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsDispatchDetail
#Region "Variables"
    Public SL_NO As String = Nothing
    Public RGP_No As String = Nothing
    Public AGREEMENT_NO As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public RGP_Qty As Double = 0
    Public Unit_code As String = Nothing
    Public Item_Cost As Double = 0
    Public Amount As Double = 0
    Public TaxGrp As String = Nothing
    Public ItemType As String = Nothing
    Public Last_RGP_No As String = Nothing
    Public Last_RGP_Date As String = Nothing
    Public Specification As String = Nothing
    Public security_amount As Double = 0
    Public FOC As String = ""
    Public chequeNo As String = ""
    Public chequeDate As String = ""
    'Public assetType As String = ""
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsDispatchDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsDispatchDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "RGP_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "RGP_Qty", obj.RGP_Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "serial_no", clsCommon.myCstr(obj.SL_NO))
                clsCommon.AddColumnsForChange(coll, "agreement_no", clsCommon.myCstr(obj.AGREEMENT_NO))

                'clsCommon.AddColumnsForChange(coll, "Asset_Type", obj.assetType)
                'clsCommon.AddColumnsForChange(coll, "Last_RGP_No", obj.Last_RGP_No)
                'If clsCommon.myLen(obj.Last_RGP_Date) > 0 Then
                'clsCommon.AddColumnsForChange(coll, "Last_RGP_Date", clsCommon.GetPrintDate(obj.Last_RGP_Date, "dd/MM/yyyy"))
                'Else
                'clsCommon.AddColumnsForChange(coll, "Last_RGP_Date", Nothing)
                'End If
                clsCommon.AddColumnsForChange(coll, "Security_Amount", obj.security_amount)
                If obj.security_amount > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Cheque_No", obj.chequeNo)
                    clsCommon.AddColumnsForChange(coll, "Cheque_Date", clsCommon.GetPrintDate(clsCommon.myCDate(obj.chequeDate), "dd/MMM/yyyy"))
                End If
                'clsCommon.AddColumnsForChange(coll, "Security_Amount", obj.security_amount)
                clsCommon.AddColumnsForChange(coll, "FOC", obj.FOC)
                clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RGP_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetBalanceRGPQty(ByVal strRGPCode As String, ByVal strICode As String, ByVal strCurrSRNNo As String) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " & _
            " select TSPL_RGP_DETAIL.Item_Code as ICode,TSPL_RGP_DETAIL.RGP_Qty as Qty,1 as RI from TSPL_RGP_DETAIL left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No where TSPL_RGP_HEAD.Status=1 and TSPL_RGP_DETAIL.RGP_No ='" + strRGPCode + "' and TSPL_RGP_DETAIL.Item_Code='" + strICode + "' " & _
            " union all " & _
            " select TSPL_SRN_DETAIL.Item_Code as ICode,(TSPL_SRN_DETAIL.SRN_Qty+ISNULL(Rejected_Qty,0)) as Qty,-1 as RI from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_DETAIL.RGP_Id='" + strRGPCode + "'   and TSPL_SRN_DETAIL.Item_Code='" + strICode + "' and TSPL_SRN_DETAIL.SRN_No not in ('" + strCurrSRNNo + "')  " & _
            " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function isFocON() As Boolean
        Try
            Dim i As Integer = clsDBFuncationality.getSingleValue("select required_FOC from tspl_purchase_settings")
            If i = 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Shared Function isSecurityAmountON() As Boolean
        Try
            Dim i As Integer = clsDBFuncationality.getSingleValue("select required_Security_Amount from tspl_purchase_settings")
            If i = 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Sub New()

    End Sub
End Class



