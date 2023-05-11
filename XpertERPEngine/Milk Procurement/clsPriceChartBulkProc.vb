
Imports System.Data.SqlClient
Imports common


Public Class clsPriceChartBulkProc
#Region "variables"
    Public effective_Date As Date?
    Public Milk_Type_Code As String = Nothing
    Public Price_Code As String = Nothing
    Public Price_Date As Date?
    Public Fat_Weightage As Double = 0
    Public Snf_Weightage As Double = 0
    Public Tolerance As Double = 0
    Public Fat_Percentage As Double = 0
    Public IsDefaultForTankerDispatch As Integer = 0
    Public Snf_Percentage As Double = 0
    Public Standard_Rate As Double = 0
    Public vendor_code As String = String.Empty
    Public vendor_desc As String = String.Empty
    Public Arr As List(Of clspriceCodeBulkProcDetail) = Nothing
    Public ArrItemWise As List(Of clspriceCodeBulkProcDetailItemWise) = Nothing
    Public Posted As Decimal = Nothing
    Public IsPrice_GradeWise As Integer = 0
    Public IsPrice_ItemWise As Integer = 0
    Public ExpiryDate As Date?
    Public Total_Solid_Rate As Decimal = 0
    Public Total_Solid_Unit_Code As String
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        ''updation by richa agarwal 16/10/2014
        'Dim qry As String = " select TSPL_Bulk_Price_MASTER.Price_Code as [Code] ,TSPL_Bulk_Price_MASTER.Price_Date as [Price Date] ,TSPL_Bulk_Price_MASTER.Fat_Weightage as [FAT Weightage] ,TSPL_Bulk_Price_MASTER.Snf_Weightage as [SNF Weightage] ,TSPL_Bulk_Price_MASTER.Fat_Percentage as [FAT Percentage] ,TSPL_Bulk_Price_MASTER.Snf_Percentage as [SNF Percentage] ,TSPL_Bulk_Price_MASTER.Standard_Rate as [Standard Rate] ,TSPL_Bulk_Price_MASTER.Vendor_Code as [Vendor Code] ,TSPL_Bulk_Price_MASTER.Vendor_Desc as [Vendor Desc] ,TSPL_Bulk_Price_MASTER.Comp_Code as [Company Code] ,TSPL_Bulk_Price_MASTER.Created_By as [Created By] ,TSPL_Bulk_Price_MASTER.Created_Date as [Created Date] ,TSPL_Bulk_Price_MASTER.Modified_By as [Modified By] ,TSPL_Bulk_Price_MASTER.Modified_Date as [Modified Date]  From TSPL_Bulk_Price_MASTER  "
        Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, Nothing))
        Dim qry As String
        If TankerFromMaster = 0 Then ''ERO/29/03/19-000524 by balwinder on 05/04/2019 
            qry = " Select TSPL_Bulk_Price_MASTER.Price_Code as Code,convert(varchar,TSPL_Bulk_Price_MASTER.Price_Date,103) as [Price Date],TSPL_Bulk_Price_MASTER.Fat_Weightage as [Fat Weightage],TSPL_Bulk_Price_MASTER.Snf_Weightage as [SNF Weightage],TSPL_Bulk_Price_MASTER.Fat_Percentage as [Fat Ratio],TSPL_Bulk_Price_MASTER.Snf_Percentage as [SNF Ratio],TSPL_Bulk_Price_MASTER.Standard_Rate as [Standard Rate],TSPL_Bulk_Price_MASTER.Total_Solid_Rate as [Total Solid Rate],TSPL_Bulk_Price_MASTER.Total_Solid_Unit_Code as [Unit Code]  from TSPL_Bulk_Price_MASTER "
            str = clsCommon.ShowSelectForm("BulkriceChartS", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Else
            qry = "select distinct TSPL_Bulk_Price_MASTER.Price_Code as Code,convert(varchar,TSPL_Bulk_Price_MASTER.Price_Date,103) as [Price Date],TSPL_BULK_PRICE_DETAIL.Fat_Weightage as [Fat Weightage],TSPL_BULK_PRICE_DETAIL.Snf_Weightage as [SNF Weightage],TSPL_BULK_PRICE_DETAIL.Fat_Percentage as [Fat Ratio],TSPL_BULK_PRICE_DETAIL.Snf_Percentage as [SNF Ratio],TSPL_BULK_PRICE_DETAIL.Standard_Rate as [Standard Rate] ,IsPrice_GradeWise as [Is Price Grade wise],Milk_Grade_code as [Grade]  from TSPL_Bulk_Price_MASTER left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code "
            str = clsCommon.ShowSelectForm("BulkriceChart", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        End If
        ''========================================

        Return str
    End Function



    Public Shared Function SaveData(ByVal obj As clsPriceChartBulkProc, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""

        Try
            qry = "Delete from TSPL_BULK_PRICE_DETAIL_ITEM_WISE_SNF_DEDUCTION where Price_Code='" + obj.Price_Code + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If isNewEntry Then
                obj.Price_Code = clsERPFuncationality.GetNextCode(trans, obj.Price_Date, clsDocType.PriceChartMasterBulk, "", "")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "ExpiryDate", clsCommon.GetPrintDate(obj.ExpiryDate, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "effective_Date", clsCommon.GetPrintDate(obj.effective_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Milk_Type_Code", obj.Milk_Type_Code, True)
            clsCommon.AddColumnsForChange(coll, "vendor_code", obj.vendor_code)
            clsCommon.AddColumnsForChange(coll, "vendor_desc", obj.vendor_desc)
            clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Fat_Weightage", obj.Fat_Weightage)
            clsCommon.AddColumnsForChange(coll, "Snf_Weightage", obj.Snf_Weightage)
            clsCommon.AddColumnsForChange(coll, "Fat_Percentage", obj.Fat_Percentage)
            clsCommon.AddColumnsForChange(coll, "Snf_Percentage", obj.Snf_Percentage)
            clsCommon.AddColumnsForChange(coll, "Tolerance", obj.Tolerance)
            clsCommon.AddColumnsForChange(coll, "Standard_Rate", obj.Standard_Rate)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "IsPrice_GradeWise", obj.IsPrice_GradeWise)
            clsCommon.AddColumnsForChange(coll, "IsDefaultForTankerDispatch", clsCommon.myCdbl(obj.IsDefaultForTankerDispatch))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "IsPrice_ItemWise", obj.IsPrice_ItemWise)
            clsCommon.AddColumnsForChange(coll, "Total_Solid_Rate", obj.Total_Solid_Rate)
            clsCommon.AddColumnsForChange(coll, "Total_Solid_Unit_Code", obj.Total_Solid_Unit_Code)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bulk_Price_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bulk_Price_MASTER", OMInsertOrUpdate.Update, "TSPL_Bulk_Price_MASTER.Price_Code='" + obj.Price_Code + "'", trans)
            End If
            If obj.IsDefaultForTankerDispatch = 1 Then
                clsDBFuncationality.ExecuteNonQuery("update TSPL_Bulk_Price_MASTER set IsDefaultForTankerDispatch=0 where  Price_Code<>'" & obj.Price_Code.ToUpper() & "'", trans)
            End If
            If obj.IsPrice_ItemWise = 1 Then
                clspriceCodeBulkProcDetailItemWise.SaveData(obj.Price_Code, obj.ArrItemWise, trans)
            Else
                clspriceCodeBulkProcDetail.SaveData(obj.Price_Code, obj.Arr, trans)
            End If


            trans.Commit()
        Catch err As Exception

            Throw New Exception(err.Message)
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
                Throw New Exception("Bulk Price No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim qry As String = "Update TSPL_Bulk_Price_MASTER set Posted=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Price_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsPriceChartBulkProc
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsPriceChartBulkProc
        Dim obj As clsPriceChartBulkProc = Nothing
        Dim Arr As List(Of clsPriceChartBulkProc) = Nothing
        Dim qry As String = "select Total_Solid_Unit_Code, Total_Solid_Rate,ExpiryDate,Tolerance,Price_Code ,Price_Date,Snf_Weightage,Snf_Percentage,Fat_Weightage,Fat_Percentage,Standard_Rate,vendor_code,vendor_desc,IsDefaultForTankerDispatch,effective_Date,Milk_Type_Code,Posted,IsPrice_GradeWise,IsPrice_ItemWise from TSPL_Bulk_Price_MASTER where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Bulk_Price_MASTER.Price_Code = (select MIN(Price_Code) from TSPL_Bulk_Price_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_Bulk_Price_MASTER.Price_Code = (select Max(Price_Code) from TSPL_Bulk_Price_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_Bulk_Price_MASTER.Price_Code = (select top 1 Price_Code from TSPL_Bulk_Price_MASTER WHERE 1=1 " + whrclas + " and Price_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_Bulk_Price_MASTER.Price_Code = (select Min(Price_Code) from TSPL_Bulk_Price_MASTER where Price_Code>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_Bulk_Price_MASTER.Price_Code = (select Max(Price_Code) from TSPL_Bulk_Price_MASTER where Price_Code<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPriceChartBulkProc
            If clsCommon.myLen(dt.Rows(0)("ExpiryDate")) > 0 Then
                obj.ExpiryDate = clsCommon.myCstr(dt.Rows(0)("ExpiryDate"))
            End If
            If clsCommon.myLen(dt.Rows(0)("effective_Date")) > 0 Then
                obj.effective_Date = clsCommon.myCstr(dt.Rows(0)("effective_Date"))
            End If
          
            obj.Milk_Type_Code = clsCommon.myCstr(dt.Rows(0)("Milk_Type_Code"))
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.Price_Date = clsCommon.myCstr(dt.Rows(0)("Price_Date"))
            obj.Snf_Weightage = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
            obj.IsDefaultForTankerDispatch = clsCommon.myCdbl(dt.Rows(0)("IsDefaultForTankerDispatch"))
            obj.Snf_Percentage = clsCommon.myCdbl(dt.Rows(0)("Snf_Percentage"))
            obj.Fat_Weightage = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
            obj.Fat_Percentage = clsCommon.myCdbl(dt.Rows(0)("Fat_Percentage"))
            obj.Standard_Rate = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
            obj.Tolerance = clsCommon.myCdbl(dt.Rows(0)("Tolerance"))
            obj.vendor_code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.vendor_desc = clsCommon.myCstr(dt.Rows(0)("Vendor_desc"))
            obj.Posted = clsCommon.myCstr(dt.Rows(0)("Posted"))
            obj.IsPrice_GradeWise = clsCommon.myCdbl(dt.Rows(0)("IsPrice_GradeWise"))
            obj.IsPrice_ItemWise = clsCommon.myCdbl(dt.Rows(0)("IsPrice_ItemWise"))
            obj.Total_Solid_Rate = clsCommon.myCdbl(dt.Rows(0)("Total_Solid_Rate"))
            obj.Total_Solid_Unit_Code = clsCommon.myCstr(dt.Rows(0)("Total_Solid_Unit_Code"))
            If obj.IsPrice_GradeWise = 1 AndAlso obj.IsPrice_ItemWise = 0 Then
                obj.Arr = clspriceCodeBulkProcDetail.GetData(obj.Price_Code, Nothing)
            ElseIf obj.IsPrice_GradeWise = 0 AndAlso obj.IsPrice_ItemWise = 1 Then
                obj.ArrItemWise = clspriceCodeBulkProcDetailItemWise.GetData(obj.Price_Code, Nothing)
            End If

            'IsDefaultForTankerDispatch
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try
            Dim qry As String = ""
            qry = "delete from tspl_bulk_price_detail_item_wise where Price_Code='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
            qry = "delete from TSPL_Bulk_Price_MASTER where Price_Code='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
End Class
Public Class clspriceCodeBulkProcDetail
#Region "Variables"
    Public Price_Code As String = Nothing
    Public Milk_Grade_code As String = Nothing
    Public Line_No As Integer = 0
    Public Fat_Weightage As Double = 0
    Public Snf_Weightage As Double = 0
    Public Fat_Percentage As Double = 0
    Public Snf_Percentage As Double = 0
    Public Standard_Rate As Double = 0
    Public Tolerance As Double = 0

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clspriceCodeBulkProcDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim sQuery As String = "Delete from tspl_bulk_price_detail where Price_Code='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            For Each obj As clspriceCodeBulkProcDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Price_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Milk_Grade_code", obj.Milk_Grade_code, True)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Fat_Weightage", obj.Fat_Weightage)
                clsCommon.AddColumnsForChange(coll, "Snf_Weightage", obj.Snf_Weightage)
                clsCommon.AddColumnsForChange(coll, "Fat_Percentage", obj.Fat_Percentage)
                clsCommon.AddColumnsForChange(coll, "Snf_Percentage", obj.Snf_Percentage)
                clsCommon.AddColumnsForChange(coll, "Standard_Rate", obj.Standard_Rate)
                clsCommon.AddColumnsForChange(coll, "Tolerance", obj.Tolerance)
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_bulk_price_detail", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clspriceCodeBulkProcDetail)
        Dim arr As List(Of clspriceCodeBulkProcDetail) = Nothing
        Dim qry As String
        qry = "select tspl_bulk_price_detail.Line_No,tspl_bulk_price_detail.Milk_Grade_code,tspl_bulk_price_detail.Fat_Weightage,tspl_bulk_price_detail.Snf_Weightage,tspl_bulk_price_detail.Fat_Percentage, " & _
            "tspl_bulk_price_detail.Snf_Percentage,tspl_bulk_price_detail.Standard_Rate,tspl_bulk_price_detail.Tolerance from " & _
            "tspl_bulk_price_detail where tspl_bulk_price_detail.Price_Code='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clspriceCodeBulkProcDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clspriceCodeBulkProcDetail = New clspriceCodeBulkProcDetail()
                obj.Price_Code = strCode
                obj.Line_No = clsCommon.myCdbl(dr("Line_No"))
                obj.Milk_Grade_code = clsCommon.myCstr(dr("Milk_Grade_code"))
                obj.Fat_Weightage = clsCommon.myCdbl(dr("Fat_Weightage"))
                obj.Snf_Weightage = clsCommon.myCdbl(dr("Snf_Weightage"))
                obj.Fat_Percentage = clsCommon.myCdbl(dr("Fat_Percentage"))
                obj.Snf_Percentage = clsCommon.myCdbl(dr("Snf_Percentage"))
                obj.Standard_Rate = clsCommon.myCdbl(dr("Standard_Rate"))
                obj.Tolerance = clsCommon.myCdbl(dr("Tolerance"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clspriceCodeBulkProcDetailItemWise
#Region "Variables"
    Public Price_Code As String = Nothing
    Public Milk_Grade_code As String = Nothing
    Public Line_No As Integer = 0
    Public Fat_Weightage As Double = 0
    Public Snf_Weightage As Double = 0
    Public Fat_Percentage As Double = 0
    Public Snf_Percentage As Double = 0
    Public Standard_Rate As Double = 0
    Public Tolerance As Double = 0
    Public PriceType As String = String.Empty
    Public TotalSolidRate As Double = 0
    Public TotalSolidUOM As String = String.Empty
    Public arrSNFDeduction As Dictionary(Of Decimal, Decimal)
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clspriceCodeBulkProcDetailItemWise), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim sQuery As String = "Delete from tspl_bulk_price_detail_item_wise where Price_Code='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            For Each obj As clspriceCodeBulkProcDetailItemWise In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Price_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Item_code", obj.Milk_Grade_code, True)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Fat_Weightage", obj.Fat_Weightage)
                clsCommon.AddColumnsForChange(coll, "Snf_Weightage", obj.Snf_Weightage)
                clsCommon.AddColumnsForChange(coll, "Fat_Percentage", obj.Fat_Percentage)
                clsCommon.AddColumnsForChange(coll, "Snf_Percentage", obj.Snf_Percentage)
                clsCommon.AddColumnsForChange(coll, "Standard_Rate", obj.Standard_Rate)
                clsCommon.AddColumnsForChange(coll, "Tolerance", obj.Tolerance)
                clsCommon.AddColumnsForChange(coll, "PriceType", obj.PriceType, True)
                clsCommon.AddColumnsForChange(coll, "TotalSolidRate", obj.TotalSolidRate)
                clsCommon.AddColumnsForChange(coll, "TotalSolidUOM", obj.TotalSolidUOM, True)
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_bulk_price_detail_item_wise", OMInsertOrUpdate.Insert, "", trans)
                clspriceCodeBulkProcDetailItemWiseSNFDeduction.SaveData(strDocNo, obj.Line_No, obj.arrSNFDeduction, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clspriceCodeBulkProcDetailItemWise)
        Dim arr As List(Of clspriceCodeBulkProcDetailItemWise) = Nothing
        Dim qry As String
        qry = "select tspl_bulk_price_detail_item_wise.Line_No,tspl_bulk_price_detail_item_wise.Item_Code,tspl_bulk_price_detail_item_wise.Fat_Weightage,tspl_bulk_price_detail_item_wise.Snf_Weightage,tspl_bulk_price_detail_item_wise.Fat_Percentage, " &
            "tspl_bulk_price_detail_item_wise.Snf_Percentage,tspl_bulk_price_detail_item_wise.Standard_Rate,tspl_bulk_price_detail_item_wise.Tolerance,tspl_bulk_price_detail_item_wise.PriceType,tspl_bulk_price_detail_item_wise.TotalSolidRate,tspl_bulk_price_detail_item_wise.TotalSolidUOM from " &
            "tspl_bulk_price_detail_item_wise where tspl_bulk_price_detail_item_wise.Price_Code='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clspriceCodeBulkProcDetailItemWise)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clspriceCodeBulkProcDetailItemWise = New clspriceCodeBulkProcDetailItemWise()
                obj.Price_Code = strCode
                obj.Line_No = clsCommon.myCdbl(dr("Line_No"))
                obj.Milk_Grade_code = clsCommon.myCstr(dr("Item_code"))
                obj.Fat_Weightage = clsCommon.myCdbl(dr("Fat_Weightage"))
                obj.Snf_Weightage = clsCommon.myCdbl(dr("Snf_Weightage"))
                obj.Fat_Percentage = clsCommon.myCdbl(dr("Fat_Percentage"))
                obj.Snf_Percentage = clsCommon.myCdbl(dr("Snf_Percentage"))
                obj.Standard_Rate = clsCommon.myCdbl(dr("Standard_Rate"))
                obj.Tolerance = clsCommon.myCdbl(dr("Tolerance"))
                obj.PriceType = clsCommon.myCstr(dr("PriceType"))
                obj.TotalSolidRate = clsCommon.myCdbl(dr("TotalSolidRate"))
                obj.TotalSolidUOM = clsCommon.myCstr(dr("TotalSolidUOM"))
                obj.arrSNFDeduction = clspriceCodeBulkProcDetailItemWiseSNFDeduction.GetData(obj.Price_Code, obj.Line_No, trans)
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clspriceCodeBulkProcDetailItemWiseSNFDeduction
#Region "variables"
    Public Price_Code As String = Nothing
    Public SNo As Integer
    Public SNF_Per As Double
    Public Ded_Amount As Double
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal intSNo As Integer, ByVal Arr As Dictionary(Of Decimal, Decimal), ByVal trans As SqlTransaction) As Boolean
        Try

            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For ii As Integer = 0 To Arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Price_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "SNo", intSNo)
                    clsCommon.AddColumnsForChange(coll, "SNF_Per", Arr.Keys(ii))
                    clsCommon.AddColumnsForChange(coll, "Ded_Amount", Arr(Arr.Keys(ii)))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULK_PRICE_DETAIL_ITEM_WISE_SNF_DEDUCTION", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal intSNo As Integer, ByVal trans As SqlTransaction) As Dictionary(Of Decimal, Decimal)
        Dim arr As New Dictionary(Of Decimal, Decimal)
        Dim qry As String = "Select TSPL_BULK_PRICE_DETAIL_ITEM_WISE_SNF_DEDUCTION.* from TSPL_BULK_PRICE_DETAIL_ITEM_WISE_SNF_DEDUCTION Where Price_Code='" + strCode + "' and SNo='" + clsCommon.myCstr(intSNo) + "' order by SNF_Per "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCdbl(dr("SNF_Per")), clsCommon.myCdbl(dr("Ded_Amount")))
            Next
        End If
        Return arr
    End Function
End Class