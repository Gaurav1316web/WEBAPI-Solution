 

Imports common
Imports System.Data.SqlClient
Public Class clsJWOSRNReturn
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime
    Public JWO_SRN_No As String = Nothing
    Public Remarks As String = Nothing
    Public JWO_SRN_Location_Code As String = Nothing ''Not a Table field
    Public JWO_SRN_Document_Type As String = Nothing ''Not a Table field
    Public Form_ID As String = Nothing
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsJWOSRNReturn, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim objSRN As clsJWOSRNHead = clsJWOSRNHead.GetData(obj.JWO_SRN_No, NavigatorType.Current)
            AllowToSave(obj, objSRN)
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "JobWork Outward", "JobWork SRN Return", obj.JWO_SRN_Location_Code, Document_Date, trans)
            Try
                If isNewEntry Then
                    obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Document_Date), clsDocType.JWOSRNReturn, obj.JWO_SRN_Document_Type, obj.JWO_SRN_Location_Code)
                End If
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "JWO_SRN_No", obj.JWO_SRN_No)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                If isNewEntry Then
                    clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_SRN_RETURN", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_JWO_SRN_RETURN", "DOCUMENT_NO", trans)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_SRN_RETURN", OMInsertOrUpdate.Update, "TSPL_JWO_SRN_RETURN.Document_No='" + obj.Document_No + "'", trans)
                End If
                isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Document_No, obj.arrCustomFields, trans)

                ''Revese Inventory movement
                Dim count As Integer = 1
                Dim qry As String = "select * from TSPL_INVENTORY_MOVEMENT where Trans_Type='JWO-SRN' and Source_Doc_No='" + obj.JWO_SRN_No + "'"
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
                        'obj.arrSrItem = Nothing
                        ArrInventoryMovement.Add(objInvMov)
                        count = count + 1
                    Next
                    isSaved = isSaved AndAlso clsInventoryMovement.SaveData("JWO-SRN-RET", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                End If
                ''End of Revese Inventory movement

                ''Revese Inventory movement NEw
                count = 1
                qry = "select * from TSPL_INVENTORY_MOVEMENT_NEW where Trans_Type='JWO-SRN' and Source_Doc_No='" + obj.JWO_SRN_No + "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim ArrInventoryMovement As New List(Of clsInventoryMovementNew)
                    Dim objInvMov As clsInventoryMovementNew
                    For Each dr As DataRow In dt.Rows
                        objInvMov = New clsInventoryMovementNew
                        objInvMov.InOut = "O"
                        objInvMov.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                        objInvMov.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objInvMov.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                        objInvMov.Qty = clsCommon.myCstr(dr("Qty"))
                        objInvMov.UOM = clsCommon.myCstr(dr("UOM"))
                        objInvMov.main_location = clsCommon.myCstr(dr("main_location"))
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
                        'obj.arrSrItem = Nothing
                        ArrInventoryMovement.Add(objInvMov)
                        count = count + 1
                    Next
                    isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("JWO-SRN-RET", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                End If
                ''Revese Inventory movement New

                ''Journal Entry
                qry = " select TSPL_JOURNAL_DETAILS.Account_code,-1*TSPL_JOURNAL_DETAILS.Amount as Amount from TSPL_JOURNAL_DETAILS " & _
                " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" & _
                " where TSPL_JOURNAL_MASTER.Source_Doc_No='" + obj.JWO_SRN_No + "'  and Source_Code in ('JW-SR')"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim ArryLstGLAC As ArrayList = New ArrayList()
                    For Each dr As DataRow In dt.Rows
                        Dim Acc() As String = {clsCommon.myCstr(dr("Account_code")), clsCommon.myCdbl(dr("Amount"))}
                        ArryLstGLAC.Add(Acc)
                    Next
                    transportSql.FunGrnlEntryWithTrans(obj.JWO_SRN_Location_Code, False, trans, obj.Document_Date, "Against JWO SRN Return " + obj.Document_No, "JS-RT", "JWO SRN Return", obj.Document_No, obj.Remarks, "V", objSRN.Vendor_Code, objSRN.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
                End If
                ''
                ''Reverse Adjutment
                qry = "select Adjustment_No from TSPL_ADJUSTMENT_HEADER  where Reference_Document in ('JWO-SRN-JLO','JWO-SRN-JLI') and Document_No='" + obj.JWO_SRN_No + "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        Dim objAdj As New ClsAdjustments
                        objAdj = objAdj.GetData(dr("Adjustment_No"), "", NavigatorType.Current, trans)
                        If objAdj IsNot Nothing AndAlso clsCommon.myLen(objAdj.Adjustment_No) > 0 Then
                            If clsCommon.CompairString(objAdj.Trans_Type, "Out") = CompairStringResult.Equal Then
                                objAdj.Trans_Type = "In"
                            Else
                                objAdj.Trans_Type = "Out"
                            End If
                            objAdj.Adjustment_No = ""
                            objAdj.Adjustment_Date = obj.Document_Date
                            objAdj.Posting_Date = obj.Document_Date
                            objAdj.EntryDateTime = obj.Document_Date
                            objAdj.MainLocationCode = obj.JWO_SRN_Location_Code
                            objAdj.Description = "Adjustment for Stock " + objAdj.Trans_Type + " against JWO SRN Return No :" + obj.Document_No + ""
                            If clsCommon.CompairString(objAdj.Reference_Document, "JWO-SRN-JLO") = CompairStringResult.Equal Then
                                objAdj.Reference_Document = "JWO-SRN-RET-JLO"
                            ElseIf clsCommon.CompairString(objAdj.Reference_Document, "JWO-SRN-JLI") = CompairStringResult.Equal Then
                                objAdj.Reference_Document = "JWO-SRN-RET-JLI"
                            Else
                                Throw New Exception("Not a valid reference Document of adjustment")
                            End If
                            objAdj.Document_No = obj.Document_No
                            For ii As Integer = 0 To objAdj.Arr.Count - 1
                                If clsCommon.CompairString(objAdj.Arr(ii).Adjustment_Type, "BD") = CompairStringResult.Equal Then
                                    objAdj.Arr(ii).Adjustment_Type = "BI"
                                Else
                                    objAdj.Arr(ii).Adjustment_Type = "BD"
                                End If
                            Next
                            objAdj.SaveData(objAdj, True, "", trans)
                            ClsAdjustments.PostData(objAdj.Adjustment_No, "Store Adjustment", trans)
                        End If
                    Next
                End If
                'Throw New Exception("Balwinder")

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
            qry = "select JWO_SRN_No from TSPL_JWO_SRN_RETURN where Document_No='" + clsCommon.myCstr(SRNReturnNo) + "'"
            SRNNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(SRNNo) > 0 Then
                qry = "update TSPL_SRN_HEAD set IsCancel=1 where JWO_SRN_No='" + clsCommon.myCstr(SRNNo) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "select distinct (CASE WHEN ISNULL(TSPL_SRN_DETAIL.MRN_Id,'')='' THEN TSPL_SRN_HEAD.Against_MRN ELSE TSPL_SRN_DETAIL.MRN_Id END) AS MRNNo from TSPL_SRN_DETAIL LEFT OUTER JOIN TSPL_SRN_HEAD ON TSPL_SRN_DETAIL.JWO_SRN_No=TSPL_SRN_HEAD.JWO_SRN_No where TSPL_SRN_HEAD.JWO_SRN_No='" + clsCommon.myCstr(SRNNo) + "' AND isnull(TSPL_SRN_HEAD.Against_MRN,'')<>''"
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
        'Dim obj As clsJWOSRNReturn = clsJWOSRNReturn.GetData(Doc_No, NavigatorType.Current, trans)
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
    Private Shared Function AllowToSave(ByVal obj As clsJWOSRNReturn, ByVal objSRN As clsJWOSRNHead) As Boolean
        Dim Qry As String = "select Document_No from TSPL_JWO_SRN_RETURN where JWO_SRN_No='" + obj.JWO_SRN_No + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Throw New Exception("SRN Return No " + clsCommon.myCstr(clsDBFuncationality.getSingleValue(dt.Rows(0)("Document_No")) + " already created"))
        End If

        Qry = "select distinct PI_No from TSPL_PI_DETAIL where SRN_Id ='" + obj.JWO_SRN_No + "'"
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Throw New Exception("SRN is used in Purchase Invoice No " + clsCommon.myCstr(clsDBFuncationality.getSingleValue(dt.Rows(0)("PI_No")) + " can not return it."))
        End If


        If objSRN.Arr IsNot Nothing AndAlso objSRN.Arr.Count > 0 Then
            For Each objsrntr As clsJWOSRNDetail In objSRN.Arr
                Dim bal As Double = clsItemLocationDetails.getBalanceWithUnapprove(objsrntr.Item_Code, objSRN.Loc_Code, 0, objsrntr.UOM, obj.Document_No, obj.Document_Date)
                If bal < 0 Then
                    Throw New Exception("Balance is going to -ve for item " + objsrntr.Item_Code)
                End If
            Next
        End If
        Return True
    End Function


    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsJWOSRNReturn
        Dim obj As clsJWOSRNReturn = Nothing
        Dim qry As String = "SELECT TSPL_JWO_SRN_RETURN.* from TSPL_JWO_SRN_RETURN  where 2=2"
        Dim whrCls As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_JWO_SRN_RETURN.Document_No = (select MIN(Document_No) from TSPL_JWO_SRN_RETURN WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_JWO_SRN_RETURN.Document_No = (select Max(Document_No) from TSPL_JWO_SRN_RETURN WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_JWO_SRN_RETURN.Document_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_JWO_SRN_RETURN.Document_No = (select Min(Document_No) from TSPL_JWO_SRN_RETURN where Document_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_JWO_SRN_RETURN.Document_No = (select Max(Document_No) from TSPL_JWO_SRN_RETURN where Document_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsJWOSRNReturn()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.JWO_SRN_No = clsCommon.myCstr(dt.Rows(0)("JWO_SRN_No"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
        End If
        Return obj
    End Function
End Class
