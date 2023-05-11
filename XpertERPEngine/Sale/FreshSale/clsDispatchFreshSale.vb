Imports common
Imports System.Data.SqlClient
Public Class clsDispatchFreshSale
#Region "Variable"
    Dim qry As String
    Public Document_No As String = Nothing
    Public Document_Date As String = Nothing
    Public DeliveryNo As String = Nothing
    Public Delivery_Date As String = Nothing
    Public Booking_No As String = Nothing
    Public Booking_Date As String = Nothing
    Public Status As String = Nothing
    Public Customer_Code As String = Nothing
    Public Location_Code As String = Nothing
    Public Vehicle_Capacity As Double = 0
    Public Lorry_No As String = Nothing
    Public Road_Permit_No As String = Nothing
    Public Transporter_Name As String = Nothing
    Public Freight As String = Nothing
    Public Freight_Amount As Double = 0
    Public Total_Amt As Double = 0
    Public CrateQty As Double = 0
    Public Posted As String = Nothing
    Public Posting_Date As String = Nothing
    Public OnHold As String = Nothing
    Public Comments As String = Nothing

    Public Form_ID As String = ""
    Public Arr As List(Of clsDispatchFreshSaleDetail) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsDispatchFreshSale, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsDispatchFreshSale, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True

        qry = "delete from TSPL_DISPATCH_DETAIL_FRESHSALE where Document_No='" + obj.Document_No + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Dim strDocNo As String = ""
        If isNewEntry Then
            obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.BookingEntry, "", "")
        End If

        If (clsCommon.myLen(obj.Document_No) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If
        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
        clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
        clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
        clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
        clsCommon.AddColumnsForChange(coll, "DeliveryNo", obj.DeliveryNo)
        clsCommon.AddColumnsForChange(coll, "Delivery_Date", clsCommon.GetPrintDate(obj.Delivery_Date, "dd/MMM/yyyy hh:mm tt"))
        clsCommon.AddColumnsForChange(coll, "Booking_No", obj.Booking_No)
        clsCommon.AddColumnsForChange(coll, "Booking_Date", clsCommon.GetPrintDate(obj.Booking_Date, "dd/MMM/yyyy hh:mm tt"))
        clsCommon.AddColumnsForChange(coll, "Vehicle_Capacity", obj.Vehicle_Capacity)
        clsCommon.AddColumnsForChange(coll, "Lorry_No", obj.Lorry_No)
        clsCommon.AddColumnsForChange(coll, "Road_Permit_No", obj.Road_Permit_No)
        clsCommon.AddColumnsForChange(coll, "Transporter_Name", obj.Transporter_Name)
        clsCommon.AddColumnsForChange(coll, "Freight", obj.Freight)
        clsCommon.AddColumnsForChange(coll, "Freight_Amount", obj.Freight_Amount)
        clsCommon.AddColumnsForChange(coll, "Posted", obj.Posted)
        clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
        clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
        clsCommon.AddColumnsForChange(coll, "CrateQty", obj.CrateQty)
        clsCommon.AddColumnsForChange(coll, "OnHold", obj.OnHold)

        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DISPATCH_MASTER_FRESHSALE", OMInsertOrUpdate.Insert, "", trans)
        Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DISPATCH_MASTER_FRESHSALE", OMInsertOrUpdate.Update, "TSPL_DISPATCH_MASTER_FRESHSALE.Document_No='" + obj.Document_No + "'", trans)
        End If

        isSaved = isSaved AndAlso clsDispatchFreshSaleDetail.SaveData(obj.Document_No, Arr, trans)

        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsDispatchFreshSale
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDispatchFreshSale
        Dim obj As clsDispatchFreshSale = Nothing
        Dim qry = "SELECT TSPL_DISPATCH_MASTER_FRESHSALE.Document_No, TSPL_DISPATCH_MASTER_FRESHSALE.Document_Date, " & _
                      "TSPL_DISPATCH_MASTER_FRESHSALE.Status, TSPL_DISPATCH_MASTER_FRESHSALE.Customer_Code, " & _
                      "TSPL_DISPATCH_MASTER_FRESHSALE.Location_Code, TSPL_DISPATCH_MASTER_FRESHSALE.DeliveryNo, " & _
                      "TSPL_DISPATCH_MASTER_FRESHSALE.Delivery_Date, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No, " & _
                      "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_Date,TSPL_DISPATCH_MASTER_FRESHSALE.Vehicle_Capacity, " & _
                      "TSPL_DISPATCH_MASTER_FRESHSALE.Lorry_No, TSPL_DISPATCH_MASTER_FRESHSALE.Road_Permit_No, " & _
                      "TSPL_DISPATCH_MASTER_FRESHSALE.Transporter_Name, TSPL_DISPATCH_MASTER_FRESHSALE.Freight, " & _
                      "TSPL_DISPATCH_MASTER_FRESHSALE.Freight_Amount, TSPL_DISPATCH_MASTER_FRESHSALE.Posted, " & _
                      "TSPL_DISPATCH_MASTER_FRESHSALE.Comments,TSPL_DISPATCH_MASTER_FRESHSALE.Total_Amt, " & _
                      "TSPL_DISPATCH_MASTER_FRESHSALE.Posting_Date,TSPL_DISPATCH_MASTER_FRESHSALE.OnHold,TSPL_DISPATCH_MASTER_FRESHSALE.CrateQty " & _
                      "FROM TSPL_DISPATCH_MASTER_FRESHSALE where 2=2 "
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DISPATCH_MASTER_FRESHSALE.Document_No = (select MIN(Document_No) from TSPL_DISPATCH_MASTER_FRESHSALE WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_DISPATCH_MASTER_FRESHSALE.Document_No = (select Max(Document_No) from TSPL_DISPATCH_MASTER_FRESHSALE WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_DISPATCH_MASTER_FRESHSALE.Document_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_DISPATCH_MASTER_FRESHSALE.Document_No = (select Min(Document_No) from TSPL_DISPATCH_MASTER_FRESHSALE where Document_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_DISPATCH_MASTER_FRESHSALE.Document_No = (select Max(Document_No) from TSPL_DISPATCH_MASTER_FRESHSALE where Document_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDispatchFreshSale()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Booking_No = clsCommon.myCstr(dt.Rows(0)("Booking_No"))
            obj.Booking_Date = clsCommon.myCDate(dt.Rows(0)("Booking_Date"))
            obj.DeliveryNo = clsCommon.myCstr(dt.Rows(0)("DeliveryNo"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.DeliveryNo = clsCommon.myCstr(dt.Rows(0)("DeliveryNo"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Vehicle_Capacity = clsCommon.myCdbl(dt.Rows(0)("Vehicle_Capacity"))
            obj.Lorry_No = clsCommon.myCstr(dt.Rows(0)("Lorry_No"))
            obj.Road_Permit_No = clsCommon.myCstr(dt.Rows(0)("Road_Permit_No"))
            obj.Transporter_Name = clsCommon.myCstr(dt.Rows(0)("Transporter_Name"))
            obj.Freight = clsCommon.myCstr(dt.Rows(0)("Freight"))
            obj.Freight_Amount = clsCommon.myCdbl(dt.Rows(0)("Freight_Amount"))
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
            obj.CrateQty = clsCommon.myCdbl(dt.Rows(0)("CrateQty"))
            obj.Posted = clsCommon.myCstr(dt.Rows(0)("Posted"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.OnHold = clsCommon.myCstr(dt.Rows(0)("OnHold"))
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If

            qry = "SELECT  TSPL_DISPATCH_DETAIL_FRESHSALE.Document_No, TSPL_DISPATCH_DETAIL_FRESHSALE.Line_No, " & _
            "TSPL_DISPATCH_DETAIL_FRESHSALE.Item_Code,  " & _
            " TSPL_DISPATCH_DETAIL_FRESHSALE.Unit_code, TSPL_DISPATCH_DETAIL_FRESHSALE.DeliveryQty, TSPL_DISPATCH_DETAIL_FRESHSALE.DispatchQty,  " & _
            "TSPL_DISPATCH_DETAIL_FRESHSALE.Rate, TSPL_DISPATCH_DETAIL_FRESHSALE.Amount, TSPL_DISPATCH_DETAIL_FRESHSALE.SchemeType,  " & _
            "TSPL_DISPATCH_DETAIL_FRESHSALE.Scheme_Applicable, TSPL_DISPATCH_DETAIL_FRESHSALE.Scheme_Item,  " & _
            "TSPL_DISPATCH_DETAIL_FRESHSALE.Scheme_Code, TSPL_DISPATCH_DETAIL_FRESHSALE.MainItem_Code,  " & _
            "TSPL_DISPATCH_DETAIL_FRESHSALE.MainItem_Unit_code  " & _
            "FROM TSPL_DISPATCH_DETAIL_FRESHSALE left outer join tspl_item_master on  " & _
            "TSPL_DISPATCH_DETAIL_FRESHSALE.item_code=tspl_item_master.item_code where Document_No='" & strDocNo & "' "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsDispatchFreshSaleDetail)
                Dim objTr As clsDispatchFreshSaleDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsDispatchFreshSaleDetail
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objTr.DeliveryQty = clsCommon.myCdbl(dr("DeliveryQty"))
                    objTr.DispatchQty = clsCommon.myCdbl(dr("DispatchQty"))
                    objTr.Rate = clsCommon.myCdbl(dr("Rate"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.SchemeType = clsCommon.myCstr(dr("SchemeType"))
                    objTr.Scheme_Applicable = clsCommon.myCstr(dr("Scheme_Applicable"))
                    objTr.Scheme_Item = clsCommon.myCstr(dr("Scheme_Item"))
                    objTr.Scheme_Code = clsCommon.myCstr(dr("Scheme_Code"))
                    objTr.MainItem_Code = clsCommon.myCstr(dr("MainItem_Code"))
                    objTr.MainItem_Unit_code = clsCommon.myCstr(dr("MainItem_Unit_code"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsDispatchFreshSale = clsDispatchFreshSale.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try
                If clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If

                Dim qry = "delete from TSPL_DISPATCH_MASTER_FRESHSALE where Document_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_DISPATCH_DETAIL_FRESHSALE where Document_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, trans)
                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("SRN No not found to Post")
            End If
            Dim obj As clsDispatchFreshSale = clsDispatchFreshSale.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim qry = "Update TSPL_DISPATCH_MASTER_FRESHSALE set Posted='Y', " & _
            "Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "',Modify_By='" + objCommonVar.CurrentUserCode + "', " & _
            "Modified_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "' " & _
            " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsDispatchFreshSaleDetail
#Region "Variable"
    Public Document_No As String = Nothing
    Public Line_No As Integer
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_code As String = Nothing
    Public DeliveryQty As Double = 0
    Public DispatchQty As Double = 0
    Public Rate As Double = 0
    Public Amount As Double = 0
    Public SchemeType As String = Nothing
    Public Scheme_Applicable As String = Nothing
    Public Scheme_Item As String = Nothing
    Public Scheme_Code As String = Nothing
    Public MainItem_Code As String = Nothing
    Public MainItem_Unit_code As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsDispatchFreshSaleDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsDispatchFreshSaleDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "DeliveryQty", obj.DeliveryQty)
                clsCommon.AddColumnsForChange(coll, "DispatchQty", obj.DispatchQty)
                clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "SchemeType", obj.SchemeType)
                clsCommon.AddColumnsForChange(coll, "Scheme_Applicable", obj.Scheme_Applicable)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item", obj.Scheme_Item)
                clsCommon.AddColumnsForChange(coll, "Scheme_Code", obj.Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "MainItem_Code", obj.MainItem_Code)
                clsCommon.AddColumnsForChange(coll, "MainItem_Unit_code", obj.MainItem_Unit_code)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_dispatch_detail_freshsale", OMInsertOrUpdate.Insert, "", trans)

            Next

        End If
        Return True
    End Function
End Class

