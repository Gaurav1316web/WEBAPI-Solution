Imports common
Imports System.Data.SqlClient
Public Class clsSRNReturn
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime
    Public SRN_No As String = Nothing
    Public Remarks As String = Nothing

    Public Bill_To_Location As String = Nothing ''Not a Table field
    Public Form_ID As String = Nothing
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing
#End Region
    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_SRN_RETURN", "Document_No", trans)
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsSRNReturn, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try

            Dim objSRN As clsSRNHead = clsSRNHead.GetData(obj.SRN_No, NavigatorType.Current)
            AllowToSave(obj, objSRN)
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Store Received Note Return", obj.Bill_To_Location, Document_Date, trans)
            Try
                If isNewEntry Then
                    obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Document_Date), clsDocType.SRNReturn, "", obj.Bill_To_Location)
                End If
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "SRN_No", obj.SRN_No)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                If isNewEntry Then
                    clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SRN_RETURN", OMInsertOrUpdate.Insert, "", trans)
                Else
                    HistoryUpdate(obj.Document_No, trans)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SRN_RETURN", OMInsertOrUpdate.Update, "TSPL_SRN_RETURN.Document_No='" + obj.Document_No + "'", trans)
                End If
                isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Document_No, obj.arrCustomFields, trans)

                ''Revese Inventory movement
                Dim count As Integer = 1
                Dim qry As String = "select * from TSPL_INVENTORY_MOVEMENT where Trans_Type='SRN' and Source_Doc_No='" + obj.SRN_No + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim ArrInventoryMovement As New List(Of clsInventoryMovement)
                    Dim objInvMov As clsInventoryMovement
                    For Each dr As DataRow In dt.Rows
                        objInvMov = New clsInventoryMovement
                        objInvMov.InOut = "O"
                        objInvMov.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                        objInvMov.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objInvMov.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                        objInvMov.Qty = clsCommon.myCstr(dr("Qty"))
                        objInvMov.UOM = clsCommon.myCstr(dr("UOM"))
                        '' check balance 
                        Dim Bal As Decimal = clsItemLocationDetails.getBalance(objInvMov.Item_Code, objInvMov.Location_Code, obj.Document_No, obj.Document_Date, trans, objInvMov.UOM, 0)
                        If Bal < objInvMov.Qty Then
                            Throw New Exception("Item Code: " & objInvMov.Item_Code & " Location: " & objInvMov.Location_Code & " Return Qty: " & objInvMov.Qty & " Available Qty: " & Bal & " " & objInvMov.UOM & " ")
                        End If
                        objInvMov.Source_Doc_No = obj.Document_No
                        objInvMov.Source_Doc_Date = obj.Document_Date
                        objInvMov.Entry_Date = clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy")
                        objInvMov.Basic_Cost = clsCommon.myCdbl(dr("Basic_Cost"))
                        objInvMov.Rec_Cost = clsCommon.myCdbl(dr("Rec_Cost"))
                        objInvMov.Add_Cost = clsCommon.myCdbl(dr("Add_Cost"))
                        objInvMov.Net_Cost = clsCommon.myCdbl(dr("Net_Cost"))
                        objInvMov.ItemType = clsCommon.myCstr(dr("ItemType"))
                        objInvMov.Punching_Date = obj.Document_Date
                        objInvMov.MRP = clsCommon.myCdbl(dr("MRP"))
                        objInvMov.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                        objInvMov.FIFO_Cost = clsCommon.myCdbl(dr("FIFO_Cost"))
                        objInvMov.LIFO_Cost = clsCommon.myCdbl(dr("LIFO_Cost"))
                        objInvMov.Avg_Cost = clsCommon.myCdbl(dr("Avg_Cost"))
                        objInvMov.Posting_Date = obj.Document_Date
                        objInvMov.Stock_UOM = clsCommon.myCstr(dr("Stock_UOM"))
                        objInvMov.Stock_Qty = clsCommon.myCdbl(dr("Stock_Qty"))
                        If dr("MFG_Date") IsNot DBNull.Value Then
                            objInvMov.MFG_Date = clsCommon.myCstr(dr("MFG_Date"))
                        End If
                        If dr("Expiry_Date") IsNot DBNull.Value Then
                            objInvMov.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
                        End If
                        objInvMov.IS_CONSUMPTION = clsCommon.myCdbl(dr("IS_CONSUMPTION"))
                        objInvMov.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                        objInvMov.Cust_Name = clsCommon.myCstr(dr("Cust_Name"))
                        objInvMov.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                        objInvMov.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))
                        objInvMov.Other_Location_Code = clsCommon.myCstr(dr("Other_Location_Code"))
                        objInvMov.Other_Location_Desc = clsCommon.myCstr(dr("Other_Location_Desc"))
                        'richa agarwal 7 Jan,2019 BHA/27/11/18-000729 inventory movement work 
                        objInvMov.Inventory_CrAcc = clsCommon.myCstr(dr("Inventory_DrAcc"))
                        objInvMov.Inventory_DrAcc = clsCommon.myCstr(dr("Inventory_CrAcc"))
                        '--------------------
                        '' Work done by sanjay on Against ticket no.TEC/05/02/19-000414 
                        'Dim item_Purchase_Class As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" & objInvMov.Item_Code & "'", trans))
                        'Dim qry1 As String = "select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" + objInvMov.Location_Code + "'"
                        'Dim strLocatinSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))
                        'If clsCommon.myLen(item_Purchase_Class) > 0 Then
                        '    Dim Inventory_Purchase_code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Inv_Control_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code='" & item_Purchase_Class & "'", trans))
                        '    If clsCommon.myLen(Inventory_Purchase_code) > 0 Then
                        '        objInvMov.Inventory_CrAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Purchase_code, strLocatinSegment, trans)
                        '    End If
                        'End If
                        '' end


                        obj.arrSrItem = clsSerializeInvenotry.GetData("Transfer", obj.SRN_No, objInvMov.Item_Code, count, trans)
                        ArrInventoryMovement.Add(objInvMov)
                        clsSerializeInvenotry.SaveData("SRN-RET", obj.Document_No, obj.Document_Date, "O", objInvMov.Item_Code, objInvMov.Location_Code, count, obj.arrSrItem, trans)
                        count = count + 1
                    Next
                    ' '' save serials of srn return
                    'Dim arrSerial As New List(Of clsSerializeInvenotry)
                    'Dim objSr As New clsSerializeInvenotry
                    'qry = " select Code,Parent_Line_No,Line_No,Auto_Sr_No,Item_Code,Document_Code,Document_Date,'SRN-RET','O',Location_Code,QC_Complete,Auto_Bin_No " & _
                    '      " from TSPL_SERIAL_ITEM where Document_Type='SRN' and Document_Code='" & obj.SRN_No & "'"
                    'Dim dtSerial As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    'For Each drSerial As DataRow In dtSerial.Rows
                    '    arrSerial = New List(Of clsSerializeInvenotry)
                    '    objSr.Parent_Line_No = clsCommon.myCdbl(drSerial.Item("Parent_Line_No"))
                    '    objSr.Line_No = clsCommon.myCdbl(drSerial.Item("Line_No"))
                    '    objSr.Auto_Sr_No = clsCommon.myCstr(drSerial.Item("Auto_Sr_No"))
                    '    objSr.Item_Code = clsCommon.myCstr(drSerial.Item("Item_Code"))
                    '    objSr.Document_Code = clsCommon.myCstr(obj.Document_No)
                    '    objSr.Document_Date = obj.Document_Date
                    '    objSr.Document_Type = "SRN-RET"
                    '    objSr.Location_Code = clsCommon.myCstr(drSerial.Item("Location_Code"))
                    '    objSr.Allow_QC = clsCommon.myCstr(drSerial.Item("QC_Complete"))
                    '    objSr.Auto_BIN_No = clsCommon.myCstr(drSerial.Item("Auto_Bin_No"))
                    '    arrSerial.Add(objSr)
                    '    clsSerializeInvenotry.SaveData("SRN-RET", obj.Document_No, obj.Document_Date, "O", objSr.Item_Code, objSr.Location_Code, objSr.Parent_Line_No, arrSerial, trans)
                    'Next
                    '' save srn return inventory
                    isSaved = isSaved AndAlso clsInventoryMovement.SaveData("SRN-RET", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                End If
                ''Journal Entry
                qry = " select TSPL_JOURNAL_DETAILS.Account_code,-1*TSPL_JOURNAL_DETAILS.Amount as Amount,TSPL_JOURNAL_DETAILS.Reco_Control_Account from TSPL_JOURNAL_DETAILS " & _
                " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" & _
                " where TSPL_JOURNAL_MASTER.Source_Doc_No='" + obj.SRN_No + "'  and Source_Code in ('SR-RG','PO-RC')"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim ArryLstGLAC As ArrayList = New ArrayList()
                    For Each dr As DataRow In dt.Rows
                        ''richa agarwal 7 Jan,2019 BHA/27/11/18-000729 inventory movement work 
                        If clsCommon.myLen(dr("Reco_Control_Account")) > 0 Then
                            Dim Acc() As String = {clsCommon.myCstr(dr("Account_code")), clsCommon.myCdbl(dr("Amount")), "", "", "", "", "", "", dr("Reco_Control_Account")}
                            ArryLstGLAC.Add(Acc)
                        Else
                            Dim Acc() As String = {clsCommon.myCstr(dr("Account_code")), clsCommon.myCdbl(dr("Amount"))}
                            ArryLstGLAC.Add(Acc)
                        End If
                        ''-------------------------------
                    Next

                    transportSql.FunGrnlEntryWithTrans(obj.Bill_To_Location, False, trans, obj.Document_Date, "Against SRN Return " + obj.Document_No, "SN-RT", "Store Received Note Return", obj.Document_No, obj.Remarks, "V", objSRN.Vendor_Code, objSRN.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
                End If

                'isSaved = isSaved AndAlso CancelDocs(obj.Document_No, trans)

                qry = "select TRANSFER_NO from TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD where AgainstSRN_No='" + obj.SRN_No + "' "
                Dim strJWOTransfer As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                If clsCommon.myLen(strJWOTransfer) > 0 Then
                    Dim objJWOTOR As New clsJWOTransferOtherReturn
                    objJWOTOR.JWO_Transfer_No = strJWOTransfer
                    objJWOTOR.Document_Date = obj.Document_Date
                    objJWOTOR.Remarks = "Auto Generanted by SRN Return No " + obj.Document_No
                    objJWOTOR.JWO_SRN_From_Location_Code = objSRN.Bill_To_Location
                    objJWOTOR.SaveData(objJWOTOR, True, trans)
                End If

                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function CancelDocs(ByVal SRNReturnNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim CanceDocDueToSrnReturn As Boolean = False
        Dim qry As String = Nothing
        Dim SRNNo As String = Nothing
        Dim MRNNo As DataTable = New DataTable()
        CanceDocDueToSrnReturn = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CancelDocDueToSRNReturn, clsFixedParameterCode.CancelDocDueToSRNReturn, Nothing)) = "1", True, False))
        If CanceDocDueToSrnReturn Then
            qry = "select srn_no from TSPL_SRN_RETURN where Document_No='" + clsCommon.myCstr(SRNReturnNo) + "'"
            SRNNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(SRNNo) > 0 Then
                qry = "update TSPL_SRN_HEAD set IsCancel=1 where SRN_No='" + clsCommon.myCstr(SRNNo) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "select distinct (CASE WHEN ISNULL(TSPL_SRN_DETAIL.MRN_Id,'')='' THEN TSPL_SRN_HEAD.Against_MRN ELSE TSPL_SRN_DETAIL.MRN_Id END) AS MRNNo from TSPL_SRN_DETAIL LEFT OUTER JOIN TSPL_SRN_HEAD ON TSPL_SRN_DETAIL.SRN_No=TSPL_SRN_HEAD.SRN_No where TSPL_SRN_HEAD.SRN_No='" + clsCommon.myCstr(SRNNo) + "' AND isnull(TSPL_SRN_HEAD.Against_MRN,'')<>''"
                MRNNo = clsDBFuncationality.GetDataTable(qry, trans)
                If MRNNo.Rows.Count > 0 Then
                    qry = ""

                End If
            End If
        End If
        Return True
    End Function
    Public Shared Function UnpostData(ByVal Doc_No As String) As Boolean
        'Dim isSaved As Boolean = True
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        'Dim obj As clsSRNReturn = clsSRNReturn.GetData(Doc_No, NavigatorType.Current, trans)
        'Try
        '    If Not (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
        '        Throw New Exception("Document not found")
        '    End If
        '    Dim qry As String = "delete from TSPL_INVENTORY_MOVEMENT where SOURCE_DOC_NO='" & obj.Document_No & "'"
        '    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        '    qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where SOURCE_DOC_NO='" & obj.Document_No & "'"
        '    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        '    trans.Commit()
        'Catch ex As Exception
        '    trans.Rollback()
        '    Throw New Exception(ex.Message)
        'End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal Doc_No As String) As Boolean
        Dim qry As String
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As clsSRNReturn = clsSRNReturn.GetData(Doc_No, NavigatorType.Current, trans)
        Try
            If Not (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                Throw New Exception("Document not found")
            End If

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='SN-RT' and Source_Doc_No='" + obj.Document_No + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            clsSerializeInvenotry.DeleteData("SRN-RET", obj.Document_No, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Document_No), "TSPL_INVENTORY_MOVEMENT", "Source_Doc_No", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Document_No), "TSPL_INVENTORY_MOVEMENT_NEW", "Source_Doc_No", trans)
            qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + obj.Document_No + "' and Trans_Type='SRN-RET'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + obj.Document_No + "' and Trans_Type='SRN-RET'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            HistoryUpdate(obj.Document_No, trans)
            qry = "delete from TSPL_SRN_RETURN where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Private Shared Function AllowToSave(ByVal obj As clsSRNReturn, ByVal objSRN As clsSRNHead) As Boolean
        Dim Qry As String = "select Document_No from TSPL_SRN_RETURN where SRN_No='" + obj.SRN_No + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Throw New Exception("SRN Return No " + clsCommon.myCstr(clsDBFuncationality.getSingleValue(dt.Rows(0)("Document_No")) + " already created"))
        End If

        Qry = "select distinct PI_No from TSPL_PI_DETAIL where SRN_Id ='" + obj.SRN_No + "'"
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Throw New Exception("SRN is used in Purchase Invoice No " + clsCommon.myCstr(clsDBFuncationality.getSingleValue(dt.Rows(0)("PI_No")) + " can not return it."))
        End If


        If objSRN.Arr IsNot Nothing AndAlso objSRN.Arr.Count > 0 Then
            For Each objsrntr As clsSRNDetail In objSRN.Arr
                Dim bal As Double = clsItemLocationDetails.getBalanceWithUnapprove(objsrntr.Item_Code, objSRN.Bill_To_Location, objsrntr.MRP, objsrntr.Unit_code, obj.Document_No, obj.Document_Date)
                If bal < 0 Then
                    Throw New Exception("Balance is going to -ve for item " + objsrntr.Item_Code)
                End If
            Next
        End If
        Return True
    End Function


    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSRNReturn
        Dim obj As clsSRNReturn = Nothing
        Dim qry As String = "SELECT TSPL_SRN_RETURN.* from TSPL_SRN_RETURN  where 2=2"
        Dim whrCls As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SRN_RETURN.Document_No = (select MIN(Document_No) from TSPL_SRN_RETURN WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_SRN_RETURN.Document_No = (select Max(Document_No) from TSPL_SRN_RETURN WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_SRN_RETURN.Document_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_SRN_RETURN.Document_No = (select Min(Document_No) from TSPL_SRN_RETURN where Document_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_SRN_RETURN.Document_No = (select Max(Document_No) from TSPL_SRN_RETURN where Document_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsSRNReturn()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.SRN_No = clsCommon.myCstr(dt.Rows(0)("SRN_No"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
        End If

        Return obj
    End Function
End Class
