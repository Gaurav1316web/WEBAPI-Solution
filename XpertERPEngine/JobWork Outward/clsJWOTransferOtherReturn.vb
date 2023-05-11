Imports common
Imports System.Data.SqlClient
Public Class clsJWOTransferOtherReturn
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime
    Public JWO_Transfer_No As String = Nothing
    Public Remarks As String = Nothing
    Public JWO_SRN_From_Location_Code As String = Nothing ''Not a Table field
    Public Form_ID As String = Nothing
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsJWOTransferOtherReturn, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsJWOTransferOtherReturn, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim objSRN As clsJWOTransferOtherHead = clsJWOTransferOtherHead.GetData(obj.JWO_Transfer_No, NavigatorType.Current, trans)
            AllowToSave(obj, trans)

            Try
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "JobWork Outward", "JobWork Other Transfer Return", obj.JWO_SRN_From_Location_Code, obj.Document_Date, trans)

                If isNewEntry Then
                    obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Document_Date), clsDocType.JobWorkTransferOtherReturn, "", obj.JWO_SRN_From_Location_Code)
                End If
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "JWO_Transfer_No", obj.JWO_Transfer_No)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                If isNewEntry Then
                    clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN", OMInsertOrUpdate.Insert, "", trans)
                Else
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Document_No), "TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN", "Document_No", trans)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN", OMInsertOrUpdate.Update, "TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN.Document_No='" + obj.Document_No + "'", trans)
                End If
                isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Document_No, obj.arrCustomFields, trans)


                ''Revese Inventory movement
                Dim count As Integer = 1
                Dim qry As String = "select * from TSPL_INVENTORY_MOVEMENT where Trans_Type='Transfer' and Source_Doc_No='" + obj.JWO_Transfer_No + "' order by InOut desc"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim ArrInventoryMovement As New List(Of clsInventoryMovement)
                    Dim objInvMov As clsInventoryMovement
                    For Each dr As DataRow In dt.Rows
                        objInvMov = New clsInventoryMovement
                        objInvMov.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                        objInvMov.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objInvMov.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                        objInvMov.Qty = clsCommon.myCstr(dr("Qty"))
                        objInvMov.UOM = clsCommon.myCstr(dr("UOM"))

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
                        'obj.arrSrItem = Nothing
                        objInvMov.InOut = clsCommon.myCstr(dr("InOut"))
                        If clsCommon.CompairString(objInvMov.InOut, "O") = CompairStringResult.Equal Then
                            objInvMov.InOut = "I"
                        Else
                            objInvMov.InOut = "O"
                            '' check balance 
                            Dim Bal As Decimal = clsItemLocationDetails.getBalance(objInvMov.Item_Code, objInvMov.Location_Code, obj.Document_No, obj.Document_Date, trans, objInvMov.UOM, 0)
                            If Bal < objInvMov.Qty Then
                                Throw New Exception("Item Code: " & objInvMov.Item_Code & " Location: " & objInvMov.Location_Code & " Return Qty: " & objInvMov.Qty & " Available Qty: " & Bal & " " & objInvMov.UOM & " ")
                            End If
                        End If

                        ArrInventoryMovement.Add(objInvMov)
                        count = count + 1
                    Next
                    isSaved = isSaved AndAlso clsInventoryMovement.SaveData("JWO-Transfer-RET", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                End If
                ''End of Revese Inventory movement

                ''Journal Entry
                qry = " select TSPL_JOURNAL_DETAILS.Account_code,-1*TSPL_JOURNAL_DETAILS.Amount as Amount from TSPL_JOURNAL_DETAILS " & _
                " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" & _
                " where TSPL_JOURNAL_MASTER.Source_Doc_No='" + obj.JWO_Transfer_No + "'  and Source_Code in ('JW-TF')"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim ArryLstGLAC As ArrayList = New ArrayList()
                    For Each dr As DataRow In dt.Rows
                        Dim Acc() As String = {clsCommon.myCstr(dr("Account_code")), clsCommon.myCdbl(dr("Amount"))}
                        ArryLstGLAC.Add(Acc)
                    Next
                    transportSql.FunGrnlEntryWithTrans(obj.JWO_SRN_From_Location_Code, False, trans, obj.Document_Date, "Against JWO Transfer Return Other -" + obj.Document_No, "JW-TR", "JWO Transfer Other Return", obj.Document_No, obj.Remarks, "V", objSRN.Vendor_Code, clsVendorMaster.GetName(objSRN.Vendor_Code, trans), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
                End If
                'Throw New Exception("Balwinder")
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Private Shared Function AllowToSave(ByVal obj As clsJWOTransferOtherReturn, ByVal trans As SqlTransaction) As Boolean
        Dim Qry As String = "select Document_No from TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN where JWO_Transfer_No='" + obj.JWO_Transfer_No + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Throw New Exception("Transfer Return No " + clsCommon.myCstr(clsDBFuncationality.getSingleValue(dt.Rows(0)("Document_No")) + " already created"))
        End If

        'Qry = "select distinct PI_No from TSPL_PI_DETAIL where SRN_Id ='" + obj.JWO_Transfer_No + "'"
        'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '    Throw New Exception("SRN is used in Purchase Invoice No " + clsCommon.myCstr(clsDBFuncationality.getSingleValue(dt.Rows(0)("PI_No")) + " can not return it."))
        'End If

        Return True
    End Function


    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsJWOTransferOtherReturn
        Dim obj As clsJWOTransferOtherReturn = Nothing
        Dim qry As String = "SELECT TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN.* from TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN  where 2=2"
        Dim whrCls As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN.Document_No = (select MIN(Document_No) from TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN.Document_No = (select Max(Document_No) from TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN.Document_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN.Document_No = (select Min(Document_No) from TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN where Document_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN.Document_No = (select Max(Document_No) from TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN where Document_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsJWOTransferOtherReturn()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.JWO_Transfer_No = clsCommon.myCstr(dt.Rows(0)("JWO_Transfer_No"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
        End If
        Return obj
    End Function
End Class
