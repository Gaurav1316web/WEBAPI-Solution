Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsCustomerTargetFixing
#Region "Variables"

    Public TARGET_CODE As String

    Public CUST_CODE As String
    Public CUST_NAME As String
    Public MONTH_NO As Integer
    Public YEAR_NO As Integer
    Public MONTH_NAME As String
    Public MONTH_YEAR As Date
    Public TARGET As Double
    Public INCENTIVE As Double
    Public TARGET_TYPE As String
    Public DESCRIPTION As String



    '' grid columns
    Public ITEM_CODE As String
    Public ITEM_NAME As String
    Public ITEM_TARGET As Double
    Public ITEM_INCENTIVE As Double

    Public Shared ObjList As List(Of clsCustomerTargetFixing)
    Public Arr As New List(Of clsCustomerTargetDetails)

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsCustomerTargetFixing
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
            qry = "delete from TSPL_SALES_CUSTOMER_TARGET_DETAIL where TARGET_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SALES_CUSTOMER_TARGET where TARGET_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCustomerTargetFixing
        Dim obj As New clsCustomerTargetFixing()
        Dim objtr As New clsCustomerTargetFixing()

        ObjList = New List(Of clsCustomerTargetFixing)

        Dim qry As String = "SELECT TAV.*,CST.Customer_Name AS CUST_NAME  FROM TSPL_SALES_CUSTOMER_TARGET TAV "
        qry += "LEFT JOIN TSPL_CUSTOMER_MASTER CST ON TAV.CUST_CODE=CST.CUST_CODE where 2=2 "


        Select Case NavType
            Case NavigatorType.First
                qry += " and TARGET_CODE = (select MIN(TARGET_CODE) from TSPL_SALES_CUSTOMER_TARGET)"
            Case NavigatorType.Last
                qry += " and TARGET_CODE = (select Max(TARGET_CODE) from TSPL_SALES_CUSTOMER_TARGET)"
            Case NavigatorType.Next
                qry += " and TARGET_CODE = (select Min(TARGET_CODE) from TSPL_SALES_CUSTOMER_TARGET where  TARGET_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TARGET_CODE = (select Max(TARGET_CODE) from TSPL_SALES_CUSTOMER_TARGET where TARGET_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TARGET_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj.TARGET_CODE = dt.Rows(0)("TARGET_CODE")
            obj.CUST_CODE = dt.Rows(0)("CUST_CODE")
            obj.CUST_NAME = dt.Rows(0)("CUST_NAME")
            obj.MONTH_NO = dt.Rows(0)("MONTH_No")
            obj.MONTH_NAME = dt.Rows(0)("MONTH_NAME")
            obj.YEAR_NO = dt.Rows(0)("YEAR_NO")
            obj.MONTH_YEAR = clsCommon.myCDate(1 & "/" & dt.Rows(0)("MONTH_NAME") & "/" & dt.Rows(0)("YEAR_NO"))
            obj.TARGET = dt.Rows(0)("TARGET")
            obj.INCENTIVE = dt.Rows(0)("INCENTIVE")
            obj.TARGET_TYPE = dt.Rows(0)("TARGET_TYPE")
            obj.DESCRIPTION = dt.Rows(0)("DESCRIPTION")

            strCode = dt.Rows(0)("TARGET_CODE")


        End If

        qry = "select TAV.TARGET_CODE,TAV.CUST_CODE,TAVD.ITEM_CODE,CST.CUSTOMER_NAME AS CUST_NAME,ITEM.Item_Desc AS ITEM_NAME, "
        qry += " TAVD.ITEM_TARGET,TAVD.ITEM_INCENTIVE FROM  TSPL_SALES_CUSTOMER_TARGET_DETAIL TAVD "
        qry += " INNER JOIN  TSPL_SALES_CUSTOMER_TARGET TAV ON TAVD.TARGET_CODE=TAV.TARGET_CODE "
        qry += " INNER JOIN TSPL_CUSTOMER_MASTER CST ON TAV.CUST_CODE=CST.CUST_CODE "
        qry += " INNER JOIN TSPL_ITEM_MASTER ITEM ON TAVD.ITEM_CODE=ITEM.ITEM_CODE where 2=2"

        qry += " and TAV.TARGET_CODE = '" + strCode + "'"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsCustomerTargetFixing()
                objtr.TARGET_CODE = clsCommon.myCstr(dr("TARGET_CODE"))
                objtr.CUST_CODE = clsCommon.myCstr(dr("CUST_CODE"))
                objtr.CUST_NAME = clsCommon.myCstr(dr("CUST_NAME"))
                objtr.ITEM_CODE = clsCommon.myCstr(dr("ITEM_CODE"))
                objtr.ITEM_NAME = clsCommon.myCstr(dr("ITEM_NAME"))
                objtr.ITEM_TARGET = clsCommon.myCstr(dr("ITEM_TARGET"))
                objtr.ITEM_INCENTIVE = clsCommon.myCstr(dr("ITEM_INCENTIVE"))

                ObjList.Add(objtr)
            Next
        End If

        clsCustomerTargetFixing.ObjList = ObjList
        Return obj
    End Function
    Public Function SaveData(ByVal obj As clsCustomerTargetFixing, ByVal objList As List(Of clsCustomerTargetFixing), ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True
        If isNewEntry Then
            If strCode = "" Then
                obj.TARGET_CODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.myCDate(clsCommon.GETSERVERDATE()), clsDocType.MonthlyAttendance, "", "")
            Else
                obj.TARGET_CODE = strCode
            End If


        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try

            Dim qry As String = "delete from TSPL_SALES_CUSTOMER_TARGET_DETAIL where TARGET_CODE='" + obj.TARGET_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""

            If (clsCommon.myLen(obj.TARGET_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "TARGET_CODE", obj.TARGET_CODE)
            clsCommon.AddColumnsForChange(coll, "CUST_CODE", obj.CUST_CODE)
            clsCommon.AddColumnsForChange(coll, "MONTH_NO", obj.MONTH_NO)
            clsCommon.AddColumnsForChange(coll, "MONTH_NAME", obj.MONTH_NAME)
            clsCommon.AddColumnsForChange(coll, "YEAR_NO", obj.YEAR_NO)
            clsCommon.AddColumnsForChange(coll, "TARGET", obj.TARGET)
            clsCommon.AddColumnsForChange(coll, "INCENTIVE", obj.INCENTIVE)
            clsCommon.AddColumnsForChange(coll, "TARGET_TYPE", obj.TARGET_TYPE)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then

                'clsCommon.AddColumnsForChange(coll, "TARGET_CODE", obj.TARGET_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALES_CUSTOMER_TARGET", OMInsertOrUpdate.Insert, "", trans)
            Else

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALES_CUSTOMER_TARGET", OMInsertOrUpdate.Update, "TSPL_SALES_CUSTOMER_TARGET.TARGET_CODE='" + obj.TARGET_CODE + "'", trans)
            End If


            isSaved = isSaved AndAlso clsCustomerTargetDetails.SaveData(obj.TARGET_CODE, objList, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetCustomerTargetDT(ByVal month As Integer, ByVal year As Integer, ByVal all As Boolean) As DataTable
        Dim DT As DataTable
        Dim strq As String
        Dim cond As String
        If all = True Then
            cond = ""
        Else
            cond = " where t1.month_code='" & MonthName(month) & "-" & year & "'"
        End If
        'CAST(ROUND((SUM(T1.ACTUAL_QTY)/SUM(T1.ITEM_TARGET) *100),2) AS NUMERIC(12,2)) AS PERFORMANCE
        strq = ""
        strq += " SELECT T1.MONTH_CODE,T1.Cust_Code,SUM(T1.ITEM_TARGET) AS TOTAL_TARGET,CAST(SUM(T1.ACTUAL_QTY) AS NUMERIC(13,3)) AS ACTUAL_QTY,"
        strq += " CAST(AVG(T1.PERFORMANCE) AS NUMERIC(10,2)) AS PERFORMANCE,CAST(ROUND(SUM(T1.INCENTIVE),2) AS NUMERIC(13,2)) AS INCENTIVE   FROM ("
        strq += " SELECT T1.MONTH_CODE,T1.CUST_CODE,T1.TARGET_TYPE,T1.Item_Code,T1.ITEM_TARGET,"
        strq += " (CASE WHEN T1.TARGET_TYPE='ITEM' THEN T1.ITEMWISE_QTY WHEN T1.TARGET_TYPE='PACK' THEN T1.PACKWISE_QTY "
        strq += " ELSE T1.FLVAORWISE_QTY END) AS ACTUAL_QTY,"
        strq += " (((CASE WHEN T1.TARGET_TYPE='ITEM' THEN T1.ITEMWISE_QTY WHEN T1.TARGET_TYPE='PACK' THEN T1.PACKWISE_QTY"
        strq += " ELSE T1.FLVAORWISE_QTY END)/T1.ITEM_TARGET)*100) AS PERFORMANCE,"
        strq += " (CASE WHEN (CASE WHEN T1.TARGET_TYPE='ITEM' THEN T1.ITEMWISE_QTY WHEN T1.TARGET_TYPE='PACK' THEN T1.PACKWISE_QTY "
        strq += " ELSE T1.FLVAORWISE_QTY END)>T1.ITEM_TARGET THEN (CASE WHEN T1.TARGET_TYPE='ITEM' THEN T1.ITEMWISE_QTY WHEN T1.TARGET_TYPE='PACK' THEN T1.PACKWISE_QTY "
        strq += " ELSE T1.FLVAORWISE_QTY END)*T1.ITEM_INCENTIVE ELSE 0 END) AS INCENTIVE"
        strq += " FROM ( "
        strq += " SELECT TTT1.MONTH_CODE,TTT1.TARGET_TYPE,TTT1.CUST_CODE,TTT1.Item_Code,TTT1.ITEM_TARGET,TTT1.ITEM_INCENTIVE, "
        strq += " (CASE WHEN TTT1.TARGET_TYPE='ITEM' THEN SUM(TTT1.ACTUAL_QTY) ELSE 0 END) AS ITEMWISE_QTY, "
        strq += " (CASE WHEN TTT1.TARGET_TYPE='PACK' THEN SUM(TTT1.ACTUAL_QTY) ELSE 0 END) AS PACKWISE_QTY, "
        strq += " (CASE WHEN TTT1.TARGET_TYPE='FLVR' THEN SUM(TTT1.ACTUAL_QTY) ELSE SUM(TTT1.ACTUAL_QTY) END) AS FLVAORWISE_QTY FROM ( "
        strq += " SELECT TT1.*,TT2.Class_Code AS PACK,TT3.Class_Code AS FALVOUR FROM ("
        strq += " SELECT T1.MONTH_CODE,T2.TARGET_TYPE,T1.CUST_CODE,T1.ITEM_CODE,T2.ITEM_TARGET,T2.ITEM_INCENTIVE,T1.ACTUAL_QTY FROM ("
        strq += " SELECT  T1.Cust_Code,T1.Cust_Name,T1.MONTH_CODE,T1.Item_Code,SUM(T1.ACTUAL_QTY) AS ACTUAL_QTY  FROM ("
        strq += " SELECT T2.SALE_INVOICE_ID AS SERIAL_NO,T1.SALE_INVOICE_NO,T2.SALE_INVOICE_DATE,DATEPART(MONTH,T2.SALE_INVOICE_DATE) AS MONTH_NO,"
        strq += " DATEPART(YEAR,T2.SALE_INVOICE_DATE) AS YEAR_NO,"
        strq += " (DATENAME(MONTH,T2.SALE_INVOICE_DATE)  + '-' + DATENAME(YEAR,T2.SALE_INVOICE_DATE)) AS MONTH_CODE ,"
        strq += " T2.CUST_CODE,T2.CUST_NAME,T1.SALE_INVOICE_ID AS ITEM_SERIAL_NO,"
        strq += " T1.ITEM_CODE,  T1.ITEM_DESC,T1.INVOICE_QTY,(T1.INVOICE_QTY/T3.CONV_FACTOR) AS ACTUAL_QTY,T1.UNIT_CODE,T3.CONV_FACTOR FROM TSPL_SALE_INVOICE_DETAIL T1 "
        strq += " INNER JOIN TSPL_SALE_INVOICE_HEAD T2 ON T1.SALE_INVOICE_NO=T2.  SALE_INVOICE_NO INNER JOIN TSPL_UNIT_MASTER T3 ON T1.UNIT_CODE=T3.UNIT_CODE) AS T1"
        strq += " GROUP BY T1.Cust_Code,T1.Cust_Name,T1.MONTH_CODE,T1.Item_Code) AS T1"
        strq += " LEFT JOIN "
        strq += " (SELECT T2.CUST_CODE,T2.TARGET_TYPE,(T2.MONTH_NAME + '-' + CAST(T2.YEAR_NO AS VARCHAR(4))) AS MONTH_CODE,T1.ITEM_CODE,T1.ITEM_TARGET,T1.ITEM_INCENTIVE "
        strq += " FROM TSPL_SALES_CUSTOMER_TARGET_DETAIL T1 INNER JOIN TSPL_SALES_CUSTOMER_TARGET T2 ON T1.TARGET_CODE=T2.TARGET_CODE) AS T2 ON T1.Cust_Code=T2.CUST_CODE"
        strq += " AND T1.Item_Code=T2.ITEM_CODE) AS TT1 LEFT JOIN (SELECT * FROM TSPL_ITEM_DETAILS WHERE Class_Name='PACK') AS TT2 ON TT1.ITEM_CODE=TT2.Item_Code"
        strq += " LEFT JOIN (SELECT * FROM TSPL_ITEM_DETAILS WHERE Class_Name='FLAVOUR') AS TT3 ON TT1.ITEM_CODE=TT3.Item_Code) AS TTT1 "
        strq += " GROUP BY TTT1.MONTH_CODE,TTT1.TARGET_TYPE,TTT1.CUST_CODE,TTT1.Item_Code,TTT1.ITEM_TARGET,TTT1.ITEM_INCENTIVE) AS T1 "
        strq += " ) AS T1 " & cond & " GROUP BY T1.MONTH_CODE,T1.Cust_Code "

        DT = clsDBFuncationality.GetDataTable(strq)
        Return DT




    End Function
    Public Shared Function GetCustomerTargetDTItemWise(ByVal month As Integer, ByVal year As Integer, ByVal all As Boolean) As DataTable
        Dim DT As DataTable
        Dim strq As String
        Dim cond As String
        If all = True Then
            cond = ""
        Else
            cond = " where TTT1.month_code='" & MonthName(month) & "-" & year & "'"
        End If
        strq = ""

        strq += " SELECT T1.MONTH_CODE,T1.CUST_CODE,T1.TARGET_TYPE,T1.Item_Code,T1.ITEM_TARGET,"
        strq += " CAST((CASE WHEN T1.TARGET_TYPE='ITEM' THEN T1.ITEMWISE_QTY WHEN T1.TARGET_TYPE='PACK' THEN T1.PACKWISE_QTY  "
        strq += " ELSE T1.FLVAORWISE_QTY END)AS NUMERIC(13,3)) AS ACTUAL_QTY,"
        strq += " CAST(ROUND((((CASE WHEN T1.TARGET_TYPE='ITEM' THEN T1.ITEMWISE_QTY WHEN T1.TARGET_TYPE='PACK' THEN T1.PACKWISE_QTY"
        strq += " ELSE T1.FLVAORWISE_QTY END)/T1.ITEM_TARGET)*100),2) AS NUMERIC(12,2)) AS PERFORMANCE,"
        strq += " CAST((CASE WHEN (CASE WHEN T1.TARGET_TYPE='ITEM' THEN T1.ITEMWISE_QTY WHEN T1.TARGET_TYPE='PACK' THEN T1.PACKWISE_QTY "
        strq += " ELSE T1.FLVAORWISE_QTY END)>T1.ITEM_TARGET THEN (CASE WHEN T1.TARGET_TYPE='ITEM' THEN T1.ITEMWISE_QTY WHEN T1.TARGET_TYPE='PACK' THEN T1.PACKWISE_QTY "
        strq += " ELSE T1.FLVAORWISE_QTY END)*T1.ITEM_INCENTIVE ELSE 0 END) AS NUMERIC(13,2)) AS INCENTIVE"
        strq += " FROM ( "
        strq += " SELECT TTT1.MONTH_CODE,TTT1.TARGET_TYPE,TTT1.CUST_CODE,TTT1.Item_Code,TTT1.ITEM_TARGET,TTT1.ITEM_INCENTIVE, "
        strq += " (CASE WHEN TTT1.TARGET_TYPE='ITEM' THEN SUM(TTT1.ACTUAL_QTY) ELSE 0 END) AS ITEMWISE_QTY, "
        strq += " (CASE WHEN TTT1.TARGET_TYPE='PACK' THEN SUM(TTT1.ACTUAL_QTY) ELSE 0 END) AS PACKWISE_QTY, "
        strq += " (CASE WHEN TTT1.TARGET_TYPE='FLVR' THEN SUM(TTT1.ACTUAL_QTY) ELSE SUM(TTT1.ACTUAL_QTY) END) AS FLVAORWISE_QTY FROM ( "
        strq += " SELECT TT1.*,TT2.Class_Code AS PACK,TT3.Class_Code AS FALVOUR FROM ("
        strq += " SELECT T1.MONTH_CODE,T2.TARGET_TYPE,T1.CUST_CODE,T1.ITEM_CODE,T2.ITEM_TARGET,T2.ITEM_INCENTIVE,T1.ACTUAL_QTY FROM ("
        strq += " SELECT  T1.Cust_Code,T1.Cust_Name,T1.MONTH_CODE,T1.Item_Code,SUM(T1.ACTUAL_QTY) AS ACTUAL_QTY  FROM ("
        strq += " SELECT T2.SALE_INVOICE_ID AS SERIAL_NO,T1.SALE_INVOICE_NO,T2.SALE_INVOICE_DATE,DATEPART(MONTH,T2.SALE_INVOICE_DATE) AS MONTH_NO,"
        strq += " DATEPART(YEAR,T2.SALE_INVOICE_DATE) AS YEAR_NO,"
        strq += " (DATENAME(MONTH,T2.SALE_INVOICE_DATE)  + '-' + DATENAME(YEAR,T2.SALE_INVOICE_DATE)) AS MONTH_CODE ,"
        strq += " T2.CUST_CODE,T2.CUST_NAME,T1.SALE_INVOICE_ID AS ITEM_SERIAL_NO,"
        strq += " T1.ITEM_CODE,  T1.ITEM_DESC,T1.INVOICE_QTY,(T1.INVOICE_QTY/T3.CONV_FACTOR) AS ACTUAL_QTY,T1.UNIT_CODE,T3.CONV_FACTOR FROM TSPL_SALE_INVOICE_DETAIL T1 "
        strq += " INNER JOIN TSPL_SALE_INVOICE_HEAD T2 ON T1.SALE_INVOICE_NO=T2.  SALE_INVOICE_NO INNER JOIN TSPL_UNIT_MASTER T3 ON T1.UNIT_CODE=T3.UNIT_CODE) AS T1"
        strq += " GROUP BY T1.Cust_Code,T1.Cust_Name,T1.MONTH_CODE,T1.Item_Code) AS T1"
        strq += " LEFT JOIN "
        strq += " (SELECT T2.CUST_CODE,T2.TARGET_TYPE,(T2.MONTH_NAME + '-' + CAST(T2.YEAR_NO AS VARCHAR(4))) AS MONTH_CODE,T1.ITEM_CODE,T1.ITEM_TARGET,T1.ITEM_INCENTIVE "
        strq += " FROM TSPL_SALES_CUSTOMER_TARGET_DETAIL T1 INNER JOIN TSPL_SALES_CUSTOMER_TARGET T2 ON T1.TARGET_CODE=T2.TARGET_CODE) AS T2 ON T1.Cust_Code=T2.CUST_CODE"
        strq += " AND T1.Item_Code=T2.ITEM_CODE) AS TT1 LEFT JOIN (SELECT * FROM TSPL_ITEM_DETAILS WHERE Class_Name='PACK') AS TT2 ON TT1.ITEM_CODE=TT2.Item_Code"
        strq += " LEFT JOIN (SELECT * FROM TSPL_ITEM_DETAILS WHERE Class_Name='FLAVOUR') AS TT3 ON TT1.ITEM_CODE=TT3.Item_Code) AS TTT1 " & cond & ""
        strq += " GROUP BY TTT1.MONTH_CODE,TTT1.TARGET_TYPE,TTT1.CUST_CODE,TTT1.Item_Code,TTT1.ITEM_TARGET,TTT1.ITEM_INCENTIVE) AS T1 "


        DT = clsDBFuncationality.GetDataTable(strq)
        Return DT

    End Function

End Class
Public Class clsCustomerTargetDetails
#Region "Variables"

    'Public Shared ObjList As List(Of clsAdjustmentVoucher)

#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsCustomerTargetFixing), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCustomerTargetFixing In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "TARGET_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "ITEM_CODE", obj.ITEM_CODE)
                clsCommon.AddColumnsForChange(coll, "ITEM_TARGET", obj.ITEM_TARGET)
                clsCommon.AddColumnsForChange(coll, "ITEM_INCENTIVE", obj.ITEM_INCENTIVE)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALES_CUSTOMER_TARGET_DETAIL", OMInsertOrUpdate.Insert, "TSPL_SALES_CUSTOMER_TARGET_DETAIL.TARGET_CODE='" + strDocNo + "'", trans)
            Next

        End If

        Return True
    End Function
End Class
