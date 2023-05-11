Imports common
Imports System.Data.SqlClient
Public Class clsGatePassHeadPS
#Region "Variables"
    Public GPCode As String = Nothing
    Public GPDate As DateTime
    Public From_Date As DateTime
    Public To_Date As DateTime
    Public Location_Code As String = Nothing
    Public Location_Desc As String = Nothing
    Public City_Code As String = Nothing
    Public City_Desc As String = Nothing
    Public Vehicle_Id As String = Nothing
    Public Vehicle_Number As String = Nothing
    Public Transporter_code As String = Nothing
    Public Transporter_Name As String = Nothing
    Public Remarks As String = Nothing
    Public Comments As String = Nothing
    Public Post As String = Nothing
    Public Gross_Weight As Decimal = 0
    Public Capacity As Decimal = 0
    Public Provision_No As String = Nothing
    Public Provision_Amt As Decimal = 0
    Public Man_Vehicle_Number As String = Nothing
    Public Arr As List(Of clsGatePassDetailPS) = Nothing
    Public Is_Own_Vehicle As Integer = 0
#End Region
    Public Function SaveData(ByVal obj As clsGatePassHeadPS, ByVal isNewEntry As Boolean, ByVal strTransType As String) As Boolean

        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            Dim qry As String = "delete from TSPL_GATEPASS_DETAIL_ProductSale where GPcode='" + obj.GPCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.GPCode = clsERPFuncationality.GetNextCode(trans, obj.GPDate, clsDocType.FrmGatePassPS, "", "")
            End If
            If (clsCommon.myLen(obj.GPCode) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "GPDate", clsCommon.GetPrintDate(obj.GPDate, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Id", obj.Vehicle_Id)
            clsCommon.AddColumnsForChange(coll, "Transporter_code", obj.Transporter_code)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Total_Gross_Weight", obj.Gross_Weight)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Capacity", obj.Capacity)
            clsCommon.AddColumnsForChange(coll, "City_Code", obj.City_Code)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Man_Vehicle_Number", obj.Man_Vehicle_Number)
            clsCommon.AddColumnsForChange(coll, "Is_Own_Vehicle", obj.Is_Own_Vehicle)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "GPCode", obj.GPCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GATEPASS_MASTER_ProductSale", OMInsertOrUpdate.Insert, "", trans)

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GATEPASS_MASTER_ProductSale", OMInsertOrUpdate.Update, "TSPL_GATEPASS_MASTER_ProductSale.GPCode='" + obj.GPCode + "'", trans)
            End If

            isSaved = isSaved AndAlso clsGatePassDetailPS.SaveData(obj.GPCode, obj.Arr, trans)

            'qry = "Update TSPL_SD_SHIPMENT_HEAD set GPCode='" & obj.GPCode & "' where  convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(obj.GPDate, "") & "' and isnull(GPCode,'') = '' and Trans_Type='FS' and Bill_To_Location='" & obj.Location_Code & "' and Vehicle_Code='" & obj.Vehicle_Id & "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType, ByVal TransType As String) As clsGatePassHeadPS
        Return GetData(strRetCode, NavType, TransType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal TransType As String, ByVal trans As SqlTransaction) As clsGatePassHeadPS
        Dim obj As clsGatePassHeadPS = Nothing
        Dim qry As String = "select TSPL_GATEPASS_MASTER_ProductSale.Is_Own_Vehicle,TSPL_GATEPASS_MASTER_ProductSale.Provision_No,TSPL_GATEPASS_MASTER_ProductSale.Provision_Amt,TSPL_CITY_MASTER.City_Name,TSPL_GATEPASS_MASTER_ProductSale.Vehicle_Capacity , TSPL_GATEPASS_MASTER_ProductSale.city_code,TSPL_GATEPASS_MASTER_ProductSale.GPCode,TSPL_GATEPASS_MASTER_ProductSale.GPDate,TSPL_GATEPASS_MASTER_ProductSale.from_date,TSPL_GATEPASS_MASTER_ProductSale.To_date,TSPL_GATEPASS_MASTER_ProductSale.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_GATEPASS_MASTER_ProductSale.Vehicle_Id,TSPL_VEHICLE_MASTER.Description as Vehicle_Number,TSPL_GATEPASS_MASTER_ProductSale.Man_Vehicle_Number ,TSPL_GATEPASS_MASTER_ProductSale.Transporter_code,TSPL_TRANSPORT_MASTER.Transporter_Name ,TSPL_GATEPASS_MASTER_ProductSale.Remarks,TSPL_GATEPASS_MASTER_ProductSale.Comments,Post,TSPL_GATEPASS_MASTER_ProductSale.Total_Gross_Weight from TSPL_GATEPASS_MASTER_ProductSale  left join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_GATEPASS_MASTER_ProductSale.Transporter_code left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GATEPASS_MASTER_ProductSale.Location_Code LEFT JOIN TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_GATEPASS_MASTER_ProductSale.Vehicle_Id left join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_GATEPASS_MASTER_ProductSale.City_code where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_GATEPASS_MASTER_ProductSale.GPCode = (select MIN(GPCode) from TSPL_GATEPASS_MASTER_ProductSale)"
            Case NavigatorType.Last
                qry += " and TSPL_GATEPASS_MASTER_ProductSale.GPCode = (select Max(GPCode) from TSPL_GATEPASS_MASTER_ProductSale)"
            Case NavigatorType.Next
                qry += " and TSPL_GATEPASS_MASTER_ProductSale.GPCode = (select Min(GPCode) from TSPL_GATEPASS_MASTER_ProductSale where GPCode>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_GATEPASS_MASTER_ProductSale.GPCode = (select Max(GPCode) from TSPL_GATEPASS_MASTER_ProductSale where GPCode<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_GATEPASS_MASTER_ProductSale.GPCode = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsGatePassHeadPS()
            obj.GPCode = clsCommon.myCstr(dt.Rows(0)("GPCode"))
            obj.GPDate = clsCommon.myCDate(dt.Rows(0)("GPDate"))
            obj.From_Date = clsCommon.myCDate(dt.Rows(0)("From_Date"))
            obj.To_Date = clsCommon.myCDate(dt.Rows(0)("To_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.Vehicle_Id = clsCommon.myCstr(dt.Rows(0)("Vehicle_Id"))
            obj.Vehicle_Number = clsCommon.myCstr(dt.Rows(0)("Vehicle_Number"))
            obj.Transporter_code = clsCommon.myCstr(dt.Rows(0)("Transporter_code"))
            obj.Transporter_Name = clsCommon.myCstr(dt.Rows(0)("Transporter_Name"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Post = clsCommon.myCstr(dt.Rows(0)("Post"))
            obj.City_Code = clsCommon.myCstr(dt.Rows(0)("City_Code"))
            obj.City_Desc = clsCommon.myCstr(dt.Rows(0)("City_Name"))
            obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Total_Gross_Weight"))
            obj.Capacity = clsCommon.myCdbl(dt.Rows(0)("Vehicle_Capacity"))
            obj.Provision_No = clsCommon.myCstr(dt.Rows(0)("Provision_No"))
            obj.Provision_Amt = clsCommon.myCdbl(dt.Rows(0)("Provision_Amt"))
            obj.Man_Vehicle_Number = clsCommon.myCstr(dt.Rows(0)("Man_Vehicle_Number"))
            obj.Is_Own_Vehicle = clsCommon.myCdbl(dt.Rows(0)("Is_Own_Vehicle"))
            obj.Arr = clsGatePassDetailPS.GetData(obj.GPCode, trans, obj.GPDate, TransType)


        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsGatePassHeadPS = clsGatePassHeadPS.GetData(strCode, NavigatorType.Current, "PS")
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.GPCode) > 0) Then
            Try
                
                'If (obj.Status = 1) Then
                '    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                'End If
                
                Dim qry As String = "delete from TSPL_GATEPASS_Detail_ProductSale where GPcode='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_GATEPASS_master_ProductSale where GPcode='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


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

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
   

    Public Shared Function PostData(ByVal strDocNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean

        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception(" Gate Pass No. not found to Post")
            End If
            Dim qry As String = Nothing
            Dim obj As clsGatePassHeadPS = Nothing
            obj = clsGatePassHeadPS.GetData(strDocNo, NavigatorType.Current, "", trans)
            '========================
            If clsCommon.myLen(obj.Transporter_code) > 0 Then
                'If clsCommon.CompairString(obj.Dispatch_Terms, "CIF") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Dispatch_Terms, "CF") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Dispatch_Terms, "FE") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Dispatch_Terms, "O") = CompairStringResult.Equal Then
                ConvertProvision(obj, trans)
               
                'End If
            End If

            '========================

            qry = "Update TSPL_GATEPASS_master_ProductSale set Post='Y' where GPCode='" + strDocNo + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Shared Function GetProvisionCharge(ByVal Loc_Code As String, ByVal City_Code As String, ByVal gross_wt As Decimal, ByVal Capacity As Decimal, Optional ByVal Transport_Id As String = Nothing) As Decimal
        Dim value As Decimal = 0
        Dim GrossWeightUnit As String = Nothing
        Dim Weight_MT_Unit As String = Nothing
        Dim Freight_Type As String = Nothing
        Dim FixedCharge As Double = 0
        Dim EmptyCharge As Double = 0
        Dim qry As String = Nothing
        Dim CapacityINMT As Double = 0

        GrossWeightUnit = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_FIXED_PARAMETER where code='" + clsFixedParameterCode.GrossWeightUnit + "' and type='" + clsFixedParameterType.GrossWeightUnit + "'"))
        'Dim city As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select city_code from TSPL_CITY_MASTER where city_code='" + City_Code + "'"))
        If clsCommon.myCdbl(Capacity) > 0 Then
            qry = "select top 1 capacitymt,freight,Fixed,Type from TSPL_ROUTE_FREIGHT_DETAILS where location_code='" + Loc_Code + "' and city_code='" + City_Code + "' and capacitymt='" + clsCommon.myCstr(Capacity) + "' and transport_id='" + Transport_Id + "' and TransType='P' order by effective_date desc"
        Else
            qry = "select top 1 capacitymt,freight,Fixed,Type from TSPL_ROUTE_FREIGHT_DETAILS where location_code='" + Loc_Code + "' and city_code='" + City_Code + "' and transport_id='" + Transport_Id + "' and TransType='P' order by effective_date desc"
        End If

        Weight_MT_Unit = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_FIXED_PARAMETER where code='" + clsFixedParameterCode.VehicleCapacityUnit + "' and type='" + clsFixedParameterType.VehicleCapacityUnit + "'"))
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim MT_CF As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Contained_Qty from TSPL_WEIGHT_CONVERSION WHERE Contained_UOM='" & GrossWeightUnit & "' AND Container_UOM='" & Weight_MT_Unit & "' and product_type='All' "))
        If MT_CF = 0 Then
            MT_CF = 1
        End If
        gross_wt = clsCommon.myCdbl(gross_wt) * MT_CF
       
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim capacitymt As Decimal = clsCommon.myCdbl(dt.Rows(0)("capacitymt"))
            Dim charge As Decimal = clsCommon.myCdbl(dt.Rows(0)("freight"))
            Dim Fixed As Decimal = clsCommon.myCdbl(dt.Rows(0)("Fixed"))
            Freight_Type = clsCommon.myCstr(dt.Rows(0)("Type"))
            FixedCharge = Fixed
            EmptyCharge = charge
            If clsCommon.CompairString(Freight_Type, "KG") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(capacitymt) <= 0 Then
                    value = System.Math.Round((gross_wt * charge), 2)
                Else
                    value = 0
                End If
            End If
            '======16/01/2019========
            CapacityINMT = Capacity * 1000
            '========================
            If clsCommon.CompairString(Freight_Type, "MT") = CompairStringResult.Equal Then
                If gross_wt > CapacityINMT Then
                    If charge > 0 Then
                        If CapacityINMT > 0 Then
                            value = System.Math.Round(((charge / CapacityINMT) * gross_wt) + Fixed, 2)
                        End If

                    Else
                        value = System.Math.Round(Fixed, 2)
                    End If

                ElseIf gross_wt <= CapacityINMT Then
                    value = charge + Fixed
                End If
            End If

        End If
        Return value
    End Function
   
    Private Shared Function ConvertProvision(ByVal objGP As clsGatePassHeadPS, ByVal trans As SqlTransaction) As clsProvisionEntry
        Dim Qry As String = "select TSPL_PROVISION_ENTRY.Doc_No from TSPL_PROVISION_ENTRY " & _
                            " left outer join TSPL_GATEPASS_MASTER_PRODUCTSALE on TSPL_PROVISION_ENTRY.Ref_Doc_No=TSPL_GATEPASS_MASTER_PRODUCTSALE.GPCode " & _
                            " where Prog_Code='GatePass-PS' and " & _
        "convert(date,Doc_Date,103)='" & clsCommon.GetPrintDate(objGP.GPDate, "dd/MMM/yyyy") & "' and TSPL_GATEPASS_MASTER_PRODUCTSALE.Vehicle_id='" & objGP.Vehicle_Id & "' and " & _
        "TSPL_PROVISION_ENTRY.Loc_Code='" & objGP.Location_Code & " ' and TSPL_PROVISION_ENTRY.Vendor_Code='" & objGP.Transporter_code & "' "

        Dim strProvisionNo = clsDBFuncationality.getSingleValue(Qry, trans)

        Qry = "Select Amount from TSPL_PROVISION_ENTRY where Doc_No='" & strProvisionNo & "'"
        Dim dblProvAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans))
        If clsCommon.myLen(strProvisionNo) = 0 Then
            Dim obj As New clsProvisionEntry()
            obj = New clsProvisionEntry()
            obj.isNewEntry = True
            obj.Doc_Date = objGP.GPDate
            obj.Vendor_Code = objGP.Transporter_code
            obj.Vendor_Desc = clsDBFuncationality.getSingleValue("Select TSPL_TRANSPORT_MASTER.Transporter_Name from TSPL_TRANSPORT_MASTER where Transport_Id='" & objGP.Transporter_code & "'", trans)
            obj.Vendor_Type = "Transporter For Product Sale"
            obj.Status = "No"
            obj.Ref_Doc_No = objGP.GPCode
            obj.Prov_type = "Freight"
            obj.Amount = Math.Round(GetProvisionCharge(objGP.Location_Code, objGP.City_Code, clsCommon.myCdbl(objGP.Gross_Weight), clsCommon.myCdbl(objGP.Capacity), clsCommon.myCstr(objGP.Transporter_code)), 2)
            obj.Prog_Code = clsUserMgtCode.FrmGatePassPS
            obj.Prov_Month = Month(objGP.GPDate)
            obj.Prov_Year = Year(objGP.GPDate)
            obj.Loc_Code = objGP.Location_Code
            obj.Loc_Desc = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Location_Desc   from TSPL_LOCATION_MASTER  where Location_Code ='" & objGP.Location_Code & "'", trans)
            'obj.Freight_Type = objGP.Freight_Type
            'obj.FixedCharge = objGP.FixedCharge
            'obj.EmptyCharge = objGP.EmptyCharge
            If clsProvisionEntry.SaveData(obj, trans) Then
                If clsProvisionEntry.PostData(obj.Doc_No, trans, True) Then

                End If
            End If
            'Else
            '    If dblProvAmount < objShipment.Freight_Charges Then
            '        Qry = "Update TSPL_PROVISION_ENTRY set Ref_Doc_No='" & objShipment.Sale_Invoice_No & "',Amount=" & objShipment.Freight_Charges & " where TSPL_PROVISION_ENTRY.Doc_No='" & strProvisionNo & "' "
            '        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            '    End If
            

        End If


        Return Nothing

    End Function

    Public Shared Function SaveDataForHistory(ByVal strCode As String, ByVal strProvCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = String.Empty

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_GATEPASS_MASTER_PRODUCTSALE", "GPCode", "TSPL_GATEPASS_Detail_PRODUCTSALE", "GPCode", trans)


            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where source_code='PR-EN' and source_doc_no=(Select Doc_No from TSPL_PROVISION_ENTRY where Ref_Doc_No ='" & clsCommon.myCstr(strCode) & "')", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            qry = "Delete from TSPL_PROVISION_ENTRY where Ref_Doc_No ='" & clsCommon.myCstr(strCode) & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Update TSPL_GATEPASS_MASTER_PRODUCTSALE set Post = 'N',Provision_No =NUll,Provision_Amt =0 where GPCode='" & clsCommon.myCstr(strCode) & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '==========preeti Gupta=========
            Dim InvoiceNo As String = Nothing
           
            qry = " select TSPL_GATEPASS_DETAIL_ProductSale.Invoice_No,TSPL_GATEPASS_MASTER_PRODUCTSALE.Provision_Amt ,TSPL_GATEPASS_MASTER_PRODUCTSALE.Total_Gross_Weight ,TSPL_GATEPASS_DETAIL_ProductSale.Documnet_Amount,TSPL_GATEPASS_DETAIL_ProductSale.Gross_Weight    from TSPL_GATEPASS_DETAIL_ProductSale" & _
                 " left join TSPL_GATEPASS_MASTER_PRODUCTSALE on TSPL_GATEPASS_MASTER_PRODUCTSALE.GPCode =TSPL_GATEPASS_DETAIL_ProductSale.GPCode  where TSPL_GATEPASS_MASTER_PRODUCTSALE.GPCode='" & clsCommon.myCstr(strCode) & " ' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    InvoiceNo = clsCommon.myCstr(dt.Rows(i)("Invoice_No"))
                    qry = "Update TSPL_SD_SHIPMENT_HEAD set Freight_Charges=0 where Sale_Invoice_No='" + InvoiceNo + "' "
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Next
            End If
            '===============================
            ''
            ''
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
End Class
Public Class clsGatePassDetailPS
#Region "Variables"
    Public GPCode As String = Nothing
    Public Invoice_No As String = Nothing
    Public Invoice_date As Date = Nothing
    Public cust_code As String = Nothing
    Public cust_Name As String = Nothing
    Public Transporter_code As String = Nothing
    Public Transporter_Name As String = Nothing
    Public Vehicle_Id As String = Nothing
    Public Vehicle_Number As String = Nothing
    Public Gross_Weight As Double = 0
    Public Documnet_Amount As Double = 0

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsGatePassDetailPS), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsGatePassDetailPS In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "GPCode", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Invoice_No", obj.Invoice_No)
                clsCommon.AddColumnsForChange(coll, "Invoice_date", clsCommon.GetPrintDate(obj.Invoice_date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "cust_code", obj.cust_code)
                clsCommon.AddColumnsForChange(coll, "Transporter_code", obj.Transporter_code)
                clsCommon.AddColumnsForChange(coll, "Vehicle_Id", obj.Vehicle_Id)
                clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)
                clsCommon.AddColumnsForChange(coll, "Documnet_Amount", obj.Documnet_Amount)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GATEPASS_Detail_ProductSale", OMInsertOrUpdate.Insert, "", trans)

            Next
        End If
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction, ByVal strdate As Date, ByVal strType As String) As List(Of clsGatePassDetailPS)
        Dim arr As List(Of clsGatePassDetailPS) = Nothing
        Dim qry As String

        qry = "select TSPL_GATEPASS_Detail_ProductSale.GPCode,TSPL_GATEPASS_Detail_ProductSale.Invoice_No,TSPL_GATEPASS_Detail_ProductSale.Invoice_date,TSPL_GATEPASS_Detail_ProductSale.cust_code,TSPL_CUSTOMER_MASTER.Customer_Name AS cust_Name,TSPL_GATEPASS_Detail_ProductSale.Transporter_code,TSPL_TRANSPORT_MASTER.Transporter_Name,TSPL_GATEPASS_Detail_ProductSale.Vehicle_Id,TSPL_VEHICLE_MASTER.Description as Vehicle_Number,TSPL_GATEPASS_Detail_ProductSale.Gross_Weight,TSPL_GATEPASS_Detail_ProductSale.Documnet_Amount from " & _
        " TSPL_GATEPASS_Detail_ProductSale left join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_GATEPASS_Detail_ProductSale.Transporter_code left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_GATEPASS_Detail_ProductSale.cust_code LEFT JOIN TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_GATEPASS_Detail_ProductSale.Vehicle_Id where TSPL_GATEPASS_Detail_ProductSale.GPCode='" + strCode + "' "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsGatePassDetailPS)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsGatePassDetailPS = New clsGatePassDetailPS()
                obj.GPCode = clsCommon.myCstr(dr("GPCode"))
                obj.Invoice_No = clsCommon.myCstr(dr("Invoice_No"))
                obj.Invoice_date = clsCommon.myCDate(dr("Invoice_date"))
                obj.cust_code = clsCommon.myCstr(dr("cust_code"))
                obj.cust_Name = clsCommon.myCstr(dr("cust_Name"))
                obj.Transporter_code = clsCommon.myCstr(dr("Transporter_code"))
                obj.Transporter_Name = clsCommon.myCstr(dr("Transporter_Name"))
                obj.Vehicle_Id = clsCommon.myCstr(dr("Vehicle_Id"))
                obj.Vehicle_Number = clsCommon.myCstr(dr("Vehicle_Number"))
                obj.Gross_Weight = clsCommon.myCdbl(dr("Gross_Weight"))
                obj.Documnet_Amount = clsCommon.myCdbl(dr("Documnet_Amount"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function

End Class
