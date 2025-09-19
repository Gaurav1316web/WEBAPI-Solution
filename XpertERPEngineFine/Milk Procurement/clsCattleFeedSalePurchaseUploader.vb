Imports System.Data.SqlClient
Imports common

Public Class clsCattleFeedSalePurchaseUploader
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_date As Date? = Nothing
    Public Location_Code As String = Nothing
    Public Sub_Location_Code As String = Nothing
    Public Remarks As String = Nothing
    Public Status As Integer = 0
    Public Total_Penalty As Decimal = 0
    Public Arr As List(Of clsCattleFeedSalePurchaseUploaderDetail) = Nothing
    Public ArrItemDetails As List(Of clsCattleFeedSalePurchaseUploaderItemDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsCattleFeedSalePurchaseUploader, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal AutoSave As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, Nothing, trans, AutoSave)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsCattleFeedSalePurchaseUploader, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal trans As SqlTransaction, ByVal AutoSave As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_ITEM_DETAIL where Document_No='" & obj.Document_No & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL where Document_No='" & obj.Document_No & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Sub_Location_Code", obj.Sub_Location_Code)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_date, clsDocType.CFSalePurchaseUploader, "", "")
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER", OMInsertOrUpdate.Update, "TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.Document_No='" & obj.Document_No & "'", trans)
            End If
            Dim objDetail As New clsCattleFeedSalePurchaseUploaderDetail()
            isSaved = isSaved AndAlso objDetail.SaveData(obj.Document_No, obj.Arr, trans)

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType) As clsCattleFeedSalePurchaseUploader
        Return GetData(strRetCode, NavType, Nothing)
    End Function

    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCattleFeedSalePurchaseUploader
        Dim obj As clsCattleFeedSalePurchaseUploader = Nothing
        Dim qry As String = "select * from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry &= " and TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.Document_No = (select MIN(Document_No) from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER)"
            Case NavigatorType.Last
                qry &= " and TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.Document_No = (select Max(Document_No) from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER)"
            Case NavigatorType.Next
                qry &= " and TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.Document_No = (select Min(Document_No) from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER where Document_No >'" & strCode & "')"
            Case NavigatorType.Previous
                qry &= " and TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.Document_No = (select Max(Document_No) from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER where Document_No <'" & strCode & "')"
            Case NavigatorType.Current
                qry &= " and TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.Document_No = '" & strCode & "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsCattleFeedSalePurchaseUploader()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_date = clsCommon.myCDate(dt.Rows(0)("Document_date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Sub_Location_Code = clsCommon.myCstr(dt.Rows(0)("Sub_Location_Code"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            Dim objDetail As New clsCattleFeedSalePurchaseUploaderDetail()
            obj.Arr = objDetail.GetData(obj.Document_No, trans)
            Dim objItemDetail As New clsCattleFeedSalePurchaseUploaderItemDetail()
            obj.ArrItemDetails = objItemDetail.GetData(obj.Document_No, 0, True, trans)
        End If
        Return obj
    End Function

    Public Function getFinder(ByVal strCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim sql As String = "select Document_No as DocumentNo ,convert(varchar(12),Document_date,103) as DocumentDate, Location_Code as [Location Code],Sub_Location_Code as [Sub Location Code],case when Status = 1 then 'posted' else 'Unposted' end as Posted from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER"
        str = clsCommon.ShowSelectForm("CFSPUP", sql, "DocumentNo", "", strCode, "DocumentNo", isButtonClicked)
        Return str
    End Function

    Public Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso PostData(FormId, strDocNo, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Docume nt No not found to Post")
            End If
            Dim obj As clsCattleFeedSalePurchaseUploader = New clsCattleFeedSalePurchaseUploader()
            obj = obj.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Posted")
            End If
            'Dim objSRN As clsSRNHead=New clsSRNHead()

            clsDBFuncationality.ExecuteNonQuery("Update TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER set Status= 1, Posted_By = '" & objCommonVar.CurrentUserCode & "',Posted_Date = '" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") & "'  where Document_No='" & obj.Document_No & "'", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim isResponse As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If ReverseAndUnpost(strCode, trans) Then
                isResponse = True
            Else
                isResponse = False
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isResponse
    End Function

    Public Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isResponse As Boolean = True
        Try

            Dim obj As clsCattleFeedSalePurchaseUploader = New clsCattleFeedSalePurchaseUploader
            obj = obj.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Status) <= 0) Then
                clsCommon.MyMessageBoxShow("No Data found to Reverse And UnPost")
                isResponse = False
            End If

            If Not obj.Status = 1 Then
                clsCommon.MyMessageBoxShow("Transaction status should be posted for reverse and unpost")
                isResponse = False
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 0)
            clsCommon.AddColumnsForChange(coll, "Posted_By", Nothing, True)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", Nothing, True)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER", OMInsertOrUpdate.Update, "Document_No='" & obj.Document_No & "'", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isResponse
    End Function

    Public Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsCattleFeedSalePurchaseUploader = New clsCattleFeedSalePurchaseUploader()
        obj = obj.GetData(strCode, NavigatorType.Current, trans)
        Try
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            If clsCommon.CompairString(obj.Status, "1") = CompairStringResult.Equal Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = Nothing

            qry = "delete from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_ITEM_DETAIL where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsCattleFeedSalePurchaseUploaderDetail

#Region "Variables"
    Public Document_No As String = Nothing
    Public SRN_Dispatch_Date As Date
    Public VLC_Code As String = Nothing
    Public VLC_Code_VLC_Uploader As String = Nothing
    Public VLC_Name As String = Nothing
    Public Zone_Code As String = Nothing
    Public GRN_No As String = Nothing
    Public Truck_No As String = Nothing
    Public Challan_No As String = Nothing
    Public Freight As Decimal
    Public Bill_No As String = Nothing
    Public Sale_Type As String = Nothing
    Public Total_Sale_Amt As Decimal
    Public ArrItemDetails As List(Of clsCattleFeedSalePurchaseUploaderItemDetail) = Nothing
#End Region
    Public Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsCattleFeedSalePurchaseUploaderDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCattleFeedSalePurchaseUploaderDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strCode)
                clsCommon.AddColumnsForChange(coll, "SRN_Dispatch_Date", clsCommon.GetPrintDate(obj.SRN_Dispatch_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "VLC_Code", obj.VLC_Code)
                clsCommon.AddColumnsForChange(coll, "Zone_Code", obj.Zone_Code)
                clsCommon.AddColumnsForChange(coll, "GRN_No", obj.GRN_No)
                clsCommon.AddColumnsForChange(coll, "Truck_No", obj.Truck_No)
                clsCommon.AddColumnsForChange(coll, "Challan_No", obj.Challan_No)
                clsCommon.AddColumnsForChange(coll, "Freight", obj.Freight)
                clsCommon.AddColumnsForChange(coll, "Bill_No", obj.Bill_No)
                clsCommon.AddColumnsForChange(coll, "Sale_Type", obj.Sale_Type)
                clsCommon.AddColumnsForChange(coll, "Total_Sale_Amt", obj.Total_Sale_Amt)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                Dim PK_ID As Integer = clsERPFuncationality.GetScopeIdentityValue(trans)
                Dim objItemDetail As New clsCattleFeedSalePurchaseUploaderItemDetail()
                objItemDetail.SaveData(strCode, PK_ID, obj.ArrItemDetails, trans)
            Next
        End If
        Return True
    End Function

    Public Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsCattleFeedSalePurchaseUploaderDetail)
        Dim arr As List(Of clsCattleFeedSalePurchaseUploaderDetail) = Nothing
        Dim qry As String = "select TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Document_No, TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.SRN_Dispatch_Date,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Zone_Code,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.GRN_No,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Truck_No,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Challan_No,
TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Freight,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Bill_No,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Sale_Type,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Total_Sale_Amt,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.pk_id
         from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL left outer join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VLC_CODE = TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.VLC_CODE where  TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Document_No = '" & strCode & "' order by Document_No,PK_ID "
        Dim PK_ID As Integer = 0
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsCattleFeedSalePurchaseUploaderDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsCattleFeedSalePurchaseUploaderDetail = New clsCattleFeedSalePurchaseUploaderDetail()
                obj.Document_No = clsCommon.myCstr(dr("Document_No"))
                obj.SRN_Dispatch_Date = clsCommon.myCDate(dr("SRN_Dispatch_Date"))
                obj.VLC_Code = clsCommon.myCstr(dr("VLC_Code"))
                obj.VLC_Code_VLC_Uploader = clsCommon.myCstr(dr("VLC_Code_VLC_Uploader"))
                obj.VLC_Name = clsCommon.myCstr(dr("VLC_Name"))
                obj.Zone_Code = clsCommon.myCstr(dr("Zone_Code"))
                obj.GRN_No = clsCommon.myCstr(dr("GRN_No"))
                obj.Truck_No = clsCommon.myCstr(dr("Truck_No"))
                obj.Challan_No = clsCommon.myCstr(dr("Challan_No"))
                obj.Freight = clsCommon.myCDecimal(dr("Freight"))
                obj.Bill_No = clsCommon.myCstr(dr("Bill_No"))
                obj.Sale_Type = clsCommon.myCstr(dr("Sale_Type"))
                obj.Total_Sale_Amt = clsCommon.myCDecimal(dr("Total_Sale_Amt"))
                PK_ID = clsCommon.myCdbl(dr("PK_ID"))
                Dim objItemDetail As New clsCattleFeedSalePurchaseUploaderItemDetail()
                obj.ArrItemDetails = objItemDetail.GetData(strCode, PK_ID, False, trans)
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function

End Class

Public Class clsCattleFeedSalePurchaseUploaderItemDetail
#Region "Variables"
    Public Document_No As String = Nothing
    Public Ref_PK_ID As Integer
    Public Item_Code As String = Nothing
    Public Unit_Code As String = Nothing
    Public Purchase_Qty As Decimal
    Public Purchase_Rate As Decimal
    Public Purchase_Amt As Decimal
    Public Sale_Rate As Decimal
    Public Sale_Amt As Decimal
#End Region
    Public Function SaveData(ByVal strCode As String, ByVal PK_ID As Integer, ByVal Arr As List(Of clsCattleFeedSalePurchaseUploaderItemDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCattleFeedSalePurchaseUploaderItemDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strCode)
                clsCommon.AddColumnsForChange(coll, "Ref_PK_ID", PK_ID)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "Purchase_Qty", obj.Purchase_Qty)
                clsCommon.AddColumnsForChange(coll, "Purchase_Rate", obj.Purchase_Rate)
                clsCommon.AddColumnsForChange(coll, "Purchase_Amt", obj.Purchase_Amt)
                clsCommon.AddColumnsForChange(coll, "Sale_Amt", obj.Sale_Amt)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Function GetData(ByVal strCode As String, ByVal Ref_PK_ID As Integer, ByVal LoadAllData As Boolean, ByVal trans As SqlTransaction) As List(Of clsCattleFeedSalePurchaseUploaderItemDetail)
        Dim arr As List(Of clsCattleFeedSalePurchaseUploaderItemDetail) = Nothing

        Dim qry As String = "select TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_ITEM_DETAIL.Ref_PK_ID,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_ITEM_DETAIL.* from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_ITEM_DETAIL  
        inner join TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL on TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.PK_Id = TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_ITEM_DETAIL.Ref_PK_ID   where  TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_ITEM_DETAIL.Document_No = '" & strCode & "' "

        If Not LoadAllData Then
            qry &= " and Ref_PK_ID = " & clsCommon.myCstr(Ref_PK_ID) & " "
        End If
        qry &= " order by TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.Document_No, TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL.PK_ID "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsCattleFeedSalePurchaseUploaderItemDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsCattleFeedSalePurchaseUploaderItemDetail = New clsCattleFeedSalePurchaseUploaderItemDetail()
                obj.Document_No = strCode
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                obj.Purchase_Qty = clsCommon.myCDecimal(dr("Purchase_Qty"))
                obj.Purchase_Rate = clsCommon.myCDecimal(dr("Purchase_Rate"))
                obj.Purchase_Amt = clsCommon.myCDecimal(dr("Purchase_Amt"))
                obj.Sale_Amt = clsCommon.myCDecimal(dr("Sale_Amt"))
                obj.Ref_PK_ID = clsCommon.myCdbl(dr("Ref_PK_ID"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class