Imports common
Imports System.Data.SqlClient

Public Class clsTransporterDeductionHead
#Region "Variables"

    Public Document_Code As String = Nothing
    Public Document_Date As DateTime
    Public Transporter_code As String = Nothing
    ' Public Route_No As String = Nothing
    Public Remarks As String = Nothing
    Public From_Date As Date? = Nothing
    Public To_Date As Date? = Nothing
    Public IsPosted As String = Nothing
    Public PostedDate As Date? = Nothing
    Public Arr As List(Of clsTransporterDeductionDetail) = Nothing
    Public arrRoute As ArrayList
#End Region

    Public Function SaveData(ByVal obj As clsTransporterDeductionHead, ByVal isNewEntry As Boolean, ByVal strTransType As String) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            Dim qry As String = "delete from TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL  where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_TRANSPOTER_DEDUCTION_ENTRY_ROUTE  where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmTranspoterDeductionEntry, "", "")
            End If
            If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Transporter_code", obj.Transporter_code)

            'clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)

            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            If isNewEntry Then
                'obj.Complaint_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.CusomerComplain, "", "")
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER", OMInsertOrUpdate.Update, "TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsTransporterDeductionDetail.SaveData(obj.Document_Code, obj.Document_Date, obj.Arr, trans)
            clsTransporterDeductionRoute.SaveData(obj.arrRoute, obj.Document_Code, obj.Document_Date, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

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

    Public Shared Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType, ByVal TransType As String) As clsTransporterDeductionHead
        Return GetData(strRetCode, NavType, TransType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal TransType As String, ByVal trans As SqlTransaction) As clsTransporterDeductionHead
        Dim obj As clsTransporterDeductionHead = Nothing
        Dim qry As String = "select  TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER.* from TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER  where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER.Document_Code = (select MIN(Document_Code) from TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER)"
            Case NavigatorType.Last
                qry += " and TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER.Document_Code = (select Max(Document_Code) from TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER)"
            Case NavigatorType.Next
                qry += " and TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER.Document_Code = (select Min(Document_Code) from TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER where Document_Code>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER.Document_Code = (select Max(Document_Code) from TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER where Document_Code<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER.Document_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsTransporterDeductionHead()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Transporter_code = clsCommon.myCstr(dt.Rows(0)("Transporter_code"))

            'obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.From_Date = clsCommon.myCDate(dt.Rows(0)("From_Date"))
            obj.To_Date = clsCommon.myCDate(dt.Rows(0)("To_Date"))
            obj.IsPosted = clsCommon.myCstr(dt.Rows(0)("IsPosted"))

        End If
        obj.Arr = clsTransporterDeductionDetail.GetData(obj.Document_Code, trans)
        obj.arrRoute = clsTransporterDeductionRoute.GetData(obj.Document_Code, trans)
        Return obj
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document Code not found to Post")
            End If
            Dim obj As clsTransporterDeductionHead = clsTransporterDeductionHead.GetData(strDocNo, NavigatorType.Current, "", trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.IsPosted = "Y") Then
                Throw New Exception("Already Posted")
            End If
            'If (clsCommon.myCdbl(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateProvisionOnOpeningAndClosingKM, clsFixedParameterCode.CreateProvisionOnOpeningAndClosingKM, trans))) = 0) Then
            '    CreateProvison(obj, FormId, trans)
            'End If
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER set IsPosted='Y', Modified_By = '" + objCommonVar.CurrentUserCode + "',Modified_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "'  ,PostedDate = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "'  where Document_Code='" & obj.Document_Code & "'", trans)
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

            Dim Qry As String = "select IsPosted from TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER where Document_Code='" + strCode + "'"
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry, trans)), "N") = CompairStringResult.Equal Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            'Qry = "Select document_code  from TSPL_SD_SHIPMENT_HEAD WHERE Customer_Complaint_No='" & strCode & "' "
            'Dim strShipmentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry, trans))

            'If clsPSShipmentHead.ReverseAndUnpost(strShipmentNo, trans) Then
            '    Qry = "Select Sale_Invoice_No  from TSPL_SD_SHIPMENT_HEAD WHERE Customer_Complaint_No='" & strCode & "' "
            '    Dim strInvoiceNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry, trans))
            '    clsPSShipmentHead.DeleteData(strShipmentNo, strInvoiceNo, trans)
            'End If

            Qry = "Update TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER set IsPosted = 'N' where Document_Code='" + strCode + "'"
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
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document Code not found to Delete")
        End If
        Dim obj As clsTransporterDeductionHead = clsTransporterDeductionHead.GetData(strCode, NavigatorType.Current, "")
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Decoument Code not found to Delete")
            End If
            If clsCommon.CompairString(obj.IsPosted, "Y") = CompairStringResult.Equal Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = Nothing

            qry = "delete from TSPL_TRANSPOTER_DEDUCTION_ENTRY_ROUTE where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
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
Public Class clsTransporterDeductionDetail
#Region "Variables"
    ' Document_Code,Deduction_Date,SOC_NoOfCrates,SOC_NoOfCrates_Amount,ELOM_QTY,ELOM_Amount,VC_TopLess,VC_LOGO,VC_InnerBodyPainting,VC_Cleaniness
    ' VC_BottomDamage , VC_Shelf ,VC_Light,VC_Amount,LateVehicleReport_Amount , ShortageOfLoadingStaffSupervisors_Amount , Net_Amount , Remarks
    Public SNo As Integer = Nothing
    Public TR_CODE As String = Nothing
    Public Document_Code As String = Nothing
    Public Deduction_Date As Date? = Nothing
    Public SOC_NoOfCrates As Double = 0
    Public SOC_NoOfCrates_Amount As Double = 0
    Public ELOM_QTY As Double = 0
    Public ELOM_Amount As Double = 0
    Public VC_TopLess As Double = 0
    Public VC_LOGO As Double = 0
    Public VC_InnerBodyPainting As Double = 0
    Public VC_Cleaniness As Double = 0
    Public VC_BottomDamage As Double = 0
    Public VC_Shelf As Double = 0
    Public VC_Light As Double = 0
    Public VC_Amount As Double = 0
    Public LateVehicleReport_Amount As Double = 0
    Public ShortageOfLoadingStaffSupervisors_Amount As Double = 0
    Public Net_Amount As Double = 0
    Public Remarks As String = Nothing



#End Region

    Public Shared Function SaveData(ByVal strDocumentCode As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsTransporterDeductionDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsTransporterDeductionDetail In Arr
                Dim coll As New Hashtable()
                obj.TR_CODE = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_CODE", obj.TR_CODE)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocumentCode)
                clsCommon.AddColumnsForChange(coll, "Deduction_Date", clsCommon.GetPrintDate(obj.Deduction_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "SOC_NoOfCrates", obj.SOC_NoOfCrates)
                clsCommon.AddColumnsForChange(coll, "SOC_NoOfCrates_Amount", obj.SOC_NoOfCrates_Amount)
                clsCommon.AddColumnsForChange(coll, "ELOM_QTY", obj.ELOM_QTY)
                clsCommon.AddColumnsForChange(coll, "ELOM_Amount", obj.ELOM_Amount)
                clsCommon.AddColumnsForChange(coll, "VC_TopLess", obj.VC_TopLess)

                clsCommon.AddColumnsForChange(coll, "VC_LOGO", obj.VC_LOGO)
                clsCommon.AddColumnsForChange(coll, "VC_InnerBodyPainting", obj.VC_InnerBodyPainting)
                clsCommon.AddColumnsForChange(coll, "VC_Cleaniness", obj.VC_Cleaniness)
                clsCommon.AddColumnsForChange(coll, "VC_BottomDamage", obj.VC_BottomDamage)
                clsCommon.AddColumnsForChange(coll, "VC_Shelf", obj.VC_Shelf)
                clsCommon.AddColumnsForChange(coll, "VC_Light", obj.VC_Light)
                clsCommon.AddColumnsForChange(coll, "VC_Amount", obj.VC_Amount)
                clsCommon.AddColumnsForChange(coll, "LateVehicleReport_Amount", obj.LateVehicleReport_Amount)
                clsCommon.AddColumnsForChange(coll, "ShortageOfLoadingStaffSupervisors_Amount", obj.ShortageOfLoadingStaffSupervisors_Amount)
                clsCommon.AddColumnsForChange(coll, "Net_Amount", obj.Net_Amount)

                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsTransporterDeductionDetail)
        Dim arr As List(Of clsTransporterDeductionDetail) = Nothing
        Dim qry As String
        qry = " Select TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.* from TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL where TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL.Document_Code = '" + strCode + "' order by Convert (date, Deduction_Date,103)  asc "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsTransporterDeductionDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsTransporterDeductionDetail = New clsTransporterDeductionDetail()
                obj.SNo = clsCommon.myCstr(dr("SNo"))
                obj.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                obj.Deduction_Date = clsCommon.myCDate(dr("Deduction_Date"))
                obj.SOC_NoOfCrates = clsCommon.myCdbl(dr("SOC_NoOfCrates"))
                obj.SOC_NoOfCrates_Amount = clsCommon.myCdbl(dr("SOC_NoOfCrates_Amount"))
                obj.ELOM_QTY = clsCommon.myCdbl(dr("ELOM_QTY"))
                obj.ELOM_Amount = clsCommon.myCdbl(dr("ELOM_Amount"))
                obj.VC_TopLess = clsCommon.myCdbl(dr("VC_TopLess"))
                obj.VC_LOGO = clsCommon.myCdbl(dr("VC_LOGO"))
                obj.VC_InnerBodyPainting = clsCommon.myCdbl(dr("VC_InnerBodyPainting"))
                obj.VC_Cleaniness = clsCommon.myCdbl(dr("VC_Cleaniness"))
                obj.VC_BottomDamage = clsCommon.myCdbl(dr("VC_BottomDamage"))
                obj.VC_Shelf = clsCommon.myCdbl(dr("VC_Shelf"))
                obj.VC_Light = clsCommon.myCdbl(dr("VC_Light"))
                obj.VC_Amount = clsCommon.myCdbl(dr("VC_Amount"))
                obj.LateVehicleReport_Amount = clsCommon.myCdbl(dr("LateVehicleReport_Amount"))
                obj.ShortageOfLoadingStaffSupervisors_Amount = clsCommon.myCdbl(dr("ShortageOfLoadingStaffSupervisors_Amount"))
                obj.Net_Amount = clsCommon.myCdbl(dr("Net_Amount"))
                obj.Remarks = clsCommon.myCstr(dr("Remarks"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsTransporterDeductionRoute
#Region "Variables"
    Public TR_Code As String = Nothing
    Public Document_Code As String = Nothing
    Public Route_Code As String
#End Region

    Public Shared Function SaveData(ByVal Arr As ArrayList, ByVal strDocCode As String, ByVal dtDocDate As Date, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each strRouteCode As String In Arr
                Dim coll As New Hashtable()
                Dim strTRCode As String = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_Code", strTRCode)
                clsCommon.AddColumnsForChange(coll, "Route_Code", strRouteCode)
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocCode)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRANSPOTER_DEDUCTION_ENTRY_ROUTE", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As ArrayList
        Dim arr As ArrayList = Nothing
        Dim qry As String = "select TSPL_TRANSPOTER_DEDUCTION_ENTRY_ROUTE.Route_Code from TSPL_TRANSPOTER_DEDUCTION_ENTRY_ROUTE where TSPL_TRANSPOTER_DEDUCTION_ENTRY_ROUTE.Document_Code='" + strDocNo + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New ArrayList()
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr("Route_Code")))
            Next
        End If
        Return arr
    End Function
End Class

