'manoj
Imports common
Imports System.Data.SqlClient

Public Class ClsQuickSettlement
    Shared Qry As String = Nothing

    Shared Function PostData(ByVal docno As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(docno, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Shared Function PostData(ByVal docno As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim LoadOutNo As String = clsDBFuncationality.getSingleValue("select Transfer_Number  from tspl_QuickSettleMent where Quick_SettleMent_Id ='" + clsCommon.myCstr(docno) + "'", trans)
            Dim LoadInNo As String = clsDBFuncationality.getSingleValue("select Transfer_No  from TSPL_TRANSFER_HEAD where post='N' and Load_Out_No='" + LoadOutNo + "' ", trans)
            If clsCommon.myLen(LoadInNo) > 0 Then
                clsTransferMaster.postTransfer(LoadInNo, trans)
            End If
            Dim EmptyAdj As String = clsDBFuncationality.getSingleValue(" select Adjustment_No  from TSPL_ADJUSTMENT_HEADER where Document_No ='" + LoadOutNo + "' and Reference_Document ='Load Out/Transfer' and Posted='N'", trans)
            If clsCommon.myLen(EmptyAdj) > 0 Then
                Qry = " update TSPL_ADJUSTMENT_HEADER  set Posted='Y' ,Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "',modified_time='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "hh:mm tt") & "' where Adjustment_No='" + EmptyAdj + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            If clsCommon.myLen(docno) > 0 And clsCommon.myLen(LoadOutNo) > 0 Then
                If CalExcessAmt(docno, LoadOutNo, trans) = True Then
                    Qry = "update  TSPL_TRANSFER_HEAD set Quick_Settlement='Y' where Transfer_No='" + clsCommon.myCstr(LoadOutNo) + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                    Qry = "update  tspl_QuickSettleMent set Post='Y' where Quick_SettleMent_Id='" + clsCommon.myCstr(docno) + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                End If
            End If

            ''Throw New Exception("deadlocked Occredddddd")
            'Throw New Exception("exception")
            'myMessages.post()
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Shared Function CalExcessAmt(ByVal QuickSettlementNo As String, ByVal LoadOutNo As String) As Boolean
        Return CalExcessAmt(QuickSettlementNo, LoadOutNo, Nothing)
    End Function
    Shared Function CalExcessAmt(ByVal QuickSettlementNo As String, ByVal LoadOutNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim Query As String = Nothing
        Dim Discount As Decimal = 0
        Dim CrSale As Decimal = 0
        Dim Deposit As Decimal = 0
        Dim Refund As Decimal = 0
        Dim CreditSale As Decimal = 0
        Dim Cash As Decimal = 0
        Dim Cheque As Decimal = 0
        Dim CashShEx As Decimal = 0
        Dim EmptyShEx As Decimal = 0
        Dim FlagAccCheck As Decimal = 0
        Dim LoadInNo As String = clsDBFuncationality.getSingleValue("select Transfer_No  from TSPL_TRANSFER_HEAD where  Load_Out_No='" + LoadOutNo + "' ", trans)
        Try
            Dim NetLoadOutAmt As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(sum(Item_Qty*(BasicPrice_WithTax+TPT_Value+Empty_Value)),0)  from TSPL_TRANSFER_DETAIL where Transfer_No ='" + LoadOutNo + "' ", trans))
            Dim NetLoadInAmt As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(sum((LoadIn_Qty+Shortage+Burst  +Leak )*(BasicPrice_WithTax+TPT_Value+Empty_Value)),0)  from TSPL_TRANSFER_DETAIL where Transfer_No ='" + LoadInNo + "' ", trans))

            Dim LoadOutAmtWithOutEmpty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(sum(Item_Qty*(BasicPrice_WithTax+TPT_Value)),0)  from TSPL_TRANSFER_DETAIL where Transfer_No ='" + LoadOutNo + "' and Uom <>'SH'", trans))
            Dim LoadInAmtWithOutEmpty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(sum((LoadIn_Qty+Shortage+Burst  +Leak )*(BasicPrice_WithTax+TPT_Value)),0)  from TSPL_TRANSFER_DETAIL where Transfer_No ='" + LoadInNo + "' and Uom <>'SH'", trans))

            Dim AdjustmentEmpty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum( case when Trans_Type ='IN' then  ISNULL(d.Item_Cost ,0) + ISNULL(d.Breakage_Cost ,0) else (ISNULL(d.Item_Cost ,0) + ISNULL(d.Breakage_Cost ,0)) *-1  end) as total from TSPL_ADJUSTMENT_HEADER h join TSPL_ADJUSTMENT_DETAIL d on h.Adjustment_No = d.Adjustment_No where h.Document_No = '" + LoadOutNo + "'", trans))

            Dim NetLoad As Decimal = NetLoadOutAmt - NetLoadInAmt
            Dim NetSale As Decimal = LoadOutAmtWithOutEmpty - LoadInAmtWithOutEmpty

            Dim Dt As DataTable = clsDBFuncationality.GetDataTable("select SettleMent_Code,Amount ,tspl_SettleMent_Master.Calculate ,tspl_SettleMent_Master.SettleMent_Type  from tspl_QuickSettleMent_Detail inner join tspl_SettleMent_Master on tspl_QuickSettleMent_Detail.SettleMent_Code =tspl_SettleMent_Master.SettleMentCode  where Quick_SettleMent_Id ='" + QuickSettlementNo + "'", trans)
            If Dt.Rows.Count > 0 Then
                For Each row As DataRow In Dt.Rows
                    If clsCommon.CompairString(clsCommon.myCstr(row("SettleMent_Type")), "DSC") = CompairStringResult.Equal Then
                        Discount = Discount + clsCommon.myCdbl(row("Amount"))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(row("SettleMent_Type")), "REF") = CompairStringResult.Equal Then
                        Refund = Refund + clsCommon.myCdbl(row("Amount"))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(row("SettleMent_Type")), "DEP") = CompairStringResult.Equal Then
                        Deposit = Deposit + clsCommon.myCdbl(row("Amount"))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(row("SettleMent_Type")), "CRS") = CompairStringResult.Equal Then
                        CreditSale = CreditSale + clsCommon.myCdbl(row("Amount"))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(row("SettleMent_Type")), "CHQ") = CompairStringResult.Equal Then
                        Cheque = Cheque + clsCommon.myCdbl(row("Amount"))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(row("SettleMent_Type")), "CSH") = CompairStringResult.Equal Then
                        Cash = Cash + clsCommon.myCdbl(row("Amount"))
                    End If
                Next
            End If
            CashShEx = NetSale - Discount - CreditSale - Cash - Cheque
            EmptyShEx = NetLoad - NetSale - Deposit - (Refund * -1) - AdjustmentEmpty

            If CashShEx > 0 Then
                Query = "select 1 from tspl_QuickSettleMent_Detail inner join  tspl_SettleMent_Master on tspl_QuickSettleMent_Detail.SettleMent_Code =tspl_SettleMent_Master.SettleMentCode  where (Type='Q' or Type='B') and SettleMent_Type ='CSE' and Calculate ='S' and Quick_SettleMent_Id ='" + QuickSettlementNo + "'"
                FlagAccCheck = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Query, trans))
                If FlagAccCheck = 1 Then
                    Query = "update tspl_QuickSettleMent_Detail set Amount=" + clsCommon.myCstr(CashShEx) + " from tspl_QuickSettleMent_Detail inner join  tspl_SettleMent_Master on tspl_QuickSettleMent_Detail.SettleMent_Code =tspl_SettleMent_Master.SettleMentCode  where (Type='Q' or Type='B') and SettleMent_Type ='CSE' and Calculate ='S' and Quick_SettleMent_Id ='" + QuickSettlementNo + "'"
                Else
                    Query = "insert into tspl_QuickSettleMent_Detail select '" + QuickSettlementNo + "', SettleMentCode ,Description," + clsCommon.myCstr(CashShEx) + ",'' from tspl_SettleMent_Master where (Type='Q' or Type='B') and SettleMent_Type ='CSE' and Calculate ='S' "
                End If
                clsDBFuncationality.ExecuteNonQuery(Query, trans)
                Query = "update tspl_QuickSettleMent_Detail set Amount=" + clsCommon.myCstr(0) + " from tspl_QuickSettleMent_Detail inner join  tspl_SettleMent_Master on tspl_QuickSettleMent_Detail.SettleMent_Code =tspl_SettleMent_Master.SettleMentCode  where (Type='Q' or Type='B') and SettleMent_Type ='CSE' and Calculate ='A' and Quick_SettleMent_Id ='" + QuickSettlementNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Query, trans)
            ElseIf CashShEx < 0 Then
                Query = "select 1 from tspl_QuickSettleMent_Detail inner join  tspl_SettleMent_Master on tspl_QuickSettleMent_Detail.SettleMent_Code =tspl_SettleMent_Master.SettleMentCode  where (Type='Q' or Type='B') and SettleMent_Type ='CSE' and Calculate ='A' and Quick_SettleMent_Id ='" + QuickSettlementNo + "'"
                FlagAccCheck = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Query, trans))
                If FlagAccCheck = 1 Then
                    Query = "update tspl_QuickSettleMent_Detail set Amount=" + clsCommon.myCstr(CashShEx * -1) + " from tspl_QuickSettleMent_Detail inner join  tspl_SettleMent_Master on tspl_QuickSettleMent_Detail.SettleMent_Code =tspl_SettleMent_Master.SettleMentCode  where (Type='Q' or Type='B') and SettleMent_Type ='CSE' and Calculate ='A' and Quick_SettleMent_Id ='" + QuickSettlementNo + "'"
                Else
                    Query = "insert into tspl_QuickSettleMent_Detail select '" + QuickSettlementNo + "', SettleMentCode ,Description," + clsCommon.myCstr(CashShEx * -1) + ",'' from tspl_SettleMent_Master where (Type='Q' or Type='B') and SettleMent_Type ='CSE' and Calculate ='A' "
                End If
                clsDBFuncationality.ExecuteNonQuery(Query, trans)
                Query = "update tspl_QuickSettleMent_Detail set Amount=" + clsCommon.myCstr(0) + " from tspl_QuickSettleMent_Detail inner join  tspl_SettleMent_Master on tspl_QuickSettleMent_Detail.SettleMent_Code =tspl_SettleMent_Master.SettleMentCode  where (Type='Q' or Type='B') and SettleMent_Type ='CSE' and Calculate ='S' and Quick_SettleMent_Id ='" + QuickSettlementNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Query, trans)
            ElseIf CashShEx = 0 Then
                Query = "update tspl_QuickSettleMent_Detail set Amount=" + clsCommon.myCstr(0) + " from tspl_QuickSettleMent_Detail inner join  tspl_SettleMent_Master on tspl_QuickSettleMent_Detail.SettleMent_Code =tspl_SettleMent_Master.SettleMentCode  where (Type='Q' or Type='B') and SettleMent_Type ='CSE' and Calculate ='A' and Quick_SettleMent_Id ='" + QuickSettlementNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Query, trans)
                Query = "update tspl_QuickSettleMent_Detail set Amount=" + clsCommon.myCstr(0) + " from tspl_QuickSettleMent_Detail inner join  tspl_SettleMent_Master on tspl_QuickSettleMent_Detail.SettleMent_Code =tspl_SettleMent_Master.SettleMentCode  where (Type='Q' or Type='B') and SettleMent_Type ='CSE' and Calculate ='S' and Quick_SettleMent_Id ='" + QuickSettlementNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Query, trans)
            End If
            If EmptyShEx > 0 Then
                Query = "select 1 from tspl_QuickSettleMent_Detail inner join  tspl_SettleMent_Master on tspl_QuickSettleMent_Detail.SettleMent_Code =tspl_SettleMent_Master.SettleMentCode  where (Type='Q' or Type='B') and SettleMent_Type ='ESE' and Calculate ='S' and Quick_SettleMent_Id ='" + QuickSettlementNo + "'"
                FlagAccCheck = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Query, trans))
                If FlagAccCheck = 1 Then
                    Query = "update tspl_QuickSettleMent_Detail set Amount=" + clsCommon.myCstr(EmptyShEx) + " from tspl_QuickSettleMent_Detail inner join  tspl_SettleMent_Master on tspl_QuickSettleMent_Detail.SettleMent_Code =tspl_SettleMent_Master.SettleMentCode  where (Type='Q' or Type='B') and SettleMent_Type ='ESE' and Calculate ='S' and Quick_SettleMent_Id ='" + QuickSettlementNo + "'"
                Else
                    Query = "insert into tspl_QuickSettleMent_Detail select '" + QuickSettlementNo + "', SettleMentCode ,Description," + clsCommon.myCstr(EmptyShEx) + ",'' from tspl_SettleMent_Master where (Type='Q' or Type='B') and SettleMent_Type ='ESE' and Calculate ='S' "
                End If
                clsDBFuncationality.ExecuteNonQuery(Query, trans)
                Query = "update tspl_QuickSettleMent_Detail set Amount=" + clsCommon.myCstr(0) + " from tspl_QuickSettleMent_Detail inner join  tspl_SettleMent_Master on tspl_QuickSettleMent_Detail.SettleMent_Code =tspl_SettleMent_Master.SettleMentCode  where (Type='Q' or Type='B') and SettleMent_Type ='ESE' and Calculate ='A' and Quick_SettleMent_Id ='" + QuickSettlementNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Query, trans)
            ElseIf EmptyShEx < 0 Then
                Query = "select 1 from tspl_QuickSettleMent_Detail inner join  tspl_SettleMent_Master on tspl_QuickSettleMent_Detail.SettleMent_Code =tspl_SettleMent_Master.SettleMentCode  where (Type='Q' or Type='B') and SettleMent_Type ='ESE' and Calculate ='A' and Quick_SettleMent_Id ='" + QuickSettlementNo + "'"
                FlagAccCheck = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Query, trans))
                If FlagAccCheck = 1 Then
                    Query = "update tspl_QuickSettleMent_Detail set Amount=" + clsCommon.myCstr(EmptyShEx * -1) + " from tspl_QuickSettleMent_Detail inner join  tspl_SettleMent_Master on tspl_QuickSettleMent_Detail.SettleMent_Code =tspl_SettleMent_Master.SettleMentCode  where (Type='Q' or Type='B') and SettleMent_Type ='ESE' and Calculate ='A' and Quick_SettleMent_Id ='" + QuickSettlementNo + "'"
                Else
                    Query = "insert into tspl_QuickSettleMent_Detail select '" + QuickSettlementNo + "', SettleMentCode ,Description," + clsCommon.myCstr(EmptyShEx * -1) + ",'' from tspl_SettleMent_Master where (Type='Q' or Type='B') and SettleMent_Type ='ESE' and Calculate ='A' "
                End If
                clsDBFuncationality.ExecuteNonQuery(Query, trans)
                Query = "update tspl_QuickSettleMent_Detail set Amount=" + clsCommon.myCstr(0) + " from tspl_QuickSettleMent_Detail inner join  tspl_SettleMent_Master on tspl_QuickSettleMent_Detail.SettleMent_Code =tspl_SettleMent_Master.SettleMentCode  where (Type='Q' or Type='B') and SettleMent_Type ='ESE' and Calculate ='S' and Quick_SettleMent_Id ='" + QuickSettlementNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Query, trans)
            ElseIf EmptyShEx < 0 Then
                Query = "update tspl_QuickSettleMent_Detail set Amount=" + clsCommon.myCstr(0) + " from tspl_QuickSettleMent_Detail inner join  tspl_SettleMent_Master on tspl_QuickSettleMent_Detail.SettleMent_Code =tspl_SettleMent_Master.SettleMentCode  where (Type='Q' or Type='B') and SettleMent_Type ='ESE' and Calculate ='A' and Quick_SettleMent_Id ='" + QuickSettlementNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Query, trans)
                Query = "update tspl_QuickSettleMent_Detail set Amount=" + clsCommon.myCstr(0) + " from tspl_QuickSettleMent_Detail inner join  tspl_SettleMent_Master on tspl_QuickSettleMent_Detail.SettleMent_Code =tspl_SettleMent_Master.SettleMentCode  where (Type='Q' or Type='B') and SettleMent_Type ='ESE' and Calculate ='S' and Quick_SettleMent_Id ='" + QuickSettlementNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Query, trans)
            End If
            clsDBFuncationality.ExecuteNonQuery("update tspl_QuickSettleMent set Balance_Amount=0 where Quick_SettleMent_Id ='" + QuickSettlementNo + "'", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
