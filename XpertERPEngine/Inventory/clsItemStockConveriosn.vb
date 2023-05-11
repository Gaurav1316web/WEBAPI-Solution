Imports common
Imports System.Data.SqlClient

Public Class clsItemStockConveriosnHead
    Public Doc_No As String = String.Empty
    Public Doc_Date As String = String.Empty
    Public Item_Code As String = String.Empty
    Public Item_Desc As String = String.Empty
    Public Location_Code As String = String.Empty
    Public Location_Desc As String = String.Empty
    Public Created_By As String = String.Empty
    Public Created_Date As String = String.Empty
    Public Modify_By As String = String.Empty
    Public Modify_Date As String = String.Empty
    Public comp_code As String = String.Empty
    Public arrDetail As List(Of clsItemStockConveriosnDetail) = Nothing
    Public isNewEntry As Boolean = False
    Public isPosted As Integer = 0
    Public Posting_Date As String = String.Empty
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Try
            Dim str As String = ""
            Dim qry As String = " select tspl_item_stock_conversion_head.Doc_No as [DocNo] ,tspl_item_stock_conversion_head.Doc_Date as [Doc Date] ,tspl_item_stock_conversion_head.Item_Code as [Item Code] ,tspl_item_stock_conversion_head.Item_Desc as [Item Desc] ,tspl_item_stock_conversion_head.Location_Code as [Location Code] ,tspl_item_stock_conversion_head.Location_Desc as [Location Desc] ,tspl_item_stock_conversion_head.IsPosted as [Isposted] ,tspl_item_stock_conversion_head.Posting_Date as [Posting Date] ,tspl_item_stock_conversion_head.Created_By as [Created By] ,tspl_item_stock_conversion_head.Created_Date as [Created Date] ,tspl_item_stock_conversion_head.Modify_By as [Modify By] ,tspl_item_stock_conversion_head.Modify_Date as [Modify Date] ,tspl_item_stock_conversion_head.comp_code as [Comp Code]  From tspl_item_stock_conversion_head "
            str = clsCommon.ShowSelectForm("ITMSTKCONV", qry, "DocNo", whrcls, curcode, "DocNo", isButtonClicked)
            Return str

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function SaveData(ByVal obj As clsItemStockConveriosnHead, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_No", obj.Doc_No)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Desc", obj.Location_Desc)
            clsCommon.AddColumnsForChange(coll, "isPosted", 0)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", obj.comp_code)
            clsCommon.AddColumnsForChange(coll, "Modify_By", obj.Modify_By)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", obj.Modify_Date)
            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", obj.Created_By)
                clsCommon.AddColumnsForChange(coll, "Created_Date", obj.Created_Date)
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Item_Stock_Conversion_Head", OMInsertOrUpdate.Insert, "", tran)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Item_Stock_Conversion_Head", OMInsertOrUpdate.Update, "TSPL_Item_Stock_Conversion_Head.Doc_No='" + obj.Doc_No + "'", tran)
            End If
            issaved = issaved And clsItemStockConveriosnDetail.SaveData(obj.arrDetail, tran)
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType) As clsItemStockConveriosnHead
        Dim obj As New clsItemStockConveriosnHead
        Try
            Dim whrCls As String = String.Empty
            obj.arrDetail = New List(Of clsItemStockConveriosnDetail)
            Dim qst As String = " select *   From TSPL_Item_Stock_Conversion_Head   where 1=1 " & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_Item_Stock_Conversion_Head.Doc_No in ('" + strCode + "')"
                Case NavigatorType.Next
                    qst += " and TSPL_Item_Stock_Conversion_Head.Doc_No in (select min(Doc_No ) from TSPL_Item_Stock_Conversion_Head where Doc_No  >'" + strCode + "' " & whrCls & ") "
                Case NavigatorType.First
                    qst += " and TSPL_Item_Stock_Conversion_Head.Doc_No in (select MIN(Doc_No ) from TSPL_Item_Stock_Conversion_Head where 1=1 " & whrCls & ") "
                Case NavigatorType.Last
                    qst += " and TSPL_Item_Stock_Conversion_Head.Doc_No in (select Max(Doc_No ) from TSPL_Item_Stock_Conversion_Head where 1=1 " & whrCls & ") "
                Case NavigatorType.Previous
                    qst += " and TSPL_Item_Stock_Conversion_Head.Doc_No in (select Max(Doc_No ) from TSPL_Item_Stock_Conversion_Head where Doc_No  <'" + strCode + "' " & whrCls & ") "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Doc_No = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
                obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
                obj.isPosted = clsCommon.myCdbl(dt.Rows(0)("isPosted"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
                End If
                obj.arrDetail = clsItemStockConveriosnDetail.getData(obj.Doc_No)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function


    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "select isposted from tspl_item_stock_conversion_head where Doc_No='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) = 1 Then
                Throw New Exception("transaction status should be posted for reverse and unpost")
            End If

            
            qry = "delete from tspl_inventory_movement where source_doc_no='" + strCode + "' and trans_type='StockConversion'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update tspl_item_stock_conversion_head set isposted = 0 where Doc_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Doc No not found to Post")
            End If

            Dim obj As clsItemStockConveriosnHead = clsItemStockConveriosnHead.getData(strDocNo, NavigatorType.Current)
            trans = clsDBFuncationality.GetTransactin()
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Inventory", "Item Stock Conversion", obj.Location_Code, clsCommon.myCDate(obj.Doc_Date), trans)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            '--------------------
            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_Item_Stock_Conversion_Head", "Doc_No", obj.Doc_No, trans)
            If isResult = False Then
                trans.Commit()
                Return False
            End If

            ''Adjustment Out Type document
            Dim objAdjOut As New ClsAdjustments
            objAdjOut.Adjustment_Date = obj.Doc_Date
            objAdjOut.Posting_Date = obj.Doc_Date
            objAdjOut.EntryDateTime = obj.Doc_Date
            objAdjOut.Against_Item_Stock_Conversion = obj.Doc_No
            objAdjOut.Loc_Code = obj.Location_Code
            objAdjOut.Loc_Desc = obj.Location_Desc
            objAdjOut.Trans_Type = "Out"
            objAdjOut.Arr = New List(Of ClsAdjustmentsDetails)
            For Each objtr As clsItemStockConveriosnDetail In obj.arrDetail
                Dim objAdjOutTR As New ClsAdjustmentsDetails()
                If objtr.Input_From_Qty > 0 AndAlso clsCommon.myLen(objtr.UOM_Code) > 0 Then
                    objAdjOutTR.Item_Code = obj.Item_Code
                    objAdjOutTR.Item_Description = obj.Item_Desc
                    objAdjOutTR.Adjustment_Type = "QD"
                    objAdjOutTR.Item_Quantity = objtr.Input_From_Qty
                    objAdjOutTR.Item_Cost = 0
                    objAdjOutTR.mrp = objtr.Input_From_MRP
                    objAdjOutTR.Unit_Code = objtr.UOM_Code
                    objAdjOut.Arr.Add(objAdjOutTR)
                End If
            Next
            objAdjOut.SaveData(objAdjOut, True, "", trans)
            ClsAdjustments.PostData(objAdjOut.Adjustment_No, objAdjOut.Trans_Type, trans)

            ''Adjustment IN Type document
            Dim objAdjIn As New ClsAdjustments
            objAdjIn.Adjustment_Date = obj.Doc_Date
            objAdjIn.Posting_Date = obj.Doc_Date
            objAdjIn.EntryDateTime = obj.Doc_Date
            objAdjIn.Against_Item_Stock_Conversion = obj.Doc_No
            objAdjIn.Loc_Code = obj.Location_Code
            objAdjIn.Loc_Desc = obj.Location_Desc
            objAdjIn.Trans_Type = "In"
            objAdjIn.Arr = New List(Of ClsAdjustmentsDetails)
            For Each objtr As clsItemStockConveriosnDetail In obj.arrDetail
                If objtr.Output_To_qty > 0 AndAlso clsCommon.myLen(objtr.Required_UOM_Code) > 0 Then
                    Dim objAdjInTR As New ClsAdjustmentsDetails()
                    objAdjInTR.Item_Code = obj.Item_Code
                    objAdjInTR.Item_Description = obj.Item_Desc
                    objAdjInTR.Adjustment_Type = "QI"
                    objAdjInTR.Item_Quantity = objtr.Output_To_qty
                    objAdjInTR.Item_Cost = 0
                    objAdjInTR.mrp = objtr.Output_To_MRP
                    objAdjInTR.Unit_Code = objtr.Required_UOM_Code
                    objAdjIn.Arr.Add(objAdjInTR)
                End If
                If objtr.Output_Stock_Qty > 0 Then
                    Dim objAdjInTR As New ClsAdjustmentsDetails()
                    objAdjInTR.Item_Code = obj.Item_Code
                    objAdjInTR.Item_Description = obj.Item_Desc
                    objAdjInTR.Adjustment_Type = "QI"
                    objAdjInTR.Item_Quantity = objtr.Output_Stock_Qty
                    objAdjInTR.Item_Cost = 0
                    objAdjInTR.mrp = objtr.Output_Stock_MRP
                    objAdjInTR.Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code  from TSPL_ITEM_UOM_DETAIL where Item_Code='" & obj.Item_Code & "' and Stocking_Unit='Y'", trans))
                    objAdjIn.Arr.Add(objAdjInTR)
                End If
            Next
            objAdjIn.SaveData(objAdjIn, True, "", trans)
            ClsAdjustments.PostData(objAdjIn.Adjustment_No, objAdjIn.Trans_Type, trans)
            'Dim qry As String = ""
            ''Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            'Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            'Dim strItemType As String = clsItemMaster.GetItemType(obj.Item_Code, trans)
            'Dim strItemTypeToSave As String = ""
            'If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
            '    strItemTypeToSave = "RM"
            'ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
            '    strItemTypeToSave = "OT"
            'ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
            '    strItemTypeToSave = "FT"
            'ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
            '    strItemTypeToSave = "A"
            'Else
            '    strItemTypeToSave = strItemType

            'End If

            'For i As Integer = 0 To obj.arrDetail.Count - 1

            '    Dim objInventoryMovemnt As New clsInventoryMovement()
            '    If obj.arrDetail.Item(i).Input_From_Qty > 0 AndAlso clsCommon.myLen(obj.arrDetail.Item(i).UOM_Code) > 0 Then
            '        objInventoryMovemnt.InOut = "O"
            '        objInventoryMovemnt.Location_Code = obj.Location_Code
            '        objInventoryMovemnt.Item_Code = obj.Item_Code
            '        objInventoryMovemnt.Item_Desc = obj.Item_Desc
            '        objInventoryMovemnt.Qty = obj.arrDetail.Item(i).Input_From_Qty
            '        objInventoryMovemnt.UOM = obj.arrDetail.Item(i).UOM_Code
            '        objInventoryMovemnt.MRP = 0
            '        objInventoryMovemnt.Add_Cost = 0
            '        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
            '            objInventoryMovemnt.ItemType = "RM"
            '        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
            '            objInventoryMovemnt.ItemType = "OT"
            '        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
            '            objInventoryMovemnt.ItemType = "FT"
            '        End If
            '        objInventoryMovemnt.ItemType = strItemTypeToSave
            '        ArrInventoryMovement.Add(objInventoryMovemnt)
            '    End If
            '    If obj.arrDetail.Item(i).Output_To_qty > 0 AndAlso clsCommon.myLen(obj.arrDetail.Item(i).Required_UOM_Code) > 0 Then
            '        objInventoryMovemnt = New clsInventoryMovement()
            '        objInventoryMovemnt.InOut = "I"
            '        objInventoryMovemnt.Location_Code = obj.Location_Code
            '        objInventoryMovemnt.Item_Code = obj.Item_Code
            '        objInventoryMovemnt.Item_Desc = obj.Item_Desc
            '        objInventoryMovemnt.Qty = obj.arrDetail.Item(i).Output_To_qty
            '        objInventoryMovemnt.UOM = obj.arrDetail.Item(i).Required_UOM_Code
            '        objInventoryMovemnt.MRP = 0
            '        objInventoryMovemnt.Add_Cost = 0
            '        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
            '            objInventoryMovemnt.ItemType = "RM"
            '        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
            '            objInventoryMovemnt.ItemType = "OT"
            '        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
            '            objInventoryMovemnt.ItemType = "FT"
            '        End If
            '        objInventoryMovemnt.ItemType = strItemTypeToSave
            '        ArrInventoryMovement.Add(objInventoryMovemnt)
            '    End If
            '    If obj.arrDetail.Item(i).Output_Stock_Qty > 0 Then
            '        objInventoryMovemnt = New clsInventoryMovement()
            '        objInventoryMovemnt.InOut = "I"
            '        objInventoryMovemnt.Location_Code = obj.Location_Code
            '        objInventoryMovemnt.Item_Code = obj.Item_Code
            '        objInventoryMovemnt.Item_Desc = obj.Item_Desc
            '        objInventoryMovemnt.Qty = obj.arrDetail.Item(i).Output_Stock_Qty
            '        Dim StkingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code  from TSPL_ITEM_UOM_DETAIL where Item_Code='" & obj.Item_Code & "' and Stocking_Unit='Y'", trans))
            '        objInventoryMovemnt.UOM = StkingUnit
            '        objInventoryMovemnt.MRP = 0
            '        objInventoryMovemnt.Add_Cost = 0
            '        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
            '            objInventoryMovemnt.ItemType = "RM"
            '        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
            '            objInventoryMovemnt.ItemType = "OT"
            '        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
            '            objInventoryMovemnt.ItemType = "FT"
            '        End If
            '        objInventoryMovemnt.ItemType = strItemTypeToSave
            '        ArrInventoryMovement.Add(objInventoryMovemnt)
            '    End If
            'Next
            'isSaved = isSaved AndAlso clsInventoryMovement.SaveData("StockConversion", obj.Doc_No, obj.Doc_Date, clsCommon.GetPrintDate(obj.Doc_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            

            Dim strQry As String = " update TSPL_Item_Stock_Conversion_Head set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where doc_No='" & obj.Doc_No & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function deleteData(ByVal DocNo As String, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_Item_Stock_Conversion_Detail where  Doc_no='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            qry = "delete from TSPL_Item_Stock_Conversion_Head where  Doc_No='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
Public Class clsItemStockConveriosnDetail
    Public Doc_No As String = String.Empty
    Public UOM_Code As String = String.Empty
    Public UOM_DESC As String = String.Empty
    Public Conversion_factor As Double = 0
    Public Stocking_Unit As String = String.Empty
    Public Stock_In_Hand As Double = 0
    Public Required_UOM_Code As String = String.Empty
    Public Required_UOM_Desc As String = String.Empty
    Public Required_Qty As Double = 0
    Public Input_From_Qty As Double = 0
    Public Input_From_MRP As Double = 0
    Public Output_To_qty As Double = 0
    Public Output_To_MRP As Double = 0
    Public Output_Stock_Qty As Double = 0
    Public Output_Stock_MRP As Double = 0
    Public Remarks As String = String.Empty



    Public Shared Function SaveData(ByVal arr As List(Of clsItemStockConveriosnDetail), ByVal tran As SqlTransaction) As Boolean
        Try
            Dim i As Integer = 0
            Dim issaved As Boolean = True
            If arr.Count > 0 Then
                Dim qry As String = "delete from TSPL_Item_Stock_Conversion_Detail where  Doc_No='" & arr.Item(0).Doc_No & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, tran)
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_No", arr.Item(i).Doc_No)
                    clsCommon.AddColumnsForChange(coll, "UOM_Code", arr.Item(i).UOM_Code)
                    clsCommon.AddColumnsForChange(coll, "UOM_DESC", arr.Item(i).UOM_DESC)
                    clsCommon.AddColumnsForChange(coll, "Conversion_factor", arr.Item(i).Conversion_factor)
                    clsCommon.AddColumnsForChange(coll, "Stocking_Unit", arr.Item(i).Stocking_Unit)
                    clsCommon.AddColumnsForChange(coll, "Stock_In_Hand", arr.Item(i).Stock_In_Hand)
                    clsCommon.AddColumnsForChange(coll, "Required_UOM_Code", arr.Item(i).Required_UOM_Code)
                    clsCommon.AddColumnsForChange(coll, "Required_UOM_Desc", arr.Item(i).Required_UOM_Desc)
                    clsCommon.AddColumnsForChange(coll, "Required_Qty", arr.Item(i).Required_Qty)
                    clsCommon.AddColumnsForChange(coll, "Input_From_Qty", arr.Item(i).Input_From_Qty)
                    clsCommon.AddColumnsForChange(coll, "Output_To_qty", arr.Item(i).Output_To_qty)
                    clsCommon.AddColumnsForChange(coll, "Output_Stock_Qty", arr.Item(i).Output_Stock_Qty)
                    clsCommon.AddColumnsForChange(coll, "Remarks", arr.Item(i).Remarks)

                    clsCommon.AddColumnsForChange(coll, "Input_From_MRP", arr.Item(i).Input_From_MRP)
                    clsCommon.AddColumnsForChange(coll, "Output_To_MRP", arr.Item(i).Output_To_MRP)
                    clsCommon.AddColumnsForChange(coll, "Output_Stock_MRP", arr.Item(i).Output_Stock_MRP)

                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Item_Stock_Conversion_Detail", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getData(ByVal DocNo As String) As List(Of clsItemStockConveriosnDetail)
        Try
            Dim arr As New List(Of clsItemStockConveriosnDetail)
            Dim obj As New clsItemStockConveriosnDetail
            Dim q As String = "select * from TSPL_Item_Stock_Conversion_Detail where Doc_no='" & DocNo & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsItemStockConveriosnDetail
                    obj.Doc_No = clsCommon.myCstr(dtbl.Rows(i)("Doc_No"))
                    obj.UOM_Code = clsCommon.myCstr(dtbl.Rows(i)("UOM_Code"))
                    obj.UOM_DESC = clsCommon.myCstr(dtbl.Rows(i)("UOM_DESC"))
                    obj.Conversion_factor = clsCommon.myCdbl(dtbl.Rows(i)("Conversion_factor"))
                    obj.Stocking_Unit = clsCommon.myCstr(dtbl.Rows(i)("Stocking_Unit"))
                    obj.Stock_In_Hand = clsCommon.myCdbl(dtbl.Rows(i)("Stock_In_Hand"))
                    obj.Required_UOM_Code = clsCommon.myCstr(dtbl.Rows(i)("Required_UOM_Code"))
                    obj.Required_UOM_Desc = clsCommon.myCstr(dtbl.Rows(i)("Required_UOM_Desc"))
                    obj.Required_Qty = clsCommon.myCdbl(dtbl.Rows(i)("Required_Qty"))
                    obj.Input_From_Qty = clsCommon.myCdbl(dtbl.Rows(i)("Input_From_Qty"))
                    obj.Output_To_qty = clsCommon.myCdbl(dtbl.Rows(i)("Output_To_qty"))
                    obj.Output_Stock_Qty = clsCommon.myCdbl(dtbl.Rows(i)("Output_Stock_Qty"))
                    obj.Remarks = clsCommon.myCstr(dtbl.Rows(i)("Remarks"))

                    obj.Input_From_MRP = clsCommon.myCdbl(dtbl.Rows(i)("Input_From_MRP"))
                    obj.Output_To_MRP = clsCommon.myCdbl(dtbl.Rows(i)("Output_To_MRP"))
                    obj.Output_Stock_MRP = clsCommon.myCdbl(dtbl.Rows(i)("Output_Stock_MRP"))

                    arr.Add(obj)
                Next
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
