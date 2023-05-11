Imports common
Imports System.Data.SqlClient
Public Class clsfrmPriceChartMaster

#Region "Variables"
    Public code As String = Nothing
    Public desc As String = Nothing
    Public effct_dt As Date = Nothing
    Public inactv_dt As Date = Nothing
    Public ratio As String = Nothing
    Public fat_pers As Decimal = Nothing
    Public snf_pers As Decimal = Nothing
    Public CLR As Decimal = Nothing
    Public rate As Decimal = Nothing
    Public ratetype As String = Nothing
    Public effctv_rate As Decimal = Nothing
    Public declrd_rate As Decimal = Nothing
    Public axis As String = Nothing
    Public matrix As String = Nothing
    Public snf_ratio As String = Nothing
    Public price_type As String = Nothing
    Public Posted As Decimal = Nothing
    Public arrSNFDed As List(Of clsPriceChartSNFDed)
#End Region

    Public Shared Function HistoryData(ByVal strCode As String, ByVal PriceType As String) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "insert into TSPL_MILK_PRICE_MASTER_HISTORY select * from TSPL_MILK_PRICE_MASTER where price_code='" + strCode + "' and price_type='" + PriceType + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)

            'trans.Commit()
            Return True
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal strCode As String, ByVal isNewEntry As Boolean, ByVal obj As clsfrmPriceChartMaster) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(strCode, isNewEntry, obj, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal isNewEntry As Boolean, ByVal obj As clsfrmPriceChartMaster, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim check As Integer = 0

            If isNewEntry Then
                obj.code = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.MILKPRCMASTER, "", "")
            End If

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "price_code", obj.code)
            clsCommon.AddColumnsForChange(coll, "Description", obj.desc)
            clsCommon.AddColumnsForChange(coll, "Effective_Date", clsCommon.GetPrintDate(obj.effct_dt, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Inactive_Date", clsCommon.GetPrintDate(obj.inactv_dt, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Ratio", obj.ratio)
            clsCommon.AddColumnsForChange(coll, "FAT_Pers", obj.fat_pers)
            clsCommon.AddColumnsForChange(coll, "SNF_Pers", obj.snf_pers)
            clsCommon.AddColumnsForChange(coll, "CLR", obj.CLR)
            clsCommon.AddColumnsForChange(coll, "Milk_Rate", obj.rate)
            clsCommon.AddColumnsForChange(coll, "Rate_Type", obj.ratetype)
            clsCommon.AddColumnsForChange(coll, "Declared_Rate", obj.declrd_rate)
            clsCommon.AddColumnsForChange(coll, "Effective_Rate", obj.effctv_rate)
            clsCommon.AddColumnsForChange(coll, "axis_type", obj.axis)
            clsCommon.AddColumnsForChange(coll, "matrix_type", obj.matrix)
            clsCommon.AddColumnsForChange(coll, "SNF_Ratio", obj.snf_ratio)
            clsCommon.AddColumnsForChange(coll, "Price_Type", obj.price_type)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PRICE_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PRICE_MASTER", OMInsertOrUpdate.Update, " TSPL_MILK_PRICE_MASTER.price_code='" + obj.code + "' and TSPL_MILK_PRICE_MASTER.price_type='" + obj.price_type + "'", trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.code, "TSPL_MILK_PRICE_MASTER", "price_code", trans)
            End If
            clsPriceChartSNFDed.SaveData(obj.code, obj.arrSNFDed, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Milk Price No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim qry As String = "Update TSPL_MILK_PRICE_MASTER set Posted=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Price_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal PriceType As String) As clsfrmPriceChartMaster
        Try
            Dim obj As clsfrmPriceChartMaster = Nothing

            Dim qry As String = "select * from tspl_milk_price_master"

            Select Case NavType
                Case NavigatorType.Current
                    qry += " where price_code='" + strCode + "' and price_type='" + PriceType + "'"
                Case NavigatorType.First
                    qry += " where price_code in (select min(price_code) from tspl_milk_price_master where price_type='" + PriceType + "')"
                Case NavigatorType.Last
                    qry += " where price_code in (select max(price_code) from tspl_milk_price_master where price_type='" + PriceType + "')"
                Case NavigatorType.Next
                    qry += " where price_code in (select min(price_code) from tspl_milk_price_master where price_code>'" + strCode + "' and price_type='" + PriceType + "')"
                Case NavigatorType.Previous
                    qry += " where price_code in (select max(price_code) from tspl_milk_price_master where price_code<'" + strCode + "' and price_type='" + PriceType + "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsfrmPriceChartMaster()

                obj.code = clsCommon.myCstr(dt.Rows(0)("price_code"))
                obj.desc = clsCommon.myCstr(dt.Rows(0)("description"))
                obj.effct_dt = clsCommon.myCDate(dt.Rows(0)("Effective_Date"))
                obj.inactv_dt = clsCommon.myCDate(dt.Rows(0)("Inactive_Date"))
                obj.ratio = clsCommon.myCstr(dt.Rows(0)("ratio"))
                obj.fat_pers = clsCommon.myCdbl(dt.Rows(0)("fat_pers"))
                obj.snf_pers = clsCommon.myCdbl(dt.Rows(0)("snf_pers"))
                obj.CLR = clsCommon.myCdbl(dt.Rows(0)("CLR"))
                obj.rate = clsCommon.myCdbl(dt.Rows(0)("milk_rate"))
                obj.ratetype = clsCommon.myCstr(dt.Rows(0)("Rate_Type"))
                obj.declrd_rate = clsCommon.myCdbl(dt.Rows(0)("Declared_Rate"))
                obj.effctv_rate = clsCommon.myCdbl(dt.Rows(0)("Effective_Rate"))
                obj.axis = clsCommon.myCstr(dt.Rows(0)("axis_type"))
                obj.matrix = clsCommon.myCstr(dt.Rows(0)("matrix_type"))
                obj.snf_ratio = clsCommon.myCstr(dt.Rows(0)("SNF_Ratio"))
                obj.price_type = clsCommon.myCstr(dt.Rows(0)("Price_Type"))
                obj.Posted = clsCommon.myCstr(dt.Rows(0)("Posted"))
                obj.arrSNFDed = clsPriceChartSNFDed.GetData(obj.code)
            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class


Public Class clsPriceChartSNFDed
#Region "variables"
    Public Price_Code As String = Nothing
    Public Per As Double
    Public Amount As Double
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsPriceChartSNFDed), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "Delete from TSPL_MILK_PRICE_SNF_DEDUCTION where Price_Code='" + strCode + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsPriceChartSNFDed In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Price_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Per", obj.Per)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PRICE_SNF_DEDUCTION", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String) As List(Of clsPriceChartSNFDed)
        Dim arr As New List(Of clsPriceChartSNFDed)
        Dim qry As String = "Select TSPL_MILK_PRICE_SNF_DEDUCTION.* from TSPL_MILK_PRICE_SNF_DEDUCTION Where Price_Code='" + strCode + "' order by Per "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsPriceChartSNFDed()
                obj.Price_Code = clsCommon.myCstr(dr("Price_Code"))
                obj.Per = clsCommon.myCdbl(dr("Per"))
                obj.Amount = clsCommon.myCdbl(dr("Amount"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class
