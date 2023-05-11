Imports common
Imports System.Data.SqlClient
Public Class clsDeliveryNoteDairySale

#Region "Variable"
    Dim qry As String
    Public isCardSale As Integer = 0
    Public Sampling As Integer = 0
    Public Document_No As String = Nothing
    Public Document_Date As String = Nothing
    Public Status As Integer = 1
    Public Ship_To_Location As String = String.Empty
    Public Customer_Code As String = Nothing
    Public Location_Code As String = Nothing
    Public Booking_No As String = Nothing
    Public Booking_Date As String = Nothing
    Public Vehicle_Capacity As Double = 0
    Public Lorry_No As String = Nothing
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
    Public TRANSACTION_TYPE As String = Nothing
    Public Arr As List(Of clsDeliveryNoteDairySaleDetail) = Nothing
    Public From_Screen_code As String = ""
    Public SalesmanCode As String = ""
    Public Podate As Date? = Nothing
    Public Cust_PO_No As String = ""
#End Region
    Public Enum EnumStatusType
        Open = 1
        Pending = 2
        Approved = 3
        Posted = 4
    End Enum
    Public Function SaveData(ByVal obj As clsDeliveryNoteDairySale, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsDeliveryNoteDairySale, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            'If Not isNewEntry Then
            '    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE", "Document_No", "TSPL_DELIVERY_NOTE_detail_FRESHSALE", "Document_No", trans)
            'End If

            qry = "delete from TSPL_DELIVERY_NOTE_detail_FRESHSALE where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmDairySaleDeliveryOrder, "", obj.Location_Code)
            End If

            If (clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Dairy Sale", "Dairy Delivery Order", obj.Location_Code, obj.Document_Date, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            'clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Booking_No", obj.Booking_No)
            clsCommon.AddColumnsForChange(coll, "Booking_Date", clsCommon.GetPrintDate(obj.Booking_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Vehicle_Capacity", obj.Vehicle_Capacity)
            clsCommon.AddColumnsForChange(coll, "Lorry_No", obj.Lorry_No, True)
            clsCommon.AddColumnsForChange(coll, "Road_Permit_No", obj.Road_Permit_No)
            clsCommon.AddColumnsForChange(coll, "Transporter_Name", obj.Transporter_Name)
            clsCommon.AddColumnsForChange(coll, "Freight", obj.Freight)
            clsCommon.AddColumnsForChange(coll, "Freight_Amount", obj.Freight_Amount)
            'clsCommon.AddColumnsForChange(coll, "Posted", obj.Posted)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "OnHold", obj.OnHold)
            clsCommon.AddColumnsForChange(coll, "Short_Close", obj.Short_Close)
            clsCommon.AddColumnsForChange(coll, "Price_code", obj.Price_code)
            clsCommon.AddColumnsForChange(coll, "Ship_To_Location", obj.Ship_To_Location, True)
            'clsCommon.AddColumnsForChange(coll, "Approval_Reqd", obj.Approval_Reqd)
            'clsCommon.AddColumnsForChange(coll, "Is_Approved", obj.Is_Approved)
            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No, True)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Sampling", obj.Sampling)
            clsCommon.AddColumnsForChange(coll, "TRANSACTION_TYPE", obj.TRANSACTION_TYPE)
            clsCommon.AddColumnsForChange(coll, "isCardSale", obj.isCardSale)
            clsCommon.AddColumnsForChange(coll, "From_Screen_code", obj.From_Screen_code)
            clsCommon.AddColumnsForChange(coll, "SalesmanCode", obj.SalesmanCode, True)
            clsCommon.AddColumnsForChange(coll, "CustPO_No", obj.Cust_PO_No, True)
            If obj.Podate IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "custpo_date", clsCommon.GetPrintDate(obj.Podate, "dd/MMM/yyyy hh:mm tt"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "custpo_date", Nothing, True)
            End If

            If isNewEntry Then
                ''richa agarwal 06 may,2016 check entry into do
                If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where Booking_No ='" & obj.Booking_No & "' and Customer_Code ='" & obj.Customer_Code & "' and Location_Code ='" & obj.Location_Code & "' AND Lorry_No ='" & obj.Lorry_No & "' AND  Sampling =" & obj.Sampling & " ", trans) < 1 Then
                    clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
                    clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                    clsCommon.AddColumnsForChange(coll, "CreditApproval_Reqd", obj.CreditApproval_Reqd)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE", OMInsertOrUpdate.Insert, "", trans)

                    If obj.Status = 2 Then
                        qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code) " & _
                        "values ('Fresh Delivery','" & clsUserMgtCode.frmDeliveryNoteFreshSale & "','" & obj.Document_No & "','" & clsCommon.GetPrintDate(obj.Document_Date, "dd-MMM-yyyy") & "','Credit Limit',0,'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" & objCommonVar.CurrentCompanyCode & "')"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                Else
                    Throw New Exception("DO already created for Booking No  " & obj.Booking_No & "")
                End If

                'clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
                'clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                'clsCommon.AddColumnsForChange(coll, "CreditApproval_Reqd", obj.CreditApproval_Reqd)
                'clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                'clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                'isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE", OMInsertOrUpdate.Insert, "", trans)

                'If obj.Status = 2 Then
                '    qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code) " & _
                '    "values ('Fresh Delivery','" & clsUserMgtCode.frmDeliveryNoteFreshSale & "','" & obj.Document_No & "','" & clsCommon.GetPrintDate(obj.Document_Date, "dd-MMM-yyyy") & "','Credit Limit',0,'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" & objCommonVar.CurrentCompanyCode & "')"
                '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                'End If
            Else
                If obj.Status = 2 Then

                    Dim intExist As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(Document_No) from TSPL_TRANSACTION_APPROVAL where Program_Code='" & clsUserMgtCode.frmDeliveryNoteFreshSale & "' and Document_No='" & obj.Document_No & "' ", trans))
                    If intExist = 0 Then
                        clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
                        qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code) " & _
                        "values ('Fresh Delivery','" & clsUserMgtCode.frmDeliveryNoteFreshSale & "','" & obj.Document_No & "','" & clsCommon.GetPrintDate(obj.Document_Date, "dd-MMM-yyyy") & "','Credit Limit',0,'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" & objCommonVar.CurrentCompanyCode & "')"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                End If
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE", OMInsertOrUpdate.Update, "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No='" + obj.Document_No + "'", trans)
            End If

            isSaved = isSaved AndAlso clsDeliveryNoteDairySaleDetail.SaveData(obj.Document_No, obj.Document_Date, Arr, trans, obj.Customer_Code, obj.Location_Code)

            qry = "Update  TSPL_CUSTOMER_MASTER set Credit_Limit =Credit_Limit  + " & obj.Credit_Limit & " where Cust_Code='" & obj.Customer_Code & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            isSaved = isSaved AndAlso clsApprovalScreen.SaveApprovalAtTransLevel(obj.Form_ID, "Document_Code", obj.Document_No, "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE", trans)
            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsDeliveryNoteDairySale
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDeliveryNoteDairySale
        Dim obj As clsDeliveryNoteDairySale = Nothing
        Dim qry = "SELECT  TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Ship_To_Location,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_Date, " &
                      "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Status, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code, " &
                      "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No, " &
                      "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_Date, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Vehicle_Capacity, " &
                      "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Lorry_No, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Road_Permit_No, " &
                      "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Transporter_Name, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Freight, " &
                      "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Freight_Amount, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted, " &
                      "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Comments,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Total_Amt, " &
                      "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posting_Date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.OnHold, " &
                      "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Approval_Reqd, " &
                      "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Price_code,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Is_Approved, " &
                      "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.CreditApproval_Reqd,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Is_Credit_Approved " &
                      ",TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No " &
                      ",isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.CustPO_No,'') as CustPO_No,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.custpo_date,isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.SalesmanCode,'') as SalesmanCode" &
                      " FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where 2=2 "
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = (select MIN(Document_No) from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = (select Max(Document_No) from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = (select Min(Document_No) from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where Document_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = (select Max(Document_No) from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where Document_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDeliveryNoteDairySale()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Ship_To_Location = clsCommon.myCstr(dt.Rows(0)("Ship_To_Location"))
            obj.Booking_No = clsCommon.myCstr(dt.Rows(0)("Booking_No"))
            obj.Booking_Date = clsCommon.myCDate(dt.Rows(0)("Booking_Date"))
            obj.Vehicle_Capacity = clsCommon.myCdbl(dt.Rows(0)("Vehicle_Capacity"))
            obj.Lorry_No = clsCommon.myCstr(dt.Rows(0)("Lorry_No"))
            obj.Road_Permit_No = clsCommon.myCstr(dt.Rows(0)("Road_Permit_No"))
            obj.Transporter_Name = clsCommon.myCstr(dt.Rows(0)("Transporter_Name"))
            obj.Freight = clsCommon.myCstr(dt.Rows(0)("Freight"))
            obj.Freight_Amount = clsCommon.myCdbl(dt.Rows(0)("Freight_Amount"))
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Price_code = clsCommon.myCstr(dt.Rows(0)("Price_code"))
            obj.OnHold = clsCommon.myCstr(dt.Rows(0)("OnHold"))
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

            obj.SalesmanCode = clsCommon.myCstr(dt.Rows(0)("SalesmanCode"))
            obj.Cust_PO_No = clsCommon.myCstr(dt.Rows(0)("CustPO_No"))
            If clsCommon.myLen(dt.Rows(0)("custpo_date")) > 0 Then
                obj.Podate = clsCommon.myCDate(dt.Rows(0)("custpo_date"))
            End If

            qry = "SELECT TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Price_IdStartDate,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.PricePlanNo,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Price_ID,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Scheme_Code,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.OrgUnit_code,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Balance_Qty,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.BookQty,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Booking_No,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No, " &
                "TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Line_No, TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code, " &
                "TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code, TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty, TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Rate, " &
                "TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Amount,tspl_item_master.Item_Desc,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.MRP,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Conv_Factor " &
                ",TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Price_Code,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Price_Date,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.OrgRate FROM TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE left outer join tspl_item_master on  " &
                "TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.item_code=tspl_item_master.item_code where Document_No='" & obj.Document_No & "' and  isnull(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Scheme_Item,'N')<>'Y' and isnull(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.FOC_Item,'0')<>'1'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsDeliveryNoteDairySaleDetail)
                Dim objTr As clsDeliveryNoteDairySaleDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsDeliveryNoteDairySaleDetail
                    objTr.Scheme_Code = clsCommon.myCstr(dr("Scheme_Code"))

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
                    objTr.Booking_No = clsCommon.myCstr(dr("Booking_No"))
                    objTr.BookQty = clsCommon.myCdbl(dr("BookQty"))
                    objTr.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
                    objTr.OrgUnit_code = clsCommon.myCstr(dr("OrgUnit_code"))

                    If dr("Price_IdStartDate") IsNot DBNull.Value Then
                        objTr.Price_IdStartDate = clsCommon.myCDate(dr("Price_IdStartDate"))
                    End If
                    objTr.Item_Price_ID = clsCommon.myCstr(dr("Item_Price_ID"))
                    objTr.PricePlanNo = clsCommon.myCstr(dr("PricePlanNo"))
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
        Dim obj As clsDeliveryNoteDairySale = clsDeliveryNoteDairySale.GetData(strCode, NavigatorType.Current)
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Dairy Sale", "Dairy Delivery Order", obj.Location_Code, obj.Document_Date, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try

                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE", "Document_No", "TSPL_DELIVERY_NOTE_detail_FRESHSALE", "Document_No", trans)


                Dim qry = "delete from TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE where Document_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where Document_No='" + strCode + "'"
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
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal intAutogenerated As Integer) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans, 1)
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
            Dim obj As clsDeliveryNoteDairySale = clsDeliveryNoteDairySale.GetData(strDocNo, NavigatorType.Current, trans)

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Dairy Sale", "Dairy Delivery Order", obj.Location_Code, obj.Document_Date, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            If clsCommon.CompairString(obj.OnHold, "Y") = CompairStringResult.Equal Then
                Throw New Exception("Delivery order No " + obj.Document_No + " Is On Hold.Can't Post it")
            End If

            If clsCommon.CompairString(obj.CreditApproval_Reqd, "Y") = CompairStringResult.Equal AndAlso obj.Is_Credit_Approved = 0 Then
                If intAutogenerated = 0 Then
                    Throw New Exception("Approval required for this Document")
                End If
            Else
                Dim qry = "Update TSPL_DELIVERY_NOTE_MASTER_FRESHSALE set Posted=1,status=4, " & _
                "Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "',Modified_By='" + objCommonVar.CurrentUserCode + "', " & _
                "Modified_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "' " & _
                " where Document_No='" + strDocNo + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                If intAutogenerated Then
                    qry = "Update TSPL_BOOKING_DETAIL set DO_Posted=4 where Delivery_No='" & obj.Document_No & "' and Cust_Code='" & obj.Customer_Code & "' and    Loc_Code='" & obj.Location_Code & "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            End If

            'clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE", "Document_No", "TSPL_DELIVERY_NOTE_detail_FRESHSALE", "Document_No", trans)


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetBalanceDeliveryQty(ByVal strDOCode As String, ByVal strICode As String, ByVal strCurrDelNNo As String, ByVal strUOM As String, ByVal dblMRP As Double) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " & _
            " select TSPL_DELIVERY_NOTE_detail_FRESHSALE.Item_Code as ICode,TSPL_DELIVERY_NOTE_detail_FRESHSALE.Qty as Qty,1 as RI from TSPL_DELIVERY_NOTE_detail_FRESHSALE left outer join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No=TSPL_DELIVERY_NOTE_detail_FRESHSALE.Document_No where  TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.posted=1 and TSPL_DELIVERY_NOTE_detail_FRESHSALE.Document_No ='" + strDOCode + "' and TSPL_DELIVERY_NOTE_detail_FRESHSALE.Item_Code='" + strICode + "' and  TSPL_DELIVERY_NOTE_detail_FRESHSALE.Unit_code='" + strUOM + "'" & _
            " union all " & _
            " select TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,(TSPL_SD_SHIPMENT_DETAIL.Qty) as Qty,-1 as RI from TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_DETAIL.Delivery_Code='" + strDOCode + "'   and TSPL_SD_SHIPMENT_DETAIL.Item_Code='" + strICode + "' and  TSPL_SD_SHIPMENT_DETAIL.Unit_code='" + strUOM + "'  and TSPL_SD_SHIPMENT_DETAIL.Document_Code not in ('" + strCurrDelNNo + "')  " & _
            " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function
    Public Shared Function ClosedData(ByVal obj As clsDeliveryNoteDairySale, ByVal strCode As String) As Boolean
        Try
            Dim qry As String = "update TSPL_DELIVERY_NOTE_MASTER_FRESHSALE set Short_Close='" + obj.Short_Close + "' where comp_code='" + objCommonVar.CurrentCompanyCode + "' and Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
Public Class clsDeliveryNoteDairySaleDetail
#Region "Variable"
    Public Document_No As String = Nothing
    Public Line_No As Integer
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_code As String = Nothing
    Public Price_IdStartDate As DateTime?
    Public PricePlanNo As String = String.Empty
    Public Item_Price_ID As Integer = 0
    Public Qty As Double = 0
    Public Rate As Double = 0
    Public Amount As Double = 0
    Public Balance_Qty As Double = 0
    Public Conv_Factor As Double = 0
    Public MRP As Double = 0
    Public Price_Code As String = Nothing
    Public Price_Date As Date?
    Public OrgRate As Double = 0
    Public Booking_No As String = Nothing
    Public BookQty As Double = 0
    Public OrgUnit_code As String = ""
    Public Sampling As Integer = 0
    Public Disc_Scheme_Code As String = Nothing
    Public Disc_Scheme_Type As String = Nothing
    Public Disc_Scheme_Pers As Double = 0
    Public Disc_Scheme_Amount As Double = 0

    Public Scheme_Type As String = Nothing
    Public Scheme_Item_Code As String = Nothing
    Public Scheme_Qty As Double = 0
    Public Scheme_Item_UOM As String = Nothing
    Public Scheme_Item As String = Nothing
    Public FOC_Item As Double = 0
    Public SellingPrice As Double = 0
    Public Scheme_Code As String = Nothing
    Public Cust_Code As String = Nothing
    Public Cust_Name As String = Nothing
    Public Loc_Code As String = Nothing
    Public BookingDate As Date = Nothing
    Public Tax_NonTax As Integer = 0
    Public FreshAmbient As String = ""
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal DocDate As Date, ByVal Arr As List(Of clsDeliveryNoteDairySaleDetail), ByVal trans As SqlTransaction, ByVal CustCode As String, ByVal strLocCode As String) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim qry As String
            Dim dblTotal As Double = 0
            Dim LineNo As Integer = 0
            Dim SchemeType As String = Nothing
            Dim Scheme_Item_Code As String = Nothing
            Dim Scheme_Qty As Double = 0
            Dim Scheme_Item_UOM As String = Nothing
            Dim SchemeCode As String = Nothing
            Dim arrRepeat As New List(Of String)
            Dim ShowSchemeItemRate As Boolean = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowSchemeItemRateonDairyDispatch, clsFixedParameterCode.ShowSchemeItemRateonDairyDispatch, trans))
            For Each obj As clsDeliveryNoteDairySaleDetail In Arr
                If arrRepeat.Contains(CustCode) Then
                    LineNo += 1
                Else
                    arrRepeat.Add(CustCode)
                    LineNo = 0
                    LineNo += 1
                End If
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", LineNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Conv_Factor", obj.Conv_Factor)
                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
                clsCommon.AddColumnsForChange(coll, "BookQty", obj.BookQty)
                clsCommon.AddColumnsForChange(coll, "Balance_Qty", obj.Balance_Qty)
                clsCommon.AddColumnsForChange(coll, "Booking_No", obj.Booking_No, True)
                clsCommon.AddColumnsForChange(coll, "OrgUnit_code", obj.OrgUnit_code)
                clsCommon.AddColumnsForChange(coll, "Sampling", obj.Sampling)
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Amount", obj.Disc_Scheme_Amount)
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Code", obj.Disc_Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Pers", obj.Disc_Scheme_Pers)
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Type", obj.Disc_Scheme_Type)
                clsCommon.AddColumnsForChange(coll, "OrgRate", obj.OrgRate)
                clsCommon.AddColumnsForChange(coll, "Item_Selling_Price", obj.SellingPrice)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item", "N")
                clsCommon.AddColumnsForChange(coll, "FOC_Item", "0")
                clsCommon.AddColumnsForChange(coll, "Scheme_Type", SchemeType)
                clsCommon.AddColumnsForChange(coll, "Tax_NonTax", obj.Tax_NonTax)
                clsCommon.AddColumnsForChange(coll, "FreshAmbient", obj.FreshAmbient)
                If clsCommon.myLen(obj.Price_IdStartDate) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Price_IdStartDate", clsCommon.GetPrintDate(obj.Price_IdStartDate, "dd/MMM/yyyy"), True)
                Else
                    clsCommon.AddColumnsForChange(coll, "Price_IdStartDate", Nothing, True)
                End If
                clsCommon.AddColumnsForChange(coll, "PricePlanNo", obj.PricePlanNo, True)
                clsCommon.AddColumnsForChange(coll, "Item_Price_ID", obj.Item_Price_ID, True)

                If obj.Price_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd-MMM-yyyy"))
                End If
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DELIVERY_NOTE_detail_FRESHSALE", OMInsertOrUpdate.Insert, "", trans)

                ''Schem Work
                qry = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code "
                qry += " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code "
                qry += " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from "
                qry += " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from "
                qry += " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + obj.Item_Code + "' and Cust_Code='" + CustCode + "'))a where a.sno=1)"
                qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + CustCode + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'"
                qry += " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + obj.Item_Code + "' "
                qry += " order by TSPL_SCHEME_MASTER_NEW.Scheme_Code"
                SchemeType = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                Dim objD As clsSchemeApplyOnDairy = Nothing
                objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(obj.Item_Code), clsCommon.myCstr(obj.Unit_code), clsCommon.myCstr(obj.Qty), CustCode, clsCommon.myCstr(SchemeType), Nothing, DocDate, trans)

                If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                    For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                        LineNo += 1
                        If clsCommon.myLen(clsCommon.myCstr(objtrScheme.schm_Type)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(objtrScheme.schm_Type), "Quantitive") = CompairStringResult.Equal Then
                            Dim colll As New Hashtable()
                            clsCommon.AddColumnsForChange(colll, "Document_No", strDocNo)
                            clsCommon.AddColumnsForChange(colll, "Line_No", LineNo)
                            clsCommon.AddColumnsForChange(colll, "Item_Code", objtrScheme.Schm_Icode)
                            clsCommon.AddColumnsForChange(colll, "Unit_code", objtrScheme.Schm_Item_Uom)
                            clsCommon.AddColumnsForChange(colll, "Qty", objtrScheme.Schm_Qty)
                            clsCommon.AddColumnsForChange(colll, "Booking_No", obj.Booking_No, True)
                            clsCommon.AddColumnsForChange(colll, "Scheme_Item", "Y")
                            clsCommon.AddColumnsForChange(colll, "FOC_Item", "1")
                            clsCommon.AddColumnsForChange(colll, "Scheme_Type", objtrScheme.schm_Type)
                            clsCommon.AddColumnsForChange(colll, "Scheme_Item_Code", obj.Item_Code)
                            clsCommon.AddColumnsForChange(colll, "Scheme_Qty", obj.Qty)
                            clsCommon.AddColumnsForChange(colll, "Scheme_Item_UOM", obj.Unit_code)
                            clsCommon.AddColumnsForChange(colll, "Scheme_Code", objtrScheme.Schm_Code)
                            If ShowSchemeItemRate Then
                                qry = " Select RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code from ( " & _
                                "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " & _
                                "Start_Date Desc) as RowNo, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " & _
                                "Item_Basic_Price,Item_Basic_Net,Price_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " & _
                                "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " & _
                                "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "'  or End_date is null) and  " & _
                                "TSPL_ITEM_PRICE_MASTER.Price_Code='" & obj.Price_Code & "' and UOM='" & objtrScheme.Schm_Item_Uom & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objtrScheme.Schm_Icode & "' AND Location_Code='" & clsCommon.myCstr(strLocCode) & "'"
                                qry += ") XXXE WHERE RowNo=1  "
                                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                    clsCommon.AddColumnsForChange(colll, "Rate", clsCommon.myCdbl(dt1.Rows(0).Item("Item_Basic_Price")))
                                    clsCommon.AddColumnsForChange(colll, "MRP", clsCommon.myCdbl(dt1.Rows(0).Item("Item_Basic_Net")))
                                    clsCommon.AddColumnsForChange(colll, "Price_Date", clsCommon.GetPrintDate(dt1.Rows(0).Item("Start_Date"), "dd/MMM/yyyy"))
                                End If
                            End If
                            clsCommonFunctionality.UpdateDataTable(colll, "TSPL_DELIVERY_NOTE_detail_FRESHSALE", OMInsertOrUpdate.Insert, "", trans)
                        End If
                    Next
                End If
                ''End OF Schem Work
            Next
            ''Add Structure Schem Work
            qry = "select Structure_Code,Unit_code,sum(Qty) as Qty from (" + Environment.NewLine + _
            "select TSPL_ITEM_MASTER.Structure_Code,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code from " + Environment.NewLine + _
            "TSPL_DELIVERY_NOTE_detail_FRESHSALE" + Environment.NewLine + _
            "left outer join TSPL_ITEM_MASTER on tspl_item_master.item_code=TSPL_DELIVERY_NOTE_detail_FRESHSALE.Item_Code" + Environment.NewLine + _
            "where TSPL_DELIVERY_NOTE_detail_FRESHSALE.Document_No='" + strDocNo + "' and TSPL_DELIVERY_NOTE_detail_FRESHSALE.Scheme_Item='N'" + Environment.NewLine + _
            ")x group by Structure_Code,Unit_code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim objD As clsSchemeApplyOnDairy = clsSchemeApplyOnDairy.GetSchemeDataStructure(clsCommon.myCstr(dr("Structure_Code")), clsCommon.myCstr(dr("Unit_code")), clsCommon.myCstr(dr("Qty")), CustCode, DocDate, trans)
                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                        For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                            LineNo += 1
                            If clsCommon.myLen(clsCommon.myCstr(objtrScheme.schm_Type)) > 0 Then
                                Dim colll As New Hashtable()
                                clsCommon.AddColumnsForChange(colll, "Document_No", strDocNo)
                                clsCommon.AddColumnsForChange(colll, "Line_No", LineNo)
                                clsCommon.AddColumnsForChange(colll, "Item_Code", objtrScheme.Schm_Icode)
                                clsCommon.AddColumnsForChange(colll, "Unit_code", objtrScheme.Schm_Item_Uom)
                                clsCommon.AddColumnsForChange(colll, "Qty", objtrScheme.Schm_Qty)
                                clsCommon.AddColumnsForChange(colll, "Booking_No", Arr(0).Booking_No, True)
                                clsCommon.AddColumnsForChange(colll, "Scheme_Item", "Y")
                                clsCommon.AddColumnsForChange(colll, "FOC_Item", "1")
                                clsCommon.AddColumnsForChange(colll, "Scheme_Type", objtrScheme.schm_Type)
                                clsCommon.AddColumnsForChange(colll, "Scheme_Item_Code", clsCommon.myCstr(dr("Structure_Code")))
                                clsCommon.AddColumnsForChange(colll, "Scheme_Qty", clsCommon.myCstr(dr("Qty")))
                                clsCommon.AddColumnsForChange(colll, "Scheme_Item_UOM", clsCommon.myCstr(dr("Unit_code")))
                                clsCommon.AddColumnsForChange(colll, "Scheme_Code", objtrScheme.Schm_Code)
                                clsCommonFunctionality.UpdateDataTable(colll, "TSPL_DELIVERY_NOTE_detail_FRESHSALE", OMInsertOrUpdate.Insert, "", trans)
                            End If
                        Next
                    End If
                Next
            End If
            ''End of Structure Schem Work
        End If
        Return True
    End Function
    Public Shared Function SaveDataDemo(ByVal strDocNo As String, ByVal Arr As List(Of clsDeliveryNoteDairySaleDetail), ByVal trans As SqlTransaction, ByVal CustCode As String) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim dblTotal As Double = 0
            Dim LineNo As Integer = 0
            Dim SchemeType As String = Nothing
            Dim Scheme_Item_Code As String = Nothing
            Dim Scheme_Qty As Double = 0
            Dim Scheme_Item_UOM As String = Nothing
            Dim SchemeCode As String = Nothing
            Dim arrRepeat As New List(Of String)

            For Each obj As clsDeliveryNoteDairySaleDetail In Arr
                If arrRepeat.Contains(CustCode) Then
                    LineNo += 1
                Else
                    arrRepeat.Add(CustCode)
                    LineNo = 0
                    LineNo += 1
                End If

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", LineNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Conv_Factor", obj.Conv_Factor)
                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
                clsCommon.AddColumnsForChange(coll, "BookQty", obj.BookQty)
                clsCommon.AddColumnsForChange(coll, "Balance_Qty", obj.Balance_Qty)
                clsCommon.AddColumnsForChange(coll, "Booking_No", obj.Booking_No, True)
                clsCommon.AddColumnsForChange(coll, "OrgUnit_code", obj.OrgUnit_code)
                clsCommon.AddColumnsForChange(coll, "Sampling", obj.Sampling)
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Amount", obj.Disc_Scheme_Amount)
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Code", obj.Disc_Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Pers", obj.Disc_Scheme_Pers)
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Type", obj.Disc_Scheme_Type)
                clsCommon.AddColumnsForChange(coll, "OrgRate", obj.OrgRate)
                clsCommon.AddColumnsForChange(coll, "Item_Selling_Price", obj.SellingPrice)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item", "N")
                clsCommon.AddColumnsForChange(coll, "FOC_Item", "0")


                Dim DOCdateCurrent As Date? = Nothing
                DOCdateCurrent = clsCommon.GETSERVERDATE(trans)









                Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code "
                qryScheme += " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code "
                qryScheme += " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from "
                qryScheme += " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(DOCdateCurrent, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(DOCdateCurrent, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from "
                qryScheme += " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + obj.Item_Code + "' and Cust_Code='" + CustCode + "'))a where a.sno=1)"
                qryScheme += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + CustCode + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'"
                qryScheme += " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + obj.Item_Code + "' "


                qryScheme += " order by TSPL_SCHEME_MASTER_NEW.Scheme_Code"
                SchemeType = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qryScheme, trans))
                Dim objD As clsSchemeApplyOnDairy = Nothing
                If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(obj.Item_Code), clsCommon.myCstr(obj.Unit_code), clsCommon.myCstr(obj.Qty), CustCode, clsCommon.myCstr(SchemeType), Nothing, Nothing, trans)

                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                        For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                            SchemeType = objtrScheme.schm_Type
                            Scheme_Item_Code = objtrScheme.Schm_Icode
                            Scheme_Qty = objtrScheme.Schm_Qty
                            Scheme_Item_UOM = objtrScheme.Schm_Item_Uom
                            SchemeCode = objtrScheme.Schm_Code



                            clsCommon.AddColumnsForChange(coll, "Scheme_Type", objtrScheme.schm_Type)
                            clsCommon.AddColumnsForChange(coll, "Scheme_Item_Code", objtrScheme.Schm_Icode)
                            clsCommon.AddColumnsForChange(coll, "Scheme_Qty", objtrScheme.Schm_Qty)
                            clsCommon.AddColumnsForChange(coll, "Scheme_Item_UOM", objtrScheme.Schm_Item_Uom)
                            clsCommon.AddColumnsForChange(coll, "Scheme_Code", objtrScheme.Schm_Code)


                        Next
                    End If
                End If

                If obj.Price_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd-MMM-yyyy"))
                End If
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DELIVERY_NOTE_detail_FRESHSALE", OMInsertOrUpdate.Insert, "", trans)
                If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal AndAlso objD IsNot Nothing AndAlso objD.Arr.Count Then
                    Dim colll As New Hashtable()
                    LineNo += 1
                    clsCommon.AddColumnsForChange(colll, "Document_No", strDocNo)
                    clsCommon.AddColumnsForChange(colll, "Line_No", LineNo)
                    clsCommon.AddColumnsForChange(colll, "Item_Code", Scheme_Item_Code)
                    clsCommon.AddColumnsForChange(colll, "Unit_code", Scheme_Item_UOM)
                    clsCommon.AddColumnsForChange(colll, "Qty", Scheme_Qty)
                    clsCommon.AddColumnsForChange(colll, "Booking_No", obj.Booking_No, True)
                    clsCommon.AddColumnsForChange(colll, "Scheme_Type", SchemeType)
                    clsCommon.AddColumnsForChange(colll, "Scheme_Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(colll, "Scheme_Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(colll, "Scheme_Item_UOM", obj.Unit_code)
                    clsCommon.AddColumnsForChange(colll, "Scheme_Item", "Y")
                    clsCommon.AddColumnsForChange(colll, "FOC_Item", "1")
                    clsCommon.AddColumnsForChange(colll, "Scheme_Code", SchemeCode)
                    clsCommonFunctionality.UpdateDataTable(colll, "TSPL_DELIVERY_NOTE_detail_FRESHSALE", OMInsertOrUpdate.Insert, "", trans)

                End If
            Next


        End If
        Return True
    End Function

End Class
