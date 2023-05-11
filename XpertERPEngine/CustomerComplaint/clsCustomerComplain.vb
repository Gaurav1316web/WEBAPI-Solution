Imports common
Imports System.Data.SqlClient

Public Class clsCustomerComplainHead
#Region "Variables"

    Public Complaint_No As String = Nothing
    Public Complaint_Date As DateTime
    Public Invoice_No As String = Nothing
    Public Invoice_Date As Date? = Nothing
    Public Type As String = Nothing
    Public Complaint_Code As String = Nothing
    Public Complaint_Desc As String = Nothing
    Public Remarks As String = Nothing
    Public IsPosted As String = Nothing
    Public Cust_Code As String = Nothing
    Public Cust_Name As String = Nothing
    Public Location_Code As String = Nothing
    Public Arr As List(Of clsCustomerComplainDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsCustomerComplainHead, ByVal isNewEntry As Boolean, ByVal strTransType As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, Nothing, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsCustomerComplainHead, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Dairy Sale", "Dairy Gatepass", obj.Location_Code, obj.GPDate, trans)
        Try
            Dim qry As String = "delete from TSPL_CUSTOMER_COMPLAINT_DETAIL where Complaint_No='" + obj.Complaint_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Complaint_No = clsERPFuncationality.GetNextCode(trans, obj.Complaint_Date, clsDocType.frmCustomerComplain, "", "")
            End If
            If (clsCommon.myLen(obj.Complaint_No) <= 0) Then
                Throw New Exception("Error in Complain Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Complaint_Date", clsCommon.GetPrintDate(obj.Complaint_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Invoice_No", obj.Invoice_No)
            If clsCommon.myLen(obj.Invoice_No) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Invoice_Date", clsCommon.GetPrintDate(obj.Invoice_Date, "dd/MMM/yyyy hh:mm tt"), True)
            End If
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
            clsCommon.AddColumnsForChange(coll, "Complaint_Code", obj.Complaint_Code, True)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            If isNewEntry Then
                'obj.Complaint_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.CusomerComplain, "", "")
                clsCommon.AddColumnsForChange(coll, "Complaint_No", obj.Complaint_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_COMPLAINT_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_COMPLAINT_HEAD", OMInsertOrUpdate.Update, "TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_No='" + obj.Complaint_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsCustomerComplainDetail.SaveData(obj.Complaint_No, obj.Complaint_Date, obj.Arr, trans)
            'If isSaved Then
            '    trans.Commit()
            'End If
        Catch err As Exception
            '  trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Private Shared Function ConvertCusComplaintToShipment(ByVal objCC As clsCustomerComplainHead, ByVal Istaxable As Integer, ByVal trans As SqlTransaction) As clsPSShipmentHead
        Dim obj As New clsPSShipmentHead()
        obj = New clsPSShipmentHead()

        obj.Customer_Code = objCC.Cust_Code
        obj.Customer_Name = objCC.Cust_Name
        obj.Document_Date = objCC.Complaint_Date
        obj.Dispatch_date = objCC.Complaint_Date
        obj.Due_Date = objCC.Complaint_Date
        obj.Challan_Date = objCC.Complaint_Date
        obj.Inv_Date = objCC.Complaint_Date
        obj.Remarks = objCC.Remarks
        obj.IsReplacement = 1
        '' obj.Invoice_No_ForReplacement = objCC.Invoice_No
        obj.Customer_Complaint_No = objCC.Complaint_No
        obj.Is_Create_Auto_Invoice = 1
        obj.ConvRate = 1
        obj.Is_Taxable = Istaxable
        ''obj.Sub_Location_code = clsCommon.myCstr(dt.Rows(0)("sub_location_code"))
        obj.Bill_To_Location = objCC.Location_Code
        obj.DO_Item_Type = IIf(obj.Is_Taxable = 1, "T", "NT")
        obj.Trans_Type = IIf(obj.Is_Taxable = 1, "PS", "FS")
        obj.Invoice_Type = IIf(obj.Is_Taxable = 1, "T", "R")
        obj.Screen_Type = "DS"
        obj.Direct_Dispatch = 1
        obj.Route_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_CUSTOMER_MASTER.Route_No from TSPL_CUSTOMER_MASTER where Cust_Code='" & obj.Customer_Code & "'", trans))
        obj.Arr = New List(Of clsPSShipmentHeadDetail)

        Dim dbllineNo As Integer = 1
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_CUSTOMER_COMPLAINT_DETAIL.*,tspl_item_master.item_desc from TSPL_CUSTOMER_COMPLAINT_detail left outer join tspl_item_master on tspl_item_master.Item_code=TSPL_CUSTOMER_COMPLAINT_DETAIL.Item_Code where TSPL_CUSTOMER_COMPLAINT_DETAIL.Complaint_No='" & objCC.Complaint_No & "' and TSPL_CUSTOMER_COMPLAINT_DETAIL.IsTaxable =" & Istaxable & "", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows

                Dim objTr As New clsPSShipmentHeadDetail()
                objTr.Line_No = dbllineNo
                objTr.Row_Type = "Item"
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty = clsCommon.myCdbl(dr("Damage_Qty"))
                objTr.Balance_Qty = clsCommon.myCdbl(dr("Damage_Qty"))
                objTr.Unit_code = clsCommon.myCstr(dr("Damage_Uom"))

                objTr.Location = obj.Bill_To_Location
                objTr.Scheme_Applicable = "N"
                objTr.Scheme_Item = "N"

                If clsCommon.myLen(clsCommon.myCstr(obj.Bill_To_Location)) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(obj.Bill_To_Location) & "'", trans)), "Y") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(obj.Customer_Code) & "' ", trans)), "Others") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(obj.Customer_Code) & "' ", trans)), "") <> CompairStringResult.Equal Then

                            objTr.Sub_Location_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select sub_location_code from tspl_item_sublocation_mapping where main_location_code='" & clsCommon.myCstr(obj.Bill_To_Location) & "' and Item_code='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))

                        End If
                    End If
                End If

                If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                    obj.Arr.Add(objTr)
                End If
                dbllineNo = dbllineNo + 1
            Next

        End If





        Return obj
    End Function

    'Private Shared Function ConvertCusComplaintToShipment(ByVal objCC As clsCustomerComplainHead, ByVal trans As SqlTransaction) As clsPSShipmentHead
    '    Dim obj As New clsPSShipmentHead()
    '    obj = New clsPSShipmentHead()

    '    obj.Customer_Code = objCC.Cust_Code
    '    obj.Customer_Name = objCC.Cust_Name
    '    obj.Document_Date = objCC.Complaint_Date
    '    obj.Dispatch_date = objCC.Complaint_Date
    '    obj.Due_Date = objCC.Complaint_Date
    '    obj.Challan_Date = objCC.Complaint_Date
    '    obj.Inv_Date = objCC.Complaint_Date
    '    obj.Remarks = objCC.Remarks
    '    obj.IsReplacement = 1
    '    '' obj.Invoice_No_ForReplacement = objCC.Invoice_No
    '    obj.Customer_Complaint_No = objCC.Complaint_No
    '    obj.Is_Create_Auto_Invoice = 1
    '    obj.ConvRate = 1
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Is_Taxable,sub_location_code,Bill_To_Location,Tax_Group,TAX1,TAX2,TAX3,Item_Type,Price_code,Route_No,Invoice_Type,Trans_Type,is_casHsale,Direct_Dispatch,DO_Item_Type,Screen_Type from tspl_sd_shipment_head where Sale_Invoice_No='" & objCC.Invoice_No & "'", trans)
    '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '        obj.Is_Taxable = clsCommon.myCdbl(dt.Rows(0)("Is_Taxable"))
    '        obj.Sub_Location_code = clsCommon.myCstr(dt.Rows(0)("sub_location_code"))
    '        obj.Bill_To_Location = clsCommon.myCstr(dt.Rows(0)("Bill_To_Location"))
    '        obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
    '        obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
    '        obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
    '        obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
    '        obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
    '        obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_code"))
    '        obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
    '        obj.Invoice_Type = clsCommon.myCstr(dt.Rows(0)("Invoice_Type"))
    '        obj.Trans_Type = clsCommon.myCstr(dt.Rows(0)("Trans_Type"))
    '        '' obj.is_casHsale = clsCommon.myCstr(dt.Rows(0)("is_casHsale"))
    '        obj.Direct_Dispatch = clsCommon.myCstr(dt.Rows(0)("Direct_Dispatch"))
    '        obj.DO_Item_Type = clsCommon.myCstr(dt.Rows(0)("DO_Item_Type"))
    '        obj.Screen_Type = clsCommon.myCstr(dt.Rows(0)("Screen_Type"))

    '    End If


    '    obj.Arr = New List(Of clsPSShipmentHeadDetail)
    '    For Each objccDetail As clsCustomerComplainDetail In objCC.Arr

    '        Dim objTr As New clsPSShipmentHeadDetail()
    '        objTr.Line_No = objccDetail.SNo
    '        objTr.Row_Type = "Item"
    '        objTr.Item_Code = objccDetail.Item_Code
    '        objTr.Item_Desc = objccDetail.Item_Desc
    '        objTr.Qty = objccDetail.Damage_Qty
    '        objTr.Balance_Qty = objccDetail.Damage_Qty
    '        objTr.Unit_code = objccDetail.Damage_Uom

    '        objTr.Location = obj.Bill_To_Location
    '        objTr.Scheme_Applicable = "N"
    '        objTr.Scheme_Item = "N"

    '        If clsCommon.myLen(clsCommon.myCstr(obj.Bill_To_Location)) > 0 Then
    '            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(obj.Bill_To_Location) & "'", trans)), "Y") = CompairStringResult.Equal Then
    '                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(obj.Customer_Code) & "' ", trans)), "Others") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(obj.Customer_Code) & "' ", trans)), "") <> CompairStringResult.Equal Then
    '                    ' For i As Integer = 0 To gv1.Rows.Count - 1
    '                    objTr.Sub_Location_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select sub_location_code from tspl_item_sublocation_mapping where main_location_code='" & clsCommon.myCstr(obj.Bill_To_Location) & "' and Item_code='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
    '                    ' Next
    '                End If
    '            End If
    '        End If

    '        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
    '            obj.Arr.Add(objTr)
    '        End If
    '    Next

    '    Return obj
    'End Function

    'Public Shared Function UpdateClosingKMAndCreateProvision(ByVal strDocNo As String, ByVal dclClosingKM As Decimal, ByVal dclDistanceInRoute As Decimal, ByVal dclPriceKMInVehicle As Decimal, ByVal dclTollAmt As Decimal, ByVal FormId As String) As Boolean
    '    If clsCommon.myLen(strDocNo) <= 0 Then
    '        Throw New Exception("No Document found")
    '    End If
    '    Dim obj As clsCustomerComplainHead = clsCustomerComplainHead.GetData(strDocNo, NavigatorType.Current, "")
    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try
    '        If Not clsCommon.CompairString(obj.Post, "Y") = CompairStringResult.Equal Then
    '            Throw New Exception("Document [" + strDocNo + "] should be posted")
    '        End If
    '        If obj.Opening_Km > dclClosingKM Then
    '            Throw New Exception("Opening KM [" + clsCommon.myCstr(obj.Opening_Km) + "] can't be greater than Closing KM [" + clsCommon.myCstr(dclClosingKM) + "]")
    '        End If
    '        obj.Closing_Km = dclClosingKM
    '        obj.Distance_In_Route = dclDistanceInRoute
    '        obj.Price_KM_In_Vehicle = dclPriceKMInVehicle
    '        obj.Toll_Amount = dclTollAmt
    '        Dim coll As New Hashtable()
    '        clsCommon.AddColumnsForChange(coll, "Closing_Km", dclClosingKM)
    '        clsCommon.AddColumnsForChange(coll, "Distance_In_Route", obj.Distance_In_Route, True)
    '        clsCommon.AddColumnsForChange(coll, "Price_KM_In_Vehicle", obj.Price_KM_In_Vehicle, True) ' 
    '        clsCommon.AddColumnsForChange(coll, "Toll_Amount", obj.Toll_Amount, True)
    '        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DAIRYSALE_GATEPASS_MASTER", OMInsertOrUpdate.Update, "TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode='" + obj.GPCode + "'", trans)

    '        CreateProvison(obj, "", trans)

    '        trans.Commit()
    '    Catch err As Exception
    '        trans.Rollback()
    '        Throw New Exception(err.Message)
    '    End Try
    '    Return True
    'End Function

    Public Shared Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType, ByVal TransType As String) As clsCustomerComplainHead
        Return GetData(strRetCode, NavType, TransType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal TransType As String, ByVal trans As SqlTransaction) As clsCustomerComplainHead
        Dim obj As clsCustomerComplainHead = Nothing
        Dim qry As String = "select  TSPL_CUSTOMER_COMPLAINT_HEAD.*,tspl_Customer_master.customer_name from TSPL_CUSTOMER_COMPLAINT_HEAD left outer join tspl_customer_master on tspl_Customer_master.Cust_code=TSPL_CUSTOMER_COMPLAINT_HEAD.cust_code  where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_No = (select MIN(Complaint_No) from TSPL_CUSTOMER_COMPLAINT_HEAD)"
            Case NavigatorType.Last
                qry += " and TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_No = (select Max(Complaint_No) from TSPL_CUSTOMER_COMPLAINT_HEAD)"
            Case NavigatorType.Next
                qry += " and TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_No = (select Min(Complaint_No) from TSPL_CUSTOMER_COMPLAINT_HEAD where Complaint_No>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_No = (select Max(Complaint_No) from TSPL_CUSTOMER_COMPLAINT_HEAD where Complaint_No<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_No = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsCustomerComplainHead()
            obj.Complaint_No = clsCommon.myCstr(dt.Rows(0)("Complaint_No"))
            obj.Complaint_Date = clsCommon.myCDate(dt.Rows(0)("Complaint_Date"))
            obj.Invoice_No = clsCommon.myCstr(dt.Rows(0)("Invoice_No"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            If clsCommon.myLen(obj.Invoice_No) > 0 Then
                obj.Invoice_Date = clsCommon.myCDate(dt.Rows(0)("Invoice_Date"))
            End If
            obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))
            obj.Complaint_Code = clsCommon.myCstr(dt.Rows(0)("Complaint_Code"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.IsPosted = clsCommon.myCstr(dt.Rows(0)("IsPosted"))
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            obj.Cust_Name = clsCommon.myCstr(dt.Rows(0)("customer_name"))
        End If
        obj.Arr = clsCustomerComplainDetail.GetData(obj.Complaint_No, trans)
        Return obj
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
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
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Complain No not found to Post")
            End If
            Dim obj As clsCustomerComplainHead = clsCustomerComplainHead.GetData(strDocNo, NavigatorType.Current, "", trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Complaint_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.IsPosted = "Y") Then
                Throw New Exception("Already Posted")
            End If

            clsDBFuncationality.ExecuteNonQuery("Update TSPL_CUSTOMER_COMPLAINT_HEAD set IsPosted='Y', Modified_By = '" + objCommonVar.CurrentUserCode + "',Modified_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "'  where Complaint_No='" & obj.Complaint_No & "'", trans)


            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData_FromBulkPosting(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Complain No not found to Post")
            End If
            Dim obj As clsCustomerComplainHead = clsCustomerComplainHead.GetData(strDocNo, NavigatorType.Current, "", trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Complaint_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.IsPosted = "Y") Then
                Throw New Exception("Already Posted")
            End If

            clsDBFuncationality.ExecuteNonQuery("Update TSPL_CUSTOMER_COMPLAINT_HEAD set IsPosted='Y', Modified_By = '" + objCommonVar.CurrentUserCode + "',Modified_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "'  where Complaint_No='" & obj.Complaint_No & "'", trans)

            Dim qry As String = "select DISTINCT IsTaxable FROM TSPL_CUSTOMER_COMPLAINT_DETAIL where TSPL_CUSTOMER_COMPLAINT_DETAIL.Complaint_No='" & strDocNo & "' "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt isnot nothing andalso dt.rows.count>0Then
                For Each dr As DataRow In dt.Rows
                    Dim objSH As clsPSShipmentHead = ConvertCusComplaintToShipment(obj, clsCommon.myCstr(dr("IsTaxable")), trans)

                    If objSH.Arr.Count > 0 Then
                        clsPSShipmentHead.SaveData(objSH, True, trans, True)
                        clsPSShipmentHead.PostData(clsUserMgtCode.frmSaleDispatchDairy, objSH.Document_Code, trans, Nothing, True, Nothing, Nothing)
                    End If
                Next
            End If




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
        Dim TransType_Str As String = ""
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If

            Dim Qry As String = "select IsPosted from TSPL_CUSTOMER_COMPLAINT_HEAD where Complaint_No='" + strCode + "'"
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry, trans)), "N") = CompairStringResult.Equal Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "Select distinct document_code  from TSPL_SD_SHIPMENT_HEAD WHERE Customer_Complaint_No='" & strCode & "' "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim strShipmentNo As String = clsCommon.myCstr(dr("document_code"))
                    If clsPSShipmentHead.ReverseAndUnpost(strShipmentNo, trans) Then
                        Qry = "Select Sale_Invoice_No  from TSPL_SD_SHIPMENT_HEAD WHERE Customer_Complaint_No='" & strCode & "' and document_code= '" & strShipmentNo & "' "
                        Dim strInvoiceNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry, trans))
                        clsPSShipmentHead.DeleteData(strShipmentNo, strInvoiceNo, trans)
                    End If


                Next
            End If

            Qry = "Update TSPL_CUSTOMER_COMPLAINT_HEAD set IsPosted = 'N' where Complaint_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    'Public Shared Function CreateProvison(ByVal obj As clsCustomerComplainHead, ByVal FormId As String, ByVal trans As SqlTransaction) As Boolean
    '    Dim isTranspoterVendor As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_VENDOR_MASTER where Transporter ='Y' and Vendor_Code = '" + obj.Transporter + "'", trans))
    '    '==========Added by preeti gupta Against ticket no[ERO/16/05/19-000603]
    '    Dim isDepoVehicle As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_VEHICLE_MASTER where  Vehicle_Type ='D' and Vehicle_Id = '" + obj.Vehicle_Id + "'", trans))
    '    If isDepoVehicle = 0 Then
    '        If clsCommon.myLen(obj.Transporter) <= 0 Or isTranspoterVendor <= 0 Then
    '            If (myMessages.DairyGatePassPovisionConfirm) Then
    '            Else
    '                Throw New Exception("")
    '            End If
    '        Else
    '            If isTranspoterVendor <= 0 Then
    '                Throw New Exception("You can't create provision entry because Transpoter [" + obj.Transporter + "] is not Transpoter type Vendor.")
    '            End If
    '            Dim objProv As clsProvisionEntry = New clsProvisionEntry()
    '            objProv.isNewEntry = True
    '            objProv.Doc_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
    '            objProv.Vendor_Code = obj.Transporter
    '            objProv.Vendor_Type = "Transpoter"
    '            objProv.Prov_type = " Dairy GatePass"
    '            objProv.Status = "No"
    '            objProv.Ref_Doc_No = obj.GPCode
    '            Dim strRatePerKM As Double = clsDBFuncationality.getSingleValue("select isnull(Price_KM,0) from TSPL_VEHICLE_MASTER   where  Vehicle_Id = '" + obj.Vehicle_Id + "' and Transport_Id = '" + obj.Transporter + "'", trans)

    '            If strRatePerKM <= 0 Then
    '                Throw New Exception("First Enter Price Per KM for Vehicle ID : " + obj.Vehicle_Id + " in Vehicle Master.")
    '            End If

    '            Dim dclDistance As Decimal = clsDBFuncationality.getSingleValue("select isNull(Distance,0) from tspl_Route_MAster where Route_No = '" + obj.Route_No + "'", trans)
    '            If dclDistance <= 0 Then
    '                Throw New Exception("First Enter +ve Distance for Route No : " + obj.Route_No + " in Route Master.")
    '            End If

    '            If (clsCommon.myCdbl(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateProvisionOnOpeningAndClosingKM, clsFixedParameterCode.CreateProvisionOnOpeningAndClosingKM, trans))) = 1) Then
    '                If dclDistance > (obj.Closing_Km - obj.Opening_Km) Then
    '                    dclDistance = (obj.Closing_Km - obj.Opening_Km)
    '                End If
    '            End If
    '            ' Ticket No : ERO/23/05/19-000619 By Prabhakar
    '            objProv.Amount = strRatePerKM * dclDistance + obj.Toll_Amount
    '            objProv.Prog_Code = FormId
    '            objProv.Prov_Month = Month(objProv.Doc_Date)
    '            objProv.Prov_Year = Year(objProv.Doc_Date)
    '            objProv.Comp_Code = objCommonVar.CurrentCompanyCode
    '            objProv.Created_By = objCommonVar.CurrentUserCode
    '            objProv.Created_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
    '            objProv.Modified_By = objCommonVar.CurrentUserCode
    '            objProv.Loc_Code = obj.Location_Code
    '            objProv.Loc_Desc = obj.Location_Desc
    '            objProv.Route_Code = obj.Route_No
    '            objProv.Toll_Amt = obj.Toll_Amount
    '            clsProvisionEntry.SaveData(objProv, trans)
    '            clsProvisionEntry.PostData(objProv.Doc_No, trans, True)
    '        End If
    '    End If

    '    Return True
    'End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
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
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Complain No not found to Delete")
        End If
        Dim obj As clsCustomerComplainHead = clsCustomerComplainHead.GetData(strCode, NavigatorType.Current, "", trans)
        ''Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Complaint_No) <= 0) Then
                Throw New Exception("Complain No not found to Delete")
            End If
            If clsCommon.CompairString(obj.IsPosted, "Y") = CompairStringResult.Equal Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = Nothing
            qry = "delete from TSPL_CUSTOMER_COMPLAINT_DETAIL where Complaint_No='" + obj.Complaint_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CUSTOMER_COMPLAINT_HEAD where Complaint_No='" + obj.Complaint_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    'Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try
    '        Dim Qry As String = "select Post from TSPL_DAIRYSALE_GATEPASS_MASTER where GPCode='" + strCode + "'"
    '        If Not clsCommon.CompairString("Y", clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry, trans))) = CompairStringResult.Equal Then
    '            Throw New Exception("Transaction status should be posted for reverse and unpost")
    '        End If

    '        Dim strProvNo As String = clsDBFuncationality.getSingleValue("select Doc_No from TSPL_PROVISION_ENTRY where Ref_Doc_No='" + strCode + "' and Prov_type = 'Dairy GatePass'", trans)
    '        If clsCommon.myLen(strProvNo) > 0 Then
    '            clsProvisionEntry.ReverseAndUnpost(strProvNo, trans)
    '            clsProvisionEntry.deleteData(strProvNo, trans)
    '        End If



    '        Qry = "update TSPL_DAIRYSALE_GATEPASS_MASTER set post='N',Closing_Km=0 where GPCode='" + strCode + "'"
    '        clsDBFuncationality.ExecuteNonQuery(Qry, trans)

    '        trans.Commit()
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function

End Class
Public Class clsCustomerComplainDetail
#Region "Variables"
    Public SNo As Integer = Nothing
    Public TR_CODE As String = Nothing
    Public Complaint_No As String = Nothing
    Public Complaint_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public HSN_Code As String = Nothing
    Public Unit_Code As String = Nothing
    Public Qty As Double = 0
    Public Damage_Qty As Double = 0
    Public Damage_Uom As String = Nothing
    Public IsTaxable As Integer = 0

#End Region

    Public Shared Function SaveData(ByVal strComplainNo As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsCustomerComplainDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCustomerComplainDetail In Arr
                Dim coll As New Hashtable()
                obj.TR_CODE = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_CODE", obj.TR_CODE)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "Complaint_No", strComplainNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Damage_Qty", obj.Damage_Qty)
                clsCommon.AddColumnsForChange(coll, "Complaint_Code", obj.Complaint_Code, True)
                clsCommon.AddColumnsForChange(coll, "Damage_Uom", obj.Damage_Uom)
                clsCommon.AddColumnsForChange(coll, "IsTaxable", obj.IsTaxable)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_COMPLAINT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsCustomerComplainDetail)
        Dim arr As List(Of clsCustomerComplainDetail) = Nothing
        Dim qry As String
        qry = " Select TSPL_CUSTOMER_COMPLAINT_DETAIL.*,TSPL_ITEM_MASTER.Item_Desc, TSPL_ITEM_MASTER.HSN_CODE from TSPL_CUSTOMER_COMPLAINT_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_CUSTOMER_COMPLAINT_DETAIL.ITEM_CODE where TSPL_CUSTOMER_COMPLAINT_DETAIL.Complaint_No = '" + strCode + "' order by TSPL_CUSTOMER_COMPLAINT_DETAIL.SNo asc "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsCustomerComplainDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsCustomerComplainDetail = New clsCustomerComplainDetail()
                obj.SNo = clsCommon.myCstr(dr("SNo"))
                obj.IsTaxable = clsCommon.myCstr(dr("IsTaxable"))
                obj.Complaint_No = clsCommon.myCstr(dr("Complaint_No"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                obj.HSN_Code = clsCommon.myCstr(dr("HSN_Code"))
                obj.Unit_Code = clsCommon.myCstr(dr("UOM"))
                obj.Qty = clsCommon.myCstr(dr("Qty"))
                obj.Damage_Uom = clsCommon.myCstr(dr("Damage_Uom"))
                obj.Damage_Qty = clsCommon.myCstr(dr("Damage_Qty"))
                obj.Complaint_Code = clsCommon.myCstr(dr("Complaint_Code"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class
