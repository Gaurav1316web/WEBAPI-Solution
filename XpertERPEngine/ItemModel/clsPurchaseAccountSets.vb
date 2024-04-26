Imports System.Data.SqlClient

Public Class clsPurchaseAccountSets
#Region "Variables"
    Public colStructureCode As String = ""
    Public colItemCode As String = ""
    Public colItemType As String = ""
    Public colItemTypeDesc As String = ""
    Public colAccountCode As String = ""
    Public colInventory As String = ""
    Public colInventoryDesc As String = ""
    Public colPayableClearing As String = ""
    Public colPayableClearingDesc As String = ""
    Public ColShipment As String = ""
    Public ColShipmentDesc As String = ""
    Public colAdj As String = ""
    Public colAdjDesc As String = ""
    Public colFGShortage As String = ""
    Public colFGShortageDesc As String = ""
    Public colBreakage As String = ""
    Public colBreakageDesc As String = ""
    Public colChillingCharges As String = ""
    Public colChillingChargesDesc As String = ""
    Public colCreditDebitNote As String = ""
    Public colCreditDebitNoteDesc As String = ""
    Public colDifferenceAccount As String = ""
    Public colDifferenceAccountDesc As String = ""
    Public colDisassembly As String = ""
    Public colDisassemblyDesc As String = ""
    Public colFAAccount As String = ""
    Public colFAAccountDesc As String = ""
    Public colFrieghtCharges As String = ""
    Public colFrieghtChargesDesc As String = ""
    Public colHandlingCharges As String = ""
    Public colHandlingChargesDesc As String = ""
    Public colJobWorkAC As String = ""
    Public colJobWorkACDesc As String = ""
    Public colLossAccount As String = ""
    Public colLossAccountDesc As String = ""
    Public colInvControlEmpties As String = ""
    Public colInvControlEmptiesDesc As String = ""
    Public colRejected As String = ""
    Public colRejectedDesc As String = ""
    Public colShortage As String = ""
    Public colShortageDesc As String = ""
    Public colPhyisalInvAdj As String = ""
    Public colPhyisalInvAdjDesc As String = ""
    Public colProvision As String = ""
    Public colProvisionDesc As String = ""
    Public colPurchaseAccount As String = ""
    Public colPurchaseAccountDesc As String = ""
    Public colPurchaseControl As String = ""
    Public colPurchaseControlDesc As String = ""
    Public colPurchaseJobWork As String = ""
    Public colPurchaseJobWorkDesc As String = ""
    Public colPurchaseLoss As String = ""
    Public colPurchaseLossDesc As String = ""
    Public colPurchaseSetOff As String = ""
    Public colPurchaseSetOffDesc As String = ""
    Public colRGPClearing As String = ""
    Public colRGPClearingDesc As String = ""
    Public colRM As String = ""
    Public colRMDesc As String = ""
    Public colStockTrasnfer As String = ""
    Public colStockTrasnferDesc As String = ""
    Public colStockTrasnferIn As String = ""
    Public colStockTrasnferInDesc As String = ""
    Public colJobWork As String = ""
    Public colJobWorkDesc As String = ""
    Public colStoreConsumption As String = ""
    Public colStoreConsumptionDesc As String = ""
    Public colTransferClearing As String = ""
    Public colTransferClearingDesc As String = ""
    Public colGainLossAccount As String = ""
    Public colGainLossAccountDesc As String = ""
    Public colWIPAccount As String = ""
    Public colWIPAccountDesc As String = ""
    Public colWreckage As String = ""
    Public colWreckageDesc As String = ""
    Public ConsignmentAc As String = ""
    Public ConsignmentDesc As String = ""
#End Region
    Public Shared Function UpdateData(ByVal Arr As List(Of clsPurchaseAccountSets)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsPurchaseAccountSets In Arr
                    If clsCommon.myLen(obj.colItemCode) > 0 Then
                        Dim qry As String = ""
                        If clsCommon.myLen(obj.colInventory) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Inv_Control_Account='" & obj.colInventory & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.colPayableClearing) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Inv_Payable_Clearing='" & obj.colPayableClearing & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.colAdj) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Adjustment_Account='" & obj.colAdj & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.colInvControlEmpties) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Non_Stock_Clearing='" & obj.colInvControlEmpties & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.colTransferClearing) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Transfer_Clearing='" & obj.colTransferClearing & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.ColShipment) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Shipment_Clearing='" & obj.ColShipment & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colDisassembly) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Disassembly_Expense='" & obj.colDisassembly & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colPhyisalInvAdj) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Physical_Inv_Adjustment='" & obj.colPhyisalInvAdj & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colCreditDebitNote) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Credit_Debit_Note_Clearing='" & obj.colCreditDebitNote & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colRGPClearing) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Reserve_Stock='" & obj.colRGPClearing & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colBreakage) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Breakage_Gl_Account='" & obj.colBreakage & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colWIPAccount) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set WIP_Account='" & obj.colWIPAccount & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colRM) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set RM_Consumption='" & obj.colRM & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colRejected) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Other_1='" & obj.colRejected & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colShortage) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Other_2='" & obj.colShortage & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colLossAccount) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Loss_Ac='" & obj.colLossAccount & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colPurchaseControl) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Purchase_Control_Account='" & obj.colPurchaseControl & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colGainLossAccount) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Transfer_Gain_Loss_Ac='" & obj.colGainLossAccount & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colJobWorkAC) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Job_Work_Ac='" & obj.colJobWorkAC & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colStockTrasnferIn) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Stock_Transfer_In='" & obj.colStockTrasnferIn & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colStockTrasnfer) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Stock_Transfer_Acc='" & obj.colStockTrasnfer & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colProvision) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Provision_Clearing='" & obj.colProvision & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colChillingCharges) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Chilling_Charges='" & obj.colChillingCharges & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colFrieghtCharges) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Freight_Charges='" & obj.colFrieghtCharges & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colPurchaseAccount) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Purchase_Account='" & obj.colPurchaseAccount & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colPurchaseSetOff) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Purchase_Set_Off='" & obj.colPurchaseSetOff & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colPurchaseJobWork) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Purchase_JobWork='" & obj.colPurchaseJobWork & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colDifferenceAccount) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Difference_Account='" & obj.colDifferenceAccount & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colJobWork) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Stock_Transfer_JobWork='" & obj.colJobWork & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colHandlingCharges) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Handling_Charge='" & obj.colHandlingCharges & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colStoreConsumption) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Store_Consumption_Acc='" & obj.colStoreConsumption & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colFAAccount) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set FA_CLEARING_AC='" & obj.colFAAccount & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colPurchaseLoss) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Purchase_Loss='" & obj.colPurchaseLoss & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        If clsCommon.myLen(obj.colWreckage) > 0 Then
                            qry = "update TSPL_PURCHASE_ACCOUNTS set Wrekage_Account='" & obj.colWreckage & "'" &
                                 " where Purchase_Class_Code='" & obj.colAccountCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                        If clsCommon.myLen(obj.ConsignmentAc) > 0 Then
                            qry = "update TSPL_ITEM_MASTER set GL_Account='" & obj.ConsignmentAc & "'" &
                                 " where ITEM_CODE='" & obj.colItemCode & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                    End If
                Next
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
