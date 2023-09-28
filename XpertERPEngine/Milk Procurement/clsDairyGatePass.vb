Imports common
Imports System.Data.SqlClient

Public Class clsDairyGatePassEntry
#Region "Variables"
    Public GPCode As String = Nothing
    Public GPDate As DateTime
    Public Vehicle_Id As String = Nothing
    Public Vehicle_Number As String = Nothing
    Public DocNo As String = Nothing
    Public Docdate As Date = Nothing
    Public ToSalesmanCode As String = Nothing
    Public ToSalesmanname As String = Nothing
    Public Route_No As String = Nothing
    Public Route_Desc As String = Nothing
    Public Item_Type As String = Nothing
    Public Transporter As String = Nothing
    Public Salesman As String = Nothing
    Public Remarks As String = Nothing
    Public Comments As String = Nothing
    Public Post As String = Nothing
    Public Location_Code As String = Nothing
    Public Location_Desc As String = Nothing
    Public TotalCAN As Integer = 0
    Public TotalCrate As Integer = 0
    Public AgainstDocumentCode As String = Nothing
    Public Opening_Km As Decimal = 0
    Public Closing_Km As Decimal = 0
    Public Distance_In_Route As Decimal = 0
    Public Price_KM_In_Vehicle As Decimal = 0
    Public Toll_Amount As Decimal = 0
    Public Closing_Date As DateTime? = Nothing
    Public Arr As List(Of clsDairyGPDetail) = Nothing
    Public IsTransfer As Integer = 0
    Public AgainstTransferNo As String = String.Empty
    Public ShiftType As String = String.Empty
    Public Loading_Slip As String = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsDairyGatePassEntry, ByVal isNewEntry As Boolean, ByVal strTransType As String) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()


        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.frmTankerProvision, obj.Location_Code, obj.GPDate, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmDairyGatePass, obj.Location_Code, obj.GPDate, trans)
            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.GPCode), "TSPL_DAIRYSALE_GATEPASS_MASTER", "GPCode", "TSPL_DAIRYSALE_GATEPASS_DETAIL", "GPCode", trans)
            End If
            Dim qry As String = "delete from TSPL_DAIRYSALE_GATEPASS_DETAIL where GPcode='" + obj.GPCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                If clsCommon.CompairString(strTransType, "PS") = CompairStringResult.Equal Then
                    obj.GPCode = clsERPFuncationality.GetNextCode(trans, obj.GPDate, clsDocType.FrmDairyGatePass, "", "")
                Else
                    obj.GPCode = clsERPFuncationality.GetNextCode(trans, obj.GPDate, clsDocType.FrmDairyGatePass, "", "")
                End If
            End If
            If (clsCommon.myLen(obj.GPCode) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "GPDate", clsCommon.GetPrintDate(obj.GPDate, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Vehicle_Id", obj.Vehicle_Id)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Number", obj.Vehicle_Number)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
            clsCommon.AddColumnsForChange(coll, "Transporter", obj.Transporter)
            clsCommon.AddColumnsForChange(coll, "Salesman", obj.Salesman)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Desc", obj.Location_Desc)
            '=============Added by preeti Gupta Against ticket no[]
            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)
            clsCommon.AddColumnsForChange(coll, "TotalCAN", obj.TotalCAN)
            clsCommon.AddColumnsForChange(coll, "TotalCrate", obj.TotalCrate)
            '============================================================================
            clsCommon.AddColumnsForChange(coll, "Opening_Km", obj.Opening_Km)

            clsCommon.AddColumnsForChange(coll, "IsTransfer", obj.IsTransfer)
            clsCommon.AddColumnsForChange(coll, "AgainstTransferNo", obj.AgainstTransferNo, True)
            clsCommon.AddColumnsForChange(coll, "ShiftType", obj.ShiftType, True)
            clsCommon.AddColumnsForChange(coll, "Loading_Slip", obj.Loading_Slip)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "GPCode", obj.GPCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DAIRYSALE_GATEPASS_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DAIRYSALE_GATEPASS_MASTER", OMInsertOrUpdate.Update, "TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode='" + obj.GPCode + "'", trans)
            End If

            isSaved = isSaved AndAlso clsDairyGPDetail.SaveData(obj.GPCode, obj.Arr, trans)
            ''richa agarwal 22 Nov,2019 ERO/22/11/19-001129
            '' qry = "Update TSPL_SD_SHIPMENT_HEAD set GPCode='" & obj.GPCode & "' where  convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(obj.GPDate, "") & "' and isnull(GPCode,'') = '' and Trans_Type='FS' and Bill_To_Location='" & obj.Location_Code & "' and Vehicle_Code='" & obj.Vehicle_Id & "' and TSPL_SD_SHIPMENT_HEAD.Document_Code  in (" + AgainstDocumentCode + ")"

            If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateGatePassFromDemand, clsFixedParameterCode.CreateGatePassFromDemand, trans)) = 0, False, True) = True Then
                qry = "Update TSPL_DEMAND_BOOKING_DETAIL set Production_Remarks='" & obj.Remarks & "',GPCode='" & obj.GPCode & "' from TSPL_DEMAND_BOOKING_DETAIL left join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Document_No=TSPL_DEMAND_BOOKING_MASTER.Document_No where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)='" & clsCommon.GetPrintDate(obj.GPDate, "") & "' and TSPL_DEMAND_BOOKING_MASTER.Location_code='" & obj.Location_Code & "' and TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code ='" & obj.Vehicle_Id & "' and TSPL_DEMAND_BOOKING_MASTER.route_no='" & obj.Route_No & "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & obj.ShiftType & "'"
            Else
                'qry = "Update TSPL_SD_SHIPMENT_HEAD set GPCode='" & obj.GPCode & "' where  convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(obj.GPDate, "") & "' and isnull(GPCode,'') = '' and Trans_Type='FS' and Bill_To_Location='" & obj.Location_Code & "' and (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" & obj.Vehicle_Id & "'  and TSPL_SD_SHIPMENT_HEAD.Document_Code  in (" + AgainstDocumentCode + ")"
                qry = "Update TSPL_SD_SHIPMENT_HEAD set GPCode='" & obj.GPCode & "' where  convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(obj.GPDate, "") & "' and isnull(GPCode,'') = '' and Trans_Type='FS' and Bill_To_Location='" & obj.Location_Code & "' and TSPL_SD_SHIPMENT_HEAD.Document_Code  in (" + AgainstDocumentCode + ")"
            End If

            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function UpdateClosingKMAndCreateProvision(ByVal strDocNo As String, ByVal dclClosingKM As Decimal, ByVal dclDistanceInRoute As Decimal, ByVal dclPriceKMInVehicle As Decimal, ByVal dclTollAmt As Decimal, ByVal FormId As String) As Boolean
        If clsCommon.myLen(strDocNo) <= 0 Then
            Throw New Exception("No Document found")
        End If
        Dim obj As clsDairyGatePassEntry = clsDairyGatePassEntry.GetData(strDocNo, NavigatorType.Current, "")
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If Not clsCommon.CompairString(obj.Post, "Y") = CompairStringResult.Equal Then
                Throw New Exception("Document [" + strDocNo + "] should be posted")
            End If
            If obj.Opening_Km > dclClosingKM Then
                Throw New Exception("Opening KM [" + clsCommon.myCstr(obj.Opening_Km) + "] can't be greater than Closing KM [" + clsCommon.myCstr(dclClosingKM) + "]")
            End If
            obj.Closing_Km = dclClosingKM
            obj.Distance_In_Route = dclDistanceInRoute
            obj.Price_KM_In_Vehicle = dclPriceKMInVehicle
            obj.Toll_Amount = dclTollAmt
            obj.Closing_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") '  HH:mm:ss
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Closing_Km", dclClosingKM)
            clsCommon.AddColumnsForChange(coll, "Distance_In_Route", obj.Distance_In_Route, True)
            clsCommon.AddColumnsForChange(coll, "Price_KM_In_Vehicle", obj.Price_KM_In_Vehicle, True) ' 
            If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TollTaxMaster, clsFixedParameterCode.TollTaxMaster, trans)) = 0, False, True) = False Then
                clsCommon.AddColumnsForChange(coll, "Toll_Amount", dclTollAmt, True)
            Else
                clsCommon.AddColumnsForChange(coll, "Toll_Amount", obj.Toll_Amount, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Closing_Date", clsCommon.GetPrintDate(obj.Closing_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DAIRYSALE_GATEPASS_MASTER", OMInsertOrUpdate.Update, "TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode='" + obj.GPCode + "'", trans)

            CreateProvison(obj, "", trans)

            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType, ByVal TransType As String) As clsDairyGatePassEntry
        Return GetData(strRetCode, NavType, TransType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal TransType As String, ByVal trans As SqlTransaction) As clsDairyGatePassEntry
        Dim obj As clsDairyGatePassEntry = Nothing
        Dim qry As String = "select  TSPL_DAIRYSALE_GATEPASS_MASTER.*,tspl_route_master.Route_Desc from TSPL_DAIRYSALE_GATEPASS_MASTER left join tspl_route_master on tspl_route_master.route_no=TSPL_DAIRYSALE_GATEPASS_MASTER.route_no  where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode = (select MIN(GPCode) from TSPL_DAIRYSALE_GATEPASS_MASTER)"
            Case NavigatorType.Last
                qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode = (select Max(GPCode) from TSPL_DAIRYSALE_GATEPASS_MASTER)"
            Case NavigatorType.Next
                qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode = (select Min(GPCode) from TSPL_DAIRYSALE_GATEPASS_MASTER where GPCode>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode = (select Max(GPCode) from TSPL_DAIRYSALE_GATEPASS_MASTER where GPCode<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsDairyGatePassEntry()
            obj.GPCode = clsCommon.myCstr(dt.Rows(0)("GPCode"))
            obj.GPDate = clsCommon.myCDate(dt.Rows(0)("GPDate"))
            obj.Vehicle_Id = clsCommon.myCstr(dt.Rows(0)("Vehicle_Id"))
            obj.Vehicle_Number = clsCommon.myCstr(dt.Rows(0)("Vehicle_Number"))
            obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
            obj.Transporter = clsCommon.myCstr(dt.Rows(0)("Transporter"))
            obj.Salesman = clsCommon.myCstr(dt.Rows(0)("Salesman"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Post = clsCommon.myCstr(dt.Rows(0)("Post"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            '============Added by preeti Gupta Against ticket no[BHA/02/08/18-000212]
            obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            obj.Route_Desc = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
            obj.TotalCrate = clsCommon.myCdbl(dt.Rows(0)("TotalCrate"))
            obj.TotalCAN = clsCommon.myCdbl(dt.Rows(0)("TotalCAN"))
            obj.IsTransfer = clsCommon.myCdbl(dt.Rows(0)("IsTransfer"))
            obj.AgainstTransferNo = clsCommon.myCstr(dt.Rows(0)("AgainstTransferNo"))
            obj.ShiftType = clsCommon.myCstr(dt.Rows(0)("ShiftType"))
            '===========================================================
            'obj.Arr = clsGPDetail.GetData(obj.GPCode, trans, obj.GPDate, TransType)
            obj.Opening_Km = clsCommon.myCdbl(dt.Rows(0)("Opening_Km"))
            obj.Closing_Km = clsCommon.myCdbl(dt.Rows(0)("Closing_Km"))
            obj.Toll_Amount = clsCommon.myCdbl(dt.Rows(0)("Toll_Amount"))
            If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("Closing_Date"))) > 0 Then
                obj.Closing_Date = clsCommon.myCDate(dt.Rows(0)("Closing_Date"))
            End If
            obj.Loading_Slip = clsCommon.myCstr(dt.Rows(0)("Loading_Slip"))
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As clsDairyGatePassEntry = clsDairyGatePassEntry.GetData(strDocNo, NavigatorType.Current, "", trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.GPCode) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmDairyGatePass, obj.Location_Code, obj.GPDate, trans)


            If (obj.Post = "Y") Then
                Throw New Exception("Already Posted")
            End If
            If (clsCommon.myCdbl(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateProvisionOnOpeningAndClosingKM, clsFixedParameterCode.CreateProvisionOnOpeningAndClosingKM, trans))) = 0) Then
                CreateProvison(obj, FormId, trans)
            End If
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_DAIRYSALE_GATEPASS_MASTER set post='Y', Modified_By = '" + objCommonVar.CurrentUserCode + "',Modified_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "'  where gpcode='" & obj.GPCode & "'", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.GPCode), "TSPL_DAIRYSALE_GATEPASS_MASTER", "GPCode", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CreateProvison(ByVal obj As clsDairyGatePassEntry, ByVal FormId As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isTranspoterVendor As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_VENDOR_MASTER where Transporter ='Y' and Vendor_Code = '" + obj.Transporter + "'", trans))
            '==========Added by preeti gupta Against ticket no[ERO/16/05/19-000603]
            Dim isDepoVehicle As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_VEHICLE_MASTER where  Vehicle_Type ='D' and Vehicle_Id = '" + obj.Vehicle_Id + "'", trans))
            If isDepoVehicle = 0 Then
                If clsCommon.myLen(obj.Transporter) <= 0 Or isTranspoterVendor <= 0 Then
                    If (myMessages.DairyGatePassPovisionConfirm) Then
                    Else
                        Throw New Exception("")
                    End If
                Else
                    If isTranspoterVendor <= 0 Then
                        Throw New Exception("You can't create provision entry because Transpoter [" + obj.Transporter + "] is not Transpoter type Vendor.")
                    End If
                    Dim objProv As clsProvisionEntry = New clsProvisionEntry()
                    objProv.isNewEntry = True
                    objProv.Doc_Date = obj.GPDate 'clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
                    objProv.Vendor_Code = obj.Transporter
                    objProv.Vendor_Type = "Transpoter"
                    If obj.IsTransfer > 0 AndAlso clsCommon.myLen(obj.AgainstTransferNo) > 0 Then
                        objProv.Prov_type = "Dairy GatePass Transfer"
                    Else
                        objProv.Prov_type = " Dairy GatePass"
                    End If

                    objProv.Status = "No"
                    objProv.Ref_Doc_No = obj.GPCode

                    Dim StrBasisOfFreight As String = clsDBFuncationality.getSingleValue("select isnull(Status,'') from TSPL_VEHICLE_MASTER   where  Vehicle_Id = '" + obj.Vehicle_Id + "' and Transport_Id = '" + obj.Transporter + "'", trans)
                    If clsCommon.CompairString(StrBasisOfFreight, "Rate/Ltr") = CompairStringResult.Equal Then
                        Dim RatePerLtrKg As Double = clsDBFuncationality.getSingleValue("select isnull(Price_Ltr_KG,0) from TSPL_VEHICLE_MASTER   where  Vehicle_Id = '" + obj.Vehicle_Id + "' and Transport_Id = '" + obj.Transporter + "'", trans)
                        If RatePerLtrKg <= 0 Then
                            Throw New Exception("First Enter Price Per Ltr/Kg for Vehicle ID : " + obj.Vehicle_Id + " in Vehicle Master.")
                        End If
                        Dim StrQuery As String = "select  " &
                           " sum(Convert(Decimal(18, 2), (TSPL_DAIRYSALE_GATEPASS_Detail.qty / LtrUnit.conversion_factor) * StockUnit.conversion_factor * CurrentUnit.conversion_factor)) As Ltr_Qty from TSPL_DAIRYSALE_GATEPASS_MASTER  left join TSPL_DAIRYSALE_GATEPASS_Detail On TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode=TSPL_DAIRYSALE_GATEPASS_Detail.GPCode  left join tspl_item_uom_detail LtrUnit On LtrUnit.item_code=TSPL_DAIRYSALE_GATEPASS_Detail.item_code  left join TSPL_UNIT_MASTER On TSPL_UNIT_MASTER.unit_code=LtrUnit.uom_code " &
                           " left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_DAIRYSALE_GATEPASS_Detail.item_code  left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_DAIRYSALE_GATEPASS_Detail.item_code and      CurrentUnit.uom_code=       TSPL_DAIRYSALE_GATEPASS_Detail.unit_code  where  tspl_unit_master.Ltr_type ='Y' and StockUnit.stocking_unit='Y'  " &
                           " and TSPL_DAIRYSALE_GATEPASS_Detail.GPCode='" & obj.GPCode & "' Group by TSPL_DAIRYSALE_GATEPASS_Detail.GPCode "
                        Dim QtyInLtr As Double = clsDBFuncationality.getSingleValue(StrQuery, trans)
                        If QtyInLtr <= 0 Then
                            Throw New Exception("Quantity In Liter is zero.")
                        End If

                        objProv.Amount = Math.Round(RatePerLtrKg * QtyInLtr + obj.Toll_Amount, 2)
                    Else
                        Dim strRatePerKM As Double = clsDBFuncationality.getSingleValue("select isnull(Price_KM,0) from TSPL_VEHICLE_MASTER   where  Vehicle_Id = '" + obj.Vehicle_Id + "' and Transport_Id = '" + obj.Transporter + "'", trans)

                        If strRatePerKM <= 0 Then
                            Throw New Exception("First Enter Price Per KM for Vehicle ID : " + obj.Vehicle_Id + " in Vehicle Master.")
                        End If

                        Dim dclDistance As Decimal = clsDBFuncationality.getSingleValue("select isNull(Distance,0) from tspl_Route_MAster where Route_No = '" + obj.Route_No + "'", trans)
                        If dclDistance <= 0 Then
                            Throw New Exception("First Enter +ve Distance for Route No : " + obj.Route_No + " in Route Master.")
                        End If

                        If (clsCommon.myCdbl(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateProvisionOnOpeningAndClosingKM, clsFixedParameterCode.CreateProvisionOnOpeningAndClosingKM, trans))) = 1) Then
                            If dclDistance > (obj.Closing_Km - obj.Opening_Km) Then
                                dclDistance = (obj.Closing_Km - obj.Opening_Km)
                            End If
                        End If
                        ' Ticket No : ERO/23/05/19-000619 By Prabhakar
                        objProv.Amount = Math.Round(strRatePerKM * dclDistance + obj.Toll_Amount, 2)
                    End If

                    objProv.Prog_Code = FormId
                    objProv.Prov_Month = Month(objProv.Doc_Date)
                    objProv.Prov_Year = Year(objProv.Doc_Date)
                    objProv.Comp_Code = objCommonVar.CurrentCompanyCode
                    objProv.Created_By = objCommonVar.CurrentUserCode
                    objProv.Created_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
                    objProv.Modified_By = objCommonVar.CurrentUserCode
                    objProv.Loc_Code = obj.Location_Code
                    objProv.Loc_Desc = obj.Location_Desc
                    objProv.Route_Code = obj.Route_No
                    objProv.Toll_Amt = obj.Toll_Amount
                    clsProvisionEntry.SaveData(objProv, trans)
                    clsProvisionEntry.PostData(objProv.Doc_No, trans, True)
                End If
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)

        End Try


        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsDairyGatePassEntry = clsDairyGatePassEntry.GetData(strCode, NavigatorType.Current, "")

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (obj Is Nothing OrElse clsCommon.myLen(obj.GPCode) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.frmTankerProvision, obj.Location_Code, obj.GPDate, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmDairyGatePass, obj.Location_Code, obj.GPDate, trans)


            If clsCommon.CompairString(obj.Post, "Y") = CompairStringResult.Equal Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = ""
            If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateGatePassFromDemand, clsFixedParameterCode.CreateGatePassFromDemand, trans)) = 0, False, True) = True Then
                qry = "Update TSPL_DEMAND_BOOKING_DETAIL set GPCode = null where GPCode='" + obj.GPCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Else
                qry = "Update TSPL_SD_SHIPMENT_HEAD set GPCode = null where GPCode='" + obj.GPCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.GPCode), "TSPL_DAIRYSALE_GATEPASS_MASTER", "GPCode", "TSPL_DAIRYSALE_GATEPASS_DETAIL", "GPCode", trans)

            qry = "delete from TSPL_DAIRYSALE_GATEPASS_DETAIL where GPCode='" + obj.GPCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_DAIRYSALE_GATEPASS_MASTER where GPCode='" + obj.GPCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "select Post from TSPL_DAIRYSALE_GATEPASS_MASTER where GPCode='" + strCode + "'"
            If Not clsCommon.CompairString("Y", clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry, trans))) = CompairStringResult.Equal Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If
            Dim obj As clsDairyGatePassEntry = clsDairyGatePassEntry.GetData(strCode, NavigatorType.Current, "", trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.frmTankerProvision, obj.Location_Code, obj.GPDate, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmDairyGatePass, obj.Location_Code, obj.GPDate, trans)


            If (obj Is Nothing OrElse clsCommon.myLen(obj.GPCode) <= 0) Then
                Throw New Exception("No Data found to UnPost")
            End If

            If clsCommon.myLen(clsCommon.myCstr(obj.AgainstTransferNo)) > 0 Then
                Dim strTransferInNo As String = clsDBFuncationality.getSingleValue("select Document_No  from TSPL_TRANSFER_ORDER_HEAD where TransferOutNo='" & obj.AgainstTransferNo & "' ", trans)
                If clsCommon.myLen(clsCommon.myCstr(strTransferInNo)) > 0 Then
                    Throw New Exception("Document can't be unpost because its Transfer In " & strTransferInNo & " has created")
                End If
            End If

            '' to check bank reverse
            Qry = "select AP_Invoice_No,Provision_No from TSPL_PROVISION_ENTRY_KNOCKOFF where Invoice_No='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current document is used in following AP Invoice No - " + clsCommon.myCstr(dt.Rows(0)("AP_Invoice_No")) + " and Provision No " + clsCommon.myCstr(dt.Rows(0)("Provision_No"))
                Throw New Exception(Qry)
            End If


            Dim strProvNo As String = clsDBFuncationality.getSingleValue("select Doc_No from TSPL_PROVISION_ENTRY where Ref_Doc_No='" + strCode + "' and Prov_type='Dairy GatePass' ", trans)
            If clsCommon.myLen(strProvNo) > 0 Then
                clsProvisionEntry.ReverseAndUnpost(strProvNo, trans)
                clsProvisionEntry.deleteData(strProvNo, trans)
            End If



            Qry = "update TSPL_DAIRYSALE_GATEPASS_MASTER set post='N',Closing_Km=0 , Closing_Date = null where GPCode='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_DAIRYSALE_GATEPASS_MASTER", "GPCode", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class
Public Class clsDairyGPDetail
#Region "Variables"
    Public GPCode As String = Nothing
    Public DocNo As String = Nothing
    Public Docdate As Date = Nothing
    Public ToSalesmanCode As String = Nothing
    Public ToSalesmanname As String = Nothing
    Public Route_No As String = Nothing
    Public Route_Desc As String = Nothing
    Public Status As String = Nothing
    Public Type As String = Nothing
    Public Vehicle_Id As String = Nothing
    Public Vehicle_Number As String = Nothing
    Public Price_Code As String = Nothing
    Public Price_Desc As String = Nothing
    Public Cust_Code As String = Nothing
    Public Customer_Name As String = Nothing

    Public Item_Code As String = Nothing
    Public Unit_Code As String = Nothing
    Public Qty As Double = 0
    Public HSN_Code As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsDairyGPDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsDairyGPDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "GPCode", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "HSN_Code", obj.HSN_Code)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DAIRYSALE_GATEPASS_DETAIL", OMInsertOrUpdate.Insert, "", trans)

            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction, ByVal strdate As Date, ByVal strType As String) As List(Of clsDairyGPDetail)
        Dim arr As List(Of clsDairyGPDetail) = Nothing
        Dim qry As String
        If clsCommon.CompairString(strType, "PS") = CompairStringResult.Equal Then
            qry = "select GPCode,DocNo,convert(date,Docdate,103) as Docdate,ToSalesmanCode,ToSalesmanname, " & _
            "Route_No,Route_Desc,1 as Status,Type,Price_Code,Price_Desc,Cust_Code,Customer_Name from " & _
            "TSPL_DAIRYSALE_GATEPASS_DETAIL where TSPL_DAIRYSALE_GATEPASS_DETAIL.GPCode='" + strCode + "' " & _
            " Union all " & _
            "select '' as GPCode,Document_Code as DOCNo,Document_Date as Docdate,'' as ToSalesmanCode ,'' as ToSalesmanname,'' as Route_No,'' as Route_Desc, " & _
                    "0  as Status," & strType & " as Type,'' as Price_Code,'' as Price_Desc,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_SD_SALE_INVOICE_HEAD  " & _
                    "left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code where Trans_Type='" & strType & "' and " & _
                    "convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy") & "' AND  " & _
                    "Document_Code not in (select DOCno From TSPL_DAIRYSALE_GATEPASS_DETAIL ) "
        Else
            qry = "select GPCode,DocNo,convert(date,Docdate,103) as Docdate,ToSalesmanCode,ToSalesmanname, " & _
                      "Route_No,Route_Desc,1 as Status,Type,Price_Code,Price_Desc,Cust_Code,Customer_Name from " & _
                      "TSPL_DAIRYSALE_GATEPASS_DETAIL where TSPL_DAIRYSALE_GATEPASS_DETAIL.GPCode='" + strCode + "' " & _
                      " Union all " & _
                      "select '' as GPCode,Document_Code as DOCNo,Document_Date as Docdate,'' as ToSalesmanCode ,'' as ToSalesmanname, TSPL_SD_SALE_INVOICE_HEAD.Route_No, TSPL_ROUTE_MASTER.Route_Desc, " & _
                      "0  as Status,'" & strType & "' as Type, TSPL_SD_SALE_INVOICE_HEAD.Price_Code,(select distinct Price_Code_Desc from  TSPL_PRICE_COMPONENT_MAPPING  where price_code=TSPL_SD_SALE_INVOICE_HEAD.price_code)  as Price_Desc,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_SD_SALE_INVOICE_HEAD " & _
                     "left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  left outer join TSPL_ROUTE_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No where Trans_Type='" & strType & "' and " & _
                     "convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy") & "' AND  " & _
                     "Document_Code not in (select DOCno From TSPL_DAIRYSALE_GATEPASS_DETAIL ) "
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsDairyGPDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsDairyGPDetail = New clsDairyGPDetail()
                obj.GPCode = clsCommon.myCstr(dr("GPCode"))
                obj.DocNo = clsCommon.myCstr(dr("DocNo"))
                obj.Docdate = clsCommon.myCstr(dr("Docdate"))
                obj.ToSalesmanCode = clsCommon.myCstr(dr("ToSalesmanCode"))
                obj.ToSalesmanname = clsCommon.myCstr(dr("ToSalesmanname"))
                obj.Route_No = clsCommon.myCstr(dr("Route_No"))
                obj.Route_Desc = clsCommon.myCstr(dr("Route_Desc"))
                obj.Status = clsCommon.myCstr(dr("Status"))
                obj.Type = clsCommon.myCstr(dr("Type"))
                obj.Price_Code = clsCommon.myCstr(dr("Price_Code"))
                obj.Price_Desc = clsCommon.myCstr(dr("Price_Desc"))

                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class
