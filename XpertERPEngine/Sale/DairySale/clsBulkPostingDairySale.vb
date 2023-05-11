Imports common
Imports System.Data.SqlClient


Public Class clsBulkPostingDairySale

    Public Shared Function PostingAndDOCreation(ByVal DocNo As String, ByVal CustCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostingAndDOCreation(DocNo, CustCode, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostingAndDOCreation(ByVal DocNo As String, ByVal CustCode As String, ByVal trans As SqlTransaction) As Boolean

        Try
            ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Dim strBookingLocationCode As String = ""
            ''richa agarwal 7 Nov,2019 ERO/24/10/19-001077
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Booking_Type ,'') as Booking_Type from TSPL_BOOKING_MATSER where Document_No='" + DocNo + "'", trans)), "CD") = CompairStringResult.Equal Then
                Dim strAgainstBookingNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Against_Booking_No ,'')  from TSPL_BOOKING_MATSER where Document_No='" & DocNo & "' ", trans))
                If clsCommon.myLen(strAgainstBookingNo) <= 0 Then
                    'Dim strReceiptNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Against_Receipt_No ,'')  from TSPL_BOOKING_MATSER where Document_No='" & DocNo & "'  ", trans))
                    'If clsCommon.myLen(strReceiptNo) <= 0 Then
                    '    Throw New Exception("Please create Advance Against Booking Entry No. " & DocNo & "")
                    'End If
                    'If clsCommon.myLen(strReceiptNo) >= 0 Then
                    '    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Posted  from TSPL_RECEIPT_HEADER WHERE Receipt_No ='" & strReceiptNo & "' AND IsChkReverse ='N'", trans)), "Y") <> CompairStringResult.Equal Then
                    '        Throw New Exception("Please Post Advance Receipt No " & strReceiptNo & " before posting of Booking Entry")
                    '    End If
                    'End If
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select * from TSPL_BOOKING_PAYMENT_MODE_DETAIL where Document_No='" & clsCommon.myCstr(DocNo) & "'", trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            Dim strReceiptNo As String = clsCommon.myCstr(dr("Against_Receipt_No"))
                            If clsCommon.myLen(strReceiptNo) <= 0 Then
                                Throw New Exception("Please create Advance Against Booking Entry No. " & DocNo & "")
                            End If
                            If clsCommon.myLen(strReceiptNo) >= 0 Then
                                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Posted  from TSPL_RECEIPT_HEADER WHERE Receipt_No ='" & strReceiptNo & "' AND IsChkReverse ='N'", trans)), "Y") <> CompairStringResult.Equal Then
                                    Throw New Exception("Please Post Advance Receipt No " & strReceiptNo & " before posting of Booking Entry")
                                End If
                            End If
                        Next
                    End If

                End If
            
            End If


            Dim qry = "Update TSPL_BOOKING_MATSER set Posted=1, " & _
             "Modified_By='" + objCommonVar.CurrentUserCode + "', " & _
             "Modified_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "' " & _
             "where Document_No='" + DocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strBookingStatus As Integer = clsCommon.myCdbl(clsDBFuncationality.ExecuteNonQuery("Select Booking_Status FROM TSPL_BOOKING_DETAIL WHERE Cust_Code='" & CustCode & "' and Document_No='" + DocNo + "'", trans))

            If (strBookingStatus = 1 Or strBookingStatus = 3) Then
                qry = "Update TSPL_BOOKING_DETAIL set Booking_Status=4 where Cust_Code='" & CustCode & "' and Document_No='" + DocNo + "'  and Booking_Status<>5"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            'For ii As Integer = 7 To gv1.Columns.Count - 1
            '    Dim objBooking As clsBookingTemp = TryCast(gv1.Columns(ii).Tag, clsBookingTemp)
            '    Dim dblTotalAmt As Double = objBooking.TotalAmt
            '    Dim strBookingStatus As Double = objBooking.Booking_Status
            '    If dblTotalAmt > 0 AndAlso (strBookingStatus = 1 OrElse strBookingStatus = 3) Then
            '        qry = "Update TSPL_BOOKING_DETAIL set Booking_Status=4 where Cust_Code='" & objBooking.CustCOde & "' and Document_No='" + txtDocNo.Value + "'  and Booking_Status<>5"
            '        clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '    End If
            'Next


            'Create DO''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim bookingDocAmount As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(Booking_Qty  *item_rate ) from TSPL_BOOKING_DETAIL where document_no='" + DocNo + "'", trans))
            If bookingDocAmount > 0 Then
                Dim blnRatezero As Boolean = False
                Dim strCustVehicleCode As String = ""
                Dim strRoute As String = ""
                Dim strCustomerDesc As String = ""
                Dim DOstatus As Integer = 0
                Dim strDeliveryNo As String = Nothing
                Dim strVehicleCode As String = Nothing
                Dim strPerformaInvoiceNo As String = Nothing
                Dim strVehicleNumber As String = Nothing
                Dim strTransport As String = Nothing
                'Dim strBookingStatus As Integer = 0
                Dim strBookingPostStatus As Integer = 0
                Dim dblAmount As Double = 0
                qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Number,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No from TSPL_CUSTOMER_MASTER left outer join " &
                       "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
                       "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(CustCode) & "'"
                Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If (dt3 IsNot Nothing AndAlso dt3.Rows.Count > 0) Then
                    strCustomerDesc = clsCommon.myCstr(dt3.Rows(0)("Customer_Name"))
                    strCustVehicleCode = clsCommon.myCstr(dt3.Rows(0)("Number"))
                    strRoute = clsCommon.myCstr(dt3.Rows(0)("Route_No"))
                End If

                Dim Price_code As String = clsDBFuncationality.getSingleValue("select price_CodeNon from tspl_customer_master where cust_code='" & clsCommon.myCstr(CustCode) & "'", trans)

                Dim dtBookingMaster As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_BOOKING_MATSER where Document_No='" + DocNo + "'", trans)
                Dim dtBookingDetail As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_BOOKING_DETAIL where Document_No='" + DocNo + "' and Cust_Code='" + CustCode + "' and FOC_ITEM=0 ", trans)

                Dim obj As New clsDeliveryNoteDairySale
                'dblTotal_Qty = 0
                'obj.Price_code = clsDBFuncationality.getSingleValue("select price_CodeNon from tspl_customer_master where cust_code='" & clsCommon.myCstr(grow.Cells(1).Value) & "'", trans)
                obj.Credit_Limit = 0
                obj.Document_Date = clsCommon.myCstr(dtBookingMaster.Rows(0).Item("Document_Date"))
                obj.Customer_Code = CustCode
                obj.Location_Code = clsCommon.myCstr(dtBookingMaster.Rows(0).Item("location_code"))
                strBookingLocationCode = clsCommon.myCstr(dtBookingMaster.Rows(0).Item("location_code"))
                obj.Ship_To_Location = clsCommon.myCstr(dtBookingMaster.Rows(0).Item("Ship_To_Location"))
                'dblTotal_Qty = dblCustTotalQty
                'obj.Sampling = 0 ' clsCommon.myCstr(dtBookingDetail.Rows(0).Item("Sampling"))
                obj.Booking_No = clsCommon.myCstr(dtBookingMaster.Rows(0).Item("Document_No"))
                obj.Booking_Date = clsCommon.myCstr(dtBookingMaster.Rows(0).Item("Document_Date"))
                obj.Sampling = clsCommon.myCstr(dtBookingMaster.Rows(0).Item("IsSampling"))
                obj.Vehicle_Capacity = 0
                obj.Lorry_No = clsDBFuncationality.getSingleValue("select Vehicle_Code from TSPL_BOOKING_DETAIL where Document_No ='" & DocNo & "' and cust_code='" & obj.Customer_Code & "'", trans)
                obj.Route_No = strRoute
                obj.Transporter_Name = obj.Lorry_No
                obj.Price_code = Price_code
                obj.Freight = ""
                obj.Freight_Amount = 0
                obj.Comments = ""
                obj.OnHold = "N"
                obj.Short_Close = "N"
                obj.Total_Amt = 0
                obj.TRANSACTION_TYPE = clsCommon.myCstr(dtBookingMaster.Rows(0).Item("TRANSACTION_TYPE"))
                If clsCommon.CompairString(clsCommon.myCstr(dtBookingMaster.Rows(0).Item("booking_type")), "CD") = CompairStringResult.Equal Then
                    obj.isCardSale = 1
                Else
                    obj.isCardSale = 0
                End If

                Dim intLineNo As Integer = 1
                Dim dblTotal As Double = 0
                blnRatezero = False
                'DOCreated = False
                'For Each grow As GridViewRowInfo In gv1.Rows


                If dtBookingDetail.Rows.Count() > 0 Then
                    obj.Arr = New List(Of clsDeliveryNoteDairySaleDetail)
                End If

                For i As Integer = 0 To dtBookingDetail.Rows.Count() - 1

                    Dim objTr As New clsDeliveryNoteDairySaleDetail()

                    objTr.Line_No = clsCommon.myCdbl(dtBookingDetail.Rows(i).Item("Line_No"))
                    objTr.Sampling = clsCommon.myCdbl(dtBookingDetail.Rows(i).Item("Sampling"))
                    'Dim objBookingitem As clsBookingTemp = TryCast(grow.Cells(ii).Tag, clsBookingTemp)
                    objTr.Item_Code = clsCommon.myCstr(dtBookingDetail.Rows(i).Item("Item_Code"))
                    objTr.Unit_code = clsCommon.myCstr(dtBookingDetail.Rows(i).Item("Unit_code"))
                    objTr.Booking_No = DocNo
                    objTr.Qty = clsCommon.myCdbl(dtBookingDetail.Rows(i).Item("Booking_Qty"))
                    objTr.BookQty = clsCommon.myCdbl(dtBookingDetail.Rows(i).Item("Booking_Qty"))
                    objTr.Balance_Qty = clsCommon.myCdbl(dtBookingDetail.Rows(i).Item("Booking_Qty"))

                    Dim dblRate As Double = 0

                    dblRate = clsCommon.myCdbl(dtBookingDetail.Rows(i).Item("Item_Rate"))

                    Dim tax_on_amt As Decimal = 0
                    Dim dt As New DataTable()

                    'Dim dblTotal As Double = 0
                    'Dim Price_code As String = clsDBFuncationality.getSingleValue("select price_CodeNon from tspl_customer_master where cust_code='" & CustCode & "'", trans)
                    Dim strCustName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER  where Cust_Code='" & CustCode & "'", trans))
                    Dim ShowMulMRPOfSameItemOnDairyBookingCustomer As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowMulMRPOfSameItemOnDairyBookingCustomer, clsFixedParameterCode.ShowMulMRPOfSameItemOnDairyBookingCustomer, trans)) = 1, True, False)

                    qry = " Select RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,item_MRP,XXXE.TAX1_Rate, " &
                " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.Against_Plan_TR_Code  from ( " &
                "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                "Start_Date Desc) as RowNo, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,item_MRP,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code  from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(clsCommon.myCDate(dtBookingMaster.Rows(0).Item("Document_Date")), "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(clsCommon.myCDate(dtBookingMaster.Rows(0).Item("Document_Date")), "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                "TSPL_ITEM_PRICE_MASTER.Price_Code='" & Price_code & "' and UOM='" & clsCommon.myCstr(dtBookingDetail.Rows(i).Item("Unit_Code")) & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & clsCommon.myCstr(dtBookingDetail.Rows(i).Item("Item_Code")) & "' AND Location_Code='" & clsCommon.myCstr(dtBookingDetail.Rows(i).Item("Loc_code")) & "'  "

                    If ShowMulMRPOfSameItemOnDairyBookingCustomer = True AndAlso clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(obj.Customer_Code) & "' ", trans)), "Others") = CompairStringResult.Equal Then
                        qry += " and round(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price,2) =" & clsCommon.myCdbl(dtBookingDetail.Rows(i).Item("Item_Rate")) & " "
                    End If

                    qry += " ) XXXE WHERE RowNo=1  "
                    dt = clsDBFuncationality.GetDataTable(qry, trans)

                    objTr.Disc_Scheme_Amount = clsCommon.myCdbl(dtBookingDetail.Rows(i).Item("Disc_Scheme_Amount"))
                    objTr.Disc_Scheme_Pers = clsCommon.myCdbl(dtBookingDetail.Rows(i).Item("Disc_Scheme_Pers"))
                    objTr.Disc_Scheme_Code = clsCommon.myCstr(dtBookingDetail.Rows(i).Item("Disc_Scheme_Code"))
                    objTr.Disc_Scheme_Type = clsCommon.myCstr(dtBookingDetail.Rows(i).Item("Disc_Scheme_Type"))
                    'objTr.Disc_Scheme_Amount = System.Math.Round((dblRate * objTr.Disc_Scheme_Pers) / 100, 2) clsCommon.myCdbl(dtBookingDetail.Rows(i).Item("Booking_Qty"))


                    objTr.Rate = dblRate 'objBookingItemRate.ItemRate
                    'sanjay

                    objTr.MRP = clsCommon.myCdbl(dt.Rows(0).Item("item_MRP"))
                    'objTr.MRP = clsCommon.myCdbl(dtBookingDetail.Rows(i).Item("Item_Rate"))  'objBookingItemRate.MRP
                    objTr.Price_Date = clsCommon.myCDate(dt.Rows(0).Item("Start_Date")) 'clsCommon.myCDate(dtBookingMaster.Rows(0).Item("Document_Date")) ' objBookingItemRate.Price_Date
                    objTr.Disc_Scheme_Amount = clsCommon.myCdbl(dtBookingDetail.Rows(i).Item("Disc_Scheme_Amount")) 'objBookingItemRate.Disc_Scheme_Amount
                    objTr.Disc_Scheme_Code = clsCommon.myCstr(dtBookingDetail.Rows(i).Item("Disc_Scheme_Code")) 'objBookingItemRate.Disc_Scheme_Code
                    objTr.Disc_Scheme_Pers = clsCommon.myCdbl(dtBookingDetail.Rows(i).Item("Disc_Scheme_Pers")) 'objBookingItemRate.Disc_Scheme_Pers
                    objTr.Disc_Scheme_Type = clsCommon.myCstr(dtBookingDetail.Rows(i).Item("Disc_Scheme_Type")) 'objBookingItemRate.Disc_Scheme_Type
                    objTr.OrgRate = clsCommon.myCdbl(dtBookingDetail.Rows(i).Item("OrgRate")) 'objBookingItemRate.OrgRate
                    objTr.SellingPrice = clsCommon.myCdbl(dtBookingDetail.Rows(i).Item("Item_Selling_Price")) 'objBookingItemRate.SellingPrice
                    objTr.Amount = clsCommon.myCdbl(objTr.Rate * objTr.Qty) 'clsCommon.myCdbl(grow.Cells(ii).Value))

                    objTr.Item_Price_ID = clsCommon.myCstr(dt.Rows(0).Item("Item_Price_ID"))
                    objTr.Price_IdStartDate = clsCommon.myCDate(dt.Rows(0).Item("Start_date"))
                    objTr.PricePlanNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Plan_Code  from TSPL_ITEM_PRICE_PLAN_DETAIL WHERE PLAN_TR_CODE='" & clsCommon.myCstr(dt.Rows(0).Item("Against_Plan_TR_Code")) & "'", trans))

                    dblTotal += objTr.Amount

                    'End If
                    'If objTr.Rate = 0 Then
                    '    blnRatezero = True
                    '    DOmsg += "Please create Price chart for customer " & objBookingCust.CustCOde & " for Location " & txtLocation.Value & "  for item " & objTr.Item_Code & "." + Environment.NewLine
                    'End If
                    objTr.Price_Code = clsCommon.myCstr(dt.Rows(0).Item("Price_Code"))  'obj.Price_code 
                    objTr.OrgUnit_code = objTr.Qty ' clsCommon.myCstr(grow.Cells(ii).Value)

                    If (clsCommon.myLen(objTr.Item_Code) > 0 And objTr.Qty > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                    objTr.OrgRate = objTr.OrgRate
                    'intLineNo += 1
                    'End If

                    obj.Total_Amt = dblTotal

                    If ShowMulMRPOfSameItemOnDairyBookingCustomer Then
                        qry = "Update TSPL_BOOKING_DETAIL set DO_Qty=" & objTr.Qty & ",Booking_Qty=" & objTr.Qty & ",Total_Qty=" & objTr.Qty & " where item_code='" & objTr.Item_Code & "' and Scheme_Item<>'Y' and vehicle_code='" & obj.Lorry_No & "' and  Document_No='" & DocNo & "' and Cust_Code='" & obj.Customer_Code & "' and    Loc_Code='" & obj.Location_Code & "' and Sampling =" & obj.Sampling & " and Line_No='" & objTr.Line_No & "' and Unit_code='" & objTr.Unit_code & "'  "
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    Else
                        qry = "Update TSPL_BOOKING_DETAIL set DO_Qty=" & objTr.Qty & ",Booking_Qty=" & objTr.Qty & ",Total_Qty=" & objTr.Qty & " where item_code='" & objTr.Item_Code & "' and Scheme_Item<>'Y' and vehicle_code='" & obj.Lorry_No & "' and  Document_No='" & DocNo & "' and Cust_Code='" & obj.Customer_Code & "' and    Loc_Code='" & obj.Location_Code & "' and Sampling =" & obj.Sampling & " and Unit_code='" & objTr.Unit_code & "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If


                Next
                'Next


                If (obj.SaveData(obj, True, trans)) Then
                    qry = "Update TSPL_BOOKING_DETAIL set Delivery_No='" & obj.Document_No & "',DocumentAmount=" & obj.Total_Amt & " where Document_No='" & DocNo & "' and Cust_Code='" & obj.Customer_Code & "' and    Loc_Code='" & obj.Location_Code & "' and vehicle_code='" & obj.Lorry_No & "' and Sampling=" & obj.Sampling & ""
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "Update TSPL_BOOKING_MATSER set CreateDO_Automatic=1 where Document_No='" & DocNo & "' "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    'Post DO''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    clsDeliveryNoteDairySale.PostData("DEL-NOTE-FS", obj.Document_No, trans, 1)
                    'Post DO''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                End If


            End If
            'Create DO''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, DocNo, "TSPL_BOOKING_MATSER", "Document_No", "TSPL_BOOKING_DETAIL", "Document_No", "TSPL_BOOKING_PAYMENT_MODE_DETAIL", "Document_No", trans)

            'trans.Commit()
            'Dim msg = "Successfully Posted"
            'common.clsCommon.MyMessageBoxShow(msg, Me.Text)
            'LoadData(txtDocNo.Value, NavigatorType.Current)
            'btnCreateDO.Enabled = True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    'Public Function PostBooking(ByVal obj As clsDeliveryNoteDairySale) As Boolean

    '    Return True
    'End Function

End Class
