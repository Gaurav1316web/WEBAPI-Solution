Imports System.Data.SqlClient
Imports common
Public Class clsJobWorkOutward

#Region "Variables"
    Public Code As String = Nothing
    Public Description As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal obj As clsJobWorkOutward, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry1 As String = clsDBFuncationality.getSingleValue("Select Code from TSPL_JOB_OUTWARD_PRICE_MASTER where code='" & obj.Code & "' ", trans)
        If clsCommon.myLen(qry1) > 0 Then
            'clsCommon.MyMessageBoxShow("Already Generate HSN Code.")
            'Return False
            'trans.Rollback()
            isNewEntry = False
        End If
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JOB_OUTWARD_PRICE_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JOB_OUTWARD_PRICE_MASTER", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsJobWorkOutward
        Dim obj As clsJobWorkOutward = Nothing
        Dim qry As String = "select TSPL_JOB_OUTWARD_PRICE_MASTER.* from TSPL_JOB_OUTWARD_PRICE_MASTER   where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_JOB_OUTWARD_PRICE_MASTER.Code = (select MIN(Code) from TSPL_JOB_OUTWARD_PRICE_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_JOB_OUTWARD_PRICE_MASTER.Code = (select Max(Code) from TSPL_JOB_OUTWARD_PRICE_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_JOB_OUTWARD_PRICE_MASTER.Code = (select TOP 1 Code from TSPL_JOB_OUTWARD_PRICE_MASTER WHERE 1=1 " + whrclas + " and Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_JOB_OUTWARD_PRICE_MASTER.Code = (select Min(Code) from TSPL_JOB_OUTWARD_PRICE_MASTER where Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_JOB_OUTWARD_PRICE_MASTER.Code = (select Max(Code) from TSPL_JOB_OUTWARD_PRICE_MASTER where Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsJobWorkOutward()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))

        End If
        Return obj
    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_JOB_OUTWARD_PRICE_MASTER where Code='" + strCode + "'"))
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = ""
        Try
            qry = "Delete from TSPL_JOB_OUTWARD_PRICE_MASTER where TSPL_JOB_OUTWARD_PRICE_MASTER.Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_JOB_OUTWARD_PRICE_MASTER.Code,TSPL_JOB_OUTWARD_PRICE_MASTER.Description from TSPL_JOB_OUTWARD_PRICE_MASTER "
        str = clsCommon.ShowSelectForm("AreaFnd", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function

    Public Shared Function CheckNewEntry(ByVal Code As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select Code from TSPL_JOB_OUTWARD_PRICE_MASTER where Code ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

End Class

Public Class clsVendorItemChargeMaster
#Region "Variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public Arr As List(Of clsVendorItemChargeDetail) = Nothing
    Public UomArr As List(Of clsVendorItemChargeDetail) = Nothing
    Public PriceCode As String = Nothing
    Public StartDate As DateTime
    Public EndDate As Date?
    Public CreatedDate As DateTime = Nothing
    Public ItemType As String = Nothing
    Public Vendor_Price_Code As String = Nothing
    Public Status As Char = Nothing

#End Region
    Public Function SaveData(ByVal obj As clsVendorItemChargeMaster, ByVal isNewEntry As Boolean, ByVal isMakeAbandomentNo As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, isMakeAbandomentNo, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsVendorItemChargeMaster, ByVal isNewEntry As Boolean, ByVal isMakeAbandomentNo As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_JOB_OUTWARD_PRICE_HEAD", "Price_Code", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_JOB_OUTWARD_PRICE_DETAIL", "Price_Code", trans)

            Dim qry As String = "delete from TSPL_JOB_OUTWARD_PRICE_DETAIL where Price_Code='" + obj.Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, obj.CreatedDate, clsDocType.JWPriceCode, "", "")
            End If

            If (clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.CreatedDate, "dd/MMM/yyyy hh:mm tt"))
            If obj.EndDate Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "EndDate", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "EndDate", clsCommon.GetPrintDate(obj.EndDate, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.ItemType)
            clsCommon.AddColumnsForChange(coll, "StartDate", clsCommon.GetPrintDate(obj.StartDate, "dd/MMM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "Vendor_Price_Code", obj.Vendor_Price_Code)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JOB_OUTWARD_PRICE_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JOB_OUTWARD_PRICE_HEAD", OMInsertOrUpdate.Update, "TSPL_JOB_OUTWARD_PRICE_HEAD.Price_Code='" + obj.Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsVendorItemChargeDetail.SaveData(obj.Code, obj.Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = ""
        Try
            qry = "Delete from TSPL_JOB_OUTWARD_PRICE_DETAIL where TSPL_JOB_OUTWARD_PRICE_DETAIL.Price_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            qry = "Delete from TSPL_JOB_OUTWARD_PRICE_HEAD where TSPL_JOB_OUTWARD_PRICE_HEAD.Price_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, Optional ByVal arrLoc As String = "") As clsVendorItemChargeMaster
        Return GetData(strDocumentNo, NavType, arrLoc, Nothing)
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, ByVal ItemType As String, ByVal trans As SqlTransaction) As clsVendorItemChargeMaster
        Dim obj As clsVendorItemChargeMaster = Nothing
        Dim qry As String = ""
        qry = " select * from TSPL_JOB_OUTWARD_PRICE_HEAD where 2=2 "
        Dim whrClas As String = ""
        If clsCommon.myLen(ItemType) > 0 Then
            whrClas = " AND Item_Type = " + ItemType + ""
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_JOB_OUTWARD_PRICE_HEAD.Price_Code = (select MIN(Price_Code) from TSPL_JOB_OUTWARD_PRICE_HEAD where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_JOB_OUTWARD_PRICE_HEAD.Price_Code = (select Max(Price_Code) from TSPL_JOB_OUTWARD_PRICE_HEAD where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_JOB_OUTWARD_PRICE_HEAD.Price_Code = (select Min(Price_Code) from TSPL_JOB_OUTWARD_PRICE_HEAD where Price_Code>'" + strDocumentNo + "'" + whrClas + " )"
            Case NavigatorType.Previous
                qry += " and TSPL_JOB_OUTWARD_PRICE_HEAD.Price_Code = (select Max(Price_Code) from TSPL_JOB_OUTWARD_PRICE_HEAD where Price_Code<'" + strDocumentNo + "'" + whrClas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_JOB_OUTWARD_PRICE_HEAD.Price_Code = '" + strDocumentNo + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)


        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsVendorItemChargeMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.ItemType = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
            obj.Vendor_Price_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Price_Code"))
            obj.CreatedDate = clsCommon.myCstr(dt.Rows(0)("Price_Date"))
            obj.StartDate = clsCommon.myCDate(dt.Rows(0)("StartDate"))
            If dt.Rows(0)("EndDate") IsNot DBNull.Value Then
                obj.EndDate = clsCommon.myCDate(dt.Rows(0)("EndDate"))
            End If
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Posted"))

            qry = " select *,TSPL_ITEM_MASTER.Item_Desc as RM_Item_Name from TSPL_JOB_OUTWARD_PRICE_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_JOB_OUTWARD_PRICE_DETAIL.RM_Item_Code where Price_Code='" & obj.Code & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsVendorItemChargeDetail)
                Dim objTr As clsVendorItemChargeDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsVendorItemChargeDetail
                    objTr.RM_Item_Code = clsCommon.myCstr(dr("RM_Item_Code"))
                    objTr.RM_Item_Name = clsCommon.myCstr(dr("RM_Item_Name"))
                    objTr.ItemCode = clsCommon.myCstr(dr("ItemCode"))
                    objTr.ItemDesc = clsCommon.myCstr(dr("ItemDesc"))
                    objTr.ItemUom = clsCommon.myCstr(dr("ItemUom"))
                    objTr.ItemCharge = clsCommon.myCstr(dr("Charges"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function


    Public Function PostData(ByVal obj As clsVendorItemChargeMaster, ByVal isNewEntry As Boolean, ByVal isMakeAbandomentNo As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(obj, isNewEntry, isMakeAbandomentNo, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Function PostData(ByVal obj As clsVendorItemChargeMaster, ByVal isNewEntry As Boolean, ByVal isMakeAbandomentNo As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = "Y") Then
                Throw New Exception("Already Post Date")
            End If

            Dim qry As String = "Update TSPL_JOB_OUTWARD_PRICE_head set Posted=1 where Price_Code='" + obj.Code + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetConvertionFactorValue(ByVal strICode As String, ByVal strUOM As String, ByVal strCharges As Decimal, ByVal UOMDetail As String, Optional ByVal trans As SqlTransaction = Nothing) As Double
        Dim finalAmt As Decimal
        If Not clsCommon.CompairString(strUOM, UOMDetail) = CompairStringResult.Equal Then
            Dim GetConvertionFactor As Decimal = clsItemMaster.GetConvertionFactor(strICode, strUOM, trans)
            Dim StockingUnitAmt As Decimal = clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" + strICode + "' and UOM_Code='" & UOMDetail & "'", trans)
            If clsCommon.myLen(GetConvertionFactor) > 0 Then
                finalAmt = (GetConvertionFactor * strCharges) / StockingUnitAmt
            End If
        Else
            finalAmt = strCharges
        End If
        Return (clsCommon.myCdbl(finalAmt))
    End Function

End Class

Public Class clsVendorItemChargeDetail
#Region "variable"

    Public Code As String = Nothing
    Public RM_Item_Code As String = Nothing
    Public RM_Item_Name As String = Nothing
    Public Description As String = Nothing
    Public ItemCode As String = Nothing
    Public ItemDesc As String = Nothing
    Public ItemUom As String = Nothing
    Public UomDesc As String = Nothing
    Public ItemCharge As Decimal = 0
    Public Price_Code As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsVendorItemChargeDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsVendorItemChargeDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Price_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "RM_Item_Code", obj.RM_Item_Code)
                clsCommon.AddColumnsForChange(coll, "ItemCode", obj.ItemCode)
                clsCommon.AddColumnsForChange(coll, "ItemDesc", obj.ItemDesc)
                clsCommon.AddColumnsForChange(coll, "ItemUom", obj.ItemUom)
                clsCommon.AddColumnsForChange(coll, "Charges", obj.ItemCharge)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JOB_OUTWARD_PRICE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetJobPrice(ByVal strVendorCode As String, ByVal strRawICode As String, ByVal strICode As String, ByVal strUOM As String, ByVal TransDate As DateTime, ByVal trans As SqlTransaction) As clsVendorItemChargeDetail
        Dim obj As clsVendorItemChargeDetail = New clsVendorItemChargeDetail
        Dim qry As String = "select top 1 TSPL_JOB_OUTWARD_PRICE_HEAD.Price_Code, TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.Charges from TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM" + Environment.NewLine +
        "left outer join TSPL_JOB_OUTWARD_PRICE_HEAD on TSPL_JOB_OUTWARD_PRICE_HEAD.Price_Code=TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.Price_Code" + Environment.NewLine +
        "left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.JWPriceCode=TSPL_JOB_OUTWARD_PRICE_HEAD.Vendor_Price_Code" + Environment.NewLine +
        "where TSPL_VENDOR_MASTER.Vendor_Code='" + strVendorCode + "' and TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.ItemCode='" + strICode + "' and TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.RM_Item_Code='" + strRawICode + "' and TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.ItemUom='" + strUOM + "' and TSPL_JOB_OUTWARD_PRICE_HEAD.Posted=1 and convert(date, TSPL_JOB_OUTWARD_PRICE_HEAD.StartDate,103)<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(TransDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and 2=(case when TSPL_JOB_OUTWARD_PRICE_HEAD.EndDate is null then 2 else case when convert(date, TSPL_JOB_OUTWARD_PRICE_HEAD.EndDate,103)>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(TransDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 2 else 1 end end)" + Environment.NewLine +
        "order by convert(date, TSPL_JOB_OUTWARD_PRICE_HEAD.StartDate,103) desc"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.ItemCharge = clsCommon.myCstr(dt.Rows(0)("Charges"))
        End If
        Return obj
    End Function
End Class


