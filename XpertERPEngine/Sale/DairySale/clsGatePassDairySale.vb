Imports common
Imports System.Data.SqlClient
Public Class clsGatePassDairySale

#Region "Variable"
    Dim qry As String
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Sampling As Integer = 0
    Public Document_No As String = Nothing
    Public Document_Date As String = Nothing
    Public Customer_Code As String = Nothing
    Public Location_Code As String = Nothing
    Public Delivery_Code As String = Nothing
    Public Delivery_Date As Date?
    Public Vehicle_Capacity As Double = 0
    Public Vehicle_Code As String = Nothing
    Public Road_Permit_No As String = Nothing
    Public Transporter_Name As String = Nothing
    Public Freight As String = Nothing
    Public Freight_Amount As Double = 0
    Public Posted As Integer = 0
    Public OnHold As String = Nothing
    Public Comments As String = Nothing
    Public Posting_Date As DateTime? = Nothing
    Public Total_Amt As Double = 0
    Public Short_Close As String = Nothing
    Public Approval_Reqd As String = Nothing
    Public CreditApproval_Reqd As String = Nothing
    Public Is_Credit_Approved As Integer = 0
    Public Is_Approved As String = Nothing
    Public Form_ID As String = ""
    Public Credit_Limit As Double = 0
    Public Price_code As String
    Public Route_No As String = Nothing
    Public Arr As List(Of clsGatePassDairySaleDetail) = Nothing
    Public ArrInvoice As List(Of clsGatePassDairyMultiBooking) = Nothing

#End Region
    Public Enum EnumStatusType
        Open = 1
        Pending = 2
        Approved = 3
        Posted = 4
    End Enum
    Public Function SaveData(ByVal obj As clsGatePassDairySale, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsGatePassDairySale, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim isSaved As Boolean = True

            qry = "delete from TSPL_GATEPASS_DETAIL_DAIRYSALE where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MULTI_BOOKING_DETAIL where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmDairySaleGatePass, "", obj.Location_Code)
            End If

            If (clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmGatePassDairy, obj.Location_Code, obj.Document_Date, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            'clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Delivery_Code", obj.Delivery_Code, True)

            If obj.Delivery_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Delivery_Date", clsCommon.GetPrintDate(obj.Delivery_Date, "dd/MMM/yyyy hh:mm tt"))
            End If


            clsCommon.AddColumnsForChange(coll, "Vehicle_Capacity", obj.Vehicle_Capacity)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code, True)
            clsCommon.AddColumnsForChange(coll, "Road_Permit_No", obj.Road_Permit_No)
            clsCommon.AddColumnsForChange(coll, "Transporter_Name", obj.Transporter_Name)
            clsCommon.AddColumnsForChange(coll, "Freight", obj.Freight)
            clsCommon.AddColumnsForChange(coll, "Freight_Amount", obj.Freight_Amount)
            clsCommon.AddColumnsForChange(coll, "Posted", 0)
            clsCommon.AddColumnsForChange(coll, "Status", 0)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "OnHold", obj.OnHold)
            clsCommon.AddColumnsForChange(coll, "Short_Close", obj.Short_Close)
            clsCommon.AddColumnsForChange(coll, "Price_code", obj.Price_code)
            clsCommon.AddColumnsForChange(coll, "Approval_Reqd", 0)
            clsCommon.AddColumnsForChange(coll, "Is_Approved", 0)
            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No, True)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Sampling", 0)
            If isNewEntry Then
                ''richa agarwal 06 may,2016 check entry into do
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "CreditApproval_Reqd", obj.CreditApproval_Reqd)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GATEPASS_MASTER_DAIRYSALE", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GATEPASS_MASTER_DAIRYSALE", OMInsertOrUpdate.Update, "TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No='" + obj.Document_No + "'", trans)

            End If

            isSaved = isSaved AndAlso clsGatePassDairySaleDetail.SaveData(obj.Document_No, Arr, trans, obj.Customer_Code)
            isSaved = isSaved AndAlso clsGatePassDairyMultiBooking.SaveData(obj.Document_No, ArrInvoice, trans)


            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsGatePassDairySale
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsGatePassDairySale
        Dim obj As clsGatePassDairySale = Nothing
        Dim qry = "SELECT  TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No, TSPL_GATEPASS_MASTER_DAIRYSALE.Document_Date, " & _
                      "TSPL_GATEPASS_MASTER_DAIRYSALE.Status, TSPL_GATEPASS_MASTER_DAIRYSALE.Customer_Code, " & _
                      "TSPL_GATEPASS_MASTER_DAIRYSALE.Location_Code, TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code, " & _
                      "TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Date, TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Capacity, " & _
                      "TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code, TSPL_GATEPASS_MASTER_DAIRYSALE.Road_Permit_No, " & _
                      "TSPL_GATEPASS_MASTER_DAIRYSALE.Transporter_Name, TSPL_GATEPASS_MASTER_DAIRYSALE.Freight, " & _
                      "TSPL_GATEPASS_MASTER_DAIRYSALE.Freight_Amount, TSPL_GATEPASS_MASTER_DAIRYSALE.Posted, " & _
                      "TSPL_GATEPASS_MASTER_DAIRYSALE.Comments,TSPL_GATEPASS_MASTER_DAIRYSALE.Total_Amt, " & _
                      "TSPL_GATEPASS_MASTER_DAIRYSALE.Posting_Date,TSPL_GATEPASS_MASTER_DAIRYSALE.OnHold, " & _
                      "TSPL_GATEPASS_MASTER_DAIRYSALE.Short_Close,TSPL_GATEPASS_MASTER_DAIRYSALE.Approval_Reqd, " & _
                      "TSPL_GATEPASS_MASTER_DAIRYSALE.Price_code,TSPL_GATEPASS_MASTER_DAIRYSALE.Is_Approved, " & _
                      "TSPL_GATEPASS_MASTER_DAIRYSALE.CreditApproval_Reqd,TSPL_GATEPASS_MASTER_DAIRYSALE.Is_Credit_Approved " & _
                      ",TSPL_GATEPASS_MASTER_DAIRYSALE.Route_No " & _
                      "FROM TSPL_GATEPASS_MASTER_DAIRYSALE where 2=2 "
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No = (select MIN(Document_No) from TSPL_GATEPASS_MASTER_DAIRYSALE WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No = (select Max(Document_No) from TSPL_GATEPASS_MASTER_DAIRYSALE WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No = (select Min(Document_No) from TSPL_GATEPASS_MASTER_DAIRYSALE where Document_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No = (select Max(Document_No) from TSPL_GATEPASS_MASTER_DAIRYSALE where Document_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsGatePassDairySale()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Delivery_Code = clsCommon.myCstr(dt.Rows(0)("Delivery_Code"))
            obj.Delivery_Date = clsCommon.myCDate(dt.Rows(0)("Delivery_Date"))
            obj.Vehicle_Capacity = clsCommon.myCdbl(dt.Rows(0)("Vehicle_Capacity"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.Road_Permit_No = clsCommon.myCstr(dt.Rows(0)("Road_Permit_No"))
            obj.Transporter_Name = clsCommon.myCstr(dt.Rows(0)("Transporter_Name"))
            obj.Freight = clsCommon.myCstr(dt.Rows(0)("Freight"))
            obj.Freight_Amount = clsCommon.myCdbl(dt.Rows(0)("Freight_Amount"))
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Price_code = clsCommon.myCstr(dt.Rows(0)("Price_code"))
            obj.OnHold = clsCommon.myCstr(dt.Rows(0)("OnHold"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If
            obj.Short_Close = clsCommon.myCstr(dt.Rows(0)("Short_Close"))
            obj.Approval_Reqd = clsCommon.myCstr(dt.Rows(0)("Approval_Reqd"))
            obj.Is_Approved = clsCommon.myCstr(dt.Rows(0)("Is_Approved"))
            'obj.Status = clsCommon.myCdbl(dt.Rows(0)("Status"))
            obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            obj.CreditApproval_Reqd = clsCommon.myCstr(dt.Rows(0)("CreditApproval_Reqd"))
            obj.Is_Credit_Approved = clsCommon.myCdbl(dt.Rows(0)("Is_Credit_Approved"))
            qry = "SELECT TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Code as 'Scheme Code',TSPL_GATEPASS_DETAIL_DAIRYSALE.OrgUnit_code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Balance_Qty,TSPL_GATEPASS_DETAIL_DAIRYSALE.DOQty,TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No, " & _
                "TSPL_GATEPASS_DETAIL_DAIRYSALE.Line_No, TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Item, " & _
                "TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code, TSPL_GATEPASS_DETAIL_DAIRYSALE.Qty, TSPL_GATEPASS_DETAIL_DAIRYSALE.Rate, " & _
                "TSPL_GATEPASS_DETAIL_DAIRYSALE.Amount,tspl_item_master.Item_Desc,TSPL_GATEPASS_DETAIL_DAIRYSALE.MRP,TSPL_GATEPASS_DETAIL_DAIRYSALE.Conv_Factor " & _
                ",TSPL_GATEPASS_DETAIL_DAIRYSALE.Price_Code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Price_Date,TSPL_GATEPASS_DETAIL_DAIRYSALE.OrgRate,isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Type,'') as 'Scheme Type',isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Qty,'') as 'Scheme Item Qty',isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Item_UOm,'') as 'Scheme Item UOM',isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Item_Code,'') as 'Scheme Item Code',isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Code,'') as 'Scheme Code',isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item,'0') as 'FOC' FROM TSPL_GATEPASS_DETAIL_DAIRYSALE left outer join tspl_item_master on  " & _
                "TSPL_GATEPASS_DETAIL_DAIRYSALE.item_code=tspl_item_master.item_code where  Document_No='" & obj.Document_No & "' order by TSPL_GATEPASS_DETAIL_DAIRYSALE.Line_No "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsGatePassDairySaleDetail)
                Dim objTr As clsGatePassDairySaleDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsGatePassDairySaleDetail
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.Rate = clsCommon.myCdbl(dr("Rate"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                    objTr.Conv_Factor = clsCommon.myCdbl(dr("Conv_Factor"))
                    objTr.Price_Code = clsCommon.myCstr(dr("Price_Code"))
                    If dr("Price_Date") IsNot DBNull.Value Then
                        objTr.Price_Date = clsCommon.myCDate(dr("Price_Date"))
                    End If
                    objTr.OrgRate = clsCommon.myCdbl(dr("OrgRate"))
                    objTr.Delivery_Code = clsCommon.myCstr(dr("Delivery_Code"))
                    objTr.DOQty = clsCommon.myCdbl(dr("DOQty"))
                    objTr.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
                    objTr.OrgUnit_code = clsCommon.myCstr(dr("OrgUnit_code"))
                    objTr.Scheme_Code = clsCommon.myCstr(dr("Scheme Code"))

                    objTr.Scheme_Type = clsCommon.myCstr(dr("Scheme Type"))
                    objTr.Scheme_Code = clsCommon.myCstr(dr("Scheme Code"))
                    objTr.Scheme_Item_Code = clsCommon.myCstr(dr("Scheme Item Code"))
                    objTr.Scheme_Item_UOM = clsCommon.myCstr(dr("Scheme Item UOM"))
                    objTr.Scheme_Qty = clsCommon.myCstr(dr("Scheme Item Qty"))
                    objTr.FOC_Item = clsCommon.myCdbl(dr("FOC"))
                    objTr.Scheme_Item = clsCommon.myCstr(dr("Scheme_Item"))
                    obj.Arr.Add(objTr)
                Next
            End If
            qry = "select * from TSPL_MULTI_BOOKING_DETAIL "
            qry += " where TSPL_MULTI_BOOKING_DETAIL.Document_No='" + obj.Document_No + "' "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrInvoice = New List(Of clsGatePassDairyMultiBooking)
                Dim objTrTr As clsGatePassDairyMultiBooking
                For Each dr As DataRow In dt.Rows
                    objTrTr = New clsGatePassDairyMultiBooking

                    objTrTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTrTr.Booking_No = clsCommon.myCstr(dr("Booking_No"))
                    obj.ArrInvoice.Add(objTrTr)
                Next
            End If
        End If
        Return obj
    End Function
    Public Shared Function GetItemDetailData(ByVal StrCode As String, ByVal trans As SqlTransaction) As clsGatePassDairySale
        Dim obj As clsGatePassDairySale = Nothing
        Dim qry As String = "SELECT TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Code as 'Scheme Code',TSPL_GATEPASS_DETAIL_DAIRYSALE.OrgUnit_code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Balance_Qty,TSPL_GATEPASS_DETAIL_DAIRYSALE.DOQty,TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No, " & _
              "TSPL_GATEPASS_DETAIL_DAIRYSALE.Line_No, TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Item, " & _
              "TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code, TSPL_GATEPASS_DETAIL_DAIRYSALE.Qty, TSPL_GATEPASS_DETAIL_DAIRYSALE.Rate, " & _
              "TSPL_GATEPASS_DETAIL_DAIRYSALE.Amount,tspl_item_master.Item_Desc,TSPL_GATEPASS_DETAIL_DAIRYSALE.MRP,TSPL_GATEPASS_DETAIL_DAIRYSALE.Conv_Factor " & _
              ",TSPL_GATEPASS_DETAIL_DAIRYSALE.Price_Code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Price_Date,TSPL_GATEPASS_DETAIL_DAIRYSALE.OrgRate,isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Type,'') as 'Scheme Type',isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Qty,'') as 'Scheme Item Qty',isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Item_UOm,'') as 'Scheme Item UOM',isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Item_Code,'') as 'Scheme Item Code',isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Code,'') as 'Scheme Code',isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item,'0') as 'FOC' FROM TSPL_GATEPASS_DETAIL_DAIRYSALE left outer join tspl_item_master on  " & _
              "TSPL_GATEPASS_DETAIL_DAIRYSALE.item_code=tspl_item_master.item_code where  Document_No='" & StrCode & "' order by TSPL_GATEPASS_DETAIL_DAIRYSALE.Line_No "
        Dim dt As DataTable = Nothing
        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsGatePassDairySale()
            obj.Arr = New List(Of clsGatePassDairySaleDetail)
            Dim objTr As clsGatePassDairySaleDetail
            For Each dr As DataRow In dt.Rows
                objTr = New clsGatePassDairySaleDetail
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                objTr.Rate = clsCommon.myCdbl(dr("Rate"))
                objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                objTr.Conv_Factor = clsCommon.myCdbl(dr("Conv_Factor"))
                objTr.Price_Code = clsCommon.myCstr(dr("Price_Code"))
                If dr("Price_Date") IsNot DBNull.Value Then
                    objTr.Price_Date = clsCommon.myCDate(dr("Price_Date"))
                End If
                objTr.OrgRate = clsCommon.myCdbl(dr("OrgRate"))
                objTr.Delivery_Code = clsCommon.myCstr(dr("Delivery_Code"))
                objTr.DOQty = clsCommon.myCdbl(dr("DOQty"))
                objTr.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
                objTr.OrgUnit_code = clsCommon.myCstr(dr("OrgUnit_code"))
                objTr.Scheme_Code = clsCommon.myCstr(dr("Scheme Code"))

                objTr.Scheme_Type = clsCommon.myCstr(dr("Scheme Type"))
                objTr.Scheme_Code = clsCommon.myCstr(dr("Scheme Code"))
                objTr.Scheme_Item_Code = clsCommon.myCstr(dr("Scheme Item Code"))
                objTr.Scheme_Item_UOM = clsCommon.myCstr(dr("Scheme Item UOM"))
                objTr.Scheme_Qty = clsCommon.myCstr(dr("Scheme Item Qty"))
                objTr.FOC_Item = clsCommon.myCdbl(dr("FOC"))
                objTr.Scheme_Item = clsCommon.myCstr(dr("Scheme_Item"))
                obj.Arr.Add(objTr)
            Next
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsGatePassDairySale = clsGatePassDairySale.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmGatePassDairy, obj.Location_Code, obj.Document_Date, trans)

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                Dim qry = "delete from TSPL_GATEPASS_DETAIL_DAIRYSALE where Document_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_MULTI_BOOKING_DETAIL where Document_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_GATEPASS_MASTER_DAIRYSALE where Document_No='" + strCode + "'"
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
            PostData(FormId, strDocNo, trans, 0)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction, ByVal intAutogenerated As Integer) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("SRN No not found to Post")
            End If
            Dim obj As clsDeliveryNoteFreshSale = clsDeliveryNoteFreshSale.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If


            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim qry = "Update TSPL_GATEPASS_MASTER_DAIRYSALE set Posted=1,status=1, " & _
                "Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "',Modified_By='" + objCommonVar.CurrentUserCode + "', " & _
                "Modified_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "' " & _
                " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetBalanceDeliveryQty(ByVal strDOCode As String, ByVal strICode As String, ByVal strCurrDelNNo As String, ByVal strUOM As String, ByVal dblMRP As Double) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " & _
            " select TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code as ICode,TSPL_GATEPASS_DETAIL_DAIRYSALE.Qty as Qty,1 as RI from TSPL_GATEPASS_DETAIL_DAIRYSALE left outer join TSPL_GATEPASS_MASTER_DAIRYSALE on TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No where  TSPL_GATEPASS_MASTER_DAIRYSALE.posted=1 and TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No ='" + strDOCode + "' and TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code='" + strICode + "' and  TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code='" + strUOM + "'" & _
            " union all " & _
            " select TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,(TSPL_SD_SHIPMENT_DETAIL.Qty) as Qty,-1 as RI from TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_DETAIL.Delivery_Code='" + strDOCode + "'   and TSPL_SD_SHIPMENT_DETAIL.Item_Code='" + strICode + "' and  TSPL_SD_SHIPMENT_DETAIL.Unit_code='" + strUOM + "'  and TSPL_SD_SHIPMENT_DETAIL.Document_Code not in ('" + strCurrDelNNo + "')  " & _
            " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function
    Public Shared Function ClosedData(ByVal obj As clsDeliveryNoteFreshSale, ByVal strCode As String) As Boolean
        Try
            Dim qry As String = "update TSPL_GATEPASS_MASTER_DAIRYSALE set Short_Close='" + obj.Short_Close + "' where comp_code='" + objCommonVar.CurrentCompanyCode + "' and Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
Public Class clsGatePassDairySaleDetail
#Region "Variable"
    Public Document_No As String = Nothing
    Public Line_No As Integer
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_code As String = Nothing
    Public Qty As Double = 0
    Public Rate As Double = 0
    Public Amount As Double = 0
    Public Balance_Qty As Double = 0
    Public Conv_Factor As Double = 0
    Public MRP As Double = 0
    Public Price_Code As String = Nothing
    Public Price_Date As Date?
    Public OrgRate As Double = 0
    Public Delivery_Code As String = Nothing
    Public DOQty As Double = 0
    Public OrgUnit_code As String = ""
    Public Sampling As Integer = 0


    Public Scheme_Type As String = Nothing
    Public Scheme_Item_Code As String = Nothing
    Public Scheme_Qty As Double = 0
    Public Scheme_Item_UOM As String = Nothing
    Public Scheme_Item As String = Nothing
    Public FOC_Item As Double = 0
    Public SellingPrice As Double = 0
    Public Scheme_Code As String = Nothing

#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsGatePassDairySaleDetail), ByVal trans As SqlTransaction, ByVal CustCode As String) As Boolean
        Dim LineNo As Integer = 0
        Dim SchemeType As String = Nothing
        Dim Scheme_Item_Code As String = Nothing
        Dim Scheme_Qty As Double = 0
        Dim Scheme_Item_UOM As String = Nothing
        Dim SchemeCode As String = Nothing

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim dblTotal As Double = 0
            For Each obj As clsGatePassDairySaleDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", IIf(obj.Document_No = "", strDocNo, obj.Document_No)) 'strDocNo 'skg use iif because qty update from booking
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Conv_Factor", obj.Conv_Factor)
                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
                clsCommon.AddColumnsForChange(coll, "OrgRate", obj.OrgRate)
                clsCommon.AddColumnsForChange(coll, "DOQty", obj.DOQty)
                clsCommon.AddColumnsForChange(coll, "Balance_Qty", obj.Balance_Qty)
                clsCommon.AddColumnsForChange(coll, "Delivery_Code", obj.Delivery_Code, True)
                clsCommon.AddColumnsForChange(coll, "OrgUnit_code", obj.OrgUnit_code)
                clsCommon.AddColumnsForChange(coll, "Sampling", obj.Sampling)
                clsCommon.AddColumnsForChange(coll, "Item_Selling_Price", obj.SellingPrice)
                clsCommon.AddColumnsForChange(coll, "Scheme_Type", obj.Scheme_Type)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item_Code", obj.Scheme_Item_Code)
                clsCommon.AddColumnsForChange(coll, "Scheme_Qty", obj.Scheme_Qty)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item_UOM", obj.Scheme_Item_UOM)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item", obj.Scheme_Item)
                clsCommon.AddColumnsForChange(coll, "FOC_Item", obj.FOC_Item)
                clsCommon.AddColumnsForChange(coll, "Scheme_Code", obj.Scheme_Code)


                Dim DOCdateCurrent As Date? = Nothing
                DOCdateCurrent = clsCommon.GETSERVERDATE(trans)




                If obj.Price_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd-MMM-yyyy"))
                End If
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GATEPASS_DETAIL_DAIRYSALE", OMInsertOrUpdate.Insert, "", trans)
               
            Next

            
        End If
        Return True
    End Function

End Class

Public Class clsGatePassDairyMultiBooking
#Region "Variable"
    Public Document_No As String = Nothing
    Public Booking_No As String = Nothing

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsGatePassDairyMultiBooking), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsGatePassDairyMultiBooking In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Booking_No", obj.Booking_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MULTI_BOOKING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
#End Region

End Class
