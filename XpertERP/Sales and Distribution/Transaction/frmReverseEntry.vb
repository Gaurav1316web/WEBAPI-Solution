'-Updation By--[Pankaj Kumar Chaudhary] Against Ticket No -[BM00000001629]
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports common
Imports System.IO
Public Class FrmReverseEntry
    Inherits FrmMainTranScreen
    Private Sub fndTransferNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTransferNo._MYValidating
        Dim qry As String = "select Quick_SettleMent_Id,Transfer_Number,Transfer_Date,Salesman,RouteNo,RouteDescription  from tspl_QuickSettleMent  "
        Dim WhrCls As String = "Post='Y' and Transfer_Number not in (select Transfer_No from TSPL_SHIPMENT_MASTER where Shipment_Type='Transfer')"

        fndTransferNo.Value = clsCommon.ShowSelectForm("QuickSettlementNo", qry, "Quick_SettleMent_Id", WhrCls, fndTransferNo.Value, "", isButtonClicked)
    End Sub



    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim Sql As String = ""
        Dim strQuickSettlementNo As String = ""
        'Dim con As SqlConnection
        Dim trans As SqlTransaction = Nothing
        trans = clsDBFuncationality.GetTransactin()
        Try

            If fndTransferNo.Value = "" Then
                MsgBox("Please select Quick settlement no", MsgBoxStyle.Information)
                Exit Sub
            End If

            strQuickSettlementNo = fndTransferNo.Value

            If MessageBox.Show("Are you sure to reverse Quick Settlement No '" & strQuickSettlementNo & "' ? ", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then


                Sql = "select Transfer_Number from tspl_QuickSettleMent where Quick_SettleMent_Id='" + strQuickSettlementNo + "'"
                Dim strTransferNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Sql, trans))

                Sql = "select Transfer_No from TSPL_TRANSFER_HEAD where Load_Out_No='" + strTransferNo + "'"
                Dim strLoadInNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Sql, trans))

                Sql = "select From_Location from TSPL_TRANSFER_HEAD where Transfer_No='" + strTransferNo + "'"
                Dim strLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Sql, trans))

                Sql = "select Adjustment_No from TSPL_ADJUSTMENT_HEADER where Document_No='" + strTransferNo + "'"
                Dim strAdjustmentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Sql, trans))

                Sql = "select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" + strLoadInNo + "'"
                Dim strVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Sql, trans))

                ''''' to insert into reverse tables

                Sql = "Insert into Tspl_Reverse_QuickSettleMent select * from tspl_QuickSettleMent where Quick_SettleMent_Id='" & strQuickSettlementNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "Insert into tspl_Revese_QuickSettleMent_Detail select * from tspl_QuickSettleMent_Detail where Quick_SettleMent_Id='" & strQuickSettlementNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "Insert into tspl_Reverse_QuickSettleMent_Item_Detail select * from tspl_QuickSettleMent_Item_Detail where Quick_SettleMent_Id='" & strQuickSettlementNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "Insert into TSPL_Reverse_TRANSFER_HEAD select * from TSPL_TRANSFER_HEAD where Transfer_No='" & strLoadInNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "Insert into TSPL_Reverse_TRANSFER_DETAIL select * from TSPL_TRANSFER_DETAIL where Transfer_No='" & strLoadInNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "Insert into TSPL_Reverse_ADJUSTMENT_HEADER select * from TSPL_ADJUSTMENT_HEADER where Adjustment_No='" & strAdjustmentNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "Insert into TSPL_Reverse_ADJUSTMENT_DETAIL select * from TSPL_ADJUSTMENT_DETAIL where Adjustment_No='" & strAdjustmentNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "Insert into TSPL_Reverse_INVENTORY_MOVEMENT(Trans_Type,InOut,Location_Code,Item_Code,Item_Desc,Qty,UOM,Source_Doc_No,Source_Doc_Date,Entry_Date,Basic_Cost,Rec_Cost,Add_Cost,Net_Cost,Created_By,Comp_Code,ItemType,Punching_Date,MRP,Batch_No) " & _
                "select Trans_Type,InOut,Location_Code,Item_Code,Item_Desc,Qty,UOM,Source_Doc_No,Source_Doc_Date,Entry_Date,Basic_Cost,Rec_Cost,Add_Cost,Net_Cost,Created_By,Comp_Code,ItemType,Punching_Date,MRP,Batch_No from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & strLoadInNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "Insert into TSPL_Reverse_JOURNAL_MASTER select * from TSPL_JOURNAL_MASTER where Source_Doc_No='" & strLoadInNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)


                Sql = "Insert into TSPL_Reverse_JOURNAL_DETAILS select * from TSPL_JOURNAL_DETAILS where Voucher_No='" & strVoucherNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)


                '''''' To insert Reverse Details


                Dim strReverseNo As String
                strReverseNo = clsERPFuncationality.GetNextCode(trans, dtpReverseDate.Value.Date, clsDocType.ReveseNo, "", "")
                Sql = "Insert into TSPL_SETTLEMENT_REVERSE_MAPPING(Reverse_DocNo,QuickSettlementNo,Transfer_LoadOutno,Transfer_LoadIntno,Adjustment_No,Location,ReverseDate,CreatedBy,GLVoucher_No) values ('" & strReverseNo & "','" & strQuickSettlementNo & "','" & strTransferNo & "','" & strLoadInNo & "','" & strAdjustmentNo & "','" & strLocation & "','" & clsCommon.GetPrintDate(dtpReverseDate.Value, "dd/MMM/yyyy hh:mm tt") & "','" & objCommonVar.CurrentUserCode & "','" & strVoucherNo & "')"
                connectSql.RunSqlTransaction(trans, Sql)

                '''''' To Update LoadOut
                Sql = "update TSPL_TRANSFER_HEAD set Quick_Settlement='N' where Transfer_No='" & strTransferNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "update TSPL_TRANSFER_DETAIL set Pending_Qty=Item_Qty,Pending_Balance_In_Bottle=Item_Qty * (select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code  and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB')  where Transfer_No='" & strTransferNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "update TEMP_PROVISIONAL_SALES set LoadInQty=0,Shortage=0,Leak=0,Breakage=0,Amount=0,LoadIn_EmptyValue=0,Post='N' where Transfer_No='" & strTransferNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                '''' To update item location

                Dim decQty, decCost, decmrp As Decimal
                Dim strItem, strLoc As String


                '' For To location
                Sql = "select  sum(Qty) as qty,sum(Cost) as cost,Item_Code,location,MRP from ( " & _
                "select LoadIn_Qty/Conversion_Factor as Qty,case when Uom <> 'SH' then " & _
                "LoadIn_Qty/Conversion_Factor  * (select Item_price from TSPL_TRANSFER_DETAIL a where " & _
                "a.Transfer_No=TSPL_TRANSFER_head.Load_Out_No and a.Item_code=TSPL_TRANSFER_DETAIL.item_code and uom='fc' and a.MRP =TSPL_TRANSFER_DETAIL.MRP ) else " & _
                "LoadIn_Qty * Item_Price   end as Cost,TSPL_TRANSFER_DETAIL.Item_Code,To_Location as location,MRP * Conversion_Factor as MRP  from TSPL_TRANSFER_HEAD inner join TSPL_TRANSFER_DETAIL on " & _
                "TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No inner join TSPL_ITEM_UOM_DETAIL on " & _
                "TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_TRANSFER_DETAIL.Uom=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "where TSPL_TRANSFER_DETAIL.Transfer_No='" & strLoadInNo & "'" & _
                ") a  group by Item_Code,location,MRP"

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Sql, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        decQty = clsCommon.myCdbl(dr("Qty"))
                        decCost = clsCommon.myCdbl(dr("cost"))
                        strItem = clsCommon.myCstr(dr("Item_Code"))
                        strLoc = clsCommon.myCstr(dr("location"))
                        decmrp = clsCommon.myCdbl(dr("MRP"))

                        Sql = "select top 1 Item_Qty as Qty,Amount,Batch_No,ItemType from TSPL_ITEM_LOCATION_DETAILS " & _
                    "where Item_Code='" & strItem & "' and Location_Code='" & strLoc & "' and MRP=" & decmrp & "  and Item_Qty >  " & decQty & " "

                        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Sql, trans)
                        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                            For Each dr1 As DataRow In dt1.Rows
                                If IsDBNull(clsCommon.myCstr(dr1("Batch_No").ToString())) = False Then
                                    Sql = "Update TSPL_ITEM_LOCATION_DETAILS set Item_Qty=" & clsCommon.myCdbl(dr1("Qty")) & " - " & decQty & ",Amount=" & clsCommon.myCdbl(dr1("Amount")) & " - " & decCost & "  " & _
                               "where Item_Code='" & strItem & "' and Location_Code='" & strLoc & "' and MRP=" & decmrp & " and Batch_No='" & clsCommon.myCstr(dr1("Batch_No").ToString()) & "' and " & _
                               "ItemType='" & clsCommon.myCstr(dr1("ItemType").ToString()) & "' "
                                Else
                                    Sql = "Update TSPL_ITEM_LOCATION_DETAILS set Item_Qty=" & clsCommon.myCdbl(dr1("Qty")) & " - " & decQty & ",Amount=" & clsCommon.myCdbl(dr1("Amount")) & " - " & decCost & "  " & _
                               "where Item_Code='" & strItem & "' and Location_Code='" & strLoc & "' and MRP=" & decmrp & "  ItemType='" & clsCommon.myCstr(dr1("ItemType").ToString()) & "' "
                                End If

                                clsDBFuncationality.ExecuteNonQuery(Sql, trans)
                            Next

                        End If
                    Next
                End If


                '' For From location
                Sql = "select  sum(Qty) as qty,sum(Cost) as cost,Item_Code,location,MRP from ( " & _
                "select LoadIn_Qty/Conversion_Factor as Qty,case when Uom <> 'SH' then " & _
                "LoadIn_Qty/Conversion_Factor  * (select Item_price from TSPL_TRANSFER_DETAIL a where " & _
                "a.Transfer_No=TSPL_TRANSFER_head.Load_Out_No and a.Item_code=TSPL_TRANSFER_DETAIL.item_code and uom='fc' and a.MRP =TSPL_TRANSFER_DETAIL.MRP ) else " & _
                "LoadIn_Qty * Item_Price   end as Cost,TSPL_TRANSFER_DETAIL.Item_Code,From_Location as location,MRP * Conversion_Factor as MRP  from TSPL_TRANSFER_HEAD inner join TSPL_TRANSFER_DETAIL on " & _
                "TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No inner join TSPL_ITEM_UOM_DETAIL on " & _
                "TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_TRANSFER_DETAIL.Uom=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "where TSPL_TRANSFER_DETAIL.Transfer_No='" & strLoadInNo & "'" & _
                ") a  group by Item_Code,location,MRP"

                dt = clsDBFuncationality.GetDataTable(Sql, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        decQty = clsCommon.myCdbl(dr("Qty"))
                        decCost = clsCommon.myCdbl(dr("cost"))
                        strItem = clsCommon.myCstr(dr("Item_Code"))
                        strLoc = clsCommon.myCstr(dr("location"))
                        decmrp = clsCommon.myCdbl(dr("MRP"))

                        Sql = "select top 1 Item_Qty as Qty,Amount,Batch_No,ItemType from TSPL_ITEM_LOCATION_DETAILS " & _
                    "where Item_Code='" & strItem & "' and Location_Code='" & strLoc & "' and MRP=" & decmrp & "  and Item_Qty >  " & decQty & " "

                        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Sql, trans)
                        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                            For Each dr1 As DataRow In dt1.Rows
                                If IsDBNull(clsCommon.myCstr(dr1("Batch_No").ToString())) = False Then
                                    Sql = "Update TSPL_ITEM_LOCATION_DETAILS set Item_Qty=" & clsCommon.myCdbl(dr1("Qty")) & " + " & decQty & ",Amount=" & clsCommon.myCdbl(dr1("Amount")) & " - " & decCost & "  " & _
                               "where Item_Code='" & strItem & "' and Location_Code='" & strLoc & "' and MRP=" & decmrp & " and Batch_No='" & clsCommon.myCstr(dr1("Batch_No").ToString()) & "' and " & _
                               "ItemType='" & clsCommon.myCstr(dr1("ItemType").ToString()) & "' "
                                Else
                                    Sql = "Update TSPL_ITEM_LOCATION_DETAILS set Item_Qty=" & clsCommon.myCdbl(dr1("Qty")) & " + " & decQty & ",Amount=" & clsCommon.myCdbl(dr1("Amount")) & " - " & decCost & "  " & _
                               "where Item_Code='" & strItem & "' and Location_Code='" & strLoc & "' and MRP=" & decmrp & "  ItemType='" & clsCommon.myCstr(dr1("ItemType").ToString()) & "' "
                                End If

                                clsDBFuncationality.ExecuteNonQuery(Sql, trans)
                            Next

                        End If
                    Next
                End If


                '''''  to delete 

                Sql = "delete from tspl_QuickSettleMent_Item_Detail where Quick_SettleMent_Id='" & strQuickSettlementNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "delete from tspl_QuickSettleMent_Detail where Quick_SettleMent_Id='" & strQuickSettlementNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "delete from  tspl_QuickSettleMent where Quick_SettleMent_Id='" & strQuickSettlementNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "delete from TSPL_TRANSFER_DETAIL where Transfer_No='" & strLoadInNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "delete from TSPL_TRANSFER_HEAD where Transfer_No='" & strLoadInNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "delete from TSPL_ADJUSTMENT_DETAIL where Adjustment_No='" & strAdjustmentNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "delete from TSPL_ADJUSTMENT_HEADER where Adjustment_No='" & strAdjustmentNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & strLoadInNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "delete from TSPL_JOURNAL_DETAILS where Voucher_No='" & strVoucherNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                Sql = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No='" & strLoadInNo & "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                trans.Commit()

                MsgBox("Reverse Entry created Successfully", MsgBoxStyle.Information, Me.Text)
                fndTransferNo.Value = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
            If trans IsNot Nothing Then
                trans.Rollback()
            End If

        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmReverseEntry_ContextMenuChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ContextMenuChanged

    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.ReverseEntry)

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub
    Private Sub FrmReverseEntry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dtpReverseDate.Value = clsCommon.GETSERVERDATE()
        SetUserMgmtNew()

    End Sub
End Class
