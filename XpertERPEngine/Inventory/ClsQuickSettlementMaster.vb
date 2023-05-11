Imports common
Imports System.Data.SqlClient
Public Class ClsQuickSettlementMaster

#Region "Variable"
    Public Quick_SettleMent_Id As String = Nothing
    Public Transfer_Number As String = Nothing
    Public Transfer_Date As DateTime
    Public Transfer_Amount As Double = 0
    Public Post As Boolean = Nothing
    Public Salesman As String = Nothing
    Public RouteNo As String = Nothing
    Public VehicleNo As String = Nothing
    Public RouteDescription As String = Nothing
    Public Quick_Settlement_Date As Date
    Public Balance_Amount As Double = 0
    Public Comments As String = Nothing
    Public Load_In_Amount As Double = 0
    Public Empty_Load_In As String = Nothing
    Public Salesman_code As String = Nothing
    Public CashMemo As Integer = 0
    Public Arr As List(Of ClsQuickSettlementDetail) = Nothing
#End Region

    Public Shared Function funSave(ByVal obj As ClsQuickSettlementMaster, ByVal isNewEntry As Boolean) As Boolean

        If obj.Quick_Settlement_Date < obj.Transfer_Date Then
            Throw New Exception("Transafer Date Can't be Greater than Quick Settlement Date")
        End If

        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from tspl_QuickSettleMent_detail where Quick_SettleMent_Id='" + obj.Quick_SettleMent_Id + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Quick_SettleMent_Id = clsERPFuncationality.GetNextCode(trans, obj.Quick_Settlement_Date, clsDocType.QuickSettlement, "", "")
                If (clsCommon.myLen(obj.Quick_SettleMent_Id) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Transfer_Number", obj.Transfer_Number)
            clsCommon.AddColumnsForChange(coll, "Transfer_Date", clsCommon.GetPrintDate(obj.Transfer_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Transfer_Amount", obj.Transfer_Amount)
            clsCommon.AddColumnsForChange(coll, "Post", IIf(obj.Post, "Y", "N"))
            clsCommon.AddColumnsForChange(coll, "Salesman", obj.Salesman)
            clsCommon.AddColumnsForChange(coll, "RouteNo", obj.RouteNo)
            clsCommon.AddColumnsForChange(coll, "VehicleNo", obj.VehicleNo)
            clsCommon.AddColumnsForChange(coll, "RouteDescription", obj.RouteDescription)
            clsCommon.AddColumnsForChange(coll, "Quick_Settlement_Date", clsCommon.GetPrintDate(obj.Quick_Settlement_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Balance_Amount", obj.Balance_Amount)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Load_In_Amount", obj.Load_In_Amount)
            clsCommon.AddColumnsForChange(coll, "Empty_Load_In", obj.Empty_Load_In)
            clsCommon.AddColumnsForChange(coll, "Salesman_code", obj.Salesman_code)
            clsCommon.AddColumnsForChange(coll, "CashMemo", obj.CashMemo)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Quick_SettleMent_Id", obj.Quick_SettleMent_Id)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_QuickSettleMent", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_QuickSettleMent", OMInsertOrUpdate.Update, "tspl_QuickSettleMent.Quick_SettleMent_Id='" + obj.Quick_SettleMent_Id + "'", trans)
            End If
            isSaved = isSaved AndAlso ClsQuickSettlementDetail.SaveData(obj.Quick_SettleMent_Id, obj.Arr, trans)



            qry = "update  TSPL_TRANSFER_HEAD set Salesmancode='" + obj.Salesman_code + "', To_Location='" + obj.Salesman_code + "' ,ToLoc_Desc='" + obj.Salesman + "' where Transfer_No='" + clsCommon.myCstr(obj.Transfer_Number) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update  TSPL_TRANSFER_HEAD set Salesmancode='" + obj.Salesman_code + "', From_Location ='" + obj.Salesman_code + "' , FromLoc_Desc ='" + obj.Salesman + "' where Load_Out_No='" + clsCommon.myCstr(obj.Transfer_Number) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update  TSPL_ADJUSTMENT_HEADER set EMP_CODE ='" + obj.Salesman_code + "',EMP_NAME ='" + obj.Salesman + "' where Document_No='" + clsCommon.myCstr(obj.Transfer_Number) + "' and Reference_Document ='Load Out/Transfer'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '----Added By--Pankaj Kuamr---On----14/12/2012----For Change Salesman In Empty Transaction Against SaleInvoice Against Transfer--------
            Dim qryForsalesmanEmpty As String = " Select TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, TSPL_SHIPMENT_MASTER.Shipment_No, TSPL_TRANSFER_HEAD.Transfer_No  from TSPL_SALE_INVOICE_HEAD "
            qryForsalesmanEmpty += " Left Outer Join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SALE_INVOICE_HEAD.Shipment_No"
            qryForsalesmanEmpty += " Left Outer Join TSPL_TRANSFER_HEAD On  TSPL_TRANSFER_HEAD.Transfer_No=TSPL_SHIPMENT_MASTER.Transfer_No Where TSPL_TRANSFER_HEAD.Transfer_No='" + clsCommon.myCstr(obj.Transfer_Number) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryForsalesmanEmpty, trans)
            For Each dr As DataRow In dt.Rows
                clsERPFuncationality.ChangeSalesman(trans, clsCommon.myCstr(dr("Sale_Invoice_No")), clsCommon.myCstr(dr("Shipment_No")), obj.Salesman_code, obj.Salesman)
                'Dim strEmptyTrans As String = "Update TSPL_ADJUSTMENT_HEADER set EMP_CODE='" + obj.Salesman_code + "', EMP_NAME='" + obj.Salesman + "' Where ItemType='E' AND Reference_Document='Sale Invoice' and Document_No='" + clsCommon.myCstr(dr("Sale_Invoice_No")) + "'"
                'clsDBFuncationality.ExecuteNonQuery(strEmptyTrans, trans)
            Next

            '----------------------------------------------Code Ends Here--------------------------------------------------------------------------
            ''''' added by priti on 20/09/12 
            'qry = "update tspl_QuickSettleMent set Net_LoadOutQty=(select isnull(SUM(Item_Qty/Conversion_Factor),0) from TSPL_TRANSFER_DETAIL a ,TSPL_ITEM_UOM_DETAIL b where a.Item_Code=b.Item_Code and a.Uom=b.UOM_Code  and a.Uom <> 'sh' and a.Transfer_No='" + clsCommon.myCstr(obj.Transfer_Number) + "')"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "update tspl_QuickSettleMent set Net_LoadInQty=(select isnull(SUM(LoadIn_Qty/Conversion_Factor),0) from TSPL_TRANSFER_DETAIL a ,TSPL_ITEM_UOM_DETAIL b,TSPL_TRANSFER_HEAD c where a.Transfer_No=c.Transfer_No and  a.Item_Code=b.Item_Code and a.Uom=b.UOM_Code and a.Uom <> 'sh' and c.Load_Out_No='" + clsCommon.myCstr(obj.Transfer_Number) + "')"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "Update tspl_QuickSettleMent set Net_ProvisionalQty=Net_LoadOutQty - Net_LoadInQty where Transfer_Number='" + clsCommon.myCstr(obj.Transfer_Number) + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '''' code ends here


            qry = "update tspl_QuickSettleMent set Net_LoadOutQty=xxxx.LoadoutQty,Net_LoadInQty=xxxx.LoadinQty,Net_ProvisionalQty=xxxx.ProvQty" + Environment.NewLine
            qry += " from(" + Environment.NewLine
            qry += " select Transfer_No,SUM(Qty * case when RI=1 then 1 else 0 end) as LoadoutQty,SUM(Qty * case when RI=-1 then 1 else 0 end) as LoadinQty,SUM(Qty * RI) as ProvQty from(" + Environment.NewLine
            qry += " select TSPL_TRANSFER_DETAIL.Transfer_No,TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL.Item_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as Qty,1 as RI from TSPL_TRANSFER_DETAIL" + Environment.NewLine
            qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_TRANSFER_DETAIL.Uom" + Environment.NewLine
            qry += " left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No " + Environment.NewLine
            qry += " where TSPL_TRANSFER_DETAIL.Uom in ('FB','FC') and TSPL_TRANSFER_HEAD.Transfer_Type='LO'  and TSPL_TRANSFER_HEAD.Transfer_No='" + obj.Transfer_Number + "'" + Environment.NewLine
            qry += " union all " + Environment.NewLine
            qry += " select  TSPL_TRANSFER_HEAD.Load_Out_No as Transfer_No,TSPL_TRANSFER_DETAIL.Item_Code,(TSPL_TRANSFER_DETAIL.LoadIn_Qty+TSPL_TRANSFER_DETAIL.Leak+TSPL_TRANSFER_DETAIL.Burst+TSPL_TRANSFER_DETAIL.Shortage)/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as Qty,-1 as RI from TSPL_TRANSFER_DETAIL" + Environment.NewLine
            qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_TRANSFER_DETAIL.Uom" + Environment.NewLine
            qry += " left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No " + Environment.NewLine
            qry += " where TSPL_TRANSFER_DETAIL.Uom in ('FB','FC') and TSPL_TRANSFER_HEAD.Transfer_Type='LI' and TSPL_TRANSFER_HEAD.Load_Out_No='" + obj.Transfer_Number + "'  " + Environment.NewLine
            qry += " )xxx " + Environment.NewLine
            qry += " group by Transfer_No" + Environment.NewLine
            qry += " ) xxxx" + Environment.NewLine
            qry += " inner join tspl_QuickSettleMent on tspl_QuickSettleMent.Transfer_Number=xxxx.Transfer_No"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)



            isSaved = isSaved AndAlso ClsQuickSettlement.CalExcessAmt(obj.Quick_SettleMent_Id, obj.Transfer_Number, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function


    Public Shared Function GetData(ByVal strDocNo As String) As ClsQuickSettlementMaster
        Dim obj As ClsQuickSettlementMaster = Nothing
        Dim Qry As String = "select Salesman_code,CashMemo,VehicleNo, Quick_Settlement_Date, Transfer_Number,Transfer_Date,Transfer_Amount,Post,Salesman,RouteDescription,routeno,Balance_Amount,Comments,Load_In_Amount,Empty_Load_In from tspl_QuickSettleMent where Quick_SettleMent_Id='" + strDocNo + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsQuickSettlementMaster()
            obj.Quick_SettleMent_Id = strDocNo
            obj.Transfer_Number = clsCommon.myCstr(dt.Rows(0)("Transfer_Number"))
            obj.Transfer_Date = clsCommon.myCDate(dt.Rows(0)("Transfer_Date"), "dd/MM/yyyy")
            obj.Quick_Settlement_Date = clsCommon.myCDate(dt.Rows(0)("Quick_Settlement_Date"), "dd/MM/yyyy")
            obj.Transfer_Amount = clsCommon.myCdbl(dt.Rows(0)("Transfer_Amount"))
            If clsCommon.myCstr(dt.Rows(0)("Post")) = "N" Then
                obj.Post = False
            Else
                obj.Post = True
            End If
            obj.Salesman = clsCommon.myCstr(dt.Rows(0)("Salesman"))
            obj.RouteNo = clsCommon.myCstr(dt.Rows(0)("RouteNo"))
            obj.VehicleNo = clsCommon.myCstr(dt.Rows(0)("VehicleNo"))
            obj.Empty_Load_In = clsCommon.myCdbl(dt.Rows(0)("Empty_Load_In"))
            obj.RouteDescription = clsCommon.myCstr(dt.Rows(0)("RouteDescription"))
            obj.Balance_Amount = clsCommon.myCdbl(dt.Rows(0)("Balance_Amount"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Load_In_Amount = clsCommon.myCdbl(dt.Rows(0)("Load_In_Amount"))
            obj.CashMemo = clsCommon.myCdbl(dt.Rows(0)("CashMemo"))
            obj.Salesman_code = clsCommon.myCstr(dt.Rows(0)("Salesman_code"))

        End If

        Qry = "select  SettleMentCode ,Description,isnull((select amount from tspl_QuickSettleMent_Detail L where L.SettleMent_Code=M.SettleMentCode and L.Quick_SettleMent_Id ='" + strDocNo + "' ),0) as [Amount] ,isnull((select Remarks  from tspl_QuickSettleMent_Detail L where L.SettleMent_Code=M.SettleMentCode and L.Quick_SettleMent_Id ='" + strDocNo + "' ),'') as [Remarks] ,SettleMent_Type from tspl_SettleMent_Master M where M.Type='Q' or M.Type='B'order by M.Sequence_No "
        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(Qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.Arr = New List(Of ClsQuickSettlementDetail)
            Dim objTr As ClsQuickSettlementDetail


            For Each dr As DataRow In dt.Rows
                objTr = New ClsQuickSettlementDetail
                objTr.SettleMent_Code = clsCommon.myCstr(dr("SettleMentCode"))
                objTr.Description = clsCommon.myCstr(dr("Description"))
                objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                objTr.SettlementType = clsCommon.myCstr(dr("SettleMent_Type"))
                obj.Arr.Add(objTr)
            Next
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("QuickSettlement No not found to Delete")
        End If
        Dim obj As ClsQuickSettlementMaster = ClsQuickSettlementMaster.GetData(strCode)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Quick_SettleMent_Id) > 0) Then
            Try
                If (obj.Post = True) Then
                    Throw New Exception("Already Posted.")
                End If
                Dim qry As String = "delete from tspl_QuickSettleMent_Detail where Quick_SettleMent_Id='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from tspl_QuickSettleMent_Item_Detail where Quick_SettleMent_Id='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)


                qry = "delete from tspl_QuickSettleMent where Quick_SettleMent_Id='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

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
End Class
Public Class ClsQuickSettlementDetail

#Region "Variable"
    Public Quick_SettleMent_Id As String = Nothing
    Public SettleMent_Code As String = Nothing
    Public Description As String = Nothing
    Public Amount As Double = 0
    Public Remarks As String = Nothing
    Public SettlementType As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of ClsQuickSettlementDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As ClsQuickSettlementDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Quick_SettleMent_Id", strDocNo)
                clsCommon.AddColumnsForChange(coll, "SettleMent_Code", obj.SettleMent_Code)
                clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_QuickSettleMent_detail", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class