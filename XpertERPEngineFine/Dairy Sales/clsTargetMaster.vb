Imports common
Imports System.Data.SqlClient
Public Class clsTargetMaster

#Region "Variables"
    Public Cust_Code As String = Nothing
    Public Cust_Group_Code As String = Nothing
    Public Discount_Type As String = Nothing
    Public Month_Year As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public UOM As String = Nothing
    Public Quantity As Double = Nothing
    Public Balance_Qty As Double = Nothing
    Public Bal_Qty_InBtl As Double = Nothing
    Public Post As Integer = Nothing
    Public Amount As Double = Nothing
    Public NewAmount As Double = Nothing
    Public Bal_Amount As Double = Nothing
#End Region

    Public Shared Function SaveData(ByVal strACode As String, ByVal strDisType As String, ByVal StrAGroupCode As String, ByVal Month_Year As String, ByVal Obj As clsTargetMaster) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (SaveData(strACode, strDisType, StrAGroupCode, Month_Year, trans, Obj)) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal strACode As String, ByVal strDisType As String, ByVal StrAGroupCode As String, ByVal Month_Year As String, ByVal trans As SqlTransaction, ByVal obj As clsTargetMaster) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim Amt As Double
            Dim NewAmt As Double
            Dim BalAmt As Double
            Dim QryFind As String = "Select Amount, Bal_Amount from TSPL_TARGET_MASTER where Cust_code='" + strACode + "' AND Discount_Type='" + strDisType + "' AND  DatePart(Month, Month_Year) = DatePart(Month, '" + clsCommon.GetPrintDate(Month_Year, "dd/MMM/yyy") + "') And DatePart(Year, Month_Year) = DatePart(Year, '" + clsCommon.GetPrintDate(Month_Year, "dd/MMM/yyy") + "')  "
            Dim dtFind As DataTable = clsDBFuncationality.GetDataTable(QryFind, trans)
            If dtFind.Rows.Count <= 0 Then
                Amt = clsCommon.myCdbl(obj.Amount)
                NewAmt = clsCommon.myCdbl(obj.NewAmount)
                BalAmt = clsCommon.myCdbl(obj.Amount)
            Else
                Amt = clsCommon.myCdbl(dtFind.Rows(0)("Amount")) + clsCommon.myCdbl(obj.Amount)
                NewAmt = clsCommon.myCdbl(obj.NewAmount)
                BalAmt = clsCommon.myCdbl(dtFind.Rows(0)("Bal_Amount")) + clsCommon.myCdbl(obj.Amount)
            End If

            Dim qry As String = "delete from TSPL_TARGET_MASTER where Cust_code='" + strACode + "' AND Discount_Type='" + strDisType + "' AND  DatePart(Month, Month_Year) = DatePart(Month, '" + clsCommon.GetPrintDate(Month_Year, "dd/MMM/yyy") + "') And DatePart(Year, Month_Year) = DatePart(Year, '" + clsCommon.GetPrintDate(Month_Year, "dd/MMM/yyy") + "')   "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            'For Each obj As clsTargetMaster In Arr
            'Dim Obj As New clsTargetMaster
            Dim coll As New Hashtable()
            'clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            'clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
            'clsCommon.AddColumnsForChange(coll, "Quantity", obj.Quantity)
            'clsCommon.AddColumnsForChange(coll, "Balance_Qty", obj.Balance_Qty)
            'clsCommon.AddColumnsForChange(coll, "Bal_Qty_InBottle", obj.Bal_Qty_InBtl)
            If NewAmt > 0 Then
                clsCommon.AddColumnsForChange(coll, "Amount", NewAmt)
                clsCommon.AddColumnsForChange(coll, "Bal_Amount", NewAmt)
            Else
                clsCommon.AddColumnsForChange(coll, "Amount", Amt)
                clsCommon.AddColumnsForChange(coll, "Bal_Amount", BalAmt)
            End If
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Cust_Code", strACode)
            clsCommon.AddColumnsForChange(coll, "Cust_Group_Code", StrAGroupCode)
            clsCommon.AddColumnsForChange(coll, "Discount_Type", strDisType)
            clsCommon.AddColumnsForChange(coll, "Month_Year", clsCommon.GetPrintDate(Month_Year, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", (clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")).ToString())
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TARGET_MASTER", OMInsertOrUpdate.Insert, "", trans)
            'Next
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal Cust_Code As String, ByVal Discount_Type As String, ByVal Month_Year As String) As List(Of clsTargetMaster)
        Dim obj As clsTargetMaster = Nothing
        Dim Arr As New List(Of clsTargetMaster)
        'Dim qry As String = "select TSPL_ITEM_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc , UOM, Quantity, Balance_Qty, Bal_Qty_InBottle, Post from TSPL_TARGET_MASTER LEFT OUTER JOIN TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_TARGET_MASTER.Item_Code Where Cust_Code='" + Cust_Code + "' AND Discount_Type='" + Discount_Type + "' AND  DatePart(Month, Month_Year) = DatePart(Month, '" + clsCommon.GetPrintDate(Month_Year, "dd/MMM/yyy") + "') And DatePart(Year, Month_Year) = DatePart(Year, '" + clsCommon.GetPrintDate(Month_Year, "dd/MMM/yyy") + "')"
        Dim qry As String = "select Amount, Bal_Amount from TSPL_TARGET_MASTER  Where Cust_Code='" + Cust_Code + "' AND Discount_Type='" + Discount_Type + "' AND  DatePart(Month, Month_Year) = DatePart(Month, '" + clsCommon.GetPrintDate(Month_Year, "dd/MMM/yyy") + "') And DatePart(Year, Month_Year) = DatePart(Year, '" + clsCommon.GetPrintDate(Month_Year, "dd/MMM/yyy") + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count <= 0 Then
        Else
            'Dim Arr As New List(Of clsTargetMaster)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Dim objTr As clsTargetMaster
                For Each dr As DataRow In dt.Rows
                    objTr = New clsTargetMaster()
                    'objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    'objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    'objTr.UOM = clsCommon.myCstr(dr("UOM"))
                    'objTr.Quantity = clsCommon.myCstr(dr("Quantity"))
                    'objTr.Balance_Qty = clsCommon.myCstr(dr("Balance_Qty"))
                    'objTr.Bal_Qty_InBtl = clsCommon.myCstr(dr("Bal_Qty_InBottle"))
                    'objTr.Post = dr("Post")
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Bal_Amount = clsCommon.myCdbl(dr("Bal_Amount"))
                    'objTr.Post = dr("Post")
                    Arr.Add(objTr)
                Next
            End If
        End If
        Return Arr
    End Function

    Public Shared Function PostData(ByVal Cust_Code As String, ByVal Discount_Type As String, ByVal Month_Year As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(Cust_Code) <= 0) Then
                Throw New Exception("Customer Code Not Found TO Post")
            ElseIf (clsCommon.myLen(Discount_Type) <= 0) Then
                Throw New Exception("Discount Type Not Found TO Post")
            ElseIf (clsCommon.myLen(Month_Year) <= 0) Then
                Throw New Exception("Month & Year Not Found TO Post")
            End If

            Dim qry As String = " Update TSPL_TARGET_MASTER set Post=1 where Cust_Code='" + Cust_Code + "' AND Discount_Type='" + Discount_Type + "' AND DatePart(Month, Month_Year) = DatePart(Month, '" + clsCommon.GetPrintDate(Month_Year, "dd/MMM/yyy") + "') And DatePart(Year, Month_Year) = DatePart(Year, '" + clsCommon.GetPrintDate(Month_Year, "dd/MMM/yyy") + "') "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal Cust_Code As String, ByVal Discount_Type As String, ByVal Month_Year As String) As Boolean
        Dim isSaved As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            'If (clsCommon.myLen(Cust_Code) <= 0) Then
            '    Throw New Exception("Customer Code No not found to Delete")
            'ElseIf (clsCommon.myLen(Discount_Type) <= 0) Then
            '    Throw New Exception("Discount Code No not found to Delete")
            'ElseIf (clsCommon.myLen(Month_Year) <= 0) Then
            '    Throw New Exception("Month-Year No not found to Delete")
            'End If

            Dim qry As String = "Delete from TSPL_TARGET_MASTER where Cust_code='" + Cust_Code + "' AND Discount_Type='" + Discount_Type + "' And DatePart(Month, Month_Year) = DatePart(Month, '" + clsCommon.GetPrintDate(Month_Year, "dd/MMM/yyy") + "') And DatePart(Year, Month_Year) = DatePart(Year, '" + clsCommon.GetPrintDate(Month_Year, "dd/MMM/yyy") + "')  "
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
        Return isSaved
    End Function

    Public Shared Function GetBalance(ByVal strCurrCode As String, ByVal strACode As String, ByVal dtDate As Date) As DataTable
        Dim dtTemp As Date = New Date(dtDate.Year, dtDate.Month, 1)
        dtTemp = dtTemp.AddMonths(1)
        dtTemp = dtTemp.AddDays(-1)

        Dim qry As String = "select Discount_Type,SUM(Amount * case when RI=1 then 1 else -1 end) as Amount from( "
        qry += " select Discount_Type,Bal_Amount as Amount,1 as RI from TSPL_TARGET_MASTER "
        qry += " where Cust_Code='" + strACode + "'  and  Month_Year <= '" + clsCommon.GetPrintDate(dtTemp, "dd/MMM/yyyy") + "' "
        qry += " union all"
        qry += " select TSPL_SHIPMENT_DETAILS.Discount_Code as Discount_Type,TSPL_SHIPMENT_DETAILS.Target_Discount_Amt   as Amount,2 as RI from TSPL_SHIPMENT_DETAILS"
        qry += " left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SHIPMENT_DETAILS.Shipment_No"
        qry += " where TSPL_SHIPMENT_MASTER.Cust_Code='" + strACode + "' and  TSPL_SHIPMENT_MASTER.Date_Time_Removal<='" + clsCommon.GetPrintDate(dtTemp, "dd/MMM/yyyy") + "' and TSPL_SHIPMENT_MASTER.Is_Post='N' and TSPL_SHIPMENT_MASTER.Shipment_No not in ('" + strCurrCode + "') "
        qry += " and LEN(isnull(TSPL_SHIPMENT_DETAILS.Discount_Code,''))>0 "
        qry += " )xxx group by Discount_Type"
        Return clsDBFuncationality.GetDataTable(qry)
    End Function
End Class
