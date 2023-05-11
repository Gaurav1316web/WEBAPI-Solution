Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsBatchOrder

#Region "Variables"

    Public BO_CODE As String
    Public Manual_Batch_No As String
    Public DESCRIPTION As String
    Public BO_DATE As Date

    Public CREATED_BY As String
    Public APPROVED_BY As String

    Public POSTED As Boolean
    Public Posting_Date As Date


    '' grid columns-pp details


    Public PROD_PLAN_CODE As String
    Public PLANNING_DATE As Date
    Public PLAN_FOR_DATE As Date

    Public PRODUCTION_LINE_CODE As String
    Public BOM_CODE As String
    Public BOM_REVISION_NO As String
    Public ITEM_CODE As String
    Public ITEM_DESCRIPTION As String
    Public MIN_QUANTITY As Decimal
    Public MAX_QUANTITY As Decimal
    Public UNIT_CODE As String
    Public REMARKS As String

    Public BATCH_QTY As Decimal
    Public START_TIME As DateTime? = Nothing
    Public END_TIME As DateTime? = Nothing
    Public HOURS As Decimal
    Public SPEED As Decimal
    Public REASON As String
    Public MF_DATE As Date
    Public EXP_DATE As Date? = Nothing

    ''GRID COLUMNS - RM DETAILS
    Public RM_ITEM_CODE As String
    Public RM_UNIT_CODE As String
    Public RM_QTY As Decimal
    Public RM_ACTUAL_REQ_QTY As Decimal
    Public ISSUE_QTY As Decimal
    Public RETURN_QTY As Decimal
    Public BREAKAGE_QTY As Decimal
    Public REJ_QTY As Decimal
    Public CONSM_QTY As Decimal
    Public locationcode As String = Nothing
    Public locationname As String = Nothing
    Public Shared ObjList As List(Of clsBatchOrder)

#End Region


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBatchOrder
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
            qry = "delete from TSPL_MF_BATCH_ORDER_DETAIL where BO_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MF_BATCH_PP_DETAIL where BO_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MF_BATCH_ORDER where BO_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsBatchOrder
        Dim obj As New clsBatchOrder()
        Dim objtr As New clsBatchOrder()

        ObjList = New List(Of clsBatchOrder)

        Dim qry As String = "SELECT T1.BO_CODE,T1.DESCRIPTION,T1.Manual_Batch_No,T1.BO_DATE,T1.Location_Code, "
        qry += " T1.MODIFIED_BY AS APPROVED_BY,T1.Created_By,T1.POSTED,T1.POSTING_DATE FROM TSPL_MF_BATCH_ORDER  T1 where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " AND BO_CODE = (select MIN(BO_CODE) from TSPL_MF_BATCH_ORDER)"
            Case NavigatorType.Last
                qry += " AND BO_CODE = (select Max(BO_CODE) from TSPL_MF_BATCH_ORDER)"
            Case NavigatorType.Next
                qry += " AND BO_CODE = (select Min(BO_CODE) from TSPL_MF_BATCH_ORDER where  BO_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " AND BO_CODE = (select Max(BO_CODE) from TSPL_MF_BATCH_ORDER where BO_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " AND BO_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj.BO_CODE = dt.Rows(0)("BO_CODE")
            obj.Manual_Batch_No = clsCommon.myCstr(dt.Rows(0)("Manual_Batch_No"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.BO_DATE = clsCommon.GetPrintDate(dt.Rows(0)("BO_DATE"), "dd/MMM/yyyy")

            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.APPROVED_BY = clsCommon.myCstr(dt.Rows(0)("APPROVED_BY"))
            obj.POSTED = clsCommon.myCstr(dt.Rows(0)("POSTED"))

            strCode = dt.Rows(0)("BO_CODE")

            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
            obj.locationcode = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.locationname = clsLocation.GetName(obj.locationcode, trans)
        End If
        qry = ""
        qry += " SELECT T1.*,T2.PLANNING_DATE,T2.PLAN_FOR_DATE FROM  TSPL_MF_BATCH_PP_DETAIL T1 INNER JOIN TSPL_MF_PRODUCTION_PLAN_HEAD T2 ON T1.PROD_PLAN_CODE=T2.PROD_PLAN_CODE WHERE 2=2"
        qry += " AND T1.BO_CODE = '" + strCode + "' ORDER BY T1.PRODUCTION_LINE_CODE"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsBatchOrder()

                objtr.PROD_PLAN_CODE = clsCommon.myCstr(dr("PROD_PLAN_CODE"))
                objtr.PLANNING_DATE = clsCommon.myCstr(dr("PLANNING_DATE"))
                objtr.PLAN_FOR_DATE = clsCommon.myCstr(dr("PLAN_FOR_DATE"))
                objtr.PRODUCTION_LINE_CODE = clsCommon.myCstr(dr("PRODUCTION_LINE_CODE"))
                objtr.PRODUCTION_LINE_CODE = clsCommon.myCstr(dr("PRODUCTION_LINE_CODE"))
                objtr.BOM_CODE = clsCommon.myCstr(dr("BOM_CODE"))
                objtr.BOM_REVISION_NO = clsCommon.myCstr(dr("BOM_REVISION_NO"))
                objtr.ITEM_CODE = clsCommon.myCstr(dr("ITEM_CODE"))
                objtr.ITEM_DESCRIPTION = clsCommon.myCstr(dr("ITEM_DESCRIPTION"))
                objtr.MIN_QUANTITY = clsCommon.myCdbl(dr("MIN_QUANTITY"))
                objtr.MAX_QUANTITY = clsCommon.myCdbl(dr("MAX_QUANTITY"))
                objtr.UNIT_CODE = clsCommon.myCstr(dr("UNIT_CODE"))
                objtr.REMARKS = clsCommon.myCstr(dr("REMARKS"))

                objtr.BATCH_QTY = clsCommon.myCdbl(dr("BATCH_QTY"))

                If IsDBNull(dr("START_TIME")) = True Then
                    objtr.START_TIME = Nothing
                Else
                    'Format(obj.START_TIME, "hh:mm:ss tt")

                    objtr.START_TIME = clsCommon.myCDate(dr("PLAN_FOR_DATE")).Add(dr("START_TIME"))
                End If

                If IsDBNull(dr("END_TIME")) = True Then
                    objtr.END_TIME = Nothing
                Else
                    objtr.END_TIME = clsCommon.myCDate(dr("PLAN_FOR_DATE")).Add(dr("END_TIME"))
                End If



                objtr.HOURS = clsCommon.myCstr(dr("HOURS"))
                objtr.SPEED = clsCommon.myCdbl(dr("SPEED"))
                objtr.REASON = clsCommon.myCstr(dr("REASON"))
                objtr.MF_DATE = clsCommon.myCDate(dr("MF_DATE"))
                If IsDBNull(dr("EXP_DATE")) = True Then
                    objtr.EXP_DATE = Nothing
                Else
                    objtr.EXP_DATE = clsCommon.myCDate(dr("EXP_DATE"))
                End If


                ObjList.Add(objtr)
            Next
        End If

        clsBatchOrder.ObjList = ObjList
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsBatchOrder, ByVal objList As List(Of clsBatchOrder), ByVal objList2 As List(Of clsBatchOrder), ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True
        If isNewEntry Then
            If strCode = "" Then
                obj.BO_CODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(obj.BO_DATE, "dd/MMM/yyyy"), clsDocType.BATCHORDER, "", obj.locationcode)
            Else
                obj.BO_CODE = strCode
            End If
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try

            Dim qry As String = "DELETE FROM TSPL_MF_BATCH_PP_DETAIL WHERE BO_CODE='" + obj.BO_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "DELETE FROM TSPL_MF_BATCH_ORDER_DETAIL WHERE BO_CODE='" + obj.BO_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""

            If (clsCommon.myLen(obj.BO_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "BO_CODE", obj.BO_CODE)
            clsCommon.AddColumnsForChange(coll, "Manual_Batch_No", obj.Manual_Batch_No, True)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "BO_DATE", clsCommon.GetPrintDate(obj.BO_DATE, "dd/MMM/yyyy"))


            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.locationcode) 'Added by preeti Gupta Against Ticket No[ADV/24/07/18-000035]


            If isNewEntry Then


                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_MF_BATCH_ORDER where BO_CODE = '" & obj.BO_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_BATCH_ORDER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.BO_CODE + " Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_BATCH_ORDER", OMInsertOrUpdate.Update, "TSPL_MF_BATCH_ORDER.BO_CODE='" + obj.BO_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsBatchOrderDetail.SavePPData(obj.BO_CODE, objList, trans)
            isSaved = isSaved AndAlso clsBatchOrderDetail.SaveRMData(obj.BO_CODE, objList2, trans)

            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
            Return False
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsBatchOrder = clsBatchOrder.GetData(strDocNo, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.BO_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_MF_BATCH_ORDER set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where BO_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetBatchOrderDT(ByVal FromBODate As Date, ByVal ToBODate As Date) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""
            qry += " SELECT BO_CODE AS 'Batch Order No',DESCRIPTION AS 'Description',BO_DATE AS 'Batch Date',"
            qry += " (case when POSTED=1 then 'Yes' else 'No' end) AS 'Is Approved',APPROVED_BY AS 'Approved By',"
            qry += " Created_By AS 'Created By' ,Created_Date AS 'Created Date' FROM TSPL_MF_BATCH_ORDER "
            qry += " where BO_DATE between '" & clsCommon.GetPrintDate(FromBODate, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(ToBODate, "dd/MMM/yyyy") & "'"


            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function
    Public Shared Function GetBatchOrderStatusDT(ByVal FromBODate As Date, ByVal ToBODate As Date, ByVal figure As String) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""

            qry = " select * from (SELECT 'All' AS 'Batch Order Status',('All' + '(' + CAST(COUNT(BO_CODE) AS VARCHAR) + ')') as 'Status', CONVERT(DECIMAL(18,2), COUNT(BO_CODE)/" + figure + ") AS Total FROM TSPL_MF_BATCH_ORDER " & _
                 " where BO_DATE between '" & clsCommon.GetPrintDate(FromBODate, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(ToBODate, "dd/MMM/yyyy") & "'" & _
                 " union all " & _
                 " SELECT 'Authorized' AS 'Batch Order Status',('Authorized' + '(' + CAST(COUNT(DISTINCT TSPL_MF_BATCH_ORDER.BO_CODE) AS VARCHAR) + ')') as 'Status',COUNT(DISTINCT TSPL_MF_BATCH_ORDER.BO_CODE) AS Total FROM TSPL_MF_BATCH_ORDER " & _
                 " LEFT JOIN TSPL_MF_REQ_DETAIL on  TSPL_MF_BATCH_ORDER.BO_CODE=TSPL_MF_REQ_DETAIL.BO_CODE " & _
                 " LEFT JOIN TSPL_MF_RECEIPT on  TSPL_MF_BATCH_ORDER.BO_CODE=TSPL_MF_RECEIPT.BO_CODE " & _
                 " where TSPL_MF_BATCH_ORDER.BO_DATE between '" & clsCommon.GetPrintDate(FromBODate, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(ToBODate, "dd/MMM/yyyy") & "' and TSPL_MF_BATCH_ORDER.POSTED=1 AND TSPL_MF_REQ_DETAIL.BO_CODE IS NULL AND TSPL_MF_RECEIPT.BO_CODE IS NULL " & _
                 " union all " & _
                 " SELECT 'WIP' AS 'Batch Order Status',('WIP' + '(' + CAST(COUNT(distinct TSPL_MF_BATCH_ORDER.BO_CODE) AS VARCHAR) + ')') as 'Status',COUNT(distinct TSPL_MF_BATCH_ORDER.BO_CODE) AS Total FROM TSPL_MF_BATCH_ORDER  " & _
                 " inner join TSPL_MF_REQ_DETAIL on  TSPL_MF_BATCH_ORDER.BO_CODE=TSPL_MF_REQ_DETAIL.BO_CODE " & _
                 " LEFT JOIN TSPL_MF_RECEIPT on  TSPL_MF_BATCH_ORDER.BO_CODE=TSPL_MF_RECEIPT.BO_CODE " & _
                 " where TSPL_MF_BATCH_ORDER.BO_DATE between '" & clsCommon.GetPrintDate(FromBODate, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(ToBODate, "dd/MMM/yyyy") & "' and TSPL_MF_BATCH_ORDER.POSTED=1 AND TSPL_MF_RECEIPT.BO_CODE IS NULL " & _
                 " union all " & _
                 " SELECT 'Closed' AS 'Batch Order Status',('Closed' + '(' + CAST(COUNT(distinct TSPL_MF_BATCH_ORDER.BO_CODE) AS VARCHAR) + ')') as 'Status',COUNT(distinct TSPL_MF_BATCH_ORDER.BO_CODE) AS Total FROM TSPL_MF_BATCH_ORDER  " & _
                 " inner join TSPL_MF_RECEIPT on  TSPL_MF_BATCH_ORDER.BO_CODE=TSPL_MF_RECEIPT.BO_CODE " & _
                 " where TSPL_MF_BATCH_ORDER.BO_DATE between '" & clsCommon.GetPrintDate(FromBODate, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(ToBODate, "dd/MMM/yyyy") & "' and TSPL_MF_BATCH_ORDER.POSTED=1) as TAB "

            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function
    Public Shared Function GetBatchOrderStatusDetail(ByVal FromBODate As Date, ByVal ToBODate As Date, Optional ByVal Status As String = "All") As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""

            qry = " select * from (SELECT BO_CODE as 'Batch Order No',DESCRIPTION AS 'Description',BO_DATE AS 'Batch Date','All' AS 'Status' FROM TSPL_MF_BATCH_ORDER " & _
                 " where BO_DATE between '" & clsCommon.GetPrintDate(FromBODate, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(ToBODate, "dd/MMM/yyyy") & "'" & _
                 " union all " & _
                 " SELECT TSPL_MF_BATCH_ORDER.BO_CODE as 'Batch Order No',TSPL_MF_BATCH_ORDER.DESCRIPTION AS 'Description',TSPL_MF_BATCH_ORDER.BO_DATE AS 'Batch Date','Authorized' AS 'Status' FROM TSPL_MF_BATCH_ORDER " & _
                 " LEFT JOIN TSPL_MF_REQ_DETAIL on  TSPL_MF_BATCH_ORDER.BO_CODE=TSPL_MF_REQ_DETAIL.BO_CODE " & _
                 " LEFT JOIN TSPL_MF_RECEIPT on  TSPL_MF_BATCH_ORDER.BO_CODE=TSPL_MF_RECEIPT.BO_CODE " & _
                 " where TSPL_MF_BATCH_ORDER.BO_DATE between '" & clsCommon.GetPrintDate(FromBODate, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(ToBODate, "dd/MMM/yyyy") & "' and TSPL_MF_BATCH_ORDER.POSTED=1 AND TSPL_MF_REQ_DETAIL.BO_CODE IS NULL AND TSPL_MF_RECEIPT.BO_CODE IS NULL " & _
                 " union all " & _
                 " SELECT distinct TSPL_MF_BATCH_ORDER.BO_CODE as 'Batch Order No',TSPL_MF_BATCH_ORDER.DESCRIPTION AS 'Description',TSPL_MF_BATCH_ORDER.BO_DATE AS 'Batch Date','WIP' AS 'Status' FROM TSPL_MF_BATCH_ORDER  " & _
                 " inner join TSPL_MF_REQ_DETAIL on  TSPL_MF_BATCH_ORDER.BO_CODE=TSPL_MF_REQ_DETAIL.BO_CODE " & _
                 " LEFT JOIN TSPL_MF_RECEIPT on  TSPL_MF_BATCH_ORDER.BO_CODE=TSPL_MF_RECEIPT.BO_CODE " & _
                 " where TSPL_MF_BATCH_ORDER.BO_DATE between '" & clsCommon.GetPrintDate(FromBODate, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(ToBODate, "dd/MMM/yyyy") & "' and TSPL_MF_BATCH_ORDER.POSTED=1  AND TSPL_MF_RECEIPT.BO_CODE IS NULL" & _
                 " union all " & _
                 " SELECT  TSPL_MF_BATCH_ORDER.BO_CODE as 'Batch Order No',TSPL_MF_BATCH_ORDER.DESCRIPTION AS 'Description',TSPL_MF_BATCH_ORDER.BO_DATE AS 'Batch Date','Closed' AS 'Status' FROM TSPL_MF_BATCH_ORDER  " & _
                 " inner join TSPL_MF_RECEIPT on  TSPL_MF_BATCH_ORDER.BO_CODE=TSPL_MF_RECEIPT.BO_CODE " & _
                 " where TSPL_MF_BATCH_ORDER.BO_DATE between '" & clsCommon.GetPrintDate(FromBODate, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(ToBODate, "dd/MMM/yyyy") & "' and TSPL_MF_BATCH_ORDER.POSTED=1) as TAB WHERE [Status]='" & Status & "'"

            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function
End Class


Public Class clsBatchOrderDetail
#Region "Variables"

#End Region

    Public Shared Function SavePPData(ByVal strDocNo As String, ByVal Arr As List(Of clsBatchOrder), ByVal trans As SqlTransaction) As Boolean

        Try
            'Dim qry As String = "DELETE FROM TSPL_MF_BATCH_PP_DETAIL WHERE BO_CODE='" + strDocNo + "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsBatchOrder In Arr

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "BO_CODE", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "PROD_PLAN_CODE", obj.PROD_PLAN_CODE)
                    clsCommon.AddColumnsForChange(coll, "PRODUCTION_LINE_CODE", obj.PRODUCTION_LINE_CODE, True)
                    clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE)
                    clsCommon.AddColumnsForChange(coll, "BOM_REVISION_NO", obj.BOM_REVISION_NO)
                    clsCommon.AddColumnsForChange(coll, "ITEM_CODE", obj.ITEM_CODE)
                    clsCommon.AddColumnsForChange(coll, "ITEM_DESCRIPTION", obj.ITEM_DESCRIPTION)
                    clsCommon.AddColumnsForChange(coll, "MIN_QUANTITY", obj.MIN_QUANTITY)
                    clsCommon.AddColumnsForChange(coll, "MAX_QUANTITY", obj.MAX_QUANTITY)
                    clsCommon.AddColumnsForChange(coll, "UNIT_CODE", obj.UNIT_CODE)
                    clsCommon.AddColumnsForChange(coll, "REMARKS", obj.REMARKS)

                    clsCommon.AddColumnsForChange(coll, "BATCH_QTY", obj.BATCH_QTY)
                    If obj.START_TIME Is Nothing Then
                    Else
                        clsCommon.AddColumnsForChange(coll, "START_TIME", Format(obj.START_TIME, "hh:mm:ss tt"), True)
                    End If
                    If obj.END_TIME Is Nothing Then
                    Else
                        clsCommon.AddColumnsForChange(coll, "END_TIME", Format(obj.END_TIME, "hh:mm:ss tt"), True)
                    End If
                    clsCommon.AddColumnsForChange(coll, "HOURS", obj.HOURS)
                    clsCommon.AddColumnsForChange(coll, "SPEED", obj.SPEED)
                    clsCommon.AddColumnsForChange(coll, "REASON", obj.REASON)
                    clsCommon.AddColumnsForChange(coll, "MF_DATE", clsCommon.GetPrintDate(obj.MF_DATE, "dd/MMM/yyyy"))

                    If obj.EXP_DATE Is Nothing Then
                    Else
                        clsCommon.AddColumnsForChange(coll, "EXP_DATE", clsCommon.GetPrintDate(obj.EXP_DATE, "dd/MMM/yyyy"), True)
                    End If


                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_BATCH_PP_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MF_BATCH_PP_DETAIL.BO_CODE='" + strDocNo + "' ", trans)
                Next

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False

        End Try

        Return True
    End Function


    Public Shared Function SaveRMData(ByVal strDocNo As String, ByVal Arr As List(Of clsBatchOrder), ByVal trans As SqlTransaction) As Boolean
        Try


            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsBatchOrder In Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "BO_CODE", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "PROD_PLAN_CODE", obj.PROD_PLAN_CODE)
                    clsCommon.AddColumnsForChange(coll, "PRODUCTION_LINE_CODE", obj.PRODUCTION_LINE_CODE, True)
                    clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE)
                    clsCommon.AddColumnsForChange(coll, "ITEM_CODE", obj.RM_ITEM_CODE)
                    clsCommon.AddColumnsForChange(coll, "UNIT_CODE", obj.RM_UNIT_CODE)
                    clsCommon.AddColumnsForChange(coll, "QTY", obj.RM_QTY)
                    clsCommon.AddColumnsForChange(coll, "ACTUAL_REQ_QTY", obj.RM_ACTUAL_REQ_QTY)
                    clsCommon.AddColumnsForChange(coll, "ISSUE_QTY", obj.ISSUE_QTY)
                    clsCommon.AddColumnsForChange(coll, "RETURN_QTY", obj.RETURN_QTY)
                    clsCommon.AddColumnsForChange(coll, "BREAKAGE_QTY", obj.BREAKAGE_QTY)
                    clsCommon.AddColumnsForChange(coll, "REJ_QTY", obj.REJ_QTY)
                    clsCommon.AddColumnsForChange(coll, "CONSM_QTY", obj.CONSM_QTY)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_BATCH_ORDER_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MF_BATCH_ORDER_DETAIL.BO_CODE='" + strDocNo + "'", trans)
                Next

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try

        Return True
    End Function
    Public Shared Function GetProdDetail(ByVal FromBODate As Date, ByVal ToBODate As Date) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""
            qry += " SELECT TSPL_MF_BATCH_PP_DETAIL.BO_CODE AS 'Batch Order No',TSPL_MF_BATCH_PP_DETAIL.PROD_PLAN_CODE AS 'Production Plan Code',"
            qry += " TSPL_MF_PRODUCTION_PLAN_HEAD.PLANNING_DATE AS 'Planning Date', "
            qry += " TSPL_MF_PRODUCTION_PLAN_HEAD.PLAN_FOR_DATE AS 'Plan For Date',TSPL_MF_BATCH_PP_DETAIL.PRODUCTION_LINE_CODE AS 'Production Line Code',"
            qry += " TSPL_MF_BATCH_PP_DETAIL.ITEM_CODE AS 'Produced Item Code',"
            qry += " TSPL_MF_BATCH_PP_DETAIL.ITEM_DESCRIPTION AS 'Item Desc',TSPL_MF_BATCH_PP_DETAIL.BOM_CODE AS 'BOM Code', "
            qry += " TSPL_MF_BATCH_PP_DETAIL.BOM_REVISION_NO AS 'BOM Revision No',"
            qry += " TSPL_MF_BATCH_PP_DETAIL.MIN_QUANTITY 'Min. Qty',TSPL_MF_BATCH_PP_DETAIL.MAX_QUANTITY AS 'Max. Qty',TSPL_MF_BATCH_PP_DETAIL.UNIT_CODE AS 'UOM',"
            qry += " TSPL_MF_BATCH_PP_DETAIL.BATCH_QTY AS 'Batch Qty',TSPL_MF_BATCH_PP_DETAIL.START_TIME AS 'Start Time',"
            qry += " TSPL_MF_BATCH_PP_DETAIL.END_TIME AS 'Stop Time',TSPL_MF_BATCH_PP_DETAIL.HOURS AS 'Run Time(HRS)' ,TSPL_MF_BATCH_PP_DETAIL.REASON AS 'Reason',"
            qry += " TSPL_MF_BATCH_PP_DETAIL.MF_DATE AS 'MFG Date',TSPL_MF_BATCH_PP_DETAIL.EXP_DATE AS 'EXP Date',"
            qry += " TSPL_MF_BATCH_PP_DETAIL.SPEED AS 'Speed' FROM TSPL_MF_BATCH_PP_DETAIL  "
            qry += " INNER JOIN TSPL_MF_BATCH_ORDER ON TSPL_MF_BATCH_PP_DETAIL.BO_CODE=TSPL_MF_BATCH_ORDER.BO_CODE "
            qry += " LEFT JOIN TSPL_MF_PRODUCTION_PLAN_HEAD  ON TSPL_MF_BATCH_PP_DETAIL.PROD_PLAN_CODE=TSPL_MF_PRODUCTION_PLAN_HEAD.PROD_PLAN_CODE  "
            qry += " WHERE TSPL_MF_BATCH_ORDER.BO_DATE BETWEEN  '" & clsCommon.GetPrintDate(FromBODate, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(ToBODate, "dd/MMM/yyyy") & "'"
            qry += " ORDER BY TSPL_MF_BATCH_PP_DETAIL.BO_CODE,TSPL_MF_BATCH_PP_DETAIL.PROD_PLAN_CODE,TSPL_MF_BATCH_PP_DETAIL.PRODUCTION_LINE_CODE,TSPL_MF_BATCH_PP_DETAIL.ITEM_CODE"

            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function

    Public Shared Function GetRMDetail(ByVal FromBODate As Date, ByVal ToBODate As Date) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""
            qry += " SELECT TSPL_MF_BATCH_ORDER_DETAIL.BO_CODE AS 'Batch Order No',TSPL_MF_BATCH_ORDER_DETAIL.PROD_PLAN_CODE AS 'Production Plan Code',"
            qry += " TSPL_MF_BATCH_ORDER_DETAIL.PRODUCTION_LINE_CODE AS 'Production Line Code',"
            qry += " TSPL_MF_BATCH_ORDER_DETAIL.ITEM_CODE AS 'RM Item Code',T2.Item_Desc AS 'RM Item Desc',TSPL_MF_BATCH_ORDER_DETAIL.QTY AS 'Required Qty',"
            qry += " TSPL_MF_BATCH_ORDER_DETAIL.UNIT_CODE AS 'UOM' FROM TSPL_MF_BATCH_ORDER_DETAIL  "
            qry += " INNER JOIN TSPL_MF_BATCH_ORDER ON TSPL_MF_BATCH_ORDER_DETAIL.BO_CODE=TSPL_MF_BATCH_ORDER.BO_CODE"
            qry += " LEFT JOIN TSPL_ITEM_MASTER T2 ON TSPL_MF_BATCH_ORDER_DETAIL.ITEM_CODE=T2.Item_Code "
            qry += " WHERE TSPL_MF_BATCH_ORDER.BO_DATE BETWEEN  '" & clsCommon.GetPrintDate(FromBODate, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(ToBODate, "dd/MMM/yyyy") & "'"
            qry += " ORDER BY TSPL_MF_BATCH_ORDER_DETAIL.BO_CODE,TSPL_MF_BATCH_ORDER_DETAIL.PROD_PLAN_CODE,TSPL_MF_BATCH_ORDER_DETAIL.PRODUCTION_LINE_CODE,TSPL_MF_BATCH_ORDER_DETAIL.ITEM_CODE"
            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function

End Class
