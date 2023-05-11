Imports common
Imports System.Data.SqlClient

Public Class clsRiceMixingEntry
#Region "variables"
    Public doccode As String = Nothing
    Public Docdate As Date = Nothing
    Public descrptn As String = Nothing
    Public frm_loc_code As String = Nothing
    Public frm_loc_desc As String = Nothing
    Public to_loc_code As String = Nothing
    Public to_loc_desc As String = Nothing
    Public Charge As Decimal = Nothing
    Public Mixi_uom As String = Nothing
    Public is_post As Integer = Nothing

    Public Arr As List(Of clsRiceMixingDetail) = Nothing
    Public Arr_FInish As List(Of clsRiceMixingFinishDetail) = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal CurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select doc_no as Code,doc_date as [Date],[Description],from_location as [From Location],to_location as [To Location],mixing_charge as [Mixing Charge],mixing_unit as [Mixing UOM],(case when status=1 then'Approved' else 'Pending' end) as Status from TSPL_RICE_MIXING_HEAD"
        str = clsCommon.ShowSelectForm("MIXFND", qry, "Code", whrCls, CurrCode, "Code", isButtonClicked)

        Return str
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsRiceMixingEntry
        Try
            Dim obj As New clsRiceMixingEntry()
            obj = GetData(strCode, NavType, Nothing)

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsRiceMixingEntry
        Try
            Dim obj As New clsRiceMixingEntry()
            obj.Arr = New List(Of clsRiceMixingDetail)
            obj.Arr_FInish = New List(Of clsRiceMixingFinishDetail)

            Dim qry As String = "select TSPL_RICE_MIXING_HEAD.*,tspl_location_master.location_desc,location.location_desc as to_loc_desc from TSPL_RICE_MIXING_HEAD left outer join tspl_location_master on tspl_location_master.location_code=TSPL_RICE_MIXING_HEAD.from_location"
            qry += " left outer join tspl_location_master location on location.location_code=TSPL_RICE_MIXING_HEAD.to_location where 2=2"

            Select Case NavType
                Case NavigatorType.Current
                    qry += " and TSPL_RICE_MIXING_HEAD.doc_no='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and TSPL_RICE_MIXING_HEAD.doc_no in (select min(doc_no) from TSPL_RICE_MIXING_HEAD)"
                Case NavigatorType.Last
                    qry += " and TSPL_RICE_MIXING_HEAD.doc_no in (select max(doc_no) from TSPL_RICE_MIXING_HEAD)"
                Case NavigatorType.Next
                    qry += " and TSPL_RICE_MIXING_HEAD.doc_no in (select min(doc_no) from TSPL_RICE_MIXING_HEAD where doc_no>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and TSPL_RICE_MIXING_HEAD.doc_no in (select max(doc_no) from TSPL_RICE_MIXING_HEAD where doc_no<'" + strCode + "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.doccode = clsCommon.myCstr(dt.Rows(0)("doc_no"))
                obj.Docdate = clsCommon.myCDate(dt.Rows(0)("doc_date"))
                obj.descrptn = clsCommon.myCstr(dt.Rows(0)("description"))
                obj.frm_loc_code = clsCommon.myCstr(dt.Rows(0)("from_location"))
                obj.frm_loc_desc = clsCommon.myCstr(dt.Rows(0)("location_desc"))
                obj.to_loc_code = clsCommon.myCstr(dt.Rows(0)("to_location"))
                obj.to_loc_desc = clsCommon.myCstr(dt.Rows(0)("to_loc_desc"))
                obj.Mixi_uom = clsCommon.myCstr(dt.Rows(0)("mixing_unit"))
                obj.Charge = clsCommon.myCdbl(dt.Rows(0)("mixing_charge"))

                If dt.Rows(0)("status") Is DBNull.Value Then
                    obj.is_post = 0
                Else
                    obj.is_post = CInt(dt.Rows(0)("status"))
                End If


                qry = "select * from TSPL_RICE_MIXING_DETAIL where doc_no='" + obj.doccode + "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsRiceMixingDetail()

                        objtr.Lineno = clsCommon.myCstr(dr("Line_No"))
                        objtr.Itemcode = clsCommon.myCstr(dr("item_code"))
                        objtr.uom = clsCommon.myCstr(dr("unit_code"))
                        objtr.qty = clsCommon.myCdbl(dr("qty"))
                        objtr.Lot_no = clsCommon.myCstr(dr("lot_no"))
                        objtr.cost = clsCommon.myCdbl(dr("item_cost"))
                        objtr.value = clsCommon.myCdbl(dr("value"))
                        objtr.remarks = clsCommon.myCstr(dr("remarks"))
                        objtr.Balance = clsCommon.myCdbl(dr("Balance_Qty"))

                        obj.Arr.Add(objtr)
                    Next
                End If

                qry = "select * from TSPL_RICE_MIXING_FINISH_DETAIL where doc_no='" + obj.doccode + "'"
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsRiceMixingFinishDetail()

                        objtr.Lineno = clsCommon.myCstr(dr("Line_No"))
                        objtr.Itemcode = clsCommon.myCstr(dr("item_code"))
                        objtr.uom = clsCommon.myCstr(dr("unit_code"))
                        objtr.qty = clsCommon.myCdbl(dr("qty"))
                        objtr.Lot_no = clsCommon.myCstr(dr("lot_no"))
                        objtr.cost = clsCommon.myCdbl(dr("item_cost"))
                        objtr.value = clsCommon.myCdbl(dr("value"))
                        objtr.Mixi_charge = clsCommon.myCdbl(dr("Mixing_Amount"))
                        objtr.Total_cost = clsCommon.myCdbl(dr("Total_Cost"))
                        objtr.remarks = clsCommon.myCstr(dr("remarks"))

                        obj.Arr_FInish.Add(objtr)
                    Next
                End If
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsRiceMixingEntry, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            Dim coll As New Hashtable()

            If isNewEntry Then
                obj.doccode = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, obj.Docdate, clsDocType.RICEMIXING, "", obj.frm_loc_code))
            End If

            Dim qry As String = "delete from TSPL_RICE_MIXING_FINISH_DETAIL where doc_no='" + obj.doccode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_RICE_MIXING_DETAIL where doc_no='" + obj.doccode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Doc_no", obj.doccode)
            clsCommon.AddColumnsForChange(coll, "doc_date", clsCommon.GetPrintDate(obj.Docdate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "description", obj.descrptn)
            clsCommon.AddColumnsForChange(coll, "from_location", obj.frm_loc_code, True)
            clsCommon.AddColumnsForChange(coll, "to_location", obj.to_loc_code, True)
            clsCommon.AddColumnsForChange(coll, "Mixing_Charge", obj.Charge)
            clsCommon.AddColumnsForChange(coll, "Mixing_Unit", obj.Mixi_uom, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RICE_MIXING_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RICE_MIXING_HEAD", OMInsertOrUpdate.Update, " doc_no='" + obj.doccode + "'", trans)
            End If

            isSaved = isSaved AndAlso clsRiceMixingDetail.SaveData(obj.doccode, obj.Arr, trans)
            isSaved = isSaved AndAlso clsRiceMixingFinishDetail.SaveData(obj.doccode, obj.Arr_FInish, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_RICE_MIXING_FINISH_DETAIL where doc_no='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_RICE_MIXING_DETAIL where doc_no='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_RICE_MIXING_HEAD where doc_no='" + strCode + "'"
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

            Dim obj As clsRiceMixingEntry = clsRiceMixingEntry.GetData(strCode, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.doccode) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.is_post = 1) Then
                Throw New Exception("Document Already Posted")
            End If

            isSaved = isSaved AndAlso SendToInventoryMovement(obj, trans)

            Dim qry As String = "update TSPL_RICE_MIXING_HEAD set status=1,modified_by='" + objCommonVar.CurrentUserCode + "',modified_date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "' where doc_no='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SendToInventoryMovement(ByVal obj As clsRiceMixingEntry, Optional ByVal trans As SqlTransaction = Nothing) As Boolean

        Dim ArrInventoryMovementOut As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        Dim ArrInventoryMovementIn As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        Dim isSaved As Boolean = True

        Dim strRgpNo As String = Nothing
        Dim intCounter As Integer = 0
        For Each objTr As clsRiceMixingDetail In obj.Arr
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
            objInventoryMovemnt.Batch_No = objTr.Lot_no
            objInventoryMovemnt.Net_Cost = objTr.cost
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
        isSaved = isSaved AndAlso clsInventoryMovement.SaveData("RICE-MIX", obj.doccode, obj.Docdate, clsCommon.GetPrintDate(obj.Docdate, "dd/MM/yyyy"), ArrInventoryMovementOut, trans)

        '===============In
        For Each objTr As clsRiceMixingFinishDetail In obj.Arr_FInish

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
            objInventoryMovemnt.Item_Code = objTr.Itemcode
            objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(objTr.Itemcode, trans)
            objInventoryMovemnt.Qty = objTr.qty
            objInventoryMovemnt.UOM = objTr.uom
            objInventoryMovemnt.Basic_Cost = objTr.cost
            objInventoryMovemnt.MRP = 0
            objInventoryMovemnt.Add_Cost = 0
            objInventoryMovemnt.Batch_No = objTr.Lot_no
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
        isSaved = isSaved AndAlso clsInventoryMovement.SaveData("RICE-MIX", obj.doccode, obj.Docdate, clsCommon.GetPrintDate(obj.Docdate, "dd/MM/yyyy"), ArrInventoryMovementIn, trans)
        Return isSaved
    End Function

    Public Shared Function GetConversionFactor(ByVal Icode As String, ByVal UOM As String) As Decimal
        Dim CF As Decimal = 0

        Dim qry As String = "select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + Icode + "' and UOM_Code='" + UOM + "'"
        CF = clsDBFuncationality.getSingleValue(qry)

        Return CF
    End Function
End Class

Public Class clsRiceMixingDetail
#Region "variables"
    Public Lineno As Integer = Nothing
    Public Itemcode As String = Nothing
    Public uom As String = Nothing
    Public qty As String = Nothing
    Public Lot_no As String = Nothing
    Public cost As String = Nothing
    Public value As String = Nothing
    Public remarks As String = Nothing 
    Public Balance As Decimal = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsRiceMixingDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_RICE_MIXING_DETAIL where doc_no='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsRiceMixingDetail In arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "doc_no", strCode)
                    clsCommon.AddColumnsForChange(coll, "line_no", objtr.Lineno)
                    clsCommon.AddColumnsForChange(coll, "item_code", objtr.Itemcode)
                    clsCommon.AddColumnsForChange(coll, "unit_code", objtr.uom, True)
                    clsCommon.AddColumnsForChange(coll, "qty", objtr.qty)
                    clsCommon.AddColumnsForChange(coll, "item_cost", objtr.cost)
                    clsCommon.AddColumnsForChange(coll, "Balance_Qty", objtr.Balance)
                    clsCommon.AddColumnsForChange(coll, "value", objtr.value)
                    clsCommon.AddColumnsForChange(coll, "lot_no", objtr.Lot_no)
                    clsCommon.AddColumnsForChange(coll, "remarks", objtr.remarks)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RICE_MIXING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If


            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsRiceMixingFinishDetail
#Region "variables"
    Public Lineno As Integer = Nothing
    Public Itemcode As String = Nothing
    Public uom As String = Nothing
    Public qty As String = Nothing
    Public Lot_no As String = Nothing
    Public cost As String = Nothing
    Public value As String = Nothing
Public Mixi_charge As Decimal=Nothing 
Public Total_cost As Decimal=Nothing 
    Public remarks As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsRiceMixingFinishDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_RICE_MIXING_FINISH_DETAIL where doc_no='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsRiceMixingFinishDetail In arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "doc_no", strCode)
                    clsCommon.AddColumnsForChange(coll, "line_no", objtr.Lineno)
                    clsCommon.AddColumnsForChange(coll, "item_code", objtr.Itemcode)
                    clsCommon.AddColumnsForChange(coll, "unit_code", objtr.uom, True)
                    clsCommon.AddColumnsForChange(coll, "qty", objtr.qty)
                    clsCommon.AddColumnsForChange(coll, "item_cost", objtr.cost)
                    clsCommon.AddColumnsForChange(coll, "value", objtr.value)
                    clsCommon.AddColumnsForChange(coll, "Mixing_Amount", objtr.Mixi_charge)
                    clsCommon.AddColumnsForChange(coll, "Total_Cost", objtr.Total_cost)
                    clsCommon.AddColumnsForChange(coll, "lot_no", objtr.Lot_no)
                    clsCommon.AddColumnsForChange(coll, "remarks", objtr.remarks)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RICE_MIXING_FINISH_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
