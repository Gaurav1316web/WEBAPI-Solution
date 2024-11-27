Imports System.Data.SqlClient

Public Class clsProductDemandBookingSale
#Region "Variable"
    Dim qry As String
    Public Document_No As String = Nothing
    Public Document_Date As String = Nothing
    Public Location_Code As String = Nothing
    Public City_Code As String = Nothing
    Public Posted As Integer = 0
    Public IsIndividualCustomer As Integer = 0
    Public Posting_Date As DateTime? = Nothing
    'Public Price_code As String
    Public Route_No As String = Nothing
    'Public ShiftType As String = String.Empty
    Public ItemType As String = String.Empty
    'Public TripNo As String = String.Empty
    'Public TotalQtyInCrates As Decimal = 0
    'Public TotalQtyInLtr As Decimal = 0
    Public DocumentAmount As Decimal = 0
    'Public Posted_Morning As Integer = 0
    'Public Posted_Evening As Integer = 0
    'Public UploderDocNo As String = String.Empty
    Public Arr As List(Of clsProductDemandBookingSaleDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsProductDemandBookingSale, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, False, trans)
            '' Is Document posted then roll back the transcation by Vinod Kumar14/Aug/2024
            If clsCommon.myLen(obj.Document_No) > 0 Then
                qry = "select Posted from TSPL_PRODUCT_DEMAND_BOOKING_MASTER where Document_No='" + obj.Document_No + "'"
                Dim isPosted As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                If isPosted = 1 Then
                    Throw New Exception("Document Already Posted.")
                End If
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As clsProductDemandBookingSale, ByVal isNewEntry As Boolean, ByVal IsDemandUploader As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim ShiftType As String = ""
            Dim isBoothReset As Boolean = False
            Dim qry As String = ""


            qry = "delete from TSPL_PRODUCT_DEMAND_BOOKING_DETAIL where tr_code in (select tr_code from TSPL_PRODUCT_DEMAND_BOOKING_DETAIL where Document_No='" + obj.Document_No + "') "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmDemandBooking, obj.Location_Code, obj.Document_Date, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "City_Code", obj.City_Code)
            'clsCommon.AddColumnsForChange(coll, "ShiftType", obj.ShiftType)
            clsCommon.AddColumnsForChange(coll, "ItemType", obj.ItemType)
            clsCommon.AddColumnsForChange(coll, "IsIndividualCustomer", obj.IsIndividualCustomer)
            'clsCommon.AddColumnsForChange(coll, "TotalQtyInCrates", obj.TotalQtyInCrates)
            'clsCommon.AddColumnsForChange(coll, "TotalQtyInLtr", obj.TotalQtyInLtr)
            clsCommon.AddColumnsForChange(coll, "DocumentAmount", obj.DocumentAmount)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmProductDemandBooking, "", obj.Route_No, False, True, False, False, False, True)

                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRODUCT_DEMAND_BOOKING_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRODUCT_DEMAND_BOOKING_MASTER", OMInsertOrUpdate.Update, "TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_No='" + obj.Document_No + "'", trans)
            End If
            clsProductDemandBookingSaleDetail.SaveData(obj.Document_No, obj.Document_Date, obj.Arr, trans, obj.Location_Code, isNewEntry, obj.Route_No)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_PRODUCT_DEMAND_BOOKING_MASTER", "Document_No", "TSPL_Product_DEMAND_BOOKING_DETAIL", "Document_No", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsProductDemandBookingSale
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsProductDemandBookingSale
        Dim obj As clsProductDemandBookingSale = Nothing
        Dim qry = "SELECT  TSPL_Product_DEMAND_BOOKING_MASTER.*  FROM TSPL_Product_DEMAND_BOOKING_MASTER where 2=2 "
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Product_DEMAND_BOOKING_MASTER.Document_No = (select MIN(Document_No) from TSPL_Product_DEMAND_BOOKING_MASTER WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_Product_DEMAND_BOOKING_MASTER.Document_No = (select Max(Document_No) from TSPL_Product_DEMAND_BOOKING_MASTER WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_Product_DEMAND_BOOKING_MASTER.Document_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_Product_DEMAND_BOOKING_MASTER.Document_No = (select Min(Document_No) from TSPL_Product_DEMAND_BOOKING_MASTER where Document_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_Product_DEMAND_BOOKING_MASTER.Document_No = (select Max(Document_No) from TSPL_Product_DEMAND_BOOKING_MASTER where Document_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsProductDemandBookingSale()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.City_Code = clsCommon.myCstr(dt.Rows(0)("City_Code"))
            obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            'obj.ShiftType = clsCommon.myCstr(dt.Rows(0)("ShiftType"))
            obj.ItemType = clsCommon.myCstr(dt.Rows(0)("ItemType"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            'obj.Posted_Morning = clsCommon.myCdbl(dt.Rows(0)("Posted_Morning"))
            'obj.Posted_Evening = clsCommon.myCdbl(dt.Rows(0)("Posted_Evening"))
            'obj.TripNo = clsCommon.myCstr(dt.Rows(0)("TripNo"))
            obj.IsIndividualCustomer = clsCommon.myCdbl(dt.Rows(0)("IsIndividualCustomer"))
            'obj.TotalQtyInCrates = clsCommon.myCdbl(dt.Rows(0)("TotalQtyInCrates"))
            obj.DocumentAmount = clsCommon.myCdbl(dt.Rows(0)("DocumentAmount"))
            'obj.TotalQtyInLtr = clsCommon.myCdbl(dt.Rows(0)("TotalQtyInLtr"))
            'obj.UploderDocNo = clsCommon.myCdbl(dt.Rows(0)("UploderDocNo"))
            'If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
            '    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            'End If
            qry = "SELECT TSPL_Product_DEMAND_BOOKING_DETAIL.*,tspl_item_master.Item_Desc FROM TSPL_Product_DEMAND_BOOKING_DETAIL left outer join tspl_item_master on  " &
                "TSPL_Product_DEMAND_BOOKING_DETAIL.item_code=tspl_item_master.item_code where Document_No='" & obj.Document_No & "' order by TSPL_Product_DEMAND_BOOKING_DETAIL.Cust_Code asc"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsProductDemandBookingSaleDetail)
                Dim objTr As clsProductDemandBookingSaleDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsProductDemandBookingSaleDetail
                    objTr.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                    objTr.Created_By = clsCommon.myCstr(dr("Created_By"))
                    objTr.Vehicle_Code = clsCommon.myCstr(dr("Vehicle_Code"))
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    'objTr.Trip_No = clsCommon.myCdbl(dr("Trip_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.Rate = clsCommon.myCdbl(dr("Item_Rate"))
                    objTr.ItemNetAmount = clsCommon.myCdbl(dr("ItemNetAmount"))
                    objTr.Price_Code = clsCommon.myCstr(dr("Price_Code"))
                    'objTr.ShiftType = clsCommon.myCstr(dr("ShiftType"))
                    objTr.IsItemUpdate = clsCommon.myCdbl(dr("IsItemUpdate"))
                    'objTr.TotalCrates_ItemWise = clsCommon.myCdbl(dr("TotalCrates_ItemWise"))
                    'objTr.TotalLtr_ItemWise = clsCommon.myCdbl(dr("TotalLtr_ItemWise"))
                    'objTr.IsTruckSheetGenerated = clsCommon.myCstr(dr("IsTruckSheetGenerated"))
                    'objTr.IsGatePassGenerated = clsCommon.myCstr(dr("IsGatePassGenerated"))
                    objTr.TAX_Group = clsCommon.myCstr(dr("TAX_Group"))
                    objTr.TAX1 = clsCommon.myCstr(dr("TAX1"))
                    objTr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                    objTr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                    objTr.TAX1_Base_Amt = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                    objTr.TAX2 = clsCommon.myCstr(dr("Tax2"))
                    objTr.TAX2_Amt = clsCommon.myCdbl(dr("Tax2_Amt"))
                    objTr.TAX2_Rate = clsCommon.myCdbl(dr("Tax2_Rate"))
                    objTr.TAX2_Base_Amt = clsCommon.myCdbl(dr("Tax2_Base_Amt"))
                    objTr.TAX3 = clsCommon.myCstr(dr("Tax3"))
                    objTr.TAX3_Amt = clsCommon.myCdbl(dr("Tax3_Amt"))
                    objTr.TAX3_Rate = clsCommon.myCdbl(dr("Tax3_Rate"))
                    objTr.TAX3_Base_Amt = clsCommon.myCdbl(dr("Tax3_Base_Amt"))
                    objTr.TAX4 = clsCommon.myCstr(dr("Tax4"))
                    objTr.TAX4_Amt = clsCommon.myCdbl(dr("Tax4_Amt"))
                    objTr.TAX4_Rate = clsCommon.myCdbl(dr("Tax4_Rate"))
                    objTr.TAX4_Base_Amt = clsCommon.myCdbl(dr("Tax4_Base_Amt"))
                    objTr.TAX5 = clsCommon.myCstr(dr("Tax5"))
                    objTr.TAX5_Amt = clsCommon.myCdbl(dr("Tax5_Amt"))
                    objTr.TAX5_Rate = clsCommon.myCdbl(dr("Tax5_Rate"))
                    objTr.TAX5_Base_Amt = clsCommon.myCdbl(dr("Tax5_Base_Amt"))
                    objTr.TAX6 = clsCommon.myCstr(dr("Tax6"))
                    objTr.TAX6_Amt = clsCommon.myCdbl(dr("Tax6_Amt"))
                    objTr.TAX6_Rate = clsCommon.myCdbl(dr("Tax6_Rate"))
                    objTr.TAX6_Base_Amt = clsCommon.myCdbl(dr("Tax6_Base_Amt"))
                    objTr.TAX7 = clsCommon.myCstr(dr("Tax7"))
                    objTr.TAX7_Amt = clsCommon.myCdbl(dr("Tax7_Amt"))
                    objTr.TAX7_Rate = clsCommon.myCdbl(dr("Tax7_Rate"))
                    objTr.TAX7_Base_Amt = clsCommon.myCdbl(dr("Tax7_Base_Amt"))
                    objTr.TAX8 = clsCommon.myCstr(dr("Tax8"))
                    objTr.TAX8_Amt = clsCommon.myCdbl(dr("Tax8_Amt"))
                    objTr.TAX8_Rate = clsCommon.myCdbl(dr("Tax8_Rate"))
                    objTr.TAX8_Base_Amt = clsCommon.myCdbl(dr("Tax8_Base_Amt"))
                    objTr.TAX9 = clsCommon.myCstr(dr("Tax9"))
                    objTr.TAX9_Amt = clsCommon.myCdbl(dr("Tax9_Amt"))
                    objTr.TAX9_Rate = clsCommon.myCdbl(dr("Tax9_Rate"))
                    objTr.TAX9_Base_Amt = clsCommon.myCdbl(dr("Tax9_Base_Amt"))
                    objTr.TAX10 = clsCommon.myCstr(dr("Tax10"))
                    objTr.TAX10_Amt = clsCommon.myCdbl(dr("Tax10_Amt"))
                    objTr.TAX10_Rate = clsCommon.myCdbl(dr("Tax10_Rate"))
                    objTr.TAX10_Base_Amt = clsCommon.myCdbl(dr("Tax10_Base_Amt"))



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
        If clsCommon.myLen(clsCommon.myCstr(strCode)) > 0 Then
            'Dim strDocNoForGatePassOrTrucksheetGeneratedMorning As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Document_No  from TSPL_PRODUCT_DEMAND_BOOKING_DETAIL where document_No='" & strCode & "'  "))
            'Dim strDocNoForGatePassOrTrucksheetGeneratedEvening As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Document_No  from TSPL_PRODUCT_DEMAND_BOOKING_DETAIL where document_No='" & strCode & "' "))
            'If clsCommon.myLen(clsCommon.myCstr(strDocNoForGatePassOrTrucksheetGeneratedMorning)) > 0 OrElse clsCommon.myLen(clsCommon.myCstr(strDocNoForGatePassOrTrucksheetGeneratedEvening)) > 0 Then
            '    Throw New Exception("Demand cannot be deleted because its Gate Pass/Trucksheet has generated")
            'End If
        End If
        Dim obj As clsProductDemandBookingSale = clsProductDemandBookingSale.GetData(strCode, NavigatorType.Current)
        'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Dairy Sale", "Demand Booking", obj.Location_Code, obj.Document_Date, Nothing)
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmbookingdairy, obj.Location_Code, obj.Document_Date, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try
                Dim qry As String = "select * from  TSPL_PRODUCT_DEMAND_BOOKING_DETAIL where Document_No='" + strCode + "'"

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                Dim obj1 As List(Of clsProductDemandBookingSaleDetail) = New List(Of clsProductDemandBookingSaleDetail)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        Dim objtr As clsProductDemandBookingSaleDetail = New clsProductDemandBookingSaleDetail()
                        objtr.Cust_Code = clsCommon.myCstr(dr("cust_code"))
                        objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objtr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                        objtr.Price_Code = clsCommon.myCstr(dr("Price_Code"))
                        objtr.Vehicle_Code = clsCommon.myCstr(dr("Vehicle_Code"))
                        objtr.Document_No = clsCommon.myCstr(dr("Document_No"))
                        objtr.Qty = 0
                        objtr.ItemNetAmount = 0
                        objtr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                        'objtr.Is_Posted = clsCommon.myCstr(dr("Is_Posted"))
                        objtr.TAX_Group = clsCommon.myCstr(dr("TAX_Group"))
                        'objtr.ShiftType = clsCommon.myCstr(dr("ShiftType"))
                        obj1.Add(objtr)

                    Next
                    qry = "delete from TSPL_PRODUCT_DEMAND_BOOKING_DETAIL where Document_No='" + strCode + "'"
                    isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    clsProductDemandBookingSaleDetail.SaveDeleteData(obj.Document_No, obj.Document_Date, obj1, trans, obj.Location_Code, False, obj.Route_No)
                End If

                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_Product_DEMAND_BOOKING_MASTER", "Document_No", "TSPL_Product_DEMAND_BOOKING_DETAIL", "Document_No", trans)


                qry = "delete from TSPL_PRODUCT_DEMAND_BOOKING_DETAIL where Document_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                'clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_DEMAND_BOOKING_DETAIL", "Document_No", trans)

                qry = "delete from TSPL_Product_DEMAND_BOOKING_MASTER where Document_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                'clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_DEMAND_BOOKING_MASTER", "Document_No", "TSPL_DEMAND_BOOKING_DETAIL", "Document_No", trans)

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
    Public Shared Function DeleteBoothDemand(ByVal DocNo As String, ByVal cust_code As String, ByVal ResetDemandOnSave As Boolean) As Boolean
        Dim obj As clsProductDemandBookingSale = clsProductDemandBookingSale.GetData(DocNo, NavigatorType.Current)

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = ""
            Dim strDocDate As DateTime = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Document_Date from TSPL_Product_Demand_Booking_Master where  Document_No='" + DocNo + "'", trans))

            If Not ResetDemandOnSave Then

                qry = "select * from  TSPL_Product_DEMAND_BOOKING_DETAIL where TR_Code in (select tr_code from TSPL_Product_DEMAND_BOOKING_DETAIL where Document_No='" + DocNo + "'  and Cust_Code ='" + cust_code + "')"

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                Dim obj1 As List(Of clsProductDemandBookingSaleDetail) = New List(Of clsProductDemandBookingSaleDetail)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        Dim objtr As clsProductDemandBookingSaleDetail = New clsProductDemandBookingSaleDetail()
                        objtr.Cust_Code = clsCommon.myCstr(dr("cust_code"))
                        objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objtr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                        objtr.Price_Code = clsCommon.myCstr(dr("Price_Code"))
                        objtr.Vehicle_Code = clsCommon.myCstr(dr("Vehicle_Code"))
                        objtr.Document_No = clsCommon.myCstr(dr("Document_No"))
                        objtr.Qty = 0
                        objtr.ItemNetAmount = 0
                        objtr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                        'objtr.Is_Posted = clsCommon.myCstr(dr("Is_Posted"))
                        objtr.TAX_Group = clsCommon.myCstr(dr("TAX_Group"))
                        'objtr.ShiftType = clsCommon.myCstr(dr("ShiftType"))
                        obj1.Add(objtr)

                    Next
                    qry = "delete from TSPL_Product_DEMAND_BOOKING_DETAIL where TR_Code in (select tr_code from TSPL_Product_DEMAND_BOOKING_DETAIL where Document_No='" + DocNo + "'  and Cust_Code ='" + cust_code + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    clsProductDemandBookingSaleDetail.SaveDeleteData(obj.Document_No, obj.Document_Date, obj1, trans, obj.Location_Code, False, obj.Route_No)
                End If

                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_Product_DEMAND_BOOKING_MASTER", "Document_No", "TSPL_Product_DEMAND_BOOKING_DETAIL", "Document_No", trans)



                qry = "select tr_code from TSPL_Product_DEMAND_BOOKING_DETAIL where Document_No='" + DocNo + "' and Cust_Code ='" + cust_code + "'"
                Dim dtDetail As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtDetail IsNot Nothing AndAlso dtDetail.Rows.Count > 0 Then

                    For Each drDetail As DataRow In dtDetail.Rows
                        'clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(drDetail("tr_code")), "TSPL_DEMAND_BOOKING_DETAIL", "tr_code", trans)

                        qry = "delete from TSPL_Product_DEMAND_BOOKING_DETAIL where tr_code='" + clsCommon.myCstr(drDetail("tr_code")) + "' "
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    Next
                    dtDetail = Nothing
                End If
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            PostData(FormId, strDocNo, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_Product_DEMAND_BOOKING_MASTER", "Document_No", "TSPL_Product_DEMAND_BOOKING_DETAIL", "Document_No", trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Demand Booking No not found to Post")
            End If

            Dim obj As clsProductDemandBookingSale = clsProductDemandBookingSale.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmbookingdairy, obj.Location_Code, obj.Document_Date, trans)

            If obj.Posted = 1 Then
                Throw New Exception("Docuemnt is already posted")

            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmDemandBooking, obj.Location_Code, obj.Document_Date, trans)
            Dim coll As New Hashtable()

            Dim dtNow As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt")
            coll = New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Posted", 1)
            clsCommon.AddColumnsForChange(coll, "Posting_Date", dtNow)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Product_DEMAND_BOOKING_MASTER", OMInsertOrUpdate.Update, "TSPL_Product_DEMAND_BOOKING_MASTER.Document_No='" + obj.Document_No + "'", trans)

            'If IsRepertOrder Then
            '    Dim docno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_Product_DEMAND_BOOKING_MASTER where convert(date,Document_Date,103)='" + clsCommon.GetPrintDate(clsCommon.myCDate(obj.Document_Date).AddDays(1)) + "' and Route_No='" + obj.Route_No + "' and IsIndividualCustomer=0", trans))
            '    Dim isNewEntry As Boolean = False
            '    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Posted from TSPL_Product_DEMAND_BOOKING_MASTER where Document_No='" + docno + "' ", trans)) = 0 Then
            '        If clsCommon.myLen(docno) > 0 Then
            '            obj.Document_No = docno
            '            obj.Document_Date = clsCommon.GetPrintDate(clsCommon.myCDate(obj.Document_Date).AddDays(1))
            '            isNewEntry = False
            '        Else
            '            obj.Document_No = ""
            '            obj.Document_Date = clsCommon.GetPrintDate(clsCommon.myCDate(obj.Document_Date).AddDays(1))
            '            isNewEntry = True
            '        End If
            '        If clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ApplyDemandAll, clsFixedParameterCode.ApplyDemandAll, trans)) = 1 Then
            '            SaveData(obj, isNewEntry, False, trans)
            '        ElseIf clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ApplyDemandCustomerWise, clsFixedParameterCode.ApplyDemandCustomerWise, trans)) = 1 Then

            '            For ii As Integer = obj.Arr.Count - 1 To 0 Step -1
            '                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsReorder  from TSPL_CUSTOMER_MASTER where Cust_Code='" & obj.Arr(ii).Cust_Code & "'", trans)) = 0 OrElse obj.IsIndividualCustomer = 1 Then
            '                    obj.Arr.RemoveAt(ii)
            '                Else
            '                    If Not isNewEntry Then
            '                        obj.Arr(ii).CustomerReorderCheck = True
            '                    End If
            '                End If
            '            Next
            '            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
            '                SaveData(obj, isNewEntry, False, trans)
            '            End If

            '        End If
            '    End If
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim Qry As String = "select Posted from TSPL_Product_Demand_BOOKING_MAstER where Document_No='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If
            Dim dt As DataTable = Nothing
            '' to check gatepass or truck sheet generated

            Dim strCount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select count( DOCUMENT_CODE) from TSPL_SD_SHIPMENT_BOOKING_DETAIL Where Booking_TR_Code in (select tr_code from TSPL_Product_DEMAND_BOOKING_DETAIL where Document_No='" + strCode + "') ", trans))
            If strCount > 0 Then
                Throw New Exception("Demand cannot be reverse because its Dispatch has generated.")
            End If

            Qry = "Update TSPL_Product_Demand_BOOKING_Master set Posted = 0 where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsProductDemandBookingSaleDetail
#Region "Variable"
    Public Document_No As String = Nothing
    Public Line_No As Integer
    'Public Trip_No As Integer
    Public Item_Code As String = Nothing
    Public Cust_Code As String = Nothing
    Public Created_By As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_code As String = Nothing
    'Public TotalCrates_ItemWise As Decimal = 0
    'Public TotalLtr_ItemWise As Decimal = 0
    Public ItemNetAmount As Decimal = 0
    'Public IsGatePassGenerated As String = "N"
    'Public IsTruckSheetGenerated As String = "N"
    'Public Is_Posted As String = Nothing
    Public Qty As Double = 0
    Public Rate As Double = 0
    Public Price_Code As String = Nothing
    Public Vehicle_Code As String = ""
    'Public ShiftType As String = ""
    Public TR_CODE As String = Nothing
    Public IsItemUpdate As Integer = 0
    'Public CustomerReorderCheck As Boolean = False
    Public TAX_Group As String = ""
    Public TAX1 As String = Nothing
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Rate As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Rate As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Rate As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Rate As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Rate As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Base_Amt As Double = 0
    Public TAX6_Rate As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Base_Amt As Double = 0
    Public TAX7_Rate As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Base_Amt As Double = 0
    Public TAX8_Rate As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Base_Amt As Double = 0
    Public TAX9_Rate As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Base_Amt As Double = 0
    Public TAX10_Rate As Double = 0
    Public TAX10_Amt As Double = 0
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal DocDate As Date, ByVal Arr As List(Of clsProductDemandBookingSaleDetail), ByVal trans As SqlTransaction, ByVal strLocCode As String, ByVal isNewEntry As Boolean, ByVal strRouteNo As String) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsProductDemandBookingSaleDetail In Arr

                If obj.Qty > 0 Then
                    Dim coll As New Hashtable()

                    obj.TR_CODE = clsERPFuncationality.GetNextCode(trans, DocDate, clsDocType.DetailSale, clsDocTransactionType.ProductDetail, strRouteNo, False, True, False, False, False, True)

                    clsCommon.AddColumnsForChange(coll, "TR_CODE", obj.TR_CODE)
                    clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    'clsCommon.AddColumnsForChange(coll, "Trip_No", obj.Trip_No)
                    clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                    clsCommon.AddColumnsForChange(coll, "Created_By", obj.Created_By)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "Item_Rate", obj.Rate)
                    clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
                    clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
                    'clsCommon.AddColumnsForChange(coll, "ShiftType", obj.ShiftType)
                    clsCommon.AddColumnsForChange(coll, "IsItemUpdate", obj.IsItemUpdate)
                    'clsCommon.AddColumnsForChange(coll, "TotalCrates_ItemWise", obj.TotalCrates_ItemWise)
                    'clsCommon.AddColumnsForChange(coll, "TotalLtr_ItemWise", obj.TotalLtr_ItemWise)
                    clsCommon.AddColumnsForChange(coll, "ItemNetAmount", obj.ItemNetAmount)
                    'clsCommon.AddColumnsForChange(coll, "IsGatePassGenerated", obj.IsGatePassGenerated)
                    'clsCommon.AddColumnsForChange(coll, "IsTruckSheetGenerated", obj.IsTruckSheetGenerated)
                    clsCommon.AddColumnsForChange(coll, "TAX_Group", obj.TAX_Group)
                    clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRODUCT_DEMAND_BOOKING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                End If
            Next
        End If
        Return True
    End Function
    Public Shared Function SaveDeleteData(ByVal strDocNo As String, ByVal DocDate As Date, ByVal Arr As List(Of clsProductDemandBookingSaleDetail), ByVal trans As SqlTransaction, ByVal strLocCode As String, ByVal isNewEntry As Boolean, ByVal strRouteNo As String) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsProductDemandBookingSaleDetail In Arr


                Dim coll As New Hashtable()

                obj.TR_CODE = clsERPFuncationality.GetNextCode(trans, DocDate, clsDocType.DetailSale, clsDocTransactionType.ProductDetail, strRouteNo, False, True, False, False, False, True)
                clsCommon.AddColumnsForChange(coll, "TR_CODE", obj.TR_CODE)
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                'clsCommon.AddColumnsForChange(coll, "Trip_No", obj.Trip_No)
                clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", obj.Created_By)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Item_Rate", obj.Rate)
                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
                clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
                ' clsCommon.AddColumnsForChange(coll, "ShiftType", obj.ShiftType)
                clsCommon.AddColumnsForChange(coll, "IsItemUpdate", obj.IsItemUpdate)
                'clsCommon.AddColumnsForChange(coll, "TotalCrates_ItemWise", obj.TotalCrates_ItemWise)
                'clsCommon.AddColumnsForChange(coll, "TotalLtr_ItemWise", obj.TotalLtr_ItemWise)
                clsCommon.AddColumnsForChange(coll, "ItemNetAmount", obj.ItemNetAmount)
                'clsCommon.AddColumnsForChange(coll, "IsGatePassGenerated", obj.IsGatePassGenerated)
                'clsCommon.AddColumnsForChange(coll, "IsTruckSheetGenerated", obj.IsTruckSheetGenerated)
                clsCommon.AddColumnsForChange(coll, "TAX_Group", obj.TAX_Group)
                clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
                clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
                clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
                clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
                clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
                clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Product_DEMAND_BOOKING_DETAIL", OMInsertOrUpdate.Insert, "", trans)

            Next
        End If
        Return True
    End Function
End Class
