Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsProductionIssue

#Region "Variables"

    Public ISSUE_CODE As String
    Public ISSUED_BY As String
    Public ISSUED_BY_NAME As String
    Public ISSUED_TO As String
    Public ISSUED_TO_NAME As String
    Public DESCRIPTION As String
    Public COMMENTS As String
    Public ISSUE_DATE As Date
    Public EXP_DATE As Date
    Public LOCATION_CODE As String
    Public LOCATION_NAME As String
    Public LOCATION_CODE_FROM As String
    Public LOCATION_FROM_NAME As String
    Public POSTED As Boolean
    Public POSTING_DATE As Date
    Public TR_TYPE As String
    Public isAgainstReq As Boolean
    Public ObjList As List(Of clsProductionIssueDetail)
    Public isTrading As Boolean = False

#End Region

    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsProductionIssue
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String
            qry = "delete from TSPL_MF_ISSUE_DETAIL where ISSUE_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_MF_ISSUE where ISSUE_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsSerializeInvenotry.DeleteData("PROD_IS", strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsProductionIssue
        Dim obj As New clsProductionIssue()
        Dim objtr As New clsProductionIssueDetail()
        ObjList = New List(Of clsProductionIssueDetail)

        Dim qry As String = " SELECT TSPL_MF_ISSUE.*,TSPL_LOCATION_MASTER.Location_Desc,TSPL_EMPLOYEE_MASTER.Emp_Name,T1.Location_Desc AS [LOCATION_FROM_NAME], T2.Emp_Name AS [ISSUED_TO_NAME] FROM TSPL_MF_ISSUE  "
        qry += " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_MF_ISSUE.LOCATION_CODE "
        qry += " LEFT OUTER JOIN TSPL_LOCATION_MASTER T1 ON T1.Location_Code = TSPL_MF_ISSUE.LOCATION_CODE_FROM "
        qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_MF_ISSUE.ISSUED_BY "
        qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER T2 ON T2.EMP_CODE = TSPL_MF_ISSUE.ISSUED_TO "
        qry += " where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and ISSUE_CODE = (select MIN(ISSUE_CODE) from TSPL_MF_ISSUE)"
            Case NavigatorType.Last
                qry += " and ISSUE_CODE = (select Max(ISSUE_CODE) from TSPL_MF_ISSUE)"
            Case NavigatorType.Next
                qry += " and ISSUE_CODE = (select Min(ISSUE_CODE) from TSPL_MF_ISSUE where ISSUE_CODE > '" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and ISSUE_CODE = (select Max(ISSUE_CODE) from TSPL_MF_ISSUE where ISSUE_CODE < '" + strCode + "')"
            Case NavigatorType.Current
                qry += " and ISSUE_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.isAgainstReq = clsCommon.myCBool(dt.Rows(0)("isAgainstReq"))
            obj.ISSUE_CODE = clsCommon.myCstr(dt.Rows(0)("ISSUE_CODE"))
            obj.ISSUED_BY = clsCommon.myCstr(dt.Rows(0)("ISSUED_BY"))
            obj.ISSUED_BY_NAME = clsCommon.myCstr(dt.Rows(0)("Emp_Name"))
            obj.ISSUED_TO = clsCommon.myCstr(dt.Rows(0)("ISSUED_TO"))
            obj.ISSUED_TO_NAME = clsCommon.myCstr(dt.Rows(0)("ISSUED_TO_NAME"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.COMMENTS = clsCommon.myCstr(dt.Rows(0)("COMMENTS"))
            obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
            obj.LOCATION_NAME = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.TR_TYPE = clsCommon.myCstr(dt.Rows(0)("TR_TYPE"))

            obj.LOCATION_CODE_FROM = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE_FROM"))
            obj.LOCATION_FROM_NAME = clsCommon.myCstr(dt.Rows(0)("LOCATION_FROM_NAME"))
            obj.ISSUE_DATE = clsCommon.myCstr(clsCommon.GetPrintDate(dt.Rows(0)("ISSUE_DATE"), "dd/MMM/yyyy"))
            obj.EXP_DATE = clsCommon.myCstr(clsCommon.GetPrintDate(dt.Rows(0)("EXP_DATE"), "dd/MMM/yyyy"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            obj.isTrading = clsCommon.myCBool(dt.Rows(0)("isTrading"))
            If clsCommon.myLen(dt.Rows(0)("POSTING_DATE")) > 0 Then
                obj.POSTING_DATE = clsCommon.myCstr(clsCommon.GetPrintDate(dt.Rows(0)("POSTING_DATE"), "dd/MMM/yyyy"))
            Else
                obj.POSTING_DATE = Nothing
            End If
            obj.ObjList = objtr.GetData(obj.ISSUE_CODE, trans)
        End If
        Return obj
    End Function
    Public Function SaveData(ByVal obj As clsProductionIssue, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            If isNewEntry Then
                If (clsCommon.myLen(obj.ISSUE_CODE) <= 0) Then
                    obj.ISSUE_CODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(obj.ISSUE_DATE, "dd/MMM/yyyy"), clsDocType.ProductionIssue, "", "")
                End If
            End If

            If (clsCommon.myLen(obj.ISSUE_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Standard Production", "Store Issue", obj.LOCATION_CODE, obj.ISSUE_DATE, trans)
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "isAgainstReq", obj.isAgainstReq)
            clsCommon.AddColumnsForChange(coll, "ISSUED_BY", obj.ISSUED_BY)
            clsCommon.AddColumnsForChange(coll, "ISSUED_TO", obj.ISSUED_TO)
            clsCommon.AddColumnsForChange(coll, "COMMENTS", obj.COMMENTS)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE)
            clsCommon.AddColumnsForChange(coll, "TR_TYPE", obj.TR_TYPE)

            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE_FROM", obj.LOCATION_CODE_FROM)
            clsCommon.AddColumnsForChange(coll, "ISSUE_DATE", clsCommon.GetPrintDate(obj.ISSUE_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "EXP_DATE", clsCommon.GetPrintDate(obj.EXP_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            Dim dblTrading As Integer = 0
            If obj.isTrading = True Then
                dblTrading = 1
            End If
            clsCommon.AddColumnsForChange(coll, "isTrading", dblTrading)


            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "ISSUE_CODE", obj.ISSUE_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_ISSUE", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_ISSUE", OMInsertOrUpdate.Update, "TSPL_MF_ISSUE.ISSUE_CODE='" + obj.ISSUE_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsProductionIssueDetail.SaveData(obj.ISSUE_CODE, obj.ObjList, trans)

            
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As New clsProductionIssue
            obj = obj.GetData(strDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.ISSUE_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Dim isSaved As Boolean = clsProductionIssue.UpdateInventoryMovement(strDocNo, trans)
            If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CreateJEOnProduction, clsFixedParameterCode.CreateJEOnProduction, trans)) = "1" Then
                isSaved = isSaved And JournalEntry(trans, obj, "")
            End If
            Dim qry As String = "Update TSPL_MF_ISSUE set POSTED=1, POSTING_DATE ='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where ISSUE_CODE ='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
            'If obj.SaveDataInIssueReturn(obj, True) Then
            '    Dim qry As String = "Update TSPL_MF_ISSUE set POSTED=1, POSTING_DATE ='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where ISSUE_CODE ='" + strDocNo + "'"
            '    clsDBFuncationality.ExecuteNonQuery(qry)
            'Else
            '    Throw New Exception("Data Not Saved in Issue/Return/Transfer.")
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function JournalEntry(ByVal trans As SqlTransaction, ByVal obj1 As clsProductionIssue, Optional ByVal strVourcherNoForRecreateOnly As String = "") As Boolean
        Try
            Dim VoucherNo As String = ""
            If clsCommon.myLen(strVourcherNoForRecreateOnly) > 0 Then
                VoucherNo = strVourcherNoForRecreateOnly
            Else
                VoucherNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='SP-IS' and Source_Doc_No='" & obj1.ISSUE_CODE & "'", trans))
            End If

            Dim JRNL_DATE As Date = obj1.ISSUE_DATE
            Dim Count As Integer = 0
            Dim qry As String
            Dim dtGL As DataTable
            Dim TotalDebitAmt As Decimal = 0
            Dim TotalCreditAmt As Decimal = 0
            Dim isSaved As Boolean = True
            Dim VoucherDesc As String = "Financial Entry for Issue- " & obj1.ISSUE_CODE & " "
            Dim SourceDocDesc As String = obj1.DESCRIPTION
            Dim SourceDocNo As String = obj1.ISSUE_CODE
            Dim Comments As String = obj1.DESCRIPTION
            Dim Remarks As String = obj1.DESCRIPTION

            Dim i As Integer = 0
            qry = " select Issue.*,CrGL.Description as CreditAccountDesc,DrGL.Description as DebitAccountDesc  from (
                 select TSPL_MF_ISSUE.ISSUE_CODE,TSPL_MF_ISSUE_DETAIL.ITEM_CODE AS PROD_ITEM_CODE,
                 (CASE WHEN SUBSTRING(RIGHT(TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,4),1,1)='-' THEN
                 REPLACE(TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,RIGHT(TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,4),'-'
                 +LEFT(COALESCE(TSPL_LOCATION_MASTER.Loc_Segment_Code,''),3))
                 ELSE TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account+ '-' + LEFT(COALESCE(TSPL_LOCATION_MASTER.Loc_Segment_Code,''),3) END) as CreditAccount,
                 (CASE WHEN SUBSTRING(RIGHT(TSPL_PURCHASE_ACCOUNTS.WIP_Account,4),1,1)='-' THEN REPLACE(TSPL_PURCHASE_ACCOUNTS.WIP_Account,
                 RIGHT(TSPL_PURCHASE_ACCOUNTS.WIP_Account,4),'-' +LEFT(COALESCE(TSPL_LOCATION_MASTER.Loc_Segment_Code,''),3))
                 ELSE TSPL_PURCHASE_ACCOUNTS.WIP_Account+ '-'+LEFT(COALESCE(TSPL_LOCATION_MASTER.Loc_Segment_Code,''),3) END) as DebitAccount,
                 sum(TSPL_MF_ISSUE_DETAIL.Avg_Cost) as ItemCost from TSPL_MF_ISSUE_DETAIL
                 inner join TSPL_MF_ISSUE on TSPL_MF_ISSUE_DETAIL.ISSUE_CODE=TSPL_MF_ISSUE.ISSUE_CODE
                 inner join TSPL_ITEM_MASTER on TSPL_MF_ISSUE_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code
                 left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code
                 left join TSPL_LOCATION_MASTER on TSPL_MF_ISSUE.LOCATION_CODE_FROM=TSPL_LOCATION_MASTER.LOCATION_CODE
                 WHERE TSPL_MF_ISSUE_DETAIL.ISSUE_CODE='" + obj1.ISSUE_CODE + "'
                  group by TSPL_MF_ISSUE.ISSUE_CODE,
                 TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.WIP_Account,TSPL_MF_ISSUE.LOCATION_CODE_FROM,
                 COALESCE(TSPL_LOCATION_MASTER.Loc_Segment_Code,''),
                 TSPL_MF_ISSUE_DETAIL.ITEM_CODE
				 ) as Issue left join TSPL_GL_ACCOUNTS as CrGL on Issue.CreditAccount=CrGL.Account_Code
                 left join TSPL_GL_ACCOUNTS as DrGL on Issue.DebitAccount=DrGL.Account_Code"
            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            For Each grow As DataRow In dtGL.Rows
                If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
                    Throw New Exception("Inventory Control Account not found for Raw Materials of Item " & grow.Item("PROD_ITEM_CODE") & "")
                    Return False
                End If
                If clsCommon.myLen(grow.Item("DebitAccount")) <= 0 Then
                    Throw New Exception("WIP Account not found for Item " & grow.Item("PROD_ITEM_CODE") & "")
                    Return False
                End If
                If clsCommon.myLen(grow.Item("CreditAccountDesc")) <= 0 Then
                    Throw New Exception("Inventory Control Account " & grow.Item("CreditAccount") & " does not exist for Raw Materials of Item " & grow.Item("PROD_ITEM_CODE") & "")
                    Return False
                End If
                If clsCommon.myLen(grow.Item("DebitAccountDesc")) <= 0 Then
                    Throw New Exception("WIP Account " & grow.Item("DebitAccount") & " does not exist for Item " & grow.Item("PROD_ITEM_CODE") & "")
                    Return False
                End If
                Count += 1
                TotalCreditAmt = TotalCreditAmt + clsCommon.myCdbl(grow.Item("ItemCost"))
                TotalDebitAmt = TotalDebitAmt + clsCommon.myCdbl(grow.Item("ItemCost"))
            Next
            If Count <= 0 Then
                Throw New Exception("No Data found")
                Return False
            End If
            Dim ArryLstGLAC As ArrayList = New ArrayList()

            qry = "SELECT ISSUE_CODE,PROD_ITEM_CODE,DebitAccount,'' AS CreditAccount,DebitAccountDesc,'' AS CreditAccountDesc,SUM(ITEMCOST) AS ITEMCOST FROM (" &
            " " & qry & " ) AS FINAL GROUP BY ISSUE_CODE,PROD_ITEM_CODE,DebitAccount,DebitAccountDesc" &
            " Union all" &
            " SELECT ISSUE_CODE,PROD_ITEM_CODE,'' AS DebitAccount,CreditAccount,'' AS DebitAccountDesc,CreditAccountDesc,SUM(ITEMCOST) AS ITEMCOST FROM (" &
            " " & qry & " ) AS FINAL GROUP BY ISSUE_CODE,PROD_ITEM_CODE,CreditAccount,CreditAccountDesc"

            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            If (dtGL IsNot Nothing AndAlso dtGL.Rows.Count > 0) Then
                For Each dr As DataRow In dtGL.Rows
                    '' debit
                    Dim DebitAcc As String = clsCommon.myCstr(dr.Item("DebitAccount"))
                    If clsCommon.myLen(DebitAcc) > 0 Then
                        Dim Acc1() As String = {DebitAcc, 1 * clsCommon.myCdbl(dr("ITEMCOST"))}
                        ArryLstGLAC.Add(Acc1)
                    End If
                    '' credit
                    Dim CreditAcc As String = clsCommon.myCstr(dr.Item("CreditAccount"))
                    If clsCommon.myLen(CreditAcc) > 0 Then
                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                            Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(dr("ITEMCOST"))}
                            ArryLstGLAC.Add(Acc2)
                        Else
                            Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(dr("ITEMCOST")), "", "", "", "", "", "", "I"}
                            ArryLstGLAC.Add(Acc2)
                            clsInventoryMovement.UpdateInvControlAccount(clsCommon.myCstr(dr("ISSUE_CODE")), clsUserMgtCode.frmProcessProductionIssueEntry, clsCommon.myCstr(dr("PROD_ITEM_CODE")), "", CreditAcc, "O", trans)
                        End If
                    End If
                Next
                Dim GLDesc As String = "Journal Entry Against Std Production Issue No." & obj1.ISSUE_CODE & " "
                If clsCommon.myLen(VoucherNo) > 0 Then
                    isSaved = isSaved AndAlso transportSql.FunGrnlEntryWithTrans(obj1.LOCATION_CODE_FROM, False, VoucherNo, trans, obj1.ISSUE_DATE, GLDesc, "SP-IS", "Std Prod Issue", obj1.ISSUE_CODE, obj1.DESCRIPTION, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, GLDesc, "")
                Else
                    isSaved = isSaved AndAlso transportSql.FunGrnlEntryWithTrans(obj1.LOCATION_CODE_FROM, False, trans, obj1.ISSUE_DATE, GLDesc, "SP-IS", "Std Prod Issue", obj1.ISSUE_CODE, obj1.DESCRIPTION, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , GLDesc, "")
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Function SaveDataInIssueReturn(ByVal obj As clsProductionIssue, ByVal isNewEntry As Boolean) As Boolean

        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            'Dim qry As String = "delete from TSPL_IssueReturn_DETAIL where Doc_No='" + obj.Doc_No + "'"
            'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            Dim strDesc As String = "Entry from Production Issue. Remark : "
            If isNewEntry Then
                strDocNo = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.ISSUE_DATE), clsDocType.IssueReturn, clsDocTransactionType.ItemIssue, obj.LOCATION_CODE_FROM)
            End If

            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.ISSUE_DATE, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Doc_Type", "Issue")
            clsCommon.AddColumnsForChange(coll, "From_Location", obj.LOCATION_CODE_FROM)
            clsCommon.AddColumnsForChange(coll, "To_Location", obj.LOCATION_CODE)
            clsCommon.AddColumnsForChange(coll, "Remarks", strDesc + obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "Comment", obj.COMMENTS)
            clsCommon.AddColumnsForChange(coll, "Issue_To", obj.ISSUED_TO)
            clsCommon.AddColumnsForChange(coll, "Request_By", obj.ISSUED_BY)
            'clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            'clsCommon.AddColumnsForChange(coll, "Req_IssueNo", obj.ISSUE_CODE)
            'clsCommon.AddColumnsForChange(coll, "RequisitionNo", obj.RequisitionNo)

            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Doc_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_IssueReturn_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_IssueReturn_HEAD", OMInsertOrUpdate.Update, "TSPL_IssueReturn_HEAD.Doc_No='" + strDocNo + "'", trans)
            End If
            isSaved = isSaved AndAlso SaveDataInIssueReturnDetail(strDocNo, obj.ObjList, trans)
            isSaved = isSaved AndAlso clsIssueReturnHead.PostData(strDocNo, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveDataInIssueReturnDetail(ByVal strDocNo As String, ByVal Arr As List(Of clsProductionIssueDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim LineNo As Int16 = 0
            For Each obj As clsProductionIssueDetail In Arr
                Dim coll As New Hashtable()
                LineNo = LineNo + 1
                clsCommon.AddColumnsForChange(coll, "Doc_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", LineNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.ITEM_CODE)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.ITEM_DESCRIPTION)
                clsCommon.AddColumnsForChange(coll, "Required_Qty", obj.REQ_QTY)
                clsCommon.AddColumnsForChange(coll, "Issued_Qty", obj.ISSUE_QTY)

                'clsCommon.AddColumnsForChange(coll, "Issued_Qty_AgainstRet", obj.Issued_Qty_AgainstRet)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.UNIT_CODE)
                'clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                Dim CostingMethod As Integer = 0
                Dim Amount As Decimal = 0
                CostingMethod = GetCostingMethod(obj.ITEM_CODE, trans)
                clsCommon.AddColumnsForChange(coll, "Cost_Code", "", True)
                If CostingMethod = 0 Then
                    Amount = clsInventoryMovement.GetCost(EnumCostingMethod.FIFO, obj.ITEM_CODE, obj.From_Location, obj.ISSUE_QTY, obj.Doc_Date, clsCommon.GETSERVERDATE(trans), obj.IsPosted, trans)
                    clsCommon.AddColumnsForChange(coll, "Unit_Cost", IIf(obj.ISSUE_QTY > 0, (Amount / obj.ISSUE_QTY), 0))
                ElseIf CostingMethod = 1 Then
                    Amount = clsInventoryMovement.GetCost(EnumCostingMethod.LIFO, obj.ITEM_CODE, obj.From_Location, obj.ISSUE_QTY, obj.Doc_Date, clsCommon.GETSERVERDATE(trans), obj.IsPosted, trans)
                    clsCommon.AddColumnsForChange(coll, "Unit_Cost", IIf(obj.ISSUE_QTY > 0, (Amount / obj.ISSUE_QTY), 0))
                ElseIf CostingMethod = 2 Then
                    Amount = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, obj.ITEM_CODE, obj.From_Location, obj.ISSUE_QTY, obj.Doc_Date, clsCommon.GETSERVERDATE(trans), obj.IsPosted, trans)
                    clsCommon.AddColumnsForChange(coll, "Unit_Cost", IIf(obj.ISSUE_QTY > 0, (Amount / obj.ISSUE_QTY), 0))
                End If
                clsCommon.AddColumnsForChange(coll, "Amount", Amount)
                clsCommon.AddColumnsForChange(coll, "Req_IssueNo", obj.REQ_CODE)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_IssueReturn_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function GetCostingMethod(ByVal ItemCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Integer
        Dim qry As String = ""
        qry = " SELECT coalesce(TSPL_PURCHASE_ACCOUNTS.Costing_Method,0) as Costing_Method " & _
              " FROM TSPL_ITEM_MASTER  left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " & _
              " WHERE TSPL_ITEM_MASTER.Item_Code='" & ItemCode & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("Costing_Method")
        Else
            Return 0
        End If
    End Function
    Public Shared Function UpdateInventoryMovement(ByVal Issue_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            Dim obj As clsInventoryMovement
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            'Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()

            Dim strq As String = ""
            Dim objIssue As New clsProductionIssue
            objIssue = objIssue.GetData(Issue_Code, NavigatorType.Current, trans)
            Dim objListIssue As List(Of clsProductionIssueDetail) = objIssue.ObjList
            ' '' get MO cost 
            'strq = "select (TOTAL_COST-DIRECT_MATERIAL_COST) as TOTAL_COST from TSPL_MF_MO_COSTING where MO_CODE='" & objIssue.MO_CODE & "' and CALC_TYPE='Actual'"
            'Dim dtMOCost As DataTable
            'dtMOCost = clsDBFuncationality.GetDataTable(strq, trans)
            'Dim MOCost As Double = 0.0
            'If dtMOCost.Rows.Count > 0 Then
            '    MOCost = dtMOCost.Rows(0).Item("TOTAL_COST")
            'End If

            If (objListIssue IsNot Nothing AndAlso objListIssue.Count > 0) Then
                For Each objProd As clsProductionIssueDetail In objListIssue

                    Dim strItemTypeToSave As String = ""
                    Dim strItemType As String

                    '' in produced item
                    obj = New clsInventoryMovement
                    obj.Trans_Type = "Issue"
                    obj.InOut = "I"
                    obj.Location_Code = objIssue.LOCATION_CODE
                    obj.Item_Code = objProd.ITEM_CODE
                    obj.Item_Desc = objProd.ITEM_DESCRIPTION
                    obj.Qty = objProd.ISSUE_QTY
                    obj.UOM = objProd.UNIT_CODE
                    obj.Source_Doc_No = objIssue.ISSUE_CODE
                    obj.Source_Doc_Date = objIssue.ISSUE_DATE

                    strItemType = clsItemMaster.GetItemType(objProd.ITEM_CODE, trans)
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        strItemTypeToSave = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        strItemTypeToSave = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        strItemTypeToSave = "FT"
                    Else
                        strItemTypeToSave = strItemType
                        'Throw New Exception("Item Type not found: " + strItemType)
                    End If
                    obj.ItemType = strItemTypeToSave
                    
                    obj.Basic_Cost = objProd.Avg_Cost / IIf(objProd.ISSUE_QTY = 0, 1, objProd.ISSUE_QTY)
                    obj.MRP = objProd.Avg_Cost / IIf(objProd.ISSUE_QTY = 0, 1, objProd.ISSUE_QTY)
                    obj.Add_Cost = 0
                    obj.FIFO_Cost = objProd.FIFO_Cost
                    obj.LIFO_Cost = objProd.LIFO_Cost
                    obj.Avg_Cost = objProd.Avg_Cost

                    ArrInventoryMovement.Add(obj)
                Next


            End If


            '' out data
            If (objListIssue IsNot Nothing AndAlso objListIssue.Count > 0) Then
                For Each objProd As clsProductionIssueDetail In objListIssue

                    Dim strItemTypeToSave As String = ""
                    Dim strItemType As String

                    '' out issued item
                    obj = New clsInventoryMovement
                    obj.Trans_Type = "Issue"
                    obj.InOut = "O"
                    obj.Location_Code = objIssue.LOCATION_CODE_FROM

                    obj.Other_Location_Code = objIssue.LOCATION_CODE
                    obj.Other_Location_Desc = objIssue.LOCATION_NAME

                    obj.Item_Code = objProd.ITEM_CODE
                    obj.Item_Desc = objProd.ITEM_DESCRIPTION
                    obj.Qty = objProd.ISSUE_QTY
                    obj.UOM = objProd.UNIT_CODE
                    obj.Source_Doc_No = objIssue.ISSUE_CODE
                    obj.Source_Doc_Date = objIssue.ISSUE_DATE

                    strItemType = clsItemMaster.GetItemType(objProd.ITEM_CODE, trans)
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        strItemTypeToSave = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        strItemTypeToSave = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        strItemTypeToSave = "FT"
                    Else
                        strItemTypeToSave = strItemType
                        'Throw New Exception("Item Type not found: " + strItemType)
                    End If
                    obj.ItemType = strItemTypeToSave

                    obj.Basic_Cost = 0
                    obj.MRP = 0
                    obj.Add_Cost = 0
                    obj.MRP = 0

                    obj.FIFO_Cost = objProd.FIFO_Cost
                    obj.LIFO_Cost = objProd.LIFO_Cost
                    obj.Avg_Cost = objProd.Avg_Cost
                    ArrInventoryMovement.Add(obj)
                Next

            End If
            clsInventoryMovement.SaveData("Prod-Issue", Issue_Code, clsCommon.GetPrintDate(objIssue.ISSUE_DATE, "dd/MMM/yyyy"), clsCommon.GetPrintDate(objIssue.ISSUE_DATE, "dd/MM/yyyy"), ArrInventoryMovement, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False

        End Try
        Return True
    End Function
End Class

Public Class clsProductionIssueDetail
#Region "Variables"

    Public ISSUE_CODE As String
    Public REQ_CODE As String
    Public PRODUCTION_LINE_CODE As String
    Public BOM_CODE As String

    Public ITEM_CODE As String
    Public ITEM_DESCRIPTION As String
    Public BATCH_QTY As Double
    Public REQ_QTY As Double
    Public ISSUE_QTY As Double
    Public UNIT_CODE As String
    Public REMARKS As String
    Public TR_TYPE As String
    Public Doc_Date As DateTime
    Public From_Location As String
    Public To_Location As String
    Public PostingDate As DateTime
    Public IsPosted As Boolean
    Public BO_CODE As String
    Public MO_CODE As String
    Public FIFO_Cost As Decimal
    Public LIFO_Cost As Decimal
    Public Avg_Cost As Decimal


    Public ObjList As List(Of clsProductionIssueDetail)
    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsProductionIssueDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String
            qry = " delete from TSPL_MF_ISSUE_DETAIL where ISSUE_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsSerializeInvenotry.DeleteData("PROD_IS", strDocNo, trans)

            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsProductionIssueDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "ISSUE_CODE", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "REQ_CODE", obj.REQ_CODE, True)
                    clsCommon.AddColumnsForChange(coll, "BO_CODE", obj.BO_CODE, True)
                    clsCommon.AddColumnsForChange(coll, "MO_CODE", obj.MO_CODE, True)

                    clsCommon.AddColumnsForChange(coll, "PRODUCTION_LINE_CODE", obj.PRODUCTION_LINE_CODE, True)
                    clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE, True)

                    clsCommon.AddColumnsForChange(coll, "ITEM_DESCRIPTION", obj.ITEM_DESCRIPTION)
                    clsCommon.AddColumnsForChange(coll, "ITEM_CODE", obj.ITEM_CODE)
                    clsCommon.AddColumnsForChange(coll, "BATCH_QTY", obj.BATCH_QTY)
                    clsCommon.AddColumnsForChange(coll, "ISSUE_QTY", obj.ISSUE_QTY)
                    clsCommon.AddColumnsForChange(coll, "REQ_QTY", obj.REQ_QTY)
                    clsCommon.AddColumnsForChange(coll, "UNIT_CODE", obj.UNIT_CODE)
                    clsCommon.AddColumnsForChange(coll, "REMARKS", obj.REMARKS)
                    clsCommon.AddColumnsForChange(coll, "TR_TYPE", obj.TR_TYPE)
                    '' saving production costing 
                    clsCommon.AddColumnsForChange(coll, "FIFO_Cost", clsInventoryMovement.GetCost(EnumCostingMethod.FIFO, obj.ITEM_CODE, obj.From_Location, obj.ISSUE_QTY, obj.Doc_Date, clsCommon.GETSERVERDATE(trans), obj.IsPosted, trans))
                    clsCommon.AddColumnsForChange(coll, "LIFO_Cost", clsInventoryMovement.GetCost(EnumCostingMethod.LIFO, obj.ITEM_CODE, obj.From_Location, obj.ISSUE_QTY, obj.Doc_Date, clsCommon.GETSERVERDATE(trans), obj.IsPosted, trans))
                    clsCommon.AddColumnsForChange(coll, "Avg_Cost", clsInventoryMovement.GetCost(EnumCostingMethod.Averege, obj.ITEM_CODE, obj.From_Location, obj.ISSUE_QTY, obj.Doc_Date, clsCommon.GETSERVERDATE(trans), obj.IsPosted, trans))


                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_ISSUE_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                    '' to stop REQUISITION update after posting
                    qry = ""
                    qry = " update TSPL_MF_REQUISITION set ISUSED = 1 where REQ_CODE ='" + obj.REQ_CODE + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    If Not obj.arrSrItem Is Nothing AndAlso obj.arrSrItem.Count > 0 Then

                        clsSerializeInvenotry.SaveData("PROD_IS", strDocNo, obj.Doc_Date, "O", obj.ITEM_CODE, obj.From_Location, (Arr.IndexOf(obj) + 1), obj.arrSrItem, trans) 'out from Fromloc
                        clsSerializeInvenotry.SaveData("PROD_IS", strDocNo, obj.Doc_Date, "I", obj.ITEM_CODE, obj.To_Location, (Arr.IndexOf(obj) + 1), obj.arrSrItem, trans) 'in at to loc
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsProductionIssueDetail)
        Dim qry As String = " "
        qry += " select * FROM TSPL_MF_ISSUE_DETAIL inner join TSPL_MF_ISSUE on TSPL_MF_ISSUE_DETAIL.ISSUE_CODE=TSPL_MF_ISSUE.ISSUE_CODE "
        qry += " where TSPL_MF_ISSUE.ISSUE_CODE = '" + strDocNo + "'"
        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsProductionIssueDetail
        ObjList = New List(Of clsProductionIssueDetail)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsProductionIssueDetail()
                objtr.ISSUE_CODE = clsCommon.myCstr(dr("ISSUE_CODE"))
                objtr.REQ_CODE = clsCommon.myCstr(dr("REQ_CODE"))
                objtr.PRODUCTION_LINE_CODE = clsCommon.myCstr(dr("PRODUCTION_LINE_CODE"))
                objtr.BOM_CODE = clsCommon.myCstr(dr("BOM_CODE"))
                objtr.ITEM_DESCRIPTION = clsCommon.myCstr(dr("ITEM_DESCRIPTION"))
                objtr.ITEM_CODE = clsCommon.myCstr(dr("ITEM_CODE"))
                objtr.BATCH_QTY = clsCommon.myCdbl(dr("BATCH_QTY"))
                objtr.REQ_QTY = clsCommon.myCdbl(dr("REQ_QTY"))
                objtr.ISSUE_QTY = clsCommon.myCdbl(dr("ISSUE_QTY"))
                objtr.UNIT_CODE = clsCommon.myCstr(dr("UNIT_CODE"))
                objtr.REMARKS = clsCommon.myCstr(dr("REMARKS"))
                objtr.TR_TYPE = clsCommon.myCstr(dr("TR_TYPE"))
                objtr.BO_CODE = clsCommon.myCstr(dr("BO_CODE"))
                objtr.MO_CODE = clsCommon.myCstr(dr("MO_CODE"))
                objtr.From_Location = clsCommon.myCstr(dr("Location_Code_From"))
                objtr.To_Location = clsCommon.myCstr(dr("Location_Code"))
                objtr.Doc_Date = clsCommon.myCDate(dr("Issue_Date"))

                objtr.FIFO_Cost = clsCommon.myCdbl(dr("FIFO_Cost"))
                objtr.LIFO_Cost = clsCommon.myCdbl(dr("LIFO_Cost"))
                objtr.Avg_Cost = clsCommon.myCdbl(dr("Avg_Cost"))
                'If clsCommon.myLen(objtr.PRODUCTION_LINE_CODE) <= 0 Then
                '    objtr.PRODUCTION_LINE_CODE = "1"
                'End If

                objtr.arrSrItem = clsSerializeInvenotry.GetData("PROD_IS", objtr.ISSUE_CODE, objtr.ITEM_CODE, (dt.Rows.IndexOf(dr) + 1), trans)
                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function
End Class
