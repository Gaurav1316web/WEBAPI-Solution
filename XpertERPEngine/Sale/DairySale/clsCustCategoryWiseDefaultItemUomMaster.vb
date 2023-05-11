Imports System.Data.SqlClient
Imports common


Public Class clsCustCategoryWiseDefaultItemUomMaster
#Region "Variables"
    'Customer_Category,Description,Created_By ,Created_Date,Modified_By, Modified_Date
    Public Customer_Category As String = Nothing
    Public Description As String = Nothing
    Public Arr As List(Of clsCustCategoryWiseDefaultItemUomDetail) = Nothing
    ' Public UomArr As List(Of clsVendorItemChargeDetail) = Nothing


#End Region
    Public Function SaveData(ByVal obj As clsCustCategoryWiseDefaultItemUomMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsCustCategoryWiseDefaultItemUomMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try


            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Customer_Category, "TSPL_CUST_CATEGORY_DEFAULT_UOM_HEAD", "Customer_Category", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Customer_Category, "TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS", "Customer_Category", trans)

            Dim qry As String = "delete from TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS where Customer_Category='" + obj.Customer_Category + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            'If isNewEntry Then
            '    obj.Customer_Category = clsERPFuncationality.GetNextCode(trans, obj.CreatedDate, clsDocType.JWPriceCode, "", "")
            'End If

            'If (clsCommon.myLen(obj.Code) <= 0) Then
            '    Throw New Exception("Error in Document Code Generation")
            'End If

            Dim coll As New Hashtable()
            'clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.CreatedDate, "dd/MMM/yyyy hh:mm tt"))
            'If obj.EndDate Is Nothing Then
            '    clsCommon.AddColumnsForChange(coll, "EndDate", Nothing, True)
            'Else
            '    clsCommon.AddColumnsForChange(coll, "EndDate", clsCommon.GetPrintDate(obj.EndDate, "dd/MMM/yyyy"))
            'End If

            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            'clsCommon.AddColumnsForChange(coll, "StartDate", clsCommon.GetPrintDate(obj.StartDate, "dd/MMM/yyyy"))

            'clsCommon.AddColumnsForChange(coll, "Vendor_Price_Code", obj.Vendor_Price_Code)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Customer_Category", obj.Customer_Category)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUST_CATEGORY_DEFAULT_UOM_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUST_CATEGORY_DEFAULT_UOM_HEAD", OMInsertOrUpdate.Update, "TSPL_CUST_CATEGORY_DEFAULT_UOM_HEAD.Customer_Category='" + obj.Customer_Category + "'", trans)
            End If
            isSaved = isSaved AndAlso clsCustCategoryWiseDefaultItemUomDetail.SaveData(obj.Customer_Category, obj.Arr, trans)


            '' Check Duplicate Price CHart
            'Dim qrycheck As String = "	select TSPL_JOB_OUTWARD_PRICE_Detail.itemcode,TSPL_JOB_OUTWARD_PRICE_Detail.ItemUom,TSPL_JOB_OUTWARD_PRICE_Detail.Charges,TSPL_JOB_OUTWARD_PRICE_head.Vendor_Price_Code,convert(date,TSPL_JOB_OUTWARD_PRICE_head.Price_Date,103) as Price_Date,sum(1) as Repeated from TSPL_JOB_OUTWARD_PRICE_Detail left outer join TSPL_JOB_OUTWARD_PRICE_head on TSPL_JOB_OUTWARD_PRICE_head.price_code=TSPL_JOB_OUTWARD_PRICE_Detail.Price_Code group by TSPL_JOB_OUTWARD_PRICE_head.Vendor_Price_Code,TSPL_JOB_OUTWARD_PRICE_Detail.itemcode,TSPL_JOB_OUTWARD_PRICE_Detail.ItemUom,TSPL_JOB_OUTWARD_PRICE_Detail.Charges,convert(date,TSPL_JOB_OUTWARD_PRICE_head.Price_Date,103) having SUM(1) > 1"
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qrycheck, trans)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    Throw New Exception("Please check ! Price Code and Item is already used. ")
            'End If

            '' End

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = ""
        Try
            qry = "Delete from TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS where TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            qry = "Delete from TSPL_CUST_CATEGORY_DEFAULT_UOM_HEAD where TSPL_CUST_CATEGORY_DEFAULT_UOM_HEAD.Customer_Category='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, Optional ByVal arrLoc As String = "") As clsCustCategoryWiseDefaultItemUomMaster
        Return GetData(strDocumentNo, NavType, arrLoc, Nothing)
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, ByVal ItemType As String, ByVal trans As SqlTransaction) As clsCustCategoryWiseDefaultItemUomMaster
        Dim obj As clsCustCategoryWiseDefaultItemUomMaster = Nothing
        Dim qry As String = ""
        qry = " select * from TSPL_CUST_CATEGORY_DEFAULT_UOM_HEAD where 2=2 and Customer_Category = '" + strDocumentNo + "' "
        Dim whrClas As String = ""
        'If clsCommon.myLen(ItemType) > 0 Then
        '    whrClas = " AND Item_Type = " + ItemType + ""
        'End If
        'Select Case NavType
        '    Case NavigatorType.First

        '        qry += " and TSPL_JOB_OUTWARD_PRICE_HEAD.Price_Code = (select MIN(Price_Code) from TSPL_JOB_OUTWARD_PRICE_HEAD where 1=1 )"
        '    Case NavigatorType.Last
        '        qry += " and TSPL_JOB_OUTWARD_PRICE_HEAD.Price_Code = (select Max(Price_Code) from TSPL_JOB_OUTWARD_PRICE_HEAD where 1=1 )"
        '    Case NavigatorType.Next
        '        qry += " and TSPL_JOB_OUTWARD_PRICE_HEAD.Price_Code = (select Min(Price_Code) from TSPL_JOB_OUTWARD_PRICE_HEAD where Price_Code>'" + strDocumentNo + "'" + whrClas + " )"
        '    Case NavigatorType.Previous
        '        qry += " and TSPL_JOB_OUTWARD_PRICE_HEAD.Price_Code = (select Max(Price_Code) from TSPL_JOB_OUTWARD_PRICE_HEAD where Price_Code<'" + strDocumentNo + "'" + whrClas + " )"
        '    Case NavigatorType.Current
        '        qry += " and TSPL_JOB_OUTWARD_PRICE_HEAD.Price_Code = '" + strDocumentNo + "'"
        'End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)


        'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        obj = New clsCustCategoryWiseDefaultItemUomMaster()
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.Customer_Category = clsCommon.myCstr(dt.Rows(0)("Customer_Category"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
        Else
            obj.Customer_Category = strDocumentNo
            obj.Description = ""
        End If


        ''====Detail Table data
        Dim whr As String = ""
        If clsCommon.CompairString(obj.Customer_Category, "Others") <> CompairStringResult.Equal Then
            whr = " and isnull(tspl_item_master.Chilled_Freezen,0)=1 and isnull(TSPL_ITEM_MASTER.item_type,'')='F'  and tspl_item_master.Active=1 "
        Else
            whr = " and isnull(tspl_item_master.Item_Type,'')='F' and isnull(tspl_item_master.TypeOfItm,'')='A' and tspl_item_master.Active=1 "
        End If
        qry = " select TSPL_ITEM_MASTER.item_Code, TSPL_ITEM_MASTER.Item_Desc, TSPL_ITEM_MASTER.Short_Description, TSPL_ITEM_MASTER.Alies_Name, TSPL_ITEM_MASTER.HSN_Code, TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM from TSPL_ITEM_MASTER left outer Join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = TSPL_ITEM_MASTER.item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = '" + obj.Customer_Category + "' where 2=2 " + whr + ""
        dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsCustCategoryWiseDefaultItemUomDetail)
                Dim objTr As clsCustCategoryWiseDefaultItemUomDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsCustCategoryWiseDefaultItemUomDetail
                    objTr.Item_code = clsCommon.myCstr(dr("item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Short_Description = clsCommon.myCstr(dr("Short_Description"))
                    objTr.Alies_Name = clsCommon.myCstr(dr("Alies_Name"))
                    objTr.HSN_Code = clsCommon.myCstr(dr("HSN_Code"))
                    objTr.Default_UOM = clsCommon.myCstr(dr("Default_UOM"))
                    obj.Arr.Add(objTr)
                Next

            End If


            'End If

            Return obj

    End Function


    'Public Function PostData(ByVal obj As clsVendorItemChargeMaster, ByVal isNewEntry As Boolean, ByVal isMakeAbandomentNo As Boolean) As Boolean
    '    Dim isSaved As Boolean = True
    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try
    '        PostData(obj, isNewEntry, isMakeAbandomentNo, trans)
    '        trans.Commit()
    '    Catch err As Exception
    '        trans.Rollback()
    '        Throw New Exception(err.Message)
    '    End Try
    '    Return True
    'End Function
    'Public Function PostData(ByVal obj As clsVendorItemChargeMaster, ByVal isNewEntry As Boolean, ByVal isMakeAbandomentNo As Boolean, ByVal trans As SqlTransaction) As Boolean
    '    Dim isSaved As Boolean = True
    '    Try
    '        If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
    '            Throw New Exception("No Data found to Post")
    '        End If
    '        If (obj.Status = "Y") Then
    '            Throw New Exception("Already Post Date")
    '        End If

    '        Dim qry As String = "Update TSPL_JOB_OUTWARD_PRICE_head set Posted=1 where Price_Code='" + obj.Code + "' "
    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '    Catch err As Exception
    '        Throw New Exception(err.Message)
    '    End Try
    '    Return isSaved
    'End Function

    'Public Shared Function GetConvertionFactorValue(ByVal strICode As String, ByVal strUOM As String, ByVal strCharges As Decimal, ByVal UOMDetail As String, Optional ByVal trans As SqlTransaction = Nothing) As Double
    '    Dim finalAmt As Decimal
    '    If Not clsCommon.CompairString(strUOM, UOMDetail) = CompairStringResult.Equal Then
    '        Dim GetConvertionFactor As Decimal = clsItemMaster.GetConvertionFactor(strICode, strUOM, trans)
    '        Dim StockingUnitAmt As Decimal = clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" + strICode + "' and UOM_Code='" & UOMDetail & "'", trans)
    '        If clsCommon.myLen(GetConvertionFactor) > 0 Then
    '            finalAmt = (GetConvertionFactor * strCharges) / StockingUnitAmt
    '        End If
    '    Else
    '        finalAmt = strCharges
    '    End If
    '    Return (clsCommon.myCdbl(finalAmt))
    'End Function

End Class

Public Class clsCustCategoryWiseDefaultItemUomDetail
#Region "variable"

    Public Customer_Category As String = Nothing
    Public Item_code As String = Nothing
    Public Default_UOM As String = Nothing
    Public Item_Desc As String = Nothing

    Public Short_Description As String = Nothing
    Public Alies_Name As String = Nothing
    Public HSN_Code As String = Nothing


#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsCustCategoryWiseDefaultItemUomDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCustCategoryWiseDefaultItemUomDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Customer_Category", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Item_code", obj.Item_code)
                clsCommon.AddColumnsForChange(coll, "Default_UOM", obj.Default_UOM)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    'Public Shared Function GetJobPrice(ByVal strVendorCode As String, ByVal strICode As String, ByVal strUOM As String, ByVal TransDate As DateTime, ByVal trans As SqlTransaction) As clsVendorItemChargeDetail
    '    Dim obj As clsVendorItemChargeDetail = New clsVendorItemChargeDetail
    '    Dim qry As String = "select top 1 TSPL_JOB_OUTWARD_PRICE_HEAD.Price_Code, TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.Charges from TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM" + Environment.NewLine + _
    '    "left outer join TSPL_JOB_OUTWARD_PRICE_HEAD on TSPL_JOB_OUTWARD_PRICE_HEAD.Price_Code=TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.Price_Code" + Environment.NewLine + _
    '    "left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.JWPriceCode=TSPL_JOB_OUTWARD_PRICE_HEAD.Vendor_Price_Code" + Environment.NewLine + _
    '    "where TSPL_VENDOR_MASTER.Vendor_Code='" + strVendorCode + "' and TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.ItemCode='" + strICode + "' and TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.ItemUom='" + strUOM + "' and TSPL_JOB_OUTWARD_PRICE_HEAD.Posted=1 and TSPL_JOB_OUTWARD_PRICE_HEAD.StartDate>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(TransDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and 2=(case when TSPL_JOB_OUTWARD_PRICE_HEAD.EndDate is null then 2 else case when TSPL_JOB_OUTWARD_PRICE_HEAD.EndDate<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(TransDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 2 else 1 end end)" + Environment.NewLine + _
    '    "order by TSPL_JOB_OUTWARD_PRICE_HEAD.StartDate desc"
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '        obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
    '        obj.ItemCharge = clsCommon.myCstr(dt.Rows(0)("Charges"))
    '    End If
    '    Return obj
    'End Function

    'Public Shared Function GetJobPrice(ByVal strVendorCode As String, ByVal strICode As String, ByVal strUOM As String, ByVal TransDate As DateTime, ByVal trans As SqlTransaction) As clsVendorItemChargeDetail
    '    Dim obj As clsVendorItemChargeDetail = New clsVendorItemChargeDetail
    '    Dim qry As String = "select top 1 TSPL_JOB_OUTWARD_PRICE_HEAD.Price_Code, TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.Charges from TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM" + Environment.NewLine +
    '    "left outer join TSPL_JOB_OUTWARD_PRICE_HEAD on TSPL_JOB_OUTWARD_PRICE_HEAD.Price_Code=TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.Price_Code" + Environment.NewLine +
    '    "left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.JWPriceCode=TSPL_JOB_OUTWARD_PRICE_HEAD.Vendor_Price_Code" + Environment.NewLine +
    '    "where TSPL_VENDOR_MASTER.Vendor_Code='" + strVendorCode + "' and TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.ItemCode='" + strICode + "' and TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.ItemUom='" + strUOM + "' and TSPL_JOB_OUTWARD_PRICE_HEAD.Posted=1 and convert(date, TSPL_JOB_OUTWARD_PRICE_HEAD.StartDate,103)<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(TransDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and 2=(case when TSPL_JOB_OUTWARD_PRICE_HEAD.EndDate is null then 2 else case when convert(date, TSPL_JOB_OUTWARD_PRICE_HEAD.EndDate,103)>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(TransDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 2 else 1 end end)" + Environment.NewLine +
    '    "order by convert(date, TSPL_JOB_OUTWARD_PRICE_HEAD.StartDate,103) desc"
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '        obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
    '        obj.ItemCharge = clsCommon.myCstr(dt.Rows(0)("Charges"))
    '    End If
    '    Return obj
    'End Function
End Class


