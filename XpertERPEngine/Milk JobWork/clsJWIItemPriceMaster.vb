Imports System.Data.SqlClient
Imports common

Public Class clsJWIItemPriceMaster

#Region "Variables"
    Public Price_Code As String = Nothing
    Public Price_Date As DateTime
    Public Description As String = Nothing
    Public Start_Date As DateTime
    Public End_Date As Date?
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Inactive As Boolean = False
    Public Arr As List(Of clsJWIItemPriceDetail) = Nothing
    Public ArrUom As List(Of clsJWIItemPriceDetail) = Nothing
    Public ArrVendor As List(Of clsJWIVendorDetail) = Nothing
#End Region
    'richa 20200616
    Public Function SaveData(ByVal obj As clsJWIItemPriceMaster, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsJWIItemPriceMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Price_Code, "TSPL_JWI_PRICE_HEAD", "Price_Code", "TSPL_JWI_PRICE_DETAIL", "Price_Code", "TSPL_JWI_PRICE_DETAIL_ALL_UOM", "Price_Code", trans)

            Dim qry As String = "delete from TSPL_JWI_PRICE_DETAIL where Price_Code='" + obj.Price_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JWI_PRICE_DETAIL_ALL_UOM where Price_Code='" + obj.Price_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JWI_PRICE_VENDOR_DETAIL where Price_Code='" + obj.Price_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
            If obj.End_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                obj.Price_Code = clsERPFuncationality.GetNextCode(trans, obj.Price_Date, clsDocType.JWIPriceCode, "", "")
                If (clsCommon.myLen(obj.Price_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWI_PRICE_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWI_PRICE_HEAD", OMInsertOrUpdate.Update, "TSPL_JWI_PRICE_HEAD.Price_Code='" + obj.Price_Code + "'", trans)
            End If
            clsJWIItemPriceDetail.SaveData(obj.Price_Code, obj.Arr, trans)
            clsJWIVendorDetail.SaveData(obj.Price_Code, obj.ArrVendor, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String = ""
        Try
            qry = "Delete from TSPL_JWI_PRICE_DETAIL_ALL_UOM where Price_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
            qry = "Delete from TSPL_JWI_PRICE_DETAIL where Price_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "Delete from TSPL_JWI_PRICE_VENDOR_DETAIL where Price_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "Delete from TSPL_JWI_PRICE_HEAD where Price_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
            tran.Commit()
        Catch err As Exception
            tran.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsJWIItemPriceMaster
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsJWIItemPriceMaster
        Dim obj As clsJWIItemPriceMaster = Nothing
        Dim qry As String = ""
        qry = " select * from TSPL_JWI_PRICE_HEAD where 2=2 "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_JWI_PRICE_HEAD.Price_Code = (select MIN(Price_Code) from TSPL_JWI_PRICE_HEAD where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_JWI_PRICE_HEAD.Price_Code = (select Max(Price_Code) from TSPL_JWI_PRICE_HEAD where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_JWI_PRICE_HEAD.Price_Code = (select Min(Price_Code) from TSPL_JWI_PRICE_HEAD where Price_Code>'" + strDocumentNo + "'" + whrClas + " )"
            Case NavigatorType.Previous
                qry += " and TSPL_JWI_PRICE_HEAD.Price_Code = (select Max(Price_Code) from TSPL_JWI_PRICE_HEAD where Price_Code<'" + strDocumentNo + "'" + whrClas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_JWI_PRICE_HEAD.Price_Code = '" + strDocumentNo + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsJWIItemPriceMaster()
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.Price_Date = clsCommon.myCDate(dt.Rows(0)("Price_Date"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Start_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
            If dt.Rows(0)("End_Date") IsNot DBNull.Value Then
                obj.End_Date = clsCommon.myCDate(dt.Rows(0)("End_Date"))
            End If
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Inactive = (clsCommon.myCdbl(dt.Rows(0)("Inactive")) = 1)


            qry = " select TSPL_JWI_PRICE_DETAIL.*,RMItemMaster.Item_Desc as RMItemName,FGItemMaster.Item_Desc as FGItemName from TSPL_JWI_PRICE_DETAIL left outer join tspl_Item_master as RMItemMaster on RMItemMaster.item_Code=TSPL_JWI_PRICE_DETAIL.RM_Item_Code left outer join tspl_Item_master as FGItemMaster on FGItemMaster.item_Code=TSPL_JWI_PRICE_DETAIL.FG_Item_Code where TSPL_JWI_PRICE_DETAIL.Price_Code='" & obj.Price_Code & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsJWIItemPriceDetail)
                For Each dr As DataRow In dt.Rows
                    Dim objTr As clsJWIItemPriceDetail = New clsJWIItemPriceDetail
                    objTr.Price_Code = clsCommon.myCstr(dr("Price_Code"))
                    objTr.RM_Item_Code = clsCommon.myCstr(dr("RM_Item_Code"))
                    objTr.RM_Item_Name = clsCommon.myCstr(dr("RMItemName"))
                    objTr.FG_Item_Code = clsCommon.myCstr(dr("FG_Item_Code"))
                    objTr.FG_Item_Name = clsCommon.myCstr(dr("FGItemName"))
                    objTr.FG_Item_UOM = clsCommon.myCstr(dr("FG_Item_UOM"))
                    objTr.FG_Item_Cost = clsCommon.myCdbl(dr("FG_Item_Cost"))
                    obj.Arr.Add(objTr)
                Next
            End If

            qry = " select TSPL_JWI_PRICE_DETAIL_ALL_UOM.*,RMItemMaster.Item_Desc as RMItemName,FGItemMaster.Item_Desc as FGItemName from TSPL_JWI_PRICE_DETAIL_ALL_UOM left outer join tspl_Item_master as RMItemMaster on RMItemMaster.item_Code=TSPL_JWI_PRICE_DETAIL_ALL_UOM.RM_Item_Code left outer join tspl_Item_master as FGItemMaster on FGItemMaster.item_Code=TSPL_JWI_PRICE_DETAIL_ALL_UOM.FG_Item_Code where TSPL_JWI_PRICE_DETAIL_ALL_UOM.Price_Code='" & obj.Price_Code & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrUom = New List(Of clsJWIItemPriceDetail)
                For Each dr As DataRow In dt.Rows
                    Dim objTr As clsJWIItemPriceDetail = New clsJWIItemPriceDetail
                    objTr.Price_Code = clsCommon.myCstr(dr("Price_Code"))
                    objTr.RM_Item_Code = clsCommon.myCstr(dr("RM_Item_Code"))
                    objTr.RM_Item_Name = clsCommon.myCstr(dr("RMItemName"))
                    objTr.FG_Item_Code = clsCommon.myCstr(dr("FG_Item_Code"))
                    objTr.FG_Item_Name = clsCommon.myCstr(dr("FGItemName"))
                    objTr.FG_Item_UOM = clsCommon.myCstr(dr("FG_Item_UOM"))
                    objTr.FG_Item_Cost = clsCommon.myCdbl(dr("FG_Item_Cost"))
                    obj.ArrUom.Add(objTr)
                Next
            End If


            qry = "select * from TSPL_JWI_PRICE_VENDOR_DETAIL where Price_Code='" & obj.Price_Code & "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.ArrVendor = New List(Of clsJWIVendorDetail)
                For Each dr As DataRow In dt.Rows
                    Dim objTr As clsJWIVendorDetail = New clsJWIVendorDetail
                    objTr.Price_Code = clsCommon.myCstr(dr("Price_Code"))
                    objTr.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                    objTr.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))
                    obj.ArrVendor.Add(objTr)
                Next

            End If

        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim obj As clsJWIItemPriceMaster = clsJWIItemPriceMaster.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Price_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Posted = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post Date")
            End If

            Dim qry As String = "Update TSPL_JWI_PRICE_HEAD set Posted=1,Posted_By='" + objCommonVar.CurrentUserCode + "',Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") + "' where Price_Code='" + obj.Price_Code + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function InactiveData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found")
            End If
            Dim obj As clsJWIItemPriceMaster = clsJWIItemPriceMaster.GetData(strCode, NavigatorType.Current, trans)
            If obj Is Nothing OrElse obj.arr.Count <= 0 Then
                Throw New Exception("Invalid Price code")
            End If
            If obj.Posted <> ERPTransactionStatus.Approved Then
                Throw New Exception("Should be posted Document " + obj.Price_Code)
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Inactive", 1)
            clsCommon.AddColumnsForChange(coll, "Inactive_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Inactive_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWI_PRICE_HEAD", OMInsertOrUpdate.Update, "Price_Code='" + obj.Price_Code + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsJWIItemPriceDetail
#Region "variable"
    Public Price_Code As String = Nothing
    Public RM_Item_Code As String = Nothing
    Public RM_Item_Name As String = Nothing
    Public FG_Item_Code As String = Nothing
    Public FG_Item_Name As String = Nothing
    Public FG_Item_UOM As String = Nothing
    Public FG_Item_Cost As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsJWIItemPriceDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsJWIItemPriceDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Price_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "RM_Item_Code", obj.RM_Item_Code)
                clsCommon.AddColumnsForChange(coll, "FG_Item_Code", obj.FG_Item_Code)
                clsCommon.AddColumnsForChange(coll, "FG_Item_UOM", obj.FG_Item_UOM)
                clsCommon.AddColumnsForChange(coll, "FG_Item_Cost", obj.FG_Item_Cost)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWI_PRICE_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                Dim qry As String = "select Item_Code,UOM_Code,UOM_Description,Conversion_Factor*" + clsCommon.myCstr(obj.FG_Item_Cost) + " as Cost from TSPL_ITEM_UOM_DETAIL  where Item_Code='" & obj.FG_Item_Code & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                    For Each dr As DataRow In dt.Rows
                        coll.Remove("FG_Item_UOM")
                        coll.Remove("FG_Item_Cost")
                        clsCommon.AddColumnsForChange(coll, "FG_Item_UOM", clsCommon.myCstr(dr("UOM_Code")))
                        clsCommon.AddColumnsForChange(coll, "FG_Item_Cost", clsCommon.myCdbl(dr("Cost")))
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWI_PRICE_DETAIL_All_UOM", OMInsertOrUpdate.Insert, "", trans)
                    Next
                End If
            Next
        End If
        Return True
    End Function

    Public Shared Function GetJobPrice(ByVal strRMICode As String, ByVal strFGICode As String, ByVal strFGUOM As String, ByVal TransDate As DateTime, ByVal strVendorCode As String, ByVal trans As SqlTransaction) As clsJWIItemPriceDetail
        Dim obj As clsJWIItemPriceDetail = Nothing
        Dim qry As String = "select top 1 TSPL_JWI_PRICE_DETAIL_ALL_UOM.Price_Code,TSPL_JWI_PRICE_DETAIL_ALL_UOM.FG_Item_Cost from TSPL_JWI_PRICE_DETAIL_ALL_UOM" + Environment.NewLine + _
        "left outer join TSPL_JWI_PRICE_HEAD on TSPL_JWI_PRICE_HEAD.Price_Code=TSPL_JWI_PRICE_DETAIL_ALL_UOM.Price_Code" + Environment.NewLine + _
        "left outer join TSPL_JWI_PRICE_VENDOR_DETAIL on TSPL_JWI_PRICE_HEAD.Price_Code=TSPL_JWI_PRICE_VENDOR_DETAIL.Price_Code " + Environment.NewLine + _
        "where TSPL_JWI_PRICE_DETAIL_ALL_UOM.RM_Item_Code='" + strRMICode + "' and TSPL_JWI_PRICE_DETAIL_ALL_UOM.FG_Item_Code='" + strFGICode + "' and TSPL_JWI_PRICE_DETAIL_ALL_UOM.FG_Item_UOM='" + strFGUOM + "' and TSPL_JWI_PRICE_VENDOR_DETAIL.Vendor_Code='" & strVendorCode & "' " + Environment.NewLine + _
        "and '" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "'>=TSPL_JWI_PRICE_HEAD.Start_Date  and (2= case when TSPL_JWI_PRICE_HEAD.End_Date is null then 2 else case when '" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "'<= TSPL_JWI_PRICE_HEAD.End_Date then 2 else 3 end end) and TSPL_JWI_PRICE_HEAD.Inactive=0 and TSPL_JWI_PRICE_HEAD.Posted=1 order by Start_Date desc,Price_Code desc"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsJWIItemPriceDetail()
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.FG_Item_Cost = clsCommon.myCstr(dt.Rows(0)("FG_Item_Cost"))
        End If
        Return obj
    End Function
End Class

Public Class clsJWIVendorDetail
#Region "variable"
    Public Price_Code As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsJWIVendorDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsJWIVendorDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Price_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWI_PRICE_VENDOR_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class
