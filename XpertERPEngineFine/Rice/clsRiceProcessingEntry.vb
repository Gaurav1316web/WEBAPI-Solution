Imports common
Imports System.Data.SqlClient

Public Class clsRiceProcessingEntry
#Region "variables"
    Public doccode As String = Nothing
    Public Docdate As Date = Nothing
    Public descrptn As String = Nothing
    Public frm_loc_code As String = Nothing
    Public frm_loc_desc As String = Nothing
    Public to_loc_code As String = Nothing
    Public to_loc_desc As String = Nothing
    Public admin_charge As Decimal = Nothing
    Public admin_cost As Decimal = Nothing
    Public process_Charge As Decimal = Nothing
    Public process_cost As Decimal = Nothing
    Public total_cost As Decimal = Nothing
    Public effective_cost As Decimal = Nothing
    Public is_post As Integer = Nothing

    Public Arr As List(Of clsRiceProcessingDetail) = Nothing
    Public Arr_FInish As List(Of clsRiceProcessingFinishDetail) = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal CurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select doc_no as Code,doc_date as [Date],Description,from_location_code as [From Location],to_location_code as [To Location],process_charge as [Process Charge],process_cost as [Process Cost],admin_charge as [Admin Charge],admin_cost as [Admin Cost],total_cost as [Total Cost],effective_cost as [Effective Cost],(Case when status=1 then 'Approved' else 'Pending' end) as Status from TSPL_RICE_PROCESSING_HEAD"
        str = clsCommon.ShowSelectForm("PRCFND", qry, "Code", whrCls, CurrCode, "Code", isButtonClicked)

        Return str
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsRiceProcessingEntry
        Try
            Dim obj As New clsRiceProcessingEntry()
            obj = GetData(strCode, NavType, Nothing)

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsRiceProcessingEntry
        Try
            Dim obj As New clsRiceProcessingEntry()
            obj.Arr = New List(Of clsRiceProcessingDetail)
            obj.Arr_FInish = New List(Of clsRiceProcessingFinishDetail)

            Dim qry As String = "select TSPL_RICE_PROCESSING_HEAD.*,tspl_location_master.location_desc,location.location_desc as to_loc_desc from TSPL_RICE_PROCESSING_HEAD left outer join tspl_location_master on tspl_location_master.location_code=TSPL_RICE_PROCESSING_HEAD.from_location_Code"
            qry += " left outer join tspl_location_master location on location.location_code=TSPL_RICE_PROCESSING_HEAD.to_location_Code"
            qry += " where 2=2"

            Select Case NavType
                Case NavigatorType.Current
                    qry += " and TSPL_RICE_PROCESSING_HEAD.doc_no='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and TSPL_RICE_PROCESSING_HEAD.doc_no in (select min(doc_no) from TSPL_RICE_PROCESSING_HEAD)"
                Case NavigatorType.Last
                    qry += " and TSPL_RICE_PROCESSING_HEAD.doc_no in (select max(doc_no) from TSPL_RICE_PROCESSING_HEAD)"
                Case NavigatorType.Next
                    qry += " and TSPL_RICE_PROCESSING_HEAD.doc_no in (select min(doc_no) from TSPL_RICE_PROCESSING_HEAD where doc_no>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and TSPL_RICE_PROCESSING_HEAD.doc_no in (select max(doc_no) from TSPL_RICE_PROCESSING_HEAD where doc_no<'" + strCode + "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

             If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.doccode = clsCommon.myCstr(dt.Rows(0)("doc_no"))
                obj.Docdate = clsCommon.myCDate(dt.Rows(0)("doc_date"))
                obj.descrptn = clsCommon.myCstr(dt.Rows(0)("description"))
                obj.frm_loc_code = clsCommon.myCstr(dt.Rows(0)("from_location_code"))
                obj.frm_loc_desc = clsCommon.myCstr(dt.Rows(0)("location_desc"))
                obj.to_loc_code = clsCommon.myCstr(dt.Rows(0)("to_location_code"))
                obj.to_loc_desc = clsCommon.myCstr(dt.Rows(0)("to_loc_desc"))
                obj.process_Charge = clsCommon.myCdbl(dt.Rows(0)("process_charge"))
                obj.process_cost = clsCommon.myCdbl(dt.Rows(0)("process_cost"))
                obj.admin_charge = clsCommon.myCdbl(dt.Rows(0)("admin_charge"))
                obj.admin_cost = clsCommon.myCdbl(dt.Rows(0)("admin_cost"))
                obj.total_cost = clsCommon.myCdbl(dt.Rows(0)("total_cost"))
                obj.effective_cost = clsCommon.myCdbl(dt.Rows(0)("effective_cost"))

                If dt.Rows(0)("status") Is DBNull.Value Then
                    obj.is_post = 0
                Else
                    obj.is_post = CInt(dt.Rows(0)("status"))
                End If


                qry = "select TSPL_RICE_PROCESSING_DETAIL.*,TSPL_MF_BOM_HEAD.description as bom_desc from TSPL_RICE_PROCESSING_DETAIL left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.bom_code=TSPL_RICE_PROCESSING_DETAIL.bom_code where TSPL_RICE_PROCESSING_DETAIL.doc_no='" + obj.doccode + "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsRiceProcessingDetail()

                        objtr.Lineno = clsCommon.myCstr(dr("Line_No"))
                        objtr.Itemcode = clsCommon.myCstr(dr("item_code"))
                        objtr.uom = clsCommon.myCstr(dr("unit_code"))
                        objtr.qty = clsCommon.myCdbl(dr("qty"))
                        objtr.balance = clsCommon.myCstr(dr("Balance_Qty"))
                        objtr.cost = clsCommon.myCdbl(dr("item_cost"))
                        objtr.value = clsCommon.myCdbl(dr("value"))
                        objtr.Lot_no = clsCommon.myCstr(dr("lot_no"))
                        objtr.remarks = clsCommon.myCstr(dr("remarks"))
                        objtr.bom_code = clsCommon.myCstr(dr("BOM_Code"))
                        objtr.bom_desc = clsCommon.myCstr(dr("bom_desc"))

                        obj.Arr.Add(objtr)
                    Next
                End If

                qry = "select * from TSPL_RICE_PROCESSING_FINISH_DETAIL where doc_no='" + obj.doccode + "'"
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsRiceProcessingFinishDetail()

                        objtr.Lineno = clsCommon.myCstr(dr("Line_No"))
                        objtr.Itemcode = clsCommon.myCstr(dr("item_code"))
                        objtr.uom = clsCommon.myCstr(dr("unit_code"))
                        objtr.qty = clsCommon.myCdbl(dr("qty"))
                        objtr.lot_no = clsCommon.myCstr(dr("lot_no"))
                        objtr.qty_pers = clsCommon.myCdbl(dr("Percentage"))
                        objtr.cost = clsCommon.myCdbl(dr("item_cost"))
                        objtr.value = clsCommon.myCdbl(dr("value"))
                        objtr.remarks = clsCommon.myCstr(dr("remarks"))
                        objtr.is_Prinicple = CInt(dr("is_Principle"))

                        obj.Arr_FInish.Add(objtr)
                    Next
                End If
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsRiceProcessingEntry, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            Dim coll As New Hashtable()

            If isNewEntry Then
                obj.doccode = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, obj.Docdate, clsDocType.RICEPROCESSING, "", obj.frm_loc_code))
            End If

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Doc_no", obj.doccode)
            clsCommon.AddColumnsForChange(coll, "doc_date", clsCommon.GetPrintDate(obj.Docdate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "description", obj.descrptn)
            clsCommon.AddColumnsForChange(coll, "from_location_code", obj.frm_loc_code, True)
            clsCommon.AddColumnsForChange(coll, "to_location_code", obj.to_loc_code, True)
            clsCommon.AddColumnsForChange(coll, "process_charge", obj.process_Charge)
            clsCommon.AddColumnsForChange(coll, "process_cost", obj.process_cost)
            clsCommon.AddColumnsForChange(coll, "admin_charge", obj.admin_charge)
            clsCommon.AddColumnsForChange(coll, "admin_cost", obj.admin_cost)
            clsCommon.AddColumnsForChange(coll, "total_cost", obj.total_cost)
            clsCommon.AddColumnsForChange(coll, "effective_cost", obj.effective_cost)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RICE_PROCESSING_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RICE_PROCESSING_HEAD", OMInsertOrUpdate.Update, " doc_no='" + obj.doccode + "'", trans)
            End If

            isSaved = isSaved AndAlso clsRiceProcessingDetail.SaveData(obj.doccode, obj.Arr, trans)
            isSaved = isSaved AndAlso clsRiceProcessingFinishDetail.SaveData(obj.doccode, obj.Arr_FInish, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetStockBalance(ByVal frm_loc As String, ByVal doc_no As String, ByVal itemcode As String, ByVal uom As String, ByVal LotNo As String) As Decimal
        Try
            Dim CF As Decimal = clsItemMaster.GetConvertionFactor(itemcode, uom, Nothing)

            If CF <= 0 Then
                CF = 1
            End If

            Dim qry As String = "select sum(aa.Qty) as Qty from ( "
            qry += "select TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_INVENTORY_MOVEMENT.Item_Desc,round((isnull(TSPL_INVENTORY_MOVEMENT.Qty,0)*tspl_item_uom_detail.conversion_factor)/" + clsCommon.myCstr(CF) + ",2) as Qty,TSPL_INVENTORY_MOVEMENT.UOM,TSPL_INVENTORY_MOVEMENT.Basic_Cost,TSPL_INVENTORY_MOVEMENT.Batch_No from TSPL_INVENTORY_MOVEMENT left outer join tspl_item_uom_detail on TSPL_INVENTORY_MOVEMENT.item_code=tspl_item_uom_detail.item_code and tspl_item_uom_detail.uom_code=TSPL_INVENTORY_MOVEMENT.uom where TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.location_code='" + frm_loc + "' "
            qry += "union all "
            qry += "select TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_INVENTORY_MOVEMENT.Item_Desc,round(((0-isnull(TSPL_INVENTORY_MOVEMENT.Qty,0))*tspl_item_uom_detail.conversion_factor)/" + clsCommon.myCstr(CF) + ",2) as Qty,TSPL_INVENTORY_MOVEMENT.UOM,TSPL_INVENTORY_MOVEMENT.Basic_Cost,TSPL_INVENTORY_MOVEMENT.Batch_No from TSPL_INVENTORY_MOVEMENT left outer join tspl_item_uom_detail on TSPL_INVENTORY_MOVEMENT.item_code=tspl_item_uom_detail.item_code and tspl_item_uom_detail.uom_code=TSPL_INVENTORY_MOVEMENT.uom where TSPL_INVENTORY_MOVEMENT.InOut='O' and TSPL_INVENTORY_MOVEMENT.location_code='" + frm_loc + "' "
            qry += ")aa left outer join TSPL_ITEM_MASTER on aa.Item_Code=TSPL_ITEM_MASTER.Item_Code "
            qry += " where aa.item_code='" + itemcode + "' and aa.batch_no='" + LotNo + "'"
            CF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

            Return CF
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_RICE_PROCESSING_FINISH_DETAIL where doc_no='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_RICE_PROCESSING_DETAIL where doc_no='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_RICE_PROCESSING_HEAD where doc_no='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Document code not found to Post")
            End If

            Dim obj As clsRiceProcessingEntry = clsRiceProcessingEntry.GetData(strCode, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.doccode) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.is_post = 1) Then
                Throw New Exception("Document Already Posted")
            End If

            isSaved = isSaved AndAlso SendToInventoryMovement(obj, trans)

            Dim qry As String = "update TSPL_RICE_PROCESSING_HEAD set status=1,modified_by='" + objCommonVar.CurrentUserCode + "',modified_date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "' where doc_no='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SendToInventoryMovement(ByVal obj As clsRiceProcessingEntry, Optional ByVal trans As SqlTransaction = Nothing) As Boolean

        Dim ArrInventoryMovementOut As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        Dim ArrInventoryMovementIn As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        Dim isSaved As Boolean = True

        Dim strRgpNo As String = Nothing
        Dim intCounter As Integer = 0
        For Each objTr As clsRiceProcessingDetail In obj.Arr
            intCounter = intCounter + 1

            Dim strItemType As String = clsItemMaster.GetItemType(objTr.Itemcode, trans)
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

            Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.Itemcode, objTr.uom, trans)
            If ConvFac = 0 Then
                Throw New Exception("Conversion Factor found zero for item :" + objTr.Itemcode + " and Uom:'" + objTr.uom)
            End If

            Dim objInventoryMovemnt As New clsInventoryMovement()

            objInventoryMovemnt.InOut = "O"
            objInventoryMovemnt.Location_Code = obj.frm_loc_code
            objInventoryMovemnt.Other_Location_Code = obj.to_loc_code
            objInventoryMovemnt.Other_Location_Desc = obj.to_loc_desc

            objInventoryMovemnt.Item_Code = objTr.Itemcode
            objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(objTr.Itemcode, trans)
            objInventoryMovemnt.Qty = objTr.qty
            objInventoryMovemnt.UOM = objTr.uom
            objInventoryMovemnt.Basic_Cost = objTr.cost
            objInventoryMovemnt.MRP = 0
            objInventoryMovemnt.Add_Cost = 0
            objInventoryMovemnt.Net_Cost = objTr.cost
            objInventoryMovemnt.Batch_No = objTr.Lot_no
            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                objInventoryMovemnt.ItemType = "RM"
            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                objInventoryMovemnt.ItemType = "OT"
            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                objInventoryMovemnt.ItemType = "FT"
            End If
            objInventoryMovemnt.ItemType = strItemTypeToSave
            ArrInventoryMovementOut.Add(objInventoryMovemnt)
        Next
        isSaved = isSaved AndAlso clsInventoryMovement.SaveData("RICE-PROC", obj.doccode, obj.Docdate, clsCommon.GetPrintDate(obj.Docdate, "dd/MM/yyyy"), ArrInventoryMovementOut, trans)

        '===============In
        For Each objTr As clsRiceProcessingFinishDetail In obj.Arr_FInish

            Dim strItemType As String = clsItemMaster.GetItemType(objTr.Itemcode, trans)
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

            Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.Itemcode, objTr.uom, trans)
            If ConvFac = 0 Then
                Throw New Exception("Conversion Factor found zero for item :" + objTr.Itemcode + " and Uom:'" + objTr.uom)
            End If

            Dim objInventoryMovemnt As New clsInventoryMovement()

            objInventoryMovemnt.InOut = "I"
            objInventoryMovemnt.Location_Code = obj.to_loc_code

            objInventoryMovemnt.Other_Location_Code = obj.frm_loc_code
            objInventoryMovemnt.Other_Location_Desc = obj.frm_loc_desc

            objInventoryMovemnt.Item_Code = objTr.Itemcode
            objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(objTr.Itemcode, trans)
            objInventoryMovemnt.Qty = objTr.qty
            objInventoryMovemnt.UOM = objTr.uom
            objInventoryMovemnt.Basic_Cost = objTr.cost
            objInventoryMovemnt.MRP = 0
            objInventoryMovemnt.Batch_No = objTr.lot_no
            objInventoryMovemnt.Add_Cost = 0
            objInventoryMovemnt.Net_Cost = objTr.cost
            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                objInventoryMovemnt.ItemType = "RM"
            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                objInventoryMovemnt.ItemType = "OT"
            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                objInventoryMovemnt.ItemType = "FT"
            End If
            objInventoryMovemnt.ItemType = strItemTypeToSave
            ArrInventoryMovementIn.Add(objInventoryMovemnt)
        Next
        isSaved = isSaved AndAlso clsInventoryMovement.SaveData("RICE-PROC", obj.doccode, obj.Docdate, clsCommon.GetPrintDate(obj.Docdate, "dd/MM/yyyy"), ArrInventoryMovementIn, trans)
        Return isSaved
    End Function

End Class

Public Class clsRiceProcessingDetail
#Region "variables"
Public Lineno As Integer = Nothing
    Public Itemcode As String = Nothing
    Public uom As String = Nothing
    Public qty As String = Nothing
    Public balance As String = Nothing
    Public cost As String = Nothing
    Public value As String = Nothing
    Public remarks As String = Nothing
    Public bom_code As String = Nothing
    Public bom_desc As String = Nothing
    Public Lot_no As String = Nothing
#End Region

Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsRiceProcessingDetail  ), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_RICE_PROCESSING_DETAIL where doc_no='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsRiceProcessingDetail In arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "doc_no", strCode)
                    clsCommon.AddColumnsForChange(coll, "BOM_Code", objtr.bom_code)
                    clsCommon.AddColumnsForChange(coll, "line_no", objtr.Lineno)
                    clsCommon.AddColumnsForChange(coll, "item_code", objtr.Itemcode)
                    clsCommon.AddColumnsForChange(coll, "unit_code", objtr.uom, True)
                    clsCommon.AddColumnsForChange(coll, "qty", objtr.qty)
                    clsCommon.AddColumnsForChange(coll, "item_cost", objtr.cost)
                    clsCommon.AddColumnsForChange(coll, "value", objtr.value)
                    clsCommon.AddColumnsForChange(coll, "Balance_Qty", objtr.balance)
                    clsCommon.AddColumnsForChange(coll, "Lot_No", objtr.Lot_no)
                    clsCommon.AddColumnsForChange(coll, "remarks", objtr.remarks)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RICE_PROCESSING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsRiceProcessingFinishDetail
#Region "variables"
Public Lineno As Integer = Nothing
    Public Itemcode As String = Nothing
    Public uom As String = Nothing
    Public qty As String = Nothing
    Public qty_pers As String = Nothing
    Public cost As String = Nothing
    Public value As String = Nothing
    Public remarks As String = Nothing
    Public lot_no As String = Nothing
    Public is_Prinicple As Integer = Nothing
#End Region

Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsRiceProcessingFinishDetail ), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_RICE_PROCESSING_FINISH_DETAIL where doc_no='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsRiceProcessingFinishDetail In arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "doc_no", strCode)
                    clsCommon.AddColumnsForChange(coll, "line_no", objtr.Lineno)
                    clsCommon.AddColumnsForChange(coll, "item_code", objtr.Itemcode)
                    clsCommon.AddColumnsForChange(coll, "unit_code", objtr.uom, True)
                    clsCommon.AddColumnsForChange(coll, "qty", objtr.qty)
                    clsCommon.AddColumnsForChange(coll, "item_cost", objtr.cost)
                    clsCommon.AddColumnsForChange(coll, "value", objtr.value)
                    clsCommon.AddColumnsForChange(coll, "Percentage", objtr.qty_pers)
                    clsCommon.AddColumnsForChange(coll, "remarks", objtr.remarks)
                    clsCommon.AddColumnsForChange(coll, "Lot_No", objtr.lot_no)
                    clsCommon.AddColumnsForChange(coll, "is_Principle", objtr.is_Prinicple)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RICE_PROCESSING_FINISH_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class