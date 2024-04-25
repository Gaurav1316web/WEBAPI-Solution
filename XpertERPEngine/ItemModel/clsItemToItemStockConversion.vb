Imports common
Imports System.Data.SqlClient

Public Class clsItemToItemStockConversion
    Public Doc_No As String = ""
    Public Doc_Date As Date
    Public Structure_Code As String
    Public Structure_Desc As String
    Public Location_Code As String
    Public Location_Desc As String
    Public IsPosted As Integer = 0
    Public Posting_Date As String = String.Empty
    Public Created_By As String = String.Empty
    Public Created_Date As String = String.Empty
    Public Modify_By As String = String.Empty
    Public Modify_Date As String = String.Empty
    Public comp_code As String = String.Empty

    Public arrFromDetail As List(Of clsItemToItemStockConveriosnFromDetail) = Nothing
    Public arrToDetail As List(Of clsItemToItemStockConveriosnToDetail) = Nothing
    Public isNewEntry As Boolean = False

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Try
            Dim str As String = ""
            Dim qry As String = " select TSPL_Item_To_Item_Stock_Conversion_Head.Doc_No as [DocNo] ,TSPL_Item_To_Item_Stock_Conversion_Head.Doc_Date as [Doc Date] ,TSPL_Item_To_Item_Stock_Conversion_Head.Structure_Code as [Structure Code] ,TSPL_Item_To_Item_Stock_Conversion_Head.Location_Code as [Location Code] ,TSPL_Item_To_Item_Stock_Conversion_Head.Location_Desc as [Location Desc] ,TSPL_Item_To_Item_Stock_Conversion_Head.IsPosted as [Isposted] ,TSPL_Item_To_Item_Stock_Conversion_Head.Posting_Date as [Posting Date] ,TSPL_Item_To_Item_Stock_Conversion_Head.Created_By as [Created By] ,TSPL_Item_To_Item_Stock_Conversion_Head.Created_Date as [Created Date] ,TSPL_Item_To_Item_Stock_Conversion_Head.Modify_By as [Modify By] ,TSPL_Item_To_Item_Stock_Conversion_Head.Modify_Date as [Modify Date] ,TSPL_Item_To_Item_Stock_Conversion_Head.comp_code as [Comp Code]  From TSPL_Item_To_Item_Stock_Conversion_Head "
            str = clsCommon.ShowSelectForm("ITMSTKCONV", qry, "DocNo", whrcls, curcode, "DocNo", isButtonClicked)
            Return str

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function SaveData(ByVal obj As clsItemToItemStockConversion, ByVal tran As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_No", obj.Doc_No)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Structure_Code", obj.Structure_Code)
            'clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Desc", obj.Location_Desc)
            clsCommon.AddColumnsForChange(coll, "isPosted", 0)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", obj.comp_code)
            clsCommon.AddColumnsForChange(coll, "Modify_By", obj.Modify_By)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", obj.Modify_Date)
            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", obj.Created_By)
                clsCommon.AddColumnsForChange(coll, "Created_Date", obj.Created_Date)
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Item_To_Item_Stock_Conversion_Head", OMInsertOrUpdate.Insert, "", tran)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Item_To_Item_Stock_Conversion_Head", OMInsertOrUpdate.Update, "TSPL_Item_To_Item_Stock_Conversion_Head.Doc_No='" + obj.Doc_No + "'", tran)
            End If
            issaved = issaved And clsItemToItemStockConveriosnFromDetail.SaveData(obj.Doc_No, obj.arrFromDetail, tran)
            issaved = issaved And clsItemToItemStockConveriosnToDetail.SaveData(obj.Doc_No, obj.arrToDetail, tran)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType) As clsItemToItemStockConversion
        Dim obj As New clsItemToItemStockConversion
        Try

            Dim whrCls As String = String.Empty
            obj.arrFromDetail = New List(Of clsItemToItemStockConveriosnFromDetail)
            obj.arrToDetail = New List(Of clsItemToItemStockConveriosnToDetail)
            Dim qst As String = " select TSPL_Item_To_Item_Stock_Conversion_Head.*,TSPL_STRUCTURE_MASTER.Structure_Descq as Structure_Desc   From TSPL_Item_To_Item_Stock_Conversion_Head left join TSPL_STRUCTURE_MASTER on TSPL_Item_To_Item_Stock_Conversion_Head.Structure_Code=TSPL_STRUCTURE_MASTER.Structure_Code  where 1=1 " & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_Item_To_Item_Stock_Conversion_Head.Doc_No in ('" + strCode + "')"
                Case NavigatorType.Next
                    qst += " and TSPL_Item_To_Item_Stock_Conversion_Head.Doc_No in (select min(Doc_No ) from TSPL_Item_To_Item_Stock_Conversion_Head where Doc_No  >'" + strCode + "' " & whrCls & ") "
                Case NavigatorType.First
                    qst += " and TSPL_Item_To_Item_Stock_Conversion_Head.Doc_No in (select MIN(Doc_No ) from TSPL_Item_To_Item_Stock_Conversion_Head where 1=1 " & whrCls & ") "
                Case NavigatorType.Last
                    qst += " and TSPL_Item_To_Item_Stock_Conversion_Head.Doc_No in (select Max(Doc_No ) from TSPL_Item_To_Item_Stock_Conversion_Head where 1=1 " & whrCls & ") "
                Case NavigatorType.Previous
                    qst += " and TSPL_Item_To_Item_Stock_Conversion_Head.Doc_No in (select Max(Doc_No ) from TSPL_Item_To_Item_Stock_Conversion_Head where Doc_No  <'" + strCode + "' " & whrCls & ") "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Doc_No = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
                obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
                obj.Structure_Code = clsCommon.myCstr(dt.Rows(0)("Structure_Code"))
                obj.Structure_Desc = clsCommon.myCstr(dt.Rows(0)("Structure_Desc"))
                obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
                obj.IsPosted = clsCommon.myCdbl(dt.Rows(0)("isPosted"))
                If obj.IsPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
                End If
                obj.arrFromDetail = clsItemToItemStockConveriosnFromDetail.getData(obj.Doc_No)
                obj.arrToDetail = clsItemToItemStockConveriosnToDetail.getData(obj.Doc_No)

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function


    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "select isposted from TSPL_Item_To_Item_Stock_Conversion_Head where Doc_No='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) = 1 Then
                Throw New Exception("transaction status should be posted for reverse and unpost")
            End If


            qry = "delete from tspl_inventory_movement where source_doc_no='" + strCode + "' and trans_type='ItemToItemStockConversion'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_Item_To_Item_Stock_Conversion_Head set isposted = 0 where Doc_No='" + strCode + "'"
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

            Dim obj As clsItemToItemStockConversion = clsItemToItemStockConversion.getData(strDocNo, NavigatorType.Current)
            trans = clsDBFuncationality.GetTransactin()
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Inventory", "Item To Item Stock Conversion", obj.Location_Code, obj.Doc_Date, trans)
            If (obj.IsPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            '--------------------
            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_Item_To_Item_Stock_Conversion_Head", "Doc_No", obj.Doc_No, trans)
            If isResult = False Then
                trans.Commit()
                Return False
            End If

            ''Adjustment Out Type document
            Dim objAdjOut As New ClsAdjustments
            objAdjOut.Adjustment_Date = obj.Doc_Date
            objAdjOut.Posting_Date = obj.Doc_Date
            objAdjOut.EntryDateTime = obj.Doc_Date
            objAdjOut.Against_Item_Stock_Conv_Doc = obj.Doc_No
            objAdjOut.Loc_Code = obj.Location_Code
            objAdjOut.Loc_Desc = obj.Location_Desc
            objAdjOut.Trans_Type = "Out"
            objAdjOut.Arr = New List(Of ClsAdjustmentsDetails)
            For Each objtr As clsItemToItemStockConveriosnFromDetail In obj.arrFromDetail
                Dim objAdjOutTR As New ClsAdjustmentsDetails()
                If objtr.Trans_Qty > 0 AndAlso clsCommon.myLen(objtr.UOM_Code) > 0 Then
                    objAdjOutTR.Item_Code = objtr.Item_Code
                    objAdjOutTR.Item_Description = objtr.Item_Desc
                    objAdjOutTR.Adjustment_Type = "QD"
                    objAdjOutTR.Item_Quantity = objtr.Trans_Qty
                    objAdjOutTR.Item_Cost = 0
                    objAdjOutTR.mrp = 0 'objtr.Input_From_MRP
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
            objAdjIn.Against_Item_Stock_Conv_Doc = obj.Doc_No
            objAdjIn.Loc_Code = obj.Location_Code
            objAdjIn.Loc_Desc = obj.Location_Desc
            objAdjIn.Trans_Type = "In"
            objAdjIn.Arr = New List(Of ClsAdjustmentsDetails)
            For Each objtr As clsItemToItemStockConveriosnToDetail In obj.arrToDetail
                If objtr.Trans_Qty > 0 AndAlso clsCommon.myLen(objtr.UOM_Code) > 0 Then
                    Dim objAdjInTR As New ClsAdjustmentsDetails()
                    objAdjInTR.Item_Code = objtr.Item_Code
                    objAdjInTR.Item_Description = objtr.Item_Desc
                    objAdjInTR.Adjustment_Type = "QI"
                    objAdjInTR.Item_Quantity = objtr.Trans_Qty
                    objAdjInTR.Item_Cost = 0
                    objAdjInTR.mrp = 0 'objtr.Output_To_MRP
                    objAdjInTR.Unit_Code = objtr.UOM_Code
                    objAdjIn.Arr.Add(objAdjInTR)
                End If
                'If objtr.Output_Stock_Qty > 0 Then
                '    Dim objAdjInTR As New ClsAdjustmentsDetails()
                '    objAdjInTR.Item_Code = obj.Item_Code
                '    objAdjInTR.Item_Description = obj.Item_Desc
                '    objAdjInTR.Adjustment_Type = "QI"
                '    objAdjInTR.Item_Quantity = objtr.Output_Stock_Qty
                '    objAdjInTR.Item_Cost = 0
                '    objAdjInTR.mrp = objtr.Output_Stock_MRP
                '    objAdjInTR.Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code  from TSPL_ITEM_UOM_DETAIL where Item_Code='" & obj.Item_Code & "' and Stocking_Unit='Y'", trans))
                '    objAdjIn.Arr.Add(objAdjInTR)
                'End If
            Next
            objAdjIn.SaveData(objAdjIn, True, "", trans)
            ClsAdjustments.PostData(objAdjIn.Adjustment_No, objAdjIn.Trans_Type, trans)
           
            Dim strQry As String = " update TSPL_Item_To_Item_Stock_Conversion_Head set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where doc_No='" & obj.Doc_No & "'"
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
            Dim qry As String = "delete from TSPL_Item_Stock_Conversion_From_Detail where  Doc_no='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "delete from TSPL_Item_Stock_Conversion_To_Detail where  Doc_no='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "delete from TSPL_Item_To_Item_Stock_Conversion_Head where  Doc_No='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
Public Class clsItemToItemStockConveriosnFromDetail
    Public Doc_No As String = String.Empty
    Public Line_No As Integer
    Public Item_Code As String = String.Empty
    Public Item_Desc As String = String.Empty
    Public UOM_Code As String = String.Empty
    Public UOM_DESC As String = String.Empty
    Public Conversion_factor As Double
    Public Stocking_Unit As String = String.Empty
    Public Stock_Unit As String = String.Empty
    Public Stock_In_Hand As Double = 0
    Public Trans_Qty As Double = 0
    Public Trans_Stock_Qty As Double = 0
    Public Remarks As String = String.Empty

    Public Shared Function SaveData(ByVal Doc_No As String, ByVal arr As List(Of clsItemToItemStockConveriosnFromDetail), ByVal tran As SqlTransaction) As Boolean
        Try
            'Dim i As Integer = 0
            Dim issaved As Boolean = True
            If arr.Count > 0 Then
                Dim qry As String = "delete from TSPL_Item_Stock_Conversion_From_Detail where  Doc_No='" & Doc_No & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, tran)
                For Each arrItem As clsItemToItemStockConveriosnFromDetail In arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_No", Doc_No)
                    clsCommon.AddColumnsForChange(coll, "Line_No", arrItem.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", arrItem.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Desc", arrItem.Item_Desc)
                    clsCommon.AddColumnsForChange(coll, "UOM_Code", arrItem.UOM_Code)
                    clsCommon.AddColumnsForChange(coll, "UOM_DESC", arrItem.UOM_DESC)
                    clsCommon.AddColumnsForChange(coll, "Conversion_factor", arrItem.Conversion_factor)
                    clsCommon.AddColumnsForChange(coll, "Stocking_Unit", arrItem.Stocking_Unit)
                    clsCommon.AddColumnsForChange(coll, "Stock_Unit", arrItem.Stock_Unit)
                    clsCommon.AddColumnsForChange(coll, "Stock_In_Hand", arrItem.Stock_In_Hand)
                    clsCommon.AddColumnsForChange(coll, "Trans_Qty", arrItem.Trans_Qty)
                    clsCommon.AddColumnsForChange(coll, "Trans_Stock_Qty", arrItem.Trans_Stock_Qty)
                    clsCommon.AddColumnsForChange(coll, "Remarks", arrItem.Remarks)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Item_Stock_Conversion_From_Detail", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getData(ByVal DocNo As String) As List(Of clsItemToItemStockConveriosnFromDetail)
        Try
            Dim arr As New List(Of clsItemToItemStockConveriosnFromDetail)
            Dim obj As New clsItemToItemStockConveriosnFromDetail
            Dim q As String = "select * from TSPL_Item_Stock_Conversion_From_Detail where Doc_no='" & DocNo & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For Each dr As DataRow In dtbl.Rows
                    obj = New clsItemToItemStockConveriosnFromDetail
                    obj.Doc_No = clsCommon.myCstr(dr("Doc_No"))
                    obj.Line_No = clsCommon.myCdbl(dr("Line_No"))
                    obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    obj.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    obj.UOM_Code = clsCommon.myCstr(dr("UOM_Code"))
                    obj.UOM_DESC = clsCommon.myCstr(dr("UOM_DESC"))
                    obj.Conversion_factor = clsCommon.myCdbl(dr("Conversion_factor"))
                    obj.Stocking_Unit = clsCommon.myCstr(dr("Stocking_Unit"))
                    obj.Stock_Unit = clsCommon.myCstr(dr("Stock_Unit"))
                    obj.Stock_In_Hand = clsCommon.myCdbl(dr("Stock_In_Hand"))
                    obj.Trans_Qty = clsCommon.myCdbl(dr("Trans_Qty"))
                    obj.Trans_Stock_Qty = clsCommon.myCdbl(dr("Trans_Stock_Qty"))
                    obj.Remarks = clsCommon.myCstr(dr("Remarks"))

                    arr.Add(obj)
                Next
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
Public Class clsItemToItemStockConveriosnToDetail
    Public Doc_No As String = String.Empty
    Public Line_No As Integer
    Public Item_Code As String = String.Empty
    Public Item_Desc As String = String.Empty
    Public UOM_Code As String = String.Empty
    Public UOM_DESC As String = String.Empty
    Public Conversion_factor As Double
    Public Stocking_Unit As String = String.Empty
    Public Stock_Unit As String = String.Empty
    Public Stock_In_Hand As Double = 0
    Public Trans_Qty As Double = 0
    Public Trans_Stock_Qty As Double = 0
    Public Remarks As String = String.Empty

    Public Shared Function SaveData(ByVal Doc_No As String, ByVal arr As List(Of clsItemToItemStockConveriosnToDetail), ByVal tran As SqlTransaction) As Boolean
        Try
            'Dim i As Integer = 0
            Dim issaved As Boolean = True
            If arr.Count > 0 Then
                Dim qry As String = "delete from TSPL_Item_Stock_Conversion_To_Detail where  Doc_No='" & Doc_No & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, tran)
                For Each arrItem As clsItemToItemStockConveriosnToDetail In arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_No", Doc_No)
                    clsCommon.AddColumnsForChange(coll, "Line_No", arrItem.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", arrItem.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Desc", arrItem.Item_Desc)
                    clsCommon.AddColumnsForChange(coll, "UOM_Code", arrItem.UOM_Code)
                    clsCommon.AddColumnsForChange(coll, "UOM_DESC", arrItem.UOM_DESC)
                    clsCommon.AddColumnsForChange(coll, "Conversion_factor", arrItem.Conversion_factor)
                    clsCommon.AddColumnsForChange(coll, "Stocking_Unit", arrItem.Stocking_Unit)
                    clsCommon.AddColumnsForChange(coll, "Stock_Unit", arrItem.Stock_Unit)
                    clsCommon.AddColumnsForChange(coll, "Stock_In_Hand", arrItem.Stock_In_Hand)
                    clsCommon.AddColumnsForChange(coll, "Trans_Qty", arrItem.Trans_Qty)
                    clsCommon.AddColumnsForChange(coll, "Trans_Stock_Qty", arrItem.Trans_Stock_Qty)
                    clsCommon.AddColumnsForChange(coll, "Remarks", arrItem.Remarks)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Item_Stock_Conversion_To_Detail", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getData(ByVal DocNo As String) As List(Of clsItemToItemStockConveriosnToDetail)
        Try
            Dim arr As New List(Of clsItemToItemStockConveriosnToDetail)
            Dim obj As New clsItemToItemStockConveriosnToDetail
            Dim q As String = "select * from TSPL_Item_Stock_Conversion_To_Detail where Doc_no='" & DocNo & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For Each dr As DataRow In dtbl.Rows
                    obj = New clsItemToItemStockConveriosnToDetail
                    obj.Doc_No = clsCommon.myCstr(dr("Doc_No"))
                    obj.Line_No = clsCommon.myCdbl(dr("Line_No"))
                    obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    obj.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    obj.UOM_Code = clsCommon.myCstr(dr("UOM_Code"))
                    obj.UOM_DESC = clsCommon.myCstr(dr("UOM_DESC"))
                    obj.Conversion_factor = clsCommon.myCdbl(dr("Conversion_factor"))
                    obj.Stocking_Unit = clsCommon.myCstr(dr("Stocking_Unit"))
                    obj.Stock_Unit = clsCommon.myCstr(dr("Stock_Unit"))
                    obj.Stock_In_Hand = clsCommon.myCdbl(dr("Stock_In_Hand"))
                    obj.Trans_Qty = clsCommon.myCdbl(dr("Trans_Qty"))
                    obj.Trans_Stock_Qty = clsCommon.myCdbl(dr("Trans_Stock_Qty"))
                    obj.Remarks = clsCommon.myCstr(dr("Remarks"))

                    arr.Add(obj)
                Next
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
