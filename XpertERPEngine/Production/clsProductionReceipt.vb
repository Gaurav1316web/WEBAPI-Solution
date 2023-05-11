Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Public Class clsProductionReceipt

#Region "Variables"

    Public RECEIPT_CODE As String
    Public DESCRIPTION As String
    Public RECEIPT_DATE As Date
    Public BO_CODE As String
    Public ISSUE_CODE As String
    Public ISSUE_DATE As Date
    Public BATCH_DATE As Date
    Public RECEIVED_BY As String
    Public RECEIVED_BY_NAME As String
    Public LOCATION_CODE As String
    Public LOCATION_NAME As String
    Public COMMENTS As String

    Public CREATED_BY As String
    Public APPROVED_BY As String
    Public POSTED As Boolean
    Public Posting_Date As Date
    Public IS_TRADING As Boolean = False

    '' grid columns details
    Public PROD_PLAN_CODE As String
    Public PRODUCTION_LINE_CODE As String
    Public BOM_CODE As String

    Public ITEM_CODE As String
    Public ITEM_DESCRIPTION As String
    Public BATCH_QTY As Decimal
    Public ISSUE_QTY As Decimal

    Public BALANCE_BATCH_QTY As Decimal
    Public BALANCE_ISSUE_QTY As Decimal
    Public UNIT_CODE As String
    Public RECEIPT_QTY As Decimal

    Public REJ_HEAD As String
    Public REJ_QTY As Decimal

    Public BREAKAGE_HEAD As String
    Public BREAKAGE_QTY As Decimal

    Public LAB_TESTING As String

    Public START_TIME As DateTime? = Nothing
    Public END_TIME As DateTime? = Nothing

    Public MFG_DATE As Date
    Public EXP_DATE As Date
    Public TR_TYPE As String
    Public MO_CODE As String
    Public FIFO_Cost As Decimal
    Public LIFO_Cost As Decimal
    Public AVG_Cost As Decimal
    Public Costing_Method As Integer
    Public Attachment As String = Nothing
    Public FileName As String = Nothing

    Public Shared ObjList As List(Of clsProductionReceipt)
    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing
    Public ObjListAttachment As List(Of clsProductionReceiptAttachment) = Nothing
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsProductionReceipt
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

            clsSerializeInvenotry.DeleteData("Production", strCode, trans)
            Dim qry As String
            qry = "delete from TSPL_MF_RECEIPT_DETAIL where RECEIPT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MF_CONSUMPTION_DETAIL where RECEIPT_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MF_RECEIPT where RECEIPT_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

           
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsProductionReceipt
        Dim obj As New clsProductionReceipt()
        Dim objtr As New clsProductionReceipt()

        ObjList = New List(Of clsProductionReceipt)

        Dim qry As String = "SELECT T1.ISSUE_CODE, T1.ISSUE_DATE,T1.IS_TRADING, T1.RECEIPT_CODE,T1.DESCRIPTION,T1.RECEIPT_DATE,T1.BO_CODE, T1.BATCH_DATE,T1.RECEIVED_BY,T2.EMP_NAME,T1.LOCATION_CODE,T3.LOCATION_DESC,T1.COMMENTS,"
        qry += " T1.MODIFIED_BY AS APPROVED_BY,T1.Created_By,T1.POSTED,T1.POSTING_DATE,T1.TR_TYPE,T1.MO_CODE FROM TSPL_MF_RECEIPT  T1 INNER JOIN TSPL_EMPLOYEE_MASTER T2 ON T1.RECEIVED_BY=T2.EMP_CODE INNER JOIN TSPL_LOCATION_MASTER T3 ON T1.LOCATION_CODE=T3.LOCATION_CODE where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " AND RECEIPT_CODE = (select MIN(RECEIPT_CODE) from TSPL_MF_RECEIPT)"
            Case NavigatorType.Last
                qry += " AND RECEIPT_CODE = (select Max(RECEIPT_CODE) from TSPL_MF_RECEIPT)"
            Case NavigatorType.Next
                qry += " AND RECEIPT_CODE = (select Min(RECEIPT_CODE) from TSPL_MF_RECEIPT where  RECEIPT_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " AND RECEIPT_CODE = (select Max(RECEIPT_CODE) from TSPL_MF_RECEIPT where RECEIPT_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " AND RECEIPT_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj.RECEIPT_CODE = dt.Rows(0)("RECEIPT_CODE")
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.RECEIPT_DATE = clsCommon.GetPrintDate(dt.Rows(0)("RECEIPT_DATE"), "dd/MMM/yyyy")
            obj.BO_CODE = clsCommon.myCstr(dt.Rows(0)("BO_CODE"))
            If clsCommon.myLen(clsCommon.myLen(obj.BO_CODE)) > 0 Then
                obj.BATCH_DATE = clsCommon.GetPrintDate(dt.Rows(0)("BATCH_DATE"), "dd/MMM/yyyy")
            End If

            obj.RECEIVED_BY = clsCommon.myCstr(dt.Rows(0)("RECEIVED_BY"))
            obj.RECEIVED_BY_NAME = clsCommon.myCstr(dt.Rows(0)("EMP_NAME"))
            obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
            obj.LOCATION_NAME = clsCommon.myCstr(dt.Rows(0)("LOCATION_DESC"))
            obj.COMMENTS = clsCommon.myCstr(dt.Rows(0)("COMMENTS"))

            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.APPROVED_BY = clsCommon.myCstr(dt.Rows(0)("APPROVED_BY"))
            obj.POSTED = clsCommon.myCstr(dt.Rows(0)("POSTED"))
            obj.TR_TYPE = clsCommon.myCstr(dt.Rows(0)("TR_TYPE"))
            obj.MO_CODE = clsCommon.myCstr(dt.Rows(0)("MO_CODE"))
            obj.ISSUE_CODE = clsCommon.myCstr(dt.Rows(0)("ISSUE_CODE"))
            obj.IS_TRADING = clsCommon.myCBool(dt.Rows(0)("IS_TRADING"))
            If clsCommon.myLen(clsCommon.myCstr(obj.ISSUE_CODE)) > 0 Then
                obj.ISSUE_DATE = clsCommon.GetPrintDate(dt.Rows(0)("ISSUE_DATE"), "dd/MMM/yyyy")
            End If

            strCode = dt.Rows(0)("RECEIPT_CODE")

            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If
        qry = ""
        qry += "SELECT T1.*,TSPL_ATTACHMENTS.FileName,T2.PLAN_FOR_DATE,coalesce(TSPL_PURCHASE_ACCOUNTS.Costing_Method,0) as Costing_Method FROM  TSPL_MF_RECEIPT_DETAIL T1 LEFT JOIN TSPL_MF_PRODUCTION_PLAN_HEAD T2 " &
        " ON T1.PROD_PLAN_CODE=T2.PROD_PLAN_CODE left join TSPL_ITEM_MASTER on T1.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " &
        " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code left outer join TSPL_ATTACHMENTS on TSPL_ATTACHMENTS.CODE = T1.Attachment  WHERE 2=2 "
        qry += " AND T1.RECEIPT_CODE = '" + strCode + "' ORDER BY T1.ITEM_CODE"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsProductionReceipt()
                objtr.PROD_PLAN_CODE = clsCommon.myCstr(dr("PROD_PLAN_CODE"))
                objtr.PRODUCTION_LINE_CODE = clsCommon.myCstr(dr("PRODUCTION_LINE_CODE"))
                objtr.BOM_CODE = clsCommon.myCstr(dr("BOM_CODE"))

                objtr.ITEM_CODE = clsCommon.myCstr(dr("ITEM_CODE"))
                objtr.ITEM_DESCRIPTION = clsCommon.myCstr(dr("ITEM_DESCRIPTION"))
                objtr.BATCH_QTY = clsCommon.myCstr(dr("BATCH_QTY"))
                objtr.BALANCE_BATCH_QTY = clsCommon.myCstr(dr("BALANCE_BATCH_QTY"))
                objtr.BALANCE_ISSUE_QTY = clsCommon.myCDecimal(dr("BALANCE_ISSUE_QTY"))
                objtr.RECEIPT_QTY = clsCommon.myCdbl(dr("RECEIPT_QTY"))
                objtr.REJ_HEAD = clsCommon.myCstr(dr("REJ_HEAD"))
                objtr.REJ_QTY = clsCommon.myCdbl(dr("REJ_QTY"))
                objtr.BREAKAGE_HEAD = clsCommon.myCstr(dr("BREAKAGE_HEAD"))
                objtr.BREAKAGE_QTY = clsCommon.myCdbl(dr("BREAKAGE_QTY"))
                objtr.LAB_TESTING = clsCommon.myCstr(dr("LAB_TESTING"))
                objtr.UNIT_CODE = clsCommon.myCstr(dr("UNIT_CODE"))
                objtr.UNIT_CODE = clsCommon.myCstr(dr("UNIT_CODE"))
                objtr.MFG_DATE = clsCommon.myCDate(dr("MFG_DATE"))
                objtr.EXP_DATE = clsCommon.myCDate(dr("EXP_DATE"))
                objtr.TR_TYPE = clsCommon.myCstr(dr("TR_TYPE"))
                objtr.FIFO_Cost = clsCommon.myCdbl(dr("FIFO_Cost"))
                objtr.LIFO_Cost = clsCommon.myCdbl(dr("LIFO_Cost"))
                objtr.AVG_Cost = clsCommon.myCdbl(dr("AVG_Cost"))
                objtr.ISSUE_QTY = clsCommon.myCdbl(dr("ISSUE_QTY"))
                objtr.Costing_Method = clsCommon.myCdbl(dr("Costing_Method"))
                objtr.Attachment = clsCommon.myCstr(dr("Attachment"))
                objtr.FileName = clsCommon.myCstr(dr("FileName"))
                If objtr.TR_TYPE = "BO" Then
                    If IsDBNull(dr("START_TIME")) = True Then
                        objtr.START_TIME = Nothing
                    Else

                        objtr.START_TIME = clsCommon.myCDate(dr("PLAN_FOR_DATE")).Add(dr("START_TIME"))
                    End If
                    If IsDBNull(dr("END_TIME")) = True Then
                        objtr.END_TIME = Nothing
                    Else
                        objtr.END_TIME = clsCommon.myCDate(dr("PLAN_FOR_DATE")).Add(dr("END_TIME"))
                    End If
                Else
                    objtr.START_TIME = Nothing
                    objtr.END_TIME = Nothing
                End If
                

                objtr.arrSrItem = clsSerializeInvenotry.GetData("Production", strCode, objtr.ITEM_CODE, (dt.Rows.IndexOf(dr) + 1), trans)
               
                ObjList.Add(objtr)
            Next
        End If

        clsProductionReceipt.ObjList = ObjList
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsProductionReceipt, ByVal objList As List(Of clsProductionReceipt), ByVal isNewEntry As Boolean, ByVal arrattachment As List(Of clsProductionReceiptAttachment), Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True


        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            If isNewEntry Then
                If strCode = "" Then
                    obj.RECEIPT_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.RECEIPT_DATE, "dd/MMM/yyyy"), clsDocType.ProductionReceipt, "", "")
                Else
                    obj.RECEIPT_CODE = strCode
                End If
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Standard Production", "Production Receipt", obj.LOCATION_CODE, obj.RECEIPT_DATE, trans)
            Dim qry As String = ""

            qry = "delete from TSPL_MF_CONSUMPTION_DETAIL where receipt_code='" & obj.RECEIPT_CODE & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MF_RECEIPT_DETAIL where receipt_code='" & obj.RECEIPT_CODE & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""

            If (clsCommon.myLen(obj.RECEIPT_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "RECEIPT_CODE", obj.RECEIPT_CODE)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "RECEIPT_DATE", clsCommon.GetPrintDate(obj.RECEIPT_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "BO_CODE", obj.BO_CODE, True)
            clsCommon.AddColumnsForChange(coll, "BATCH_DATE", clsCommon.GetPrintDate(obj.BATCH_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "RECEIVED_BY", clsCommon.myCstr(obj.RECEIVED_BY))
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", clsCommon.myCstr(obj.LOCATION_CODE))
            clsCommon.AddColumnsForChange(coll, "COMMENTS", clsCommon.myCstr(obj.COMMENTS))
            clsCommon.AddColumnsForChange(coll, "TR_TYPE", clsCommon.myCstr(obj.TR_TYPE))
            clsCommon.AddColumnsForChange(coll, "MO_CODE", clsCommon.myCstr(obj.MO_CODE), True)

            clsCommon.AddColumnsForChange(coll, "POSTED", "0")

            Dim dblISTrading As Integer = 0
            If obj.IS_TRADING = True Then
                clsCommon.AddColumnsForChange(coll, "ISSUE_CODE", clsCommon.myCstr(obj.ISSUE_CODE), True)
                clsCommon.AddColumnsForChange(coll, "IS_TRADING", 1)
                clsCommon.AddColumnsForChange(coll, "ISSUE_DATE", clsCommon.GetPrintDate(obj.ISSUE_DATE, "dd/MMM/yyyy"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "IS_TRADING", 0)
            End If

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then


                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_MF_RECEIPT where RECEIPT_CODE = '" & obj.RECEIPT_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_RECEIPT", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.RECEIPT_CODE + " Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_RECEIPT", OMInsertOrUpdate.Update, "TSPL_MF_RECEIPT.RECEIPT_CODE='" + obj.RECEIPT_CODE + "'", trans)
            End If

            isSaved = isSaved AndAlso clsProductionReceiptDetail.SaveDetailData(obj.RECEIPT_CODE, obj, objList, trans)
            '' CODE FOR SAVING CONSUMPTION DETAIL
            qry = ""
            If obj.IS_TRADING = False Then


                If obj.TR_TYPE = "BO" Then
                    clsProductionRM.SaveConsumeRMData(obj.RECEIPT_CODE, clsProductionRM.GetRMBO(obj, 1, 0, trans), trans)
                    'qry += " INSERT INTO TSPL_MF_CONSUMPTION_DETAIL(RECEIPT_CODE,PRODUCTION_LINE_CODE,CONSM_ITEM_CODE,CONSM_QTY,LOCATION_CODE,UNIT_CODE,FIFO_Cost,LIFO_Cost,Avg_Cost)"
                    'qry += " ("
                    'qry += " SELECT '" & obj.RECEIPT_CODE & "' AS RECEIPT_CODE,PRODUCTION_LINE_CODE,ITEM_CODE,SUM(QTY) AS QTY,'" & obj.LOCATION_CODE & "' AS LOCATION_CODE,UNIT_CODE," & clsInventoryMovement.GetCost(EnumCostingMethod.FIFO, obj.ITEM_CODE, obj.LOCATION_CODE, obj.RECEIPT_QTY, obj.RECEIPT_DATE, clsCommon.GETSERVERDATE(trans), obj.POSTED, trans) & "," & clsInventoryMovement.GetCost(EnumCostingMethod.LIFO, obj.ITEM_CODE, obj.LOCATION_CODE, obj.RECEIPT_QTY, obj.RECEIPT_DATE, clsCommon.GETSERVERDATE(trans), obj.POSTED, trans) & "," & clsInventoryMovement.GetCost(EnumCostingMethod.Averege, obj.ITEM_CODE, obj.LOCATION_CODE, obj.RECEIPT_QTY, obj.RECEIPT_DATE, clsCommon.GETSERVERDATE(trans), obj.POSTED, trans) & " FROM TSPL_MF_BATCH_ORDER_DETAIL"
                    'qry += " WHERE BO_CODE='" & obj.BO_CODE & "' GROUP BY PRODUCTION_LINE_CODE,ITEM_CODE,UNIT_CODE "
                    'qry += " )"
                Else
                    clsProductionRM.SaveConsumeRMData(obj.RECEIPT_CODE, clsProductionRM.GetRMMO(obj, 1, 0, trans), trans)
                    'qry += " INSERT INTO TSPL_MF_CONSUMPTION_DETAIL(RECEIPT_CODE,PRODUCTION_LINE_CODE,CONSM_ITEM_CODE,CONSM_QTY,LOCATION_CODE,UNIT_CODE,FIFO_Cost,LIFO_Cost,Avg_Cost)"
                    'qry += " ("
                    'qry += " SELECT TSPL_MF_RECEIPT.RECEIPT_CODE,cast((case when coalesce(TSPL_MF_MANUFACTURING_ORDER.PRODUCTION_AREA,'')='' then null else TSPL_MF_MANUFACTURING_ORDER.PRODUCTION_AREA end) as varchar(30)) as PRODUCTION_AREA,TSPL_MF_MO_MATERIAL.CONSM_ITEM_CODE,"
                    'qry += " ((TSPL_MF_MO_MATERIAL.CONSM_QUANTITY*(TSPL_MF_RECEIPT_DETAIL.RECEIPT_QTY+TSPL_MF_RECEIPT_DETAIL.REJ_QTY))/TSPL_MF_MANUFACTURING_ORDER.QTY_ORDERED_STOCK) as Qty, "
                    'qry += " TSPL_MF_RECEIPT.LOCATION_CODE,TSPL_MF_MO_MATERIAL.CONSM_ITEM_UNIT_CODE," & clsInventoryMovement.GetCost(EnumCostingMethod.FIFO, obj.ITEM_CODE, obj.LOCATION_CODE, obj.RECEIPT_QTY, obj.RECEIPT_DATE, clsCommon.GETSERVERDATE(trans), obj.POSTED, trans) & "," & clsInventoryMovement.GetCost(EnumCostingMethod.LIFO, obj.ITEM_CODE, obj.LOCATION_CODE, obj.RECEIPT_QTY, obj.RECEIPT_DATE, clsCommon.GETSERVERDATE(trans), obj.POSTED, trans) & "," & clsInventoryMovement.GetCost(EnumCostingMethod.Averege, obj.ITEM_CODE, obj.LOCATION_CODE, obj.RECEIPT_QTY, obj.RECEIPT_DATE, clsCommon.GETSERVERDATE(trans), obj.POSTED, trans) & "  from TSPL_MF_MO_MATERIAL inner join TSPL_MF_MANUFACTURING_ORDER on TSPL_MF_MO_MATERIAL.MO_CODE=TSPL_MF_MANUFACTURING_ORDER.MO_CODE "
                    'qry += " inner join TSPL_MF_RECEIPT on TSPL_MF_MANUFACTURING_ORDER.MO_CODE=TSPL_MF_RECEIPT.MO_CODE "
                    'qry += " inner join TSPL_MF_RECEIPT_DETAIL on TSPL_MF_RECEIPT_DETAIL.RECEIPT_CODE=TSPL_MF_RECEIPT.RECEIPT_CODE"
                    'qry += " WHERE TSPL_MF_MANUFACTURING_ORDER.MO_CODE='" & obj.MO_CODE & "' and TSPL_MF_RECEIPT.RECEIPT_CODE='" & obj.RECEIPT_CODE & "'"
                    'qry += " )"

                End If
            End If
            'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            isSaved = isSaved AndAlso clsProductionReceiptDetail.UpdateCostingRM(obj, trans)
            If isSaved Then
                trans.Commit()
                clsProductionReceiptAttachment.SaveData(obj.RECEIPT_CODE, arrattachment, trans)
            Else
                trans.Rollback()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal trans As SqlTransaction) As Boolean

        'Try
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Code not found to Post")
        End If
        Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
        Dim obj As clsProductionReceipt = clsProductionReceipt.GetData(strDocNo, NavigatorType.Current, trans)

        If (obj Is Nothing OrElse clsCommon.myLen(obj.RECEIPT_CODE) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If
        If (isCheckForPosted AndAlso obj.POSTED = 1) Then
            Throw New Exception("Already Post on :" + obj.Posting_Date)
        End If

        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.CreateJEOnProduction, clsFixedParameterCode.CreateJEOnProduction, trans), "1") = CompairStringResult.Equal Then
            JournalEntry(trans, obj)
        End If

        Dim qry As String = "Update TSPL_MF_RECEIPT set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where RECEIPT_CODE ='" + strDocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        'Catch ex As Exception

        '    Throw New Exception(ex.Message)
        'End Try
        Return True
    End Function

    Public Shared Function JournalEntry(ByVal trans As SqlTransaction, ByVal obj As clsProductionReceipt, Optional ByVal strVourcherNoForRecreateOnly As String = "") As Boolean
        Try
            Dim RecoControlACC As String = ""
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                RecoControlACC = "I"
            End If
            Dim VoucherNo As String
            If clsCommon.myLen(strVourcherNoForRecreateOnly) > 0 Then
                VoucherNo = strVourcherNoForRecreateOnly
            Else
                VoucherNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='SP-PE' and Source_Doc_No='" & obj.RECEIPT_CODE & "'", trans))
            End If


            Dim Count As Integer = 0
            Dim qry As String
            Dim dtGL As DataTable
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim VoucherDesc As String = "Financial Entry for Std Production Entry -" & obj.RECEIPT_CODE & " "
            Dim SourceDocDesc As String = obj.DESCRIPTION
            Dim SourceDocNo As String = obj.RECEIPT_CODE
            Dim Comments As String = obj.COMMENTS
            Dim Remarks As String = obj.DESCRIPTION
            Dim i As Integer = 0
            Dim dblTotalLossAmt As Decimal = 0
            Dim dclPLAmt As Decimal = 0

            qry = "SELECT TSPL_INVENTORY_MOVEMENT.Item_Code as CONSM_ITEM_CODE,TSPL_INVENTORY_MOVEMENT.Avg_Cost,TSPL_ITEM_MASTER.Item_Desc,TSPL_PURCHASE_ACCOUNTS.WIP_Account AS CreditAccount,TSPL_PURCHASE_ACCOUNTS.Loss_Ac  FROM TSPL_INVENTORY_MOVEMENT   
left join TSPL_ITEM_MASTER on TSPL_INVENTORY_MOVEMENT.Item_Code=TSPL_ITEM_MASTER.Item_Code 
left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code 
WHERE TSPL_INVENTORY_MOVEMENT.source_Doc_no='" & obj.RECEIPT_CODE & "' and TSPL_INVENTORY_MOVEMENT.InOut='O' and TSPL_INVENTORY_MOVEMENT.Trans_Type='Production'"

            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            For Each grow As DataRow In dtGL.Rows
                '' check for account setting  exist or not
                If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
                    Throw New Exception("WIP Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
                End If
                Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.LOCATION_CODE, trans)
                If clsCommon.myLen(CreditAcc) > 0 Then
                    Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
                    ArryLstGLAC.Add(Acc2)
                    dclPLAmt += -1 * clsCommon.myCdbl(grow("Avg_Cost"))
                End If
            Next



            qry = "SELECT TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Avg_Cost,TSPL_ITEM_MASTER.Item_Desc,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account ,TSPL_PURCHASE_ACCOUNTS.Physical_Inv_Adjustment,TSPL_PURCHASE_ACCOUNTS.Loss_Ac,TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code
FROM TSPL_INVENTORY_MOVEMENT   
left join TSPL_ITEM_MASTER on TSPL_INVENTORY_MOVEMENT.Item_Code=TSPL_ITEM_MASTER.Item_Code 
left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code 
WHERE TSPL_INVENTORY_MOVEMENT.source_Doc_no='" & obj.RECEIPT_CODE & "' and TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.Trans_Type='Production'"
            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            For ii As Integer = 0 To dtGL.Rows.Count - 1
                Dim AccountCode As String = clsCommon.myCstr(dtGL.Rows(ii).Item("Inv_Control_Account"))
                If clsCommon.myLen(AccountCode) <= 0 Then
                    Throw New Exception("Inventory Control account not found purchase Account set-" + clsCommon.myCstr(dtGL.Rows(ii).Item("Purchase_Class_Code")) + "  for Item Code " & dtGL.Rows(ii).Item("ITEM_CODE") & "")
                End If
                AccountCode = clsERPFuncationality.ChangeGLAccountLocationSegment(AccountCode, obj.LOCATION_CODE, trans)

                Dim Acc2() As String = {AccountCode, clsCommon.myCdbl(dtGL.Rows(ii)("Avg_Cost")), "", "", "", "", "", "", RecoControlACC}
                ArryLstGLAC.Add(Acc2)
                If clsCommon.CompairString(RecoControlACC, "I") = CompairStringResult.Equal Then
                    clsInventoryMovement.UpdateInvControlAccount(obj.RECEIPT_CODE, "Production", dtGL.Rows(ii).Item("ITEM_CODE"), AccountCode, "", "", trans)
                End If


                dclPLAmt += clsCommon.myCdbl(dtGL.Rows(ii)("Avg_Cost"))

                If dclPLAmt <> 0 Then
                    Dim ACCCode As String = clsCommon.myCstr(dtGL.Rows(ii).Item("Loss_Ac"))
                    If clsCommon.myLen(ACCCode) <= 0 Then
                        Throw New Exception("Gain/Loss account not found purchase Account set-" + clsCommon.myCstr(dtGL.Rows(ii).Item("Purchase_Class_Code")) + " for Item Code " & dtGL.Rows(ii).Item("ITEM_CODE") & "")
                    End If
                    ACCCode = clsERPFuncationality.ChangeGLAccountLocationSegment(ACCCode, obj.LOCATION_CODE, trans)
                    Dim Acc4() As String = {ACCCode, -1 * dclPLAmt} ''It should be last account 
                    ArryLstGLAC.Add(Acc4)
                End If
            Next
            Dim GLDesc As String = "Journal Entry Against Production Entry- Doc No." & obj.RECEIPT_CODE & " "
            If clsCommon.myLen(VoucherNo) > 0 Then
                transportSql.FunGrnlEntryWithTrans(obj.LOCATION_CODE, False, VoucherNo, trans, obj.RECEIPT_DATE, GLDesc, "SP-PE", "Std Production Entry", obj.RECEIPT_CODE, obj.DESCRIPTION, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, GLDesc, "")
            Else
                transportSql.FunGrnlEntryWithTrans(obj.LOCATION_CODE, False, trans, obj.RECEIPT_DATE, GLDesc, "SP-PE", "Std Production Entry", obj.RECEIPT_CODE, obj.DESCRIPTION, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , GLDesc, "")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "select 1 from TSPL_MF_RECEIPT where receipt_code='" + strCode + "' and POSTED=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Transaction status should be posted.")
            End If

            qry = "update TSPL_MF_RECEIPT set POSTED=0,Posting_Date=null where receipt_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetProductionReportData(ByVal FromDate As Date, ByVal ToDate As Date) As DataTable
        Dim dt As DataTable = Nothing
        Try
            Dim qry As String = ""
            qry = " DECLARE @STRQ VARCHAR(MAX); "
            qry += " EXEC TSPL_DATEWISE_PRODUCTION '" & clsCommon.GetPrintDate(FromDate.AddDays(-1), "dd/MMM/yyyy") & "' ,'" & clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") & "',@STRQ OUTPUT; "
            qry += " SELECT @STRQ; "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dt = clsDBFuncationality.GetDataTable(clsCommon.myCstr(dt.Rows(0)(0)))
            Else
                dt = New DataTable
            End If
        Catch ex As Exception
            'Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function
    Public Shared Function checkStock(ByVal TR_Type As String, ByVal BO_MO_Code As String, ByVal ProdQty As Decimal, ByVal Receipt_Date As Date, ByVal Location_Code As String, ByVal Receipt_Code As String) As Boolean
        Dim strq As String = ""
        strq = ""
        If TR_Type = "BO" Then

            strq = " SELECT TSPL_MF_BATCH_PP_DETAIL.ITEM_CODE as PROD_ITEM_CODE,SUM(TSPL_MF_BATCH_PP_DETAIL.BATCH_QTY) AS PROD_QTY,TSPL_MF_BATCH_ORDER_DETAIL.ITEM_CODE," & _
                   " SUM(TSPL_MF_BATCH_ORDER_DETAIL.QTY) AS BOM_QTY,((SUM(TSPL_MF_BATCH_ORDER_DETAIL.QTY)/SUM(TSPL_MF_BATCH_PP_DETAIL.BATCH_QTY))* " & ProdQty & ") as REQUIR_QTY,TSPL_MF_BATCH_ORDER_DETAIL.UNIT_CODE " & _
                   " FROM TSPL_MF_BATCH_ORDER_DETAIL inner join TSPL_MF_BATCH_ORDER on TSPL_MF_BATCH_ORDER.BO_CODE=TSPL_MF_BATCH_ORDER_DETAIL.BO_CODE " & _
                   " inner join TSPL_MF_BATCH_PP_DETAIL on TSPL_MF_BATCH_ORDER.BO_CODE=TSPL_MF_BATCH_PP_DETAIL.BO_CODE " & _
                   " and TSPL_MF_BATCH_ORDER_DETAIL.PROD_PLAN_CODE=TSPL_MF_BATCH_PP_DETAIL.PROD_PLAN_CODE " & _
                   " and TSPL_MF_BATCH_ORDER_DETAIL.PRODUCTION_LINE_CODE=TSPL_MF_BATCH_PP_DETAIL.PRODUCTION_LINE_CODE " & _
                   " WHERE TSPL_MF_BATCH_ORDER.BO_CODE='" & BO_MO_Code & "' " & _
                   " GROUP BY TSPL_MF_BATCH_PP_DETAIL.ITEM_CODE,TSPL_MF_BATCH_ORDER_DETAIL.ITEM_CODE,TSPL_MF_BATCH_ORDER_DETAIL.UNIT_CODE "
        ElseIf TR_Type = "MO" Then
            strq = " select TSPL_MF_MANUFACTURING_ORDER.ITEM_CODE as PROD_ITEM_CODE,TSPL_MF_MANUFACTURING_ORDER.QTY_ORDERED_STOCK as PROD_QTY," & _
                   " TSPL_MF_MO_MATERIAL.CONSM_ITEM_CODE as ITEM_CODE,TSPL_MF_MO_MATERIAL.BOM_QUANTITY AS BOM_QTY, " & _
                   " ((TSPL_MF_MO_MATERIAL.CONSM_QUANTITY * " & ProdQty & ")/TSPL_MF_MANUFACTURING_ORDER.QTY_ORDERED_STOCK)  AS REQUIR_QTY, " & _
                   " TSPL_MF_MO_MATERIAL.CONSM_ITEM_UNIT_CODE AS UNIT_CODE from TSPL_MF_MO_MATERIAL inner join TSPL_MF_MANUFACTURING_ORDER " & _
                   " on TSPL_MF_MO_MATERIAL.MO_CODE=TSPL_MF_MANUFACTURING_ORDER.MO_CODE WHERE TSPL_MF_MANUFACTURING_ORDER.MO_CODE='" & BO_MO_Code & "'"
        End If
        'strq += " SELECT REQR.ITEM_CODE,(COALESCE(REQR.REQR_QTY,0)-COALESCE(AVAIL.AVAIL_QTY,0)) AS BAL_QTY FROM ("
        'strq += " SELECT ITEM_CODE,SUM(QTY) AS REQR_QTY FROM TSPL_MF_BATCH_ORDER_DETAIL"
        'strq += " WHERE BO_CODE='" & Batch_Order_Code & "' GROUP BY ITEM_CODE ) REQR"
        'strq += " LEFT JOIN "
        'strq += " ("
        'strq += " SELECT ITEM_CODE,SUM(ISSUE_QTY) AS AVAIL_QTY FROM ("
        'strq += " SELECT TSPL_MF_ISSUE.ISSUE_CODE,TSPL_MF_ISSUE_DETAIL.ITEM_CODE,TSPL_MF_ISSUE_DETAIL.ISSUE_QTY FROM TSPL_MF_ISSUE_DETAIL INNER JOIN TSPL_MF_ISSUE ON "
        'strq += " TSPL_MF_ISSUE_DETAIL.ISSUE_CODE=TSPL_MF_ISSUE.ISSUE_CODE WHERE TSPL_MF_ISSUE.ISSUE_DATE<='" & clsCommon.GetPrintDate(Receipt_Date, "dd/MMM/yyyy") & "' AND TSPL_MF_ISSUE.LOCATION_CODE='" & Location_Code & "'"
        'strq += " UNION ALL "
        'strq += " SELECT TSPL_MF_ISSUE.ISSUE_CODE,TSPL_MF_ISSUE_DETAIL.ITEM_CODE,(-TSPL_MF_ISSUE_DETAIL.ISSUE_QTY) AS LESS_QTY FROM TSPL_MF_ISSUE_DETAIL INNER JOIN "
        'strq += " TSPL_MF_ISSUE ON TSPL_MF_ISSUE_DETAIL.ISSUE_CODE=TSPL_MF_ISSUE.ISSUE_CODE WHERE TSPL_MF_ISSUE.ISSUE_DATE<='" & clsCommon.GetPrintDate(Receipt_Date, "dd/MMM/yyyy") & "' "
        'strq += " AND TSPL_MF_ISSUE.LOCATION_CODE_FROM='" & Location_Code & "'"
        'strq += " UNION ALL "
        'strq += " SELECT TSPL_MF_CONSUMPTION_DETAIL.CONSM_CODE,TSPL_MF_CONSUMPTION_DETAIL.CONSM_ITEM_CODE,(-TSPL_MF_CONSUMPTION_DETAIL.CONSM_QTY) AS CONSM_QTY "
        'strq += " FROM TSPL_MF_CONSUMPTION_DETAIL INNER JOIN TSPL_MF_RECEIPT ON TSPL_MF_CONSUMPTION_DETAIL.RECEIPT_CODE=TSPL_MF_RECEIPT.RECEIPT_CODE "
        'strq += " WHERE TSPL_MF_RECEIPT.RECEIPT_DATE<='" & clsCommon.GetPrintDate(Receipt_Date, "dd/MMM/yyyy") & "' AND TSPL_MF_CONSUMPTION_DETAIL.LOCATION_CODE='" & Location_Code & "' AND TSPL_MF_CONSUMPTION_DETAIL.RECEIPT_CODE <>'" & Receipt_Code & "') AS ITEM_QTY_BAL GROUP BY ITEM_CODE"
        'strq += " ) AVAIL ON REQR.ITEM_CODE=AVAIL.ITEM_CODE WHERE (COALESCE(REQR.REQR_QTY,0)-COALESCE(AVAIL.AVAIL_QTY,0))>0"

        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq)
        For Each dr As DataRow In dt.Rows
            Dim availQty As Double = 0
            Dim reqQty As Double = 0

            availQty = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(dr.Item("ITEM_CODE"), Location_Code, Receipt_Code, Receipt_Date, Nothing, dr.Item("Unit_Code"))
            reqQty = dr.Item("REQUIR_QTY") ''clsCommon.myCdbl(dr.Cells(colItemCode)) * (clsCommon.myCdbl(Me.txtQuantity.Text) / clsCommon.myCdbl(Me.txtBuildQty.Text))
            If availQty < reqQty Then
                clsCommon.MyMessageBoxShow("Item Code: " & dr.Item("ITEM_CODE") & " ; Required Qty : " & reqQty & " ; Available Qty : " & availQty & "")
                Return False
            End If
        Next
        
        Return True
    End Function
    Public Shared Function GetCategorywiseProduction(ByVal FromBODate As Date, ByVal ToBODate As Date, ByVal figure As String) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""

            qry = "SELECT TSPL_ITEM_MASTER.PROD_ITEM_CATEGORY_CODE AS 'Category', CONVERT(Decimal(18,2), SUM(TSPL_MF_RECEIPT_DETAIL.RECEIPT_QTY)/" + figure + ") AS 'Produced Qty', " & _
                 " SUM(TSPL_MF_RECEIPT_DETAIL.REJ_QTY) AS 'Rejected Qty',SUM(TSPL_MF_RECEIPT_DETAIL.BREAKAGE_QTY) as 'Break Qty' " & _
                 " FROM TSPL_MF_RECEIPT_DETAIL INNER JOIN TSPL_MF_RECEIPT ON TSPL_MF_RECEIPT_DETAIL.RECEIPT_CODE=TSPL_MF_RECEIPT.RECEIPT_CODE  " & _
                 " LEFT JOIN TSPL_ITEM_MASTER ON TSPL_MF_RECEIPT_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " & _
                 " where TSPL_MF_RECEIPT.RECEIPT_DATE between '" & clsCommon.GetPrintDate(FromBODate, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(ToBODate, "dd/MMM/yyyy") & "'  " & _
                 " GROUP BY TSPL_ITEM_MASTER.PROD_ITEM_CATEGORY_CODE "


            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function
    Public Shared Function GetCategorywiseProductionDetail(ByVal FromBODate As Date, ByVal ToBODate As Date, ByVal Category As String) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""

            qry = "SELECT TSPL_MF_RECEIPT.RECEIPT_CODE AS 'Production No',TSPL_MF_RECEIPT.DESCRIPTION AS 'Description',TSPL_MF_RECEIPT.RECEIPT_DATE AS 'Production Date',TSPL_ITEM_MASTER.PROD_ITEM_CATEGORY_CODE AS 'Category',(TSPL_MF_RECEIPT_DETAIL.RECEIPT_QTY) AS 'Produced Qty', " & _
                 " (TSPL_MF_RECEIPT_DETAIL.REJ_QTY) AS 'Rejected Qty',(TSPL_MF_RECEIPT_DETAIL.BREAKAGE_QTY) as 'Break Qty' " & _
                 " FROM TSPL_MF_RECEIPT_DETAIL INNER JOIN TSPL_MF_RECEIPT ON TSPL_MF_RECEIPT_DETAIL.RECEIPT_CODE=TSPL_MF_RECEIPT.RECEIPT_CODE  " & _
                 " LEFT JOIN TSPL_ITEM_MASTER ON TSPL_MF_RECEIPT_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " & _
                 " where TSPL_MF_RECEIPT.RECEIPT_DATE between '" & clsCommon.GetPrintDate(FromBODate, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(ToBODate, "dd/MMM/yyyy") & "'  " & _
                 " and TSPL_ITEM_MASTER.PROD_ITEM_CATEGORY_CODE='" & Category & "'"


            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function
    

End Class


Public Class clsProductionReceiptDetail
#Region "Variables"

#End Region

    Public Shared Function SaveDetailData(ByVal strDocNo As String, ByVal objRec As clsProductionReceipt, ByVal Arr As List(Of clsProductionReceipt), ByVal trans As SqlTransaction) As Boolean

        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                'Dim qry As String = "DELETE FROM TSPL_MF_RECEIPT_DETAIL WHERE RECEIPT_CODE='" + strDocNo + "'"
                'clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Dim qry As String = "DELETE FROM TSPL_SERIAL_ITEM WHERE Document_Code='" + strDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                For Each obj As clsProductionReceipt In Arr

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "RECEIPT_CODE", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "PROD_PLAN_CODE", obj.PROD_PLAN_CODE, True)
                    clsCommon.AddColumnsForChange(coll, "PRODUCTION_LINE_CODE", obj.PRODUCTION_LINE_CODE, True)
                    clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE, True)

                    clsCommon.AddColumnsForChange(coll, "ITEM_CODE", obj.ITEM_CODE)
                    clsCommon.AddColumnsForChange(coll, "ITEM_DESCRIPTION", obj.ITEM_DESCRIPTION)
                    clsCommon.AddColumnsForChange(coll, "BATCH_QTY", obj.BATCH_QTY, True)
                    clsCommon.AddColumnsForChange(coll, "ISSUE_QTY", obj.ISSUE_QTY, True)
                    clsCommon.AddColumnsForChange(coll, "BALANCE_BATCH_QTY", obj.BALANCE_BATCH_QTY, True)
                    clsCommon.AddColumnsForChange(coll, "BALANCE_ISSUE_QTY", obj.BALANCE_ISSUE_QTY, True)
                    clsCommon.AddColumnsForChange(coll, "RECEIPT_QTY", obj.RECEIPT_QTY)
                    clsCommon.AddColumnsForChange(coll, "REJ_HEAD", obj.REJ_HEAD)
                    clsCommon.AddColumnsForChange(coll, "REJ_QTY", obj.REJ_QTY)
                    clsCommon.AddColumnsForChange(coll, "BREAKAGE_HEAD", obj.BREAKAGE_HEAD)
                    clsCommon.AddColumnsForChange(coll, "BREAKAGE_QTY", obj.BREAKAGE_QTY)
                    clsCommon.AddColumnsForChange(coll, "LAB_TESTING", obj.LAB_TESTING)
                    clsCommon.AddColumnsForChange(coll, "UNIT_CODE", obj.UNIT_CODE)
                    clsCommon.AddColumnsForChange(coll, "MFG_DATE", clsCommon.GetPrintDate(obj.MFG_DATE, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "EXP_DATE", clsCommon.GetPrintDate(obj.EXP_DATE, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "TR_TYPE", obj.TR_TYPE)

                    If obj.START_TIME Is Nothing Then
                    Else
                        clsCommon.AddColumnsForChange(coll, "START_TIME", Format(obj.START_TIME, "hh:mm:ss tt"), True)
                    End If
                    If obj.END_TIME Is Nothing Then
                    Else
                        clsCommon.AddColumnsForChange(coll, "END_TIME", Format(obj.END_TIME, "hh:mm:ss tt"), True)
                    End If
                    Dim isTrading As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from TSPL_MF_RECEIPT where  IS_TRADING = 1 and Receipt_Code = '" + clsCommon.myCstr(strDocNo) + "'", trans))
                    If isTrading = True Then
                        ' '' saving production costing 

                        Dim strIssuecode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISSUE_CODE from TSPL_MF_RECEIPT where Receipt_Code = '" + clsCommon.myCstr(strDocNo) + "' ", trans))
                        Dim dblFIFO_Cost As Double = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull (FIFO_Cost,0) as FIFO_Cost  from TSPL_MF_ISSUE_DETAIL where ISSUE_Code = '" + strIssuecode + "' and ITEM_CODE = '" + obj.ITEM_CODE + "' ", trans))
                        Dim dblLIFO_Cost As Double = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull (LIFO_Cost,0) as LIFO_Cost  from TSPL_MF_ISSUE_DETAIL where ISSUE_Code = '" + strIssuecode + "' and ITEM_CODE = '" + obj.ITEM_CODE + "' ", trans))
                        Dim dblAvg_Cost As Double = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull ( Avg_Cost,0) as Avg_Cost from TSPL_MF_ISSUE_DETAIL where ISSUE_Code = '" + strIssuecode + "' and ITEM_CODE = '" + obj.ITEM_CODE + "' ", trans))

                        Dim strIssueLocationCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select LOCATION_CODE from TSPL_MF_ISSUE where ISSUE_CODE = '" + strIssuecode + "' ", trans))
                        clsCommon.AddColumnsForChange(coll, "FIFO_Cost", dblFIFO_Cost)
                        clsCommon.AddColumnsForChange(coll, "LIFO_Cost", dblLIFO_Cost)
                        clsCommon.AddColumnsForChange(coll, "Avg_Cost", dblAvg_Cost)
                        'clsCommon.AddColumnsForChange(coll, "FIFO_Cost", clsInventoryMovement.GetCost(EnumCostingMethod.FIFO, obj.ITEM_CODE, strIssueLocationCode, obj.RECEIPT_QTY, objRec.RECEIPT_DATE, clsCommon.GETSERVERDATE(trans), obj.POSTED, trans))
                        'clsCommon.AddColumnsForChange(coll, "LIFO_Cost", clsInventoryMovement.GetCost(EnumCostingMethod.LIFO, obj.ITEM_CODE, strIssueLocationCode, obj.RECEIPT_QTY, objRec.RECEIPT_DATE, clsCommon.GETSERVERDATE(trans), obj.POSTED, trans))
                        'clsCommon.AddColumnsForChange(coll, "Avg_Cost", clsInventoryMovement.GetCost(EnumCostingMethod.Averege, obj.ITEM_CODE, strIssueLocationCode, obj.RECEIPT_QTY, objRec.RECEIPT_DATE, clsCommon.GETSERVERDATE(trans), obj.POSTED, trans))
                        'clsCommon.AddColumnsForChange(coll, "FIFO_Cost", clsInventoryMovement.GetCost(EnumCostingMethod.FIFO, obj.ITEM_CODE, objRec.LOCATION_CODE, obj.RECEIPT_QTY, objRec.RECEIPT_DATE, clsCommon.GETSERVERDATE(trans), obj.POSTED, trans))
                        'clsCommon.AddColumnsForChange(coll, "LIFO_Cost", clsInventoryMovement.GetCost(EnumCostingMethod.LIFO, obj.ITEM_CODE, objRec.LOCATION_CODE, obj.RECEIPT_QTY, objRec.RECEIPT_DATE, clsCommon.GETSERVERDATE(trans), obj.POSTED, trans))
                        'clsCommon.AddColumnsForChange(coll, "Avg_Cost", clsInventoryMovement.GetCost(EnumCostingMethod.Averege, obj.ITEM_CODE, objRec.LOCATION_CODE, obj.RECEIPT_QTY, objRec.RECEIPT_DATE, clsCommon.GETSERVERDATE(trans), obj.POSTED, trans))
                    End If
                    Dim isUpdate As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from TSPL_MF_RECEIPT_DETAIL where Prod_Plan_Code ='" + clsCommon.myCstr(obj.PROD_PLAN_CODE) + "' and BOM_CODE='" + clsCommon.myCstr(obj.BOM_CODE) + "' and ITEM_CODE ='" + clsCommon.myCstr(obj.ITEM_CODE) + "' and Receipt_Code = '" + clsCommon.myCstr(strDocNo) + "' and PRODUCTION_LINE_CODE = '" + clsCommon.myCstr(obj.PRODUCTION_LINE_CODE) + "' ", trans))

                    If isTrading = True Then
                        isUpdate = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from TSPL_MF_RECEIPT_DETAIL where  ITEM_CODE ='" + clsCommon.myCstr(obj.ITEM_CODE) + "' and Receipt_Code = '" + clsCommon.myCstr(strDocNo) + "'  ", trans))
                    End If
                    If isUpdate Then
                        If isTrading Then
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_RECEIPT_DETAIL", OMInsertOrUpdate.Update, "TSPL_MF_RECEIPT_DETAIL.ITEM_CODE ='" + clsCommon.myCstr(obj.ITEM_CODE) + "' and TSPL_MF_RECEIPT_DETAIL.Receipt_Code = '" + clsCommon.myCstr(strDocNo) + "' ", trans)
                        Else
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_RECEIPT_DETAIL", OMInsertOrUpdate.Update, "TSPL_MF_RECEIPT_DETAIL.Prod_Plan_Code ='" + clsCommon.myCstr(obj.PROD_PLAN_CODE) + "' and TSPL_MF_RECEIPT_DETAIL.BOM_CODE='" + clsCommon.myCstr(obj.BOM_CODE) + "' and TSPL_MF_RECEIPT_DETAIL.ITEM_CODE ='" + clsCommon.myCstr(obj.ITEM_CODE) + "' and TSPL_MF_RECEIPT_DETAIL.Receipt_Code = '" + clsCommon.myCstr(strDocNo) + "' and TSPL_MF_RECEIPT_DETAIL.PRODUCTION_LINE_CODE = '" + clsCommon.myCstr(obj.PRODUCTION_LINE_CODE) + "'  ", trans)
                        End If

                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_RECEIPT_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MF_RECEIPT_DETAIL.RECEIPT_CODE='" + strDocNo + "' ", trans)
                    End If

                    clsSerializeInvenotry.SaveData("Production", strDocNo, objRec.RECEIPT_DATE, "I", obj.ITEM_CODE, objRec.LOCATION_CODE, (Arr.IndexOf(obj) + 1), obj.arrSrItem, trans)
                Next


            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False

        End Try

        Return True
    End Function
    Public Shared Function UpdateCostingRM(ByVal obj As clsProductionReceipt, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        If obj Is Nothing Then
            Return False
        End If
        Try
            Dim qry As String = "select RECEIPT_CODE,PRODUCTION_LINE_CODE,CONSM_ITEM_CODE,CONSM_QTY,LOCATION_CODE,UNIT_CODE,FIFO_COST,LIFO_COST,AVG_COST from TSPL_MF_CONSUMPTION_DETAIL where receipt_code='" & obj.RECEIPT_CODE & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            For Each dr As DataRow In dt.Rows
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "FIFO_COST", clsInventoryMovement.GetCost(EnumCostingMethod.FIFO, dr.Item("CONSM_ITEM_CODE"), dr.Item("LOCATION_CODE"), dr.Item("CONSM_QTY"), obj.RECEIPT_DATE, clsCommon.GETSERVERDATE(trans), obj.POSTED, trans))
                clsCommon.AddColumnsForChange(coll, "LIFO_COST", clsInventoryMovement.GetCost(EnumCostingMethod.LIFO, dr.Item("CONSM_ITEM_CODE"), dr.Item("LOCATION_CODE"), dr.Item("CONSM_QTY"), obj.RECEIPT_DATE, clsCommon.GETSERVERDATE(trans), obj.POSTED, trans))
                clsCommon.AddColumnsForChange(coll, "AVG_COST", clsInventoryMovement.GetCost(EnumCostingMethod.Averege, dr.Item("CONSM_ITEM_CODE"), dr.Item("LOCATION_CODE"), dr.Item("CONSM_QTY"), obj.RECEIPT_DATE, clsCommon.GETSERVERDATE(trans), obj.POSTED, trans))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_CONSUMPTION_DETAIL", OMInsertOrUpdate.Update, "TSPL_MF_CONSUMPTION_DETAIL.RECEIPT_CODE='" + obj.RECEIPT_CODE + "' and TSPL_MF_CONSUMPTION_DETAIL.CONSM_ITEM_CODE='" + dr.Item("CONSM_ITEM_CODE") + "' ", trans)
            Next
            qry = "select (TOTAL_COST-DIRECT_MATERIAL_COST) as TOTAL_COST from TSPL_MF_MO_COSTING where MO_CODE='" & obj.MO_CODE & "' and CALC_TYPE='Actual'"
            Dim dtMOCost As DataTable
            dtMOCost = clsDBFuncationality.GetDataTable(qry, trans)
            Dim MOCost As Double = 0.0
            If dtMOCost.Rows.Count > 0 Then
                MOCost = dtMOCost.Rows(0).Item("TOTAL_COST")
            End If
            qry = "update TSPL_MF_RECEIPT_DETAIL set fifo_cost=(consm.fifo_cost+" & MOCost & "),lifo_cost=(consm.lifo_cost+" & MOCost & "),avg_cost=(consm.avg_cost+" & MOCost & ") from " & _
                     " (select RECEIPT_CODE,PRODUCTION_LINE_CODE,SUM(fifo_cost) as fifo_cost,SUM(lifo_cost) as lifo_cost,SUM(avg_cost) as avg_cost from " & _
                     " TSPL_MF_CONSUMPTION_DETAIL group by receipt_code,PRODUCTION_LINE_CODE) as consm where TSPL_MF_RECEIPT_DETAIL.RECEIPT_CODE=consm.RECEIPT_CODE and coalesce(TSPL_MF_RECEIPT_DETAIL.PRODUCTION_LINE_CODE,'')=coalesce(consm.PRODUCTION_LINE_CODE,'') and TSPL_MF_RECEIPT_DETAIL.receipt_code='" & obj.RECEIPT_CODE & "'"


            '' execute query for updating cost of produced item.
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
       
        Return True
    End Function
   
End Class
Public Class clsProductionRM
#Region "Variables"
    Dim RECEIPT_CODE As String
    Dim PRODUCTION_LINE_CODE As String
    Dim CONSM_ITEM_CODE As String
    Dim CONSM_QTY As Decimal
    Dim LOCATION_CODE As String
    Dim UNIT_CODE As String
    Dim FIFO_COST As Decimal
    Dim LIFO_COST As Decimal
    Dim AVG_COST As Decimal

#End Region

    Public Shared Function GetRMBO(ByVal obj As clsProductionReceipt, ByVal proportion As Decimal, ByVal TotalRecQty As Decimal, ByVal trans As SqlTransaction) As List(Of clsProductionRM)
        Dim qry As String = ""
        Dim objList As New List(Of clsProductionRM)
        Dim objRM As New clsProductionRM

        'If clsBatchOrder.GetBatchBalanceQty(obj.BO_CODE, obj.RECEIPT_CODE, trans) = TotalRecQty Then
        qry = "SELECT ISSUE.PRODUCTION_LINE_CODE,ISSUE.ITEM_CODE,COALESCE(ISSUE.Issue_Qty,0) AS Issue_Qty," & _
              " COALESCE(REC.CONSM_QTY,0) AS CONSM_QTY,COALESCE(RETURN1.RETURN_QTY,0) AS RETURN_QTY, " & _
              " (COALESCE(ISSUE.Issue_Qty,0)-COALESCE(REC.CONSM_QTY,0)-COALESCE(RETURN1.RETURN_QTY,0)) AS Qty,ISSUE.UNIT_CODE," & _
              " (COALESCE(ISSUE.FIFO_Cost,0)-COALESCE(RETURN1.FIFO_Cost,0)) AS FIFO_Cost," & _
              " (COALESCE(ISSUE.LIFO_Cost,0)-COALESCE(RETURN1.LIFO_Cost,0)) AS LIFO_Cost," & _
              " (COALESCE(ISSUE.Avg_Cost,0)-COALESCE(RETURN1.Avg_Cost,0)) AS Avg_Cost  FROM ( " & _
              " select TSPL_MF_ISSUE_DETAIL.PRODUCTION_LINE_CODE,TSPL_MF_ISSUE_DETAIL.ITEM_CODE, " & _
              " SUM(TSPL_MF_ISSUE_DETAIL.issue_qty) as Issue_Qty,SUM(TSPL_MF_ISSUE_DETAIL.FIFO_Cost) AS FIFO_Cost,SUM(TSPL_MF_ISSUE_DETAIL.LIFO_Cost) AS LIFO_Cost,SUM(TSPL_MF_ISSUE_DETAIL.Avg_Cost) AS Avg_Cost,TSPL_MF_ISSUE_DETAIL.UNIT_CODE from TSPL_MF_ISSUE_DETAIL inner join TSPL_MF_ISSUE on " & _
              " TSPL_MF_ISSUE_DETAIL.ISSUE_CODE = TSPL_MF_ISSUE.ISSUE_CODE " & _
              " where TSPL_MF_ISSUE_DETAIL.BO_CODE='" & obj.BO_CODE & "' " & _
              " group by TSPL_MF_ISSUE_DETAIL.PRODUCTION_LINE_CODE,TSPL_MF_ISSUE_DETAIL.ITEM_CODE,TSPL_MF_ISSUE_DETAIL.UNIT_CODE) AS ISSUE " & _
              " left join (select PRODUCTION_LINE_CODE,CONSM_ITEM_CODE,SUM(CONSM_QTY) AS CONSM_QTY " & _
              " from TSPL_MF_CONSUMPTION_DETAIL INNER JOIN TSPL_MF_RECEIPT ON  TSPL_MF_RECEIPT.RECEIPT_CODE=TSPL_MF_CONSUMPTION_DETAIL.RECEIPT_CODE " & _
              " WHERE TSPL_MF_RECEIPT.BO_CODE='" & obj.BO_CODE & "' GROUP BY PRODUCTION_LINE_CODE,CONSM_ITEM_CODE ) AS REC " & _
              " ON ISSUE.PRODUCTION_LINE_CODE=REC.PRODUCTION_LINE_CODE AND ISSUE.ITEM_CODE=REC.CONSM_ITEM_CODE " & _
              " LEFT JOIN ( " & _
              " SELECT TSPL_MF_RETURN_DETAIL.PRODUCTION_LINE_CODE,TSPL_MF_RETURN_DETAIL.ITEM_CODE," & _
              " TSPL_MF_RETURN_DETAIL.UNIT_CODE,SUM(TSPL_MF_RETURN_DETAIL.RETURN_QTY) AS RETURN_QTY, " & _
              " SUM(TSPL_MF_RETURN_DETAIL.FIFO_Cost) AS FIFO_Cost, " & _
              " SUM(TSPL_MF_RETURN_DETAIL.LIFO_Cost) AS LIFO_Cost,SUM(TSPL_MF_RETURN_DETAIL.Avg_Cost) AS Avg_Cost " & _
              " FROM TSPL_MF_RETURN_DETAIL " & _
              " inner join TSPL_MF_RETURN on TSPL_MF_RETURN_DETAIL.RETURN_CODE=TSPL_MF_RETURN.RETURN_CODE  " & _
              " INNER JOIN TSPL_MF_ISSUE ON TSPL_MF_RETURN_DETAIL.ISSUE_CODE=TSPL_MF_ISSUE.ISSUE_CODE " & _
              " inner join TSPL_MF_ISSUE_DETAIL on TSPL_MF_ISSUE_DETAIL.ISSUE_CODE=TSPL_MF_ISSUE.ISSUE_CODE  " & _
              " AND TSPL_MF_RETURN_DETAIL.ITEM_CODE=TSPL_MF_ISSUE_DETAIL.ITEM_CODE " & _
              " AND  TSPL_MF_RETURN_DETAIL.PRODUCTION_LINE_CODE=TSPL_MF_ISSUE_DETAIL.PRODUCTION_LINE_CODE " & _
              " WHERE TSPL_MF_ISSUE_DETAIL.BO_CODE='" & obj.BO_CODE & "' " & _
              " group by TSPL_MF_RETURN_DETAIL.PRODUCTION_LINE_CODE,TSPL_MF_RETURN_DETAIL.ITEM_CODE,TSPL_MF_RETURN_DETAIL.UNIT_CODE ) AS RETURN1 " & _
              " ON ISSUE.PRODUCTION_LINE_CODE=RETURN1.PRODUCTION_LINE_CODE AND ISSUE.ITEM_CODE=RETURN1.ITEM_CODE "


        'Else
        'Return objList
        ''qry += " SELECT PRODUCTION_LINE_CODE,ITEM_CODE,(SUM(QTY)* " & proportion & ") AS QTY ,UNIT_CODE FROM TSPL_MF_BATCH_ORDER_DETAIL"
        ''qry += " WHERE BO_CODE='" & obj.BO_CODE & "' GROUP BY PRODUCTION_LINE_CODE,ITEM_CODE,UNIT_CODE "
        'End If

        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        For Each dr As DataRow In dt.Rows
            objRM = New clsProductionRM
            objRM.RECEIPT_CODE = obj.RECEIPT_CODE
            objRM.PRODUCTION_LINE_CODE = clsCommon.myCstr(dr.Item("PRODUCTION_LINE_CODE"))
            objRM.CONSM_ITEM_CODE = clsCommon.myCstr(dr.Item("ITEM_CODE"))
            objRM.CONSM_QTY = clsCommon.myCdbl(dr.Item("QTY"))
            objRM.LOCATION_CODE = obj.LOCATION_CODE
            objRM.UNIT_CODE = clsCommon.myCstr(dr.Item("UNIT_CODE"))
            objRM.FIFO_COST = clsCommon.myCdbl(dr.Item("FIFO_COST"))
            objRM.LIFO_COST = clsCommon.myCdbl(dr.Item("LIFO_COST"))
            objRM.AVG_COST = clsCommon.myCdbl(dr.Item("AVG_COST"))
            objList.Add(objRM)
        Next
        Return objList
    End Function
    Public Shared Function GetRMMO(ByVal obj As clsProductionReceipt, ByVal proportion As Decimal, ByVal TotalRecQty As Decimal, ByVal trans As SqlTransaction) As List(Of clsProductionRM)
        Dim qry As String = ""
        Dim objList As New List(Of clsProductionRM)
        Dim objRM As New clsProductionRM

        'If clsBatchOrder.GetBatchBalanceQty(obj.BO_CODE, obj.RECEIPT_CODE, trans) = TotalRecQty Then
        qry = "SELECT ISSUE.PRODUCTION_LINE_CODE,ISSUE.ITEM_CODE,COALESCE(ISSUE.Issue_Qty,0) AS Issue_Qty," & _
              " COALESCE(REC.CONSM_QTY,0) AS CONSM_QTY,COALESCE(RETURN1.RETURN_QTY,0) AS RETURN_QTY, " & _
              " (COALESCE(ISSUE.Issue_Qty,0)-COALESCE(REC.CONSM_QTY,0)-COALESCE(RETURN1.RETURN_QTY,0)) AS Qty,ISSUE.UNIT_CODE," & _
              " (COALESCE(ISSUE.FIFO_Cost,0)-COALESCE(RETURN1.FIFO_Cost,0)) AS FIFO_Cost," & _
              " (COALESCE(ISSUE.LIFO_Cost,0)-COALESCE(RETURN1.LIFO_Cost,0)) AS LIFO_Cost," & _
              " (COALESCE(ISSUE.Avg_Cost,0)-COALESCE(RETURN1.Avg_Cost,0)) AS Avg_Cost  FROM ( " & _
              " select TSPL_MF_ISSUE_DETAIL.PRODUCTION_LINE_CODE,TSPL_MF_ISSUE_DETAIL.ITEM_CODE, " & _
              " SUM(TSPL_MF_ISSUE_DETAIL.issue_qty) as Issue_Qty,SUM(TSPL_MF_ISSUE_DETAIL.FIFO_Cost) AS FIFO_Cost,SUM(TSPL_MF_ISSUE_DETAIL.LIFO_Cost) AS LIFO_Cost,SUM(TSPL_MF_ISSUE_DETAIL.Avg_Cost) AS Avg_Cost,TSPL_MF_ISSUE_DETAIL.UNIT_CODE from TSPL_MF_ISSUE_DETAIL inner join TSPL_MF_ISSUE on " & _
              " TSPL_MF_ISSUE_DETAIL.ISSUE_CODE = TSPL_MF_ISSUE.ISSUE_CODE " & _
              " where TSPL_MF_ISSUE_DETAIL.MO_CODE='" & obj.MO_CODE & "' " & _
              " group by TSPL_MF_ISSUE_DETAIL.PRODUCTION_LINE_CODE,TSPL_MF_ISSUE_DETAIL.ITEM_CODE,TSPL_MF_ISSUE_DETAIL.UNIT_CODE) AS ISSUE " & _
              " left join (select PRODUCTION_LINE_CODE,CONSM_ITEM_CODE,SUM(CONSM_QTY) AS CONSM_QTY " & _
              " from TSPL_MF_CONSUMPTION_DETAIL INNER JOIN TSPL_MF_RECEIPT ON  TSPL_MF_RECEIPT.RECEIPT_CODE=TSPL_MF_CONSUMPTION_DETAIL.RECEIPT_CODE " & _
              " WHERE TSPL_MF_RECEIPT.MO_CODE='" & obj.MO_CODE & "' GROUP BY PRODUCTION_LINE_CODE,CONSM_ITEM_CODE ) AS REC " & _
              " ON ISSUE.PRODUCTION_LINE_CODE=REC.PRODUCTION_LINE_CODE AND ISSUE.ITEM_CODE=REC.CONSM_ITEM_CODE " & _
              " LEFT JOIN ( " & _
              " SELECT TSPL_MF_RETURN_DETAIL.PRODUCTION_LINE_CODE,TSPL_MF_RETURN_DETAIL.ITEM_CODE," & _
              " TSPL_MF_RETURN_DETAIL.UNIT_CODE,SUM(TSPL_MF_RETURN_DETAIL.RETURN_QTY) AS RETURN_QTY, " & _
              " SUM(TSPL_MF_RETURN_DETAIL.FIFO_Cost) AS FIFO_Cost, " & _
              " SUM(TSPL_MF_RETURN_DETAIL.LIFO_Cost) AS LIFO_Cost,SUM(TSPL_MF_RETURN_DETAIL.Avg_Cost) AS Avg_Cost " & _
              " FROM TSPL_MF_RETURN_DETAIL " & _
              " inner join TSPL_MF_RETURN on TSPL_MF_RETURN_DETAIL.RETURN_CODE=TSPL_MF_RETURN.RETURN_CODE  " & _
              " INNER JOIN TSPL_MF_ISSUE ON TSPL_MF_RETURN_DETAIL.ISSUE_CODE=TSPL_MF_ISSUE.ISSUE_CODE " & _
              " inner join TSPL_MF_ISSUE_DETAIL on TSPL_MF_ISSUE_DETAIL.ISSUE_CODE=TSPL_MF_ISSUE.ISSUE_CODE  " & _
              " AND TSPL_MF_RETURN_DETAIL.ITEM_CODE=TSPL_MF_ISSUE_DETAIL.ITEM_CODE " & _
              " AND  TSPL_MF_RETURN_DETAIL.PRODUCTION_LINE_CODE=TSPL_MF_ISSUE_DETAIL.PRODUCTION_LINE_CODE " & _
              " WHERE TSPL_MF_ISSUE_DETAIL.MO_CODE='" & obj.MO_CODE & "' " & _
              " group by TSPL_MF_RETURN_DETAIL.PRODUCTION_LINE_CODE,TSPL_MF_RETURN_DETAIL.ITEM_CODE,TSPL_MF_RETURN_DETAIL.UNIT_CODE ) AS RETURN1 " & _
              " ON ISSUE.PRODUCTION_LINE_CODE=RETURN1.PRODUCTION_LINE_CODE AND ISSUE.ITEM_CODE=RETURN1.ITEM_CODE "


        'Else
        'Return objList
        ''qry += " SELECT PRODUCTION_LINE_CODE,ITEM_CODE,(SUM(QTY)* " & proportion & ") AS QTY ,UNIT_CODE FROM TSPL_MF_BATCH_ORDER_DETAIL"
        ''qry += " WHERE BO_CODE='" & obj.BO_CODE & "' GROUP BY PRODUCTION_LINE_CODE,ITEM_CODE,UNIT_CODE "
        'End If

        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        For Each dr As DataRow In dt.Rows
            objRM = New clsProductionRM
            objRM.RECEIPT_CODE = obj.RECEIPT_CODE
            objRM.PRODUCTION_LINE_CODE = clsCommon.myCstr(dr.Item("PRODUCTION_LINE_CODE"))
            objRM.CONSM_ITEM_CODE = clsCommon.myCstr(dr.Item("ITEM_CODE"))
            objRM.CONSM_QTY = dr.Item("QTY")
            objRM.LOCATION_CODE = obj.LOCATION_CODE
            objRM.UNIT_CODE = clsCommon.myCstr(dr.Item("UNIT_CODE"))
            objRM.FIFO_COST = clsCommon.myCstr(dr.Item("FIFO_COST"))
            objRM.LIFO_COST = dr.Item("LIFO_COST")
            objRM.AVG_COST = dr.Item("AVG_COST")
            objList.Add(objRM)
        Next
        Return objList
    End Function
    Public Shared Function SaveConsumeRMData(ByVal strDocNo As String, ByVal Arr As List(Of clsProductionRM), ByVal trans As SqlTransaction) As Boolean

        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                Dim qry As String = "DELETE FROM TSPL_MF_CONSUMPTION_DETAIL WHERE RECEIPT_CODE='" + strDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                For Each obj As clsProductionRM In Arr

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "RECEIPT_CODE", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "PRODUCTION_LINE_CODE", obj.PRODUCTION_LINE_CODE, True)
                    clsCommon.AddColumnsForChange(coll, "CONSM_ITEM_CODE", obj.CONSM_ITEM_CODE)

                    clsCommon.AddColumnsForChange(coll, "CONSM_QTY", obj.CONSM_QTY)
                    clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE)
                    clsCommon.AddColumnsForChange(coll, "UNIT_CODE", obj.UNIT_CODE)
                    clsCommon.AddColumnsForChange(coll, "FIFO_COST", obj.FIFO_COST)
                    clsCommon.AddColumnsForChange(coll, "LIFO_COST", obj.LIFO_COST)
                    clsCommon.AddColumnsForChange(coll, "AVG_COST", obj.AVG_COST)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_CONSUMPTION_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MF_CONSUMPTION_DETAIL.RECEIPT_CODE='" + strDocNo + "' ", trans)
                Next

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False

        End Try

        Return True
    End Function

    Public Shared Function GetConsumedRM(ByVal ReceiptCode As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsProductionRM)
        Dim qry As String = "select RECEIPT_CODE,PRODUCTION_LINE_CODE,CONSM_ITEM_CODE,CONSM_QTY,LOCATION_CODE,UNIT_CODE,FIFO_COST,LIFO_COST,AVG_COST from TSPL_MF_CONSUMPTION_DETAIL where receipt_code='" & ReceiptCode & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim obj As clsProductionRM
        Dim objList As New List(Of clsProductionRM)
        For Each dr As DataRow In dt.Rows
            obj = New clsProductionRM
            obj.RECEIPT_CODE = dr.Item("RECEIPT_CODE")
            obj.PRODUCTION_LINE_CODE = clsCommon.myCstr(dr.Item("PRODUCTION_LINE_CODE"))
            obj.CONSM_ITEM_CODE = dr.Item("CONSM_ITEM_CODE")
            obj.CONSM_QTY = dr.Item("CONSM_QTY")
            obj.LOCATION_CODE = dr.Item("LOCATION_CODE")
            obj.UNIT_CODE = dr.Item("UNIT_CODE")
            obj.FIFO_COST = dr.Item("FIFO_COST")
            obj.LIFO_COST = dr.Item("LIFO_COST")
            obj.AVG_COST = dr.Item("AVG_COST")
            objList.Add(obj)
        Next
        Return objList
    End Function
    Public Shared Function UpdateInventoryMovement(ByVal ReceiptCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            Dim obj As clsInventoryMovement
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()

            Dim strq As String = ""
            Dim objRec As clsProductionReceipt = clsProductionReceipt.GetData(ReceiptCode, NavigatorType.Current, trans)
            Dim objListProd As List(Of clsProductionReceipt) = clsProductionReceipt.ObjList
            '' get MO cost 
            strq = "select (TOTAL_COST-DIRECT_MATERIAL_COST) as TOTAL_COST from TSPL_MF_MO_COSTING where MO_CODE='" & objRec.MO_CODE & "' and CALC_TYPE='Actual'"
            Dim dtMOCost As DataTable
            dtMOCost = clsDBFuncationality.GetDataTable(strq, trans)
            Dim MOCost As Double = 0.0
            If dtMOCost.Rows.Count > 0 Then
                MOCost = dtMOCost.Rows(0).Item("TOTAL_COST")
            End If

            If (objListProd IsNot Nothing AndAlso objListProd.Count > 0) Then
                For Each objProd As clsProductionReceipt In objListProd

                    '' item locations


                    Dim objLocationDetails1 As New clsItemLocationDetails()
                    objLocationDetails1.Item_Code = objProd.ITEM_CODE
                    objLocationDetails1.Item_Desc = objProd.ITEM_DESCRIPTION
                    objLocationDetails1.Location_Code = objRec.LOCATION_CODE
                    objLocationDetails1.Location_Desc = objRec.LOCATION_NAME
                    objLocationDetails1.Item_Qty = objProd.RECEIPT_QTY
                    objLocationDetails1.Amount = objProd.FIFO_Cost
                    'objLocationDetails.MRP = 0
                    'If objTr.MFG_Date.HasValue Then
                    '    objLocationDetails.MFG_Date = objTr.MFG_Date
                    'End If
                    'objLocationDetails.Batch_No = objTr.Batch_No
                    'If objTr.Expiry_Date.HasValue Then
                    '    objLocationDetails.Expiry_Date = objTr.Expiry_Date
                    'End If

                    Dim itemtype As String = "select item_type from TSPL_ITEM_MASTER where item_code='" + objProd.ITEM_CODE + "'"
                    Dim type As String = clsDBFuncationality.getSingleValue(itemtype, trans)
                    If clsCommon.CompairString(type, "R") = CompairStringResult.Equal Then
                        objLocationDetails1.ItemType = "RM"
                    ElseIf clsCommon.CompairString(type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(type, "O") = CompairStringResult.Equal Then
                        objLocationDetails1.ItemType = "OT"
                    ElseIf clsCommon.CompairString(type, "F") = CompairStringResult.Equal Then
                        objLocationDetails1.ItemType = "FT"
                    End If
                    'objLocationDetails.ItemType = itemtype
                    ArrLocationDetails.Add(objLocationDetails1)

                    '''' end item locations
                    Dim strItemTypeToSave As String = ""
                    Dim strItemType As String

                    '' in produced item
                    obj = New clsInventoryMovement
                    obj.Trans_Type = "Production"
                    obj.InOut = "I"
                    obj.Location_Code = objRec.LOCATION_CODE
                    obj.Item_Code = objProd.ITEM_CODE
                    obj.Item_Desc = objProd.ITEM_DESCRIPTION
                    obj.Qty = objProd.RECEIPT_QTY
                    obj.UOM = objProd.UNIT_CODE
                    obj.Source_Doc_No = objRec.RECEIPT_CODE
                    obj.Source_Doc_Date = objRec.RECEIPT_DATE

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
                    If objProd.Costing_Method = 0 Then
                        If obj.Qty <> 0 Then
                            obj.Basic_Cost = objProd.FIFO_Cost / obj.Qty
                            obj.MRP = (objProd.FIFO_Cost + MOCost) / obj.Qty
                        Else
                            obj.Basic_Cost = 0
                            obj.MRP = 0
                        End If


                        obj.Add_Cost = 0
                        obj.MFG_Date = objProd.RECEIPT_DATE

                    ElseIf objProd.Costing_Method = 2 Then
                        If obj.Qty <> 0 Then
                            obj.Basic_Cost = objProd.LIFO_Cost / obj.Qty
                            obj.MRP = (objProd.LIFO_Cost + MOCost) / obj.Qty
                        Else
                            obj.Basic_Cost = 0
                            obj.MRP = 0

                        End If

                        obj.Add_Cost = 0
                        obj.MFG_Date = objProd.RECEIPT_DATE
                    ElseIf objProd.Costing_Method = 1 Then
                        obj.Basic_Cost = objProd.AVG_Cost / obj.Qty 'objProd.Basic_Cost
                        If obj.Qty <> 0 Then
                            obj.MRP = (objProd.AVG_Cost + MOCost) / obj.Qty
                        Else
                            obj.MRP = 0
                        End If

                        obj.Add_Cost = 0
                        obj.MFG_Date = objProd.RECEIPT_DATE
                    Else
                        If obj.Qty <> 0 Then
                            obj.Basic_Cost = objProd.FIFO_Cost / obj.Qty
                            obj.MRP = (objProd.FIFO_Cost + MOCost) / obj.Qty
                        Else
                            obj.Basic_Cost = 0
                            obj.MRP = 0
                        End If

                        obj.Add_Cost = 0
                        obj.MFG_Date = objProd.RECEIPT_DATE

                    End If
                    obj.FIFO_Cost = objProd.FIFO_Cost
                    obj.LIFO_Cost = objProd.LIFO_Cost
                    obj.Avg_Cost = objProd.AVG_Cost
                    ArrInventoryMovement.Add(obj)
                Next


            End If

            '' out consumed data
            '' get data
            If objRec.IS_TRADING = True Then

                For Each objProd As clsProductionReceipt In objListProd

                    'Dim objData As List(Of clsProductionRM) = clsProductionRM.GetConsumedRM(ReceiptCode, trans)
                    'For Each dr As clsProductionRM In objData

                    Dim objLocationDetails As New clsItemLocationDetails()
                    objLocationDetails.Item_Code = objProd.ITEM_CODE
                    objLocationDetails.Item_Desc = clsItemMaster.GetItemName(objProd.ITEM_CODE, trans)
                    Dim strIssueLocationCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select LOCATION_CODE from TSPL_MF_ISSUE where ISSUE_CODE = '" + objRec.ISSUE_CODE + "' ", trans))
                    objLocationDetails.Location_Code = strIssueLocationCode
                    objLocationDetails.Location_Desc = strIssueLocationCode
                    objLocationDetails.Item_Qty = -1 * (objProd.RECEIPT_QTY)
                    objLocationDetails.Amount = -1 * (objProd.FIFO_Cost)
                    'objLocationDetails.MRP = 0
                    'If objTr.MFG_Date.HasValue Then
                    '    objLocationDetails.MFG_Date = objTr.MFG_Date
                    'End If
                    'objLocationDetails.Batch_No = objTr.Batch_No
                    'If objTr.Expiry_Date.HasValue Then
                    '    objLocationDetails.Expiry_Date = objTr.Expiry_Date
                    'End If

                    Dim itemtype As String = "select item_type from TSPL_ITEM_MASTER where item_code='" + objProd.ITEM_CODE + "'"
                    Dim type As String = clsDBFuncationality.getSingleValue(itemtype, trans)
                    If clsCommon.CompairString(type, "R") = CompairStringResult.Equal Then
                        objLocationDetails.ItemType = "RM"
                    ElseIf clsCommon.CompairString(type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(type, "O") = CompairStringResult.Equal Then
                        objLocationDetails.ItemType = "OT"
                    ElseIf clsCommon.CompairString(type, "F") = CompairStringResult.Equal Then
                        objLocationDetails.ItemType = "FT"
                    End If
                    'objLocationDetails.ItemType = itemtype
                    ArrLocationDetails.Add(objLocationDetails)

                    obj = New clsInventoryMovement
                    obj.Trans_Type = "Consumption"
                    obj.InOut = "O"
                    obj.Location_Code = strIssueLocationCode
                    obj.Item_Code = objProd.ITEM_CODE
                    obj.Item_Desc = clsItemMaster.GetItemName(objProd.ITEM_CODE, trans)
                    obj.Qty = objProd.RECEIPT_QTY
                    obj.UOM = objProd.UNIT_CODE
                    obj.Source_Doc_No = objProd.RECEIPT_CODE
                    obj.Source_Doc_Date = objRec.RECEIPT_DATE

                    Dim strItemTypeToSave As String = ""
                    Dim strItemType As String

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
                    obj.Basic_Cost = objProd.AVG_Cost / obj.Qty 'objProd.Basic_Cost
                    obj.Add_Cost = 0
                    obj.MRP = (objProd.AVG_Cost + MOCost) / obj.Qty
                    obj.FIFO_Cost = objProd.FIFO_Cost
                    obj.LIFO_Cost = objProd.LIFO_Cost
                    obj.Avg_Cost = objProd.AVG_Cost


                    ArrInventoryMovement.Add(obj)

                Next
            Else

                'End If
                Dim objData As List(Of clsProductionRM) = clsProductionRM.GetConsumedRM(ReceiptCode, trans)
            For Each dr As clsProductionRM In objData

                Dim objLocationDetails As New clsItemLocationDetails()
                objLocationDetails.Item_Code = dr.CONSM_ITEM_CODE
                objLocationDetails.Item_Desc = clsItemMaster.GetItemName(dr.CONSM_ITEM_CODE, trans)
                objLocationDetails.Location_Code = dr.LOCATION_CODE
                objLocationDetails.Location_Desc = dr.LOCATION_CODE
                objLocationDetails.Item_Qty = -1 * (dr.CONSM_QTY)
                objLocationDetails.Amount = -1 * (dr.FIFO_COST)
                'objLocationDetails.MRP = 0
                'If objTr.MFG_Date.HasValue Then
                '    objLocationDetails.MFG_Date = objTr.MFG_Date
                'End If
                'objLocationDetails.Batch_No = objTr.Batch_No
                'If objTr.Expiry_Date.HasValue Then
                '    objLocationDetails.Expiry_Date = objTr.Expiry_Date
                'End If

                Dim itemtype As String = "select item_type from TSPL_ITEM_MASTER where item_code='" + dr.CONSM_ITEM_CODE + "'"
                Dim type As String = clsDBFuncationality.getSingleValue(itemtype, trans)
                If clsCommon.CompairString(type, "R") = CompairStringResult.Equal Then
                    objLocationDetails.ItemType = "RM"
                ElseIf clsCommon.CompairString(type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(type, "O") = CompairStringResult.Equal Then
                    objLocationDetails.ItemType = "OT"
                ElseIf clsCommon.CompairString(type, "F") = CompairStringResult.Equal Then
                    objLocationDetails.ItemType = "FT"
                End If
                'objLocationDetails.ItemType = itemtype
                ArrLocationDetails.Add(objLocationDetails)

                obj = New clsInventoryMovement
                obj.Trans_Type = "Consumption"
                obj.InOut = "O"
                obj.Location_Code = dr.LOCATION_CODE
                obj.Item_Code = dr.CONSM_ITEM_CODE
                obj.Item_Desc = clsItemMaster.GetItemName(dr.CONSM_ITEM_CODE, trans)
                obj.Qty = dr.CONSM_QTY
                obj.UOM = dr.UNIT_CODE
                obj.Source_Doc_No = dr.RECEIPT_CODE
                obj.Source_Doc_Date = objRec.RECEIPT_DATE

                Dim strItemTypeToSave As String = ""
                Dim strItemType As String

                strItemType = clsItemMaster.GetItemType(dr.CONSM_ITEM_CODE, trans)
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
                obj.Add_Cost = 0
                obj.MRP = 0
                obj.FIFO_Cost = dr.FIFO_COST
                obj.LIFO_Cost = dr.LIFO_COST
                obj.Avg_Cost = dr.AVG_COST

                ArrInventoryMovement.Add(obj)

            Next
            End If
            clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(objRec.RECEIPT_DATE, "dd/MM/yyyy"), ArrLocationDetails, trans)
            clsInventoryMovement.SaveData("Production", ReceiptCode, clsCommon.GetPrintDate(objRec.RECEIPT_DATE, "dd/MMM/yyyy"), clsCommon.GetPrintDate(objRec.RECEIPT_DATE, "dd/MM/yyyy"), ArrInventoryMovement, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False

        End Try
        Return True
    End Function

End Class
Public Class clsProductionReceiptAttachment
    Public Form_ID As String = ""
    Public Transaction_ID As String = ""
    Dim isInsideLoadData As Boolean = False
    Public ColCODE As String = ""
    Public ColFormId As String = ""
    Public ColTransactionId As String = ""
    Public ColSNo As String = ""
    Public ColFileName As String = ""
    Public ColCOMMENTS As String = ""
    Public ColPath As String = ""
    Public ColView As String = ""
    Public ColDelete As String = ""
    Public ColSelect As String = ""
    Public Prod_Plan_Code As String = ""
    Public BOM_CODE As String = ""
    Public ITEM_CODE As String = ""


    Dim objListAttachment As New List(Of clsProductionReceiptAttachment)

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsProductionReceiptAttachment), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                Dim obj As clsAttachDocument
                For Each obj1 As clsProductionReceiptAttachment In Arr
                    obj = New clsAttachDocument
                    obj.CODE = clsCommon.myCstr(obj1.ColCODE)
                    obj.FormId = obj1.Form_ID
                    obj.TransactionId = IIf(obj1.Transaction_ID = "", strDocNo, obj1.Transaction_ID)
                    obj.SNo = 1 'clsCommon.myCstr(obj1.ColSNo)
                    obj.FileName = clsCommon.myCstr(obj1.ColFileName)
                    obj.COMMENTS = clsCommon.myCstr(obj1.ColCOMMENTS)
                    obj.SaveData(obj)

                    Dim str As String
                    If clsCommon.myLen(obj1.ColPath) > 0 Then
                        Dim bData As Byte()
                        Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(clsCommon.myCstr(obj1.ColPath)))
                        bData = br.ReadBytes(br.BaseStream.Length)

                        str = " UPDATE TSPL_ATTACHMENTS set FileData = @BLOBData where CODE='" + obj.CODE + "'"
                        Dim cmd As SqlCommand = New SqlCommand(str, clsDBFuncationality.GetConnnection)
                        Dim prm As New SqlParameter("@BLOBData", bData)
                        cmd.Parameters.Add(prm)
                        cmd.ExecuteNonQuery()
                        br.Close() ' done by stuti reagrding memory leakage
                    End If
                    Dim isTrading As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select Count (*) from TSPL_MF_RECEIPT where  IS_TRADING = 1 and Receipt_Code = '" & strDocNo & "'  ", trans))

                    If isTrading = True Then
                        str = "update TSPL_MF_RECEIPT_DETAIL set Attachment='" & obj.CODE & "' where Receipt_Code ='" & strDocNo & "'   and  ITEM_CODE='" & obj1.ITEM_CODE & "'"
                    Else
                        str = "update TSPL_MF_RECEIPT_DETAIL set Attachment='" & obj.CODE & "' where Receipt_Code ='" & strDocNo & "' and Prod_Plan_Code = '" + obj1.Prod_Plan_Code + "' and BOM_CODE = '" + obj1.BOM_CODE + "' and  ITEM_CODE='" & obj1.ITEM_CODE & "'"
                    End If
                    clsDBFuncationality.ExecuteNonQuery(str, trans)
                Next
            End If
            Return True
            'LoadData(Transaction_ID)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try

    End Function
End Class