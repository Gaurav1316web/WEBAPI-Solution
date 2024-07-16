Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Office.Interop
Public Class clsDemandBookingUploader
#Region "Variable"
    Dim qry As String
    Public Document_No As String = Nothing
    Public Document_Date As String = Nothing
    Public Location_Code As String = Nothing
    Public City_Code As String = Nothing
    Public Posted As Integer = 0
    Public IsIndividualCustomer As Integer = 0
    Public Posting_Date As DateTime? = Nothing
    Public Price_code As String
    Public Route_No As String = Nothing
    Public ShiftType As String = String.Empty
    Public ItemType As String = String.Empty
    Public TripNo As String = String.Empty
    Public TotalQtyInCrates As Decimal = 0
    Public TotalQtyInLtr As Decimal = 0
    Public DocumentAmount As Decimal = 0

    Public Posted_Morning As Integer = 0
    Public Posted_Evening As Integer = 0
    Public UploderDocNo As String = String.Empty

    Public Arr As List(Of clsDemandBookingUploaderDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsDemandBookingUploader, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, False, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As clsDemandBookingUploader, ByVal isNewEntry As Boolean, ByVal IsDemandUploader As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim ShiftType As String = ""
            Dim isBoothReset As Boolean = False
            Dim qry As String = ""



            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Dairy Sale", "Demand Booking", obj.Location_Code, obj.Document_Date, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "City_Code", obj.City_Code)
            clsCommon.AddColumnsForChange(coll, "ShiftType", obj.ShiftType)
            clsCommon.AddColumnsForChange(coll, "ItemType", obj.ItemType)
            clsCommon.AddColumnsForChange(coll, "IsIndividualCustomer", obj.IsIndividualCustomer)
            clsCommon.AddColumnsForChange(coll, "TotalQtyInCrates", obj.TotalQtyInCrates)
            clsCommon.AddColumnsForChange(coll, "TotalQtyInLtr", obj.TotalQtyInLtr)
            clsCommon.AddColumnsForChange(coll, "DocumentAmount", obj.DocumentAmount)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Posted", obj.Posted)

            If isNewEntry Then
                If IsDemandUploader Then
                    obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmDemandBookingUploader, "", obj.Location_Code)

                Else
                    obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmDemandBooking, "", obj.Location_Code)

                End If
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TEMP_DEMAND_BOOKING_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TEMP_DEMAND_BOOKING_MASTER", OMInsertOrUpdate.Update, "TSPL_TEMP_DEMAND_BOOKING_MASTER.Document_No='" + obj.Document_No + "'", trans)
            End If

            clsDemandBookingUploaderDetail.SaveData(obj.Document_No, obj.Document_Date, obj.Arr, trans, obj.Location_Code, ShiftType, isNewEntry, IsDemandUploader)
            createDairyBookingDoc(obj.Document_No, trans, isNewEntry, ShiftType, obj.Document_Date, "", False, IsDemandUploader)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function createDairyBookingDoc(ByVal strDemandBookingNo As String, ByVal trans As SqlTransaction, ByVal isDemandBookingNewEntry As Boolean, ByVal UpdatedShiftType As String, ByVal strDocumentDate As Date, ByVal CustCode As String, ByVal isReset As Boolean, ByVal IsDemandUploader As Boolean)
        Dim obj As New clsDairyBookingCustomerbyUploader()
        Dim objTr As New clsDairyBookingCustomerDetailbyUploader()
        Try
            If clsCommon.myLen(clsCommon.myCstr(strDemandBookingNo)) > 0 Then
                Dim strBookingDocNo As String = String.Empty
                Dim AmountToCheckCustomerOutstandingForTCSTax As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AmountToCheckCustomerOutstandingForTCSTax, clsFixedParameterCode.AmountToCheckCustomerOutstandingForTCSTax, trans))

                Dim intCounter As Integer = 0
                Dim LocationCode As String = String.Empty
                Dim CustomerCode As String = String.Empty
                Dim TripNo As Integer = 1
                Dim strShiftType As String = String.Empty
                Dim IsTCSApplicable As Boolean = False
                Dim DocuAmount As Double = 0
                Dim totalQty As Double = 0
                Dim TCSBaseAmount As Double = 0
                Dim TCSAmount As Double = 0
                Dim strdocdate As Date? = Nothing
                Dim CustomerCount As Integer = 0
                Dim strcountno As String = ""
                Dim TotalCrate As Double = 0
                Dim LineNo As Integer = 1
                Dim TCSTaxRate As Double = 0
                Dim strwhrcls As String = ""
                Dim CurrFinYR As String = String.Empty
                Dim FinancialYear As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT CASE WHEN DatePart(Month, '" + clsCommon.GetPrintDate(strDocumentDate, "dd/MMM/yyyy") + "') >= 4 THEN DatePart(Year, '" + clsCommon.GetPrintDate(strDocumentDate, "dd/MMM/yyyy") + "') + 1 ELSE DatePart(Year, '" + clsCommon.GetPrintDate(strDocumentDate, "dd/MMM/yyyy") + "') END AS Fiscal_Year", trans))
                Dim strStartDate As Date = "01/Apr/" + clsCommon.myCstr(clsCommon.myCdbl(FinancialYear - 1))
                Dim strEndDate As Date = "31/Mar/" + FinancialYear
                CurrFinYR = clsCommon.myCstr(clsCommon.myCdbl(FinancialYear - 1)) + "-" + FinancialYear
                If isDemandBookingNewEntry = False Then
                    If clsCommon.myLen(UpdatedShiftType) > 0 Then
                        strwhrcls = " and TSPL_TEMP_DEMAND_BOOKING_DETAIL.ShiftType='" & UpdatedShiftType & "' "
                    End If
                End If

                Dim QryStr As String = " select TSPL_TEMP_DEMAND_BOOKING_DETAIL.*,TSPL_TEMP_DEMAND_BOOKING_MASTER.Route_No,TSPL_TEMP_DEMAND_BOOKING_MASTER.Location_Code,TSPL_TEMP_DEMAND_BOOKING_MASTER.Document_Date,TSPL_TEMP_DEMAND_BOOKING_MASTER.Created_By   from TSPL_TEMP_DEMAND_BOOKING_DETAIL 
left outer join TSPL_TEMP_DEMAND_BOOKING_MASTER on TSPL_TEMP_DEMAND_BOOKING_MASTER.document_no=TSPL_TEMP_DEMAND_BOOKING_DETAIL.Document_No
where TSPL_TEMP_DEMAND_BOOKING_DETAIL.Document_No='" & strDemandBookingNo & "' "
                If clsCommon.myLen(CustCode) > 0 Then
                    QryStr += " and TSPL_TEMP_DEMAND_BOOKING_DETAIL.Cust_Code='" + CustCode + "' "
                End If
                QryStr += " " + strwhrcls + " order by TSPL_TEMP_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_TEMP_DEMAND_BOOKING_DETAIL.ShiftType asc "

                '                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_TEMP_DEMAND_BOOKING_DETAIL.*,TSPL_TEMP_DEMAND_BOOKING_MASTER.Route_No,TSPL_TEMP_DEMAND_BOOKING_MASTER.Location_Code,TSPL_TEMP_DEMAND_BOOKING_MASTER.Document_Date,TSPL_TEMP_DEMAND_BOOKING_MASTER.Created_By   from TSPL_TEMP_DEMAND_BOOKING_DETAIL 
                'left outer join TSPL_TEMP_DEMAND_BOOKING_MASTER on TSPL_TEMP_DEMAND_BOOKING_MASTER.document_no=TSPL_TEMP_DEMAND_BOOKING_DETAIL.Document_No
                'where TSPL_TEMP_DEMAND_BOOKING_DETAIL.Document_No='" & strDemandBookingNo & "' " & strwhrcls & " 
                'order by TSPL_TEMP_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_TEMP_DEMAND_BOOKING_DETAIL.ShiftType asc", trans)
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(QryStr, trans)
                Dim dtmain As DataTable = clsDBFuncationality.GetDataTable("select '' as DemandBookingSrNo,'' as Document_No,'' as 	Line_No,'' as	Cust_Code,'' as Trip_No, '' as 	Item_Code,'' as 	Qty,'' as 	Unit_code,'' as 	Vehicle_Code,'' as 	Item_Rate,'' as DocumentAmount	,'' as Price_code	,'' as ShiftType	,'' as Route_No	,'' as Location_Code,'' as Document_Date,'' as TR_Code,'' as Created_By ", trans)
                dtmain.Rows.RemoveAt(0)
                For Each dr As DataRow In dt.Rows
                    If clsCommon.CompairString(clsCommon.myCDate(strdocdate), clsCommon.myCDate(dr("Document_Date"))) = CompairStringResult.Equal AndAlso clsCommon.CompairString(CustomerCode, clsCommon.myCstr(dr("Cust_Code"))) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strShiftType, clsCommon.myCstr(dr("ShiftType"))) = CompairStringResult.Equal Then
                    Else
                        CustomerCount = CustomerCount + 1
                    End If
                    CustomerCode = clsCommon.myCstr(dr("Cust_Code"))
                    TripNo = clsCommon.myCdbl(dr("Trip_No"))

                    strShiftType = clsCommon.myCstr(dr("ShiftType"))
                    strdocdate = clsCommon.myCDate(dr("Document_Date"))

                    dtmain.Rows.Add("" + clsCommon.myCstr(CustomerCount) + "", "" + clsCommon.myCstr(dr("Document_No")) + "", "" + clsCommon.myCstr(dr("Line_No")) + "", "" + clsCommon.myCstr(dr("Cust_Code")) + "", "" + clsCommon.myCstr(dr("Trip_No")) + "", "" + clsCommon.myCstr(dr("Item_Code")) + "", "" + clsCommon.myCstr(dr("Qty")) + "", "" + clsCommon.myCstr(dr("Unit_Code")) + "", "" + clsCommon.myCstr(dr("Vehicle_Code")) + "", "" + clsCommon.myCstr(dr("Item_Rate")) + "", "" + clsCommon.myCstr(dr("ItemNetAmount")) + "", "" + clsCommon.myCstr(dr("Price_code")) + "", "" + clsCommon.myCstr(dr("ShiftType")) + "", " " + clsCommon.myCstr(dr("Route_No")) + "", "" + clsCommon.myCstr(dr("Location_Code")) + "", "" + clsCommon.myCstr(dr("Document_Date")) + "", "" + clsCommon.myCstr(dr("TR_Code")) + "", "" + clsCommon.myCstr(dr("Created_By")) + "")
                Next
                For Each dr1 As DataRow In dtmain.Rows
                    Dim intCurrInvNo As Integer = clsCommon.myCdbl(dr1("DemandBookingSrNo"))
                    IsTCSApplicable = False
                    Dim balanceAmt As Double = 0
                    Dim OPInvoice_Sale_Amt As Double = 0
                    TCSBaseAmount = 0
                    Dim strqry As String = "select sum(ItemNetAmount) from TSPL_TEMP_DEMAND_BOOKING_MASTER left join TSPL_TEMP_DEMAND_BOOKING_DETAIL on TSPL_TEMP_DEMAND_BOOKING_MASTER.Document_No=TSPL_TEMP_DEMAND_BOOKING_DETAIL.Document_No where TSPL_TEMP_DEMAND_BOOKING_DETAIL.Cust_Code='" + clsCommon.myCstr(dr1("cust_code")) + "' and convert(date, TSPL_TEMP_DEMAND_BOOKING_MASTER.Document_Date,103)>='" + clsCommon.GetPrintDate(strStartDate) + "' and convert(date, TSPL_TEMP_DEMAND_BOOKING_MASTER.Document_Date,103)<='" + clsCommon.GetPrintDate(strEndDate) + "' "

                    balanceAmt = clsDBFuncationality.getSingleValue(strqry, trans)
                    OPInvoice_Sale_Amt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Sale_amt from TSPL_OP_INVOICE_FOR_TCS where Customer_Code='" + clsCommon.myCstr(dr1("cust_code")) + "' and Financial_Year_Code='" + clsCommon.myCstr(CurrFinYR) + "'", trans))
                    TCSBaseAmount = OPInvoice_Sale_Amt + balanceAmt

                    If AmountToCheckCustomerOutstandingForTCSTax > 0 Then

                        If TCSBaseAmount > AmountToCheckCustomerOutstandingForTCSTax Then
                            IsTCSApplicable = True
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, trans)) = 1 Then
                                Dim Is_ITR_Filled_And_TCSAmountGreater50K As Boolean = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT CASE WHEN ISNULL(IsTCSGreaterthan50K,0)=1 AND ISNULL(IsITRfilledinLast2Years,0)=1 THEN 1 ELSE 0 END FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + clsCommon.myCstr(dr1("cust_code")) + "'", trans)) = 1, True, False)
                                If Is_ITR_Filled_And_TCSAmountGreater50K = True Then
                                    TCSTaxRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, trans))
                                Else
                                    TCSTaxRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, trans))
                                End If
                            Else
                                Dim panno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(pan,'')+isnull(Additional3 ,'') as PanNoAdhar from tspl_customer_master where cust_code='" + clsCommon.myCstr(dr1("cust_code")) + "'", trans))
                                If clsCommon.myLen(panno) > 0 Then
                                    TCSTaxRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, trans))
                                Else
                                    TCSTaxRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, trans))
                                End If
                            End If
                        Else
                            TCSTaxRate = 0
                        End If
                    Else
                        TCSTaxRate = 0
                    End If
                    'TCSAmount = TCSBaseAmount * (TCSTaxRate / 100)
                    If clsCommon.CompairString(strcountno, clsCommon.myCstr(dr1("DemandBookingSrNo"))) <> CompairStringResult.Equal Then
                        LineNo = 1
                        DocuAmount = 0
                        TotalCrate = 0
                        totalQty = 0
                        obj = New clsDairyBookingCustomerbyUploader()
                        If clsCommon.CompairString(clsCommon.myCstr(dr1("ShiftType")), "Morning") = CompairStringResult.Equal Then
                            obj.Document_Date = clsCommon.myCDate(clsCommon.GetPrintDate(clsCommon.myCstr(dr1("Document_Date")), "dd/MMM/yyyy"))
                            obj.GatePass_Type = "AM"
                        Else
                            obj.Document_Date = clsCommon.myCDate(clsCommon.GetPrintDate(clsCommon.myCstr(dr1("Document_Date")), "dd/MMM/yyyy"))
                            obj.GatePass_Type = "PM"
                        End If

                        obj.location_code = clsCommon.myCstr(dr1("Location_Code"))
                        obj.Trip_No = clsCommon.myCdbl(dr1("Trip_No"))
                        obj.Is_Taxable = 2
                        obj.TRANSACTION_TYPE = ""
                        obj.From_Screen_code = clsUserMgtCode.frmDairyBookingCustomer
                        obj.Against_DemandBooking_No = clsCommon.myCstr(dr1("Document_No"))

                        ''for detail table
                        obj.Arr = New List(Of clsDairyBookingCustomerDetailbyUploader)
                        If clsCommon.myCdbl(dr1("Qty")) > 0 Then
                            objTr = New clsDairyBookingCustomerDetailbyUploader()
                            objTr.Line_No = LineNo
                            If isReset Then
                                objTr.Booking_Qty = 0
                            Else
                                objTr.Booking_Qty = clsCommon.myCdbl(dr1("Qty"))
                            End If
                            objTr.Cust_Code = clsCommon.myCstr(dr1("cust_code"))
                            objTr.Sampling = 0
                            objTr.Loc_Code = clsCommon.myCstr(dr1("Location_Code"))
                            objTr.Item_Code = clsCommon.myCstr(dr1("Item_Code"))
                            objTr.Unit_code = clsCommon.myCstr(dr1("Unit_code"))
                            objTr.Against_DemandBooking_No = clsCommon.myCstr(dr1("Document_No"))
                            objTr.Against_DemandBooking_TR_Code = clsCommon.myCstr(dr1("TR_Code"))

                            Dim qry As String = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,case when len (isnull(TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM ,'')) > 0 then TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM else TSPL_ITEM_UOM_DETAIL.UOM_Code end as UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                            " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master " & Environment.NewLine &
                            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                            " left outer join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = tspl_item_master.Item_Code  " & Environment.NewLine &
                            " where tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 and tspl_item_master.Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'  "

                            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                For Each dr2 As DataRow In dt1.Rows
                                    objTr.Tax_NonTax = clsCommon.myCstr(dr2("IsTaxable"))
                                    objTr.FreshAmbient = clsCommon.myCstr(dr2("IsFreshAmbient"))
                                    objTr.Short_Description = clsCommon.myCstr(dr2("Short_Description"))
                                Next
                            End If

                            objTr.Route_No = clsCommon.myCstr(dr1("Route_No"))
                            objTr.Vehicle_Code = clsCommon.myCstr(dr1("vehicle_code"))

                            Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code " & Environment.NewLine &
                                    " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code " & Environment.NewLine &
                                    " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + objTr.Item_Code + "' and Cust_Code='" + objTr.Cust_Code + "'))a where a.sno=1)" & Environment.NewLine &
                                    " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'" & Environment.NewLine &
                                     " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + objTr.Item_Code + "' order by TSPL_SCHEME_MASTER_NEW.Scheme_Code "





                            'crate conversion
                            Dim dblTotalCrateRowWise As Double = 0
                            If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) > 0 Then
                                Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
                                If ItemCrateType = 1 Then
                                    Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(objTr.Unit_code) & "'", trans))
                                    Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where 1=1 and TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and tspl_unit_master.Crate_Type ='Y' ", trans))
                                    Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objTr.Unit_code) & "' ", trans))

                                    If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                        Dim DispatchQty As Double = clsCommon.myCdbl(objTr.Booking_Qty) * ItemConvFactor
                                        If DispatchQty >= CrateConvFactor Then
                                            TotalCrate = TotalCrate + Math.Floor(DispatchQty / CrateConvFactor)

                                        End If
                                    End If
                                End If
                            End If
                            ''end of crate Conversion
                            Dim dt3 As New DataTable()
                            Dim dblRate As Double = 0
                            Dim dblTotal As Double = 0
                            Dim dblItemBasicPrice As Double = 0

                            qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
                            " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                            "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                            " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                            " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                            " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                            " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                            "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                            "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                            "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                            "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                            " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                            "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                            "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & clsCommon.myCstr(dr1("Price_code")) & "' and UOM='" & objTr.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objTr.Item_Code & "' AND Location_Code='" & clsCommon.myCstr(obj.location_code) & "'  " &
                            ") XXXE WHERE RowNo=1  "
                            dt3 = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt3.Rows.Count > 0 Then
                                dblRate = clsCommon.myCdbl(dt3.Rows(0).Item("Item_Selling_Price"))
                                If clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0).Item("Is_With_Tax")), "N") = CompairStringResult.Equal Then
                                    dblItemBasicPrice = Math.Round(clsCommon.myCdbl(dt3.Rows(0).Item("Item_Basic_Price")) + Math.Round(clsCommon.myCdbl(dt3.Rows(0).Item("TAX1_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt3.Rows(0).Item("TAX2_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt3.Rows(0).Item("TAX3_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt3.Rows(0).Item("TAX4_Amt")), 2), 2)
                                Else
                                    dblItemBasicPrice = clsCommon.myCdbl(dt3.Rows(0).Item("Item_Basic_Price"))
                                End If

                                If dblRate = 0 Then
                                    Throw New Exception("Please Fill Selling Price for Location " & obj.location_code & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                End If

                                objTr.Item_Rate = clsCommon.myCdbl(dblRate)
                                objTr.Tax_On_Amount = clsCommon.myCdbl(dblItemBasicPrice)
                                objTr.Item_Basic_Rate = clsCommon.myCdbl(dblItemBasicPrice)
                                objTr.SellingPrice = clsCommon.myCdbl(dt3.Rows(0).Item("Item_Selling_Price"))
                                objTr.OrgRate = clsCommon.myCdbl(dt3.Rows(0).Item("Item_Selling_Price"))
                                'objTr.DocumentAmount = Math.Round(clsCommon.myCdbl(objTr.Booking_Qty) * clsCommon.myCdbl(objTr.Item_Rate), 2)


                                objTr.Price_with_Tax = clsCommon.myCdbl(dblItemBasicPrice)
                                objTr.Amount_with_Tax = dblItemBasicPrice * clsCommon.myCdbl(objTr.Booking_Qty)
                                objTr.DocumentAmount = Math.Round(clsCommon.myCdbl(objTr.Amount_with_Tax), 2)

                                objTr.Booking_Status = 1
                                objTr.Item_Price_ID = clsCommon.myCstr(dt3.Rows(0).Item("Item_Price_ID"))
                                objTr.Price_IdStartDate = clsCommon.myCstr(dt3.Rows(0).Item("Start_date"))
                                objTr.PricePlanNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Plan_Code  from TSPL_ITEM_PRICE_PLAN_DETAIL WHERE PLAN_TR_CODE='" & clsCommon.myCstr(dt3.Rows(0).Item("Against_Plan_TR_Code")) & "'", trans))

                            Else
                                Throw New Exception("Please create Price chart for customer " & objTr.Cust_Code & " for Location " & obj.location_code & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                            End If

                            DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(clsCommon.myCdbl(objTr.Amount_with_Tax), 2)
                            totalQty = totalQty + clsCommon.myCdbl(objTr.Booking_Qty)
                            obj.Arr.Add(objTr)

                            LineNo = LineNo + 1
                        End If
                    Else
                        If clsCommon.myCdbl(dr1("Qty")) > 0 Then
                            objTr = New clsDairyBookingCustomerDetailbyUploader()
                            objTr.Line_No = LineNo
                            If isReset Then
                                objTr.Booking_Qty = 0
                            Else
                                objTr.Booking_Qty = clsCommon.myCdbl(dr1("Qty"))

                            End If
                            objTr.Cust_Code = clsCommon.myCstr(dr1("cust_code"))

                            objTr.Sampling = 0
                            objTr.Loc_Code = clsCommon.myCstr(dr1("Location_Code"))
                            objTr.Item_Code = clsCommon.myCstr(dr1("Item_Code"))
                            objTr.Unit_code = clsCommon.myCstr(dr1("Unit_code"))
                            objTr.Against_DemandBooking_No = clsCommon.myCstr(dr1("Document_No"))
                            objTr.Against_DemandBooking_TR_Code = clsCommon.myCstr(dr1("TR_Code"))

                            Dim qry As String = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,case when len (isnull(TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM ,'')) > 0 then TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM else TSPL_ITEM_UOM_DETAIL.UOM_Code end as UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                            " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master " & Environment.NewLine &
                            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                            " left outer join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = tspl_item_master.Item_Code  " & Environment.NewLine &
                            " where tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 and tspl_item_master.Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'  "

                            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                For Each dr2 As DataRow In dt1.Rows
                                    objTr.Tax_NonTax = clsCommon.myCstr(dr2("IsTaxable"))
                                    objTr.FreshAmbient = clsCommon.myCstr(dr2("IsFreshAmbient"))
                                    objTr.Short_Description = clsCommon.myCstr(dr2("Short_Description"))
                                Next
                            End If

                            objTr.Route_No = clsCommon.myCstr(dr1("Route_No"))
                            objTr.Vehicle_Code = clsCommon.myCstr(dr1("vehicle_code"))

                            Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code " & Environment.NewLine &
                                    " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code " & Environment.NewLine &
                                    " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + objTr.Item_Code + "' and Cust_Code='" + objTr.Cust_Code + "'))a where a.sno=1)" & Environment.NewLine &
                                    " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'" & Environment.NewLine &
                                     " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + objTr.Item_Code + "' order by TSPL_SCHEME_MASTER_NEW.Scheme_Code "


                            Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme, trans)



                            'crate conversion
                            Dim dblTotalCrateRowWise As Double = 0
                            If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) > 0 Then
                                Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
                                If ItemCrateType = 1 Then
                                    Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(objTr.Unit_code) & "'", trans))
                                    Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where 1=1 and TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and tspl_unit_master.Crate_Type ='Y' ", trans))
                                    Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objTr.Unit_code) & "' ", trans))

                                    If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                        Dim DispatchQty As Double = clsCommon.myCdbl(objTr.Booking_Qty) * ItemConvFactor
                                        If DispatchQty >= CrateConvFactor Then
                                            TotalCrate = TotalCrate + Math.Floor(DispatchQty / CrateConvFactor)
                                        End If
                                    Else
                                        'Throw New Exception("Please fill conversion factor for this unit at line no." & i + 1 & "" & Environment.NewLine)
                                    End If
                                End If
                            End If
                            ''end of crate Conversion

                            Dim dt3 As New DataTable()
                            Dim dblRate As Double = 0
                            Dim dblTotal As Double = 0
                            Dim dblItemBasicPrice As Double = 0

                            qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
                            " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                            "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                            " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                            " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                            " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                            " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                            "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                            "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                            "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                            "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                            " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                            "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                            "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & clsCommon.myCstr(dr1("Price_code")) & "' and UOM='" & objTr.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objTr.Item_Code & "' AND Location_Code='" & clsCommon.myCstr(obj.location_code) & "'  " &
                            ") XXXE WHERE RowNo=1  "
                            dt3 = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt3.Rows.Count > 0 Then
                                dblRate = clsCommon.myCdbl(dt3.Rows(0).Item("Item_Selling_Price"))
                                If clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0).Item("Is_With_Tax")), "N") = CompairStringResult.Equal Then
                                    dblItemBasicPrice = Math.Round(clsCommon.myCdbl(dt3.Rows(0).Item("Item_Basic_Price")) + Math.Round(clsCommon.myCdbl(dt3.Rows(0).Item("TAX1_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt3.Rows(0).Item("TAX2_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt3.Rows(0).Item("TAX3_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt3.Rows(0).Item("TAX4_Amt")), 2), 2)
                                Else
                                    dblItemBasicPrice = clsCommon.myCdbl(dt3.Rows(0).Item("Item_Basic_Price"))
                                End If

                                If dblRate = 0 Then
                                    Throw New Exception("Please Fill Selling Price for Location " & obj.location_code & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                End If

                                objTr.Item_Rate = clsCommon.myCdbl(dblRate)
                                objTr.Tax_On_Amount = clsCommon.myCdbl(dblItemBasicPrice)
                                objTr.Item_Basic_Rate = clsCommon.myCdbl(dblItemBasicPrice)
                                objTr.SellingPrice = clsCommon.myCdbl(dt3.Rows(0).Item("Item_Selling_Price"))
                                objTr.OrgRate = clsCommon.myCdbl(dt3.Rows(0).Item("Item_Selling_Price"))


                                objTr.Price_with_Tax = clsCommon.myCdbl(dblItemBasicPrice)
                                objTr.Amount_with_Tax = dblItemBasicPrice * clsCommon.myCdbl(objTr.Booking_Qty)
                                objTr.DocumentAmount = Math.Round(clsCommon.myCdbl(objTr.Amount_with_Tax), 2)
                                objTr.Booking_Status = 1
                                objTr.Item_Price_ID = clsCommon.myCstr(dt3.Rows(0).Item("Item_Price_ID"))
                                objTr.Price_IdStartDate = clsCommon.myCstr(dt3.Rows(0).Item("Start_date"))
                                objTr.PricePlanNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Plan_Code  from TSPL_ITEM_PRICE_PLAN_DETAIL WHERE PLAN_TR_CODE='" & clsCommon.myCstr(dt3.Rows(0).Item("Against_Plan_TR_Code")) & "'", trans))

                            Else
                                Throw New Exception("Please create Price chart for customer " & objTr.Cust_Code & " for Location " & obj.location_code & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                            End If

                            DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(clsCommon.myCdbl(objTr.DocumentAmount), 2)

                            totalQty = totalQty + clsCommon.myCdbl(objTr.Booking_Qty)
                            obj.Arr.Add(objTr)

                            LineNo = LineNo + 1
                        End If
                    End If
                    strcountno = intCurrInvNo
                    Dim intNextInvNo As Integer = -1

                    If intCounter + 1 < dtmain.Rows.Count Then
                        intNextInvNo = clsCommon.myCdbl(dtmain.Rows(intCounter + 1)("DemandBookingSrNo"))
                    End If

                    If Not (intCurrInvNo = intNextInvNo) Then
                        'obj.Total_Amt = DocuAmount
                        obj.TotalCrate = TotalCrate
                        TCSAmount = 0
                        If IsTCSApplicable Then
                            If (TCSBaseAmount - DocuAmount) > AmountToCheckCustomerOutstandingForTCSTax Then
                                TCSAmount = DocuAmount * (TCSTaxRate / 100)
                                obj.TCSBaseAmt = DocuAmount
                            Else
                                TCSAmount = (TCSBaseAmount - AmountToCheckCustomerOutstandingForTCSTax) * (TCSTaxRate / 100)
                                obj.TCSBaseAmt = (TCSBaseAmount - AmountToCheckCustomerOutstandingForTCSTax)
                            End If

                        End If

                        obj.TCSAmount = TCSAmount
                        obj.Total_Amt = DocuAmount + TCSAmount
                        obj.SaveData(obj, True, trans, "", IsDemandUploader)
                        clsDBFuncationality.ExecuteNonQuery("update TSPL_TEMP_BOOKING_DETAIL set DocumentAmount =" & DocuAmount & ", Total_Qty =" & totalQty & " where Document_No ='" & obj.Document_No & "' and Scheme_Item='N'", trans)
                        clsDBFuncationality.ExecuteNonQuery("update TSPL_TEMP_BOOKING_MATSER set Created_Date ='" & clsCommon.GetPrintDate(clsCommon.myCDate(dr1("Document_Date")), "dd/MMM/yyyy hh:mm tt") & "',Created_By ='" & clsCommon.myCstr(dr1("Created_By")) & "' where Document_No ='" & obj.Document_No & "'", trans)
                    End If
                    intCounter += 1
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
End Class
Public Class clsDemandBookingUploaderDetail
#Region "Variable"
    Public Document_No As String = Nothing
    Public Line_No As Integer
    Public Trip_No As Integer
    Public Item_Code As String = Nothing
    Public Cust_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_code As String = Nothing
    Public TotalCrates_ItemWise As Decimal = 0
    Public TotalLtr_ItemWise As Decimal = 0
    Public ItemNetAmount As Decimal = 0
    Public IsGatePassGenerated As String = "N"
    Public IsTruckSheetGenerated As String = "N"
    Public Is_Posted As String = "N"
    Public Qty As Double = 0
    Public Rate As Double = 0
    Public Price_Code As String = Nothing
    Public Vehicle_Code As String = ""
    Public ShiftType As String = ""
    Public TR_CODE As String = Nothing
    Public IsItemUpdate As Integer = 0

    Public CustomerReorderCheck As Boolean = False
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal DocDate As Date, ByVal Arr As List(Of clsDemandBookingUploaderDetail), ByVal trans As SqlTransaction, ByVal strLocCode As String, ByVal ShiftType As String, ByVal isNewEntry As Boolean, ByVal isUploader As Boolean) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsDemandBookingUploaderDetail In Arr
                If clsCommon.myLen(ShiftType) > 0 Then
                    If Not clsCommon.CompairString(ShiftType, obj.ShiftType) = CompairStringResult.Equal Then
                        Continue For
                    End If
                End If
                If obj.Qty > 0 Then
                    Dim coll As New Hashtable()
                    If isUploader Then
                        obj.TR_CODE = clsERPFuncationality.GetNextCode(trans, DocDate, clsDocType.Detail, clsDocTransactionType.Uploader, "")
                    Else
                        obj.TR_CODE = clsERPFuncationality.GetNextCode(trans, DocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")

                    End If
                    clsCommon.AddColumnsForChange(coll, "TR_CODE", obj.TR_CODE)
                    clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Trip_No", obj.Trip_No)
                    clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "Item_Rate", obj.Rate)
                    clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
                    clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
                    clsCommon.AddColumnsForChange(coll, "ShiftType", obj.ShiftType)
                    clsCommon.AddColumnsForChange(coll, "IsItemUpdate", obj.IsItemUpdate)
                    clsCommon.AddColumnsForChange(coll, "TotalCrates_ItemWise", obj.TotalCrates_ItemWise)
                    clsCommon.AddColumnsForChange(coll, "TotalLtr_ItemWise", obj.TotalLtr_ItemWise)
                    clsCommon.AddColumnsForChange(coll, "ItemNetAmount", obj.ItemNetAmount)
                    clsCommon.AddColumnsForChange(coll, "IsGatePassGenerated", obj.IsGatePassGenerated)
                    clsCommon.AddColumnsForChange(coll, "IsTruckSheetGenerated", obj.IsTruckSheetGenerated)
                    clsCommon.AddColumnsForChange(coll, "Is_Posted", obj.Is_Posted)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TEMP_DEMAND_BOOKING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                End If

            Next
        End If
        Return True
    End Function

End Class
