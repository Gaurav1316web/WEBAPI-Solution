Imports common
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class clsOutputEntry
#Region "Variables"
    Public Doc_Code As String = Nothing
    Public Doc_Date As DateTime = Nothing
    Public Plant_Code As String = Nothing
    Public Mcc_Code As String = Nothing
    Public FromDate As DateTime? = Nothing
    Public ToDate As DateTime? = Nothing
    Public Output_Type As String = Nothing
    Public FatPer As Decimal = 0
    Public SNFPer As Decimal = 0
    Public FatKG As Decimal = 0
    Public SNFKG As Decimal = 0
    Public QtyKG As Decimal = 0
    Public QtyLTR As Decimal = 0
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Post_Date As DateTime? = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsOutputEntry, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsOutputEntry, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Fixed Asset", "Asset Account Change", obj.Loc_Code, obj.Doc_Date, trans)
        Try
            If Not isNewEntry Then
                Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Status from TSPL_OUTPUT_ENTRY Where Doc_Code='" + obj.Doc_Code + "'", trans))
                If Status = 1 Then
                    Throw New Exception("This document is already posted.")
                End If
            End If

            Dim strDocNo As String = ""

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Plant_Code", obj.Plant_Code)
            clsCommon.AddColumnsForChange(coll, "Mcc_Code", obj.Mcc_Code)
            clsCommon.AddColumnsForChange(coll, "FromDate", clsCommon.GetPrintDate(obj.FromDate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "ToDate", clsCommon.GetPrintDate(obj.ToDate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Output_Type", obj.Output_Type)
            clsCommon.AddColumnsForChange(coll, "FatPer", obj.FatPer)
            clsCommon.AddColumnsForChange(coll, "SNFPer", obj.SNFPer)
            clsCommon.AddColumnsForChange(coll, "FatKG", obj.FatKG)
            clsCommon.AddColumnsForChange(coll, "SNFKG", obj.SNFKG)
            clsCommon.AddColumnsForChange(coll, "QtyKG", obj.QtyKG)
            clsCommon.AddColumnsForChange(coll, "QtyLTR", obj.QtyLTR)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
                obj.Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Doc_Date, clsDocType.OutputEntry, "", obj.Plant_Code)
                If (clsCommon.myLen(obj.Doc_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.Doc_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OUTPUT_ENTRY", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OUTPUT_ENTRY", OMInsertOrUpdate.Update, "TSPL_OUTPUT_ENTRY.Doc_Code='" + obj.Doc_Code + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function


    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsOutputEntry
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsOutputEntry
        Dim obj As clsOutputEntry = Nothing
        Dim qry As String = "SELECT TSPL_OUTPUT_ENTRY.*  FROM TSPL_OUTPUT_ENTRY  "
        qry += " where 2=2  "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_OUTPUT_ENTRY.Doc_Code = (select MIN(Doc_Code) from TSPL_OUTPUT_ENTRY where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_OUTPUT_ENTRY.Doc_Code = (select Max(Doc_Code) from TSPL_OUTPUT_ENTRY where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_OUTPUT_ENTRY.Doc_Code = (select Min(Doc_Code) from TSPL_OUTPUT_ENTRY where Doc_Code>'" + strPONo + "'" + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_OUTPUT_ENTRY.Doc_Code = (select Max(Doc_Code) from TSPL_OUTPUT_ENTRY where Doc_Code<'" + strPONo + "'" + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_OUTPUT_ENTRY.Doc_Code = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsOutputEntry()
            obj.Doc_Code = clsCommon.myCstr(dt.Rows(0)("Doc_Code"))
            obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
            obj.Plant_Code = clsCommon.myCstr(dt.Rows(0)("Plant_Code"))
            obj.Mcc_Code = clsCommon.myCstr(dt.Rows(0)("Mcc_Code"))
            obj.FromDate = clsCommon.myCDate(dt.Rows(0)("FromDate"))
            obj.ToDate = clsCommon.myCDate(dt.Rows(0)("ToDate"))
            obj.Output_Type = clsCommon.myCstr(dt.Rows(0)("Output_Type"))
            obj.FatPer = clsCommon.myCdbl(dt.Rows(0)("FatPer"))
            obj.SNFPer = clsCommon.myCdbl(dt.Rows(0)("SNFPer"))
            obj.FatKG = clsCommon.myCdbl(dt.Rows(0)("FatKG"))
            obj.SNFKG = clsCommon.myCdbl(dt.Rows(0)("SNFKG"))
            obj.QtyKG = clsCommon.myCdbl(dt.Rows(0)("QtyKG"))
            obj.QtyLTR = clsCommon.myCdbl(dt.Rows(0)("QtyLTR"))

            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Post_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If

        End If

        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String
        Try

            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim dtPostDate As DateTime = clsCommon.GETSERVERDATE(trans) ' clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsOutputEntry = clsOutputEntry.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.Post_Date, "dd/MM/yyyy"))
            End If
            ''richa VIJ/26/07/21-001248
            UpdateInventoryMovement(obj, trans, False)
            CreateJournalEntry(obj, trans, "")
            'Dim ArryLst As ArrayList = New ArrayList()

            'Dim strQ As String = "select * from TSPL_ASSET_ACCOUNT_CHANGE_DETAIL" &
            '         " where TSPL_ASSET_ACCOUNT_CHANGE_DETAIL.Doc_Code ='" & strDocNo & "'"
            'Dim dtData As DataTable = clsDBFuncationality.GetDataTable(strQ, trans)
            'If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
            '    For i As Integer = 0 To dtData.Rows.Count - 1
            '        Dim strAc_Control As String = clsERPFuncationality.ChangeGLAccountLocationSegment(dtData.Rows(i)("ChangedAc_Code"), obj.Loc_Code, trans)
            '        If clsCommon.myLen(strAc_Control) <= 0 Then
            '            Throw New Exception("GL Account " & dtData.Rows(i)("ChangedAc_Code") & " not Found For Location " & obj.Loc_Code & "")
            '        End If
            '        ArryLst.Add(New String() {strAc_Control, clsCommon.myCdbl(dtData.Rows(i)("Item_Net_Amt"))})

            '        Dim strWIP_AC As String = clsERPFuncationality.ChangeGLAccountLocationSegment(dtData.Rows(i)("Ac_Code"), obj.Loc_Code, trans)
            '        If clsCommon.myLen(strWIP_AC) <= 0 Then
            '            Throw New Exception("GL Account " & dtData.Rows(i)("Ac_Code") & " not Found For Location " & obj.Loc_Code & "")
            '        End If
            '        ArryLst.Add(New String() {strWIP_AC, clsCommon.myCdbl(dtData.Rows(i)("Item_Net_Amt")) * -1})
            '    Next

            '    clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Doc_Date, "Asset Account Change, Against Acquisition Code:  " & obj.Acquisition_Code, "AQ-AC", "Asset Account Change", strDocNo, "Asset Account Change", "V", strDocNo, "Asset Account Change", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
            'End If


            qry = "Update TSPL_OUTPUT_ENTRY set Status=1, Posted_Date='" + clsCommon.GetPrintDate(dtPostDate, "dd/MMM/yyyy hh:mm tt") + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Doc_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function UpdateInventoryMovement(ByVal obj As clsOutputEntry, ByVal trans As SqlTransaction, Optional ByVal UpdateInventory As Boolean = False) As Boolean
        Try

            Dim TransType_Str As String = ""
            Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            If UpdateInventory = True Then
                clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" & obj.Doc_Code & "'", trans)
            End If
            Dim strRgpNo As String = Nothing
            Dim intCounter As Integer = 0
            intCounter = intCounter + 1
            Dim strItemCode As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans))
            If clsCommon.myLen(strItemCode) > 0 Then

                Dim strItemType As String = clsItemMaster.GetItemType(strItemCode, trans)
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
                Dim objInventoryMovemnt As New clsInventoryMovementNew()
                objInventoryMovemnt.InOut = "O"

                objInventoryMovemnt.Location_Code = obj.Mcc_Code
                objInventoryMovemnt.Item_Code = strItemCode
                objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(strItemCode, trans)
                objInventoryMovemnt.Qty = obj.QtyKG
                objInventoryMovemnt.UOM = "KG"
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "FT"
                End If
                objInventoryMovemnt.ItemType = strItemTypeToSave
                objInventoryMovemnt.FAT_KG = obj.FatKG
                objInventoryMovemnt.SNF_KG = obj.SNFKG
                objInventoryMovemnt.FAT_Per = obj.FatPer
                objInventoryMovemnt.SNF_Per = obj.SNFPer
                objInventoryMovemnt.CalculateAvgCost = True
                objInventoryMovemnt.Punching_Date = obj.ToDate
                ArrInventoryMovement.Add(objInventoryMovemnt)

                clsInventoryMovementNew.SaveData("OUT-PUT", obj.Doc_Code, obj.Doc_Date, clsCommon.GetPrintDate(obj.Doc_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            Else
                Throw New Exception("Please enter item on MCCDefaultMilkItem in Utility")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Sub CreateJournalEntry(ByVal obj As clsOutputEntry, ByVal trans As SqlTransaction, Optional ByVal strVoucherNoForRecreateOnly As String = Nothing, Optional ByVal IsDairyModule As Boolean = False)
        Try
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim strInventoryControlAc As String = ""
            Dim strStoreConsumptionAC As String = ""
            Dim dblTotalCost As Double = 0
            Dim strItemCode As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans))
            If clsCommon.myLen(strItemCode) > 0 Then

                strStoreConsumptionAC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.Store_Consumption_Acc FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
              " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
               " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + strItemCode + "'", trans))
                If clsCommon.myLen(strStoreConsumptionAC) = 0 Then
                    Throw New Exception("Please set Store Consumption Account for first item")
                End If
                strStoreConsumptionAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strStoreConsumptionAC, obj.Mcc_Code, False, trans)


                Dim dblCogsCost As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end) as COst from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0 and Source_Doc_No='" & obj.Doc_Code & "'", trans))

                Dim Acc() As String = {strStoreConsumptionAC, dblCogsCost}
                ArryLstGLAC.Add(Acc)

                Dim strSql As String = "select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end as Cost from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code  where isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0 and Source_Doc_No='" & obj.Doc_Code & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows
                        strInventoryControlAc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                        " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
                        " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'", trans))
                        If clsCommon.myLen(strInventoryControlAc) = 0 Then
                            Throw New Exception("Please set Inventory Control Account for first item")
                        End If
                        strInventoryControlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInventoryControlAc, obj.Mcc_Code, trans)


                        Dim Acc1() As String = {strInventoryControlAc, -1 * clsCommon.myCdbl(dr("Cost")), "", "", "", "", "", "", "I"}
                        ArryLstGLAC.Add(Acc1)

                        clsInventoryMovement.UpdateInvControlAccount(obj.Doc_Code, "OUT-PUT", clsCommon.myCstr(dr("Item_Code")), "", strInventoryControlAc, "", trans)
                    Next
                End If
                If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                    clsJournalMaster.FunGrnlEntryWithTrans(obj.Mcc_Code, False, strVoucherNoForRecreateOnly, trans, obj.ToDate, "Journal Entry created Against Output Entry " & obj.Doc_Code & "", "OT-PT", "Output Entry", obj.Doc_Code, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, "")
                Else
                    clsJournalMaster.FunGrnlEntryWithTrans(obj.Mcc_Code, False, trans, obj.ToDate, "Journal Entry created Against Output Entry " & obj.Doc_Code & "", "OT-PT", "Output Entry", obj.Doc_Code, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, "")
                End If
            Else
                Throw New Exception("Please enter item on MCCDefaultMilkItem in Utility")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = ""
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document not found to Delete")
        End If
        Dim obj As clsOutputEntry = clsOutputEntry.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then
            Try
                If (obj.Status = ERPTransactionStatus.Approved) Then
                    Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Post_Date, "dd/MM/yyyy"))
                End If

                qry = "delete from TSPL_OUTPUT_ENTRY where Doc_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

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
    Public Shared Function ReverseAndUnpost(ByVal strCode As String)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsOutputEntry.ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        ''Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            Dim Qry As String = String.Empty
            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code in('OT-PT') and Source_Doc_No='" + strDocNo + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Qry = "delete from TSPL_INVENTORY_MOVEMENT_New where Source_Doc_No='" + strDocNo + "' and Trans_Type='OUT-PUT'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            clsDBFuncationality.ExecuteNonQuery("Update TSPL_OUTPUT_ENTRY set Status=0 where Doc_Code ='" + strDocNo + "'", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    ''---------------

    'Public Shared Function CheckCode(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
    '    Dim Qry As String = "select count(Acquisition_Code) from TSPL_ACQUISITION_HEAD where Acquisition_Code='" & Code & "'"
    '    Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans))
    '    If count > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function


End Class