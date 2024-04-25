Imports System.Data.SqlClient
Public Class clsItemUpdateDetail
#Region "Variables"
    Public Item_Code As String = ""

    'Public Item_Name As String = ""
    'Public Item_Type As String = ""
    Public Purchase_Account_Set As String = ""
    Public Sale_Account_Set As String = ""
    Public Batch_Wise As Integer = 0
    'Public Fresh_Ambient As String = ""
    'Public Taxable As Integer = 0
    'Public MRP_Wise As Integer = 0

    Public Structure_Code As String = ""
    Public Weight_UOM As String = ""
    Public Weight_Value As Double = 0
    Public Stocking_UOM As String = ""
    Public Stocking_Conversion As Double = 0
    Public Default_UOM As String = ""
    Public Default_Conversion As Double = 0
    Public Weight_UOM1 As String = ""
    Public Weight_Conversion1 As Double = 0
    Public Weight_UOM2 As String = ""
    Public Weight_Conversion2 As Double = 0
    Public Other_UOM1 As String = ""
    Public Other_Conversion1 As Double = 0
    Public Other_UOM2 As String = ""
    Public Other_Conversion2 As Double = 0
    Public FatRate As Double = 0
    Public SNfRate As Double = 0
    Public Item_cost As Double = 0
    Public Product_Type As String = ""
    Public Cheapter_Heads As String = ""
#End Region



    Public Shared Function UpdateData(ByVal Arr As List(Of clsItemUpdateDetail)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String = ""
        Dim arrItem As New ArrayList
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsItemUpdateDetail In Arr
                    If clsCommon.myLen(obj.Item_Code) > 0 Then
                        arrItem.Add(obj.Item_Code)
                        If clsCommon.myLen(obj.Structure_Code) > 0 Then
                            ',Weight_UOM = '" & obj.Weight_UOM & "'
                            qry = "update tspl_item_master set Structure_Code='" & obj.Structure_Code & "',Weight_Value='" & obj.Weight_Value & "'" &
                                ",Purchase_Class_Code='" & obj.Purchase_Account_Set & "',Sale_Class_Code='" & obj.Sale_Account_Set & "',Is_Batch_Item='" & obj.Batch_Wise & "', Product_Type ='" & obj.Product_Type & "' , Cheapter_Heads = '" & obj.Cheapter_Heads & "' " &
                                " where item_code='" & obj.Item_Code & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.Stocking_UOM) > 0 Then
                            qry = "update TSPL_ITEM_UOM_DETAIL set conversion_factor='" & obj.Stocking_Conversion & "'" &
                          " where item_code='" & obj.Item_Code & "'" &
                          " and UOM_Code='" & obj.Stocking_UOM & "'" &
                          " and stocking_unit='Y'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.Default_UOM) > 0 Then
                            qry = "update TSPL_ITEM_UOM_DETAIL set conversion_factor='" & obj.Default_Conversion & "'" &
                                  " where item_code='" & obj.Item_Code & "'" &
                                  " and UOM_Code='" & obj.Default_UOM & "'" &
                                  " and Default_UOM=1 "
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        ''====Added by preeti  gupta against ticket no[BHA/18/09/18-000557]=========
                        If clsCommon.myLen(obj.Item_cost) > 0 Then
                            Dim StockUnitItemCost As Decimal = 0

                            qry = "select tspl_item_uom_detail.conversion_factor,stocking_unit,uom_code from tspl_item_uom_detail where item_code='" & obj.Item_Code & "' "
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                                For ii As Integer = 0 To dt.Rows.Count - 1
                                    If clsCommon.CompairString(dt.Rows(ii)("stocking_unit"), "Y") = CompairStringResult.Equal Then
                                        StockUnitItemCost = obj.Item_cost
                                        Exit For
                                    End If

                                Next
                                For ii As Integer = 0 To dt.Rows.Count - 1
                                    Dim ItemCost As Decimal = Math.Round(StockUnitItemCost * dt.Rows(ii)("conversion_factor"), 2, MidpointRounding.AwayFromZero)
                                    Dim Uom_Code As String = dt.Rows(ii)("uom_code")
                                    qry = "update tspl_item_uom_detail set item_cost='" & ItemCost & "' where item_code='" & obj.Item_Code & "' and Uom_Code='" & Uom_Code & "'"
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                Next

                            End If
                        End If


                        ''===================================


                        If clsCommon.myLen(obj.Weight_UOM1) > 0 Then
                            qry = "update TSPL_ITEM_UOM_DETAIL set conversion_factor='" & obj.Weight_Conversion1 & "'" &
                                " from tspl_item_uom_detail left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code = tspl_item_uom_detail.UOM_code " &
                                 " where item_code='" & obj.Item_Code & "'" &
                                  " and UOM_Code='" & obj.Weight_UOM1 & "'" &
                                  " and TSPL_UNIT_MASTER.Weight_Type='Y' " &
                                 " and tspl_item_uom_detail.Stocking_unit='N'" &
                                 " and tspl_item_uom_detail.Default_UOM=0"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.Weight_UOM2) > 0 Then
                            qry = "update TSPL_ITEM_UOM_DETAIL set conversion_factor='" & obj.Weight_Conversion2 & "'" &
                                " from tspl_item_uom_detail left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code = tspl_item_uom_detail.UOM_code " &
                                 " where item_code='" & obj.Item_Code & "'" &
                                  " and UOM_Code='" & obj.Weight_UOM2 & "'" &
                                  " and TSPL_UNIT_MASTER.Weight_Type='Y' " &
                                 " and tspl_item_uom_detail.Stocking_unit='N'" &
                                 " and tspl_item_uom_detail.Default_UOM=0"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.Other_UOM1) > 0 Then
                            qry = "update TSPL_ITEM_UOM_DETAIL set conversion_factor='" & obj.Other_Conversion1 & "'" &
                                " from tspl_item_uom_detail left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code = tspl_item_uom_detail.UOM_code " &
                                 " where item_code='" & obj.Item_Code & "'" &
                                  " and UOM_Code='" & obj.Other_UOM1 & "'" &
                                  " and TSPL_UNIT_MASTER.Weight_Type='N' " &
                                  " and tspl_item_uom_detail.Stocking_unit='N' " &
                                   " and tspl_item_uom_detail.Default_UOM=0 "
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.Other_UOM2) > 0 Then
                            qry = "update TSPL_ITEM_UOM_DETAIL set conversion_factor='" & obj.Other_Conversion2 & "'" &
                                " from tspl_item_uom_detail left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code = tspl_item_uom_detail.UOM_code " &
                                 " where item_code='" & obj.Item_Code & "'" &
                                  " and UOM_Code='" & obj.Other_UOM2 & "'" &
                                  " and TSPL_UNIT_MASTER.Weight_Type='N' " &
                                  " and tspl_item_uom_detail.Stocking_unit='N' " &
                                   " and tspl_item_uom_detail.Default_UOM=0 "
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.FatRate) > 0 Then
                            Dim check As String = clsDBFuncationality.getSingleValue("select TSPL_PARAMETER_MASTER.code from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_ITEM_QC_PARAMETER_MASTER.Code=TSPL_PARAMETER_MASTER.Code where Item_Code='" & obj.Item_Code & "'  and TSPL_PARAMETER_MASTER.Description='Fat %'", trans)
                            If clsCommon.myLen(check) > 0 Then
                                qry = "update TSPL_ITEM_QC_PARAMETER_MASTER set StandardRate='" & obj.FatRate & "'" &
                   " where item_code='" & obj.Item_Code & "'" &
                   " and Code='" & check & "'"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If

                        End If
                        If clsCommon.myLen(obj.SNfRate) > 0 Then
                            Dim check2 As String = clsDBFuncationality.getSingleValue("select TSPL_PARAMETER_MASTER.code from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_ITEM_QC_PARAMETER_MASTER.Code=TSPL_PARAMETER_MASTER.Code where Item_Code='" & obj.Item_Code & "'  and TSPL_PARAMETER_MASTER.Description='SNF %'", trans)
                            If clsCommon.myLen(check2) > 0 Then
                                qry = "update TSPL_ITEM_QC_PARAMETER_MASTER set StandardRate='" & obj.SNfRate & "'" &
                     " where item_code='" & obj.Item_Code & "'" &
                     " and Code='" & check2 & "'"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If

                        End If
                    End If
                Next
                ''By Balwinder for correct All item stock unit in Inventory Movement and Inventory Movement Against Ticket no-BHA/29/10/18-000645
                If arrItem IsNot Nothing AndAlso arrItem.Count > 0 Then
                    qry = "update TSPL_INVENTORY_MOVEMENT set Stock_UOM=xx.NewUOM  from TSPL_INVENTORY_MOVEMENT" + Environment.NewLine +
                     "inner join (" + Environment.NewLine +
                    "select TabstockUnit.UOM_Code as NewUOM,Stock_UOM as OLDUOM, TSPL_INVENTORY_MOVEMENT.* from TSPL_INVENTORY_MOVEMENT" + Environment.NewLine +
                    "left outer join (select Item_Code,UOM_Code from TSPL_ITEM_UOM_DETAIL where Stocking_Unit='Y') as TabstockUnit on TabstockUnit.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code" + Environment.NewLine +
                    "where UOM_Code<>Stock_UOM and TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(arrItem) + ") " + Environment.NewLine +
                    ")xx on xx.Trans_Id=TSPL_INVENTORY_MOVEMENT.Trans_Id and xx.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code and xx.Trans_Type=TSPL_INVENTORY_MOVEMENT.Trans_Type"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "update TSPL_INVENTORY_MOVEMENT set Stock_Qty=Qty*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_INVENTORY_MOVEMENT.UOM ) where TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(arrItem) + ") "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "update TSPL_INVENTORY_MOVEMENT_NEW set Stock_UOM=xx.NewUOM  from TSPL_INVENTORY_MOVEMENT_NEW" + Environment.NewLine +
                    "inner join (" + Environment.NewLine +
                    "select TabstockUnit.UOM_Code as NewUOM,Stock_UOM as OLDUOM, TSPL_INVENTORY_MOVEMENT_NEW.* from TSPL_INVENTORY_MOVEMENT_NEW" + Environment.NewLine +
                    "left outer join (select Item_Code,UOM_Code from TSPL_ITEM_UOM_DETAIL where Stocking_Unit='Y') as TabstockUnit on TabstockUnit.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code" + Environment.NewLine +
                    "where UOM_Code<>Stock_UOM and TSPL_INVENTORY_MOVEMENT_NEW.Item_Code in (" + clsCommon.GetMulcallString(arrItem) + ")" + Environment.NewLine +
                    ")xx on xx.Trans_Id=TSPL_INVENTORY_MOVEMENT_NEW.Trans_Id and xx.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code and xx.Trans_Type=TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "update TSPL_INVENTORY_MOVEMENT_NEW set Stock_Qty=Qty*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_INVENTORY_MOVEMENT_NEW.UOM ) where TSPL_INVENTORY_MOVEMENT_NEW.Item_Code in (" + clsCommon.GetMulcallString(arrItem) + ")"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
                ''End of By Balwinder for correct All item stock unit in Inventory Movement and Inventory Movement
            End If

            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
            Return False
        End Try
        Return True
    End Function

End Class